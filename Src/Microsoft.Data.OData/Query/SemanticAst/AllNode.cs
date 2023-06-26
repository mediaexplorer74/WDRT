using System;
using System.Collections.ObjectModel;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x0200009B RID: 155
	public sealed class AllNode : LambdaNode
	{
		// Token: 0x060003AB RID: 939 RVA: 0x0000BD42 File Offset: 0x00009F42
		public AllNode(Collection<RangeVariable> rangeVariables)
			: this(rangeVariables, null)
		{
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000BD4C File Offset: 0x00009F4C
		public AllNode(Collection<RangeVariable> rangeVariables, RangeVariable currentRangeVariable)
			: base(rangeVariables, currentRangeVariable)
		{
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000BD56 File Offset: 0x00009F56
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return EdmCoreModel.Instance.GetBoolean(true);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000BD63 File Offset: 0x00009F63
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.All;
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000BD67 File Offset: 0x00009F67
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
