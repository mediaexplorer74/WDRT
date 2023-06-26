using System;
using Microsoft.Data.OData.Atom;

namespace Microsoft.Data.OData
{
	// Token: 0x02000215 RID: 533
	internal static class AtomMetadataReaderUtils
	{
		// Token: 0x06001074 RID: 4212 RVA: 0x0003C7C8 File Offset: 0x0003A9C8
		internal static AtomEntryMetadata CreateNewAtomEntryMetadata()
		{
			return new AtomEntryMetadata
			{
				Authors = ReadOnlyEnumerable<AtomPersonMetadata>.Empty(),
				Categories = ReadOnlyEnumerable<AtomCategoryMetadata>.Empty(),
				Contributors = ReadOnlyEnumerable<AtomPersonMetadata>.Empty(),
				Links = ReadOnlyEnumerable<AtomLinkMetadata>.Empty()
			};
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x0003C808 File Offset: 0x0003AA08
		internal static AtomFeedMetadata CreateNewAtomFeedMetadata()
		{
			return new AtomFeedMetadata
			{
				Authors = ReadOnlyEnumerable<AtomPersonMetadata>.Empty(),
				Categories = ReadOnlyEnumerable<AtomCategoryMetadata>.Empty(),
				Contributors = ReadOnlyEnumerable<AtomPersonMetadata>.Empty(),
				Links = ReadOnlyEnumerable<AtomLinkMetadata>.Empty()
			};
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x0003C848 File Offset: 0x0003AA48
		internal static void AddAuthor(this AtomEntryMetadata entryMetadata, AtomPersonMetadata authorMetadata)
		{
			entryMetadata.Authors = entryMetadata.Authors.ConcatToReadOnlyEnumerable("Authors", authorMetadata);
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0003C861 File Offset: 0x0003AA61
		internal static void AddContributor(this AtomEntryMetadata entryMetadata, AtomPersonMetadata contributorMetadata)
		{
			entryMetadata.Contributors = entryMetadata.Contributors.ConcatToReadOnlyEnumerable("Contributors", contributorMetadata);
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0003C87A File Offset: 0x0003AA7A
		internal static void AddLink(this AtomEntryMetadata entryMetadata, AtomLinkMetadata linkMetadata)
		{
			entryMetadata.Links = entryMetadata.Links.ConcatToReadOnlyEnumerable("Links", linkMetadata);
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x0003C893 File Offset: 0x0003AA93
		internal static void AddLink(this AtomFeedMetadata feedMetadata, AtomLinkMetadata linkMetadata)
		{
			feedMetadata.Links = feedMetadata.Links.ConcatToReadOnlyEnumerable("Links", linkMetadata);
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x0003C8AC File Offset: 0x0003AAAC
		internal static void AddCategory(this AtomEntryMetadata entryMetadata, AtomCategoryMetadata categoryMetadata)
		{
			entryMetadata.Categories = entryMetadata.Categories.ConcatToReadOnlyEnumerable("Categories", categoryMetadata);
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0003C8C5 File Offset: 0x0003AAC5
		internal static void AddCategory(this AtomFeedMetadata feedMetadata, AtomCategoryMetadata categoryMetadata)
		{
			feedMetadata.Categories = feedMetadata.Categories.ConcatToReadOnlyEnumerable("Categories", categoryMetadata);
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0003C8DE File Offset: 0x0003AADE
		internal static void AddAuthor(this AtomFeedMetadata feedMetadata, AtomPersonMetadata authorMetadata)
		{
			feedMetadata.Authors = feedMetadata.Authors.ConcatToReadOnlyEnumerable("Authors", authorMetadata);
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0003C8F7 File Offset: 0x0003AAF7
		internal static void AddContributor(this AtomFeedMetadata feedMetadata, AtomPersonMetadata contributorMetadata)
		{
			feedMetadata.Contributors = feedMetadata.Contributors.ConcatToReadOnlyEnumerable("Contributors", contributorMetadata);
		}
	}
}
