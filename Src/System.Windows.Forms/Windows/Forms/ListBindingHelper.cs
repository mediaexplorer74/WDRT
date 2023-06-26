using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace System.Windows.Forms
{
	/// <summary>Provides functionality to discover a bindable list and the properties of the items contained in the list when they differ from the public properties of the object to which they bind.</summary>
	// Token: 0x020002CA RID: 714
	public static class ListBindingHelper
	{
		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06002BC0 RID: 11200 RVA: 0x000C4E44 File Offset: 0x000C3044
		private static Attribute[] BrowsableAttributeList
		{
			get
			{
				if (ListBindingHelper.browsableAttribute == null)
				{
					ListBindingHelper.browsableAttribute = new Attribute[]
					{
						new BrowsableAttribute(true)
					};
				}
				return ListBindingHelper.browsableAttribute;
			}
		}

		/// <summary>Returns a list associated with the specified data source.</summary>
		/// <param name="list">The data source to examine for its underlying list.</param>
		/// <returns>An <see cref="T:System.Object" /> representing the underlying list if it exists; otherwise, the original data source specified by <paramref name="list" />.</returns>
		// Token: 0x06002BC1 RID: 11201 RVA: 0x000C4E66 File Offset: 0x000C3066
		public static object GetList(object list)
		{
			if (list is IListSource)
			{
				return (list as IListSource).GetList();
			}
			return list;
		}

		/// <summary>Returns an object, typically a list, from the evaluation of a specified data source and optional data member.</summary>
		/// <param name="dataSource">The data source from which to find the list.</param>
		/// <param name="dataMember">The name of the data source property that contains the list. This can be <see langword="null" />.</param>
		/// <returns>An <see cref="T:System.Object" /> representing the underlying list if it was found; otherwise, <paramref name="dataSource" />.</returns>
		/// <exception cref="T:System.ArgumentException">The specified data member name did not match any of the properties found for the data source.</exception>
		// Token: 0x06002BC2 RID: 11202 RVA: 0x000C4E80 File Offset: 0x000C3080
		public static object GetList(object dataSource, string dataMember)
		{
			dataSource = ListBindingHelper.GetList(dataSource);
			if (dataSource == null || dataSource is Type || string.IsNullOrEmpty(dataMember))
			{
				return dataSource;
			}
			PropertyDescriptorCollection listItemProperties = ListBindingHelper.GetListItemProperties(dataSource);
			PropertyDescriptor propertyDescriptor = listItemProperties.Find(dataMember, true);
			if (propertyDescriptor == null)
			{
				throw new ArgumentException(SR.GetString("DataSourceDataMemberPropNotFound", new object[] { dataMember }));
			}
			object obj;
			if (dataSource is ICurrencyManagerProvider)
			{
				CurrencyManager currencyManager = (dataSource as ICurrencyManagerProvider).CurrencyManager;
				obj = ((currencyManager != null && currencyManager.Position >= 0 && currencyManager.Position <= currencyManager.Count - 1) ? currencyManager.Current : null);
			}
			else if (dataSource is IEnumerable)
			{
				obj = ListBindingHelper.GetFirstItemByEnumerable(dataSource as IEnumerable);
			}
			else
			{
				obj = dataSource;
			}
			if (obj != null)
			{
				return propertyDescriptor.GetValue(obj);
			}
			return null;
		}

		/// <summary>Returns the name of an underlying list, given a data source and optional <see cref="T:System.ComponentModel.PropertyDescriptor" /> array.</summary>
		/// <param name="list">The data source to examine for the list name.</param>
		/// <param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects to find in the data source. This can be <see langword="null" />.</param>
		/// <returns>The name of the list in the data source, as described by <paramref name="listAccessors" />, or the name of the data source type.</returns>
		// Token: 0x06002BC3 RID: 11203 RVA: 0x000C4F44 File Offset: 0x000C3144
		public static string GetListName(object list, PropertyDescriptor[] listAccessors)
		{
			if (list == null)
			{
				return string.Empty;
			}
			ITypedList typedList = list as ITypedList;
			string text;
			if (typedList != null)
			{
				text = typedList.GetListName(listAccessors);
			}
			else
			{
				Type type2;
				if (listAccessors == null || listAccessors.Length == 0)
				{
					Type type = list as Type;
					if (type != null)
					{
						type2 = type;
					}
					else
					{
						type2 = list.GetType();
					}
				}
				else
				{
					PropertyDescriptor propertyDescriptor = listAccessors[0];
					type2 = propertyDescriptor.PropertyType;
				}
				text = ListBindingHelper.GetListNameFromType(type2);
			}
			return text;
		}

		/// <summary>Returns the <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that describes the properties of an item type contained in a specified data source, or properties of the specified data source.</summary>
		/// <param name="list">The data source to examine for property information.</param>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the properties of the items contained in <paramref name="list" />, or properties of <paramref name="list." /></returns>
		// Token: 0x06002BC4 RID: 11204 RVA: 0x000C4FA8 File Offset: 0x000C31A8
		public static PropertyDescriptorCollection GetListItemProperties(object list)
		{
			if (list == null)
			{
				return new PropertyDescriptorCollection(null);
			}
			PropertyDescriptorCollection propertyDescriptorCollection;
			if (list is Type)
			{
				propertyDescriptorCollection = ListBindingHelper.GetListItemPropertiesByType(list as Type);
			}
			else
			{
				object list2 = ListBindingHelper.GetList(list);
				if (list2 is ITypedList)
				{
					propertyDescriptorCollection = (list2 as ITypedList).GetItemProperties(null);
				}
				else if (list2 is IEnumerable)
				{
					propertyDescriptorCollection = ListBindingHelper.GetListItemPropertiesByEnumerable(list2 as IEnumerable);
				}
				else
				{
					propertyDescriptorCollection = TypeDescriptor.GetProperties(list2);
				}
			}
			return propertyDescriptorCollection;
		}

		/// <summary>Returns the <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that describes the properties of an item type contained in a collection property of a data source. Uses the specified <see cref="T:System.ComponentModel.PropertyDescriptor" /> array to indicate which properties to examine.</summary>
		/// <param name="list">The data source to be examined for property information.</param>
		/// <param name="listAccessors">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> array describing which properties of the data source to examine. This can be <see langword="null" />.</param>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> describing the properties of the item type contained in a collection property of the data source.</returns>
		// Token: 0x06002BC5 RID: 11205 RVA: 0x000C5014 File Offset: 0x000C3214
		public static PropertyDescriptorCollection GetListItemProperties(object list, PropertyDescriptor[] listAccessors)
		{
			PropertyDescriptorCollection propertyDescriptorCollection;
			if (listAccessors == null || listAccessors.Length == 0)
			{
				propertyDescriptorCollection = ListBindingHelper.GetListItemProperties(list);
			}
			else if (list is Type)
			{
				propertyDescriptorCollection = ListBindingHelper.GetListItemPropertiesByType(list as Type, listAccessors);
			}
			else
			{
				object list2 = ListBindingHelper.GetList(list);
				if (list2 is ITypedList)
				{
					propertyDescriptorCollection = (list2 as ITypedList).GetItemProperties(listAccessors);
				}
				else if (list2 is IEnumerable)
				{
					propertyDescriptorCollection = ListBindingHelper.GetListItemPropertiesByEnumerable(list2 as IEnumerable, listAccessors);
				}
				else
				{
					propertyDescriptorCollection = ListBindingHelper.GetListItemPropertiesByInstance(list2, listAccessors, 0);
				}
			}
			return propertyDescriptorCollection;
		}

		/// <summary>Returns the <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that describes the properties of an item type contained in the specified data member of a data source. Uses the specified <see cref="T:System.ComponentModel.PropertyDescriptor" /> array to indicate which properties to examine.</summary>
		/// <param name="dataSource">The data source to be examined for property information.</param>
		/// <param name="dataMember">The optional data member to be examined for property information. This can be <see langword="null" />.</param>
		/// <param name="listAccessors">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> array describing which properties of the data member to examine. This can be <see langword="null" />.</param>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> describing the properties of an item type contained in a collection property of the specified data source.</returns>
		/// <exception cref="T:System.ArgumentException">The specified data member could not be found in the specified data source.</exception>
		// Token: 0x06002BC6 RID: 11206 RVA: 0x000C5088 File Offset: 0x000C3288
		public static PropertyDescriptorCollection GetListItemProperties(object dataSource, string dataMember, PropertyDescriptor[] listAccessors)
		{
			dataSource = ListBindingHelper.GetList(dataSource);
			if (!string.IsNullOrEmpty(dataMember))
			{
				PropertyDescriptorCollection listItemProperties = ListBindingHelper.GetListItemProperties(dataSource);
				PropertyDescriptor propertyDescriptor = listItemProperties.Find(dataMember, true);
				if (propertyDescriptor == null)
				{
					throw new ArgumentException(SR.GetString("DataSourceDataMemberPropNotFound", new object[] { dataMember }));
				}
				int num = ((listAccessors == null) ? 1 : (listAccessors.Length + 1));
				PropertyDescriptor[] array = new PropertyDescriptor[num];
				array[0] = propertyDescriptor;
				for (int i = 1; i < num; i++)
				{
					array[i] = listAccessors[i - 1];
				}
				listAccessors = array;
			}
			return ListBindingHelper.GetListItemProperties(dataSource, listAccessors);
		}

		/// <summary>Returns the data type of the items in the specified list.</summary>
		/// <param name="list">The list to be examined for type information.</param>
		/// <returns>The <see cref="T:System.Type" /> of the items contained in the list.</returns>
		// Token: 0x06002BC7 RID: 11207 RVA: 0x000C5110 File Offset: 0x000C3310
		public static Type GetListItemType(object list)
		{
			if (list == null)
			{
				return null;
			}
			if (list is Type && typeof(IListSource).IsAssignableFrom(list as Type))
			{
				list = ListBindingHelper.CreateInstanceOfType(list as Type);
			}
			list = ListBindingHelper.GetList(list);
			Type type = ((list is Type) ? (list as Type) : list.GetType());
			object obj = ((list is Type) ? null : list);
			Type type2;
			if (typeof(Array).IsAssignableFrom(type))
			{
				type2 = type.GetElementType();
			}
			else
			{
				PropertyInfo typedIndexer = ListBindingHelper.GetTypedIndexer(type);
				if (typedIndexer != null)
				{
					type2 = typedIndexer.PropertyType;
				}
				else if (obj is IEnumerable)
				{
					type2 = ListBindingHelper.GetListItemTypeByEnumerable(obj as IEnumerable);
				}
				else
				{
					type2 = type;
				}
			}
			return type2;
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x000C51CC File Offset: 0x000C33CC
		private static object CreateInstanceOfType(Type type)
		{
			object obj = null;
			Exception ex = null;
			try
			{
				obj = SecurityUtils.SecureCreateInstance(type);
			}
			catch (TargetInvocationException ex2)
			{
				ex = ex2;
			}
			catch (MethodAccessException ex3)
			{
				ex = ex3;
			}
			catch (MissingMethodException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				throw new NotSupportedException(SR.GetString("BindingSourceInstanceError"), ex);
			}
			return obj;
		}

		/// <summary>Returns the data type of the items in the specified data source.</summary>
		/// <param name="dataSource">The data source to examine for items.</param>
		/// <param name="dataMember">The optional name of the property on the data source that is to be used as the data member. This can be <see langword="null" />.</param>
		/// <returns>For complex data binding, the <see cref="T:System.Type" /> of the items represented by the <paramref name="dataMember" /> in the data source; otherwise, the <see cref="T:System.Type" /> of the item in the list itself.</returns>
		// Token: 0x06002BC9 RID: 11209 RVA: 0x000C5234 File Offset: 0x000C3434
		public static Type GetListItemType(object dataSource, string dataMember)
		{
			if (dataSource == null)
			{
				return typeof(object);
			}
			if (string.IsNullOrEmpty(dataMember))
			{
				return ListBindingHelper.GetListItemType(dataSource);
			}
			PropertyDescriptorCollection listItemProperties = ListBindingHelper.GetListItemProperties(dataSource);
			if (listItemProperties == null)
			{
				return typeof(object);
			}
			PropertyDescriptor propertyDescriptor = listItemProperties.Find(dataMember, true);
			if (propertyDescriptor == null || propertyDescriptor.PropertyType is ICustomTypeDescriptor)
			{
				return typeof(object);
			}
			return ListBindingHelper.GetListItemType(propertyDescriptor.PropertyType);
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x000C52A4 File Offset: 0x000C34A4
		private static string GetListNameFromType(Type type)
		{
			string text;
			if (typeof(Array).IsAssignableFrom(type))
			{
				text = type.GetElementType().Name;
			}
			else if (typeof(IList).IsAssignableFrom(type))
			{
				PropertyInfo typedIndexer = ListBindingHelper.GetTypedIndexer(type);
				if (typedIndexer != null)
				{
					text = typedIndexer.PropertyType.Name;
				}
				else
				{
					text = type.Name;
				}
			}
			else
			{
				text = type.Name;
			}
			return text;
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x000C5314 File Offset: 0x000C3514
		private static PropertyDescriptorCollection GetListItemPropertiesByType(Type type, PropertyDescriptor[] listAccessors)
		{
			PropertyDescriptorCollection propertyDescriptorCollection;
			if (listAccessors == null || listAccessors.Length == 0)
			{
				propertyDescriptorCollection = ListBindingHelper.GetListItemPropertiesByType(type);
			}
			else
			{
				propertyDescriptorCollection = ListBindingHelper.GetListItemPropertiesByType(type, listAccessors, 0);
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x06002BCC RID: 11212 RVA: 0x000C5340 File Offset: 0x000C3540
		private static PropertyDescriptorCollection GetListItemPropertiesByType(Type type, PropertyDescriptor[] listAccessors, int startIndex)
		{
			Type propertyType = listAccessors[startIndex].PropertyType;
			startIndex++;
			PropertyDescriptorCollection propertyDescriptorCollection;
			if (startIndex >= listAccessors.Length)
			{
				propertyDescriptorCollection = ListBindingHelper.GetListItemProperties(propertyType);
			}
			else
			{
				propertyDescriptorCollection = ListBindingHelper.GetListItemPropertiesByType(propertyType, listAccessors, startIndex);
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x000C5378 File Offset: 0x000C3578
		private static PropertyDescriptorCollection GetListItemPropertiesByEnumerable(IEnumerable iEnumerable, PropertyDescriptor[] listAccessors, int startIndex)
		{
			object obj = null;
			object firstItemByEnumerable = ListBindingHelper.GetFirstItemByEnumerable(iEnumerable);
			if (firstItemByEnumerable != null)
			{
				obj = ListBindingHelper.GetList(listAccessors[startIndex].GetValue(firstItemByEnumerable));
			}
			PropertyDescriptorCollection propertyDescriptorCollection;
			if (obj == null)
			{
				propertyDescriptorCollection = ListBindingHelper.GetListItemPropertiesByType(listAccessors[startIndex].PropertyType, listAccessors, startIndex);
			}
			else
			{
				startIndex++;
				IEnumerable enumerable = obj as IEnumerable;
				if (enumerable != null)
				{
					if (startIndex == listAccessors.Length)
					{
						propertyDescriptorCollection = ListBindingHelper.GetListItemPropertiesByEnumerable(enumerable);
					}
					else
					{
						propertyDescriptorCollection = ListBindingHelper.GetListItemPropertiesByEnumerable(enumerable, listAccessors, startIndex);
					}
				}
				else
				{
					propertyDescriptorCollection = ListBindingHelper.GetListItemPropertiesByInstance(obj, listAccessors, startIndex);
				}
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x000C53EC File Offset: 0x000C35EC
		private static PropertyDescriptorCollection GetListItemPropertiesByEnumerable(IEnumerable enumerable, PropertyDescriptor[] listAccessors)
		{
			PropertyDescriptorCollection propertyDescriptorCollection;
			if (listAccessors == null || listAccessors.Length == 0)
			{
				propertyDescriptorCollection = ListBindingHelper.GetListItemPropertiesByEnumerable(enumerable);
			}
			else
			{
				ITypedList typedList = enumerable as ITypedList;
				if (typedList != null)
				{
					propertyDescriptorCollection = typedList.GetItemProperties(listAccessors);
				}
				else
				{
					propertyDescriptorCollection = ListBindingHelper.GetListItemPropertiesByEnumerable(enumerable, listAccessors, 0);
				}
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x000C542C File Offset: 0x000C362C
		private static Type GetListItemTypeByEnumerable(IEnumerable iEnumerable)
		{
			object firstItemByEnumerable = ListBindingHelper.GetFirstItemByEnumerable(iEnumerable);
			if (firstItemByEnumerable == null)
			{
				return typeof(object);
			}
			return firstItemByEnumerable.GetType();
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x000C5454 File Offset: 0x000C3654
		private static PropertyDescriptorCollection GetListItemPropertiesByInstance(object target, PropertyDescriptor[] listAccessors, int startIndex)
		{
			PropertyDescriptorCollection propertyDescriptorCollection;
			if (listAccessors != null && listAccessors.Length > startIndex)
			{
				object value = listAccessors[startIndex].GetValue(target);
				if (value == null)
				{
					propertyDescriptorCollection = ListBindingHelper.GetListItemPropertiesByType(listAccessors[startIndex].PropertyType, listAccessors, startIndex);
				}
				else
				{
					PropertyDescriptor[] array = null;
					if (listAccessors.Length > startIndex + 1)
					{
						int num = listAccessors.Length - (startIndex + 1);
						array = new PropertyDescriptor[num];
						for (int i = 0; i < num; i++)
						{
							array[i] = listAccessors[startIndex + 1 + i];
						}
					}
					propertyDescriptorCollection = ListBindingHelper.GetListItemProperties(value, array);
				}
			}
			else
			{
				propertyDescriptorCollection = TypeDescriptor.GetProperties(target, ListBindingHelper.BrowsableAttributeList);
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x000C54D8 File Offset: 0x000C36D8
		private static bool IsListBasedType(Type type)
		{
			if (typeof(IList).IsAssignableFrom(type) || typeof(ITypedList).IsAssignableFrom(type) || typeof(IListSource).IsAssignableFrom(type))
			{
				return true;
			}
			if (type.IsGenericType && !type.IsGenericTypeDefinition && typeof(IList<>).IsAssignableFrom(type.GetGenericTypeDefinition()))
			{
				return true;
			}
			foreach (Type type2 in type.GetInterfaces())
			{
				if (type2.IsGenericType && typeof(IList<>).IsAssignableFrom(type2.GetGenericTypeDefinition()))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x000C5584 File Offset: 0x000C3784
		private static PropertyInfo GetTypedIndexer(Type type)
		{
			PropertyInfo propertyInfo = null;
			if (!ListBindingHelper.IsListBasedType(type))
			{
				return null;
			}
			PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < properties.Length; i++)
			{
				if (properties[i].GetIndexParameters().Length != 0 && properties[i].PropertyType != typeof(object))
				{
					propertyInfo = properties[i];
					if (propertyInfo.Name == "Item")
					{
						break;
					}
				}
			}
			return propertyInfo;
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x000C55EF File Offset: 0x000C37EF
		private static PropertyDescriptorCollection GetListItemPropertiesByType(Type type)
		{
			return TypeDescriptor.GetProperties(ListBindingHelper.GetListItemType(type), ListBindingHelper.BrowsableAttributeList);
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x000C5604 File Offset: 0x000C3804
		private static PropertyDescriptorCollection GetListItemPropertiesByEnumerable(IEnumerable enumerable)
		{
			PropertyDescriptorCollection propertyDescriptorCollection = null;
			Type type = enumerable.GetType();
			if (typeof(Array).IsAssignableFrom(type))
			{
				propertyDescriptorCollection = TypeDescriptor.GetProperties(type.GetElementType(), ListBindingHelper.BrowsableAttributeList);
			}
			else
			{
				ITypedList typedList = enumerable as ITypedList;
				if (typedList != null)
				{
					propertyDescriptorCollection = typedList.GetItemProperties(null);
				}
				else
				{
					PropertyInfo typedIndexer = ListBindingHelper.GetTypedIndexer(type);
					if (typedIndexer != null && !typeof(ICustomTypeDescriptor).IsAssignableFrom(typedIndexer.PropertyType))
					{
						Type propertyType = typedIndexer.PropertyType;
						propertyDescriptorCollection = TypeDescriptor.GetProperties(propertyType, ListBindingHelper.BrowsableAttributeList);
					}
				}
			}
			if (propertyDescriptorCollection == null)
			{
				object firstItemByEnumerable = ListBindingHelper.GetFirstItemByEnumerable(enumerable);
				if (enumerable is string)
				{
					propertyDescriptorCollection = TypeDescriptor.GetProperties(enumerable, ListBindingHelper.BrowsableAttributeList);
				}
				else if (firstItemByEnumerable == null)
				{
					propertyDescriptorCollection = new PropertyDescriptorCollection(null);
				}
				else
				{
					propertyDescriptorCollection = TypeDescriptor.GetProperties(firstItemByEnumerable, ListBindingHelper.BrowsableAttributeList);
					if (!(enumerable is IList) && (propertyDescriptorCollection == null || propertyDescriptorCollection.Count == 0))
					{
						propertyDescriptorCollection = TypeDescriptor.GetProperties(enumerable, ListBindingHelper.BrowsableAttributeList);
					}
				}
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x06002BD5 RID: 11221 RVA: 0x000C56EC File Offset: 0x000C38EC
		private static object GetFirstItemByEnumerable(IEnumerable enumerable)
		{
			object obj = null;
			if (enumerable is IList)
			{
				IList list = enumerable as IList;
				obj = ((list.Count > 0) ? list[0] : null);
			}
			else
			{
				try
				{
					IEnumerator enumerator = enumerable.GetEnumerator();
					enumerator.Reset();
					if (enumerator.MoveNext())
					{
						obj = enumerator.Current;
					}
					enumerator.Reset();
				}
				catch (NotSupportedException)
				{
					obj = null;
				}
			}
			return obj;
		}

		// Token: 0x0400124D RID: 4685
		private static Attribute[] browsableAttribute;
	}
}
