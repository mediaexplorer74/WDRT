using System;
using System.Collections;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000680 RID: 1664
	internal class StoreDeploymentMetadataEnumeration : IEnumerator
	{
		// Token: 0x06004F4F RID: 20303 RVA: 0x0011DB51 File Offset: 0x0011BD51
		public StoreDeploymentMetadataEnumeration(IEnumSTORE_DEPLOYMENT_METADATA pI)
		{
			this._enum = pI;
		}

		// Token: 0x06004F50 RID: 20304 RVA: 0x0011DB60 File Offset: 0x0011BD60
		private IDefinitionAppId GetCurrent()
		{
			if (!this._fValid)
			{
				throw new InvalidOperationException();
			}
			return this._current;
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06004F51 RID: 20305 RVA: 0x0011DB76 File Offset: 0x0011BD76
		object IEnumerator.Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06004F52 RID: 20306 RVA: 0x0011DB7E File Offset: 0x0011BD7E
		public IDefinitionAppId Current
		{
			get
			{
				return this.GetCurrent();
			}
		}

		// Token: 0x06004F53 RID: 20307 RVA: 0x0011DB86 File Offset: 0x0011BD86
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x06004F54 RID: 20308 RVA: 0x0011DB8C File Offset: 0x0011BD8C
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

		// Token: 0x06004F55 RID: 20309 RVA: 0x0011DBC8 File Offset: 0x0011BDC8
		[SecuritySafeCritical]
		public void Reset()
		{
			this._fValid = false;
			this._enum.Reset();
		}

		// Token: 0x04002203 RID: 8707
		private IEnumSTORE_DEPLOYMENT_METADATA _enum;

		// Token: 0x04002204 RID: 8708
		private bool _fValid;

		// Token: 0x04002205 RID: 8709
		private IDefinitionAppId _current;
	}
}
