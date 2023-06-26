using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Diagnostics
{
	/// <summary>Provides interaction with Windows event logs.</summary>
	// Token: 0x020004CA RID: 1226
	[DefaultEvent("EntryWritten")]
	[InstallerType("System.Diagnostics.EventLogInstaller, System.Configuration.Install, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[MonitoringDescription("EventLogDesc")]
	public class EventLog : Component, ISupportInitialize
	{
		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06002DA7 RID: 11687 RVA: 0x000CD4EC File Offset: 0x000CB6EC
		private static bool SkipRegPatch
		{
			get
			{
				if (!EventLog.s_CheckedOsVersion)
				{
					OperatingSystem osversion = Environment.OSVersion;
					EventLog.s_SkipRegPatch = osversion.Platform == PlatformID.Win32NT && osversion.Version.Major > 5;
					EventLog.s_CheckedOsVersion = true;
				}
				return EventLog.s_SkipRegPatch;
			}
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x000CD538 File Offset: 0x000CB738
		internal static PermissionSet _UnsafeGetAssertPermSet()
		{
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			RegistryPermission registryPermission = new RegistryPermission(PermissionState.Unrestricted);
			permissionSet.AddPermission(registryPermission);
			EnvironmentPermission environmentPermission = new EnvironmentPermission(PermissionState.Unrestricted);
			permissionSet.AddPermission(environmentPermission);
			SecurityPermission securityPermission = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
			permissionSet.AddPermission(securityPermission);
			return permissionSet;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLog" /> class. Does not associate the instance with any log.</summary>
		// Token: 0x06002DA9 RID: 11689 RVA: 0x000CD57A File Offset: 0x000CB77A
		public EventLog()
			: this("", ".", "")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLog" /> class. Associates the instance with a log on the local computer.</summary>
		/// <param name="logName">The name of the log on the local computer.</param>
		/// <exception cref="T:System.ArgumentNullException">The log name is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The log name is invalid.</exception>
		// Token: 0x06002DAA RID: 11690 RVA: 0x000CD591 File Offset: 0x000CB791
		public EventLog(string logName)
			: this(logName, ".", "")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLog" /> class. Associates the instance with a log on the specified computer.</summary>
		/// <param name="logName">The name of the log on the specified computer.</param>
		/// <param name="machineName">The computer on which the log exists.</param>
		/// <exception cref="T:System.ArgumentNullException">The log name is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The log name is invalid.  
		///  -or-  
		///  The computer name is invalid.</exception>
		// Token: 0x06002DAB RID: 11691 RVA: 0x000CD5A4 File Offset: 0x000CB7A4
		public EventLog(string logName, string machineName)
			: this(logName, machineName, "")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLog" /> class. Associates the instance with a log on the specified computer and creates or assigns the specified source to the <see cref="T:System.Diagnostics.EventLog" />.</summary>
		/// <param name="logName">The name of the log on the specified computer</param>
		/// <param name="machineName">The computer on which the log exists.</param>
		/// <param name="source">The source of event log entries.</param>
		/// <exception cref="T:System.ArgumentNullException">The log name is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The log name is invalid.  
		///  -or-  
		///  The computer name is invalid.</exception>
		// Token: 0x06002DAC RID: 11692 RVA: 0x000CD5B3 File Offset: 0x000CB7B3
		public EventLog(string logName, string machineName, string source)
		{
			this.m_underlyingEventLog = new EventLogInternal(logName, machineName, source, this);
		}

		/// <summary>Gets the contents of the event log.</summary>
		/// <returns>An <see cref="T:System.Diagnostics.EventLogEntryCollection" /> holding the entries in the event log. Each entry is associated with an instance of the <see cref="T:System.Diagnostics.EventLogEntry" /> class.</returns>
		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06002DAD RID: 11693 RVA: 0x000CD5CA File Offset: 0x000CB7CA
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("LogEntries")]
		public EventLogEntryCollection Entries
		{
			get
			{
				return this.m_underlyingEventLog.Entries;
			}
		}

		/// <summary>Gets the event log's friendly name.</summary>
		/// <returns>A name that represents the event log in the system's event viewer.</returns>
		/// <exception cref="T:System.InvalidOperationException">The specified <see cref="P:System.Diagnostics.EventLog.Log" /> does not exist in the registry for this computer.</exception>
		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x06002DAE RID: 11694 RVA: 0x000CD5D7 File Offset: 0x000CB7D7
		[Browsable(false)]
		public string LogDisplayName
		{
			get
			{
				return this.m_underlyingEventLog.LogDisplayName;
			}
		}

		/// <summary>Gets or sets the name of the log to read from or write to.</summary>
		/// <returns>The name of the log. This can be Application, System, Security, or a custom log name. The default is an empty string ("").</returns>
		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06002DAF RID: 11695 RVA: 0x000CD5E4 File Offset: 0x000CB7E4
		// (set) Token: 0x06002DB0 RID: 11696 RVA: 0x000CD5F4 File Offset: 0x000CB7F4
		[TypeConverter("System.Diagnostics.Design.LogConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[ReadOnly(true)]
		[MonitoringDescription("LogLog")]
		[DefaultValue("")]
		[SettingsBindable(true)]
		public string Log
		{
			get
			{
				return this.m_underlyingEventLog.Log;
			}
			set
			{
				EventLogInternal eventLogInternal = new EventLogInternal(value, this.m_underlyingEventLog.MachineName, this.m_underlyingEventLog.Source, this);
				EventLogInternal underlyingEventLog = this.m_underlyingEventLog;
				new EventLogPermission(EventLogPermissionAccess.Write, underlyingEventLog.machineName).Assert();
				if (underlyingEventLog.EnableRaisingEvents)
				{
					eventLogInternal.onEntryWrittenHandler = underlyingEventLog.onEntryWrittenHandler;
					eventLogInternal.EnableRaisingEvents = true;
				}
				this.m_underlyingEventLog = eventLogInternal;
				underlyingEventLog.Close();
			}
		}

		/// <summary>Gets or sets the name of the computer on which to read or write events.</summary>
		/// <returns>The name of the server on which the event log resides. The default is the local computer (".").</returns>
		/// <exception cref="T:System.ArgumentException">The computer name is invalid.</exception>
		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06002DB1 RID: 11697 RVA: 0x000CD660 File Offset: 0x000CB860
		// (set) Token: 0x06002DB2 RID: 11698 RVA: 0x000CD670 File Offset: 0x000CB870
		[ReadOnly(true)]
		[MonitoringDescription("LogMachineName")]
		[DefaultValue(".")]
		[SettingsBindable(true)]
		public string MachineName
		{
			get
			{
				return this.m_underlyingEventLog.MachineName;
			}
			set
			{
				EventLogInternal eventLogInternal = new EventLogInternal(this.m_underlyingEventLog.logName, value, this.m_underlyingEventLog.sourceName, this);
				EventLogInternal underlyingEventLog = this.m_underlyingEventLog;
				new EventLogPermission(EventLogPermissionAccess.Write, underlyingEventLog.machineName).Assert();
				if (underlyingEventLog.EnableRaisingEvents)
				{
					eventLogInternal.onEntryWrittenHandler = underlyingEventLog.onEntryWrittenHandler;
					eventLogInternal.EnableRaisingEvents = true;
				}
				this.m_underlyingEventLog = eventLogInternal;
				underlyingEventLog.Close();
			}
		}

		/// <summary>Gets or sets the maximum event log size in kilobytes.</summary>
		/// <returns>The maximum event log size in kilobytes. The default is 512, indicating a maximum file size of 512 kilobytes.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is less than 64, or greater than 4194240, or not an even multiple of 64.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.EventLog.Log" /> value is not a valid log name.  
		/// -or-
		///  The registry key for the event log could not be opened on the target computer.</exception>
		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06002DB3 RID: 11699 RVA: 0x000CD6DC File Offset: 0x000CB8DC
		// (set) Token: 0x06002DB4 RID: 11700 RVA: 0x000CD6E9 File Offset: 0x000CB8E9
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[ComVisible(false)]
		public long MaximumKilobytes
		{
			get
			{
				return this.m_underlyingEventLog.MaximumKilobytes;
			}
			set
			{
				this.m_underlyingEventLog.MaximumKilobytes = value;
			}
		}

		/// <summary>Gets the configured behavior for storing new entries when the event log reaches its maximum log file size.</summary>
		/// <returns>The <see cref="T:System.Diagnostics.OverflowAction" /> value that specifies the configured behavior for storing new entries when the event log reaches its maximum log size. The default is <see cref="F:System.Diagnostics.OverflowAction.OverwriteOlder" />.</returns>
		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06002DB5 RID: 11701 RVA: 0x000CD6F7 File Offset: 0x000CB8F7
		[Browsable(false)]
		[ComVisible(false)]
		public OverflowAction OverflowAction
		{
			get
			{
				return this.m_underlyingEventLog.OverflowAction;
			}
		}

		/// <summary>Gets the number of days to retain entries in the event log.</summary>
		/// <returns>The number of days that entries in the event log are retained. The default value is 7.</returns>
		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06002DB6 RID: 11702 RVA: 0x000CD704 File Offset: 0x000CB904
		[Browsable(false)]
		[ComVisible(false)]
		public int MinimumRetentionDays
		{
			get
			{
				return this.m_underlyingEventLog.MinimumRetentionDays;
			}
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06002DB7 RID: 11703 RVA: 0x000CD711 File Offset: 0x000CB911
		internal bool ComponentDesignMode
		{
			get
			{
				return base.DesignMode;
			}
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x000CD719 File Offset: 0x000CB919
		internal object ComponentGetService(Type service)
		{
			return this.GetService(service);
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Diagnostics.EventLog" /> receives <see cref="E:System.Diagnostics.EventLog.EntryWritten" /> event notifications.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Diagnostics.EventLog" /> receives notification when an entry is written to the log; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The event log is on a remote computer.</exception>
		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06002DB9 RID: 11705 RVA: 0x000CD722 File Offset: 0x000CB922
		// (set) Token: 0x06002DBA RID: 11706 RVA: 0x000CD72F File Offset: 0x000CB92F
		[Browsable(false)]
		[MonitoringDescription("LogMonitoring")]
		[DefaultValue(false)]
		public bool EnableRaisingEvents
		{
			get
			{
				return this.m_underlyingEventLog.EnableRaisingEvents;
			}
			set
			{
				this.m_underlyingEventLog.EnableRaisingEvents = value;
			}
		}

		/// <summary>Gets or sets the object used to marshal the event handler calls issued as a result of an <see cref="T:System.Diagnostics.EventLog" /> entry written event.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISynchronizeInvoke" /> used to marshal event-handler calls issued as a result of an <see cref="E:System.Diagnostics.EventLog.EntryWritten" /> event on the event log.</returns>
		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06002DBB RID: 11707 RVA: 0x000CD73D File Offset: 0x000CB93D
		// (set) Token: 0x06002DBC RID: 11708 RVA: 0x000CD74A File Offset: 0x000CB94A
		[Browsable(false)]
		[DefaultValue(null)]
		[MonitoringDescription("LogSynchronizingObject")]
		public ISynchronizeInvoke SynchronizingObject
		{
			[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
			get
			{
				return this.m_underlyingEventLog.SynchronizingObject;
			}
			set
			{
				this.m_underlyingEventLog.SynchronizingObject = value;
			}
		}

		/// <summary>Gets or sets the source name to register and use when writing to the event log.</summary>
		/// <returns>The name registered with the event log as a source of entries. The default is an empty string ("").</returns>
		/// <exception cref="T:System.ArgumentException">The source name results in a registry key path longer than 254 characters.</exception>
		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06002DBD RID: 11709 RVA: 0x000CD758 File Offset: 0x000CB958
		// (set) Token: 0x06002DBE RID: 11710 RVA: 0x000CD768 File Offset: 0x000CB968
		[ReadOnly(true)]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MonitoringDescription("LogSource")]
		[DefaultValue("")]
		[SettingsBindable(true)]
		public string Source
		{
			get
			{
				return this.m_underlyingEventLog.Source;
			}
			set
			{
				EventLogInternal eventLogInternal = new EventLogInternal(this.m_underlyingEventLog.Log, this.m_underlyingEventLog.MachineName, EventLog.CheckAndNormalizeSourceName(value), this);
				EventLogInternal underlyingEventLog = this.m_underlyingEventLog;
				new EventLogPermission(EventLogPermissionAccess.Write, underlyingEventLog.machineName).Assert();
				if (underlyingEventLog.EnableRaisingEvents)
				{
					eventLogInternal.onEntryWrittenHandler = underlyingEventLog.onEntryWrittenHandler;
					eventLogInternal.EnableRaisingEvents = true;
				}
				this.m_underlyingEventLog = eventLogInternal;
				underlyingEventLog.Close();
			}
		}

		/// <summary>Occurs when an entry is written to an event log on the local computer.</summary>
		// Token: 0x14000040 RID: 64
		// (add) Token: 0x06002DBF RID: 11711 RVA: 0x000CD7D9 File Offset: 0x000CB9D9
		// (remove) Token: 0x06002DC0 RID: 11712 RVA: 0x000CD7E7 File Offset: 0x000CB9E7
		[MonitoringDescription("LogEntryWritten")]
		public event EntryWrittenEventHandler EntryWritten
		{
			add
			{
				this.m_underlyingEventLog.EntryWritten += value;
			}
			remove
			{
				this.m_underlyingEventLog.EntryWritten -= value;
			}
		}

		/// <summary>Begins the initialization of an <see cref="T:System.Diagnostics.EventLog" /> used on a form or used by another component. The initialization occurs at runtime.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="T:System.Diagnostics.EventLog" /> is already initialized.</exception>
		// Token: 0x06002DC1 RID: 11713 RVA: 0x000CD7F5 File Offset: 0x000CB9F5
		public void BeginInit()
		{
			this.m_underlyingEventLog.BeginInit();
		}

		/// <summary>Removes all entries from the event log.</summary>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The event log was not cleared successfully.  
		///  -or-  
		///  The log cannot be opened. A Windows error code is not available.</exception>
		/// <exception cref="T:System.ArgumentException">A value is not specified for the <see cref="P:System.Diagnostics.EventLog.Log" /> property. Make sure the log name is not an empty string.</exception>
		/// <exception cref="T:System.InvalidOperationException">The log does not exist.</exception>
		// Token: 0x06002DC2 RID: 11714 RVA: 0x000CD802 File Offset: 0x000CBA02
		public void Clear()
		{
			this.m_underlyingEventLog.Clear();
		}

		/// <summary>Closes the event log and releases read and write handles.</summary>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The event log's read handle or write handle was not released successfully.</exception>
		// Token: 0x06002DC3 RID: 11715 RVA: 0x000CD80F File Offset: 0x000CBA0F
		public void Close()
		{
			this.m_underlyingEventLog.Close();
		}

		/// <summary>Establishes the specified source name as a valid event source for writing entries to a log on the local computer. This method can also create a new custom log on the local computer.</summary>
		/// <param name="source">The source name by which the application is registered on the local computer.</param>
		/// <param name="logName">The name of the log the source's entries are written to. Possible values include Application, System, or a custom event log.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is an empty string ("") or <see langword="null" />.  
		/// -or-
		///  <paramref name="logName" /> is not a valid event log name. Event log names must consist of printable characters, and cannot include the characters '*', '?', or '\'.  
		/// -or-
		///  <paramref name="logName" /> is not valid for user log creation. The event log names AppEvent, SysEvent, and SecEvent are reserved for system use.  
		/// -or-
		///  The log name matches an existing event source name.  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.  
		/// -or-
		///  The first 8 characters of <paramref name="logName" /> match the first 8 characters of an existing event log name.  
		/// -or-
		///  The source cannot be registered because it already exists on the local computer.  
		/// -or-
		///  The source name matches an existing event log name.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened on the local computer.</exception>
		// Token: 0x06002DC4 RID: 11716 RVA: 0x000CD81C File Offset: 0x000CBA1C
		public static void CreateEventSource(string source, string logName)
		{
			EventLog.CreateEventSource(new EventSourceCreationData(source, logName, "."));
		}

		/// <summary>Establishes the specified source name as a valid event source for writing entries to a log on the specified computer. This method can also be used to create a new custom log on the specified computer.</summary>
		/// <param name="source">The source by which the application is registered on the specified computer.</param>
		/// <param name="logName">The name of the log the source's entries are written to. Possible values include Application, System, or a custom event log. If you do not specify a value, <paramref name="logName" /> defaults to Application.</param>
		/// <param name="machineName">The name of the computer to register this event source with, or "." for the local computer.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> is not a valid computer name.  
		/// -or-
		///  <paramref name="source" /> is an empty string ("") or <see langword="null" />.  
		/// -or-
		///  <paramref name="logName" /> is not a valid event log name. Event log names must consist of printable characters, and cannot include the characters '*', '?', or '\'.  
		/// -or-
		///  <paramref name="logName" /> is not valid for user log creation. The event log names AppEvent, SysEvent, and SecEvent are reserved for system use.  
		/// -or-
		///  The log name matches an existing event source name.  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.  
		/// -or-
		///  The first 8 characters of <paramref name="logName" /> match the first 8 characters of an existing event log name on the specified computer.  
		/// -or-
		///  The source cannot be registered because it already exists on the specified computer.  
		/// -or-
		///  The source name matches an existing event source name.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened on the specified computer.</exception>
		// Token: 0x06002DC5 RID: 11717 RVA: 0x000CD82F File Offset: 0x000CBA2F
		[Obsolete("This method has been deprecated.  Please use System.Diagnostics.EventLog.CreateEventSource(EventSourceCreationData sourceData) instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public static void CreateEventSource(string source, string logName, string machineName)
		{
			EventLog.CreateEventSource(new EventSourceCreationData(source, logName, machineName));
		}

		/// <summary>Establishes a valid event source for writing localized event messages, using the specified configuration properties for the event source and the corresponding event log.</summary>
		/// <param name="sourceData">The configuration properties for the event source and its target event log.</param>
		/// <exception cref="T:System.ArgumentException">The computer name specified in <paramref name="sourceData" /> is not valid.  
		/// -or-
		///  The source name specified in <paramref name="sourceData" /> is <see langword="null" />.  
		/// -or-
		///  The log name specified in <paramref name="sourceData" /> is not valid. Event log names must consist of printable characters and cannot include the characters '*', '?', or '\'.  
		/// -or-
		///  The log name specified in <paramref name="sourceData" /> is not valid for user log creation. The Event log names AppEvent, SysEvent, and SecEvent are reserved for system use.  
		/// -or-
		///  The log name matches an existing event source name.  
		/// -or-
		///  The source name specified in <paramref name="sourceData" /> results in a registry key path longer than 254 characters.  
		/// -or-
		///  The first 8 characters of the log name specified in <paramref name="sourceData" /> are not unique.  
		/// -or-
		///  The source name specified in <paramref name="sourceData" /> is already registered.  
		/// -or-
		///  The source name specified in <paramref name="sourceData" /> matches an existing event log name.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceData" /> is <see langword="null" />.</exception>
		// Token: 0x06002DC6 RID: 11718 RVA: 0x000CD840 File Offset: 0x000CBA40
		public static void CreateEventSource(EventSourceCreationData sourceData)
		{
			if (sourceData == null)
			{
				throw new ArgumentNullException("sourceData");
			}
			string text = sourceData.LogName;
			string source = sourceData.Source;
			string machineName = sourceData.MachineName;
			if (!SyntaxCheck.CheckMachineName(machineName))
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[] { "machineName", machineName }));
			}
			if (text == null || text.Length == 0)
			{
				text = "Application";
			}
			if (!EventLog.ValidLogName(text, false))
			{
				throw new ArgumentException(SR.GetString("BadLogName"));
			}
			if (source == null || source.Length == 0)
			{
				throw new ArgumentException(SR.GetString("MissingParameter", new object[] { "source" }));
			}
			if (source.Length + "SYSTEM\\CurrentControlSet\\Services\\EventLog".Length > 254)
			{
				throw new ArgumentException(SR.GetString("ParameterTooLong", new object[]
				{
					"source",
					254 - "SYSTEM\\CurrentControlSet\\Services\\EventLog".Length
				}));
			}
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, machineName);
			eventLogPermission.Demand();
			Mutex mutex = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				SharedUtils.EnterMutex("netfxeventlog.1.0", ref mutex);
				if (EventLog.SourceExists(source, machineName, true))
				{
					if (".".Equals(machineName))
					{
						throw new ArgumentException(SR.GetString("LocalSourceAlreadyExists", new object[] { source }));
					}
					throw new ArgumentException(SR.GetString("SourceAlreadyExists", new object[] { source, machineName }));
				}
				else
				{
					PermissionSet permissionSet = EventLog._UnsafeGetAssertPermSet();
					permissionSet.Assert();
					RegistryKey registryKey = null;
					RegistryKey registryKey2 = null;
					RegistryKey registryKey3 = null;
					RegistryKey registryKey4 = null;
					RegistryKey registryKey5 = null;
					try
					{
						if (machineName == ".")
						{
							registryKey = Registry.LocalMachine;
						}
						else
						{
							registryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, machineName);
						}
						registryKey2 = registryKey.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\EventLog", true);
						if (registryKey2 == null)
						{
							if (!".".Equals(machineName))
							{
								throw new InvalidOperationException(SR.GetString("RegKeyMissing", new object[] { "SYSTEM\\CurrentControlSet\\Services\\EventLog", text, source, machineName }));
							}
							throw new InvalidOperationException(SR.GetString("LocalRegKeyMissing", new object[] { "SYSTEM\\CurrentControlSet\\Services\\EventLog", text, source }));
						}
						else
						{
							registryKey3 = registryKey2.OpenSubKey(text, true);
							if (registryKey3 == null && text.Length >= 8)
							{
								string text2 = text.Substring(0, 8);
								if (string.Compare(text2, "AppEvent", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(text2, "SecEvent", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(text2, "SysEvent", StringComparison.OrdinalIgnoreCase) == 0)
								{
									throw new ArgumentException(SR.GetString("InvalidCustomerLogName", new object[] { text }));
								}
								string text3 = EventLog.FindSame8FirstCharsLog(registryKey2, text);
								if (text3 != null)
								{
									throw new ArgumentException(SR.GetString("DuplicateLogName", new object[] { text, text3 }));
								}
							}
							bool flag = registryKey3 == null;
							if (flag)
							{
								if (EventLog.SourceExists(text, machineName, true))
								{
									if (".".Equals(machineName))
									{
										throw new ArgumentException(SR.GetString("LocalLogAlreadyExistsAsSource", new object[] { text }));
									}
									throw new ArgumentException(SR.GetString("LogAlreadyExistsAsSource", new object[] { text, machineName }));
								}
								else
								{
									registryKey3 = registryKey2.CreateSubKey(text);
									if (!EventLog.SkipRegPatch)
									{
										registryKey3.SetValue("Sources", new string[] { text, source }, RegistryValueKind.MultiString);
									}
									EventLog.SetSpecialLogRegValues(registryKey3, text);
									registryKey4 = registryKey3.CreateSubKey(text);
									EventLog.SetSpecialSourceRegValues(registryKey4, sourceData);
								}
							}
							if (text != source)
							{
								if (!flag)
								{
									EventLog.SetSpecialLogRegValues(registryKey3, text);
									if (!EventLog.SkipRegPatch)
									{
										string[] array = registryKey3.GetValue("Sources") as string[];
										if (array == null)
										{
											registryKey3.SetValue("Sources", new string[] { text, source }, RegistryValueKind.MultiString);
										}
										else if (Array.IndexOf<string>(array, source) == -1)
										{
											string[] array2 = new string[array.Length + 1];
											Array.Copy(array, array2, array.Length);
											array2[array.Length] = source;
											registryKey3.SetValue("Sources", array2, RegistryValueKind.MultiString);
										}
									}
								}
								registryKey5 = registryKey3.CreateSubKey(source);
								EventLog.SetSpecialSourceRegValues(registryKey5, sourceData);
							}
						}
					}
					finally
					{
						if (registryKey != null)
						{
							registryKey.Close();
						}
						if (registryKey2 != null)
						{
							registryKey2.Close();
						}
						if (registryKey3 != null)
						{
							registryKey3.Flush();
							registryKey3.Close();
						}
						if (registryKey4 != null)
						{
							registryKey4.Flush();
							registryKey4.Close();
						}
						if (registryKey5 != null)
						{
							registryKey5.Flush();
							registryKey5.Close();
						}
						CodeAccessPermission.RevertAssert();
					}
				}
			}
			finally
			{
				if (mutex != null)
				{
					mutex.ReleaseMutex();
					mutex.Close();
				}
			}
		}

		/// <summary>Removes an event log from the local computer.</summary>
		/// <param name="logName">The name of the log to delete. Possible values include: Application, Security, System, and any custom event logs on the computer.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="logName" /> is an empty string ("") or <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened on the local computer.  
		/// -or-
		///  The log does not exist on the local computer.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The event log was not cleared successfully.  
		///  -or-  
		///  The log cannot be opened. A Windows error code is not available.</exception>
		// Token: 0x06002DC7 RID: 11719 RVA: 0x000CDCD8 File Offset: 0x000CBED8
		public static void Delete(string logName)
		{
			EventLog.Delete(logName, ".");
		}

		/// <summary>Removes an event log from the specified computer.</summary>
		/// <param name="logName">The name of the log to delete. Possible values include: Application, Security, System, and any custom event logs on the specified computer.</param>
		/// <param name="machineName">The name of the computer to delete the log from, or "." for the local computer.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="logName" /> is an empty string ("") or <see langword="null" />.  
		/// -or-
		///  <paramref name="machineName" /> is not a valid computer name.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened on the specified computer.  
		/// -or-
		///  The log does not exist on the specified computer.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The event log was not cleared successfully.  
		///  -or-  
		///  The log cannot be opened. A Windows error code is not available.</exception>
		// Token: 0x06002DC8 RID: 11720 RVA: 0x000CDCE8 File Offset: 0x000CBEE8
		public static void Delete(string logName, string machineName)
		{
			if (!SyntaxCheck.CheckMachineName(machineName))
			{
				throw new ArgumentException(SR.GetString("InvalidParameterFormat", new object[] { "machineName" }));
			}
			if (logName == null || logName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("NoLogName"));
			}
			if (!EventLog.ValidLogName(logName, false))
			{
				throw new InvalidOperationException(SR.GetString("BadLogName"));
			}
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, machineName);
			eventLogPermission.Demand();
			SharedUtils.CheckEnvironment();
			PermissionSet permissionSet = EventLog._UnsafeGetAssertPermSet();
			permissionSet.Assert();
			RegistryKey registryKey = null;
			Mutex mutex = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				SharedUtils.EnterMutex("netfxeventlog.1.0", ref mutex);
				try
				{
					registryKey = EventLog.GetEventLogRegKey(machineName, true);
					if (registryKey == null)
					{
						throw new InvalidOperationException(SR.GetString("RegKeyNoAccess", new object[] { "HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\EventLog", machineName }));
					}
					using (RegistryKey registryKey2 = registryKey.OpenSubKey(logName))
					{
						if (registryKey2 == null)
						{
							throw new InvalidOperationException(SR.GetString("MissingLog", new object[] { logName, machineName }));
						}
						EventLog eventLog = new EventLog(logName, machineName);
						try
						{
							eventLog.Clear();
						}
						finally
						{
							eventLog.Close();
						}
						string text = null;
						try
						{
							text = (string)registryKey2.GetValue("File");
						}
						catch
						{
						}
						if (text != null)
						{
							try
							{
								File.Delete(text);
							}
							catch
							{
							}
						}
					}
					registryKey.DeleteSubKeyTree(logName);
				}
				finally
				{
					if (registryKey != null)
					{
						registryKey.Close();
					}
					CodeAccessPermission.RevertAssert();
				}
			}
			finally
			{
				if (mutex != null)
				{
					mutex.ReleaseMutex();
				}
			}
		}

		/// <summary>Removes the event source registration from the event log of the local computer.</summary>
		/// <param name="source">The name by which the application is registered in the event log system.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> parameter does not exist in the registry of the local computer.  
		/// -or-
		///  You do not have write access on the registry key for the event log.</exception>
		// Token: 0x06002DC9 RID: 11721 RVA: 0x000CDEA0 File Offset: 0x000CC0A0
		public static void DeleteEventSource(string source)
		{
			EventLog.DeleteEventSource(source, ".");
		}

		/// <summary>Removes the application's event source registration from the specified computer.</summary>
		/// <param name="source">The name by which the application is registered in the event log system.</param>
		/// <param name="machineName">The name of the computer to remove the registration from, or "." for the local computer.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> parameter is invalid.  
		/// -or-
		///  The <paramref name="source" /> parameter does not exist in the registry of the specified computer.  
		/// -or-
		///  You do not have write access on the registry key for the event log.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> cannot be deleted because in the registry, the parent registry key for <paramref name="source" /> does not contain a subkey with the same name.</exception>
		// Token: 0x06002DCA RID: 11722 RVA: 0x000CDEB0 File Offset: 0x000CC0B0
		public static void DeleteEventSource(string source, string machineName)
		{
			if (!SyntaxCheck.CheckMachineName(machineName))
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[] { "machineName", machineName }));
			}
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, machineName);
			eventLogPermission.Demand();
			SharedUtils.CheckEnvironment();
			PermissionSet permissionSet = EventLog._UnsafeGetAssertPermSet();
			permissionSet.Assert();
			Mutex mutex = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				SharedUtils.EnterMutex("netfxeventlog.1.0", ref mutex);
				RegistryKey registryKey = null;
				RegistryKey registryKey2;
				registryKey = (registryKey2 = EventLog.FindSourceRegistration(source, machineName, true));
				try
				{
					if (registryKey == null)
					{
						if (machineName == null)
						{
							throw new ArgumentException(SR.GetString("LocalSourceNotRegistered", new object[] { source }));
						}
						throw new ArgumentException(SR.GetString("SourceNotRegistered", new object[] { source, machineName, "HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\EventLog" }));
					}
					else
					{
						string name = registryKey.Name;
						int num = name.LastIndexOf('\\');
						if (string.Compare(name, num + 1, source, 0, name.Length - num, StringComparison.Ordinal) == 0)
						{
							throw new InvalidOperationException(SR.GetString("CannotDeleteEqualSource", new object[] { source }));
						}
					}
				}
				finally
				{
					if (registryKey2 != null)
					{
						((IDisposable)registryKey2).Dispose();
					}
				}
				try
				{
					registryKey = EventLog.FindSourceRegistration(source, machineName, false);
					registryKey.DeleteSubKeyTree(source);
					if (!EventLog.SkipRegPatch)
					{
						string[] array = (string[])registryKey.GetValue("Sources");
						ArrayList arrayList = new ArrayList(array.Length - 1);
						for (int i = 0; i < array.Length; i++)
						{
							if (array[i] != source)
							{
								arrayList.Add(array[i]);
							}
						}
						string[] array2 = new string[arrayList.Count];
						arrayList.CopyTo(array2);
						registryKey.SetValue("Sources", array2, RegistryValueKind.MultiString);
					}
				}
				finally
				{
					if (registryKey != null)
					{
						registryKey.Flush();
						registryKey.Close();
					}
					CodeAccessPermission.RevertAssert();
				}
			}
			finally
			{
				if (mutex != null)
				{
					mutex.ReleaseMutex();
				}
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Diagnostics.EventLog" />, and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002DCB RID: 11723 RVA: 0x000CE0BC File Offset: 0x000CC2BC
		protected override void Dispose(bool disposing)
		{
			if (this.m_underlyingEventLog != null)
			{
				this.m_underlyingEventLog.Dispose(disposing);
			}
			base.Dispose(disposing);
		}

		/// <summary>Ends the initialization of an <see cref="T:System.Diagnostics.EventLog" /> used on a form or by another component. The initialization occurs at runtime.</summary>
		// Token: 0x06002DCC RID: 11724 RVA: 0x000CE0D9 File Offset: 0x000CC2D9
		public void EndInit()
		{
			this.m_underlyingEventLog.EndInit();
		}

		/// <summary>Determines whether the log exists on the local computer.</summary>
		/// <param name="logName">The name of the log to search for. Possible values include: Application, Security, System, other application-specific logs (such as those associated with Active Directory), or any custom log on the computer.</param>
		/// <returns>
		///   <see langword="true" /> if the log exists on the local computer; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The logName is <see langword="null" /> or the value is empty.</exception>
		// Token: 0x06002DCD RID: 11725 RVA: 0x000CE0E6 File Offset: 0x000CC2E6
		public static bool Exists(string logName)
		{
			return EventLog.Exists(logName, ".");
		}

		/// <summary>Determines whether the log exists on the specified computer.</summary>
		/// <param name="logName">The log for which to search. Possible values include: Application, Security, System, other application-specific logs (such as those associated with Active Directory), or any custom log on the computer.</param>
		/// <param name="machineName">The name of the computer on which to search for the log, or "." for the local computer.</param>
		/// <returns>
		///   <see langword="true" /> if the log exists on the specified computer; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> parameter is an invalid format. Make sure you have used proper syntax for the computer on which you are searching.  
		///  -or-  
		///  The <paramref name="logName" /> is <see langword="null" /> or the value is empty.</exception>
		// Token: 0x06002DCE RID: 11726 RVA: 0x000CE0F4 File Offset: 0x000CC2F4
		public static bool Exists(string logName, string machineName)
		{
			if (!SyntaxCheck.CheckMachineName(machineName))
			{
				throw new ArgumentException(SR.GetString("InvalidParameterFormat", new object[] { "machineName" }));
			}
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, machineName);
			eventLogPermission.Demand();
			if (logName == null || logName.Length == 0)
			{
				return false;
			}
			SharedUtils.CheckEnvironment();
			PermissionSet permissionSet = EventLog._UnsafeGetAssertPermSet();
			permissionSet.Assert();
			RegistryKey registryKey = null;
			RegistryKey registryKey2 = null;
			bool flag;
			try
			{
				registryKey = EventLog.GetEventLogRegKey(machineName, false);
				if (registryKey == null)
				{
					flag = false;
				}
				else
				{
					registryKey2 = registryKey.OpenSubKey(logName, false);
					flag = registryKey2 != null;
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
				if (registryKey2 != null)
				{
					registryKey2.Close();
				}
				CodeAccessPermission.RevertAssert();
			}
			return flag;
		}

		// Token: 0x06002DCF RID: 11727 RVA: 0x000CE1A4 File Offset: 0x000CC3A4
		private static string FindSame8FirstCharsLog(RegistryKey keyParent, string logName)
		{
			string text = logName.Substring(0, 8);
			foreach (string text2 in keyParent.GetSubKeyNames())
			{
				if (text2.Length >= 8 && string.Compare(text2.Substring(0, 8), text, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return text2;
				}
			}
			return null;
		}

		// Token: 0x06002DD0 RID: 11728 RVA: 0x000CE1F0 File Offset: 0x000CC3F0
		private static RegistryKey FindSourceRegistration(string source, string machineName, bool readOnly)
		{
			return EventLog.FindSourceRegistration(source, machineName, readOnly, false);
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x000CE1FC File Offset: 0x000CC3FC
		private static RegistryKey FindSourceRegistration(string source, string machineName, bool readOnly, bool wantToCreate)
		{
			if (source != null && source.Length != 0)
			{
				SharedUtils.CheckEnvironment();
				PermissionSet permissionSet = EventLog._UnsafeGetAssertPermSet();
				permissionSet.Assert();
				RegistryKey registryKey = null;
				try
				{
					registryKey = EventLog.GetEventLogRegKey(machineName, !readOnly);
					if (registryKey == null)
					{
						return null;
					}
					StringBuilder stringBuilder = null;
					string[] subKeyNames = registryKey.GetSubKeyNames();
					for (int i = 0; i < subKeyNames.Length; i++)
					{
						RegistryKey registryKey2 = null;
						try
						{
							RegistryKey registryKey3 = registryKey.OpenSubKey(subKeyNames[i], !readOnly);
							if (registryKey3 != null)
							{
								registryKey2 = registryKey3.OpenSubKey(source, !readOnly);
								if (registryKey2 != null)
								{
									return registryKey3;
								}
								registryKey3.Close();
							}
						}
						catch (UnauthorizedAccessException)
						{
							if (stringBuilder == null)
							{
								stringBuilder = new StringBuilder(subKeyNames[i]);
							}
							else
							{
								stringBuilder.Append(", ");
								stringBuilder.Append(subKeyNames[i]);
							}
						}
						catch (SecurityException)
						{
							if (stringBuilder == null)
							{
								stringBuilder = new StringBuilder(subKeyNames[i]);
							}
							else
							{
								stringBuilder.Append(", ");
								stringBuilder.Append(subKeyNames[i]);
							}
						}
						finally
						{
							if (registryKey2 != null)
							{
								registryKey2.Close();
							}
						}
					}
					if (stringBuilder != null)
					{
						throw new SecurityException(SR.GetString(wantToCreate ? "SomeLogsInaccessibleToCreate" : "SomeLogsInaccessible", new object[] { stringBuilder.ToString() }));
					}
				}
				finally
				{
					if (registryKey != null)
					{
						registryKey.Close();
					}
					CodeAccessPermission.RevertAssert();
				}
			}
			return null;
		}

		/// <summary>Searches for all event logs on the local computer and creates an array of <see cref="T:System.Diagnostics.EventLog" /> objects that contain the list.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.EventLog" /> that represents the logs on the local computer.</returns>
		/// <exception cref="T:System.SystemException">You do not have read access to the registry.  
		///  -or-  
		///  There is no event log service on the computer.</exception>
		// Token: 0x06002DD2 RID: 11730 RVA: 0x000CE374 File Offset: 0x000CC574
		public static EventLog[] GetEventLogs()
		{
			return EventLog.GetEventLogs(".");
		}

		/// <summary>Searches for all event logs on the given computer and creates an array of <see cref="T:System.Diagnostics.EventLog" /> objects that contain the list.</summary>
		/// <param name="machineName">The computer on which to search for event logs.</param>
		/// <returns>An array of type <see cref="T:System.Diagnostics.EventLog" /> that represents the logs on the given computer.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> parameter is an invalid computer name.</exception>
		/// <exception cref="T:System.InvalidOperationException">You do not have read access to the registry.  
		///  -or-  
		///  There is no event log service on the computer.</exception>
		// Token: 0x06002DD3 RID: 11731 RVA: 0x000CE380 File Offset: 0x000CC580
		public static EventLog[] GetEventLogs(string machineName)
		{
			if (!SyntaxCheck.CheckMachineName(machineName))
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[] { "machineName", machineName }));
			}
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, machineName);
			eventLogPermission.Demand();
			SharedUtils.CheckEnvironment();
			string[] array = new string[0];
			PermissionSet permissionSet = EventLog._UnsafeGetAssertPermSet();
			permissionSet.Assert();
			RegistryKey registryKey = null;
			try
			{
				registryKey = EventLog.GetEventLogRegKey(machineName, false);
				if (registryKey == null)
				{
					throw new InvalidOperationException(SR.GetString("RegKeyMissingShort", new object[] { "SYSTEM\\CurrentControlSet\\Services\\EventLog", machineName }));
				}
				array = registryKey.GetSubKeyNames();
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
				CodeAccessPermission.RevertAssert();
			}
			if (EventLog.s_dontFilterRegKeys || machineName != ".")
			{
				EventLog[] array2 = new EventLog[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					EventLog eventLog = new EventLog(array[i], machineName);
					array2[i] = eventLog;
				}
				return array2;
			}
			List<EventLog> list = new List<EventLog>(array.Length);
			for (int j = 0; j < array.Length; j++)
			{
				EventLog eventLog2 = new EventLog(array[j], machineName);
				SafeEventLogReadHandle safeEventLogReadHandle = SafeEventLogReadHandle.OpenEventLog(machineName, array[j]);
				if (!safeEventLogReadHandle.IsInvalid)
				{
					safeEventLogReadHandle.Close();
					list.Add(eventLog2);
				}
				else if (Marshal.GetLastWin32Error() != 87)
				{
					list.Add(eventLog2);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06002DD4 RID: 11732 RVA: 0x000CE4E4 File Offset: 0x000CC6E4
		private static bool IsWindowsRS5OrUp()
		{
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
			Microsoft.Win32.NativeMethods.RTL_OSVERSIONINFOEX rtl_OSVERSIONINFOEX = default(Microsoft.Win32.NativeMethods.RTL_OSVERSIONINFOEX);
			rtl_OSVERSIONINFOEX.dwOSVersionInfoSize = (uint)Marshal.SizeOf(rtl_OSVERSIONINFOEX);
			return Microsoft.Win32.NativeMethods.RtlGetVersion(out rtl_OSVERSIONINFOEX) == 0 && rtl_OSVERSIONINFOEX.dwPlatformId == 2U && (rtl_OSVERSIONINFOEX.dwMajorVersion > 10U || (rtl_OSVERSIONINFOEX.dwMajorVersion == 10U && (rtl_OSVERSIONINFOEX.dwMinorVersion > 0U || (rtl_OSVERSIONINFOEX.dwMinorVersion == 0U && rtl_OSVERSIONINFOEX.dwBuildNumber >= 17763U))));
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x000CE56C File Offset: 0x000CC76C
		internal static RegistryKey GetEventLogRegKey(string machine, bool writable)
		{
			RegistryKey registryKey = null;
			try
			{
				if (machine.Equals("."))
				{
					registryKey = Registry.LocalMachine;
				}
				else
				{
					registryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, machine);
				}
				if (registryKey != null)
				{
					return registryKey.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\EventLog", writable);
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
			return null;
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x000CE5D0 File Offset: 0x000CC7D0
		internal static string GetDllPath(string machineName)
		{
			return Path.Combine(SharedUtils.GetLatestBuildDllDirectory(machineName), "EventLogMessages.dll");
		}

		/// <summary>Determines whether an event source is registered on the local computer.</summary>
		/// <param name="source">The name of the event source.</param>
		/// <returns>
		///   <see langword="true" /> if the event source is registered on the local computer; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">
		///   <paramref name="source" /> was not found, but some or all of the event logs could not be searched.</exception>
		// Token: 0x06002DD7 RID: 11735 RVA: 0x000CE5E2 File Offset: 0x000CC7E2
		public static bool SourceExists(string source)
		{
			return EventLog.SourceExists(source, ".");
		}

		/// <summary>Determines whether an event source is registered on a specified computer.</summary>
		/// <param name="source">The name of the event source.</param>
		/// <param name="machineName">The name the computer on which to look, or "." for the local computer.</param>
		/// <returns>
		///   <see langword="true" /> if the event source is registered on the given computer; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="machineName" /> is an invalid computer name.</exception>
		/// <exception cref="T:System.Security.SecurityException">
		///   <paramref name="source" /> was not found, but some or all of the event logs could not be searched.</exception>
		// Token: 0x06002DD8 RID: 11736 RVA: 0x000CE5EF File Offset: 0x000CC7EF
		public static bool SourceExists(string source, string machineName)
		{
			return EventLog.SourceExists(source, machineName, false);
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x000CE5FC File Offset: 0x000CC7FC
		internal static bool SourceExists(string source, string machineName, bool wantToCreate)
		{
			if (!SyntaxCheck.CheckMachineName(machineName))
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[] { "machineName", machineName }));
			}
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Write, machineName);
			eventLogPermission.Demand();
			bool flag;
			using (RegistryKey registryKey = EventLog.FindSourceRegistration(source, machineName, true, wantToCreate))
			{
				flag = registryKey != null;
			}
			return flag;
		}

		/// <summary>Gets the name of the log to which the specified source is registered.</summary>
		/// <param name="source">The name of the event source.</param>
		/// <param name="machineName">The name of the computer on which to look, or "." for the local computer.</param>
		/// <returns>The name of the log associated with the specified source in the registry.</returns>
		// Token: 0x06002DDA RID: 11738 RVA: 0x000CE670 File Offset: 0x000CC870
		public static string LogNameFromSourceName(string source, string machineName)
		{
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, machineName);
			eventLogPermission.Demand();
			return EventLog._InternalLogNameFromSourceName(source, machineName);
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x000CE694 File Offset: 0x000CC894
		internal static string _InternalLogNameFromSourceName(string source, string machineName)
		{
			string text;
			using (RegistryKey registryKey = EventLog.FindSourceRegistration(source, machineName, true))
			{
				if (registryKey == null)
				{
					text = "";
				}
				else
				{
					string name = registryKey.Name;
					int num = name.LastIndexOf('\\');
					text = name.Substring(num + 1);
				}
			}
			return text;
		}

		/// <summary>Changes the configured behavior for writing new entries when the event log reaches its maximum file size.</summary>
		/// <param name="action">The overflow behavior for writing new entries to the event log.</param>
		/// <param name="retentionDays">The minimum number of days each event log entry is retained. This parameter is used only if <paramref name="action" /> is set to <see cref="F:System.Diagnostics.OverflowAction.OverwriteOlder" />.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="action" /> is not a valid <see cref="P:System.Diagnostics.EventLog.OverflowAction" /> value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="retentionDays" /> is less than one, or larger than 365.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.EventLog.Log" /> value is not a valid log name.  
		/// -or-
		///  The registry key for the event log could not be opened on the target computer.</exception>
		// Token: 0x06002DDC RID: 11740 RVA: 0x000CE6EC File Offset: 0x000CC8EC
		[ComVisible(false)]
		public void ModifyOverflowPolicy(OverflowAction action, int retentionDays)
		{
			this.m_underlyingEventLog.ModifyOverflowPolicy(action, retentionDays);
		}

		/// <summary>Specifies the localized name of the event log, which is displayed in the server Event Viewer.</summary>
		/// <param name="resourceFile">The fully specified path to a localized resource file.</param>
		/// <param name="resourceId">The resource identifier that indexes a localized string within the resource file.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.EventLog.Log" /> value is not a valid log name.  
		/// -or-
		///  The registry key for the event log could not be opened on the target computer.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="resourceFile" /> is <see langword="null" />.</exception>
		// Token: 0x06002DDD RID: 11741 RVA: 0x000CE6FB File Offset: 0x000CC8FB
		[ComVisible(false)]
		public void RegisterDisplayName(string resourceFile, long resourceId)
		{
			this.m_underlyingEventLog.RegisterDisplayName(resourceFile, resourceId);
		}

		// Token: 0x06002DDE RID: 11742 RVA: 0x000CE70C File Offset: 0x000CC90C
		private static void SetSpecialLogRegValues(RegistryKey logKey, string logName)
		{
			if (logKey.GetValue("MaxSize") == null)
			{
				logKey.SetValue("MaxSize", 524288, RegistryValueKind.DWord);
			}
			if (logKey.GetValue("AutoBackupLogFiles") == null)
			{
				logKey.SetValue("AutoBackupLogFiles", 0, RegistryValueKind.DWord);
			}
			if (!EventLog.SkipRegPatch)
			{
				if (logKey.GetValue("Retention") == null)
				{
					logKey.SetValue("Retention", 604800, RegistryValueKind.DWord);
				}
				if (logKey.GetValue("File") == null)
				{
					string text;
					if (logName.Length > 8)
					{
						text = "%SystemRoot%\\System32\\config\\" + logName.Substring(0, 8) + ".evt";
					}
					else
					{
						text = "%SystemRoot%\\System32\\config\\" + logName + ".evt";
					}
					logKey.SetValue("File", text, RegistryValueKind.ExpandString);
				}
			}
		}

		// Token: 0x06002DDF RID: 11743 RVA: 0x000CE7D4 File Offset: 0x000CC9D4
		private static void SetSpecialSourceRegValues(RegistryKey sourceLogKey, EventSourceCreationData sourceData)
		{
			if (string.IsNullOrEmpty(sourceData.MessageResourceFile))
			{
				sourceLogKey.SetValue("EventMessageFile", EventLog.GetDllPath(sourceData.MachineName), RegistryValueKind.ExpandString);
			}
			else
			{
				sourceLogKey.SetValue("EventMessageFile", EventLog.FixupPath(sourceData.MessageResourceFile), RegistryValueKind.ExpandString);
			}
			if (!string.IsNullOrEmpty(sourceData.ParameterResourceFile))
			{
				sourceLogKey.SetValue("ParameterMessageFile", EventLog.FixupPath(sourceData.ParameterResourceFile), RegistryValueKind.ExpandString);
			}
			if (!string.IsNullOrEmpty(sourceData.CategoryResourceFile))
			{
				sourceLogKey.SetValue("CategoryMessageFile", EventLog.FixupPath(sourceData.CategoryResourceFile), RegistryValueKind.ExpandString);
				sourceLogKey.SetValue("CategoryCount", sourceData.CategoryCount, RegistryValueKind.DWord);
			}
		}

		// Token: 0x06002DE0 RID: 11744 RVA: 0x000CE87D File Offset: 0x000CCA7D
		private static string FixupPath(string path)
		{
			if (path[0] == '%')
			{
				return path;
			}
			return Path.GetFullPath(path);
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x000CE894 File Offset: 0x000CCA94
		internal static string TryFormatMessage(Microsoft.Win32.SafeHandles.SafeLibraryHandle hModule, uint messageNum, string[] insertionStrings)
		{
			if (insertionStrings.Length == 0)
			{
				return EventLog.UnsafeTryFormatMessage(hModule, messageNum, insertionStrings);
			}
			string text = EventLog.UnsafeTryFormatMessage(hModule, messageNum, new string[0]);
			if (text == null)
			{
				return null;
			}
			int num = 0;
			for (int i = 0; i < text.Length; i++)
			{
				if (text[i] == '%' && text.Length > i + 1)
				{
					StringBuilder stringBuilder = new StringBuilder();
					while (i + 1 < text.Length && char.IsDigit(text[i + 1]))
					{
						stringBuilder.Append(text[i + 1]);
						i++;
					}
					i++;
					if (stringBuilder.Length > 0)
					{
						int num2 = -1;
						if (int.TryParse(stringBuilder.ToString(), NumberStyles.None, CultureInfo.InvariantCulture, out num2))
						{
							num = Math.Max(num, num2);
						}
					}
				}
			}
			if (num > insertionStrings.Length)
			{
				string[] array = new string[num];
				Array.Copy(insertionStrings, array, insertionStrings.Length);
				for (int j = insertionStrings.Length; j < array.Length; j++)
				{
					array[j] = "%" + (j + 1).ToString();
				}
				insertionStrings = array;
			}
			return EventLog.UnsafeTryFormatMessage(hModule, messageNum, insertionStrings);
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x000CE9A8 File Offset: 0x000CCBA8
		internal static string UnsafeTryFormatMessage(Microsoft.Win32.SafeHandles.SafeLibraryHandle hModule, uint messageNum, string[] insertionStrings)
		{
			string text = null;
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder(1024);
			int num2 = 10240;
			IntPtr[] array = new IntPtr[insertionStrings.Length];
			GCHandle[] array2 = new GCHandle[insertionStrings.Length];
			GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			if (insertionStrings.Length == 0)
			{
				num2 |= 512;
			}
			try
			{
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = GCHandle.Alloc(insertionStrings[i], GCHandleType.Pinned);
					array[i] = array2[i].AddrOfPinnedObject();
				}
				int num3 = 122;
				while (num == 0 && num3 == 122)
				{
					num = SafeNativeMethods.FormatMessage(num2, hModule, messageNum, 0, stringBuilder, stringBuilder.Capacity, array);
					if (num == 0)
					{
						num3 = Marshal.GetLastWin32Error();
						if (num3 == 122)
						{
							stringBuilder.Capacity *= 2;
						}
					}
				}
			}
			catch
			{
				num = 0;
			}
			finally
			{
				for (int j = 0; j < array2.Length; j++)
				{
					if (array2[j].IsAllocated)
					{
						array2[j].Free();
					}
				}
				gchandle.Free();
			}
			if (num > 0)
			{
				text = stringBuilder.ToString();
				if (text.Length > 1 && text[text.Length - 1] == '\n')
				{
					text = text.Substring(0, text.Length - 2);
				}
			}
			return text;
		}

		// Token: 0x06002DE3 RID: 11747 RVA: 0x000CEB04 File Offset: 0x000CCD04
		private static bool CharIsPrintable(char c)
		{
			UnicodeCategory unicodeCategory = char.GetUnicodeCategory(c);
			return unicodeCategory != UnicodeCategory.Control || unicodeCategory == UnicodeCategory.Format || unicodeCategory == UnicodeCategory.LineSeparator || unicodeCategory == UnicodeCategory.ParagraphSeparator || unicodeCategory == UnicodeCategory.OtherNotAssigned;
		}

		// Token: 0x06002DE4 RID: 11748 RVA: 0x000CEB34 File Offset: 0x000CCD34
		internal static bool ValidLogName(string logName, bool ignoreEmpty)
		{
			if (logName.Length == 0 && !ignoreEmpty)
			{
				return false;
			}
			foreach (char c in logName)
			{
				if (!EventLog.CharIsPrintable(c) || c == '\\' || c == '*' || c == '?')
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Writes an information type entry, with the given message text, to the event log.</summary>
		/// <param name="message">The string to write to the event log.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.EventLog.Source" /> property of the <see cref="T:System.Diagnostics.EventLog" /> has not been set.  
		///  -or-  
		///  The method attempted to register a new event source, but the computer name in <see cref="P:System.Diagnostics.EventLog.MachineName" /> is not valid.  
		/// -or-
		///  The source is already registered for a different event log.  
		/// -or-
		///  The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		// Token: 0x06002DE5 RID: 11749 RVA: 0x000CEB83 File Offset: 0x000CCD83
		public void WriteEntry(string message)
		{
			this.WriteEntry(message, EventLogEntryType.Information, 0, 0, null);
		}

		/// <summary>Writes an information type entry with the given message text to the event log, using the specified registered event source.</summary>
		/// <param name="source">The source by which the application is registered on the specified computer.</param>
		/// <param name="message">The string to write to the event log.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").  
		/// -or-
		///  The <paramref name="source" /> value is <see langword="null" />.  
		/// -or-
		///  The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		// Token: 0x06002DE6 RID: 11750 RVA: 0x000CEB90 File Offset: 0x000CCD90
		public static void WriteEntry(string source, string message)
		{
			EventLog.WriteEntry(source, message, EventLogEntryType.Information, 0, 0, null);
		}

		/// <summary>Writes an error, warning, information, success audit, or failure audit entry with the given message text to the event log.</summary>
		/// <param name="message">The string to write to the event log.</param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.EventLog.Source" /> property of the <see cref="T:System.Diagnostics.EventLog" /> has not been set.  
		///  -or-  
		///  The method attempted to register a new event source, but the computer name in <see cref="P:System.Diagnostics.EventLog.MachineName" /> is not valid.  
		/// -or-
		///  The source is already registered for a different event log.  
		/// -or-
		///  The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		// Token: 0x06002DE7 RID: 11751 RVA: 0x000CEB9D File Offset: 0x000CCD9D
		public void WriteEntry(string message, EventLogEntryType type)
		{
			this.WriteEntry(message, type, 0, 0, null);
		}

		/// <summary>Writes an error, warning, information, success audit, or failure audit entry with the given message text to the event log, using the specified registered event source.</summary>
		/// <param name="source">The source by which the application is registered on the specified computer.</param>
		/// <param name="message">The string to write to the event log.</param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").  
		/// -or-
		///  The <paramref name="source" /> value is <see langword="null" />.  
		/// -or-
		///  The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		// Token: 0x06002DE8 RID: 11752 RVA: 0x000CEBAA File Offset: 0x000CCDAA
		public static void WriteEntry(string source, string message, EventLogEntryType type)
		{
			EventLog.WriteEntry(source, message, type, 0, 0, null);
		}

		/// <summary>Writes an entry with the given message text and application-defined event identifier to the event log.</summary>
		/// <param name="message">The string to write to the event log.</param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values.</param>
		/// <param name="eventID">The application-specific identifier for the event.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.EventLog.Source" /> property of the <see cref="T:System.Diagnostics.EventLog" /> has not been set.  
		///  -or-  
		///  The method attempted to register a new event source, but the computer name in <see cref="P:System.Diagnostics.EventLog.MachineName" /> is not valid.  
		/// -or-
		///  The source is already registered for a different event log.  
		/// -or-
		///  <paramref name="eventID" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.  
		/// -or-
		///  The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		// Token: 0x06002DE9 RID: 11753 RVA: 0x000CEBB7 File Offset: 0x000CCDB7
		public void WriteEntry(string message, EventLogEntryType type, int eventID)
		{
			this.WriteEntry(message, type, eventID, 0, null);
		}

		/// <summary>Writes an entry with the given message text and application-defined event identifier to the event log, using the specified registered event source.</summary>
		/// <param name="source">The source by which the application is registered on the specified computer.</param>
		/// <param name="message">The string to write to the event log.</param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values.</param>
		/// <param name="eventID">The application-specific identifier for the event.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").  
		/// -or-
		///  The <paramref name="source" /> value is <see langword="null" />.  
		/// -or-
		///  <paramref name="eventID" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.  
		/// -or-
		///  The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		// Token: 0x06002DEA RID: 11754 RVA: 0x000CEBC4 File Offset: 0x000CCDC4
		public static void WriteEntry(string source, string message, EventLogEntryType type, int eventID)
		{
			EventLog.WriteEntry(source, message, type, eventID, 0, null);
		}

		/// <summary>Writes an entry with the given message text, application-defined event identifier, and application-defined category to the event log.</summary>
		/// <param name="message">The string to write to the event log.</param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values.</param>
		/// <param name="eventID">The application-specific identifier for the event.</param>
		/// <param name="category">The application-specific subcategory associated with the message.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.EventLog.Source" /> property of the <see cref="T:System.Diagnostics.EventLog" /> has not been set.  
		///  -or-  
		///  The method attempted to register a new event source, but the computer name in <see cref="P:System.Diagnostics.EventLog.MachineName" /> is not valid.  
		/// -or-
		///  The source is already registered for a different event log.  
		/// -or-
		///  <paramref name="eventID" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.  
		/// -or-
		///  The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		// Token: 0x06002DEB RID: 11755 RVA: 0x000CEBD1 File Offset: 0x000CCDD1
		public void WriteEntry(string message, EventLogEntryType type, int eventID, short category)
		{
			this.WriteEntry(message, type, eventID, category, null);
		}

		/// <summary>Writes an entry with the given message text, application-defined event identifier, and application-defined category to the event log, using the specified registered event source. The <paramref name="category" /> can be used by the Event Viewer to filter events in the log.</summary>
		/// <param name="source">The source by which the application is registered on the specified computer.</param>
		/// <param name="message">The string to write to the event log.</param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values.</param>
		/// <param name="eventID">The application-specific identifier for the event.</param>
		/// <param name="category">The application-specific subcategory associated with the message.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").  
		/// -or-
		///  The <paramref name="source" /> value is <see langword="null" />.  
		/// -or-
		///  <paramref name="eventID" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.  
		/// -or-
		///  The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		// Token: 0x06002DEC RID: 11756 RVA: 0x000CEBDF File Offset: 0x000CCDDF
		public static void WriteEntry(string source, string message, EventLogEntryType type, int eventID, short category)
		{
			EventLog.WriteEntry(source, message, type, eventID, category, null);
		}

		/// <summary>Writes an entry with the given message text, application-defined event identifier, and application-defined category to the event log (using the specified registered event source) and appends binary data to the message.</summary>
		/// <param name="source">The source by which the application is registered on the specified computer.</param>
		/// <param name="message">The string to write to the event log.</param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values.</param>
		/// <param name="eventID">The application-specific identifier for the event.</param>
		/// <param name="category">The application-specific subcategory associated with the message.</param>
		/// <param name="rawData">An array of bytes that holds the binary data associated with the entry.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").  
		/// -or-
		///  The <paramref name="source" /> value is <see langword="null" />.  
		/// -or-
		///  <paramref name="eventID" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.  
		/// -or-
		///  The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		// Token: 0x06002DED RID: 11757 RVA: 0x000CEBF0 File Offset: 0x000CCDF0
		public static void WriteEntry(string source, string message, EventLogEntryType type, int eventID, short category, byte[] rawData)
		{
			using (EventLogInternal eventLogInternal = new EventLogInternal("", ".", EventLog.CheckAndNormalizeSourceName(source)))
			{
				eventLogInternal.WriteEntry(message, type, eventID, category, rawData);
			}
		}

		/// <summary>Writes an entry with the given message text, application-defined event identifier, and application-defined category to the event log, and appends binary data to the message.</summary>
		/// <param name="message">The string to write to the event log.</param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values.</param>
		/// <param name="eventID">The application-specific identifier for the event.</param>
		/// <param name="category">The application-specific subcategory associated with the message.</param>
		/// <param name="rawData">An array of bytes that holds the binary data associated with the entry.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.EventLog.Source" /> property of the <see cref="T:System.Diagnostics.EventLog" /> has not been set.  
		///  -or-  
		///  The method attempted to register a new event source, but the computer name in <see cref="P:System.Diagnostics.EventLog.MachineName" /> is not valid.  
		/// -or-
		///  The source is already registered for a different event log.  
		/// -or-
		///  <paramref name="eventID" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.  
		/// -or-
		///  The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		// Token: 0x06002DEE RID: 11758 RVA: 0x000CEC3C File Offset: 0x000CCE3C
		public void WriteEntry(string message, EventLogEntryType type, int eventID, short category, byte[] rawData)
		{
			this.m_underlyingEventLog.WriteEntry(message, type, eventID, category, rawData);
		}

		/// <summary>Writes a localized entry to the event log.</summary>
		/// <param name="instance">An <see cref="T:System.Diagnostics.EventInstance" /> instance that represents a localized event log entry.</param>
		/// <param name="values">An array of strings to merge into the message text of the event log entry.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.EventLog.Source" /> property of the <see cref="T:System.Diagnostics.EventLog" /> has not been set.  
		///  -or-  
		///  The method attempted to register a new event source, but the computer name in <see cref="P:System.Diagnostics.EventLog.MachineName" /> is not valid.  
		/// -or-
		///  The source is already registered for a different event log.  
		/// -or-
		///  <paramref name="instance.InstanceId" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.  
		/// -or-
		///  <paramref name="values" /> has more than 256 elements.  
		/// -or-
		///  One of the <paramref name="values" /> elements is longer than 32766 bytes.  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		// Token: 0x06002DEF RID: 11759 RVA: 0x000CEC50 File Offset: 0x000CCE50
		[ComVisible(false)]
		public void WriteEvent(EventInstance instance, params object[] values)
		{
			this.WriteEvent(instance, null, values);
		}

		/// <summary>Writes an event log entry with the given event data, message replacement strings, and associated binary data.</summary>
		/// <param name="instance">An <see cref="T:System.Diagnostics.EventInstance" /> instance that represents a localized event log entry.</param>
		/// <param name="data">An array of bytes that holds the binary data associated with the entry.</param>
		/// <param name="values">An array of strings to merge into the message text of the event log entry.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.EventLog.Source" /> property of the <see cref="T:System.Diagnostics.EventLog" /> has not been set.  
		///  -or-  
		///  The method attempted to register a new event source, but the computer name in <see cref="P:System.Diagnostics.EventLog.MachineName" /> is not valid.  
		/// -or-
		///  The source is already registered for a different event log.  
		/// -or-
		///  <paramref name="instance.InstanceId" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.  
		/// -or-
		///  <paramref name="values" /> has more than 256 elements.  
		/// -or-
		///  One of the <paramref name="values" /> elements is longer than 32766 bytes.  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		// Token: 0x06002DF0 RID: 11760 RVA: 0x000CEC5B File Offset: 0x000CCE5B
		[ComVisible(false)]
		public void WriteEvent(EventInstance instance, byte[] data, params object[] values)
		{
			this.m_underlyingEventLog.WriteEvent(instance, data, values);
		}

		/// <summary>Writes an event log entry with the given event data and message replacement strings, using the specified registered event source.</summary>
		/// <param name="source">The name of the event source registered for the application on the specified computer.</param>
		/// <param name="instance">An <see cref="T:System.Diagnostics.EventInstance" /> instance that represents a localized event log entry.</param>
		/// <param name="values">An array of strings to merge into the message text of the event log entry.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").  
		/// -or-
		///  The <paramref name="source" /> value is <see langword="null" />.  
		/// -or-
		///  <paramref name="instance.InstanceId" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.  
		/// -or-
		///  <paramref name="values" /> has more than 256 elements.  
		/// -or-
		///  One of the <paramref name="values" /> elements is longer than 32766 bytes.  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		// Token: 0x06002DF1 RID: 11761 RVA: 0x000CEC6C File Offset: 0x000CCE6C
		public static void WriteEvent(string source, EventInstance instance, params object[] values)
		{
			using (EventLogInternal eventLogInternal = new EventLogInternal("", ".", EventLog.CheckAndNormalizeSourceName(source)))
			{
				eventLogInternal.WriteEvent(instance, null, values);
			}
		}

		/// <summary>Writes an event log entry with the given event data, message replacement strings, and associated binary data, and using the specified registered event source.</summary>
		/// <param name="source">The name of the event source registered for the application on the specified computer.</param>
		/// <param name="instance">An <see cref="T:System.Diagnostics.EventInstance" /> instance that represents a localized event log entry.</param>
		/// <param name="data">An array of bytes that holds the binary data associated with the entry.</param>
		/// <param name="values">An array of strings to merge into the message text of the event log entry.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").  
		/// -or-
		///  The <paramref name="source" /> value is <see langword="null" />.  
		/// -or-
		///  <paramref name="instance.InstanceId" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.  
		/// -or-
		///  <paramref name="values" /> has more than 256 elements.  
		/// -or-
		///  One of the <paramref name="values" /> elements is longer than 32766 bytes.  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		// Token: 0x06002DF2 RID: 11762 RVA: 0x000CECB4 File Offset: 0x000CCEB4
		public static void WriteEvent(string source, EventInstance instance, byte[] data, params object[] values)
		{
			using (EventLogInternal eventLogInternal = new EventLogInternal("", ".", EventLog.CheckAndNormalizeSourceName(source)))
			{
				eventLogInternal.WriteEvent(instance, data, values);
			}
		}

		// Token: 0x06002DF3 RID: 11763 RVA: 0x000CECFC File Offset: 0x000CCEFC
		private static string CheckAndNormalizeSourceName(string source)
		{
			if (source == null)
			{
				source = string.Empty;
			}
			if (source.Length + "SYSTEM\\CurrentControlSet\\Services\\EventLog".Length > 254)
			{
				throw new ArgumentException(SR.GetString("ParameterTooLong", new object[]
				{
					"source",
					254 - "SYSTEM\\CurrentControlSet\\Services\\EventLog".Length
				}));
			}
			return source;
		}

		// Token: 0x04002720 RID: 10016
		private const string EventLogKey = "SYSTEM\\CurrentControlSet\\Services\\EventLog";

		// Token: 0x04002721 RID: 10017
		internal const string DllName = "EventLogMessages.dll";

		// Token: 0x04002722 RID: 10018
		private const string eventLogMutexName = "netfxeventlog.1.0";

		// Token: 0x04002723 RID: 10019
		private const int DefaultMaxSize = 524288;

		// Token: 0x04002724 RID: 10020
		private const int DefaultRetention = 604800;

		// Token: 0x04002725 RID: 10021
		private const int SecondsPerDay = 86400;

		// Token: 0x04002726 RID: 10022
		private EventLogInternal m_underlyingEventLog;

		// Token: 0x04002727 RID: 10023
		private static volatile bool s_CheckedOsVersion;

		// Token: 0x04002728 RID: 10024
		private static volatile bool s_SkipRegPatch;

		// Token: 0x04002729 RID: 10025
		private static readonly bool s_dontFilterRegKeys = !EventLog.IsWindowsRS5OrUp() || LocalAppContextSwitches.DisableEventLogRegistryKeysFiltering;
	}
}
