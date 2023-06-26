using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides event notifications when root designers are added and removed, when a selected component changes, and when the current root designer changes.</summary>
	// Token: 0x020005E6 RID: 1510
	public interface IDesignerEventService
	{
		/// <summary>Gets the root designer for the currently active document.</summary>
		/// <returns>The currently active document, or <see langword="null" /> if there is no active document.</returns>
		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x060037DE RID: 14302
		IDesignerHost ActiveDesigner { get; }

		/// <summary>Gets a collection of root designers for design documents that are currently active in the development environment.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerCollection" /> containing the root designers that have been created and not yet disposed.</returns>
		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x060037DF RID: 14303
		DesignerCollection Designers { get; }

		/// <summary>Occurs when the current root designer changes.</summary>
		// Token: 0x1400005A RID: 90
		// (add) Token: 0x060037E0 RID: 14304
		// (remove) Token: 0x060037E1 RID: 14305
		event ActiveDesignerEventHandler ActiveDesignerChanged;

		/// <summary>Occurs when a root designer is created.</summary>
		// Token: 0x1400005B RID: 91
		// (add) Token: 0x060037E2 RID: 14306
		// (remove) Token: 0x060037E3 RID: 14307
		event DesignerEventHandler DesignerCreated;

		/// <summary>Occurs when a root designer for a document is disposed.</summary>
		// Token: 0x1400005C RID: 92
		// (add) Token: 0x060037E4 RID: 14308
		// (remove) Token: 0x060037E5 RID: 14309
		event DesignerEventHandler DesignerDisposed;

		/// <summary>Occurs when the current design-view selection changes.</summary>
		// Token: 0x1400005D RID: 93
		// (add) Token: 0x060037E6 RID: 14310
		// (remove) Token: 0x060037E7 RID: 14311
		event EventHandler SelectionChanged;
	}
}
