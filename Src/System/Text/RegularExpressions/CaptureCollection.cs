using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the set of captures made by a single capturing group.</summary>
	// Token: 0x0200068C RID: 1676
	[global::__DynamicallyInvokable]
	[Serializable]
	public class CaptureCollection : ICollection, IEnumerable
	{
		// Token: 0x06003DE1 RID: 15841 RVA: 0x000FD485 File Offset: 0x000FB685
		internal CaptureCollection(Group group)
		{
			this._group = group;
			this._capcount = this._group._capcount;
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x06003DE2 RID: 15842 RVA: 0x000FD4A5 File Offset: 0x000FB6A5
		public object SyncRoot
		{
			get
			{
				return this._group;
			}
		}

		/// <summary>Gets a value that indicates whether access to the collection is synchronized (thread-safe).</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x06003DE3 RID: 15843 RVA: 0x000FD4AD File Offset: 0x000FB6AD
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
		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x06003DE4 RID: 15844 RVA: 0x000FD4B0 File Offset: 0x000FB6B0
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the number of substrings captured by the group.</summary>
		/// <returns>The number of items in the <see cref="T:System.Text.RegularExpressions.CaptureCollection" />.</returns>
		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x06003DE5 RID: 15845 RVA: 0x000FD4B3 File Offset: 0x000FB6B3
		[global::__DynamicallyInvokable]
		public int Count
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this._capcount;
			}
		}

		/// <summary>Gets an individual member of the collection.</summary>
		/// <param name="i">Index into the capture collection.</param>
		/// <returns>The captured substring at position <paramref name="i" /> in the collection.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="i" /> is less than 0 or greater than <see cref="P:System.Text.RegularExpressions.CaptureCollection.Count" />.</exception>
		// Token: 0x17000EAC RID: 3756
		[global::__DynamicallyInvokable]
		public Capture this[int i]
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.GetCapture(i);
			}
		}

		/// <summary>Copies all the elements of the collection to the given array beginning at the given index.</summary>
		/// <param name="array">The array the collection is to be copied into.</param>
		/// <param name="arrayIndex">The position in the destination array where copying is to begin.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is outside the bounds of <paramref name="array" />.  
		/// -or-  
		/// <paramref name="arrayIndex" /> plus <see cref="P:System.Text.RegularExpressions.CaptureCollection.Count" /> is outside the bounds of <paramref name="array" />.</exception>
		// Token: 0x06003DE7 RID: 15847 RVA: 0x000FD4C4 File Offset: 0x000FB6C4
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
		/// <returns>An object that contains all <see cref="T:System.Text.RegularExpressions.Capture" /> objects within the <see cref="T:System.Text.RegularExpressions.CaptureCollection" />.</returns>
		// Token: 0x06003DE8 RID: 15848 RVA: 0x000FD504 File Offset: 0x000FB704
		[global::__DynamicallyInvokable]
		public IEnumerator GetEnumerator()
		{
			return new CaptureEnumerator(this);
		}

		// Token: 0x06003DE9 RID: 15849 RVA: 0x000FD50C File Offset: 0x000FB70C
		internal Capture GetCapture(int i)
		{
			if (i == this._capcount - 1 && i >= 0)
			{
				return this._group;
			}
			if (i >= this._capcount || i < 0)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			if (this._captures == null)
			{
				this._captures = new Capture[this._capcount];
				for (int j = 0; j < this._capcount - 1; j++)
				{
					this._captures[j] = new Capture(this._group._text, this._group._caps[j * 2], this._group._caps[j * 2 + 1]);
				}
			}
			return this._captures[i];
		}

		// Token: 0x04002CE7 RID: 11495
		internal Group _group;

		// Token: 0x04002CE8 RID: 11496
		internal int _capcount;

		// Token: 0x04002CE9 RID: 11497
		internal Capture[] _captures;
	}
}
