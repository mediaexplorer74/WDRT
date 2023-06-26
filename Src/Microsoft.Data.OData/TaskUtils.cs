using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Data.OData
{
	// Token: 0x020002B1 RID: 689
	internal static class TaskUtils
	{
		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001763 RID: 5987 RVA: 0x00054114 File Offset: 0x00052314
		internal static Task CompletedTask
		{
			get
			{
				if (TaskUtils.completedTask == null)
				{
					TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>();
					taskCompletionSource.SetResult(null);
					TaskUtils.completedTask = taskCompletionSource.Task;
				}
				return TaskUtils.completedTask;
			}
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x00054148 File Offset: 0x00052348
		internal static Task<T> GetCompletedTask<T>(T value)
		{
			TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();
			taskCompletionSource.SetResult(value);
			return taskCompletionSource.Task;
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x00054168 File Offset: 0x00052368
		internal static Task GetFaultedTask(Exception exception)
		{
			return TaskUtils.GetFaultedTask<object>(exception);
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x00054170 File Offset: 0x00052370
		internal static Task<T> GetFaultedTask<T>(Exception exception)
		{
			TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();
			taskCompletionSource.SetException(exception);
			return taskCompletionSource.Task;
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x00054190 File Offset: 0x00052390
		internal static Task GetTaskForSynchronousOperation(Action synchronousOperation)
		{
			Task faultedTask;
			try
			{
				synchronousOperation();
				faultedTask = TaskUtils.CompletedTask;
			}
			catch (Exception ex)
			{
				if (!ExceptionUtils.IsCatchableExceptionType(ex))
				{
					throw;
				}
				faultedTask = TaskUtils.GetFaultedTask(ex);
			}
			return faultedTask;
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x000541D0 File Offset: 0x000523D0
		internal static Task<T> GetTaskForSynchronousOperation<T>(Func<T> synchronousOperation)
		{
			Task<T> faultedTask;
			try
			{
				T t = synchronousOperation();
				faultedTask = TaskUtils.GetCompletedTask<T>(t);
			}
			catch (Exception ex)
			{
				if (!ExceptionUtils.IsCatchableExceptionType(ex))
				{
					throw;
				}
				faultedTask = TaskUtils.GetFaultedTask<T>(ex);
			}
			return faultedTask;
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x00054214 File Offset: 0x00052414
		internal static Task GetTaskForSynchronousOperationReturningTask(Func<Task> synchronousOperation)
		{
			Task task;
			try
			{
				task = synchronousOperation();
			}
			catch (Exception ex)
			{
				if (!ExceptionUtils.IsCatchableExceptionType(ex))
				{
					throw;
				}
				task = TaskUtils.GetFaultedTask(ex);
			}
			return task;
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x00054250 File Offset: 0x00052450
		internal static Task<TResult> GetTaskForSynchronousOperationReturningTask<TResult>(Func<Task<TResult>> synchronousOperation)
		{
			Task<TResult> task;
			try
			{
				task = synchronousOperation();
			}
			catch (Exception ex)
			{
				if (!ExceptionUtils.IsCatchableExceptionType(ex))
				{
					throw;
				}
				task = TaskUtils.GetFaultedTask<TResult>(ex);
			}
			return task;
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x000542A4 File Offset: 0x000524A4
		internal static Task FollowOnSuccessWith(this Task antecedentTask, Action<Task> operation)
		{
			return TaskUtils.FollowOnSuccessWithImplementation<object>(antecedentTask, delegate(Task t)
			{
				operation(t);
				return null;
			});
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x000542D0 File Offset: 0x000524D0
		internal static Task<TFollowupTaskResult> FollowOnSuccessWith<TFollowupTaskResult>(this Task antecedentTask, Func<Task, TFollowupTaskResult> operation)
		{
			return TaskUtils.FollowOnSuccessWithImplementation<TFollowupTaskResult>(antecedentTask, operation);
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x000542F8 File Offset: 0x000524F8
		internal static Task FollowOnSuccessWith<TAntecedentTaskResult>(this Task<TAntecedentTaskResult> antecedentTask, Action<Task<TAntecedentTaskResult>> operation)
		{
			return TaskUtils.FollowOnSuccessWithImplementation<object>(antecedentTask, delegate(Task t)
			{
				operation((Task<TAntecedentTaskResult>)t);
				return null;
			});
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x00054340 File Offset: 0x00052540
		internal static Task<TFollowupTaskResult> FollowOnSuccessWith<TAntecedentTaskResult, TFollowupTaskResult>(this Task<TAntecedentTaskResult> antecedentTask, Func<Task<TAntecedentTaskResult>, TFollowupTaskResult> operation)
		{
			return TaskUtils.FollowOnSuccessWithImplementation<TFollowupTaskResult>(antecedentTask, (Task t) => operation((Task<TAntecedentTaskResult>)t));
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x00054388 File Offset: 0x00052588
		internal static Task FollowOnSuccessWithTask(this Task antecedentTask, Func<Task, Task> operation)
		{
			TaskCompletionSource<Task> taskCompletionSource = new TaskCompletionSource<Task>();
			antecedentTask.ContinueWith(delegate(Task taskToContinueOn)
			{
				TaskUtils.FollowOnSuccessWithContinuation<Task>(taskToContinueOn, taskCompletionSource, operation);
			}, TaskContinuationOptions.ExecuteSynchronously);
			return taskCompletionSource.Task.Unwrap();
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x000543F4 File Offset: 0x000525F4
		internal static Task<TFollowupTaskResult> FollowOnSuccessWithTask<TFollowupTaskResult>(this Task antecedentTask, Func<Task, Task<TFollowupTaskResult>> operation)
		{
			TaskCompletionSource<Task<TFollowupTaskResult>> taskCompletionSource = new TaskCompletionSource<Task<TFollowupTaskResult>>();
			antecedentTask.ContinueWith(delegate(Task taskToContinueOn)
			{
				TaskUtils.FollowOnSuccessWithContinuation<Task<TFollowupTaskResult>>(taskToContinueOn, taskCompletionSource, operation);
			}, TaskContinuationOptions.ExecuteSynchronously);
			return taskCompletionSource.Task.Unwrap<TFollowupTaskResult>();
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x00054478 File Offset: 0x00052678
		internal static Task FollowOnSuccessWithTask<TAntecedentTaskResult>(this Task<TAntecedentTaskResult> antecedentTask, Func<Task<TAntecedentTaskResult>, Task> operation)
		{
			TaskCompletionSource<Task> taskCompletionSource = new TaskCompletionSource<Task>();
			antecedentTask.ContinueWith(delegate(Task<TAntecedentTaskResult> taskToContinueOn)
			{
				TaskUtils.FollowOnSuccessWithContinuation<Task>(taskToContinueOn, taskCompletionSource, (Task taskForOperation) => operation((Task<TAntecedentTaskResult>)taskForOperation));
			}, TaskContinuationOptions.ExecuteSynchronously);
			return taskCompletionSource.Task.Unwrap();
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x000544FC File Offset: 0x000526FC
		internal static Task<TFollowupTaskResult> FollowOnSuccessWithTask<TAntecedentTaskResult, TFollowupTaskResult>(this Task<TAntecedentTaskResult> antecedentTask, Func<Task<TAntecedentTaskResult>, Task<TFollowupTaskResult>> operation)
		{
			TaskCompletionSource<Task<TFollowupTaskResult>> taskCompletionSource = new TaskCompletionSource<Task<TFollowupTaskResult>>();
			antecedentTask.ContinueWith(delegate(Task<TAntecedentTaskResult> taskToContinueOn)
			{
				TaskUtils.FollowOnSuccessWithContinuation<Task<TFollowupTaskResult>>(taskToContinueOn, taskCompletionSource, (Task taskForOperation) => operation((Task<TAntecedentTaskResult>)taskForOperation));
			}, TaskContinuationOptions.ExecuteSynchronously);
			return taskCompletionSource.Task.Unwrap<TFollowupTaskResult>();
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x0005454C File Offset: 0x0005274C
		internal static Task FollowOnFaultWith(this Task antecedentTask, Action<Task> operation)
		{
			return TaskUtils.FollowOnFaultWithImplementation<object>(antecedentTask, (Task t) => null, operation);
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x0005459C File Offset: 0x0005279C
		internal static Task<TResult> FollowOnFaultWith<TResult>(this Task<TResult> antecedentTask, Action<Task<TResult>> operation)
		{
			return TaskUtils.FollowOnFaultWithImplementation<TResult>(antecedentTask, (Task t) => ((Task<TResult>)t).Result, delegate(Task t)
			{
				operation((Task<TResult>)t);
			});
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x000545E1 File Offset: 0x000527E1
		internal static Task<TResult> FollowOnFaultAndCatchExceptionWith<TResult, TExceptionType>(this Task<TResult> antecedentTask, Func<TExceptionType, TResult> catchBlock) where TExceptionType : Exception
		{
			return TaskUtils.FollowOnFaultAndCatchExceptionWithImplementation<TResult, TExceptionType>(antecedentTask, (Task t) => ((Task<TResult>)t).Result, catchBlock);
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x000545F9 File Offset: 0x000527F9
		internal static Task FollowAlwaysWith(this Task antecedentTask, Action<Task> operation)
		{
			return antecedentTask.FollowAlwaysWithImplementation((Task t) => null, operation);
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x00054648 File Offset: 0x00052848
		internal static Task<TResult> FollowAlwaysWith<TResult>(this Task<TResult> antecedentTask, Action<Task<TResult>> operation)
		{
			return antecedentTask.FollowAlwaysWithImplementation((Task t) => ((Task<TResult>)t).Result, delegate(Task t)
			{
				operation((Task<TResult>)t);
			});
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x00054689 File Offset: 0x00052889
		internal static Task IgnoreExceptions(this Task task)
		{
			task.ContinueWith(delegate(Task t)
			{
				AggregateException exception = t.Exception;
			}, CancellationToken.None, TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnCanceled | TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
			return task;
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x000546BF File Offset: 0x000528BF
		internal static TaskScheduler GetTargetScheduler(this TaskFactory factory)
		{
			return factory.Scheduler ?? TaskScheduler.Current;
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x000547C0 File Offset: 0x000529C0
		internal static Task Iterate(this TaskFactory factory, IEnumerable<Task> source)
		{
			IEnumerator<Task> enumerator = source.GetEnumerator();
			TaskCompletionSource<object> trc = new TaskCompletionSource<object>(null, factory.CreationOptions);
			trc.Task.ContinueWith(delegate(Task<object> _)
			{
				enumerator.Dispose();
			}, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
			Action<Task> recursiveBody = null;
			recursiveBody = delegate(Task antecedent)
			{
				try
				{
					if (antecedent != null && antecedent.IsFaulted)
					{
						trc.TrySetException(antecedent.Exception);
					}
					else if (enumerator.MoveNext())
					{
						Task task = enumerator.Current;
						task.ContinueWith(recursiveBody).IgnoreExceptions();
					}
					else
					{
						trc.TrySetResult(null);
					}
				}
				catch (Exception ex)
				{
					if (!ExceptionUtils.IsCatchableExceptionType(ex))
					{
						throw;
					}
					OperationCanceledException ex2 = ex as OperationCanceledException;
					if (ex2 != null && ex2.CancellationToken == factory.CancellationToken)
					{
						trc.TrySetCanceled();
					}
					else
					{
						trc.TrySetException(ex);
					}
				}
			};
			factory.StartNew(delegate
			{
				recursiveBody(null);
			}, CancellationToken.None, TaskCreationOptions.None, factory.GetTargetScheduler()).IgnoreExceptions();
			return trc.Task;
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x0005487C File Offset: 0x00052A7C
		private static void FollowOnSuccessWithContinuation<TResult>(Task antecedentTask, TaskCompletionSource<TResult> taskCompletionSource, Func<Task, TResult> operation)
		{
			switch (antecedentTask.Status)
			{
			case TaskStatus.RanToCompletion:
				try
				{
					taskCompletionSource.TrySetResult(operation(antecedentTask));
					return;
				}
				catch (Exception ex)
				{
					if (!ExceptionUtils.IsCatchableExceptionType(ex))
					{
						throw;
					}
					taskCompletionSource.TrySetException(ex);
					return;
				}
				break;
			case TaskStatus.Canceled:
				taskCompletionSource.TrySetCanceled();
				return;
			case TaskStatus.Faulted:
				break;
			default:
				return;
			}
			taskCompletionSource.TrySetException(antecedentTask.Exception);
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x0005490C File Offset: 0x00052B0C
		private static Task<TResult> FollowOnSuccessWithImplementation<TResult>(Task antecedentTask, Func<Task, TResult> operation)
		{
			TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
			antecedentTask.ContinueWith(delegate(Task taskToContinueOn)
			{
				TaskUtils.FollowOnSuccessWithContinuation<TResult>(taskToContinueOn, taskCompletionSource, operation);
			}, TaskContinuationOptions.ExecuteSynchronously).IgnoreExceptions();
			return taskCompletionSource.Task;
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x00054A18 File Offset: 0x00052C18
		private static Task<TResult> FollowOnFaultWithImplementation<TResult>(Task antecedentTask, Func<Task, TResult> getTaskResult, Action<Task> operation)
		{
			TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
			antecedentTask.ContinueWith(delegate(Task t)
			{
				switch (t.Status)
				{
				case TaskStatus.RanToCompletion:
					taskCompletionSource.TrySetResult(getTaskResult(t));
					return;
				case TaskStatus.Canceled:
					break;
				case TaskStatus.Faulted:
					try
					{
						operation(t);
						taskCompletionSource.TrySetException(t.Exception);
						return;
					}
					catch (Exception ex)
					{
						if (!ExceptionUtils.IsCatchableExceptionType(ex))
						{
							throw;
						}
						AggregateException ex2 = new AggregateException(new Exception[] { t.Exception, ex });
						taskCompletionSource.TrySetException(ex2);
						return;
					}
					break;
				default:
					return;
				}
				taskCompletionSource.TrySetCanceled();
			}, TaskContinuationOptions.ExecuteSynchronously).IgnoreExceptions();
			return taskCompletionSource.Task;
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x00054B70 File Offset: 0x00052D70
		private static Task<TResult> FollowOnFaultAndCatchExceptionWithImplementation<TResult, TExceptionType>(Task antecedentTask, Func<Task, TResult> getTaskResult, Func<TExceptionType, TResult> catchBlock) where TExceptionType : Exception
		{
			TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
			antecedentTask.ContinueWith(delegate(Task t)
			{
				switch (t.Status)
				{
				case TaskStatus.RanToCompletion:
					taskCompletionSource.TrySetResult(getTaskResult(t));
					return;
				case TaskStatus.Canceled:
					taskCompletionSource.TrySetCanceled();
					break;
				case TaskStatus.Faulted:
				{
					Exception ex = t.Exception;
					AggregateException ex2 = ex as AggregateException;
					if (ex2 != null)
					{
						ex2 = ex2.Flatten();
						if (ex2.InnerExceptions.Count == 1)
						{
							ex = ex2.InnerExceptions[0];
						}
					}
					if (ex is TExceptionType)
					{
						try
						{
							taskCompletionSource.TrySetResult(catchBlock((TExceptionType)((object)ex)));
							break;
						}
						catch (Exception ex3)
						{
							if (!ExceptionUtils.IsCatchableExceptionType(ex3))
							{
								throw;
							}
							AggregateException ex4 = new AggregateException(new Exception[] { ex, ex3 });
							taskCompletionSource.TrySetException(ex4);
							break;
						}
					}
					taskCompletionSource.TrySetException(ex);
					return;
				}
				default:
					return;
				}
			}, TaskContinuationOptions.ExecuteSynchronously).IgnoreExceptions();
			return taskCompletionSource.Task;
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x00054CA0 File Offset: 0x00052EA0
		private static Task<TResult> FollowAlwaysWithImplementation<TResult>(this Task antecedentTask, Func<Task, TResult> getTaskResult, Action<Task> operation)
		{
			TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
			antecedentTask.ContinueWith(delegate(Task t)
			{
				Exception ex = null;
				try
				{
					operation(t);
				}
				catch (Exception ex2)
				{
					if (!ExceptionUtils.IsCatchableExceptionType(ex2))
					{
						throw;
					}
					ex = ex2;
				}
				switch (t.Status)
				{
				case TaskStatus.RanToCompletion:
					if (ex != null)
					{
						taskCompletionSource.TrySetException(ex);
						return;
					}
					taskCompletionSource.TrySetResult(getTaskResult(t));
					return;
				case TaskStatus.Canceled:
					if (ex != null)
					{
						taskCompletionSource.TrySetException(ex);
						return;
					}
					taskCompletionSource.TrySetCanceled();
					return;
				case TaskStatus.Faulted:
				{
					Exception ex3 = t.Exception;
					if (ex != null)
					{
						ex3 = new AggregateException(new Exception[] { ex3, ex });
					}
					taskCompletionSource.TrySetException(ex3);
					return;
				}
				default:
					return;
				}
			}, TaskContinuationOptions.ExecuteSynchronously).IgnoreExceptions();
			return taskCompletionSource.Task;
		}

		// Token: 0x040009AC RID: 2476
		private static Task completedTask;
	}
}
