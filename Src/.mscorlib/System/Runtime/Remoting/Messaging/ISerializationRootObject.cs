using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200086A RID: 2154
	internal interface ISerializationRootObject
	{
		// Token: 0x06005BDE RID: 23518
		[SecurityCritical]
		void RootSetObjectData(SerializationInfo info, StreamingContext ctx);
	}
}
