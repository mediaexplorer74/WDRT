using System;

namespace System.ComponentModel
{
	/// <summary>Allows coordination of initialization for a component and its dependent properties.</summary>
	// Token: 0x02000577 RID: 1399
	public interface ISupportInitializeNotification : ISupportInitialize
	{
		/// <summary>Gets a value indicating whether the component is initialized.</summary>
		/// <returns>
		///   <see langword="true" /> to indicate the component has completed initialization; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x060033D6 RID: 13270
		bool IsInitialized { get; }

		/// <summary>Occurs when initialization of the component is completed.</summary>
		// Token: 0x14000050 RID: 80
		// (add) Token: 0x060033D7 RID: 13271
		// (remove) Token: 0x060033D8 RID: 13272
		event EventHandler Initialized;
	}
}
