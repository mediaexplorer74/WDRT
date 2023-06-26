using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000035 RID: 53
	internal class StoreSubcategoryEnumeration : IEnumerator
	{
		// Token: 0x06000101 RID: 257 RVA: 0x00006CDC File Offset: 0x00004EDC
		[SecuritySafeCritical]
		public StoreSubcategoryEnumeration(IEnumSTORE_CATEGORY_SUBCATEGORY pI)
		{
			this._enum = pI;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00006A49 File Offset: 0x00004C49
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00006CEB File Offset: 0x00004EEB
		private STORE_CATEGORY_SUBCATEGORY GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00006D01 File Offset: 0x00004F01
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00006D0E File Offset: 0x00004F0E
		public STORE_CATEGORY_SUBCATEGORY Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00006D18 File Offset: 0x00004F18
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

		// Token: 0x06000107 RID: 263 RVA: 0x00006D58 File Offset: 0x00004F58
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x04000128 RID: 296
		private IEnumSTORE_CATEGORY_SUBCATEGORY _enum;

		// Token: 0x04000129 RID: 297
		private bool _fValid;

		// Token: 0x0400012A RID: 298
		private STORE_CATEGORY_SUBCATEGORY _current;
	}
}
