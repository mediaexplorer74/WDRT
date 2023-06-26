using System;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000283 RID: 643
	internal static class AtomUtils
	{
		// Token: 0x06001564 RID: 5476 RVA: 0x0004E7A8 File Offset: 0x0004C9A8
		internal static string ComputeODataNavigationLinkRelation(ODataNavigationLink navigationLink)
		{
			return string.Join("/", new string[] { "http://schemas.microsoft.com/ado/2007/08/dataservices", "related", navigationLink.Name });
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x0004E7E0 File Offset: 0x0004C9E0
		internal static string ComputeODataNavigationLinkType(ODataNavigationLink navigationLink)
		{
			if (!navigationLink.IsCollection.Value)
			{
				return "application/atom+xml;type=entry";
			}
			return "application/atom+xml;type=feed";
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x0004E808 File Offset: 0x0004CA08
		internal static string ComputeODataAssociationLinkRelation(ODataAssociationLink associationLink)
		{
			return string.Join("/", new string[] { "http://schemas.microsoft.com/ado/2007/08/dataservices", "relatedlinks", associationLink.Name });
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x0004E840 File Offset: 0x0004CA40
		internal static string ComputeStreamPropertyRelation(ODataProperty streamProperty, bool forEditLink)
		{
			string text = (forEditLink ? "edit-media" : "mediaresource");
			return string.Join("/", new string[] { "http://schemas.microsoft.com/ado/2007/08/dataservices", text, streamProperty.Name });
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x0004E884 File Offset: 0x0004CA84
		internal static string UnescapeAtomLinkRelationAttribute(string relation)
		{
			Uri uri;
			if (!string.IsNullOrEmpty(relation) && Uri.TryCreate(relation, UriKind.RelativeOrAbsolute, out uri) && uri.IsAbsoluteUri)
			{
				return uri.GetComponents(UriComponents.AbsoluteUri, UriFormat.SafeUnescaped);
			}
			return null;
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x0004E8B7 File Offset: 0x0004CAB7
		internal static string GetNameFromAtomLinkRelationAttribute(string relation, string namespacePrefix)
		{
			if (relation != null && relation.StartsWith(namespacePrefix, StringComparison.Ordinal))
			{
				return relation.Substring(namespacePrefix.Length);
			}
			return null;
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x0004E8D4 File Offset: 0x0004CAD4
		internal static bool IsExactNavigationLinkTypeMatch(string navigationLinkType, out bool hasEntryType, out bool hasFeedType)
		{
			hasEntryType = false;
			hasFeedType = false;
			if (!navigationLinkType.StartsWith("application/atom+xml", StringComparison.Ordinal))
			{
				return false;
			}
			int length = navigationLinkType.Length;
			int num = length;
			switch (num)
			{
			case 20:
				return true;
			case 21:
				return navigationLinkType[length - 1] == ';';
			default:
				switch (num)
				{
				case 30:
					hasFeedType = string.Compare(";type=feed", 0, navigationLinkType, 20, ";type=feed".Length, StringComparison.Ordinal) == 0;
					return hasFeedType;
				case 31:
					hasEntryType = string.Compare(";type=entry", 0, navigationLinkType, 20, ";type=entry".Length, StringComparison.Ordinal) == 0;
					return hasEntryType;
				default:
					return false;
				}
				break;
			}
		}

		// Token: 0x040007CD RID: 1997
		private const int MimeApplicationAtomXmlLength = 20;

		// Token: 0x040007CE RID: 1998
		private const int MimeApplicationAtomXmlLengthWithSemicolon = 21;

		// Token: 0x040007CF RID: 1999
		private const int MimeApplicationAtomXmlTypeEntryLength = 31;

		// Token: 0x040007D0 RID: 2000
		private const int MimeApplicationAtomXmlTypeFeedLength = 30;

		// Token: 0x040007D1 RID: 2001
		private const string MimeApplicationAtomXmlTypeEntryParameter = ";type=entry";

		// Token: 0x040007D2 RID: 2002
		private const string MimeApplicationAtomXmlTypeFeedParameter = ";type=feed";
	}
}
