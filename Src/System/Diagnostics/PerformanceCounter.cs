using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;
using System.Threading;

namespace System.Diagnostics
{
	/// <summary>Represents a Windows NT performance counter component.</summary>
	// Token: 0x020004DD RID: 1245
	[InstallerType("System.Diagnostics.PerformanceCounterInstaller,System.Configuration.Install, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("PerformanceCounterDesc")]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, SharedState = true)]
	public sealed class PerformanceCounter : Component, ISupportInitialize
	{
		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06002EE5 RID: 12005 RVA: 0x000D2930 File Offset: 0x000D0B30
		private object InstanceLockObject
		{
			get
			{
				if (this.m_InstanceLockObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref this.m_InstanceLockObject, obj, null);
				}
				return this.m_InstanceLockObject;
			}
		}

		/// <summary>Initializes a new, read-only instance of the <see cref="T:System.Diagnostics.PerformanceCounter" /> class, without associating the instance with any system or custom performance counter.</summary>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		// Token: 0x06002EE6 RID: 12006 RVA: 0x000D2960 File Offset: 0x000D0B60
		public PerformanceCounter()
		{
			this.machineName = ".";
			this.categoryName = string.Empty;
			this.counterName = string.Empty;
			this.instanceName = string.Empty;
			this.isReadOnly = true;
			GC.SuppressFinalize(this);
		}

		/// <summary>Initializes a new, read-only instance of the <see cref="T:System.Diagnostics.PerformanceCounter" /> class and associates it with the specified system or custom performance counter and category instance, on the specified computer.</summary>
		/// <param name="categoryName">The name of the performance counter category (performance object) with which this performance counter is associated.</param>
		/// <param name="counterName">The name of the performance counter.</param>
		/// <param name="instanceName">The name of the performance counter category instance, or an empty string (""), if the category contains a single instance.</param>
		/// <param name="machineName">The computer on which the performance counter and its associated category exist.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="categoryName" /> is an empty string ("").  
		/// -or-  
		/// <paramref name="counterName" /> is an empty string ("").  
		/// -or-  
		/// The read/write permission setting requested is invalid for this counter.  
		/// -or-  
		/// The counter does not exist on the specified computer.  
		/// -or-  
		/// The category specified is marked as multi-instance and requires the performance counter to be created with an instance name.  
		/// -or-  
		/// <paramref name="instanceName" /> is longer than 127 characters.  
		/// -or-  
		/// <paramref name="categoryName" /> and <paramref name="counterName" /> have been localized into different languages.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> parameter is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="categoryName" /> or <paramref name="counterName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06002EE7 RID: 12007 RVA: 0x000D29C0 File Offset: 0x000D0BC0
		public PerformanceCounter(string categoryName, string counterName, string instanceName, string machineName)
		{
			this.MachineName = machineName;
			this.CategoryName = categoryName;
			this.CounterName = counterName;
			this.InstanceName = instanceName;
			this.isReadOnly = true;
			this.Initialize();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x000D2A18 File Offset: 0x000D0C18
		internal PerformanceCounter(string categoryName, string counterName, string instanceName, string machineName, bool skipInit)
		{
			this.MachineName = machineName;
			this.CategoryName = categoryName;
			this.CounterName = counterName;
			this.InstanceName = instanceName;
			this.isReadOnly = true;
			this.initialized = true;
			GC.SuppressFinalize(this);
		}

		/// <summary>Initializes a new, read-only instance of the <see cref="T:System.Diagnostics.PerformanceCounter" /> class and associates it with the specified system or custom performance counter and category instance on the local computer.</summary>
		/// <param name="categoryName">The name of the performance counter category (performance object) with which this performance counter is associated.</param>
		/// <param name="counterName">The name of the performance counter.</param>
		/// <param name="instanceName">The name of the performance counter category instance, or an empty string (""), if the category contains a single instance.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="categoryName" /> is an empty string ("").  
		/// -or-  
		/// <paramref name="counterName" /> is an empty string ("").  
		/// -or-  
		/// The category specified is not valid.  
		/// -or-  
		/// The category specified is marked as multi-instance and requires the performance counter to be created with an instance name.  
		/// -or-  
		/// <paramref name="instanceName" /> is longer than 127 characters.  
		/// -or-  
		/// <paramref name="categoryName" /> and <paramref name="counterName" /> have been localized into different languages.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="categoryName" /> or <paramref name="counterName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06002EE9 RID: 12009 RVA: 0x000D2A6E File Offset: 0x000D0C6E
		public PerformanceCounter(string categoryName, string counterName, string instanceName)
			: this(categoryName, counterName, instanceName, true)
		{
		}

		/// <summary>Initializes a new, read-only or read/write instance of the <see cref="T:System.Diagnostics.PerformanceCounter" /> class and associates it with the specified system or custom performance counter and category instance on the local computer.</summary>
		/// <param name="categoryName">The name of the performance counter category (performance object) with which this performance counter is associated.</param>
		/// <param name="counterName">The name of the performance counter.</param>
		/// <param name="instanceName">The name of the performance counter category instance, or an empty string (""), if the category contains a single instance.</param>
		/// <param name="readOnly">
		///   <see langword="true" /> to access a counter in read-only mode; <see langword="false" /> to access a counter in read/write mode.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="categoryName" /> is an empty string ("").  
		/// -or-  
		/// <paramref name="counterName" /> is an empty string ("").  
		/// -or-  
		/// The read/write permission setting requested is invalid for this counter.  
		/// -or-  
		/// The category specified does not exist (if <paramref name="readOnly" /> is <see langword="true" />).  
		/// -or-  
		/// The category specified is not a .NET Framework custom category (if <paramref name="readOnly" /> is <see langword="false" />).  
		/// -or-  
		/// The category specified is marked as multi-instance and requires the performance counter to be created with an instance name.  
		/// -or-  
		/// <paramref name="instanceName" /> is longer than 127 characters.  
		/// -or-  
		/// <paramref name="categoryName" /> and <paramref name="counterName" /> have been localized into different languages.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="categoryName" /> or <paramref name="counterName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06002EEA RID: 12010 RVA: 0x000D2A7C File Offset: 0x000D0C7C
		public PerformanceCounter(string categoryName, string counterName, string instanceName, bool readOnly)
		{
			if (!readOnly)
			{
				PerformanceCounter.VerifyWriteableCounterAllowed();
			}
			this.MachineName = ".";
			this.CategoryName = categoryName;
			this.CounterName = counterName;
			this.InstanceName = instanceName;
			this.isReadOnly = readOnly;
			this.Initialize();
			GC.SuppressFinalize(this);
		}

		/// <summary>Initializes a new, read-only instance of the <see cref="T:System.Diagnostics.PerformanceCounter" /> class and associates it with the specified system or custom performance counter on the local computer. This constructor requires that the category have a single instance.</summary>
		/// <param name="categoryName">The name of the performance counter category (performance object) with which this performance counter is associated.</param>
		/// <param name="counterName">The name of the performance counter.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="categoryName" /> is an empty string ("").  
		/// -or-  
		/// <paramref name="counterName" /> is an empty string ("").  
		/// -or-  
		/// The category specified does not exist.  
		/// -or-  
		/// The category specified is marked as multi-instance and requires the performance counter to be created with an instance name.  
		/// -or-  
		/// <paramref name="categoryName" /> and <paramref name="counterName" /> have been localized into different languages.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="categoryName" /> or <paramref name="counterName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06002EEB RID: 12011 RVA: 0x000D2ADE File Offset: 0x000D0CDE
		public PerformanceCounter(string categoryName, string counterName)
			: this(categoryName, counterName, true)
		{
		}

		/// <summary>Initializes a new, read-only or read/write instance of the <see cref="T:System.Diagnostics.PerformanceCounter" /> class and associates it with the specified system or custom performance counter on the local computer. This constructor requires that the category contain a single instance.</summary>
		/// <param name="categoryName">The name of the performance counter category (performance object) with which this performance counter is associated.</param>
		/// <param name="counterName">The name of the performance counter.</param>
		/// <param name="readOnly">
		///   <see langword="true" /> to access the counter in read-only mode (although the counter itself could be read/write); <see langword="false" /> to access the counter in read/write mode.</param>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="categoryName" /> is an empty string ("").  
		///  -or-  
		///  The <paramref name="counterName" /> is an empty string ("").  
		///  -or-  
		///  The category specified does not exist. (if <paramref name="readOnly" /> is <see langword="true" />).  
		///  -or-  
		///  The category specified is not a .NET Framework custom category (if <paramref name="readOnly" /> is <see langword="false" />).  
		///  -or-  
		///  The category specified is marked as multi-instance and requires the performance counter to be created with an instance name.  
		///  -or-  
		///  <paramref name="categoryName" /> and <paramref name="counterName" /> have been localized into different languages.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="categoryName" /> or <paramref name="counterName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06002EEC RID: 12012 RVA: 0x000D2AE9 File Offset: 0x000D0CE9
		public PerformanceCounter(string categoryName, string counterName, bool readOnly)
			: this(categoryName, counterName, "", readOnly)
		{
		}

		/// <summary>Gets or sets the name of the performance counter category for this performance counter.</summary>
		/// <returns>The name of the performance counter category (performance object) with which this performance counter is associated.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Diagnostics.PerformanceCounter.CategoryName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06002EED RID: 12013 RVA: 0x000D2AF9 File Offset: 0x000D0CF9
		// (set) Token: 0x06002EEE RID: 12014 RVA: 0x000D2B01 File Offset: 0x000D0D01
		[ReadOnly(true)]
		[DefaultValue("")]
		[TypeConverter("System.Diagnostics.Design.CategoryValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[SRDescription("PCCategoryName")]
		[SettingsBindable(true)]
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
				if (this.categoryName == null || string.Compare(this.categoryName, value, StringComparison.OrdinalIgnoreCase) != 0)
				{
					this.categoryName = value;
					this.Close();
				}
			}
		}

		/// <summary>Gets the description for this performance counter.</summary>
		/// <returns>A description of the item or quantity that this performance counter measures.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Diagnostics.PerformanceCounter" /> instance is not associated with a performance counter.  
		///  -or-  
		///  The <see cref="P:System.Diagnostics.PerformanceCounter.InstanceLifetime" /> property is set to <see cref="F:System.Diagnostics.PerformanceCounterInstanceLifetime.Process" /> when using global shared memory.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x06002EEF RID: 12015 RVA: 0x000D2B38 File Offset: 0x000D0D38
		[ReadOnly(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("PC_CounterHelp")]
		public string CounterHelp
		{
			get
			{
				string text = this.categoryName;
				string text2 = this.machineName;
				PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Browse, text2, text);
				performanceCounterPermission.Demand();
				this.Initialize();
				if (this.helpMsg == null)
				{
					this.helpMsg = PerformanceCounterLib.GetCounterHelp(text2, text, this.counterName);
				}
				return this.helpMsg;
			}
		}

		/// <summary>Gets or sets the name of the performance counter that is associated with this <see cref="T:System.Diagnostics.PerformanceCounter" /> instance.</summary>
		/// <returns>The name of the counter, which generally describes the quantity being counted. This name is displayed in the list of counters of the Performance Counter Manager MMC snap in's Add Counters dialog box.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Diagnostics.PerformanceCounter.CounterName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x06002EF0 RID: 12016 RVA: 0x000D2B89 File Offset: 0x000D0D89
		// (set) Token: 0x06002EF1 RID: 12017 RVA: 0x000D2B91 File Offset: 0x000D0D91
		[ReadOnly(true)]
		[DefaultValue("")]
		[TypeConverter("System.Diagnostics.Design.CounterNameConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[SRDescription("PCCounterName")]
		[SettingsBindable(true)]
		public string CounterName
		{
			get
			{
				return this.counterName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this.counterName == null || string.Compare(this.counterName, value, StringComparison.OrdinalIgnoreCase) != 0)
				{
					this.counterName = value;
					this.Close();
				}
			}
		}

		/// <summary>Gets the counter type of the associated performance counter.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterType" /> that describes both how the counter interacts with a monitoring application and the nature of the values it contains (for example, calculated or uncalculated).</returns>
		/// <exception cref="T:System.InvalidOperationException">The instance is not correctly associated with a performance counter.  
		///  -or-  
		///  The <see cref="P:System.Diagnostics.PerformanceCounter.InstanceLifetime" /> property is set to <see cref="F:System.Diagnostics.PerformanceCounterInstanceLifetime.Process" /> when using global shared memory.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x06002EF2 RID: 12018 RVA: 0x000D2BC8 File Offset: 0x000D0DC8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("PC_CounterType")]
		public PerformanceCounterType CounterType
		{
			get
			{
				if (this.counterType == -1)
				{
					string text = this.categoryName;
					string text2 = this.machineName;
					PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Browse, text2, text);
					performanceCounterPermission.Demand();
					this.Initialize();
					CategorySample categorySample = PerformanceCounterLib.GetCategorySample(text2, text);
					CounterDefinitionSample counterDefinitionSample = categorySample.GetCounterDefinitionSample(this.counterName);
					this.counterType = counterDefinitionSample.CounterType;
				}
				return (PerformanceCounterType)this.counterType;
			}
		}

		/// <summary>Gets or sets the lifetime of a process.</summary>
		/// <returns>One of the <see cref="T:System.Diagnostics.PerformanceCounterInstanceLifetime" /> values. The default is <see cref="F:System.Diagnostics.PerformanceCounterInstanceLifetime.Global" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value set is not a member of the <see cref="T:System.Diagnostics.PerformanceCounterInstanceLifetime" /> enumeration.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Diagnostics.PerformanceCounter.InstanceLifetime" /> is set after the <see cref="T:System.Diagnostics.PerformanceCounter" /> has been initialized.</exception>
		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x06002EF3 RID: 12019 RVA: 0x000D2C2A File Offset: 0x000D0E2A
		// (set) Token: 0x06002EF4 RID: 12020 RVA: 0x000D2C32 File Offset: 0x000D0E32
		[DefaultValue(PerformanceCounterInstanceLifetime.Global)]
		[SRDescription("PCInstanceLifetime")]
		public PerformanceCounterInstanceLifetime InstanceLifetime
		{
			get
			{
				return this.instanceLifetime;
			}
			set
			{
				if (value > PerformanceCounterInstanceLifetime.Process || value < PerformanceCounterInstanceLifetime.Global)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.initialized)
				{
					throw new InvalidOperationException(SR.GetString("CantSetLifetimeAfterInitialized"));
				}
				this.instanceLifetime = value;
			}
		}

		/// <summary>Gets or sets an instance name for this performance counter.</summary>
		/// <returns>The name of the performance counter category instance, or an empty string (""), if the counter is a single-instance counter.</returns>
		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06002EF5 RID: 12021 RVA: 0x000D2C66 File Offset: 0x000D0E66
		// (set) Token: 0x06002EF6 RID: 12022 RVA: 0x000D2C6E File Offset: 0x000D0E6E
		[ReadOnly(true)]
		[DefaultValue("")]
		[TypeConverter("System.Diagnostics.Design.InstanceNameConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[SRDescription("PCInstanceName")]
		[SettingsBindable(true)]
		public string InstanceName
		{
			get
			{
				return this.instanceName;
			}
			set
			{
				if (value == null && this.instanceName == null)
				{
					return;
				}
				if ((value == null && this.instanceName != null) || (value != null && this.instanceName == null) || string.Compare(this.instanceName, value, StringComparison.OrdinalIgnoreCase) != 0)
				{
					this.instanceName = value;
					this.Close();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether this <see cref="T:System.Diagnostics.PerformanceCounter" /> instance is in read-only mode.</summary>
		/// <returns>
		///   <see langword="true" />, if the <see cref="T:System.Diagnostics.PerformanceCounter" /> instance is in read-only mode (even if the counter itself is a custom .NET Framework counter); <see langword="false" /> if it is in read/write mode. The default is the value set by the constructor.</returns>
		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06002EF7 RID: 12023 RVA: 0x000D2CAE File Offset: 0x000D0EAE
		// (set) Token: 0x06002EF8 RID: 12024 RVA: 0x000D2CB6 File Offset: 0x000D0EB6
		[Browsable(false)]
		[DefaultValue(true)]
		[MonitoringDescription("PC_ReadOnly")]
		public bool ReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
			set
			{
				if (value != this.isReadOnly)
				{
					if (!value)
					{
						PerformanceCounter.VerifyWriteableCounterAllowed();
					}
					this.isReadOnly = value;
					this.Close();
				}
			}
		}

		/// <summary>Gets or sets the computer name for this performance counter</summary>
		/// <returns>The server on which the performance counter and its associated category reside.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.PerformanceCounter.MachineName" /> format is invalid.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06002EF9 RID: 12025 RVA: 0x000D2CD6 File Offset: 0x000D0ED6
		// (set) Token: 0x06002EFA RID: 12026 RVA: 0x000D2CE0 File Offset: 0x000D0EE0
		[Browsable(false)]
		[DefaultValue(".")]
		[SRDescription("PCMachineName")]
		[SettingsBindable(true)]
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
					throw new ArgumentException(SR.GetString("InvalidParameter", new object[] { "machineName", value }));
				}
				if (this.machineName != value)
				{
					this.machineName = value;
					this.Close();
				}
			}
		}

		/// <summary>Gets or sets the raw, or uncalculated, value of this counter.</summary>
		/// <returns>The raw value of the counter.</returns>
		/// <exception cref="T:System.InvalidOperationException">You are trying to set the counter's raw value, but the counter is read-only.  
		///  -or-  
		///  The instance is not correctly associated with a performance counter.  
		///  -or-  
		///  The <see cref="P:System.Diagnostics.PerformanceCounter.InstanceLifetime" /> property is set to <see cref="F:System.Diagnostics.PerformanceCounterInstanceLifetime.Process" /> when using global shared memory.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06002EFB RID: 12027 RVA: 0x000D2D34 File Offset: 0x000D0F34
		// (set) Token: 0x06002EFC RID: 12028 RVA: 0x000D2D69 File Offset: 0x000D0F69
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("PC_RawValue")]
		public long RawValue
		{
			get
			{
				if (this.ReadOnly)
				{
					return this.NextSample().RawValue;
				}
				this.Initialize();
				return this.sharedCounter.Value;
			}
			set
			{
				if (this.ReadOnly)
				{
					this.ThrowReadOnly();
				}
				this.Initialize();
				this.sharedCounter.Value = value;
			}
		}

		/// <summary>Begins the initialization of a <see cref="T:System.Diagnostics.PerformanceCounter" /> instance used on a form or by another component. The initialization occurs at runtime.</summary>
		// Token: 0x06002EFD RID: 12029 RVA: 0x000D2D8B File Offset: 0x000D0F8B
		public void BeginInit()
		{
			this.Close();
		}

		/// <summary>Closes the performance counter and frees all the resources allocated by this performance counter instance.</summary>
		// Token: 0x06002EFE RID: 12030 RVA: 0x000D2D93 File Offset: 0x000D0F93
		public void Close()
		{
			this.helpMsg = null;
			this.oldSample = CounterSample.Empty;
			this.sharedCounter = null;
			this.initialized = false;
			this.counterType = -1;
		}

		/// <summary>Frees the performance counter library shared state allocated by the counters.</summary>
		// Token: 0x06002EFF RID: 12031 RVA: 0x000D2DBC File Offset: 0x000D0FBC
		public static void CloseSharedResources()
		{
			PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Browse, ".", "*");
			performanceCounterPermission.Demand();
			PerformanceCounterLib.CloseAllLibraries();
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x000D2DE5 File Offset: 0x000D0FE5
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
			base.Dispose(disposing);
		}

		/// <summary>Decrements the associated performance counter by one through an efficient atomic operation.</summary>
		/// <returns>The decremented counter value.</returns>
		/// <exception cref="T:System.InvalidOperationException">The counter is read-only, so the application cannot decrement it.  
		///  -or-  
		///  The instance is not correctly associated with a performance counter.  
		///  -or-  
		///  The <see cref="P:System.Diagnostics.PerformanceCounter.InstanceLifetime" /> property is set to <see cref="F:System.Diagnostics.PerformanceCounterInstanceLifetime.Process" /> when using global shared memory.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		// Token: 0x06002F01 RID: 12033 RVA: 0x000D2DF7 File Offset: 0x000D0FF7
		public long Decrement()
		{
			if (this.ReadOnly)
			{
				this.ThrowReadOnly();
			}
			this.Initialize();
			return this.sharedCounter.Decrement();
		}

		/// <summary>Ends the initialization of a <see cref="T:System.Diagnostics.PerformanceCounter" /> instance that is used on a form or by another component. The initialization occurs at runtime.</summary>
		// Token: 0x06002F02 RID: 12034 RVA: 0x000D2E18 File Offset: 0x000D1018
		public void EndInit()
		{
			this.Initialize();
		}

		/// <summary>Increments or decrements the value of the associated performance counter by a specified amount through an efficient atomic operation.</summary>
		/// <param name="value">The value to increment by. (A negative value decrements the counter.)</param>
		/// <returns>The new counter value.</returns>
		/// <exception cref="T:System.InvalidOperationException">The counter is read-only, so the application cannot increment it.  
		///  -or-  
		///  The instance is not correctly associated with a performance counter.  
		///  -or-  
		///  The <see cref="P:System.Diagnostics.PerformanceCounter.InstanceLifetime" /> property is set to <see cref="F:System.Diagnostics.PerformanceCounterInstanceLifetime.Process" /> when using global shared memory.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		// Token: 0x06002F03 RID: 12035 RVA: 0x000D2E20 File Offset: 0x000D1020
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public long IncrementBy(long value)
		{
			if (this.isReadOnly)
			{
				this.ThrowReadOnly();
			}
			this.Initialize();
			return this.sharedCounter.IncrementBy(value);
		}

		/// <summary>Increments the associated performance counter by one through an efficient atomic operation.</summary>
		/// <returns>The incremented counter value.</returns>
		/// <exception cref="T:System.InvalidOperationException">The counter is read-only, so the application cannot increment it.  
		///  -or-  
		///  The instance is not correctly associated with a performance counter.  
		///  -or-  
		///  The <see cref="P:System.Diagnostics.PerformanceCounter.InstanceLifetime" /> property is set to <see cref="F:System.Diagnostics.PerformanceCounterInstanceLifetime.Process" /> when using global shared memory.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		// Token: 0x06002F04 RID: 12036 RVA: 0x000D2E42 File Offset: 0x000D1042
		public long Increment()
		{
			if (this.isReadOnly)
			{
				this.ThrowReadOnly();
			}
			this.Initialize();
			return this.sharedCounter.Increment();
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x000D2E63 File Offset: 0x000D1063
		private void ThrowReadOnly()
		{
			throw new InvalidOperationException(SR.GetString("ReadOnlyCounter"));
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x000D2E74 File Offset: 0x000D1074
		private static void VerifyWriteableCounterAllowed()
		{
			if (EnvironmentHelpers.IsAppContainerProcess)
			{
				throw new NotSupportedException(SR.GetString("PCNotSupportedUnderAppContainer"));
			}
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x000D2E8D File Offset: 0x000D108D
		private void Initialize()
		{
			if (!this.initialized && !base.DesignMode)
			{
				this.InitializeImpl();
			}
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x000D2EA8 File Offset: 0x000D10A8
		private void InitializeImpl()
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(this.InstanceLockObject, ref flag);
				if (!this.initialized)
				{
					string text = this.categoryName;
					string text2 = this.machineName;
					if (text == string.Empty)
					{
						throw new InvalidOperationException(SR.GetString("CategoryNameMissing"));
					}
					if (this.counterName == string.Empty)
					{
						throw new InvalidOperationException(SR.GetString("CounterNameMissing"));
					}
					if (this.ReadOnly)
					{
						PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Browse, text2, text);
						performanceCounterPermission.Demand();
						if (!PerformanceCounterLib.CounterExists(text2, text, this.counterName))
						{
							throw new InvalidOperationException(SR.GetString("CounterExists", new object[] { text, this.counterName }));
						}
						PerformanceCounterCategoryType categoryType = PerformanceCounterLib.GetCategoryType(text2, text);
						if (categoryType == PerformanceCounterCategoryType.MultiInstance)
						{
							if (string.IsNullOrEmpty(this.instanceName))
							{
								throw new InvalidOperationException(SR.GetString("MultiInstanceOnly", new object[] { text }));
							}
						}
						else if (categoryType == PerformanceCounterCategoryType.SingleInstance && !string.IsNullOrEmpty(this.instanceName))
						{
							throw new InvalidOperationException(SR.GetString("SingleInstanceOnly", new object[] { text }));
						}
						if (this.instanceLifetime != PerformanceCounterInstanceLifetime.Global)
						{
							throw new InvalidOperationException(SR.GetString("InstanceLifetimeProcessonReadOnly"));
						}
						this.initialized = true;
					}
					else
					{
						PerformanceCounterPermission performanceCounterPermission2 = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Write, text2, text);
						performanceCounterPermission2.Demand();
						if (text2 != "." && string.Compare(text2, PerformanceCounterLib.ComputerName, StringComparison.OrdinalIgnoreCase) != 0)
						{
							throw new InvalidOperationException(SR.GetString("RemoteWriting"));
						}
						SharedUtils.CheckNtEnvironment();
						if (!PerformanceCounterLib.IsCustomCategory(text2, text))
						{
							throw new InvalidOperationException(SR.GetString("NotCustomCounter"));
						}
						PerformanceCounterCategoryType categoryType2 = PerformanceCounterLib.GetCategoryType(text2, text);
						if (categoryType2 == PerformanceCounterCategoryType.MultiInstance)
						{
							if (string.IsNullOrEmpty(this.instanceName))
							{
								throw new InvalidOperationException(SR.GetString("MultiInstanceOnly", new object[] { text }));
							}
						}
						else if (categoryType2 == PerformanceCounterCategoryType.SingleInstance && !string.IsNullOrEmpty(this.instanceName))
						{
							throw new InvalidOperationException(SR.GetString("SingleInstanceOnly", new object[] { text }));
						}
						if (string.IsNullOrEmpty(this.instanceName) && this.InstanceLifetime == PerformanceCounterInstanceLifetime.Process)
						{
							throw new InvalidOperationException(SR.GetString("InstanceLifetimeProcessforSingleInstance"));
						}
						this.sharedCounter = new SharedPerformanceCounter(text.ToLower(CultureInfo.InvariantCulture), this.counterName.ToLower(CultureInfo.InvariantCulture), this.instanceName.ToLower(CultureInfo.InvariantCulture), this.instanceLifetime);
						this.initialized = true;
					}
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this.InstanceLockObject);
				}
			}
		}

