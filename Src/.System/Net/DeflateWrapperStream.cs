using System;
using System.IO;
using System.IO.Compression;

namespace System.Net
{
	// Token: 0x0200010D RID: 269
	internal class DeflateWrapperStream : DeflateStream, ICloseEx, IRequestLifetimeTracker
	{
		// Token: 0x06000AE8 RID: 2792 RVA: 0x0003C7EC File Offset: 0x0003A9EC
		public DeflateWrapperStream(Stream stream, CompressionMode mode)
			: base(stream, mode, false)
		{
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0003C7F8 File Offset: 0x0003A9F8
		void ICloseEx.CloseEx(CloseExState closeState)
		{
			ICloseEx closeEx = base.BaseStream as ICloseEx;
			if (closeEx != null)
			{
				closeEx.CloseEx(closeState);
				return;
			}
			this.Close();
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0003C824 File Offset: 0x0003AA24
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			IAsyncResult asyncResult;
			try
			{
				asyncResult = base.BeginRead(buffer, offset, size, callback, state);
			}
			catch (Exception ex)
			{
				try
				{
					if (NclUtilities.IsFatal(ex))
					{
						throw;
					}
					if (ex is InvalidDataException || ex is InvalidOperationException || ex is IndexOutOfRangeException)
					{
						this.Close();
					}
				}
				catch
				{
				}
				throw ex;
			}
			return asyncResult;
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0003C8CC File Offset: 0x0003AACC
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			int num;
			try
			{
				num = base.EndRead(asyncResult);
			}
			catch (Exception ex)
			{
				try
				{
					if (NclUtilities.IsFatal(ex))
					{
						throw;
					}
					if (ex is InvalidDataException || ex is InvalidOperationException || ex is IndexOutOfRangeException)
					{
						this.Close();
					}
				}
				catch
				{
				}
				throw ex;
			}
			return num;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0003C940 File Offset: 0x0003AB40
		public override int Read(byte[] buffer, int offset, int size)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			int num;
			try
			{
				num = base.Read(buffer, offset, size);
			}
			catch (Exception ex)
			{
				try
				{
					if (NclUtilities.IsFatal(ex))
					{
						throw;
					}
					if (ex is InvalidDataException || ex is InvalidOperationException || ex is IndexOutOfRangeException)
					{
						this.Close();
					}
				}
				catch
				{
				}
				throw ex;
			}
			return num;
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0003C9E4 File Offset: 0x0003ABE4
		void IRequestLifetimeTracker.TrackRequestLifetime(long requestStartTimestamp)
		{
			IRequestLifetimeTracker requestLifetimeTracker = base.BaseStream as IRequestLifetimeTracker;
			requestLifetimeTracker.TrackRequestLifetime(requestStartTimestamp);
		}
	}
}
