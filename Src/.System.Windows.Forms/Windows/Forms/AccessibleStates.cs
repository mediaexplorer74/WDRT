using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies values representing possible states for an accessible object.</summary>
	// Token: 0x0200011B RID: 283
	[Flags]
	public enum AccessibleStates
	{
		/// <summary>No state.</summary>
		// Token: 0x0400059B RID: 1435
		None = 0,
		/// <summary>An unavailable object.</summary>
		// Token: 0x0400059C RID: 1436
		Unavailable = 1,
		/// <summary>A selected object.</summary>
		// Token: 0x0400059D RID: 1437
		Selected = 2,
		/// <summary>An object with the keyboard focus.</summary>
		// Token: 0x0400059E RID: 1438
		Focused = 4,
		/// <summary>A pressed object.</summary>
		// Token: 0x0400059F RID: 1439
		Pressed = 8,
		/// <summary>An object with a selected check box.</summary>
		// Token: 0x040005A0 RID: 1440
		Checked = 16,
		/// <summary>A three-state check box or toolbar button whose state is indeterminate. The check box is neither checked nor unchecked, and it is in the third or mixed state.</summary>
		// Token: 0x040005A1 RID: 1441
		Mixed = 32,
		/// <summary>A three-state check box or toolbar button whose state is indeterminate. The check box is neither checked nor unchecked, and it is in the third or mixed state.</summary>
		// Token: 0x040005A2 RID: 1442
		Indeterminate = 32,
		/// <summary>A read-only object.</summary>
		// Token: 0x040005A3 RID: 1443
		ReadOnly = 64,
		/// <summary>The object hot-tracked by the mouse, meaning its appearance is highlighted to indicate the mouse pointer is located over it.</summary>
		// Token: 0x040005A4 RID: 1444
		HotTracked = 128,
		/// <summary>The default button or menu item.</summary>
		// Token: 0x040005A5 RID: 1445
		Default = 256,
		/// <summary>The displayed children of the object that are items in an outline or tree structure.</summary>
		// Token: 0x040005A6 RID: 1446
		Expanded = 512,
		/// <summary>The hidden children of the object that are items in an outline or tree structure.</summary>
		// Token: 0x040005A7 RID: 1447
		Collapsed = 1024,
		/// <summary>A control that cannot accept input in its current condition.</summary>
		// Token: 0x040005A8 RID: 1448
		Busy = 2048,
		/// <summary>The object that is not fixed to the boundary of its parent object and that does not move automatically along with the parent.</summary>
		// Token: 0x040005A9 RID: 1449
		Floating = 4096,
		/// <summary>An object with scrolling or moving text or graphics.</summary>
		// Token: 0x040005AA RID: 1450
		Marqueed = 8192,
		/// <summary>The object that rapidly or constantly changes appearance. Graphics that are occasionally animated, but not always, should be defined as <see cref="F:System.Windows.Forms.AccessibleRole.Graphic" /><see langword="OR" /><see langword="Animated" />. This state should not be used to indicate that the object's location is changing.</summary>
		// Token: 0x040005AB RID: 1451
		Animated = 16384,
		/// <summary>An object without a visible user interface.</summary>
		// Token: 0x040005AC RID: 1452
		Invisible = 32768,
		/// <summary>No on-screen representation. A sound or alert object would have this state, or a hidden window that is never made visible.</summary>
		// Token: 0x040005AD RID: 1453
		Offscreen = 65536,
		/// <summary>A sizable object.</summary>
		// Token: 0x040005AE RID: 1454
		Sizeable = 131072,
		/// <summary>A movable object.</summary>
		// Token: 0x040005AF RID: 1455
		Moveable = 262144,
		/// <summary>The object or child can use text-to-speech (TTS) to describe itself. A speech-based accessibility aid should not announce information when an object with this state has the focus, because the object automatically announces information about itself.</summary>
		// Token: 0x040005B0 RID: 1456
		SelfVoicing = 524288,
		/// <summary>The object on the active window that can receive keyboard focus.</summary>
		// Token: 0x040005B1 RID: 1457
		Focusable = 1048576,
		/// <summary>An object that can accept selection.</summary>
		// Token: 0x040005B2 RID: 1458
		Selectable = 2097152,
		/// <summary>A linked object that has not been previously selected.</summary>
		// Token: 0x040005B3 RID: 1459
		Linked = 4194304,
		/// <summary>A linked object that has previously been selected.</summary>
		// Token: 0x040005B4 RID: 1460
		Traversed = 8388608,
		/// <summary>An object that accepts multiple selected items.</summary>
		// Token: 0x040005B5 RID: 1461
		MultiSelectable = 16777216,
		/// <summary>The altered selection such that all objects between the selection anchor, which is the object with the keyboard focus, and this object take on the anchor object's selection state. If the anchor object is not selected, the objects are removed from the selection. If the anchor object is selected, the selection is extended to include this object and all objects in between. You can set the selection state by combining this with <see cref="F:System.Windows.Forms.AccessibleSelection.AddSelection" /> or <see cref="F:System.Windows.Forms.AccessibleSelection.RemoveSelection" />. This state does not change the focus or the selection anchor unless it is combined with <see cref="F:System.Windows.Forms.AccessibleSelection.TakeFocus" />.</summary>
		// Token: 0x040005B6 RID: 1462
		ExtSelectable = 33554432,
		/// <summary>The low-priority information that might not be important to the user.</summary>
		// Token: 0x040005B7 RID: 1463
		AlertLow = 67108864,
		/// <summary>The important information that does not need to be conveyed to the user immediately. For example, when a battery-level indicator is starting to reach a low level, it could generate a medium-level alert. Blind access utilities could then generate a sound to let the user know that important information is available, without actually interrupting the user's work. Users can then query the alert information any time they choose.</summary>
		// Token: 0x040005B8 RID: 1464
		AlertMedium = 134217728,
		/// <summary>The important information that should be conveyed to the user immediately. For example, a battery-level indicator reaching a critical low level would transition to this state, in which case, a blind access utility would announce this information immediately to the user, and a screen magnification program would scroll the screen so that the battery indicator is in view. This state is also appropriate for any prompt or operation that must be completed before the user can continue.</summary>
		// Token: 0x040005B9 RID: 1465
		AlertHigh = 268435456,
		/// <summary>A password-protected edit control.</summary>
		// Token: 0x040005BA RID: 1466
		Protected = 536870912,
		/// <summary>The object displays a pop-up menu or window when invoked.</summary>
		// Token: 0x040005BB RID: 1467
		HasPopup = 1073741824,
		/// <summary>A valid object. This property is deprecated in .NET Framework 2.0.</summary>
		// Token: 0x040005BC RID: 1468
		[Obsolete("This enumeration value has been deprecated. There is no replacement. http://go.microsoft.com/fwlink/?linkid=14202")]
		Valid = 1073741823
	}
}
