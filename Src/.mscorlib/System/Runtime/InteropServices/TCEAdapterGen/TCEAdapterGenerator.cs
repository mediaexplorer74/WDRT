using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices.TCEAdapterGen
{
	// Token: 0x020009C3 RID: 2499
	internal class TCEAdapterGenerator
	{
		// Token: 0x060063CF RID: 25551 RVA: 0x00155D4C File Offset: 0x00153F4C
		public void Process(ModuleBuilder ModBldr, ArrayList EventItfList)
		{
			this.m_Module = ModBldr;
			int count = EventItfList.Count;
			for (int i = 0; i < count; i++)
			{
				EventItfInfo eventItfInfo = (EventItfInfo)EventItfList[i];
				Type eventItfType = eventItfInfo.GetEventItfType();
				Type srcItfType = eventItfInfo.GetSrcItfType();
				string eventProviderName = eventItfInfo.GetEventProviderName();
				Type type = new EventSinkHelperWriter(this.m_Module, srcItfType, eventItfType).Perform();
				new EventProviderWriter(this.m_Module, eventProviderName, eventItfType, srcItfType, type).Perform();
			}
		}

		// Token: 0x060063D0 RID: 25552 RVA: 0x00155DC4 File Offset: 0x00153FC4
		internal static void SetClassInterfaceTypeToNone(TypeBuilder tb)
		{
			if (TCEAdapterGenerator.s_NoClassItfCABuilder == null)
			{
				Type[] array = new Type[] { typeof(ClassInterfaceType) };
				ConstructorInfo constructor = typeof(ClassInterfaceAttribute).GetConstructor(array);
				TCEAdapterGenerator.s_NoClassItfCABuilder = new CustomAttributeBuilder(constructor, new object[] { ClassInterfaceType.None });
			}
			tb.SetCustomAttribute(TCEAdapterGenerator.s_NoClassItfCABuilder);
		}

		// Token: 0x060063D1 RID: 25553 RVA: 0x00155E2C File Offset: 0x0015402C
		internal static TypeBuilder DefineUniqueType(string strInitFullName, TypeAttributes attrs, Type BaseType, Type[] aInterfaceTypes, ModuleBuilder mb)
		{
			string text = strInitFullName;
			int num = 2;
			while (mb.GetType(text) != null)
			{
				text = strInitFullName + "_" + num.ToString();
				num++;
			}
			return mb.DefineType(text, attrs, BaseType, aInterfaceTypes);
		}

		// Token: 0x060063D2 RID: 25554 RVA: 0x00155E74 File Offset: 0x00154074
		internal static void SetHiddenAttribute(TypeBuilder tb)
		{
			if (TCEAdapterGenerator.s_HiddenCABuilder == null)
			{
				Type[] array = new Type[] { typeof(TypeLibTypeFlags) };
				ConstructorInfo constructor = typeof(TypeLibTypeAttribute).GetConstructor(array);
				TCEAdapterGenerator.s_HiddenCABuilder = new CustomAttributeBuilder(constructor, new object[] { TypeLibTypeFlags.FHidden });
			}
			tb.SetCustomAttribute(TCEAdapterGenerator.s_HiddenCABuilder);
		}

		// Token: 0x060063D3 RID: 25555 RVA: 0x00155EDC File Offset: 0x001540DC
		internal static MethodInfo[] GetNonPropertyMethods(Type type)
		{
			MethodInfo[] methods = type.GetMethods();
			ArrayList arrayList = new ArrayList(methods);
			PropertyInfo[] properties = type.GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				MethodInfo[] accessors = propertyInfo.GetAccessors();
				foreach (MethodInfo methodInfo in accessors)
				{
					for (int k = 0; k < arrayList.Count; k++)
					{
						if ((MethodInfo)arrayList[k] == methodInfo)
						{
							arrayList.RemoveAt(k);
						}
					}
				}
			}
			MethodInfo[] array3 = new MethodInfo[arrayList.Count];
			arrayList.CopyTo(array3);
			return array3;
		}

		// Token: 0x060063D4 RID: 25556 RVA: 0x00155F8C File Offset: 0x0015418C
		internal static MethodInfo[] GetPropertyMethods(Type type)
		{
			MethodInfo[] methods = type.GetMethods();
			ArrayList arrayList = new ArrayList();
			PropertyInfo[] properties = type.GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				MethodInfo[] accessors = propertyInfo.GetAccessors();
				foreach (MethodInfo methodInfo in accessors)
				{
					arrayList.Add(methodInfo);
				}
			}
			MethodInfo[] array3 = new MethodInfo[arrayList.Count];
			arrayList.CopyTo(array3);
			return array3;
		}

		// Token: 0x04002CDE RID: 11486
		private ModuleBuilder m_Module;

		// Token: 0x04002CDF RID: 11487
		private Hashtable m_SrcItfToSrcItfInfoMap = new Hashtable();

		// Token: 0x04002CE0 RID: 11488
		private static volatile CustomAttributeBuilder s_NoClassItfCABuilder;

		// Token: 0x04002CE1 RID: 11489
		private static volatile CustomAttributeBuilder s_HiddenCABuilder;
	}
}
