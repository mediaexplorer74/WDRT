using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Data.Services.Client.Serializers
{
	// Token: 0x0200001A RID: 26
	[DebuggerDisplay("EpmTargetPathSegment {SegmentName} HasContent={HasContent}")]
	internal class EpmTargetPathSegment
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00004154 File Offset: 0x00002354
		internal EpmTargetPathSegment()
		{
			this.subSegments = new List<EpmTargetPathSegment>();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004167 File Offset: 0x00002367
		internal EpmTargetPathSegment(string segmentName, string segmentNamespaceUri, EpmTargetPathSegment parentSegment)
			: this()
		{
			this.segmentName = segmentName;
			this.segmentNamespaceUri = segmentNamespaceUri;
			this.parentSegment = parentSegment;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00004184 File Offset: 0x00002384
		internal string SegmentName
		{
			get
			{
				return this.segmentName;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000418C File Offset: 0x0000238C
		internal string SegmentNamespaceUri
		{
			get
			{
				return this.segmentNamespaceUri;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00004194 File Offset: 0x00002394
		// (set) Token: 0x06000093 RID: 147 RVA: 0x0000419C File Offset: 0x0000239C
		internal EntityPropertyMappingInfo EpmInfo { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000041A5 File Offset: 0x000023A5
		internal bool HasContent
		{
			get
			{
				return this.EpmInfo != null;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000041B3 File Offset: 0x000023B3
		internal bool IsAttribute
		{
			get
			{
				return !string.IsNullOrEmpty(this.SegmentName) && this.SegmentName[0] == '@';
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000041D4 File Offset: 0x000023D4
		internal EpmTargetPathSegment ParentSegment
		{
			get
			{
				return this.parentSegment;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000041DC File Offset: 0x000023DC
		internal List<EpmTargetPathSegment> SubSegments
		{
			get
			{
				return this.subSegments;
			}
		}

		// Token: 0x04000172 RID: 370
		private readonly string segmentName;

		// Token: 0x04000173 RID: 371
		private readonly string segmentNamespaceUri;

		// Token: 0x04000174 RID: 372
		private readonly List<EpmTargetPathSegment> subSegments;

		// Token: 0x04000175 RID: 373
		private readonly EpmTargetPathSegment parentSegment;
	}
}
