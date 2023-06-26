using System;
using System.Collections;
using System.Globalization;
using System.Net.Configuration;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200021D RID: 541
	internal class TlsStream : NetworkStream, IDisposable
	{
		// Token: 0x060013DD RID: 5085 RVA: 0x000697DC File Offset: 0x000679DC
		public TlsStream(string destinationHost, NetworkStream networkStream, bool checkCertificateRevocationList, SslProtocols sslProtocols, X509CertificateCollection clientCertificates, ServicePoint servicePoint, object initiatingRequest, ExecutionContext executionContext)
			: base(networkStream, true)
		{
			this.m_CheckCertificateRevocationList = checkCertificateRevocationList;
			this.m_SslProtocols = sslProtocols;
			this._ExecutionContext = executionContext;
			if (this._ExecutionContext == null)
			{
				this._ExecutionContext = ExecutionContext.Capture();
			}
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, this, ".ctor", string.Format("host={0}, #certs={1}, checkCertificateRevocationList={2}, sslProtocols={3}", new object[]
				{
					destinationHost,
					(clientCertificates == null) ? "null" : clientCertificates.Count.ToString(NumberFormatInfo.InvariantInfo),
					checkCertificateRevocationList,
					sslProtocols
				}));
			}
			this.m_ExceptionStatus = WebExceptionStatus.SecureChannelFailure;
			this.m_Worker = new SslState(networkStream, initiatingRequest is HttpWebRequest, SettingsSectionInternal.Section.EncryptionPolicy);
			this.m_DestinationHost = destinationHost;
			this.m_ClientCertificates = clientCertificates;
			RemoteCertValidationCallback remoteCertValidationCallback = servicePoint.SetupHandshakeDoneProcedure(this, initiatingRequest);
			this.m_Worker.SetCertValidationDelegate(remoteCertValidationCallback);
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060013DE RID: 5086 RVA: 0x000698D4 File Offset: 0x00067AD4
		internal WebExceptionStatus ExceptionStatus
		{
			get
			{
				return this.m_ExceptionStatus;
			}
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x000698DC File Offset: 0x00067ADC
		protected override void Dispose(bool disposing)
		{
			if (Interlocked.Exchange(ref this.m_ShutDown, 1) == 1)
			{
				return;
			}
			try
			{
				if (disposing)
				{
					this.m_CachedChannelBinding = this.GetChannelBinding(ChannelBindingKind.Endpoint);
					this.m_Worker.Close();
				}
				else
				{
					this.m_Worker = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x0006993C File Offset: 0x00067B3C
		public override bool DataAvailable
		{
			get
			{
				return this.m_Worker.DataAvailable || base.DataAvailable;
			}
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x00069954 File Offset: 0x00067B54
		public override int Read(byte[] buffer, int offset, int size)
		{
			if (!this.m_Worker.IsAuthenticated)
			{
				this.ProcessAuthentication(null);
			}
			int num;
			try
			{
				num = this.m_Worker.SecureStream.Read(buffer, offset, size);
			}
			catch
			{
				if (this.m_Worker.IsCertValidationFailed)
				{
					this.m_ExceptionStatus = WebExceptionStatus.TrustFailure;
				}
				else if (this.m_Worker.LastSecurityStatus != SecurityStatus.OK)
				{
					this.m_ExceptionStatus = WebExceptionStatus.SecureChannelFailure;
				}
				else
				{
					this.m_ExceptionStatus = WebExceptionStatus.ReceiveFailure;
				}
				throw;
			}
			return num;
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x000699D8 File Offset: 0x00067BD8
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback asyncCallback, object asyncState)
		{
			if (!this.m_Worker.IsAuthenticated)
			{
				BufferAsyncResult bufferAsyncResult = new BufferAsyncResult(this, buffer, offset, size, false, asyncState, asyncCallback);
				if (this.ProcessAuthentication(bufferAsyncResult))
				{
					return bufferAsyncResult;
				}
			}
			IAsyncResult asyncResult;
			try
			{
				asyncResult = this.m_Worker.SecureStream.BeginRead(buffer, offset, size, asyncCallback, asyncState);
			}
			catch
			{
				if (this.m_Worker.IsCertValidationFailed)
				{
					this.m_ExceptionStatus = WebExceptionStatus.TrustFailure;
				}
				else if (this.m_Worker.LastSecurityStatus != SecurityStatus.OK)
				{
					this.m_ExceptionStatus = WebExceptionStatus.SecureChannelFailure;
				}
				else
				{
					this.m_ExceptionStatus = WebExceptionStatus.ReceiveFailure;
				}
				throw;
			}
			return asyncResult;
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x00069A70 File Offset: 0x00067C70
		internal override IAsyncResult UnsafeBeginRead(byte[] buffer, int offset, int size, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginRead(buffer, offset, size, asyncCallback, asyncState);
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x00069A80 File Offset: 0x00067C80
		public override int EndRead(IAsyncResult asyncResult)
		{
			int num;
			try
			{
				BufferAsyncResult bufferAsyncResult = asyncResult as BufferAsyncResult;
				if (bufferAsyncResult == null || bufferAsyncResult.AsyncObject != this)
				{
					num = this.m_Worker.SecureStream.EndRead(asyncResult);
				}
				else
				{
					bufferAsyncResult.InternalWaitForCompletion();
					Exception ex = bufferAsyncResult.Result as Exception;
					if (ex != null)
					{
						throw ex;
					}
					num = (int)bufferAsyncResult.Result;
				}
			}
			catch
			{
				if (this.m_Worker.IsCertValidationFailed)
				{
					this.m_ExceptionStatus = WebExceptionStatus.TrustFailure;
				}
				else if (this.m_Worker.LastSecurityStatus != SecurityStatus.OK)
				{
					this.m_ExceptionStatus = WebExceptionStatus.SecureChannelFailure;
				}
				else
				{
					this.m_ExceptionStatus = WebExceptionStatus.ReceiveFailure;
				}
				throw;
			}
			return num;
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x00069B24 File Offset: 0x00067D24
		public override void Write(byte[] buffer, int offset, int size)
		{
			if (!this.m_Worker.IsAuthenticated)
			{
				this.ProcessAuthentication(null);
			}
			try
			{
				this.m_Worker.SecureStream.Write(buffer, offset, size);
			}
			catch
			{
				if (this.m_Worker.IsCertValidationFailed)
				{
					this.m_ExceptionStatus = WebExceptionStatus.TrustFailure;
				}
				else if (this.m_Worker.LastSecurityStatus != SecurityStatus.OK)
				{
					this.m_ExceptionStatus = WebExceptionStatus.SecureChannelFailure;
				}
				else
				{
					this.m_ExceptionStatus = WebExceptionStatus.SendFailure;
				}
				Socket socket = base.Socket;
				if (socket != null)
				{
					socket.InternalShutdown(SocketShutdown.Both);
				}
				throw;
			}
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x00069BB8 File Offset: 0x00067DB8
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback asyncCallback, object asyncState)
		{
			if (!this.m_Worker.IsAuthenticated)
			{
				BufferAsyncResult bufferAsyncResult = new BufferAsyncResult(this, buffer, offset, size, true, asyncState, asyncCallback);
				if (this.ProcessAuthentication(bufferAsyncResult))
				{
					return bufferAsyncResult;
				}
			}
			IAsyncResult asyncResult;
			try
			{
				asyncResult = this.m_Worker.SecureStream.BeginWrite(buffer, offset, size, asyncCallback, asyncState);
			}
			catch
			{
				if (this.m_Worker.IsCertValidationFailed)
				{
					this.m_ExceptionStatus = WebExceptionStatus.TrustFailure;
				}
				else if (this.m_Worker.LastSecurityStatus != SecurityStatus.OK)
				{
					this.m_ExceptionStatus = WebExceptionStatus.SecureChannelFailure;
				}
				else
				{
					this.m_ExceptionStatus = WebExceptionStatus.SendFailure;
				}
				throw;
			}
			return asyncResult;
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x00069C50 File Offset: 0x00067E50
		internal override IAsyncResult UnsafeBeginWrite(byte[] buffer, int offset, int size, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginWrite(buffer, offset, size, asyncCallback, asyncState);
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x00069C60 File Offset: 0x00067E60
		public override void EndWrite(IAsyncResult asyncResult)
		{
			try
			{
				BufferAsyncResult bufferAsyncResult = asyncResult as BufferAsyncResult;
				if (bufferAsyncResult == null || bufferAsyncResult.AsyncObject != this)
				{
					this.m_Worker.SecureStream.EndWrite(asyncResult);
				}
				else
				{
					bufferAsyncResult.InternalWaitForCompletion();
					Exception ex = bufferAsyncResult.Result as Exception;
					if (ex != null)
					{
						throw ex;
					}
				}
			}
			catch
			{
				Socket socket = base.Socket;
				if (socket != null)
				{
					socket.InternalShutdown(SocketShutdown.Both);
				}
				if (this.m_Worker.IsCertValidationFailed)
				{
					this.m_ExceptionStatus = WebExceptionStatus.TrustFailure;
				}
				else if (this.m_Worker.LastSecurityStatus != SecurityStatus.OK)
				{
					this.m_ExceptionStatus = WebExceptionStatus.SecureChannelFailure;
				}
				else
				{
					this.m_ExceptionStatus = WebExceptionStatus.SendFailure;
				}
				throw;
			}
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x00069D08 File Offset: 0x00067F08
		internal override void MultipleWrite(BufferOffsetSize[] buffers)
		{
			if (!this.m_Worker.IsAuthenticated)
			{
				this.ProcessAuthentication(null);
			}
			try
			{
				this.m_Worker.SecureStream.Write(buffers);
			}
			catch
			{
				Socket socket = base.Socket;
				if (socket != null)
				{
					socket.InternalShutdown(SocketShutdown.Both);
				}
				if (this.m_Worker.IsCertValidationFailed)
				{
					this.m_ExceptionStatus = WebExceptionStatus.TrustFailure;
				}
				else if (this.m_Worker.LastSecurityStatus != SecurityStatus.OK)
				{
					this.m_ExceptionStatus = WebExceptionStatus.SecureChannelFailure;
				}
				else
				{
					this.m_ExceptionStatus = WebExceptionStatus.SendFailure;
				}
				throw;
			}
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x00069D98 File Offset: 0x00067F98
		internal override IAsyncResult BeginMultipleWrite(BufferOffsetSize[] buffers, AsyncCallback callback, object state)
		{
			if (!this.m_Worker.IsAuthenticated)
			{
				BufferAsyncResult bufferAsyncResult = new BufferAsyncResult(this, buffers, state, callback);
				if (this.ProcessAuthentication(bufferAsyncResult))
				{
					return bufferAsyncResult;
				}
			}
			IAsyncResult asyncResult;
			try
			{
				asyncResult = this.m_Worker.SecureStream.BeginWrite(buffers, callback, state);
			}
			catch
			{
				if (this.m_Worker.IsCertValidationFailed)
				{
					this.m_ExceptionStatus = WebExceptionStatus.TrustFailure;
				}
				else if (this.m_Worker.LastSecurityStatus != SecurityStatus.OK)
				{
					this.m_ExceptionStatus = WebExceptionStatus.SecureChannelFailure;
				}
				else
				{
					this.m_ExceptionStatus = WebExceptionStatus.SendFailure;
				}
				throw;
			}
			return asyncResult;
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x00069E28 File Offset: 0x00068028
		internal override IAsyncResult UnsafeBeginMultipleWrite(BufferOffsetSize[] buffers, AsyncCallback callback, object state)
		{
			return this.BeginMultipleWrite(buffers, callback, state);
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x00069E33 File Offset: 0x00068033
		internal override void EndMultipleWrite(IAsyncResult asyncResult)
		{
			this.EndWrite(asyncResult);
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060013ED RID: 5101 RVA: 0x00069E3C File Offset: 0x0006803C
		public X509Certificate ClientCertificate
		{
			get
			{
				return this.m_Worker.InternalLocalCertificate;
			}
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x00069E49 File Offset: 0x00068049
		internal ChannelBinding GetChannelBinding(ChannelBindingKind kind)
		{
			if (kind == ChannelBindingKind.Endpoint && this.m_CachedChannelBinding != null)
			{
				return this.m_CachedChannelBinding;
			}
			return this.m_Worker.GetChannelBinding(kind);
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x00069E6C File Offset: 0x0006806C
		internal bool ProcessAuthentication(LazyAsyncResult result)
		{
			bool flag = false;
			bool flag2 = result == null;
			ArrayList pendingIO = this.m_PendingIO;
			lock (pendingIO)
			{
				if (this.m_Worker.IsAuthenticated)
				{
					return false;
				}
				if (this.m_PendingIO.Count == 0)
				{
					flag = true;
				}
				if (flag2)
				{
					result = new LazyAsyncResult(this, null, null);
				}
				this.m_PendingIO.Add(result);
			}
			try
			{
				if (flag)
				{
					bool flag4 = true;
					LazyAsyncResult lazyAsyncResult = null;
					try
					{
						try
						{
							this.m_Worker.ValidateCreateContext(false, this.m_DestinationHost, this.m_SslProtocols, null, this.m_ClientCertificates, true, this.m_CheckCertificateRevocationList, ServicePointManager.CheckCertificateName);
							if (!flag2)
							{
								lazyAsyncResult = new LazyAsyncResult(this.m_Worker, null, new AsyncCallback(this.WakeupPendingIO));
							}
							if (this._ExecutionContext != null)
							{
								ExecutionContext.Run(this._ExecutionContext.CreateCopy(), new ContextCallback(this.CallProcessAuthentication), lazyAsyncResult);
							}
							else
							{
								this.m_Worker.ProcessAuthentication(lazyAsyncResult);
							}
						}
						catch
						{
							flag4 = false;
							throw;
						}
						goto IL_165;
					}
					finally
					{
						if (flag2 || !flag4)
						{
							ArrayList pendingIO2 = this.m_PendingIO;
							lock (pendingIO2)
							{
								if (this.m_PendingIO.Count > 1)
								{
									ThreadPool.QueueUserWorkItem(new WaitCallback(this.StartWakeupPendingIO), null);
								}
								else
								{
									this.m_PendingIO.Clear();
								}
							}
						}
					}
				}
				if (flag2)
				{
					Exception ex = result.InternalWaitForCompletion() as Exception;
					if (ex != null)
					{
						throw ex;
					}
				}
				IL_165:;
			}
			catch
			{
				if (this.m_Worker.IsCertValidationFailed)
				{
					this.m_ExceptionStatus = WebExceptionStatus.TrustFailure;
				}
				else if (this.m_Worker.LastSecurityStatus != SecurityStatus.OK)
				{
					this.m_ExceptionStatus = WebExceptionStatus.SecureChannelFailure;
				}
				else
				{
					this.m_ExceptionStatus = WebExceptionStatus.ReceiveFailure;
				}
				throw;
			}
			return true;
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0006A098 File Offset: 0x00068298
		private void CallProcessAuthentication(object state)
		{
			this.m_Worker.ProcessAuthentication((LazyAsyncResult)state);
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x0006A0AB File Offset: 0x000682AB
		private void StartWakeupPendingIO(object nullState)
		{
			this.WakeupPendingIO(null);
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0006A0B4 File Offset: 0x000682B4
		private void WakeupPendingIO(IAsyncResult ar)
		{
			Exception ex = null;
			try
			{
				if (ar != null)
				{
					this.m_Worker.EndProcessAuthentication(ar);
				}
			}
			catch (Exception ex2)
			{
				ex = ex2;
				if (this.m_Worker.IsCertValidationFailed)
				{
					this.m_ExceptionStatus = WebExceptionStatus.TrustFailure;
				}
				else if (this.m_Worker.LastSecurityStatus != SecurityStatus.OK)
				{
					this.m_ExceptionStatus = WebExceptionStatus.SecureChannelFailure;
				}
				else
				{
					this.m_ExceptionStatus = WebExceptionStatus.ReceiveFailure;
				}
			}
			ArrayList pendingIO = this.m_PendingIO;
			lock (pendingIO)
			{
				while (this.m_PendingIO.Count != 0)
				{
					LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)this.m_PendingIO[this.m_PendingIO.Count - 1];
					this.m_PendingIO.RemoveAt(this.m_PendingIO.Count - 1);
					if (lazyAsyncResult is BufferAsyncResult)
					{
						if (this.m_PendingIO.Count == 0)
						{
							this.ResumeIOWorker(lazyAsyncResult);
						}
						else
						{
							ThreadPool.QueueUserWorkItem(new WaitCallback(this.ResumeIOWorker), lazyAsyncResult);
						}
					}
					else
					{
						try
						{
							lazyAsyncResult.InvokeCallback(ex);
						}
						catch
						{
						}
					}
				}
			}
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x0006A1E0 File Offset: 0x000683E0
		private void ResumeIOWorker(object result)
		{
			BufferAsyncResult bufferAsyncResult = (BufferAsyncResult)result;
			try
			{
				this.ResumeIO(bufferAsyncResult);
			}
			catch (Exception ex)
			{
				if (ex is OutOfMemoryException || ex is StackOverflowException || ex is ThreadAbortException)
				{
					throw;
				}
				if (bufferAsyncResult.InternalPeekCompleted)
				{
					throw;
				}
				bufferAsyncResult.InvokeCallback(ex);
			}
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x0006A23C File Offset: 0x0006843C
		private void ResumeIO(BufferAsyncResult bufferResult)
		{
			IAsyncResult asyncResult;
			if (bufferResult.IsWrite)
			{
				if (bufferResult.Buffers != null)
				{
					asyncResult = this.m_Worker.SecureStream.BeginWrite(bufferResult.Buffers, TlsStream._CompleteIOCallback, bufferResult);
				}
				else
				{
					asyncResult = this.m_Worker.SecureStream.BeginWrite(bufferResult.Buffer, bufferResult.Offset, bufferResult.Count, TlsStream._CompleteIOCallback, bufferResult);
				}
			}
			else
			{
				asyncResult = this.m_Worker.SecureStream.BeginRead(bufferResult.Buffer, bufferResult.Offset, bufferResult.Count, TlsStream._CompleteIOCallback, bufferResult);
			}
			if (asyncResult.CompletedSynchronously)
			{
				TlsStream.CompleteIO(asyncResult);
			}
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x0006A2DC File Offset: 0x000684DC
		private static void CompleteIOCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			try
			{
				TlsStream.CompleteIO(result);
			}
			catch (Exception ex)
			{
				if (ex is OutOfMemoryException || ex is StackOverflowException || ex is ThreadAbortException)
				{
					throw;
				}
				if (((LazyAsyncResult)result.AsyncState).InternalPeekCompleted)
				{
					throw;
				}
				((LazyAsyncResult)result.AsyncState).InvokeCallback(ex);
			}
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x0006A350 File Offset: 0x00068550
		private static void CompleteIO(IAsyncResult result)
		{
			BufferAsyncResult bufferAsyncResult = (BufferAsyncResult)result.AsyncState;
			object obj = null;
			if (bufferAsyncResult.IsWrite)
			{
				((TlsStream)bufferAsyncResult.AsyncObject).m_Worker.SecureStream.EndWrite(result);
			}
			else
			{
				obj = ((TlsStream)bufferAsyncResult.AsyncObject).m_Worker.SecureStream.EndRead(result);
			}
			bufferAsyncResult.InvokeCallback(obj);
		}

		// Token: 0x040015E1 RID: 5601
		private SslState m_Worker;

		// Token: 0x040015E2 RID: 5602
		private WebExceptionStatus m_ExceptionStatus;

		// Token: 0x040015E3 RID: 5603
		private string m_DestinationHost;

		// Token: 0x040015E4 RID: 5604
		private X509CertificateCollection m_ClientCertificates;

		// Token: 0x040015E5 RID: 5605
		private static AsyncCallback _CompleteIOCallback = new AsyncCallback(TlsStream.CompleteIOCallback);

		// Token: 0x040015E6 RID: 5606
		private ExecutionContext _ExecutionContext;

		// Token: 0x040015E7 RID: 5607
		private ChannelBinding m_CachedChannelBinding;

		// Token: 0x040015E8 RID: 5608
		private bool m_CheckCertificateRevocationList;

		// Token: 0x040015E9 RID: 5609
		private SslProtocols m_SslProtocols;

		// Token: 0x040015EA RID: 5610
		private int m_ShutDown;

		// Token: 0x040015EB RID: 5611
		private ArrayList m_PendingIO = new ArrayList();
	}
}
