using System;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.IO.Compression
{
	/// <summary>Provides methods and properties for compressing and decompressing streams by using the Deflate algorithm.</summary>
	// Token: 0x02000426 RID: 1062
	[global::__DynamicallyInvokable]
	public class DeflateStream : Stream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.DeflateStream" /> class by using the specified stream and compression mode.</summary>
		/// <param name="stream">The stream to compress or decompress.</param>
		/// <param name="mode">One of the enumeration values that indicates whether to compress or decompress the stream.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a valid <see cref="T:System.IO.Compression.CompressionMode" /> value.  
		/// -or-  
		/// <see cref="T:System.IO.Compression.CompressionMode" /> is <see cref="F:System.IO.Compression.CompressionMode.Compress" /> and <see cref="P:System.IO.Stream.CanWrite" /> is <see langword="false" />.  
		/// -or-  
		/// <see cref="T:System.IO.Compression.CompressionMode" /> is <see cref="F:System.IO.Compression.CompressionMode.Decompress" /> and <see cref="P:System.IO.Stream.CanRead" /> is <see langword="false" />.</exception>
		// Token: 0x060027A8 RID: 10152 RVA: 0x000B6635 File Offset: 0x000B4835
		[global::__DynamicallyInvokable]
		public DeflateStream(Stream stream, CompressionMode mode)
			: this(stream, mode, false)
		{
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x000B6640 File Offset: 0x000B4840
		internal DeflateStream(Stream stream, bool leaveOpen, IFileFormatReader reader)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException(SR.GetString("NotReadableStream"), "stream");
			}
			this.inflater = DeflateStream.CreateInflater(reader);
			this.m_CallBack = new AsyncCallback(this.ReadCallback);
			this._stream = stream;
			this._mode = CompressionMode.Decompress;
			this._leaveOpen = leaveOpen;
			this.buffer = new byte[8192];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.DeflateStream" /> class by using the specified stream and compression mode, and optionally leaves the stream open.</summary>
		/// <param name="stream">The stream to compress or decompress.</param>
		/// <param name="mode">One of the enumeration values that indicates whether to compress or decompress the stream.</param>
		/// <param name="leaveOpen">
		///   <see langword="true" /> to leave the stream open after disposing the <see cref="T:System.IO.Compression.DeflateStream" /> object; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a valid <see cref="T:System.IO.Compression.CompressionMode" /> value.  
		/// -or-  
		/// <see cref="T:System.IO.Compression.CompressionMode" /> is <see cref="F:System.IO.Compression.CompressionMode.Compress" /> and <see cref="P:System.IO.Stream.CanWrite" /> is <see langword="false" />.  
		/// -or-  
		/// <see cref="T:System.IO.Compression.CompressionMode" /> is <see cref="F:System.IO.Compression.CompressionMode.Decompress" /> and <see cref="P:System.IO.Stream.CanRead" /> is <see langword="false" />.</exception>
		// Token: 0x060027AA RID: 10154 RVA: 0x000B66C4 File Offset: 0x000B48C4
		[global::__DynamicallyInvokable]
		public DeflateStream(Stream stream, CompressionMode mode, bool leaveOpen)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (CompressionMode.Compress != mode && mode != CompressionMode.Decompress)
			{
				throw new ArgumentException(SR.GetString("ArgumentOutOfRange_Enum"), "mode");
			}
			this._stream = stream;
			this._mode = mode;
			this._leaveOpen = leaveOpen;
			CompressionMode mode2 = this._mode;
			if (mode2 != CompressionMode.Decompress)
			{
				if (mode2 == CompressionMode.Compress)
				{
					if (!this._stream.CanWrite)
					{
						throw new ArgumentException(SR.GetString("NotWriteableStream"), "stream");
					}
					this.deflater = DeflateStream.CreateDeflater(null);
					this.m_AsyncWriterDelegate = new DeflateStream.AsyncWriteDelegate(this.InternalWrite);
					this.m_CallBack = new AsyncCallback(this.WriteCallback);
				}
			}
			else
			{
				if (!this._stream.CanRead)
				{
					throw new ArgumentException(SR.GetString("NotReadableStream"), "stream");
				}
				this.inflater = DeflateStream.CreateInflater(null);
				this.m_CallBack = new AsyncCallback(this.ReadCallback);
			}
			this.buffer = new byte[8192];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.DeflateStream" /> class by using the specified stream and compression level.</summary>
		/// <param name="stream">The stream to compress.</param>
		/// <param name="compressionLevel">One of the enumeration values that indicates whether to emphasize speed or compression efficiency when compressing the stream.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The stream does not support write operations such as compression. (The <see cref="P:System.IO.Stream.CanWrite" /> property on the stream object is <see langword="false" />.)</exception>
		// Token: 0x060027AB RID: 10155 RVA: 0x000B67D5 File Offset: 0x000B49D5
		[global::__DynamicallyInvokable]
		public DeflateStream(Stream stream, CompressionLevel compressionLevel)
			: this(stream, compressionLevel, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.DeflateStream" /> class by using the specified stream and compression level, and optionally leaves the stream open.</summary>
		/// <param name="stream">The stream to compress.</param>
		/// <param name="compressionLevel">One of the enumeration values that indicates whether to emphasize speed or compression efficiency when compressing the stream.</param>
		/// <param name="leaveOpen">
		///   <see langword="true" /> to leave the stream object open after disposing the <see cref="T:System.IO.Compression.DeflateStream" /> object; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The stream does not support write operations such as compression. (The <see cref="P:System.IO.Stream.CanWrite" /> property on the stream object is <see langword="false" />.)</exception>
		// Token: 0x060027AC RID: 10156 RVA: 0x000B67E0 File Offset: 0x000B49E0
		[global::__DynamicallyInvokable]
		public DeflateStream(Stream stream, CompressionLevel compressionLevel, bool leaveOpen)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentException(SR.GetString("NotWriteableStream"), "stream");
			}
			this._stream = stream;
			this._mode = CompressionMode.Compress;
			this._leaveOpen = leaveOpen;
			this.deflater = DeflateStream.CreateDeflater(new CompressionLevel?(compressionLevel));
			this.m_AsyncWriterDelegate = new DeflateStream.AsyncWriteDelegate(this.InternalWrite);
			this.m_CallBack = new AsyncCallback(this.WriteCallback);
			this.buffer = new byte[8192];
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x000B6878 File Offset: 0x000B4A78
		private static IDeflater CreateDeflater(CompressionLevel? compressionLevel)
		{
			DeflateStream.WorkerType workerType = DeflateStream.GetDeflaterType();
			if (workerType == DeflateStream.WorkerType.Managed)
			{
				return new DeflaterManaged();
			}
			if (workerType != DeflateStream.WorkerType.ZLib)
			{
				throw new SystemException("Program entered an unexpected state.");
			}
			if (compressionLevel != null)
			{
				return new DeflaterZLib(compressionLevel.Value);
			}
			return new DeflaterZLib();
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x000B68C0 File Offset: 0x000B4AC0
		private static IInflater CreateInflater(IFileFormatReader reader = null)
		{
			DeflateStream.WorkerType workerType = DeflateStream.GetInflaterType();
			if (workerType == DeflateStream.WorkerType.Managed)
			{
				return new Inflater(reader);
			}
			if (workerType != DeflateStream.WorkerType.ZLib)
			{
				throw new SystemException("Program entered an unexpected state.");
			}
			if (reader == null)
			{
				return new InflaterZlib(-15);
			}
			return new InflaterZlib(47);
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x000B6900 File Offset: 0x000B4B00
		[SecuritySafeCritical]
		private static DeflateStream.WorkerType GetDeflaterType()
		{
			if (DeflateStream.WorkerType.Unknown != DeflateStream.deflaterType)
			{
				return DeflateStream.deflaterType;
			}
			if (CLRConfig.CheckLegacyManagedDeflateStream())
			{
				return DeflateStream.deflaterType = DeflateStream.WorkerType.Managed;
			}
			if (!CompatibilitySwitches.IsNetFx45LegacyManagedDeflateStream)
			{
				return DeflateStream.deflaterType = DeflateStream.WorkerType.ZLib;
			}
			return DeflateStream.deflaterType = DeflateStream.WorkerType.Managed;
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x000B693F File Offset: 0x000B4B3F
		[SecuritySafeCritical]
		private static DeflateStream.WorkerType GetInflaterType()
		{
			if (DeflateStream.WorkerType.Unknown != DeflateStream.inflaterType)
			{
				return DeflateStream.inflaterType;
			}
			if (!LocalAppContextSwitches.DoNotUseNativeZipLibraryForDecompression)
			{
				return DeflateStream.inflaterType = DeflateStream.WorkerType.ZLib;
			}
			return DeflateStream.inflaterType = DeflateStream.WorkerType.Managed;
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x000B696D File Offset: 0x000B4B6D
		internal void SetFileFormatWriter(IFileFormatWriter writer)
		{
			if (writer != null)
			{
				this.formatWriter = writer;
			}
		}

		/// <summary>Gets a reference to the underlying stream.</summary>
		/// <returns>A stream object that represents the underlying stream.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The underlying stream is closed.</exception>
		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x060027B2 RID: 10162 RVA: 0x000B6979 File Offset: 0x000B4B79
		[global::__DynamicallyInvokable]
		public Stream BaseStream
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this._stream;
			}
		}

		/// <summary>Gets a value indicating whether the stream supports reading while decompressing a file.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.IO.Compression.CompressionMode" /> value is <see langword="Decompress" />, and the underlying stream is opened and supports reading; otherwise, <see langword="false" />.</returns>
		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x060027B3 RID: 10163 RVA: 0x000B6981 File Offset: 0x000B4B81
		[global::__DynamicallyInvokable]
		public override bool CanRead
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this._stream != null && this._mode == CompressionMode.Decompress && this._stream.CanRead;
			}
		}

		/// <summary>Gets a value indicating whether the stream supports writing.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.IO.Compression.CompressionMode" /> value is <see langword="Compress" />, and the underlying stream supports writing and is not closed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x060027B4 RID: 10164 RVA: 0x000B69A2 File Offset: 0x000B4BA2
		[global::__DynamicallyInvokable]
		public override bool CanWrite
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this._stream != null && this._mode == CompressionMode.Compress && this._stream.CanWrite;
			}
		}

		/// <summary>Gets a value indicating whether the stream supports seeking.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x060027B5 RID: 10165 RVA: 0x000B69C4 File Offset: 0x000B4BC4
		[global::__DynamicallyInvokable]
		public override bool CanSeek
		{
			[global::__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		/// <summary>This property is not supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>A long value.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this stream.</exception>
		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x060027B6 RID: 10166 RVA: 0x000B69C7 File Offset: 0x000B4BC7
		[global::__DynamicallyInvokable]
		public override long Length
		{
			[global::__DynamicallyInvokable]
			get
			{
				throw new NotSupportedException(SR.GetString("NotSupported"));
			}
		}

		/// <summary>This property is not supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>A long value.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this stream.</exception>
		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x060027B7 RID: 10167 RVA: 0x000B69D8 File Offset: 0x000B4BD8
		// (set) Token: 0x060027B8 RID: 10168 RVA: 0x000B69E9 File Offset: 0x000B4BE9
		[global::__DynamicallyInvokable]
		public override long Position
		{
			[global::__DynamicallyInvokable]
			get
			{
				throw new NotSupportedException(SR.GetString("NotSupported"));
			}
			[global::__DynamicallyInvokable]
			set
			{
				throw new NotSupportedException(SR.GetString("NotSupported"));
			}
		}

		/// <summary>The current implementation of this method has no functionality.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x060027B9 RID: 10169 RVA: 0x000B69FA File Offset: 0x000B4BFA
		[global::__DynamicallyInvokable]
		public override void Flush()
		{
			this.EnsureNotDisposed();
		}

		/// <summary>This operation is not supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="offset">The location in the stream.</param>
		/// <param name="origin">One of the <see cref="T:System.IO.SeekOrigin" /> values.</param>
		/// <returns>A long value.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this stream.</exception>
		// Token: 0x060027BA RID: 10170 RVA: 0x000B6A02 File Offset: 0x000B4C02
		[global::__DynamicallyInvokable]
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("NotSupported"));
		}

		/// <summary>This operation is not supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="value">The length of the stream.</param>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this stream.</exception>
		// Token: 0x060027BB RID: 10171 RVA: 0x000B6A13 File Offset: 0x000B4C13
		[global::__DynamicallyInvokable]
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.GetString("NotSupported"));
		}

		/// <summary>Reads a number of decompressed bytes into the specified byte array.</summary>
		/// <param name="array">The array to store decompressed bytes.</param>
		/// <param name="offset">The byte offset in <paramref name="array" /> at which the read bytes will be placed.</param>
		/// <param name="count">The maximum number of decompressed bytes to read.</param>
		/// <returns>The number of bytes that were read into the byte array.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.IO.Compression.CompressionMode" /> value was <see langword="Compress" /> when the object was created.  
		/// -or-
		///  The underlying stream does not support reading.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="array" /> length minus the index starting point is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">The data is in an invalid format.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x060027BC RID: 10172 RVA: 0x000B6A24 File Offset: 0x000B4C24
		[global::__DynamicallyInvokable]
		public override int Read(byte[] array, int offset, int count)
		{
			this.EnsureDecompressionMode();
			this.ValidateParameters(array, offset, count);
			this.EnsureNotDisposed();
			int num = offset;
			int num2 = count;
			for (;;)
			{
				int num3 = this.inflater.Inflate(array, num, num2);
				num += num3;
				num2 -= num3;
				if (num2 == 0 || this.inflater.Finished())
				{
					break;
				}
				int num4 = this._stream.Read(this.buffer, 0, this.buffer.Length);
				if (num4 == 0)
				{
					break;
				}
				this.inflater.SetInput(this.buffer, 0, num4);
			}
			return count - num2;
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x000B6AA8 File Offset: 0x000B4CA8
		private void ValidateParameters(byte[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException(SR.GetString("InvalidArgumentOffsetCount"));
			}
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x000B6AF9 File Offset: 0x000B4CF9
		private void EnsureNotDisposed()
		{
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, SR.GetString("ObjectDisposed_StreamClosed"));
			}
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x000B6B14 File Offset: 0x000B4D14
		private void EnsureDecompressionMode()
		{
			if (this._mode != CompressionMode.Decompress)
			{
				throw new InvalidOperationException(SR.GetString("CannotReadFromDeflateStream"));
			}
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x000B6B2E File Offset: 0x000B4D2E
		private void EnsureCompressionMode()
		{
			if (this._mode != CompressionMode.Compress)
			{
				throw new InvalidOperationException(SR.GetString("CannotWriteToDeflateStream"));
			}
		}

		/// <summary>Begins an asynchronous read operation. (Consider using the <see cref="M:System.IO.Stream.ReadAsync(System.Byte[],System.Int32,System.Int32)" /> method instead.)</summary>
		/// <param name="array">The byte array to read the data into.</param>
		/// <param name="offset">The byte offset in <paramref name="array" /> at which to begin reading data from the stream.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <param name="asyncCallback">An optional asynchronous callback, to be called when the read operation is complete.</param>
		/// <param name="asyncState">A user-provided object that distinguishes this particular asynchronous read request from other requests.</param>
		/// <returns>An  object that represents the asynchronous read operation, which could still be pending.</returns>
		/// <exception cref="T:System.IO.IOException">The method tried to read asynchronously past the end of the stream, or a disk error occurred.</exception>
		/// <exception cref="T:System.ArgumentException">One or more of the arguments is invalid.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The current <see cref="T:System.IO.Compression.DeflateStream" /> implementation does not support the read operation.</exception>
		/// <exception cref="T:System.InvalidOperationException">This call cannot be completed.</exception>
		// Token: 0x060027C1 RID: 10177 RVA: 0x000B6B4C File Offset: 0x000B4D4C
		[global::__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] array, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			this.EnsureDecompressionMode();
			if (this.asyncOperations != 0)
			{
				throw new InvalidOperationException(SR.GetString("InvalidBeginCall"));
			}
			this.ValidateParameters(array, offset, count);
			this.EnsureNotDisposed();
			Interlocked.Increment(ref this.asyncOperations);
			IAsyncResult asyncResult;
			try
			{
				DeflateStreamAsyncResult deflateStreamAsyncResult = new DeflateStreamAsyncResult(this, asyncState, asyncCallback, array, offset, count);
				deflateStreamAsyncResult.isWrite = false;
				int num = this.inflater.Inflate(array, offset, count);
				if (num != 0)
				{
					deflateStreamAsyncResult.InvokeCallback(true, num);
					asyncResult = deflateStreamAsyncResult;
				}
				else if (this.inflater.Finished())
				{
					deflateStreamAsyncResult.InvokeCallback(true, 0);
					asyncResult = deflateStreamAsyncResult;
				}
				else
				{
					this._stream.BeginRead(this.buffer, 0, this.buffer.Length, this.m_CallBack, deflateStreamAsyncResult);
					deflateStreamAsyncResult.m_CompletedSynchronously &= deflateStreamAsyncResult.IsCompleted;
					asyncResult = deflateStreamAsyncResult;
				}
			}
			catch
			{
				Interlocked.Decrement(ref this.asyncOperations);
				throw;
			}
			return asyncResult;
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x000B6C44 File Offset: 0x000B4E44
		private void ReadCallback(IAsyncResult baseStreamResult)
		{
			DeflateStreamAsyncResult deflateStreamAsyncResult = (DeflateStreamAsyncResult)baseStreamResult.AsyncState;
			deflateStreamAsyncResult.m_CompletedSynchronously &= baseStreamResult.CompletedSynchronously;
			try
			{
				this.EnsureNotDisposed();
				int num = this._stream.EndRead(baseStreamResult);
				if (num <= 0)
				{
					deflateStreamAsyncResult.InvokeCallback(0);
				}
				else
				{
					this.inflater.SetInput(this.buffer, 0, num);
					num = this.inflater.Inflate(deflateStreamAsyncResult.buffer, deflateStreamAsyncResult.offset, deflateStreamAsyncResult.count);
					if (num == 0 && !this.inflater.Finished())
					{
						this._stream.BeginRead(this.buffer, 0, this.buffer.Length, this.m_CallBack, deflateStreamAsyncResult);
					}
					else
					{
						deflateStreamAsyncResult.InvokeCallback(num);
					}
				}
			}
			catch (Exception ex)
			{
				deflateStreamAsyncResult.InvokeCallback(ex);
			}
		}

		/// <summary>Waits for the pending asynchronous read to complete. (Consider using the <see cref="M:System.IO.Stream.ReadAsync(System.Byte[],System.Int32,System.Int32)" /> method instead.)</summary>
		/// <param name="asyncResult">The reference to the pending asynchronous request to finish.</param>
		/// <returns>The number of bytes read from the stream, between 0 (zero) and the number of bytes you requested. <see cref="T:System.IO.Compression.DeflateStream" /> returns 0 only at the end of the stream; otherwise, it blocks until at least one byte is available.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> did not originate from a <see cref="M:System.IO.Compression.DeflateStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> method on the current stream.</exception>
		/// <exception cref="T:System.SystemException">An exception was thrown during a call to <see cref="M:System.Threading.WaitHandle.WaitOne" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The end call is invalid because asynchronous read operations for this stream are not yet complete.
		/// -or-
		/// The stream is <see langword="null" />.</exception>
		// Token: 0x060027C3 RID: 10179 RVA: 0x000B6D24 File Offset: 0x000B4F24
		[global::__DynamicallyInvokable]
		public override int EndRead(IAsyncResult asyncResult)
		{
			this.EnsureDecompressionMode();
			this.CheckEndXxxxLegalStateAndParams(asyncResult);
			DeflateStreamAsyncResult deflateStreamAsyncResult = (DeflateStreamAsyncResult)asyncResult;
			this.AwaitAsyncResultCompletion(deflateStreamAsyncResult);
			Exception ex = deflateStreamAsyncResult.Result as Exception;
			if (ex != null)
			{
				throw ex;
			}
			return (int)deflateStreamAsyncResult.Result;
		}

		/// <summary>Writes compressed bytes to the underlying stream from the specified byte array.</summary>
		/// <param name="array">The buffer that contains the data to compress.</param>
		/// <param name="offset">The byte offset in <paramref name="array" /> from which the bytes will be read.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		// Token: 0x060027C4 RID: 10180 RVA: 0x000B6D68 File Offset: 0x000B4F68
		[global::__DynamicallyInvokable]
		public override void Write(byte[] array, int offset, int count)
		{
			this.EnsureCompressionMode();
			this.ValidateParameters(array, offset, count);
			this.EnsureNotDisposed();
			this.InternalWrite(array, offset, count, false);
		}

		// Token: 0x060027C5 RID: 10181 RVA: 0x000B6D89 File Offset: 0x000B4F89
		internal void InternalWrite(byte[] array, int offset, int count, bool isAsync)
		{
			this.DoMaintenance(array, offset, count);
			this.WriteDeflaterOutput(isAsync);
			this.deflater.SetInput(array, offset, count);
			this.WriteDeflaterOutput(isAsync);
		}

		// Token: 0x060027C6 RID: 10182 RVA: 0x000B6DB4 File Offset: 0x000B4FB4
		private void WriteDeflaterOutput(bool isAsync)
		{
			while (!this.deflater.NeedsInput())
			{
				int deflateOutput = this.deflater.GetDeflateOutput(this.buffer);
				if (deflateOutput > 0)
				{
					this.DoWrite(this.buffer, 0, deflateOutput, isAsync);
				}
			}
		}

		// Token: 0x060027C7 RID: 10183 RVA: 0x000B6DF8 File Offset: 0x000B4FF8
		private void DoWrite(byte[] array, int offset, int count, bool isAsync)
		{
			if (isAsync)
			{
				IAsyncResult asyncResult = this._stream.BeginWrite(array, offset, count, null, null);
				this._stream.EndWrite(asyncResult);
				return;
			}
			this._stream.Write(array, offset, count);
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x000B6E38 File Offset: 0x000B5038
		private void DoMaintenance(byte[] array, int offset, int count)
		{
			if (count <= 0)
			{
				return;
			}
			this.wroteBytes = true;
			if (this.formatWriter == null)
			{
				return;
			}
			if (!this.wroteHeader)
			{
				byte[] header = this.formatWriter.GetHeader();
				this._stream.Write(header, 0, header.Length);
				this.wroteHeader = true;
			}
			this.formatWriter.UpdateWithBytesRead(array, offset, count);
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x000B6E94 File Offset: 0x000B5094
		private void PurgeBuffers(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			if (this._stream == null)
			{
				return;
			}
			this.Flush();
			if (this._mode != CompressionMode.Compress)
			{
				return;
			}
			if (this.wroteBytes)
			{
				this.WriteDeflaterOutput(false);
				bool flag;
				do
				{
					int num;
					flag = this.deflater.Finish(this.buffer, out num);
					if (num > 0)
					{
						this.DoWrite(this.buffer, 0, num, false);
					}
				}
				while (!flag);
			}
			if (this.formatWriter != null && this.wroteHeader)
			{
				byte[] footer = this.formatWriter.GetFooter();
				this._stream.Write(footer, 0, footer.Length);
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.Compression.DeflateStream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x060027CA RID: 10186 RVA: 0x000B6F24 File Offset: 0x000B5124
		[global::__DynamicallyInvokable]
		protected override void Dispose(bool disposing)
		{
			try
			{
				this.PurgeBuffers(disposing);
			}
			finally
			{
				try
				{
					if (disposing && !this._leaveOpen && this._stream != null)
					{
						this._stream.Close();
					}
				}
				finally
				{
					this._stream = null;
					try
					{
						if (this.deflater != null)
						{
							this.deflater.Dispose();
						}
						if (this.inflater != null)
						{
							this.inflater.Dispose();
						}
					}
					finally
					{
						this.inflater = null;
						this.deflater = null;
						base.Dispose(disposing);
					}
				}
			}
		}

		/// <summary>Begins an asynchronous write operation. (Consider using the <see cref="M:System.IO.Stream.WriteAsync(System.Byte[],System.Int32,System.Int32)" /> method instead.)</summary>
		/// <param name="array">The buffer to write data from.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> to begin writing from.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <param name="asyncCallback">An optional asynchronous callback, to be called when the write operation is complete.</param>
		/// <param name="asyncState">A user-provided object that distinguishes this particular asynchronous write request from other requests.</param>
		/// <returns>An object that represents the asynchronous write operation, which could still be pending.</returns>
		/// <exception cref="T:System.IO.IOException">The method tried to write asynchronously past the end of the stream, or a disk error occurred.</exception>
		/// <exception cref="T:System.ArgumentException">One or more of the arguments is invalid.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The current <see cref="T:System.IO.Compression.DeflateStream" /> implementation does not support the write operation.</exception>
		/// <exception cref="T:System.InvalidOperationException">The write operation cannot be performed because the stream is closed.</exception>
		// Token: 0x060027CB RID: 10187 RVA: 0x000B6FCC File Offset: 0x000B51CC
		[global::__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] array, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			this.EnsureCompressionMode();
			if (this.asyncOperations != 0)
			{
				throw new InvalidOperationException(SR.GetString("InvalidBeginCall"));
			}
			this.ValidateParameters(array, offset, count);
			this.EnsureNotDisposed();
			Interlocked.Increment(ref this.asyncOperations);
			IAsyncResult asyncResult;
			try
			{
				DeflateStreamAsyncResult deflateStreamAsyncResult = new DeflateStreamAsyncResult(this, asyncState, asyncCallback, array, offset, count);
				deflateStreamAsyncResult.isWrite = true;
				this.m_AsyncWriterDelegate.BeginInvoke(array, offset, count, true, this.m_CallBack, deflateStreamAsyncResult);
				deflateStreamAsyncResult.m_CompletedSynchronously &= deflateStreamAsyncResult.IsCompleted;
				asyncResult = deflateStreamAsyncResult;
			}
			catch
			{
				Interlocked.Decrement(ref this.asyncOperations);
				throw;
			}
			return asyncResult;
		}

		// Token: 0x060027CC RID: 10188 RVA: 0x000B7078 File Offset: 0x000B5278
		private void WriteCallback(IAsyncResult asyncResult)
		{
			DeflateStreamAsyncResult deflateStreamAsyncResult = (DeflateStreamAsyncResult)asyncResult.AsyncState;
			deflateStreamAsyncResult.m_CompletedSynchronously &= asyncResult.CompletedSynchronously;
			try
			{
				this.m_AsyncWriterDelegate.EndInvoke(asyncResult);
			}
			catch (Exception ex)
			{
				deflateStreamAsyncResult.InvokeCallback(ex);
				return;
			}
			deflateStreamAsyncResult.InvokeCallback(null);
		}

		/// <summary>Ends an asynchronous write operation. (Consider using the <see cref="M:System.IO.Stream.WriteAsync(System.Byte[],System.Int32,System.Int32)" /> method instead.)</summary>
		/// <param name="asyncResult">A reference to the outstanding asynchronous I/O request.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> did not originate from a <see cref="M:System.IO.Compression.DeflateStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> method on the current stream.</exception>
		/// <exception cref="T:System.Exception">An exception was thrown during a call to <see cref="M:System.Threading.WaitHandle.WaitOne" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is <see langword="null" />.
		/// -or-
		/// The end write call is invalid.</exception>
		// Token: 0x060027CD RID: 10189 RVA: 0x000B70D4 File Offset: 0x000B52D4
		[global::__DynamicallyInvokable]
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.EnsureCompressionMode();
			this.CheckEndXxxxLegalStateAndParams(asyncResult);
			DeflateStreamAsyncResult deflateStreamAsyncResult = (DeflateStreamAsyncResult)asyncResult;
			this.AwaitAsyncResultCompletion(deflateStreamAsyncResult);
			Exception ex = deflateStreamAsyncResult.Result as Exception;
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x000B7110 File Offset: 0x000B5310
		private void CheckEndXxxxLegalStateAndParams(IAsyncResult asyncResult)
		{
			if (this.asyncOperations != 1)
			{
				throw new InvalidOperationException(SR.GetString("InvalidEndCall"));
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			this.EnsureNotDisposed();
			if (!(asyncResult is DeflateStreamAsyncResult))
			{
				throw new ArgumentNullException("asyncResult");
			}
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x000B7160 File Offset: 0x000B5360
		private void AwaitAsyncResultCompletion(DeflateStreamAsyncResult asyncResult)
		{
			try
			{
				if (!asyncResult.IsCompleted)
				{
					asyncResult.AsyncWaitHandle.WaitOne();
				}
			}
			finally
			{
				Interlocked.Decrement(ref this.asyncOperations);
				asyncResult.Close();
			}
		}

		// Token: 0x0400217A RID: 8570
		internal const int DefaultBufferSize = 8192;

		// Token: 0x0400217B RID: 8571
		private const int WindowSizeUpperBound = 47;

		// Token: 0x0400217C RID: 8572
		private Stream _stream;

		// Token: 0x0400217D RID: 8573
		private CompressionMode _mode;

		// Token: 0x0400217E RID: 8574
		private bool _leaveOpen;

		// Token: 0x0400217F RID: 8575
		private IInflater inflater;

		// Token: 0x04002180 RID: 8576
		private IDeflater deflater;

		// Token: 0x04002181 RID: 8577
		private byte[] buffer;

		// Token: 0x04002182 RID: 8578
		private int asyncOperations;

		// Token: 0x04002183 RID: 8579
		private readonly AsyncCallback m_CallBack;

		// Token: 0x04002184 RID: 8580
		private readonly DeflateStream.AsyncWriteDelegate m_AsyncWriterDelegate;

		// Token: 0x04002185 RID: 8581
		private IFileFormatWriter formatWriter;

		// Token: 0x04002186 RID: 8582
		private bool wroteHeader;

		// Token: 0x04002187 RID: 8583
		private bool wroteBytes;

		// Token: 0x04002188 RID: 8584
		private static volatile DeflateStream.WorkerType deflaterType = DeflateStream.WorkerType.Unknown;

		// Token: 0x04002189 RID: 8585
		private static volatile DeflateStream.WorkerType inflaterType = DeflateStream.WorkerType.Unknown;

		// Token: 0x02000827 RID: 2087
		// (Invoke) Token: 0x06004524 RID: 17700
		internal delegate void AsyncWriteDelegate(byte[] array, int offset, int count, bool isAsync);

		// Token: 0x02000828 RID: 2088
		private enum WorkerType : byte
		{
			// Token: 0x040035B1 RID: 13745
			Managed,
			// Token: 0x040035B2 RID: 13746
			ZLib,
			// Token: 0x040035B3 RID: 13747
			Unknown
		}
	}
}
