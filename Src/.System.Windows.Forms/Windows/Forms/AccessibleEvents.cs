using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies events that are reported by accessible applications.</summary>
	// Token: 0x02000115 RID: 277
	public enum AccessibleEvents
	{
		/// <summary>A sound was played. The system sends this event when a system sound, such as for menus, is played, even if no sound is audible. This might be caused by lack of a sound file or sound card. Servers send this event if a custom user interface element generates a sound.</summary>
		// Token: 0x040004FD RID: 1277
		SystemSound = 1,
		/// <summary>An alert was generated. Server applications send this event whenever an important user interface change has occurred that a user might need to know about. The system does not send the event consistently for dialog box objects.</summary>
		// Token: 0x040004FE RID: 1278
		SystemAlert,
		/// <summary>The foreground window changed. The system sends this event even if the foreground window is changed to another window in the same thread. Server applications never send this event.</summary>
		// Token: 0x040004FF RID: 1279
		SystemForeground,
		/// <summary>A menu item on the menu bar was selected. The system sends this event for standard menus. Servers send this event for custom menus. The system might raise more than one <see langword="MenuStart" /> event that might or might not have a corresponding <see langword="MenuEnd" /> event.</summary>
		// Token: 0x04000500 RID: 1280
		SystemMenuStart,
		/// <summary>A menu from the menu bar was closed. The system sends this event for standard menus. Servers send this event for custom menus.</summary>
		// Token: 0x04000501 RID: 1281
		SystemMenuEnd,
		/// <summary>A shortcut menu was displayed. The system sends this event for standard menus. Servers send this event for custom menus. The system does not send the event consistently.</summary>
		// Token: 0x04000502 RID: 1282
		SystemMenuPopupStart,
		/// <summary>A shortcut menu was closed. The system sends this event for standard menus. Servers send this event for custom menus. When a shortcut menu is closed, the client receives this message followed almost immediately by the <see langword="SystemMenuEnd" /> event. The system does not send the event consistently.</summary>
		// Token: 0x04000503 RID: 1283
		SystemMenuPopupEnd,
		/// <summary>A window is being moved or resized. The system sends the event; servers never send this event.</summary>
		// Token: 0x04000504 RID: 1284
		SystemCaptureStart,
		/// <summary>A window has lost mouse capture. The system sends the event; servers never send this event.</summary>
		// Token: 0x04000505 RID: 1285
		SystemCaptureEnd,
		/// <summary>A window is being moved or resized. The system sends the event; servers never send this event.</summary>
		// Token: 0x04000506 RID: 1286
		SystemMoveSizeStart,
		/// <summary>The movement or resizing of a window is finished. The system sends the event; servers never send this event.</summary>
		// Token: 0x04000507 RID: 1287
		SystemMoveSizeEnd,
		/// <summary>A window entered context-sensitive Help mode. The system does not send the event consistently.</summary>
		// Token: 0x04000508 RID: 1288
		SystemContextHelpStart,
		/// <summary>A window exited context-sensitive Help mode. The system does not send the event consistently.</summary>
		// Token: 0x04000509 RID: 1289
		SystemContextHelpEnd,
		/// <summary>An application is about to enter drag-and-drop mode. Applications that support drag-and-drop operations must send this event; the system does not.</summary>
		// Token: 0x0400050A RID: 1290
		SystemDragDropStart,
		/// <summary>An application is about to exit drag-and-drop mode. Applications that support drag-and-drop operations must send this event; the system does not.</summary>
		// Token: 0x0400050B RID: 1291
		SystemDragDropEnd,
		/// <summary>A dialog box was displayed. The system sends the event for standard dialog boxes. Servers send this event for custom dialog boxes. The system does not send the event consistently.</summary>
		// Token: 0x0400050C RID: 1292
		SystemDialogStart,
		/// <summary>A dialog box was closed. The system does not send the event for standard dialog boxes. Servers send this event for custom dialog boxes. The system does not send the event consistently.</summary>
		// Token: 0x0400050D RID: 1293
		SystemDialogEnd,
		/// <summary>Scrolling has started on a scroll bar. The system sends the event for scroll bars attached to a window and for standard scroll bar controls. Servers send this event for custom scroll bars.</summary>
		// Token: 0x0400050E RID: 1294
		SystemScrollingStart,
		/// <summary>Scrolling has ended on a scroll bar. The system sends this event for scroll bars attached to a window and for standard scroll bar controls. Servers send this event for custom scroll bars.</summary>
		// Token: 0x0400050F RID: 1295
		SystemScrollingEnd,
		/// <summary>The user pressed ALT+TAB, which activates the switch window. If only one application is running when the user presses ALT+TAB, the system raises the <see langword="SwitchEnd" /> event without a corresponding <see langword="SwitchStart" /> event.</summary>
		// Token: 0x04000510 RID: 1296
		SystemSwitchStart,
		/// <summary>The user released ALT+TAB. The system sends the <see langword="SwitchEnd" /> event; servers never send this event. If only one application is running when the user presses ALT+TAB, the system sends the <see langword="SwitchEnd" /> event without a corresponding <see langword="SwitchStart" /> event.</summary>
		// Token: 0x04000511 RID: 1297
		SystemSwitchEnd,
		/// <summary>A window object is about to be minimized or maximized. The system sends the event; servers never send this event.</summary>
		// Token: 0x04000512 RID: 1298
		SystemMinimizeStart,
		/// <summary>A window object was minimized or maximized. The system sends the event; servers never send this event.</summary>
		// Token: 0x04000513 RID: 1299
		SystemMinimizeEnd,
		/// <summary>An object was created. The operating system sends the event for the following user interface elements: caret, header control, list view control, tab control, toolbar control, tree view control, and window object. Server applications send this event for their accessible objects. Servers must send this event for all an object's child objects before sending the event for the parent object. Servers must ensure that all child objects are fully created and ready to accept calls from clients when the parent object sends the event.</summary>
		// Token: 0x04000514 RID: 1300
		Create = 32768,
		/// <summary>An object was destroyed. The system sends this event for the following user interface elements: caret, header control, list view control, tab control, toolbar control, tree view control, and window object. Server applications send this event for their accessible objects. This event may or may not be sent for child objects. However, clients can conclude that all the children of an object have been destroyed when the parent object sends this event.</summary>
		// Token: 0x04000515 RID: 1301
		Destroy,
		/// <summary>A hidden object is being shown. The system sends this event for the following user interface elements: caret, cursor, and window object. Server applications send this event for their accessible objects. Clients can conclude that, when this event is sent by a parent object, all child objects have already been displayed. Therefore, server applications do not need to send this event for the child objects.</summary>
		// Token: 0x04000516 RID: 1302
		Show,
		/// <summary>An object is hidden. The system sends the event for the following user interface elements: caret and cursor. Server applications send the event for their accessible objects. When the event is generated for a parent object, all child objects have already been hidden. Therefore, server applications do not need to send the event for the child objects. The system does not send the event consistently.</summary>
		// Token: 0x04000517 RID: 1303
		Hide,
		/// <summary>A container object has added, removed, or reordered its children. The system sends this event for the following user interface elements: header control, list view control, toolbar control, and window object. Server applications send this event as appropriate for their accessible objects. This event is also sent by a parent window when the z order for the child windows changes.</summary>
		// Token: 0x04000518 RID: 1304
		Reorder,
		/// <summary>An object has received the keyboard focus. The system sends this event for the following user interface elements: list view control, menu bar, shortcut menu, switch window, tab control, tree view control, and window object. Server applications send this event for their accessible objects.</summary>
		// Token: 0x04000519 RID: 1305
		Focus,
		/// <summary>An accessible object within a container object has been selected. This event signals a single selection. Either a child has been selected in a container that previously did not contain any selected children, or the selection has changed from one child to another.</summary>
		// Token: 0x0400051A RID: 1306
		Selection,
		/// <summary>An item within a container object was added to the selection. The system sends this event for the following user interface elements: list box, list view control, and tree view control. Server applications send this event for their accessible objects. This event signals that a child has been added to an existing selection.</summary>
		// Token: 0x0400051B RID: 1307
		SelectionAdd,
		/// <summary>An item within a container object was removed from the selection. The system sends this event for the following user interface elements: list box, list view control, and tree view control. Server applications send this event for their accessible objects. This event signals that a child has been removed from an existing selection.</summary>
		// Token: 0x0400051C RID: 1308
		SelectionRemove,
		/// <summary>Numerous selection changes occurred within a container object. The system sends this event for list boxes. Server applications send this event for their accessible objects. This event can be sent when the selected items within a control have changed substantially. This event informs the client that many selection changes have occurred. This is preferable to sending several <see langword="SelectionAdd" /> or <see langword="SelectionRemove" /> events.</summary>
		// Token: 0x0400051D RID: 1309
		SelectionWithin,
		/// <summary>An object's state has changed. The system sends the event for the following user interface elements: check box, combo box, header control, push button, radio button, scroll bar, toolbar control, tree view control, up-down control, and window object. Server applications send the event for their accessible objects. For example, a state change can occur when a button object has been pressed or released, or when an object is being enabled or disabled. The system does not send the event consistently.</summary>
		// Token: 0x0400051E RID: 1310
		StateChange,
		/// <summary>An object has changed location, shape, or size. The system sends this event for the following user interface elements: caret and window object. Server applications send this event for their accessible objects. This event is generated in response to the top-level object within the object hierarchy that has changed, not for any children it might contain. For example, if the user resizes a window, the system sends this notification for the window, but not for the menu bar, title bar, scroll bars, or other objects that have also changed. The system does not send this event for every non-floating child window when the parent moves. However, if an application explicitly resizes child windows as a result of being resized, the system sends multiple events for the resized children. If an object's <see cref="P:System.Windows.Forms.AccessibleObject.State" /> property is set to <see cref="F:System.Windows.Forms.AccessibleStates.Floating" />, servers should send a location change event whenever the object changes location. If an object does not have this state, servers should raise this event when the object moves relative to its parent.</summary>
		// Token: 0x0400051F RID: 1311
		LocationChange,
		/// <summary>An object's <see cref="P:System.Windows.Forms.AccessibleObject.Name" /> property changed. The system sends this event for the following user interface elements: check box, cursor, list view control, push button, radio button, status bar control, tree view control, and window object. Server applications send this event for their accessible objects.</summary>
		// Token: 0x04000520 RID: 1312
		NameChange,
		/// <summary>An object's <see cref="P:System.Windows.Forms.AccessibleObject.Description" /> property changed. Server applications send this event for their accessible objects.</summary>
		// Token: 0x04000521 RID: 1313
		DescriptionChange,
		/// <summary>An object's <see cref="P:System.Windows.Forms.AccessibleObject.Value" /> property changed. The system raises the <see langword="ValueChange" /> event for the following user interface elements: edit control, header control, hot key control, progress bar control, scroll bar, slider control, and up-down control. Server applications send this event for their accessible objects.</summary>
		// Token: 0x04000522 RID: 1314
		ValueChange,
		/// <summary>An object has a new parent object. Server applications send this event for their accessible objects.</summary>
		// Token: 0x04000523 RID: 1315
		ParentChange,
		/// <summary>An object's <see cref="P:System.Windows.Forms.AccessibleObject.Help" /> property changed. Server applications send this event for their accessible objects.</summary>
		// Token: 0x04000524 RID: 1316
		HelpChange,
		/// <summary>An object's <see cref="P:System.Windows.Forms.AccessibleObject.DefaultAction" /> property changed. The system sends this event for dialog boxes. Server applications send this event for their accessible objects. Therefore, server applications do not need to send this event for the child objects. Hidden objects have a state of <see cref="F:System.Windows.Forms.AccessibleStates.Invisible" />, and shown objects do not. Events of type <see langword="AccessibleEvents.Hide" /> indicate that a state of <see cref="F:System.Windows.Forms.AccessibleStates.Invisible" /> has been set. Therefore, servers do not need to send the <see langword="AccessibleEvents.StateChange" /> event in this case.</summary>
		// Token: 0x04000525 RID: 1317
		DefaultActionChange,
		/// <summary>An object's <see cref="P:System.Windows.Forms.AccessibleObject.KeyboardShortcut" /> property changed. Server applications send the event for their accessible objects.</summary>
		// Token: 0x04000526 RID: 1318
		AcceleratorChange
	}
}
