using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x02000031 RID: 49
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class JsonValidatingReader : JsonReader, IJsonLineInfo
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600031A RID: 794 RVA: 0x0000C024 File Offset: 0x0000A224
		// (remove) Token: 0x0600031B RID: 795 RVA: 0x0000C05C File Offset: 0x0000A25C
		public event ValidationEventHandler ValidationEventHandler;

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000C091 File Offset: 0x0000A291
		public override object Value
		{
			get
			{
				return this._reader.Value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000C09E File Offset: 0x0000A29E
		public override int Depth
		{
			get
			{
				return this._reader.Depth;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000C0AB File Offset: 0x0000A2AB
		public override string Path
		{
			get
			{
				return this._reader.Path;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000C0B8 File Offset: 0x0000A2B8
		// (set) Token: 0x06000320 RID: 800 RVA: 0x0000C0C5 File Offset: 0x0000A2C5
		public override char QuoteChar
		{
			get
			{
				return this._reader.QuoteChar;
			}
			protected internal set
			{
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000C0C7 File Offset: 0x0000A2C7
		public override JsonToken TokenType
		{
			get
			{
				return this._reader.TokenType;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000C0D4 File Offset: 0x0000A2D4
		public override Type ValueType
		{
			get
			{
				return this._reader.ValueType;
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000C0E1 File Offset: 0x0000A2E1
		private void Push(JsonValidatingReader.SchemaScope scope)
		{
			this._stack.Push(scope);
			this._currentScope = scope;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000C0F6 File Offset: 0x0000A2F6
		private JsonValidatingReader.SchemaScope Pop()
		{
			JsonValidatingReader.SchemaScope schemaScope = this._stack.Pop();
			this._currentScope = ((this._stack.Count != 0) ? this._stack.Peek() : null);
			return schemaScope;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000C124 File Offset: 0x0000A324
		private IList<JsonSchemaModel> CurrentSchemas
		{
			get
			{
				return this._currentScope.Schemas;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000C134 File Offset: 0x0000A334
		private IList<JsonSchemaModel> CurrentMemberSchemas
		{
			get
			{
				if (this._currentScope == null)
				{
					return new List<JsonSchemaModel>(new JsonSchemaModel[] { this._model });
				}
				if (this._currentScope.Schemas == null || this._currentScope.Schemas.Count == 0)
				{
					return JsonValidatingReader.EmptySchemaList;
				}
				switch (this._currentScope.TokenType)
				{
				case JTokenType.None:
					return this._currentScope.Schemas;
				case JTokenType.Object:
				{
					if (this._currentScope.CurrentPropertyName == null)
					{
						throw new JsonReaderException("CurrentPropertyName has not been set on scope.");
					}
					IList<JsonSchemaModel> list = new List<JsonSchemaModel>();
					foreach (JsonSchemaModel jsonSchemaModel in this.CurrentSchemas)
					{
						JsonSchemaModel jsonSchemaModel2;
						if (jsonSchemaModel.Properties != null && jsonSchemaModel.Properties.TryGetValue(this._currentScope.CurrentPropertyName, out jsonSchemaModel2))
						{
							list.Add(jsonSchemaModel2);
						}
						if (jsonSchemaModel.PatternProperties != null)
						{
							foreach (KeyValuePair<string, JsonSchemaModel> keyValuePair in jsonSchemaModel.PatternProperties)
							{
								if (Regex.IsMatch(this._currentScope.CurrentPropertyName, keyValuePair.Key))
								{
									list.Add(keyValuePair.Value);
								}
							}
						}
						if (list.Count == 0 && jsonSchemaModel.AllowAdditionalProperties && jsonSchemaModel.AdditionalProperties != null)
						{
							list.Add(jsonSchemaModel.AdditionalProperties);
						}
					}
					return list;
				}
				case JTokenType.Array:
				{
					IList<JsonSchemaModel> list2 = new List<JsonSchemaModel>();
					foreach (JsonSchemaModel jsonSchemaModel3 in this.CurrentSchemas)
					{
						if (!jsonSchemaModel3.PositionalItemsValidation)
						{
							if (jsonSchemaModel3.Items != null && jsonSchemaModel3.Items.Count > 0)
							{
								list2.Add(jsonSchemaModel3.Items[0]);
							}
						}
						else
						{
							if (jsonSchemaModel3.Items != null && jsonSchemaModel3.Items.Count > 0 && jsonSchemaModel3.Items.Count > this._currentScope.ArrayItemCount - 1)
							{
								list2.Add(jsonSchemaModel3.Items[this._currentScope.ArrayItemCount - 1]);
							}
							if (jsonSchemaModel3.AllowAdditionalItems && jsonSchemaModel3.AdditionalItems != null)
							{
								list2.Add(jsonSchemaModel3.AdditionalItems);
							}
						}
					}
					return list2;
				}
				case JTokenType.Constructor:
					return JsonValidatingReader.EmptySchemaList;
				default:
					throw new ArgumentOutOfRangeException("TokenType", "Unexpected token type: {0}".FormatWith(CultureInfo.InvariantCulture, this._currentScope.TokenType));
				}
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000C3F4 File Offset: 0x0000A5F4
		private void RaiseError(string message, JsonSchemaModel schema)
		{
			string text = (((IJsonLineInfo)this).HasLineInfo() ? (message + " Line {0}, position {1}.".FormatWith(CultureInfo.InvariantCulture, ((IJsonLineInfo)this).LineNumber, ((IJsonLineInfo)this).LinePosition)) : message);
			this.OnValidationEvent(new JsonSchemaException(text, null, this.Path, ((IJsonLineInfo)this).LineNumber, ((IJsonLineInfo)this).LinePosition));
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000C45C File Offset: 0x0000A65C
		private void OnValidationEvent(JsonSchemaException exception)
		{
			ValidationEventHandler validationEventHandler = this.ValidationEventHandler;
			if (validationEventHandler != null)
			{
				validationEventHandler(this, new ValidationEventArgs(exception));
				return;
			}
			throw exception;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000C482 File Offset: 0x0000A682
		public JsonValidatingReader(JsonReader reader)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			this._reader = reader;
			this._stack = new Stack<JsonValidatingReader.SchemaScope>();
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000C4A7 File Offset: 0x0000A6A7
		// (set) Token: 0x0600032B RID: 811 RVA: 0x0000C4AF File Offset: 0x0000A6AF
		public JsonSchema Schema
		{
			get
			{
				return this._schema;
			}
			set
			{
				if (this.TokenType != JsonToken.None)
				{
					throw new InvalidOperationException("Cannot change schema while validating JSON.");
				}
				this._schema = value;
				this._model = null;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000C4D2 File Offset: 0x0000A6D2
		public JsonReader Reader
		{
			get
			{
				return this._reader;
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000C4DA File Offset: 0x0000A6DA
		public override void Close()
		{
			base.Close();
			if (base.CloseInput)
			{
				JsonReader reader = this._reader;
				if (reader == null)
				{
					return;
				}
				reader.Close();
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000C4FC File Offset: 0x0000A6FC
		private void ValidateNotDisallowed(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			JsonSchemaType? currentNodeSchemaType = this.GetCurrentNodeSchemaType();
			if (currentNodeSchemaType != null && JsonSchemaGenerator.HasFlag(new JsonSchemaType?(schema.Disallow), currentNodeSchemaType.GetValueOrDefault()))
			{
				this.RaiseError("Type {0} is disallowed.".FormatWith(CultureInfo.InvariantCulture, currentNodeSchemaType), schema);
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000C554 File Offset: 0x0000A754
		private JsonSchemaType? GetCurrentNodeSchemaType()
		{
			switch (this._reader.TokenType)
			{
			case JsonToken.StartObject:
				return new JsonSchemaType?(JsonSchemaType.Object);
			case JsonToken.StartArray:
				return new JsonSchemaType?(JsonSchemaType.Array);
			case JsonToken.Integer:
				return new JsonSchemaType?(JsonSchemaType.Integer);
			case JsonToken.Float:
				return new JsonSchemaType?(JsonSchemaType.Float);
			case JsonToken.String:
				return new JsonSchemaType?(JsonSchemaType.String);
			case JsonToken.Boolean:
				return new JsonSchemaType?(JsonSchemaType.Boolean);
			case JsonToken.Null:
				return new JsonSchemaType?(JsonSchemaType.Null);
			}
			return null;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000C5E0 File Offset: 0x0000A7E0
		public override int? ReadAsInt32()
		{
			int? num = this._reader.ReadAsInt32();
			this.ValidateCurrentToken();
			return num;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000C5F3 File Offset: 0x0000A7F3
		public override byte[] ReadAsBytes()
		{
			byte[] array = this._reader.ReadAsBytes();
			this.ValidateCurrentToken();
			return array;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000C606 File Offset: 0x0000A806
		public override decimal? ReadAsDecimal()
		{
			decimal? num = this._reader.ReadAsDecimal();
			this.ValidateCurrentToken();
			return num;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000C619 File Offset: 0x0000A819
		public override double? ReadAsDouble()
		{
			double? num = this._reader.ReadAsDouble();
			this.ValidateCurrentToken();
			return num;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000C62C File Offset: 0x0000A82C
		public override bool? ReadAsBoolean()
		{
			bool? flag = this._reader.ReadAsBoolean();
			this.ValidateCurrentToken();
			return flag;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000C63F File Offset: 0x0000A83F
		public override string ReadAsString()
		{
			string text = this._reader.ReadAsString();
			this.ValidateCurrentToken();
			return text;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000C652 File Offset: 0x0000A852
		public override DateTime? ReadAsDateTime()
		{
			DateTime? dateTime = this._reader.ReadAsDateTime();
			this.ValidateCurrentToken();
			return dateTime;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000C665 File Offset: 0x0000A865
		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			DateTimeOffset? dateTimeOffset = this._reader.ReadAsDateTimeOffset();
			this.ValidateCurrentToken();
			return dateTimeOffset;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000C678 File Offset: 0x0000A878
		public override bool Read()
		{
			if (!this._reader.Read())
			{
				return false;
			}
			if (this._reader.TokenType == JsonToken.Comment)
			{
				return true;
			}
			this.ValidateCurrentToken();
			return true;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000C6A0 File Offset: 0x0000A8A0
		private void ValidateCurrentToken()
		{
			if (this._model == null)
			{
				JsonSchemaModelBuilder jsonSchemaModelBuilder = new JsonSchemaModelBuilder();
				this._model = jsonSchemaModelBuilder.Build(this._schema);
				if (!JsonTokenUtils.IsStartToken(this._reader.TokenType))
				{
					this.Push(new JsonValidatingReader.SchemaScope(JTokenType.None, this.CurrentMemberSchemas));
				}
			}
			switch (this._reader.TokenType)
			{
			case JsonToken.None:
				return;
			case JsonToken.StartObject:
			{
				this.ProcessValue();
				IList<JsonSchemaModel> list = this.CurrentMemberSchemas.Where(new Func<JsonSchemaModel, bool>(this.ValidateObject)).ToList<JsonSchemaModel>();
				this.Push(new JsonValidatingReader.SchemaScope(JTokenType.Object, list));
				this.WriteToken(this.CurrentSchemas);
				return;
			}
			case JsonToken.StartArray:
			{
				this.ProcessValue();
				IList<JsonSchemaModel> list2 = this.CurrentMemberSchemas.Where(new Func<JsonSchemaModel, bool>(this.ValidateArray)).ToList<JsonSchemaModel>();
				this.Push(new JsonValidatingReader.SchemaScope(JTokenType.Array, list2));
				this.WriteToken(this.CurrentSchemas);
				return;
			}
			case JsonToken.StartConstructor:
				this.ProcessValue();
				this.Push(new JsonValidatingReader.SchemaScope(JTokenType.Constructor, null));
				this.WriteToken(this.CurrentSchemas);
				return;
			case JsonToken.PropertyName:
			{
				this.WriteToken(this.CurrentSchemas);
				using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentSchemas.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JsonSchemaModel jsonSchemaModel = enumerator.Current;
						this.ValidatePropertyName(jsonSchemaModel);
					}
					return;
				}
				break;
			}
			case JsonToken.Comment:
				goto IL_3BD;
			case JsonToken.Raw:
				break;
			case JsonToken.Integer:
			{
				this.ProcessValue();
				this.WriteToken(this.CurrentMemberSchemas);
				using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentMemberSchemas.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JsonSchemaModel jsonSchemaModel2 = enumerator.Current;
						this.ValidateInteger(jsonSchemaModel2);
					}
					return;
				}
				goto IL_1D6;
			}
			case JsonToken.Float:
				goto IL_1D6;
			case JsonToken.String:
				goto IL_222;
			case JsonToken.Boolean:
				goto IL_26E;
			case JsonToken.Null:
				goto IL_2BA;
			case JsonToken.Undefined:
			case JsonToken.Date:
			case JsonToken.Bytes:
				this.WriteToken(this.CurrentMemberSchemas);
				return;
			case JsonToken.EndObject:
				goto IL_306;
			case JsonToken.EndArray:
				this.WriteToken(this.CurrentSchemas);
				foreach (JsonSchemaModel jsonSchemaModel3 in this.CurrentSchemas)
				{
					this.ValidateEndArray(jsonSchemaModel3);
				}
				this.Pop();
				return;
			case JsonToken.EndConstructor:
				this.WriteToken(this.CurrentSchemas);
				this.Pop();
				return;
			default:
				goto IL_3BD;
			}
			this.ProcessValue();
			return;
			IL_1D6:
			this.ProcessValue();
			this.WriteToken(this.CurrentMemberSchemas);
			using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentMemberSchemas.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JsonSchemaModel jsonSchemaModel4 = enumerator.Current;
					this.ValidateFloat(jsonSchemaModel4);
				}
				return;
			}
			IL_222:
			this.ProcessValue();
			this.WriteToken(this.CurrentMemberSchemas);
			using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentMemberSchemas.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JsonSchemaModel jsonSchemaModel5 = enumerator.Current;
					this.ValidateString(jsonSchemaModel5);
				}
				return;
			}
			IL_26E:
			this.ProcessValue();
			this.WriteToken(this.CurrentMemberSchemas);
			using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentMemberSchemas.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JsonSchemaModel jsonSchemaModel6 = enumerator.Current;
					this.ValidateBoolean(jsonSchemaModel6);
				}
				return;
			}
			IL_2BA:
			this.ProcessValue();
			this.WriteToken(this.CurrentMemberSchemas);
			using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentMemberSchemas.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JsonSchemaModel jsonSchemaModel7 = enumerator.Current;
					this.ValidateNull(jsonSchemaModel7);
				}
				return;
			}
			IL_306:
			this.WriteToken(this.CurrentSchemas);
			foreach (JsonSchemaModel jsonSchemaModel8 in this.CurrentSchemas)
			{
				this.ValidateEndObject(jsonSchemaModel8);
			}
			this.Pop();
			return;
			IL_3BD:
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000CAD4 File Offset: 0x0000ACD4
		private void WriteToken(IList<JsonSchemaModel> schemas)
		{
			foreach (JsonValidatingReader.SchemaScope schemaScope in this._stack)
			{
				bool flag = schemaScope.TokenType == JTokenType.Array && schemaScope.IsUniqueArray && schemaScope.ArrayItemCount > 0;
				if (!flag)
				{
					if (!schemas.Any((JsonSchemaModel s) => s.Enum != null))
					{
						continue;
					}
				}
				if (schemaScope.CurrentItemWriter == null)
				{
					if (JsonTokenUtils.IsEndToken(this._reader.TokenType))
					{
						continue;
					}
					schemaScope.CurrentItemWriter = new JTokenWriter();
				}
				schemaScope.CurrentItemWriter.WriteToken(this._reader, false);
				if (schemaScope.CurrentItemWriter.Top == 0 && this._reader.TokenType != JsonToken.PropertyName)
				{
					JToken token = schemaScope.CurrentItemWriter.Token;
					schemaScope.CurrentItemWriter = null;
					if (flag)
					{
						if (schemaScope.UniqueArrayItems.Contains(token, JToken.EqualityComparer))
						{
							this.RaiseError("Non-unique array item at index {0}.".FormatWith(CultureInfo.InvariantCulture, schemaScope.ArrayItemCount - 1), schemaScope.Schemas.First((JsonSchemaModel s) => s.UniqueItems));
						}
						schemaScope.UniqueArrayItems.Add(token);
					}
					else if (schemas.Any((JsonSchemaModel s) => s.Enum != null))
					{
						foreach (JsonSchemaModel jsonSchemaModel in schemas)
						{
							if (jsonSchemaModel.Enum != null && !jsonSchemaModel.Enum.ContainsValue(token, JToken.EqualityComparer))
							{
								StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
								token.WriteTo(new JsonTextWriter(stringWriter), new JsonConverter[0]);
								this.RaiseError("Value {0} is not defined in enum.".FormatWith(CultureInfo.InvariantCulture, stringWriter.ToString()), jsonSchemaModel);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000CD24 File Offset: 0x0000AF24
		private void ValidateEndObject(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			Dictionary<string, bool> requiredProperties = this._currentScope.RequiredProperties;
			if (requiredProperties != null)
			{
				if (requiredProperties.Values.Any((bool v) => !v))
				{
					IEnumerable<string> enumerable = from kv in requiredProperties
						where !kv.Value
						select kv.Key;
					this.RaiseError("Required properties are missing from object: {0}.".FormatWith(CultureInfo.InvariantCulture, string.Join(", ", enumerable)), schema);
				}
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000CDE0 File Offset: 0x0000AFE0
		private void ValidateEndArray(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			int arrayItemCount = this._currentScope.ArrayItemCount;
			if (schema.MaximumItems != null)
			{
				int num = arrayItemCount;
				int? num2 = schema.MaximumItems;
				if ((num > num2.GetValueOrDefault()) & (num2 != null))
				{
					this.RaiseError("Array item count {0} exceeds maximum count of {1}.".FormatWith(CultureInfo.InvariantCulture, arrayItemCount, schema.MaximumItems), schema);
				}
			}
			if (schema.MinimumItems != null)
			{
				int num3 = arrayItemCount;
				int? num2 = schema.MinimumItems;
				if ((num3 < num2.GetValueOrDefault()) & (num2 != null))
				{
					this.RaiseError("Array item count {0} is less than minimum count of {1}.".FormatWith(CultureInfo.InvariantCulture, arrayItemCount, schema.MinimumItems), schema);
				}
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000CEA1 File Offset: 0x0000B0A1
		private void ValidateNull(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Null))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000CEBA File Offset: 0x0000B0BA
		private void ValidateBoolean(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Boolean))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000CED4 File Offset: 0x0000B0D4
		private void ValidateString(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.String))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
			string text = this._reader.Value.ToString();
			if (schema.MaximumLength != null)
			{
				int length = text.Length;
				int? num = schema.MaximumLength;
				if ((length > num.GetValueOrDefault()) & (num != null))
				{
					this.RaiseError("String '{0}' exceeds maximum length of {1}.".FormatWith(CultureInfo.InvariantCulture, text, schema.MaximumLength), schema);
				}
			}
			if (schema.MinimumLength != null)
			{
				int length2 = text.Length;
				int? num = schema.MinimumLength;
				if ((length2 < num.GetValueOrDefault()) & (num != null))
				{
					this.RaiseError("String '{0}' is less than minimum length of {1}.".FormatWith(CultureInfo.InvariantCulture, text, schema.MinimumLength), schema);
				}
			}
			if (schema.Patterns != null)
			{
				foreach (string text2 in schema.Patterns)
				{
					if (!Regex.IsMatch(text, text2))
					{
						this.RaiseError("String '{0}' does not match regex pattern '{1}'.".FormatWith(CultureInfo.InvariantCulture, text, text2), schema);
					}
				}
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000D010 File Offset: 0x0000B210
		private void ValidateInteger(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Integer))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
			object value = this._reader.Value;
			if (schema.Maximum != null)
			{
				if (JValue.Compare(JTokenType.Integer, value, schema.Maximum) > 0)
				{
					this.RaiseError("Integer {0} exceeds maximum value of {1}.".FormatWith(CultureInfo.InvariantCulture, value, schema.Maximum), schema);
				}
				if (schema.ExclusiveMaximum && JValue.Compare(JTokenType.Integer, value, schema.Maximum) == 0)
				{
					this.RaiseError("Integer {0} equals maximum value of {1} and exclusive maximum is true.".FormatWith(CultureInfo.InvariantCulture, value, schema.Maximum), schema);
				}
			}
			if (schema.Minimum != null)
			{
				if (JValue.Compare(JTokenType.Integer, value, schema.Minimum) < 0)
				{
					this.RaiseError("Integer {0} is less than minimum value of {1}.".FormatWith(CultureInfo.InvariantCulture, value, schema.Minimum), schema);
				}
				if (schema.ExclusiveMinimum && JValue.Compare(JTokenType.Integer, value, schema.Minimum) == 0)
				{
					this.RaiseError("Integer {0} equals minimum value of {1} and exclusive minimum is true.".FormatWith(CultureInfo.InvariantCulture, value, schema.Minimum), schema);
				}
			}
			if (schema.DivisibleBy != null)
			{
				bool flag;
				if (value is BigInteger)
				{
					BigInteger bigInteger = (BigInteger)value;
					if (!Math.Abs(schema.DivisibleBy.Value - Math.Truncate(schema.DivisibleBy.Value)).Equals(0.0))
					{
						flag = bigInteger != 0L;
					}
					else
					{
						flag = bigInteger % new BigInteger(schema.DivisibleBy.Value) != 0L;
					}
				}
				else
				{
					flag = !JsonValidatingReader.IsZero((double)Convert.ToInt64(value, CultureInfo.InvariantCulture) % schema.DivisibleBy.GetValueOrDefault());
				}
				if (flag)
				{
					this.RaiseError("Integer {0} is not evenly divisible by {1}.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(value), schema.DivisibleBy), schema);
				}
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000D228 File Offset: 0x0000B428
		private void ProcessValue()
		{
			if (this._currentScope != null && this._currentScope.TokenType == JTokenType.Array)
			{
				JsonValidatingReader.SchemaScope currentScope = this._currentScope;
				int arrayItemCount = currentScope.ArrayItemCount;
				currentScope.ArrayItemCount = arrayItemCount + 1;
				foreach (JsonSchemaModel jsonSchemaModel in this.CurrentSchemas)
				{
					if (jsonSchemaModel != null && jsonSchemaModel.PositionalItemsValidation && !jsonSchemaModel.AllowAdditionalItems && (jsonSchemaModel.Items == null || this._currentScope.ArrayItemCount - 1 >= jsonSchemaModel.Items.Count))
					{
						this.RaiseError("Index {0} has not been defined and the schema does not allow additional items.".FormatWith(CultureInfo.InvariantCulture, this._currentScope.ArrayItemCount), jsonSchemaModel);
					}
				}
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000D2FC File Offset: 0x0000B4FC
		private void ValidateFloat(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Float))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
			double num = Convert.ToDouble(this._reader.Value, CultureInfo.InvariantCulture);
			if (schema.Maximum != null)
			{
				double num2 = num;
				double? num3 = schema.Maximum;
				if ((num2 > num3.GetValueOrDefault()) & (num3 != null))
				{
					this.RaiseError("Float {0} exceeds maximum value of {1}.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.Maximum), schema);
				}
				if (schema.ExclusiveMaximum)
				{
					double num4 = num;
					num3 = schema.Maximum;
					if ((num4 == num3.GetValueOrDefault()) & (num3 != null))
					{
						this.RaiseError("Float {0} equals maximum value of {1} and exclusive maximum is true.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.Maximum), schema);
					}
				}
			}
			if (schema.Minimum != null)
			{
				double num5 = num;
				double? num3 = schema.Minimum;
				if ((num5 < num3.GetValueOrDefault()) & (num3 != null))
				{
					this.RaiseError("Float {0} is less than minimum value of {1}.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.Minimum), schema);
				}
				if (schema.ExclusiveMinimum)
				{
					double num6 = num;
					num3 = schema.Minimum;
					if ((num6 == num3.GetValueOrDefault()) & (num3 != null))
					{
						this.RaiseError("Float {0} equals minimum value of {1} and exclusive minimum is true.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.Minimum), schema);
					}
				}
			}
			if (schema.DivisibleBy != null && !JsonValidatingReader.IsZero(JsonValidatingReader.FloatingPointRemainder(num, schema.DivisibleBy.GetValueOrDefault())))
			{
				this.RaiseError("Float {0} is not evenly divisible by {1}.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.DivisibleBy), schema);
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000D4C5 File Offset: 0x0000B6C5
		private static double FloatingPointRemainder(double dividend, double divisor)
		{
			return dividend - Math.Floor(dividend / divisor) * divisor;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000D4D3 File Offset: 0x0000B6D3
		private static bool IsZero(double value)
		{
			return Math.Abs(value) < 4.4408920985006262E-15;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000D4E8 File Offset: 0x0000B6E8
		private void ValidatePropertyName(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			string text = Convert.ToString(this._reader.Value, CultureInfo.InvariantCulture);
			if (this._currentScope.RequiredProperties.ContainsKey(text))
			{
				this._currentScope.RequiredProperties[text] = true;
			}
			if (!schema.AllowAdditionalProperties && !this.IsPropertyDefinied(schema, text))
			{
				this.RaiseError("Property '{0}' has not been defined and the schema does not allow additional properties.".FormatWith(CultureInfo.InvariantCulture, text), schema);
			}
			this._currentScope.CurrentPropertyName = text;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000D56C File Offset: 0x0000B76C
		private bool IsPropertyDefinied(JsonSchemaModel schema, string propertyName)
		{
			if (schema.Properties != null && schema.Properties.ContainsKey(propertyName))
			{
				return true;
			}
			if (schema.PatternProperties != null)
			{
				foreach (string text in schema.PatternProperties.Keys)
				{
					if (Regex.IsMatch(propertyName, text))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000D5E8 File Offset: 0x0000B7E8
		private bool ValidateArray(JsonSchemaModel schema)
		{
			return schema == null || this.TestType(schema, JsonSchemaType.Array);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000D5F8 File Offset: 0x0000B7F8
		private bool ValidateObject(JsonSchemaModel schema)
		{
			return schema == null || this.TestType(schema, JsonSchemaType.Object);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000D608 File Offset: 0x0000B808
		private bool TestType(JsonSchemaModel currentSchema, JsonSchemaType currentType)
		{
			if (!JsonSchemaGenerator.HasFlag(new JsonSchemaType?(currentSchema.Type), currentType))
			{
				this.RaiseError("Invalid type. Expected {0} but got {1}.".FormatWith(CultureInfo.InvariantCulture, currentSchema.Type, currentType), currentSchema);
				return false;
			}
			return true;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000D648 File Offset: 0x0000B848
		bool IJsonLineInfo.HasLineInfo()
		{
			IJsonLineInfo jsonLineInfo = this._reader as IJsonLineInfo;
			return jsonLineInfo != null && jsonLineInfo.HasLineInfo();
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000D66C File Offset: 0x0000B86C
		int IJsonLineInfo.LineNumber
		{
			get
			{
				IJsonLineInfo jsonLineInfo = this._reader as IJsonLineInfo;
				if (jsonLineInfo == null)
				{
					return 0;
				}
				return jsonLineInfo.LineNumber;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000D690 File Offset: 0x0000B890
		int IJsonLineInfo.LinePosition
		{
			get
			{
				IJsonLineInfo jsonLineInfo = this._reader as IJsonLineInfo;
				if (jsonLineInfo == null)
				{
					return 0;
				}
				return jsonLineInfo.LinePosition;
			}
		}

		// Token: 0x04000100 RID: 256
		private readonly JsonReader _reader;

		// Token: 0x04000101 RID: 257
		private readonly Stack<JsonValidatingReader.SchemaScope> _stack;

		// Token: 0x04000102 RID: 258
		private JsonSchema _schema;

		// Token: 0x04000103 RID: 259
		private JsonSchemaModel _model;

		// Token: 0x04000104 RID: 260
		private JsonValidatingReader.SchemaScope _currentScope;

		// Token: 0x04000106 RID: 262
		private static readonly IList<JsonSchemaModel> EmptySchemaList = new List<JsonSchemaModel>();

		// Token: 0x02000155 RID: 341
		private class SchemaScope
		{
			// Token: 0x17000286 RID: 646
			// (get) Token: 0x06000E59 RID: 3673 RVA: 0x0003FFAE File Offset: 0x0003E1AE
			// (set) Token: 0x06000E5A RID: 3674 RVA: 0x0003FFB6 File Offset: 0x0003E1B6
			public string CurrentPropertyName { get; set; }

			// Token: 0x17000287 RID: 647
			// (get) Token: 0x06000E5B RID: 3675 RVA: 0x0003FFBF File Offset: 0x0003E1BF
			// (set) Token: 0x06000E5C RID: 3676 RVA: 0x0003FFC7 File Offset: 0x0003E1C7
			public int ArrayItemCount { get; set; }

			// Token: 0x17000288 RID: 648
			// (get) Token: 0x06000E5D RID: 3677 RVA: 0x0003FFD0 File Offset: 0x0003E1D0
			public bool IsUniqueArray { get; }

			// Token: 0x17000289 RID: 649
			// (get) Token: 0x06000E5E RID: 3678 RVA: 0x0003FFD8 File Offset: 0x0003E1D8
			public IList<JToken> UniqueArrayItems { get; }

			// Token: 0x1700028A RID: 650
			// (get) Token: 0x06000E5F RID: 3679 RVA: 0x0003FFE0 File Offset: 0x0003E1E0
			// (set) Token: 0x06000E60 RID: 3680 RVA: 0x0003FFE8 File Offset: 0x0003E1E8
			public JTokenWriter CurrentItemWriter { get; set; }

			// Token: 0x1700028B RID: 651
			// (get) Token: 0x06000E61 RID: 3681 RVA: 0x0003FFF1 File Offset: 0x0003E1F1
			public IList<JsonSchemaModel> Schemas
			{
				get
				{
					return this._schemas;
				}
			}

			// Token: 0x1700028C RID: 652
			// (get) Token: 0x06000E62 RID: 3682 RVA: 0x0003FFF9 File Offset: 0x0003E1F9
			public Dictionary<string, bool> RequiredProperties
			{
				get
				{
					return this._requiredProperties;
				}
			}

			// Token: 0x1700028D RID: 653
			// (get) Token: 0x06000E63 RID: 3683 RVA: 0x00040001 File Offset: 0x0003E201
			public JTokenType TokenType
			{
				get
				{
					return this._tokenType;
				}
			}

			// Token: 0x06000E64 RID: 3684 RVA: 0x0004000C File Offset: 0x0003E20C
			public SchemaScope(JTokenType tokenType, IList<JsonSchemaModel> schemas)
			{
				this._tokenType = tokenType;
				this._schemas = schemas;
				this._requiredProperties = schemas.SelectMany(new Func<JsonSchemaModel, IEnumerable<string>>(this.GetRequiredProperties)).Distinct<string>().ToDictionary((string p) => p, (string p) => false);
				if (tokenType == JTokenType.Array)
				{
					if (schemas.Any((JsonSchemaModel s) => s.UniqueItems))
					{
						this.IsUniqueArray = true;
						this.UniqueArrayItems = new List<JToken>();
					}
				}
			}

			// Token: 0x06000E65 RID: 3685 RVA: 0x000400CC File Offset: 0x0003E2CC
			private IEnumerable<string> GetRequiredProperties(JsonSchemaModel schema)
			{
				if (((schema != null) ? schema.Properties : null) == null)
				{
					return Enumerable.Empty<string>();
				}
				return from p in schema.Properties
					where p.Value.Required
					select p.Key;
			}

			// Token: 0x0400060A RID: 1546
			private readonly JTokenType _tokenType;

			// Token: 0x0400060B RID: 1547
			private readonly IList<JsonSchemaModel> _schemas;

			// Token: 0x0400060C RID: 1548
			private readonly Dictionary<string, bool> _requiredProperties;
		}
	}
}
