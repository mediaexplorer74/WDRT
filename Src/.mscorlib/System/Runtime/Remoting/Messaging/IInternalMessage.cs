using System;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000854 RID: 2132
	internal interface IInternalMessage
	{
		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x06005A8D RID: 23181
		// (set) Token: 0x06005A8E RID: 23182
		ServerIdentity ServerIdentityObject
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06005A8F RID: 23183
		// (set) Token: 0x06005A90 RID: 23184
		Identity IdentityObject
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}

		// Token: 0x06005A91 RID: 23185
		[SecurityCritical]
		void SetURI(string uri);

		// Token: 0x06005A92 RID: 23186
		[SecurityCritical]
		void SetCallContext(LogicalCallContext callContext);

		// Token: 0x06005A93 RID: 23187
		[SecurityCritical]
		bool HasProperties();
	}
}
