using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Drawing.Printing
{
	/// <summary>Represents the resolution supported by a printer.</summary>
	// Token: 0x02000062 RID: 98
	[Serializable]
	public class PrinterResolution
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrinterResolution" /> class.</summary>
		// Token: 0x06000794 RID: 1940 RVA: 0x0001EA02 File Offset: 0x0001CC02
		public PrinterResolution()
		{
			this.kind = PrinterResolutionKind.Custom;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0001EA11 File Offset: 0x0001CC11
		internal PrinterResolution(PrinterResolutionKind kind, int x, int y)
		{
			this.kind = kind;
			this.x = x;
			this.y = y;
		}

		/// <summary>Gets or sets the printer resolution.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Printing.PrinterResolutionKind" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not a member of the <see cref="T:System.Drawing.Printing.PrinterResolutionKind" /> enumeration.</exception>
		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x0001EA2E File Offset: 0x0001CC2E
		// (set) Token: 0x06000797 RID: 1943 RVA: 0x0001EA36 File Offset: 0x0001CC36
		public PrinterResolutionKind Kind
		{
			get
			{
				return this.kind;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, -4, 0))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(PrinterResolutionKind));
				}
				this.kind = value;
			}
		}

		/// <summary>Gets the horizontal printer resolution, in dots per inch.</summary>
		/// <returns>The horizontal printer resolution, in dots per inch, if <see cref="P:System.Drawing.Printing.PrinterResolution.Kind" /> is set to <see cref="F:System.Drawing.Printing.PrinterResolutionKind.Custom" />; otherwise, a <see langword="dmPrintQuality" /> value.</returns>
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x0001EA66 File Offset: 0x0001CC66
		// (set) Token: 0x06000799 RID: 1945 RVA: 0x0001EA6E File Offset: 0x0001CC6E
		public int X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		/// <summary>Gets the vertical printer resolution, in dots per inch.</summary>
		/// <returns>The vertical printer resolution, in dots per inch.</returns>
		// Token: 0x170002EF RID: 751
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x0001EA77 File Offset: 0x0001CC77
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x0001EA7F File Offset: 0x0001CC7F
		public int Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		/// <summary>This member overrides the <see cref="M:System.Object.ToString" /> method.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains information about the <see cref="T:System.Drawing.Printing.PrinterResolution" />.</returns>
		// Token: 0x0600079C RID: 1948 RVA: 0x0001EA88 File Offset: 0x0001CC88
		public override string ToString()
		{
			if (this.kind != PrinterResolutionKind.Custom)
			{
				return "[PrinterResolution " + TypeDescriptor.GetConverter(typeof(PrinterResolutionKind)).ConvertToString((int)this.Kind) + "]";
			}
			return string.Concat(new string[]
			{
				"[PrinterResolution X=",
				this.X.ToString(CultureInfo.InvariantCulture),
				" Y=",
				this.Y.ToString(CultureInfo.InvariantCulture),
				"]"
			});
		}

		// Token: 0x040006C9 RID: 1737
		private int x;

		// Token: 0x040006CA RID: 1738
		private int y;

		// Token: 0x040006CB RID: 1739
		private PrinterResolutionKind kind;
	}
}
