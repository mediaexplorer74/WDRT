using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>Provides helper methods for marshaling data between the .NET Framework and the Windows Runtime.</summary>
	// Token: 0x020009F4 RID: 2548
	public static class WindowsRuntimeMarshal
	{
		/// <summary>Adds the specified event handler to a Windows Runtime event.</summary>
		/// <param name="addMethod">A delegate that represents the method that adds event handlers to the Windows Runtime event.</param>
		/// <param name="removeMethod">A delegate that represents the method that removes event handlers from the Windows Runtime event.</param>
		/// <param name="handler">A delegate the represents the event handler that is added.</param>
		/// <typeparam name="T">The type of the delegate that represents the event handler.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="addMethod" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="removeMethod" /> is <see langword="null" />.</exception>
		// Token: 0x060064E0 RID: 25824 RVA: 0x00158AA4 File Offset: 0x00156CA4
		[SecurityCritical]
		public static void AddEventHandler<T>(Func<T, EventRegistrationToken> addMethod, Action<EventRegistrationToken> removeMethod, T handler)
		{
			if (addMethod == null)
			{
				throw new ArgumentNullException("addMethod");
			}
			if (removeMethod == null)
			{
				throw new ArgumentNullException("removeMethod");
			}
			if (handler == null)
			{
				return;
			}
			object target = removeMethod.Target;
			if (target == null || Marshal.IsComObject(target))
			{
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.AddEventHandler<T>(addMethod, removeMethod, handler);
				return;
			}
			WindowsRuntimeMarshal.ManagedEventRegistrationImpl.AddEventHandler<T>(addMethod, removeMethod, handler);
		}

		/// <summary>Removes the specified event handler from a Windows Runtime event.</summary>
		/// <param name="removeMethod">A delegate that represents the method that removes event handlers from the Windows Runtime event.</param>
		/// <param name="handler">The event handler that is removed.</param>
		/// <typeparam name="T">The type of the delegate that represents the event handler.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="removeMethod" /> is <see langword="null" />.</exception>
		// Token: 0x060064E1 RID: 25825 RVA: 0x00158AFC File Offset: 0x00156CFC
		[SecurityCritical]
		public static void RemoveEventHandler<T>(Action<EventRegistrationToken> removeMethod, T handler)
		{
			if (removeMethod == null)
			{
				throw new ArgumentNullException("removeMethod");
			}
			if (handler == null)
			{
				return;
			}
			object target = removeMethod.Target;
			if (target == null || Marshal.IsComObject(target))
			{
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.RemoveEventHandler<T>(removeMethod, handler);
				return;
			}
			WindowsRuntimeMarshal.ManagedEventRegistrationImpl.RemoveEventHandler<T>(removeMethod, handler);
		}

		/// <summary>Removes all the event handlers that can be removed by using the specified method.</summary>
		/// <param name="removeMethod">A delegate that represents the method that removes event handlers from the Windows Runtime event.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="removeMethod" /> is <see langword="null" />.</exception>
		// Token: 0x060064E2 RID: 25826 RVA: 0x00158B44 File Offset: 0x00156D44
		[SecurityCritical]
		public static void RemoveAllEventHandlers(Action<EventRegistrationToken> removeMethod)
		{
			if (removeMethod == null)
			{
				throw new ArgumentNullException("removeMethod");
			}
			object target = removeMethod.Target;
			if (target == null || Marshal.IsComObject(target))
			{
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.RemoveAllEventHandlers(removeMethod);
				return;
			}
			WindowsRuntimeMarshal.ManagedEventRegistrationImpl.RemoveAllEventHandlers(removeMethod);
		}

		// Token: 0x060064E3 RID: 25827 RVA: 0x00158B80 File Offset: 0x00156D80
		internal static int GetRegistrationTokenCacheSize()
		{
			int num = 0;
			if (WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations != null)
			{
				ConditionalWeakTable<object, Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>> s_eventRegistrations = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations;
				lock (s_eventRegistrations)
				{
					num += WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations.Keys.Count;
				}
			}
			if (WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations != null)
			{
				Dictionary<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry> s_eventRegistrations2 = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations;
				lock (s_eventRegistrations2)
				{
					num += WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations.Count;
				}
			}
			return num;
		}

		// Token: 0x060064E4 RID: 25828 RVA: 0x00158C20 File Offset: 0x00156E20
		internal static void CallRemoveMethods(Action<EventRegistrationToken> removeMethod, List<EventRegistrationToken> tokensToRemove)
		{
			List<Exception> list = new List<Exception>();
			foreach (EventRegistrationToken eventRegistrationToken in tokensToRemove)
			{
				try
				{
					removeMethod(eventRegistrationToken);
				}
				catch (Exception ex)
				{
					list.Add(ex);
				}
			}
			if (list.Count > 0)
			{
				throw new AggregateException(list.ToArray());
			}
		}

		// Token: 0x060064E5 RID: 25829 RVA: 0x00158CA4 File Offset: 0x00156EA4
		[SecurityCritical]
		internal unsafe static string HStringToString(IntPtr hstring)
		{
			if (hstring == IntPtr.Zero)
			{
				return string.Empty;
			}
			uint num;
			char* ptr = UnsafeNativeMethods.WindowsGetStringRawBuffer(hstring, &num);
			return new string(ptr, 0, checked((int)num));
		}

		// Token: 0x060064E6 RID: 25830 RVA: 0x00158CD8 File Offset: 0x00156ED8
		internal static Exception GetExceptionForHR(int hresult, Exception innerException, string messageResource)
		{
			Exception ex;
			if (innerException != null)
			{
				string text = innerException.Message;
				if (text == null && messageResource != null)
				{
					text = Environment.GetResourceString(messageResource);
				}
				ex = new Exception(text, innerException);
			}
			else
			{
				string text2 = ((messageResource != null) ? Environment.GetResourceString(messageResource) : null);
				ex = new Exception(text2);
			}
			ex.SetErrorCode(hresult);
			return ex;
		}

		// Token: 0x060064E7 RID: 25831 RVA: 0x00158D24 File Offset: 0x00156F24
		internal static Exception GetExceptionForHR(int hresult, Exception innerException)
		{
			return WindowsRuntimeMarshal.GetExceptionForHR(hresult, innerException, null);
		}

		// Token: 0x060064E8 RID: 25832 RVA: 0x00158D30 File Offset: 0x00156F30
		[SecurityCritical]
		private static bool RoOriginateLanguageException(int error, string message, IntPtr languageException)
		{
			if (WindowsRuntimeMarshal.s_haveBlueErrorApis)
			{
				try
				{
					return UnsafeNativeMethods.RoOriginateLanguageException(error, message, languageException);
				}
				catch (EntryPointNotFoundException)
				{
					WindowsRuntimeMarshal.s_haveBlueErrorApis = false;
				}
				return false;
			}
			return false;
		}

		// Token: 0x060064E9 RID: 25833 RVA: 0x00158D6C File Offset: 0x00156F6C
		[SecurityCritical]
		private static void RoReportUnhandledError(IRestrictedErrorInfo error)
		{
			if (WindowsRuntimeMarshal.s_haveBlueErrorApis)
			{
				try
				{
					UnsafeNativeMethods.RoReportUnhandledError(error);
				}
				catch (EntryPointNotFoundException)
				{
					WindowsRuntimeMarshal.s_haveBlueErrorApis = false;
				}
			}
		}

		// Token: 0x060064EA RID: 25834 RVA: 0x00158DA4 File Offset: 0x00156FA4
		[FriendAccessAllowed]
		[SecuritySafeCritical]
		internal static bool ReportUnhandledError(Exception e)
		{
			if (!AppDomain.IsAppXModel())
			{
				return false;
			}
			if (!WindowsRuntimeMarshal.s_haveBlueErrorApis)
			{
				return false;
			}
			if (e != null)
			{
				IntPtr intPtr = IntPtr.Zero;
				IntPtr zero = IntPtr.Zero;
				try
				{
					intPtr = Marshal.GetIUnknownForObject(e);
					if (intPtr != IntPtr.Zero)
					{
						Marshal.QueryInterface(intPtr, ref WindowsRuntimeMarshal.s_iidIErrorInfo, out zero);
						if (zero != IntPtr.Zero && WindowsRuntimeMarshal.RoOriginateLanguageException(Marshal.GetHRForException_WinRT(e), e.Message, zero))
						{
							IRestrictedErrorInfo restrictedErrorInfo = UnsafeNativeMethods.GetRestrictedErrorInfo();
							if (restrictedErrorInfo != null)
							{
								WindowsRuntimeMarshal.RoReportUnhandledError(restrictedErrorInfo);
								return true;
							}
						}
					}
				}
				finally
				{
					if (zero != IntPtr.Zero)
					{
						Marshal.Release(zero);
					}
					if (intPtr != IntPtr.Zero)
					{
						Marshal.Release(intPtr);
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060064EB RID: 25835 RVA: 0x00158E6C File Offset: 0x0015706C
		[SecurityCritical]
		internal static IntPtr GetActivationFactoryForType(Type type)
		{
			ManagedActivationFactory managedActivationFactory = WindowsRuntimeMarshal.GetManagedActivationFactory(type);
			return Marshal.GetComInterfaceForObject(managedActivationFactory, typeof(IActivationFactory));
		}

		// Token: 0x060064EC RID: 25836 RVA: 0x00158E90 File Offset: 0x00157090
		[SecurityCritical]
		internal static ManagedActivationFactory GetManagedActivationFactory(Type type)
		{
			ManagedActivationFactory managedActivationFactory = new ManagedActivationFactory(type);
			Marshal.InitializeManagedWinRTFactoryObject(managedActivationFactory, (RuntimeType)type);
			return managedActivationFactory;
		}

		// Token: 0x060064ED RID: 25837 RVA: 0x00158EB4 File Offset: 0x001570B4
		[SecurityCritical]
		internal static IntPtr GetClassActivatorForApplication(string appBase)
		{
			if (WindowsRuntimeMarshal.s_pClassActivator == IntPtr.Zero)
			{
				AppDomainSetup appDomainSetup = new AppDomainSetup
				{
					ApplicationBase = appBase
				};
				AppDomain appDomain = AppDomain.CreateDomain(Environment.GetResourceString("WinRTHostDomainName", new object[] { appBase }), null, appDomainSetup);
				WinRTClassActivator winRTClassActivator = (WinRTClassActivator)appDomain.CreateInstanceAndUnwrap(typeof(WinRTClassActivator).Assembly.FullName, typeof(WinRTClassActivator).FullName);
				IntPtr iwinRTClassActivator = winRTClassActivator.GetIWinRTClassActivator();
				if (Interlocked.CompareExchange(ref WindowsRuntimeMarshal.s_pClassActivator, iwinRTClassActivator, IntPtr.Zero) != IntPtr.Zero)
				{
					Marshal.Release(iwinRTClassActivator);
					try
					{
						AppDomain.Unload(appDomain);
					}
					catch (CannotUnloadAppDomainException)
					{
					}
				}
			}
			Marshal.AddRef(WindowsRuntimeMarshal.s_pClassActivator);
			return WindowsRuntimeMarshal.s_pClassActivator;
		}

		/// <summary>Returns an object that implements the activation factory interface for the specified Windows Runtime type.</summary>
		/// <param name="type">The Windows Runtime type to get the activation factory interface for.</param>
		/// <returns>An object that implements the activation factory interface.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> does not represent a Windows Runtime type (that is, belonging to the Windows Runtime itself or defined in a Windows Runtime component).  
		/// -or-  
		/// The object specified for <paramref name="type" /> was not provided by the common language runtime type system.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.TypeLoadException">The specified Windows Runtime class is not properly registered. For example, the .winmd file was located, but the Windows Runtime failed to locate the implementation.</exception>
		// Token: 0x060064EE RID: 25838 RVA: 0x00158F84 File Offset: 0x00157184
		[SecurityCritical]
		public static IActivationFactory GetActivationFactory(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type.IsWindowsRuntimeObject && type.IsImport)
			{
				return (IActivationFactory)Marshal.GetNativeActivationFactory(type);
			}
			return WindowsRuntimeMarshal.GetManagedActivationFactory(type);
		}

		/// <summary>Allocates a Windows RuntimeHSTRING and copies the specified managed string to it.</summary>
		/// <param name="s">The managed string to copy.</param>
		/// <returns>An unmanaged pointer to the new HSTRING, or <see cref="F:System.IntPtr.Zero" /> if <paramref name="s" /> is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The Windows Runtime is not supported on the current version of the operating system.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		// Token: 0x060064EF RID: 25839 RVA: 0x00158FBC File Offset: 0x001571BC
		[SecurityCritical]
		public unsafe static IntPtr StringToHString(string s)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
			}
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			IntPtr intPtr;
			int num = UnsafeNativeMethods.WindowsCreateString(s, s.Length, &intPtr);
			Marshal.ThrowExceptionForHR(num, new IntPtr(-1));
			return intPtr;
		}

		/// <summary>Returns a managed string that contains a copy of the specified Windows RuntimeHSTRING.</summary>
		/// <param name="ptr">An unmanaged pointer to the HSTRING to copy.</param>
		/// <returns>A managed string that contains a copy of the HSTRING if <paramref name="ptr" /> is not <see cref="F:System.IntPtr.Zero" />; otherwise, <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The Windows Runtime is not supported on the current version of the operating system.</exception>
		// Token: 0x060064F0 RID: 25840 RVA: 0x0015900B File Offset: 0x0015720B
		[SecurityCritical]
		public static string PtrToStringHString(IntPtr ptr)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
			}
			return WindowsRuntimeMarshal.HStringToString(ptr);
		}

		/// <summary>Frees the specified Windows RuntimeHSTRING.</summary>
		/// <param name="ptr">The address of the HSTRING to free.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">The Windows Runtime is not supported on the current version of the operating system.</exception>
		// Token: 0x060064F1 RID: 25841 RVA: 0x0015902A File Offset: 0x0015722A
		[SecurityCritical]
		public static void FreeHString(IntPtr ptr)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
			}
			if (ptr != IntPtr.Zero)
			{
				UnsafeNativeMethods.WindowsDeleteString(ptr);
			}
		}

		// Token: 0x04002D2F RID: 11567
		private static bool s_haveBlueErrorApis = true;

		// Token: 0x04002D30 RID: 11568
		private static Guid s_iidIErrorInfo = new Guid(485667104, 21629, 4123, 142, 101, 8, 0, 43, 43, 209, 25);

		// Token: 0x04002D31 RID: 11569
		private static IntPtr s_pClassActivator = IntPtr.Zero;

		// Token: 0x02000C9F RID: 3231
		internal struct EventRegistrationTokenList
		{
			// Token: 0x06007151 RID: 29009 RVA: 0x0018701E File Offset: 0x0018521E
			internal EventRegistrationTokenList(EventRegistrationToken token)
			{
				this.firstToken = token;
				this.restTokens = null;
			}

			// Token: 0x06007152 RID: 29010 RVA: 0x0018702E File Offset: 0x0018522E
			internal EventRegistrationTokenList(WindowsRuntimeMarshal.EventRegistrationTokenList list)
			{
				this.firstToken = list.firstToken;
				this.restTokens = list.restTokens;
			}

			// Token: 0x06007153 RID: 29011 RVA: 0x00187048 File Offset: 0x00185248
			public bool Push(EventRegistrationToken token)
			{
				bool flag = false;
				if (this.restTokens == null)
				{
					this.restTokens = new List<EventRegistrationToken>();
					flag = true;
				}
				this.restTokens.Add(token);
				return flag;
			}

			// Token: 0x06007154 RID: 29012 RVA: 0x0018707C File Offset: 0x0018527C
			public bool Pop(out EventRegistrationToken token)
			{
				if (this.restTokens == null || this.restTokens.Count == 0)
				{
					token = this.firstToken;
					return false;
				}
				int num = this.restTokens.Count - 1;
				token = this.restTokens[num];
				this.restTokens.RemoveAt(num);
				return true;
			}

			// Token: 0x06007155 RID: 29013 RVA: 0x001870D9 File Offset: 0x001852D9
			public void CopyTo(List<EventRegistrationToken> tokens)
			{
				tokens.Add(this.firstToken);
				if (this.restTokens != null)
				{
					tokens.AddRange(this.restTokens);
				}
			}

			// Token: 0x04003881 RID: 14465
			private EventRegistrationToken firstToken;

			// Token: 0x04003882 RID: 14466
			private List<EventRegistrationToken> restTokens;
		}

		// Token: 0x02000CA0 RID: 3232
		internal static class ManagedEventRegistrationImpl
		{
			// Token: 0x06007156 RID: 29014 RVA: 0x001870FC File Offset: 0x001852FC
			[SecurityCritical]
			internal static void AddEventHandler<T>(Func<T, EventRegistrationToken> addMethod, Action<EventRegistrationToken> removeMethod, T handler)
			{
				object target = removeMethod.Target;
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> eventRegistrationTokenTable = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.GetEventRegistrationTokenTable(target, removeMethod);
				EventRegistrationToken eventRegistrationToken = addMethod(handler);
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> dictionary = eventRegistrationTokenTable;
				lock (dictionary)
				{
					WindowsRuntimeMarshal.EventRegistrationTokenList eventRegistrationTokenList;
					if (!eventRegistrationTokenTable.TryGetValue(handler, out eventRegistrationTokenList))
					{
						eventRegistrationTokenList = new WindowsRuntimeMarshal.EventRegistrationTokenList(eventRegistrationToken);
						eventRegistrationTokenTable[handler] = eventRegistrationTokenList;
					}
					else
					{
						bool flag2 = eventRegistrationTokenList.Push(eventRegistrationToken);
						if (flag2)
						{
							eventRegistrationTokenTable[handler] = eventRegistrationTokenList;
						}
					}
				}
			}

			// Token: 0x06007157 RID: 29015 RVA: 0x00187190 File Offset: 0x00185390
			private static Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> GetEventRegistrationTokenTable(object instance, Action<EventRegistrationToken> removeMethod)
			{
				ConditionalWeakTable<object, Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>> conditionalWeakTable = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations;
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> dictionary3;
				lock (conditionalWeakTable)
				{
					Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>> dictionary = null;
					if (!WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations.TryGetValue(instance, out dictionary))
					{
						dictionary = new Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>();
						WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations.Add(instance, dictionary);
					}
					Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> dictionary2 = null;
					if (!dictionary.TryGetValue(removeMethod.Method, out dictionary2))
					{
						dictionary2 = new Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>();
						dictionary.Add(removeMethod.Method, dictionary2);
					}
					dictionary3 = dictionary2;
				}
				return dictionary3;
			}

			// Token: 0x06007158 RID: 29016 RVA: 0x0018721C File Offset: 0x0018541C
			[SecurityCritical]
			internal static void RemoveEventHandler<T>(Action<EventRegistrationToken> removeMethod, T handler)
			{
				object target = removeMethod.Target;
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> eventRegistrationTokenTable = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.GetEventRegistrationTokenTable(target, removeMethod);
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> dictionary = eventRegistrationTokenTable;
				EventRegistrationToken eventRegistrationToken;
				lock (dictionary)
				{
					WindowsRuntimeMarshal.EventRegistrationTokenList eventRegistrationTokenList;
					if (!eventRegistrationTokenTable.TryGetValue(handler, out eventRegistrationTokenList))
					{
						return;
					}
					if (!eventRegistrationTokenList.Pop(out eventRegistrationToken))
					{
						eventRegistrationTokenTable.Remove(handler);
					}
				}
				removeMethod(eventRegistrationToken);
			}

			// Token: 0x06007159 RID: 29017 RVA: 0x00187298 File Offset: 0x00185498
			[SecurityCritical]
			internal static void RemoveAllEventHandlers(Action<EventRegistrationToken> removeMethod)
			{
				object target = removeMethod.Target;
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> eventRegistrationTokenTable = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.GetEventRegistrationTokenTable(target, removeMethod);
				List<EventRegistrationToken> list = new List<EventRegistrationToken>();
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> dictionary = eventRegistrationTokenTable;
				lock (dictionary)
				{
					foreach (WindowsRuntimeMarshal.EventRegistrationTokenList eventRegistrationTokenList in eventRegistrationTokenTable.Values)
					{
						eventRegistrationTokenList.CopyTo(list);
					}
					eventRegistrationTokenTable.Clear();
				}
				WindowsRuntimeMarshal.CallRemoveMethods(removeMethod, list);
			}

			// Token: 0x04003883 RID: 14467
			internal static volatile ConditionalWeakTable<object, Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>> s_eventRegistrations = new ConditionalWeakTable<object, Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>>();
		}

		// Token: 0x02000CA1 RID: 3233
		internal static class NativeOrStaticEventRegistrationImpl
		{
			// Token: 0x0600715B RID: 29019 RVA: 0x00187348 File Offset: 0x00185548
			[SecuritySafeCritical]
			private static object GetInstanceKey(Action<EventRegistrationToken> removeMethod)
			{
				object target = removeMethod.Target;
				if (target == null)
				{
					return removeMethod.Method.DeclaringType;
				}
				return Marshal.GetRawIUnknownForComObjectNoAddRef(target);
			}

			// Token: 0x0600715C RID: 29020 RVA: 0x00187378 File Offset: 0x00185578
			[SecurityCritical]
			internal static void AddEventHandler<T>(Func<T, EventRegistrationToken> addMethod, Action<EventRegistrationToken> removeMethod, T handler)
			{
				object instanceKey = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetInstanceKey(removeMethod);
				EventRegistrationToken eventRegistrationToken = addMethod(handler);
				bool flag = false;
				try
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.AcquireReaderLock(-1);
					try
					{
						WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount;
						ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> orCreateEventRegistrationTokenTable = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetOrCreateEventRegistrationTokenTable(instanceKey, removeMethod, out tokenListCount);
						ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> conditionalWeakTable = orCreateEventRegistrationTokenTable;
						lock (conditionalWeakTable)
						{
							WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount eventRegistrationTokenListWithCount;
							if (orCreateEventRegistrationTokenTable.FindEquivalentKeyUnsafe(handler, out eventRegistrationTokenListWithCount) == null)
							{
								eventRegistrationTokenListWithCount = new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount(tokenListCount, eventRegistrationToken);
								orCreateEventRegistrationTokenTable.Add(handler, eventRegistrationTokenListWithCount);
							}
							else
							{
								eventRegistrationTokenListWithCount.Push(eventRegistrationToken);
							}
							flag = true;
						}
					}
					finally
					{
						WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.ReleaseReaderLock();
					}
				}
				catch (Exception)
				{
					if (!flag)
					{
						removeMethod(eventRegistrationToken);
					}
					throw;
				}
			}

			// Token: 0x0600715D RID: 29021 RVA: 0x0018744C File Offset: 0x0018564C
			private static ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> GetEventRegistrationTokenTableNoCreate(object instance, Action<EventRegistrationToken> removeMethod, out WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount)
			{
				return WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetEventRegistrationTokenTableInternal(instance, removeMethod, out tokenListCount, false);
			}

			// Token: 0x0600715E RID: 29022 RVA: 0x00187457 File Offset: 0x00185657
			private static ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> GetOrCreateEventRegistrationTokenTable(object instance, Action<EventRegistrationToken> removeMethod, out WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount)
			{
				return WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetEventRegistrationTokenTableInternal(instance, removeMethod, out tokenListCount, true);
			}

			// Token: 0x0600715F RID: 29023 RVA: 0x00187464 File Offset: 0x00185664
			private static ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> GetEventRegistrationTokenTableInternal(object instance, Action<EventRegistrationToken> removeMethod, out WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount, bool createIfNotFound)
			{
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey eventCacheKey;
				eventCacheKey.target = instance;
				eventCacheKey.method = removeMethod.Method;
				Dictionary<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry> dictionary = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations;
				ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> registrationTable;
				lock (dictionary)
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry eventCacheEntry;
					if (!WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations.TryGetValue(eventCacheKey, out eventCacheEntry))
					{
						if (!createIfNotFound)
						{
							tokenListCount = null;
							return null;
						}
						eventCacheEntry = default(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry);
						eventCacheEntry.registrationTable = new ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount>();
						eventCacheEntry.tokenListCount = new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount(eventCacheKey);
						WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations.Add(eventCacheKey, eventCacheEntry);
					}
					tokenListCount = eventCacheEntry.tokenListCount;
					registrationTable = eventCacheEntry.registrationTable;
				}
				return registrationTable;
			}

			// Token: 0x06007160 RID: 29024 RVA: 0x00187514 File Offset: 0x00185714
			[SecurityCritical]
			internal static void RemoveEventHandler<T>(Action<EventRegistrationToken> removeMethod, T handler)
			{
				object instanceKey = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetInstanceKey(removeMethod);
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.AcquireReaderLock(-1);
				EventRegistrationToken eventRegistrationToken;
				try
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount;
					ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> eventRegistrationTokenTableNoCreate = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetEventRegistrationTokenTableNoCreate(instanceKey, removeMethod, out tokenListCount);
					if (eventRegistrationTokenTableNoCreate == null)
					{
						return;
					}
					ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> conditionalWeakTable = eventRegistrationTokenTableNoCreate;
					lock (conditionalWeakTable)
					{
						WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount eventRegistrationTokenListWithCount;
						object obj = eventRegistrationTokenTableNoCreate.FindEquivalentKeyUnsafe(handler, out eventRegistrationTokenListWithCount);
						if (eventRegistrationTokenListWithCount == null)
						{
							return;
						}
						if (!eventRegistrationTokenListWithCount.Pop(out eventRegistrationToken))
						{
							eventRegistrationTokenTableNoCreate.Remove(obj);
						}
					}
				}
				finally
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.ReleaseReaderLock();
				}
				removeMethod(eventRegistrationToken);
			}

			// Token: 0x06007161 RID: 29025 RVA: 0x001875C0 File Offset: 0x001857C0
			[SecurityCritical]
			internal static void RemoveAllEventHandlers(Action<EventRegistrationToken> removeMethod)
			{
				object instanceKey = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetInstanceKey(removeMethod);
				List<EventRegistrationToken> list = new List<EventRegistrationToken>();
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.AcquireReaderLock(-1);
				try
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount;
					ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> eventRegistrationTokenTableNoCreate = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetEventRegistrationTokenTableNoCreate(instanceKey, removeMethod, out tokenListCount);
					if (eventRegistrationTokenTableNoCreate == null)
					{
						return;
					}
					ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> conditionalWeakTable = eventRegistrationTokenTableNoCreate;
					lock (conditionalWeakTable)
					{
						foreach (WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount eventRegistrationTokenListWithCount in eventRegistrationTokenTableNoCreate.Values)
						{
							eventRegistrationTokenListWithCount.CopyTo(list);
						}
						eventRegistrationTokenTableNoCreate.Clear();
					}
				}
				finally
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.ReleaseReaderLock();
				}
				WindowsRuntimeMarshal.CallRemoveMethods(removeMethod, list);
			}

			// Token: 0x04003884 RID: 14468
			internal static volatile Dictionary<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry> s_eventRegistrations = new Dictionary<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry>(new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKeyEqualityComparer());

			// Token: 0x04003885 RID: 14469
			private static volatile WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.MyReaderWriterLock s_eventCacheRWLock = new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.MyReaderWriterLock();

			// Token: 0x02000D0E RID: 3342
			internal struct EventCacheKey
			{
				// Token: 0x0600723F RID: 29247 RVA: 0x0018AE44 File Offset: 0x00189044
				public override string ToString()
				{
					string[] array = new string[5];
					array[0] = "(";
					int num = 1;
					object obj = this.target;
					array[num] = ((obj != null) ? obj.ToString() : null);
					array[2] = ", ";
					int num2 = 3;
					MethodInfo methodInfo = this.method;
					array[num2] = ((methodInfo != null) ? methodInfo.ToString() : null);
					array[4] = ")";
					return string.Concat(array);
				}

				// Token: 0x0400396A RID: 14698
				internal object target;

				// Token: 0x0400396B RID: 14699
				internal MethodInfo method;
			}

			// Token: 0x02000D0F RID: 3343
			internal class EventCacheKeyEqualityComparer : IEqualityComparer<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey>
			{
				// Token: 0x06007240 RID: 29248 RVA: 0x0018AE9E File Offset: 0x0018909E
				public bool Equals(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey lhs, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey rhs)
				{
					return object.Equals(lhs.target, rhs.target) && object.Equals(lhs.method, rhs.method);
				}

				// Token: 0x06007241 RID: 29249 RVA: 0x0018AEC6 File Offset: 0x001890C6
				public int GetHashCode(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey key)
				{
					return key.target.GetHashCode() ^ key.method.GetHashCode();
				}
			}

			// Token: 0x02000D10 RID: 3344
			internal class EventRegistrationTokenListWithCount
			{
				// Token: 0x06007243 RID: 29251 RVA: 0x0018AEE7 File Offset: 0x001890E7
				internal EventRegistrationTokenListWithCount(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount, EventRegistrationToken token)
				{
					this._tokenListCount = tokenListCount;
					this._tokenListCount.Inc();
					this._tokenList = new WindowsRuntimeMarshal.EventRegistrationTokenList(token);
				}

				// Token: 0x06007244 RID: 29252 RVA: 0x0018AF10 File Offset: 0x00189110
				~EventRegistrationTokenListWithCount()
				{
					this._tokenListCount.Dec();
				}

				// Token: 0x06007245 RID: 29253 RVA: 0x0018AF44 File Offset: 0x00189144
				public void Push(EventRegistrationToken token)
				{
					this._tokenList.Push(token);
				}

				// Token: 0x06007246 RID: 29254 RVA: 0x0018AF53 File Offset: 0x00189153
				public bool Pop(out EventRegistrationToken token)
				{
					return this._tokenList.Pop(out token);
				}

				// Token: 0x06007247 RID: 29255 RVA: 0x0018AF61 File Offset: 0x00189161
				public void CopyTo(List<EventRegistrationToken> tokens)
				{
					this._tokenList.CopyTo(tokens);
				}

				// Token: 0x0400396C RID: 14700
				private WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount _tokenListCount;

				// Token: 0x0400396D RID: 14701
				private WindowsRuntimeMarshal.EventRegistrationTokenList _tokenList;
			}

			// Token: 0x02000D11 RID: 3345
			internal class TokenListCount
			{
				// Token: 0x06007248 RID: 29256 RVA: 0x0018AF6F File Offset: 0x0018916F
				internal TokenListCount(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey key)
				{
					this._key = key;
				}

				// Token: 0x17001393 RID: 5011
				// (get) Token: 0x06007249 RID: 29257 RVA: 0x0018AF7E File Offset: 0x0018917E
				internal WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey Key
				{
					get
					{
						return this._key;
					}
				}

				// Token: 0x0600724A RID: 29258 RVA: 0x0018AF88 File Offset: 0x00189188
				internal void Inc()
				{
					int num = Interlocked.Increment(ref this._count);
				}

				// Token: 0x0600724B RID: 29259 RVA: 0x0018AFA4 File Offset: 0x001891A4
				internal void Dec()
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.AcquireWriterLock(-1);
					try
					{
						if (Interlocked.Decrement(ref this._count) == 0)
						{
							this.CleanupCache();
						}
					}
					finally
					{
						WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.ReleaseWriterLock();
					}
				}

				// Token: 0x0600724C RID: 29260 RVA: 0x0018AFF4 File Offset: 0x001891F4
				private void CleanupCache()
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations.Remove(this._key);
				}

				// Token: 0x0400396E RID: 14702
				private int _count;

				// Token: 0x0400396F RID: 14703
				private WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey _key;
			}

			// Token: 0x02000D12 RID: 3346
			internal struct EventCacheEntry
			{
				// Token: 0x04003970 RID: 14704
				internal ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> registrationTable;

				// Token: 0x04003971 RID: 14705
				internal WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount;
			}

			// Token: 0x02000D13 RID: 3347
			internal class ReaderWriterLockTimedOutException : ApplicationException
			{
			}

			// Token: 0x02000D14 RID: 3348
			internal class MyReaderWriterLock
			{
				// Token: 0x0600724E RID: 29262 RVA: 0x0018B011 File Offset: 0x00189211
				internal MyReaderWriterLock()
				{
				}

				// Token: 0x0600724F RID: 29263 RVA: 0x0018B01C File Offset: 0x0018921C
				internal void AcquireReaderLock(int millisecondsTimeout)
				{
					this.EnterMyLock();
					while (this.owners < 0 || this.numWriteWaiters != 0U)
					{
						if (this.readEvent == null)
						{
							this.LazyCreateEvent(ref this.readEvent, false);
						}
						else
						{
							this.WaitOnEvent(this.readEvent, ref this.numReadWaiters, millisecondsTimeout);
						}
					}
					this.owners++;
					this.ExitMyLock();
				}

				// Token: 0x06007250 RID: 29264 RVA: 0x0018B084 File Offset: 0x00189284
				internal void AcquireWriterLock(int millisecondsTimeout)
				{
					this.EnterMyLock();
					while (this.owners != 0)
					{
						if (this.writeEvent == null)
						{
							this.LazyCreateEvent(ref this.writeEvent, true);
						}
						else
						{
							this.WaitOnEvent(this.writeEvent, ref this.numWriteWaiters, millisecondsTimeout);
						}
					}
					this.owners = -1;
					this.ExitMyLock();
				}

				// Token: 0x06007251 RID: 29265 RVA: 0x0018B0DA File Offset: 0x001892DA
				internal void ReleaseReaderLock()
				{
					this.EnterMyLock();
					this.owners--;
					this.ExitAndWakeUpAppropriateWaiters();
				}

				// Token: 0x06007252 RID: 29266 RVA: 0x0018B0F6 File Offset: 0x001892F6
				internal void ReleaseWriterLock()
				{
					this.EnterMyLock();
					this.owners++;
					this.ExitAndWakeUpAppropriateWaiters();
				}

				// Token: 0x06007253 RID: 29267 RVA: 0x0018B114 File Offset: 0x00189314
				private void LazyCreateEvent(ref EventWaitHandle waitEvent, bool makeAutoResetEvent)
				{
					this.ExitMyLock();
					EventWaitHandle eventWaitHandle;
					if (makeAutoResetEvent)
					{
						eventWaitHandle = new AutoResetEvent(false);
					}
					else
					{
						eventWaitHandle = new ManualResetEvent(false);
					}
					this.EnterMyLock();
					if (waitEvent == null)
					{
						waitEvent = eventWaitHandle;
					}
				}

				// Token: 0x06007254 RID: 29268 RVA: 0x0018B148 File Offset: 0x00189348
				private void WaitOnEvent(EventWaitHandle waitEvent, ref uint numWaiters, int millisecondsTimeout)
				{
					waitEvent.Reset();
					numWaiters += 1U;
					bool flag = false;
					this.ExitMyLock();
					try
					{
						if (!waitEvent.WaitOne(millisecondsTimeout, false))
						{
							throw new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.ReaderWriterLockTimedOutException();
						}
						flag = true;
					}
					finally
					{
						this.EnterMyLock();
						numWaiters -= 1U;
						if (!flag)
						{
							this.ExitMyLock();
						}
					}
				}

				// Token: 0x06007255 RID: 29269 RVA: 0x0018B1A4 File Offset: 0x001893A4
				private void ExitAndWakeUpAppropriateWaiters()
				{
					if (this.owners == 0 && this.numWriteWaiters > 0U)
					{
						this.ExitMyLock();
						this.writeEvent.Set();
						return;
					}
					if (this.owners >= 0 && this.numReadWaiters != 0U)
					{
						this.ExitMyLock();
						this.readEvent.Set();
						return;
					}
					this.ExitMyLock();
				}

				// Token: 0x06007256 RID: 29270 RVA: 0x0018B1FF File Offset: 0x001893FF
				private void EnterMyLock()
				{
					if (Interlocked.CompareExchange(ref this.myLock, 1, 0) != 0)
					{
						this.EnterMyLockSpin();
					}
				}

				// Token: 0x06007257 RID: 29271 RVA: 0x0018B218 File Offset: 0x00189418
				private void EnterMyLockSpin()
				{
					int num = 0;
					for (;;)
					{
						if (num < 3 && Environment.ProcessorCount > 1)
						{
							Thread.SpinWait(20);
						}
						else
						{
							Thread.Sleep(0);
						}
						if (Interlocked.CompareExchange(ref this.myLock, 1, 0) == 0)
						{
							break;
						}
						num++;
					}
				}

				// Token: 0x06007258 RID: 29272 RVA: 0x0018B257 File Offset: 0x00189457
				private void ExitMyLock()
				{
					this.myLock = 0;
				}

				// Token: 0x04003972 RID: 14706
				private int myLock;

				// Token: 0x04003973 RID: 14707
				private int owners;

				// Token: 0x04003974 RID: 14708
				private uint numWriteWaiters;

				// Token: 0x04003975 RID: 14709
				private uint numReadWaiters;

				// Token: 0x04003976 RID: 14710
				private EventWaitHandle writeEvent;

				// Token: 0x04003977 RID: 14711
				private EventWaitHandle readEvent;
			}
		}
	}
}
