using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
	// Token: 0x02000061 RID: 97
	internal sealed class Gen2GcCallback : CriticalFinalizerObject
	{
		// Token: 0x0600042B RID: 1067 RVA: 0x0001E348 File Offset: 0x0001C548
		[SecuritySafeCritical]
		public Gen2GcCallback()
		{
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0001E350 File Offset: 0x0001C550
		public static void Register(Func<object, bool> callback, object targetObj)
		{
			Gen2GcCallback gen2GcCallback = new Gen2GcCallback();
			gen2GcCallback.Setup(callback, targetObj);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0001E36B File Offset: 0x0001C56B
		[SecuritySafeCritical]
		private void Setup(Func<object, bool> callback, object targetObj)
		{
			this.m_callback = callback;
			this.m_weakTargetObj = GCHandle.Alloc(targetObj, GCHandleType.Weak);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0001E384 File Offset: 0x0001C584
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

		// Token: 0x04000528 RID: 1320
		private Func<object, bool> m_callback;

		// Token: 0x04000529 RID: 1321
		private GCHandle m_weakTargetObj;
	}
}
