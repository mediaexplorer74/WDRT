using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Defines arrays of colors and positions used for interpolating color blending in a multicolor gradient. This class cannot be inherited.</summary>
	// Token: 0x020000B5 RID: 181
	public sealed class ColorBlend
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.ColorBlend" /> class.</summary>
		// Token: 0x06000A40 RID: 2624 RVA: 0x00025A9A File Offset: 0x00023C9A
		public ColorBlend()
		{
			this.colors = new Color[1];
			this.positions = new float[1];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.ColorBlend" /> class with the specified number of colors and positions.</summary>
		/// <param name="count">The number of colors and positions in this <see cref="T:System.Drawing.Drawing2D.ColorBlend" />.</param>
		// Token: 0x06000A41 RID: 2625 RVA: 0x00025ABA File Offset: 0x00023CBA
		public ColorBlend(int count)
		{
			this.colors = new Color[count];
			this.positions = new float[count];
		}

		/// <summary>Gets or sets an array of colors that represents the colors to use at corresponding positions along a gradient.</summary>
		/// <returns>An array of <see cref="T:System.Drawing.Color" /> structures that represents the colors to use at corresponding positions along a gradient.</returns>
		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x00025ADA File Offset: 0x00023CDA
		// (set) Token: 0x06000A43 RID: 2627 RVA: 0x00025AE2 File Offset: 0x00023CE2
		public Color[] Colors
		{
			get
			{
				return this.colors;
			}
			set
			{
				this.colors = value;
			}
		}

		/// <summary>Gets or sets the positions along a gradient line.</summary>
		/// <returns>An array of values that specify percentages of distance along the gradient line.</returns>
		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x00025AEB File Offset: 0x00023CEB
		// (set) Token: 0x06000A45 RID: 2629 RVA: 0x00025AF3 File Offset: 0x00023CF3
		public float[] Positions
		{
			get
			{
				return this.positions;
			}
			set
			{
				this.positions = value;
			}
		}

		// Token: 0x04000966 RID: 2406
		private Color[] colors;

		// Token: 0x04000967 RID: 2407
		private float[] positions;
	}
}
