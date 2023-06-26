using System;

namespace System.Runtime.Remoting
{
	// Token: 0x020007C3 RID: 1987
	internal class RemoteAppEntry
	{
		// Token: 0x06005628 RID: 22056 RVA: 0x00132CC7 File Offset: 0x00130EC7
		internal RemoteAppEntry(string appName, string appURI)
		{
			this._remoteAppName = appName;
			this._remoteAppURI = appURI;
		}

		// Token: 0x06005629 RID: 22057 RVA: 0x00132CDD File Offset: 0x00130EDD
		internal string GetAppURI()
		{
			return this._remoteAppURI;
		}

		// Token: 0x04002790 RID: 10128
		private string _remoteAppName;

		// Token: 0x04002791 RID: 10129
		private string _remoteAppURI;
	}
}
