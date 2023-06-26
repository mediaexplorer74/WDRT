using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000047 RID: 71
	[NullableContext(1)]
	[Nullable(0)]
	internal class TypeInformation
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x00010E7E File Offset: 0x0000F07E
		public Type Type { get; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00010E86 File Offset: 0x0000F086
		public PrimitiveTypeCode TypeCode { get; }

		// Token: 0x06000466 RID: 1126 RVA: 0x00010E8E File Offset: 0x0000F08E
		public TypeInformation(Type type, PrimitiveTypeCode typeCode)
		{
			this.Type = type;
			this.TypeCode = typeCode;
		}
	}
}
