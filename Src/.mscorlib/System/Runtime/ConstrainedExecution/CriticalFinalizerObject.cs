using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Runtime.ConstrainedExecution
{
	/// <summary>Ensures that all finalization code in derived classes is marked as critical.</summary>
	// Token: 0x02000727 RID: 1831
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
	public abstract class CriticalFinalizerObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.ConstrainedExecution.CriticalFinalizerObject" /> class.</summary>
		// Token: 0x06005189 RID: 20873 RVA: 0x001208BE File Offset: 0x0011EABE
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected CriticalFinalizerObject()
		{
		}

		/// <summary>Releases all the resources used by the <see cref="T:System.Runtime.ConstrainedExecution.CriticalFinalizerObject" /> class.</summary>
		// Token: 0x0600518A RID: 20874 RVA: 0x001208C8 File Offset: 0x0011EAC8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		~CriticalFinalizerObject()
		{
		}
	}
}
