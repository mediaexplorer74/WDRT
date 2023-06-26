using System;

namespace System.IO.Ports
{
	/// <summary>Provides data for the <see cref="E:System.IO.Ports.SerialPort.DataReceived" /> event.</summary>
	// Token: 0x02000413 RID: 1043
	public class SerialDataReceivedEventArgs : EventArgs
	{
		// Token: 0x0600270D RID: 9997 RVA: 0x000B39DC File Offset: 0x000B1BDC
		internal SerialDataReceivedEventArgs(SerialData eventCode)
		{
			this.receiveType = eventCode;
		}

		/// <summary>Gets or sets the event type.</summary>
		/// <returns>One of the <see cref="T:System.IO.Ports.SerialData" /> values.</returns>
		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x0600270E RID: 9998 RVA: 0x000B39EB File Offset: 0x000B1BEB
		public SerialData EventType
		{
			get
			{
				return this.receiveType;
			}
		}

		// Token: 0x04002129 RID: 8489
		internal SerialData receiveType;
	}
}
