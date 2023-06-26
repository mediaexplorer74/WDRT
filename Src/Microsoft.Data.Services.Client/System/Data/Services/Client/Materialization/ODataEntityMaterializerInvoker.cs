using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Data.OData;

namespace System.Data.Services.Client.Materialization
{
	// Token: 0x0200006E RID: 110
	internal static class ODataEntityMaterializerInvoker
	{
		// Token: 0x060003B5 RID: 949 RVA: 0x00010118 File Offset: 0x0000E318
		internal static IEnumerable<T> EnumerateAsElementType<T>(IEnumerable source)
		{
			return ODataEntityMaterializer.EnumerateAsElementType<T>(source);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00010120 File Offset: 0x0000E320
		internal static List<TTarget> ListAsElementType<T, TTarget>(object materializer, IEnumerable<T> source) where T : TTarget
		{
			return ODataEntityMaterializer.ListAsElementType<T, TTarget>((ODataEntityMaterializer)materializer, source);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0001012E File Offset: 0x0000E32E
		internal static bool ProjectionCheckValueForPathIsNull(object entry, Type expectedType, object path)
		{
			return ODataEntityMaterializer.ProjectionCheckValueForPathIsNull(MaterializerEntry.GetEntry((ODataEntry)entry), expectedType, (ProjectionPath)path);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00010147 File Offset: 0x0000E347
		internal static IEnumerable ProjectionSelect(object materializer, object entry, Type expectedType, Type resultType, object path, Func<object, object, Type, object> selector)
		{
			return ODataEntityMaterializer.ProjectionSelect((ODataEntityMaterializer)materializer, MaterializerEntry.GetEntry((ODataEntry)entry), expectedType, resultType, (ProjectionPath)path, selector);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0001016A File Offset: 0x0000E36A
		internal static object ProjectionGetEntry(object entry, string name)
		{
			return ODataEntityMaterializer.ProjectionGetEntry(MaterializerEntry.GetEntry((ODataEntry)entry), name);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0001017D File Offset: 0x0000E37D
		internal static object ProjectionGetEntryOrNull(object entry, string name)
		{
			return ODataEntityMaterializer.ProjectionGetEntryOrNull(MaterializerEntry.GetEntry((ODataEntry)entry), name);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00010190 File Offset: 0x0000E390
		internal static object ProjectionInitializeEntity(object materializer, object entry, Type expectedType, Type resultType, string[] properties, Func<object, object, Type, object>[] propertyValues)
		{
			return ODataEntityMaterializer.ProjectionInitializeEntity((ODataEntityMaterializer)materializer, MaterializerEntry.GetEntry((ODataEntry)entry), expectedType, resultType, properties, propertyValues);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000101AE File Offset: 0x0000E3AE
		internal static object ProjectionValueForPath(object materializer, object entry, Type expectedType, object path)
		{
			return ((ODataEntityMaterializer)materializer).ProjectionValueForPath(MaterializerEntry.GetEntry((ODataEntry)entry), expectedType, (ProjectionPath)path);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x000101CD File Offset: 0x0000E3CD
		internal static object DirectMaterializePlan(object materializer, object entry, Type expectedEntryType)
		{
			return ODataEntityMaterializer.DirectMaterializePlan((ODataEntityMaterializer)materializer, MaterializerEntry.GetEntry((ODataEntry)entry), expectedEntryType);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000101E6 File Offset: 0x0000E3E6
		internal static object ShallowMaterializePlan(object materializer, object entry, Type expectedEntryType)
		{
			return ODataEntityMaterializer.ShallowMaterializePlan((ODataEntityMaterializer)materializer, MaterializerEntry.GetEntry((ODataEntry)entry), expectedEntryType);
		}
	}
}
