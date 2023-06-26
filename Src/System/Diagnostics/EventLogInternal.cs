using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Diagnostics
{
	// Token: 0x020004CB RID: 1227
	internal class EventLogInternal : IDisposable, ISupportInitialize
	{
		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06002DF5 RID: 11765 RVA: 0x000CED78 File Offset: 0x000CCF78
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

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06002DF6 RID: 11766 RVA: 0x000CEDA8 File Offset: 0x000CCFA8
		private static object InternalSyncObject
		{
			get
			{
				if (EventLogInternal.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref EventLogInternal.s_InternalSyncObject, obj, null);
				}
				return EventLogInternal.s_InternalSyncObject;
			}
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x000CEDD4 File Offset: 0x000CCFD4
		public EventLogInternal()
			: this("", ".", "", null)
		{
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x000CEDEC File Offset: 0x000CCFEC
		public EventLogInternal(string logName)
			: this(logName, ".", "", null)
		{
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x000CEE00 File Offset: 0x000CD000
		public EventLogInternal(string logName, string machineName)
			: this(logName, machineName, "", null)
		{
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x000CEE10 File Offset: 0x000CD010
		public EventLogInternal(string logName, string machineName, string source)
			: this(logName, machineName, source, null)
		{
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x000CEE1C File Offset: 0x000CD01C
		public EventLogInternal(string logName, string machineName, string source, EventLog parent)
		{
			if (logName == null)
			{
				throw new ArgumentNullException("logName");
			}
			if (!EventLogInternal.ValidLogName(logName, true))
			{
				throw new ArgumentException(SR.GetString("BadLogName"));
			}
			if (!SyntaxCheck.CheckMachineName(machineName))
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[] { "machineName", machineName }));
			}
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Write, machineName);
			eventLogPermission.Demand();
			this.machineName = machineName;
			this.logName = logName;
			this.sourceName = source;
			this.readHandle = null;
			this.writeHandle = null;
			this.boolFlags[2] = true;
			this.parent = parent;
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06002DFC RID: 11772 RVA: 0x000CEED0 File Offset: 0x000CD0D0
		public EventLogEntryCollection Entries
		{
			get
			{
				string text = this.machineName;
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, text);
				eventLogPermission.Demand();
				if (this.entriesCollection == null)
				{
					this.entriesCollection = new EventLogEntryCollection(this);
				}
				return this.entriesCollection;
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06002DFD RID: 11773 RVA: 0x000CEF10 File Offset: 0x000CD110
		internal int EntryCount
		{
			get
			{
				if (!this.IsOpenForRead)
				{
					this.OpenForRead(this.machineName);
				}
				int num;
				if (!Microsoft.Win32.UnsafeNativeMethods.GetNumberOfEventLogRecords(this.readHandle, out num))
				{
					throw SharedUtils.CreateSafeWin32Exception();
				}
				return num;
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06002DFE RID: 11774 RVA: 0x000CEF49 File Offset: 0x000CD149
		private bool IsOpen
		{
			get
			{
				return this.readHandle != null || this.writeHandle != null;
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06002DFF RID: 11775 RVA: 0x000CEF5E File Offset: 0x000CD15E
		private bool IsOpenForRead
		{
			get
			{
				return this.readHandle != null;
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06002E00 RID: 11776 RVA: 0x000CEF69 File Offset: 0x000CD169
		private bool IsOpenForWrite
		{
			get
			{
				return this.writeHandle != null;
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06002E01 RID: 11777 RVA: 0x000CEF74 File Offset: 0x000CD174
		public string LogDisplayName
		{
			get
			{
				if (this.logDisplayName != null)
				{
					return this.logDisplayName;
				}
				string text = this.machineName;
				if (this.GetLogName(text) != null)
				{
					EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, text);
					eventLogPermission.Demand();
					SharedUtils.CheckEnvironment();
					PermissionSet permissionSet = EventLog._UnsafeGetAssertPermSet();
					permissionSet.Assert();
					RegistryKey registryKey = null;
					try
					{
						registryKey = this.GetLogRegKey(text, false);
						if (registryKey == null)
						{
							throw new InvalidOperationException(SR.GetString("MissingLog", new object[]
							{
								this.GetLogName(text),
								text
							}));
						}
						string text2 = (string)registryKey.GetValue("DisplayNameFile");
						if (text2 == null)
						{
							this.logDisplayName = this.GetLogName(text);
						}
						else
						{
							int num = (int)registryKey.GetValue("DisplayNameID");
							this.logDisplayName = this.FormatMessageWrapper(text2, (uint)num, null);
							if (this.logDisplayName == null)
							{
								this.logDisplayName = this.GetLogName(text);
							}
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
				return this.logDisplayName;
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06002E02 RID: 11778 RVA: 0x000CF07C File Offset: 0x000CD27C
		public string Log
		{
			get
			{
				string text = this.machineName;
				if (this.logName == null || this.logName.Length == 0)
				{
					EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, text);
					eventLogPermission.Demand();
				}
				return this.GetLogName(text);
			}
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x000CF0BC File Offset: 0x000CD2BC
		private string GetLogName(string currentMachineName)
		{
			if ((this.logName == null || this.logName.Length == 0) && this.sourceName != null && this.sourceName.Length != 0)
			{
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Write, currentMachineName);
				eventLogPermission.Demand();
				this.logName = EventLog._InternalLogNameFromSourceName(this.sourceName, currentMachineName);
			}
			return this.logName;
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06002E04 RID: 11780 RVA: 0x000CF11C File Offset: 0x000CD31C
		public string MachineName
		{
			get
			{
				string text = this.machineName;
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Write, text);
				eventLogPermission.Demand();
				return text;
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06002E05 RID: 11781 RVA: 0x000CF140 File Offset: 0x000CD340
		// (set) Token: 0x06002E06 RID: 11782 RVA: 0x000CF18C File Offset: 0x000CD38C
		[ComVisible(false)]
		public long MaximumKilobytes
		{
			get
			{
				string text = this.machineName;
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, text);
				eventLogPermission.Demand();
				object logRegValue = this.GetLogRegValue(text, "MaxSize");
				if (logRegValue != null)
				{
					int num = (int)logRegValue;
					return (long)((ulong)(num / 1024));
				}
				return 512L;
			}
			set
			{
				string text = this.machineName;
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, text);
				eventLogPermission.Demand();
				if (value < 64L || value > 4194240L || value % 64L != 0L)
				{
					throw new ArgumentOutOfRangeException("MaximumKilobytes", SR.GetString("MaximumKilobytesOutOfRange"));
				}
				PermissionSet permissionSet = EventLog._UnsafeGetAssertPermSet();
				permissionSet.Assert();
				long num = value * 1024L;
				int num2 = (int)num;
				using (RegistryKey logRegKey = this.GetLogRegKey(text, true))
				{
					logRegKey.SetValue("MaxSize", num2, RegistryValueKind.DWord);
				}
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06002E07 RID: 11783 RVA: 0x000CF230 File Offset: 0x000CD430
		internal Hashtable MessageLibraries
		{
			get
			{
				if (this.messageLibraries == null)
				{
					this.messageLibraries = new Hashtable(StringComparer.OrdinalIgnoreCase);
				}
				return this.messageLibraries;
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06002E08 RID: 11784 RVA: 0x000CF250 File Offset: 0x000CD450
		[ComVisible(false)]
		public OverflowAction OverflowAction
		{
			get
			{
				string text = this.machineName;
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, text);
				eventLogPermission.Demand();
				object logRegValue = this.GetLogRegValue(text, "Retention");
				if (logRegValue == null)
				{
					return OverflowAction.OverwriteOlder;
				}
				int num = (int)logRegValue;
				if (num == 0)
				{
					return OverflowAction.OverwriteAsNeeded;
				}
				if (num == -1)
				{
					return OverflowAction.DoNotOverwrite;
				}
				return OverflowAction.OverwriteOlder;
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06002E09 RID: 11785 RVA: 0x000CF298 File Offset: 0x000CD498
		[ComVisible(false)]
		public int MinimumRetentionDays
		{
			get
			{
				string text = this.machineName;
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, text);
				eventLogPermission.Demand();
				object logRegValue = this.GetLogRegValue(text, "Retention");
				if (logRegValue == null)
				{
					return 7;
				}
				int num = (int)logRegValue;
				if (num == 0 || num == -1)
				{
					return num;
				}
				return (int)((double)num / 86400.0);
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06002E0A RID: 11786 RVA: 0x000CF2EC File Offset: 0x000CD4EC
		// (set) Token: 0x06002E0B RID: 11787 RVA: 0x000CF31C File Offset: 0x000CD51C
		public bool EnableRaisingEvents
		{
			get
			{
				string text = this.machineName;
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Write, text);
				eventLogPermission.Demand();
				return this.boolFlags[8];
			}
			set
			{
				string text = this.machineName;
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Write, text);
				eventLogPermission.Demand();
				if (this.parent.ComponentDesignMode)
				{
					this.boolFlags[8] = value;
					return;
				}
				if (value)
				{
					this.StartRaisingEvents(text, this.GetLogName(text));
					return;
				}
				this.StopRaisingEvents(this.GetLogName(text));
			}
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06002E0C RID: 11788 RVA: 0x000CF37C File Offset: 0x000CD57C
		private int OldestEntryNumber
		{
			get
			{
				if (!this.IsOpenForRead)
				{
					this.OpenForRead(this.machineName);
				}
				int num;
				if (!Microsoft.Win32.UnsafeNativeMethods.GetOldestEventLogRecord(this.readHandle, out num))
				{
					throw SharedUtils.CreateSafeWin32Exception();
				}
				if (num == 0)
				{
					num = 1;
				}
				return num;
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06002E0D RID: 11789 RVA: 0x000CF3BA File Offset: 0x000CD5BA
		internal SafeEventLogReadHandle ReadHandle
		{
			get
			{
				if (!this.IsOpenForRead)
				{
					this.OpenForRead(this.machineName);
				}
				return this.readHandle;
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06002E0E RID: 11790 RVA: 0x000CF3D8 File Offset: 0x000CD5D8
		// (set) Token: 0x06002E0F RID: 11791 RVA: 0x000CF452 File Offset: 0x000CD652
		public ISynchronizeInvoke SynchronizingObject
		{
			[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
			get
			{
				string text = this.machineName;
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Write, text);
				eventLogPermission.Demand();
				if (this.synchronizingObject == null && this.parent.ComponentDesignMode)
				{
					IDesignerHost designerHost = (IDesignerHost)this.parent.ComponentGetService(typeof(IDesignerHost));
					if (designerHost != null)
					{
						object rootComponent = designerHost.RootComponent;
						if (rootComponent != null && rootComponent is ISynchronizeInvoke)
						{
							this.synchronizingObject = (ISynchronizeInvoke)rootComponent;
						}
					}
				}
				return this.synchronizingObject;
			}
			set
			{
				this.synchronizingObject = value;
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06002E10 RID: 11792 RVA: 0x000CF45C File Offset: 0x000CD65C
		public string Source
		{
			get
			{
				string text = this.machineName;
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Write, text);
				eventLogPermission.Demand();
				return this.sourceName;
			}
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x000CF488 File Offset: 0x000CD688
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		private static void AddListenerComponent(EventLogInternal component, string compMachineName, string compLogName)
		{
			object internalSyncObject = EventLogInternal.InternalSyncObject;
			lock (internalSyncObject)
			{
				EventLogInternal.LogListeningInfo logListeningInfo = (EventLogInternal.LogListeningInfo)EventLogInternal.listenerInfos[compLogName];
				if (logListeningInfo != null)
				{
					logListeningInfo.listeningComponents.Add(component);
				}
				else
				{
					logListeningInfo = new EventLogInternal.LogListeningInfo();
					logListeningInfo.listeningComponents.Add(component);
					logListeningInfo.handleOwner = new EventLogInternal(compLogName, compMachineName);
					logListeningInfo.waitHandle = new AutoResetEvent(false);
					if (!Microsoft.Win32.UnsafeNativeMethods.NotifyChangeEventLog(logListeningInfo.handleOwner.ReadHandle, logListeningInfo.waitHandle.SafeWaitHandle))
					{
						throw new InvalidOperationException(SR.GetString("CantMonitorEventLog"), SharedUtils.CreateSafeWin32Exception());
					}
					logListeningInfo.registeredWaitHandle = ThreadPool.RegisterWaitForSingleObject(logListeningInfo.waitHandle, new WaitOrTimerCallback(EventLogInternal.StaticCompletionCallback), logListeningInfo, -1, false);
					EventLogInternal.listenerInfos[compLogName] = logListeningInfo;
				}
			}
		}

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06002E12 RID: 11794 RVA: 0x000CF574 File Offset: 0x000CD774
		// (remove) Token: 0x06002E13 RID: 11795 RVA: 0x000CF5B0 File Offset: 0x000CD7B0
		public event EntryWrittenEventHandler EntryWritten
		{
			add
			{
				string text = this.machineName;
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, text);
				eventLogPermission.Demand();
				this.onEntryWrittenHandler = (EntryWrittenEventHandler)Delegate.Combine(this.onEntryWrittenHandler, value);
			}
			remove
			{
				string text = this.machineName;
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, text);
				eventLogPermission.Demand();
				this.onEntryWrittenHandler = (EntryWrittenEventHandler)Delegate.Remove(this.onEntryWrittenHandler, value);
			}
		}

		// Token: 0x06002E14 RID: 11796 RVA: 0x000CF5EC File Offset: 0x000CD7EC
		public void BeginInit()
		{
			string text = this.machineName;
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Write, text);
			eventLogPermission.Demand();
			if (this.boolFlags[4])
			{
				throw new InvalidOperationException(SR.GetString("InitTwice"));
			}
			this.boolFlags[4] = true;
			if (this.boolFlags[8])
			{
				this.StopListening(this.GetLogName(text));
			}
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x000CF658 File Offset: 0x000CD858
		public void Clear()
		{
			string text = this.machineName;
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, text);
			eventLogPermission.Demand();
			if (!this.IsOpenForRead)
			{
				this.OpenForRead(text);
			}
			if (!Microsoft.Win32.UnsafeNativeMethods.ClearEventLog(this.readHandle, Microsoft.Win32.NativeMethods.NullHandleRef))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error != 2)
				{
					throw SharedUtils.CreateSafeWin32Exception();
				}
			}
			this.Reset(text);
		}

		// Token: 0x06002E16 RID: 11798 RVA: 0x000CF6B5 File Offset: 0x000CD8B5
		public void Close()
		{
			this.Close(this.machineName);
		}

		// Token: 0x06002E17 RID: 11799 RVA: 0x000CF6C4 File Offset: 0x000CD8C4
		private void Close(string currentMachineName)
		{
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Write, currentMachineName);
			eventLogPermission.Demand();
			if (this.readHandle != null)
			{
				try
				{
					this.readHandle.Close();
				}
				catch (IOException)
				{
					throw SharedUtils.CreateSafeWin32Exception();
				}
				this.readHandle = null;
			}
			if (this.writeHandle != null)
			{
				try
				{
					this.writeHandle.Close();
				}
				catch (IOException)
				{
					throw SharedUtils.CreateSafeWin32Exception();
				}
				this.writeHandle = null;
			}
			if (this.boolFlags[8])
			{
				this.StopRaisingEvents(this.GetLogName(currentMachineName));
			}
			if (this.messageLibraries != null)
			{
				foreach (object obj in this.messageLibraries.Values)
				{
					Microsoft.Win32.SafeHandles.SafeLibraryHandle safeLibraryHandle = (Microsoft.Win32.SafeHandles.SafeLibraryHandle)obj;
					safeLibraryHandle.Close();
				}
				this.messageLibraries = null;
			}
			this.boolFlags[512] = false;
		}

		// Token: 0x06002E18 RID: 11800 RVA: 0x000CF7CC File Offset: 0x000CD9CC
		private void CompletionCallback(object context)
		{
			if (this.boolFlags[256])
			{
				return;
			}
			object instanceLockObject = this.InstanceLockObject;
			lock (instanceLockObject)
			{
				if (this.boolFlags[1])
				{
					return;
				}
				this.boolFlags[1] = true;
			}
			int i = this.lastSeenCount;
			try
			{
				int num = this.OldestEntryNumber;
				int num2 = this.EntryCount + num;
				if (this.lastSeenCount < num || this.lastSeenCount > num2)
				{
					this.lastSeenCount = num;
					i = this.lastSeenCount;
				}
				while (i < num2)
				{
					while (i < num2)
					{
						EventLogEntry entryWithOldest = this.GetEntryWithOldest(i);
						if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
						{
							this.SynchronizingObject.BeginInvoke(this.onEntryWrittenHandler, new object[]
							{
								this,
								new EntryWrittenEventArgs(entryWithOldest)
							});
						}
						else
						{
							this.onEntryWrittenHandler(this, new EntryWrittenEventArgs(entryWithOldest));
						}
						i++;
					}
					num = this.OldestEntryNumber;
					num2 = this.EntryCount + num;
				}
			}
			catch (Exception ex)
			{
			}
			try
			{
				int num3 = this.EntryCount + this.OldestEntryNumber;
				if (i > num3)
				{
					this.lastSeenCount = num3;
				}
				else
				{
					this.lastSeenCount = i;
				}
			}
			catch (Win32Exception ex2)
			{
			}
			object instanceLockObject2 = this.InstanceLockObject;
			lock (instanceLockObject2)
			{
				this.boolFlags[1] = false;
			}
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x000CF974 File Offset: 0x000CDB74
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x000CF984 File Offset: 0x000CDB84
		internal void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (this.IsOpen)
					{
						this.Close();
					}
					if (this.readHandle != null)
					{
						this.readHandle.Close();
						this.readHandle = null;
					}
					if (this.writeHandle != null)
					{
						this.writeHandle.Close();
						this.writeHandle = null;
					}
				}
			}
			finally
			{
				this.messageLibraries = null;
				this.boolFlags[256] = true;
			}
		}

		// Token: 0x06002E1B RID: 11803 RVA: 0x000CFA04 File Offset: 0x000CDC04
		public void EndInit()
		{
			string text = this.machineName;
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Write, text);
			eventLogPermission.Demand();
			this.boolFlags[4] = false;
			if (this.boolFlags[8])
			{
				this.StartListening(text, this.GetLogName(text));
			}
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x000CFA50 File Offset: 0x000CDC50
		internal string FormatMessageWrapper(string dllNameList, uint messageNum, string[] insertionStrings)
		{
			if (dllNameList == null)
			{
				return null;
			}
			if (insertionStrings == null)
			{
				insertionStrings = new string[0];
			}
			string[] array = dllNameList.Split(new char[] { ';' });
			foreach (string text in array)
			{
				if (text != null && text.Length != 0)
				{
					Microsoft.Win32.SafeHandles.SafeLibraryHandle safeLibraryHandle = null;
					if (this.IsOpen)
					{
						safeLibraryHandle = this.MessageLibraries[text] as Microsoft.Win32.SafeHandles.SafeLibraryHandle;
						if (safeLibraryHandle == null || safeLibraryHandle.IsInvalid)
						{
							safeLibraryHandle = Microsoft.Win32.SafeHandles.SafeLibraryHandle.LoadLibraryEx(text, IntPtr.Zero, 2);
							this.MessageLibraries[text] = safeLibraryHandle;
						}
					}
					else
					{
						safeLibraryHandle = Microsoft.Win32.SafeHandles.SafeLibraryHandle.LoadLibraryEx(text, IntPtr.Zero, 2);
					}
					if (!safeLibraryHandle.IsInvalid)
					{
						string text2 = null;
						try
						{
							text2 = EventLog.TryFormatMessage(safeLibraryHandle, messageNum, insertionStrings);
						}
						finally
						{
							if (!this.IsOpen)
							{
								safeLibraryHandle.Close();
							}
						}
						if (text2 != null)
						{
							return text2;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06002E1D RID: 11805 RVA: 0x000CFB44 File Offset: 0x000CDD44
		internal EventLogEntry[] GetAllEntries()
		{
			string text = this.machineName;
			if (!this.IsOpenForRead)
			{
				this.OpenForRead(text);
			}
			EventLogEntry[] array = new EventLogEntry[this.EntryCount];
			int i = 0;
			int oldestEntryNumber = this.OldestEntryNumber;
			int num = 0;
			while (i < array.Length)
			{
				byte[] array2 = new byte[40000];
				int num2;
				int num3;
				if (!Microsoft.Win32.UnsafeNativeMethods.ReadEventLog(this.readHandle, 6, oldestEntryNumber + i, array2, array2.Length, out num2, out num3))
				{
					num = Marshal.GetLastWin32Error();
					if (num != 122 && num != 1503)
					{
						break;
					}
					if (num == 1503)
					{
						this.Reset(text);
					}
					else if (num3 > array2.Length)
					{
						array2 = new byte[num3];
					}
					bool flag = Microsoft.Win32.UnsafeNativeMethods.ReadEventLog(this.readHandle, 6, oldestEntryNumber + i, array2, array2.Length, out num2, out num3);
					if (!flag)
					{
						break;
					}
					num = 0;
				}
				array[i] = new EventLogEntry(array2, 0, this);
				int num4 = EventLogInternal.IntFrom(array2, 0);
				i++;
				while (num4 < num2 && i < array.Length)
				{
					array[i] = new EventLogEntry(array2, num4, this);
					num4 += EventLogInternal.IntFrom(array2, num4);
					i++;
				}
			}
			if (i == array.Length)
			{
				return array;
			}
			if (num != 0)
			{
				throw new InvalidOperationException(SR.GetString("CantRetrieveEntries"), SharedUtils.CreateSafeWin32Exception(num));
			}
			throw new InvalidOperationException(SR.GetString("CantRetrieveEntries"));
		}

		// Token: 0x06002E1E RID: 11806 RVA: 0x000CFC90 File Offset: 0x000CDE90
		private int GetCachedEntryPos(int entryIndex)
		{
			if (this.cache == null || (this.boolFlags[2] && entryIndex < this.firstCachedEntry) || (!this.boolFlags[2] && entryIndex > this.firstCachedEntry) || this.firstCachedEntry == -1)
			{
				return -1;
			}
			while (this.lastSeenEntry < entryIndex)
			{
				this.lastSeenEntry++;
				if (this.boolFlags[2])
				{
					this.lastSeenPos = this.GetNextEntryPos(this.lastSeenPos);
					if (this.lastSeenPos < this.bytesCached)
					{
						continue;
					}
				}
				else
				{
					this.lastSeenPos = this.GetPreviousEntryPos(this.lastSeenPos);
					if (this.lastSeenPos >= 0)
					{
						continue;
					}
				}
				IL_FE:
				while (this.lastSeenEntry > entryIndex)
				{
					this.lastSeenEntry--;
					if (this.boolFlags[2])
					{
						this.lastSeenPos = this.GetPreviousEntryPos(this.lastSeenPos);
						if (this.lastSeenPos < 0)
						{
							break;
						}
					}
					else
					{
						this.lastSeenPos = this.GetNextEntryPos(this.lastSeenPos);
						if (this.lastSeenPos >= this.bytesCached)
						{
							break;
						}
					}
				}
				if (this.lastSeenPos >= this.bytesCached)
				{
					this.lastSeenPos = this.GetPreviousEntryPos(this.lastSeenPos);
					if (this.boolFlags[2])
					{
						this.lastSeenEntry--;
					}
					else
					{
						this.lastSeenEntry++;
					}
					return -1;
				}
				if (this.lastSeenPos < 0)
				{
					this.lastSeenPos = 0;
					if (this.boolFlags[2])
					{
						this.lastSeenEntry++;
					}
					else
					{
						this.lastSeenEntry--;
					}
					return -1;
				}
				return this.lastSeenPos;
			}
			goto IL_FE;
		}

		// Token: 0x06002E1F RID: 11807 RVA: 0x000CFE38 File Offset: 0x000CE038
		internal EventLogEntry GetEntryAt(int index)
		{
			EventLogEntry entryAtNoThrow = this.GetEntryAtNoThrow(index);
			if (entryAtNoThrow == null)
			{
				throw new ArgumentException(SR.GetString("IndexOutOfBounds", new object[] { index.ToString(CultureInfo.CurrentCulture) }));
			}
			return entryAtNoThrow;
		}

		// Token: 0x06002E20 RID: 11808 RVA: 0x000CFE78 File Offset: 0x000CE078
		internal EventLogEntry GetEntryAtNoThrow(int index)
		{
			if (!this.IsOpenForRead)
			{
				this.OpenForRead(this.machineName);
			}
			if (index < 0 || index >= this.EntryCount)
			{
				return null;
			}
			index += this.OldestEntryNumber;
			EventLogEntry eventLogEntry = null;
			try
			{
				eventLogEntry = this.GetEntryWithOldest(index);
			}
			catch (InvalidOperationException)
			{
			}
			return eventLogEntry;
		}

		// Token: 0x06002E21 RID: 11809 RVA: 0x000CFED4 File Offset: 0x000CE0D4
		private EventLogEntry GetEntryWithOldest(int index)
		{
			int cachedEntryPos = this.GetCachedEntryPos(index);
			if (cachedEntryPos >= 0)
			{
				return new EventLogEntry(this.cache, cachedEntryPos, this);
			}
			string text = this.machineName;
			int num;
			if (this.GetCachedEntryPos(index + 1) < 0)
			{
				num = 6;
				this.boolFlags[2] = true;
			}
			else
			{
				num = 10;
				this.boolFlags[2] = false;
			}
			this.cache = new byte[40000];
			int num2;
			int num3;
			bool flag = Microsoft.Win32.UnsafeNativeMethods.ReadEventLog(this.readHandle, num, index, this.cache, this.cache.Length, out num2, out num3);
			if (!flag)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error == 122 || lastWin32Error == 1503)
				{
					if (lastWin32Error == 1503)
					{
						byte[] array = this.cache;
						this.Reset(text);
						this.cache = array;
					}
					else if (num3 > this.cache.Length)
					{
						this.cache = new byte[num3];
					}
					flag = Microsoft.Win32.UnsafeNativeMethods.ReadEventLog(this.readHandle, 6, index, this.cache, this.cache.Length, out num2, out num3);
				}
				if (!flag)
				{
					throw new InvalidOperationException(SR.GetString("CantReadLogEntryAt", new object[] { index.ToString(CultureInfo.CurrentCulture) }), SharedUtils.CreateSafeWin32Exception());
				}
			}
			this.bytesCached = num2;
			this.firstCachedEntry = index;
			this.lastSeenEntry = index;
			this.lastSeenPos = 0;
			return new EventLogEntry(this.cache, 0, this);
		}

		// Token: 0x06002E22 RID: 11810 RVA: 0x000D0038 File Offset: 0x000CE238
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

		// Token: 0x06002E23 RID: 11811 RVA: 0x000D009C File Offset: 0x000CE29C
		private RegistryKey GetLogRegKey(string currentMachineName, bool writable)
		{
			string text = this.GetLogName(currentMachineName);
			if (!EventLogInternal.ValidLogName(text, false))
			{
				throw new InvalidOperationException(SR.GetString("BadLogName"));
			}
			RegistryKey registryKey = null;
			RegistryKey registryKey2 = null;
			try
			{
				registryKey = EventLogInternal.GetEventLogRegKey(currentMachineName, false);
				if (registryKey == null)
				{
					throw new InvalidOperationException(SR.GetString("RegKeyMissingShort", new object[] { "SYSTEM\\CurrentControlSet\\Services\\EventLog", currentMachineName }));
				}
				registryKey2 = registryKey.OpenSubKey(text, writable);
				if (registryKey2 == null)
				{
					throw new InvalidOperationException(SR.GetString("MissingLog", new object[] { text, currentMachineName }));
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
			return registryKey2;
		}

		// Token: 0x06002E24 RID: 11812 RVA: 0x000D0144 File Offset: 0x000CE344
		private object GetLogRegValue(string currentMachineName, string valuename)
		{
			PermissionSet permissionSet = EventLog._UnsafeGetAssertPermSet();
			permissionSet.Assert();
			RegistryKey registryKey = null;
			object obj;
			try
			{
				registryKey = this.GetLogRegKey(currentMachineName, false);
				if (registryKey == null)
				{
					throw new InvalidOperationException(SR.GetString("MissingLog", new object[]
					{
						this.GetLogName(currentMachineName),
						currentMachineName
					}));
				}
				object value = registryKey.GetValue(valuename);
				obj = value;
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
				CodeAccessPermission.RevertAssert();
			}
			return obj;
		}

		// Token: 0x06002E25 RID: 11813 RVA: 0x000D01BC File Offset: 0x000CE3BC
		private int GetNextEntryPos(int pos)
		{
			return pos + EventLogInternal.IntFrom(this.cache, pos);
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x000D01CC File Offset: 0x000CE3CC
		private int GetPreviousEntryPos(int pos)
		{
			return pos - EventLogInternal.IntFrom(this.cache, pos - 4);
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x000D01DE File Offset: 0x000CE3DE
		internal static string GetDllPath(string machineName)
		{
			return Path.Combine(SharedUtils.GetLatestBuildDllDirectory(machineName), "EventLogMessages.dll");
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x000D01F0 File Offset: 0x000CE3F0
		private static int IntFrom(byte[] buf, int offset)
		{
			return (-16777216 & ((int)buf[offset + 3] << 24)) | (16711680 & ((int)buf[offset + 2] << 16)) | (65280 & ((int)buf[offset + 1] << 8)) | (int)(byte.MaxValue & buf[offset]);
		}

		// Token: 0x06002E29 RID: 11817 RVA: 0x000D0228 File Offset: 0x000CE428
		[ComVisible(false)]
		public void ModifyOverflowPolicy(OverflowAction action, int retentionDays)
		{
			string text = this.machineName;
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, text);
			eventLogPermission.Demand();
			if (action < OverflowAction.DoNotOverwrite || action > OverflowAction.OverwriteOlder)
			{
				throw new InvalidEnumArgumentException("action", (int)action, typeof(OverflowAction));
			}
			long num = (long)action;
			if (action == OverflowAction.OverwriteOlder)
			{
				if (retentionDays < 1 || retentionDays > 365)
				{
					throw new ArgumentOutOfRangeException(SR.GetString("RentionDaysOutOfRange"));
				}
				num = (long)retentionDays * 86400L;
			}
			PermissionSet permissionSet = EventLog._UnsafeGetAssertPermSet();
			permissionSet.Assert();
			using (RegistryKey logRegKey = this.GetLogRegKey(text, true))
			{
				logRegKey.SetValue("Retention", num, RegistryValueKind.DWord);
			}
		}

		// Token: 0x06002E2A RID: 11818 RVA: 0x000D02E0 File Offset: 0x000CE4E0
		private void OpenForRead(string currentMachineName)
		{
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, currentMachineName);
			eventLogPermission.Demand();
			if (this.boolFlags[256])
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			string text = this.GetLogName(currentMachineName);
			if (text == null || text.Length == 0)
			{
				throw new ArgumentException(SR.GetString("MissingLogProperty"));
			}
			if (!EventLog.Exists(text, currentMachineName))
			{
				throw new InvalidOperationException(SR.GetString("LogDoesNotExists", new object[] { text, currentMachineName }));
			}
			SharedUtils.CheckEnvironment();
			this.lastSeenEntry = 0;
			this.lastSeenPos = 0;
			this.bytesCached = 0;
			this.firstCachedEntry = -1;
			SafeEventLogReadHandle safeEventLogReadHandle = SafeEventLogReadHandle.OpenEventLog(currentMachineName, text);
			if (safeEventLogReadHandle.IsInvalid)
			{
				Win32Exception ex = null;
				if (Marshal.GetLastWin32Error() != 0)
				{
					ex = SharedUtils.CreateSafeWin32Exception();
				}
				throw new InvalidOperationException(SR.GetString("CantOpenLog", new object[]
				{
					text.ToString(),
					currentMachineName
				}), ex);
			}
			this.readHandle = safeEventLogReadHandle;
		}

		// Token: 0x06002E2B RID: 11819 RVA: 0x000D03D4 File Offset: 0x000CE5D4
		private void OpenForWrite(string currentMachineName)
		{
			if (this.boolFlags[256])
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (this.sourceName == null || this.sourceName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("NeedSourceToOpen"));
			}
			SharedUtils.CheckEnvironment();
			SafeEventLogWriteHandle safeEventLogWriteHandle = SafeEventLogWriteHandle.RegisterEventSource(currentMachineName, this.sourceName);
			if (safeEventLogWriteHandle.IsInvalid)
			{
				Win32Exception ex = null;
				if (Marshal.GetLastWin32Error() != 0)
				{
					ex = SharedUtils.CreateSafeWin32Exception();
				}
				throw new InvalidOperationException(SR.GetString("CantOpenLogAccess", new object[] { this.sourceName }), ex);
			}
			this.writeHandle = safeEventLogWriteHandle;
		}

		// Token: 0x06002E2C RID: 11820 RVA: 0x000D047C File Offset: 0x000CE67C
		[ComVisible(false)]
		public void RegisterDisplayName(string resourceFile, long resourceId)
		{
			string text = this.machineName;
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Administer, text);
			eventLogPermission.Demand();
			PermissionSet permissionSet = EventLog._UnsafeGetAssertPermSet();
			permissionSet.Assert();
			using (RegistryKey logRegKey = this.GetLogRegKey(text, true))
			{
				logRegKey.SetValue("DisplayNameFile", resourceFile, RegistryValueKind.ExpandString);
				logRegKey.SetValue("DisplayNameID", resourceId, RegistryValueKind.DWord);
			}
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x000D04F0 File Offset: 0x000CE6F0
		private void Reset(string currentMachineName)
		{
			bool isOpenForRead = this.IsOpenForRead;
			bool isOpenForWrite = this.IsOpenForWrite;
			bool flag = this.boolFlags[8];
			bool flag2 = this.boolFlags[16];
			this.Close(currentMachineName);
			this.cache = null;
			if (isOpenForRead)
			{
				this.OpenForRead(currentMachineName);
			}
			if (isOpenForWrite)
			{
				this.OpenForWrite(currentMachineName);
			}
			if (flag2)
			{
				this.StartListening(currentMachineName, this.GetLogName(currentMachineName));
			}
			this.boolFlags[8] = flag;
		}

		// Token: 0x06002E2E RID: 11822 RVA: 0x000D0568 File Offset: 0x000CE768
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		private static void RemoveListenerComponent(EventLogInternal component, string compLogName)
		{
			object internalSyncObject = EventLogInternal.InternalSyncObject;
			lock (internalSyncObject)
			{
				EventLogInternal.LogListeningInfo logListeningInfo = (EventLogInternal.LogListeningInfo)EventLogInternal.listenerInfos[compLogName];
				logListeningInfo.listeningComponents.Remove(component);
				if (logListeningInfo.listeningComponents.Count == 0)
				{
					logListeningInfo.handleOwner.Dispose();
					logListeningInfo.registeredWaitHandle.Unregister(logListeningInfo.waitHandle);
					logListeningInfo.waitHandle.Close();
					EventLogInternal.listenerInfos[compLogName] = null;
				}
			}
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x000D0604 File Offset: 0x000CE804
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		private void StartListening(string currentMachineName, string currentLogName)
		{
			this.lastSeenCount = this.EntryCount + this.OldestEntryNumber;
			EventLogInternal.AddListenerComponent(this, currentMachineName, currentLogName);
			this.boolFlags[16] = true;
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x000D062F File Offset: 0x000CE82F
		private void StartRaisingEvents(string currentMachineName, string currentLogName)
		{
			if (!this.boolFlags[4] && !this.boolFlags[8] && !this.parent.ComponentDesignMode)
			{
				this.StartListening(currentMachineName, currentLogName);
			}
			this.boolFlags[8] = true;
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x000D0670 File Offset: 0x000CE870
		private static void StaticCompletionCallback(object context, bool wasSignaled)
		{
			EventLogInternal.LogListeningInfo logListeningInfo = (EventLogInternal.LogListeningInfo)context;
			if (logListeningInfo == null)
			{
				return;
			}
			object internalSyncObject = EventLogInternal.InternalSyncObject;
			EventLogInternal[] array;
			lock (internalSyncObject)
			{
				array = (EventLogInternal[])logListeningInfo.listeningComponents.ToArray(typeof(EventLogInternal));
			}
			for (int i = 0; i < array.Length; i++)
			{
				try
				{
					if (array[i] != null)
					{
						array[i].CompletionCallback(null);
					}
				}
				catch (ObjectDisposedException)
				{
				}
			}
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x000D0704 File Offset: 0x000CE904
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		private void StopListening(string currentLogName)
		{
			EventLogInternal.RemoveListenerComponent(this, currentLogName);
			this.boolFlags[16] = false;
		}

		// Token: 0x06002E33 RID: 11827 RVA: 0x000D071B File Offset: 0x000CE91B
		private void StopRaisingEvents(string currentLogName)
		{
			if (!this.boolFlags[4] && this.boolFlags[8] && !this.parent.ComponentDesignMode)
			{
				this.StopListening(currentLogName);
			}
			this.boolFlags[8] = false;
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x000D075C File Offset: 0x000CE95C
		private static bool CharIsPrintable(char c)
		{
			UnicodeCategory unicodeCategory = char.GetUnicodeCategory(c);
			return unicodeCategory != UnicodeCategory.Control || unicodeCategory == UnicodeCategory.Format || unicodeCategory == UnicodeCategory.LineSeparator || unicodeCategory == UnicodeCategory.ParagraphSeparator || unicodeCategory == UnicodeCategory.OtherNotAssigned;
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x000D078C File Offset: 0x000CE98C
		internal static bool ValidLogName(string logName, bool ignoreEmpty)
		{
			if (logName.Length == 0 && !ignoreEmpty)
			{
				return false;
			}
			foreach (char c in logName)
			{
				if (!EventLogInternal.CharIsPrintable(c) || c == '\\' || c == '*' || c == '?')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002E36 RID: 11830 RVA: 0x000D07DC File Offset: 0x000CE9DC
		private void VerifyAndCreateSource(string sourceName, string currentMachineName)
		{
			if (this.boolFlags[512])
			{
				return;
			}
			if (!EventLog.SourceExists(sourceName, currentMachineName, true))
			{
				Mutex mutex = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					SharedUtils.EnterMutex("netfxeventlog.1.0", ref mutex);
					if (!EventLog.SourceExists(sourceName, currentMachineName, true))
					{
						if (this.GetLogName(currentMachineName) == null)
						{
							this.logName = "Application";
						}
						EventLog.CreateEventSource(new EventSourceCreationData(sourceName, this.GetLogName(currentMachineName), currentMachineName));
						this.Reset(currentMachineName);
						goto IL_131;
					}
					string text = EventLog.LogNameFromSourceName(sourceName, currentMachineName);
					string text2 = this.GetLogName(currentMachineName);
					if (text != null && text2 != null && string.Compare(text, text2, StringComparison.OrdinalIgnoreCase) != 0)
					{
						throw new ArgumentException(SR.GetString("LogSourceMismatch", new object[]
						{
							this.Source.ToString(),
							text2,
							text
						}));
					}
					goto IL_131;
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
			EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Write, currentMachineName);
			eventLogPermission.Demand();
			string text3 = EventLog._InternalLogNameFromSourceName(sourceName, currentMachineName);
			string text4 = this.GetLogName(currentMachineName);
			if (text3 != null && text4 != null && string.Compare(text3, text4, StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new ArgumentException(SR.GetString("LogSourceMismatch", new object[]
				{
					this.Source.ToString(),
					text4,
					text3
				}));
			}
			IL_131:
			this.boolFlags[512] = true;
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x000D093C File Offset: 0x000CEB3C
		public void WriteEntry(string message)
		{
			this.WriteEntry(message, EventLogEntryType.Information, 0, 0, null);
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x000D0949 File Offset: 0x000CEB49
		public void WriteEntry(string message, EventLogEntryType type)
		{
			this.WriteEntry(message, type, 0, 0, null);
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x000D0956 File Offset: 0x000CEB56
		public void WriteEntry(string message, EventLogEntryType type, int eventID)
		{
			this.WriteEntry(message, type, eventID, 0, null);
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x000D0963 File Offset: 0x000CEB63
		public void WriteEntry(string message, EventLogEntryType type, int eventID, short category)
		{
			this.WriteEntry(message, type, eventID, category, null);
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x000D0974 File Offset: 0x000CEB74
		public void WriteEntry(string message, EventLogEntryType type, int eventID, short category, byte[] rawData)
		{
			if (eventID < 0 || eventID > 65535)
			{
				throw new ArgumentException(SR.GetString("EventID", new object[] { eventID, 0, 65535 }));
			}
			if (this.Source.Length == 0)
			{
				throw new ArgumentException(SR.GetString("NeedSourceToWrite"));
			}
			if (!Enum.IsDefined(typeof(EventLogEntryType), type))
			{
				throw new InvalidEnumArgumentException("type", (int)type, typeof(EventLogEntryType));
			}
			string text = this.machineName;
			if (!this.boolFlags[32])
			{
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Write, text);
				eventLogPermission.Demand();
				this.boolFlags[32] = true;
			}
			this.VerifyAndCreateSource(this.sourceName, text);
			this.InternalWriteEvent((uint)eventID, (ushort)category, type, new string[] { message }, rawData, text);
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x000D0A64 File Offset: 0x000CEC64
		[ComVisible(false)]
		public void WriteEvent(EventInstance instance, params object[] values)
		{
			this.WriteEvent(instance, null, values);
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x000D0A70 File Offset: 0x000CEC70
		[ComVisible(false)]
		public void WriteEvent(EventInstance instance, byte[] data, params object[] values)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (this.Source.Length == 0)
			{
				throw new ArgumentException(SR.GetString("NeedSourceToWrite"));
			}
			string text = this.machineName;
			if (!this.boolFlags[32])
			{
				EventLogPermission eventLogPermission = new EventLogPermission(EventLogPermissionAccess.Write, text);
				eventLogPermission.Demand();
				this.boolFlags[32] = true;
			}
			this.VerifyAndCreateSource(this.Source, text);
			string[] array = null;
			if (values != null)
			{
				array = new string[values.Length];
				for (int i = 0; i < values.Length; i++)
				{
					if (values[i] != null)
					{
						array[i] = values[i].ToString();
					}
					else
					{
						array[i] = string.Empty;
					}
				}
			}
			this.InternalWriteEvent((uint)instance.InstanceId, (ushort)instance.CategoryId, instance.EntryType, array, data, text);
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x000D0B3C File Offset: 0x000CED3C
		private void InternalWriteEvent(uint eventID, ushort category, EventLogEntryType type, string[] strings, byte[] rawData, string currentMachineName)
		{
			if (strings == null)
			{
				strings = new string[0];
			}
			if (strings.Length >= 256)
			{
				throw new ArgumentException(SR.GetString("TooManyReplacementStrings"));
			}
			for (int i = 0; i < strings.Length; i++)
			{
				if (strings[i] == null)
				{
					strings[i] = string.Empty;
				}
				if (strings[i].Length > 32766)
				{
					throw new ArgumentException(SR.GetString("LogEntryTooLong"));
				}
			}
			if (rawData == null)
			{
				rawData = new byte[0];
			}
			if (this.Source.Length == 0)
			{
				throw new ArgumentException(SR.GetString("NeedSourceToWrite"));
			}
			if (!this.IsOpenForWrite)
			{
				this.OpenForWrite(currentMachineName);
			}
			IntPtr[] array = new IntPtr[strings.Length];
			GCHandle[] array2 = new GCHandle[strings.Length];
			GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			try
			{
				for (int j = 0; j < strings.Length; j++)
				{
					array2[j] = GCHandle.Alloc(strings[j], GCHandleType.Pinned);
					array[j] = array2[j].AddrOfPinnedObject();
				}
				byte[] array3 = null;
				if (!Microsoft.Win32.UnsafeNativeMethods.ReportEvent(this.writeHandle, (short)type, category, eventID, array3, (short)strings.Length, rawData.Length, new HandleRef(this, gchandle.AddrOfPinnedObject()), rawData))
				{
					throw SharedUtils.CreateSafeWin32Exception();
				}
			}
			finally
			{
				for (int k = 0; k < strings.Length; k++)
				{
					if (array2[k].IsAllocated)
					{
						array2[k].Free();
					}
				}
				gchandle.Free();
			}
		}

		// Token: 0x0400272A RID: 10026
		private EventLogEntryCollection entriesCollection;

		// Token: 0x0400272B RID: 10027
		internal string logName;

		// Token: 0x0400272C RID: 10028
		private int lastSeenCount;

		// Token: 0x0400272D RID: 10029
		internal readonly string machineName;

		// Token: 0x0400272E RID: 10030
		internal EntryWrittenEventHandler onEntryWrittenHandler;

		// Token: 0x0400272F RID: 10031
		private SafeEventLogReadHandle readHandle;

		// Token: 0x04002730 RID: 10032
		internal readonly string sourceName;

		// Token: 0x04002731 RID: 10033
		private SafeEventLogWriteHandle writeHandle;

		// Token: 0x04002732 RID: 10034
		private string logDisplayName;

		// Token: 0x04002733 RID: 10035
		private const int BUF_SIZE = 40000;

		// Token: 0x04002734 RID: 10036
		private int bytesCached;

		// Token: 0x04002735 RID: 10037
		private byte[] cache;

		// Token: 0x04002736 RID: 10038
		private int firstCachedEntry = -1;

		// Token: 0x04002737 RID: 10039
		private int lastSeenEntry;

		// Token: 0x04002738 RID: 10040
		private int lastSeenPos;

		// Token: 0x04002739 RID: 10041
		private ISynchronizeInvoke synchronizingObject;

		// Token: 0x0400273A RID: 10042
		private readonly EventLog parent;

		// Token: 0x0400273B RID: 10043
		private const string EventLogKey = "SYSTEM\\CurrentControlSet\\Services\\EventLog";

		// Token: 0x0400273C RID: 10044
		internal const string DllName = "EventLogMessages.dll";

		// Token: 0x0400273D RID: 10045
		private const string eventLogMutexName = "netfxeventlog.1.0";

		// Token: 0x0400273E RID: 10046
		private const int SecondsPerDay = 86400;

		// Token: 0x0400273F RID: 10047
		private const int DefaultMaxSize = 524288;

		// Token: 0x04002740 RID: 10048
		private const int DefaultRetention = 604800;

		// Token: 0x04002741 RID: 10049
		private const int Flag_notifying = 1;

		// Token: 0x04002742 RID: 10050
		private const int Flag_forwards = 2;

		// Token: 0x04002743 RID: 10051
		private const int Flag_initializing = 4;

		// Token: 0x04002744 RID: 10052
		internal const int Flag_monitoring = 8;

		// Token: 0x04002745 RID: 10053
		private const int Flag_registeredAsListener = 16;

		// Token: 0x04002746 RID: 10054
		private const int Flag_writeGranted = 32;

		// Token: 0x04002747 RID: 10055
		private const int Flag_disposed = 256;

		// Token: 0x04002748 RID: 10056
		private const int Flag_sourceVerified = 512;

		// Token: 0x04002749 RID: 10057
		private BitVector32 boolFlags;

		// Token: 0x0400274A RID: 10058
		private Hashtable messageLibraries;

		// Token: 0x0400274B RID: 10059
		private static readonly Hashtable listenerInfos = new Hashtable(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400274C RID: 10060
		private object m_InstanceLockObject;

		// Token: 0x0400274D RID: 10061
		private static object s_InternalSyncObject;

		// Token: 0x0200087A RID: 2170
		private class LogListeningInfo
		{
			// Token: 0x0400370C RID: 14092
			public EventLogInternal handleOwner;

			// Token: 0x0400370D RID: 14093
			public RegisteredWaitHandle registeredWaitHandle;

			// Token: 0x0400370E RID: 14094
			public WaitHandle waitHandle;

			// Token: 0x0400370F RID: 14095
			public ArrayList listeningComponents = new ArrayList();
		}
	}
}
