using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000BA RID: 186
	internal sealed class FunctionSignatureWithReturnType : FunctionSignature
	{
		// Token: 0x06000486 RID: 1158 RVA: 0x0000EBD9 File Offset: 0x0000CDD9
		internal FunctionSignatureWithReturnType(IEdmTypeReference returnType, params IEdmTypeReference[] argumentTypes)
			: base(argumentTypes)
		{
			this.returnType = returnType;
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0000EBE9 File Offset: 0x0000CDE9
		internal IEdmTypeReference ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x04000188 RID: 392
		private readonly IEdmTypeReference returnType;
	}
}
