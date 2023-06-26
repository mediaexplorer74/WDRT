using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x02000213 RID: 531
	[DebuggerDisplay("EpmTargetPathSegment {SegmentName} HasContent={HasContent}")]
	internal sealed class EpmTargetPathSegment
	{
		// Token: 0x0600105E RID: 4190 RVA: 0x0003C1A7 File Offset: 0x0003A3A7
		internal EpmTargetPathSegment()
		{
			this.subSegments = new List<EpmTargetPathSegment>();
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0003C1BC File Offset: 0x0003A3BC
		internal EpmTargetPathSegment(string segmentName, string segmentNamespaceUri, string segmentNamespacePrefix, EpmTargetPathSegment parentSegment)
			: this()
		{
			this.segmentName = segmentName;
			this.segmentNamespaceUri = segmentNamespaceUri;
			this.segmentNamespacePrefix = segmentNamespacePrefix;
			this.parentSegment = parentSegment;
			if (!string.IsNullOrEmpty(segmentName) && segmentName[0] == '@')
			{
				this.segmentAttributeName = segmentName.Substring(1);
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x0003C20C File Offset: 0x0003A40C
		internal string SegmentName
		{
			get
			{
				return this.segmentName;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x0003C214 File Offset: 0x0003A414
		internal string AttributeName
		{
			get
			{
				return this.segmentAttributeName;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x0003C21C File Offset: 0x0003A41C
		internal string SegmentNamespaceUri
		{
			get
			{
				return this.segmentNamespaceUri;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x0003C224 File Offset: 0x0003A424
		internal string SegmentNamespacePrefix
		{
			get
			{
				return this.segmentNamespacePrefix;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x0003C22C File Offset: 0x0003A42C
		// (set) Token: 0x06001065 RID: 4197 RVA: 0x0003C234 File Offset: 0x0003A434
		internal EntityPropertyMappingInfo EpmInfo
		{
			get
			{
				return this.epmInfo;
			}
			set
			{
				this.epmInfo = value;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x0003C23D File Offset: 0x0003A43D
		internal bool HasContent
		{
			get
			{
				return this.EpmInfo != null;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x0003C24B File Offset: 0x0003A44B
		internal bool IsAttribute
		{
			get
			{
				return this.segmentAttributeName != null;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001068 RID: 4200 RVA: 0x0003C259 File Offset: 0x0003A459
		internal EpmTargetPathSegment ParentSegment
		{
			get
			{
				return this.parentSegment;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x0003C261 File Offset: 0x0003A461
		internal List<EpmTargetPathSegment> SubSegments
		{
			get
			{
				return this.subSegments;
			}
		}

		// Token: 0x040005FD RID: 1533
		private readonly string segmentName;

		// Token: 0x040005FE RID: 1534
		private readonly string segmentAttributeName;

		// Token: 0x040005FF RID: 1535
		private readonly string segmentNamespaceUri;

		// Token: 0x04000600 RID: 1536
		private readonly string segmentNamespacePrefix;

		// Token: 0x04000601 RID: 1537
		private readonly List<EpmTargetPathSegment> subSegments;

		// Token: 0x04000602 RID: 1538
		private readonly EpmTargetPathSegment parentSegment;

		// Token: 0x04000603 RID: 1539
		private EntityPropertyMappingInfo epmInfo;
	}
}
