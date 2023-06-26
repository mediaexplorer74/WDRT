using System;
using System.IO;
using System.Text;

namespace Microsoft.Data.OData.Json
{
	// Token: 0x020002A3 RID: 675
	internal sealed class IndentedTextWriter : TextWriter
	{
		// Token: 0x060016BD RID: 5821 RVA: 0x000530F9 File Offset: 0x000512F9
		public IndentedTextWriter(TextWriter writer, bool enableIndentation)
			: base(writer.FormatProvider)
		{
			this.writer = writer;
			this.enableIndentation = enableIndentation;
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060016BE RID: 5822 RVA: 0x00053115 File Offset: 0x00051315
		public override Encoding Encoding
		{
			get
			{
				return this.writer.Encoding;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060016BF RID: 5823 RVA: 0x00053122 File Offset: 0x00051322
		public override string NewLine
		{
			get
			{
				return this.writer.NewLine;
			}
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x0005312F File Offset: 0x0005132F
		public void IncreaseIndentation()
		{
			this.indentLevel++;
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0005313F File Offset: 0x0005133F
		public void DecreaseIndentation()
		{
			if (this.indentLevel < 1)
			{
				this.indentLevel = 0;
				return;
			}
			this.indentLevel--;
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x00053160 File Offset: 0x00051360
		public override void Close()
		{
			IndentedTextWriter.InternalCloseOrDispose();
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x00053167 File Offset: 0x00051367
		public override void Flush()
		{
			this.writer.Flush();
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x00053174 File Offset: 0x00051374
		public override void Write(string s)
		{
			this.WriteIndentation();
			this.writer.Write(s);
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x00053188 File Offset: 0x00051388
		public override void Write(char value)
		{
			this.WriteIndentation();
			this.writer.Write(value);
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x0005319C File Offset: 0x0005139C
		public override void WriteLine()
		{
			if (this.enableIndentation)
			{
				base.WriteLine();
			}
			this.indentationPending = true;
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x000531B3 File Offset: 0x000513B3
		private static void InternalCloseOrDispose()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x000531BC File Offset: 0x000513BC
		private void WriteIndentation()
		{
			if (!this.enableIndentation || !this.indentationPending)
			{
				return;
			}
			for (int i = 0; i < this.indentLevel; i++)
			{
				this.writer.Write("  ");
			}
			this.indentationPending = false;
		}

		// Token: 0x04000937 RID: 2359
		private const string IndentationString = "  ";

		// Token: 0x04000938 RID: 2360
		private readonly TextWriter writer;

		// Token: 0x04000939 RID: 2361
		private readonly bool enableIndentation;

		// Token: 0x0400093A RID: 2362
		private int indentLevel;

		// Token: 0x0400093B RID: 2363
		private bool indentationPending;
	}
}
