using System;

namespace System.IO.Ports
{
	/// <summary>Specifies the type of character that was received on the serial port of the <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
	// Token: 0x02000412 RID: 1042
	public enum SerialData
	{
		/// <summary>A character was received and placed in the input buffer.</summary>
		// Token: 0x04002127 RID: 8487
		Chars = 1,
		/// <summary>The end of file character was received and placed in the input buffer.</summary>
		// Token: 0x04002128 RID: 8488
		Eof
	}
}
