using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Values;

namespace System.Data.Services.Client
{
	// Token: 0x0200002E RID: 46
	internal sealed class ClientEdmCollectionValue : IEdmCollectionValue, IEdmValue, IEdmElement
	{
		// Token: 0x0600015B RID: 347 RVA: 0x00008112 File Offset: 0x00006312
		public ClientEdmCollectionValue(IEdmTypeReference type, IEnumerable<IEdmValue> elements)
		{
			this.Type = type;
			this.Elements = elements.Select((IEdmValue v) => new ClientEdmCollectionValue.NullEdmDelayedValue(v));
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600015C RID: 348 RVA: 0x0000814A File Offset: 0x0000634A
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00008152 File Offset: 0x00006352
		public IEdmTypeReference Type { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600015E RID: 350 RVA: 0x0000815B File Offset: 0x0000635B
		public EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Collection;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000815E File Offset: 0x0000635E
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00008166 File Offset: 0x00006366
		public IEnumerable<IEdmDelayedValue> Elements { get; private set; }

		// Token: 0x0200002F RID: 47
		private class NullEdmDelayedValue : IEdmDelayedValue
		{
			// Token: 0x06000162 RID: 354 RVA: 0x0000816F File Offset: 0x0000636F
			public NullEdmDelayedValue(IEdmValue value)
			{
				this.Value = value;
			}

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x06000163 RID: 355 RVA: 0x0000817E File Offset: 0x0000637E
			// (set) Token: 0x06000164 RID: 356 RVA: 0x00008186 File Offset: 0x00006386
			public IEdmValue Value { get; private set; }
		}
	}
}
