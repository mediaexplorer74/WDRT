using System;
using System.Collections;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000863 RID: 2147
	internal abstract class MessageDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06005B27 RID: 23335 RVA: 0x00140FC7 File Offset: 0x0013F1C7
		internal MessageDictionary(string[] keys, IDictionary idict)
		{
			this._keys = keys;
			this._dict = idict;
		}

		// Token: 0x06005B28 RID: 23336 RVA: 0x00140FDD File Offset: 0x0013F1DD
		internal bool HasUserData()
		{
			return this._dict != null && this._dict.Count > 0;
		}

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06005B29 RID: 23337 RVA: 0x00140FF8 File Offset: 0x0013F1F8
		internal IDictionary InternalDictionary
		{
			get
			{
				return this._dict;
			}
		}

		// Token: 0x06005B2A RID: 23338
		internal abstract object GetMessageValue(int i);

		// Token: 0x06005B2B RID: 23339
		[SecurityCritical]
		internal abstract void SetSpecialKey(int keyNum, object value);

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x06005B2C RID: 23340 RVA: 0x00141000 File Offset: 0x0013F200
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x06005B2D RID: 23341 RVA: 0x00141003 File Offset: 0x0013F203
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x06005B2E RID: 23342 RVA: 0x00141006 File Offset: 0x0013F206
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x06005B2F RID: 23343 RVA: 0x00141009 File Offset: 0x0013F209
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06005B30 RID: 23344 RVA: 0x0014100C File Offset: 0x0013F20C
		public virtual bool Contains(object key)
		{
			return this.ContainsSpecialKey(key) || (this._dict != null && this._dict.Contains(key));
		}

		// Token: 0x06005B31 RID: 23345 RVA: 0x00141030 File Offset: 0x0013F230
		protected virtual bool ContainsSpecialKey(object key)
		{
			if (!(key is string))
			{
				return false;
			}
			string text = (string)key;
			for (int i = 0; i < this._keys.Length; i++)
			{
				if (text.Equals(this._keys[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005B32 RID: 23346 RVA: 0x00141074 File Offset: 0x0013F274
		public virtual void CopyTo(Array array, int index)
		{
			for (int i = 0; i < this._keys.Length; i++)
			{
				array.SetValue(this.GetMessageValue(i), index + i);
			}
			if (this._dict != null)
			{
				this._dict.CopyTo(array, index + this._keys.Length);
			}
		}

		// Token: 0x17000F5C RID: 3932
		public virtual object this[object key]
		{
			get
			{
				string text = key as string;
				if (text != null)
				{
					for (int i = 0; i < this._keys.Length; i++)
					{
						if (text.Equals(this._keys[i]))
						{
							return this.GetMessageValue(i);
						}
					}
					if (this._dict != null)
					{
						return this._dict[key];
					}
				}
				return null;
			}
			[SecuritySafeCritical]
			set
			{
				if (!this.ContainsSpecialKey(key))
				{
					if (this._dict == null)
					{
						this._dict = new Hashtable();
					}
					this._dict[key] = value;
					return;
				}
				if (key.Equals(Message.UriKey))
				{
					this.SetSpecialKey(0, value);
					return;
				}
				if (key.Equals(Message.CallContextKey))
				{
					this.SetSpecialKey(1, value);
					return;
				}
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidKey"));
			}
		}

		// Token: 0x06005B35 RID: 23349 RVA: 0x0014118E File Offset: 0x0013F38E
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new MessageDictionaryEnumerator(this, this._dict);
		}

		// Token: 0x06005B36 RID: 23350 RVA: 0x0014119C File Offset: 0x0013F39C
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005B37 RID: 23351 RVA: 0x001411A3 File Offset: 0x0013F3A3
		public virtual void Add(object key, object value)
		{
			if (this.ContainsSpecialKey(key))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidKey"));
			}
			if (this._dict == null)
			{
				this._dict = new Hashtable();
			}
			this._dict.Add(key, value);
		}

		// Token: 0x06005B38 RID: 23352 RVA: 0x001411DE File Offset: 0x0013F3DE
		public virtual void Clear()
		{
			if (this._dict != null)
			{
				this._dict.Clear();
			}
		}

		// Token: 0x06005B39 RID: 23353 RVA: 0x001411F3 File Offset: 0x0013F3F3
		public virtual void Remove(object key)
		{
			if (this.ContainsSpecialKey(key) || this._dict == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidKey"));
			}
			this._dict.Remove(key);
		}

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x06005B3A RID: 23354 RVA: 0x00141224 File Offset: 0x0013F424
		public virtual ICollection Keys
		{
			get
			{
				int num = this._keys.Length;
				ICollection collection = ((this._dict != null) ? this._dict.Keys : null);
				if (collection != null)
				{
					num += collection.Count;
				}
				ArrayList arrayList = new ArrayList(num);
				for (int i = 0; i < this._keys.Length; i++)
				{
					arrayList.Add(this._keys[i]);
				}
				if (collection != null)
				{
					arrayList.AddRange(collection);
				}
				return arrayList;
			}
		}

		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06005B3B RID: 23355 RVA: 0x00141294 File Offset: 0x0013F494
		public virtual ICollection Values
		{
			get
			{
				int num = this._keys.Length;
				ICollection collection = ((this._dict != null) ? this._dict.Keys : null);
				if (collection != null)
				{
					num += collection.Count;
				}
				ArrayList arrayList = new ArrayList(num);
				for (int i = 0; i < this._keys.Length; i++)
				{
					arrayList.Add(this.GetMessageValue(i));
				}
				if (collection != null)
				{
					arrayList.AddRange(collection);
				}
				return arrayList;
			}
		}

		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06005B3C RID: 23356 RVA: 0x00141300 File Offset: 0x0013F500
		public virtual int Count
		{
			get
			{
				if (this._dict != null)
				{
					return this._dict.Count + this._keys.Length;
				}
				return this._keys.Length;
			}
		}

		// Token: 0x0400294C RID: 10572
		internal string[] _keys;

		// Token: 0x0400294D RID: 10573
		internal IDictionary _dict;
	}
}
