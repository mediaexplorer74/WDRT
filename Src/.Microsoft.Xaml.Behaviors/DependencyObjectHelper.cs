using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x0200000C RID: 12
	public static class DependencyObjectHelper
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002B38 File Offset: 0x00000D38
		public static IEnumerable<DependencyObject> GetSelfAndAncestors(this DependencyObject dependencyObject)
		{
			while (dependencyObject != null)
			{
				yield return dependencyObject;
				dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
			}
			yield break;
		}
	}
}
