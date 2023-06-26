using System;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200005F RID: 95
	internal static class JsonTokenUtils
	{
		// Token: 0x0600055A RID: 1370 RVA: 0x00017256 File Offset: 0x00015456
		internal static bool IsEndToken(JsonToken token)
		{
			return token - JsonToken.EndObject <= 2;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00017262 File Offset: 0x00015462
		internal static bool IsStartToken(JsonToken token)
		{
			return token - JsonToken.StartObject <= 2;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0001726D File Offset: 0x0001546D
		internal static bool IsPrimitiveToken(JsonToken token)
		{
			return token - JsonToken.Integer <= 5 || token - JsonToken.Date <= 1;
		}
	}
}
