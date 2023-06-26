using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020000CE RID: 206
	internal class ArrayIndexFilter : PathFilter
	{
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x0002EFF1 File Offset: 0x0002D1F1
		// (set) Token: 0x06000BDD RID: 3037 RVA: 0x0002EFF9 File Offset: 0x0002D1F9
		public int? Index { get; set; }

		// Token: 0x06000BDE RID: 3038 RVA: 0x0002F002 File Offset: 0x0002D202
		[NullableContext(1)]
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, [Nullable(2)] JsonSelectSettings settings)
		{
			foreach (JToken jtoken in current)
			{
				if (this.Index != null)
				{
					JToken tokenIndex = PathFilter.GetTokenIndex(jtoken, settings, this.Index.GetValueOrDefault());
					if (tokenIndex != null)
					{
						yield return tokenIndex;
					}
				}
				else if (jtoken is JArray || jtoken is JConstructor)
				{
					foreach (JToken jtoken2 in ((IEnumerable<JToken>)jtoken))
					{
						yield return jtoken2;
					}
					IEnumerator<JToken> enumerator2 = null;
				}
				else if (settings != null && settings.ErrorWhenNoMatch)
				{
					throw new JsonException("Index * not valid on {0}.".FormatWith(CultureInfo.InvariantCulture, jtoken.GetType().Name));
				}
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}
	}
}
