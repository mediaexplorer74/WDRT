using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x0200000E RID: 14
	internal static class DataServiceProviderMethods
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00002B78 File Offset: 0x00000D78
		public static object GetValue(object value, IEdmProperty property)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B7F File Offset: 0x00000D7F
		public static IEnumerable<T> GetSequenceValue<T>(object value, IEdmProperty property)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002B86 File Offset: 0x00000D86
		public static object Convert(object value, IEdmTypeReference typeReference)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002B8D File Offset: 0x00000D8D
		public static bool TypeIs(object value, IEdmTypeReference typeReference)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002B94 File Offset: 0x00000D94
		public static int Compare(string left, string right)
		{
			return Comparer<string>.Default.Compare(left, right);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002BA2 File Offset: 0x00000DA2
		public static int Compare(bool left, bool right)
		{
			return Comparer<bool>.Default.Compare(left, right);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002BB0 File Offset: 0x00000DB0
		public static int Compare(bool? left, bool? right)
		{
			return Comparer<bool?>.Default.Compare(left, right);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002BBE File Offset: 0x00000DBE
		public static int Compare(Guid left, Guid right)
		{
			return Comparer<Guid>.Default.Compare(left, right);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002BCC File Offset: 0x00000DCC
		public static int Compare(Guid? left, Guid? right)
		{
			return Comparer<Guid?>.Default.Compare(left, right);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002BDC File Offset: 0x00000DDC
		public static bool AreByteArraysEqual(byte[] left, byte[] right)
		{
			if (left == right)
			{
				return true;
			}
			if (left == null || right == null)
			{
				return false;
			}
			if (left.Length != right.Length)
			{
				return false;
			}
			for (int i = 0; i < left.Length; i++)
			{
				if (left[i] != right[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002C1A File Offset: 0x00000E1A
		public static bool AreByteArraysNotEqual(byte[] left, byte[] right)
		{
			return !DataServiceProviderMethods.AreByteArraysEqual(left, right);
		}

		// Token: 0x0400001A RID: 26
		internal static readonly MethodInfo GetValueMethodInfo = typeof(DataServiceProviderMethods).GetMethod("GetValue", new Type[]
		{
			typeof(object),
			typeof(IEdmProperty)
		}, true, true);

		// Token: 0x0400001B RID: 27
		internal static readonly MethodInfo GetSequenceValueMethodInfo = typeof(DataServiceProviderMethods).GetMethod("GetSequenceValue", new Type[]
		{
			typeof(object),
			typeof(IEdmProperty)
		}, true, true);

		// Token: 0x0400001C RID: 28
		internal static readonly MethodInfo ConvertMethodInfo = typeof(DataServiceProviderMethods).GetMethod("Convert", BindingFlags.Static | BindingFlags.Public);

		// Token: 0x0400001D RID: 29
		internal static readonly MethodInfo TypeIsMethodInfo = typeof(DataServiceProviderMethods).GetMethod("TypeIs", BindingFlags.Static | BindingFlags.Public);

		// Token: 0x0400001E RID: 30
		internal static readonly MethodInfo StringCompareMethodInfo = typeof(DataServiceProviderMethods).GetMethods(BindingFlags.Static | BindingFlags.Public).Single((MethodInfo m) => m.Name == "Compare" && m.GetParameters()[0].ParameterType == typeof(string));

		// Token: 0x0400001F RID: 31
		internal static readonly MethodInfo BoolCompareMethodInfo = typeof(DataServiceProviderMethods).GetMethods(BindingFlags.Static | BindingFlags.Public).Single((MethodInfo m) => m.Name == "Compare" && m.GetParameters()[0].ParameterType == typeof(bool));

		// Token: 0x04000020 RID: 32
		internal static readonly MethodInfo BoolCompareMethodInfoNullable = typeof(DataServiceProviderMethods).GetMethods(BindingFlags.Static | BindingFlags.Public).Single((MethodInfo m) => m.Name == "Compare" && m.GetParameters()[0].ParameterType == typeof(bool?));

		// Token: 0x04000021 RID: 33
		internal static readonly MethodInfo GuidCompareMethodInfo = typeof(DataServiceProviderMethods).GetMethods(BindingFlags.Static | BindingFlags.Public).Single((MethodInfo m) => m.Name == "Compare" && m.GetParameters()[0].ParameterType == typeof(Guid));

		// Token: 0x04000022 RID: 34
		internal static readonly MethodInfo GuidCompareMethodInfoNullable = typeof(DataServiceProviderMethods).GetMethods(BindingFlags.Static | BindingFlags.Public).Single((MethodInfo m) => m.Name == "Compare" && m.GetParameters()[0].ParameterType == typeof(Guid?));

		// Token: 0x04000023 RID: 35
		internal static readonly MethodInfo AreByteArraysEqualMethodInfo = typeof(DataServiceProviderMethods).GetMethod("AreByteArraysEqual", BindingFlags.Static | BindingFlags.Public);

		// Token: 0x04000024 RID: 36
		internal static readonly MethodInfo AreByteArraysNotEqualMethodInfo = typeof(DataServiceProviderMethods).GetMethod("AreByteArraysNotEqual", BindingFlags.Static | BindingFlags.Public);
	}
}
