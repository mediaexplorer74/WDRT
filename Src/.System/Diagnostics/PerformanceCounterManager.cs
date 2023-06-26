using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Prepares performance data for the performance.dll the system loads when working with performance counters.</summary>
	// Token: 0x020004E7 RID: 1255
	[ComVisible(true)]
	[Guid("82840BE1-D273-11D2-B94A-00600893B17A")]
	[Obsolete("This class has been deprecated.  Use the PerformanceCounters through the System.Diagnostics.PerformanceCounter class instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class PerformanceCounterManager : ICollectData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterManager" /> class.</summary>
		// Token: 0x06002F6A RID: 12138 RVA: 0x000D67A6 File Offset: 0x000D49A6
		[Obsolete("This class has been deprecated.  Use the PerformanceCounters through the System.Diagnostics.PerformanceCounter class instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public PerformanceCounterManager()
		{
		}

		/// <summary>Performance data collection routine. Called by the PerfCount perf dll.</summary>
		/// <param name="callIdx">The call index.</param>
		/// <param name="valueNamePtr">A pointer to a Unicode string list with the requested Object identifiers.</param>
		/// <param name="dataPtr">A pointer to the data buffer.</param>
		/// <param name="totalBytes">A pointer to a number of bytes.</param>
		/// <param name="res">When this method returns, contains a <see cref="T:System.IntPtr" /> with a value of -1.</param>
		// Token: 0x06002F6B RID: 12139 RVA: 0x000D67AE File Offset: 0x000D49AE
		[Obsolete("This class has been deprecated.  Use the PerformanceCounters through the System.Diagnostics.PerformanceCounter class instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		void ICollectData.CollectData(int callIdx, IntPtr valueNamePtr, IntPtr dataPtr, int totalBytes, out IntPtr res)
		{
			res = (IntPtr)(-1);
		}

		/// <summary>Called by the perf dll's close performance data</summary>
		// Token: 0x06002F6C RID: 12140 RVA: 0x000D67B9 File Offset: 0x000D49B9
		[Obsolete("This class has been deprecated.  Use the PerformanceCounters through the System.Diagnostics.PerformanceCounter class instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		void ICollectData.CloseData()
		{
		}
	}
}
