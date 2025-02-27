﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Internal;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000303 RID: 771
	internal static class NativeMethods
	{
		// Token: 0x06003130 RID: 12592 RVA: 0x000DD678 File Offset: 0x000DB878
		public static int MAKELANGID(int primary, int sub)
		{
			return ((int)((ushort)sub) << 10) | (int)((ushort)primary);
		}

		// Token: 0x06003131 RID: 12593 RVA: 0x000DD682 File Offset: 0x000DB882
		public static int MAKELCID(int lgid)
		{
			return NativeMethods.MAKELCID(lgid, 0);
		}

		// Token: 0x06003132 RID: 12594 RVA: 0x000DD68B File Offset: 0x000DB88B
		public static int MAKELCID(int lgid, int sort)
		{
			return (65535 & lgid) | ((15 & sort) << 16);
		}

		// Token: 0x06003133 RID: 12595 RVA: 0x000DD69C File Offset: 0x000DB89C
		public static bool Succeeded(int hr)
		{
			return hr >= 0;
		}

		// Token: 0x06003134 RID: 12596 RVA: 0x000DD6A5 File Offset: 0x000DB8A5
		public static bool Failed(int hr)
		{
			return hr < 0;
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x06003135 RID: 12597 RVA: 0x000DD6AB File Offset: 0x000DB8AB
		public static int WM_MOUSEENTER
		{
			get
			{
				if (NativeMethods.wmMouseEnterMessage == -1)
				{
					NativeMethods.wmMouseEnterMessage = SafeNativeMethods.RegisterWindowMessage("WinFormsMouseEnter");
				}
				return NativeMethods.wmMouseEnterMessage;
			}
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x06003136 RID: 12598 RVA: 0x000DD6C9 File Offset: 0x000DB8C9
		public static int WM_UIUNSUBCLASS
		{
			get
			{
				if (NativeMethods.wmUnSubclass == -1)
				{
					NativeMethods.wmUnSubclass = SafeNativeMethods.RegisterWindowMessage("WinFormsUnSubclass");
				}
				return NativeMethods.wmUnSubclass;
			}
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x000DD6E8 File Offset: 0x000DB8E8
		static NativeMethods()
		{
			if (Marshal.SystemDefaultCharSize == 1)
			{
				NativeMethods.BFFM_SETSELECTION = 1126;
				NativeMethods.CBEM_GETITEM = 1028;
				NativeMethods.CBEM_SETITEM = 1029;
				NativeMethods.CBEN_ENDEDIT = -805;
				NativeMethods.CBEM_INSERTITEM = 1025;
				NativeMethods.LVM_GETITEMTEXT = 4141;
				NativeMethods.LVM_SETITEMTEXT = 4142;
				NativeMethods.ACM_OPEN = 1124;
				NativeMethods.DTM_SETFORMAT = 4101;
				NativeMethods.DTN_USERSTRING = -758;
				NativeMethods.DTN_WMKEYDOWN = -757;
				NativeMethods.DTN_FORMAT = -756;
				NativeMethods.DTN_FORMATQUERY = -755;
				NativeMethods.EMR_POLYTEXTOUT = 96;
				NativeMethods.HDM_INSERTITEM = 4609;
				NativeMethods.HDM_GETITEM = 4611;
				NativeMethods.HDM_SETITEM = 4612;
				NativeMethods.HDN_ITEMCHANGING = -300;
				NativeMethods.HDN_ITEMCHANGED = -301;
				NativeMethods.HDN_ITEMCLICK = -302;
				NativeMethods.HDN_ITEMDBLCLICK = -303;
				NativeMethods.HDN_DIVIDERDBLCLICK = -305;
				NativeMethods.HDN_BEGINTRACK = -306;
				NativeMethods.HDN_ENDTRACK = -307;
				NativeMethods.HDN_TRACK = -308;
				NativeMethods.HDN_GETDISPINFO = -309;
				NativeMethods.LVM_SETBKIMAGE = 4164;
				NativeMethods.LVM_GETITEM = 4101;
				NativeMethods.LVM_SETITEM = 4102;
				NativeMethods.LVM_INSERTITEM = 4103;
				NativeMethods.LVM_FINDITEM = 4109;
				NativeMethods.LVM_GETSTRINGWIDTH = 4113;
				NativeMethods.LVM_EDITLABEL = 4119;
				NativeMethods.LVM_GETCOLUMN = 4121;
				NativeMethods.LVM_SETCOLUMN = 4122;
				NativeMethods.LVM_GETISEARCHSTRING = 4148;
				NativeMethods.LVM_INSERTCOLUMN = 4123;
				NativeMethods.LVN_BEGINLABELEDIT = -105;
				NativeMethods.LVN_ENDLABELEDIT = -106;
				NativeMethods.LVN_ODFINDITEM = -152;
				NativeMethods.LVN_GETDISPINFO = -150;
				NativeMethods.LVN_GETINFOTIP = -157;
				NativeMethods.LVN_SETDISPINFO = -151;
				NativeMethods.PSM_SETTITLE = 1135;
				NativeMethods.PSM_SETFINISHTEXT = 1139;
				NativeMethods.RB_INSERTBAND = 1025;
				NativeMethods.SB_SETTEXT = 1025;
				NativeMethods.SB_GETTEXT = 1026;
				NativeMethods.SB_GETTEXTLENGTH = 1027;
				NativeMethods.SB_SETTIPTEXT = 1040;
				NativeMethods.SB_GETTIPTEXT = 1042;
				NativeMethods.TB_SAVERESTORE = 1050;
				NativeMethods.TB_ADDSTRING = 1052;
				NativeMethods.TB_GETBUTTONTEXT = 1069;
				NativeMethods.TB_MAPACCELERATOR = 1102;
				NativeMethods.TB_GETBUTTONINFO = 1089;
				NativeMethods.TB_SETBUTTONINFO = 1090;
				NativeMethods.TB_INSERTBUTTON = 1045;
				NativeMethods.TB_ADDBUTTONS = 1044;
				NativeMethods.TBN_GETBUTTONINFO = -700;
				NativeMethods.TBN_GETINFOTIP = -718;
				NativeMethods.TBN_GETDISPINFO = -716;
				NativeMethods.TTM_ADDTOOL = 1028;
				NativeMethods.TTM_SETTITLE = 1056;
				NativeMethods.TTM_DELTOOL = 1029;
				NativeMethods.TTM_NEWTOOLRECT = 1030;
				NativeMethods.TTM_GETTOOLINFO = 1032;
				NativeMethods.TTM_SETTOOLINFO = 1033;
				NativeMethods.TTM_HITTEST = 1034;
				NativeMethods.TTM_GETTEXT = 1035;
				NativeMethods.TTM_UPDATETIPTEXT = 1036;
				NativeMethods.TTM_ENUMTOOLS = 1038;
				NativeMethods.TTM_GETCURRENTTOOL = 1039;
				NativeMethods.TTN_GETDISPINFO = -520;
				NativeMethods.TTN_NEEDTEXT = -520;
				NativeMethods.TVM_INSERTITEM = 4352;
				NativeMethods.TVM_GETITEM = 4364;
				NativeMethods.TVM_SETITEM = 4365;
				NativeMethods.TVM_EDITLABEL = 4366;
				NativeMethods.TVM_GETISEARCHSTRING = 4375;
				NativeMethods.TVN_SELCHANGING = -401;
				NativeMethods.TVN_SELCHANGED = -402;
				NativeMethods.TVN_GETDISPINFO = -403;
				NativeMethods.TVN_SETDISPINFO = -404;
				NativeMethods.TVN_ITEMEXPANDING = -405;
				NativeMethods.TVN_ITEMEXPANDED = -406;
				NativeMethods.TVN_BEGINDRAG = -407;
				NativeMethods.TVN_BEGINRDRAG = -408;
				NativeMethods.TVN_BEGINLABELEDIT = -410;
				NativeMethods.TVN_ENDLABELEDIT = -411;
				NativeMethods.TCM_GETITEM = 4869;
				NativeMethods.TCM_SETITEM = 4870;
				NativeMethods.TCM_INSERTITEM = 4871;
				return;
			}
			NativeMethods.BFFM_SETSELECTION = 1127;
			NativeMethods.CBEM_GETITEM = 1037;
			NativeMethods.CBEM_SETITEM = 1036;
			NativeMethods.CBEN_ENDEDIT = -806;
			NativeMethods.CBEM_INSERTITEM = 1035;
			NativeMethods.LVM_GETITEMTEXT = 4211;
			NativeMethods.LVM_SETITEMTEXT = 4212;
			NativeMethods.ACM_OPEN = 1127;
			NativeMethods.DTM_SETFORMAT = 4146;
			NativeMethods.DTN_USERSTRING = -745;
			NativeMethods.DTN_WMKEYDOWN = -744;
			NativeMethods.DTN_FORMAT = -743;
			NativeMethods.DTN_FORMATQUERY = -742;
			NativeMethods.EMR_POLYTEXTOUT = 97;
			NativeMethods.HDM_INSERTITEM = 4618;
			NativeMethods.HDM_GETITEM = 4619;
			NativeMethods.HDM_SETITEM = 4620;
			NativeMethods.HDN_ITEMCHANGING = -320;
			NativeMethods.HDN_ITEMCHANGED = -321;
			NativeMethods.HDN_ITEMCLICK = -322;
			NativeMethods.HDN_ITEMDBLCLICK = -323;
			NativeMethods.HDN_DIVIDERDBLCLICK = -325;
			NativeMethods.HDN_BEGINTRACK = -326;
			NativeMethods.HDN_ENDTRACK = -327;
			NativeMethods.HDN_TRACK = -328;
			NativeMethods.HDN_GETDISPINFO = -329;
			NativeMethods.LVM_SETBKIMAGE = 4234;
			NativeMethods.LVM_GETITEM = 4171;
			NativeMethods.LVM_SETITEM = 4172;
			NativeMethods.LVM_INSERTITEM = 4173;
			NativeMethods.LVM_FINDITEM = 4179;
			NativeMethods.LVM_GETSTRINGWIDTH = 4183;
			NativeMethods.LVM_EDITLABEL = 4214;
			NativeMethods.LVM_GETCOLUMN = 4191;
			NativeMethods.LVM_SETCOLUMN = 4192;
			NativeMethods.LVM_GETISEARCHSTRING = 4213;
			NativeMethods.LVM_INSERTCOLUMN = 4193;
			NativeMethods.LVN_BEGINLABELEDIT = -175;
			NativeMethods.LVN_ENDLABELEDIT = -176;
			NativeMethods.LVN_ODFINDITEM = -179;
			NativeMethods.LVN_GETDISPINFO = -177;
			NativeMethods.LVN_GETINFOTIP = -158;
			NativeMethods.LVN_SETDISPINFO = -178;
			NativeMethods.PSM_SETTITLE = 1144;
			NativeMethods.PSM_SETFINISHTEXT = 1145;
			NativeMethods.RB_INSERTBAND = 1034;
			NativeMethods.SB_SETTEXT = 1035;
			NativeMethods.SB_GETTEXT = 1037;
			NativeMethods.SB_GETTEXTLENGTH = 1036;
			NativeMethods.SB_SETTIPTEXT = 1041;
			NativeMethods.SB_GETTIPTEXT = 1043;
			NativeMethods.TB_SAVERESTORE = 1100;
			NativeMethods.TB_ADDSTRING = 1101;
			NativeMethods.TB_GETBUTTONTEXT = 1099;
			NativeMethods.TB_MAPACCELERATOR = 1114;
			NativeMethods.TB_GETBUTTONINFO = 1087;
			NativeMethods.TB_SETBUTTONINFO = 1088;
			NativeMethods.TB_INSERTBUTTON = 1091;
			NativeMethods.TB_ADDBUTTONS = 1092;
			NativeMethods.TBN_GETBUTTONINFO = -720;
			NativeMethods.TBN_GETINFOTIP = -719;
			NativeMethods.TBN_GETDISPINFO = -717;
			NativeMethods.TTM_ADDTOOL = 1074;
			NativeMethods.TTM_SETTITLE = 1057;
			NativeMethods.TTM_DELTOOL = 1075;
			NativeMethods.TTM_NEWTOOLRECT = 1076;
			NativeMethods.TTM_GETTOOLINFO = 1077;
			NativeMethods.TTM_SETTOOLINFO = 1078;
			NativeMethods.TTM_HITTEST = 1079;
			NativeMethods.TTM_GETTEXT = 1080;
			NativeMethods.TTM_UPDATETIPTEXT = 1081;
			NativeMethods.TTM_ENUMTOOLS = 1082;
			NativeMethods.TTM_GETCURRENTTOOL = 1083;
			NativeMethods.TTN_GETDISPINFO = -530;
			NativeMethods.TTN_NEEDTEXT = -530;
			NativeMethods.TVM_INSERTITEM = 4402;
			NativeMethods.TVM_GETITEM = 4414;
			NativeMethods.TVM_SETITEM = 4415;
			NativeMethods.TVM_EDITLABEL = 4417;
			NativeMethods.TVM_GETISEARCHSTRING = 4416;
			NativeMethods.TVN_SELCHANGING = -450;
			NativeMethods.TVN_SELCHANGED = -451;
			NativeMethods.TVN_GETDISPINFO = -452;
			NativeMethods.TVN_SETDISPINFO = -453;
			NativeMethods.TVN_ITEMEXPANDING = -454;
			NativeMethods.TVN_ITEMEXPANDED = -455;
			NativeMethods.TVN_BEGINDRAG = -456;
			NativeMethods.TVN_BEGINRDRAG = -457;
			NativeMethods.TVN_BEGINLABELEDIT = -459;
			NativeMethods.TVN_ENDLABELEDIT = -460;
			NativeMethods.TCM_GETITEM = 4924;
			NativeMethods.TCM_SETITEM = 4925;
			NativeMethods.TCM_INSERTITEM = 4926;
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x000DDEE3 File Offset: 0x000DC0E3
		internal static bool HasFlag(int value, int flag)
		{
			return (value & flag) == flag;
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x000DDEEC File Offset: 0x000DC0EC
		internal static string GetLocalPath(string fileName)
		{
			Uri uri = new Uri(fileName);
			return uri.LocalPath + uri.Fragment;
		}

		// Token: 0x04001445 RID: 5189
		public static IntPtr InvalidIntPtr = (IntPtr)(-1);

		// Token: 0x04001446 RID: 5190
		public static IntPtr LPSTR_TEXTCALLBACK = (IntPtr)(-1);

		// Token: 0x04001447 RID: 5191
		public static HandleRef NullHandleRef = new HandleRef(null, IntPtr.Zero);

		// Token: 0x04001448 RID: 5192
		public const int BITMAPINFO_MAX_COLORSIZE = 256;

		// Token: 0x04001449 RID: 5193
		public const int BI_BITFIELDS = 3;

		// Token: 0x0400144A RID: 5194
		public const int STATUS_PENDING = 259;

		// Token: 0x0400144B RID: 5195
		public const int DESKTOP_SWITCHDESKTOP = 256;

		// Token: 0x0400144C RID: 5196
		public const int ERROR_ACCESS_DENIED = 5;

		// Token: 0x0400144D RID: 5197
		public const int FW_DONTCARE = 0;

		// Token: 0x0400144E RID: 5198
		public const int FW_THIN = 100;

		// Token: 0x0400144F RID: 5199
		public const int FW_EXTRALIGHT = 200;

		// Token: 0x04001450 RID: 5200
		public const int FW_ULTRALIGHT = 200;

		// Token: 0x04001451 RID: 5201
		public const int FW_LIGHT = 300;

		// Token: 0x04001452 RID: 5202
		public const int FW_NORMAL = 400;

		// Token: 0x04001453 RID: 5203
		public const int FW_REGULAR = 400;

		// Token: 0x04001454 RID: 5204
		public const int FW_MEDIUM = 500;

		// Token: 0x04001455 RID: 5205
		public const int FW_SEMIBOLD = 600;

		// Token: 0x04001456 RID: 5206
		public const int FW_DEMIBOLD = 600;

		// Token: 0x04001457 RID: 5207
		public const int FW_BOLD = 700;

		// Token: 0x04001458 RID: 5208
		public const int FW_EXTRABOLD = 800;

		// Token: 0x04001459 RID: 5209
		public const int FW_ULTRABOLD = 800;

		// Token: 0x0400145A RID: 5210
		public const int FW_HEAVY = 900;

		// Token: 0x0400145B RID: 5211
		public const int FW_BLACK = 900;

		// Token: 0x0400145C RID: 5212
		public const int ANSI_CHARSET = 0;

		// Token: 0x0400145D RID: 5213
		public const int DEFAULT_CHARSET = 1;

		// Token: 0x0400145E RID: 5214
		public const int OUT_DEFAULT_PRECIS = 0;

		// Token: 0x0400145F RID: 5215
		public const int OUT_TT_PRECIS = 4;

		// Token: 0x04001460 RID: 5216
		public const int OUT_TT_ONLY_PRECIS = 7;

		// Token: 0x04001461 RID: 5217
		public const int ALTERNATE = 1;

		// Token: 0x04001462 RID: 5218
		public const int WINDING = 2;

		// Token: 0x04001463 RID: 5219
		public const int TA_DEFAULT = 0;

		// Token: 0x04001464 RID: 5220
		public const int BS_SOLID = 0;

		// Token: 0x04001465 RID: 5221
		public const int HOLLOW_BRUSH = 5;

		// Token: 0x04001466 RID: 5222
		public const int R2_BLACK = 1;

		// Token: 0x04001467 RID: 5223
		public const int R2_NOTMERGEPEN = 2;

		// Token: 0x04001468 RID: 5224
		public const int R2_MASKNOTPEN = 3;

		// Token: 0x04001469 RID: 5225
		public const int R2_NOTCOPYPEN = 4;

		// Token: 0x0400146A RID: 5226
		public const int R2_MASKPENNOT = 5;

		// Token: 0x0400146B RID: 5227
		public const int R2_NOT = 6;

		// Token: 0x0400146C RID: 5228
		public const int R2_XORPEN = 7;

		// Token: 0x0400146D RID: 5229
		public const int R2_NOTMASKPEN = 8;

		// Token: 0x0400146E RID: 5230
		public const int R2_MASKPEN = 9;

		// Token: 0x0400146F RID: 5231
		public const int R2_NOTXORPEN = 10;

		// Token: 0x04001470 RID: 5232
		public const int R2_NOP = 11;

		// Token: 0x04001471 RID: 5233
		public const int R2_MERGENOTPEN = 12;

		// Token: 0x04001472 RID: 5234
		public const int R2_COPYPEN = 13;

		// Token: 0x04001473 RID: 5235
		public const int R2_MERGEPENNOT = 14;

		// Token: 0x04001474 RID: 5236
		public const int R2_MERGEPEN = 15;

		// Token: 0x04001475 RID: 5237
		public const int R2_WHITE = 16;

		// Token: 0x04001476 RID: 5238
		public const int GM_COMPATIBLE = 1;

		// Token: 0x04001477 RID: 5239
		public const int GM_ADVANCED = 2;

		// Token: 0x04001478 RID: 5240
		public const int MWT_IDENTITY = 1;

		// Token: 0x04001479 RID: 5241
		public const int PAGE_READONLY = 2;

		// Token: 0x0400147A RID: 5242
		public const int PAGE_READWRITE = 4;

		// Token: 0x0400147B RID: 5243
		public const int PAGE_WRITECOPY = 8;

		// Token: 0x0400147C RID: 5244
		public const int FILE_MAP_COPY = 1;

		// Token: 0x0400147D RID: 5245
		public const int FILE_MAP_WRITE = 2;

		// Token: 0x0400147E RID: 5246
		public const int FILE_MAP_READ = 4;

		// Token: 0x0400147F RID: 5247
		public const int SHGFI_ICON = 256;

		// Token: 0x04001480 RID: 5248
		public const int SHGFI_DISPLAYNAME = 512;

		// Token: 0x04001481 RID: 5249
		public const int SHGFI_TYPENAME = 1024;

		// Token: 0x04001482 RID: 5250
		public const int SHGFI_ATTRIBUTES = 2048;

		// Token: 0x04001483 RID: 5251
		public const int SHGFI_ICONLOCATION = 4096;

		// Token: 0x04001484 RID: 5252
		public const int SHGFI_EXETYPE = 8192;

		// Token: 0x04001485 RID: 5253
		public const int SHGFI_SYSICONINDEX = 16384;

		// Token: 0x04001486 RID: 5254
		public const int SHGFI_LINKOVERLAY = 32768;

		// Token: 0x04001487 RID: 5255
		public const int SHGFI_SELECTED = 65536;

		// Token: 0x04001488 RID: 5256
		public const int SHGFI_ATTR_SPECIFIED = 131072;

		// Token: 0x04001489 RID: 5257
		public const int SHGFI_LARGEICON = 0;

		// Token: 0x0400148A RID: 5258
		public const int SHGFI_SMALLICON = 1;

		// Token: 0x0400148B RID: 5259
		public const int SHGFI_OPENICON = 2;

		// Token: 0x0400148C RID: 5260
		public const int SHGFI_SHELLICONSIZE = 4;

		// Token: 0x0400148D RID: 5261
		public const int SHGFI_PIDL = 8;

		// Token: 0x0400148E RID: 5262
		public const int SHGFI_USEFILEATTRIBUTES = 16;

		// Token: 0x0400148F RID: 5263
		public const int SHGFI_ADDOVERLAYS = 32;

		// Token: 0x04001490 RID: 5264
		public const int SHGFI_OVERLAYINDEX = 64;

		// Token: 0x04001491 RID: 5265
		public const int DM_DISPLAYORIENTATION = 128;

		// Token: 0x04001492 RID: 5266
		public const int AUTOSUGGEST = 268435456;

		// Token: 0x04001493 RID: 5267
		public const int AUTOSUGGEST_OFF = 536870912;

		// Token: 0x04001494 RID: 5268
		public const int AUTOAPPEND = 1073741824;

		// Token: 0x04001495 RID: 5269
		public const int AUTOAPPEND_OFF = -2147483648;

		// Token: 0x04001496 RID: 5270
		public const int ARW_BOTTOMLEFT = 0;

		// Token: 0x04001497 RID: 5271
		public const int ARW_BOTTOMRIGHT = 1;

		// Token: 0x04001498 RID: 5272
		public const int ARW_TOPLEFT = 2;

		// Token: 0x04001499 RID: 5273
		public const int ARW_TOPRIGHT = 3;

		// Token: 0x0400149A RID: 5274
		public const int ARW_LEFT = 0;

		// Token: 0x0400149B RID: 5275
		public const int ARW_RIGHT = 0;

		// Token: 0x0400149C RID: 5276
		public const int ARW_UP = 4;

		// Token: 0x0400149D RID: 5277
		public const int ARW_DOWN = 4;

		// Token: 0x0400149E RID: 5278
		public const int ARW_HIDE = 8;

		// Token: 0x0400149F RID: 5279
		public const int ACM_OPENA = 1124;

		// Token: 0x040014A0 RID: 5280
		public const int ACM_OPENW = 1127;

		// Token: 0x040014A1 RID: 5281
		public const int ADVF_NODATA = 1;

		// Token: 0x040014A2 RID: 5282
		public const int ADVF_ONLYONCE = 4;

		// Token: 0x040014A3 RID: 5283
		public const int ADVF_PRIMEFIRST = 2;

		// Token: 0x040014A4 RID: 5284
		public const int BCM_GETIDEALSIZE = 5633;

		// Token: 0x040014A5 RID: 5285
		public const int BI_RGB = 0;

		// Token: 0x040014A6 RID: 5286
		public const int BS_PATTERN = 3;

		// Token: 0x040014A7 RID: 5287
		public const int BITSPIXEL = 12;

		// Token: 0x040014A8 RID: 5288
		public const int BDR_RAISEDOUTER = 1;

		// Token: 0x040014A9 RID: 5289
		public const int BDR_SUNKENOUTER = 2;

		// Token: 0x040014AA RID: 5290
		public const int BDR_RAISEDINNER = 4;

		// Token: 0x040014AB RID: 5291
		public const int BDR_SUNKENINNER = 8;

		// Token: 0x040014AC RID: 5292
		public const int BDR_RAISED = 5;

		// Token: 0x040014AD RID: 5293
		public const int BDR_SUNKEN = 10;

		// Token: 0x040014AE RID: 5294
		public const int BF_LEFT = 1;

		// Token: 0x040014AF RID: 5295
		public const int BF_TOP = 2;

		// Token: 0x040014B0 RID: 5296
		public const int BF_RIGHT = 4;

		// Token: 0x040014B1 RID: 5297
		public const int BF_BOTTOM = 8;

		// Token: 0x040014B2 RID: 5298
		public const int BF_ADJUST = 8192;

		// Token: 0x040014B3 RID: 5299
		public const int BF_FLAT = 16384;

		// Token: 0x040014B4 RID: 5300
		public const int BF_MIDDLE = 2048;

		// Token: 0x040014B5 RID: 5301
		public const int BFFM_INITIALIZED = 1;

		// Token: 0x040014B6 RID: 5302
		public const int BFFM_SELCHANGED = 2;

		// Token: 0x040014B7 RID: 5303
		public const int BFFM_SETSELECTIONA = 1126;

		// Token: 0x040014B8 RID: 5304
		public const int BFFM_SETSELECTIONW = 1127;

		// Token: 0x040014B9 RID: 5305
		public const int BFFM_ENABLEOK = 1125;

		// Token: 0x040014BA RID: 5306
		public const int BS_PUSHBUTTON = 0;

		// Token: 0x040014BB RID: 5307
		public const int BS_DEFPUSHBUTTON = 1;

		// Token: 0x040014BC RID: 5308
		public const int BS_MULTILINE = 8192;

		// Token: 0x040014BD RID: 5309
		public const int BS_PUSHLIKE = 4096;

		// Token: 0x040014BE RID: 5310
		public const int BS_OWNERDRAW = 11;

		// Token: 0x040014BF RID: 5311
		public const int BS_RADIOBUTTON = 4;

		// Token: 0x040014C0 RID: 5312
		public const int BS_3STATE = 5;

		// Token: 0x040014C1 RID: 5313
		public const int BS_GROUPBOX = 7;

		// Token: 0x040014C2 RID: 5314
		public const int BS_LEFT = 256;

		// Token: 0x040014C3 RID: 5315
		public const int BS_RIGHT = 512;

		// Token: 0x040014C4 RID: 5316
		public const int BS_CENTER = 768;

		// Token: 0x040014C5 RID: 5317
		public const int BS_TOP = 1024;

		// Token: 0x040014C6 RID: 5318
		public const int BS_BOTTOM = 2048;

		// Token: 0x040014C7 RID: 5319
		public const int BS_VCENTER = 3072;

		// Token: 0x040014C8 RID: 5320
		public const int BS_RIGHTBUTTON = 32;

		// Token: 0x040014C9 RID: 5321
		public const int BN_CLICKED = 0;

		// Token: 0x040014CA RID: 5322
		public const int BM_SETCHECK = 241;

		// Token: 0x040014CB RID: 5323
		public const int BM_SETSTATE = 243;

		// Token: 0x040014CC RID: 5324
		public const int BM_CLICK = 245;

		// Token: 0x040014CD RID: 5325
		public const int CDERR_DIALOGFAILURE = 65535;

		// Token: 0x040014CE RID: 5326
		public const int CDERR_STRUCTSIZE = 1;

		// Token: 0x040014CF RID: 5327
		public const int CDERR_INITIALIZATION = 2;

		// Token: 0x040014D0 RID: 5328
		public const int CDERR_NOTEMPLATE = 3;

		// Token: 0x040014D1 RID: 5329
		public const int CDERR_NOHINSTANCE = 4;

		// Token: 0x040014D2 RID: 5330
		public const int CDERR_LOADSTRFAILURE = 5;

		// Token: 0x040014D3 RID: 5331
		public const int CDERR_FINDRESFAILURE = 6;

		// Token: 0x040014D4 RID: 5332
		public const int CDERR_LOADRESFAILURE = 7;

		// Token: 0x040014D5 RID: 5333
		public const int CDERR_LOCKRESFAILURE = 8;

		// Token: 0x040014D6 RID: 5334
		public const int CDERR_MEMALLOCFAILURE = 9;

		// Token: 0x040014D7 RID: 5335
		public const int CDERR_MEMLOCKFAILURE = 10;

		// Token: 0x040014D8 RID: 5336
		public const int CDERR_NOHOOK = 11;

		// Token: 0x040014D9 RID: 5337
		public const int CDERR_REGISTERMSGFAIL = 12;

		// Token: 0x040014DA RID: 5338
		public const int CFERR_NOFONTS = 8193;

		// Token: 0x040014DB RID: 5339
		public const int CFERR_MAXLESSTHANMIN = 8194;

		// Token: 0x040014DC RID: 5340
		public const int CC_RGBINIT = 1;

		// Token: 0x040014DD RID: 5341
		public const int CC_FULLOPEN = 2;

		// Token: 0x040014DE RID: 5342
		public const int CC_PREVENTFULLOPEN = 4;

		// Token: 0x040014DF RID: 5343
		public const int CC_SHOWHELP = 8;

		// Token: 0x040014E0 RID: 5344
		public const int CC_ENABLEHOOK = 16;

		// Token: 0x040014E1 RID: 5345
		public const int CC_SOLIDCOLOR = 128;

		// Token: 0x040014E2 RID: 5346
		public const int CC_ANYCOLOR = 256;

		// Token: 0x040014E3 RID: 5347
		public const int CF_SCREENFONTS = 1;

		// Token: 0x040014E4 RID: 5348
		public const int CF_SHOWHELP = 4;

		// Token: 0x040014E5 RID: 5349
		public const int CF_ENABLEHOOK = 8;

		// Token: 0x040014E6 RID: 5350
		public const int CF_INITTOLOGFONTSTRUCT = 64;

		// Token: 0x040014E7 RID: 5351
		public const int CF_EFFECTS = 256;

		// Token: 0x040014E8 RID: 5352
		public const int CF_APPLY = 512;

		// Token: 0x040014E9 RID: 5353
		public const int CF_SCRIPTSONLY = 1024;

		// Token: 0x040014EA RID: 5354
		public const int CF_NOVECTORFONTS = 2048;

		// Token: 0x040014EB RID: 5355
		public const int CF_NOSIMULATIONS = 4096;

		// Token: 0x040014EC RID: 5356
		public const int CF_LIMITSIZE = 8192;

		// Token: 0x040014ED RID: 5357
		public const int CF_FIXEDPITCHONLY = 16384;

		// Token: 0x040014EE RID: 5358
		public const int CF_FORCEFONTEXIST = 65536;

		// Token: 0x040014EF RID: 5359
		public const int CF_TTONLY = 262144;

		// Token: 0x040014F0 RID: 5360
		public const int CF_SELECTSCRIPT = 4194304;

		// Token: 0x040014F1 RID: 5361
		public const int CF_NOVERTFONTS = 16777216;

		// Token: 0x040014F2 RID: 5362
		public const int CP_WINANSI = 1004;

		// Token: 0x040014F3 RID: 5363
		public const int cmb4 = 1139;

		// Token: 0x040014F4 RID: 5364
		public const int CS_DBLCLKS = 8;

		// Token: 0x040014F5 RID: 5365
		public const int CS_DROPSHADOW = 131072;

		// Token: 0x040014F6 RID: 5366
		public const int CS_SAVEBITS = 2048;

		// Token: 0x040014F7 RID: 5367
		public const int CF_TEXT = 1;

		// Token: 0x040014F8 RID: 5368
		public const int CF_BITMAP = 2;

		// Token: 0x040014F9 RID: 5369
		public const int CF_METAFILEPICT = 3;

		// Token: 0x040014FA RID: 5370
		public const int CF_SYLK = 4;

		// Token: 0x040014FB RID: 5371
		public const int CF_DIF = 5;

		// Token: 0x040014FC RID: 5372
		public const int CF_TIFF = 6;

		// Token: 0x040014FD RID: 5373
		public const int CF_OEMTEXT = 7;

		// Token: 0x040014FE RID: 5374
		public const int CF_DIB = 8;

		// Token: 0x040014FF RID: 5375
		public const int CF_PALETTE = 9;

		// Token: 0x04001500 RID: 5376
		public const int CF_PENDATA = 10;

		// Token: 0x04001501 RID: 5377
		public const int CF_RIFF = 11;

		// Token: 0x04001502 RID: 5378
		public const int CF_WAVE = 12;

		// Token: 0x04001503 RID: 5379
		public const int CF_UNICODETEXT = 13;

		// Token: 0x04001504 RID: 5380
		public const int CF_ENHMETAFILE = 14;

		// Token: 0x04001505 RID: 5381
		public const int CF_HDROP = 15;

		// Token: 0x04001506 RID: 5382
		public const int CF_LOCALE = 16;

		// Token: 0x04001507 RID: 5383
		public const int CLSCTX_INPROC_SERVER = 1;

		// Token: 0x04001508 RID: 5384
		public const int CLSCTX_LOCAL_SERVER = 4;

		// Token: 0x04001509 RID: 5385
		public const int CW_USEDEFAULT = -2147483648;

		// Token: 0x0400150A RID: 5386
		public const int CWP_SKIPINVISIBLE = 1;

		// Token: 0x0400150B RID: 5387
		public const int COLOR_WINDOW = 5;

		// Token: 0x0400150C RID: 5388
		public const int COLOR_WINDOWTEXT = 8;

		// Token: 0x0400150D RID: 5389
		public const int CB_ERR = -1;

		// Token: 0x0400150E RID: 5390
		public const int CBN_SELCHANGE = 1;

		// Token: 0x0400150F RID: 5391
		public const int CBN_DBLCLK = 2;

		// Token: 0x04001510 RID: 5392
		public const int CBN_EDITCHANGE = 5;

		// Token: 0x04001511 RID: 5393
		public const int CBN_EDITUPDATE = 6;

		// Token: 0x04001512 RID: 5394
		public const int CBN_DROPDOWN = 7;

		// Token: 0x04001513 RID: 5395
		public const int CBN_CLOSEUP = 8;

		// Token: 0x04001514 RID: 5396
		public const int CBN_SELENDOK = 9;

		// Token: 0x04001515 RID: 5397
		public const int CBS_SIMPLE = 1;

		// Token: 0x04001516 RID: 5398
		public const int CBS_DROPDOWN = 2;

		// Token: 0x04001517 RID: 5399
		public const int CBS_DROPDOWNLIST = 3;

		// Token: 0x04001518 RID: 5400
		public const int CBS_OWNERDRAWFIXED = 16;

		// Token: 0x04001519 RID: 5401
		public const int CBS_OWNERDRAWVARIABLE = 32;

		// Token: 0x0400151A RID: 5402
		public const int CBS_AUTOHSCROLL = 64;

		// Token: 0x0400151B RID: 5403
		public const int CBS_HASSTRINGS = 512;

		// Token: 0x0400151C RID: 5404
		public const int CBS_NOINTEGRALHEIGHT = 1024;

		// Token: 0x0400151D RID: 5405
		public const int CB_GETEDITSEL = 320;

		// Token: 0x0400151E RID: 5406
		public const int CB_LIMITTEXT = 321;

		// Token: 0x0400151F RID: 5407
		public const int CB_SETEDITSEL = 322;

		// Token: 0x04001520 RID: 5408
		public const int CB_ADDSTRING = 323;

		// Token: 0x04001521 RID: 5409
		public const int CB_DELETESTRING = 324;

		// Token: 0x04001522 RID: 5410
		public const int CB_GETCURSEL = 327;

		// Token: 0x04001523 RID: 5411
		public const int CB_GETLBTEXT = 328;

		// Token: 0x04001524 RID: 5412
		public const int CB_GETLBTEXTLEN = 329;

		// Token: 0x04001525 RID: 5413
		public const int CB_INSERTSTRING = 330;

		// Token: 0x04001526 RID: 5414
		public const int CB_RESETCONTENT = 331;

		// Token: 0x04001527 RID: 5415
		public const int CB_FINDSTRING = 332;

		// Token: 0x04001528 RID: 5416
		public const int CB_SETCURSEL = 334;

		// Token: 0x04001529 RID: 5417
		public const int CB_SHOWDROPDOWN = 335;

		// Token: 0x0400152A RID: 5418
		public const int CB_GETITEMDATA = 336;

		// Token: 0x0400152B RID: 5419
		public const int CB_SETITEMHEIGHT = 339;

		// Token: 0x0400152C RID: 5420
		public const int CB_GETITEMHEIGHT = 340;

		// Token: 0x0400152D RID: 5421
		public const int CB_GETDROPPEDSTATE = 343;

		// Token: 0x0400152E RID: 5422
		public const int CB_FINDSTRINGEXACT = 344;

		// Token: 0x0400152F RID: 5423
		public const int CB_GETTOPINDEX = 347;

		// Token: 0x04001530 RID: 5424
		public const int CB_SETTOPINDEX = 348;

		// Token: 0x04001531 RID: 5425
		public const int CB_GETDROPPEDWIDTH = 351;

		// Token: 0x04001532 RID: 5426
		public const int CB_SETDROPPEDWIDTH = 352;

		// Token: 0x04001533 RID: 5427
		public const int CDRF_DODEFAULT = 0;

		// Token: 0x04001534 RID: 5428
		public const int CDRF_NEWFONT = 2;

		// Token: 0x04001535 RID: 5429
		public const int CDRF_SKIPDEFAULT = 4;

		// Token: 0x04001536 RID: 5430
		public const int CDRF_NOTIFYPOSTPAINT = 16;

		// Token: 0x04001537 RID: 5431
		public const int CDRF_NOTIFYITEMDRAW = 32;

		// Token: 0x04001538 RID: 5432
		public const int CDRF_NOTIFYSUBITEMDRAW = 32;

		// Token: 0x04001539 RID: 5433
		public const int CDDS_PREPAINT = 1;

		// Token: 0x0400153A RID: 5434
		public const int CDDS_POSTPAINT = 2;

		// Token: 0x0400153B RID: 5435
		public const int CDDS_ITEM = 65536;

		// Token: 0x0400153C RID: 5436
		public const int CDDS_SUBITEM = 131072;

		// Token: 0x0400153D RID: 5437
		public const int CDDS_ITEMPREPAINT = 65537;

		// Token: 0x0400153E RID: 5438
		public const int CDDS_ITEMPOSTPAINT = 65538;

		// Token: 0x0400153F RID: 5439
		public const int CDIS_SELECTED = 1;

		// Token: 0x04001540 RID: 5440
		public const int CDIS_GRAYED = 2;

		// Token: 0x04001541 RID: 5441
		public const int CDIS_DISABLED = 4;

		// Token: 0x04001542 RID: 5442
		public const int CDIS_CHECKED = 8;

		// Token: 0x04001543 RID: 5443
		public const int CDIS_FOCUS = 16;

		// Token: 0x04001544 RID: 5444
		public const int CDIS_DEFAULT = 32;

		// Token: 0x04001545 RID: 5445
		public const int CDIS_HOT = 64;

		// Token: 0x04001546 RID: 5446
		public const int CDIS_MARKED = 128;

		// Token: 0x04001547 RID: 5447
		public const int CDIS_INDETERMINATE = 256;

		// Token: 0x04001548 RID: 5448
		public const int CDIS_SHOWKEYBOARDCUES = 512;

		// Token: 0x04001549 RID: 5449
		public const int CLR_NONE = -1;

		// Token: 0x0400154A RID: 5450
		public const int CLR_DEFAULT = -16777216;

		// Token: 0x0400154B RID: 5451
		public const int CCM_SETVERSION = 8199;

		// Token: 0x0400154C RID: 5452
		public const int CCM_GETVERSION = 8200;

		// Token: 0x0400154D RID: 5453
		public const int CCS_NORESIZE = 4;

		// Token: 0x0400154E RID: 5454
		public const int CCS_NOPARENTALIGN = 8;

		// Token: 0x0400154F RID: 5455
		public const int CCS_NODIVIDER = 64;

		// Token: 0x04001550 RID: 5456
		public const int CBEM_INSERTITEMA = 1025;

		// Token: 0x04001551 RID: 5457
		public const int CBEM_GETITEMA = 1028;

		// Token: 0x04001552 RID: 5458
		public const int CBEM_SETITEMA = 1029;

		// Token: 0x04001553 RID: 5459
		public const int CBEM_INSERTITEMW = 1035;

		// Token: 0x04001554 RID: 5460
		public const int CBEM_SETITEMW = 1036;

		// Token: 0x04001555 RID: 5461
		public const int CBEM_GETITEMW = 1037;

		// Token: 0x04001556 RID: 5462
		public const int CBEN_ENDEDITA = -805;

		// Token: 0x04001557 RID: 5463
		public const int CBEN_ENDEDITW = -806;

		// Token: 0x04001558 RID: 5464
		public const int CONNECT_E_NOCONNECTION = -2147220992;

		// Token: 0x04001559 RID: 5465
		public const int CONNECT_E_CANNOTCONNECT = -2147220990;

		// Token: 0x0400155A RID: 5466
		public const int CTRLINFO_EATS_RETURN = 1;

		// Token: 0x0400155B RID: 5467
		public const int CTRLINFO_EATS_ESCAPE = 2;

		// Token: 0x0400155C RID: 5468
		public const int CSIDL_DESKTOP = 0;

		// Token: 0x0400155D RID: 5469
		public const int CSIDL_INTERNET = 1;

		// Token: 0x0400155E RID: 5470
		public const int CSIDL_PROGRAMS = 2;

		// Token: 0x0400155F RID: 5471
		public const int CSIDL_PERSONAL = 5;

		// Token: 0x04001560 RID: 5472
		public const int CSIDL_FAVORITES = 6;

		// Token: 0x04001561 RID: 5473
		public const int CSIDL_STARTUP = 7;

		// Token: 0x04001562 RID: 5474
		public const int CSIDL_RECENT = 8;

		// Token: 0x04001563 RID: 5475
		public const int CSIDL_SENDTO = 9;

		// Token: 0x04001564 RID: 5476
		public const int CSIDL_STARTMENU = 11;

		// Token: 0x04001565 RID: 5477
		public const int CSIDL_DESKTOPDIRECTORY = 16;

		// Token: 0x04001566 RID: 5478
		public const int CSIDL_TEMPLATES = 21;

		// Token: 0x04001567 RID: 5479
		public const int CSIDL_APPDATA = 26;

		// Token: 0x04001568 RID: 5480
		public const int CSIDL_LOCAL_APPDATA = 28;

		// Token: 0x04001569 RID: 5481
		public const int CSIDL_INTERNET_CACHE = 32;

		// Token: 0x0400156A RID: 5482
		public const int CSIDL_COOKIES = 33;

		// Token: 0x0400156B RID: 5483
		public const int CSIDL_HISTORY = 34;

		// Token: 0x0400156C RID: 5484
		public const int CSIDL_COMMON_APPDATA = 35;

		// Token: 0x0400156D RID: 5485
		public const int CSIDL_SYSTEM = 37;

		// Token: 0x0400156E RID: 5486
		public const int CSIDL_PROGRAM_FILES = 38;

		// Token: 0x0400156F RID: 5487
		public const int CSIDL_PROGRAM_FILES_COMMON = 43;

		// Token: 0x04001570 RID: 5488
		public const int DUPLICATE = 6;

		// Token: 0x04001571 RID: 5489
		public const int DISPID_UNKNOWN = -1;

		// Token: 0x04001572 RID: 5490
		public const int DISPID_PROPERTYPUT = -3;

		// Token: 0x04001573 RID: 5491
		public const int DISPATCH_METHOD = 1;

		// Token: 0x04001574 RID: 5492
		public const int DISPATCH_PROPERTYGET = 2;

		// Token: 0x04001575 RID: 5493
		public const int DISPATCH_PROPERTYPUT = 4;

		// Token: 0x04001576 RID: 5494
		public const int DV_E_DVASPECT = -2147221397;

		// Token: 0x04001577 RID: 5495
		public const int DISP_E_MEMBERNOTFOUND = -2147352573;

		// Token: 0x04001578 RID: 5496
		public const int DISP_E_PARAMNOTFOUND = -2147352572;

		// Token: 0x04001579 RID: 5497
		public const int DISP_E_EXCEPTION = -2147352567;

		// Token: 0x0400157A RID: 5498
		public const int DEFAULT_GUI_FONT = 17;

		// Token: 0x0400157B RID: 5499
		public const int DIB_RGB_COLORS = 0;

		// Token: 0x0400157C RID: 5500
		public const int DRAGDROP_E_NOTREGISTERED = -2147221248;

		// Token: 0x0400157D RID: 5501
		public const int DRAGDROP_E_ALREADYREGISTERED = -2147221247;

		// Token: 0x0400157E RID: 5502
		public const int DUPLICATE_SAME_ACCESS = 2;

		// Token: 0x0400157F RID: 5503
		public const int DFC_CAPTION = 1;

		// Token: 0x04001580 RID: 5504
		public const int DFC_MENU = 2;

		// Token: 0x04001581 RID: 5505
		public const int DFC_SCROLL = 3;

		// Token: 0x04001582 RID: 5506
		public const int DFC_BUTTON = 4;

		// Token: 0x04001583 RID: 5507
		public const int DFCS_CAPTIONCLOSE = 0;

		// Token: 0x04001584 RID: 5508
		public const int DFCS_CAPTIONMIN = 1;

		// Token: 0x04001585 RID: 5509
		public const int DFCS_CAPTIONMAX = 2;

		// Token: 0x04001586 RID: 5510
		public const int DFCS_CAPTIONRESTORE = 3;

		// Token: 0x04001587 RID: 5511
		public const int DFCS_CAPTIONHELP = 4;

		// Token: 0x04001588 RID: 5512
		public const int DFCS_MENUARROW = 0;

		// Token: 0x04001589 RID: 5513
		public const int DFCS_MENUCHECK = 1;

		// Token: 0x0400158A RID: 5514
		public const int DFCS_MENUBULLET = 2;

		// Token: 0x0400158B RID: 5515
		public const int DFCS_SCROLLUP = 0;

		// Token: 0x0400158C RID: 5516
		public const int DFCS_SCROLLDOWN = 1;

		// Token: 0x0400158D RID: 5517
		public const int DFCS_SCROLLLEFT = 2;

		// Token: 0x0400158E RID: 5518
		public const int DFCS_SCROLLRIGHT = 3;

		// Token: 0x0400158F RID: 5519
		public const int DFCS_SCROLLCOMBOBOX = 5;

		// Token: 0x04001590 RID: 5520
		public const int DFCS_BUTTONCHECK = 0;

		// Token: 0x04001591 RID: 5521
		public const int DFCS_BUTTONRADIO = 4;

		// Token: 0x04001592 RID: 5522
		public const int DFCS_BUTTON3STATE = 8;

		// Token: 0x04001593 RID: 5523
		public const int DFCS_BUTTONPUSH = 16;

		// Token: 0x04001594 RID: 5524
		public const int DFCS_INACTIVE = 256;

		// Token: 0x04001595 RID: 5525
		public const int DFCS_PUSHED = 512;

		// Token: 0x04001596 RID: 5526
		public const int DFCS_CHECKED = 1024;

		// Token: 0x04001597 RID: 5527
		public const int DFCS_FLAT = 16384;

		// Token: 0x04001598 RID: 5528
		public const int DCX_WINDOW = 1;

		// Token: 0x04001599 RID: 5529
		public const int DCX_CACHE = 2;

		// Token: 0x0400159A RID: 5530
		public const int DCX_LOCKWINDOWUPDATE = 1024;

		// Token: 0x0400159B RID: 5531
		public const int DCX_INTERSECTRGN = 128;

		// Token: 0x0400159C RID: 5532
		public const int DI_NORMAL = 3;

		// Token: 0x0400159D RID: 5533
		public const int DLGC_WANTARROWS = 1;

		// Token: 0x0400159E RID: 5534
		public const int DLGC_WANTTAB = 2;

		// Token: 0x0400159F RID: 5535
		public const int DLGC_WANTALLKEYS = 4;

		// Token: 0x040015A0 RID: 5536
		public const int DLGC_WANTCHARS = 128;

		// Token: 0x040015A1 RID: 5537
		public const int DLGC_WANTMESSAGE = 4;

		// Token: 0x040015A2 RID: 5538
		public const int DLGC_HASSETSEL = 8;

		// Token: 0x040015A3 RID: 5539
		public const int DTM_GETSYSTEMTIME = 4097;

		// Token: 0x040015A4 RID: 5540
		public const int DTM_SETSYSTEMTIME = 4098;

		// Token: 0x040015A5 RID: 5541
		public const int DTM_SETRANGE = 4100;

		// Token: 0x040015A6 RID: 5542
		public const int DTM_SETFORMATA = 4101;

		// Token: 0x040015A7 RID: 5543
		public const int DTM_SETFORMATW = 4146;

		// Token: 0x040015A8 RID: 5544
		public const int DTM_SETMCCOLOR = 4102;

		// Token: 0x040015A9 RID: 5545
		public const int DTM_GETMONTHCAL = 4104;

		// Token: 0x040015AA RID: 5546
		public const int DTM_SETMCFONT = 4105;

		// Token: 0x040015AB RID: 5547
		public const int DTM_CLOSEMONTHCAL = 4109;

		// Token: 0x040015AC RID: 5548
		public const int DTS_UPDOWN = 1;

		// Token: 0x040015AD RID: 5549
		public const int DTS_SHOWNONE = 2;

		// Token: 0x040015AE RID: 5550
		public const int DTS_LONGDATEFORMAT = 4;

		// Token: 0x040015AF RID: 5551
		public const int DTS_TIMEFORMAT = 9;

		// Token: 0x040015B0 RID: 5552
		public const int DTS_RIGHTALIGN = 32;

		// Token: 0x040015B1 RID: 5553
		public const int DTN_DATETIMECHANGE = -759;

		// Token: 0x040015B2 RID: 5554
		public const int DTN_USERSTRINGA = -758;

		// Token: 0x040015B3 RID: 5555
		public const int DTN_USERSTRINGW = -745;

		// Token: 0x040015B4 RID: 5556
		public const int DTN_WMKEYDOWNA = -757;

		// Token: 0x040015B5 RID: 5557
		public const int DTN_WMKEYDOWNW = -744;

		// Token: 0x040015B6 RID: 5558
		public const int DTN_FORMATA = -756;

		// Token: 0x040015B7 RID: 5559
		public const int DTN_FORMATW = -743;

		// Token: 0x040015B8 RID: 5560
		public const int DTN_FORMATQUERYA = -755;

		// Token: 0x040015B9 RID: 5561
		public const int DTN_FORMATQUERYW = -742;

		// Token: 0x040015BA RID: 5562
		public const int DTN_DROPDOWN = -754;

		// Token: 0x040015BB RID: 5563
		public const int DTN_CLOSEUP = -753;

		// Token: 0x040015BC RID: 5564
		public const int DVASPECT_CONTENT = 1;

		// Token: 0x040015BD RID: 5565
		public const int DVASPECT_TRANSPARENT = 32;

		// Token: 0x040015BE RID: 5566
		public const int DVASPECT_OPAQUE = 16;

		// Token: 0x040015BF RID: 5567
		public const int E_NOTIMPL = -2147467263;

		// Token: 0x040015C0 RID: 5568
		public const int E_OUTOFMEMORY = -2147024882;

		// Token: 0x040015C1 RID: 5569
		public const int E_INVALIDARG = -2147024809;

		// Token: 0x040015C2 RID: 5570
		public const int E_NOINTERFACE = -2147467262;

		// Token: 0x040015C3 RID: 5571
		public const int E_POINTER = -2147467261;

		// Token: 0x040015C4 RID: 5572
		public const int E_FAIL = -2147467259;

		// Token: 0x040015C5 RID: 5573
		public const int E_ABORT = -2147467260;

		// Token: 0x040015C6 RID: 5574
		public const int E_UNEXPECTED = -2147418113;

		// Token: 0x040015C7 RID: 5575
		public const int INET_E_DEFAULT_ACTION = -2146697199;

		// Token: 0x040015C8 RID: 5576
		public const int ETO_OPAQUE = 2;

		// Token: 0x040015C9 RID: 5577
		public const int ETO_CLIPPED = 4;

		// Token: 0x040015CA RID: 5578
		public const int EMR_POLYTEXTOUTA = 96;

		// Token: 0x040015CB RID: 5579
		public const int EMR_POLYTEXTOUTW = 97;

		// Token: 0x040015CC RID: 5580
		public const int EDGE_RAISED = 5;

		// Token: 0x040015CD RID: 5581
		public const int EDGE_SUNKEN = 10;

		// Token: 0x040015CE RID: 5582
		public const int EDGE_ETCHED = 6;

		// Token: 0x040015CF RID: 5583
		public const int EDGE_BUMP = 9;

		// Token: 0x040015D0 RID: 5584
		public const int ES_LEFT = 0;

		// Token: 0x040015D1 RID: 5585
		public const int ES_CENTER = 1;

		// Token: 0x040015D2 RID: 5586
		public const int ES_RIGHT = 2;

		// Token: 0x040015D3 RID: 5587
		public const int ES_MULTILINE = 4;

		// Token: 0x040015D4 RID: 5588
		public const int ES_UPPERCASE = 8;

		// Token: 0x040015D5 RID: 5589
		public const int ES_LOWERCASE = 16;

		// Token: 0x040015D6 RID: 5590
		public const int ES_AUTOVSCROLL = 64;

		// Token: 0x040015D7 RID: 5591
		public const int ES_AUTOHSCROLL = 128;

		// Token: 0x040015D8 RID: 5592
		public const int ES_NOHIDESEL = 256;

		// Token: 0x040015D9 RID: 5593
		public const int ES_READONLY = 2048;

		// Token: 0x040015DA RID: 5594
		public const int ES_PASSWORD = 32;

		// Token: 0x040015DB RID: 5595
		public const int EN_CHANGE = 768;

		// Token: 0x040015DC RID: 5596
		public const int EN_UPDATE = 1024;

		// Token: 0x040015DD RID: 5597
		public const int EN_HSCROLL = 1537;

		// Token: 0x040015DE RID: 5598
		public const int EN_VSCROLL = 1538;

		// Token: 0x040015DF RID: 5599
		public const int EN_ALIGN_LTR_EC = 1792;

		// Token: 0x040015E0 RID: 5600
		public const int EN_ALIGN_RTL_EC = 1793;

		// Token: 0x040015E1 RID: 5601
		public const int EC_LEFTMARGIN = 1;

		// Token: 0x040015E2 RID: 5602
		public const int EC_RIGHTMARGIN = 2;

		// Token: 0x040015E3 RID: 5603
		public const int EM_GETSEL = 176;

		// Token: 0x040015E4 RID: 5604
		public const int EM_SETSEL = 177;

		// Token: 0x040015E5 RID: 5605
		public const int EM_SCROLL = 181;

		// Token: 0x040015E6 RID: 5606
		public const int EM_SCROLLCARET = 183;

		// Token: 0x040015E7 RID: 5607
		public const int EM_GETMODIFY = 184;

		// Token: 0x040015E8 RID: 5608
		public const int EM_SETMODIFY = 185;

		// Token: 0x040015E9 RID: 5609
		public const int EM_GETLINECOUNT = 186;

		// Token: 0x040015EA RID: 5610
		public const int EM_REPLACESEL = 194;

		// Token: 0x040015EB RID: 5611
		public const int EM_GETLINE = 196;

		// Token: 0x040015EC RID: 5612
		public const int EM_LIMITTEXT = 197;

		// Token: 0x040015ED RID: 5613
		public const int EM_CANUNDO = 198;

		// Token: 0x040015EE RID: 5614
		public const int EM_UNDO = 199;

		// Token: 0x040015EF RID: 5615
		public const int EM_SETPASSWORDCHAR = 204;

		// Token: 0x040015F0 RID: 5616
		public const int EM_GETPASSWORDCHAR = 210;

		// Token: 0x040015F1 RID: 5617
		public const int EM_EMPTYUNDOBUFFER = 205;

		// Token: 0x040015F2 RID: 5618
		public const int EM_SETREADONLY = 207;

		// Token: 0x040015F3 RID: 5619
		public const int EM_SETMARGINS = 211;

		// Token: 0x040015F4 RID: 5620
		public const int EM_POSFROMCHAR = 214;

		// Token: 0x040015F5 RID: 5621
		public const int EM_CHARFROMPOS = 215;

		// Token: 0x040015F6 RID: 5622
		public const int EM_LINEFROMCHAR = 201;

		// Token: 0x040015F7 RID: 5623
		public const int EM_GETFIRSTVISIBLELINE = 206;

		// Token: 0x040015F8 RID: 5624
		public const int EM_LINEINDEX = 187;

		// Token: 0x040015F9 RID: 5625
		public const int EM_GETRECT = 178;

		// Token: 0x040015FA RID: 5626
		public const int EM_SETRECT = 179;

		// Token: 0x040015FB RID: 5627
		public const int EM_SETRECTNP = 180;

		// Token: 0x040015FC RID: 5628
		public const int EM_LINESCROLL = 182;

		// Token: 0x040015FD RID: 5629
		public const int EM_SETHANDLE = 188;

		// Token: 0x040015FE RID: 5630
		public const int EM_GETHANDLE = 189;

		// Token: 0x040015FF RID: 5631
		public const int EM_GETTHUMB = 190;

		// Token: 0x04001600 RID: 5632
		public const int EM_LINELENGTH = 193;

		// Token: 0x04001601 RID: 5633
		public const int EM_FMTLINES = 200;

		// Token: 0x04001602 RID: 5634
		public const int EM_SETTABSTOPS = 203;

		// Token: 0x04001603 RID: 5635
		public const int EM_SETWORDBREAKPROC = 208;

		// Token: 0x04001604 RID: 5636
		public const int EM_GETWORDBREAKPROC = 209;

		// Token: 0x04001605 RID: 5637
		public const int EM_GETMARGINS = 212;

		// Token: 0x04001606 RID: 5638
		public const int EM_SETLIMITTEXT = 197;

		// Token: 0x04001607 RID: 5639
		public const int EM_GETLIMITTEXT = 213;

		// Token: 0x04001608 RID: 5640
		public const int EM_SETIMESTATUS = 216;

		// Token: 0x04001609 RID: 5641
		public const int EM_GETIMESTATUS = 217;

		// Token: 0x0400160A RID: 5642
		public const int EM_ENABLEFEATURE = 218;

		// Token: 0x0400160B RID: 5643
		public const int EM_GETTEXTEX = 1118;

		// Token: 0x0400160C RID: 5644
		public const int EM_GETTEXTLENGTHEX = 1119;

		// Token: 0x0400160D RID: 5645
		public const int ERROR_INVALID_HANDLE = 6;

		// Token: 0x0400160E RID: 5646
		public const int ERROR_CLASS_ALREADY_EXISTS = 1410;

		// Token: 0x0400160F RID: 5647
		public const int FNERR_SUBCLASSFAILURE = 12289;

		// Token: 0x04001610 RID: 5648
		public const int FNERR_INVALIDFILENAME = 12290;

		// Token: 0x04001611 RID: 5649
		public const int FNERR_BUFFERTOOSMALL = 12291;

		// Token: 0x04001612 RID: 5650
		public const int FRERR_BUFFERLENGTHZERO = 16385;

		// Token: 0x04001613 RID: 5651
		public const int FADF_BSTR = 256;

		// Token: 0x04001614 RID: 5652
		public const int FADF_UNKNOWN = 512;

		// Token: 0x04001615 RID: 5653
		public const int FADF_DISPATCH = 1024;

		// Token: 0x04001616 RID: 5654
		public const int FADF_VARIANT = 2048;

		// Token: 0x04001617 RID: 5655
		public const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;

		// Token: 0x04001618 RID: 5656
		public const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;

		// Token: 0x04001619 RID: 5657
		public const int FVIRTKEY = 1;

		// Token: 0x0400161A RID: 5658
		public const int FSHIFT = 4;

		// Token: 0x0400161B RID: 5659
		public const int FALT = 16;

		// Token: 0x0400161C RID: 5660
		public const int GMEM_FIXED = 0;

		// Token: 0x0400161D RID: 5661
		public const int GMEM_MOVEABLE = 2;

		// Token: 0x0400161E RID: 5662
		public const int GMEM_NOCOMPACT = 16;

		// Token: 0x0400161F RID: 5663
		public const int GMEM_NODISCARD = 32;

		// Token: 0x04001620 RID: 5664
		public const int GMEM_ZEROINIT = 64;

		// Token: 0x04001621 RID: 5665
		public const int GMEM_MODIFY = 128;

		// Token: 0x04001622 RID: 5666
		public const int GMEM_DISCARDABLE = 256;

		// Token: 0x04001623 RID: 5667
		public const int GMEM_NOT_BANKED = 4096;

		// Token: 0x04001624 RID: 5668
		public const int GMEM_SHARE = 8192;

		// Token: 0x04001625 RID: 5669
		public const int GMEM_DDESHARE = 8192;

		// Token: 0x04001626 RID: 5670
		public const int GMEM_NOTIFY = 16384;

		// Token: 0x04001627 RID: 5671
		public const int GMEM_LOWER = 4096;

		// Token: 0x04001628 RID: 5672
		public const int GMEM_VALID_FLAGS = 32626;

		// Token: 0x04001629 RID: 5673
		public const int GMEM_INVALID_HANDLE = 32768;

		// Token: 0x0400162A RID: 5674
		public const int GHND = 66;

		// Token: 0x0400162B RID: 5675
		public const int GPTR = 64;

		// Token: 0x0400162C RID: 5676
		public const int GCL_WNDPROC = -24;

		// Token: 0x0400162D RID: 5677
		public const int GCW_ATOM = -32;

		// Token: 0x0400162E RID: 5678
		public const int GWL_WNDPROC = -4;

		// Token: 0x0400162F RID: 5679
		public const int GWL_HWNDPARENT = -8;

		// Token: 0x04001630 RID: 5680
		public const int GWL_STYLE = -16;

		// Token: 0x04001631 RID: 5681
		public const int GWL_EXSTYLE = -20;

		// Token: 0x04001632 RID: 5682
		public const int GWL_ID = -12;

		// Token: 0x04001633 RID: 5683
		public const int GW_HWNDFIRST = 0;

		// Token: 0x04001634 RID: 5684
		public const int GW_HWNDLAST = 1;

		// Token: 0x04001635 RID: 5685
		public const int GW_HWNDNEXT = 2;

		// Token: 0x04001636 RID: 5686
		public const int GW_HWNDPREV = 3;

		// Token: 0x04001637 RID: 5687
		public const int GW_CHILD = 5;

		// Token: 0x04001638 RID: 5688
		public const int GMR_VISIBLE = 0;

		// Token: 0x04001639 RID: 5689
		public const int GMR_DAYSTATE = 1;

		// Token: 0x0400163A RID: 5690
		public const int GDI_ERROR = -1;

		// Token: 0x0400163B RID: 5691
		public const int GDTR_MIN = 1;

		// Token: 0x0400163C RID: 5692
		public const int GDTR_MAX = 2;

		// Token: 0x0400163D RID: 5693
		public const int GDT_VALID = 0;

		// Token: 0x0400163E RID: 5694
		public const int GDT_NONE = 1;

		// Token: 0x0400163F RID: 5695
		public const int GA_PARENT = 1;

		// Token: 0x04001640 RID: 5696
		public const int GA_ROOT = 2;

		// Token: 0x04001641 RID: 5697
		public const int GCS_COMPSTR = 8;

		// Token: 0x04001642 RID: 5698
		public const int GCS_COMPATTR = 16;

		// Token: 0x04001643 RID: 5699
		public const int GCS_RESULTSTR = 2048;

		// Token: 0x04001644 RID: 5700
		public const int ATTR_INPUT = 0;

		// Token: 0x04001645 RID: 5701
		public const int ATTR_TARGET_CONVERTED = 1;

		// Token: 0x04001646 RID: 5702
		public const int ATTR_CONVERTED = 2;

		// Token: 0x04001647 RID: 5703
		public const int ATTR_TARGET_NOTCONVERTED = 3;

		// Token: 0x04001648 RID: 5704
		public const int ATTR_INPUT_ERROR = 4;

		// Token: 0x04001649 RID: 5705
		public const int ATTR_FIXEDCONVERTED = 5;

		// Token: 0x0400164A RID: 5706
		public const int NI_COMPOSITIONSTR = 21;

		// Token: 0x0400164B RID: 5707
		public const int CPS_COMPLETE = 1;

		// Token: 0x0400164C RID: 5708
		public const int CPS_CANCEL = 4;

		// Token: 0x0400164D RID: 5709
		public const int HC_ACTION = 0;

		// Token: 0x0400164E RID: 5710
		public const int HC_GETNEXT = 1;

		// Token: 0x0400164F RID: 5711
		public const int HC_SKIP = 2;

		// Token: 0x04001650 RID: 5712
		public const int HTTRANSPARENT = -1;

		// Token: 0x04001651 RID: 5713
		public const int HTNOWHERE = 0;

		// Token: 0x04001652 RID: 5714
		public const int HTCLIENT = 1;

		// Token: 0x04001653 RID: 5715
		public const int HTLEFT = 10;

		// Token: 0x04001654 RID: 5716
		public const int HTBOTTOM = 15;

		// Token: 0x04001655 RID: 5717
		public const int HTBOTTOMLEFT = 16;

		// Token: 0x04001656 RID: 5718
		public const int HTBOTTOMRIGHT = 17;

		// Token: 0x04001657 RID: 5719
		public const int HTBORDER = 18;

		// Token: 0x04001658 RID: 5720
		public const int HELPINFO_WINDOW = 1;

		// Token: 0x04001659 RID: 5721
		public const int HCF_HIGHCONTRASTON = 1;

		// Token: 0x0400165A RID: 5722
		public const int HDI_ORDER = 128;

		// Token: 0x0400165B RID: 5723
		public const int HDI_WIDTH = 1;

		// Token: 0x0400165C RID: 5724
		public const int HDM_GETITEMCOUNT = 4608;

		// Token: 0x0400165D RID: 5725
		public const int HDM_INSERTITEMA = 4609;

		// Token: 0x0400165E RID: 5726
		public const int HDM_INSERTITEMW = 4618;

		// Token: 0x0400165F RID: 5727
		public const int HDM_GETITEMA = 4611;

		// Token: 0x04001660 RID: 5728
		public const int HDM_GETITEMW = 4619;

		// Token: 0x04001661 RID: 5729
		public const int HDM_LAYOUT = 4613;

		// Token: 0x04001662 RID: 5730
		public const int HDM_SETITEMA = 4612;

		// Token: 0x04001663 RID: 5731
		public const int HDM_SETITEMW = 4620;

		// Token: 0x04001664 RID: 5732
		public const int HDN_ITEMCHANGINGA = -300;

		// Token: 0x04001665 RID: 5733
		public const int HDN_ITEMCHANGINGW = -320;

		// Token: 0x04001666 RID: 5734
		public const int HDN_ITEMCHANGEDA = -301;

		// Token: 0x04001667 RID: 5735
		public const int HDN_ITEMCHANGEDW = -321;

		// Token: 0x04001668 RID: 5736
		public const int HDN_ITEMCLICKA = -302;

		// Token: 0x04001669 RID: 5737
		public const int HDN_ITEMCLICKW = -322;

		// Token: 0x0400166A RID: 5738
		public const int HDN_ITEMDBLCLICKA = -303;

		// Token: 0x0400166B RID: 5739
		public const int HDN_ITEMDBLCLICKW = -323;

		// Token: 0x0400166C RID: 5740
		public const int HDN_DIVIDERDBLCLICKA = -305;

		// Token: 0x0400166D RID: 5741
		public const int HDN_DIVIDERDBLCLICKW = -325;

		// Token: 0x0400166E RID: 5742
		public const int HDN_BEGINTDRAG = -310;

		// Token: 0x0400166F RID: 5743
		public const int HDN_BEGINTRACKA = -306;

		// Token: 0x04001670 RID: 5744
		public const int HDN_BEGINTRACKW = -326;

		// Token: 0x04001671 RID: 5745
		public const int HDN_ENDDRAG = -311;

		// Token: 0x04001672 RID: 5746
		public const int HDN_ENDTRACKA = -307;

		// Token: 0x04001673 RID: 5747
		public const int HDN_ENDTRACKW = -327;

		// Token: 0x04001674 RID: 5748
		public const int HDN_TRACKA = -308;

		// Token: 0x04001675 RID: 5749
		public const int HDN_TRACKW = -328;

		// Token: 0x04001676 RID: 5750
		public const int HDN_GETDISPINFOA = -309;

		// Token: 0x04001677 RID: 5751
		public const int HDN_GETDISPINFOW = -329;

		// Token: 0x04001678 RID: 5752
		public const int HDS_FULLDRAG = 128;

		// Token: 0x04001679 RID: 5753
		public const int HBMMENU_CALLBACK = -1;

		// Token: 0x0400167A RID: 5754
		public const int HBMMENU_SYSTEM = 1;

		// Token: 0x0400167B RID: 5755
		public const int HBMMENU_MBAR_RESTORE = 2;

		// Token: 0x0400167C RID: 5756
		public const int HBMMENU_MBAR_MINIMIZE = 3;

		// Token: 0x0400167D RID: 5757
		public const int HBMMENU_MBAR_CLOSE = 5;

		// Token: 0x0400167E RID: 5758
		public const int HBMMENU_MBAR_CLOSE_D = 6;

		// Token: 0x0400167F RID: 5759
		public const int HBMMENU_MBAR_MINIMIZE_D = 7;

		// Token: 0x04001680 RID: 5760
		public const int HBMMENU_POPUP_CLOSE = 8;

		// Token: 0x04001681 RID: 5761
		public const int HBMMENU_POPUP_RESTORE = 9;

		// Token: 0x04001682 RID: 5762
		public const int HBMMENU_POPUP_MAXIMIZE = 10;

		// Token: 0x04001683 RID: 5763
		public const int HBMMENU_POPUP_MINIMIZE = 11;

		// Token: 0x04001684 RID: 5764
		public static HandleRef HWND_TOP = new HandleRef(null, (IntPtr)0);

		// Token: 0x04001685 RID: 5765
		public static HandleRef HWND_BOTTOM = new HandleRef(null, (IntPtr)1);

		// Token: 0x04001686 RID: 5766
		public static HandleRef HWND_TOPMOST = new HandleRef(null, new IntPtr(-1));

		// Token: 0x04001687 RID: 5767
		public static HandleRef HWND_NOTOPMOST = new HandleRef(null, new IntPtr(-2));

		// Token: 0x04001688 RID: 5768
		public static HandleRef HWND_MESSAGE = new HandleRef(null, new IntPtr(-3));

		// Token: 0x04001689 RID: 5769
		public const int IME_CMODE_NATIVE = 1;

		// Token: 0x0400168A RID: 5770
		public const int IME_CMODE_KATAKANA = 2;

		// Token: 0x0400168B RID: 5771
		public const int IME_CMODE_FULLSHAPE = 8;

		// Token: 0x0400168C RID: 5772
		public const int INPLACE_E_NOTOOLSPACE = -2147221087;

		// Token: 0x0400168D RID: 5773
		public const int ICON_SMALL = 0;

		// Token: 0x0400168E RID: 5774
		public const int ICON_BIG = 1;

		// Token: 0x0400168F RID: 5775
		public const int IDC_ARROW = 32512;

		// Token: 0x04001690 RID: 5776
		public const int IDC_IBEAM = 32513;

		// Token: 0x04001691 RID: 5777
		public const int IDC_WAIT = 32514;

		// Token: 0x04001692 RID: 5778
		public const int IDC_CROSS = 32515;

		// Token: 0x04001693 RID: 5779
		public const int IDC_SIZEALL = 32646;

		// Token: 0x04001694 RID: 5780
		public const int IDC_SIZENWSE = 32642;

		// Token: 0x04001695 RID: 5781
		public const int IDC_SIZENESW = 32643;

		// Token: 0x04001696 RID: 5782
		public const int IDC_SIZEWE = 32644;

		// Token: 0x04001697 RID: 5783
		public const int IDC_SIZENS = 32645;

		// Token: 0x04001698 RID: 5784
		public const int IDC_UPARROW = 32516;

		// Token: 0x04001699 RID: 5785
		public const int IDC_NO = 32648;

		// Token: 0x0400169A RID: 5786
		public const int IDC_APPSTARTING = 32650;

		// Token: 0x0400169B RID: 5787
		public const int IDC_HELP = 32651;

		// Token: 0x0400169C RID: 5788
		public const int IMAGE_ICON = 1;

		// Token: 0x0400169D RID: 5789
		public const int IMAGE_CURSOR = 2;

		// Token: 0x0400169E RID: 5790
		public const int ICC_LISTVIEW_CLASSES = 1;

		// Token: 0x0400169F RID: 5791
		public const int ICC_TREEVIEW_CLASSES = 2;

		// Token: 0x040016A0 RID: 5792
		public const int ICC_BAR_CLASSES = 4;

		// Token: 0x040016A1 RID: 5793
		public const int ICC_TAB_CLASSES = 8;

		// Token: 0x040016A2 RID: 5794
		public const int ICC_PROGRESS_CLASS = 32;

		// Token: 0x040016A3 RID: 5795
		public const int ICC_DATE_CLASSES = 256;

		// Token: 0x040016A4 RID: 5796
		public const int ILC_MASK = 1;

		// Token: 0x040016A5 RID: 5797
		public const int ILC_COLOR = 0;

		// Token: 0x040016A6 RID: 5798
		public const int ILC_COLOR4 = 4;

		// Token: 0x040016A7 RID: 5799
		public const int ILC_COLOR8 = 8;

		// Token: 0x040016A8 RID: 5800
		public const int ILC_COLOR16 = 16;

		// Token: 0x040016A9 RID: 5801
		public const int ILC_COLOR24 = 24;

		// Token: 0x040016AA RID: 5802
		public const int ILC_COLOR32 = 32;

		// Token: 0x040016AB RID: 5803
		public const int ILC_MIRROR = 8192;

		// Token: 0x040016AC RID: 5804
		public const int ILD_NORMAL = 0;

		// Token: 0x040016AD RID: 5805
		public const int ILD_TRANSPARENT = 1;

		// Token: 0x040016AE RID: 5806
		public const int ILD_MASK = 16;

		// Token: 0x040016AF RID: 5807
		public const int ILD_ROP = 64;

		// Token: 0x040016B0 RID: 5808
		public const int ILP_NORMAL = 0;

		// Token: 0x040016B1 RID: 5809
		public const int ILP_DOWNLEVEL = 1;

		// Token: 0x040016B2 RID: 5810
		public const int ILS_NORMAL = 0;

		// Token: 0x040016B3 RID: 5811
		public const int ILS_GLOW = 1;

		// Token: 0x040016B4 RID: 5812
		public const int ILS_SHADOW = 2;

		// Token: 0x040016B5 RID: 5813
		public const int ILS_SATURATE = 4;

		// Token: 0x040016B6 RID: 5814
		public const int ILS_ALPHA = 8;

		// Token: 0x040016B7 RID: 5815
		public const int IDM_PRINT = 27;

		// Token: 0x040016B8 RID: 5816
		public const int IDM_PAGESETUP = 2004;

		// Token: 0x040016B9 RID: 5817
		public const int IDM_PRINTPREVIEW = 2003;

		// Token: 0x040016BA RID: 5818
		public const int IDM_PROPERTIES = 28;

		// Token: 0x040016BB RID: 5819
		public const int IDM_SAVEAS = 71;

		// Token: 0x040016BC RID: 5820
		public const int CSC_NAVIGATEFORWARD = 1;

		// Token: 0x040016BD RID: 5821
		public const int CSC_NAVIGATEBACK = 2;

		// Token: 0x040016BE RID: 5822
		public const int STG_E_INVALIDFUNCTION = -2147287039;

		// Token: 0x040016BF RID: 5823
		public const int STG_E_FILENOTFOUND = -2147287038;

		// Token: 0x040016C0 RID: 5824
		public const int STG_E_PATHNOTFOUND = -2147287037;

		// Token: 0x040016C1 RID: 5825
		public const int STG_E_TOOMANYOPENFILES = -2147287036;

		// Token: 0x040016C2 RID: 5826
		public const int STG_E_ACCESSDENIED = -2147287035;

		// Token: 0x040016C3 RID: 5827
		public const int STG_E_INVALIDHANDLE = -2147287034;

		// Token: 0x040016C4 RID: 5828
		public const int STG_E_INSUFFICIENTMEMORY = -2147287032;

		// Token: 0x040016C5 RID: 5829
		public const int STG_E_INVALIDPOINTER = -2147287031;

		// Token: 0x040016C6 RID: 5830
		public const int STG_E_NOMOREFILES = -2147287022;

		// Token: 0x040016C7 RID: 5831
		public const int STG_E_DISKISWRITEPROTECTED = -2147287021;

		// Token: 0x040016C8 RID: 5832
		public const int STG_E_SEEKERROR = -2147287015;

		// Token: 0x040016C9 RID: 5833
		public const int STG_E_WRITEFAULT = -2147287011;

		// Token: 0x040016CA RID: 5834
		public const int STG_E_READFAULT = -2147287010;

		// Token: 0x040016CB RID: 5835
		public const int STG_E_SHAREVIOLATION = -2147287008;

		// Token: 0x040016CC RID: 5836
		public const int STG_E_LOCKVIOLATION = -2147287007;

		// Token: 0x040016CD RID: 5837
		public const int INPUT_KEYBOARD = 1;

		// Token: 0x040016CE RID: 5838
		public const int KEYEVENTF_EXTENDEDKEY = 1;

		// Token: 0x040016CF RID: 5839
		public const int KEYEVENTF_KEYUP = 2;

		// Token: 0x040016D0 RID: 5840
		public const int KEYEVENTF_UNICODE = 4;

		// Token: 0x040016D1 RID: 5841
		public const int LOGPIXELSX = 88;

		// Token: 0x040016D2 RID: 5842
		public const int LOGPIXELSY = 90;

		// Token: 0x040016D3 RID: 5843
		public const int LB_ERR = -1;

		// Token: 0x040016D4 RID: 5844
		public const int LB_ERRSPACE = -2;

		// Token: 0x040016D5 RID: 5845
		public const int LBN_SELCHANGE = 1;

		// Token: 0x040016D6 RID: 5846
		public const int LBN_DBLCLK = 2;

		// Token: 0x040016D7 RID: 5847
		public const int LB_ADDSTRING = 384;

		// Token: 0x040016D8 RID: 5848
		public const int LB_INSERTSTRING = 385;

		// Token: 0x040016D9 RID: 5849
		public const int LB_DELETESTRING = 386;

		// Token: 0x040016DA RID: 5850
		public const int LB_RESETCONTENT = 388;

		// Token: 0x040016DB RID: 5851
		public const int LB_SETSEL = 389;

		// Token: 0x040016DC RID: 5852
		public const int LB_SETCURSEL = 390;

		// Token: 0x040016DD RID: 5853
		public const int LB_GETSEL = 391;

		// Token: 0x040016DE RID: 5854
		public const int LB_GETCARETINDEX = 415;

		// Token: 0x040016DF RID: 5855
		public const int LB_GETCURSEL = 392;

		// Token: 0x040016E0 RID: 5856
		public const int LB_GETTEXT = 393;

		// Token: 0x040016E1 RID: 5857
		public const int LB_GETTEXTLEN = 394;

		// Token: 0x040016E2 RID: 5858
		public const int LB_GETTOPINDEX = 398;

		// Token: 0x040016E3 RID: 5859
		public const int LB_FINDSTRING = 399;

		// Token: 0x040016E4 RID: 5860
		public const int LB_GETSELCOUNT = 400;

		// Token: 0x040016E5 RID: 5861
		public const int LB_GETSELITEMS = 401;

		// Token: 0x040016E6 RID: 5862
		public const int LB_SETTABSTOPS = 402;

		// Token: 0x040016E7 RID: 5863
		public const int LB_SETHORIZONTALEXTENT = 404;

		// Token: 0x040016E8 RID: 5864
		public const int LB_SETCOLUMNWIDTH = 405;

		// Token: 0x040016E9 RID: 5865
		public const int LB_SETTOPINDEX = 407;

		// Token: 0x040016EA RID: 5866
		public const int LB_GETITEMRECT = 408;

		// Token: 0x040016EB RID: 5867
		public const int LB_SETITEMHEIGHT = 416;

		// Token: 0x040016EC RID: 5868
		public const int LB_GETITEMHEIGHT = 417;

		// Token: 0x040016ED RID: 5869
		public const int LB_FINDSTRINGEXACT = 418;

		// Token: 0x040016EE RID: 5870
		public const int LB_ITEMFROMPOINT = 425;

		// Token: 0x040016EF RID: 5871
		public const int LB_SETLOCALE = 421;

		// Token: 0x040016F0 RID: 5872
		public const int LBS_NOTIFY = 1;

		// Token: 0x040016F1 RID: 5873
		public const int LBS_MULTIPLESEL = 8;

		// Token: 0x040016F2 RID: 5874
		public const int LBS_OWNERDRAWFIXED = 16;

		// Token: 0x040016F3 RID: 5875
		public const int LBS_OWNERDRAWVARIABLE = 32;

		// Token: 0x040016F4 RID: 5876
		public const int LBS_HASSTRINGS = 64;

		// Token: 0x040016F5 RID: 5877
		public const int LBS_USETABSTOPS = 128;

		// Token: 0x040016F6 RID: 5878
		public const int LBS_NOINTEGRALHEIGHT = 256;

		// Token: 0x040016F7 RID: 5879
		public const int LBS_MULTICOLUMN = 512;

		// Token: 0x040016F8 RID: 5880
		public const int LBS_WANTKEYBOARDINPUT = 1024;

		// Token: 0x040016F9 RID: 5881
		public const int LBS_EXTENDEDSEL = 2048;

		// Token: 0x040016FA RID: 5882
		public const int LBS_DISABLENOSCROLL = 4096;

		// Token: 0x040016FB RID: 5883
		public const int LBS_NOSEL = 16384;

		// Token: 0x040016FC RID: 5884
		public const int LOCK_WRITE = 1;

		// Token: 0x040016FD RID: 5885
		public const int LOCK_EXCLUSIVE = 2;

		// Token: 0x040016FE RID: 5886
		public const int LOCK_ONLYONCE = 4;

		// Token: 0x040016FF RID: 5887
		public const int LV_VIEW_TILE = 4;

		// Token: 0x04001700 RID: 5888
		public const int LVBKIF_SOURCE_NONE = 0;

		// Token: 0x04001701 RID: 5889
		public const int LVBKIF_SOURCE_URL = 2;

		// Token: 0x04001702 RID: 5890
		public const int LVBKIF_STYLE_NORMAL = 0;

		// Token: 0x04001703 RID: 5891
		public const int LVBKIF_STYLE_TILE = 16;

		// Token: 0x04001704 RID: 5892
		public const int LVS_ICON = 0;

		// Token: 0x04001705 RID: 5893
		public const int LVS_REPORT = 1;

		// Token: 0x04001706 RID: 5894
		public const int LVS_SMALLICON = 2;

		// Token: 0x04001707 RID: 5895
		public const int LVS_LIST = 3;

		// Token: 0x04001708 RID: 5896
		public const int LVS_SINGLESEL = 4;

		// Token: 0x04001709 RID: 5897
		public const int LVS_SHOWSELALWAYS = 8;

		// Token: 0x0400170A RID: 5898
		public const int LVS_SORTASCENDING = 16;

		// Token: 0x0400170B RID: 5899
		public const int LVS_SORTDESCENDING = 32;

		// Token: 0x0400170C RID: 5900
		public const int LVS_SHAREIMAGELISTS = 64;

		// Token: 0x0400170D RID: 5901
		public const int LVS_NOLABELWRAP = 128;

		// Token: 0x0400170E RID: 5902
		public const int LVS_AUTOARRANGE = 256;

		// Token: 0x0400170F RID: 5903
		public const int LVS_EDITLABELS = 512;

		// Token: 0x04001710 RID: 5904
		public const int LVS_NOSCROLL = 8192;

		// Token: 0x04001711 RID: 5905
		public const int LVS_ALIGNTOP = 0;

		// Token: 0x04001712 RID: 5906
		public const int LVS_ALIGNLEFT = 2048;

		// Token: 0x04001713 RID: 5907
		public const int LVS_NOCOLUMNHEADER = 16384;

		// Token: 0x04001714 RID: 5908
		public const int LVS_NOSORTHEADER = 32768;

		// Token: 0x04001715 RID: 5909
		public const int LVS_OWNERDATA = 4096;

		// Token: 0x04001716 RID: 5910
		public const int LVSCW_AUTOSIZE = -1;

		// Token: 0x04001717 RID: 5911
		public const int LVSCW_AUTOSIZE_USEHEADER = -2;

		// Token: 0x04001718 RID: 5912
		public const int LVM_REDRAWITEMS = 4117;

		// Token: 0x04001719 RID: 5913
		public const int LVM_SCROLL = 4116;

		// Token: 0x0400171A RID: 5914
		public const int LVM_SETBKCOLOR = 4097;

		// Token: 0x0400171B RID: 5915
		public const int LVM_SETBKIMAGEA = 4164;

		// Token: 0x0400171C RID: 5916
		public const int LVM_SETBKIMAGEW = 4234;

		// Token: 0x0400171D RID: 5917
		public const int LVM_SETCALLBACKMASK = 4107;

		// Token: 0x0400171E RID: 5918
		public const int LVM_GETCALLBACKMASK = 4106;

		// Token: 0x0400171F RID: 5919
		public const int LVM_GETCOLUMNORDERARRAY = 4155;

		// Token: 0x04001720 RID: 5920
		public const int LVM_GETITEMCOUNT = 4100;

		// Token: 0x04001721 RID: 5921
		public const int LVM_SETCOLUMNORDERARRAY = 4154;

		// Token: 0x04001722 RID: 5922
		public const int LVM_SETINFOTIP = 4269;

		// Token: 0x04001723 RID: 5923
		public const int LVSIL_NORMAL = 0;

		// Token: 0x04001724 RID: 5924
		public const int LVSIL_SMALL = 1;

		// Token: 0x04001725 RID: 5925
		public const int LVSIL_STATE = 2;

		// Token: 0x04001726 RID: 5926
		public const int LVM_SETIMAGELIST = 4099;

		// Token: 0x04001727 RID: 5927
		public const int LVM_SETSELECTIONMARK = 4163;

		// Token: 0x04001728 RID: 5928
		public const int LVM_SETTOOLTIPS = 4170;

		// Token: 0x04001729 RID: 5929
		public const int LVIF_TEXT = 1;

		// Token: 0x0400172A RID: 5930
		public const int LVIF_IMAGE = 2;

		// Token: 0x0400172B RID: 5931
		public const int LVIF_INDENT = 16;

		// Token: 0x0400172C RID: 5932
		public const int LVIF_PARAM = 4;

		// Token: 0x0400172D RID: 5933
		public const int LVIF_STATE = 8;

		// Token: 0x0400172E RID: 5934
		public const int LVIF_GROUPID = 256;

		// Token: 0x0400172F RID: 5935
		public const int LVIF_COLUMNS = 512;

		// Token: 0x04001730 RID: 5936
		public const int LVIS_FOCUSED = 1;

		// Token: 0x04001731 RID: 5937
		public const int LVIS_SELECTED = 2;

		// Token: 0x04001732 RID: 5938
		public const int LVIS_CUT = 4;

		// Token: 0x04001733 RID: 5939
		public const int LVIS_DROPHILITED = 8;

		// Token: 0x04001734 RID: 5940
		public const int LVIS_OVERLAYMASK = 3840;

		// Token: 0x04001735 RID: 5941
		public const int LVIS_STATEIMAGEMASK = 61440;

		// Token: 0x04001736 RID: 5942
		public const int LVM_GETITEMA = 4101;

		// Token: 0x04001737 RID: 5943
		public const int LVM_GETITEMW = 4171;

		// Token: 0x04001738 RID: 5944
		public const int LVM_SETITEMA = 4102;

		// Token: 0x04001739 RID: 5945
		public const int LVM_SETITEMW = 4172;

		// Token: 0x0400173A RID: 5946
		public const int LVM_SETITEMPOSITION32 = 4145;

		// Token: 0x0400173B RID: 5947
		public const int LVM_INSERTITEMA = 4103;

		// Token: 0x0400173C RID: 5948
		public const int LVM_INSERTITEMW = 4173;

		// Token: 0x0400173D RID: 5949
		public const int LVM_DELETEITEM = 4104;

		// Token: 0x0400173E RID: 5950
		public const int LVM_DELETECOLUMN = 4124;

		// Token: 0x0400173F RID: 5951
		public const int LVM_DELETEALLITEMS = 4105;

		// Token: 0x04001740 RID: 5952
		public const int LVM_UPDATE = 4138;

		// Token: 0x04001741 RID: 5953
		public const int LVNI_FOCUSED = 1;

		// Token: 0x04001742 RID: 5954
		public const int LVNI_SELECTED = 2;

		// Token: 0x04001743 RID: 5955
		public const int LVM_GETNEXTITEM = 4108;

		// Token: 0x04001744 RID: 5956
		public const int LVFI_PARAM = 1;

		// Token: 0x04001745 RID: 5957
		public const int LVFI_NEARESTXY = 64;

		// Token: 0x04001746 RID: 5958
		public const int LVFI_PARTIAL = 8;

		// Token: 0x04001747 RID: 5959
		public const int LVFI_STRING = 2;

		// Token: 0x04001748 RID: 5960
		public const int LVM_FINDITEMA = 4109;

		// Token: 0x04001749 RID: 5961
		public const int LVM_FINDITEMW = 4179;

		// Token: 0x0400174A RID: 5962
		public const int LVIR_BOUNDS = 0;

		// Token: 0x0400174B RID: 5963
		public const int LVIR_ICON = 1;

		// Token: 0x0400174C RID: 5964
		public const int LVIR_LABEL = 2;

		// Token: 0x0400174D RID: 5965
		public const int LVIR_SELECTBOUNDS = 3;

		// Token: 0x0400174E RID: 5966
		public const int LVM_GETITEMPOSITION = 4112;

		// Token: 0x0400174F RID: 5967
		public const int LVM_GETITEMRECT = 4110;

		// Token: 0x04001750 RID: 5968
		public const int LVM_GETSUBITEMRECT = 4152;

		// Token: 0x04001751 RID: 5969
		public const int LVM_GETSTRINGWIDTHA = 4113;

		// Token: 0x04001752 RID: 5970
		public const int LVM_GETSTRINGWIDTHW = 4183;

		// Token: 0x04001753 RID: 5971
		public const int LVHT_NOWHERE = 1;

		// Token: 0x04001754 RID: 5972
		public const int LVHT_ONITEMICON = 2;

		// Token: 0x04001755 RID: 5973
		public const int LVHT_ONITEMLABEL = 4;

		// Token: 0x04001756 RID: 5974
		public const int LVHT_ABOVE = 8;

		// Token: 0x04001757 RID: 5975
		public const int LVHT_BELOW = 16;

		// Token: 0x04001758 RID: 5976
		public const int LVHT_RIGHT = 32;

		// Token: 0x04001759 RID: 5977
		public const int LVHT_LEFT = 64;

		// Token: 0x0400175A RID: 5978
		public const int LVHT_ONITEM = 14;

		// Token: 0x0400175B RID: 5979
		public const int LVHT_ONITEMSTATEICON = 8;

		// Token: 0x0400175C RID: 5980
		public const int LVM_SUBITEMHITTEST = 4153;

		// Token: 0x0400175D RID: 5981
		public const int LVM_HITTEST = 4114;

		// Token: 0x0400175E RID: 5982
		public const int LVM_ENSUREVISIBLE = 4115;

		// Token: 0x0400175F RID: 5983
		public const int LVA_DEFAULT = 0;

		// Token: 0x04001760 RID: 5984
		public const int LVA_ALIGNLEFT = 1;

		// Token: 0x04001761 RID: 5985
		public const int LVA_ALIGNTOP = 2;

		// Token: 0x04001762 RID: 5986
		public const int LVA_SNAPTOGRID = 5;

		// Token: 0x04001763 RID: 5987
		public const int LVM_ARRANGE = 4118;

		// Token: 0x04001764 RID: 5988
		public const int LVM_EDITLABELA = 4119;

		// Token: 0x04001765 RID: 5989
		public const int LVM_EDITLABELW = 4214;

		// Token: 0x04001766 RID: 5990
		public const int LVCDI_ITEM = 0;

		// Token: 0x04001767 RID: 5991
		public const int LVCDI_GROUP = 1;

		// Token: 0x04001768 RID: 5992
		public const int LVCF_FMT = 1;

		// Token: 0x04001769 RID: 5993
		public const int LVCF_WIDTH = 2;

		// Token: 0x0400176A RID: 5994
		public const int LVCF_TEXT = 4;

		// Token: 0x0400176B RID: 5995
		public const int LVCF_SUBITEM = 8;

		// Token: 0x0400176C RID: 5996
		public const int LVCF_IMAGE = 16;

		// Token: 0x0400176D RID: 5997
		public const int LVCF_ORDER = 32;

		// Token: 0x0400176E RID: 5998
		public const int LVCFMT_IMAGE = 2048;

		// Token: 0x0400176F RID: 5999
		public const int LVGA_HEADER_LEFT = 1;

		// Token: 0x04001770 RID: 6000
		public const int LVGA_HEADER_CENTER = 2;

		// Token: 0x04001771 RID: 6001
		public const int LVGA_HEADER_RIGHT = 4;

		// Token: 0x04001772 RID: 6002
		public const int LVGA_FOOTER_LEFT = 8;

		// Token: 0x04001773 RID: 6003
		public const int LVGA_FOOTER_CENTER = 16;

		// Token: 0x04001774 RID: 6004
		public const int LVGA_FOOTER_RIGHT = 32;

		// Token: 0x04001775 RID: 6005
		public const int LVGF_NONE = 0;

		// Token: 0x04001776 RID: 6006
		public const int LVGF_HEADER = 1;

		// Token: 0x04001777 RID: 6007
		public const int LVGF_FOOTER = 2;

		// Token: 0x04001778 RID: 6008
		public const int LVGF_STATE = 4;

		// Token: 0x04001779 RID: 6009
		public const int LVGF_ALIGN = 8;

		// Token: 0x0400177A RID: 6010
		public const int LVGF_GROUPID = 16;

		// Token: 0x0400177B RID: 6011
		public const int LVGS_NORMAL = 0;

		// Token: 0x0400177C RID: 6012
		public const int LVGS_COLLAPSED = 1;

		// Token: 0x0400177D RID: 6013
		public const int LVGS_HIDDEN = 2;

		// Token: 0x0400177E RID: 6014
		public const int LVIM_AFTER = 1;

		// Token: 0x0400177F RID: 6015
		public const int LVTVIF_FIXEDSIZE = 3;

		// Token: 0x04001780 RID: 6016
		public const int LVTVIM_TILESIZE = 1;

		// Token: 0x04001781 RID: 6017
		public const int LVTVIM_COLUMNS = 2;

		// Token: 0x04001782 RID: 6018
		public const int LVM_ENABLEGROUPVIEW = 4253;

		// Token: 0x04001783 RID: 6019
		public const int LVM_MOVEITEMTOGROUP = 4250;

		// Token: 0x04001784 RID: 6020
		public const int LVM_GETCOLUMNA = 4121;

		// Token: 0x04001785 RID: 6021
		public const int LVM_GETCOLUMNW = 4191;

		// Token: 0x04001786 RID: 6022
		public const int LVM_SETCOLUMNA = 4122;

		// Token: 0x04001787 RID: 6023
		public const int LVM_SETCOLUMNW = 4192;

		// Token: 0x04001788 RID: 6024
		public const int LVM_INSERTCOLUMNA = 4123;

		// Token: 0x04001789 RID: 6025
		public const int LVM_INSERTCOLUMNW = 4193;

		// Token: 0x0400178A RID: 6026
		public const int LVM_INSERTGROUP = 4241;

		// Token: 0x0400178B RID: 6027
		public const int LVM_REMOVEGROUP = 4246;

		// Token: 0x0400178C RID: 6028
		public const int LVM_INSERTMARKHITTEST = 4264;

		// Token: 0x0400178D RID: 6029
		public const int LVM_REMOVEALLGROUPS = 4256;

		// Token: 0x0400178E RID: 6030
		public const int LVM_GETCOLUMNWIDTH = 4125;

		// Token: 0x0400178F RID: 6031
		public const int LVM_SETCOLUMNWIDTH = 4126;

		// Token: 0x04001790 RID: 6032
		public const int LVM_SETINSERTMARK = 4262;

		// Token: 0x04001791 RID: 6033
		public const int LVM_GETHEADER = 4127;

		// Token: 0x04001792 RID: 6034
		public const int LVM_SETTEXTCOLOR = 4132;

		// Token: 0x04001793 RID: 6035
		public const int LVM_SETTEXTBKCOLOR = 4134;

		// Token: 0x04001794 RID: 6036
		public const int LVM_GETTOPINDEX = 4135;

		// Token: 0x04001795 RID: 6037
		public const int LVM_SETITEMPOSITION = 4111;

		// Token: 0x04001796 RID: 6038
		public const int LVM_SETITEMSTATE = 4139;

		// Token: 0x04001797 RID: 6039
		public const int LVM_GETITEMSTATE = 4140;

		// Token: 0x04001798 RID: 6040
		public const int LVM_GETITEMTEXTA = 4141;

		// Token: 0x04001799 RID: 6041
		public const int LVM_GETITEMTEXTW = 4211;

		// Token: 0x0400179A RID: 6042
		public const int LVM_GETHOTITEM = 4157;

		// Token: 0x0400179B RID: 6043
		public const int LVM_SETITEMTEXTA = 4142;

		// Token: 0x0400179C RID: 6044
		public const int LVM_SETITEMTEXTW = 4212;

		// Token: 0x0400179D RID: 6045
		public const int LVM_SETITEMCOUNT = 4143;

		// Token: 0x0400179E RID: 6046
		public const int LVM_SORTITEMS = 4144;

		// Token: 0x0400179F RID: 6047
		public const int LVM_GETSELECTEDCOUNT = 4146;

		// Token: 0x040017A0 RID: 6048
		public const int LVM_GETISEARCHSTRINGA = 4148;

		// Token: 0x040017A1 RID: 6049
		public const int LVM_GETISEARCHSTRINGW = 4213;

		// Token: 0x040017A2 RID: 6050
		public const int LVM_SETEXTENDEDLISTVIEWSTYLE = 4150;

		// Token: 0x040017A3 RID: 6051
		public const int LVM_SETVIEW = 4238;

		// Token: 0x040017A4 RID: 6052
		public const int LVM_GETGROUPINFO = 4245;

		// Token: 0x040017A5 RID: 6053
		public const int LVM_SETGROUPINFO = 4243;

		// Token: 0x040017A6 RID: 6054
		public const int LVM_HASGROUP = 4257;

		// Token: 0x040017A7 RID: 6055
		public const int LVM_SETTILEVIEWINFO = 4258;

		// Token: 0x040017A8 RID: 6056
		public const int LVM_GETTILEVIEWINFO = 4259;

		// Token: 0x040017A9 RID: 6057
		public const int LVM_GETINSERTMARK = 4263;

		// Token: 0x040017AA RID: 6058
		public const int LVM_GETINSERTMARKRECT = 4265;

		// Token: 0x040017AB RID: 6059
		public const int LVM_SETINSERTMARKCOLOR = 4266;

		// Token: 0x040017AC RID: 6060
		public const int LVM_GETINSERTMARKCOLOR = 4267;

		// Token: 0x040017AD RID: 6061
		public const int LVM_ISGROUPVIEWENABLED = 4271;

		// Token: 0x040017AE RID: 6062
		public const int LVS_EX_GRIDLINES = 1;

		// Token: 0x040017AF RID: 6063
		public const int LVS_EX_CHECKBOXES = 4;

		// Token: 0x040017B0 RID: 6064
		public const int LVS_EX_TRACKSELECT = 8;

		// Token: 0x040017B1 RID: 6065
		public const int LVS_EX_HEADERDRAGDROP = 16;

		// Token: 0x040017B2 RID: 6066
		public const int LVS_EX_FULLROWSELECT = 32;

		// Token: 0x040017B3 RID: 6067
		public const int LVS_EX_ONECLICKACTIVATE = 64;

		// Token: 0x040017B4 RID: 6068
		public const int LVS_EX_TWOCLICKACTIVATE = 128;

		// Token: 0x040017B5 RID: 6069
		public const int LVS_EX_INFOTIP = 1024;

		// Token: 0x040017B6 RID: 6070
		public const int LVS_EX_UNDERLINEHOT = 2048;

		// Token: 0x040017B7 RID: 6071
		public const int LVS_EX_DOUBLEBUFFER = 65536;

		// Token: 0x040017B8 RID: 6072
		public const int LVN_ITEMCHANGING = -100;

		// Token: 0x040017B9 RID: 6073
		public const int LVN_ITEMCHANGED = -101;

		// Token: 0x040017BA RID: 6074
		public const int LVN_BEGINLABELEDITA = -105;

		// Token: 0x040017BB RID: 6075
		public const int LVN_BEGINLABELEDITW = -175;

		// Token: 0x040017BC RID: 6076
		public const int LVN_ENDLABELEDITA = -106;

		// Token: 0x040017BD RID: 6077
		public const int LVN_ENDLABELEDITW = -176;

		// Token: 0x040017BE RID: 6078
		public const int LVN_COLUMNCLICK = -108;

		// Token: 0x040017BF RID: 6079
		public const int LVN_BEGINDRAG = -109;

		// Token: 0x040017C0 RID: 6080
		public const int LVN_BEGINRDRAG = -111;

		// Token: 0x040017C1 RID: 6081
		public const int LVN_ODFINDITEMA = -152;

		// Token: 0x040017C2 RID: 6082
		public const int LVN_ODFINDITEMW = -179;

		// Token: 0x040017C3 RID: 6083
		public const int LVN_ITEMACTIVATE = -114;

		// Token: 0x040017C4 RID: 6084
		public const int LVN_GETDISPINFOA = -150;

		// Token: 0x040017C5 RID: 6085
		public const int LVN_GETDISPINFOW = -177;

		// Token: 0x040017C6 RID: 6086
		public const int LVN_ODCACHEHINT = -113;

		// Token: 0x040017C7 RID: 6087
		public const int LVN_ODSTATECHANGED = -115;

		// Token: 0x040017C8 RID: 6088
		public const int LVN_SETDISPINFOA = -151;

		// Token: 0x040017C9 RID: 6089
		public const int LVN_SETDISPINFOW = -178;

		// Token: 0x040017CA RID: 6090
		public const int LVN_GETINFOTIPA = -157;

		// Token: 0x040017CB RID: 6091
		public const int LVN_GETINFOTIPW = -158;

		// Token: 0x040017CC RID: 6092
		public const int LVN_KEYDOWN = -155;

		// Token: 0x040017CD RID: 6093
		public const int LWA_COLORKEY = 1;

		// Token: 0x040017CE RID: 6094
		public const int LWA_ALPHA = 2;

		// Token: 0x040017CF RID: 6095
		public const int LANG_NEUTRAL = 0;

		// Token: 0x040017D0 RID: 6096
		public const int LOCALE_IFIRSTDAYOFWEEK = 4108;

		// Token: 0x040017D1 RID: 6097
		public const int LOCALE_IMEASURE = 13;

		// Token: 0x040017D2 RID: 6098
		public static readonly int LOCALE_USER_DEFAULT = NativeMethods.MAKELCID(NativeMethods.LANG_USER_DEFAULT);

		// Token: 0x040017D3 RID: 6099
		public static readonly int LANG_USER_DEFAULT = NativeMethods.MAKELANGID(0, 1);

		// Token: 0x040017D4 RID: 6100
		public const int MEMBERID_NIL = -1;

		// Token: 0x040017D5 RID: 6101
		public const int MAX_PATH = 260;

		// Token: 0x040017D6 RID: 6102
		public const int MAX_UNICODESTRING_LEN = 32767;

		// Token: 0x040017D7 RID: 6103
		public const int ERROR_INSUFFICIENT_BUFFER = 122;

		// Token: 0x040017D8 RID: 6104
		public const int MA_ACTIVATE = 1;

		// Token: 0x040017D9 RID: 6105
		public const int MA_ACTIVATEANDEAT = 2;

		// Token: 0x040017DA RID: 6106
		public const int MA_NOACTIVATE = 3;

		// Token: 0x040017DB RID: 6107
		public const int MA_NOACTIVATEANDEAT = 4;

		// Token: 0x040017DC RID: 6108
		public const int MM_TEXT = 1;

		// Token: 0x040017DD RID: 6109
		public const int MM_ANISOTROPIC = 8;

		// Token: 0x040017DE RID: 6110
		public const int MK_LBUTTON = 1;

		// Token: 0x040017DF RID: 6111
		public const int MK_RBUTTON = 2;

		// Token: 0x040017E0 RID: 6112
		public const int MK_SHIFT = 4;

		// Token: 0x040017E1 RID: 6113
		public const int MK_CONTROL = 8;

		// Token: 0x040017E2 RID: 6114
		public const int MK_MBUTTON = 16;

		// Token: 0x040017E3 RID: 6115
		public const int MNC_EXECUTE = 2;

		// Token: 0x040017E4 RID: 6116
		public const int MNC_SELECT = 3;

		// Token: 0x040017E5 RID: 6117
		public const int MIIM_STATE = 1;

		// Token: 0x040017E6 RID: 6118
		public const int MIIM_ID = 2;

		// Token: 0x040017E7 RID: 6119
		public const int MIIM_SUBMENU = 4;

		// Token: 0x040017E8 RID: 6120
		public const int MIIM_TYPE = 16;

		// Token: 0x040017E9 RID: 6121
		public const int MIIM_DATA = 32;

		// Token: 0x040017EA RID: 6122
		public const int MIIM_STRING = 64;

		// Token: 0x040017EB RID: 6123
		public const int MIIM_BITMAP = 128;

		// Token: 0x040017EC RID: 6124
		public const int MIIM_FTYPE = 256;

		// Token: 0x040017ED RID: 6125
		public const int MB_OK = 0;

		// Token: 0x040017EE RID: 6126
		public const int MF_BYCOMMAND = 0;

		// Token: 0x040017EF RID: 6127
		public const int MF_BYPOSITION = 1024;

		// Token: 0x040017F0 RID: 6128
		public const int MF_ENABLED = 0;

		// Token: 0x040017F1 RID: 6129
		public const int MF_GRAYED = 1;

		// Token: 0x040017F2 RID: 6130
		public const int MF_POPUP = 16;

		// Token: 0x040017F3 RID: 6131
		public const int MF_SYSMENU = 8192;

		// Token: 0x040017F4 RID: 6132
		public const int MFS_DISABLED = 3;

		// Token: 0x040017F5 RID: 6133
		public const int MFT_MENUBREAK = 64;

		// Token: 0x040017F6 RID: 6134
		public const int MFT_SEPARATOR = 2048;

		// Token: 0x040017F7 RID: 6135
		public const int MFT_RIGHTORDER = 8192;

		// Token: 0x040017F8 RID: 6136
		public const int MFT_RIGHTJUSTIFY = 16384;

		// Token: 0x040017F9 RID: 6137
		public const int MDIS_ALLCHILDSTYLES = 1;

		// Token: 0x040017FA RID: 6138
		public const int MDITILE_VERTICAL = 0;

		// Token: 0x040017FB RID: 6139
		public const int MDITILE_HORIZONTAL = 1;

		// Token: 0x040017FC RID: 6140
		public const int MDITILE_SKIPDISABLED = 2;

		// Token: 0x040017FD RID: 6141
		public const int MCM_FIRST = 4096;

		// Token: 0x040017FE RID: 6142
		public const int MCM_GETCURSEL = 4097;

		// Token: 0x040017FF RID: 6143
		public const int MCM_SETCURSEL = 4098;

		// Token: 0x04001800 RID: 6144
		public const int MCM_GETMAXSELCOUNT = 4099;

		// Token: 0x04001801 RID: 6145
		public const int MCM_SETMAXSELCOUNT = 4100;

		// Token: 0x04001802 RID: 6146
		public const int MCM_GETSELRANGE = 4101;

		// Token: 0x04001803 RID: 6147
		public const int MCM_SETSELRANGE = 4102;

		// Token: 0x04001804 RID: 6148
		public const int MCM_GETMONTHRANGE = 4103;

		// Token: 0x04001805 RID: 6149
		public const int MCM_SETDAYSTATE = 4104;

		// Token: 0x04001806 RID: 6150
		public const int MCM_GETMINREQRECT = 4105;

		// Token: 0x04001807 RID: 6151
		public const int MCM_SETCOLOR = 4106;

		// Token: 0x04001808 RID: 6152
		public const int MCM_GETCOLOR = 4107;

		// Token: 0x04001809 RID: 6153
		public const int MCM_SETTODAY = 4108;

		// Token: 0x0400180A RID: 6154
		public const int MCM_GETTODAY = 4109;

		// Token: 0x0400180B RID: 6155
		public const int MCM_HITTEST = 4110;

		// Token: 0x0400180C RID: 6156
		public const int MCM_SETFIRSTDAYOFWEEK = 4111;

		// Token: 0x0400180D RID: 6157
		public const int MCM_GETFIRSTDAYOFWEEK = 4112;

		// Token: 0x0400180E RID: 6158
		public const int MCM_GETRANGE = 4113;

		// Token: 0x0400180F RID: 6159
		public const int MCM_SETRANGE = 4114;

		// Token: 0x04001810 RID: 6160
		public const int MCM_GETMONTHDELTA = 4115;

		// Token: 0x04001811 RID: 6161
		public const int MCM_SETMONTHDELTA = 4116;

		// Token: 0x04001812 RID: 6162
		public const int MCM_GETMAXTODAYWIDTH = 4117;

		// Token: 0x04001813 RID: 6163
		public const int MCM_GETCURRENTVIEW = 4118;

		// Token: 0x04001814 RID: 6164
		public const int MCM_GETCALENDARCOUNT = 4119;

		// Token: 0x04001815 RID: 6165
		public const int MCM_GETCALENDARGRIDINFO = 4120;

		// Token: 0x04001816 RID: 6166
		public const int MCM_GETCALID = 4123;

		// Token: 0x04001817 RID: 6167
		public const int MCM_SETCALID = 4124;

		// Token: 0x04001818 RID: 6168
		public const int MCM_SIZERECTTOMIN = 4125;

		// Token: 0x04001819 RID: 6169
		public const int MCM_SETCALENDARBORDER = 4126;

		// Token: 0x0400181A RID: 6170
		public const int MCM_GETCALENDARBORDER = 4127;

		// Token: 0x0400181B RID: 6171
		public const int MCM_SETCURRENTVIEW = 4128;

		// Token: 0x0400181C RID: 6172
		public const int MCHT_NOWHERE = 0;

		// Token: 0x0400181D RID: 6173
		public const int MCHT_TITLE = 65536;

		// Token: 0x0400181E RID: 6174
		public const int MCHT_CALENDAR = 131072;

		// Token: 0x0400181F RID: 6175
		public const int MCHT_TODAYLINK = 196608;

		// Token: 0x04001820 RID: 6176
		public const int MCHT_TITLEBK = 65536;

		// Token: 0x04001821 RID: 6177
		public const int MCHT_TITLEMONTH = 65537;

		// Token: 0x04001822 RID: 6178
		public const int MCHT_TITLEYEAR = 65538;

		// Token: 0x04001823 RID: 6179
		public const int MCHT_TITLEBTNNEXT = 16842755;

		// Token: 0x04001824 RID: 6180
		public const int MCHT_TITLEBTNPREV = 33619971;

		// Token: 0x04001825 RID: 6181
		public const int MCHT_CALENDARBK = 131072;

		// Token: 0x04001826 RID: 6182
		public const int MCHT_CALENDARDATE = 131073;

		// Token: 0x04001827 RID: 6183
		public const int MCHT_CALENDARDATENEXT = 16908289;

		// Token: 0x04001828 RID: 6184
		public const int MCHT_CALENDARDATEPREV = 33685505;

		// Token: 0x04001829 RID: 6185
		public const int MCHT_CALENDARDAY = 131074;

		// Token: 0x0400182A RID: 6186
		public const int MCHT_CALENDARWEEKNUM = 131075;

		// Token: 0x0400182B RID: 6187
		public const int MCHT_CALENDARDATEMIN = 131076;

		// Token: 0x0400182C RID: 6188
		public const int MCHT_CALENDARDATEMAX = 131077;

		// Token: 0x0400182D RID: 6189
		public const int MCHT_CALENDARCONTROL = 1048576;

		// Token: 0x0400182E RID: 6190
		public const int MCHT_NEXT = 16777216;

		// Token: 0x0400182F RID: 6191
		public const int MCHT_PREV = 33554432;

		// Token: 0x04001830 RID: 6192
		public const int MCGIF_DATE = 1;

		// Token: 0x04001831 RID: 6193
		public const int MCGIF_RECT = 2;

		// Token: 0x04001832 RID: 6194
		public const int MCGIF_NAME = 4;

		// Token: 0x04001833 RID: 6195
		public const int MCSC_TEXT = 1;

		// Token: 0x04001834 RID: 6196
		public const int MCSC_TITLEBK = 2;

		// Token: 0x04001835 RID: 6197
		public const int MCSC_TITLETEXT = 3;

		// Token: 0x04001836 RID: 6198
		public const int MCSC_MONTHBK = 4;

		// Token: 0x04001837 RID: 6199
		public const int MCSC_TRAILINGTEXT = 5;

		// Token: 0x04001838 RID: 6200
		public const int MCN_VIEWCHANGE = -750;

		// Token: 0x04001839 RID: 6201
		public const int MCN_SELCHANGE = -749;

		// Token: 0x0400183A RID: 6202
		public const int MCN_GETDAYSTATE = -747;

		// Token: 0x0400183B RID: 6203
		public const int MCN_SELECT = -746;

		// Token: 0x0400183C RID: 6204
		public const int MCS_DAYSTATE = 1;

		// Token: 0x0400183D RID: 6205
		public const int MCS_MULTISELECT = 2;

		// Token: 0x0400183E RID: 6206
		public const int MCS_WEEKNUMBERS = 4;

		// Token: 0x0400183F RID: 6207
		public const int MCS_NOTODAYCIRCLE = 8;

		// Token: 0x04001840 RID: 6208
		public const int MCS_NOTODAY = 16;

		// Token: 0x04001841 RID: 6209
		public const int MSAA_MENU_SIG = -1441927155;

		// Token: 0x04001842 RID: 6210
		public const uint MCGIP_CALENDARCONTROL = 0U;

		// Token: 0x04001843 RID: 6211
		public const uint MCGIP_NEXT = 1U;

		// Token: 0x04001844 RID: 6212
		public const uint MCGIP_PREV = 2U;

		// Token: 0x04001845 RID: 6213
		public const uint MCGIP_FOOTER = 3U;

		// Token: 0x04001846 RID: 6214
		public const uint MCGIP_CALENDAR = 4U;

		// Token: 0x04001847 RID: 6215
		public const uint MCGIP_CALENDARHEADER = 5U;

		// Token: 0x04001848 RID: 6216
		public const uint MCGIP_CALENDARBODY = 6U;

		// Token: 0x04001849 RID: 6217
		public const uint MCGIP_CALENDARROW = 7U;

		// Token: 0x0400184A RID: 6218
		public const uint MCGIP_CALENDARCELL = 8U;

		// Token: 0x0400184B RID: 6219
		public const uint INPUTENUM_MOUSE = 0U;

		// Token: 0x0400184C RID: 6220
		public const uint INPUTENUM_KEYBOARD = 1U;

		// Token: 0x0400184D RID: 6221
		public const uint INPUTENUM_HARDWARE = 2U;

		// Token: 0x0400184E RID: 6222
		public const uint MOUSEEVENTF_MOVE = 1U;

		// Token: 0x0400184F RID: 6223
		public const uint MOUSEEVENTF_LEFTDOWN = 2U;

		// Token: 0x04001850 RID: 6224
		public const uint MOUSEEVENTF_LEFTUP = 4U;

		// Token: 0x04001851 RID: 6225
		public const uint MOUSEEVENTF_RIGHTDOWN = 8U;

		// Token: 0x04001852 RID: 6226
		public const uint MOUSEEVENTF_RIGHTUP = 16U;

		// Token: 0x04001853 RID: 6227
		public const uint MOUSEEVENTF_MIDDLEDOWN = 32U;

		// Token: 0x04001854 RID: 6228
		public const uint MOUSEEVENTF_MIDDLEUP = 64U;

		// Token: 0x04001855 RID: 6229
		public const uint MOUSEEVENTF_XDOWN = 128U;

		// Token: 0x04001856 RID: 6230
		public const uint MOUSEEVENTF_XUP = 256U;

		// Token: 0x04001857 RID: 6231
		public const uint MOUSEEVENTF_WHEEL = 2048U;

		// Token: 0x04001858 RID: 6232
		public const uint MOUSEEVENTF_HWHEEL = 4096U;

		// Token: 0x04001859 RID: 6233
		public const uint MOUSEEVENTF_MOVE_NOCOALESCE = 8192U;

		// Token: 0x0400185A RID: 6234
		public const uint MOUSEEVENTF_VIRTUALDESK = 16384U;

		// Token: 0x0400185B RID: 6235
		public const uint MOUSEEVENTF_ABSOLUTE = 32768U;

		// Token: 0x0400185C RID: 6236
		public const int NIM_ADD = 0;

		// Token: 0x0400185D RID: 6237
		public const int NIM_MODIFY = 1;

		// Token: 0x0400185E RID: 6238
		public const int NIM_DELETE = 2;

		// Token: 0x0400185F RID: 6239
		public const int NIF_MESSAGE = 1;

		// Token: 0x04001860 RID: 6240
		public const int NIM_SETVERSION = 4;

		// Token: 0x04001861 RID: 6241
		public const int NIF_ICON = 2;

		// Token: 0x04001862 RID: 6242
		public const int NIF_INFO = 16;

		// Token: 0x04001863 RID: 6243
		public const int NIF_TIP = 4;

		// Token: 0x04001864 RID: 6244
		public const int NIIF_NONE = 0;

		// Token: 0x04001865 RID: 6245
		public const int NIIF_INFO = 1;

		// Token: 0x04001866 RID: 6246
		public const int NIIF_WARNING = 2;

		// Token: 0x04001867 RID: 6247
		public const int NIIF_ERROR = 3;

		// Token: 0x04001868 RID: 6248
		public const int NIN_BALLOONSHOW = 1026;

		// Token: 0x04001869 RID: 6249
		public const int NIN_BALLOONHIDE = 1027;

		// Token: 0x0400186A RID: 6250
		public const int NIN_BALLOONTIMEOUT = 1028;

		// Token: 0x0400186B RID: 6251
		public const int NIN_BALLOONUSERCLICK = 1029;

		// Token: 0x0400186C RID: 6252
		public const int NFR_ANSI = 1;

		// Token: 0x0400186D RID: 6253
		public const int NFR_UNICODE = 2;

		// Token: 0x0400186E RID: 6254
		public const int NM_CLICK = -2;

		// Token: 0x0400186F RID: 6255
		public const int NM_DBLCLK = -3;

		// Token: 0x04001870 RID: 6256
		public const int NM_RCLICK = -5;

		// Token: 0x04001871 RID: 6257
		public const int NM_RDBLCLK = -6;

		// Token: 0x04001872 RID: 6258
		public const int NM_CUSTOMDRAW = -12;

		// Token: 0x04001873 RID: 6259
		public const int NM_RELEASEDCAPTURE = -16;

		// Token: 0x04001874 RID: 6260
		public const int NONANTIALIASED_QUALITY = 3;

		// Token: 0x04001875 RID: 6261
		public const int OFN_READONLY = 1;

		// Token: 0x04001876 RID: 6262
		public const int OFN_OVERWRITEPROMPT = 2;

		// Token: 0x04001877 RID: 6263
		public const int OFN_HIDEREADONLY = 4;

		// Token: 0x04001878 RID: 6264
		public const int OFN_NOCHANGEDIR = 8;

		// Token: 0x04001879 RID: 6265
		public const int OFN_SHOWHELP = 16;

		// Token: 0x0400187A RID: 6266
		public const int OFN_ENABLEHOOK = 32;

		// Token: 0x0400187B RID: 6267
		public const int OFN_NOVALIDATE = 256;

		// Token: 0x0400187C RID: 6268
		public const int OFN_ALLOWMULTISELECT = 512;

		// Token: 0x0400187D RID: 6269
		public const int OFN_PATHMUSTEXIST = 2048;

		// Token: 0x0400187E RID: 6270
		public const int OFN_FILEMUSTEXIST = 4096;

		// Token: 0x0400187F RID: 6271
		public const int OFN_CREATEPROMPT = 8192;

		// Token: 0x04001880 RID: 6272
		public const int OFN_EXPLORER = 524288;

		// Token: 0x04001881 RID: 6273
		public const int OFN_NODEREFERENCELINKS = 1048576;

		// Token: 0x04001882 RID: 6274
		public const int OFN_ENABLESIZING = 8388608;

		// Token: 0x04001883 RID: 6275
		public const int OFN_USESHELLITEM = 16777216;

		// Token: 0x04001884 RID: 6276
		public const int OLEIVERB_PRIMARY = 0;

		// Token: 0x04001885 RID: 6277
		public const int OLEIVERB_SHOW = -1;

		// Token: 0x04001886 RID: 6278
		public const int OLEIVERB_HIDE = -3;

		// Token: 0x04001887 RID: 6279
		public const int OLEIVERB_UIACTIVATE = -4;

		// Token: 0x04001888 RID: 6280
		public const int OLEIVERB_INPLACEACTIVATE = -5;

		// Token: 0x04001889 RID: 6281
		public const int OLEIVERB_DISCARDUNDOSTATE = -6;

		// Token: 0x0400188A RID: 6282
		public const int OLEIVERB_PROPERTIES = -7;

		// Token: 0x0400188B RID: 6283
		public const int OLE_E_INVALIDRECT = -2147221491;

		// Token: 0x0400188C RID: 6284
		public const int OLE_E_NOCONNECTION = -2147221500;

		// Token: 0x0400188D RID: 6285
		public const int OLE_E_PROMPTSAVECANCELLED = -2147221492;

		// Token: 0x0400188E RID: 6286
		public const int OLEMISC_RECOMPOSEONRESIZE = 1;

		// Token: 0x0400188F RID: 6287
		public const int OLEMISC_INSIDEOUT = 128;

		// Token: 0x04001890 RID: 6288
		public const int OLEMISC_ACTIVATEWHENVISIBLE = 256;

		// Token: 0x04001891 RID: 6289
		public const int OLEMISC_ACTSLIKEBUTTON = 4096;

		// Token: 0x04001892 RID: 6290
		public const int OLEMISC_SETCLIENTSITEFIRST = 131072;

		// Token: 0x04001893 RID: 6291
		public const int OBJ_PEN = 1;

		// Token: 0x04001894 RID: 6292
		public const int OBJ_BRUSH = 2;

		// Token: 0x04001895 RID: 6293
		public const int OBJ_DC = 3;

		// Token: 0x04001896 RID: 6294
		public const int OBJ_METADC = 4;

		// Token: 0x04001897 RID: 6295
		public const int OBJ_PAL = 5;

		// Token: 0x04001898 RID: 6296
		public const int OBJ_FONT = 6;

		// Token: 0x04001899 RID: 6297
		public const int OBJ_BITMAP = 7;

		// Token: 0x0400189A RID: 6298
		public const int OBJ_REGION = 8;

		// Token: 0x0400189B RID: 6299
		public const int OBJ_METAFILE = 9;

		// Token: 0x0400189C RID: 6300
		public const int OBJ_MEMDC = 10;

		// Token: 0x0400189D RID: 6301
		public const int OBJ_EXTPEN = 11;

		// Token: 0x0400189E RID: 6302
		public const int OBJ_ENHMETADC = 12;

		// Token: 0x0400189F RID: 6303
		public const int ODS_CHECKED = 8;

		// Token: 0x040018A0 RID: 6304
		public const int ODS_COMBOBOXEDIT = 4096;

		// Token: 0x040018A1 RID: 6305
		public const int ODS_DEFAULT = 32;

		// Token: 0x040018A2 RID: 6306
		public const int ODS_DISABLED = 4;

		// Token: 0x040018A3 RID: 6307
		public const int ODS_FOCUS = 16;

		// Token: 0x040018A4 RID: 6308
		public const int ODS_GRAYED = 2;

		// Token: 0x040018A5 RID: 6309
		public const int ODS_HOTLIGHT = 64;

		// Token: 0x040018A6 RID: 6310
		public const int ODS_INACTIVE = 128;

		// Token: 0x040018A7 RID: 6311
		public const int ODS_NOACCEL = 256;

		// Token: 0x040018A8 RID: 6312
		public const int ODS_NOFOCUSRECT = 512;

		// Token: 0x040018A9 RID: 6313
		public const int ODS_SELECTED = 1;

		// Token: 0x040018AA RID: 6314
		public const int OLECLOSE_SAVEIFDIRTY = 0;

		// Token: 0x040018AB RID: 6315
		public const int OLECLOSE_PROMPTSAVE = 2;

		// Token: 0x040018AC RID: 6316
		public const int PDERR_SETUPFAILURE = 4097;

		// Token: 0x040018AD RID: 6317
		public const int PDERR_PARSEFAILURE = 4098;

		// Token: 0x040018AE RID: 6318
		public const int PDERR_RETDEFFAILURE = 4099;

		// Token: 0x040018AF RID: 6319
		public const int PDERR_LOADDRVFAILURE = 4100;

		// Token: 0x040018B0 RID: 6320
		public const int PDERR_GETDEVMODEFAIL = 4101;

		// Token: 0x040018B1 RID: 6321
		public const int PDERR_INITFAILURE = 4102;

		// Token: 0x040018B2 RID: 6322
		public const int PDERR_NODEVICES = 4103;

		// Token: 0x040018B3 RID: 6323
		public const int PDERR_NODEFAULTPRN = 4104;

		// Token: 0x040018B4 RID: 6324
		public const int PDERR_DNDMMISMATCH = 4105;

		// Token: 0x040018B5 RID: 6325
		public const int PDERR_CREATEICFAILURE = 4106;

		// Token: 0x040018B6 RID: 6326
		public const int PDERR_PRINTERNOTFOUND = 4107;

		// Token: 0x040018B7 RID: 6327
		public const int PDERR_DEFAULTDIFFERENT = 4108;

		// Token: 0x040018B8 RID: 6328
		public const int PD_ALLPAGES = 0;

		// Token: 0x040018B9 RID: 6329
		public const int PD_SELECTION = 1;

		// Token: 0x040018BA RID: 6330
		public const int PD_PAGENUMS = 2;

		// Token: 0x040018BB RID: 6331
		public const int PD_NOSELECTION = 4;

		// Token: 0x040018BC RID: 6332
		public const int PD_NOPAGENUMS = 8;

		// Token: 0x040018BD RID: 6333
		public const int PD_COLLATE = 16;

		// Token: 0x040018BE RID: 6334
		public const int PD_PRINTTOFILE = 32;

		// Token: 0x040018BF RID: 6335
		public const int PD_PRINTSETUP = 64;

		// Token: 0x040018C0 RID: 6336
		public const int PD_NOWARNING = 128;

		// Token: 0x040018C1 RID: 6337
		public const int PD_RETURNDC = 256;

		// Token: 0x040018C2 RID: 6338
		public const int PD_RETURNIC = 512;

		// Token: 0x040018C3 RID: 6339
		public const int PD_RETURNDEFAULT = 1024;

		// Token: 0x040018C4 RID: 6340
		public const int PD_SHOWHELP = 2048;

		// Token: 0x040018C5 RID: 6341
		public const int PD_ENABLEPRINTHOOK = 4096;

		// Token: 0x040018C6 RID: 6342
		public const int PD_ENABLESETUPHOOK = 8192;

		// Token: 0x040018C7 RID: 6343
		public const int PD_ENABLEPRINTTEMPLATE = 16384;

		// Token: 0x040018C8 RID: 6344
		public const int PD_ENABLESETUPTEMPLATE = 32768;

		// Token: 0x040018C9 RID: 6345
		public const int PD_ENABLEPRINTTEMPLATEHANDLE = 65536;

		// Token: 0x040018CA RID: 6346
		public const int PD_ENABLESETUPTEMPLATEHANDLE = 131072;

		// Token: 0x040018CB RID: 6347
		public const int PD_USEDEVMODECOPIES = 262144;

		// Token: 0x040018CC RID: 6348
		public const int PD_USEDEVMODECOPIESANDCOLLATE = 262144;

		// Token: 0x040018CD RID: 6349
		public const int PD_DISABLEPRINTTOFILE = 524288;

		// Token: 0x040018CE RID: 6350
		public const int PD_HIDEPRINTTOFILE = 1048576;

		// Token: 0x040018CF RID: 6351
		public const int PD_NONETWORKBUTTON = 2097152;

		// Token: 0x040018D0 RID: 6352
		public const int PD_CURRENTPAGE = 4194304;

		// Token: 0x040018D1 RID: 6353
		public const int PD_NOCURRENTPAGE = 8388608;

		// Token: 0x040018D2 RID: 6354
		public const int PD_EXCLUSIONFLAGS = 16777216;

		// Token: 0x040018D3 RID: 6355
		public const int PD_USELARGETEMPLATE = 268435456;

		// Token: 0x040018D4 RID: 6356
		public const int PSD_MINMARGINS = 1;

		// Token: 0x040018D5 RID: 6357
		public const int PSD_MARGINS = 2;

		// Token: 0x040018D6 RID: 6358
		public const int PSD_INHUNDREDTHSOFMILLIMETERS = 8;

		// Token: 0x040018D7 RID: 6359
		public const int PSD_DISABLEMARGINS = 16;

		// Token: 0x040018D8 RID: 6360
		public const int PSD_DISABLEPRINTER = 32;

		// Token: 0x040018D9 RID: 6361
		public const int PSD_DISABLEORIENTATION = 256;

		// Token: 0x040018DA RID: 6362
		public const int PSD_DISABLEPAPER = 512;

		// Token: 0x040018DB RID: 6363
		public const int PSD_SHOWHELP = 2048;

		// Token: 0x040018DC RID: 6364
		public const int PSD_ENABLEPAGESETUPHOOK = 8192;

		// Token: 0x040018DD RID: 6365
		public const int PSD_NONETWORKBUTTON = 2097152;

		// Token: 0x040018DE RID: 6366
		public const int PS_SOLID = 0;

		// Token: 0x040018DF RID: 6367
		public const int PS_DOT = 2;

		// Token: 0x040018E0 RID: 6368
		public const int PLANES = 14;

		// Token: 0x040018E1 RID: 6369
		public const int PRF_CHECKVISIBLE = 1;

		// Token: 0x040018E2 RID: 6370
		public const int PRF_NONCLIENT = 2;

		// Token: 0x040018E3 RID: 6371
		public const int PRF_CLIENT = 4;

		// Token: 0x040018E4 RID: 6372
		public const int PRF_ERASEBKGND = 8;

		// Token: 0x040018E5 RID: 6373
		public const int PRF_CHILDREN = 16;

		// Token: 0x040018E6 RID: 6374
		public const int PM_NOREMOVE = 0;

		// Token: 0x040018E7 RID: 6375
		public const int PM_REMOVE = 1;

		// Token: 0x040018E8 RID: 6376
		public const int PM_NOYIELD = 2;

		// Token: 0x040018E9 RID: 6377
		public const int PBM_SETRANGE = 1025;

		// Token: 0x040018EA RID: 6378
		public const int PBM_SETPOS = 1026;

		// Token: 0x040018EB RID: 6379
		public const int PBM_SETSTEP = 1028;

		// Token: 0x040018EC RID: 6380
		public const int PBM_SETRANGE32 = 1030;

		// Token: 0x040018ED RID: 6381
		public const int PBM_SETBARCOLOR = 1033;

		// Token: 0x040018EE RID: 6382
		public const int PBM_SETMARQUEE = 1034;

		// Token: 0x040018EF RID: 6383
		public const int PBM_SETBKCOLOR = 8193;

		// Token: 0x040018F0 RID: 6384
		public const int PSM_SETTITLEA = 1135;

		// Token: 0x040018F1 RID: 6385
		public const int PSM_SETTITLEW = 1144;

		// Token: 0x040018F2 RID: 6386
		public const int PSM_SETFINISHTEXTA = 1139;

		// Token: 0x040018F3 RID: 6387
		public const int PSM_SETFINISHTEXTW = 1145;

		// Token: 0x040018F4 RID: 6388
		public const int PATCOPY = 15728673;

		// Token: 0x040018F5 RID: 6389
		public const int PATINVERT = 5898313;

		// Token: 0x040018F6 RID: 6390
		public const int PBS_SMOOTH = 1;

		// Token: 0x040018F7 RID: 6391
		public const int PBS_MARQUEE = 8;

		// Token: 0x040018F8 RID: 6392
		public const int QS_KEY = 1;

		// Token: 0x040018F9 RID: 6393
		public const int QS_MOUSEMOVE = 2;

		// Token: 0x040018FA RID: 6394
		public const int QS_MOUSEBUTTON = 4;

		// Token: 0x040018FB RID: 6395
		public const int QS_POSTMESSAGE = 8;

		// Token: 0x040018FC RID: 6396
		public const int QS_TIMER = 16;

		// Token: 0x040018FD RID: 6397
		public const int QS_PAINT = 32;

		// Token: 0x040018FE RID: 6398
		public const int QS_SENDMESSAGE = 64;

		// Token: 0x040018FF RID: 6399
		public const int QS_HOTKEY = 128;

		// Token: 0x04001900 RID: 6400
		public const int QS_ALLPOSTMESSAGE = 256;

		// Token: 0x04001901 RID: 6401
		public const int QS_MOUSE = 6;

		// Token: 0x04001902 RID: 6402
		public const int QS_INPUT = 7;

		// Token: 0x04001903 RID: 6403
		public const int QS_ALLEVENTS = 191;

		// Token: 0x04001904 RID: 6404
		public const int QS_ALLINPUT = 255;

		// Token: 0x04001905 RID: 6405
		public const int MWMO_INPUTAVAILABLE = 4;

		// Token: 0x04001906 RID: 6406
		public const int RECO_DROP = 1;

		// Token: 0x04001907 RID: 6407
		public const int RPC_E_CHANGED_MODE = -2147417850;

		// Token: 0x04001908 RID: 6408
		public const int RPC_E_CANTCALLOUT_ININPUTSYNCCALL = -2147417843;

		// Token: 0x04001909 RID: 6409
		public const int RGN_AND = 1;

		// Token: 0x0400190A RID: 6410
		public const int RGN_XOR = 3;

		// Token: 0x0400190B RID: 6411
		public const int RGN_DIFF = 4;

		// Token: 0x0400190C RID: 6412
		public const int RDW_INVALIDATE = 1;

		// Token: 0x0400190D RID: 6413
		public const int RDW_ERASE = 4;

		// Token: 0x0400190E RID: 6414
		public const int RDW_ALLCHILDREN = 128;

		// Token: 0x0400190F RID: 6415
		public const int RDW_ERASENOW = 512;

		// Token: 0x04001910 RID: 6416
		public const int RDW_UPDATENOW = 256;

		// Token: 0x04001911 RID: 6417
		public const int RDW_FRAME = 1024;

		// Token: 0x04001912 RID: 6418
		public const int RB_INSERTBANDA = 1025;

		// Token: 0x04001913 RID: 6419
		public const int RB_INSERTBANDW = 1034;

		// Token: 0x04001914 RID: 6420
		public const int stc4 = 1091;

		// Token: 0x04001915 RID: 6421
		public const int SHGFP_TYPE_CURRENT = 0;

		// Token: 0x04001916 RID: 6422
		public const int STGM_READ = 0;

		// Token: 0x04001917 RID: 6423
		public const int STGM_WRITE = 1;

		// Token: 0x04001918 RID: 6424
		public const int STGM_READWRITE = 2;

		// Token: 0x04001919 RID: 6425
		public const int STGM_SHARE_EXCLUSIVE = 16;

		// Token: 0x0400191A RID: 6426
		public const int STGM_CREATE = 4096;

		// Token: 0x0400191B RID: 6427
		public const int STGM_TRANSACTED = 65536;

		// Token: 0x0400191C RID: 6428
		public const int STGM_CONVERT = 131072;

		// Token: 0x0400191D RID: 6429
		public const int STGM_DELETEONRELEASE = 67108864;

		// Token: 0x0400191E RID: 6430
		public const int STARTF_USESHOWWINDOW = 1;

		// Token: 0x0400191F RID: 6431
		public const int SB_HORZ = 0;

		// Token: 0x04001920 RID: 6432
		public const int SB_VERT = 1;

		// Token: 0x04001921 RID: 6433
		public const int SB_CTL = 2;

		// Token: 0x04001922 RID: 6434
		public const int SB_LINEUP = 0;

		// Token: 0x04001923 RID: 6435
		public const int SB_LINELEFT = 0;

		// Token: 0x04001924 RID: 6436
		public const int SB_LINEDOWN = 1;

		// Token: 0x04001925 RID: 6437
		public const int SB_LINERIGHT = 1;

		// Token: 0x04001926 RID: 6438
		public const int SB_PAGEUP = 2;

		// Token: 0x04001927 RID: 6439
		public const int SB_PAGELEFT = 2;

		// Token: 0x04001928 RID: 6440
		public const int SB_PAGEDOWN = 3;

		// Token: 0x04001929 RID: 6441
		public const int SB_PAGERIGHT = 3;

		// Token: 0x0400192A RID: 6442
		public const int SB_THUMBPOSITION = 4;

		// Token: 0x0400192B RID: 6443
		public const int SB_THUMBTRACK = 5;

		// Token: 0x0400192C RID: 6444
		public const int SB_LEFT = 6;

		// Token: 0x0400192D RID: 6445
		public const int SB_RIGHT = 7;

		// Token: 0x0400192E RID: 6446
		public const int SB_ENDSCROLL = 8;

		// Token: 0x0400192F RID: 6447
		public const int SB_TOP = 6;

		// Token: 0x04001930 RID: 6448
		public const int SB_BOTTOM = 7;

		// Token: 0x04001931 RID: 6449
		public const int SIZE_RESTORED = 0;

		// Token: 0x04001932 RID: 6450
		public const int SIZE_MAXIMIZED = 2;

		// Token: 0x04001933 RID: 6451
		public const int ESB_ENABLE_BOTH = 0;

		// Token: 0x04001934 RID: 6452
		public const int ESB_DISABLE_BOTH = 3;

		// Token: 0x04001935 RID: 6453
		public const int SORT_DEFAULT = 0;

		// Token: 0x04001936 RID: 6454
		public const int SUBLANG_DEFAULT = 1;

		// Token: 0x04001937 RID: 6455
		public const int SW_HIDE = 0;

		// Token: 0x04001938 RID: 6456
		public const int SW_NORMAL = 1;

		// Token: 0x04001939 RID: 6457
		public const int SW_SHOWMINIMIZED = 2;

		// Token: 0x0400193A RID: 6458
		public const int SW_SHOWMAXIMIZED = 3;

		// Token: 0x0400193B RID: 6459
		public const int SW_MAXIMIZE = 3;

		// Token: 0x0400193C RID: 6460
		public const int SW_SHOWNOACTIVATE = 4;

		// Token: 0x0400193D RID: 6461
		public const int SW_SHOW = 5;

		// Token: 0x0400193E RID: 6462
		public const int SW_MINIMIZE = 6;

		// Token: 0x0400193F RID: 6463
		public const int SW_SHOWMINNOACTIVE = 7;

		// Token: 0x04001940 RID: 6464
		public const int SW_SHOWNA = 8;

		// Token: 0x04001941 RID: 6465
		public const int SW_RESTORE = 9;

		// Token: 0x04001942 RID: 6466
		public const int SW_MAX = 10;

		// Token: 0x04001943 RID: 6467
		public const int SWP_NOSIZE = 1;

		// Token: 0x04001944 RID: 6468
		public const int SWP_NOMOVE = 2;

		// Token: 0x04001945 RID: 6469
		public const int SWP_NOZORDER = 4;

		// Token: 0x04001946 RID: 6470
		public const int SWP_NOACTIVATE = 16;

		// Token: 0x04001947 RID: 6471
		public const int SWP_SHOWWINDOW = 64;

		// Token: 0x04001948 RID: 6472
		public const int SWP_HIDEWINDOW = 128;

		// Token: 0x04001949 RID: 6473
		public const int SWP_DRAWFRAME = 32;

		// Token: 0x0400194A RID: 6474
		public const int SWP_NOOWNERZORDER = 512;

		// Token: 0x0400194B RID: 6475
		public const int SM_CXSCREEN = 0;

		// Token: 0x0400194C RID: 6476
		public const int SM_CYSCREEN = 1;

		// Token: 0x0400194D RID: 6477
		public const int SM_CXVSCROLL = 2;

		// Token: 0x0400194E RID: 6478
		public const int SM_CYHSCROLL = 3;

		// Token: 0x0400194F RID: 6479
		public const int SM_CYCAPTION = 4;

		// Token: 0x04001950 RID: 6480
		public const int SM_CXBORDER = 5;

		// Token: 0x04001951 RID: 6481
		public const int SM_CYBORDER = 6;

		// Token: 0x04001952 RID: 6482
		public const int SM_CYVTHUMB = 9;

		// Token: 0x04001953 RID: 6483
		public const int SM_CXHTHUMB = 10;

		// Token: 0x04001954 RID: 6484
		public const int SM_CXICON = 11;

		// Token: 0x04001955 RID: 6485
		public const int SM_CYICON = 12;

		// Token: 0x04001956 RID: 6486
		public const int SM_CXCURSOR = 13;

		// Token: 0x04001957 RID: 6487
		public const int SM_CYCURSOR = 14;

		// Token: 0x04001958 RID: 6488
		public const int SM_CYMENU = 15;

		// Token: 0x04001959 RID: 6489
		public const int SM_CYKANJIWINDOW = 18;

		// Token: 0x0400195A RID: 6490
		public const int SM_MOUSEPRESENT = 19;

		// Token: 0x0400195B RID: 6491
		public const int SM_CYVSCROLL = 20;

		// Token: 0x0400195C RID: 6492
		public const int SM_CXHSCROLL = 21;

		// Token: 0x0400195D RID: 6493
		public const int SM_DEBUG = 22;

		// Token: 0x0400195E RID: 6494
		public const int SM_SWAPBUTTON = 23;

		// Token: 0x0400195F RID: 6495
		public const int SM_CXMIN = 28;

		// Token: 0x04001960 RID: 6496
		public const int SM_CYMIN = 29;

		// Token: 0x04001961 RID: 6497
		public const int SM_CXSIZE = 30;

		// Token: 0x04001962 RID: 6498
		public const int SM_CYSIZE = 31;

		// Token: 0x04001963 RID: 6499
		public const int SM_CXFRAME = 32;

		// Token: 0x04001964 RID: 6500
		public const int SM_CYFRAME = 33;

		// Token: 0x04001965 RID: 6501
		public const int SM_CXMINTRACK = 34;

		// Token: 0x04001966 RID: 6502
		public const int SM_CYMINTRACK = 35;

		// Token: 0x04001967 RID: 6503
		public const int SM_CXDOUBLECLK = 36;

		// Token: 0x04001968 RID: 6504
		public const int SM_CYDOUBLECLK = 37;

		// Token: 0x04001969 RID: 6505
		public const int SM_CXICONSPACING = 38;

		// Token: 0x0400196A RID: 6506
		public const int SM_CYICONSPACING = 39;

		// Token: 0x0400196B RID: 6507
		public const int SM_MENUDROPALIGNMENT = 40;

		// Token: 0x0400196C RID: 6508
		public const int SM_PENWINDOWS = 41;

		// Token: 0x0400196D RID: 6509
		public const int SM_DBCSENABLED = 42;

		// Token: 0x0400196E RID: 6510
		public const int SM_CMOUSEBUTTONS = 43;

		// Token: 0x0400196F RID: 6511
		public const int SM_CXFIXEDFRAME = 7;

		// Token: 0x04001970 RID: 6512
		public const int SM_CYFIXEDFRAME = 8;

		// Token: 0x04001971 RID: 6513
		public const int SM_SECURE = 44;

		// Token: 0x04001972 RID: 6514
		public const int SM_CXEDGE = 45;

		// Token: 0x04001973 RID: 6515
		public const int SM_CYEDGE = 46;

		// Token: 0x04001974 RID: 6516
		public const int SM_CXMINSPACING = 47;

		// Token: 0x04001975 RID: 6517
		public const int SM_CYMINSPACING = 48;

		// Token: 0x04001976 RID: 6518
		public const int SM_CXSMICON = 49;

		// Token: 0x04001977 RID: 6519
		public const int SM_CYSMICON = 50;

		// Token: 0x04001978 RID: 6520
		public const int SM_CYSMCAPTION = 51;

		// Token: 0x04001979 RID: 6521
		public const int SM_CXSMSIZE = 52;

		// Token: 0x0400197A RID: 6522
		public const int SM_CYSMSIZE = 53;

		// Token: 0x0400197B RID: 6523
		public const int SM_CXMENUSIZE = 54;

		// Token: 0x0400197C RID: 6524
		public const int SM_CYMENUSIZE = 55;

		// Token: 0x0400197D RID: 6525
		public const int SM_ARRANGE = 56;

		// Token: 0x0400197E RID: 6526
		public const int SM_CXMINIMIZED = 57;

		// Token: 0x0400197F RID: 6527
		public const int SM_CYMINIMIZED = 58;

		// Token: 0x04001980 RID: 6528
		public const int SM_CXMAXTRACK = 59;

		// Token: 0x04001981 RID: 6529
		public const int SM_CYMAXTRACK = 60;

		// Token: 0x04001982 RID: 6530
		public const int SM_CXMAXIMIZED = 61;

		// Token: 0x04001983 RID: 6531
		public const int SM_CYMAXIMIZED = 62;

		// Token: 0x04001984 RID: 6532
		public const int SM_NETWORK = 63;

		// Token: 0x04001985 RID: 6533
		public const int SM_CLEANBOOT = 67;

		// Token: 0x04001986 RID: 6534
		public const int SM_CXDRAG = 68;

		// Token: 0x04001987 RID: 6535
		public const int SM_CYDRAG = 69;

		// Token: 0x04001988 RID: 6536
		public const int SM_SHOWSOUNDS = 70;

		// Token: 0x04001989 RID: 6537
		public const int SM_CXMENUCHECK = 71;

		// Token: 0x0400198A RID: 6538
		public const int SM_CYMENUCHECK = 72;

		// Token: 0x0400198B RID: 6539
		public const int SM_MIDEASTENABLED = 74;

		// Token: 0x0400198C RID: 6540
		public const int SM_MOUSEWHEELPRESENT = 75;

		// Token: 0x0400198D RID: 6541
		public const int SM_XVIRTUALSCREEN = 76;

		// Token: 0x0400198E RID: 6542
		public const int SM_YVIRTUALSCREEN = 77;

		// Token: 0x0400198F RID: 6543
		public const int SM_CXVIRTUALSCREEN = 78;

		// Token: 0x04001990 RID: 6544
		public const int SM_CYVIRTUALSCREEN = 79;

		// Token: 0x04001991 RID: 6545
		public const int SM_CMONITORS = 80;

		// Token: 0x04001992 RID: 6546
		public const int SM_SAMEDISPLAYFORMAT = 81;

		// Token: 0x04001993 RID: 6547
		public const int SM_REMOTESESSION = 4096;

		// Token: 0x04001994 RID: 6548
		public const int HLP_FILE = 1;

		// Token: 0x04001995 RID: 6549
		public const int HLP_KEYWORD = 2;

		// Token: 0x04001996 RID: 6550
		public const int HLP_NAVIGATOR = 3;

		// Token: 0x04001997 RID: 6551
		public const int HLP_OBJECT = 4;

		// Token: 0x04001998 RID: 6552
		public const int SW_SCROLLCHILDREN = 1;

		// Token: 0x04001999 RID: 6553
		public const int SW_INVALIDATE = 2;

		// Token: 0x0400199A RID: 6554
		public const int SW_ERASE = 4;

		// Token: 0x0400199B RID: 6555
		public const int SW_SMOOTHSCROLL = 16;

		// Token: 0x0400199C RID: 6556
		public const int SC_SIZE = 61440;

		// Token: 0x0400199D RID: 6557
		public const int SC_MINIMIZE = 61472;

		// Token: 0x0400199E RID: 6558
		public const int SC_MAXIMIZE = 61488;

		// Token: 0x0400199F RID: 6559
		public const int SC_CLOSE = 61536;

		// Token: 0x040019A0 RID: 6560
		public const int SC_KEYMENU = 61696;

		// Token: 0x040019A1 RID: 6561
		public const int SC_RESTORE = 61728;

		// Token: 0x040019A2 RID: 6562
		public const int SC_MOVE = 61456;

		// Token: 0x040019A3 RID: 6563
		public const int SC_CONTEXTHELP = 61824;

		// Token: 0x040019A4 RID: 6564
		public const int SS_LEFT = 0;

		// Token: 0x040019A5 RID: 6565
		public const int SS_CENTER = 1;

		// Token: 0x040019A6 RID: 6566
		public const int SS_RIGHT = 2;

		// Token: 0x040019A7 RID: 6567
		public const int SS_OWNERDRAW = 13;

		// Token: 0x040019A8 RID: 6568
		public const int SS_NOPREFIX = 128;

		// Token: 0x040019A9 RID: 6569
		public const int SS_SUNKEN = 4096;

		// Token: 0x040019AA RID: 6570
		public const int SBS_HORZ = 0;

		// Token: 0x040019AB RID: 6571
		public const int SBS_VERT = 1;

		// Token: 0x040019AC RID: 6572
		public const int SIF_RANGE = 1;

		// Token: 0x040019AD RID: 6573
		public const int SIF_PAGE = 2;

		// Token: 0x040019AE RID: 6574
		public const int SIF_POS = 4;

		// Token: 0x040019AF RID: 6575
		public const int SIF_TRACKPOS = 16;

		// Token: 0x040019B0 RID: 6576
		public const int SIF_ALL = 23;

		// Token: 0x040019B1 RID: 6577
		public const int SPI_GETFONTSMOOTHING = 74;

		// Token: 0x040019B2 RID: 6578
		public const int SPI_GETDROPSHADOW = 4132;

		// Token: 0x040019B3 RID: 6579
		public const int SPI_GETFLATMENU = 4130;

		// Token: 0x040019B4 RID: 6580
		public const int SPI_GETFONTSMOOTHINGTYPE = 8202;

		// Token: 0x040019B5 RID: 6581
		public const int SPI_GETFONTSMOOTHINGCONTRAST = 8204;

		// Token: 0x040019B6 RID: 6582
		public const int SPI_ICONHORIZONTALSPACING = 13;

		// Token: 0x040019B7 RID: 6583
		public const int SPI_ICONVERTICALSPACING = 24;

		// Token: 0x040019B8 RID: 6584
		public const int SPI_GETICONTITLEWRAP = 25;

		// Token: 0x040019B9 RID: 6585
		public const int SPI_GETICONTITLELOGFONT = 31;

		// Token: 0x040019BA RID: 6586
		public const int SPI_GETKEYBOARDCUES = 4106;

		// Token: 0x040019BB RID: 6587
		public const int SPI_GETKEYBOARDDELAY = 22;

		// Token: 0x040019BC RID: 6588
		public const int SPI_GETKEYBOARDPREF = 68;

		// Token: 0x040019BD RID: 6589
		public const int SPI_GETKEYBOARDSPEED = 10;

		// Token: 0x040019BE RID: 6590
		public const int SPI_GETMOUSEHOVERWIDTH = 98;

		// Token: 0x040019BF RID: 6591
		public const int SPI_GETMOUSEHOVERHEIGHT = 100;

		// Token: 0x040019C0 RID: 6592
		public const int SPI_GETMOUSEHOVERTIME = 102;

		// Token: 0x040019C1 RID: 6593
		public const int SPI_GETMOUSESPEED = 112;

		// Token: 0x040019C2 RID: 6594
		public const int SPI_GETMENUDROPALIGNMENT = 27;

		// Token: 0x040019C3 RID: 6595
		public const int SPI_GETMENUFADE = 4114;

		// Token: 0x040019C4 RID: 6596
		public const int SPI_GETMENUSHOWDELAY = 106;

		// Token: 0x040019C5 RID: 6597
		public const int SPI_GETCOMBOBOXANIMATION = 4100;

		// Token: 0x040019C6 RID: 6598
		public const int SPI_GETGRADIENTCAPTIONS = 4104;

		// Token: 0x040019C7 RID: 6599
		public const int SPI_GETHOTTRACKING = 4110;

		// Token: 0x040019C8 RID: 6600
		public const int SPI_GETLISTBOXSMOOTHSCROLLING = 4102;

		// Token: 0x040019C9 RID: 6601
		public const int SPI_GETMENUANIMATION = 4098;

		// Token: 0x040019CA RID: 6602
		public const int SPI_GETSELECTIONFADE = 4116;

		// Token: 0x040019CB RID: 6603
		public const int SPI_GETTOOLTIPANIMATION = 4118;

		// Token: 0x040019CC RID: 6604
		public const int SPI_GETUIEFFECTS = 4158;

		// Token: 0x040019CD RID: 6605
		public const int SPI_GETACTIVEWINDOWTRACKING = 4096;

		// Token: 0x040019CE RID: 6606
		public const int SPI_GETACTIVEWNDTRKTIMEOUT = 8194;

		// Token: 0x040019CF RID: 6607
		public const int SPI_GETANIMATION = 72;

		// Token: 0x040019D0 RID: 6608
		public const int SPI_GETBORDER = 5;

		// Token: 0x040019D1 RID: 6609
		public const int SPI_GETCARETWIDTH = 8198;

		// Token: 0x040019D2 RID: 6610
		public const int SM_CYFOCUSBORDER = 84;

		// Token: 0x040019D3 RID: 6611
		public const int SM_CXFOCUSBORDER = 83;

		// Token: 0x040019D4 RID: 6612
		public const int SM_CYSIZEFRAME = 33;

		// Token: 0x040019D5 RID: 6613
		public const int SM_CXSIZEFRAME = 32;

		// Token: 0x040019D6 RID: 6614
		public const int SPI_GETDRAGFULLWINDOWS = 38;

		// Token: 0x040019D7 RID: 6615
		public const int SPI_GETNONCLIENTMETRICS = 41;

		// Token: 0x040019D8 RID: 6616
		public const int SPI_GETWORKAREA = 48;

		// Token: 0x040019D9 RID: 6617
		public const int SPI_GETHIGHCONTRAST = 66;

		// Token: 0x040019DA RID: 6618
		public const int SPI_GETDEFAULTINPUTLANG = 89;

		// Token: 0x040019DB RID: 6619
		public const int SPI_GETSNAPTODEFBUTTON = 95;

		// Token: 0x040019DC RID: 6620
		public const int SPI_GETWHEELSCROLLLINES = 104;

		// Token: 0x040019DD RID: 6621
		public const int SBARS_SIZEGRIP = 256;

		// Token: 0x040019DE RID: 6622
		public const int SB_SETTEXTA = 1025;

		// Token: 0x040019DF RID: 6623
		public const int SB_SETTEXTW = 1035;

		// Token: 0x040019E0 RID: 6624
		public const int SB_GETTEXTA = 1026;

		// Token: 0x040019E1 RID: 6625
		public const int SB_GETTEXTW = 1037;

		// Token: 0x040019E2 RID: 6626
		public const int SB_GETTEXTLENGTHA = 1027;

		// Token: 0x040019E3 RID: 6627
		public const int SB_GETTEXTLENGTHW = 1036;

		// Token: 0x040019E4 RID: 6628
		public const int SB_SETPARTS = 1028;

		// Token: 0x040019E5 RID: 6629
		public const int SB_SIMPLE = 1033;

		// Token: 0x040019E6 RID: 6630
		public const int SB_GETRECT = 1034;

		// Token: 0x040019E7 RID: 6631
		public const int SB_SETICON = 1039;

		// Token: 0x040019E8 RID: 6632
		public const int SB_SETTIPTEXTA = 1040;

		// Token: 0x040019E9 RID: 6633
		public const int SB_SETTIPTEXTW = 1041;

		// Token: 0x040019EA RID: 6634
		public const int SB_GETTIPTEXTA = 1042;

		// Token: 0x040019EB RID: 6635
		public const int SB_GETTIPTEXTW = 1043;

		// Token: 0x040019EC RID: 6636
		public const int SBT_OWNERDRAW = 4096;

		// Token: 0x040019ED RID: 6637
		public const int SBT_NOBORDERS = 256;

		// Token: 0x040019EE RID: 6638
		public const int SBT_POPOUT = 512;

		// Token: 0x040019EF RID: 6639
		public const int SBT_RTLREADING = 1024;

		// Token: 0x040019F0 RID: 6640
		public const int SRCCOPY = 13369376;

		// Token: 0x040019F1 RID: 6641
		public const int SRCAND = 8913094;

		// Token: 0x040019F2 RID: 6642
		public const int SRCPAINT = 15597702;

		// Token: 0x040019F3 RID: 6643
		public const int NOTSRCCOPY = 3342344;

		// Token: 0x040019F4 RID: 6644
		public const int STATFLAG_DEFAULT = 0;

		// Token: 0x040019F5 RID: 6645
		public const int STATFLAG_NONAME = 1;

		// Token: 0x040019F6 RID: 6646
		public const int STATFLAG_NOOPEN = 2;

		// Token: 0x040019F7 RID: 6647
		public const int STGC_DEFAULT = 0;

		// Token: 0x040019F8 RID: 6648
		public const int STGC_OVERWRITE = 1;

		// Token: 0x040019F9 RID: 6649
		public const int STGC_ONLYIFCURRENT = 2;

		// Token: 0x040019FA RID: 6650
		public const int STGC_DANGEROUSLYCOMMITMERELYTODISKCACHE = 4;

		// Token: 0x040019FB RID: 6651
		public const int STREAM_SEEK_SET = 0;

		// Token: 0x040019FC RID: 6652
		public const int STREAM_SEEK_CUR = 1;

		// Token: 0x040019FD RID: 6653
		public const int STREAM_SEEK_END = 2;

		// Token: 0x040019FE RID: 6654
		public const int S_OK = 0;

		// Token: 0x040019FF RID: 6655
		public const int S_FALSE = 1;

		// Token: 0x04001A00 RID: 6656
		public const int TRANSPARENT = 1;

		// Token: 0x04001A01 RID: 6657
		public const int OPAQUE = 2;

		// Token: 0x04001A02 RID: 6658
		public const int TME_HOVER = 1;

		// Token: 0x04001A03 RID: 6659
		public const int TME_LEAVE = 2;

		// Token: 0x04001A04 RID: 6660
		public const int TPM_LEFTBUTTON = 0;

		// Token: 0x04001A05 RID: 6661
		public const int TPM_RIGHTBUTTON = 2;

		// Token: 0x04001A06 RID: 6662
		public const int TPM_LEFTALIGN = 0;

		// Token: 0x04001A07 RID: 6663
		public const int TPM_RIGHTALIGN = 8;

		// Token: 0x04001A08 RID: 6664
		public const int TPM_VERTICAL = 64;

		// Token: 0x04001A09 RID: 6665
		public const int TV_FIRST = 4352;

		// Token: 0x04001A0A RID: 6666
		public const int TBSTATE_CHECKED = 1;

		// Token: 0x04001A0B RID: 6667
		public const int TBSTATE_ENABLED = 4;

		// Token: 0x04001A0C RID: 6668
		public const int TBSTATE_HIDDEN = 8;

		// Token: 0x04001A0D RID: 6669
		public const int TBSTATE_INDETERMINATE = 16;

		// Token: 0x04001A0E RID: 6670
		public const int TBSTYLE_BUTTON = 0;

		// Token: 0x04001A0F RID: 6671
		public const int TBSTYLE_SEP = 1;

		// Token: 0x04001A10 RID: 6672
		public const int TBSTYLE_CHECK = 2;

		// Token: 0x04001A11 RID: 6673
		public const int TBSTYLE_DROPDOWN = 8;

		// Token: 0x04001A12 RID: 6674
		public const int TBSTYLE_TOOLTIPS = 256;

		// Token: 0x04001A13 RID: 6675
		public const int TBSTYLE_FLAT = 2048;

		// Token: 0x04001A14 RID: 6676
		public const int TBSTYLE_LIST = 4096;

		// Token: 0x04001A15 RID: 6677
		public const int TBSTYLE_EX_DRAWDDARROWS = 1;

		// Token: 0x04001A16 RID: 6678
		public const int TB_ENABLEBUTTON = 1025;

		// Token: 0x04001A17 RID: 6679
		public const int TB_ISBUTTONCHECKED = 1034;

		// Token: 0x04001A18 RID: 6680
		public const int TB_ISBUTTONINDETERMINATE = 1037;

		// Token: 0x04001A19 RID: 6681
		public const int TB_ADDBUTTONSA = 1044;

		// Token: 0x04001A1A RID: 6682
		public const int TB_ADDBUTTONSW = 1092;

		// Token: 0x04001A1B RID: 6683
		public const int TB_INSERTBUTTONA = 1045;

		// Token: 0x04001A1C RID: 6684
		public const int TB_INSERTBUTTONW = 1091;

		// Token: 0x04001A1D RID: 6685
		public const int TB_DELETEBUTTON = 1046;

		// Token: 0x04001A1E RID: 6686
		public const int TB_GETBUTTON = 1047;

		// Token: 0x04001A1F RID: 6687
		public const int TB_SAVERESTOREA = 1050;

		// Token: 0x04001A20 RID: 6688
		public const int TB_SAVERESTOREW = 1100;

		// Token: 0x04001A21 RID: 6689
		public const int TB_ADDSTRINGA = 1052;

		// Token: 0x04001A22 RID: 6690
		public const int TB_ADDSTRINGW = 1101;

		// Token: 0x04001A23 RID: 6691
		public const int TB_BUTTONSTRUCTSIZE = 1054;

		// Token: 0x04001A24 RID: 6692
		public const int TB_SETBUTTONSIZE = 1055;

		// Token: 0x04001A25 RID: 6693
		public const int TB_AUTOSIZE = 1057;

		// Token: 0x04001A26 RID: 6694
		public const int TB_GETROWS = 1064;

		// Token: 0x04001A27 RID: 6695
		public const int TB_GETBUTTONTEXTA = 1069;

		// Token: 0x04001A28 RID: 6696
		public const int TB_GETBUTTONTEXTW = 1099;

		// Token: 0x04001A29 RID: 6697
		public const int TB_SETIMAGELIST = 1072;

		// Token: 0x04001A2A RID: 6698
		public const int TB_GETRECT = 1075;

		// Token: 0x04001A2B RID: 6699
		public const int TB_GETBUTTONSIZE = 1082;

		// Token: 0x04001A2C RID: 6700
		public const int TB_GETBUTTONINFOW = 1087;

		// Token: 0x04001A2D RID: 6701
		public const int TB_SETBUTTONINFOW = 1088;

		// Token: 0x04001A2E RID: 6702
		public const int TB_GETBUTTONINFOA = 1089;

		// Token: 0x04001A2F RID: 6703
		public const int TB_SETBUTTONINFOA = 1090;

		// Token: 0x04001A30 RID: 6704
		public const int TB_MAPACCELERATORA = 1102;

		// Token: 0x04001A31 RID: 6705
		public const int TB_SETEXTENDEDSTYLE = 1108;

		// Token: 0x04001A32 RID: 6706
		public const int TB_MAPACCELERATORW = 1114;

		// Token: 0x04001A33 RID: 6707
		public const int TB_GETTOOLTIPS = 1059;

		// Token: 0x04001A34 RID: 6708
		public const int TB_SETTOOLTIPS = 1060;

		// Token: 0x04001A35 RID: 6709
		public const int TBIF_IMAGE = 1;

		// Token: 0x04001A36 RID: 6710
		public const int TBIF_TEXT = 2;

		// Token: 0x04001A37 RID: 6711
		public const int TBIF_STATE = 4;

		// Token: 0x04001A38 RID: 6712
		public const int TBIF_STYLE = 8;

		// Token: 0x04001A39 RID: 6713
		public const int TBIF_COMMAND = 32;

		// Token: 0x04001A3A RID: 6714
		public const int TBIF_SIZE = 64;

		// Token: 0x04001A3B RID: 6715
		public const int TBN_GETBUTTONINFOA = -700;

		// Token: 0x04001A3C RID: 6716
		public const int TBN_GETBUTTONINFOW = -720;

		// Token: 0x04001A3D RID: 6717
		public const int TBN_QUERYINSERT = -706;

		// Token: 0x04001A3E RID: 6718
		public const int TBN_DROPDOWN = -710;

		// Token: 0x04001A3F RID: 6719
		public const int TBN_HOTITEMCHANGE = -713;

		// Token: 0x04001A40 RID: 6720
		public const int TBN_GETDISPINFOA = -716;

		// Token: 0x04001A41 RID: 6721
		public const int TBN_GETDISPINFOW = -717;

		// Token: 0x04001A42 RID: 6722
		public const int TBN_GETINFOTIPA = -718;

		// Token: 0x04001A43 RID: 6723
		public const int TBN_GETINFOTIPW = -719;

		// Token: 0x04001A44 RID: 6724
		public const int TTS_ALWAYSTIP = 1;

		// Token: 0x04001A45 RID: 6725
		public const int TTS_NOPREFIX = 2;

		// Token: 0x04001A46 RID: 6726
		public const int TTS_NOANIMATE = 16;

		// Token: 0x04001A47 RID: 6727
		public const int TTS_NOFADE = 32;

		// Token: 0x04001A48 RID: 6728
		public const int TTS_BALLOON = 64;

		// Token: 0x04001A49 RID: 6729
		public const int TTI_WARNING = 2;

		// Token: 0x04001A4A RID: 6730
		public const int TTF_IDISHWND = 1;

		// Token: 0x04001A4B RID: 6731
		public const int TTF_RTLREADING = 4;

		// Token: 0x04001A4C RID: 6732
		public const int TTF_TRACK = 32;

		// Token: 0x04001A4D RID: 6733
		public const int TTF_CENTERTIP = 2;

		// Token: 0x04001A4E RID: 6734
		public const int TTF_SUBCLASS = 16;

		// Token: 0x04001A4F RID: 6735
		public const int TTF_TRANSPARENT = 256;

		// Token: 0x04001A50 RID: 6736
		public const int TTF_ABSOLUTE = 128;

		// Token: 0x04001A51 RID: 6737
		public const int TTDT_AUTOMATIC = 0;

		// Token: 0x04001A52 RID: 6738
		public const int TTDT_RESHOW = 1;

		// Token: 0x04001A53 RID: 6739
		public const int TTDT_AUTOPOP = 2;

		// Token: 0x04001A54 RID: 6740
		public const int TTDT_INITIAL = 3;

		// Token: 0x04001A55 RID: 6741
		public const int TTM_TRACKACTIVATE = 1041;

		// Token: 0x04001A56 RID: 6742
		public const int TTM_TRACKPOSITION = 1042;

		// Token: 0x04001A57 RID: 6743
		public const int TTM_ACTIVATE = 1025;

		// Token: 0x04001A58 RID: 6744
		public const int TTM_POP = 1052;

		// Token: 0x04001A59 RID: 6745
		public const int TTM_ADJUSTRECT = 1055;

		// Token: 0x04001A5A RID: 6746
		public const int TTM_SETDELAYTIME = 1027;

		// Token: 0x04001A5B RID: 6747
		public const int TTM_SETTITLEA = 1056;

		// Token: 0x04001A5C RID: 6748
		public const int TTM_SETTITLEW = 1057;

		// Token: 0x04001A5D RID: 6749
		public const int TTM_ADDTOOLA = 1028;

		// Token: 0x04001A5E RID: 6750
		public const int TTM_ADDTOOLW = 1074;

		// Token: 0x04001A5F RID: 6751
		public const int TTM_DELTOOLA = 1029;

		// Token: 0x04001A60 RID: 6752
		public const int TTM_DELTOOLW = 1075;

		// Token: 0x04001A61 RID: 6753
		public const int TTM_NEWTOOLRECTA = 1030;

		// Token: 0x04001A62 RID: 6754
		public const int TTM_NEWTOOLRECTW = 1076;

		// Token: 0x04001A63 RID: 6755
		public const int TTM_RELAYEVENT = 1031;

		// Token: 0x04001A64 RID: 6756
		public const int TTM_GETTIPBKCOLOR = 1046;

		// Token: 0x04001A65 RID: 6757
		public const int TTM_SETTIPBKCOLOR = 1043;

		// Token: 0x04001A66 RID: 6758
		public const int TTM_SETTIPTEXTCOLOR = 1044;

		// Token: 0x04001A67 RID: 6759
		public const int TTM_GETTIPTEXTCOLOR = 1047;

		// Token: 0x04001A68 RID: 6760
		public const int TTM_GETTOOLINFOA = 1032;

		// Token: 0x04001A69 RID: 6761
		public const int TTM_GETTOOLINFOW = 1077;

		// Token: 0x04001A6A RID: 6762
		public const int TTM_SETTOOLINFOA = 1033;

		// Token: 0x04001A6B RID: 6763
		public const int TTM_SETTOOLINFOW = 1078;

		// Token: 0x04001A6C RID: 6764
		public const int TTM_HITTESTA = 1034;

		// Token: 0x04001A6D RID: 6765
		public const int TTM_HITTESTW = 1079;

		// Token: 0x04001A6E RID: 6766
		public const int TTM_GETTEXTA = 1035;

		// Token: 0x04001A6F RID: 6767
		public const int TTM_GETTEXTW = 1080;

		// Token: 0x04001A70 RID: 6768
		public const int TTM_UPDATE = 1053;

		// Token: 0x04001A71 RID: 6769
		public const int TTM_UPDATETIPTEXTA = 1036;

		// Token: 0x04001A72 RID: 6770
		public const int TTM_UPDATETIPTEXTW = 1081;

		// Token: 0x04001A73 RID: 6771
		public const int TTM_ENUMTOOLSA = 1038;

		// Token: 0x04001A74 RID: 6772
		public const int TTM_ENUMTOOLSW = 1082;

		// Token: 0x04001A75 RID: 6773
		public const int TTM_GETCURRENTTOOLA = 1039;

		// Token: 0x04001A76 RID: 6774
		public const int TTM_GETCURRENTTOOLW = 1083;

		// Token: 0x04001A77 RID: 6775
		public const int TTM_WINDOWFROMPOINT = 1040;

		// Token: 0x04001A78 RID: 6776
		public const int TTM_GETDELAYTIME = 1045;

		// Token: 0x04001A79 RID: 6777
		public const int TTM_SETMAXTIPWIDTH = 1048;

		// Token: 0x04001A7A RID: 6778
		public const int TTM_GETBUBBLESIZE = 1054;

		// Token: 0x04001A7B RID: 6779
		public const int TTN_GETDISPINFOA = -520;

		// Token: 0x04001A7C RID: 6780
		public const int TTN_GETDISPINFOW = -530;

		// Token: 0x04001A7D RID: 6781
		public const int TTN_SHOW = -521;

		// Token: 0x04001A7E RID: 6782
		public const int TTN_POP = -522;

		// Token: 0x04001A7F RID: 6783
		public const int TTN_NEEDTEXTA = -520;

		// Token: 0x04001A80 RID: 6784
		public const int TTN_NEEDTEXTW = -530;

		// Token: 0x04001A81 RID: 6785
		public const int TBS_AUTOTICKS = 1;

		// Token: 0x04001A82 RID: 6786
		public const int TBS_VERT = 2;

		// Token: 0x04001A83 RID: 6787
		public const int TBS_TOP = 4;

		// Token: 0x04001A84 RID: 6788
		public const int TBS_BOTTOM = 0;

		// Token: 0x04001A85 RID: 6789
		public const int TBS_BOTH = 8;

		// Token: 0x04001A86 RID: 6790
		public const int TBS_NOTICKS = 16;

		// Token: 0x04001A87 RID: 6791
		public const int TBM_GETPOS = 1024;

		// Token: 0x04001A88 RID: 6792
		public const int TBM_SETTIC = 1028;

		// Token: 0x04001A89 RID: 6793
		public const int TBM_SETPOS = 1029;

		// Token: 0x04001A8A RID: 6794
		public const int TBM_SETRANGE = 1030;

		// Token: 0x04001A8B RID: 6795
		public const int TBM_SETRANGEMIN = 1031;

		// Token: 0x04001A8C RID: 6796
		public const int TBM_SETRANGEMAX = 1032;

		// Token: 0x04001A8D RID: 6797
		public const int TBM_SETTICFREQ = 1044;

		// Token: 0x04001A8E RID: 6798
		public const int TBM_SETPAGESIZE = 1045;

		// Token: 0x04001A8F RID: 6799
		public const int TBM_SETLINESIZE = 1047;

		// Token: 0x04001A90 RID: 6800
		public const int TB_LINEUP = 0;

		// Token: 0x04001A91 RID: 6801
		public const int TB_LINEDOWN = 1;

		// Token: 0x04001A92 RID: 6802
		public const int TB_PAGEUP = 2;

		// Token: 0x04001A93 RID: 6803
		public const int TB_PAGEDOWN = 3;

		// Token: 0x04001A94 RID: 6804
		public const int TB_THUMBPOSITION = 4;

		// Token: 0x04001A95 RID: 6805
		public const int TB_THUMBTRACK = 5;

		// Token: 0x04001A96 RID: 6806
		public const int TB_TOP = 6;

		// Token: 0x04001A97 RID: 6807
		public const int TB_BOTTOM = 7;

		// Token: 0x04001A98 RID: 6808
		public const int TB_ENDTRACK = 8;

		// Token: 0x04001A99 RID: 6809
		public const int TVS_HASBUTTONS = 1;

		// Token: 0x04001A9A RID: 6810
		public const int TVS_HASLINES = 2;

		// Token: 0x04001A9B RID: 6811
		public const int TVS_LINESATROOT = 4;

		// Token: 0x04001A9C RID: 6812
		public const int TVS_EDITLABELS = 8;

		// Token: 0x04001A9D RID: 6813
		public const int TVS_SHOWSELALWAYS = 32;

		// Token: 0x04001A9E RID: 6814
		public const int TVS_RTLREADING = 64;

		// Token: 0x04001A9F RID: 6815
		public const int TVS_CHECKBOXES = 256;

		// Token: 0x04001AA0 RID: 6816
		public const int TVS_TRACKSELECT = 512;

		// Token: 0x04001AA1 RID: 6817
		public const int TVS_FULLROWSELECT = 4096;

		// Token: 0x04001AA2 RID: 6818
		public const int TVS_NONEVENHEIGHT = 16384;

		// Token: 0x04001AA3 RID: 6819
		public const int TVS_INFOTIP = 2048;

		// Token: 0x04001AA4 RID: 6820
		public const int TVS_NOTOOLTIPS = 128;

		// Token: 0x04001AA5 RID: 6821
		public const int TVIF_TEXT = 1;

		// Token: 0x04001AA6 RID: 6822
		public const int TVIF_IMAGE = 2;

		// Token: 0x04001AA7 RID: 6823
		public const int TVIF_PARAM = 4;

		// Token: 0x04001AA8 RID: 6824
		public const int TVIF_STATE = 8;

		// Token: 0x04001AA9 RID: 6825
		public const int TVIF_HANDLE = 16;

		// Token: 0x04001AAA RID: 6826
		public const int TVIF_SELECTEDIMAGE = 32;

		// Token: 0x04001AAB RID: 6827
		public const int TVIS_SELECTED = 2;

		// Token: 0x04001AAC RID: 6828
		public const int TVIS_EXPANDED = 32;

		// Token: 0x04001AAD RID: 6829
		public const int TVIS_EXPANDEDONCE = 64;

		// Token: 0x04001AAE RID: 6830
		public const int TVIS_STATEIMAGEMASK = 61440;

		// Token: 0x04001AAF RID: 6831
		public const int TVI_ROOT = -65536;

		// Token: 0x04001AB0 RID: 6832
		public const int TVI_FIRST = -65535;

		// Token: 0x04001AB1 RID: 6833
		public const int TVM_INSERTITEMA = 4352;

		// Token: 0x04001AB2 RID: 6834
		public const int TVM_INSERTITEMW = 4402;

		// Token: 0x04001AB3 RID: 6835
		public const int TVM_DELETEITEM = 4353;

		// Token: 0x04001AB4 RID: 6836
		public const int TVM_EXPAND = 4354;

		// Token: 0x04001AB5 RID: 6837
		public const int TVE_COLLAPSE = 1;

		// Token: 0x04001AB6 RID: 6838
		public const int TVE_EXPAND = 2;

		// Token: 0x04001AB7 RID: 6839
		public const int TVM_GETITEMRECT = 4356;

		// Token: 0x04001AB8 RID: 6840
		public const int TVM_GETINDENT = 4358;

		// Token: 0x04001AB9 RID: 6841
		public const int TVM_SETINDENT = 4359;

		// Token: 0x04001ABA RID: 6842
		public const int TVM_GETIMAGELIST = 4360;

		// Token: 0x04001ABB RID: 6843
		public const int TVM_SETIMAGELIST = 4361;

		// Token: 0x04001ABC RID: 6844
		public const int TVM_GETNEXTITEM = 4362;

		// Token: 0x04001ABD RID: 6845
		public const int TVGN_NEXT = 1;

		// Token: 0x04001ABE RID: 6846
		public const int TVGN_PREVIOUS = 2;

		// Token: 0x04001ABF RID: 6847
		public const int TVGN_FIRSTVISIBLE = 5;

		// Token: 0x04001AC0 RID: 6848
		public const int TVGN_NEXTVISIBLE = 6;

		// Token: 0x04001AC1 RID: 6849
		public const int TVGN_PREVIOUSVISIBLE = 7;

		// Token: 0x04001AC2 RID: 6850
		public const int TVGN_DROPHILITE = 8;

		// Token: 0x04001AC3 RID: 6851
		public const int TVGN_CARET = 9;

		// Token: 0x04001AC4 RID: 6852
		public const int TVM_SELECTITEM = 4363;

		// Token: 0x04001AC5 RID: 6853
		public const int TVM_GETITEMA = 4364;

		// Token: 0x04001AC6 RID: 6854
		public const int TVM_GETITEMW = 4414;

		// Token: 0x04001AC7 RID: 6855
		public const int TVM_SETITEMA = 4365;

		// Token: 0x04001AC8 RID: 6856
		public const int TVM_SETITEMW = 4415;

		// Token: 0x04001AC9 RID: 6857
		public const int TVM_EDITLABELA = 4366;

		// Token: 0x04001ACA RID: 6858
		public const int TVM_EDITLABELW = 4417;

		// Token: 0x04001ACB RID: 6859
		public const int TVM_GETEDITCONTROL = 4367;

		// Token: 0x04001ACC RID: 6860
		public const int TVM_GETVISIBLECOUNT = 4368;

		// Token: 0x04001ACD RID: 6861
		public const int TVM_HITTEST = 4369;

		// Token: 0x04001ACE RID: 6862
		public const int TVM_ENSUREVISIBLE = 4372;

		// Token: 0x04001ACF RID: 6863
		public const int TVM_ENDEDITLABELNOW = 4374;

		// Token: 0x04001AD0 RID: 6864
		public const int TVM_GETISEARCHSTRINGA = 4375;

		// Token: 0x04001AD1 RID: 6865
		public const int TVM_GETISEARCHSTRINGW = 4416;

		// Token: 0x04001AD2 RID: 6866
		public const int TVM_SETITEMHEIGHT = 4379;

		// Token: 0x04001AD3 RID: 6867
		public const int TVM_GETITEMHEIGHT = 4380;

		// Token: 0x04001AD4 RID: 6868
		public const int TVN_SELCHANGINGA = -401;

		// Token: 0x04001AD5 RID: 6869
		public const int TVN_SELCHANGINGW = -450;

		// Token: 0x04001AD6 RID: 6870
		public const int TVN_GETINFOTIPA = -413;

		// Token: 0x04001AD7 RID: 6871
		public const int TVN_GETINFOTIPW = -414;

		// Token: 0x04001AD8 RID: 6872
		public const int TVN_SELCHANGEDA = -402;

		// Token: 0x04001AD9 RID: 6873
		public const int TVN_SELCHANGEDW = -451;

		// Token: 0x04001ADA RID: 6874
		public const int TVC_UNKNOWN = 0;

		// Token: 0x04001ADB RID: 6875
		public const int TVC_BYMOUSE = 1;

		// Token: 0x04001ADC RID: 6876
		public const int TVC_BYKEYBOARD = 2;

		// Token: 0x04001ADD RID: 6877
		public const int TVN_GETDISPINFOA = -403;

		// Token: 0x04001ADE RID: 6878
		public const int TVN_GETDISPINFOW = -452;

		// Token: 0x04001ADF RID: 6879
		public const int TVN_SETDISPINFOA = -404;

		// Token: 0x04001AE0 RID: 6880
		public const int TVN_SETDISPINFOW = -453;

		// Token: 0x04001AE1 RID: 6881
		public const int TVN_ITEMEXPANDINGA = -405;

		// Token: 0x04001AE2 RID: 6882
		public const int TVN_ITEMEXPANDINGW = -454;

		// Token: 0x04001AE3 RID: 6883
		public const int TVN_ITEMEXPANDEDA = -406;

		// Token: 0x04001AE4 RID: 6884
		public const int TVN_ITEMEXPANDEDW = -455;

		// Token: 0x04001AE5 RID: 6885
		public const int TVN_BEGINDRAGA = -407;

		// Token: 0x04001AE6 RID: 6886
		public const int TVN_BEGINDRAGW = -456;

		// Token: 0x04001AE7 RID: 6887
		public const int TVN_BEGINRDRAGA = -408;

		// Token: 0x04001AE8 RID: 6888
		public const int TVN_BEGINRDRAGW = -457;

		// Token: 0x04001AE9 RID: 6889
		public const int TVN_BEGINLABELEDITA = -410;

		// Token: 0x04001AEA RID: 6890
		public const int TVN_BEGINLABELEDITW = -459;

		// Token: 0x04001AEB RID: 6891
		public const int TVN_ENDLABELEDITA = -411;

		// Token: 0x04001AEC RID: 6892
		public const int TVN_ENDLABELEDITW = -460;

		// Token: 0x04001AED RID: 6893
		public const int TCS_BOTTOM = 2;

		// Token: 0x04001AEE RID: 6894
		public const int TCS_RIGHT = 2;

		// Token: 0x04001AEF RID: 6895
		public const int TCS_FLATBUTTONS = 8;

		// Token: 0x04001AF0 RID: 6896
		public const int TCS_HOTTRACK = 64;

		// Token: 0x04001AF1 RID: 6897
		public const int TCS_VERTICAL = 128;

		// Token: 0x04001AF2 RID: 6898
		public const int TCS_TABS = 0;

		// Token: 0x04001AF3 RID: 6899
		public const int TCS_BUTTONS = 256;

		// Token: 0x04001AF4 RID: 6900
		public const int TCS_MULTILINE = 512;

		// Token: 0x04001AF5 RID: 6901
		public const int TCS_RIGHTJUSTIFY = 0;

		// Token: 0x04001AF6 RID: 6902
		public const int TCS_FIXEDWIDTH = 1024;

		// Token: 0x04001AF7 RID: 6903
		public const int TCS_RAGGEDRIGHT = 2048;

		// Token: 0x04001AF8 RID: 6904
		public const int TCS_OWNERDRAWFIXED = 8192;

		// Token: 0x04001AF9 RID: 6905
		public const int TCS_TOOLTIPS = 16384;

		// Token: 0x04001AFA RID: 6906
		public const int TCM_SETIMAGELIST = 4867;

		// Token: 0x04001AFB RID: 6907
		public const int TCIF_TEXT = 1;

		// Token: 0x04001AFC RID: 6908
		public const int TCIF_IMAGE = 2;

		// Token: 0x04001AFD RID: 6909
		public const int TCM_GETITEMA = 4869;

		// Token: 0x04001AFE RID: 6910
		public const int TCM_GETITEMW = 4924;

		// Token: 0x04001AFF RID: 6911
		public const int TCM_SETITEMA = 4870;

		// Token: 0x04001B00 RID: 6912
		public const int TCM_SETITEMW = 4925;

		// Token: 0x04001B01 RID: 6913
		public const int TCM_INSERTITEMA = 4871;

		// Token: 0x04001B02 RID: 6914
		public const int TCM_INSERTITEMW = 4926;

		// Token: 0x04001B03 RID: 6915
		public const int TCM_DELETEITEM = 4872;

		// Token: 0x04001B04 RID: 6916
		public const int TCM_DELETEALLITEMS = 4873;

		// Token: 0x04001B05 RID: 6917
		public const int TCM_GETITEMRECT = 4874;

		// Token: 0x04001B06 RID: 6918
		public const int TCM_GETCURSEL = 4875;

		// Token: 0x04001B07 RID: 6919
		public const int TCM_SETCURSEL = 4876;

		// Token: 0x04001B08 RID: 6920
		public const int TCM_ADJUSTRECT = 4904;

		// Token: 0x04001B09 RID: 6921
		public const int TCM_SETITEMSIZE = 4905;

		// Token: 0x04001B0A RID: 6922
		public const int TCM_SETPADDING = 4907;

		// Token: 0x04001B0B RID: 6923
		public const int TCM_GETROWCOUNT = 4908;

		// Token: 0x04001B0C RID: 6924
		public const int TCM_GETTOOLTIPS = 4909;

		// Token: 0x04001B0D RID: 6925
		public const int TCM_SETTOOLTIPS = 4910;

		// Token: 0x04001B0E RID: 6926
		public const int TCN_SELCHANGE = -551;

		// Token: 0x04001B0F RID: 6927
		public const int TCN_SELCHANGING = -552;

		// Token: 0x04001B10 RID: 6928
		public const int TBSTYLE_WRAPPABLE = 512;

		// Token: 0x04001B11 RID: 6929
		public const int TVM_SETBKCOLOR = 4381;

		// Token: 0x04001B12 RID: 6930
		public const int TVM_SETTEXTCOLOR = 4382;

		// Token: 0x04001B13 RID: 6931
		public const int TYMED_NULL = 0;

		// Token: 0x04001B14 RID: 6932
		public const int TVM_GETLINECOLOR = 4393;

		// Token: 0x04001B15 RID: 6933
		public const int TVM_SETLINECOLOR = 4392;

		// Token: 0x04001B16 RID: 6934
		public const int TVM_SETTOOLTIPS = 4376;

		// Token: 0x04001B17 RID: 6935
		public const int TVSIL_STATE = 2;

		// Token: 0x04001B18 RID: 6936
		public const int TVM_SORTCHILDRENCB = 4373;

		// Token: 0x04001B19 RID: 6937
		public const int TMPF_FIXED_PITCH = 1;

		// Token: 0x04001B1A RID: 6938
		public const int TVHT_NOWHERE = 1;

		// Token: 0x04001B1B RID: 6939
		public const int TVHT_ONITEMICON = 2;

		// Token: 0x04001B1C RID: 6940
		public const int TVHT_ONITEMLABEL = 4;

		// Token: 0x04001B1D RID: 6941
		public const int TVHT_ONITEM = 70;

		// Token: 0x04001B1E RID: 6942
		public const int TVHT_ONITEMINDENT = 8;

		// Token: 0x04001B1F RID: 6943
		public const int TVHT_ONITEMBUTTON = 16;

		// Token: 0x04001B20 RID: 6944
		public const int TVHT_ONITEMRIGHT = 32;

		// Token: 0x04001B21 RID: 6945
		public const int TVHT_ONITEMSTATEICON = 64;

		// Token: 0x04001B22 RID: 6946
		public const int TVHT_ABOVE = 256;

		// Token: 0x04001B23 RID: 6947
		public const int TVHT_BELOW = 512;

		// Token: 0x04001B24 RID: 6948
		public const int TVHT_TORIGHT = 1024;

		// Token: 0x04001B25 RID: 6949
		public const int TVHT_TOLEFT = 2048;

		// Token: 0x04001B26 RID: 6950
		public const int UIS_SET = 1;

		// Token: 0x04001B27 RID: 6951
		public const int UIS_CLEAR = 2;

		// Token: 0x04001B28 RID: 6952
		public const int UIS_INITIALIZE = 3;

		// Token: 0x04001B29 RID: 6953
		public const int UISF_HIDEFOCUS = 1;

		// Token: 0x04001B2A RID: 6954
		public const int UISF_HIDEACCEL = 2;

		// Token: 0x04001B2B RID: 6955
		public const int USERCLASSTYPE_FULL = 1;

		// Token: 0x04001B2C RID: 6956
		public const int USERCLASSTYPE_SHORT = 2;

		// Token: 0x04001B2D RID: 6957
		public const int USERCLASSTYPE_APPNAME = 3;

		// Token: 0x04001B2E RID: 6958
		public const int UOI_FLAGS = 1;

		// Token: 0x04001B2F RID: 6959
		public const int VIEW_E_DRAW = -2147221184;

		// Token: 0x04001B30 RID: 6960
		public const int VK_PRIOR = 33;

		// Token: 0x04001B31 RID: 6961
		public const int VK_NEXT = 34;

		// Token: 0x04001B32 RID: 6962
		public const int VK_LEFT = 37;

		// Token: 0x04001B33 RID: 6963
		public const int VK_UP = 38;

		// Token: 0x04001B34 RID: 6964
		public const int VK_RIGHT = 39;

		// Token: 0x04001B35 RID: 6965
		public const int VK_DOWN = 40;

		// Token: 0x04001B36 RID: 6966
		public const int VK_TAB = 9;

		// Token: 0x04001B37 RID: 6967
		public const int VK_SHIFT = 16;

		// Token: 0x04001B38 RID: 6968
		public const int VK_CONTROL = 17;

		// Token: 0x04001B39 RID: 6969
		public const int VK_MENU = 18;

		// Token: 0x04001B3A RID: 6970
		public const int VK_CAPITAL = 20;

		// Token: 0x04001B3B RID: 6971
		public const int VK_KANA = 21;

		// Token: 0x04001B3C RID: 6972
		public const int VK_ESCAPE = 27;

		// Token: 0x04001B3D RID: 6973
		public const int VK_END = 35;

		// Token: 0x04001B3E RID: 6974
		public const int VK_HOME = 36;

		// Token: 0x04001B3F RID: 6975
		public const int VK_NUMLOCK = 144;

		// Token: 0x04001B40 RID: 6976
		public const int VK_SCROLL = 145;

		// Token: 0x04001B41 RID: 6977
		public const int VK_INSERT = 45;

		// Token: 0x04001B42 RID: 6978
		public const int VK_DELETE = 46;

		// Token: 0x04001B43 RID: 6979
		public const int VK_LWIN = 91;

		// Token: 0x04001B44 RID: 6980
		public const int VK_RWIN = 92;

		// Token: 0x04001B45 RID: 6981
		public const int VK_APPS = 93;

		// Token: 0x04001B46 RID: 6982
		public const int VK_RCONTROL = 163;

		// Token: 0x04001B47 RID: 6983
		public const int VK_RMENU = 165;

		// Token: 0x04001B48 RID: 6984
		public const int WH_JOURNALPLAYBACK = 1;

		// Token: 0x04001B49 RID: 6985
		public const int WH_GETMESSAGE = 3;

		// Token: 0x04001B4A RID: 6986
		public const int WH_MOUSE = 7;

		// Token: 0x04001B4B RID: 6987
		public const int WSF_VISIBLE = 1;

		// Token: 0x04001B4C RID: 6988
		public const int WM_NULL = 0;

		// Token: 0x04001B4D RID: 6989
		public const int WM_CREATE = 1;

		// Token: 0x04001B4E RID: 6990
		public const int WM_DELETEITEM = 45;

		// Token: 0x04001B4F RID: 6991
		public const int WM_DESTROY = 2;

		// Token: 0x04001B50 RID: 6992
		public const int WM_MOVE = 3;

		// Token: 0x04001B51 RID: 6993
		public const int WM_SIZE = 5;

		// Token: 0x04001B52 RID: 6994
		public const int WM_ACTIVATE = 6;

		// Token: 0x04001B53 RID: 6995
		public const int WA_INACTIVE = 0;

		// Token: 0x04001B54 RID: 6996
		public const int WA_ACTIVE = 1;

		// Token: 0x04001B55 RID: 6997
		public const int WA_CLICKACTIVE = 2;

		// Token: 0x04001B56 RID: 6998
		public const int WM_SETFOCUS = 7;

		// Token: 0x04001B57 RID: 6999
		public const int WM_KILLFOCUS = 8;

		// Token: 0x04001B58 RID: 7000
		public const int WM_ENABLE = 10;

		// Token: 0x04001B59 RID: 7001
		public const int WM_SETREDRAW = 11;

		// Token: 0x04001B5A RID: 7002
		public const int WM_SETTEXT = 12;

		// Token: 0x04001B5B RID: 7003
		public const int WM_GETTEXT = 13;

		// Token: 0x04001B5C RID: 7004
		public const int WM_GETTEXTLENGTH = 14;

		// Token: 0x04001B5D RID: 7005
		public const int WM_PAINT = 15;

		// Token: 0x04001B5E RID: 7006
		public const int WM_CLOSE = 16;

		// Token: 0x04001B5F RID: 7007
		public const int WM_QUERYENDSESSION = 17;

		// Token: 0x04001B60 RID: 7008
		public const int WM_QUIT = 18;

		// Token: 0x04001B61 RID: 7009
		public const int WM_QUERYOPEN = 19;

		// Token: 0x04001B62 RID: 7010
		public const int WM_ERASEBKGND = 20;

		// Token: 0x04001B63 RID: 7011
		public const int WM_SYSCOLORCHANGE = 21;

		// Token: 0x04001B64 RID: 7012
		public const int WM_ENDSESSION = 22;

		// Token: 0x04001B65 RID: 7013
		public const int WM_SHOWWINDOW = 24;

		// Token: 0x04001B66 RID: 7014
		public const int WM_WININICHANGE = 26;

		// Token: 0x04001B67 RID: 7015
		public const int WM_SETTINGCHANGE = 26;

		// Token: 0x04001B68 RID: 7016
		public const int WM_DEVMODECHANGE = 27;

		// Token: 0x04001B69 RID: 7017
		public const int WM_ACTIVATEAPP = 28;

		// Token: 0x04001B6A RID: 7018
		public const int WM_FONTCHANGE = 29;

		// Token: 0x04001B6B RID: 7019
		public const int WM_TIMECHANGE = 30;

		// Token: 0x04001B6C RID: 7020
		public const int WM_CANCELMODE = 31;

		// Token: 0x04001B6D RID: 7021
		public const int WM_SETCURSOR = 32;

		// Token: 0x04001B6E RID: 7022
		public const int WM_MOUSEACTIVATE = 33;

		// Token: 0x04001B6F RID: 7023
		public const int WM_CHILDACTIVATE = 34;

		// Token: 0x04001B70 RID: 7024
		public const int WM_QUEUESYNC = 35;

		// Token: 0x04001B71 RID: 7025
		public const int WM_GETMINMAXINFO = 36;

		// Token: 0x04001B72 RID: 7026
		public const int WM_PAINTICON = 38;

		// Token: 0x04001B73 RID: 7027
		public const int WM_ICONERASEBKGND = 39;

		// Token: 0x04001B74 RID: 7028
		public const int WM_NEXTDLGCTL = 40;

		// Token: 0x04001B75 RID: 7029
		public const int WM_SPOOLERSTATUS = 42;

		// Token: 0x04001B76 RID: 7030
		public const int WM_DRAWITEM = 43;

		// Token: 0x04001B77 RID: 7031
		public const int WM_MEASUREITEM = 44;

		// Token: 0x04001B78 RID: 7032
		public const int WM_VKEYTOITEM = 46;

		// Token: 0x04001B79 RID: 7033
		public const int WM_CHARTOITEM = 47;

		// Token: 0x04001B7A RID: 7034
		public const int WM_SETFONT = 48;

		// Token: 0x04001B7B RID: 7035
		public const int WM_GETFONT = 49;

		// Token: 0x04001B7C RID: 7036
		public const int WM_SETHOTKEY = 50;

		// Token: 0x04001B7D RID: 7037
		public const int WM_GETHOTKEY = 51;

		// Token: 0x04001B7E RID: 7038
		public const int WM_QUERYDRAGICON = 55;

		// Token: 0x04001B7F RID: 7039
		public const int WM_COMPAREITEM = 57;

		// Token: 0x04001B80 RID: 7040
		public const int WM_GETOBJECT = 61;

		// Token: 0x04001B81 RID: 7041
		public const int WM_COMPACTING = 65;

		// Token: 0x04001B82 RID: 7042
		public const int WM_COMMNOTIFY = 68;

		// Token: 0x04001B83 RID: 7043
		public const int WM_WINDOWPOSCHANGING = 70;

		// Token: 0x04001B84 RID: 7044
		public const int WM_WINDOWPOSCHANGED = 71;

		// Token: 0x04001B85 RID: 7045
		public const int WM_POWER = 72;

		// Token: 0x04001B86 RID: 7046
		public const int WM_COPYDATA = 74;

		// Token: 0x04001B87 RID: 7047
		public const int WM_CANCELJOURNAL = 75;

		// Token: 0x04001B88 RID: 7048
		public const int WM_NOTIFY = 78;

		// Token: 0x04001B89 RID: 7049
		public const int WM_INPUTLANGCHANGEREQUEST = 80;

		// Token: 0x04001B8A RID: 7050
		public const int WM_INPUTLANGCHANGE = 81;

		// Token: 0x04001B8B RID: 7051
		public const int WM_TCARD = 82;

		// Token: 0x04001B8C RID: 7052
		public const int WM_HELP = 83;

		// Token: 0x04001B8D RID: 7053
		public const int WM_USERCHANGED = 84;

		// Token: 0x04001B8E RID: 7054
		public const int WM_NOTIFYFORMAT = 85;

		// Token: 0x04001B8F RID: 7055
		public const int WM_CONTEXTMENU = 123;

		// Token: 0x04001B90 RID: 7056
		public const int WM_STYLECHANGING = 124;

		// Token: 0x04001B91 RID: 7057
		public const int WM_STYLECHANGED = 125;

		// Token: 0x04001B92 RID: 7058
		public const int WM_DISPLAYCHANGE = 126;

		// Token: 0x04001B93 RID: 7059
		public const int WM_GETICON = 127;

		// Token: 0x04001B94 RID: 7060
		public const int WM_SETICON = 128;

		// Token: 0x04001B95 RID: 7061
		public const int WM_NCCREATE = 129;

		// Token: 0x04001B96 RID: 7062
		public const int WM_NCDESTROY = 130;

		// Token: 0x04001B97 RID: 7063
		public const int WM_NCCALCSIZE = 131;

		// Token: 0x04001B98 RID: 7064
		public const int WM_NCHITTEST = 132;

		// Token: 0x04001B99 RID: 7065
		public const int WM_NCPAINT = 133;

		// Token: 0x04001B9A RID: 7066
		public const int WM_NCACTIVATE = 134;

		// Token: 0x04001B9B RID: 7067
		public const int WM_GETDLGCODE = 135;

		// Token: 0x04001B9C RID: 7068
		public const int WM_NCMOUSEMOVE = 160;

		// Token: 0x04001B9D RID: 7069
		public const int WM_NCMOUSELEAVE = 674;

		// Token: 0x04001B9E RID: 7070
		public const int WM_NCLBUTTONDOWN = 161;

		// Token: 0x04001B9F RID: 7071
		public const int WM_NCLBUTTONUP = 162;

		// Token: 0x04001BA0 RID: 7072
		public const int WM_NCLBUTTONDBLCLK = 163;

		// Token: 0x04001BA1 RID: 7073
		public const int WM_NCRBUTTONDOWN = 164;

		// Token: 0x04001BA2 RID: 7074
		public const int WM_NCRBUTTONUP = 165;

		// Token: 0x04001BA3 RID: 7075
		public const int WM_NCRBUTTONDBLCLK = 166;

		// Token: 0x04001BA4 RID: 7076
		public const int WM_NCMBUTTONDOWN = 167;

		// Token: 0x04001BA5 RID: 7077
		public const int WM_NCMBUTTONUP = 168;

		// Token: 0x04001BA6 RID: 7078
		public const int WM_NCMBUTTONDBLCLK = 169;

		// Token: 0x04001BA7 RID: 7079
		public const int WM_NCXBUTTONDOWN = 171;

		// Token: 0x04001BA8 RID: 7080
		public const int WM_NCXBUTTONUP = 172;

		// Token: 0x04001BA9 RID: 7081
		public const int WM_NCXBUTTONDBLCLK = 173;

		// Token: 0x04001BAA RID: 7082
		public const int WM_KEYFIRST = 256;

		// Token: 0x04001BAB RID: 7083
		public const int WM_KEYDOWN = 256;

		// Token: 0x04001BAC RID: 7084
		public const int WM_KEYUP = 257;

		// Token: 0x04001BAD RID: 7085
		public const int WM_CHAR = 258;

		// Token: 0x04001BAE RID: 7086
		public const int WM_DEADCHAR = 259;

		// Token: 0x04001BAF RID: 7087
		public const int WM_CTLCOLOR = 25;

		// Token: 0x04001BB0 RID: 7088
		public const int WM_SYSKEYDOWN = 260;

		// Token: 0x04001BB1 RID: 7089
		public const int WM_SYSKEYUP = 261;

		// Token: 0x04001BB2 RID: 7090
		public const int WM_SYSCHAR = 262;

		// Token: 0x04001BB3 RID: 7091
		public const int WM_SYSDEADCHAR = 263;

		// Token: 0x04001BB4 RID: 7092
		public const int WM_KEYLAST = 264;

		// Token: 0x04001BB5 RID: 7093
		public const int WM_IME_STARTCOMPOSITION = 269;

		// Token: 0x04001BB6 RID: 7094
		public const int WM_IME_ENDCOMPOSITION = 270;

		// Token: 0x04001BB7 RID: 7095
		public const int WM_IME_COMPOSITION = 271;

		// Token: 0x04001BB8 RID: 7096
		public const int WM_IME_KEYLAST = 271;

		// Token: 0x04001BB9 RID: 7097
		public const int WM_INITDIALOG = 272;

		// Token: 0x04001BBA RID: 7098
		public const int WM_COMMAND = 273;

		// Token: 0x04001BBB RID: 7099
		public const int WM_SYSCOMMAND = 274;

		// Token: 0x04001BBC RID: 7100
		public const int WM_TIMER = 275;

		// Token: 0x04001BBD RID: 7101
		public const int WM_HSCROLL = 276;

		// Token: 0x04001BBE RID: 7102
		public const int WM_VSCROLL = 277;

		// Token: 0x04001BBF RID: 7103
		public const int WM_INITMENU = 278;

		// Token: 0x04001BC0 RID: 7104
		public const int WM_INITMENUPOPUP = 279;

		// Token: 0x04001BC1 RID: 7105
		public const int WM_MENUSELECT = 287;

		// Token: 0x04001BC2 RID: 7106
		public const int WM_MENUCHAR = 288;

		// Token: 0x04001BC3 RID: 7107
		public const int WM_ENTERIDLE = 289;

		// Token: 0x04001BC4 RID: 7108
		public const int WM_UNINITMENUPOPUP = 293;

		// Token: 0x04001BC5 RID: 7109
		public const int WM_CHANGEUISTATE = 295;

		// Token: 0x04001BC6 RID: 7110
		public const int WM_UPDATEUISTATE = 296;

		// Token: 0x04001BC7 RID: 7111
		public const int WM_QUERYUISTATE = 297;

		// Token: 0x04001BC8 RID: 7112
		public const int WM_CTLCOLORMSGBOX = 306;

		// Token: 0x04001BC9 RID: 7113
		public const int WM_CTLCOLOREDIT = 307;

		// Token: 0x04001BCA RID: 7114
		public const int WM_CTLCOLORLISTBOX = 308;

		// Token: 0x04001BCB RID: 7115
		public const int WM_CTLCOLORBTN = 309;

		// Token: 0x04001BCC RID: 7116
		public const int WM_CTLCOLORDLG = 310;

		// Token: 0x04001BCD RID: 7117
		public const int WM_CTLCOLORSCROLLBAR = 311;

		// Token: 0x04001BCE RID: 7118
		public const int WM_CTLCOLORSTATIC = 312;

		// Token: 0x04001BCF RID: 7119
		public const int WM_MOUSEFIRST = 512;

		// Token: 0x04001BD0 RID: 7120
		public const int WM_MOUSEMOVE = 512;

		// Token: 0x04001BD1 RID: 7121
		public const int WM_LBUTTONDOWN = 513;

		// Token: 0x04001BD2 RID: 7122
		public const int WM_LBUTTONUP = 514;

		// Token: 0x04001BD3 RID: 7123
		public const int WM_LBUTTONDBLCLK = 515;

		// Token: 0x04001BD4 RID: 7124
		public const int WM_RBUTTONDOWN = 516;

		// Token: 0x04001BD5 RID: 7125
		public const int WM_RBUTTONUP = 517;

		// Token: 0x04001BD6 RID: 7126
		public const int WM_RBUTTONDBLCLK = 518;

		// Token: 0x04001BD7 RID: 7127
		public const int WM_MBUTTONDOWN = 519;

		// Token: 0x04001BD8 RID: 7128
		public const int WM_MBUTTONUP = 520;

		// Token: 0x04001BD9 RID: 7129
		public const int WM_MBUTTONDBLCLK = 521;

		// Token: 0x04001BDA RID: 7130
		public const int WM_XBUTTONDOWN = 523;

		// Token: 0x04001BDB RID: 7131
		public const int WM_XBUTTONUP = 524;

		// Token: 0x04001BDC RID: 7132
		public const int WM_XBUTTONDBLCLK = 525;

		// Token: 0x04001BDD RID: 7133
		public const int WM_MOUSEWHEEL = 522;

		// Token: 0x04001BDE RID: 7134
		public const int WM_MOUSELAST = 522;

		// Token: 0x04001BDF RID: 7135
		public const int WHEEL_DELTA = 120;

		// Token: 0x04001BE0 RID: 7136
		public const int WM_PARENTNOTIFY = 528;

		// Token: 0x04001BE1 RID: 7137
		public const int WM_ENTERMENULOOP = 529;

		// Token: 0x04001BE2 RID: 7138
		public const int WM_EXITMENULOOP = 530;

		// Token: 0x04001BE3 RID: 7139
		public const int WM_NEXTMENU = 531;

		// Token: 0x04001BE4 RID: 7140
		public const int WM_SIZING = 532;

		// Token: 0x04001BE5 RID: 7141
		public const int WM_CAPTURECHANGED = 533;

		// Token: 0x04001BE6 RID: 7142
		public const int WM_MOVING = 534;

		// Token: 0x04001BE7 RID: 7143
		public const int WM_POWERBROADCAST = 536;

		// Token: 0x04001BE8 RID: 7144
		public const int WM_DEVICECHANGE = 537;

		// Token: 0x04001BE9 RID: 7145
		public const int WM_IME_SETCONTEXT = 641;

		// Token: 0x04001BEA RID: 7146
		public const int WM_IME_NOTIFY = 642;

		// Token: 0x04001BEB RID: 7147
		public const int WM_IME_CONTROL = 643;

		// Token: 0x04001BEC RID: 7148
		public const int WM_IME_COMPOSITIONFULL = 644;

		// Token: 0x04001BED RID: 7149
		public const int WM_IME_SELECT = 645;

		// Token: 0x04001BEE RID: 7150
		public const int WM_IME_CHAR = 646;

		// Token: 0x04001BEF RID: 7151
		public const int WM_IME_KEYDOWN = 656;

		// Token: 0x04001BF0 RID: 7152
		public const int WM_IME_KEYUP = 657;

		// Token: 0x04001BF1 RID: 7153
		public const int WM_MDICREATE = 544;

		// Token: 0x04001BF2 RID: 7154
		public const int WM_MDIDESTROY = 545;

		// Token: 0x04001BF3 RID: 7155
		public const int WM_MDIACTIVATE = 546;

		// Token: 0x04001BF4 RID: 7156
		public const int WM_MDIRESTORE = 547;

		// Token: 0x04001BF5 RID: 7157
		public const int WM_MDINEXT = 548;

		// Token: 0x04001BF6 RID: 7158
		public const int WM_MDIMAXIMIZE = 549;

		// Token: 0x04001BF7 RID: 7159
		public const int WM_MDITILE = 550;

		// Token: 0x04001BF8 RID: 7160
		public const int WM_MDICASCADE = 551;

		// Token: 0x04001BF9 RID: 7161
		public const int WM_MDIICONARRANGE = 552;

		// Token: 0x04001BFA RID: 7162
		public const int WM_MDIGETACTIVE = 553;

		// Token: 0x04001BFB RID: 7163
		public const int WM_MDISETMENU = 560;

		// Token: 0x04001BFC RID: 7164
		public const int WM_ENTERSIZEMOVE = 561;

		// Token: 0x04001BFD RID: 7165
		public const int WM_EXITSIZEMOVE = 562;

		// Token: 0x04001BFE RID: 7166
		public const int WM_DROPFILES = 563;

		// Token: 0x04001BFF RID: 7167
		public const int WM_MDIREFRESHMENU = 564;

		// Token: 0x04001C00 RID: 7168
		public const int WM_MOUSEHOVER = 673;

		// Token: 0x04001C01 RID: 7169
		public const int WM_MOUSELEAVE = 675;

		// Token: 0x04001C02 RID: 7170
		public const int WM_DPICHANGED = 736;

		// Token: 0x04001C03 RID: 7171
		public const int WM_GETDPISCALEDSIZE = 737;

		// Token: 0x04001C04 RID: 7172
		public const int WM_DPICHANGED_BEFOREPARENT = 738;

		// Token: 0x04001C05 RID: 7173
		public const int WM_DPICHANGED_AFTERPARENT = 739;

		// Token: 0x04001C06 RID: 7174
		public const int WM_CUT = 768;

		// Token: 0x04001C07 RID: 7175
		public const int WM_COPY = 769;

		// Token: 0x04001C08 RID: 7176
		public const int WM_PASTE = 770;

		// Token: 0x04001C09 RID: 7177
		public const int WM_CLEAR = 771;

		// Token: 0x04001C0A RID: 7178
		public const int WM_UNDO = 772;

		// Token: 0x04001C0B RID: 7179
		public const int WM_RENDERFORMAT = 773;

		// Token: 0x04001C0C RID: 7180
		public const int WM_RENDERALLFORMATS = 774;

		// Token: 0x04001C0D RID: 7181
		public const int WM_DESTROYCLIPBOARD = 775;

		// Token: 0x04001C0E RID: 7182
		public const int WM_DRAWCLIPBOARD = 776;

		// Token: 0x04001C0F RID: 7183
		public const int WM_PAINTCLIPBOARD = 777;

		// Token: 0x04001C10 RID: 7184
		public const int WM_VSCROLLCLIPBOARD = 778;

		// Token: 0x04001C11 RID: 7185
		public const int WM_SIZECLIPBOARD = 779;

		// Token: 0x04001C12 RID: 7186
		public const int WM_ASKCBFORMATNAME = 780;

		// Token: 0x04001C13 RID: 7187
		public const int WM_CHANGECBCHAIN = 781;

		// Token: 0x04001C14 RID: 7188
		public const int WM_HSCROLLCLIPBOARD = 782;

		// Token: 0x04001C15 RID: 7189
		public const int WM_QUERYNEWPALETTE = 783;

		// Token: 0x04001C16 RID: 7190
		public const int WM_PALETTEISCHANGING = 784;

		// Token: 0x04001C17 RID: 7191
		public const int WM_PALETTECHANGED = 785;

		// Token: 0x04001C18 RID: 7192
		public const int WM_HOTKEY = 786;

		// Token: 0x04001C19 RID: 7193
		public const int WM_PRINT = 791;

		// Token: 0x04001C1A RID: 7194
		public const int WM_PRINTCLIENT = 792;

		// Token: 0x04001C1B RID: 7195
		public const int WM_THEMECHANGED = 794;

		// Token: 0x04001C1C RID: 7196
		public const int WM_HANDHELDFIRST = 856;

		// Token: 0x04001C1D RID: 7197
		public const int WM_HANDHELDLAST = 863;

		// Token: 0x04001C1E RID: 7198
		public const int WM_AFXFIRST = 864;

		// Token: 0x04001C1F RID: 7199
		public const int WM_AFXLAST = 895;

		// Token: 0x04001C20 RID: 7200
		public const int WM_PENWINFIRST = 896;

		// Token: 0x04001C21 RID: 7201
		public const int WM_PENWINLAST = 911;

		// Token: 0x04001C22 RID: 7202
		public const int WM_APP = 32768;

		// Token: 0x04001C23 RID: 7203
		public const int WM_USER = 1024;

		// Token: 0x04001C24 RID: 7204
		public const int WM_REFLECT = 8192;

		// Token: 0x04001C25 RID: 7205
		public const int WS_OVERLAPPED = 0;

		// Token: 0x04001C26 RID: 7206
		public const int WS_POPUP = -2147483648;

		// Token: 0x04001C27 RID: 7207
		public const int WS_CHILD = 1073741824;

		// Token: 0x04001C28 RID: 7208
		public const int WS_MINIMIZE = 536870912;

		// Token: 0x04001C29 RID: 7209
		public const int WS_VISIBLE = 268435456;

		// Token: 0x04001C2A RID: 7210
		public const int WS_DISABLED = 134217728;

		// Token: 0x04001C2B RID: 7211
		public const int WS_CLIPSIBLINGS = 67108864;

		// Token: 0x04001C2C RID: 7212
		public const int WS_CLIPCHILDREN = 33554432;

		// Token: 0x04001C2D RID: 7213
		public const int WS_MAXIMIZE = 16777216;

		// Token: 0x04001C2E RID: 7214
		public const int WS_CAPTION = 12582912;

		// Token: 0x04001C2F RID: 7215
		public const int WS_BORDER = 8388608;

		// Token: 0x04001C30 RID: 7216
		public const int WS_DLGFRAME = 4194304;

		// Token: 0x04001C31 RID: 7217
		public const int WS_VSCROLL = 2097152;

		// Token: 0x04001C32 RID: 7218
		public const int WS_HSCROLL = 1048576;

		// Token: 0x04001C33 RID: 7219
		public const int WS_SYSMENU = 524288;

		// Token: 0x04001C34 RID: 7220
		public const int WS_THICKFRAME = 262144;

		// Token: 0x04001C35 RID: 7221
		public const int WS_TABSTOP = 65536;

		// Token: 0x04001C36 RID: 7222
		public const int WS_MINIMIZEBOX = 131072;

		// Token: 0x04001C37 RID: 7223
		public const int WS_MAXIMIZEBOX = 65536;

		// Token: 0x04001C38 RID: 7224
		public const int WS_EX_DEFAULT = 0;

		// Token: 0x04001C39 RID: 7225
		public const int WS_EX_DLGMODALFRAME = 1;

		// Token: 0x04001C3A RID: 7226
		public const int WS_EX_MDICHILD = 64;

		// Token: 0x04001C3B RID: 7227
		public const int WS_EX_TOOLWINDOW = 128;

		// Token: 0x04001C3C RID: 7228
		public const int WS_EX_CLIENTEDGE = 512;

		// Token: 0x04001C3D RID: 7229
		public const int WS_EX_CONTEXTHELP = 1024;

		// Token: 0x04001C3E RID: 7230
		public const int WS_EX_RIGHT = 4096;

		// Token: 0x04001C3F RID: 7231
		public const int WS_EX_LEFT = 0;

		// Token: 0x04001C40 RID: 7232
		public const int WS_EX_RTLREADING = 8192;

		// Token: 0x04001C41 RID: 7233
		public const int WS_EX_LTRREADING = 0;

		// Token: 0x04001C42 RID: 7234
		public const int WS_EX_LEFTSCROLLBAR = 16384;

		// Token: 0x04001C43 RID: 7235
		public const int WS_EX_CONTROLPARENT = 65536;

		// Token: 0x04001C44 RID: 7236
		public const int WS_EX_STATICEDGE = 131072;

		// Token: 0x04001C45 RID: 7237
		public const int WS_EX_APPWINDOW = 262144;

		// Token: 0x04001C46 RID: 7238
		public const int WS_EX_LAYERED = 524288;

		// Token: 0x04001C47 RID: 7239
		public const int WS_EX_TOPMOST = 8;

		// Token: 0x04001C48 RID: 7240
		public const int WS_EX_LAYOUTRTL = 4194304;

		// Token: 0x04001C49 RID: 7241
		public const int WS_EX_NOINHERITLAYOUT = 1048576;

		// Token: 0x04001C4A RID: 7242
		public const int WPF_SETMINPOSITION = 1;

		// Token: 0x04001C4B RID: 7243
		public const int WM_CHOOSEFONT_GETLOGFONT = 1025;

		// Token: 0x04001C4C RID: 7244
		public const int IMN_OPENSTATUSWINDOW = 2;

		// Token: 0x04001C4D RID: 7245
		public const int IMN_SETCONVERSIONMODE = 6;

		// Token: 0x04001C4E RID: 7246
		public const int IMN_SETOPENSTATUS = 8;

		// Token: 0x04001C4F RID: 7247
		public static int START_PAGE_GENERAL = -1;

		// Token: 0x04001C50 RID: 7248
		public const int PD_RESULT_CANCEL = 0;

		// Token: 0x04001C51 RID: 7249
		public const int PD_RESULT_PRINT = 1;

		// Token: 0x04001C52 RID: 7250
		public const int PD_RESULT_APPLY = 2;

		// Token: 0x04001C53 RID: 7251
		private static int wmMouseEnterMessage = -1;

		// Token: 0x04001C54 RID: 7252
		private static int wmUnSubclass = -1;

		// Token: 0x04001C55 RID: 7253
		public const int XBUTTON1 = 1;

		// Token: 0x04001C56 RID: 7254
		public const int XBUTTON2 = 2;

		// Token: 0x04001C57 RID: 7255
		public static readonly int BFFM_SETSELECTION;

		// Token: 0x04001C58 RID: 7256
		public static readonly int CBEM_GETITEM;

		// Token: 0x04001C59 RID: 7257
		public static readonly int CBEM_SETITEM;

		// Token: 0x04001C5A RID: 7258
		public static readonly int CBEN_ENDEDIT;

		// Token: 0x04001C5B RID: 7259
		public static readonly int CBEM_INSERTITEM;

		// Token: 0x04001C5C RID: 7260
		public static readonly int LVM_GETITEMTEXT;

		// Token: 0x04001C5D RID: 7261
		public static readonly int LVM_SETITEMTEXT;

		// Token: 0x04001C5E RID: 7262
		public static readonly int ACM_OPEN;

		// Token: 0x04001C5F RID: 7263
		public static readonly int DTM_SETFORMAT;

		// Token: 0x04001C60 RID: 7264
		public static readonly int DTN_USERSTRING;

		// Token: 0x04001C61 RID: 7265
		public static readonly int DTN_WMKEYDOWN;

		// Token: 0x04001C62 RID: 7266
		public static readonly int DTN_FORMAT;

		// Token: 0x04001C63 RID: 7267
		public static readonly int DTN_FORMATQUERY;

		// Token: 0x04001C64 RID: 7268
		public static readonly int EMR_POLYTEXTOUT;

		// Token: 0x04001C65 RID: 7269
		public static readonly int HDM_INSERTITEM;

		// Token: 0x04001C66 RID: 7270
		public static readonly int HDM_GETITEM;

		// Token: 0x04001C67 RID: 7271
		public static readonly int HDM_SETITEM;

		// Token: 0x04001C68 RID: 7272
		public static readonly int HDN_ITEMCHANGING;

		// Token: 0x04001C69 RID: 7273
		public static readonly int HDN_ITEMCHANGED;

		// Token: 0x04001C6A RID: 7274
		public static readonly int HDN_ITEMCLICK;

		// Token: 0x04001C6B RID: 7275
		public static readonly int HDN_ITEMDBLCLICK;

		// Token: 0x04001C6C RID: 7276
		public static readonly int HDN_DIVIDERDBLCLICK;

		// Token: 0x04001C6D RID: 7277
		public static readonly int HDN_BEGINTRACK;

		// Token: 0x04001C6E RID: 7278
		public static readonly int HDN_ENDTRACK;

		// Token: 0x04001C6F RID: 7279
		public static readonly int HDN_TRACK;

		// Token: 0x04001C70 RID: 7280
		public static readonly int HDN_GETDISPINFO;

		// Token: 0x04001C71 RID: 7281
		public static readonly int LVM_GETITEM;

		// Token: 0x04001C72 RID: 7282
		public static readonly int LVM_SETBKIMAGE;

		// Token: 0x04001C73 RID: 7283
		public static readonly int LVM_SETITEM;

		// Token: 0x04001C74 RID: 7284
		public static readonly int LVM_INSERTITEM;

		// Token: 0x04001C75 RID: 7285
		public static readonly int LVM_FINDITEM;

		// Token: 0x04001C76 RID: 7286
		public static readonly int LVM_GETSTRINGWIDTH;

		// Token: 0x04001C77 RID: 7287
		public static readonly int LVM_EDITLABEL;

		// Token: 0x04001C78 RID: 7288
		public static readonly int LVM_GETCOLUMN;

		// Token: 0x04001C79 RID: 7289
		public static readonly int LVM_SETCOLUMN;

		// Token: 0x04001C7A RID: 7290
		public static readonly int LVM_GETISEARCHSTRING;

		// Token: 0x04001C7B RID: 7291
		public static readonly int LVM_INSERTCOLUMN;

		// Token: 0x04001C7C RID: 7292
		public static readonly int LVN_BEGINLABELEDIT;

		// Token: 0x04001C7D RID: 7293
		public static readonly int LVN_ENDLABELEDIT;

		// Token: 0x04001C7E RID: 7294
		public static readonly int LVN_ODFINDITEM;

		// Token: 0x04001C7F RID: 7295
		public static readonly int LVN_GETDISPINFO;

		// Token: 0x04001C80 RID: 7296
		public static readonly int LVN_GETINFOTIP;

		// Token: 0x04001C81 RID: 7297
		public static readonly int LVN_SETDISPINFO;

		// Token: 0x04001C82 RID: 7298
		public static readonly int PSM_SETTITLE;

		// Token: 0x04001C83 RID: 7299
		public static readonly int PSM_SETFINISHTEXT;

		// Token: 0x04001C84 RID: 7300
		public static readonly int RB_INSERTBAND;

		// Token: 0x04001C85 RID: 7301
		public static readonly int SB_SETTEXT;

		// Token: 0x04001C86 RID: 7302
		public static readonly int SB_GETTEXT;

		// Token: 0x04001C87 RID: 7303
		public static readonly int SB_GETTEXTLENGTH;

		// Token: 0x04001C88 RID: 7304
		public static readonly int SB_SETTIPTEXT;

		// Token: 0x04001C89 RID: 7305
		public static readonly int SB_GETTIPTEXT;

		// Token: 0x04001C8A RID: 7306
		public static readonly int TB_SAVERESTORE;

		// Token: 0x04001C8B RID: 7307
		public static readonly int TB_ADDSTRING;

		// Token: 0x04001C8C RID: 7308
		public static readonly int TB_GETBUTTONTEXT;

		// Token: 0x04001C8D RID: 7309
		public static readonly int TB_MAPACCELERATOR;

		// Token: 0x04001C8E RID: 7310
		public static readonly int TB_GETBUTTONINFO;

		// Token: 0x04001C8F RID: 7311
		public static readonly int TB_SETBUTTONINFO;

		// Token: 0x04001C90 RID: 7312
		public static readonly int TB_INSERTBUTTON;

		// Token: 0x04001C91 RID: 7313
		public static readonly int TB_ADDBUTTONS;

		// Token: 0x04001C92 RID: 7314
		public static readonly int TBN_GETBUTTONINFO;

		// Token: 0x04001C93 RID: 7315
		public static readonly int TBN_GETINFOTIP;

		// Token: 0x04001C94 RID: 7316
		public static readonly int TBN_GETDISPINFO;

		// Token: 0x04001C95 RID: 7317
		public static readonly int TTM_ADDTOOL;

		// Token: 0x04001C96 RID: 7318
		public static readonly int TTM_SETTITLE;

		// Token: 0x04001C97 RID: 7319
		public static readonly int TTM_DELTOOL;

		// Token: 0x04001C98 RID: 7320
		public static readonly int TTM_NEWTOOLRECT;

		// Token: 0x04001C99 RID: 7321
		public static readonly int TTM_GETTOOLINFO;

		// Token: 0x04001C9A RID: 7322
		public static readonly int TTM_SETTOOLINFO;

		// Token: 0x04001C9B RID: 7323
		public static readonly int TTM_HITTEST;

		// Token: 0x04001C9C RID: 7324
		public static readonly int TTM_GETTEXT;

		// Token: 0x04001C9D RID: 7325
		public static readonly int TTM_UPDATETIPTEXT;

		// Token: 0x04001C9E RID: 7326
		public static readonly int TTM_ENUMTOOLS;

		// Token: 0x04001C9F RID: 7327
		public static readonly int TTM_GETCURRENTTOOL;

		// Token: 0x04001CA0 RID: 7328
		public static readonly int TTN_GETDISPINFO;

		// Token: 0x04001CA1 RID: 7329
		public static readonly int TTN_NEEDTEXT;

		// Token: 0x04001CA2 RID: 7330
		public static readonly int TVM_INSERTITEM;

		// Token: 0x04001CA3 RID: 7331
		public static readonly int TVM_GETITEM;

		// Token: 0x04001CA4 RID: 7332
		public static readonly int TVM_SETITEM;

		// Token: 0x04001CA5 RID: 7333
		public static readonly int TVM_EDITLABEL;

		// Token: 0x04001CA6 RID: 7334
		public static readonly int TVM_GETISEARCHSTRING;

		// Token: 0x04001CA7 RID: 7335
		public static readonly int TVN_SELCHANGING;

		// Token: 0x04001CA8 RID: 7336
		public static readonly int TVN_SELCHANGED;

		// Token: 0x04001CA9 RID: 7337
		public static readonly int TVN_GETDISPINFO;

		// Token: 0x04001CAA RID: 7338
		public static readonly int TVN_SETDISPINFO;

		// Token: 0x04001CAB RID: 7339
		public static readonly int TVN_ITEMEXPANDING;

		// Token: 0x04001CAC RID: 7340
		public static readonly int TVN_ITEMEXPANDED;

		// Token: 0x04001CAD RID: 7341
		public static readonly int TVN_BEGINDRAG;

		// Token: 0x04001CAE RID: 7342
		public static readonly int TVN_BEGINRDRAG;

		// Token: 0x04001CAF RID: 7343
		public static readonly int TVN_BEGINLABELEDIT;

		// Token: 0x04001CB0 RID: 7344
		public static readonly int TVN_ENDLABELEDIT;

		// Token: 0x04001CB1 RID: 7345
		public static readonly int TCM_GETITEM;

		// Token: 0x04001CB2 RID: 7346
		public static readonly int TCM_SETITEM;

		// Token: 0x04001CB3 RID: 7347
		public static readonly int TCM_INSERTITEM;

		// Token: 0x04001CB4 RID: 7348
		public const string TOOLTIPS_CLASS = "tooltips_class32";

		// Token: 0x04001CB5 RID: 7349
		public const string WC_DATETIMEPICK = "SysDateTimePick32";

		// Token: 0x04001CB6 RID: 7350
		public const string WC_LISTVIEW = "SysListView32";

		// Token: 0x04001CB7 RID: 7351
		public const string WC_MONTHCAL = "SysMonthCal32";

		// Token: 0x04001CB8 RID: 7352
		public const string WC_PROGRESS = "msctls_progress32";

		// Token: 0x04001CB9 RID: 7353
		public const string WC_STATUSBAR = "msctls_statusbar32";

		// Token: 0x04001CBA RID: 7354
		public const string WC_TOOLBAR = "ToolbarWindow32";

		// Token: 0x04001CBB RID: 7355
		public const string WC_TRACKBAR = "msctls_trackbar32";

		// Token: 0x04001CBC RID: 7356
		public const string WC_TREEVIEW = "SysTreeView32";

		// Token: 0x04001CBD RID: 7357
		public const string WC_TABCONTROL = "SysTabControl32";

		// Token: 0x04001CBE RID: 7358
		public const string MSH_MOUSEWHEEL = "MSWHEEL_ROLLMSG";

		// Token: 0x04001CBF RID: 7359
		public const string MSH_SCROLL_LINES = "MSH_SCROLL_LINES_MSG";

		// Token: 0x04001CC0 RID: 7360
		public const string MOUSEZ_CLASSNAME = "MouseZ";

		// Token: 0x04001CC1 RID: 7361
		public const string MOUSEZ_TITLE = "Magellan MSWHEEL";

		// Token: 0x04001CC2 RID: 7362
		public const int CHILDID_SELF = 0;

		// Token: 0x04001CC3 RID: 7363
		public const int OBJID_QUERYCLASSNAMEIDX = -12;

		// Token: 0x04001CC4 RID: 7364
		public const int OBJID_CLIENT = -4;

		// Token: 0x04001CC5 RID: 7365
		public const int OBJID_WINDOW = 0;

		// Token: 0x04001CC6 RID: 7366
		public const int UiaRootObjectId = -25;

		// Token: 0x04001CC7 RID: 7367
		public const int UiaAppendRuntimeId = 3;

		// Token: 0x04001CC8 RID: 7368
		public const string uuid_IAccessible = "{618736E0-3C3D-11CF-810C-00AA00389B71}";

		// Token: 0x04001CC9 RID: 7369
		public const string uuid_IEnumVariant = "{00020404-0000-0000-C000-000000000046}";

		// Token: 0x04001CCA RID: 7370
		public const int HH_FTS_DEFAULT_PROXIMITY = -1;

		// Token: 0x04001CCB RID: 7371
		public const int HICF_OTHER = 0;

		// Token: 0x04001CCC RID: 7372
		public const int HICF_MOUSE = 1;

		// Token: 0x04001CCD RID: 7373
		public const int HICF_ARROWKEYS = 2;

		// Token: 0x04001CCE RID: 7374
		public const int HICF_ACCELERATOR = 4;

		// Token: 0x04001CCF RID: 7375
		public const int HICF_DUPACCEL = 8;

		// Token: 0x04001CD0 RID: 7376
		public const int HICF_ENTERING = 16;

		// Token: 0x04001CD1 RID: 7377
		public const int HICF_LEAVING = 32;

		// Token: 0x04001CD2 RID: 7378
		public const int HICF_RESELECT = 64;

		// Token: 0x04001CD3 RID: 7379
		public const int HICF_LMOUSE = 128;

		// Token: 0x04001CD4 RID: 7380
		public const int HICF_TOGGLEDROPDOWN = 256;

		// Token: 0x04001CD5 RID: 7381
		public const int DPI_AWARENESS_CONTEXT_UNAWARE = -1;

		// Token: 0x04001CD6 RID: 7382
		public const int DPI_AWARENESS_CONTEXT_SYSTEM_AWARE = -2;

		// Token: 0x04001CD7 RID: 7383
		public const int DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE = -3;

		// Token: 0x04001CD8 RID: 7384
		public const int DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2 = -4;

		// Token: 0x04001CD9 RID: 7385
		public const int STAP_ALLOW_NONCLIENT = 1;

		// Token: 0x04001CDA RID: 7386
		public const int STAP_ALLOW_CONTROLS = 2;

		// Token: 0x04001CDB RID: 7387
		public const int STAP_ALLOW_WEBCONTENT = 4;

		// Token: 0x04001CDC RID: 7388
		public const int PS_NULL = 5;

		// Token: 0x04001CDD RID: 7389
		public const int PS_INSIDEFRAME = 6;

		// Token: 0x04001CDE RID: 7390
		public const int PS_GEOMETRIC = 65536;

		// Token: 0x04001CDF RID: 7391
		public const int PS_ENDCAP_SQUARE = 256;

		// Token: 0x04001CE0 RID: 7392
		public const int NULL_BRUSH = 5;

		// Token: 0x04001CE1 RID: 7393
		public const int MM_HIMETRIC = 3;

		// Token: 0x04001CE2 RID: 7394
		public const uint STILL_ACTIVE = 259U;

		// Token: 0x04001CE3 RID: 7395
		internal const int UIA_InvokePatternId = 10000;

		// Token: 0x04001CE4 RID: 7396
		internal const int UIA_SelectionPatternId = 10001;

		// Token: 0x04001CE5 RID: 7397
		internal const int UIA_ValuePatternId = 10002;

		// Token: 0x04001CE6 RID: 7398
		internal const int UIA_RangeValuePatternId = 10003;

		// Token: 0x04001CE7 RID: 7399
		internal const int UIA_ScrollPatternId = 10004;

		// Token: 0x04001CE8 RID: 7400
		internal const int UIA_ExpandCollapsePatternId = 10005;

		// Token: 0x04001CE9 RID: 7401
		internal const int UIA_GridPatternId = 10006;

		// Token: 0x04001CEA RID: 7402
		internal const int UIA_GridItemPatternId = 10007;

		// Token: 0x04001CEB RID: 7403
		internal const int UIA_MultipleViewPatternId = 10008;

		// Token: 0x04001CEC RID: 7404
		internal const int UIA_WindowPatternId = 10009;

		// Token: 0x04001CED RID: 7405
		internal const int UIA_SelectionItemPatternId = 10010;

		// Token: 0x04001CEE RID: 7406
		internal const int UIA_DockPatternId = 10011;

		// Token: 0x04001CEF RID: 7407
		internal const int UIA_TablePatternId = 10012;

		// Token: 0x04001CF0 RID: 7408
		internal const int UIA_TableItemPatternId = 10013;

		// Token: 0x04001CF1 RID: 7409
		internal const int UIA_TextPatternId = 10014;

		// Token: 0x04001CF2 RID: 7410
		internal const int UIA_TogglePatternId = 10015;

		// Token: 0x04001CF3 RID: 7411
		internal const int UIA_TransformPatternId = 10016;

		// Token: 0x04001CF4 RID: 7412
		internal const int UIA_ScrollItemPatternId = 10017;

		// Token: 0x04001CF5 RID: 7413
		internal const int UIA_LegacyIAccessiblePatternId = 10018;

		// Token: 0x04001CF6 RID: 7414
		internal const int UIA_ItemContainerPatternId = 10019;

		// Token: 0x04001CF7 RID: 7415
		internal const int UIA_VirtualizedItemPatternId = 10020;

		// Token: 0x04001CF8 RID: 7416
		internal const int UIA_SynchronizedInputPatternId = 10021;

		// Token: 0x04001CF9 RID: 7417
		internal const int UIA_ObjectModelPatternId = 10022;

		// Token: 0x04001CFA RID: 7418
		internal const int UIA_AnnotationPatternId = 10023;

		// Token: 0x04001CFB RID: 7419
		internal const int UIA_TextPattern2Id = 10024;

		// Token: 0x04001CFC RID: 7420
		internal const int UIA_StylesPatternId = 10025;

		// Token: 0x04001CFD RID: 7421
		internal const int UIA_SpreadsheetPatternId = 10026;

		// Token: 0x04001CFE RID: 7422
		internal const int UIA_SpreadsheetItemPatternId = 10027;

		// Token: 0x04001CFF RID: 7423
		internal const int UIA_TransformPattern2Id = 10028;

		// Token: 0x04001D00 RID: 7424
		internal const int UIA_TextChildPatternId = 10029;

		// Token: 0x04001D01 RID: 7425
		internal const int UIA_DragPatternId = 10030;

		// Token: 0x04001D02 RID: 7426
		internal const int UIA_DropTargetPatternId = 10031;

		// Token: 0x04001D03 RID: 7427
		internal const int UIA_TextEditPatternId = 10032;

		// Token: 0x04001D04 RID: 7428
		internal const int UIA_CustomNavigationPatternId = 10033;

		// Token: 0x04001D05 RID: 7429
		internal const int UIA_ToolTipOpenedEventId = 20000;

		// Token: 0x04001D06 RID: 7430
		internal const int UIA_ToolTipClosedEventId = 20001;

		// Token: 0x04001D07 RID: 7431
		internal const int UIA_StructureChangedEventId = 20002;

		// Token: 0x04001D08 RID: 7432
		internal const int UIA_MenuOpenedEventId = 20003;

		// Token: 0x04001D09 RID: 7433
		internal const int UIA_AutomationPropertyChangedEventId = 20004;

		// Token: 0x04001D0A RID: 7434
		internal const int UIA_AutomationFocusChangedEventId = 20005;

		// Token: 0x04001D0B RID: 7435
		internal const int UIA_AsyncContentLoadedEventId = 20006;

		// Token: 0x04001D0C RID: 7436
		internal const int UIA_MenuClosedEventId = 20007;

		// Token: 0x04001D0D RID: 7437
		internal const int UIA_LayoutInvalidatedEventId = 20008;

		// Token: 0x04001D0E RID: 7438
		internal const int UIA_Invoke_InvokedEventId = 20009;

		// Token: 0x04001D0F RID: 7439
		internal const int UIA_SelectionItem_ElementAddedToSelectionEventId = 20010;

		// Token: 0x04001D10 RID: 7440
		internal const int UIA_SelectionItem_ElementRemovedFromSelectionEventId = 20011;

		// Token: 0x04001D11 RID: 7441
		internal const int UIA_SelectionItem_ElementSelectedEventId = 20012;

		// Token: 0x04001D12 RID: 7442
		internal const int UIA_Selection_InvalidatedEventId = 20013;

		// Token: 0x04001D13 RID: 7443
		internal const int UIA_Text_TextSelectionChangedEventId = 20014;

		// Token: 0x04001D14 RID: 7444
		internal const int UIA_Text_TextChangedEventId = 20015;

		// Token: 0x04001D15 RID: 7445
		internal const int UIA_Window_WindowOpenedEventId = 20016;

		// Token: 0x04001D16 RID: 7446
		internal const int UIA_Window_WindowClosedEventId = 20017;

		// Token: 0x04001D17 RID: 7447
		internal const int UIA_MenuModeStartEventId = 20018;

		// Token: 0x04001D18 RID: 7448
		internal const int UIA_MenuModeEndEventId = 20019;

		// Token: 0x04001D19 RID: 7449
		internal const int UIA_InputReachedTargetEventId = 20020;

		// Token: 0x04001D1A RID: 7450
		internal const int UIA_InputReachedOtherElementEventId = 20021;

		// Token: 0x04001D1B RID: 7451
		internal const int UIA_InputDiscardedEventId = 20022;

		// Token: 0x04001D1C RID: 7452
		internal const int UIA_SystemAlertEventId = 20023;

		// Token: 0x04001D1D RID: 7453
		internal const int UIA_LiveRegionChangedEventId = 20024;

		// Token: 0x04001D1E RID: 7454
		internal const int UIA_HostedFragmentRootsInvalidatedEventId = 20025;

		// Token: 0x04001D1F RID: 7455
		internal const int UIA_Drag_DragStartEventId = 20026;

		// Token: 0x04001D20 RID: 7456
		internal const int UIA_Drag_DragCancelEventId = 20027;

		// Token: 0x04001D21 RID: 7457
		internal const int UIA_Drag_DragCompleteEventId = 20028;

		// Token: 0x04001D22 RID: 7458
		internal const int UIA_DropTarget_DragEnterEventId = 20029;

		// Token: 0x04001D23 RID: 7459
		internal const int UIA_DropTarget_DragLeaveEventId = 20030;

		// Token: 0x04001D24 RID: 7460
		internal const int UIA_DropTarget_DroppedEventId = 20031;

		// Token: 0x04001D25 RID: 7461
		internal const int UIA_TextEdit_TextChangedEventId = 20032;

		// Token: 0x04001D26 RID: 7462
		internal const int UIA_TextEdit_ConversionTargetChangedEventId = 20033;

		// Token: 0x04001D27 RID: 7463
		internal const int UIA_ChangesEventId = 20034;

		// Token: 0x04001D28 RID: 7464
		internal const int UIA_RuntimeIdPropertyId = 30000;

		// Token: 0x04001D29 RID: 7465
		internal const int UIA_BoundingRectanglePropertyId = 30001;

		// Token: 0x04001D2A RID: 7466
		internal const int UIA_ProcessIdPropertyId = 30002;

		// Token: 0x04001D2B RID: 7467
		internal const int UIA_ControlTypePropertyId = 30003;

		// Token: 0x04001D2C RID: 7468
		internal const int UIA_LocalizedControlTypePropertyId = 30004;

		// Token: 0x04001D2D RID: 7469
		internal const int UIA_NamePropertyId = 30005;

		// Token: 0x04001D2E RID: 7470
		internal const int UIA_AcceleratorKeyPropertyId = 30006;

		// Token: 0x04001D2F RID: 7471
		internal const int UIA_AccessKeyPropertyId = 30007;

		// Token: 0x04001D30 RID: 7472
		internal const int UIA_HasKeyboardFocusPropertyId = 30008;

		// Token: 0x04001D31 RID: 7473
		internal const int UIA_IsKeyboardFocusablePropertyId = 30009;

		// Token: 0x04001D32 RID: 7474
		internal const int UIA_IsEnabledPropertyId = 30010;

		// Token: 0x04001D33 RID: 7475
		internal const int UIA_AutomationIdPropertyId = 30011;

		// Token: 0x04001D34 RID: 7476
		internal const int UIA_ClassNamePropertyId = 30012;

		// Token: 0x04001D35 RID: 7477
		internal const int UIA_HelpTextPropertyId = 30013;

		// Token: 0x04001D36 RID: 7478
		internal const int UIA_ClickablePointPropertyId = 30014;

		// Token: 0x04001D37 RID: 7479
		internal const int UIA_CulturePropertyId = 30015;

		// Token: 0x04001D38 RID: 7480
		internal const int UIA_IsControlElementPropertyId = 30016;

		// Token: 0x04001D39 RID: 7481
		internal const int UIA_IsContentElementPropertyId = 30017;

		// Token: 0x04001D3A RID: 7482
		internal const int UIA_LabeledByPropertyId = 30018;

		// Token: 0x04001D3B RID: 7483
		internal const int UIA_IsPasswordPropertyId = 30019;

		// Token: 0x04001D3C RID: 7484
		internal const int UIA_NativeWindowHandlePropertyId = 30020;

		// Token: 0x04001D3D RID: 7485
		internal const int UIA_ItemTypePropertyId = 30021;

		// Token: 0x04001D3E RID: 7486
		internal const int UIA_IsOffscreenPropertyId = 30022;

		// Token: 0x04001D3F RID: 7487
		internal const int UIA_OrientationPropertyId = 30023;

		// Token: 0x04001D40 RID: 7488
		internal const int UIA_FrameworkIdPropertyId = 30024;

		// Token: 0x04001D41 RID: 7489
		internal const int UIA_IsRequiredForFormPropertyId = 30025;

		// Token: 0x04001D42 RID: 7490
		internal const int UIA_ItemStatusPropertyId = 30026;

		// Token: 0x04001D43 RID: 7491
		internal const int UIA_IsDockPatternAvailablePropertyId = 30027;

		// Token: 0x04001D44 RID: 7492
		internal const int UIA_IsExpandCollapsePatternAvailablePropertyId = 30028;

		// Token: 0x04001D45 RID: 7493
		internal const int UIA_IsGridItemPatternAvailablePropertyId = 30029;

		// Token: 0x04001D46 RID: 7494
		internal const int UIA_IsGridPatternAvailablePropertyId = 30030;

		// Token: 0x04001D47 RID: 7495
		internal const int UIA_IsInvokePatternAvailablePropertyId = 30031;

		// Token: 0x04001D48 RID: 7496
		internal const int UIA_IsMultipleViewPatternAvailablePropertyId = 30032;

		// Token: 0x04001D49 RID: 7497
		internal const int UIA_IsRangeValuePatternAvailablePropertyId = 30033;

		// Token: 0x04001D4A RID: 7498
		internal const int UIA_IsScrollPatternAvailablePropertyId = 30034;

		// Token: 0x04001D4B RID: 7499
		internal const int UIA_IsScrollItemPatternAvailablePropertyId = 30035;

		// Token: 0x04001D4C RID: 7500
		internal const int UIA_IsSelectionItemPatternAvailablePropertyId = 30036;

		// Token: 0x04001D4D RID: 7501
		internal const int UIA_IsSelectionPatternAvailablePropertyId = 30037;

		// Token: 0x04001D4E RID: 7502
		internal const int UIA_IsTablePatternAvailablePropertyId = 30038;

		// Token: 0x04001D4F RID: 7503
		internal const int UIA_IsTableItemPatternAvailablePropertyId = 30039;

		// Token: 0x04001D50 RID: 7504
		internal const int UIA_IsTextPatternAvailablePropertyId = 30040;

		// Token: 0x04001D51 RID: 7505
		internal const int UIA_IsTogglePatternAvailablePropertyId = 30041;

		// Token: 0x04001D52 RID: 7506
		internal const int UIA_IsTransformPatternAvailablePropertyId = 30042;

		// Token: 0x04001D53 RID: 7507
		internal const int UIA_IsValuePatternAvailablePropertyId = 30043;

		// Token: 0x04001D54 RID: 7508
		internal const int UIA_IsWindowPatternAvailablePropertyId = 30044;

		// Token: 0x04001D55 RID: 7509
		internal const int UIA_ValueValuePropertyId = 30045;

		// Token: 0x04001D56 RID: 7510
		internal const int UIA_ValueIsReadOnlyPropertyId = 30046;

		// Token: 0x04001D57 RID: 7511
		internal const int UIA_RangeValueValuePropertyId = 30047;

		// Token: 0x04001D58 RID: 7512
		internal const int UIA_RangeValueIsReadOnlyPropertyId = 30048;

		// Token: 0x04001D59 RID: 7513
		internal const int UIA_RangeValueMinimumPropertyId = 30049;

		// Token: 0x04001D5A RID: 7514
		internal const int UIA_RangeValueMaximumPropertyId = 30050;

		// Token: 0x04001D5B RID: 7515
		internal const int UIA_RangeValueLargeChangePropertyId = 30051;

		// Token: 0x04001D5C RID: 7516
		internal const int UIA_RangeValueSmallChangePropertyId = 30052;

		// Token: 0x04001D5D RID: 7517
		internal const int UIA_ScrollHorizontalScrollPercentPropertyId = 30053;

		// Token: 0x04001D5E RID: 7518
		internal const int UIA_ScrollHorizontalViewSizePropertyId = 30054;

		// Token: 0x04001D5F RID: 7519
		internal const int UIA_ScrollVerticalScrollPercentPropertyId = 30055;

		// Token: 0x04001D60 RID: 7520
		internal const int UIA_ScrollVerticalViewSizePropertyId = 30056;

		// Token: 0x04001D61 RID: 7521
		internal const int UIA_ScrollHorizontallyScrollablePropertyId = 30057;

		// Token: 0x04001D62 RID: 7522
		internal const int UIA_ScrollVerticallyScrollablePropertyId = 30058;

		// Token: 0x04001D63 RID: 7523
		internal const int UIA_SelectionSelectionPropertyId = 30059;

		// Token: 0x04001D64 RID: 7524
		internal const int UIA_SelectionCanSelectMultiplePropertyId = 30060;

		// Token: 0x04001D65 RID: 7525
		internal const int UIA_SelectionIsSelectionRequiredPropertyId = 30061;

		// Token: 0x04001D66 RID: 7526
		internal const int UIA_GridRowCountPropertyId = 30062;

		// Token: 0x04001D67 RID: 7527
		internal const int UIA_GridColumnCountPropertyId = 30063;

		// Token: 0x04001D68 RID: 7528
		internal const int UIA_GridItemRowPropertyId = 30064;

		// Token: 0x04001D69 RID: 7529
		internal const int UIA_GridItemColumnPropertyId = 30065;

		// Token: 0x04001D6A RID: 7530
		internal const int UIA_GridItemRowSpanPropertyId = 30066;

		// Token: 0x04001D6B RID: 7531
		internal const int UIA_GridItemColumnSpanPropertyId = 30067;

		// Token: 0x04001D6C RID: 7532
		internal const int UIA_GridItemContainingGridPropertyId = 30068;

		// Token: 0x04001D6D RID: 7533
		internal const int UIA_DockDockPositionPropertyId = 30069;

		// Token: 0x04001D6E RID: 7534
		internal const int UIA_ExpandCollapseExpandCollapseStatePropertyId = 30070;

		// Token: 0x04001D6F RID: 7535
		internal const int UIA_MultipleViewCurrentViewPropertyId = 30071;

		// Token: 0x04001D70 RID: 7536
		internal const int UIA_MultipleViewSupportedViewsPropertyId = 30072;

		// Token: 0x04001D71 RID: 7537
		internal const int UIA_WindowCanMaximizePropertyId = 30073;

		// Token: 0x04001D72 RID: 7538
		internal const int UIA_WindowCanMinimizePropertyId = 30074;

		// Token: 0x04001D73 RID: 7539
		internal const int UIA_WindowWindowVisualStatePropertyId = 30075;

		// Token: 0x04001D74 RID: 7540
		internal const int UIA_WindowWindowInteractionStatePropertyId = 30076;

		// Token: 0x04001D75 RID: 7541
		internal const int UIA_WindowIsModalPropertyId = 30077;

		// Token: 0x04001D76 RID: 7542
		internal const int UIA_WindowIsTopmostPropertyId = 30078;

		// Token: 0x04001D77 RID: 7543
		internal const int UIA_SelectionItemIsSelectedPropertyId = 30079;

		// Token: 0x04001D78 RID: 7544
		internal const int UIA_SelectionItemSelectionContainerPropertyId = 30080;

		// Token: 0x04001D79 RID: 7545
		internal const int UIA_TableRowHeadersPropertyId = 30081;

		// Token: 0x04001D7A RID: 7546
		internal const int UIA_TableColumnHeadersPropertyId = 30082;

		// Token: 0x04001D7B RID: 7547
		internal const int UIA_TableRowOrColumnMajorPropertyId = 30083;

		// Token: 0x04001D7C RID: 7548
		internal const int UIA_TableItemRowHeaderItemsPropertyId = 30084;

		// Token: 0x04001D7D RID: 7549
		internal const int UIA_TableItemColumnHeaderItemsPropertyId = 30085;

		// Token: 0x04001D7E RID: 7550
		internal const int UIA_ToggleToggleStatePropertyId = 30086;

		// Token: 0x04001D7F RID: 7551
		internal const int UIA_TransformCanMovePropertyId = 30087;

		// Token: 0x04001D80 RID: 7552
		internal const int UIA_TransformCanResizePropertyId = 30088;

		// Token: 0x04001D81 RID: 7553
		internal const int UIA_TransformCanRotatePropertyId = 30089;

		// Token: 0x04001D82 RID: 7554
		internal const int UIA_IsLegacyIAccessiblePatternAvailablePropertyId = 30090;

		// Token: 0x04001D83 RID: 7555
		internal const int UIA_LegacyIAccessibleChildIdPropertyId = 30091;

		// Token: 0x04001D84 RID: 7556
		internal const int UIA_LegacyIAccessibleNamePropertyId = 30092;

		// Token: 0x04001D85 RID: 7557
		internal const int UIA_LegacyIAccessibleValuePropertyId = 30093;

		// Token: 0x04001D86 RID: 7558
		internal const int UIA_LegacyIAccessibleDescriptionPropertyId = 30094;

		// Token: 0x04001D87 RID: 7559
		internal const int UIA_LegacyIAccessibleRolePropertyId = 30095;

		// Token: 0x04001D88 RID: 7560
		internal const int UIA_LegacyIAccessibleStatePropertyId = 30096;

		// Token: 0x04001D89 RID: 7561
		internal const int UIA_LegacyIAccessibleHelpPropertyId = 30097;

		// Token: 0x04001D8A RID: 7562
		internal const int UIA_LegacyIAccessibleKeyboardShortcutPropertyId = 30098;

		// Token: 0x04001D8B RID: 7563
		internal const int UIA_LegacyIAccessibleSelectionPropertyId = 30099;

		// Token: 0x04001D8C RID: 7564
		internal const int UIA_LegacyIAccessibleDefaultActionPropertyId = 30100;

		// Token: 0x04001D8D RID: 7565
		internal const int UIA_AriaRolePropertyId = 30101;

		// Token: 0x04001D8E RID: 7566
		internal const int UIA_AriaPropertiesPropertyId = 30102;

		// Token: 0x04001D8F RID: 7567
		internal const int UIA_IsDataValidForFormPropertyId = 30103;

		// Token: 0x04001D90 RID: 7568
		internal const int UIA_ControllerForPropertyId = 30104;

		// Token: 0x04001D91 RID: 7569
		internal const int UIA_DescribedByPropertyId = 30105;

		// Token: 0x04001D92 RID: 7570
		internal const int UIA_FlowsToPropertyId = 30106;

		// Token: 0x04001D93 RID: 7571
		internal const int UIA_ProviderDescriptionPropertyId = 30107;

		// Token: 0x04001D94 RID: 7572
		internal const int UIA_IsItemContainerPatternAvailablePropertyId = 30108;

		// Token: 0x04001D95 RID: 7573
		internal const int UIA_IsVirtualizedItemPatternAvailablePropertyId = 30109;

		// Token: 0x04001D96 RID: 7574
		internal const int UIA_IsSynchronizedInputPatternAvailablePropertyId = 30110;

		// Token: 0x04001D97 RID: 7575
		internal const int UIA_OptimizeForVisualContentPropertyId = 30111;

		// Token: 0x04001D98 RID: 7576
		internal const int UIA_IsObjectModelPatternAvailablePropertyId = 30112;

		// Token: 0x04001D99 RID: 7577
		internal const int UIA_AnnotationAnnotationTypeIdPropertyId = 30113;

		// Token: 0x04001D9A RID: 7578
		internal const int UIA_AnnotationAnnotationTypeNamePropertyId = 30114;

		// Token: 0x04001D9B RID: 7579
		internal const int UIA_AnnotationAuthorPropertyId = 30115;

		// Token: 0x04001D9C RID: 7580
		internal const int UIA_AnnotationDateTimePropertyId = 30116;

		// Token: 0x04001D9D RID: 7581
		internal const int UIA_AnnotationTargetPropertyId = 30117;

		// Token: 0x04001D9E RID: 7582
		internal const int UIA_IsAnnotationPatternAvailablePropertyId = 30118;

		// Token: 0x04001D9F RID: 7583
		internal const int UIA_IsTextPattern2AvailablePropertyId = 30119;

		// Token: 0x04001DA0 RID: 7584
		internal const int UIA_StylesStyleIdPropertyId = 30120;

		// Token: 0x04001DA1 RID: 7585
		internal const int UIA_StylesStyleNamePropertyId = 30121;

		// Token: 0x04001DA2 RID: 7586
		internal const int UIA_StylesFillColorPropertyId = 30122;

		// Token: 0x04001DA3 RID: 7587
		internal const int UIA_StylesFillPatternStylePropertyId = 30123;

		// Token: 0x04001DA4 RID: 7588
		internal const int UIA_StylesShapePropertyId = 30124;

		// Token: 0x04001DA5 RID: 7589
		internal const int UIA_StylesFillPatternColorPropertyId = 30125;

		// Token: 0x04001DA6 RID: 7590
		internal const int UIA_StylesExtendedPropertiesPropertyId = 30126;

		// Token: 0x04001DA7 RID: 7591
		internal const int UIA_IsStylesPatternAvailablePropertyId = 30127;

		// Token: 0x04001DA8 RID: 7592
		internal const int UIA_IsSpreadsheetPatternAvailablePropertyId = 30128;

		// Token: 0x04001DA9 RID: 7593
		internal const int UIA_SpreadsheetItemFormulaPropertyId = 30129;

		// Token: 0x04001DAA RID: 7594
		internal const int UIA_SpreadsheetItemAnnotationObjectsPropertyId = 30130;

		// Token: 0x04001DAB RID: 7595
		internal const int UIA_SpreadsheetItemAnnotationTypesPropertyId = 30131;

		// Token: 0x04001DAC RID: 7596
		internal const int UIA_IsSpreadsheetItemPatternAvailablePropertyId = 30132;

		// Token: 0x04001DAD RID: 7597
		internal const int UIA_Transform2CanZoomPropertyId = 30133;

		// Token: 0x04001DAE RID: 7598
		internal const int UIA_IsTransformPattern2AvailablePropertyId = 30134;

		// Token: 0x04001DAF RID: 7599
		internal const int UIA_LiveSettingPropertyId = 30135;

		// Token: 0x04001DB0 RID: 7600
		internal const int UIA_IsTextChildPatternAvailablePropertyId = 30136;

		// Token: 0x04001DB1 RID: 7601
		internal const int UIA_IsDragPatternAvailablePropertyId = 30137;

		// Token: 0x04001DB2 RID: 7602
		internal const int UIA_DragIsGrabbedPropertyId = 30138;

		// Token: 0x04001DB3 RID: 7603
		internal const int UIA_DragDropEffectPropertyId = 30139;

		// Token: 0x04001DB4 RID: 7604
		internal const int UIA_DragDropEffectsPropertyId = 30140;

		// Token: 0x04001DB5 RID: 7605
		internal const int UIA_IsDropTargetPatternAvailablePropertyId = 30141;

		// Token: 0x04001DB6 RID: 7606
		internal const int UIA_DropTargetDropTargetEffectPropertyId = 30142;

		// Token: 0x04001DB7 RID: 7607
		internal const int UIA_DropTargetDropTargetEffectsPropertyId = 30143;

		// Token: 0x04001DB8 RID: 7608
		internal const int UIA_DragGrabbedItemsPropertyId = 30144;

		// Token: 0x04001DB9 RID: 7609
		internal const int UIA_Transform2ZoomLevelPropertyId = 30145;

		// Token: 0x04001DBA RID: 7610
		internal const int UIA_Transform2ZoomMinimumPropertyId = 30146;

		// Token: 0x04001DBB RID: 7611
		internal const int UIA_Transform2ZoomMaximumPropertyId = 30147;

		// Token: 0x04001DBC RID: 7612
		internal const int UIA_FlowsFromPropertyId = 30148;

		// Token: 0x04001DBD RID: 7613
		internal const int UIA_IsTextEditPatternAvailablePropertyId = 30149;

		// Token: 0x04001DBE RID: 7614
		internal const int UIA_IsPeripheralPropertyId = 30150;

		// Token: 0x04001DBF RID: 7615
		internal const int UIA_IsCustomNavigationPatternAvailablePropertyId = 30151;

		// Token: 0x04001DC0 RID: 7616
		internal const int UIA_PositionInSetPropertyId = 30152;

		// Token: 0x04001DC1 RID: 7617
		internal const int UIA_SizeOfSetPropertyId = 30153;

		// Token: 0x04001DC2 RID: 7618
		internal const int UIA_LevelPropertyId = 30154;

		// Token: 0x04001DC3 RID: 7619
		internal const int UIA_AnnotationTypesPropertyId = 30155;

		// Token: 0x04001DC4 RID: 7620
		internal const int UIA_AnnotationObjectsPropertyId = 30156;

		// Token: 0x04001DC5 RID: 7621
		internal const int UIA_LandmarkTypePropertyId = 30157;

		// Token: 0x04001DC6 RID: 7622
		internal const int UIA_LocalizedLandmarkTypePropertyId = 30158;

		// Token: 0x04001DC7 RID: 7623
		internal const int UIA_FullDescriptionPropertyId = 30159;

		// Token: 0x04001DC8 RID: 7624
		internal const int UIA_FillColorPropertyId = 30160;

		// Token: 0x04001DC9 RID: 7625
		internal const int UIA_OutlineColorPropertyId = 30161;

		// Token: 0x04001DCA RID: 7626
		internal const int UIA_FillTypePropertyId = 30162;

		// Token: 0x04001DCB RID: 7627
		internal const int UIA_VisualEffectsPropertyId = 30163;

		// Token: 0x04001DCC RID: 7628
		internal const int UIA_OutlineThicknessPropertyId = 30164;

		// Token: 0x04001DCD RID: 7629
		internal const int UIA_CenterPointPropertyId = 30165;

		// Token: 0x04001DCE RID: 7630
		internal const int UIA_RotationPropertyId = 30166;

		// Token: 0x04001DCF RID: 7631
		internal const int UIA_SizePropertyId = 30167;

		// Token: 0x04001DD0 RID: 7632
		internal const int UIA_ButtonControlTypeId = 50000;

		// Token: 0x04001DD1 RID: 7633
		internal const int UIA_CalendarControlTypeId = 50001;

		// Token: 0x04001DD2 RID: 7634
		internal const int UIA_CheckBoxControlTypeId = 50002;

		// Token: 0x04001DD3 RID: 7635
		internal const int UIA_ComboBoxControlTypeId = 50003;

		// Token: 0x04001DD4 RID: 7636
		internal const int UIA_EditControlTypeId = 50004;

		// Token: 0x04001DD5 RID: 7637
		internal const int UIA_HyperlinkControlTypeId = 50005;

		// Token: 0x04001DD6 RID: 7638
		internal const int UIA_ImageControlTypeId = 50006;

		// Token: 0x04001DD7 RID: 7639
		internal const int UIA_ListItemControlTypeId = 50007;

		// Token: 0x04001DD8 RID: 7640
		internal const int UIA_ListControlTypeId = 50008;

		// Token: 0x04001DD9 RID: 7641
		internal const int UIA_MenuControlTypeId = 50009;

		// Token: 0x04001DDA RID: 7642
		internal const int UIA_MenuBarControlTypeId = 50010;

		// Token: 0x04001DDB RID: 7643
		internal const int UIA_MenuItemControlTypeId = 50011;

		// Token: 0x04001DDC RID: 7644
		internal const int UIA_ProgressBarControlTypeId = 50012;

		// Token: 0x04001DDD RID: 7645
		internal const int UIA_RadioButtonControlTypeId = 50013;

		// Token: 0x04001DDE RID: 7646
		internal const int UIA_ScrollBarControlTypeId = 50014;

		// Token: 0x04001DDF RID: 7647
		internal const int UIA_SliderControlTypeId = 50015;

		// Token: 0x04001DE0 RID: 7648
		internal const int UIA_SpinnerControlTypeId = 50016;

		// Token: 0x04001DE1 RID: 7649
		internal const int UIA_StatusBarControlTypeId = 50017;

		// Token: 0x04001DE2 RID: 7650
		internal const int UIA_TabControlTypeId = 50018;

		// Token: 0x04001DE3 RID: 7651
		internal const int UIA_TabItemControlTypeId = 50019;

		// Token: 0x04001DE4 RID: 7652
		internal const int UIA_TextControlTypeId = 50020;

		// Token: 0x04001DE5 RID: 7653
		internal const int UIA_ToolBarControlTypeId = 50021;

		// Token: 0x04001DE6 RID: 7654
		internal const int UIA_ToolTipControlTypeId = 50022;

		// Token: 0x04001DE7 RID: 7655
		internal const int UIA_TreeControlTypeId = 50023;

		// Token: 0x04001DE8 RID: 7656
		internal const int UIA_TreeItemControlTypeId = 50024;

		// Token: 0x04001DE9 RID: 7657
		internal const int UIA_CustomControlTypeId = 50025;

		// Token: 0x04001DEA RID: 7658
		internal const int UIA_GroupControlTypeId = 50026;

		// Token: 0x04001DEB RID: 7659
		internal const int UIA_ThumbControlTypeId = 50027;

		// Token: 0x04001DEC RID: 7660
		internal const int UIA_DataGridControlTypeId = 50028;

		// Token: 0x04001DED RID: 7661
		internal const int UIA_DataItemControlTypeId = 50029;

		// Token: 0x04001DEE RID: 7662
		internal const int UIA_DocumentControlTypeId = 50030;

		// Token: 0x04001DEF RID: 7663
		internal const int UIA_SplitButtonControlTypeId = 50031;

		// Token: 0x04001DF0 RID: 7664
		internal const int UIA_WindowControlTypeId = 50032;

		// Token: 0x04001DF1 RID: 7665
		internal const int UIA_PaneControlTypeId = 50033;

		// Token: 0x04001DF2 RID: 7666
		internal const int UIA_HeaderControlTypeId = 50034;

		// Token: 0x04001DF3 RID: 7667
		internal const int UIA_HeaderItemControlTypeId = 50035;

		// Token: 0x04001DF4 RID: 7668
		internal const int UIA_TableControlTypeId = 50036;

		// Token: 0x04001DF5 RID: 7669
		internal const int UIA_TitleBarControlTypeId = 50037;

		// Token: 0x04001DF6 RID: 7670
		internal const int UIA_SeparatorControlTypeId = 50038;

		// Token: 0x04001DF7 RID: 7671
		internal const int UIA_SemanticZoomControlTypeId = 50039;

		// Token: 0x04001DF8 RID: 7672
		internal const int UIA_AppBarControlTypeId = 50040;

		// Token: 0x04001DF9 RID: 7673
		internal const int LOAD_LIBRARY_SEARCH_SYSTEM32 = 2048;

		// Token: 0x020006EC RID: 1772
		public enum RegionFlags
		{
			// Token: 0x04003BA8 RID: 15272
			ERROR,
			// Token: 0x04003BA9 RID: 15273
			NULLREGION,
			// Token: 0x04003BAA RID: 15274
			SIMPLEREGION,
			// Token: 0x04003BAB RID: 15275
			COMPLEXREGION
		}

		// Token: 0x020006ED RID: 1773
		[CLSCompliant(false)]
		[StructLayout(LayoutKind.Sequential)]
		public class OLECMD
		{
			// Token: 0x04003BAC RID: 15276
			[MarshalAs(UnmanagedType.U4)]
			public int cmdID;

			// Token: 0x04003BAD RID: 15277
			[MarshalAs(UnmanagedType.U4)]
			public int cmdf;
		}

		// Token: 0x020006EE RID: 1774
		[ComVisible(true)]
		[Guid("B722BCCB-4E68-101B-A2BC-00AA00404770")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[CLSCompliant(false)]
		[ComImport]
		public interface IOleCommandTarget
		{
			// Token: 0x06006BD4 RID: 27604
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int QueryStatus(ref Guid pguidCmdGroup, int cCmds, [In] [Out] NativeMethods.OLECMD prgCmds, [In] [Out] IntPtr pCmdText);

			// Token: 0x06006BD5 RID: 27605
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int Exec(ref Guid pguidCmdGroup, int nCmdID, int nCmdexecopt, [MarshalAs(UnmanagedType.LPArray)] [In] object[] pvaIn, int pvaOut);
		}

		// Token: 0x020006EF RID: 1775
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public class FONTDESC
		{
			// Token: 0x04003BAE RID: 15278
			public int cbSizeOfStruct = Marshal.SizeOf(typeof(NativeMethods.FONTDESC));

			// Token: 0x04003BAF RID: 15279
			public string lpstrName;

			// Token: 0x04003BB0 RID: 15280
			public long cySize;

			// Token: 0x04003BB1 RID: 15281
			public short sWeight;

			// Token: 0x04003BB2 RID: 15282
			public short sCharset;

			// Token: 0x04003BB3 RID: 15283
			public bool fItalic;

			// Token: 0x04003BB4 RID: 15284
			public bool fUnderline;

			// Token: 0x04003BB5 RID: 15285
			public bool fStrikethrough;
		}

		// Token: 0x020006F0 RID: 1776
		[StructLayout(LayoutKind.Sequential)]
		public class PICTDESCbmp
		{
			// Token: 0x06006BD7 RID: 27607 RVA: 0x0018EE28 File Offset: 0x0018D028
			public PICTDESCbmp(Bitmap bitmap)
			{
				this.hbitmap = bitmap.GetHbitmap();
			}

			// Token: 0x04003BB6 RID: 15286
			internal int cbSizeOfStruct = Marshal.SizeOf(typeof(NativeMethods.PICTDESCbmp));

			// Token: 0x04003BB7 RID: 15287
			internal int picType = 1;

			// Token: 0x04003BB8 RID: 15288
			internal IntPtr hbitmap = IntPtr.Zero;

			// Token: 0x04003BB9 RID: 15289
			internal IntPtr hpalette = IntPtr.Zero;

			// Token: 0x04003BBA RID: 15290
			internal int unused;
		}

		// Token: 0x020006F1 RID: 1777
		[StructLayout(LayoutKind.Sequential)]
		public class PICTDESCicon
		{
			// Token: 0x06006BD8 RID: 27608 RVA: 0x0018EE7C File Offset: 0x0018D07C
			public PICTDESCicon(Icon icon)
			{
				this.hicon = SafeNativeMethods.CopyImage(new HandleRef(icon, icon.Handle), 1, icon.Size.Width, icon.Size.Height, 0);
			}

			// Token: 0x04003BBB RID: 15291
			internal int cbSizeOfStruct = Marshal.SizeOf(typeof(NativeMethods.PICTDESCicon));

			// Token: 0x04003BBC RID: 15292
			internal int picType = 3;

			// Token: 0x04003BBD RID: 15293
			internal IntPtr hicon = IntPtr.Zero;

			// Token: 0x04003BBE RID: 15294
			internal int unused1;

			// Token: 0x04003BBF RID: 15295
			internal int unused2;
		}

		// Token: 0x020006F2 RID: 1778
		[StructLayout(LayoutKind.Sequential)]
		public class PICTDESCemf
		{
			// Token: 0x06006BD9 RID: 27609 RVA: 0x0018EEEB File Offset: 0x0018D0EB
			public PICTDESCemf(Metafile metafile)
			{
			}

			// Token: 0x04003BC0 RID: 15296
			internal int cbSizeOfStruct = Marshal.SizeOf(typeof(NativeMethods.PICTDESCemf));

			// Token: 0x04003BC1 RID: 15297
			internal int picType = 4;

			// Token: 0x04003BC2 RID: 15298
			internal IntPtr hemf = IntPtr.Zero;

			// Token: 0x04003BC3 RID: 15299
			internal int unused1;

			// Token: 0x04003BC4 RID: 15300
			internal int unused2;
		}

		// Token: 0x020006F3 RID: 1779
		[StructLayout(LayoutKind.Sequential)]
		public class USEROBJECTFLAGS
		{
			// Token: 0x04003BC5 RID: 15301
			public int fInherit;

			// Token: 0x04003BC6 RID: 15302
			public int fReserved;

			// Token: 0x04003BC7 RID: 15303
			public int dwFlags;
		}

		// Token: 0x020006F4 RID: 1780
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		internal class SYSTEMTIMEARRAY
		{
			// Token: 0x04003BC8 RID: 15304
			public short wYear1;

			// Token: 0x04003BC9 RID: 15305
			public short wMonth1;

			// Token: 0x04003BCA RID: 15306
			public short wDayOfWeek1;

			// Token: 0x04003BCB RID: 15307
			public short wDay1;

			// Token: 0x04003BCC RID: 15308
			public short wHour1;

			// Token: 0x04003BCD RID: 15309
			public short wMinute1;

			// Token: 0x04003BCE RID: 15310
			public short wSecond1;

			// Token: 0x04003BCF RID: 15311
			public short wMilliseconds1;

			// Token: 0x04003BD0 RID: 15312
			public short wYear2;

			// Token: 0x04003BD1 RID: 15313
			public short wMonth2;

			// Token: 0x04003BD2 RID: 15314
			public short wDayOfWeek2;

			// Token: 0x04003BD3 RID: 15315
			public short wDay2;

			// Token: 0x04003BD4 RID: 15316
			public short wHour2;

			// Token: 0x04003BD5 RID: 15317
			public short wMinute2;

			// Token: 0x04003BD6 RID: 15318
			public short wSecond2;

			// Token: 0x04003BD7 RID: 15319
			public short wMilliseconds2;
		}

		// Token: 0x020006F5 RID: 1781
		// (Invoke) Token: 0x06006BDD RID: 27613
		public delegate bool EnumChildrenCallback(IntPtr hwnd, IntPtr lParam);

		// Token: 0x020006F6 RID: 1782
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class HH_AKLINK
		{
			// Token: 0x04003BD8 RID: 15320
			internal int cbStruct = Marshal.SizeOf(typeof(NativeMethods.HH_AKLINK));

			// Token: 0x04003BD9 RID: 15321
			internal bool fReserved;

			// Token: 0x04003BDA RID: 15322
			internal string pszKeywords;

			// Token: 0x04003BDB RID: 15323
			internal string pszUrl;

			// Token: 0x04003BDC RID: 15324
			internal string pszMsgText;

			// Token: 0x04003BDD RID: 15325
			internal string pszMsgTitle;

			// Token: 0x04003BDE RID: 15326
			internal string pszWindow;

			// Token: 0x04003BDF RID: 15327
			internal bool fIndexOnFail;
		}

		// Token: 0x020006F7 RID: 1783
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class HH_POPUP
		{
			// Token: 0x04003BE0 RID: 15328
			internal int cbStruct = Marshal.SizeOf(typeof(NativeMethods.HH_POPUP));

			// Token: 0x04003BE1 RID: 15329
			internal IntPtr hinst = IntPtr.Zero;

			// Token: 0x04003BE2 RID: 15330
			internal int idString;

			// Token: 0x04003BE3 RID: 15331
			internal IntPtr pszText;

			// Token: 0x04003BE4 RID: 15332
			internal NativeMethods.POINT pt;

			// Token: 0x04003BE5 RID: 15333
			internal int clrForeground = -1;

			// Token: 0x04003BE6 RID: 15334
			internal int clrBackground = -1;

			// Token: 0x04003BE7 RID: 15335
			internal NativeMethods.RECT rcMargins = NativeMethods.RECT.FromXYWH(-1, -1, -1, -1);

			// Token: 0x04003BE8 RID: 15336
			internal string pszFont;
		}

		// Token: 0x020006F8 RID: 1784
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class HH_FTS_QUERY
		{
			// Token: 0x04003BE9 RID: 15337
			internal int cbStruct = Marshal.SizeOf(typeof(NativeMethods.HH_FTS_QUERY));

			// Token: 0x04003BEA RID: 15338
			internal bool fUniCodeStrings;

			// Token: 0x04003BEB RID: 15339
			[MarshalAs(UnmanagedType.LPStr)]
			internal string pszSearchQuery;

			// Token: 0x04003BEC RID: 15340
			internal int iProximity = -1;

			// Token: 0x04003BED RID: 15341
			internal bool fStemmedSearch;

			// Token: 0x04003BEE RID: 15342
			internal bool fTitleOnly;

			// Token: 0x04003BEF RID: 15343
			internal bool fExecute = true;

			// Token: 0x04003BF0 RID: 15344
			[MarshalAs(UnmanagedType.LPStr)]
			internal string pszWindow;
		}

		// Token: 0x020006F9 RID: 1785
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
		public class MONITORINFOEX
		{
			// Token: 0x04003BF1 RID: 15345
			internal int cbSize = Marshal.SizeOf(typeof(NativeMethods.MONITORINFOEX));

			// Token: 0x04003BF2 RID: 15346
			internal NativeMethods.RECT rcMonitor;

			// Token: 0x04003BF3 RID: 15347
			internal NativeMethods.RECT rcWork;

			// Token: 0x04003BF4 RID: 15348
			internal int dwFlags;

			// Token: 0x04003BF5 RID: 15349
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
			internal char[] szDevice = new char[32];
		}

		// Token: 0x020006FA RID: 1786
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
		public class MONITORINFO
		{
			// Token: 0x04003BF6 RID: 15350
			internal int cbSize = Marshal.SizeOf(typeof(NativeMethods.MONITORINFO));

			// Token: 0x04003BF7 RID: 15351
			internal NativeMethods.RECT rcMonitor;

			// Token: 0x04003BF8 RID: 15352
			internal NativeMethods.RECT rcWork;

			// Token: 0x04003BF9 RID: 15353
			internal int dwFlags;
		}

		// Token: 0x020006FB RID: 1787
		// (Invoke) Token: 0x06006BE6 RID: 27622
		public delegate int EditStreamCallback(IntPtr dwCookie, IntPtr buf, int cb, out int transferred);

		// Token: 0x020006FC RID: 1788
		[StructLayout(LayoutKind.Sequential)]
		public class EDITSTREAM
		{
			// Token: 0x04003BFA RID: 15354
			public IntPtr dwCookie = IntPtr.Zero;

			// Token: 0x04003BFB RID: 15355
			public int dwError;

			// Token: 0x04003BFC RID: 15356
			public NativeMethods.EditStreamCallback pfnCallback;
		}

		// Token: 0x020006FD RID: 1789
		[StructLayout(LayoutKind.Sequential)]
		public class EDITSTREAM64
		{
			// Token: 0x04003BFD RID: 15357
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
			public byte[] contents = new byte[20];
		}

		// Token: 0x020006FE RID: 1790
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct DEVMODE
		{
			// Token: 0x04003BFE RID: 15358
			private const int CCHDEVICENAME = 32;

			// Token: 0x04003BFF RID: 15359
			private const int CCHFORMNAME = 32;

			// Token: 0x04003C00 RID: 15360
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string dmDeviceName;

			// Token: 0x04003C01 RID: 15361
			public short dmSpecVersion;

			// Token: 0x04003C02 RID: 15362
			public short dmDriverVersion;

			// Token: 0x04003C03 RID: 15363
			public short dmSize;

			// Token: 0x04003C04 RID: 15364
			public short dmDriverExtra;

			// Token: 0x04003C05 RID: 15365
			public int dmFields;

			// Token: 0x04003C06 RID: 15366
			public int dmPositionX;

			// Token: 0x04003C07 RID: 15367
			public int dmPositionY;

			// Token: 0x04003C08 RID: 15368
			public ScreenOrientation dmDisplayOrientation;

			// Token: 0x04003C09 RID: 15369
			public int dmDisplayFixedOutput;

			// Token: 0x04003C0A RID: 15370
			public short dmColor;

			// Token: 0x04003C0B RID: 15371
			public short dmDuplex;

			// Token: 0x04003C0C RID: 15372
			public short dmYResolution;

			// Token: 0x04003C0D RID: 15373
			public short dmTTOption;

			// Token: 0x04003C0E RID: 15374
			public short dmCollate;

			// Token: 0x04003C0F RID: 15375
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string dmFormName;

			// Token: 0x04003C10 RID: 15376
			public short dmLogPixels;

			// Token: 0x04003C11 RID: 15377
			public int dmBitsPerPel;

			// Token: 0x04003C12 RID: 15378
			public int dmPelsWidth;

			// Token: 0x04003C13 RID: 15379
			public int dmPelsHeight;

			// Token: 0x04003C14 RID: 15380
			public int dmDisplayFlags;

			// Token: 0x04003C15 RID: 15381
			public int dmDisplayFrequency;

			// Token: 0x04003C16 RID: 15382
			public int dmICMMethod;

			// Token: 0x04003C17 RID: 15383
			public int dmICMIntent;

			// Token: 0x04003C18 RID: 15384
			public int dmMediaType;

			// Token: 0x04003C19 RID: 15385
			public int dmDitherType;

			// Token: 0x04003C1A RID: 15386
			public int dmReserved1;

			// Token: 0x04003C1B RID: 15387
			public int dmReserved2;

			// Token: 0x04003C1C RID: 15388
			public int dmPanningWidth;

			// Token: 0x04003C1D RID: 15389
			public int dmPanningHeight;
		}

		// Token: 0x020006FF RID: 1791
		[Guid("0FF510A3-5FA5-49F1-8CCC-190D71083F3E")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IVsPerPropertyBrowsing
		{
			// Token: 0x06006BEB RID: 27627
			[PreserveSig]
			int HideProperty(int dispid, ref bool pfHide);

			// Token: 0x06006BEC RID: 27628
			[PreserveSig]
			int DisplayChildProperties(int dispid, ref bool pfDisplay);

			// Token: 0x06006BED RID: 27629
			[PreserveSig]
			int GetLocalizedPropertyInfo(int dispid, int localeID, [MarshalAs(UnmanagedType.LPArray)] [Out] string[] pbstrLocalizedName, [MarshalAs(UnmanagedType.LPArray)] [Out] string[] pbstrLocalizeDescription);

			// Token: 0x06006BEE RID: 27630
			[PreserveSig]
			int HasDefaultValue(int dispid, ref bool fDefault);

			// Token: 0x06006BEF RID: 27631
			[PreserveSig]
			int IsPropertyReadOnly(int dispid, ref bool fReadOnly);

			// Token: 0x06006BF0 RID: 27632
			[PreserveSig]
			int GetClassName([In] [Out] ref string pbstrClassName);

			// Token: 0x06006BF1 RID: 27633
			[PreserveSig]
			int CanResetPropertyValue(int dispid, [In] [Out] ref bool pfCanReset);

			// Token: 0x06006BF2 RID: 27634
			[PreserveSig]
			int ResetPropertyValue(int dispid);
		}

		// Token: 0x02000700 RID: 1792
		[Guid("7494683C-37A0-11d2-A273-00C04F8EF4FF")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IManagedPerPropertyBrowsing
		{
			// Token: 0x06006BF3 RID: 27635
			[PreserveSig]
			int GetPropertyAttributes(int dispid, ref int pcAttributes, ref IntPtr pbstrAttrNames, ref IntPtr pvariantInitValues);
		}

		// Token: 0x02000701 RID: 1793
		[Guid("33C0C1D8-33CF-11d3-BFF2-00C04F990235")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IProvidePropertyBuilder
		{
			// Token: 0x06006BF4 RID: 27636
			[PreserveSig]
			int MapPropertyToBuilder(int dispid, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] int[] pdwCtlBldType, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] string[] pbstrGuidBldr, [MarshalAs(UnmanagedType.Bool)] [In] [Out] ref bool builderAvailable);

			// Token: 0x06006BF5 RID: 27637
			[PreserveSig]
			int ExecuteBuilder(int dispid, [MarshalAs(UnmanagedType.BStr)] [In] string bstrGuidBldr, [MarshalAs(UnmanagedType.Interface)] [In] object pdispApp, HandleRef hwndBldrOwner, [MarshalAs(UnmanagedType.Struct)] [In] [Out] ref object pvarValue, [MarshalAs(UnmanagedType.Bool)] [In] [Out] ref bool actionCommitted);
		}

		// Token: 0x02000702 RID: 1794
		[StructLayout(LayoutKind.Sequential)]
		public class INITCOMMONCONTROLSEX
		{
			// Token: 0x04003C1E RID: 15390
			public int dwSize = 8;

			// Token: 0x04003C1F RID: 15391
			public int dwICC;
		}

		// Token: 0x02000703 RID: 1795
		[StructLayout(LayoutKind.Sequential)]
		public class IMAGELISTDRAWPARAMS
		{
			// Token: 0x04003C20 RID: 15392
			public int cbSize = Marshal.SizeOf(typeof(NativeMethods.IMAGELISTDRAWPARAMS));

			// Token: 0x04003C21 RID: 15393
			public IntPtr himl = IntPtr.Zero;

			// Token: 0x04003C22 RID: 15394
			public int i;

			// Token: 0x04003C23 RID: 15395
			public IntPtr hdcDst = IntPtr.Zero;

			// Token: 0x04003C24 RID: 15396
			public int x;

			// Token: 0x04003C25 RID: 15397
			public int y;

			// Token: 0x04003C26 RID: 15398
			public int cx;

			// Token: 0x04003C27 RID: 15399
			public int cy;

			// Token: 0x04003C28 RID: 15400
			public int xBitmap;

			// Token: 0x04003C29 RID: 15401
			public int yBitmap;

			// Token: 0x04003C2A RID: 15402
			public int rgbBk;

			// Token: 0x04003C2B RID: 15403
			public int rgbFg;

			// Token: 0x04003C2C RID: 15404
			public int fStyle;

			// Token: 0x04003C2D RID: 15405
			public int dwRop;

			// Token: 0x04003C2E RID: 15406
			public int fState;

			// Token: 0x04003C2F RID: 15407
			public int Frame;

			// Token: 0x04003C30 RID: 15408
			public int crEffect;
		}

		// Token: 0x02000704 RID: 1796
		[StructLayout(LayoutKind.Sequential)]
		public class IMAGEINFO
		{
			// Token: 0x04003C31 RID: 15409
			public IntPtr hbmImage = IntPtr.Zero;

			// Token: 0x04003C32 RID: 15410
			public IntPtr hbmMask = IntPtr.Zero;

			// Token: 0x04003C33 RID: 15411
			public int Unused1;

			// Token: 0x04003C34 RID: 15412
			public int Unused2;

			// Token: 0x04003C35 RID: 15413
			public int rcImage_left;

			// Token: 0x04003C36 RID: 15414
			public int rcImage_top;

			// Token: 0x04003C37 RID: 15415
			public int rcImage_right;

			// Token: 0x04003C38 RID: 15416
			public int rcImage_bottom;
		}

		// Token: 0x02000705 RID: 1797
		[StructLayout(LayoutKind.Sequential)]
		public class TRACKMOUSEEVENT
		{
			// Token: 0x04003C39 RID: 15417
			public int cbSize = Marshal.SizeOf(typeof(NativeMethods.TRACKMOUSEEVENT));

			// Token: 0x04003C3A RID: 15418
			public int dwFlags;

			// Token: 0x04003C3B RID: 15419
			public IntPtr hwndTrack;

			// Token: 0x04003C3C RID: 15420
			public int dwHoverTime = 100;
		}

		// Token: 0x02000706 RID: 1798
		[StructLayout(LayoutKind.Sequential)]
		public class POINT
		{
			// Token: 0x06006BFA RID: 27642 RVA: 0x00002843 File Offset: 0x00000A43
			public POINT()
			{
			}

			// Token: 0x06006BFB RID: 27643 RVA: 0x0018F0A7 File Offset: 0x0018D2A7
			public POINT(int x, int y)
			{
				this.x = x;
				this.y = y;
			}

			// Token: 0x04003C3D RID: 15421
			public int x;

			// Token: 0x04003C3E RID: 15422
			public int y;
		}

		// Token: 0x02000707 RID: 1799
		public struct POINTSTRUCT
		{
			// Token: 0x06006BFC RID: 27644 RVA: 0x0018F0BD File Offset: 0x0018D2BD
			public POINTSTRUCT(int x, int y)
			{
				this.x = x;
				this.y = y;
			}

			// Token: 0x04003C3F RID: 15423
			public int x;

			// Token: 0x04003C40 RID: 15424
			public int y;
		}

		// Token: 0x02000708 RID: 1800
		// (Invoke) Token: 0x06006BFE RID: 27646
		public delegate IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x02000709 RID: 1801
		public struct RECT
		{
			// Token: 0x06006C01 RID: 27649 RVA: 0x0018F0CD File Offset: 0x0018D2CD
			public RECT(int left, int top, int right, int bottom)
			{
				this.left = left;
				this.top = top;
				this.right = right;
				this.bottom = bottom;
			}

			// Token: 0x06006C02 RID: 27650 RVA: 0x0018F0EC File Offset: 0x0018D2EC
			public RECT(Rectangle r)
			{
				this.left = r.Left;
				this.top = r.Top;
				this.right = r.Right;
				this.bottom = r.Bottom;
			}

			// Token: 0x06006C03 RID: 27651 RVA: 0x0018F122 File Offset: 0x0018D322
			public static NativeMethods.RECT FromXYWH(int x, int y, int width, int height)
			{
				return new NativeMethods.RECT(x, y, x + width, y + height);
			}

			// Token: 0x170017A8 RID: 6056
			// (get) Token: 0x06006C04 RID: 27652 RVA: 0x0018F131 File Offset: 0x0018D331
			public Size Size
			{
				get
				{
					return new Size(this.right - this.left, this.bottom - this.top);
				}
			}

			// Token: 0x06006C05 RID: 27653 RVA: 0x0018F152 File Offset: 0x0018D352
			public static implicit operator Rectangle(NativeMethods.RECT r)
			{
				return Rectangle.FromLTRB(r.left, r.top, r.right, r.bottom);
			}

			// Token: 0x06006C06 RID: 27654 RVA: 0x0018F171 File Offset: 0x0018D371
			public static implicit operator NativeMethods.RECT(Rectangle r)
			{
				return new NativeMethods.RECT(r);
			}

			// Token: 0x04003C41 RID: 15425
			public int left;

			// Token: 0x04003C42 RID: 15426
			public int top;

			// Token: 0x04003C43 RID: 15427
			public int right;

			// Token: 0x04003C44 RID: 15428
			public int bottom;
		}

		// Token: 0x0200070A RID: 1802
		public struct MARGINS
		{
			// Token: 0x04003C45 RID: 15429
			public int cxLeftWidth;

			// Token: 0x04003C46 RID: 15430
			public int cxRightWidth;

			// Token: 0x04003C47 RID: 15431
			public int cyTopHeight;

			// Token: 0x04003C48 RID: 15432
			public int cyBottomHeight;
		}

		// Token: 0x0200070B RID: 1803
		// (Invoke) Token: 0x06006C08 RID: 27656
		public delegate int ListViewCompareCallback(IntPtr lParam1, IntPtr lParam2, IntPtr lParamSort);

		// Token: 0x0200070C RID: 1804
		// (Invoke) Token: 0x06006C0C RID: 27660
		public delegate int TreeViewCompareCallback(IntPtr lParam1, IntPtr lParam2, IntPtr lParamSort);

		// Token: 0x0200070D RID: 1805
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class WNDCLASS_I
		{
			// Token: 0x04003C49 RID: 15433
			public int style;

			// Token: 0x04003C4A RID: 15434
			public IntPtr lpfnWndProc = IntPtr.Zero;

			// Token: 0x04003C4B RID: 15435
			public int cbClsExtra;

			// Token: 0x04003C4C RID: 15436
			public int cbWndExtra;

			// Token: 0x04003C4D RID: 15437
			public IntPtr hInstance = IntPtr.Zero;

			// Token: 0x04003C4E RID: 15438
			public IntPtr hIcon = IntPtr.Zero;

			// Token: 0x04003C4F RID: 15439
			public IntPtr hCursor = IntPtr.Zero;

			// Token: 0x04003C50 RID: 15440
			public IntPtr hbrBackground = IntPtr.Zero;

			// Token: 0x04003C51 RID: 15441
			public IntPtr lpszMenuName = IntPtr.Zero;

			// Token: 0x04003C52 RID: 15442
			public IntPtr lpszClassName = IntPtr.Zero;
		}

		// Token: 0x0200070E RID: 1806
		[StructLayout(LayoutKind.Sequential)]
		public class NONCLIENTMETRICS
		{
			// Token: 0x04003C53 RID: 15443
			public int cbSize = Marshal.SizeOf(typeof(NativeMethods.NONCLIENTMETRICS));

			// Token: 0x04003C54 RID: 15444
			public int iBorderWidth;

			// Token: 0x04003C55 RID: 15445
			public int iScrollWidth;

			// Token: 0x04003C56 RID: 15446
			public int iScrollHeight;

			// Token: 0x04003C57 RID: 15447
			public int iCaptionWidth;

			// Token: 0x04003C58 RID: 15448
			public int iCaptionHeight;

			// Token: 0x04003C59 RID: 15449
			[MarshalAs(UnmanagedType.Struct)]
			public NativeMethods.LOGFONT lfCaptionFont;

			// Token: 0x04003C5A RID: 15450
			public int iSmCaptionWidth;

			// Token: 0x04003C5B RID: 15451
			public int iSmCaptionHeight;

			// Token: 0x04003C5C RID: 15452
			[MarshalAs(UnmanagedType.Struct)]
			public NativeMethods.LOGFONT lfSmCaptionFont;

			// Token: 0x04003C5D RID: 15453
			public int iMenuWidth;

			// Token: 0x04003C5E RID: 15454
			public int iMenuHeight;

			// Token: 0x04003C5F RID: 15455
			[MarshalAs(UnmanagedType.Struct)]
			public NativeMethods.LOGFONT lfMenuFont;

			// Token: 0x04003C60 RID: 15456
			[MarshalAs(UnmanagedType.Struct)]
			public NativeMethods.LOGFONT lfStatusFont;

			// Token: 0x04003C61 RID: 15457
			[MarshalAs(UnmanagedType.Struct)]
			public NativeMethods.LOGFONT lfMessageFont;

			// Token: 0x04003C62 RID: 15458
			public int iPaddedBorderWidth;
		}

		// Token: 0x0200070F RID: 1807
		[Serializable]
		public struct MSG
		{
			// Token: 0x04003C63 RID: 15459
			public IntPtr hwnd;

			// Token: 0x04003C64 RID: 15460
			public int message;

			// Token: 0x04003C65 RID: 15461
			public IntPtr wParam;

			// Token: 0x04003C66 RID: 15462
			public IntPtr lParam;

			// Token: 0x04003C67 RID: 15463
			public int time;

			// Token: 0x04003C68 RID: 15464
			public int pt_x;

			// Token: 0x04003C69 RID: 15465
			public int pt_y;
		}

		// Token: 0x02000710 RID: 1808
		public struct PAINTSTRUCT
		{
			// Token: 0x04003C6A RID: 15466
			public IntPtr hdc;

			// Token: 0x04003C6B RID: 15467
			public bool fErase;

			// Token: 0x04003C6C RID: 15468
			public int rcPaint_left;

			// Token: 0x04003C6D RID: 15469
			public int rcPaint_top;

			// Token: 0x04003C6E RID: 15470
			public int rcPaint_right;

			// Token: 0x04003C6F RID: 15471
			public int rcPaint_bottom;

			// Token: 0x04003C70 RID: 15472
			public bool fRestore;

			// Token: 0x04003C71 RID: 15473
			public bool fIncUpdate;

			// Token: 0x04003C72 RID: 15474
			public int reserved1;

			// Token: 0x04003C73 RID: 15475
			public int reserved2;

			// Token: 0x04003C74 RID: 15476
			public int reserved3;

			// Token: 0x04003C75 RID: 15477
			public int reserved4;

			// Token: 0x04003C76 RID: 15478
			public int reserved5;

			// Token: 0x04003C77 RID: 15479
			public int reserved6;

			// Token: 0x04003C78 RID: 15480
			public int reserved7;

			// Token: 0x04003C79 RID: 15481
			public int reserved8;
		}

		// Token: 0x02000711 RID: 1809
		[StructLayout(LayoutKind.Sequential)]
		public class SCROLLINFO
		{
			// Token: 0x06006C11 RID: 27665 RVA: 0x0018F1F9 File Offset: 0x0018D3F9
			public SCROLLINFO()
			{
			}

			// Token: 0x06006C12 RID: 27666 RVA: 0x0018F218 File Offset: 0x0018D418
			public SCROLLINFO(int mask, int min, int max, int page, int pos)
			{
				this.fMask = mask;
				this.nMin = min;
				this.nMax = max;
				this.nPage = page;
				this.nPos = pos;
			}

			// Token: 0x04003C7A RID: 15482
			public int cbSize = Marshal.SizeOf(typeof(NativeMethods.SCROLLINFO));

			// Token: 0x04003C7B RID: 15483
			public int fMask;

			// Token: 0x04003C7C RID: 15484
			public int nMin;

			// Token: 0x04003C7D RID: 15485
			public int nMax;

			// Token: 0x04003C7E RID: 15486
			public int nPage;

			// Token: 0x04003C7F RID: 15487
			public int nPos;

			// Token: 0x04003C80 RID: 15488
			public int nTrackPos;
		}

		// Token: 0x02000712 RID: 1810
		[StructLayout(LayoutKind.Sequential)]
		public class TPMPARAMS
		{
			// Token: 0x04003C81 RID: 15489
			public int cbSize = Marshal.SizeOf(typeof(NativeMethods.TPMPARAMS));

			// Token: 0x04003C82 RID: 15490
			public int rcExclude_left;

			// Token: 0x04003C83 RID: 15491
			public int rcExclude_top;

			// Token: 0x04003C84 RID: 15492
			public int rcExclude_right;

			// Token: 0x04003C85 RID: 15493
			public int rcExclude_bottom;
		}

		// Token: 0x02000713 RID: 1811
		[StructLayout(LayoutKind.Sequential)]
		public class SIZE
		{
			// Token: 0x06006C14 RID: 27668 RVA: 0x00002843 File Offset: 0x00000A43
			public SIZE()
			{
			}

			// Token: 0x06006C15 RID: 27669 RVA: 0x0018F282 File Offset: 0x0018D482
			public SIZE(int cx, int cy)
			{
				this.cx = cx;
				this.cy = cy;
			}

			// Token: 0x04003C86 RID: 15494
			public int cx;

			// Token: 0x04003C87 RID: 15495
			public int cy;
		}

		// Token: 0x02000714 RID: 1812
		public struct WINDOWPLACEMENT
		{
			// Token: 0x04003C88 RID: 15496
			public int length;

			// Token: 0x04003C89 RID: 15497
			public int flags;

			// Token: 0x04003C8A RID: 15498
			public int showCmd;

			// Token: 0x04003C8B RID: 15499
			public int ptMinPosition_x;

			// Token: 0x04003C8C RID: 15500
			public int ptMinPosition_y;

			// Token: 0x04003C8D RID: 15501
			public int ptMaxPosition_x;

			// Token: 0x04003C8E RID: 15502
			public int ptMaxPosition_y;

			// Token: 0x04003C8F RID: 15503
			public int rcNormalPosition_left;

			// Token: 0x04003C90 RID: 15504
			public int rcNormalPosition_top;

			// Token: 0x04003C91 RID: 15505
			public int rcNormalPosition_right;

			// Token: 0x04003C92 RID: 15506
			public int rcNormalPosition_bottom;
		}

		// Token: 0x02000715 RID: 1813
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class STARTUPINFO_I
		{
			// Token: 0x04003C93 RID: 15507
			public int cb;

			// Token: 0x04003C94 RID: 15508
			public IntPtr lpReserved = IntPtr.Zero;

			// Token: 0x04003C95 RID: 15509
			public IntPtr lpDesktop = IntPtr.Zero;

			// Token: 0x04003C96 RID: 15510
			public IntPtr lpTitle = IntPtr.Zero;

			// Token: 0x04003C97 RID: 15511
			public int dwX;

			// Token: 0x04003C98 RID: 15512
			public int dwY;

			// Token: 0x04003C99 RID: 15513
			public int dwXSize;

			// Token: 0x04003C9A RID: 15514
			public int dwYSize;

			// Token: 0x04003C9B RID: 15515
			public int dwXCountChars;

			// Token: 0x04003C9C RID: 15516
			public int dwYCountChars;

			// Token: 0x04003C9D RID: 15517
			public int dwFillAttribute;

			// Token: 0x04003C9E RID: 15518
			public int dwFlags;

			// Token: 0x04003C9F RID: 15519
			public short wShowWindow;

			// Token: 0x04003CA0 RID: 15520
			public short cbReserved2;

			// Token: 0x04003CA1 RID: 15521
			public IntPtr lpReserved2 = IntPtr.Zero;

			// Token: 0x04003CA2 RID: 15522
			public IntPtr hStdInput = IntPtr.Zero;

			// Token: 0x04003CA3 RID: 15523
			public IntPtr hStdOutput = IntPtr.Zero;

			// Token: 0x04003CA4 RID: 15524
			public IntPtr hStdError = IntPtr.Zero;
		}

		// Token: 0x02000716 RID: 1814
		[StructLayout(LayoutKind.Sequential)]
		public class PAGESETUPDLG
		{
			// Token: 0x04003CA5 RID: 15525
			public int lStructSize;

			// Token: 0x04003CA6 RID: 15526
			public IntPtr hwndOwner;

			// Token: 0x04003CA7 RID: 15527
			public IntPtr hDevMode;

			// Token: 0x04003CA8 RID: 15528
			public IntPtr hDevNames;

			// Token: 0x04003CA9 RID: 15529
			public int Flags;

			// Token: 0x04003CAA RID: 15530
			public int paperSizeX;

			// Token: 0x04003CAB RID: 15531
			public int paperSizeY;

			// Token: 0x04003CAC RID: 15532
			public int minMarginLeft;

			// Token: 0x04003CAD RID: 15533
			public int minMarginTop;

			// Token: 0x04003CAE RID: 15534
			public int minMarginRight;

			// Token: 0x04003CAF RID: 15535
			public int minMarginBottom;

			// Token: 0x04003CB0 RID: 15536
			public int marginLeft;

			// Token: 0x04003CB1 RID: 15537
			public int marginTop;

			// Token: 0x04003CB2 RID: 15538
			public int marginRight;

			// Token: 0x04003CB3 RID: 15539
			public int marginBottom;

			// Token: 0x04003CB4 RID: 15540
			public IntPtr hInstance = IntPtr.Zero;

			// Token: 0x04003CB5 RID: 15541
			public IntPtr lCustData = IntPtr.Zero;

			// Token: 0x04003CB6 RID: 15542
			public NativeMethods.WndProc lpfnPageSetupHook;

			// Token: 0x04003CB7 RID: 15543
			public NativeMethods.WndProc lpfnPagePaintHook;

			// Token: 0x04003CB8 RID: 15544
			public string lpPageSetupTemplateName;

			// Token: 0x04003CB9 RID: 15545
			public IntPtr hPageSetupTemplate = IntPtr.Zero;
		}

		// Token: 0x02000717 RID: 1815
		public interface PRINTDLG
		{
			// Token: 0x170017A9 RID: 6057
			// (get) Token: 0x06006C18 RID: 27672
			// (set) Token: 0x06006C19 RID: 27673
			int lStructSize { get; set; }

			// Token: 0x170017AA RID: 6058
			// (get) Token: 0x06006C1A RID: 27674
			// (set) Token: 0x06006C1B RID: 27675
			IntPtr hwndOwner { get; set; }

			// Token: 0x170017AB RID: 6059
			// (get) Token: 0x06006C1C RID: 27676
			// (set) Token: 0x06006C1D RID: 27677
			IntPtr hDevMode { get; set; }

			// Token: 0x170017AC RID: 6060
			// (get) Token: 0x06006C1E RID: 27678
			// (set) Token: 0x06006C1F RID: 27679
			IntPtr hDevNames { get; set; }

			// Token: 0x170017AD RID: 6061
			// (get) Token: 0x06006C20 RID: 27680
			// (set) Token: 0x06006C21 RID: 27681
			IntPtr hDC { get; set; }

			// Token: 0x170017AE RID: 6062
			// (get) Token: 0x06006C22 RID: 27682
			// (set) Token: 0x06006C23 RID: 27683
			int Flags { get; set; }

			// Token: 0x170017AF RID: 6063
			// (get) Token: 0x06006C24 RID: 27684
			// (set) Token: 0x06006C25 RID: 27685
			short nFromPage { get; set; }

			// Token: 0x170017B0 RID: 6064
			// (get) Token: 0x06006C26 RID: 27686
			// (set) Token: 0x06006C27 RID: 27687
			short nToPage { get; set; }

			// Token: 0x170017B1 RID: 6065
			// (get) Token: 0x06006C28 RID: 27688
			// (set) Token: 0x06006C29 RID: 27689
			short nMinPage { get; set; }

			// Token: 0x170017B2 RID: 6066
			// (get) Token: 0x06006C2A RID: 27690
			// (set) Token: 0x06006C2B RID: 27691
			short nMaxPage { get; set; }

			// Token: 0x170017B3 RID: 6067
			// (get) Token: 0x06006C2C RID: 27692
			// (set) Token: 0x06006C2D RID: 27693
			short nCopies { get; set; }

			// Token: 0x170017B4 RID: 6068
			// (get) Token: 0x06006C2E RID: 27694
			// (set) Token: 0x06006C2F RID: 27695
			IntPtr hInstance { get; set; }

			// Token: 0x170017B5 RID: 6069
			// (get) Token: 0x06006C30 RID: 27696
			// (set) Token: 0x06006C31 RID: 27697
			IntPtr lCustData { get; set; }

			// Token: 0x170017B6 RID: 6070
			// (get) Token: 0x06006C32 RID: 27698
			// (set) Token: 0x06006C33 RID: 27699
			NativeMethods.WndProc lpfnPrintHook { get; set; }

			// Token: 0x170017B7 RID: 6071
			// (get) Token: 0x06006C34 RID: 27700
			// (set) Token: 0x06006C35 RID: 27701
			NativeMethods.WndProc lpfnSetupHook { get; set; }

			// Token: 0x170017B8 RID: 6072
			// (get) Token: 0x06006C36 RID: 27702
			// (set) Token: 0x06006C37 RID: 27703
			string lpPrintTemplateName { get; set; }

			// Token: 0x170017B9 RID: 6073
			// (get) Token: 0x06006C38 RID: 27704
			// (set) Token: 0x06006C39 RID: 27705
			string lpSetupTemplateName { get; set; }

			// Token: 0x170017BA RID: 6074
			// (get) Token: 0x06006C3A RID: 27706
			// (set) Token: 0x06006C3B RID: 27707
			IntPtr hPrintTemplate { get; set; }

			// Token: 0x170017BB RID: 6075
			// (get) Token: 0x06006C3C RID: 27708
			// (set) Token: 0x06006C3D RID: 27709
			IntPtr hSetupTemplate { get; set; }
		}

		// Token: 0x02000718 RID: 1816
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
		public class PRINTDLG_32 : NativeMethods.PRINTDLG
		{
			// Token: 0x170017BC RID: 6076
			// (get) Token: 0x06006C3E RID: 27710 RVA: 0x0018F321 File Offset: 0x0018D521
			// (set) Token: 0x06006C3F RID: 27711 RVA: 0x0018F329 File Offset: 0x0018D529
			public int lStructSize
			{
				get
				{
					return this.m_lStructSize;
				}
				set
				{
					this.m_lStructSize = value;
				}
			}

			// Token: 0x170017BD RID: 6077
			// (get) Token: 0x06006C40 RID: 27712 RVA: 0x0018F332 File Offset: 0x0018D532
			// (set) Token: 0x06006C41 RID: 27713 RVA: 0x0018F33A File Offset: 0x0018D53A
			public IntPtr hwndOwner
			{
				get
				{
					return this.m_hwndOwner;
				}
				set
				{
					this.m_hwndOwner = value;
				}
			}

			// Token: 0x170017BE RID: 6078
			// (get) Token: 0x06006C42 RID: 27714 RVA: 0x0018F343 File Offset: 0x0018D543
			// (set) Token: 0x06006C43 RID: 27715 RVA: 0x0018F34B File Offset: 0x0018D54B
			public IntPtr hDevMode
			{
				get
				{
					return this.m_hDevMode;
				}
				set
				{
					this.m_hDevMode = value;
				}
			}

			// Token: 0x170017BF RID: 6079
			// (get) Token: 0x06006C44 RID: 27716 RVA: 0x0018F354 File Offset: 0x0018D554
			// (set) Token: 0x06006C45 RID: 27717 RVA: 0x0018F35C File Offset: 0x0018D55C
			public IntPtr hDevNames
			{
				get
				{
					return this.m_hDevNames;
				}
				set
				{
					this.m_hDevNames = value;
				}
			}

			// Token: 0x170017C0 RID: 6080
			// (get) Token: 0x06006C46 RID: 27718 RVA: 0x0018F365 File Offset: 0x0018D565
			// (set) Token: 0x06006C47 RID: 27719 RVA: 0x0018F36D File Offset: 0x0018D56D
			public IntPtr hDC
			{
				get
				{
					return this.m_hDC;
				}
				set
				{
					this.m_hDC = value;
				}
			}

			// Token: 0x170017C1 RID: 6081
			// (get) Token: 0x06006C48 RID: 27720 RVA: 0x0018F376 File Offset: 0x0018D576
			// (set) Token: 0x06006C49 RID: 27721 RVA: 0x0018F37E File Offset: 0x0018D57E
			public int Flags
			{
				get
				{
					return this.m_Flags;
				}
				set
				{
					this.m_Flags = value;
				}
			}

			// Token: 0x170017C2 RID: 6082
			// (get) Token: 0x06006C4A RID: 27722 RVA: 0x0018F387 File Offset: 0x0018D587
			// (set) Token: 0x06006C4B RID: 27723 RVA: 0x0018F38F File Offset: 0x0018D58F
			public short nFromPage
			{
				get
				{
					return this.m_nFromPage;
				}
				set
				{
					this.m_nFromPage = value;
				}
			}

			// Token: 0x170017C3 RID: 6083
			// (get) Token: 0x06006C4C RID: 27724 RVA: 0x0018F398 File Offset: 0x0018D598
			// (set) Token: 0x06006C4D RID: 27725 RVA: 0x0018F3A0 File Offset: 0x0018D5A0
			public short nToPage
			{
				get
				{
					return this.m_nToPage;
				}
				set
				{
					this.m_nToPage = value;
				}
			}

			// Token: 0x170017C4 RID: 6084
			// (get) Token: 0x06006C4E RID: 27726 RVA: 0x0018F3A9 File Offset: 0x0018D5A9
			// (set) Token: 0x06006C4F RID: 27727 RVA: 0x0018F3B1 File Offset: 0x0018D5B1
			public short nMinPage
			{
				get
				{
					return this.m_nMinPage;
				}
				set
				{
					this.m_nMinPage = value;
				}
			}

			// Token: 0x170017C5 RID: 6085
			// (get) Token: 0x06006C50 RID: 27728 RVA: 0x0018F3BA File Offset: 0x0018D5BA
			// (set) Token: 0x06006C51 RID: 27729 RVA: 0x0018F3C2 File Offset: 0x0018D5C2
			public short nMaxPage
			{
				get
				{
					return this.m_nMaxPage;
				}
				set
				{
					this.m_nMaxPage = value;
				}
			}

			// Token: 0x170017C6 RID: 6086
			// (get) Token: 0x06006C52 RID: 27730 RVA: 0x0018F3CB File Offset: 0x0018D5CB
			// (set) Token: 0x06006C53 RID: 27731 RVA: 0x0018F3D3 File Offset: 0x0018D5D3
			public short nCopies
			{
				get
				{
					return this.m_nCopies;
				}
				set
				{
					this.m_nCopies = value;
				}
			}

			// Token: 0x170017C7 RID: 6087
			// (get) Token: 0x06006C54 RID: 27732 RVA: 0x0018F3DC File Offset: 0x0018D5DC
			// (set) Token: 0x06006C55 RID: 27733 RVA: 0x0018F3E4 File Offset: 0x0018D5E4
			public IntPtr hInstance
			{
				get
				{
					return this.m_hInstance;
				}
				set
				{
					this.m_hInstance = value;
				}
			}

			// Token: 0x170017C8 RID: 6088
			// (get) Token: 0x06006C56 RID: 27734 RVA: 0x0018F3ED File Offset: 0x0018D5ED
			// (set) Token: 0x06006C57 RID: 27735 RVA: 0x0018F3F5 File Offset: 0x0018D5F5
			public IntPtr lCustData
			{
				get
				{
					return this.m_lCustData;
				}
				set
				{
					this.m_lCustData = value;
				}
			}

			// Token: 0x170017C9 RID: 6089
			// (get) Token: 0x06006C58 RID: 27736 RVA: 0x0018F3FE File Offset: 0x0018D5FE
			// (set) Token: 0x06006C59 RID: 27737 RVA: 0x0018F406 File Offset: 0x0018D606
			public NativeMethods.WndProc lpfnPrintHook
			{
				get
				{
					return this.m_lpfnPrintHook;
				}
				set
				{
					this.m_lpfnPrintHook = value;
				}
			}

			// Token: 0x170017CA RID: 6090
			// (get) Token: 0x06006C5A RID: 27738 RVA: 0x0018F40F File Offset: 0x0018D60F
			// (set) Token: 0x06006C5B RID: 27739 RVA: 0x0018F417 File Offset: 0x0018D617
			public NativeMethods.WndProc lpfnSetupHook
			{
				get
				{
					return this.m_lpfnSetupHook;
				}
				set
				{
					this.m_lpfnSetupHook = value;
				}
			}

			// Token: 0x170017CB RID: 6091
			// (get) Token: 0x06006C5C RID: 27740 RVA: 0x0018F420 File Offset: 0x0018D620
			// (set) Token: 0x06006C5D RID: 27741 RVA: 0x0018F428 File Offset: 0x0018D628
			public string lpPrintTemplateName
			{
				get
				{
					return this.m_lpPrintTemplateName;
				}
				set
				{
					this.m_lpPrintTemplateName = value;
				}
			}

			// Token: 0x170017CC RID: 6092
			// (get) Token: 0x06006C5E RID: 27742 RVA: 0x0018F431 File Offset: 0x0018D631
			// (set) Token: 0x06006C5F RID: 27743 RVA: 0x0018F439 File Offset: 0x0018D639
			public string lpSetupTemplateName
			{
				get
				{
					return this.m_lpSetupTemplateName;
				}
				set
				{
					this.m_lpSetupTemplateName = value;
				}
			}

			// Token: 0x170017CD RID: 6093
			// (get) Token: 0x06006C60 RID: 27744 RVA: 0x0018F442 File Offset: 0x0018D642
			// (set) Token: 0x06006C61 RID: 27745 RVA: 0x0018F44A File Offset: 0x0018D64A
			public IntPtr hPrintTemplate
			{
				get
				{
					return this.m_hPrintTemplate;
				}
				set
				{
					this.m_hPrintTemplate = value;
				}
			}

			// Token: 0x170017CE RID: 6094
			// (get) Token: 0x06006C62 RID: 27746 RVA: 0x0018F453 File Offset: 0x0018D653
			// (set) Token: 0x06006C63 RID: 27747 RVA: 0x0018F45B File Offset: 0x0018D65B
			public IntPtr hSetupTemplate
			{
				get
				{
					return this.m_hSetupTemplate;
				}
				set
				{
					this.m_hSetupTemplate = value;
				}
			}

			// Token: 0x04003CBA RID: 15546
			private int m_lStructSize;

			// Token: 0x04003CBB RID: 15547
			private IntPtr m_hwndOwner;

			// Token: 0x04003CBC RID: 15548
			private IntPtr m_hDevMode;

			// Token: 0x04003CBD RID: 15549
			private IntPtr m_hDevNames;

			// Token: 0x04003CBE RID: 15550
			private IntPtr m_hDC;

			// Token: 0x04003CBF RID: 15551
			private int m_Flags;

			// Token: 0x04003CC0 RID: 15552
			private short m_nFromPage;

			// Token: 0x04003CC1 RID: 15553
			private short m_nToPage;

			// Token: 0x04003CC2 RID: 15554
			private short m_nMinPage;

			// Token: 0x04003CC3 RID: 15555
			private short m_nMaxPage;

			// Token: 0x04003CC4 RID: 15556
			private short m_nCopies;

			// Token: 0x04003CC5 RID: 15557
			private IntPtr m_hInstance;

			// Token: 0x04003CC6 RID: 15558
			private IntPtr m_lCustData;

			// Token: 0x04003CC7 RID: 15559
			private NativeMethods.WndProc m_lpfnPrintHook;

			// Token: 0x04003CC8 RID: 15560
			private NativeMethods.WndProc m_lpfnSetupHook;

			// Token: 0x04003CC9 RID: 15561
			private string m_lpPrintTemplateName;

			// Token: 0x04003CCA RID: 15562
			private string m_lpSetupTemplateName;

			// Token: 0x04003CCB RID: 15563
			private IntPtr m_hPrintTemplate;

			// Token: 0x04003CCC RID: 15564
			private IntPtr m_hSetupTemplate;
		}

		// Token: 0x02000719 RID: 1817
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class PRINTDLG_64 : NativeMethods.PRINTDLG
		{
			// Token: 0x170017CF RID: 6095
			// (get) Token: 0x06006C65 RID: 27749 RVA: 0x0018F464 File Offset: 0x0018D664
			// (set) Token: 0x06006C66 RID: 27750 RVA: 0x0018F46C File Offset: 0x0018D66C
			public int lStructSize
			{
				get
				{
					return this.m_lStructSize;
				}
				set
				{
					this.m_lStructSize = value;
				}
			}

			// Token: 0x170017D0 RID: 6096
			// (get) Token: 0x06006C67 RID: 27751 RVA: 0x0018F475 File Offset: 0x0018D675
			// (set) Token: 0x06006C68 RID: 27752 RVA: 0x0018F47D File Offset: 0x0018D67D
			public IntPtr hwndOwner
			{
				get
				{
					return this.m_hwndOwner;
				}
				set
				{
					this.m_hwndOwner = value;
				}
			}

			// Token: 0x170017D1 RID: 6097
			// (get) Token: 0x06006C69 RID: 27753 RVA: 0x0018F486 File Offset: 0x0018D686
			// (set) Token: 0x06006C6A RID: 27754 RVA: 0x0018F48E File Offset: 0x0018D68E
			public IntPtr hDevMode
			{
				get
				{
					return this.m_hDevMode;
				}
				set
				{
					this.m_hDevMode = value;
				}
			}

			// Token: 0x170017D2 RID: 6098
			// (get) Token: 0x06006C6B RID: 27755 RVA: 0x0018F497 File Offset: 0x0018D697
			// (set) Token: 0x06006C6C RID: 27756 RVA: 0x0018F49F File Offset: 0x0018D69F
			public IntPtr hDevNames
			{
				get
				{
					return this.m_hDevNames;
				}
				set
				{
					this.m_hDevNames = value;
				}
			}

			// Token: 0x170017D3 RID: 6099
			// (get) Token: 0x06006C6D RID: 27757 RVA: 0x0018F4A8 File Offset: 0x0018D6A8
			// (set) Token: 0x06006C6E RID: 27758 RVA: 0x0018F4B0 File Offset: 0x0018D6B0
			public IntPtr hDC
			{
				get
				{
					return this.m_hDC;
				}
				set
				{
					this.m_hDC = value;
				}
			}

			// Token: 0x170017D4 RID: 6100
			// (get) Token: 0x06006C6F RID: 27759 RVA: 0x0018F4B9 File Offset: 0x0018D6B9
			// (set) Token: 0x06006C70 RID: 27760 RVA: 0x0018F4C1 File Offset: 0x0018D6C1
			public int Flags
			{
				get
				{
					return this.m_Flags;
				}
				set
				{
					this.m_Flags = value;
				}
			}

			// Token: 0x170017D5 RID: 6101
			// (get) Token: 0x06006C71 RID: 27761 RVA: 0x0018F4CA File Offset: 0x0018D6CA
			// (set) Token: 0x06006C72 RID: 27762 RVA: 0x0018F4D2 File Offset: 0x0018D6D2
			public short nFromPage
			{
				get
				{
					return this.m_nFromPage;
				}
				set
				{
					this.m_nFromPage = value;
				}
			}

			// Token: 0x170017D6 RID: 6102
			// (get) Token: 0x06006C73 RID: 27763 RVA: 0x0018F4DB File Offset: 0x0018D6DB
			// (set) Token: 0x06006C74 RID: 27764 RVA: 0x0018F4E3 File Offset: 0x0018D6E3
			public short nToPage
			{
				get
				{
					return this.m_nToPage;
				}
				set
				{
					this.m_nToPage = value;
				}
			}

			// Token: 0x170017D7 RID: 6103
			// (get) Token: 0x06006C75 RID: 27765 RVA: 0x0018F4EC File Offset: 0x0018D6EC
			// (set) Token: 0x06006C76 RID: 27766 RVA: 0x0018F4F4 File Offset: 0x0018D6F4
			public short nMinPage
			{
				get
				{
					return this.m_nMinPage;
				}
				set
				{
					this.m_nMinPage = value;
				}
			}

			// Token: 0x170017D8 RID: 6104
			// (get) Token: 0x06006C77 RID: 27767 RVA: 0x0018F4FD File Offset: 0x0018D6FD
			// (set) Token: 0x06006C78 RID: 27768 RVA: 0x0018F505 File Offset: 0x0018D705
			public short nMaxPage
			{
				get
				{
					return this.m_nMaxPage;
				}
				set
				{
					this.m_nMaxPage = value;
				}
			}

			// Token: 0x170017D9 RID: 6105
			// (get) Token: 0x06006C79 RID: 27769 RVA: 0x0018F50E File Offset: 0x0018D70E
			// (set) Token: 0x06006C7A RID: 27770 RVA: 0x0018F516 File Offset: 0x0018D716
			public short nCopies
			{
				get
				{
					return this.m_nCopies;
				}
				set
				{
					this.m_nCopies = value;
				}
			}

			// Token: 0x170017DA RID: 6106
			// (get) Token: 0x06006C7B RID: 27771 RVA: 0x0018F51F File Offset: 0x0018D71F
			// (set) Token: 0x06006C7C RID: 27772 RVA: 0x0018F527 File Offset: 0x0018D727
			public IntPtr hInstance
			{
				get
				{
					return this.m_hInstance;
				}
				set
				{
					this.m_hInstance = value;
				}
			}

			// Token: 0x170017DB RID: 6107
			// (get) Token: 0x06006C7D RID: 27773 RVA: 0x0018F530 File Offset: 0x0018D730
			// (set) Token: 0x06006C7E RID: 27774 RVA: 0x0018F538 File Offset: 0x0018D738
			public IntPtr lCustData
			{
				get
				{
					return this.m_lCustData;
				}
				set
				{
					this.m_lCustData = value;
				}
			}

			// Token: 0x170017DC RID: 6108
			// (get) Token: 0x06006C7F RID: 27775 RVA: 0x0018F541 File Offset: 0x0018D741
			// (set) Token: 0x06006C80 RID: 27776 RVA: 0x0018F549 File Offset: 0x0018D749
			public NativeMethods.WndProc lpfnPrintHook
			{
				get
				{
					return this.m_lpfnPrintHook;
				}
				set
				{
					this.m_lpfnPrintHook = value;
				}
			}

			// Token: 0x170017DD RID: 6109
			// (get) Token: 0x06006C81 RID: 27777 RVA: 0x0018F552 File Offset: 0x0018D752
			// (set) Token: 0x06006C82 RID: 27778 RVA: 0x0018F55A File Offset: 0x0018D75A
			public NativeMethods.WndProc lpfnSetupHook
			{
				get
				{
					return this.m_lpfnSetupHook;
				}
				set
				{
					this.m_lpfnSetupHook = value;
				}
			}

			// Token: 0x170017DE RID: 6110
			// (get) Token: 0x06006C83 RID: 27779 RVA: 0x0018F563 File Offset: 0x0018D763
			// (set) Token: 0x06006C84 RID: 27780 RVA: 0x0018F56B File Offset: 0x0018D76B
			public string lpPrintTemplateName
			{
				get
				{
					return this.m_lpPrintTemplateName;
				}
				set
				{
					this.m_lpPrintTemplateName = value;
				}
			}

			// Token: 0x170017DF RID: 6111
			// (get) Token: 0x06006C85 RID: 27781 RVA: 0x0018F574 File Offset: 0x0018D774
			// (set) Token: 0x06006C86 RID: 27782 RVA: 0x0018F57C File Offset: 0x0018D77C
			public string lpSetupTemplateName
			{
				get
				{
					return this.m_lpSetupTemplateName;
				}
				set
				{
					this.m_lpSetupTemplateName = value;
				}
			}

			// Token: 0x170017E0 RID: 6112
			// (get) Token: 0x06006C87 RID: 27783 RVA: 0x0018F585 File Offset: 0x0018D785
			// (set) Token: 0x06006C88 RID: 27784 RVA: 0x0018F58D File Offset: 0x0018D78D
			public IntPtr hPrintTemplate
			{
				get
				{
					return this.m_hPrintTemplate;
				}
				set
				{
					this.m_hPrintTemplate = value;
				}
			}

			// Token: 0x170017E1 RID: 6113
			// (get) Token: 0x06006C89 RID: 27785 RVA: 0x0018F596 File Offset: 0x0018D796
			// (set) Token: 0x06006C8A RID: 27786 RVA: 0x0018F59E File Offset: 0x0018D79E
			public IntPtr hSetupTemplate
			{
				get
				{
					return this.m_hSetupTemplate;
				}
				set
				{
					this.m_hSetupTemplate = value;
				}
			}

			// Token: 0x04003CCD RID: 15565
			private int m_lStructSize;

			// Token: 0x04003CCE RID: 15566
			private IntPtr m_hwndOwner;

			// Token: 0x04003CCF RID: 15567
			private IntPtr m_hDevMode;

			// Token: 0x04003CD0 RID: 15568
			private IntPtr m_hDevNames;

			// Token: 0x04003CD1 RID: 15569
			private IntPtr m_hDC;

			// Token: 0x04003CD2 RID: 15570
			private int m_Flags;

			// Token: 0x04003CD3 RID: 15571
			private short m_nFromPage;

			// Token: 0x04003CD4 RID: 15572
			private short m_nToPage;

			// Token: 0x04003CD5 RID: 15573
			private short m_nMinPage;

			// Token: 0x04003CD6 RID: 15574
			private short m_nMaxPage;

			// Token: 0x04003CD7 RID: 15575
			private short m_nCopies;

			// Token: 0x04003CD8 RID: 15576
			private IntPtr m_hInstance;

			// Token: 0x04003CD9 RID: 15577
			private IntPtr m_lCustData;

			// Token: 0x04003CDA RID: 15578
			private NativeMethods.WndProc m_lpfnPrintHook;

			// Token: 0x04003CDB RID: 15579
			private NativeMethods.WndProc m_lpfnSetupHook;

			// Token: 0x04003CDC RID: 15580
			private string m_lpPrintTemplateName;

			// Token: 0x04003CDD RID: 15581
			private string m_lpSetupTemplateName;

			// Token: 0x04003CDE RID: 15582
			private IntPtr m_hPrintTemplate;

			// Token: 0x04003CDF RID: 15583
			private IntPtr m_hSetupTemplate;
		}

		// Token: 0x0200071A RID: 1818
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class PRINTDLGEX
		{
			// Token: 0x04003CE0 RID: 15584
			public int lStructSize;

			// Token: 0x04003CE1 RID: 15585
			public IntPtr hwndOwner;

			// Token: 0x04003CE2 RID: 15586
			public IntPtr hDevMode;

			// Token: 0x04003CE3 RID: 15587
			public IntPtr hDevNames;

			// Token: 0x04003CE4 RID: 15588
			public IntPtr hDC;

			// Token: 0x04003CE5 RID: 15589
			public int Flags;

			// Token: 0x04003CE6 RID: 15590
			public int Flags2;

			// Token: 0x04003CE7 RID: 15591
			public int ExclusionFlags;

			// Token: 0x04003CE8 RID: 15592
			public int nPageRanges;

			// Token: 0x04003CE9 RID: 15593
			public int nMaxPageRanges;

			// Token: 0x04003CEA RID: 15594
			public IntPtr pageRanges;

			// Token: 0x04003CEB RID: 15595
			public int nMinPage;

			// Token: 0x04003CEC RID: 15596
			public int nMaxPage;

			// Token: 0x04003CED RID: 15597
			public int nCopies;

			// Token: 0x04003CEE RID: 15598
			public IntPtr hInstance;

			// Token: 0x04003CEF RID: 15599
			[MarshalAs(UnmanagedType.LPStr)]
			public string lpPrintTemplateName;

			// Token: 0x04003CF0 RID: 15600
			public NativeMethods.WndProc lpCallback;

			// Token: 0x04003CF1 RID: 15601
			public int nPropertyPages;

			// Token: 0x04003CF2 RID: 15602
			public IntPtr lphPropertyPages;

			// Token: 0x04003CF3 RID: 15603
			public int nStartPage;

			// Token: 0x04003CF4 RID: 15604
			public int dwResultAction;
		}

		// Token: 0x0200071B RID: 1819
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
		public class PRINTPAGERANGE
		{
			// Token: 0x04003CF5 RID: 15605
			public int nFromPage;

			// Token: 0x04003CF6 RID: 15606
			public int nToPage;
		}

		// Token: 0x0200071C RID: 1820
		[StructLayout(LayoutKind.Sequential)]
		public class PICTDESC
		{
			// Token: 0x06006C8E RID: 27790 RVA: 0x0018F5A8 File Offset: 0x0018D7A8
			public static NativeMethods.PICTDESC CreateBitmapPICTDESC(IntPtr hbitmap, IntPtr hpal)
			{
				return new NativeMethods.PICTDESC
				{
					cbSizeOfStruct = 16,
					picType = 1,
					union1 = hbitmap,
					union2 = (int)((long)hpal & (long)((ulong)(-1))),
					union3 = (int)((long)hpal >> 32)
				};
			}

			// Token: 0x06006C8F RID: 27791 RVA: 0x0018F5F4 File Offset: 0x0018D7F4
			public static NativeMethods.PICTDESC CreateIconPICTDESC(IntPtr hicon)
			{
				return new NativeMethods.PICTDESC
				{
					cbSizeOfStruct = 12,
					picType = 3,
					union1 = hicon
				};
			}

			// Token: 0x06006C90 RID: 27792 RVA: 0x0018F61E File Offset: 0x0018D81E
			public virtual IntPtr GetHandle()
			{
				return this.union1;
			}

			// Token: 0x06006C91 RID: 27793 RVA: 0x0018F626 File Offset: 0x0018D826
			public virtual IntPtr GetHPal()
			{
				if (this.picType == 1)
				{
					return (IntPtr)((long)((ulong)this.union2 | (ulong)((ulong)((long)this.union3) << 32)));
				}
				return IntPtr.Zero;
			}

			// Token: 0x04003CF7 RID: 15607
			internal int cbSizeOfStruct;

			// Token: 0x04003CF8 RID: 15608
			public int picType;

			// Token: 0x04003CF9 RID: 15609
			internal IntPtr union1;

			// Token: 0x04003CFA RID: 15610
			internal int union2;

			// Token: 0x04003CFB RID: 15611
			internal int union3;
		}

		// Token: 0x0200071D RID: 1821
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagFONTDESC
		{
			// Token: 0x04003CFC RID: 15612
			public int cbSizeofstruct = Marshal.SizeOf(typeof(NativeMethods.tagFONTDESC));

			// Token: 0x04003CFD RID: 15613
			[MarshalAs(UnmanagedType.LPWStr)]
			public string lpstrName;

			// Token: 0x04003CFE RID: 15614
			[MarshalAs(UnmanagedType.U8)]
			public long cySize;

			// Token: 0x04003CFF RID: 15615
			[MarshalAs(UnmanagedType.U2)]
			public short sWeight;

			// Token: 0x04003D00 RID: 15616
			[MarshalAs(UnmanagedType.U2)]
			public short sCharset;

			// Token: 0x04003D01 RID: 15617
			[MarshalAs(UnmanagedType.Bool)]
			public bool fItalic;

			// Token: 0x04003D02 RID: 15618
			[MarshalAs(UnmanagedType.Bool)]
			public bool fUnderline;

			// Token: 0x04003D03 RID: 15619
			[MarshalAs(UnmanagedType.Bool)]
			public bool fStrikethrough;
		}

		// Token: 0x0200071E RID: 1822
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class CHOOSECOLOR
		{
			// Token: 0x04003D04 RID: 15620
			public int lStructSize = Marshal.SizeOf(typeof(NativeMethods.CHOOSECOLOR));

			// Token: 0x04003D05 RID: 15621
			public IntPtr hwndOwner;

			// Token: 0x04003D06 RID: 15622
			public IntPtr hInstance;

			// Token: 0x04003D07 RID: 15623
			public int rgbResult;

			// Token: 0x04003D08 RID: 15624
			public IntPtr lpCustColors;

			// Token: 0x04003D09 RID: 15625
			public int Flags;

			// Token: 0x04003D0A RID: 15626
			public IntPtr lCustData = IntPtr.Zero;

			// Token: 0x04003D0B RID: 15627
			public NativeMethods.WndProc lpfnHook;

			// Token: 0x04003D0C RID: 15628
			public string lpTemplateName;
		}

		// Token: 0x0200071F RID: 1823
		// (Invoke) Token: 0x06006C96 RID: 27798
		public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

		// Token: 0x02000720 RID: 1824
		[StructLayout(LayoutKind.Sequential)]
		public class BITMAP
		{
			// Token: 0x04003D0D RID: 15629
			public int bmType;

			// Token: 0x04003D0E RID: 15630
			public int bmWidth;

			// Token: 0x04003D0F RID: 15631
			public int bmHeight;

			// Token: 0x04003D10 RID: 15632
			public int bmWidthBytes;

			// Token: 0x04003D11 RID: 15633
			public short bmPlanes;

			// Token: 0x04003D12 RID: 15634
			public short bmBitsPixel;

			// Token: 0x04003D13 RID: 15635
			public IntPtr bmBits = IntPtr.Zero;
		}

		// Token: 0x02000721 RID: 1825
		[StructLayout(LayoutKind.Sequential)]
		public class ICONINFO
		{
			// Token: 0x04003D14 RID: 15636
			public int fIcon;

			// Token: 0x04003D15 RID: 15637
			public int xHotspot;

			// Token: 0x04003D16 RID: 15638
			public int yHotspot;

			// Token: 0x04003D17 RID: 15639
			public IntPtr hbmMask = IntPtr.Zero;

			// Token: 0x04003D18 RID: 15640
			public IntPtr hbmColor = IntPtr.Zero;
		}

		// Token: 0x02000722 RID: 1826
		[StructLayout(LayoutKind.Sequential)]
		public class LOGPEN
		{
			// Token: 0x04003D19 RID: 15641
			public int lopnStyle;

			// Token: 0x04003D1A RID: 15642
			public int lopnWidth_x;

			// Token: 0x04003D1B RID: 15643
			public int lopnWidth_y;

			// Token: 0x04003D1C RID: 15644
			public int lopnColor;
		}

		// Token: 0x02000723 RID: 1827
		[StructLayout(LayoutKind.Sequential)]
		public class LOGBRUSH
		{
			// Token: 0x04003D1D RID: 15645
			public int lbStyle;

			// Token: 0x04003D1E RID: 15646
			public int lbColor;

			// Token: 0x04003D1F RID: 15647
			public IntPtr lbHatch;
		}

		// Token: 0x02000724 RID: 1828
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class LOGFONT
		{
			// Token: 0x06006C9D RID: 27805 RVA: 0x00002843 File Offset: 0x00000A43
			public LOGFONT()
			{
			}

			// Token: 0x06006C9E RID: 27806 RVA: 0x0018F6C4 File Offset: 0x0018D8C4
			public LOGFONT(NativeMethods.LOGFONT lf)
			{
				this.lfHeight = lf.lfHeight;
				this.lfWidth = lf.lfWidth;
				this.lfEscapement = lf.lfEscapement;
				this.lfOrientation = lf.lfOrientation;
				this.lfWeight = lf.lfWeight;
				this.lfItalic = lf.lfItalic;
				this.lfUnderline = lf.lfUnderline;
				this.lfStrikeOut = lf.lfStrikeOut;
				this.lfCharSet = lf.lfCharSet;
				this.lfOutPrecision = lf.lfOutPrecision;
				this.lfClipPrecision = lf.lfClipPrecision;
				this.lfQuality = lf.lfQuality;
				this.lfPitchAndFamily = lf.lfPitchAndFamily;
				this.lfFaceName = lf.lfFaceName;
			}

			// Token: 0x06006C9F RID: 27807 RVA: 0x0018F780 File Offset: 0x0018D980
			public static NativeMethods.LOGFONT FromFont(Font font)
			{
				object obj = new NativeMethods.LOGFONT();
				font.ToLogFont(obj);
				return (NativeMethods.LOGFONT)obj;
			}

			// Token: 0x04003D20 RID: 15648
			public int lfHeight;

			// Token: 0x04003D21 RID: 15649
			public int lfWidth;

			// Token: 0x04003D22 RID: 15650
			public int lfEscapement;

			// Token: 0x04003D23 RID: 15651
			public int lfOrientation;

			// Token: 0x04003D24 RID: 15652
			public int lfWeight;

			// Token: 0x04003D25 RID: 15653
			public byte lfItalic;

			// Token: 0x04003D26 RID: 15654
			public byte lfUnderline;

			// Token: 0x04003D27 RID: 15655
			public byte lfStrikeOut;

			// Token: 0x04003D28 RID: 15656
			public byte lfCharSet;

			// Token: 0x04003D29 RID: 15657
			public byte lfOutPrecision;

			// Token: 0x04003D2A RID: 15658
			public byte lfClipPrecision;

			// Token: 0x04003D2B RID: 15659
			public byte lfQuality;

			// Token: 0x04003D2C RID: 15660
			public byte lfPitchAndFamily;

			// Token: 0x04003D2D RID: 15661
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string lfFaceName;
		}

		// Token: 0x02000725 RID: 1829
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct TEXTMETRIC
		{
			// Token: 0x04003D2E RID: 15662
			public int tmHeight;

			// Token: 0x04003D2F RID: 15663
			public int tmAscent;

			// Token: 0x04003D30 RID: 15664
			public int tmDescent;

			// Token: 0x04003D31 RID: 15665
			public int tmInternalLeading;

			// Token: 0x04003D32 RID: 15666
			public int tmExternalLeading;

			// Token: 0x04003D33 RID: 15667
			public int tmAveCharWidth;

			// Token: 0x04003D34 RID: 15668
			public int tmMaxCharWidth;

			// Token: 0x04003D35 RID: 15669
			public int tmWeight;

			// Token: 0x04003D36 RID: 15670
			public int tmOverhang;

			// Token: 0x04003D37 RID: 15671
			public int tmDigitizedAspectX;

			// Token: 0x04003D38 RID: 15672
			public int tmDigitizedAspectY;

			// Token: 0x04003D39 RID: 15673
			public char tmFirstChar;

			// Token: 0x04003D3A RID: 15674
			public char tmLastChar;

			// Token: 0x04003D3B RID: 15675
			public char tmDefaultChar;

			// Token: 0x04003D3C RID: 15676
			public char tmBreakChar;

			// Token: 0x04003D3D RID: 15677
			public byte tmItalic;

			// Token: 0x04003D3E RID: 15678
			public byte tmUnderlined;

			// Token: 0x04003D3F RID: 15679
			public byte tmStruckOut;

			// Token: 0x04003D40 RID: 15680
			public byte tmPitchAndFamily;

			// Token: 0x04003D41 RID: 15681
			public byte tmCharSet;
		}

		// Token: 0x02000726 RID: 1830
		public struct TEXTMETRICA
		{
			// Token: 0x04003D42 RID: 15682
			public int tmHeight;

			// Token: 0x04003D43 RID: 15683
			public int tmAscent;

			// Token: 0x04003D44 RID: 15684
			public int tmDescent;

			// Token: 0x04003D45 RID: 15685
			public int tmInternalLeading;

			// Token: 0x04003D46 RID: 15686
			public int tmExternalLeading;

			// Token: 0x04003D47 RID: 15687
			public int tmAveCharWidth;

			// Token: 0x04003D48 RID: 15688
			public int tmMaxCharWidth;

			// Token: 0x04003D49 RID: 15689
			public int tmWeight;

			// Token: 0x04003D4A RID: 15690
			public int tmOverhang;

			// Token: 0x04003D4B RID: 15691
			public int tmDigitizedAspectX;

			// Token: 0x04003D4C RID: 15692
			public int tmDigitizedAspectY;

			// Token: 0x04003D4D RID: 15693
			public byte tmFirstChar;

			// Token: 0x04003D4E RID: 15694
			public byte tmLastChar;

			// Token: 0x04003D4F RID: 15695
			public byte tmDefaultChar;

			// Token: 0x04003D50 RID: 15696
			public byte tmBreakChar;

			// Token: 0x04003D51 RID: 15697
			public byte tmItalic;

			// Token: 0x04003D52 RID: 15698
			public byte tmUnderlined;

			// Token: 0x04003D53 RID: 15699
			public byte tmStruckOut;

			// Token: 0x04003D54 RID: 15700
			public byte tmPitchAndFamily;

			// Token: 0x04003D55 RID: 15701
			public byte tmCharSet;
		}

		// Token: 0x02000727 RID: 1831
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class NOTIFYICONDATA
		{
			// Token: 0x04003D56 RID: 15702
			public int cbSize = Marshal.SizeOf(typeof(NativeMethods.NOTIFYICONDATA));

			// Token: 0x04003D57 RID: 15703
			public IntPtr hWnd;

			// Token: 0x04003D58 RID: 15704
			public int uID;

			// Token: 0x04003D59 RID: 15705
			public int uFlags;

			// Token: 0x04003D5A RID: 15706
			public int uCallbackMessage;

			// Token: 0x04003D5B RID: 15707
			public IntPtr hIcon;

			// Token: 0x04003D5C RID: 15708
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string szTip;

			// Token: 0x04003D5D RID: 15709
			public int dwState;

			// Token: 0x04003D5E RID: 15710
			public int dwStateMask;

			// Token: 0x04003D5F RID: 15711
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string szInfo;

			// Token: 0x04003D60 RID: 15712
			public int uTimeoutOrVersion;

			// Token: 0x04003D61 RID: 15713
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string szInfoTitle;

			// Token: 0x04003D62 RID: 15714
			public int dwInfoFlags;
		}

		// Token: 0x02000728 RID: 1832
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class MENUITEMINFO_T
		{
			// Token: 0x04003D63 RID: 15715
			public int cbSize = Marshal.SizeOf(typeof(NativeMethods.MENUITEMINFO_T));

			// Token: 0x04003D64 RID: 15716
			public int fMask;

			// Token: 0x04003D65 RID: 15717
			public int fType;

			// Token: 0x04003D66 RID: 15718
			public int fState;

			// Token: 0x04003D67 RID: 15719
			public int wID;

			// Token: 0x04003D68 RID: 15720
			public IntPtr hSubMenu;

			// Token: 0x04003D69 RID: 15721
			public IntPtr hbmpChecked;

			// Token: 0x04003D6A RID: 15722
			public IntPtr hbmpUnchecked;

			// Token: 0x04003D6B RID: 15723
			public IntPtr dwItemData;

			// Token: 0x04003D6C RID: 15724
			public string dwTypeData;

			// Token: 0x04003D6D RID: 15725
			public int cch;
		}

		// Token: 0x02000729 RID: 1833
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class MENUITEMINFO_T_RW
		{
			// Token: 0x04003D6E RID: 15726
			public int cbSize = Marshal.SizeOf(typeof(NativeMethods.MENUITEMINFO_T_RW));

			// Token: 0x04003D6F RID: 15727
			public int fMask;

			// Token: 0x04003D70 RID: 15728
			public int fType;

			// Token: 0x04003D71 RID: 15729
			public int fState;

			// Token: 0x04003D72 RID: 15730
			public int wID;

			// Token: 0x04003D73 RID: 15731
			public IntPtr hSubMenu = IntPtr.Zero;

			// Token: 0x04003D74 RID: 15732
			public IntPtr hbmpChecked = IntPtr.Zero;

			// Token: 0x04003D75 RID: 15733
			public IntPtr hbmpUnchecked = IntPtr.Zero;

			// Token: 0x04003D76 RID: 15734
			public IntPtr dwItemData = IntPtr.Zero;

			// Token: 0x04003D77 RID: 15735
			public IntPtr dwTypeData = IntPtr.Zero;

			// Token: 0x04003D78 RID: 15736
			public int cch;

			// Token: 0x04003D79 RID: 15737
			public IntPtr hbmpItem = IntPtr.Zero;
		}

		// Token: 0x0200072A RID: 1834
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct MSAAMENUINFO
		{
			// Token: 0x06006CA3 RID: 27811 RVA: 0x0018F846 File Offset: 0x0018DA46
			public MSAAMENUINFO(string text)
			{
				this.dwMSAASignature = -1441927155;
				this.cchWText = text.Length;
				this.pszWText = text;
			}

			// Token: 0x04003D7A RID: 15738
			public int dwMSAASignature;

			// Token: 0x04003D7B RID: 15739
			public int cchWText;

			// Token: 0x04003D7C RID: 15740
			public string pszWText;
		}

		// Token: 0x0200072B RID: 1835
		// (Invoke) Token: 0x06006CA5 RID: 27813
		public delegate bool EnumThreadWindowsCallback(IntPtr hWnd, IntPtr lParam);

		// Token: 0x0200072C RID: 1836
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class OPENFILENAME_I
		{
			// Token: 0x04003D7D RID: 15741
			public int lStructSize = Marshal.SizeOf(typeof(NativeMethods.OPENFILENAME_I));

			// Token: 0x04003D7E RID: 15742
			public IntPtr hwndOwner;

			// Token: 0x04003D7F RID: 15743
			public IntPtr hInstance;

			// Token: 0x04003D80 RID: 15744
			public string lpstrFilter;

			// Token: 0x04003D81 RID: 15745
			public IntPtr lpstrCustomFilter = IntPtr.Zero;

			// Token: 0x04003D82 RID: 15746
			public int nMaxCustFilter;

			// Token: 0x04003D83 RID: 15747
			public int nFilterIndex;

			// Token: 0x04003D84 RID: 15748
			public IntPtr lpstrFile;

			// Token: 0x04003D85 RID: 15749
			public int nMaxFile = 260;

			// Token: 0x04003D86 RID: 15750
			public IntPtr lpstrFileTitle = IntPtr.Zero;

			// Token: 0x04003D87 RID: 15751
			public int nMaxFileTitle = 260;

			// Token: 0x04003D88 RID: 15752
			public string lpstrInitialDir;

			// Token: 0x04003D89 RID: 15753
			public string lpstrTitle;

			// Token: 0x04003D8A RID: 15754
			public int Flags;

			// Token: 0x04003D8B RID: 15755
			public short nFileOffset;

			// Token: 0x04003D8C RID: 15756
			public short nFileExtension;

			// Token: 0x04003D8D RID: 15757
			public string lpstrDefExt;

			// Token: 0x04003D8E RID: 15758
			public IntPtr lCustData = IntPtr.Zero;

			// Token: 0x04003D8F RID: 15759
			public NativeMethods.WndProc lpfnHook;

			// Token: 0x04003D90 RID: 15760
			public string lpTemplateName;

			// Token: 0x04003D91 RID: 15761
			public IntPtr pvReserved = IntPtr.Zero;

			// Token: 0x04003D92 RID: 15762
			public int dwReserved;

			// Token: 0x04003D93 RID: 15763
			public int FlagsEx;
		}

		// Token: 0x0200072D RID: 1837
		[CLSCompliant(false)]
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class CHOOSEFONT
		{
			// Token: 0x04003D94 RID: 15764
			public int lStructSize = Marshal.SizeOf(typeof(NativeMethods.CHOOSEFONT));

			// Token: 0x04003D95 RID: 15765
			public IntPtr hwndOwner;

			// Token: 0x04003D96 RID: 15766
			public IntPtr hDC;

			// Token: 0x04003D97 RID: 15767
			public IntPtr lpLogFont;

			// Token: 0x04003D98 RID: 15768
			public int iPointSize;

			// Token: 0x04003D99 RID: 15769
			public int Flags;

			// Token: 0x04003D9A RID: 15770
			public int rgbColors;

			// Token: 0x04003D9B RID: 15771
			public IntPtr lCustData = IntPtr.Zero;

			// Token: 0x04003D9C RID: 15772
			public NativeMethods.WndProc lpfnHook;

			// Token: 0x04003D9D RID: 15773
			public string lpTemplateName;

			// Token: 0x04003D9E RID: 15774
			public IntPtr hInstance;

			// Token: 0x04003D9F RID: 15775
			public string lpszStyle;

			// Token: 0x04003DA0 RID: 15776
			public short nFontType;

			// Token: 0x04003DA1 RID: 15777
			public short ___MISSING_ALIGNMENT__;

			// Token: 0x04003DA2 RID: 15778
			public int nSizeMin;

			// Token: 0x04003DA3 RID: 15779
			public int nSizeMax;
		}

		// Token: 0x0200072E RID: 1838
		[StructLayout(LayoutKind.Sequential)]
		public class BITMAPINFO
		{
			// Token: 0x06006CAA RID: 27818 RVA: 0x0018F8FA File Offset: 0x0018DAFA
			private BITMAPINFO()
			{
			}

			// Token: 0x04003DA4 RID: 15780
			public int bmiHeader_biSize = 40;

			// Token: 0x04003DA5 RID: 15781
			public int bmiHeader_biWidth;

			// Token: 0x04003DA6 RID: 15782
			public int bmiHeader_biHeight;

			// Token: 0x04003DA7 RID: 15783
			public short bmiHeader_biPlanes;

			// Token: 0x04003DA8 RID: 15784
			public short bmiHeader_biBitCount;

			// Token: 0x04003DA9 RID: 15785
			public int bmiHeader_biCompression;

			// Token: 0x04003DAA RID: 15786
			public int bmiHeader_biSizeImage;

			// Token: 0x04003DAB RID: 15787
			public int bmiHeader_biXPelsPerMeter;

			// Token: 0x04003DAC RID: 15788
			public int bmiHeader_biYPelsPerMeter;

			// Token: 0x04003DAD RID: 15789
			public int bmiHeader_biClrUsed;

			// Token: 0x04003DAE RID: 15790
			public int bmiHeader_biClrImportant;

			// Token: 0x04003DAF RID: 15791
			public byte bmiColors_rgbBlue;

			// Token: 0x04003DB0 RID: 15792
			public byte bmiColors_rgbGreen;

			// Token: 0x04003DB1 RID: 15793
			public byte bmiColors_rgbRed;

			// Token: 0x04003DB2 RID: 15794
			public byte bmiColors_rgbReserved;
		}

		// Token: 0x0200072F RID: 1839
		[StructLayout(LayoutKind.Sequential)]
		public class BITMAPINFOHEADER
		{
			// Token: 0x04003DB3 RID: 15795
			public int biSize = 40;

			// Token: 0x04003DB4 RID: 15796
			public int biWidth;

			// Token: 0x04003DB5 RID: 15797
			public int biHeight;

			// Token: 0x04003DB6 RID: 15798
			public short biPlanes;

			// Token: 0x04003DB7 RID: 15799
			public short biBitCount;

			// Token: 0x04003DB8 RID: 15800
			public int biCompression;

			// Token: 0x04003DB9 RID: 15801
			public int biSizeImage;

			// Token: 0x04003DBA RID: 15802
			public int biXPelsPerMeter;

			// Token: 0x04003DBB RID: 15803
			public int biYPelsPerMeter;

			// Token: 0x04003DBC RID: 15804
			public int biClrUsed;

			// Token: 0x04003DBD RID: 15805
			public int biClrImportant;
		}

		// Token: 0x02000730 RID: 1840
		public class Ole
		{
			// Token: 0x04003DBE RID: 15806
			public const int PICTYPE_UNINITIALIZED = -1;

			// Token: 0x04003DBF RID: 15807
			public const int PICTYPE_NONE = 0;

			// Token: 0x04003DC0 RID: 15808
			public const int PICTYPE_BITMAP = 1;

			// Token: 0x04003DC1 RID: 15809
			public const int PICTYPE_METAFILE = 2;

			// Token: 0x04003DC2 RID: 15810
			public const int PICTYPE_ICON = 3;

			// Token: 0x04003DC3 RID: 15811
			public const int PICTYPE_ENHMETAFILE = 4;

			// Token: 0x04003DC4 RID: 15812
			public const int STATFLAG_DEFAULT = 0;

			// Token: 0x04003DC5 RID: 15813
			public const int STATFLAG_NONAME = 1;
		}

		// Token: 0x02000731 RID: 1841
		[StructLayout(LayoutKind.Sequential)]
		public class STATSTG
		{
			// Token: 0x04003DC6 RID: 15814
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pwcsName;

			// Token: 0x04003DC7 RID: 15815
			public int type;

			// Token: 0x04003DC8 RID: 15816
			[MarshalAs(UnmanagedType.I8)]
			public long cbSize;

			// Token: 0x04003DC9 RID: 15817
			[MarshalAs(UnmanagedType.I8)]
			public long mtime;

			// Token: 0x04003DCA RID: 15818
			[MarshalAs(UnmanagedType.I8)]
			public long ctime;

			// Token: 0x04003DCB RID: 15819
			[MarshalAs(UnmanagedType.I8)]
			public long atime;

			// Token: 0x04003DCC RID: 15820
			[MarshalAs(UnmanagedType.I4)]
			public int grfMode;

			// Token: 0x04003DCD RID: 15821
			[MarshalAs(UnmanagedType.I4)]
			public int grfLocksSupported;

			// Token: 0x04003DCE RID: 15822
			public int clsid_data1;

			// Token: 0x04003DCF RID: 15823
			[MarshalAs(UnmanagedType.I2)]
			public short clsid_data2;

			// Token: 0x04003DD0 RID: 15824
			[MarshalAs(UnmanagedType.I2)]
			public short clsid_data3;

			// Token: 0x04003DD1 RID: 15825
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b0;

			// Token: 0x04003DD2 RID: 15826
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b1;

			// Token: 0x04003DD3 RID: 15827
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b2;

			// Token: 0x04003DD4 RID: 15828
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b3;

			// Token: 0x04003DD5 RID: 15829
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b4;

			// Token: 0x04003DD6 RID: 15830
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b5;

			// Token: 0x04003DD7 RID: 15831
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b6;

			// Token: 0x04003DD8 RID: 15832
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b7;

			// Token: 0x04003DD9 RID: 15833
			[MarshalAs(UnmanagedType.I4)]
			public int grfStateBits;

			// Token: 0x04003DDA RID: 15834
			[MarshalAs(UnmanagedType.I4)]
			public int reserved;
		}

		// Token: 0x02000732 RID: 1842
		[StructLayout(LayoutKind.Sequential)]
		public class FILETIME
		{
			// Token: 0x04003DDB RID: 15835
			public uint dwLowDateTime;

			// Token: 0x04003DDC RID: 15836
			public uint dwHighDateTime;
		}

		// Token: 0x02000733 RID: 1843
		[StructLayout(LayoutKind.Sequential)]
		public class SYSTEMTIME
		{
			// Token: 0x06006CAF RID: 27823 RVA: 0x0018F91C File Offset: 0x0018DB1C
			public static implicit operator DateTime(NativeMethods.SYSTEMTIME sysTime)
			{
				if (sysTime.wYear <= 0 || sysTime.wMonth <= 0 || sysTime.wDay <= 0)
				{
					return DateTime.MinValue;
				}
				return new DateTime((int)sysTime.wYear, (int)sysTime.wMonth, (int)sysTime.wDay, (int)sysTime.wHour, (int)sysTime.wMinute, (int)sysTime.wSecond, (int)sysTime.wMilliseconds);
			}

			// Token: 0x06006CB0 RID: 27824 RVA: 0x0018F97C File Offset: 0x0018DB7C
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					"[SYSTEMTIME: ",
					this.wDay.ToString(CultureInfo.InvariantCulture),
					"/",
					this.wMonth.ToString(CultureInfo.InvariantCulture),
					"/",
					this.wYear.ToString(CultureInfo.InvariantCulture),
					" ",
					this.wHour.ToString(CultureInfo.InvariantCulture),
					":",
					this.wMinute.ToString(CultureInfo.InvariantCulture),
					":",
					this.wSecond.ToString(CultureInfo.InvariantCulture),
					"]"
				});
			}

			// Token: 0x04003DDD RID: 15837
			public short wYear;

			// Token: 0x04003DDE RID: 15838
			public short wMonth;

			// Token: 0x04003DDF RID: 15839
			public short wDayOfWeek;

			// Token: 0x04003DE0 RID: 15840
			public short wDay;

			// Token: 0x04003DE1 RID: 15841
			public short wHour;

			// Token: 0x04003DE2 RID: 15842
			public short wMinute;

			// Token: 0x04003DE3 RID: 15843
			public short wSecond;

			// Token: 0x04003DE4 RID: 15844
			public short wMilliseconds;
		}

		// Token: 0x02000734 RID: 1844
		[CLSCompliant(false)]
		[StructLayout(LayoutKind.Sequential)]
		public sealed class _POINTL
		{
			// Token: 0x04003DE5 RID: 15845
			public int x;

			// Token: 0x04003DE6 RID: 15846
			public int y;
		}

		// Token: 0x02000735 RID: 1845
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagSIZE
		{
			// Token: 0x04003DE7 RID: 15847
			public int cx;

			// Token: 0x04003DE8 RID: 15848
			public int cy;
		}

		// Token: 0x02000736 RID: 1846
		[StructLayout(LayoutKind.Sequential)]
		public class COMRECT
		{
			// Token: 0x06006CB4 RID: 27828 RVA: 0x00002843 File Offset: 0x00000A43
			public COMRECT()
			{
			}

			// Token: 0x06006CB5 RID: 27829 RVA: 0x0018FA43 File Offset: 0x0018DC43
			public COMRECT(Rectangle r)
			{
				this.left = r.X;
				this.top = r.Y;
				this.right = r.Right;
				this.bottom = r.Bottom;
			}

			// Token: 0x06006CB6 RID: 27830 RVA: 0x0018FA7F File Offset: 0x0018DC7F
			public COMRECT(int left, int top, int right, int bottom)
			{
				this.left = left;
				this.top = top;
				this.right = right;
				this.bottom = bottom;
			}

			// Token: 0x06006CB7 RID: 27831 RVA: 0x0018FAA4 File Offset: 0x0018DCA4
			public static NativeMethods.COMRECT FromXYWH(int x, int y, int width, int height)
			{
				return new NativeMethods.COMRECT(x, y, x + width, y + height);
			}

			// Token: 0x06006CB8 RID: 27832 RVA: 0x0018FAB4 File Offset: 0x0018DCB4
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					"Left = ",
					this.left.ToString(),
					" Top ",
					this.top.ToString(),
					" Right = ",
					this.right.ToString(),
					" Bottom = ",
					this.bottom.ToString()
				});
			}

			// Token: 0x04003DE9 RID: 15849
			public int left;

			// Token: 0x04003DEA RID: 15850
			public int top;

			// Token: 0x04003DEB RID: 15851
			public int right;

			// Token: 0x04003DEC RID: 15852
			public int bottom;
		}

		// Token: 0x02000737 RID: 1847
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagOleMenuGroupWidths
		{
			// Token: 0x04003DED RID: 15853
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
			public int[] widths = new int[6];
		}

		// Token: 0x02000738 RID: 1848
		[Serializable]
		[StructLayout(LayoutKind.Sequential)]
		public class MSOCRINFOSTRUCT
		{
			// Token: 0x04003DEE RID: 15854
			public int cbSize = Marshal.SizeOf(typeof(NativeMethods.MSOCRINFOSTRUCT));

			// Token: 0x04003DEF RID: 15855
			public int uIdleTimeInterval;

			// Token: 0x04003DF0 RID: 15856
			public int grfcrf;

			// Token: 0x04003DF1 RID: 15857
			public int grfcadvf;
		}

		// Token: 0x02000739 RID: 1849
		public struct NMLISTVIEW
		{
			// Token: 0x04003DF2 RID: 15858
			public NativeMethods.NMHDR hdr;

			// Token: 0x04003DF3 RID: 15859
			public int iItem;

			// Token: 0x04003DF4 RID: 15860
			public int iSubItem;

			// Token: 0x04003DF5 RID: 15861
			public int uNewState;

			// Token: 0x04003DF6 RID: 15862
			public int uOldState;

			// Token: 0x04003DF7 RID: 15863
			public int uChanged;

			// Token: 0x04003DF8 RID: 15864
			public IntPtr lParam;
		}

		// Token: 0x0200073A RID: 1850
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagPOINTF
		{
			// Token: 0x04003DF9 RID: 15865
			[MarshalAs(UnmanagedType.R4)]
			public float x;

			// Token: 0x04003DFA RID: 15866
			[MarshalAs(UnmanagedType.R4)]
			public float y;
		}

		// Token: 0x0200073B RID: 1851
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagOIFI
		{
			// Token: 0x04003DFB RID: 15867
			[MarshalAs(UnmanagedType.U4)]
			public int cb;

			// Token: 0x04003DFC RID: 15868
			public bool fMDIApp;

			// Token: 0x04003DFD RID: 15869
			public IntPtr hwndFrame;

			// Token: 0x04003DFE RID: 15870
			public IntPtr hAccel;

			// Token: 0x04003DFF RID: 15871
			[MarshalAs(UnmanagedType.U4)]
			public int cAccelEntries;
		}

		// Token: 0x0200073C RID: 1852
		public struct NMLVFINDITEM
		{
			// Token: 0x04003E00 RID: 15872
			public NativeMethods.NMHDR hdr;

			// Token: 0x04003E01 RID: 15873
			public int iStart;

			// Token: 0x04003E02 RID: 15874
			public NativeMethods.LVFINDINFO lvfi;
		}

		// Token: 0x0200073D RID: 1853
		public struct NMHDR
		{
			// Token: 0x04003E03 RID: 15875
			public IntPtr hwndFrom;

			// Token: 0x04003E04 RID: 15876
			public IntPtr idFrom;

			// Token: 0x04003E05 RID: 15877
			public int code;
		}

		// Token: 0x0200073E RID: 1854
		[ComVisible(true)]
		[Guid("626FC520-A41E-11CF-A731-00A0C9082637")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLDocument
		{
			// Token: 0x06006CBD RID: 27837
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetScript();
		}

		// Token: 0x0200073F RID: 1855
		[Guid("376BD3AA-3845-101B-84ED-08002B2EC713")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IPerPropertyBrowsing
		{
			// Token: 0x06006CBE RID: 27838
			[PreserveSig]
			int GetDisplayString(int dispID, [MarshalAs(UnmanagedType.LPArray)] [Out] string[] pBstr);

			// Token: 0x06006CBF RID: 27839
			[PreserveSig]
			int MapPropertyToPage(int dispID, out Guid pGuid);

			// Token: 0x06006CC0 RID: 27840
			[PreserveSig]
			int GetPredefinedStrings(int dispID, [Out] NativeMethods.CA_STRUCT pCaStringsOut, [Out] NativeMethods.CA_STRUCT pCaCookiesOut);

			// Token: 0x06006CC1 RID: 27841
			[PreserveSig]
			int GetPredefinedValue(int dispID, [MarshalAs(UnmanagedType.U4)] [In] int dwCookie, [Out] NativeMethods.VARIANT pVarOut);
		}

		// Token: 0x02000740 RID: 1856
		[Guid("4D07FC10-F931-11CE-B001-00AA006884E5")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface ICategorizeProperties
		{
			// Token: 0x06006CC2 RID: 27842
			[PreserveSig]
			int MapPropertyToCategory(int dispID, ref int categoryID);

			// Token: 0x06006CC3 RID: 27843
			[PreserveSig]
			int GetCategoryName(int propcat, [MarshalAs(UnmanagedType.U4)] [In] int lcid, out string categoryName);
		}

		// Token: 0x02000741 RID: 1857
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagSIZEL
		{
			// Token: 0x04003E06 RID: 15878
			public int cx;

			// Token: 0x04003E07 RID: 15879
			public int cy;
		}

		// Token: 0x02000742 RID: 1858
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagOLEVERB
		{
			// Token: 0x04003E08 RID: 15880
			public int lVerb;

			// Token: 0x04003E09 RID: 15881
			[MarshalAs(UnmanagedType.LPWStr)]
			public string lpszVerbName;

			// Token: 0x04003E0A RID: 15882
			[MarshalAs(UnmanagedType.U4)]
			public int fuFlags;

			// Token: 0x04003E0B RID: 15883
			[MarshalAs(UnmanagedType.U4)]
			public int grfAttribs;
		}

		// Token: 0x02000743 RID: 1859
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagLOGPALETTE
		{
			// Token: 0x04003E0C RID: 15884
			[MarshalAs(UnmanagedType.U2)]
			public short palVersion;

			// Token: 0x04003E0D RID: 15885
			[MarshalAs(UnmanagedType.U2)]
			public short palNumEntries;
		}

		// Token: 0x02000744 RID: 1860
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagCONTROLINFO
		{
			// Token: 0x04003E0E RID: 15886
			[MarshalAs(UnmanagedType.U4)]
			public int cb = Marshal.SizeOf(typeof(NativeMethods.tagCONTROLINFO));

			// Token: 0x04003E0F RID: 15887
			public IntPtr hAccel;

			// Token: 0x04003E10 RID: 15888
			[MarshalAs(UnmanagedType.U2)]
			public short cAccel;

			// Token: 0x04003E11 RID: 15889
			[MarshalAs(UnmanagedType.U4)]
			public int dwFlags;
		}

		// Token: 0x02000745 RID: 1861
		[StructLayout(LayoutKind.Sequential)]
		public sealed class CA_STRUCT
		{
			// Token: 0x04003E12 RID: 15890
			public int cElems;

			// Token: 0x04003E13 RID: 15891
			public IntPtr pElems = IntPtr.Zero;
		}

		// Token: 0x02000746 RID: 1862
		[StructLayout(LayoutKind.Sequential)]
		public sealed class VARIANT
		{
			// Token: 0x170017E2 RID: 6114
			// (get) Token: 0x06006CC9 RID: 27849 RVA: 0x0018FB85 File Offset: 0x0018DD85
			public bool Byref
			{
				get
				{
					return (this.vt & 16384) != 0;
				}
			}

			// Token: 0x06006CCA RID: 27850 RVA: 0x0018FB98 File Offset: 0x0018DD98
			public void Clear()
			{
				if ((this.vt == 13 || this.vt == 9) && this.data1 != IntPtr.Zero)
				{
					Marshal.Release(this.data1);
				}
				if (this.vt == 8 && this.data1 != IntPtr.Zero)
				{
					NativeMethods.VARIANT.SysFreeString(this.data1);
				}
				this.data1 = (this.data2 = IntPtr.Zero);
				this.vt = 0;
			}

			// Token: 0x06006CCB RID: 27851 RVA: 0x0018FC18 File Offset: 0x0018DE18
			~VARIANT()
			{
				this.Clear();
			}

			// Token: 0x06006CCC RID: 27852 RVA: 0x0018FC44 File Offset: 0x0018DE44
			public static NativeMethods.VARIANT FromObject(object var)
			{
				NativeMethods.VARIANT variant = new NativeMethods.VARIANT();
				if (var == null)
				{
					variant.vt = 0;
				}
				else if (!Convert.IsDBNull(var))
				{
					Type type = var.GetType();
					if (type == typeof(bool))
					{
						variant.vt = 11;
					}
					else if (type == typeof(byte))
					{
						variant.vt = 17;
						variant.data1 = (IntPtr)((int)Convert.ToByte(var, CultureInfo.InvariantCulture));
					}
					else if (type == typeof(char))
					{
						variant.vt = 18;
						variant.data1 = (IntPtr)((int)Convert.ToChar(var, CultureInfo.InvariantCulture));
					}
					else if (type == typeof(string))
					{
						variant.vt = 8;
						variant.data1 = NativeMethods.VARIANT.SysAllocString(Convert.ToString(var, CultureInfo.InvariantCulture));
					}
					else if (type == typeof(short))
					{
						variant.vt = 2;
						variant.data1 = (IntPtr)((int)Convert.ToInt16(var, CultureInfo.InvariantCulture));
					}
					else if (type == typeof(int))
					{
						variant.vt = 3;
						variant.data1 = (IntPtr)Convert.ToInt32(var, CultureInfo.InvariantCulture);
					}
					else if (type == typeof(long))
					{
						variant.vt = 20;
						variant.SetLong(Convert.ToInt64(var, CultureInfo.InvariantCulture));
					}
					else if (type == typeof(decimal))
					{
						variant.vt = 6;
						decimal num = (decimal)var;
						variant.SetLong(decimal.ToInt64(num));
					}
					else if (type == typeof(decimal))
					{
						variant.vt = 14;
						decimal num2 = Convert.ToDecimal(var, CultureInfo.InvariantCulture);
						variant.SetLong(decimal.ToInt64(num2));
					}
					else if (type == typeof(double))
					{
						variant.vt = 5;
					}
					else if (type == typeof(float) || type == typeof(float))
					{
						variant.vt = 4;
					}
					else if (type == typeof(DateTime))
					{
						variant.vt = 7;
						variant.SetLong(Convert.ToDateTime(var, CultureInfo.InvariantCulture).ToFileTime());
					}
					else if (type == typeof(sbyte))
					{
						variant.vt = 16;
						variant.data1 = (IntPtr)((int)Convert.ToSByte(var, CultureInfo.InvariantCulture));
					}
					else if (type == typeof(ushort))
					{
						variant.vt = 18;
						variant.data1 = (IntPtr)((int)Convert.ToUInt16(var, CultureInfo.InvariantCulture));
					}
					else if (type == typeof(uint))
					{
						variant.vt = 19;
						variant.data1 = (IntPtr)((long)((ulong)Convert.ToUInt32(var, CultureInfo.InvariantCulture)));
					}
					else if (type == typeof(ulong))
					{
						variant.vt = 21;
						variant.SetLong((long)Convert.ToUInt64(var, CultureInfo.InvariantCulture));
					}
					else
					{
						if (!(type == typeof(object)) && !(type == typeof(UnsafeNativeMethods.IDispatch)) && !type.IsCOMObject)
						{
							throw new ArgumentException(SR.GetString("ConnPointUnhandledType", new object[] { type.Name }));
						}
						variant.vt = ((type == typeof(UnsafeNativeMethods.IDispatch)) ? 9 : 13);
						variant.data1 = Marshal.GetIUnknownForObject(var);
					}
				}
				return variant;
			}

			// Token: 0x06006CCD RID: 27853
			[DllImport("oleaut32.dll", CharSet = CharSet.Auto)]
			private static extern IntPtr SysAllocString([MarshalAs(UnmanagedType.LPWStr)] [In] string s);

			// Token: 0x06006CCE RID: 27854
			[DllImport("oleaut32.dll", CharSet = CharSet.Auto)]
			private static extern void SysFreeString(IntPtr pbstr);

			// Token: 0x06006CCF RID: 27855 RVA: 0x00190000 File Offset: 0x0018E200
			public void SetLong(long lVal)
			{
				this.data1 = (IntPtr)(lVal & (long)((ulong)(-1)));
				this.data2 = (IntPtr)((lVal >> 32) & (long)((ulong)(-1)));
			}

			// Token: 0x06006CD0 RID: 27856 RVA: 0x00190024 File Offset: 0x0018E224
			public IntPtr ToCoTaskMemPtr()
			{
				IntPtr intPtr = Marshal.AllocCoTaskMem(16);
				Marshal.WriteInt16(intPtr, this.vt);
				Marshal.WriteInt16(intPtr, 2, this.reserved1);
				Marshal.WriteInt16(intPtr, 4, this.reserved2);
				Marshal.WriteInt16(intPtr, 6, this.reserved3);
				Marshal.WriteInt32(intPtr, 8, (int)this.data1);
				Marshal.WriteInt32(intPtr, 12, (int)this.data2);
				return intPtr;
			}

			// Token: 0x170017E3 RID: 6115
			// (get) Token: 0x06006CD1 RID: 27857 RVA: 0x00190092 File Offset: 0x0018E292
			private static bool Is64bitProcess
			{
				get
				{
					return IntPtr.Size == 8;
				}
			}

			// Token: 0x06006CD2 RID: 27858 RVA: 0x0019009C File Offset: 0x0018E29C
			private static int Lo32(IntPtr val)
			{
				if (NativeMethods.VARIANT.Is64bitProcess)
				{
					return (int)(long)val;
				}
				return (int)val;
			}

			// Token: 0x06006CD3 RID: 27859 RVA: 0x001900B3 File Offset: 0x0018E2B3
			private static IntPtr Lo32AsIntPtr(IntPtr val)
			{
				if (NativeMethods.VARIANT.Is64bitProcess)
				{
					return new IntPtr(NativeMethods.VARIANT.Lo32(val));
				}
				return val;
			}

			// Token: 0x06006CD4 RID: 27860 RVA: 0x001900C9 File Offset: 0x0018E2C9
			private static IntPtr GetRefInt32(IntPtr val)
			{
				if (NativeMethods.VARIANT.Is64bitProcess)
				{
					return (IntPtr)Marshal.ReadInt32(val);
				}
				return NativeMethods.VARIANT.GetRefInt(val);
			}

			// Token: 0x06006CD5 RID: 27861 RVA: 0x001900E4 File Offset: 0x0018E2E4
			private static long Lo64(IntPtr data1, IntPtr data2)
			{
				if (NativeMethods.VARIANT.Is64bitProcess)
				{
					long num = (long)data1;
					IntPtr intPtr = new IntPtr((int)num);
					IntPtr intPtr2 = new IntPtr((int)(num >> 32));
					return (long)((ulong)(((int)intPtr & -1) | (int)intPtr2));
				}
				return (long)((ulong)(((int)data1 & -1) | (int)data2));
			}

			// Token: 0x06006CD6 RID: 27862 RVA: 0x00190138 File Offset: 0x0018E338
			public object ToObject()
			{
				IntPtr intPtr = this.data1;
				int num = (int)(this.vt & 4095);
				switch (num)
				{
				case 0:
					return null;
				case 1:
					return Convert.DBNull;
				case 2:
					if (this.Byref)
					{
						intPtr = (IntPtr)((int)Marshal.ReadInt16(intPtr));
					}
					return (short)(65535 & (int)((short)NativeMethods.VARIANT.Lo32(intPtr)));
				case 3:
					break;
				default:
					switch (num)
					{
					case 16:
						if (this.Byref)
						{
							intPtr = (IntPtr)((int)Marshal.ReadByte(intPtr));
						}
						return (sbyte)(255 & (int)((sbyte)NativeMethods.VARIANT.Lo32(intPtr)));
					case 17:
						if (this.Byref)
						{
							intPtr = (IntPtr)((int)Marshal.ReadByte(intPtr));
						}
						return byte.MaxValue & (byte)NativeMethods.VARIANT.Lo32(intPtr);
					case 18:
						if (this.Byref)
						{
							intPtr = (IntPtr)((int)Marshal.ReadInt16(intPtr));
						}
						return ushort.MaxValue & (ushort)NativeMethods.VARIANT.Lo32(intPtr);
					case 19:
					case 23:
						if (this.Byref)
						{
							intPtr = (IntPtr)Marshal.ReadInt32(intPtr);
						}
						return (uint)NativeMethods.VARIANT.Lo32(intPtr);
					case 20:
					case 21:
					{
						long num2;
						if (this.Byref)
						{
							num2 = Marshal.ReadInt64(intPtr);
						}
						else
						{
							num2 = NativeMethods.VARIANT.Lo64(this.data1, this.data2);
						}
						if (this.vt == 20)
						{
							return num2;
						}
						return (ulong)num2;
					}
					case 22:
						break;
					default:
					{
						if (this.Byref)
						{
							IntPtr refInt = NativeMethods.VARIANT.GetRefInt32(intPtr);
						}
						if (num <= 72)
						{
							switch (num)
							{
							case 4:
							case 5:
								throw new FormatException(SR.GetString("CannotConvertIntToFloat"));
							case 6:
							{
								long num2 = NativeMethods.VARIANT.Lo64(this.data1, this.data2);
								return new decimal(num2);
							}
							case 7:
								throw new FormatException(SR.GetString("CannotConvertDoubleToDate"));
							case 8:
							case 31:
								if (this.Byref)
								{
									intPtr = NativeMethods.VARIANT.GetRefInt(intPtr);
								}
								return Marshal.PtrToStringUni(intPtr);
							case 9:
							case 13:
								if (this.Byref)
								{
									intPtr = NativeMethods.VARIANT.GetRefInt(intPtr);
								}
								return Marshal.GetObjectForIUnknown(intPtr);
							case 10:
							case 15:
							case 16:
							case 17:
							case 18:
							case 19:
							case 20:
							case 21:
							case 22:
							case 23:
							case 24:
							case 26:
							case 27:
							case 28:
							case 32:
							case 33:
							case 34:
							case 35:
							case 36:
								break;
							case 11:
								if (this.Byref)
								{
									intPtr = NativeMethods.VARIANT.GetRefInt32(intPtr);
								}
								return NativeMethods.VARIANT.Lo32AsIntPtr(intPtr) != IntPtr.Zero;
							case 12:
							{
								if (this.Byref)
								{
									intPtr = NativeMethods.VARIANT.GetRefInt(intPtr);
								}
								NativeMethods.VARIANT variant = (NativeMethods.VARIANT)UnsafeNativeMethods.PtrToStructure(intPtr, typeof(NativeMethods.VARIANT));
								return variant.ToObject();
							}
							case 14:
							{
								long num2 = NativeMethods.VARIANT.Lo64(this.data1, this.data2);
								return new decimal(num2);
							}
							case 25:
								if (this.Byref)
								{
									intPtr = NativeMethods.VARIANT.GetRefInt32(intPtr);
								}
								return NativeMethods.VARIANT.Lo32AsIntPtr(intPtr);
							case 29:
								throw new ArgumentException(SR.GetString("COM2UnhandledVT", new object[] { "VT_USERDEFINED" }));
							case 30:
								if (this.Byref)
								{
									intPtr = NativeMethods.VARIANT.GetRefInt(intPtr);
								}
								return Marshal.PtrToStringAnsi(intPtr);
							default:
								switch (num)
								{
								case 64:
								{
									long num2 = NativeMethods.VARIANT.Lo64(this.data1, this.data2);
									return new DateTime(num2);
								}
								case 72:
								{
									if (this.Byref)
									{
										intPtr = NativeMethods.VARIANT.GetRefInt(intPtr);
									}
									Guid guid = (Guid)UnsafeNativeMethods.PtrToStructure(intPtr, typeof(Guid));
									return guid;
								}
								}
								break;
							}
						}
						else if (num - 4095 > 1 && num != 8192 && num != 16384)
						{
						}
						int num3 = (int)this.vt;
						throw new ArgumentException(SR.GetString("COM2UnhandledVT", new object[] { num3.ToString(CultureInfo.InvariantCulture) }));
					}
					}
					break;
				}
				if (this.Byref)
				{
					intPtr = (IntPtr)Marshal.ReadInt32(intPtr);
				}
				return NativeMethods.VARIANT.Lo32(intPtr);
			}

			// Token: 0x06006CD7 RID: 27863 RVA: 0x0019055F File Offset: 0x0018E75F
			private static IntPtr GetRefInt(IntPtr value)
			{
				return Marshal.ReadIntPtr(value);
			}

			// Token: 0x04003E14 RID: 15892
			[MarshalAs(UnmanagedType.I2)]
			public short vt;

			// Token: 0x04003E15 RID: 15893
			[MarshalAs(UnmanagedType.I2)]
			public short reserved1;

			// Token: 0x04003E16 RID: 15894
			[MarshalAs(UnmanagedType.I2)]
			public short reserved2;

			// Token: 0x04003E17 RID: 15895
			[MarshalAs(UnmanagedType.I2)]
			public short reserved3;

			// Token: 0x04003E18 RID: 15896
			public IntPtr data1;

			// Token: 0x04003E19 RID: 15897
			public IntPtr data2;
		}

		// Token: 0x02000747 RID: 1863
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagLICINFO
		{
			// Token: 0x04003E1A RID: 15898
			[MarshalAs(UnmanagedType.U4)]
			public int cbLicInfo = Marshal.SizeOf(typeof(NativeMethods.tagLICINFO));

			// Token: 0x04003E1B RID: 15899
			public int fRuntimeAvailable;

			// Token: 0x04003E1C RID: 15900
			public int fLicVerified;
		}

		// Token: 0x02000748 RID: 1864
		public enum tagVT
		{
			// Token: 0x04003E1E RID: 15902
			VT_EMPTY,
			// Token: 0x04003E1F RID: 15903
			VT_NULL,
			// Token: 0x04003E20 RID: 15904
			VT_I2,
			// Token: 0x04003E21 RID: 15905
			VT_I4,
			// Token: 0x04003E22 RID: 15906
			VT_R4,
			// Token: 0x04003E23 RID: 15907
			VT_R8,
			// Token: 0x04003E24 RID: 15908
			VT_CY,
			// Token: 0x04003E25 RID: 15909
			VT_DATE,
			// Token: 0x04003E26 RID: 15910
			VT_BSTR,
			// Token: 0x04003E27 RID: 15911
			VT_DISPATCH,
			// Token: 0x04003E28 RID: 15912
			VT_ERROR,
			// Token: 0x04003E29 RID: 15913
			VT_BOOL,
			// Token: 0x04003E2A RID: 15914
			VT_VARIANT,
			// Token: 0x04003E2B RID: 15915
			VT_UNKNOWN,
			// Token: 0x04003E2C RID: 15916
			VT_DECIMAL,
			// Token: 0x04003E2D RID: 15917
			VT_I1 = 16,
			// Token: 0x04003E2E RID: 15918
			VT_UI1,
			// Token: 0x04003E2F RID: 15919
			VT_UI2,
			// Token: 0x04003E30 RID: 15920
			VT_UI4,
			// Token: 0x04003E31 RID: 15921
			VT_I8,
			// Token: 0x04003E32 RID: 15922
			VT_UI8,
			// Token: 0x04003E33 RID: 15923
			VT_INT,
			// Token: 0x04003E34 RID: 15924
			VT_UINT,
			// Token: 0x04003E35 RID: 15925
			VT_VOID,
			// Token: 0x04003E36 RID: 15926
			VT_HRESULT,
			// Token: 0x04003E37 RID: 15927
			VT_PTR,
			// Token: 0x04003E38 RID: 15928
			VT_SAFEARRAY,
			// Token: 0x04003E39 RID: 15929
			VT_CARRAY,
			// Token: 0x04003E3A RID: 15930
			VT_USERDEFINED,
			// Token: 0x04003E3B RID: 15931
			VT_LPSTR,
			// Token: 0x04003E3C RID: 15932
			VT_LPWSTR,
			// Token: 0x04003E3D RID: 15933
			VT_RECORD = 36,
			// Token: 0x04003E3E RID: 15934
			VT_FILETIME = 64,
			// Token: 0x04003E3F RID: 15935
			VT_BLOB,
			// Token: 0x04003E40 RID: 15936
			VT_STREAM,
			// Token: 0x04003E41 RID: 15937
			VT_STORAGE,
			// Token: 0x04003E42 RID: 15938
			VT_STREAMED_OBJECT,
			// Token: 0x04003E43 RID: 15939
			VT_STORED_OBJECT,
			// Token: 0x04003E44 RID: 15940
			VT_BLOB_OBJECT,
			// Token: 0x04003E45 RID: 15941
			VT_CF,
			// Token: 0x04003E46 RID: 15942
			VT_CLSID,
			// Token: 0x04003E47 RID: 15943
			VT_BSTR_BLOB = 4095,
			// Token: 0x04003E48 RID: 15944
			VT_VECTOR,
			// Token: 0x04003E49 RID: 15945
			VT_ARRAY = 8192,
			// Token: 0x04003E4A RID: 15946
			VT_BYREF = 16384,
			// Token: 0x04003E4B RID: 15947
			VT_RESERVED = 32768,
			// Token: 0x04003E4C RID: 15948
			VT_ILLEGAL = 65535,
			// Token: 0x04003E4D RID: 15949
			VT_ILLEGALMASKED = 4095,
			// Token: 0x04003E4E RID: 15950
			VT_TYPEMASK = 4095
		}

		// Token: 0x02000749 RID: 1865
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class WNDCLASS_D
		{
			// Token: 0x04003E4F RID: 15951
			public int style;

			// Token: 0x04003E50 RID: 15952
			public NativeMethods.WndProc lpfnWndProc;

			// Token: 0x04003E51 RID: 15953
			public int cbClsExtra;

			// Token: 0x04003E52 RID: 15954
			public int cbWndExtra;

			// Token: 0x04003E53 RID: 15955
			public IntPtr hInstance = IntPtr.Zero;

			// Token: 0x04003E54 RID: 15956
			public IntPtr hIcon = IntPtr.Zero;

			// Token: 0x04003E55 RID: 15957
			public IntPtr hCursor = IntPtr.Zero;

			// Token: 0x04003E56 RID: 15958
			public IntPtr hbrBackground = IntPtr.Zero;

			// Token: 0x04003E57 RID: 15959
			public string lpszMenuName;

			// Token: 0x04003E58 RID: 15960
			public string lpszClassName;
		}

		// Token: 0x0200074A RID: 1866
		public class MSOCM
		{
			// Token: 0x04003E59 RID: 15961
			public const int msocrfNeedIdleTime = 1;

			// Token: 0x04003E5A RID: 15962
			public const int msocrfNeedPeriodicIdleTime = 2;

			// Token: 0x04003E5B RID: 15963
			public const int msocrfPreTranslateKeys = 4;

			// Token: 0x04003E5C RID: 15964
			public const int msocrfPreTranslateAll = 8;

			// Token: 0x04003E5D RID: 15965
			public const int msocrfNeedSpecActiveNotifs = 16;

			// Token: 0x04003E5E RID: 15966
			public const int msocrfNeedAllActiveNotifs = 32;

			// Token: 0x04003E5F RID: 15967
			public const int msocrfExclusiveBorderSpace = 64;

			// Token: 0x04003E60 RID: 15968
			public const int msocrfExclusiveActivation = 128;

			// Token: 0x04003E61 RID: 15969
			public const int msocrfNeedAllMacEvents = 256;

			// Token: 0x04003E62 RID: 15970
			public const int msocrfMaster = 512;

			// Token: 0x04003E63 RID: 15971
			public const int msocadvfModal = 1;

			// Token: 0x04003E64 RID: 15972
			public const int msocadvfRedrawOff = 2;

			// Token: 0x04003E65 RID: 15973
			public const int msocadvfWarningsOff = 4;

			// Token: 0x04003E66 RID: 15974
			public const int msocadvfRecording = 8;

			// Token: 0x04003E67 RID: 15975
			public const int msochostfExclusiveBorderSpace = 1;

			// Token: 0x04003E68 RID: 15976
			public const int msoidlefPeriodic = 1;

			// Token: 0x04003E69 RID: 15977
			public const int msoidlefNonPeriodic = 2;

			// Token: 0x04003E6A RID: 15978
			public const int msoidlefPriority = 4;

			// Token: 0x04003E6B RID: 15979
			public const int msoidlefAll = -1;

			// Token: 0x04003E6C RID: 15980
			public const int msoloopDoEventsModal = -2;

			// Token: 0x04003E6D RID: 15981
			public const int msoloopMain = -1;

			// Token: 0x04003E6E RID: 15982
			public const int msoloopFocusWait = 1;

			// Token: 0x04003E6F RID: 15983
			public const int msoloopDoEvents = 2;

			// Token: 0x04003E70 RID: 15984
			public const int msoloopDebug = 3;

			// Token: 0x04003E71 RID: 15985
			public const int msoloopModalForm = 4;

			// Token: 0x04003E72 RID: 15986
			public const int msoloopModalAlert = 5;

			// Token: 0x04003E73 RID: 15987
			public const int msocstateModal = 1;

			// Token: 0x04003E74 RID: 15988
			public const int msocstateRedrawOff = 2;

			// Token: 0x04003E75 RID: 15989
			public const int msocstateWarningsOff = 3;

			// Token: 0x04003E76 RID: 15990
			public const int msocstateRecording = 4;

			// Token: 0x04003E77 RID: 15991
			public const int msoccontextAll = 0;

			// Token: 0x04003E78 RID: 15992
			public const int msoccontextMine = 1;

			// Token: 0x04003E79 RID: 15993
			public const int msoccontextOthers = 2;

			// Token: 0x04003E7A RID: 15994
			public const int msogacActive = 0;

			// Token: 0x04003E7B RID: 15995
			public const int msogacTracking = 1;

			// Token: 0x04003E7C RID: 15996
			public const int msogacTrackingOrActive = 2;

			// Token: 0x04003E7D RID: 15997
			public const int msocWindowFrameToplevel = 0;

			// Token: 0x04003E7E RID: 15998
			public const int msocWindowFrameOwner = 1;

			// Token: 0x04003E7F RID: 15999
			public const int msocWindowComponent = 2;

			// Token: 0x04003E80 RID: 16000
			public const int msocWindowDlgOwner = 3;
		}

		// Token: 0x0200074B RID: 1867
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class TOOLINFO_T
		{
			// Token: 0x04003E81 RID: 16001
			public int cbSize = Marshal.SizeOf(typeof(NativeMethods.TOOLINFO_T));

			// Token: 0x04003E82 RID: 16002
			public int uFlags;

			// Token: 0x04003E83 RID: 16003
			public IntPtr hwnd;

			// Token: 0x04003E84 RID: 16004
			public IntPtr uId;

			// Token: 0x04003E85 RID: 16005
			public NativeMethods.RECT rect;

			// Token: 0x04003E86 RID: 16006
			public IntPtr hinst = IntPtr.Zero;

			// Token: 0x04003E87 RID: 16007
			public string lpszText;

			// Token: 0x04003E88 RID: 16008
			public IntPtr lParam = IntPtr.Zero;
		}

		// Token: 0x0200074C RID: 1868
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class TOOLINFO_TOOLTIP
		{
			// Token: 0x04003E89 RID: 16009
			public int cbSize = Marshal.SizeOf(typeof(NativeMethods.TOOLINFO_TOOLTIP));

			// Token: 0x04003E8A RID: 16010
			public int uFlags;

			// Token: 0x04003E8B RID: 16011
			public IntPtr hwnd;

			// Token: 0x04003E8C RID: 16012
			public IntPtr uId;

			// Token: 0x04003E8D RID: 16013
			public NativeMethods.RECT rect;

			// Token: 0x04003E8E RID: 16014
			public IntPtr hinst = IntPtr.Zero;

			// Token: 0x04003E8F RID: 16015
			public IntPtr lpszText;

			// Token: 0x04003E90 RID: 16016
			public IntPtr lParam = IntPtr.Zero;
		}

		// Token: 0x0200074D RID: 1869
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagDVTARGETDEVICE
		{
			// Token: 0x04003E91 RID: 16017
			[MarshalAs(UnmanagedType.U4)]
			public int tdSize;

			// Token: 0x04003E92 RID: 16018
			[MarshalAs(UnmanagedType.U2)]
			public short tdDriverNameOffset;

			// Token: 0x04003E93 RID: 16019
			[MarshalAs(UnmanagedType.U2)]
			public short tdDeviceNameOffset;

			// Token: 0x04003E94 RID: 16020
			[MarshalAs(UnmanagedType.U2)]
			public short tdPortNameOffset;

			// Token: 0x04003E95 RID: 16021
			[MarshalAs(UnmanagedType.U2)]
			public short tdExtDevmodeOffset;
		}

		// Token: 0x0200074E RID: 1870
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct TV_ITEM
		{
			// Token: 0x04003E96 RID: 16022
			public int mask;

			// Token: 0x04003E97 RID: 16023
			public IntPtr hItem;

			// Token: 0x04003E98 RID: 16024
			public int state;

			// Token: 0x04003E99 RID: 16025
			public int stateMask;

			// Token: 0x04003E9A RID: 16026
			public IntPtr pszText;

			// Token: 0x04003E9B RID: 16027
			public int cchTextMax;

			// Token: 0x04003E9C RID: 16028
			public int iImage;

			// Token: 0x04003E9D RID: 16029
			public int iSelectedImage;

			// Token: 0x04003E9E RID: 16030
			public int cChildren;

			// Token: 0x04003E9F RID: 16031
			public IntPtr lParam;
		}

		// Token: 0x0200074F RID: 1871
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct TVSORTCB
		{
			// Token: 0x04003EA0 RID: 16032
			public IntPtr hParent;

			// Token: 0x04003EA1 RID: 16033
			public NativeMethods.TreeViewCompareCallback lpfnCompare;

			// Token: 0x04003EA2 RID: 16034
			public IntPtr lParam;
		}

		// Token: 0x02000750 RID: 1872
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct TV_INSERTSTRUCT
		{
			// Token: 0x04003EA3 RID: 16035
			public IntPtr hParent;

			// Token: 0x04003EA4 RID: 16036
			public IntPtr hInsertAfter;

			// Token: 0x04003EA5 RID: 16037
			public int item_mask;

			// Token: 0x04003EA6 RID: 16038
			public IntPtr item_hItem;

			// Token: 0x04003EA7 RID: 16039
			public int item_state;

			// Token: 0x04003EA8 RID: 16040
			public int item_stateMask;

			// Token: 0x04003EA9 RID: 16041
			public IntPtr item_pszText;

			// Token: 0x04003EAA RID: 16042
			public int item_cchTextMax;

			// Token: 0x04003EAB RID: 16043
			public int item_iImage;

			// Token: 0x04003EAC RID: 16044
			public int item_iSelectedImage;

			// Token: 0x04003EAD RID: 16045
			public int item_cChildren;

			// Token: 0x04003EAE RID: 16046
			public IntPtr item_lParam;

			// Token: 0x04003EAF RID: 16047
			public int item_iIntegral;
		}

		// Token: 0x02000751 RID: 1873
		public struct NMTREEVIEW
		{
			// Token: 0x04003EB0 RID: 16048
			public NativeMethods.NMHDR nmhdr;

			// Token: 0x04003EB1 RID: 16049
			public int action;

			// Token: 0x04003EB2 RID: 16050
			public NativeMethods.TV_ITEM itemOld;

			// Token: 0x04003EB3 RID: 16051
			public NativeMethods.TV_ITEM itemNew;

			// Token: 0x04003EB4 RID: 16052
			public int ptDrag_X;

			// Token: 0x04003EB5 RID: 16053
			public int ptDrag_Y;
		}

		// Token: 0x02000752 RID: 1874
		public struct NMTVGETINFOTIP
		{
			// Token: 0x04003EB6 RID: 16054
			public NativeMethods.NMHDR nmhdr;

			// Token: 0x04003EB7 RID: 16055
			public string pszText;

			// Token: 0x04003EB8 RID: 16056
			public int cchTextMax;

			// Token: 0x04003EB9 RID: 16057
			public IntPtr item;

			// Token: 0x04003EBA RID: 16058
			public IntPtr lParam;
		}

		// Token: 0x02000753 RID: 1875
		[StructLayout(LayoutKind.Sequential)]
		public class NMTVDISPINFO
		{
			// Token: 0x04003EBB RID: 16059
			public NativeMethods.NMHDR hdr;

			// Token: 0x04003EBC RID: 16060
			public NativeMethods.TV_ITEM item;
		}

		// Token: 0x02000754 RID: 1876
		[StructLayout(LayoutKind.Sequential)]
		public sealed class POINTL
		{
			// Token: 0x04003EBD RID: 16061
			public int x;

			// Token: 0x04003EBE RID: 16062
			public int y;
		}

		// Token: 0x02000755 RID: 1877
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct HIGHCONTRAST
		{
			// Token: 0x04003EBF RID: 16063
			public int cbSize;

			// Token: 0x04003EC0 RID: 16064
			public int dwFlags;

			// Token: 0x04003EC1 RID: 16065
			public string lpszDefaultScheme;
		}

		// Token: 0x02000756 RID: 1878
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct HIGHCONTRAST_I
		{
			// Token: 0x04003EC2 RID: 16066
			public int cbSize;

			// Token: 0x04003EC3 RID: 16067
			public int dwFlags;

			// Token: 0x04003EC4 RID: 16068
			public IntPtr lpszDefaultScheme;
		}

		// Token: 0x02000757 RID: 1879
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class TCITEM_T
		{
			// Token: 0x04003EC5 RID: 16069
			public int mask;

			// Token: 0x04003EC6 RID: 16070
			public int dwState;

			// Token: 0x04003EC7 RID: 16071
			public int dwStateMask;

			// Token: 0x04003EC8 RID: 16072
			public string pszText;

			// Token: 0x04003EC9 RID: 16073
			public int cchTextMax;

			// Token: 0x04003ECA RID: 16074
			public int iImage;

			// Token: 0x04003ECB RID: 16075
			public IntPtr lParam;
		}

		// Token: 0x02000758 RID: 1880
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagDISPPARAMS
		{
			// Token: 0x04003ECC RID: 16076
			public IntPtr rgvarg;

			// Token: 0x04003ECD RID: 16077
			public IntPtr rgdispidNamedArgs;

			// Token: 0x04003ECE RID: 16078
			[MarshalAs(UnmanagedType.U4)]
			public int cArgs;

			// Token: 0x04003ECF RID: 16079
			[MarshalAs(UnmanagedType.U4)]
			public int cNamedArgs;
		}

		// Token: 0x02000759 RID: 1881
		public enum tagINVOKEKIND
		{
			// Token: 0x04003ED1 RID: 16081
			INVOKE_FUNC = 1,
			// Token: 0x04003ED2 RID: 16082
			INVOKE_PROPERTYGET,
			// Token: 0x04003ED3 RID: 16083
			INVOKE_PROPERTYPUT = 4,
			// Token: 0x04003ED4 RID: 16084
			INVOKE_PROPERTYPUTREF = 8
		}

		// Token: 0x0200075A RID: 1882
		[StructLayout(LayoutKind.Sequential)]
		public class tagEXCEPINFO
		{
			// Token: 0x04003ED5 RID: 16085
			[MarshalAs(UnmanagedType.U2)]
			public short wCode;

			// Token: 0x04003ED6 RID: 16086
			[MarshalAs(UnmanagedType.U2)]
			public short wReserved;

			// Token: 0x04003ED7 RID: 16087
			[MarshalAs(UnmanagedType.BStr)]
			public string bstrSource;

			// Token: 0x04003ED8 RID: 16088
			[MarshalAs(UnmanagedType.BStr)]
			public string bstrDescription;

			// Token: 0x04003ED9 RID: 16089
			[MarshalAs(UnmanagedType.BStr)]
			public string bstrHelpFile;

			// Token: 0x04003EDA RID: 16090
			[MarshalAs(UnmanagedType.U4)]
			public int dwHelpContext;

			// Token: 0x04003EDB RID: 16091
			public IntPtr pvReserved = IntPtr.Zero;

			// Token: 0x04003EDC RID: 16092
			public IntPtr pfnDeferredFillIn = IntPtr.Zero;

			// Token: 0x04003EDD RID: 16093
			[MarshalAs(UnmanagedType.U4)]
			public int scode;
		}

		// Token: 0x0200075B RID: 1883
		public enum tagDESCKIND
		{
			// Token: 0x04003EDF RID: 16095
			DESCKIND_NONE,
			// Token: 0x04003EE0 RID: 16096
			DESCKIND_FUNCDESC,
			// Token: 0x04003EE1 RID: 16097
			DESCKIND_VARDESC,
			// Token: 0x04003EE2 RID: 16098
			DESCKIND_TYPECOMP,
			// Token: 0x04003EE3 RID: 16099
			DESCKIND_IMPLICITAPPOBJ,
			// Token: 0x04003EE4 RID: 16100
			DESCKIND_MAX
		}

		// Token: 0x0200075C RID: 1884
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagFUNCDESC
		{
			// Token: 0x04003EE5 RID: 16101
			public int memid;

			// Token: 0x04003EE6 RID: 16102
			public IntPtr lprgscode = IntPtr.Zero;

			// Token: 0x04003EE7 RID: 16103
			public IntPtr lprgelemdescParam = IntPtr.Zero;

			// Token: 0x04003EE8 RID: 16104
			public int funckind;

			// Token: 0x04003EE9 RID: 16105
			public int invkind;

			// Token: 0x04003EEA RID: 16106
			public int callconv;

			// Token: 0x04003EEB RID: 16107
			[MarshalAs(UnmanagedType.I2)]
			public short cParams;

			// Token: 0x04003EEC RID: 16108
			[MarshalAs(UnmanagedType.I2)]
			public short cParamsOpt;

			// Token: 0x04003EED RID: 16109
			[MarshalAs(UnmanagedType.I2)]
			public short oVft;

			// Token: 0x04003EEE RID: 16110
			[MarshalAs(UnmanagedType.I2)]
			public short cScodesi;

			// Token: 0x04003EEF RID: 16111
			public NativeMethods.value_tagELEMDESC elemdescFunc;

			// Token: 0x04003EF0 RID: 16112
			[MarshalAs(UnmanagedType.U2)]
			public short wFuncFlags;
		}

		// Token: 0x0200075D RID: 1885
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagVARDESC
		{
			// Token: 0x04003EF1 RID: 16113
			public int memid;

			// Token: 0x04003EF2 RID: 16114
			public IntPtr lpstrSchema = IntPtr.Zero;

			// Token: 0x04003EF3 RID: 16115
			public IntPtr unionMember = IntPtr.Zero;

			// Token: 0x04003EF4 RID: 16116
			public NativeMethods.value_tagELEMDESC elemdescVar;

			// Token: 0x04003EF5 RID: 16117
			[MarshalAs(UnmanagedType.U2)]
			public short wVarFlags;

			// Token: 0x04003EF6 RID: 16118
			public int varkind;
		}

		// Token: 0x0200075E RID: 1886
		public struct value_tagELEMDESC
		{
			// Token: 0x04003EF7 RID: 16119
			public NativeMethods.tagTYPEDESC tdesc;

			// Token: 0x04003EF8 RID: 16120
			public NativeMethods.tagPARAMDESC paramdesc;
		}

		// Token: 0x0200075F RID: 1887
		public struct WINDOWPOS
		{
			// Token: 0x04003EF9 RID: 16121
			public IntPtr hwnd;

			// Token: 0x04003EFA RID: 16122
			public IntPtr hwndInsertAfter;

			// Token: 0x04003EFB RID: 16123
			public int x;

			// Token: 0x04003EFC RID: 16124
			public int y;

			// Token: 0x04003EFD RID: 16125
			public int cx;

			// Token: 0x04003EFE RID: 16126
			public int cy;

			// Token: 0x04003EFF RID: 16127
			public int flags;
		}

		// Token: 0x02000760 RID: 1888
		public struct HDLAYOUT
		{
			// Token: 0x04003F00 RID: 16128
			public IntPtr prc;

			// Token: 0x04003F01 RID: 16129
			public IntPtr pwpos;
		}

		// Token: 0x02000761 RID: 1889
		[StructLayout(LayoutKind.Sequential)]
		public class DRAWITEMSTRUCT
		{
			// Token: 0x04003F02 RID: 16130
			public int CtlType;

			// Token: 0x04003F03 RID: 16131
			public int CtlID;

			// Token: 0x04003F04 RID: 16132
			public int itemID;

			// Token: 0x04003F05 RID: 16133
			public int itemAction;

			// Token: 0x04003F06 RID: 16134
			public int itemState;

			// Token: 0x04003F07 RID: 16135
			public IntPtr hwndItem = IntPtr.Zero;

			// Token: 0x04003F08 RID: 16136
			public IntPtr hDC = IntPtr.Zero;

			// Token: 0x04003F09 RID: 16137
			public NativeMethods.RECT rcItem;

			// Token: 0x04003F0A RID: 16138
			public IntPtr itemData = IntPtr.Zero;
		}

		// Token: 0x02000762 RID: 1890
		[StructLayout(LayoutKind.Sequential)]
		public class MEASUREITEMSTRUCT
		{
			// Token: 0x04003F0B RID: 16139
			public int CtlType;

			// Token: 0x04003F0C RID: 16140
			public int CtlID;

			// Token: 0x04003F0D RID: 16141
			public int itemID;

			// Token: 0x04003F0E RID: 16142
			public int itemWidth;

			// Token: 0x04003F0F RID: 16143
			public int itemHeight;

			// Token: 0x04003F10 RID: 16144
			public IntPtr itemData = IntPtr.Zero;
		}

		// Token: 0x02000763 RID: 1891
		[StructLayout(LayoutKind.Sequential)]
		public class HELPINFO
		{
			// Token: 0x04003F11 RID: 16145
			public int cbSize = Marshal.SizeOf(typeof(NativeMethods.HELPINFO));

			// Token: 0x04003F12 RID: 16146
			public int iContextType;

			// Token: 0x04003F13 RID: 16147
			public int iCtrlId;

			// Token: 0x04003F14 RID: 16148
			public IntPtr hItemHandle = IntPtr.Zero;

			// Token: 0x04003F15 RID: 16149
			public IntPtr dwContextId = IntPtr.Zero;

			// Token: 0x04003F16 RID: 16150
			public NativeMethods.POINT MousePos;
		}

		// Token: 0x02000764 RID: 1892
		[StructLayout(LayoutKind.Sequential)]
		public class ACCEL
		{
			// Token: 0x04003F17 RID: 16151
			public byte fVirt;

			// Token: 0x04003F18 RID: 16152
			public short key;

			// Token: 0x04003F19 RID: 16153
			public short cmd;
		}

		// Token: 0x02000765 RID: 1893
		[StructLayout(LayoutKind.Sequential)]
		public class MINMAXINFO
		{
			// Token: 0x04003F1A RID: 16154
			public NativeMethods.POINT ptReserved;

			// Token: 0x04003F1B RID: 16155
			public NativeMethods.POINT ptMaxSize;

			// Token: 0x04003F1C RID: 16156
			public NativeMethods.POINT ptMaxPosition;

			// Token: 0x04003F1D RID: 16157
			public NativeMethods.POINT ptMinTrackSize;

			// Token: 0x04003F1E RID: 16158
			public NativeMethods.POINT ptMaxTrackSize;
		}

		// Token: 0x02000766 RID: 1894
		[Guid("B196B28B-BAB4-101A-B69C-00AA00341D07")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface ISpecifyPropertyPages
		{
			// Token: 0x06006CEB RID: 27883
			void GetPages([Out] NativeMethods.tagCAUUID pPages);
		}

		// Token: 0x02000767 RID: 1895
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagCAUUID
		{
			// Token: 0x04003F1F RID: 16159
			[MarshalAs(UnmanagedType.U4)]
			public int cElems;

			// Token: 0x04003F20 RID: 16160
			public IntPtr pElems = IntPtr.Zero;
		}

		// Token: 0x02000768 RID: 1896
		public struct NMTOOLBAR
		{
			// Token: 0x04003F21 RID: 16161
			public NativeMethods.NMHDR hdr;

			// Token: 0x04003F22 RID: 16162
			public int iItem;

			// Token: 0x04003F23 RID: 16163
			public NativeMethods.TBBUTTON tbButton;

			// Token: 0x04003F24 RID: 16164
			public int cchText;

			// Token: 0x04003F25 RID: 16165
			public IntPtr pszText;
		}

		// Token: 0x02000769 RID: 1897
		public struct TBBUTTON
		{
			// Token: 0x04003F26 RID: 16166
			public int iBitmap;

			// Token: 0x04003F27 RID: 16167
			public int idCommand;

			// Token: 0x04003F28 RID: 16168
			public byte fsState;

			// Token: 0x04003F29 RID: 16169
			public byte fsStyle;

			// Token: 0x04003F2A RID: 16170
			public byte bReserved0;

			// Token: 0x04003F2B RID: 16171
			public byte bReserved1;

			// Token: 0x04003F2C RID: 16172
			public IntPtr dwData;

			// Token: 0x04003F2D RID: 16173
			public IntPtr iString;
		}

		// Token: 0x0200076A RID: 1898
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class TOOLTIPTEXT
		{
			// Token: 0x04003F2E RID: 16174
			public NativeMethods.NMHDR hdr;

			// Token: 0x04003F2F RID: 16175
			public string lpszText;

			// Token: 0x04003F30 RID: 16176
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
			public string szText;

			// Token: 0x04003F31 RID: 16177
			public IntPtr hinst;

			// Token: 0x04003F32 RID: 16178
			public int uFlags;
		}

		// Token: 0x0200076B RID: 1899
		[StructLayout(LayoutKind.Sequential)]
		public class TOOLTIPTEXTA
		{
			// Token: 0x04003F33 RID: 16179
			public NativeMethods.NMHDR hdr;

			// Token: 0x04003F34 RID: 16180
			public string lpszText;

			// Token: 0x04003F35 RID: 16181
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
			public string szText;

			// Token: 0x04003F36 RID: 16182
			public IntPtr hinst;

			// Token: 0x04003F37 RID: 16183
			public int uFlags;
		}

		// Token: 0x0200076C RID: 1900
		public struct NMTBHOTITEM
		{
			// Token: 0x04003F38 RID: 16184
			public NativeMethods.NMHDR hdr;

			// Token: 0x04003F39 RID: 16185
			public int idOld;

			// Token: 0x04003F3A RID: 16186
			public int idNew;

			// Token: 0x04003F3B RID: 16187
			public int dwFlags;
		}

		// Token: 0x0200076D RID: 1901
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class HDITEM2
		{
			// Token: 0x04003F3C RID: 16188
			public int mask;

			// Token: 0x04003F3D RID: 16189
			public int cxy;

			// Token: 0x04003F3E RID: 16190
			public IntPtr pszText_notUsed = IntPtr.Zero;

			// Token: 0x04003F3F RID: 16191
			public IntPtr hbm = IntPtr.Zero;

			// Token: 0x04003F40 RID: 16192
			public int cchTextMax;

			// Token: 0x04003F41 RID: 16193
			public int fmt;

			// Token: 0x04003F42 RID: 16194
			public IntPtr lParam = IntPtr.Zero;

			// Token: 0x04003F43 RID: 16195
			public int iImage;

			// Token: 0x04003F44 RID: 16196
			public int iOrder;

			// Token: 0x04003F45 RID: 16197
			public int type;

			// Token: 0x04003F46 RID: 16198
			public IntPtr pvFilter = IntPtr.Zero;
		}

		// Token: 0x0200076E RID: 1902
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct TBBUTTONINFO
		{
			// Token: 0x04003F47 RID: 16199
			public int cbSize;

			// Token: 0x04003F48 RID: 16200
			public int dwMask;

			// Token: 0x04003F49 RID: 16201
			public int idCommand;

			// Token: 0x04003F4A RID: 16202
			public int iImage;

			// Token: 0x04003F4B RID: 16203
			public byte fsState;

			// Token: 0x04003F4C RID: 16204
			public byte fsStyle;

			// Token: 0x04003F4D RID: 16205
			public short cx;

			// Token: 0x04003F4E RID: 16206
			public IntPtr lParam;

			// Token: 0x04003F4F RID: 16207
			public IntPtr pszText;

			// Token: 0x04003F50 RID: 16208
			public int cchTest;
		}

		// Token: 0x0200076F RID: 1903
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class TV_HITTESTINFO
		{
			// Token: 0x04003F51 RID: 16209
			public int pt_x;

			// Token: 0x04003F52 RID: 16210
			public int pt_y;

			// Token: 0x04003F53 RID: 16211
			public int flags;

			// Token: 0x04003F54 RID: 16212
			public IntPtr hItem = IntPtr.Zero;
		}

		// Token: 0x02000770 RID: 1904
		[StructLayout(LayoutKind.Sequential)]
		public class NMTVCUSTOMDRAW
		{
			// Token: 0x04003F55 RID: 16213
			public NativeMethods.NMCUSTOMDRAW nmcd;

			// Token: 0x04003F56 RID: 16214
			public int clrText;

			// Token: 0x04003F57 RID: 16215
			public int clrTextBk;

			// Token: 0x04003F58 RID: 16216
			public int iLevel;
		}

		// Token: 0x02000771 RID: 1905
		public struct NMCUSTOMDRAW
		{
			// Token: 0x04003F59 RID: 16217
			public NativeMethods.NMHDR nmcd;

			// Token: 0x04003F5A RID: 16218
			public int dwDrawStage;

			// Token: 0x04003F5B RID: 16219
			public IntPtr hdc;

			// Token: 0x04003F5C RID: 16220
			public NativeMethods.RECT rc;

			// Token: 0x04003F5D RID: 16221
			public IntPtr dwItemSpec;

			// Token: 0x04003F5E RID: 16222
			public int uItemState;

			// Token: 0x04003F5F RID: 16223
			public IntPtr lItemlParam;
		}

		// Token: 0x02000772 RID: 1906
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class MCHITTESTINFO
		{
			// Token: 0x04003F60 RID: 16224
			public int cbSize = Marshal.SizeOf(typeof(NativeMethods.MCHITTESTINFO));

			// Token: 0x04003F61 RID: 16225
			public int pt_x;

			// Token: 0x04003F62 RID: 16226
			public int pt_y;

			// Token: 0x04003F63 RID: 16227
			public int uHit;

			// Token: 0x04003F64 RID: 16228
			public short st_wYear;

			// Token: 0x04003F65 RID: 16229
			public short st_wMonth;

			// Token: 0x04003F66 RID: 16230
			public short st_wDayOfWeek;

			// Token: 0x04003F67 RID: 16231
			public short st_wDay;

			// Token: 0x04003F68 RID: 16232
			public short st_wHour;

			// Token: 0x04003F69 RID: 16233
			public short st_wMinute;

			// Token: 0x04003F6A RID: 16234
			public short st_wSecond;

			// Token: 0x04003F6B RID: 16235
			public short st_wMilliseconds;
		}

		// Token: 0x02000773 RID: 1907
		public struct MCHITTESTINFOLEVEL5
		{
			// Token: 0x04003F6C RID: 16236
			public uint cbSize;

			// Token: 0x04003F6D RID: 16237
			public Point pt;

			// Token: 0x04003F6E RID: 16238
			public int uHit;

			// Token: 0x04003F6F RID: 16239
			public NativeMethods.SYSTEMTIME st;

			// Token: 0x04003F70 RID: 16240
			public NativeMethods.RECT rc;

			// Token: 0x04003F71 RID: 16241
			public int iOffset;

			// Token: 0x04003F72 RID: 16242
			public int iRow;

			// Token: 0x04003F73 RID: 16243
			public int iCol;
		}

		// Token: 0x02000774 RID: 1908
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct MCGRIDINFO
		{
			// Token: 0x04003F74 RID: 16244
			public uint cbSize;

			// Token: 0x04003F75 RID: 16245
			public uint dwPart;

			// Token: 0x04003F76 RID: 16246
			public int dwFlags;

			// Token: 0x04003F77 RID: 16247
			public int iCalendar;

			// Token: 0x04003F78 RID: 16248
			public int iRow;

			// Token: 0x04003F79 RID: 16249
			public int iCol;

			// Token: 0x04003F7A RID: 16250
			public UnsafeNativeMethods.BOOL bSelected;

			// Token: 0x04003F7B RID: 16251
			public NativeMethods.SYSTEMTIME stStart;

			// Token: 0x04003F7C RID: 16252
			public NativeMethods.SYSTEMTIME stEnd;

			// Token: 0x04003F7D RID: 16253
			public NativeMethods.RECT rc;

			// Token: 0x04003F7E RID: 16254
			public unsafe char* pszName;

			// Token: 0x04003F7F RID: 16255
			public UIntPtr cchName;
		}

		// Token: 0x02000775 RID: 1909
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class NMSELCHANGE
		{
			// Token: 0x04003F80 RID: 16256
			public NativeMethods.NMHDR nmhdr;

			// Token: 0x04003F81 RID: 16257
			public NativeMethods.SYSTEMTIME stSelStart;

			// Token: 0x04003F82 RID: 16258
			public NativeMethods.SYSTEMTIME stSelEnd;
		}

		// Token: 0x02000776 RID: 1910
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class NMDAYSTATE
		{
			// Token: 0x04003F83 RID: 16259
			public NativeMethods.NMHDR nmhdr;

			// Token: 0x04003F84 RID: 16260
			public NativeMethods.SYSTEMTIME stStart;

			// Token: 0x04003F85 RID: 16261
			public int cDayState;

			// Token: 0x04003F86 RID: 16262
			public IntPtr prgDayState;
		}

		// Token: 0x02000777 RID: 1911
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class NMVIEWCHANGE
		{
			// Token: 0x04003F87 RID: 16263
			public NativeMethods.NMHDR nmhdr;

			// Token: 0x04003F88 RID: 16264
			public uint uOldView;

			// Token: 0x04003F89 RID: 16265
			public uint uNewView;
		}

		// Token: 0x02000778 RID: 1912
		public struct NMLVCUSTOMDRAW
		{
			// Token: 0x04003F8A RID: 16266
			public NativeMethods.NMCUSTOMDRAW nmcd;

			// Token: 0x04003F8B RID: 16267
			public int clrText;

			// Token: 0x04003F8C RID: 16268
			public int clrTextBk;

			// Token: 0x04003F8D RID: 16269
			public int iSubItem;

			// Token: 0x04003F8E RID: 16270
			public int dwItemType;

			// Token: 0x04003F8F RID: 16271
			public int clrFace;

			// Token: 0x04003F90 RID: 16272
			public int iIconEffect;

			// Token: 0x04003F91 RID: 16273
			public int iIconPhase;

			// Token: 0x04003F92 RID: 16274
			public int iPartId;

			// Token: 0x04003F93 RID: 16275
			public int iStateId;

			// Token: 0x04003F94 RID: 16276
			public NativeMethods.RECT rcText;

			// Token: 0x04003F95 RID: 16277
			public uint uAlign;
		}

		// Token: 0x02000779 RID: 1913
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class NMLVGETINFOTIP
		{
			// Token: 0x04003F96 RID: 16278
			public NativeMethods.NMHDR nmhdr;

			// Token: 0x04003F97 RID: 16279
			public int flags;

			// Token: 0x04003F98 RID: 16280
			public IntPtr lpszText = IntPtr.Zero;

			// Token: 0x04003F99 RID: 16281
			public int cchTextMax;

			// Token: 0x04003F9A RID: 16282
			public int item;

			// Token: 0x04003F9B RID: 16283
			public int subItem;

			// Token: 0x04003F9C RID: 16284
			public IntPtr lParam = IntPtr.Zero;
		}

		// Token: 0x0200077A RID: 1914
		[StructLayout(LayoutKind.Sequential)]
		public class NMLVKEYDOWN
		{
			// Token: 0x04003F9D RID: 16285
			public NativeMethods.NMHDR hdr;

			// Token: 0x04003F9E RID: 16286
			public short wVKey;

			// Token: 0x04003F9F RID: 16287
			public uint flags;
		}

		// Token: 0x0200077B RID: 1915
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class LVHITTESTINFO
		{
			// Token: 0x04003FA0 RID: 16288
			public int pt_x;

			// Token: 0x04003FA1 RID: 16289
			public int pt_y;

			// Token: 0x04003FA2 RID: 16290
			public int flags;

			// Token: 0x04003FA3 RID: 16291
			public int iItem;

			// Token: 0x04003FA4 RID: 16292
			public int iSubItem;
		}

		// Token: 0x0200077C RID: 1916
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class LVBKIMAGE
		{
			// Token: 0x04003FA5 RID: 16293
			public int ulFlags;

			// Token: 0x04003FA6 RID: 16294
			public IntPtr hBmp = IntPtr.Zero;

			// Token: 0x04003FA7 RID: 16295
			public string pszImage;

			// Token: 0x04003FA8 RID: 16296
			public int cchImageMax;

			// Token: 0x04003FA9 RID: 16297
			public int xOffset;

			// Token: 0x04003FAA RID: 16298
			public int yOffset;
		}

		// Token: 0x0200077D RID: 1917
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class LVCOLUMN_T
		{
			// Token: 0x04003FAB RID: 16299
			public int mask;

			// Token: 0x04003FAC RID: 16300
			public int fmt;

			// Token: 0x04003FAD RID: 16301
			public int cx;

			// Token: 0x04003FAE RID: 16302
			public string pszText;

			// Token: 0x04003FAF RID: 16303
			public int cchTextMax;

			// Token: 0x04003FB0 RID: 16304
			public int iSubItem;

			// Token: 0x04003FB1 RID: 16305
			public int iImage;

			// Token: 0x04003FB2 RID: 16306
			public int iOrder;
		}

		// Token: 0x0200077E RID: 1918
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct LVFINDINFO
		{
			// Token: 0x04003FB3 RID: 16307
			public int flags;

			// Token: 0x04003FB4 RID: 16308
			public string psz;

			// Token: 0x04003FB5 RID: 16309
			public IntPtr lParam;

			// Token: 0x04003FB6 RID: 16310
			public int ptX;

			// Token: 0x04003FB7 RID: 16311
			public int ptY;

			// Token: 0x04003FB8 RID: 16312
			public int vkDirection;
		}

		// Token: 0x0200077F RID: 1919
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct LVITEM
		{
			// Token: 0x06006CFB RID: 27899 RVA: 0x00190790 File Offset: 0x0018E990
			public void Reset()
			{
				this.pszText = null;
				this.mask = 0;
				this.iItem = 0;
				this.iSubItem = 0;
				this.stateMask = 0;
				this.state = 0;
				this.cchTextMax = 0;
				this.iImage = 0;
				this.lParam = IntPtr.Zero;
				this.iIndent = 0;
				this.iGroupId = 0;
				this.cColumns = 0;
				this.puColumns = IntPtr.Zero;
			}

			// Token: 0x06006CFC RID: 27900 RVA: 0x00190800 File Offset: 0x0018EA00
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					"LVITEM: pszText = ",
					this.pszText,
					", iItem = ",
					this.iItem.ToString(CultureInfo.InvariantCulture),
					", iSubItem = ",
					this.iSubItem.ToString(CultureInfo.InvariantCulture),
					", state = ",
					this.state.ToString(CultureInfo.InvariantCulture),
					", iGroupId = ",
					this.iGroupId.ToString(CultureInfo.InvariantCulture),
					", cColumns = ",
					this.cColumns.ToString(CultureInfo.InvariantCulture)
				});
			}

			// Token: 0x04003FB9 RID: 16313
			public int mask;

			// Token: 0x04003FBA RID: 16314
			public int iItem;

			// Token: 0x04003FBB RID: 16315
			public int iSubItem;

			// Token: 0x04003FBC RID: 16316
			public int state;

			// Token: 0x04003FBD RID: 16317
			public int stateMask;

			// Token: 0x04003FBE RID: 16318
			public string pszText;

			// Token: 0x04003FBF RID: 16319
			public int cchTextMax;

			// Token: 0x04003FC0 RID: 16320
			public int iImage;

			// Token: 0x04003FC1 RID: 16321
			public IntPtr lParam;

			// Token: 0x04003FC2 RID: 16322
			public int iIndent;

			// Token: 0x04003FC3 RID: 16323
			public int iGroupId;

			// Token: 0x04003FC4 RID: 16324
			public int cColumns;

			// Token: 0x04003FC5 RID: 16325
			public IntPtr puColumns;
		}

		// Token: 0x02000780 RID: 1920
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct LVITEM_NOTEXT
		{
			// Token: 0x04003FC6 RID: 16326
			public int mask;

			// Token: 0x04003FC7 RID: 16327
			public int iItem;

			// Token: 0x04003FC8 RID: 16328
			public int iSubItem;

			// Token: 0x04003FC9 RID: 16329
			public int state;

			// Token: 0x04003FCA RID: 16330
			public int stateMask;

			// Token: 0x04003FCB RID: 16331
			public IntPtr pszText;

			// Token: 0x04003FCC RID: 16332
			public int cchTextMax;

			// Token: 0x04003FCD RID: 16333
			public int iImage;

			// Token: 0x04003FCE RID: 16334
			public IntPtr lParam;

			// Token: 0x04003FCF RID: 16335
			public int iIndent;
		}

		// Token: 0x02000781 RID: 1921
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class LVCOLUMN
		{
			// Token: 0x04003FD0 RID: 16336
			public int mask;

			// Token: 0x04003FD1 RID: 16337
			public int fmt;

			// Token: 0x04003FD2 RID: 16338
			public int cx;

			// Token: 0x04003FD3 RID: 16339
			public IntPtr pszText;

			// Token: 0x04003FD4 RID: 16340
			public int cchTextMax;

			// Token: 0x04003FD5 RID: 16341
			public int iSubItem;

			// Token: 0x04003FD6 RID: 16342
			public int iImage;

			// Token: 0x04003FD7 RID: 16343
			public int iOrder;
		}

		// Token: 0x02000782 RID: 1922
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public class LVGROUP
		{
			// Token: 0x06006CFE RID: 27902 RVA: 0x001908B4 File Offset: 0x0018EAB4
			public override string ToString()
			{
				return "LVGROUP: header = " + this.pszHeader.ToString() + ", iGroupId = " + this.iGroupId.ToString(CultureInfo.InvariantCulture);
			}

			// Token: 0x04003FD8 RID: 16344
			public uint cbSize = (uint)Marshal.SizeOf(typeof(NativeMethods.LVGROUP));

			// Token: 0x04003FD9 RID: 16345
			public uint mask;

			// Token: 0x04003FDA RID: 16346
			public IntPtr pszHeader;

			// Token: 0x04003FDB RID: 16347
			public int cchHeader;

			// Token: 0x04003FDC RID: 16348
			public IntPtr pszFooter = IntPtr.Zero;

			// Token: 0x04003FDD RID: 16349
			public int cchFooter;

			// Token: 0x04003FDE RID: 16350
			public int iGroupId;

			// Token: 0x04003FDF RID: 16351
			public uint stateMask;

			// Token: 0x04003FE0 RID: 16352
			public uint state;

			// Token: 0x04003FE1 RID: 16353
			public uint uAlign;
		}

		// Token: 0x02000783 RID: 1923
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class LVINSERTMARK
		{
			// Token: 0x04003FE2 RID: 16354
			public uint cbSize = (uint)Marshal.SizeOf(typeof(NativeMethods.LVINSERTMARK));

			// Token: 0x04003FE3 RID: 16355
			public int dwFlags;

			// Token: 0x04003FE4 RID: 16356
			public int iItem;

			// Token: 0x04003FE5 RID: 16357
			public int dwReserved;
		}

		// Token: 0x02000784 RID: 1924
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class LVTILEVIEWINFO
		{
			// Token: 0x04003FE6 RID: 16358
			public uint cbSize = (uint)Marshal.SizeOf(typeof(NativeMethods.LVTILEVIEWINFO));

			// Token: 0x04003FE7 RID: 16359
			public int dwMask;

			// Token: 0x04003FE8 RID: 16360
			public int dwFlags;

			// Token: 0x04003FE9 RID: 16361
			public NativeMethods.SIZE sizeTile;

			// Token: 0x04003FEA RID: 16362
			public int cLines;

			// Token: 0x04003FEB RID: 16363
			public NativeMethods.RECT rcLabelMargin;
		}

		// Token: 0x02000785 RID: 1925
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class NMLVCACHEHINT
		{
			// Token: 0x04003FEC RID: 16364
			public NativeMethods.NMHDR hdr;

			// Token: 0x04003FED RID: 16365
			public int iFrom;

			// Token: 0x04003FEE RID: 16366
			public int iTo;
		}

		// Token: 0x02000786 RID: 1926
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class NMLVDISPINFO
		{
			// Token: 0x04003FEF RID: 16367
			public NativeMethods.NMHDR hdr;

			// Token: 0x04003FF0 RID: 16368
			public NativeMethods.LVITEM item;
		}

		// Token: 0x02000787 RID: 1927
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class NMLVDISPINFO_NOTEXT
		{
			// Token: 0x04003FF1 RID: 16369
			public NativeMethods.NMHDR hdr;

			// Token: 0x04003FF2 RID: 16370
			public NativeMethods.LVITEM_NOTEXT item;
		}

		// Token: 0x02000788 RID: 1928
		[StructLayout(LayoutKind.Sequential)]
		public class NMLVODSTATECHANGE
		{
			// Token: 0x04003FF3 RID: 16371
			public NativeMethods.NMHDR hdr;

			// Token: 0x04003FF4 RID: 16372
			public int iFrom;

			// Token: 0x04003FF5 RID: 16373
			public int iTo;

			// Token: 0x04003FF6 RID: 16374
			public int uNewState;

			// Token: 0x04003FF7 RID: 16375
			public int uOldState;
		}

		// Token: 0x02000789 RID: 1929
		[StructLayout(LayoutKind.Sequential)]
		public class CLIENTCREATESTRUCT
		{
			// Token: 0x06006D06 RID: 27910 RVA: 0x00190942 File Offset: 0x0018EB42
			public CLIENTCREATESTRUCT(IntPtr hmenu, int idFirst)
			{
				this.hWindowMenu = hmenu;
				this.idFirstChild = idFirst;
			}

			// Token: 0x04003FF8 RID: 16376
			public IntPtr hWindowMenu;

			// Token: 0x04003FF9 RID: 16377
			public int idFirstChild;
		}

		// Token: 0x0200078A RID: 1930
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class NMDATETIMECHANGE
		{
			// Token: 0x04003FFA RID: 16378
			public NativeMethods.NMHDR nmhdr;

			// Token: 0x04003FFB RID: 16379
			public int dwFlags;

			// Token: 0x04003FFC RID: 16380
			public NativeMethods.SYSTEMTIME st;
		}

		// Token: 0x0200078B RID: 1931
		[StructLayout(LayoutKind.Sequential)]
		public class COPYDATASTRUCT
		{
			// Token: 0x04003FFD RID: 16381
			public int dwData;

			// Token: 0x04003FFE RID: 16382
			public int cbData;

			// Token: 0x04003FFF RID: 16383
			public IntPtr lpData = IntPtr.Zero;
		}

		// Token: 0x0200078C RID: 1932
		[StructLayout(LayoutKind.Sequential)]
		public class NMHEADER
		{
			// Token: 0x04004000 RID: 16384
			public NativeMethods.NMHDR nmhdr;

			// Token: 0x04004001 RID: 16385
			public int iItem;

			// Token: 0x04004002 RID: 16386
			public int iButton;

			// Token: 0x04004003 RID: 16387
			public IntPtr pItem = IntPtr.Zero;
		}

		// Token: 0x0200078D RID: 1933
		[StructLayout(LayoutKind.Sequential)]
		public class MOUSEHOOKSTRUCT
		{
			// Token: 0x04004004 RID: 16388
			public int pt_x;

			// Token: 0x04004005 RID: 16389
			public int pt_y;

			// Token: 0x04004006 RID: 16390
			public IntPtr hWnd = IntPtr.Zero;

			// Token: 0x04004007 RID: 16391
			public int wHitTestCode;

			// Token: 0x04004008 RID: 16392
			public int dwExtraInfo;
		}

		// Token: 0x0200078E RID: 1934
		public struct MOUSEINPUT
		{
			// Token: 0x04004009 RID: 16393
			public int dx;

			// Token: 0x0400400A RID: 16394
			public int dy;

			// Token: 0x0400400B RID: 16395
			public int mouseData;

			// Token: 0x0400400C RID: 16396
			public int dwFlags;

			// Token: 0x0400400D RID: 16397
			public int time;

			// Token: 0x0400400E RID: 16398
			public IntPtr dwExtraInfo;
		}

		// Token: 0x0200078F RID: 1935
		public struct KEYBDINPUT
		{
			// Token: 0x0400400F RID: 16399
			public short wVk;

			// Token: 0x04004010 RID: 16400
			public short wScan;

			// Token: 0x04004011 RID: 16401
			public int dwFlags;

			// Token: 0x04004012 RID: 16402
			public int time;

			// Token: 0x04004013 RID: 16403
			public IntPtr dwExtraInfo;
		}

		// Token: 0x02000790 RID: 1936
		public struct HARDWAREINPUT
		{
			// Token: 0x04004014 RID: 16404
			public int uMsg;

			// Token: 0x04004015 RID: 16405
			public short wParamL;

			// Token: 0x04004016 RID: 16406
			public short wParamH;
		}

		// Token: 0x02000791 RID: 1937
		public struct INPUT
		{
			// Token: 0x04004017 RID: 16407
			public int type;

			// Token: 0x04004018 RID: 16408
			public NativeMethods.INPUTUNION inputUnion;
		}

		// Token: 0x02000792 RID: 1938
		[StructLayout(LayoutKind.Explicit)]
		public struct INPUTUNION
		{
			// Token: 0x04004019 RID: 16409
			[FieldOffset(0)]
			public NativeMethods.MOUSEINPUT mi;

			// Token: 0x0400401A RID: 16410
			[FieldOffset(0)]
			public NativeMethods.KEYBDINPUT ki;

			// Token: 0x0400401B RID: 16411
			[FieldOffset(0)]
			public NativeMethods.HARDWAREINPUT hi;
		}

		// Token: 0x02000793 RID: 1939
		[StructLayout(LayoutKind.Sequential)]
		public class CHARRANGE
		{
			// Token: 0x0400401C RID: 16412
			public int cpMin;

			// Token: 0x0400401D RID: 16413
			public int cpMax;
		}

		// Token: 0x02000794 RID: 1940
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		public class CHARFORMATW
		{
			// Token: 0x0400401E RID: 16414
			public int cbSize = Marshal.SizeOf(typeof(NativeMethods.CHARFORMATW));

			// Token: 0x0400401F RID: 16415
			public int dwMask;

			// Token: 0x04004020 RID: 16416
			public int dwEffects;

			// Token: 0x04004021 RID: 16417
			public int yHeight;

			// Token: 0x04004022 RID: 16418
			public int yOffset;

			// Token: 0x04004023 RID: 16419
			public int crTextColor;

			// Token: 0x04004024 RID: 16420
			public byte bCharSet;

			// Token: 0x04004025 RID: 16421
			public byte bPitchAndFamily;

			// Token: 0x04004026 RID: 16422
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
			public byte[] szFaceName = new byte[64];
		}

		// Token: 0x02000795 RID: 1941
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		public class CHARFORMATA
		{
			// Token: 0x04004027 RID: 16423
			public int cbSize = Marshal.SizeOf(typeof(NativeMethods.CHARFORMATA));

			// Token: 0x04004028 RID: 16424
			public int dwMask;

			// Token: 0x04004029 RID: 16425
			public int dwEffects;

			// Token: 0x0400402A RID: 16426
			public int yHeight;

			// Token: 0x0400402B RID: 16427
			public int yOffset;

			// Token: 0x0400402C RID: 16428
			public int crTextColor;

			// Token: 0x0400402D RID: 16429
			public byte bCharSet;

			// Token: 0x0400402E RID: 16430
			public byte bPitchAndFamily;

			// Token: 0x0400402F RID: 16431
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
			public byte[] szFaceName = new byte[32];
		}

		// Token: 0x02000796 RID: 1942
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		public class CHARFORMAT2A
		{
			// Token: 0x04004030 RID: 16432
			public int cbSize = Marshal.SizeOf(typeof(NativeMethods.CHARFORMAT2A));

			// Token: 0x04004031 RID: 16433
			public int dwMask;

			// Token: 0x04004032 RID: 16434
			public int dwEffects;

			// Token: 0x04004033 RID: 16435
			public int yHeight;

			// Token: 0x04004034 RID: 16436
			public int yOffset;

			// Token: 0x04004035 RID: 16437
			public int crTextColor;

			// Token: 0x04004036 RID: 16438
			public byte bCharSet;

			// Token: 0x04004037 RID: 16439
			public byte bPitchAndFamily;

			// Token: 0x04004038 RID: 16440
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
			public byte[] szFaceName = new byte[32];

			// Token: 0x04004039 RID: 16441
			public short wWeight;

			// Token: 0x0400403A RID: 16442
			public short sSpacing;

			// Token: 0x0400403B RID: 16443
			public int crBackColor;

			// Token: 0x0400403C RID: 16444
			public int lcid;

			// Token: 0x0400403D RID: 16445
			public int dwReserved;

			// Token: 0x0400403E RID: 16446
			public short sStyle;

			// Token: 0x0400403F RID: 16447
			public short wKerning;

			// Token: 0x04004040 RID: 16448
			public byte bUnderlineType;

			// Token: 0x04004041 RID: 16449
			public byte bAnimation;

			// Token: 0x04004042 RID: 16450
			public byte bRevAuthor;
		}

		// Token: 0x02000797 RID: 1943
		[StructLayout(LayoutKind.Sequential)]
		public class TEXTRANGE
		{
			// Token: 0x04004043 RID: 16451
			public NativeMethods.CHARRANGE chrg;

			// Token: 0x04004044 RID: 16452
			public IntPtr lpstrText;
		}

		// Token: 0x02000798 RID: 1944
		[StructLayout(LayoutKind.Sequential)]
		public class GETTEXTLENGTHEX
		{
			// Token: 0x04004045 RID: 16453
			public uint flags;

			// Token: 0x04004046 RID: 16454
			public uint codepage;
		}

		// Token: 0x02000799 RID: 1945
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		public class SELCHANGE
		{
			// Token: 0x04004047 RID: 16455
			public NativeMethods.NMHDR nmhdr;

			// Token: 0x04004048 RID: 16456
			public NativeMethods.CHARRANGE chrg;

			// Token: 0x04004049 RID: 16457
			public int seltyp;
		}

		// Token: 0x0200079A RID: 1946
		[StructLayout(LayoutKind.Sequential)]
		public class PARAFORMAT
		{
			// Token: 0x0400404A RID: 16458
			public int cbSize = Marshal.SizeOf(typeof(NativeMethods.PARAFORMAT));

			// Token: 0x0400404B RID: 16459
			public int dwMask;

			// Token: 0x0400404C RID: 16460
			public short wNumbering;

			// Token: 0x0400404D RID: 16461
			public short wReserved;

			// Token: 0x0400404E RID: 16462
			public int dxStartIndent;

			// Token: 0x0400404F RID: 16463
			public int dxRightIndent;

			// Token: 0x04004050 RID: 16464
			public int dxOffset;

			// Token: 0x04004051 RID: 16465
			public short wAlignment;

			// Token: 0x04004052 RID: 16466
			public short cTabCount;

			// Token: 0x04004053 RID: 16467
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
			public int[] rgxTabs;
		}

		// Token: 0x0200079B RID: 1947
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class FINDTEXT
		{
			// Token: 0x04004054 RID: 16468
			public NativeMethods.CHARRANGE chrg;

			// Token: 0x04004055 RID: 16469
			public string lpstrText;
		}

		// Token: 0x0200079C RID: 1948
		[StructLayout(LayoutKind.Sequential)]
		public class REPASTESPECIAL
		{
			// Token: 0x04004056 RID: 16470
			public int dwAspect;

			// Token: 0x04004057 RID: 16471
			public int dwParam;
		}

		// Token: 0x0200079D RID: 1949
		[StructLayout(LayoutKind.Sequential)]
		public class ENLINK
		{
			// Token: 0x04004058 RID: 16472
			public NativeMethods.NMHDR nmhdr;

			// Token: 0x04004059 RID: 16473
			public int msg;

			// Token: 0x0400405A RID: 16474
			public IntPtr wParam = IntPtr.Zero;

			// Token: 0x0400405B RID: 16475
			public IntPtr lParam = IntPtr.Zero;

			// Token: 0x0400405C RID: 16476
			public NativeMethods.CHARRANGE charrange;
		}

		// Token: 0x0200079E RID: 1950
		[StructLayout(LayoutKind.Sequential)]
		public class ENLINK64
		{
			// Token: 0x0400405D RID: 16477
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 56)]
			public byte[] contents = new byte[56];
		}

		// Token: 0x0200079F RID: 1951
		public struct RGNDATAHEADER
		{
			// Token: 0x0400405E RID: 16478
			public int cbSizeOfStruct;

			// Token: 0x0400405F RID: 16479
			public int iType;

			// Token: 0x04004060 RID: 16480
			public int nCount;

			// Token: 0x04004061 RID: 16481
			public int nRgnSize;
		}

		// Token: 0x020007A0 RID: 1952
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public class OCPFIPARAMS
		{
			// Token: 0x04004062 RID: 16482
			public int cbSizeOfStruct = Marshal.SizeOf(typeof(NativeMethods.OCPFIPARAMS));

			// Token: 0x04004063 RID: 16483
			public IntPtr hwndOwner;

			// Token: 0x04004064 RID: 16484
			public int x;

			// Token: 0x04004065 RID: 16485
			public int y;

			// Token: 0x04004066 RID: 16486
			public string lpszCaption;

			// Token: 0x04004067 RID: 16487
			public int cObjects = 1;

			// Token: 0x04004068 RID: 16488
			public IntPtr ppUnk;

			// Token: 0x04004069 RID: 16489
			public int pageCount = 1;

			// Token: 0x0400406A RID: 16490
			public IntPtr uuid;

			// Token: 0x0400406B RID: 16491
			public int lcid = Application.CurrentCulture.LCID;

			// Token: 0x0400406C RID: 16492
			public int dispidInitial;
		}

		// Token: 0x020007A1 RID: 1953
		[ComVisible(true)]
		[StructLayout(LayoutKind.Sequential)]
		public class DOCHOSTUIINFO
		{
			// Token: 0x0400406D RID: 16493
			[MarshalAs(UnmanagedType.U4)]
			public int cbSize = Marshal.SizeOf(typeof(NativeMethods.DOCHOSTUIINFO));

			// Token: 0x0400406E RID: 16494
			[MarshalAs(UnmanagedType.I4)]
			public int dwFlags;

			// Token: 0x0400406F RID: 16495
			[MarshalAs(UnmanagedType.I4)]
			public int dwDoubleClick;

			// Token: 0x04004070 RID: 16496
			[MarshalAs(UnmanagedType.I4)]
			public int dwReserved1;

			// Token: 0x04004071 RID: 16497
			[MarshalAs(UnmanagedType.I4)]
			public int dwReserved2;
		}

		// Token: 0x020007A2 RID: 1954
		public enum DOCHOSTUIFLAG
		{
			// Token: 0x04004073 RID: 16499
			DIALOG = 1,
			// Token: 0x04004074 RID: 16500
			DISABLE_HELP_MENU,
			// Token: 0x04004075 RID: 16501
			NO3DBORDER = 4,
			// Token: 0x04004076 RID: 16502
			SCROLL_NO = 8,
			// Token: 0x04004077 RID: 16503
			DISABLE_SCRIPT_INACTIVE = 16,
			// Token: 0x04004078 RID: 16504
			OPENNEWWIN = 32,
			// Token: 0x04004079 RID: 16505
			DISABLE_OFFSCREEN = 64,
			// Token: 0x0400407A RID: 16506
			FLAT_SCROLLBAR = 128,
			// Token: 0x0400407B RID: 16507
			DIV_BLOCKDEFAULT = 256,
			// Token: 0x0400407C RID: 16508
			ACTIVATE_CLIENTHIT_ONLY = 512,
			// Token: 0x0400407D RID: 16509
			NO3DOUTERBORDER = 2097152,
			// Token: 0x0400407E RID: 16510
			THEME = 262144,
			// Token: 0x0400407F RID: 16511
			NOTHEME = 524288,
			// Token: 0x04004080 RID: 16512
			DISABLE_COOKIE = 1024
		}

		// Token: 0x020007A3 RID: 1955
		public enum DOCHOSTUIDBLCLICK
		{
			// Token: 0x04004082 RID: 16514
			DEFAULT,
			// Token: 0x04004083 RID: 16515
			SHOWPROPERTIES,
			// Token: 0x04004084 RID: 16516
			SHOWCODE
		}

		// Token: 0x020007A4 RID: 1956
		public enum OLECMDID
		{
			// Token: 0x04004086 RID: 16518
			OLECMDID_SAVEAS = 4,
			// Token: 0x04004087 RID: 16519
			OLECMDID_PRINT = 6,
			// Token: 0x04004088 RID: 16520
			OLECMDID_PRINTPREVIEW,
			// Token: 0x04004089 RID: 16521
			OLECMDID_PAGESETUP,
			// Token: 0x0400408A RID: 16522
			OLECMDID_PROPERTIES = 10
		}

		// Token: 0x020007A5 RID: 1957
		public enum OLECMDEXECOPT
		{
			// Token: 0x0400408C RID: 16524
			OLECMDEXECOPT_DODEFAULT,
			// Token: 0x0400408D RID: 16525
			OLECMDEXECOPT_PROMPTUSER,
			// Token: 0x0400408E RID: 16526
			OLECMDEXECOPT_DONTPROMPTUSER,
			// Token: 0x0400408F RID: 16527
			OLECMDEXECOPT_SHOWHELP
		}

		// Token: 0x020007A6 RID: 1958
		public enum OLECMDF
		{
			// Token: 0x04004091 RID: 16529
			OLECMDF_SUPPORTED = 1,
			// Token: 0x04004092 RID: 16530
			OLECMDF_ENABLED,
			// Token: 0x04004093 RID: 16531
			OLECMDF_LATCHED = 4,
			// Token: 0x04004094 RID: 16532
			OLECMDF_NINCHED = 8,
			// Token: 0x04004095 RID: 16533
			OLECMDF_INVISIBLE = 16,
			// Token: 0x04004096 RID: 16534
			OLECMDF_DEFHIDEONCTXTMENU = 32
		}

		// Token: 0x020007A7 RID: 1959
		[StructLayout(LayoutKind.Sequential)]
		public class ENDROPFILES
		{
			// Token: 0x04004097 RID: 16535
			public NativeMethods.NMHDR nmhdr;

			// Token: 0x04004098 RID: 16536
			public IntPtr hDrop = IntPtr.Zero;

			// Token: 0x04004099 RID: 16537
			public int cp;

			// Token: 0x0400409A RID: 16538
			public bool fProtected;
		}

		// Token: 0x020007A8 RID: 1960
		[StructLayout(LayoutKind.Sequential)]
		public class REQRESIZE
		{
			// Token: 0x0400409B RID: 16539
			public NativeMethods.NMHDR nmhdr;

			// Token: 0x0400409C RID: 16540
			public NativeMethods.RECT rc;
		}

		// Token: 0x020007A9 RID: 1961
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class ENPROTECTED
		{
			// Token: 0x0400409D RID: 16541
			public NativeMethods.NMHDR nmhdr;

			// Token: 0x0400409E RID: 16542
			public int msg;

			// Token: 0x0400409F RID: 16543
			public IntPtr wParam;

			// Token: 0x040040A0 RID: 16544
			public IntPtr lParam;

			// Token: 0x040040A1 RID: 16545
			public NativeMethods.CHARRANGE chrg;
		}

		// Token: 0x020007AA RID: 1962
		[StructLayout(LayoutKind.Sequential)]
		public class ENPROTECTED64
		{
			// Token: 0x040040A2 RID: 16546
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 56)]
			public byte[] contents = new byte[56];
		}

		// Token: 0x020007AB RID: 1963
		public class ActiveX
		{
			// Token: 0x06006D1D RID: 27933 RVA: 0x00002843 File Offset: 0x00000A43
			private ActiveX()
			{
			}

			// Token: 0x040040A3 RID: 16547
			public const int OCM__BASE = 8192;

			// Token: 0x040040A4 RID: 16548
			public const int DISPID_VALUE = 0;

			// Token: 0x040040A5 RID: 16549
			public const int DISPID_UNKNOWN = -1;

			// Token: 0x040040A6 RID: 16550
			public const int DISPID_AUTOSIZE = -500;

			// Token: 0x040040A7 RID: 16551
			public const int DISPID_BACKCOLOR = -501;

			// Token: 0x040040A8 RID: 16552
			public const int DISPID_BACKSTYLE = -502;

			// Token: 0x040040A9 RID: 16553
			public const int DISPID_BORDERCOLOR = -503;

			// Token: 0x040040AA RID: 16554
			public const int DISPID_BORDERSTYLE = -504;

			// Token: 0x040040AB RID: 16555
			public const int DISPID_BORDERWIDTH = -505;

			// Token: 0x040040AC RID: 16556
			public const int DISPID_DRAWMODE = -507;

			// Token: 0x040040AD RID: 16557
			public const int DISPID_DRAWSTYLE = -508;

			// Token: 0x040040AE RID: 16558
			public const int DISPID_DRAWWIDTH = -509;

			// Token: 0x040040AF RID: 16559
			public const int DISPID_FILLCOLOR = -510;

			// Token: 0x040040B0 RID: 16560
			public const int DISPID_FILLSTYLE = -511;

			// Token: 0x040040B1 RID: 16561
			public const int DISPID_FONT = -512;

			// Token: 0x040040B2 RID: 16562
			public const int DISPID_FORECOLOR = -513;

			// Token: 0x040040B3 RID: 16563
			public const int DISPID_ENABLED = -514;

			// Token: 0x040040B4 RID: 16564
			public const int DISPID_HWND = -515;

			// Token: 0x040040B5 RID: 16565
			public const int DISPID_TABSTOP = -516;

			// Token: 0x040040B6 RID: 16566
			public const int DISPID_TEXT = -517;

			// Token: 0x040040B7 RID: 16567
			public const int DISPID_CAPTION = -518;

			// Token: 0x040040B8 RID: 16568
			public const int DISPID_BORDERVISIBLE = -519;

			// Token: 0x040040B9 RID: 16569
			public const int DISPID_APPEARANCE = -520;

			// Token: 0x040040BA RID: 16570
			public const int DISPID_MOUSEPOINTER = -521;

			// Token: 0x040040BB RID: 16571
			public const int DISPID_MOUSEICON = -522;

			// Token: 0x040040BC RID: 16572
			public const int DISPID_PICTURE = -523;

			// Token: 0x040040BD RID: 16573
			public const int DISPID_VALID = -524;

			// Token: 0x040040BE RID: 16574
			public const int DISPID_READYSTATE = -525;

			// Token: 0x040040BF RID: 16575
			public const int DISPID_REFRESH = -550;

			// Token: 0x040040C0 RID: 16576
			public const int DISPID_DOCLICK = -551;

			// Token: 0x040040C1 RID: 16577
			public const int DISPID_ABOUTBOX = -552;

			// Token: 0x040040C2 RID: 16578
			public const int DISPID_CLICK = -600;

			// Token: 0x040040C3 RID: 16579
			public const int DISPID_DBLCLICK = -601;

			// Token: 0x040040C4 RID: 16580
			public const int DISPID_KEYDOWN = -602;

			// Token: 0x040040C5 RID: 16581
			public const int DISPID_KEYPRESS = -603;

			// Token: 0x040040C6 RID: 16582
			public const int DISPID_KEYUP = -604;

			// Token: 0x040040C7 RID: 16583
			public const int DISPID_MOUSEDOWN = -605;

			// Token: 0x040040C8 RID: 16584
			public const int DISPID_MOUSEMOVE = -606;

			// Token: 0x040040C9 RID: 16585
			public const int DISPID_MOUSEUP = -607;

			// Token: 0x040040CA RID: 16586
			public const int DISPID_ERROREVENT = -608;

			// Token: 0x040040CB RID: 16587
			public const int DISPID_RIGHTTOLEFT = -611;

			// Token: 0x040040CC RID: 16588
			public const int DISPID_READYSTATECHANGE = -609;

			// Token: 0x040040CD RID: 16589
			public const int DISPID_AMBIENT_BACKCOLOR = -701;

			// Token: 0x040040CE RID: 16590
			public const int DISPID_AMBIENT_DISPLAYNAME = -702;

			// Token: 0x040040CF RID: 16591
			public const int DISPID_AMBIENT_FONT = -703;

			// Token: 0x040040D0 RID: 16592
			public const int DISPID_AMBIENT_FORECOLOR = -704;

			// Token: 0x040040D1 RID: 16593
			public const int DISPID_AMBIENT_LOCALEID = -705;

			// Token: 0x040040D2 RID: 16594
			public const int DISPID_AMBIENT_MESSAGEREFLECT = -706;

			// Token: 0x040040D3 RID: 16595
			public const int DISPID_AMBIENT_SCALEUNITS = -707;

			// Token: 0x040040D4 RID: 16596
			public const int DISPID_AMBIENT_TEXTALIGN = -708;

			// Token: 0x040040D5 RID: 16597
			public const int DISPID_AMBIENT_USERMODE = -709;

			// Token: 0x040040D6 RID: 16598
			public const int DISPID_AMBIENT_UIDEAD = -710;

			// Token: 0x040040D7 RID: 16599
			public const int DISPID_AMBIENT_SHOWGRABHANDLES = -711;

			// Token: 0x040040D8 RID: 16600
			public const int DISPID_AMBIENT_SHOWHATCHING = -712;

			// Token: 0x040040D9 RID: 16601
			public const int DISPID_AMBIENT_DISPLAYASDEFAULT = -713;

			// Token: 0x040040DA RID: 16602
			public const int DISPID_AMBIENT_SUPPORTSMNEMONICS = -714;

			// Token: 0x040040DB RID: 16603
			public const int DISPID_AMBIENT_AUTOCLIP = -715;

			// Token: 0x040040DC RID: 16604
			public const int DISPID_AMBIENT_APPEARANCE = -716;

			// Token: 0x040040DD RID: 16605
			public const int DISPID_AMBIENT_PALETTE = -726;

			// Token: 0x040040DE RID: 16606
			public const int DISPID_AMBIENT_TRANSFERPRIORITY = -728;

			// Token: 0x040040DF RID: 16607
			public const int DISPID_AMBIENT_RIGHTTOLEFT = -732;

			// Token: 0x040040E0 RID: 16608
			public const int DISPID_Name = -800;

			// Token: 0x040040E1 RID: 16609
			public const int DISPID_Delete = -801;

			// Token: 0x040040E2 RID: 16610
			public const int DISPID_Object = -802;

			// Token: 0x040040E3 RID: 16611
			public const int DISPID_Parent = -803;

			// Token: 0x040040E4 RID: 16612
			public const int DVASPECT_CONTENT = 1;

			// Token: 0x040040E5 RID: 16613
			public const int DVASPECT_THUMBNAIL = 2;

			// Token: 0x040040E6 RID: 16614
			public const int DVASPECT_ICON = 4;

			// Token: 0x040040E7 RID: 16615
			public const int DVASPECT_DOCPRINT = 8;

			// Token: 0x040040E8 RID: 16616
			public const int OLEMISC_RECOMPOSEONRESIZE = 1;

			// Token: 0x040040E9 RID: 16617
			public const int OLEMISC_ONLYICONIC = 2;

			// Token: 0x040040EA RID: 16618
			public const int OLEMISC_INSERTNOTREPLACE = 4;

			// Token: 0x040040EB RID: 16619
			public const int OLEMISC_STATIC = 8;

			// Token: 0x040040EC RID: 16620
			public const int OLEMISC_CANTLINKINSIDE = 16;

			// Token: 0x040040ED RID: 16621
			public const int OLEMISC_CANLINKBYOLE1 = 32;

			// Token: 0x040040EE RID: 16622
			public const int OLEMISC_ISLINKOBJECT = 64;

			// Token: 0x040040EF RID: 16623
			public const int OLEMISC_INSIDEOUT = 128;

			// Token: 0x040040F0 RID: 16624
			public const int OLEMISC_ACTIVATEWHENVISIBLE = 256;

			// Token: 0x040040F1 RID: 16625
			public const int OLEMISC_RENDERINGISDEVICEINDEPENDENT = 512;

			// Token: 0x040040F2 RID: 16626
			public const int OLEMISC_INVISIBLEATRUNTIME = 1024;

			// Token: 0x040040F3 RID: 16627
			public const int OLEMISC_ALWAYSRUN = 2048;

			// Token: 0x040040F4 RID: 16628
			public const int OLEMISC_ACTSLIKEBUTTON = 4096;

			// Token: 0x040040F5 RID: 16629
			public const int OLEMISC_ACTSLIKELABEL = 8192;

			// Token: 0x040040F6 RID: 16630
			public const int OLEMISC_NOUIACTIVATE = 16384;

			// Token: 0x040040F7 RID: 16631
			public const int OLEMISC_ALIGNABLE = 32768;

			// Token: 0x040040F8 RID: 16632
			public const int OLEMISC_SIMPLEFRAME = 65536;

			// Token: 0x040040F9 RID: 16633
			public const int OLEMISC_SETCLIENTSITEFIRST = 131072;

			// Token: 0x040040FA RID: 16634
			public const int OLEMISC_IMEMODE = 262144;

			// Token: 0x040040FB RID: 16635
			public const int OLEMISC_IGNOREACTIVATEWHENVISIBLE = 524288;

			// Token: 0x040040FC RID: 16636
			public const int OLEMISC_WANTSTOMENUMERGE = 1048576;

			// Token: 0x040040FD RID: 16637
			public const int OLEMISC_SUPPORTSMULTILEVELUNDO = 2097152;

			// Token: 0x040040FE RID: 16638
			public const int QACONTAINER_SHOWHATCHING = 1;

			// Token: 0x040040FF RID: 16639
			public const int QACONTAINER_SHOWGRABHANDLES = 2;

			// Token: 0x04004100 RID: 16640
			public const int QACONTAINER_USERMODE = 4;

			// Token: 0x04004101 RID: 16641
			public const int QACONTAINER_DISPLAYASDEFAULT = 8;

			// Token: 0x04004102 RID: 16642
			public const int QACONTAINER_UIDEAD = 16;

			// Token: 0x04004103 RID: 16643
			public const int QACONTAINER_AUTOCLIP = 32;

			// Token: 0x04004104 RID: 16644
			public const int QACONTAINER_MESSAGEREFLECT = 64;

			// Token: 0x04004105 RID: 16645
			public const int QACONTAINER_SUPPORTSMNEMONICS = 128;

			// Token: 0x04004106 RID: 16646
			public const int XFORMCOORDS_POSITION = 1;

			// Token: 0x04004107 RID: 16647
			public const int XFORMCOORDS_SIZE = 2;

			// Token: 0x04004108 RID: 16648
			public const int XFORMCOORDS_HIMETRICTOCONTAINER = 4;

			// Token: 0x04004109 RID: 16649
			public const int XFORMCOORDS_CONTAINERTOHIMETRIC = 8;

			// Token: 0x0400410A RID: 16650
			public const int PROPCAT_Nil = -1;

			// Token: 0x0400410B RID: 16651
			public const int PROPCAT_Misc = -2;

			// Token: 0x0400410C RID: 16652
			public const int PROPCAT_Font = -3;

			// Token: 0x0400410D RID: 16653
			public const int PROPCAT_Position = -4;

			// Token: 0x0400410E RID: 16654
			public const int PROPCAT_Appearance = -5;

			// Token: 0x0400410F RID: 16655
			public const int PROPCAT_Behavior = -6;

			// Token: 0x04004110 RID: 16656
			public const int PROPCAT_Data = -7;

			// Token: 0x04004111 RID: 16657
			public const int PROPCAT_List = -8;

			// Token: 0x04004112 RID: 16658
			public const int PROPCAT_Text = -9;

			// Token: 0x04004113 RID: 16659
			public const int PROPCAT_Scale = -10;

			// Token: 0x04004114 RID: 16660
			public const int PROPCAT_DDE = -11;

			// Token: 0x04004115 RID: 16661
			public const int GC_WCH_SIBLING = 1;

			// Token: 0x04004116 RID: 16662
			public const int GC_WCH_CONTAINER = 2;

			// Token: 0x04004117 RID: 16663
			public const int GC_WCH_CONTAINED = 3;

			// Token: 0x04004118 RID: 16664
			public const int GC_WCH_ALL = 4;

			// Token: 0x04004119 RID: 16665
			public const int GC_WCH_FREVERSEDIR = 134217728;

			// Token: 0x0400411A RID: 16666
			public const int GC_WCH_FONLYNEXT = 268435456;

			// Token: 0x0400411B RID: 16667
			public const int GC_WCH_FONLYPREV = 536870912;

			// Token: 0x0400411C RID: 16668
			public const int GC_WCH_FSELECTED = 1073741824;

			// Token: 0x0400411D RID: 16669
			public const int OLECONTF_EMBEDDINGS = 1;

			// Token: 0x0400411E RID: 16670
			public const int OLECONTF_LINKS = 2;

			// Token: 0x0400411F RID: 16671
			public const int OLECONTF_OTHERS = 4;

			// Token: 0x04004120 RID: 16672
			public const int OLECONTF_ONLYUSER = 8;

			// Token: 0x04004121 RID: 16673
			public const int OLECONTF_ONLYIFRUNNING = 16;

			// Token: 0x04004122 RID: 16674
			public const int ALIGN_MIN = 0;

			// Token: 0x04004123 RID: 16675
			public const int ALIGN_NO_CHANGE = 0;

			// Token: 0x04004124 RID: 16676
			public const int ALIGN_TOP = 1;

			// Token: 0x04004125 RID: 16677
			public const int ALIGN_BOTTOM = 2;

			// Token: 0x04004126 RID: 16678
			public const int ALIGN_LEFT = 3;

			// Token: 0x04004127 RID: 16679
			public const int ALIGN_RIGHT = 4;

			// Token: 0x04004128 RID: 16680
			public const int ALIGN_MAX = 4;

			// Token: 0x04004129 RID: 16681
			public const int OLEVERBATTRIB_NEVERDIRTIES = 1;

			// Token: 0x0400412A RID: 16682
			public const int OLEVERBATTRIB_ONCONTAINERMENU = 2;

			// Token: 0x0400412B RID: 16683
			public static Guid IID_IUnknown = new Guid("{00000000-0000-0000-C000-000000000046}");
		}

		// Token: 0x020007AC RID: 1964
		public static class Util
		{
			// Token: 0x06006D1F RID: 27935 RVA: 0x00190AF0 File Offset: 0x0018ECF0
			public static int MAKELONG(int low, int high)
			{
				return (high << 16) | (low & 65535);
			}

			// Token: 0x06006D20 RID: 27936 RVA: 0x00190AFE File Offset: 0x0018ECFE
			public static IntPtr MAKELPARAM(int low, int high)
			{
				return (IntPtr)((high << 16) | (low & 65535));
			}

			// Token: 0x06006D21 RID: 27937 RVA: 0x00190B11 File Offset: 0x0018ED11
			public static int HIWORD(int n)
			{
				return (n >> 16) & 65535;
			}

			// Token: 0x06006D22 RID: 27938 RVA: 0x00190B1D File Offset: 0x0018ED1D
			public static int HIWORD(IntPtr n)
			{
				return NativeMethods.Util.HIWORD((int)(long)n);
			}

			// Token: 0x06006D23 RID: 27939 RVA: 0x00190B2B File Offset: 0x0018ED2B
			public static int LOWORD(int n)
			{
				return n & 65535;
			}

			// Token: 0x06006D24 RID: 27940 RVA: 0x00190B34 File Offset: 0x0018ED34
			public static int LOWORD(IntPtr n)
			{
				return NativeMethods.Util.LOWORD((int)(long)n);
			}

			// Token: 0x06006D25 RID: 27941 RVA: 0x00190B42 File Offset: 0x0018ED42
			public static int SignedHIWORD(IntPtr n)
			{
				return NativeMethods.Util.SignedHIWORD((int)(long)n);
			}

			// Token: 0x06006D26 RID: 27942 RVA: 0x00190B50 File Offset: 0x0018ED50
			public static int SignedLOWORD(IntPtr n)
			{
				return NativeMethods.Util.SignedLOWORD((int)(long)n);
			}

			// Token: 0x06006D27 RID: 27943 RVA: 0x00190B60 File Offset: 0x0018ED60
			public static int SignedHIWORD(int n)
			{
				return (int)((short)((n >> 16) & 65535));
			}

			// Token: 0x06006D28 RID: 27944 RVA: 0x00190B7C File Offset: 0x0018ED7C
			public static int SignedLOWORD(int n)
			{
				return (int)((short)(n & 65535));
			}

			// Token: 0x06006D29 RID: 27945 RVA: 0x00190B93 File Offset: 0x0018ED93
			public static int GetPInvokeStringLength(string s)
			{
				if (s == null)
				{
					return 0;
				}
				if (Marshal.SystemDefaultCharSize == 2)
				{
					return s.Length;
				}
				if (s.Length == 0)
				{
					return 0;
				}
				if (s.IndexOf('\0') > -1)
				{
					return NativeMethods.Util.GetEmbeddedNullStringLengthAnsi(s);
				}
				return NativeMethods.Util.lstrlen(s);
			}

			// Token: 0x06006D2A RID: 27946 RVA: 0x00190BCC File Offset: 0x0018EDCC
			private static int GetEmbeddedNullStringLengthAnsi(string s)
			{
				int num = s.IndexOf('\0');
				if (num > -1)
				{
					string text = s.Substring(0, num);
					string text2 = s.Substring(num + 1);
					return NativeMethods.Util.GetPInvokeStringLength(text) + NativeMethods.Util.GetEmbeddedNullStringLengthAnsi(text2) + 1;
				}
				return NativeMethods.Util.GetPInvokeStringLength(s);
			}

			// Token: 0x06006D2B RID: 27947
			[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
			private static extern int lstrlen(string s);
		}

		// Token: 0x020007AD RID: 1965
		public enum tagTYPEKIND
		{
			// Token: 0x0400412D RID: 16685
			TKIND_ENUM,
			// Token: 0x0400412E RID: 16686
			TKIND_RECORD,
			// Token: 0x0400412F RID: 16687
			TKIND_MODULE,
			// Token: 0x04004130 RID: 16688
			TKIND_INTERFACE,
			// Token: 0x04004131 RID: 16689
			TKIND_DISPATCH,
			// Token: 0x04004132 RID: 16690
			TKIND_COCLASS,
			// Token: 0x04004133 RID: 16691
			TKIND_ALIAS,
			// Token: 0x04004134 RID: 16692
			TKIND_UNION,
			// Token: 0x04004135 RID: 16693
			TKIND_MAX
		}

		// Token: 0x020007AE RID: 1966
		[StructLayout(LayoutKind.Sequential)]
		public class tagTYPEDESC
		{
			// Token: 0x04004136 RID: 16694
			public IntPtr unionMember;

			// Token: 0x04004137 RID: 16695
			public short vt;
		}

		// Token: 0x020007AF RID: 1967
		public struct tagPARAMDESC
		{
			// Token: 0x04004138 RID: 16696
			public IntPtr pparamdescex;

			// Token: 0x04004139 RID: 16697
			[MarshalAs(UnmanagedType.U2)]
			public short wParamFlags;
		}

		// Token: 0x020007B0 RID: 1968
		public sealed class CommonHandles
		{
			// Token: 0x0400413A RID: 16698
			public static readonly int Accelerator = System.Internal.HandleCollector.RegisterType("Accelerator", 80, 50);

			// Token: 0x0400413B RID: 16699
			public static readonly int Cursor = System.Internal.HandleCollector.RegisterType("Cursor", 20, 500);

			// Token: 0x0400413C RID: 16700
			public static readonly int EMF = System.Internal.HandleCollector.RegisterType("EnhancedMetaFile", 20, 500);

			// Token: 0x0400413D RID: 16701
			public static readonly int Find = System.Internal.HandleCollector.RegisterType("Find", 0, 1000);

			// Token: 0x0400413E RID: 16702
			public static readonly int GDI = System.Internal.HandleCollector.RegisterType("GDI", 50, 500);

			// Token: 0x0400413F RID: 16703
			public static readonly int HDC = System.Internal.HandleCollector.RegisterType("HDC", 100, 2);

			// Token: 0x04004140 RID: 16704
			public static readonly int CompatibleHDC = System.Internal.HandleCollector.RegisterType("ComptibleHDC", 50, 50);

			// Token: 0x04004141 RID: 16705
			public static readonly int Icon = System.Internal.HandleCollector.RegisterType("Icon", 20, 500);

			// Token: 0x04004142 RID: 16706
			public static readonly int Kernel = System.Internal.HandleCollector.RegisterType("Kernel", 0, 1000);

			// Token: 0x04004143 RID: 16707
			public static readonly int Menu = System.Internal.HandleCollector.RegisterType("Menu", 30, 1000);

			// Token: 0x04004144 RID: 16708
			public static readonly int Window = System.Internal.HandleCollector.RegisterType("Window", 5, 1000);
		}

		// Token: 0x020007B1 RID: 1969
		public enum tagSYSKIND
		{
			// Token: 0x04004146 RID: 16710
			SYS_WIN16,
			// Token: 0x04004147 RID: 16711
			SYS_MAC = 2
		}

		// Token: 0x020007B2 RID: 1970
		// (Invoke) Token: 0x06006D30 RID: 27952
		public delegate bool MonitorEnumProc(IntPtr monitor, IntPtr hdc, IntPtr lprcMonitor, IntPtr lParam);

		// Token: 0x020007B3 RID: 1971
		[Guid("A7ABA9C1-8983-11cf-8F20-00805F2CD064")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IProvideMultipleClassInfo
		{
			// Token: 0x06006D33 RID: 27955
			[PreserveSig]
			UnsafeNativeMethods.ITypeInfo GetClassInfo();

			// Token: 0x06006D34 RID: 27956
			[PreserveSig]
			int GetGUID(int dwGuidKind, [In] [Out] ref Guid pGuid);

			// Token: 0x06006D35 RID: 27957
			[PreserveSig]
			int GetMultiTypeInfoCount([In] [Out] ref int pcti);

			// Token: 0x06006D36 RID: 27958
			[PreserveSig]
			int GetInfoOfIndex(int iti, int dwFlags, [In] [Out] ref UnsafeNativeMethods.ITypeInfo pTypeInfo, int pTIFlags, int pcdispidReserved, IntPtr piidPrimary, IntPtr piidSource);
		}

		// Token: 0x020007B4 RID: 1972
		[StructLayout(LayoutKind.Sequential)]
		public class EVENTMSG
		{
			// Token: 0x04004148 RID: 16712
			public int message;

			// Token: 0x04004149 RID: 16713
			public int paramL;

			// Token: 0x0400414A RID: 16714
			public int paramH;

			// Token: 0x0400414B RID: 16715
			public int time;

			// Token: 0x0400414C RID: 16716
			public IntPtr hwnd;
		}

		// Token: 0x020007B5 RID: 1973
		[Guid("B196B283-BAB4-101A-B69C-00AA00341D07")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IProvideClassInfo
		{
			// Token: 0x06006D38 RID: 27960
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.ITypeInfo GetClassInfo();
		}

		// Token: 0x020007B6 RID: 1974
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagTYPEATTR
		{
			// Token: 0x06006D39 RID: 27961 RVA: 0x00190D04 File Offset: 0x0018EF04
			public NativeMethods.tagTYPEDESC Get_tdescAlias()
			{
				return new NativeMethods.tagTYPEDESC
				{
					unionMember = (IntPtr)this.tdescAlias_unionMember,
					vt = this.tdescAlias_vt
				};
			}

			// Token: 0x06006D3A RID: 27962 RVA: 0x00190D38 File Offset: 0x0018EF38
			public NativeMethods.tagIDLDESC Get_idldescType()
			{
				return new NativeMethods.tagIDLDESC
				{
					dwReserved = this.idldescType_dwReserved,
					wIDLFlags = this.idldescType_wIDLFlags
				};
			}

			// Token: 0x0400414D RID: 16717
			public Guid guid;

			// Token: 0x0400414E RID: 16718
			[MarshalAs(UnmanagedType.U4)]
			public int lcid;

			// Token: 0x0400414F RID: 16719
			[MarshalAs(UnmanagedType.U4)]
			public int dwReserved;

			// Token: 0x04004150 RID: 16720
			public int memidConstructor;

			// Token: 0x04004151 RID: 16721
			public int memidDestructor;

			// Token: 0x04004152 RID: 16722
			public IntPtr lpstrSchema = IntPtr.Zero;

			// Token: 0x04004153 RID: 16723
			[MarshalAs(UnmanagedType.U4)]
			public int cbSizeInstance;

			// Token: 0x04004154 RID: 16724
			public int typekind;

			// Token: 0x04004155 RID: 16725
			[MarshalAs(UnmanagedType.U2)]
			public short cFuncs;

			// Token: 0x04004156 RID: 16726
			[MarshalAs(UnmanagedType.U2)]
			public short cVars;

			// Token: 0x04004157 RID: 16727
			[MarshalAs(UnmanagedType.U2)]
			public short cImplTypes;

			// Token: 0x04004158 RID: 16728
			[MarshalAs(UnmanagedType.U2)]
			public short cbSizeVft;

			// Token: 0x04004159 RID: 16729
			[MarshalAs(UnmanagedType.U2)]
			public short cbAlignment;

			// Token: 0x0400415A RID: 16730
			[MarshalAs(UnmanagedType.U2)]
			public short wTypeFlags;

			// Token: 0x0400415B RID: 16731
			[MarshalAs(UnmanagedType.U2)]
			public short wMajorVerNum;

			// Token: 0x0400415C RID: 16732
			[MarshalAs(UnmanagedType.U2)]
			public short wMinorVerNum;

			// Token: 0x0400415D RID: 16733
			[MarshalAs(UnmanagedType.U4)]
			public int tdescAlias_unionMember;

			// Token: 0x0400415E RID: 16734
			[MarshalAs(UnmanagedType.U2)]
			public short tdescAlias_vt;

			// Token: 0x0400415F RID: 16735
			[MarshalAs(UnmanagedType.U4)]
			public int idldescType_dwReserved;

			// Token: 0x04004160 RID: 16736
			[MarshalAs(UnmanagedType.U2)]
			public short idldescType_wIDLFlags;
		}

		// Token: 0x020007B7 RID: 1975
		public enum tagVARFLAGS
		{
			// Token: 0x04004162 RID: 16738
			VARFLAG_FREADONLY = 1,
			// Token: 0x04004163 RID: 16739
			VARFLAG_FSOURCE,
			// Token: 0x04004164 RID: 16740
			VARFLAG_FBINDABLE = 4,
			// Token: 0x04004165 RID: 16741
			VARFLAG_FREQUESTEDIT = 8,
			// Token: 0x04004166 RID: 16742
			VARFLAG_FDISPLAYBIND = 16,
			// Token: 0x04004167 RID: 16743
			VARFLAG_FDEFAULTBIND = 32,
			// Token: 0x04004168 RID: 16744
			VARFLAG_FHIDDEN = 64,
			// Token: 0x04004169 RID: 16745
			VARFLAG_FDEFAULTCOLLELEM = 256,
			// Token: 0x0400416A RID: 16746
			VARFLAG_FUIDEFAULT = 512,
			// Token: 0x0400416B RID: 16747
			VARFLAG_FNONBROWSABLE = 1024,
			// Token: 0x0400416C RID: 16748
			VARFLAG_FREPLACEABLE = 2048,
			// Token: 0x0400416D RID: 16749
			VARFLAG_FIMMEDIATEBIND = 4096
		}

		// Token: 0x020007B8 RID: 1976
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagELEMDESC
		{
			// Token: 0x0400416E RID: 16750
			public NativeMethods.tagTYPEDESC tdesc;

			// Token: 0x0400416F RID: 16751
			public NativeMethods.tagPARAMDESC paramdesc;
		}

		// Token: 0x020007B9 RID: 1977
		public enum tagVARKIND
		{
			// Token: 0x04004171 RID: 16753
			VAR_PERINSTANCE,
			// Token: 0x04004172 RID: 16754
			VAR_STATIC,
			// Token: 0x04004173 RID: 16755
			VAR_CONST,
			// Token: 0x04004174 RID: 16756
			VAR_DISPATCH
		}

		// Token: 0x020007BA RID: 1978
		public struct tagIDLDESC
		{
			// Token: 0x04004175 RID: 16757
			[MarshalAs(UnmanagedType.U4)]
			public int dwReserved;

			// Token: 0x04004176 RID: 16758
			[MarshalAs(UnmanagedType.U2)]
			public short wIDLFlags;
		}

		// Token: 0x020007BB RID: 1979
		public struct RGBQUAD
		{
			// Token: 0x04004177 RID: 16759
			public byte rgbBlue;

			// Token: 0x04004178 RID: 16760
			public byte rgbGreen;

			// Token: 0x04004179 RID: 16761
			public byte rgbRed;

			// Token: 0x0400417A RID: 16762
			public byte rgbReserved;
		}

		// Token: 0x020007BC RID: 1980
		public struct PALETTEENTRY
		{
			// Token: 0x0400417B RID: 16763
			public byte peRed;

			// Token: 0x0400417C RID: 16764
			public byte peGreen;

			// Token: 0x0400417D RID: 16765
			public byte peBlue;

			// Token: 0x0400417E RID: 16766
			public byte peFlags;
		}

		// Token: 0x020007BD RID: 1981
		public struct BITMAPINFO_FLAT
		{
			// Token: 0x0400417F RID: 16767
			public int bmiHeader_biSize;

			// Token: 0x04004180 RID: 16768
			public int bmiHeader_biWidth;

			// Token: 0x04004181 RID: 16769
			public int bmiHeader_biHeight;

			// Token: 0x04004182 RID: 16770
			public short bmiHeader_biPlanes;

			// Token: 0x04004183 RID: 16771
			public short bmiHeader_biBitCount;

			// Token: 0x04004184 RID: 16772
			public int bmiHeader_biCompression;

			// Token: 0x04004185 RID: 16773
			public int bmiHeader_biSizeImage;

			// Token: 0x04004186 RID: 16774
			public int bmiHeader_biXPelsPerMeter;

			// Token: 0x04004187 RID: 16775
			public int bmiHeader_biYPelsPerMeter;

			// Token: 0x04004188 RID: 16776
			public int bmiHeader_biClrUsed;

			// Token: 0x04004189 RID: 16777
			public int bmiHeader_biClrImportant;

			// Token: 0x0400418A RID: 16778
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
			public byte[] bmiColors;
		}

		// Token: 0x020007BE RID: 1982
		public struct SYSTEM_POWER_STATUS
		{
			// Token: 0x0400418B RID: 16779
			public byte ACLineStatus;

			// Token: 0x0400418C RID: 16780
			public byte BatteryFlag;

			// Token: 0x0400418D RID: 16781
			public byte BatteryLifePercent;

			// Token: 0x0400418E RID: 16782
			public byte Reserved1;

			// Token: 0x0400418F RID: 16783
			public int BatteryLifeTime;

			// Token: 0x04004190 RID: 16784
			public int BatteryFullLifeTime;
		}

		// Token: 0x020007BF RID: 1983
		[StructLayout(LayoutKind.Sequential)]
		internal class DLLVERSIONINFO
		{
			// Token: 0x04004191 RID: 16785
			internal uint cbSize;

			// Token: 0x04004192 RID: 16786
			internal uint dwMajorVersion;

			// Token: 0x04004193 RID: 16787
			internal uint dwMinorVersion;

			// Token: 0x04004194 RID: 16788
			internal uint dwBuildNumber;

			// Token: 0x04004195 RID: 16789
			internal uint dwPlatformID;
		}

		// Token: 0x020007C0 RID: 1984
		public enum OLERENDER
		{
			// Token: 0x04004197 RID: 16791
			OLERENDER_NONE,
			// Token: 0x04004198 RID: 16792
			OLERENDER_DRAW,
			// Token: 0x04004199 RID: 16793
			OLERENDER_FORMAT,
			// Token: 0x0400419A RID: 16794
			OLERENDER_ASIS
		}

		// Token: 0x020007C1 RID: 1985
		public enum PROCESS_DPI_AWARENESS
		{
			// Token: 0x0400419C RID: 16796
			PROCESS_DPI_UNINITIALIZED = -1,
			// Token: 0x0400419D RID: 16797
			PROCESS_DPI_UNAWARE,
			// Token: 0x0400419E RID: 16798
			PROCESS_SYSTEM_DPI_AWARE,
			// Token: 0x0400419F RID: 16799
			PROCESS_PER_MONITOR_DPI_AWARE
		}

		// Token: 0x020007C2 RID: 1986
		public enum MONTCALENDAR_VIEW_MODE
		{
			// Token: 0x040041A1 RID: 16801
			MCMV_MONTH,
			// Token: 0x040041A2 RID: 16802
			MCMV_YEAR,
			// Token: 0x040041A3 RID: 16803
			MCMV_DECADE,
			// Token: 0x040041A4 RID: 16804
			MCMV_CENTURY
		}

		// Token: 0x020007C3 RID: 1987
		public struct UiaRect
		{
			// Token: 0x06006D3E RID: 27966 RVA: 0x00190D7B File Offset: 0x0018EF7B
			public UiaRect(Rectangle r)
			{
				this.left = (double)r.Left;
				this.top = (double)r.Top;
				this.width = (double)r.Width;
				this.height = (double)r.Height;
			}

			// Token: 0x040041A5 RID: 16805
			public double left;

			// Token: 0x040041A6 RID: 16806
			public double top;

			// Token: 0x040041A7 RID: 16807
			public double width;

			// Token: 0x040041A8 RID: 16808
			public double height;
		}

		// Token: 0x020007C4 RID: 1988
		internal class NtDll
		{
			// Token: 0x06006D3F RID: 27967
			[DllImport("ntdll.dll", EntryPoint = "RtlGetVersion", ExactSpelling = true)]
			private static extern int RtlGetVersionInternal(ref NativeMethods.NtDll.RTL_OSVERSIONINFOEX lpVersionInformation);

			// Token: 0x06006D40 RID: 27968 RVA: 0x00190DB8 File Offset: 0x0018EFB8
			internal unsafe static int RtlGetVersion(out NativeMethods.NtDll.RTL_OSVERSIONINFOEX versionInfo)
			{
				versionInfo = new NativeMethods.NtDll.RTL_OSVERSIONINFOEX
				{
					dwOSVersionInfoSize = (uint)sizeof(NativeMethods.NtDll.RTL_OSVERSIONINFOEX)
				};
				return NativeMethods.NtDll.RtlGetVersionInternal(ref versionInfo);
			}

			// Token: 0x020008C4 RID: 2244
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			internal struct RTL_OSVERSIONINFOEX
			{
				// Token: 0x04004542 RID: 17730
				internal uint dwOSVersionInfoSize;

				// Token: 0x04004543 RID: 17731
				internal uint dwMajorVersion;

				// Token: 0x04004544 RID: 17732
				internal uint dwMinorVersion;

				// Token: 0x04004545 RID: 17733
				internal uint dwBuildNumber;

				// Token: 0x04004546 RID: 17734
				internal uint dwPlatformId;

				// Token: 0x04004547 RID: 17735
				[FixedBuffer(typeof(char), 128)]
				internal NativeMethods.NtDll.RTL_OSVERSIONINFOEX.<szCSDVersion>e__FixedBuffer szCSDVersion;

				// Token: 0x04004548 RID: 17736
				internal ushort wServicePackMajor;

				// Token: 0x04004549 RID: 17737
				internal ushort wServicePackMinor;

				// Token: 0x0400454A RID: 17738
				internal ushort wSuiteMask;

				// Token: 0x0400454B RID: 17739
				internal byte wProductType;

				// Token: 0x0400454C RID: 17740
				internal byte wReserved;

				// Token: 0x02000985 RID: 2437
				[CompilerGenerated]
				[UnsafeValueType]
				[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Size = 256)]
				public struct <szCSDVersion>e__FixedBuffer
				{
					// Token: 0x040047D8 RID: 18392
					public char FixedElementField;
				}
			}
		}
	}
}
