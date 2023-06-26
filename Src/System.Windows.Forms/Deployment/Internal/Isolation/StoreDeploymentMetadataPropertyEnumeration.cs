using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200002D RID: 45
	internal class StoreDeploymentMetadataPropertyEnumeration : IEnumerator
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00006A9C File Offset: 0x00004C9C
		[SecuritySafeCritical]
		public StoreDeploymentMetadataPropertyEnumeration(IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY pI)
		{
			this._enum = pI;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00006AAB File Offset: 0x00004CAB
		private StoreOperationMetadataProperty GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00006AC1 File Offset: 0x00004CC1
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00006ACE File Offset: 0x00004CCE
		public StoreOperationMetadataProperty Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00006A49 File Offset: 0x00004C49
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00006AD8 File Offset: 0x00004CD8
		[SecuritySafeCritical]
		public bool MoveNext()
		{
			StoreOperationMetadataProperty[] array = new StoreOperationMetadataProperty[1];
			uint num = this._enum.Next(1U, array);
			if (num == 1U)
			{
				this._current = array[0];
			}
			return this._fValid = num == 1U;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00006B18 File Offset: 0x00004D18
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x0400011C RID: 284
		private IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY _enum;

		// Token: 0x0400011D RID: 285
		private bool _fValid;

		// Token: 0x0400011E RID: 286
		private StoreOperationMetadataProperty _current;
	}
}
