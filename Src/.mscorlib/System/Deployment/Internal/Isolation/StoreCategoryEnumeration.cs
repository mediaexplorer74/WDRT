using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000688 RID: 1672
	internal class StoreCategoryEnumeration : IEnumerator
	{
		// Token: 0x06004F7B RID: 20347 RVA: 0x0011DD98 File Offset: 0x0011BF98
		public StoreCategoryEnumeration(IEnumSTORE_CATEGORY pI)
		{
			this._enum = pI;
		}

		// Token: 0x06004F7C RID: 20348 RVA: 0x0011DDA7 File Offset: 0x0011BFA7
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06004F7D RID: 20349 RVA: 0x0011DDAA File Offset: 0x0011BFAA
		private STORE_CATEGORY GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x06004F7E RID: 20350 RVA: 0x0011DDC0 File Offset: 0x0011BFC0
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06004F7F RID: 20351 RVA: 0x0011DDCD File Offset: 0x0011BFCD
		public STORE_CATEGORY Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06004F80 RID: 20352 RVA: 0x0011DDD8 File Offset: 0x0011BFD8
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

		// Token: 0x06004F81 RID: 20353 RVA: 0x0011DE18 File Offset: 0x0011C018
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x0400220F RID: 8719
		private IEnumSTORE_CATEGORY _enum;

		// Token: 0x04002210 RID: 8720
		private bool _fValid;

		// Token: 0x04002211 RID: 8721
		private STORE_CATEGORY _current;
	}
}
