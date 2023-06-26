using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020007CF RID: 1999
	internal class RemotingFieldCachedData : RemotingCachedData
	{
		// Token: 0x060056E4 RID: 22244 RVA: 0x00135B68 File Offset: 0x00133D68
		internal RemotingFieldCachedData(RuntimeFieldInfo ri)
		{
			this.RI = ri;
		}

		// Token: 0x060056E5 RID: 22245 RVA: 0x00135B77 File Offset: 0x00133D77
		internal RemotingFieldCachedData(SerializationFieldInfo ri)
		{
			this.RI = ri;
		}

		// Token: 0x060056E6 RID: 22246 RVA: 0x00135B88 File Offset: 0x00133D88
		internal override SoapAttribute GetSoapAttributeNoLock()
		{
			object[] customAttributes = this.RI.GetCustomAttributes(typeof(SoapFieldAttribute), false);
			SoapAttribute soapAttribute;
			if (customAttributes != null && customAttributes.Length != 0)
			{
				soapAttribute = (SoapAttribute)customAttributes[0];
			}
			else
			{
				soapAttribute = new SoapFieldAttribute();
			}
			soapAttribute.SetReflectInfo(this.RI);
			return soapAttribute;
		}

		// Token: 0x040027BE RID: 10174
		private FieldInfo RI;
	}
}
