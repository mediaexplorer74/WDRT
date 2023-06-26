using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000776 RID: 1910
	[Serializable]
	internal enum SoapAttributeType
	{
		// Token: 0x0400257D RID: 9597
		None,
		// Token: 0x0400257E RID: 9598
		SchemaType,
		// Token: 0x0400257F RID: 9599
		Embedded,
		// Token: 0x04002580 RID: 9600
		XmlElement = 4,
		// Token: 0x04002581 RID: 9601
		XmlAttribute = 8
	}
}
