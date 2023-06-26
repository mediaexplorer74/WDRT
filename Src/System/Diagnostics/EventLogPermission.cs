using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Controls code access permissions for event logging.</summary>
	// Token: 0x020004CF RID: 1231
	[Serializable]
	public sealed class EventLogPermission : ResourcePermissionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermission" /> class.</summary>
		// Token: 0x06002E62 RID: 11874 RVA: 0x000D1695 File Offset: 0x000CF895
		public EventLogPermission()
		{
			this.SetNames();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermission" /> class with the specified permission state.</summary>
		/// <param name="state">One of the enumeration values that specifies the permission state (full access or no access to resources).</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />.</exception>
		// Token: 0x06002E63 RID: 11875 RVA: 0x000D16A3 File Offset: 0x000CF8A3
		public EventLogPermission(PermissionState state)
			: base(state)
		{
			this.SetNames();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermission" /> class with the specified access levels and the name of the computer to use.</summary>
		/// <param name="permissionAccess">One of the enumeration values that specifies an access level.</param>
		/// <param name="machineName">The name of the computer on which to read or write events.</param>
		// Token: 0x06002E64 RID: 11876 RVA: 0x000D16B2 File Offset: 0x000CF8B2
		public EventLogPermission(EventLogPermissionAccess permissionAccess, string machineName)
		{
			this.SetNames();
			this.AddPermissionAccess(new EventLogPermissionEntry(permissionAccess, machineName));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermission" /> class with the specified permission entries.</summary>
		/// <param name="permissionAccessEntries">An array of  objects that represent permission entries. The <see cref="P:System.Diagnostics.EventLogPermission.PermissionEntries" /> property is set to this value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="permissionAccessEntries" /> is <see langword="null" />.</exception>
		// Token: 0x06002E65 RID: 11877 RVA: 0x000D16D0 File Offset: 0x000CF8D0
		public EventLogPermission(EventLogPermissionEntry[] permissionAccessEntries)
		{
			if (permissionAccessEntries == null)
			{
				throw new ArgumentNullException("permissionAccessEntries");
			}
			this.SetNames();
			for (int i = 0; i < permissionAccessEntries.Length; i++)
			{
				this.AddPermissionAccess(permissionAccessEntries[i]);
			}
		}

		/// <summary>Gets the collection of permission entries for this permissions request.</summary>
		/// <returns>A collection that contains the permission entries for this permissions request.</returns>
		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06002E66 RID: 11878 RVA: 0x000D170E File Offset: 0x000CF90E
		public EventLogPermissionEntryCollection PermissionEntries
		{
			get
			{
				if (this.innerCollection == null)
				{
					this.innerCollection = new EventLogPermissionEntryCollection(this, base.GetPermissionEntries());
				}
				return this.innerCollection;
			}
		}

		// Token: 0x06002E67 RID: 11879 RVA: 0x000D1730 File Offset: 0x000CF930
		internal void AddPermissionAccess(EventLogPermissionEntry entry)
		{
			base.AddPermissionAccess(entry.GetBaseEntry());
		}

		// Token: 0x06002E68 RID: 11880 RVA: 0x000D173E File Offset: 0x000CF93E
		internal new void Clear()
		{
			base.Clear();
		}

		// Token: 0x06002E69 RID: 11881 RVA: 0x000D1746 File Offset: 0x000CF946
		internal void RemovePermissionAccess(EventLogPermissionEntry entry)
		{
			base.RemovePermissionAccess(entry.GetBaseEntry());
		}

		// Token: 0x06002E6A RID: 11882 RVA: 0x000D1754 File Offset: 0x000CF954
		private void SetNames()
		{
			base.PermissionAccessType = typeof(EventLogPermissionAccess);
			base.TagNames = new string[] { "Machine" };
		}

		// Token: 0x0400275C RID: 10076
		private EventLogPermissionEntryCollection innerCollection;
	}
}
