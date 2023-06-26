using System;

namespace Microsoft.Data.Edm.Validation
{
	// Token: 0x0200023C RID: 572
	public sealed class ValidationRule<TItem> : ValidationRule where TItem : IEdmElement
	{
		// Token: 0x06000D13 RID: 3347 RVA: 0x00029645 File Offset: 0x00027845
		public ValidationRule(Action<ValidationContext, TItem> validate)
		{
			this.validate = validate;
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x00029654 File Offset: 0x00027854
		internal override Type ValidatedType
		{
			get
			{
				return typeof(TItem);
			}
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00029660 File Offset: 0x00027860
		internal override void Evaluate(ValidationContext context, object item)
		{
			TItem titem = (TItem)((object)item);
			this.validate(context, titem);
		}

		// Token: 0x0400067D RID: 1661
		private readonly Action<ValidationContext, TItem> validate;
	}
}
