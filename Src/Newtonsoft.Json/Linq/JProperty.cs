using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000BE RID: 190
	[NullableContext(1)]
	[Nullable(0)]
	public class JProperty : JContainer
	{
		// Token: 0x06000A6E RID: 2670 RVA: 0x0002A210 File Offset: 0x00028410
		public override Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			Task task = writer.WritePropertyNameAsync(this._name, cancellationToken);
			if (task.IsCompletedSucessfully())
			{
				return this.WriteValueAsync(writer, cancellationToken, converters);
			}
			return this.WriteToAsync(task, writer, cancellationToken, converters);
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0002A248 File Offset: 0x00028448
		private async Task WriteToAsync(Task task, JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			await task.ConfigureAwait(false);
			await this.WriteValueAsync(writer, cancellationToken, converters).ConfigureAwait(false);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0002A2AC File Offset: 0x000284AC
		private Task WriteValueAsync(JsonWriter writer, CancellationToken cancellationToken, JsonConverter[] converters)
		{
			JToken value = this.Value;
			if (value == null)
			{
				return writer.WriteNullAsync(cancellationToken);
			}
			return value.WriteToAsync(writer, cancellationToken, converters);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0002A2D4 File Offset: 0x000284D4
		public new static Task<JProperty> LoadAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			return JProperty.LoadAsync(reader, null, cancellationToken);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0002A2E0 File Offset: 0x000284E0
		public new static async Task<JProperty> LoadAsync(JsonReader reader, [Nullable(2)] JsonLoadSettings settings, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (reader.TokenType == JsonToken.None)
			{
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = reader.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (!configuredTaskAwaiter.GetResult())
				{
					throw JsonReaderException.Create(reader, "Error reading JProperty from JsonReader.");
				}
			}
			await reader.MoveToContentAsync(cancellationToken).ConfigureAwait(false);
			if (reader.TokenType != JsonToken.PropertyName)
			{
				throw JsonReaderException.Create(reader, "Error reading JProperty from JsonReader. Current JsonReader item is not a property: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JProperty p = new JProperty((string)reader.Value);
			p.SetLineInfo(reader as IJsonLineInfo, settings);
			await p.ReadTokenFromAsync(reader, settings, cancellationToken).ConfigureAwait(false);
			return p;
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x0002A333 File Offset: 0x00028533
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x0002A33B File Offset: 0x0002853B
		public string Name
		{
			[DebuggerStepThrough]
			get
			{
				return this._name;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x0002A343 File Offset: 0x00028543
		// (set) Token: 0x06000A76 RID: 2678 RVA: 0x0002A350 File Offset: 0x00028550
		public new JToken Value
		{
			[DebuggerStepThrough]
			get
			{
				return this._content._token;
			}
			set
			{
				base.CheckReentrancy();
				JToken jtoken = value ?? JValue.CreateNull();
				if (this._content._token == null)
				{
					this.InsertItem(0, jtoken, false);
					return;
				}
				this.SetItem(0, jtoken);
			}
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0002A38E File Offset: 0x0002858E
		public JProperty(JProperty other)
			: base(other)
		{
			this._name = other.Name;
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0002A3AE File Offset: 0x000285AE
		internal override JToken GetItem(int index)
		{
			if (index != 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this.Value;
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0002A3C0 File Offset: 0x000285C0
		[NullableContext(2)]
		internal override void SetItem(int index, JToken item)
		{
			if (index != 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (JContainer.IsTokenUnchanged(this.Value, item))
			{
				return;
			}
			JObject jobject = (JObject)base.Parent;
			if (jobject != null)
			{
				jobject.InternalPropertyChanging(this);
			}
			base.SetItem(0, item);
			JObject jobject2 = (JObject)base.Parent;
			if (jobject2 == null)
			{
				return;
			}
			jobject2.InternalPropertyChanged(this);
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0002A41A File Offset: 0x0002861A
		[NullableContext(2)]
		internal override bool RemoveItem(JToken item)
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0002A43A File Offset: 0x0002863A
		internal override void RemoveItemAt(int index)
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0002A45A File Offset: 0x0002865A
		[NullableContext(2)]
		internal override int IndexOfItem(JToken item)
		{
			if (item == null)
			{
				return -1;
			}
			return this._content.IndexOf(item);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0002A46D File Offset: 0x0002866D
		[NullableContext(2)]
		internal override bool InsertItem(int index, JToken item, bool skipParentCheck)
		{
			if (item != null && item.Type == JTokenType.Comment)
			{
				return false;
			}
			if (this.Value != null)
			{
				throw new JsonException("{0} cannot have multiple values.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
			}
			return base.InsertItem(0, item, false);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0002A4AD File Offset: 0x000286AD
		[NullableContext(2)]
		internal override bool ContainsItem(JToken item)
		{
			return this.Value == item;
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0002A4B8 File Offset: 0x000286B8
		internal override void MergeItem(object content, [Nullable(2)] JsonMergeSettings settings)
		{
			JProperty jproperty = content as JProperty;
			JToken jtoken = ((jproperty != null) ? jproperty.Value : null);
			if (jtoken != null && jtoken.Type != JTokenType.Null)
			{
				this.Value = jtoken;
			}
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0002A4EC File Offset: 0x000286EC
		internal override void ClearItems()
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0002A50C File Offset: 0x0002870C
		internal override bool DeepEquals(JToken node)
		{
			JProperty jproperty = node as JProperty;
			return jproperty != null && this._name == jproperty.Name && base.ContentsEqual(jproperty);
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0002A53F File Offset: 0x0002873F
		internal override JToken CloneToken()
		{
			return new JProperty(this);
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x0002A547 File Offset: 0x00028747
		public override JTokenType Type
		{
			[DebuggerStepThrough]
			get
			{
				return JTokenType.Property;
			}
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0002A54A File Offset: 0x0002874A
		internal JProperty(string name)
		{
			ValidationUtils.ArgumentNotNull(name, "name");
			this._name = name;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0002A56F File Offset: 0x0002876F
		public JProperty(string name, params object[] content)
			: this(name, content)
		{
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0002A57C File Offset: 0x0002877C
		public JProperty(string name, [Nullable(2)] object content)
		{
			ValidationUtils.ArgumentNotNull(name, "name");
			this._name = name;
			this.Value = (base.IsMultiContent(content) ? new JArray(content) : JContainer.CreateFromContent(content));
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0002A5CC File Offset: 0x000287CC
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WritePropertyName(this._name);
			JToken value = this.Value;
			if (value != null)
			{
				value.WriteTo(writer, converters);
				return;
			}
			writer.WriteNull();
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0002A5FE File Offset: 0x000287FE
		internal override int GetDeepHashCode()
		{
			int hashCode = this._name.GetHashCode();
			JToken value = this.Value;
			return hashCode ^ ((value != null) ? value.GetDeepHashCode() : 0);
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0002A61E File Offset: 0x0002881E
		public new static JProperty Load(JsonReader reader)
		{
			return JProperty.Load(reader, null);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0002A628 File Offset: 0x00028828
		public new static JProperty Load(JsonReader reader, [Nullable(2)] JsonLoadSettings settings)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading JProperty from JsonReader.");
			}
			reader.MoveToContent();
			if (reader.TokenType != JsonToken.PropertyName)
			{
				throw JsonReaderException.Create(reader, "Error reading JProperty from JsonReader. Current JsonReader item is not a property: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JProperty jproperty = new JProperty((string)reader.Value);
			jproperty.SetLineInfo(reader as IJsonLineInfo, settings);
			jproperty.ReadTokenFrom(reader, settings);
			return jproperty;
		}

		// Token: 0x0400036B RID: 875
		private readonly JProperty.JPropertyList _content = new JProperty.JPropertyList();

		// Token: 0x0400036C RID: 876
		private readonly string _name;

		// Token: 0x020001C8 RID: 456
		[Nullable(0)]
		private class JPropertyList : IList<JToken>, ICollection<JToken>, IEnumerable<JToken>, IEnumerable
		{
			// Token: 0x06000F9F RID: 3999 RVA: 0x00044CA6 File Offset: 0x00042EA6
			public IEnumerator<JToken> GetEnumerator()
			{
				if (this._token != null)
				{
					yield return this._token;
				}
				yield break;
			}

			// Token: 0x06000FA0 RID: 4000 RVA: 0x00044CB5 File Offset: 0x00042EB5
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000FA1 RID: 4001 RVA: 0x00044CBD File Offset: 0x00042EBD
			public void Add(JToken item)
			{
				this._token = item;
			}

			// Token: 0x06000FA2 RID: 4002 RVA: 0x00044CC6 File Offset: 0x00042EC6
			public void Clear()
			{
				this._token = null;
			}

			// Token: 0x06000FA3 RID: 4003 RVA: 0x00044CCF File Offset: 0x00042ECF
			public bool Contains(JToken item)
			{
				return this._token == item;
			}

			// Token: 0x06000FA4 RID: 4004 RVA: 0x00044CDA File Offset: 0x00042EDA
			public void CopyTo(JToken[] array, int arrayIndex)
			{
				if (this._token != null)
				{
					array[arrayIndex] = this._token;
				}
			}

			// Token: 0x06000FA5 RID: 4005 RVA: 0x00044CED File Offset: 0x00042EED
			public bool Remove(JToken item)
			{
				if (this._token == item)
				{
					this._token = null;
					return true;
				}
				return false;
			}

			// Token: 0x1700029F RID: 671
			// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x00044D02 File Offset: 0x00042F02
			public int Count
			{
				get
				{
					if (this._token == null)
					{
						return 0;
					}
					return 1;
				}
			}

			// Token: 0x170002A0 RID: 672
			// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x00044D0F File Offset: 0x00042F0F
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000FA8 RID: 4008 RVA: 0x00044D12 File Offset: 0x00042F12
			public int IndexOf(JToken item)
			{
				if (this._token != item)
				{
					return -1;
				}
				return 0;
			}

			// Token: 0x06000FA9 RID: 4009 RVA: 0x00044D20 File Offset: 0x00042F20
			public void Insert(int index, JToken item)
			{
				if (index == 0)
				{
					this._token = item;
				}
			}

			// Token: 0x06000FAA RID: 4010 RVA: 0x00044D2C File Offset: 0x00042F2C
			public void RemoveAt(int index)
			{
				if (index == 0)
				{
					this._token = null;
				}
			}

			// Token: 0x170002A1 RID: 673
			public JToken this[int index]
			{
				get
				{
					if (index != 0)
					{
						throw new IndexOutOfRangeException();
					}
					return this._token;
				}
				set
				{
					if (index != 0)
					{
						throw new IndexOutOfRangeException();
					}
					this._token = value;
				}
			}

			// Token: 0x040007A8 RID: 1960
			[Nullable(2)]
			internal JToken _token;
		}
	}
}
