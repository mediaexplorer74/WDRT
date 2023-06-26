using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x0200002C RID: 44
	[NullableContext(2)]
	[Nullable(0)]
	public class JsonSerializerSettings
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00005D2D File Offset: 0x00003F2D
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00005D3A File Offset: 0x00003F3A
		public ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				return this._referenceLoopHandling.GetValueOrDefault();
			}
			set
			{
				this._referenceLoopHandling = new ReferenceLoopHandling?(value);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00005D48 File Offset: 0x00003F48
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00005D55 File Offset: 0x00003F55
		public MissingMemberHandling MissingMemberHandling
		{
			get
			{
				return this._missingMemberHandling.GetValueOrDefault();
			}
			set
			{
				this._missingMemberHandling = new MissingMemberHandling?(value);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00005D63 File Offset: 0x00003F63
		// (set) Token: 0x0600019C RID: 412 RVA: 0x00005D70 File Offset: 0x00003F70
		public ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				return this._objectCreationHandling.GetValueOrDefault();
			}
			set
			{
				this._objectCreationHandling = new ObjectCreationHandling?(value);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00005D7E File Offset: 0x00003F7E
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00005D8B File Offset: 0x00003F8B
		public NullValueHandling NullValueHandling
		{
			get
			{
				return this._nullValueHandling.GetValueOrDefault();
			}
			set
			{
				this._nullValueHandling = new NullValueHandling?(value);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00005D99 File Offset: 0x00003F99
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00005DA6 File Offset: 0x00003FA6
		public DefaultValueHandling DefaultValueHandling
		{
			get
			{
				return this._defaultValueHandling.GetValueOrDefault();
			}
			set
			{
				this._defaultValueHandling = new DefaultValueHandling?(value);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00005DB4 File Offset: 0x00003FB4
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00005DBC File Offset: 0x00003FBC
		[Nullable(1)]
		public IList<JsonConverter> Converters
		{
			[NullableContext(1)]
			get;
			[NullableContext(1)]
			set;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00005DC5 File Offset: 0x00003FC5
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x00005DD2 File Offset: 0x00003FD2
		public PreserveReferencesHandling PreserveReferencesHandling
		{
			get
			{
				return this._preserveReferencesHandling.GetValueOrDefault();
			}
			set
			{
				this._preserveReferencesHandling = new PreserveReferencesHandling?(value);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00005DE0 File Offset: 0x00003FE0
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x00005DED File Offset: 0x00003FED
		public TypeNameHandling TypeNameHandling
		{
			get
			{
				return this._typeNameHandling.GetValueOrDefault();
			}
			set
			{
				this._typeNameHandling = new TypeNameHandling?(value);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00005DFB File Offset: 0x00003FFB
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00005E08 File Offset: 0x00004008
		public MetadataPropertyHandling MetadataPropertyHandling
		{
			get
			{
				return this._metadataPropertyHandling.GetValueOrDefault();
			}
			set
			{
				this._metadataPropertyHandling = new MetadataPropertyHandling?(value);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00005E16 File Offset: 0x00004016
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00005E1E File Offset: 0x0000401E
		[Obsolete("TypeNameAssemblyFormat is obsolete. Use TypeNameAssemblyFormatHandling instead.")]
		public FormatterAssemblyStyle TypeNameAssemblyFormat
		{
			get
			{
				return (FormatterAssemblyStyle)this.TypeNameAssemblyFormatHandling;
			}
			set
			{
				this.TypeNameAssemblyFormatHandling = (TypeNameAssemblyFormatHandling)value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00005E27 File Offset: 0x00004027
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00005E34 File Offset: 0x00004034
		public TypeNameAssemblyFormatHandling TypeNameAssemblyFormatHandling
		{
			get
			{
				return this._typeNameAssemblyFormatHandling.GetValueOrDefault();
			}
			set
			{
				this._typeNameAssemblyFormatHandling = new TypeNameAssemblyFormatHandling?(value);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00005E42 File Offset: 0x00004042
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00005E4F File Offset: 0x0000404F
		public ConstructorHandling ConstructorHandling
		{
			get
			{
				return this._constructorHandling.GetValueOrDefault();
			}
			set
			{
				this._constructorHandling = new ConstructorHandling?(value);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00005E5D File Offset: 0x0000405D
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00005E65 File Offset: 0x00004065
		public IContractResolver ContractResolver { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00005E6E File Offset: 0x0000406E
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00005E76 File Offset: 0x00004076
		public IEqualityComparer EqualityComparer { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00005E7F File Offset: 0x0000407F
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x00005E94 File Offset: 0x00004094
		[Obsolete("ReferenceResolver property is obsolete. Use the ReferenceResolverProvider property to set the IReferenceResolver: settings.ReferenceResolverProvider = () => resolver")]
		public IReferenceResolver ReferenceResolver
		{
			get
			{
				Func<IReferenceResolver> referenceResolverProvider = this.ReferenceResolverProvider;
				if (referenceResolverProvider == null)
				{
					return null;
				}
				return referenceResolverProvider();
			}
			set
			{
				this.ReferenceResolverProvider = ((value != null) ? (() => value) : null);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00005ECB File Offset: 0x000040CB
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00005ED3 File Offset: 0x000040D3
		public Func<IReferenceResolver> ReferenceResolverProvider { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00005EDC File Offset: 0x000040DC
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x00005EE4 File Offset: 0x000040E4
		public ITraceWriter TraceWriter { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00005EF0 File Offset: 0x000040F0
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00005F27 File Offset: 0x00004127
		[Obsolete("Binder is obsolete. Use SerializationBinder instead.")]
		public SerializationBinder Binder
		{
			get
			{
				if (this.SerializationBinder == null)
				{
					return null;
				}
				SerializationBinderAdapter serializationBinderAdapter = this.SerializationBinder as SerializationBinderAdapter;
				if (serializationBinderAdapter != null)
				{
					return serializationBinderAdapter.SerializationBinder;
				}
				throw new InvalidOperationException("Cannot get SerializationBinder because an ISerializationBinder was previously set.");
			}
			set
			{
				this.SerializationBinder = ((value == null) ? null : new SerializationBinderAdapter(value));
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00005F3B File Offset: 0x0000413B
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00005F43 File Offset: 0x00004143
		public ISerializationBinder SerializationBinder { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00005F4C File Offset: 0x0000414C
		// (set) Token: 0x060001BE RID: 446 RVA: 0x00005F54 File Offset: 0x00004154
		[Nullable(new byte[] { 2, 1 })]
		public EventHandler<ErrorEventArgs> Error
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00005F60 File Offset: 0x00004160
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x00005F8A File Offset: 0x0000418A
		public StreamingContext Context
		{
			get
			{
				StreamingContext? context = this._context;
				if (context == null)
				{
					return JsonSerializerSettings.DefaultContext;
				}
				return context.GetValueOrDefault();
			}
			set
			{
				this._context = new StreamingContext?(value);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00005F98 File Offset: 0x00004198
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x00005FA9 File Offset: 0x000041A9
		[Nullable(1)]
		public string DateFormatString
		{
			[NullableContext(1)]
			get
			{
				return this._dateFormatString ?? "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";
			}
			[NullableContext(1)]
			set
			{
				this._dateFormatString = value;
				this._dateFormatStringSet = true;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00005FB9 File Offset: 0x000041B9
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x00005FD4 File Offset: 0x000041D4
		public int? MaxDepth
		{
			get
			{
				if (!this._maxDepthSet)
				{
					return new int?(64);
				}
				return this._maxDepth;
			}
			set
			{
				int? num = value;
				int num2 = 0;
				if ((num.GetValueOrDefault() <= num2) & (num != null))
				{
					throw new ArgumentException("Value must be positive.", "value");
				}
				this._maxDepth = value;
				this._maxDepthSet = true;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000601A File Offset: 0x0000421A
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00006027 File Offset: 0x00004227
		public Formatting Formatting
		{
			get
			{
				return this._formatting.GetValueOrDefault();
			}
			set
			{
				this._formatting = new Formatting?(value);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00006035 File Offset: 0x00004235
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x00006042 File Offset: 0x00004242
		public DateFormatHandling DateFormatHandling
		{
			get
			{
				return this._dateFormatHandling.GetValueOrDefault();
			}
			set
			{
				this._dateFormatHandling = new DateFormatHandling?(value);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00006050 File Offset: 0x00004250
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00006076 File Offset: 0x00004276
		public DateTimeZoneHandling DateTimeZoneHandling
		{
			get
			{
				DateTimeZoneHandling? dateTimeZoneHandling = this._dateTimeZoneHandling;
				if (dateTimeZoneHandling == null)
				{
					return DateTimeZoneHandling.RoundtripKind;
				}
				return dateTimeZoneHandling.GetValueOrDefault();
			}
			set
			{
				this._dateTimeZoneHandling = new DateTimeZoneHandling?(value);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00006084 File Offset: 0x00004284
		// (set) Token: 0x060001CC RID: 460 RVA: 0x000060AA File Offset: 0x000042AA
		public DateParseHandling DateParseHandling
		{
			get
			{
				DateParseHandling? dateParseHandling = this._dateParseHandling;
				if (dateParseHandling == null)
				{
					return DateParseHandling.DateTime;
				}
				return dateParseHandling.GetValueOrDefault();
			}
			set
			{
				this._dateParseHandling = new DateParseHandling?(value);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001CD RID: 461 RVA: 0x000060B8 File Offset: 0x000042B8
		// (set) Token: 0x060001CE RID: 462 RVA: 0x000060C5 File Offset: 0x000042C5
		public FloatFormatHandling FloatFormatHandling
		{
			get
			{
				return this._floatFormatHandling.GetValueOrDefault();
			}
			set
			{
				this._floatFormatHandling = new FloatFormatHandling?(value);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001CF RID: 463 RVA: 0x000060D3 File Offset: 0x000042D3
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x000060E0 File Offset: 0x000042E0
		public FloatParseHandling FloatParseHandling
		{
			get
			{
				return this._floatParseHandling.GetValueOrDefault();
			}
			set
			{
				this._floatParseHandling = new FloatParseHandling?(value);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x000060EE File Offset: 0x000042EE
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x000060FB File Offset: 0x000042FB
		public StringEscapeHandling StringEscapeHandling
		{
			get
			{
				return this._stringEscapeHandling.GetValueOrDefault();
			}
			set
			{
				this._stringEscapeHandling = new StringEscapeHandling?(value);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00006109 File Offset: 0x00004309
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x0000611A File Offset: 0x0000431A
		[Nullable(1)]
		public CultureInfo Culture
		{
			[NullableContext(1)]
			get
			{
				return this._culture ?? JsonSerializerSettings.DefaultCulture;
			}
			[NullableContext(1)]
			set
			{
				this._culture = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00006123 File Offset: 0x00004323
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x00006130 File Offset: 0x00004330
		public bool CheckAdditionalContent
		{
			get
			{
				return this._checkAdditionalContent.GetValueOrDefault();
			}
			set
			{
				this._checkAdditionalContent = new bool?(value);
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00006155 File Offset: 0x00004355
		[DebuggerStepThrough]
		public JsonSerializerSettings()
		{
			this.Converters = new List<JsonConverter>();
		}

		// Token: 0x04000092 RID: 146
		internal const ReferenceLoopHandling DefaultReferenceLoopHandling = ReferenceLoopHandling.Error;

		// Token: 0x04000093 RID: 147
		internal const MissingMemberHandling DefaultMissingMemberHandling = MissingMemberHandling.Ignore;

		// Token: 0x04000094 RID: 148
		internal const NullValueHandling DefaultNullValueHandling = NullValueHandling.Include;

		// Token: 0x04000095 RID: 149
		internal const DefaultValueHandling DefaultDefaultValueHandling = DefaultValueHandling.Include;

		// Token: 0x04000096 RID: 150
		internal const ObjectCreationHandling DefaultObjectCreationHandling = ObjectCreationHandling.Auto;

		// Token: 0x04000097 RID: 151
		internal const PreserveReferencesHandling DefaultPreserveReferencesHandling = PreserveReferencesHandling.None;

		// Token: 0x04000098 RID: 152
		internal const ConstructorHandling DefaultConstructorHandling = ConstructorHandling.Default;

		// Token: 0x04000099 RID: 153
		internal const TypeNameHandling DefaultTypeNameHandling = TypeNameHandling.None;

		// Token: 0x0400009A RID: 154
		internal const MetadataPropertyHandling DefaultMetadataPropertyHandling = MetadataPropertyHandling.Default;

		// Token: 0x0400009B RID: 155
		internal static readonly StreamingContext DefaultContext = default(StreamingContext);

		// Token: 0x0400009C RID: 156
		internal const Formatting DefaultFormatting = Formatting.None;

		// Token: 0x0400009D RID: 157
		internal const DateFormatHandling DefaultDateFormatHandling = DateFormatHandling.IsoDateFormat;

		// Token: 0x0400009E RID: 158
		internal const DateTimeZoneHandling DefaultDateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;

		// Token: 0x0400009F RID: 159
		internal const DateParseHandling DefaultDateParseHandling = DateParseHandling.DateTime;

		// Token: 0x040000A0 RID: 160
		internal const FloatParseHandling DefaultFloatParseHandling = FloatParseHandling.Double;

		// Token: 0x040000A1 RID: 161
		internal const FloatFormatHandling DefaultFloatFormatHandling = FloatFormatHandling.String;

		// Token: 0x040000A2 RID: 162
		internal const StringEscapeHandling DefaultStringEscapeHandling = StringEscapeHandling.Default;

		// Token: 0x040000A3 RID: 163
		internal const TypeNameAssemblyFormatHandling DefaultTypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple;

		// Token: 0x040000A4 RID: 164
		[Nullable(1)]
		internal static readonly CultureInfo DefaultCulture = CultureInfo.InvariantCulture;

		// Token: 0x040000A5 RID: 165
		internal const bool DefaultCheckAdditionalContent = false;

		// Token: 0x040000A6 RID: 166
		[Nullable(1)]
		internal const string DefaultDateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";

		// Token: 0x040000A7 RID: 167
		internal const int DefaultMaxDepth = 64;

		// Token: 0x040000A8 RID: 168
		internal Formatting? _formatting;

		// Token: 0x040000A9 RID: 169
		internal DateFormatHandling? _dateFormatHandling;

		// Token: 0x040000AA RID: 170
		internal DateTimeZoneHandling? _dateTimeZoneHandling;

		// Token: 0x040000AB RID: 171
		internal DateParseHandling? _dateParseHandling;

		// Token: 0x040000AC RID: 172
		internal FloatFormatHandling? _floatFormatHandling;

		// Token: 0x040000AD RID: 173
		internal FloatParseHandling? _floatParseHandling;

		// Token: 0x040000AE RID: 174
		internal StringEscapeHandling? _stringEscapeHandling;

		// Token: 0x040000AF RID: 175
		internal CultureInfo _culture;

		// Token: 0x040000B0 RID: 176
		internal bool? _checkAdditionalContent;

		// Token: 0x040000B1 RID: 177
		internal int? _maxDepth;

		// Token: 0x040000B2 RID: 178
		internal bool _maxDepthSet;

		// Token: 0x040000B3 RID: 179
		internal string _dateFormatString;

		// Token: 0x040000B4 RID: 180
		internal bool _dateFormatStringSet;

		// Token: 0x040000B5 RID: 181
		internal TypeNameAssemblyFormatHandling? _typeNameAssemblyFormatHandling;

		// Token: 0x040000B6 RID: 182
		internal DefaultValueHandling? _defaultValueHandling;

		// Token: 0x040000B7 RID: 183
		internal PreserveReferencesHandling? _preserveReferencesHandling;

		// Token: 0x040000B8 RID: 184
		internal NullValueHandling? _nullValueHandling;

		// Token: 0x040000B9 RID: 185
		internal ObjectCreationHandling? _objectCreationHandling;

		// Token: 0x040000BA RID: 186
		internal MissingMemberHandling? _missingMemberHandling;

		// Token: 0x040000BB RID: 187
		internal ReferenceLoopHandling? _referenceLoopHandling;

		// Token: 0x040000BC RID: 188
		internal StreamingContext? _context;

		// Token: 0x040000BD RID: 189
		internal ConstructorHandling? _constructorHandling;

		// Token: 0x040000BE RID: 190
		internal TypeNameHandling? _typeNameHandling;

		// Token: 0x040000BF RID: 191
		internal MetadataPropertyHandling? _metadataPropertyHandling;
	}
}
