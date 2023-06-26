using System;
using System.Diagnostics;

namespace Microsoft.Data.OData
{
	// Token: 0x0200025E RID: 606
	[DebuggerDisplay("{Url.OriginalString}")]
	public sealed class ODataEntityReferenceLink : ODataItem
	{
		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x0004B727 File Offset: 0x00049927
		// (set) Token: 0x06001407 RID: 5127 RVA: 0x0004B72F File Offset: 0x0004992F
		public Uri Url { get; set; }

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x0004B738 File Offset: 0x00049938
		// (set) Token: 0x06001409 RID: 5129 RVA: 0x0004B740 File Offset: 0x00049940
		internal ODataEntityReferenceLinkSerializationInfo SerializationInfo
		{
			get
			{
				return this.serializationInfo;
			}
			set
			{
				this.serializationInfo = ODataEntityReferenceLinkSerializationInfo.Validate(value);
			}
		}

		// Token: 0x0400071F RID: 1823
		private ODataEntityReferenceLinkSerializationInfo serializationInfo;
	}
}
