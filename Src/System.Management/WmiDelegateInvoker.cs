using System;

namespace System.Management
{
	// Token: 0x02000027 RID: 39
	internal class WmiDelegateInvoker
	{
		// Token: 0x06000137 RID: 311 RVA: 0x00007F19 File Offset: 0x00006119
		internal WmiDelegateInvoker(object sender)
		{
			this.sender = sender;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00007F28 File Offset: 0x00006128
		internal void FireEventToDelegates(MulticastDelegate md, ManagementEventArgs args)
		{
			try
			{
				if (md != null)
				{
					foreach (Delegate @delegate in md.GetInvocationList())
					{
						try
						{
							@delegate.DynamicInvoke(new object[] { this.sender, args });
						}
						catch
						{
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x04000120 RID: 288
		internal object sender;
	}
}
