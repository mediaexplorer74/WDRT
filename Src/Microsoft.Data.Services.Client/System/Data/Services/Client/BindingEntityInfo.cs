using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Services.Client.Metadata;
using System.Data.Services.Common;
using System.Linq;
using System.Threading;

namespace System.Data.Services.Client
{
	// Token: 0x020000E5 RID: 229
	internal class BindingEntityInfo
	{
		// Token: 0x06000779 RID: 1913 RVA: 0x000200E0 File Offset: 0x0001E2E0
		internal static IList<BindingEntityInfo.BindingPropertyInfo> GetObservableProperties(Type entityType, ClientEdmModel model)
		{
			return BindingEntityInfo.GetBindingEntityInfoFor(entityType, model).ObservableProperties;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x000200EE File Offset: 0x0001E2EE
		internal static ClientTypeAnnotation GetClientType(Type entityType, ClientEdmModel model)
		{
			return BindingEntityInfo.GetBindingEntityInfoFor(entityType, model).ClientType;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x000200FC File Offset: 0x0001E2FC
		internal static string GetEntitySet(object target, string targetEntitySet, ClientEdmModel model)
		{
			if (!string.IsNullOrEmpty(targetEntitySet))
			{
				return targetEntitySet;
			}
			return BindingEntityInfo.GetEntitySetAttribute(target.GetType(), model);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00020114 File Offset: 0x0001E314
		internal static bool IsDataServiceCollection(Type collectionType, ClientEdmModel model)
		{
			BindingEntityInfo.metadataCacheLock.EnterReadLock();
			try
			{
				object obj;
				if (BindingEntityInfo.knownObservableCollectionTypes.TryGetValue(collectionType, out obj))
				{
					return obj == BindingEntityInfo.TrueObject;
				}
			}
			finally
			{
				BindingEntityInfo.metadataCacheLock.ExitReadLock();
			}
			Type type = collectionType;
			bool flag = false;
			while (type != null)
			{
				if (type.IsGenericType())
				{
					Type[] genericArguments = type.GetGenericArguments();
					if (genericArguments != null && genericArguments.Length == 1 && BindingEntityInfo.IsEntityType(genericArguments[0], model))
					{
						Type dataServiceCollectionOfT = WebUtil.GetDataServiceCollectionOfT(genericArguments);
						if (dataServiceCollectionOfT != null && dataServiceCollectionOfT.IsAssignableFrom(type))
						{
							flag = true;
							break;
						}
					}
				}
				type = type.GetBaseType();
			}
			BindingEntityInfo.metadataCacheLock.EnterWriteLock();
			try
			{
				if (!BindingEntityInfo.knownObservableCollectionTypes.ContainsKey(collectionType))
				{
					BindingEntityInfo.knownObservableCollectionTypes[collectionType] = (flag ? BindingEntityInfo.TrueObject : BindingEntityInfo.FalseObject);
				}
			}
			finally
			{
				BindingEntityInfo.metadataCacheLock.ExitWriteLock();
			}
			return flag;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00020210 File Offset: 0x0001E410
		internal static bool IsEntityType(Type type, ClientEdmModel model)
		{
			BindingEntityInfo.metadataCacheLock.EnterReadLock();
			try
			{
				if (BindingEntityInfo.knownNonEntityTypes.Contains(type))
				{
					return false;
				}
			}
			finally
			{
				BindingEntityInfo.metadataCacheLock.ExitReadLock();
			}
			bool flag;
			try
			{
				if (BindingEntityInfo.IsDataServiceCollection(type, model))
				{
					return false;
				}
				flag = ClientTypeUtil.TypeOrElementTypeIsEntity(type);
			}
			catch (InvalidOperationException)
			{
				flag = false;
			}
			if (!flag)
			{
				BindingEntityInfo.metadataCacheLock.EnterWriteLock();
				try
				{
					if (!BindingEntityInfo.knownNonEntityTypes.Contains(type))
					{
						BindingEntityInfo.knownNonEntityTypes.Add(type);
					}
				}
				finally
				{
					BindingEntityInfo.metadataCacheLock.ExitWriteLock();
				}
			}
			return flag;
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x000202E0 File Offset: 0x0001E4E0
		internal static bool TryGetPropertyValue(object source, string sourceProperty, ClientEdmModel model, out BindingEntityInfo.BindingPropertyInfo bindingPropertyInfo, out ClientPropertyAnnotation clientProperty, out object propertyValue)
		{
			Type type = source.GetType();
			bindingPropertyInfo = BindingEntityInfo.GetObservableProperties(type, model).SingleOrDefault((BindingEntityInfo.BindingPropertyInfo x) => x.PropertyInfo.PropertyName == sourceProperty);
			bool flag = bindingPropertyInfo != null;
			if (!flag)
			{
				clientProperty = BindingEntityInfo.GetClientType(type, model).GetProperty(sourceProperty, true);
				flag = clientProperty != null;
				if (!flag)
				{
					propertyValue = null;
				}
				else
				{
					propertyValue = clientProperty.GetValue(source);
				}
			}
			else
			{
				clientProperty = null;
				propertyValue = bindingPropertyInfo.PropertyInfo.GetValue(source);
			}
			return flag;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00020374 File Offset: 0x0001E574
		private static BindingEntityInfo.BindingEntityInfoPerType GetBindingEntityInfoFor(Type entityType, ClientEdmModel model)
		{
			BindingEntityInfo.metadataCacheLock.EnterReadLock();
			BindingEntityInfo.BindingEntityInfoPerType bindingEntityInfoPerType;
			try
			{
				if (BindingEntityInfo.bindingEntityInfos.TryGetValue(entityType, out bindingEntityInfoPerType))
				{
					return bindingEntityInfoPerType;
				}
			}
			finally
			{
				BindingEntityInfo.metadataCacheLock.ExitReadLock();
			}
			bindingEntityInfoPerType = new BindingEntityInfo.BindingEntityInfoPerType();
			EntitySetAttribute entitySetAttribute = (EntitySetAttribute)entityType.GetCustomAttributes(typeof(EntitySetAttribute), true).SingleOrDefault<object>();
			bindingEntityInfoPerType.EntitySet = ((entitySetAttribute != null) ? entitySetAttribute.EntitySet : null);
			bindingEntityInfoPerType.ClientType = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(entityType));
			foreach (ClientPropertyAnnotation clientPropertyAnnotation in bindingEntityInfoPerType.ClientType.Properties())
			{
				BindingEntityInfo.BindingPropertyInfo bindingPropertyInfo = null;
				Type propertyType = clientPropertyAnnotation.PropertyType;
				if (!clientPropertyAnnotation.IsStreamLinkProperty)
				{
					if (clientPropertyAnnotation.IsPrimitiveOrComplexCollection)
					{
						bindingPropertyInfo = new BindingEntityInfo.BindingPropertyInfo
						{
							PropertyKind = BindingPropertyKind.BindingPropertyKindPrimitiveOrComplexCollection
						};
					}
					else if (clientPropertyAnnotation.IsEntityCollection)
					{
						if (BindingEntityInfo.IsDataServiceCollection(propertyType, model))
						{
							bindingPropertyInfo = new BindingEntityInfo.BindingPropertyInfo
							{
								PropertyKind = BindingPropertyKind.BindingPropertyKindDataServiceCollection
							};
						}
					}
					else if (BindingEntityInfo.IsEntityType(propertyType, model))
					{
						bindingPropertyInfo = new BindingEntityInfo.BindingPropertyInfo
						{
							PropertyKind = BindingPropertyKind.BindingPropertyKindEntity
						};
					}
					else if (BindingEntityInfo.CanBeComplexType(propertyType))
					{
						bindingPropertyInfo = new BindingEntityInfo.BindingPropertyInfo
						{
							PropertyKind = BindingPropertyKind.BindingPropertyKindComplex
						};
					}
					if (bindingPropertyInfo != null)
					{
						bindingPropertyInfo.PropertyInfo = clientPropertyAnnotation;
						if (bindingEntityInfoPerType.ClientType.IsEntityType || bindingPropertyInfo.PropertyKind == BindingPropertyKind.BindingPropertyKindComplex || bindingPropertyInfo.PropertyKind == BindingPropertyKind.BindingPropertyKindPrimitiveOrComplexCollection)
						{
							bindingEntityInfoPerType.ObservableProperties.Add(bindingPropertyInfo);
						}
					}
				}
			}
			BindingEntityInfo.metadataCacheLock.EnterWriteLock();
			try
			{
				if (!BindingEntityInfo.bindingEntityInfos.ContainsKey(entityType))
				{
					BindingEntityInfo.bindingEntityInfos[entityType] = bindingEntityInfoPerType;
				}
			}
			finally
			{
				BindingEntityInfo.metadataCacheLock.ExitWriteLock();
			}
			return bindingEntityInfoPerType;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00020550 File Offset: 0x0001E750
		private static bool CanBeComplexType(Type type)
		{
			return typeof(INotifyPropertyChanged).IsAssignableFrom(type);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00020562 File Offset: 0x0001E762
		private static string GetEntitySetAttribute(Type entityType, ClientEdmModel model)
		{
			return BindingEntityInfo.GetBindingEntityInfoFor(entityType, model).EntitySet;
		}

		// Token: 0x0400049B RID: 1179
		private static readonly object FalseObject = new object();

		// Token: 0x0400049C RID: 1180
		private static readonly object TrueObject = new object();

		// Token: 0x0400049D RID: 1181
		private static readonly ReaderWriterLockSlim metadataCacheLock = new ReaderWriterLockSlim();

		// Token: 0x0400049E RID: 1182
		private static readonly HashSet<Type> knownNonEntityTypes = new HashSet<Type>(EqualityComparer<Type>.Default);

		// Token: 0x0400049F RID: 1183
		private static readonly Dictionary<Type, object> knownObservableCollectionTypes = new Dictionary<Type, object>(EqualityComparer<Type>.Default);

		// Token: 0x040004A0 RID: 1184
		private static readonly Dictionary<Type, BindingEntityInfo.BindingEntityInfoPerType> bindingEntityInfos = new Dictionary<Type, BindingEntityInfo.BindingEntityInfoPerType>(EqualityComparer<Type>.Default);

		// Token: 0x020000E6 RID: 230
		internal class BindingPropertyInfo
		{
			// Token: 0x170001B1 RID: 433
			// (get) Token: 0x06000784 RID: 1924 RVA: 0x000205D0 File Offset: 0x0001E7D0
			// (set) Token: 0x06000785 RID: 1925 RVA: 0x000205D8 File Offset: 0x0001E7D8
			public ClientPropertyAnnotation PropertyInfo { get; set; }

			// Token: 0x170001B2 RID: 434
			// (get) Token: 0x06000786 RID: 1926 RVA: 0x000205E1 File Offset: 0x0001E7E1
			// (set) Token: 0x06000787 RID: 1927 RVA: 0x000205E9 File Offset: 0x0001E7E9
			public BindingPropertyKind PropertyKind { get; set; }
		}

		// Token: 0x020000E7 RID: 231
		private sealed class BindingEntityInfoPerType
		{
			// Token: 0x06000789 RID: 1929 RVA: 0x000205FA File Offset: 0x0001E7FA
			public BindingEntityInfoPerType()
			{
				this.observableProperties = new List<BindingEntityInfo.BindingPropertyInfo>();
			}

			// Token: 0x170001B3 RID: 435
			// (get) Token: 0x0600078A RID: 1930 RVA: 0x0002060D File Offset: 0x0001E80D
			// (set) Token: 0x0600078B RID: 1931 RVA: 0x00020615 File Offset: 0x0001E815
			public string EntitySet { get; set; }

			// Token: 0x170001B4 RID: 436
			// (get) Token: 0x0600078C RID: 1932 RVA: 0x0002061E File Offset: 0x0001E81E
			// (set) Token: 0x0600078D RID: 1933 RVA: 0x00020626 File Offset: 0x0001E826
			public ClientTypeAnnotation ClientType { get; set; }

			// Token: 0x170001B5 RID: 437
			// (get) Token: 0x0600078E RID: 1934 RVA: 0x0002062F File Offset: 0x0001E82F
			public List<BindingEntityInfo.BindingPropertyInfo> ObservableProperties
			{
				get
				{
					return this.observableProperties;
				}
			}

			// Token: 0x040004A3 RID: 1187
			private List<BindingEntityInfo.BindingPropertyInfo> observableProperties;
		}
	}
}
