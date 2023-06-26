using System;
using System.Reflection;

namespace Microsoft.CSharp
{
	// Token: 0x0200000E RID: 14
	internal class CSharpTypeAttributeConverter : CSharpModifierAttributeConverter
	{
		// Token: 0x06000140 RID: 320 RVA: 0x0000D11D File Offset: 0x0000B31D
		private CSharpTypeAttributeConverter()
		{
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000D125 File Offset: 0x0000B325
		public static CSharpTypeAttributeConverter Default
		{
			get
			{
				if (CSharpTypeAttributeConverter.defaultConverter == null)
				{
					CSharpTypeAttributeConverter.defaultConverter = new CSharpTypeAttributeConverter();
				}
				return CSharpTypeAttributeConverter.defaultConverter;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000142 RID: 322 RVA: 0x0000D143 File Offset: 0x0000B343
		protected override string[] Names
		{
			get
			{
				if (CSharpTypeAttributeConverter.names == null)
				{
					CSharpTypeAttributeConverter.names = new string[] { "Public", "Internal" };
				}
				return CSharpTypeAttributeConverter.names;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000143 RID: 323 RVA: 0x0000D172 File Offset: 0x0000B372
		protected override object[] Values
		{
			get
			{
				if (CSharpTypeAttributeConverter.values == null)
				{
					CSharpTypeAttributeConverter.values = new object[]
					{
						TypeAttributes.Public,
						TypeAttributes.NotPublic
					};
				}
				return CSharpTypeAttributeConverter.values;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000D1A3 File Offset: 0x0000B3A3
		protected override object DefaultValue
		{
			get
			{
				return TypeAttributes.NotPublic;
			}
		}

		// Token: 0x04000074 RID: 116
		private static volatile string[] names;

		// Token: 0x04000075 RID: 117
		private static volatile object[] values;

		// Token: 0x04000076 RID: 118
		private static volatile CSharpTypeAttributeConverter defaultConverter;
	}
}
