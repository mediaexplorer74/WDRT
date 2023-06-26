using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Implements a <see cref="T:System.IO.TextReader" /> that reads characters from a byte stream in a particular encoding.</summary>
	// Token: 0x020001A2 RID: 418
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class StreamReader : TextReader
	{
		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060019A2 RID: 6562 RVA: 0x00055334 File Offset: 0x00053534
		internal static int DefaultBufferSize
		{
			get
			{
				return 1024;
			}
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x0005533C File Offset: 0x0005353C
		private void CheckAsyncTaskInProgress()
		{
			Task asyncReadTask = this._asyncReadTask;
			if (asyncReadTask != null && !asyncReadTask.IsCompleted)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AsyncIOInProgress"));
			}
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x0005536D File Offset: 0x0005356D
		internal StreamReader()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified stream.</summary>
		/// <param name="stream">The stream to be read.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> does not support reading.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		// Token: 0x060019A5 RID: 6565 RVA: 0x00055375 File Offset: 0x00053575
		[__DynamicallyInvokable]
		public StreamReader(Stream stream)
			: this(stream, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified stream, with the specified byte order mark detection option.</summary>
		/// <param name="stream">The stream to be read.</param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> does not support reading.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		// Token: 0x060019A6 RID: 6566 RVA: 0x0005537F File Offset: 0x0005357F
		[__DynamicallyInvokable]
		public StreamReader(Stream stream, bool detectEncodingFromByteOrderMarks)
			: this(stream, Encoding.UTF8, detectEncodingFromByteOrderMarks, StreamReader.DefaultBufferSize, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified stream, with the specified character encoding.</summary>
		/// <param name="stream">The stream to be read.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> does not support reading.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="encoding" /> is <see langword="null" />.</exception>
		// Token: 0x060019A7 RID: 6567 RVA: 0x00055394 File Offset: 0x00053594
		[__DynamicallyInvokable]
		public StreamReader(Stream stream, Encoding encoding)
			: this(stream, encoding, true, StreamReader.DefaultBufferSize, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified stream, with the specified character encoding and byte order mark detection option.</summary>
		/// <param name="stream">The stream to be read.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> does not support reading.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="encoding" /> is <see langword="null" />.</exception>
		// Token: 0x060019A8 RID: 6568 RVA: 0x000553A5 File Offset: 0x000535A5
		[__DynamicallyInvokable]
		public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks)
			: this(stream, encoding, detectEncodingFromByteOrderMarks, StreamReader.DefaultBufferSize, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified stream, with the specified character encoding, byte order mark detection option, and buffer size.</summary>
		/// <param name="stream">The stream to be read.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
		/// <param name="bufferSize">The minimum buffer size.</param>
		/// <exception cref="T:System.ArgumentException">The stream does not support reading.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="encoding" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is less than or equal to zero.</exception>
		// Token: 0x060019A9 RID: 6569 RVA: 0x000553B6 File Offset: 0x000535B6
		[__DynamicallyInvokable]
		public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
			: this(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified stream based on the specified character encoding, byte order mark detection option, and buffer size, and optionally leaves the stream open.</summary>
		/// <param name="stream">The stream to read.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="detectEncodingFromByteOrderMarks">
		///   <see langword="true" /> to look for byte order marks at the beginning of the file; otherwise, <see langword="false" />.</param>
		/// <param name="bufferSize">The minimum buffer size.</param>
		/// <param name="leaveOpen">
		///   <see langword="true" /> to leave the stream open after the <see cref="T:System.IO.StreamReader" /> object is disposed; otherwise, <see langword="false" />.</param>
		// Token: 0x060019AA RID: 6570 RVA: 0x000553C4 File Offset: 0x000535C4
		[__DynamicallyInvokable]
		public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize, bool leaveOpen)
		{
			if (stream == null || encoding == null)
			{
				throw new ArgumentNullException((stream == null) ? "stream" : "encoding");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_StreamNotReadable"));
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			this.Init(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified file name.</summary>
		/// <param name="path">The complete file path to be read.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label.</exception>
		// Token: 0x060019AB RID: 6571 RVA: 0x00055431 File Offset: 0x00053631
		public StreamReader(string path)
			: this(path, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified file name, with the specified byte order mark detection option.</summary>
		/// <param name="path">The complete file path to be read.</param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label.</exception>
		// Token: 0x060019AC RID: 6572 RVA: 0x0005543B File Offset: 0x0005363B
		public StreamReader(string path, bool detectEncodingFromByteOrderMarks)
			: this(path, Encoding.UTF8, detectEncodingFromByteOrderMarks, StreamReader.DefaultBufferSize)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified file name, with the specified character encoding.</summary>
		/// <param name="path">The complete file path to be read.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="encoding" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label.</exception>
		// Token: 0x060019AD RID: 6573 RVA: 0x0005544F File Offset: 0x0005364F
		public StreamReader(string path, Encoding encoding)
			: this(path, encoding, true, StreamReader.DefaultBufferSize)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified file name, with the specified character encoding and byte order mark detection option.</summary>
		/// <param name="path">The complete file path to be read.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="encoding" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label.</exception>
		// Token: 0x060019AE RID: 6574 RVA: 0x0005545F File Offset: 0x0005365F
		public StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks)
			: this(path, encoding, detectEncodingFromByteOrderMarks, StreamReader.DefaultBufferSize)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified file name, with the specified character encoding, byte order mark detection option, and buffer size.</summary>
		/// <param name="path">The complete file path to be read.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
		/// <param name="bufferSize">The minimum buffer size, in number of 16-bit characters.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="encoding" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="buffersize" /> is less than or equal to zero.</exception>
		// Token: 0x060019AF RID: 6575 RVA: 0x0005546F File Offset: 0x0005366F
		[SecuritySafeCritical]
		public StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
			: this(path, encoding, detectEncodingFromByteOrderMarks, bufferSize, true)
		{
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x00055480 File Offset: 0x00053680
		[SecurityCritical]
		internal StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize, bool checkHost)
		{
			if (path == null || encoding == null)
			{
				throw new ArgumentNullException((path == null) ? "path" : "encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.SequentialScan, Path.GetFileName(path), false, false, checkHost);
			this.Init(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, false);
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x0005550C File Offset: 0x0005370C
		private void Init(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize, bool leaveOpen)
		{
			this.stream = stream;
			this.encoding = encoding;
			this.decoder = encoding.GetDecoder();
			if (bufferSize < 128)
			{
				bufferSize = 128;
			}
			this.byteBuffer = new byte[bufferSize];
			this._maxCharsPerBuffer = encoding.GetMaxCharCount(bufferSize);
			this.charBuffer = new char[this._maxCharsPerBuffer];
			this.byteLen = 0;
			this.bytePos = 0;
			this._detectEncoding = detectEncodingFromByteOrderMarks;
			this._preamble = encoding.GetPreamble();
			this._checkPreamble = this._preamble.Length != 0;
			this._isBlocked = false;
			this._closable = !leaveOpen;
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x000555B2 File Offset: 0x000537B2
		internal void Init(Stream stream)
		{
			this.stream = stream;
			this._closable = true;
		}

		/// <summary>Closes the <see cref="T:System.IO.StreamReader" /> object and the underlying stream, and releases any system resources associated with the reader.</summary>
		// Token: 0x060019B3 RID: 6579 RVA: 0x000555C2 File Offset: 0x000537C2
		public override void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Closes the underlying stream, releases the unmanaged resources used by the <see cref="T:System.IO.StreamReader" />, and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x060019B4 RID: 6580 RVA: 0x000555CC File Offset: 0x000537CC
		[__DynamicallyInvokable]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this.LeaveOpen && disposing && this.stream != null)
				{
					this.stream.Close();
				}
			}
			finally
			{
				if (!this.LeaveOpen && this.stream != null)
				{
					this.stream = null;
					this.encoding = null;
					this.decoder = null;
					this.byteBuffer = null;
					this.charBuffer = null;
					this.charPos = 0;
					this.charLen = 0;
					base.Dispose(disposing);
				}
			}
		}

		/// <summary>Gets the current character encoding that the current <see cref="T:System.IO.StreamReader" /> object is using.</summary>
		/// <returns>The current character encoding used by the current reader. The value can be different after the first call to any <see cref="Overload:System.IO.StreamReader.Read" /> method of <see cref="T:System.IO.StreamReader" />, since encoding autodetection is not done until the first call to a <see cref="Overload:System.IO.StreamReader.Read" /> method.</returns>
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060019B5 RID: 6581 RVA: 0x00055654 File Offset: 0x00053854
		[__DynamicallyInvokable]
		public virtual Encoding CurrentEncoding
		{
			[__DynamicallyInvokable]
			get
			{
				return this.encoding;
			}
		}

		/// <summary>Returns the underlying stream.</summary>
		/// <returns>The underlying stream.</returns>
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060019B6 RID: 6582 RVA: 0x0005565C File Offset: 0x0005385C
		[__DynamicallyInvokable]
		public virtual Stream BaseStream
		{
			[__DynamicallyInvokable]
			get
			{
				return this.stream;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060019B7 RID: 6583 RVA: 0x00055664 File Offset: 0x00053864
		internal bool LeaveOpen
		{
			get
			{
				return !this._closable;
			}
		}

		/// <summary>Clears the internal buffer.</summary>
		// Token: 0x060019B8 RID: 6584 RVA: 0x0005566F File Offset: 0x0005386F
		[__DynamicallyInvokable]
		public void DiscardBufferedData()
		{
			this.CheckAsyncTaskInProgress();
			this.byteLen = 0;
			this.charLen = 0;
			this.charPos = 0;
			if (this.encoding != null)
			{
				this.decoder = this.encoding.GetDecoder();
			}
			this._isBlocked = false;
		}

		/// <summary>Gets a value that indicates whether the current stream position is at the end of the stream.</summary>
		/// <returns>
		///   <see langword="true" /> if the current stream position is at the end of the stream; otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The underlying stream has been disposed.</exception>
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060019B9 RID: 6585 RVA: 0x000556AC File Offset: 0x000538AC
		[__DynamicallyInvokable]
		public bool EndOfStream
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.stream == null)
				{
					__Error.ReaderClosed();
				}
				this.CheckAsyncTaskInProgress();
				if (this.charPos < this.charLen)
				{
					return false;
				}
				int num = this.ReadBuffer();
				return num == 0;
			}
		}

		/// <summary>Returns the next available character but does not consume it.</summary>
		/// <returns>An integer representing the next character to be read, or -1 if there are no characters to be read or if the stream does not support seeking.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x060019BA RID: 6586 RVA: 0x000556E8 File Offset: 0x000538E8
		[__DynamicallyInvokable]
		public override int Peek()
		{
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			if (this.charPos == this.charLen && (this._isBlocked || this.ReadBuffer() == 0))
			{
				return -1;
			}
			return (int)this.charBuffer[this.charPos];
		}

		/// <summary>Reads the next character from the input stream and advances the character position by one character.</summary>
		/// <returns>The next character from the input stream represented as an <see cref="T:System.Int32" /> object, or -1 if no more characters are available.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x060019BB RID: 6587 RVA: 0x00055738 File Offset: 0x00053938
		[__DynamicallyInvokable]
		public override int Read()
		{
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			if (this.charPos == this.charLen && this.ReadBuffer() == 0)
			{
				return -1;
			}
			int num = (int)this.charBuffer[this.charPos];
			this.charPos++;
			return num;
		}

		/// <summary>Reads a specified maximum of characters from the current stream into a buffer, beginning at the specified index.</summary>
		/// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index" /> and (index + count - 1) replaced by the characters read from the current source.</param>
		/// <param name="index">The index of <paramref name="buffer" /> at which to begin writing.</param>
		/// <param name="count">The maximum number of characters to read.</param>
		/// <returns>The number of characters that have been read, or 0 if at the end of the stream and no data was read. The number will be less than or equal to the <paramref name="count" /> parameter, depending on whether the data is available within the stream.</returns>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as the stream is closed.</exception>
		// Token: 0x060019BC RID: 6588 RVA: 0x00055790 File Offset: 0x00053990
		[__DynamicallyInvokable]
		public override int Read([In] [Out] char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			int num = 0;
			bool flag = false;
			while (count > 0)
			{
				int num2 = this.charLen - this.charPos;
				if (num2 == 0)
				{
					num2 = this.ReadBuffer(buffer, index + num, count, out flag);
				}
				if (num2 == 0)
				{
					break;
				}
				if (num2 > count)
				{
					num2 = count;
				}
				if (!flag)
				{
					Buffer.InternalBlockCopy(this.charBuffer, this.charPos * 2, buffer, (index + num) * 2, num2 * 2);
					this.charPos += num2;
				}
				num += num2;
				count -= num2;
				if (this._isBlocked)
				{
					break;
				}
			}
			return num;
		}

		/// <summary>Reads all characters from the current position to the end of the stream.</summary>
		/// <returns>The rest of the stream as a string, from the current position to the end. If the current position is at the end of the stream, returns an empty string ("").</returns>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x060019BD RID: 6589 RVA: 0x0005587C File Offset: 0x00053A7C
		[__DynamicallyInvokable]
		public override string ReadToEnd()
		{
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			StringBuilder stringBuilder = new StringBuilder(this.charLen - this.charPos);
			do
			{
				stringBuilder.Append(this.charBuffer, this.charPos, this.charLen - this.charPos);
				this.charPos = this.charLen;
				this.ReadBuffer();
			}
			while (this.charLen > 0);
			return stringBuilder.ToString();
		}

		/// <summary>Reads a specified maximum number of characters from the current stream and writes the data to a buffer, beginning at the specified index.</summary>
		/// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index" /> and (index + count - 1) replaced by the characters read from the current source.</param>
		/// <param name="index">The position in <paramref name="buffer" /> at which to begin writing.</param>
		/// <param name="count">The maximum number of characters to read.</param>
		/// <returns>The number of characters that have been read. The number will be less than or equal to <paramref name="count" />, depending on whether all input characters have been read.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.StreamReader" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x060019BE RID: 6590 RVA: 0x000558F4 File Offset: 0x00053AF4
		[__DynamicallyInvokable]
		public override int ReadBlock([In] [Out] char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			return base.ReadBlock(buffer, index, count);
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x00055975 File Offset: 0x00053B75
		private void CompressBuffer(int n)
		{
			Buffer.InternalBlockCopy(this.byteBuffer, n, this.byteBuffer, 0, this.byteLen - n);
			this.byteLen -= n;
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x000559A0 File Offset: 0x00053BA0
		private void DetectEncoding()
		{
			if (this.byteLen < 2)
			{
				return;
			}
			this._detectEncoding = false;
			bool flag = false;
			if (this.byteBuffer[0] == 254 && this.byteBuffer[1] == 255)
			{
				this.encoding = new UnicodeEncoding(true, true);
				this.CompressBuffer(2);
				flag = true;
			}
			else if (this.byteBuffer[0] == 255 && this.byteBuffer[1] == 254)
			{
				if (this.byteLen < 4 || this.byteBuffer[2] != 0 || this.byteBuffer[3] != 0)
				{
					this.encoding = new UnicodeEncoding(false, true);
					this.CompressBuffer(2);
					flag = true;
				}
				else
				{
					this.encoding = new UTF32Encoding(false, true);
					this.CompressBuffer(4);
					flag = true;
				}
			}
			else if (this.byteLen >= 3 && this.byteBuffer[0] == 239 && this.byteBuffer[1] == 187 && this.byteBuffer[2] == 191)
			{
				this.encoding = Encoding.UTF8;
				this.CompressBuffer(3);
				flag = true;
			}
			else if (this.byteLen >= 4 && this.byteBuffer[0] == 0 && this.byteBuffer[1] == 0 && this.byteBuffer[2] == 254 && this.byteBuffer[3] == 255)
			{
				this.encoding = new UTF32Encoding(true, true);
				this.CompressBuffer(4);
				flag = true;
			}
			else if (this.byteLen == 2)
			{
				this._detectEncoding = true;
			}
			if (flag)
			{
				this.decoder = this.encoding.GetDecoder();
				this._maxCharsPerBuffer = this.encoding.GetMaxCharCount(this.byteBuffer.Length);
				this.charBuffer = new char[this._maxCharsPerBuffer];
			}
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x00055B58 File Offset: 0x00053D58
		private bool IsPreamble()
		{
			if (!this._checkPreamble)
			{
				return this._checkPreamble;
			}
			int num = ((this.byteLen >= this._preamble.Length) ? (this._preamble.Length - this.bytePos) : (this.byteLen - this.bytePos));
			int i = 0;
			while (i < num)
			{
				if (this.byteBuffer[this.bytePos] != this._preamble[this.bytePos])
				{
					this.bytePos = 0;
					this._checkPreamble = false;
					break;
				}
				i++;
				this.bytePos++;
			}
			if (this._checkPreamble && this.bytePos == this._preamble.Length)
			{
				this.CompressBuffer(this._preamble.Length);
				this.bytePos = 0;
				this._checkPreamble = false;
				this._detectEncoding = false;
			}
			return this._checkPreamble;
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x00055C2C File Offset: 0x00053E2C
		internal virtual int ReadBuffer()
		{
			this.charLen = 0;
			this.charPos = 0;
			if (!this._checkPreamble)
			{
				this.byteLen = 0;
			}
			for (;;)
			{
				if (this._checkPreamble)
				{
					int num = this.stream.Read(this.byteBuffer, this.bytePos, this.byteBuffer.Length - this.bytePos);
					if (num == 0)
					{
						break;
					}
					this.byteLen += num;
				}
				else
				{
					this.byteLen = this.stream.Read(this.byteBuffer, 0, this.byteBuffer.Length);
					if (this.byteLen == 0)
					{
						goto Block_5;
					}
				}
				this._isBlocked = this.byteLen < this.byteBuffer.Length;
				if (!this.IsPreamble())
				{
					if (this._detectEncoding && this.byteLen >= 2)
					{
						this.DetectEncoding();
					}
					this.charLen += this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, this.charBuffer, this.charLen);
				}
				if (this.charLen != 0)
				{
					goto Block_9;
				}
			}
			if (this.byteLen > 0)
			{
				this.charLen += this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, this.charBuffer, this.charLen);
				this.bytePos = (this.byteLen = 0);
			}
			return this.charLen;
			Block_5:
			return this.charLen;
			Block_9:
			return this.charLen;
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x00055D94 File Offset: 0x00053F94
		private int ReadBuffer(char[] userBuffer, int userOffset, int desiredChars, out bool readToUserBuffer)
		{
			this.charLen = 0;
			this.charPos = 0;
			if (!this._checkPreamble)
			{
				this.byteLen = 0;
			}
			int num = 0;
			readToUserBuffer = desiredChars >= this._maxCharsPerBuffer;
			for (;;)
			{
				if (this._checkPreamble)
				{
					int num2 = this.stream.Read(this.byteBuffer, this.bytePos, this.byteBuffer.Length - this.bytePos);
					if (num2 == 0)
					{
						break;
					}
					this.byteLen += num2;
				}
				else
				{
					this.byteLen = this.stream.Read(this.byteBuffer, 0, this.byteBuffer.Length);
					if (this.byteLen == 0)
					{
						goto IL_1B1;
					}
				}
				this._isBlocked = this.byteLen < this.byteBuffer.Length;
				if (!this.IsPreamble())
				{
					if (this._detectEncoding && this.byteLen >= 2)
					{
						this.DetectEncoding();
						readToUserBuffer = desiredChars >= this._maxCharsPerBuffer;
					}
					this.charPos = 0;
					if (readToUserBuffer)
					{
						num += this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, userBuffer, userOffset + num);
						this.charLen = 0;
					}
					else
					{
						num = this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, this.charBuffer, num);
						this.charLen += num;
					}
				}
				if (num != 0)
				{
					goto IL_1B1;
				}
			}
			if (this.byteLen > 0)
			{
				if (readToUserBuffer)
				{
					num = this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, userBuffer, userOffset + num);
					this.charLen = 0;
				}
				else
				{
					num = this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, this.charBuffer, num);
					this.charLen += num;
				}
			}
			return num;
			IL_1B1:
			this._isBlocked &= num < desiredChars;
			return num;
		}

		/// <summary>Reads a line of characters from the current stream and returns the data as a string.</summary>
		/// <returns>The next line from the input stream, or <see langword="null" /> if the end of the input stream is reached.</returns>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x060019C4 RID: 6596 RVA: 0x00055F64 File Offset: 0x00054164
		[__DynamicallyInvokable]
		public override string ReadLine()
		{
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			if (this.charPos == this.charLen && this.ReadBuffer() == 0)
			{
				return null;
			}
			StringBuilder stringBuilder = null;
			int num;
			char c;
			for (;;)
			{
				num = this.charPos;
				do
				{
					c = this.charBuffer[num];
					if (c == '\r' || c == '\n')
					{
						goto IL_4A;
					}
					num++;
				}
				while (num < this.charLen);
				num = this.charLen - this.charPos;
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(num + 80);
				}
				stringBuilder.Append(this.charBuffer, this.charPos, num);
				if (this.ReadBuffer() <= 0)
				{
					goto Block_11;
				}
			}
			IL_4A:
			string text;
			if (stringBuilder != null)
			{
				stringBuilder.Append(this.charBuffer, this.charPos, num - this.charPos);
				text = stringBuilder.ToString();
			}
			else
			{
				text = new string(this.charBuffer, this.charPos, num - this.charPos);
			}
			this.charPos = num + 1;
			if (c == '\r' && (this.charPos < this.charLen || this.ReadBuffer() > 0) && this.charBuffer[this.charPos] == '\n')
			{
				this.charPos++;
			}
			return text;
			Block_11:
			return stringBuilder.ToString();
		}

		/// <summary>Reads a line of characters asynchronously from the current stream and returns the data as a string.</summary>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the next line from the stream, or is <see langword="null" /> if all the characters have been read.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of characters in the next line is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The reader is currently in use by a previous read operation.</exception>
		// Token: 0x060019C5 RID: 6597 RVA: 0x00056094 File Offset: 0x00054294
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task<string> ReadLineAsync()
		{
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadLineAsync();
			}
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			Task<string> task = this.ReadLineAsyncInternal();
			this._asyncReadTask = task;
			return task;
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x000560E4 File Offset: 0x000542E4
		private async Task<string> ReadLineAsyncInternal()
		{
			bool flag = this.CharPos_Prop == this.CharLen_Prop;
			bool flag2 = flag;
			if (flag2)
			{
				int num = await this.ReadBufferAsync().ConfigureAwait(false);
				flag2 = num == 0;
			}
			string text;
			if (flag2)
			{
				text = null;
			}
			else
			{
				StringBuilder sb = null;
				char[] charBuffer_Prop;
				int charLen_Prop;
				int num3;
				int num2;
				char c;
				for (;;)
				{
					charBuffer_Prop = this.CharBuffer_Prop;
					charLen_Prop = this.CharLen_Prop;
					num2 = (num3 = this.CharPos_Prop);
					do
					{
						c = charBuffer_Prop[num3];
						if (c == '\r' || c == '\n')
						{
							goto IL_EB;
						}
						num3++;
					}
					while (num3 < charLen_Prop);
					num3 = charLen_Prop - num2;
					if (sb == null)
					{
						sb = new StringBuilder(num3 + 80);
					}
					sb.Append(charBuffer_Prop, num2, num3);
					if (await this.ReadBufferAsync().ConfigureAwait(false) <= 0)
					{
						goto Block_11;
					}
				}
				IL_EB:
				string s;
				if (sb != null)
				{
					sb.Append(charBuffer_Prop, num2, num3 - num2);
					s = sb.ToString();
				}
				else
				{
					s = new string(charBuffer_Prop, num2, num3 - num2);
				}
				int num4 = num3 + 1;
				this.CharPos_Prop = num4;
				bool flag3 = c == '\r';
				if (flag3)
				{
					bool flag4 = num4 < charLen_Prop;
					if (!flag4)
					{
						flag4 = await this.ReadBufferAsync().ConfigureAwait(false) > 0;
					}
					flag3 = flag4;
				}
				if (flag3)
				{
					num2 = this.CharPos_Prop;
					if (this.CharBuffer_Prop[num2] == '\n')
					{
						this.CharPos_Prop = num2 + 1;
					}
				}
				return s;
				Block_11:
				text = sb.ToString();
			}
			return text;
		}

		/// <summary>Reads all characters from the current position to the end of the stream asynchronously and returns them as one string.</summary>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains a string with the characters from the current position to the end of the stream.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of characters is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The reader is currently in use by a previous read operation.</exception>
		// Token: 0x060019C7 RID: 6599 RVA: 0x00056128 File Offset: 0x00054328
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task<string> ReadToEndAsync()
		{
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadToEndAsync();
			}
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			Task<string> task = this.ReadToEndAsyncInternal();
			this._asyncReadTask = task;
			return task;
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x00056178 File Offset: 0x00054378
		private async Task<string> ReadToEndAsyncInternal()
		{
			StringBuilder sb = new StringBuilder(this.CharLen_Prop - this.CharPos_Prop);
			do
			{
				int charPos_Prop = this.CharPos_Prop;
				sb.Append(this.CharBuffer_Prop, charPos_Prop, this.CharLen_Prop - charPos_Prop);
				this.CharPos_Prop = this.CharLen_Prop;
				await this.ReadBufferAsync().ConfigureAwait(false);
			}
			while (this.CharLen_Prop > 0);
			return sb.ToString();
		}

		/// <summary>Reads a specified maximum number of characters from the current stream asynchronously and writes the data to a buffer, beginning at the specified index.</summary>
		/// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index" /> and (<paramref name="index" /> + <paramref name="count" /> - 1) replaced by the characters read from the current source.</param>
		/// <param name="index">The position in <paramref name="buffer" /> at which to begin writing.</param>
		/// <param name="count">The maximum number of characters to read. If the end of the stream is reached before the specified number of characters is written into the buffer, the current method returns.</param>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the total number of characters read into the buffer. The result value can be less than the number of characters requested if the number of characters currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="index" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The reader is currently in use by a previous read operation.</exception>
		// Token: 0x060019C9 RID: 6601 RVA: 0x000561BC File Offset: 0x000543BC
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task<int> ReadAsync(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadAsync(buffer, index, count);
			}
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			Task<int> task = this.ReadAsyncInternal(buffer, index, count);
			this._asyncReadTask = task;
			return task;
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x0005626C File Offset: 0x0005446C
		internal override async Task<int> ReadAsyncInternal(char[] buffer, int index, int count)
		{
			bool flag = this.CharPos_Prop == this.CharLen_Prop;
			bool flag2 = flag;
			if (flag2)
			{
				int num = await this.ReadBufferAsync().ConfigureAwait(false);
				flag2 = num == 0;
			}
			int num2;
			if (flag2)
			{
				num2 = 0;
			}
			else
			{
				int charsRead = 0;
				bool readToUserBuffer = false;
				byte[] tmpByteBuffer = this.ByteBuffer_Prop;
				Stream tmpStream = this.Stream_Prop;
				while (count > 0)
				{
					int i = this.CharLen_Prop - this.CharPos_Prop;
					if (i == 0)
					{
						this.CharLen_Prop = 0;
						this.CharPos_Prop = 0;
						if (!this.CheckPreamble_Prop)
						{
							this.ByteLen_Prop = 0;
						}
						readToUserBuffer = count >= this.MaxCharsPerBuffer_Prop;
						do
						{
							if (this.CheckPreamble_Prop)
							{
								int bytePos_Prop = this.BytePos_Prop;
								int num3 = await tmpStream.ReadAsync(tmpByteBuffer, bytePos_Prop, tmpByteBuffer.Length - bytePos_Prop).ConfigureAwait(false);
								if (num3 == 0)
								{
									goto Block_6;
								}
								this.ByteLen_Prop += num3;
							}
							else
							{
								this.ByteLen_Prop = await tmpStream.ReadAsync(tmpByteBuffer, 0, tmpByteBuffer.Length).ConfigureAwait(false);
								if (this.ByteLen_Prop == 0)
								{
									goto Block_9;
								}
							}
							this.IsBlocked_Prop = this.ByteLen_Prop < tmpByteBuffer.Length;
							if (!this.IsPreamble())
							{
								if (this.DetectEncoding_Prop && this.ByteLen_Prop >= 2)
								{
									this.DetectEncoding();
									readToUserBuffer = count >= this.MaxCharsPerBuffer_Prop;
								}
								this.CharPos_Prop = 0;
								if (readToUserBuffer)
								{
									i += this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, buffer, index + charsRead);
									this.CharLen_Prop = 0;
								}
								else
								{
									i = this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, this.CharBuffer_Prop, 0);
									this.CharLen_Prop += i;
								}
							}
						}
						while (i == 0);
						IL_3EE:
						if (i != 0)
						{
							goto IL_3F9;
						}
						break;
						Block_9:
						this.IsBlocked_Prop = true;
						goto IL_3EE;
						Block_6:
						if (this.ByteLen_Prop > 0)
						{
							if (readToUserBuffer)
							{
								i = this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, buffer, index + charsRead);
								this.CharLen_Prop = 0;
							}
							else
							{
								i = this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, this.CharBuffer_Prop, 0);
								this.CharLen_Prop += i;
							}
						}
						this.IsBlocked_Prop = true;
						goto IL_3EE;
					}
					IL_3F9:
					if (i > count)
					{
						i = count;
					}
					if (!readToUserBuffer)
					{
						Buffer.InternalBlockCopy(this.CharBuffer_Prop, this.CharPos_Prop * 2, buffer, (index + charsRead) * 2, i * 2);
						this.CharPos_Prop += i;
					}
					charsRead += i;
					count -= i;
					if (this.IsBlocked_Prop)
					{
						break;
					}
				}
				num2 = charsRead;
			}
			return num2;
		}

		/// <summary>Reads a specified maximum number of characters from the current stream asynchronously and writes the data to a buffer, beginning at the specified index.</summary>
		/// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index" /> and (<paramref name="index" /> + <paramref name="count" /> - 1) replaced by the characters read from the current source.</param>
		/// <param name="index">The position in <paramref name="buffer" /> at which to begin writing.</param>
		/// <param name="count">The maximum number of characters to read. If the end of the stream is reached before the specified number of characters is written into the buffer, the method returns.</param>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the total number of characters read into the buffer. The result value can be less than the number of characters requested if the number of characters currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="index" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The reader is currently in use by a previous read operation.</exception>
		// Token: 0x060019CB RID: 6603 RVA: 0x000562C8 File Offset: 0x000544C8
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task<int> ReadBlockAsync(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadBlockAsync(buffer, index, count);
			}
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			Task<int> task = base.ReadBlockAsync(buffer, index, count);
			this._asyncReadTask = task;
			return task;
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060019CC RID: 6604 RVA: 0x00056375 File Offset: 0x00054575
		// (set) Token: 0x060019CD RID: 6605 RVA: 0x0005637D File Offset: 0x0005457D
		private int CharLen_Prop
		{
			get
			{
				return this.charLen;
			}
			set
			{
				this.charLen = value;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060019CE RID: 6606 RVA: 0x00056386 File Offset: 0x00054586
		// (set) Token: 0x060019CF RID: 6607 RVA: 0x0005638E File Offset: 0x0005458E
		private int CharPos_Prop
		{
			get
			{
				return this.charPos;
			}
			set
			{
				this.charPos = value;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060019D0 RID: 6608 RVA: 0x00056397 File Offset: 0x00054597
		// (set) Token: 0x060019D1 RID: 6609 RVA: 0x0005639F File Offset: 0x0005459F
		private int ByteLen_Prop
		{
			get
			{
				return this.byteLen;
			}
			set
			{
				this.byteLen = value;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060019D2 RID: 6610 RVA: 0x000563A8 File Offset: 0x000545A8
		// (set) Token: 0x060019D3 RID: 6611 RVA: 0x000563B0 File Offset: 0x000545B0
		private int BytePos_Prop
		{
			get
			{
				return this.bytePos;
			}
			set
			{
				this.bytePos = value;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060019D4 RID: 6612 RVA: 0x000563B9 File Offset: 0x000545B9
		private byte[] Preamble_Prop
		{
			get
			{
				return this._preamble;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060019D5 RID: 6613 RVA: 0x000563C1 File Offset: 0x000545C1
		private bool CheckPreamble_Prop
		{
			get
			{
				return this._checkPreamble;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060019D6 RID: 6614 RVA: 0x000563C9 File Offset: 0x000545C9
		private Decoder Decoder_Prop
		{
			get
			{
				return this.decoder;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060019D7 RID: 6615 RVA: 0x000563D1 File Offset: 0x000545D1
		private bool DetectEncoding_Prop
		{
			get
			{
				return this._detectEncoding;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060019D8 RID: 6616 RVA: 0x000563D9 File Offset: 0x000545D9
		private char[] CharBuffer_Prop
		{
			get
			{
				return this.charBuffer;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060019D9 RID: 6617 RVA: 0x000563E1 File Offset: 0x000545E1
		private byte[] ByteBuffer_Prop
		{
			get
			{
				return this.byteBuffer;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060019DA RID: 6618 RVA: 0x000563E9 File Offset: 0x000545E9
		// (set) Token: 0x060019DB RID: 6619 RVA: 0x000563F1 File Offset: 0x000545F1
		private bool IsBlocked_Prop
		{
			get
			{
				return this._isBlocked;
			}
			set
			{
				this._isBlocked = value;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060019DC RID: 6620 RVA: 0x000563FA File Offset: 0x000545FA
		private Stream Stream_Prop
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060019DD RID: 6621 RVA: 0x00056402 File Offset: 0x00054602
		private int MaxCharsPerBuffer_Prop
		{
			get
			{
				return this._maxCharsPerBuffer;
			}
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x0005640C File Offset: 0x0005460C
		private async Task<int> ReadBufferAsync()
		{
			this.CharLen_Prop = 0;
			this.CharPos_Prop = 0;
			byte[] tmpByteBuffer = this.ByteBuffer_Prop;
			Stream tmpStream = this.Stream_Prop;
			if (!this.CheckPreamble_Prop)
			{
				this.ByteLen_Prop = 0;
			}
			for (;;)
			{
				if (this.CheckPreamble_Prop)
				{
					int bytePos_Prop = this.BytePos_Prop;
					int num = await tmpStream.ReadAsync(tmpByteBuffer, bytePos_Prop, tmpByteBuffer.Length - bytePos_Prop).ConfigureAwait(false);
					int num2 = num;
					if (num2 == 0)
					{
						break;
					}
					this.ByteLen_Prop += num2;
				}
				else
				{
					this.ByteLen_Prop = await tmpStream.ReadAsync(tmpByteBuffer, 0, tmpByteBuffer.Length).ConfigureAwait(false);
					if (this.ByteLen_Prop == 0)
					{
						goto Block_5;
					}
				}
				this.IsBlocked_Prop = this.ByteLen_Prop < tmpByteBuffer.Length;
				if (!this.IsPreamble())
				{
					if (this.DetectEncoding_Prop && this.ByteLen_Prop >= 2)
					{
						this.DetectEncoding();
					}
					this.CharLen_Prop += this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, this.CharBuffer_Prop, this.CharLen_Prop);
				}
				if (this.CharLen_Prop != 0)
				{
					goto Block_9;
				}
			}
			if (this.ByteLen_Prop > 0)
			{
				this.CharLen_Prop += this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, this.CharBuffer_Prop, this.CharLen_Prop);
				this.BytePos_Prop = 0;
				this.ByteLen_Prop = 0;
			}
			return this.CharLen_Prop;
			Block_5:
			return this.CharLen_Prop;
			Block_9:
			return this.CharLen_Prop;
		}

		/// <summary>A <see cref="T:System.IO.StreamReader" /> object around an empty stream.</summary>
		// Token: 0x040008F7 RID: 2295
		[__DynamicallyInvokable]
		public new static readonly StreamReader Null = new StreamReader.NullStreamReader();

		// Token: 0x040008F8 RID: 2296
		private const int DefaultFileStreamBufferSize = 4096;

		// Token: 0x040008F9 RID: 2297
		private const int MinBufferSize = 128;

		// Token: 0x040008FA RID: 2298
		private Stream stream;

		// Token: 0x040008FB RID: 2299
		private Encoding encoding;

		// Token: 0x040008FC RID: 2300
		private Decoder decoder;

		// Token: 0x040008FD RID: 2301
		private byte[] byteBuffer;

		// Token: 0x040008FE RID: 2302
		private char[] charBuffer;

		// Token: 0x040008FF RID: 2303
		private byte[] _preamble;

		// Token: 0x04000900 RID: 2304
		private int charPos;

		// Token: 0x04000901 RID: 2305
		private int charLen;

		// Token: 0x04000902 RID: 2306
		private int byteLen;

		// Token: 0x04000903 RID: 2307
		private int bytePos;

		// Token: 0x04000904 RID: 2308
		private int _maxCharsPerBuffer;

		// Token: 0x04000905 RID: 2309
		private bool _detectEncoding;

		// Token: 0x04000906 RID: 2310
		private bool _checkPreamble;

		// Token: 0x04000907 RID: 2311
		private bool _isBlocked;

		// Token: 0x04000908 RID: 2312
		private bool _closable;

		// Token: 0x04000909 RID: 2313
		[NonSerialized]
		private volatile Task _asyncReadTask;

		// Token: 0x02000B17 RID: 2839
		private class NullStreamReader : StreamReader
		{
			// Token: 0x06006AFE RID: 27390 RVA: 0x00172D1A File Offset: 0x00170F1A
			internal NullStreamReader()
			{
				base.Init(Stream.Null);
			}

			// Token: 0x17001218 RID: 4632
			// (get) Token: 0x06006AFF RID: 27391 RVA: 0x00172D2D File Offset: 0x00170F2D
			public override Stream BaseStream
			{
				get
				{
					return Stream.Null;
				}
			}

			// Token: 0x17001219 RID: 4633
			// (get) Token: 0x06006B00 RID: 27392 RVA: 0x00172D34 File Offset: 0x00170F34
			public override Encoding CurrentEncoding
			{
				get
				{
					return Encoding.Unicode;
				}
			}

			// Token: 0x06006B01 RID: 27393 RVA: 0x00172D3B File Offset: 0x00170F3B
			protected override void Dispose(bool disposing)
			{
			}

			// Token: 0x06006B02 RID: 27394 RVA: 0x00172D3D File Offset: 0x00170F3D
			public override int Peek()
			{
				return -1;
			}

			// Token: 0x06006B03 RID: 27395 RVA: 0x00172D40 File Offset: 0x00170F40
			public override int Read()
			{
				return -1;
			}

			// Token: 0x06006B04 RID: 27396 RVA: 0x00172D43 File Offset: 0x00170F43
			public override int Read(char[] buffer, int index, int count)
			{
				return 0;
			}

			// Token: 0x06006B05 RID: 27397 RVA: 0x00172D46 File Offset: 0x00170F46
			public override string ReadLine()
			{
				return null;
			}

			// Token: 0x06006B06 RID: 27398 RVA: 0x00172D49 File Offset: 0x00170F49
			public override string ReadToEnd()
			{
				return string.Empty;
			}

			// Token: 0x06006B07 RID: 27399 RVA: 0x00172D50 File Offset: 0x00170F50
			internal override int ReadBuffer()
			{
				return 0;
			}
		}
	}
}
