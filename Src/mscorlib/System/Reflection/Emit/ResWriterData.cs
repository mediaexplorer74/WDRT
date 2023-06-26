using System;
using System.IO;
using System.Resources;

namespace System.Reflection.Emit
{
	// Token: 0x0200062A RID: 1578
	internal class ResWriterData
	{
		// Token: 0x060049A3 RID: 18851 RVA: 0x0010C031 File Offset: 0x0010A231
		internal ResWriterData(ResourceWriter resWriter, Stream memoryStream, string strName, string strFileName, string strFullFileName, ResourceAttributes attribute)
		{
			this.m_resWriter = resWriter;
			this.m_memoryStream = memoryStream;
			this.m_strName = strName;
			this.m_strFileName = strFileName;
			this.m_strFullFileName = strFullFileName;
			this.m_nextResWriter = null;
			this.m_attribute = attribute;
		}

		// Token: 0x04001E74 RID: 7796
		internal ResourceWriter m_resWriter;

		// Token: 0x04001E75 RID: 7797
		internal string m_strName;

		// Token: 0x04001E76 RID: 7798
		internal string m_strFileName;

		// Token: 0x04001E77 RID: 7799
		internal string m_strFullFileName;

		// Token: 0x04001E78 RID: 7800
		internal Stream m_memoryStream;

		// Token: 0x04001E79 RID: 7801
		internal ResWriterData m_nextResWriter;

		// Token: 0x04001E7A RID: 7802
		internal ResourceAttributes m_attribute;
	}
}
