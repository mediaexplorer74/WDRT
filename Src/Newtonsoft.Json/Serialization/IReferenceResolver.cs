using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200007F RID: 127
	[NullableContext(1)]
	public interface IReferenceResolver
	{
		// Token: 0x0600067B RID: 1659
		object ResolveReference(object context, string reference);

		// Token: 0x0600067C RID: 1660
		string GetReference(object context, object value);

		// Token: 0x0600067D RID: 1661
		bool IsReferenced(object context, object value);

		// Token: 0x0600067E RID: 1662
		void AddReference(object context, string reference, object value);
	}
}
