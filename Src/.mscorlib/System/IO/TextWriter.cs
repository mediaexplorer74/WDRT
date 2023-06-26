using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Represents a writer that can write a sequential series of characters. This class is abstract.</summary>
	// Token: 0x020001A7 RID: 423
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class TextWriter : MarshalByRefObject, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.TextWriter" /> class.</summary>
		// Token: 0x06001A3D RID: 6717 RVA: 0x00057B71 File Offset: 0x00055D71
		[__DynamicallyInvokable]
		protected TextWriter()
		{
			this.InternalFormatProvider = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.TextWriter" /> class with the specified format provider.</summary>
		/// <param name="formatProvider">An <see cref="T:System.IFormatProvider" /> object that controls formatting.</param>
		// Token: 0x06001A3E RID: 6718 RVA: 0x00057B96 File Offset: 0x00055D96
		[__DynamicallyInvokable]
		protected TextWriter(IFormatProvider formatProvider)
		{
			this.InternalFormatProvider = formatProvider;
		}

		/// <summary>Gets an object that controls formatting.</summary>
		/// <returns>An <see cref="T:System.IFormatProvider" /> object for a specific culture, or the formatting of the current culture if no other culture is specified.</returns>
		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06001A3F RID: 6719 RVA: 0x00057BBB File Offset: 0x00055DBB
		[__DynamicallyInvokable]
		public virtual IFormatProvider FormatProvider
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.InternalFormatProvider == null)
				{
					return Thread.CurrentThread.CurrentCulture;
				}
				return this.InternalFormatProvider;
			}
		}

		/// <summary>Closes the current writer and releases any system resources associated with the writer.</summary>
		// Token: 0x06001A40 RID: 6720 RVA: 0x00057BD6 File Offset: 0x00055DD6
		public virtual void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.TextWriter" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06001A41 RID: 6721 RVA: 0x00057BE5 File Offset: 0x00055DE5
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Releases all resources used by the <see cref="T:System.IO.TextWriter" /> object.</summary>
		// Token: 0x06001A42 RID: 6722 RVA: 0x00057BE7 File Offset: 0x00055DE7
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Clears all buffers for the current writer and causes any buffered data to be written to the underlying device.</summary>
		// Token: 0x06001A43 RID: 6723 RVA: 0x00057BF6 File Offset: 0x00055DF6
		[__DynamicallyInvokable]
		public virtual void Flush()
		{
		}

		/// <summary>When overridden in a derived class, returns the character encoding in which the output is written.</summary>
		/// <returns>The character encoding in which the output is written.</returns>
		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06001A44 RID: 6724
		[__DynamicallyInvokable]
		public abstract Encoding Encoding
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets or sets the line terminator string used by the current <see langword="TextWriter" />.</summary>
		/// <returns>The line terminator string for the current <see langword="TextWriter" />.</returns>
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06001A45 RID: 6725 RVA: 0x00057BF8 File Offset: 0x00055DF8
		// (set) Token: 0x06001A46 RID: 6726 RVA: 0x00057C05 File Offset: 0x00055E05
		[__DynamicallyInvokable]
		public virtual string NewLine
		{
			[__DynamicallyInvokable]
			get
			{
				return new string(this.CoreNewLine);
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					value = "\r\n";
				}
				this.CoreNewLine = value.ToCharArray();
			}
		}

		/// <summary>Creates a thread-safe wrapper around the specified <see langword="TextWriter" />.</summary>
		/// <param name="writer">The <see langword="TextWriter" /> to synchronize.</param>
		/// <returns>A thread-safe wrapper.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="writer" /> is <see langword="null" />.</exception>
		// Token: 0x06001A47 RID: 6727 RVA: 0x00057C1D File Offset: 0x00055E1D
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static TextWriter Synchronized(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (writer is TextWriter.SyncTextWriter)
			{
				return writer;
			}
			return new TextWriter.SyncTextWriter(writer);
		}

		/// <summary>Writes a character to the text string or stream.</summary>
		/// <param name="value">The character to write to the text stream.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06001A48 RID: 6728 RVA: 0x00057C3D File Offset: 0x00055E3D
		[__DynamicallyInvokable]
		public virtual void Write(char value)
		{
		}

		/// <summary>Writes a character array to the text string or stream.</summary>
		/// <param name="buffer">The character array to write to the text stream.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06001A49 RID: 6729 RVA: 0x00057C3F File Offset: 0x00055E3F
		[__DynamicallyInvokable]
		public virtual void Write(char[] buffer)
		{
			if (buffer != null)
			{
				this.Write(buffer, 0, buffer.Length);
			}
		}

		/// <summary>Writes a subarray of characters to the text string or stream.</summary>
		/// <param name="buffer">The character array to write data from.</param>
		/// <param name="index">The character position in the buffer at which to start retrieving data.</param>
		/// <param name="count">The number of characters to write.</param>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06001A4A RID: 6730 RVA: 0x00057C50 File Offset: 0x00055E50
		[__DynamicallyInvokable]
		public virtual void Write(char[] buffer, int index, int count)
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
			for (int i = 0; i < count; i++)
			{
				this.Write(buffer[index + i]);
			}
		}

		/// <summary>Writes the text representation of a <see langword="Boolean" /> value to the text string or stream.</summary>
		/// <param name="value">The <see langword="Boolean" /> value to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06001A4B RID: 6731 RVA: 0x00057CD6 File Offset: 0x00055ED6
		[__DynamicallyInvokable]
		public virtual void Write(bool value)
		{
			this.Write(value ? "True" : "False");
		}

		/// <summary>Writes the text representation of a 4-byte signed integer to the text string or stream.</summary>
		/// <param name="value">The 4-byte signed integer to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06001A4C RID: 6732 RVA: 0x00057CED File Offset: 0x00055EED
		[__DynamicallyInvokable]
		public virtual void Write(int value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		/// <summary>Writes the text representation of a 4-byte unsigned integer to the text string or stream.</summary>
		/// <param name="value">The 4-byte unsigned integer to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06001A4D RID: 6733 RVA: 0x00057D02 File Offset: 0x00055F02
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public virtual void Write(uint value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		/// <summary>Writes the text representation of an 8-byte signed integer to the text string or stream.</summary>
		/// <param name="value">The 8-byte signed integer to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06001A4E RID: 6734 RVA: 0x00057D17 File Offset: 0x00055F17
		[__DynamicallyInvokable]
		public virtual void Write(long value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		/// <summary>Writes the text representation of an 8-byte unsigned integer to the text string or stream.</summary>
		/// <param name="value">The 8-byte unsigned integer to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06001A4F RID: 6735 RVA: 0x00057D2C File Offset: 0x00055F2C
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public virtual void Write(ulong value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		/// <summary>Writes the text representation of a 4-byte floating-point value to the text string or stream.</summary>
		/// <param name="value">The 4-byte floating-point value to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06001A50 RID: 6736 RVA: 0x00057D41 File Offset: 0x00055F41
		[__DynamicallyInvokable]
		public virtual void Write(float value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		/// <summary>Writes the text representation of an 8-byte floating-point value to the text string or stream.</summary>
		/// <param name="value">The 8-byte floating-point value to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06001A51 RID: 6737 RVA: 0x00057D56 File Offset: 0x00055F56
		[__DynamicallyInvokable]
		public virtual void Write(double value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		/// <summary>Writes the text representation of a decimal value to the text string or stream.</summary>
		/// <param name="value">The decimal value to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06001A52 RID: 6738 RVA: 0x00057D6B File Offset: 0x00055F6B
		[__DynamicallyInvokable]
		public virtual void Write(decimal value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		/// <summary>Writes a string to the text string or stream.</summary>
		/// <param name="value">The string to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06001A53 RID: 6739 RVA: 0x00057D80 File Offset: 0x00055F80
		[__DynamicallyInvokable]
		public virtual void Write(string value)
		{
			if (value != null)
			{
				this.Write(value.ToCharArray());
			}
		}

		/// <summary>Writes the text representation of an object to the text string or stream by calling the <see langword="ToString" /> method on that object.</summary>
		/// <param name="value">The object to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06001A54 RID: 6740 RVA: 0x00057D94 File Offset: 0x00055F94
		[__DynamicallyInvokable]
		public virtual void Write(object value)
		{
			if (value != null)
			{
				IFormattable formattable = value as IFormattable;
				if (formattable != null)
				{
					this.Write(formattable.ToString(null, this.FormatProvider));
					return;
				}
				this.Write(value.ToString());
			}
		}

		/// <summary>Writes a formatted string to the text string or stream, using the same semantics as the <see cref="M:System.String.Format(System.String,System.Object)" /> method.</summary>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg0">The object to format and write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid composite format string.  
		/// -or-  
		/// The index of a format item is less than 0 (zero), or greater than or equal to the number of objects to be formatted (which, for this method overload, is one).</exception>
		// Token: 0x06001A55 RID: 6741 RVA: 0x00057DCE File Offset: 0x00055FCE
		[__DynamicallyInvokable]
		public virtual void Write(string format, object arg0)
		{
			this.Write(string.Format(this.FormatProvider, format, arg0));
		}

		/// <summary>Writes a formatted string to the text string or stream, using the same semantics as the <see cref="M:System.String.Format(System.String,System.Object,System.Object)" /> method.</summary>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg0">The first object to format and write.</param>
		/// <param name="arg1">The second object to format and write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid composite format string.  
		/// -or-  
		/// The index of a format item is less than 0 (zero) or greater than or equal to the number of objects to be formatted (which, for this method overload, is two).</exception>
		// Token: 0x06001A56 RID: 6742 RVA: 0x00057DE3 File Offset: 0x00055FE3
		[__DynamicallyInvokable]
		public virtual void Write(string format, object arg0, object arg1)
		{
			this.Write(string.Format(this.FormatProvider, format, arg0, arg1));
		}

		/// <summary>Writes a formatted string to the text string or stream, using the same semantics as the <see cref="M:System.String.Format(System.String,System.Object,System.Object,System.Object)" /> method.</summary>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg0">The first object to format and write.</param>
		/// <param name="arg1">The second object to format and write.</param>
		/// <param name="arg2">The third object to format and write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid composite format string.  
		/// -or-  
		/// The index of a format item is less than 0 (zero), or greater than or equal to the number of objects to be formatted (which, for this method overload, is three).</exception>
		// Token: 0x06001A57 RID: 6743 RVA: 0x00057DF9 File Offset: 0x00055FF9
		[__DynamicallyInvokable]
		public virtual void Write(string format, object arg0, object arg1, object arg2)
		{
			this.Write(string.Format(this.FormatProvider, format, arg0, arg1, arg2));
		}

		/// <summary>Writes a formatted string to the text string or stream, using the same semantics as the <see cref="M:System.String.Format(System.String,System.Object[])" /> method.</summary>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg">An object array that contains zero or more objects to format and write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> or <paramref name="arg" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid composite format string.  
		/// -or-  
		/// The index of a format item is less than 0 (zero), or greater than or equal to the length of the <paramref name="arg" /> array.</exception>
		// Token: 0x06001A58 RID: 6744 RVA: 0x00057E11 File Offset: 0x00056011
		[__DynamicallyInvokable]
		public virtual void Write(string format, params object[] arg)
		{
			this.Write(string.Format(this.FormatProvider, format, arg));
		}

		/// <summary>Writes a line terminator to the text string or stream.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06001A59 RID: 6745 RVA: 0x00057E26 File Offset: 0x00056026
		[__DynamicallyInvokable]
		public virtual void WriteLine()
		{
			this.Write(this.CoreNewLine);
		}

		/// <summary>Writes a character followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The character to write to the text stream.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06001A5A RID: 6746 RVA: 0x00057E34 File Offset: 0x00056034
		[__DynamicallyInvokable]
		public virtual void WriteLine(char value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes an array of characters followed by a line terminator to the text string or stream.</summary>
		/// <param name="buffer">The character array from which data is read.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06001A5B RID: 6747 RVA: 0x00057E43 File Offset: 0x00056043
		[__DynamicallyInvokable]
		public virtual void WriteLine(char[] buffer)
		{
			this.Write(buffer);
			this.WriteLine();
		}

		/// <summary>Writes a subarray of characters followed by a line terminator to the text string or stream.</summary>
		/// <param name="buffer">The character array from which data is read.</param>
		/// <param name="index">The character position in <paramref name="buffer" /> at which to start reading data.</param>
		/// <param name="count">The maximum number of characters to write.</param>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06001A5C RID: 6748 RVA: 0x00057E52 File Offset: 0x00056052
		[__DynamicallyInvokable]
		public virtual void WriteLine(char[] buffer, int index, int count)
		{
			this.Write(buffer, index, count);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of a <see langword="Boolean" /> value followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The <see langword="Boolean" /> value to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06001A5D RID: 6749 RVA: 0x00057E63 File Offset: 0x00056063
		[__DynamicallyInvokable]
		public virtual void WriteLine(bool value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of a 4-byte signed integer followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The 4-byte signed integer to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06001A5E RID: 6750 RVA: 0x00057E72 File Offset: 0x00056072
		[__DynamicallyInvokable]
		public virtual void WriteLine(int value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of a 4-byte unsigned integer followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The 4-byte unsigned integer to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06001A5F RID: 6751 RVA: 0x00057E81 File Offset: 0x00056081
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public virtual void WriteLine(uint value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of an 8-byte signed integer followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The 8-byte signed integer to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06001A60 RID: 6752 RVA: 0x00057E90 File Offset: 0x00056090
		[__DynamicallyInvokable]
		public virtual void WriteLine(long value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of an 8-byte unsigned integer followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The 8-byte unsigned integer to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06001A61 RID: 6753 RVA: 0x00057E9F File Offset: 0x0005609F
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public virtual void WriteLine(ulong value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of a 4-byte floating-point value followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The 4-byte floating-point value to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06001A62 RID: 6754 RVA: 0x00057EAE File Offset: 0x000560AE
		[__DynamicallyInvokable]
		public virtual void WriteLine(float value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of a 8-byte floating-point value followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The 8-byte floating-point value to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06001A63 RID: 6755 RVA: 0x00057EBD File Offset: 0x000560BD
		[__DynamicallyInvokable]
		public virtual void WriteLine(double value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of a decimal value followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The decimal value to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06001A64 RID: 6756 RVA: 0x00057ECC File Offset: 0x000560CC
		[__DynamicallyInvokable]
		public virtual void WriteLine(decimal value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes a string followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The string to write. If <paramref name="value" /> is <see langword="null" />, only the line terminator is written.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06001A65 RID: 6757 RVA: 0x00057EDC File Offset: 0x000560DC
		[__DynamicallyInvokable]
		public virtual void WriteLine(string value)
		{
			if (value == null)
			{
				this.WriteLine();
				return;
			}
			int length = value.Length;
			int num = this.CoreNewLine.Length;
			char[] array = new char[length + num];
			value.CopyTo(0, array, 0, length);
			if (num == 2)
			{
				array[length] = this.CoreNewLine[0];
				array[length + 1] = this.CoreNewLine[1];
			}
			else if (num == 1)
			{
				array[length] = this.CoreNewLine[0];
			}
			else
			{
				Buffer.InternalBlockCopy(this.CoreNewLine, 0, array, length * 2, num * 2);
			}
			this.Write(array, 0, length + num);
		}

		/// <summary>Writes the text representation of an object by calling the <see langword="ToString" /> method on that object, followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The object to write. If <paramref name="value" /> is <see langword="null" />, only the line terminator is written.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		// Token: 0x06001A66 RID: 6758 RVA: 0x00057F64 File Offset: 0x00056164
		[__DynamicallyInvokable]
		public virtual void WriteLine(object value)
		{
			if (value == null)
			{
				this.WriteLine();
				return;
			}
			IFormattable formattable = value as IFormattable;
			if (formattable != null)
			{
				this.WriteLine(formattable.ToString(null, this.FormatProvider));
				return;
			}
			this.WriteLine(value.ToString());
		}

		/// <summary>Writes a formatted string and a new line to the text string or stream, using the same semantics as the <see cref="M:System.String.Format(System.String,System.Object)" /> method.</summary>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg0">The object to format and write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid composite format string.  
		/// -or-  
		/// The index of a format item is less than 0 (zero), or greater than or equal to the number of objects to be formatted (which, for this method overload, is one).</exception>
		// Token: 0x06001A67 RID: 6759 RVA: 0x00057FA5 File Offset: 0x000561A5
		[__DynamicallyInvokable]
		public virtual void WriteLine(string format, object arg0)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, arg0));
		}

		/// <summary>Writes a formatted string and a new line to the text string or stream, using the same semantics as the <see cref="M:System.String.Format(System.String,System.Object,System.Object)" /> method.</summary>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg0">The first object to format and write.</param>
		/// <param name="arg1">The second object to format and write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid composite format string.  
		/// -or-  
		/// The index of a format item is less than 0 (zero), or greater than or equal to the number of objects to be formatted (which, for this method overload, is two).</exception>
		// Token: 0x06001A68 RID: 6760 RVA: 0x00057FBA File Offset: 0x000561BA
		[__DynamicallyInvokable]
		public virtual void WriteLine(string format, object arg0, object arg1)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, arg0, arg1));
		}

		/// <summary>Writes out a formatted string and a new line, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)" />.</summary>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg0">The first object to format and write.</param>
		/// <param name="arg1">The second object to format and write.</param>
		/// <param name="arg2">The third object to format and write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid composite format string.  
		/// -or-  
		/// The index of a format item is less than 0 (zero), or greater than or equal to the number of objects to be formatted (which, for this method overload, is three).</exception>
		// Token: 0x06001A69 RID: 6761 RVA: 0x00057FD0 File Offset: 0x000561D0
		[__DynamicallyInvokable]
		public virtual void WriteLine(string format, object arg0, object arg1, object arg2)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, arg0, arg1, arg2));
		}

		/// <summary>Writes out a formatted string and a new line, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)" />.</summary>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg">An object array that contains zero or more objects to format and write.</param>
		/// <exception cref="T:System.ArgumentNullException">A string or object is passed in as <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid composite format string.  
		/// -or-  
		/// The index of a format item is less than 0 (zero), or greater than or equal to the length of the <paramref name="arg" /> array.</exception>
		// Token: 0x06001A6A RID: 6762 RVA: 0x00057FE8 File Offset: 0x000561E8
		[__DynamicallyInvokable]
		public virtual void WriteLine(string format, params object[] arg)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, arg));
		}

		/// <summary>Writes a character to the text string or stream asynchronously.</summary>
		/// <param name="value">The character to write to the text stream.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The text writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The text writer is currently in use by a previous write operation.</exception>
		// Token: 0x06001A6B RID: 6763 RVA: 0x00058000 File Offset: 0x00056200
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteAsync(char value)
		{
			Tuple<TextWriter, char> tuple = new Tuple<TextWriter, char>(this, value);
			return Task.Factory.StartNew(TextWriter._WriteCharDelegate, tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		/// <summary>Writes a string to the text string or stream asynchronously.</summary>
		/// <param name="value">The string to write. If <paramref name="value" /> is <see langword="null" />, nothing is written to the text stream.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The text writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The text writer is currently in use by a previous write operation.</exception>
		// Token: 0x06001A6C RID: 6764 RVA: 0x00058030 File Offset: 0x00056230
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteAsync(string value)
		{
			Tuple<TextWriter, string> tuple = new Tuple<TextWriter, string>(this, value);
			return Task.Factory.StartNew(TextWriter._WriteStringDelegate, tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		/// <summary>Writes a character array to the text string or stream asynchronously.</summary>
		/// <param name="buffer">The character array to write to the text stream. If <paramref name="buffer" /> is <see langword="null" />, nothing is written.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The text writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The text writer is currently in use by a previous write operation.</exception>
		// Token: 0x06001A6D RID: 6765 RVA: 0x00058060 File Offset: 0x00056260
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task WriteAsync(char[] buffer)
		{
			if (buffer == null)
			{
				return Task.CompletedTask;
			}
			return this.WriteAsync(buffer, 0, buffer.Length);
		}

		/// <summary>Writes a subarray of characters to the text string or stream asynchronously.</summary>
		/// <param name="buffer">The character array to write data from.</param>
		/// <param name="index">The character position in the buffer at which to start retrieving data.</param>
		/// <param name="count">The number of characters to write.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> plus <paramref name="count" /> is greater than the buffer length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The text writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The text writer is currently in use by a previous write operation.</exception>
		// Token: 0x06001A6E RID: 6766 RVA: 0x00058078 File Offset: 0x00056278
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteAsync(char[] buffer, int index, int count)
		{
			Tuple<TextWriter, char[], int, int> tuple = new Tuple<TextWriter, char[], int, int>(this, buffer, index, count);
			return Task.Factory.StartNew(TextWriter._WriteCharArrayRangeDelegate, tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		/// <summary>Writes a character followed by a line terminator asynchronously to the text string or stream.</summary>
		/// <param name="value">The character to write to the text stream.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The text writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The text writer is currently in use by a previous write operation.</exception>
		// Token: 0x06001A6F RID: 6767 RVA: 0x000580AC File Offset: 0x000562AC
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteLineAsync(char value)
		{
			Tuple<TextWriter, char> tuple = new Tuple<TextWriter, char>(this, value);
			return Task.Factory.StartNew(TextWriter._WriteLineCharDelegate, tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		/// <summary>Writes a string followed by a line terminator asynchronously to the text string or stream.</summary>
		/// <param name="value">The string to write. If the value is <see langword="null" />, only a line terminator is written.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The text writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The text writer is currently in use by a previous write operation.</exception>
		// Token: 0x06001A70 RID: 6768 RVA: 0x000580DC File Offset: 0x000562DC
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteLineAsync(string value)
		{
			Tuple<TextWriter, string> tuple = new Tuple<TextWriter, string>(this, value);
			return Task.Factory.StartNew(TextWriter._WriteLineStringDelegate, tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		/// <summary>Writes an array of characters followed by a line terminator asynchronously to the text string or stream.</summary>
		/// <param name="buffer">The character array to write to the text stream. If the character array is <see langword="null" />, only the line terminator is written.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The text writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The text writer is currently in use by a previous write operation.</exception>
		// Token: 0x06001A71 RID: 6769 RVA: 0x0005810C File Offset: 0x0005630C
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task WriteLineAsync(char[] buffer)
		{
			if (buffer == null)
			{
				return Task.CompletedTask;
			}
			return this.WriteLineAsync(buffer, 0, buffer.Length);
		}

		/// <summary>Writes a subarray of characters followed by a line terminator asynchronously to the text string or stream.</summary>
		/// <param name="buffer">The character array to write data from.</param>
		/// <param name="index">The character position in the buffer at which to start retrieving data.</param>
		/// <param name="count">The number of characters to write.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> plus <paramref name="count" /> is greater than the buffer length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The text writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The text writer is currently in use by a previous write operation.</exception>
		// Token: 0x06001A72 RID: 6770 RVA: 0x00058124 File Offset: 0x00056324
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteLineAsync(char[] buffer, int index, int count)
		{
			Tuple<TextWriter, char[], int, int> tuple = new Tuple<TextWriter, char[], int, int>(this, buffer, index, count);
			return Task.Factory.StartNew(TextWriter._WriteLineCharArrayRangeDelegate, tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		/// <summary>Writes a line terminator asynchronously to the text string or stream.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The text writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The text writer is currently in use by a previous write operation.</exception>
		// Token: 0x06001A73 RID: 6771 RVA: 0x00058156 File Offset: 0x00056356
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteLineAsync()
		{
			return this.WriteAsync(this.CoreNewLine);
		}

		/// <summary>Asynchronously clears all buffers for the current writer and causes any buffered data to be written to the underlying device.</summary>
		/// <returns>A task that represents the asynchronous flush operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The text writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The writer is currently in use by a previous write operation.</exception>
		// Token: 0x06001A74 RID: 6772 RVA: 0x00058164 File Offset: 0x00056364
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task FlushAsync()
		{
			return Task.Factory.StartNew(TextWriter._FlushDelegate, this, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		/// <summary>Provides a <see langword="TextWriter" /> with no backing store that can be written to, but not read from.</summary>
		// Token: 0x04000925 RID: 2341
		[__DynamicallyInvokable]
		public static readonly TextWriter Null = new TextWriter.NullTextWriter();

		// Token: 0x04000926 RID: 2342
		[NonSerialized]
		private static Action<object> _WriteCharDelegate = delegate(object state)
		{
			Tuple<TextWriter, char> tuple = (Tuple<TextWriter, char>)state;
			tuple.Item1.Write(tuple.Item2);
		};

		// Token: 0x04000927 RID: 2343
		[NonSerialized]
		private static Action<object> _WriteStringDelegate = delegate(object state)
		{
			Tuple<TextWriter, string> tuple2 = (Tuple<TextWriter, string>)state;
			tuple2.Item1.Write(tuple2.Item2);
		};

		// Token: 0x04000928 RID: 2344
		[NonSerialized]
		private static Action<object> _WriteCharArrayRangeDelegate = delegate(object state)
		{
			Tuple<TextWriter, char[], int, int> tuple3 = (Tuple<TextWriter, char[], int, int>)state;
			tuple3.Item1.Write(tuple3.Item2, tuple3.Item3, tuple3.Item4);
		};

		// Token: 0x04000929 RID: 2345
		[NonSerialized]
		private static Action<object> _WriteLineCharDelegate = delegate(object state)
		{
			Tuple<TextWriter, char> tuple4 = (Tuple<TextWriter, char>)state;
			tuple4.Item1.WriteLine(tuple4.Item2);
		};

		// Token: 0x0400092A RID: 2346
		[NonSerialized]
		private static Action<object> _WriteLineStringDelegate = delegate(object state)
		{
			Tuple<TextWriter, string> tuple5 = (Tuple<TextWriter, string>)state;
			tuple5.Item1.WriteLine(tuple5.Item2);
		};

		// Token: 0x0400092B RID: 2347
		[NonSerialized]
		private static Action<object> _WriteLineCharArrayRangeDelegate = delegate(object state)
		{
			Tuple<TextWriter, char[], int, int> tuple6 = (Tuple<TextWriter, char[], int, int>)state;
			tuple6.Item1.WriteLine(tuple6.Item2, tuple6.Item3, tuple6.Item4);
		};

		// Token: 0x0400092C RID: 2348
		[NonSerialized]
		private static Action<object> _FlushDelegate = delegate(object state)
		{
			((TextWriter)state).Flush();
		};

		// Token: 0x0400092D RID: 2349
		private const string InitialNewLine = "\r\n";

		/// <summary>Stores the newline characters used for this <see langword="TextWriter" />.</summary>
		// Token: 0x0400092E RID: 2350
		[__DynamicallyInvokable]
		protected char[] CoreNewLine = new char[] { '\r', '\n' };

		// Token: 0x0400092F RID: 2351
		private IFormatProvider InternalFormatProvider;

		// Token: 0x02000B26 RID: 2854
		[Serializable]
		private sealed class NullTextWriter : TextWriter
		{
			// Token: 0x06006B32 RID: 27442 RVA: 0x00174A4D File Offset: 0x00172C4D
			internal NullTextWriter()
				: base(CultureInfo.InvariantCulture)
			{
			}

			// Token: 0x1700121A RID: 4634
			// (get) Token: 0x06006B33 RID: 27443 RVA: 0x00174A5A File Offset: 0x00172C5A
			public override Encoding Encoding
			{
				get
				{
					return Encoding.Default;
				}
			}

			// Token: 0x06006B34 RID: 27444 RVA: 0x00174A61 File Offset: 0x00172C61
			public override void Write(char[] buffer, int index, int count)
			{
			}

			// Token: 0x06006B35 RID: 27445 RVA: 0x00174A63 File Offset: 0x00172C63
			public override void Write(string value)
			{
			}

			// Token: 0x06006B36 RID: 27446 RVA: 0x00174A65 File Offset: 0x00172C65
			public override void WriteLine()
			{
			}

			// Token: 0x06006B37 RID: 27447 RVA: 0x00174A67 File Offset: 0x00172C67
			public override void WriteLine(string value)
			{
			}

			// Token: 0x06006B38 RID: 27448 RVA: 0x00174A69 File Offset: 0x00172C69
			public override void WriteLine(object value)
			{
			}
		}

		// Token: 0x02000B27 RID: 2855
		[Serializable]
		internal sealed class SyncTextWriter : TextWriter, IDisposable
		{
			// Token: 0x06006B39 RID: 27449 RVA: 0x00174A6B File Offset: 0x00172C6B
			internal SyncTextWriter(TextWriter t)
				: base(t.FormatProvider)
			{
				this._out = t;
			}

			// Token: 0x1700121B RID: 4635
			// (get) Token: 0x06006B3A RID: 27450 RVA: 0x00174A80 File Offset: 0x00172C80
			public override Encoding Encoding
			{
				get
				{
					return this._out.Encoding;
				}
			}

			// Token: 0x1700121C RID: 4636
			// (get) Token: 0x06006B3B RID: 27451 RVA: 0x00174A8D File Offset: 0x00172C8D
			public override IFormatProvider FormatProvider
			{
				get
				{
					return this._out.FormatProvider;
				}
			}

			// Token: 0x1700121D RID: 4637
			// (get) Token: 0x06006B3C RID: 27452 RVA: 0x00174A9A File Offset: 0x00172C9A
			// (set) Token: 0x06006B3D RID: 27453 RVA: 0x00174AA7 File Offset: 0x00172CA7
			public override string NewLine
			{
				[MethodImpl(MethodImplOptions.Synchronized)]
				get
				{
					return this._out.NewLine;
				}
				[MethodImpl(MethodImplOptions.Synchronized)]
				set
				{
					this._out.NewLine = value;
				}
			}

			// Token: 0x06006B3E RID: 27454 RVA: 0x00174AB5 File Offset: 0x00172CB5
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Close()
			{
				this._out.Close();
			}

			// Token: 0x06006B3F RID: 27455 RVA: 0x00174AC2 File Offset: 0x00172CC2
			[MethodImpl(MethodImplOptions.Synchronized)]
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					((IDisposable)this._out).Dispose();
				}
			}

			// Token: 0x06006B40 RID: 27456 RVA: 0x00174AD2 File Offset: 0x00172CD2
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Flush()
			{
				this._out.Flush();
			}

			// Token: 0x06006B41 RID: 27457 RVA: 0x00174ADF File Offset: 0x00172CDF
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(char value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006B42 RID: 27458 RVA: 0x00174AED File Offset: 0x00172CED
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(char[] buffer)
			{
				this._out.Write(buffer);
			}

			// Token: 0x06006B43 RID: 27459 RVA: 0x00174AFB File Offset: 0x00172CFB
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(char[] buffer, int index, int count)
			{
				this._out.Write(buffer, index, count);
			}

			// Token: 0x06006B44 RID: 27460 RVA: 0x00174B0B File Offset: 0x00172D0B
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(bool value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006B45 RID: 27461 RVA: 0x00174B19 File Offset: 0x00172D19
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(int value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006B46 RID: 27462 RVA: 0x00174B27 File Offset: 0x00172D27
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(uint value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006B47 RID: 27463 RVA: 0x00174B35 File Offset: 0x00172D35
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(long value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006B48 RID: 27464 RVA: 0x00174B43 File Offset: 0x00172D43
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(ulong value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006B49 RID: 27465 RVA: 0x00174B51 File Offset: 0x00172D51
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(float value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006B4A RID: 27466 RVA: 0x00174B5F File Offset: 0x00172D5F
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(double value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006B4B RID: 27467 RVA: 0x00174B6D File Offset: 0x00172D6D
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(decimal value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006B4C RID: 27468 RVA: 0x00174B7B File Offset: 0x00172D7B
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006B4D RID: 27469 RVA: 0x00174B89 File Offset: 0x00172D89
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(object value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006B4E RID: 27470 RVA: 0x00174B97 File Offset: 0x00172D97
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, object arg0)
			{
				this._out.Write(format, arg0);
			}

			// Token: 0x06006B4F RID: 27471 RVA: 0x00174BA6 File Offset: 0x00172DA6
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, object arg0, object arg1)
			{
				this._out.Write(format, arg0, arg1);
			}

			// Token: 0x06006B50 RID: 27472 RVA: 0x00174BB6 File Offset: 0x00172DB6
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, object arg0, object arg1, object arg2)
			{
				this._out.Write(format, arg0, arg1, arg2);
			}

			// Token: 0x06006B51 RID: 27473 RVA: 0x00174BC8 File Offset: 0x00172DC8
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, params object[] arg)
			{
				this._out.Write(format, arg);
			}

			// Token: 0x06006B52 RID: 27474 RVA: 0x00174BD7 File Offset: 0x00172DD7
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine()
			{
				this._out.WriteLine();
			}

			// Token: 0x06006B53 RID: 27475 RVA: 0x00174BE4 File Offset: 0x00172DE4
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(char value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006B54 RID: 27476 RVA: 0x00174BF2 File Offset: 0x00172DF2
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(decimal value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006B55 RID: 27477 RVA: 0x00174C00 File Offset: 0x00172E00
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(char[] buffer)
			{
				this._out.WriteLine(buffer);
			}

			// Token: 0x06006B56 RID: 27478 RVA: 0x00174C0E File Offset: 0x00172E0E
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(char[] buffer, int index, int count)
			{
				this._out.WriteLine(buffer, index, count);
			}

			// Token: 0x06006B57 RID: 27479 RVA: 0x00174C1E File Offset: 0x00172E1E
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(bool value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006B58 RID: 27480 RVA: 0x00174C2C File Offset: 0x00172E2C
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(int value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006B59 RID: 27481 RVA: 0x00174C3A File Offset: 0x00172E3A
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(uint value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006B5A RID: 27482 RVA: 0x00174C48 File Offset: 0x00172E48
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(long value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006B5B RID: 27483 RVA: 0x00174C56 File Offset: 0x00172E56
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(ulong value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006B5C RID: 27484 RVA: 0x00174C64 File Offset: 0x00172E64
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(float value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006B5D RID: 27485 RVA: 0x00174C72 File Offset: 0x00172E72
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(double value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006B5E RID: 27486 RVA: 0x00174C80 File Offset: 0x00172E80
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006B5F RID: 27487 RVA: 0x00174C8E File Offset: 0x00172E8E
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(object value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006B60 RID: 27488 RVA: 0x00174C9C File Offset: 0x00172E9C
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, object arg0)
			{
				this._out.WriteLine(format, arg0);
			}

			// Token: 0x06006B61 RID: 27489 RVA: 0x00174CAB File Offset: 0x00172EAB
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, object arg0, object arg1)
			{
				this._out.WriteLine(format, arg0, arg1);
			}

			// Token: 0x06006B62 RID: 27490 RVA: 0x00174CBB File Offset: 0x00172EBB
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, object arg0, object arg1, object arg2)
			{
				this._out.WriteLine(format, arg0, arg1, arg2);
			}

			// Token: 0x06006B63 RID: 27491 RVA: 0x00174CCD File Offset: 0x00172ECD
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, params object[] arg)
			{
				this._out.WriteLine(format, arg);
			}

			// Token: 0x06006B64 RID: 27492 RVA: 0x00174CDC File Offset: 0x00172EDC
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteAsync(char value)
			{
				this.Write(value);
				return Task.CompletedTask;
			}

			// Token: 0x06006B65 RID: 27493 RVA: 0x00174CEA File Offset: 0x00172EEA
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteAsync(string value)
			{
				this.Write(value);
				return Task.CompletedTask;
			}

			// Token: 0x06006B66 RID: 27494 RVA: 0x00174CF8 File Offset: 0x00172EF8
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteAsync(char[] buffer, int index, int count)
			{
				this.Write(buffer, index, count);
				return Task.CompletedTask;
			}

			// Token: 0x06006B67 RID: 27495 RVA: 0x00174D08 File Offset: 0x00172F08
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteLineAsync(char value)
			{
				this.WriteLine(value);
				return Task.CompletedTask;
			}

			// Token: 0x06006B68 RID: 27496 RVA: 0x00174D16 File Offset: 0x00172F16
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteLineAsync(string value)
			{
				this.WriteLine(value);
				return Task.CompletedTask;
			}

			// Token: 0x06006B69 RID: 27497 RVA: 0x00174D24 File Offset: 0x00172F24
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteLineAsync(char[] buffer, int index, int count)
			{
				this.WriteLine(buffer, index, count);
				return Task.CompletedTask;
			}

			// Token: 0x06006B6A RID: 27498 RVA: 0x00174D34 File Offset: 0x00172F34
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task FlushAsync()
			{
				this.Flush();
				return Task.CompletedTask;
			}

			// Token: 0x04003342 RID: 13122
			private TextWriter _out;
		}
	}
}
