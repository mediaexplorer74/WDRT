using System;

namespace System.Runtime.ExceptionServices
{
	/// <summary>Represents an exception whose state is captured at a certain point in code.</summary>
	// Token: 0x020007A8 RID: 1960
	[__DynamicallyInvokable]
	public sealed class ExceptionDispatchInfo
	{
		// Token: 0x0600551B RID: 21787 RVA: 0x0012F9CC File Offset: 0x0012DBCC
		private ExceptionDispatchInfo(Exception exception)
		{
			this.m_Exception = exception;
			this.m_remoteStackTrace = exception.RemoteStackTrace;
			object obj;
			object obj2;
			this.m_Exception.GetStackTracesDeepCopy(out obj, out obj2);
			this.m_stackTrace = obj;
			this.m_dynamicMethods = obj2;
			this.m_IPForWatsonBuckets = exception.IPForWatsonBuckets;
			this.m_WatsonBuckets = exception.WatsonBuckets;
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x0600551C RID: 21788 RVA: 0x0012FA27 File Offset: 0x0012DC27
		internal UIntPtr IPForWatsonBuckets
		{
			get
			{
				return this.m_IPForWatsonBuckets;
			}
		}

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x0600551D RID: 21789 RVA: 0x0012FA2F File Offset: 0x0012DC2F
		internal object WatsonBuckets
		{
			get
			{
				return this.m_WatsonBuckets;
			}
		}

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x0600551E RID: 21790 RVA: 0x0012FA37 File Offset: 0x0012DC37
		internal object BinaryStackTraceArray
		{
			get
			{
				return this.m_stackTrace;
			}
		}

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x0600551F RID: 21791 RVA: 0x0012FA3F File Offset: 0x0012DC3F
		internal object DynamicMethodArray
		{
			get
			{
				return this.m_dynamicMethods;
			}
		}

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x06005520 RID: 21792 RVA: 0x0012FA47 File Offset: 0x0012DC47
		internal string RemoteStackTrace
		{
			get
			{
				return this.m_remoteStackTrace;
			}
		}

		/// <summary>Creates an <see cref="T:System.Runtime.ExceptionServices.ExceptionDispatchInfo" /> object that represents the specified exception at the current point in code.</summary>
		/// <param name="source">The exception whose state is captured, and which is represented by the returned object.</param>
		/// <returns>An object that represents the specified exception at the current point in code.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x06005521 RID: 21793 RVA: 0x0012FA4F File Offset: 0x0012DC4F
		[__DynamicallyInvokable]
		public static ExceptionDispatchInfo Capture(Exception source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source", Environment.GetResourceString("ArgumentNull_Obj"));
			}
			return new ExceptionDispatchInfo(source);
		}

		/// <summary>Gets the exception that is represented by the current instance.</summary>
		/// <returns>The exception that is represented by the current instance.</returns>
		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x06005522 RID: 21794 RVA: 0x0012FA6F File Offset: 0x0012DC6F
		[__DynamicallyInvokable]
		public Exception SourceException
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Exception;
			}
		}

		/// <summary>Throws the exception that is represented by the current <see cref="T:System.Runtime.ExceptionServices.ExceptionDispatchInfo" /> object, after restoring the state that was saved when the exception was captured.</summary>
		// Token: 0x06005523 RID: 21795 RVA: 0x0012FA77 File Offset: 0x0012DC77
		[__DynamicallyInvokable]
		public void Throw()
		{
			this.m_Exception.RestoreExceptionDispatchInfo(this);
			throw this.m_Exception;
		}

		// Token: 0x04002728 RID: 10024
		private Exception m_Exception;

		// Token: 0x04002729 RID: 10025
		private string m_remoteStackTrace;

		// Token: 0x0400272A RID: 10026
		private object m_stackTrace;

		// Token: 0x0400272B RID: 10027
		private object m_dynamicMethods;

		// Token: 0x0400272C RID: 10028
		private UIntPtr m_IPForWatsonBuckets;

		// Token: 0x0400272D RID: 10029
		private object m_WatsonBuckets;
	}
}
