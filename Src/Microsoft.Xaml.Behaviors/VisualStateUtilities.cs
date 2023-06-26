using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x02000026 RID: 38
	public static class VisualStateUtilities
	{
		// Token: 0x0600012D RID: 301 RVA: 0x00005104 File Offset: 0x00003304
		public static bool GoToState(FrameworkElement element, string stateName, bool useTransitions)
		{
			bool flag = false;
			if (!string.IsNullOrEmpty(stateName))
			{
				Control control = element as Control;
				if (control != null)
				{
					control.ApplyTemplate();
					flag = VisualStateManager.GoToState(control, stateName, useTransitions);
				}
				else
				{
					flag = VisualStateManager.GoToElementState(element, stateName, useTransitions);
				}
			}
			return flag;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005144 File Offset: 0x00003344
		public static IList GetVisualStateGroups(FrameworkElement targetObject)
		{
			IList list = new List<VisualStateGroup>();
			if (targetObject != null)
			{
				list = VisualStateManager.GetVisualStateGroups(targetObject);
				if (list.Count == 0 && VisualTreeHelper.GetChildrenCount(targetObject) > 0)
				{
					list = VisualStateManager.GetVisualStateGroups(VisualTreeHelper.GetChild(targetObject, 0) as FrameworkElement);
				}
				if (list.Count == 0)
				{
					UserControl userControl = targetObject as UserControl;
					if (userControl != null)
					{
						FrameworkElement frameworkElement = userControl.Content as FrameworkElement;
						if (frameworkElement != null)
						{
							list = VisualStateManager.GetVisualStateGroups(frameworkElement);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000051B0 File Offset: 0x000033B0
		public static bool TryFindNearestStatefulControl(FrameworkElement contextElement, out FrameworkElement resolvedControl)
		{
			FrameworkElement frameworkElement = contextElement;
			if (frameworkElement == null)
			{
				resolvedControl = null;
				return false;
			}
			FrameworkElement frameworkElement2 = frameworkElement.Parent as FrameworkElement;
			bool flag = true;
			while (!VisualStateUtilities.HasVisualStateGroupsDefined(frameworkElement) && VisualStateUtilities.ShouldContinueTreeWalk(frameworkElement2))
			{
				frameworkElement = frameworkElement2;
				frameworkElement2 = frameworkElement2.Parent as FrameworkElement;
			}
			if (VisualStateUtilities.HasVisualStateGroupsDefined(frameworkElement))
			{
				if (frameworkElement.TemplatedParent != null && frameworkElement.TemplatedParent is Control)
				{
					frameworkElement = frameworkElement.TemplatedParent as FrameworkElement;
				}
				else if (frameworkElement2 != null && frameworkElement2 is UserControl)
				{
					frameworkElement = frameworkElement2;
				}
			}
			else
			{
				flag = false;
			}
			resolvedControl = frameworkElement;
			return flag;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005235 File Offset: 0x00003435
		private static bool HasVisualStateGroupsDefined(FrameworkElement frameworkElement)
		{
			return frameworkElement != null && VisualStateManager.GetVisualStateGroups(frameworkElement).Count != 0;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000524C File Offset: 0x0000344C
		internal static FrameworkElement FindNearestStatefulControl(FrameworkElement contextElement)
		{
			FrameworkElement frameworkElement = null;
			VisualStateUtilities.TryFindNearestStatefulControl(contextElement, out frameworkElement);
			return frameworkElement;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00005268 File Offset: 0x00003468
		private static bool ShouldContinueTreeWalk(FrameworkElement element)
		{
			if (element == null)
			{
				return false;
			}
			if (element is UserControl)
			{
				return false;
			}
			if (element.Parent == null)
			{
				FrameworkElement frameworkElement = VisualStateUtilities.FindTemplatedParent(element);
				if (frameworkElement == null || (!(frameworkElement is Control) && !(frameworkElement is ContentPresenter)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000052A9 File Offset: 0x000034A9
		private static FrameworkElement FindTemplatedParent(FrameworkElement parent)
		{
			return parent.TemplatedParent as FrameworkElement;
		}
	}
}
