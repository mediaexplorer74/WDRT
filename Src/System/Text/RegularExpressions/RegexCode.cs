using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200068F RID: 1679
	internal sealed class RegexCode
	{
		// Token: 0x06003E17 RID: 15895 RVA: 0x000FFE74 File Offset: 0x000FE074
		internal RegexCode(int[] codes, List<string> stringlist, int trackcount, Hashtable caps, int capsize, RegexBoyerMoore bmPrefix, RegexPrefix fcPrefix, int anchors, bool rightToLeft)
		{
			this._codes = codes;
			this._strings = new string[stringlist.Count];
			this._trackcount = trackcount;
			this._caps = caps;
			this._capsize = capsize;
			this._bmPrefix = bmPrefix;
			this._fcPrefix = fcPrefix;
			this._anchors = anchors;
			this._rightToLeft = rightToLeft;
			stringlist.CopyTo(0, this._strings, 0, stringlist.Count);
		}

		// Token: 0x06003E18 RID: 15896 RVA: 0x000FFEEC File Offset: 0x000FE0EC
		internal static bool OpcodeBacktracks(int Op)
		{
			Op &= 63;
			switch (Op)
			{
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 23:
			case 24:
			case 25:
			case 26:
			case 27:
			case 28:
			case 29:
			case 31:
			case 32:
			case 33:
			case 34:
			case 35:
			case 36:
			case 38:
				return true;
			}
			return false;
		}

		// Token: 0x06003E19 RID: 15897 RVA: 0x000FFF9C File Offset: 0x000FE19C
		internal static int OpcodeSize(int Opcode)
		{
			Opcode &= 63;
			switch (Opcode)
			{
			case 0:
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 28:
			case 29:
			case 32:
				return 3;
			case 9:
			case 10:
			case 11:
			case 12:
			case 13:
			case 23:
			case 24:
			case 25:
			case 26:
			case 27:
			case 37:
			case 38:
			case 39:
				return 2;
			case 14:
			case 15:
			case 16:
			case 17:
			case 18:
			case 19:
			case 20:
			case 21:
			case 22:
			case 30:
			case 31:
			case 33:
			case 34:
			case 35:
			case 36:
			case 40:
			case 41:
			case 42:
				return 1;
			default:
				throw RegexCode.MakeException(SR.GetString("UnexpectedOpcode", new object[] { Opcode.ToString(CultureInfo.CurrentCulture) }));
			}
		}

		// Token: 0x06003E1A RID: 15898 RVA: 0x0010008D File Offset: 0x000FE28D
		internal static ArgumentException MakeException(string message)
		{
			return new ArgumentException(message);
		}

		// Token: 0x04002D1C RID: 11548
		internal const int Onerep = 0;

		// Token: 0x04002D1D RID: 11549
		internal const int Notonerep = 1;

		// Token: 0x04002D1E RID: 11550
		internal const int Setrep = 2;

		// Token: 0x04002D1F RID: 11551
		internal const int Oneloop = 3;

		// Token: 0x04002D20 RID: 11552
		internal const int Notoneloop = 4;

		// Token: 0x04002D21 RID: 11553
		internal const int Setloop = 5;

		// Token: 0x04002D22 RID: 11554
		internal const int Onelazy = 6;

		// Token: 0x04002D23 RID: 11555
		internal const int Notonelazy = 7;

		// Token: 0x04002D24 RID: 11556
		internal const int Setlazy = 8;

		// Token: 0x04002D25 RID: 11557
		internal const int One = 9;

		// Token: 0x04002D26 RID: 11558
		internal const int Notone = 10;

		// Token: 0x04002D27 RID: 11559
		internal const int Set = 11;

		// Token: 0x04002D28 RID: 11560
		internal const int Multi = 12;

		// Token: 0x04002D29 RID: 11561
		internal const int Ref = 13;

		// Token: 0x04002D2A RID: 11562
		internal const int Bol = 14;

		// Token: 0x04002D2B RID: 11563
		internal const int Eol = 15;

		// Token: 0x04002D2C RID: 11564
		internal const int Boundary = 16;

		// Token: 0x04002D2D RID: 11565
		internal const int Nonboundary = 17;

		// Token: 0x04002D2E RID: 11566
		internal const int Beginning = 18;

		// Token: 0x04002D2F RID: 11567
		internal const int Start = 19;

		// Token: 0x04002D30 RID: 11568
		internal const int EndZ = 20;

		// Token: 0x04002D31 RID: 11569
		internal const int End = 21;

		// Token: 0x04002D32 RID: 11570
		internal const int Nothing = 22;

		// Token: 0x04002D33 RID: 11571
		internal const int Lazybranch = 23;

		// Token: 0x04002D34 RID: 11572
		internal const int Branchmark = 24;

		// Token: 0x04002D35 RID: 11573
		internal const int Lazybranchmark = 25;

		// Token: 0x04002D36 RID: 11574
		internal const int Nullcount = 26;

		// Token: 0x04002D37 RID: 11575
		internal const int Setcount = 27;

		// Token: 0x04002D38 RID: 11576
		internal const int Branchcount = 28;

		// Token: 0x04002D39 RID: 11577
		internal const int Lazybranchcount = 29;

		// Token: 0x04002D3A RID: 11578
		internal const int Nullmark = 30;

		// Token: 0x04002D3B RID: 11579
		internal const int Setmark = 31;

		// Token: 0x04002D3C RID: 11580
		internal const int Capturemark = 32;

		// Token: 0x04002D3D RID: 11581
		internal const int Getmark = 33;

		// Token: 0x04002D3E RID: 11582
		internal const int Setjump = 34;

		// Token: 0x04002D3F RID: 11583
		internal const int Backjump = 35;

		// Token: 0x04002D40 RID: 11584
		internal const int Forejump = 36;

		// Token: 0x04002D41 RID: 11585
		internal const int Testref = 37;

		// Token: 0x04002D42 RID: 11586
		internal const int Goto = 38;

		// Token: 0x04002D43 RID: 11587
		internal const int Prune = 39;

		// Token: 0x04002D44 RID: 11588
		internal const int Stop = 40;

		// Token: 0x04002D45 RID: 11589
		internal const int ECMABoundary = 41;

		// Token: 0x04002D46 RID: 11590
		internal const int NonECMABoundary = 42;

		// Token: 0x04002D47 RID: 11591
		internal const int Mask = 63;

		// Token: 0x04002D48 RID: 11592
		internal const int Rtl = 64;

		// Token: 0x04002D49 RID: 11593
		internal const int Back = 128;

		// Token: 0x04002D4A RID: 11594
		internal const int Back2 = 256;

		// Token: 0x04002D4B RID: 11595
		internal const int Ci = 512;

		// Token: 0x04002D4C RID: 11596
		internal int[] _codes;

		// Token: 0x04002D4D RID: 11597
		internal string[] _strings;

		// Token: 0x04002D4E RID: 11598
		internal int _trackcount;

		// Token: 0x04002D4F RID: 11599
		internal Hashtable _caps;

		// Token: 0x04002D50 RID: 11600
		internal int _capsize;

		// Token: 0x04002D51 RID: 11601
		internal RegexPrefix _fcPrefix;

		// Token: 0x04002D52 RID: 11602
		internal RegexBoyerMoore _bmPrefix;

		// Token: 0x04002D53 RID: 11603
		internal int _anchors;

		// Token: 0x04002D54 RID: 11604
		internal bool _rightToLeft;
	}
}
