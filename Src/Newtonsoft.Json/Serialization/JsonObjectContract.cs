using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000090 RID: 144
	[NullableContext(2)]
	[Nullable(0)]
	public class JsonObjectContract : JsonContainerContract
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x0001CF7A File Offset: 0x0001B17A
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x0001CF82 File Offset: 0x0001B182
		public MemberSerialization MemberSerialization { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x0001CF8B File Offset: 0x0001B18B
		// (set) Token: 0x060006FD RID: 1789 RVA: 0x0001CF93 File Offset: 0x0001B193
		public MissingMemberHandling? MissingMemberHandling { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x0001CF9C File Offset: 0x0001B19C
		// (set) Token: 0x060006FF RID: 1791 RVA: 0x0001CFA4 File Offset: 0x0001B1A4
		public Required? ItemRequired { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x0001CFAD File Offset: 0x0001B1AD
		// (set) Token: 0x06000701 RID: 1793 RVA: 0x0001CFB5 File Offset: 0x0001B1B5
		public NullValueHandling? ItemNullValueHandling { get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0001CFBE File Offset: 0x0001B1BE
		[Nullable(1)]
		public JsonPropertyCollection Properties
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x0001CFC6 File Offset: 0x0001B1C6
		[Nullable(1)]
		public JsonPropertyCollection CreatorParameters
		{
			[NullableContext(1)]
			get
			{
				if (this._creatorParameters == null)
				{
					this._creatorParameters = new JsonPropertyCollection(base.UnderlyingType);
				}
				return this._creatorParameters;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x0001CFE7 File Offset: 0x0001B1E7
		// (set) Token: 0x06000705 RID: 1797 RVA: 0x0001CFEF File Offset: 0x0001B1EF
		[Nullable(new byte[] { 2, 1 })]
		public ObjectConstructor<object> OverrideCreator
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return this._overrideCreator;
			}
			[param: Nullable(new byte[] { 2, 1 })]
			set
			{
				this._overrideCreator = value;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x0001CFF8 File Offset: 0x0001B1F8
		// (set) Token: 0x06000707 RID: 1799 RVA: 0x0001D000 File Offset: 0x0001B200
		[Nullable(new byte[] { 2, 1 })]
		internal ObjectConstructor<object> ParameterizedCreator
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return this._parameterizedCreator;
			}
			[param: Nullable(new byte[] { 2, 1 })]
			set
			{
				this._parameterizedCreator = value;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x0001D009 File Offset: 0x0001B209
		// (set) Token: 0x06000709 RID: 1801 RVA: 0x0001D011 File Offset: 0x0001B211
		public ExtensionDataSetter ExtensionDataSetter { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x0001D01A File Offset: 0x0001B21A
		// (set) Token: 0x0600070B RID: 1803 RVA: 0x0001D022 File Offset: 0x0001B222
		public ExtensionDataGetter ExtensionDataGetter { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x0001D02B File Offset: 0x0001B22B
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x0001D033 File Offset: 0x0001B233
		public Type ExtensionDataValueType
		{
			get
			{
				return this._extensionDataValueType;
			}
			set
			{
				this._extensionDataValueType = value;
				this.ExtensionDataIsJToken = value != null && typeof(JToken).IsAssignableFrom(value);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x0001D05E File Offset: 0x0001B25E
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x0001D066 File Offset: 0x0001B266
		[Nullable(new byte[] { 2, 1, 1 })]
		public Func<string, string> ExtensionDataNameResolver
		{
			[return: Nullable(new byte[] { 2, 1, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1, 1 })]
			set;
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x0001D070 File Offset: 0x0001B270
		internal bool HasRequiredOrDefaultValueProperties
		{
			get
			{
				if (this._hasRequiredOrDefaultValueProperties == null)
				{
					this._hasRequiredOrDefaultValueProperties = new bool?(false);
					if (this.ItemRequired.GetValueOrDefault(Required.Default) != Required.Default)
					{
						this._hasRequiredOrDefaultValueProperties = new bool?(true);
					}
					else
					{
						foreach (JsonProperty jsonProperty in this.Properties)
						{
							if (jsonProperty.Required == Required.Default)
							{
								DefaultValueHandling? defaultValueHandling = jsonProperty.DefaultValueHandling & DefaultValueHandling.Populate;
								DefaultValueHandling defaultValueHandling2 = DefaultValueHandling.Populate;
								if (!((defaultValueHandling.GetValueOrDefault() == defaultValueHandling2) & (defaultValueHandling != null)))
								{
									continue;
								}
							}
							this._hasRequiredOrDefaultValueProperties = new bool?(true);
							break;
						}
					}
				}
				return this._hasRequiredOrDefaultValueProperties.GetValueOrDefault();
			}
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0001D15C File Offset: 0x0001B35C
		[NullableContext(1)]
		public JsonObjectContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.Object;
			this.Properties = new JsonPropertyCollection(base.UnderlyingType);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0001D17D File Offset: 0x0001B37D
		[NullableContext(1)]
		[SecuritySafeCritical]
		internal object GetUninitializedObject()
		{
			if (!JsonTypeReflector.FullyTrusted)
			{
				throw new JsonException("Insufficient permissions. Creating an uninitialized '{0}' type requires full trust.".FormatWith(CultureInfo.InvariantCulture, this.NonNullableUnderlyingType));
			}
			return FormatterServices.GetUninitializedObject(this.NonNullableUnderlyingType);
		}

		// Token: 0x04000280 RID: 640
		internal bool ExtensionDataIsJToken;

		// Token: 0x04000281 RID: 641
		private bool? _hasRequiredOrDefaultValueProperties;

		// Token: 0x04000282 RID: 642
		[Nullable(new byte[] { 2, 1 })]
		private ObjectConstructor<object> _overrideCreator;

		// Token: 0x04000283 RID: 643
		[Nullable(new byte[] { 2, 1 })]
		private ObjectConstructor<object> _parameterizedCreator;

		// Token: 0x04000284 RID: 644
		private JsonPropertyCollection _creatorParameters;

		// Token: 0x04000285 RID: 645
		private Type _extensionDataValueType;
	}
}
