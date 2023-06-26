using System;
using System.Collections;
using System.Configuration;
using System.Threading;

namespace System.Net.Configuration
{
	// Token: 0x0200032E RID: 814
	internal sealed class ConnectionManagementSectionInternal
	{
		// Token: 0x06001D1F RID: 7455 RVA: 0x0008AB8C File Offset: 0x00088D8C
		internal ConnectionManagementSectionInternal(ConnectionManagementSection section)
		{
			if (section.ConnectionManagement.Count > 0)
			{
				this.connectionManagement = new Hashtable(section.ConnectionManagement.Count);
				foreach (object obj in section.ConnectionManagement)
				{
					ConnectionManagementElement connectionManagementElement = (ConnectionManagementElement)obj;
					this.connectionManagement[connectionManagementElement.Address] = connectionManagementElement.MaxConnection;
				}
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001D20 RID: 7456 RVA: 0x0008AC24 File Offset: 0x00088E24
		internal Hashtable ConnectionManagement
		{
			get
			{
				Hashtable hashtable = this.connectionManagement;
				if (hashtable == null)
				{
					hashtable = new Hashtable();
				}
				return hashtable;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001D21 RID: 7457 RVA: 0x0008AC44 File Offset: 0x00088E44
		internal static object ClassSyncObject
		{
			get
			{
				if (ConnectionManagementSectionInternal.classSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref ConnectionManagementSectionInternal.classSyncObject, obj, null);
				}
				return ConnectionManagementSectionInternal.classSyncObject;
			}
		}

		// Token: 0x06001D22 RID: 7458 RVA: 0x0008AC70 File Offset: 0x00088E70
		internal static ConnectionManagementSectionInternal GetSection()
		{
			object obj = ConnectionManagementSectionInternal.ClassSyncObject;
			ConnectionManagementSectionInternal connectionManagementSectionInternal;
			lock (obj)
			{
				ConnectionManagementSection connectionManagementSection = PrivilegedConfigurationManager.GetSection(ConfigurationStrings.ConnectionManagementSectionPath) as ConnectionManagementSection;
				if (connectionManagementSection == null)
				{
					connectionManagementSectionInternal = null;
				}
				else
				{
					connectionManagementSectionInternal = new ConnectionManagementSectionInternal(connectionManagementSection);
				}
			}
			return connectionManagementSectionInternal;
		}

		// Token: 0x04001C12 RID: 7186
		private Hashtable connectionManagement;

		// Token: 0x04001C13 RID: 7187
		private static object classSyncObject;
	}
}
