using System;
using System.IO;
using System.Spatial;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000B0 RID: 176
	internal static class LiteralUtils
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000D4A8 File Offset: 0x0000B6A8
		private static WellKnownTextSqlFormatter Formatter
		{
			get
			{
				return SpatialImplementation.CurrentImplementation.CreateWellKnownTextSqlFormatter(false);
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000D4B8 File Offset: 0x0000B6B8
		internal static Geography ParseGeography(string text)
		{
			Geography geography;
			using (StringReader stringReader = new StringReader(text))
			{
				geography = LiteralUtils.Formatter.Read<Geography>(stringReader);
			}
			return geography;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000D4F8 File Offset: 0x0000B6F8
		internal static Geometry ParseGeometry(string text)
		{
			Geometry geometry;
			using (StringReader stringReader = new StringReader(text))
			{
				geometry = LiteralUtils.Formatter.Read<Geometry>(stringReader);
			}
			return geometry;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000D538 File Offset: 0x0000B738
		internal static string ToWellKnownText(Geography instance)
		{
			return LiteralUtils.Formatter.Write(instance);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000D545 File Offset: 0x0000B745
		internal static string ToWellKnownText(Geometry instance)
		{
			return LiteralUtils.Formatter.Write(instance);
		}
	}
}
