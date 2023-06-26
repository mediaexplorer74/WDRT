using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the set of successful matches found by iteratively applying a regular expression pattern to the input string.</summary>
	// Token: 0x0200069D RID: 1693
	[global::__DynamicallyInvokable]
	[Serializable]
	public class MatchCollection : ICollection, IEnumerable
	{
		// Token: 0x06003F08 RID: 16136 RVA: 0x00106EE4 File Offset: 0x001050E4
		internal MatchCollection(Regex regex, string input, int beginning, int length, int startat)
		{
			if (startat < 0 || startat > input.Length)
			{
				throw new ArgumentOutOfRangeException("startat", SR.GetString("BeginIndexNotNegative"));
			}
			this._regex = regex;
			this._input = input;
			this._beginning = beginning;
			this._length = length;
			this._startat = startat;
			this._prevlen = -1;
			this._matches = new ArrayList();
			this._done = false;
		}

		// Token: 0x06003F09 RID: 16137 RVA: 0x00106F5C File Offset: 0x0010515C
		internal Match GetMatch(int i)
		{
			if (i < 0)
			{
				return null;
			}
			if (this._matches.Count > i)
			{
				return (Match)this._matches[i];
			}
			if (this._done)
			{
				return null;
			}
			for (;;)
			{
				Match match = this._regex.Run(false, this._prevlen, this._input, this._beginning, this._length, this._startat);
				if (!match.Success)
				{
					break;
				}
				this._matches.Add(match);
				this._prevlen = match._length;
				this._startat = match._textpos;
				if (this._matches.Count > i)
				{
					return match;
				}
			}
			this._done = true;
			return null;
		}

		/// <summary>Gets the number of matches.</summary>
		/// <returns>The number of matches.</returns>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred.</exception>
		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x06003F0A RID: 16138 RVA: 0x00107009 File Offset: 0x00105209
		[global::__DynamicallyInvokable]
		public int Count
		{
			[global::__DynamicallyInvokable]
			get
			{
				if (this._done)
				{
					return this._matches.Count;
				}
				this.GetMatch(MatchCollection.infinite);
				return this._matches.Count;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection. This property always returns the object itself.</returns>
		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x06003F0B RID: 16139 RVA: 0x00107036 File Offset: 0x00105236
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread-safe).</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x06003F0C RID: 16140 RVA: 0x00107039 File Offset: 0x00105239
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the collection is read only.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x06003F0D RID: 16141 RVA: 0x0010703C File Offset: 0x0010523C
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets an individual member of the collection.</summary>
		/// <param name="i">Index into the <see cref="T:System.Text.RegularExpressions.Match" /> collection.</param>
		/// <returns>The captured substring at position <paramref name="i" /> in the collection.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="i" /> is less than 0 or greater than or equal to <see cref="P:System.Text.RegularExpressions.MatchCollection.Count" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred.</exception>
		// Token: 0x17000ECC RID: 3788
		[global::__DynamicallyInvokable]
		public virtual Match this[int i]
		{
			[global::__DynamicallyInvokable]
			get
			{
				Match match = this.GetMatch(i);
				if (match == null)
				{
					throw new ArgumentOutOfRangeException("i");
				}
				return match;
			}
		}

		/// <summary>Copies all the elements of the collection to the given array starting at the given index.</summary>
		/// <param name="array">The array the collection is to be copied into.</param>
		/// <param name="arrayIndex">The position in the array where copying is to begin.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is a multi-dimensional array.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="arrayIndex" /> is outside the bounds of <paramref name="array" />.  
		/// -or-  
		/// <paramref name="arrayIndex" /> plus <see cref="P:System.Text.RegularExpressions.MatchCollection.Count" /> is outside the bounds of <paramref name="array" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred.</exception>
		// Token: 0x06003F0F RID: 16143 RVA: 0x00107064 File Offset: 0x00105264
		public void CopyTo(Array array, int arrayIndex)
		{
			if (array != null && array.Rank != 1)
			{
				throw new ArgumentException(SR.GetString("Arg_RankMultiDimNotSupported"));
			}
			int count = this.Count;
			try
			{
				this._matches.CopyTo(array, arrayIndex);
			}
			catch (ArrayTypeMismatchException ex)
			{
				throw new ArgumentException(SR.GetString("Arg_InvalidArrayType"), ex);
			}
		}

		/// <summary>Provides an enumerator that iterates through the collection.</summary>
		/// <returns>An object that contains all <see cref="T:System.Text.RegularExpressions.Match" /> objects within the <see cref="T:System.Text.RegularExpressions.MatchCollection" />.</returns>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred.</exception>
		// Token: 0x06003F10 RID: 16144 RVA: 0x001070C8 File Offset: 0x001052C8
		[global::__DynamicallyInvokable]
		public IEnumerator GetEnumerator()
		{
			return new MatchEnumerator(this);
		}

		// Token: 0x04002DE2 RID: 11746
		internal Regex _regex;

		// Token: 0x04002DE3 RID: 11747
		internal ArrayList _matches;

		// Token: 0x04002DE4 RID: 11748
		internal bool _done;

		// Token: 0x04002DE5 RID: 11749
		internal string _input;

		// Token: 0x04002DE6 RID: 11750
		internal int _beginning;

		// Token: 0x04002DE7 RID: 11751
		internal int _length;

		// Token: 0x04002DE8 RID: 11752
		internal int _startat;

		// Token: 0x04002DE9 RID: 11753
		internal int _prevlen;

		// Token: 0x04002DEA RID: 11754
		private static int infinite = int.MaxValue;
	}
}
