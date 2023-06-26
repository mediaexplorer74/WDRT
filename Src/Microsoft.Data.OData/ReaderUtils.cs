using System;
using System.Linq;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData
{
	// Token: 0x02000243 RID: 579
	internal static class ReaderUtils
	{
		// Token: 0x06001283 RID: 4739 RVA: 0x00045AB0 File Offset: 0x00043CB0
		internal static ODataEntry CreateNewEntry()
		{
			return new ODataEntry
			{
				Properties = new ReadOnlyEnumerable<ODataProperty>(),
				AssociationLinks = ReadOnlyEnumerable<ODataAssociationLink>.Empty(),
				Actions = ReadOnlyEnumerable<ODataAction>.Empty(),
				Functions = ReadOnlyEnumerable<ODataFunction>.Empty()
			};
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x00045AF0 File Offset: 0x00043CF0
		internal static void CheckForDuplicateNavigationLinkNameAndSetAssociationLink(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, ODataNavigationLink navigationLink, bool isExpanded, bool? isCollection)
		{
			ODataAssociationLink odataAssociationLink = duplicatePropertyNamesChecker.CheckForDuplicatePropertyNames(navigationLink, isExpanded, isCollection);
			if (odataAssociationLink != null && odataAssociationLink.Url != null && navigationLink.AssociationLinkUrl == null)
			{
				navigationLink.AssociationLinkUrl = odataAssociationLink.Url;
			}
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x00045B34 File Offset: 0x00043D34
		internal static void CheckForDuplicateAssociationLinkAndUpdateNavigationLink(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, ODataAssociationLink associationLink)
		{
			ODataNavigationLink odataNavigationLink = duplicatePropertyNamesChecker.CheckForDuplicateAssociationLinkNames(associationLink);
			if (odataNavigationLink != null && odataNavigationLink.AssociationLinkUrl == null && associationLink.Url != null)
			{
				odataNavigationLink.AssociationLinkUrl = associationLink.Url;
			}
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x00045B94 File Offset: 0x00043D94
		internal static ODataAssociationLink GetOrCreateAssociationLinkForNavigationProperty(ODataEntry entry, IEdmNavigationProperty navigationProperty)
		{
			ODataAssociationLink odataAssociationLink = entry.AssociationLinks.FirstOrDefault((ODataAssociationLink al) => al.Name == navigationProperty.Name);
			if (odataAssociationLink == null)
			{
				odataAssociationLink = new ODataAssociationLink
				{
					Name = navigationProperty.Name
				};
				entry.AddAssociationLink(odataAssociationLink);
			}
			return odataAssociationLink;
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x00045BEA File Offset: 0x00043DEA
		internal static bool HasFlag(this ODataUndeclaredPropertyBehaviorKinds undeclaredPropertyBehaviorKinds, ODataUndeclaredPropertyBehaviorKinds flag)
		{
			return (undeclaredPropertyBehaviorKinds & flag) == flag;
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x00045BF2 File Offset: 0x00043DF2
		internal static string GetExpectedPropertyName(IEdmStructuralProperty expectedProperty)
		{
			if (expectedProperty == null)
			{
				return null;
			}
			return expectedProperty.Name;
		}
	}
}
