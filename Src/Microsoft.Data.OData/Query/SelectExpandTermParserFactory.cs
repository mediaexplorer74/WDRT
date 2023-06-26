using System;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x0200005E RID: 94
	internal static class SelectExpandTermParserFactory
	{
		// Token: 0x06000267 RID: 615 RVA: 0x00009BDA File Offset: 0x00007DDA
		public static ISelectExpandTermParser Create(string clauseToParse, ODataUriParserSettings settings)
		{
			if (settings.SupportExpandOptions)
			{
				return new ExpandOptionSelectExpandTermParser(clauseToParse, settings.SelectExpandLimit);
			}
			return new NonOptionSelectExpandTermParser(clauseToParse, settings.SelectExpandLimit);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00009BFD File Offset: 0x00007DFD
		public static ISelectExpandTermParser Create(string clauseToParse)
		{
			return new NonOptionSelectExpandTermParser(clauseToParse, 800);
		}
	}
}
