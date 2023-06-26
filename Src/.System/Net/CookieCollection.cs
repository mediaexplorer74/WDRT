using System;
using System.Collections;
using System.Runtime.Serialization;

namespace System.Net
{
	/// <summary>Provides a collection container for instances of the <see cref="T:System.Net.Cookie" /> class.</summary>
	// Token: 0x020000D6 RID: 214
	[global::__DynamicallyInvokable]
	[Serializable]
	public class CookieCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.CookieCollection" /> class.</summary>
		// Token: 0x06000734 RID: 1844 RVA: 0x00027E98 File Offset: 0x00026098
		[global::__DynamicallyInvokable]
		public CookieCollection()
		{
			this.m_IsReadOnly = true;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00027EBD File Offset: 0x000260BD
		internal CookieCollection(bool IsReadOnly)
		{
			this.m_IsReadOnly = IsReadOnly;
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Net.CookieCollection" /> is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if this is a read-only <see cref="T:System.Net.CookieCollection" />; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x00027EE2 File Offset: 0x000260E2
		public bool IsReadOnly
		{
			get
			{
				return this.m_IsReadOnly;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.Cookie" /> with a specific index from a <see cref="T:System.Net.CookieCollection" />.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Net.Cookie" /> to be found.</param>
		/// <returns>A <see cref="T:System.Net.Cookie" /> with a specific index from a <see cref="T:System.Net.CookieCollection" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or <paramref name="index" /> is greater than or equal to <see cref="P:System.Net.CookieCollection.Count" />.</exception>
		// Token: 0x17000151 RID: 337
		public Cookie this[int index]
		{
			get
			{
				if (index < 0 || index >= this.m_list.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return (Cookie)this.m_list[index];
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.Cookie" /> with a specific name from a <see cref="T:System.Net.CookieCollection" />.</summary>
		/// <param name="name">The name of the <see cref="T:System.Net.Cookie" /> to be found.</param>
		/// <returns>The <see cref="T:System.Net.Cookie" /> with a specific name from a <see cref="T:System.Net.CookieCollection" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x17000152 RID: 338
		[global::__DynamicallyInvokable]
		public Cookie this[string name]
		{
			[global::__DynamicallyInvokable]
			get
			{
				foreach (object obj in this.m_list)
				{
					Cookie cookie = (Cookie)obj;
					if (string.Compare(cookie.Name, name, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return cookie;
					}
				}
				return null;
			}
		}

		/// <summary>Adds a <see cref="T:System.Net.Cookie" /> to a <see cref="T:System.Net.CookieCollection" />.</summary>
		/// <param name="cookie">The <see cref="T:System.Net.Cookie" /> to be added to a <see cref="T:System.Net.CookieCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cookie" /> is <see langword="null" />.</exception>
		// Token: 0x06000739 RID: 1849 RVA: 0x00027F84 File Offset: 0x00026184
		[global::__DynamicallyInvokable]
		public void Add(Cookie cookie)
		{
			if (cookie == null)
			{
				throw new ArgumentNullException("cookie");
			}
			this.m_version++;
			int num = this.IndexOf(cookie);
			if (num == -1)
			{
				this.m_list.Add(cookie);
				return;
			}
			this.m_list[num] = cookie;
		}

		/// <summary>Adds the contents of a <see cref="T:System.Net.CookieCollection" /> to the current instance.</summary>
		/// <param name="cookies">The <see cref="T:System.Net.CookieCollection" /> to be added.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cookies" /> is <see langword="null" />.</exception>
		// Token: 0x0600073A RID: 1850 RVA: 0x00027FD4 File Offset: 0x000261D4
		[global::__DynamicallyInvokable]
		public void Add(CookieCollection cookies)
		{
			if (cookies == null)
			{
				throw new ArgumentNullException("cookies");
			}
			foreach (object obj in cookies)
			{
				Cookie cookie = (Cookie)obj;
				this.Add(cookie);
			}
		}

		/// <summary>Gets the number of cookies contained in a <see cref="T:System.Net.CookieCollection" />.</summary>
		/// <returns>The number of cookies contained in a <see cref="T:System.Net.CookieCollection" />.</returns>
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x00028038 File Offset: 0x00026238
		[global::__DynamicallyInvokable]
		public int Count
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.m_list.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to a <see cref="T:System.Net.CookieCollection" /> is thread safe.</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Net.CookieCollection" /> is thread safe; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x00028045 File Offset: 0x00026245
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object to synchronize access to the <see cref="T:System.Net.CookieCollection" />.</summary>
		/// <returns>An object to synchronize access to the <see cref="T:System.Net.CookieCollection" />.</returns>
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x00028048 File Offset: 0x00026248
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies the elements of a <see cref="T:System.Net.CookieCollection" /> to an instance of the <see cref="T:System.Array" /> class, starting at a particular index.</summary>
		/// <param name="array">The target <see cref="T:System.Array" /> to which the <see cref="T:System.Net.CookieCollection" /> will be copied.</param>
		/// <param name="index">The zero-based index in the target <see cref="T:System.Array" /> where copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in this <see cref="T:System.Net.CookieCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The elements in this <see cref="T:System.Net.CookieCollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x0600073E RID: 1854 RVA: 0x0002804B File Offset: 0x0002624B
		public void CopyTo(Array array, int index)
		{
			this.m_list.CopyTo(array, index);
		}

		/// <summary>Copies the elements of this <see cref="T:System.Net.CookieCollection" /> to a <see cref="T:System.Net.Cookie" /> array starting at the specified index of the target array.</summary>
		/// <param name="array">The target <see cref="T:System.Net.Cookie" /> array to which the <see cref="T:System.Net.CookieCollection" /> will be copied.</param>
		/// <param name="index">The zero-based index in the target <see cref="T:System.Array" /> where copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in this <see cref="T:System.Net.CookieCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The elements in this <see cref="T:System.Net.CookieCollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x0600073F RID: 1855 RVA: 0x0002805A File Offset: 0x0002625A
		public void CopyTo(Cookie[] array, int index)
		{
			this.m_list.CopyTo(array, index);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0002806C File Offset: 0x0002626C
		internal DateTime TimeStamp(CookieCollection.Stamp how)
		{
			switch (how)
			{
			case CookieCollection.Stamp.Set:
				this.m_TimeStamp = DateTime.Now;
				break;
			case CookieCollection.Stamp.SetToUnused:
				this.m_TimeStamp = DateTime.MinValue;
				break;
			case CookieCollection.Stamp.SetToMaxUsed:
				this.m_TimeStamp = DateTime.MaxValue;
				break;
			}
			return this.m_TimeStamp;
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x000280BC File Offset: 0x000262BC
		internal bool IsOtherVersionSeen
		{
			get
			{
				return this.m_has_other_versions;
			}
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x000280C4 File Offset: 0x000262C4
		internal int InternalAdd(Cookie cookie, bool isStrict)
		{
			int num = 1;
			if (isStrict)
			{
				IComparer comparer = Cookie.GetComparer();
				int num2 = 0;
				foreach (object obj in this.m_list)
				{
					Cookie cookie2 = (Cookie)obj;
					if (comparer.Compare(cookie, cookie2) == 0)
					{
						num = 0;
						if (cookie2.Variant <= cookie.Variant)
						{
							this.m_list[num2] = cookie;
							break;
						}
						break;
					}
					else
					{
						num2++;
					}
				}
				if (num2 == this.m_list.Count)
				{
					this.m_list.Add(cookie);
				}
			}
			else
			{
				this.m_list.Add(cookie);
			}
			if (cookie.Version != 1)
			{
				this.m_has_other_versions = true;
			}
			return num;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00028194 File Offset: 0x00026394
		internal int IndexOf(Cookie cookie)
		{
			IComparer comparer = Cookie.GetComparer();
			int num = 0;
			foreach (object obj in this.m_list)
			{
				Cookie cookie2 = (Cookie)obj;
				if (comparer.Compare(cookie, cookie2) == 0)
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00028208 File Offset: 0x00026408
		internal void RemoveAt(int idx)
		{
			this.m_list.RemoveAt(idx);
		}

		/// <summary>Gets an enumerator that can iterate through a <see cref="T:System.Net.CookieCollection" />.</summary>
		/// <returns>An instance of an implementation of an <see cref="T:System.Collections.IEnumerator" /> interface that can iterate through a <see cref="T:System.Net.CookieCollection" />.</returns>
		// Token: 0x06000745 RID: 1861 RVA: 0x00028216 File Offset: 0x00026416
		[global::__DynamicallyInvokable]
		public IEnumerator GetEnumerator()
		{
			return new CookieCollection.CookieCollectionEnumerator(this);
		}

		// Token: 0x04000D02 RID: 3330
		internal int m_version;

		// Token: 0x04000D03 RID: 3331
		private ArrayList m_list = new ArrayList();

		// Token: 0x04000D04 RID: 3332
		private DateTime m_TimeStamp = DateTime.MinValue;

		// Token: 0x04000D05 RID: 3333
		private bool m_has_other_versions;

		// Token: 0x04000D06 RID: 3334
		[OptionalField]
		private bool m_IsReadOnly;

		// Token: 0x020006F2 RID: 1778
		internal enum Stamp
		{
			// Token: 0x04003079 RID: 12409
			Check,
			// Token: 0x0400307A RID: 12410
			Set,
			// Token: 0x0400307B RID: 12411
			SetToUnused,
			// Token: 0x0400307C RID: 12412
			SetToMaxUsed
		}

		// Token: 0x020006F3 RID: 1779
		private class CookieCollectionEnumerator : IEnumerator
		{
			// Token: 0x06004053 RID: 16467 RVA: 0x0010DAD5 File Offset: 0x0010BCD5
			internal CookieCollectionEnumerator(CookieCollection cookies)
			{
				this.m_cookies = cookies;
				this.m_count = cookies.Count;
				this.m_version = cookies.m_version;
			}

			// Token: 0x17000EE0 RID: 3808
			// (get) Token: 0x06004054 RID: 16468 RVA: 0x0010DB04 File Offset: 0x0010BD04
			object IEnumerator.Current
			{
				get
				{
					if (this.m_index < 0 || this.m_index >= this.m_count)
					{
						throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumOpCantHappen"));
					}
					if (this.m_version != this.m_cookies.m_version)
					{
						throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumFailedVersion"));
					}
					return this.m_cookies[this.m_index];
				}
			}

			// Token: 0x06004055 RID: 16469 RVA: 0x0010DB6C File Offset: 0x0010BD6C
			bool IEnumerator.MoveNext()
			{
				if (this.m_version != this.m_cookies.m_version)
				{
					throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumFailedVersion"));
				}
				int num = this.m_index + 1;
				this.m_index = num;
				if (num < this.m_count)
				{
					return true;
				}
				this.m_index = this.m_count;
				return false;
			}

			// Token: 0x06004056 RID: 16470 RVA: 0x0010DBC4 File Offset: 0x0010BDC4
			void IEnumerator.Reset()
			{
				this.m_index = -1;
			}

			// Token: 0x0400307D RID: 12413
			private CookieCollection m_cookies;

			// Token: 0x0400307E RID: 12414
			private int m_count;

			// Token: 0x0400307F RID: 12415
			private int m_index = -1;

			// Token: 0x04003080 RID: 12416
			private int m_version;
		}
	}
}
