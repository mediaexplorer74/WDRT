using System;

namespace System.IO.Ports
{
	/// <summary>Prepares data for the <see cref="E:System.IO.Ports.SerialPort.ErrorReceived" /> event.</summary>
	// Token: 0x0200040C RID: 1036
	public class SerialErrorReceivedEventArgs : EventArgs
	{
		// Token: 0x060026AE RID: 9902 RVA: 0x000B1EA5 File Offset: 0x000B00A5
		internal SerialErrorReceivedEventArgs(SerialError eventCode)
		{
			this.errorType = eventCode;
		}

		/// <summary>Gets or sets the event type.</summary>
		/// <returns>One of the <see cref="T:System.IO.Ports.SerialError" /> values.</returns>
		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x060026AF RID: 9903 RVA: 0x000B1EB4 File Offset: 0x000B00B4
		public SerialError EventType
		{
			get
			{
				return this.errorType;
			}
		}

		// Token: 0x040020ED RID: 8429
		private SerialError errorType;
	}
}
