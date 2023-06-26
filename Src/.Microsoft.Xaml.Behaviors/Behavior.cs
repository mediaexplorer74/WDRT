using System;
using System.Windows;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x02000004 RID: 4
	public abstract class Behavior<T> : Behavior where T : DependencyObject
	{
		// Token: 0x0600000E RID: 14 RVA: 0x0000242D File Offset: 0x0000062D
		protected Behavior()
			: base(typeof(T))
		{
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000243F File Offset: 0x0000063F
		protected new T AssociatedObject
		{
			get
			{
				return (T)((object)base.AssociatedObject);
			}
		}
	}
}
