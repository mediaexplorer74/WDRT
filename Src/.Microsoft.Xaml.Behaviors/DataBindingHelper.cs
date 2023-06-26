using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Data;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x0200000A RID: 10
	internal static class DataBindingHelper
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002900 File Offset: 0x00000B00
		public static void EnsureDataBindingUpToDateOnMembers(DependencyObject dpObject)
		{
			IList<DependencyProperty> list = null;
			if (!DataBindingHelper.DependenciesPropertyCache.TryGetValue(dpObject.GetType(), out list))
			{
				list = new List<DependencyProperty>();
				Type type = dpObject.GetType();
				while (type != null)
				{
					foreach (FieldInfo fieldInfo in type.GetFields())
					{
						if (fieldInfo.IsPublic && fieldInfo.FieldType == typeof(DependencyProperty))
						{
							DependencyProperty dependencyProperty = fieldInfo.GetValue(null) as DependencyProperty;
							if (dependencyProperty != null)
							{
								list.Add(dependencyProperty);
							}
						}
					}
					type = type.BaseType;
				}
				DataBindingHelper.DependenciesPropertyCache[dpObject.GetType()] = list;
			}
			if (list == null)
			{
				return;
			}
			foreach (DependencyProperty dependencyProperty2 in list)
			{
				DataBindingHelper.EnsureBindingUpToDate(dpObject, dependencyProperty2);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000029F4 File Offset: 0x00000BF4
		public static void EnsureDataBindingOnActionsUpToDate(TriggerBase<DependencyObject> trigger)
		{
			foreach (TriggerAction triggerAction in trigger.Actions)
			{
				DataBindingHelper.EnsureDataBindingUpToDateOnMembers(triggerAction);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002A44 File Offset: 0x00000C44
		public static void EnsureBindingUpToDate(DependencyObject target, DependencyProperty dp)
		{
			BindingExpression bindingExpression = BindingOperations.GetBindingExpression(target, dp);
			if (bindingExpression != null)
			{
				bindingExpression.UpdateTarget();
			}
		}

		// Token: 0x0400001A RID: 26
		private static Dictionary<Type, IList<DependencyProperty>> DependenciesPropertyCache = new Dictionary<Type, IList<DependencyProperty>>();
	}
}
