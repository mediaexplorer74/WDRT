using System;
using System.Security.Permissions;

namespace System.IO.Compression
{
	/// <summary>Provides methods and properties used to compress and decompress streams.</summary>
	// Token: 0x0200042E RID: 1070
	[global::__DynamicallyInvokable]
	public class GZipStream : Stream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.GZipStream" /> class by using the specified stream and compression mode.</summary>
		/// <param name="stream">The stream the compressed or decompressed data is written to.</param>
		/// <param name="mode">One of the enumeration values that indicates whether to compress or decompress the stream.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a valid <see cref="T:System.IO.Compression.CompressionMode" /> enumeration value.  
		/// -or-  
		/// <see cref="T:System.IO.Compression.CompressionMode" /> is <see cref="F:System.IO.Compression.CompressionMode.Compress" /> and <see cref="P:System.IO.Stream.CanWrite" /> is <see langword="false" />.  
		/// -or-  
		/// <see cref="T:System.IO.Compression.CompressionMode" /> is <see cref="F:System.IO.Compression.CompressionMode.Decompress" /> and <see cref="P:System.IO.Stream.CanRead" /> is <see langword="false" />.</exception>
		// Token: 0x0600280C RID: 10252 RVA: 0x000B814D File Offset: 0x000B634D
		[global::__DynamicallyInvokable]
		public GZipStream(Stream stream, CompressionMode mode)
			: this(stream, mode, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.GZipStream" /> class by using the specified stream and compression mode, and optionally leaves the stream open.</summary>
		/// <param name="stream">The stream the compressed or decompressed data is written to.</param>
		/// <param name="mode">One of the enumeration values that indicates whether to compress or decompress the stream.</param>
		/// <param name="leaveOpen">
		///   <see langword="true" /> to leave the stream open after disposing the <see cref="T:System.IO.Compression.GZipStream" /> object; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a valid <see cref="T:System.IO.Compression.CompressionMode" /> value.  
		/// -or-  
		/// <see cref="T:System.IO.Compression.CompressionMode" /> is <see cref="F:System.IO.Compression.CompressionMode.Compress" /> and <see cref="P:System.IO.Stream.CanWrite" /> is <see langword="false" />.  
		/// -or-  
		/// <see cref="T:System.IO.Compression.CompressionMode" /> is <see cref="F:System.IO.Compression.CompressionMode.Decompress" /> and <see cref="P:System.IO.Stream.CanRead" /> is <see langword="false" />.</exception>
		// Token: 0x0600280D RID: 10253 RVA: 0x000B8158 File Offset: 0x000B6358
		[global::__DynamicallyInvokable]
		public GZipStream(Stream stream, CompressionMode mode, bool leaveOpen)
		{
			if (mode == CompressionMode.Decompress)
			{
				this.deflateStream = new DeflateStream(stream, leaveOpen, new GZipDecoder());
				return;
			}
			this.deflateStream = new DeflateStream(stream, mode, leaveOpen);
			this.deflateStream.SetFileFormatWriter(new GZipFormatter());
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.GZipStream" /> class by using the specified stream and compression level.</summary>
		/// <param name="stream">The stream to write the compressed data to.</param>
		/// <param name="compressionLevel">One of the enumeration values that indicates whether to emphasize speed or compression efficiency when compressing the stream.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The stream does not support write operations such as compression. (The <see cref="P:System.IO.Stream.CanWrite" /> property on the stream object is <see langword="false" />.)</exception>
		// Token: 0x0600280E RID: 10254 RVA: 0x000B8194 File Offset: 0x000B6394
		[global::__DynamicallyInvokable]
		public GZipStream(Stream stream, CompressionLevel compressionLevel)
			: this(stream, compressionLevel, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.GZipStream" /> class by using the specified stream and compression level, and optionally leaves the stream open.</summary>
		/// <param name="stream">The stream to write the compressed data to.</param>
		/// <param name="compressionLevel">One of the enumeration values that indicates whether to emphasize speed or compression efficiency when compressing the stream.</param>
		/// <param name="leaveOpen">
		///   <see langword="true" /> to leave the stream object open after disposing the <see cref="T:System.IO.Compression.GZipStream" /> object; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The stream does not support write operations such as compression. (The <see cref="P:System.IO.Stream.CanWrite" /> property on the stream object is <see langword="false" />.)</exception>
		// Token: 0x0600280F RID: 10255 RVA: 0x000B819F File Offset: 0x000B639F
		[global::__DynamicallyInvokable]
		public GZipStream(Stream stream, CompressionLevel compressionLevel, bool leaveOpen)
		{
			this.deflateStream = new DeflateStream(stream, compressionLevel, leaveOpen);
			this.deflateStream.SetFileFormatWriter(new GZipFormatter());
		}

		/// <summary>Gets a value indicating whether the stream supports reading while decompressing a file.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.IO.Compression.CompressionMode" /> value is <see langword="Decompress," /> and the underlying stream supports reading and is not closed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06002810 RID: 10256 RVA: 0x000B81C5 File Offset: 0x000B63C5
		[global::__DynamicallyInvokable]
		public override bool CanRead
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.deflateStream != null && this.deflateStream.CanRead;
			}
		}

		/// <summary>Gets a value indicating whether the stream supports writing.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.IO.Compression.CompressionMode" /> value is <see langword="Compress" />, and the underlying stream supports writing and is not closed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06002811 RID: 10257 RVA: 0x000B81DC File Offset: 0x000B63DC
		[global::__DynamicallyInvokable]
		public override bool CanWrite
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.deflateStream != null && this.deflateStream.CanWrite;
			}
		}

		/// <summary>Gets a value indicating whether the stream supports seeking.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06002812 RID: 10258 RVA: 0x000B81F3 File Offset: 0x000B63F3
		[global::__DynamicallyInvokable]
		public override bool CanSeek
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.deflateStream != null && this.deflateStream.CanSeek;
			}
		}

		/// <summary>This property is not supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>A long value.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this stream.</exception>
		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06002813 RID: 10259 RVA: 0x000B820A File Offset: 0x000B640A
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
		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06002814 RID: 10260 RVA: 0x000B821B File Offset: 0x000B641B
		// (set) Token: 0x06002815 RID: 10261 RVA: 0x000B822C File Offset: 0x000B642C
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
		// Token: 0x06002816 RID: 10262 RVA: 0x000B823D File Offset: 0x000B643D
		[global::__DynamicallyInvokable]
		public override void Flush()
		{
			if (this.deflateStream == null)
			{
				throw new ObjectDisposedException(null, SR.GetString("ObjectDisposed_StreamClosed"));
			}
			this.deflateStream.Flush();
		}

		/// <summary>This property is not supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="offset">The location in the stream.</param>
		/// <param name="origin">One of the <see cref="T:System.IO.SeekOrigin" /> values.</param>
		/// <returns>A long value.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this stream.</exception>
		// Token: 0x06002817 RID: 10263 RVA: 0x000B8263 File Offset: 0x000B6463
		[global::__DynamicallyInvokable]
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("NotSupported"));
		}

		/// <summary>This property is not supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="value">The length of the stream.</param>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this stream.</exception>
		// Token: 0x06002818 RID: 10264 RVA: 0x000B8274 File Offset: 0x000B6474
		[global::__DynamicallyInvokable]
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.GetString("NotSupported"));
		}

		/// <summary>Begins an asynchronous read operation. (Consider using the <see cref="M:System.IO.Stream.ReadAsync(System.Byte[],System.Int32,System.Int32)" /> method instead.)</summary>
		/// <param name="array">The byte array to read the data into.</param>
		/// <param name="offset">The byte offset in <paramref name="array" /> at which to begin reading data from the stream.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <param name="asyncCallback">An optional asynchronous callback, to be called when the read operation is complete.</param>
		/// <param name="asyncState">A user-provided object that distinguishes this particular asynchronous read request from other requests.</param>
		/// <returns>An object that represents the asynchronous read operation, which could still be pending.</returns>
		/// <exception cref="T:System.IO.IOException">The method tried to  read asynchronously past the end of the stream, or a disk error occurred.</exception>
		/// <exception cref="T:System.ArgumentException">One or more of the arguments is invalid.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The current <see cref="T:System.IO.Compression.GZipStream" /> implementation does not support the read operation.</exception>
		/// <exception cref="T:System.InvalidOperationException">A read operation cannot be performed because the stream is closed.</exception>
		// Token: 0x06002819 RID: 10265 RVA: 0x000B8285 File Offset: 0x000B6485
		[global::__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] array, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			if (this.deflateStream == null)
			{
				throw new InvalidOperationException(SR.GetString("ObjectDisposed_StreamClosed"));
			}
			return this.deflateStream.BeginRead(array, offset, count, asyncCallback, asyncState);
		}

		/// <summary>Waits for the pending asynchronous read to complete. (Consider using the <see cref="M:System.IO.Stream.ReadAsync(System.Byte[],System.Int32,System.Int32)" /> method instead.)</summary>
		/// <param name="asyncResult">The reference to the pending asynchronous request to finish.</param>
		/// <returns>The number of bytes read from the stream, between 0 (zero) and the number of bytes you requested. <see cref="T:System.IO.Compression.GZipStream" /> returns 0 only at the end of the stream; otherwise, it blocks until at least one byte is available.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> did not originate from a <see cref="M:System.IO.Compression.GZipStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> method on the current stream.</exception>
		/// <exception cref="T:System.InvalidOperationException">The end operation cannot be performed because the stream is closed.</exception>
		// Token: 0x0600281A RID: 10266 RVA: 0x000B82B1 File Offset: 0x000B64B1
		[global::__DynamicallyInvokable]
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (this.deflateStream == null)
			{
				throw new InvalidOperationException(SR.GetString("ObjectDisposed_StreamClosed"));
			}
			return this.deflateStream.EndRead(asyncResult);
		}

		/// <summary>Begins an asynchronous write operation. (Consider using the <see cref="M:System.IO.Stream.WriteAsync(System.Byte[],System.Int32,System.Int32)" /> method instead.)</summary>
		/// <param name="array">The buffer containing data to write to the current stream.</param>
		/// <param name="offset">The byte offset in <paramref name="array" /> at which to begin writing.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <param name="asyncCallback">An optional asynchronous callback to be called when the write operation is complete.</param>
		/// <param name="asyncState">A user-provided object that distinguishes this particular asynchronous write request from other requests.</param>
		/// <returns>An  object that represents the asynchronous write operation, which could still be pending.</returns>
		/// <exception cref="T:System.InvalidOperationException">The underlying stream is <see langword="null" />.  
		///  -or-  
		///  The underlying stream is closed.</exception>
		// Token: 0x0600281B RID: 10267 RVA: 0x000B82D7 File Offset: 0x000B64D7
		[global::__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] array, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			if (this.deflateStream == null)
			{
				throw new InvalidOperationException(SR.GetString("ObjectDisposed_StreamClosed"));
			}
			return this.deflateStream.BeginWrite(array, offset, count, asyncCallback, asyncState);
		}

		/// <summary>Handles the end of an asynchronous write operation. (Consider using the <see cref="M:System.IO.Stream.WriteAsync(System.Byte[],System.Int32,System.Int32)" /> method instead.)</summary>
		/// <param name="asyncResult">The object that represents the asynchronous call.</param>
		/// <exception cref="T:System.InvalidOperationException">The underlying stream is <see langword="null" />.  
		///  -or-  
		///  The underlying stream is closed.</exception>
		// Token: 0x0600281C RID: 10268 RVA: 0x000B8303 File Offset: 0x000B6503
		[global::__DynamicallyInvokable]
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (this.deflateStream == null)
			{
				throw new InvalidOperationException(SR.GetString("ObjectDisposed_StreamClosed"));
			}
			this.deflateStream.EndWrite(asyncResult);
		}

		/// <summary>Reads a number of decompressed bytes into the specified byte array.</summary>
		/// <param name="array">The array used to store decompressed bytes.</param>
		/// <param name="offset">The byte offset in <paramref name="array" /> at which the read bytes will be placed.</param>
		/// <param name="count">The maximum number of decompressed bytes to read.</param>
		/// <returns>The number of bytes that were decompressed into the byte array. If the end of the stream has been reached, zero or the number of bytes read is returned.</returns>
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
		// Token: 0x0600281D RID: 10269 RVA: 0x000B8329 File Offset: 0x000B6529
		[global::__DynamicallyInvokable]
		public override int Read(byte[] array, int offset, int count)
		{
			if (this.deflateStream == null)
			{
				throw new ObjectDisposedException(null, SR.GetString("ObjectDisposed_StreamClosed"));
			}
			return this.deflateStream.Read(array, offset, count);
		}

		/// <summary>Writes compressed bytes to the underlying stream from the specified byte array.</summary>
		/// <param name="array">The buffer that contains the data to compress.</param>
		/// <param name="offset">The byte offset in <paramref name="array" /> from which the bytes will be read.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The write operation cannot be performed because the stream is closed.</exception>
		// Token: 0x0600281E RID: 10270 RVA: 0x000B8352 File Offset: 0x000B6552
		[global::__DynamicallyInvokable]
		public override void Write(byte[] array, int offset, int count)
		{
			if (this.deflateStream == null)
			{
				throw new ObjectDisposedException(null, SR.GetString("ObjectDisposed_StreamClosed"));
			}
			this.deflateStream.Write(array, offset, count);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.Compression.GZipStream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x0600281F RID: 10271 RVA: 0x000B837C File Offset: 0x000B657C
		[global::__DynamicallyInvokable]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.deflateStream != null)
				{
					this.deflateStream.Close();
				}
				this.deflateStream = null;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>Gets a reference to the underlying stream.</summary>
		/// <returns>A stream object that represents the underlying stream.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The underlying stream is closed.</exception>
		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06002820 RID: 10272 RVA: 0x000B83C0 File Offset: 0x000B65C0
		[global::__DynamicallyInvokable]
		public Stream BaseStream
		{
			[global::__DynamicallyInvokable]
			get
			{
				if (this.deflateStream != null)
				{
					return this.deflateStream.BaseStream;
				}
				return null;
			}
		}

		// Token: 0x040021C5 RID: 8645
		private DeflateStream deflateStream;
	}
}
