using System;

namespace System.Drawing.Imaging
{
	/// <summary>Defines a map for converting colors. Several methods of the <see cref="T:System.Drawing.Imaging.ImageAttributes" /> class adjust image colors by using a color-remap table, which is an array of <see cref="T:System.Drawing.Imaging.ColorMap" /> structures. Not inheritable.</summary>
	// Token: 0x0200008F RID: 143
	public sealed class ColorMap
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.ColorMap" /> class.</summary>
		// Token: 0x060008E4 RID: 2276 RVA: 0x0002264F File Offset: 0x0002084F
		public ColorMap()
		{
			this.oldColor = default(Color);
			this.newColor = default(Color);
		}

		/// <summary>Gets or sets the existing <see cref="T:System.Drawing.Color" /> structure to be converted.</summary>
		/// <returns>The existing <see cref="T:System.Drawing.Color" /> structure to be converted.</returns>
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x0002266F File Offset: 0x0002086F
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x00022677 File Offset: 0x00020877
		public Color OldColor
		{
			get
			{
				return this.oldColor;
			}
			set
			{
				this.oldColor = value;
			}
		}

		/// <summary>Gets or sets the new <see cref="T:System.Drawing.Color" /> structure to which to convert.</summary>
		/// <returns>The new <see cref="T:System.Drawing.Color" /> structure to which to convert.</returns>
		// Token: 0x17000341 RID: 833
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x00022680 File Offset: 0x00020880
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x00022688 File Offset: 0x00020888
		public Color NewColor
		{
			get
			{
				return this.newColor;
			}
			set
			{
				this.newColor = value;
			}
		}

		// Token: 0x04000748 RID: 1864
		private Color oldColor;

		// Token: 0x04000749 RID: 1865
		private Color newColor;
	}
}
