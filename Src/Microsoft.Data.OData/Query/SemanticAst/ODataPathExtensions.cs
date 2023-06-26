using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x0200004C RID: 76
	internal static class ODataPathExtensions
	{
		// Token: 0x060001FC RID: 508 RVA: 0x00007D40 File Offset: 0x00005F40
		public static IEdmTypeReference EdmType(this ODataPath path)
		{
			return path.LastSegment.EdmType.ToTypeReference();
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00007D52 File Offset: 0x00005F52
		public static IEdmEntitySet EntitySet(this ODataPath path)
		{
			return path.LastSegment.Translate<IEdmEntitySet>(new DetermineEntitySetTranslator());
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00007D64 File Offset: 0x00005F64
		public static bool IsCollection(this ODataPath path)
		{
			return path.LastSegment.Translate<bool>(new IsCollectionTranslator());
		}
	}
}
