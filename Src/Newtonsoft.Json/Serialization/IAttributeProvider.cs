using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200007D RID: 125
	[NullableContext(1)]
	public interface IAttributeProvider
	{
		// Token: 0x06000678 RID: 1656
		IList<Attribute> GetAttributes(bool inherit);

		// Token: 0x06000679 RID: 1657
		IList<Attribute> GetAttributes(Type attributeType, bool inherit);
	}
}
