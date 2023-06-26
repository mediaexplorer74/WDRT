using System;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000286 RID: 646
	public static class ExtensionMethods
	{
		// Token: 0x0600157D RID: 5501 RVA: 0x0004EB80 File Offset: 0x0004CD80
		public static AtomEntryMetadata Atom(this ODataEntry entry)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntry>(entry, "entry");
			AtomEntryMetadata atomEntryMetadata = entry.GetAnnotation<AtomEntryMetadata>();
			if (atomEntryMetadata == null)
			{
				atomEntryMetadata = new AtomEntryMetadata();
				entry.SetAnnotation<AtomEntryMetadata>(atomEntryMetadata);
			}
			return atomEntryMetadata;
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0004EBB0 File Offset: 0x0004CDB0
		public static AtomFeedMetadata Atom(this ODataFeed feed)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataFeed>(feed, "feed");
			AtomFeedMetadata atomFeedMetadata = feed.GetAnnotation<AtomFeedMetadata>();
			if (atomFeedMetadata == null)
			{
				atomFeedMetadata = new AtomFeedMetadata();
				feed.SetAnnotation<AtomFeedMetadata>(atomFeedMetadata);
			}
			return atomFeedMetadata;
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x0004EBE0 File Offset: 0x0004CDE0
		public static AtomLinkMetadata Atom(this ODataNavigationLink navigationLink)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataNavigationLink>(navigationLink, "navigationLink");
			AtomLinkMetadata atomLinkMetadata = navigationLink.GetAnnotation<AtomLinkMetadata>();
			if (atomLinkMetadata == null)
			{
				atomLinkMetadata = new AtomLinkMetadata();
				navigationLink.SetAnnotation<AtomLinkMetadata>(atomLinkMetadata);
			}
			return atomLinkMetadata;
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x0004EC10 File Offset: 0x0004CE10
		public static AtomWorkspaceMetadata Atom(this ODataWorkspace workspace)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataWorkspace>(workspace, "workspace");
			AtomWorkspaceMetadata atomWorkspaceMetadata = workspace.GetAnnotation<AtomWorkspaceMetadata>();
			if (atomWorkspaceMetadata == null)
			{
				atomWorkspaceMetadata = new AtomWorkspaceMetadata();
				workspace.SetAnnotation<AtomWorkspaceMetadata>(atomWorkspaceMetadata);
			}
			return atomWorkspaceMetadata;
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x0004EC40 File Offset: 0x0004CE40
		public static AtomResourceCollectionMetadata Atom(this ODataResourceCollectionInfo collection)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataResourceCollectionInfo>(collection, "collection");
			AtomResourceCollectionMetadata atomResourceCollectionMetadata = collection.GetAnnotation<AtomResourceCollectionMetadata>();
			if (atomResourceCollectionMetadata == null)
			{
				atomResourceCollectionMetadata = new AtomResourceCollectionMetadata();
				collection.SetAnnotation<AtomResourceCollectionMetadata>(atomResourceCollectionMetadata);
			}
			return atomResourceCollectionMetadata;
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x0004EC70 File Offset: 0x0004CE70
		public static AtomLinkMetadata Atom(this ODataAssociationLink associationLink)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataAssociationLink>(associationLink, "associationLink");
			AtomLinkMetadata atomLinkMetadata = associationLink.GetAnnotation<AtomLinkMetadata>();
			if (atomLinkMetadata == null)
			{
				atomLinkMetadata = new AtomLinkMetadata();
				associationLink.SetAnnotation<AtomLinkMetadata>(atomLinkMetadata);
			}
			return atomLinkMetadata;
		}
	}
}
