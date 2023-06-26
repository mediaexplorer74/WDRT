using System;

namespace System.Runtime.InteropServices.TCEAdapterGen
{
	// Token: 0x020009C2 RID: 2498
	internal static class NameSpaceExtractor
	{
		// Token: 0x060063CD RID: 25549 RVA: 0x00155D18 File Offset: 0x00153F18
		public static string ExtractNameSpace(string FullyQualifiedTypeName)
		{
			int num = FullyQualifiedTypeName.LastIndexOf(NameSpaceExtractor.NameSpaceSeperator);
			if (num == -1)
			{
				return "";
			}
			return FullyQualifiedTypeName.Substring(0, num);
		}

		// Token: 0x04002CDD RID: 11485
		private static char NameSpaceSeperator = '.';
	}
}
