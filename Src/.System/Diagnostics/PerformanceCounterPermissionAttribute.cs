using System;
using System.ComponentModel;
using System.Security;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Allows declaritive performance counter permission checks.</summary>
	// Token: 0x020004EA RID: 1258
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Event, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public class PerformanceCounterPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionAttribute" /> class.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x06002F76 RID: 12150 RVA: 0x000D68A6 File Offset: 0x000D4AA6
		public PerformanceCounterPermissionAttribute(SecurityAction action)
			: base(action)
		{
			this.categoryName = "*";
			this.machineName = ".";
			this.permissionAccess = PerformanceCounterPermissionAccess.Write;
		}

		/// <summary>Gets or sets the name of the performance counter category.</summary>
		/// <returns>The name of the performance counter category (performance object).</returns>
		/// <exception cref="T:System.ArgumentNullException">The value is <see langword="null" />.</exception>
		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06002F77 RID: 12151 RVA: 0x000D68CC File Offset: 0x000D4ACC
		// (set) Token: 0x06002F78 RID: 12152 RVA: 0x000D68D4 File Offset: 0x000D4AD4
		public string CategoryName
		{
			get
			{
				return this.categoryName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.categoryName = value;
			}
		}

		/// <summary>Gets or sets the computer name for the performance counter.</summary>
		/// <returns>The server on which the category of the performance counter resides.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.PerformanceCounterPermissionAttribute.MachineName" /> format is invalid.</exception>
		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06002F79 RID: 12153 RVA: 0x000D68EB File Offset: 0x000D4AEB
		// (set) Token: 0x06002F7A RID: 12154 RVA: 0x000D68F3 File Offset: 0x000D4AF3
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
			set
			{
				if (!SyntaxCheck.CheckMachineName(value))
				{
					throw new ArgumentException(SR.GetString("InvalidProperty", new object[] { "MachineName", value }));
				}
				this.machineName = value;
			}
		}

		/// <summary>Gets or sets the access levels used in the permissions request.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Diagnostics.PerformanceCounterPermissionAccess" /> values. The default is <see cref="F:System.Diagnostics.EventLogPermissionAccess.Write" />.</returns>
		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06002F7B RID: 12155 RVA: 0x000D6926 File Offset: 0x000D4B26
		// (set) Token: 0x06002F7C RID: 12156 RVA: 0x000D692E File Offset: 0x000D4B2E
		public PerformanceCounterPermissionAccess PermissionAccess
		{
			get
			{
				return this.permissionAccess;
			}
			set
			{
				this.permissionAccess = value;
			}
		}

		/// <summary>Creates the permission based on the requested access levels that are set through the <see cref="P:System.Diagnostics.PerformanceCounterPermissionAttribute.PermissionAccess" /> property on the attribute.</summary>
		/// <returns>An <see cref="T:System.Security.IPermission" /> that represents the created permission.</returns>
		// Token: 0x06002F7D RID: 12157 RVA: 0x000D6937 File Offset: 0x000D4B37
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new PerformanceCounterPermission(PermissionState.Unrestricted);
			}
			return new PerformanceCounterPermission(this.PermissionAccess, this.MachineName, this.CategoryName);
		}

		// Token: 0x040027F4 RID: 10228
		private string categoryName;

		// Token: 0x040027F5 RID: 10229
		private string machineName;

		// Token: 0x040027F6 RID: 10230
		private PerformanceCounterPermissionAccess permissionAccess;
	}
}
