using System;

namespace System.IO.Ports
{
	/// <summary>Provides data for the <see cref="E:System.IO.Ports.SerialPort.PinChanged" /> event.</summary>
	// Token: 0x0200040F RID: 1039
	public class SerialPinChangedEventArgs : EventArgs
	{
		// Token: 0x060026B4 RID: 9908 RVA: 0x000B1EBC File Offset: 0x000B00BC
		internal SerialPinChangedEventArgs(SerialPinChange eventCode)
		{
			this.pinChanged = eventCode;
		}

		/// <summary>Gets or sets the event type.</summary>
		/// <returns>One of the <see cref="T:System.IO.Ports.SerialPinChange" /> values.</returns>
		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x060026B5 RID: 9909 RVA: 0x000B1ECB File Offset: 0x000B00CB
		public SerialPinChange EventType
		{
			get
			{
				return this.pinChanged;
			}
		}

		// Token: 0x040020F4 RID: 8436
		private SerialPinChange pinChanged;
	}
}
