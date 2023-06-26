using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x02000016 RID: 22
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
	public sealed class JsonArrayAttribute : JsonContainerAttribute
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000235E File Offset: 0x0000055E
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002366 File Offset: 0x00000566
		public bool AllowNullItems
		{
			get
			{
				return this._allowNullItems;
			}
			set
			{
				this._allowNullItems = value;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000236F File Offset: 0x0000056F
		public JsonArrayAttribute()
		{
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002377 File Offset: 0x00000577
		public JsonArrayAttribute(bool allowNullItems)
		{
			this._allowNullItems = allowNullItems;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002386 File Offset: 0x00000586
		[NullableContext(1)]
		public JsonArrayAttribute(string id)
			: base(id)
		{
		}

		// Token: 0x04000027 RID: 39
		private bool _allowNullItems;
	}
}
