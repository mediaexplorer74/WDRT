using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008EF RID: 2287
	internal static class AsyncTaskCache
	{
		// Token: 0x06005E44 RID: 24132 RVA: 0x0014C6C8 File Offset: 0x0014A8C8
		private static Task<int>[] CreateInt32Tasks()
		{
			Task<int>[] array = new Task<int>[10];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = AsyncTaskCache.CreateCacheableTask<int>(i + -1);
			}
			return array;
		}

		// Token: 0x06005E45 RID: 24133 RVA: 0x0014C6F8 File Offset: 0x0014A8F8
		internal static Task<TResult> CreateCacheableTask<TResult>(TResult result)
		{
			return new Task<TResult>(false, result, (TaskCreationOptions)16384, default(CancellationToken));
		}

		// Token: 0x04002A56 RID: 10838
		internal static readonly Task<bool> TrueTask = AsyncTaskCache.CreateCacheableTask<bool>(true);

		// Token: 0x04002A57 RID: 10839
		internal static readonly Task<bool> FalseTask = AsyncTaskCache.CreateCacheableTask<bool>(false);

		// Token: 0x04002A58 RID: 10840
		internal static readonly Task<int>[] Int32Tasks = AsyncTaskCache.CreateInt32Tasks();

		// Token: 0x04002A59 RID: 10841
		internal const int INCLUSIVE_INT32_MIN = -1;

		// Token: 0x04002A5A RID: 10842
		internal const int EXCLUSIVE_INT32_MAX = 9;
	}
}
