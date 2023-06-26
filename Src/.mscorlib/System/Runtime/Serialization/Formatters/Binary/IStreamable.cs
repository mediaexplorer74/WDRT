using System;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200077E RID: 1918
	internal interface IStreamable
	{
		// Token: 0x060053D8 RID: 21464
		[SecurityCritical]
		void Read(__BinaryParser input);

		// Token: 0x060053D9 RID: 21465
		void Write(__BinaryWriter sout);
	}
}
