using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Allows control of code access permissions for <see cref="T:System.Diagnostics.PerformanceCounter" />.</summary>
	// Token: 0x020004E8 RID: 1256
	[Serializable]
	public sealed class PerformanceCounterPermission : ResourcePermissionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermission" /> class.</summary>
		// Token: 0x06002F6D RID: 12141 RVA: 0x000D67BB File Offset: 0x000D49BB
		public PerformanceCounterPermission()
		{
			this.SetNames();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermission" /> class with the specified permission state.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />.</exception>
		// Token: 0x06002F6E RID: 12142 RVA: 0x000D67C9 File Offset: 0x000D49C9
		public PerformanceCounterPermission(PermissionState state)
			: base(state)
		{
			this.SetNames();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermission" /> class with the specified access levels, the name of the computer to use, and the category associated with the performance counter.</summary>
		/// <param name="permissionAccess">One of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionAccess" /> values.</param>
		/// <param name="machineName">The server on which the performance counter and its associate category reside.</param>
		/// <param name="categoryName">The name of the performance counter category (performance object) with which the performance counter is associated.</param>
		// Token: 0x06002F6F RID: 12143 RVA: 0x000D67D8 File Offset: 0x000D49D8
		public PerformanceCounterPermission(PerformanceCounterPermissionAccess permissionAccess, string machineName, string categoryName)
		{
			this.SetNames();
			this.AddPermissionAccess(new PerformanceCounterPermissionEntry(permissionAccess, machineName, categoryName));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermission" /> class with the specified permission access level entries.</summary>
		/// <param name="permissionAccessEntries">An array of <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> objects. The <see cref="P:System.Diagnostics.PerformanceCounterPermission.PermissionEntries" /> property is set to this value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="permissionAccessEntries" /> is <see langword="null" />.</exception>
		// Token: 0x06002F70 RID: 12144 RVA: 0x000D67F4 File Offset: 0x000D49F4
		public PerformanceCounterPermission(PerformanceCounterPermissionEntry[] permissionAccessEntries)
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
		/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntryCollection" /> that contains the permission entries for this permissions request.</returns>
		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x06002F71 RID: 12145 RVA: 0x000D6832 File Offset: 0x000D4A32
		public PerformanceCounterPermissionEntryCollection PermissionEntries
		{
			get
			{
				if (this.innerCollection == null)
				{
					this.innerCollection = new PerformanceCounterPermissionEntryCollection(this, base.GetPermissionEntries());
				}
				return this.innerCollection;
			}
		}

		// Token: 0x06002F72 RID: 12146 RVA: 0x000D6854 File Offset: 0x000D4A54
		internal void AddPermissionAccess(PerformanceCounterPermissionEntry entry)
		{
			base.AddPermissionAccess(entry.GetBaseEntry());
		}

		// Token: 0x06002F73 RID: 12147 RVA: 0x000D6862 File Offset: 0x000D4A62
		internal new void Clear()
		{
			base.Clear();
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x000D686A File Offset: 0x000D4A6A
		internal void RemovePermissionAccess(PerformanceCounterPermissionEntry entry)
		{
			base.RemovePermissionAccess(entry.GetBaseEntry());
		}

		// Token: 0x06002F75 RID: 12149 RVA: 0x000D6878 File Offset: 0x000D4A78
		private void SetNames()
		{
			base.PermissionAccessType = typeof(PerformanceCounterPermissionAccess);
			base.TagNames = new string[] { "Machine", "Category" };
		}

		// Token: 0x040027EC RID: 10220
		private PerformanceCounterPermissionEntryCollection innerCollection;
	}
}
