using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x02000026 RID: 38
	[NullableContext(2)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public sealed class JsonPropertyAttribute : Attribute
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003356 File Offset: 0x00001556
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x0000335E File Offset: 0x0000155E
		public Type ItemConverterType { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003367 File Offset: 0x00001567
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x0000336F File Offset: 0x0000156F
		[Nullable(new byte[] { 2, 1 })]
		public object[] ItemConverterParameters
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003378 File Offset: 0x00001578
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00003380 File Offset: 0x00001580
		public Type NamingStrategyType { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003389 File Offset: 0x00001589
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00003391 File Offset: 0x00001591
		[Nullable(new byte[] { 2, 1 })]
		public object[] NamingStrategyParameters
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000339A File Offset: 0x0000159A
		// (set) Token: 0x060000BC RID: 188 RVA: 0x000033A7 File Offset: 0x000015A7
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

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000033B5 File Offset: 0x000015B5
		// (set) Token: 0x060000BE RID: 190 RVA: 0x000033C2 File Offset: 0x000015C2
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

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000033D0 File Offset: 0x000015D0
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x000033DD File Offset: 0x000015DD
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

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000033EB File Offset: 0x000015EB
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x000033F8 File Offset: 0x000015F8
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

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00003406 File Offset: 0x00001606
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00003413 File Offset: 0x00001613
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

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00003421 File Offset: 0x00001621
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x0000342E File Offset: 0x0000162E
		public bool IsReference
		{
			get
			{
				return this._isReference.GetValueOrDefault();
			}
			set
			{
				this._isReference = new bool?(value);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000343C File Offset: 0x0000163C
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00003449 File Offset: 0x00001649
		public int Order
		{
			get
			{
				return this._order.GetValueOrDefault();
			}
			set
			{
				this._order = new int?(value);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003457 File Offset: 0x00001657
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00003464 File Offset: 0x00001664
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

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00003472 File Offset: 0x00001672
		// (set) Token: 0x060000CC RID: 204 RVA: 0x0000347A File Offset: 0x0000167A
		public string PropertyName { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00003483 File Offset: 0x00001683
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00003490 File Offset: 0x00001690
		public ReferenceLoopHandling ItemReferenceLoopHandling
		{
			get
			{
				return this._itemReferenceLoopHandling.GetValueOrDefault();
			}
			set
			{
				this._itemReferenceLoopHandling = new ReferenceLoopHandling?(value);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000CF RID: 207 RVA: 0x0000349E File Offset: 0x0000169E
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x000034AB File Offset: 0x000016AB
		public TypeNameHandling ItemTypeNameHandling
		{
			get
			{
				return this._itemTypeNameHandling.GetValueOrDefault();
			}
			set
			{
				this._itemTypeNameHandling = new TypeNameHandling?(value);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x000034B9 File Offset: 0x000016B9
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x000034C6 File Offset: 0x000016C6
		public bool ItemIsReference
		{
			get
			{
				return this._itemIsReference.GetValueOrDefault();
			}
			set
			{
				this._itemIsReference = new bool?(value);
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000034D4 File Offset: 0x000016D4
		public JsonPropertyAttribute()
		{
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000034DC File Offset: 0x000016DC
		[NullableContext(1)]
		public JsonPropertyAttribute(string propertyName)
		{
			this.PropertyName = propertyName;
		}

		// Token: 0x0400004E RID: 78
		internal NullValueHandling? _nullValueHandling;

		// Token: 0x0400004F RID: 79
		internal DefaultValueHandling? _defaultValueHandling;

		// Token: 0x04000050 RID: 80
		internal ReferenceLoopHandling? _referenceLoopHandling;

		// Token: 0x04000051 RID: 81
		internal ObjectCreationHandling? _objectCreationHandling;

		// Token: 0x04000052 RID: 82
		internal TypeNameHandling? _typeNameHandling;

		// Token: 0x04000053 RID: 83
		internal bool? _isReference;

		// Token: 0x04000054 RID: 84
		internal int? _order;

		// Token: 0x04000055 RID: 85
		internal Required? _required;

		// Token: 0x04000056 RID: 86
		internal bool? _itemIsReference;

		// Token: 0x04000057 RID: 87
		internal ReferenceLoopHandling? _itemReferenceLoopHandling;

		// Token: 0x04000058 RID: 88
		internal TypeNameHandling? _itemTypeNameHandling;
	}
}
