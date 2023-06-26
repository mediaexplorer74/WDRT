using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	/// <summary>Controls <see cref="T:System.Windows.Forms.ToolStrip" /> rendering and rafting, and the merging of <see cref="T:System.Windows.Forms.MenuStrip" />, <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />, and <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020003E0 RID: 992
	public sealed class ToolStripManager
	{
		// Token: 0x0600438A RID: 17290 RVA: 0x0011D55A File Offset: 0x0011B75A
		private static void InitalizeThread()
		{
			if (!ToolStripManager.initialized)
			{
				ToolStripManager.initialized = true;
				ToolStripManager.currentRendererType = ToolStripManager.ProfessionalRendererType;
			}
		}

		// Token: 0x0600438B RID: 17291 RVA: 0x00002843 File Offset: 0x00000A43
		private ToolStripManager()
		{
		}

		// Token: 0x0600438C RID: 17292 RVA: 0x0011D574 File Offset: 0x0011B774
		static ToolStripManager()
		{
			SystemEvents.UserPreferenceChanging += ToolStripManager.OnUserPreferenceChanging;
		}

		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x0600438D RID: 17293 RVA: 0x0011D5D4 File Offset: 0x0011B7D4
		internal static Font DefaultFont
		{
			get
			{
				if (DpiHelper.EnableToolStripPerMonitorV2HighDpiImprovements)
				{
					int num = ToolStripManager.CurrentDpi;
					Font font = null;
					if (!ToolStripManager.defaultFontCache.TryGetValue(num, out font) || font == null)
					{
						Font font2 = SystemInformation.GetMenuFontForDpi(num);
						if (font2 != null)
						{
							if (font2.Unit != GraphicsUnit.Point)
							{
								font = ControlPaint.FontInPoints(font2);
								font2.Dispose();
							}
							else
							{
								font = font2;
							}
							ToolStripManager.defaultFontCache[num] = font;
						}
					}
					return font;
				}
				Font font3 = ToolStripManager.defaultFont;
				if (font3 == null)
				{
					object obj = ToolStripManager.internalSyncObject;
					lock (obj)
					{
						font3 = ToolStripManager.defaultFont;
						if (font3 == null)
						{
							Font font2 = SystemFonts.MenuFont;
							if (font2 == null)
							{
								font2 = Control.DefaultFont;
							}
							if (font2 != null)
							{
								if (font2.Unit != GraphicsUnit.Point)
								{
									ToolStripManager.defaultFont = ControlPaint.FontInPoints(font2);
									font3 = ToolStripManager.defaultFont;
									font2.Dispose();
								}
								else
								{
									ToolStripManager.defaultFont = font2;
									font3 = ToolStripManager.defaultFont;
								}
							}
							return font3;
						}
					}
					return font3;
				}
				return font3;
			}
		}

		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x0600438E RID: 17294 RVA: 0x0011D6C4 File Offset: 0x0011B8C4
		// (set) Token: 0x0600438F RID: 17295 RVA: 0x0011D6CB File Offset: 0x0011B8CB
		internal static int CurrentDpi
		{
			get
			{
				return ToolStripManager.currentDpi;
			}
			set
			{
				ToolStripManager.currentDpi = value;
			}
		}

		// Token: 0x17001081 RID: 4225
		// (get) Token: 0x06004390 RID: 17296 RVA: 0x0011D6D3 File Offset: 0x0011B8D3
		internal static ClientUtils.WeakRefCollection ToolStrips
		{
			get
			{
				if (ToolStripManager.toolStripWeakArrayList == null)
				{
					ToolStripManager.toolStripWeakArrayList = new ClientUtils.WeakRefCollection();
				}
				return ToolStripManager.toolStripWeakArrayList;
			}
		}

		// Token: 0x06004391 RID: 17297 RVA: 0x0011D6EC File Offset: 0x0011B8EC
		private static void AddEventHandler(int key, Delegate value)
		{
			object obj = ToolStripManager.internalSyncObject;
			lock (obj)
			{
				if (ToolStripManager.staticEventHandlers == null)
				{
					ToolStripManager.staticEventHandlers = new Delegate[1];
				}
				ToolStripManager.staticEventHandlers[key] = Delegate.Combine(ToolStripManager.staticEventHandlers[key], value);
			}
		}

		/// <summary>Finds the specified <see cref="T:System.Windows.Forms.ToolStrip" /> or a type derived from <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <param name="toolStripName">A string specifying the name of the <see cref="T:System.Windows.Forms.ToolStrip" /> or derived <see cref="T:System.Windows.Forms.ToolStrip" /> type to find.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStrip" /> or one of its derived types as specified by the <paramref name="toolStripName" /> parameter, or <see langword="null" /> if the <see cref="T:System.Windows.Forms.ToolStrip" /> is not found.</returns>
		// Token: 0x06004392 RID: 17298 RVA: 0x0011D74C File Offset: 0x0011B94C
		[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
		public static ToolStrip FindToolStrip(string toolStripName)
		{
			ToolStrip toolStrip = null;
			for (int i = 0; i < ToolStripManager.ToolStrips.Count; i++)
			{
				if (ToolStripManager.ToolStrips[i] != null && string.Equals(((ToolStrip)ToolStripManager.ToolStrips[i]).Name, toolStripName, StringComparison.Ordinal))
				{
					toolStrip = (ToolStrip)ToolStripManager.ToolStrips[i];
					break;
				}
			}
			return toolStrip;
		}

		// Token: 0x06004393 RID: 17299 RVA: 0x0011D7B0 File Offset: 0x0011B9B0
		[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
		internal static ToolStrip FindToolStrip(Form owningForm, string toolStripName)
		{
			ToolStrip toolStrip = null;
			for (int i = 0; i < ToolStripManager.ToolStrips.Count; i++)
			{
				if (ToolStripManager.ToolStrips[i] != null && string.Equals(((ToolStrip)ToolStripManager.ToolStrips[i]).Name, toolStripName, StringComparison.Ordinal))
				{
					toolStrip = (ToolStrip)ToolStripManager.ToolStrips[i];
					if (toolStrip.FindForm() == owningForm)
					{
						break;
					}
				}
			}
			return toolStrip;
		}

		// Token: 0x06004394 RID: 17300 RVA: 0x0011D81C File Offset: 0x0011BA1C
		private static bool CanChangeSelection(ToolStrip start, ToolStrip toolStrip)
		{
			if (toolStrip == null)
			{
				return false;
			}
			bool flag = !toolStrip.TabStop && toolStrip.Enabled && toolStrip.Visible && !toolStrip.IsDisposed && !toolStrip.Disposing && !toolStrip.IsDropDown && ToolStripManager.IsOnSameWindow(start, toolStrip);
			if (flag)
			{
				foreach (object obj in toolStrip.Items)
				{
					ToolStripItem toolStripItem = (ToolStripItem)obj;
					if (toolStripItem.CanSelect)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06004395 RID: 17301 RVA: 0x0011D8C4 File Offset: 0x0011BAC4
		private static bool ChangeSelection(ToolStrip start, ToolStrip toolStrip)
		{
			if (toolStrip == null || start == null)
			{
				return false;
			}
			if (start == toolStrip)
			{
				return false;
			}
			if (ToolStripManager.ModalMenuFilter.InMenuMode)
			{
				if (ToolStripManager.ModalMenuFilter.GetActiveToolStrip() == start)
				{
					ToolStripManager.ModalMenuFilter.RemoveActiveToolStrip(start);
					start.NotifySelectionChange(null);
				}
				ToolStripManager.ModalMenuFilter.SetActiveToolStrip(toolStrip);
			}
			else
			{
				toolStrip.FocusInternal();
			}
			start.SnapFocusChange(toolStrip);
			toolStrip.SelectNextToolStripItem(null, toolStrip.RightToLeft != RightToLeft.Yes);
			return true;
		}

		// Token: 0x06004396 RID: 17302 RVA: 0x0011D928 File Offset: 0x0011BB28
		private static Delegate GetEventHandler(int key)
		{
			object obj = ToolStripManager.internalSyncObject;
			Delegate @delegate;
			lock (obj)
			{
				if (ToolStripManager.staticEventHandlers == null)
				{
					@delegate = null;
				}
				else
				{
					@delegate = ToolStripManager.staticEventHandlers[key];
				}
			}
			return @delegate;
		}

		// Token: 0x06004397 RID: 17303 RVA: 0x0011D978 File Offset: 0x0011BB78
		private static bool IsOnSameWindow(Control control1, Control control2)
		{
			return WindowsFormsUtils.GetRootHWnd(control1).Handle == WindowsFormsUtils.GetRootHWnd(control2).Handle;
		}

		// Token: 0x06004398 RID: 17304 RVA: 0x0011D9A6 File Offset: 0x0011BBA6
		internal static bool IsThreadUsingToolStrips()
		{
			return ToolStripManager.toolStripWeakArrayList != null && ToolStripManager.toolStripWeakArrayList.Count > 0;
		}

		// Token: 0x06004399 RID: 17305 RVA: 0x0011D9C0 File Offset: 0x0011BBC0
		private static void OnUserPreferenceChanging(object sender, UserPreferenceChangingEventArgs e)
		{
			if (e.Category == UserPreferenceCategory.Window)
			{
				if (DpiHelper.EnableToolStripPerMonitorV2HighDpiImprovements)
				{
					ToolStripManager.defaultFontCache.Clear();
					return;
				}
				object obj = ToolStripManager.internalSyncObject;
				lock (obj)
				{
					ToolStripManager.defaultFont = null;
				}
			}
		}

		// Token: 0x0600439A RID: 17306 RVA: 0x0011DA1C File Offset: 0x0011BC1C
		internal static void NotifyMenuModeChange(bool invalidateText, bool activationChange)
		{
			bool flag = false;
			for (int i = 0; i < ToolStripManager.ToolStrips.Count; i++)
			{
				ToolStrip toolStrip = ToolStripManager.ToolStrips[i] as ToolStrip;
				if (toolStrip == null)
				{
					flag = true;
				}
				else
				{
					if (invalidateText)
					{
						toolStrip.InvalidateTextItems();
					}
					if (activationChange)
					{
						toolStrip.KeyboardActive = false;
					}
				}
			}
			if (flag)
			{
				ToolStripManager.PruneToolStripList();
			}
		}

		// Token: 0x0600439B RID: 17307 RVA: 0x0011DA74 File Offset: 0x0011BC74
		internal static void PruneToolStripList()
		{
			if (ToolStripManager.toolStripWeakArrayList != null && ToolStripManager.toolStripWeakArrayList.Count > 0)
			{
				for (int i = ToolStripManager.toolStripWeakArrayList.Count - 1; i >= 0; i--)
				{
					if (ToolStripManager.toolStripWeakArrayList[i] == null)
					{
						ToolStripManager.toolStripWeakArrayList.RemoveAt(i);
					}
				}
			}
		}

		// Token: 0x0600439C RID: 17308 RVA: 0x0011DAC4 File Offset: 0x0011BCC4
		private static void RemoveEventHandler(int key, Delegate value)
		{
			object obj = ToolStripManager.internalSyncObject;
			lock (obj)
			{
				if (ToolStripManager.staticEventHandlers != null)
				{
					ToolStripManager.staticEventHandlers[key] = Delegate.Remove(ToolStripManager.staticEventHandlers[key], value);
				}
			}
		}

		// Token: 0x0600439D RID: 17309 RVA: 0x0011DB18 File Offset: 0x0011BD18
		internal static bool SelectNextToolStrip(ToolStrip start, bool forward)
		{
			if (start == null || start.ParentInternal == null)
			{
				return false;
			}
			ToolStrip toolStrip = null;
			ToolStrip toolStrip2 = null;
			int tabIndex = start.TabIndex;
			int num = ToolStripManager.ToolStrips.IndexOf(start);
			int count = ToolStripManager.ToolStrips.Count;
			for (int i = 0; i < count; i++)
			{
				num = (forward ? ((num + 1) % count) : ((num + count - 1) % count));
				ToolStrip toolStrip3 = ToolStripManager.ToolStrips[num] as ToolStrip;
				if (toolStrip3 != null && toolStrip3 != start)
				{
					int tabIndex2 = toolStrip3.TabIndex;
					if (forward)
					{
						if (tabIndex2 >= tabIndex && ToolStripManager.CanChangeSelection(start, toolStrip3))
						{
							if (toolStrip2 == null)
							{
								toolStrip2 = toolStrip3;
							}
							else if (toolStrip3.TabIndex < toolStrip2.TabIndex)
							{
								toolStrip2 = toolStrip3;
							}
						}
						else if ((toolStrip == null || toolStrip3.TabIndex < toolStrip.TabIndex) && ToolStripManager.CanChangeSelection(start, toolStrip3))
						{
							toolStrip = toolStrip3;
						}
					}
					else if (tabIndex2 <= tabIndex && ToolStripManager.CanChangeSelection(start, toolStrip3))
					{
						if (toolStrip2 == null)
						{
							toolStrip2 = toolStrip3;
						}
						else if (toolStrip3.TabIndex > toolStrip2.TabIndex)
						{
							toolStrip2 = toolStrip3;
						}
					}
					else if ((toolStrip == null || toolStrip3.TabIndex > toolStrip.TabIndex) && ToolStripManager.CanChangeSelection(start, toolStrip3))
					{
						toolStrip = toolStrip3;
					}
					if (toolStrip2 != null && Math.Abs(toolStrip2.TabIndex - tabIndex) <= 1)
					{
						break;
					}
				}
			}
			if (toolStrip2 != null)
			{
				return ToolStripManager.ChangeSelection(start, toolStrip2);
			}
			return toolStrip != null && ToolStripManager.ChangeSelection(start, toolStrip);
		}

		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x0600439E RID: 17310 RVA: 0x0011DC6E File Offset: 0x0011BE6E
		// (set) Token: 0x0600439F RID: 17311 RVA: 0x0011DC7A File Offset: 0x0011BE7A
		private static Type CurrentRendererType
		{
			get
			{
				ToolStripManager.InitalizeThread();
				return ToolStripManager.currentRendererType;
			}
			set
			{
				ToolStripManager.currentRendererType = value;
			}
		}

		// Token: 0x17001083 RID: 4227
		// (get) Token: 0x060043A0 RID: 17312 RVA: 0x0011DC82 File Offset: 0x0011BE82
		private static Type DefaultRendererType
		{
			get
			{
				return ToolStripManager.ProfessionalRendererType;
			}
		}

		/// <summary>Gets or sets the default painting styles for the form.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripRenderer" /> values.</returns>
		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x060043A1 RID: 17313 RVA: 0x0011DC89 File Offset: 0x0011BE89
		// (set) Token: 0x060043A2 RID: 17314 RVA: 0x0011DCA8 File Offset: 0x0011BEA8
		public static ToolStripRenderer Renderer
		{
			get
			{
				if (ToolStripManager.defaultRenderer == null)
				{
					ToolStripManager.defaultRenderer = ToolStripManager.CreateRenderer(ToolStripManager.RenderMode);
				}
				return ToolStripManager.defaultRenderer;
			}
			[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
			set
			{
				if (ToolStripManager.defaultRenderer != value)
				{
					ToolStripManager.CurrentRendererType = ((value == null) ? ToolStripManager.DefaultRendererType : value.GetType());
					ToolStripManager.defaultRenderer = value;
					EventHandler eventHandler = (EventHandler)ToolStripManager.GetEventHandler(0);
					if (eventHandler != null)
					{
						eventHandler(null, EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripManager.Renderer" /> property changes.</summary>
		// Token: 0x14000353 RID: 851
		// (add) Token: 0x060043A3 RID: 17315 RVA: 0x0011DCF3 File Offset: 0x0011BEF3
		// (remove) Token: 0x060043A4 RID: 17316 RVA: 0x0011DCFC File Offset: 0x0011BEFC
		public static event EventHandler RendererChanged
		{
			add
			{
				ToolStripManager.AddEventHandler(0, value);
			}
			remove
			{
				ToolStripManager.RemoveEventHandler(0, value);
			}
		}

		/// <summary>Gets or sets the default theme for the form.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripManagerRenderMode" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The set value was not one of the <see cref="T:System.Windows.Forms.ToolStripManagerRenderMode" /> values.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="T:System.Windows.Forms.ToolStripManagerRenderMode" /> is set to <see cref="F:System.Windows.Forms.ToolStripManagerRenderMode.Custom" />; use the <see cref="P:System.Windows.Forms.ToolStripManager.Renderer" /> property instead.</exception>
		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x060043A5 RID: 17317 RVA: 0x0011DD08 File Offset: 0x0011BF08
		// (set) Token: 0x060043A6 RID: 17318 RVA: 0x0011DD50 File Offset: 0x0011BF50
		public static ToolStripManagerRenderMode RenderMode
		{
			get
			{
				Type type = ToolStripManager.CurrentRendererType;
				if (ToolStripManager.defaultRenderer != null && !ToolStripManager.defaultRenderer.IsAutoGenerated)
				{
					return ToolStripManagerRenderMode.Custom;
				}
				if (type == ToolStripManager.ProfessionalRendererType)
				{
					return ToolStripManagerRenderMode.Professional;
				}
				if (type == ToolStripManager.SystemRendererType)
				{
					return ToolStripManagerRenderMode.System;
				}
				return ToolStripManagerRenderMode.Custom;
			}
			[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ToolStripManagerRenderMode));
				}
				if (value == ToolStripManagerRenderMode.Custom)
				{
					throw new NotSupportedException(SR.GetString("ToolStripRenderModeUseRendererPropertyInstead"));
				}
				if (value - ToolStripManagerRenderMode.System <= 1)
				{
					ToolStripManager.Renderer = ToolStripManager.CreateRenderer(value);
					return;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether a <see cref="T:System.Windows.Forms.ToolStrip" /> is rendered using visual style information called themes.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is rendered using themes; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001086 RID: 4230
		// (get) Token: 0x060043A7 RID: 17319 RVA: 0x0011DDA8 File Offset: 0x0011BFA8
		// (set) Token: 0x060043A8 RID: 17320 RVA: 0x0011DDB8 File Offset: 0x0011BFB8
		public static bool VisualStylesEnabled
		{
			get
			{
				return ToolStripManager.visualStylesEnabledIfPossible && Application.RenderWithVisualStyles;
			}
			[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
			set
			{
				bool visualStylesEnabled = ToolStripManager.VisualStylesEnabled;
				ToolStripManager.visualStylesEnabledIfPossible = value;
				if (visualStylesEnabled != ToolStripManager.VisualStylesEnabled)
				{
					EventHandler eventHandler = (EventHandler)ToolStripManager.GetEventHandler(0);
					if (eventHandler != null)
					{
						eventHandler(null, EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x060043A9 RID: 17321 RVA: 0x0011DDF4 File Offset: 0x0011BFF4
		internal static ToolStripRenderer CreateRenderer(ToolStripManagerRenderMode renderMode)
		{
			switch (renderMode)
			{
			case ToolStripManagerRenderMode.System:
				return new ToolStripSystemRenderer(true);
			case ToolStripManagerRenderMode.Professional:
				return new ToolStripProfessionalRenderer(true);
			}
			return new ToolStripSystemRenderer(true);
		}

		// Token: 0x060043AA RID: 17322 RVA: 0x0011DDF4 File Offset: 0x0011BFF4
		internal static ToolStripRenderer CreateRenderer(ToolStripRenderMode renderMode)
		{
			switch (renderMode)
			{
			case ToolStripRenderMode.System:
				return new ToolStripSystemRenderer(true);
			case ToolStripRenderMode.Professional:
				return new ToolStripProfessionalRenderer(true);
			}
			return new ToolStripSystemRenderer(true);
		}

		// Token: 0x17001087 RID: 4231
		// (get) Token: 0x060043AB RID: 17323 RVA: 0x0011DE1E File Offset: 0x0011C01E
		internal static ClientUtils.WeakRefCollection ToolStripPanels
		{
			get
			{
				if (ToolStripManager.toolStripPanelWeakArrayList == null)
				{
					ToolStripManager.toolStripPanelWeakArrayList = new ClientUtils.WeakRefCollection();
				}
				return ToolStripManager.toolStripPanelWeakArrayList;
			}
		}

		// Token: 0x060043AC RID: 17324 RVA: 0x0011DE38 File Offset: 0x0011C038
		internal static ToolStripPanel ToolStripPanelFromPoint(Control draggedControl, Point screenLocation)
		{
			if (ToolStripManager.toolStripPanelWeakArrayList != null)
			{
				ISupportToolStripPanel supportToolStripPanel = draggedControl as ISupportToolStripPanel;
				bool isCurrentlyDragging = supportToolStripPanel.IsCurrentlyDragging;
				for (int i = 0; i < ToolStripManager.toolStripPanelWeakArrayList.Count; i++)
				{
					ToolStripPanel toolStripPanel = ToolStripManager.toolStripPanelWeakArrayList[i] as ToolStripPanel;
					if (toolStripPanel != null && toolStripPanel.IsHandleCreated && toolStripPanel.Visible && toolStripPanel.DragBounds.Contains(toolStripPanel.PointToClient(screenLocation)))
					{
						if (!isCurrentlyDragging)
						{
							return toolStripPanel;
						}
						if (ToolStripManager.IsOnSameWindow(draggedControl, toolStripPanel))
						{
							return toolStripPanel;
						}
					}
				}
			}
			return null;
		}

		/// <summary>Loads settings for the given <see cref="T:System.Windows.Forms.Form" /> using the full name of the <see cref="T:System.Windows.Forms.Form" /> as the settings key.</summary>
		/// <param name="targetForm">The <see cref="T:System.Windows.Forms.Form" /> whose name is also the settings key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetForm" /> is <see langword="null" />.</exception>
		// Token: 0x060043AD RID: 17325 RVA: 0x0011DEBC File Offset: 0x0011C0BC
		public static void LoadSettings(Form targetForm)
		{
			if (targetForm == null)
			{
				throw new ArgumentNullException("targetForm");
			}
			ToolStripManager.LoadSettings(targetForm, targetForm.GetType().FullName);
		}

		/// <summary>Loads settings for the specified <see cref="T:System.Windows.Forms.Form" /> using the specified settings key.</summary>
		/// <param name="targetForm">The <see cref="T:System.Windows.Forms.Form" /> for which to load settings.</param>
		/// <param name="key">A <see cref="T:System.String" /> representing the settings key for this <see cref="T:System.Windows.Forms.Form" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetForm" /> is <see langword="null" />.
		/// -or-
		/// <paramref name="key" /> is <see langword="null" /> or empty.</exception>
		// Token: 0x060043AE RID: 17326 RVA: 0x0011DEE0 File Offset: 0x0011C0E0
		public static void LoadSettings(Form targetForm, string key)
		{
			if (targetForm == null)
			{
				throw new ArgumentNullException("targetForm");
			}
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentNullException("key");
			}
			ToolStripSettingsManager toolStripSettingsManager = new ToolStripSettingsManager(targetForm, key);
			toolStripSettingsManager.Load();
		}

		/// <summary>Saves settings for the given <see cref="T:System.Windows.Forms.Form" /> using the full name of the <see cref="T:System.Windows.Forms.Form" /> as the settings key.</summary>
		/// <param name="sourceForm">The <see cref="T:System.Windows.Forms.Form" /> whose name is also the settings key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceForm" /> is <see langword="null" />.</exception>
		// Token: 0x060043AF RID: 17327 RVA: 0x0011DF1C File Offset: 0x0011C11C
		public static void SaveSettings(Form sourceForm)
		{
			if (sourceForm == null)
			{
				throw new ArgumentNullException("sourceForm");
			}
			ToolStripManager.SaveSettings(sourceForm, sourceForm.GetType().FullName);
		}

		/// <summary>Saves settings for the specified <see cref="T:System.Windows.Forms.Form" /> using the specified settings key.</summary>
		/// <param name="sourceForm">The <see cref="T:System.Windows.Forms.Form" /> for which to save settings.</param>
		/// <param name="key">A <see cref="T:System.String" /> representing the settings key for this <see cref="T:System.Windows.Forms.Form" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceForm" /> is <see langword="null" />.
		/// -or-
		/// <paramref name="key" /> is <see langword="null" /> or empty.</exception>
		// Token: 0x060043B0 RID: 17328 RVA: 0x0011DF40 File Offset: 0x0011C140
		public static void SaveSettings(Form sourceForm, string key)
		{
			if (sourceForm == null)
			{
				throw new ArgumentNullException("sourceForm");
			}
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentNullException("key");
			}
			ToolStripSettingsManager toolStripSettingsManager = new ToolStripSettingsManager(sourceForm, key);
			toolStripSettingsManager.Save();
		}

		// Token: 0x17001088 RID: 4232
		// (get) Token: 0x060043B1 RID: 17329 RVA: 0x0011DF7C File Offset: 0x0011C17C
		internal static bool ShowMenuFocusCues
		{
			get
			{
				return DisplayInformation.MenuAccessKeysUnderlined || ToolStripManager.ModalMenuFilter.Instance.ShowUnderlines;
			}
		}

		/// <summary>Retrieves a value indicating whether a defined shortcut key is valid.</summary>
		/// <param name="shortcut">The shortcut key to test for validity.</param>
		/// <returns>
		///   <see langword="true" /> if the shortcut key is valid; otherwise, <see langword="false" />.</returns>
		// Token: 0x060043B2 RID: 17330 RVA: 0x0011DF94 File Offset: 0x0011C194
		public static bool IsValidShortcut(Keys shortcut)
		{
			Keys keys = shortcut & Keys.KeyCode;
			Keys keys2 = shortcut & Keys.Modifiers;
			return shortcut != Keys.None && (keys == Keys.Delete || keys == Keys.Insert || (keys >= Keys.F1 && keys <= Keys.F24) || (keys != Keys.None && keys2 != Keys.None && keys - Keys.ShiftKey > 2 && keys2 != Keys.Shift));
		}

		// Token: 0x060043B3 RID: 17331 RVA: 0x0011DFF0 File Offset: 0x0011C1F0
		internal static bool IsMenuKey(Keys keyData)
		{
			Keys keys = keyData & Keys.KeyCode;
			return Keys.Menu == keys || Keys.F10 == keys;
		}

		/// <summary>Retrieves a value indicating whether the specified shortcut key is used by any of the <see cref="T:System.Windows.Forms.ToolStrip" /> controls of a form.</summary>
		/// <param name="shortcut">The shortcut key for which to search.</param>
		/// <returns>
		///   <see langword="true" /> if the shortcut key is used by any <see cref="T:System.Windows.Forms.ToolStrip" /> on the form; otherwise, <see langword="false" />.</returns>
		// Token: 0x060043B4 RID: 17332 RVA: 0x0011E014 File Offset: 0x0011C214
		public static bool IsShortcutDefined(Keys shortcut)
		{
			for (int i = 0; i < ToolStripManager.ToolStrips.Count; i++)
			{
				ToolStrip toolStrip = ToolStripManager.ToolStrips[i] as ToolStrip;
				if (toolStrip != null && toolStrip.Shortcuts.Contains(shortcut))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060043B5 RID: 17333 RVA: 0x0011E060 File Offset: 0x0011C260
		internal static bool ProcessCmdKey(ref Message m, Keys keyData)
		{
			if (ToolStripManager.IsValidShortcut(keyData))
			{
				return ToolStripManager.ProcessShortcut(ref m, keyData);
			}
			if (m.Msg == 260)
			{
				ToolStripManager.ModalMenuFilter.ProcessMenuKeyDown(ref m);
			}
			return false;
		}

		// Token: 0x060043B6 RID: 17334 RVA: 0x0011E088 File Offset: 0x0011C288
		internal static bool ProcessShortcut(ref Message m, Keys shortcut)
		{
			if (!ToolStripManager.IsThreadUsingToolStrips())
			{
				return false;
			}
			Control control = Control.FromChildHandleInternal(m.HWnd);
			Control control2 = control;
			if (control2 != null && ToolStripManager.IsValidShortcut(shortcut))
			{
				for (;;)
				{
					if (control2.ContextMenuStrip != null && control2.ContextMenuStrip.Shortcuts.ContainsKey(shortcut))
					{
						ToolStripMenuItem toolStripMenuItem = control2.ContextMenuStrip.Shortcuts[shortcut] as ToolStripMenuItem;
						if (toolStripMenuItem.ProcessCmdKey(ref m, shortcut))
						{
							break;
						}
					}
					control2 = control2.ParentInternal;
					if (control2 == null)
					{
						goto Block_6;
					}
				}
				return true;
				Block_6:
				if (control2 != null)
				{
					control = control2;
				}
				bool flag = false;
				bool flag2 = false;
				for (int i = 0; i < ToolStripManager.ToolStrips.Count; i++)
				{
					ToolStrip toolStrip = ToolStripManager.ToolStrips[i] as ToolStrip;
					bool flag3 = false;
					bool flag4 = false;
					if (toolStrip == null)
					{
						flag2 = true;
					}
					else if ((control == null || toolStrip != control.ContextMenuStrip) && toolStrip.Shortcuts.ContainsKey(shortcut))
					{
						if (toolStrip.IsDropDown)
						{
							ToolStripDropDown toolStripDropDown = toolStrip as ToolStripDropDown;
							ContextMenuStrip contextMenuStrip = toolStripDropDown.GetFirstDropDown() as ContextMenuStrip;
							if (contextMenuStrip != null)
							{
								flag4 = contextMenuStrip.IsAssignedToDropDownItem;
								if (!flag4)
								{
									if (contextMenuStrip != control.ContextMenuStrip)
									{
										goto IL_1D0;
									}
									flag3 = true;
								}
							}
						}
						bool flag5 = false;
						if (!flag3)
						{
							ToolStrip toplevelOwnerToolStrip = toolStrip.GetToplevelOwnerToolStrip();
							if (toplevelOwnerToolStrip != null && control != null)
							{
								HandleRef rootHWnd = WindowsFormsUtils.GetRootHWnd(toplevelOwnerToolStrip);
								HandleRef rootHWnd2 = WindowsFormsUtils.GetRootHWnd(control);
								flag5 = rootHWnd.Handle == rootHWnd2.Handle;
								if (flag5)
								{
									Form form = Control.FromHandleInternal(rootHWnd2.Handle) as Form;
									if (form != null && form.IsMdiContainer)
									{
										Form form2 = toplevelOwnerToolStrip.FindFormInternal();
										if (form2 != form && form2 != null)
										{
											flag5 = form2 == form.ActiveMdiChildInternal;
										}
									}
								}
							}
						}
						if (flag3 || flag5 || flag4)
						{
							ToolStripMenuItem toolStripMenuItem2 = toolStrip.Shortcuts[shortcut] as ToolStripMenuItem;
							if (toolStripMenuItem2 != null && toolStripMenuItem2.ProcessCmdKey(ref m, shortcut))
							{
								flag = true;
								break;
							}
						}
					}
					IL_1D0:;
				}
				if (flag2)
				{
					ToolStripManager.PruneToolStripList();
				}
				return flag;
			}
			return false;
		}

		// Token: 0x060043B7 RID: 17335 RVA: 0x0011E288 File Offset: 0x0011C488
		internal static bool ProcessMenuKey(ref Message m)
		{
			if (!ToolStripManager.IsThreadUsingToolStrips())
			{
				return false;
			}
			Keys keys = (Keys)(int)m.LParam;
			Control control = Control.FromHandleInternal(m.HWnd);
			Control control2 = null;
			MenuStrip menuStrip = null;
			if (control != null)
			{
				control2 = control.TopLevelControlInternal;
				if (control2 != null)
				{
					IntPtr menu = UnsafeNativeMethods.GetMenu(new HandleRef(control2, control2.Handle));
					if (menu == IntPtr.Zero)
					{
						menuStrip = ToolStripManager.GetMainMenuStrip(control2);
					}
				}
			}
			if ((ushort)keys == 32)
			{
				ToolStripManager.ModalMenuFilter.MenuKeyToggle = false;
			}
			else if ((ushort)keys == 45)
			{
				Form form = control2 as Form;
				if (form != null && form.IsMdiChild && form.WindowState == FormWindowState.Maximized)
				{
					ToolStripManager.ModalMenuFilter.MenuKeyToggle = false;
				}
			}
			else
			{
				if (UnsafeNativeMethods.GetKeyState(16) < 0 && keys == Keys.None)
				{
					return ToolStripManager.ModalMenuFilter.InMenuMode;
				}
				if (menuStrip != null && !ToolStripManager.ModalMenuFilter.MenuKeyToggle)
				{
					HandleRef rootHWnd = WindowsFormsUtils.GetRootHWnd(menuStrip);
					IntPtr foregroundWindow = UnsafeNativeMethods.GetForegroundWindow();
					if (rootHWnd.Handle == foregroundWindow)
					{
						return menuStrip.OnMenuKey();
					}
				}
				else if (menuStrip != null)
				{
					ToolStripManager.ModalMenuFilter.MenuKeyToggle = false;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060043B8 RID: 17336 RVA: 0x0011E37C File Offset: 0x0011C57C
		internal static MenuStrip GetMainMenuStrip(Control control)
		{
			if (control == null)
			{
				return null;
			}
			Form form = control.FindFormInternal();
			if (form != null && form.MainMenuStrip != null)
			{
				return form.MainMenuStrip;
			}
			return ToolStripManager.GetFirstMenuStripRecursive(control.Controls);
		}

		// Token: 0x060043B9 RID: 17337 RVA: 0x0011E3B4 File Offset: 0x0011C5B4
		private static MenuStrip GetFirstMenuStripRecursive(Control.ControlCollection controlsToLookIn)
		{
			try
			{
				for (int i = 0; i < controlsToLookIn.Count; i++)
				{
					if (controlsToLookIn[i] != null && controlsToLookIn[i] is MenuStrip)
					{
						return controlsToLookIn[i] as MenuStrip;
					}
				}
				for (int j = 0; j < controlsToLookIn.Count; j++)
				{
					if (controlsToLookIn[j] != null && controlsToLookIn[j].Controls != null && controlsToLookIn[j].Controls.Count > 0)
					{
						MenuStrip firstMenuStripRecursive = ToolStripManager.GetFirstMenuStripRecursive(controlsToLookIn[j].Controls);
						if (firstMenuStripRecursive != null)
						{
							return firstMenuStripRecursive;
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsCriticalException(ex))
				{
					throw;
				}
			}
			return null;
		}

		// Token: 0x060043BA RID: 17338 RVA: 0x0011E474 File Offset: 0x0011C674
		private static ToolStripItem FindMatch(ToolStripItem source, ToolStripItemCollection destinationItems)
		{
			ToolStripItem toolStripItem = null;
			if (source != null)
			{
				for (int i = 0; i < destinationItems.Count; i++)
				{
					ToolStripItem toolStripItem2 = destinationItems[i];
					if (WindowsFormsUtils.SafeCompareStrings(source.Text, toolStripItem2.Text, true))
					{
						toolStripItem = toolStripItem2;
						break;
					}
				}
				if (toolStripItem == null && source.MergeIndex > -1 && source.MergeIndex < destinationItems.Count)
				{
					toolStripItem = destinationItems[source.MergeIndex];
				}
			}
			return toolStripItem;
		}

		// Token: 0x060043BB RID: 17339 RVA: 0x0011E4E0 File Offset: 0x0011C6E0
		internal static ArrayList FindMergeableToolStrips(ContainerControl container)
		{
			ArrayList arrayList = new ArrayList();
			if (container != null)
			{
				for (int i = 0; i < ToolStripManager.ToolStrips.Count; i++)
				{
					ToolStrip toolStrip = (ToolStrip)ToolStripManager.ToolStrips[i];
					if (toolStrip != null && toolStrip.AllowMerge && container == toolStrip.FindFormInternal())
					{
						arrayList.Add(toolStrip);
					}
				}
			}
			arrayList.Sort(new ToolStripCustomIComparer());
			return arrayList;
		}

		// Token: 0x060043BC RID: 17340 RVA: 0x0011E544 File Offset: 0x0011C744
		private static bool IsSpecialMDIStrip(ToolStrip toolStrip)
		{
			return toolStrip is MdiControlStrip || toolStrip is MdiWindowListStrip;
		}

		/// <summary>Combines two <see cref="T:System.Windows.Forms.ToolStrip" /> objects of different types.</summary>
		/// <param name="sourceToolStrip">The <see cref="T:System.Windows.Forms.ToolStrip" /> to be combined with the <see cref="T:System.Windows.Forms.ToolStrip" /> referred to by the <paramref name="targetToolStrip" /> parameter.</param>
		/// <param name="targetToolStrip">The <see cref="T:System.Windows.Forms.ToolStrip" /> that receives the <see cref="T:System.Windows.Forms.ToolStrip" /> referred to by the <paramref name="sourceToolStrip" /> parameter.</param>
		/// <returns>
		///   <see langword="true" /> if the merge is successful; otherwise, <see langword="false" />.</returns>
		// Token: 0x060043BD RID: 17341 RVA: 0x0011E55C File Offset: 0x0011C75C
		public static bool Merge(ToolStrip sourceToolStrip, ToolStrip targetToolStrip)
		{
			if (sourceToolStrip == null)
			{
				throw new ArgumentNullException("sourceToolStrip");
			}
			if (targetToolStrip == null)
			{
				throw new ArgumentNullException("targetToolStrip");
			}
			if (targetToolStrip == sourceToolStrip)
			{
				throw new ArgumentException(SR.GetString("ToolStripMergeImpossibleIdentical"));
			}
			bool flag = ToolStripManager.IsSpecialMDIStrip(sourceToolStrip) || (sourceToolStrip.AllowMerge && targetToolStrip.AllowMerge && (sourceToolStrip.GetType().IsAssignableFrom(targetToolStrip.GetType()) || targetToolStrip.GetType().IsAssignableFrom(sourceToolStrip.GetType())));
			MergeHistory mergeHistory = null;
			if (flag)
			{
				mergeHistory = new MergeHistory(sourceToolStrip);
				int count = sourceToolStrip.Items.Count;
				if (count > 0)
				{
					sourceToolStrip.SuspendLayout();
					targetToolStrip.SuspendLayout();
					try
					{
						int num = count;
						int i = 0;
						int num2 = 0;
						while (i < count)
						{
							ToolStripItem toolStripItem = sourceToolStrip.Items[num2];
							ToolStripManager.MergeRecursive(toolStripItem, targetToolStrip.Items, mergeHistory.MergeHistoryItemsStack);
							int num3 = num - sourceToolStrip.Items.Count;
							num2 = ((num3 > 0) ? num2 : (num2 + 1));
							num = sourceToolStrip.Items.Count;
							i++;
						}
					}
					finally
					{
						sourceToolStrip.ResumeLayout();
						targetToolStrip.ResumeLayout();
					}
					if (mergeHistory.MergeHistoryItemsStack.Count > 0)
					{
						targetToolStrip.MergeHistoryStack.Push(mergeHistory);
					}
				}
			}
			bool flag2 = false;
			if (mergeHistory != null && mergeHistory.MergeHistoryItemsStack.Count > 0)
			{
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x060043BE RID: 17342 RVA: 0x0011E6C4 File Offset: 0x0011C8C4
		private static void MergeRecursive(ToolStripItem source, ToolStripItemCollection destinationItems, Stack<MergeHistoryItem> history)
		{
			switch (source.MergeAction)
			{
			case MergeAction.Append:
			{
				MergeHistoryItem mergeHistoryItem = new MergeHistoryItem(MergeAction.Remove);
				mergeHistoryItem.PreviousIndexCollection = source.Owner.Items;
				mergeHistoryItem.PreviousIndex = mergeHistoryItem.PreviousIndexCollection.IndexOf(source);
				mergeHistoryItem.TargetItem = source;
				int num = destinationItems.Add(source);
				mergeHistoryItem.Index = num;
				mergeHistoryItem.IndexCollection = destinationItems;
				history.Push(mergeHistoryItem);
				break;
			}
			case MergeAction.Insert:
				if (source.MergeIndex > -1)
				{
					MergeHistoryItem mergeHistoryItem = new MergeHistoryItem(MergeAction.Remove);
					mergeHistoryItem.PreviousIndexCollection = source.Owner.Items;
					mergeHistoryItem.PreviousIndex = mergeHistoryItem.PreviousIndexCollection.IndexOf(source);
					mergeHistoryItem.TargetItem = source;
					int num2 = Math.Min(destinationItems.Count, source.MergeIndex);
					destinationItems.Insert(num2, source);
					mergeHistoryItem.IndexCollection = destinationItems;
					mergeHistoryItem.Index = num2;
					history.Push(mergeHistoryItem);
					return;
				}
				break;
			case MergeAction.Replace:
			case MergeAction.Remove:
			case MergeAction.MatchOnly:
			{
				ToolStripItem toolStripItem = ToolStripManager.FindMatch(source, destinationItems);
				if (toolStripItem != null)
				{
					MergeAction mergeAction = source.MergeAction;
					if (mergeAction - MergeAction.Replace > 1)
					{
						if (mergeAction != MergeAction.MatchOnly)
						{
							break;
						}
						ToolStripDropDownItem toolStripDropDownItem = toolStripItem as ToolStripDropDownItem;
						ToolStripDropDownItem toolStripDropDownItem2 = source as ToolStripDropDownItem;
						if (toolStripDropDownItem == null || toolStripDropDownItem2 == null || toolStripDropDownItem2.DropDownItems.Count == 0)
						{
							break;
						}
						int count = toolStripDropDownItem2.DropDownItems.Count;
						if (count <= 0)
						{
							break;
						}
						int num3 = count;
						toolStripDropDownItem2.DropDown.SuspendLayout();
						try
						{
							int i = 0;
							int num4 = 0;
							while (i < count)
							{
								ToolStripManager.MergeRecursive(toolStripDropDownItem2.DropDownItems[num4], toolStripDropDownItem.DropDownItems, history);
								int num5 = num3 - toolStripDropDownItem2.DropDownItems.Count;
								num4 = ((num5 > 0) ? num4 : (num4 + 1));
								num3 = toolStripDropDownItem2.DropDownItems.Count;
								i++;
							}
							break;
						}
						finally
						{
							toolStripDropDownItem2.DropDown.ResumeLayout();
						}
					}
					MergeHistoryItem mergeHistoryItem = new MergeHistoryItem(MergeAction.Insert);
					mergeHistoryItem.TargetItem = toolStripItem;
					int num6 = destinationItems.IndexOf(toolStripItem);
					destinationItems.RemoveAt(num6);
					mergeHistoryItem.Index = num6;
					mergeHistoryItem.IndexCollection = destinationItems;
					mergeHistoryItem.TargetItem = toolStripItem;
					history.Push(mergeHistoryItem);
					if (source.MergeAction == MergeAction.Replace)
					{
						mergeHistoryItem = new MergeHistoryItem(MergeAction.Remove);
						mergeHistoryItem.PreviousIndexCollection = source.Owner.Items;
						mergeHistoryItem.PreviousIndex = mergeHistoryItem.PreviousIndexCollection.IndexOf(source);
						mergeHistoryItem.TargetItem = source;
						destinationItems.Insert(num6, source);
						mergeHistoryItem.Index = num6;
						mergeHistoryItem.IndexCollection = destinationItems;
						history.Push(mergeHistoryItem);
						return;
					}
				}
				break;
			}
			default:
				return;
			}
		}

		/// <summary>Combines two <see cref="T:System.Windows.Forms.ToolStrip" /> objects of the same type.</summary>
		/// <param name="sourceToolStrip">The <see cref="T:System.Windows.Forms.ToolStrip" /> to be combined with the <see cref="T:System.Windows.Forms.ToolStrip" /> referred to by the <paramref name="targetName" /> parameter.</param>
		/// <param name="targetName">The name of the <see cref="T:System.Windows.Forms.ToolStrip" /> that receives the <see cref="T:System.Windows.Forms.ToolStrip" /> referred to by the <paramref name="sourceToolStrip" /> parameter.</param>
		/// <returns>
		///   <see langword="true" /> if the merge is successful; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceToolStrip" /> or <paramref name="targetName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceToolStrip" /> or <paramref name="targetName" /> refer to the same <see cref="T:System.Windows.Forms.ToolStrip" />.</exception>
		// Token: 0x060043BF RID: 17343 RVA: 0x0011E948 File Offset: 0x0011CB48
		public static bool Merge(ToolStrip sourceToolStrip, string targetName)
		{
			if (sourceToolStrip == null)
			{
				throw new ArgumentNullException("sourceToolStrip");
			}
			if (targetName == null)
			{
				throw new ArgumentNullException("targetName");
			}
			ToolStrip toolStrip = ToolStripManager.FindToolStrip(targetName);
			return toolStrip != null && ToolStripManager.Merge(sourceToolStrip, toolStrip);
		}

		// Token: 0x060043C0 RID: 17344 RVA: 0x0011E984 File Offset: 0x0011CB84
		internal static bool RevertMergeInternal(ToolStrip targetToolStrip, ToolStrip sourceToolStrip, bool revertMDIControls)
		{
			bool flag = false;
			if (targetToolStrip == null)
			{
				throw new ArgumentNullException("targetToolStrip");
			}
			if (targetToolStrip == sourceToolStrip)
			{
				throw new ArgumentException(SR.GetString("ToolStripMergeImpossibleIdentical"));
			}
			bool flag2 = false;
			if (sourceToolStrip != null)
			{
				foreach (MergeHistory mergeHistory in targetToolStrip.MergeHistoryStack)
				{
					flag2 = mergeHistory.MergedToolStrip == sourceToolStrip;
					if (flag2)
					{
						break;
					}
				}
				if (!flag2)
				{
					return false;
				}
			}
			if (sourceToolStrip != null)
			{
				sourceToolStrip.SuspendLayout();
			}
			targetToolStrip.SuspendLayout();
			try
			{
				Stack<ToolStrip> stack = new Stack<ToolStrip>();
				flag2 = false;
				while (targetToolStrip.MergeHistoryStack.Count > 0)
				{
					if (flag2)
					{
						break;
					}
					flag = true;
					MergeHistory mergeHistory2 = targetToolStrip.MergeHistoryStack.Pop();
					if (mergeHistory2.MergedToolStrip == sourceToolStrip)
					{
						flag2 = true;
					}
					else if (!revertMDIControls && sourceToolStrip == null)
					{
						if (ToolStripManager.IsSpecialMDIStrip(mergeHistory2.MergedToolStrip))
						{
							stack.Push(mergeHistory2.MergedToolStrip);
						}
					}
					else
					{
						stack.Push(mergeHistory2.MergedToolStrip);
					}
					while (mergeHistory2.MergeHistoryItemsStack.Count > 0)
					{
						MergeHistoryItem mergeHistoryItem = mergeHistory2.MergeHistoryItemsStack.Pop();
						MergeAction mergeAction = mergeHistoryItem.MergeAction;
						if (mergeAction != MergeAction.Insert)
						{
							if (mergeAction == MergeAction.Remove)
							{
								mergeHistoryItem.IndexCollection.Remove(mergeHistoryItem.TargetItem);
								mergeHistoryItem.PreviousIndexCollection.Insert(Math.Min(mergeHistoryItem.PreviousIndex, mergeHistoryItem.PreviousIndexCollection.Count), mergeHistoryItem.TargetItem);
							}
						}
						else
						{
							mergeHistoryItem.IndexCollection.Insert(Math.Min(mergeHistoryItem.Index, mergeHistoryItem.IndexCollection.Count), mergeHistoryItem.TargetItem);
						}
					}
				}
				while (stack.Count > 0)
				{
					ToolStrip toolStrip = stack.Pop();
					ToolStripManager.Merge(toolStrip, targetToolStrip);
				}
			}
			finally
			{
				if (sourceToolStrip != null)
				{
					sourceToolStrip.ResumeLayout();
				}
				targetToolStrip.ResumeLayout();
			}
			return flag;
		}

		/// <summary>Undoes a merging of two <see cref="T:System.Windows.Forms.ToolStrip" /> objects, returning the specified <see cref="T:System.Windows.Forms.ToolStrip" /> to its state before the merge and nullifying all previous merge operations.</summary>
		/// <param name="targetToolStrip">The <see cref="T:System.Windows.Forms.ToolStripItem" /> for which to undo a merge operation.</param>
		/// <returns>
		///   <see langword="true" /> if the undoing of the merge is successful; otherwise, <see langword="false" />.</returns>
		// Token: 0x060043C1 RID: 17345 RVA: 0x0011EB90 File Offset: 0x0011CD90
		public static bool RevertMerge(ToolStrip targetToolStrip)
		{
			return ToolStripManager.RevertMergeInternal(targetToolStrip, null, false);
		}

		/// <summary>Undoes a merging of two <see cref="T:System.Windows.Forms.ToolStrip" /> objects, returning both <see cref="T:System.Windows.Forms.ToolStrip" /> controls to their state before the merge and nullifying all previous merge operations.</summary>
		/// <param name="targetToolStrip">The name of the <see cref="T:System.Windows.Forms.ToolStripItem" /> for which to undo a merge operation.</param>
		/// <param name="sourceToolStrip">The <see cref="T:System.Windows.Forms.ToolStrip" /> that was merged with the <paramref name="targetToolStrip" />.</param>
		/// <returns>
		///   <see langword="true" /> if the undoing of the merge is successful; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="sourceToolStrip" /> is <see langword="null" />.</exception>
		// Token: 0x060043C2 RID: 17346 RVA: 0x0011EB9A File Offset: 0x0011CD9A
		public static bool RevertMerge(ToolStrip targetToolStrip, ToolStrip sourceToolStrip)
		{
			if (sourceToolStrip == null)
			{
				throw new ArgumentNullException("sourceToolStrip");
			}
			return ToolStripManager.RevertMergeInternal(targetToolStrip, sourceToolStrip, false);
		}

		/// <summary>Undoes a merging of two <see cref="T:System.Windows.Forms.ToolStrip" /> objects, returning the <see cref="T:System.Windows.Forms.ToolStrip" /> with the specified name to its state before the merge and nullifying all previous merge operations.</summary>
		/// <param name="targetName">The name of the <see cref="T:System.Windows.Forms.ToolStripItem" /> for which to undo a merge operation.</param>
		/// <returns>
		///   <see langword="true" /> if the undoing of the merge is successful; otherwise, <see langword="false" />.</returns>
		// Token: 0x060043C3 RID: 17347 RVA: 0x0011EBB4 File Offset: 0x0011CDB4
		public static bool RevertMerge(string targetName)
		{
			ToolStrip toolStrip = ToolStripManager.FindToolStrip(targetName);
			return toolStrip != null && ToolStripManager.RevertMerge(toolStrip);
		}

		// Token: 0x040025D8 RID: 9688
		[ThreadStatic]
		private static ClientUtils.WeakRefCollection toolStripWeakArrayList;

		// Token: 0x040025D9 RID: 9689
		[ThreadStatic]
		private static ClientUtils.WeakRefCollection toolStripPanelWeakArrayList;

		// Token: 0x040025DA RID: 9690
		[ThreadStatic]
		private static bool initialized;

		// Token: 0x040025DB RID: 9691
		private static Font defaultFont;

		// Token: 0x040025DC RID: 9692
		private static ConcurrentDictionary<int, Font> defaultFontCache = new ConcurrentDictionary<int, Font>();

		// Token: 0x040025DD RID: 9693
		[ThreadStatic]
		private static Delegate[] staticEventHandlers;

		// Token: 0x040025DE RID: 9694
		private const int staticEventDefaultRendererChanged = 0;

		// Token: 0x040025DF RID: 9695
		private const int staticEventCount = 1;

		// Token: 0x040025E0 RID: 9696
		private static object internalSyncObject = new object();

		// Token: 0x040025E1 RID: 9697
		private static int currentDpi = DpiHelper.DeviceDpi;

		// Token: 0x040025E2 RID: 9698
		[ThreadStatic]
		private static ToolStripRenderer defaultRenderer;

		// Token: 0x040025E3 RID: 9699
		internal static Type SystemRendererType = typeof(ToolStripSystemRenderer);

		// Token: 0x040025E4 RID: 9700
		internal static Type ProfessionalRendererType = typeof(ToolStripProfessionalRenderer);

		// Token: 0x040025E5 RID: 9701
		private static bool visualStylesEnabledIfPossible = true;

		// Token: 0x040025E6 RID: 9702
		[ThreadStatic]
		private static Type currentRendererType;

		// Token: 0x02000808 RID: 2056
		internal class ModalMenuFilter : IMessageModifyAndFilter, IMessageFilter
		{
			// Token: 0x17001848 RID: 6216
			// (get) Token: 0x06006EEB RID: 28395 RVA: 0x00196328 File Offset: 0x00194528
			internal static ToolStripManager.ModalMenuFilter Instance
			{
				get
				{
					if (ToolStripManager.ModalMenuFilter._instance == null)
					{
						ToolStripManager.ModalMenuFilter._instance = new ToolStripManager.ModalMenuFilter();
					}
					return ToolStripManager.ModalMenuFilter._instance;
				}
			}

			// Token: 0x06006EEC RID: 28396 RVA: 0x00196340 File Offset: 0x00194540
			private ModalMenuFilter()
			{
			}

			// Token: 0x17001849 RID: 6217
			// (get) Token: 0x06006EED RID: 28397 RVA: 0x0019636A File Offset: 0x0019456A
			internal static HandleRef ActiveHwnd
			{
				get
				{
					return ToolStripManager.ModalMenuFilter.Instance.ActiveHwndInternal;
				}
			}

			// Token: 0x1700184A RID: 6218
			// (get) Token: 0x06006EEE RID: 28398 RVA: 0x00196376 File Offset: 0x00194576
			// (set) Token: 0x06006EEF RID: 28399 RVA: 0x0019637E File Offset: 0x0019457E
			public bool ShowUnderlines
			{
				get
				{
					return this._showUnderlines;
				}
				set
				{
					if (this._showUnderlines != value)
					{
						this._showUnderlines = value;
						ToolStripManager.NotifyMenuModeChange(true, false);
					}
				}
			}

			// Token: 0x1700184B RID: 6219
			// (get) Token: 0x06006EF0 RID: 28400 RVA: 0x00196397 File Offset: 0x00194597
			// (set) Token: 0x06006EF1 RID: 28401 RVA: 0x001963A0 File Offset: 0x001945A0
			private HandleRef ActiveHwndInternal
			{
				get
				{
					return this._activeHwnd;
				}
				set
				{
					if (this._activeHwnd.Handle != value.Handle)
					{
						Control control;
						if (this._activeHwnd.Handle != IntPtr.Zero)
						{
							control = Control.FromHandleInternal(this._activeHwnd.Handle);
							if (control != null)
							{
								control.HandleCreated -= this.OnActiveHwndHandleCreated;
							}
						}
						this._activeHwnd = value;
						control = Control.FromHandleInternal(this._activeHwnd.Handle);
						if (control != null)
						{
							control.HandleCreated += this.OnActiveHwndHandleCreated;
						}
					}
				}
			}

			// Token: 0x1700184C RID: 6220
			// (get) Token: 0x06006EF2 RID: 28402 RVA: 0x00196432 File Offset: 0x00194632
			internal static bool InMenuMode
			{
				get
				{
					return ToolStripManager.ModalMenuFilter.Instance._inMenuMode;
				}
			}

			// Token: 0x1700184D RID: 6221
			// (get) Token: 0x06006EF3 RID: 28403 RVA: 0x0019643E File Offset: 0x0019463E
			// (set) Token: 0x06006EF4 RID: 28404 RVA: 0x0019644A File Offset: 0x0019464A
			internal static bool MenuKeyToggle
			{
				get
				{
					return ToolStripManager.ModalMenuFilter.Instance.menuKeyToggle;
				}
				set
				{
					if (ToolStripManager.ModalMenuFilter.Instance.menuKeyToggle != value)
					{
						ToolStripManager.ModalMenuFilter.Instance.menuKeyToggle = value;
					}
				}
			}

			// Token: 0x1700184E RID: 6222
			// (get) Token: 0x06006EF5 RID: 28405 RVA: 0x00196464 File Offset: 0x00194664
			private ToolStripManager.ModalMenuFilter.HostedWindowsFormsMessageHook MessageHook
			{
				get
				{
					if (this.messageHook == null)
					{
						this.messageHook = new ToolStripManager.ModalMenuFilter.HostedWindowsFormsMessageHook();
					}
					return this.messageHook;
				}
			}

			// Token: 0x06006EF6 RID: 28406 RVA: 0x00196480 File Offset: 0x00194680
			private void EnterMenuModeCore()
			{
				if (!ToolStripManager.ModalMenuFilter.InMenuMode)
				{
					IntPtr activeWindow = UnsafeNativeMethods.GetActiveWindow();
					if (activeWindow != IntPtr.Zero)
					{
						this.ActiveHwndInternal = new HandleRef(this, activeWindow);
					}
					Application.ThreadContext.FromCurrent().AddMessageFilter(this);
					Application.ThreadContext.FromCurrent().TrackInput(true);
					if (!Application.ThreadContext.FromCurrent().GetMessageLoop(true))
					{
						this.MessageHook.HookMessages = true;
					}
					this._inMenuMode = true;
					if (!AccessibilityImprovements.UseLegacyToolTipDisplay)
					{
						this.NotifyLastLastFocusedToolAboutFocusLoss();
					}
					this.ProcessMessages(true);
				}
			}

			// Token: 0x06006EF7 RID: 28407 RVA: 0x00196500 File Offset: 0x00194700
			internal void NotifyLastLastFocusedToolAboutFocusLoss()
			{
				IKeyboardToolTip keyboardToolTip = KeyboardToolTipStateMachine.Instance.LastFocusedTool;
				if (keyboardToolTip != null)
				{
					this.lastFocusedTool.SetTarget(keyboardToolTip);
					KeyboardToolTipStateMachine.Instance.NotifyAboutLostFocus(keyboardToolTip);
				}
			}

			// Token: 0x06006EF8 RID: 28408 RVA: 0x00196532 File Offset: 0x00194732
			internal static void ExitMenuMode()
			{
				ToolStripManager.ModalMenuFilter.Instance.ExitMenuModeCore();
			}

			// Token: 0x06006EF9 RID: 28409 RVA: 0x00196540 File Offset: 0x00194740
			private void ExitMenuModeCore()
			{
				this.ProcessMessages(false);
				if (ToolStripManager.ModalMenuFilter.InMenuMode)
				{
					try
					{
						if (this.messageHook != null)
						{
							this.messageHook.HookMessages = false;
						}
						Application.ThreadContext.FromCurrent().RemoveMessageFilter(this);
						Application.ThreadContext.FromCurrent().TrackInput(false);
						if (ToolStripManager.ModalMenuFilter.ActiveHwnd.Handle != IntPtr.Zero)
						{
							Control control = Control.FromHandleInternal(ToolStripManager.ModalMenuFilter.ActiveHwnd.Handle);
							if (control != null)
							{
								control.HandleCreated -= this.OnActiveHwndHandleCreated;
							}
							this.ActiveHwndInternal = NativeMethods.NullHandleRef;
						}
						if (this._inputFilterQueue != null)
						{
							this._inputFilterQueue.Clear();
						}
						if (this._caretHidden)
						{
							this._caretHidden = false;
							SafeNativeMethods.ShowCaret(NativeMethods.NullHandleRef);
						}
						IKeyboardToolTip keyboardToolTip;
						if (!AccessibilityImprovements.UseLegacyToolTipDisplay && this.lastFocusedTool.TryGetTarget(out keyboardToolTip) && keyboardToolTip != null)
						{
							KeyboardToolTipStateMachine.Instance.NotifyAboutGotFocus(keyboardToolTip);
						}
					}
					finally
					{
						this._inMenuMode = false;
						bool showUnderlines = this._showUnderlines;
						this._showUnderlines = false;
						ToolStripManager.NotifyMenuModeChange(showUnderlines, true);
					}
				}
			}

			// Token: 0x06006EFA RID: 28410 RVA: 0x00196654 File Offset: 0x00194854
			internal static ToolStrip GetActiveToolStrip()
			{
				return ToolStripManager.ModalMenuFilter.Instance.GetActiveToolStripInternal();
			}

			// Token: 0x06006EFB RID: 28411 RVA: 0x00196660 File Offset: 0x00194860
			internal ToolStrip GetActiveToolStripInternal()
			{
				if (this._inputFilterQueue != null && this._inputFilterQueue.Count > 0)
				{
					return this._inputFilterQueue[this._inputFilterQueue.Count - 1];
				}
				return null;
			}

			// Token: 0x06006EFC RID: 28412 RVA: 0x00196694 File Offset: 0x00194894
			private ToolStrip GetCurrentToplevelToolStrip()
			{
				if (this._toplevelToolStrip == null)
				{
					ToolStrip activeToolStripInternal = this.GetActiveToolStripInternal();
					if (activeToolStripInternal != null)
					{
						this._toplevelToolStrip = activeToolStripInternal.GetToplevelOwnerToolStrip();
					}
				}
				return this._toplevelToolStrip;
			}

			// Token: 0x06006EFD RID: 28413 RVA: 0x001966C8 File Offset: 0x001948C8
			private void OnActiveHwndHandleCreated(object sender, EventArgs e)
			{
				Control control = sender as Control;
				this.ActiveHwndInternal = new HandleRef(this, control.Handle);
			}

			// Token: 0x06006EFE RID: 28414 RVA: 0x001966F0 File Offset: 0x001948F0
			internal static void ProcessMenuKeyDown(ref Message m)
			{
				Keys keys = (Keys)(int)m.WParam;
				ToolStrip toolStrip = Control.FromHandleInternal(m.HWnd) as ToolStrip;
				if (toolStrip != null && !toolStrip.IsDropDown)
				{
					return;
				}
				if (ToolStripManager.IsMenuKey(keys))
				{
					if (!ToolStripManager.ModalMenuFilter.InMenuMode && ToolStripManager.ModalMenuFilter.MenuKeyToggle)
					{
						ToolStripManager.ModalMenuFilter.MenuKeyToggle = false;
						return;
					}
					if (!ToolStripManager.ModalMenuFilter.MenuKeyToggle)
					{
						ToolStripManager.ModalMenuFilter.Instance.ShowUnderlines = true;
					}
				}
			}

			// Token: 0x06006EFF RID: 28415 RVA: 0x00196755 File Offset: 0x00194955
			internal static void CloseActiveDropDown(ToolStripDropDown activeToolStripDropDown, ToolStripDropDownCloseReason reason)
			{
				activeToolStripDropDown.SetCloseReason(reason);
				activeToolStripDropDown.Visible = false;
				if (ToolStripManager.ModalMenuFilter.GetActiveToolStrip() == null)
				{
					ToolStripManager.ModalMenuFilter.ExitMenuMode();
					if (activeToolStripDropDown.OwnerItem != null)
					{
						activeToolStripDropDown.OwnerItem.Unselect();
					}
				}
			}

			// Token: 0x06006F00 RID: 28416 RVA: 0x00196784 File Offset: 0x00194984
			private void ProcessMessages(bool process)
			{
				if (process)
				{
					if (this._ensureMessageProcessingTimer == null)
					{
						this._ensureMessageProcessingTimer = new Timer();
					}
					this._ensureMessageProcessingTimer.Interval = 500;
					this._ensureMessageProcessingTimer.Enabled = true;
					return;
				}
				if (this._ensureMessageProcessingTimer != null)
				{
					this._ensureMessageProcessingTimer.Enabled = false;
					this._ensureMessageProcessingTimer.Dispose();
					this._ensureMessageProcessingTimer = null;
				}
			}

			// Token: 0x06006F01 RID: 28417 RVA: 0x001967EC File Offset: 0x001949EC
			private void ProcessMouseButtonPressed(IntPtr hwndMouseMessageIsFrom, int x, int y)
			{
				int count = this._inputFilterQueue.Count;
				for (int i = 0; i < count; i++)
				{
					ToolStrip activeToolStripInternal = this.GetActiveToolStripInternal();
					if (activeToolStripInternal == null)
					{
						break;
					}
					NativeMethods.POINT point = new NativeMethods.POINT();
					point.x = x;
					point.y = y;
					UnsafeNativeMethods.MapWindowPoints(new HandleRef(activeToolStripInternal, hwndMouseMessageIsFrom), new HandleRef(activeToolStripInternal, activeToolStripInternal.Handle), point, 1);
					if (activeToolStripInternal.ClientRectangle.Contains(point.x, point.y))
					{
						break;
					}
					ToolStripDropDown toolStripDropDown = activeToolStripInternal as ToolStripDropDown;
					if (toolStripDropDown != null)
					{
						if (toolStripDropDown.OwnerToolStrip == null || !(toolStripDropDown.OwnerToolStrip.Handle == hwndMouseMessageIsFrom) || toolStripDropDown.OwnerDropDownItem == null || !toolStripDropDown.OwnerDropDownItem.DropDownButtonArea.Contains(x, y))
						{
							ToolStripManager.ModalMenuFilter.CloseActiveDropDown(toolStripDropDown, ToolStripDropDownCloseReason.AppClicked);
						}
					}
					else
					{
						activeToolStripInternal.NotifySelectionChange(null);
						this.ExitMenuModeCore();
					}
				}
			}

			// Token: 0x06006F02 RID: 28418 RVA: 0x001968D4 File Offset: 0x00194AD4
			private bool ProcessActivationChange()
			{
				int count = this._inputFilterQueue.Count;
				for (int i = 0; i < count; i++)
				{
					ToolStripDropDown toolStripDropDown = this.GetActiveToolStripInternal() as ToolStripDropDown;
					if (toolStripDropDown != null && toolStripDropDown.AutoClose)
					{
						toolStripDropDown.Visible = false;
					}
				}
				this.ExitMenuModeCore();
				return true;
			}

			// Token: 0x06006F03 RID: 28419 RVA: 0x0019691E File Offset: 0x00194B1E
			internal static void SetActiveToolStrip(ToolStrip toolStrip, bool menuKeyPressed)
			{
				if (!ToolStripManager.ModalMenuFilter.InMenuMode && menuKeyPressed)
				{
					ToolStripManager.ModalMenuFilter.Instance.ShowUnderlines = true;
				}
				ToolStripManager.ModalMenuFilter.Instance.SetActiveToolStripCore(toolStrip);
			}

			// Token: 0x06006F04 RID: 28420 RVA: 0x00196942 File Offset: 0x00194B42
			internal static void SetActiveToolStrip(ToolStrip toolStrip)
			{
				ToolStripManager.ModalMenuFilter.Instance.SetActiveToolStripCore(toolStrip);
			}

			// Token: 0x06006F05 RID: 28421 RVA: 0x00196950 File Offset: 0x00194B50
			private void SetActiveToolStripCore(ToolStrip toolStrip)
			{
				if (toolStrip == null)
				{
					return;
				}
				if (toolStrip.IsDropDown)
				{
					ToolStripDropDown toolStripDropDown = toolStrip as ToolStripDropDown;
					if (!toolStripDropDown.AutoClose)
					{
						IntPtr activeWindow = UnsafeNativeMethods.GetActiveWindow();
						if (activeWindow != IntPtr.Zero)
						{
							this.ActiveHwndInternal = new HandleRef(this, activeWindow);
						}
						return;
					}
				}
				toolStrip.KeyboardActive = true;
				if (this._inputFilterQueue == null)
				{
					this._inputFilterQueue = new List<ToolStrip>();
				}
				else
				{
					ToolStrip activeToolStripInternal = this.GetActiveToolStripInternal();
					if (activeToolStripInternal != null)
					{
						if (!activeToolStripInternal.IsDropDown)
						{
							this._inputFilterQueue.Remove(activeToolStripInternal);
						}
						else if (toolStrip.IsDropDown && ToolStripDropDown.GetFirstDropDown(toolStrip) != ToolStripDropDown.GetFirstDropDown(activeToolStripInternal))
						{
							this._inputFilterQueue.Remove(activeToolStripInternal);
							ToolStripDropDown toolStripDropDown2 = activeToolStripInternal as ToolStripDropDown;
							toolStripDropDown2.DismissAll();
						}
					}
				}
				this._toplevelToolStrip = null;
				if (!this._inputFilterQueue.Contains(toolStrip))
				{
					this._inputFilterQueue.Add(toolStrip);
				}
				if (!ToolStripManager.ModalMenuFilter.InMenuMode && this._inputFilterQueue.Count > 0)
				{
					this.EnterMenuModeCore();
				}
				if (!this._caretHidden && toolStrip.IsDropDown && ToolStripManager.ModalMenuFilter.InMenuMode)
				{
					this._caretHidden = true;
					SafeNativeMethods.HideCaret(NativeMethods.NullHandleRef);
				}
			}

			// Token: 0x06006F06 RID: 28422 RVA: 0x00196A6B File Offset: 0x00194C6B
			internal static void SuspendMenuMode()
			{
				ToolStripManager.ModalMenuFilter.Instance._suspendMenuMode = true;
			}

			// Token: 0x06006F07 RID: 28423 RVA: 0x00196A78 File Offset: 0x00194C78
			internal static void ResumeMenuMode()
			{
				ToolStripManager.ModalMenuFilter.Instance._suspendMenuMode = false;
			}

			// Token: 0x06006F08 RID: 28424 RVA: 0x00196A85 File Offset: 0x00194C85
			internal static void RemoveActiveToolStrip(ToolStrip toolStrip)
			{
				ToolStripManager.ModalMenuFilter.Instance.RemoveActiveToolStripCore(toolStrip);
			}

			// Token: 0x06006F09 RID: 28425 RVA: 0x00196A92 File Offset: 0x00194C92
			private void RemoveActiveToolStripCore(ToolStrip toolStrip)
			{
				this._toplevelToolStrip = null;
				if (this._inputFilterQueue != null)
				{
					this._inputFilterQueue.Remove(toolStrip);
				}
			}

			// Token: 0x06006F0A RID: 28426 RVA: 0x00196AB0 File Offset: 0x00194CB0
			private static bool IsChildOrSameWindow(HandleRef hwndParent, HandleRef hwndChild)
			{
				return hwndParent.Handle == hwndChild.Handle || UnsafeNativeMethods.IsChild(hwndParent, hwndChild);
			}

			// Token: 0x06006F0B RID: 28427 RVA: 0x00196AD8 File Offset: 0x00194CD8
			private static bool IsKeyOrMouseMessage(Message m)
			{
				bool flag = false;
				if (m.Msg >= 512 && m.Msg <= 522)
				{
					flag = true;
				}
				else if (m.Msg >= 161 && m.Msg <= 169)
				{
					flag = true;
				}
				else if (m.Msg >= 256 && m.Msg <= 264)
				{
					flag = true;
				}
				return flag;
			}

			// Token: 0x06006F0C RID: 28428 RVA: 0x00196B48 File Offset: 0x00194D48
			public bool PreFilterMessage(ref Message m)
			{
				if (this._suspendMenuMode)
				{
					return false;
				}
				ToolStrip activeToolStrip = ToolStripManager.ModalMenuFilter.GetActiveToolStrip();
				if (activeToolStrip == null)
				{
					return false;
				}
				if (activeToolStrip.IsDisposed)
				{
					this.RemoveActiveToolStripCore(activeToolStrip);
					return false;
				}
				HandleRef handleRef = new HandleRef(activeToolStrip, activeToolStrip.Handle);
				HandleRef handleRef2 = new HandleRef(null, UnsafeNativeMethods.GetActiveWindow());
				if (handleRef2.Handle != this._lastActiveWindow.Handle)
				{
					if (handleRef2.Handle == IntPtr.Zero)
					{
						this.ProcessActivationChange();
					}
					else if (!(Control.FromChildHandleInternal(handleRef2.Handle) is ToolStripDropDown) && !ToolStripManager.ModalMenuFilter.IsChildOrSameWindow(handleRef2, handleRef) && !ToolStripManager.ModalMenuFilter.IsChildOrSameWindow(handleRef2, ToolStripManager.ModalMenuFilter.ActiveHwnd))
					{
						this.ProcessActivationChange();
					}
				}
				this._lastActiveWindow = handleRef2;
				if (!ToolStripManager.ModalMenuFilter.IsKeyOrMouseMessage(m))
				{
					return false;
				}
				DpiAwarenessContext dpiAwarenessContext = CommonUnsafeNativeMethods.TryGetDpiAwarenessContextForWindow(m.HWnd);
				using (DpiHelper.EnterDpiAwarenessScope(dpiAwarenessContext))
				{
					int msg = m.Msg;
					if (msg <= 167)
					{
						switch (msg)
						{
						case 160:
							goto IL_153;
						case 161:
						case 164:
							break;
						case 162:
						case 163:
							goto IL_23E;
						default:
							if (msg != 167)
							{
								goto IL_23E;
							}
							break;
						}
						this.ProcessMouseButtonPressed(IntPtr.Zero, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam));
						goto IL_23E;
					}
					if (msg - 256 > 7)
					{
						switch (msg)
						{
						case 512:
							goto IL_153;
						case 513:
						case 516:
							break;
						case 514:
						case 515:
							goto IL_23E;
						default:
							if (msg != 519)
							{
								goto IL_23E;
							}
							break;
						}
						this.ProcessMouseButtonPressed(m.HWnd, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam));
						goto IL_23E;
					}
					if (!activeToolStrip.ContainsFocus)
					{
						m.HWnd = activeToolStrip.Handle;
						goto IL_23E;
					}
					goto IL_23E;
					IL_153:
					Control control = Control.FromChildHandleInternal(m.HWnd);
					if ((control == null || !(control.TopLevelControlInternal is ToolStripDropDown)) && !ToolStripManager.ModalMenuFilter.IsChildOrSameWindow(handleRef, new HandleRef(null, m.HWnd)))
					{
						ToolStrip currentToplevelToolStrip = this.GetCurrentToplevelToolStrip();
						if (currentToplevelToolStrip != null && ToolStripManager.ModalMenuFilter.IsChildOrSameWindow(new HandleRef(currentToplevelToolStrip, currentToplevelToolStrip.Handle), new HandleRef(null, m.HWnd)))
						{
							return false;
						}
						if (!ToolStripManager.ModalMenuFilter.IsChildOrSameWindow(ToolStripManager.ModalMenuFilter.ActiveHwnd, new HandleRef(null, m.HWnd)))
						{
							return false;
						}
						return true;
					}
					IL_23E:;
				}
				return false;
			}

			// Token: 0x04004302 RID: 17154
			private HandleRef _activeHwnd = NativeMethods.NullHandleRef;

			// Token: 0x04004303 RID: 17155
			private HandleRef _lastActiveWindow = NativeMethods.NullHandleRef;

			// Token: 0x04004304 RID: 17156
			private List<ToolStrip> _inputFilterQueue;

			// Token: 0x04004305 RID: 17157
			private bool _inMenuMode;

			// Token: 0x04004306 RID: 17158
			private bool _caretHidden;

			// Token: 0x04004307 RID: 17159
			private bool _showUnderlines;

			// Token: 0x04004308 RID: 17160
			private bool menuKeyToggle;

			// Token: 0x04004309 RID: 17161
			private bool _suspendMenuMode;

			// Token: 0x0400430A RID: 17162
			private ToolStripManager.ModalMenuFilter.HostedWindowsFormsMessageHook messageHook;

			// Token: 0x0400430B RID: 17163
			private Timer _ensureMessageProcessingTimer;

			// Token: 0x0400430C RID: 17164
			private const int MESSAGE_PROCESSING_INTERVAL = 500;

			// Token: 0x0400430D RID: 17165
			private ToolStrip _toplevelToolStrip;

			// Token: 0x0400430E RID: 17166
			private readonly WeakReference<IKeyboardToolTip> lastFocusedTool = new WeakReference<IKeyboardToolTip>(null);

			// Token: 0x0400430F RID: 17167
			[ThreadStatic]
			private static ToolStripManager.ModalMenuFilter _instance;

			// Token: 0x020008C9 RID: 2249
			private class HostedWindowsFormsMessageHook
			{
				// Token: 0x1700193B RID: 6459
				// (get) Token: 0x060072D3 RID: 29395 RVA: 0x001A34E8 File Offset: 0x001A16E8
				// (set) Token: 0x060072D4 RID: 29396 RVA: 0x001A34FA File Offset: 0x001A16FA
				public bool HookMessages
				{
					get
					{
						return this.messageHookHandle != IntPtr.Zero;
					}
					set
					{
						if (value)
						{
							this.InstallMessageHook();
							return;
						}
						this.UninstallMessageHook();
					}
				}

				// Token: 0x060072D5 RID: 29397 RVA: 0x001A350C File Offset: 0x001A170C
				private void InstallMessageHook()
				{
					lock (this)
					{
						if (!(this.messageHookHandle != IntPtr.Zero))
						{
							this.hookProc = new NativeMethods.HookProc(this.MessageHookProc);
							this.messageHookHandle = UnsafeNativeMethods.SetWindowsHookEx(3, this.hookProc, new HandleRef(null, IntPtr.Zero), SafeNativeMethods.GetCurrentThreadId());
							if (this.messageHookHandle != IntPtr.Zero)
							{
								this.isHooked = true;
							}
						}
					}
				}

				// Token: 0x060072D6 RID: 29398 RVA: 0x001A35A4 File Offset: 0x001A17A4
				private unsafe IntPtr MessageHookProc(int nCode, IntPtr wparam, IntPtr lparam)
				{
					if (nCode == 0 && this.isHooked && (int)wparam == 1)
					{
						NativeMethods.MSG* ptr = (NativeMethods.MSG*)(void*)lparam;
						if (ptr != null && Application.ThreadContext.FromCurrent().PreTranslateMessage(ref *ptr))
						{
							ptr->message = 0;
						}
					}
					return UnsafeNativeMethods.CallNextHookEx(new HandleRef(this, this.messageHookHandle), nCode, wparam, lparam);
				}

				// Token: 0x060072D7 RID: 29399 RVA: 0x001A35FC File Offset: 0x001A17FC
				private void UninstallMessageHook()
				{
					lock (this)
					{
						if (this.messageHookHandle != IntPtr.Zero)
						{
							UnsafeNativeMethods.UnhookWindowsHookEx(new HandleRef(this, this.messageHookHandle));
							this.hookProc = null;
							this.messageHookHandle = IntPtr.Zero;
							this.isHooked = false;
						}
					}
				}

				// Token: 0x04004552 RID: 17746
				private IntPtr messageHookHandle = IntPtr.Zero;

				// Token: 0x04004553 RID: 17747
				private bool isHooked;

				// Token: 0x04004554 RID: 17748
				private NativeMethods.HookProc hookProc;
			}
		}
	}
}
