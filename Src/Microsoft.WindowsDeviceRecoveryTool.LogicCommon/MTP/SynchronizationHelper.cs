using System;
using System.Threading;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.MTP
{
	// Token: 0x02000026 RID: 38
	internal static class SynchronizationHelper
	{
		// Token: 0x06000295 RID: 661 RVA: 0x00009228 File Offset: 0x00007428
		public static EventHandler<T> ExecuteInCurrentContext<T>(EventHandler<T> handler)
		{
			SynchronizationContext context = SynchronizationContext.Current;
			return delegate(object sender, T e)
			{
				context.Post(delegate(object _)
				{
					handler(sender, e);
				}, null);
			};
		}
	}
}
