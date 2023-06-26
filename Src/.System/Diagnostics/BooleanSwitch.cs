using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Provides a simple on/off switch that controls debugging and tracing output.</summary>
	// Token: 0x02000492 RID: 1170
	[SwitchLevel(typeof(bool))]
	public class BooleanSwitch : Switch
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.BooleanSwitch" /> class with the specified display name and description.</summary>
		/// <param name="displayName">The name to display on a user interface.</param>
		/// <param name="description">The description of the switch.</param>
		// Token: 0x06002B4C RID: 11084 RVA: 0x000C4C4B File Offset: 0x000C2E4B
		public BooleanSwitch(string displayName, string description)
			: base(displayName, description)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.BooleanSwitch" /> class with the specified display name, description, and default switch value.</summary>
		/// <param name="displayName">The name to display on the user interface.</param>
		/// <param name="description">The description of the switch.</param>
		/// <param name="defaultSwitchValue">The default value of the switch.</param>
		// Token: 0x06002B4D RID: 11085 RVA: 0x000C4C55 File Offset: 0x000C2E55
		public BooleanSwitch(string displayName, string description, string defaultSwitchValue)
			: base(displayName, description, defaultSwitchValue)
		{
		}

		/// <summary>Gets or sets a value indicating whether the switch is enabled or disabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the switch is enabled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permission.</exception>
		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06002B4E RID: 11086 RVA: 0x000C4C60 File Offset: 0x000C2E60
		// (set) Token: 0x06002B4F RID: 11087 RVA: 0x000C4C6D File Offset: 0x000C2E6D
		public bool Enabled
		{
			get
			{
				return base.SwitchSetting != 0;
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				base.SwitchSetting = (value ? 1 : 0);
			}
		}

		/// <summary>Determines whether the new value of the <see cref="P:System.Diagnostics.Switch.Value" /> property can be parsed as a Boolean value.</summary>
		// Token: 0x06002B50 RID: 11088 RVA: 0x000C4C7C File Offset: 0x000C2E7C
		protected override void OnValueChanged()
		{
			bool flag;
			if (bool.TryParse(base.Value, out flag))
			{
				base.SwitchSetting = (flag ? 1 : 0);
				return;
			}
			base.OnValueChanged();
		}
	}
}
