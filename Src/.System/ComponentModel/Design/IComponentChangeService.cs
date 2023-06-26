using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface to add and remove the event handlers for events that add, change, remove or rename components, and provides methods to raise a <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanged" /> or <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanging" /> event.</summary>
	// Token: 0x020005E2 RID: 1506
	[ComVisible(true)]
	public interface IComponentChangeService
	{
		/// <summary>Occurs when a component has been added.</summary>
		// Token: 0x14000053 RID: 83
		// (add) Token: 0x060037C7 RID: 14279
		// (remove) Token: 0x060037C8 RID: 14280
		event ComponentEventHandler ComponentAdded;

		/// <summary>Occurs when a component is in the process of being added.</summary>
		// Token: 0x14000054 RID: 84
		// (add) Token: 0x060037C9 RID: 14281
		// (remove) Token: 0x060037CA RID: 14282
		event ComponentEventHandler ComponentAdding;

		/// <summary>Occurs when a component has been changed.</summary>
		// Token: 0x14000055 RID: 85
		// (add) Token: 0x060037CB RID: 14283
		// (remove) Token: 0x060037CC RID: 14284
		event ComponentChangedEventHandler ComponentChanged;

		/// <summary>Occurs when a component is in the process of being changed.</summary>
		// Token: 0x14000056 RID: 86
		// (add) Token: 0x060037CD RID: 14285
		// (remove) Token: 0x060037CE RID: 14286
		event ComponentChangingEventHandler ComponentChanging;

		/// <summary>Occurs when a component has been removed.</summary>
		// Token: 0x14000057 RID: 87
		// (add) Token: 0x060037CF RID: 14287
		// (remove) Token: 0x060037D0 RID: 14288
		event ComponentEventHandler ComponentRemoved;

		/// <summary>Occurs when a component is in the process of being removed.</summary>
		// Token: 0x14000058 RID: 88
		// (add) Token: 0x060037D1 RID: 14289
		// (remove) Token: 0x060037D2 RID: 14290
		event ComponentEventHandler ComponentRemoving;

		/// <summary>Occurs when a component is renamed.</summary>
		// Token: 0x14000059 RID: 89
		// (add) Token: 0x060037D3 RID: 14291
		// (remove) Token: 0x060037D4 RID: 14292
		event ComponentRenameEventHandler ComponentRename;

		/// <summary>Announces to the component change service that a particular component has changed.</summary>
		/// <param name="component">The component that has changed.</param>
		/// <param name="member">The member that has changed. This is <see langword="null" /> if this change is not related to a single member.</param>
		/// <param name="oldValue">The old value of the member. This is valid only if the member is not <see langword="null" />.</param>
		/// <param name="newValue">The new value of the member. This is valid only if the member is not <see langword="null" />.</param>
		// Token: 0x060037D5 RID: 14293
		void OnComponentChanged(object component, MemberDescriptor member, object oldValue, object newValue);

		/// <summary>Announces to the component change service that a particular component is changing.</summary>
		/// <param name="component">The component that is about to change.</param>
		/// <param name="member">The member that is changing. This is <see langword="null" /> if this change is not related to a single member.</param>
		// Token: 0x060037D6 RID: 14294
		void OnComponentChanging(object component, MemberDescriptor member);
	}
}
