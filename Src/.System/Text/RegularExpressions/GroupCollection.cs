using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	/// <summary>Returns the set of captured groups in a single match.</summary>
	// Token: 0x02000698 RID: 1688
	[global::__DynamicallyInvokable]
	[Serializable]
	public class GroupCollection : ICollection, IEnumerable
	{
		// Token: 0x06003EBB RID: 16059 RVA: 0x00104ED1 File Offset: 0x001030D1
		internal GroupCollection(Match match, Hashtable caps)
		{
			this._match = match;
			this._captureMap = caps;
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Text.RegularExpressions.GroupCollection" />.</summary>
		/// <returns>A copy of the <see cref="T:System.Text.RegularExpressions.Match" /> object to synchronize.</returns>
		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x06003EBC RID: 16060 RVA: 0x00104EE7 File Offset: 0x001030E7
		public object SyncRoot
		{
			get
			{
				return this._match;
			}
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Text.RegularExpressions.GroupCollection" /> is synchronized (thread-safe).</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x06003EBD RID: 16061 RVA: 0x00104EEF File Offset: 0x001030EF
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the collection is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x06003EBE RID: 16062 RVA: 0x00104EF2 File Offset: 0x001030F2
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Returns the number of groups in the collection.</summary>
		/// <returns>The number of groups in the collection.</returns>
		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x06003EBF RID: 16063 RVA: 0x00104EF5 File Offset: 0x001030F5
		[global::__DynamicallyInvokable]
		public int Count
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this._match._matchcount.Length;
			}
		}

		/// <summary>Enables access to a member of the collection by integer index.</summary>
		/// <param name="groupnum">The zero-based index of the collection member to be retrieved.</param>
		/// <returns>The member of the collection specified by <paramref name="groupnum" />.</returns>
		// Token: 0x17000EC1 RID: 3777
		[global::__DynamicallyInvokable]
		public Group this[int groupnum]
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.GetGroup(groupnum);
			}
		}

		/// <summary>Enables access to a member of the collection by string index.</summary>
		/// <param name="groupname">The name of a capturing group.</param>
		/// <returns>The member of the collection specified by <paramref name="groupname" />.</returns>
		// Token: 0x17000EC2 RID: 3778
		[global::__DynamicallyInvokable]
		public Group this[string groupname]
		{
			[global::__DynamicallyInvokable]
			get
			{
				if (this._match._regex == null)
				{
					return Group._emptygroup;
				}
				return this.GetGroup(this._match._regex.GroupNumberFromName(groupname));
			}
		}

		// Token: 0x06003EC2 RID: 16066 RVA: 0x00104F3C File Offset: 0x0010313C
		internal Group GetGroup(int groupnum)
		{
			if (this._captureMap != null)
			{
				object obj = this._captureMap[groupnum];
				if (obj == null)
				{
					return Group._emptygroup;
				}
				return this.GetGroupImpl((int)obj);
			}
			else
			{
				if (groupnum >= this._match._matchcount.Length || groupnum < 0)
				{
					return Group._emptygroup;
				}
				return this.GetGroupImpl(groupnum);
			}
		}

		// Token: 0x06003EC3 RID: 16067 RVA: 0x00104F9C File Offset: 0x0010319C
		internal Group GetGroupImpl(int groupnum)
		{
			if (groupnum == 0)
			{
				return this._match;
			}
			if (this._groups == null)
			{
				this._groups = new Group[this._match._matchcount.Length - 1];
				for (int i = 0; i < this._groups.Length; i++)
				{
					string text = this._match._regex.GroupNameFromNumber(i + 1);
					this._groups[i] = new Group(this._match._text, this._match._matches[i + 1], this._match._matchcount[i + 1], text);
				}
			}
			return this._groups[groupnum - 1];
		}

		/// <summary>Copies all the elements of the collection to the given array beginning at the given index.</summary>
		/// <param name="array">The array the collection is to be copied into.</param>
		/// <param name="arrayIndex">The position in the destination array where the copying is to begin.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="arrayIndex" /> is outside the bounds of <paramref name="array" />.  
		/// -or-  
		/// <paramref name="arrayIndex" /> plus <see cref="P:System.Text.RegularExpressions.GroupCollection.Count" /> is outside the bounds of <paramref name="array" />.</exception>
		// Token: 0x06003EC4 RID: 16068 RVA: 0x00105040 File Offset: 0x00103240
		public void CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int num = arrayIndex;
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], num);
				num++;
			}
		}

		/// <summary>Provides an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that contains all <see cref="T:System.Text.RegularExpressions.Group" /> objects in the <see cref="T:System.Text.RegularExpressions.GroupCollection" />.</returns>
		// Token: 0x06003EC5 RID: 16069 RVA: 0x00105080 File Offset: 0x00103280
		[global::__DynamicallyInvokable]
		public IEnumerator GetEnumerator()
		{
			return new GroupEnumerator(this);
		}

		// Token: 0x04002DC5 RID: 11717
		internal Match _match;

		// Token: 0x04002DC6 RID: 11718
		internal Hashtable _captureMap;

		// Token: 0x04002DC7 RID: 11719
		internal Group[] _groups;
	}
}
