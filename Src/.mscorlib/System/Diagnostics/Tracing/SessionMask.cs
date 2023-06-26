using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200042C RID: 1068
	internal struct SessionMask
	{
		// Token: 0x06003577 RID: 13687 RVA: 0x000D0813 File Offset: 0x000CEA13
		public SessionMask(SessionMask m)
		{
			this.m_mask = m.m_mask;
		}

		// Token: 0x06003578 RID: 13688 RVA: 0x000D0821 File Offset: 0x000CEA21
		public SessionMask(uint mask = 0U)
		{
			this.m_mask = mask & 15U;
		}

		// Token: 0x06003579 RID: 13689 RVA: 0x000D082D File Offset: 0x000CEA2D
		public bool IsEqualOrSupersetOf(SessionMask m)
		{
			return (this.m_mask | m.m_mask) == this.m_mask;
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x0600357A RID: 13690 RVA: 0x000D0844 File Offset: 0x000CEA44
		public static SessionMask All
		{
			get
			{
				return new SessionMask(15U);
			}
		}

		// Token: 0x0600357B RID: 13691 RVA: 0x000D084D File Offset: 0x000CEA4D
		public static SessionMask FromId(int perEventSourceSessionId)
		{
			return new SessionMask(1U << perEventSourceSessionId);
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x000D085A File Offset: 0x000CEA5A
		public ulong ToEventKeywords()
		{
			return (ulong)this.m_mask << 44;
		}

		// Token: 0x0600357D RID: 13693 RVA: 0x000D0866 File Offset: 0x000CEA66
		public static SessionMask FromEventKeywords(ulong m)
		{
			return new SessionMask((uint)(m >> 44));
		}

		// Token: 0x170007F3 RID: 2035
		public bool this[int perEventSourceSessionId]
		{
			get
			{
				return ((ulong)this.m_mask & (ulong)(1L << (perEventSourceSessionId & 31))) > 0UL;
			}
			set
			{
				if (value)
				{
					this.m_mask |= 1U << perEventSourceSessionId;
					return;
				}
				this.m_mask &= ~(1U << perEventSourceSessionId);
			}
		}

		// Token: 0x06003580 RID: 13696 RVA: 0x000D08B4 File Offset: 0x000CEAB4
		public static SessionMask operator |(SessionMask m1, SessionMask m2)
		{
			return new SessionMask(m1.m_mask | m2.m_mask);
		}

		// Token: 0x06003581 RID: 13697 RVA: 0x000D08C8 File Offset: 0x000CEAC8
		public static SessionMask operator &(SessionMask m1, SessionMask m2)
		{
			return new SessionMask(m1.m_mask & m2.m_mask);
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x000D08DC File Offset: 0x000CEADC
		public static SessionMask operator ^(SessionMask m1, SessionMask m2)
		{
			return new SessionMask(m1.m_mask ^ m2.m_mask);
		}

		// Token: 0x06003583 RID: 13699 RVA: 0x000D08F0 File Offset: 0x000CEAF0
		public static SessionMask operator ~(SessionMask m)
		{
			return new SessionMask(15U & ~m.m_mask);
		}

		// Token: 0x06003584 RID: 13700 RVA: 0x000D0901 File Offset: 0x000CEB01
		public static explicit operator ulong(SessionMask m)
		{
			return (ulong)m.m_mask;
		}

		// Token: 0x06003585 RID: 13701 RVA: 0x000D090A File Offset: 0x000CEB0A
		public static explicit operator uint(SessionMask m)
		{
			return m.m_mask;
		}

		// Token: 0x040017C6 RID: 6086
		private uint m_mask;

		// Token: 0x040017C7 RID: 6087
		internal const int SHIFT_SESSION_TO_KEYWORD = 44;

		// Token: 0x040017C8 RID: 6088
		internal const uint MASK = 15U;

		// Token: 0x040017C9 RID: 6089
		internal const uint MAX = 4U;
	}
}
