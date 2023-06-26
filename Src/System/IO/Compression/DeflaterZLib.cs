using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.IO.Compression
{
	// Token: 0x02000422 RID: 1058
	internal class DeflaterZLib : IDeflater, IDisposable
	{
		// Token: 0x0600277C RID: 10108 RVA: 0x000B5BC5 File Offset: 0x000B3DC5
		internal DeflaterZLib()
			: this(CompressionLevel.Optimal)
		{
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x000B5BD0 File Offset: 0x000B3DD0
		internal DeflaterZLib(CompressionLevel compressionLevel)
		{
			ZLibNative.CompressionLevel compressionLevel2;
			int num;
			int num2;
			ZLibNative.CompressionStrategy compressionStrategy;
			switch (compressionLevel)
			{
			case CompressionLevel.Optimal:
				compressionLevel2 = (ZLibNative.CompressionLevel)6;
				num = -15;
				num2 = 8;
				compressionStrategy = ZLibNative.CompressionStrategy.DefaultStrategy;
				break;
			case CompressionLevel.Fastest:
				compressionLevel2 = ZLibNative.CompressionLevel.BestSpeed;
				num = -15;
				num2 = 8;
				compressionStrategy = ZLibNative.CompressionStrategy.DefaultStrategy;
				break;
			case CompressionLevel.NoCompression:
				compressionLevel2 = ZLibNative.CompressionLevel.NoCompression;
				num = -15;
				num2 = 7;
				compressionStrategy = ZLibNative.CompressionStrategy.DefaultStrategy;
				break;
			default:
				throw new ArgumentOutOfRangeException("compressionLevel");
			}
			this._isDisposed = false;
			this.DeflateInit(compressionLevel2, num, num2, compressionStrategy);
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x000B5C40 File Offset: 0x000B3E40
		~DeflaterZLib()
		{
			if (!Environment.HasShutdownStarted)
			{
				this.Dispose(false);
			}
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x000B5C78 File Offset: 0x000B3E78
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x000B5C87 File Offset: 0x000B3E87
		[SecuritySafeCritical]
		protected virtual void Dispose(bool disposing)
		{
			if (!this._isDisposed)
			{
				if (disposing)
				{
					this._zlibStream.Dispose();
				}
				if (this._inputBufferHandle.IsAllocated)
				{
					this.DeallocateInputBufferHandle();
				}
				this._isDisposed = true;
			}
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x000B5CB9 File Offset: 0x000B3EB9
		private bool NeedsInput()
		{
			return ((IDeflater)this).NeedsInput();
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x000B5CC1 File Offset: 0x000B3EC1
		[SecuritySafeCritical]
		bool IDeflater.NeedsInput()
		{
			return this._zlibStream.AvailIn == 0U;
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x000B5CD4 File Offset: 0x000B3ED4
		[SecuritySafeCritical]
		void IDeflater.SetInput(byte[] inputBuffer, int startIndex, int count)
		{
			if (count == 0)
			{
				return;
			}
			object obj = this.syncLock;
			lock (obj)
			{
				this._inputBufferHandle = GCHandle.Alloc(inputBuffer, GCHandleType.Pinned);
				this._isValid = 1;
				this._zlibStream.NextIn = this._inputBufferHandle.AddrOfPinnedObject() + startIndex;
				this._zlibStream.AvailIn = (uint)count;
			}
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x000B5D50 File Offset: 0x000B3F50
		[SecuritySafeCritical]
		int IDeflater.GetDeflateOutput(byte[] outputBuffer)
		{
			int num2;
			try
			{
				int num;
				this.ReadDeflateOutput(outputBuffer, ZLibNative.FlushCode.NoFlush, out num);
				num2 = num;
			}
			finally
			{
				if (this._zlibStream.AvailIn == 0U && this._inputBufferHandle.IsAllocated)
				{
					this.DeallocateInputBufferHandle();
				}
			}
			return num2;
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x000B5DA0 File Offset: 0x000B3FA0
		private unsafe ZLibNative.ErrorCode ReadDeflateOutput(byte[] outputBuffer, ZLibNative.FlushCode flushCode, out int bytesRead)
		{
			object obj = this.syncLock;
			ZLibNative.ErrorCode errorCode2;
			lock (obj)
			{
				byte* ptr;
				if (outputBuffer == null || outputBuffer.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &outputBuffer[0];
				}
				this._zlibStream.NextOut = (IntPtr)((void*)ptr);
				this._zlibStream.AvailOut = (uint)outputBuffer.Length;
				ZLibNative.ErrorCode errorCode = this.Deflate(flushCode);
				bytesRead = outputBuffer.Length - (int)this._zlibStream.AvailOut;
				errorCode2 = errorCode;
			}
			return errorCode2;
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x000B5E34 File Offset: 0x000B4034
		bool IDeflater.Finish(byte[] outputBuffer, out int bytesRead)
		{
			ZLibNative.ErrorCode errorCode = this.ReadDeflateOutput(outputBuffer, ZLibNative.FlushCode.Finish, out bytesRead);
			return errorCode == ZLibNative.ErrorCode.StreamEnd;
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x000B5E50 File Offset: 0x000B4050
		private void DeallocateInputBufferHandle()
		{
			object obj = this.syncLock;
			lock (obj)
			{
				this._zlibStream.AvailIn = 0U;
				this._zlibStream.NextIn = ZLibNative.ZNullPtr;
				if (Interlocked.Exchange(ref this._isValid, 0) != 0)
				{
					this._inputBufferHandle.Free();
				}
			}
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x000B5EC0 File Offset: 0x000B40C0
		[SecuritySafeCritical]
		private void DeflateInit(ZLibNative.CompressionLevel compressionLevel, int windowBits, int memLevel, ZLibNative.CompressionStrategy strategy)
		{
			ZLibNative.ErrorCode errorCode;
			try
			{
				errorCode = ZLibNative.CreateZLibStreamForDeflate(out this._zlibStream, compressionLevel, windowBits, memLevel, strategy);
			}
			catch (Exception ex)
			{
				throw new ZLibException(SR.GetString("ZLibErrorDLLLoadError"), ex);
			}
			switch (errorCode)
			{
			case ZLibNative.ErrorCode.VersionError:
				throw new ZLibException(SR.GetString("ZLibErrorVersionMismatch"), "deflateInit2_", (int)errorCode, this._zlibStream.GetErrorMessage());
			case ZLibNative.ErrorCode.MemError:
				throw new ZLibException(SR.GetString("ZLibErrorNotEnoughMemory"), "deflateInit2_", (int)errorCode, this._zlibStream.GetErrorMessage());
			case ZLibNative.ErrorCode.StreamError:
				throw new ZLibException(SR.GetString("ZLibErrorIncorrectInitParameters"), "deflateInit2_", (int)errorCode, this._zlibStream.GetErrorMessage());
			case ZLibNative.ErrorCode.Ok:
				return;
			}
			throw new ZLibException(SR.GetString("ZLibErrorUnexpected"), "deflateInit2_", (int)errorCode, this._zlibStream.GetErrorMessage());
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x000B5FB0 File Offset: 0x000B41B0
		[SecuritySafeCritical]
		private ZLibNative.ErrorCode Deflate(ZLibNative.FlushCode flushCode)
		{
			ZLibNative.ErrorCode errorCode;
			try
			{
				errorCode = this._zlibStream.Deflate(flushCode);
			}
			catch (Exception ex)
			{
				throw new ZLibException(SR.GetString("ZLibErrorDLLLoadError"), ex);
			}
			switch (errorCode)
			{
			case ZLibNative.ErrorCode.BufError:
				return errorCode;
			case ZLibNative.ErrorCode.StreamError:
				throw new ZLibException(SR.GetString("ZLibErrorInconsistentStream"), "deflate", (int)errorCode, this._zlibStream.GetErrorMessage());
			case ZLibNative.ErrorCode.Ok:
			case ZLibNative.ErrorCode.StreamEnd:
				return errorCode;
			}
			throw new ZLibException(SR.GetString("ZLibErrorUnexpected"), "deflate", (int)errorCode, this._zlibStream.GetErrorMessage());
		}

		// Token: 0x04002166 RID: 8550
		private ZLibNative.ZLibStreamHandle _zlibStream;

		// Token: 0x04002167 RID: 8551
		private GCHandle _inputBufferHandle;

		// Token: 0x04002168 RID: 8552
		private bool _isDisposed;

		// Token: 0x04002169 RID: 8553
		private int _isValid;

		// Token: 0x0400216A RID: 8554
		private readonly object syncLock = new object();
	}
}
