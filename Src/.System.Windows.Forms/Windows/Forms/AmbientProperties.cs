using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides ambient property values to top-level controls.</summary>
	// Token: 0x0200011D RID: 285
	public sealed class AmbientProperties
	{
		/// <summary>Gets or sets the ambient background color of an object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> value that represents the background color of an object.</returns>
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x000181C9 File Offset: 0x000163C9
		// (set) Token: 0x060008E9 RID: 2281 RVA: 0x000181D1 File Offset: 0x000163D1
		public Color BackColor
		{
			get
			{
				return this.backColor;
			}
			set
			{
				this.backColor = value;
			}
		}

		/// <summary>Gets or sets the ambient cursor of an object.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor of an object.</returns>
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x000181DA File Offset: 0x000163DA
		// (set) Token: 0x060008EB RID: 2283 RVA: 0x000181E2 File Offset: 0x000163E2
		public Cursor Cursor
		{
			get
			{
				return this.cursor;
			}
			set
			{
				this.cursor = value;
			}
		}

		/// <summary>Gets or sets the ambient font of an object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> that represents the font used when displaying text within an object.</returns>
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060008EC RID: 2284 RVA: 0x000181EB File Offset: 0x000163EB
		// (set) Token: 0x060008ED RID: 2285 RVA: 0x000181F3 File Offset: 0x000163F3
		public Font Font
		{
			get
			{
				return this.font;
			}
			set
			{
				this.font = value;
			}
		}

		/// <summary>Gets or sets the ambient foreground color of an object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> value that represents the foreground color of an object.</returns>
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x000181FC File Offset: 0x000163FC
		// (set) Token: 0x060008EF RID: 2287 RVA: 0x00018204 File Offset: 0x00016404
		public Color ForeColor
		{
			get
			{
				return this.foreColor;
			}
			set
			{
				this.foreColor = value;
			}
		}

		// Token: 0x040005BD RID: 1469
		private Color backColor;

		// Token: 0x040005BE RID: 1470
		private Color foreColor;

		// Token: 0x040005BF RID: 1471
		private Cursor cursor;

		// Token: 0x040005C0 RID: 1472
		private Font font;
	}
}
