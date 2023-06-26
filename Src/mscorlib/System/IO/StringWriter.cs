using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Implements a <see cref="T:System.IO.TextWriter" /> for writing information to a string. The information is stored in an underlying <see cref="T:System.Text.StringBuilder" />.</summary>
	// Token: 0x020001A5 RID: 421
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class StringWriter : TextWriter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StringWriter" /> class.</summary>
		// Token: 0x06001A18 RID: 6680 RVA: 0x000575EB File Offset: 0x000557EB
		[__DynamicallyInvokable]
		public StringWriter()
			: this(new StringBuilder(), CultureInfo.CurrentCulture)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StringWriter" /> class with the specified format control.</summary>
		/// <param name="formatProvider">An <see cref="T:System.IFormatProvider" /> object that controls formatting.</param>
		// Token: 0x06001A19 RID: 6681 RVA: 0x000575FD File Offset: 0x000557FD
		[__DynamicallyInvokable]
		public StringWriter(IFormatProvider formatProvider)
			: this(new StringBuilder(), formatProvider)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StringWriter" /> class that writes to the specified <see cref="T:System.Text.StringBuilder" />.</summary>
		/// <param name="sb">The <see cref="T:System.Text.StringBuilder" /> object to write to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sb" /> is <see langword="null" />.</exception>
		// Token: 0x06001A1A RID: 6682 RVA: 0x0005760B File Offset: 0x0005580B
		[__DynamicallyInvokable]
		public StringWriter(StringBuilder sb)
			: this(sb, CultureInfo.CurrentCulture)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StringWriter" /> class that writes to the specified <see cref="T:System.Text.StringBuilder" /> and has the specified format provider.</summary>
		/// <param name="sb">The <see cref="T:System.Text.StringBuilder" /> object to write to.</param>
		/// <param name="formatProvider">An <see cref="T:System.IFormatProvider" /> object that controls formatting.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sb" /> is <see langword="null" />.</exception>
		// Token: 0x06001A1B RID: 6683 RVA: 0x00057619 File Offset: 0x00055819
		[__DynamicallyInvokable]
		public StringWriter(StringBuilder sb, IFormatProvider formatProvider)
			: base(formatProvider)
		{
			if (sb == null)
			{
				throw new ArgumentNullException("sb", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			this._sb = sb;
			this._isOpen = true;
		}

		/// <summary>Closes the current <see cref="T:System.IO.StringWriter" /> and the underlying stream.</summary>
		// Token: 0x06001A1C RID: 6684 RVA: 0x00057648 File Offset: 0x00055848
		public override void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.StringWriter" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06001A1D RID: 6685 RVA: 0x00057651 File Offset: 0x00055851
		[__DynamicallyInvokable]
		protected override void Dispose(bool disposing)
		{
			this._isOpen = false;
			base.Dispose(disposing);
		}

		/// <summary>Gets the <see cref="T:System.Text.Encoding" /> in which the output is written.</summary>
		/// <returns>The <see langword="Encoding" /> in which the output is written.</returns>
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06001A1E RID: 6686 RVA: 0x00057661 File Offset: 0x00055861
		[__DynamicallyInvokable]
		public override Encoding Encoding
		{
			[__DynamicallyInvokable]
			get
			{
				if (StringWriter.m_encoding == null)
				{
					StringWriter.m_encoding = new UnicodeEncoding(false, false);
				}
				return StringWriter.m_encoding;
			}
		}

		/// <summary>Returns the underlying <see cref="T:System.Text.StringBuilder" />.</summary>
		/// <returns>The underlying <see langword="StringBuilder" />.</returns>
		// Token: 0x06001A1F RID: 6687 RVA: 0x00057681 File Offset: 0x00055881
		[__DynamicallyInvokable]
		public virtual StringBuilder GetStringBuilder()
		{
			return this._sb;
		}

		/// <summary>Writes a character to the string.</summary>
		/// <param name="value">The character to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The writer is closed.</exception>
		// Token: 0x06001A20 RID: 6688 RVA: 0x00057689 File Offset: 0x00055889
		[__DynamicallyInvokable]
		public override void Write(char value)
		{
			if (!this._isOpen)
			{
				__Error.WriterClosed();
			}
			this._sb.Append(value);
		}

		/// <summary>Writes a subarray of characters to the string.</summary>
		/// <param name="buffer">The character array to write data from.</param>
		/// <param name="index">The position in the buffer at which to start reading data.</param>
		/// <param name="count">The maximum number of characters to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">(<paramref name="index" /> + <paramref name="count" />)&gt; <paramref name="buffer" />. <see langword="Length" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The writer is closed.</exception>
		// Token: 0x06001A21 RID: 6689 RVA: 0x000576A8 File Offset: 0x000558A8
		[__DynamicallyInvokable]
		public override void Write(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (!this._isOpen)
			{
				__Error.WriterClosed();
			}
			this._sb.Append(buffer, index, count);
		}

		/// <summary>Writes a string to the current string.</summary>
		/// <param name="value">The string to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The writer is closed.</exception>
		// Token: 0x06001A22 RID: 6690 RVA: 0x00057733 File Offset: 0x00055933
		[__DynamicallyInvokable]
		public override void Write(string value)
		{
			if (!this._isOpen)
			{
				__Error.WriterClosed();
			}
			if (value != null)
			{
				this._sb.Append(value);
			}
		}

		/// <summary>Writes a character to the string asynchronously.</summary>
		/// <param name="value">The character to write to the string.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The string writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The string writer is currently in use by a previous write operation.</exception>
		// Token: 0x06001A23 RID: 6691 RVA: 0x00057752 File Offset: 0x00055952
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteAsync(char value)
		{
			this.Write(value);
			return Task.CompletedTask;
		}

		/// <summary>Writes a string to the current string asynchronously.</summary>
		/// <param name="value">The string to write. If <paramref name="value" /> is <see langword="null" />, nothing is written to the text stream.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The string writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The string writer is currently in use by a previous write operation.</exception>
		// Token: 0x06001A24 RID: 6692 RVA: 0x00057760 File Offset: 0x00055960
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteAsync(string value)
		{
			this.Write(value);
			return Task.CompletedTask;
		}

		/// <summary>Writes a subarray of characters to the string asynchronously.</summary>
		/// <param name="buffer">The character array to write data from.</param>
		/// <param name="index">The position in the buffer at which to start reading data.</param>
		/// <param name="count">The maximum number of characters to write.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> plus <paramref name="count" /> is greater than the buffer length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The string writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The string writer is currently in use by a previous write operation.</exception>
		// Token: 0x06001A25 RID: 6693 RVA: 0x0005776E File Offset: 0x0005596E
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteAsync(char[] buffer, int index, int count)
		{
			this.Write(buffer, index, count);
			return Task.CompletedTask;
		}

		/// <summary>Writes a character followed by a line terminator asynchronously to the string.</summary>
		/// <param name="value">The character to write to the string.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The string writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The string writer is currently in use by a previous write operation.</exception>
		// Token: 0x06001A26 RID: 6694 RVA: 0x0005777E File Offset: 0x0005597E
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteLineAsync(char value)
		{
			this.WriteLine(value);
			return Task.CompletedTask;
		}

		/// <summary>Writes a string followed by a line terminator asynchronously to the current string.</summary>
		/// <param name="value">The string to write. If the value is <see langword="null" />, only a line terminator is written.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The string writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The string writer is currently in use by a previous write operation.</exception>
		// Token: 0x06001A27 RID: 6695 RVA: 0x0005778C File Offset: 0x0005598C
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteLineAsync(string value)
		{
			this.WriteLine(value);
			return Task.CompletedTask;
		}

		/// <summary>Writes a subarray of characters followed by a line terminator asynchronously to the string.</summary>
		/// <param name="buffer">The character array to write data from.</param>
		/// <param name="index">The position in the buffer at which to start reading data.</param>
		/// <param name="count">The maximum number of characters to write.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> plus <paramref name="count" /> is greater than the buffer length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The string writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The string writer is currently in use by a previous write operation.</exception>
		// Token: 0x06001A28 RID: 6696 RVA: 0x0005779A File Offset: 0x0005599A
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteLineAsync(char[] buffer, int index, int count)
		{
			this.WriteLine(buffer, index, count);
			return Task.CompletedTask;
		}

		/// <summary>Asynchronously clears all buffers for the current writer and causes any buffered data to be written to the underlying device.</summary>
		/// <returns>A task that represents the asynchronous flush operation.</returns>
		// Token: 0x06001A29 RID: 6697 RVA: 0x000577AA File Offset: 0x000559AA
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task FlushAsync()
		{
			return Task.CompletedTask;
		}

		/// <summary>Returns a string containing the characters written to the current <see langword="StringWriter" /> so far.</summary>
		/// <returns>The string containing the characters written to the current <see langword="StringWriter" />.</returns>
		// Token: 0x06001A2A RID: 6698 RVA: 0x000577B1 File Offset: 0x000559B1
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this._sb.ToString();
		}

		// Token: 0x0400091F RID: 2335
		private static volatile UnicodeEncoding m_encoding;

		// Token: 0x04000920 RID: 2336
		private StringBuilder _sb;

		// Token: 0x04000921 RID: 2337
		private bool _isOpen;
	}
}
