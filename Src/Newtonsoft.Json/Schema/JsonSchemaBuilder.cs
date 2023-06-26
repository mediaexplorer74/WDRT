using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020000A7 RID: 167
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaBuilder
	{
		// Token: 0x060008E3 RID: 2275 RVA: 0x00024D5D File Offset: 0x00022F5D
		public JsonSchemaBuilder(JsonSchemaResolver resolver)
		{
			this._stack = new List<JsonSchema>();
			this._documentSchemas = new Dictionary<string, JsonSchema>();
			this._resolver = resolver;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00024D82 File Offset: 0x00022F82
		private void Push(JsonSchema value)
		{
			this._currentSchema = value;
			this._stack.Add(value);
			this._resolver.LoadedSchemas.Add(value);
			this._documentSchemas.Add(value.Location, value);
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00024DBA File Offset: 0x00022FBA
		private JsonSchema Pop()
		{
			JsonSchema currentSchema = this._currentSchema;
			this._stack.RemoveAt(this._stack.Count - 1);
			this._currentSchema = this._stack.LastOrDefault<JsonSchema>();
			return currentSchema;
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x00024DEB File Offset: 0x00022FEB
		private JsonSchema CurrentSchema
		{
			get
			{
				return this._currentSchema;
			}
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00024DF4 File Offset: 0x00022FF4
		internal JsonSchema Read(JsonReader reader)
		{
			JToken jtoken = JToken.ReadFrom(reader);
			this._rootSchema = jtoken as JObject;
			JsonSchema jsonSchema = this.BuildSchema(jtoken);
			this.ResolveReferences(jsonSchema);
			return jsonSchema;
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00024E25 File Offset: 0x00023025
		private string UnescapeReference(string reference)
		{
			return Uri.UnescapeDataString(reference).Replace("~1", "/").Replace("~0", "~");
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00024E4C File Offset: 0x0002304C
		private JsonSchema ResolveReferences(JsonSchema schema)
		{
			if (schema.DeferredReference != null)
			{
				string text = schema.DeferredReference;
				bool flag = text.StartsWith("#", StringComparison.Ordinal);
				if (flag)
				{
					text = this.UnescapeReference(text);
				}
				JsonSchema jsonSchema = this._resolver.GetSchema(text);
				if (jsonSchema == null)
				{
					if (flag)
					{
						string[] array = schema.DeferredReference.TrimStart(new char[] { '#' }).Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
						JToken jtoken = this._rootSchema;
						foreach (string text2 in array)
						{
							string text3 = this.UnescapeReference(text2);
							if (jtoken.Type == JTokenType.Object)
							{
								jtoken = jtoken[text3];
							}
							else if (jtoken.Type == JTokenType.Array || jtoken.Type == JTokenType.Constructor)
							{
								int num;
								if (int.TryParse(text3, out num) && num >= 0 && num < jtoken.Count<JToken>())
								{
									jtoken = jtoken[num];
								}
								else
								{
									jtoken = null;
								}
							}
							if (jtoken == null)
							{
								break;
							}
						}
						if (jtoken != null)
						{
							jsonSchema = this.BuildSchema(jtoken);
						}
					}
					if (jsonSchema == null)
					{
						throw new JsonException("Could not resolve schema reference '{0}'.".FormatWith(CultureInfo.InvariantCulture, schema.DeferredReference));
					}
				}
				schema = jsonSchema;
			}
			if (schema.ReferencesResolved)
			{
				return schema;
			}
			schema.ReferencesResolved = true;
			if (schema.Extends != null)
			{
				for (int j = 0; j < schema.Extends.Count; j++)
				{
					schema.Extends[j] = this.ResolveReferences(schema.Extends[j]);
				}
			}
			if (schema.Items != null)
			{
				for (int k = 0; k < schema.Items.Count; k++)
				{
					schema.Items[k] = this.ResolveReferences(schema.Items[k]);
				}
			}
			if (schema.AdditionalItems != null)
			{
				schema.AdditionalItems = this.ResolveReferences(schema.AdditionalItems);
			}
			if (schema.PatternProperties != null)
			{
				foreach (KeyValuePair<string, JsonSchema> keyValuePair in schema.PatternProperties.ToList<KeyValuePair<string, JsonSchema>>())
				{
					schema.PatternProperties[keyValuePair.Key] = this.ResolveReferences(keyValuePair.Value);
				}
			}
			if (schema.Properties != null)
			{
				foreach (KeyValuePair<string, JsonSchema> keyValuePair2 in schema.Properties.ToList<KeyValuePair<string, JsonSchema>>())
				{
					schema.Properties[keyValuePair2.Key] = this.ResolveReferences(keyValuePair2.Value);
				}
			}
			if (schema.AdditionalProperties != null)
			{
				schema.AdditionalProperties = this.ResolveReferences(schema.AdditionalProperties);
			}
			return schema;
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00025114 File Offset: 0x00023314
		private JsonSchema BuildSchema(JToken token)
		{
			JObject jobject = token as JObject;
			if (jobject == null)
			{
				throw JsonException.Create(token, token.Path, "Expected object while parsing schema object, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			JToken jtoken;
			if (jobject.TryGetValue("$ref", out jtoken))
			{
				return new JsonSchema
				{
					DeferredReference = (string)jtoken
				};
			}
			string text = token.Path.Replace(".", "/").Replace("[", "/").Replace("]", string.Empty);
			if (!StringUtils.IsNullOrEmpty(text))
			{
				text = "/" + text;
			}
			text = "#" + text;
			JsonSchema jsonSchema;
			if (this._documentSchemas.TryGetValue(text, out jsonSchema))
			{
				return jsonSchema;
			}
			this.Push(new JsonSchema
			{
				Location = text
			});
			this.ProcessSchemaProperties(jobject);
			return this.Pop();
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x000251F8 File Offset: 0x000233F8
		private void ProcessSchemaProperties(JObject schemaObject)
		{
			foreach (KeyValuePair<string, JToken> keyValuePair in schemaObject)
			{
				string key = keyValuePair.Key;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(key);
				if (num <= 2223801888U)
				{
					if (num <= 981021583U)
					{
						if (num <= 353304077U)
						{
							if (num != 299789532U)
							{
								if (num != 334560121U)
								{
									if (num == 353304077U)
									{
										if (key == "divisibleBy")
										{
											this.CurrentSchema.DivisibleBy = new double?((double)keyValuePair.Value);
										}
									}
								}
								else if (key == "minItems")
								{
									this.CurrentSchema.MinimumItems = new int?((int)keyValuePair.Value);
								}
							}
							else if (key == "properties")
							{
								this.CurrentSchema.Properties = this.ProcessProperties(keyValuePair.Value);
							}
						}
						else if (num <= 879704937U)
						{
							if (num != 479998177U)
							{
								if (num == 879704937U)
								{
									if (key == "description")
									{
										this.CurrentSchema.Description = (string)keyValuePair.Value;
									}
								}
							}
							else if (key == "additionalProperties")
							{
								this.ProcessAdditionalProperties(keyValuePair.Value);
							}
						}
						else if (num != 926444256U)
						{
							if (num == 981021583U)
							{
								if (key == "items")
								{
									this.ProcessItems(keyValuePair.Value);
								}
							}
						}
						else if (key == "id")
						{
							this.CurrentSchema.Id = (string)keyValuePair.Value;
						}
					}
					else if (num <= 1693958795U)
					{
						if (num != 1361572173U)
						{
							if (num != 1542649473U)
							{
								if (num == 1693958795U)
								{
									if (key == "exclusiveMaximum")
									{
										this.CurrentSchema.ExclusiveMaximum = new bool?((bool)keyValuePair.Value);
									}
								}
							}
							else if (key == "maximum")
							{
								this.CurrentSchema.Maximum = new double?((double)keyValuePair.Value);
							}
						}
						else if (key == "type")
						{
							this.CurrentSchema.Type = this.ProcessType(keyValuePair.Value);
						}
					}
					else if (num <= 2051482624U)
					{
						if (num != 1913005517U)
						{
							if (num == 2051482624U)
							{
								if (key == "additionalItems")
								{
									this.ProcessAdditionalItems(keyValuePair.Value);
								}
							}
						}
						else if (key == "exclusiveMinimum")
						{
							this.CurrentSchema.ExclusiveMinimum = new bool?((bool)keyValuePair.Value);
						}
					}
					else if (num != 2171383808U)
					{
						if (num == 2223801888U)
						{
							if (key == "required")
							{
								this.CurrentSchema.Required = new bool?((bool)keyValuePair.Value);
							}
						}
					}
					else if (key == "enum")
					{
						this.ProcessEnum(keyValuePair.Value);
					}
				}
				else if (num <= 2692244416U)
				{
					if (num <= 2474713847U)
					{
						if (num != 2268922153U)
						{
							if (num != 2470140894U)
							{
								if (num == 2474713847U)
								{
									if (key == "minimum")
									{
										this.CurrentSchema.Minimum = new double?((double)keyValuePair.Value);
									}
								}
							}
							else if (key == "default")
							{
								this.CurrentSchema.Default = keyValuePair.Value.DeepClone();
							}
						}
						else if (key == "pattern")
						{
							this.CurrentSchema.Pattern = (string)keyValuePair.Value;
						}
					}
					else if (num <= 2609687125U)
					{
						if (num != 2556802313U)
						{
							if (num == 2609687125U)
							{
								if (key == "requires")
								{
									this.CurrentSchema.Requires = (string)keyValuePair.Value;
								}
							}
						}
						else if (key == "title")
						{
							this.CurrentSchema.Title = (string)keyValuePair.Value;
						}
					}
					else if (num != 2642794062U)
					{
						if (num == 2692244416U)
						{
							if (key == "disallow")
							{
								this.CurrentSchema.Disallow = this.ProcessType(keyValuePair.Value);
							}
						}
					}
					else if (key == "extends")
					{
						this.ProcessExtends(keyValuePair.Value);
					}
				}
				else if (num <= 3522602594U)
				{
					if (num <= 3114108242U)
					{
						if (num != 2957261815U)
						{
							if (num == 3114108242U)
							{
								if (key == "format")
								{
									this.CurrentSchema.Format = (string)keyValuePair.Value;
								}
							}
						}
						else if (key == "minLength")
						{
							this.CurrentSchema.MinimumLength = new int?((int)keyValuePair.Value);
						}
					}
					else if (num != 3456888823U)
					{
						if (num == 3522602594U)
						{
							if (key == "uniqueItems")
							{
								this.CurrentSchema.UniqueItems = (bool)keyValuePair.Value;
							}
						}
					}
					else if (key == "readonly")
					{
						this.CurrentSchema.ReadOnly = new bool?((bool)keyValuePair.Value);
					}
				}
				else if (num <= 3947606640U)
				{
					if (num != 3526559937U)
					{
						if (num == 3947606640U)
						{
							if (key == "patternProperties")
							{
								this.CurrentSchema.PatternProperties = this.ProcessProperties(keyValuePair.Value);
							}
						}
					}
					else if (key == "maxLength")
					{
						this.CurrentSchema.MaximumLength = new int?((int)keyValuePair.Value);
					}
				}
				else if (num != 4128829753U)
				{
					if (num == 4244322099U)
					{
						if (key == "maxItems")
						{
							this.CurrentSchema.MaximumItems = new int?((int)keyValuePair.Value);
						}
					}
				}
				else if (key == "hidden")
				{
					this.CurrentSchema.Hidden = new bool?((bool)keyValuePair.Value);
				}
			}
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x000259C8 File Offset: 0x00023BC8
		private void ProcessExtends(JToken token)
		{
			IList<JsonSchema> list = new List<JsonSchema>();
			if (token.Type == JTokenType.Array)
			{
				using (IEnumerator<JToken> enumerator = ((IEnumerable<JToken>)token).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JToken jtoken = enumerator.Current;
						list.Add(this.BuildSchema(jtoken));
					}
					goto IL_52;
				}
			}
			JsonSchema jsonSchema = this.BuildSchema(token);
			if (jsonSchema != null)
			{
				list.Add(jsonSchema);
			}
			IL_52:
			if (list.Count > 0)
			{
				this.CurrentSchema.Extends = list;
			}
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00025A4C File Offset: 0x00023C4C
		private void ProcessEnum(JToken token)
		{
			if (token.Type != JTokenType.Array)
			{
				throw JsonException.Create(token, token.Path, "Expected Array token while parsing enum values, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			this.CurrentSchema.Enum = new List<JToken>();
			foreach (JToken jtoken in ((IEnumerable<JToken>)token))
			{
				this.CurrentSchema.Enum.Add(jtoken.DeepClone());
			}
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00025AE4 File Offset: 0x00023CE4
		private void ProcessAdditionalProperties(JToken token)
		{
			if (token.Type == JTokenType.Boolean)
			{
				this.CurrentSchema.AllowAdditionalProperties = (bool)token;
				return;
			}
			this.CurrentSchema.AdditionalProperties = this.BuildSchema(token);
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00025B14 File Offset: 0x00023D14
		private void ProcessAdditionalItems(JToken token)
		{
			if (token.Type == JTokenType.Boolean)
			{
				this.CurrentSchema.AllowAdditionalItems = (bool)token;
				return;
			}
			this.CurrentSchema.AdditionalItems = this.BuildSchema(token);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00025B44 File Offset: 0x00023D44
		private IDictionary<string, JsonSchema> ProcessProperties(JToken token)
		{
			IDictionary<string, JsonSchema> dictionary = new Dictionary<string, JsonSchema>();
			if (token.Type != JTokenType.Object)
			{
				throw JsonException.Create(token, token.Path, "Expected Object token while parsing schema properties, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			foreach (JToken jtoken in ((IEnumerable<JToken>)token))
			{
				JProperty jproperty = (JProperty)jtoken;
				if (dictionary.ContainsKey(jproperty.Name))
				{
					throw new JsonException("Property {0} has already been defined in schema.".FormatWith(CultureInfo.InvariantCulture, jproperty.Name));
				}
				dictionary.Add(jproperty.Name, this.BuildSchema(jproperty.Value));
			}
			return dictionary;
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00025C04 File Offset: 0x00023E04
		private void ProcessItems(JToken token)
		{
			this.CurrentSchema.Items = new List<JsonSchema>();
			JTokenType type = token.Type;
			if (type != JTokenType.Object)
			{
				if (type == JTokenType.Array)
				{
					this.CurrentSchema.PositionalItemsValidation = true;
					using (IEnumerator<JToken> enumerator = ((IEnumerable<JToken>)token).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							JToken jtoken = enumerator.Current;
							this.CurrentSchema.Items.Add(this.BuildSchema(jtoken));
						}
						return;
					}
				}
				throw JsonException.Create(token, token.Path, "Expected array or JSON schema object, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			this.CurrentSchema.Items.Add(this.BuildSchema(token));
			this.CurrentSchema.PositionalItemsValidation = false;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00025CD4 File Offset: 0x00023ED4
		private JsonSchemaType? ProcessType(JToken token)
		{
			JTokenType type = token.Type;
			if (type == JTokenType.Array)
			{
				JsonSchemaType? jsonSchemaType = new JsonSchemaType?(JsonSchemaType.None);
				foreach (JToken jtoken in ((IEnumerable<JToken>)token))
				{
					if (jtoken.Type != JTokenType.String)
					{
						throw JsonException.Create(jtoken, jtoken.Path, "Expected JSON schema type string token, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
					}
					jsonSchemaType |= JsonSchemaBuilder.MapType((string)jtoken);
				}
				return jsonSchemaType;
			}
			if (type != JTokenType.String)
			{
				throw JsonException.Create(token, token.Path, "Expected array or JSON schema type string token, got {0}.".FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			return new JsonSchemaType?(JsonSchemaBuilder.MapType((string)token));
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00025DD4 File Offset: 0x00023FD4
		internal static JsonSchemaType MapType(string type)
		{
			JsonSchemaType jsonSchemaType;
			if (!JsonSchemaConstants.JsonSchemaTypeMapping.TryGetValue(type, out jsonSchemaType))
			{
				throw new JsonException("Invalid JSON schema type: {0}".FormatWith(CultureInfo.InvariantCulture, type));
			}
			return jsonSchemaType;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00025E08 File Offset: 0x00024008
		internal static string MapType(JsonSchemaType type)
		{
			return JsonSchemaConstants.JsonSchemaTypeMapping.Single((KeyValuePair<string, JsonSchemaType> kv) => kv.Value == type).Key;
		}

		// Token: 0x040002F6 RID: 758
		private readonly IList<JsonSchema> _stack;

		// Token: 0x040002F7 RID: 759
		private readonly JsonSchemaResolver _resolver;

		// Token: 0x040002F8 RID: 760
		private readonly IDictionary<string, JsonSchema> _documentSchemas;

		// Token: 0x040002F9 RID: 761
		private JsonSchema _currentSchema;

		// Token: 0x040002FA RID: 762
		private JObject _rootSchema;
	}
}
