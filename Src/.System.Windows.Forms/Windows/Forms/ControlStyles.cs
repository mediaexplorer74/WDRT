using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Specifies the style and behavior of a control.</summary>
	// Token: 0x0200016F RID: 367
	[Flags]
	public enum ControlStyles
	{
		/// <summary>If <see langword="true" />, the control is a container-like control.</summary>
		// Token: 0x04000917 RID: 2327
		ContainerControl = 1,
		/// <summary>If <see langword="true" />, the control paints itself rather than the operating system doing so. If <see langword="false" />, the <see cref="E:System.Windows.Forms.Control.Paint" /> event is not raised. This style only applies to classes derived from <see cref="T:System.Windows.Forms.Control" />.</summary>
		// Token: 0x04000918 RID: 2328
		UserPaint = 2,
		/// <summary>If <see langword="true" />, the control is drawn opaque and the background is not painted.</summary>
		// Token: 0x04000919 RID: 2329
		Opaque = 4,
		/// <summary>If <see langword="true" />, the control is redrawn when it is resized.</summary>
		// Token: 0x0400091A RID: 2330
		ResizeRedraw = 16,
		/// <summary>If <see langword="true" />, the control has a fixed width when auto-scaled. For example, if a layout operation attempts to rescale the control to accommodate a new <see cref="T:System.Drawing.Font" />, the control's <see cref="P:System.Windows.Forms.Control.Width" /> remains unchanged.</summary>
		// Token: 0x0400091B RID: 2331
		FixedWidth = 32,
		/// <summary>If <see langword="true" />, the control has a fixed height when auto-scaled. For example, if a layout operation attempts to rescale the control to accommodate a new <see cref="T:System.Drawing.Font" />, the control's <see cref="P:System.Windows.Forms.Control.Height" /> remains unchanged.</summary>
		// Token: 0x0400091C RID: 2332
		FixedHeight = 64,
		/// <summary>If <see langword="true" />, the control implements the standard <see cref="E:System.Windows.Forms.Control.Click" /> behavior.</summary>
		// Token: 0x0400091D RID: 2333
		StandardClick = 256,
		/// <summary>If <see langword="true" />, the control can receive focus.</summary>
		// Token: 0x0400091E RID: 2334
		Selectable = 512,
		/// <summary>If <see langword="true" />, the control does its own mouse processing, and mouse events are not handled by the operating system.</summary>
		// Token: 0x0400091F RID: 2335
		UserMouse = 1024,
		/// <summary>If <see langword="true" />, the control accepts a <see cref="P:System.Windows.Forms.Control.BackColor" /> with an alpha component of less than 255 to simulate transparency. Transparency will be simulated only if the <see cref="F:System.Windows.Forms.ControlStyles.UserPaint" /> bit is set to <see langword="true" /> and the parent control is derived from <see cref="T:System.Windows.Forms.Control" />.</summary>
		// Token: 0x04000920 RID: 2336
		SupportsTransparentBackColor = 2048,
		/// <summary>If <see langword="true" />, the control implements the standard <see cref="E:System.Windows.Forms.Control.DoubleClick" /> behavior. This style is ignored if the <see cref="F:System.Windows.Forms.ControlStyles.StandardClick" /> bit is not set to <see langword="true" />.</summary>
		// Token: 0x04000921 RID: 2337
		StandardDoubleClick = 4096,
		/// <summary>If <see langword="true" />, the control ignores the window message WM_ERASEBKGND to reduce flicker. This style should only be applied if the <see cref="F:System.Windows.Forms.ControlStyles.UserPaint" /> bit is set to <see langword="true" />.</summary>
		// Token: 0x04000922 RID: 2338
		AllPaintingInWmPaint = 8192,
		/// <summary>If <see langword="true" />, the control keeps a copy of the text rather than getting it from the <see cref="P:System.Windows.Forms.Control.Handle" /> each time it is needed. This style defaults to <see langword="false" />. This behavior improves performance, but makes it difficult to keep the text synchronized.</summary>
		// Token: 0x04000923 RID: 2339
		CacheText = 16384,
		/// <summary>If <see langword="true" />, the <see cref="M:System.Windows.Forms.Control.OnNotifyMessage(System.Windows.Forms.Message)" /> method is called for every message sent to the control's <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" />. This style defaults to <see langword="false" />. <see cref="F:System.Windows.Forms.ControlStyles.EnableNotifyMessage" /> does not work in partial trust.</summary>
		// Token: 0x04000924 RID: 2340
		EnableNotifyMessage = 32768,
		/// <summary>If <see langword="true" />, drawing is performed in a buffer, and after it completes, the result is output to the screen. Double-buffering prevents flicker caused by the redrawing of the control. If you set <see cref="F:System.Windows.Forms.ControlStyles.DoubleBuffer" /> to <see langword="true" />, you should also set <see cref="F:System.Windows.Forms.ControlStyles.UserPaint" /> and <see cref="F:System.Windows.Forms.ControlStyles.AllPaintingInWmPaint" /> to <see langword="true" />.</summary>
		// Token: 0x04000925 RID: 2341
		[EditorBrowsable(EditorBrowsableState.Never)]
		DoubleBuffer = 65536,
		/// <summary>If <see langword="true" />, the control is first drawn to a buffer rather than directly to the screen, which can reduce flicker. If you set this property to <see langword="true" />, you should also set the <see cref="F:System.Windows.Forms.ControlStyles.AllPaintingInWmPaint" /> to <see langword="true" />.</summary>
		// Token: 0x04000926 RID: 2342
		OptimizedDoubleBuffer = 131072,
		/// <summary>Specifies that the value of the control's <c>Text</c> property, if set, determines the control's default Active Accessibility name and shortcut key.</summary>
		// Token: 0x04000927 RID: 2343
		UseTextForAccessibility = 262144
	}
}
