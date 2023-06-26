using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace System.Net
{
	// Token: 0x020000FD RID: 253
	internal class ListenerClientCertAsyncResult : LazyAsyncResult
	{
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x00032CC9 File Offset: 0x00030EC9
		internal unsafe NativeOverlapped* NativeOverlapped
		{
			get
			{
				return this.m_pOverlapped;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x00032CD1 File Offset: 0x00030ED1
		internal unsafe UnsafeNclNativeMethods.HttpApi.HTTP_SSL_CLIENT_CERT_INFO* RequestBlob
		{
			get
			{
				return this.m_MemoryBlob;
			}
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00032CD9 File Offset: 0x00030ED9
		internal ListenerClientCertAsyncResult(object asyncObject, object userState, AsyncCallback callback, uint size)
			: base(asyncObject, userState, callback)
		{
			this.Reset(size);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00032CEC File Offset: 0x00030EEC
		internal unsafe void Reset(uint size)
		{
			if (size == this.m_Size)
			{
				return;
			}
			if (this.m_Size != 0U)
			{
				Overlapped.Free(this.m_pOverlapped);
			}
			this.m_Size = size;
			if (size == 0U)
			{
				this.m_pOverlapped = null;
				this.m_MemoryBlob = null;
				this.m_BackingBuffer = null;
				return;
			}
			this.m_BackingBuffer = new byte[checked((int)size)];
			this.m_pOverlapped = new Overlapped
			{
				AsyncResult = this
			}.Pack(ListenerClientCertAsyncResult.s_IOCallback, this.m_BackingBuffer);
			this.m_MemoryBlob = (UnsafeNclNativeMethods.HttpApi.HTTP_SSL_CLIENT_CERT_INFO*)(void*)Marshal.UnsafeAddrOfPinnedArrayElement(this.m_BackingBuffer, 0);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00032D80 File Offset: 0x00030F80
		internal void IOCompleted(uint errorCode, uint numBytes)
		{
			ListenerClientCertAsyncResult.IOCompleted(this, errorCode, numBytes);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00032D8C File Offset: 0x00030F8C
		private unsafe static void IOCompleted(ListenerClientCertAsyncResult asyncResult, uint errorCode, uint numBytes)
		{
			HttpListenerRequest httpListenerRequest = (HttpListenerRequest)asyncResult.AsyncObject;
			object obj = null;
			try
			{
				if (errorCode == 234U)
				{
					UnsafeNclNativeMethods.HttpApi.HTTP_SSL_CLIENT_CERT_INFO* requestBlob = asyncResult.RequestBlob;
					asyncResult.Reset(numBytes + requestBlob->CertEncodedSize);
					uint num = 0U;
					errorCode = UnsafeNclNativeMethods.HttpApi.HttpReceiveClientCertificate(httpListenerRequest.HttpListenerContext.RequestQueueHandle, httpListenerRequest.m_ConnectionId, 0U, asyncResult.m_MemoryBlob, asyncResult.m_Size, &num, asyncResult.m_pOverlapped);
					if (errorCode == 997U || (errorCode == 0U && !HttpListener.SkipIOCPCallbackOnSuccess))
					{
						return;
					}
				}
				if (errorCode != 0U)
				{
					asyncResult.ErrorCode = (int)errorCode;
					obj = new HttpListenerException((int)errorCode);
				}
				else
				{
					UnsafeNclNativeMethods.HttpApi.HTTP_SSL_CLIENT_CERT_INFO* memoryBlob = asyncResult.m_MemoryBlob;
					if (memoryBlob != null)
					{
						if (memoryBlob->pCertEncoded != null)
						{
							try
							{
								byte[] array = new byte[memoryBlob->CertEncodedSize];
								Marshal.Copy((IntPtr)((void*)memoryBlob->pCertEncoded), array, 0, array.Length);
								obj = (httpListenerRequest.ClientCertificate = new X509Certificate2(array));
							}
							catch (CryptographicException ex)
							{
								obj = ex;
							}
							catch (SecurityException ex2)
							{
								obj = ex2;
							}
						}
						httpListenerRequest.SetClientCertificateError((int)memoryBlob->CertFlags);
					}
				}
			}
			catch (Exception ex3)
			{
				if (NclUtilities.IsFatal(ex3))
				{
					throw;
				}
				obj = ex3;
			}
			finally
			{
				if (errorCode != 997U)
				{
					httpListenerRequest.ClientCertState = ListenerClientCertState.Completed;
				}
			}
			asyncResult.InvokeCallback(obj);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00032EEC File Offset: 0x000310EC
		private unsafe static void WaitCallback(uint errorCode, uint numBytes, NativeOverlapped* nativeOverlapped)
		{
			Overlapped overlapped = Overlapped.Unpack(nativeOverlapped);
			ListenerClientCertAsyncResult listenerClientCertAsyncResult = (ListenerClientCertAsyncResult)overlapped.AsyncResult;
			ListenerClientCertAsyncResult.IOCompleted(listenerClientCertAsyncResult, errorCode, numBytes);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00032F14 File Offset: 0x00031114
		protected override void Cleanup()
		{
			if (this.m_pOverlapped != null)
			{
				this.m_MemoryBlob = null;
				Overlapped.Free(this.m_pOverlapped);
				this.m_pOverlapped = null;
			}
			GC.SuppressFinalize(this);
			base.Cleanup();
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00032F48 File Offset: 0x00031148
		~ListenerClientCertAsyncResult()
		{
			if (this.m_pOverlapped != null && !NclUtilities.HasShutdownStarted)
			{
				Overlapped.Free(this.m_pOverlapped);
				this.m_pOverlapped = null;
			}
		}

		// Token: 0x04000E0A RID: 3594
		private unsafe NativeOverlapped* m_pOverlapped;

		// Token: 0x04000E0B RID: 3595
		private byte[] m_BackingBuffer;

		// Token: 0x04000E0C RID: 3596
		private unsafe UnsafeNclNativeMethods.HttpApi.HTTP_SSL_CLIENT_CERT_INFO* m_MemoryBlob;

		// Token: 0x04000E0D RID: 3597
		private uint m_Size;

		// Token: 0x04000E0E RID: 3598
		private static readonly IOCompletionCallback s_IOCallback = new IOCompletionCallback(ListenerClientCertAsyncResult.WaitCallback);
	}
}
