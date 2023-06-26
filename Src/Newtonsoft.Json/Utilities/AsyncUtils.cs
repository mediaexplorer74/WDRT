using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000040 RID: 64
	[NullableContext(1)]
	[Nullable(0)]
	internal static class AsyncUtils
	{
		// Token: 0x0600041C RID: 1052 RVA: 0x000100E4 File Offset: 0x0000E2E4
		internal static Task<bool> ToAsync(this bool value)
		{
			if (!value)
			{
				return AsyncUtils.False;
			}
			return AsyncUtils.True;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000100F4 File Offset: 0x0000E2F4
		[NullableContext(2)]
		public static Task CancelIfRequestedAsync(this CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return null;
			}
			return cancellationToken.FromCanceled();
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00010107 File Offset: 0x0000E307
		[NullableContext(2)]
		[return: Nullable(new byte[] { 2, 1 })]
		public static Task<T> CancelIfRequestedAsync<T>(this CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return null;
			}
			return cancellationToken.FromCanceled<T>();
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001011A File Offset: 0x0000E31A
		public static Task FromCanceled(this CancellationToken cancellationToken)
		{
			return new Task(delegate
			{
			}, cancellationToken);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00010144 File Offset: 0x0000E344
		public static Task<T> FromCanceled<[Nullable(2)] T>(this CancellationToken cancellationToken)
		{
			Func<T> func;
			if ((func = AsyncUtils.<>c__6<T>.<>9__6_0) == null)
			{
				Func<T> func2 = (AsyncUtils.<>c__6<T>.<>9__6_0 = () => default(T));
				func = func2;
			}
			return new Task<T>(func, cancellationToken);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00010178 File Offset: 0x0000E378
		public static Task WriteAsync(this TextWriter writer, char value, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return writer.WriteAsync(value);
			}
			return cancellationToken.FromCanceled();
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00010191 File Offset: 0x0000E391
		public static Task WriteAsync(this TextWriter writer, [Nullable(2)] string value, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return writer.WriteAsync(value);
			}
			return cancellationToken.FromCanceled();
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000101AA File Offset: 0x0000E3AA
		public static Task WriteAsync(this TextWriter writer, char[] value, int start, int count, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return writer.WriteAsync(value, start, count);
			}
			return cancellationToken.FromCanceled();
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000101C6 File Offset: 0x0000E3C6
		public static Task<int> ReadAsync(this TextReader reader, char[] buffer, int index, int count, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return reader.ReadAsync(buffer, index, count);
			}
			return cancellationToken.FromCanceled<int>();
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x000101E2 File Offset: 0x0000E3E2
		public static bool IsCompletedSucessfully(this Task task)
		{
			return task.Status == TaskStatus.RanToCompletion;
		}

		// Token: 0x0400014B RID: 331
		public static readonly Task<bool> False = Task.FromResult<bool>(false);

		// Token: 0x0400014C RID: 332
		public static readonly Task<bool> True = Task.FromResult<bool>(true);

		// Token: 0x0400014D RID: 333
		internal static readonly Task CompletedTask = Task.Delay(0);
	}
}
