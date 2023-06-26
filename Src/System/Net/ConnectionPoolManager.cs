using System;
using System.Collections;
using System.Globalization;
using System.Threading;

namespace System.Net
{
	// Token: 0x020000CF RID: 207
	internal class ConnectionPoolManager
	{
		// Token: 0x060006DF RID: 1759 RVA: 0x00026018 File Offset: 0x00024218
		private ConnectionPoolManager()
		{
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x00026020 File Offset: 0x00024220
		private static object InternalSyncObject
		{
			get
			{
				if (ConnectionPoolManager.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref ConnectionPoolManager.s_InternalSyncObject, obj, null);
				}
				return ConnectionPoolManager.s_InternalSyncObject;
			}
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0002604C File Offset: 0x0002424C
		private static string GenerateKey(string hostName, int port, string groupName)
		{
			return string.Concat(new string[]
			{
				hostName,
				"\r",
				port.ToString(NumberFormatInfo.InvariantInfo),
				"\r",
				groupName
			});
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00026080 File Offset: 0x00024280
		internal static ConnectionPool GetConnectionPool(ServicePoint servicePoint, string groupName, CreateConnectionDelegate createConnectionCallback)
		{
			string text = ConnectionPoolManager.GenerateKey(servicePoint.Host, servicePoint.Port, groupName);
			object internalSyncObject = ConnectionPoolManager.InternalSyncObject;
			ConnectionPool connectionPool2;
			lock (internalSyncObject)
			{
				ConnectionPool connectionPool = (ConnectionPool)ConnectionPoolManager.m_ConnectionPools[text];
				if (connectionPool == null)
				{
					connectionPool = new ConnectionPool(servicePoint, servicePoint.ConnectionLimit, 0, servicePoint.MaxIdleTime, createConnectionCallback);
					ConnectionPoolManager.m_ConnectionPools[text] = connectionPool;
				}
				connectionPool2 = connectionPool;
			}
			return connectionPool2;
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00026108 File Offset: 0x00024308
		internal static bool RemoveConnectionPool(ServicePoint servicePoint, string groupName)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, "ConnectionPoolManager::RemoveConnectionPool, groupName=" + groupName);
			}
			string text = ConnectionPoolManager.GenerateKey(servicePoint.Host, servicePoint.Port, groupName);
			object internalSyncObject = ConnectionPoolManager.InternalSyncObject;
			lock (internalSyncObject)
			{
				ConnectionPool connectionPool = (ConnectionPool)ConnectionPoolManager.m_ConnectionPools[text];
				if (connectionPool != null)
				{
					ConnectionPoolManager.m_ConnectionPools[text] = null;
					ConnectionPoolManager.m_ConnectionPools.Remove(text);
					if (Logging.On)
					{
						Logging.PrintInfo(Logging.Web, "ConnectionPoolManager::RemoveConnectionPool, removed connection pool: " + text);
					}
					return true;
				}
			}
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, "ConnectionPoolManager::RemoveConnectionPool, no connection pool found: " + text);
			}
			return false;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x000261DC File Offset: 0x000243DC
		internal static void CleanupConnectionPool(ServicePoint servicePoint, string groupName)
		{
			string text = ConnectionPoolManager.GenerateKey(servicePoint.Host, servicePoint.Port, groupName);
			object internalSyncObject = ConnectionPoolManager.InternalSyncObject;
			lock (internalSyncObject)
			{
				ConnectionPool connectionPool = (ConnectionPool)ConnectionPoolManager.m_ConnectionPools[text];
				if (connectionPool != null)
				{
					connectionPool.ForceCleanup();
				}
			}
		}

		// Token: 0x04000CAE RID: 3246
		private static Hashtable m_ConnectionPools = new Hashtable();

		// Token: 0x04000CAF RID: 3247
		private static object s_InternalSyncObject;
	}
}
