﻿using System;
using Microsoft.Data.Edm.Expressions;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.Edm.Library.Values
{
	// Token: 0x020001A1 RID: 417
	public class EdmFloatingConstant : EdmValue, IEdmFloatingConstantExpression, IEdmExpression, IEdmFloatingValue, IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x06000916 RID: 2326 RVA: 0x000187C6 File Offset: 0x000169C6
		public EdmFloatingConstant(double value)
			: this(null, value)
		{
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x000187D0 File Offset: 0x000169D0
		public EdmFloatingConstant(IEdmPrimitiveTypeReference type, double value)
			: base(type)
		{
			this.value = value;
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x000187E0 File Offset: 0x000169E0
		public double Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x000187E8 File Offset: 0x000169E8
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.FloatingConstant;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x000187EB File Offset: 0x000169EB
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Floating;
			}
		}

		// Token: 0x04000472 RID: 1138
		private readonly double value;
	}
}
