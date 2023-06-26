using System;
using System.Security.Permissions;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the results from a single regular expression match.</summary>
	// Token: 0x0200069B RID: 1691
	[global::__DynamicallyInvokable]
	[Serializable]
	public class Match : Group
	{
		/// <summary>Gets the empty group. All failed matches return this empty match.</summary>
		/// <returns>An empty match.</returns>
		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x06003EF5 RID: 16117 RVA: 0x00106981 File Offset: 0x00104B81
		[global::__DynamicallyInvokable]
		public static Match Empty
		{
			[global::__DynamicallyInvokable]
			get
			{
				return Match._empty;
			}
		}

		// Token: 0x06003EF6 RID: 16118 RVA: 0x00106988 File Offset: 0x00104B88
		internal Match(Regex regex, int capcount, string text, int begpos, int len, int startpos)
			: base(text, new int[2], 0, "0")
		{
			this._regex = regex;
			this._matchcount = new int[capcount];
			this._matches = new int[capcount][];
			this._matches[0] = this._caps;
			this._textbeg = begpos;
			this._textend = begpos + len;
			this._textstart = startpos;
			this._balancing = false;
		}

		// Token: 0x06003EF7 RID: 16119 RVA: 0x001069F8 File Offset: 0x00104BF8
		internal virtual void Reset(Regex regex, string text, int textbeg, int textend, int textstart)
		{
			this._regex = regex;
			this._text = text;
			this._textbeg = textbeg;
			this._textend = textend;
			this._textstart = textstart;
			for (int i = 0; i < this._matchcount.Length; i++)
			{
				this._matchcount[i] = 0;
			}
			this._balancing = false;
		}

		/// <summary>Gets a collection of groups matched by the regular expression.</summary>
		/// <returns>The character groups matched by the pattern.</returns>
		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x06003EF8 RID: 16120 RVA: 0x00106A4D File Offset: 0x00104C4D
		[global::__DynamicallyInvokable]
		public virtual GroupCollection Groups
		{
			[global::__DynamicallyInvokable]
			get
			{
				if (this._groupcoll == null)
				{
					this._groupcoll = new GroupCollection(this, null);
				}
				return this._groupcoll;
			}
		}

		/// <summary>Returns a new <see cref="T:System.Text.RegularExpressions.Match" /> object with the results for the next match, starting at the position at which the last match ended (at the character after the last matched character).</summary>
		/// <returns>The next regular expression match.</returns>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred.</exception>
		// Token: 0x06003EF9 RID: 16121 RVA: 0x00106A6A File Offset: 0x00104C6A
		[global::__DynamicallyInvokable]
		public Match NextMatch()
		{
			if (this._regex == null)
			{
				return this;
			}
			return this._regex.Run(false, this._length, this._text, this._textbeg, this._textend - this._textbeg, this._textpos);
		}

		/// <summary>Returns the expansion of the specified replacement pattern.</summary>
		/// <param name="replacement">The replacement pattern to use.</param>
		/// <returns>The expanded version of the <paramref name="replacement" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="replacement" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">Expansion is not allowed for this pattern.</exception>
		// Token: 0x06003EFA RID: 16122 RVA: 0x00106AA8 File Offset: 0x00104CA8
		[global::__DynamicallyInvokable]
		public virtual string Result(string replacement)
		{
			if (replacement == null)
			{
				throw new ArgumentNullException("replacement");
			}
			if (this._regex == null)
			{
				throw new NotSupportedException(SR.GetString("NoResultOnFailed"));
			}
			RegexReplacement regexReplacement = (RegexReplacement)this._regex.replref.Get();
			if (regexReplacement == null || !regexReplacement.Pattern.Equals(replacement))
			{
				regexReplacement = RegexParser.ParseReplacement(replacement, this._regex.caps, this._regex.capsize, this._regex.capnames, this._regex.roptions);
				this._regex.replref.Cache(regexReplacement);
			}
			return regexReplacement.Replacement(this);
		}

		// Token: 0x06003EFB RID: 16123 RVA: 0x00106B50 File Offset: 0x00104D50
		internal virtual string GroupToStringImpl(int groupnum)
		{
			int num = this._matchcount[groupnum];
			if (num == 0)
			{
				return string.Empty;
			}
			int[] array = this._matches[groupnum];
			return this._text.Substring(array[(num - 1) * 2], array[num * 2 - 1]);
		}

		// Token: 0x06003EFC RID: 16124 RVA: 0x00106B91 File Offset: 0x00104D91
		internal string LastGroupToStringImpl()
		{
			return this.GroupToStringImpl(this._matchcount.Length - 1);
		}

		/// <summary>Returns a <see cref="T:System.Text.RegularExpressions.Match" /> instance equivalent to the one supplied that is suitable to share between multiple threads.</summary>
		/// <param name="inner">A regular expression match equivalent to the one expected.</param>
		/// <returns>A regular expression match that is suitable to share between multiple threads.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inner" /> is <see langword="null" />.</exception>
		// Token: 0x06003EFD RID: 16125 RVA: 0x00106BA4 File Offset: 0x00104DA4
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static Match Synchronized(Match inner)
		{
			if (inner == null)
			{
				throw new ArgumentNullException("inner");
			}
			int num = inner._matchcount.Length;
			for (int i = 0; i < num; i++)
			{
				Group group = inner.Groups[i];
				Group.Synchronized(group);
			}
			return inner;
		}

		// Token: 0x06003EFE RID: 16126 RVA: 0x00106BEC File Offset: 0x00104DEC
		internal virtual void AddMatch(int cap, int start, int len)
		{
			if (this._matches[cap] == null)
			{
				this._matches[cap] = new int[2];
			}
			int num = this._matchcount[cap];
			if (num * 2 + 2 > this._matches[cap].Length)
			{
				int[] array = this._matches[cap];
				int[] array2 = new int[num * 8];
				for (int i = 0; i < num * 2; i++)
				{
					array2[i] = array[i];
				}
				this._matches[cap] = array2;
			}
			this._matches[cap][num * 2] = start;
			this._matches[cap][num * 2 + 1] = len;
			this._matchcount[cap] = num + 1;
		}

		// Token: 0x06003EFF RID: 16127 RVA: 0x00106C84 File Offset: 0x00104E84
		internal virtual void BalanceMatch(int cap)
		{
			this._balancing = true;
			int num = this._matchcount[cap];
			int num2 = num * 2 - 2;
			if (this._matches[cap][num2] < 0)
			{
				num2 = -3 - this._matches[cap][num2];
			}
			num2 -= 2;
			if (num2 >= 0 && this._matches[cap][num2] < 0)
			{
				this.AddMatch(cap, this._matches[cap][num2], this._matches[cap][num2 + 1]);
				return;
			}
			this.AddMatch(cap, -3 - num2, -4 - num2);
		}

		// Token: 0x06003F00 RID: 16128 RVA: 0x00106D04 File Offset: 0x00104F04
		internal virtual void RemoveMatch(int cap)
		{
			this._matchcount[cap]--;
		}

		// Token: 0x06003F01 RID: 16129 RVA: 0x00106D17 File Offset: 0x00104F17
		internal virtual bool IsMatched(int cap)
		{
			return cap < this._matchcount.Length && this._matchcount[cap] > 0 && this._matches[cap][this._matchcount[cap] * 2 - 1] != -2;
		}

		// Token: 0x06003F02 RID: 16130 RVA: 0x00106D50 File Offset: 0x00104F50
		internal virtual int MatchIndex(int cap)
		{
			int num = this._matches[cap][this._matchcount[cap] * 2 - 2];
			if (num >= 0)
			{
				return num;
			}
			return this._matches[cap][-3 - num];
		}

		// Token: 0x06003F03 RID: 16131 RVA: 0x00106D88 File Offset: 0x00104F88
		internal virtual int MatchLength(int cap)
		{
			int num = this._matches[cap][this._matchcount[cap] * 2 - 1];
			if (num >= 0)
			{
				return num;
			}
			return this._matches[cap][-3 - num];
		}

		// Token: 0x06003F04 RID: 16132 RVA: 0x00106DC0 File Offset: 0x00104FC0
		internal virtual void Tidy(int textpos)
		{
			int[] array = this._matches[0];
			this._index = array[0];
			this._length = array[1];
			this._textpos = textpos;
			this._capcount = this._matchcount[0];
			if (this._balancing)
			{
				for (int i = 0; i < this._matchcount.Length; i++)
				{
					int num = this._matchcount[i] * 2;
					int[] array2 = this._matches[i];
					int j = 0;
					while (j < num && array2[j] >= 0)
					{
						j++;
					}
					int num2 = j;
					while (j < num)
					{
						if (array2[j] < 0)
						{
							num2--;
						}
						else
						{
							if (j != num2)
							{
								array2[num2] = array2[j];
							}
							num2++;
						}
						j++;
					}
					this._matchcount[i] = num2 / 2;
				}
				this._balancing = false;
			}
		}

		// Token: 0x04002DD7 RID: 11735
		internal static Match _empty = new Match(null, 1, string.Empty, 0, 0, 0);

		// Token: 0x04002DD8 RID: 11736
		internal GroupCollection _groupcoll;

		// Token: 0x04002DD9 RID: 11737
		internal Regex _regex;

		// Token: 0x04002DDA RID: 11738
		internal int _textbeg;

		// Token: 0x04002DDB RID: 11739
		internal int _textpos;

		// Token: 0x04002DDC RID: 11740
		internal int _textend;

		// Token: 0x04002DDD RID: 11741
		internal int _textstart;

		// Token: 0x04002DDE RID: 11742
		internal int[][] _matches;

		// Token: 0x04002DDF RID: 11743
		internal int[] _matchcount;

		// Token: 0x04002DE0 RID: 11744
		internal bool _balancing;
	}
}
