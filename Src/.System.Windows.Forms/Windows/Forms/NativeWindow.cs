using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Internal;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	/// <summary>Provides a low-level encapsulation of a window handle and a window procedure.</summary>
	// Token: 0x02000304 RID: 772
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class NativeWindow : MarshalByRefObject, IWin32Window
	{
		// Token: 0x0600313A RID: 12602 RVA: 0x000DDF14 File Offset: 0x000DC114
		static NativeWindow()
		{
			EventHandler eventHandler = new EventHandler(NativeWindow.OnShutdown);
			AppDomain.CurrentDomain.ProcessExit += eventHandler;
			AppDomain.CurrentDomain.DomainUnload += eventHandler;
			int num = NativeWindow.primes[4];
			NativeWindow.hashBuckets = new NativeWindow.HandleBucket[num];
			NativeWindow.hashLoadSize = (int)(0.72f * (float)num);
			if (NativeWindow.hashLoadSize >= num)
			{
				NativeWindow.hashLoadSize = num - 1;
			}
			NativeWindow.hashForIdHandle = new Dictionary<short, IntPtr>();
			NativeWindow.hashForHandleId = new Dictionary<IntPtr, short>();
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Windows.Forms.NativeWindow" /> class.</summary>
		// Token: 0x0600313B RID: 12603 RVA: 0x000DDFC6 File Offset: 0x000DC1C6
		public NativeWindow()
		{
			this.weakThisPtr = new WeakReference(this);
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x0600313C RID: 12604 RVA: 0x000DDFEF File Offset: 0x000DC1EF
		internal DpiAwarenessContext DpiAwarenessContext
		{
			get
			{
				return this.windowDpiAwarenessContext;
			}
		}

		/// <summary>Releases the resources associated with this window.</summary>
		// Token: 0x0600313D RID: 12605 RVA: 0x000DDFF8 File Offset: 0x000DC1F8
		~NativeWindow()
		{
			this.ForceExitMessageLoop();
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x000DE024 File Offset: 0x000DC224
		internal void ForceExitMessageLoop()
		{
			IntPtr intPtr;
			bool flag2;
			lock (this)
			{
				intPtr = this.handle;
				flag2 = this.ownHandle;
			}
			if (this.handle != IntPtr.Zero)
			{
				if (UnsafeNativeMethods.IsWindow(new HandleRef(null, this.handle)))
				{
					int num;
					int windowThreadProcessId = SafeNativeMethods.GetWindowThreadProcessId(new HandleRef(null, this.handle), out num);
					Application.ThreadContext threadContext = Application.ThreadContext.FromId(windowThreadProcessId);
					IntPtr intPtr2 = ((threadContext == null) ? IntPtr.Zero : threadContext.GetHandle());
					if (intPtr2 != IntPtr.Zero)
					{
						int num2 = 0;
						SafeNativeMethods.GetExitCodeThread(new HandleRef(null, intPtr2), out num2);
						if (!AppDomain.CurrentDomain.IsFinalizingForUnload() && num2 == 259)
						{
							IntPtr intPtr3;
							UnsafeNativeMethods.SendMessageTimeout(new HandleRef(null, this.handle), NativeMethods.WM_UIUNSUBCLASS, IntPtr.Zero, IntPtr.Zero, 2, 100, out intPtr3) == IntPtr.Zero;
						}
					}
				}
				if (this.handle != IntPtr.Zero)
				{
					this.ReleaseHandle(true);
				}
			}
			if (intPtr != IntPtr.Zero && flag2)
			{
				UnsafeNativeMethods.PostMessage(new HandleRef(this, intPtr), 16, 0, 0);
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x0600313F RID: 12607 RVA: 0x000DE164 File Offset: 0x000DC364
		internal static bool AnyHandleCreated
		{
			get
			{
				return NativeWindow.anyHandleCreated;
			}
		}

		/// <summary>Gets the handle for this window.</summary>
		/// <returns>If successful, an <see cref="T:System.IntPtr" /> representing the handle to the associated native Win32 window; otherwise, 0 if no handle is associated with the window.</returns>
		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x06003140 RID: 12608 RVA: 0x000DE16B File Offset: 0x000DC36B
		public IntPtr Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06003141 RID: 12609 RVA: 0x000DE173 File Offset: 0x000DC373
		internal NativeWindow PreviousWindow
		{
			get
			{
				return this.previousWindow;
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06003142 RID: 12610 RVA: 0x000DE17B File Offset: 0x000DC37B
		internal static IntPtr UserDefindowProc
		{
			get
			{
				return NativeWindow.userDefWindowProc;
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06003143 RID: 12611 RVA: 0x000DE184 File Offset: 0x000DC384
		private static int WndProcFlags
		{
			get
			{
				int num = (int)NativeWindow.wndProcFlags;
				if (num == 0)
				{
					if (NativeWindow.userSetProcFlags != 0)
					{
						num = (int)NativeWindow.userSetProcFlags;
					}
					else if (NativeWindow.userSetProcFlagsForApp != 0)
					{
						num = (int)NativeWindow.userSetProcFlagsForApp;
					}
					else if (!Application.CustomThreadExceptionHandlerAttached)
					{
						if (Debugger.IsAttached)
						{
							num |= 4;
						}
						else
						{
							num = NativeWindow.AdjustWndProcFlagsFromRegistry(num);
							if ((num & 2) != 0)
							{
								num = NativeWindow.AdjustWndProcFlagsFromMetadata(num);
								if ((num & 16) != 0)
								{
									if ((num & 8) != 0)
									{
										num = NativeWindow.AdjustWndProcFlagsFromConfig(num);
									}
									else
									{
										num |= 4;
									}
								}
							}
						}
					}
					num |= 1;
					NativeWindow.wndProcFlags = (byte)num;
				}
				return num;
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x06003144 RID: 12612 RVA: 0x000DE203 File Offset: 0x000DC403
		internal static bool WndProcShouldBeDebuggable
		{
			get
			{
				return (NativeWindow.WndProcFlags & 4) != 0;
			}
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x000DE210 File Offset: 0x000DC410
		private static void AddWindowToTable(IntPtr handle, NativeWindow window)
		{
			object obj = NativeWindow.internalSyncObject;
			lock (obj)
			{
				if (NativeWindow.handleCount >= NativeWindow.hashLoadSize)
				{
					NativeWindow.ExpandTable();
				}
				NativeWindow.anyHandleCreated = true;
				NativeWindow.anyHandleCreatedInApp = true;
				uint num2;
				uint num3;
				uint num = NativeWindow.InitHash(handle, NativeWindow.hashBuckets.Length, out num2, out num3);
				int num4 = 0;
				int num5 = -1;
				GCHandle gchandle = GCHandle.Alloc(window, GCHandleType.Weak);
				int num6;
				for (;;)
				{
					num6 = (int)(num2 % (uint)NativeWindow.hashBuckets.Length);
					if (num5 == -1 && NativeWindow.hashBuckets[num6].handle == new IntPtr(-1) && NativeWindow.hashBuckets[num6].hash_coll < 0)
					{
						num5 = num6;
					}
					if (NativeWindow.hashBuckets[num6].handle == IntPtr.Zero || (NativeWindow.hashBuckets[num6].handle == new IntPtr(-1) && ((long)NativeWindow.hashBuckets[num6].hash_coll & (long)((ulong)(-2147483648))) == 0L))
					{
						break;
					}
					if ((long)(NativeWindow.hashBuckets[num6].hash_coll & 2147483647) == (long)((ulong)num) && handle == NativeWindow.hashBuckets[num6].handle)
					{
						goto Block_11;
					}
					if (num5 == -1)
					{
						NativeWindow.HandleBucket[] array = NativeWindow.hashBuckets;
						int num7 = num6;
						array[num7].hash_coll = array[num7].hash_coll | int.MinValue;
					}
					num2 += num3;
					if (++num4 >= NativeWindow.hashBuckets.Length)
					{
						goto Block_15;
					}
				}
				if (num5 != -1)
				{
					num6 = num5;
				}
				NativeWindow.hashBuckets[num6].window = gchandle;
				NativeWindow.hashBuckets[num6].handle = handle;
				NativeWindow.HandleBucket[] array2 = NativeWindow.hashBuckets;
				int num8 = num6;
				array2[num8].hash_coll = array2[num8].hash_coll | (int)num;
				NativeWindow.handleCount++;
				return;
				Block_11:
				GCHandle window2 = NativeWindow.hashBuckets[num6].window;
				if (window2.IsAllocated)
				{
					if (window2.Target != null)
					{
						window.previousWindow = (NativeWindow)window2.Target;
						window.previousWindow.nextWindow = window;
					}
					window2.Free();
				}
				NativeWindow.hashBuckets[num6].window = gchandle;
				return;
				Block_15:
				if (num5 != -1)
				{
					NativeWindow.hashBuckets[num5].window = gchandle;
					NativeWindow.hashBuckets[num5].handle = handle;
					NativeWindow.HandleBucket[] array3 = NativeWindow.hashBuckets;
					int num9 = num5;
					array3[num9].hash_coll = array3[num9].hash_coll | (int)num;
					NativeWindow.handleCount++;
				}
			}
		}

		// Token: 0x06003146 RID: 12614 RVA: 0x000DE4A4 File Offset: 0x000DC6A4
		internal static void AddWindowToIDTable(object wrapper, IntPtr handle)
		{
			NativeWindow.hashForIdHandle[NativeWindow.globalID] = handle;
			NativeWindow.hashForHandleId[handle] = NativeWindow.globalID;
			UnsafeNativeMethods.SetWindowLong(new HandleRef(wrapper, handle), -12, new HandleRef(wrapper, (IntPtr)((int)NativeWindow.globalID)));
			NativeWindow.globalID += 1;
		}

		// Token: 0x06003147 RID: 12615 RVA: 0x000DE4FD File Offset: 0x000DC6FD
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static int AdjustWndProcFlagsFromConfig(int wndProcFlags)
		{
			if (WindowsFormsSection.GetSection().JitDebugging)
			{
				wndProcFlags |= 4;
			}
			return wndProcFlags;
		}

		// Token: 0x06003148 RID: 12616 RVA: 0x000DE514 File Offset: 0x000DC714
		private static int AdjustWndProcFlagsFromRegistry(int wndProcFlags)
		{
			new RegistryPermission(PermissionState.Unrestricted).Assert();
			try
			{
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\.NETFramework");
				if (registryKey == null)
				{
					return wndProcFlags;
				}
				try
				{
					object value = registryKey.GetValue("DbgJITDebugLaunchSetting");
					if (value != null)
					{
						int num = 0;
						try
						{
							num = (int)value;
						}
						catch (InvalidCastException)
						{
							num = 1;
						}
						if (num != 1)
						{
							wndProcFlags |= 2;
							wndProcFlags |= 8;
						}
					}
					else if (registryKey.GetValue("DbgManagedDebugger") != null)
					{
						wndProcFlags |= 2;
						wndProcFlags |= 8;
					}
				}
				finally
				{
					registryKey.Close();
				}
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return wndProcFlags;
		}

		// Token: 0x06003149 RID: 12617 RVA: 0x000DE5C4 File Offset: 0x000DC7C4
		private static int AdjustWndProcFlagsFromMetadata(int wndProcFlags)
		{
			if ((wndProcFlags & 2) != 0)
			{
				Assembly entryAssembly = Assembly.GetEntryAssembly();
				if (entryAssembly != null && Attribute.IsDefined(entryAssembly, typeof(DebuggableAttribute)))
				{
					Attribute[] customAttributes = Attribute.GetCustomAttributes(entryAssembly, typeof(DebuggableAttribute));
					if (customAttributes.Length != 0)
					{
						DebuggableAttribute debuggableAttribute = (DebuggableAttribute)customAttributes[0];
						if (debuggableAttribute.IsJITTrackingEnabled)
						{
							wndProcFlags |= 16;
						}
					}
				}
			}
			return wndProcFlags;
		}

		/// <summary>Assigns a handle to this window.</summary>
		/// <param name="handle">The handle to assign to this window.</param>
		/// <exception cref="T:System.Exception">This window already has a handle.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The windows procedure for the associated native window could not be retrieved.</exception>
		// Token: 0x0600314A RID: 12618 RVA: 0x000DE624 File Offset: 0x000DC824
		public void AssignHandle(IntPtr handle)
		{
			this.AssignHandle(handle, true);
		}

		// Token: 0x0600314B RID: 12619 RVA: 0x000DE630 File Offset: 0x000DC830
		internal void AssignHandle(IntPtr handle, bool assignUniqueID)
		{
			lock (this)
			{
				this.CheckReleased();
				this.handle = handle;
				if (DpiHelper.EnableDpiChangedHighDpiImprovements && this.windowDpiAwarenessContext != DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED)
				{
					DpiAwarenessContext dpiAwarenessContext = CommonUnsafeNativeMethods.TryGetDpiAwarenessContextForWindow(this.handle);
					if (dpiAwarenessContext != DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED && !CommonUnsafeNativeMethods.TryFindDpiAwarenessContextsEqual(this.windowDpiAwarenessContext, dpiAwarenessContext))
					{
						this.windowDpiAwarenessContext = dpiAwarenessContext;
					}
				}
				if (NativeWindow.userDefWindowProc == IntPtr.Zero)
				{
					string text = ((Marshal.SystemDefaultCharSize == 1) ? "DefWindowProcA" : "DefWindowProcW");
					NativeWindow.userDefWindowProc = UnsafeNativeMethods.GetProcAddress(new HandleRef(null, UnsafeNativeMethods.GetModuleHandle("user32.dll")), text);
					if (NativeWindow.userDefWindowProc == IntPtr.Zero)
					{
						throw new Win32Exception();
					}
				}
				this.defWindowProc = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, handle), -4);
				if (NativeWindow.WndProcShouldBeDebuggable)
				{
					this.windowProc = new NativeMethods.WndProc(this.DebuggableCallback);
				}
				else
				{
					this.windowProc = new NativeMethods.WndProc(this.Callback);
				}
				NativeWindow.AddWindowToTable(handle, this);
				UnsafeNativeMethods.SetWindowLong(new HandleRef(this, handle), -4, this.windowProc);
				this.windowProcPtr = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, handle), -4);
				if (assignUniqueID && ((int)(long)UnsafeNativeMethods.GetWindowLong(new HandleRef(this, handle), -16) & 1073741824) != 0 && (int)(long)UnsafeNativeMethods.GetWindowLong(new HandleRef(this, handle), -12) == 0)
				{
					UnsafeNativeMethods.SetWindowLong(new HandleRef(this, handle), -12, new HandleRef(this, handle));
				}
				if (this.suppressedGC)
				{
					new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
					try
					{
						GC.ReRegisterForFinalize(this);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					this.suppressedGC = false;
				}
				this.OnHandleChange();
			}
		}

		// Token: 0x0600314C RID: 12620 RVA: 0x000DE80C File Offset: 0x000DCA0C
		private IntPtr Callback(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
		{
			Message message = Message.Create(hWnd, msg, wparam, lparam);
			try
			{
				if (this.weakThisPtr.IsAlive && this.weakThisPtr.Target != null)
				{
					this.WndProc(ref message);
				}
				else
				{
					this.DefWndProc(ref message);
				}
			}
			catch (Exception ex)
			{
				this.OnThreadException(ex);
			}
			finally
			{
				if (msg == 130)
				{
					this.ReleaseHandle(false);
				}
				if (msg == NativeMethods.WM_UIUNSUBCLASS)
				{
					this.ReleaseHandle(true);
				}
			}
			return message.Result;
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x000DE8A0 File Offset: 0x000DCAA0
		private void CheckReleased()
		{
			if (this.handle != IntPtr.Zero)
			{
				throw new InvalidOperationException(SR.GetString("HandleAlreadyExists"));
			}
		}

		/// <summary>Creates a window and its handle with the specified creation parameters.</summary>
		/// <param name="cp">A <see cref="T:System.Windows.Forms.CreateParams" /> that specifies the creation parameters for this window.</param>
		/// <exception cref="T:System.OutOfMemoryException">The operating system ran out of resources when trying to create the native window.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The native Win32 API could not create the specified window.</exception>
		/// <exception cref="T:System.InvalidOperationException">The handle of the current native window is already assigned; in explanation, the <see cref="P:System.Windows.Forms.NativeWindow.Handle" /> property is not equal to <see cref="F:System.IntPtr.Zero" />.</exception>
		// Token: 0x0600314E RID: 12622 RVA: 0x000DE8C4 File Offset: 0x000DCAC4
		public virtual void CreateHandle(CreateParams cp)
		{
			IntSecurity.CreateAnyWindow.Demand();
			if ((cp.Style & 1073741824) != 1073741824 || cp.Parent == IntPtr.Zero)
			{
				IntSecurity.TopLevelWindow.Demand();
			}
			lock (this)
			{
				this.CheckReleased();
				NativeWindow.WindowClass windowClass = NativeWindow.WindowClass.Create(cp.ClassName, cp.ClassStyle);
				object obj = NativeWindow.createWindowSyncObject;
				lock (obj)
				{
					if (!(this.handle != IntPtr.Zero))
					{
						windowClass.targetWindow = this;
						IntPtr intPtr = IntPtr.Zero;
						int num = 0;
						using (DpiHelper.EnterDpiAwarenessScope(this.windowDpiAwarenessContext))
						{
							IntPtr moduleHandle = UnsafeNativeMethods.GetModuleHandle(null);
							try
							{
								if (cp.Caption != null && cp.Caption.Length > 32767)
								{
									cp.Caption = cp.Caption.Substring(0, 32767);
								}
								intPtr = UnsafeNativeMethods.CreateWindowEx(cp.ExStyle, windowClass.windowClassName, cp.Caption, cp.Style, cp.X, cp.Y, cp.Width, cp.Height, new HandleRef(cp, cp.Parent), NativeMethods.NullHandleRef, new HandleRef(null, moduleHandle), cp.Param);
								num = Marshal.GetLastWin32Error();
							}
							catch (NullReferenceException ex)
							{
								throw new OutOfMemoryException(SR.GetString("ErrorCreatingHandle"), ex);
							}
						}
						windowClass.targetWindow = null;
						if (intPtr == IntPtr.Zero)
						{
							throw new Win32Exception(num, SR.GetString("ErrorCreatingHandle"));
						}
						this.ownHandle = true;
						System.Internal.HandleCollector.Add(intPtr, NativeMethods.CommonHandles.Window);
					}
				}
			}
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x000DEAE4 File Offset: 0x000DCCE4
		private IntPtr DebuggableCallback(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
		{
			Message message = Message.Create(hWnd, msg, wparam, lparam);
			try
			{
				if (this.weakThisPtr.IsAlive && this.weakThisPtr.Target != null)
				{
					this.WndProc(ref message);
				}
				else
				{
					this.DefWndProc(ref message);
				}
			}
			finally
			{
				if (msg == 130)
				{
					this.ReleaseHandle(false);
				}
				if (msg == NativeMethods.WM_UIUNSUBCLASS)
				{
					this.ReleaseHandle(true);
				}
			}
			return message.Result;
		}

		/// <summary>Invokes the default window procedure associated with this window.</summary>
		/// <param name="m">The message that is currently being processed.</param>
		// Token: 0x06003150 RID: 12624 RVA: 0x000DEB60 File Offset: 0x000DCD60
		public void DefWndProc(ref Message m)
		{
			if (this.previousWindow != null)
			{
				m.Result = this.previousWindow.Callback(m.HWnd, m.Msg, m.WParam, m.LParam);
				return;
			}
			if (this.defWindowProc == IntPtr.Zero)
			{
				m.Result = UnsafeNativeMethods.DefWindowProc(m.HWnd, m.Msg, m.WParam, m.LParam);
				return;
			}
			m.Result = UnsafeNativeMethods.CallWindowProc(this.defWindowProc, m.HWnd, m.Msg, m.WParam, m.LParam);
		}

		/// <summary>Destroys the window and its handle.</summary>
		// Token: 0x06003151 RID: 12625 RVA: 0x000DEC00 File Offset: 0x000DCE00
		public virtual void DestroyHandle()
		{
			lock (this)
			{
				if (this.handle != IntPtr.Zero)
				{
					if (!UnsafeNativeMethods.DestroyWindow(new HandleRef(this, this.handle)))
					{
						this.UnSubclass();
						UnsafeNativeMethods.PostMessage(new HandleRef(this, this.handle), 16, 0, 0);
					}
					this.handle = IntPtr.Zero;
					this.ownHandle = false;
				}
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
				try
				{
					GC.SuppressFinalize(this);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				this.suppressedGC = true;
			}
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x000DECB4 File Offset: 0x000DCEB4
		private static void ExpandTable()
		{
			int num = NativeWindow.hashBuckets.Length;
			int prime = NativeWindow.GetPrime(1 + num * 2);
			NativeWindow.HandleBucket[] array = new NativeWindow.HandleBucket[prime];
			for (int i = 0; i < num; i++)
			{
				NativeWindow.HandleBucket handleBucket = NativeWindow.hashBuckets[i];
				if (handleBucket.handle != IntPtr.Zero && handleBucket.handle != new IntPtr(-1))
				{
					uint num2 = (uint)(handleBucket.hash_coll & int.MaxValue);
					uint num3 = 1U + ((num2 >> 5) + 1U) % (uint)(array.Length - 1);
					int num4;
					for (;;)
					{
						num4 = (int)(num2 % (uint)array.Length);
						if (array[num4].handle == IntPtr.Zero || array[num4].handle == new IntPtr(-1))
						{
							break;
						}
						NativeWindow.HandleBucket[] array2 = array;
						int num5 = num4;
						array2[num5].hash_coll = array2[num5].hash_coll | int.MinValue;
						num2 += num3;
					}
					array[num4].window = handleBucket.window;
					array[num4].handle = handleBucket.handle;
					NativeWindow.HandleBucket[] array3 = array;
					int num6 = num4;
					array3[num6].hash_coll = array3[num6].hash_coll | (handleBucket.hash_coll & int.MaxValue);
				}
			}
			NativeWindow.hashBuckets = array;
			NativeWindow.hashLoadSize = (int)(0.72f * (float)prime);
			if (NativeWindow.hashLoadSize >= prime)
			{
				NativeWindow.hashLoadSize = prime - 1;
			}
		}

		/// <summary>Retrieves the window associated with the specified handle.</summary>
		/// <param name="handle">A handle to a window.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.NativeWindow" /> associated with the specified handle. This method returns <see langword="null" /> when the handle does not have an associated window.</returns>
		// Token: 0x06003153 RID: 12627 RVA: 0x000DEE0F File Offset: 0x000DD00F
		public static NativeWindow FromHandle(IntPtr handle)
		{
			if (handle != IntPtr.Zero && NativeWindow.handleCount > 0)
			{
				return NativeWindow.GetWindowFromTable(handle);
			}
			return null;
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x000DEE30 File Offset: 0x000DD030
		private static int GetPrime(int minSize)
		{
			if (minSize < 0)
			{
				throw new OutOfMemoryException();
			}
			for (int i = 0; i < NativeWindow.primes.Length; i++)
			{
				int num = NativeWindow.primes[i];
				if (num >= minSize)
				{
					return num;
				}
			}
			for (int j = (minSize - 2) | 1; j < 2147483647; j += 2)
			{
				bool flag = true;
				if ((j & 1) != 0)
				{
					int num2 = (int)Math.Sqrt((double)j);
					for (int k = 3; k < num2; k += 2)
					{
						if (j % k == 0)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						return j;
					}
				}
				else if (j == 2)
				{
					return j;
				}
			}
			return minSize;
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x000DEEB4 File Offset: 0x000DD0B4
		private static NativeWindow GetWindowFromTable(IntPtr handle)
		{
			NativeWindow.HandleBucket[] array = NativeWindow.hashBuckets;
			int num = 0;
			uint num3;
			uint num4;
			uint num2 = NativeWindow.InitHash(handle, array.Length, out num3, out num4);
			NativeWindow.HandleBucket handleBucket;
			for (;;)
			{
				int num5 = (int)(num3 % (uint)array.Length);
				handleBucket = array[num5];
				if (handleBucket.handle == IntPtr.Zero)
				{
					break;
				}
				if ((long)(handleBucket.hash_coll & 2147483647) == (long)((ulong)num2) && handle == handleBucket.handle && handleBucket.window.IsAllocated)
				{
					goto Block_4;
				}
				num3 += num4;
				if (handleBucket.hash_coll >= 0 || ++num >= array.Length)
				{
					goto IL_97;
				}
			}
			return null;
			Block_4:
			return (NativeWindow)handleBucket.window.Target;
			IL_97:
			return null;
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x000DEF5C File Offset: 0x000DD15C
		internal IntPtr GetHandleFromID(short id)
		{
			IntPtr zero;
			if (NativeWindow.hashForIdHandle == null || !NativeWindow.hashForIdHandle.TryGetValue(id, out zero))
			{
				zero = IntPtr.Zero;
			}
			return zero;
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x000DEF88 File Offset: 0x000DD188
		private static uint InitHash(IntPtr handle, int hashsize, out uint seed, out uint incr)
		{
			uint num = (uint)(handle.GetHashCode() & int.MaxValue);
			seed = num;
			incr = 1U + ((seed >> 5) + 1U) % (uint)(hashsize - 1);
			return num;
		}

		/// <summary>Specifies a notification method that is called when the handle for a window is changed.</summary>
		// Token: 0x06003158 RID: 12632 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void OnHandleChange()
		{
		}

		// Token: 0x06003159 RID: 12633 RVA: 0x000DEFB8 File Offset: 0x000DD1B8
		[PrePrepareMethod]
		private static void OnShutdown(object sender, EventArgs e)
		{
			if (NativeWindow.handleCount > 0)
			{
				object obj = NativeWindow.internalSyncObject;
				lock (obj)
				{
					for (int i = 0; i < NativeWindow.hashBuckets.Length; i++)
					{
						NativeWindow.HandleBucket handleBucket = NativeWindow.hashBuckets[i];
						if (handleBucket.handle != IntPtr.Zero && handleBucket.handle != new IntPtr(-1))
						{
							HandleRef handleRef = new HandleRef(handleBucket, handleBucket.handle);
							UnsafeNativeMethods.SetWindowLong(handleRef, -4, new HandleRef(null, NativeWindow.userDefWindowProc));
							UnsafeNativeMethods.SetClassLong(handleRef, -24, NativeWindow.userDefWindowProc);
							UnsafeNativeMethods.PostMessage(handleRef, 16, 0, 0);
							if (handleBucket.window.IsAllocated)
							{
								NativeWindow nativeWindow = (NativeWindow)handleBucket.window.Target;
								if (nativeWindow != null)
								{
									nativeWindow.handle = IntPtr.Zero;
								}
							}
							handleBucket.window.Free();
						}
						NativeWindow.hashBuckets[i].handle = IntPtr.Zero;
						NativeWindow.hashBuckets[i].hash_coll = 0;
					}
					NativeWindow.handleCount = 0;
				}
			}
			NativeWindow.WindowClass.DisposeCache();
		}

		/// <summary>When overridden in a derived class, manages an unhandled thread exception.</summary>
		/// <param name="e">An <see cref="T:System.Exception" /> that specifies the unhandled thread exception.</param>
		// Token: 0x0600315A RID: 12634 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void OnThreadException(Exception e)
		{
		}

		/// <summary>Releases the handle associated with this window.</summary>
		// Token: 0x0600315B RID: 12635 RVA: 0x000DF108 File Offset: 0x000DD308
		public virtual void ReleaseHandle()
		{
			this.ReleaseHandle(true);
		}

		// Token: 0x0600315C RID: 12636 RVA: 0x000DF114 File Offset: 0x000DD314
		private void ReleaseHandle(bool handleValid)
		{
			if (this.handle != IntPtr.Zero)
			{
				lock (this)
				{
					if (this.handle != IntPtr.Zero)
					{
						if (handleValid)
						{
							this.UnSubclass();
						}
						NativeWindow.RemoveWindowFromTable(this.handle, this);
						if (this.ownHandle)
						{
							System.Internal.HandleCollector.Remove(this.handle, NativeMethods.CommonHandles.Window);
							this.ownHandle = false;
						}
						this.handle = IntPtr.Zero;
						if (this.weakThisPtr.IsAlive && this.weakThisPtr.Target != null)
						{
							this.OnHandleChange();
							new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
							try
							{
								GC.SuppressFinalize(this);
							}
							finally
							{
								CodeAccessPermission.RevertAssert();
							}
							this.suppressedGC = true;
						}
					}
				}
			}
		}

		// Token: 0x0600315D RID: 12637 RVA: 0x000DF200 File Offset: 0x000DD400
		private static void RemoveWindowFromTable(IntPtr handle, NativeWindow window)
		{
			object obj = NativeWindow.internalSyncObject;
			lock (obj)
			{
				uint num2;
				uint num3;
				uint num = NativeWindow.InitHash(handle, NativeWindow.hashBuckets.Length, out num2, out num3);
				int num4 = 0;
				NativeWindow nativeWindow = window.PreviousWindow;
				int num5;
				for (;;)
				{
					num5 = (int)(num2 % (uint)NativeWindow.hashBuckets.Length);
					NativeWindow.HandleBucket handleBucket = NativeWindow.hashBuckets[num5];
					if ((long)(handleBucket.hash_coll & 2147483647) == (long)((ulong)num) && handle == handleBucket.handle)
					{
						break;
					}
					num2 += num3;
					if (NativeWindow.hashBuckets[num5].hash_coll >= 0 || ++num4 >= NativeWindow.hashBuckets.Length)
					{
						goto IL_1ED;
					}
				}
				bool flag2 = window.nextWindow == null;
				bool flag3 = NativeWindow.IsRootWindowInListWithChildren(window);
				if (window.previousWindow != null)
				{
					window.previousWindow.nextWindow = window.nextWindow;
				}
				if (window.nextWindow != null)
				{
					window.nextWindow.defWindowProc = window.defWindowProc;
					window.nextWindow.previousWindow = window.previousWindow;
				}
				window.nextWindow = null;
				window.previousWindow = null;
				if (flag3)
				{
					if (NativeWindow.hashBuckets[num5].window.IsAllocated)
					{
						NativeWindow.hashBuckets[num5].window.Free();
					}
					NativeWindow.hashBuckets[num5].window = GCHandle.Alloc(nativeWindow, GCHandleType.Weak);
				}
				else if (flag2)
				{
					NativeWindow.HandleBucket[] array = NativeWindow.hashBuckets;
					int num6 = num5;
					array[num6].hash_coll = array[num6].hash_coll & int.MinValue;
					if (NativeWindow.hashBuckets[num5].hash_coll != 0)
					{
						NativeWindow.hashBuckets[num5].handle = new IntPtr(-1);
					}
					else
					{
						NativeWindow.hashBuckets[num5].handle = IntPtr.Zero;
					}
					if (NativeWindow.hashBuckets[num5].window.IsAllocated)
					{
						NativeWindow.hashBuckets[num5].window.Free();
					}
					NativeWindow.handleCount--;
				}
				IL_1ED:;
			}
		}

		// Token: 0x0600315E RID: 12638 RVA: 0x000DF424 File Offset: 0x000DD624
		private static bool IsRootWindowInListWithChildren(NativeWindow window)
		{
			return window.PreviousWindow != null && window.nextWindow == null;
		}

		// Token: 0x0600315F RID: 12639 RVA: 0x000DF43C File Offset: 0x000DD63C
		internal static void RemoveWindowFromIDTable(IntPtr handle)
		{
			short num = NativeWindow.hashForHandleId[handle];
			NativeWindow.hashForHandleId.Remove(handle);
			NativeWindow.hashForIdHandle.Remove(num);
		}

		// Token: 0x06003160 RID: 12640 RVA: 0x000DF470 File Offset: 0x000DD670
		internal static void SetUnhandledExceptionModeInternal(UnhandledExceptionMode mode, bool threadScope)
		{
			if (!threadScope && NativeWindow.anyHandleCreatedInApp)
			{
				throw new InvalidOperationException(SR.GetString("ApplicationCannotChangeApplicationExceptionMode"));
			}
			if (threadScope && NativeWindow.anyHandleCreated)
			{
				throw new InvalidOperationException(SR.GetString("ApplicationCannotChangeThreadExceptionMode"));
			}
			switch (mode)
			{
			case UnhandledExceptionMode.Automatic:
				if (threadScope)
				{
					NativeWindow.userSetProcFlags = 0;
					return;
				}
				NativeWindow.userSetProcFlagsForApp = 0;
				return;
			case UnhandledExceptionMode.ThrowException:
				if (threadScope)
				{
					NativeWindow.userSetProcFlags = 5;
					return;
				}
				NativeWindow.userSetProcFlagsForApp = 5;
				return;
			case UnhandledExceptionMode.CatchException:
				if (threadScope)
				{
					NativeWindow.userSetProcFlags = 1;
					return;
				}
				NativeWindow.userSetProcFlagsForApp = 1;
				return;
			default:
				throw new InvalidEnumArgumentException("mode", (int)mode, typeof(UnhandledExceptionMode));
			}
		}

		// Token: 0x06003161 RID: 12641 RVA: 0x000DF510 File Offset: 0x000DD710
		private void UnSubclass()
		{
			bool flag = !this.weakThisPtr.IsAlive || this.weakThisPtr.Target == null;
			HandleRef handleRef = new HandleRef(this, this.handle);
			IntPtr windowLong = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, this.handle), -4);
			if (!(this.windowProcPtr == windowLong))
			{
				if (this.nextWindow == null || this.nextWindow.defWindowProc != this.windowProcPtr)
				{
					UnsafeNativeMethods.SetWindowLong(handleRef, -4, new HandleRef(this, NativeWindow.userDefWindowProc));
				}
				return;
			}
			if (this.previousWindow == null)
			{
				UnsafeNativeMethods.SetWindowLong(handleRef, -4, new HandleRef(this, this.defWindowProc));
				return;
			}
			if (flag)
			{
				UnsafeNativeMethods.SetWindowLong(handleRef, -4, new HandleRef(this, NativeWindow.userDefWindowProc));
				return;
			}
			UnsafeNativeMethods.SetWindowLong(handleRef, -4, this.previousWindow.windowProc);
		}

		/// <summary>Invokes the default window procedure associated with this window.</summary>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" /> that is associated with the current Windows message.</param>
		// Token: 0x06003162 RID: 12642 RVA: 0x000DF5EB File Offset: 0x000DD7EB
		protected virtual void WndProc(ref Message m)
		{
			this.DefWndProc(ref m);
		}

		// Token: 0x04001DFA RID: 7674
		private static readonly TraceSwitch WndProcChoice;

		// Token: 0x04001DFB RID: 7675
		private static readonly int[] primes = new int[]
		{
			11, 17, 23, 29, 37, 47, 59, 71, 89, 107,
			131, 163, 197, 239, 293, 353, 431, 521, 631, 761,
			919, 1103, 1327, 1597, 1931, 2333, 2801, 3371, 4049, 4861,
			5839, 7013, 8419, 10103, 12143, 14591, 17519, 21023, 25229, 30293,
			36353, 43627, 52361, 62851, 75431, 90523, 108631, 130363, 156437, 187751,
			225307, 270371, 324449, 389357, 467237, 560689, 672827, 807403, 968897, 1162687,
			1395263, 1674319, 2009191, 2411033, 2893249, 3471899, 4166287, 4999559, 5999471, 7199369
		};

		// Token: 0x04001DFC RID: 7676
		private const int InitializedFlags = 1;

		// Token: 0x04001DFD RID: 7677
		private const int DebuggerPresent = 2;

		// Token: 0x04001DFE RID: 7678
		private const int UseDebuggableWndProc = 4;

		// Token: 0x04001DFF RID: 7679
		private const int LoadConfigSettings = 8;

		// Token: 0x04001E00 RID: 7680
		private const int AssemblyIsDebuggable = 16;

		// Token: 0x04001E01 RID: 7681
		[ThreadStatic]
		private static bool anyHandleCreated;

		// Token: 0x04001E02 RID: 7682
		private static bool anyHandleCreatedInApp;

		// Token: 0x04001E03 RID: 7683
		private const float hashLoadFactor = 0.72f;

		// Token: 0x04001E04 RID: 7684
		private static int handleCount;

		// Token: 0x04001E05 RID: 7685
		private static int hashLoadSize;

		// Token: 0x04001E06 RID: 7686
		private static NativeWindow.HandleBucket[] hashBuckets;

		// Token: 0x04001E07 RID: 7687
		private static IntPtr userDefWindowProc;

		// Token: 0x04001E08 RID: 7688
		[ThreadStatic]
		private static byte wndProcFlags = 0;

		// Token: 0x04001E09 RID: 7689
		[ThreadStatic]
		private static byte userSetProcFlags = 0;

		// Token: 0x04001E0A RID: 7690
		private static byte userSetProcFlagsForApp;

		// Token: 0x04001E0B RID: 7691
		private static short globalID = 1;

		// Token: 0x04001E0C RID: 7692
		private static Dictionary<short, IntPtr> hashForIdHandle;

		// Token: 0x04001E0D RID: 7693
		private static Dictionary<IntPtr, short> hashForHandleId;

		// Token: 0x04001E0E RID: 7694
		private static object internalSyncObject = new object();

		// Token: 0x04001E0F RID: 7695
		private static object createWindowSyncObject = new object();

		// Token: 0x04001E10 RID: 7696
		private IntPtr handle;

		// Token: 0x04001E11 RID: 7697
		private NativeMethods.WndProc windowProc;

		// Token: 0x04001E12 RID: 7698
		private IntPtr windowProcPtr;

		// Token: 0x04001E13 RID: 7699
		private IntPtr defWindowProc;

		// Token: 0x04001E14 RID: 7700
		private bool suppressedGC;

		// Token: 0x04001E15 RID: 7701
		private bool ownHandle;

		// Token: 0x04001E16 RID: 7702
		private NativeWindow previousWindow;

		// Token: 0x04001E17 RID: 7703
		private NativeWindow nextWindow;

		// Token: 0x04001E18 RID: 7704
		private WeakReference weakThisPtr;

		// Token: 0x04001E19 RID: 7705
		private DpiAwarenessContext windowDpiAwarenessContext = (DpiHelper.EnableDpiChangedHighDpiImprovements ? CommonUnsafeNativeMethods.TryGetThreadDpiAwarenessContext() : DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED);

		// Token: 0x020007C5 RID: 1989
		private struct HandleBucket
		{
			// Token: 0x040041A9 RID: 16809
			public IntPtr handle;

			// Token: 0x040041AA RID: 16810
			public GCHandle window;

			// Token: 0x040041AB RID: 16811
			public int hash_coll;
		}

		// Token: 0x020007C6 RID: 1990
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		private class WindowClass
		{
			// Token: 0x06006D42 RID: 27970 RVA: 0x00190DE7 File Offset: 0x0018EFE7
			internal WindowClass(string className, int classStyle)
			{
				this.className = className;
				this.classStyle = classStyle;
				this.RegisterClass();
			}

			// Token: 0x06006D43 RID: 27971 RVA: 0x00190E03 File Offset: 0x0018F003
			public IntPtr Callback(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
			{
				UnsafeNativeMethods.SetWindowLong(new HandleRef(null, hWnd), -4, new HandleRef(this, this.defWindowProc));
				this.targetWindow.AssignHandle(hWnd);
				return this.targetWindow.Callback(hWnd, msg, wparam, lparam);
			}

			// Token: 0x06006D44 RID: 27972 RVA: 0x00190E3C File Offset: 0x0018F03C
			internal static NativeWindow.WindowClass Create(string className, int classStyle)
			{
				object obj = NativeWindow.WindowClass.wcInternalSyncObject;
				NativeWindow.WindowClass windowClass2;
				lock (obj)
				{
					NativeWindow.WindowClass windowClass = NativeWindow.WindowClass.cache;
					if (className == null)
					{
						while (windowClass != null)
						{
							if (windowClass.className == null && windowClass.classStyle == classStyle)
							{
								break;
							}
							windowClass = windowClass.next;
						}
					}
					else
					{
						while (windowClass != null && !className.Equals(windowClass.className))
						{
							windowClass = windowClass.next;
						}
					}
					if (windowClass == null)
					{
						windowClass = new NativeWindow.WindowClass(className, classStyle);
						windowClass.next = NativeWindow.WindowClass.cache;
						NativeWindow.WindowClass.cache = windowClass;
					}
					else if (!windowClass.registered)
					{
						windowClass.RegisterClass();
					}
					windowClass2 = windowClass;
				}
				return windowClass2;
			}

			// Token: 0x06006D45 RID: 27973 RVA: 0x00190EE4 File Offset: 0x0018F0E4
			internal static void DisposeCache()
			{
				object obj = NativeWindow.WindowClass.wcInternalSyncObject;
				lock (obj)
				{
					for (NativeWindow.WindowClass windowClass = NativeWindow.WindowClass.cache; windowClass != null; windowClass = windowClass.next)
					{
						windowClass.UnregisterClass();
					}
				}
			}

			// Token: 0x06006D46 RID: 27974 RVA: 0x00190F38 File Offset: 0x0018F138
			private string GetFullClassName(string className)
			{
				StringBuilder stringBuilder = new StringBuilder(50);
				stringBuilder.Append(Application.WindowsFormsVersion);
				stringBuilder.Append('.');
				stringBuilder.Append(className);
				stringBuilder.Append(".app.");
				stringBuilder.Append(NativeWindow.WindowClass.domainQualifier);
				stringBuilder.Append('.');
				string text = Convert.ToString(AppDomain.CurrentDomain.GetHashCode(), 16);
				stringBuilder.Append(VersioningHelper.MakeVersionSafeName(text, ResourceScope.Process, ResourceScope.AppDomain));
				return stringBuilder.ToString();
			}

			// Token: 0x06006D47 RID: 27975 RVA: 0x00190FB4 File Offset: 0x0018F1B4
			private void RegisterClass()
			{
				NativeMethods.WNDCLASS_D wndclass_D = new NativeMethods.WNDCLASS_D();
				if (NativeWindow.userDefWindowProc == IntPtr.Zero)
				{
					string text = ((Marshal.SystemDefaultCharSize == 1) ? "DefWindowProcA" : "DefWindowProcW");
					NativeWindow.userDefWindowProc = UnsafeNativeMethods.GetProcAddress(new HandleRef(null, UnsafeNativeMethods.GetModuleHandle("user32.dll")), text);
					if (NativeWindow.userDefWindowProc == IntPtr.Zero)
					{
						throw new Win32Exception();
					}
				}
				string text2;
				if (this.className == null)
				{
					wndclass_D.hbrBackground = UnsafeNativeMethods.GetStockObject(5);
					wndclass_D.style = this.classStyle;
					this.defWindowProc = NativeWindow.userDefWindowProc;
					text2 = "Window." + Convert.ToString(this.classStyle, 16);
					this.hashCode = 0;
				}
				else
				{
					NativeMethods.WNDCLASS_I wndclass_I = new NativeMethods.WNDCLASS_I();
					bool classInfo = UnsafeNativeMethods.GetClassInfo(NativeMethods.NullHandleRef, this.className, wndclass_I);
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (!classInfo)
					{
						throw new Win32Exception(lastWin32Error, SR.GetString("InvalidWndClsName"));
					}
					wndclass_D.style = wndclass_I.style;
					wndclass_D.cbClsExtra = wndclass_I.cbClsExtra;
					wndclass_D.cbWndExtra = wndclass_I.cbWndExtra;
					wndclass_D.hIcon = wndclass_I.hIcon;
					wndclass_D.hCursor = wndclass_I.hCursor;
					wndclass_D.hbrBackground = wndclass_I.hbrBackground;
					wndclass_D.lpszMenuName = Marshal.PtrToStringAuto(wndclass_I.lpszMenuName);
					text2 = this.className;
					this.defWindowProc = wndclass_I.lpfnWndProc;
					this.hashCode = this.className.GetHashCode();
				}
				this.windowClassName = this.GetFullClassName(text2);
				this.windowProc = new NativeMethods.WndProc(this.Callback);
				wndclass_D.lpfnWndProc = this.windowProc;
				wndclass_D.hInstance = UnsafeNativeMethods.GetModuleHandle(null);
				wndclass_D.lpszClassName = this.windowClassName;
				short num = UnsafeNativeMethods.RegisterClass(wndclass_D);
				if (num == 0)
				{
					int lastWin32Error2 = Marshal.GetLastWin32Error();
					if (lastWin32Error2 == 1410)
					{
						NativeMethods.WNDCLASS_I wndclass_I2 = new NativeMethods.WNDCLASS_I();
						bool classInfo2 = UnsafeNativeMethods.GetClassInfo(new HandleRef(null, UnsafeNativeMethods.GetModuleHandle(null)), this.windowClassName, wndclass_I2);
						if (classInfo2 && wndclass_I2.lpfnWndProc == NativeWindow.UserDefindowProc)
						{
							if (UnsafeNativeMethods.UnregisterClass(this.windowClassName, new HandleRef(null, UnsafeNativeMethods.GetModuleHandle(null))))
							{
								num = UnsafeNativeMethods.RegisterClass(wndclass_D);
							}
							else
							{
								do
								{
									NativeWindow.WindowClass.domainQualifier++;
									this.windowClassName = this.GetFullClassName(text2);
									wndclass_D.lpszClassName = this.windowClassName;
									num = UnsafeNativeMethods.RegisterClass(wndclass_D);
								}
								while (num == 0 && Marshal.GetLastWin32Error() == 1410);
							}
						}
					}
					if (num == 0)
					{
						this.windowProc = null;
						throw new Win32Exception(lastWin32Error2);
					}
				}
				this.registered = true;
			}

			// Token: 0x06006D48 RID: 27976 RVA: 0x0019123F File Offset: 0x0018F43F
			private void UnregisterClass()
			{
				if (this.registered && UnsafeNativeMethods.UnregisterClass(this.windowClassName, new HandleRef(null, UnsafeNativeMethods.GetModuleHandle(null))))
				{
					this.windowProc = null;
					this.registered = false;
				}
			}

			// Token: 0x040041AC RID: 16812
			internal static NativeWindow.WindowClass cache;

			// Token: 0x040041AD RID: 16813
			internal NativeWindow.WindowClass next;

			// Token: 0x040041AE RID: 16814
			internal string className;

			// Token: 0x040041AF RID: 16815
			internal int classStyle;

			// Token: 0x040041B0 RID: 16816
			internal string windowClassName;

			// Token: 0x040041B1 RID: 16817
			internal int hashCode;

			// Token: 0x040041B2 RID: 16818
			internal IntPtr defWindowProc;

			// Token: 0x040041B3 RID: 16819
			internal NativeMethods.WndProc windowProc;

			// Token: 0x040041B4 RID: 16820
			internal bool registered;

			// Token: 0x040041B5 RID: 16821
			internal NativeWindow targetWindow;

			// Token: 0x040041B6 RID: 16822
			private static object wcInternalSyncObject = new object();

			// Token: 0x040041B7 RID: 16823
			private static int domainQualifier = 0;
		}
	}
}
