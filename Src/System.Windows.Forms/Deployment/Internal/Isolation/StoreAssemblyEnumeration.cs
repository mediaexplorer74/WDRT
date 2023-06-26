using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200002F RID: 47
	internal class StoreAssemblyEnumeration : IEnumerator
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00006B2C File Offset: 0x00004D2C
		[SecuritySafeCritical]
		public StoreAssemblyEnumeration(IEnumSTORE_ASSEMBLY pI)
		{
			this._enum = pI;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00006B3B File Offset: 0x00004D3B
		private STORE_ASSEMBLY GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00006B51 File Offset: 0x00004D51
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00006B5E File Offset: 0x00004D5E
		public STORE_ASSEMBLY Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00006A49 File Offset: 0x00004C49
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00006B68 File Offset: 0x00004D68
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

		// Token: 0x060000E6 RID: 230 RVA: 0x00006BA8 File Offset: 0x00004DA8
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x0400011F RID: 287
		private IEnumSTORE_ASSEMBLY _enum;

		// Token: 0x04000120 RID: 288
		private bool _fValid;

		// Token: 0x04000121 RID: 289
		private STORE_ASSEMBLY _current;
	}
}
