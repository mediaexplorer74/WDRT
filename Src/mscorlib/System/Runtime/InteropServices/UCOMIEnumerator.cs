using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200097D RID: 2429
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IEnumerator instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("496B0ABF-CDEE-11d3-88E8-00902754C43A")]
	internal interface UCOMIEnumerator
	{
		// Token: 0x060062A0 RID: 25248
		bool MoveNext();

		// Token: 0x17001116 RID: 4374
		// (get) Token: 0x060062A1 RID: 25249
		object Current { get; }

		// Token: 0x060062A2 RID: 25250
		void Reset();
	}
}
