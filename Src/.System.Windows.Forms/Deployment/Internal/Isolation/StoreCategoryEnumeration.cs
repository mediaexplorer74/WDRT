using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000033 RID: 51
	internal class StoreCategoryEnumeration : IEnumerator
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x00006C4C File Offset: 0x00004E4C
		[SecuritySafeCritical]
		public StoreCategoryEnumeration(IEnumSTORE_CATEGORY pI)
		{
			this._enum = pI;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006A49 File Offset: 0x00004C49
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00006C5B File Offset: 0x00004E5B
		private STORE_CATEGORY GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00006C71 File Offset: 0x00004E71
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00006C7E File Offset: 0x00004E7E
		public STORE_CATEGORY Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00006C88 File Offset: 0x00004E88
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			STORE_CATEGORY[] array = new STORE_CATEGORY[1];
			uint num = this._enum.Next(1U, array);
			if (num == 1U)
			{
				this._current = array[0];
			}
			return this._fValid = num == 1U;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00006CC8 File Offset: 0x00004EC8
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x04000125 RID: 293
		private IEnumSTORE_CATEGORY _enum;

		// Token: 0x04000126 RID: 294
		private bool _fValid;

		// Token: 0x04000127 RID: 295
		private STORE_CATEGORY _current;
	}
}
