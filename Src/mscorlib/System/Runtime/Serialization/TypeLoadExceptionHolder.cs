using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000750 RID: 1872
	internal class TypeLoadExceptionHolder
	{
		// Token: 0x060052E2 RID: 21218 RVA: 0x00124727 File Offset: 0x00122927
		internal TypeLoadExceptionHolder(string typeName)
		{
			this.m_typeName = typeName;
		}

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x060052E3 RID: 21219 RVA: 0x00124736 File Offset: 0x00122936
		internal string TypeName
		{
			get
			{
				return this.m_typeName;
			}
		}

		// Token: 0x040024BC RID: 9404
		private string m_typeName;
	}
}
