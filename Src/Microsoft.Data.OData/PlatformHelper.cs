using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;

namespace Microsoft.Data.OData
{
	// Token: 0x020002B2 RID: 690
	internal static class PlatformHelper
	{
		// Token: 0x06001786 RID: 6022 RVA: 0x00054CF4 File Offset: 0x00052EF4
		internal static Assembly GetAssembly(this Type type)
		{
			return type.Assembly;
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x00054CFC File Offset: 0x00052EFC
		internal static bool IsValueType(this Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x00054D04 File Offset: 0x00052F04
		internal static bool IsGenericParameter(this Type type)
		{
			return type.IsGenericParameter;
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x00054D0C File Offset: 0x00052F0C
		internal static bool IsAbstract(this Type type)
		{
			return type.IsAbstract;
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x00054D14 File Offset: 0x00052F14
		internal static bool IsGenericType(this Type type)
		{
			return type.IsGenericType;
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x00054D1C File Offset: 0x00052F1C
		internal static bool IsGenericTypeDefinition(this Type type)
		{
			return type.IsGenericTypeDefinition;
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x00054D24 File Offset: 0x00052F24
		internal static bool IsVisible(this Type type)
		{
			return type.IsVisible;
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x00054D2C File Offset: 0x00052F2C
		internal static bool IsInterface(this Type type)
		{
			return type.IsInterface;
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x00054D34 File Offset: 0x00052F34
		internal static bool IsClass(this Type type)
		{
			return type.IsClass;
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x00054D3C File Offset: 0x00052F3C
		internal static bool IsEnum(this Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x00054D44 File Offset: 0x00052F44
		internal static Type GetBaseType(this Type type)
		{
			return type.BaseType;
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x00054D4C File Offset: 0x00052F4C
		internal static bool ContainsGenericParameters(this Type type)
		{
			return type.ContainsGenericParameters;
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x00054D54 File Offset: 0x00052F54
		internal static ReadOnlyCollection<T> AsReadOnly<T>(this T[] array)
		{
			return Array.AsReadOnly<T>(array);
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x00054D5C File Offset: 0x00052F5C
		internal static DateTime ConvertStringToDateTime(string text)
		{
			text = PlatformHelper.AddSecondsPaddingIfMissing(text);
			return XmlConvert.ToDateTime(text, XmlDateTimeSerializationMode.RoundtripKind);
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x00054D6D File Offset: 0x00052F6D
		internal static DateTimeOffset ConvertStringToDateTimeOffset(string text)
		{
			text = PlatformHelper.AddSecondsPaddingIfMissing(text);
			return XmlConvert.ToDateTimeOffset(text);
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x00054D80 File Offset: 0x00052F80
		internal static string AddSecondsPaddingIfMissing(string text)
		{
			int num = text.IndexOf("T", StringComparison.Ordinal);
			int num2 = num + 6;
			if (num > 0 && (text.Length <= num2 || text[num2] != ':'))
			{
				text = text.Insert(num2, ":00");
			}
			return text;
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x00054DC8 File Offset: 0x00052FC8
		internal static string ConvertDateTimeToStringInternal(DateTime dateTime)
		{
			if (dateTime.Kind == DateTimeKind.Unspecified)
			{
				DateTimeOffset dateTimeOffset = new DateTimeOffset(dateTime, TimeSpan.Zero);
				string text = XmlConvert.ToString(dateTimeOffset);
				return text.TrimEnd(new char[] { 'Z' });
			}
			return XmlConvert.ToString(dateTime);
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x00054E11 File Offset: 0x00053011
		internal static string ConvertDateTimeToString(DateTime dateTime)
		{
			return XmlConvert.ToString(dateTime, XmlDateTimeSerializationMode.RoundtripKind);
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x00054E1A File Offset: 0x0005301A
		internal static Type GetTypeOrThrow(string typeName)
		{
			return Type.GetType(typeName, true);
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x00054E23 File Offset: 0x00053023
		internal static TypeCode GetTypeCode(Type type)
		{
			return Type.GetTypeCode(type);
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x00054E2B File Offset: 0x0005302B
		internal static UnicodeCategory GetUnicodeCategory(char c)
		{
			return char.GetUnicodeCategory(c);
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x00054E33 File Offset: 0x00053033
		internal static bool IsProperty(MemberInfo member)
		{
			return member.MemberType == MemberTypes.Property;
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x00054E3F File Offset: 0x0005303F
		internal static bool IsPrimitive(this Type type)
		{
			return type.IsPrimitive;
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x00054E47 File Offset: 0x00053047
		internal static bool IsSealed(this Type type)
		{
			return type.IsSealed;
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x00054E4F File Offset: 0x0005304F
		internal static bool IsMethod(MemberInfo member)
		{
			return member.MemberType == MemberTypes.Method;
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x00054E5A File Offset: 0x0005305A
		internal static bool AreMembersEqual(MemberInfo member1, MemberInfo member2)
		{
			return member1.MetadataToken == member2.MetadataToken;
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x00054E6A File Offset: 0x0005306A
		internal static IEnumerable<PropertyInfo> GetPublicProperties(this Type type, bool instanceOnly)
		{
			return type.GetPublicProperties(instanceOnly, false);
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x00054E74 File Offset: 0x00053074
		internal static IEnumerable<PropertyInfo> GetPublicProperties(this Type type, bool instanceOnly, bool declaredOnly)
		{
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
			if (!instanceOnly)
			{
				bindingFlags |= BindingFlags.Static;
			}
			if (declaredOnly)
			{
				bindingFlags |= BindingFlags.DeclaredOnly;
			}
			return type.GetProperties(bindingFlags);
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x00054E9C File Offset: 0x0005309C
		internal static IEnumerable<ConstructorInfo> GetInstanceConstructors(this Type type, bool isPublic)
		{
			BindingFlags bindingFlags = BindingFlags.Instance;
			bindingFlags |= (isPublic ? BindingFlags.Public : BindingFlags.NonPublic);
			return type.GetConstructors(bindingFlags);
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x00054EC0 File Offset: 0x000530C0
		internal static ConstructorInfo GetInstanceConstructor(this Type type, bool isPublic, Type[] argTypes)
		{
			BindingFlags bindingFlags = BindingFlags.Instance;
			bindingFlags |= (isPublic ? BindingFlags.Public : BindingFlags.NonPublic);
			return type.GetConstructor(bindingFlags, null, argTypes, null);
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x00054EE8 File Offset: 0x000530E8
		internal static bool TryGetMethod(this Type type, string name, Type[] parameterTypes, out MethodInfo foundMethod)
		{
			foundMethod = null;
			bool flag;
			try
			{
				foundMethod = type.GetMethod(name, parameterTypes);
				flag = foundMethod != null;
			}
			catch (ArgumentNullException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x00054F24 File Offset: 0x00053124
		internal static MethodInfo GetMethod(this Type type, string name, bool isPublic, bool isStatic)
		{
			BindingFlags bindingFlags = BindingFlags.Default;
			bindingFlags |= (isPublic ? BindingFlags.Public : BindingFlags.NonPublic);
			bindingFlags |= (isStatic ? BindingFlags.Static : BindingFlags.Instance);
			return type.GetMethod(name, bindingFlags);
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x00054F54 File Offset: 0x00053154
		internal static MethodInfo GetMethod(this Type type, string name, Type[] types, bool isPublic, bool isStatic)
		{
			BindingFlags bindingFlags = BindingFlags.Default;
			bindingFlags |= (isPublic ? BindingFlags.Public : BindingFlags.NonPublic);
			bindingFlags |= (isStatic ? BindingFlags.Static : BindingFlags.Instance);
			return type.GetMethod(name, bindingFlags, null, types, null);
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x00054F85 File Offset: 0x00053185
		internal static IEnumerable<MethodInfo> GetPublicStaticMethods(this Type type)
		{
			return type.GetMethods(BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x00054F8F File Offset: 0x0005318F
		internal static IEnumerable<Type> GetNonPublicNestedTypes(this Type type)
		{
			return type.GetNestedTypes(BindingFlags.NonPublic);
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x00054F99 File Offset: 0x00053199
		public static Regex CreateCompiled(string pattern, RegexOptions options)
		{
			options |= RegexOptions.Compiled;
			return new Regex(pattern, options);
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x00054FA7 File Offset: 0x000531A7
		public static string[] GetSegments(this Uri uri)
		{
			return uri.Segments;
		}

		// Token: 0x040009B0 RID: 2480
		internal static readonly Type[] EmptyTypes = new Type[0];

		// Token: 0x040009B1 RID: 2481
		internal static readonly string UriSchemeHttp = Uri.UriSchemeHttp;

		// Token: 0x040009B2 RID: 2482
		internal static readonly string UriSchemeHttps = Uri.UriSchemeHttps;
	}
}
