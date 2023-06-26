using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Lifetime
{
	/// <summary>Defines a lifetime lease object that is used by the remoting lifetime service.</summary>
	// Token: 0x0200081C RID: 2076
	[ComVisible(true)]
	public interface ILease
	{
		/// <summary>Registers a sponsor for the lease, and renews it by the specified <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="obj">The callback object of the sponsor.</param>
		/// <param name="renewalTime">The length of time to renew the lease by.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x06005934 RID: 22836
		[SecurityCritical]
		void Register(ISponsor obj, TimeSpan renewalTime);

		/// <summary>Registers a sponsor for the lease without renewing the lease.</summary>
		/// <param name="obj">The callback object of the sponsor.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x06005935 RID: 22837
		[SecurityCritical]
		void Register(ISponsor obj);

		/// <summary>Removes a sponsor from the sponsor list.</summary>
		/// <param name="obj">The lease sponsor to unregister.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x06005936 RID: 22838
		[SecurityCritical]
		void Unregister(ISponsor obj);

		/// <summary>Renews a lease for the specified time.</summary>
		/// <param name="renewalTime">The length of time to renew the lease by.</param>
		/// <returns>The new expiration time of the lease.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x06005937 RID: 22839
		[SecurityCritical]
		TimeSpan Renew(TimeSpan renewalTime);

		/// <summary>Gets or sets the amount of time by which a call to the remote object renews the <see cref="P:System.Runtime.Remoting.Lifetime.ILease.CurrentLeaseTime" />.</summary>
		/// <returns>The amount of time by which a call to the remote object renews the <see cref="P:System.Runtime.Remoting.Lifetime.ILease.CurrentLeaseTime" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x06005938 RID: 22840
		// (set) Token: 0x06005939 RID: 22841
		TimeSpan RenewOnCallTime
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}

		/// <summary>Gets or sets the amount of time to wait for a sponsor to return with a lease renewal time.</summary>
		/// <returns>The amount of time to wait for a sponsor to return with a lease renewal time.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x0600593A RID: 22842
		// (set) Token: 0x0600593B RID: 22843
		TimeSpan SponsorshipTimeout
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}

		/// <summary>Gets or sets the initial time for the lease.</summary>
		/// <returns>The initial time for the lease.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x0600593C RID: 22844
		// (set) Token: 0x0600593D RID: 22845
		TimeSpan InitialLeaseTime
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}

		/// <summary>Gets the amount of time remaining on the lease.</summary>
		/// <returns>The amount of time remaining on the lease.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x0600593E RID: 22846
		TimeSpan CurrentLeaseTime
		{
			[SecurityCritical]
			get;
		}

		/// <summary>Gets the current <see cref="T:System.Runtime.Remoting.Lifetime.LeaseState" /> of the lease.</summary>
		/// <returns>The current <see cref="T:System.Runtime.Remoting.Lifetime.LeaseState" /> of the lease.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x0600593F RID: 22847
		LeaseState CurrentState
		{
			[SecurityCritical]
			get;
		}
	}
}
