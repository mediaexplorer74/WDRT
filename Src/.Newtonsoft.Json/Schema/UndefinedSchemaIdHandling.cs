using System;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020000B2 RID: 178
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public enum UndefinedSchemaIdHandling
	{
		// Token: 0x04000353 RID: 851
		None,
		// Token: 0x04000354 RID: 852
		UseTypeName,
		// Token: 0x04000355 RID: 853
		UseAssemblyQualifiedName
	}
}
