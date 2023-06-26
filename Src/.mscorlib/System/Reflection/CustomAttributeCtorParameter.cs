using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005D9 RID: 1497
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	internal struct CustomAttributeCtorParameter
	{
		// Token: 0x06004583 RID: 17795 RVA: 0x00100CF4 File Offset: 0x000FEEF4
		public CustomAttributeCtorParameter(CustomAttributeType type)
		{
			this.m_type = type;
			this.m_encodedArgument = default(CustomAttributeEncodedArgument);
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06004584 RID: 17796 RVA: 0x00100D09 File Offset: 0x000FEF09
		public CustomAttributeEncodedArgument CustomAttributeEncodedArgument
		{
			get
			{
				return this.m_encodedArgument;
			}
		}

		// Token: 0x04001C8C RID: 7308
		private CustomAttributeType m_type;

		// Token: 0x04001C8D RID: 7309
		private CustomAttributeEncodedArgument m_encodedArgument;
	}
}
