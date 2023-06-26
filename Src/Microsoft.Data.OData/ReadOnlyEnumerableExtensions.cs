using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData
{
	// Token: 0x02000137 RID: 311
	internal static class ReadOnlyEnumerableExtensions
	{
		// Token: 0x0600082F RID: 2095 RVA: 0x0001ACE3 File Offset: 0x00018EE3
		internal static bool IsEmptyReadOnlyEnumerable<T>(this IEnumerable<T> source)
		{
			return object.ReferenceEquals(source, ReadOnlyEnumerable<T>.Empty());
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0001ACF0 File Offset: 0x00018EF0
		internal static ReadOnlyEnumerable<T> ToReadOnlyEnumerable<T>(this IEnumerable<T> source, string collectionName)
		{
			ReadOnlyEnumerable<T> readOnlyEnumerable = source as ReadOnlyEnumerable<T>;
			if (readOnlyEnumerable == null)
			{
				throw new ODataException(Strings.ReaderUtils_EnumerableModified(collectionName));
			}
			return readOnlyEnumerable;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001AD14 File Offset: 0x00018F14
		internal static ReadOnlyEnumerable<T> GetOrCreateReadOnlyEnumerable<T>(this IEnumerable<T> source, string collectionName)
		{
			if (source.IsEmptyReadOnlyEnumerable<T>())
			{
				return new ReadOnlyEnumerable<T>();
			}
			return source.ToReadOnlyEnumerable(collectionName);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001AD2C File Offset: 0x00018F2C
		internal static ReadOnlyEnumerable<T> ConcatToReadOnlyEnumerable<T>(this IEnumerable<T> source, string collectionName, T item)
		{
			ReadOnlyEnumerable<T> orCreateReadOnlyEnumerable = source.GetOrCreateReadOnlyEnumerable(collectionName);
			orCreateReadOnlyEnumerable.AddToSourceList(item);
			return orCreateReadOnlyEnumerable;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001AD49 File Offset: 0x00018F49
		internal static void AddAction(this ODataEntry entry, ODataAction action)
		{
			entry.Actions = entry.Actions.ConcatToReadOnlyEnumerable("Actions", action);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0001AD62 File Offset: 0x00018F62
		internal static void AddFunction(this ODataEntry entry, ODataFunction function)
		{
			entry.Functions = entry.Functions.ConcatToReadOnlyEnumerable("Functions", function);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0001AD7B File Offset: 0x00018F7B
		internal static void AddAssociationLink(this ODataEntry entry, ODataAssociationLink associationLink)
		{
			entry.AssociationLinks = entry.AssociationLinks.ConcatToReadOnlyEnumerable("AssociationLinks", associationLink);
		}
	}
}
