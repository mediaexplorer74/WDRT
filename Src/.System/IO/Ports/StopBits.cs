using System;

namespace System.IO.Ports
{
	/// <summary>Specifies the number of stop bits used on the <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
	// Token: 0x02000416 RID: 1046
	public enum StopBits
	{
		/// <summary>No stop bits are used. This value is not supported by the <see cref="P:System.IO.Ports.SerialPort.StopBits" /> property.</summary>
		// Token: 0x04002142 RID: 8514
		None,
		/// <summary>One stop bit is used.</summary>
		// Token: 0x04002143 RID: 8515
		One,
		/// <summary>Two stop bits are used.</summary>
		// Token: 0x04002144 RID: 8516
		Two,
		/// <summary>1.5 stop bits are used.</summary>
		// Token: 0x04002145 RID: 8517
		OnePointFive
	}
}
