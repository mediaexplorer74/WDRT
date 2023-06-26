using System;
using System.Reflection;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000831 RID: 2097
	internal static class AsyncMessageHelper
	{
		// Token: 0x060059D4 RID: 22996 RVA: 0x0013DD28 File Offset: 0x0013BF28
		internal static void GetOutArgs(ParameterInfo[] syncParams, object[] syncArgs, object[] endArgs)
		{
			int num = 0;
			for (int i = 0; i < syncParams.Length; i++)
			{
				if (syncParams[i].IsOut || syncParams[i].ParameterType.IsByRef)
				{
					endArgs[num++] = syncArgs[i];
				}
			}
		}
	}
}
