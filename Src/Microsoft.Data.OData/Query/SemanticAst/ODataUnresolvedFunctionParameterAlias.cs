using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x0200008B RID: 139
	public class ODataUnresolvedFunctionParameterAlias : ODataValue
	{
		// Token: 0x06000345 RID: 837 RVA: 0x0000B689 File Offset: 0x00009889
		public ODataUnresolvedFunctionParameterAlias(string alias, IEdmTypeReference type)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(alias, "alias");
			this.Alias = alias;
			this.Type = type;
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000B6AA File Offset: 0x000098AA
		// (set) Token: 0x06000347 RID: 839 RVA: 0x0000B6B2 File Offset: 0x000098B2
		public IEdmTypeReference Type { get; private set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000B6BB File Offset: 0x000098BB
		// (set) Token: 0x06000349 RID: 841 RVA: 0x0000B6C3 File Offset: 0x000098C3
		public string Alias { get; private set; }
	}
}
