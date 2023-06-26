using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x020004C4 RID: 1220
	[Serializable]
	internal sealed class SByteEnumEqualityComparer<T> : EnumEqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x06003AB4 RID: 15028 RVA: 0x000E0DDA File Offset: 0x000DEFDA
		public SByteEnumEqualityComparer()
		{
		}

		// Token: 0x06003AB5 RID: 15029 RVA: 0x000E0DE2 File Offset: 0x000DEFE2
		public SByteEnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x06003AB6 RID: 15030 RVA: 0x000E0DEC File Offset: 0x000DEFEC
		public override int GetHashCode(T obj)
		{
			int num = JitHelpers.UnsafeEnumCast<T>(obj);
			return ((sbyte)num).GetHashCode();
		}
	}
}
