using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x020006A2 RID: 1698
	internal sealed class RegexParser
	{
		// Token: 0x06003F35 RID: 16181 RVA: 0x00107B4C File Offset: 0x00105D4C
		internal static RegexTree Parse(string re, RegexOptions op)
		{
			RegexParser regexParser = new RegexParser(((op & RegexOptions.CultureInvariant) != RegexOptions.None) ? CultureInfo.InvariantCulture : CultureInfo.CurrentCulture);
			regexParser._options = op;
			regexParser.SetPattern(re);
			regexParser.CountCaptures();
			regexParser.Reset(op);
			RegexNode regexNode = regexParser.ScanRegex();
			string[] array;
			if (regexParser._capnamelist == null)
			{
				array = null;
			}
			else
			{
				array = regexParser._capnamelist.ToArray();
			}
			return new RegexTree(regexNode, regexParser._caps, regexParser._capnumlist, regexParser._captop, regexParser._capnames, array, op);
		}

		// Token: 0x06003F36 RID: 16182 RVA: 0x00107BD0 File Offset: 0x00105DD0
		internal static RegexReplacement ParseReplacement(string rep, Hashtable caps, int capsize, Hashtable capnames, RegexOptions op)
		{
			RegexParser regexParser = new RegexParser(((op & RegexOptions.CultureInvariant) != RegexOptions.None) ? CultureInfo.InvariantCulture : CultureInfo.CurrentCulture);
			regexParser._options = op;
			regexParser.NoteCaptures(caps, capsize, capnames);
			regexParser.SetPattern(rep);
			RegexNode regexNode = regexParser.ScanReplacement();
			return new RegexReplacement(rep, regexNode, caps);
		}

		// Token: 0x06003F37 RID: 16183 RVA: 0x00107C20 File Offset: 0x00105E20
		internal static string Escape(string input)
		{
			for (int i = 0; i < input.Length; i++)
			{
				if (RegexParser.IsMetachar(input[i]))
				{
					StringBuilder stringBuilder = new StringBuilder();
					char c = input[i];
					stringBuilder.Append(input, 0, i);
					do
					{
						stringBuilder.Append('\\');
						switch (c)
						{
						case '\t':
							c = 't';
							break;
						case '\n':
							c = 'n';
							break;
						case '\f':
							c = 'f';
							break;
						case '\r':
							c = 'r';
							break;
						}
						stringBuilder.Append(c);
						i++;
						int num = i;
						while (i < input.Length)
						{
							c = input[i];
							if (RegexParser.IsMetachar(c))
							{
								break;
							}
							i++;
						}
						stringBuilder.Append(input, num, i - num);
					}
					while (i < input.Length);
					return stringBuilder.ToString();
				}
			}
			return input;
		}

		// Token: 0x06003F38 RID: 16184 RVA: 0x00107CF4 File Offset: 0x00105EF4
		internal static string Unescape(string input)
		{
			for (int i = 0; i < input.Length; i++)
			{
				if (input[i] == '\\')
				{
					StringBuilder stringBuilder = new StringBuilder();
					RegexParser regexParser = new RegexParser(CultureInfo.InvariantCulture);
					regexParser.SetPattern(input);
					stringBuilder.Append(input, 0, i);
					do
					{
						i++;
						regexParser.Textto(i);
						if (i < input.Length)
						{
							stringBuilder.Append(regexParser.ScanCharEscape());
						}
						i = regexParser.Textpos();
						int num = i;
						while (i < input.Length && input[i] != '\\')
						{
							i++;
						}
						stringBuilder.Append(input, num, i - num);
					}
					while (i < input.Length);
					return stringBuilder.ToString();
				}
			}
			return input;
		}

		// Token: 0x06003F39 RID: 16185 RVA: 0x00107DA9 File Offset: 0x00105FA9
		private RegexParser(CultureInfo culture)
		{
			this._culture = culture;
			this._optionsStack = new List<RegexOptions>();
			this._caps = new Hashtable();
		}

		// Token: 0x06003F3A RID: 16186 RVA: 0x00107DCE File Offset: 0x00105FCE
		internal void SetPattern(string Re)
		{
			if (Re == null)
			{
				Re = string.Empty;
			}
			this._pattern = Re;
			this._currentPos = 0;
		}

		// Token: 0x06003F3B RID: 16187 RVA: 0x00107DE8 File Offset: 0x00105FE8
		internal void Reset(RegexOptions topopts)
		{
			this._currentPos = 0;
			this._autocap = 1;
			this._ignoreNextParen = false;
			if (this._optionsStack.Count > 0)
			{
				this._optionsStack.RemoveRange(0, this._optionsStack.Count - 1);
			}
			this._options = topopts;
			this._stack = null;
		}

		// Token: 0x06003F3C RID: 16188 RVA: 0x00107E40 File Offset: 0x00106040
		internal RegexNode ScanRegex()
		{
			bool flag = false;
			this.StartGroup(new RegexNode(28, this._options, 0, -1));
			while (this.CharsRight() > 0)
			{
				bool flag2 = flag;
				flag = false;
				this.ScanBlank();
				int num = this.Textpos();
				char c;
				if (this.UseOptionX())
				{
					while (this.CharsRight() > 0)
					{
						if (RegexParser.IsStopperX(c = this.RightChar()))
						{
							if (c != '{')
							{
								break;
							}
							if (this.IsTrueQuantifier())
							{
								break;
							}
						}
						this.MoveRight();
					}
				}
				else
				{
					while (this.CharsRight() > 0 && (!RegexParser.IsSpecial(c = this.RightChar()) || (c == '{' && !this.IsTrueQuantifier())))
					{
						this.MoveRight();
					}
				}
				int num2 = this.Textpos();
				this.ScanBlank();
				if (this.CharsRight() == 0)
				{
					c = '!';
				}
				else if (RegexParser.IsSpecial(c = this.RightChar()))
				{
					flag = RegexParser.IsQuantifier(c);
					this.MoveRight();
				}
				else
				{
					c = ' ';
				}
				if (num < num2)
				{
					int num3 = num2 - num - (flag ? 1 : 0);
					flag2 = false;
					if (num3 > 0)
					{
						this.AddConcatenate(num, num3, false);
					}
					if (flag)
					{
						this.AddUnitOne(this.CharAt(num2 - 1));
					}
				}
				if (c <= '?')
				{
					switch (c)
					{
					case ' ':
						continue;
					case '!':
						goto IL_437;
					case '"':
					case '#':
					case '%':
					case '&':
					case '\'':
					case ',':
					case '-':
						goto IL_2B7;
					case '$':
						this.AddUnitType(this.UseOptionM() ? 15 : 20);
						break;
					case '(':
					{
						this.PushOptions();
						RegexNode regexNode;
						if ((regexNode = this.ScanGroupOpen()) == null)
						{
							this.PopKeepOptions();
							continue;
						}
						this.PushGroup();
						this.StartGroup(regexNode);
						continue;
					}
					case ')':
						if (this.EmptyStack())
						{
							throw this.MakeException(SR.GetString("TooManyParens"));
						}
						this.AddGroup();
						this.PopGroup();
						this.PopOptions();
						if (this.Unit() == null)
						{
							continue;
						}
						break;
					case '*':
					case '+':
						goto IL_277;
					case '.':
						if (this.UseOptionS())
						{
							this.AddUnitSet("\0\u0001\0\0");
						}
						else
						{
							this.AddUnitNotone('\n');
						}
						break;
					default:
						if (c != '?')
						{
							goto IL_2B7;
						}
						goto IL_277;
					}
				}
				else
				{
					switch (c)
					{
					case '[':
						this.AddUnitSet(this.ScanCharClass(this.UseOptionI()).ToStringClass());
						break;
					case '\\':
						this.AddUnitNode(this.ScanBackslash());
						break;
					case ']':
						goto IL_2B7;
					case '^':
						this.AddUnitType(this.UseOptionM() ? 14 : 18);
						break;
					default:
						if (c == '{')
						{
							goto IL_277;
						}
						if (c != '|')
						{
							goto IL_2B7;
						}
						this.AddAlternate();
						continue;
					}
				}
				IL_2C8:
				this.ScanBlank();
				if (this.CharsRight() == 0 || !(flag = this.IsTrueQuantifier()))
				{
					this.AddConcatenate();
					continue;
				}
				c = this.MoveRightGetChar();
				while (this.Unit() != null)
				{
					int num4;
					int num5;
					if (c <= '+')
					{
						if (c != '*')
						{
							if (c != '+')
							{
								goto IL_3C6;
							}
							num4 = 1;
							num5 = int.MaxValue;
						}
						else
						{
							num4 = 0;
							num5 = int.MaxValue;
						}
					}
					else if (c != '?')
					{
						if (c != '{')
						{
							goto IL_3C6;
						}
						num = this.Textpos();
						num4 = (num5 = this.ScanDecimal());
						if (num < this.Textpos() && this.CharsRight() > 0 && this.RightChar() == ',')
						{
							this.MoveRight();
							if (this.CharsRight() == 0 || this.RightChar() == '}')
							{
								num5 = int.MaxValue;
							}
							else
							{
								num5 = this.ScanDecimal();
							}
						}
						if (num == this.Textpos() || this.CharsRight() == 0 || this.MoveRightGetChar() != '}')
						{
							this.AddConcatenate();
							this.Textto(num - 1);
							break;
						}
					}
					else
					{
						num4 = 0;
						num5 = 1;
					}
					this.ScanBlank();
					bool flag3;
					if (this.CharsRight() == 0 || this.RightChar() != '?')
					{
						flag3 = false;
					}
					else
					{
						this.MoveRight();
						flag3 = true;
					}
					if (num4 > num5)
					{
						throw this.MakeException(SR.GetString("IllegalRange"));
					}
					this.AddConcatenate(flag3, num4, num5);
					continue;
					IL_3C6:
					throw this.MakeException(SR.GetString("InternalError"));
				}
				continue;
				IL_277:
				if (this.Unit() == null)
				{
					throw this.MakeException(flag2 ? SR.GetString("NestedQuantify", new object[] { c.ToString() }) : SR.GetString("QuantifyAfterNothing"));
				}
				this.MoveLeft();
				goto IL_2C8;
				IL_2B7:
				throw this.MakeException(SR.GetString("InternalError"));
			}
			IL_437:
			if (!this.EmptyStack())
			{
				throw this.MakeException(SR.GetString("NotEnoughParens"));
			}
			this.AddGroup();
			return this.Unit();
		}

		// Token: 0x06003F3D RID: 16189 RVA: 0x001082AC File Offset: 0x001064AC
		internal RegexNode ScanReplacement()
		{
			this._concatenation = new RegexNode(25, this._options);
			for (;;)
			{
				int num = this.CharsRight();
				if (num == 0)
				{
					break;
				}
				int num2 = this.Textpos();
				while (num > 0 && this.RightChar() != '$')
				{
					this.MoveRight();
					num--;
				}
				this.AddConcatenate(num2, this.Textpos() - num2, true);
				if (num > 0)
				{
					if (this.MoveRightGetChar() == '$')
					{
						this.AddUnitNode(this.ScanDollar());
					}
					this.AddConcatenate();
				}
			}
			return this._concatenation;
		}

		// Token: 0x06003F3E RID: 16190 RVA: 0x0010832F File Offset: 0x0010652F
		internal RegexCharClass ScanCharClass(bool caseInsensitive)
		{
			return this.ScanCharClass(caseInsensitive, false);
		}

		// Token: 0x06003F3F RID: 16191 RVA: 0x0010833C File Offset: 0x0010653C
		internal RegexCharClass ScanCharClass(bool caseInsensitive, bool scanOnly)
		{
			char c = '\0';
			bool flag = false;
			bool flag2 = true;
			bool flag3 = false;
			RegexCharClass regexCharClass = (scanOnly ? null : new RegexCharClass());
			if (this.CharsRight() > 0 && this.RightChar() == '^')
			{
				this.MoveRight();
				if (!scanOnly)
				{
					regexCharClass.Negate = true;
				}
			}
			while (this.CharsRight() > 0)
			{
				bool flag4 = false;
				char c2 = this.MoveRightGetChar();
				if (c2 == ']')
				{
					if (!flag2)
					{
						flag3 = true;
						break;
					}
					goto IL_28C;
				}
				else
				{
					if (c2 == '\\' && this.CharsRight() > 0)
					{
						char c3;
						c2 = (c3 = this.MoveRightGetChar());
						if (c3 <= 'S')
						{
							if (c3 <= 'D')
							{
								if (c3 != '-')
								{
									if (c3 != 'D')
									{
										goto IL_224;
									}
								}
								else
								{
									if (!scanOnly)
									{
										regexCharClass.AddRange(c2, c2);
										goto IL_3AB;
									}
									goto IL_3AB;
								}
							}
							else
							{
								if (c3 == 'P')
								{
									goto IL_1BC;
								}
								if (c3 != 'S')
								{
									goto IL_224;
								}
								goto IL_13A;
							}
						}
						else
						{
							if (c3 <= 'd')
							{
								if (c3 != 'W')
								{
									if (c3 != 'd')
									{
										goto IL_224;
									}
									goto IL_F3;
								}
							}
							else
							{
								if (c3 == 'p')
								{
									goto IL_1BC;
								}
								if (c3 == 's')
								{
									goto IL_13A;
								}
								if (c3 != 'w')
								{
									goto IL_224;
								}
							}
							if (scanOnly)
							{
								goto IL_3AB;
							}
							if (flag)
							{
								throw this.MakeException(SR.GetString("BadClassInCharRange", new object[] { c2.ToString() }));
							}
							regexCharClass.AddWord(this.UseOptionE(), c2 == 'W');
							goto IL_3AB;
						}
						IL_F3:
						if (scanOnly)
						{
							goto IL_3AB;
						}
						if (flag)
						{
							throw this.MakeException(SR.GetString("BadClassInCharRange", new object[] { c2.ToString() }));
						}
						regexCharClass.AddDigit(this.UseOptionE(), c2 == 'D', this._pattern);
						goto IL_3AB;
						IL_13A:
						if (scanOnly)
						{
							goto IL_3AB;
						}
						if (flag)
						{
							throw this.MakeException(SR.GetString("BadClassInCharRange", new object[] { c2.ToString() }));
						}
						regexCharClass.AddSpace(this.UseOptionE(), c2 == 'S');
						goto IL_3AB;
						IL_1BC:
						if (scanOnly)
						{
							this.ParseProperty();
							goto IL_3AB;
						}
						if (flag)
						{
							throw this.MakeException(SR.GetString("BadClassInCharRange", new object[] { c2.ToString() }));
						}
						regexCharClass.AddCategoryFromName(this.ParseProperty(), c2 != 'p', caseInsensitive, this._pattern);
						goto IL_3AB;
						IL_224:
						this.MoveLeft();
						c2 = this.ScanCharEscape();
						flag4 = true;
						goto IL_28C;
					}
					if (c2 != '[' || this.CharsRight() <= 0 || this.RightChar() != ':' || flag)
					{
						goto IL_28C;
					}
					int num = this.Textpos();
					this.MoveRight();
					string text = this.ScanCapname();
					if (this.CharsRight() < 2 || this.MoveRightGetChar() != ':' || this.MoveRightGetChar() != ']')
					{
						this.Textto(num);
						goto IL_28C;
					}
					goto IL_28C;
				}
				IL_3AB:
				flag2 = false;
				continue;
				IL_28C:
				if (flag)
				{
					flag = false;
					if (scanOnly)
					{
						goto IL_3AB;
					}
					if (c2 == '[' && !flag4 && !flag2)
					{
						regexCharClass.AddChar(c);
						regexCharClass.AddSubtraction(this.ScanCharClass(caseInsensitive, false));
						if (this.CharsRight() > 0 && this.RightChar() != ']')
						{
							throw this.MakeException(SR.GetString("SubtractionMustBeLast"));
						}
						goto IL_3AB;
					}
					else
					{
						if (c > c2)
						{
							throw this.MakeException(SR.GetString("ReversedCharRange"));
						}
						regexCharClass.AddRange(c, c2);
						goto IL_3AB;
					}
				}
				else
				{
					if (this.CharsRight() >= 2 && this.RightChar() == '-' && this.RightChar(1) != ']')
					{
						c = c2;
						flag = true;
						this.MoveRight();
						goto IL_3AB;
					}
					if (this.CharsRight() >= 1 && c2 == '-' && !flag4 && this.RightChar() == '[' && !flag2)
					{
						if (scanOnly)
						{
							this.MoveRight(1);
							this.ScanCharClass(caseInsensitive, true);
							goto IL_3AB;
						}
						this.MoveRight(1);
						regexCharClass.AddSubtraction(this.ScanCharClass(caseInsensitive, false));
						if (this.CharsRight() > 0 && this.RightChar() != ']')
						{
							throw this.MakeException(SR.GetString("SubtractionMustBeLast"));
						}
						goto IL_3AB;
					}
					else
					{
						if (!scanOnly)
						{
							regexCharClass.AddRange(c2, c2);
							goto IL_3AB;
						}
						goto IL_3AB;
					}
				}
			}
			if (!flag3)
			{
				throw this.MakeException(SR.GetString("UnterminatedBracket"));
			}
			if (!scanOnly && caseInsensitive)
			{
				regexCharClass.AddLowercase(this._culture);
			}
			return regexCharClass;
		}

		// Token: 0x06003F40 RID: 16192 RVA: 0x00108730 File Offset: 0x00106930
		internal RegexNode ScanGroupOpen()
		{
			char c = '>';
			if (this.CharsRight() != 0 && this.RightChar() == '?' && (this.RightChar() != '?' || this.CharsRight() <= 1 || this.RightChar(1) != ')'))
			{
				this.MoveRight();
				if (this.CharsRight() != 0)
				{
					char c2 = this.MoveRightGetChar();
					int num;
					char c3;
					if (c2 <= '\'')
					{
						if (c2 == '!')
						{
							this._options &= ~RegexOptions.RightToLeft;
							num = 31;
							goto IL_54D;
						}
						if (c2 != '\'')
						{
							goto IL_523;
						}
						c = '\'';
					}
					else if (c2 != '(')
					{
						switch (c2)
						{
						case ':':
							num = 29;
							goto IL_54D;
						case ';':
							goto IL_523;
						case '<':
							break;
						case '=':
							this._options &= ~RegexOptions.RightToLeft;
							num = 30;
							goto IL_54D;
						case '>':
							num = 32;
							goto IL_54D;
						default:
							goto IL_523;
						}
					}
					else
					{
						int num2 = this.Textpos();
						if (this.CharsRight() > 0)
						{
							c3 = this.RightChar();
							if (c3 >= '0' && c3 <= '9')
							{
								int num3 = this.ScanDecimal();
								if (this.CharsRight() <= 0 || this.MoveRightGetChar() != ')')
								{
									throw this.MakeException(SR.GetString("MalformedReference", new object[] { num3.ToString(CultureInfo.CurrentCulture) }));
								}
								if (this.IsCaptureSlot(num3))
								{
									return new RegexNode(33, this._options, num3);
								}
								throw this.MakeException(SR.GetString("UndefinedReference", new object[] { num3.ToString(CultureInfo.CurrentCulture) }));
							}
							else if (RegexCharClass.IsWordChar(c3))
							{
								string text = this.ScanCapname();
								if (this.IsCaptureName(text) && this.CharsRight() > 0 && this.MoveRightGetChar() == ')')
								{
									return new RegexNode(33, this._options, this.CaptureSlotFromName(text));
								}
							}
						}
						num = 34;
						this.Textto(num2 - 1);
						this._ignoreNextParen = true;
						int num4 = this.CharsRight();
						if (num4 < 3 || this.RightChar(1) != '?')
						{
							goto IL_54D;
						}
						char c4 = this.RightChar(2);
						if (c4 == '#')
						{
							throw this.MakeException(SR.GetString("AlternationCantHaveComment"));
						}
						if (c4 == '\'')
						{
							throw this.MakeException(SR.GetString("AlternationCantCapture"));
						}
						if (num4 >= 4 && c4 == '<' && this.RightChar(3) != '!' && this.RightChar(3) != '=')
						{
							throw this.MakeException(SR.GetString("AlternationCantCapture"));
						}
						goto IL_54D;
					}
					if (this.CharsRight() == 0)
					{
						goto IL_55A;
					}
					char c5;
					c3 = (c5 = this.MoveRightGetChar());
					if (c5 != '!')
					{
						if (c5 == '=')
						{
							if (c != '\'')
							{
								this._options |= RegexOptions.RightToLeft;
								num = 30;
								goto IL_54D;
							}
							goto IL_55A;
						}
						else
						{
							this.MoveLeft();
							int num5 = -1;
							int num6 = -1;
							bool flag = false;
							if (c3 >= '0' && c3 <= '9')
							{
								num5 = this.ScanDecimal();
								if (!this.IsCaptureSlot(num5))
								{
									num5 = -1;
								}
								if (this.CharsRight() > 0 && this.RightChar() != c && this.RightChar() != '-')
								{
									throw this.MakeException(SR.GetString("InvalidGroupName"));
								}
								if (num5 == 0)
								{
									throw this.MakeException(SR.GetString("CapnumNotZero"));
								}
							}
							else if (RegexCharClass.IsWordChar(c3))
							{
								string text2 = this.ScanCapname();
								if (this.IsCaptureName(text2))
								{
									num5 = this.CaptureSlotFromName(text2);
								}
								if (this.CharsRight() > 0 && this.RightChar() != c && this.RightChar() != '-')
								{
									throw this.MakeException(SR.GetString("InvalidGroupName"));
								}
							}
							else
							{
								if (c3 != '-')
								{
									throw this.MakeException(SR.GetString("InvalidGroupName"));
								}
								flag = true;
							}
							if ((num5 != -1 || flag) && this.CharsRight() > 0 && this.RightChar() == '-')
							{
								this.MoveRight();
								c3 = this.RightChar();
								if (c3 >= '0' && c3 <= '9')
								{
									num6 = this.ScanDecimal();
									if (!this.IsCaptureSlot(num6))
									{
										throw this.MakeException(SR.GetString("UndefinedBackref", new object[] { num6 }));
									}
									if (this.CharsRight() > 0 && this.RightChar() != c)
									{
										throw this.MakeException(SR.GetString("InvalidGroupName"));
									}
								}
								else
								{
									if (!RegexCharClass.IsWordChar(c3))
									{
										throw this.MakeException(SR.GetString("InvalidGroupName"));
									}
									string text3 = this.ScanCapname();
									if (!this.IsCaptureName(text3))
									{
										throw this.MakeException(SR.GetString("UndefinedNameRef", new object[] { text3 }));
									}
									num6 = this.CaptureSlotFromName(text3);
									if (this.CharsRight() > 0 && this.RightChar() != c)
									{
										throw this.MakeException(SR.GetString("InvalidGroupName"));
									}
								}
							}
							if ((num5 != -1 || num6 != -1) && this.CharsRight() > 0 && this.MoveRightGetChar() == c)
							{
								return new RegexNode(28, this._options, num5, num6);
							}
							goto IL_55A;
						}
					}
					else
					{
						if (c != '\'')
						{
							this._options |= RegexOptions.RightToLeft;
							num = 31;
							goto IL_54D;
						}
						goto IL_55A;
					}
					IL_523:
					this.MoveLeft();
					num = 29;
					this.ScanOptions();
					if (this.CharsRight() == 0)
					{
						goto IL_55A;
					}
					if ((c3 = this.MoveRightGetChar()) == ')')
					{
						return null;
					}
					if (c3 != ':')
					{
						goto IL_55A;
					}
					IL_54D:
					return new RegexNode(num, this._options);
				}
				IL_55A:
				throw this.MakeException(SR.GetString("UnrecognizedGrouping"));
			}
			if (this.UseOptionN() || this._ignoreNextParen)
			{
				this._ignoreNextParen = false;
				return new RegexNode(29, this._options);
			}
			int num7 = 28;
			RegexOptions options = this._options;
			int autocap = this._autocap;
			this._autocap = autocap + 1;
			return new RegexNode(num7, options, autocap, -1);
		}

		// Token: 0x06003F41 RID: 16193 RVA: 0x00108CA8 File Offset: 0x00106EA8
		internal void ScanBlank()
		{
			if (this.UseOptionX())
			{
				for (;;)
				{
					if (this.CharsRight() <= 0 || !RegexParser.IsSpace(this.RightChar()))
					{
						if (this.CharsRight() == 0)
						{
							return;
						}
						if (this.RightChar() == '#')
						{
							while (this.CharsRight() > 0)
							{
								if (this.RightChar() == '\n')
								{
									break;
								}
								this.MoveRight();
							}
						}
						else
						{
							if (this.CharsRight() < 3 || this.RightChar(2) != '#' || this.RightChar(1) != '?' || this.RightChar() != '(')
							{
								return;
							}
							while (this.CharsRight() > 0 && this.RightChar() != ')')
							{
								this.MoveRight();
							}
							if (this.CharsRight() == 0)
							{
								break;
							}
							this.MoveRight();
						}
					}
					else
					{
						this.MoveRight();
					}
				}
				throw this.MakeException(SR.GetString("UnterminatedComment"));
			}
			while (this.CharsRight() >= 3 && this.RightChar(2) == '#' && this.RightChar(1) == '?' && this.RightChar() == '(')
			{
				while (this.CharsRight() > 0 && this.RightChar() != ')')
				{
					this.MoveRight();
				}
				if (this.CharsRight() == 0)
				{
					throw this.MakeException(SR.GetString("UnterminatedComment"));
				}
				this.MoveRight();
			}
		}

		// Token: 0x06003F42 RID: 16194 RVA: 0x00108DE8 File Offset: 0x00106FE8
		internal RegexNode ScanBackslash()
		{
			if (this.CharsRight() == 0)
			{
				throw this.MakeException(SR.GetString("IllegalEndEscape"));
			}
			char c2;
			char c = (c2 = this.RightChar());
			if (c2 <= 'Z')
			{
				if (c2 <= 'P')
				{
					switch (c2)
					{
					case 'A':
					case 'B':
					case 'G':
						break;
					case 'C':
					case 'E':
					case 'F':
						goto IL_251;
					case 'D':
						this.MoveRight();
						if (this.UseOptionE())
						{
							return new RegexNode(11, this._options, "\u0001\u0002\00:");
						}
						return new RegexNode(11, this._options, RegexCharClass.NotDigitClass);
					default:
						if (c2 != 'P')
						{
							goto IL_251;
						}
						goto IL_1FD;
					}
				}
				else if (c2 != 'S')
				{
					if (c2 != 'W')
					{
						if (c2 != 'Z')
						{
							goto IL_251;
						}
					}
					else
					{
						this.MoveRight();
						if (this.UseOptionE())
						{
							return new RegexNode(11, this._options, "\u0001\n\00:A[_`a{İı");
						}
						return new RegexNode(11, this._options, RegexCharClass.NotWordClass);
					}
				}
				else
				{
					this.MoveRight();
					if (this.UseOptionE())
					{
						return new RegexNode(11, this._options, "\u0001\u0004\0\t\u000e !");
					}
					return new RegexNode(11, this._options, RegexCharClass.NotSpaceClass);
				}
			}
			else if (c2 <= 'p')
			{
				if (c2 != 'b')
				{
					if (c2 != 'd')
					{
						if (c2 != 'p')
						{
							goto IL_251;
						}
						goto IL_1FD;
					}
					else
					{
						this.MoveRight();
						if (this.UseOptionE())
						{
							return new RegexNode(11, this._options, "\0\u0002\00:");
						}
						return new RegexNode(11, this._options, RegexCharClass.DigitClass);
					}
				}
			}
			else if (c2 != 's')
			{
				if (c2 != 'w')
				{
					if (c2 != 'z')
					{
						goto IL_251;
					}
				}
				else
				{
					this.MoveRight();
					if (this.UseOptionE())
					{
						return new RegexNode(11, this._options, "\0\n\00:A[_`a{İı");
					}
					return new RegexNode(11, this._options, RegexCharClass.WordClass);
				}
			}
			else
			{
				this.MoveRight();
				if (this.UseOptionE())
				{
					return new RegexNode(11, this._options, "\0\u0004\0\t\u000e !");
				}
				return new RegexNode(11, this._options, RegexCharClass.SpaceClass);
			}
			this.MoveRight();
			return new RegexNode(this.TypeFromCode(c), this._options);
			IL_1FD:
			this.MoveRight();
			RegexCharClass regexCharClass = new RegexCharClass();
			regexCharClass.AddCategoryFromName(this.ParseProperty(), c != 'p', this.UseOptionI(), this._pattern);
			if (this.UseOptionI())
			{
				regexCharClass.AddLowercase(this._culture);
			}
			return new RegexNode(11, this._options, regexCharClass.ToStringClass());
			IL_251:
			return this.ScanBasicBackslash();
		}

		// Token: 0x06003F43 RID: 16195 RVA: 0x0010904C File Offset: 0x0010724C
		internal RegexNode ScanBasicBackslash()
		{
			if (this.CharsRight() == 0)
			{
				throw this.MakeException(SR.GetString("IllegalEndEscape"));
			}
			bool flag = false;
			char c = '\0';
			int num = this.Textpos();
			char c2 = this.RightChar();
			if (c2 == 'k')
			{
				if (this.CharsRight() >= 2)
				{
					this.MoveRight();
					c2 = this.MoveRightGetChar();
					if (c2 == '<' || c2 == '\'')
					{
						flag = true;
						c = ((c2 == '\'') ? '\'' : '>');
					}
				}
				if (!flag || this.CharsRight() <= 0)
				{
					throw this.MakeException(SR.GetString("MalformedNameRef"));
				}
				c2 = this.RightChar();
			}
			else if ((c2 == '<' || c2 == '\'') && this.CharsRight() > 1)
			{
				flag = true;
				c = ((c2 == '\'') ? '\'' : '>');
				this.MoveRight();
				c2 = this.RightChar();
			}
			if (flag && c2 >= '0' && c2 <= '9')
			{
				int num2 = this.ScanDecimal();
				if (this.CharsRight() > 0 && this.MoveRightGetChar() == c)
				{
					if (this.IsCaptureSlot(num2))
					{
						return new RegexNode(13, this._options, num2);
					}
					throw this.MakeException(SR.GetString("UndefinedBackref", new object[] { num2.ToString(CultureInfo.CurrentCulture) }));
				}
			}
			else if (!flag && c2 >= '1' && c2 <= '9')
			{
				if (this.UseOptionE())
				{
					int num3 = -1;
					int i = (int)(c2 - '0');
					int num4 = this.Textpos() - 1;
					while (i <= this._captop)
					{
						if (this.IsCaptureSlot(i) && (this._caps == null || (int)this._caps[i] < num4))
						{
							num3 = i;
						}
						this.MoveRight();
						if (this.CharsRight() == 0 || (c2 = this.RightChar()) < '0' || c2 > '9')
						{
							break;
						}
						i = i * 10 + (int)(c2 - '0');
					}
					if (num3 >= 0)
					{
						return new RegexNode(13, this._options, num3);
					}
				}
				else
				{
					int num5 = this.ScanDecimal();
					if (this.IsCaptureSlot(num5))
					{
						return new RegexNode(13, this._options, num5);
					}
					if (num5 <= 9)
					{
						throw this.MakeException(SR.GetString("UndefinedBackref", new object[] { num5.ToString(CultureInfo.CurrentCulture) }));
					}
				}
			}
			else if (flag && RegexCharClass.IsWordChar(c2))
			{
				string text = this.ScanCapname();
				if (this.CharsRight() > 0 && this.MoveRightGetChar() == c)
				{
					if (this.IsCaptureName(text))
					{
						return new RegexNode(13, this._options, this.CaptureSlotFromName(text));
					}
					throw this.MakeException(SR.GetString("UndefinedNameRef", new object[] { text }));
				}
			}
			this.Textto(num);
			c2 = this.ScanCharEscape();
			if (this.UseOptionI())
			{
				c2 = char.ToLower(c2, this._culture);
			}
			return new RegexNode(9, this._options, c2);
		}

		// Token: 0x06003F44 RID: 16196 RVA: 0x00109310 File Offset: 0x00107510
		internal RegexNode ScanDollar()
		{
			if (this.CharsRight() == 0)
			{
				return new RegexNode(9, this._options, '$');
			}
			char c = this.RightChar();
			int num = this.Textpos();
			int num2 = num;
			bool flag;
			if (c == '{' && this.CharsRight() > 1)
			{
				flag = true;
				this.MoveRight();
				c = this.RightChar();
			}
			else
			{
				flag = false;
			}
			if (c >= '0' && c <= '9')
			{
				if (!flag && this.UseOptionE())
				{
					int num3 = -1;
					int num4 = (int)(c - '0');
					this.MoveRight();
					if (this.IsCaptureSlot(num4))
					{
						num3 = num4;
						num2 = this.Textpos();
					}
					while (this.CharsRight() > 0 && (c = this.RightChar()) >= '0' && c <= '9')
					{
						int num5 = (int)(c - '0');
						if (num4 > 214748364 || (num4 == 214748364 && num5 > 7))
						{
							throw this.MakeException(SR.GetString("CaptureGroupOutOfRange"));
						}
						num4 = num4 * 10 + num5;
						this.MoveRight();
						if (this.IsCaptureSlot(num4))
						{
							num3 = num4;
							num2 = this.Textpos();
						}
					}
					this.Textto(num2);
					if (num3 >= 0)
					{
						return new RegexNode(13, this._options, num3);
					}
				}
				else
				{
					int num6 = this.ScanDecimal();
					if ((!flag || (this.CharsRight() > 0 && this.MoveRightGetChar() == '}')) && this.IsCaptureSlot(num6))
					{
						return new RegexNode(13, this._options, num6);
					}
				}
			}
			else if (flag && RegexCharClass.IsWordChar(c))
			{
				string text = this.ScanCapname();
				if (this.CharsRight() > 0 && this.MoveRightGetChar() == '}' && this.IsCaptureName(text))
				{
					return new RegexNode(13, this._options, this.CaptureSlotFromName(text));
				}
			}
			else if (!flag)
			{
				int num7 = 1;
				if (c <= '+')
				{
					switch (c)
					{
					case '$':
						this.MoveRight();
						return new RegexNode(9, this._options, '$');
					case '%':
						break;
					case '&':
						num7 = 0;
						break;
					case '\'':
						num7 = -2;
						break;
					default:
						if (c == '+')
						{
							num7 = -3;
						}
						break;
					}
				}
				else if (c != '_')
				{
					if (c == '`')
					{
						num7 = -1;
					}
				}
				else
				{
					num7 = -4;
				}
				if (num7 != 1)
				{
					this.MoveRight();
					return new RegexNode(13, this._options, num7);
				}
			}
			this.Textto(num);
			return new RegexNode(9, this._options, '$');
		}

		// Token: 0x06003F45 RID: 16197 RVA: 0x00109564 File Offset: 0x00107764
		internal string ScanCapname()
		{
			int num = this.Textpos();
			while (this.CharsRight() > 0)
			{
				if (!RegexCharClass.IsWordChar(this.MoveRightGetChar()))
				{
					this.MoveLeft();
					break;
				}
			}
			return this._pattern.Substring(num, this.Textpos() - num);
		}

		// Token: 0x06003F46 RID: 16198 RVA: 0x001095AC File Offset: 0x001077AC
		internal char ScanOctal()
		{
			int num = 3;
			if (num > this.CharsRight())
			{
				num = this.CharsRight();
			}
			int num2 = 0;
			int num3;
			while (num > 0 && (num3 = (int)(this.RightChar() - '0')) <= 7)
			{
				this.MoveRight();
				num2 *= 8;
				num2 += num3;
				if (this.UseOptionE() && num2 >= 32)
				{
					break;
				}
				num--;
			}
			num2 &= 255;
			return (char)num2;
		}

		// Token: 0x06003F47 RID: 16199 RVA: 0x0010960C File Offset: 0x0010780C
		internal int ScanDecimal()
		{
			int num = 0;
			int num2;
			while (this.CharsRight() > 0 && (num2 = (int)((ushort)(this.RightChar() - '0'))) <= 9)
			{
				this.MoveRight();
				if (num > 214748364 || (num == 214748364 && num2 > 7))
				{
					throw this.MakeException(SR.GetString("CaptureGroupOutOfRange"));
				}
				num *= 10;
				num += num2;
			}
			return num;
		}

		// Token: 0x06003F48 RID: 16200 RVA: 0x0010966C File Offset: 0x0010786C
		internal char ScanHex(int c)
		{
			int num = 0;
			if (this.CharsRight() >= c)
			{
				int num2;
				while (c > 0 && (num2 = RegexParser.HexDigit(this.MoveRightGetChar())) >= 0)
				{
					num *= 16;
					num += num2;
					c--;
				}
			}
			if (c > 0)
			{
				throw this.MakeException(SR.GetString("TooFewHex"));
			}
			return (char)num;
		}

		// Token: 0x06003F49 RID: 16201 RVA: 0x001096C0 File Offset: 0x001078C0
		internal static int HexDigit(char ch)
		{
			int num;
			if ((num = (int)(ch - '0')) <= 9)
			{
				return num;
			}
			if ((num = (int)(ch - 'a')) <= 5)
			{
				return num + 10;
			}
			if ((num = (int)(ch - 'A')) <= 5)
			{
				return num + 10;
			}
			return -1;
		}

		// Token: 0x06003F4A RID: 16202 RVA: 0x001096F8 File Offset: 0x001078F8
		internal char ScanControl()
		{
			if (this.CharsRight() <= 0)
			{
				throw this.MakeException(SR.GetString("MissingControl"));
			}
			char c = this.MoveRightGetChar();
			if (c >= 'a' && c <= 'z')
			{
				c -= ' ';
			}
			if ((c -= '@') < ' ')
			{
				return c;
			}
			throw this.MakeException(SR.GetString("UnrecognizedControl"));
		}

		// Token: 0x06003F4B RID: 16203 RVA: 0x00109753 File Offset: 0x00107953
		internal bool IsOnlyTopOption(RegexOptions option)
		{
			return option == RegexOptions.RightToLeft || option == RegexOptions.Compiled || option == RegexOptions.CultureInvariant || option == RegexOptions.ECMAScript;
		}

		// Token: 0x06003F4C RID: 16204 RVA: 0x00109770 File Offset: 0x00107970
		internal void ScanOptions()
		{
			bool flag = false;
			while (this.CharsRight() > 0)
			{
				char c = this.RightChar();
				if (c == '-')
				{
					flag = true;
				}
				else if (c == '+')
				{
					flag = false;
				}
				else
				{
					RegexOptions regexOptions = RegexParser.OptionFromCode(c);
					if (regexOptions == RegexOptions.None || this.IsOnlyTopOption(regexOptions))
					{
						return;
					}
					if (flag)
					{
						this._options &= ~regexOptions;
					}
					else
					{
						this._options |= regexOptions;
					}
				}
				this.MoveRight();
			}
		}

		// Token: 0x06003F4D RID: 16205 RVA: 0x001097E0 File Offset: 0x001079E0
		internal char ScanCharEscape()
		{
			char c = this.MoveRightGetChar();
			if (c >= '0' && c <= '7')
			{
				this.MoveLeft();
				return this.ScanOctal();
			}
			switch (c)
			{
			case 'a':
				return '\a';
			case 'b':
				return '\b';
			case 'c':
				return this.ScanControl();
			case 'd':
				break;
			case 'e':
				return '\u001b';
			case 'f':
				return '\f';
			default:
				switch (c)
				{
				case 'n':
					return '\n';
				case 'r':
					return '\r';
				case 't':
					return '\t';
				case 'u':
					return this.ScanHex(4);
				case 'v':
					return '\v';
				case 'x':
					return this.ScanHex(2);
				}
				break;
			}
			if (!this.UseOptionE() && RegexCharClass.IsWordChar(c))
			{
				throw this.MakeException(SR.GetString("UnrecognizedEscape", new object[] { c.ToString() }));
			}
			return c;
		}

		// Token: 0x06003F4E RID: 16206 RVA: 0x001098C4 File Offset: 0x00107AC4
		internal string ParseProperty()
		{
			if (this.CharsRight() < 3)
			{
				throw this.MakeException(SR.GetString("IncompleteSlashP"));
			}
			char c = this.MoveRightGetChar();
			if (c != '{')
			{
				throw this.MakeException(SR.GetString("MalformedSlashP"));
			}
			int num = this.Textpos();
			while (this.CharsRight() > 0)
			{
				c = this.MoveRightGetChar();
				if (!RegexCharClass.IsWordChar(c) && c != '-')
				{
					this.MoveLeft();
					break;
				}
			}
			string text = this._pattern.Substring(num, this.Textpos() - num);
			if (this.CharsRight() == 0 || this.MoveRightGetChar() != '}')
			{
				throw this.MakeException(SR.GetString("IncompleteSlashP"));
			}
			return text;
		}

		// Token: 0x06003F4F RID: 16207 RVA: 0x00109970 File Offset: 0x00107B70
		internal int TypeFromCode(char ch)
		{
			if (ch <= 'G')
			{
				if (ch == 'A')
				{
					return 18;
				}
				if (ch != 'B')
				{
					if (ch == 'G')
					{
						return 19;
					}
				}
				else
				{
					if (!this.UseOptionE())
					{
						return 17;
					}
					return 42;
				}
			}
			else
			{
				if (ch == 'Z')
				{
					return 20;
				}
				if (ch != 'b')
				{
					if (ch == 'z')
					{
						return 21;
					}
				}
				else
				{
					if (!this.UseOptionE())
					{
						return 16;
					}
					return 41;
				}
			}
			return 22;
		}

		// Token: 0x06003F50 RID: 16208 RVA: 0x001099D0 File Offset: 0x00107BD0
		internal static RegexOptions OptionFromCode(char ch)
		{
			if (ch >= 'A' && ch <= 'Z')
			{
				ch += ' ';
			}
			if (ch <= 'e')
			{
				if (ch == 'c')
				{
					return RegexOptions.Compiled;
				}
				if (ch == 'e')
				{
					return RegexOptions.ECMAScript;
				}
			}
			else
			{
				if (ch == 'i')
				{
					return RegexOptions.IgnoreCase;
				}
				switch (ch)
				{
				case 'm':
					return RegexOptions.Multiline;
				case 'n':
					return RegexOptions.ExplicitCapture;
				case 'o':
				case 'p':
				case 'q':
					break;
				case 'r':
					return RegexOptions.RightToLeft;
				case 's':
					return RegexOptions.Singleline;
				default:
					if (ch == 'x')
					{
						return RegexOptions.IgnorePatternWhitespace;
					}
					break;
				}
			}
			return RegexOptions.None;
		}

		// Token: 0x06003F51 RID: 16209 RVA: 0x00109A48 File Offset: 0x00107C48
		internal void CountCaptures()
		{
			this.NoteCaptureSlot(0, 0);
			this._autocap = 1;
			while (this.CharsRight() > 0)
			{
				int num = this.Textpos();
				char c = this.MoveRightGetChar();
				if (c <= '(')
				{
					if (c != '#')
					{
						if (c == '(')
						{
							if (this.CharsRight() >= 2 && this.RightChar(1) == '#' && this.RightChar() == '?')
							{
								this.MoveLeft();
								this.ScanBlank();
							}
							else
							{
								this.PushOptions();
								if (this.CharsRight() > 0 && this.RightChar() == '?')
								{
									this.MoveRight();
									if (this.CharsRight() > 1 && (this.RightChar() == '<' || this.RightChar() == '\''))
									{
										this.MoveRight();
										c = this.RightChar();
										if (c != '0' && RegexCharClass.IsWordChar(c))
										{
											if (c >= '1' && c <= '9')
											{
												this.NoteCaptureSlot(this.ScanDecimal(), num);
											}
											else
											{
												this.NoteCaptureName(this.ScanCapname(), num);
											}
										}
									}
									else
									{
										this.ScanOptions();
										if (this.CharsRight() > 0)
										{
											if (this.RightChar() == ')')
											{
												this.MoveRight();
												this.PopKeepOptions();
											}
											else if (this.RightChar() == '(')
											{
												this._ignoreNextParen = true;
												continue;
											}
										}
									}
								}
								else if (!this.UseOptionN() && !this._ignoreNextParen)
								{
									int autocap = this._autocap;
									this._autocap = autocap + 1;
									this.NoteCaptureSlot(autocap, num);
								}
							}
							this._ignoreNextParen = false;
						}
					}
					else if (this.UseOptionX())
					{
						this.MoveLeft();
						this.ScanBlank();
					}
				}
				else if (c != ')')
				{
					if (c != '[')
					{
						if (c == '\\' && this.CharsRight() > 0)
						{
							this.MoveRight();
						}
					}
					else
					{
						this.ScanCharClass(false, true);
					}
				}
				else if (!this.EmptyOptionsStack())
				{
					this.PopOptions();
				}
			}
			this.AssignNameSlots();
		}

		// Token: 0x06003F52 RID: 16210 RVA: 0x00109C28 File Offset: 0x00107E28
		internal void NoteCaptureSlot(int i, int pos)
		{
			if (!this._caps.ContainsKey(i))
			{
				this._caps.Add(i, pos);
				this._capcount++;
				if (this._captop <= i)
				{
					if (i == 2147483647)
					{
						this._captop = i;
						return;
					}
					this._captop = i + 1;
				}
			}
		}

		// Token: 0x06003F53 RID: 16211 RVA: 0x00109C90 File Offset: 0x00107E90
		internal void NoteCaptureName(string name, int pos)
		{
			if (this._capnames == null)
			{
				this._capnames = new Hashtable();
				this._capnamelist = new List<string>();
			}
			if (!this._capnames.ContainsKey(name))
			{
				this._capnames.Add(name, pos);
				this._capnamelist.Add(name);
			}
		}

		// Token: 0x06003F54 RID: 16212 RVA: 0x00109CE7 File Offset: 0x00107EE7
		internal void NoteCaptures(Hashtable caps, int capsize, Hashtable capnames)
		{
			this._caps = caps;
			this._capsize = capsize;
			this._capnames = capnames;
		}

		// Token: 0x06003F55 RID: 16213 RVA: 0x00109D00 File Offset: 0x00107F00
		internal void AssignNameSlots()
		{
			if (this._capnames != null)
			{
				for (int i = 0; i < this._capnamelist.Count; i++)
				{
					while (this.IsCaptureSlot(this._autocap))
					{
						this._autocap++;
					}
					string text = this._capnamelist[i];
					int num = (int)this._capnames[text];
					this._capnames[text] = this._autocap;
					this.NoteCaptureSlot(this._autocap, num);
					this._autocap++;
				}
			}
			if (this._capcount < this._captop)
			{
				this._capnumlist = new int[this._capcount];
				int num2 = 0;
				IDictionaryEnumerator enumerator = this._caps.GetEnumerator();
				while (enumerator.MoveNext())
				{
					this._capnumlist[num2++] = (int)enumerator.Key;
				}
				Array.Sort<int>(this._capnumlist, Comparer<int>.Default);
			}
			if (this._capnames != null || this._capnumlist != null)
			{
				int num3 = 0;
				List<string> list;
				int num4;
				if (this._capnames == null)
				{
					list = null;
					this._capnames = new Hashtable();
					this._capnamelist = new List<string>();
					num4 = -1;
				}
				else
				{
					list = this._capnamelist;
					this._capnamelist = new List<string>();
					num4 = (int)this._capnames[list[0]];
				}
				for (int j = 0; j < this._capcount; j++)
				{
					int num5 = ((this._capnumlist == null) ? j : this._capnumlist[j]);
					if (num4 == num5)
					{
						this._capnamelist.Add(list[num3++]);
						num4 = ((num3 == list.Count) ? (-1) : ((int)this._capnames[list[num3]]));
					}
					else
					{
						string text2 = Convert.ToString(num5, this._culture);
						this._capnamelist.Add(text2);
						this._capnames[text2] = num5;
					}
				}
			}
		}

		// Token: 0x06003F56 RID: 16214 RVA: 0x00109F11 File Offset: 0x00108111
		internal int CaptureSlotFromName(string capname)
		{
			return (int)this._capnames[capname];
		}

		// Token: 0x06003F57 RID: 16215 RVA: 0x00109F24 File Offset: 0x00108124
		internal bool IsCaptureSlot(int i)
		{
			if (this._caps != null)
			{
				return this._caps.ContainsKey(i);
			}
			return i >= 0 && i < this._capsize;
		}

		// Token: 0x06003F58 RID: 16216 RVA: 0x00109F4F File Offset: 0x0010814F
		internal bool IsCaptureName(string capname)
		{
			return this._capnames != null && this._capnames.ContainsKey(capname);
		}

		// Token: 0x06003F59 RID: 16217 RVA: 0x00109F67 File Offset: 0x00108167
		internal bool UseOptionN()
		{
			return (this._options & RegexOptions.ExplicitCapture) > RegexOptions.None;
		}

		// Token: 0x06003F5A RID: 16218 RVA: 0x00109F74 File Offset: 0x00108174
		internal bool UseOptionI()
		{
			return (this._options & RegexOptions.IgnoreCase) > RegexOptions.None;
		}

		// Token: 0x06003F5B RID: 16219 RVA: 0x00109F81 File Offset: 0x00108181
		internal bool UseOptionM()
		{
			return (this._options & RegexOptions.Multiline) > RegexOptions.None;
		}

		// Token: 0x06003F5C RID: 16220 RVA: 0x00109F8E File Offset: 0x0010818E
		internal bool UseOptionS()
		{
			return (this._options & RegexOptions.Singleline) > RegexOptions.None;
		}

		// Token: 0x06003F5D RID: 16221 RVA: 0x00109F9C File Offset: 0x0010819C
		internal bool UseOptionX()
		{
			return (this._options & RegexOptions.IgnorePatternWhitespace) > RegexOptions.None;
		}

		// Token: 0x06003F5E RID: 16222 RVA: 0x00109FAA File Offset: 0x001081AA
		internal bool UseOptionE()
		{
			return (this._options & RegexOptions.ECMAScript) > RegexOptions.None;
		}

		// Token: 0x06003F5F RID: 16223 RVA: 0x00109FBB File Offset: 0x001081BB
		internal static bool IsSpecial(char ch)
		{
			return ch <= '|' && RegexParser._category[(int)ch] >= 4;
		}

		// Token: 0x06003F60 RID: 16224 RVA: 0x00109FD1 File Offset: 0x001081D1
		internal static bool IsStopperX(char ch)
		{
			return ch <= '|' && RegexParser._category[(int)ch] >= 2;
		}

		// Token: 0x06003F61 RID: 16225 RVA: 0x00109FE7 File Offset: 0x001081E7
		internal static bool IsQuantifier(char ch)
		{
			return ch <= '{' && RegexParser._category[(int)ch] >= 5;
		}

		// Token: 0x06003F62 RID: 16226 RVA: 0x0010A000 File Offset: 0x00108200
		internal bool IsTrueQuantifier()
		{
			int num = this.CharsRight();
			if (num == 0)
			{
				return false;
			}
			int num2 = this.Textpos();
			char c = this.CharAt(num2);
			if (c != '{')
			{
				return c <= '{' && RegexParser._category[(int)c] >= 5;
			}
			int num3 = num2;
			while (--num > 0 && (c = this.CharAt(++num3)) >= '0' && c <= '9')
			{
			}
			if (num == 0 || num3 - num2 == 1)
			{
				return false;
			}
			if (c == '}')
			{
				return true;
			}
			if (c != ',')
			{
				return false;
			}
			while (--num > 0 && (c = this.CharAt(++num3)) >= '0' && c <= '9')
			{
			}
			return num > 0 && c == '}';
		}

		// Token: 0x06003F63 RID: 16227 RVA: 0x0010A0A4 File Offset: 0x001082A4
		internal static bool IsSpace(char ch)
		{
			return ch <= ' ' && RegexParser._category[(int)ch] == 2;
		}

		// Token: 0x06003F64 RID: 16228 RVA: 0x0010A0B7 File Offset: 0x001082B7
		internal static bool IsMetachar(char ch)
		{
			return ch <= '|' && RegexParser._category[(int)ch] >= 1;
		}

		// Token: 0x06003F65 RID: 16229 RVA: 0x0010A0D0 File Offset: 0x001082D0
		internal void AddConcatenate(int pos, int cch, bool isReplacement)
		{
			if (cch == 0)
			{
				return;
			}
			RegexNode regexNode;
			if (cch > 1)
			{
				string text = this._pattern.Substring(pos, cch);
				if (this.UseOptionI() && !isReplacement)
				{
					StringBuilder stringBuilder = new StringBuilder(text.Length);
					for (int i = 0; i < text.Length; i++)
					{
						stringBuilder.Append(char.ToLower(text[i], this._culture));
					}
					text = stringBuilder.ToString();
				}
				regexNode = new RegexNode(12, this._options, text);
			}
			else
			{
				char c = this._pattern[pos];
				if (this.UseOptionI() && !isReplacement)
				{
					c = char.ToLower(c, this._culture);
				}
				regexNode = new RegexNode(9, this._options, c);
			}
			this._concatenation.AddChild(regexNode);
		}

		// Token: 0x06003F66 RID: 16230 RVA: 0x0010A190 File Offset: 0x00108390
		internal void PushGroup()
		{
			this._group._next = this._stack;
			this._alternation._next = this._group;
			this._concatenation._next = this._alternation;
			this._stack = this._concatenation;
		}

		// Token: 0x06003F67 RID: 16231 RVA: 0x0010A1DC File Offset: 0x001083DC
		internal void PopGroup()
		{
			this._concatenation = this._stack;
			this._alternation = this._concatenation._next;
			this._group = this._alternation._next;
			this._stack = this._group._next;
			if (this._group.Type() == 34 && this._group.ChildCount() == 0)
			{
				if (this._unit == null)
				{
					throw this.MakeException(SR.GetString("IllegalCondition"));
				}
				this._group.AddChild(this._unit);
				this._unit = null;
			}
		}

		// Token: 0x06003F68 RID: 16232 RVA: 0x0010A275 File Offset: 0x00108475
		internal bool EmptyStack()
		{
			return this._stack == null;
		}

		// Token: 0x06003F69 RID: 16233 RVA: 0x0010A280 File Offset: 0x00108480
		internal void StartGroup(RegexNode openGroup)
		{
			this._group = openGroup;
			this._alternation = new RegexNode(24, this._options);
			this._concatenation = new RegexNode(25, this._options);
		}

		// Token: 0x06003F6A RID: 16234 RVA: 0x0010A2B0 File Offset: 0x001084B0
		internal void AddAlternate()
		{
			if (this._group.Type() == 34 || this._group.Type() == 33)
			{
				this._group.AddChild(this._concatenation.ReverseLeft());
			}
			else
			{
				this._alternation.AddChild(this._concatenation.ReverseLeft());
			}
			this._concatenation = new RegexNode(25, this._options);
		}

		// Token: 0x06003F6B RID: 16235 RVA: 0x0010A31C File Offset: 0x0010851C
		internal void AddConcatenate()
		{
			this._concatenation.AddChild(this._unit);
			this._unit = null;
		}

		// Token: 0x06003F6C RID: 16236 RVA: 0x0010A336 File Offset: 0x00108536
		internal void AddConcatenate(bool lazy, int min, int max)
		{
			this._concatenation.AddChild(this._unit.MakeQuantifier(lazy, min, max));
			this._unit = null;
		}

		// Token: 0x06003F6D RID: 16237 RVA: 0x0010A358 File Offset: 0x00108558
		internal RegexNode Unit()
		{
			return this._unit;
		}

		// Token: 0x06003F6E RID: 16238 RVA: 0x0010A360 File Offset: 0x00108560
		internal void AddUnitOne(char ch)
		{
			if (this.UseOptionI())
			{
				ch = char.ToLower(ch, this._culture);
			}
			this._unit = new RegexNode(9, this._options, ch);
		}

		// Token: 0x06003F6F RID: 16239 RVA: 0x0010A38C File Offset: 0x0010858C
		internal void AddUnitNotone(char ch)
		{
			if (this.UseOptionI())
			{
				ch = char.ToLower(ch, this._culture);
			}
			this._unit = new RegexNode(10, this._options, ch);
		}

		// Token: 0x06003F70 RID: 16240 RVA: 0x0010A3B8 File Offset: 0x001085B8
		internal void AddUnitSet(string cc)
		{
			this._unit = new RegexNode(11, this._options, cc);
		}

		// Token: 0x06003F71 RID: 16241 RVA: 0x0010A3CE File Offset: 0x001085CE
		internal void AddUnitNode(RegexNode node)
		{
			this._unit = node;
		}

		// Token: 0x06003F72 RID: 16242 RVA: 0x0010A3D7 File Offset: 0x001085D7
		internal void AddUnitType(int type)
		{
			this._unit = new RegexNode(type, this._options);
		}

		// Token: 0x06003F73 RID: 16243 RVA: 0x0010A3EC File Offset: 0x001085EC
		internal void AddGroup()
		{
			if (this._group.Type() == 34 || this._group.Type() == 33)
			{
				this._group.AddChild(this._concatenation.ReverseLeft());
				if ((this._group.Type() == 33 && this._group.ChildCount() > 2) || this._group.ChildCount() > 3)
				{
					throw this.MakeException(SR.GetString("TooManyAlternates"));
				}
			}
			else
			{
				this._alternation.AddChild(this._concatenation.ReverseLeft());
				this._group.AddChild(this._alternation);
			}
			this._unit = this._group;
		}

		// Token: 0x06003F74 RID: 16244 RVA: 0x0010A49C File Offset: 0x0010869C
		internal void PushOptions()
		{
			this._optionsStack.Add(this._options);
		}

		// Token: 0x06003F75 RID: 16245 RVA: 0x0010A4AF File Offset: 0x001086AF
		internal void PopOptions()
		{
			this._options = this._optionsStack[this._optionsStack.Count - 1];
			this._optionsStack.RemoveAt(this._optionsStack.Count - 1);
		}

		// Token: 0x06003F76 RID: 16246 RVA: 0x0010A4E7 File Offset: 0x001086E7
		internal bool EmptyOptionsStack()
		{
			return this._optionsStack.Count == 0;
		}

		// Token: 0x06003F77 RID: 16247 RVA: 0x0010A4F7 File Offset: 0x001086F7
		internal void PopKeepOptions()
		{
			this._optionsStack.RemoveAt(this._optionsStack.Count - 1);
		}

		// Token: 0x06003F78 RID: 16248 RVA: 0x0010A511 File Offset: 0x00108711
		internal ArgumentException MakeException(string message)
		{
			return new ArgumentException(SR.GetString("MakeException", new object[] { this._pattern, message }));
		}

		// Token: 0x06003F79 RID: 16249 RVA: 0x0010A535 File Offset: 0x00108735
		internal int Textpos()
		{
			return this._currentPos;
		}

		// Token: 0x06003F7A RID: 16250 RVA: 0x0010A53D File Offset: 0x0010873D
		internal void Textto(int pos)
		{
			this._currentPos = pos;
		}

		// Token: 0x06003F7B RID: 16251 RVA: 0x0010A548 File Offset: 0x00108748
		internal char MoveRightGetChar()
		{
			string pattern = this._pattern;
			int currentPos = this._currentPos;
			this._currentPos = currentPos + 1;
			return pattern[currentPos];
		}

		// Token: 0x06003F7C RID: 16252 RVA: 0x0010A571 File Offset: 0x00108771
		internal void MoveRight()
		{
			this.MoveRight(1);
		}

		// Token: 0x06003F7D RID: 16253 RVA: 0x0010A57A File Offset: 0x0010877A
		internal void MoveRight(int i)
		{
			this._currentPos += i;
		}

		// Token: 0x06003F7E RID: 16254 RVA: 0x0010A58A File Offset: 0x0010878A
		internal void MoveLeft()
		{
			this._currentPos--;
		}

		// Token: 0x06003F7F RID: 16255 RVA: 0x0010A59A File Offset: 0x0010879A
		internal char CharAt(int i)
		{
			return this._pattern[i];
		}

		// Token: 0x06003F80 RID: 16256 RVA: 0x0010A5A8 File Offset: 0x001087A8
		internal char RightChar()
		{
			return this._pattern[this._currentPos];
		}

		// Token: 0x06003F81 RID: 16257 RVA: 0x0010A5BB File Offset: 0x001087BB
		internal char RightChar(int i)
		{
			return this._pattern[this._currentPos + i];
		}

		// Token: 0x06003F82 RID: 16258 RVA: 0x0010A5D0 File Offset: 0x001087D0
		internal int CharsRight()
		{
			return this._pattern.Length - this._currentPos;
		}

		// Token: 0x04002E27 RID: 11815
		internal RegexNode _stack;

		// Token: 0x04002E28 RID: 11816
		internal RegexNode _group;

		// Token: 0x04002E29 RID: 11817
		internal RegexNode _alternation;

		// Token: 0x04002E2A RID: 11818
		internal RegexNode _concatenation;

		// Token: 0x04002E2B RID: 11819
		internal RegexNode _unit;

		// Token: 0x04002E2C RID: 11820
		internal string _pattern;

		// Token: 0x04002E2D RID: 11821
		internal int _currentPos;

		// Token: 0x04002E2E RID: 11822
		internal CultureInfo _culture;

		// Token: 0x04002E2F RID: 11823
		internal int _autocap;

		// Token: 0x04002E30 RID: 11824
		internal int _capcount;

		// Token: 0x04002E31 RID: 11825
		internal int _captop;

		// Token: 0x04002E32 RID: 11826
		internal int _capsize;

		// Token: 0x04002E33 RID: 11827
		internal Hashtable _caps;

		// Token: 0x04002E34 RID: 11828
		internal Hashtable _capnames;

		// Token: 0x04002E35 RID: 11829
		internal int[] _capnumlist;

		// Token: 0x04002E36 RID: 11830
		internal List<string> _capnamelist;

		// Token: 0x04002E37 RID: 11831
		internal RegexOptions _options;

		// Token: 0x04002E38 RID: 11832
		internal List<RegexOptions> _optionsStack;

		// Token: 0x04002E39 RID: 11833
		internal bool _ignoreNextParen;

		// Token: 0x04002E3A RID: 11834
		internal const int MaxValueDiv10 = 214748364;

		// Token: 0x04002E3B RID: 11835
		internal const int MaxValueMod10 = 7;

		// Token: 0x04002E3C RID: 11836
		internal const byte Q = 5;

		// Token: 0x04002E3D RID: 11837
		internal const byte S = 4;

		// Token: 0x04002E3E RID: 11838
		internal const byte Z = 3;

		// Token: 0x04002E3F RID: 11839
		internal const byte X = 2;

		// Token: 0x04002E40 RID: 11840
		internal const byte E = 1;

		// Token: 0x04002E41 RID: 11841
		internal static readonly byte[] _category = new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 2,
			2, 0, 2, 2, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 2, 0, 0, 3, 4, 0, 0, 0,
			4, 4, 5, 5, 0, 0, 4, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 5, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 4, 4, 0, 4, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 5, 4, 0, 0, 0
		};
	}
}
