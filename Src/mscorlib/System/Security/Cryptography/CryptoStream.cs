using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Security.Cryptography
{
	/// <summary>Defines a stream that links data streams to cryptographic transformations.</summary>
	// Token: 0x02000257 RID: 599
	[ComVisible(true)]
	public class CryptoStream : Stream, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptoStream" /> class with a target data stream, the transformation to use, and the mode of the stream.</summary>
		/// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
		/// <param name="transform">The cryptographic transformation that is to be performed on the stream.</param>
		/// <param name="mode">One of the <see cref="T:System.Security.Cryptography.CryptoStreamMode" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> is invalid.</exception>
		// Token: 0x0600213B RID: 8507 RVA: 0x000752FF File Offset: 0x000734FF
		public CryptoStream(Stream stream, ICryptoTransform transform, CryptoStreamMode mode)
			: this(stream, transform, mode, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptoStream" /> class.</summary>
		/// <param name="stream">The stream on which to perform the cryptographic transformation.</param>
		/// <param name="transform">The cryptographic transformation that is to be performed on the stream.</param>
		/// <param name="mode">The mode of the stream.</param>
		/// <param name="leaveOpen">
		///   <see langword="true" /> to not close the underlying stream when the <see cref="T:System.Security.Cryptography.CryptoStream" /> object is disposed; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is invalid.</exception>
		// Token: 0x0600213C RID: 8508 RVA: 0x0007530C File Offset: 0x0007350C
		public CryptoStream(Stream stream, ICryptoTransform transform, CryptoStreamMode mode, bool leaveOpen)
		{
			this._stream = stream;
			this._transformMode = mode;
			this._Transform = transform;
			this._leaveOpen = leaveOpen;
			CryptoStreamMode transformMode = this._transformMode;
			if (transformMode != CryptoStreamMode.Read)
			{
				if (transformMode != CryptoStreamMode.Write)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
				}
				if (!this._stream.CanWrite)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_StreamNotWritable"), "stream");
				}
				this._canWrite = true;
			}
			else
			{
				if (!this._stream.CanRead)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_StreamNotReadable"), "stream");
				}
				this._canRead = true;
			}
			this.InitializeBuffer();
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Security.Cryptography.CryptoStream" /> is readable.</summary>
		/// <returns>
		///   <see langword="true" /> if the current stream is readable; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x0600213D RID: 8509 RVA: 0x000753B8 File Offset: 0x000735B8
		public override bool CanRead
		{
			get
			{
				return this._canRead;
			}
		}

		/// <summary>Gets a value indicating whether you can seek within the current <see cref="T:System.Security.Cryptography.CryptoStream" />.</summary>
		/// <returns>Always <see langword="false" />.</returns>
		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x0600213E RID: 8510 RVA: 0x000753C0 File Offset: 0x000735C0
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Security.Cryptography.CryptoStream" /> is writable.</summary>
		/// <returns>
		///   <see langword="true" /> if the current stream is writable; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x0600213F RID: 8511 RVA: 0x000753C3 File Offset: 0x000735C3
		public override bool CanWrite
		{
			get
			{
				return this._canWrite;
			}
		}

		/// <summary>Gets the length in bytes of the stream.</summary>
		/// <returns>This property is not supported.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported.</exception>
		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06002140 RID: 8512 RVA: 0x000753CB File Offset: 0x000735CB
		public override long Length
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
			}
		}

		/// <summary>Gets or sets the position within the current stream.</summary>
		/// <returns>This property is not supported.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported.</exception>
		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06002141 RID: 8513 RVA: 0x000753DC File Offset: 0x000735DC
		// (set) Token: 0x06002142 RID: 8514 RVA: 0x000753ED File Offset: 0x000735ED
		public override long Position
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
			}
			set
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
			}
		}

		/// <summary>Gets a value indicating whether the final buffer block has been written to the underlying stream.</summary>
		/// <returns>
		///   <see langword="true" /> if the final block has been flushed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06002143 RID: 8515 RVA: 0x000753FE File Offset: 0x000735FE
		public bool HasFlushedFinalBlock
		{
			get
			{
				return this._finalBlockTransformed;
			}
		}

		/// <summary>Updates the underlying data source or repository with the current state of the buffer, then clears the buffer.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key is corrupt which can cause invalid padding to the stream.</exception>
		/// <exception cref="T:System.NotSupportedException">The current stream is not writable.  
		///  -or-  
		///  The final block has already been transformed.</exception>
		// Token: 0x06002144 RID: 8516 RVA: 0x00075408 File Offset: 0x00073608
		public void FlushFinalBlock()
		{
			if (this._finalBlockTransformed)
			{
				throw new NotSupportedException(Environment.GetResourceString("Cryptography_CryptoStream_FlushFinalBlockTwice"));
			}
			byte[] array = this._Transform.TransformFinalBlock(this._InputBuffer, 0, this._InputBufferIndex);
			this._finalBlockTransformed = true;
			if (this._canWrite && this._OutputBufferIndex > 0)
			{
				this._stream.Write(this._OutputBuffer, 0, this._OutputBufferIndex);
				this._OutputBufferIndex = 0;
			}
			if (this._canWrite)
			{
				this._stream.Write(array, 0, array.Length);
			}
			CryptoStream cryptoStream = this._stream as CryptoStream;
			if (cryptoStream != null)
			{
				if (!cryptoStream.HasFlushedFinalBlock)
				{
					cryptoStream.FlushFinalBlock();
				}
			}
			else
			{
				this._stream.Flush();
			}
			if (this._InputBuffer != null)
			{
				Array.Clear(this._InputBuffer, 0, this._InputBuffer.Length);
			}
			if (this._OutputBuffer != null)
			{
				Array.Clear(this._OutputBuffer, 0, this._OutputBuffer.Length);
			}
		}

		/// <summary>Clears all buffers for the current stream and causes any buffered data to be written to the underlying device.</summary>
		// Token: 0x06002145 RID: 8517 RVA: 0x000754F7 File Offset: 0x000736F7
		public override void Flush()
		{
		}

		/// <summary>Clears all buffers for the current stream asynchronously, causes any buffered data to be written to the underlying device, and monitors cancellation requests.</summary>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
		/// <returns>A task that represents the asynchronous flush operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		// Token: 0x06002146 RID: 8518 RVA: 0x000754F9 File Offset: 0x000736F9
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			if (base.GetType() != typeof(CryptoStream))
			{
				return base.FlushAsync(cancellationToken);
			}
			if (!cancellationToken.IsCancellationRequested)
			{
				return Task.CompletedTask;
			}
			return Task.FromCancellation(cancellationToken);
		}

		/// <summary>Sets the position within the current stream.</summary>
		/// <param name="offset">A byte offset relative to the <paramref name="origin" /> parameter.</param>
		/// <param name="origin">A <see cref="T:System.IO.SeekOrigin" /> object indicating the reference point used to obtain the new position.</param>
		/// <returns>This method is not supported.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x06002147 RID: 8519 RVA: 0x0007552F File Offset: 0x0007372F
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
		}

		/// <summary>Sets the length of the current stream.</summary>
		/// <param name="value">The desired length of the current stream in bytes.</param>
		/// <exception cref="T:System.NotSupportedException">This property exists only to support inheritance from <see cref="T:System.IO.Stream" />, and cannot be used.</exception>
		// Token: 0x06002148 RID: 8520 RVA: 0x00075540 File Offset: 0x00073740
		public override void SetLength(long value)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
		}

		/// <summary>Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.</summary>
		/// <param name="buffer">An array of bytes. A maximum of <paramref name="count" /> bytes are read from the current stream and stored in <paramref name="buffer" />.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin storing the data read from the current stream.</param>
		/// <param name="count">The maximum number of bytes to be read from the current stream.</param>
		/// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero if the end of the stream has been reached.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Security.Cryptography.CryptoStreamMode" /> associated with current <see cref="T:System.Security.Cryptography.CryptoStream" /> object does not match the underlying stream.  For example, this exception is thrown when using <see cref="F:System.Security.Cryptography.CryptoStreamMode.Read" /> with an underlying stream that is write only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than zero.  
		///  -or-  
		///  The <paramref name="count" /> parameter is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">Thesum of the <paramref name="count" /> and <paramref name="offset" /> parameters is longer than the length of the buffer.</exception>
		// Token: 0x06002149 RID: 8521 RVA: 0x00075554 File Offset: 0x00073754
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnreadableStream"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			int i = count;
			int num = offset;
			if (this._OutputBufferIndex != 0)
			{
				if (this._OutputBufferIndex > count)
				{
					Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, offset, count);
					Buffer.InternalBlockCopy(this._OutputBuffer, count, this._OutputBuffer, 0, this._OutputBufferIndex - count);
					this._OutputBufferIndex -= count;
					return count;
				}
				Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, offset, this._OutputBufferIndex);
				i -= this._OutputBufferIndex;
				num += this._OutputBufferIndex;
				this._OutputBufferIndex = 0;
			}
			if (this._finalBlockTransformed)
			{
				return count - i;
			}
			if (i > this._OutputBlockSize && this._Transform.CanTransformMultipleBlocks)
			{
				int num2 = i / this._OutputBlockSize;
				int num3 = num2 * this._InputBlockSize;
				byte[] array = new byte[num3];
				Buffer.InternalBlockCopy(this._InputBuffer, 0, array, 0, this._InputBufferIndex);
				int num4 = this._InputBufferIndex;
				num4 += this._stream.Read(array, this._InputBufferIndex, num3 - this._InputBufferIndex);
				this._InputBufferIndex = 0;
				if (num4 <= this._InputBlockSize)
				{
					this._InputBuffer = array;
					this._InputBufferIndex = num4;
				}
				else
				{
					int num5 = num4 / this._InputBlockSize * this._InputBlockSize;
					int num6 = num4 - num5;
					if (num6 != 0)
					{
						this._InputBufferIndex = num6;
						Buffer.InternalBlockCopy(array, num5, this._InputBuffer, 0, num6);
					}
					byte[] array2 = new byte[num5 / this._InputBlockSize * this._OutputBlockSize];
					int num7 = this._Transform.TransformBlock(array, 0, num5, array2, 0);
					Buffer.InternalBlockCopy(array2, 0, buffer, num, num7);
					Array.Clear(array, 0, array.Length);
					Array.Clear(array2, 0, array2.Length);
					i -= num7;
					num += num7;
				}
			}
			while (i > 0)
			{
				while (this._InputBufferIndex < this._InputBlockSize)
				{
					int num4 = this._stream.Read(this._InputBuffer, this._InputBufferIndex, this._InputBlockSize - this._InputBufferIndex);
					if (num4 != 0)
					{
						this._InputBufferIndex += num4;
					}
					else
					{
						byte[] array3 = this._Transform.TransformFinalBlock(this._InputBuffer, 0, this._InputBufferIndex);
						this._OutputBuffer = array3;
						this._OutputBufferIndex = array3.Length;
						this._finalBlockTransformed = true;
						if (i < this._OutputBufferIndex)
						{
							Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, num, i);
							this._OutputBufferIndex -= i;
							Buffer.InternalBlockCopy(this._OutputBuffer, i, this._OutputBuffer, 0, this._OutputBufferIndex);
							return count;
						}
						Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, num, this._OutputBufferIndex);
						i -= this._OutputBufferIndex;
						this._OutputBufferIndex = 0;
						return count - i;
					}
				}
				int num7 = this._Transform.TransformBlock(this._InputBuffer, 0, this._InputBlockSize, this._OutputBuffer, 0);
				this._InputBufferIndex = 0;
				if (i < num7)
				{
					Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, num, i);
					this._OutputBufferIndex = num7 - i;
					Buffer.InternalBlockCopy(this._OutputBuffer, i, this._OutputBuffer, 0, this._OutputBufferIndex);
					return count;
				}
				Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, num, num7);
				num += num7;
				i -= num7;
			}
			return count;
		}

		/// <summary>Reads a sequence of bytes from the current stream asynchronously, advances the position within the stream by the number of bytes read, and monitors cancellation requests.</summary>
		/// <param name="buffer">The buffer to write the data into.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin writing data from the stream.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
		/// <returns>A task that represents the asynchronous read operation. The value of the task object's <paramref name="TResult" /> parameter contains the total number of bytes read into the buffer. The result can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is currently in use by a previous read operation.</exception>
		// Token: 0x0600214A RID: 8522 RVA: 0x000758DC File Offset: 0x00073ADC
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnreadableStream"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (base.GetType() != typeof(CryptoStream))
			{
				return base.ReadAsync(buffer, offset, count, cancellationToken);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation<int>(cancellationToken);
			}
			return this.ReadAsyncInternal(buffer, offset, count, cancellationToken);
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x0007598C File Offset: 0x00073B8C
		private async Task<int> ReadAsyncInternal(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			await default(CryptoStream.HopToThreadPoolAwaitable);
			SemaphoreSlim sem = base.EnsureAsyncActiveSemaphoreInitialized();
			await sem.WaitAsync().ConfigureAwait(false);
			int num;
			try
			{
				int bytesToDeliver = count;
				int currentOutputIndex = offset;
				if (this._OutputBufferIndex != 0)
				{
					if (this._OutputBufferIndex > count)
					{
						Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, offset, count);
						Buffer.InternalBlockCopy(this._OutputBuffer, count, this._OutputBuffer, 0, this._OutputBufferIndex - count);
						this._OutputBufferIndex -= count;
						return count;
					}
					Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, offset, this._OutputBufferIndex);
					bytesToDeliver -= this._OutputBufferIndex;
					currentOutputIndex += this._OutputBufferIndex;
					this._OutputBufferIndex = 0;
				}
				if (this._finalBlockTransformed)
				{
					num = count - bytesToDeliver;
				}
				else
				{
					if (bytesToDeliver > this._OutputBlockSize && this._Transform.CanTransformMultipleBlocks)
					{
						int num2 = bytesToDeliver / this._OutputBlockSize * this._InputBlockSize;
						byte[] tempInputBuffer = new byte[num2];
						Buffer.InternalBlockCopy(this._InputBuffer, 0, tempInputBuffer, 0, this._InputBufferIndex);
						int inputBufferIndex = this._InputBufferIndex;
						int num3 = inputBufferIndex + await this._stream.ReadAsync(tempInputBuffer, this._InputBufferIndex, num2 - this._InputBufferIndex, cancellationToken).ConfigureAwait(false);
						this._InputBufferIndex = 0;
						if (num3 <= this._InputBlockSize)
						{
							this._InputBuffer = tempInputBuffer;
							this._InputBufferIndex = num3;
						}
						else
						{
							int num4 = num3 / this._InputBlockSize * this._InputBlockSize;
							int num5 = num3 - num4;
							if (num5 != 0)
							{
								this._InputBufferIndex = num5;
								Buffer.InternalBlockCopy(tempInputBuffer, num4, this._InputBuffer, 0, num5);
							}
							byte[] array = new byte[num4 / this._InputBlockSize * this._OutputBlockSize];
							int num6 = this._Transform.TransformBlock(tempInputBuffer, 0, num4, array, 0);
							Buffer.InternalBlockCopy(array, 0, buffer, currentOutputIndex, num6);
							Array.Clear(tempInputBuffer, 0, tempInputBuffer.Length);
							Array.Clear(array, 0, array.Length);
							bytesToDeliver -= num6;
							currentOutputIndex += num6;
							tempInputBuffer = null;
						}
					}
					while (bytesToDeliver > 0)
					{
						while (this._InputBufferIndex < this._InputBlockSize)
						{
							int num3 = await this._stream.ReadAsync(this._InputBuffer, this._InputBufferIndex, this._InputBlockSize - this._InputBufferIndex, cancellationToken).ConfigureAwait(false);
							if (num3 != 0)
							{
								this._InputBufferIndex += num3;
							}
							else
							{
								byte[] array2 = this._Transform.TransformFinalBlock(this._InputBuffer, 0, this._InputBufferIndex);
								this._OutputBuffer = array2;
								this._OutputBufferIndex = array2.Length;
								this._finalBlockTransformed = true;
								if (bytesToDeliver < this._OutputBufferIndex)
								{
									Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, currentOutputIndex, bytesToDeliver);
									this._OutputBufferIndex -= bytesToDeliver;
									Buffer.InternalBlockCopy(this._OutputBuffer, bytesToDeliver, this._OutputBuffer, 0, this._OutputBufferIndex);
									return count;
								}
								Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, currentOutputIndex, this._OutputBufferIndex);
								bytesToDeliver -= this._OutputBufferIndex;
								this._OutputBufferIndex = 0;
								return count - bytesToDeliver;
							}
						}
						int num6 = this._Transform.TransformBlock(this._InputBuffer, 0, this._InputBlockSize, this._OutputBuffer, 0);
						this._InputBufferIndex = 0;
						if (bytesToDeliver < num6)
						{
							Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, currentOutputIndex, bytesToDeliver);
							this._OutputBufferIndex = num6 - bytesToDeliver;
							Buffer.InternalBlockCopy(this._OutputBuffer, bytesToDeliver, this._OutputBuffer, 0, this._OutputBufferIndex);
							return count;
						}
						Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, currentOutputIndex, num6);
						currentOutputIndex += num6;
						bytesToDeliver -= num6;
					}
					num = count;
				}
			}
			finally
			{
				sem.Release();
			}
			return num;
		}

		/// <summary>Writes a sequence of bytes to the current <see cref="T:System.Security.Cryptography.CryptoStream" /> and advances the current position within the stream by the number of bytes written.</summary>
		/// <param name="buffer">An array of bytes. This method copies <paramref name="count" /> bytes from <paramref name="buffer" /> to the current stream.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream.</param>
		/// <param name="count">The number of bytes to be written to the current stream.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Security.Cryptography.CryptoStreamMode" /> associated with current <see cref="T:System.Security.Cryptography.CryptoStream" /> object does not match the underlying stream.  For example, this exception is thrown when using <see cref="F:System.Security.Cryptography.CryptoStreamMode.Write" /> with an underlying stream that is read only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than zero.  
		///  -or-  
		///  The <paramref name="count" /> parameter is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of the <paramref name="count" /> and <paramref name="offset" /> parameters is longer than the length of the buffer.</exception>
		// Token: 0x0600214C RID: 8524 RVA: 0x000759F0 File Offset: 0x00073BF0
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnwritableStream"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			int i = count;
			int num = offset;
			if (this._InputBufferIndex > 0)
			{
				if (count < this._InputBlockSize - this._InputBufferIndex)
				{
					Buffer.InternalBlockCopy(buffer, offset, this._InputBuffer, this._InputBufferIndex, count);
					this._InputBufferIndex += count;
					return;
				}
				Buffer.InternalBlockCopy(buffer, offset, this._InputBuffer, this._InputBufferIndex, this._InputBlockSize - this._InputBufferIndex);
				num += this._InputBlockSize - this._InputBufferIndex;
				i -= this._InputBlockSize - this._InputBufferIndex;
				this._InputBufferIndex = this._InputBlockSize;
			}
			if (this._OutputBufferIndex > 0)
			{
				this._stream.Write(this._OutputBuffer, 0, this._OutputBufferIndex);
				this._OutputBufferIndex = 0;
			}
			if (this._InputBufferIndex == this._InputBlockSize)
			{
				int num2 = this._Transform.TransformBlock(this._InputBuffer, 0, this._InputBlockSize, this._OutputBuffer, 0);
				this._stream.Write(this._OutputBuffer, 0, num2);
				this._InputBufferIndex = 0;
			}
			while (i > 0)
			{
				if (i < this._InputBlockSize)
				{
					Buffer.InternalBlockCopy(buffer, num, this._InputBuffer, 0, i);
					this._InputBufferIndex += i;
					return;
				}
				if (this._Transform.CanTransformMultipleBlocks)
				{
					int num3 = i / this._InputBlockSize;
					int num4 = num3 * this._InputBlockSize;
					byte[] array = new byte[num3 * this._OutputBlockSize];
					int num2 = this._Transform.TransformBlock(buffer, num, num4, array, 0);
					this._stream.Write(array, 0, num2);
					num += num4;
					i -= num4;
				}
				else
				{
					int num2 = this._Transform.TransformBlock(buffer, num, this._InputBlockSize, this._OutputBuffer, 0);
					this._stream.Write(this._OutputBuffer, 0, num2);
					num += this._InputBlockSize;
					i -= this._InputBlockSize;
				}
			}
		}

		/// <summary>Writes a sequence of bytes to the current stream asynchronously, advances the current position within the stream by the number of bytes written, and monitors cancellation requests.</summary>
		/// <param name="buffer">The buffer to write data from.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> from which to begin writing bytes to the stream.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is currently in use by a previous write operation.</exception>
		// Token: 0x0600214D RID: 8525 RVA: 0x00075C38 File Offset: 0x00073E38
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnwritableStream"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (base.GetType() != typeof(CryptoStream))
			{
				return base.WriteAsync(buffer, offset, count, cancellationToken);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation(cancellationToken);
			}
			return this.WriteAsyncInternal(buffer, offset, count, cancellationToken);
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x00075CE8 File Offset: 0x00073EE8
		private async Task WriteAsyncInternal(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			await default(CryptoStream.HopToThreadPoolAwaitable);
			SemaphoreSlim sem = base.EnsureAsyncActiveSemaphoreInitialized();
			await sem.WaitAsync().ConfigureAwait(false);
			try
			{
				int bytesToWrite = count;
				int currentInputIndex = offset;
				if (this._InputBufferIndex > 0)
				{
					if (count < this._InputBlockSize - this._InputBufferIndex)
					{
						Buffer.InternalBlockCopy(buffer, offset, this._InputBuffer, this._InputBufferIndex, count);
						this._InputBufferIndex += count;
						return;
					}
					Buffer.InternalBlockCopy(buffer, offset, this._InputBuffer, this._InputBufferIndex, this._InputBlockSize - this._InputBufferIndex);
					currentInputIndex += this._InputBlockSize - this._InputBufferIndex;
					bytesToWrite -= this._InputBlockSize - this._InputBufferIndex;
					this._InputBufferIndex = this._InputBlockSize;
				}
				if (this._OutputBufferIndex > 0)
				{
					await this._stream.WriteAsync(this._OutputBuffer, 0, this._OutputBufferIndex, cancellationToken).ConfigureAwait(false);
					this._OutputBufferIndex = 0;
				}
				if (this._InputBufferIndex == this._InputBlockSize)
				{
					int num = this._Transform.TransformBlock(this._InputBuffer, 0, this._InputBlockSize, this._OutputBuffer, 0);
					await this._stream.WriteAsync(this._OutputBuffer, 0, num, cancellationToken).ConfigureAwait(false);
					this._InputBufferIndex = 0;
				}
				while (bytesToWrite > 0)
				{
					if (bytesToWrite < this._InputBlockSize)
					{
						Buffer.InternalBlockCopy(buffer, currentInputIndex, this._InputBuffer, 0, bytesToWrite);
						this._InputBufferIndex += bytesToWrite;
						break;
					}
					if (this._Transform.CanTransformMultipleBlocks)
					{
						int num2 = bytesToWrite / this._InputBlockSize;
						int numWholeBlocksInBytes = num2 * this._InputBlockSize;
						byte[] array = new byte[num2 * this._OutputBlockSize];
						int num = this._Transform.TransformBlock(buffer, currentInputIndex, numWholeBlocksInBytes, array, 0);
						await this._stream.WriteAsync(array, 0, num, cancellationToken).ConfigureAwait(false);
						currentInputIndex += numWholeBlocksInBytes;
						bytesToWrite -= numWholeBlocksInBytes;
					}
					else
					{
						int num = this._Transform.TransformBlock(buffer, currentInputIndex, this._InputBlockSize, this._OutputBuffer, 0);
						await this._stream.WriteAsync(this._OutputBuffer, 0, num, cancellationToken).ConfigureAwait(false);
						currentInputIndex += this._InputBlockSize;
						bytesToWrite -= this._InputBlockSize;
					}
				}
			}
			finally
			{
				sem.Release();
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Cryptography.CryptoStream" />.</summary>
		// Token: 0x0600214F RID: 8527 RVA: 0x00075D4C File Offset: 0x00073F4C
		public void Clear()
		{
			this.Close();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.CryptoStream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002150 RID: 8528 RVA: 0x00075D54 File Offset: 0x00073F54
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (!this._finalBlockTransformed)
					{
						this.FlushFinalBlock();
					}
					if (!this._leaveOpen)
					{
						this._stream.Close();
					}
				}
			}
			finally
			{
				try
				{
					this._finalBlockTransformed = true;
					if (this._InputBuffer != null)
					{
						Array.Clear(this._InputBuffer, 0, this._InputBuffer.Length);
					}
					if (this._OutputBuffer != null)
					{
						Array.Clear(this._OutputBuffer, 0, this._OutputBuffer.Length);
					}
					this._InputBuffer = null;
					this._OutputBuffer = null;
					this._canRead = false;
					this._canWrite = false;
				}
				finally
				{
					base.Dispose(disposing);
				}
			}
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x00075E0C File Offset: 0x0007400C
		private void InitializeBuffer()
		{
			if (this._Transform != null)
			{
				this._InputBlockSize = this._Transform.InputBlockSize;
				this._InputBuffer = new byte[this._InputBlockSize];
				this._OutputBlockSize = this._Transform.OutputBlockSize;
				this._OutputBuffer = new byte[this._OutputBlockSize];
			}
		}

		// Token: 0x04000C1C RID: 3100
		private Stream _stream;

		// Token: 0x04000C1D RID: 3101
		private ICryptoTransform _Transform;

		// Token: 0x04000C1E RID: 3102
		private byte[] _InputBuffer;

		// Token: 0x04000C1F RID: 3103
		private int _InputBufferIndex;

		// Token: 0x04000C20 RID: 3104
		private int _InputBlockSize;

		// Token: 0x04000C21 RID: 3105
		private byte[] _OutputBuffer;

		// Token: 0x04000C22 RID: 3106
		private int _OutputBufferIndex;

		// Token: 0x04000C23 RID: 3107
		private int _OutputBlockSize;

		// Token: 0x04000C24 RID: 3108
		private CryptoStreamMode _transformMode;

		// Token: 0x04000C25 RID: 3109
		private bool _canRead;

		// Token: 0x04000C26 RID: 3110
		private bool _canWrite;

		// Token: 0x04000C27 RID: 3111
		private bool _finalBlockTransformed;

		// Token: 0x04000C28 RID: 3112
		private bool _leaveOpen;

		// Token: 0x02000B3F RID: 2879
		private struct HopToThreadPoolAwaitable : INotifyCompletion
		{
			// Token: 0x06006B99 RID: 27545 RVA: 0x00175189 File Offset: 0x00173389
			public CryptoStream.HopToThreadPoolAwaitable GetAwaiter()
			{
				return this;
			}

			// Token: 0x17001224 RID: 4644
			// (get) Token: 0x06006B9A RID: 27546 RVA: 0x00175191 File Offset: 0x00173391
			public bool IsCompleted
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06006B9B RID: 27547 RVA: 0x00175194 File Offset: 0x00173394
			public void OnCompleted(Action continuation)
			{
				Task.Run(continuation);
			}

			// Token: 0x06006B9C RID: 27548 RVA: 0x0017519D File Offset: 0x0017339D
			public void GetResult()
			{
			}
		}
	}
}
