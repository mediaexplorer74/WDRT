using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000092 RID: 146
	[NullableContext(2)]
	[Nullable(0)]
	public class JsonProperty
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0001D318 File Offset: 0x0001B518
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x0001D320 File Offset: 0x0001B520
		internal JsonContract PropertyContract { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0001D329 File Offset: 0x0001B529
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x0001D331 File Offset: 0x0001B531
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
			set
			{
				this._propertyName = value;
				this._skipPropertyNameEscape = !JavaScriptUtils.ShouldEscapeJavaScriptString(this._propertyName, JavaScriptUtils.HtmlCharEscapeFlags);
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0001D353 File Offset: 0x0001B553
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x0001D35B File Offset: 0x0001B55B
		public Type DeclaringType { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0001D364 File Offset: 0x0001B564
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x0001D36C File Offset: 0x0001B56C
		public int? Order { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x0001D375 File Offset: 0x0001B575
		// (set) Token: 0x06000720 RID: 1824 RVA: 0x0001D37D File Offset: 0x0001B57D
		public string UnderlyingName { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x0001D386 File Offset: 0x0001B586
		// (set) Token: 0x06000722 RID: 1826 RVA: 0x0001D38E File Offset: 0x0001B58E
		public IValueProvider ValueProvider { get; set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x0001D397 File Offset: 0x0001B597
		// (set) Token: 0x06000724 RID: 1828 RVA: 0x0001D39F File Offset: 0x0001B59F
		public IAttributeProvider AttributeProvider { get; set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x0001D3A8 File Offset: 0x0001B5A8
		// (set) Token: 0x06000726 RID: 1830 RVA: 0x0001D3B0 File Offset: 0x0001B5B0
		public Type PropertyType
		{
			get
			{
				return this._propertyType;
			}
			set
			{
				if (this._propertyType != value)
				{
					this._propertyType = value;
					this._hasGeneratedDefaultValue = false;
				}
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x0001D3CE File Offset: 0x0001B5CE
		// (set) Token: 0x06000728 RID: 1832 RVA: 0x0001D3D6 File Offset: 0x0001B5D6
		public JsonConverter Converter { get; set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x0001D3DF File Offset: 0x0001B5DF
		// (set) Token: 0x0600072A RID: 1834 RVA: 0x0001D3E7 File Offset: 0x0001B5E7
		[Obsolete("MemberConverter is obsolete. Use Converter instead.")]
		public JsonConverter MemberConverter
		{
			get
			{
				return this.Converter;
			}
			set
			{
				this.Converter = value;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x0001D3F0 File Offset: 0x0001B5F0
		// (set) Token: 0x0600072C RID: 1836 RVA: 0x0001D3F8 File Offset: 0x0001B5F8
		public bool Ignored { get; set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x0001D401 File Offset: 0x0001B601
		// (set) Token: 0x0600072E RID: 1838 RVA: 0x0001D409 File Offset: 0x0001B609
		public bool Readable { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x0001D412 File Offset: 0x0001B612
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x0001D41A File Offset: 0x0001B61A
		public bool Writable { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x0001D423 File Offset: 0x0001B623
		// (set) Token: 0x06000732 RID: 1842 RVA: 0x0001D42B File Offset: 0x0001B62B
		public bool HasMemberAttribute { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x0001D434 File Offset: 0x0001B634
		// (set) Token: 0x06000734 RID: 1844 RVA: 0x0001D446 File Offset: 0x0001B646
		public object DefaultValue
		{
			get
			{
				if (!this._hasExplicitDefaultValue)
				{
					return null;
				}
				return this._defaultValue;
			}
			set
			{
				this._hasExplicitDefaultValue = true;
				this._defaultValue = value;
			}
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0001D456 File Offset: 0x0001B656
		internal object GetResolvedDefaultValue()
		{
			if (this._propertyType == null)
			{
				return null;
			}
			if (!this._hasExplicitDefaultValue && !this._hasGeneratedDefaultValue)
			{
				this._defaultValue = ReflectionUtils.GetDefaultValue(this._propertyType);
				this._hasGeneratedDefaultValue = true;
			}
			return this._defaultValue;
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0001D496 File Offset: 0x0001B696
		// (set) Token: 0x06000737 RID: 1847 RVA: 0x0001D4A3 File Offset: 0x0001B6A3
		public Required Required
		{
			get
			{
				return this._required.GetValueOrDefault();
			}
			set
			{
				this._required = new Required?(value);
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x0001D4B1 File Offset: 0x0001B6B1
		public bool IsRequiredSpecified
		{
			get
			{
				return this._required != null;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x0001D4BE File Offset: 0x0001B6BE
		// (set) Token: 0x0600073A RID: 1850 RVA: 0x0001D4C6 File Offset: 0x0001B6C6
		public bool? IsReference { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x0001D4CF File Offset: 0x0001B6CF
		// (set) Token: 0x0600073C RID: 1852 RVA: 0x0001D4D7 File Offset: 0x0001B6D7
		public NullValueHandling? NullValueHandling { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x0001D4E0 File Offset: 0x0001B6E0
		// (set) Token: 0x0600073E RID: 1854 RVA: 0x0001D4E8 File Offset: 0x0001B6E8
		public DefaultValueHandling? DefaultValueHandling { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x0001D4F1 File Offset: 0x0001B6F1
		// (set) Token: 0x06000740 RID: 1856 RVA: 0x0001D4F9 File Offset: 0x0001B6F9
		public ReferenceLoopHandling? ReferenceLoopHandling { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x0001D502 File Offset: 0x0001B702
		// (set) Token: 0x06000742 RID: 1858 RVA: 0x0001D50A File Offset: 0x0001B70A
		public ObjectCreationHandling? ObjectCreationHandling { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x0001D513 File Offset: 0x0001B713
		// (set) Token: 0x06000744 RID: 1860 RVA: 0x0001D51B File Offset: 0x0001B71B
		public TypeNameHandling? TypeNameHandling { get; set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x0001D524 File Offset: 0x0001B724
		// (set) Token: 0x06000746 RID: 1862 RVA: 0x0001D52C File Offset: 0x0001B72C
		[Nullable(new byte[] { 2, 1 })]
		public Predicate<object> ShouldSerialize
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x0001D535 File Offset: 0x0001B735
		// (set) Token: 0x06000748 RID: 1864 RVA: 0x0001D53D File Offset: 0x0001B73D
		[Nullable(new byte[] { 2, 1 })]
		public Predicate<object> ShouldDeserialize
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x0001D546 File Offset: 0x0001B746
		// (set) Token: 0x0600074A RID: 1866 RVA: 0x0001D54E File Offset: 0x0001B74E
		[Nullable(new byte[] { 2, 1 })]
		public Predicate<object> GetIsSpecified
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x0001D557 File Offset: 0x0001B757
		// (set) Token: 0x0600074C RID: 1868 RVA: 0x0001D55F File Offset: 0x0001B75F
		[Nullable(new byte[] { 2, 1, 2 })]
		public Action<object, object> SetIsSpecified
		{
			[return: Nullable(new byte[] { 2, 1, 2 })]
			get;
			[param: Nullable(new byte[] { 2, 1, 2 })]
			set;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0001D568 File Offset: 0x0001B768
		[NullableContext(1)]
		public override string ToString()
		{
			return this.PropertyName ?? string.Empty;
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x0001D579 File Offset: 0x0001B779
		// (set) Token: 0x0600074F RID: 1871 RVA: 0x0001D581 File Offset: 0x0001B781
		public JsonConverter ItemConverter { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x0001D58A File Offset: 0x0001B78A
		// (set) Token: 0x06000751 RID: 1873 RVA: 0x0001D592 File Offset: 0x0001B792
		public bool? ItemIsReference { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x0001D59B File Offset: 0x0001B79B
		// (set) Token: 0x06000753 RID: 1875 RVA: 0x0001D5A3 File Offset: 0x0001B7A3
		public TypeNameHandling? ItemTypeNameHandling { get; set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x0001D5AC File Offset: 0x0001B7AC
		// (set) Token: 0x06000755 RID: 1877 RVA: 0x0001D5B4 File Offset: 0x0001B7B4
		public ReferenceLoopHandling? ItemReferenceLoopHandling { get; set; }

		// Token: 0x06000756 RID: 1878 RVA: 0x0001D5C0 File Offset: 0x0001B7C0
		[NullableContext(1)]
		internal void WritePropertyName(JsonWriter writer)
		{
			string propertyName = this.PropertyName;
			if (this._skipPropertyNameEscape)
			{
				writer.WritePropertyName(propertyName, false);
				return;
			}
			writer.WritePropertyName(propertyName);
		}

		// Token: 0x04000288 RID: 648
		internal Required? _required;

		// Token: 0x04000289 RID: 649
		internal bool _hasExplicitDefaultValue;

		// Token: 0x0400028A RID: 650
		private object _defaultValue;

		// Token: 0x0400028B RID: 651
		private bool _hasGeneratedDefaultValue;

		// Token: 0x0400028C RID: 652
		private string _propertyName;

		// Token: 0x0400028D RID: 653
		internal bool _skipPropertyNameEscape;

		// Token: 0x0400028E RID: 654
		private Type _propertyType;
	}
}
