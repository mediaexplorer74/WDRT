using System;
using System.Runtime.Serialization;

namespace System.Threading.Tasks
{
	/// <summary>Represents an exception used to communicate task cancellation.</summary>
	// Token: 0x02000571 RID: 1393
	[__DynamicallyInvokable]
	[Serializable]
	public class TaskCanceledException : OperationCanceledException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskCanceledException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x060041A3 RID: 16803 RVA: 0x000F644C File Offset: 0x000F464C
		[__DynamicallyInvokable]
		public TaskCanceledException()
			: base(Environment.GetResourceString("TaskCanceledException_ctor_DefaultMessage"))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskCanceledException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x060041A4 RID: 16804 RVA: 0x000F645E File Offset: 0x000F465E
		[__DynamicallyInvokable]
		public TaskCanceledException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskCanceledException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060041A5 RID: 16805 RVA: 0x000F6467 File Offset: 0x000F4667
		[__DynamicallyInvokable]
		public TaskCanceledException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskCanceledException" /> class with a reference to the <see cref="T:System.Threading.Tasks.Task" /> that has been canceled.</summary>
		/// <param name="task">A task that has been canceled.</param>
		// Token: 0x060041A6 RID: 16806 RVA: 0x000F6474 File Offset: 0x000F4674
		[__DynamicallyInvokable]
		public TaskCanceledException(Task task)
			: base(Environment.GetResourceString("TaskCanceledException_ctor_DefaultMessage"), (task != null) ? task.CancellationToken : default(CancellationToken))
		{
			this.m_canceledTask = task;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskCanceledException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x060041A7 RID: 16807 RVA: 0x000F64AC File Offset: 0x000F46AC
		protected TaskCanceledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Gets the task associated with this exception.</summary>
		/// <returns>A reference to the <see cref="T:System.Threading.Tasks.Task" /> that is associated with this exception.</returns>
		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x060041A8 RID: 16808 RVA: 0x000F64B6 File Offset: 0x000F46B6
		[__DynamicallyInvokable]
		public Task Task
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_canceledTask;
			}
		}

		// Token: 0x04001B69 RID: 7017
		[NonSerialized]
		private Task m_canceledTask;
	}
}
