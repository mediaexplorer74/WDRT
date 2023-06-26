using System;

namespace System.Diagnostics
{
	// Token: 0x0200049A RID: 1178
	internal class FilterElement : TypedElement
	{
		// Token: 0x06002BB2 RID: 11186 RVA: 0x000C5D0C File Offset: 0x000C3F0C
		public FilterElement()
			: base(typeof(TraceFilter))
		{
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x000C5D20 File Offset: 0x000C3F20
		public TraceFilter GetRuntimeObject()
		{
			TraceFilter traceFilter = (TraceFilter)base.BaseGetRuntimeObject();
			traceFilter.initializeData = base.InitData;
			return traceFilter;
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x000C5D46 File Offset: 0x000C3F46
		internal TraceFilter RefreshRuntimeObject(TraceFilter filter)
		{
			if (Type.GetType(this.TypeName) != filter.GetType() || base.InitData != filter.initializeData)
			{
				this._runtimeObject = null;
				return this.GetRuntimeObject();
			}
			return filter;
		}
	}
}
