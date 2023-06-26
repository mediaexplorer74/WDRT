using System;

namespace System.Reflection.Emit
{
	// Token: 0x0200062B RID: 1579
	internal class NativeVersionInfo
	{
		// Token: 0x060049A4 RID: 18852 RVA: 0x0010C070 File Offset: 0x0010A270
		internal NativeVersionInfo()
		{
			this.m_strDescription = null;
			this.m_strCompany = null;
			this.m_strTitle = null;
			this.m_strCopyright = null;
			this.m_strTrademark = null;
			this.m_strProduct = null;
			this.m_strProductVersion = null;
			this.m_strFileVersion = null;
			this.m_lcid = -1;
		}

		// Token: 0x04001E7B RID: 7803
		internal string m_strDescription;

		// Token: 0x04001E7C RID: 7804
		internal string m_strCompany;

		// Token: 0x04001E7D RID: 7805
		internal string m_strTitle;

		// Token: 0x04001E7E RID: 7806
		internal string m_strCopyright;

		// Token: 0x04001E7F RID: 7807
		internal string m_strTrademark;

		// Token: 0x04001E80 RID: 7808
		internal string m_strProduct;

		// Token: 0x04001E81 RID: 7809
		internal string m_strProductVersion;

		// Token: 0x04001E82 RID: 7810
		internal string m_strFileVersion;

		// Token: 0x04001E83 RID: 7811
		internal int m_lcid;
	}
}
