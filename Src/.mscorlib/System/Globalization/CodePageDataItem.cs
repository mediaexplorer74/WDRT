using System;
using System.Security;

namespace System.Globalization
{
	// Token: 0x020003B5 RID: 949
	[Serializable]
	internal class CodePageDataItem
	{
		// Token: 0x06002F61 RID: 12129 RVA: 0x000B7158 File Offset: 0x000B5358
		[SecurityCritical]
		internal unsafe CodePageDataItem(int dataIndex)
		{
			this.m_dataIndex = dataIndex;
			this.m_uiFamilyCodePage = (int)EncodingTable.codePageDataPtr[dataIndex].uiFamilyCodePage;
			this.m_flags = EncodingTable.codePageDataPtr[dataIndex].flags;
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x000B71A8 File Offset: 0x000B53A8
		[SecurityCritical]
		internal unsafe static string CreateString(sbyte* pStrings, uint index)
		{
			if (*pStrings == 124)
			{
				int num = 1;
				int num2 = 1;
				for (;;)
				{
					sbyte b = pStrings[num2];
					if (b == 124 || b == 0)
					{
						if (index == 0U)
						{
							break;
						}
						index -= 1U;
						num = num2 + 1;
						if (b == 0)
						{
							goto IL_37;
						}
					}
					num2++;
				}
				return new string(pStrings, num, num2 - num);
				IL_37:
				throw new ArgumentException("pStrings");
			}
			return new string(pStrings);
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06002F63 RID: 12131 RVA: 0x000B71FD File Offset: 0x000B53FD
		public unsafe string WebName
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_webName == null)
				{
					this.m_webName = CodePageDataItem.CreateString(EncodingTable.codePageDataPtr[this.m_dataIndex].Names, 0U);
				}
				return this.m_webName;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06002F64 RID: 12132 RVA: 0x000B7232 File Offset: 0x000B5432
		public virtual int UIFamilyCodePage
		{
			get
			{
				return this.m_uiFamilyCodePage;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06002F65 RID: 12133 RVA: 0x000B723A File Offset: 0x000B543A
		public unsafe string HeaderName
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_headerName == null)
				{
					this.m_headerName = CodePageDataItem.CreateString(EncodingTable.codePageDataPtr[this.m_dataIndex].Names, 1U);
				}
				return this.m_headerName;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06002F66 RID: 12134 RVA: 0x000B726F File Offset: 0x000B546F
		public unsafe string BodyName
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_bodyName == null)
				{
					this.m_bodyName = CodePageDataItem.CreateString(EncodingTable.codePageDataPtr[this.m_dataIndex].Names, 2U);
				}
				return this.m_bodyName;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06002F67 RID: 12135 RVA: 0x000B72A4 File Offset: 0x000B54A4
		public uint Flags
		{
			get
			{
				return this.m_flags;
			}
		}

		// Token: 0x04001417 RID: 5143
		internal int m_dataIndex;

		// Token: 0x04001418 RID: 5144
		internal int m_uiFamilyCodePage;

		// Token: 0x04001419 RID: 5145
		internal string m_webName;

		// Token: 0x0400141A RID: 5146
		internal string m_headerName;

		// Token: 0x0400141B RID: 5147
		internal string m_bodyName;

		// Token: 0x0400141C RID: 5148
		internal uint m_flags;
	}
}
