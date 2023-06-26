using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000758 RID: 1880
	[Serializable]
	internal class SurrogateKey
	{
		// Token: 0x06005308 RID: 21256 RVA: 0x00125205 File Offset: 0x00123405
		internal SurrogateKey(Type type, StreamingContext context)
		{
			this.m_type = type;
			this.m_context = context;
		}

		// Token: 0x06005309 RID: 21257 RVA: 0x0012521B File Offset: 0x0012341B
		public override int GetHashCode()
		{
			return this.m_type.GetHashCode();
		}

		// Token: 0x040024CF RID: 9423
		internal Type m_type;

		// Token: 0x040024D0 RID: 9424
		internal StreamingContext m_context;
	}
}
