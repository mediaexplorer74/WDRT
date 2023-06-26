using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Provides colors used for Microsoft Office display elements.</summary>
	// Token: 0x02000326 RID: 806
	public class ProfessionalColorTable
	{
		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06003350 RID: 13136 RVA: 0x000E3D14 File Offset: 0x000E1F14
		private Dictionary<ProfessionalColorTable.KnownColors, Color> ColorTable
		{
			get
			{
				if (this.UseSystemColors)
				{
					if (!this.usingSystemColors || this.professionalRGB == null)
					{
						if (this.professionalRGB == null)
						{
							this.professionalRGB = new Dictionary<ProfessionalColorTable.KnownColors, Color>(212);
						}
						this.InitSystemColors(ref this.professionalRGB);
					}
				}
				else if (ToolStripManager.VisualStylesEnabled)
				{
					if (this.usingSystemColors || this.professionalRGB == null)
					{
						if (this.professionalRGB == null)
						{
							this.professionalRGB = new Dictionary<ProfessionalColorTable.KnownColors, Color>(212);
						}
						this.InitThemedColors(ref this.professionalRGB);
					}
				}
				else if (!this.usingSystemColors || this.professionalRGB == null)
				{
					if (this.professionalRGB == null)
					{
						this.professionalRGB = new Dictionary<ProfessionalColorTable.KnownColors, Color>(212);
					}
					this.InitSystemColors(ref this.professionalRGB);
				}
				return this.professionalRGB;
			}
		}

		/// <summary>Gets or sets a value indicating whether to use <see cref="T:System.Drawing.SystemColors" /> rather than colors that match the current visual style.</summary>
		/// <returns>
		///   <see langword="true" /> to use <see cref="T:System.Drawing.SystemColors" />; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x06003351 RID: 13137 RVA: 0x000E3DD9 File Offset: 0x000E1FD9
		// (set) Token: 0x06003352 RID: 13138 RVA: 0x000E3DE1 File Offset: 0x000E1FE1
		public bool UseSystemColors
		{
			get
			{
				return this.useSystemColors;
			}
			set
			{
				if (this.useSystemColors != value)
				{
					this.useSystemColors = value;
					this.ResetRGBTable();
				}
			}
		}

		// Token: 0x06003353 RID: 13139 RVA: 0x000E3DFC File Offset: 0x000E1FFC
		internal Color FromKnownColor(ProfessionalColorTable.KnownColors color)
		{
			if (ProfessionalColors.ColorFreshnessKey != this.colorFreshnessKey || ProfessionalColors.ColorScheme != this.lastKnownColorScheme)
			{
				this.ResetRGBTable();
			}
			this.colorFreshnessKey = ProfessionalColors.ColorFreshnessKey;
			this.lastKnownColorScheme = ProfessionalColors.ColorScheme;
			return this.ColorTable[color];
		}

		// Token: 0x06003354 RID: 13140 RVA: 0x000E3E50 File Offset: 0x000E2050
		private void ResetRGBTable()
		{
			if (this.professionalRGB != null)
			{
				this.professionalRGB.Clear();
			}
			this.professionalRGB = null;
		}

		/// <summary>Gets the solid color used when the button is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color used when the button is selected.</returns>
		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x06003355 RID: 13141 RVA: 0x000E3E6C File Offset: 0x000E206C
		[SRDescription("ProfessionalColorsButtonSelectedHighlightDescr")]
		public virtual Color ButtonSelectedHighlight
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.ButtonSelectedHighlight);
			}
		}

		/// <summary>Gets the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedHighlight" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedHighlight" />.</returns>
		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06003356 RID: 13142 RVA: 0x000E3E79 File Offset: 0x000E2079
		[SRDescription("ProfessionalColorsButtonSelectedHighlightBorderDescr")]
		public virtual Color ButtonSelectedHighlightBorder
		{
			get
			{
				return this.ButtonPressedBorder;
			}
		}

		/// <summary>Gets the solid color used when the button is pressed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color used when the button is pressed.</returns>
		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06003357 RID: 13143 RVA: 0x000E3E81 File Offset: 0x000E2081
		[SRDescription("ProfessionalColorsButtonPressedHighlightDescr")]
		public virtual Color ButtonPressedHighlight
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.ButtonPressedHighlight);
			}
		}

		/// <summary>Gets the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedHighlight" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedHighlight" />.</returns>
		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06003358 RID: 13144 RVA: 0x000E3E8E File Offset: 0x000E208E
		[SRDescription("ProfessionalColorsButtonPressedHighlightBorderDescr")]
		public virtual Color ButtonPressedHighlightBorder
		{
			get
			{
				return SystemColors.Highlight;
			}
		}

		/// <summary>Gets the solid color used when the button is checked.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color used when the button is checked.</returns>
		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06003359 RID: 13145 RVA: 0x000E3E95 File Offset: 0x000E2095
		[SRDescription("ProfessionalColorsButtonCheckedHighlightDescr")]
		public virtual Color ButtonCheckedHighlight
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.ButtonCheckedHighlight);
			}
		}

		/// <summary>Gets the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonCheckedHighlight" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonCheckedHighlight" />.</returns>
		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x0600335A RID: 13146 RVA: 0x000E3E8E File Offset: 0x000E208E
		[SRDescription("ProfessionalColorsButtonCheckedHighlightBorderDescr")]
		public virtual Color ButtonCheckedHighlightBorder
		{
			get
			{
				return SystemColors.Highlight;
			}
		}

		/// <summary>Gets the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientBegin" />, <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientMiddle" />, and <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientEnd" /> colors.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientBegin" />, <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientMiddle" />, and <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientEnd" /> colors.</returns>
		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x0600335B RID: 13147 RVA: 0x000E3EA2 File Offset: 0x000E20A2
		[SRDescription("ProfessionalColorsButtonPressedBorderDescr")]
		public virtual Color ButtonPressedBorder
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrMouseOver);
			}
		}

		/// <summary>Gets the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientBegin" />, <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientMiddle" />, and <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientEnd" /> colors.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientBegin" />, <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientMiddle" />, and <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientEnd" /> colors.</returns>
		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x0600335C RID: 13148 RVA: 0x000E3EA2 File Offset: 0x000E20A2
		[SRDescription("ProfessionalColorsButtonSelectedBorderDescr")]
		public virtual Color ButtonSelectedBorder
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrMouseOver);
			}
		}

		/// <summary>Gets the starting color of the gradient used when the button is checked.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used when the button is checked.</returns>
		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x0600335D RID: 13149 RVA: 0x000E3EAB File Offset: 0x000E20AB
		[SRDescription("ProfessionalColorsButtonCheckedGradientBeginDescr")]
		public virtual Color ButtonCheckedGradientBegin
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedBegin);
			}
		}

		/// <summary>Gets the middle color of the gradient used when the button is checked.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used when the button is checked.</returns>
		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x0600335E RID: 13150 RVA: 0x000E3EB5 File Offset: 0x000E20B5
		[SRDescription("ProfessionalColorsButtonCheckedGradientMiddleDescr")]
		public virtual Color ButtonCheckedGradientMiddle
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedMiddle);
			}
		}

		/// <summary>Gets the end color of the gradient used when the button is checked.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when the button is checked.</returns>
		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x0600335F RID: 13151 RVA: 0x000E3EBF File Offset: 0x000E20BF
		[SRDescription("ProfessionalColorsButtonCheckedGradientEndDescr")]
		public virtual Color ButtonCheckedGradientEnd
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedEnd);
			}
		}

		/// <summary>Gets the starting color of the gradient used when the button is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used when the button is selected.</returns>
		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x06003360 RID: 13152 RVA: 0x000E3EC9 File Offset: 0x000E20C9
		[SRDescription("ProfessionalColorsButtonSelectedGradientBeginDescr")]
		public virtual Color ButtonSelectedGradientBegin
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverBegin);
			}
		}

		/// <summary>Gets the middle color of the gradient used when the button is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used when the button is selected.</returns>
		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06003361 RID: 13153 RVA: 0x000E3ED3 File Offset: 0x000E20D3
		[SRDescription("ProfessionalColorsButtonSelectedGradientMiddleDescr")]
		public virtual Color ButtonSelectedGradientMiddle
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverMiddle);
			}
		}

		/// <summary>Gets the end color of the gradient used when the button is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when the button is selected.</returns>
		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06003362 RID: 13154 RVA: 0x000E3EDD File Offset: 0x000E20DD
		[SRDescription("ProfessionalColorsButtonSelectedGradientEndDescr")]
		public virtual Color ButtonSelectedGradientEnd
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverEnd);
			}
		}

		/// <summary>Gets the starting color of the gradient used when the button is pressed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used when the button is pressed.</returns>
		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06003363 RID: 13155 RVA: 0x000E3EE7 File Offset: 0x000E20E7
		[SRDescription("ProfessionalColorsButtonPressedGradientBeginDescr")]
		public virtual Color ButtonPressedGradientBegin
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownBegin);
			}
		}

		/// <summary>Gets the middle color of the gradient used when the button is pressed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used when the button is pressed.</returns>
		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06003364 RID: 13156 RVA: 0x000E3EF1 File Offset: 0x000E20F1
		[SRDescription("ProfessionalColorsButtonPressedGradientMiddleDescr")]
		public virtual Color ButtonPressedGradientMiddle
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownMiddle);
			}
		}

		/// <summary>Gets the end color of the gradient used when the button is pressed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when the button is pressed.</returns>
		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06003365 RID: 13157 RVA: 0x000E3EFB File Offset: 0x000E20FB
		[SRDescription("ProfessionalColorsButtonPressedGradientEndDescr")]
		public virtual Color ButtonPressedGradientEnd
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownEnd);
			}
		}

		/// <summary>Gets the solid color to use when the button is checked and gradients are being used.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color to use when the button is checked and gradients are being used.</returns>
		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06003366 RID: 13158 RVA: 0x000E3F05 File Offset: 0x000E2105
		[SRDescription("ProfessionalColorsCheckBackgroundDescr")]
		public virtual Color CheckBackground
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelected);
			}
		}

		/// <summary>Gets the solid color to use when the button is checked and selected and gradients are being used.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color to use when the button is checked and selected and gradients are being used.</returns>
		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06003367 RID: 13159 RVA: 0x000E3F0F File Offset: 0x000E210F
		[SRDescription("ProfessionalColorsCheckSelectedBackgroundDescr")]
		public virtual Color CheckSelectedBackground
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelectedMouseOver);
			}
		}

		/// <summary>Gets the solid color to use when the button is checked and selected and gradients are being used.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color to use when the button is checked and selected and gradients are being used.</returns>
		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06003368 RID: 13160 RVA: 0x000E3F0F File Offset: 0x000E210F
		[SRDescription("ProfessionalColorsCheckPressedBackgroundDescr")]
		public virtual Color CheckPressedBackground
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelectedMouseOver);
			}
		}

		/// <summary>Gets the color to use for shadow effects on the grip (move handle).</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color to use for shadow effects on the grip (move handle).</returns>
		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06003369 RID: 13161 RVA: 0x000E3F19 File Offset: 0x000E2119
		[SRDescription("ProfessionalColorsGripDarkDescr")]
		public virtual Color GripDark
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBDragHandle);
			}
		}

		/// <summary>Gets the color to use for highlight effects on the grip (move handle).</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color to use for highlight effects on the grip (move handle).</returns>
		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x0600336A RID: 13162 RVA: 0x000E3F23 File Offset: 0x000E2123
		[SRDescription("ProfessionalColorsGripLightDescr")]
		public virtual Color GripLight
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBDragHandleShadow);
			}
		}

		/// <summary>Gets the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</returns>
		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x0600336B RID: 13163 RVA: 0x000E3F2D File Offset: 0x000E212D
		[SRDescription("ProfessionalColorsImageMarginGradientBeginDescr")]
		public virtual Color ImageMarginGradientBegin
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradVertBegin);
			}
		}

		/// <summary>Gets the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</returns>
		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x0600336C RID: 13164 RVA: 0x000E3F37 File Offset: 0x000E2137
		[SRDescription("ProfessionalColorsImageMarginGradientMiddleDescr")]
		public virtual Color ImageMarginGradientMiddle
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradVertMiddle);
			}
		}

		/// <summary>Gets the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</returns>
		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x0600336D RID: 13165 RVA: 0x000E3F41 File Offset: 0x000E2141
		[SRDescription("ProfessionalColorsImageMarginGradientEndDescr")]
		public virtual Color ImageMarginGradientEnd
		{
			get
			{
				if (!this.usingSystemColors)
				{
					return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradVertEnd);
				}
				return SystemColors.Control;
			}
		}

		/// <summary>Gets the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</returns>
		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x0600336E RID: 13166 RVA: 0x000E3F59 File Offset: 0x000E2159
		[SRDescription("ProfessionalColorsImageMarginRevealedGradientBeginDescr")]
		public virtual Color ImageMarginRevealedGradientBegin
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedBegin);
			}
		}

		/// <summary>Gets the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</returns>
		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x0600336F RID: 13167 RVA: 0x000E3F63 File Offset: 0x000E2163
		[SRDescription("ProfessionalColorsImageMarginRevealedGradientMiddleDescr")]
		public virtual Color ImageMarginRevealedGradientMiddle
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedMiddle);
			}
		}

		/// <summary>Gets the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</returns>
		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06003370 RID: 13168 RVA: 0x000E3F6D File Offset: 0x000E216D
		[SRDescription("ProfessionalColorsImageMarginRevealedGradientEndDescr")]
		public virtual Color ImageMarginRevealedGradientEnd
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedEnd);
			}
		}

		/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip" />.</returns>
		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06003371 RID: 13169 RVA: 0x000E3F77 File Offset: 0x000E2177
		[SRDescription("ProfessionalColorsMenuStripGradientBeginDescr")]
		public virtual Color MenuStripGradientBegin
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzBegin);
			}
		}

		/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip" />.</returns>
		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06003372 RID: 13170 RVA: 0x000E3F81 File Offset: 0x000E2181
		[SRDescription("ProfessionalColorsMenuStripGradientEndDescr")]
		public virtual Color MenuStripGradientEnd
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzEnd);
			}
		}

		/// <summary>Gets the solid color to use when a <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> other than the top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color to use when a <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> other than the top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</returns>
		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06003373 RID: 13171 RVA: 0x000E3F8B File Offset: 0x000E218B
		[SRDescription("ProfessionalColorsMenuItemSelectedDescr")]
		public virtual Color MenuItemSelected
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseOver);
			}
		}

		/// <summary>Gets the border color to use with a <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with a <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</returns>
		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06003374 RID: 13172 RVA: 0x000E3F95 File Offset: 0x000E2195
		[SRDescription("ProfessionalColorsMenuItemBorderDescr")]
		public virtual Color MenuItemBorder
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrSelected);
			}
		}

		/// <summary>Gets the color that is the border color to use on a <see cref="T:System.Windows.Forms.MenuStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use on a <see cref="T:System.Windows.Forms.MenuStrip" />.</returns>
		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06003375 RID: 13173 RVA: 0x000E3F9E File Offset: 0x000E219E
		[SRDescription("ProfessionalColorsMenuBorderDescr")]
		public virtual Color MenuBorder
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBMenuBdrOuter);
			}
		}

		/// <summary>Gets the starting color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</returns>
		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06003376 RID: 13174 RVA: 0x000E3EC9 File Offset: 0x000E20C9
		[SRDescription("ProfessionalColorsMenuItemSelectedGradientBeginDescr")]
		public virtual Color MenuItemSelectedGradientBegin
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverBegin);
			}
		}

		/// <summary>Gets the end color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</returns>
		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06003377 RID: 13175 RVA: 0x000E3EDD File Offset: 0x000E20DD
		[SRDescription("ProfessionalColorsMenuItemSelectedGradientEndDescr")]
		public virtual Color MenuItemSelectedGradientEnd
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverEnd);
			}
		}

		/// <summary>Gets the starting color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed.</returns>
		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06003378 RID: 13176 RVA: 0x000E3FA8 File Offset: 0x000E21A8
		[SRDescription("ProfessionalColorsMenuItemPressedGradientBeginDescr")]
		public virtual Color MenuItemPressedGradientBegin
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuTitleBkgdBegin);
			}
		}

		/// <summary>Gets the middle color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed.</returns>
		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06003379 RID: 13177 RVA: 0x000E3F63 File Offset: 0x000E2163
		[SRDescription("ProfessionalColorsMenuItemPressedGradientMiddleDescr")]
		public virtual Color MenuItemPressedGradientMiddle
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedMiddle);
			}
		}

		/// <summary>Gets the end color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed.</returns>
		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x0600337A RID: 13178 RVA: 0x000E3FB2 File Offset: 0x000E21B2
		[SRDescription("ProfessionalColorsMenuItemPressedGradientEndDescr")]
		public virtual Color MenuItemPressedGradientEnd
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuTitleBkgdEnd);
			}
		}

		/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</returns>
		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x0600337B RID: 13179 RVA: 0x000E3F77 File Offset: 0x000E2177
		[SRDescription("ProfessionalColorsRaftingContainerGradientBeginDescr")]
		public virtual Color RaftingContainerGradientBegin
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzBegin);
			}
		}

		/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</returns>
		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x0600337C RID: 13180 RVA: 0x000E3F81 File Offset: 0x000E2181
		[SRDescription("ProfessionalColorsRaftingContainerGradientEndDescr")]
		public virtual Color RaftingContainerGradientEnd
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzEnd);
			}
		}

		/// <summary>Gets the color to use to for shadow effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color to use to for shadow effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</returns>
		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x0600337D RID: 13181 RVA: 0x000E3FBC File Offset: 0x000E21BC
		[SRDescription("ProfessionalColorsSeparatorDarkDescr")]
		public virtual Color SeparatorDark
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBSplitterLine);
			}
		}

		/// <summary>Gets the color to use to for highlight effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color to use to for highlight effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</returns>
		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x0600337E RID: 13182 RVA: 0x000E3FC6 File Offset: 0x000E21C6
		[SRDescription("ProfessionalColorsSeparatorLightDescr")]
		public virtual Color SeparatorLight
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBSplitterLineLight);
			}
		}

		/// <summary>Gets the starting color of the gradient used on the <see cref="T:System.Windows.Forms.StatusStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used on the <see cref="T:System.Windows.Forms.StatusStrip" />.</returns>
		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x0600337F RID: 13183 RVA: 0x000E3F77 File Offset: 0x000E2177
		[SRDescription("ProfessionalColorsStatusStripGradientBeginDescr")]
		public virtual Color StatusStripGradientBegin
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzBegin);
			}
		}

		/// <summary>Gets the end color of the gradient used on the <see cref="T:System.Windows.Forms.StatusStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used on the <see cref="T:System.Windows.Forms.StatusStrip" />.</returns>
		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06003380 RID: 13184 RVA: 0x000E3F81 File Offset: 0x000E2181
		[SRDescription("ProfessionalColorsStatusStripGradientEndDescr")]
		public virtual Color StatusStripGradientEnd
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzEnd);
			}
		}

		/// <summary>Gets the border color to use on the bottom edge of the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use on the bottom edge of the <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06003381 RID: 13185 RVA: 0x000E3FD0 File Offset: 0x000E21D0
		[SRDescription("ProfessionalColorsToolStripBorderDescr")]
		public virtual Color ToolStripBorder
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBShadow);
			}
		}

		/// <summary>Gets the solid background color of the <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid background color of the <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</returns>
		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06003382 RID: 13186 RVA: 0x000E3FDA File Offset: 0x000E21DA
		[SRDescription("ProfessionalColorsToolStripDropDownBackgroundDescr")]
		public virtual Color ToolStripDropDownBackground
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBMenuBkgd);
			}
		}

		/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</returns>
		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06003383 RID: 13187 RVA: 0x000E3F2D File Offset: 0x000E212D
		[SRDescription("ProfessionalColorsToolStripGradientBeginDescr")]
		public virtual Color ToolStripGradientBegin
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradVertBegin);
			}
		}

		/// <summary>Gets the middle color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</returns>
		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x06003384 RID: 13188 RVA: 0x000E3F37 File Offset: 0x000E2137
		[SRDescription("ProfessionalColorsToolStripGradientMiddleDescr")]
		public virtual Color ToolStripGradientMiddle
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradVertMiddle);
			}
		}

		/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</returns>
		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06003385 RID: 13189 RVA: 0x000E3FE4 File Offset: 0x000E21E4
		[SRDescription("ProfessionalColorsToolStripGradientEndDescr")]
		public virtual Color ToolStripGradientEnd
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradVertEnd);
			}
		}

		/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</returns>
		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06003386 RID: 13190 RVA: 0x000E3F77 File Offset: 0x000E2177
		[SRDescription("ProfessionalColorsToolStripContentPanelGradientBeginDescr")]
		public virtual Color ToolStripContentPanelGradientBegin
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzBegin);
			}
		}

		/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</returns>
		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06003387 RID: 13191 RVA: 0x000E3F81 File Offset: 0x000E2181
		[SRDescription("ProfessionalColorsToolStripContentPanelGradientEndDescr")]
		public virtual Color ToolStripContentPanelGradientEnd
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzEnd);
			}
		}

		/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</returns>
		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06003388 RID: 13192 RVA: 0x000E3F77 File Offset: 0x000E2177
		[SRDescription("ProfessionalColorsToolStripPanelGradientBeginDescr")]
		public virtual Color ToolStripPanelGradientBegin
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzBegin);
			}
		}

		/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</returns>
		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06003389 RID: 13193 RVA: 0x000E3F81 File Offset: 0x000E2181
		[SRDescription("ProfessionalColorsToolStripPanelGradientEndDescr")]
		public virtual Color ToolStripPanelGradientEnd
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzEnd);
			}
		}

		/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</returns>
		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x0600338A RID: 13194 RVA: 0x000E3FEE File Offset: 0x000E21EE
		[SRDescription("ProfessionalColorsOverflowButtonGradientBeginDescr")]
		public virtual Color OverflowButtonGradientBegin
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsBegin);
			}
		}

		/// <summary>Gets the middle color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</returns>
		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x0600338B RID: 13195 RVA: 0x000E3FF8 File Offset: 0x000E21F8
		[SRDescription("ProfessionalColorsOverflowButtonGradientMiddleDescr")]
		public virtual Color OverflowButtonGradientMiddle
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMiddle);
			}
		}

		/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</returns>
		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x0600338C RID: 13196 RVA: 0x000E4002 File Offset: 0x000E2202
		[SRDescription("ProfessionalColorsOverflowButtonGradientEndDescr")]
		public virtual Color OverflowButtonGradientEnd
		{
			get
			{
				return this.FromKnownColor(ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsEnd);
			}
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x0600338D RID: 13197 RVA: 0x000E400C File Offset: 0x000E220C
		internal Color ComboBoxButtonGradientBegin
		{
			get
			{
				return this.MenuItemPressedGradientBegin;
			}
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x0600338E RID: 13198 RVA: 0x000E4014 File Offset: 0x000E2214
		internal Color ComboBoxButtonGradientEnd
		{
			get
			{
				return this.MenuItemPressedGradientEnd;
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x0600338F RID: 13199 RVA: 0x000E401C File Offset: 0x000E221C
		internal Color ComboBoxButtonSelectedGradientBegin
		{
			get
			{
				return this.MenuItemSelectedGradientBegin;
			}
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06003390 RID: 13200 RVA: 0x000E4024 File Offset: 0x000E2224
		internal Color ComboBoxButtonSelectedGradientEnd
		{
			get
			{
				return this.MenuItemSelectedGradientEnd;
			}
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06003391 RID: 13201 RVA: 0x000E402C File Offset: 0x000E222C
		internal Color ComboBoxButtonPressedGradientBegin
		{
			get
			{
				return this.ButtonPressedGradientBegin;
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06003392 RID: 13202 RVA: 0x000E4034 File Offset: 0x000E2234
		internal Color ComboBoxButtonPressedGradientEnd
		{
			get
			{
				return this.ButtonPressedGradientEnd;
			}
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06003393 RID: 13203 RVA: 0x000E403C File Offset: 0x000E223C
		internal Color ComboBoxButtonOnOverflow
		{
			get
			{
				return this.ToolStripDropDownBackground;
			}
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06003394 RID: 13204 RVA: 0x000E4044 File Offset: 0x000E2244
		internal Color ComboBoxBorder
		{
			get
			{
				return this.ButtonSelectedHighlightBorder;
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06003395 RID: 13205 RVA: 0x000E4044 File Offset: 0x000E2244
		internal Color TextBoxBorder
		{
			get
			{
				return this.ButtonSelectedHighlightBorder;
			}
		}

		// Token: 0x06003396 RID: 13206 RVA: 0x000E404C File Offset: 0x000E224C
		private static Color GetAlphaBlendedColor(Graphics g, Color src, Color dest, int alpha)
		{
			int num = ((int)src.R * alpha + (255 - alpha) * (int)dest.R) / 255;
			int num2 = ((int)src.G * alpha + (255 - alpha) * (int)dest.G) / 255;
			int num3 = ((int)src.B * alpha + (255 - alpha) * (int)dest.B) / 255;
			int num4 = ((int)src.A * alpha + (255 - alpha) * (int)dest.A) / 255;
			if (g == null)
			{
				return Color.FromArgb(num4, num, num2, num3);
			}
			return g.GetNearestColor(Color.FromArgb(num4, num, num2, num3));
		}

		// Token: 0x06003397 RID: 13207 RVA: 0x000E40F8 File Offset: 0x000E22F8
		private static Color GetAlphaBlendedColorHighRes(Graphics graphics, Color src, Color dest, int alpha)
		{
			int num;
			int num2;
			if (alpha < 100)
			{
				num = 100 - alpha;
				num2 = 100;
			}
			else
			{
				num = 1000 - alpha;
				num2 = 1000;
			}
			int num3 = (alpha * (int)src.R + num * (int)dest.R + num2 / 2) / num2;
			int num4 = (alpha * (int)src.G + num * (int)dest.G + num2 / 2) / num2;
			int num5 = (alpha * (int)src.B + num * (int)dest.B + num2 / 2) / num2;
			if (graphics == null)
			{
				return Color.FromArgb(num3, num4, num5);
			}
			return graphics.GetNearestColor(Color.FromArgb(num3, num4, num5));
		}

		// Token: 0x06003398 RID: 13208 RVA: 0x000E4198 File Offset: 0x000E2398
		private void InitCommonColors(ref Dictionary<ProfessionalColorTable.KnownColors, Color> rgbTable)
		{
			if (!DisplayInformation.LowResolution)
			{
				using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
				{
					rgbTable[ProfessionalColorTable.KnownColors.ButtonPressedHighlight] = ProfessionalColorTable.GetAlphaBlendedColor(graphics, SystemColors.Window, ProfessionalColorTable.GetAlphaBlendedColor(graphics, SystemColors.Highlight, SystemColors.Window, 160), 50);
					rgbTable[ProfessionalColorTable.KnownColors.ButtonCheckedHighlight] = ProfessionalColorTable.GetAlphaBlendedColor(graphics, SystemColors.Window, ProfessionalColorTable.GetAlphaBlendedColor(graphics, SystemColors.Highlight, SystemColors.Window, 80), 20);
					rgbTable[ProfessionalColorTable.KnownColors.ButtonSelectedHighlight] = rgbTable[ProfessionalColorTable.KnownColors.ButtonCheckedHighlight];
					return;
				}
			}
			rgbTable[ProfessionalColorTable.KnownColors.ButtonPressedHighlight] = SystemColors.Highlight;
			rgbTable[ProfessionalColorTable.KnownColors.ButtonCheckedHighlight] = SystemColors.ControlLight;
			rgbTable[ProfessionalColorTable.KnownColors.ButtonSelectedHighlight] = SystemColors.ControlLight;
		}

		// Token: 0x06003399 RID: 13209 RVA: 0x000E4278 File Offset: 0x000E2478
		internal void InitSystemColors(ref Dictionary<ProfessionalColorTable.KnownColors, Color> rgbTable)
		{
			this.usingSystemColors = true;
			this.InitCommonColors(ref rgbTable);
			Color buttonFace = SystemColors.ButtonFace;
			Color buttonShadow = SystemColors.ButtonShadow;
			Color highlight = SystemColors.Highlight;
			Color window = SystemColors.Window;
			Color empty = Color.Empty;
			Color controlText = SystemColors.ControlText;
			Color buttonHighlight = SystemColors.ButtonHighlight;
			Color grayText = SystemColors.GrayText;
			Color highlightText = SystemColors.HighlightText;
			Color windowText = SystemColors.WindowText;
			Color color = buttonFace;
			Color color2 = buttonFace;
			Color color3 = buttonFace;
			Color color4 = highlight;
			Color color5 = highlight;
			bool lowResolution = DisplayInformation.LowResolution;
			bool highContrast = DisplayInformation.HighContrast;
			if (lowResolution)
			{
				color4 = window;
			}
			else if (!highContrast)
			{
				color = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, buttonFace, window, 23);
				color2 = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, buttonFace, window, 50);
				color3 = SystemColors.ButtonFace;
				color4 = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, highlight, window, 30);
				color5 = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, highlight, window, 50);
			}
			if (lowResolution || highContrast)
			{
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBkgd] = buttonFace;
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelectedMouseOver] = SystemColors.ControlLight;
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDragHandle] = controlText;
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzEnd] = buttonFace;
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsBegin] = buttonShadow;
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMiddle] = buttonShadow;
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedBegin] = buttonShadow;
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedMiddle] = buttonShadow;
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedEnd] = buttonShadow;
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuBdrOuter] = controlText;
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuBkgd] = window;
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBSplitterLine] = buttonShadow;
			}
			else
			{
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBkgd] = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, window, buttonFace, 165);
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelectedMouseOver] = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, highlight, window, 50);
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDragHandle] = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, buttonShadow, window, 75);
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzEnd] = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, buttonFace, window, 205);
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsBegin] = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, buttonFace, window, 70);
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMiddle] = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, buttonFace, window, 90);
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedBegin] = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, buttonFace, window, 40);
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedMiddle] = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, buttonFace, window, 70);
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedEnd] = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, buttonFace, window, 90);
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuBdrOuter] = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, controlText, buttonShadow, 20);
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuBkgd] = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, buttonFace, window, 143);
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBSplitterLine] = ProfessionalColorTable.GetAlphaBlendedColorHighRes(null, buttonShadow, window, 70);
			}
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelected] = (lowResolution ? SystemColors.ControlLight : highlight);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBdrOuterDocked] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBdrOuterDocked] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBdrOuterFloating] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrMouseDown] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrMouseOver] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrSelected] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrSelectedMouseOver] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgd] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdLight] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseDown] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseOver] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlText] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextDisabled] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextLight] = grayText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextMouseDown] = highlightText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextMouseOver] = windowText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDockSeparatorLine] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDragHandleShadow] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDropDownArrow] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzBegin] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverEnd] = color4;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverBegin] = color4;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverMiddle] = color4;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsEnd] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverBegin] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverEnd] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverMiddle] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedBegin] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedEnd] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedMiddle] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedBegin] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedEnd] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedMiddle] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertBegin] = color;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertMiddle] = color2;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertEnd] = color3;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownBegin] = color5;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownMiddle] = color5;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownEnd] = color5;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuTitleBkgdBegin] = color;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuTitleBkgdEnd] = color2;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBIconDisabledDark] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBIconDisabledLight] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBLabelBkgnd] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBLowColorIconDisabled] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMainMenuBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuCtlText] = windowText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuCtlTextDisabled] = grayText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuIconBkgd] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuIconBkgdDropped] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuShadow] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuSplitArrow] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBOptionsButtonShadow] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBShadow] = rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBkgd];
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBSplitterLineLight] = buttonHighlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTearOffHandle] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTearOffHandleMouseOver] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTitleBkgd] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTitleText] = buttonHighlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDisabledFocuslessHighlightedText] = grayText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDisabledHighlightedText] = grayText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDlgGroupBoxText] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdr] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDark] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseDown] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseOver] = SystemColors.MenuText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLight] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseDown] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseOver] = SystemColors.MenuText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseDown] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseOver] = SystemColors.MenuText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrSelected] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdMouseDown] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdMouseOver] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdSelected] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabText] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseDown] = highlightText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseOver] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextSelected] = windowText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabBkgd] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabText] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabText] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabTextDisabled] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabTextDisabled] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabBkgd] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabText] = buttonHighlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabText] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabBkgdMouseDown] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabBkgdMouseOver] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabTextMouseDown] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabTextMouseOver] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedBkgd] = SystemColors.InactiveCaption;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedText] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedText] = SystemColors.InactiveCaptionText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderBdr] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderBkgd] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBdr] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBkgdSelected] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderSeeThroughSelection] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPDarkBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPDarkBkgd] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentDarkBkgd] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentLightBkgd] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentText] = windowText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentTextDisabled] = grayText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderDarkBkgd] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderLightBkgd] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderText] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderText] = windowText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupline] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupline] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPHyperlink] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPLightBkgd] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrHyperlink] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrHyperlinkFollowed] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIBdr] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIBdr] = windowText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradBegin] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradBegin] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradEnd] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradMiddle] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradMiddle] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIText] = windowText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrListHeaderArrow] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrNetLookBkgnd] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOABBkgd] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOBBkgdBdr] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOBBkgdBdrContrast] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGMDIParentWorkspaceBkgd] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerActiveBkgd] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerBdr] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerInactiveBkgd] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabBoxBdr] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabBoxBdrHighlight] = buttonHighlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabStopTicks] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerText] = windowText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGTaskPaneGroupBoxHeaderBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGWorkspaceBkgd] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFlagNone] = buttonHighlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarDark] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarLight] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarText] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGridlines] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupLine] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupNested] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupShaded] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupText] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKIconBar] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKInfoBarBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKInfoBarText] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKPreviewPaneLabelText] = windowText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKTodayIndicatorDark] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKTodayIndicatorLight] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBActionDividerLine] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBButtonDark] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBButtonLight] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBButtonLight] = buttonHighlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBDarkOutline] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBFoldersBackground] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBHoverButtonDark] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBHoverButtonLight] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBLabelText] = windowText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBPressedButtonDark] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBPressedButtonLight] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSelectedButtonDark] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSelectedButtonLight] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSplitterDark] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSplitterLight] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSplitterLight] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPlacesBarBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabAreaBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabBdr] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabInactiveBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabText] = windowText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrActiveSelected] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrActiveSelectedMouseOver] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrInactiveSelected] = grayText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrMouseOver] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPubPrintDocScratchPageBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPubWebDocScratchPageBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrSBBdr] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrScrollbarBkgd] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrToastGradBegin] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrToastGradEnd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrInnerDocked] = empty;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrOuterDocked] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrOuterFloating] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBkgd] = window;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdr] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdrDefault] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdrDefault] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdrDisabled] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBkgd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBkgdDisabled] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlText] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlTextDisabled] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlTextMouseDown] = highlightText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPGroupline] = buttonShadow;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPInfoTipBkgd] = SystemColors.Info;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPInfoTipText] = SystemColors.InfoText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPNavBarBkgnd] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPText] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPText] = windowText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTextDisabled] = grayText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleBkgdActive] = highlight;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleBkgdInactive] = buttonFace;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleTextActive] = highlightText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleTextInactive] = controlText;
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrXLFormulaBarBkgd] = buttonFace;
		}

		// Token: 0x0600339A RID: 13210 RVA: 0x000E4EA0 File Offset: 0x000E30A0
		internal void InitOliveLunaColors(ref Dictionary<ProfessionalColorTable.KnownColors, Color> rgbTable)
		{
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBdrOuterDocked] = Color.FromArgb(81, 94, 51);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBdrOuterDocked] = Color.FromArgb(81, 94, 51);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBdrOuterFloating] = Color.FromArgb(116, 134, 94);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBkgd] = Color.FromArgb(209, 222, 173);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrMouseDown] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrMouseOver] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrSelected] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrSelectedMouseOver] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgd] = Color.FromArgb(209, 222, 173);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdLight] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseDown] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelected] = Color.FromArgb(255, 192, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelectedMouseOver] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextDisabled] = Color.FromArgb(141, 141, 141);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextLight] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextMouseDown] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDockSeparatorLine] = Color.FromArgb(96, 119, 66);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDragHandle] = Color.FromArgb(81, 94, 51);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDragHandleShadow] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDropDownArrow] = Color.FromArgb(236, 233, 216);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzBegin] = Color.FromArgb(217, 217, 167);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzEnd] = Color.FromArgb(242, 241, 228);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedBegin] = Color.FromArgb(230, 230, 209);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedEnd] = Color.FromArgb(160, 177, 116);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedMiddle] = Color.FromArgb(186, 201, 143);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuTitleBkgdBegin] = Color.FromArgb(237, 240, 214);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuTitleBkgdEnd] = Color.FromArgb(181, 196, 143);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownBegin] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownEnd] = Color.FromArgb(255, 223, 154);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownMiddle] = Color.FromArgb(255, 177, 109);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverBegin] = Color.FromArgb(255, 255, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverEnd] = Color.FromArgb(255, 203, 136);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverMiddle] = Color.FromArgb(255, 225, 172);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsBegin] = Color.FromArgb(186, 204, 150);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsEnd] = Color.FromArgb(96, 119, 107);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMiddle] = Color.FromArgb(141, 160, 107);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverBegin] = Color.FromArgb(255, 255, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverEnd] = Color.FromArgb(255, 193, 118);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverMiddle] = Color.FromArgb(255, 225, 172);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedBegin] = Color.FromArgb(254, 140, 73);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedEnd] = Color.FromArgb(255, 221, 152);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedMiddle] = Color.FromArgb(255, 184, 116);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedBegin] = Color.FromArgb(255, 223, 154);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedEnd] = Color.FromArgb(255, 166, 76);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedMiddle] = Color.FromArgb(255, 195, 116);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertBegin] = Color.FromArgb(255, 255, 237);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertEnd] = Color.FromArgb(181, 196, 143);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertMiddle] = Color.FromArgb(206, 220, 167);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBIconDisabledDark] = Color.FromArgb(131, 144, 113);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBIconDisabledLight] = Color.FromArgb(243, 244, 240);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBLabelBkgnd] = Color.FromArgb(218, 227, 187);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBLabelBkgnd] = Color.FromArgb(218, 227, 187);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBLowColorIconDisabled] = Color.FromArgb(159, 174, 122);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMainMenuBkgd] = Color.FromArgb(236, 233, 216);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuBdrOuter] = Color.FromArgb(117, 141, 94);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuBkgd] = Color.FromArgb(244, 244, 238);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuCtlText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuCtlTextDisabled] = Color.FromArgb(141, 141, 141);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuIconBkgd] = Color.FromArgb(216, 227, 182);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuIconBkgdDropped] = Color.FromArgb(173, 181, 157);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuIconBkgdDropped] = Color.FromArgb(173, 181, 157);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuShadow] = Color.FromArgb(134, 148, 108);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuSplitArrow] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBOptionsButtonShadow] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBShadow] = Color.FromArgb(96, 128, 88);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBSplitterLine] = Color.FromArgb(96, 128, 88);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBSplitterLineLight] = Color.FromArgb(244, 247, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTearOffHandle] = Color.FromArgb(197, 212, 159);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTearOffHandleMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTitleBkgd] = Color.FromArgb(116, 134, 94);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTitleText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDisabledFocuslessHighlightedText] = Color.FromArgb(172, 168, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDisabledHighlightedText] = Color.FromArgb(220, 224, 208);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDlgGroupBoxText] = Color.FromArgb(153, 84, 10);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdr] = Color.FromArgb(96, 119, 107);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDark] = Color.FromArgb(176, 194, 140);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseDown] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseOver] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseOver] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseOver] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLight] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseDown] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseOver] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseOver] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseOver] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseDown] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseOver] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseOver] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseOver] = Color.FromArgb(63, 93, 56);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrSelected] = Color.FromArgb(96, 128, 88);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgd] = Color.FromArgb(218, 227, 187);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdMouseDown] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdSelected] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseDown] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextSelected] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabBkgd] = Color.FromArgb(218, 227, 187);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabBkgd] = Color.FromArgb(218, 227, 187);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabTextDisabled] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabTextDisabled] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabBkgd] = Color.FromArgb(183, 198, 145);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabBkgd] = Color.FromArgb(183, 198, 145);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabBkgdMouseDown] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabBkgdMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabTextMouseDown] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedBkgd] = Color.FromArgb(236, 233, 216);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedBkgd] = Color.FromArgb(236, 233, 216);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderBdr] = Color.FromArgb(191, 191, 223);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderBkgd] = Color.FromArgb(239, 235, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBdr] = Color.FromArgb(126, 125, 104);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBkgd] = Color.FromArgb(239, 235, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBkgdSelected] = Color.FromArgb(255, 192, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderSeeThroughSelection] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPDarkBkgd] = Color.FromArgb(159, 171, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPDarkBkgd] = Color.FromArgb(159, 171, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentDarkBkgd] = Color.FromArgb(217, 227, 187);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentLightBkgd] = Color.FromArgb(230, 234, 208);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentTextDisabled] = Color.FromArgb(150, 145, 133);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderDarkBkgd] = Color.FromArgb(161, 176, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderLightBkgd] = Color.FromArgb(210, 223, 174);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderText] = Color.FromArgb(90, 107, 70);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderText] = Color.FromArgb(90, 107, 70);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupline] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupline] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPHyperlink] = Color.FromArgb(0, 61, 178);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPLightBkgd] = Color.FromArgb(243, 242, 231);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrHyperlink] = Color.FromArgb(0, 61, 178);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrHyperlinkFollowed] = Color.FromArgb(170, 0, 170);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIBdr] = Color.FromArgb(96, 128, 88);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIBdr] = Color.FromArgb(96, 128, 88);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradBegin] = Color.FromArgb(217, 217, 167);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradBegin] = Color.FromArgb(217, 217, 167);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradEnd] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradMiddle] = Color.FromArgb(242, 241, 228);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradMiddle] = Color.FromArgb(242, 241, 228);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrListHeaderArrow] = Color.FromArgb(172, 168, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrNetLookBkgnd] = Color.FromArgb(255, 255, 237);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOABBkgd] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOBBkgdBdr] = Color.FromArgb(211, 211, 211);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOBBkgdBdrContrast] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGMDIParentWorkspaceBkgd] = Color.FromArgb(151, 160, 123);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerActiveBkgd] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerBdr] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerBkgd] = Color.FromArgb(226, 231, 191);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerInactiveBkgd] = Color.FromArgb(171, 192, 138);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabBoxBdr] = Color.FromArgb(117, 141, 94);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabBoxBdrHighlight] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabStopTicks] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGTaskPaneGroupBoxHeaderBkgd] = Color.FromArgb(218, 227, 187);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGWorkspaceBkgd] = Color.FromArgb(151, 160, 123);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFlagNone] = Color.FromArgb(242, 240, 228);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarDark] = Color.FromArgb(96, 119, 66);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarLight] = Color.FromArgb(175, 192, 130);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGridlines] = Color.FromArgb(234, 233, 225);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupLine] = Color.FromArgb(181, 196, 143);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupNested] = Color.FromArgb(253, 238, 201);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupShaded] = Color.FromArgb(175, 186, 145);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupText] = Color.FromArgb(115, 137, 84);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKIconBar] = Color.FromArgb(253, 247, 233);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKInfoBarBkgd] = Color.FromArgb(151, 160, 123);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKInfoBarText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKPreviewPaneLabelText] = Color.FromArgb(151, 160, 123);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKTodayIndicatorDark] = Color.FromArgb(187, 85, 3);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKTodayIndicatorLight] = Color.FromArgb(251, 200, 79);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBActionDividerLine] = Color.FromArgb(200, 212, 172);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBButtonDark] = Color.FromArgb(176, 191, 138);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBButtonLight] = Color.FromArgb(234, 240, 207);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBButtonLight] = Color.FromArgb(234, 240, 207);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBDarkOutline] = Color.FromArgb(96, 128, 88);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBFoldersBackground] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBHoverButtonDark] = Color.FromArgb(247, 190, 87);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBHoverButtonLight] = Color.FromArgb(255, 255, 220);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBLabelText] = Color.FromArgb(50, 69, 105);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBPressedButtonDark] = Color.FromArgb(248, 222, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBPressedButtonLight] = Color.FromArgb(232, 127, 8);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSelectedButtonDark] = Color.FromArgb(238, 147, 17);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSelectedButtonLight] = Color.FromArgb(251, 230, 148);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSplitterDark] = Color.FromArgb(64, 81, 59);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSplitterLight] = Color.FromArgb(120, 142, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSplitterLight] = Color.FromArgb(120, 142, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPlacesBarBkgd] = Color.FromArgb(236, 233, 216);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabAreaBkgd] = Color.FromArgb(242, 240, 228);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabBdr] = Color.FromArgb(96, 128, 88);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabInactiveBkgd] = Color.FromArgb(206, 220, 167);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrActiveSelected] = Color.FromArgb(107, 129, 107);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrActiveSelectedMouseOver] = Color.FromArgb(107, 129, 107);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrInactiveSelected] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrMouseOver] = Color.FromArgb(107, 129, 107);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPubPrintDocScratchPageBkgd] = Color.FromArgb(151, 160, 123);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPubWebDocScratchPageBkgd] = Color.FromArgb(193, 198, 176);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrSBBdr] = Color.FromArgb(211, 211, 211);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrScrollbarBkgd] = Color.FromArgb(249, 249, 247);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrToastGradBegin] = Color.FromArgb(237, 242, 212);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrToastGradEnd] = Color.FromArgb(191, 206, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrInnerDocked] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrOuterDocked] = Color.FromArgb(242, 241, 228);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrOuterFloating] = Color.FromArgb(116, 134, 94);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBkgd] = Color.FromArgb(243, 242, 231);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdr] = Color.FromArgb(164, 185, 127);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdrDefault] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdrDefault] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdrDisabled] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBkgd] = Color.FromArgb(197, 212, 159);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBkgdDisabled] = Color.FromArgb(222, 222, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlTextDisabled] = Color.FromArgb(172, 168, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlTextMouseDown] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPGroupline] = Color.FromArgb(188, 187, 177);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPInfoTipBkgd] = Color.FromArgb(255, 255, 204);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPInfoTipText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPNavBarBkgnd] = Color.FromArgb(116, 134, 94);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTextDisabled] = Color.FromArgb(172, 168, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleBkgdActive] = Color.FromArgb(216, 227, 182);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleBkgdInactive] = Color.FromArgb(188, 205, 131);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleTextActive] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleTextInactive] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrXLFormulaBarBkgd] = Color.FromArgb(217, 217, 167);
		}

		// Token: 0x0600339B RID: 13211 RVA: 0x000E67A0 File Offset: 0x000E49A0
		internal void InitSilverLunaColors(ref Dictionary<ProfessionalColorTable.KnownColors, Color> rgbTable)
		{
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBdrOuterDocked] = Color.FromArgb(173, 174, 193);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBdrOuterFloating] = Color.FromArgb(122, 121, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBkgd] = Color.FromArgb(219, 218, 228);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrMouseDown] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrMouseOver] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrSelected] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrSelectedMouseOver] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgd] = Color.FromArgb(219, 218, 228);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdLight] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseDown] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelected] = Color.FromArgb(255, 192, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelectedMouseOver] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextDisabled] = Color.FromArgb(141, 141, 141);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextLight] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextMouseDown] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDockSeparatorLine] = Color.FromArgb(110, 109, 143);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDragHandle] = Color.FromArgb(84, 84, 117);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDragHandleShadow] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDropDownArrow] = Color.FromArgb(224, 223, 227);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzBegin] = Color.FromArgb(215, 215, 229);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzEnd] = Color.FromArgb(243, 243, 247);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedBegin] = Color.FromArgb(215, 215, 226);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedEnd] = Color.FromArgb(118, 116, 151);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedMiddle] = Color.FromArgb(184, 185, 202);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuTitleBkgdBegin] = Color.FromArgb(232, 233, 242);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuTitleBkgdEnd] = Color.FromArgb(172, 170, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownBegin] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownEnd] = Color.FromArgb(255, 223, 154);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownMiddle] = Color.FromArgb(255, 177, 109);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverBegin] = Color.FromArgb(255, 255, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverEnd] = Color.FromArgb(255, 203, 136);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverMiddle] = Color.FromArgb(255, 225, 172);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsBegin] = Color.FromArgb(186, 185, 206);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsEnd] = Color.FromArgb(118, 116, 146);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMiddle] = Color.FromArgb(156, 155, 180);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverBegin] = Color.FromArgb(255, 255, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverEnd] = Color.FromArgb(255, 193, 118);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverMiddle] = Color.FromArgb(255, 225, 172);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedBegin] = Color.FromArgb(254, 140, 73);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedEnd] = Color.FromArgb(255, 221, 152);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedMiddle] = Color.FromArgb(255, 184, 116);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedBegin] = Color.FromArgb(255, 223, 154);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedEnd] = Color.FromArgb(255, 166, 76);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedMiddle] = Color.FromArgb(255, 195, 116);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertBegin] = Color.FromArgb(249, 249, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertEnd] = Color.FromArgb(147, 145, 176);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertMiddle] = Color.FromArgb(225, 226, 236);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBIconDisabledDark] = Color.FromArgb(122, 121, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBIconDisabledLight] = Color.FromArgb(247, 245, 249);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBLabelBkgnd] = Color.FromArgb(212, 212, 226);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBLabelBkgnd] = Color.FromArgb(212, 212, 226);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBLowColorIconDisabled] = Color.FromArgb(168, 167, 190);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMainMenuBkgd] = Color.FromArgb(198, 200, 215);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuBdrOuter] = Color.FromArgb(124, 124, 148);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuBkgd] = Color.FromArgb(253, 250, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuCtlText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuCtlTextDisabled] = Color.FromArgb(141, 141, 141);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuIconBkgd] = Color.FromArgb(214, 211, 231);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuIconBkgdDropped] = Color.FromArgb(185, 187, 200);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuIconBkgdDropped] = Color.FromArgb(185, 187, 200);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuShadow] = Color.FromArgb(154, 140, 176);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuSplitArrow] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBOptionsButtonShadow] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBShadow] = Color.FromArgb(124, 124, 148);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBSplitterLine] = Color.FromArgb(110, 109, 143);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBSplitterLineLight] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTearOffHandle] = Color.FromArgb(192, 192, 211);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTearOffHandleMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTitleBkgd] = Color.FromArgb(122, 121, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTitleText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDisabledFocuslessHighlightedText] = Color.FromArgb(172, 168, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDisabledHighlightedText] = Color.FromArgb(59, 59, 63);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDlgGroupBoxText] = Color.FromArgb(7, 70, 213);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdr] = Color.FromArgb(118, 116, 146);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDark] = Color.FromArgb(186, 185, 206);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseDown] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseOver] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseOver] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseOver] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLight] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseDown] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseOver] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseOver] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseOver] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseDown] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseOver] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseOver] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseOver] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrSelected] = Color.FromArgb(124, 124, 148);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgd] = Color.FromArgb(212, 212, 226);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdMouseDown] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdSelected] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseDown] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextSelected] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabBkgd] = Color.FromArgb(212, 212, 226);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabBkgd] = Color.FromArgb(212, 212, 226);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabTextDisabled] = Color.FromArgb(148, 148, 148);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabTextDisabled] = Color.FromArgb(148, 148, 148);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabBkgd] = Color.FromArgb(171, 169, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabBkgd] = Color.FromArgb(171, 169, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabBkgdMouseDown] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabBkgdMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabTextMouseDown] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedBkgd] = Color.FromArgb(224, 223, 227);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedBkgd] = Color.FromArgb(224, 223, 227);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderBdr] = Color.FromArgb(191, 191, 223);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderBkgd] = Color.FromArgb(239, 235, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBdr] = Color.FromArgb(126, 125, 104);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBkgd] = Color.FromArgb(223, 223, 234);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBkgdSelected] = Color.FromArgb(255, 192, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderSeeThroughSelection] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPDarkBkgd] = Color.FromArgb(162, 162, 181);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPDarkBkgd] = Color.FromArgb(162, 162, 181);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentDarkBkgd] = Color.FromArgb(212, 213, 229);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentLightBkgd] = Color.FromArgb(227, 227, 236);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentTextDisabled] = Color.FromArgb(150, 145, 133);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderDarkBkgd] = Color.FromArgb(169, 168, 191);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderLightBkgd] = Color.FromArgb(208, 208, 223);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderText] = Color.FromArgb(92, 91, 121);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderText] = Color.FromArgb(92, 91, 121);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupline] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupline] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPHyperlink] = Color.FromArgb(0, 61, 178);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPLightBkgd] = Color.FromArgb(238, 238, 244);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrHyperlink] = Color.FromArgb(0, 61, 178);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrHyperlinkFollowed] = Color.FromArgb(170, 0, 170);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIBdr] = Color.FromArgb(124, 124, 148);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIBdr] = Color.FromArgb(124, 124, 148);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradBegin] = Color.FromArgb(215, 215, 229);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradBegin] = Color.FromArgb(215, 215, 229);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradEnd] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradMiddle] = Color.FromArgb(243, 243, 247);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradMiddle] = Color.FromArgb(243, 243, 247);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrListHeaderArrow] = Color.FromArgb(172, 168, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrNetLookBkgnd] = Color.FromArgb(249, 249, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOABBkgd] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOBBkgdBdr] = Color.FromArgb(211, 211, 211);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOBBkgdBdrContrast] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGMDIParentWorkspaceBkgd] = Color.FromArgb(155, 154, 179);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerActiveBkgd] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerBdr] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerBkgd] = Color.FromArgb(223, 223, 234);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerInactiveBkgd] = Color.FromArgb(177, 176, 195);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabBoxBdr] = Color.FromArgb(124, 124, 148);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabBoxBdrHighlight] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabStopTicks] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGTaskPaneGroupBoxHeaderBkgd] = Color.FromArgb(212, 212, 226);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGWorkspaceBkgd] = Color.FromArgb(155, 154, 179);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFlagNone] = Color.FromArgb(239, 239, 244);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarDark] = Color.FromArgb(110, 109, 143);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarLight] = Color.FromArgb(168, 167, 191);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGridlines] = Color.FromArgb(234, 233, 225);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupLine] = Color.FromArgb(165, 164, 189);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupNested] = Color.FromArgb(253, 238, 201);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupShaded] = Color.FromArgb(229, 229, 235);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupText] = Color.FromArgb(112, 111, 145);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKIconBar] = Color.FromArgb(253, 247, 233);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKInfoBarBkgd] = Color.FromArgb(155, 154, 179);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKInfoBarText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKPreviewPaneLabelText] = Color.FromArgb(155, 154, 179);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKTodayIndicatorDark] = Color.FromArgb(187, 85, 3);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKTodayIndicatorLight] = Color.FromArgb(251, 200, 79);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBActionDividerLine] = Color.FromArgb(204, 206, 219);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBButtonDark] = Color.FromArgb(147, 145, 176);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBButtonLight] = Color.FromArgb(225, 226, 236);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBButtonLight] = Color.FromArgb(225, 226, 236);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBDarkOutline] = Color.FromArgb(124, 124, 148);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBFoldersBackground] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBHoverButtonDark] = Color.FromArgb(247, 190, 87);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBHoverButtonLight] = Color.FromArgb(255, 255, 220);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBLabelText] = Color.FromArgb(50, 69, 105);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBPressedButtonDark] = Color.FromArgb(248, 222, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBPressedButtonLight] = Color.FromArgb(232, 127, 8);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSelectedButtonDark] = Color.FromArgb(238, 147, 17);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSelectedButtonLight] = Color.FromArgb(251, 230, 148);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSplitterDark] = Color.FromArgb(110, 109, 143);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSplitterLight] = Color.FromArgb(168, 167, 191);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSplitterLight] = Color.FromArgb(168, 167, 191);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPlacesBarBkgd] = Color.FromArgb(224, 223, 227);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabAreaBkgd] = Color.FromArgb(243, 243, 247);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabBdr] = Color.FromArgb(124, 124, 148);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabInactiveBkgd] = Color.FromArgb(215, 215, 229);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrActiveSelected] = Color.FromArgb(142, 142, 170);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrActiveSelectedMouseOver] = Color.FromArgb(142, 142, 170);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrInactiveSelected] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrMouseOver] = Color.FromArgb(142, 142, 170);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPubPrintDocScratchPageBkgd] = Color.FromArgb(155, 154, 179);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPubWebDocScratchPageBkgd] = Color.FromArgb(195, 195, 210);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrSBBdr] = Color.FromArgb(236, 234, 218);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrScrollbarBkgd] = Color.FromArgb(247, 247, 249);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrToastGradBegin] = Color.FromArgb(239, 239, 247);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrToastGradEnd] = Color.FromArgb(179, 178, 204);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrInnerDocked] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrOuterDocked] = Color.FromArgb(243, 243, 247);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrOuterFloating] = Color.FromArgb(122, 121, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBkgd] = Color.FromArgb(238, 238, 244);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdr] = Color.FromArgb(165, 172, 178);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdrDefault] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdrDefault] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdrDisabled] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBkgd] = Color.FromArgb(192, 192, 211);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBkgdDisabled] = Color.FromArgb(222, 222, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlTextDisabled] = Color.FromArgb(172, 168, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlTextMouseDown] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPGroupline] = Color.FromArgb(161, 160, 187);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPInfoTipBkgd] = Color.FromArgb(255, 255, 204);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPInfoTipText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPNavBarBkgnd] = Color.FromArgb(122, 121, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTextDisabled] = Color.FromArgb(172, 168, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleBkgdActive] = Color.FromArgb(184, 188, 234);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleBkgdInactive] = Color.FromArgb(198, 198, 217);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleTextActive] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleTextInactive] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrXLFormulaBarBkgd] = Color.FromArgb(215, 215, 229);
		}

		// Token: 0x0600339C RID: 13212 RVA: 0x000E8090 File Offset: 0x000E6290
		private void InitRoyaleColors(ref Dictionary<ProfessionalColorTable.KnownColors, Color> rgbTable)
		{
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBkgd] = Color.FromArgb(238, 237, 240);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDragHandle] = Color.FromArgb(189, 188, 191);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBSplitterLine] = Color.FromArgb(193, 193, 196);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTitleBkgd] = Color.FromArgb(167, 166, 170);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTitleText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBdrOuterFloating] = Color.FromArgb(142, 141, 145);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBdrOuterDocked] = Color.FromArgb(235, 233, 237);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTearOffHandle] = Color.FromArgb(238, 237, 240);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTearOffHandleMouseOver] = Color.FromArgb(194, 207, 229);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgd] = Color.FromArgb(238, 237, 240);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextDisabled] = Color.FromArgb(176, 175, 179);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseOver] = Color.FromArgb(194, 207, 229);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrMouseOver] = Color.FromArgb(51, 94, 168);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseDown] = Color.FromArgb(153, 175, 212);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrMouseDown] = Color.FromArgb(51, 94, 168);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextMouseDown] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelected] = Color.FromArgb(226, 229, 238);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrSelected] = Color.FromArgb(51, 94, 168);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelectedMouseOver] = Color.FromArgb(51, 94, 168);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrSelectedMouseOver] = Color.FromArgb(51, 94, 168);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdLight] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextLight] = Color.FromArgb(167, 166, 170);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMainMenuBkgd] = Color.FromArgb(235, 233, 237);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuBkgd] = Color.FromArgb(252, 252, 252);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuCtlText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuCtlTextDisabled] = Color.FromArgb(193, 193, 196);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuBdrOuter] = Color.FromArgb(134, 133, 136);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuIconBkgd] = Color.FromArgb(238, 237, 240);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuIconBkgdDropped] = Color.FromArgb(228, 226, 230);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuSplitArrow] = Color.FromArgb(167, 166, 170);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBkgd] = Color.FromArgb(245, 244, 246);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPText] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleBkgdActive] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleBkgdInactive] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleTextActive] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleTextInactive] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrOuterFloating] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrOuterDocked] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdr] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlText] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdrDisabled] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlTextDisabled] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBkgdDisabled] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdrDefault] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPGroupline] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrSBBdr] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOBBkgdBdr] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOBBkgdBdrContrast] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOABBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderBdr] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBdr] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderSeeThroughSelection] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBkgdSelected] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBSplitterLineLight] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBShadow] = Color.FromArgb(238, 237, 240);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBOptionsButtonShadow] = Color.FromArgb(245, 244, 246);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPNavBarBkgnd] = Color.FromArgb(193, 193, 196);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrInnerDocked] = Color.FromArgb(245, 244, 246);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBLabelBkgnd] = Color.FromArgb(235, 233, 237);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBIconDisabledLight] = Color.FromArgb(235, 233, 237);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBIconDisabledDark] = Color.FromArgb(167, 166, 170);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBLowColorIconDisabled] = Color.FromArgb(176, 175, 179);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzBegin] = Color.FromArgb(235, 233, 237);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzEnd] = Color.FromArgb(251, 250, 251);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertBegin] = Color.FromArgb(252, 252, 252);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertMiddle] = Color.FromArgb(245, 244, 246);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertEnd] = Color.FromArgb(235, 233, 237);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsBegin] = Color.FromArgb(242, 242, 242);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMiddle] = Color.FromArgb(224, 224, 225);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsEnd] = Color.FromArgb(167, 166, 170);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuTitleBkgdBegin] = Color.FromArgb(252, 252, 252);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuTitleBkgdEnd] = Color.FromArgb(245, 244, 246);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedBegin] = Color.FromArgb(247, 246, 248);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedMiddle] = Color.FromArgb(241, 240, 242);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedEnd] = Color.FromArgb(228, 226, 230);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedBegin] = Color.FromArgb(226, 229, 238);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedMiddle] = Color.FromArgb(226, 229, 238);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedEnd] = Color.FromArgb(226, 229, 238);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverBegin] = Color.FromArgb(194, 207, 229);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverMiddle] = Color.FromArgb(194, 207, 229);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverEnd] = Color.FromArgb(194, 207, 229);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedBegin] = Color.FromArgb(226, 229, 238);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedMiddle] = Color.FromArgb(226, 229, 238);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedEnd] = Color.FromArgb(226, 229, 238);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverBegin] = Color.FromArgb(194, 207, 229);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverMiddle] = Color.FromArgb(194, 207, 229);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverEnd] = Color.FromArgb(194, 207, 229);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownBegin] = Color.FromArgb(153, 175, 212);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownMiddle] = Color.FromArgb(153, 175, 212);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownEnd] = Color.FromArgb(153, 175, 212);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrNetLookBkgnd] = Color.FromArgb(235, 233, 237);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuShadow] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDockSeparatorLine] = Color.FromArgb(51, 94, 168);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDropDownArrow] = Color.FromArgb(235, 233, 237);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGridlines] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupText] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupLine] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupShaded] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupNested] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKIconBar] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFlagNone] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarLight] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarDark] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarText] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBButtonLight] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBButtonDark] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSelectedButtonLight] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSelectedButtonDark] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBHoverButtonLight] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBHoverButtonDark] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBPressedButtonLight] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBPressedButtonDark] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBDarkOutline] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSplitterLight] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSplitterDark] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBActionDividerLine] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBLabelText] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBFoldersBackground] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKTodayIndicatorLight] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKTodayIndicatorDark] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKInfoBarBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKInfoBarText] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKPreviewPaneLabelText] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrHyperlink] = Color.FromArgb(0, 61, 178);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrHyperlinkFollowed] = Color.FromArgb(170, 0, 170);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGWorkspaceBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGMDIParentWorkspaceBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerActiveBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerInactiveBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerText] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabStopTicks] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerBdr] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabBoxBdr] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabBoxBdrHighlight] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrXLFormulaBarBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDragHandleShadow] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGTaskPaneGroupBoxHeaderBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabAreaBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabInactiveBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabBdr] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabText] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrActiveSelected] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrInactiveSelected] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrMouseOver] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrActiveSelectedMouseOver] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDlgGroupBoxText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrScrollbarBkgd] = Color.FromArgb(237, 235, 239);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrListHeaderArrow] = Color.FromArgb(155, 154, 156);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDisabledHighlightedText] = Color.FromArgb(188, 202, 226);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedBkgd] = Color.FromArgb(235, 233, 237);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDisabledFocuslessHighlightedText] = Color.FromArgb(167, 166, 170);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlTextMouseDown] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTextDisabled] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPInfoTipBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPInfoTipText] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabText] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabTextDisabled] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabText] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabBkgdMouseOver] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabTextMouseOver] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabBkgdMouseDown] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabTextMouseDown] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPLightBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPDarkBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderLightBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderDarkBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderText] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentLightBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentDarkBkgd] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentText] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentTextDisabled] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupline] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPHyperlink] = Color.FromArgb(255, 51, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgd] = Color.FromArgb(212, 212, 226);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdr] = Color.FromArgb(118, 116, 146);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLight] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDark] = Color.FromArgb(186, 185, 206);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdSelected] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextSelected] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrSelected] = Color.FromArgb(124, 124, 148);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdMouseOver] = Color.FromArgb(193, 210, 238);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseOver] = Color.FromArgb(49, 106, 197);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseOver] = Color.FromArgb(49, 106, 197);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseOver] = Color.FromArgb(49, 106, 197);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseOver] = Color.FromArgb(49, 106, 197);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdMouseDown] = Color.FromArgb(154, 183, 228);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseDown] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseDown] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseDown] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseDown] = Color.FromArgb(75, 75, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrToastGradBegin] = Color.FromArgb(246, 244, 236);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrToastGradEnd] = Color.FromArgb(179, 178, 204);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradBegin] = Color.FromArgb(236, 233, 216);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradMiddle] = Color.FromArgb(236, 233, 216);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradEnd] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIBdr] = Color.FromArgb(172, 168, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPlacesBarBkgd] = Color.FromArgb(224, 223, 227);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPubPrintDocScratchPageBkgd] = Color.FromArgb(152, 181, 226);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPubWebDocScratchPageBkgd] = Color.FromArgb(193, 210, 238);
		}

		// Token: 0x0600339D RID: 13213 RVA: 0x000E9748 File Offset: 0x000E7948
		internal void InitThemedColors(ref Dictionary<ProfessionalColorTable.KnownColors, Color> rgbTable)
		{
			string colorScheme = VisualStyleInformation.ColorScheme;
			string fileName = Path.GetFileName(VisualStyleInformation.ThemeFilename);
			bool flag = false;
			if (string.Equals("luna.msstyles", fileName, StringComparison.OrdinalIgnoreCase))
			{
				if (colorScheme == "NormalColor")
				{
					this.InitBlueLunaColors(ref rgbTable);
					this.usingSystemColors = false;
					flag = true;
				}
				else if (colorScheme == "HomeStead")
				{
					this.InitOliveLunaColors(ref rgbTable);
					this.usingSystemColors = false;
					flag = true;
				}
				else if (colorScheme == "Metallic")
				{
					this.InitSilverLunaColors(ref rgbTable);
					this.usingSystemColors = false;
					flag = true;
				}
			}
			else if (string.Equals("aero.msstyles", fileName, StringComparison.OrdinalIgnoreCase))
			{
				this.InitSystemColors(ref rgbTable);
				this.usingSystemColors = true;
				flag = true;
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseOver] = rgbTable[ProfessionalColorTable.KnownColors.ButtonSelectedHighlight];
				rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelected] = rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseOver];
			}
			else if (string.Equals("royale.msstyles", fileName, StringComparison.OrdinalIgnoreCase) && (colorScheme == "NormalColor" || colorScheme == "Royale"))
			{
				this.InitRoyaleColors(ref rgbTable);
				this.usingSystemColors = false;
				flag = true;
			}
			if (!flag)
			{
				this.InitSystemColors(ref rgbTable);
				this.usingSystemColors = true;
			}
			this.InitCommonColors(ref rgbTable);
		}

		// Token: 0x0600339E RID: 13214 RVA: 0x000E9874 File Offset: 0x000E7A74
		internal void InitBlueLunaColors(ref Dictionary<ProfessionalColorTable.KnownColors, Color> rgbTable)
		{
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBdrOuterDocked] = Color.FromArgb(196, 205, 218);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBdrOuterDocked] = Color.FromArgb(196, 205, 218);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBdrOuterFloating] = Color.FromArgb(42, 102, 201);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBBkgd] = Color.FromArgb(196, 219, 249);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrMouseDown] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrMouseOver] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrSelected] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBdrSelectedMouseOver] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgd] = Color.FromArgb(196, 219, 249);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdLight] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseDown] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelected] = Color.FromArgb(255, 192, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlBkgdSelectedMouseOver] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextDisabled] = Color.FromArgb(141, 141, 141);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextLight] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextMouseDown] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBCtlTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDockSeparatorLine] = Color.FromArgb(0, 53, 145);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDragHandle] = Color.FromArgb(39, 65, 118);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDragHandleShadow] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBDropDownArrow] = Color.FromArgb(236, 233, 216);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzBegin] = Color.FromArgb(158, 190, 245);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMainMenuHorzEnd] = Color.FromArgb(196, 218, 250);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedBegin] = Color.FromArgb(203, 221, 246);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedEnd] = Color.FromArgb(114, 155, 215);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuIconBkgdDroppedMiddle] = Color.FromArgb(161, 197, 249);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuTitleBkgdBegin] = Color.FromArgb(227, 239, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMenuTitleBkgdEnd] = Color.FromArgb(123, 164, 224);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownBegin] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownEnd] = Color.FromArgb(255, 223, 154);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseDownMiddle] = Color.FromArgb(255, 177, 109);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverBegin] = Color.FromArgb(255, 255, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverEnd] = Color.FromArgb(255, 203, 136);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradMouseOverMiddle] = Color.FromArgb(255, 225, 172);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsBegin] = Color.FromArgb(127, 177, 250);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsEnd] = Color.FromArgb(0, 53, 145);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMiddle] = Color.FromArgb(82, 127, 208);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverBegin] = Color.FromArgb(255, 255, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverEnd] = Color.FromArgb(255, 193, 118);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsMouseOverMiddle] = Color.FromArgb(255, 225, 172);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedBegin] = Color.FromArgb(254, 140, 73);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedEnd] = Color.FromArgb(255, 221, 152);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradOptionsSelectedMiddle] = Color.FromArgb(255, 184, 116);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedBegin] = Color.FromArgb(255, 223, 154);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedEnd] = Color.FromArgb(255, 166, 76);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradSelectedMiddle] = Color.FromArgb(255, 195, 116);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertBegin] = Color.FromArgb(227, 239, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertEnd] = Color.FromArgb(123, 164, 224);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBGradVertMiddle] = Color.FromArgb(203, 225, 252);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBIconDisabledDark] = Color.FromArgb(97, 122, 172);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBIconDisabledLight] = Color.FromArgb(233, 236, 242);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBLabelBkgnd] = Color.FromArgb(186, 211, 245);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBLabelBkgnd] = Color.FromArgb(186, 211, 245);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBLowColorIconDisabled] = Color.FromArgb(109, 150, 208);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMainMenuBkgd] = Color.FromArgb(153, 204, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuBdrOuter] = Color.FromArgb(0, 45, 150);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuBkgd] = Color.FromArgb(246, 246, 246);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuCtlText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuCtlTextDisabled] = Color.FromArgb(141, 141, 141);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuIconBkgd] = Color.FromArgb(203, 225, 252);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuIconBkgdDropped] = Color.FromArgb(172, 183, 201);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuIconBkgdDropped] = Color.FromArgb(172, 183, 201);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuShadow] = Color.FromArgb(95, 130, 234);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBMenuSplitArrow] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBOptionsButtonShadow] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBShadow] = Color.FromArgb(59, 97, 156);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBSplitterLine] = Color.FromArgb(106, 140, 203);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBSplitterLineLight] = Color.FromArgb(241, 249, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTearOffHandle] = Color.FromArgb(169, 199, 240);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTearOffHandleMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTitleBkgd] = Color.FromArgb(42, 102, 201);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrCBTitleText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDisabledFocuslessHighlightedText] = Color.FromArgb(172, 168, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDisabledHighlightedText] = Color.FromArgb(187, 206, 236);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDlgGroupBoxText] = Color.FromArgb(0, 70, 213);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdr] = Color.FromArgb(0, 53, 154);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDark] = Color.FromArgb(117, 166, 241);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseDown] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseOver] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseOver] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrDarkMouseOver] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLight] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseDown] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseOver] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseOver] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrLightMouseOver] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseDown] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseOver] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseOver] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrMouseOver] = Color.FromArgb(0, 0, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBdrSelected] = Color.FromArgb(59, 97, 156);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgd] = Color.FromArgb(186, 211, 245);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdMouseDown] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabBkgdSelected] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseDown] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDocTabTextSelected] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabBkgd] = Color.FromArgb(186, 211, 245);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabBkgd] = Color.FromArgb(186, 211, 245);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabTextDisabled] = Color.FromArgb(94, 94, 94);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWActiveTabTextDisabled] = Color.FromArgb(94, 94, 94);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabBkgd] = Color.FromArgb(129, 169, 226);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabBkgd] = Color.FromArgb(129, 169, 226);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWInactiveTabText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabBkgdMouseDown] = Color.FromArgb(254, 128, 62);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabBkgdMouseOver] = Color.FromArgb(255, 238, 194);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabTextMouseDown] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrDWTabTextMouseOver] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedBkgd] = Color.FromArgb(236, 233, 216);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedBkgd] = Color.FromArgb(236, 233, 216);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrFocuslessHighlightedText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderBdr] = Color.FromArgb(89, 89, 172);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderBkgd] = Color.FromArgb(239, 235, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBdr] = Color.FromArgb(126, 125, 104);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBkgd] = Color.FromArgb(239, 235, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderCellBkgdSelected] = Color.FromArgb(255, 192, 111);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGDHeaderSeeThroughSelection] = Color.FromArgb(191, 191, 223);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPDarkBkgd] = Color.FromArgb(74, 122, 201);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPDarkBkgd] = Color.FromArgb(74, 122, 201);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentDarkBkgd] = Color.FromArgb(185, 208, 241);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentLightBkgd] = Color.FromArgb(221, 236, 254);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupContentTextDisabled] = Color.FromArgb(150, 145, 133);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderDarkBkgd] = Color.FromArgb(101, 143, 224);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderLightBkgd] = Color.FromArgb(196, 219, 249);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderText] = Color.FromArgb(0, 45, 134);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupHeaderText] = Color.FromArgb(0, 45, 134);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupline] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPGroupline] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPHyperlink] = Color.FromArgb(0, 61, 178);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrGSPLightBkgd] = Color.FromArgb(221, 236, 254);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrHyperlink] = Color.FromArgb(0, 61, 178);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrHyperlinkFollowed] = Color.FromArgb(170, 0, 170);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIBdr] = Color.FromArgb(59, 97, 156);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIBdr] = Color.FromArgb(59, 97, 156);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradBegin] = Color.FromArgb(158, 190, 245);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradBegin] = Color.FromArgb(158, 190, 245);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradEnd] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradMiddle] = Color.FromArgb(196, 218, 250);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIGradMiddle] = Color.FromArgb(196, 218, 250);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrJotNavUIText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrListHeaderArrow] = Color.FromArgb(172, 168, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrNetLookBkgnd] = Color.FromArgb(227, 239, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOABBkgd] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOBBkgdBdr] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOBBkgdBdrContrast] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGMDIParentWorkspaceBkgd] = Color.FromArgb(144, 153, 174);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerActiveBkgd] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerBdr] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerBkgd] = Color.FromArgb(216, 231, 252);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerInactiveBkgd] = Color.FromArgb(158, 190, 245);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabBoxBdr] = Color.FromArgb(75, 120, 202);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabBoxBdrHighlight] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerTabStopTicks] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGRulerText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGTaskPaneGroupBoxHeaderBkgd] = Color.FromArgb(186, 211, 245);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOGWorkspaceBkgd] = Color.FromArgb(144, 153, 174);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFlagNone] = Color.FromArgb(242, 240, 228);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarDark] = Color.FromArgb(0, 53, 145);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarLight] = Color.FromArgb(89, 135, 214);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKFolderbarText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGridlines] = Color.FromArgb(234, 233, 225);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupLine] = Color.FromArgb(123, 164, 224);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupNested] = Color.FromArgb(253, 238, 201);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupShaded] = Color.FromArgb(190, 218, 251);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKGroupText] = Color.FromArgb(55, 104, 185);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKIconBar] = Color.FromArgb(253, 247, 233);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKInfoBarBkgd] = Color.FromArgb(144, 153, 174);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKInfoBarText] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKPreviewPaneLabelText] = Color.FromArgb(144, 153, 174);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKTodayIndicatorDark] = Color.FromArgb(187, 85, 3);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKTodayIndicatorLight] = Color.FromArgb(251, 200, 79);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBActionDividerLine] = Color.FromArgb(215, 228, 251);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBButtonDark] = Color.FromArgb(123, 164, 224);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBButtonLight] = Color.FromArgb(203, 225, 252);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBButtonLight] = Color.FromArgb(203, 225, 252);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBDarkOutline] = Color.FromArgb(0, 45, 150);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBFoldersBackground] = Color.FromArgb(255, 255, 255);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBHoverButtonDark] = Color.FromArgb(247, 190, 87);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBHoverButtonLight] = Color.FromArgb(255, 255, 220);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBLabelText] = Color.FromArgb(50, 69, 105);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBPressedButtonDark] = Color.FromArgb(248, 222, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBPressedButtonLight] = Color.FromArgb(232, 127, 8);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSelectedButtonDark] = Color.FromArgb(238, 147, 17);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSelectedButtonLight] = Color.FromArgb(251, 230, 148);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSplitterDark] = Color.FromArgb(0, 53, 145);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSplitterLight] = Color.FromArgb(89, 135, 214);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrOLKWBSplitterLight] = Color.FromArgb(89, 135, 214);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPlacesBarBkgd] = Color.FromArgb(236, 233, 216);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabAreaBkgd] = Color.FromArgb(195, 218, 249);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabBdr] = Color.FromArgb(59, 97, 156);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabInactiveBkgd] = Color.FromArgb(158, 190, 245);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPOutlineThumbnailsPaneTabText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrActiveSelected] = Color.FromArgb(61, 108, 192);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrActiveSelectedMouseOver] = Color.FromArgb(61, 108, 192);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrInactiveSelected] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPPSlideBdrMouseOver] = Color.FromArgb(61, 108, 192);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPubPrintDocScratchPageBkgd] = Color.FromArgb(144, 153, 174);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrPubWebDocScratchPageBkgd] = Color.FromArgb(189, 194, 207);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrSBBdr] = Color.FromArgb(211, 211, 211);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrScrollbarBkgd] = Color.FromArgb(251, 251, 248);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrToastGradBegin] = Color.FromArgb(220, 236, 254);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrToastGradEnd] = Color.FromArgb(167, 197, 238);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrInnerDocked] = Color.FromArgb(185, 212, 249);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrOuterDocked] = Color.FromArgb(196, 218, 250);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBdrOuterFloating] = Color.FromArgb(42, 102, 201);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPBkgd] = Color.FromArgb(221, 236, 254);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdr] = Color.FromArgb(127, 157, 185);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdrDefault] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdrDefault] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBdrDisabled] = Color.FromArgb(128, 128, 128);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBkgd] = Color.FromArgb(169, 199, 240);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlBkgdDisabled] = Color.FromArgb(222, 222, 222);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlTextDisabled] = Color.FromArgb(172, 168, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPCtlTextMouseDown] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPGroupline] = Color.FromArgb(123, 164, 224);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPInfoTipBkgd] = Color.FromArgb(255, 255, 204);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPInfoTipText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPNavBarBkgnd] = Color.FromArgb(74, 122, 201);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPText] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTextDisabled] = Color.FromArgb(172, 168, 153);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleBkgdActive] = Color.FromArgb(123, 164, 224);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleBkgdInactive] = Color.FromArgb(148, 187, 239);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleTextActive] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrWPTitleTextInactive] = Color.FromArgb(0, 0, 0);
			rgbTable[ProfessionalColorTable.KnownColors.msocbvcrXLFormulaBarBkgd] = Color.FromArgb(158, 190, 245);
		}

		// Token: 0x04001EB7 RID: 7863
		private Dictionary<ProfessionalColorTable.KnownColors, Color> professionalRGB;

		// Token: 0x04001EB8 RID: 7864
		private bool usingSystemColors;

		// Token: 0x04001EB9 RID: 7865
		private bool useSystemColors;

		// Token: 0x04001EBA RID: 7866
		private string lastKnownColorScheme = string.Empty;

		// Token: 0x04001EBB RID: 7867
		private const string oliveColorScheme = "HomeStead";

		// Token: 0x04001EBC RID: 7868
		private const string normalColorScheme = "NormalColor";

		// Token: 0x04001EBD RID: 7869
		private const string silverColorScheme = "Metallic";

		// Token: 0x04001EBE RID: 7870
		private const string royaleColorScheme = "Royale";

		// Token: 0x04001EBF RID: 7871
		private const string lunaFileName = "luna.msstyles";

		// Token: 0x04001EC0 RID: 7872
		private const string royaleFileName = "royale.msstyles";

		// Token: 0x04001EC1 RID: 7873
		private const string aeroFileName = "aero.msstyles";

		// Token: 0x04001EC2 RID: 7874
		private object colorFreshnessKey;

		// Token: 0x020007CA RID: 1994
		internal enum KnownColors
		{
			// Token: 0x040041BF RID: 16831
			msocbvcrCBBdrOuterDocked,
			// Token: 0x040041C0 RID: 16832
			msocbvcrCBBdrOuterFloating,
			// Token: 0x040041C1 RID: 16833
			msocbvcrCBBkgd,
			// Token: 0x040041C2 RID: 16834
			msocbvcrCBCtlBdrMouseDown,
			// Token: 0x040041C3 RID: 16835
			msocbvcrCBCtlBdrMouseOver,
			// Token: 0x040041C4 RID: 16836
			msocbvcrCBCtlBdrSelected,
			// Token: 0x040041C5 RID: 16837
			msocbvcrCBCtlBdrSelectedMouseOver,
			// Token: 0x040041C6 RID: 16838
			msocbvcrCBCtlBkgd,
			// Token: 0x040041C7 RID: 16839
			msocbvcrCBCtlBkgdLight,
			// Token: 0x040041C8 RID: 16840
			msocbvcrCBCtlBkgdMouseDown,
			// Token: 0x040041C9 RID: 16841
			msocbvcrCBCtlBkgdMouseOver,
			// Token: 0x040041CA RID: 16842
			msocbvcrCBCtlBkgdSelected,
			// Token: 0x040041CB RID: 16843
			msocbvcrCBCtlBkgdSelectedMouseOver,
			// Token: 0x040041CC RID: 16844
			msocbvcrCBCtlText,
			// Token: 0x040041CD RID: 16845
			msocbvcrCBCtlTextDisabled,
			// Token: 0x040041CE RID: 16846
			msocbvcrCBCtlTextLight,
			// Token: 0x040041CF RID: 16847
			msocbvcrCBCtlTextMouseDown,
			// Token: 0x040041D0 RID: 16848
			msocbvcrCBCtlTextMouseOver,
			// Token: 0x040041D1 RID: 16849
			msocbvcrCBDockSeparatorLine,
			// Token: 0x040041D2 RID: 16850
			msocbvcrCBDragHandle,
			// Token: 0x040041D3 RID: 16851
			msocbvcrCBDragHandleShadow,
			// Token: 0x040041D4 RID: 16852
			msocbvcrCBDropDownArrow,
			// Token: 0x040041D5 RID: 16853
			msocbvcrCBGradMainMenuHorzBegin,
			// Token: 0x040041D6 RID: 16854
			msocbvcrCBGradMainMenuHorzEnd,
			// Token: 0x040041D7 RID: 16855
			msocbvcrCBGradMenuIconBkgdDroppedBegin,
			// Token: 0x040041D8 RID: 16856
			msocbvcrCBGradMenuIconBkgdDroppedEnd,
			// Token: 0x040041D9 RID: 16857
			msocbvcrCBGradMenuIconBkgdDroppedMiddle,
			// Token: 0x040041DA RID: 16858
			msocbvcrCBGradMenuTitleBkgdBegin,
			// Token: 0x040041DB RID: 16859
			msocbvcrCBGradMenuTitleBkgdEnd,
			// Token: 0x040041DC RID: 16860
			msocbvcrCBGradMouseDownBegin,
			// Token: 0x040041DD RID: 16861
			msocbvcrCBGradMouseDownEnd,
			// Token: 0x040041DE RID: 16862
			msocbvcrCBGradMouseDownMiddle,
			// Token: 0x040041DF RID: 16863
			msocbvcrCBGradMouseOverBegin,
			// Token: 0x040041E0 RID: 16864
			msocbvcrCBGradMouseOverEnd,
			// Token: 0x040041E1 RID: 16865
			msocbvcrCBGradMouseOverMiddle,
			// Token: 0x040041E2 RID: 16866
			msocbvcrCBGradOptionsBegin,
			// Token: 0x040041E3 RID: 16867
			msocbvcrCBGradOptionsEnd,
			// Token: 0x040041E4 RID: 16868
			msocbvcrCBGradOptionsMiddle,
			// Token: 0x040041E5 RID: 16869
			msocbvcrCBGradOptionsMouseOverBegin,
			// Token: 0x040041E6 RID: 16870
			msocbvcrCBGradOptionsMouseOverEnd,
			// Token: 0x040041E7 RID: 16871
			msocbvcrCBGradOptionsMouseOverMiddle,
			// Token: 0x040041E8 RID: 16872
			msocbvcrCBGradOptionsSelectedBegin,
			// Token: 0x040041E9 RID: 16873
			msocbvcrCBGradOptionsSelectedEnd,
			// Token: 0x040041EA RID: 16874
			msocbvcrCBGradOptionsSelectedMiddle,
			// Token: 0x040041EB RID: 16875
			msocbvcrCBGradSelectedBegin,
			// Token: 0x040041EC RID: 16876
			msocbvcrCBGradSelectedEnd,
			// Token: 0x040041ED RID: 16877
			msocbvcrCBGradSelectedMiddle,
			// Token: 0x040041EE RID: 16878
			msocbvcrCBGradVertBegin,
			// Token: 0x040041EF RID: 16879
			msocbvcrCBGradVertEnd,
			// Token: 0x040041F0 RID: 16880
			msocbvcrCBGradVertMiddle,
			// Token: 0x040041F1 RID: 16881
			msocbvcrCBIconDisabledDark,
			// Token: 0x040041F2 RID: 16882
			msocbvcrCBIconDisabledLight,
			// Token: 0x040041F3 RID: 16883
			msocbvcrCBLabelBkgnd,
			// Token: 0x040041F4 RID: 16884
			msocbvcrCBLowColorIconDisabled,
			// Token: 0x040041F5 RID: 16885
			msocbvcrCBMainMenuBkgd,
			// Token: 0x040041F6 RID: 16886
			msocbvcrCBMenuBdrOuter,
			// Token: 0x040041F7 RID: 16887
			msocbvcrCBMenuBkgd,
			// Token: 0x040041F8 RID: 16888
			msocbvcrCBMenuCtlText,
			// Token: 0x040041F9 RID: 16889
			msocbvcrCBMenuCtlTextDisabled,
			// Token: 0x040041FA RID: 16890
			msocbvcrCBMenuIconBkgd,
			// Token: 0x040041FB RID: 16891
			msocbvcrCBMenuIconBkgdDropped,
			// Token: 0x040041FC RID: 16892
			msocbvcrCBMenuShadow,
			// Token: 0x040041FD RID: 16893
			msocbvcrCBMenuSplitArrow,
			// Token: 0x040041FE RID: 16894
			msocbvcrCBOptionsButtonShadow,
			// Token: 0x040041FF RID: 16895
			msocbvcrCBShadow,
			// Token: 0x04004200 RID: 16896
			msocbvcrCBSplitterLine,
			// Token: 0x04004201 RID: 16897
			msocbvcrCBSplitterLineLight,
			// Token: 0x04004202 RID: 16898
			msocbvcrCBTearOffHandle,
			// Token: 0x04004203 RID: 16899
			msocbvcrCBTearOffHandleMouseOver,
			// Token: 0x04004204 RID: 16900
			msocbvcrCBTitleBkgd,
			// Token: 0x04004205 RID: 16901
			msocbvcrCBTitleText,
			// Token: 0x04004206 RID: 16902
			msocbvcrDisabledFocuslessHighlightedText,
			// Token: 0x04004207 RID: 16903
			msocbvcrDisabledHighlightedText,
			// Token: 0x04004208 RID: 16904
			msocbvcrDlgGroupBoxText,
			// Token: 0x04004209 RID: 16905
			msocbvcrDocTabBdr,
			// Token: 0x0400420A RID: 16906
			msocbvcrDocTabBdrDark,
			// Token: 0x0400420B RID: 16907
			msocbvcrDocTabBdrDarkMouseDown,
			// Token: 0x0400420C RID: 16908
			msocbvcrDocTabBdrDarkMouseOver,
			// Token: 0x0400420D RID: 16909
			msocbvcrDocTabBdrLight,
			// Token: 0x0400420E RID: 16910
			msocbvcrDocTabBdrLightMouseDown,
			// Token: 0x0400420F RID: 16911
			msocbvcrDocTabBdrLightMouseOver,
			// Token: 0x04004210 RID: 16912
			msocbvcrDocTabBdrMouseDown,
			// Token: 0x04004211 RID: 16913
			msocbvcrDocTabBdrMouseOver,
			// Token: 0x04004212 RID: 16914
			msocbvcrDocTabBdrSelected,
			// Token: 0x04004213 RID: 16915
			msocbvcrDocTabBkgd,
			// Token: 0x04004214 RID: 16916
			msocbvcrDocTabBkgdMouseDown,
			// Token: 0x04004215 RID: 16917
			msocbvcrDocTabBkgdMouseOver,
			// Token: 0x04004216 RID: 16918
			msocbvcrDocTabBkgdSelected,
			// Token: 0x04004217 RID: 16919
			msocbvcrDocTabText,
			// Token: 0x04004218 RID: 16920
			msocbvcrDocTabTextMouseDown,
			// Token: 0x04004219 RID: 16921
			msocbvcrDocTabTextMouseOver,
			// Token: 0x0400421A RID: 16922
			msocbvcrDocTabTextSelected,
			// Token: 0x0400421B RID: 16923
			msocbvcrDWActiveTabBkgd,
			// Token: 0x0400421C RID: 16924
			msocbvcrDWActiveTabText,
			// Token: 0x0400421D RID: 16925
			msocbvcrDWActiveTabTextDisabled,
			// Token: 0x0400421E RID: 16926
			msocbvcrDWInactiveTabBkgd,
			// Token: 0x0400421F RID: 16927
			msocbvcrDWInactiveTabText,
			// Token: 0x04004220 RID: 16928
			msocbvcrDWTabBkgdMouseDown,
			// Token: 0x04004221 RID: 16929
			msocbvcrDWTabBkgdMouseOver,
			// Token: 0x04004222 RID: 16930
			msocbvcrDWTabTextMouseDown,
			// Token: 0x04004223 RID: 16931
			msocbvcrDWTabTextMouseOver,
			// Token: 0x04004224 RID: 16932
			msocbvcrFocuslessHighlightedBkgd,
			// Token: 0x04004225 RID: 16933
			msocbvcrFocuslessHighlightedText,
			// Token: 0x04004226 RID: 16934
			msocbvcrGDHeaderBdr,
			// Token: 0x04004227 RID: 16935
			msocbvcrGDHeaderBkgd,
			// Token: 0x04004228 RID: 16936
			msocbvcrGDHeaderCellBdr,
			// Token: 0x04004229 RID: 16937
			msocbvcrGDHeaderCellBkgd,
			// Token: 0x0400422A RID: 16938
			msocbvcrGDHeaderCellBkgdSelected,
			// Token: 0x0400422B RID: 16939
			msocbvcrGDHeaderSeeThroughSelection,
			// Token: 0x0400422C RID: 16940
			msocbvcrGSPDarkBkgd,
			// Token: 0x0400422D RID: 16941
			msocbvcrGSPGroupContentDarkBkgd,
			// Token: 0x0400422E RID: 16942
			msocbvcrGSPGroupContentLightBkgd,
			// Token: 0x0400422F RID: 16943
			msocbvcrGSPGroupContentText,
			// Token: 0x04004230 RID: 16944
			msocbvcrGSPGroupContentTextDisabled,
			// Token: 0x04004231 RID: 16945
			msocbvcrGSPGroupHeaderDarkBkgd,
			// Token: 0x04004232 RID: 16946
			msocbvcrGSPGroupHeaderLightBkgd,
			// Token: 0x04004233 RID: 16947
			msocbvcrGSPGroupHeaderText,
			// Token: 0x04004234 RID: 16948
			msocbvcrGSPGroupline,
			// Token: 0x04004235 RID: 16949
			msocbvcrGSPHyperlink,
			// Token: 0x04004236 RID: 16950
			msocbvcrGSPLightBkgd,
			// Token: 0x04004237 RID: 16951
			msocbvcrHyperlink,
			// Token: 0x04004238 RID: 16952
			msocbvcrHyperlinkFollowed,
			// Token: 0x04004239 RID: 16953
			msocbvcrJotNavUIBdr,
			// Token: 0x0400423A RID: 16954
			msocbvcrJotNavUIGradBegin,
			// Token: 0x0400423B RID: 16955
			msocbvcrJotNavUIGradEnd,
			// Token: 0x0400423C RID: 16956
			msocbvcrJotNavUIGradMiddle,
			// Token: 0x0400423D RID: 16957
			msocbvcrJotNavUIText,
			// Token: 0x0400423E RID: 16958
			msocbvcrListHeaderArrow,
			// Token: 0x0400423F RID: 16959
			msocbvcrNetLookBkgnd,
			// Token: 0x04004240 RID: 16960
			msocbvcrOABBkgd,
			// Token: 0x04004241 RID: 16961
			msocbvcrOBBkgdBdr,
			// Token: 0x04004242 RID: 16962
			msocbvcrOBBkgdBdrContrast,
			// Token: 0x04004243 RID: 16963
			msocbvcrOGMDIParentWorkspaceBkgd,
			// Token: 0x04004244 RID: 16964
			msocbvcrOGRulerActiveBkgd,
			// Token: 0x04004245 RID: 16965
			msocbvcrOGRulerBdr,
			// Token: 0x04004246 RID: 16966
			msocbvcrOGRulerBkgd,
			// Token: 0x04004247 RID: 16967
			msocbvcrOGRulerInactiveBkgd,
			// Token: 0x04004248 RID: 16968
			msocbvcrOGRulerTabBoxBdr,
			// Token: 0x04004249 RID: 16969
			msocbvcrOGRulerTabBoxBdrHighlight,
			// Token: 0x0400424A RID: 16970
			msocbvcrOGRulerTabStopTicks,
			// Token: 0x0400424B RID: 16971
			msocbvcrOGRulerText,
			// Token: 0x0400424C RID: 16972
			msocbvcrOGTaskPaneGroupBoxHeaderBkgd,
			// Token: 0x0400424D RID: 16973
			msocbvcrOGWorkspaceBkgd,
			// Token: 0x0400424E RID: 16974
			msocbvcrOLKFlagNone,
			// Token: 0x0400424F RID: 16975
			msocbvcrOLKFolderbarDark,
			// Token: 0x04004250 RID: 16976
			msocbvcrOLKFolderbarLight,
			// Token: 0x04004251 RID: 16977
			msocbvcrOLKFolderbarText,
			// Token: 0x04004252 RID: 16978
			msocbvcrOLKGridlines,
			// Token: 0x04004253 RID: 16979
			msocbvcrOLKGroupLine,
			// Token: 0x04004254 RID: 16980
			msocbvcrOLKGroupNested,
			// Token: 0x04004255 RID: 16981
			msocbvcrOLKGroupShaded,
			// Token: 0x04004256 RID: 16982
			msocbvcrOLKGroupText,
			// Token: 0x04004257 RID: 16983
			msocbvcrOLKIconBar,
			// Token: 0x04004258 RID: 16984
			msocbvcrOLKInfoBarBkgd,
			// Token: 0x04004259 RID: 16985
			msocbvcrOLKInfoBarText,
			// Token: 0x0400425A RID: 16986
			msocbvcrOLKPreviewPaneLabelText,
			// Token: 0x0400425B RID: 16987
			msocbvcrOLKTodayIndicatorDark,
			// Token: 0x0400425C RID: 16988
			msocbvcrOLKTodayIndicatorLight,
			// Token: 0x0400425D RID: 16989
			msocbvcrOLKWBActionDividerLine,
			// Token: 0x0400425E RID: 16990
			msocbvcrOLKWBButtonDark,
			// Token: 0x0400425F RID: 16991
			msocbvcrOLKWBButtonLight,
			// Token: 0x04004260 RID: 16992
			msocbvcrOLKWBDarkOutline,
			// Token: 0x04004261 RID: 16993
			msocbvcrOLKWBFoldersBackground,
			// Token: 0x04004262 RID: 16994
			msocbvcrOLKWBHoverButtonDark,
			// Token: 0x04004263 RID: 16995
			msocbvcrOLKWBHoverButtonLight,
			// Token: 0x04004264 RID: 16996
			msocbvcrOLKWBLabelText,
			// Token: 0x04004265 RID: 16997
			msocbvcrOLKWBPressedButtonDark,
			// Token: 0x04004266 RID: 16998
			msocbvcrOLKWBPressedButtonLight,
			// Token: 0x04004267 RID: 16999
			msocbvcrOLKWBSelectedButtonDark,
			// Token: 0x04004268 RID: 17000
			msocbvcrOLKWBSelectedButtonLight,
			// Token: 0x04004269 RID: 17001
			msocbvcrOLKWBSplitterDark,
			// Token: 0x0400426A RID: 17002
			msocbvcrOLKWBSplitterLight,
			// Token: 0x0400426B RID: 17003
			msocbvcrPlacesBarBkgd,
			// Token: 0x0400426C RID: 17004
			msocbvcrPPOutlineThumbnailsPaneTabAreaBkgd,
			// Token: 0x0400426D RID: 17005
			msocbvcrPPOutlineThumbnailsPaneTabBdr,
			// Token: 0x0400426E RID: 17006
			msocbvcrPPOutlineThumbnailsPaneTabInactiveBkgd,
			// Token: 0x0400426F RID: 17007
			msocbvcrPPOutlineThumbnailsPaneTabText,
			// Token: 0x04004270 RID: 17008
			msocbvcrPPSlideBdrActiveSelected,
			// Token: 0x04004271 RID: 17009
			msocbvcrPPSlideBdrActiveSelectedMouseOver,
			// Token: 0x04004272 RID: 17010
			msocbvcrPPSlideBdrInactiveSelected,
			// Token: 0x04004273 RID: 17011
			msocbvcrPPSlideBdrMouseOver,
			// Token: 0x04004274 RID: 17012
			msocbvcrPubPrintDocScratchPageBkgd,
			// Token: 0x04004275 RID: 17013
			msocbvcrPubWebDocScratchPageBkgd,
			// Token: 0x04004276 RID: 17014
			msocbvcrSBBdr,
			// Token: 0x04004277 RID: 17015
			msocbvcrScrollbarBkgd,
			// Token: 0x04004278 RID: 17016
			msocbvcrToastGradBegin,
			// Token: 0x04004279 RID: 17017
			msocbvcrToastGradEnd,
			// Token: 0x0400427A RID: 17018
			msocbvcrWPBdrInnerDocked,
			// Token: 0x0400427B RID: 17019
			msocbvcrWPBdrOuterDocked,
			// Token: 0x0400427C RID: 17020
			msocbvcrWPBdrOuterFloating,
			// Token: 0x0400427D RID: 17021
			msocbvcrWPBkgd,
			// Token: 0x0400427E RID: 17022
			msocbvcrWPCtlBdr,
			// Token: 0x0400427F RID: 17023
			msocbvcrWPCtlBdrDefault,
			// Token: 0x04004280 RID: 17024
			msocbvcrWPCtlBdrDisabled,
			// Token: 0x04004281 RID: 17025
			msocbvcrWPCtlBkgd,
			// Token: 0x04004282 RID: 17026
			msocbvcrWPCtlBkgdDisabled,
			// Token: 0x04004283 RID: 17027
			msocbvcrWPCtlText,
			// Token: 0x04004284 RID: 17028
			msocbvcrWPCtlTextDisabled,
			// Token: 0x04004285 RID: 17029
			msocbvcrWPCtlTextMouseDown,
			// Token: 0x04004286 RID: 17030
			msocbvcrWPGroupline,
			// Token: 0x04004287 RID: 17031
			msocbvcrWPInfoTipBkgd,
			// Token: 0x04004288 RID: 17032
			msocbvcrWPInfoTipText,
			// Token: 0x04004289 RID: 17033
			msocbvcrWPNavBarBkgnd,
			// Token: 0x0400428A RID: 17034
			msocbvcrWPText,
			// Token: 0x0400428B RID: 17035
			msocbvcrWPTextDisabled,
			// Token: 0x0400428C RID: 17036
			msocbvcrWPTitleBkgdActive,
			// Token: 0x0400428D RID: 17037
			msocbvcrWPTitleBkgdInactive,
			// Token: 0x0400428E RID: 17038
			msocbvcrWPTitleTextActive,
			// Token: 0x0400428F RID: 17039
			msocbvcrWPTitleTextInactive,
			// Token: 0x04004290 RID: 17040
			msocbvcrXLFormulaBarBkgd,
			// Token: 0x04004291 RID: 17041
			ButtonSelectedHighlight,
			// Token: 0x04004292 RID: 17042
			ButtonPressedHighlight,
			// Token: 0x04004293 RID: 17043
			ButtonCheckedHighlight,
			// Token: 0x04004294 RID: 17044
			lastKnownColor = 212
		}
	}
}
