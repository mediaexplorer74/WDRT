using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000B9 RID: 185
	internal class FunctionSignature
	{
		// Token: 0x06000484 RID: 1156 RVA: 0x0000EBC2 File Offset: 0x0000CDC2
		internal FunctionSignature(params IEdmTypeReference[] argumentTypes)
		{
			this.argumentTypes = argumentTypes;
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0000EBD1 File Offset: 0x0000CDD1
		internal IEdmTypeReference[] ArgumentTypes
		{
			get
			{
				return this.argumentTypes;
			}
		}

		// Token: 0x04000187 RID: 391
		private readonly IEdmTypeReference[] argumentTypes;
	}
}
