using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	/// <summary>Provides <see cref="T:System.Drawing.Color" /> structures that are colors of a Windows display element. This class cannot be inherited.</summary>
	// Token: 0x02000325 RID: 805
	public sealed class ProfessionalColors
	{
		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06003310 RID: 13072 RVA: 0x000E39EE File Offset: 0x000E1BEE
		internal static ProfessionalColorTable ColorTable
		{
			get
			{
				if (ProfessionalColors.professionalColorTable == null)
				{
					ProfessionalColors.professionalColorTable = new ProfessionalColorTable();
				}
				return ProfessionalColors.professionalColorTable;
			}
		}

		// Token: 0x06003311 RID: 13073 RVA: 0x000E3A06 File Offset: 0x000E1C06
		static ProfessionalColors()
		{
			SystemEvents.UserPreferenceChanged += ProfessionalColors.OnUserPreferenceChanged;
			ProfessionalColors.SetScheme();
		}

		// Token: 0x06003312 RID: 13074 RVA: 0x00002843 File Offset: 0x00000A43
		private ProfessionalColors()
		{
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06003313 RID: 13075 RVA: 0x000E3A1E File Offset: 0x000E1C1E
		internal static string ColorScheme
		{
			get
			{
				return ProfessionalColors.colorScheme;
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06003314 RID: 13076 RVA: 0x000E3A25 File Offset: 0x000E1C25
		internal static object ColorFreshnessKey
		{
			get
			{
				return ProfessionalColors.colorFreshnessKey;
			}
		}

		/// <summary>Gets the solid color used when the button is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color used when the button is selected.</returns>
		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06003315 RID: 13077 RVA: 0x000E3A2C File Offset: 0x000E1C2C
		[SRDescription("ProfessionalColorsButtonSelectedHighlightDescr")]
		public static Color ButtonSelectedHighlight
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonSelectedHighlight;
			}
		}

		/// <summary>Gets the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonSelectedHighlight" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonSelectedHighlight" />.</returns>
		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06003316 RID: 13078 RVA: 0x000E3A38 File Offset: 0x000E1C38
		[SRDescription("ProfessionalColorsButtonSelectedHighlightBorderDescr")]
		public static Color ButtonSelectedHighlightBorder
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonSelectedHighlightBorder;
			}
		}

		/// <summary>Gets the solid color used when the button is pressed down.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color used when the button is pressed down.</returns>
		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06003317 RID: 13079 RVA: 0x000E3A44 File Offset: 0x000E1C44
		[SRDescription("ProfessionalColorsButtonPressedHighlightDescr")]
		public static Color ButtonPressedHighlight
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonPressedHighlight;
			}
		}

		/// <summary>Gets the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonPressedHighlight" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonPressedHighlight" />.</returns>
		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06003318 RID: 13080 RVA: 0x000E3A50 File Offset: 0x000E1C50
		[SRDescription("ProfessionalColorsButtonPressedHighlightBorderDescr")]
		public static Color ButtonPressedHighlightBorder
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonPressedHighlightBorder;
			}
		}

		/// <summary>Gets the solid color used when the button is checked.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color used when the button is checked.</returns>
		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06003319 RID: 13081 RVA: 0x000E3A5C File Offset: 0x000E1C5C
		[SRDescription("ProfessionalColorsButtonCheckedHighlightDescr")]
		public static Color ButtonCheckedHighlight
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonCheckedHighlight;
			}
		}

		/// <summary>Gets the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonCheckedHighlight" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonCheckedHighlight" />.</returns>
		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x0600331A RID: 13082 RVA: 0x000E3A68 File Offset: 0x000E1C68
		[SRDescription("ProfessionalColorsButtonCheckedHighlightBorderDescr")]
		public static Color ButtonCheckedHighlightBorder
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonCheckedHighlightBorder;
			}
		}

		/// <summary>Gets the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonPressedGradientBegin" />, <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonPressedGradientMiddle" />, and <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonPressedGradientEnd" /> colors.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonPressedGradientBegin" />, <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonPressedGradientMiddle" />, and <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonPressedGradientEnd" /> colors.</returns>
		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x0600331B RID: 13083 RVA: 0x000E3A74 File Offset: 0x000E1C74
		[SRDescription("ProfessionalColorsButtonPressedBorderDescr")]
		public static Color ButtonPressedBorder
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonPressedBorder;
			}
		}

		/// <summary>Gets the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonSelectedGradientBegin" />, <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonSelectedGradientMiddle" />, and <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonSelectedGradientEnd" /> colors.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonSelectedGradientBegin" />, <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonSelectedGradientMiddle" />, and <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonSelectedGradientEnd" /> colors.</returns>
		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x0600331C RID: 13084 RVA: 0x000E3A80 File Offset: 0x000E1C80
		[SRDescription("ProfessionalColorsButtonSelectedBorderDescr")]
		public static Color ButtonSelectedBorder
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonSelectedBorder;
			}
		}

		/// <summary>Gets the starting color of the gradient used when the button is checked.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the gradient used when the button is checked.</returns>
		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x0600331D RID: 13085 RVA: 0x000E3A8C File Offset: 0x000E1C8C
		[SRDescription("ProfessionalColorsButtonCheckedGradientBeginDescr")]
		public static Color ButtonCheckedGradientBegin
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonCheckedGradientBegin;
			}
		}

		/// <summary>Gets the middle color of the gradient used when the button is checked.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used when the button is checked.</returns>
		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x0600331E RID: 13086 RVA: 0x000E3A98 File Offset: 0x000E1C98
		[SRDescription("ProfessionalColorsButtonCheckedGradientMiddleDescr")]
		public static Color ButtonCheckedGradientMiddle
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonCheckedGradientMiddle;
			}
		}

		/// <summary>Gets the end color of the gradient used when the button is checked.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when the button is checked.</returns>
		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x0600331F RID: 13087 RVA: 0x000E3AA4 File Offset: 0x000E1CA4
		[SRDescription("ProfessionalColorsButtonCheckedGradientEndDescr")]
		public static Color ButtonCheckedGradientEnd
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonCheckedGradientEnd;
			}
		}

		/// <summary>Gets the starting color of the gradient used when the button is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used when the button is selected.</returns>
		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06003320 RID: 13088 RVA: 0x000E3AB0 File Offset: 0x000E1CB0
		[SRDescription("ProfessionalColorsButtonSelectedGradientBeginDescr")]
		public static Color ButtonSelectedGradientBegin
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonSelectedGradientBegin;
			}
		}

		/// <summary>Gets the middle color of the gradient used when the button is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used when the button is selected.</returns>
		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06003321 RID: 13089 RVA: 0x000E3ABC File Offset: 0x000E1CBC
		[SRDescription("ProfessionalColorsButtonSelectedGradientMiddleDescr")]
		public static Color ButtonSelectedGradientMiddle
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonSelectedGradientMiddle;
			}
		}

		/// <summary>Gets the end color of the gradient used when the button is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when the button is selected.</returns>
		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06003322 RID: 13090 RVA: 0x000E3AC8 File Offset: 0x000E1CC8
		[SRDescription("ProfessionalColorsButtonSelectedGradientEndDescr")]
		public static Color ButtonSelectedGradientEnd
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonSelectedGradientEnd;
			}
		}

		/// <summary>Gets the starting color of the gradient used when the button is pressed down.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used when the button is pressed down.</returns>
		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06003323 RID: 13091 RVA: 0x000E3AD4 File Offset: 0x000E1CD4
		[SRDescription("ProfessionalColorsButtonPressedGradientBeginDescr")]
		public static Color ButtonPressedGradientBegin
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonPressedGradientBegin;
			}
		}

		/// <summary>Gets the middle color of the gradient used when the button is pressed down.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used when the button is pressed.</returns>
		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06003324 RID: 13092 RVA: 0x000E3AE0 File Offset: 0x000E1CE0
		[SRDescription("ProfessionalColorsButtonPressedGradientMiddleDescr")]
		public static Color ButtonPressedGradientMiddle
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonPressedGradientMiddle;
			}
		}

		/// <summary>Gets the end color of the gradient used when the button is pressed down.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when the button is pressed down.</returns>
		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06003325 RID: 13093 RVA: 0x000E3AEC File Offset: 0x000E1CEC
		[SRDescription("ProfessionalColorsButtonPressedGradientEndDescr")]
		public static Color ButtonPressedGradientEnd
		{
			get
			{
				return ProfessionalColors.ColorTable.ButtonPressedGradientEnd;
			}
		}

		/// <summary>Gets the solid color to use when the check box is selected and gradients are being used.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color to use when the check box is selected and gradients are being used.</returns>
		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06003326 RID: 13094 RVA: 0x000E3AF8 File Offset: 0x000E1CF8
		[SRDescription("ProfessionalColorsCheckBackgroundDescr")]
		public static Color CheckBackground
		{
			get
			{
				return ProfessionalColors.ColorTable.CheckBackground;
			}
		}

		/// <summary>Gets the solid color to use when the check box is selected and gradients are being used.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color to use when the check box is selected and gradients are being used.</returns>
		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06003327 RID: 13095 RVA: 0x000E3B04 File Offset: 0x000E1D04
		[SRDescription("ProfessionalColorsCheckSelectedBackgroundDescr")]
		public static Color CheckSelectedBackground
		{
			get
			{
				return ProfessionalColors.ColorTable.CheckSelectedBackground;
			}
		}

		/// <summary>Gets the solid color to use when the check box is selected and gradients are being used.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color to use when the check box is selected and gradients are being used.</returns>
		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06003328 RID: 13096 RVA: 0x000E3B10 File Offset: 0x000E1D10
		[SRDescription("ProfessionalColorsCheckPressedBackgroundDescr")]
		public static Color CheckPressedBackground
		{
			get
			{
				return ProfessionalColors.ColorTable.CheckPressedBackground;
			}
		}

		/// <summary>Gets the color to use for shadow effects on the grip or move handle.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color to use for shadow effects on the grip or move handle.</returns>
		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06003329 RID: 13097 RVA: 0x000E3B1C File Offset: 0x000E1D1C
		[SRDescription("ProfessionalColorsGripDarkDescr")]
		public static Color GripDark
		{
			get
			{
				return ProfessionalColors.ColorTable.GripDark;
			}
		}

		/// <summary>Gets the color to use for highlight effects on the grip or move handle.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color to use for highlight effects on the grip or move handle.</returns>
		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x0600332A RID: 13098 RVA: 0x000E3B28 File Offset: 0x000E1D28
		[SRDescription("ProfessionalColorsGripLightDescr")]
		public static Color GripLight
		{
			get
			{
				return ProfessionalColors.ColorTable.GripLight;
			}
		}

		/// <summary>Gets the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</returns>
		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x0600332B RID: 13099 RVA: 0x000E3B34 File Offset: 0x000E1D34
		[SRDescription("ProfessionalColorsImageMarginGradientBeginDescr")]
		public static Color ImageMarginGradientBegin
		{
			get
			{
				return ProfessionalColors.ColorTable.ImageMarginGradientBegin;
			}
		}

		/// <summary>Gets the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</returns>
		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x0600332C RID: 13100 RVA: 0x000E3B40 File Offset: 0x000E1D40
		[SRDescription("ProfessionalColorsImageMarginGradientMiddleDescr")]
		public static Color ImageMarginGradientMiddle
		{
			get
			{
				return ProfessionalColors.ColorTable.ImageMarginGradientMiddle;
			}
		}

		/// <summary>Gets the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</returns>
		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x0600332D RID: 13101 RVA: 0x000E3B4C File Offset: 0x000E1D4C
		[SRDescription("ProfessionalColorsImageMarginGradientEndDescr")]
		public static Color ImageMarginGradientEnd
		{
			get
			{
				return ProfessionalColors.ColorTable.ImageMarginGradientEnd;
			}
		}

		/// <summary>Gets the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</returns>
		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x0600332E RID: 13102 RVA: 0x000E3B58 File Offset: 0x000E1D58
		[SRDescription("ProfessionalColorsImageMarginRevealedGradientBeginDescr")]
		public static Color ImageMarginRevealedGradientBegin
		{
			get
			{
				return ProfessionalColors.ColorTable.ImageMarginRevealedGradientBegin;
			}
		}

		/// <summary>Gets the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</returns>
		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x0600332F RID: 13103 RVA: 0x000E3B64 File Offset: 0x000E1D64
		[SRDescription("ProfessionalColorsImageMarginRevealedGradientMiddleDescr")]
		public static Color ImageMarginRevealedGradientMiddle
		{
			get
			{
				return ProfessionalColors.ColorTable.ImageMarginRevealedGradientMiddle;
			}
		}

		/// <summary>Gets the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</returns>
		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06003330 RID: 13104 RVA: 0x000E3B70 File Offset: 0x000E1D70
		[SRDescription("ProfessionalColorsImageMarginRevealedGradientEndDescr")]
		public static Color ImageMarginRevealedGradientEnd
		{
			get
			{
				return ProfessionalColors.ColorTable.ImageMarginRevealedGradientEnd;
			}
		}

		/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip" />.</returns>
		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06003331 RID: 13105 RVA: 0x000E3B7C File Offset: 0x000E1D7C
		[SRDescription("ProfessionalColorsMenuStripGradientBeginDescr")]
		public static Color MenuStripGradientBegin
		{
			get
			{
				return ProfessionalColors.ColorTable.MenuStripGradientBegin;
			}
		}

		/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip" />.</returns>
		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06003332 RID: 13106 RVA: 0x000E3B88 File Offset: 0x000E1D88
		[SRDescription("ProfessionalColorsMenuStripGradientEndDescr")]
		public static Color MenuStripGradientEnd
		{
			get
			{
				return ProfessionalColors.ColorTable.MenuStripGradientEnd;
			}
		}

		/// <summary>Gets the border color or a <see cref="T:System.Windows.Forms.MenuStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color or a <see cref="T:System.Windows.Forms.MenuStrip" />.</returns>
		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06003333 RID: 13107 RVA: 0x000E3B94 File Offset: 0x000E1D94
		[SRDescription("ProfessionalColorsMenuBorderDescr")]
		public static Color MenuBorder
		{
			get
			{
				return ProfessionalColors.ColorTable.MenuBorder;
			}
		}

		/// <summary>Gets the solid color to use when a <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> other than the top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color to use when a <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> other than the top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</returns>
		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x06003334 RID: 13108 RVA: 0x000E3BA0 File Offset: 0x000E1DA0
		[SRDescription("ProfessionalColorsMenuItemSelectedDescr")]
		public static Color MenuItemSelected
		{
			get
			{
				return ProfessionalColors.ColorTable.MenuItemSelected;
			}
		}

		/// <summary>Gets the border color to use with a <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with a <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</returns>
		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x06003335 RID: 13109 RVA: 0x000E3BAC File Offset: 0x000E1DAC
		[SRDescription("ProfessionalColorsMenuItemBorderDescr")]
		public static Color MenuItemBorder
		{
			get
			{
				return ProfessionalColors.ColorTable.MenuItemBorder;
			}
		}

		/// <summary>Gets the starting color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</returns>
		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x06003336 RID: 13110 RVA: 0x000E3BB8 File Offset: 0x000E1DB8
		[SRDescription("ProfessionalColorsMenuItemSelectedGradientBeginDescr")]
		public static Color MenuItemSelectedGradientBegin
		{
			get
			{
				return ProfessionalColors.ColorTable.MenuItemSelectedGradientBegin;
			}
		}

		/// <summary>Gets the end color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</returns>
		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x06003337 RID: 13111 RVA: 0x000E3BC4 File Offset: 0x000E1DC4
		[SRDescription("ProfessionalColorsMenuItemSelectedGradientEndDescr")]
		public static Color MenuItemSelectedGradientEnd
		{
			get
			{
				return ProfessionalColors.ColorTable.MenuItemSelectedGradientEnd;
			}
		}

		/// <summary>Gets the starting color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed down.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed down.</returns>
		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06003338 RID: 13112 RVA: 0x000E3BD0 File Offset: 0x000E1DD0
		[SRDescription("ProfessionalColorsMenuItemPressedGradientBeginDescr")]
		public static Color MenuItemPressedGradientBegin
		{
			get
			{
				return ProfessionalColors.ColorTable.MenuItemPressedGradientBegin;
			}
		}

		/// <summary>Gets the middle color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed down.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed down.</returns>
		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06003339 RID: 13113 RVA: 0x000E3BDC File Offset: 0x000E1DDC
		[SRDescription("ProfessionalColorsMenuItemPressedGradientMiddleDescr")]
		public static Color MenuItemPressedGradientMiddle
		{
			get
			{
				return ProfessionalColors.ColorTable.MenuItemPressedGradientMiddle;
			}
		}

		/// <summary>Gets the end color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed down.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed down.</returns>
		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x0600333A RID: 13114 RVA: 0x000E3BE8 File Offset: 0x000E1DE8
		[SRDescription("ProfessionalColorsMenuItemPressedGradientEndDescr")]
		public static Color MenuItemPressedGradientEnd
		{
			get
			{
				return ProfessionalColors.ColorTable.MenuItemPressedGradientEnd;
			}
		}

		/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</returns>
		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x0600333B RID: 13115 RVA: 0x000E3BF4 File Offset: 0x000E1DF4
		[SRDescription("ProfessionalColorsRaftingContainerGradientBeginDescr")]
		public static Color RaftingContainerGradientBegin
		{
			get
			{
				return ProfessionalColors.ColorTable.RaftingContainerGradientBegin;
			}
		}

		/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</returns>
		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x0600333C RID: 13116 RVA: 0x000E3C00 File Offset: 0x000E1E00
		[SRDescription("ProfessionalColorsRaftingContainerGradientEndDescr")]
		public static Color RaftingContainerGradientEnd
		{
			get
			{
				return ProfessionalColors.ColorTable.RaftingContainerGradientEnd;
			}
		}

		/// <summary>Gets the color to use to for shadow effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color to use to for shadow effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</returns>
		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x0600333D RID: 13117 RVA: 0x000E3C0C File Offset: 0x000E1E0C
		[SRDescription("ProfessionalColorsSeparatorDarkDescr")]
		public static Color SeparatorDark
		{
			get
			{
				return ProfessionalColors.ColorTable.SeparatorDark;
			}
		}

		/// <summary>Gets the color to use to for highlight effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color to use to create highlight effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</returns>
		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x0600333E RID: 13118 RVA: 0x000E3C18 File Offset: 0x000E1E18
		[SRDescription("ProfessionalColorsSeparatorLightDescr")]
		public static Color SeparatorLight
		{
			get
			{
				return ProfessionalColors.ColorTable.SeparatorLight;
			}
		}

		/// <summary>Gets the starting color of the gradient used on the <see cref="T:System.Windows.Forms.StatusStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used on the <see cref="T:System.Windows.Forms.StatusStrip" />.</returns>
		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x0600333F RID: 13119 RVA: 0x000E3C24 File Offset: 0x000E1E24
		[SRDescription("ProfessionalColorsStatusStripGradientBeginDescr")]
		public static Color StatusStripGradientBegin
		{
			get
			{
				return ProfessionalColors.ColorTable.StatusStripGradientBegin;
			}
		}

		/// <summary>Gets the end color of the gradient used on the <see cref="T:System.Windows.Forms.StatusStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used on the <see cref="T:System.Windows.Forms.StatusStrip" />.</returns>
		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06003340 RID: 13120 RVA: 0x000E3C30 File Offset: 0x000E1E30
		[SRDescription("ProfessionalColorsStatusStripGradientEndDescr")]
		public static Color StatusStripGradientEnd
		{
			get
			{
				return ProfessionalColors.ColorTable.StatusStripGradientEnd;
			}
		}

		/// <summary>Gets the border color to use on the bottom edge of the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use on the bottom edge of the <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x06003341 RID: 13121 RVA: 0x000E3C3C File Offset: 0x000E1E3C
		[SRDescription("ProfessionalColorsToolStripBorderDescr")]
		public static Color ToolStripBorder
		{
			get
			{
				return ProfessionalColors.ColorTable.ToolStripBorder;
			}
		}

		/// <summary>Gets the solid background color of the <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid background color of the <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</returns>
		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x06003342 RID: 13122 RVA: 0x000E3C48 File Offset: 0x000E1E48
		[SRDescription("ProfessionalColorsToolStripDropDownBackgroundDescr")]
		public static Color ToolStripDropDownBackground
		{
			get
			{
				return ProfessionalColors.ColorTable.ToolStripDropDownBackground;
			}
		}

		/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</returns>
		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x06003343 RID: 13123 RVA: 0x000E3C54 File Offset: 0x000E1E54
		[SRDescription("ProfessionalColorsToolStripGradientBeginDescr")]
		public static Color ToolStripGradientBegin
		{
			get
			{
				return ProfessionalColors.ColorTable.ToolStripGradientBegin;
			}
		}

		/// <summary>Gets the middle color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</returns>
		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x06003344 RID: 13124 RVA: 0x000E3C60 File Offset: 0x000E1E60
		[SRDescription("ProfessionalColorsToolStripGradientMiddleDescr")]
		public static Color ToolStripGradientMiddle
		{
			get
			{
				return ProfessionalColors.ColorTable.ToolStripGradientMiddle;
			}
		}

		/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</returns>
		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x06003345 RID: 13125 RVA: 0x000E3C6C File Offset: 0x000E1E6C
		[SRDescription("ProfessionalColorsToolStripGradientEndDescr")]
		public static Color ToolStripGradientEnd
		{
			get
			{
				return ProfessionalColors.ColorTable.ToolStripGradientEnd;
			}
		}

		/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</returns>
		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x06003346 RID: 13126 RVA: 0x000E3C78 File Offset: 0x000E1E78
		[SRDescription("ProfessionalColorsToolStripContentPanelGradientBeginDescr")]
		public static Color ToolStripContentPanelGradientBegin
		{
			get
			{
				return ProfessionalColors.ColorTable.ToolStripContentPanelGradientBegin;
			}
		}

		/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</returns>
		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06003347 RID: 13127 RVA: 0x000E3C84 File Offset: 0x000E1E84
		[SRDescription("ProfessionalColorsToolStripContentPanelGradientEndDescr")]
		public static Color ToolStripContentPanelGradientEnd
		{
			get
			{
				return ProfessionalColors.ColorTable.ToolStripContentPanelGradientEnd;
			}
		}

		/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</returns>
		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x06003348 RID: 13128 RVA: 0x000E3C90 File Offset: 0x000E1E90
		[SRDescription("ProfessionalColorsToolStripPanelGradientBeginDescr")]
		public static Color ToolStripPanelGradientBegin
		{
			get
			{
				return ProfessionalColors.ColorTable.ToolStripPanelGradientBegin;
			}
		}

		/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</returns>
		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x06003349 RID: 13129 RVA: 0x000E3C9C File Offset: 0x000E1E9C
		[SRDescription("ProfessionalColorsToolStripPanelGradientEndDescr")]
		public static Color ToolStripPanelGradientEnd
		{
			get
			{
				return ProfessionalColors.ColorTable.ToolStripPanelGradientEnd;
			}
		}

		/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</returns>
		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x0600334A RID: 13130 RVA: 0x000E3CA8 File Offset: 0x000E1EA8
		[SRDescription("ProfessionalColorsOverflowButtonGradientBeginDescr")]
		public static Color OverflowButtonGradientBegin
		{
			get
			{
				return ProfessionalColors.ColorTable.OverflowButtonGradientBegin;
			}
		}

		/// <summary>Gets the middle color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</returns>
		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x0600334B RID: 13131 RVA: 0x000E3CB4 File Offset: 0x000E1EB4
		[SRDescription("ProfessionalColorsOverflowButtonGradientMiddleDescr")]
		public static Color OverflowButtonGradientMiddle
		{
			get
			{
				return ProfessionalColors.ColorTable.OverflowButtonGradientMiddle;
			}
		}

		/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</returns>
		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x0600334C RID: 13132 RVA: 0x000E3CC0 File Offset: 0x000E1EC0
		[SRDescription("ProfessionalColorsOverflowButtonGradientEndDescr")]
		public static Color OverflowButtonGradientEnd
		{
			get
			{
				return ProfessionalColors.ColorTable.OverflowButtonGradientEnd;
			}
		}

		// Token: 0x0600334D RID: 13133 RVA: 0x000E3CCC File Offset: 0x000E1ECC
		private static void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
		{
			ProfessionalColors.SetScheme();
			if (e.Category == UserPreferenceCategory.Color)
			{
				ProfessionalColors.colorFreshnessKey = new object();
			}
		}

		// Token: 0x0600334E RID: 13134 RVA: 0x000E3CE6 File Offset: 0x000E1EE6
		private static void SetScheme()
		{
			if (VisualStyleRenderer.IsSupported)
			{
				ProfessionalColors.colorScheme = VisualStyleInformation.ColorScheme;
				return;
			}
			ProfessionalColors.colorScheme = null;
		}

		// Token: 0x04001EB4 RID: 7860
		[ThreadStatic]
		private static ProfessionalColorTable professionalColorTable;

		// Token: 0x04001EB5 RID: 7861
		[ThreadStatic]
		private static string colorScheme;

		// Token: 0x04001EB6 RID: 7862
		[ThreadStatic]
		private static object colorFreshnessKey;
	}
}
