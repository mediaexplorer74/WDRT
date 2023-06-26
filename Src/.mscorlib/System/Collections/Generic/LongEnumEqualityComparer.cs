using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x020004C6 RID: 1222
	[Serializable]
	internal sealed class LongEnumEqualityComparer<T> : EqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x06003ABA RID: 15034 RVA: 0x000E0E3C File Offset: 0x000DF03C
		public override bool Equals(T x, T y)
		{
			long num = JitHelpers.UnsafeEnumCastLong<T>(x);
			long num2 = JitHelpers.UnsafeEnumCastLong<T>(y);
			return num == num2;
		}

		// Token: 0x06003ABB RID: 15035 RVA: 0x000E0E5C File Offset: 0x000DF05C
		public override int GetHashCode(T obj)
		{
			return JitHelpers.UnsafeEnumCastLong<T>(obj).GetHashCode();
		}

		// Token: 0x06003ABC RID: 15036 RVA: 0x000E0E78 File Offset: 0x000DF078
		public override bool Equals(object obj)
		{
			LongEnumEqualityComparer<T> longEnumEqualityComparer = obj as LongEnumEqualityComparer<T>;
			return longEnumEqualityComparer != null;
		}

		// Token: 0x06003ABD RID: 15037 RVA: 0x000E0E90 File Offset: 0x000DF090
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}

		// Token: 0x06003ABE RID: 15038 RVA: 0x000E0EA2 File Offset: 0x000DF0A2
		public LongEnumEqualityComparer()
		{
		}

		// Token: 0x06003ABF RID: 15039 RVA: 0x000E0EAA File Offset: 0x000DF0AA
		public LongEnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x06003AC0 RID: 15040 RVA: 0x000E0EB2 File Offset: 0x000DF0B2
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.SetType(typeof(ObjectEqualityComparer<T>));
		}
	}
}
