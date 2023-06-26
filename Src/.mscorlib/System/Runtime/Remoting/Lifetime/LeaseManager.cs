using System;
using System.Collections;
using System.Diagnostics;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000820 RID: 2080
	internal class LeaseManager
	{
		// Token: 0x06005961 RID: 22881 RVA: 0x0013BF1C File Offset: 0x0013A11C
		internal static bool IsInitialized()
		{
			DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
			LeaseManager leaseManager = remotingData.LeaseManager;
			return leaseManager != null;
		}

		// Token: 0x06005962 RID: 22882 RVA: 0x0013BF40 File Offset: 0x0013A140
		[SecurityCritical]
		internal static LeaseManager GetLeaseManager(TimeSpan pollTime)
		{
			DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
			LeaseManager leaseManager = remotingData.LeaseManager;
			if (leaseManager == null)
			{
				DomainSpecificRemotingData domainSpecificRemotingData = remotingData;
				lock (domainSpecificRemotingData)
				{
					if (remotingData.LeaseManager == null)
					{
						remotingData.LeaseManager = new LeaseManager(pollTime);
					}
					leaseManager = remotingData.LeaseManager;
				}
			}
			return leaseManager;
		}

		// Token: 0x06005963 RID: 22883 RVA: 0x0013BFA8 File Offset: 0x0013A1A8
		internal static LeaseManager GetLeaseManager()
		{
			DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
			return remotingData.LeaseManager;
		}

		// Token: 0x06005964 RID: 22884 RVA: 0x0013BFC8 File Offset: 0x0013A1C8
		[SecurityCritical]
		private LeaseManager(TimeSpan pollTime)
		{
			this.pollTime = pollTime;
			this.leaseTimeAnalyzerDelegate = new TimerCallback(this.LeaseTimeAnalyzer);
			this.waitHandle = new AutoResetEvent(false);
			this.leaseTimer = new Timer(this.leaseTimeAnalyzerDelegate, null, -1, -1);
			this.leaseTimer.Change((int)pollTime.TotalMilliseconds, -1);
		}

		// Token: 0x06005965 RID: 22885 RVA: 0x0013C050 File Offset: 0x0013A250
		internal void ChangePollTime(TimeSpan pollTime)
		{
			this.pollTime = pollTime;
		}

		// Token: 0x06005966 RID: 22886 RVA: 0x0013C05C File Offset: 0x0013A25C
		internal void ActivateLease(Lease lease)
		{
			Hashtable hashtable = this.leaseToTimeTable;
			lock (hashtable)
			{
				this.leaseToTimeTable[lease] = lease.leaseTime;
			}
		}

		// Token: 0x06005967 RID: 22887 RVA: 0x0013C0B0 File Offset: 0x0013A2B0
		internal void DeleteLease(Lease lease)
		{
			Hashtable hashtable = this.leaseToTimeTable;
			lock (hashtable)
			{
				this.leaseToTimeTable.Remove(lease);
			}
		}

		// Token: 0x06005968 RID: 22888 RVA: 0x0013C0F8 File Offset: 0x0013A2F8
		[Conditional("_LOGGING")]
		internal void DumpLeases(Lease[] leases)
		{
			for (int i = 0; i < leases.Length; i++)
			{
			}
		}

		// Token: 0x06005969 RID: 22889 RVA: 0x0013C114 File Offset: 0x0013A314
		internal ILease GetLease(MarshalByRefObject obj)
		{
			bool flag = true;
			Identity identity = MarshalByRefObject.GetIdentity(obj, out flag);
			if (identity == null)
			{
				return null;
			}
			return identity.Lease;
		}

		// Token: 0x0600596A RID: 22890 RVA: 0x0013C138 File Offset: 0x0013A338
		internal void ChangedLeaseTime(Lease lease, DateTime newTime)
		{
			Hashtable hashtable = this.leaseToTimeTable;
			lock (hashtable)
			{
				this.leaseToTimeTable[lease] = newTime;
			}
		}

		// Token: 0x0600596B RID: 22891 RVA: 0x0013C184 File Offset: 0x0013A384
		internal void RegisterSponsorCall(Lease lease, object sponsorId, TimeSpan sponsorshipTimeOut)
		{
			Hashtable hashtable = this.sponsorTable;
			lock (hashtable)
			{
				DateTime dateTime = DateTime.UtcNow.Add(sponsorshipTimeOut);
				this.sponsorTable[sponsorId] = new LeaseManager.SponsorInfo(lease, sponsorId, dateTime);
			}
		}

		// Token: 0x0600596C RID: 22892 RVA: 0x0013C1E4 File Offset: 0x0013A3E4
		internal void DeleteSponsor(object sponsorId)
		{
			Hashtable hashtable = this.sponsorTable;
			lock (hashtable)
			{
				this.sponsorTable.Remove(sponsorId);
			}
		}

		// Token: 0x0600596D RID: 22893 RVA: 0x0013C22C File Offset: 0x0013A42C
		[SecurityCritical]
		private void LeaseTimeAnalyzer(object state)
		{
			DateTime utcNow = DateTime.UtcNow;
			Hashtable hashtable = this.leaseToTimeTable;
			lock (hashtable)
			{
				IDictionaryEnumerator enumerator = this.leaseToTimeTable.GetEnumerator();
				while (enumerator.MoveNext())
				{
					DateTime dateTime = (DateTime)enumerator.Value;
					Lease lease = (Lease)enumerator.Key;
					if (dateTime.CompareTo(utcNow) < 0)
					{
						this.tempObjects.Add(lease);
					}
				}
				for (int i = 0; i < this.tempObjects.Count; i++)
				{
					Lease lease2 = (Lease)this.tempObjects[i];
					this.leaseToTimeTable.Remove(lease2);
				}
			}
			for (int j = 0; j < this.tempObjects.Count; j++)
			{
				Lease lease3 = (Lease)this.tempObjects[j];
				if (lease3 != null)
				{
					lease3.LeaseExpired(utcNow);
				}
			}
			this.tempObjects.Clear();
			Hashtable hashtable2 = this.sponsorTable;
			lock (hashtable2)
			{
				IDictionaryEnumerator enumerator2 = this.sponsorTable.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					object key = enumerator2.Key;
					LeaseManager.SponsorInfo sponsorInfo = (LeaseManager.SponsorInfo)enumerator2.Value;
					if (sponsorInfo.sponsorWaitTime.CompareTo(utcNow) < 0)
					{
						this.tempObjects.Add(sponsorInfo);
					}
				}
				for (int k = 0; k < this.tempObjects.Count; k++)
				{
					LeaseManager.SponsorInfo sponsorInfo2 = (LeaseManager.SponsorInfo)this.tempObjects[k];
					this.sponsorTable.Remove(sponsorInfo2.sponsorId);
				}
			}
			for (int l = 0; l < this.tempObjects.Count; l++)
			{
				LeaseManager.SponsorInfo sponsorInfo3 = (LeaseManager.SponsorInfo)this.tempObjects[l];
				if (sponsorInfo3 != null && sponsorInfo3.lease != null)
				{
					sponsorInfo3.lease.SponsorTimeout(sponsorInfo3.sponsorId);
					this.tempObjects[l] = null;
				}
			}
			this.tempObjects.Clear();
			this.leaseTimer.Change((int)this.pollTime.TotalMilliseconds, -1);
		}

		// Token: 0x040028B1 RID: 10417
		private Hashtable leaseToTimeTable = new Hashtable();

		// Token: 0x040028B2 RID: 10418
		private Hashtable sponsorTable = new Hashtable();

		// Token: 0x040028B3 RID: 10419
		private TimeSpan pollTime;

		// Token: 0x040028B4 RID: 10420
		private AutoResetEvent waitHandle;

		// Token: 0x040028B5 RID: 10421
		private TimerCallback leaseTimeAnalyzerDelegate;

		// Token: 0x040028B6 RID: 10422
		private volatile Timer leaseTimer;

		// Token: 0x040028B7 RID: 10423
		private ArrayList tempObjects = new ArrayList(10);

		// Token: 0x02000C70 RID: 3184
		internal class SponsorInfo
		{
			// Token: 0x060070CC RID: 28876 RVA: 0x001860EA File Offset: 0x001842EA
			internal SponsorInfo(Lease lease, object sponsorId, DateTime sponsorWaitTime)
			{
				this.lease = lease;
				this.sponsorId = sponsorId;
				this.sponsorWaitTime = sponsorWaitTime;
			}

			// Token: 0x040037FB RID: 14331
			internal Lease lease;

			// Token: 0x040037FC RID: 14332
			internal object sponsorId;

			// Token: 0x040037FD RID: 14333
			internal DateTime sponsorWaitTime;
		}
	}
}
