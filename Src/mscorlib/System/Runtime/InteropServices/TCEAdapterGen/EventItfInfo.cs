using System;
using System.Reflection;

namespace System.Runtime.InteropServices.TCEAdapterGen
{
	// Token: 0x020009C1 RID: 2497
	internal class EventItfInfo
	{
		// Token: 0x060063C9 RID: 25545 RVA: 0x00155C73 File Offset: 0x00153E73
		public EventItfInfo(string strEventItfName, string strSrcItfName, string strEventProviderName, RuntimeAssembly asmImport, RuntimeAssembly asmSrcItf)
		{
			this.m_strEventItfName = strEventItfName;
			this.m_strSrcItfName = strSrcItfName;
			this.m_strEventProviderName = strEventProviderName;
			this.m_asmImport = asmImport;
			this.m_asmSrcItf = asmSrcItf;
		}

		// Token: 0x060063CA RID: 25546 RVA: 0x00155CA0 File Offset: 0x00153EA0
		public Type GetEventItfType()
		{
			Type type = this.m_asmImport.GetType(this.m_strEventItfName, true, false);
			if (type != null && !type.IsVisible)
			{
				type = null;
			}
			return type;
		}

		// Token: 0x060063CB RID: 25547 RVA: 0x00155CD8 File Offset: 0x00153ED8
		public Type GetSrcItfType()
		{
			Type type = this.m_asmSrcItf.GetType(this.m_strSrcItfName, true, false);
			if (type != null && !type.IsVisible)
			{
				type = null;
			}
			return type;
		}

		// Token: 0x060063CC RID: 25548 RVA: 0x00155D0D File Offset: 0x00153F0D
		public string GetEventProviderName()
		{
			return this.m_strEventProviderName;
		}

		// Token: 0x04002CD8 RID: 11480
		private string m_strEventItfName;

		// Token: 0x04002CD9 RID: 11481
		private string m_strSrcItfName;

		// Token: 0x04002CDA RID: 11482
		private string m_strEventProviderName;

		// Token: 0x04002CDB RID: 11483
		private RuntimeAssembly m_asmImport;

		// Token: 0x04002CDC RID: 11484
		private RuntimeAssembly m_asmSrcItf;
	}
}
