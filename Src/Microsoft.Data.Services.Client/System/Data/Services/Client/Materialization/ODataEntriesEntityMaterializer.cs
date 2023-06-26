using System;
using System.Collections.Generic;
using Microsoft.Data.OData;

namespace System.Data.Services.Client.Materialization
{
	// Token: 0x0200006D RID: 109
	internal sealed class ODataEntriesEntityMaterializer : ODataEntityMaterializer
	{
		// Token: 0x060003AB RID: 939 RVA: 0x00010071 File Offset: 0x0000E271
		public ODataEntriesEntityMaterializer(IEnumerable<ODataEntry> entries, IODataMaterializerContext materializerContext, EntityTrackingAdapter entityTrackingAdapter, QueryComponents queryComponents, Type expectedType, ProjectionPlan materializeEntryPlan, ODataFormat format)
			: base(materializerContext, entityTrackingAdapter, queryComponents, expectedType, materializeEntryPlan)
		{
			this.format = format;
			this.feedEntries = entries.GetEnumerator();
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00010095 File Offset: 0x0000E295
		internal override ODataFeed CurrentFeed
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003AD RID: 941 RVA: 0x00010098 File Offset: 0x0000E298
		internal override ODataEntry CurrentEntry
		{
			get
			{
				base.VerifyNotDisposed();
				return this.feedEntries.Current;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003AE RID: 942 RVA: 0x000100AB File Offset: 0x0000E2AB
		internal override long CountValue
		{
			get
			{
				throw new InvalidOperationException(Strings.MaterializeFromAtom_CountNotPresent);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003AF RID: 943 RVA: 0x000100B7 File Offset: 0x0000E2B7
		internal override bool IsCountable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x000100BA File Offset: 0x0000E2BA
		internal override bool IsEndOfStream
		{
			get
			{
				return this.isFinished;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x000100C2 File Offset: 0x0000E2C2
		protected override bool IsDisposed
		{
			get
			{
				return this.feedEntries == null;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x000100CD File Offset: 0x0000E2CD
		protected override ODataFormat Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000100D5 File Offset: 0x0000E2D5
		protected override bool ReadNextFeedOrEntry()
		{
			if (!this.isFinished && !this.feedEntries.MoveNext())
			{
				this.isFinished = true;
			}
			return !this.isFinished;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x000100FC File Offset: 0x0000E2FC
		protected override void OnDispose()
		{
			if (this.feedEntries != null)
			{
				this.feedEntries.Dispose();
				this.feedEntries = null;
			}
		}

		// Token: 0x040002B0 RID: 688
		private readonly ODataFormat format;

		// Token: 0x040002B1 RID: 689
		private IEnumerator<ODataEntry> feedEntries;

		// Token: 0x040002B2 RID: 690
		private bool isFinished;
	}
}
