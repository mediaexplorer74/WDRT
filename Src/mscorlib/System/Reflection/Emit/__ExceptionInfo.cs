using System;

namespace System.Reflection.Emit
{
	// Token: 0x0200063D RID: 1597
	internal sealed class __ExceptionInfo
	{
		// Token: 0x06004AF6 RID: 19190 RVA: 0x00110BC4 File Offset: 0x0010EDC4
		private __ExceptionInfo()
		{
			this.m_startAddr = 0;
			this.m_filterAddr = null;
			this.m_catchAddr = null;
			this.m_catchEndAddr = null;
			this.m_endAddr = 0;
			this.m_currentCatch = 0;
			this.m_type = null;
			this.m_endFinally = -1;
			this.m_currentState = 0;
		}

		// Token: 0x06004AF7 RID: 19191 RVA: 0x00110C18 File Offset: 0x0010EE18
		internal __ExceptionInfo(int startAddr, Label endLabel)
		{
			this.m_startAddr = startAddr;
			this.m_endAddr = -1;
			this.m_filterAddr = new int[4];
			this.m_catchAddr = new int[4];
			this.m_catchEndAddr = new int[4];
			this.m_catchClass = new Type[4];
			this.m_currentCatch = 0;
			this.m_endLabel = endLabel;
			this.m_type = new int[4];
			this.m_endFinally = -1;
			this.m_currentState = 0;
		}

		// Token: 0x06004AF8 RID: 19192 RVA: 0x00110C94 File Offset: 0x0010EE94
		private static Type[] EnlargeArray(Type[] incoming)
		{
			Type[] array = new Type[incoming.Length * 2];
			Array.Copy(incoming, array, incoming.Length);
			return array;
		}

		// Token: 0x06004AF9 RID: 19193 RVA: 0x00110CB8 File Offset: 0x0010EEB8
		private void MarkHelper(int catchorfilterAddr, int catchEndAddr, Type catchClass, int type)
		{
			if (this.m_currentCatch >= this.m_catchAddr.Length)
			{
				this.m_filterAddr = ILGenerator.EnlargeArray(this.m_filterAddr);
				this.m_catchAddr = ILGenerator.EnlargeArray(this.m_catchAddr);
				this.m_catchEndAddr = ILGenerator.EnlargeArray(this.m_catchEndAddr);
				this.m_catchClass = __ExceptionInfo.EnlargeArray(this.m_catchClass);
				this.m_type = ILGenerator.EnlargeArray(this.m_type);
			}
			if (type == 1)
			{
				this.m_type[this.m_currentCatch] = type;
				this.m_filterAddr[this.m_currentCatch] = catchorfilterAddr;
				this.m_catchAddr[this.m_currentCatch] = -1;
				if (this.m_currentCatch > 0)
				{
					this.m_catchEndAddr[this.m_currentCatch - 1] = catchorfilterAddr;
				}
			}
			else
			{
				this.m_catchClass[this.m_currentCatch] = catchClass;
				if (this.m_type[this.m_currentCatch] != 1)
				{
					this.m_type[this.m_currentCatch] = type;
				}
				this.m_catchAddr[this.m_currentCatch] = catchorfilterAddr;
				if (this.m_currentCatch > 0 && this.m_type[this.m_currentCatch] != 1)
				{
					this.m_catchEndAddr[this.m_currentCatch - 1] = catchEndAddr;
				}
				this.m_catchEndAddr[this.m_currentCatch] = -1;
				this.m_currentCatch++;
			}
			if (this.m_endAddr == -1)
			{
				this.m_endAddr = catchorfilterAddr;
			}
		}

		// Token: 0x06004AFA RID: 19194 RVA: 0x00110E0B File Offset: 0x0010F00B
		internal void MarkFilterAddr(int filterAddr)
		{
			this.m_currentState = 1;
			this.MarkHelper(filterAddr, filterAddr, null, 1);
		}

		// Token: 0x06004AFB RID: 19195 RVA: 0x00110E1E File Offset: 0x0010F01E
		internal void MarkFaultAddr(int faultAddr)
		{
			this.m_currentState = 4;
			this.MarkHelper(faultAddr, faultAddr, null, 4);
		}

		// Token: 0x06004AFC RID: 19196 RVA: 0x00110E31 File Offset: 0x0010F031
		internal void MarkCatchAddr(int catchAddr, Type catchException)
		{
			this.m_currentState = 2;
			this.MarkHelper(catchAddr, catchAddr, catchException, 0);
		}

		// Token: 0x06004AFD RID: 19197 RVA: 0x00110E44 File Offset: 0x0010F044
		internal void MarkFinallyAddr(int finallyAddr, int endCatchAddr)
		{
			if (this.m_endFinally != -1)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_TooManyFinallyClause"));
			}
			this.m_currentState = 3;
			this.m_endFinally = finallyAddr;
			this.MarkHelper(finallyAddr, endCatchAddr, null, 2);
		}

		// Token: 0x06004AFE RID: 19198 RVA: 0x00110E77 File Offset: 0x0010F077
		internal void Done(int endAddr)
		{
			this.m_catchEndAddr[this.m_currentCatch - 1] = endAddr;
			this.m_currentState = 5;
		}

