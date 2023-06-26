using System;

namespace System.Threading.Tasks
{
	/// <summary>Stores options that configure the operation of methods on the <see cref="T:System.Threading.Tasks.Parallel" /> class.</summary>
	// Token: 0x0200054E RID: 1358
	[__DynamicallyInvokable]
	public class ParallelOptions
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.ParallelOptions" /> class.</summary>
		// Token: 0x06004024 RID: 16420 RVA: 0x000EFA21 File Offset: 0x000EDC21
		[__DynamicallyInvokable]
		public ParallelOptions()
		{
			this.m_scheduler = TaskScheduler.Default;
			this.m_maxDegreeOfParallelism = -1;
			this.m_cancellationToken = CancellationToken.None;
		}

		/// <summary>Gets or sets the <see cref="T:System.Threading.Tasks.TaskScheduler" /> associated with this <see cref="T:System.Threading.Tasks.ParallelOptions" /> instance. Setting this property to null indicates that the current scheduler should be used.</summary>
		/// <returns>The task scheduler that is associated with this instance.</returns>
		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06004025 RID: 16421 RVA: 0x000EFA46 File Offset: 0x000EDC46
		// (set) Token: 0x06004026 RID: 16422 RVA: 0x000EFA4E File Offset: 0x000EDC4E
		[__DynamicallyInvokable]
		public TaskScheduler TaskScheduler
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_scheduler;
			}
			[__DynamicallyInvokable]
			set
			{
				this.m_scheduler = value;
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06004027 RID: 16423 RVA: 0x000EFA57 File Offset: 0x000EDC57
		internal TaskScheduler EffectiveTaskScheduler
		{
			get
			{
				if (this.m_scheduler == null)
				{
					return TaskScheduler.Current;
				}
				return this.m_scheduler;
			}
		}

		/// <summary>Gets or sets the maximum number of concurrent tasks enabled by this <see cref="T:System.Threading.Tasks.ParallelOptions" /> instance.</summary>
		/// <returns>An integer that represents the maximum degree of parallelism.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to zero or to a value that is less than -1.</exception>
		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06004028 RID: 16424 RVA: 0x000EFA6D File Offset: 0x000EDC6D
		// (set) Token: 0x06004029 RID: 16425 RVA: 0x000EFA75 File Offset: 0x000EDC75
		[__DynamicallyInvokable]
		public int MaxDegreeOfParallelism
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_maxDegreeOfParallelism;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == 0 || value < -1)
				{
					throw new ArgumentOutOfRangeException("MaxDegreeOfParallelism");
				}
				this.m_maxDegreeOfParallelism = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Threading.CancellationToken" /> associated with this <see cref="T:System.Threading.Tasks.ParallelOptions" /> instance.</summary>
		/// <returns>The token that is associated with this instance.</returns>
		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x0600402A RID: 16426 RVA: 0x000EFA90 File Offset: 0x000EDC90
		// (set) Token: 0x0600402B RID: 16427 RVA: 0x000EFA98 File Offset: 0x000EDC98
		[__DynamicallyInvokable]
		public CancellationToken CancellationToken
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_cancellationToken;
			}
			[__DynamicallyInvokable]
			set
			{
				this.m_cancellationToken = value;
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x0600402C RID: 16428 RVA: 0x000EFAA4 File Offset: 0x000EDCA4
		internal int EffectiveMaxConcurrencyLevel
		{
			get
			{
				int num = this.MaxDegreeOfParallelism;
				int maximumConcurrencyLevel = this.EffectiveTaskScheduler.MaximumConcurrencyLevel;
				if (maximumConcurrencyLevel > 0 && maximumConcurrencyLevel != 2147483647)
				{
					num = ((num == -1) ? maximumConcurrencyLevel : Math.Min(maximumConcurrencyLevel, num));
				}
				return num;
			}
		}

		// Token: 0x04001AD2 RID: 6866
		private TaskScheduler m_scheduler;

		// Token: 0x04001AD3 RID: 6867
		private int m_maxDegreeOfParallelism;

		// Token: 0x04001AD4 RID: 6868
		private CancellationToken m_cancellationToken;
	}
}
