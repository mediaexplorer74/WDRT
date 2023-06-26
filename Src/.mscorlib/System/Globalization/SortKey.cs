using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Globalization
{
	/// <summary>Represents the result of mapping a string to its sort key.</summary>
	// Token: 0x020003CE RID: 974
	[ComVisible(true)]
	[Serializable]
	public class SortKey
	{
		// Token: 0x06003178 RID: 12664 RVA: 0x000BF21C File Offset: 0x000BD41C
		internal SortKey(string localeName, string str, CompareOptions options, byte[] keyData)
		{
			this.m_KeyData = keyData;
			this.localeName = localeName;
			this.options = options;
			this.m_String = str;
		}

		// Token: 0x06003179 RID: 12665 RVA: 0x000BF241 File Offset: 0x000BD441
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			if (this.win32LCID == 0)
			{
				this.win32LCID = CultureInfo.GetCultureInfo(this.localeName).LCID;
			}
		}

		// Token: 0x0600317A RID: 12666 RVA: 0x000BF261 File Offset: 0x000BD461
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (string.IsNullOrEmpty(this.localeName) && this.win32LCID != 0)
			{
				this.localeName = CultureInfo.GetCultureInfo(this.win32LCID).Name;
			}
		}

		/// <summary>Gets the original string used to create the current <see cref="T:System.Globalization.SortKey" /> object.</summary>
		/// <returns>The original string used to create the current <see cref="T:System.Globalization.SortKey" /> object.</returns>
		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x0600317B RID: 12667 RVA: 0x000BF28E File Offset: 0x000BD48E
		public virtual string OriginalString
		{
			get
			{
				return this.m_String;
			}
		}

		/// <summary>Gets the byte array representing the current <see cref="T:System.Globalization.SortKey" /> object.</summary>
		/// <returns>A byte array representing the current <see cref="T:System.Globalization.SortKey" /> object.</returns>
		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x0600317C RID: 12668 RVA: 0x000BF296 File Offset: 0x000BD496
		public virtual byte[] KeyData
		{
			get
			{
				return (byte[])this.m_KeyData.Clone();
			}
		}

		/// <summary>Compares two sort keys.</summary>
		/// <param name="sortkey1">The first sort key to compare.</param>
		/// <param name="sortkey2">The second sort key to compare.</param>
		/// <returns>A signed integer that indicates the relationship between <paramref name="sortkey1" /> and <paramref name="sortkey2" />.  
		///   Value  
		///
		///   Condition  
		///
		///   Less than zero  
		///
		///  <paramref name="sortkey1" /> is less than <paramref name="sortkey2" />.  
		///
		///   Zero  
		///
		///  <paramref name="sortkey1" /> is equal to <paramref name="sortkey2" />.  
		///
		///   Greater than zero  
		///
		///  <paramref name="sortkey1" /> is greater than <paramref name="sortkey2" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sortkey1" /> or <paramref name="sortkey2" /> is <see langword="null" />.</exception>
		// Token: 0x0600317D RID: 12669 RVA: 0x000BF2A8 File Offset: 0x000BD4A8
		public static int Compare(SortKey sortkey1, SortKey sortkey2)
		{
			if (sortkey1 == null || sortkey2 == null)
			{
				throw new ArgumentNullException((sortkey1 == null) ? "sortkey1" : "sortkey2");
			}
			byte[] keyData = sortkey1.m_KeyData;
			byte[] keyData2 = sortkey2.m_KeyData;
			if (keyData.Length == 0)
			{
				if (keyData2.Length == 0)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (keyData2.Length == 0)
				{
					return 1;
				}
				int num = ((keyData.Length < keyData2.Length) ? keyData.Length : keyData2.Length);
				for (int i = 0; i < num; i++)
				{
					if (keyData[i] > keyData2[i])
					{
						return 1;
					}
					if (keyData[i] < keyData2[i])
					{
						return -1;
					}
				}
				return 0;
			}
		}

		/// <summary>Determines whether the specified object is equal to the current <see cref="T:System.Globalization.SortKey" /> object.</summary>
		/// <param name="value">The object to compare with the current <see cref="T:System.Globalization.SortKey" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="value" /> parameter is equal to the current <see cref="T:System.Globalization.SortKey" /> object; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x0600317E RID: 12670 RVA: 0x000BF324 File Offset: 0x000BD524
		public override bool Equals(object value)
		{
			SortKey sortKey = value as SortKey;
			return sortKey != null && SortKey.Compare(this, sortKey) == 0;
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Globalization.SortKey" /> object that is suitable for hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Globalization.SortKey" /> object.</returns>
		// Token: 0x0600317F RID: 12671 RVA: 0x000BF347 File Offset: 0x000BD547
		public override int GetHashCode()
		{
			return CompareInfo.GetCompareInfo(this.localeName).GetHashCodeOfString(this.m_String, this.options);
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Globalization.SortKey" /> object.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Globalization.SortKey" /> object.</returns>
		// Token: 0x06003180 RID: 12672 RVA: 0x000BF368 File Offset: 0x000BD568
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"SortKey - ",
				this.localeName,
				", ",
				this.options.ToString(),
				", ",
				this.m_String
			});
		}

		// Token: 0x04001521 RID: 5409
		[OptionalField(VersionAdded = 3)]
		internal string localeName;

		// Token: 0x04001522 RID: 5410
		[OptionalField(VersionAdded = 1)]
		internal int win32LCID;

		// Token: 0x04001523 RID: 5411
		internal CompareOptions options;

		// Token: 0x04001524 RID: 5412
		internal string m_String;

		// Token: 0x04001525 RID: 5413
		internal byte[] m_KeyData;
	}
}
