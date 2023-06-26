using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides an interface that extends <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderHost" /> to specify whether errors are tolerated while loading a design document.</summary>
	// Token: 0x02000608 RID: 1544
	public interface IDesignerLoaderHost2 : IDesignerLoaderHost, IDesignerHost, IServiceContainer, IServiceProvider
	{
		/// <summary>Gets or sets a value indicating whether errors should be ignored when <see cref="M:System.ComponentModel.Design.Serialization.IDesignerLoaderHost.Reload" /> is called.</summary>
		/// <returns>
		///   <see langword="true" /> if the designer loader will ignore errors when it reloads; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x0600389F RID: 14495
		// (set) Token: 0x060038A0 RID: 14496
		bool IgnoreErrorsDuringReload { get; set; }

		/// <summary>Gets or sets a value indicating whether it is possible to reload with errors.</summary>
		/// <returns>
		///   <see langword="true" /> if the designer loader can reload the design document when errors are detected; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x060038A1 RID: 14497
		// (set) Token: 0x060038A2 RID: 14498
		bool CanReloadWithErrors { get; set; }
	}
}
