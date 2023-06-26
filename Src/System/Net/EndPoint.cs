using System;
using System.Net.Sockets;

namespace System.Net
{
	/// <summary>Identifies a network address. This is an <see langword="abstract" /> class.</summary>
	// Token: 0x020000E3 RID: 227
	[global::__DynamicallyInvokable]
	[Serializable]
	public abstract class EndPoint
	{
		/// <summary>Gets the address family to which the endpoint belongs.</summary>
		/// <returns>One of the <see cref="T:System.Net.Sockets.AddressFamily" /> values.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to get or set the property when the property is not overridden in a descendant class.</exception>
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x0002B41B File Offset: 0x0002961B
		[global::__DynamicallyInvokable]
		public virtual AddressFamily AddressFamily
		{
			[global::__DynamicallyInvokable]
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>Serializes endpoint information into a <see cref="T:System.Net.SocketAddress" /> instance.</summary>
		/// <returns>A <see cref="T:System.Net.SocketAddress" /> instance that contains the endpoint information.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method when the method is not overridden in a descendant class.</exception>
		// Token: 0x060007C5 RID: 1989 RVA: 0x0002B422 File Offset: 0x00029622
		[global::__DynamicallyInvokable]
		public virtual SocketAddress Serialize()
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		/// <summary>Creates an <see cref="T:System.Net.EndPoint" /> instance from a <see cref="T:System.Net.SocketAddress" /> instance.</summary>
		/// <param name="socketAddress">The socket address that serves as the endpoint for a connection.</param>
		/// <returns>A new <see cref="T:System.Net.EndPoint" /> instance that is initialized from the specified <see cref="T:System.Net.SocketAddress" /> instance.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method when the method is not overridden in a descendant class.</exception>
		// Token: 0x060007C6 RID: 1990 RVA: 0x0002B429 File Offset: 0x00029629
		[global::__DynamicallyInvokable]
		public virtual EndPoint Create(SocketAddress socketAddress)
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.EndPoint" /> class.</summary>
		// Token: 0x060007C7 RID: 1991 RVA: 0x0002B430 File Offset: 0x00029630
		[global::__DynamicallyInvokable]
		protected EndPoint()
		{
		}
	}
}
