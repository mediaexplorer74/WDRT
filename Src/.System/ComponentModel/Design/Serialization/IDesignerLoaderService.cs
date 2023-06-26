using System;
using System.Collections;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides an interface that can extend a designer loader to support asynchronous loading of external components.</summary>
	// Token: 0x02000609 RID: 1545
	public interface IDesignerLoaderService
	{
		/// <summary>Registers an external component as part of the load process managed by this interface.</summary>
		// Token: 0x060038A3 RID: 14499
		void AddLoadDependency();

		/// <summary>Signals that a dependent load has finished.</summary>
		/// <param name="successful">
		///   <see langword="true" /> if the load of the designer is successful; <see langword="false" /> if errors prevented the load from finishing.</param>
		/// <param name="errorCollection">A collection of errors that occurred during the load, if any. If no errors occurred, pass either an empty collection or <see langword="null" />.</param>
		// Token: 0x060038A4 RID: 14500
		void DependentLoadComplete(bool successful, ICollection errorCollection);

		/// <summary>Reloads the design document.</summary>
		/// <returns>
		///   <see langword="true" /> if the reload request is accepted, or <see langword="false" /> if the loader does not allow the reload.</returns>
		// Token: 0x060038A5 RID: 14501
		bool Reload();
	}
}
