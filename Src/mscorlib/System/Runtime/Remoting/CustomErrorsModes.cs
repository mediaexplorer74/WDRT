using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	/// <summary>Specifies how custom errors are handled.</summary>
	// Token: 0x020007C4 RID: 1988
	[ComVisible(true)]
	public enum CustomErrorsModes
	{
		/// <summary>All callers receive filtered exception information.</summary>
		// Token: 0x04002793 RID: 10131
		On,
		/// <summary>All callers receive complete exception information.</summary>
		// Token: 0x04002794 RID: 10132
		Off,
		/// <summary>Local callers receive complete exception information; remote callers receive filtered exception information.</summary>
		// Token: 0x04002795 RID: 10133
		RemoteOnly
	}
}
