using System;

namespace System.Management
{
	/// <summary>Specifies options for management event watching.</summary>
	// Token: 0x0200002D RID: 45
	public class EventWatcherOptions : ManagementOptions
	{
		/// <summary>Gets or sets the block size for block operations. When waiting for events, this value specifies how many events to wait for before returning.</summary>
		/// <returns>An integer value indicating the block size for a block of operations.</returns>
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600015E RID: 350 RVA: 0x000083E7 File Offset: 0x000065E7
		// (set) Token: 0x0600015F RID: 351 RVA: 0x000083EF File Offset: 0x000065EF
		public int BlockSize
		{
			get
			{
				return this.blockSize;
			}
			set
			{
				this.blockSize = value;
				base.FireIdentifierChanged();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.EventWatcherOptions" /> class for event watching, using default values. This is the default constructor.</summary>
		// Token: 0x06000160 RID: 352 RVA: 0x000083FE File Offset: 0x000065FE
		public EventWatcherOptions()
			: this(null, ManagementOptions.InfiniteTimeout, 1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.EventWatcherOptions" /> class with the given values.</summary>
		/// <param name="context">The options context object containing provider-specific information to be passed through to the provider.</param>
		/// <param name="timeout">The time-out to wait for the next events.</param>
		/// <param name="blockSize">The number of events to wait for in each block.</param>
		// Token: 0x06000161 RID: 353 RVA: 0x0000840D File Offset: 0x0000660D
		public EventWatcherOptions(ManagementNamedValueCollection context, TimeSpan timeout, int blockSize)
			: base(context, timeout)
		{
			base.Flags = 48;
			this.BlockSize = blockSize;
		}

		/// <summary>Returns a copy of the object.</summary>
		/// <returns>The cloned object.</returns>
		// Token: 0x06000162 RID: 354 RVA: 0x00008430 File Offset: 0x00006630
		public override object Clone()
		{
			ManagementNamedValueCollection managementNamedValueCollection = null;
			if (base.Context != null)
			{
				managementNamedValueCollection = base.Context.Clone();
			}
			return new EventWatcherOptions(managementNamedValueCollection, base.Timeout, this.blockSize);
		}

		// Token: 0x0400013B RID: 315
		private int blockSize = 1;
	}
}
