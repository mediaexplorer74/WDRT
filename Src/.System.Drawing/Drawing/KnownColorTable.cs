﻿using System;
using Microsoft.Win32;

namespace System.Drawing
{
	// Token: 0x02000025 RID: 37
	internal static class KnownColorTable
	{
		// Token: 0x0600036F RID: 879 RVA: 0x00010604 File Offset: 0x0000E804
		public static Color ArgbToKnownColor(int targetARGB)
		{
			KnownColorTable.EnsureColorTable();
			for (int i = 0; i < KnownColorTable.colorTable.Length; i++)
			{
				int num = KnownColorTable.colorTable[i];
				if (num == targetARGB)
				{
					Color color = Color.FromKnownColor((KnownColor)i);
					if (!color.IsSystemColor)
					{
						return color;
					}
				}
			}
			return Color.FromArgb(targetARGB);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0001064C File Offset: 0x0000E84C
		private static void EnsureColorTable()
		{
			if (KnownColorTable.colorTable == null)
			{
				KnownColorTable.InitColorTable();
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0001065C File Offset: 0x0000E85C
		private static void InitColorTable()
		{
			int[] array = new int[175];
			SystemEvents.UserPreferenceChanging += KnownColorTable.OnUserPreferenceChanging;
			KnownColorTable.UpdateSystemColors(array);
			array[27] = 16777215;
			array[28] = -984833;
			array[29] = -332841;
			array[30] = -16711681;
			array[31] = -8388652;
			array[32] = -983041;
			array[33] = -657956;
			array[34] = -6972;
			array[35] = -16777216;
			array[36] = -5171;
			array[37] = -16776961;
			array[38] = -7722014;
			array[39] = -5952982;
			array[40] = -2180985;
			array[41] = -10510688;
			array[42] = -8388864;
			array[43] = -2987746;
			array[44] = -32944;
			array[45] = -10185235;
			array[46] = -1828;
			array[47] = -2354116;
			array[48] = -16711681;
			array[49] = -16777077;
			array[50] = -16741493;
			array[51] = -4684277;
			array[52] = -5658199;
			array[53] = -16751616;
			array[54] = -4343957;
			array[55] = -7667573;
			array[56] = -11179217;
			array[57] = -29696;
			array[58] = -6737204;
			array[59] = -7667712;
			array[60] = -1468806;
			array[61] = -7357301;
			array[62] = -12042869;
			array[63] = -13676721;
			array[64] = -16724271;
			array[65] = -7077677;
			array[66] = -60269;
			array[67] = -16728065;
			array[68] = -9868951;
			array[69] = -14774017;
			array[70] = -5103070;
			array[71] = -1296;
			array[72] = -14513374;
			array[73] = -65281;
			array[74] = -2302756;
			array[75] = -460545;
			array[76] = -10496;
			array[77] = -2448096;
			array[78] = -8355712;
			array[79] = -16744448;
			array[80] = -5374161;
			array[81] = -983056;
			array[82] = -38476;
			array[83] = -3318692;
			array[84] = -11861886;
			array[85] = -16;
			array[86] = -989556;
			array[87] = -1644806;
			array[88] = -3851;
			array[89] = -8586240;
			array[90] = -1331;
			array[91] = -5383962;
			array[92] = -1015680;
			array[93] = -2031617;
			array[94] = -329006;
			array[95] = -2894893;
			array[96] = -7278960;
			array[97] = -18751;
			array[98] = -24454;
			array[99] = -14634326;
			array[100] = -7876870;
			array[101] = -8943463;
			array[102] = -5192482;
			array[103] = -32;
			array[104] = -16711936;
			array[105] = -13447886;
			array[106] = -331546;
			array[107] = -65281;
			array[108] = -8388608;
			array[109] = -10039894;
			array[110] = -16777011;
			array[111] = -4565549;
			array[112] = -7114533;
			array[113] = -12799119;
			array[114] = -8689426;
			array[115] = -16713062;
			array[116] = -12004916;
			array[117] = -3730043;
			array[118] = -15132304;
			array[119] = -655366;
			array[120] = -6943;
			array[121] = -6987;
			array[122] = -8531;
			array[123] = -16777088;
			array[124] = -133658;
			array[125] = -8355840;
			array[126] = -9728477;
			array[127] = -23296;
			array[128] = -47872;
			array[129] = -2461482;
			array[130] = -1120086;
			array[131] = -6751336;
			array[132] = -5247250;
			array[133] = -2396013;
			array[134] = -4139;
			array[135] = -9543;
			array[136] = -3308225;
			array[137] = -16181;
			array[138] = -2252579;
			array[139] = -5185306;
			array[140] = -8388480;
			array[141] = -65536;
			array[142] = -4419697;
			array[143] = -12490271;
			array[144] = -7650029;
			array[145] = -360334;
			array[146] = -744352;
			array[147] = -13726889;
			array[148] = -2578;
			array[149] = -6270419;
			array[150] = -4144960;
			array[151] = -7876885;
			array[152] = -9807155;
			array[153] = -9404272;
			array[154] = -1286;
			array[155] = -16711809;
			array[156] = -12156236;
			array[157] = -2968436;
			array[158] = -16744320;
			array[159] = -2572328;
			array[160] = -40121;
			array[161] = -12525360;
			array[162] = -1146130;
			array[163] = -663885;
			array[164] = -1;
			array[165] = -657931;
			array[166] = -256;
			array[167] = -6632142;
			KnownColorTable.colorTable = array;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00010BF4 File Offset: 0x0000EDF4
		private static void EnsureColorNameTable()
		{
			if (KnownColorTable.colorNameTable == null)
			{
				KnownColorTable.InitColorNameTable();
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00010C04 File Offset: 0x0000EE04
		private static void InitColorNameTable()
		{
			string[] array = new string[175];
			array[1] = "ActiveBorder";
			array[2] = "ActiveCaption";
			array[3] = "ActiveCaptionText";
			array[4] = "AppWorkspace";
			array[168] = "ButtonFace";
			array[169] = "ButtonHighlight";
			array[170] = "ButtonShadow";
			array[5] = "Control";
			array[6] = "ControlDark";
			array[7] = "ControlDarkDark";
			array[8] = "ControlLight";
			array[9] = "ControlLightLight";
			array[10] = "ControlText";
			array[11] = "Desktop";
			array[171] = "GradientActiveCaption";
			array[172] = "GradientInactiveCaption";
			array[12] = "GrayText";
			array[13] = "Highlight";
			array[14] = "HighlightText";
			array[15] = "HotTrack";
			array[16] = "InactiveBorder";
			array[17] = "InactiveCaption";
			array[18] = "InactiveCaptionText";
			array[19] = "Info";
			array[20] = "InfoText";
			array[21] = "Menu";
			array[173] = "MenuBar";
			array[174] = "MenuHighlight";
			array[22] = "MenuText";
			array[23] = "ScrollBar";
			array[24] = "Window";
			array[25] = "WindowFrame";
			array[26] = "WindowText";
			array[27] = "Transparent";
			array[28] = "AliceBlue";
			array[29] = "AntiqueWhite";
			array[30] = "Aqua";
			array[31] = "Aquamarine";
			array[32] = "Azure";
			array[33] = "Beige";
			array[34] = "Bisque";
			array[35] = "Black";
			array[36] = "BlanchedAlmond";
			array[37] = "Blue";
			array[38] = "BlueViolet";
			array[39] = "Brown";
			array[40] = "BurlyWood";
			array[41] = "CadetBlue";
			array[42] = "Chartreuse";
			array[43] = "Chocolate";
			array[44] = "Coral";
			array[45] = "CornflowerBlue";
			array[46] = "Cornsilk";
			array[47] = "Crimson";
			array[48] = "Cyan";
			array[49] = "DarkBlue";
			array[50] = "DarkCyan";
			array[51] = "DarkGoldenrod";
			array[52] = "DarkGray";
			array[53] = "DarkGreen";
			array[54] = "DarkKhaki";
			array[55] = "DarkMagenta";
			array[56] = "DarkOliveGreen";
			array[57] = "DarkOrange";
			array[58] = "DarkOrchid";
			array[59] = "DarkRed";
			array[60] = "DarkSalmon";
			array[61] = "DarkSeaGreen";
			array[62] = "DarkSlateBlue";
			array[63] = "DarkSlateGray";
			array[64] = "DarkTurquoise";
			array[65] = "DarkViolet";
			array[66] = "DeepPink";
			array[67] = "DeepSkyBlue";
			array[68] = "DimGray";
			array[69] = "DodgerBlue";
			array[70] = "Firebrick";
			array[71] = "FloralWhite";
			array[72] = "ForestGreen";
			array[73] = "Fuchsia";
			array[74] = "Gainsboro";
			array[75] = "GhostWhite";
			array[76] = "Gold";
			array[77] = "Goldenrod";
			array[78] = "Gray";
			array[79] = "Green";
			array[80] = "GreenYellow";
			array[81] = "Honeydew";
			array[82] = "HotPink";
			array[83] = "IndianRed";
			array[84] = "Indigo";
			array[85] = "Ivory";
			array[86] = "Khaki";
			array[87] = "Lavender";
			array[88] = "LavenderBlush";
			array[89] = "LawnGreen";
			array[90] = "LemonChiffon";
			array[91] = "LightBlue";
			array[92] = "LightCoral";
			array[93] = "LightCyan";
			array[94] = "LightGoldenrodYellow";
			array[95] = "LightGray";
			array[96] = "LightGreen";
			array[97] = "LightPink";
			array[98] = "LightSalmon";
			array[99] = "LightSeaGreen";
			array[100] = "LightSkyBlue";
			array[101] = "LightSlateGray";
			array[102] = "LightSteelBlue";
			array[103] = "LightYellow";
			array[104] = "Lime";
			array[105] = "LimeGreen";
			array[106] = "Linen";
			array[107] = "Magenta";
			array[108] = "Maroon";
			array[109] = "MediumAquamarine";
			array[110] = "MediumBlue";
			array[111] = "MediumOrchid";
			array[112] = "MediumPurple";
			array[113] = "MediumSeaGreen";
			array[114] = "MediumSlateBlue";
			array[115] = "MediumSpringGreen";
			array[116] = "MediumTurquoise";
			array[117] = "MediumVioletRed";
			array[118] = "MidnightBlue";
			array[119] = "MintCream";
			array[120] = "MistyRose";
			array[121] = "Moccasin";
			array[122] = "NavajoWhite";
			array[123] = "Navy";
			array[124] = "OldLace";
			array[125] = "Olive";
			array[126] = "OliveDrab";
			array[127] = "Orange";
			array[128] = "OrangeRed";
			array[129] = "Orchid";
			array[130] = "PaleGoldenrod";
			array[131] = "PaleGreen";
			array[132] = "PaleTurquoise";
			array[133] = "PaleVioletRed";
			array[134] = "PapayaWhip";
			array[135] = "PeachPuff";
			array[136] = "Peru";
			array[137] = "Pink";
			array[138] = "Plum";
			array[139] = "PowderBlue";
			array[140] = "Purple";
			array[141] = "Red";
			array[142] = "RosyBrown";
			array[143] = "RoyalBlue";
			array[144] = "SaddleBrown";
			array[145] = "Salmon";
			array[146] = "SandyBrown";
			array[147] = "SeaGreen";
			array[148] = "SeaShell";
			array[149] = "Sienna";
			array[150] = "Silver";
			array[151] = "SkyBlue";
			array[152] = "SlateBlue";
			array[153] = "SlateGray";
			array[154] = "Snow";
			array[155] = "SpringGreen";
			array[156] = "SteelBlue";
			array[157] = "Tan";
			array[158] = "Teal";
			array[159] = "Thistle";
			array[160] = "Tomato";
			array[161] = "Turquoise";
			array[162] = "Violet";
			array[163] = "Wheat";
			array[164] = "White";
			array[165] = "WhiteSmoke";
			array[166] = "Yellow";
			array[167] = "YellowGreen";
			KnownColorTable.colorNameTable = array;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x000112C5 File Offset: 0x0000F4C5
		public static int KnownColorToArgb(KnownColor color)
		{
			KnownColorTable.EnsureColorTable();
			if (color <= KnownColor.MenuHighlight)
			{
				return KnownColorTable.colorTable[(int)color];
			}
			return 0;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000112DD File Offset: 0x0000F4DD
		public static string KnownColorToName(KnownColor color)
		{
			KnownColorTable.EnsureColorNameTable();
			if (color <= KnownColor.MenuHighlight)
			{
				return KnownColorTable.colorNameTable[(int)color];
			}
			return null;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x000112F5 File Offset: 0x0000F4F5
		private static int SystemColorToArgb(int index)
		{
			return KnownColorTable.FromWin32Value(SafeNativeMethods.GetSysColor(index));
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00011302 File Offset: 0x0000F502
		private static int Encode(int alpha, int red, int green, int blue)
		{
			return (red << 16) | (green << 8) | blue | (alpha << 24);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00011313 File Offset: 0x0000F513
		private static int FromWin32Value(int value)
		{
			return KnownColorTable.Encode(255, value & 255, (value >> 8) & 255, (value >> 16) & 255);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00011339 File Offset: 0x0000F539
		private static void OnUserPreferenceChanging(object sender, UserPreferenceChangingEventArgs e)
		{
			if (e.Category == UserPreferenceCategory.Color && KnownColorTable.colorTable != null)
			{
				KnownColorTable.UpdateSystemColors(KnownColorTable.colorTable);
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00011358 File Offset: 0x0000F558
		private static void UpdateSystemColors(int[] colorTable)
		{
			colorTable[1] = KnownColorTable.SystemColorToArgb(10);
			colorTable[2] = KnownColorTable.SystemColorToArgb(2);
			colorTable[3] = KnownColorTable.SystemColorToArgb(9);
			colorTable[4] = KnownColorTable.SystemColorToArgb(12);
			colorTable[168] = KnownColorTable.SystemColorToArgb(15);
			colorTable[169] = KnownColorTable.SystemColorToArgb(20);
			colorTable[170] = KnownColorTable.SystemColorToArgb(16);
			colorTable[5] = KnownColorTable.SystemColorToArgb(15);
			colorTable[6] = KnownColorTable.SystemColorToArgb(16);
			colorTable[7] = KnownColorTable.SystemColorToArgb(21);
			colorTable[8] = KnownColorTable.SystemColorToArgb(22);
			colorTable[9] = KnownColorTable.SystemColorToArgb(20);
			colorTable[10] = KnownColorTable.SystemColorToArgb(18);
			colorTable[11] = KnownColorTable.SystemColorToArgb(1);
			colorTable[171] = KnownColorTable.SystemColorToArgb(27);
			colorTable[172] = KnownColorTable.SystemColorToArgb(28);
			colorTable[12] = KnownColorTable.SystemColorToArgb(17);
			colorTable[13] = KnownColorTable.SystemColorToArgb(13);
			colorTable[14] = KnownColorTable.SystemColorToArgb(14);
			colorTable[15] = KnownColorTable.SystemColorToArgb(26);
			colorTable[16] = KnownColorTable.SystemColorToArgb(11);
			colorTable[17] = KnownColorTable.SystemColorToArgb(3);
			colorTable[18] = KnownColorTable.SystemColorToArgb(19);
			colorTable[19] = KnownColorTable.SystemColorToArgb(24);
			colorTable[20] = KnownColorTable.SystemColorToArgb(23);
			colorTable[21] = KnownColorTable.SystemColorToArgb(4);
			colorTable[173] = KnownColorTable.SystemColorToArgb(30);
			colorTable[174] = KnownColorTable.SystemColorToArgb(29);
			colorTable[22] = KnownColorTable.SystemColorToArgb(7);
			colorTable[23] = KnownColorTable.SystemColorToArgb(0);
			colorTable[24] = KnownColorTable.SystemColorToArgb(5);
			colorTable[25] = KnownColorTable.SystemColorToArgb(6);
			colorTable[26] = KnownColorTable.SystemColorToArgb(8);
		}

		// Token: 0x04000250 RID: 592
		private static int[] colorTable;

		// Token: 0x04000251 RID: 593
		private static string[] colorNameTable;

		// Token: 0x04000252 RID: 594
		private const int AlphaShift = 24;

		// Token: 0x04000253 RID: 595
		private const int RedShift = 16;

		// Token: 0x04000254 RID: 596
		private const int GreenShift = 8;

		// Token: 0x04000255 RID: 597
		private const int BlueShift = 0;

		// Token: 0x04000256 RID: 598
		private const int Win32RedShift = 0;

		// Token: 0x04000257 RID: 599
		private const int Win32GreenShift = 8;

		// Token: 0x04000258 RID: 600
		private const int Win32BlueShift = 16;
	}
}
