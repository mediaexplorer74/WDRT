using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	/// <summary>Provides support for building a set of related custom designers.</summary>
	// Token: 0x020005F7 RID: 1527
	public interface ITreeDesigner : IDesigner, IDisposable
	{
		/// <summary>Gets a collection of child designers.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" />, containing the collection of <see cref="T:System.ComponentModel.Design.IDesigner" /> child objects of the current designer.</returns>
		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x06003845 RID: 14405
		ICollection Children { get; }

		/// <summary>Gets the parent designer.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesigner" /> representing the parent designer, or <see langword="null" /> if there is no parent.</returns>
		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x06003846 RID: 14406
		IDesigner Parent { get; }
	}
}
