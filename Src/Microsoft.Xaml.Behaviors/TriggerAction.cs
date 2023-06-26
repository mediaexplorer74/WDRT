using System;
using System.Windows;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x0200001D RID: 29
	public abstract class TriggerAction<T> : TriggerAction where T : DependencyObject
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00004A21 File Offset: 0x00002C21
		protected TriggerAction()
			: base(typeof(T))
		{
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00004A33 File Offset: 0x00002C33
		protected new T AssociatedObject
		{
			get
			{
				return (T)((object)base.AssociatedObject);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00004A40 File Offset: 0x00002C40
		protected sealed override Type AssociatedObjectTypeConstraint
		{
			get
			{
				return base.AssociatedObjectTypeConstraint;
			}
		}
	}
}
