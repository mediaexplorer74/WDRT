using System;

namespace System.Net
{
	/// <summary>Represents the method that specifies a local Internet Protocol address and port number for a <see cref="T:System.Net.ServicePoint" />.</summary>
	/// <param name="servicePoint">The <see cref="T:System.Net.ServicePoint" /> associated with the connection to be created.</param>
	/// <param name="remoteEndPoint">The remote <see cref="T:System.Net.IPEndPoint" /> that specifies the remote host.</param>
	/// <param name="retryCount">The number of times this delegate was called for a specified connection.</param>
	/// <returns>The local <see cref="T:System.Net.IPEndPoint" /> to which the <see cref="T:System.Net.ServicePoint" /> is bound.</returns>
	// Token: 0x0200015E RID: 350
	// (Invoke) Token: 0x06000C15 RID: 3093
	public delegate IPEndPoint BindIPEndPoint(ServicePoint servicePoint, IPEndPoint remoteEndPoint, int retryCount);
}
