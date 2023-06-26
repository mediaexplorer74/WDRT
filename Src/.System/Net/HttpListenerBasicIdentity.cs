using System;
using System.Security.Principal;

namespace System.Net
{
	/// <summary>Holds the user name and password from a basic authentication request.</summary>
	// Token: 0x020000F3 RID: 243
	public class HttpListenerBasicIdentity : GenericIdentity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListenerBasicIdentity" /> class using the specified user name and password.</summary>
		/// <param name="username">The user name.</param>
		/// <param name="password">The password.</param>
		// Token: 0x06000878 RID: 2168 RVA: 0x0002F4E9 File Offset: 0x0002D6E9
		public HttpListenerBasicIdentity(string username, string password)
			: base(username, "Basic")
		{
			this.m_Password = password;
		}

		/// <summary>Indicates the password from a basic authentication attempt.</summary>
		/// <returns>A <see cref="T:System.String" /> that holds the password.</returns>
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x0002F4FE File Offset: 0x0002D6FE
		public virtual string Password
		{
			get
			{
				return this.m_Password;
			}
		}

		// Token: 0x04000DD2 RID: 3538
		private string m_Password;
	}
}
