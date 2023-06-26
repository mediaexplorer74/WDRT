using System;
using System.Windows;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x02000020 RID: 32
	public abstract class TriggerBase<T> : TriggerBase where T : DependencyObject
	{
		// Token: 0x0600010D RID: 269 RVA: 0x00004CCE File Offset: 0x00002ECE
		protected TriggerBase()
			: base(typeof(T))
		{
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00004CE0 File Offset: 0x00002EE0
		protected new T AssociatedObject
		{
			get
			{
				return (T)((object)base.AssociatedObject);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00004CED File Offset: 0x00002EED
		protected sealed override Type AssociatedObjectTypeConstraint
		{
			get
			{
				return base.AssociatedObjectTypeConstraint;
			}
		}
	}
}
