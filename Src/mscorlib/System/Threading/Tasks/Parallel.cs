using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
	/// <summary>Provides support for parallel loops and regions.</summary>
	// Token: 0x0200054F RID: 1359
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public static class Parallel
	{
		/// <summary>Executes each of the provided actions, possibly in parallel.</summary>
		/// <param name="actions">An array of <see cref="T:System.Action" /> to execute.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="actions" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that is thrown when any action in the <paramref name="actions" /> array throws an exception.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="actions" /> array contains a <see langword="null" /> element.</exception>
		// Token: 0x0600402D RID: 16429 RVA: 0x000EFAE0 File Offset: 0x000EDCE0
		[__DynamicallyInvokable]
		public static void Invoke(params Action[] actions)
		{
			Parallel.Invoke(Parallel.s_defaultParallelOptions, actions);
		}

		/// <summary>Executes each of the provided actions, possibly in parallel, unless the operation is cancelled by the user.</summary>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="actions">An array of actions to execute.</param>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> is set.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="actions" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that is thrown when any action in the <paramref name="actions" /> array throws an exception.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="actions" /> array contains a <see langword="null" /> element.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x0600402E RID: 16430 RVA: 0x000EFAF0 File Offset: 0x000EDCF0
		[__DynamicallyInvokable]
		public static void Invoke(ParallelOptions parallelOptions, params Action[] actions)
		{
			if (actions == null)
			{
				throw new ArgumentNullException("actions");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			if (parallelOptions.CancellationToken.CanBeCanceled && AppContextSwitches.ThrowExceptionIfDisposedCancellationTokenSource)
			{
				parallelOptions.CancellationToken.ThrowIfSourceDisposed();
			}
			if (parallelOptions.CancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException(parallelOptions.CancellationToken);
			}
			Action[] actionsCopy = new Action[actions.Length];
			for (int i = 0; i < actionsCopy.Length; i++)
			{
				actionsCopy[i] = actions[i];
				if (actionsCopy[i] == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Parallel_Invoke_ActionNull"));
				}
			}
			int num = 0;
			Task task = null;
			if (TplEtwProvider.Log.IsEnabled())
			{
				num = Interlocked.Increment(ref Parallel.s_forkJoinContextID);
				task = Task.InternalCurrent;
				TplEtwProvider.Log.ParallelInvokeBegin((task != null) ? task.m_taskScheduler.Id : TaskScheduler.Current.Id, (task != null) ? task.Id : 0, num, TplEtwProvider.ForkJoinOperationType.ParallelInvoke, actionsCopy.Length);
			}
			if (actionsCopy.Length < 1)
			{
				return;
			}
			try
			{
				if (actionsCopy.Length > 10 || (parallelOptions.MaxDegreeOfParallelism != -1 && parallelOptions.MaxDegreeOfParallelism < actionsCopy.Length))
				{
					ConcurrentQueue<Exception> exceptionQ = null;
					try
					{
						int actionIndex = 0;
						ParallelForReplicatingTask parallelForReplicatingTask = new ParallelForReplicatingTask(parallelOptions, delegate
						{
							for (int l = Interlocked.Increment(ref actionIndex); l <= actionsCopy.Length; l = Interlocked.Increment(ref actionIndex))
							{
								try
								{
									actionsCopy[l - 1]();
								}
								catch (Exception ex5)
								{
									LazyInitializer.EnsureInitialized<ConcurrentQueue<Exception>>(ref exceptionQ, () => new ConcurrentQueue<Exception>());
									exceptionQ.Enqueue(ex5);
								}
								if (parallelOptions.CancellationToken.IsCancellationRequested)
								{
									throw new OperationCanceledException(parallelOptions.CancellationToken);
								}
							}
						}, TaskCreationOptions.None, InternalTaskOptions.SelfReplicating);
						parallelForReplicatingTask.RunSynchronously(parallelOptions.EffectiveTaskScheduler);
						parallelForReplicatingTask.Wait();
					}
					catch (Exception ex)
					{
						LazyInitializer.EnsureInitialized<ConcurrentQueue<Exception>>(ref exceptionQ, () => new ConcurrentQueue<Exception>());
						AggregateException ex2 = ex as AggregateException;
						if (ex2 != null)
						{
							using (IEnumerator<Exception> enumerator = ex2.InnerExceptions.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									Exception ex3 = enumerator.Current;
									exceptionQ.Enqueue(ex3);
								}
								goto IL_23C;
							}
						}
						exceptionQ.Enqueue(ex);
						IL_23C:;
					}
					if (exceptionQ != null && exceptionQ.Count > 0)
					{
						Parallel.ThrowIfReducableToSingleOCE(exceptionQ, parallelOptions.CancellationToken);
						throw new AggregateException(exceptionQ);
					}
				}
				else
				{
					Task[] array = new Task[actionsCopy.Length];
					if (parallelOptions.CancellationToken.IsCancellationRequested)
					{
						throw new OperationCanceledException(parallelOptions.CancellationToken);
					}
					for (int j = 1; j < array.Length; j++)
					{
						array[j] = Task.Factory.StartNew(actionsCopy[j], parallelOptions.CancellationToken, TaskCreationOptions.None, InternalTaskOptions.None, parallelOptions.EffectiveTaskScheduler);
					}
					array[0] = new Task(actionsCopy[0]);
					array[0].RunSynchronously(parallelOptions.EffectiveTaskScheduler);
					try
					{
						if (array.Length <= 4)
						{
							Task.FastWaitAll(array);
						}
						else
						{
							Task.WaitAll(array);
						}
					}
					catch (AggregateException ex4)
					{
						Parallel.ThrowIfReducableToSingleOCE(ex4.InnerExceptions, parallelOptions.CancellationToken);
						throw;
					}
					finally
					{
						for (int k = 0; k < array.Length; k++)
						{
							if (array[k].IsCompleted)
							{
								array[k].Dispose();
							}
						}
					}
				}
			}
			finally
			{
				if (TplEtwProvider.Log.IsEnabled())
				{
					TplEtwProvider.Log.ParallelInvokeEnd((task != null) ? task.m_taskScheduler.Id : TaskScheduler.Current.Id, (task != null) ? task.Id : 0, num);
				}
			}
		}

		/// <summary>Executes a <see langword="for" /> (<see langword="For" /> in Visual Basic) loop in which iterations may run in parallel.</summary>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x0600402F RID: 16431 RVA: 0x000EFF34 File Offset: 0x000EE134
		[__DynamicallyInvokable]
		public static ParallelLoopResult For(int fromInclusive, int toExclusive, Action<int> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForWorker<object>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, body, null, null, null, null);
		}

		/// <summary>Executes a <see langword="for" /> (<see langword="For" /> in Visual Basic) loop with 64-bit indexes in which iterations may run in parallel.</summary>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x06004030 RID: 16432 RVA: 0x000EFF55 File Offset: 0x000EE155
		[__DynamicallyInvokable]
		public static ParallelLoopResult For(long fromInclusive, long toExclusive, Action<long> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForWorker64<object>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, body, null, null, null, null);
		}

		/// <summary>Executes a <see langword="for" /> (<see langword="For" /> in Visual Basic) loop in which iterations may run in parallel and loop options can be configured.</summary>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <returns>A  structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x06004031 RID: 16433 RVA: 0x000EFF76 File Offset: 0x000EE176
		[__DynamicallyInvokable]
		public static ParallelLoopResult For(int fromInclusive, int toExclusive, ParallelOptions parallelOptions, Action<int> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker<object>(fromInclusive, toExclusive, parallelOptions, body, null, null, null, null);
		}

		/// <summary>Executes a <see langword="for" /> (<see langword="For" /> in Visual Basic) loop with 64-bit indexes in which iterations may run in parallel and loop options can be configured.</summary>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x06004032 RID: 16434 RVA: 0x000EFFA1 File Offset: 0x000EE1A1
		[__DynamicallyInvokable]
		public static ParallelLoopResult For(long fromInclusive, long toExclusive, ParallelOptions parallelOptions, Action<long> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker64<object>(fromInclusive, toExclusive, parallelOptions, body, null, null, null, null);
		}

		/// <summary>Executes a <see langword="for" /> (<see langword="For" /> in Visual Basic) loop in which iterations may run in parallel and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <returns>A  structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x06004033 RID: 16435 RVA: 0x000EFFCC File Offset: 0x000EE1CC
		[__DynamicallyInvokable]
		public static ParallelLoopResult For(int fromInclusive, int toExclusive, Action<int, ParallelLoopState> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForWorker<object>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, null, body, null, null, null);
		}

		/// <summary>Executes a <see langword="for" /> (<see langword="For" /> in Visual Basic) loop with 64-bit indexes in which iterations may run in parallel and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <returns>A <see cref="T:System.Threading.Tasks.ParallelLoopResult" /> structure that contains information on what portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x06004034 RID: 16436 RVA: 0x000EFFED File Offset: 0x000EE1ED
		[__DynamicallyInvokable]
		public static ParallelLoopResult For(long fromInclusive, long toExclusive, Action<long, ParallelLoopState> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForWorker64<object>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, null, body, null, null, null);
		}

		/// <summary>Executes a <see langword="for" /> (<see langword="For" /> in Visual Basic) loop in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x06004035 RID: 16437 RVA: 0x000F000E File Offset: 0x000EE20E
		[__DynamicallyInvokable]
		public static ParallelLoopResult For(int fromInclusive, int toExclusive, ParallelOptions parallelOptions, Action<int, ParallelLoopState> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker<object>(fromInclusive, toExclusive, parallelOptions, null, body, null, null, null);
		}

		/// <summary>Executes a <see langword="for" /> (<see langword="For" /> in Visual Basic)  loop with 64-bit indexes in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x06004036 RID: 16438 RVA: 0x000F0039 File Offset: 0x000EE239
		[__DynamicallyInvokable]
		public static ParallelLoopResult For(long fromInclusive, long toExclusive, ParallelOptions parallelOptions, Action<long, ParallelLoopState> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker64<object>(fromInclusive, toExclusive, parallelOptions, null, body, null, null, null);
		}

		/// <summary>Executes a <see langword="for" /> (<see langword="For" /> in Visual Basic) loop with thread-local data in which iterations may run in parallel, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <returns>A  structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localInit" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localFinally" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x06004037 RID: 16439 RVA: 0x000F0064 File Offset: 0x000EE264
		[__DynamicallyInvokable]
		public static ParallelLoopResult For<TLocal>(int fromInclusive, int toExclusive, Func<TLocal> localInit, Func<int, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			return Parallel.ForWorker<TLocal>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, null, null, body, localInit, localFinally);
		}

		/// <summary>Executes a <see langword="for" /> (<see langword="For" /> in Visual Basic)  loop with 64-bit indexes and thread-local data in which iterations may run in parallel, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localInit" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localFinally" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x06004038 RID: 16440 RVA: 0x000F00A3 File Offset: 0x000EE2A3
		[__DynamicallyInvokable]
		public static ParallelLoopResult For<TLocal>(long fromInclusive, long toExclusive, Func<TLocal> localInit, Func<long, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			return Parallel.ForWorker64<TLocal>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, null, null, body, localInit, localFinally);
		}

		/// <summary>Executes a <see langword="for" /> (<see langword="For" /> in Visual Basic)  loop with thread-local data in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localInit" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localFinally" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x06004039 RID: 16441 RVA: 0x000F00E4 File Offset: 0x000EE2E4
		[__DynamicallyInvokable]
		public static ParallelLoopResult For<TLocal>(int fromInclusive, int toExclusive, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<int, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker<TLocal>(fromInclusive, toExclusive, parallelOptions, null, null, body, localInit, localFinally);
		}

		/// <summary>Executes a <see langword="for" /> (<see langword="For" /> in Visual Basic) loop with 64-bit indexes and thread-local data in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each thread.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each thread.</param>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localInit" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localFinally" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x0600403A RID: 16442 RVA: 0x000F013C File Offset: 0x000EE33C
		[__DynamicallyInvokable]
		public static ParallelLoopResult For<TLocal>(long fromInclusive, long toExclusive, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<long, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker64<TLocal>(fromInclusive, toExclusive, parallelOptions, null, null, body, localInit, localFinally);
		}

		// Token: 0x0600403B RID: 16443 RVA: 0x000F0194 File Offset: 0x000EE394
		private static ParallelLoopResult ForWorker<TLocal>(int fromInclusive, int toExclusive, ParallelOptions parallelOptions, Action<int> body, Action<int, ParallelLoopState> bodyWithState, Func<int, ParallelLoopState, TLocal, TLocal> bodyWithLocal, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			ParallelLoopResult parallelLoopResult = default(ParallelLoopResult);
			if (toExclusive <= fromInclusive)
			{
				parallelLoopResult.m_completed = true;
				return parallelLoopResult;
			}
			ParallelLoopStateFlags32 sharedPStateFlags = new ParallelLoopStateFlags32();
			TaskCreationOptions taskCreationOptions = TaskCreationOptions.None;
			InternalTaskOptions internalTaskOptions = InternalTaskOptions.SelfReplicating;
			if (parallelOptions.CancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException(parallelOptions.CancellationToken);
			}
			int num = ((parallelOptions.EffectiveMaxConcurrencyLevel == -1) ? PlatformHelper.ProcessorCount : parallelOptions.EffectiveMaxConcurrencyLevel);
			RangeManager rangeManager = new RangeManager((long)fromInclusive, (long)toExclusive, 1L, num);
			OperationCanceledException oce = null;
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			if (parallelOptions.CancellationToken.CanBeCanceled)
			{
				cancellationTokenRegistration = parallelOptions.CancellationToken.InternalRegisterWithoutEC(delegate(object o)
				{
					sharedPStateFlags.Cancel();
					oce = new OperationCanceledException(parallelOptions.CancellationToken);
				}, null);
			}
			int forkJoinContextID = 0;
			Task task = null;
			if (TplEtwProvider.Log.IsEnabled())
			{
				forkJoinContextID = Interlocked.Increment(ref Parallel.s_forkJoinContextID);
				task = Task.InternalCurrent;
				TplEtwProvider.Log.ParallelLoopBegin((task != null) ? task.m_taskScheduler.Id : TaskScheduler.Current.Id, (task != null) ? task.Id : 0, forkJoinContextID, TplEtwProvider.ForkJoinOperationType.ParallelFor, (long)fromInclusive, (long)toExclusive);
			}
			ParallelForReplicatingTask rootTask = null;
			try
			{
				rootTask = new ParallelForReplicatingTask(parallelOptions, delegate
				{
					Task internalCurrent = Task.InternalCurrent;
					bool flag = internalCurrent == rootTask;
					RangeWorker rangeWorker = default(RangeWorker);
					object savedStateFromPreviousReplica = internalCurrent.SavedStateFromPreviousReplica;
					if (savedStateFromPreviousReplica is RangeWorker)
					{
						rangeWorker = (RangeWorker)savedStateFromPreviousReplica;
					}
					else
					{
						rangeWorker = rangeManager.RegisterNewWorker();
					}
					int num3;
					int num4;
					if (!rangeWorker.FindNewWork32(out num3, out num4) || sharedPStateFlags.ShouldExitLoop(num3))
					{
						return;
					}
					if (TplEtwProvider.Log.IsEnabled())
					{
						TplEtwProvider.Log.ParallelFork((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Current.Id, (internalCurrent != null) ? internalCurrent.Id : 0, forkJoinContextID);
					}
					TLocal tlocal = default(TLocal);
					bool flag2 = false;
					try
					{
						ParallelLoopState32 parallelLoopState = null;
						if (bodyWithState != null)
						{
							parallelLoopState = new ParallelLoopState32(sharedPStateFlags);
						}
						else if (bodyWithLocal != null)
						{
							parallelLoopState = new ParallelLoopState32(sharedPStateFlags);
							if (localInit != null)
							{
								tlocal = localInit();
								flag2 = true;
							}
						}
						Parallel.LoopTimer loopTimer = new Parallel.LoopTimer(rootTask.ActiveChildCount);
						for (;;)
						{
							if (body != null)
							{
								for (int i = num3; i < num4; i++)
								{
									if (sharedPStateFlags.LoopStateFlags != ParallelLoopStateFlags.PLS_NONE && sharedPStateFlags.ShouldExitLoop())
									{
										break;
									}
									body(i);
								}
							}
							else if (bodyWithState != null)
							{
								for (int j = num3; j < num4; j++)
								{
									if (sharedPStateFlags.LoopStateFlags != ParallelLoopStateFlags.PLS_NONE && sharedPStateFlags.ShouldExitLoop(j))
									{
										break;
									}
									parallelLoopState.CurrentIteration = j;
									bodyWithState(j, parallelLoopState);
								}
							}
							else
							{
								int num5 = num3;
								while (num5 < num4 && (sharedPStateFlags.LoopStateFlags == ParallelLoopStateFlags.PLS_NONE || !sharedPStateFlags.ShouldExitLoop(num5)))
								{
									parallelLoopState.CurrentIteration = num5;
									tlocal = bodyWithLocal(num5, parallelLoopState, tlocal);
									num5++;
								}
							}
							if (!flag && loopTimer.LimitExceeded())
							{
								break;
							}
							if (!rangeWorker.FindNewWork32(out num3, out num4) || (sharedPStateFlags.LoopStateFlags != ParallelLoopStateFlags.PLS_NONE && sharedPStateFlags.ShouldExitLoop(num3)))
							{
								goto IL_23F;
							}
						}
						internalCurrent.SavedStateForNextReplica = rangeWorker;
						IL_23F:;
					}
					catch
					{
						sharedPStateFlags.SetExceptional();
						throw;
					}
					finally
					{
						if (localFinally != null && flag2)
						{
							localFinally(tlocal);
						}
						if (TplEtwProvider.Log.IsEnabled())
						{
							TplEtwProvider.Log.ParallelJoin((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Current.Id, (internalCurrent != null) ? internalCurrent.Id : 0, forkJoinContextID);
						}
					}
				}, taskCreationOptions, internalTaskOptions);
				rootTask.RunSynchronously(parallelOptions.EffectiveTaskScheduler);
				rootTask.Wait();
				if (parallelOptions.CancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration.Dispose();
				}
				if (oce != null)
				{
					throw oce;
				}
			}
			catch (AggregateException ex)
			{
				if (parallelOptions.CancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration.Dispose();
				}
				Parallel.ThrowIfReducableToSingleOCE(ex.InnerExceptions, parallelOptions.CancellationToken);
				throw;
			}
			catch (TaskSchedulerException)
			{
				if (parallelOptions.CancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration.Dispose();
				}
				throw;
			}
			finally
			{
				int loopStateFlags = sharedPStateFlags.LoopStateFlags;
				parallelLoopResult.m_completed = loopStateFlags == ParallelLoopStateFlags.PLS_NONE;
				if ((loopStateFlags & ParallelLoopStateFlags.PLS_BROKEN) != 0)
				{
					parallelLoopResult.m_lowestBreakIteration = new long?((long)sharedPStateFlags.LowestBreakIteration);
				}
				if (rootTask != null && rootTask.IsCompleted)
				{
					rootTask.Dispose();
				}
				if (TplEtwProvider.Log.IsEnabled())
				{
					int num2;
					if (loopStateFlags == ParallelLoopStateFlags.PLS_NONE)
					{
						num2 = toExclusive - fromInclusive;
					}
					else if ((loopStateFlags & ParallelLoopStateFlags.PLS_BROKEN) != 0)
					{
						num2 = sharedPStateFlags.LowestBreakIteration - fromInclusive;
					}
					else
					{
						num2 = -1;
					}
					TplEtwProvider.Log.ParallelLoopEnd((task != null) ? task.m_taskScheduler.Id : TaskScheduler.Current.Id, (task != null) ? task.Id : 0, forkJoinContextID, (long)num2);
				}
			}
			return parallelLoopResult;
		}

		// Token: 0x0600403C RID: 16444 RVA: 0x000F04E4 File Offset: 0x000EE6E4
		private static ParallelLoopResult ForWorker64<TLocal>(long fromInclusive, long toExclusive, ParallelOptions parallelOptions, Action<long> body, Action<long, ParallelLoopState> bodyWithState, Func<long, ParallelLoopState, TLocal, TLocal> bodyWithLocal, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			ParallelLoopResult parallelLoopResult = default(ParallelLoopResult);
			if (toExclusive <= fromInclusive)
			{
				parallelLoopResult.m_completed = true;
				return parallelLoopResult;
			}
			ParallelLoopStateFlags64 sharedPStateFlags = new ParallelLoopStateFlags64();
			TaskCreationOptions taskCreationOptions = TaskCreationOptions.None;
			InternalTaskOptions internalTaskOptions = InternalTaskOptions.SelfReplicating;
			if (parallelOptions.CancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException(parallelOptions.CancellationToken);
			}
			int num = ((parallelOptions.EffectiveMaxConcurrencyLevel == -1) ? PlatformHelper.ProcessorCount : parallelOptions.EffectiveMaxConcurrencyLevel);
			RangeManager rangeManager = new RangeManager(fromInclusive, toExclusive, 1L, num);
			OperationCanceledException oce = null;
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			if (parallelOptions.CancellationToken.CanBeCanceled)
			{
				cancellationTokenRegistration = parallelOptions.CancellationToken.InternalRegisterWithoutEC(delegate(object o)
				{
					sharedPStateFlags.Cancel();
					oce = new OperationCanceledException(parallelOptions.CancellationToken);
				}, null);
			}
			Task task = null;
			int forkJoinContextID = 0;
			if (TplEtwProvider.Log.IsEnabled())
			{
				forkJoinContextID = Interlocked.Increment(ref Parallel.s_forkJoinContextID);
				task = Task.InternalCurrent;
				TplEtwProvider.Log.ParallelLoopBegin((task != null) ? task.m_taskScheduler.Id : TaskScheduler.Current.Id, (task != null) ? task.Id : 0, forkJoinContextID, TplEtwProvider.ForkJoinOperationType.ParallelFor, fromInclusive, toExclusive);
			}
			ParallelForReplicatingTask rootTask = null;
			try
			{
				rootTask = new ParallelForReplicatingTask(parallelOptions, delegate
				{
					Task internalCurrent = Task.InternalCurrent;
					bool flag = internalCurrent == rootTask;
					RangeWorker rangeWorker = default(RangeWorker);
					object savedStateFromPreviousReplica = internalCurrent.SavedStateFromPreviousReplica;
					if (savedStateFromPreviousReplica is RangeWorker)
					{
						rangeWorker = (RangeWorker)savedStateFromPreviousReplica;
					}
					else
					{
						rangeWorker = rangeManager.RegisterNewWorker();
					}
					long num3;
					long num4;
					if (!rangeWorker.FindNewWork(out num3, out num4) || sharedPStateFlags.ShouldExitLoop(num3))
					{
						return;
					}
					if (TplEtwProvider.Log.IsEnabled())
					{
						TplEtwProvider.Log.ParallelFork((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Current.Id, (internalCurrent != null) ? internalCurrent.Id : 0, forkJoinContextID);
					}
					TLocal tlocal = default(TLocal);
					bool flag2 = false;
					try
					{
						ParallelLoopState64 parallelLoopState = null;
						if (bodyWithState != null)
						{
							parallelLoopState = new ParallelLoopState64(sharedPStateFlags);
						}
						else if (bodyWithLocal != null)
						{
							parallelLoopState = new ParallelLoopState64(sharedPStateFlags);
							if (localInit != null)
							{
								tlocal = localInit();
								flag2 = true;
							}
						}
						Parallel.LoopTimer loopTimer = new Parallel.LoopTimer(rootTask.ActiveChildCount);
						for (;;)
						{
							if (body != null)
							{
								for (long num5 = num3; num5 < num4; num5 += 1L)
								{
									if (sharedPStateFlags.LoopStateFlags != ParallelLoopStateFlags.PLS_NONE && sharedPStateFlags.ShouldExitLoop())
									{
										break;
									}
									body(num5);
								}
							}
							else if (bodyWithState != null)
							{
								for (long num6 = num3; num6 < num4; num6 += 1L)
								{
									if (sharedPStateFlags.LoopStateFlags != ParallelLoopStateFlags.PLS_NONE && sharedPStateFlags.ShouldExitLoop(num6))
									{
										break;
									}
									parallelLoopState.CurrentIteration = num6;
									bodyWithState(num6, parallelLoopState);
								}
							}
							else
							{
								long num7 = num3;
								while (num7 < num4 && (sharedPStateFlags.LoopStateFlags == ParallelLoopStateFlags.PLS_NONE || !sharedPStateFlags.ShouldExitLoop(num7)))
								{
									parallelLoopState.CurrentIteration = num7;
									tlocal = bodyWithLocal(num7, parallelLoopState, tlocal);
									num7 += 1L;
								}
							}
							if (!flag && loopTimer.LimitExceeded())
							{
								break;
							}
							if (!rangeWorker.FindNewWork(out num3, out num4) || (sharedPStateFlags.LoopStateFlags != ParallelLoopStateFlags.PLS_NONE && sharedPStateFlags.ShouldExitLoop(num3)))
							{
								goto IL_242;
							}
						}
						internalCurrent.SavedStateForNextReplica = rangeWorker;
						IL_242:;
					}
					catch
					{
						sharedPStateFlags.SetExceptional();
						throw;
					}
					finally
					{
						if (localFinally != null && flag2)
						{
							localFinally(tlocal);
						}
						if (TplEtwProvider.Log.IsEnabled())
						{
							TplEtwProvider.Log.ParallelJoin((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Current.Id, (internalCurrent != null) ? internalCurrent.Id : 0, forkJoinContextID);
						}
					}
				}, taskCreationOptions, internalTaskOptions);
				rootTask.RunSynchronously(parallelOptions.EffectiveTaskScheduler);
				rootTask.Wait();
				if (parallelOptions.CancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration.Dispose();
				}
				if (oce != null)
				{
					throw oce;
				}
			}
			catch (AggregateException ex)
			{
				if (parallelOptions.CancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration.Dispose();
				}
				Parallel.ThrowIfReducableToSingleOCE(ex.InnerExceptions, parallelOptions.CancellationToken);
				throw;
			}
			catch (TaskSchedulerException)
			{
				if (parallelOptions.CancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration.Dispose();
				}
				throw;
			}
			finally
			{
				int loopStateFlags = sharedPStateFlags.LoopStateFlags;
				parallelLoopResult.m_completed = loopStateFlags == ParallelLoopStateFlags.PLS_NONE;
				if ((loopStateFlags & ParallelLoopStateFlags.PLS_BROKEN) != 0)
				{
					parallelLoopResult.m_lowestBreakIteration = new long?(sharedPStateFlags.LowestBreakIteration);
				}
				if (rootTask != null && rootTask.IsCompleted)
				{
					rootTask.Dispose();
				}
				if (TplEtwProvider.Log.IsEnabled())
				{
					long num2;
					if (loopStateFlags == ParallelLoopStateFlags.PLS_NONE)
					{
						num2 = toExclusive - fromInclusive;
					}
					else if ((loopStateFlags & ParallelLoopStateFlags.PLS_BROKEN) != 0)
					{
						num2 = sharedPStateFlags.LowestBreakIteration - fromInclusive;
					}
					else
					{
						num2 = -1L;
					}
					TplEtwProvider.Log.ParallelLoopEnd((task != null) ? task.m_taskScheduler.Id : TaskScheduler.Current.Id, (task != null) ? task.Id : 0, forkJoinContextID, num2);
				}
			}
			return parallelLoopResult;
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel.</summary>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x0600403D RID: 16445 RVA: 0x000F0830 File Offset: 0x000EEA30
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, Action<TSource> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, body, null, null, null, null, null, null);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel and loop options can be configured.</summary>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x0600403E RID: 16446 RVA: 0x000F086C File Offset: 0x000EEA6C
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Action<TSource> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForEachWorker<TSource, object>(source, parallelOptions, body, null, null, null, null, null, null);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x0600403F RID: 16447 RVA: 0x000F08B4 File Offset: 0x000EEAB4
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, Action<TSource, ParallelLoopState> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, null, body, null, null, null, null, null);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x06004040 RID: 16448 RVA: 0x000F08F0 File Offset: 0x000EEAF0
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Action<TSource, ParallelLoopState> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForEachWorker<TSource, object>(source, parallelOptions, null, body, null, null, null, null, null);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation with 64-bit indexes on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x06004041 RID: 16449 RVA: 0x000F0938 File Offset: 0x000EEB38
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, Action<TSource, ParallelLoopState, long> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, null, null, body, null, null, null, null);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation with 64-bit indexes on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x06004042 RID: 16450 RVA: 0x000F0974 File Offset: 0x000EEB74
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Action<TSource, ParallelLoopState, long> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForEachWorker<TSource, object>(source, parallelOptions, null, null, body, null, null, null, null);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation with thread-local data on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localInit" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localFinally" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x06004043 RID: 16451 RVA: 0x000F09BC File Offset: 0x000EEBBC
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource, TLocal>(IEnumerable<TSource> source, Func<TLocal> localInit, Func<TSource, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			return Parallel.ForEachWorker<TSource, TLocal>(source, Parallel.s_defaultParallelOptions, null, null, null, body, null, localInit, localFinally);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation with thread-local data on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localInit" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localFinally" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x06004044 RID: 16452 RVA: 0x000F0A14 File Offset: 0x000EEC14
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource, TLocal>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<TSource, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForEachWorker<TSource, TLocal>(source, parallelOptions, null, null, null, body, null, localInit, localFinally);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation with thread-local data on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localInit" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localFinally" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x06004045 RID: 16453 RVA: 0x000F0A78 File Offset: 0x000EEC78
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource, TLocal>(IEnumerable<TSource> source, Func<TLocal> localInit, Func<TSource, ParallelLoopState, long, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			return Parallel.ForEachWorker<TSource, TLocal>(source, Parallel.s_defaultParallelOptions, null, null, null, null, body, localInit, localFinally);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation with thread-local data and 64-bit indexes on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localInit" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localFinally" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x06004046 RID: 16454 RVA: 0x000F0AD0 File Offset: 0x000EECD0
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource, TLocal>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<TSource, ParallelLoopState, long, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForEachWorker<TSource, TLocal>(source, parallelOptions, null, null, null, null, body, localInit, localFinally);
		}

		// Token: 0x06004047 RID: 16455 RVA: 0x000F0B34 File Offset: 0x000EED34
		private static ParallelLoopResult ForEachWorker<TSource, TLocal>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Action<TSource> body, Action<TSource, ParallelLoopState> bodyWithState, Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex, Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal, Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			if (parallelOptions.CancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException(parallelOptions.CancellationToken);
			}
			TSource[] array = source as TSource[];
			if (array != null)
			{
				return Parallel.ForEachWorker<TSource, TLocal>(array, parallelOptions, body, bodyWithState, bodyWithStateAndIndex, bodyWithStateAndLocal, bodyWithEverything, localInit, localFinally);
			}
			IList<TSource> list = source as IList<TSource>;
			if (list != null)
			{
				return Parallel.ForEachWorker<TSource, TLocal>(list, parallelOptions, body, bodyWithState, bodyWithStateAndIndex, bodyWithStateAndLocal, bodyWithEverything, localInit, localFinally);
			}
			return Parallel.PartitionerForEachWorker<TSource, TLocal>(Partitioner.Create<TSource>(source), parallelOptions, body, bodyWithState, bodyWithStateAndIndex, bodyWithStateAndLocal, bodyWithEverything, localInit, localFinally);
		}

		// Token: 0x06004048 RID: 16456 RVA: 0x000F0BB4 File Offset: 0x000EEDB4
		private static ParallelLoopResult ForEachWorker<TSource, TLocal>(TSource[] array, ParallelOptions parallelOptions, Action<TSource> body, Action<TSource, ParallelLoopState> bodyWithState, Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex, Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal, Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			int lowerBound = array.GetLowerBound(0);
			int num = array.GetUpperBound(0) + 1;
			if (body != null)
			{
				return Parallel.ForWorker<object>(lowerBound, num, parallelOptions, delegate(int i)
				{
					body(array[i]);
				}, null, null, null, null);
			}
			if (bodyWithState != null)
			{
				return Parallel.ForWorker<object>(lowerBound, num, parallelOptions, null, delegate(int i, ParallelLoopState state)
				{
					bodyWithState(array[i], state);
				}, null, null, null);
			}
			if (bodyWithStateAndIndex != null)
			{
				return Parallel.ForWorker<object>(lowerBound, num, parallelOptions, null, delegate(int i, ParallelLoopState state)
				{
					bodyWithStateAndIndex(array[i], state, (long)i);
				}, null, null, null);
			}
			if (bodyWithStateAndLocal != null)
			{
				return Parallel.ForWorker<TLocal>(lowerBound, num, parallelOptions, null, null, (int i, ParallelLoopState state, TLocal local) => bodyWithStateAndLocal(array[i], state, local), localInit, localFinally);
			}
			return Parallel.ForWorker<TLocal>(lowerBound, num, parallelOptions, null, null, (int i, ParallelLoopState state, TLocal local) => bodyWithEverything(array[i], state, (long)i, local), localInit, localFinally);
		}

		// Token: 0x06004049 RID: 16457 RVA: 0x000F0CB0 File Offset: 0x000EEEB0
		private static ParallelLoopResult ForEachWorker<TSource, TLocal>(IList<TSource> list, ParallelOptions parallelOptions, Action<TSource> body, Action<TSource, ParallelLoopState> bodyWithState, Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex, Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal, Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			if (body != null)
			{
				return Parallel.ForWorker<object>(0, list.Count, parallelOptions, delegate(int i)
				{
					body(list[i]);
				}, null, null, null, null);
			}
			if (bodyWithState != null)
			{
				return Parallel.ForWorker<object>(0, list.Count, parallelOptions, null, delegate(int i, ParallelLoopState state)
				{
					bodyWithState(list[i], state);
				}, null, null, null);
			}
			if (bodyWithStateAndIndex != null)
			{
				return Parallel.ForWorker<object>(0, list.Count, parallelOptions, null, delegate(int i, ParallelLoopState state)
				{
					bodyWithStateAndIndex(list[i], state, (long)i);
				}, null, null, null);
			}
			if (bodyWithStateAndLocal != null)
			{
				return Parallel.ForWorker<TLocal>(0, list.Count, parallelOptions, null, null, (int i, ParallelLoopState state, TLocal local) => bodyWithStateAndLocal(list[i], state, local), localInit, localFinally);
			}
			return Parallel.ForWorker<TLocal>(0, list.Count, parallelOptions, null, null, (int i, ParallelLoopState state, TLocal local) => bodyWithEverything(list[i], state, (long)i, local), localInit, localFinally);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation on a <see cref="T:System.Collections.Concurrent.Partitioner" /> in which iterations may run in parallel.</summary>
		/// <param name="source">The partitioner that contains the original data source.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is  <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /> partitioner returns <see langword="false" />.  
		///  -or-  
		///  The exception that is thrown when any methods in the <paramref name="source" /> partitioner return <see langword="null" />.  
		///  -or-  
		///  The <see cref="M:System.Collections.Concurrent.Partitioner`1.GetPartitions(System.Int32)" /> method in the <paramref name="source" /> partitioner does not return the correct number of partitions.</exception>
		// Token: 0x0600404A RID: 16458 RVA: 0x000F0DC4 File Offset: 0x000EEFC4
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource>(Partitioner<TSource> source, Action<TSource> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, body, null, null, null, null, null, null);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation on a <see cref="T:System.Collections.Concurrent.Partitioner" /> in which iterations may run in parallel, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">The partitioner that contains the original data source.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /> partitioner returns <see langword="false" />.  
		///  -or-  
		///  A method in the <paramref name="source" /> partitioner returns <see langword="null" />.  
		///  -or-  
		///  The <see cref="M:System.Collections.Concurrent.Partitioner`1.GetPartitions(System.Int32)" /> method in the <paramref name="source" /> partitioner does not return the correct number of partitions.</exception>
		// Token: 0x0600404B RID: 16459 RVA: 0x000F0E00 File Offset: 0x000EF000
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource>(Partitioner<TSource> source, Action<TSource, ParallelLoopState> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, null, body, null, null, null, null, null);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation on a <see cref="T:System.Collections.Concurrent.OrderablePartitioner`1" /> in which iterations may run in parallel and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">The orderable partitioner that contains the original data source.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /> orderable partitioner returns <see langword="false" />.  
		///  -or-  
		///  The <see cref="P:System.Collections.Concurrent.OrderablePartitioner`1.KeysNormalized" /> property in the source orderable partitioner returns <see langword="false" />.  
		///  -or-  
		///  Any methods in the source orderable partitioner return <see langword="null" />.</exception>
		// Token: 0x0600404C RID: 16460 RVA: 0x000F0E3C File Offset: 0x000EF03C
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource>(OrderablePartitioner<TSource> source, Action<TSource, ParallelLoopState, long> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (!source.KeysNormalized)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Parallel_ForEach_OrderedPartitionerKeysNotNormalized"));
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, null, null, body, null, null, null, null);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation with thread-local data on a <see cref="T:System.Collections.Concurrent.Partitioner" /> in which iterations may run in parallel and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">The partitioner that contains the original data source.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localInit" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localFinally" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /><see cref="T:System.Collections.Concurrent.Partitioner" /> returns <see langword="false" /> or the partitioner returns <see langword="null" /> partitions.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x0600404D RID: 16461 RVA: 0x000F0E90 File Offset: 0x000EF090
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource, TLocal>(Partitioner<TSource> source, Func<TLocal> localInit, Func<TSource, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			return Parallel.PartitionerForEachWorker<TSource, TLocal>(source, Parallel.s_defaultParallelOptions, null, null, null, body, null, localInit, localFinally);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation with thread-local data on a <see cref="T:System.Collections.Concurrent.OrderablePartitioner`1" /> in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">The orderable partitioner that contains the original data source.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localInit" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localFinally" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /><see cref="T:System.Collections.Concurrent.Partitioner" /> returns <see langword="false" /> or the partitioner returns <see langword="null" /> partitions.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x0600404E RID: 16462 RVA: 0x000F0EE8 File Offset: 0x000EF0E8
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource, TLocal>(OrderablePartitioner<TSource> source, Func<TLocal> localInit, Func<TSource, ParallelLoopState, long, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (!source.KeysNormalized)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Parallel_ForEach_OrderedPartitionerKeysNotNormalized"));
			}
			return Parallel.PartitionerForEachWorker<TSource, TLocal>(source, Parallel.s_defaultParallelOptions, null, null, null, null, body, localInit, localFinally);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation on a <see cref="T:System.Collections.Concurrent.Partitioner" /> in which iterations may run in parallel and loop options can be configured.</summary>
		/// <param name="source">The partitioner that contains the original data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /> partitioner returns <see langword="false" />.  
		///  -or-  
		///  The exception that is thrown when any methods in the <paramref name="source" /> partitioner return <see langword="null" />.</exception>
		// Token: 0x0600404F RID: 16463 RVA: 0x000F0F58 File Offset: 0x000EF158
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource>(Partitioner<TSource> source, ParallelOptions parallelOptions, Action<TSource> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, parallelOptions, body, null, null, null, null, null, null);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation on a <see cref="T:System.Collections.Concurrent.Partitioner" /> in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">The partitioner that contains the original data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <returns>A  structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /> partitioner returns <see langword="false" />.  
		///  -or-  
		///  The exception that is thrown when any methods in the <paramref name="source" /> partitioner return <see langword="null" />.</exception>
		// Token: 0x06004050 RID: 16464 RVA: 0x000F0FA0 File Offset: 0x000EF1A0
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource>(Partitioner<TSource> source, ParallelOptions parallelOptions, Action<TSource, ParallelLoopState> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, parallelOptions, null, body, null, null, null, null, null);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation on a <see cref="T:System.Collections.Concurrent.OrderablePartitioner`1" /> in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">The orderable partitioner that contains the original data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is  <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /> orderable partitioner returns <see langword="false" />.  
		///  -or-  
		///  The <see cref="P:System.Collections.Concurrent.OrderablePartitioner`1.KeysNormalized" /> property in the <paramref name="source" /> orderable partitioner returns <see langword="false" />.  
		///  -or-  
		///  The exception that is thrown when any methods in the <paramref name="source" /> orderable partitioner return <see langword="null" />.</exception>
		// Token: 0x06004051 RID: 16465 RVA: 0x000F0FE8 File Offset: 0x000EF1E8
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource>(OrderablePartitioner<TSource> source, ParallelOptions parallelOptions, Action<TSource, ParallelLoopState, long> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			if (!source.KeysNormalized)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Parallel_ForEach_OrderedPartitionerKeysNotNormalized"));
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, parallelOptions, null, null, body, null, null, null, null);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation  with thread-local data on a <see cref="T:System.Collections.Concurrent.Partitioner" /> in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">The partitioner that contains the original data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localInit" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localFinally" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /><see cref="T:System.Collections.Concurrent.Partitioner" /> returns <see langword="false" /> or the partitioner returns <see langword="null" /> partitions.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x06004052 RID: 16466 RVA: 0x000F1048 File Offset: 0x000EF248
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource, TLocal>(Partitioner<TSource> source, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<TSource, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.PartitionerForEachWorker<TSource, TLocal>(source, parallelOptions, null, null, null, body, null, localInit, localFinally);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation with 64-bit indexes and  with thread-local data on a <see cref="T:System.Collections.Concurrent.OrderablePartitioner`1" /> in which iterations may run in parallel , loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">The orderable partitioner that contains the original data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localInit" /> or <paramref name="localFinally" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /><see cref="T:System.Collections.Concurrent.Partitioner" /> returns <see langword="false" /> or the partitioner returns <see langword="null" /> partitions.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x06004053 RID: 16467 RVA: 0x000F10AC File Offset: 0x000EF2AC
		[__DynamicallyInvokable]
		public static ParallelLoopResult ForEach<TSource, TLocal>(OrderablePartitioner<TSource> source, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<TSource, ParallelLoopState, long, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			if (!source.KeysNormalized)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Parallel_ForEach_OrderedPartitionerKeysNotNormalized"));
			}
			return Parallel.PartitionerForEachWorker<TSource, TLocal>(source, parallelOptions, null, null, null, null, body, localInit, localFinally);
		}

		// Token: 0x06004054 RID: 16468 RVA: 0x000F1128 File Offset: 0x000EF328
		private static ParallelLoopResult PartitionerForEachWorker<TSource, TLocal>(Partitioner<TSource> source, ParallelOptions parallelOptions, Action<TSource> simpleBody, Action<TSource, ParallelLoopState> bodyWithState, Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex, Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal, Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			OrderablePartitioner<TSource> orderedSource = source as OrderablePartitioner<TSource>;
			if (!source.SupportsDynamicPartitions)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Parallel_ForEach_PartitionerNotDynamic"));
			}
			if (parallelOptions.CancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException(parallelOptions.CancellationToken);
			}
			int forkJoinContextID = 0;
			Task task = null;
			if (TplEtwProvider.Log.IsEnabled())
			{
				forkJoinContextID = Interlocked.Increment(ref Parallel.s_forkJoinContextID);
				task = Task.InternalCurrent;
				TplEtwProvider.Log.ParallelLoopBegin((task != null) ? task.m_taskScheduler.Id : TaskScheduler.Current.Id, (task != null) ? task.Id : 0, forkJoinContextID, TplEtwProvider.ForkJoinOperationType.ParallelForEach, 0L, 0L);
			}
			ParallelLoopStateFlags64 sharedPStateFlags = new ParallelLoopStateFlags64();
			ParallelLoopResult parallelLoopResult = default(ParallelLoopResult);
			OperationCanceledException oce = null;
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			if (parallelOptions.CancellationToken.CanBeCanceled)
			{
				cancellationTokenRegistration = parallelOptions.CancellationToken.InternalRegisterWithoutEC(delegate(object o)
				{
					sharedPStateFlags.Cancel();
					oce = new OperationCanceledException(parallelOptions.CancellationToken);
				}, null);
			}
			IEnumerable<TSource> partitionerSource = null;
			IEnumerable<KeyValuePair<long, TSource>> orderablePartitionerSource = null;
			if (orderedSource != null)
			{
				orderablePartitionerSource = orderedSource.GetOrderableDynamicPartitions();
				if (orderablePartitionerSource == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Parallel_ForEach_PartitionerReturnedNull"));
				}
			}
			else
			{
				partitionerSource = source.GetDynamicPartitions();
				if (partitionerSource == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Parallel_ForEach_PartitionerReturnedNull"));
				}
			}
			ParallelForReplicatingTask rootTask = null;
			Action action = delegate
			{
				Task internalCurrent = Task.InternalCurrent;
				if (TplEtwProvider.Log.IsEnabled())
				{
					TplEtwProvider.Log.ParallelFork((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Current.Id, (internalCurrent != null) ? internalCurrent.Id : 0, forkJoinContextID);
				}
				TLocal tlocal = default(TLocal);
				bool flag = false;
				IDisposable disposable2 = null;
				try
				{
					ParallelLoopState64 parallelLoopState = null;
					if (bodyWithState != null || bodyWithStateAndIndex != null)
					{
						parallelLoopState = new ParallelLoopState64(sharedPStateFlags);
					}
					else if (bodyWithStateAndLocal != null || bodyWithEverything != null)
					{
						parallelLoopState = new ParallelLoopState64(sharedPStateFlags);
						if (localInit != null)
						{
							tlocal = localInit();
							flag = true;
						}
					}
					bool flag2 = rootTask == internalCurrent;
					Parallel.LoopTimer loopTimer = new Parallel.LoopTimer(rootTask.ActiveChildCount);
					if (orderedSource != null)
					{
						IEnumerator<KeyValuePair<long, TSource>> enumerator = internalCurrent.SavedStateFromPreviousReplica as IEnumerator<KeyValuePair<long, TSource>>;
						if (enumerator == null)
						{
							enumerator = orderablePartitionerSource.GetEnumerator();
							if (enumerator == null)
							{
								throw new InvalidOperationException(Environment.GetResourceString("Parallel_ForEach_NullEnumerator"));
							}
						}
						disposable2 = enumerator;
						while (enumerator.MoveNext())
						{
							KeyValuePair<long, TSource> keyValuePair = enumerator.Current;
							long key = keyValuePair.Key;
							TSource value = keyValuePair.Value;
							if (parallelLoopState != null)
							{
								parallelLoopState.CurrentIteration = key;
							}
							if (simpleBody != null)
							{
								simpleBody(value);
							}
							else if (bodyWithState != null)
							{
								bodyWithState(value, parallelLoopState);
							}
							else if (bodyWithStateAndIndex != null)
							{
								bodyWithStateAndIndex(value, parallelLoopState, key);
							}
							else if (bodyWithStateAndLocal != null)
							{
								tlocal = bodyWithStateAndLocal(value, parallelLoopState, tlocal);
							}
							else
							{
								tlocal = bodyWithEverything(value, parallelLoopState, key, tlocal);
							}
							if (sharedPStateFlags.ShouldExitLoop(key))
							{
								break;
							}
							if (!flag2 && loopTimer.LimitExceeded())
							{
								internalCurrent.SavedStateForNextReplica = enumerator;
								disposable2 = null;
								break;
							}
						}
					}
					else
					{
						IEnumerator<TSource> enumerator2 = internalCurrent.SavedStateFromPreviousReplica as IEnumerator<TSource>;
						if (enumerator2 == null)
						{
							enumerator2 = partitionerSource.GetEnumerator();
							if (enumerator2 == null)
							{
								throw new InvalidOperationException(Environment.GetResourceString("Parallel_ForEach_NullEnumerator"));
							}
						}
						disposable2 = enumerator2;
						if (parallelLoopState != null)
						{
							parallelLoopState.CurrentIteration = 0L;
						}
						while (enumerator2.MoveNext())
						{
							TSource tsource = enumerator2.Current;
							if (simpleBody != null)
							{
								simpleBody(tsource);
							}
							else if (bodyWithState != null)
							{
								bodyWithState(tsource, parallelLoopState);
							}
							else if (bodyWithStateAndLocal != null)
							{
								tlocal = bodyWithStateAndLocal(tsource, parallelLoopState, tlocal);
							}
							if (sharedPStateFlags.LoopStateFlags != ParallelLoopStateFlags.PLS_NONE)
							{
								break;
							}
							if (!flag2 && loopTimer.LimitExceeded())
							{
								internalCurrent.SavedStateForNextReplica = enumerator2;
								disposable2 = null;
								break;
							}
						}
					}
				}
				catch
				{
					sharedPStateFlags.SetExceptional();
					throw;
				}
				finally
				{
					if (localFinally != null && flag)
					{
						localFinally(tlocal);
					}
					if (disposable2 != null)
					{
						disposable2.Dispose();
					}
					if (TplEtwProvider.Log.IsEnabled())
					{
						TplEtwProvider.Log.ParallelJoin((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Current.Id, (internalCurrent != null) ? internalCurrent.Id : 0, forkJoinContextID);
					}
				}
			};
			try
			{
				rootTask = new ParallelForReplicatingTask(parallelOptions, action, TaskCreationOptions.None, InternalTaskOptions.SelfReplicating);
				rootTask.RunSynchronously(parallelOptions.EffectiveTaskScheduler);
				rootTask.Wait();
				if (parallelOptions.CancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration.Dispose();
				}
				if (oce != null)
				{
					throw oce;
				}
			}
			catch (AggregateException ex)
			{
				if (parallelOptions.CancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration.Dispose();
				}
				Parallel.ThrowIfReducableToSingleOCE(ex.InnerExceptions, parallelOptions.CancellationToken);
				throw;
			}
			catch (TaskSchedulerException)
			{
				if (parallelOptions.CancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration.Dispose();
				}
				throw;
			}
			finally
			{
				int loopStateFlags = sharedPStateFlags.LoopStateFlags;
				parallelLoopResult.m_completed = loopStateFlags == ParallelLoopStateFlags.PLS_NONE;
				if ((loopStateFlags & ParallelLoopStateFlags.PLS_BROKEN) != 0)
				{
					parallelLoopResult.m_lowestBreakIteration = new long?(sharedPStateFlags.LowestBreakIteration);
				}
				if (rootTask != null && rootTask.IsCompleted)
				{
					rootTask.Dispose();
				}
				IDisposable disposable;
				if (orderablePartitionerSource != null)
				{
					disposable = orderablePartitionerSource as IDisposable;
				}
				else
				{
					disposable = partitionerSource as IDisposable;
				}
				if (disposable != null)
				{
					disposable.Dispose();
				}
				if (TplEtwProvider.Log.IsEnabled())
				{
					TplEtwProvider.Log.ParallelLoopEnd((task != null) ? task.m_taskScheduler.Id : TaskScheduler.Current.Id, (task != null) ? task.Id : 0, forkJoinContextID, 0L);
				}
			}
			return parallelLoopResult;
		}

		// Token: 0x06004055 RID: 16469 RVA: 0x000F14BC File Offset: 0x000EF6BC
		internal static void ThrowIfReducableToSingleOCE(IEnumerable<Exception> excCollection, CancellationToken ct)
		{
			bool flag = false;
			if (ct.IsCancellationRequested)
			{
				foreach (Exception ex in excCollection)
				{
					flag = true;
					OperationCanceledException ex2 = ex as OperationCanceledException;
					if (ex2 == null || ex2.CancellationToken != ct)
					{
						return;
					}
				}
				if (flag)
				{
					throw new OperationCanceledException(ct);
				}
			}
		}

		// Token: 0x04001AD5 RID: 6869
		internal static int s_forkJoinContextID;

		// Token: 0x04001AD6 RID: 6870
		internal const int DEFAULT_LOOP_STRIDE = 16;

		// Token: 0x04001AD7 RID: 6871
		internal static ParallelOptions s_defaultParallelOptions = new ParallelOptions();

		// Token: 0x02000C05 RID: 3077
		internal struct LoopTimer
		{
			// Token: 0x06006FCC RID: 28620 RVA: 0x001827E0 File Offset: 0x001809E0
			public LoopTimer(int nWorkerTaskIndex)
			{
				int num = 100 + nWorkerTaskIndex % PlatformHelper.ProcessorCount * 50;
				this.m_timeLimit = Environment.TickCount + num;
			}

			// Token: 0x06006FCD RID: 28621 RVA: 0x00182808 File Offset: 0x00180A08
			public bool LimitExceeded()
			{
				return Environment.TickCount > this.m_timeLimit;
			}

			// Token: 0x04003671 RID: 13937
			private const int s_BaseNotifyPeriodMS = 100;

			// Token: 0x04003672 RID: 13938
			private const int s_NotifyPeriodIncrementMS = 50;

			// Token: 0x04003673 RID: 13939
			private int m_timeLimit;
		}
	}
}
