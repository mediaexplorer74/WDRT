using System;
using System.Collections.ObjectModel;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x0200009A RID: 154
	public abstract class LambdaNode : SingleValueNode
	{
		// Token: 0x060003A3 RID: 931 RVA: 0x0000BCF0 File Offset: 0x00009EF0
		protected LambdaNode(Collection<RangeVariable> rangeVariables)
			: this(rangeVariables, null)
		{
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000BCFA File Offset: 0x00009EFA
		protected LambdaNode(Collection<RangeVariable> rangeVariables, RangeVariable currentRangeVariable)
		{
			this.rangeVariables = rangeVariables;
			this.currentRangeVariable = currentRangeVariable;
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000BD10 File Offset: 0x00009F10
		public Collection<RangeVariable> RangeVariables
		{
			get
			{
				return this.rangeVariables;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000BD18 File Offset: 0x00009F18
		public RangeVariable CurrentRangeVariable
		{
			get
			{
				return this.currentRangeVariable;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000BD20 File Offset: 0x00009F20
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x0000BD28 File Offset: 0x00009F28
		public SingleValueNode Body { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000BD31 File Offset: 0x00009F31
		// (set) Token: 0x060003AA RID: 938 RVA: 0x0000BD39 File Offset: 0x00009F39
		public CollectionNode Source { get; set; }

		// Token: 0x04000119 RID: 281
		private readonly Collection<RangeVariable> rangeVariables;

		// Token: 0x0400011A RID: 282
		private readonly RangeVariable currentRangeVariable;
	}
}
