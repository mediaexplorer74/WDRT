using System;
using System.CodeDom;

namespace Microsoft.VisualBasic
{
	// Token: 0x02000009 RID: 9
	internal class VBMemberAttributeConverter : VBModifierAttributeConverter
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00006B06 File Offset: 0x00004D06
		private VBMemberAttributeConverter()
		{
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00006B0E File Offset: 0x00004D0E
		public static VBMemberAttributeConverter Default
		{
			get
			{
				if (VBMemberAttributeConverter.defaultConverter == null)
				{
					VBMemberAttributeConverter.defaultConverter = new VBMemberAttributeConverter();
				}
				return VBMemberAttributeConverter.defaultConverter;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00006B2C File Offset: 0x00004D2C
		protected override string[] Names
		{
			get
			{
				if (VBMemberAttributeConverter.names == null)
				{
					VBMemberAttributeConverter.names = new string[] { "Public", "Protected", "Protected Friend", "Friend", "Private" };
				}
				return VBMemberAttributeConverter.names;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00006B80 File Offset: 0x00004D80
		protected override object[] Values
		{
			get
			{
				if (VBMemberAttributeConverter.values == null)
				{
					VBMemberAttributeConverter.values = new object[]
					{
						MemberAttributes.Public,
						MemberAttributes.Family,
						MemberAttributes.FamilyOrAssembly,
						MemberAttributes.Assembly,
						MemberAttributes.Private
					};
				}
				return VBMemberAttributeConverter.values;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00006BEB File Offset: 0x00004DEB
		protected override object DefaultValue
		{
			get
			{
				return MemberAttributes.Private;
			}
		}

		// Token: 0x04000060 RID: 96
		private static volatile string[] names;

		// Token: 0x04000061 RID: 97
		private static volatile object[] values;

		// Token: 0x04000062 RID: 98
		private static volatile VBMemberAttributeConverter defaultConverter;
	}
}
