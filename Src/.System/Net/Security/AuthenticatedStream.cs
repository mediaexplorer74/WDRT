using System;
using System.IO;

namespace System.Net.Security
{
	/// <summary>Provides methods for passing credentials across a stream and requesting or performing authentication for client-server applications.</summary>
	// Token: 0x02000354 RID: 852
	public abstract class AuthenticatedStream : Stream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Security.AuthenticatedStream" /> class.</summary>
		/// <param name="innerStream">A <see cref="T:System.IO.Stream" /> object used by the <see cref="T:System.Net.Security.AuthenticatedStream" /> for sending and receiving data.</param>
		/// <param name="leaveInnerStreamOpen">A <see cref="T:System.Boolean" /> that indicates whether closing this <see cref="T:System.Net.Security.AuthenticatedStream" /> object also closes <paramref name="innerStream" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="innerStream" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="innerStream" /> is equal to <see cref="F:System.IO.Stream.Null" />.</exception>
		// Token: 0x06001E80 RID: 7808 RVA: 0x0008FA38 File Offset: 0x0008DC38
		protected AuthenticatedStream(Stream innerStream, bool leaveInnerStreamOpen)
		{
			if (innerStream == null || innerStream == Stream.Null)
			{
				throw new ArgumentNullException("innerStream");
			}
			if (!innerStream.CanRead || !innerStream.CanWrite)
			{
				throw new ArgumentException(SR.GetString("net_io_must_be_rw_stream"), "innerStream");
			}
			this._InnerStream = innerStream;
			this._LeaveStreamOpen = leaveInnerStreamOpen;
		}

		/// <summary>Gets whether the stream used by this <see cref="T:System.Net.Security.AuthenticatedStream" /> for sending and receiving data has been left open.</summary>
		/// <returns>
		///   <see langword="true" /> if the inner stream has been left open; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06001E81 RID: 7809 RVA: 0x0008FA94 File Offset: 0x0008DC94
		public bool LeaveInnerStreamOpen
		{
			get
			{
				return this._LeaveStreamOpen;
			}
		}

		/// <summary>Gets the stream used by this <see cref="T:System.Net.Security.AuthenticatedStream" /> for sending and receiving data.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object.</returns>
		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06001E82 RID: 7810 RVA: 0x0008FA9C File Offset: 0x0008DC9C
		protected Stream InnerStream
		{
			get
			{
				return this._InnerStream;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Security.AuthenticatedStream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06001E83 RID: 7811 RVA: 0x0008FAA4 File Offset: 0x0008DCA4
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (this._LeaveStreamOpen)
					{
						this._InnerStream.Flush();
					}
					else
					{
						this._InnerStream.Close();
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether authentication was successful.</summary>
		/// <returns>
		///   <see langword="true" /> if successful authentication occurred; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06001E84 RID: 7812
		public abstract bool IsAuthenticated { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether both server and client have been authenticated.</summary>
		/// <returns>
		///   <see langword="true" /> if the client and server have been authenticated; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06001E85 RID: 7813
		public abstract bool IsMutuallyAuthenticated { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether data sent using this <see cref="T:System.Net.Security.AuthenticatedStream" /> is encrypted.</summary>
		/// <returns>
		///   <see langword="true" /> if data is encrypted before being transmitted over the network and decrypted when it reaches the remote endpoint; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06001E86 RID: 7814
		public abstract bool IsEncrypted { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the data sent using this stream is signed.</summary>
		/// <returns>
		///   <see langword="true" /> if the data is signed before being transmitted; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06001E87 RID: 7815
		public abstract bool IsSigned { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the local side of the connection was authenticated as the server.</summary>
		/// <returns>
		///   <see langword="true" /> if the local endpoint was authenticated as the server side of a client-server authenticated connection; <see langword="false" /> if the local endpoint was authenticated as the client.</returns>
		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06001E88 RID: 7816
		public abstract bool IsServer { get; }

		// Token: 0x04001CD7 RID: 7383
		private Stream _InnerStream;

		// Token: 0x04001CD8 RID: 7384
		private bool _LeaveStreamOpen;
	}
}
