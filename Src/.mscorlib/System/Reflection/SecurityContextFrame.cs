using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020005DA RID: 1498
	internal struct SecurityContextFrame
	{
		// Token: 0x06004585 RID: 17797
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Push(RuntimeAssembly assembly);

		// Token: 0x06004586 RID: 17798
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Pop();

		// Token: 0x04001C8E RID: 7310
		private IntPtr m_GSCookie;

		// Token: 0x04001C8F RID: 7311
		private IntPtr __VFN_table;

		// Token: 0x04001C90 RID: 7312
		private IntPtr m_Next;

		// Token: 0x04001C91 RID: 7313
		private IntPtr m_Assembly;
	}
}
