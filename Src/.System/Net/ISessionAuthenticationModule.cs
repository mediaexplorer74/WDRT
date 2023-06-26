using System;

namespace System.Net
{
	// Token: 0x020001B8 RID: 440
	internal interface ISessionAuthenticationModule : IAuthenticationModule
	{
		// Token: 0x06001139 RID: 4409
		bool Update(string challenge, WebRequest webRequest);

		// Token: 0x0600113A RID: 4410
		void ClearSession(WebRequest webRequest);

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x0600113B RID: 4411
		bool CanUseDefaultCredentials { get; }
	}
}
