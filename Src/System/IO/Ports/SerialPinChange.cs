using System;

namespace System.IO.Ports
{
	/// <summary>Specifies the type of change that occurred on the <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
	// Token: 0x0200040E RID: 1038
	public enum SerialPinChange
	{
		/// <summary>The Clear to Send (CTS) signal changed state. This signal is used to indicate whether data can be sent over the serial port.</summary>
		// Token: 0x040020EF RID: 8431
		CtsChanged = 8,
		/// <summary>The Data Set Ready (DSR) signal changed state. This signal is used to indicate whether the device on the serial port is ready to operate.</summary>
		// Token: 0x040020F0 RID: 8432
		DsrChanged = 16,
		/// <summary>The Carrier Detect (CD) signal changed state. This signal is used to indicate whether a modem is connected to a working phone line and a data carrier signal is detected.</summary>
		// Token: 0x040020F1 RID: 8433
		CDChanged = 32,
		/// <summary>A ring indicator was detected.</summary>
		// Token: 0x040020F2 RID: 8434
		Ring = 256,
		/// <summary>A break was detected on input.</summary>
		// Token: 0x040020F3 RID: 8435
		Break = 64
	}
}
