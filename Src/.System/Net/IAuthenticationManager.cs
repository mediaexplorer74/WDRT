using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Net
{
	// Token: 0x020000BE RID: 190
	internal interface IAuthenticationManager
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000644 RID: 1604
		// (set) Token: 0x06000645 RID: 1605
		ICredentialPolicy CredentialPolicy { get; set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000646 RID: 1606
		StringDictionary CustomTargetNameDictionary { get; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000647 RID: 1607
		SpnDictionary SpnDictionary { get; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000648 RID: 1608
		bool OSSupportsExtendedProtection { get; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000649 RID: 1609
		bool SspSupportsExtendedProtection { get; }

		// Token: 0x0600064A RID: 1610
		void EnsureConfigLoaded();

		// Token: 0x0600064B RID: 1611
		Authorization Authenticate(string challenge, WebRequest request, ICredentials credentials);

		// Token: 0x0600064C RID: 1612
		Authorization PreAuthenticate(WebRequest request, ICredentials credentials);

		// Token: 0x0600064D RID: 1613
		void Register(IAuthenticationModule authenticationModule);

		// Token: 0x0600064E RID: 1614
		void Unregister(IAuthenticationModule authenticationModule);

		// Token: 0x0600064F RID: 1615
		void Unregister(string authenticationScheme);

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000650 RID: 1616
		IEnumerator RegisteredModules { get; }

		// Token: 0x06000651 RID: 1617
		void BindModule(Uri uri, Authorization response, IAuthenticationModule module);
	}
}
