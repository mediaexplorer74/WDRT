using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x020006A0 RID: 1696
	internal sealed class RegexNode
	{
		// Token: 0x06003F21 RID: 16161 RVA: 0x001072D9 File Offset: 0x001054D9
		internal RegexNode(int type, RegexOptions options)
		{
			this._type = type;
			this._options = options;
		}

		// Token: 0x06003F22 RID: 16162 RVA: 0x001072EF File Offset: 0x001054EF
		internal RegexNode(int type, RegexOptions options, char ch)
		{
			this._type = type;
			this._options = options;
			this._ch = ch;
		}

		// Token: 0x06003F23 RID: 16163 RVA: 0x0010730C File Offset: 0x0010550C
		internal RegexNode(int type, RegexOptions options, string str)
		{
			this._type = type;
			this._options = options;
			this._str = str;
		}

		// Token: 0x06003F24 RID: 16164 RVA: 0x00107329 File Offset: 0x00105529
		internal RegexNode(int type, RegexOptions options, int m)
		{
			this._type = type;
			this._options = options;
			this._m = m;
		}

		// Token: 0x06003F25 RID: 16165 RVA: 0x00107346 File Offset: 0x00105546
		internal RegexNode(int type, RegexOptions options, int m, int n)
		{
			this._type = type;
			this._options = options;
			this._m = m;
			this._n = n;
		}

		// Token: 0x06003F26 RID: 16166 RVA: 0x0010736B File Offset: 0x0010556B
		internal bool UseOptionR()
		{
			return (this._options & RegexOptions.RightToLeft) > RegexOptions.None;
		}

		// Token: 0x06003F27 RID: 16167 RVA: 0x00107379 File Offset: 0x00105579
		internal RegexNode ReverseLeft()
		{
			if (this.UseOptionR() && this._type == 25 && this._children != null)
			{
				this._children.Reverse(0, this._children.Count);
			}
			return this;
		}

		// Token: 0x06003F28 RID: 16168 RVA: 0x001073AD File Offset: 0x001055AD
		internal void MakeRep(int type, int min, int max)
		{
			this._type += type - 9;
			this._m = min;
			this._n = max;
		}

		// Token: 0x06003F29 RID: 16169 RVA: 0x001073D0 File Offset: 0x001055D0
		internal RegexNode Reduce()
		{
			int num = this.Type();
			RegexNode regexNode;
			if (num != 5 && num != 11)
			{
				switch (num)
				{
				case 24:
					return this.ReduceAlternation();
				case 25:
					return this.ReduceConcatenation();
				case 26:
				case 27:
					return this.ReduceRep();
				case 29:
					return this.ReduceGroup();
				}
				regexNode = this;
			}
			else
			{
				regexNode = this.ReduceSet();
			}
			return regexNode;
		}

		// Token: 0x06003F2A RID: 16170 RVA: 0x00107440 File Offset: 0x00105640
		internal RegexNode StripEnation(int emptyType)
		{
			int num = this.ChildCount();
			if (num == 0)
			{
				return new RegexNode(emptyType, this._options);
			}
			if (num != 1)
			{
				return this;
			}
			return this.Child(0);
		}

		// Token: 0x06003F2B RID: 16171 RVA: 0x00107474 File Offset: 0x00105674
		internal RegexNode ReduceGroup()
		{
			RegexNode regexNode = this;
			while (regexNode.Type() == 29)
			{
				regexNode = regexNode.Child(0);
			}
			return regexNode;
		}

		// Token: 0x06003F2C RID: 16172 RVA: 0x00107498 File Offset: 0x00105698
		internal RegexNode ReduceRep()
		{
			RegexNode regexNode = this;
			int num = this.Type();
			int num2 = this._m;
			int num3 = this._n;
			while (regexNode.ChildCount() != 0)
			{
				RegexNode regexNode2 = regexNode.Child(0);
				if (regexNode2.Type() != num)
				{
					int num4 = regexNode2.Type();
					if ((num4 < 3 || num4 > 5 || num != 26) && (num4 < 6 || num4 > 8 || num != 27))
					{
						break;
					}
				}
				if ((regexNode._m == 0 && regexNode2._m > 1) || regexNode2._n < regexNode2._m * 2)
				{
					break;
				}
				regexNode = regexNode2;
				if (regexNode._m > 0)
				{
					num2 = (regexNode._m = ((2147483646 / regexNode._m < num2) ? int.MaxValue : (regexNode._m * num2)));
				}
				if (regexNode._n > 0)
				{
					num3 = (regexNode._n = ((2147483646 / regexNode._n < num3) ? int.MaxValue : (regexNode._n * num3)));
				}
			}
			if (num2 != 2147483647)
			{
				return regexNode;
			}
			return new RegexNode(22, this._options);
		}

		// Token: 0x06003F2D RID: 16173 RVA: 0x001075AC File Offset: 0x001057AC
		internal RegexNode ReduceSet()
		{
			if (RegexCharClass.IsEmpty(this._str))
			{
				this._type = 22;
				this._str = null;
			}
			else if (RegexCharClass.IsSingleton(this._str))
			{
				this._ch = RegexCharClass.SingletonChar(this._str);
				this._str = null;
				this._type += -2;
			}
			else if (RegexCharClass.IsSingletonInverse(this._str))
			{
				this._ch = RegexCharClass.SingletonChar(this._str);
				this._str = null;
				this._type += -1;
			}
			return this;
		}

		// Token: 0x06003F2E RID: 16174 RVA: 0x00107644 File Offset: 0x00105844
		internal RegexNode ReduceAlternation()
		{
			if (this._children == null)
			{
				return new RegexNode(22, this._options);
			}
			bool flag = false;
			bool flag2 = false;
			RegexOptions regexOptions = RegexOptions.None;
			int i = 0;
			int num = 0;
			while (i < this._children.Count)
			{
				RegexNode regexNode = this._children[i];
				if (num < i)
				{
					this._children[num] = regexNode;
				}
				if (regexNode._type == 24)
				{
					for (int j = 0; j < regexNode._children.Count; j++)
					{
						regexNode._children[j]._next = this;
					}
					this._children.InsertRange(i + 1, regexNode._children);
					num--;
				}
				else if (regexNode._type == 11 || regexNode._type == 9)
				{
					RegexOptions regexOptions2 = regexNode._options & (RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
					if (regexNode._type == 11)
					{
						if (!flag || regexOptions != regexOptions2 || flag2 || !RegexCharClass.IsMergeable(regexNode._str))
						{
							flag = true;
							flag2 = !RegexCharClass.IsMergeable(regexNode._str);
							regexOptions = regexOptions2;
							goto IL_1D0;
						}
					}
					else if (!flag || regexOptions != regexOptions2 || flag2)
					{
						flag = true;
						flag2 = false;
						regexOptions = regexOptions2;
						goto IL_1D0;
					}
					num--;
					RegexNode regexNode2 = this._children[num];
					RegexCharClass regexCharClass;
					if (regexNode2._type == 9)
					{
						regexCharClass = new RegexCharClass();
						regexCharClass.AddChar(regexNode2._ch);
					}
					else
					{
						regexCharClass = RegexCharClass.Parse(regexNode2._str);
					}
					if (regexNode._type == 9)
					{
						regexCharClass.AddChar(regexNode._ch);
					}
					else
					{
						RegexCharClass regexCharClass2 = RegexCharClass.Parse(regexNode._str);
						regexCharClass.AddCharClass(regexCharClass2);
					}
					regexNode2._type = 11;
					regexNode2._str = regexCharClass.ToStringClass();
				}
				else if (regexNode._type == 22)
				{
					num--;
				}
				else
				{
					flag = false;
					flag2 = false;
				}
				IL_1D0:
				i++;
				num++;
			}
			if (num < i)
			{
				this._children.RemoveRange(num, i - num);
			}
			return this.StripEnation(22);
		}

		// Token: 0x06003F2F RID: 16175 RVA: 0x00107860 File Offset: 0x00105A60
		internal RegexNode ReduceConcatenation()
		{
			if (this._children == null)
			{
				return new RegexNode(23, this._options);
			}
			bool flag = false;
			RegexOptions regexOptions = RegexOptions.None;
			int i = 0;
			int num = 0;
			while (i < this._children.Count)
			{
				RegexNode regexNode = this._children[i];
				if (num < i)
				{
					this._children[num] = regexNode;
				}
				if (regexNode._type == 25 && (regexNode._options & RegexOptions.RightToLeft) == (this._options & RegexOptions.RightToLeft))
				{
					for (int j = 0; j < regexNode._children.Count; j++)
					{
						regexNode._children[j]._next = this;
					}
					this._children.InsertRange(i + 1, regexNode._children);
					num--;
				}
				else if (regexNode._type == 12 || regexNode._type == 9)
				{
					RegexOptions regexOptions2 = regexNode._options & (RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
					if (!flag || regexOptions != regexOptions2)
					{
						flag = true;
						regexOptions = regexOptions2;
					}
					else
					{
						RegexNode regexNode2 = this._children[--num];
						if (regexNode2._type == 9)
						{
							regexNode2._type = 12;
							regexNode2._str = Convert.ToString(regexNode2._ch, CultureInfo.InvariantCulture);
						}
						if ((regexOptions2 & RegexOptions.RightToLeft) == RegexOptions.None)
						{
							if (regexNode._type == 9)
							{
								RegexNode regexNode3 = regexNode2;
								regexNode3._str += regexNode._ch.ToString();
							}
							else
							{
								RegexNode regexNode4 = regexNode2;
								regexNode4._str += regexNode._str;
							}
						}
						else if (regexNode._type == 9)
						{
							regexNode2._str = regexNode._ch.ToString() + regexNode2._str;
						}
						else
						{
							regexNode2._str = regexNode._str + regexNode2._str;
						}
					}
				}
				else if (regexNode._type == 23)
				{
					num--;
				}
				else
				{
					flag = false;
				}
				i++;
				num++;
			}
			if (num < i)
			{
				this._children.RemoveRange(num, i - num);
			}
			return this.StripEnation(23);
		}

		// Token: 0x06003F30 RID: 16176 RVA: 0x00107A78 File Offset: 0x00105C78
		internal RegexNode MakeQuantifier(bool lazy, int min, int max)
		{
			if (min == 0 && max == 0)
			{
				return new RegexNode(23, this._options);
			}
			if (min == 1 && max == 1)
			{
				return this;
			}
			int type = this._type;
			if (type - 9 <= 2)
			{
				this.MakeRep(lazy ? 6 : 3, min, max);
				return this;
			}
			RegexNode regexNode = new RegexNode(lazy ? 27 : 26, this._options, min, max);
			regexNode.AddChild(this);
			return regexNode;
		}

		// Token: 0x06003F31 RID: 16177 RVA: 0x00107AE4 File Offset: 0x00105CE4
		internal void AddChild(RegexNode newChild)
		{
			if (this._children == null)
			{
				this._children = new List<RegexNode>(4);
			}
			RegexNode regexNode = newChild.Reduce();
			this._children.Add(regexNode);
			regexNode._next = this;
		}

		// Token: 0x06003F32 RID: 16178 RVA: 0x00107B1F File Offset: 0x00105D1F
		internal RegexNode Child(int i)
		{
			return this._children[i];
		}

		// Token: 0x06003F33 RID: 16179 RVA: 0x00107B2D File Offset: 0x00105D2D
		internal int ChildCount()
		{
			if (this._children != null)
			{
				return this._children.Count;
			}
			return 0;
		}

		// Token: 0x06003F34 RID: 16180 RVA: 0x00107B44 File Offset: 0x00105D44
		internal int Type()
		{
			return this._type;
		}

		// Token: 0x04002DF2 RID: 11762
		internal const int Oneloop = 3;

		// Token: 0x04002DF3 RID: 11763
		internal const int Notoneloop = 4;

		// Token: 0x04002DF4 RID: 11764
		internal const int Setloop = 5;

		// Token: 0x04002DF5 RID: 11765
		internal const int Onelazy = 6;

		// Token: 0x04002DF6 RID: 11766
		internal const int Notonelazy = 7;

		// Token: 0x04002DF7 RID: 11767
		internal const int Setlazy = 8;

		// Token: 0x04002DF8 RID: 11768
		internal const int One = 9;

		// Token: 0x04002DF9 RID: 11769
		internal const int Notone = 10;

		// Token: 0x04002DFA RID: 11770
		internal const int Set = 11;

		// Token: 0x04002DFB RID: 11771
		internal const int Multi = 12;

		// Token: 0x04002DFC RID: 11772
		internal const int Ref = 13;

		// Token: 0x04002DFD RID: 11773
		internal const int Bol = 14;

		// Token: 0x04002DFE RID: 11774
		internal const int Eol = 15;

		// Token: 0x04002DFF RID: 11775
		internal const int Boundary = 16;

		// Token: 0x04002E00 RID: 11776
		internal const int Nonboundary = 17;

		// Token: 0x04002E01 RID: 11777
		internal const int ECMABoundary = 41;

		// Token: 0x04002E02 RID: 11778
		internal const int NonECMABoundary = 42;

		// Token: 0x04002E03 RID: 11779
		internal const int Beginning = 18;

		// Token: 0x04002E04 RID: 11780
		internal const int Start = 19;

		// Token: 0x04002E05 RID: 11781
		internal const int EndZ = 20;

		// Token: 0x04002E06 RID: 11782
		internal const int End = 21;

		// Token: 0x04002E07 RID: 11783
		internal const int Nothing = 22;

		// Token: 0x04002E08 RID: 11784
		internal const int Empty = 23;

		// Token: 0x04002E09 RID: 11785
		internal const int Alternate = 24;

		// Token: 0x04002E0A RID: 11786
		internal const int Concatenate = 25;

		// Token: 0x04002E0B RID: 11787
		internal const int Loop = 26;

		// Token: 0x04002E0C RID: 11788
		internal const int Lazyloop = 27;

		// Token: 0x04002E0D RID: 11789
		internal const int Capture = 28;

		// Token: 0x04002E0E RID: 11790
		internal const int Group = 29;

		// Token: 0x04002E0F RID: 11791
		internal const int Require = 30;

		// Token: 0x04002E10 RID: 11792
		internal const int Prevent = 31;

		// Token: 0x04002E11 RID: 11793
		internal const int Greedy = 32;

		// Token: 0x04002E12 RID: 11794
		internal const int Testref = 33;

		// Token: 0x04002E13 RID: 11795
		internal const int Testgroup = 34;

		// Token: 0x04002E14 RID: 11796
		internal int _type;

		// Token: 0x04002E15 RID: 11797
		internal List<RegexNode> _children;

		// Token: 0x04002E16 RID: 11798
		internal string _str;

		// Token: 0x04002E17 RID: 11799
		internal char _ch;

		// Token: 0x04002E18 RID: 11800
		internal int _m;

		// Token: 0x04002E19 RID: 11801
		internal int _n;

		// Token: 0x04002E1A RID: 11802
		internal RegexOptions _options;

		// Token: 0x04002E1B RID: 11803
		internal RegexNode _next;
	}
}
