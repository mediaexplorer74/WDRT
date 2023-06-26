using System;

namespace System.Windows.Forms
{
	/// <summary>Provides options that specify the relationship between the control and preprocessing messages.</summary>
	// Token: 0x02000322 RID: 802
	public enum PreProcessControlState
	{
		/// <summary>Specifies that the message has been processed and no further processing is required.</summary>
		// Token: 0x04001EAF RID: 7855
		MessageProcessed,
		/// <summary>Specifies that the control requires the message and that processing should continue.</summary>
		// Token: 0x04001EB0 RID: 7856
		MessageNeeded,
		/// <summary>Specifies that the control does not require the message.</summary>
		// Token: 0x04001EB1 RID: 7857
		MessageNotNeeded
	}
}
