using System;
using System.Runtime.CompilerServices;

namespace System.StubHelpers
{
	// Token: 0x020005A3 RID: 1443
	internal static class WinRTTypeNameConverter
	{
		// Token: 0x0600433B RID: 17211
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string ConvertToWinRTTypeName(Type managedType, out bool isPrimitive);

		// Token: 0x0600433C RID: 17212
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Type GetTypeFromWinRTTypeName(string typeName, out bool isPrimitive);
	}
}
