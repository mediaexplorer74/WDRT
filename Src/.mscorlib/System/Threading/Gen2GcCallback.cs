using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x0200050C RID: 1292
	internal sealed class Gen2GcCallback : CriticalFinalizerObject
	{
		// Token: 0x06003CED RID: 15597 RVA: 0x000E7304 File Offset: 0x000E5504
		[SecuritySafeCritical]
		public Gen2GcCallback()
		{
		}

		// Token: 0x06003CEE RID: 15598 RVA: 0x000E730C File Offset: 0x000E550C
		public static void Register(Func<object, bool> callback, object targetObj)
		{
			Gen2GcCallback gen2GcCallback = new Gen2GcCallback();
			gen2GcCallback.Setup(callback, targetObj);
		}

		// Token: 0x06003CEF RID: 15599 RVA: 0x000E7327 File Offset: 0x000E5527
		[SecuritySafeCritical]
		private void Setup(Func<object, bool> callback, object targetObj)
		{
			this.m_callback = callback;
			this.m_weakTargetObj = GCHandle.Alloc(targetObj, GCHandleType.Weak);
		}

		// Token: 0x06003CF0 RID: 15600 RVA: 0x000E7340 File Offset: 0x000E5540
		[SecuritySafeCritical]
		protected override void Finalize()
		{
			try
			{
				if (this.m_weakTargetObj.IsAllocated)
				{
					object target = this.m_weakTargetObj.Target;
					if (target == null)
					{
						this.m_weakTargetObj.Free();
					}
					else
					{
						try
						{
							if (!this.m_callback(target))
							{
								return;
							}
						}
						catch
						{
						}
						if (!Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload())
						{
							GC.ReRegisterForFinalize(this);
						}
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x040019DF RID: 6623
		private Func<object, bool> m_callback;

		// Token: 0x040019E0 RID: 6624
		private GCHandle m_weakTargetObj;
	}
}
