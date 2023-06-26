using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;

namespace System.Data.Services.Client
{
	// Token: 0x02000139 RID: 313
	internal static class PlatformHelper
	{
		// Token: 0x06000B38 RID: 2872 RVA: 0x0002CA5F File Offset: 0x0002AC5F
		internal static Assembly GetAssembly(this Type type)
		{
			return type.Assembly;
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0002CA67 File Offset: 0x0002AC67
		internal static bool IsValueType(this Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0002CA6F File Offset: 0x0002AC6F
		internal static bool IsGenericParameter(this Type type)
		{
			return type.IsGenericParameter;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0002CA77 File Offset: 0x0002AC77
		internal static bool IsAbstract(this Type type)
		{
			return type.IsAbstract;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0002CA7F File Offset: 0x0002AC7F
		internal static bool IsGenericType(this Type type)
		{
			return type.IsGenericType;
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0002CA87 File Offset: 0x0002AC87
		internal static bool IsGenericTypeDefinition(this Type type)
		{
			return type.IsGenericTypeDefinition;
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0002CA8F File Offset: 0x0002AC8F
		internal static bool IsVisible(this Type type)
		{
			return type.IsVisible;
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0002CA97 File Offset: 0x0002AC97
		internal static bool IsInterface(this Type type)
		{
			return type.IsInterface;
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0002CA9F File Offset: 0x0002AC9F
		internal static bool IsClass(this Type type)
		{
			return type.IsClass;
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0002CAA7 File Offset: 0x0002ACA7
		internal static bool IsEnum(this Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0002CAAF File Offset: 0x0002ACAF
		internal static Type GetBaseType(this Type type)
		{
			return type.BaseType;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0002CAB7 File Offset: 0x0002ACB7
		internal static bool ContainsGenericParameters(this Type type)
		{
			return type.ContainsGenericParameters;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0002CABF File Offset: 0x0002ACBF
		internal static ReadOnlyCollection<T> AsReadOnly<T>(this T[] array)
		{
			return Array.AsReadOnly<T>(array);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0002CAC7 File Offset: 0x0002ACC7
		internal static DateTime ConvertStringToDateTime(string text)
		{
			text = PlatformHelper.AddSecondsPaddingIfMissing(text);
			return XmlConvert.ToDateTime(text, XmlDateTimeSerializationMode.RoundtripKind);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0002CAD8 File Offset: 0x0002ACD8
		internal static DateTimeOffset ConvertStringToDateTimeOffset(string text)
		{
			text = PlatformHelper.AddSecondsPaddingIfMissing(text);
			return XmlConvert.ToDateTimeOffset(text);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0002CAE8 File Offset: 0x0002ACE8
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

		// Token: 0x06000B48 RID: 2888 RVA: 0x0002CB30 File Offset: 0x0002AD30
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

		// Token: 0x06000B49 RID: 2889 RVA: 0x0002CB79 File Offset: 0x0002AD79
		internal static string ConvertDateTimeToString(DateTime dateTime)
		{
			return XmlConvert.ToString(dateTime, XmlDateTimeSerializationMode.RoundtripKind);
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0002CB82 File Offset: 0x0002AD82
		internal static Type GetTypeOrThrow(string typeName)
		{
			return Type.GetType(typeName, true);
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0002CB8B File Offset: 0x0002AD8B
		internal static TypeCode GetTypeCode(Type type)
		{
			return Type.GetTypeCode(type);
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0002CB93 File Offset: 0x0002AD93
		internal static UnicodeCategory GetUnicodeCategory(char c)
		{
			return char.GetUnicodeCategory(c);
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0002CB9B File Offset: 0x0002AD9B
		internal static bool IsProperty(MemberInfo member)
		{
			return member.MemberType == MemberTypes.Property;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0002CBA7 File Offset: 0x0002ADA7
		internal static bool IsPrimitive(this Type type)
		{
			return type.IsPrimitive;
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0002CBAF File Offset: 0x0002ADAF
		internal static bool IsSealed(this Type type)
		{
			return type.IsSealed;
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0002CBB7 File Offset: 0x0002ADB7
		internal static bool IsMethod(MemberInfo member)
		{
			return member.MemberType == MemberTypes.Method;
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0002CBC2 File Offset: 0x0002ADC2
		internal static bool AreMembersEqual(MemberInfo member1, MemberInfo member2)
		{
			return member1.MetadataToken == member2.MetadataToken;
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0002CBD2 File Offset: 0x0002ADD2
		internal static IEnumerable<PropertyInfo> GetPublicProperties(this Type type, bool instanceOnly)
		{
			return type.GetPublicProperties(instanceOnly, false);
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0002CBDC File Offset: 0x0002ADDC
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

		// Token: 0x06000B54 RID: 2900 RVA: 0x0002CC04 File Offset: 0x0002AE04
		internal static IEnumerable<ConstructorInfo> GetInstanceConstructors(this Type type, bool isPublic)
		{
			BindingFlags bindingFlags = BindingFlags.Instance;
			bindingFlags |= (isPublic ? BindingFlags.Public : BindingFlags.NonPublic);
			return type.GetConstructors(bindingFlags);
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0002CC28 File Offset: 0x0002AE28
		internal static ConstructorInfo GetInstanceConstructor(this Type type, bool isPublic, Type[] argTypes)
		{
			BindingFlags bindingFlags = BindingFlags.Instance;
			bindingFlags |= (isPublic ? BindingFlags.Public : BindingFlags.NonPublic);
			return type.GetConstructor(bindingFlags, null, argTypes, null);
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0002CC50 File Offset: 0x0002AE50
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

		// Token: 0x06000B57 RID: 2903 RVA: 0x0002CC8C File Offset: 0x0002AE8C
		internal static MethodInfo GetMethod(this Type type, string name, bool isPublic, bool isStatic)
		{
			BindingFlags bindingFlags = BindingFlags.Default;
			bindingFlags |= (isPublic ? BindingFlags.Public : BindingFlags.NonPublic);
			bindingFlags |= (isStatic ? BindingFlags.Static : BindingFlags.Instance);
			return type.GetMethod(name, bindingFlags);
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0002CCBC File Offset: 0x0002AEBC
		internal static MethodInfo GetMethod(this Type type, string name, Type[] types, bool isPublic, bool isStatic)
		{
			BindingFlags bindingFlags = BindingFlags.Default;
			bindingFlags |= (isPublic ? BindingFlags.Public : BindingFlags.NonPublic);
			bindingFlags |= (isStatic ? BindingFlags.Static : BindingFlags.Instance);
			return type.GetMethod(name, bindingFlags, null, types, null);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0002CCED File Offset: 0x0002AEED
		internal static IEnumerable<MethodInfo> GetPublicStaticMethods(this Type type)
		{
			return type.GetMethods(BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0002CCF7 File Offset: 0x0002AEF7
		internal static IEnumerable<Type> GetNonPublicNestedTypes(this Type type)
		{
			return type.GetNestedTypes(BindingFlags.NonPublic);
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0002CD01 File Offset: 0x0002AF01
		public static Regex CreateCompiled(string pattern, RegexOptions options)
		{
			options |= RegexOptions.Compiled;
			return new Regex(pattern, options);
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0002CD0F File Offset: 0x0002AF0F
		public static string[] GetSegments(this Uri uri)
		{
			return uri.Segments;
		}

		// Token: 0x04000611 RID: 1553
		internal static readonly Type[] EmptyTypes = new Type[0];

		// Token: 0x04000612 RID: 1554
		internal static readonly string UriSchemeHttp = Uri.UriSchemeHttp;

		// Token: 0x04000613 RID: 1555
		internal static readonly string UriSchemeHttps = Uri.UriSchemeHttps;
	}
}
