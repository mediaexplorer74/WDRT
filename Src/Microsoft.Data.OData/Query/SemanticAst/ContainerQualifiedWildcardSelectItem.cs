using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000067 RID: 103
	public sealed class ContainerQualifiedWildcardSelectItem : SelectItem
	{
		// Token: 0x06000281 RID: 641 RVA: 0x0000A189 File Offset: 0x00008389
		public ContainerQualifiedWildcardSelectItem(IEdmEntityContainer container)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityContainer>(container, "container");
			this.container = container;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000A1A3 File Offset: 0x000083A3
		public IEdmEntityContainer Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x040000A9 RID: 169
		private readonly IEdmEntityContainer container;
	}
}
