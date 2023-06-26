using System;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200080F RID: 2063
	internal class ArrayWithSize
	{
		// Token: 0x060058F1 RID: 22769 RVA: 0x0013AA2A File Offset: 0x00138C2A
		internal ArrayWithSize(IDynamicMessageSink[] sinks, int count)
		{
			this.Sinks = sinks;
			this.Count = count;
		}

		// Token: 0x04002881 RID: 10369
		internal IDynamicMessageSink[] Sinks;

		// Token: 0x04002882 RID: 10370
		internal int Count;
	}
}
