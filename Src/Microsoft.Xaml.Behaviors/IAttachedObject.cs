using System;
using System.Windows;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x02000012 RID: 18
	public interface IAttachedObject
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000081 RID: 129
		DependencyObject AssociatedObject { get; }

		// Token: 0x06000082 RID: 130
		void Attach(DependencyObject dependencyObject);

		// Token: 0x06000083 RID: 131
		void Detach();
	}
}
