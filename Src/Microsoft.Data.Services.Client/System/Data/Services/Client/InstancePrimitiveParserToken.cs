using System;

namespace System.Data.Services.Client
{
	// Token: 0x020000B0 RID: 176
	internal class InstancePrimitiveParserToken<T> : PrimitiveParserToken
	{
		// Token: 0x060005B4 RID: 1460 RVA: 0x00015BE8 File Offset: 0x00013DE8
		internal InstancePrimitiveParserToken(T instance)
		{
			this.Instance = instance;
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x00015BF7 File Offset: 0x00013DF7
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x00015BFF File Offset: 0x00013DFF
		internal T Instance { get; private set; }

		// Token: 0x060005B7 RID: 1463 RVA: 0x00015C08 File Offset: 0x00013E08
		internal override object Materialize(Type clrType)
		{
			return this.Instance;
		}
	}
}
