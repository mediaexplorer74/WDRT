using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x0200002B RID: 43
	[NullableContext(1)]
	[Nullable(0)]
	public class JsonSerializer
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600013F RID: 319 RVA: 0x00004CA0 File Offset: 0x00002EA0
		// (remove) Token: 0x06000140 RID: 320 RVA: 0x00004CD8 File Offset: 0x00002ED8
		[Nullable(new byte[] { 2, 1 })]
		[field: Nullable(new byte[] { 2, 1 })]
		public virtual event EventHandler<Newtonsoft.Json.Serialization.ErrorEventArgs> Error;

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00004D0D File Offset: 0x00002F0D
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00004D15 File Offset: 0x00002F15
		[Nullable(2)]
		public virtual IReferenceResolver ReferenceResolver
		{
			[NullableContext(2)]
			get
			{
				return this.GetReferenceResolver();
			}
			[NullableContext(2)]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", "Reference resolver cannot be null.");
				}
				this._referenceResolver = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00004D34 File Offset: 0x00002F34
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00004D72 File Offset: 0x00002F72
		[Obsolete("Binder is obsolete. Use SerializationBinder instead.")]
		public virtual SerializationBinder Binder
		{
			get
			{
				SerializationBinder serializationBinder = this._serializationBinder as SerializationBinder;
				if (serializationBinder != null)
				{
					return serializationBinder;
				}
				SerializationBinderAdapter serializationBinderAdapter = this._serializationBinder as SerializationBinderAdapter;
				if (serializationBinderAdapter != null)
				{
					return serializationBinderAdapter.SerializationBinder;
				}
				throw new InvalidOperationException("Cannot get SerializationBinder because an ISerializationBinder was previously set.");
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", "Serialization binder cannot be null.");
				}
				this._serializationBinder = (value as ISerializationBinder) ?? new SerializationBinderAdapter(value);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00004D9D File Offset: 0x00002F9D
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00004DA5 File Offset: 0x00002FA5
		public virtual ISerializationBinder SerializationBinder
		{
			get
			{
				return this._serializationBinder;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", "Serialization binder cannot be null.");
				}
				this._serializationBinder = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00004DC1 File Offset: 0x00002FC1
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00004DC9 File Offset: 0x00002FC9
		[Nullable(2)]
		public virtual ITraceWriter TraceWriter
		{
			[NullableContext(2)]
			get
			{
				return this._traceWriter;
			}
			[NullableContext(2)]
			set
			{
				this._traceWriter = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00004DD2 File Offset: 0x00002FD2
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00004DDA File Offset: 0x00002FDA
		[Nullable(2)]
		public virtual IEqualityComparer EqualityComparer
		{
			[NullableContext(2)]
			get
			{
				return this._equalityComparer;
			}
			[NullableContext(2)]
			set
			{
				this._equalityComparer = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00004DE3 File Offset: 0x00002FE3
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00004DEB File Offset: 0x00002FEB
		public virtual TypeNameHandling TypeNameHandling
		{
			get
			{
				return this._typeNameHandling;
			}
			set
			{
				if (value < TypeNameHandling.None || value > TypeNameHandling.Auto)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._typeNameHandling = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00004E07 File Offset: 0x00003007
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00004E0F File Offset: 0x0000300F
		[Obsolete("TypeNameAssemblyFormat is obsolete. Use TypeNameAssemblyFormatHandling instead.")]
		public virtual FormatterAssemblyStyle TypeNameAssemblyFormat
		{
			get
			{
				return (FormatterAssemblyStyle)this._typeNameAssemblyFormatHandling;
			}
			set
			{
				if (value < FormatterAssemblyStyle.Simple || value > FormatterAssemblyStyle.Full)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._typeNameAssemblyFormatHandling = (TypeNameAssemblyFormatHandling)value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00004E2B File Offset: 0x0000302B
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00004E33 File Offset: 0x00003033
		public virtual TypeNameAssemblyFormatHandling TypeNameAssemblyFormatHandling
		{
			get
			{
				return this._typeNameAssemblyFormatHandling;
			}
			set
			{
				if (value < TypeNameAssemblyFormatHandling.Simple || value > TypeNameAssemblyFormatHandling.Full)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._typeNameAssemblyFormatHandling = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00004E4F File Offset: 0x0000304F
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00004E57 File Offset: 0x00003057
		public virtual PreserveReferencesHandling PreserveReferencesHandling
		{
			get
			{
				return this._preserveReferencesHandling;
			}
			set
			{
				if (value < PreserveReferencesHandling.None || value > PreserveReferencesHandling.All)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._preserveReferencesHandling = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00004E73 File Offset: 0x00003073
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00004E7B File Offset: 0x0000307B
		public virtual ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				return this._referenceLoopHandling;
			}
			set
			{
				if (value < ReferenceLoopHandling.Error || value > ReferenceLoopHandling.Serialize)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._referenceLoopHandling = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00004E97 File Offset: 0x00003097
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00004E9F File Offset: 0x0000309F
		public virtual MissingMemberHandling MissingMemberHandling
		{
			get
			{
				return this._missingMemberHandling;
			}
			set
			{
				if (value < MissingMemberHandling.Ignore || value > MissingMemberHandling.Error)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._missingMemberHandling = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00004EBB File Offset: 0x000030BB
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00004EC3 File Offset: 0x000030C3
		public virtual NullValueHandling NullValueHandling
		{
			get
			{
				return this._nullValueHandling;
			}
			set
			{
				if (value < NullValueHandling.Include || value > NullValueHandling.Ignore)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._nullValueHandling = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00004EDF File Offset: 0x000030DF
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00004EE7 File Offset: 0x000030E7
		public virtual DefaultValueHandling DefaultValueHandling
		{
			get
			{
				return this._defaultValueHandling;
			}
			set
			{
				if (value < DefaultValueHandling.Include || value > DefaultValueHandling.IgnoreAndPopulate)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._defaultValueHandling = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00004F03 File Offset: 0x00003103
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00004F0B File Offset: 0x0000310B
		public virtual ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				return this._objectCreationHandling;
			}
			set
			{
				if (value < ObjectCreationHandling.Auto || value > ObjectCreationHandling.Replace)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._objectCreationHandling = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00004F27 File Offset: 0x00003127
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00004F2F File Offset: 0x0000312F
		public virtual ConstructorHandling ConstructorHandling
		{
			get
			{
				return this._constructorHandling;
			}
			set
			{
				if (value < ConstructorHandling.Default || value > ConstructorHandling.AllowNonPublicDefaultConstructor)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._constructorHandling = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00004F4B File Offset: 0x0000314B
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00004F53 File Offset: 0x00003153
		public virtual MetadataPropertyHandling MetadataPropertyHandling
		{
			get
			{
				return this._metadataPropertyHandling;
			}
			set
			{
				if (value < MetadataPropertyHandling.Default || value > MetadataPropertyHandling.Ignore)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._metadataPropertyHandling = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00004F6F File Offset: 0x0000316F
		public virtual JsonConverterCollection Converters
		{
			get
			{
				if (this._converters == null)
				{
					this._converters = new JsonConverterCollection();
				}
				return this._converters;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00004F8A File Offset: 0x0000318A
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00004F92 File Offset: 0x00003192
		public virtual IContractResolver ContractResolver
		{
			get
			{
				return this._contractResolver;
			}
			set
			{
				this._contractResolver = value ?? DefaultContractResolver.Instance;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00004FA4 File Offset: 0x000031A4
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00004FAC File Offset: 0x000031AC
		public virtual StreamingContext Context
		{
			get
			{
				return this._context;
			}
			set
			{
				this._context = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00004FB5 File Offset: 0x000031B5
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00004FC2 File Offset: 0x000031C2
		public virtual Formatting Formatting
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

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00004FD0 File Offset: 0x000031D0
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00004FDD File Offset: 0x000031DD
		public virtual DateFormatHandling DateFormatHandling
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

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00004FEC File Offset: 0x000031EC
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00005012 File Offset: 0x00003212
		public virtual DateTimeZoneHandling DateTimeZoneHandling
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

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00005020 File Offset: 0x00003220
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00005046 File Offset: 0x00003246
		public virtual DateParseHandling DateParseHandling
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

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00005054 File Offset: 0x00003254
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00005061 File Offset: 0x00003261
		public virtual FloatParseHandling FloatParseHandling
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

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000506F File Offset: 0x0000326F
		// (set) Token: 0x06000171 RID: 369 RVA: 0x0000507C File Offset: 0x0000327C
		public virtual FloatFormatHandling FloatFormatHandling
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

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000508A File Offset: 0x0000328A
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00005097 File Offset: 0x00003297
		public virtual StringEscapeHandling StringEscapeHandling
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

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000174 RID: 372 RVA: 0x000050A5 File Offset: 0x000032A5
		// (set) Token: 0x06000175 RID: 373 RVA: 0x000050B6 File Offset: 0x000032B6
		public virtual string DateFormatString
		{
			get
			{
				return this._dateFormatString ?? "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";
			}
			set
			{
				this._dateFormatString = value;
				this._dateFormatStringSet = true;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000176 RID: 374 RVA: 0x000050C6 File Offset: 0x000032C6
		// (set) Token: 0x06000177 RID: 375 RVA: 0x000050D7 File Offset: 0x000032D7
		public virtual CultureInfo Culture
		{
			get
			{
				return this._culture ?? JsonSerializerSettings.DefaultCulture;
			}
			set
			{
				this._culture = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000178 RID: 376 RVA: 0x000050E0 File Offset: 0x000032E0
		// (set) Token: 0x06000179 RID: 377 RVA: 0x000050E8 File Offset: 0x000032E8
		public virtual int? MaxDepth
		{
			get
			{
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

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000512E File Offset: 0x0000332E
		// (set) Token: 0x0600017B RID: 379 RVA: 0x0000513B File Offset: 0x0000333B
		public virtual bool CheckAdditionalContent
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

		// Token: 0x0600017C RID: 380 RVA: 0x00005149 File Offset: 0x00003349
		internal bool IsCheckAdditionalContentSet()
		{
			return this._checkAdditionalContent != null;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005158 File Offset: 0x00003358
		public JsonSerializer()
		{
			this._referenceLoopHandling = ReferenceLoopHandling.Error;
			this._missingMemberHandling = MissingMemberHandling.Ignore;
			this._nullValueHandling = NullValueHandling.Include;
			this._defaultValueHandling = DefaultValueHandling.Include;
			this._objectCreationHandling = ObjectCreationHandling.Auto;
			this._preserveReferencesHandling = PreserveReferencesHandling.None;
			this._constructorHandling = ConstructorHandling.Default;
			this._typeNameHandling = TypeNameHandling.None;
			this._metadataPropertyHandling = MetadataPropertyHandling.Default;
			this._context = JsonSerializerSettings.DefaultContext;
			this._serializationBinder = DefaultSerializationBinder.Instance;
			this._culture = JsonSerializerSettings.DefaultCulture;
			this._contractResolver = DefaultContractResolver.Instance;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000051D6 File Offset: 0x000033D6
		public static JsonSerializer Create()
		{
			return new JsonSerializer();
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000051E0 File Offset: 0x000033E0
		public static JsonSerializer Create([Nullable(2)] JsonSerializerSettings settings)
		{
			JsonSerializer jsonSerializer = JsonSerializer.Create();
			if (settings != null)
			{
				JsonSerializer.ApplySerializerSettings(jsonSerializer, settings);
			}
			return jsonSerializer;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000051FE File Offset: 0x000033FE
		public static JsonSerializer CreateDefault()
		{
			Func<JsonSerializerSettings> defaultSettings = JsonConvert.DefaultSettings;
			return JsonSerializer.Create((defaultSettings != null) ? defaultSettings() : null);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005218 File Offset: 0x00003418
		public static JsonSerializer CreateDefault([Nullable(2)] JsonSerializerSettings settings)
		{
			JsonSerializer jsonSerializer = JsonSerializer.CreateDefault();
			if (settings != null)
			{
				JsonSerializer.ApplySerializerSettings(jsonSerializer, settings);
			}
			return jsonSerializer;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00005238 File Offset: 0x00003438
		private static void ApplySerializerSettings(JsonSerializer serializer, JsonSerializerSettings settings)
		{
			if (!CollectionUtils.IsNullOrEmpty<JsonConverter>(settings.Converters))
			{
				for (int i = 0; i < settings.Converters.Count; i++)
				{
					serializer.Converters.Insert(i, settings.Converters[i]);
				}
			}
			if (settings._typeNameHandling != null)
			{
				serializer.TypeNameHandling = settings.TypeNameHandling;
			}
			if (settings._metadataPropertyHandling != null)
			{
				serializer.MetadataPropertyHandling = settings.MetadataPropertyHandling;
			}
			if (settings._typeNameAssemblyFormatHandling != null)
			{
				serializer.TypeNameAssemblyFormatHandling = settings.TypeNameAssemblyFormatHandling;
			}
			if (settings._preserveReferencesHandling != null)
			{
				serializer.PreserveReferencesHandling = settings.PreserveReferencesHandling;
			}
			if (settings._referenceLoopHandling != null)
			{
				serializer.ReferenceLoopHandling = settings.ReferenceLoopHandling;
			}
			if (settings._missingMemberHandling != null)
			{
				serializer.MissingMemberHandling = settings.MissingMemberHandling;
			}
			if (settings._objectCreationHandling != null)
			{
				serializer.ObjectCreationHandling = settings.ObjectCreationHandling;
			}
			if (settings._nullValueHandling != null)
			{
				serializer.NullValueHandling = settings.NullValueHandling;
			}
			if (settings._defaultValueHandling != null)
			{
				serializer.DefaultValueHandling = settings.DefaultValueHandling;
			}
			if (settings._constructorHandling != null)
			{
				serializer.ConstructorHandling = settings.ConstructorHandling;
			}
			if (settings._context != null)
			{
				serializer.Context = settings.Context;
			}
			if (settings._checkAdditionalContent != null)
			{
				serializer._checkAdditionalContent = settings._checkAdditionalContent;
			}
			if (settings.Error != null)
			{
				serializer.Error += settings.Error;
			}
			if (settings.ContractResolver != null)
			{
				serializer.ContractResolver = settings.ContractResolver;
			}
			if (settings.ReferenceResolverProvider != null)
			{
				serializer.ReferenceResolver = settings.ReferenceResolverProvider();
			}
			if (settings.TraceWriter != null)
			{
				serializer.TraceWriter = settings.TraceWriter;
			}
			if (settings.EqualityComparer != null)
			{
				serializer.EqualityComparer = settings.EqualityComparer;
			}
			if (settings.SerializationBinder != null)
			{
				serializer.SerializationBinder = settings.SerializationBinder;
			}
			if (settings._formatting != null)
			{
				serializer._formatting = settings._formatting;
			}
			if (settings._dateFormatHandling != null)
			{
				serializer._dateFormatHandling = settings._dateFormatHandling;
			}
			if (settings._dateTimeZoneHandling != null)
			{
				serializer._dateTimeZoneHandling = settings._dateTimeZoneHandling;
			}
			if (settings._dateParseHandling != null)
			{
				serializer._dateParseHandling = settings._dateParseHandling;
			}
			if (settings._dateFormatStringSet)
			{
				serializer._dateFormatString = settings._dateFormatString;
				serializer._dateFormatStringSet = settings._dateFormatStringSet;
			}
			if (settings._floatFormatHandling != null)
			{
				serializer._floatFormatHandling = settings._floatFormatHandling;
			}
			if (settings._floatParseHandling != null)
			{
				serializer._floatParseHandling = settings._floatParseHandling;
			}
			if (settings._stringEscapeHandling != null)
			{
				serializer._stringEscapeHandling = settings._stringEscapeHandling;
			}
			if (settings._culture != null)
			{
				serializer._culture = settings._culture;
			}
			if (settings._maxDepthSet)
			{
				serializer._maxDepth = settings._maxDepth;
				serializer._maxDepthSet = settings._maxDepthSet;
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000552C File Offset: 0x0000372C
		[DebuggerStepThrough]
		public void Populate(TextReader reader, object target)
		{
			this.Populate(new JsonTextReader(reader), target);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000553B File Offset: 0x0000373B
		[DebuggerStepThrough]
		public void Populate(JsonReader reader, object target)
		{
			this.PopulateInternal(reader, target);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00005548 File Offset: 0x00003748
		internal virtual void PopulateInternal(JsonReader reader, object target)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			ValidationUtils.ArgumentNotNull(target, "target");
			CultureInfo cultureInfo;
			DateTimeZoneHandling? dateTimeZoneHandling;
			DateParseHandling? dateParseHandling;
			FloatParseHandling? floatParseHandling;
			int? num;
			string text;
			this.SetupReader(reader, out cultureInfo, out dateTimeZoneHandling, out dateParseHandling, out floatParseHandling, out num, out text);
			TraceJsonReader traceJsonReader = ((this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose) ? this.CreateTraceJsonReader(reader) : null);
			new JsonSerializerInternalReader(this).Populate(traceJsonReader ?? reader, target);
			if (traceJsonReader != null)
			{
				this.TraceWriter.Trace(TraceLevel.Verbose, traceJsonReader.GetDeserializedJsonMessage(), null);
			}
			this.ResetReader(reader, cultureInfo, dateTimeZoneHandling, dateParseHandling, floatParseHandling, num, text);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000055DA File Offset: 0x000037DA
		[DebuggerStepThrough]
		[return: Nullable(2)]
		public object Deserialize(JsonReader reader)
		{
			return this.Deserialize(reader, null);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000055E4 File Offset: 0x000037E4
		[DebuggerStepThrough]
		[return: Nullable(2)]
		public object Deserialize(TextReader reader, Type objectType)
		{
			return this.Deserialize(new JsonTextReader(reader), objectType);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000055F3 File Offset: 0x000037F3
		[NullableContext(2)]
		[DebuggerStepThrough]
		public T Deserialize<T>([Nullable(1)] JsonReader reader)
		{
			return (T)((object)this.Deserialize(reader, typeof(T)));
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000560B File Offset: 0x0000380B
		[NullableContext(2)]
		[DebuggerStepThrough]
		public object Deserialize([Nullable(1)] JsonReader reader, Type objectType)
		{
			return this.DeserializeInternal(reader, objectType);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00005618 File Offset: 0x00003818
		[NullableContext(2)]
		internal virtual object DeserializeInternal([Nullable(1)] JsonReader reader, Type objectType)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			CultureInfo cultureInfo;
			DateTimeZoneHandling? dateTimeZoneHandling;
			DateParseHandling? dateParseHandling;
			FloatParseHandling? floatParseHandling;
			int? num;
			string text;
			this.SetupReader(reader, out cultureInfo, out dateTimeZoneHandling, out dateParseHandling, out floatParseHandling, out num, out text);
			TraceJsonReader traceJsonReader = ((this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose) ? this.CreateTraceJsonReader(reader) : null);
			object obj = new JsonSerializerInternalReader(this).Deserialize(traceJsonReader ?? reader, objectType, this.CheckAdditionalContent);
			if (traceJsonReader != null)
			{
				this.TraceWriter.Trace(TraceLevel.Verbose, traceJsonReader.GetDeserializedJsonMessage(), null);
			}
			this.ResetReader(reader, cultureInfo, dateTimeZoneHandling, dateParseHandling, floatParseHandling, num, text);
			return obj;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000056A8 File Offset: 0x000038A8
		[NullableContext(2)]
		private void SetupReader([Nullable(1)] JsonReader reader, out CultureInfo previousCulture, out DateTimeZoneHandling? previousDateTimeZoneHandling, out DateParseHandling? previousDateParseHandling, out FloatParseHandling? previousFloatParseHandling, out int? previousMaxDepth, out string previousDateFormatString)
		{
			if (this._culture != null && !this._culture.Equals(reader.Culture))
			{
				previousCulture = reader.Culture;
				reader.Culture = this._culture;
			}
			else
			{
				previousCulture = null;
			}
			if (this._dateTimeZoneHandling != null)
			{
				DateTimeZoneHandling dateTimeZoneHandling = reader.DateTimeZoneHandling;
				DateTimeZoneHandling? dateTimeZoneHandling2 = this._dateTimeZoneHandling;
				if (!((dateTimeZoneHandling == dateTimeZoneHandling2.GetValueOrDefault()) & (dateTimeZoneHandling2 != null)))
				{
					previousDateTimeZoneHandling = new DateTimeZoneHandling?(reader.DateTimeZoneHandling);
					reader.DateTimeZoneHandling = this._dateTimeZoneHandling.GetValueOrDefault();
					goto IL_8C;
				}
			}
			previousDateTimeZoneHandling = null;
			IL_8C:
			if (this._dateParseHandling != null)
			{
				DateParseHandling dateParseHandling = reader.DateParseHandling;
				DateParseHandling? dateParseHandling2 = this._dateParseHandling;
				if (!((dateParseHandling == dateParseHandling2.GetValueOrDefault()) & (dateParseHandling2 != null)))
				{
					previousDateParseHandling = new DateParseHandling?(reader.DateParseHandling);
					reader.DateParseHandling = this._dateParseHandling.GetValueOrDefault();
					goto IL_E6;
				}
			}
			previousDateParseHandling = null;
			IL_E6:
			if (this._floatParseHandling != null)
			{
				FloatParseHandling floatParseHandling = reader.FloatParseHandling;
				FloatParseHandling? floatParseHandling2 = this._floatParseHandling;
				if (!((floatParseHandling == floatParseHandling2.GetValueOrDefault()) & (floatParseHandling2 != null)))
				{
					previousFloatParseHandling = new FloatParseHandling?(reader.FloatParseHandling);
					reader.FloatParseHandling = this._floatParseHandling.GetValueOrDefault();
					goto IL_140;
				}
			}
			previousFloatParseHandling = null;
			IL_140:
			if (this._maxDepthSet)
			{
				int? maxDepth = reader.MaxDepth;
				int? maxDepth2 = this._maxDepth;
				if (!((maxDepth.GetValueOrDefault() == maxDepth2.GetValueOrDefault()) & (maxDepth != null == (maxDepth2 != null))))
				{
					previousMaxDepth = reader.MaxDepth;
					reader.MaxDepth = this._maxDepth;
					goto IL_19E;
				}
			}
			previousMaxDepth = null;
			IL_19E:
			if (this._dateFormatStringSet && reader.DateFormatString != this._dateFormatString)
			{
				previousDateFormatString = reader.DateFormatString;
				reader.DateFormatString = this._dateFormatString;
			}
			else
			{
				previousDateFormatString = null;
			}
			JsonTextReader jsonTextReader = reader as JsonTextReader;
			if (jsonTextReader != null && jsonTextReader.PropertyNameTable == null)
			{
				DefaultContractResolver defaultContractResolver = this._contractResolver as DefaultContractResolver;
				if (defaultContractResolver != null)
				{
					jsonTextReader.PropertyNameTable = defaultContractResolver.GetNameTable();
				}
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000058BC File Offset: 0x00003ABC
		[NullableContext(2)]
		private void ResetReader([Nullable(1)] JsonReader reader, CultureInfo previousCulture, DateTimeZoneHandling? previousDateTimeZoneHandling, DateParseHandling? previousDateParseHandling, FloatParseHandling? previousFloatParseHandling, int? previousMaxDepth, string previousDateFormatString)
		{
			if (previousCulture != null)
			{
				reader.Culture = previousCulture;
			}
			if (previousDateTimeZoneHandling != null)
			{
				reader.DateTimeZoneHandling = previousDateTimeZoneHandling.GetValueOrDefault();
			}
			if (previousDateParseHandling != null)
			{
				reader.DateParseHandling = previousDateParseHandling.GetValueOrDefault();
			}
			if (previousFloatParseHandling != null)
			{
				reader.FloatParseHandling = previousFloatParseHandling.GetValueOrDefault();
			}
			if (this._maxDepthSet)
			{
				reader.MaxDepth = previousMaxDepth;
			}
			if (this._dateFormatStringSet)
			{
				reader.DateFormatString = previousDateFormatString;
			}
			JsonTextReader jsonTextReader = reader as JsonTextReader;
			if (jsonTextReader != null && jsonTextReader.PropertyNameTable != null)
			{
				DefaultContractResolver defaultContractResolver = this._contractResolver as DefaultContractResolver;
				if (defaultContractResolver != null && jsonTextReader.PropertyNameTable == defaultContractResolver.GetNameTable())
				{
					jsonTextReader.PropertyNameTable = null;
				}
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000596B File Offset: 0x00003B6B
		public void Serialize(TextWriter textWriter, [Nullable(2)] object value)
		{
			this.Serialize(new JsonTextWriter(textWriter), value);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000597A File Offset: 0x00003B7A
		[NullableContext(2)]
		public void Serialize([Nullable(1)] JsonWriter jsonWriter, object value, Type objectType)
		{
			this.SerializeInternal(jsonWriter, value, objectType);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005985 File Offset: 0x00003B85
		public void Serialize(TextWriter textWriter, [Nullable(2)] object value, Type objectType)
		{
			this.Serialize(new JsonTextWriter(textWriter), value, objectType);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00005995 File Offset: 0x00003B95
		public void Serialize(JsonWriter jsonWriter, [Nullable(2)] object value)
		{
			this.SerializeInternal(jsonWriter, value, null);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000059A0 File Offset: 0x00003BA0
		private TraceJsonReader CreateTraceJsonReader(JsonReader reader)
		{
			TraceJsonReader traceJsonReader = new TraceJsonReader(reader);
			if (reader.TokenType != JsonToken.None)
			{
				traceJsonReader.WriteCurrentToken();
			}
			return traceJsonReader;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000059C4 File Offset: 0x00003BC4
		[NullableContext(2)]
		internal virtual void SerializeInternal([Nullable(1)] JsonWriter jsonWriter, object value, Type objectType)
		{
			ValidationUtils.ArgumentNotNull(jsonWriter, "jsonWriter");
			Formatting? formatting = null;
			if (this._formatting != null)
			{
				Formatting formatting2 = jsonWriter.Formatting;
				Formatting? formatting3 = this._formatting;
				if (!((formatting2 == formatting3.GetValueOrDefault()) & (formatting3 != null)))
				{
					formatting = new Formatting?(jsonWriter.Formatting);
					jsonWriter.Formatting = this._formatting.GetValueOrDefault();
				}
			}
			DateFormatHandling? dateFormatHandling = null;
			if (this._dateFormatHandling != null)
			{
				DateFormatHandling dateFormatHandling2 = jsonWriter.DateFormatHandling;
				DateFormatHandling? dateFormatHandling3 = this._dateFormatHandling;
				if (!((dateFormatHandling2 == dateFormatHandling3.GetValueOrDefault()) & (dateFormatHandling3 != null)))
				{
					dateFormatHandling = new DateFormatHandling?(jsonWriter.DateFormatHandling);
					jsonWriter.DateFormatHandling = this._dateFormatHandling.GetValueOrDefault();
				}
			}
			DateTimeZoneHandling? dateTimeZoneHandling = null;
			if (this._dateTimeZoneHandling != null)
			{
				DateTimeZoneHandling dateTimeZoneHandling2 = jsonWriter.DateTimeZoneHandling;
				DateTimeZoneHandling? dateTimeZoneHandling3 = this._dateTimeZoneHandling;
				if (!((dateTimeZoneHandling2 == dateTimeZoneHandling3.GetValueOrDefault()) & (dateTimeZoneHandling3 != null)))
				{
					dateTimeZoneHandling = new DateTimeZoneHandling?(jsonWriter.DateTimeZoneHandling);
					jsonWriter.DateTimeZoneHandling = this._dateTimeZoneHandling.GetValueOrDefault();
				}
			}
			FloatFormatHandling? floatFormatHandling = null;
			if (this._floatFormatHandling != null)
			{
				FloatFormatHandling floatFormatHandling2 = jsonWriter.FloatFormatHandling;
				FloatFormatHandling? floatFormatHandling3 = this._floatFormatHandling;
				if (!((floatFormatHandling2 == floatFormatHandling3.GetValueOrDefault()) & (floatFormatHandling3 != null)))
				{
					floatFormatHandling = new FloatFormatHandling?(jsonWriter.FloatFormatHandling);
					jsonWriter.FloatFormatHandling = this._floatFormatHandling.GetValueOrDefault();
				}
			}
			StringEscapeHandling? stringEscapeHandling = null;
			if (this._stringEscapeHandling != null)
			{
				StringEscapeHandling stringEscapeHandling2 = jsonWriter.StringEscapeHandling;
				StringEscapeHandling? stringEscapeHandling3 = this._stringEscapeHandling;
				if (!((stringEscapeHandling2 == stringEscapeHandling3.GetValueOrDefault()) & (stringEscapeHandling3 != null)))
				{
					stringEscapeHandling = new StringEscapeHandling?(jsonWriter.StringEscapeHandling);
					jsonWriter.StringEscapeHandling = this._stringEscapeHandling.GetValueOrDefault();
				}
			}
			CultureInfo cultureInfo = null;
			if (this._culture != null && !this._culture.Equals(jsonWriter.Culture))
			{
				cultureInfo = jsonWriter.Culture;
				jsonWriter.Culture = this._culture;
			}
			string text = null;
			if (this._dateFormatStringSet && jsonWriter.DateFormatString != this._dateFormatString)
			{
				text = jsonWriter.DateFormatString;
				jsonWriter.DateFormatString = this._dateFormatString;
			}
			TraceJsonWriter traceJsonWriter = ((this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose) ? new TraceJsonWriter(jsonWriter) : null);
			new JsonSerializerInternalWriter(this).Serialize(traceJsonWriter ?? jsonWriter, value, objectType);
			if (traceJsonWriter != null)
			{
				this.TraceWriter.Trace(TraceLevel.Verbose, traceJsonWriter.GetSerializedJsonMessage(), null);
			}
			if (formatting != null)
			{
				jsonWriter.Formatting = formatting.GetValueOrDefault();
			}
			if (dateFormatHandling != null)
			{
				jsonWriter.DateFormatHandling = dateFormatHandling.GetValueOrDefault();
			}
			if (dateTimeZoneHandling != null)
			{
				jsonWriter.DateTimeZoneHandling = dateTimeZoneHandling.GetValueOrDefault();
			}
			if (floatFormatHandling != null)
			{
				jsonWriter.FloatFormatHandling = floatFormatHandling.GetValueOrDefault();
			}
			if (stringEscapeHandling != null)
			{
				jsonWriter.StringEscapeHandling = stringEscapeHandling.GetValueOrDefault();
			}
			if (this._dateFormatStringSet)
			{
				jsonWriter.DateFormatString = text;
			}
			if (cultureInfo != null)
			{
				jsonWriter.Culture = cultureInfo;
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005CBB File Offset: 0x00003EBB
		internal IReferenceResolver GetReferenceResolver()
		{
			if (this._referenceResolver == null)
			{
				this._referenceResolver = new DefaultReferenceResolver();
			}
			return this._referenceResolver;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00005CD6 File Offset: 0x00003ED6
		[return: Nullable(2)]
		internal JsonConverter GetMatchingConverter(Type type)
		{
			return JsonSerializer.GetMatchingConverter(this._converters, type);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00005CE4 File Offset: 0x00003EE4
		[return: Nullable(2)]
		internal static JsonConverter GetMatchingConverter([Nullable(new byte[] { 2, 1 })] IList<JsonConverter> converters, Type objectType)
		{
			if (converters != null)
			{
				for (int i = 0; i < converters.Count; i++)
				{
					JsonConverter jsonConverter = converters[i];
					if (jsonConverter.CanConvert(objectType))
					{
						return jsonConverter;
					}
				}
			}
			return null;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00005D19 File Offset: 0x00003F19
		internal void OnError(Newtonsoft.Json.Serialization.ErrorEventArgs e)
		{
			EventHandler<Newtonsoft.Json.Serialization.ErrorEventArgs> error = this.Error;
			if (error == null)
			{
				return;
			}
			error(this, e);
		}

		// Token: 0x04000073 RID: 115
		internal TypeNameHandling _typeNameHandling;

		// Token: 0x04000074 RID: 116
		internal TypeNameAssemblyFormatHandling _typeNameAssemblyFormatHandling;

		// Token: 0x04000075 RID: 117
		internal PreserveReferencesHandling _preserveReferencesHandling;

		// Token: 0x04000076 RID: 118
		internal ReferenceLoopHandling _referenceLoopHandling;

		// Token: 0x04000077 RID: 119
		internal MissingMemberHandling _missingMemberHandling;

		// Token: 0x04000078 RID: 120
		internal ObjectCreationHandling _objectCreationHandling;

		// Token: 0x04000079 RID: 121
		internal NullValueHandling _nullValueHandling;

		// Token: 0x0400007A RID: 122
		internal DefaultValueHandling _defaultValueHandling;

		// Token: 0x0400007B RID: 123
		internal ConstructorHandling _constructorHandling;

		// Token: 0x0400007C RID: 124
		internal MetadataPropertyHandling _metadataPropertyHandling;

		// Token: 0x0400007D RID: 125
		[Nullable(2)]
		internal JsonConverterCollection _converters;

		// Token: 0x0400007E RID: 126
		internal IContractResolver _contractResolver;

		// Token: 0x0400007F RID: 127
		[Nullable(2)]
		internal ITraceWriter _traceWriter;

		// Token: 0x04000080 RID: 128
		[Nullable(2)]
		internal IEqualityComparer _equalityComparer;

		// Token: 0x04000081 RID: 129
		internal ISerializationBinder _serializationBinder;

		// Token: 0x04000082 RID: 130
		internal StreamingContext _context;

		// Token: 0x04000083 RID: 131
		[Nullable(2)]
		private IReferenceResolver _referenceResolver;

		// Token: 0x04000084 RID: 132
		private Formatting? _formatting;

		// Token: 0x04000085 RID: 133
		private DateFormatHandling? _dateFormatHandling;

		// Token: 0x04000086 RID: 134
		private DateTimeZoneHandling? _dateTimeZoneHandling;

		// Token: 0x04000087 RID: 135
		private DateParseHandling? _dateParseHandling;

		// Token: 0x04000088 RID: 136
		private FloatFormatHandling? _floatFormatHandling;

		// Token: 0x04000089 RID: 137
		private FloatParseHandling? _floatParseHandling;

		// Token: 0x0400008A RID: 138
		private StringEscapeHandling? _stringEscapeHandling;

		// Token: 0x0400008B RID: 139
		private CultureInfo _culture;

		// Token: 0x0400008C RID: 140
		private int? _maxDepth;

		// Token: 0x0400008D RID: 141
		private bool _maxDepthSet;

		// Token: 0x0400008E RID: 142
		private bool? _checkAdditionalContent;

		// Token: 0x0400008F RID: 143
		[Nullable(2)]
		private string _dateFormatString;

		// Token: 0x04000090 RID: 144
		private bool _dateFormatStringSet;
	}
}
