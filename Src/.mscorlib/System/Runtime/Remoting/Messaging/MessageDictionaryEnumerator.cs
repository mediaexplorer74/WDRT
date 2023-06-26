using System;
using System.Collections;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000864 RID: 2148
	internal class MessageDictionaryEnumerator : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x06005B3D RID: 23357 RVA: 0x00141327 File Offset: 0x0013F527
		public MessageDictionaryEnumerator(MessageDictionary md, IDictionary hashtable)
		{
			this._md = md;
			if (hashtable != null)
			{
				this._enumHash = hashtable.GetEnumerator();
				return;
			}
			this._enumHash = null;
		}

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x06005B3E RID: 23358 RVA: 0x00141354 File Offset: 0x0013F554
		public object Key
		{
			get
			{
				if (this.i < 0)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
				}
				if (this.i < this._md._keys.Length)
				{
					return this._md._keys[this.i];
				}
				return this._enumHash.Key;
			}
		}

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x06005B3F RID: 23359 RVA: 0x001413B0 File Offset: 0x0013F5B0
		public object Value
		{
			get
			{
				if (this.i < 0)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
				}
				if (this.i < this._md._keys.Length)
				{
					return this._md.GetMessageValue(this.i);
				}
				return this._enumHash.Value;
			}
		}

		// Token: 0x06005B40 RID: 23360 RVA: 0x00141408 File Offset: 0x0013F608
		public bool MoveNext()
		{
			if (this.i == -2)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
			this.i++;
			if (this.i < this._md._keys.Length)
			{
				return true;
			}
			if (this._enumHash != null && this._enumHash.MoveNext())
			{
				return true;
			}
			this.i = -2;
			return false;
		}

		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x06005B41 RID: 23361 RVA: 0x00141474 File Offset: 0x0013F674
		public object Current
		{
			get
			{
				return this.Entry;
			}
		}

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x06005B42 RID: 23362 RVA: 0x00141481 File Offset: 0x0013F681
		public DictionaryEntry Entry
		{
			get
			{
				return new DictionaryEntry(this.Key, this.Value);
			}
		}

		// Token: 0x06005B43 RID: 23363 RVA: 0x00141494 File Offset: 0x0013F694
		public void Reset()
		{
			this.i = -1;
			if (this._enumHash != null)
			{
				this._enumHash.Reset();
			}
		}

		// Token: 0x0400294E RID: 10574
		private int i = -1;

		// Token: 0x0400294F RID: 10575
		private IDictionaryEnumerator _enumHash;

		// Token: 0x04002950 RID: 10576
		private MessageDictionary _md;
	}
}
