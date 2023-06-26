using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x020004C5 RID: 1221
	[Serializable]
	internal sealed class ShortEnumEqualityComparer<T> : EnumEqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x06003AB7 RID: 15031 RVA: 0x000E0E0A File Offset: 0x000DF00A
		public ShortEnumEqualityComparer()
		{
		}

		// Token: 0x06003AB8 RID: 15032 RVA: 0x000E0E12 File Offset: 0x000DF012
		public ShortEnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x06003AB9 RID: 15033 RVA: 0x000E0E1C File Offset: 0x000DF01C
		public override int GetHashCode(T obj)
		{
			int num = JitHelpers.UnsafeEnumCast<T>(obj);
			return ((short)num).GetHashCode();
		}
	}
}
