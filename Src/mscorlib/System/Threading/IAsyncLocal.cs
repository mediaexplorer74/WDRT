using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004E4 RID: 1252
	internal interface IAsyncLocal
	{
		// Token: 0x06003B94 RID: 15252
		[SecurityCritical]
		void OnValueChanged(object previousValue, object currentValue, bool contextChanged);
	}
}
