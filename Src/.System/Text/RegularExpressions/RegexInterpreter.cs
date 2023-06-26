using System;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200069A RID: 1690
	internal sealed class RegexInterpreter : RegexRunner
	{
		// Token: 0x06003ECB RID: 16075 RVA: 0x0010512C File Offset: 0x0010332C
		internal RegexInterpreter(RegexCode code, CultureInfo culture)
		{
			this.runcode = code;
			this.runcodes = code._codes;
			this.runstrings = code._strings;
			this.runfcPrefix = code._fcPrefix;
			this.runbmPrefix = code._bmPrefix;
			this.runanchors = code._anchors;
			this.runculture = culture;
		}

		// Token: 0x06003ECC RID: 16076 RVA: 0x00105189 File Offset: 0x00103389
		protected override void InitTrackCount()
		{
			this.runtrackcount = this.runcode._trackcount;
		}

		// Token: 0x06003ECD RID: 16077 RVA: 0x0010519C File Offset: 0x0010339C
		private void Advance()
		{
			this.Advance(0);
		}

		// Token: 0x06003ECE RID: 16078 RVA: 0x001051A5 File Offset: 0x001033A5
		private void Advance(int i)
		{
			this.runcodepos += i + 1;
			this.SetOperator(this.runcodes[this.runcodepos]);
		}

		// Token: 0x06003ECF RID: 16079 RVA: 0x001051CA File Offset: 0x001033CA
		private void Goto(int newpos)
		{
			if (newpos < this.runcodepos)
			{
				base.EnsureStorage();
			}
			this.SetOperator(this.runcodes[newpos]);
			this.runcodepos = newpos;
		}

		// Token: 0x06003ED0 RID: 16080 RVA: 0x001051F0 File Offset: 0x001033F0
		private void Textto(int newpos)
		{
			this.runtextpos = newpos;
		}

		// Token: 0x06003ED1 RID: 16081 RVA: 0x001051F9 File Offset: 0x001033F9
		private void Trackto(int newpos)
		{
			this.runtrackpos = this.runtrack.Length - newpos;
		}

		// Token: 0x06003ED2 RID: 16082 RVA: 0x0010520B File Offset: 0x0010340B
		private int Textstart()
		{
			return this.runtextstart;
		}

		// Token: 0x06003ED3 RID: 16083 RVA: 0x00105213 File Offset: 0x00103413
		private int Textpos()
		{
			return this.runtextpos;
		}

		// Token: 0x06003ED4 RID: 16084 RVA: 0x0010521B File Offset: 0x0010341B
		private int Trackpos()
		{
			return this.runtrack.Length - this.runtrackpos;
		}

		// Token: 0x06003ED5 RID: 16085 RVA: 0x0010522C File Offset: 0x0010342C
		private void TrackPush()
		{
			int[] runtrack = this.runtrack;
			int num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack[num] = this.runcodepos;
		}

		// Token: 0x06003ED6 RID: 16086 RVA: 0x00105258 File Offset: 0x00103458
		private void TrackPush(int I1)
		{
			int[] runtrack = this.runtrack;
			int num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack[num] = I1;
			int[] runtrack2 = this.runtrack;
			num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack2[num] = this.runcodepos;
		}

		// Token: 0x06003ED7 RID: 16087 RVA: 0x0010529C File Offset: 0x0010349C
		private void TrackPush(int I1, int I2)
		{
			int[] runtrack = this.runtrack;
			int num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack[num] = I1;
			int[] runtrack2 = this.runtrack;
			num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack2[num] = I2;
			int[] runtrack3 = this.runtrack;
			num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack3[num] = this.runcodepos;
		}

		// Token: 0x06003ED8 RID: 16088 RVA: 0x001052FC File Offset: 0x001034FC
		private void TrackPush(int I1, int I2, int I3)
		{
			int[] runtrack = this.runtrack;
			int num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack[num] = I1;
			int[] runtrack2 = this.runtrack;
			num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack2[num] = I2;
			int[] runtrack3 = this.runtrack;
			num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack3[num] = I3;
			int[] runtrack4 = this.runtrack;
			num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack4[num] = this.runcodepos;
		}

		// Token: 0x06003ED9 RID: 16089 RVA: 0x00105374 File Offset: 0x00103574
		private void TrackPush2(int I1)
		{
			int[] runtrack = this.runtrack;
			int num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack[num] = I1;
			int[] runtrack2 = this.runtrack;
			num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack2[num] = -this.runcodepos;
		}

		// Token: 0x06003EDA RID: 16090 RVA: 0x001053BC File Offset: 0x001035BC
		private void TrackPush2(int I1, int I2)
		{
			int[] runtrack = this.runtrack;
			int num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack[num] = I1;
			int[] runtrack2 = this.runtrack;
			num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack2[num] = I2;
			int[] runtrack3 = this.runtrack;
			num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack3[num] = -this.runcodepos;
		}

		// Token: 0x06003EDB RID: 16091 RVA: 0x0010541C File Offset: 0x0010361C
		private void Backtrack()
		{
			int[] runtrack = this.runtrack;
			int runtrackpos = this.runtrackpos;
			this.runtrackpos = runtrackpos + 1;
			int num = runtrack[runtrackpos];
			if (num < 0)
			{
				num = -num;
				this.SetOperator(this.runcodes[num] | 256);
			}
			else
			{
				this.SetOperator(this.runcodes[num] | 128);
			}
			if (num < this.runcodepos)
			{
				base.EnsureStorage();
			}
			this.runcodepos = num;
		}

		// Token: 0x06003EDC RID: 16092 RVA: 0x00105489 File Offset: 0x00103689
		private void SetOperator(int op)
		{
			this.runci = (op & 512) != 0;
			this.runrtl = (op & 64) != 0;
			this.runoperator = op & -577;
		}

		// Token: 0x06003EDD RID: 16093 RVA: 0x001054B5 File Offset: 0x001036B5
		private void TrackPop()
		{
			this.runtrackpos++;
		}

		// Token: 0x06003EDE RID: 16094 RVA: 0x001054C5 File Offset: 0x001036C5
		private void TrackPop(int framesize)
		{
			this.runtrackpos += framesize;
		}

		// Token: 0x06003EDF RID: 16095 RVA: 0x001054D5 File Offset: 0x001036D5
		private int TrackPeek()
		{
			return this.runtrack[this.runtrackpos - 1];
		}

		// Token: 0x06003EE0 RID: 16096 RVA: 0x001054E6 File Offset: 0x001036E6
		private int TrackPeek(int i)
		{
			return this.runtrack[this.runtrackpos - i - 1];
		}

		// Token: 0x06003EE1 RID: 16097 RVA: 0x001054FC File Offset: 0x001036FC
		private void StackPush(int I1)
		{
			int[] runstack = this.runstack;
			int num = this.runstackpos - 1;
			this.runstackpos = num;
			runstack[num] = I1;
		}

		// Token: 0x06003EE2 RID: 16098 RVA: 0x00105524 File Offset: 0x00103724
		private void StackPush(int I1, int I2)
		{
			int[] runstack = this.runstack;
			int num = this.runstackpos - 1;
			this.runstackpos = num;
			runstack[num] = I1;
			int[] runstack2 = this.runstack;
			num = this.runstackpos - 1;
			this.runstackpos = num;
			runstack2[num] = I2;
		}

		// Token: 0x06003EE3 RID: 16099 RVA: 0x00105563 File Offset: 0x00103763
		private void StackPop()
		{
			this.runstackpos++;
		}

		// Token: 0x06003EE4 RID: 16100 RVA: 0x00105573 File Offset: 0x00103773
		private void StackPop(int framesize)
		{
			this.runstackpos += framesize;
		}

		// Token: 0x06003EE5 RID: 16101 RVA: 0x00105583 File Offset: 0x00103783
		private int StackPeek()
		{
			return this.runstack[this.runstackpos - 1];
		}

		// Token: 0x06003EE6 RID: 16102 RVA: 0x00105594 File Offset: 0x00103794
		private int StackPeek(int i)
		{
			return this.runstack[this.runstackpos - i - 1];
		}

		// Token: 0x06003EE7 RID: 16103 RVA: 0x001055A7 File Offset: 0x001037A7
		private int Operator()
		{
			return this.runoperator;
		}

		// Token: 0x06003EE8 RID: 16104 RVA: 0x001055AF File Offset: 0x001037AF
		private int Operand(int i)
		{
			return this.runcodes[this.runcodepos + i + 1];
		}

		// Token: 0x06003EE9 RID: 16105 RVA: 0x001055C2 File Offset: 0x001037C2
		private int Leftchars()
		{
			return this.runtextpos - this.runtextbeg;
		}

		// Token: 0x06003EEA RID: 16106 RVA: 0x001055D1 File Offset: 0x001037D1
		private int Rightchars()
		{
			return this.runtextend - this.runtextpos;
		}

		// Token: 0x06003EEB RID: 16107 RVA: 0x001055E0 File Offset: 0x001037E0
		private int Bump()
		{
			if (!this.runrtl)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x06003EEC RID: 16108 RVA: 0x001055ED File Offset: 0x001037ED
		private int Forwardchars()
		{
			if (!this.runrtl)
			{
				return this.runtextend - this.runtextpos;
			}
			return this.runtextpos - this.runtextbeg;
		}

		// Token: 0x06003EED RID: 16109 RVA: 0x00105614 File Offset: 0x00103814
		private char Forwardcharnext()
		{
			char c;
			if (!this.runrtl)
			{
				string runtext = this.runtext;
				int num = this.runtextpos;
				this.runtextpos = num + 1;
				c = runtext[num];
			}
			else
			{
				string runtext2 = this.runtext;
				int num = this.runtextpos - 1;
				this.runtextpos = num;
				c = runtext2[num];
			}
			char c2 = c;
			if (!this.runci)
			{
				return c2;
			}
			return char.ToLower(c2, this.runculture);
		}

		// Token: 0x06003EEE RID: 16110 RVA: 0x0010567C File Offset: 0x0010387C
		private bool Stringmatch(string str)
		{
			int num;
			int num2;
			if (!this.runrtl)
			{
				if (this.runtextend - this.runtextpos < (num = str.Length))
				{
					return false;
				}
				num2 = this.runtextpos + num;
			}
			else
			{
				if (this.runtextpos - this.runtextbeg < (num = str.Length))
				{
					return false;
				}
				num2 = this.runtextpos;
			}
			if (!this.runci)
			{
				while (num != 0)
				{
					if (str[--num] != this.runtext[--num2])
					{
						return false;
					}
				}
			}
			else
			{
				while (num != 0)
				{
					if (str[--num] != char.ToLower(this.runtext[--num2], this.runculture))
					{
						return false;
					}
				}
			}
			if (!this.runrtl)
			{
				num2 += str.Length;
			}
			this.runtextpos = num2;
			return true;
		}

		// Token: 0x06003EEF RID: 16111 RVA: 0x0010574C File Offset: 0x0010394C
		private bool Refmatch(int index, int len)
		{
			int num;
			if (!this.runrtl)
			{
				if (this.runtextend - this.runtextpos < len)
				{
					return false;
				}
				num = this.runtextpos + len;
			}
			else
			{
				if (this.runtextpos - this.runtextbeg < len)
				{
					return false;
				}
				num = this.runtextpos;
			}
			int num2 = index + len;
			int num3 = len;
			if (!this.runci)
			{
				while (num3-- != 0)
				{
					if (this.runtext[--num2] != this.runtext[--num])
					{
						return false;
					}
				}
			}
			else
			{
				while (num3-- != 0)
				{
					if (char.ToLower(this.runtext[--num2], this.runculture) != char.ToLower(this.runtext[--num], this.runculture))
					{
						return false;
					}
				}
			}
			if (!this.runrtl)
			{
				num += len;
			}
			this.runtextpos = num;
			return true;
		}

		// Token: 0x06003EF0 RID: 16112 RVA: 0x00105829 File Offset: 0x00103A29
		private void Backwardnext()
		{
			this.runtextpos += (this.runrtl ? 1 : (-1));
		}

		// Token: 0x06003EF1 RID: 16113 RVA: 0x00105844 File Offset: 0x00103A44
		private char CharAt(int j)
		{
			return this.runtext[j];
		}

		// Token: 0x06003EF2 RID: 16114 RVA: 0x00105854 File Offset: 0x00103A54
		protected override bool FindFirstChar()
		{
			if ((this.runanchors & 53) != 0)
			{
				if (!this.runcode._rightToLeft)
				{
					if (((this.runanchors & 1) != 0 && this.runtextpos > this.runtextbeg) || ((this.runanchors & 4) != 0 && this.runtextpos > this.runtextstart))
					{
						this.runtextpos = this.runtextend;
						return false;
					}
					if ((this.runanchors & 16) != 0 && this.runtextpos < this.runtextend - 1)
					{
						this.runtextpos = this.runtextend - 1;
					}
					else if ((this.runanchors & 32) != 0 && this.runtextpos < this.runtextend)
					{
						this.runtextpos = this.runtextend;
					}
				}
				else
				{
					if (((this.runanchors & 32) != 0 && this.runtextpos < this.runtextend) || ((this.runanchors & 16) != 0 && (this.runtextpos < this.runtextend - 1 || (this.runtextpos == this.runtextend - 1 && this.CharAt(this.runtextpos) != '\n'))) || ((this.runanchors & 4) != 0 && this.runtextpos < this.runtextstart))
					{
						this.runtextpos = this.runtextbeg;
						return false;
					}
					if ((this.runanchors & 1) != 0 && this.runtextpos > this.runtextbeg)
					{
						this.runtextpos = this.runtextbeg;
					}
				}
				return this.runbmPrefix == null || this.runbmPrefix.IsMatch(this.runtext, this.runtextpos, this.runtextbeg, this.runtextend);
			}
			if (this.runbmPrefix != null)
			{
				this.runtextpos = this.runbmPrefix.Scan(this.runtext, this.runtextpos, this.runtextbeg, this.runtextend);
				if (this.runtextpos == -1)
				{
					this.runtextpos = (this.runcode._rightToLeft ? this.runtextbeg : this.runtextend);
					return false;
				}
				return true;
			}
			else
			{
				if (this.runfcPrefix == null)
				{
					return true;
				}
				this.runrtl = this.runcode._rightToLeft;
				this.runci = this.runfcPrefix.CaseInsensitive;
				string prefix = this.runfcPrefix.Prefix;
				if (RegexCharClass.IsSingleton(prefix))
				{
					char c = RegexCharClass.SingletonChar(prefix);
					for (int i = this.Forwardchars(); i > 0; i--)
					{
						if (c == this.Forwardcharnext())
						{
							this.Backwardnext();
							return true;
						}
					}
				}
				else
				{
					for (int i = this.Forwardchars(); i > 0; i--)
					{
						if (RegexCharClass.CharInClass(this.Forwardcharnext(), prefix))
						{
							this.Backwardnext();
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x06003EF3 RID: 16115 RVA: 0x00105ADC File Offset: 0x00103CDC
		protected override void Go()
		{
			this.Goto(0);
			for (;;)
			{
				base.CheckTimeout();
				int num = this.Operator();
				switch (num)
				{
				case 0:
				{
					int num2 = this.Operand(1);
					if (this.Forwardchars() >= num2)
					{
						char c = (char)this.Operand(0);
						while (num2-- > 0)
						{
							if (this.Forwardcharnext() != c)
							{
								goto IL_E82;
							}
						}
						this.Advance(2);
						continue;
					}
					break;
				}
				case 1:
				{
					int num3 = this.Operand(1);
					if (this.Forwardchars() >= num3)
					{
						char c2 = (char)this.Operand(0);
						while (num3-- > 0)
						{
							if (this.Forwardcharnext() == c2)
							{
								goto IL_E82;
							}
						}
						this.Advance(2);
						continue;
					}
					break;
				}
				case 2:
				{
					int num4 = this.Operand(1);
					if (this.Forwardchars() >= num4)
					{
						string text = this.runstrings[this.Operand(0)];
						while (num4-- > 0)
						{
							if (!RegexInterpreter.UseLegacyTimeoutCheck && num4 % 2000 == 0)
							{
								base.CheckTimeout();
							}
							if (!RegexCharClass.CharInClass(this.Forwardcharnext(), text))
							{
								goto IL_E82;
							}
						}
						this.Advance(2);
						continue;
					}
					break;
				}
				case 3:
				{
					int num5 = this.Operand(1);
					if (num5 > this.Forwardchars())
					{
						num5 = this.Forwardchars();
					}
					char c3 = (char)this.Operand(0);
					int i;
					for (i = num5; i > 0; i--)
					{
						if (this.Forwardcharnext() != c3)
						{
							this.Backwardnext();
							break;
						}
					}
					if (num5 > i)
					{
						this.TrackPush(num5 - i - 1, this.Textpos() - this.Bump());
					}
					this.Advance(2);
					continue;
				}
				case 4:
				{
					int num6 = this.Operand(1);
					if (num6 > this.Forwardchars())
					{
						num6 = this.Forwardchars();
					}
					char c4 = (char)this.Operand(0);
					int j;
					for (j = num6; j > 0; j--)
					{
						if (this.Forwardcharnext() == c4)
						{
							this.Backwardnext();
							break;
						}
					}
					if (num6 > j)
					{
						this.TrackPush(num6 - j - 1, this.Textpos() - this.Bump());
					}
					this.Advance(2);
					continue;
				}
				case 5:
				{
					int num7 = this.Operand(1);
					if (num7 > this.Forwardchars())
					{
						num7 = this.Forwardchars();
					}
					string text2 = this.runstrings[this.Operand(0)];
					int k;
					for (k = num7; k > 0; k--)
					{
						if (!RegexInterpreter.UseLegacyTimeoutCheck && k % 2000 == 0)
						{
							base.CheckTimeout();
						}
						if (!RegexCharClass.CharInClass(this.Forwardcharnext(), text2))
						{
							this.Backwardnext();
							break;
						}
					}
					if (num7 > k)
					{
						this.TrackPush(num7 - k - 1, this.Textpos() - this.Bump());
					}
					this.Advance(2);
					continue;
				}
				case 6:
				case 7:
				{
					int num8 = this.Operand(1);
					if (num8 > this.Forwardchars())
					{
						num8 = this.Forwardchars();
					}
					if (num8 > 0)
					{
						this.TrackPush(num8 - 1, this.Textpos());
					}
					this.Advance(2);
					continue;
				}
				case 8:
				{
					int num9 = this.Operand(1);
					if (num9 > this.Forwardchars())
					{
						num9 = this.Forwardchars();
					}
					if (num9 > 0)
					{
						this.TrackPush(num9 - 1, this.Textpos());
					}
					this.Advance(2);
					continue;
				}
				case 9:
					if (this.Forwardchars() >= 1 && this.Forwardcharnext() == (char)this.Operand(0))
					{
						this.Advance(1);
						continue;
					}
					break;
				case 10:
					if (this.Forwardchars() >= 1 && this.Forwardcharnext() != (char)this.Operand(0))
					{
						this.Advance(1);
						continue;
					}
					break;
				case 11:
					if (this.Forwardchars() >= 1 && RegexCharClass.CharInClass(this.Forwardcharnext(), this.runstrings[this.Operand(0)]))
					{
						this.Advance(1);
						continue;
					}
					break;
				case 12:
					if (this.Stringmatch(this.runstrings[this.Operand(0)]))
					{
						this.Advance(1);
						continue;
					}
					break;
				case 13:
				{
					int num10 = this.Operand(0);
					if (base.IsMatched(num10))
					{
						if (!this.Refmatch(base.MatchIndex(num10), base.MatchLength(num10)))
						{
							break;
						}
					}
					else if ((this.runregex.roptions & RegexOptions.ECMAScript) == RegexOptions.None)
					{
						break;
					}
					this.Advance(1);
					continue;
				}
				case 14:
					if (this.Leftchars() <= 0 || this.CharAt(this.Textpos() - 1) == '\n')
					{
						this.Advance();
						continue;
					}
					break;
				case 15:
					if (this.Rightchars() <= 0 || this.CharAt(this.Textpos()) == '\n')
					{
						this.Advance();
						continue;
					}
					break;
				case 16:
					if (base.IsBoundary(this.Textpos(), this.runtextbeg, this.runtextend))
					{
						this.Advance();
						continue;
					}
					break;
				case 17:
					if (!base.IsBoundary(this.Textpos(), this.runtextbeg, this.runtextend))
					{
						this.Advance();
						continue;
					}
					break;
				case 18:
					if (this.Leftchars() <= 0)
					{
						this.Advance();
						continue;
					}
					break;
				case 19:
					if (this.Textpos() == this.Textstart())
					{
						this.Advance();
						continue;
					}
					break;
				case 20:
					if (this.Rightchars() <= 1 && (this.Rightchars() != 1 || this.CharAt(this.Textpos()) == '\n'))
					{
						this.Advance();
						continue;
					}
					break;
				case 21:
					if (this.Rightchars() <= 0)
					{
						this.Advance();
						continue;
					}
					break;
				case 22:
					break;
				case 23:
					this.TrackPush(this.Textpos());
					this.Advance(1);
					continue;
				case 24:
				{
					this.StackPop();
					int num11 = this.Textpos() - this.StackPeek();
					if (num11 != 0)
					{
						this.TrackPush(this.StackPeek(), this.Textpos());
						this.StackPush(this.Textpos());
						this.Goto(this.Operand(0));
						continue;
					}
					this.TrackPush2(this.StackPeek());
					this.Advance(1);
					continue;
				}
				case 25:
				{
					this.StackPop();
					int num12 = this.StackPeek();
					if (this.Textpos() != num12)
					{
						if (num12 != -1)
						{
							this.TrackPush(num12, this.Textpos());
						}
						else
						{
							this.TrackPush(this.Textpos(), this.Textpos());
						}
					}
					else
					{
						this.StackPush(num12);
						this.TrackPush2(this.StackPeek());
					}
					this.Advance(1);
					continue;
				}
				case 26:
					this.StackPush(-1, this.Operand(0));
					this.TrackPush();
					this.Advance(1);
					continue;
				case 27:
					this.StackPush(this.Textpos(), this.Operand(0));
					this.TrackPush();
					this.Advance(1);
					continue;
				case 28:
				{
					this.StackPop(2);
					int num13 = this.StackPeek();
					int num14 = this.StackPeek(1);
					int num15 = this.Textpos() - num13;
					if (num14 >= this.Operand(1) || (num15 == 0 && num14 >= 0))
					{
						this.TrackPush2(num13, num14);
						this.Advance(2);
						continue;
					}
					this.TrackPush(num13);
					this.StackPush(this.Textpos(), num14 + 1);
					this.Goto(this.Operand(0));
					continue;
				}
				case 29:
				{
					this.StackPop(2);
					int num16 = this.StackPeek();
					int num17 = this.StackPeek(1);
					if (num17 < 0)
					{
						this.TrackPush2(num16);
						this.StackPush(this.Textpos(), num17 + 1);
						this.Goto(this.Operand(0));
						continue;
					}
					this.TrackPush(num16, num17, this.Textpos());
					this.Advance(2);
					continue;
				}
				case 30:
					this.StackPush(-1);
					this.TrackPush();
					this.Advance();
					continue;
				case 31:
					this.StackPush(this.Textpos());
					this.TrackPush();
					this.Advance();
					continue;
				case 32:
					if (this.Operand(1) == -1 || base.IsMatched(this.Operand(1)))
					{
						this.StackPop();
						if (this.Operand(1) != -1)
						{
							base.TransferCapture(this.Operand(0), this.Operand(1), this.StackPeek(), this.Textpos());
						}
						else
						{
							base.Capture(this.Operand(0), this.StackPeek(), this.Textpos());
						}
						this.TrackPush(this.StackPeek());
						this.Advance(2);
						continue;
					}
					break;
				case 33:
					this.StackPop();
					this.TrackPush(this.StackPeek());
					this.Textto(this.StackPeek());
					this.Advance();
					continue;
				case 34:
					this.StackPush(this.Trackpos(), base.Crawlpos());
					this.TrackPush();
					this.Advance();
					continue;
				case 35:
					this.StackPop(2);
					this.Trackto(this.StackPeek());
					while (base.Crawlpos() != this.StackPeek(1))
					{
						base.Uncapture();
					}
					break;
				case 36:
					this.StackPop(2);
					this.Trackto(this.StackPeek());
					this.TrackPush(this.StackPeek(1));
					this.Advance();
					continue;
				case 37:
					if (base.IsMatched(this.Operand(0)))
					{
						this.Advance(1);
						continue;
					}
					break;
				case 38:
					this.Goto(this.Operand(0));
					continue;
				case 39:
					goto IL_E72;
				case 40:
					return;
				case 41:
					if (base.IsECMABoundary(this.Textpos(), this.runtextbeg, this.runtextend))
					{
						this.Advance();
						continue;
					}
					break;
				case 42:
					if (!base.IsECMABoundary(this.Textpos(), this.runtextbeg, this.runtextend))
					{
						this.Advance();
						continue;
					}
					break;
				default:
					switch (num)
					{
					case 131:
					case 132:
					{
						this.TrackPop(2);
						int num18 = this.TrackPeek();
						int num19 = this.TrackPeek(1);
						this.Textto(num19);
						if (num18 > 0)
						{
							this.TrackPush(num18 - 1, num19 - this.Bump());
						}
						this.Advance(2);
						continue;
					}
					case 133:
					{
						this.TrackPop(2);
						int num20 = this.TrackPeek();
						int num21 = this.TrackPeek(1);
						this.Textto(num21);
						if (num20 > 0)
						{
							this.TrackPush(num20 - 1, num21 - this.Bump());
						}
						this.Advance(2);
						continue;
					}
					case 134:
					{
						this.TrackPop(2);
						int num22 = this.TrackPeek(1);
						this.Textto(num22);
						if (this.Forwardcharnext() == (char)this.Operand(0))
						{
							int num23 = this.TrackPeek();
							if (num23 > 0)
							{
								this.TrackPush(num23 - 1, num22 + this.Bump());
							}
							this.Advance(2);
							continue;
						}
						break;
					}
					case 135:
					{
						this.TrackPop(2);
						int num24 = this.TrackPeek(1);
						this.Textto(num24);
						if (this.Forwardcharnext() != (char)this.Operand(0))
						{
							int num25 = this.TrackPeek();
							if (num25 > 0)
							{
								this.TrackPush(num25 - 1, num24 + this.Bump());
							}
							this.Advance(2);
							continue;
						}
						break;
					}
					case 136:
					{
						this.TrackPop(2);
						int num26 = this.TrackPeek(1);
						this.Textto(num26);
						if (RegexCharClass.CharInClass(this.Forwardcharnext(), this.runstrings[this.Operand(0)]))
						{
							int num27 = this.TrackPeek();
							if (num27 > 0)
							{
								this.TrackPush(num27 - 1, num26 + this.Bump());
							}
							this.Advance(2);
							continue;
						}
						break;
					}
					case 137:
					case 138:
					case 139:
					case 140:
					case 141:
					case 142:
					case 143:
					case 144:
					case 145:
					case 146:
					case 147:
					case 148:
					case 149:
					case 150:
					case 163:
						goto IL_E72;
					case 151:
						this.TrackPop();
						this.Textto(this.TrackPeek());
						this.Goto(this.Operand(0));
						continue;
					case 152:
						this.TrackPop(2);
						this.StackPop();
						this.Textto(this.TrackPeek(1));
						this.TrackPush2(this.TrackPeek());
						this.Advance(1);
						continue;
					case 153:
					{
						this.TrackPop(2);
						int num28 = this.TrackPeek(1);
						this.TrackPush2(this.TrackPeek());
						this.StackPush(num28);
						this.Textto(num28);
						this.Goto(this.Operand(0));
						continue;
					}
					case 154:
						this.StackPop(2);
						break;
					case 155:
						this.StackPop(2);
						break;
					case 156:
						this.TrackPop();
						this.StackPop(2);
						if (this.StackPeek(1) > 0)
						{
							this.Textto(this.StackPeek());
							this.TrackPush2(this.TrackPeek(), this.StackPeek(1) - 1);
							this.Advance(2);
							continue;
						}
						this.StackPush(this.TrackPeek(), this.StackPeek(1) - 1);
						break;
					case 157:
					{
						this.TrackPop(3);
						int num29 = this.TrackPeek();
						int num30 = this.TrackPeek(2);
						if (this.TrackPeek(1) < this.Operand(1) && num30 != num29)
						{
							this.Textto(num30);
							this.StackPush(num30, this.TrackPeek(1) + 1);
							this.TrackPush2(num29);
							this.Goto(this.Operand(0));
							continue;
						}
						this.StackPush(this.TrackPeek(), this.TrackPeek(1));
						break;
					}
					case 158:
					case 159:
						this.StackPop();
						break;
					case 160:
						this.TrackPop();
						this.StackPush(this.TrackPeek());
						base.Uncapture();
						if (this.Operand(0) != -1 && this.Operand(1) != -1)
						{
							base.Uncapture();
						}
						break;
					case 161:
						this.TrackPop();
						this.StackPush(this.TrackPeek());
						break;
					case 162:
						this.StackPop(2);
						break;
					case 164:
						this.TrackPop();
						while (base.Crawlpos() != this.TrackPeek())
						{
							base.Uncapture();
						}
						break;
					default:
						switch (num)
						{
						case 280:
							this.TrackPop();
							this.StackPush(this.TrackPeek());
							goto IL_E82;
						case 281:
							this.StackPop();
							this.TrackPop();
							this.StackPush(this.TrackPeek());
							goto IL_E82;
						case 284:
							this.TrackPop(2);
							this.StackPush(this.TrackPeek(), this.TrackPeek(1));
							goto IL_E82;
						case 285:
							this.TrackPop();
							this.StackPop(2);
							this.StackPush(this.TrackPeek(), this.StackPeek(1) - 1);
							goto IL_E82;
						}
						goto Block_3;
					}
					break;
				}
				IL_E82:
				this.Backtrack();
			}
			Block_3:
			IL_E72:
			throw new NotImplementedException(SR.GetString("UnimplementedState"));
		}

		// Token: 0x04002DCA RID: 11722
		internal int runoperator;

		// Token: 0x04002DCB RID: 11723
		internal int[] runcodes;

		// Token: 0x04002DCC RID: 11724
		internal int runcodepos;

		// Token: 0x04002DCD RID: 11725
		internal string[] runstrings;

		// Token: 0x04002DCE RID: 11726
		internal RegexCode runcode;

		// Token: 0x04002DCF RID: 11727
		internal RegexPrefix runfcPrefix;

		// Token: 0x04002DD0 RID: 11728
		internal RegexBoyerMoore runbmPrefix;

		// Token: 0x04002DD1 RID: 11729
		internal int runanchors;

		// Token: 0x04002DD2 RID: 11730
		internal bool runrtl;

		// Token: 0x04002DD3 RID: 11731
		internal bool runci;

		// Token: 0x04002DD4 RID: 11732
		internal CultureInfo runculture;

		// Token: 0x04002DD5 RID: 11733
		private const int LoopTimeoutCheckCount = 2000;

		// Token: 0x04002DD6 RID: 11734
		private static readonly bool UseLegacyTimeoutCheck = LocalAppContextSwitches.UseLegacyTimeoutCheck;
	}
}
