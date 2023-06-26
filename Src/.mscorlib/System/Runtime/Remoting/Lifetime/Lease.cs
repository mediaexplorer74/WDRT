using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x0200081E RID: 2078
	internal class Lease : MarshalByRefObject, ILease
	{
		// Token: 0x06005941 RID: 22849 RVA: 0x0013B5B0 File Offset: 0x001397B0
		internal Lease(TimeSpan initialLeaseTime, TimeSpan renewOnCallTime, TimeSpan sponsorshipTimeout, MarshalByRefObject managedObject)
		{
			this.id = Lease.nextId++;
			this.renewOnCallTime = renewOnCallTime;
			this.sponsorshipTimeout = sponsorshipTimeout;
			this.initialLeaseTime = initialLeaseTime;
			this.managedObject = managedObject;
			this.leaseManager = LeaseManager.GetLeaseManager();
			this.sponsorTable = new Hashtable(10);
			this.state = LeaseState.Initial;
		}

		// Token: 0x06005942 RID: 22850 RVA: 0x0013B618 File Offset: 0x00139818
		internal void ActivateLease()
		{
			this.leaseTime = DateTime.UtcNow.Add(this.initialLeaseTime);
			this.state = LeaseState.Active;
			this.leaseManager.ActivateLease(this);
		}

		// Token: 0x06005943 RID: 22851 RVA: 0x0013B651 File Offset: 0x00139851
		[SecurityCritical]
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x06005944 RID: 22852 RVA: 0x0013B654 File Offset: 0x00139854
		// (set) Token: 0x06005945 RID: 22853 RVA: 0x0013B65C File Offset: 0x0013985C
		public TimeSpan RenewOnCallTime
		{
			[SecurityCritical]
			get
			{
				return this.renewOnCallTime;
			}
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
			set
			{
				if (this.state == LeaseState.Initial)
				{
					this.renewOnCallTime = value;
					return;
				}
				throw new RemotingException(Environment.GetResourceString("Remoting_Lifetime_InitialStateRenewOnCall", new object[] { this.state.ToString() }));
			}
		}

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x06005946 RID: 22854 RVA: 0x0013B697 File Offset: 0x00139897
		// (set) Token: 0x06005947 RID: 22855 RVA: 0x0013B69F File Offset: 0x0013989F
		public TimeSpan SponsorshipTimeout
		{
			[SecurityCritical]
			get
			{
				return this.sponsorshipTimeout;
			}
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
			set
			{
				if (this.state == LeaseState.Initial)
				{
					this.sponsorshipTimeout = value;
					return;
				}
				throw new RemotingException(Environment.GetResourceString("Remoting_Lifetime_InitialStateSponsorshipTimeout", new object[] { this.state.ToString() }));
			}
		}

		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x06005948 RID: 22856 RVA: 0x0013B6DA File Offset: 0x001398DA
		// (set) Token: 0x06005949 RID: 22857 RVA: 0x0013B6E4 File Offset: 0x001398E4
		public TimeSpan InitialLeaseTime
		{
			[SecurityCritical]
			get
			{
				return this.initialLeaseTime;
			}
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
			set
			{
				if (this.state != LeaseState.Initial)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_Lifetime_InitialStateInitialLeaseTime", new object[] { this.state.ToString() }));
				}
				this.initialLeaseTime = value;
				if (TimeSpan.Zero.CompareTo(value) >= 0)
				{
					this.state = LeaseState.Null;
					return;
				}
			}
		}

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x0600594A RID: 22858 RVA: 0x0013B743 File Offset: 0x00139943
		public TimeSpan CurrentLeaseTime
		{
			[SecurityCritical]
			get
			{
				return this.leaseTime.Subtract(DateTime.UtcNow);
			}
		}

		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x0600594B RID: 22859 RVA: 0x0013B755 File Offset: 0x00139955
		public LeaseState CurrentState
		{
			[SecurityCritical]
			get
			{
				return this.state;
			}
		}

		// Token: 0x0600594C RID: 22860 RVA: 0x0013B75D File Offset: 0x0013995D
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public void Register(ISponsor obj)
		{
			this.Register(obj, TimeSpan.Zero);
		}

		// Token: 0x0600594D RID: 22861 RVA: 0x0013B76C File Offset: 0x0013996C
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public void Register(ISponsor obj, TimeSpan renewalTime)
		{
			lock (this)
			{
				if (this.state != LeaseState.Expired && !(this.sponsorshipTimeout == TimeSpan.Zero))
				{
					object sponsorId = this.GetSponsorId(obj);
					Hashtable hashtable = this.sponsorTable;
					lock (hashtable)
					{
						if (renewalTime > TimeSpan.Zero)
						{
							this.AddTime(renewalTime);
						}
						if (!this.sponsorTable.ContainsKey(sponsorId))
						{
							this.sponsorTable[sponsorId] = new Lease.SponsorStateInfo(renewalTime, Lease.SponsorState.Initial);
						}
					}
				}
			}
		}

		// Token: 0x0600594E RID: 22862 RVA: 0x0013B824 File Offset: 0x00139A24
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public void Unregister(ISponsor sponsor)
		{
			lock (this)
			{
				if (this.state != LeaseState.Expired)
				{
					object sponsorId = this.GetSponsorId(sponsor);
					Hashtable hashtable = this.sponsorTable;
					lock (hashtable)
					{
						if (sponsorId != null)
						{
							this.leaseManager.DeleteSponsor(sponsorId);
							Lease.SponsorStateInfo sponsorStateInfo = (Lease.SponsorStateInfo)this.sponsorTable[sponsorId];
							this.sponsorTable.Remove(sponsorId);
						}
					}
				}
			}
		}

		// Token: 0x0600594F RID: 22863 RVA: 0x0013B8C4 File Offset: 0x00139AC4
		[SecurityCritical]
		private object GetSponsorId(ISponsor obj)
		{
			object obj2 = null;
			if (obj != null)
			{
				if (RemotingServices.IsTransparentProxy(obj))
				{
					obj2 = RemotingServices.GetRealProxy(obj);
				}
				else
				{
					obj2 = obj;
				}
			}
			return obj2;
		}

		// Token: 0x06005950 RID: 22864 RVA: 0x0013B8EC File Offset: 0x00139AEC
		[SecurityCritical]
		private ISponsor GetSponsorFromId(object sponsorId)
		{
			RealProxy realProxy = sponsorId as RealProxy;
			object obj;
			if (realProxy != null)
			{
				obj = realProxy.GetTransparentProxy();
			}
			else
			{
				obj = sponsorId;
			}
			return (ISponsor)obj;
		}

		// Token: 0x06005951 RID: 22865 RVA: 0x0013B916 File Offset: 0x00139B16
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public TimeSpan Renew(TimeSpan renewalTime)
		{
			return this.RenewInternal(renewalTime);
		}

		// Token: 0x06005952 RID: 22866 RVA: 0x0013B920 File Offset: 0x00139B20
		internal TimeSpan RenewInternal(TimeSpan renewalTime)
		{
			TimeSpan timeSpan;
			lock (this)
			{
				if (this.state == LeaseState.Expired)
				{
					timeSpan = TimeSpan.Zero;
				}
				else
				{
					this.AddTime(renewalTime);
					timeSpan = this.leaseTime.Subtract(DateTime.UtcNow);
				}
			}
			return timeSpan;
		}

		// Token: 0x06005953 RID: 22867 RVA: 0x0013B980 File Offset: 0x00139B80
		internal void Remove()
		{
			if (this.state == LeaseState.Expired)
			{
				return;
			}
			this.state = LeaseState.Expired;
			this.leaseManager.DeleteLease(this);
		}

		// Token: 0x06005954 RID: 22868 RVA: 0x0013B9A0 File Offset: 0x00139BA0
		[SecurityCritical]
		internal void Cancel()
		{
			lock (this)
			{
				if (this.state != LeaseState.Expired)
				{
					this.Remove();
					RemotingServices.Disconnect(this.managedObject, false);
					RemotingServices.Disconnect(this);
				}
			}
		}

		// Token: 0x06005955 RID: 22869 RVA: 0x0013B9FC File Offset: 0x00139BFC
		internal void RenewOnCall()
		{
			lock (this)
			{
				if (this.state != LeaseState.Initial && this.state != LeaseState.Expired)
				{
					this.AddTime(this.renewOnCallTime);
				}
			}
		}

		// Token: 0x06005956 RID: 22870 RVA: 0x0013BA54 File Offset: 0x00139C54
		[SecurityCritical]
		internal void LeaseExpired(DateTime now)
		{
			lock (this)
			{
				if (this.state != LeaseState.Expired)
				{
					if (this.leaseTime.CompareTo(now) < 0)
					{
						this.ProcessNextSponsor();
					}
				}
			}
		}

		// Token: 0x06005957 RID: 22871 RVA: 0x0013BAAC File Offset: 0x00139CAC
		[SecurityCritical]
		internal void SponsorCall(ISponsor sponsor)
		{
			bool flag = false;
			if (this.state == LeaseState.Expired)
			{
				return;
			}
			Hashtable hashtable = this.sponsorTable;
			lock (hashtable)
			{
				try
				{
					object sponsorId = this.GetSponsorId(sponsor);
					this.sponsorCallThread = Thread.CurrentThread.GetHashCode();
					Lease.AsyncRenewal asyncRenewal = new Lease.AsyncRenewal(sponsor.Renewal);
					Lease.SponsorStateInfo sponsorStateInfo = (Lease.SponsorStateInfo)this.sponsorTable[sponsorId];
					sponsorStateInfo.sponsorState = Lease.SponsorState.Waiting;
					IAsyncResult asyncResult = asyncRenewal.BeginInvoke(this, new AsyncCallback(this.SponsorCallback), null);
					if (sponsorStateInfo.sponsorState == Lease.SponsorState.Waiting && this.state != LeaseState.Expired)
					{
						this.leaseManager.RegisterSponsorCall(this, sponsorId, this.sponsorshipTimeout);
					}
					this.sponsorCallThread = 0;
				}
				catch (Exception)
				{
					flag = true;
					this.sponsorCallThread = 0;
				}
			}
			if (flag)
			{
				this.Unregister(sponsor);
				this.ProcessNextSponsor();
			}
		}

		// Token: 0x06005958 RID: 22872 RVA: 0x0013BBA0 File Offset: 0x00139DA0
		[SecurityCritical]
		internal void SponsorTimeout(object sponsorId)
		{
			lock (this)
			{
				if (this.sponsorTable.ContainsKey(sponsorId))
				{
					Hashtable hashtable = this.sponsorTable;
					lock (hashtable)
					{
						Lease.SponsorStateInfo sponsorStateInfo = (Lease.SponsorStateInfo)this.sponsorTable[sponsorId];
						if (sponsorStateInfo.sponsorState == Lease.SponsorState.Waiting)
						{
							this.Unregister(this.GetSponsorFromId(sponsorId));
							this.ProcessNextSponsor();
						}
					}
				}
			}
		}

		// Token: 0x06005959 RID: 22873 RVA: 0x0013BC3C File Offset: 0x00139E3C
		[SecurityCritical]
		private void ProcessNextSponsor()
		{
			object obj = null;
			TimeSpan timeSpan = TimeSpan.Zero;
			Hashtable hashtable = this.sponsorTable;
			lock (hashtable)
			{
				IDictionaryEnumerator enumerator = this.sponsorTable.GetEnumerator();
				while (enumerator.MoveNext())
				{
					object key = enumerator.Key;
					Lease.SponsorStateInfo sponsorStateInfo = (Lease.SponsorStateInfo)enumerator.Value;
					if (sponsorStateInfo.sponsorState == Lease.SponsorState.Initial && timeSpan == TimeSpan.Zero)
					{
						timeSpan = sponsorStateInfo.renewalTime;
						obj = key;
					}
					else if (sponsorStateInfo.renewalTime > timeSpan)
					{
						timeSpan = sponsorStateInfo.renewalTime;
						obj = key;
					}
				}
			}
			if (obj != null)
			{
				this.SponsorCall(this.GetSponsorFromId(obj));
				return;
			}
			this.Cancel();
		}

		// Token: 0x0600595A RID: 22874 RVA: 0x0013BD04 File Offset: 0x00139F04
		[SecurityCritical]
		internal void SponsorCallback(object obj)
		{
			this.SponsorCallback((IAsyncResult)obj);
		}

		// Token: 0x0600595B RID: 22875 RVA: 0x0013BD14 File Offset: 0x00139F14
		[SecurityCritical]
		internal void SponsorCallback(IAsyncResult iar)
		{
			if (this.state == LeaseState.Expired)
			{
				return;
			}
			int hashCode = Thread.CurrentThread.GetHashCode();
			if (hashCode == this.sponsorCallThread)
			{
				WaitCallback waitCallback = new WaitCallback(this.SponsorCallback);
				ThreadPool.QueueUserWorkItem(waitCallback, iar);
				return;
			}
			AsyncResult asyncResult = (AsyncResult)iar;
			Lease.AsyncRenewal asyncRenewal = (Lease.AsyncRenewal)asyncResult.AsyncDelegate;
			ISponsor sponsor = (ISponsor)asyncRenewal.Target;
			Lease.SponsorStateInfo sponsorStateInfo = null;
			if (!iar.IsCompleted)
			{
				this.Unregister(sponsor);
				this.ProcessNextSponsor();
				return;
			}
			bool flag = false;
			TimeSpan timeSpan = TimeSpan.Zero;
			try
			{
				timeSpan = asyncRenewal.EndInvoke(iar);
			}
			catch (Exception)
			{
				flag = true;
			}
			if (flag)
			{
				this.Unregister(sponsor);
				this.ProcessNextSponsor();
				return;
			}
			object sponsorId = this.GetSponsorId(sponsor);
			Hashtable hashtable = this.sponsorTable;
			lock (hashtable)
			{
				if (this.sponsorTable.ContainsKey(sponsorId))
				{
					sponsorStateInfo = (Lease.SponsorStateInfo)this.sponsorTable[sponsorId];
					sponsorStateInfo.sponsorState = Lease.SponsorState.Completed;
					sponsorStateInfo.renewalTime = timeSpan;
				}
			}
			if (sponsorStateInfo == null)
			{
				this.ProcessNextSponsor();
				return;
			}
			if (sponsorStateInfo.renewalTime == TimeSpan.Zero)
			{
				this.Unregister(sponsor);
				this.ProcessNextSponsor();
				return;
			}
			this.RenewInternal(sponsorStateInfo.renewalTime);
		}

		// Token: 0x0600595C RID: 22876 RVA: 0x0013BE74 File Offset: 0x0013A074
		private void AddTime(TimeSpan renewalSpan)
		{
			if (this.state == LeaseState.Expired)
			{
				return;
			}
			DateTime utcNow = DateTime.UtcNow;
			DateTime dateTime = this.leaseTime;
			DateTime dateTime2 = utcNow.Add(renewalSpan);
			if (this.leaseTime.CompareTo(dateTime2) < 0)
			{
				this.leaseManager.ChangedLeaseTime(this, dateTime2);
				this.leaseTime = dateTime2;
				this.state = LeaseState.Active;
			}
		}

		// Token: 0x040028A4 RID: 10404
		internal int id;

		// Token: 0x040028A5 RID: 10405
		internal DateTime leaseTime;

		// Token: 0x040028A6 RID: 10406
		internal TimeSpan initialLeaseTime;

		// Token: 0x040028A7 RID: 10407
		internal TimeSpan renewOnCallTime;

		// Token: 0x040028A8 RID: 10408
		internal TimeSpan sponsorshipTimeout;

		// Token: 0x040028A9 RID: 10409
		internal Hashtable sponsorTable;

		// Token: 0x040028AA RID: 10410
		internal int sponsorCallThread;

		// Token: 0x040028AB RID: 10411
		internal LeaseManager leaseManager;

		// Token: 0x040028AC RID: 10412
		internal MarshalByRefObject managedObject;

		// Token: 0x040028AD RID: 10413
		internal LeaseState state;

		// Token: 0x040028AE RID: 10414
		internal static volatile int nextId;

		// Token: 0x02000C6D RID: 3181
		// (Invoke) Token: 0x060070C8 RID: 28872
		internal delegate TimeSpan AsyncRenewal(ILease lease);

		// Token: 0x02000C6E RID: 3182
		[Serializable]
		internal enum SponsorState
		{
			// Token: 0x040037F6 RID: 14326
			Initial,
			// Token: 0x040037F7 RID: 14327
			Waiting,
			// Token: 0x040037F8 RID: 14328
			Completed
		}

		// Token: 0x02000C6F RID: 3183
		internal sealed class SponsorStateInfo
		{
			// Token: 0x060070CB RID: 28875 RVA: 0x001860D4 File Offset: 0x001842D4
			internal SponsorStateInfo(TimeSpan renewalTime, Lease.SponsorState sponsorState)
			{
				this.renewalTime = renewalTime;
				this.sponsorState = sponsorState;
			}

			// Token: 0x040037F9 RID: 14329
			internal TimeSpan renewalTime;

			// Token: 0x040037FA RID: 14330
			internal Lease.SponsorState sponsorState;
		}
	}
}
