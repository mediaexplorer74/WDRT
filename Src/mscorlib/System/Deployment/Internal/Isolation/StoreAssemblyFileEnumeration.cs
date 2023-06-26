using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000686 RID: 1670
	internal class StoreAssemblyFileEnumeration : IEnumerator
	{
		// Token: 0x06004F70 RID: 20336 RVA: 0x0011DD04 File Offset: 0x0011BF04
		public StoreAssemblyFileEnumeration(IEnumSTORE_ASSEMBLY_FILE pI)
		{
			this._enum = pI;
		}

		// Token: 0x06004F71 RID: 20337 RVA: 0x0011DD13 File Offset: 0x0011BF13
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06004F72 RID: 20338 RVA: 0x0011DD16 File Offset: 0x0011BF16
		private STORE_ASSEMBLY_FILE GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x06004F73 RID: 20339 RVA: 0x0011DD2C File Offset: 0x0011BF2C
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x06004F74 RID: 20340 RVA: 0x0011DD39 File Offset: 0x0011BF39
		public STORE_ASSEMBLY_FILE Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06004F75 RID: 20341 RVA: 0x0011DD44 File Offset: 0x0011BF44
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

		// Token: 0x06004F76 RID: 20342 RVA: 0x0011DD84 File Offset: 0x0011BF84
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x0400220C RID: 8716
		private IEnumSTORE_ASSEMBLY_FILE _enum;

		// Token: 0x0400220D RID: 8717
		private bool _fValid;

		// Token: 0x0400220E RID: 8718
		private STORE_ASSEMBLY_FILE _current;
	}
}
