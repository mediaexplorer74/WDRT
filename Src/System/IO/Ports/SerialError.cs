using System;

namespace System.IO.Ports
{
	/// <summary>Specifies errors that occur on the <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
	// Token: 0x0200040B RID: 1035
	public enum SerialError
	{
		/// <summary>The application tried to transmit a character, but the output buffer was full.</summary>
		// Token: 0x040020E8 RID: 8424
		TXFull = 256,
		/// <summary>An input buffer overflow has occurred. There is either no room in the input buffer, or a character was received after the end-of-file (EOF) character.</summary>
		// Token: 0x040020E9 RID: 8425
		RXOver = 1,
		/// <summary>A character-buffer overrun has occurred. The next character is lost.</summary>
		// Token: 0x040020EA RID: 8426
		Overrun,
		/// <summary>The hardware detected a parity error.</summary>
		// Token: 0x040020EB RID: 8427
		RXParity = 4,
		/// <summary>The hardware detected a framing error.</summary>
		// Token: 0x040020EC RID: 8428
		Frame = 8
	}
}
