using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides the basic framework for building a custom designer.</summary>
	// Token: 0x020005E5 RID: 1509
	[ComVisible(true)]
	public interface IDesigner : IDisposable
	{
		/// <summary>Gets the base component that this designer is designing.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.IComponent" /> indicating the base component that this designer is designing.</returns>
		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x060037DA RID: 14298
		IComponent Component { get; }

		/// <summary>Gets a collection of the design-time verbs supported by the designer.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" /> that contains the verbs supported by the designer, or <see langword="null" /> if the component has no verbs.</returns>
		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x060037DB RID: 14299
		DesignerVerbCollection Verbs { get; }

		/// <summary>Performs the default action for this designer.</summary>
		// Token: 0x060037DC RID: 14300
		void DoDefaultAction();

		/// <summary>Initializes the designer with the specified component.</summary>
		/// <param name="component">The component to associate with this designer.</param>
		// Token: 0x060037DD RID: 14301
		void Initialize(IComponent component);
	}
}
