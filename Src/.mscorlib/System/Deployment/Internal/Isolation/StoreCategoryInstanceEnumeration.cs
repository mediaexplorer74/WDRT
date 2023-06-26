using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200068C RID: 1676
	internal class StoreCategoryInstanceEnumeration : IEnumerator
	{
		// Token: 0x06004F91 RID: 20369 RVA: 0x0011DEC0 File Offset: 0x0011C0C0
		public StoreCategoryInstanceEnumeration(IEnumSTORE_CATEGORY_INSTANCE pI)
		{
			this._enum = pI;
		}

		// Token: 0x06004F92 RID: 20370 RVA: 0x0011DECF File Offset: 0x0011C0CF
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06004F93 RID: 20371 RVA: 0x0011DED2 File Offset: 0x0011C0D2
		private STORE_CATEGORY_INSTANCE GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x06004F94 RID: 20372 RVA: 0x0011DEE8 File Offset: 0x0011C0E8
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06004F95 RID: 20373 RVA: 0x0011DEF5 File Offset: 0x0011C0F5
		public STORE_CATEGORY_INSTANCE Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06004F96 RID: 20374 RVA: 0x0011DF00 File Offset: 0x0011C100
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			STORE_CATEGORY_INSTANCE[] array = new STORE_CATEGORY_INSTANCE[1];
			uint num = this._enum.Next(1U, array);
			if (num == 1U)
			{
				this._current = array[0];
			}
			return this._fValid = num == 1U;
		}

		// Token: 0x06004F97 RID: 20375 RVA: 0x0011DF40 File Offset: 0x0011C140
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x04002215 RID: 8725
		private IEnumSTORE_CATEGORY_INSTANCE _enum;

		// Token: 0x04002216 RID: 8726
		private bool _fValid;

		// Token: 0x04002217 RID: 8727
		private STORE_CATEGORY_INSTANCE _current;
	}
}
