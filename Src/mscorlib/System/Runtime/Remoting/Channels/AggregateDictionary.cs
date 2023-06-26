using System;
using System.Collections;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000851 RID: 2129
	internal class AggregateDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06005A6C RID: 23148 RVA: 0x0013F1BF File Offset: 0x0013D3BF
		public AggregateDictionary(ICollection dictionaries)
		{
			this._dictionaries = dictionaries;
		}

		// Token: 0x17000F12 RID: 3858
		public virtual object this[object key]
		{
			get
			{
				foreach (object obj in this._dictionaries)
				{
					IDictionary dictionary = (IDictionary)obj;
					if (dictionary.Contains(key))
					{
						return dictionary[key];
					}
				}
				return null;
			}
			set
			{
				foreach (object obj in this._dictionaries)
				{
					IDictionary dictionary = (IDictionary)obj;
					if (dictionary.Contains(key))
					{
						dictionary[key] = value;
					}
				}
			}
		}

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x06005A6F RID: 23151 RVA: 0x0013F29C File Offset: 0x0013D49C
		public virtual ICollection Keys
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this._dictionaries)
				{
					IDictionary dictionary = (IDictionary)obj;
					ICollection keys = dictionary.Keys;
					if (keys != null)
					{
						foreach (object obj2 in keys)
						{
							arrayList.Add(obj2);
						}
					}
				}
				return arrayList;
			}
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x06005A70 RID: 23152 RVA: 0x0013F34C File Offset: 0x0013D54C
		public virtual ICollection Values
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this._dictionaries)
				{
					IDictionary dictionary = (IDictionary)obj;
					ICollection values = dictionary.Values;
					if (values != null)
					{
						foreach (object obj2 in values)
						{
							arrayList.Add(obj2);
						}
					}
				}
				return arrayList;
			}
		}

		// Token: 0x06005A71 RID: 23153 RVA: 0x0013F3FC File Offset: 0x0013D5FC
		public virtual bool Contains(object key)
		{
			foreach (object obj in this._dictionaries)
			{
				IDictionary dictionary = (IDictionary)obj;
				if (dictionary.Contains(key))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x06005A72 RID: 23154 RVA: 0x0013F460 File Offset: 0x0013D660
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x06005A73 RID: 23155 RVA: 0x0013F463 File Offset: 0x0013D663
		public virtual bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005A74 RID: 23156 RVA: 0x0013F466 File Offset: 0x0013D666
		public virtual void Add(object key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005A75 RID: 23157 RVA: 0x0013F46D File Offset: 0x0013D66D
		public virtual void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005A76 RID: 23158 RVA: 0x0013F474 File Offset: 0x0013D674
		public virtual void Remove(object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005A77 RID: 23159 RVA: 0x0013F47B File Offset: 0x0013D67B
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new DictionaryEnumeratorByKeys(this);
		}

		// Token: 0x06005A78 RID: 23160 RVA: 0x0013F483 File Offset: 0x0013D683
		public virtual void CopyTo(Array array, int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x06005A79 RID: 23161 RVA: 0x0013F48C File Offset: 0x0013D68C
		public virtual int Count
		{
			get
			{
				int num = 0;
				foreach (object obj in this._dictionaries)
				{
					IDictionary dictionary = (IDictionary)obj;
					num += dictionary.Count;
				}
				return num;
			}
		}

		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x06005A7A RID: 23162 RVA: 0x0013F4EC File Offset: 0x0013D6EC
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x06005A7B RID: 23163 RVA: 0x0013F4EF File Offset: 0x0013D6EF
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005A7C RID: 23164 RVA: 0x0013F4F2 File Offset: 0x0013D6F2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new DictionaryEnumeratorByKeys(this);
		}

		// Token: 0x0400290C RID: 10508
		private ICollection _dictionaries;
	}
}
