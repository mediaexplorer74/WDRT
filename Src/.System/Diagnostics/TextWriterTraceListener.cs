using System;
using System.IO;
using System.Security.Permissions;
using System.Text;

namespace System.Diagnostics
{
	/// <summary>Directs tracing or debugging output to a <see cref="T:System.IO.TextWriter" /> or to a <see cref="T:System.IO.Stream" />, such as <see cref="T:System.IO.FileStream" />.</summary>
	// Token: 0x020004AB RID: 1195
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class TextWriterTraceListener : TraceListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> class with <see cref="T:System.IO.TextWriter" /> as the output recipient.</summary>
		// Token: 0x06002C36 RID: 11318 RVA: 0x000C75A8 File Offset: 0x000C57A8
		public TextWriterTraceListener()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> class, using the stream as the recipient of the debugging and tracing output.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that represents the stream the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> writes to.</param>
		/// <exception cref="T:System.ArgumentNullException">The stream is <see langword="null" />.</exception>
		// Token: 0x06002C37 RID: 11319 RVA: 0x000C75B0 File Offset: 0x000C57B0
		public TextWriterTraceListener(Stream stream)
			: this(stream, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> class with the specified name, using the stream as the recipient of the debugging and tracing output.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that represents the stream the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> writes to.</param>
		/// <param name="name">The name of the new instance.</param>
		/// <exception cref="T:System.ArgumentNullException">The stream is <see langword="null" />.</exception>
		// Token: 0x06002C38 RID: 11320 RVA: 0x000C75BE File Offset: 0x000C57BE
		public TextWriterTraceListener(Stream stream, string name)
			: base(name)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.writer = new StreamWriter(stream);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> class using the specified writer as recipient of the tracing or debugging output.</summary>
		/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> that receives the output from the <see cref="T:System.Diagnostics.TextWriterTraceListener" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The writer is <see langword="null" />.</exception>
		// Token: 0x06002C39 RID: 11321 RVA: 0x000C75E1 File Offset: 0x000C57E1
		public TextWriterTraceListener(TextWriter writer)
			: this(writer, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> class with the specified name, using the specified writer as recipient of the tracing or debugging output.</summary>
		/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> that receives the output from the <see cref="T:System.Diagnostics.TextWriterTraceListener" />.</param>
		/// <param name="name">The name of the new instance.</param>
		/// <exception cref="T:System.ArgumentNullException">The writer is <see langword="null" />.</exception>
		// Token: 0x06002C3A RID: 11322 RVA: 0x000C75EF File Offset: 0x000C57EF
		public TextWriterTraceListener(TextWriter writer, string name)
			: base(name)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.writer = writer;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> class, using the file as the recipient of the debugging and tracing output.</summary>
		/// <param name="fileName">The name of the file the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> writes to.</param>
		/// <exception cref="T:System.ArgumentNullException">The file is <see langword="null" />.</exception>
		// Token: 0x06002C3B RID: 11323 RVA: 0x000C760D File Offset: 0x000C580D
		public TextWriterTraceListener(string fileName)
		{
			this.fileName = fileName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> class with the specified name, using the file as the recipient of the debugging and tracing output.</summary>
		/// <param name="fileName">The name of the file the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> writes to.</param>
		/// <param name="name">The name of the new instance.</param>
		/// <exception cref="T:System.ArgumentNullException">The stream is <see langword="null" />.</exception>
		// Token: 0x06002C3C RID: 11324 RVA: 0x000C761C File Offset: 0x000C581C
		public TextWriterTraceListener(string fileName, string name)
			: base(name)
		{
			this.fileName = fileName;
		}

		/// <summary>Gets or sets the text writer that receives the tracing or debugging output.</summary>
		/// <returns>A <see cref="T:System.IO.TextWriter" /> that represents the writer that receives the tracing or debugging output.</returns>
		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06002C3D RID: 11325 RVA: 0x000C762C File Offset: 0x000C582C
		// (set) Token: 0x06002C3E RID: 11326 RVA: 0x000C763B File Offset: 0x000C583B
		public TextWriter Writer
		{
			get
			{
				this.EnsureWriter();
				return this.writer;
			}
			set
			{
				this.writer = value;
			}
		}

		/// <summary>Closes the <see cref="P:System.Diagnostics.TextWriterTraceListener.Writer" /> so that it no longer receives tracing or debugging output.</summary>
		// Token: 0x06002C3F RID: 11327 RVA: 0x000C7644 File Offset: 0x000C5844
		public override void Close()
		{
			if (this.writer != null)
			{
				try
				{
					this.writer.Close();
				}
				catch (ObjectDisposedException)
				{
				}
			}
			this.writer = null;
		}

		/// <summary>Disposes this <see cref="T:System.Diagnostics.TextWriterTraceListener" /> object.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release managed resources; if <see langword="false" />, <see cref="M:System.Diagnostics.TextWriterTraceListener.Dispose(System.Boolean)" /> has no effect.</param>
		// Token: 0x06002C40 RID: 11328 RVA: 0x000C7680 File Offset: 0x000C5880
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.Close();
				}
				else
				{
					if (this.writer != null)
					{
						try
						{
							this.writer.Close();
						}
						catch (ObjectDisposedException)
						{
						}
					}
					this.writer = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>Flushes the output buffer for the <see cref="P:System.Diagnostics.TextWriterTraceListener.Writer" />.</summary>
		// Token: 0x06002C41 RID: 11329 RVA: 0x000C76E0 File Offset: 0x000C58E0
		public override void Flush()
		{
			if (!this.EnsureWriter())
			{
				return;
			}
			try
			{
				this.writer.Flush();
			}
			catch (ObjectDisposedException)
			{
			}
		}

		/// <summary>Writes a message to this instance's <see cref="P:System.Diagnostics.TextWriterTraceListener.Writer" />.</summary>
		/// <param name="message">A message to write.</param>
		// Token: 0x06002C42 RID: 11330 RVA: 0x000C7718 File Offset: 0x000C5918
		public override void Write(string message)
		{
			if (!this.EnsureWriter())
			{
				return;
			}
			if (base.NeedIndent)
			{
				this.WriteIndent();
			}
			try
			{
				this.writer.Write(message);
			}
			catch (ObjectDisposedException)
			{
			}
		}

		/// <summary>Writes a message to this instance's <see cref="P:System.Diagnostics.TextWriterTraceListener.Writer" /> followed by a line terminator. The default line terminator is a carriage return followed by a line feed (\r\n).</summary>
		/// <param name="message">A message to write.</param>
		// Token: 0x06002C43 RID: 11331 RVA: 0x000C7760 File Offset: 0x000C5960
		public override void WriteLine(string message)
		{
			if (!this.EnsureWriter())
			{
				return;
			}
			if (base.NeedIndent)
			{
				this.WriteIndent();
			}
			try
			{
				this.writer.WriteLine(message);
				base.NeedIndent = true;
			}
			catch (ObjectDisposedException)
			{
			}
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x000C77B0 File Offset: 0x000C59B0
		private static Encoding GetEncodingWithFallback(Encoding encoding)
		{
			Encoding encoding2 = (Encoding)encoding.Clone();
			encoding2.EncoderFallback = EncoderFallback.ReplacementFallback;
			encoding2.DecoderFallback = DecoderFallback.ReplacementFallback;
			return encoding2;
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x000C77E0 File Offset: 0x000C59E0
		internal bool EnsureWriter()
		{
			bool flag = true;
			if (this.writer == null)
			{
				flag = false;
				if (this.fileName == null)
				{
					return flag;
				}
				Encoding encodingWithFallback = TextWriterTraceListener.GetEncodingWithFallback(new UTF8Encoding(false));
				string text = Path.GetFullPath(this.fileName);
				string directoryName = Path.GetDirectoryName(text);
				string text2 = Path.GetFileName(text);
				for (int i = 0; i < 2; i++)
				{
					try
					{
						this.writer = new StreamWriter(text, true, encodingWithFallback, 4096);
						flag = true;
						break;
					}
					catch (IOException)
					{
						text2 = Guid.NewGuid().ToString() + text2;
						text = Path.Combine(directoryName, text2);
					}
					catch (UnauthorizedAccessException)
					{
						break;
					}
					catch (Exception)
					{
						break;
					}
				}
				if (!flag)
				{
					this.fileName = null;
				}
			}
			return flag;
		}

		// Token: 0x040026B3 RID: 9907
		internal TextWriter writer;

		// Token: 0x040026B4 RID: 9908
		private string fileName;
	}
}
