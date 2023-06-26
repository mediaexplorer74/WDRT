using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020000A3 RID: 163
	[NullableContext(1)]
	[Nullable(0)]
	internal class TraceJsonReader : JsonReader, IJsonLineInfo
	{
		// Token: 0x0600083F RID: 2111 RVA: 0x00023D70 File Offset: 0x00021F70
		public TraceJsonReader(JsonReader innerReader)
		{
			this._innerReader = innerReader;
			this._sw = new StringWriter(CultureInfo.InvariantCulture);
			this._sw.Write("Deserialized JSON: " + Environment.NewLine);
			this._textWriter = new JsonTextWriter(this._sw);
			this._textWriter.Formatting = Formatting.Indented;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00023DD1 File Offset: 0x00021FD1
		public string GetDeserializedJsonMessage()
		{
			return this._sw.ToString();
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00023DDE File Offset: 0x00021FDE
		public override bool Read()
		{
			bool flag = this._innerReader.Read();
			this.WriteCurrentToken();
			return flag;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00023DF1 File Offset: 0x00021FF1
		public override int? ReadAsInt32()
		{
			int? num = this._innerReader.ReadAsInt32();
			this.WriteCurrentToken();
			return num;
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00023E04 File Offset: 0x00022004
		[NullableContext(2)]
		public override string ReadAsString()
		{
			string text = this._innerReader.ReadAsString();
			this.WriteCurrentToken();
			return text;
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00023E17 File Offset: 0x00022017
		[NullableContext(2)]
		public override byte[] ReadAsBytes()
		{
			byte[] array = this._innerReader.ReadAsBytes();
			this.WriteCurrentToken();
			return array;
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00023E2A File Offset: 0x0002202A
		public override decimal? ReadAsDecimal()
		{
			decimal? num = this._innerReader.ReadAsDecimal();
			this.WriteCurrentToken();
			return num;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00023E3D File Offset: 0x0002203D
		public override double? ReadAsDouble()
		{
			double? num = this._innerReader.ReadAsDouble();
			this.WriteCurrentToken();
			return num;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00023E50 File Offset: 0x00022050
		public override bool? ReadAsBoolean()
		{
			bool? flag = this._innerReader.ReadAsBoolean();
			this.WriteCurrentToken();
			return flag;
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00023E63 File Offset: 0x00022063
		public override DateTime? ReadAsDateTime()
		{
			DateTime? dateTime = this._innerReader.ReadAsDateTime();
			this.WriteCurrentToken();
			return dateTime;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00023E76 File Offset: 0x00022076
		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			DateTimeOffset? dateTimeOffset = this._innerReader.ReadAsDateTimeOffset();
			this.WriteCurrentToken();
			return dateTimeOffset;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00023E89 File Offset: 0x00022089
		public void WriteCurrentToken()
		{
			this._textWriter.WriteToken(this._innerReader, false, false, true);
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x00023E9F File Offset: 0x0002209F
		public override int Depth
		{
			get
			{
				return this._innerReader.Depth;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x00023EAC File Offset: 0x000220AC
		public override string Path
		{
			get
			{
				return this._innerReader.Path;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x00023EB9 File Offset: 0x000220B9
		// (set) Token: 0x0600084E RID: 2126 RVA: 0x00023EC6 File Offset: 0x000220C6
		public override char QuoteChar
		{
			get
			{
				return this._innerReader.QuoteChar;
			}
			protected internal set
			{
				this._innerReader.QuoteChar = value;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x00023ED4 File Offset: 0x000220D4
		public override JsonToken TokenType
		{
			get
			{
				return this._innerReader.TokenType;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x00023EE1 File Offset: 0x000220E1
		[Nullable(2)]
		public override object Value
		{
			[NullableContext(2)]
			get
			{
				return this._innerReader.Value;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x00023EEE File Offset: 0x000220EE
		[Nullable(2)]
		public override Type ValueType
		{
			[NullableContext(2)]
			get
			{
				return this._innerReader.ValueType;
			}
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00023EFB File Offset: 0x000220FB
		public override void Close()
		{
			this._innerReader.Close();
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00023F08 File Offset: 0x00022108
		bool IJsonLineInfo.HasLineInfo()
		{
			IJsonLineInfo jsonLineInfo = this._innerReader as IJsonLineInfo;
			return jsonLineInfo != null && jsonLineInfo.HasLineInfo();
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x00023F2C File Offset: 0x0002212C
		int IJsonLineInfo.LineNumber
		{
			get
			{
				IJsonLineInfo jsonLineInfo = this._innerReader as IJsonLineInfo;
				if (jsonLineInfo == null)
				{
					return 0;
				}
				return jsonLineInfo.LineNumber;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x00023F50 File Offset: 0x00022150
		int IJsonLineInfo.LinePosition
		{
			get
			{
				IJsonLineInfo jsonLineInfo = this._innerReader as IJsonLineInfo;
				if (jsonLineInfo == null)
				{
					return 0;
				}
				return jsonLineInfo.LinePosition;
			}
		}

		// Token: 0x040002CB RID: 715
		private readonly JsonReader _innerReader;

		// Token: 0x040002CC RID: 716
		private readonly JsonTextWriter _textWriter;

		// Token: 0x040002CD RID: 717
		private readonly StringWriter _sw;
	}
}
