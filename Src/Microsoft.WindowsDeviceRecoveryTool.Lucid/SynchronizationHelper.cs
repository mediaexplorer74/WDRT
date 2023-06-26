using System;
using System.Threading;

namespace Microsoft.WindowsDeviceRecoveryTool.Lucid
{
	// Token: 0x02000005 RID: 5
	public static class SynchronizationHelper
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002634 File Offset: 0x00000834
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
