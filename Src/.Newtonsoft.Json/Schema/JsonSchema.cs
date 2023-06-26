using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020000A6 RID: 166
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class JsonSchema
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x000249C8 File Offset: 0x00022BC8
		// (set) Token: 0x06000893 RID: 2195 RVA: 0x000249D0 File Offset: 0x00022BD0
		public string Id { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x000249D9 File Offset: 0x00022BD9
		// (set) Token: 0x06000895 RID: 2197 RVA: 0x000249E1 File Offset: 0x00022BE1
		public string Title { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x000249EA File Offset: 0x00022BEA
		// (set) Token: 0x06000897 RID: 2199 RVA: 0x000249F2 File Offset: 0x00022BF2
		public bool? Required { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x000249FB File Offset: 0x00022BFB
		// (set) Token: 0x06000899 RID: 2201 RVA: 0x00024A03 File Offset: 0x00022C03
		public bool? ReadOnly { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x00024A0C File Offset: 0x00022C0C
		// (set) Token: 0x0600089B RID: 2203 RVA: 0x00024A14 File Offset: 0x00022C14
		public bool? Hidden { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x00024A1D File Offset: 0x00022C1D
		// (set) Token: 0x0600089D RID: 2205 RVA: 0x00024A25 File Offset: 0x00022C25
		public bool? Transient { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x00024A2E File Offset: 0x00022C2E
		// (set) Token: 0x0600089F RID: 2207 RVA: 0x00024A36 File Offset: 0x00022C36
		public string Description { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x00024A3F File Offset: 0x00022C3F
		// (set) Token: 0x060008A1 RID: 2209 RVA: 0x00024A47 File Offset: 0x00022C47
		public JsonSchemaType? Type { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x00024A50 File Offset: 0x00022C50
		// (set) Token: 0x060008A3 RID: 2211 RVA: 0x00024A58 File Offset: 0x00022C58
		public string Pattern { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x00024A61 File Offset: 0x00022C61
		// (set) Token: 0x060008A5 RID: 2213 RVA: 0x00024A69 File Offset: 0x00022C69
		public int? MinimumLength { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x00024A72 File Offset: 0x00022C72
		// (set) Token: 0x060008A7 RID: 2215 RVA: 0x00024A7A File Offset: 0x00022C7A
		public int? MaximumLength { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x00024A83 File Offset: 0x00022C83
		// (set) Token: 0x060008A9 RID: 2217 RVA: 0x00024A8B File Offset: 0x00022C8B
		public double? DivisibleBy { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x00024A94 File Offset: 0x00022C94
		// (set) Token: 0x060008AB RID: 2219 RVA: 0x00024A9C File Offset: 0x00022C9C
		public double? Minimum { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x00024AA5 File Offset: 0x00022CA5
		// (set) Token: 0x060008AD RID: 2221 RVA: 0x00024AAD File Offset: 0x00022CAD
		public double? Maximum { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x00024AB6 File Offset: 0x00022CB6
		// (set) Token: 0x060008AF RID: 2223 RVA: 0x00024ABE File Offset: 0x00022CBE
		public bool? ExclusiveMinimum { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x00024AC7 File Offset: 0x00022CC7
		// (set) Token: 0x060008B1 RID: 2225 RVA: 0x00024ACF File Offset: 0x00022CCF
		public bool? ExclusiveMaximum { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x00024AD8 File Offset: 0x00022CD8
		// (set) Token: 0x060008B3 RID: 2227 RVA: 0x00024AE0 File Offset: 0x00022CE0
		public int? MinimumItems { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x00024AE9 File Offset: 0x00022CE9
		// (set) Token: 0x060008B5 RID: 2229 RVA: 0x00024AF1 File Offset: 0x00022CF1
		public int? MaximumItems { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x00024AFA File Offset: 0x00022CFA
		// (set) Token: 0x060008B7 RID: 2231 RVA: 0x00024B02 File Offset: 0x00022D02
		public IList<JsonSchema> Items { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x00024B0B File Offset: 0x00022D0B
		// (set) Token: 0x060008B9 RID: 2233 RVA: 0x00024B13 File Offset: 0x00022D13
		public bool PositionalItemsValidation { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x00024B1C File Offset: 0x00022D1C
		// (set) Token: 0x060008BB RID: 2235 RVA: 0x00024B24 File Offset: 0x00022D24
		public JsonSchema AdditionalItems { get; set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x00024B2D File Offset: 0x00022D2D
		// (set) Token: 0x060008BD RID: 2237 RVA: 0x00024B35 File Offset: 0x00022D35
		public bool AllowAdditionalItems { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x00024B3E File Offset: 0x00022D3E
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x00024B46 File Offset: 0x00022D46
		public bool UniqueItems { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x00024B4F File Offset: 0x00022D4F
		// (set) Token: 0x060008C1 RID: 2241 RVA: 0x00024B57 File Offset: 0x00022D57
		public IDictionary<string, JsonSchema> Properties { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x00024B60 File Offset: 0x00022D60
		// (set) Token: 0x060008C3 RID: 2243 RVA: 0x00024B68 File Offset: 0x00022D68
		public JsonSchema AdditionalProperties { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x00024B71 File Offset: 0x00022D71
		// (set) Token: 0x060008C5 RID: 2245 RVA: 0x00024B79 File Offset: 0x00022D79
		public IDictionary<string, JsonSchema> PatternProperties { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x00024B82 File Offset: 0x00022D82
		// (set) Token: 0x060008C7 RID: 2247 RVA: 0x00024B8A File Offset: 0x00022D8A
		public bool AllowAdditionalProperties { get; set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x00024B93 File Offset: 0x00022D93
		// (set) Token: 0x060008C9 RID: 2249 RVA: 0x00024B9B File Offset: 0x00022D9B
		public string Requires { get; set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x00024BA4 File Offset: 0x00022DA4
		// (set) Token: 0x060008CB RID: 2251 RVA: 0x00024BAC File Offset: 0x00022DAC
		public IList<JToken> Enum { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x00024BB5 File Offset: 0x00022DB5
		// (set) Token: 0x060008CD RID: 2253 RVA: 0x00024BBD File Offset: 0x00022DBD
		public JsonSchemaType? Disallow { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x00024BC6 File Offset: 0x00022DC6
		// (set) Token: 0x060008CF RID: 2255 RVA: 0x00024BCE File Offset: 0x00022DCE
		public JToken Default { get; set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x00024BD7 File Offset: 0x00022DD7
		// (set) Token: 0x060008D1 RID: 2257 RVA: 0x00024BDF File Offset: 0x00022DDF
		public IList<JsonSchema> Extends { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x00024BE8 File Offset: 0x00022DE8
		// (set) Token: 0x060008D3 RID: 2259 RVA: 0x00024BF0 File Offset: 0x00022DF0
		public string Format { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x00024BF9 File Offset: 0x00022DF9
		// (set) Token: 0x060008D5 RID: 2261 RVA: 0x00024C01 File Offset: 0x00022E01
		internal string Location { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x00024C0A File Offset: 0x00022E0A
		internal string InternalId
		{
			get
			{
				return this._internalId;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x00024C12 File Offset: 0x00022E12
		// (set) Token: 0x060008D8 RID: 2264 RVA: 0x00024C1A File Offset: 0x00022E1A
		internal string DeferredReference { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x00024C23 File Offset: 0x00022E23
		// (set) Token: 0x060008DA RID: 2266 RVA: 0x00024C2B File Offset: 0x00022E2B
		internal bool ReferencesResolved { get; set; }

		// Token: 0x060008DB RID: 2267 RVA: 0x00024C34 File Offset: 0x00022E34
		public JsonSchema()
		{
			this.AllowAdditionalProperties = true;
			this.AllowAdditionalItems = true;
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00024C6D File Offset: 0x00022E6D
		public static JsonSchema Read(JsonReader reader)
		{
			return JsonSchema.Read(reader, new JsonSchemaResolver());
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00024C7A File Offset: 0x00022E7A
		public static JsonSchema Read(JsonReader reader, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			ValidationUtils.ArgumentNotNull(resolver, "resolver");
			return new JsonSchemaBuilder(resolver).Read(reader);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00024C9E File Offset: 0x00022E9E
		public static JsonSchema Parse(string json)
		{
			return JsonSchema.Parse(json, new JsonSchemaResolver());
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00024CAC File Offset: 0x00022EAC
		public static JsonSchema Parse(string json, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(json, "json");
			JsonSchema jsonSchema;
			using (JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
			{
				jsonSchema = JsonSchema.Read(jsonReader, resolver);
			}
			return jsonSchema;
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00024CF8 File Offset: 0x00022EF8
		public void WriteTo(JsonWriter writer)
		{
			this.WriteTo(writer, new JsonSchemaResolver());
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00024D06 File Offset: 0x00022F06
		public void WriteTo(JsonWriter writer, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(writer, "writer");
			ValidationUtils.ArgumentNotNull(resolver, "resolver");
			new JsonSchemaWriter(writer, resolver).WriteSchema(this);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00024D2C File Offset: 0x00022F2C
		public override string ToString()
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			this.WriteTo(new JsonTextWriter(stringWriter)
			{
				Formatting = Formatting.Indented
			});
			return stringWriter.ToString();
		}

		// Token: 0x040002F3 RID: 755
		private readonly string _internalId = Guid.NewGuid().ToString("N");
	}
}
