using System;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000419 RID: 1049
	internal class ActivityTracker
	{
		// Token: 0x06003451 RID: 13393 RVA: 0x000C8528 File Offset: 0x000C6728
		public void OnStart(string providerName, string activityName, int task, ref Guid activityId, ref Guid relatedActivityId, EventActivityOptions options)
		{
			if (this.m_current == null)
			{
				if (this.m_checkedForEnable)
				{
					return;
				}
				this.m_checkedForEnable = true;
				if (TplEtwProvider.Log.IsEnabled(EventLevel.Informational, (EventKeywords)128L))
				{
					this.Enable();
				}
				if (this.m_current == null)
				{
					return;
				}
			}
			ActivityTracker.ActivityInfo activityInfo = this.m_current.Value;
			string text = this.NormalizeActivityName(providerName, activityName, task);
			TplEtwProvider log = TplEtwProvider.Log;
			if (log.Debug)
			{
				log.DebugFacilityMessage("OnStartEnter", text);
				log.DebugFacilityMessage("OnStartEnterActivityState", ActivityTracker.ActivityInfo.LiveActivities(activityInfo));
			}
			if (activityInfo != null)
			{
				if (activityInfo.m_level >= 100)
				{
					activityId = Guid.Empty;
					relatedActivityId = Guid.Empty;
					if (log.Debug)
					{
						log.DebugFacilityMessage("OnStartRET", "Fail");
					}
					return;
				}
				if ((options & EventActivityOptions.Recursive) == EventActivityOptions.None)
				{
					ActivityTracker.ActivityInfo activityInfo2 = this.FindActiveActivity(text, activityInfo);
					if (activityInfo2 != null)
					{
						this.OnStop(providerName, activityName, task, ref activityId);
						activityInfo = this.m_current.Value;
					}
				}
			}
			long num;
			if (activityInfo == null)
			{
				num = Interlocked.Increment(ref ActivityTracker.m_nextId);
			}
			else
			{
				num = Interlocked.Increment(ref activityInfo.m_lastChildID);
			}
			relatedActivityId = EventSource.CurrentThreadActivityId;
			ActivityTracker.ActivityInfo activityInfo3 = new ActivityTracker.ActivityInfo(text, num, activityInfo, relatedActivityId, options);
			this.m_current.Value = activityInfo3;
			activityId = activityInfo3.ActivityId;
			if (log.Debug)
			{
				log.DebugFacilityMessage("OnStartRetActivityState", ActivityTracker.ActivityInfo.LiveActivities(activityInfo3));
				log.DebugFacilityMessage1("OnStartRet", activityId.ToString(), relatedActivityId.ToString());
			}
		}

		// Token: 0x06003452 RID: 13394 RVA: 0x000C86B0 File Offset: 0x000C68B0
		public void OnStop(string providerName, string activityName, int task, ref Guid activityId)
		{
			if (this.m_current == null)
			{
				return;
			}
			string text = this.NormalizeActivityName(providerName, activityName, task);
			TplEtwProvider log = TplEtwProvider.Log;
			if (log.Debug)
			{
				log.DebugFacilityMessage("OnStopEnter", text);
				log.DebugFacilityMessage("OnStopEnterActivityState", ActivityTracker.ActivityInfo.LiveActivities(this.m_current.Value));
			}
			ActivityTracker.ActivityInfo activityInfo;
			for (;;)
			{
				ActivityTracker.ActivityInfo value = this.m_current.Value;
				activityInfo = null;
				ActivityTracker.ActivityInfo activityInfo2 = this.FindActiveActivity(text, value);
				if (activityInfo2 == null)
				{
					break;
				}
				activityId = activityInfo2.ActivityId;
				ActivityTracker.ActivityInfo activityInfo3 = value;
				while (activityInfo3 != activityInfo2 && activityInfo3 != null)
				{
					if (activityInfo3.m_stopped != 0)
					{
						activityInfo3 = activityInfo3.m_creator;
					}
					else
					{
						if (activityInfo3.CanBeOrphan())
						{
							if (activityInfo == null)
							{
								activityInfo = activityInfo3;
							}
						}
						else
						{
							activityInfo3.m_stopped = 1;
						}
						activityInfo3 = activityInfo3.m_creator;
					}
				}
				if (Interlocked.CompareExchange(ref activityInfo2.m_stopped, 1, 0) == 0)
				{
					goto Block_9;
				}
			}
			activityId = Guid.Empty;
			if (log.Debug)
			{
				log.DebugFacilityMessage("OnStopRET", "Fail");
			}
			return;
			Block_9:
			if (activityInfo == null)
			{
				ActivityTracker.ActivityInfo activityInfo2;
				activityInfo = activityInfo2.m_creator;
			}
			this.m_current.Value = activityInfo;
			if (log.Debug)
			{
				log.DebugFacilityMessage("OnStopRetActivityState", ActivityTracker.ActivityInfo.LiveActivities(activityInfo));
				log.DebugFacilityMessage("OnStopRet", activityId.ToString());
			}
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x000C87F4 File Offset: 0x000C69F4
		[SecuritySafeCritical]
		public void Enable()
		{
			if (this.m_current == null)
			{
				this.m_current = new AsyncLocal<ActivityTracker.ActivityInfo>(new Action<AsyncLocalValueChangedArgs<ActivityTracker.ActivityInfo>>(this.ActivityChanging));
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06003454 RID: 13396 RVA: 0x000C8815 File Offset: 0x000C6A15
		public static ActivityTracker Instance
		{
			get
			{
				return ActivityTracker.s_activityTrackerInstance;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06003455 RID: 13397 RVA: 0x000C881C File Offset: 0x000C6A1C
		private Guid CurrentActivityId
		{
			get
			{
				return this.m_current.Value.ActivityId;
			}
		}

		// Token: 0x06003456 RID: 13398 RVA: 0x000C8830 File Offset: 0x000C6A30
		private ActivityTracker.ActivityInfo FindActiveActivity(string name, ActivityTracker.ActivityInfo startLocation)
		{
			for (ActivityTracker.ActivityInfo activityInfo = startLocation; activityInfo != null; activityInfo = activityInfo.m_creator)
			{
				if (name == activityInfo.m_name && activityInfo.m_stopped == 0)
				{
					return activityInfo;
				}
			}
			return null;
		}

		// Token: 0x06003457 RID: 13399 RVA: 0x000C8864 File Offset: 0x000C6A64
		private string NormalizeActivityName(string providerName, string activityName, int task)
		{
			if (activityName.EndsWith("Start"))
			{
				activityName = activityName.Substring(0, activityName.Length - "Start".Length);
			}
			else if (activityName.EndsWith("Stop"))
			{
				activityName = activityName.Substring(0, activityName.Length - "Stop".Length);
			}
			else if (task != 0)
			{
				activityName = "task" + task.ToString();
			}
			return providerName + activityName;
		}

		// Token: 0x06003458 RID: 13400 RVA: 0x000C88E0 File Offset: 0x000C6AE0
		private void ActivityChanging(AsyncLocalValueChangedArgs<ActivityTracker.ActivityInfo> args)
		{
			ActivityTracker.ActivityInfo activityInfo = args.CurrentValue;
			ActivityTracker.ActivityInfo previousValue = args.PreviousValue;
			if (previousValue != null && previousValue.m_creator == activityInfo && (activityInfo == null || previousValue.m_activityIdToRestore != activityInfo.ActivityId))
			{
				EventSource.SetCurrentThreadActivityId(previousValue.m_activityIdToRestore);
				return;
			}
			while (activityInfo != null)
			{
				if (activityInfo.m_stopped == 0)
				{
					EventSource.SetCurrentThreadActivityId(activityInfo.ActivityId);
					return;
				}
				activityInfo = activityInfo.m_creator;
			}
		}

		// Token: 0x0400172F RID: 5935
		private AsyncLocal<ActivityTracker.ActivityInfo> m_current;

		// Token: 0x04001730 RID: 5936
		private bool m_checkedForEnable;

		// Token: 0x04001731 RID: 5937
		private static ActivityTracker s_activityTrackerInstance = new ActivityTracker();

		// Token: 0x04001732 RID: 5938
		private static long m_nextId = 0L;

		// Token: 0x04001733 RID: 5939
		private const ushort MAX_ACTIVITY_DEPTH = 100;

		// Token: 0x02000B7F RID: 2943
		private class ActivityInfo
		{
			// Token: 0x06006C77 RID: 27767 RVA: 0x00178C20 File Offset: 0x00176E20
			public ActivityInfo(string name, long uniqueId, ActivityTracker.ActivityInfo creator, Guid activityIDToRestore, EventActivityOptions options)
			{
				this.m_name = name;
				this.m_eventOptions = options;
				this.m_creator = creator;
				this.m_uniqueId = uniqueId;
				this.m_level = ((creator != null) ? (creator.m_level + 1) : 0);
				this.m_activityIdToRestore = activityIDToRestore;
				this.CreateActivityPathGuid(out this.m_guid, out this.m_activityPathGuidOffset);
			}

			// Token: 0x1700125B RID: 4699
			// (get) Token: 0x06006C78 RID: 27768 RVA: 0x00178C7E File Offset: 0x00176E7E
			public Guid ActivityId
			{
				get
				{
					return this.m_guid;
				}
			}

			// Token: 0x06006C79 RID: 27769 RVA: 0x00178C88 File Offset: 0x00176E88
			public static string Path(ActivityTracker.ActivityInfo activityInfo)
			{
				if (activityInfo == null)
				{
					return "";
				}
				return ActivityTracker.ActivityInfo.Path(activityInfo.m_creator) + "/" + activityInfo.m_uniqueId.ToString();
			}

			// Token: 0x06006C7A RID: 27770 RVA: 0x00178CC4 File Offset: 0x00176EC4
			public override string ToString()
			{
				string text = "";
				if (this.m_stopped != 0)
				{
					text = ",DEAD";
				}
				return string.Concat(new string[]
				{
					this.m_name,
					"(",
					ActivityTracker.ActivityInfo.Path(this),
					text,
					")"
				});
			}

			// Token: 0x06006C7B RID: 27771 RVA: 0x00178D16 File Offset: 0x00176F16
			public static string LiveActivities(ActivityTracker.ActivityInfo list)
			{
				if (list == null)
				{
					return "";
				}
				return list.ToString() + ";" + ActivityTracker.ActivityInfo.LiveActivities(list.m_creator);
			}

			// Token: 0x06006C7C RID: 27772 RVA: 0x00178D3C File Offset: 0x00176F3C
			public bool CanBeOrphan()
			{
				return (this.m_eventOptions & EventActivityOptions.Detachable) != EventActivityOptions.None;
			}

			// Token: 0x06006C7D RID: 27773 RVA: 0x00178D4C File Offset: 0x00176F4C
			[SecuritySafeCritical]
			private unsafe void CreateActivityPathGuid(out Guid idRet, out int activityPathGuidOffset)
			{
				fixed (Guid* ptr = &idRet)
				{
					Guid* ptr2 = ptr;
					int num = 0;
					if (this.m_creator != null)
					{
						num = this.m_creator.m_activityPathGuidOffset;
						idRet = this.m_creator.m_guid;
					}
					else
					{
						int domainID = Thread.GetDomainID();
						num = ActivityTracker.ActivityInfo.AddIdToGuid(ptr2, num, (uint)domainID, false);
					}
					activityPathGuidOffset = ActivityTracker.ActivityInfo.AddIdToGuid(ptr2, num, (uint)this.m_uniqueId, false);
					if (12 < activityPathGuidOffset)
					{
						this.CreateOverflowGuid(ptr2);
					}
				}
			}

			// Token: 0x06006C7E RID: 27774 RVA: 0x00178DBC File Offset: 0x00176FBC
			[SecurityCritical]
			private unsafe void CreateOverflowGuid(Guid* outPtr)
			{
				for (ActivityTracker.ActivityInfo activityInfo = this.m_creator; activityInfo != null; activityInfo = activityInfo.m_creator)
				{
					if (activityInfo.m_activityPathGuidOffset <= 10)
					{
						uint num = (uint)Interlocked.Increment(ref activityInfo.m_lastChildID);
						*outPtr = activityInfo.m_guid;
						int num2 = ActivityTracker.ActivityInfo.AddIdToGuid(outPtr, activityInfo.m_activityPathGuidOffset, num, true);
						if (num2 <= 12)
						{
							break;
						}
					}
				}
			}

			// Token: 0x06006C7F RID: 27775 RVA: 0x00178E14 File Offset: 0x00177014
			[SecurityCritical]
			private unsafe static int AddIdToGuid(Guid* outPtr, int whereToAddId, uint id, bool overflow = false)
			{
				byte* ptr = (byte*)outPtr;
				byte* ptr2 = ptr + 12;
				ptr += whereToAddId;
				if (ptr2 == ptr)
				{
					return 13;
				}
				if (0U < id && id <= 10U && !overflow)
				{
					ActivityTracker.ActivityInfo.WriteNibble(ref ptr, ptr2, id);
				}
				else
				{
					uint num = 4U;
					if (id <= 255U)
					{
						num = 1U;
					}
					else if (id <= 65535U)
					{
						num = 2U;
					}
					else if (id <= 16777215U)
					{
						num = 3U;
					}
					if (overflow)
					{
						if (ptr2 == ptr + 2)
						{
							return 13;
						}
						ActivityTracker.ActivityInfo.WriteNibble(ref ptr, ptr2, 11U);
					}
					ActivityTracker.ActivityInfo.WriteNibble(ref ptr, ptr2, 12U + (num - 1U));
					if (ptr < ptr2 && *ptr != 0)
					{
						if (id < 4096U)
						{
							*ptr = (byte)(192U + (id >> 8));
							id &= 255U;
						}
						ptr++;
					}
					while (0U < num)
					{
						if (ptr2 == ptr)
						{
							ptr++;
							break;
						}
						*(ptr++) = (byte)id;
						id >>= 8;
						num -= 1U;
					}
				}
				*(int*)(outPtr + (IntPtr)3 * 4 / (IntPtr)sizeof(Guid)) = (int)(*(uint*)outPtr + *(uint*)(outPtr + 4 / sizeof(Guid)) + *(uint*)(outPtr + (IntPtr)2 * 4 / (IntPtr)sizeof(Guid)) + 1503500717U);
				return (int)((long)((byte*)ptr - (byte*)outPtr));
			}

			// Token: 0x06006C80 RID: 27776 RVA: 0x00178F04 File Offset: 0x00177104
			[SecurityCritical]
			private unsafe static void WriteNibble(ref byte* ptr, byte* endPtr, uint value)
			{
				if (*ptr != 0)
				{
					byte* ptr2 = ptr;
					ptr = ptr2 + 1;
					byte* ptr3 = ptr2;
					*ptr3 |= (byte)value;
					return;
				}
				*ptr = (byte)(value << 4);
			}

			// Token: 0x040034DF RID: 13535
			internal readonly string m_name;

			// Token: 0x040034E0 RID: 13536
			private readonly long m_uniqueId;

			// Token: 0x040034E1 RID: 13537
			internal readonly Guid m_guid;

			// Token: 0x040034E2 RID: 13538
			internal readonly int m_activityPathGuidOffset;

			// Token: 0x040034E3 RID: 13539
			internal readonly int m_level;

			// Token: 0x040034E4 RID: 13540
			internal readonly EventActivityOptions m_eventOptions;

			// Token: 0x040034E5 RID: 13541
			internal long m_lastChildID;

			// Token: 0x040034E6 RID: 13542
			internal int m_stopped;

			// Token: 0x040034E7 RID: 13543
			internal readonly ActivityTracker.ActivityInfo m_creator;

			// Token: 0x040034E8 RID: 13544
			internal readonly Guid m_activityIdToRestore;

			// Token: 0x02000CFD RID: 3325
			private enum NumberListCodes : byte
			{
				// Token: 0x0400392F RID: 14639
				End,
				// Token: 0x04003930 RID: 14640
				LastImmediateValue = 10,
				// Token: 0x04003931 RID: 14641
				PrefixCode,
				// Token: 0x04003932 RID: 14642
				MultiByte1
			}
		}
	}
}
