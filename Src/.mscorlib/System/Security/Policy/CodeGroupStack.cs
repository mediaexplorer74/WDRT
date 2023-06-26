using System;
using System.Collections;

namespace System.Security.Policy
{
	// Token: 0x02000365 RID: 869
	internal sealed class CodeGroupStack
	{
		// Token: 0x06002B2C RID: 11052 RVA: 0x000A217F File Offset: 0x000A037F
		internal CodeGroupStack()
		{
			this.m_array = new ArrayList();
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x000A2192 File Offset: 0x000A0392
		internal void Push(CodeGroupStackFrame element)
		{
			this.m_array.Add(element);
		}

		// Token: 0x06002B2E RID: 11054 RVA: 0x000A21A4 File Offset: 0x000A03A4
		internal CodeGroupStackFrame Pop()
		{
			if (this.IsEmpty())
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EmptyStack"));
			}
			int count = this.m_array.Count;
			CodeGroupStackFrame codeGroupStackFrame = (CodeGroupStackFrame)this.m_array[count - 1];
			this.m_array.RemoveAt(count - 1);
			return codeGroupStackFrame;
		}

		// Token: 0x06002B2F RID: 11055 RVA: 0x000A21F8 File Offset: 0x000A03F8
		internal bool IsEmpty()
		{
			return this.m_array.Count == 0;
		}

		// Token: 0x04001197 RID: 4503
		private ArrayList m_array;
	}
}
