using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.IO.Compression
{
	// Token: 0x02000423 RID: 1059
	internal class InflaterZlib : IInflater, IDisposable
	{
		// Token: 0x0600278A RID: 10122 RVA: 0x000B605C File Offset: 0x000B425C
		internal InflaterZlib(int windowBits)
		{
			this._finished = false;
			this._isDisposed = false;
			this.InflateInit(windowBits);
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x0600278B RID: 10123 RVA: 0x000B6084 File Offset: 0x000B4284
		public int AvailableOutput
		{
			get
			{
				return (int)this._zlibStream.AvailOut;
			}
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x000B6091 File Offset: 0x000B4291
		public bool Finished()
		{
			return this._finished;
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x000B609C File Offset: 0x000B429C
		public int Inflate(byte[] bytes, int offset, int length)
		{
			if (length == 0)
			{
				return 0;
			}
			int num2;
			try
			{
				int num;
				ZLibNative.ErrorCode errorCode = this.ReadInflateOutput(bytes, offset, length, ZLibNative.FlushCode.NoFlush, out num);
				if (errorCode == ZLibNative.ErrorCode.StreamEnd)
				{
					this._finished = true;
				}
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

		// Token: 0x0600278E RID: 10126 RVA: 0x000B6100 File Offset: 0x000B4300
		public bool NeedsInput()
		{
			return this._zlibStream.AvailIn == 0U;
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x000B6110 File Offset: 0x000B4310
		public void SetInput(byte[] inputBuffer, int startIndex, int count)
		{
			if (count == 0)
			{
				return;
			}
			object syncLock = this._syncLock;
			lock (syncLock)
			{
				this._inputBufferHandle = GCHandle.Alloc(inputBuffer, GCHandleType.Pinned);
				this._isValid = 1;
				this._zlibStream.NextIn = this._inputBufferHandle.AddrOfPinnedObject() + startIndex;
				this._zlibStream.AvailIn = (uint)count;
				this._finished = false;
			}
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x000B6194 File Offset: 0x000B4394
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

		// Token: 0x06002791 RID: 10129 RVA: 0x000B61C6 File Offset: 0x000B43C6
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x000B61D8 File Offset: 0x000B43D8
		~InflaterZlib()
		{
			if (!Environment.HasShutdownStarted)
			{
				this.Dispose(false);
			}
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x000B6210 File Offset: 0x000B4410
		[SecuritySafeCritical]
		private void InflateInit(int windowBits)
		{
			ZLibNative.ErrorCode errorCode;
			try
			{
				errorCode = ZLibNative.CreateZLibStreamForInflate(out this._zlibStream, windowBits);
			}
			catch (Exception ex)
			{
				throw new ZLibException(SR.GetString("ZLibErrorDLLLoadError"), ex);
			}
			switch (errorCode)
			{
			case ZLibNative.ErrorCode.VersionError:
				throw new ZLibException(SR.GetString("ZLibErrorVersionMismatch"), "inflateInit2_", (int)errorCode, this._zlibStream.GetErrorMessage());
			case ZLibNative.ErrorCode.MemError:
				throw new ZLibException(SR.GetString("ZLibErrorNotEnoughMemory"), "inflateInit2_", (int)errorCode, this._zlibStream.GetErrorMessage());
			case ZLibNative.ErrorCode.StreamError:
				throw new ZLibException(SR.GetString("ZLibErrorIncorrectInitParameters"), "inflateInit2_", (int)errorCode, this._zlibStream.GetErrorMessage());
			case ZLibNative.ErrorCode.Ok:
				return;
			}
			throw new ZLibException(SR.GetString("ZLibErrorUnexpected"), "inflateInit2_", (int)errorCode, this._zlibStream.GetErrorMessage());
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x000B62FC File Offset: 0x000B44FC
		private unsafe ZLibNative.ErrorCode ReadInflateOutput(byte[] outputBuffer, int offset, int length, ZLibNative.FlushCode flushCode, out int bytesRead)
		{
			object syncLock = this._syncLock;
			ZLibNative.ErrorCode errorCode2;
			lock (syncLock)
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
				this._zlibStream.NextOut = (IntPtr)((void*)ptr) + offset;
				this._zlibStream.AvailOut = (uint)length;
				ZLibNative.ErrorCode errorCode = this.Inflate(flushCode);
				bytesRead = length - (int)this._zlibStream.AvailOut;
				errorCode2 = errorCode;
			}
			return errorCode2;
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x000B6394 File Offset: 0x000B4594
		[SecuritySafeCritical]
		private ZLibNative.ErrorCode Inflate(ZLibNative.FlushCode flushCode)
		{
			ZLibNative.ErrorCode errorCode;
			try
			{
				errorCode = this._zlibStream.Inflate(flushCode);
			}
			catch (Exception ex)
			{
				throw new ZLibException(SR.GetString("ZLibErrorDLLLoadError"), ex);
			}
			switch (errorCode)
			{
			case ZLibNative.ErrorCode.BufError:
				return errorCode;
			case ZLibNative.ErrorCode.MemError:
				throw new ZLibException(SR.GetString("ZLibErrorNotEnoughMemory"), "inflate_", (int)errorCode, this._zlibStream.GetErrorMessage());
			case ZLibNative.ErrorCode.DataError:
				throw new InvalidDataException(SR.GetString("GenericInvalidData"));
			case ZLibNative.ErrorCode.StreamError:
				throw new ZLibException(SR.GetString("ZLibErrorInconsistentStream"), "inflate_", (int)errorCode, this._zlibStream.GetErrorMessage());
			case ZLibNative.ErrorCode.Ok:
			case ZLibNative.ErrorCode.StreamEnd:
				return errorCode;
			}
			throw new ZLibException(SR.GetString("ZLibErrorUnexpected"), "inflate_", (int)errorCode, this._zlibStream.GetErrorMessage());
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x000B6470 File Offset: 0x000B4670
		private void DeallocateInputBufferHandle()
		{
			object syncLock = this._syncLock;
			lock (syncLock)
			{
				this._zlibStream.AvailIn = 0U;
				this._zlibStream.NextIn = ZLibNative.ZNullPtr;
				if (Interlocked.Exchange(ref this._isValid, 0) != 0)
				{
					this._inputBufferHandle.Free();
				}
			}
		}

		// Token: 0x0400216B RID: 8555
		private bool _finished;

		// Token: 0x0400216C RID: 8556
		private bool _isDisposed;

		// Token: 0x0400216D RID: 8557
		private ZLibNative.ZLibStreamHandle _zlibStream;

		// Token: 0x0400216E RID: 8558
		private GCHandle _inputBufferHandle;

		// Token: 0x0400216F RID: 8559
		private readonly object _syncLock = new object();

		// Token: 0x04002170 RID: 8560
		private int _isValid;
	}
}
