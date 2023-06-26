using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x020005A3 RID: 1443
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal sealed class ReflectTypeDescriptionProvider : TypeDescriptionProvider
	{
		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x060035C1 RID: 13761 RVA: 0x000EA4EF File Offset: 0x000E86EF
		internal static Guid ExtenderProviderKey
		{
			get
			{
				return ReflectTypeDescriptionProvider._extenderProviderKey;
			}
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x000EA4F6 File Offset: 0x000E86F6
		internal ReflectTypeDescriptionProvider()
		{
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x060035C3 RID: 13763 RVA: 0x000EA500 File Offset: 0x000E8700
		private static Hashtable IntrinsicTypeConverters
		{
			get
			{
				if (ReflectTypeDescriptionProvider._intrinsicTypeConverters == null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable[typeof(bool)] = typeof(BooleanConverter);
					hashtable[typeof(byte)] = typeof(ByteConverter);
					hashtable[typeof(sbyte)] = typeof(SByteConverter);
					hashtable[typeof(char)] = typeof(CharConverter);
					hashtable[typeof(double)] = typeof(DoubleConverter);
					hashtable[typeof(string)] = typeof(StringConverter);
					hashtable[typeof(int)] = typeof(Int32Converter);
					hashtable[typeof(short)] = typeof(Int16Converter);
					hashtable[typeof(long)] = typeof(Int64Converter);
					hashtable[typeof(float)] = typeof(SingleConverter);
					hashtable[typeof(ushort)] = typeof(UInt16Converter);
					hashtable[typeof(uint)] = typeof(UInt32Converter);
					hashtable[typeof(ulong)] = typeof(UInt64Converter);
					hashtable[typeof(object)] = typeof(TypeConverter);
					hashtable[typeof(void)] = typeof(TypeConverter);
					hashtable[typeof(CultureInfo)] = typeof(CultureInfoConverter);
					hashtable[typeof(DateTime)] = typeof(DateTimeConverter);
					hashtable[typeof(DateTimeOffset)] = typeof(DateTimeOffsetConverter);
					hashtable[typeof(decimal)] = typeof(DecimalConverter);
					hashtable[typeof(TimeSpan)] = typeof(TimeSpanConverter);
					hashtable[typeof(Guid)] = typeof(GuidConverter);
					hashtable[typeof(Array)] = typeof(ArrayConverter);
					hashtable[typeof(ICollection)] = typeof(CollectionConverter);
					hashtable[typeof(Enum)] = typeof(EnumConverter);
					hashtable[ReflectTypeDescriptionProvider._intrinsicReferenceKey] = typeof(ReferenceConverter);
					hashtable[ReflectTypeDescriptionProvider._intrinsicNullableKey] = typeof(NullableConverter);
					ReflectTypeDescriptionProvider._intrinsicTypeConverters = hashtable;
				}
				return ReflectTypeDescriptionProvider._intrinsicTypeConverters;
			}
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x000EA7C8 File Offset: 0x000E89C8
		internal static void AddEditorTable(Type editorBaseType, Hashtable table)
		{
			if (editorBaseType == null)
			{
				throw new ArgumentNullException("editorBaseType");
			}
			object internalSyncObject = ReflectTypeDescriptionProvider._internalSyncObject;
			lock (internalSyncObject)
			{
				if (ReflectTypeDescriptionProvider._editorTables == null)
				{
					ReflectTypeDescriptionProvider._editorTables = new Hashtable(4);
				}
				if (!ReflectTypeDescriptionProvider._editorTables.ContainsKey(editorBaseType))
				{
					ReflectTypeDescriptionProvider._editorTables[editorBaseType] = table;
				}
			}
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x000EA84C File Offset: 0x000E8A4C
		public override object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
		{
			object obj;
			if (argTypes != null)
			{
				obj = SecurityUtils.SecureConstructorInvoke(objectType, argTypes, args, true, BindingFlags.ExactBinding);
			}
			else
			{
				if (args != null)
				{
					argTypes = new Type[args.Length];
					for (int i = 0; i < args.Length; i++)
					{
						if (args[i] != null)
						{
							argTypes[i] = args[i].GetType();
						}
						else
						{
							argTypes[i] = typeof(object);
						}
					}
				}
				else
				{
					argTypes = new Type[0];
				}
				obj = SecurityUtils.SecureConstructorInvoke(objectType, argTypes, args, true);
			}
			if (obj == null)
			{
				obj = SecurityUtils.SecureCreateInstance(objectType, args);
			}
			return obj;
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x000EA8D4 File Offset: 0x000E8AD4
		private static object CreateInstance(Type objectType, Type callingType)
		{
			object obj = SecurityUtils.SecureConstructorInvoke(objectType, ReflectTypeDescriptionProvider._typeConstructor, new object[] { callingType }, false);
			if (obj == null)
			{
				obj = SecurityUtils.SecureCreateInstance(objectType);
			}
			return obj;
		}

		// Token: 0x060035C7 RID: 13767 RVA: 0x000EA904 File Offset: 0x000E8B04
		internal AttributeCollection GetAttributes(Type type)
		{
			ReflectTypeDescriptionProvider.ReflectedTypeData typeData = this.GetTypeData(type, true);
			return typeData.GetAttributes();
		}

		// Token: 0x060035C8 RID: 13768 RVA: 0x000EA920 File Offset: 0x000E8B20
		public override IDictionary GetCache(object instance)
		{
			IComponent component = instance as IComponent;
			if (component != null && component.Site != null)
			{
				IDictionaryService dictionaryService = component.Site.GetService(typeof(IDictionaryService)) as IDictionaryService;
				if (dictionaryService != null)
				{
					IDictionary dictionary = dictionaryService.GetValue(ReflectTypeDescriptionProvider._dictionaryKey) as IDictionary;
					if (dictionary == null)
					{
						dictionary = new Hashtable();
						dictionaryService.SetValue(ReflectTypeDescriptionProvider._dictionaryKey, dictionary);
					}
					return dictionary;
				}
			}
			return null;
		}

		// Token: 0x060035C9 RID: 13769 RVA: 0x000EA988 File Offset: 0x000E8B88
		internal string GetClassName(Type type)
		{
			ReflectTypeDescriptionProvider.ReflectedTypeData typeData = this.GetTypeData(type, true);
			return typeData.GetClassName(null);
		}

		// Token: 0x060035CA RID: 13770 RVA: 0x000EA9A8 File Offset: 0x000E8BA8
		internal string GetComponentName(Type type, object instance)
		{
			ReflectTypeDescriptionProvider.ReflectedTypeData typeData = this.GetTypeData(type, true);
			return typeData.GetComponentName(instance);
		}

		// Token: 0x060035CB RID: 13771 RVA: 0x000EA9C8 File Offset: 0x000E8BC8
		internal TypeConverter GetConverter(Type type, object instance)
		{
			ReflectTypeDescriptionProvider.ReflectedTypeData typeData = this.GetTypeData(type, true);
			return typeData.GetConverter(instance);
		}

		// Token: 0x060035CC RID: 13772 RVA: 0x000EA9E8 File Offset: 0x000E8BE8
		internal EventDescriptor GetDefaultEvent(Type type, object instance)
		{
			ReflectTypeDescriptionProvider.ReflectedTypeData typeData = this.GetTypeData(type, true);
			return typeData.GetDefaultEvent(instance);
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x000EAA08 File Offset: 0x000E8C08
		internal PropertyDescriptor GetDefaultProperty(Type type, object instance)
		{
			ReflectTypeDescriptionProvider.ReflectedTypeData typeData = this.GetTypeData(type, true);
			return typeData.GetDefaultProperty(instance);
		}

		// Token: 0x060035CE RID: 13774 RVA: 0x000EAA28 File Offset: 0x000E8C28
		internal object GetEditor(Type type, object instance, Type editorBaseType)
		{
			ReflectTypeDescriptionProvider.ReflectedTypeData typeData = this.GetTypeData(type, true);
			return typeData.GetEditor(instance, editorBaseType);
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x000EAA48 File Offset: 0x000E8C48
		private static Hashtable GetEditorTable(Type editorBaseType)
		{
			if (ReflectTypeDescriptionProvider._editorTables == null)
			{
				object internalSyncObject = ReflectTypeDescriptionProvider._internalSyncObject;
				lock (internalSyncObject)
				{
					if (ReflectTypeDescriptionProvider._editorTables == null)
					{
						ReflectTypeDescriptionProvider._editorTables = new Hashtable(4);
					}
				}
			}
			object obj = ReflectTypeDescriptionProvider._editorTables[editorBaseType];
			if (obj == null)
			{
				RuntimeHelpers.RunClassConstructor(editorBaseType.TypeHandle);
				obj = ReflectTypeDescriptionProvider._editorTables[editorBaseType];
				if (obj == null)
				{
					object internalSyncObject2 = ReflectTypeDescriptionProvider._internalSyncObject;
					lock (internalSyncObject2)
					{
						obj = ReflectTypeDescriptionProvider._editorTables[editorBaseType];
						if (obj == null)
						{
							ReflectTypeDescriptionProvider._editorTables[editorBaseType] = ReflectTypeDescriptionProvider._editorTables;
						}
					}
				}
			}
			if (obj == ReflectTypeDescriptionProvider._editorTables)
			{
				obj = null;
			}
			return (Hashtable)obj;
		}

		// Token: 0x060035D0 RID: 13776 RVA: 0x000EAB30 File Offset: 0x000E8D30
		internal EventDescriptorCollection GetEvents(Type type)
		{
			ReflectTypeDescriptionProvider.ReflectedTypeData typeData = this.GetTypeData(type, true);
			return typeData.GetEvents();
		}

		// Token: 0x060035D1 RID: 13777 RVA: 0x000EAB4C File Offset: 0x000E8D4C
		internal AttributeCollection GetExtendedAttributes(object instance)
		{
			return AttributeCollection.Empty;
		}

		// Token: 0x060035D2 RID: 13778 RVA: 0x000EAB53 File Offset: 0x000E8D53
		internal string GetExtendedClassName(object instance)
		{
			return this.GetClassName(instance.GetType());
		}

		// Token: 0x060035D3 RID: 13779 RVA: 0x000EAB61 File Offset: 0x000E8D61
		internal string GetExtendedComponentName(object instance)
		{
			return this.GetComponentName(instance.GetType(), instance);
		}

		// Token: 0x060035D4 RID: 13780 RVA: 0x000EAB70 File Offset: 0x000E8D70
		internal TypeConverter GetExtendedConverter(object instance)
		{
			return this.GetConverter(instance.GetType(), instance);
		}

		// Token: 0x060035D5 RID: 13781 RVA: 0x000EAB7F File Offset: 0x000E8D7F
		internal EventDescriptor GetExtendedDefaultEvent(object instance)
		{
			return null;
		}

		// Token: 0x060035D6 RID: 13782 RVA: 0x000EAB82 File Offset: 0x000E8D82
		internal PropertyDescriptor GetExtendedDefaultProperty(object instance)
		{
			return null;
		}

		// Token: 0x060035D7 RID: 13783 RVA: 0x000EAB85 File Offset: 0x000E8D85
		internal object GetExtendedEditor(object instance, Type editorBaseType)
		{
			return this.GetEditor(instance.GetType(), instance, editorBaseType);
		}

		// Token: 0x060035D8 RID: 13784 RVA: 0x000EAB95 File Offset: 0x000E8D95
		internal EventDescriptorCollection GetExtendedEvents(object instance)
		{
			return EventDescriptorCollection.Empty;
		}

		// Token: 0x060035D9 RID: 13785 RVA: 0x000EAB9C File Offset: 0x000E8D9C
		internal PropertyDescriptorCollection GetExtendedProperties(object instance)
		{
			Type type = instance.GetType();
			IExtenderProvider[] extenderProviders = this.GetExtenderProviders(instance);
			IDictionary cache = TypeDescriptor.GetCache(instance);
			if (extenderProviders.Length == 0)
			{
				return PropertyDescriptorCollection.Empty;
			}
			PropertyDescriptorCollection propertyDescriptorCollection = null;
			if (cache != null)
			{
				propertyDescriptorCollection = cache[ReflectTypeDescriptionProvider._extenderPropertiesKey] as PropertyDescriptorCollection;
			}
			if (propertyDescriptorCollection != null)
			{
				return propertyDescriptorCollection;
			}
			ArrayList arrayList = null;
			for (int i = 0; i < extenderProviders.Length; i++)
			{
				PropertyDescriptor[] array = ReflectTypeDescriptionProvider.ReflectGetExtendedProperties(extenderProviders[i]);
				if (arrayList == null)
				{
					arrayList = new ArrayList(array.Length * extenderProviders.Length);
				}
				foreach (PropertyDescriptor propertyDescriptor in array)
				{
					ExtenderProvidedPropertyAttribute extenderProvidedPropertyAttribute = propertyDescriptor.Attributes[typeof(ExtenderProvidedPropertyAttribute)] as ExtenderProvidedPropertyAttribute;
					if (extenderProvidedPropertyAttribute != null)
					{
						Type receiverType = extenderProvidedPropertyAttribute.ReceiverType;
						if (receiverType != null && receiverType.IsAssignableFrom(type))
						{
							arrayList.Add(propertyDescriptor);
						}
					}
				}
			}
			if (arrayList != null)
			{
				PropertyDescriptor[] array2 = new PropertyDescriptor[arrayList.Count];
				arrayList.CopyTo(array2, 0);
				propertyDescriptorCollection = new PropertyDescriptorCollection(array2, true);
			}
			else
			{
				propertyDescriptorCollection = PropertyDescriptorCollection.Empty;
			}
			if (cache != null)
			{
				cache[ReflectTypeDescriptionProvider._extenderPropertiesKey] = propertyDescriptorCollection;
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x060035DA RID: 13786 RVA: 0x000EACC8 File Offset: 0x000E8EC8
		protected internal override IExtenderProvider[] GetExtenderProviders(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			IComponent component = instance as IComponent;
			if (component != null && component.Site != null)
			{
				IExtenderListService extenderListService = component.Site.GetService(typeof(IExtenderListService)) as IExtenderListService;
				IDictionary cache = TypeDescriptor.GetCache(instance);
				if (extenderListService != null)
				{
					return ReflectTypeDescriptionProvider.GetExtenders(extenderListService.GetExtenderProviders(), instance, cache);
				}
				IContainer container = component.Site.Container;
				if (container != null)
				{
					return ReflectTypeDescriptionProvider.GetExtenders(container.Components, instance, cache);
				}
			}
			return new IExtenderProvider[0];
		}

		// Token: 0x060035DB RID: 13787 RVA: 0x000EAD4C File Offset: 0x000E8F4C
		private static IExtenderProvider[] GetExtenders(ICollection components, object instance, IDictionary cache)
		{
			bool flag = false;
			int num = 0;
			IExtenderProvider[] array = null;
			ulong num2 = 0UL;
			int num3 = 64;
			IExtenderProvider[] array2 = components as IExtenderProvider[];
			if (cache != null)
			{
				array = cache[ReflectTypeDescriptionProvider._extenderProviderKey] as IExtenderProvider[];
			}
			if (array == null)
			{
				flag = true;
			}
			int i = 0;
			int num4 = 0;
			if (array2 != null)
			{
				for (i = 0; i < array2.Length; i++)
				{
					if (array2[i].CanExtend(instance))
					{
						num++;
						if (i < num3)
						{
							num2 |= 1UL << i;
						}
						if (!flag && (num4 >= array.Length || array2[i] != array[num4++]))
						{
							flag = true;
						}
					}
				}
			}
			else if (components != null)
			{
				foreach (object obj in components)
				{
					IExtenderProvider extenderProvider = obj as IExtenderProvider;
					if (extenderProvider != null && extenderProvider.CanExtend(instance))
					{
						num++;
						if (i < num3)
						{
							num2 |= 1UL << i;
						}
						if (!flag && (num4 >= array.Length || extenderProvider != array[num4++]))
						{
							flag = true;
						}
					}
					i++;
				}
			}
			if (array != null && num != array.Length)
			{
				flag = true;
			}
			if (flag)
			{
				if (array2 == null || num != array2.Length)
				{
					IExtenderProvider[] array3 = new IExtenderProvider[num];
					i = 0;
					num4 = 0;
					if (array2 != null && num > 0)
					{
						while (i < array2.Length)
						{
							if ((i < num3 && (num2 & (1UL << i)) != 0UL) || (i >= num3 && array2[i].CanExtend(instance)))
							{
								array3[num4++] = array2[i];
							}
							i++;
						}
					}
					else if (num > 0)
					{
						foreach (object obj2 in components)
						{
							IExtenderProvider extenderProvider2 = obj2 as IExtenderProvider;
							if (extenderProvider2 != null && ((i < num3 && (num2 & (1UL << i)) != 0UL) || (i >= num3 && extenderProvider2.CanExtend(instance))))
							{
								array3[num4++] = extenderProvider2;
							}
							i++;
						}
					}
					array2 = array3;
				}
				if (cache != null)
				{
					cache[ReflectTypeDescriptionProvider._extenderProviderKey] = array2;
					cache.Remove(ReflectTypeDescriptionProvider._extenderPropertiesKey);
				}
			}
			else
			{
				array2 = array;
			}
			return array2;
		}

		// Token: 0x060035DC RID: 13788 RVA: 0x000EAF88 File Offset: 0x000E9188
		internal object GetExtendedPropertyOwner(object instance, PropertyDescriptor pd)
		{
			return this.GetPropertyOwner(instance.GetType(), instance, pd);
		}

		// Token: 0x060035DD RID: 13789 RVA: 0x000EAF98 File Offset: 0x000E9198
		public override ICustomTypeDescriptor GetExtendedTypeDescriptor(object instance)
		{
			return null;
		}

		// Token: 0x060035DE RID: 13790 RVA: 0x000EAF9C File Offset: 0x000E919C
		public override string GetFullComponentName(object component)
		{
			IComponent component2 = component as IComponent;
			if (component2 != null)
			{
				INestedSite nestedSite = component2.Site as INestedSite;
				if (nestedSite != null)
				{
					return nestedSite.FullName;
				}
			}
			return TypeDescriptor.GetComponentName(component);
		}

		// Token: 0x060035DF RID: 13791 RVA: 0x000EAFD0 File Offset: 0x000E91D0
		internal Type[] GetPopulatedTypes(Module module)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this._typeData)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				Type type = (Type)dictionaryEntry.Key;
				ReflectTypeDescriptionProvider.ReflectedTypeData reflectedTypeData = (ReflectTypeDescriptionProvider.ReflectedTypeData)dictionaryEntry.Value;
				if (type.Module == module && reflectedTypeData.IsPopulated)
				{
					arrayList.Add(type);
				}
			}
			return (Type[])arrayList.ToArray(typeof(Type));
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x000EB07C File Offset: 0x000E927C
		internal PropertyDescriptorCollection GetProperties(Type type)
		{
			ReflectTypeDescriptionProvider.ReflectedTypeData typeData = this.GetTypeData(type, true);
			return typeData.GetProperties();
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x000EB098 File Offset: 0x000E9298
		internal object GetPropertyOwner(Type type, object instance, PropertyDescriptor pd)
		{
			return TypeDescriptor.GetAssociation(type, instance);
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x000EB0A1 File Offset: 0x000E92A1
		public override Type GetReflectionType(Type objectType, object instance)
		{
			return objectType;
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x000EB0A4 File Offset: 0x000E92A4
		private ReflectTypeDescriptionProvider.ReflectedTypeData GetTypeData(Type type, bool createIfNeeded)
		{
			ReflectTypeDescriptionProvider.ReflectedTypeData reflectedTypeData = null;
			if (this._typeData != null)
			{
				reflectedTypeData = (ReflectTypeDescriptionProvider.ReflectedTypeData)this._typeData[type];
				if (reflectedTypeData != null)
				{
					return reflectedTypeData;
				}
			}
			object internalSyncObject = ReflectTypeDescriptionProvider._internalSyncObject;
			lock (internalSyncObject)
			{
				if (this._typeData != null)
				{
					reflectedTypeData = (ReflectTypeDescriptionProvider.ReflectedTypeData)this._typeData[type];
				}
				if (reflectedTypeData == null && createIfNeeded)
				{
					reflectedTypeData = new ReflectTypeDescriptionProvider.ReflectedTypeData(type);
					if (this._typeData == null)
					{
						this._typeData = new Hashtable();
					}
					this._typeData[type] = reflectedTypeData;
				}
			}
			return reflectedTypeData;
		}

		// Token: 0x060035E4 RID: 13796 RVA: 0x000EB148 File Offset: 0x000E9348
		public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
		{
			return null;
		}

		// Token: 0x060035E5 RID: 13797 RVA: 0x000EB14C File Offset: 0x000E934C
		private static Type GetTypeFromName(string typeName)
		{
			Type type = Type.GetType(typeName);
			if (type == null)
			{
				int num = typeName.IndexOf(',');
				if (num != -1)
				{
					type = Type.GetType(typeName.Substring(0, num));
				}
			}
			return type;
		}

		// Token: 0x060035E6 RID: 13798 RVA: 0x000EB188 File Offset: 0x000E9388
		internal bool IsPopulated(Type type)
		{
			ReflectTypeDescriptionProvider.ReflectedTypeData typeData = this.GetTypeData(type, false);
			return typeData != null && typeData.IsPopulated;
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x000EB1AC File Offset: 0x000E93AC
		private static Attribute[] ReflectGetAttributes(Type type)
		{
			if (ReflectTypeDescriptionProvider._attributeCache == null)
			{
				object internalSyncObject = ReflectTypeDescriptionProvider._internalSyncObject;
				lock (internalSyncObject)
				{
					if (ReflectTypeDescriptionProvider._attributeCache == null)
					{
						ReflectTypeDescriptionProvider._attributeCache = new Hashtable();
					}
				}
			}
			Attribute[] array = (Attribute[])ReflectTypeDescriptionProvider._attributeCache[type];
			if (array != null)
			{
				return array;
			}
			object internalSyncObject2 = ReflectTypeDescriptionProvider._internalSyncObject;
			lock (internalSyncObject2)
			{
				array = (Attribute[])ReflectTypeDescriptionProvider._attributeCache[type];
				if (array == null)
				{
					object[] customAttributes = type.GetCustomAttributes(typeof(Attribute), false);
					array = new Attribute[customAttributes.Length];
					customAttributes.CopyTo(array, 0);
					ReflectTypeDescriptionProvider._attributeCache[type] = array;
				}
			}
			return array;
		}

		// Token: 0x060035E8 RID: 13800 RVA: 0x000EB290 File Offset: 0x000E9490
		internal static Attribute[] ReflectGetAttributes(MemberInfo member)
		{
			if (ReflectTypeDescriptionProvider._attributeCache == null)
			{
				object internalSyncObject = ReflectTypeDescriptionProvider._internalSyncObject;
				lock (internalSyncObject)
				{
					if (ReflectTypeDescriptionProvider._attributeCache == null)
					{
						ReflectTypeDescriptionProvider._attributeCache = new Hashtable();
					}
				}
			}
			Attribute[] array = (Attribute[])ReflectTypeDescriptionProvider._attributeCache[member];
			if (array != null)
			{
				return array;
			}
			object internalSyncObject2 = ReflectTypeDescriptionProvider._internalSyncObject;
			lock (internalSyncObject2)
			{
				array = (Attribute[])ReflectTypeDescriptionProvider._attributeCache[member];
				if (array == null)
				{
					object[] customAttributes = member.GetCustomAttributes(typeof(Attribute), false);
					array = new Attribute[customAttributes.Length];
					customAttributes.CopyTo(array, 0);
					ReflectTypeDescriptionProvider._attributeCache[member] = array;
				}
			}
			return array;
		}

		// Token: 0x060035E9 RID: 13801 RVA: 0x000EB374 File Offset: 0x000E9574
		private static EventDescriptor[] ReflectGetEvents(Type type)
		{
			if (ReflectTypeDescriptionProvider._eventCache == null)
			{
				object internalSyncObject = ReflectTypeDescriptionProvider._internalSyncObject;
				lock (internalSyncObject)
				{
					if (ReflectTypeDescriptionProvider._eventCache == null)
					{
						ReflectTypeDescriptionProvider._eventCache = new Hashtable();
					}
				}
			}
			EventDescriptor[] array = (EventDescriptor[])ReflectTypeDescriptionProvider._eventCache[type];
			if (array != null)
			{
				return array;
			}
			object internalSyncObject2 = ReflectTypeDescriptionProvider._internalSyncObject;
			lock (internalSyncObject2)
			{
				array = (EventDescriptor[])ReflectTypeDescriptionProvider._eventCache[type];
				if (array == null)
				{
					BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
					EventInfo[] events = type.GetEvents(bindingFlags);
					array = new EventDescriptor[events.Length];
					int num = 0;
					foreach (EventInfo eventInfo in events)
					{
						if (eventInfo.DeclaringType.IsPublic || eventInfo.DeclaringType.IsNestedPublic || !(eventInfo.DeclaringType.Assembly == typeof(ReflectTypeDescriptionProvider).Assembly))
						{
							MethodInfo addMethod = eventInfo.GetAddMethod();
							MethodInfo removeMethod = eventInfo.GetRemoveMethod();
							if (addMethod != null && removeMethod != null)
							{
								array[num++] = new ReflectEventDescriptor(type, eventInfo);
							}
						}
					}
					if (num != array.Length)
					{
						EventDescriptor[] array2 = new EventDescriptor[num];
						Array.Copy(array, 0, array2, 0, num);
						array = array2;
					}
					ReflectTypeDescriptionProvider._eventCache[type] = array;
				}
			}
			return array;
		}

		// Token: 0x060035EA RID: 13802 RVA: 0x000EB51C File Offset: 0x000E971C
		private static PropertyDescriptor[] ReflectGetExtendedProperties(IExtenderProvider provider)
		{
			IDictionary cache = TypeDescriptor.GetCache(provider);
			PropertyDescriptor[] array;
			if (cache != null)
			{
				array = cache[ReflectTypeDescriptionProvider._extenderProviderPropertiesKey] as PropertyDescriptor[];
				if (array != null)
				{
					return array;
				}
			}
			if (ReflectTypeDescriptionProvider._extendedPropertyCache == null)
			{
				object internalSyncObject = ReflectTypeDescriptionProvider._internalSyncObject;
				lock (internalSyncObject)
				{
					if (ReflectTypeDescriptionProvider._extendedPropertyCache == null)
					{
						ReflectTypeDescriptionProvider._extendedPropertyCache = new Hashtable();
					}
				}
			}
			Type type = provider.GetType();
			ReflectPropertyDescriptor[] array2 = (ReflectPropertyDescriptor[])ReflectTypeDescriptionProvider._extendedPropertyCache[type];
			if (array2 == null)
			{
				object internalSyncObject2 = ReflectTypeDescriptionProvider._internalSyncObject;
				lock (internalSyncObject2)
				{
					array2 = (ReflectPropertyDescriptor[])ReflectTypeDescriptionProvider._extendedPropertyCache[type];
					if (array2 == null)
					{
						AttributeCollection attributes = TypeDescriptor.GetAttributes(type);
						ArrayList arrayList = new ArrayList(attributes.Count);
						foreach (object obj in attributes)
						{
							Attribute attribute = (Attribute)obj;
							ProvidePropertyAttribute providePropertyAttribute = attribute as ProvidePropertyAttribute;
							if (providePropertyAttribute != null)
							{
								Type typeFromName = ReflectTypeDescriptionProvider.GetTypeFromName(providePropertyAttribute.ReceiverTypeName);
								if (typeFromName != null)
								{
									MethodInfo method = type.GetMethod("Get" + providePropertyAttribute.PropertyName, new Type[] { typeFromName });
									if (method != null && !method.IsStatic && method.IsPublic)
									{
										MethodInfo methodInfo = type.GetMethod("Set" + providePropertyAttribute.PropertyName, new Type[] { typeFromName, method.ReturnType });
										if (methodInfo != null && (methodInfo.IsStatic || !methodInfo.IsPublic))
										{
											methodInfo = null;
										}
										arrayList.Add(new ReflectPropertyDescriptor(type, providePropertyAttribute.PropertyName, method.ReturnType, typeFromName, method, methodInfo, null));
									}
								}
							}
						}
						array2 = new ReflectPropertyDescriptor[arrayList.Count];
						arrayList.CopyTo(array2, 0);
						ReflectTypeDescriptionProvider._extendedPropertyCache[type] = array2;
					}
				}
			}
			array = new PropertyDescriptor[array2.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				Attribute[] array3 = null;
				IComponent component = provider as IComponent;
				if (component == null || component.Site == null)
				{
					array3 = new Attribute[] { DesignOnlyAttribute.Yes };
				}
				ReflectPropertyDescriptor reflectPropertyDescriptor = array2[i];
				ExtendedPropertyDescriptor extendedPropertyDescriptor = new ExtendedPropertyDescriptor(reflectPropertyDescriptor, reflectPropertyDescriptor.ExtenderGetReceiverType(), provider, array3);
				array[i] = extendedPropertyDescriptor;
			}
			if (cache != null)
			{
				cache[ReflectTypeDescriptionProvider._extenderProviderPropertiesKey] = array;
			}
			return array;
		}

		// Token: 0x060035EB RID: 13803 RVA: 0x000EB804 File Offset: 0x000E9A04
		private static PropertyDescriptor[] ReflectGetProperties(Type type)
		{
			if (ReflectTypeDescriptionProvider._propertyCache == null)
			{
				object internalSyncObject = ReflectTypeDescriptionProvider._internalSyncObject;
				lock (internalSyncObject)
				{
					if (ReflectTypeDescriptionProvider._propertyCache == null)
					{
						ReflectTypeDescriptionProvider._propertyCache = new Hashtable();
					}
				}
			}
			PropertyDescriptor[] array = (PropertyDescriptor[])ReflectTypeDescriptionProvider._propertyCache[type];
			if (array != null)
			{
				return array;
			}
			object internalSyncObject2 = ReflectTypeDescriptionProvider._internalSyncObject;
			lock (internalSyncObject2)
			{
				array = (PropertyDescriptor[])ReflectTypeDescriptionProvider._propertyCache[type];
				if (array == null)
				{
					BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
					PropertyInfo[] properties = type.GetProperties(bindingFlags);
					array = new PropertyDescriptor[properties.Length];
					int num = 0;
					foreach (PropertyInfo propertyInfo in properties)
					{
						if (propertyInfo.GetIndexParameters().Length == 0)
						{
							MethodInfo getMethod = propertyInfo.GetGetMethod();
							MethodInfo setMethod = propertyInfo.GetSetMethod();
							string name = propertyInfo.Name;
							if (getMethod != null)
							{
								array[num++] = new ReflectPropertyDescriptor(type, name, propertyInfo.PropertyType, propertyInfo, getMethod, setMethod, null);
							}
						}
					}
					if (num != array.Length)
					{
						PropertyDescriptor[] array2 = new PropertyDescriptor[num];
						Array.Copy(array, 0, array2, 0, num);
						array = array2;
					}
					ReflectTypeDescriptionProvider._propertyCache[type] = array;
				}
			}
			return array;
		}

		// Token: 0x060035EC RID: 13804 RVA: 0x000EB968 File Offset: 0x000E9B68
		internal void Refresh(Type type)
		{
			ReflectTypeDescriptionProvider.ReflectedTypeData typeData = this.GetTypeData(type, false);
			if (typeData != null)
			{
				typeData.Refresh();
			}
		}

		// Token: 0x060035ED RID: 13805 RVA: 0x000EB988 File Offset: 0x000E9B88
		private static object SearchIntrinsicTable(Hashtable table, Type callingType)
		{
			object obj = null;
			lock (table)
			{
				Type type = callingType;
				while (type != null && type != typeof(object))
				{
					obj = table[type];
					string text = obj as string;
					if (text != null)
					{
						obj = Type.GetType(text);
						if (obj != null)
						{
							table[type] = obj;
						}
					}
					if (obj != null)
					{
						break;
					}
					type = type.BaseType;
				}
				if (obj == null)
				{
					foreach (object obj2 in table)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
						Type type2 = dictionaryEntry.Key as Type;
						if (type2 != null && type2.IsInterface && type2.IsAssignableFrom(callingType))
						{
							obj = dictionaryEntry.Value;
							string text2 = obj as string;
							if (text2 != null)
							{
								obj = Type.GetType(text2);
								if (obj != null)
								{
									table[callingType] = obj;
								}
							}
							if (obj != null)
							{
								break;
							}
						}
					}
				}
				if (obj == null)
				{
					if (callingType.IsGenericType && callingType.GetGenericTypeDefinition() == typeof(Nullable<>))
					{
						obj = table[ReflectTypeDescriptionProvider._intrinsicNullableKey];
					}
					else if (callingType.IsInterface)
					{
						obj = table[ReflectTypeDescriptionProvider._intrinsicReferenceKey];
					}
				}
				if (obj == null)
				{
					obj = table[typeof(object)];
				}
				Type type3 = obj as Type;
				if (type3 != null)
				{
					obj = ReflectTypeDescriptionProvider.CreateInstance(type3, callingType);
					if (type3.GetConstructor(ReflectTypeDescriptionProvider._typeConstructor) == null)
					{
						table[callingType] = obj;
					}
				}
			}
			return obj;
		}

		// Token: 0x04002A68 RID: 10856
		private Hashtable _typeData;

		// Token: 0x04002A69 RID: 10857
		private static Type[] _typeConstructor = new Type[] { typeof(Type) };

		// Token: 0x04002A6A RID: 10858
		private static volatile Hashtable _editorTables;

		// Token: 0x04002A6B RID: 10859
		private static volatile Hashtable _intrinsicTypeConverters;

		// Token: 0x04002A6C RID: 10860
		private static object _intrinsicReferenceKey = new object();

		// Token: 0x04002A6D RID: 10861
		private static object _intrinsicNullableKey = new object();

		// Token: 0x04002A6E RID: 10862
		private static object _dictionaryKey = new object();

		// Token: 0x04002A6F RID: 10863
		private static volatile Hashtable _propertyCache;

		// Token: 0x04002A70 RID: 10864
		private static volatile Hashtable _eventCache;

		// Token: 0x04002A71 RID: 10865
		private static volatile Hashtable _attributeCache;

		// Token: 0x04002A72 RID: 10866
		private static volatile Hashtable _extendedPropertyCache;

		// Token: 0x04002A73 RID: 10867
		private static readonly Guid _extenderProviderKey = Guid.NewGuid();

		// Token: 0x04002A74 RID: 10868
		private static readonly Guid _extenderPropertiesKey = Guid.NewGuid();

		// Token: 0x04002A75 RID: 10869
		private static readonly Guid _extenderProviderPropertiesKey = Guid.NewGuid();

		// Token: 0x04002A76 RID: 10870
		private static readonly Type[] _skipInterfaceAttributeList = new Type[]
		{
			typeof(GuidAttribute),
			typeof(ComVisibleAttribute),
			typeof(InterfaceTypeAttribute)
		};

		// Token: 0x04002A77 RID: 10871
		private static object _internalSyncObject = new object();

		// Token: 0x0200089A RID: 2202
		private class ReflectedTypeData
		{
			// Token: 0x0600458C RID: 17804 RVA: 0x001227C8 File Offset: 0x001209C8
			internal ReflectedTypeData(Type type)
			{
				this._type = type;
			}

			// Token: 0x17000FBF RID: 4031
			// (get) Token: 0x0600458D RID: 17805 RVA: 0x001227D7 File Offset: 0x001209D7
			internal bool IsPopulated
			{
				get
				{
					return (this._attributes != null) | (this._events != null) | (this._properties != null);
				}
			}

			// Token: 0x0600458E RID: 17806 RVA: 0x001227F8 File Offset: 0x001209F8
			internal AttributeCollection GetAttributes()
			{
				if (this._attributes == null)
				{
					Attribute[] array = ReflectTypeDescriptionProvider.ReflectGetAttributes(this._type);
					Type type = this._type.BaseType;
					while (type != null && type != typeof(object))
					{
						Attribute[] array2 = ReflectTypeDescriptionProvider.ReflectGetAttributes(type);
						Attribute[] array3 = new Attribute[array.Length + array2.Length];
						Array.Copy(array, 0, array3, 0, array.Length);
						Array.Copy(array2, 0, array3, array.Length, array2.Length);
						array = array3;
						type = type.BaseType;
					}
					int num = array.Length;
					foreach (Type type2 in this._type.GetInterfaces())
					{
						if ((type2.Attributes & TypeAttributes.NestedPrivate) != TypeAttributes.NotPublic)
						{
							AttributeCollection attributes = TypeDescriptor.GetAttributes(type2);
							if (attributes.Count > 0)
							{
								Attribute[] array4 = new Attribute[array.Length + attributes.Count];
								Array.Copy(array, 0, array4, 0, array.Length);
								attributes.CopyTo(array4, array.Length);
								array = array4;
							}
						}
					}
					OrderedDictionary orderedDictionary = new OrderedDictionary(array.Length);
					for (int j = 0; j < array.Length; j++)
					{
						bool flag = true;
						if (j >= num)
						{
							for (int k = 0; k < ReflectTypeDescriptionProvider._skipInterfaceAttributeList.Length; k++)
							{
								if (ReflectTypeDescriptionProvider._skipInterfaceAttributeList[k].IsInstanceOfType(array[j]))
								{
									flag = false;
									break;
								}
							}
						}
						if (flag && !orderedDictionary.Contains(array[j].TypeId))
						{
							orderedDictionary[array[j].TypeId] = array[j];
						}
					}
					array = new Attribute[orderedDictionary.Count];
					orderedDictionary.Values.CopyTo(array, 0);
					this._attributes = new AttributeCollection(array);
				}
				return this._attributes;
			}

			// Token: 0x0600458F RID: 17807 RVA: 0x001229A1 File Offset: 0x00120BA1
			internal string GetClassName(object instance)
			{
				return this._type.FullName;
			}

			// Token: 0x06004590 RID: 17808 RVA: 0x001229B0 File Offset: 0x00120BB0
			internal string GetComponentName(object instance)
			{
				IComponent component = instance as IComponent;
				if (component != null)
				{
					ISite site = component.Site;
					if (site != null)
					{
						INestedSite nestedSite = site as INestedSite;
						if (nestedSite != null)
						{
							return nestedSite.FullName;
						}
						return site.Name;
					}
				}
				return null;
			}

			// Token: 0x06004591 RID: 17809 RVA: 0x001229EC File Offset: 0x00120BEC
			internal TypeConverter GetConverter(object instance)
			{
				TypeConverterAttribute typeConverterAttribute = null;
				if (instance != null)
				{
					typeConverterAttribute = (TypeConverterAttribute)TypeDescriptor.GetAttributes(this._type)[typeof(TypeConverterAttribute)];
					TypeConverterAttribute typeConverterAttribute2 = (TypeConverterAttribute)TypeDescriptor.GetAttributes(instance)[typeof(TypeConverterAttribute)];
					if (typeConverterAttribute != typeConverterAttribute2)
					{
						Type typeFromName = this.GetTypeFromName(typeConverterAttribute2.ConverterTypeName);
						if (typeFromName != null && typeof(TypeConverter).IsAssignableFrom(typeFromName))
						{
							try
							{
								IntSecurity.FullReflection.Assert();
								return (TypeConverter)ReflectTypeDescriptionProvider.CreateInstance(typeFromName, this._type);
							}
							finally
							{
								CodeAccessPermission.RevertAssert();
							}
						}
					}
				}
				if (this._converter == null)
				{
					if (typeConverterAttribute == null)
					{
						typeConverterAttribute = (TypeConverterAttribute)TypeDescriptor.GetAttributes(this._type)[typeof(TypeConverterAttribute)];
					}
					if (typeConverterAttribute != null)
					{
						Type typeFromName2 = this.GetTypeFromName(typeConverterAttribute.ConverterTypeName);
						if (typeFromName2 != null && typeof(TypeConverter).IsAssignableFrom(typeFromName2))
						{
							try
							{
								IntSecurity.FullReflection.Assert();
								this._converter = (TypeConverter)ReflectTypeDescriptionProvider.CreateInstance(typeFromName2, this._type);
							}
							finally
							{
								CodeAccessPermission.RevertAssert();
							}
						}
					}
					if (this._converter == null)
					{
						this._converter = (TypeConverter)ReflectTypeDescriptionProvider.SearchIntrinsicTable(ReflectTypeDescriptionProvider.IntrinsicTypeConverters, this._type);
					}
				}
				return this._converter;
			}

			// Token: 0x06004592 RID: 17810 RVA: 0x00122B5C File Offset: 0x00120D5C
			internal EventDescriptor GetDefaultEvent(object instance)
			{
				AttributeCollection attributeCollection;
				if (instance != null)
				{
					attributeCollection = TypeDescriptor.GetAttributes(instance);
				}
				else
				{
					attributeCollection = TypeDescriptor.GetAttributes(this._type);
				}
				DefaultEventAttribute defaultEventAttribute = (DefaultEventAttribute)attributeCollection[typeof(DefaultEventAttribute)];
				if (defaultEventAttribute == null || defaultEventAttribute.Name == null)
				{
					return null;
				}
				if (instance != null)
				{
					return TypeDescriptor.GetEvents(instance)[defaultEventAttribute.Name];
				}
				return TypeDescriptor.GetEvents(this._type)[defaultEventAttribute.Name];
			}

			// Token: 0x06004593 RID: 17811 RVA: 0x00122BD0 File Offset: 0x00120DD0
			internal PropertyDescriptor GetDefaultProperty(object instance)
			{
				AttributeCollection attributeCollection;
				if (instance != null)
				{
					attributeCollection = TypeDescriptor.GetAttributes(instance);
				}
				else
				{
					attributeCollection = TypeDescriptor.GetAttributes(this._type);
				}
				DefaultPropertyAttribute defaultPropertyAttribute = (DefaultPropertyAttribute)attributeCollection[typeof(DefaultPropertyAttribute)];
				if (defaultPropertyAttribute == null || defaultPropertyAttribute.Name == null)
				{
					return null;
				}
				if (instance != null)
				{
					return TypeDescriptor.GetProperties(instance)[defaultPropertyAttribute.Name];
				}
				return TypeDescriptor.GetProperties(this._type)[defaultPropertyAttribute.Name];
			}

			// Token: 0x06004594 RID: 17812 RVA: 0x00122C44 File Offset: 0x00120E44
			internal object GetEditor(object instance, Type editorBaseType)
			{
				EditorAttribute editorAttribute;
				if (instance != null)
				{
					editorAttribute = ReflectTypeDescriptionProvider.ReflectedTypeData.GetEditorAttribute(TypeDescriptor.GetAttributes(this._type), editorBaseType);
					EditorAttribute editorAttribute2 = ReflectTypeDescriptionProvider.ReflectedTypeData.GetEditorAttribute(TypeDescriptor.GetAttributes(instance), editorBaseType);
					if (editorAttribute != editorAttribute2)
					{
						Type typeFromName = this.GetTypeFromName(editorAttribute2.EditorTypeName);
						if (typeFromName != null && editorBaseType.IsAssignableFrom(typeFromName))
						{
							return ReflectTypeDescriptionProvider.CreateInstance(typeFromName, this._type);
						}
					}
				}
				lock (this)
				{
					for (int i = 0; i < this._editorCount; i++)
					{
						if (this._editorTypes[i] == editorBaseType)
						{
							return this._editors[i];
						}
					}
				}
				object obj = null;
				editorAttribute = ReflectTypeDescriptionProvider.ReflectedTypeData.GetEditorAttribute(TypeDescriptor.GetAttributes(this._type), editorBaseType);
				if (editorAttribute != null)
				{
					Type typeFromName2 = this.GetTypeFromName(editorAttribute.EditorTypeName);
					if (typeFromName2 != null && editorBaseType.IsAssignableFrom(typeFromName2))
					{
						obj = ReflectTypeDescriptionProvider.CreateInstance(typeFromName2, this._type);
					}
				}
				if (obj == null)
				{
					Hashtable editorTable = ReflectTypeDescriptionProvider.GetEditorTable(editorBaseType);
					if (editorTable != null)
					{
						obj = ReflectTypeDescriptionProvider.SearchIntrinsicTable(editorTable, this._type);
					}
					if (obj != null && !editorBaseType.IsInstanceOfType(obj))
					{
						obj = null;
					}
				}
				if (obj != null)
				{
					lock (this)
					{
						if (this._editorTypes == null || this._editorTypes.Length == this._editorCount)
						{
							int num = ((this._editorTypes == null) ? 4 : (this._editorTypes.Length * 2));
							Type[] array = new Type[num];
							object[] array2 = new object[num];
							if (this._editorTypes != null)
							{
								this._editorTypes.CopyTo(array, 0);
								this._editors.CopyTo(array2, 0);
							}
							this._editorTypes = array;
							this._editors = array2;
							this._editorTypes[this._editorCount] = editorBaseType;
							object[] editors = this._editors;
							int editorCount = this._editorCount;
							this._editorCount = editorCount + 1;
							editors[editorCount] = obj;
						}
					}
				}
				return obj;
			}

			// Token: 0x06004595 RID: 17813 RVA: 0x00122E50 File Offset: 0x00121050
			private static EditorAttribute GetEditorAttribute(AttributeCollection attributes, Type editorBaseType)
			{
				foreach (object obj in attributes)
				{
					Attribute attribute = (Attribute)obj;
					EditorAttribute editorAttribute = attribute as EditorAttribute;
					if (editorAttribute != null)
					{
						Type type = Type.GetType(editorAttribute.EditorBaseTypeName);
						if (type != null && type == editorBaseType)
						{
							return editorAttribute;
						}
					}
				}
				return null;
			}

			// Token: 0x06004596 RID: 17814 RVA: 0x00122ED4 File Offset: 0x001210D4
			internal EventDescriptorCollection GetEvents()
			{
				if (this._events == null)
				{
					Dictionary<string, EventDescriptor> dictionary = new Dictionary<string, EventDescriptor>(16);
					Type type = this._type;
					Type typeFromHandle = typeof(object);
					EventDescriptor[] array;
					do
					{
						array = ReflectTypeDescriptionProvider.ReflectGetEvents(type);
						foreach (EventDescriptor eventDescriptor in array)
						{
							if (!dictionary.ContainsKey(eventDescriptor.Name))
							{
								dictionary.Add(eventDescriptor.Name, eventDescriptor);
							}
						}
						type = type.BaseType;
					}
					while (type != null && type != typeFromHandle);
					array = new EventDescriptor[dictionary.Count];
					dictionary.Values.CopyTo(array, 0);
					this._events = new EventDescriptorCollection(array, true);
				}
				return this._events;
			}

			// Token: 0x06004597 RID: 17815 RVA: 0x00122F90 File Offset: 0x00121190
			internal PropertyDescriptorCollection GetProperties()
			{
				if (this._properties == null)
				{
					Dictionary<string, PropertyDescriptor> dictionary = new Dictionary<string, PropertyDescriptor>(10);
					Type type = this._type;
					Type typeFromHandle = typeof(object);
					PropertyDescriptor[] array;
					do
					{
						array = ReflectTypeDescriptionProvider.ReflectGetProperties(type);
						foreach (PropertyDescriptor propertyDescriptor in array)
						{
							if (!dictionary.ContainsKey(propertyDescriptor.Name))
							{
								dictionary.Add(propertyDescriptor.Name, propertyDescriptor);
							}
						}
						type = type.BaseType;
					}
					while (type != null && type != typeFromHandle);
					array = new PropertyDescriptor[dictionary.Count];
					dictionary.Values.CopyTo(array, 0);
					this._properties = new PropertyDescriptorCollection(array, true);
				}
				return this._properties;
			}

			// Token: 0x06004598 RID: 17816 RVA: 0x0012304C File Offset: 0x0012124C
			private Type GetTypeFromName(string typeName)
			{
				if (typeName == null || typeName.Length == 0)
				{
					return null;
				}
				int num = typeName.IndexOf(',');
				Type type = null;
				if (num == -1)
				{
					type = this._type.Assembly.GetType(typeName);
				}
				if (type == null)
				{
					type = Type.GetType(typeName);
				}
				if (type == null && num != -1)
				{
					type = Type.GetType(typeName.Substring(0, num));
				}
				return type;
			}

			// Token: 0x06004599 RID: 17817 RVA: 0x001230B3 File Offset: 0x001212B3
			internal void Refresh()
			{
				this._attributes = null;
				this._events = null;
				this._properties = null;
				this._converter = null;
				this._editors = null;
				this._editorTypes = null;
				this._editorCount = 0;
			}

			// Token: 0x040037C9 RID: 14281
			private Type _type;

			// Token: 0x040037CA RID: 14282
			private AttributeCollection _attributes;

			// Token: 0x040037CB RID: 14283
			private EventDescriptorCollection _events;

			// Token: 0x040037CC RID: 14284
			private PropertyDescriptorCollection _properties;

			// Token: 0x040037CD RID: 14285
			private TypeConverter _converter;

			// Token: 0x040037CE RID: 14286
			private object[] _editors;

			// Token: 0x040037CF RID: 14287
			private Type[] _editorTypes;

			// Token: 0x040037D0 RID: 14288
			private int _editorCount;
		}
	}
}
