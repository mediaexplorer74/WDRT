using System;

namespace System.Net
{
	/// <summary>Selects the authentication scheme for an <see cref="T:System.Net.HttpListener" /> instance.</summary>
	/// <param name="httpRequest">The <see cref="T:System.Net.HttpListenerRequest" /> instance for which to select an authentication scheme.</param>
	/// <returns>One of the <see cref="T:System.Net.AuthenticationSchemes" /> values that indicates the method of authentication to use for the specified client request.</returns>
	// Token: 0x020000C5 RID: 197
	// (Invoke) Token: 0x06000693 RID: 1683
	public delegate AuthenticationSchemes AuthenticationSchemeSelector(HttpListenerRequest httpRequest);
}
