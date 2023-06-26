using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Query.SemanticAst;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x02000023 RID: 35
	internal sealed class ExpandOptionExpandBinder : ExpandBinder
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x000044ED File Offset: 0x000026ED
		public ExpandOptionExpandBinder(ODataUriParserConfiguration configuration, IEdmEntityType entityType, IEdmEntitySet entitySet)
			: base(configuration, entityType, entitySet)
		{
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000044F8 File Offset: 0x000026F8
		protected override SelectExpandClause GenerateSubExpand(IEdmNavigationProperty currentNavProp, ExpandTermToken tokenIn)
		{
			ExpandBinder expandBinder = new ExpandOptionExpandBinder(base.Configuration, currentNavProp.ToEntityType(), (base.EntitySet != null) ? base.EntitySet.FindNavigationTarget(currentNavProp) : null);
			return expandBinder.Bind(tokenIn.ExpandOption);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000453C File Offset: 0x0000273C
		protected override SelectExpandClause DecorateExpandWithSelect(SelectExpandClause subExpand, IEdmNavigationProperty currentNavProp, SelectToken select)
		{
			SelectBinder selectBinder = new SelectBinder(base.Model, currentNavProp.ToEntityType(), base.Settings.SelectExpandLimit, subExpand);
			return selectBinder.Bind(select);
		}
	}
}
