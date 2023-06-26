using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009EF RID: 2543
	[Guid("60D27C8D-5F61-4CCE-B751-690FAE66AA53")]
	[ComImport]
	internal interface IManagedActivationFactory
	{
		// Token: 0x060064D5 RID: 25813
		void RunClassConstructor();
	}
}
