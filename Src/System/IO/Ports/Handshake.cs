using System;

namespace System.IO.Ports
{
	/// <summary>Specifies the control protocol used in establishing a serial port communication for a <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
	// Token: 0x02000408 RID: 1032
	public enum Handshake
	{
		/// <summary>No control is used for the handshake.</summary>
		// Token: 0x040020DD RID: 8413
		None,
		/// <summary>The XON/XOFF software control protocol is used. The XOFF control is sent to stop the transmission of data. The XON control is sent to resume the transmission. These software controls are used instead of Request to Send (RTS) and Clear to Send (CTS) hardware controls.</summary>
		// Token: 0x040020DE RID: 8414
		XOnXOff,
		/// <summary>Request-to-Send (RTS) hardware flow control is used. RTS signals that data is available for transmission. If the input buffer becomes full, the RTS line will be set to <see langword="false" />. The RTS line will be set to <see langword="true" /> when more room becomes available in the input buffer.</summary>
		// Token: 0x040020DF RID: 8415
		RequestToSend,
		/// <summary>Both the Request-to-Send (RTS) hardware control and the XON/XOFF software controls are used.</summary>
		// Token: 0x040020E0 RID: 8416
		RequestToSendXOnXOff
	}
}
