using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200041B RID: 1051
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	internal struct EventDescriptor
	{
		// Token: 0x0600345B RID: 13403 RVA: 0x000C8964 File Offset: 0x000C6B64
		public EventDescriptor(int traceloggingId, byte level, byte opcode, long keywords)
		{
			this.m_id = 0;
			this.m_version = 0;
			this.m_channel = 0;
			this.m_traceloggingId = traceloggingId;
			this.m_level = level;
			this.m_opcode = opcode;
			this.m_task = 0;
			this.m_keywords = keywords;
		}

		// Token: 0x0600345C RID: 13404 RVA: 0x000C89A0 File Offset: 0x000C6BA0
		public EventDescriptor(int id, byte version, byte channel, byte level, byte opcode, int task, long keywords)
		{
			if (id < 0)
			{
				throw new ArgumentOutOfRangeException("id", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (id > 65535)
			{
				throw new ArgumentOutOfRangeException("id", Environment.GetResourceString("ArgumentOutOfRange_NeedValidId", new object[] { 1, ushort.MaxValue }));
			}
			this.m_traceloggingId = 0;
			this.m_id = (ushort)id;
			this.m_version = version;
			this.m_channel = channel;
			this.m_level = level;
			this.m_opcode = opcode;
			this.m_keywords = keywords;
			if (task < 0)
			{
				throw new ArgumentOutOfRangeException("task", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (task > 65535)
			{
				throw new ArgumentOutOfRangeException("task", Environment.GetResourceString("ArgumentOutOfRange_NeedValidId", new object[] { 1, ushort.MaxValue }));
			}
			this.m_task = (ushort)task;
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x0600345D RID: 13405 RVA: 0x000C8A91 File Offset: 0x000C6C91
		public int EventId
		{
			get
			{
				return (int)this.m_id;
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x0600345E RID: 13406 RVA: 0x000C8A99 File Offset: 0x000C6C99
		public byte Version
		{
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x0600345F RID: 13407 RVA: 0x000C8AA1 File Offset: 0x000C6CA1
		public byte Channel
		{
			get
			{
				return this.m_channel;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06003460 RID: 13408 RVA: 0x000C8AA9 File Offset: 0x000C6CA9
		public byte Level
		{
			get
			{
				return this.m_level;
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06003461 RID: 13409 RVA: 0x000C8AB1 File Offset: 0x000C6CB1
		public byte Opcode
		{
			get
			{
				return this.m_opcode;
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06003462 RID: 13410 RVA: 0x000C8AB9 File Offset: 0x000C6CB9
		public int Task
		{
			get
			{
				return (int)this.m_task;
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06003463 RID: 13411 RVA: 0x000C8AC1 File Offset: 0x000C6CC1
		public long Keywords
		{
			get
			{
				return this.m_keywords;
			}
		}

		// Token: 0x06003464 RID: 13412 RVA: 0x000C8AC9 File Offset: 0x000C6CC9
		public override bool Equals(object obj)
		{
			return obj is EventDescriptor && this.Equals((EventDescriptor)obj);
		}

		// Token: 0x06003465 RID: 13413 RVA: 0x000C8AE1 File Offset: 0x000C6CE1
		public override int GetHashCode()
		{
			return (int)(this.m_id ^ (ushort)this.m_version ^ (ushort)this.m_channel ^ (ushort)this.m_level ^ (ushort)this.m_opcode ^ this.m_task) ^ (int)this.m_keywords;
		}

		// Token: 0x06003466 RID: 13414 RVA: 0x000C8B14 File Offset: 0x000C6D14
		public bool Equals(EventDescriptor other)
		{
			return this.m_id == other.m_id && this.m_version == other.m_version && this.m_channel == other.m_channel && this.m_level == other.m_level && this.m_opcode == other.m_opcode && this.m_task == other.m_task && this.m_keywords == other.m_keywords;
		}

		// Token: 0x06003467 RID: 13415 RVA: 0x000C8B86 File Offset: 0x000C6D86
		public static bool operator ==(EventDescriptor event1, EventDescriptor event2)
		{
			return event1.Equals(event2);
		}

		// Token: 0x06003468 RID: 13416 RVA: 0x000C8B90 File Offset: 0x000C6D90
		public static bool operator !=(EventDescriptor event1, EventDescriptor event2)
		{
			return !event1.Equals(event2);
		}

		// Token: 0x04001739 RID: 5945
		[FieldOffset(0)]
		private int m_traceloggingId;

		// Token: 0x0400173A RID: 5946
		[FieldOffset(0)]
		private ushort m_id;

		// Token: 0x0400173B RID: 5947
		[FieldOffset(2)]
		private byte m_version;

		// Token: 0x0400173C RID: 5948
		[FieldOffset(3)]
		private byte m_channel;

		// Token: 0x0400173D RID: 5949
		[FieldOffset(4)]
		private byte m_level;

		// Token: 0x0400173E RID: 5950
		[FieldOffset(5)]
		private byte m_opcode;

		// Token: 0x0400173F RID: 5951
		[FieldOffset(6)]
		private ushort m_task;

		// Token: 0x04001740 RID: 5952
		[FieldOffset(8)]
		private long m_keywords;
	}
}
