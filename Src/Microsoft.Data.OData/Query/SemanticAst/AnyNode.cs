using System;
using System.Collections.ObjectModel;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x0200009E RID: 158
	public sealed class AnyNode : LambdaNode
	{
		// Token: 0x060003B8 RID: 952 RVA: 0x0000BDD1 File Offset: 0x00009FD1
		public AnyNode(Collection<RangeVariable> parameters)
			: this(parameters, null)
		{
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000BDDB File Offset: 0x00009FDB
		public AnyNode(Collection<RangeVariable> parameters, RangeVariable currentRangeVariable)
			: base(parameters, currentRangeVariable)
		{
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000BDE5 File Offset: 0x00009FE5
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return EdmCoreModel.Instance.GetBoolean(true);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000BDF2 File Offset: 0x00009FF2
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.Any;
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000BDF6 File Offset: 0x00009FF6
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}
	}
}
