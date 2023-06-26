using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Drawing.Printing
{
	/// <summary>Specifies the size of a piece of paper.</summary>
	// Token: 0x0200005A RID: 90
	[Serializable]
	public class PaperSize
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PaperSize" /> class.</summary>
		// Token: 0x0600074B RID: 1867 RVA: 0x0001DD97 File Offset: 0x0001BF97
		public PaperSize()
		{
			this.kind = PaperKind.Custom;
			this.name = string.Empty;
			this.createdByDefaultConstructor = true;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0001DDB8 File Offset: 0x0001BFB8
		internal PaperSize(PaperKind kind, string name, int width, int height)
		{
			this.kind = kind;
			this.name = name;
			this.width = width;
			this.height = height;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PaperSize" /> class.</summary>
		/// <param name="name">The name of the paper.</param>
		/// <param name="width">The width of the paper, in hundredths of an inch.</param>
		/// <param name="height">The height of the paper, in hundredths of an inch.</param>
		// Token: 0x0600074D RID: 1869 RVA: 0x0001DDDD File Offset: 0x0001BFDD
		public PaperSize(string name, int width, int height)
		{
			this.kind = PaperKind.Custom;
			this.name = name;
			this.width = width;
			this.height = height;
		}

		/// <summary>Gets or sets the height of the paper, in hundredths of an inch.</summary>
		/// <returns>The height of the paper, in hundredths of an inch.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Printing.PaperSize.Kind" /> property is not set to <see cref="F:System.Drawing.Printing.PaperKind.Custom" />.</exception>
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x0001DE01 File Offset: 0x0001C001
		// (set) Token: 0x0600074F RID: 1871 RVA: 0x0001DE09 File Offset: 0x0001C009
		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				if (this.kind != PaperKind.Custom && !this.createdByDefaultConstructor)
				{
					throw new ArgumentException(SR.GetString("PSizeNotCustom"));
				}
				this.height = value;
			}
		}

		/// <summary>Gets the type of paper.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Printing.PaperKind" /> values.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Printing.PaperSize.Kind" /> property is not set to <see cref="F:System.Drawing.Printing.PaperKind.Custom" />.</exception>
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x0001DE32 File Offset: 0x0001C032
		public PaperKind Kind
		{
			get
			{
				if (this.kind <= PaperKind.PrcEnvelopeNumber10Rotated && this.kind != (PaperKind)48 && this.kind != (PaperKind)49)
				{
					return this.kind;
				}
				return PaperKind.Custom;
			}
		}

		/// <summary>Gets or sets the name of the type of paper.</summary>
		/// <returns>The name of the type of paper.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Printing.PaperSize.Kind" /> property is not set to <see cref="F:System.Drawing.Printing.PaperKind.Custom" />.</exception>
		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x0001DE5A File Offset: 0x0001C05A
		// (set) Token: 0x06000752 RID: 1874 RVA: 0x0001DE62 File Offset: 0x0001C062
		public string PaperName
		{
			get
			{
				return this.name;
			}
			set
			{
				if (this.kind != PaperKind.Custom && !this.createdByDefaultConstructor)
				{
					throw new ArgumentException(SR.GetString("PSizeNotCustom"));
				}
				this.name = value;
			}
		}

		/// <summary>Gets or sets an integer representing one of the <see cref="T:System.Drawing.Printing.PaperSize" /> values or a custom value.</summary>
		/// <returns>An integer representing one of the <see cref="T:System.Drawing.Printing.PaperSize" /> values, or a custom value.</returns>
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x0001DE8B File Offset: 0x0001C08B
		// (set) Token: 0x06000754 RID: 1876 RVA: 0x0001DE93 File Offset: 0x0001C093
		public int RawKind
		{
			get
			{
				return (int)this.kind;
			}
			set
			{
				this.kind = (PaperKind)value;
			}
		}

		/// <summary>Gets or sets the width of the paper, in hundredths of an inch.</summary>
		/// <returns>The width of the paper, in hundredths of an inch.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Printing.PaperSize.Kind" /> property is not set to <see cref="F:System.Drawing.Printing.PaperKind.Custom" />.</exception>
		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x0001DE9C File Offset: 0x0001C09C
		// (set) Token: 0x06000756 RID: 1878 RVA: 0x0001DEA4 File Offset: 0x0001C0A4
		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				if (this.kind != PaperKind.Custom && !this.createdByDefaultConstructor)
				{
					throw new ArgumentException(SR.GetString("PSizeNotCustom"));
				}
				this.width = value;
			}
		}

		/// <summary>Provides information about the <see cref="T:System.Drawing.Printing.PaperSize" /> in string form.</summary>
		/// <returns>A string.</returns>
		// Token: 0x06000757 RID: 1879 RVA: 0x0001DED0 File Offset: 0x0001C0D0
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"[PaperSize ",
				this.PaperName,
				" Kind=",
				TypeDescriptor.GetConverter(typeof(PaperKind)).ConvertToString((int)this.Kind),
				" Height=",
				this.Height.ToString(CultureInfo.InvariantCulture),
				" Width=",
				this.Width.ToString(CultureInfo.InvariantCulture),
				"]"
			});
		}

		// Token: 0x0400069E RID: 1694
		private PaperKind kind;

		// Token: 0x0400069F RID: 1695
		private string name;

		// Token: 0x040006A0 RID: 1696
		private int width;

		// Token: 0x040006A1 RID: 1697
		private int height;

		// Token: 0x040006A2 RID: 1698
		private bool createdByDefaultConstructor;
	}
}
