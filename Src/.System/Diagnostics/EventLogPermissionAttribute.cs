using System;
using System.ComponentModel;
using System.Security;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Allows declaritive permission checks for event logging.</summary>
	// Token: 0x020004D1 RID: 1233
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Event, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public class EventLogPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermissionAttribute" /> class.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x06002E6B RID: 11883 RVA: 0x000D177A File Offset: 0x000CF97A
		public EventLogPermissionAttribute(SecurityAction action)
			: base(action)
		{
			this.machineName = ".";
			this.permissionAccess = EventLogPermissionAccess.Write;
		}

		/// <summary>Gets or sets the name of the computer on which events might be read.</summary>
		/// <returns>The name of the computer on which events might be read. The default is ".".</returns>
		/// <exception cref="T:System.ArgumentException">The computer name is invalid.</exception>
		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06002E6C RID: 11884 RVA: 0x000D1796 File Offset: 0x000CF996
		// (set) Token: 0x06002E6D RID: 11885 RVA: 0x000D179E File Offset: 0x000CF99E
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
		/// <returns>A bitwise combination of the <see cref="T:System.Diagnostics.EventLogPermissionAccess" /> values. The default is <see cref="F:System.Diagnostics.EventLogPermissionAccess.Write" />.</returns>
		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06002E6E RID: 11886 RVA: 0x000D17D1 File Offset: 0x000CF9D1
		// (set) Token: 0x06002E6F RID: 11887 RVA: 0x000D17D9 File Offset: 0x000CF9D9
		public EventLogPermissionAccess PermissionAccess
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

		/// <summary>Creates the permission based on the <see cref="P:System.Diagnostics.EventLogPermissionAttribute.MachineName" /> property and the requested access levels that are set through the <see cref="P:System.Diagnostics.EventLogPermissionAttribute.PermissionAccess" /> property on the attribute.</summary>
		/// <returns>An <see cref="T:System.Security.IPermission" /> that represents the created permission.</returns>
		// Token: 0x06002E70 RID: 11888 RVA: 0x000D17E2 File Offset: 0x000CF9E2
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new EventLogPermission(PermissionState.Unrestricted);
			}
			return new EventLogPermission(this.PermissionAccess, this.MachineName);
		}

		// Token: 0x04002764 RID: 10084
		private string machineName;

		// Token: 0x04002765 RID: 10085
		private EventLogPermissionAccess permissionAccess;
	}
}
