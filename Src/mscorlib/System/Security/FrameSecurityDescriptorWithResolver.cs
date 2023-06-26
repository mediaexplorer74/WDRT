using System;
using System.Reflection.Emit;

namespace System.Security
{
	// Token: 0x020001D7 RID: 471
	internal class FrameSecurityDescriptorWithResolver : FrameSecurityDescriptor
	{
		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06001C99 RID: 7321 RVA: 0x00062084 File Offset: 0x00060284
		public DynamicResolver Resolver
		{
			get
			{
				return this.m_resolver;
			}
		}

		// Token: 0x040009FB RID: 2555
		private DynamicResolver m_resolver;
	}
}
