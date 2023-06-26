using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A26 RID: 2598
	[Guid("496B0ABF-CDEE-11d3-88E8-00902754C43A")]
	internal interface IEnumerator
	{
		// Token: 0x06006633 RID: 26163
		bool MoveNext();

		// Token: 0x1700118A RID: 4490
		// (get) Token: 0x06006634 RID: 26164
		object Current { get; }

		// Token: 0x06006635 RID: 26165
		void Reset();
	}
}
