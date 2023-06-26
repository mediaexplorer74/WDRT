using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies the attributes of an event.</summary>
	// Token: 0x020005DF RID: 1503
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum EventAttributes
	{
		/// <summary>Specifies that the event has no attributes.</summary>
		// Token: 0x04001C9D RID: 7325
		[__DynamicallyInvokable]
		None = 0,
		/// <summary>Specifies that the event is special in a way described by the name.</summary>
		// Token: 0x04001C9E RID: 7326
		[__DynamicallyInvokable]
		SpecialName = 512,
		/// <summary>Specifies a reserved flag for common language runtime use only.</summary>
		// Token: 0x04001C9F RID: 7327
		ReservedMask = 1024,
		/// <summary>Specifies that the common language runtime should check name encoding.</summary>
		// Token: 0x04001CA0 RID: 7328
		[__DynamicallyInvokable]
		RTSpecialName = 1024
	}
}
