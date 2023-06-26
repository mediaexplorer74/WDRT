using System;
using System.Collections;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200097C RID: 2428
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IEnumerable instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("496B0ABE-CDEE-11d3-88E8-00902754C43A")]
	internal interface UCOMIEnumerable
	{
		// Token: 0x0600629F RID: 25247
		[DispId(-4)]
		IEnumerator GetEnumerator();
	}
}
