using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000031 RID: 49
	internal class StoreAssemblyFileEnumeration : IEnumerator
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00006BBC File Offset: 0x00004DBC
		[SecuritySafeCritical]
		public StoreAssemblyFileEnumeration(IEnumSTORE_ASSEMBLY_FILE pI)
		{
			this._enum = pI;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00006A49 File Offset: 0x00004C49
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00006BCB File Offset: 0x00004DCB
		private STORE_ASSEMBLY_FILE GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00006BE1 File Offset: 0x00004DE1
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00006BEE File Offset: 0x00004DEE
		public STORE_ASSEMBLY_FILE Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00006BF8 File Offset: 0x00004DF8
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			STORE_ASSEMBLY_FILE[] array = new STORE_ASSEMBLY_FILE[1];
			uint num = this._enum.Next(1U, array);
			if (num == 1U)
			{
				this._current = array[0];
			}
			return this._fValid = num == 1U;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00006C38 File Offset: 0x00004E38
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x04000122 RID: 290
		private IEnumSTORE_ASSEMBLY_FILE _enum;

		// Token: 0x04000123 RID: 291
		private bool _fValid;

		// Token: 0x04000124 RID: 292
		private STORE_ASSEMBLY_FILE _current;
	}
}
