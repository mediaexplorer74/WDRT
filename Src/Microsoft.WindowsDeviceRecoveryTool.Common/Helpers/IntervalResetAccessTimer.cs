using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Microsoft.WindowsDeviceRecoveryTool.Common.Helpers
{
	// Token: 0x02000011 RID: 17
	public class IntervalResetAccessTimer
	{
		// Token: 0x06000081 RID: 129 RVA: 0x00003BAC File Offset: 0x00001DAC
		public IntervalResetAccessTimer(int intervalMillis, bool isAccessAvailableInitialValue)
		{
			this.intervalMillis = intervalMillis;
			this.isAccessAvailable = isAccessAvailableInitialValue;
			this.modifyIsAccessAvailableValueSemaphore = new SemaphoreSlim(1, 1);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003BD4 File Offset: 0x00001DD4
		public void StartTimer()
		{
			bool flag = this.intervalMillis <= 0;
			if (flag)
			{
				this.isAccessAvailable = true;
			}
			else
			{
				this.intervalTimer = new System.Timers.Timer((double)this.intervalMillis);
				this.intervalTimer.Elapsed += delegate(object sender, ElapsedEventArgs args)
				{
					this.isAccessAvailable = true;
				};
				this.intervalTimer.AutoReset = true;
				this.intervalTimer.Enabled = true;
				this.intervalTimer.Start();
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003C4C File Offset: 0x00001E4C
		public void StopTimer()
		{
			bool flag = this.intervalTimer == null;
			if (!flag)
			{
				this.intervalTimer.Stop();
				this.intervalTimer = null;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003C80 File Offset: 0x00001E80
		public bool RunIfAccessAvailable(Action actionToRun)
		{
			bool flag = this.TryAccessSectionAndSet();
			bool flag2 = flag;
			if (flag2)
			{
				actionToRun();
			}
			return flag;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003CA8 File Offset: 0x00001EA8
		public async Task<bool> RunIfAccessAvailableAsync(Task taskToRun, CancellationToken cancellationToken)
		{
			bool flag = await this.TryAccessSectionAndSetAsync(cancellationToken);
			bool result = flag;
			if (result)
			{
				await taskToRun;
			}
			return result;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003CFC File Offset: 0x00001EFC
		public bool TryAccessSectionAndSet()
		{
			bool flag = this.intervalMillis <= 0;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				try
				{
					this.modifyIsAccessAvailableValueSemaphore.Wait();
					bool flag3 = this.isAccessAvailable;
					bool flag4 = flag3;
					if (flag4)
					{
						this.isAccessAvailable = false;
					}
					flag2 = flag3;
				}
				finally
				{
					this.modifyIsAccessAvailableValueSemaphore.Release();
				}
			}
			return flag2;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003D68 File Offset: 0x00001F68
		public async Task<bool> TryAccessSectionAndSetAsync(CancellationToken cancellationToken)
		{
			bool flag = this.intervalMillis <= 0;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				try
				{
					await this.modifyIsAccessAvailableValueSemaphore.WaitAsync(cancellationToken);
					bool value = this.isAccessAvailable;
					if (value)
					{
						this.isAccessAvailable = false;
					}
					flag2 = value;
				}
				finally
				{
					this.modifyIsAccessAvailableValueSemaphore.Release();
				}
			}
			return flag2;
		}

		// Token: 0x04000017 RID: 23
		private readonly int intervalMillis;

		// Token: 0x04000018 RID: 24
		private bool isAccessAvailable;

		// Token: 0x04000019 RID: 25
		private System.Timers.Timer intervalTimer;

		// Token: 0x0400001A RID: 26
		private readonly SemaphoreSlim modifyIsAccessAvailableValueSemaphore;
	}
}
