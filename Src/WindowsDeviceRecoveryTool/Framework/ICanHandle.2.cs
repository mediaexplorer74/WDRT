using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Framework
{
	// Token: 0x0200008C RID: 140
	public interface ICanHandle<in T> : ICanHandle
	{
		// Token: 0x060004B5 RID: 1205
		void Handle(T message);
	}
}
