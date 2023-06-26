using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000C1 RID: 193
	[NullableContext(1)]
	[Nullable(0)]
	public class JRaw : JValue
	{
		// Token: 0x06000AAA RID: 2730 RVA: 0x0002AAB8 File Offset: 0x00028CB8
		public static async Task<JRaw> CreateAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			JRaw jraw;
			using (StringWriter sw = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (JsonTextWriter jsonWriter = new JsonTextWriter(sw))
				{
					await jsonWriter.WriteTokenSyncReadingAsync(reader, cancellationToken).ConfigureAwait(false);
					jraw = new JRaw(sw.ToString());
				}
			}
			return jraw;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0002AB03 File Offset: 0x00028D03
		public JRaw(JRaw other)
			: base(other)
		{
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0002AB0C File Offset: 0x00028D0C
		[NullableContext(2)]
		public JRaw(object rawJson)
			: base(rawJson, JTokenType.Raw)
		{
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0002AB18 File Offset: 0x00028D18
		public static JRaw Create(JsonReader reader)
		{
			JRaw jraw;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter))
				{
					jsonTextWriter.WriteToken(reader);
					jraw = new JRaw(stringWriter.ToString());
				}
			}
			return jraw;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0002AB80 File Offset: 0x00028D80
		internal override JToken CloneToken()
		{
			return new JRaw(this);
		}
	}
}
