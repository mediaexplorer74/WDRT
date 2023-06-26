using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Identifies the platform targeted by an executable.</summary>
	// Token: 0x0200060A RID: 1546
	[ComVisible(true)]
	[Serializable]
	public enum ImageFileMachine
	{
		/// <summary>Targets a 32-bit Intel processor.</summary>
		// Token: 0x04001DBE RID: 7614
		I386 = 332,
		/// <summary>Targets a 64-bit Intel processor.</summary>
		// Token: 0x04001DBF RID: 7615
		IA64 = 512,
		/// <summary>Targets a 64-bit AMD processor.</summary>
		// Token: 0x04001DC0 RID: 7616
		AMD64 = 34404,
		/// <summary>Targets an ARM processor.</summary>
		// Token: 0x04001DC1 RID: 7617
		ARM = 452
	}
}
