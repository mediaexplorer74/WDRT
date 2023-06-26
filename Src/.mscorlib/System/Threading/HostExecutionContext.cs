using System;
using System.Security;

namespace System.Threading
{
	/// <summary>Encapsulates and propagates the host execution context across threads.</summary>
	// Token: 0x020004F9 RID: 1273
	public class HostExecutionContext : IDisposable
	{
		/// <summary>Gets or sets the state of the host execution context.</summary>
		/// <returns>An object representing the host execution context state.</returns>
		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06003C55 RID: 15445 RVA: 0x000E5705 File Offset: 0x000E3905
		// (set) Token: 0x06003C56 RID: 15446 RVA: 0x000E570D File Offset: 0x000E390D
		protected internal object State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.HostExecutionContext" /> class.</summary>
		// Token: 0x06003C57 RID: 15447 RVA: 0x000E5716 File Offset: 0x000E3916
		public HostExecutionContext()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.HostExecutionContext" /> class using the specified state.</summary>
		/// <param name="state">An object representing the host execution context state.</param>
		// Token: 0x06003C58 RID: 15448 RVA: 0x000E571E File Offset: 0x000E391E
		public HostExecutionContext(object state)
		{
			this.state = state;
		}

		/// <summary>Creates a copy of the current host execution context.</summary>
		/// <returns>A <see cref="T:System.Threading.HostExecutionContext" /> object representing the host context for the current thread.</returns>
		// Token: 0x06003C59 RID: 15449 RVA: 0x000E5730 File Offset: 0x000E3930
		[SecuritySafeCritical]
		public virtual HostExecutionContext CreateCopy()
		{
			object obj = this.state;
			if (this.state is IUnknownSafeHandle)
			{
				obj = ((IUnknownSafeHandle)this.state).Clone();
			}
			return new HostExecutionContext(this.state);
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.HostExecutionContext" /> class.</summary>
		// Token: 0x06003C5A RID: 15450 RVA: 0x000E576D File Offset: 0x000E396D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>When overridden in a derived class, releases the unmanaged resources used by the <see cref="T:System.Threading.WaitHandle" />, and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06003C5B RID: 15451 RVA: 0x000E577C File Offset: 0x000E397C
		public virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x040019A3 RID: 6563
		private object state;
	}
}
