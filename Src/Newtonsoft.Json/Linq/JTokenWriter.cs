using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000C9 RID: 201
	[NullableContext(2)]
	[Nullable(0)]
	public class JTokenWriter : JsonWriter
	{
		// Token: 0x06000B74 RID: 2932 RVA: 0x0002D7DD File Offset: 0x0002B9DD
		[NullableContext(1)]
		internal override Task WriteTokenAsync(JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate, bool writeComments, CancellationToken cancellationToken)
		{
			if (reader is JTokenReader)
			{
				this.WriteToken(reader, writeChildren, writeDateConstructorAsDate, writeComments);
				return AsyncUtils.CompletedTask;
			}
			return base.WriteTokenSyncReadingAsync(reader, cancellationToken);
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x0002D801 File Offset: 0x0002BA01
		public JToken CurrentToken
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x0002D809 File Offset: 0x0002BA09
		public JToken Token
		{
			get
			{
				if (this._token != null)
				{
					return this._token;
				}
				return this._value;
			}
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0002D820 File Offset: 0x0002BA20
		[NullableContext(1)]
		public JTokenWriter(JContainer container)
		{
			ValidationUtils.ArgumentNotNull(container, "container");
			this._token = container;
			this._parent = container;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0002D841 File Offset: 0x0002BA41
		public JTokenWriter()
		{
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0002D849 File Offset: 0x0002BA49
		public override void Flush()
		{
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0002D84B File Offset: 0x0002BA4B
		public override void Close()
		{
			base.Close();
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0002D853 File Offset: 0x0002BA53
		public override void WriteStartObject()
		{
			base.WriteStartObject();
			this.AddParent(new JObject());
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0002D866 File Offset: 0x0002BA66
		[NullableContext(1)]
		private void AddParent(JContainer container)
		{
			if (this._parent == null)
			{
				this._token = container;
			}
			else
			{
				this._parent.AddAndSkipParentCheck(container);
			}
			this._parent = container;
			this._current = container;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0002D894 File Offset: 0x0002BA94
		private void RemoveParent()
		{
			this._current = this._parent;
			this._parent = this._parent.Parent;
			if (this._parent != null && this._parent.Type == JTokenType.Property)
			{
				this._parent = this._parent.Parent;
			}
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0002D8E5 File Offset: 0x0002BAE5
		public override void WriteStartArray()
		{
			base.WriteStartArray();
			this.AddParent(new JArray());
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0002D8F8 File Offset: 0x0002BAF8
		[NullableContext(1)]
		public override void WriteStartConstructor(string name)
		{
			base.WriteStartConstructor(name);
			this.AddParent(new JConstructor(name));
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0002D90D File Offset: 0x0002BB0D
		protected override void WriteEnd(JsonToken token)
		{
			this.RemoveParent();
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0002D915 File Offset: 0x0002BB15
		[NullableContext(1)]
		public override void WritePropertyName(string name)
		{
			JObject jobject = this._parent as JObject;
			if (jobject != null)
			{
				jobject.Remove(name);
			}
			this.AddParent(new JProperty(name));
			base.WritePropertyName(name);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0002D942 File Offset: 0x0002BB42
		private void AddValue(object value, JsonToken token)
		{
			this.AddValue(new JValue(value), token);
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0002D954 File Offset: 0x0002BB54
		internal void AddValue(JValue value, JsonToken token)
		{
			if (this._parent != null)
			{
				if (this._parent.TryAdd(value))
				{
					this._current = this._parent.Last;
					if (this._parent.Type == JTokenType.Property)
					{
						this._parent = this._parent.Parent;
						return;
					}
				}
			}
			else
			{
				this._value = value ?? JValue.CreateNull();
				this._current = this._value;
			}
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0002D9C4 File Offset: 0x0002BBC4
		public override void WriteValue(object value)
		{
			if (value is BigInteger)
			{
				base.InternalWriteValue(JsonToken.Integer);
				this.AddValue(value, JsonToken.Integer);
				return;
			}
			base.WriteValue(value);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0002D9E5 File Offset: 0x0002BBE5
		public override void WriteNull()
		{
			base.WriteNull();
			this.AddValue(null, JsonToken.Null);
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0002D9F6 File Offset: 0x0002BBF6
		public override void WriteUndefined()
		{
			base.WriteUndefined();
			this.AddValue(null, JsonToken.Undefined);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0002DA07 File Offset: 0x0002BC07
		public override void WriteRaw(string json)
		{
			base.WriteRaw(json);
			this.AddValue(new JRaw(json), JsonToken.Raw);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0002DA1D File Offset: 0x0002BC1D
		public override void WriteComment(string text)
		{
			base.WriteComment(text);
			this.AddValue(JValue.CreateComment(text), JsonToken.Comment);
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0002DA33 File Offset: 0x0002BC33
		public override void WriteValue(string value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0002DA45 File Offset: 0x0002BC45
		public override void WriteValue(int value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0002DA5B File Offset: 0x0002BC5B
		[CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0002DA71 File Offset: 0x0002BC71
		public override void WriteValue(long value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0002DA87 File Offset: 0x0002BC87
		[CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0002DA9D File Offset: 0x0002BC9D
		public override void WriteValue(float value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Float);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0002DAB3 File Offset: 0x0002BCB3
		public override void WriteValue(double value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Float);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0002DAC9 File Offset: 0x0002BCC9
		public override void WriteValue(bool value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Boolean);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0002DAE0 File Offset: 0x0002BCE0
		public override void WriteValue(short value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0002DAF6 File Offset: 0x0002BCF6
		[CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0002DB0C File Offset: 0x0002BD0C
		public override void WriteValue(char value)
		{
			base.WriteValue(value);
			string text = value.ToString(CultureInfo.InvariantCulture);
			this.AddValue(text, JsonToken.String);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0002DB36 File Offset: 0x0002BD36
		public override void WriteValue(byte value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0002DB4C File Offset: 0x0002BD4C
		[CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0002DB62 File Offset: 0x0002BD62
		public override void WriteValue(decimal value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Float);
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0002DB78 File Offset: 0x0002BD78
		public override void WriteValue(DateTime value)
		{
			base.WriteValue(value);
			value = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			this.AddValue(value, JsonToken.Date);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0002DB9D File Offset: 0x0002BD9D
		public override void WriteValue(DateTimeOffset value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Date);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0002DBB4 File Offset: 0x0002BDB4
		public override void WriteValue(byte[] value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Bytes);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0002DBC6 File Offset: 0x0002BDC6
		public override void WriteValue(TimeSpan value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0002DBDD File Offset: 0x0002BDDD
		public override void WriteValue(Guid value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0002DBF4 File Offset: 0x0002BDF4
		public override void WriteValue(Uri value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0002DC08 File Offset: 0x0002BE08
		[NullableContext(1)]
		internal override void WriteToken(JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate, bool writeComments)
		{
			JTokenReader jtokenReader = reader as JTokenReader;
			if (jtokenReader == null || !writeChildren || !writeDateConstructorAsDate || !writeComments)
			{
				base.WriteToken(reader, writeChildren, writeDateConstructorAsDate, writeComments);
				return;
			}
			if (jtokenReader.TokenType == JsonToken.None && !jtokenReader.Read())
			{
				return;
			}
			JToken jtoken = jtokenReader.CurrentToken.CloneToken();
			if (this._parent != null)
			{
				this._parent.Add(jtoken);
				this._current = this._parent.Last;
				if (this._parent.Type == JTokenType.Property)
				{
					this._parent = this._parent.Parent;
					base.InternalWriteValue(JsonToken.Null);
				}
			}
			else
			{
				this._current = jtoken;
				if (this._token == null && this._value == null)
				{
					this._token = jtoken as JContainer;
					this._value = jtoken as JValue;
				}
			}
			jtokenReader.Skip();
		}

		// Token: 0x0400039D RID: 925
		private JContainer _token;

		// Token: 0x0400039E RID: 926
		private JContainer _parent;

		// Token: 0x0400039F RID: 927
		private JValue _value;

		// Token: 0x040003A0 RID: 928
		private JToken _current;
	}
}
