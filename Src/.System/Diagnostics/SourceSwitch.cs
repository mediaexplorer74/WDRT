using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Provides a multilevel switch to control tracing and debug output without recompiling your code.</summary>
	// Token: 0x020004A4 RID: 1188
	public class SourceSwitch : Switch
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.SourceSwitch" /> class, specifying the name of the source.</summary>
		/// <param name="name">The name of the source.</param>
		// Token: 0x06002BF6 RID: 11254 RVA: 0x000C6A5F File Offset: 0x000C4C5F
		public SourceSwitch(string name)
			: base(name, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.SourceSwitch" /> class, specifying the display name and the default value for the source switch.</summary>
		/// <param name="displayName">The name of the source switch.</param>
		/// <param name="defaultSwitchValue">The default value for the switch.</param>
		// Token: 0x06002BF7 RID: 11255 RVA: 0x000C6A6D File Offset: 0x000C4C6D
		public SourceSwitch(string displayName, string defaultSwitchValue)
			: base(displayName, string.Empty, defaultSwitchValue)
		{
		}

		/// <summary>Gets or sets the level of the switch.</summary>
		/// <returns>One of the <see cref="T:System.Diagnostics.SourceLevels" /> values that represents the event level of the switch.</returns>
		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06002BF8 RID: 11256 RVA: 0x000C6A7C File Offset: 0x000C4C7C
		// (set) Token: 0x06002BF9 RID: 11257 RVA: 0x000C6A84 File Offset: 0x000C4C84
		public SourceLevels Level
		{
			get
			{
				return (SourceLevels)base.SwitchSetting;
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				base.SwitchSetting = (int)value;
			}
		}

		/// <summary>Determines if trace listeners should be called, based on the trace event type.</summary>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values.</param>
		/// <returns>
		///   <see langword="true" /> if the trace listeners should be called; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002BFA RID: 11258 RVA: 0x000C6A8D File Offset: 0x000C4C8D
		public bool ShouldTrace(TraceEventType eventType)
		{
			return (base.SwitchSetting & (int)eventType) != 0;
		}

		/// <summary>Invoked when the value of the <see cref="P:System.Diagnostics.Switch.Value" /> property changes.</summary>
		/// <exception cref="T:System.ArgumentException">The new value of <see cref="P:System.Diagnostics.Switch.Value" /> is not one of the <see cref="T:System.Diagnostics.SourceLevels" /> values.</exception>
		// Token: 0x06002BFB RID: 11259 RVA: 0x000C6A9A File Offset: 0x000C4C9A
		protected override void OnValueChanged()
		{
			base.SwitchSetting = (int)Enum.Parse(typeof(SourceLevels), base.Value, true);
		}
	}
}
