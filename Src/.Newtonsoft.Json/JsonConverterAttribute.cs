using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x0200001C RID: 28
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Parameter, AllowMultiple = false)]
	public sealed class JsonConverterAttribute : Attribute
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00002F45 File Offset: 0x00001145
		public Type ConverterType
		{
			get
			{
				return this._converterType;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00002F4D File Offset: 0x0000114D
		[Nullable(new byte[] { 2, 1 })]
		public object[] ConverterParameters
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002F55 File Offset: 0x00001155
		public JsonConverterAttribute(Type converterType)
		{
			if (converterType == null)
			{
				throw new ArgumentNullException("converterType");
			}
			this._converterType = converterType;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002F78 File Offset: 0x00001178
		public JsonConverterAttribute(Type converterType, params object[] converterParameters)
			: this(converterType)
		{
			this.ConverterParameters = converterParameters;
		}

		// Token: 0x0400003C RID: 60
		private readonly Type _converterType;
	}
}
