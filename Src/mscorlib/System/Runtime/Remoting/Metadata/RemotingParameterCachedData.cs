using System;
using System.Reflection;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020007D0 RID: 2000
	internal class RemotingParameterCachedData : RemotingCachedData
	{
		// Token: 0x060056E7 RID: 22247 RVA: 0x00135BD3 File Offset: 0x00133DD3
		internal RemotingParameterCachedData(RuntimeParameterInfo ri)
		{
			this.RI = ri;
		}

		// Token: 0x060056E8 RID: 22248 RVA: 0x00135BE4 File Offset: 0x00133DE4
		internal override SoapAttribute GetSoapAttributeNoLock()
		{
			object[] customAttributes = this.RI.GetCustomAttributes(typeof(SoapParameterAttribute), true);
			SoapAttribute soapAttribute;
			if (customAttributes != null && customAttributes.Length != 0)
			{
				soapAttribute = (SoapParameterAttribute)customAttributes[0];
			}
			else
			{
				soapAttribute = new SoapParameterAttribute();
			}
			soapAttribute.SetReflectInfo(this.RI);
			return soapAttribute;
		}

		// Token: 0x040027BF RID: 10175
		private RuntimeParameterInfo RI;
	}
}
