using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200006F RID: 111
	[NullableContext(1)]
	[Nullable(0)]
	internal static class TypeExtensions
	{
		// Token: 0x060005F4 RID: 1524 RVA: 0x00019458 File Offset: 0x00017658
		public static MethodInfo Method(this Delegate d)
		{
			return d.Method;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00019460 File Offset: 0x00017660
		public static MemberTypes MemberType(this MemberInfo memberInfo)
		{
			return memberInfo.MemberType;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00019468 File Offset: 0x00017668
		public static bool ContainsGenericParameters(this Type type)
		{
			return type.ContainsGenericParameters;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00019470 File Offset: 0x00017670
		public static bool IsInterface(this Type type)
		{
			return type.IsInterface;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00019478 File Offset: 0x00017678
		public static bool IsGenericType(this Type type)
		{
			return type.IsGenericType;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00019480 File Offset: 0x00017680
		public static bool IsGenericTypeDefinition(this Type type)
		{
			return type.IsGenericTypeDefinition;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00019488 File Offset: 0x00017688
		public static Type BaseType(this Type type)
		{
			return type.BaseType;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00019490 File Offset: 0x00017690
		public static Assembly Assembly(this Type type)
		{
			return type.Assembly;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00019498 File Offset: 0x00017698
		public static bool IsEnum(this Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x000194A0 File Offset: 0x000176A0
		public static bool IsClass(this Type type)
		{
			return type.IsClass;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x000194A8 File Offset: 0x000176A8
		public static bool IsSealed(this Type type)
		{
			return type.IsSealed;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x000194B0 File Offset: 0x000176B0
		public static bool IsAbstract(this Type type)
		{
			return type.IsAbstract;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x000194B8 File Offset: 0x000176B8
		public static bool IsVisible(this Type type)
		{
			return type.IsVisible;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x000194C0 File Offset: 0x000176C0
		public static bool IsValueType(this Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x000194C8 File Offset: 0x000176C8
		public static bool IsPrimitive(this Type type)
		{
			return type.IsPrimitive;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x000194D0 File Offset: 0x000176D0
		public static bool AssignableToTypeName(this Type type, string fullTypeName, bool searchInterfaces, [Nullable(2)] [NotNullWhen(true)] out Type match)
		{
			Type type2 = type;
			while (type2 != null)
			{
				if (string.Equals(type2.FullName, fullTypeName, StringComparison.Ordinal))
				{
					match = type2;
					return true;
				}
				type2 = type2.BaseType();
			}
			if (searchInterfaces)
			{
				Type[] interfaces = type.GetInterfaces();
				for (int i = 0; i < interfaces.Length; i++)
				{
					if (string.Equals(interfaces[i].Name, fullTypeName, StringComparison.Ordinal))
					{
						match = type;
						return true;
					}
				}
			}
			match = null;
			return false;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00019538 File Offset: 0x00017738
		public static bool AssignableToTypeName(this Type type, string fullTypeName, bool searchInterfaces)
		{
			Type type2;
			return type.AssignableToTypeName(fullTypeName, searchInterfaces, out type2);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00019550 File Offset: 0x00017750
		public static bool ImplementInterface(this Type type, Type interfaceType)
		{
			Type type2 = type;
			while (type2 != null)
			{
				foreach (Type type3 in ((IEnumerable<Type>)type2.GetInterfaces()))
				{
					if (type3 == interfaceType || (type3 != null && type3.ImplementInterface(interfaceType)))
					{
						return true;
					}
				}
				type2 = type2.BaseType();
			}
			return false;
		}
	}
}
