using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005DB RID: 1499
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	internal struct CustomAttributeType
	{
		// Token: 0x06004587 RID: 17799 RVA: 0x00100D11 File Offset: 0x000FEF11
		public CustomAttributeType(CustomAttributeEncoding encodedType, CustomAttributeEncoding encodedArrayType, CustomAttributeEncoding encodedEnumType, string enumName)
		{
			this.m_encodedType = encodedType;
			this.m_encodedArrayType = encodedArrayType;
			this.m_encodedEnumType = encodedEnumType;
			this.m_enumName = enumName;
			this.m_padding = this.m_encodedType;
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06004588 RID: 17800 RVA: 0x00100D3C File Offset: 0x000FEF3C
		public CustomAttributeEncoding EncodedType
		{
			get
			{
				return this.m_encodedType;
			}
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06004589 RID: 17801 RVA: 0x00100D44 File Offset: 0x000FEF44
		public CustomAttributeEncoding EncodedEnumType
		{
			get
			{
				return this.m_encodedEnumType;
			}
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x0600458A RID: 17802 RVA: 0x00100D4C File Offset: 0x000FEF4C
		public CustomAttributeEncoding EncodedArrayType
		{
			get
			{
				return this.m_encodedArrayType;
			}
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x0600458B RID: 17803 RVA: 0x00100D54 File Offset: 0x000FEF54
		[ComVisible(true)]
		public string EnumName
		{
			get
			{
				return this.m_enumName;
			}
		}

		// Token: 0x04001C92 RID: 7314
		private string m_enumName;

		// Token: 0x04001C93 RID: 7315
		private CustomAttributeEncoding m_encodedType;

		// Token: 0x04001C94 RID: 7316
		private CustomAttributeEncoding m_encodedEnumType;

		// Token: 0x04001C95 RID: 7317
		private CustomAttributeEncoding m_encodedArrayType;

		// Token: 0x04001C96 RID: 7318
		private CustomAttributeEncoding m_padding;
	}
}
