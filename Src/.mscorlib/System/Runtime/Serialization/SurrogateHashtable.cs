using System;
using System.Collections;

namespace System.Runtime.Serialization
{
	// Token: 0x02000759 RID: 1881
	internal class SurrogateHashtable : Hashtable
	{
		// Token: 0x0600530A RID: 21258 RVA: 0x00125228 File Offset: 0x00123428
		internal SurrogateHashtable(int size)
			: base(size)
		{
		}

		// Token: 0x0600530B RID: 21259 RVA: 0x00125234 File Offset: 0x00123434
		protected override bool KeyEquals(object key, object item)
		{
			SurrogateKey surrogateKey = (SurrogateKey)item;
			SurrogateKey surrogateKey2 = (SurrogateKey)key;
			return surrogateKey2.m_type == surrogateKey.m_type && (surrogateKey2.m_context.m_state & surrogateKey.m_context.m_state) == surrogateKey.m_context.m_state && surrogateKey2.m_context.Context == surrogateKey.m_context.Context;
		}
	}
}
