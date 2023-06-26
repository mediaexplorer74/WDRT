using System;

namespace Microsoft.Data.OData
{
	// Token: 0x0200011A RID: 282
	public static class ODataObjectModelExtensions
	{
		// Token: 0x06000795 RID: 1941 RVA: 0x00019B44 File Offset: 0x00017D44
		public static void SetSerializationInfo(this ODataFeed feed, ODataFeedAndEntrySerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataFeed>(feed, "feed");
			feed.SerializationInfo = serializationInfo;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00019B58 File Offset: 0x00017D58
		public static void SetSerializationInfo(this ODataEntry entry, ODataFeedAndEntrySerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntry>(entry, "entry");
			entry.SerializationInfo = serializationInfo;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00019B6C File Offset: 0x00017D6C
		public static void SetSerializationInfo(this ODataProperty property, ODataPropertySerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataProperty>(property, "property");
			property.SerializationInfo = serializationInfo;
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00019B80 File Offset: 0x00017D80
		public static void SetSerializationInfo(this ODataCollectionStart collectionStart, ODataCollectionStartSerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataCollectionStart>(collectionStart, "collectionStart");
			collectionStart.SerializationInfo = serializationInfo;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00019B94 File Offset: 0x00017D94
		public static void SetSerializationInfo(this ODataEntityReferenceLink entityReferenceLink, ODataEntityReferenceLinkSerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntityReferenceLink>(entityReferenceLink, "entityReferenceLink");
			entityReferenceLink.SerializationInfo = serializationInfo;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00019BA8 File Offset: 0x00017DA8
		public static void SetSerializationInfo(this ODataEntityReferenceLinks entityReferenceLinks, ODataEntityReferenceLinksSerializationInfo serializationInfo)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntityReferenceLinks>(entityReferenceLinks, "entityReferenceLinks");
			entityReferenceLinks.SerializationInfo = serializationInfo;
		}
	}
}
