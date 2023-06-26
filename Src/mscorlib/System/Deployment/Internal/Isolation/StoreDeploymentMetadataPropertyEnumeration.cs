using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000682 RID: 1666
	internal class StoreDeploymentMetadataPropertyEnumeration : IEnumerator
	{
		// Token: 0x06004F5A RID: 20314 RVA: 0x0011DBDC File Offset: 0x0011BDDC
		public StoreDeploymentMetadataPropertyEnumeration(IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY pI)
		{
			this._enum = pI;
		}

		// Token: 0x06004F5B RID: 20315 RVA: 0x0011DBEB File Offset: 0x0011BDEB
		private StoreOperationMetadataProperty GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06004F5C RID: 20316 RVA: 0x0011DC01 File Offset: 0x0011BE01
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06004F5D RID: 20317 RVA: 0x0011DC0E File Offset: 0x0011BE0E
		public StoreOperationMetadataProperty Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06004F5E RID: 20318 RVA: 0x0011DC16 File Offset: 0x0011BE16
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06004F5F RID: 20319 RVA: 0x0011DC1C File Offset: 0x0011BE1C
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

		// Token: 0x06004F60 RID: 20320 RVA: 0x0011DC5C File Offset: 0x0011BE5C
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x04002206 RID: 8710
		private IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY _enum;

		// Token: 0x04002207 RID: 8711
		private bool _fValid;

		// Token: 0x04002208 RID: 8712
		private StoreOperationMetadataProperty _current;
	}
}
