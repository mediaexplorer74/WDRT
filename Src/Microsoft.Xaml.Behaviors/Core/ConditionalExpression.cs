using System;
using System.Windows;
using System.Windows.Markup;

namespace Microsoft.Xaml.Behaviors.Core
{
	// Token: 0x0200003E RID: 62
	[ContentProperty("Conditions")]
	public class ConditionalExpression : Freezable, ICondition
	{
		// Token: 0x0600022B RID: 555 RVA: 0x00009225 File Offset: 0x00007425
		protected override Freezable CreateInstanceCore()
		{
			return new ConditionalExpression();
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000922C File Offset: 0x0000742C
		// (set) Token: 0x0600022D RID: 557 RVA: 0x0000923E File Offset: 0x0000743E
		public ForwardChaining ForwardChaining
		{
			get
			{
				return (ForwardChaining)base.GetValue(ConditionalExpression.ForwardChainingProperty);
			}
			set
			{
				base.SetValue(ConditionalExpression.ForwardChainingProperty, value);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00009251 File Offset: 0x00007451
		public ConditionCollection Conditions
		{
			get
			{
				return (ConditionCollection)base.GetValue(ConditionalExpression.ConditionsProperty);
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00009263 File Offset: 0x00007463
		public ConditionalExpression()
		{
			base.SetValue(ConditionalExpression.ConditionsProperty, new ConditionCollection());
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000927C File Offset: 0x0000747C
		public bool Evaluate()
		{
			bool flag = false;
			foreach (ComparisonCondition comparisonCondition in this.Conditions)
			{
				flag = comparisonCondition.Evaluate();
				if (!flag && this.ForwardChaining == ForwardChaining.And)
				{
					return flag;
				}
				if (flag && this.ForwardChaining == ForwardChaining.Or)
				{
					return flag;
				}
			}
			return flag;
		}

		// Token: 0x040000C0 RID: 192
		public static readonly DependencyProperty ConditionsProperty = DependencyProperty.Register("Conditions", typeof(ConditionCollection), typeof(ConditionalExpression), new PropertyMetadata(null));

		// Token: 0x040000C1 RID: 193
		public static readonly DependencyProperty ForwardChainingProperty = DependencyProperty.Register("ForwardChaining", typeof(ForwardChaining), typeof(ConditionalExpression), new PropertyMetadata(ForwardChaining.And));
	}
}
