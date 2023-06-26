using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x020004C3 RID: 1219
	[Serializable]
	internal class EnumEqualityComparer<T> : EqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x06003AAD RID: 15021 RVA: 0x000E0D38 File Offset: 0x000DEF38
		public override bool Equals(T x, T y)
		{
			int num = JitHelpers.UnsafeEnumCast<T>(x);
			int num2 = JitHelpers.UnsafeEnumCast<T>(y);
			return num == num2;
		}

		// Token: 0x06003AAE RID: 15022 RVA: 0x000E0D58 File Offset: 0x000DEF58
		public override int GetHashCode(T obj)
		{
			return JitHelpers.UnsafeEnumCast<T>(obj).GetHashCode();
		}

		// Token: 0x06003AAF RID: 15023 RVA: 0x000E0D73 File Offset: 0x000DEF73
		public EnumEqualityComparer()
		{
		}

		// Token: 0x06003AB0 RID: 15024 RVA: 0x000E0D7B File Offset: 0x000DEF7B
		protected EnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x06003AB1 RID: 15025 RVA: 0x000E0D83 File Offset: 0x000DEF83
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (Type.GetTypeCode(Enum.GetUnderlyingType(typeof(T))) != TypeCode.Int32)
			{
				info.SetType(typeof(ObjectEqualityComparer<T>));
			}
		}

		// Token: 0x06003AB2 RID: 15026 RVA: 0x000E0DB0 File Offset: 0x000DEFB0
		public override bool Equals(object obj)
		{
			EnumEqualityComparer<T> enumEqualityComparer = obj as EnumEqualityComparer<T>;
			return enumEqualityComparer != null;
		}

		// Token: 0x06003AB3 RID: 15027 RVA: 0x000E0DC8 File Offset: 0x000DEFC8
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
