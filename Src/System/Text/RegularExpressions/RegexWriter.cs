using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x020006A7 RID: 1703
	internal sealed class RegexWriter
	{
		// Token: 0x06003FAB RID: 16299 RVA: 0x0010B564 File Offset: 0x00109764
		internal static RegexCode Write(RegexTree t)
		{
			RegexWriter regexWriter = new RegexWriter();
			return regexWriter.RegexCodeFromRegexTree(t);
		}

		// Token: 0x06003FAC RID: 16300 RVA: 0x0010B580 File Offset: 0x00109780
		private RegexWriter()
		{
			this._intStack = new int[32];
			this._emitted = new int[32];
			this._stringhash = new Dictionary<string, int>();
			this._stringtable = new List<string>();
		}

		// Token: 0x06003FAD RID: 16301 RVA: 0x0010B5B8 File Offset: 0x001097B8
		internal void PushInt(int I)
		{
			if (this._depth >= this._intStack.Length)
			{
				int[] array = new int[this._depth * 2];
				Array.Copy(this._intStack, 0, array, 0, this._depth);
				this._intStack = array;
			}
			int[] intStack = this._intStack;
			int depth = this._depth;
			this._depth = depth + 1;
			intStack[depth] = I;
		}

		// Token: 0x06003FAE RID: 16302 RVA: 0x0010B617 File Offset: 0x00109817
		internal bool EmptyStack()
		{
			return this._depth == 0;
		}

		// Token: 0x06003FAF RID: 16303 RVA: 0x0010B624 File Offset: 0x00109824
		internal int PopInt()
		{
			int[] intStack = this._intStack;
			int num = this._depth - 1;
			this._depth = num;
			return intStack[num];
		}

		// Token: 0x06003FB0 RID: 16304 RVA: 0x0010B649 File Offset: 0x00109849
		internal int CurPos()
		{
			return this._curpos;
		}

		// Token: 0x06003FB1 RID: 16305 RVA: 0x0010B651 File Offset: 0x00109851
		internal void PatchJump(int Offset, int jumpDest)
		{
			this._emitted[Offset + 1] = jumpDest;
		}

		// Token: 0x06003FB2 RID: 16306 RVA: 0x0010B660 File Offset: 0x00109860
		internal void Emit(int op)
		{
			if (this._counting)
			{
				this._count++;
				if (RegexCode.OpcodeBacktracks(op))
				{
					this._trackcount++;
				}
				return;
			}
			int[] emitted = this._emitted;
			int curpos = this._curpos;
			this._curpos = curpos + 1;
			emitted[curpos] = op;
		}

		// Token: 0x06003FB3 RID: 16307 RVA: 0x0010B6B4 File Offset: 0x001098B4
		internal void Emit(int op, int opd1)
		{
			if (this._counting)
			{
				this._count += 2;
				if (RegexCode.OpcodeBacktracks(op))
				{
					this._trackcount++;
				}
				return;
			}
			int[] emitted = this._emitted;
			int num = this._curpos;
			this._curpos = num + 1;
			emitted[num] = op;
			int[] emitted2 = this._emitted;
			num = this._curpos;
			this._curpos = num + 1;
			emitted2[num] = opd1;
		}

		// Token: 0x06003FB4 RID: 16308 RVA: 0x0010B720 File Offset: 0x00109920
		internal void Emit(int op, int opd1, int opd2)
		{
			if (this._counting)
			{
				this._count += 3;
				if (RegexCode.OpcodeBacktracks(op))
				{
					this._trackcount++;
				}
				return;
			}
			int[] emitted = this._emitted;
			int num = this._curpos;
			this._curpos = num + 1;
			emitted[num] = op;
			int[] emitted2 = this._emitted;
			num = this._curpos;
			this._curpos = num + 1;
			emitted2[num] = opd1;
			int[] emitted3 = this._emitted;
			num = this._curpos;
			this._curpos = num + 1;
			emitted3[num] = opd2;
		}

		// Token: 0x06003FB5 RID: 16309 RVA: 0x0010B7A8 File Offset: 0x001099A8
		internal int StringCode(string str)
		{
			if (this._counting)
			{
				return 0;
			}
			if (str == null)
			{
				str = string.Empty;
			}
			int num;
			if (this._stringhash.ContainsKey(str))
			{
				num = this._stringhash[str];
			}
			else
			{
				num = this._stringtable.Count;
				this._stringhash[str] = num;
				this._stringtable.Add(str);
			}
			return num;
		}

		// Token: 0x06003FB6 RID: 16310 RVA: 0x0010B80C File Offset: 0x00109A0C
		internal ArgumentException MakeException(string message)
		{
			return new ArgumentException(message);
		}

		// Token: 0x06003FB7 RID: 16311 RVA: 0x0010B814 File Offset: 0x00109A14
		internal int MapCapnum(int capnum)
		{
			if (capnum == -1)
			{
				return -1;
			}
			if (this._caps != null)
			{
				return (int)this._caps[capnum];
			}
			return capnum;
		}

		// Token: 0x06003FB8 RID: 16312 RVA: 0x0010B83C File Offset: 0x00109A3C
		internal RegexCode RegexCodeFromRegexTree(RegexTree tree)
		{
			int num;
			if (tree._capnumlist == null || tree._captop == tree._capnumlist.Length)
			{
				num = tree._captop;
				this._caps = null;
			}
			else
			{
				num = tree._capnumlist.Length;
				this._caps = tree._caps;
				for (int i = 0; i < tree._capnumlist.Length; i++)
				{
					this._caps[tree._capnumlist[i]] = i;
				}
			}
			this._counting = true;
			for (;;)
			{
				if (!this._counting)
				{
					this._emitted = new int[this._count];
				}
				RegexNode regexNode = tree._root;
				int num2 = 0;
				this.Emit(23, 0);
				for (;;)
				{
					if (regexNode._children == null)
					{
						this.EmitFragment(regexNode._type, regexNode, 0);
					}
					else if (num2 < regexNode._children.Count)
					{
						this.EmitFragment(regexNode._type | 64, regexNode, num2);
						regexNode = regexNode._children[num2];
						this.PushInt(num2);
						num2 = 0;
						continue;
					}
					if (this.EmptyStack())
					{
						break;
					}
					num2 = this.PopInt();
					regexNode = regexNode._next;
					this.EmitFragment(regexNode._type | 128, regexNode, num2);
					num2++;
				}
				this.PatchJump(0, this.CurPos());
				this.Emit(40);
				if (!this._counting)
				{
					break;
				}
				this._counting = false;
			}
			RegexPrefix regexPrefix = RegexFCD.FirstChars(tree);
			RegexPrefix regexPrefix2 = RegexFCD.Prefix(tree);
			bool flag = (tree._options & RegexOptions.RightToLeft) > RegexOptions.None;
			CultureInfo cultureInfo = (((tree._options & RegexOptions.CultureInvariant) != RegexOptions.None) ? CultureInfo.InvariantCulture : CultureInfo.CurrentCulture);
			RegexBoyerMoore regexBoyerMoore;
			if (regexPrefix2 != null && regexPrefix2.Prefix.Length > 0)
			{
				regexBoyerMoore = new RegexBoyerMoore(regexPrefix2.Prefix, regexPrefix2.CaseInsensitive, flag, cultureInfo);
			}
			else
			{
				regexBoyerMoore = null;
			}
			int num3 = RegexFCD.Anchors(tree);
			return new RegexCode(this._emitted, this._stringtable, this._trackcount, this._caps, num, regexBoyerMoore, regexPrefix, num3, flag);
		}

		// Token: 0x06003FB9 RID: 16313 RVA: 0x0010BA30 File Offset: 0x00109C30
		internal void EmitFragment(int nodetype, RegexNode node, int CurIndex)
		{
			int num = 0;
			if (nodetype <= 13)
			{
				if (node.UseOptionR())
				{
					num |= 64;
				}
				if ((node._options & RegexOptions.IgnoreCase) != RegexOptions.None)
				{
					num |= 512;
				}
			}
			switch (nodetype)
			{
			case 3:
			case 4:
			case 6:
			case 7:
				if (node._m > 0)
				{
					this.Emit(((node._type == 3 || node._type == 6) ? 0 : 1) | num, (int)node._ch, node._m);
				}
				if (node._n > node._m)
				{
					this.Emit(node._type | num, (int)node._ch, (node._n == int.MaxValue) ? int.MaxValue : (node._n - node._m));
					return;
				}
				return;
			case 5:
			case 8:
				if (node._m > 0)
				{
					this.Emit(2 | num, this.StringCode(node._str), node._m);
				}
				if (node._n > node._m)
				{
					this.Emit(node._type | num, this.StringCode(node._str), (node._n == int.MaxValue) ? int.MaxValue : (node._n - node._m));
					return;
				}
				return;
			case 9:
			case 10:
				this.Emit(node._type | num, (int)node._ch);
				return;
			case 11:
				this.Emit(node._type | num, this.StringCode(node._str));
				return;
			case 12:
				this.Emit(node._type | num, this.StringCode(node._str));
				return;
			case 13:
				this.Emit(node._type | num, this.MapCapnum(node._m));
				return;
			case 14:
			case 15:
			case 16:
			case 17:
			case 18:
			case 19:
			case 20:
			case 21:
			case 22:
			case 41:
			case 42:
				this.Emit(node._type);
				return;
			case 23:
				return;
			case 24:
			case 25:
			case 26:
			case 27:
			case 28:
			case 29:
			case 30:
			case 31:
			case 32:
			case 33:
			case 34:
			case 35:
			case 36:
			case 37:
			case 38:
			case 39:
			case 40:
				break;
			default:
				switch (nodetype)
				{
				case 88:
					if (CurIndex < node._children.Count - 1)
					{
						this.PushInt(this.CurPos());
						this.Emit(23, 0);
						return;
					}
					return;
				case 89:
				case 93:
					return;
				case 90:
				case 91:
					if (node._n < 2147483647 || node._m > 1)
					{
						this.Emit((node._m == 0) ? 26 : 27, (node._m == 0) ? 0 : (1 - node._m));
					}
					else
					{
						this.Emit((node._m == 0) ? 30 : 31);
					}
					if (node._m == 0)
					{
						this.PushInt(this.CurPos());
						this.Emit(38, 0);
					}
					this.PushInt(this.CurPos());
					return;
				case 92:
					this.Emit(31);
					return;
				case 94:
					this.Emit(34);
					this.Emit(31);
					return;
				case 95:
					this.Emit(34);
					this.PushInt(this.CurPos());
					this.Emit(23, 0);
					return;
				case 96:
					this.Emit(34);
					return;
				case 97:
					if (CurIndex == 0)
					{
						this.Emit(34);
						this.PushInt(this.CurPos());
						this.Emit(23, 0);
						this.Emit(37, this.MapCapnum(node._m));
						this.Emit(36);
						return;
					}
					return;
				case 98:
					if (CurIndex == 0)
					{
						this.Emit(34);
						this.Emit(31);
						this.PushInt(this.CurPos());
						this.Emit(23, 0);
						return;
					}
					return;
				default:
					switch (nodetype)
					{
					case 152:
					{
						if (CurIndex < node._children.Count - 1)
						{
							int num2 = this.PopInt();
							this.PushInt(this.CurPos());
							this.Emit(38, 0);
							this.PatchJump(num2, this.CurPos());
							return;
						}
						for (int i = 0; i < CurIndex; i++)
						{
							this.PatchJump(this.PopInt(), this.CurPos());
						}
						return;
					}
					case 153:
					case 157:
						return;
					case 154:
					case 155:
					{
						int num3 = this.CurPos();
						int num4 = nodetype - 154;
						if (node._n < 2147483647 || node._m > 1)
						{
							this.Emit(28 + num4, this.PopInt(), (node._n == int.MaxValue) ? int.MaxValue : (node._n - node._m));
						}
						else
						{
							this.Emit(24 + num4, this.PopInt());
						}
						if (node._m == 0)
						{
							this.PatchJump(this.PopInt(), num3);
							return;
						}
						return;
					}
					case 156:
						this.Emit(32, this.MapCapnum(node._m), this.MapCapnum(node._n));
						return;
					case 158:
						this.Emit(33);
						this.Emit(36);
						return;
					case 159:
						this.Emit(35);
						this.PatchJump(this.PopInt(), this.CurPos());
						this.Emit(36);
						return;
					case 160:
						this.Emit(36);
						return;
					case 161:
						if (CurIndex != 0)
						{
							if (CurIndex != 1)
							{
								return;
							}
						}
						else
						{
							int num5 = this.PopInt();
							this.PushInt(this.CurPos());
							this.Emit(38, 0);
							this.PatchJump(num5, this.CurPos());
							this.Emit(36);
							if (node._children.Count > 1)
							{
								return;
							}
						}
						this.PatchJump(this.PopInt(), this.CurPos());
						return;
					case 162:
						switch (CurIndex)
						{
						case 0:
							this.Emit(33);
							this.Emit(36);
							return;
						case 1:
						{
							int num6 = this.PopInt();
							this.PushInt(this.CurPos());
							this.Emit(38, 0);
							this.PatchJump(num6, this.CurPos());
							this.Emit(33);
							this.Emit(36);
							if (node._children.Count > 2)
							{
								return;
							}
							break;
						}
						case 2:
							break;
						default:
							return;
						}
						this.PatchJump(this.PopInt(), this.CurPos());
						return;
					}
					break;
				}
				break;
			}
			throw this.MakeException(SR.GetString("UnexpectedOpcode", new object[] { nodetype.ToString(CultureInfo.CurrentCulture) }));
		}

		// Token: 0x04002E64 RID: 11876
		internal int[] _intStack;

		// Token: 0x04002E65 RID: 11877
		internal int _depth;

		// Token: 0x04002E66 RID: 11878
		internal int[] _emitted;

		// Token: 0x04002E67 RID: 11879
		internal int _curpos;

		// Token: 0x04002E68 RID: 11880
		internal Dictionary<string, int> _stringhash;

		// Token: 0x04002E69 RID: 11881
		internal List<string> _stringtable;

		// Token: 0x04002E6A RID: 11882
		internal bool _counting;

		// Token: 0x04002E6B RID: 11883
		internal int _count;

		// Token: 0x04002E6C RID: 11884
		internal int _trackcount;

		// Token: 0x04002E6D RID: 11885
		internal Hashtable _caps;

		// Token: 0x04002E6E RID: 11886
		internal const int BeforeChild = 64;

		// Token: 0x04002E6F RID: 11887
		internal const int AfterChild = 128;
	}
}
