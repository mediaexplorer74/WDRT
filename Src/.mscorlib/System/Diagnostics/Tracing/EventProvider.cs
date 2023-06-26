using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200041D RID: 1053
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	internal class EventProvider : IDisposable
	{
		// Token: 0x06003469 RID: 13417 RVA: 0x000C8B9D File Offset: 0x000C6D9D
		[SecurityCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		protected EventProvider(Guid providerGuid)
		{
			this.m_providerId = providerGuid;
			this.Register(providerGuid);
		}

		// Token: 0x0600346A RID: 13418 RVA: 0x000C8BB3 File Offset: 0x000C6DB3
		internal EventProvider()
		{
		}

		// Token: 0x0600346B RID: 13419 RVA: 0x000C8BBC File Offset: 0x000C6DBC
		[SecurityCritical]
		internal void Register(Guid providerGuid)
		{
			this.m_providerId = providerGuid;
			this.m_etwCallback = new UnsafeNativeMethods.ManifestEtw.EtwEnableCallback(this.EtwEnableCallBack);
			uint num = this.EventRegister(ref this.m_providerId, this.m_etwCallback);
			if (num != 0U)
			{
				throw new ArgumentException(Win32Native.GetMessage((int)num));
			}
		}

		// Token: 0x0600346C RID: 13420 RVA: 0x000C8C04 File Offset: 0x000C6E04
		[SecurityCritical]
		internal unsafe int SetInformation(UnsafeNativeMethods.ManifestEtw.EVENT_INFO_CLASS eventInfoClass, void* data, int dataSize)
		{
			int num = 50;
			if (!EventProvider.m_setInformationMissing)
			{
				try
				{
					num = UnsafeNativeMethods.ManifestEtw.EventSetInformation(this.m_regHandle, eventInfoClass, data, dataSize);
				}
				catch (TypeLoadException)
				{
					EventProvider.m_setInformationMissing = true;
				}
			}
			return num;
		}

		// Token: 0x0600346D RID: 13421 RVA: 0x000C8C48 File Offset: 0x000C6E48
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600346E RID: 13422 RVA: 0x000C8C58 File Offset: 0x000C6E58
		[SecuritySafeCritical]
		protected virtual void Dispose(bool disposing)
		{
			if (this.m_disposed)
			{
				return;
			}
			this.m_enabled = false;
			long num = 0L;
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				if (this.m_disposed)
				{
					return;
				}
				num = this.m_regHandle;
				this.m_regHandle = 0L;
				this.m_disposed = true;
			}
			if (num != 0L)
			{
				this.EventUnregister(num);
			}
		}

		// Token: 0x0600346F RID: 13423 RVA: 0x000C8CD0 File Offset: 0x000C6ED0
		public virtual void Close()
		{
			this.Dispose();
		}

		// Token: 0x06003470 RID: 13424 RVA: 0x000C8CD8 File Offset: 0x000C6ED8
		~EventProvider()
		{
			this.Dispose(false);
		}

		// Token: 0x06003471 RID: 13425 RVA: 0x000C8D08 File Offset: 0x000C6F08
		[SecurityCritical]
		private unsafe void EtwEnableCallBack([In] ref Guid sourceId, [In] int controlCode, [In] byte setLevel, [In] long anyKeyword, [In] long allKeyword, [In] UnsafeNativeMethods.ManifestEtw.EVENT_FILTER_DESCRIPTOR* filterData, [In] void* callbackContext)
		{
			try
			{
				ControllerCommand controllerCommand = ControllerCommand.Update;
				IDictionary<string, string> dictionary = null;
				bool flag = false;
				if (controlCode == 1)
				{
					this.m_enabled = true;
					this.m_level = setLevel;
					this.m_anyKeywordMask = anyKeyword;
					this.m_allKeywordMask = allKeyword;
					List<Tuple<EventProvider.SessionInfo, bool>> sessions = this.GetSessions();
					using (List<Tuple<EventProvider.SessionInfo, bool>>.Enumerator enumerator = sessions.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Tuple<EventProvider.SessionInfo, bool> tuple = enumerator.Current;
							int sessionIdBit = tuple.Item1.sessionIdBit;
							int etwSessionId = tuple.Item1.etwSessionId;
							bool item = tuple.Item2;
							flag = true;
							dictionary = null;
							if (sessions.Count > 1)
							{
								filterData = null;
							}
							byte[] array;
							int i;
							if (item && this.GetDataFromController(etwSessionId, filterData, out controllerCommand, out array, out i))
							{
								dictionary = new Dictionary<string, string>(4);
								while (i < array.Length)
								{
									int num = EventProvider.FindNull(array, i);
									int num2 = num + 1;
									int num3 = EventProvider.FindNull(array, num2);
									if (num3 < array.Length)
									{
										string @string = Encoding.UTF8.GetString(array, i, num - i);
										string string2 = Encoding.UTF8.GetString(array, num2, num3 - num2);
										dictionary[@string] = string2;
									}
									i = num3 + 1;
								}
							}
							this.OnControllerCommand(controllerCommand, dictionary, item ? sessionIdBit : (-sessionIdBit), etwSessionId);
						}
						goto IL_162;
					}
				}
				if (controlCode == 0)
				{
					this.m_enabled = false;
					this.m_level = 0;
					this.m_anyKeywordMask = 0L;
					this.m_allKeywordMask = 0L;
					this.m_liveSessions = null;
				}
				else
				{
					if (controlCode != 2)
					{
						return;
					}
					controllerCommand = ControllerCommand.SendManifest;
				}
				IL_162:
				if (!flag)
				{
					this.OnControllerCommand(controllerCommand, dictionary, 0, 0);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06003472 RID: 13426 RVA: 0x000C8EC0 File Offset: 0x000C70C0
		protected virtual void OnControllerCommand(ControllerCommand command, IDictionary<string, string> arguments, int sessionId, int etwSessionId)
		{
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06003473 RID: 13427 RVA: 0x000C8EC2 File Offset: 0x000C70C2
		// (set) Token: 0x06003474 RID: 13428 RVA: 0x000C8ECA File Offset: 0x000C70CA
		protected EventLevel Level
		{
			get
			{
				return (EventLevel)this.m_level;
			}
			set
			{
				this.m_level = (byte)value;
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06003475 RID: 13429 RVA: 0x000C8ED4 File Offset: 0x000C70D4
		// (set) Token: 0x06003476 RID: 13430 RVA: 0x000C8EDC File Offset: 0x000C70DC
		protected EventKeywords MatchAnyKeyword
		{
			get
			{
				return (EventKeywords)this.m_anyKeywordMask;
			}
			set
			{
				this.m_anyKeywordMask = (long)value;
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06003477 RID: 13431 RVA: 0x000C8EE5 File Offset: 0x000C70E5
		// (set) Token: 0x06003478 RID: 13432 RVA: 0x000C8EED File Offset: 0x000C70ED
		protected EventKeywords MatchAllKeyword
		{
			get
			{
				return (EventKeywords)this.m_allKeywordMask;
			}
			set
			{
				this.m_allKeywordMask = (long)value;
			}
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x000C8EF6 File Offset: 0x000C70F6
		private static int FindNull(byte[] buffer, int idx)
		{
			while (idx < buffer.Length && buffer[idx] != 0)
			{
				idx++;
			}
			return idx;
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x000C8F0C File Offset: 0x000C710C
		[SecuritySafeCritical]
		private List<Tuple<EventProvider.SessionInfo, bool>> GetSessions()
		{
			List<EventProvider.SessionInfo> liveSessionList = null;
			this.GetSessionInfo(delegate(int etwSessionId, long matchAllKeywords)
			{
				EventProvider.GetSessionInfoCallback(etwSessionId, matchAllKeywords, ref liveSessionList);
			});
			List<Tuple<EventProvider.SessionInfo, bool>> list = new List<Tuple<EventProvider.SessionInfo, bool>>();
			if (this.m_liveSessions != null)
			{
				foreach (EventProvider.SessionInfo sessionInfo in this.m_liveSessions)
				{
					int num;
					if ((num = EventProvider.IndexOfSessionInList(liveSessionList, sessionInfo.etwSessionId)) < 0 || liveSessionList[num].sessionIdBit != sessionInfo.sessionIdBit)
					{
						list.Add(Tuple.Create<EventProvider.SessionInfo, bool>(sessionInfo, false));
					}
				}
			}
			if (liveSessionList != null)
			{
				foreach (EventProvider.SessionInfo sessionInfo2 in liveSessionList)
				{
					int num2;
					if ((num2 = EventProvider.IndexOfSessionInList(this.m_liveSessions, sessionInfo2.etwSessionId)) < 0 || this.m_liveSessions[num2].sessionIdBit != sessionInfo2.sessionIdBit)
					{
						list.Add(Tuple.Create<EventProvider.SessionInfo, bool>(sessionInfo2, true));
					}
				}
			}
			this.m_liveSessions = liveSessionList;
			return list;
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x000C9058 File Offset: 0x000C7258
		private static void GetSessionInfoCallback(int etwSessionId, long matchAllKeywords, ref List<EventProvider.SessionInfo> sessionList)
		{
			uint num = (uint)SessionMask.FromEventKeywords((ulong)matchAllKeywords);
			if (EventProvider.bitcount(num) > 1)
			{
				return;
			}
			if (sessionList == null)
			{
				sessionList = new List<EventProvider.SessionInfo>(8);
			}
			if (EventProvider.bitcount(num) == 1)
			{
				sessionList.Add(new EventProvider.SessionInfo(EventProvider.bitindex(num) + 1, etwSessionId));
				return;
			}
			sessionList.Add(new EventProvider.SessionInfo(EventProvider.bitcount((uint)SessionMask.All) + 1, etwSessionId));
		}

		// Token: 0x0600347C RID: 13436 RVA: 0x000C90C4 File Offset: 0x000C72C4
		[SecurityCritical]
		private unsafe void GetSessionInfo(Action<int, long> action)
		{
			int num = 256;
			byte* ptr2;
			int num2;
			do
			{
				byte* ptr = stackalloc byte[(UIntPtr)num];
				ptr2 = ptr;
				fixed (Guid* ptr3 = &this.m_providerId)
				{
					Guid* ptr4 = ptr3;
					num2 = UnsafeNativeMethods.ManifestEtw.EnumerateTraceGuidsEx(UnsafeNativeMethods.ManifestEtw.TRACE_QUERY_INFO_CLASS.TraceGuidQueryInfo, (void*)ptr4, sizeof(Guid), (void*)ptr2, num, ref num);
				}
				if (num2 == 0)
				{
					goto IL_42;
				}
			}
			while (num2 == 122);
			return;
			IL_42:
			UnsafeNativeMethods.ManifestEtw.TRACE_GUID_INFO* ptr5 = (UnsafeNativeMethods.ManifestEtw.TRACE_GUID_INFO*)ptr2;
			UnsafeNativeMethods.ManifestEtw.TRACE_PROVIDER_INSTANCE_INFO* ptr6 = (UnsafeNativeMethods.ManifestEtw.TRACE_PROVIDER_INSTANCE_INFO*)(ptr5 + 1);
			int currentProcessId = (int)Win32Native.GetCurrentProcessId();
			for (int i = 0; i < ptr5->InstanceCount; i++)
			{
				if (ptr6->Pid == currentProcessId)
				{
					UnsafeNativeMethods.ManifestEtw.TRACE_ENABLE_INFO* ptr7 = (UnsafeNativeMethods.ManifestEtw.TRACE_ENABLE_INFO*)(ptr6 + 1);
					for (int j = 0; j < ptr6->EnableCount; j++)
					{
						action((int)ptr7[j].LoggerId, ptr7[j].MatchAllKeyword);
					}
				}
				if (ptr6->NextOffset == 0)
				{
					break;
				}
				byte* ptr8 = (byte*)ptr6;
				ptr6 = (UnsafeNativeMethods.ManifestEtw.TRACE_PROVIDER_INSTANCE_INFO*)(ptr8 + ptr6->NextOffset);
			}
		}

		// Token: 0x0600347D RID: 13437 RVA: 0x000C91A8 File Offset: 0x000C73A8
		private static int IndexOfSessionInList(List<EventProvider.SessionInfo> sessions, int etwSessionId)
		{
			if (sessions == null)
			{
				return -1;
			}
			for (int i = 0; i < sessions.Count; i++)
			{
				if (sessions[i].etwSessionId == etwSessionId)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600347E RID: 13438 RVA: 0x000C91E0 File Offset: 0x000C73E0
		[SecurityCritical]
		private unsafe bool GetDataFromController(int etwSessionId, UnsafeNativeMethods.ManifestEtw.EVENT_FILTER_DESCRIPTOR* filterData, out ControllerCommand command, out byte[] data, out int dataStart)
		{
			data = null;
			dataStart = 0;
			if (filterData != null)
			{
				if (filterData->Ptr != 0L && 0 < filterData->Size && filterData->Size <= 1024)
				{
					data = new byte[filterData->Size];
					Marshal.Copy((IntPtr)filterData->Ptr, data, 0, data.Length);
				}
				command = (ControllerCommand)filterData->Type;
				return true;
			}
			string text = "\\Microsoft\\Windows\\CurrentVersion\\Winevt\\Publishers\\{";
			Guid providerId = this.m_providerId;
			string text2 = text + providerId.ToString() + "}";
			if (Marshal.SizeOf(typeof(IntPtr)) == 8)
			{
				text2 = "HKEY_LOCAL_MACHINE\\Software\\Wow6432Node" + text2;
			}
			else
			{
				text2 = "HKEY_LOCAL_MACHINE\\Software" + text2;
			}
			string text3 = "ControllerData_Session_" + etwSessionId.ToString(CultureInfo.InvariantCulture);
			new RegistryPermission(RegistryPermissionAccess.Read, text2).Assert();
			data = Registry.GetValue(text2, text3, null) as byte[];
			if (data != null)
			{
				command = ControllerCommand.Update;
				return true;
			}
			command = ControllerCommand.Update;
			return false;
		}

		// Token: 0x0600347F RID: 13439 RVA: 0x000C92DD File Offset: 0x000C74DD
		public bool IsEnabled()
		{
			return this.m_enabled;
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x000C92E5 File Offset: 0x000C74E5
		public bool IsEnabled(byte level, long keywords)
		{
			return this.m_enabled && ((level <= this.m_level || this.m_level == 0) && (keywords == 0L || ((keywords & this.m_anyKeywordMask) != 0L && (keywords & this.m_allKeywordMask) == this.m_allKeywordMask)));
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x000C9322 File Offset: 0x000C7522
		internal bool IsValid()
		{
			return this.m_regHandle != 0L;
		}

		// Token: 0x06003482 RID: 13442 RVA: 0x000C932E File Offset: 0x000C752E
		public static EventProvider.WriteEventErrorCode GetLastWriteEventError()
		{
			return EventProvider.s_returnCode;
		}

		// Token: 0x06003483 RID: 13443 RVA: 0x000C9335 File Offset: 0x000C7535
		private static void SetLastError(int error)
		{
			if (error != 8)
			{
				if (error == 234 || error == 534)
				{
					EventProvider.s_returnCode = EventProvider.WriteEventErrorCode.EventTooBig;
					return;
				}
			}
			else
			{
				EventProvider.s_returnCode = EventProvider.WriteEventErrorCode.NoFreeBuffers;
			}
		}

		// Token: 0x06003484 RID: 13444 RVA: 0x000C9358 File Offset: 0x000C7558
		[SecurityCritical]
		private unsafe static object EncodeObject(ref object data, ref EventProvider.EventData* dataDescriptor, ref byte* dataBuffer, ref uint totalEventSize)
		{
			string text;
			byte[] array;
			for (;;)
			{
				dataDescriptor.Reserved = 0U;
				text = data as string;
				array = null;
				if (text != null)
				{
					break;
				}
				if ((array = data as byte[]) != null)
				{
					goto Block_1;
				}
				if (data is IntPtr)
				{
					goto Block_2;
				}
				if (data is int)
				{
					goto Block_3;
				}
				if (data is long)
				{
					goto Block_4;
				}
				if (data is uint)
				{
					goto Block_5;
				}
				if (data is ulong)
				{
					goto Block_6;
				}
				if (data is char)
				{
					goto Block_7;
				}
				if (data is byte)
				{
					goto Block_8;
				}
				if (data is short)
				{
					goto Block_9;
				}
				if (data is sbyte)
				{
					goto Block_10;
				}
				if (data is ushort)
				{
					goto Block_11;
				}
				if (data is float)
				{
					goto Block_12;
				}
				if (data is double)
				{
					goto Block_13;
				}
				if (data is bool)
				{
					goto Block_14;
				}
				if (data is Guid)
				{
					goto Block_16;
				}
				if (data is decimal)
				{
					goto Block_17;
				}
				if (data is DateTime)
				{
					goto Block_18;
				}
				if (!(data is Enum))
				{
					goto IL_40C;
				}
				Type underlyingType = Enum.GetUnderlyingType(data.GetType());
				if (underlyingType == typeof(int))
				{
					data = ((IConvertible)data).ToInt32(null);
				}
				else
				{
					if (!(underlyingType == typeof(long)))
					{
						goto IL_40C;
					}
					data = ((IConvertible)data).ToInt64(null);
				}
			}
			dataDescriptor.Size = (uint)((text.Length + 1) * 2);
			goto IL_431;
			Block_1:
			*dataBuffer = array.Length;
			dataDescriptor.Ptr = (ulong)dataBuffer;
			dataDescriptor.Size = 4U;
			totalEventSize += dataDescriptor.Size;
			dataDescriptor += (IntPtr)sizeof(EventProvider.EventData);
			dataBuffer += 16;
			dataDescriptor.Size = (uint)array.Length;
			goto IL_431;
			Block_2:
			dataDescriptor.Size = (uint)sizeof(IntPtr);
			IntPtr* ptr = dataBuffer;
			*ptr = (IntPtr)data;
			dataDescriptor.Ptr = ptr;
			goto IL_431;
			Block_3:
			dataDescriptor.Size = 4U;
			int* ptr2 = dataBuffer;
			*ptr2 = (int)data;
			dataDescriptor.Ptr = ptr2;
			goto IL_431;
			Block_4:
			dataDescriptor.Size = 8U;
			long* ptr3 = dataBuffer;
			*ptr3 = (long)data;
			dataDescriptor.Ptr = ptr3;
			goto IL_431;
			Block_5:
			dataDescriptor.Size = 4U;
			uint* ptr4 = dataBuffer;
			*ptr4 = (uint)data;
			dataDescriptor.Ptr = ptr4;
			goto IL_431;
			Block_6:
			dataDescriptor.Size = 8U;
			ulong* ptr5 = dataBuffer;
			*ptr5 = (ulong)data;
			dataDescriptor.Ptr = ptr5;
			goto IL_431;
			Block_7:
			dataDescriptor.Size = 2U;
			char* ptr6 = dataBuffer;
			*ptr6 = (char)data;
			dataDescriptor.Ptr = ptr6;
			goto IL_431;
			Block_8:
			dataDescriptor.Size = 1U;
			byte* ptr7 = dataBuffer;
			*ptr7 = (byte)data;
			dataDescriptor.Ptr = ptr7;
			goto IL_431;
			Block_9:
			dataDescriptor.Size = 2U;
			short* ptr8 = dataBuffer;
			*ptr8 = (short)data;
			dataDescriptor.Ptr = ptr8;
			goto IL_431;
			Block_10:
			dataDescriptor.Size = 1U;
			sbyte* ptr9 = dataBuffer;
			*ptr9 = (sbyte)data;
			dataDescriptor.Ptr = ptr9;
			goto IL_431;
			Block_11:
			dataDescriptor.Size = 2U;
			ushort* ptr10 = dataBuffer;
			*ptr10 = (ushort)data;
			dataDescriptor.Ptr = ptr10;
			goto IL_431;
			Block_12:
			dataDescriptor.Size = 4U;
			float* ptr11 = dataBuffer;
			*ptr11 = (float)data;
			dataDescriptor.Ptr = ptr11;
			goto IL_431;
			Block_13:
			dataDescriptor.Size = 8U;
			double* ptr12 = dataBuffer;
			*ptr12 = (double)data;
			dataDescriptor.Ptr = ptr12;
			goto IL_431;
			Block_14:
			dataDescriptor.Size = 4U;
			int* ptr13 = dataBuffer;
			if ((bool)data)
			{
				*ptr13 = 1;
			}
			else
			{
				*ptr13 = 0;
			}
			dataDescriptor.Ptr = ptr13;
			goto IL_431;
			Block_16:
			dataDescriptor.Size = (uint)sizeof(Guid);
			Guid* ptr14 = dataBuffer;
			*ptr14 = (Guid)data;
			dataDescriptor.Ptr = ptr14;
			goto IL_431;
			Block_17:
			dataDescriptor.Size = 16U;
			decimal* ptr15 = dataBuffer;
			*ptr15 = (decimal)data;
			dataDescriptor.Ptr = ptr15;
			goto IL_431;
			Block_18:
			long num = 0L;
			if (((DateTime)data).Ticks > 504911232000000000L)
			{
				num = ((DateTime)data).ToFileTimeUtc();
			}
			dataDescriptor.Size = 8U;
			long* ptr16 = dataBuffer;
			*ptr16 = num;
			dataDescriptor.Ptr = ptr16;
			goto IL_431;
			IL_40C:
			if (data == null)
			{
				text = "";
			}
			else
			{
				text = data.ToString();
			}
			dataDescriptor.Size = (uint)((text.Length + 1) * 2);
			IL_431:
			totalEventSize += dataDescriptor.Size;
			dataDescriptor += (IntPtr)sizeof(EventProvider.EventData);
			dataBuffer += 16;
			return text ?? array;
		}

		// Token: 0x06003485 RID: 13445 RVA: 0x000C97BC File Offset: 0x000C79BC
		[SecurityCritical]
		internal unsafe bool WriteEvent(ref EventDescriptor eventDescriptor, Guid* activityID, Guid* childActivityID, params object[] eventPayload)
		{
			int num = 0;
			if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
			{
				int num2 = eventPayload.Length;
				if (num2 > 128)
				{
					EventProvider.s_returnCode = EventProvider.WriteEventErrorCode.TooManyArgs;
					return false;
				}
				uint num3 = 0U;
				int i = 0;
				List<int> list = new List<int>(8);
				List<object> list2 = new List<object>(8);
				EventProvider.EventData* ptr;
				EventProvider.EventData* ptr2;
				checked
				{
					ptr = stackalloc EventProvider.EventData[unchecked((UIntPtr)(2 * num2)) * (UIntPtr)sizeof(EventProvider.EventData)];
					ptr2 = ptr;
				}
				byte* ptr3 = stackalloc byte[(UIntPtr)(32 * num2)];
				byte* ptr4 = ptr3;
				bool flag = false;
				for (int j = 0; j < eventPayload.Length; j++)
				{
					if (eventPayload[j] == null)
					{
						EventProvider.s_returnCode = EventProvider.WriteEventErrorCode.NullInput;
						return false;
					}
					object obj = EventProvider.EncodeObject(ref eventPayload[j], ref ptr2, ref ptr4, ref num3);
					if (obj != null)
					{
						int num4 = (int)((long)(ptr2 - ptr) - 1L);
						if (!(obj is string))
						{
							if (eventPayload.Length + num4 + 1 - j > 128)
							{
								EventProvider.s_returnCode = EventProvider.WriteEventErrorCode.TooManyArgs;
								return false;
							}
							flag = true;
						}
						list2.Add(obj);
						list.Add(num4);
						i++;
					}
				}
				num2 = (int)((long)(ptr2 - ptr));
				if (num3 > 65482U)
				{
					EventProvider.s_returnCode = EventProvider.WriteEventErrorCode.EventTooBig;
					return false;
				}
				if (!flag && i < 8)
				{
					while (i < 8)
					{
						list2.Add(null);
						i++;
					}
					fixed (string text = (string)list2[0])
					{
						char* ptr5 = text;
						if (ptr5 != null)
						{
							ptr5 += RuntimeHelpers.OffsetToStringData / 2;
						}
						fixed (string text2 = (string)list2[1])
						{
							char* ptr6 = text2;
							if (ptr6 != null)
							{
								ptr6 += RuntimeHelpers.OffsetToStringData / 2;
							}
							fixed (string text3 = (string)list2[2])
							{
								char* ptr7 = text3;
								if (ptr7 != null)
								{
									ptr7 += RuntimeHelpers.OffsetToStringData / 2;
								}
								fixed (string text4 = (string)list2[3])
								{
									char* ptr8 = text4;
									if (ptr8 != null)
									{
										ptr8 += RuntimeHelpers.OffsetToStringData / 2;
									}
									fixed (string text5 = (string)list2[4])
									{
										char* ptr9 = text5;
										if (ptr9 != null)
										{
											ptr9 += RuntimeHelpers.OffsetToStringData / 2;
										}
										fixed (string text6 = (string)list2[5])
										{
											char* ptr10 = text6;
											if (ptr10 != null)
											{
												ptr10 += RuntimeHelpers.OffsetToStringData / 2;
											}
											fixed (string text7 = (string)list2[6])
											{
												char* ptr11 = text7;
												if (ptr11 != null)
												{
													ptr11 += RuntimeHelpers.OffsetToStringData / 2;
												}
												fixed (string text8 = (string)list2[7])
												{
													char* ptr12 = text8;
													if (ptr12 != null)
													{
														ptr12 += RuntimeHelpers.OffsetToStringData / 2;
													}
													ptr2 = ptr;
													if (list2[0] != null)
													{
														ptr2[list[0]].Ptr = ptr5;
													}
													if (list2[1] != null)
													{
														ptr2[list[1]].Ptr = ptr6;
													}
													if (list2[2] != null)
													{
														ptr2[list[2]].Ptr = ptr7;
													}
													if (list2[3] != null)
													{
														ptr2[list[3]].Ptr = ptr8;
													}
													if (list2[4] != null)
													{
														ptr2[list[4]].Ptr = ptr9;
													}
													if (list2[5] != null)
													{
														ptr2[list[5]].Ptr = ptr10;
													}
													if (list2[6] != null)
													{
														ptr2[list[6]].Ptr = ptr11;
													}
													if (list2[7] != null)
													{
														ptr2[list[7]].Ptr = ptr12;
													}
													num = UnsafeNativeMethods.ManifestEtw.EventWriteTransferWrapper(this.m_regHandle, ref eventDescriptor, activityID, childActivityID, num2, ptr);
													text = null;
													text2 = null;
													text3 = null;
													text4 = null;
													text5 = null;
													text6 = null;
													text7 = null;
												}
											}
										}
									}
								}
							}
						}
					}
				}
				else
				{
					ptr2 = ptr;
					GCHandle[] array = new GCHandle[i];
					for (int k = 0; k < i; k++)
					{
						array[k] = GCHandle.Alloc(list2[k], GCHandleType.Pinned);
						if (list2[k] is string)
						{
							fixed (string text9 = (string)list2[k])
							{
								char* ptr13 = text9;
								if (ptr13 != null)
								{
									ptr13 += RuntimeHelpers.OffsetToStringData / 2;
								}
								ptr2[list[k]].Ptr = ptr13;
							}
						}
						else
						{
							byte[] array2;
							byte* ptr14;
							if ((array2 = (byte[])list2[k]) == null || array2.Length == 0)
							{
								ptr14 = null;
							}
							else
							{
								ptr14 = &array2[0];
							}
							ptr2[list[k]].Ptr = ptr14;
							array2 = null;
						}
					}
					num = UnsafeNativeMethods.ManifestEtw.EventWriteTransferWrapper(this.m_regHandle, ref eventDescriptor, activityID, childActivityID, num2, ptr);
					for (int l = 0; l < i; l++)
					{
						array[l].Free();
					}
				}
			}
			if (num != 0)
			{
				EventProvider.SetLastError(num);
				return false;
			}
			return true;
		}

		// Token: 0x06003486 RID: 13446 RVA: 0x000C9C84 File Offset: 0x000C7E84
		[SecurityCritical]
		protected internal unsafe bool WriteEvent(ref EventDescriptor eventDescriptor, Guid* activityID, Guid* childActivityID, int dataCount, IntPtr data)
		{
			UIntPtr uintPtr = (UIntPtr)0;
			int num = UnsafeNativeMethods.ManifestEtw.EventWriteTransferWrapper(this.m_regHandle, ref eventDescriptor, activityID, childActivityID, dataCount, (EventProvider.EventData*)(void*)data);
			if (num != 0)
			{
				EventProvider.SetLastError(num);
				return false;
			}
			return true;
		}

		// Token: 0x06003487 RID: 13447 RVA: 0x000C9CBC File Offset: 0x000C7EBC
		[SecurityCritical]
		internal unsafe bool WriteEventRaw(ref EventDescriptor eventDescriptor, Guid* activityID, Guid* relatedActivityID, int dataCount, IntPtr data)
		{
			int num = UnsafeNativeMethods.ManifestEtw.EventWriteTransferWrapper(this.m_regHandle, ref eventDescriptor, activityID, relatedActivityID, dataCount, (EventProvider.EventData*)(void*)data);
			if (num != 0)
			{
				EventProvider.SetLastError(num);
				return false;
			}
			return true;
		}

		// Token: 0x06003488 RID: 13448 RVA: 0x000C9CED File Offset: 0x000C7EED
		[SecurityCritical]
		private uint EventRegister(ref Guid providerId, UnsafeNativeMethods.ManifestEtw.EtwEnableCallback enableCallback)
		{
			this.m_providerId = providerId;
			this.m_etwCallback = enableCallback;
			return UnsafeNativeMethods.ManifestEtw.EventRegister(ref providerId, enableCallback, null, ref this.m_regHandle);
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x000C9D11 File Offset: 0x000C7F11
		[SecurityCritical]
		private uint EventUnregister(long registrationHandle)
		{
			return UnsafeNativeMethods.ManifestEtw.EventUnregister(registrationHandle);
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x000C9D1C File Offset: 0x000C7F1C
		private static int bitcount(uint n)
		{
			int num = 0;
			while (n != 0U)
			{
				num += EventProvider.nibblebits[(int)(n & 15U)];
				n >>= 4;
			}
			return num;
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x000C9D44 File Offset: 0x000C7F44
		private static int bitindex(uint n)
		{
			int num = 0;
			while (((ulong)n & (ulong)(1L << (num & 31))) == 0UL)
			{
				num++;
			}
			return num;
		}

		// Token: 0x04001746 RID: 5958
		private static bool m_setInformationMissing;

		// Token: 0x04001747 RID: 5959
		[SecurityCritical]
		private UnsafeNativeMethods.ManifestEtw.EtwEnableCallback m_etwCallback;

		// Token: 0x04001748 RID: 5960
		private long m_regHandle;

		// Token: 0x04001749 RID: 5961
		private byte m_level;

		// Token: 0x0400174A RID: 5962
		private long m_anyKeywordMask;

		// Token: 0x0400174B RID: 5963
		private long m_allKeywordMask;

		// Token: 0x0400174C RID: 5964
		private List<EventProvider.SessionInfo> m_liveSessions;

		// Token: 0x0400174D RID: 5965
		private bool m_enabled;

		// Token: 0x0400174E RID: 5966
		private Guid m_providerId;

		// Token: 0x0400174F RID: 5967
		internal bool m_disposed;

		// Token: 0x04001750 RID: 5968
		[ThreadStatic]
		private static EventProvider.WriteEventErrorCode s_returnCode;

		// Token: 0x04001751 RID: 5969
		private const int s_basicTypeAllocationBufferSize = 16;

		// Token: 0x04001752 RID: 5970
		private const int s_etwMaxNumberArguments = 128;

		// Token: 0x04001753 RID: 5971
		private const int s_etwAPIMaxRefObjCount = 8;

		// Token: 0x04001754 RID: 5972
		private const int s_maxEventDataDescriptors = 128;

		// Token: 0x04001755 RID: 5973
		private const int s_traceEventMaximumSize = 65482;

		// Token: 0x04001756 RID: 5974
		private const int s_traceEventMaximumStringSize = 32724;

		// Token: 0x04001757 RID: 5975
		private static int[] nibblebits = new int[]
		{
			0, 1, 1, 2, 1, 2, 2, 3, 1, 2,
			2, 3, 2, 3, 3, 4
		};

		// Token: 0x02000B80 RID: 2944
		public struct EventData
		{
			// Token: 0x040034E9 RID: 13545
			internal ulong Ptr;

			// Token: 0x040034EA RID: 13546
			internal uint Size;

			// Token: 0x040034EB RID: 13547
			internal uint Reserved;
		}

		// Token: 0x02000B81 RID: 2945
		public struct SessionInfo
		{
			// Token: 0x06006C81 RID: 27777 RVA: 0x00178F2E File Offset: 0x0017712E
			internal SessionInfo(int sessionIdBit_, int etwSessionId_)
			{
				this.sessionIdBit = sessionIdBit_;
				this.etwSessionId = etwSessionId_;
			}

			// Token: 0x040034EC RID: 13548
			internal int sessionIdBit;

			// Token: 0x040034ED RID: 13549
			internal int etwSessionId;
		}

		// Token: 0x02000B82 RID: 2946
		public enum WriteEventErrorCode
		{
			// Token: 0x040034EF RID: 13551
			NoError,
			// Token: 0x040034F0 RID: 13552
			NoFreeBuffers,
			// Token: 0x040034F1 RID: 13553
			EventTooBig,
			// Token: 0x040034F2 RID: 13554
			NullInput,
			// Token: 0x040034F3 RID: 13555
			TooManyArgs,
			// Token: 0x040034F4 RID: 13556
			Other
		}
	}
}
