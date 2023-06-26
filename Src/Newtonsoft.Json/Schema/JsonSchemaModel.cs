using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020000AB RID: 171
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaModel
	{
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x000268C0 File Offset: 0x00024AC0
		// (set) Token: 0x06000915 RID: 2325 RVA: 0x000268C8 File Offset: 0x00024AC8
		public bool Required { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x000268D1 File Offset: 0x00024AD1
		// (set) Token: 0x06000917 RID: 2327 RVA: 0x000268D9 File Offset: 0x00024AD9
		public JsonSchemaType Type { get; set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x000268E2 File Offset: 0x00024AE2
		// (set) Token: 0x06000919 RID: 2329 RVA: 0x000268EA File Offset: 0x00024AEA
		public int? MinimumLength { get; set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x000268F3 File Offset: 0x00024AF3
		// (set) Token: 0x0600091B RID: 2331 RVA: 0x000268FB File Offset: 0x00024AFB
		public int? MaximumLength { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x00026904 File Offset: 0x00024B04
		// (set) Token: 0x0600091D RID: 2333 RVA: 0x0002690C File Offset: 0x00024B0C
		public double? DivisibleBy { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x00026915 File Offset: 0x00024B15
		// (set) Token: 0x0600091F RID: 2335 RVA: 0x0002691D File Offset: 0x00024B1D
		public double? Minimum { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x00026926 File Offset: 0x00024B26
		// (set) Token: 0x06000921 RID: 2337 RVA: 0x0002692E File Offset: 0x00024B2E
		public double? Maximum { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x00026937 File Offset: 0x00024B37
		// (set) Token: 0x06000923 RID: 2339 RVA: 0x0002693F File Offset: 0x00024B3F
		public bool ExclusiveMinimum { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x00026948 File Offset: 0x00024B48
		// (set) Token: 0x06000925 RID: 2341 RVA: 0x00026950 File Offset: 0x00024B50
		public bool ExclusiveMaximum { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x00026959 File Offset: 0x00024B59
		// (set) Token: 0x06000927 RID: 2343 RVA: 0x00026961 File Offset: 0x00024B61
		public int? MinimumItems { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0002696A File Offset: 0x00024B6A
		// (set) Token: 0x06000929 RID: 2345 RVA: 0x00026972 File Offset: 0x00024B72
		public int? MaximumItems { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0002697B File Offset: 0x00024B7B
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x00026983 File Offset: 0x00024B83
		public IList<string> Patterns { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0002698C File Offset: 0x00024B8C
		// (set) Token: 0x0600092D RID: 2349 RVA: 0x00026994 File Offset: 0x00024B94
		public IList<JsonSchemaModel> Items { get; set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x0002699D File Offset: 0x00024B9D
		// (set) Token: 0x0600092F RID: 2351 RVA: 0x000269A5 File Offset: 0x00024BA5
		public IDictionary<string, JsonSchemaModel> Properties { get; set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x000269AE File Offset: 0x00024BAE
		// (set) Token: 0x06000931 RID: 2353 RVA: 0x000269B6 File Offset: 0x00024BB6
		public IDictionary<string, JsonSchemaModel> PatternProperties { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x000269BF File Offset: 0x00024BBF
		// (set) Token: 0x06000933 RID: 2355 RVA: 0x000269C7 File Offset: 0x00024BC7
		public JsonSchemaModel AdditionalProperties { get; set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x000269D0 File Offset: 0x00024BD0
		// (set) Token: 0x06000935 RID: 2357 RVA: 0x000269D8 File Offset: 0x00024BD8
		public JsonSchemaModel AdditionalItems { get; set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x000269E1 File Offset: 0x00024BE1
		// (set) Token: 0x06000937 RID: 2359 RVA: 0x000269E9 File Offset: 0x00024BE9
		public bool PositionalItemsValidation { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x000269F2 File Offset: 0x00024BF2
		// (set) Token: 0x06000939 RID: 2361 RVA: 0x000269FA File Offset: 0x00024BFA
		public bool AllowAdditionalProperties { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x00026A03 File Offset: 0x00024C03
		// (set) Token: 0x0600093B RID: 2363 RVA: 0x00026A0B File Offset: 0x00024C0B
		public bool AllowAdditionalItems { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x00026A14 File Offset: 0x00024C14
		// (set) Token: 0x0600093D RID: 2365 RVA: 0x00026A1C File Offset: 0x00024C1C
		public bool UniqueItems { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x00026A25 File Offset: 0x00024C25
		// (set) Token: 0x0600093F RID: 2367 RVA: 0x00026A2D File Offset: 0x00024C2D
		public IList<JToken> Enum { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00026A36 File Offset: 0x00024C36
		// (set) Token: 0x06000941 RID: 2369 RVA: 0x00026A3E File Offset: 0x00024C3E
		public JsonSchemaType Disallow { get; set; }

		// Token: 0x06000942 RID: 2370 RVA: 0x00026A47 File Offset: 0x00024C47
		public JsonSchemaModel()
		{
			this.Type = JsonSchemaType.Any;
			this.AllowAdditionalProperties = true;
			this.AllowAdditionalItems = true;
			this.Required = false;
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00026A6C File Offset: 0x00024C6C
		public static JsonSchemaModel Create(IList<JsonSchema> schemata)
		{
			JsonSchemaModel jsonSchemaModel = new JsonSchemaModel();
			foreach (JsonSchema jsonSchema in schemata)
			{
				JsonSchemaModel.Combine(jsonSchemaModel, jsonSchema);
			}
			return jsonSchemaModel;
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00026ABC File Offset: 0x00024CBC
		private static void Combine(JsonSchemaModel model, JsonSchema schema)
		{
			model.Required = model.Required || schema.Required.GetValueOrDefault();
			model.Type &= schema.Type ?? JsonSchemaType.Any;
			model.MinimumLength = MathUtils.Max(model.MinimumLength, schema.MinimumLength);
			model.MaximumLength = MathUtils.Min(model.MaximumLength, schema.MaximumLength);
			model.DivisibleBy = MathUtils.Max(model.DivisibleBy, schema.DivisibleBy);
			model.Minimum = MathUtils.Max(model.Minimum, schema.Minimum);
			model.Maximum = MathUtils.Max(model.Maximum, schema.Maximum);
			model.ExclusiveMinimum = model.ExclusiveMinimum || schema.ExclusiveMinimum.GetValueOrDefault();
			model.ExclusiveMaximum = model.ExclusiveMaximum || schema.ExclusiveMaximum.GetValueOrDefault();
			model.MinimumItems = MathUtils.Max(model.MinimumItems, schema.MinimumItems);
			model.MaximumItems = MathUtils.Min(model.MaximumItems, schema.MaximumItems);
			model.PositionalItemsValidation = model.PositionalItemsValidation || schema.PositionalItemsValidation;
			model.AllowAdditionalProperties = model.AllowAdditionalProperties && schema.AllowAdditionalProperties;
			model.AllowAdditionalItems = model.AllowAdditionalItems && schema.AllowAdditionalItems;
			model.UniqueItems = model.UniqueItems || schema.UniqueItems;
			if (schema.Enum != null)
			{
				if (model.Enum == null)
				{
					model.Enum = new List<JToken>();
				}
				model.Enum.AddRangeDistinct(schema.Enum, JToken.EqualityComparer);
			}
			model.Disallow |= schema.Disallow.GetValueOrDefault();
			if (schema.Pattern != null)
			{
				if (model.Patterns == null)
				{
					model.Patterns = new List<string>();
				}
				model.Patterns.AddDistinct(schema.Pattern);
			}
		}
	}
}
