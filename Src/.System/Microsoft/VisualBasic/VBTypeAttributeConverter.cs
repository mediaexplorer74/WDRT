using System;
using System.Reflection;

namespace Microsoft.VisualBasic
{
	// Token: 0x0200000A RID: 10
	internal class VBTypeAttributeConverter : VBModifierAttributeConverter
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00006BF7 File Offset: 0x00004DF7
		private VBTypeAttributeConverter()
		{
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00006BFF File Offset: 0x00004DFF
		public static VBTypeAttributeConverter Default
		{
			get
			{
				if (VBTypeAttributeConverter.defaultConverter == null)
				{
					VBTypeAttributeConverter.defaultConverter = new VBTypeAttributeConverter();
				}
				return VBTypeAttributeConverter.defaultConverter;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00006C1D File Offset: 0x00004E1D
		protected override string[] Names
		{
			get
			{
				if (VBTypeAttributeConverter.names == null)
				{
					VBTypeAttributeConverter.names = new string[] { "Public", "Friend" };
				}
				return VBTypeAttributeConverter.names;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00006C4C File Offset: 0x00004E4C
		protected override object[] Values
		{
			get
			{
				if (VBTypeAttributeConverter.values == null)
				{
					VBTypeAttributeConverter.values = new object[]
					{
						TypeAttributes.Public,
						TypeAttributes.NotPublic
					};
				}
				return VBTypeAttributeConverter.values;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00006C7D File Offset: 0x00004E7D
		protected override object DefaultValue
		{
			get
			{
				return TypeAttributes.Public;
			}
		}

		// Token: 0x04000063 RID: 99
		private static volatile VBTypeAttributeConverter defaultConverter;

		// Token: 0x04000064 RID: 100
		private static volatile string[] names;

		// Token: 0x04000065 RID: 101
		private static volatile object[] values;
	}
}
