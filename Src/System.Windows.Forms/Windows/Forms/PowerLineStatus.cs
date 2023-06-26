using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the system power status.</summary>
	// Token: 0x0200031E RID: 798
	public enum PowerLineStatus
	{
		/// <summary>The system is offline.</summary>
		// Token: 0x04001EA0 RID: 7840
		Offline,
		/// <summary>The system is online.</summary>
		// Token: 0x04001EA1 RID: 7841
		Online,
		/// <summary>The power status of the system is unknown.</summary>
		// Token: 0x04001EA2 RID: 7842
		Unknown = 255
	}
}
