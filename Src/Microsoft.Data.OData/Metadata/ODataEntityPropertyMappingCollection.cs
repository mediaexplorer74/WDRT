using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Services.Common;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x02000206 RID: 518
	public sealed class ODataEntityPropertyMappingCollection : IEnumerable<EntityPropertyMappingAttribute>, IEnumerable
	{
		// Token: 0x06000FE2 RID: 4066 RVA: 0x0003A063 File Offset: 0x00038263
		public ODataEntityPropertyMappingCollection()
		{
			this.mappings = new List<EntityPropertyMappingAttribute>();
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0003A076 File Offset: 0x00038276
		public ODataEntityPropertyMappingCollection(IEnumerable<EntityPropertyMappingAttribute> other)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<EntityPropertyMappingAttribute>>(other, "other");
			this.mappings = new List<EntityPropertyMappingAttribute>(other);
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x0003A095 File Offset: 0x00038295
		internal int Count
		{
			get
			{
				return this.mappings.Count;
			}
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0003A0A2 File Offset: 0x000382A2
		public void Add(EntityPropertyMappingAttribute mapping)
		{
			ExceptionUtils.CheckArgumentNotNull<EntityPropertyMappingAttribute>(mapping, "mapping");
			this.mappings.Add(mapping);
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0003A0BB File Offset: 0x000382BB
		public IEnumerator<EntityPropertyMappingAttribute> GetEnumerator()
		{
			return this.mappings.GetEnumerator();
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0003A0CD File Offset: 0x000382CD
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.mappings.GetEnumerator();
		}

		// Token: 0x040005C4 RID: 1476
		private readonly List<EntityPropertyMappingAttribute> mappings;
	}
}
