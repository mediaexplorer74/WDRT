using System;

namespace System.Data.Services.Client
{
	// Token: 0x020000F0 RID: 240
	internal static class BindingUtils
	{
		// Token: 0x06000808 RID: 2056 RVA: 0x000225F8 File Offset: 0x000207F8
		internal static void ValidateEntitySetName(string entitySetName, object entity)
		{
			if (string.IsNullOrEmpty(entitySetName))
			{
				throw new InvalidOperationException(Strings.DataBinding_Util_UnknownEntitySetName(entity.GetType().FullName));
			}
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00022618 File Offset: 0x00020818
		internal static Type GetCollectionEntityType(Type collectionType)
		{
			while (collectionType != null)
			{
				if (collectionType.IsGenericType() && WebUtil.IsDataServiceCollectionType(collectionType.GetGenericTypeDefinition()))
				{
					return collectionType.GetGenericArguments()[0];
				}
				collectionType = collectionType.GetBaseType();
			}
			return null;
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0002264C File Offset: 0x0002084C
		internal static void VerifyObserverNotPresent<T>(object oec, string sourceProperty, Type sourceType)
		{
			DataServiceCollection<T> dataServiceCollection = oec as DataServiceCollection<T>;
			if (dataServiceCollection.Observer != null)
			{
				throw new InvalidOperationException(Strings.DataBinding_CollectionPropertySetterValueHasObserver(sourceProperty, sourceType));
			}
		}
	}
}
