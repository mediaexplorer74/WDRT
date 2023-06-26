using System;
using System.Collections.Generic;
using Microsoft.Data.OData.Query.SemanticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x0200004D RID: 77
	internal static class ODataPathFactory
	{
		// Token: 0x060001FF RID: 511 RVA: 0x00007D78 File Offset: 0x00005F78
		internal static ODataPath BindPath(ICollection<string> segments, ODataUriParserConfiguration configuration)
		{
			ODataPathParser odataPathParser = new ODataPathParser(configuration);
			IList<ODataPathSegment> list = odataPathParser.ParsePath(segments);
			return new ODataPath(list);
		}
	}
}
