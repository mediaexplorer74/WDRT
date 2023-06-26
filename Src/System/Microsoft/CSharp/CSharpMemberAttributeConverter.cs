using System;
using System.CodeDom;

namespace Microsoft.CSharp
{
	// Token: 0x0200000F RID: 15
	internal class CSharpMemberAttributeConverter : CSharpModifierAttributeConverter
	{
		// Token: 0x06000145 RID: 325 RVA: 0x0000D1AB File Offset: 0x0000B3AB
		private CSharpMemberAttributeConverter()
		{
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000D1B3 File Offset: 0x0000B3B3
		public static CSharpMemberAttributeConverter Default
		{
			get
			{
				if (CSharpMemberAttributeConverter.defaultConverter == null)
				{
					CSharpMemberAttributeConverter.defaultConverter = new CSharpMemberAttributeConverter();
				}
				return CSharpMemberAttributeConverter.defaultConverter;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000147 RID: 327 RVA: 0x0000D1D4 File Offset: 0x0000B3D4
		protected override string[] Names
		{
			get
			{
				if (CSharpMemberAttributeConverter.names == null)
				{
					CSharpMemberAttributeConverter.names = new string[] { "Public", "Protected", "Protected Internal", "Internal", "Private" };
				}
				return CSharpMemberAttributeConverter.names;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000D228 File Offset: 0x0000B428
		protected override object[] Values
		{
			get
			{
				if (CSharpMemberAttributeConverter.values == null)
				{
					CSharpMemberAttributeConverter.values = new object[]
					{
						MemberAttributes.Public,
						MemberAttributes.Family,
						MemberAttributes.FamilyOrAssembly,
						MemberAttributes.Assembly,
						MemberAttributes.Private
					};
				}
				return CSharpMemberAttributeConverter.values;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0000D293 File Offset: 0x0000B493
		protected override object DefaultValue
		{
			get
			{
				return MemberAttributes.Private;
			}
		}

		// Token: 0x04000077 RID: 119
		private static volatile string[] names;

		// Token: 0x04000078 RID: 120
		private static volatile object[] values;

		// Token: 0x04000079 RID: 121
		private static volatile CSharpMemberAttributeConverter defaultConverter;
	}
}
