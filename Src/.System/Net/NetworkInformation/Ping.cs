using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.NetworkInformation
{
	/// <summary>Allows an application to determine whether a remote computer is accessible over the network.</summary>
	// Token: 0x020002EA RID: 746
	public class Ping : Component
	{
		/// <summary>Occurs when an asynchronous operation to send an Internet Control Message Protocol (ICMP) echo message and receive the corresponding ICMP echo reply message completes or is canceled.</summary>
		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06001A12 RID: 6674 RVA: 0x0007E92C File Offset: 0x0007CB2C
		// (remove) Token: 0x06001A13 RID: 6675 RVA: 0x0007E964 File Offset: 0x0007CB64
		public event PingCompletedEventHandler PingCompleted;

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001A14 RID: 6676 RVA: 0x0007E999 File Offset: 0x0007CB99
		// (set) Token: 0x06001A15 RID: 6677 RVA: 0x0007E9B4 File Offset: 0x0007CBB4
		private bool InAsyncCall
		{
			get
			{
				return this.asyncFinished != null && !this.asyncFinished.WaitOne(0);
			}
			set
			{
				if (this.asyncFinished == null)
				{
					this.asyncFinished = new ManualResetEvent(!value);
					return;
				}
				if (value)
				{
					this.asyncFinished.Reset();
					return;
				}
				this.asyncFinished.Set();
			}
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x0007E9EC File Offset: 0x0007CBEC
		private void CheckStart(bool async)
		{
			if (this.disposeRequested)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			int num = Interlocked.CompareExchange(ref this.status, 1, 0);
			if (num == 1)
			{
				throw new InvalidOperationException(SR.GetString("net_inasync"));
			}
			if (num == 2)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (async)
			{
				this.InAsyncCall = true;
			}
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x0007EA53 File Offset: 0x0007CC53
		private void Finish(bool async)
		{
			this.status = 0;
			if (async)
			{
				this.InAsyncCall = false;
			}
			if (this.disposeRequested)
			{
				this.InternalDispose();
			}
		}

		/// <summary>Raises the <see cref="E:System.Net.NetworkInformation.Ping.PingCompleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.NetworkInformation.PingCompletedEventArgs" /> object that contains event data.</param>
		// Token: 0x06001A18 RID: 6680 RVA: 0x0007EA74 File Offset: 0x0007CC74
		protected void OnPingCompleted(PingCompletedEventArgs e)
		{
			if (this.PingCompleted != null)
			{
				this.PingCompleted(this, e);
			}
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x0007EA8B File Offset: 0x0007CC8B
		private void PingCompletedWaitCallback(object operationState)
		{
			this.OnPingCompleted((PingCompletedEventArgs)operationState);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.Ping" /> class.</summary>
		// Token: 0x06001A1A RID: 6682 RVA: 0x0007EA99 File Offset: 0x0007CC99
		public Ping()
		{
			this.onPingCompletedDelegate = new SendOrPostCallback(this.PingCompletedWaitCallback);
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x0007EAC0 File Offset: 0x0007CCC0
		private void InternalDispose()
		{
			this.disposeRequested = true;
			if (Interlocked.CompareExchange(ref this.status, 2, 0) != 0)
			{
				return;
			}
			if (this.handlePingV4 != null)
			{
				this.handlePingV4.Close();
				this.handlePingV4 = null;
			}
			if (this.handlePingV6 != null)
			{
				this.handlePingV6.Close();
				this.handlePingV6 = null;
			}
			this.UnregisterWaitHandle();
			if (this.pingEvent != null)
			{
				this.pingEvent.Close();
				this.pingEvent = null;
			}
			if (this.replyBuffer != null)
			{
				this.replyBuffer.Close();
				this.replyBuffer = null;
			}
			if (this.asyncFinished != null)
			{
				this.asyncFinished.Close();
				this.asyncFinished = null;
			}
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x0007EB6C File Offset: 0x0007CD6C
		private void UnregisterWaitHandle()
		{
			object obj = this.lockObject;
			lock (obj)
			{
				if (this.registeredWait != null)
				{
					this.registeredWait.Unregister(null);
					this.registeredWait = null;
				}
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.NetworkInformation.Ping" /> object, and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to releases only unmanaged resources.</param>
		// Token: 0x06001A1D RID: 6685 RVA: 0x0007EBC4 File Offset: 0x0007CDC4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.InternalDispose();
			}
			base.Dispose(disposing);
		}

		/// <summary>Cancels all pending asynchronous requests to send an Internet Control Message Protocol (ICMP) echo message and receives a corresponding ICMP echo reply message.</summary>
		// Token: 0x06001A1E RID: 6686 RVA: 0x0007EBD8 File Offset: 0x0007CDD8
		public void SendAsyncCancel()
		{
			object obj = this.lockObject;
			lock (obj)
			{
				if (!this.InAsyncCall)
				{
					return;
				}
				this.cancelled = true;
			}
			this.asyncFinished.WaitOne();
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x0007EC30 File Offset: 0x0007CE30
		private static void PingCallback(object state, bool signaled)
		{
			Ping ping = (Ping)state;
			PingCompletedEventArgs pingCompletedEventArgs = null;
			AsyncOperation asyncOperation = null;
			SendOrPostCallback sendOrPostCallback = null;
			try
			{
				object obj = ping.lockObject;
				lock (obj)
				{
					bool flag2 = ping.cancelled;
					asyncOperation = ping.asyncOp;
					sendOrPostCallback = ping.onPingCompletedDelegate;
					if (!flag2)
					{
						SafeLocalFree safeLocalFree = ping.replyBuffer;
						PingReply pingReply;
						if (ping.ipv6)
						{
							Icmp6EchoReply icmp6EchoReply = (Icmp6EchoReply)Marshal.PtrToStructure(safeLocalFree.DangerousGetHandle(), typeof(Icmp6EchoReply));
							pingReply = new PingReply(icmp6EchoReply, safeLocalFree.DangerousGetHandle(), ping.sendSize);
						}
						else
						{
							IcmpEchoReply icmpEchoReply = (IcmpEchoReply)Marshal.PtrToStructure(safeLocalFree.DangerousGetHandle(), typeof(IcmpEchoReply));
							pingReply = new PingReply(icmpEchoReply);
						}
						pingCompletedEventArgs = new PingCompletedEventArgs(pingReply, null, false, asyncOperation.UserSuppliedState);
					}
					else
					{
						pingCompletedEventArgs = new PingCompletedEventArgs(null, null, true, asyncOperation.UserSuppliedState);
					}
				}
			}
			catch (Exception ex)
			{
				PingException ex2 = new PingException(SR.GetString("net_ping"), ex);
				pingCompletedEventArgs = new PingCompletedEventArgs(null, ex2, false, asyncOperation.UserSuppliedState);
			}
			finally
			{
				ping.FreeUnmanagedStructures();
				ping.UnregisterWaitHandle();
				ping.Finish(true);
			}
			asyncOperation.PostOperationCompleted(sendOrPostCallback, pingCompletedEventArgs);
		}

		/// <summary>Attempts to send an Internet Control Message Protocol (ICMP) echo message to the specified computer, and receive a corresponding ICMP echo reply message from that computer.</summary>
		/// <param name="hostNameOrAddress">A <see cref="T:System.String" /> that identifies the computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingReply" /> object that provides information about the ICMP echo reply message, if one was received, or provides the reason for the failure, if no message was received.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is <see langword="null" /> or is an empty string ("").</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" /> is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000.</exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x06001A20 RID: 6688 RVA: 0x0007ED84 File Offset: 0x0007CF84
		public PingReply Send(string hostNameOrAddress)
		{
			return this.Send(hostNameOrAddress, 5000, this.DefaultSendBuffer, null);
		}

		/// <summary>Attempts to send an Internet Control Message Protocol (ICMP) echo message to the specified computer, and receive a corresponding ICMP echo reply message from that computer. This method allows you to specify a time-out value for the operation.</summary>
		/// <param name="hostNameOrAddress">A <see cref="T:System.String" /> that identifies the computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingReply" /> object that provides information about the ICMP echo reply message if one was received, or provides the reason for the failure if no message was received.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is <see langword="null" /> or is an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" /> is in progress.</exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x06001A21 RID: 6689 RVA: 0x0007ED99 File Offset: 0x0007CF99
		public PingReply Send(string hostNameOrAddress, int timeout)
		{
			return this.Send(hostNameOrAddress, timeout, this.DefaultSendBuffer, null);
		}

		/// <summary>Attempts to send an Internet Control Message Protocol (ICMP) echo message to the computer that has the specified <see cref="T:System.Net.IPAddress" />, and receive a corresponding ICMP echo reply message from that computer.</summary>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" /> that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingReply" /> object that provides information about the ICMP echo reply message, if one was received, or describes the reason for the failure if no message was received.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" /> is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000.</exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x06001A22 RID: 6690 RVA: 0x0007EDAA File Offset: 0x0007CFAA
		public PingReply Send(IPAddress address)
		{
			return this.Send(address, 5000, this.DefaultSendBuffer, null);
		}

		/// <summary>Attempts to send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the computer that has the specified <see cref="T:System.Net.IPAddress" />, and receive a corresponding ICMP echo reply message from that computer. This method allows you to specify a time-out value for the operation.</summary>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" /> that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingReply" /> object that provides information about the ICMP echo reply message if one was received, or provides the reason for the failure if no message was received.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" /> is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000.</exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x06001A23 RID: 6691 RVA: 0x0007EDBF File Offset: 0x0007CFBF
		public PingReply Send(IPAddress address, int timeout)
		{
			return this.Send(address, timeout, this.DefaultSendBuffer, null);
		}

		/// <summary>Attempts to send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the specified computer, and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation.</summary>
		/// <param name="hostNameOrAddress">A <see cref="T:System.String" /> that identifies the computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingReply" /> object that provides information about the ICMP echo reply message if one was received, or provides the reason for the failure if no message was received.</returns>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="buffer" /> exceeds 65500 bytes.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is <see langword="null" /> or is an empty string ("").  
		/// -or-  
		/// <paramref name="buffer" /> is <see langword="null" />, or the <paramref name="buffer" /> size is greater than 65500 bytes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" /> is in progress.</exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x06001A24 RID: 6692 RVA: 0x0007EDD0 File Offset: 0x0007CFD0
		public PingReply Send(string hostNameOrAddress, int timeout, byte[] buffer)
		{
			return this.Send(hostNameOrAddress, timeout, buffer, null);
		}

		/// <summary>Attempts to send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the computer that has the specified <see cref="T:System.Net.IPAddress" />, and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation.</summary>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" /> that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingReply" /> object that provides information about the ICMP echo reply message, if one was received, or provides the reason for the failure, if no message was received. The method will return <see cref="F:System.Net.NetworkInformation.IPStatus.PacketTooBig" /> if the packet exceeds the Maximum Transmission Unit (MTU).</returns>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="buffer" /> exceeds 65500 bytes.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="buffer" /> is <see langword="null" />, or the <paramref name="buffer" /> size is greater than 65500 bytes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" /> is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000.</exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x06001A25 RID: 6693 RVA: 0x0007EDDC File Offset: 0x0007CFDC
		public PingReply Send(IPAddress address, int timeout, byte[] buffer)
		{
			return this.Send(address, timeout, buffer, null);
		}

		/// <summary>Attempts to send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the specified computer, and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation and control fragmentation and Time-to-Live values for the ICMP packet.</summary>
		/// <param name="hostNameOrAddress">A <see cref="T:System.String" /> that identifies the computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <param name="options">A <see cref="T:System.Net.NetworkInformation.PingOptions" /> object used to control fragmentation and Time-to-Live values for the ICMP echo message packet.</param>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingReply" /> object that provides information about the ICMP echo reply message if one was received, or provides the reason for the failure if no message was received.</returns>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="buffer" /> exceeds 65500 bytes.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is <see langword="null" /> or is a zero length string.  
		/// -or-  
		/// <paramref name="buffer" /> is <see langword="null" />, or the <paramref name="buffer" /> size is greater than 65500 bytes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" /> is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000.</exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x06001A26 RID: 6694 RVA: 0x0007EDE8 File Offset: 0x0007CFE8
		public PingReply Send(string hostNameOrAddress, int timeout, byte[] buffer, PingOptions options)
		{
			if (ValidationHelper.IsBlankString(hostNameOrAddress))
			{
				throw new ArgumentNullException("hostNameOrAddress");
			}
			IPAddress ipaddress;
			if (!IPAddress.TryParse(hostNameOrAddress, out ipaddress))
			{
				try
				{
					ipaddress = Dns.GetHostAddresses(hostNameOrAddress)[0];
				}
				catch (ArgumentException)
				{
					throw;
				}
				catch (Exception ex)
				{
					throw new PingException(SR.GetString("net_ping"), ex);
				}
			}
			return this.Send(ipaddress, timeout, buffer, options);
		}

		/// <summary>Attempts to send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the computer that has the specified <see cref="T:System.Net.IPAddress" /> and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation and control fragmentation and Time-to-Live values for the ICMP echo message packet.</summary>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" /> that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <param name="options">A <see cref="T:System.Net.NetworkInformation.PingOptions" /> object used to control fragmentation and Time-to-Live values for the ICMP echo message packet.</param>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingReply" /> object that provides information about the ICMP echo reply message, if one was received, or provides the reason for the failure, if no message was received. The method will return <see cref="F:System.Net.NetworkInformation.IPStatus.PacketTooBig" /> if the packet exceeds the Maximum Transmission Unit (MTU).</returns>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="buffer" /> exceeds 65500 bytes.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="buffer" /> is <see langword="null" />, or the <paramref name="buffer" /> size is greater than 65500 bytes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" /> is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000.</exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x06001A27 RID: 6695 RVA: 0x0007EE5C File Offset: 0x0007D05C
		public PingReply Send(IPAddress address, int timeout, byte[] buffer, PingOptions options)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (buffer.Length > 65500)
			{
				throw new ArgumentException(SR.GetString("net_invalidPingBufferSize"), "buffer");
			}
			if (timeout < 0)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			this.TestIsIpSupported(address);
			if (address.Equals(IPAddress.Any) || address.Equals(IPAddress.IPv6Any))
			{
				throw new ArgumentException(SR.GetString("net_invalid_ip_addr"), "address");
			}
			IPAddress ipaddress;
			if (address.AddressFamily == AddressFamily.InterNetwork)
			{
				ipaddress = new IPAddress(address.GetAddressBytes());
			}
			else
			{
				ipaddress = new IPAddress(address.GetAddressBytes(), address.ScopeId);
			}
			new NetworkInformationPermission(NetworkInformationAccess.Ping).Demand();
			this.CheckStart(false);
			PingReply pingReply;
			try
			{
				pingReply = this.InternalSend(ipaddress, buffer, timeout, options, false);
			}
			catch (Exception ex)
			{
				throw new PingException(SR.GetString("net_ping"), ex);
			}
			finally
			{
				this.Finish(false);
			}
			return pingReply;
		}

		/// <summary>Asynchronously attempts to send an Internet Control Message Protocol (ICMP) echo message to the specified computer, and receive a corresponding ICMP echo reply message from that computer.</summary>
		/// <param name="hostNameOrAddress">A <see cref="T:System.String" /> that identifies the computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <param name="userToken">An object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is <see langword="null" /> or is an empty string ("").</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="M:System.Net.NetworkInformation.Ping.SendAsync(System.String,System.Object)" /> method is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000.</exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="hostNameOrAddress" /> could not be resolved to a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x06001A28 RID: 6696 RVA: 0x0007EF6C File Offset: 0x0007D16C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void SendAsync(string hostNameOrAddress, object userToken)
		{
			this.SendAsync(hostNameOrAddress, 5000, this.DefaultSendBuffer, userToken);
		}

		/// <summary>Asynchronously attempts to send an Internet Control Message Protocol (ICMP) echo message to the specified computer, and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation.</summary>
		/// <param name="hostNameOrAddress">A <see cref="T:System.String" /> that identifies the computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="userToken">An object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is <see langword="null" /> or is an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" /> is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="hostNameOrAddress" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000.</exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="hostNameOrAddress" /> could not be resolved to a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x06001A29 RID: 6697 RVA: 0x0007EF81 File Offset: 0x0007D181
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void SendAsync(string hostNameOrAddress, int timeout, object userToken)
		{
			this.SendAsync(hostNameOrAddress, timeout, this.DefaultSendBuffer, userToken);
		}

		/// <summary>Asynchronously attempts to send an Internet Control Message Protocol (ICMP) echo message to the computer that has the specified <see cref="T:System.Net.IPAddress" />, and receive a corresponding ICMP echo reply message from that computer.</summary>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" /> that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <param name="userToken">An object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to the <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" /> method is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000.</exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="address" /> is not a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x06001A2A RID: 6698 RVA: 0x0007EF92 File Offset: 0x0007D192
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void SendAsync(IPAddress address, object userToken)
		{
			this.SendAsync(address, 5000, this.DefaultSendBuffer, userToken);
		}

		/// <summary>Asynchronously attempts to send an Internet Control Message Protocol (ICMP) echo message to the computer that has the specified <see cref="T:System.Net.IPAddress" />, and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation.</summary>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" /> that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="userToken">An object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="M:System.Net.NetworkInformation.Ping.SendAsync(System.Net.IPAddress,System.Int32,System.Byte[],System.Object)" /> method is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000.</exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="address" /> is not a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x06001A2B RID: 6699 RVA: 0x0007EFA7 File Offset: 0x0007D1A7
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void SendAsync(IPAddress address, int timeout, object userToken)
		{
			this.SendAsync(address, timeout, this.DefaultSendBuffer, userToken);
		}

		/// <summary>Asynchronously attempts to send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the specified computer, and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation.</summary>
		/// <param name="hostNameOrAddress">A <see cref="T:System.String" /> that identifies the computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <param name="userToken">An object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is <see langword="null" /> or is an empty string ("").  
		/// -or-  
		/// <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" /> is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="hostNameOrAddress" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000.</exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="hostNameOrAddress" /> could not be resolved to a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="buffer" /> exceeds 65500 bytes.</exception>
		// Token: 0x06001A2C RID: 6700 RVA: 0x0007EFB8 File Offset: 0x0007D1B8
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void SendAsync(string hostNameOrAddress, int timeout, byte[] buffer, object userToken)
		{
			this.SendAsync(hostNameOrAddress, timeout, buffer, null, userToken);
		}

		/// <summary>Asynchronously attempts to send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the computer that has the specified <see cref="T:System.Net.IPAddress" />, and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation.</summary>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" /> that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <param name="userToken">An object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" /> is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000.</exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="address" /> is not a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="buffer" /> exceeds 65500 bytes.</exception>
		// Token: 0x06001A2D RID: 6701 RVA: 0x0007EFC6 File Offset: 0x0007D1C6
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void SendAsync(IPAddress address, int timeout, byte[] buffer, object userToken)
		{
			this.SendAsync(address, timeout, buffer, null, userToken);
		}

		/// <summary>Asynchronously attempts to send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the specified computer, and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation and control fragmentation and Time-to-Live values for the ICMP packet.</summary>
		/// <param name="hostNameOrAddress">A <see cref="T:System.String" /> that identifies the computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <param name="timeout">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <param name="buffer">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="options">A <see cref="T:System.Net.NetworkInformation.PingOptions" /> object used to control fragmentation and Time-to-Live values for the ICMP echo message packet.</param>
		/// <param name="userToken">An object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is <see langword="null" /> or is an empty string ("").  
		/// -or-  
		/// <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" /> is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000.</exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="hostNameOrAddress" /> could not be resolved to a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="buffer" /> exceeds 65500 bytes.</exception>
		// Token: 0x06001A2E RID: 6702 RVA: 0x0007EFD4 File Offset: 0x0007D1D4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void SendAsync(string hostNameOrAddress, int timeout, byte[] buffer, PingOptions options, object userToken)
		{
			if (ValidationHelper.IsBlankString(hostNameOrAddress))
			{
				throw new ArgumentNullException("hostNameOrAddress");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (buffer.Length > 65500)
			{
				throw new ArgumentException(SR.GetString("net_invalidPingBufferSize"), "buffer");
			}
			if (timeout < 0)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			IPAddress ipaddress;
			if (IPAddress.TryParse(hostNameOrAddress, out ipaddress))
			{
				this.SendAsync(ipaddress, timeout, buffer, options, userToken);
				return;
			}
			this.CheckStart(true);
			try
			{
				this.cancelled = false;
				this.asyncOp = AsyncOperationManager.CreateOperation(userToken);
				Ping.AsyncStateObject asyncStateObject = new Ping.AsyncStateObject(hostNameOrAddress, buffer, timeout, options, userToken);
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.ContinueAsyncSend), asyncStateObject);
			}
			catch (Exception ex)
			{
				this.Finish(true);
				throw new PingException(SR.GetString("net_ping"), ex);
			}
		}

		/// <summary>Asynchronously attempts to send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the computer that has the specified <see cref="T:System.Net.IPAddress" />, and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation and control fragmentation and Time-to-Live values for the ICMP echo message packet.</summary>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" /> that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <param name="options">A <see cref="T:System.Net.NetworkInformation.PingOptions" /> object used to control fragmentation and Time-to-Live values for the ICMP echo message packet.</param>
		/// <param name="userToken">An object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" /> is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000.</exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="address" /> is not a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="buffer" /> exceeds 65500 bytes.</exception>
		// Token: 0x06001A2F RID: 6703 RVA: 0x0007F0B0 File Offset: 0x0007D2B0
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void SendAsync(IPAddress address, int timeout, byte[] buffer, PingOptions options, object userToken)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (buffer.Length > 65500)
			{
				throw new ArgumentException(SR.GetString("net_invalidPingBufferSize"), "buffer");
			}
			if (timeout < 0)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			this.TestIsIpSupported(address);
			if (address.Equals(IPAddress.Any) || address.Equals(IPAddress.IPv6Any))
			{
				throw new ArgumentException(SR.GetString("net_invalid_ip_addr"), "address");
			}
			IPAddress ipaddress;
			if (address.AddressFamily == AddressFamily.InterNetwork)
			{
				ipaddress = new IPAddress(address.GetAddressBytes());
			}
			else
			{
				ipaddress = new IPAddress(address.GetAddressBytes(), address.ScopeId);
			}
			new NetworkInformationPermission(NetworkInformationAccess.Ping).Demand();
			this.CheckStart(true);
			try
			{
				this.cancelled = false;
				this.asyncOp = AsyncOperationManager.CreateOperation(userToken);
				this.InternalSend(ipaddress, buffer, timeout, options, true);
			}
			catch (Exception ex)
			{
				this.Finish(true);
				throw new PingException(SR.GetString("net_ping"), ex);
			}
		}

		/// <summary>Send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the computer that has the specified <see cref="T:System.Net.IPAddress" />, and receives a corresponding ICMP echo reply message from that computer as an asynchronous operation.</summary>
		/// <param name="address">An IP address that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendPingAsync" /> is in progress.</exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="address" /> is not a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x06001A30 RID: 6704 RVA: 0x0007F1C4 File Offset: 0x0007D3C4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<PingReply> SendPingAsync(IPAddress address)
		{
			return this.SendPingAsyncCore(delegate(TaskCompletionSource<PingReply> tcs)
			{
				this.SendAsync(address, tcs);
			});
		}

		/// <summary>Sends an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the specified computer, and receive a corresponding ICMP echo reply message from that computer as an asynchronous operation.</summary>
		/// <param name="hostNameOrAddress">The computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x06001A31 RID: 6705 RVA: 0x0007F1F8 File Offset: 0x0007D3F8
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<PingReply> SendPingAsync(string hostNameOrAddress)
		{
			return this.SendPingAsyncCore(delegate(TaskCompletionSource<PingReply> tcs)
			{
				this.SendAsync(hostNameOrAddress, tcs);
			});
		}

		/// <summary>Send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the computer that has the specified <see cref="T:System.Net.IPAddress" />, and receives a corresponding ICMP echo reply message from that computer as an asynchronous operation. This overload allows you to specify a time-out value for the operation.</summary>
		/// <param name="address">An IP address that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <param name="timeout">The maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x06001A32 RID: 6706 RVA: 0x0007F22C File Offset: 0x0007D42C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<PingReply> SendPingAsync(IPAddress address, int timeout)
		{
			return this.SendPingAsyncCore(delegate(TaskCompletionSource<PingReply> tcs)
			{
				this.SendAsync(address, timeout, tcs);
			});
		}

		/// <summary>Sends an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the specified computer, and receive a corresponding ICMP echo reply message from that computer as an asynchronous operation. This overload allows you to specify a time-out value for the operation.</summary>
		/// <param name="hostNameOrAddress">The computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <param name="timeout">The maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x06001A33 RID: 6707 RVA: 0x0007F268 File Offset: 0x0007D468
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<PingReply> SendPingAsync(string hostNameOrAddress, int timeout)
		{
			return this.SendPingAsyncCore(delegate(TaskCompletionSource<PingReply> tcs)
			{
				this.SendAsync(hostNameOrAddress, timeout, tcs);
			});
		}

		/// <summary>Send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the computer that has the specified <see cref="T:System.Net.IPAddress" />, and receives a corresponding ICMP echo reply message from that computer as an asynchronous operation. This overload allows you to specify a time-out value for the operation and a buffer to use for send and receive.</summary>
		/// <param name="address">An IP address that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <param name="timeout">The maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendPingAsync" /> is in progress.</exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="address" /> is not a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="buffer" /> exceeds 65,500 bytes.</exception>
		// Token: 0x06001A34 RID: 6708 RVA: 0x0007F2A4 File Offset: 0x0007D4A4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<PingReply> SendPingAsync(IPAddress address, int timeout, byte[] buffer)
		{
			return this.SendPingAsyncCore(delegate(TaskCompletionSource<PingReply> tcs)
			{
				this.SendAsync(address, timeout, buffer, tcs);
			});
		}

		/// <summary>Sends an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the specified computer, and receive a corresponding ICMP echo reply message from that computer as an asynchronous operation. This overload allows you to specify a time-out value for the operation and a buffer to use for send and receive.</summary>
		/// <param name="hostNameOrAddress">The computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <param name="timeout">The maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x06001A35 RID: 6709 RVA: 0x0007F2E8 File Offset: 0x0007D4E8
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<PingReply> SendPingAsync(string hostNameOrAddress, int timeout, byte[] buffer)
		{
			return this.SendPingAsyncCore(delegate(TaskCompletionSource<PingReply> tcs)
			{
				this.SendAsync(hostNameOrAddress, timeout, buffer, tcs);
			});
		}

		/// <summary>Sends an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the computer that has the specified <see cref="T:System.Net.IPAddress" />, and receives a corresponding ICMP echo reply message from that computer as an asynchronous operation. This overload allows you to specify a time-out value for the operation, a buffer to use for send and receive, and control fragmentation and Time-to-Live values for the ICMP echo message packet.</summary>
		/// <param name="address">An IP address that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <param name="timeout">The maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <param name="options">A <see cref="T:System.Net.NetworkInformation.PingOptions" /> object used to control fragmentation and Time-to-Live values for the ICMP echo message packet.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendPingAsync" /> is in progress.</exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="address" /> is not a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="buffer" /> exceeds 65,500 bytes.</exception>
		// Token: 0x06001A36 RID: 6710 RVA: 0x0007F32C File Offset: 0x0007D52C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<PingReply> SendPingAsync(IPAddress address, int timeout, byte[] buffer, PingOptions options)
		{
			return this.SendPingAsyncCore(delegate(TaskCompletionSource<PingReply> tcs)
			{
				this.SendAsync(address, timeout, buffer, options, tcs);
			});
		}

		/// <summary>Sends an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the specified computer, and receive a corresponding ICMP echo reply message from that computer as an asynchronous operation. This overload allows you to specify a time-out value for the operation, a buffer to use for send and receive, and control fragmentation and Time-to-Live values for the ICMP echo message packet.</summary>
		/// <param name="hostNameOrAddress">The computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <param name="timeout">The maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <param name="options">A <see cref="T:System.Net.NetworkInformation.PingOptions" /> object used to control fragmentation and Time-to-Live values for the ICMP echo message packet.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x06001A37 RID: 6711 RVA: 0x0007F378 File Offset: 0x0007D578
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<PingReply> SendPingAsync(string hostNameOrAddress, int timeout, byte[] buffer, PingOptions options)
		{
			return this.SendPingAsyncCore(delegate(TaskCompletionSource<PingReply> tcs)
			{
				this.SendAsync(hostNameOrAddress, timeout, buffer, options, tcs);
			});
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x0007F3C4 File Offset: 0x0007D5C4
		private Task<PingReply> SendPingAsyncCore(Action<TaskCompletionSource<PingReply>> sendAsync)
		{
			TaskCompletionSource<PingReply> tcs = new TaskCompletionSource<PingReply>();
			PingCompletedEventHandler handler = null;
			handler = delegate(object sender, PingCompletedEventArgs e)
			{
				this.HandleCompletion(tcs, e, handler);
			};
			this.PingCompleted += handler;
			try
			{
				sendAsync(tcs);
			}
			catch
			{
				this.PingCompleted -= handler;
				throw;
			}
			return tcs.Task;
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x0007F448 File Offset: 0x0007D648
		private void HandleCompletion(TaskCompletionSource<PingReply> tcs, PingCompletedEventArgs e, PingCompletedEventHandler handler)
		{
			if (e.UserState == tcs)
			{
				try
				{
					this.PingCompleted -= handler;
				}
				finally
				{
					if (e.Error != null)
					{
						tcs.TrySetException(e.Error);
					}
					else if (e.Cancelled)
					{
						tcs.TrySetCanceled();
					}
					else
					{
						tcs.TrySetResult(e.Reply);
					}
				}
			}
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x0007F4B0 File Offset: 0x0007D6B0
		private void ContinueAsyncSend(object state)
		{
			Ping.AsyncStateObject asyncStateObject = (Ping.AsyncStateObject)state;
			try
			{
				IPAddress ipaddress = Dns.GetHostAddresses(asyncStateObject.hostName)[0];
				new NetworkInformationPermission(NetworkInformationAccess.Ping).Demand();
				this.InternalSend(ipaddress, asyncStateObject.buffer, asyncStateObject.timeout, asyncStateObject.options, true);
			}
			catch (Exception ex)
			{
				PingException ex2 = new PingException(SR.GetString("net_ping"), ex);
				PingCompletedEventArgs pingCompletedEventArgs = new PingCompletedEventArgs(null, ex2, false, this.asyncOp.UserSuppliedState);
				this.Finish(true);
				this.asyncOp.PostOperationCompleted(this.onPingCompletedDelegate, pingCompletedEventArgs);
			}
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x0007F550 File Offset: 0x0007D750
		private PingReply InternalSend(IPAddress address, byte[] buffer, int timeout, PingOptions options, bool async)
		{
			this.ipv6 = address.AddressFamily == AddressFamily.InterNetworkV6;
			this.sendSize = buffer.Length;
			if (!this.ipv6 && this.handlePingV4 == null)
			{
				this.handlePingV4 = UnsafeNetInfoNativeMethods.IcmpCreateFile();
				if (this.handlePingV4.IsInvalid)
				{
					this.handlePingV4 = null;
					throw new Win32Exception();
				}
			}
			else if (this.ipv6 && this.handlePingV6 == null)
			{
				this.handlePingV6 = UnsafeNetInfoNativeMethods.Icmp6CreateFile();
				if (this.handlePingV6.IsInvalid)
				{
					this.handlePingV6 = null;
					throw new Win32Exception();
				}
			}
			IPOptions ipoptions = new IPOptions(options);
			if (this.replyBuffer == null)
			{
				this.replyBuffer = SafeLocalFree.LocalAlloc(65791);
			}
			int num;
			try
			{
				if (async)
				{
					if (this.pingEvent == null)
					{
						this.pingEvent = new ManualResetEvent(false);
					}
					else
					{
						this.pingEvent.Reset();
					}
					this.registeredWait = ThreadPool.RegisterWaitForSingleObject(this.pingEvent, new WaitOrTimerCallback(Ping.PingCallback), this, -1, true);
				}
				this.SetUnmanagedStructures(buffer);
				if (!this.ipv6)
				{
					if (async)
					{
						num = (int)UnsafeNetInfoNativeMethods.IcmpSendEcho2(this.handlePingV4, this.pingEvent.SafeWaitHandle, IntPtr.Zero, IntPtr.Zero, (uint)address.m_Address, this.requestBuffer, (ushort)buffer.Length, ref ipoptions, this.replyBuffer, 65791U, (uint)timeout);
					}
					else
					{
						num = (int)UnsafeNetInfoNativeMethods.IcmpSendEcho2(this.handlePingV4, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, (uint)address.m_Address, this.requestBuffer, (ushort)buffer.Length, ref ipoptions, this.replyBuffer, 65791U, (uint)timeout);
					}
				}
				else
				{
					IPEndPoint ipendPoint = new IPEndPoint(address, 0);
					SocketAddress socketAddress = ipendPoint.Serialize();
					byte[] array = new byte[28];
					if (async)
					{
						num = (int)UnsafeNetInfoNativeMethods.Icmp6SendEcho2(this.handlePingV6, this.pingEvent.SafeWaitHandle, IntPtr.Zero, IntPtr.Zero, array, socketAddress.m_Buffer, this.requestBuffer, (ushort)buffer.Length, ref ipoptions, this.replyBuffer, 65791U, (uint)timeout);
					}
					else
					{
						num = (int)UnsafeNetInfoNativeMethods.Icmp6SendEcho2(this.handlePingV6, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, array, socketAddress.m_Buffer, this.requestBuffer, (ushort)buffer.Length, ref ipoptions, this.replyBuffer, 65791U, (uint)timeout);
					}
				}
			}
			catch
			{
				this.UnregisterWaitHandle();
				throw;
			}
			if (num == 0)
			{
				num = Marshal.GetLastWin32Error();
				if (async && (long)num == 997L)
				{
					return null;
				}
				this.FreeUnmanagedStructures();
				this.UnregisterWaitHandle();
				if (async || num < 11002 || num > 11045)
				{
					throw new Win32Exception(num);
				}
				return new PingReply((IPStatus)num);
			}
			else
			{
				if (async)
				{
					return null;
				}
				this.FreeUnmanagedStructures();
				PingReply pingReply;
				if (this.ipv6)
				{
					Icmp6EchoReply icmp6EchoReply = (Icmp6EchoReply)Marshal.PtrToStructure(this.replyBuffer.DangerousGetHandle(), typeof(Icmp6EchoReply));
					pingReply = new PingReply(icmp6EchoReply, this.replyBuffer.DangerousGetHandle(), this.sendSize);
				}
				else
				{
					IcmpEchoReply icmpEchoReply = (IcmpEchoReply)Marshal.PtrToStructure(this.replyBuffer.DangerousGetHandle(), typeof(IcmpEchoReply));
					pingReply = new PingReply(icmpEchoReply);
				}
				GC.KeepAlive(this.replyBuffer);
				return pingReply;
			}
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x0007F878 File Offset: 0x0007DA78
		private void TestIsIpSupported(IPAddress ip)
		{
			if (ip.AddressFamily == AddressFamily.InterNetwork && !Socket.OSSupportsIPv4)
			{
				throw new NotSupportedException(SR.GetString("net_ipv4_not_installed"));
			}
			if (ip.AddressFamily == AddressFamily.InterNetworkV6 && !Socket.OSSupportsIPv6)
			{
				throw new NotSupportedException(SR.GetString("net_ipv6_not_installed"));
			}
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x0007F8C8 File Offset: 0x0007DAC8
		private unsafe void SetUnmanagedStructures(byte[] buffer)
		{
			this.requestBuffer = SafeLocalFree.LocalAlloc(buffer.Length);
			byte* ptr = (byte*)(void*)this.requestBuffer.DangerousGetHandle();
			for (int i = 0; i < buffer.Length; i++)
			{
				ptr[i] = buffer[i];
			}
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x0007F909 File Offset: 0x0007DB09
		private void FreeUnmanagedStructures()
		{
			if (this.requestBuffer != null)
			{
				this.requestBuffer.Close();
				this.requestBuffer = null;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001A3F RID: 6719 RVA: 0x0007F928 File Offset: 0x0007DB28
		private byte[] DefaultSendBuffer
		{
			get
			{
				if (this.defaultSendBuffer == null)
				{
					this.defaultSendBuffer = new byte[32];
					for (int i = 0; i < 32; i++)
					{
						this.defaultSendBuffer[i] = (byte)(97 + i % 23);
					}
				}
				return this.defaultSendBuffer;
			}
		}

		// Token: 0x04001A59 RID: 6745
		private const int MaxUdpPacket = 65791;

		// Token: 0x04001A5A RID: 6746
		private const int MaxBufferSize = 65500;

		// Token: 0x04001A5B RID: 6747
		private const int DefaultTimeout = 5000;

		// Token: 0x04001A5C RID: 6748
		private const int DefaultSendBufferSize = 32;

		// Token: 0x04001A5D RID: 6749
		private byte[] defaultSendBuffer;

		// Token: 0x04001A5E RID: 6750
		private bool ipv6;

		// Token: 0x04001A5F RID: 6751
		private bool cancelled;

		// Token: 0x04001A60 RID: 6752
		private bool disposeRequested;

		// Token: 0x04001A61 RID: 6753
		private object lockObject = new object();

		// Token: 0x04001A62 RID: 6754
		internal ManualResetEvent pingEvent;

		// Token: 0x04001A63 RID: 6755
		private RegisteredWaitHandle registeredWait;

		// Token: 0x04001A64 RID: 6756
		private SafeLocalFree requestBuffer;

		// Token: 0x04001A65 RID: 6757
		private SafeLocalFree replyBuffer;

		// Token: 0x04001A66 RID: 6758
		private int sendSize;

		// Token: 0x04001A67 RID: 6759
		private SafeCloseIcmpHandle handlePingV4;

		// Token: 0x04001A68 RID: 6760
		private SafeCloseIcmpHandle handlePingV6;

		// Token: 0x04001A69 RID: 6761
		private AsyncOperation asyncOp;

		// Token: 0x04001A6A RID: 6762
		private SendOrPostCallback onPingCompletedDelegate;

		// Token: 0x04001A6C RID: 6764
		private ManualResetEvent asyncFinished;

		// Token: 0x04001A6D RID: 6765
		private const int Free = 0;

		// Token: 0x04001A6E RID: 6766
		private const int InProgress = 1;

		// Token: 0x04001A6F RID: 6767
		private new const int Disposed = 2;

		// Token: 0x04001A70 RID: 6768
		private int status;

		// Token: 0x020007A5 RID: 1957
		internal class AsyncStateObject
		{
			// Token: 0x060042F9 RID: 17145 RVA: 0x00118EFC File Offset: 0x001170FC
			internal AsyncStateObject(string hostName, byte[] buffer, int timeout, PingOptions options, object userToken)
			{
				this.hostName = hostName;
				this.buffer = buffer;
				this.timeout = timeout;
				this.options = options;
				this.userToken = userToken;
			}

			// Token: 0x040033C4 RID: 13252
			internal byte[] buffer;

			// Token: 0x040033C5 RID: 13253
			internal string hostName;

			// Token: 0x040033C6 RID: 13254
			internal int timeout;

			// Token: 0x040033C7 RID: 13255
			internal PingOptions options;

			// Token: 0x040033C8 RID: 13256
			internal object userToken;
		}
	}
}
