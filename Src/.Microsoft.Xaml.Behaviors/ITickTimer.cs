using System;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x02000016 RID: 22
	internal interface ITickTimer
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060000B5 RID: 181
		// (remove) Token: 0x060000B6 RID: 182
		event EventHandler Tick;

		// Token: 0x060000B7 RID: 183
		void Start();

		// Token: 0x060000B8 RID: 184
		void Stop();

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000B9 RID: 185
		// (set) Token: 0x060000BA RID: 186
		TimeSpan Interval { get; set; }
	}
}
