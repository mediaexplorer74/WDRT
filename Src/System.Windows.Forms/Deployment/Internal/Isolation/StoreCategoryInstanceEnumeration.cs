using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000037 RID: 55
	internal class StoreCategoryInstanceEnumeration : IEnumerator
	{
		// Token: 0x0600010C RID: 268 RVA: 0x00006D6C File Offset: 0x00004F6C
		[SecuritySafeCritical]
		public StoreCategoryInstanceEnumeration(IEnumSTORE_CATEGORY_INSTANCE pI)
		{
			this._enum = pI;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006A49 File Offset: 0x00004C49
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00006D7B File Offset: 0x00004F7B
		private STORE_CATEGORY_INSTANCE GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00006D91 File Offset: 0x00004F91
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00006D9E File Offset: 0x00004F9E
		public STORE_CATEGORY_INSTANCE Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00006DA8 File Offset: 0x00004FA8
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

		// Token: 0x06000112 RID: 274 RVA: 0x00006DE8 File Offset: 0x00004FE8
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x0400012B RID: 299
		private IEnumSTORE_CATEGORY_INSTANCE _enum;

		// Token: 0x0400012C RID: 300
		private bool _fValid;

		// Token: 0x0400012D RID: 301
		private STORE_CATEGORY_INSTANCE _current;
	}
}
