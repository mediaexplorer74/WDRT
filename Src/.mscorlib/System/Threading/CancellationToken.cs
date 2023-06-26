using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Propagates notification that operations should be canceled.</summary>
	// Token: 0x02000549 RID: 1353
	[ComVisible(false)]
	[DebuggerDisplay("IsCancellationRequested = {IsCancellationRequested}")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public struct CancellationToken
	{
		/// <summary>Returns an empty <see cref="T:System.Threading.CancellationToken" /> value.</summary>
		/// <returns>An empty cancellation token.</returns>
		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06003F90 RID: 16272 RVA: 0x000EDB74 File Offset: 0x000EBD74
		[__DynamicallyInvokable]
		public static CancellationToken None
		{
			[__DynamicallyInvokable]
			get
			{
				return default(CancellationToken);
			}
		}

		/// <summary>Gets whether cancellation has been requested for this token.</summary>
		/// <returns>
		///   <see langword="true" /> if cancellation has been requested for this token; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06003F91 RID: 16273 RVA: 0x000EDB8A File Offset: 0x000EBD8A
		[__DynamicallyInvokable]
		public bool IsCancellationRequested
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_source != null && this.m_source.IsCancellationRequested;
			}
		}

		/// <summary>Gets whether this token is capable of being in the canceled state.</summary>
		/// <returns>
		///   <see langword="true" /> if this token is capable of being in the canceled state; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06003F92 RID: 16274 RVA: 0x000EDBA1 File Offset: 0x000EBDA1
		[__DynamicallyInvokable]
		public bool CanBeCanceled
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_source != null && this.m_source.CanBeCanceled;
			}
		}

		/// <summary>Gets a <see cref="T:System.Threading.WaitHandle" /> that is signaled when the token is canceled.</summary>
		/// <returns>A <see cref="T:System.Threading.WaitHandle" /> that is signaled when the token is canceled.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06003F93 RID: 16275 RVA: 0x000EDBB8 File Offset: 0x000EBDB8
		[__DynamicallyInvokable]
		public WaitHandle WaitHandle
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_source == null)
				{
					this.InitializeDefaultSource();
				}
				return this.m_source.WaitHandle;
			}
		}

		// Token: 0x06003F94 RID: 16276 RVA: 0x000EDBD3 File Offset: 0x000EBDD3
		internal CancellationToken(CancellationTokenSource source)
		{
			this.m_source = source;
		}

		/// <summary>Initializes the <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="canceled">The canceled state for the token.</param>
		// Token: 0x06003F95 RID: 16277 RVA: 0x000EDBDC File Offset: 0x000EBDDC
		[__DynamicallyInvokable]
		public CancellationToken(bool canceled)
		{
			this = default(CancellationToken);
			if (canceled)
			{
				this.m_source = CancellationTokenSource.InternalGetStaticSource(canceled);
			}
		}

		// Token: 0x06003F96 RID: 16278 RVA: 0x000EDBF4 File Offset: 0x000EBDF4
		private static void ActionToActionObjShunt(object obj)
		{
			Action action = obj as Action;
			action();
		}

		/// <summary>Registers a delegate that will be called when this <see cref="T:System.Threading.CancellationToken" /> is canceled.</summary>
		/// <param name="callback">The delegate to be executed when the <see cref="T:System.Threading.CancellationToken" /> is canceled.</param>
		/// <returns>The <see cref="T:System.Threading.CancellationTokenRegistration" /> instance that can be used to unregister the callback.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callback" /> is null.</exception>
		// Token: 0x06003F97 RID: 16279 RVA: 0x000EDC0E File Offset: 0x000EBE0E
		[__DynamicallyInvokable]
		public CancellationTokenRegistration Register(Action callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			return this.Register(CancellationToken.s_ActionToActionObjShunt, callback, false, true);
		}

		/// <summary>Registers a delegate that will be called when this <see cref="T:System.Threading.CancellationToken" /> is canceled.</summary>
		/// <param name="callback">The delegate to be executed when the <see cref="T:System.Threading.CancellationToken" /> is canceled.</param>
		/// <param name="useSynchronizationContext">A value that indicates whether to capture the current <see cref="T:System.Threading.SynchronizationContext" /> and use it when invoking the <paramref name="callback" />.</param>
		/// <returns>The <see cref="T:System.Threading.CancellationTokenRegistration" /> instance that can be used to unregister the callback.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callback" /> is null.</exception>
		// Token: 0x06003F98 RID: 16280 RVA: 0x000EDC2C File Offset: 0x000EBE2C
		[__DynamicallyInvokable]
		public CancellationTokenRegistration Register(Action callback, bool useSynchronizationContext)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			return this.Register(CancellationToken.s_ActionToActionObjShunt, callback, useSynchronizationContext, true);
		}

		/// <summary>Registers a delegate that will be called when this <see cref="T:System.Threading.CancellationToken" /> is canceled.</summary>
		/// <param name="callback">The delegate to be executed when the <see cref="T:System.Threading.CancellationToken" /> is canceled.</param>
		/// <param name="state">The state to pass to the <paramref name="callback" /> when the delegate is invoked. This may be null.</param>
		/// <returns>The <see cref="T:System.Threading.CancellationTokenRegistration" /> instance that can be used to unregister the callback.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callback" /> is null.</exception>
		// Token: 0x06003F99 RID: 16281 RVA: 0x000EDC4A File Offset: 0x000EBE4A
		[__DynamicallyInvokable]
		public CancellationTokenRegistration Register(Action<object> callback, object state)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			return this.Register(callback, state, false, true);
		}

		/// <summary>Registers a delegate that will be called when this <see cref="T:System.Threading.CancellationToken" /> is canceled.</summary>
		/// <param name="callback">The delegate to be executed when the <see cref="T:System.Threading.CancellationToken" /> is canceled.</param>
		/// <param name="state">The state to pass to the <paramref name="callback" /> when the delegate is invoked. This may be null.</param>
		/// <param name="useSynchronizationContext">A Boolean value that indicates whether to capture the current <see cref="T:System.Threading.SynchronizationContext" /> and use it when invoking the <paramref name="callback" />.</param>
		/// <returns>The <see cref="T:System.Threading.CancellationTokenRegistration" /> instance that can be used to unregister the callback.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callback" /> is null.</exception>
		// Token: 0x06003F9A RID: 16282 RVA: 0x000EDC64 File Offset: 0x000EBE64
		[__DynamicallyInvokable]
		public CancellationTokenRegistration Register(Action<object> callback, object state, bool useSynchronizationContext)
		{
			return this.Register(callback, state, useSynchronizationContext, true);
		}

		// Token: 0x06003F9B RID: 16283 RVA: 0x000EDC70 File Offset: 0x000EBE70
		internal CancellationTokenRegistration InternalRegisterWithoutEC(Action<object> callback, object state)
		{
			return this.Register(callback, state, false, false);
		}

		// Token: 0x06003F9C RID: 16284 RVA: 0x000EDC7C File Offset: 0x000EBE7C
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private CancellationTokenRegistration Register(Action<object> callback, object state, bool useSynchronizationContext, bool useExecutionContext)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			if (!this.CanBeCanceled)
			{
				return default(CancellationTokenRegistration);
			}
			SynchronizationContext synchronizationContext = null;
			ExecutionContext executionContext = null;
			if (!this.IsCancellationRequested)
			{
				if (useSynchronizationContext)
				{
					synchronizationContext = SynchronizationContext.Current;
				}
				if (useExecutionContext)
				{
					executionContext = ExecutionContext.Capture(ref stackCrawlMark, ExecutionContext.CaptureOptions.OptimizeDefaultCase);
				}
			}
			return this.m_source.InternalRegister(callback, state, synchronizationContext, executionContext);
		}

		/// <summary>Determines whether the current <see cref="T:System.Threading.CancellationToken" /> instance is equal to the specified token.</summary>
		/// <param name="other">The other <see cref="T:System.Threading.CancellationToken" /> to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if the instances are equal; otherwise, <see langword="false" />. See the Remarks section for more information.</returns>
		// Token: 0x06003F9D RID: 16285 RVA: 0x000EDCDC File Offset: 0x000EBEDC
		[__DynamicallyInvokable]
		public bool Equals(CancellationToken other)
		{
			if (this.m_source == null && other.m_source == null)
			{
				return true;
			}
			if (this.m_source == null)
			{
				return other.m_source == CancellationTokenSource.InternalGetStaticSource(false);
			}
			if (other.m_source == null)
			{
				return this.m_source == CancellationTokenSource.InternalGetStaticSource(false);
			}
			return this.m_source == other.m_source;
		}

		/// <summary>Determines whether the current <see cref="T:System.Threading.CancellationToken" /> instance is equal to the specified <see cref="T:System.Object" />.</summary>
		/// <param name="other">The other object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="other" /> is a <see cref="T:System.Threading.CancellationToken" /> and if the two instances are equal; otherwise, <see langword="false" />. See the Remarks section for more information.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		// Token: 0x06003F9E RID: 16286 RVA: 0x000EDD37 File Offset: 0x000EBF37
		[__DynamicallyInvokable]
		public override bool Equals(object other)
		{
			return other is CancellationToken && this.Equals((CancellationToken)other);
		}

		/// <summary>Serves as a hash function for a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Threading.CancellationToken" /> instance.</returns>
		// Token: 0x06003F9F RID: 16287 RVA: 0x000EDD4F File Offset: 0x000EBF4F
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			if (this.m_source == null)
			{
				return CancellationTokenSource.InternalGetStaticSource(false).GetHashCode();
			}
			return this.m_source.GetHashCode();
		}

		/// <summary>Determines whether two <see cref="T:System.Threading.CancellationToken" /> instances are equal.</summary>
		/// <param name="left">The first instance.</param>
		/// <param name="right">The second instance.</param>
		/// <returns>
		///   <see langword="true" /> if the instances are equal; otherwise, <see langword="false" /> See the Remarks section for more information.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		// Token: 0x06003FA0 RID: 16288 RVA: 0x000EDD70 File Offset: 0x000EBF70
		[__DynamicallyInvokable]
		public static bool operator ==(CancellationToken left, CancellationToken right)
		{
			return left.Equals(right);
		}

		/// <summary>Determines whether two <see cref="T:System.Threading.CancellationToken" /> instances are not equal.</summary>
		/// <param name="left">The first instance.</param>
		/// <param name="right">The second instance.</param>
		/// <returns>
		///   <see langword="true" /> if the instances are not equal; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		// Token: 0x06003FA1 RID: 16289 RVA: 0x000EDD7A File Offset: 0x000EBF7A
		[__DynamicallyInvokable]
		public static bool operator !=(CancellationToken left, CancellationToken right)
		{
			return !left.Equals(right);
		}

		/// <summary>Throws a <see cref="T:System.OperationCanceledException" /> if this token has had cancellation requested.</summary>
		/// <exception cref="T:System.OperationCanceledException">The token has had cancellation requested.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		// Token: 0x06003FA2 RID: 16290 RVA: 0x000EDD87 File Offset: 0x000EBF87
		[__DynamicallyInvokable]
		public void ThrowIfCancellationRequested()
		{
			if (this.IsCancellationRequested)
			{
				this.ThrowOperationCanceledException();
			}
		}

		// Token: 0x06003FA3 RID: 16291 RVA: 0x000EDD97 File Offset: 0x000EBF97
		internal void ThrowIfSourceDisposed()
		{
			if (this.m_source != null && this.m_source.IsDisposed)
			{
				CancellationToken.ThrowObjectDisposedException();
			}
		}

		// Token: 0x06003FA4 RID: 16292 RVA: 0x000EDDB3 File Offset: 0x000EBFB3
		private void ThrowOperationCanceledException()
		{
			throw new OperationCanceledException(Environment.GetResourceString("OperationCanceled"), this);
		}

		// Token: 0x06003FA5 RID: 16293 RVA: 0x000EDDCA File Offset: 0x000EBFCA
		private static void ThrowObjectDisposedException()
		{
			throw new ObjectDisposedException(null, Environment.GetResourceString("CancellationToken_SourceDisposed"));
		}

		// Token: 0x06003FA6 RID: 16294 RVA: 0x000EDDDC File Offset: 0x000EBFDC
		private void InitializeDefaultSource()
		{
			this.m_source = CancellationTokenSource.InternalGetStaticSource(false);
		}

		// Token: 0x04001AC4 RID: 6852
		private CancellationTokenSource m_source;

		// Token: 0x04001AC5 RID: 6853
		private static readonly Action<object> s_ActionToActionObjShunt = new Action<object>(CancellationToken.ActionToActionObjShunt);
	}
}
