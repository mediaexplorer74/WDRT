using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents the base class for classes that contain event data, and provides a value to use for events that do not include event data.</summary>
	// Token: 0x020000DF RID: 223
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EventArgs" /> class.</summary>
		// Token: 0x06000E74 RID: 3700 RVA: 0x0002CC53 File Offset: 0x0002AE53
		[__DynamicallyInvokable]
		public EventArgs()
		{
		}

		/// <summary>Provides a value to use with events that do not have event data.</summary>
		// Token: 0x0400057C RID: 1404
		[__DynamicallyInvokable]
		public static readonly EventArgs Empty = new EventArgs();
	}
}
