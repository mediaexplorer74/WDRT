using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200002B RID: 43
	internal class StoreDeploymentMetadataEnumeration : IEnumerator
	{
		// Token: 0x060000CA RID: 202 RVA: 0x00006A1C File Offset: 0x00004C1C
		[SecuritySafeCritical]
		public StoreDeploymentMetadataEnumeration(IEnumSTORE_DEPLOYMENT_METADATA pI)
		{
			this._enum = pI;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00006A2B File Offset: 0x00004C2B
		private IDefinitionAppId GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00006A41 File Offset: 0x00004C41
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00006A41 File Offset: 0x00004C41
		public IDefinitionAppId Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00006A49 File Offset: 0x00004C49
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00006A4C File Offset: 0x00004C4C
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			IDefinitionAppId[] array = new IDefinitionAppId[1];
			uint num = this._enum.Next(1U, array);
			if (num == 1U)
			{
				this._current = array[0];
			}
			return this._fValid = num == 1U;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00006A88 File Offset: 0x00004C88
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x04000119 RID: 281
		private IEnumSTORE_DEPLOYMENT_METADATA _enum;

		// Token: 0x0400011A RID: 282
		private bool _fValid;

		// Token: 0x0400011B RID: 283
		private IDefinitionAppId _current;
	}
}
