using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Lifetime
{
	/// <summary>Controls the.NET remoting lifetime services.</summary>
	// Token: 0x02000822 RID: 2082
	[SecurityCritical]
	[ComVisible(true)]
	public sealed class LifetimeServices
	{
		// Token: 0x0600596E RID: 22894 RVA: 0x0013C474 File Offset: 0x0013A674
		private static TimeSpan GetTimeSpan(ref long ticks)
		{
			return TimeSpan.FromTicks(Volatile.Read(ref ticks));
		}

		// Token: 0x0600596F RID: 22895 RVA: 0x0013C481 File Offset: 0x0013A681
		private static void SetTimeSpan(ref long ticks, TimeSpan value)
		{
			Volatile.Write(ref ticks, value.Ticks);
		}

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x06005970 RID: 22896 RVA: 0x0013C490 File Offset: 0x0013A690
		private static object LifetimeSyncObject
		{
			get
			{
				if (LifetimeServices.s_LifetimeSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref LifetimeServices.s_LifetimeSyncObject, obj, null);
				}
				return LifetimeServices.s_LifetimeSyncObject;
			}
		}

		/// <summary>Creates an instance of <see cref="T:System.Runtime.Remoting.Lifetime.LifetimeServices" />.</summary>
		// Token: 0x06005971 RID: 22897 RVA: 0x0013C4BC File Offset: 0x0013A6BC
		[Obsolete("Do not create instances of the LifetimeServices class.  Call the static methods directly on this type instead", true)]
		public LifetimeServices()
		{
		}

		/// <summary>Gets or sets the initial lease time span for an <see cref="T:System.AppDomain" />.</summary>
		/// <returns>The initial lease <see cref="T:System.TimeSpan" /> for objects that can have leases in the <see cref="T:System.AppDomain" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. This exception is thrown only when setting the property value.</exception>
		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x06005972 RID: 22898 RVA: 0x0013C4C4 File Offset: 0x0013A6C4
		// (set) Token: 0x06005973 RID: 22899 RVA: 0x0013C4D0 File Offset: 0x0013A6D0
		public static TimeSpan LeaseTime
		{
			get
			{
				return LifetimeServices.GetTimeSpan(ref LifetimeServices.s_leaseTimeTicks);
			}
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
			set
			{
				object lifetimeSyncObject = LifetimeServices.LifetimeSyncObject;
				lock (lifetimeSyncObject)
				{
					if (LifetimeServices.s_isLeaseTime)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_Lifetime_SetOnce", new object[] { "LeaseTime" }));
					}
					LifetimeServices.SetTimeSpan(ref LifetimeServices.s_leaseTimeTicks, value);
					LifetimeServices.s_isLeaseTime = true;
				}
			}
		}

		/// <summary>Gets or sets the amount of time by which the lease is extended every time a call comes in on the server object.</summary>
		/// <returns>The <see cref="T:System.TimeSpan" /> by which a lifetime lease in the current <see cref="T:System.AppDomain" /> is extended after each call.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. This exception is thrown only when setting the property value.</exception>
		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x06005974 RID: 22900 RVA: 0x0013C540 File Offset: 0x0013A740
		// (set) Token: 0x06005975 RID: 22901 RVA: 0x0013C54C File Offset: 0x0013A74C
		public static TimeSpan RenewOnCallTime
		{
			get
			{
				return LifetimeServices.GetTimeSpan(ref LifetimeServices.s_renewOnCallTimeTicks);
			}
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
			set
			{
				object lifetimeSyncObject = LifetimeServices.LifetimeSyncObject;
				lock (lifetimeSyncObject)
				{
					if (LifetimeServices.s_isRenewOnCallTime)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_Lifetime_SetOnce", new object[] { "RenewOnCallTime" }));
					}
					LifetimeServices.SetTimeSpan(ref LifetimeServices.s_renewOnCallTimeTicks, value);
					LifetimeServices.s_isRenewOnCallTime = true;
				}
			}
		}

		/// <summary>Gets or sets the amount of time the lease manager waits for a sponsor to return with a lease renewal time.</summary>
		/// <returns>The initial sponsorship time-out.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. This exception is thrown only when setting the property value.</exception>
		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x06005976 RID: 22902 RVA: 0x0013C5BC File Offset: 0x0013A7BC
		// (set) Token: 0x06005977 RID: 22903 RVA: 0x0013C5C8 File Offset: 0x0013A7C8
		public static TimeSpan SponsorshipTimeout
		{
			get
			{
				return LifetimeServices.GetTimeSpan(ref LifetimeServices.s_sponsorshipTimeoutTicks);
			}
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
			set
			{
				object lifetimeSyncObject = LifetimeServices.LifetimeSyncObject;
				lock (lifetimeSyncObject)
				{
					if (LifetimeServices.s_isSponsorshipTimeout)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_Lifetime_SetOnce", new object[] { "SponsorshipTimeout" }));
					}
					LifetimeServices.SetTimeSpan(ref LifetimeServices.s_sponsorshipTimeoutTicks, value);
					LifetimeServices.s_isSponsorshipTimeout = true;
				}
			}
		}

		/// <summary>Gets or sets the time interval between each activation of the lease manager to clean up expired leases.</summary>
		/// <returns>The default amount of time the lease manager sleeps after checking for expired leases.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. This exception is thrown only when setting the property value.</exception>
		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x06005978 RID: 22904 RVA: 0x0013C638 File Offset: 0x0013A838
		// (set) Token: 0x06005979 RID: 22905 RVA: 0x0013C644 File Offset: 0x0013A844
		public static TimeSpan LeaseManagerPollTime
		{
			get
			{
				return LifetimeServices.GetTimeSpan(ref LifetimeServices.s_pollTimeTicks);
			}
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
			set
			{
				object lifetimeSyncObject = LifetimeServices.LifetimeSyncObject;
				lock (lifetimeSyncObject)
				{
					LifetimeServices.SetTimeSpan(ref LifetimeServices.s_pollTimeTicks, value);
					if (LeaseManager.IsInitialized())
					{
						LeaseManager.GetLeaseManager().ChangePollTime(value);
					}
				}
			}
		}

		// Token: 0x0600597A RID: 22906 RVA: 0x0013C69C File Offset: 0x0013A89C
		[SecurityCritical]
		internal static ILease GetLeaseInitial(MarshalByRefObject obj)
		{
			LeaseManager leaseManager = LeaseManager.GetLeaseManager(LifetimeServices.LeaseManagerPollTime);
			ILease lease = leaseManager.GetLease(obj);
			if (lease == null)
			{
				lease = LifetimeServices.CreateLease(obj);
			}
			return lease;
		}

		// Token: 0x0600597B RID: 22907 RVA: 0x0013C6CC File Offset: 0x0013A8CC
		[SecurityCritical]
		internal static ILease GetLease(MarshalByRefObject obj)
		{
			LeaseManager leaseManager = LeaseManager.GetLeaseManager(LifetimeServices.LeaseManagerPollTime);
			return leaseManager.GetLease(obj);
		}

		// Token: 0x0600597C RID: 22908 RVA: 0x0013C6EF File Offset: 0x0013A8EF
		[SecurityCritical]
		internal static ILease CreateLease(MarshalByRefObject obj)
		{
			return LifetimeServices.CreateLease(LifetimeServices.LeaseTime, LifetimeServices.RenewOnCallTime, LifetimeServices.SponsorshipTimeout, obj);
		}

		// Token: 0x0600597D RID: 22909 RVA: 0x0013C706 File Offset: 0x0013A906
		[SecurityCritical]
		internal static ILease CreateLease(TimeSpan leaseTime, TimeSpan renewOnCallTime, TimeSpan sponsorshipTimeout, MarshalByRefObject obj)
		{
			LeaseManager.GetLeaseManager(LifetimeServices.LeaseManagerPollTime);
			return new Lease(leaseTime, renewOnCallTime, sponsorshipTimeout, obj);
		}

		// Token: 0x040028BE RID: 10430
		private static bool s_isLeaseTime = false;

		// Token: 0x040028BF RID: 10431
		private static bool s_isRenewOnCallTime = false;

		// Token: 0x040028C0 RID: 10432
		private static bool s_isSponsorshipTimeout = false;

		// Token: 0x040028C1 RID: 10433
		private static long s_leaseTimeTicks = TimeSpan.FromMinutes(5.0).Ticks;

		// Token: 0x040028C2 RID: 10434
		private static long s_renewOnCallTimeTicks = TimeSpan.FromMinutes(2.0).Ticks;

		// Token: 0x040028C3 RID: 10435
		private static long s_sponsorshipTimeoutTicks = TimeSpan.FromMinutes(2.0).Ticks;

		// Token: 0x040028C4 RID: 10436
		private static long s_pollTimeTicks = TimeSpan.FromMilliseconds(10000.0).Ticks;

		// Token: 0x040028C5 RID: 10437
		private static object s_LifetimeSyncObject = null;
	}
}
