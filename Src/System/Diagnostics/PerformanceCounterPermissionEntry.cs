using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Defines the smallest unit of a code access security permission that is set for a <see cref="T:System.Diagnostics.PerformanceCounter" />.</summary>
	// Token: 0x020004EB RID: 1259
	[Serializable]
	public class PerformanceCounterPermissionEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> class.</summary>
		/// <param name="permissionAccess">A bitwise combination of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionAccess" /> values. The <see cref="P:System.Diagnostics.PerformanceCounterPermissionEntry.PermissionAccess" /> property is set to this value.</param>
		/// <param name="machineName">The server on which the category of the performance counter resides.</param>
		/// <param name="categoryName">The name of the performance counter category (performance object) with which this performance counter is associated.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="categoryName" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="machineName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="permissionAccess" /> is not a valid <see cref="T:System.Diagnostics.PerformanceCounterPermissionAccess" /> value.  
		/// -or-  
		/// <paramref name="machineName" /> is not a valid computer name.</exception>
		// Token: 0x06002F7E RID: 12158 RVA: 0x000D6960 File Offset: 0x000D4B60
		public PerformanceCounterPermissionEntry(PerformanceCounterPermissionAccess permissionAccess, string machineName, string categoryName)
		{
			if (categoryName == null)
			{
				throw new ArgumentNullException("categoryName");
			}
			if ((permissionAccess & (PerformanceCounterPermissionAccess)(-8)) != PerformanceCounterPermissionAccess.None)
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[] { "permissionAccess", permissionAccess }));
			}
			if (machineName == null)
			{
				throw new ArgumentNullException("machineName");
			}
			if (!SyntaxCheck.CheckMachineName(machineName))
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[] { "MachineName", machineName }));
			}
			this.permissionAccess = permissionAccess;
			this.machineName = machineName;
			this.categoryName = categoryName;
		}

		// Token: 0x06002F7F RID: 12159 RVA: 0x000D69FB File Offset: 0x000D4BFB
		internal PerformanceCounterPermissionEntry(ResourcePermissionBaseEntry baseEntry)
		{
			this.permissionAccess = (PerformanceCounterPermissionAccess)baseEntry.PermissionAccess;
			this.machineName = baseEntry.PermissionAccessPath[0];
			this.categoryName = baseEntry.PermissionAccessPath[1];
		}

		/// <summary>Gets the name of the performance counter category (performance object).</summary>
		/// <returns>The name of the performance counter category (performance object).</returns>
		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x06002F80 RID: 12160 RVA: 0x000D6A2B File Offset: 0x000D4C2B
		public string CategoryName
		{
			get
			{
				return this.categoryName;
			}
		}

		/// <summary>Gets the name of the server on which the category of the performance counter resides.</summary>
		/// <returns>The name of the server on which the category resides.</returns>
		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06002F81 RID: 12161 RVA: 0x000D6A33 File Offset: 0x000D4C33
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		/// <summary>Gets the permission access level of the entry.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionAccess" /> values.</returns>
		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x06002F82 RID: 12162 RVA: 0x000D6A3B File Offset: 0x000D4C3B
		public PerformanceCounterPermissionAccess PermissionAccess
		{
			get
			{
				return this.permissionAccess;
			}
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x000D6A44 File Offset: 0x000D4C44
		internal ResourcePermissionBaseEntry GetBaseEntry()
		{
			return new ResourcePermissionBaseEntry((int)this.PermissionAccess, new string[] { this.MachineName, this.CategoryName });
		}

		// Token: 0x040027F7 RID: 10231
		private string categoryName;

		// Token: 0x040027F8 RID: 10232
		private string machineName;

		// Token: 0x040027F9 RID: 10233
		private PerformanceCounterPermissionAccess permissionAccess;
	}
}
