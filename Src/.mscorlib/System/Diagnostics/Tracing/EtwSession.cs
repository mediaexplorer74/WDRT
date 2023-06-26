using System;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200042B RID: 1067
	internal class EtwSession
	{
		// Token: 0x06003572 RID: 13682 RVA: 0x000D06C4 File Offset: 0x000CE8C4
		public static EtwSession GetEtwSession(int etwSessionId, bool bCreateIfNeeded = false)
		{
			if (etwSessionId < 0)
			{
				return null;
			}
			EtwSession etwSession;
			foreach (WeakReference<EtwSession> weakReference in EtwSession.s_etwSessions)
			{
				if (weakReference.TryGetTarget(out etwSession) && etwSession.m_etwSessionId == etwSessionId)
				{
					return etwSession;
				}
			}
			if (!bCreateIfNeeded)
			{
				return null;
			}
			if (EtwSession.s_etwSessions == null)
			{
				EtwSession.s_etwSessions = new List<WeakReference<EtwSession>>();
			}
			etwSession = new EtwSession(etwSessionId);
			EtwSession.s_etwSessions.Add(new WeakReference<EtwSession>(etwSession));
			if (EtwSession.s_etwSessions.Count > 16)
			{
				EtwSession.TrimGlobalList();
			}
			return etwSession;
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x000D0770 File Offset: 0x000CE970
		public static void RemoveEtwSession(EtwSession etwSession)
		{
			if (EtwSession.s_etwSessions == null || etwSession == null)
			{
				return;
			}
			EtwSession.s_etwSessions.RemoveAll(delegate(WeakReference<EtwSession> wrEtwSession)
			{
				EtwSession etwSession2;
				return wrEtwSession.TryGetTarget(out etwSession2) && etwSession2.m_etwSessionId == etwSession.m_etwSessionId;
			});
			if (EtwSession.s_etwSessions.Count > 16)
			{
				EtwSession.TrimGlobalList();
			}
		}

		// Token: 0x06003574 RID: 13684 RVA: 0x000D07C4 File Offset: 0x000CE9C4
		private static void TrimGlobalList()
		{
			if (EtwSession.s_etwSessions == null)
			{
				return;
			}
			EtwSession.s_etwSessions.RemoveAll(delegate(WeakReference<EtwSession> wrEtwSession)
			{
				EtwSession etwSession;
				return !wrEtwSession.TryGetTarget(out etwSession);
			});
		}

		// Token: 0x06003575 RID: 13685 RVA: 0x000D07F8 File Offset: 0x000CE9F8
		private EtwSession(int etwSessionId)
		{
			this.m_etwSessionId = etwSessionId;
		}

		// Token: 0x040017C2 RID: 6082
		public readonly int m_etwSessionId;

		// Token: 0x040017C3 RID: 6083
		public ActivityFilter m_activityFilter;

		// Token: 0x040017C4 RID: 6084
		private static List<WeakReference<EtwSession>> s_etwSessions = new List<WeakReference<EtwSession>>();

		// Token: 0x040017C5 RID: 6085
		private const int s_thrSessionCount = 16;
	}
}
