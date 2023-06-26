using System;
using System.Reflection;
using System.Security;
using System.StubHelpers;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A0A RID: 2570
	internal static class ICustomPropertyProviderImpl
	{
		// Token: 0x0600659B RID: 26011 RVA: 0x0015AC48 File Offset: 0x00158E48
		internal static ICustomProperty CreateProperty(object target, string propertyName)
		{
			PropertyInfo property = target.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
			if (property == null)
			{
				return null;
			}
			return new CustomPropertyImpl(property);
		}

		// Token: 0x0600659C RID: 26012 RVA: 0x0015AC78 File Offset: 0x00158E78
		[SecurityCritical]
		internal unsafe static ICustomProperty CreateIndexedProperty(object target, string propertyName, TypeNameNative* pIndexedParamType)
		{
			Type type = null;
			SystemTypeMarshaler.ConvertToManaged(pIndexedParamType, ref type);
			return ICustomPropertyProviderImpl.CreateIndexedProperty(target, propertyName, type);
		}

		// Token: 0x0600659D RID: 26013 RVA: 0x0015AC98 File Offset: 0x00158E98
		internal static ICustomProperty CreateIndexedProperty(object target, string propertyName, Type indexedParamType)
		{
			PropertyInfo property = target.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, null, new Type[] { indexedParamType }, null);
			if (property == null)
			{
				return null;
			}
			return new CustomPropertyImpl(property);
		}

		// Token: 0x0600659E RID: 26014 RVA: 0x0015ACD2 File Offset: 0x00158ED2
		[SecurityCritical]
		internal unsafe static void GetType(object target, TypeNameNative* pIndexedParamType)
		{
			SystemTypeMarshaler.ConvertToNative(target.GetType(), pIndexedParamType);
		}
	}
}