		// Token: 0x06004AFF RID: 19199 RVA: 0x00110E90 File Offset: 0x0010F090
		internal int GetStartAddress()
		{
			return this.m_startAddr;
		}

		// Token: 0x06004B00 RID: 19200 RVA: 0x00110E98 File Offset: 0x0010F098
		internal int GetEndAddress()
		{
			return this.m_endAddr;
		}

		// Token: 0x06004B01 RID: 19201 RVA: 0x00110EA0 File Offset: 0x0010F0A0
		internal int GetFinallyEndAddress()
		{
			return this.m_endFinally;
		}

		// Token: 0x06004B02 RID: 19202 RVA: 0x00110EA8 File Offset: 0x0010F0A8
		internal Label GetEndLabel()
		{
			return this.m_endLabel;
		}

		// Token: 0x06004B03 RID: 19203 RVA: 0x00110EB0 File Offset: 0x0010F0B0
		internal int[] GetFilterAddresses()
		{
			return this.m_filterAddr;
		}

		// Token: 0x06004B04 RID: 19204 RVA: 0x00110EB8 File Offset: 0x0010F0B8
		internal int[] GetCatchAddresses()
		{
			return this.m_catchAddr;
		}

		// Token: 0x06004B05 RID: 19205 RVA: 0x00110EC0 File Offset: 0x0010F0C0
		internal int[] GetCatchEndAddresses()
		{
			return this.m_catchEndAddr;
		}

		// Token: 0x06004B06 RID: 19206 RVA: 0x00110EC8 File Offset: 0x0010F0C8
		internal Type[] GetCatchClass()
		{
			return this.m_catchClass;
		}

		// Token: 0x06004B07 RID: 19207 RVA: 0x00110ED0 File Offset: 0x0010F0D0
		internal int GetNumberOfCatches()
		{
			return this.m_currentCatch;
		}

		// Token: 0x06004B08 RID: 19208 RVA: 0x00110ED8 File Offset: 0x0010F0D8
		internal int[] GetExceptionTypes()
		{
			return this.m_type;
		}

		// Token: 0x06004B09 RID: 19209 RVA: 0x00110EE0 File Offset: 0x0010F0E0
		internal void SetFinallyEndLabel(Label lbl)
		{
			this.m_finallyEndLabel = lbl;
		}

		// Token: 0x06004B0A RID: 19210 RVA: 0x00110EE9 File Offset: 0x0010F0E9
		internal Label GetFinallyEndLabel()
		{
			return this.m_finallyEndLabel;
		}

		// Token: 0x06004B0B RID: 19211 RVA: 0x00110EF4 File Offset: 0x0010F0F4
		internal bool IsInner(__ExceptionInfo exc)
		{
			int num = exc.m_currentCatch - 1;
			int num2 = this.m_currentCatch - 1;
			return exc.m_catchEndAddr[num] < this.m_catchEndAddr[num2] || (exc.m_catchEndAddr[num] == this.m_catchEndAddr[num2] && exc.GetEndAddress() > this.GetEndAddress());
		}

		// Token: 0x06004B0C RID: 19212 RVA: 0x00110F4A File Offset: 0x0010F14A
		internal int GetCurrentState()
		{
			return this.m_currentState;
		}

		// Token: 0x04001EE1 RID: 7905
		internal const int None = 0;

		// Token: 0x04001EE2 RID: 7906
		internal const int Filter = 1;

		// Token: 0x04001EE3 RID: 7907
		internal const int Finally = 2;

		// Token: 0x04001EE4 RID: 7908
		internal const int Fault = 4;

		// Token: 0x04001EE5 RID: 7909
		internal const int PreserveStack = 4;

		// Token: 0x04001EE6 RID: 7910
		internal const int State_Try = 0;

		// Token: 0x04001EE7 RID: 7911
		internal const int State_Filter = 1;

		// Token: 0x04001EE8 RID: 7912
		internal const int State_Catch = 2;

		// Token: 0x04001EE9 RID: 7913
		internal const int State_Finally = 3;

		// Token: 0x04001EEA RID: 7914
		internal const int State_Fault = 4;

		// Token: 0x04001EEB RID: 7915
		internal const int State_Done = 5;

		// Token: 0x04001EEC RID: 7916
		internal int m_startAddr;

		// Token: 0x04001EED RID: 7917
		internal int[] m_filterAddr;

		// Token: 0x04001EEE RID: 7918
		internal int[] m_catchAddr;

		// Token: 0x04001EEF RID: 7919
		internal int[] m_catchEndAddr;

		// Token: 0x04001EF0 RID: 7920
		internal int[] m_type;

		// Token: 0x04001EF1 RID: 7921
		internal Type[] m_catchClass;

		// Token: 0x04001EF2 RID: 7922
		internal Label m_endLabel;

		// Token: 0x04001EF3 RID: 7923
		internal Label m_finallyEndLabel;

		// Token: 0x04001EF4 RID: 7924
		internal int m_endAddr;

		// Token: 0x04001EF5 RID: 7925
		internal int m_endFinally;

		// Token: 0x04001EF6 RID: 7926
		internal int m_currentCatch;

		// Token: 0x04001EF7 RID: 7927
		private int m_currentState;
	}
}
