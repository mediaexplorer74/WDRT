using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a service for registering event handlers for component events.</summary>
	// Token: 0x020005EC RID: 1516
	[ComVisible(true)]
	public interface IEventBindingService
	{
		/// <summary>Creates a unique name for an event-handler method for the specified component and event.</summary>
		/// <param name="component">The component instance the event is connected to.</param>
		/// <param name="e">The event to create a name for.</param>
		/// <returns>The recommended name for the event-handler method for this event.</returns>
		// Token: 0x06003810 RID: 14352
		string CreateUniqueMethodName(IComponent component, EventDescriptor e);

		/// <summary>Gets a collection of event-handler methods that have a method signature compatible with the specified event.</summary>
		/// <param name="e">The event to get the compatible event-handler methods for.</param>
		/// <returns>A collection of strings.</returns>
		// Token: 0x06003811 RID: 14353
		ICollection GetCompatibleMethods(EventDescriptor e);

		/// <summary>Gets an <see cref="T:System.ComponentModel.EventDescriptor" /> for the event that the specified property descriptor represents, if it represents an event.</summary>
		/// <param name="property">The property that represents an event.</param>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> for the event that the property represents, or <see langword="null" /> if the property does not represent an event.</returns>
		// Token: 0x06003812 RID: 14354
		EventDescriptor GetEvent(PropertyDescriptor property);

		/// <summary>Converts a set of event descriptors to a set of property descriptors.</summary>
		/// <param name="events">The events to convert to properties.</param>
		/// <returns>An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects that describe the event set.</returns>
		// Token: 0x06003813 RID: 14355
		PropertyDescriptorCollection GetEventProperties(EventDescriptorCollection events);

		/// <summary>Converts a single event descriptor to a property descriptor.</summary>
		/// <param name="e">The event to convert.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that describes the event.</returns>
		// Token: 0x06003814 RID: 14356
		PropertyDescriptor GetEventProperty(EventDescriptor e);

		/// <summary>Displays the user code for the designer.</summary>
		/// <returns>
		///   <see langword="true" /> if the code is displayed; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003815 RID: 14357
		bool ShowCode();

		/// <summary>Displays the user code for the designer at the specified line.</summary>
		/// <param name="lineNumber">The line number to place the caret on.</param>
		/// <returns>
		///   <see langword="true" /> if the code is displayed; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003816 RID: 14358
		bool ShowCode(int lineNumber);

		/// <summary>Displays the user code for the specified event.</summary>
		/// <param name="component">The component that the event is connected to.</param>
		/// <param name="e">The event to display.</param>
		/// <returns>
		///   <see langword="true" /> if the code is displayed; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003817 RID: 14359
		bool ShowCode(IComponent component, EventDescriptor e);
	}
}
