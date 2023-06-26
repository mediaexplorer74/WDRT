using System;
using System.IO;
using System.IO.Compression;

namespace System.Net
{
	// Token: 0x0200010C RID: 268
	internal class GZipWrapperStream : GZipStream, ICloseEx, IRequestLifetimeTracker
	{
		// Token: 0x06000AE2 RID: 2786 RVA: 0x0003C5D2 File Offset: 0x0003A7D2
		public GZipWrapperStream(Stream stream, CompressionMode mode)
			: base(stream, mode, false)
		{
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0003C5E0 File Offset: 0x0003A7E0
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

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0003C60C File Offset: 0x0003A80C
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

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0003C6B4 File Offset: 0x0003A8B4
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

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0003C728 File Offset: 0x0003A928
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

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0003C7CC File Offset: 0x0003A9CC
		void IRequestLifetimeTracker.TrackRequestLifetime(long requestStartTimestamp)
		{
			IRequestLifetimeTracker requestLifetimeTracker = base.BaseStream as IRequestLifetimeTracker;
			requestLifetimeTracker.TrackRequestLifetime(requestStartTimestamp);
		}
	}
}
