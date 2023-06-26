using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Principal;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000836 RID: 2102
	internal class CrossAppDomainSink : InternalSink, IMessageSink
	{
		// Token: 0x060059FF RID: 23039 RVA: 0x0013E555 File Offset: 0x0013C755
		internal CrossAppDomainSink(CrossAppDomainData xadData)
		{
			this._xadData = xadData;
		}

		// Token: 0x06005A00 RID: 23040 RVA: 0x0013E564 File Offset: 0x0013C764
		internal static void GrowArrays(int oldSize)
		{
			if (CrossAppDomainSink._sinks == null)
			{
				CrossAppDomainSink._sinks = new CrossAppDomainSink[8];
				CrossAppDomainSink._sinkKeys = new int[8];
				return;
			}
			CrossAppDomainSink[] array = new CrossAppDomainSink[CrossAppDomainSink._sinks.Length + 8];
			int[] array2 = new int[CrossAppDomainSink._sinkKeys.Length + 8];
			Array.Copy(CrossAppDomainSink._sinks, array, CrossAppDomainSink._sinks.Length);
			Array.Copy(CrossAppDomainSink._sinkKeys, array2, CrossAppDomainSink._sinkKeys.Length);
			CrossAppDomainSink._sinks = array;
			CrossAppDomainSink._sinkKeys = array2;
		}

		// Token: 0x06005A01 RID: 23041 RVA: 0x0013E5F4 File Offset: 0x0013C7F4
		internal static CrossAppDomainSink FindOrCreateSink(CrossAppDomainData xadData)
		{
			object obj = CrossAppDomainSink.staticSyncObject;
			CrossAppDomainSink crossAppDomainSink;
			lock (obj)
			{
				int domainID = xadData.DomainID;
				if (CrossAppDomainSink._sinks == null)
				{
					CrossAppDomainSink.GrowArrays(0);
				}
				int num = 0;
				while (CrossAppDomainSink._sinks[num] != null)
				{
					if (CrossAppDomainSink._sinkKeys[num] == domainID)
					{
						return CrossAppDomainSink._sinks[num];
					}
					num++;
					if (num == CrossAppDomainSink._sinks.Length)
					{
						CrossAppDomainSink.GrowArrays(num);
						break;
					}
				}
				CrossAppDomainSink._sinks[num] = new CrossAppDomainSink(xadData);
				CrossAppDomainSink._sinkKeys[num] = domainID;
				crossAppDomainSink = CrossAppDomainSink._sinks[num];
			}
			return crossAppDomainSink;
		}

		// Token: 0x06005A02 RID: 23042 RVA: 0x0013E6AC File Offset: 0x0013C8AC
		internal static void DomainUnloaded(int domainID)
		{
			object obj = CrossAppDomainSink.staticSyncObject;
			lock (obj)
			{
				if (CrossAppDomainSink._sinks != null)
				{
					int num = 0;
					int num2 = -1;
					while (CrossAppDomainSink._sinks[num] != null)
					{
						if (CrossAppDomainSink._sinkKeys[num] == domainID)
						{
							num2 = num;
						}
						num++;
						if (num == CrossAppDomainSink._sinks.Length)
						{
							break;
						}
					}
					if (num2 != -1)
					{
						CrossAppDomainSink._sinkKeys[num2] = CrossAppDomainSink._sinkKeys[num - 1];
						CrossAppDomainSink._sinks[num2] = CrossAppDomainSink._sinks[num - 1];
						CrossAppDomainSink._sinkKeys[num - 1] = 0;
						CrossAppDomainSink._sinks[num - 1] = null;
					}
				}
			}
		}

		// Token: 0x06005A03 RID: 23043 RVA: 0x0013E770 File Offset: 0x0013C970
		[SecurityCritical]
		internal static byte[] DoDispatch(byte[] reqStmBuff, SmuggledMethodCallMessage smuggledMcm, out SmuggledMethodReturnMessage smuggledMrm)
		{
			IMessage message;
			if (smuggledMcm != null)
			{
				ArrayList arrayList = smuggledMcm.FixupForNewAppDomain();
				message = new MethodCall(smuggledMcm, arrayList);
			}
			else
			{
				MemoryStream memoryStream = new MemoryStream(reqStmBuff);
				message = CrossAppDomainSerializer.DeserializeMessage(memoryStream);
			}
			LogicalCallContext logicalCallContext = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
			logicalCallContext.SetData("__xADCall", true);
			IMessage message2 = ChannelServices.SyncDispatchMessage(message);
			logicalCallContext.FreeNamedDataSlot("__xADCall");
			smuggledMrm = SmuggledMethodReturnMessage.SmuggleIfPossible(message2);
			if (smuggledMrm != null)
			{
				return null;
			}
			if (message2 != null)
			{
				LogicalCallContext logicalCallContext2 = (LogicalCallContext)message2.Properties[Message.CallContextKey];
				if (logicalCallContext2 != null && logicalCallContext2.Principal != null)
				{
					logicalCallContext2.Principal = null;
				}
				return CrossAppDomainSerializer.SerializeMessage(message2).GetBuffer();
			}
			return null;
		}

		// Token: 0x06005A04 RID: 23044 RVA: 0x0013E820 File Offset: 0x0013CA20
		[SecurityCritical]
		internal static object DoTransitionDispatchCallback(object[] args)
		{
			byte[] array = (byte[])args[0];
			SmuggledMethodCallMessage smuggledMethodCallMessage = (SmuggledMethodCallMessage)args[1];
			SmuggledMethodReturnMessage smuggledMethodReturnMessage = null;
			byte[] array2 = null;
			try
			{
				array2 = CrossAppDomainSink.DoDispatch(array, smuggledMethodCallMessage, out smuggledMethodReturnMessage);
			}
			catch (Exception ex)
			{
				IMessage message = new ReturnMessage(ex, new ErrorMessage());
				array2 = CrossAppDomainSerializer.SerializeMessage(message).GetBuffer();
			}
			args[2] = smuggledMethodReturnMessage;
			return array2;
		}

		// Token: 0x06005A05 RID: 23045 RVA: 0x0013E888 File Offset: 0x0013CA88
		[SecurityCritical]
		internal byte[] DoTransitionDispatch(byte[] reqStmBuff, SmuggledMethodCallMessage smuggledMcm, out SmuggledMethodReturnMessage smuggledMrm)
		{
			object[] array = new object[3];
			array[0] = reqStmBuff;
			array[1] = smuggledMcm;
			object[] array2 = array;
			byte[] array3 = (byte[])Thread.CurrentThread.InternalCrossContextCallback(null, this._xadData.ContextID, this._xadData.DomainID, CrossAppDomainSink.s_xctxDel, array2);
			smuggledMrm = (SmuggledMethodReturnMessage)array2[2];
			return array3;
		}

		// Token: 0x06005A06 RID: 23046 RVA: 0x0013E8E0 File Offset: 0x0013CAE0
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			IMessage message = InternalSink.ValidateMessage(reqMsg);
			if (message != null)
			{
				return message;
			}
			IPrincipal principal = null;
			IMessage message2 = null;
			try
			{
				IMethodCallMessage methodCallMessage = reqMsg as IMethodCallMessage;
				if (methodCallMessage != null)
				{
					LogicalCallContext logicalCallContext = methodCallMessage.LogicalCallContext;
					if (logicalCallContext != null)
					{
						principal = logicalCallContext.RemovePrincipalIfNotSerializable();
					}
				}
				MemoryStream memoryStream = null;
				SmuggledMethodCallMessage smuggledMethodCallMessage = SmuggledMethodCallMessage.SmuggleIfPossible(reqMsg);
				if (smuggledMethodCallMessage == null)
				{
					memoryStream = CrossAppDomainSerializer.SerializeMessage(reqMsg);
				}
				LogicalCallContext logicalCallContext2 = CallContext.SetLogicalCallContext(null);
				byte[] array = null;
				SmuggledMethodReturnMessage smuggledMethodReturnMessage;
				try
				{
					if (smuggledMethodCallMessage != null)
					{
						array = this.DoTransitionDispatch(null, smuggledMethodCallMessage, out smuggledMethodReturnMessage);
					}
					else
					{
						array = this.DoTransitionDispatch(memoryStream.GetBuffer(), null, out smuggledMethodReturnMessage);
					}
				}
				finally
				{
					CallContext.SetLogicalCallContext(logicalCallContext2);
				}
				if (smuggledMethodReturnMessage != null)
				{
					ArrayList arrayList = smuggledMethodReturnMessage.FixupForNewAppDomain();
					message2 = new MethodResponse((IMethodCallMessage)reqMsg, smuggledMethodReturnMessage, arrayList);
				}
				else if (array != null)
				{
					MemoryStream memoryStream2 = new MemoryStream(array);
					message2 = CrossAppDomainSerializer.DeserializeMessage(memoryStream2, reqMsg as IMethodCallMessage);
				}
			}
			catch (Exception ex)
			{
				try
				{
					message2 = new ReturnMessage(ex, reqMsg as IMethodCallMessage);
				}
				catch (Exception)
				{
				}
			}
			if (principal != null)
			{
				IMethodReturnMessage methodReturnMessage = message2 as IMethodReturnMessage;
				if (methodReturnMessage != null)
				{
					LogicalCallContext logicalCallContext3 = methodReturnMessage.LogicalCallContext;
					logicalCallContext3.Principal = principal;
				}
			}
			return message2;
		}

		// Token: 0x06005A07 RID: 23047 RVA: 0x0013EA0C File Offset: 0x0013CC0C
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			ADAsyncWorkItem adasyncWorkItem = new ADAsyncWorkItem(reqMsg, this, replySink);
			WaitCallback waitCallback = new WaitCallback(adasyncWorkItem.FinishAsyncWork);
			ThreadPool.QueueUserWorkItem(waitCallback);
			return null;
		}

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06005A08 RID: 23048 RVA: 0x0013EA38 File Offset: 0x0013CC38
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x040028F4 RID: 10484
		internal const int GROW_BY = 8;

		// Token: 0x040028F5 RID: 10485
		internal static volatile int[] _sinkKeys;

		// Token: 0x040028F6 RID: 10486
		internal static volatile CrossAppDomainSink[] _sinks;

		// Token: 0x040028F7 RID: 10487
		internal const string LCC_DATA_KEY = "__xADCall";

		// Token: 0x040028F8 RID: 10488
		private static object staticSyncObject = new object();

		// Token: 0x040028F9 RID: 10489
		private static InternalCrossContextDelegate s_xctxDel = new InternalCrossContextDelegate(CrossAppDomainSink.DoTransitionDispatchCallback);

		// Token: 0x040028FA RID: 10490
		internal CrossAppDomainData _xadData;
	}
}
