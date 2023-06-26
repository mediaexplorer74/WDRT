using System;
using System.Reflection;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200043E RID: 1086
	internal static class EnumHelper<UnderlyingType>
	{
		// Token: 0x0600360F RID: 13839 RVA: 0x000D3B24 File Offset: 0x000D1D24
		public static UnderlyingType Cast<ValueType>(ValueType value)
		{
			return EnumHelper<UnderlyingType>.Caster<ValueType>.Instance(value);
		}

		// Token: 0x06003610 RID: 13840 RVA: 0x000D3B31 File Offset: 0x000D1D31
		internal static UnderlyingType Identity(UnderlyingType value)
		{
			return value;
		}

		// Token: 0x04001824 RID: 6180
		private static readonly MethodInfo IdentityInfo = Statics.GetDeclaredStaticMethod(typeof(EnumHelper<UnderlyingType>), "Identity");

		// Token: 0x02000B94 RID: 2964
		// (Invoke) Token: 0x06006CAB RID: 27819
		private delegate UnderlyingType Transformer<ValueType>(ValueType value);

		// Token: 0x02000B95 RID: 2965
		private static class Caster<ValueType>
		{
			// Token: 0x04003529 RID: 13609
			public static readonly EnumHelper<UnderlyingType>.Transformer<ValueType> Instance = (EnumHelper<UnderlyingType>.Transformer<ValueType>)Statics.CreateDelegate(typeof(EnumHelper<UnderlyingType>.Transformer<ValueType>), EnumHelper<UnderlyingType>.IdentityInfo);
		}
	}
}
