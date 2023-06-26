using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a set of recommended default values during component creation.</summary>
	// Token: 0x020005E4 RID: 1508
	public interface IComponentInitializer
	{
		/// <summary>Restores an instance of a component to its default state.</summary>
		/// <param name="defaultValues">A dictionary of default property values, which are name/value pairs, with which to reset the component's state.</param>
		// Token: 0x060037D8 RID: 14296
		void InitializeExistingComponent(IDictionary defaultValues);

		/// <summary>Initializes a new component using a set of recommended values.</summary>
		/// <param name="defaultValues">A dictionary of default property values, which are name/value pairs, with which to initialize the component's state.</param>
		// Token: 0x060037D9 RID: 14297
		void InitializeNewComponent(IDictionary defaultValues);
	}
}
