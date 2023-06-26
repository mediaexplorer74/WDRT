using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000566 RID: 1382
	internal interface ITaskCompletionAction
	{
		// Token: 0x06004175 RID: 16757
		void Invoke(Task completingTask);
	}
}
