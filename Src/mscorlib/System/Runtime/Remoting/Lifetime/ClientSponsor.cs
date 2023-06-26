using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Lifetime
{
	/// <summary>Provides a default implementation for a lifetime sponsor class.</summary>
	// Token: 0x0200081B RID: 2075
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public class ClientSponsor : MarshalByRefObject, ISponsor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" /> class with default values.</summary>
		// Token: 0x0600592A RID: 22826 RVA: 0x0013B3EE File Offset: 0x001395EE
		public ClientSponsor()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" /> class with the renewal time of the sponsored object.</summary>
		/// <param name="renewalTime">The <see cref="T:System.TimeSpan" /> by which to increase the lifetime of the sponsored objects when renewal is requested.</param>
		// Token: 0x0600592B RID: 22827 RVA: 0x0013B417 File Offset: 0x00139617
		public ClientSponsor(TimeSpan renewalTime)
		{
			this.m_renewalTime = renewalTime;
		}

		/// <summary>Gets or sets the <see cref="T:System.TimeSpan" /> by which to increase the lifetime of the sponsored objects when renewal is requested.</summary>
		/// <returns>The <see cref="T:System.TimeSpan" /> by which to increase the lifetime of the sponsored objects when renewal is requested.</returns>
		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x0600592C RID: 22828 RVA: 0x0013B447 File Offset: 0x00139647
		// (set) Token: 0x0600592D RID: 22829 RVA: 0x0013B44F File Offset: 0x0013964F
		public TimeSpan RenewalTime
		{
			get
			{
				return this.m_renewalTime;
			}
			set
			{
				this.m_renewalTime = value;
			}
		}

		/// <summary>Registers the specified <see cref="T:System.MarshalByRefObject" /> for sponsorship.</summary>
		/// <param name="obj">The object to register for sponsorship with the <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" />.</param>
		/// <returns>
		///   <see langword="true" /> if registration succeeded; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600592E RID: 22830 RVA: 0x0013B458 File Offset: 0x00139658
		[SecurityCritical]
		public bool Register(MarshalByRefObject obj)
		{
			ILease lease = (ILease)obj.GetLifetimeService();
			if (lease == null)
			{
				return false;
			}
			lease.Register(this);
			Hashtable hashtable = this.sponsorTable;
			lock (hashtable)
			{
				this.sponsorTable[obj] = lease;
			}
			return true;
		}

		/// <summary>Unregisters the specified <see cref="T:System.MarshalByRefObject" /> from the list of objects sponsored by the current <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" />.</summary>
		/// <param name="obj">The object to unregister.</param>
		// Token: 0x0600592F RID: 22831 RVA: 0x0013B4B8 File Offset: 0x001396B8
		[SecurityCritical]
		public void Unregister(MarshalByRefObject obj)
		{
			ILease lease = null;
			Hashtable hashtable = this.sponsorTable;
			lock (hashtable)
			{
				lease = (ILease)this.sponsorTable[obj];
			}
			if (lease != null)
			{
				lease.Unregister(this);
			}
		}

		/// <summary>Requests a sponsoring client to renew the lease for the specified object.</summary>
		/// <param name="lease">The lifetime lease of the object that requires lease renewal.</param>
		/// <returns>The additional lease time for the specified object.</returns>
		// Token: 0x06005930 RID: 22832 RVA: 0x0013B510 File Offset: 0x00139710
		[SecurityCritical]
		public TimeSpan Renewal(ILease lease)
		{
			return this.m_renewalTime;
		}

		/// <summary>Empties the list objects registered with the current <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" />.</summary>
		// Token: 0x06005931 RID: 22833 RVA: 0x0013B518 File Offset: 0x00139718
		[SecurityCritical]
		public void Close()
		{
			Hashtable hashtable = this.sponsorTable;
			lock (hashtable)
			{
				IDictionaryEnumerator enumerator = this.sponsorTable.GetEnumerator();
				while (enumerator.MoveNext())
				{
					((ILease)enumerator.Value).Unregister(this);
				}
				this.sponsorTable.Clear();
			}
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" />, providing a lease for the current object.</summary>
		/// <returns>An <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> for the current object.</returns>
		// Token: 0x06005932 RID: 22834 RVA: 0x0013B584 File Offset: 0x00139784
		[SecurityCritical]
		public override object InitializeLifetimeService()
		{
			return null;
		}

		/// <summary>Frees the resources of the current <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" /> before the garbage collector reclaims them.</summary>
		// Token: 0x06005933 RID: 22835 RVA: 0x0013B588 File Offset: 0x00139788
		[SecuritySafeCritical]
		~ClientSponsor()
		{
		}

		// Token: 0x040028A2 RID: 10402
		private Hashtable sponsorTable = new Hashtable(10);

		// Token: 0x040028A3 RID: 10403
		private TimeSpan m_renewalTime = TimeSpan.FromMinutes(2.0);
	}
}
