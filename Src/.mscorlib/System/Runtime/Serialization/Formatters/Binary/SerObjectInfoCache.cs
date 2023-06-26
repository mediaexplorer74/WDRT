using System;
using System.Reflection;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007A2 RID: 1954
	internal sealed class SerObjectInfoCache
	{
		// Token: 0x06005502 RID: 21762 RVA: 0x0012F766 File Offset: 0x0012D966
		internal SerObjectInfoCache(string typeName, string assemblyName, bool hasTypeForwardedFrom)
		{
			this.fullTypeName = typeName;
			this.assemblyString = assemblyName;
			this.hasTypeForwardedFrom = hasTypeForwardedFrom;
		}

		// Token: 0x06005503 RID: 21763 RVA: 0x0012F784 File Offset: 0x0012D984
		internal SerObjectInfoCache(Type type)
		{
			TypeInformation typeInformation = BinaryFormatter.GetTypeInformation(type);
			this.fullTypeName = typeInformation.FullTypeName;
			this.assemblyString = typeInformation.AssemblyString;
			this.hasTypeForwardedFrom = typeInformation.HasTypeForwardedFrom;
		}

		// Token: 0x04002710 RID: 10000
		internal string fullTypeName;

		// Token: 0x04002711 RID: 10001
		internal string assemblyString;

		// Token: 0x04002712 RID: 10002
		internal bool hasTypeForwardedFrom;

		// Token: 0x04002713 RID: 10003
		internal MemberInfo[] memberInfos;

		// Token: 0x04002714 RID: 10004
		internal string[] memberNames;

		// Token: 0x04002715 RID: 10005
		internal Type[] memberTypes;
	}
}
