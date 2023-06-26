using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.IO
{
	/// <summary>Listens to the file system change notifications and raises events when a directory, or file in a directory, changes.</summary>
	// Token: 0x020003FF RID: 1023
	[DefaultEvent("Changed")]
	[IODescription("FileSystemWatcherDesc")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class FileSystemWatcher : Component, ISupportInitialize
	{
		// Token: 0x06002656 RID: 9814 RVA: 0x000B0974 File Offset: 0x000AEB74
		static FileSystemWatcher()
		{
			foreach (object obj in Enum.GetValues(typeof(NotifyFilters)))
			{
				int num = (int)obj;
				FileSystemWatcher.notifyFiltersValidMask |= num;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemWatcher" /> class.</summary>
		// Token: 0x06002657 RID: 9815 RVA: 0x000B09F8 File Offset: 0x000AEBF8
		public FileSystemWatcher()
		{
			this.directory = string.Empty;
			this.filter = "*.*";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemWatcher" /> class, given the specified directory to monitor.</summary>
		/// <param name="path">The directory to monitor, in standard or Universal Naming Convention (UNC) notation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter is an empty string ("").  
		///  -or-  
		///  The path specified through the <paramref name="path" /> parameter does not exist.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">
		///   <paramref name="path" /> is too long.</exception>
		// Token: 0x06002658 RID: 9816 RVA: 0x000B0A29 File Offset: 0x000AEC29
		public FileSystemWatcher(string path)
			: this(path, "*.*")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemWatcher" /> class, given the specified directory and type of files to monitor.</summary>
		/// <param name="path">The directory to monitor, in standard or Universal Naming Convention (UNC) notation.</param>
		/// <param name="filter">The type of files to watch. For example, "*.txt" watches for changes to all text files.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="filter" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter is an empty string ("").  
		///  -or-  
		///  The path specified through the <paramref name="path" /> parameter does not exist.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">
		///   <paramref name="path" /> is too long.</exception>
		// Token: 0x06002659 RID: 9817 RVA: 0x000B0A38 File Offset: 0x000AEC38
		public FileSystemWatcher(string path, string filter)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			if (path.Length == 0 || !Directory.Exists(path))
			{
				throw new ArgumentException(SR.GetString("InvalidDirName", new object[] { path }));
			}
			this.directory = path;
			this.filter = filter;
		}

		/// <summary>Gets or sets the type of changes to watch for.</summary>
		/// <returns>One of the <see cref="T:System.IO.NotifyFilters" /> values. The default is the bitwise OR combination of <see langword="LastWrite" />, <see langword="FileName" />, and <see langword="DirectoryName" />.</returns>
		/// <exception cref="T:System.ArgumentException">The value is not a valid bitwise OR combination of the <see cref="T:System.IO.NotifyFilters" /> values.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value that is being set is not valid.</exception>
		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x0600265A RID: 9818 RVA: 0x000B0AB2 File Offset: 0x000AECB2
		// (set) Token: 0x0600265B RID: 9819 RVA: 0x000B0ABA File Offset: 0x000AECBA
		[DefaultValue(NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite)]
		[IODescription("FSW_ChangedFilter")]
		public NotifyFilters NotifyFilter
		{
			get
			{
				return this.notifyFilters;
			}
			set
			{
				if ((value & (NotifyFilters)(~(NotifyFilters)FileSystemWatcher.notifyFiltersValidMask)) != (NotifyFilters)0)
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(NotifyFilters));
				}
				if (this.notifyFilters != value)
				{
					this.notifyFilters = value;
					this.Restart();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the component is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the component is enabled; otherwise, <see langword="false" />. The default is <see langword="false" />. If you are using the component on a designer in Visual Studio 2005, the default is <see langword="true" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.FileSystemWatcher" /> object has been disposed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The directory specified in <see cref="P:System.IO.FileSystemWatcher.Path" /> could not be found.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.IO.FileSystemWatcher.Path" /> has not been set or is invalid.</exception>
		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x0600265C RID: 9820 RVA: 0x000B0AF2 File Offset: 0x000AECF2
		// (set) Token: 0x0600265D RID: 9821 RVA: 0x000B0AFA File Offset: 0x000AECFA
		[DefaultValue(false)]
		[IODescription("FSW_Enabled")]
		public bool EnableRaisingEvents
		{
			get
			{
				return this.enabled;
			}
			set
			{
				if (this.enabled == value)
				{
					return;
				}
				this.enabled = value;
				if (!this.IsSuspended())
				{
					if (this.enabled)
					{
						this.StartRaisingEvents();
						return;
					}
					this.StopRaisingEvents();
				}
			}
		}

		/// <summary>Gets or sets the filter string used to determine what files are monitored in a directory.</summary>
		/// <returns>The filter string. The default is "*.*" (Watches all files.)</returns>
		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x0600265E RID: 9822 RVA: 0x000B0B2A File Offset: 0x000AED2A
		// (set) Token: 0x0600265F RID: 9823 RVA: 0x000B0B32 File Offset: 0x000AED32
		[DefaultValue("*.*")]
		[IODescription("FSW_Filter")]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[SettingsBindable(true)]
		public string Filter
		{
			get
			{
				return this.filter;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = "*.*";
				}
				if (string.Compare(this.filter, value, StringComparison.OrdinalIgnoreCase) != 0)
				{
					this.filter = value;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether subdirectories within the specified path should be monitored.</summary>
		/// <returns>
		///   <see langword="true" /> if you want to monitor subdirectories; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06002660 RID: 9824 RVA: 0x000B0B59 File Offset: 0x000AED59
		// (set) Token: 0x06002661 RID: 9825 RVA: 0x000B0B61 File Offset: 0x000AED61
		[DefaultValue(false)]
		[IODescription("FSW_IncludeSubdirectories")]
		public bool IncludeSubdirectories
		{
			get
			{
				return this.includeSubdirectories;
			}
			set
			{
				if (this.includeSubdirectories != value)
				{
					this.includeSubdirectories = value;
					this.Restart();
				}
			}
		}

		/// <summary>Gets or sets the size (in bytes) of the internal buffer.</summary>
		/// <returns>The internal buffer size in bytes. The default is 8192 (8 KB).</returns>
		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06002662 RID: 9826 RVA: 0x000B0B79 File Offset: 0x000AED79
		// (set) Token: 0x06002663 RID: 9827 RVA: 0x000B0B81 File Offset: 0x000AED81
		[Browsable(false)]
		[DefaultValue(8192)]
		public int InternalBufferSize
		{
			get
			{
				return this.internalBufferSize;
			}
			set
			{
				if (this.internalBufferSize != value)
				{
					if (value < 4096)
					{
						value = 4096;
					}
					this.internalBufferSize = value;
					this.Restart();
				}
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06002664 RID: 9828 RVA: 0x000B0BA8 File Offset: 0x000AEDA8
		private bool IsHandleInvalid
		{
			get
			{
				return this.directoryHandle == null || this.directoryHandle.IsInvalid;
			}
		}

		/// <summary>Gets or sets the path of the directory to watch.</summary>
		/// <returns>The path to monitor. The default is an empty string ("").</returns>
		/// <exception cref="T:System.ArgumentException">The specified path does not exist or could not be found.  
		///  -or-  
		///  The specified path contains wildcard characters.  
		///  -or-  
		///  The specified path contains invalid path characters.</exception>
		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06002665 RID: 9829 RVA: 0x000B0BBF File Offset: 0x000AEDBF
		// (set) Token: 0x06002666 RID: 9830 RVA: 0x000B0BC8 File Offset: 0x000AEDC8
		[DefaultValue("")]
		[IODescription("FSW_Path")]
		[Editor("System.Diagnostics.Design.FSWPathEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[SettingsBindable(true)]
		public string Path
		{
			get
			{
				return this.directory;
			}
			set
			{
				value = ((value == null) ? string.Empty : value);
				if (string.Compare(this.directory, value, StringComparison.OrdinalIgnoreCase) != 0)
				{
					if (base.DesignMode)
					{
						if (value.IndexOfAny(FileSystemWatcher.wildcards) != -1 || value.IndexOfAny(System.IO.Path.GetInvalidPathChars()) != -1)
						{
							throw new ArgumentException(SR.GetString("InvalidDirName", new object[] { value }));
						}
					}
					else if (!Directory.Exists(value))
					{
						throw new ArgumentException(SR.GetString("InvalidDirName", new object[] { value }));
					}
					this.directory = value;
					this.readGranted = false;
					this.Restart();
				}
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.ComponentModel.ISite" /> for the <see cref="T:System.IO.FileSystemWatcher" />.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ISite" /> for the <see cref="T:System.IO.FileSystemWatcher" />.</returns>
		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06002667 RID: 9831 RVA: 0x000B0C65 File Offset: 0x000AEE65
		// (set) Token: 0x06002668 RID: 9832 RVA: 0x000B0C6D File Offset: 0x000AEE6D
		[Browsable(false)]
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				base.Site = value;
				if (this.Site != null && this.Site.DesignMode)
				{
					this.EnableRaisingEvents = true;
				}
			}
		}

		/// <summary>Gets or sets the object used to marshal the event handler calls issued as a result of a directory change.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISynchronizeInvoke" /> that represents the object used to marshal the event handler calls issued as a result of a directory change. The default is <see langword="null" />.</returns>
		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06002669 RID: 9833 RVA: 0x000B0C94 File Offset: 0x000AEE94
		// (set) Token: 0x0600266A RID: 9834 RVA: 0x000B0CEE File Offset: 0x000AEEEE
		[Browsable(false)]
		[DefaultValue(null)]
		[IODescription("FSW_SynchronizingObject")]
		public ISynchronizeInvoke SynchronizingObject
		{
			get
			{
				if (this.synchronizingObject == null && base.DesignMode)
				{
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
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

		/// <summary>Occurs when a file or directory in the specified <see cref="P:System.IO.FileSystemWatcher.Path" /> is changed.</summary>
		// Token: 0x14000035 RID: 53
		// (add) Token: 0x0600266B RID: 9835 RVA: 0x000B0CF7 File Offset: 0x000AEEF7
		// (remove) Token: 0x0600266C RID: 9836 RVA: 0x000B0D10 File Offset: 0x000AEF10
		[IODescription("FSW_Changed")]
		public event FileSystemEventHandler Changed
		{
			add
			{
				this.onChangedHandler = (FileSystemEventHandler)Delegate.Combine(this.onChangedHandler, value);
			}
			remove
			{
				this.onChangedHandler = (FileSystemEventHandler)Delegate.Remove(this.onChangedHandler, value);
			}
		}

		/// <summary>Occurs when a file or directory in the specified <see cref="P:System.IO.FileSystemWatcher.Path" /> is created.</summary>
		// Token: 0x14000036 RID: 54
		// (add) Token: 0x0600266D RID: 9837 RVA: 0x000B0D29 File Offset: 0x000AEF29
		// (remove) Token: 0x0600266E RID: 9838 RVA: 0x000B0D42 File Offset: 0x000AEF42
		[IODescription("FSW_Created")]
		public event FileSystemEventHandler Created
		{
			add
			{
				this.onCreatedHandler = (FileSystemEventHandler)Delegate.Combine(this.onCreatedHandler, value);
			}
			remove
			{
				this.onCreatedHandler = (FileSystemEventHandler)Delegate.Remove(this.onCreatedHandler, value);
			}
		}

		/// <summary>Occurs when a file or directory in the specified <see cref="P:System.IO.FileSystemWatcher.Path" /> is deleted.</summary>
		// Token: 0x14000037 RID: 55
		// (add) Token: 0x0600266F RID: 9839 RVA: 0x000B0D5B File Offset: 0x000AEF5B
		// (remove) Token: 0x06002670 RID: 9840 RVA: 0x000B0D74 File Offset: 0x000AEF74
		[IODescription("FSW_Deleted")]
		public event FileSystemEventHandler Deleted
		{
			add
			{
				this.onDeletedHandler = (FileSystemEventHandler)Delegate.Combine(this.onDeletedHandler, value);
			}
			remove
			{
				this.onDeletedHandler = (FileSystemEventHandler)Delegate.Remove(this.onDeletedHandler, value);
			}
		}

		/// <summary>Occurs when the instance of <see cref="T:System.IO.FileSystemWatcher" /> is unable to continue monitoring changes or when the internal buffer overflows.</summary>
		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06002671 RID: 9841 RVA: 0x000B0D8D File Offset: 0x000AEF8D
		// (remove) Token: 0x06002672 RID: 9842 RVA: 0x000B0DA6 File Offset: 0x000AEFA6
		[Browsable(false)]
		public event ErrorEventHandler Error
		{
			add
			{
				this.onErrorHandler = (ErrorEventHandler)Delegate.Combine(this.onErrorHandler, value);
			}
			remove
			{
				this.onErrorHandler = (ErrorEventHandler)Delegate.Remove(this.onErrorHandler, value);
			}
		}

		/// <summary>Occurs when a file or directory in the specified <see cref="P:System.IO.FileSystemWatcher.Path" /> is renamed.</summary>
		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06002673 RID: 9843 RVA: 0x000B0DBF File Offset: 0x000AEFBF
		// (remove) Token: 0x06002674 RID: 9844 RVA: 0x000B0DD8 File Offset: 0x000AEFD8
		[IODescription("FSW_Renamed")]
		public event RenamedEventHandler Renamed
		{
			add
			{
				this.onRenamedHandler = (RenamedEventHandler)Delegate.Combine(this.onRenamedHandler, value);
			}
			remove
			{
				this.onRenamedHandler = (RenamedEventHandler)Delegate.Remove(this.onRenamedHandler, value);
			}
		}

		/// <summary>Begins the initialization of a <see cref="T:System.IO.FileSystemWatcher" /> used on a form or used by another component. The initialization occurs at run time.</summary>
		// Token: 0x06002675 RID: 9845 RVA: 0x000B0DF4 File Offset: 0x000AEFF4
		public void BeginInit()
		{
			bool flag = this.enabled;
			this.StopRaisingEvents();
			this.enabled = flag;
			this.initializing = true;
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x000B0E1C File Offset: 0x000AF01C
		private unsafe void CompletionStatusChanged(uint errorCode, uint numBytes, NativeOverlapped* overlappedPointer)
		{
			Overlapped overlapped = Overlapped.Unpack(overlappedPointer);
			FileSystemWatcher.FSWAsyncResult fswasyncResult = (FileSystemWatcher.FSWAsyncResult)overlapped.AsyncResult;
			try
			{
				if (!this.stopListening)
				{
					lock (this)
					{
						if (errorCode != 0U)
						{
							if (errorCode != 995U)
							{
								this.OnError(new ErrorEventArgs(new Win32Exception((int)errorCode)));
								this.EnableRaisingEvents = false;
							}
						}
						else if (fswasyncResult.session == this.currentSession)
						{
							if (numBytes == 0U)
							{
								this.NotifyInternalBufferOverflowEvent();
							}
							else
							{
								int num = 0;
								string text = null;
								string text2 = null;
								int num2;
								do
								{
									int num3;
									try
									{
										byte[] array;
										byte* ptr;
										if ((array = fswasyncResult.buffer) == null || array.Length == 0)
										{
											ptr = null;
										}
										else
										{
											ptr = &array[0];
										}
										num2 = *(int*)(ptr + num);
										num3 = *(int*)(ptr + num + 4);
										int num4 = *(int*)(ptr + num + 8);
										text2 = new string((char*)(ptr + num + 12), 0, num4 / 2);
									}
									finally
									{
										byte[] array = null;
									}
									if (num3 == 4)
									{
										text = text2;
									}
									else if (num3 == 5)
									{
										if (text != null)
										{
											this.NotifyRenameEventArgs(WatcherChangeTypes.Renamed, text2, text);
											text = null;
										}
										else
										{
											this.NotifyRenameEventArgs(WatcherChangeTypes.Renamed, text2, text);
											text = null;
										}
									}
									else
									{
										if (text != null)
										{
											this.NotifyRenameEventArgs(WatcherChangeTypes.Renamed, null, text);
											text = null;
										}
										this.NotifyFileSystemEventArgs(num3, text2);
									}
									num += num2;
								}
								while (num2 != 0);
								if (text != null)
								{
									this.NotifyRenameEventArgs(WatcherChangeTypes.Renamed, null, text);
									text = null;
								}
							}
						}
					}
				}
			}
			finally
			{
				Overlapped.Free(overlappedPointer);
				if (!this.stopListening && !this.runOnce)
				{
					this.Monitor(fswasyncResult.buffer);
				}
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.FileSystemWatcher" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002677 RID: 9847 RVA: 0x000B0FF0 File Offset: 0x000AF1F0
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.StopRaisingEvents();
					this.onChangedHandler = null;
					this.onCreatedHandler = null;
					this.onDeletedHandler = null;
					this.onRenamedHandler = null;
					this.onErrorHandler = null;
					this.readGranted = false;
				}
				else
				{
					this.stopListening = true;
					if (!this.IsHandleInvalid)
					{
						this.directoryHandle.Close();
					}
				}
			}
			finally
			{
				this.disposed = true;
				base.Dispose(disposing);
			}
		}

		/// <summary>Ends the initialization of a <see cref="T:System.IO.FileSystemWatcher" /> used on a form or used by another component. The initialization occurs at run time.</summary>
		// Token: 0x06002678 RID: 9848 RVA: 0x000B1070 File Offset: 0x000AF270
		public void EndInit()
		{
			this.initializing = false;
			if (this.directory.Length != 0 && this.enabled)
			{
				this.StartRaisingEvents();
			}
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x000B1094 File Offset: 0x000AF294
		private bool IsSuspended()
		{
			return this.initializing || base.DesignMode;
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x000B10A8 File Offset: 0x000AF2A8
		private bool MatchPattern(string relativePath)
		{
			string fileName = System.IO.Path.GetFileName(relativePath);
			return fileName != null && PatternMatcher.StrictMatchPattern(this.filter.ToUpper(CultureInfo.InvariantCulture), fileName.ToUpper(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x000B10E4 File Offset: 0x000AF2E4
		private unsafe void Monitor(byte[] buffer)
		{
			if (!this.enabled || this.IsHandleInvalid)
			{
				return;
			}
			Overlapped overlapped = new Overlapped();
			if (buffer == null)
			{
				try
				{
					buffer = new byte[this.internalBufferSize];
				}
				catch (OutOfMemoryException)
				{
					throw new OutOfMemoryException(SR.GetString("BufferSizeTooLarge", new object[] { this.internalBufferSize.ToString(CultureInfo.CurrentCulture) }));
				}
			}
			overlapped.AsyncResult = new FileSystemWatcher.FSWAsyncResult
			{
				session = this.currentSession,
				buffer = buffer
			};
			NativeOverlapped* ptr = overlapped.Pack(new IOCompletionCallback(this.CompletionStatusChanged), buffer);
			bool flag = false;
			try
			{
				if (!this.IsHandleInvalid)
				{
					try
					{
						byte[] array;
						byte* ptr2;
						if ((array = buffer) == null || array.Length == 0)
						{
							ptr2 = null;
						}
						else
						{
							ptr2 = &array[0];
						}
						int num;
						flag = Microsoft.Win32.UnsafeNativeMethods.ReadDirectoryChangesW(this.directoryHandle, new HandleRef(this, (IntPtr)((void*)ptr2)), this.internalBufferSize, this.includeSubdirectories ? 1 : 0, (int)this.notifyFilters, out num, ptr, Microsoft.Win32.NativeMethods.NullHandleRef);
					}
					finally
					{
						byte[] array = null;
					}
				}
			}
			catch (ObjectDisposedException)
			{
			}
			catch (ArgumentNullException)
			{
			}
			finally
			{
				if (!flag)
				{
					Overlapped.Free(ptr);
					if (!this.IsHandleInvalid)
					{
						this.OnError(new ErrorEventArgs(new Win32Exception()));
					}
				}
			}
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x000B124C File Offset: 0x000AF44C
		private void NotifyFileSystemEventArgs(int action, string name)
		{
			if (!this.MatchPattern(name))
			{
				return;
			}
			switch (action)
			{
			case 1:
				this.OnCreated(new FileSystemEventArgs(WatcherChangeTypes.Created, this.directory, name));
				return;
			case 2:
				this.OnDeleted(new FileSystemEventArgs(WatcherChangeTypes.Deleted, this.directory, name));
				return;
			case 3:
				this.OnChanged(new FileSystemEventArgs(WatcherChangeTypes.Changed, this.directory, name));
				return;
			default:
				return;
			}
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x000B12B4 File Offset: 0x000AF4B4
		private void NotifyInternalBufferOverflowEvent()
		{
			InternalBufferOverflowException ex = new InternalBufferOverflowException(SR.GetString("FSW_BufferOverflow", new object[] { this.directory }));
			ErrorEventArgs errorEventArgs = new ErrorEventArgs(ex);
			this.OnError(errorEventArgs);
		}

		// Token: 0x0600267E RID: 9854 RVA: 0x000B12F0 File Offset: 0x000AF4F0
		private void NotifyRenameEventArgs(WatcherChangeTypes action, string name, string oldName)
		{
			if (!this.MatchPattern(name) && !this.MatchPattern(oldName))
			{
				return;
			}
			RenamedEventArgs renamedEventArgs = new RenamedEventArgs(action, this.directory, name, oldName);
			this.OnRenamed(renamedEventArgs);
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Changed" /> event.</summary>
		/// <param name="e">A <see cref="T:System.IO.FileSystemEventArgs" /> that contains the event data.</param>
		// Token: 0x0600267F RID: 9855 RVA: 0x000B1328 File Offset: 0x000AF528
		protected void OnChanged(FileSystemEventArgs e)
		{
			FileSystemEventHandler fileSystemEventHandler = this.onChangedHandler;
			if (fileSystemEventHandler != null)
			{
				if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
				{
					this.SynchronizingObject.BeginInvoke(fileSystemEventHandler, new object[] { this, e });
					return;
				}
				fileSystemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Created" /> event.</summary>
		/// <param name="e">A <see cref="T:System.IO.FileSystemEventArgs" /> that contains the event data.</param>
		// Token: 0x06002680 RID: 9856 RVA: 0x000B1378 File Offset: 0x000AF578
		protected void OnCreated(FileSystemEventArgs e)
		{
			FileSystemEventHandler fileSystemEventHandler = this.onCreatedHandler;
			if (fileSystemEventHandler != null)
			{
				if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
				{
					this.SynchronizingObject.BeginInvoke(fileSystemEventHandler, new object[] { this, e });
					return;
				}
				fileSystemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Deleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.IO.FileSystemEventArgs" /> that contains the event data.</param>
		// Token: 0x06002681 RID: 9857 RVA: 0x000B13C8 File Offset: 0x000AF5C8
		protected void OnDeleted(FileSystemEventArgs e)
		{
			FileSystemEventHandler fileSystemEventHandler = this.onDeletedHandler;
			if (fileSystemEventHandler != null)
			{
				if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
				{
					this.SynchronizingObject.BeginInvoke(fileSystemEventHandler, new object[] { this, e });
					return;
				}
				fileSystemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Error" /> event.</summary>
		/// <param name="e">An <see cref="T:System.IO.ErrorEventArgs" /> that contains the event data.</param>
		// Token: 0x06002682 RID: 9858 RVA: 0x000B1418 File Offset: 0x000AF618
		protected void OnError(ErrorEventArgs e)
		{
			ErrorEventHandler errorEventHandler = this.onErrorHandler;
			if (errorEventHandler != null)
			{
				if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
				{
					this.SynchronizingObject.BeginInvoke(errorEventHandler, new object[] { this, e });
					return;
				}
				errorEventHandler(this, e);
			}
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x000B1468 File Offset: 0x000AF668
		private void OnInternalFileSystemEventArgs(object sender, FileSystemEventArgs e)
		{
			lock (this)
			{
				if (!this.isChanged)
				{
					this.changedResult = new WaitForChangedResult(e.ChangeType, e.Name, false);
					this.isChanged = true;
					System.Threading.Monitor.Pulse(this);
				}
			}
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x000B14CC File Offset: 0x000AF6CC
		private void OnInternalRenameEventArgs(object sender, RenamedEventArgs e)
		{
			lock (this)
			{
				if (!this.isChanged)
				{
					this.changedResult = new WaitForChangedResult(e.ChangeType, e.Name, e.OldName, false);
					this.isChanged = true;
					System.Threading.Monitor.Pulse(this);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Renamed" /> event.</summary>
		/// <param name="e">A <see cref="T:System.IO.RenamedEventArgs" /> that contains the event data.</param>
		// Token: 0x06002685 RID: 9861 RVA: 0x000B1534 File Offset: 0x000AF734
		protected void OnRenamed(RenamedEventArgs e)
		{
			RenamedEventHandler renamedEventHandler = this.onRenamedHandler;
			if (renamedEventHandler != null)
			{
				if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
				{
					this.SynchronizingObject.BeginInvoke(renamedEventHandler, new object[] { this, e });
					return;
				}
				renamedEventHandler(this, e);
			}
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x000B1584 File Offset: 0x000AF784
		private void Restart()
		{
			if (!this.IsSuspended() && this.enabled)
			{
				this.StopRaisingEvents();
				this.StartRaisingEvents();
			}
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x000B15A4 File Offset: 0x000AF7A4
		private void StartRaisingEvents()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			try
			{
				new EnvironmentPermission(PermissionState.Unrestricted).Assert();
				if (Environment.OSVersion.Platform != PlatformID.Win32NT)
				{
					throw new PlatformNotSupportedException(SR.GetString("WinNTRequired"));
				}
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			if (this.IsSuspended())
			{
				this.enabled = true;
				return;
			}
			if (!this.readGranted)
			{
				string fullPath = System.IO.Path.GetFullPath(this.directory);
				FileIOPermission fileIOPermission = new FileIOPermission(FileIOPermissionAccess.Read, fullPath);
				fileIOPermission.Demand();
				this.readGranted = true;
			}
			if (!this.IsHandleInvalid)
			{
				return;
			}
			this.directoryHandle = Microsoft.Win32.NativeMethods.CreateFile(this.directory, 1, 7, null, 3, 1107296256, new SafeFileHandle(IntPtr.Zero, false));
			if (this.IsHandleInvalid)
			{
				throw new FileNotFoundException(SR.GetString("FSW_IOError", new object[] { this.directory }));
			}
			this.stopListening = false;
			Interlocked.Increment(ref this.currentSession);
			SecurityPermission securityPermission = new SecurityPermission(PermissionState.Unrestricted);
			securityPermission.Assert();
			try
			{
				ThreadPool.BindHandle(this.directoryHandle);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			this.enabled = true;
			this.Monitor(null);
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x000B16E8 File Offset: 0x000AF8E8
		private void StopRaisingEvents()
		{
			if (this.IsSuspended())
			{
				this.enabled = false;
				return;
			}
			if (this.IsHandleInvalid)
			{
				return;
			}
			this.stopListening = true;
			this.directoryHandle.Close();
			this.directoryHandle = null;
			Interlocked.Increment(ref this.currentSession);
			this.enabled = false;
		}

		/// <summary>A synchronous method that returns a structure that contains specific information on the change that occurred, given the type of change you want to monitor.</summary>
		/// <param name="changeType">The <see cref="T:System.IO.WatcherChangeTypes" /> to watch for.</param>
		/// <returns>A <see cref="T:System.IO.WaitForChangedResult" /> that contains specific information on the change that occurred.</returns>
		// Token: 0x06002689 RID: 9865 RVA: 0x000B173A File Offset: 0x000AF93A
		public WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType)
		{
			return this.WaitForChanged(changeType, -1);
		}

		/// <summary>A synchronous method that returns a structure that contains specific information on the change that occurred, given the type of change you want to monitor and the time (in milliseconds) to wait before timing out.</summary>
		/// <param name="changeType">The <see cref="T:System.IO.WatcherChangeTypes" /> to watch for.</param>
		/// <param name="timeout">The time (in milliseconds) to wait before timing out.</param>
		/// <returns>A <see cref="T:System.IO.WaitForChangedResult" /> that contains specific information on the change that occurred.</returns>
		// Token: 0x0600268A RID: 9866 RVA: 0x000B1744 File Offset: 0x000AF944
		public WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType, int timeout)
		{
			FileSystemEventHandler fileSystemEventHandler = new FileSystemEventHandler(this.OnInternalFileSystemEventArgs);
			RenamedEventHandler renamedEventHandler = new RenamedEventHandler(this.OnInternalRenameEventArgs);
			this.isChanged = false;
			this.changedResult = WaitForChangedResult.TimedOutResult;
			if ((changeType & WatcherChangeTypes.Created) != (WatcherChangeTypes)0)
			{
				this.Created += fileSystemEventHandler;
			}
			if ((changeType & WatcherChangeTypes.Deleted) != (WatcherChangeTypes)0)
			{
				this.Deleted += fileSystemEventHandler;
			}
			if ((changeType & WatcherChangeTypes.Changed) != (WatcherChangeTypes)0)
			{
				this.Changed += fileSystemEventHandler;
			}
			if ((changeType & WatcherChangeTypes.Renamed) != (WatcherChangeTypes)0)
			{
				this.Renamed += renamedEventHandler;
			}
			bool enableRaisingEvents = this.EnableRaisingEvents;
			if (!enableRaisingEvents)
			{
				this.runOnce = true;
				this.EnableRaisingEvents = true;
			}
			WaitForChangedResult timedOutResult = WaitForChangedResult.TimedOutResult;
			lock (this)
			{
				if (timeout == -1)
				{
					while (!this.isChanged)
					{
						System.Threading.Monitor.Wait(this);
					}
				}
				else
				{
					System.Threading.Monitor.Wait(this, timeout, true);
				}
				timedOutResult = this.changedResult;
			}
			this.EnableRaisingEvents = enableRaisingEvents;
			this.runOnce = false;
			if ((changeType & WatcherChangeTypes.Created) != (WatcherChangeTypes)0)
			{
				this.Created -= fileSystemEventHandler;
			}
			if ((changeType & WatcherChangeTypes.Deleted) != (WatcherChangeTypes)0)
			{
				this.Deleted -= fileSystemEventHandler;
			}
			if ((changeType & WatcherChangeTypes.Changed) != (WatcherChangeTypes)0)
			{
				this.Changed -= fileSystemEventHandler;
			}
			if ((changeType & WatcherChangeTypes.Renamed) != (WatcherChangeTypes)0)
			{
				this.Renamed -= renamedEventHandler;
			}
			return timedOutResult;
		}

		// Token: 0x040020A4 RID: 8356
		private string directory;

		// Token: 0x040020A5 RID: 8357
		private string filter;

		// Token: 0x040020A6 RID: 8358
		private SafeFileHandle directoryHandle;

		// Token: 0x040020A7 RID: 8359
		private const NotifyFilters defaultNotifyFilters = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite;

		// Token: 0x040020A8 RID: 8360
		private NotifyFilters notifyFilters = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite;

		// Token: 0x040020A9 RID: 8361
		private bool includeSubdirectories;

		// Token: 0x040020AA RID: 8362
		private bool enabled;

		// Token: 0x040020AB RID: 8363
		private bool initializing;

		// Token: 0x040020AC RID: 8364
		private int internalBufferSize = 8192;

		// Token: 0x040020AD RID: 8365
		private WaitForChangedResult changedResult;

		// Token: 0x040020AE RID: 8366
		private bool isChanged;

		// Token: 0x040020AF RID: 8367
		private ISynchronizeInvoke synchronizingObject;

		// Token: 0x040020B0 RID: 8368
		private bool readGranted;

		// Token: 0x040020B1 RID: 8369
		private bool disposed;

		// Token: 0x040020B2 RID: 8370
		private int currentSession;

		// Token: 0x040020B3 RID: 8371
		private FileSystemEventHandler onChangedHandler;

		// Token: 0x040020B4 RID: 8372
		private FileSystemEventHandler onCreatedHandler;

		// Token: 0x040020B5 RID: 8373
		private FileSystemEventHandler onDeletedHandler;

		// Token: 0x040020B6 RID: 8374
		private RenamedEventHandler onRenamedHandler;

		// Token: 0x040020B7 RID: 8375
		private ErrorEventHandler onErrorHandler;

		// Token: 0x040020B8 RID: 8376
		private bool stopListening;

		// Token: 0x040020B9 RID: 8377
		private bool runOnce;

		// Token: 0x040020BA RID: 8378
		private static readonly char[] wildcards = new char[] { '?', '*' };

		// Token: 0x040020BB RID: 8379
		private static int notifyFiltersValidMask = 0;

		// Token: 0x02000812 RID: 2066
		private sealed class FSWAsyncResult : IAsyncResult
		{
			// Token: 0x17000F9B RID: 3995
			// (get) Token: 0x060044D4 RID: 17620 RVA: 0x0011FE66 File Offset: 0x0011E066
			public bool IsCompleted
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000F9C RID: 3996
			// (get) Token: 0x060044D5 RID: 17621 RVA: 0x0011FE6D File Offset: 0x0011E06D
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000F9D RID: 3997
			// (get) Token: 0x060044D6 RID: 17622 RVA: 0x0011FE74 File Offset: 0x0011E074
			public object AsyncState
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000F9E RID: 3998
			// (get) Token: 0x060044D7 RID: 17623 RVA: 0x0011FE7B File Offset: 0x0011E07B
			public bool CompletedSynchronously
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x04003560 RID: 13664
			internal int session;

			// Token: 0x04003561 RID: 13665
			internal byte[] buffer;
		}
	}
}
