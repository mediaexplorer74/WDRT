using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.CodeDom.Compiler
{
	/// <summary>Provides a text writer that can indent new lines by a tab string token.</summary>
	// Token: 0x02000680 RID: 1664
	public class IndentedTextWriter : TextWriter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.IndentedTextWriter" /> class using the specified text writer and default tab string.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to use for output.</param>
		// Token: 0x06003D34 RID: 15668 RVA: 0x000FB447 File Offset: 0x000F9647
		public IndentedTextWriter(TextWriter writer)
			: this(writer, "    ")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.IndentedTextWriter" /> class using the specified text writer and tab string.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to use for output.</param>
		/// <param name="tabString">The tab string to use for indentation.</param>
		// Token: 0x06003D35 RID: 15669 RVA: 0x000FB455 File Offset: 0x000F9655
		public IndentedTextWriter(TextWriter writer, string tabString)
			: base(CultureInfo.InvariantCulture)
		{
			this.writer = writer;
			this.tabString = tabString;
			this.indentLevel = 0;
			this.tabsPending = false;
		}

		/// <summary>Gets the encoding for the text writer to use.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> that indicates the encoding for the text writer to use.</returns>
		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x06003D36 RID: 15670 RVA: 0x000FB47E File Offset: 0x000F967E
		public override Encoding Encoding
		{
			get
			{
				return this.writer.Encoding;
			}
		}

		/// <summary>Gets or sets the new line character to use.</summary>
		/// <returns>The new line character to use.</returns>
		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x06003D37 RID: 15671 RVA: 0x000FB48B File Offset: 0x000F968B
		// (set) Token: 0x06003D38 RID: 15672 RVA: 0x000FB498 File Offset: 0x000F9698
		public override string NewLine
		{
			get
			{
				return this.writer.NewLine;
			}
			set
			{
				this.writer.NewLine = value;
			}
		}

		/// <summary>Gets or sets the number of spaces to indent.</summary>
		/// <returns>The number of spaces to indent.</returns>
		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x06003D39 RID: 15673 RVA: 0x000FB4A6 File Offset: 0x000F96A6
		// (set) Token: 0x06003D3A RID: 15674 RVA: 0x000FB4AE File Offset: 0x000F96AE
		public int Indent
		{
			get
			{
				return this.indentLevel;
			}
			set
			{
				if (value < 0)
				{
					value = 0;
				}
				this.indentLevel = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.IO.TextWriter" /> to use.</summary>
		/// <returns>The <see cref="T:System.IO.TextWriter" /> to use.</returns>
		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x06003D3B RID: 15675 RVA: 0x000FB4BE File Offset: 0x000F96BE
		public TextWriter InnerWriter
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06003D3C RID: 15676 RVA: 0x000FB4C6 File Offset: 0x000F96C6
		internal string TabString
		{
			get
			{
				return this.tabString;
			}
		}

		/// <summary>Closes the document being written to.</summary>
		// Token: 0x06003D3D RID: 15677 RVA: 0x000FB4CE File Offset: 0x000F96CE
		public override void Close()
		{
			this.writer.Close();
		}

		/// <summary>Flushes the stream.</summary>
		// Token: 0x06003D3E RID: 15678 RVA: 0x000FB4DB File Offset: 0x000F96DB
		public override void Flush()
		{
			this.writer.Flush();
		}

		/// <summary>Outputs the tab string once for each level of indentation according to the <see cref="P:System.CodeDom.Compiler.IndentedTextWriter.Indent" /> property.</summary>
		// Token: 0x06003D3F RID: 15679 RVA: 0x000FB4E8 File Offset: 0x000F96E8
		protected virtual void OutputTabs()
		{
			if (this.tabsPending)
			{
				for (int i = 0; i < this.indentLevel; i++)
				{
					this.writer.Write(this.tabString);
				}
				this.tabsPending = false;
			}
		}

		/// <summary>Writes the specified string to the text stream.</summary>
		/// <param name="s">The string to write.</param>
		// Token: 0x06003D40 RID: 15680 RVA: 0x000FB526 File Offset: 0x000F9726
		public override void Write(string s)
		{
			this.OutputTabs();
			this.writer.Write(s);
		}

		/// <summary>Writes the text representation of a Boolean value to the text stream.</summary>
		/// <param name="value">The Boolean value to write.</param>
		// Token: 0x06003D41 RID: 15681 RVA: 0x000FB53A File Offset: 0x000F973A
		public override void Write(bool value)
		{
			this.OutputTabs();
			this.writer.Write(value);
		}

		/// <summary>Writes a character to the text stream.</summary>
		/// <param name="value">The character to write.</param>
		// Token: 0x06003D42 RID: 15682 RVA: 0x000FB54E File Offset: 0x000F974E
		public override void Write(char value)
		{
			this.OutputTabs();
			this.writer.Write(value);
		}

		/// <summary>Writes a character array to the text stream.</summary>
		/// <param name="buffer">The character array to write.</param>
		// Token: 0x06003D43 RID: 15683 RVA: 0x000FB562 File Offset: 0x000F9762
		public override void Write(char[] buffer)
		{
			this.OutputTabs();
			this.writer.Write(buffer);
		}

		/// <summary>Writes a subarray of characters to the text stream.</summary>
		/// <param name="buffer">The character array to write data from.</param>
		/// <param name="index">Starting index in the buffer.</param>
		/// <param name="count">The number of characters to write.</param>
		// Token: 0x06003D44 RID: 15684 RVA: 0x000FB576 File Offset: 0x000F9776
		public override void Write(char[] buffer, int index, int count)
		{
			this.OutputTabs();
			this.writer.Write(buffer, index, count);
		}

		/// <summary>Writes the text representation of a Double to the text stream.</summary>
		/// <param name="value">The <see langword="double" /> to write.</param>
		// Token: 0x06003D45 RID: 15685 RVA: 0x000FB58C File Offset: 0x000F978C
		public override void Write(double value)
		{
			this.OutputTabs();
			this.writer.Write(value);
		}

		/// <summary>Writes the text representation of a Single to the text stream.</summary>
		/// <param name="value">The <see langword="single" /> to write.</param>
		// Token: 0x06003D46 RID: 15686 RVA: 0x000FB5A0 File Offset: 0x000F97A0
		public override void Write(float value)
		{
			this.OutputTabs();
			this.writer.Write(value);
		}

		/// <summary>Writes the text representation of an integer to the text stream.</summary>
		/// <param name="value">The integer to write.</param>
		// Token: 0x06003D47 RID: 15687 RVA: 0x000FB5B4 File Offset: 0x000F97B4
		public override void Write(int value)
		{
			this.OutputTabs();
			this.writer.Write(value);
		}

		/// <summary>Writes the text representation of an 8-byte integer to the text stream.</summary>
		/// <param name="value">The 8-byte integer to write.</param>
		// Token: 0x06003D48 RID: 15688 RVA: 0x000FB5C8 File Offset: 0x000F97C8
		public override void Write(long value)
		{
			this.OutputTabs();
			this.writer.Write(value);
		}

		/// <summary>Writes the text representation of an object to the text stream.</summary>
		/// <param name="value">The object to write.</param>
		// Token: 0x06003D49 RID: 15689 RVA: 0x000FB5DC File Offset: 0x000F97DC
		public override void Write(object value)
		{
			this.OutputTabs();
			this.writer.Write(value);
		}

		/// <summary>Writes out a formatted string, using the same semantics as specified.</summary>
		/// <param name="format">The formatting string.</param>
		/// <param name="arg0">The object to write into the formatted string.</param>
		// Token: 0x06003D4A RID: 15690 RVA: 0x000FB5F0 File Offset: 0x000F97F0
		public override void Write(string format, object arg0)
		{
			this.OutputTabs();
			this.writer.Write(format, arg0);
		}

		/// <summary>Writes out a formatted string, using the same semantics as specified.</summary>
		/// <param name="format">The formatting string to use.</param>
		/// <param name="arg0">The first object to write into the formatted string.</param>
		/// <param name="arg1">The second object to write into the formatted string.</param>
		// Token: 0x06003D4B RID: 15691 RVA: 0x000FB605 File Offset: 0x000F9805
		public override void Write(string format, object arg0, object arg1)
		{
			this.OutputTabs();
			this.writer.Write(format, arg0, arg1);
		}

		/// <summary>Writes out a formatted string, using the same semantics as specified.</summary>
		/// <param name="format">The formatting string to use.</param>
		/// <param name="arg">The argument array to output.</param>
		// Token: 0x06003D4C RID: 15692 RVA: 0x000FB61B File Offset: 0x000F981B
		public override void Write(string format, params object[] arg)
		{
			this.OutputTabs();
			this.writer.Write(format, arg);
		}

		/// <summary>Writes the specified string to a line without tabs.</summary>
		/// <param name="s">The string to write.</param>
		// Token: 0x06003D4D RID: 15693 RVA: 0x000FB630 File Offset: 0x000F9830
		public void WriteLineNoTabs(string s)
		{
			this.writer.WriteLine(s);
		}

		/// <summary>Writes the specified string, followed by a line terminator, to the text stream.</summary>
		/// <param name="s">The string to write.</param>
		// Token: 0x06003D4E RID: 15694 RVA: 0x000FB63E File Offset: 0x000F983E
		public override void WriteLine(string s)
		{
			this.OutputTabs();
			this.writer.WriteLine(s);
			this.tabsPending = true;
		}

		/// <summary>Writes a line terminator.</summary>
		// Token: 0x06003D4F RID: 15695 RVA: 0x000FB659 File Offset: 0x000F9859
		public override void WriteLine()
		{
			this.OutputTabs();
			this.writer.WriteLine();
			this.tabsPending = true;
		}

		/// <summary>Writes the text representation of a Boolean, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">The Boolean to write.</param>
		// Token: 0x06003D50 RID: 15696 RVA: 0x000FB673 File Offset: 0x000F9873
		public override void WriteLine(bool value)
		{
			this.OutputTabs();
			this.writer.WriteLine(value);
			this.tabsPending = true;
		}

		/// <summary>Writes a character, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">The character to write.</param>
		// Token: 0x06003D51 RID: 15697 RVA: 0x000FB68E File Offset: 0x000F988E
		public override void WriteLine(char value)
		{
			this.OutputTabs();
			this.writer.WriteLine(value);
			this.tabsPending = true;
		}

		/// <summary>Writes a character array, followed by a line terminator, to the text stream.</summary>
		/// <param name="buffer">The character array to write.</param>
		// Token: 0x06003D52 RID: 15698 RVA: 0x000FB6A9 File Offset: 0x000F98A9
		public override void WriteLine(char[] buffer)
		{
			this.OutputTabs();
			this.writer.WriteLine(buffer);
			this.tabsPending = true;
		}

		/// <summary>Writes a subarray of characters, followed by a line terminator, to the text stream.</summary>
		/// <param name="buffer">The character array to write data from.</param>
		/// <param name="index">Starting index in the buffer.</param>
		/// <param name="count">The number of characters to write.</param>
		// Token: 0x06003D53 RID: 15699 RVA: 0x000FB6C4 File Offset: 0x000F98C4
		public override void WriteLine(char[] buffer, int index, int count)
		{
			this.OutputTabs();
			this.writer.WriteLine(buffer, index, count);
			this.tabsPending = true;
		}

		/// <summary>Writes the text representation of a Double, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">The <see langword="double" /> to write.</param>
		// Token: 0x06003D54 RID: 15700 RVA: 0x000FB6E1 File Offset: 0x000F98E1
		public override void WriteLine(double value)
		{
			this.OutputTabs();
			this.writer.WriteLine(value);
			this.tabsPending = true;
		}

		/// <summary>Writes the text representation of a Single, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">The <see langword="single" /> to write.</param>
		// Token: 0x06003D55 RID: 15701 RVA: 0x000FB6FC File Offset: 0x000F98FC
		public override void WriteLine(float value)
		{
			this.OutputTabs();
			this.writer.WriteLine(value);
			this.tabsPending = true;
		}

		/// <summary>Writes the text representation of an integer, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">The integer to write.</param>
		// Token: 0x06003D56 RID: 15702 RVA: 0x000FB717 File Offset: 0x000F9917
		public override void WriteLine(int value)
		{
			this.OutputTabs();
			this.writer.WriteLine(value);
			this.tabsPending = true;
		}

		/// <summary>Writes the text representation of an 8-byte integer, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">The 8-byte integer to write.</param>
		// Token: 0x06003D57 RID: 15703 RVA: 0x000FB732 File Offset: 0x000F9932
		public override void WriteLine(long value)
		{
			this.OutputTabs();
			this.writer.WriteLine(value);
			this.tabsPending = true;
		}

		/// <summary>Writes the text representation of an object, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">The object to write.</param>
		// Token: 0x06003D58 RID: 15704 RVA: 0x000FB74D File Offset: 0x000F994D
		public override void WriteLine(object value)
		{
			this.OutputTabs();
			this.writer.WriteLine(value);
			this.tabsPending = true;
		}

		/// <summary>Writes out a formatted string, followed by a line terminator, using the same semantics as specified.</summary>
		/// <param name="format">The formatting string.</param>
		/// <param name="arg0">The object to write into the formatted string.</param>
		// Token: 0x06003D59 RID: 15705 RVA: 0x000FB768 File Offset: 0x000F9968
		public override void WriteLine(string format, object arg0)
		{
			this.OutputTabs();
			this.writer.WriteLine(format, arg0);
			this.tabsPending = true;
		}

		/// <summary>Writes out a formatted string, followed by a line terminator, using the same semantics as specified.</summary>
		/// <param name="format">The formatting string to use.</param>
		/// <param name="arg0">The first object to write into the formatted string.</param>
		/// <param name="arg1">The second object to write into the formatted string.</param>
		// Token: 0x06003D5A RID: 15706 RVA: 0x000FB784 File Offset: 0x000F9984
		public override void WriteLine(string format, object arg0, object arg1)
		{
			this.OutputTabs();
			this.writer.WriteLine(format, arg0, arg1);
			this.tabsPending = true;
		}

		/// <summary>Writes out a formatted string, followed by a line terminator, using the same semantics as specified.</summary>
		/// <param name="format">The formatting string to use.</param>
		/// <param name="arg">The argument array to output.</param>
		// Token: 0x06003D5B RID: 15707 RVA: 0x000FB7A1 File Offset: 0x000F99A1
		public override void WriteLine(string format, params object[] arg)
		{
			this.OutputTabs();
			this.writer.WriteLine(format, arg);
			this.tabsPending = true;
		}

		/// <summary>Writes the text representation of a UInt32, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">A UInt32 to output.</param>
		// Token: 0x06003D5C RID: 15708 RVA: 0x000FB7BD File Offset: 0x000F99BD
		[CLSCompliant(false)]
		public override void WriteLine(uint value)
		{
			this.OutputTabs();
			this.writer.WriteLine(value);
			this.tabsPending = true;
		}

		// Token: 0x06003D5D RID: 15709 RVA: 0x000FB7D8 File Offset: 0x000F99D8
		internal void InternalOutputTabs()
		{
			for (int i = 0; i < this.indentLevel; i++)
			{
				this.writer.Write(this.tabString);
			}
		}

		// Token: 0x04002C9F RID: 11423
		private TextWriter writer;

		// Token: 0x04002CA0 RID: 11424
		private int indentLevel;

		// Token: 0x04002CA1 RID: 11425
		private bool tabsPending;

		// Token: 0x04002CA2 RID: 11426
		private string tabString;

		/// <summary>Specifies the default tab string. This field is constant.</summary>
		// Token: 0x04002CA3 RID: 11427
		public const string DefaultTabString = "    ";
	}
}
