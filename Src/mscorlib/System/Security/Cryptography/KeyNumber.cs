using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Specifies whether to create an asymmetric signature key or an asymmetric exchange key.</summary>
	// Token: 0x0200026A RID: 618
	[ComVisible(true)]
	[Serializable]
	public enum KeyNumber
	{
		/// <summary>An exchange key pair used to encrypt session keys so that they can be safely stored and exchanged with other users.</summary>
		// Token: 0x04000C55 RID: 3157
		Exchange = 1,
		/// <summary>A signature key pair used for authenticating digitally signed messages or files.</summary>
		// Token: 0x04000C56 RID: 3158
		Signature
	}
}
