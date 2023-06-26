using System;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x02000585 RID: 1413
	[EventSource(Name = "System.Threading.Tasks.TplEventSource", Guid = "2e5dba47-a3d2-4d16-8ee0-6671ffdcd7b5", LocalizationResources = "mscorlib")]
	internal sealed class TplEtwProvider : EventSource
	{
		// Token: 0x06004293 RID: 17043 RVA: 0x000F9170 File Offset: 0x000F7370
		protected override void OnEventCommand(EventCommandEventArgs command)
		{
			if (command.Command == EventCommand.Enable)
			{
				AsyncCausalityTracer.EnableToETW(true);
			}
			else if (command.Command == EventCommand.Disable)
			{
				AsyncCausalityTracer.EnableToETW(false);
			}
			if (base.IsEnabled(EventLevel.Informational, (EventKeywords)128L))
			{
				ActivityTracker.Instance.Enable();
			}
			else
			{
				this.TasksSetActivityIds = base.IsEnabled(EventLevel.Informational, (EventKeywords)65536L);
			}
			this.Debug = base.IsEnabled(EventLevel.Informational, (EventKeywords)131072L);
			this.DebugActivityId = base.IsEnabled(EventLevel.Informational, (EventKeywords)262144L);
		}

		// Token: 0x06004294 RID: 17044 RVA: 0x000F91F3 File Offset: 0x000F73F3
		private TplEtwProvider()
		{
		}

		// Token: 0x06004295 RID: 17045 RVA: 0x000F91FC File Offset: 0x000F73FC
		[SecuritySafeCritical]
		[Event(1, Level = EventLevel.Informational, ActivityOptions = EventActivityOptions.Recursive, Task = (EventTask)1, Opcode = EventOpcode.Start)]
		public unsafe void ParallelLoopBegin(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID, TplEtwProvider.ForkJoinOperationType OperationType, long InclusiveFrom, long ExclusiveTo)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)4L))
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)6) * (UIntPtr)sizeof(EventSource.EventData)];
					ptr->Size = 4;
				}
				ptr->DataPointer = (IntPtr)((void*)(&OriginatingTaskSchedulerID));
				ptr[1].Size = 4;
				ptr[1].DataPointer = (IntPtr)((void*)(&OriginatingTaskID));
				ptr[2].Size = 4;
				ptr[2].DataPointer = (IntPtr)((void*)(&ForkJoinContextID));
				ptr[3].Size = 4;
				ptr[3].DataPointer = (IntPtr)((void*)(&OperationType));
				ptr[4].Size = 8;
				ptr[4].DataPointer = (IntPtr)((void*)(&InclusiveFrom));
				ptr[5].Size = 8;
				ptr[5].DataPointer = (IntPtr)((void*)(&ExclusiveTo));
				base.WriteEventCore(1, 6, ptr);
			}
		}

		// Token: 0x06004296 RID: 17046 RVA: 0x000F9314 File Offset: 0x000F7514
		[SecuritySafeCritical]
		[Event(2, Level = EventLevel.Informational, Task = (EventTask)1, Opcode = EventOpcode.Stop)]
		public unsafe void ParallelLoopEnd(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID, long TotalIterations)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)4L))
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData)];
					ptr->Size = 4;
				}
				ptr->DataPointer = (IntPtr)((void*)(&OriginatingTaskSchedulerID));
				ptr[1].Size = 4;
				ptr[1].DataPointer = (IntPtr)((void*)(&OriginatingTaskID));
				ptr[2].Size = 4;
				ptr[2].DataPointer = (IntPtr)((void*)(&ForkJoinContextID));
				ptr[3].Size = 8;
				ptr[3].DataPointer = (IntPtr)((void*)(&TotalIterations));
				base.WriteEventCore(2, 4, ptr);
			}
		}

		// Token: 0x06004297 RID: 17047 RVA: 0x000F93DC File Offset: 0x000F75DC
		[SecuritySafeCritical]
		[Event(3, Level = EventLevel.Informational, ActivityOptions = EventActivityOptions.Recursive, Task = (EventTask)2, Opcode = EventOpcode.Start)]
		public unsafe void ParallelInvokeBegin(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID, TplEtwProvider.ForkJoinOperationType OperationType, int ActionCount)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)4L))
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)5) * (UIntPtr)sizeof(EventSource.EventData)];
					ptr->Size = 4;
				}
				ptr->DataPointer = (IntPtr)((void*)(&OriginatingTaskSchedulerID));
				ptr[1].Size = 4;
				ptr[1].DataPointer = (IntPtr)((void*)(&OriginatingTaskID));
				ptr[2].Size = 4;
				ptr[2].DataPointer = (IntPtr)((void*)(&ForkJoinContextID));
				ptr[3].Size = 4;
				ptr[3].DataPointer = (IntPtr)((void*)(&OperationType));
				ptr[4].Size = 4;
				ptr[4].DataPointer = (IntPtr)((void*)(&ActionCount));
				base.WriteEventCore(3, 5, ptr);
			}
		}

		// Token: 0x06004298 RID: 17048 RVA: 0x000F94CA File Offset: 0x000F76CA
		[Event(4, Level = EventLevel.Informational, Task = (EventTask)2, Opcode = EventOpcode.Stop)]
		public void ParallelInvokeEnd(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)4L))
			{
				base.WriteEvent(4, OriginatingTaskSchedulerID, OriginatingTaskID, ForkJoinContextID);
			}
		}

		// Token: 0x06004299 RID: 17049 RVA: 0x000F94E9 File Offset: 0x000F76E9
		[Event(5, Level = EventLevel.Verbose, ActivityOptions = EventActivityOptions.Recursive, Task = (EventTask)5, Opcode = EventOpcode.Start)]
		public void ParallelFork(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Verbose, (EventKeywords)4L))
			{
				base.WriteEvent(5, OriginatingTaskSchedulerID, OriginatingTaskID, ForkJoinContextID);
			}
		}

		// Token: 0x0600429A RID: 17050 RVA: 0x000F9508 File Offset: 0x000F7708
		[Event(6, Level = EventLevel.Verbose, Task = (EventTask)5, Opcode = EventOpcode.Stop)]
		public void ParallelJoin(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Verbose, (EventKeywords)4L))
			{
				base.WriteEvent(6, OriginatingTaskSchedulerID, OriginatingTaskID, ForkJoinContextID);
			}
		}

		// Token: 0x0600429B RID: 17051 RVA: 0x000F9528 File Offset: 0x000F7728
		[SecuritySafeCritical]
		[Event(7, Task = (EventTask)6, Version = 1, Opcode = EventOpcode.Send, Level = EventLevel.Informational, Keywords = (EventKeywords)3L)]
		public unsafe void TaskScheduled(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID, int CreatingTaskID, int TaskCreationOptions, int appDomain)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)3L))
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)5) * (UIntPtr)sizeof(EventSource.EventData)];
					ptr->Size = 4;
				}
				ptr->DataPointer = (IntPtr)((void*)(&OriginatingTaskSchedulerID));
				ptr[1].Size = 4;
				ptr[1].DataPointer = (IntPtr)((void*)(&OriginatingTaskID));
				ptr[2].Size = 4;
				ptr[2].DataPointer = (IntPtr)((void*)(&TaskID));
				ptr[3].Size = 4;
				ptr[3].DataPointer = (IntPtr)((void*)(&CreatingTaskID));
				ptr[4].Size = 4;
				ptr[4].DataPointer = (IntPtr)((void*)(&TaskCreationOptions));
				if (this.TasksSetActivityIds)
				{
					Guid guid = TplEtwProvider.CreateGuidForTaskID(TaskID);
					base.WriteEventWithRelatedActivityIdCore(7, &guid, 5, ptr);
					return;
				}
				base.WriteEventCore(7, 5, ptr);
			}
		}

		// Token: 0x0600429C RID: 17052 RVA: 0x000F9632 File Offset: 0x000F7832
		[Event(8, Level = EventLevel.Informational, Keywords = (EventKeywords)2L)]
		public void TaskStarted(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID)
		{
			if (base.IsEnabled(EventLevel.Informational, (EventKeywords)2L))
			{
				base.WriteEvent(8, OriginatingTaskSchedulerID, OriginatingTaskID, TaskID);
			}
		}

		// Token: 0x0600429D RID: 17053 RVA: 0x000F964C File Offset: 0x000F784C
		[SecuritySafeCritical]
		[Event(9, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)64L)]
		public unsafe void TaskCompleted(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID, bool IsExceptional)
		{
			if (base.IsEnabled(EventLevel.Informational, (EventKeywords)2L))
			{
				EventSource.EventData* ptr;
				int num;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData)];
					num = (IsExceptional ? 1 : 0);
					ptr->Size = 4;
				}
				ptr->DataPointer = (IntPtr)((void*)(&OriginatingTaskSchedulerID));
				ptr[1].Size = 4;
				ptr[1].DataPointer = (IntPtr)((void*)(&OriginatingTaskID));
				ptr[2].Size = 4;
				ptr[2].DataPointer = (IntPtr)((void*)(&TaskID));
				ptr[3].Size = 4;
				ptr[3].DataPointer = (IntPtr)((void*)(&num));
				base.WriteEventCore(9, 4, ptr);
			}
		}

		// Token: 0x0600429E RID: 17054 RVA: 0x000F9710 File Offset: 0x000F7910
		[SecuritySafeCritical]
		[Event(10, Version = 3, Task = (EventTask)4, Opcode = EventOpcode.Send, Level = EventLevel.Informational, Keywords = (EventKeywords)3L)]
		public unsafe void TaskWaitBegin(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID, TplEtwProvider.TaskWaitBehavior Behavior, int ContinueWithTaskID, int appDomain)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)3L))
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)5) * (UIntPtr)sizeof(EventSource.EventData)];
					ptr->Size = 4;
				}
				ptr->DataPointer = (IntPtr)((void*)(&OriginatingTaskSchedulerID));
				ptr[1].Size = 4;
				ptr[1].DataPointer = (IntPtr)((void*)(&OriginatingTaskID));
				ptr[2].Size = 4;
				ptr[2].DataPointer = (IntPtr)((void*)(&TaskID));
				ptr[3].Size = 4;
				ptr[3].DataPointer = (IntPtr)((void*)(&Behavior));
				ptr[4].Size = 4;
				ptr[4].DataPointer = (IntPtr)((void*)(&ContinueWithTaskID));
				if (this.TasksSetActivityIds)
				{
					Guid guid = TplEtwProvider.CreateGuidForTaskID(TaskID);
					base.WriteEventWithRelatedActivityIdCore(10, &guid, 5, ptr);
					return;
				}
				base.WriteEventCore(10, 5, ptr);
			}
		}

		// Token: 0x0600429F RID: 17055 RVA: 0x000F981C File Offset: 0x000F7A1C
		[Event(11, Level = EventLevel.Verbose, Keywords = (EventKeywords)2L)]
		public void TaskWaitEnd(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Verbose, (EventKeywords)2L))
			{
				base.WriteEvent(11, OriginatingTaskSchedulerID, OriginatingTaskID, TaskID);
			}
		}

		// Token: 0x060042A0 RID: 17056 RVA: 0x000F983C File Offset: 0x000F7A3C
		[Event(13, Level = EventLevel.Verbose, Keywords = (EventKeywords)64L)]
		public void TaskWaitContinuationComplete(int TaskID)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Verbose, (EventKeywords)2L))
			{
				base.WriteEvent(13, TaskID);
			}
		}

		// Token: 0x060042A1 RID: 17057 RVA: 0x000F985A File Offset: 0x000F7A5A
		[Event(19, Level = EventLevel.Verbose, Keywords = (EventKeywords)64L)]
		public void TaskWaitContinuationStarted(int TaskID)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Verbose, (EventKeywords)2L))
			{
				base.WriteEvent(19, TaskID);
			}
		}

		// Token: 0x060042A2 RID: 17058 RVA: 0x000F9878 File Offset: 0x000F7A78
		[SecuritySafeCritical]
		[Event(12, Task = (EventTask)7, Opcode = EventOpcode.Send, Level = EventLevel.Informational, Keywords = (EventKeywords)3L)]
		public unsafe void AwaitTaskContinuationScheduled(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ContinuwWithTaskId)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)3L))
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData)];
					ptr->Size = 4;
				}
				ptr->DataPointer = (IntPtr)((void*)(&OriginatingTaskSchedulerID));
				ptr[1].Size = 4;
				ptr[1].DataPointer = (IntPtr)((void*)(&OriginatingTaskID));
				ptr[2].Size = 4;
				ptr[2].DataPointer = (IntPtr)((void*)(&ContinuwWithTaskId));
				if (this.TasksSetActivityIds)
				{
					Guid guid = TplEtwProvider.CreateGuidForTaskID(ContinuwWithTaskId);
					base.WriteEventWithRelatedActivityIdCore(12, &guid, 3, ptr);
					return;
				}
				base.WriteEventCore(12, 3, ptr);
			}
		}

		// Token: 0x060042A3 RID: 17059 RVA: 0x000F9934 File Offset: 0x000F7B34
		[SecuritySafeCritical]
		[Event(14, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)8L)]
		public unsafe void TraceOperationBegin(int TaskID, string OperationName, long RelatedContext)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)8L))
			{
				fixed (string text = OperationName)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					EventSource.EventData* ptr2;
					checked
					{
						ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData)];
						ptr2->Size = 4;
					}
					ptr2->DataPointer = (IntPtr)((void*)(&TaskID));
					ptr2[1].Size = (OperationName.Length + 1) * 2;
					ptr2[1].DataPointer = (IntPtr)((void*)ptr);
					ptr2[2].Size = 8;
					ptr2[2].DataPointer = (IntPtr)((void*)(&RelatedContext));
					base.WriteEventCore(14, 3, ptr2);
				}
			}
		}

		// Token: 0x060042A4 RID: 17060 RVA: 0x000F99EA File Offset: 0x000F7BEA
		[SecuritySafeCritical]
		[Event(16, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)16L)]
		public void TraceOperationRelation(int TaskID, CausalityRelation Relation)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)16L))
			{
				base.WriteEvent(16, TaskID, (int)Relation);
			}
		}

		// Token: 0x060042A5 RID: 17061 RVA: 0x000F9A0A File Offset: 0x000F7C0A
		[SecuritySafeCritical]
		[Event(15, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)8L)]
		public void TraceOperationEnd(int TaskID, AsyncCausalityStatus Status)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)8L))
			{
				base.WriteEvent(15, TaskID, (int)Status);
			}
		}

		// Token: 0x060042A6 RID: 17062 RVA: 0x000F9A29 File Offset: 0x000F7C29
		[SecuritySafeCritical]
		[Event(17, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)32L)]
		public void TraceSynchronousWorkBegin(int TaskID, CausalitySynchronousWork Work)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)32L))
			{
				base.WriteEvent(17, TaskID, (int)Work);
			}
		}

		// Token: 0x060042A7 RID: 17063 RVA: 0x000F9A4C File Offset: 0x000F7C4C
		[SecuritySafeCritical]
		[Event(18, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)32L)]
		public unsafe void TraceSynchronousWorkEnd(CausalitySynchronousWork Work)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)32L))
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)1) * (UIntPtr)sizeof(EventSource.EventData)];
					ptr->Size = 4;
				}
				ptr->DataPointer = (IntPtr)((void*)(&Work));
				base.WriteEventCore(18, 1, ptr);
			}
		}

		// Token: 0x060042A8 RID: 17064 RVA: 0x000F9A98 File Offset: 0x000F7C98
		[NonEvent]
		[SecuritySafeCritical]
		public unsafe void RunningContinuation(int TaskID, object Object)
		{
			this.RunningContinuation(TaskID, (long)((ulong)(*(IntPtr*)(void*)JitHelpers.UnsafeCastToStackPointer<object>(ref Object))));
		}

		// Token: 0x060042A9 RID: 17065 RVA: 0x000F9AAF File Offset: 0x000F7CAF
		[Event(20, Keywords = (EventKeywords)131072L)]
		private void RunningContinuation(int TaskID, long Object)
		{
			if (this.Debug)
			{
				base.WriteEvent(20, (long)TaskID, Object);
			}
		}

		// Token: 0x060042AA RID: 17066 RVA: 0x000F9AC4 File Offset: 0x000F7CC4
		[NonEvent]
		[SecuritySafeCritical]
		public unsafe void RunningContinuationList(int TaskID, int Index, object Object)
		{
			this.RunningContinuationList(TaskID, Index, (long)((ulong)(*(IntPtr*)(void*)JitHelpers.UnsafeCastToStackPointer<object>(ref Object))));
		}

		// Token: 0x060042AB RID: 17067 RVA: 0x000F9ADC File Offset: 0x000F7CDC
		[Event(21, Keywords = (EventKeywords)131072L)]
		public void RunningContinuationList(int TaskID, int Index, long Object)
		{
			if (this.Debug)
			{
				base.WriteEvent(21, (long)TaskID, (long)Index, Object);
			}
		}

		// Token: 0x060042AC RID: 17068 RVA: 0x000F9AF3 File Offset: 0x000F7CF3
		[Event(22, Keywords = (EventKeywords)131072L)]
		public void DebugMessage(string Message)
		{
			base.WriteEvent(22, Message);
		}

		// Token: 0x060042AD RID: 17069 RVA: 0x000F9AFE File Offset: 0x000F7CFE
		[Event(23, Keywords = (EventKeywords)131072L)]
		public void DebugFacilityMessage(string Facility, string Message)
		{
			base.WriteEvent(23, Facility, Message);
		}

		// Token: 0x060042AE RID: 17070 RVA: 0x000F9B0A File Offset: 0x000F7D0A
		[Event(24, Keywords = (EventKeywords)131072L)]
		public void DebugFacilityMessage1(string Facility, string Message, string Value1)
		{
			base.WriteEvent(24, Facility, Message, Value1);
		}

		// Token: 0x060042AF RID: 17071 RVA: 0x000F9B17 File Offset: 0x000F7D17
		[Event(25, Keywords = (EventKeywords)262144L)]
		public void SetActivityId(Guid NewId)
		{
			if (this.DebugActivityId)
			{
				base.WriteEvent(25, new object[] { NewId });
			}
		}

		// Token: 0x060042B0 RID: 17072 RVA: 0x000F9B38 File Offset: 0x000F7D38
		[Event(26, Keywords = (EventKeywords)131072L)]
		public void NewID(int TaskID)
		{
			if (this.Debug)
			{
				base.WriteEvent(26, TaskID);
			}
		}

		// Token: 0x060042B1 RID: 17073 RVA: 0x000F9B4C File Offset: 0x000F7D4C
		internal static Guid CreateGuidForTaskID(int taskID)
		{
			uint s_currentPid = EventSource.s_currentPid;
			int domainID = Thread.GetDomainID();
			return new Guid(taskID, (short)domainID, (short)(domainID >> 16), (byte)s_currentPid, (byte)(s_currentPid >> 8), (byte)(s_currentPid >> 16), (byte)(s_currentPid >> 24), byte.MaxValue, 220, 215, 181);
		}

		// Token: 0x04001BA8 RID: 7080
		internal bool TasksSetActivityIds;

		// Token: 0x04001BA9 RID: 7081
		internal bool Debug;

		// Token: 0x04001BAA RID: 7082
		private bool DebugActivityId;

		// Token: 0x04001BAB RID: 7083
		public static TplEtwProvider Log = new TplEtwProvider();

		// Token: 0x04001BAC RID: 7084
		private const EventKeywords ALL_KEYWORDS = EventKeywords.All;

		// Token: 0x04001BAD RID: 7085
		private const int PARALLELLOOPBEGIN_ID = 1;

		// Token: 0x04001BAE RID: 7086
		private const int PARALLELLOOPEND_ID = 2;

		// Token: 0x04001BAF RID: 7087
		private const int PARALLELINVOKEBEGIN_ID = 3;

		// Token: 0x04001BB0 RID: 7088
		private const int PARALLELINVOKEEND_ID = 4;

		// Token: 0x04001BB1 RID: 7089
		private const int PARALLELFORK_ID = 5;

		// Token: 0x04001BB2 RID: 7090
		private const int PARALLELJOIN_ID = 6;

		// Token: 0x04001BB3 RID: 7091
		private const int TASKSCHEDULED_ID = 7;

		// Token: 0x04001BB4 RID: 7092
		private const int TASKSTARTED_ID = 8;

		// Token: 0x04001BB5 RID: 7093
		private const int TASKCOMPLETED_ID = 9;

		// Token: 0x04001BB6 RID: 7094
		private const int TASKWAITBEGIN_ID = 10;

		// Token: 0x04001BB7 RID: 7095
		private const int TASKWAITEND_ID = 11;

		// Token: 0x04001BB8 RID: 7096
		private const int AWAITTASKCONTINUATIONSCHEDULED_ID = 12;

		// Token: 0x04001BB9 RID: 7097
		private const int TASKWAITCONTINUATIONCOMPLETE_ID = 13;

		// Token: 0x04001BBA RID: 7098
		private const int TASKWAITCONTINUATIONSTARTED_ID = 19;

		// Token: 0x04001BBB RID: 7099
		private const int TRACEOPERATIONSTART_ID = 14;

		// Token: 0x04001BBC RID: 7100
		private const int TRACEOPERATIONSTOP_ID = 15;

		// Token: 0x04001BBD RID: 7101
		private const int TRACEOPERATIONRELATION_ID = 16;

		// Token: 0x04001BBE RID: 7102
		private const int TRACESYNCHRONOUSWORKSTART_ID = 17;

		// Token: 0x04001BBF RID: 7103
		private const int TRACESYNCHRONOUSWORKSTOP_ID = 18;

		// Token: 0x02000C29 RID: 3113
		public enum ForkJoinOperationType
		{
			// Token: 0x04003702 RID: 14082
			ParallelInvoke = 1,
			// Token: 0x04003703 RID: 14083
			ParallelFor,
			// Token: 0x04003704 RID: 14084
			ParallelForEach
		}

		// Token: 0x02000C2A RID: 3114
		public enum TaskWaitBehavior
		{
			// Token: 0x04003706 RID: 14086
			Synchronous = 1,
			// Token: 0x04003707 RID: 14087
			Asynchronous
		}

		// Token: 0x02000C2B RID: 3115
		public class Tasks
		{
			// Token: 0x04003708 RID: 14088
			public const EventTask Loop = (EventTask)1;

			// Token: 0x04003709 RID: 14089
			public const EventTask Invoke = (EventTask)2;

			// Token: 0x0400370A RID: 14090
			public const EventTask TaskExecute = (EventTask)3;

			// Token: 0x0400370B RID: 14091
			public const EventTask TaskWait = (EventTask)4;

			// Token: 0x0400370C RID: 14092
			public const EventTask ForkJoin = (EventTask)5;

			// Token: 0x0400370D RID: 14093
			public const EventTask TaskScheduled = (EventTask)6;

			// Token: 0x0400370E RID: 14094
			public const EventTask AwaitTaskContinuationScheduled = (EventTask)7;

			// Token: 0x0400370F RID: 14095
			public const EventTask TraceOperation = (EventTask)8;

			// Token: 0x04003710 RID: 14096
			public const EventTask TraceSynchronousWork = (EventTask)9;
		}

		// Token: 0x02000C2C RID: 3116
		public class Keywords
		{
			// Token: 0x04003711 RID: 14097
			public const EventKeywords TaskTransfer = (EventKeywords)1L;

			// Token: 0x04003712 RID: 14098
			public const EventKeywords Tasks = (EventKeywords)2L;

			// Token: 0x04003713 RID: 14099
			public const EventKeywords Parallel = (EventKeywords)4L;

			// Token: 0x04003714 RID: 14100
			public const EventKeywords AsyncCausalityOperation = (EventKeywords)8L;

			// Token: 0x04003715 RID: 14101
			public const EventKeywords AsyncCausalityRelation = (EventKeywords)16L;

			// Token: 0x04003716 RID: 14102
			public const EventKeywords AsyncCausalitySynchronousWork = (EventKeywords)32L;

			// Token: 0x04003717 RID: 14103
			public const EventKeywords TaskStops = (EventKeywords)64L;

			// Token: 0x04003718 RID: 14104
			public const EventKeywords TasksFlowActivityIds = (EventKeywords)128L;

			// Token: 0x04003719 RID: 14105
			public const EventKeywords TasksSetActivityIds = (EventKeywords)65536L;

			// Token: 0x0400371A RID: 14106
			public const EventKeywords Debug = (EventKeywords)131072L;

			// Token: 0x0400371B RID: 14107
			public const EventKeywords DebugActivityId = (EventKeywords)262144L;
		}
	}
}
