using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020000AD RID: 173
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaNode
	{
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x00027126 File Offset: 0x00025326
		public string Id { get; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x0002712E File Offset: 0x0002532E
		public ReadOnlyCollection<JsonSchema> Schemas { get; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x00027136 File Offset: 0x00025336
		public Dictionary<string, JsonSchemaNode> Properties { get; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x0002713E File Offset: 0x0002533E
		public Dictionary<string, JsonSchemaNode> PatternProperties { get; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x00027146 File Offset: 0x00025346
		public List<JsonSchemaNode> Items { get; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x0002714E File Offset: 0x0002534E
		// (set) Token: 0x06000954 RID: 2388 RVA: 0x00027156 File Offset: 0x00025356
		public JsonSchemaNode AdditionalProperties { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x0002715F File Offset: 0x0002535F
		// (set) Token: 0x06000956 RID: 2390 RVA: 0x00027167 File Offset: 0x00025367
		public JsonSchemaNode AdditionalItems { get; set; }

		// Token: 0x06000957 RID: 2391 RVA: 0x00027170 File Offset: 0x00025370
		public JsonSchemaNode(JsonSchema schema)
		{
			this.Schemas = new ReadOnlyCollection<JsonSchema>(new JsonSchema[] { schema });
			this.Properties = new Dictionary<string, JsonSchemaNode>();
			this.PatternProperties = new Dictionary<string, JsonSchemaNode>();
			this.Items = new List<JsonSchemaNode>();
			this.Id = JsonSchemaNode.GetId(this.Schemas);
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x000271CC File Offset: 0x000253CC
		private JsonSchemaNode(JsonSchemaNode source, JsonSchema schema)
		{
			this.Schemas = new ReadOnlyCollection<JsonSchema>(source.Schemas.Union(new JsonSchema[] { schema }).ToList<JsonSchema>());
			this.Properties = new Dictionary<string, JsonSchemaNode>(source.Properties);
			this.PatternProperties = new Dictionary<string, JsonSchemaNode>(source.PatternProperties);
			this.Items = new List<JsonSchemaNode>(source.Items);
			this.AdditionalProperties = source.AdditionalProperties;
			this.AdditionalItems = source.AdditionalItems;
			this.Id = JsonSchemaNode.GetId(this.Schemas);
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00027260 File Offset: 0x00025460
		public JsonSchemaNode Combine(JsonSchema schema)
		{
			return new JsonSchemaNode(this, schema);
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0002726C File Offset: 0x0002546C
		public static string GetId(IEnumerable<JsonSchema> schemata)
		{
			return string.Join("-", schemata.Select((JsonSchema s) => s.InternalId).OrderBy((string id) => id, StringComparer.Ordinal));
		}
	}
}
