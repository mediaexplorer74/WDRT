using System;
using System.ComponentModel;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.WriteStreamClosed" /> event.</summary>
	// Token: 0x0200018E RID: 398
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class WriteStreamClosedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WriteStreamClosedEventArgs" /> class.</summary>
		// Token: 0x06000F4A RID: 3914 RVA: 0x0004F5B3 File Offset: 0x0004D7B3
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public WriteStreamClosedEventArgs()
		{
		}

		/// <summary>Gets the error value when a write stream is closed.</summary>
		/// <returns>Returns <see cref="T:System.Exception" />.</returns>
		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000F4B RID: 3915 RVA: 0x0004F5BB File Offset: 0x0004D7BB
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Exception Error
		{
			get
			{
				return null;
			}
		}
	}
}
