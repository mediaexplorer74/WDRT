using System;

namespace Microsoft.Data.OData
{
	// Token: 0x0200027C RID: 636
	internal static class TypeUtils
	{
		// Token: 0x060014FB RID: 5371 RVA: 0x0004E328 File Offset: 0x0004C528
		internal static bool IsNullableType(Type type)
		{
			return type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x0004E349 File Offset: 0x0004C549
		internal static Type GetNonNullableType(Type type)
		{
			return Nullable.GetUnderlyingType(type) ?? type;
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x0004E358 File Offset: 0x0004C558
		internal static Type GetNullableType(Type type)
		{
			if (!TypeUtils.TypeAllowsNull(type))
			{
				type = typeof(Nullable<>).MakeGenericType(new Type[] { type });
			}
			return type;
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x0004E38B File Offset: 0x0004C58B
		internal static bool TypeAllowsNull(Type type)
		{
			return !type.IsValueType() || TypeUtils.IsNullableType(type);
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x0004E39D File Offset: 0x0004C59D
		internal static bool AreTypesEquivalent(Type typeA, Type typeB)
		{
			return !(typeA == null) && !(typeB == null) && typeA.IsEquivalentTo(typeB);
		}
	}
}
