using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000270 RID: 624
	internal static class ODataAtomWriterMetadataEpmMergeUtils
	{
		// Token: 0x0600148C RID: 5260 RVA: 0x0004CCAC File Offset: 0x0004AEAC
		internal static AtomEntryMetadata MergeCustomAndEpmEntryMetadata(AtomEntryMetadata customEntryMetadata, AtomEntryMetadata epmEntryMetadata, ODataWriterBehavior writerBehavior)
		{
			if (customEntryMetadata != null && writerBehavior.FormatBehaviorKind == ODataBehaviorKind.WcfDataServicesClient)
			{
				if (customEntryMetadata.Updated != null)
				{
					customEntryMetadata.UpdatedString = ODataAtomConvert.ToAtomString(customEntryMetadata.Updated.Value);
				}
				if (customEntryMetadata.Published != null)
				{
					customEntryMetadata.PublishedString = ODataAtomConvert.ToAtomString(customEntryMetadata.Published.Value);
				}
			}
			AtomEntryMetadata atomEntryMetadata;
			if (ODataAtomWriterMetadataEpmMergeUtils.TryMergeIfNull<AtomEntryMetadata>(customEntryMetadata, epmEntryMetadata, out atomEntryMetadata))
			{
				return atomEntryMetadata;
			}
			epmEntryMetadata.Title = ODataAtomWriterMetadataEpmMergeUtils.MergeAtomTextValue(customEntryMetadata.Title, epmEntryMetadata.Title, "Title");
			epmEntryMetadata.Summary = ODataAtomWriterMetadataEpmMergeUtils.MergeAtomTextValue(customEntryMetadata.Summary, epmEntryMetadata.Summary, "Summary");
			epmEntryMetadata.Rights = ODataAtomWriterMetadataEpmMergeUtils.MergeAtomTextValue(customEntryMetadata.Rights, epmEntryMetadata.Rights, "Rights");
			if (writerBehavior.FormatBehaviorKind == ODataBehaviorKind.WcfDataServicesClient)
			{
				epmEntryMetadata.PublishedString = ODataAtomWriterMetadataEpmMergeUtils.MergeTextValue(customEntryMetadata.PublishedString, epmEntryMetadata.PublishedString, "PublishedString");
				epmEntryMetadata.UpdatedString = ODataAtomWriterMetadataEpmMergeUtils.MergeTextValue(customEntryMetadata.UpdatedString, epmEntryMetadata.UpdatedString, "UpdatedString");
			}
			else
			{
				epmEntryMetadata.Published = ODataAtomWriterMetadataEpmMergeUtils.MergeDateTimeValue(customEntryMetadata.Published, epmEntryMetadata.Published, "Published");
				epmEntryMetadata.Updated = ODataAtomWriterMetadataEpmMergeUtils.MergeDateTimeValue(customEntryMetadata.Updated, epmEntryMetadata.Updated, "Updated");
			}
			epmEntryMetadata.Authors = ODataAtomWriterMetadataEpmMergeUtils.MergeSyndicationMapping<AtomPersonMetadata>(customEntryMetadata.Authors, epmEntryMetadata.Authors);
			epmEntryMetadata.Contributors = ODataAtomWriterMetadataEpmMergeUtils.MergeSyndicationMapping<AtomPersonMetadata>(customEntryMetadata.Contributors, epmEntryMetadata.Contributors);
			epmEntryMetadata.Categories = ODataAtomWriterMetadataEpmMergeUtils.MergeSyndicationMapping<AtomCategoryMetadata>(customEntryMetadata.Categories, epmEntryMetadata.Categories);
			epmEntryMetadata.Links = ODataAtomWriterMetadataEpmMergeUtils.MergeSyndicationMapping<AtomLinkMetadata>(customEntryMetadata.Links, epmEntryMetadata.Links);
			epmEntryMetadata.Source = customEntryMetadata.Source;
			return epmEntryMetadata;
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x0004CE60 File Offset: 0x0004B060
		private static IEnumerable<T> MergeSyndicationMapping<T>(IEnumerable<T> customValues, IEnumerable<T> epmValues)
		{
			IEnumerable<T> enumerable;
			if (ODataAtomWriterMetadataEpmMergeUtils.TryMergeIfNull<IEnumerable<T>>(customValues, epmValues, out enumerable))
			{
				return enumerable;
			}
			List<T> list = (List<T>)epmValues;
			foreach (T t in customValues)
			{
				list.Add(t);
			}
			return list;
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x0004CEC0 File Offset: 0x0004B0C0
		private static AtomTextConstruct MergeAtomTextValue(AtomTextConstruct customValue, AtomTextConstruct epmValue, string propertyName)
		{
			AtomTextConstruct atomTextConstruct;
			if (ODataAtomWriterMetadataEpmMergeUtils.TryMergeIfNull<AtomTextConstruct>(customValue, epmValue, out atomTextConstruct))
			{
				return atomTextConstruct;
			}
			if (customValue.Kind != epmValue.Kind)
			{
				throw new ODataException(Strings.ODataAtomMetadataEpmMerge_TextKindConflict(propertyName, customValue.Kind.ToString(), epmValue.Kind.ToString()));
			}
			if (string.CompareOrdinal(customValue.Text, epmValue.Text) != 0)
			{
				throw new ODataException(Strings.ODataAtomMetadataEpmMerge_TextValueConflict(propertyName, customValue.Text, epmValue.Text));
			}
			return epmValue;
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x0004CF40 File Offset: 0x0004B140
		private static string MergeTextValue(string customValue, string epmValue, string propertyName)
		{
			string text;
			if (ODataAtomWriterMetadataEpmMergeUtils.TryMergeIfNull<string>(customValue, epmValue, out text))
			{
				return text;
			}
			if (string.CompareOrdinal(customValue, epmValue) != 0)
			{
				throw new ODataException(Strings.ODataAtomMetadataEpmMerge_TextValueConflict(propertyName, customValue, epmValue));
			}
			return epmValue;
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x0004CF74 File Offset: 0x0004B174
		private static DateTimeOffset? MergeDateTimeValue(DateTimeOffset? customValue, DateTimeOffset? epmValue, string propertyName)
		{
			DateTimeOffset? dateTimeOffset;
			if (ODataAtomWriterMetadataEpmMergeUtils.TryMergeIfNull<DateTimeOffset>(customValue, epmValue, out dateTimeOffset))
			{
				return dateTimeOffset;
			}
			if (customValue != epmValue)
			{
				throw new ODataException(Strings.ODataAtomMetadataEpmMerge_TextValueConflict(propertyName, customValue.ToString(), epmValue.ToString()));
			}
			return epmValue;
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x0004CFED File Offset: 0x0004B1ED
		private static bool TryMergeIfNull<T>(T customValue, T epmValue, out T result) where T : class
		{
			if (customValue == null)
			{
				result = epmValue;
				return true;
			}
			if (epmValue == null)
			{
				result = customValue;
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x0004D019 File Offset: 0x0004B219
		private static bool TryMergeIfNull<T>(T? customValue, T? epmValue, out T? result) where T : struct
		{
			if (customValue == null)
			{
				result = epmValue;
				return true;
			}
			if (epmValue == null)
			{
				result = customValue;
				return true;
			}
			result = null;
			return false;
		}
	}
}
