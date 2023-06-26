using System;
using System.Windows;

namespace Microsoft.Xaml.Behaviors.Core
{
	// Token: 0x0200003B RID: 59
	public class ComparisonCondition : Freezable
	{
		// Token: 0x06000220 RID: 544 RVA: 0x000090E9 File Offset: 0x000072E9
		protected override Freezable CreateInstanceCore()
		{
			return new ComparisonCondition();
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000221 RID: 545 RVA: 0x000090F0 File Offset: 0x000072F0
		// (set) Token: 0x06000222 RID: 546 RVA: 0x000090FD File Offset: 0x000072FD
		public object LeftOperand
		{
			get
			{
				return base.GetValue(ComparisonCondition.LeftOperandProperty);
			}
			set
			{
				base.SetValue(ComparisonCondition.LeftOperandProperty, value);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000910B File Offset: 0x0000730B
		// (set) Token: 0x06000224 RID: 548 RVA: 0x00009118 File Offset: 0x00007318
		public object RightOperand
		{
			get
			{
				return base.GetValue(ComparisonCondition.RightOperandProperty);
			}
			set
			{
				base.SetValue(ComparisonCondition.RightOperandProperty, value);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00009126 File Offset: 0x00007326
		// (set) Token: 0x06000226 RID: 550 RVA: 0x00009138 File Offset: 0x00007338
		public ComparisonConditionType Operator
		{
			get
			{
				return (ComparisonConditionType)base.GetValue(ComparisonCondition.OperatorProperty);
			}
			set
			{
				base.SetValue(ComparisonCondition.OperatorProperty, value);
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000914B File Offset: 0x0000734B
		public bool Evaluate()
		{
			this.EnsureBindingUpToDate();
			return ComparisonLogic.EvaluateImpl(this.LeftOperand, this.Operator, this.RightOperand);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000916A File Offset: 0x0000736A
		private void EnsureBindingUpToDate()
		{
			DataBindingHelper.EnsureBindingUpToDate(this, ComparisonCondition.LeftOperandProperty);
			DataBindingHelper.EnsureBindingUpToDate(this, ComparisonCondition.OperatorProperty);
			DataBindingHelper.EnsureBindingUpToDate(this, ComparisonCondition.RightOperandProperty);
		}

		// Token: 0x040000B3 RID: 179
		public static readonly DependencyProperty LeftOperandProperty = DependencyProperty.Register("LeftOperand", typeof(object), typeof(ComparisonCondition), new PropertyMetadata(null));

		// Token: 0x040000B4 RID: 180
		public static readonly DependencyProperty OperatorProperty = DependencyProperty.Register("Operator", typeof(ComparisonConditionType), typeof(ComparisonCondition), new PropertyMetadata(ComparisonConditionType.Equal));

		// Token: 0x040000B5 RID: 181
		public static readonly DependencyProperty RightOperandProperty = DependencyProperty.Register("RightOperand", typeof(object), typeof(ComparisonCondition), new PropertyMetadata(null));
	}
}
