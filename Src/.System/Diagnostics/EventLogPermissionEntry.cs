using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Defines the smallest unit of a code access security permission that is set for an <see cref="T:System.Diagnostics.EventLog" />.</summary>
	// Token: 0x020004D2 RID: 1234
	[Serializable]
	public class EventLogPermissionEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> class.</summary>
		/// <param name="permissionAccess">A bitwise combination of the <see cref="T:System.Diagnostics.EventLogPermissionAccess" /> values. The <see cref="P:System.Diagnostics.EventLogPermissionEntry.PermissionAccess" /> property is set to this value.</param>
		/// <param name="machineName">The name of the computer on which to read or write events. The <see cref="P:System.Diagnostics.EventLogPermissionEntry.MachineName" /> property is set to this value.</param>
		/// <exception cref="T:System.ArgumentException">The computer name is invalid.</exception>
		// Token: 0x06002E71 RID: 11889 RVA: 0x000D1804 File Offset: 0x000CFA04
		public EventLogPermissionEntry(EventLogPermissionAccess permissionAccess, string machineName)
		{
			if (!SyntaxCheck.CheckMachineName(machineName))
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[] { "MachineName", machineName }));
			}
			this.permissionAccess = permissionAccess;
			this.machineName = machineName;
		}

		// Token: 0x06002E72 RID: 11890 RVA: 0x000D1844 File Offset: 0x000CFA44
		internal EventLogPermissionEntry(ResourcePermissionBaseEntry baseEntry)
		{
			this.permissionAccess = (EventLogPermissionAccess)baseEntry.PermissionAccess;
			this.machineName = baseEntry.PermissionAccessPath[0];
		}

		/// <summary>Gets the name of the computer on which to read or write events.</summary>
		/// <returns>The name of the computer on which to read or write events.</returns>
		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06002E73 RID: 11891 RVA: 0x000D1866 File Offset: 0x000CFA66
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		/// <summary>Gets the permission access levels used in the permissions request.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Diagnostics.EventLogPermissionAccess" /> values.</returns>
		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06002E74 RID: 11892 RVA: 0x000D186E File Offset: 0x000CFA6E
		public EventLogPermissionAccess PermissionAccess
		{
			get
			{
				return this.permissionAccess;
			}
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x000D1878 File Offset: 0x000CFA78
		internal ResourcePermissionBaseEntry GetBaseEntry()
		{
			return new ResourcePermissionBaseEntry((int)this.PermissionAccess, new string[] { this.MachineName });
		}

		// Token: 0x04002766 RID: 10086
		private string machineName;

		// Token: 0x04002767 RID: 10087
		private EventLogPermissionAccess permissionAccess;
	}
}
