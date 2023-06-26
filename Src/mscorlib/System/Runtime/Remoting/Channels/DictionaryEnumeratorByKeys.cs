using System;
using System.Collections;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000850 RID: 2128
	internal class DictionaryEnumeratorByKeys : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x06005A65 RID: 23141 RVA: 0x0013F145 File Offset: 0x0013D345
		public DictionaryEnumeratorByKeys(IDictionary properties)
		{
			this._properties = properties;
			this._keyEnum = properties.Keys.GetEnumerator();
		}

		// Token: 0x06005A66 RID: 23142 RVA: 0x0013F165 File Offset: 0x0013D365
		public bool MoveNext()
		{
			return this._keyEnum.MoveNext();
		}

		// Token: 0x06005A67 RID: 23143 RVA: 0x0013F172 File Offset: 0x0013D372
		public void Reset()
		{
			this._keyEnum.Reset();
		}

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x06005A68 RID: 23144 RVA: 0x0013F17F File Offset: 0x0013D37F
		public object Current
		{
			get
			{
				return this.Entry;
			}
		}

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x06005A69 RID: 23145 RVA: 0x0013F18C File Offset: 0x0013D38C
		public DictionaryEntry Entry
		{
			get
			{
				return new DictionaryEntry(this.Key, this.Value);
			}
		}

		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x06005A6A RID: 23146 RVA: 0x0013F19F File Offset: 0x0013D39F
		public object Key
		{
			get
			{
				return this._keyEnum.Current;
			}
		}

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x06005A6B RID: 23147 RVA: 0x0013F1AC File Offset: 0x0013D3AC
		public object Value
		{
			get
			{
				return this._properties[this.Key];
			}
		}

		// Token: 0x0400290A RID: 10506
		private IDictionary _properties;

		// Token: 0x0400290B RID: 10507
		private IEnumerator _keyEnum;
	}
}