		/// <summary>Obtains a counter sample, and returns the raw, or uncalculated, value for it.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.CounterSample" /> that represents the next raw value that the system obtains for this counter.</returns>
		/// <exception cref="T:System.InvalidOperationException">The instance is not correctly associated with a performance counter.  
		///  -or-  
		///  The <see cref="P:System.Diagnostics.PerformanceCounter.InstanceLifetime" /> property is set to <see cref="F:System.Diagnostics.PerformanceCounterInstanceLifetime.Process" /> when using global shared memory.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06002F09 RID: 12041 RVA: 0x000D3144 File Offset: 0x000D1344
		public CounterSample NextSample()
		{
			string text = this.categoryName;
			string text2 = this.machineName;
			PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Browse, text2, text);
			performanceCounterPermission.Demand();
			this.Initialize();
			CategorySample categorySample = PerformanceCounterLib.GetCategorySample(text2, text);
			CounterDefinitionSample counterDefinitionSample = categorySample.GetCounterDefinitionSample(this.counterName);
			this.counterType = counterDefinitionSample.CounterType;
			if (!categorySample.IsMultiInstance)
			{
				if (this.instanceName != null && this.instanceName.Length != 0)
				{
					throw new InvalidOperationException(SR.GetString("InstanceNameProhibited", new object[] { this.instanceName }));
				}
				return counterDefinitionSample.GetSingleValue();
			}
			else
			{
				if (this.instanceName == null || this.instanceName.Length == 0)
				{
					throw new InvalidOperationException(SR.GetString("InstanceNameRequired"));
				}
				return counterDefinitionSample.GetInstanceValue(this.instanceName);
			}
		}

		/// <summary>Obtains a counter sample and returns the calculated value for it.</summary>
		/// <returns>The next calculated value that the system obtains for this counter.</returns>
		/// <exception cref="T:System.InvalidOperationException">The instance is not correctly associated with a performance counter.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Code that is executing without administrative privileges attempted to read a performance counter.</exception>
		// Token: 0x06002F0A RID: 12042 RVA: 0x000D3210 File Offset: 0x000D1410
		public float NextValue()
		{
			CounterSample counterSample = this.NextSample();
			float num = CounterSample.Calculate(this.oldSample, counterSample);
			this.oldSample = counterSample;
			return num;
		}

		/// <summary>Deletes the category instance specified by the <see cref="T:System.Diagnostics.PerformanceCounter" /> object <see cref="P:System.Diagnostics.PerformanceCounter.InstanceName" /> property.</summary>
		/// <exception cref="T:System.InvalidOperationException">This counter is read-only, so any instance that is associated with the category cannot be removed.  
		///  -or-  
		///  The instance is not correctly associated with a performance counter.  
		///  -or-  
		///  The <see cref="P:System.Diagnostics.PerformanceCounter.InstanceLifetime" /> property is set to <see cref="F:System.Diagnostics.PerformanceCounterInstanceLifetime.Process" /> when using global shared memory.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when accessing a system API.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Me), which does not support performance counters.</exception>
		// Token: 0x06002F0B RID: 12043 RVA: 0x000D3240 File Offset: 0x000D1440
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public void RemoveInstance()
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException(SR.GetString("ReadOnlyRemoveInstance"));
			}
			this.Initialize();
			this.sharedCounter.RemoveInstance(this.instanceName.ToLower(CultureInfo.InvariantCulture), this.instanceLifetime);
		}

		// Token: 0x04002791 RID: 10129
		private string machineName;

		// Token: 0x04002792 RID: 10130
		private string categoryName;

		// Token: 0x04002793 RID: 10131
		private string counterName;

		// Token: 0x04002794 RID: 10132
		private string instanceName;

		// Token: 0x04002795 RID: 10133
		private PerformanceCounterInstanceLifetime instanceLifetime;

		// Token: 0x04002796 RID: 10134
		private bool isReadOnly;

		// Token: 0x04002797 RID: 10135
		private bool initialized;

		// Token: 0x04002798 RID: 10136
		private string helpMsg;

		// Token: 0x04002799 RID: 10137
		private int counterType = -1;

		// Token: 0x0400279A RID: 10138
		private CounterSample oldSample = CounterSample.Empty;

		// Token: 0x0400279B RID: 10139
		private SharedPerformanceCounter sharedCounter;

		/// <summary>Specifies the size, in bytes, of the global memory shared by performance counters. The default size is 524,288 bytes.</summary>
		// Token: 0x0400279C RID: 10140
		[Obsolete("This field has been deprecated and is not used.  Use machine.config or an application configuration file to set the size of the PerformanceCounter file mapping.")]
		public static int DefaultFileMappingSize = 524288;

		// Token: 0x0400279D RID: 10141
		private object m_InstanceLockObject;
	}
}
