using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200068A RID: 1674
	internal class StoreSubcategoryEnumeration : IEnumerator
	{
		// Token: 0x06004F86 RID: 20358 RVA: 0x0011DE2C File Offset: 0x0011C02C
		public StoreSubcategoryEnumeration(IEnumSTORE_CATEGORY_SUBCATEGORY pI)
		{
			this._enum = pI;
		}

		// Token: 0x06004F87 RID: 20359 RVA: 0x0011DE3B File Offset: 0x0011C03B
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06004F88 RID: 20360 RVA: 0x0011DE3E File Offset: 0x0011C03E
		private STORE_CATEGORY_SUBCATEGORY GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06004F89 RID: 20361 RVA: 0x0011DE54 File Offset: 0x0011C054
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x06004F8A RID: 20362 RVA: 0x0011DE61 File Offset: 0x0011C061
		public STORE_CATEGORY_SUBCATEGORY Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06004F8B RID: 20363 RVA: 0x0011DE6C File Offset: 0x0011C06C
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			STORE_CATEGORY_SUBCATEGORY[] array = new STORE_CATEGORY_SUBCATEGORY[1];
			uint num = this._enum.Next(1U, array);
			if (num == 1U)
			{
				this._current = array[0];
			}
			return this._fValid = num == 1U;
		}

		// Token: 0x06004F8C RID: 20364 RVA: 0x0011DEAC File Offset: 0x0011C0AC
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x04002212 RID: 8722
		private IEnumSTORE_CATEGORY_SUBCATEGORY _enum;

		// Token: 0x04002213 RID: 8723
		private bool _fValid;

		// Token: 0x04002214 RID: 8724
		private STORE_CATEGORY_SUBCATEGORY _current;
	}
}
