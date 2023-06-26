using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace System.Data.Services.Client.Metadata
{
	// Token: 0x02000136 RID: 310
	[DebuggerDisplay("{PropertyName}")]
	internal static class ClientTypeCache
	{
		// Token: 0x06000B32 RID: 2866 RVA: 0x0002C830 File Offset: 0x0002AA30
		internal static Type ResolveFromName(string wireName, Type userType)
		{
			ClientTypeCache.TypeName typeName;
			typeName.Type = userType;
			typeName.Name = wireName;
			Type type;
			bool flag2;
			lock (ClientTypeCache.namedTypes)
			{
				flag2 = ClientTypeCache.namedTypes.TryGetValue(typeName, out type);
			}
			if (!flag2)
			{
				string text = wireName;
				int num = wireName.LastIndexOf('.');
				if (0 <= num && num < wireName.Length - 1)
				{
					text = wireName.Substring(num + 1);
				}
				if (userType.Name == text)
				{
					type = userType;
				}
				else
				{
					foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
					{
						Type type2 = assembly.GetType(wireName, false);
						ClientTypeCache.ResolveSubclass(text, userType, type2, ref type);
						if (null == type2)
						{
							IEnumerable<Type> enumerable = null;
							try
							{
								enumerable = assembly.GetTypes();
							}
							catch (ReflectionTypeLoadException)
							{
							}
							if (enumerable != null)
							{
								foreach (Type type3 in enumerable)
								{
									ClientTypeCache.ResolveSubclass(text, userType, type3, ref type);
								}
							}
						}
					}
				}
				lock (ClientTypeCache.namedTypes)
				{
					ClientTypeCache.namedTypes[typeName] = type;
				}
			}
			return type;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0002C9B0 File Offset: 0x0002ABB0
		private static void ResolveSubclass(string wireClassName, Type userType, Type type, ref Type existing)
		{
			if (null != type && type.IsVisible() && wireClassName == type.Name && userType.IsAssignableFrom(type))
			{
				if (null != existing)
				{
					throw Error.InvalidOperation(Strings.ClientType_Ambiguous(wireClassName, userType));
				}
				existing = type;
			}
		}

		// Token: 0x0400060E RID: 1550
		private static readonly Dictionary<ClientTypeCache.TypeName, Type> namedTypes = new Dictionary<ClientTypeCache.TypeName, Type>(new ClientTypeCache.TypeNameEqualityComparer());

		// Token: 0x02000137 RID: 311
		private struct TypeName
		{
			// Token: 0x0400060F RID: 1551
			internal Type Type;

			// Token: 0x04000610 RID: 1552
			internal string Name;
		}

		// Token: 0x02000138 RID: 312
		private sealed class TypeNameEqualityComparer : IEqualityComparer<ClientTypeCache.TypeName>
		{
			// Token: 0x06000B35 RID: 2869 RVA: 0x0002CA10 File Offset: 0x0002AC10
			public bool Equals(ClientTypeCache.TypeName x, ClientTypeCache.TypeName y)
			{
				return x.Type == y.Type && x.Name == y.Name;
			}

			// Token: 0x06000B36 RID: 2870 RVA: 0x0002CA3C File Offset: 0x0002AC3C
			public int GetHashCode(ClientTypeCache.TypeName obj)
			{
				return obj.Type.GetHashCode() ^ obj.Name.GetHashCode();
			}
		}
	}
}
