using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000684 RID: 1668
	internal class StoreAssemblyEnumeration : IEnumerator
	{
		// Token: 0x06004F65 RID: 20325 RVA: 0x0011DC70 File Offset: 0x0011BE70
		public StoreAssemblyEnumeration(IEnumSTORE_ASSEMBLY pI)
		{
			this._enum = pI;
		}

		// Token: 0x06004F66 RID: 20326 RVA: 0x0011DC7F File Offset: 0x0011BE7F
		private STORE_ASSEMBLY GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x06004F67 RID: 20327 RVA: 0x0011DC95 File Offset: 0x0011BE95
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x06004F68 RID: 20328 RVA: 0x0011DCA2 File Offset: 0x0011BEA2
		public STORE_ASSEMBLY Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06004F69 RID: 20329 RVA: 0x0011DCAA File Offset: 0x0011BEAA
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06004F6A RID: 20330 RVA: 0x0011DCB0 File Offset: 0x0011BEB0
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			STORE_ASSEMBLY[] array = new STORE_ASSEMBLY[1];
			uint num = this._enum.Next(1U, array);
			if (num == 1U)
			{
				this._current = array[0];
			}
			return this._fValid = num == 1U;
		}

		// Token: 0x06004F6B RID: 20331 RVA: 0x0011DCF0 File Offset: 0x0011BEF0
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x04002209 RID: 8713
		private IEnumSTORE_ASSEMBLY _enum;

		// Token: 0x0400220A RID: 8714
		private bool _fValid;

		// Token: 0x0400220B RID: 8715
		private STORE_ASSEMBLY _current;
	}
}
