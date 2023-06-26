using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Provides a multilevel switch to control tracing and debug output without recompiling your code.</summary>
	// Token: 0x020004B7 RID: 1207
	[SwitchLevel(typeof(TraceLevel))]
	public class TraceSwitch : Switch
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TraceSwitch" /> class, using the specified display name and description.</summary>
		/// <param name="displayName">The name to display on a user interface.</param>
		/// <param name="description">The description of the switch.</param>
		// Token: 0x06002D12 RID: 11538 RVA: 0x000CAF9D File Offset: 0x000C919D
		public TraceSwitch(string displayName, string description)
			: base(displayName, description)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TraceSwitch" /> class, using the specified display name, description, and default value for the switch.</summary>
		/// <param name="displayName">The name to display on a user interface.</param>
		/// <param name="description">The description of the switch.</param>
		/// <param name="defaultSwitchValue">The default value of the switch.</param>
		// Token: 0x06002D13 RID: 11539 RVA: 0x000CAFA7 File Offset: 0x000C91A7
		public TraceSwitch(string displayName, string description, string defaultSwitchValue)
			: base(displayName, description, defaultSwitchValue)
		{
		}

		/// <summary>Gets or sets the trace level that determines the messages the switch allows.</summary>
		/// <returns>One of the <see cref="T:System.Diagnostics.TraceLevel" /> values that specifies the level of messages that are allowed by the switch.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Diagnostics.TraceSwitch.Level" /> is set to a value that is not one of the <see cref="T:System.Diagnostics.TraceLevel" /> values.</exception>
		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06002D14 RID: 11540 RVA: 0x000CAFB2 File Offset: 0x000C91B2
		// (set) Token: 0x06002D15 RID: 11541 RVA: 0x000CAFBA File Offset: 0x000C91BA
		public TraceLevel Level
		{
			get
			{
				return (TraceLevel)base.SwitchSetting;
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				if (value < TraceLevel.Off || value > TraceLevel.Verbose)
				{
					throw new ArgumentException(SR.GetString("TraceSwitchInvalidLevel"));
				}
				base.SwitchSetting = (int)value;
			}
		}

		/// <summary>Gets a value indicating whether the switch allows error-handling messages.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Diagnostics.TraceSwitch.Level" /> property is set to <see cref="F:System.Diagnostics.TraceLevel.Error" />, <see cref="F:System.Diagnostics.TraceLevel.Warning" />, <see cref="F:System.Diagnostics.TraceLevel.Info" />, or <see cref="F:System.Diagnostics.TraceLevel.Verbose" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06002D16 RID: 11542 RVA: 0x000CAFDB File Offset: 0x000C91DB
		public bool TraceError
		{
			get
			{
				return this.Level >= TraceLevel.Error;
			}
		}

		/// <summary>Gets a value indicating whether the switch allows warning messages.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Diagnostics.TraceSwitch.Level" /> property is set to <see cref="F:System.Diagnostics.TraceLevel.Warning" />, <see cref="F:System.Diagnostics.TraceLevel.Info" />, or <see cref="F:System.Diagnostics.TraceLevel.Verbose" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06002D17 RID: 11543 RVA: 0x000CAFE9 File Offset: 0x000C91E9
		public bool TraceWarning
		{
			get
			{
				return this.Level >= TraceLevel.Warning;
			}
		}

		/// <summary>Gets a value indicating whether the switch allows informational messages.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Diagnostics.TraceSwitch.Level" /> property is set to <see cref="F:System.Diagnostics.TraceLevel.Info" /> or <see cref="F:System.Diagnostics.TraceLevel.Verbose" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06002D18 RID: 11544 RVA: 0x000CAFF7 File Offset: 0x000C91F7
		public bool TraceInfo
		{
			get
			{
				return this.Level >= TraceLevel.Info;
			}
		}

		/// <summary>Gets a value indicating whether the switch allows all messages.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Diagnostics.TraceSwitch.Level" /> property is set to <see cref="F:System.Diagnostics.TraceLevel.Verbose" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06002D19 RID: 11545 RVA: 0x000CB005 File Offset: 0x000C9205
		public bool TraceVerbose
		{
			get
			{
				return this.Level == TraceLevel.Verbose;
			}
		}

		/// <summary>Updates and corrects the level for this switch.</summary>
		// Token: 0x06002D1A RID: 11546 RVA: 0x000CB010 File Offset: 0x000C9210
		protected override void OnSwitchSettingChanged()
		{
			int switchSetting = base.SwitchSetting;
			if (switchSetting < 0)
			{
				Trace.WriteLine(SR.GetString("TraceSwitchLevelTooLow", new object[] { base.DisplayName }));
				base.SwitchSetting = 0;
				return;
			}
			if (switchSetting > 4)
			{
				Trace.WriteLine(SR.GetString("TraceSwitchLevelTooHigh", new object[] { base.DisplayName }));
				base.SwitchSetting = 4;
			}
		}

		/// <summary>Sets the <see cref="P:System.Diagnostics.Switch.SwitchSetting" /> property to the integer equivalent of the <see cref="P:System.Diagnostics.Switch.Value" /> property.</summary>
		// Token: 0x06002D1B RID: 11547 RVA: 0x000CB077 File Offset: 0x000C9277
		protected override void OnValueChanged()
		{
			base.SwitchSetting = (int)Enum.Parse(typeof(TraceLevel), base.Value, true);
		}
	}
}
