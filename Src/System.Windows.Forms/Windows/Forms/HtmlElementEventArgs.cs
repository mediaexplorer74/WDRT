using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the events defined on <see cref="T:System.Windows.Forms.HtmlDocument" /> and <see cref="T:System.Windows.Forms.HtmlElement" />.</summary>
	// Token: 0x0200027E RID: 638
	public sealed class HtmlElementEventArgs : EventArgs
	{
		// Token: 0x060028F6 RID: 10486 RVA: 0x000BC8BC File Offset: 0x000BAABC
		internal HtmlElementEventArgs(HtmlShimManager shimManager, UnsafeNativeMethods.IHTMLEventObj eventObj)
		{
			this.htmlEventObj = eventObj;
			this.shimManager = shimManager;
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x060028F7 RID: 10487 RVA: 0x000BC8D2 File Offset: 0x000BAAD2
		private UnsafeNativeMethods.IHTMLEventObj NativeHTMLEventObj
		{
			get
			{
				return this.htmlEventObj;
			}
		}

		/// <summary>Gets the mouse button that was clicked during a <see cref="E:System.Windows.Forms.HtmlElement.MouseDown" /> or <see cref="E:System.Windows.Forms.HtmlElement.MouseUp" /> event.</summary>
		/// <returns>The mouse button that was clicked.</returns>
		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x060028F8 RID: 10488 RVA: 0x000BC8DC File Offset: 0x000BAADC
		public MouseButtons MouseButtonsPressed
		{
			get
			{
				MouseButtons mouseButtons = MouseButtons.None;
				int button = this.NativeHTMLEventObj.GetButton();
				if ((button & 1) != 0)
				{
					mouseButtons |= MouseButtons.Left;
				}
				if ((button & 2) != 0)
				{
					mouseButtons |= MouseButtons.Right;
				}
				if ((button & 4) != 0)
				{
					mouseButtons |= MouseButtons.Middle;
				}
				return mouseButtons;
			}
		}

		/// <summary>Gets or sets the position of the mouse cursor in the document's client area.</summary>
		/// <returns>The current position of the mouse cursor.</returns>
		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x060028F9 RID: 10489 RVA: 0x000BC91F File Offset: 0x000BAB1F
		public Point ClientMousePosition
		{
			get
			{
				return new Point(this.NativeHTMLEventObj.GetClientX(), this.NativeHTMLEventObj.GetClientY());
			}
		}

		/// <summary>Gets or sets the position of the mouse cursor relative to the element that raises the event.</summary>
		/// <returns>The mouse position relative to the element that raises the event.</returns>
		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x060028FA RID: 10490 RVA: 0x000BC93C File Offset: 0x000BAB3C
		public Point OffsetMousePosition
		{
			get
			{
				return new Point(this.NativeHTMLEventObj.GetOffsetX(), this.NativeHTMLEventObj.GetOffsetY());
			}
		}

		/// <summary>Gets or sets the position of the mouse cursor relative to a relatively positioned parent element.</summary>
		/// <returns>The position of the mouse cursor relative to the upper-left corner of the parent of the element that raised the event, if the parent element is relatively positioned.</returns>
		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x060028FB RID: 10491 RVA: 0x000BC959 File Offset: 0x000BAB59
		public Point MousePosition
		{
			get
			{
				return new Point(this.NativeHTMLEventObj.GetX(), this.NativeHTMLEventObj.GetY());
			}
		}

		/// <summary>Gets or sets a value indicating whether the current event bubbles up through the element hierarchy of the HTML Document Object Model.</summary>
		/// <returns>
		///   <see langword="true" /> if the event bubbles; <see langword="false" /> if it does not.</returns>
		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x060028FC RID: 10492 RVA: 0x000BC976 File Offset: 0x000BAB76
		// (set) Token: 0x060028FD RID: 10493 RVA: 0x000BC986 File Offset: 0x000BAB86
		public bool BubbleEvent
		{
			get
			{
				return !this.NativeHTMLEventObj.GetCancelBubble();
			}
			set
			{
				this.NativeHTMLEventObj.SetCancelBubble(!value);
			}
		}

		/// <summary>Gets the ASCII value of the keyboard character typed in a <see cref="E:System.Windows.Forms.HtmlElement.KeyPress" />, <see cref="E:System.Windows.Forms.HtmlElement.KeyDown" />, or <see cref="E:System.Windows.Forms.HtmlElement.KeyUp" /> event.</summary>
		/// <returns>The ASCII value of the composed keyboard entry.</returns>
		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x060028FE RID: 10494 RVA: 0x000BC997 File Offset: 0x000BAB97
		public int KeyPressedCode
		{
			get
			{
				return this.NativeHTMLEventObj.GetKeyCode();
			}
		}

		/// <summary>Indicates whether the ALT key was pressed when this event occurred.</summary>
		/// <returns>
		///   <see langword="true" /> is the ALT key was pressed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x060028FF RID: 10495 RVA: 0x000BC9A4 File Offset: 0x000BABA4
		public bool AltKeyPressed
		{
			get
			{
				return this.NativeHTMLEventObj.GetAltKey();
			}
		}

		/// <summary>Indicates whether the CTRL key was pressed when this event occurred.</summary>
		/// <returns>
		///   <see langword="true" /> if the CTRL key was pressed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06002900 RID: 10496 RVA: 0x000BC9B1 File Offset: 0x000BABB1
		public bool CtrlKeyPressed
		{
			get
			{
				return this.NativeHTMLEventObj.GetCtrlKey();
			}
		}

		/// <summary>Indicates whether the SHIFT key was pressed when this event occurred.</summary>
		/// <returns>
		///   <see langword="true" /> if the SHIFT key was pressed; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06002901 RID: 10497 RVA: 0x000BC9BE File Offset: 0x000BABBE
		public bool ShiftKeyPressed
		{
			get
			{
				return this.NativeHTMLEventObj.GetShiftKey();
			}
		}

		/// <summary>Gets the name of the event that was raised.</summary>
		/// <returns>The name of the event.</returns>
		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06002902 RID: 10498 RVA: 0x000BC9CB File Offset: 0x000BABCB
		public string EventType
		{
			get
			{
				return this.NativeHTMLEventObj.GetEventType();
			}
		}

		/// <summary>Gets or sets the return value of the handled event.</summary>
		/// <returns>
		///   <see langword="true" /> if the event has been handled; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06002903 RID: 10499 RVA: 0x000BC9D8 File Offset: 0x000BABD8
		// (set) Token: 0x06002904 RID: 10500 RVA: 0x000BC9FC File Offset: 0x000BABFC
		public bool ReturnValue
		{
			get
			{
				object returnValue = this.NativeHTMLEventObj.GetReturnValue();
				return returnValue == null || (bool)returnValue;
			}
			set
			{
				object obj = value;
				this.NativeHTMLEventObj.SetReturnValue(obj);
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.HtmlElement" /> the mouse pointer is moving away from.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.HtmlElement" /> the mouse pointer is moving away from.</returns>
		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06002905 RID: 10501 RVA: 0x000BCA1C File Offset: 0x000BAC1C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public HtmlElement FromElement
		{
			get
			{
				UnsafeNativeMethods.IHTMLElement fromElement = this.NativeHTMLEventObj.GetFromElement();
				if (fromElement != null)
				{
					return new HtmlElement(this.shimManager, fromElement);
				}
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.HtmlElement" /> toward which the user is moving the mouse pointer.</summary>
		/// <returns>The element toward which the mouse pointer is moving.</returns>
		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06002906 RID: 10502 RVA: 0x000BCA48 File Offset: 0x000BAC48
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public HtmlElement ToElement
		{
			get
			{
				UnsafeNativeMethods.IHTMLElement toElement = this.NativeHTMLEventObj.GetToElement();
				if (toElement != null)
				{
					return new HtmlElement(this.shimManager, toElement);
				}
				return null;
			}
		}

		// Token: 0x040010C8 RID: 4296
		private UnsafeNativeMethods.IHTMLEventObj htmlEventObj;

		// Token: 0x040010C9 RID: 4297
		private HtmlShimManager shimManager;
	}
}
