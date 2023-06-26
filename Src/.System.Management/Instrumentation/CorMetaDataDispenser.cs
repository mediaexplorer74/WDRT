using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Management.Instrumentation
{
	// Token: 0x020000C3 RID: 195
	[Guid("E5CB7A31-7512-11d2-89CE-0080C792E5D8")]
	[TypeLibType(TypeLibTypeFlags.FCanCreate)]
	[ClassInterface(ClassInterfaceType.None)]
	[ComImport]
	internal class CorMetaDataDispenser
	{
		// Token: 0x0600056B RID: 1387
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		public extern CorMetaDataDispenser();
	}
}
