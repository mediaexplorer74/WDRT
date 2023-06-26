using System;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020007CE RID: 1998
	internal abstract class RemotingCachedData
	{
		// Token: 0x060056E1 RID: 22241 RVA: 0x00135B08 File Offset: 0x00133D08
		internal SoapAttribute GetSoapAttribute()
		{
			if (this._soapAttr == null)
			{
				lock (this)
				{
					if (this._soapAttr == null)
					{
						this._soapAttr = this.GetSoapAttributeNoLock();
					}
				}
			}
			return this._soapAttr;
		}

		// Token: 0x060056E2 RID: 22242
		internal abstract SoapAttribute GetSoapAttributeNoLock();

		// Token: 0x040027BD RID: 10173
		private SoapAttribute _soapAttr;
	}
}
