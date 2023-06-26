using System;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms.Automation;
using Accessibility;

namespace System.Windows.Forms
{
	/// <summary>Provides information that accessibility applications use to adjust an application's user interface (UI) for users with impairments.</summary>
	// Token: 0x02000117 RID: 279
	[ComVisible(true)]
	public class AccessibleObject : StandardOleMarshalObject, IReflect, IAccessible, UnsafeNativeMethods.IAccessibleEx, UnsafeNativeMethods.IServiceProvider, UnsafeNativeMethods.IRawElementProviderSimple, UnsafeNativeMethods.IRawElementProviderFragment, UnsafeNativeMethods.IRawElementProviderFragmentRoot, UnsafeNativeMethods.IInvokeProvider, UnsafeNativeMethods.IValueProvider, UnsafeNativeMethods.IRangeValueProvider, UnsafeNativeMethods.IExpandCollapseProvider, UnsafeNativeMethods.IToggleProvider, UnsafeNativeMethods.ITableProvider, UnsafeNativeMethods.ITableItemProvider, UnsafeNativeMethods.IGridProvider, UnsafeNativeMethods.IGridItemProvider, UnsafeNativeMethods.IEnumVariant, UnsafeNativeMethods.IOleWindow, UnsafeNativeMethods.ILegacyIAccessibleProvider, UnsafeNativeMethods.ISelectionProvider, UnsafeNativeMethods.ISelectionItemProvider, UnsafeNativeMethods.IRawElementProviderHwndOverride, UnsafeNativeMethods.IScrollItemProvider, UnsafeNativeMethods.UiaCore.ITextProvider, UnsafeNativeMethods.UiaCore.ITextProvider2
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AccessibleObject" /> class.</summary>
		// Token: 0x0600077E RID: 1918 RVA: 0x000158F3 File Offset: 0x00013AF3
		public AccessibleObject()
		{
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00015903 File Offset: 0x00013B03
		private AccessibleObject(IAccessible iAcc)
		{
			this.systemIAccessible = iAcc;
			this.systemWrapper = true;
		}

		/// <summary>Gets the location and size of the accessible object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the accessible object.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The bounds of control cannot be retrieved.</exception>
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x00015924 File Offset: 0x00013B24
		public virtual Rectangle Bounds
		{
			get
			{
				if (this.systemIAccessible != null)
				{
					int num = 0;
					int num2 = 0;
					int num3 = 0;
					int num4 = 0;
					try
					{
						this.systemIAccessible.accLocation(out num, out num2, out num3, out num4, 0);
						return new Rectangle(num, num2, num3, num4);
					}
					catch (COMException ex)
					{
						if (ex.ErrorCode != -2147352573)
						{
							throw ex;
						}
					}
				}
				return Rectangle.Empty;
			}
		}

		/// <summary>Gets a string that describes the default action of the object. Not all objects have a default action.</summary>
		/// <returns>A description of the default action for an object, or <see langword="null" /> if this object has no default action.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The default action for the control cannot be retrieved.</exception>
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x00015994 File Offset: 0x00013B94
		public virtual string DefaultAction
		{
			get
			{
				if (this.systemIAccessible != null)
				{
					try
					{
						return this.systemIAccessible.get_accDefaultAction(0);
					}
					catch (COMException ex)
					{
						if (ex.ErrorCode != -2147352573)
						{
							throw ex;
						}
					}
				}
				return null;
			}
		}

		/// <summary>Gets a string that describes the visual appearance of the specified object. Not all objects have a description.</summary>
		/// <returns>A description of the object's visual appearance to the user, or <see langword="null" /> if the object does not have a description.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The description for the control cannot be retrieved.</exception>
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x000159E4 File Offset: 0x00013BE4
		public virtual string Description
		{
			get
			{
				if (this.systemIAccessible != null)
				{
					try
					{
						return this.systemIAccessible.get_accDescription(0);
					}
					catch (COMException ex)
					{
						if (ex.ErrorCode != -2147352573)
						{
							throw ex;
						}
					}
				}
				return null;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x00015A34 File Offset: 0x00013C34
		private UnsafeNativeMethods.IEnumVariant EnumVariant
		{
			get
			{
				if (this.enumVariant == null)
				{
					this.enumVariant = new AccessibleObject.EnumVariantObject(this);
				}
				return this.enumVariant;
			}
		}

		/// <summary>Gets a description of what the object does or how the object is used.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the description of what the object does or how the object is used. Returns <see langword="null" /> if no help is defined.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The help string for the control cannot be retrieved.</exception>
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x00015A50 File Offset: 0x00013C50
		public virtual string Help
		{
			get
			{
				if (this.systemIAccessible != null)
				{
					try
					{
						return this.systemIAccessible.get_accHelp(0);
					}
					catch (COMException ex)
					{
						if (ex.ErrorCode != -2147352573)
						{
							throw ex;
						}
					}
				}
				return null;
			}
		}

		/// <summary>Gets the shortcut key or access key for the accessible object.</summary>
		/// <returns>The shortcut key or access key for the accessible object, or <see langword="null" /> if there is no shortcut key associated with the object.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The shortcut for the control cannot be retrieved.</exception>
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x00015AA0 File Offset: 0x00013CA0
		public virtual string KeyboardShortcut
		{
			get
			{
				if (this.systemIAccessible != null)
				{
					try
					{
						return this.systemIAccessible.get_accKeyboardShortcut(0);
					}
					catch (COMException ex)
					{
						if (ex.ErrorCode != -2147352573)
						{
							throw ex;
						}
					}
				}
				return null;
			}
		}

		/// <summary>Gets or sets the object name.</summary>
		/// <returns>The object name, or <see langword="null" /> if the property has not been set.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The name of the control cannot be retrieved or set.</exception>
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x00015AF0 File Offset: 0x00013CF0
		// (set) Token: 0x06000787 RID: 1927 RVA: 0x00015B40 File Offset: 0x00013D40
		public virtual string Name
		{
			get
			{
				if (this.systemIAccessible != null)
				{
					try
					{
						return this.systemIAccessible.get_accName(0);
					}
					catch (COMException ex)
					{
						if (ex.ErrorCode != -2147352573)
						{
							throw ex;
						}
					}
				}
				return null;
			}
			set
			{
				if (this.systemIAccessible != null)
				{
					try
					{
						this.systemIAccessible.set_accName(0, value);
					}
					catch (COMException ex)
					{
						if (ex.ErrorCode != -2147352573)
						{
							throw ex;
						}
					}
				}
			}
		}

		/// <summary>Gets the parent of an accessible object.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the parent of an accessible object, or <see langword="null" /> if there is no parent object.</returns>
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x00015B8C File Offset: 0x00013D8C
		public virtual AccessibleObject Parent
		{
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				if (this.systemIAccessible != null)
				{
					return this.WrapIAccessible(this.systemIAccessible.accParent);
				}
				return null;
			}
		}

		/// <summary>Gets the role of this accessible object.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleRole" /> values, or <see cref="F:System.Windows.Forms.AccessibleRole.None" /> if no role has been specified.</returns>
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x00015BA9 File Offset: 0x00013DA9
		public virtual AccessibleRole Role
		{
			get
			{
				if (this.systemIAccessible != null)
				{
					return (AccessibleRole)this.systemIAccessible.get_accRole(0);
				}
				return AccessibleRole.None;
			}
		}

		/// <summary>Gets the state of this accessible object.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleStates" /> values, or <see cref="F:System.Windows.Forms.AccessibleStates.None" />, if no state has been set.</returns>
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x00015BCB File Offset: 0x00013DCB
		public virtual AccessibleStates State
		{
			get
			{
				if (this.systemIAccessible != null)
				{
					return (AccessibleStates)this.systemIAccessible.get_accState(0);
				}
				return AccessibleStates.None;
			}
		}

		/// <summary>Gets or sets the value of an accessible object.</summary>
		/// <returns>The value of an accessible object, or <see langword="null" /> if the object has no value set.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The value cannot be set or retrieved.</exception>
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x00015BF0 File Offset: 0x00013DF0
		// (set) Token: 0x0600078C RID: 1932 RVA: 0x00015C44 File Offset: 0x00013E44
		public virtual string Value
		{
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				if (this.systemIAccessible != null)
				{
					try
					{
						return this.systemIAccessible.get_accValue(0);
					}
					catch (COMException ex)
					{
						if (ex.ErrorCode != -2147352573)
						{
							throw ex;
						}
					}
				}
				return "";
			}
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				if (this.systemIAccessible != null)
				{
					try
					{
						this.systemIAccessible.set_accValue(0, value);
					}
					catch (COMException ex)
					{
						if (ex.ErrorCode != -2147352573)
						{
							throw ex;
						}
					}
				}
			}
		}

		/// <summary>Retrieves the accessible child corresponding to the specified index.</summary>
		/// <param name="index">The zero-based index of the accessible child.</param>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the accessible child corresponding to the specified index.</returns>
		// Token: 0x0600078D RID: 1933 RVA: 0x00015C90 File Offset: 0x00013E90
		public virtual AccessibleObject GetChild(int index)
		{
			return null;
		}

		/// <summary>Retrieves the number of children belonging to an accessible object.</summary>
		/// <returns>The number of children belonging to an accessible object.</returns>
		// Token: 0x0600078E RID: 1934 RVA: 0x00015C93 File Offset: 0x00013E93
		public virtual int GetChildCount()
		{
			return -1;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00015C90 File Offset: 0x00013E90
		internal virtual int[] GetSysChildOrder()
		{
			return null;
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00015C96 File Offset: 0x00013E96
		internal virtual bool GetSysChild(AccessibleNavigation navdir, out AccessibleObject accessibleObject)
		{
			accessibleObject = null;
			return false;
		}

		/// <summary>Retrieves the object that has the keyboard focus.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that specifies the currently focused child. This method returns the calling object if the object itself is focused. Returns <see langword="null" /> if no object has focus.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The control cannot be retrieved.</exception>
		// Token: 0x06000791 RID: 1937 RVA: 0x00015C9C File Offset: 0x00013E9C
		public virtual AccessibleObject GetFocused()
		{
			if (this.GetChildCount() < 0)
			{
				if (this.systemIAccessible != null)
				{
					try
					{
						return this.WrapIAccessible(this.systemIAccessible.accFocus);
					}
					catch (COMException ex)
					{
						if (ex.ErrorCode != -2147352573)
						{
							throw ex;
						}
					}
				}
				return null;
			}
			int childCount = this.GetChildCount();
			for (int i = 0; i < childCount; i++)
			{
				AccessibleObject child = this.GetChild(i);
				if (child != null && (child.State & AccessibleStates.Focused) != AccessibleStates.None)
				{
					return child;
				}
			}
			if ((this.State & AccessibleStates.Focused) != AccessibleStates.None)
			{
				return this;
			}
			return null;
		}

		/// <summary>Gets an identifier for a Help topic identifier and the path to the Help file associated with this accessible object.</summary>
		/// <param name="fileName">On return, this property contains the path to the Help file associated with this accessible object.</param>
		/// <returns>An identifier for a Help topic, or -1 if there is no Help topic. On return, the <paramref name="fileName" /> parameter contains the path to the Help file associated with this accessible object.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The Help topic for the control cannot be retrieved.</exception>
		// Token: 0x06000792 RID: 1938 RVA: 0x00015D30 File Offset: 0x00013F30
		public virtual int GetHelpTopic(out string fileName)
		{
			if (this.systemIAccessible != null)
			{
				try
				{
					int num = this.systemIAccessible.get_accHelpTopic(out fileName, 0);
					if (fileName != null && fileName.Length > 0)
					{
						IntSecurity.DemandFileIO(FileIOPermissionAccess.PathDiscovery, fileName);
					}
					return num;
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
			}
			fileName = null;
			return -1;
		}

		/// <summary>Retrieves the currently selected child.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the currently selected child. This method returns the calling object if the object itself is selected. Returns <see langword="null" /> if is no child is currently selected and the object itself does not have focus.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The selected child cannot be retrieved.</exception>
		// Token: 0x06000793 RID: 1939 RVA: 0x00015D9C File Offset: 0x00013F9C
		public virtual AccessibleObject GetSelected()
		{
			if (this.GetChildCount() < 0)
			{
				if (this.systemIAccessible != null)
				{
					try
					{
						return this.WrapIAccessible(this.systemIAccessible.accSelection);
					}
					catch (COMException ex)
					{
						if (ex.ErrorCode != -2147352573)
						{
							throw ex;
						}
					}
				}
				return null;
			}
			int childCount = this.GetChildCount();
			for (int i = 0; i < childCount; i++)
			{
				AccessibleObject child = this.GetChild(i);
				if (child != null && (child.State & AccessibleStates.Selected) != AccessibleStates.None)
				{
					return child;
				}
			}
			if ((this.State & AccessibleStates.Selected) != AccessibleStates.None)
			{
				return this;
			}
			return null;
		}

		/// <summary>Retrieves the child object at the specified screen coordinates.</summary>
		/// <param name="x">The horizontal screen coordinate.</param>
		/// <param name="y">The vertical screen coordinate.</param>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the child object at the given screen coordinates. This method returns the calling object if the object itself is at the location specified. Returns <see langword="null" /> if no object is at the tested location.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The control cannot be hit tested.</exception>
		// Token: 0x06000794 RID: 1940 RVA: 0x00015E30 File Offset: 0x00014030
		public virtual AccessibleObject HitTest(int x, int y)
		{
			if (this.GetChildCount() >= 0)
			{
				int childCount = this.GetChildCount();
				for (int i = 0; i < childCount; i++)
				{
					AccessibleObject child = this.GetChild(i);
					if (child != null && child.Bounds.Contains(x, y))
					{
						return child;
					}
				}
				return this;
			}
			if (this.systemIAccessible != null)
			{
				try
				{
					return this.WrapIAccessible(this.systemIAccessible.accHitTest(x, y));
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
			}
			if (this.Bounds.Contains(x, y))
			{
				return this;
			}
			return null;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal virtual bool IsIAccessibleExSupported()
		{
			return false;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00015ED8 File Offset: 0x000140D8
		internal virtual bool IsPatternSupported(int patternId)
		{
			return AccessibilityImprovements.Level3 && patternId == 10000 && this.IsInvokePatternAvailable;
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x00015C90 File Offset: 0x00013E90
		internal virtual int[] RuntimeId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x00015EF1 File Offset: 0x000140F1
		internal virtual int ProviderOptions
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x00015C90 File Offset: 0x00013E90
		internal virtual UnsafeNativeMethods.IRawElementProviderSimple HostRawElementProvider
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00015EF5 File Offset: 0x000140F5
		internal virtual object GetPropertyValue(int propertyID)
		{
			if (AccessibilityImprovements.Level3 && propertyID == 30031)
			{
				return this.IsInvokePatternAvailable;
			}
			return null;
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x00015F14 File Offset: 0x00014114
		private bool IsInvokePatternAvailable
		{
			get
			{
				AccessibleRole role = this.Role;
				switch (role)
				{
				case AccessibleRole.Default:
				case AccessibleRole.None:
				case AccessibleRole.Sound:
				case AccessibleRole.Cursor:
				case AccessibleRole.Caret:
				case AccessibleRole.Alert:
				case AccessibleRole.Client:
				case AccessibleRole.Chart:
				case AccessibleRole.Dialog:
				case AccessibleRole.Border:
					return false;
				case AccessibleRole.TitleBar:
				case AccessibleRole.MenuBar:
				case AccessibleRole.ScrollBar:
				case AccessibleRole.Grip:
				case AccessibleRole.Window:
				case AccessibleRole.MenuPopup:
				case AccessibleRole.ToolTip:
				case AccessibleRole.Application:
				case AccessibleRole.Document:
				case AccessibleRole.Pane:
					goto IL_10A;
				case AccessibleRole.MenuItem:
					break;
				default:
					switch (role)
					{
					case AccessibleRole.Column:
					case AccessibleRole.Row:
					case AccessibleRole.HelpBalloon:
					case AccessibleRole.Character:
					case AccessibleRole.PageTab:
					case AccessibleRole.PropertyPage:
					case AccessibleRole.DropList:
					case AccessibleRole.Dial:
					case AccessibleRole.HotkeyField:
					case AccessibleRole.Diagram:
					case AccessibleRole.Animation:
					case AccessibleRole.Equation:
					case AccessibleRole.WhiteSpace:
					case AccessibleRole.IpAddress:
					case AccessibleRole.OutlineButton:
						return false;
					case AccessibleRole.Cell:
					case AccessibleRole.List:
					case AccessibleRole.ListItem:
					case AccessibleRole.Outline:
					case AccessibleRole.OutlineItem:
					case AccessibleRole.Indicator:
					case AccessibleRole.Graphic:
					case AccessibleRole.StaticText:
					case AccessibleRole.Text:
					case AccessibleRole.CheckButton:
					case AccessibleRole.RadioButton:
					case AccessibleRole.ComboBox:
					case AccessibleRole.ProgressBar:
					case AccessibleRole.Slider:
					case AccessibleRole.SpinButton:
					case AccessibleRole.PageTabList:
						goto IL_10A;
					case AccessibleRole.Link:
					case AccessibleRole.PushButton:
					case AccessibleRole.ButtonDropDown:
					case AccessibleRole.ButtonMenu:
					case AccessibleRole.ButtonDropDownGrid:
					case AccessibleRole.Clock:
					case AccessibleRole.SplitButton:
						break;
					default:
						goto IL_10A;
					}
					break;
				}
				return true;
				IL_10A:
				return !string.IsNullOrEmpty(this.DefaultAction);
			}
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal virtual int GetChildId()
		{
			return 0;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00015C90 File Offset: 0x00013E90
		internal virtual UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
		{
			return null;
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00015C90 File Offset: 0x00013E90
		internal virtual UnsafeNativeMethods.IRawElementProviderSimple[] GetEmbeddedFragmentRoots()
		{
			return null;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void SetFocus()
		{
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x00016039 File Offset: 0x00014239
		internal virtual Rectangle BoundingRectangle
		{
			get
			{
				return this.Bounds;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x00015C90 File Offset: 0x00013E90
		internal virtual UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00006A49 File Offset: 0x00004C49
		internal virtual UnsafeNativeMethods.IRawElementProviderFragment ElementProviderFromPoint(double x, double y)
		{
			return this;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00015C90 File Offset: 0x00013E90
		internal virtual UnsafeNativeMethods.IRawElementProviderFragment GetFocus()
		{
			return null;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void Expand()
		{
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void Collapse()
		{
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal virtual UnsafeNativeMethods.ExpandCollapseState ExpandCollapseState
		{
			get
			{
				return UnsafeNativeMethods.ExpandCollapseState.Collapsed;
			}
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void Toggle()
		{
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x00016041 File Offset: 0x00014241
		internal virtual UnsafeNativeMethods.ToggleState ToggleState
		{
			get
			{
				return UnsafeNativeMethods.ToggleState.ToggleState_Indeterminate;
			}
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00015C90 File Offset: 0x00013E90
		internal virtual UnsafeNativeMethods.IRawElementProviderSimple[] GetRowHeaders()
		{
			return null;
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00015C90 File Offset: 0x00013E90
		internal virtual UnsafeNativeMethods.IRawElementProviderSimple[] GetColumnHeaders()
		{
			return null;
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal virtual UnsafeNativeMethods.RowOrColumnMajor RowOrColumnMajor
		{
			get
			{
				return UnsafeNativeMethods.RowOrColumnMajor.RowOrColumnMajor_RowMajor;
			}
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00015C90 File Offset: 0x00013E90
		internal virtual UnsafeNativeMethods.IRawElementProviderSimple[] GetRowHeaderItems()
		{
			return null;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00015C90 File Offset: 0x00013E90
		internal virtual UnsafeNativeMethods.IRawElementProviderSimple[] GetColumnHeaderItems()
		{
			return null;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00015C90 File Offset: 0x00013E90
		internal virtual UnsafeNativeMethods.IRawElementProviderSimple GetItem(int row, int column)
		{
			return null;
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x00015C93 File Offset: 0x00013E93
		internal virtual int RowCount
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x00015C93 File Offset: 0x00013E93
		internal virtual int ColumnCount
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x00015C93 File Offset: 0x00013E93
		internal virtual int Row
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00015C93 File Offset: 0x00013E93
		internal virtual int Column
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x00012E4E File Offset: 0x0001104E
		internal virtual int RowSpan
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00012E4E File Offset: 0x0001104E
		internal virtual int ColumnSpan
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x00015C90 File Offset: 0x00013E90
		internal virtual UnsafeNativeMethods.IRawElementProviderSimple ContainingGrid
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00016044 File Offset: 0x00014244
		internal virtual void Invoke()
		{
			this.DoDefaultAction();
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x00015C90 File Offset: 0x00013E90
		internal virtual UnsafeNativeMethods.UiaCore.ITextRangeProvider DocumentRangeInternal
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00015C90 File Offset: 0x00013E90
		internal virtual UnsafeNativeMethods.UiaCore.ITextRangeProvider[] GetTextSelection()
		{
			return null;
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00015C90 File Offset: 0x00013E90
		internal virtual UnsafeNativeMethods.UiaCore.ITextRangeProvider[] GetTextVisibleRanges()
		{
			return null;
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00015C90 File Offset: 0x00013E90
		internal virtual UnsafeNativeMethods.UiaCore.ITextRangeProvider GetTextRangeFromChild(UnsafeNativeMethods.IRawElementProviderSimple childElement)
		{
			return null;
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00015C90 File Offset: 0x00013E90
		internal virtual UnsafeNativeMethods.UiaCore.ITextRangeProvider GetTextRangeFromPoint(Point screenLocation)
		{
			return null;
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal virtual UnsafeNativeMethods.UiaCore.SupportedTextSelection SupportedTextSelectionInternal
		{
			get
			{
				return UnsafeNativeMethods.UiaCore.SupportedTextSelection.None;
			}
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0001604C File Offset: 0x0001424C
		internal virtual UnsafeNativeMethods.UiaCore.ITextRangeProvider GetTextCaretRange(out UnsafeNativeMethods.BOOL isActive)
		{
			isActive = UnsafeNativeMethods.BOOL.FALSE;
			return null;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00015C90 File Offset: 0x00013E90
		internal virtual UnsafeNativeMethods.UiaCore.ITextRangeProvider GetRangeFromAnnotation(UnsafeNativeMethods.IRawElementProviderSimple annotationElement)
		{
			return null;
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00016052 File Offset: 0x00014252
		internal virtual void SetValue(string newValue)
		{
			this.Value = newValue;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00015C90 File Offset: 0x00013E90
		internal virtual UnsafeNativeMethods.IRawElementProviderSimple GetOverrideProviderForHwnd(IntPtr hwnd)
		{
			return null;
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void SetValue(double newValue)
		{
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x0001605B File Offset: 0x0001425B
		internal virtual double LargeChange
		{
			get
			{
				return double.NaN;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x0001605B File Offset: 0x0001425B
		internal virtual double Maximum
		{
			get
			{
				return double.NaN;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x0001605B File Offset: 0x0001425B
		internal virtual double Minimum
		{
			get
			{
				return double.NaN;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x0001605B File Offset: 0x0001425B
		internal virtual double SmallChange
		{
			get
			{
				return double.NaN;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x0001605B File Offset: 0x0001425B
		internal virtual double RangeValue
		{
			get
			{
				return double.NaN;
			}
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00015C90 File Offset: 0x00013E90
		internal virtual UnsafeNativeMethods.IRawElementProviderSimple[] GetSelection()
		{
			return null;
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal virtual bool CanSelectMultiple
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal virtual bool IsSelectionRequired
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void SelectItem()
		{
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void AddToSelection()
		{
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void RemoveFromSelection()
		{
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal virtual bool IsItemSelected
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x00015C90 File Offset: 0x00013E90
		internal virtual UnsafeNativeMethods.IRawElementProviderSimple ItemSelectionContainer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void SetParent(AccessibleObject parent)
		{
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void SetDetachableChild(AccessibleObject child)
		{
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00016068 File Offset: 0x00014268
		int UnsafeNativeMethods.IServiceProvider.QueryService(ref Guid service, ref Guid riid, out IntPtr ppvObject)
		{
			int num = -2147467262;
			ppvObject = IntPtr.Zero;
			if (this.IsIAccessibleExSupported() && service.Equals(UnsafeNativeMethods.guid_IAccessibleEx) && riid.Equals(UnsafeNativeMethods.guid_IAccessibleEx))
			{
				ppvObject = Marshal.GetComInterfaceForObject(this, typeof(UnsafeNativeMethods.IAccessibleEx));
				num = 0;
			}
			return num;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x000160B9 File Offset: 0x000142B9
		object UnsafeNativeMethods.IAccessibleEx.GetObjectForChild(int childId)
		{
			return this.GetObjectForChild(childId);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00015C90 File Offset: 0x00013E90
		internal virtual object GetObjectForChild(int childId)
		{
			return null;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x000160C2 File Offset: 0x000142C2
		int UnsafeNativeMethods.IAccessibleEx.GetIAccessiblePair(out object ppAcc, out int pidChild)
		{
			ppAcc = null;
			pidChild = 0;
			return -2147467261;
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x000160CF File Offset: 0x000142CF
		int[] UnsafeNativeMethods.IAccessibleEx.GetRuntimeId()
		{
			return this.RuntimeId;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x000160D7 File Offset: 0x000142D7
		int UnsafeNativeMethods.IAccessibleEx.ConvertReturnedElement(object pIn, out object ppRetValOut)
		{
			ppRetValOut = null;
			return -2147467263;
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x000160E1 File Offset: 0x000142E1
		UnsafeNativeMethods.ProviderOptions UnsafeNativeMethods.IRawElementProviderSimple.ProviderOptions
		{
			get
			{
				return (UnsafeNativeMethods.ProviderOptions)this.ProviderOptions;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x000160E9 File Offset: 0x000142E9
		UnsafeNativeMethods.IRawElementProviderSimple UnsafeNativeMethods.IRawElementProviderSimple.HostRawElementProvider
		{
			get
			{
				return this.HostRawElementProvider;
			}
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x000160F1 File Offset: 0x000142F1
		object UnsafeNativeMethods.IRawElementProviderSimple.GetPatternProvider(int patternId)
		{
			if (this.IsPatternSupported(patternId))
			{
				return this;
			}
			return null;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x000160FF File Offset: 0x000142FF
		object UnsafeNativeMethods.IRawElementProviderSimple.GetPropertyValue(int propertyID)
		{
			return this.GetPropertyValue(propertyID);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00016108 File Offset: 0x00014308
		object UnsafeNativeMethods.IRawElementProviderFragment.Navigate(UnsafeNativeMethods.NavigateDirection direction)
		{
			return this.FragmentNavigate(direction);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x000160CF File Offset: 0x000142CF
		int[] UnsafeNativeMethods.IRawElementProviderFragment.GetRuntimeId()
		{
			return this.RuntimeId;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00016114 File Offset: 0x00014314
		object[] UnsafeNativeMethods.IRawElementProviderFragment.GetEmbeddedFragmentRoots()
		{
			return this.GetEmbeddedFragmentRoots();
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00016129 File Offset: 0x00014329
		void UnsafeNativeMethods.IRawElementProviderFragment.SetFocus()
		{
			this.SetFocus();
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x00016131 File Offset: 0x00014331
		NativeMethods.UiaRect UnsafeNativeMethods.IRawElementProviderFragment.BoundingRectangle
		{
			get
			{
				return new NativeMethods.UiaRect(this.BoundingRectangle);
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x0001613E File Offset: 0x0001433E
		UnsafeNativeMethods.IRawElementProviderFragmentRoot UnsafeNativeMethods.IRawElementProviderFragment.FragmentRoot
		{
			get
			{
				return this.FragmentRoot;
			}
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00016146 File Offset: 0x00014346
		object UnsafeNativeMethods.IRawElementProviderFragmentRoot.ElementProviderFromPoint(double x, double y)
		{
			return this.ElementProviderFromPoint(x, y);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00016150 File Offset: 0x00014350
		object UnsafeNativeMethods.IRawElementProviderFragmentRoot.GetFocus()
		{
			return this.GetFocus();
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x00016158 File Offset: 0x00014358
		string UnsafeNativeMethods.ILegacyIAccessibleProvider.DefaultAction
		{
			get
			{
				return this.DefaultAction;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x00016160 File Offset: 0x00014360
		string UnsafeNativeMethods.ILegacyIAccessibleProvider.Description
		{
			get
			{
				return this.Description;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x00016168 File Offset: 0x00014368
		string UnsafeNativeMethods.ILegacyIAccessibleProvider.Help
		{
			get
			{
				return this.Help;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x00016170 File Offset: 0x00014370
		string UnsafeNativeMethods.ILegacyIAccessibleProvider.KeyboardShortcut
		{
			get
			{
				return this.KeyboardShortcut;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x00016178 File Offset: 0x00014378
		string UnsafeNativeMethods.ILegacyIAccessibleProvider.Name
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x00016180 File Offset: 0x00014380
		uint UnsafeNativeMethods.ILegacyIAccessibleProvider.Role
		{
			get
			{
				return (uint)this.Role;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x00016188 File Offset: 0x00014388
		uint UnsafeNativeMethods.ILegacyIAccessibleProvider.State
		{
			get
			{
				return (uint)this.State;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x00016190 File Offset: 0x00014390
		string UnsafeNativeMethods.ILegacyIAccessibleProvider.Value
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x00016198 File Offset: 0x00014398
		int UnsafeNativeMethods.ILegacyIAccessibleProvider.ChildId
		{
			get
			{
				return this.GetChildId();
			}
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00016044 File Offset: 0x00014244
		void UnsafeNativeMethods.ILegacyIAccessibleProvider.DoDefaultAction()
		{
			this.DoDefaultAction();
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x000161A0 File Offset: 0x000143A0
		IAccessible UnsafeNativeMethods.ILegacyIAccessibleProvider.GetIAccessible()
		{
			return this.AsIAccessible(this);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x000161AC File Offset: 0x000143AC
		object[] UnsafeNativeMethods.ILegacyIAccessibleProvider.GetSelection()
		{
			return new UnsafeNativeMethods.IRawElementProviderSimple[] { this.GetSelected() };
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x000161CA File Offset: 0x000143CA
		void UnsafeNativeMethods.ILegacyIAccessibleProvider.Select(int flagsSelect)
		{
			this.Select((AccessibleSelection)flagsSelect);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x000161D3 File Offset: 0x000143D3
		void UnsafeNativeMethods.ILegacyIAccessibleProvider.SetValue(string szValue)
		{
			this.SetValue(szValue);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x000161DC File Offset: 0x000143DC
		void UnsafeNativeMethods.IExpandCollapseProvider.Expand()
		{
			this.Expand();
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x000161E4 File Offset: 0x000143E4
		void UnsafeNativeMethods.IExpandCollapseProvider.Collapse()
		{
			this.Collapse();
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x000161EC File Offset: 0x000143EC
		UnsafeNativeMethods.ExpandCollapseState UnsafeNativeMethods.IExpandCollapseProvider.ExpandCollapseState
		{
			get
			{
				return this.ExpandCollapseState;
			}
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x000161F4 File Offset: 0x000143F4
		void UnsafeNativeMethods.IInvokeProvider.Invoke()
		{
			this.Invoke();
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x000161FC File Offset: 0x000143FC
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider.DocumentRange
		{
			get
			{
				return this.DocumentRangeInternal;
			}
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00016204 File Offset: 0x00014404
		UnsafeNativeMethods.UiaCore.ITextRangeProvider[] UnsafeNativeMethods.UiaCore.ITextProvider.GetSelection()
		{
			return this.GetTextSelection();
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0001620C File Offset: 0x0001440C
		UnsafeNativeMethods.UiaCore.ITextRangeProvider[] UnsafeNativeMethods.UiaCore.ITextProvider.GetVisibleRanges()
		{
			return this.GetTextVisibleRanges();
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00016214 File Offset: 0x00014414
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider.RangeFromChild(UnsafeNativeMethods.IRawElementProviderSimple childElement)
		{
			return this.GetTextRangeFromChild(childElement);
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0001621D File Offset: 0x0001441D
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider.RangeFromPoint(Point screenLocation)
		{
			return this.GetTextRangeFromPoint(screenLocation);
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x00016226 File Offset: 0x00014426
		UnsafeNativeMethods.UiaCore.SupportedTextSelection UnsafeNativeMethods.UiaCore.ITextProvider.SupportedTextSelection
		{
			get
			{
				return this.SupportedTextSelectionInternal;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x000161FC File Offset: 0x000143FC
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider2.DocumentRange
		{
			get
			{
				return this.DocumentRangeInternal;
			}
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00016204 File Offset: 0x00014404
		UnsafeNativeMethods.UiaCore.ITextRangeProvider[] UnsafeNativeMethods.UiaCore.ITextProvider2.GetSelection()
		{
			return this.GetTextSelection();
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0001620C File Offset: 0x0001440C
		UnsafeNativeMethods.UiaCore.ITextRangeProvider[] UnsafeNativeMethods.UiaCore.ITextProvider2.GetVisibleRanges()
		{
			return this.GetTextVisibleRanges();
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00016214 File Offset: 0x00014414
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider2.RangeFromChild(UnsafeNativeMethods.IRawElementProviderSimple childElement)
		{
			return this.GetTextRangeFromChild(childElement);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0001621D File Offset: 0x0001441D
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider2.RangeFromPoint(Point screenLocation)
		{
			return this.GetTextRangeFromPoint(screenLocation);
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x00016226 File Offset: 0x00014426
		UnsafeNativeMethods.UiaCore.SupportedTextSelection UnsafeNativeMethods.UiaCore.ITextProvider2.SupportedTextSelection
		{
			get
			{
				return this.SupportedTextSelectionInternal;
			}
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0001622E File Offset: 0x0001442E
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider2.GetCaretRange(out UnsafeNativeMethods.BOOL isActive)
		{
			return this.GetTextCaretRange(out isActive);
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00016237 File Offset: 0x00014437
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider2.RangeFromAnnotation(UnsafeNativeMethods.IRawElementProviderSimple annotationElement)
		{
			return this.GetRangeFromAnnotation(annotationElement);
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x00016240 File Offset: 0x00014440
		bool UnsafeNativeMethods.IValueProvider.IsReadOnly
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x00016190 File Offset: 0x00014390
		string UnsafeNativeMethods.IValueProvider.Value
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x000161D3 File Offset: 0x000143D3
		void UnsafeNativeMethods.IValueProvider.SetValue(string newValue)
		{
			this.SetValue(newValue);
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00016248 File Offset: 0x00014448
		void UnsafeNativeMethods.IToggleProvider.Toggle()
		{
			this.Toggle();
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x00016250 File Offset: 0x00014450
		UnsafeNativeMethods.ToggleState UnsafeNativeMethods.IToggleProvider.ToggleState
		{
			get
			{
				return this.ToggleState;
			}
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00016258 File Offset: 0x00014458
		object[] UnsafeNativeMethods.ITableProvider.GetRowHeaders()
		{
			return this.GetRowHeaders();
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00016270 File Offset: 0x00014470
		object[] UnsafeNativeMethods.ITableProvider.GetColumnHeaders()
		{
			return this.GetColumnHeaders();
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x00016285 File Offset: 0x00014485
		UnsafeNativeMethods.RowOrColumnMajor UnsafeNativeMethods.ITableProvider.RowOrColumnMajor
		{
			get
			{
				return this.RowOrColumnMajor;
			}
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00016290 File Offset: 0x00014490
		object[] UnsafeNativeMethods.ITableItemProvider.GetRowHeaderItems()
		{
			return this.GetRowHeaderItems();
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x000162A8 File Offset: 0x000144A8
		object[] UnsafeNativeMethods.ITableItemProvider.GetColumnHeaderItems()
		{
			return this.GetColumnHeaderItems();
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x000162BD File Offset: 0x000144BD
		object UnsafeNativeMethods.IGridProvider.GetItem(int row, int column)
		{
			return this.GetItem(row, column);
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x000162C7 File Offset: 0x000144C7
		int UnsafeNativeMethods.IGridProvider.RowCount
		{
			get
			{
				return this.RowCount;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x000162CF File Offset: 0x000144CF
		int UnsafeNativeMethods.IGridProvider.ColumnCount
		{
			get
			{
				return this.ColumnCount;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x000162D7 File Offset: 0x000144D7
		int UnsafeNativeMethods.IGridItemProvider.Row
		{
			get
			{
				return this.Row;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x000162DF File Offset: 0x000144DF
		int UnsafeNativeMethods.IGridItemProvider.Column
		{
			get
			{
				return this.Column;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x000162E7 File Offset: 0x000144E7
		int UnsafeNativeMethods.IGridItemProvider.RowSpan
		{
			get
			{
				return this.RowSpan;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x000162EF File Offset: 0x000144EF
		int UnsafeNativeMethods.IGridItemProvider.ColumnSpan
		{
			get
			{
				return this.ColumnSpan;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x000162F7 File Offset: 0x000144F7
		UnsafeNativeMethods.IRawElementProviderSimple UnsafeNativeMethods.IGridItemProvider.ContainingGrid
		{
			get
			{
				return this.ContainingGrid;
			}
		}

		/// <summary>Performs the specified object's default action. Not all objects have a default action. For a description of this member, see <see cref="M:Accessibility.IAccessible.accDoDefaultAction(System.Object)" />.</summary>
		/// <param name="childID">The child ID in the <see cref="T:Accessibility.IAccessible" /> interface/child ID pair that represents the accessible object.</param>
		// Token: 0x06000816 RID: 2070 RVA: 0x00016300 File Offset: 0x00014500
		void IAccessible.accDoDefaultAction(object childID)
		{
			IntSecurity.UnmanagedCode.Demand();
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					this.DoDefaultAction();
					return;
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					accessibleChild.DoDefaultAction();
					return;
				}
			}
			if (this.systemIAccessible != null)
			{
				try
				{
					this.systemIAccessible.accDoDefaultAction(childID);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
			}
		}

		/// <summary>Gets the child object at the specified screen coordinates. For a description of this member, see <see cref="M:Accessibility.IAccessible.accHitTest(System.Int32,System.Int32)" />.</summary>
		/// <param name="xLeft">The horizontal coordinate.</param>
		/// <param name="yTop">The vertical coordinate.</param>
		/// <returns>The accessible object at the point specified by <paramref name="xLeft" /> and <paramref name="yTop" />.</returns>
		// Token: 0x06000817 RID: 2071 RVA: 0x00016388 File Offset: 0x00014588
		object IAccessible.accHitTest(int xLeft, int yTop)
		{
			if (this.IsClientObject)
			{
				AccessibleObject accessibleObject = this.HitTest(xLeft, yTop);
				if (accessibleObject != null)
				{
					return this.AsVariant(accessibleObject);
				}
			}
			if (this.systemIAccessible != null)
			{
				try
				{
					return this.systemIAccessible.accHitTest(xLeft, yTop);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
			}
			return null;
		}

		/// <summary>Gets the object's current screen location. For a description of this member, see <see cref="M:Accessibility.IAccessible.accLocation(System.Int32@,System.Int32@,System.Int32@,System.Int32@,System.Object)" />.</summary>
		/// <param name="pxLeft">When this method returns, contains the x-coordinate of the object's left edge. This parameter is passed uninitialized.</param>
		/// <param name="pyTop">When this method returns, contains the y-coordinate of the object's top edge. This parameter is passed uninitialized.</param>
		/// <param name="pcxWidth">When this method returns, contains the width of the object. This parameter is passed uninitialized.</param>
		/// <param name="pcyHeight">When this method returns, contains the height of the object. This parameter is passed uninitialized.</param>
		/// <param name="childID">The ID number of the accessible object. This parameter is 0 to get the location of the object, or a child ID to get the location of one of the object's child objects.</param>
		// Token: 0x06000818 RID: 2072 RVA: 0x000163F0 File Offset: 0x000145F0
		void IAccessible.accLocation(out int pxLeft, out int pyTop, out int pcxWidth, out int pcyHeight, object childID)
		{
			pxLeft = 0;
			pyTop = 0;
			pcxWidth = 0;
			pcyHeight = 0;
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					Rectangle bounds = this.Bounds;
					pxLeft = bounds.X;
					pyTop = bounds.Y;
					pcxWidth = bounds.Width;
					pcyHeight = bounds.Height;
					return;
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					Rectangle bounds2 = accessibleChild.Bounds;
					pxLeft = bounds2.X;
					pyTop = bounds2.Y;
					pcxWidth = bounds2.Width;
					pcyHeight = bounds2.Height;
					return;
				}
			}
			if (this.systemIAccessible != null)
			{
				try
				{
					this.systemIAccessible.accLocation(out pxLeft, out pyTop, out pcxWidth, out pcyHeight, childID);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
				return;
			}
		}

		/// <summary>Navigates to an accessible object relative to the current object. For a description of this member, see <see cref="M:Accessibility.IAccessible.accNavigate(System.Int32,System.Object)" />.</summary>
		/// <param name="navDir">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> enumerations that specifies the direction to navigate.</param>
		/// <param name="childID">The ID number of the accessible object. This parameter is 0 to start from the object, or a child ID to start from one of the object's child objects.</param>
		/// <returns>The accessible object positioned at the value specified by <paramref name="navDir" />.</returns>
		// Token: 0x06000819 RID: 2073 RVA: 0x000164D0 File Offset: 0x000146D0
		object IAccessible.accNavigate(int navDir, object childID)
		{
			IntSecurity.UnmanagedCode.Demand();
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					AccessibleObject accessibleObject = this.Navigate((AccessibleNavigation)navDir);
					if (accessibleObject != null)
					{
						return this.AsVariant(accessibleObject);
					}
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					return this.AsVariant(accessibleChild.Navigate((AccessibleNavigation)navDir));
				}
			}
			if (this.systemIAccessible != null)
			{
				try
				{
					object obj;
					if (!this.SysNavigate(navDir, childID, out obj))
					{
						obj = this.systemIAccessible.accNavigate(navDir, childID);
					}
					return obj;
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
			}
			return null;
		}

		/// <summary>Modifies the selection or moves the keyboard focus of the accessible object. For a description of this member, see <see cref="M:Accessibility.IAccessible.accSelect(System.Int32,System.Object)" />.</summary>
		/// <param name="flagsSelect">A bitwise combination of the <see cref="T:System.Windows.Forms.AccessibleSelection" /> values.</param>
		/// <param name="childID">The ID number of the accessible object on which to change the selection. This parameter is 0 to select the object, or a child ID to select one of the object's child objects.</param>
		// Token: 0x0600081A RID: 2074 RVA: 0x00016580 File Offset: 0x00014780
		void IAccessible.accSelect(int flagsSelect, object childID)
		{
			IntSecurity.UnmanagedCode.Demand();
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					this.Select((AccessibleSelection)flagsSelect);
					return;
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					accessibleChild.Select((AccessibleSelection)flagsSelect);
					return;
				}
			}
			if (this.systemIAccessible != null)
			{
				try
				{
					this.systemIAccessible.accSelect(flagsSelect, childID);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
				return;
			}
		}

		/// <summary>Performs the default action associated with this accessible object.</summary>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The default action for the control cannot be performed.</exception>
		// Token: 0x0600081B RID: 2075 RVA: 0x0001660C File Offset: 0x0001480C
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public virtual void DoDefaultAction()
		{
			if (this.systemIAccessible != null)
			{
				try
				{
					this.systemIAccessible.accDoDefaultAction(0);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
				return;
			}
		}

		/// <summary>Retrieves a specified child object.</summary>
		/// <param name="childID">The ID number of the child object to retrieve.</param>
		/// <returns>The specified child object, if it exists, or <see langword="null" /> if it does not exist.</returns>
		// Token: 0x0600081C RID: 2076 RVA: 0x00016658 File Offset: 0x00014858
		object IAccessible.get_accChild(object childID)
		{
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					return this.AsIAccessible(this);
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					if (accessibleChild == this)
					{
						return null;
					}
					return this.AsIAccessible(accessibleChild);
				}
			}
			if (this.systemIAccessible != null)
			{
				return this.systemIAccessible.get_accChild(childID);
			}
			return null;
		}

		/// <summary>Gets the number of child interfaces that belong to this object. For a description of this member, see <see cref="P:Accessibility.IAccessible.accChildCount" />.</summary>
		/// <returns>The number of child accessible objects that belong to this object. If the object has no child objects, this value is 0.</returns>
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x000166BC File Offset: 0x000148BC
		int IAccessible.accChildCount
		{
			get
			{
				int num = -1;
				if (this.IsClientObject)
				{
					num = this.GetChildCount();
				}
				if (num == -1)
				{
					if (this.systemIAccessible != null)
					{
						num = this.systemIAccessible.accChildCount;
					}
					else
					{
						num = 0;
					}
				}
				return num;
			}
		}

		/// <summary>Returns a string that indicates the specified object's default action.</summary>
		/// <param name="childID">The ID number of the accessible object for which to get the default action. This parameter is 0 to select the object, or a child ID to select one of the object's child objects.</param>
		/// <returns>A string that indicates the default action of <paramref name="childID" />, or <see langword="name" /> if <paramref name="childID" /> has no default action.</returns>
		// Token: 0x0600081E RID: 2078 RVA: 0x000166F8 File Offset: 0x000148F8
		string IAccessible.get_accDefaultAction(object childID)
		{
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					return this.DefaultAction;
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					return accessibleChild.DefaultAction;
				}
			}
			if (this.systemIAccessible != null)
			{
				try
				{
					return this.systemIAccessible.get_accDefaultAction(childID);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
			}
			return null;
		}

		/// <summary>Returns a string that describes the visual appearance of the specified accessible object.</summary>
		/// <param name="childID">The ID number of the accessible object for which to get a description. This parameter is 0 to select the object, or a child ID to select one of the object's child objects.</param>
		/// <returns>A localized string that describes the specified accessible object.</returns>
		// Token: 0x0600081F RID: 2079 RVA: 0x00016778 File Offset: 0x00014978
		string IAccessible.get_accDescription(object childID)
		{
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					return this.Description;
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					return accessibleChild.Description;
				}
			}
			if (this.systemIAccessible != null)
			{
				try
				{
					return this.systemIAccessible.get_accDescription(childID);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
			}
			return null;
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x000167F8 File Offset: 0x000149F8
		private AccessibleObject GetAccessibleChild(object childID)
		{
			if (!childID.Equals(0))
			{
				int num = (int)childID - 1;
				if (num >= 0 && num < this.GetChildCount())
				{
					return this.GetChild(num);
				}
			}
			return null;
		}

		/// <summary>Gets the object that has the keyboard focus. For a description of this member, see <see cref="P:Accessibility.IAccessible.accFocus" />.</summary>
		/// <returns>The object that has keyboard focus.</returns>
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x00016834 File Offset: 0x00014A34
		object IAccessible.accFocus
		{
			get
			{
				if (this.IsClientObject)
				{
					AccessibleObject focused = this.GetFocused();
					if (focused != null)
					{
						return this.AsVariant(focused);
					}
				}
				if (this.systemIAccessible != null)
				{
					try
					{
						return this.systemIAccessible.accFocus;
					}
					catch (COMException ex)
					{
						if (ex.ErrorCode != -2147352573)
						{
							throw ex;
						}
					}
				}
				return null;
			}
		}

		/// <summary>Retrieves the full path of the WinHelp file that is associated with the specified accessible object.</summary>
		/// <param name="childID">The ID number of the accessible object for which to get help information. This parameter is 0 to select the object, or a child ID to select one of the object's child objects.</param>
		/// <returns>The full path of the WinHelp file that is associated with <paramref name="childID" />.</returns>
		// Token: 0x06000822 RID: 2082 RVA: 0x00016898 File Offset: 0x00014A98
		string IAccessible.get_accHelp(object childID)
		{
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					return this.Help;
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					return accessibleChild.Help;
				}
			}
			if (this.systemIAccessible != null)
			{
				try
				{
					return this.systemIAccessible.get_accHelp(childID);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
			}
			return null;
		}

		/// <summary>Retrives the full path of a WinHelp file that is associated with the specified object along with the identifier of a specific topic in the file.</summary>
		/// <param name="pszHelpFile">When the method returns, the full path of the WinHelp file associated with the specified object.</param>
		/// <param name="childID">The ID number of the accessible object for which to retrieve a help topic. This parameter is 0 to select the object, or a child ID to select one of the object's child objects..</param>
		/// <returns>The identifier of a specific topic in <paramref name="pszHelpFile" />.</returns>
		// Token: 0x06000823 RID: 2083 RVA: 0x00016918 File Offset: 0x00014B18
		int IAccessible.get_accHelpTopic(out string pszHelpFile, object childID)
		{
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					return this.GetHelpTopic(out pszHelpFile);
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					return accessibleChild.GetHelpTopic(out pszHelpFile);
				}
			}
			if (this.systemIAccessible != null)
			{
				try
				{
					return this.systemIAccessible.get_accHelpTopic(out pszHelpFile, childID);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
			}
			pszHelpFile = null;
			return -1;
		}

		/// <summary>Retrieves the specified object's keyboard shortcut or access key.</summary>
		/// <param name="childID">The ID number of the accessible object for which to get a keyboard shortcut. This parameter is 0 to select the object, or a child ID to select one of the object's child objects.</param>
		/// <returns>A localized string that identifies the keyboard shortcut, or <see langword="null" /> if no keyboard shortcut is associated with the specified object.</returns>
		// Token: 0x06000824 RID: 2084 RVA: 0x000169A0 File Offset: 0x00014BA0
		string IAccessible.get_accKeyboardShortcut(object childID)
		{
			return this.get_accKeyboardShortcutInternal(childID);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x000169AC File Offset: 0x00014BAC
		internal virtual string get_accKeyboardShortcutInternal(object childID)
		{
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					return this.KeyboardShortcut;
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					return accessibleChild.KeyboardShortcut;
				}
			}
			if (this.systemIAccessible != null)
			{
				try
				{
					return this.systemIAccessible.get_accKeyboardShortcut(childID);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
			}
			return null;
		}

		/// <summary>Retrieves the name of the specified object.</summary>
		/// <param name="childID">The ID number of the accessible object whose name is to be retrieved. This parameter is 0 to select the object, or a child ID to select one of the object's child objects.</param>
		/// <returns>The name of the specified object.</returns>
		// Token: 0x06000826 RID: 2086 RVA: 0x00016A2C File Offset: 0x00014C2C
		string IAccessible.get_accName(object childID)
		{
			return this.get_accNameInternal(childID);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00016A38 File Offset: 0x00014C38
		internal virtual string get_accNameInternal(object childID)
		{
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					return this.Name;
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					return accessibleChild.Name;
				}
			}
			if (this.systemIAccessible != null)
			{
				string text = this.systemIAccessible.get_accName(childID);
				if (this.IsClientObject && (text == null || text.Length == 0))
				{
					text = this.Name;
				}
				return text;
			}
			return null;
		}

		/// <summary>Gets the parent accessible object of this object. For a description of this member, see <see cref="P:Accessibility.IAccessible.accParent" />.</summary>
		/// <returns>An <see cref="T:Accessibility.IAccessible" /> that represents the parent of the accessible object, or <see langword="null" /> if there is no parent object.</returns>
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x00016AB0 File Offset: 0x00014CB0
		object IAccessible.accParent
		{
			get
			{
				IntSecurity.UnmanagedCode.Demand();
				AccessibleObject accessibleObject = this.Parent;
				if (accessibleObject != null && accessibleObject == this)
				{
					accessibleObject = null;
				}
				return this.AsIAccessible(accessibleObject);
			}
		}

		/// <summary>Retrieves information that describes the role of the specified object.</summary>
		/// <param name="childID">The ID number of the accessible object for which to get role information. This parameter is 0 to select the object, or a child ID to select one of the object's child objects.</param>
		/// <returns>TAn object that provides role information about the specified accessible object.</returns>
		// Token: 0x06000829 RID: 2089 RVA: 0x00016AE0 File Offset: 0x00014CE0
		object IAccessible.get_accRole(object childID)
		{
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					return (int)this.Role;
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					return (int)accessibleChild.Role;
				}
			}
			if (this.systemIAccessible != null)
			{
				return this.systemIAccessible.get_accRole(childID);
			}
			return null;
		}

		/// <summary>Gets the selected child objects of an accessible object. For a description of this member, see <see cref="P:Accessibility.IAccessible.accSelection" />.</summary>
		/// <returns>The selected child objects of an accessible object.</returns>
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x00016B44 File Offset: 0x00014D44
		object IAccessible.accSelection
		{
			get
			{
				if (this.IsClientObject)
				{
					AccessibleObject selected = this.GetSelected();
					if (selected != null)
					{
						return this.AsVariant(selected);
					}
				}
				if (this.systemIAccessible != null)
				{
					try
					{
						return this.systemIAccessible.accSelection;
					}
					catch (COMException ex)
					{
						if (ex.ErrorCode != -2147352573)
						{
							throw ex;
						}
					}
				}
				return null;
			}
		}

		/// <summary>Retrieves the current state of the specified accessible object.</summary>
		/// <param name="childID">The ID number of the accessible object for which to get state information. This parameter is 0 to select the object, or a child ID to select one of the object's child objects.</param>
		/// <returns>An object that describes the current state of the specified accessible object.</returns>
		// Token: 0x0600082B RID: 2091 RVA: 0x00016BA8 File Offset: 0x00014DA8
		object IAccessible.get_accState(object childID)
		{
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					return (int)this.State;
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					return (int)accessibleChild.State;
				}
			}
			if (this.systemIAccessible != null)
			{
				return this.systemIAccessible.get_accState(childID);
			}
			return null;
		}

		/// <summary>Retrieves the value of the specified accessible object. Not all objects have a value.</summary>
		/// <param name="childID">The ID number of the accessible object whose value is to be retrieved. This parameter is 0 to select the object, or a child ID to select one of the object's child objects.</param>
		/// <returns>The value of <paramref name="childID" />, or <see langword="null" /> if the object has no value.</returns>
		// Token: 0x0600082C RID: 2092 RVA: 0x00016C0C File Offset: 0x00014E0C
		string IAccessible.get_accValue(object childID)
		{
			IntSecurity.UnmanagedCode.Demand();
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					return this.Value;
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					return accessibleChild.Value;
				}
			}
			if (this.systemIAccessible != null)
			{
				try
				{
					return this.systemIAccessible.get_accValue(childID);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
			}
			return null;
		}

		/// <summary>Assigns a new name to the specified accessible object.</summary>
		/// <param name="childID">The ID number of the accessible object to which to assign a new name. This parameter is 0 to select the object, or a child ID to select one of the object's child objects.</param>
		/// <param name="newName">The new name to assign to <paramref name="childID" />.</param>
		// Token: 0x0600082D RID: 2093 RVA: 0x00016C98 File Offset: 0x00014E98
		void IAccessible.set_accName(object childID, string newName)
		{
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					this.Name = newName;
					return;
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					accessibleChild.Name = newName;
					return;
				}
			}
			if (this.systemIAccessible != null)
			{
				this.systemIAccessible.set_accName(childID, newName);
				return;
			}
		}

		/// <summary>Assigns a new value to the specified accessible object.</summary>
		/// <param name="childID">The ID number of the accessible object to which to assign a new value. This parameter is 0 to select the object, or a child ID to select one of the object's child objects.</param>
		/// <param name="newValue">The new value to assign to the specified accessible object.</param>
		// Token: 0x0600082E RID: 2094 RVA: 0x00016CF4 File Offset: 0x00014EF4
		void IAccessible.set_accValue(object childID, string newValue)
		{
			IntSecurity.UnmanagedCode.Demand();
			if (this.IsClientObject)
			{
				this.ValidateChildID(ref childID);
				if (childID.Equals(0))
				{
					this.Value = newValue;
					return;
				}
				AccessibleObject accessibleChild = this.GetAccessibleChild(childID);
				if (accessibleChild != null)
				{
					accessibleChild.Value = newValue;
					return;
				}
			}
			if (this.systemIAccessible != null)
			{
				try
				{
					this.systemIAccessible.set_accValue(childID, newValue);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
				return;
			}
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00016D80 File Offset: 0x00014F80
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		int UnsafeNativeMethods.IOleWindow.GetWindow(out IntPtr hwnd)
		{
			if (this.systemIOleWindow != null)
			{
				return this.systemIOleWindow.GetWindow(out hwnd);
			}
			AccessibleObject parent = this.Parent;
			if (parent != null)
			{
				return ((UnsafeNativeMethods.IOleWindow)parent).GetWindow(out hwnd);
			}
			hwnd = IntPtr.Zero;
			return -2147467259;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00016DC0 File Offset: 0x00014FC0
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		void UnsafeNativeMethods.IOleWindow.ContextSensitiveHelp(int fEnterMode)
		{
			if (this.systemIOleWindow != null)
			{
				this.systemIOleWindow.ContextSensitiveHelp(fEnterMode);
				return;
			}
			AccessibleObject parent = this.Parent;
			if (parent != null)
			{
				((UnsafeNativeMethods.IOleWindow)parent).ContextSensitiveHelp(fEnterMode);
				return;
			}
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00016DF4 File Offset: 0x00014FF4
		void UnsafeNativeMethods.IEnumVariant.Clone(UnsafeNativeMethods.IEnumVariant[] v)
		{
			this.EnumVariant.Clone(v);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00016E02 File Offset: 0x00015002
		int UnsafeNativeMethods.IEnumVariant.Next(int n, IntPtr rgvar, int[] ns)
		{
			return this.EnumVariant.Next(n, rgvar, ns);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00016E12 File Offset: 0x00015012
		void UnsafeNativeMethods.IEnumVariant.Reset()
		{
			this.EnumVariant.Reset();
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00016E1F File Offset: 0x0001501F
		void UnsafeNativeMethods.IEnumVariant.Skip(int n)
		{
			this.EnumVariant.Skip(n);
		}

		/// <summary>Navigates to another accessible object.</summary>
		/// <param name="navdir">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> values.</param>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents one of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> values.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The navigation attempt fails.</exception>
		// Token: 0x06000835 RID: 2101 RVA: 0x00016E30 File Offset: 0x00015030
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public virtual AccessibleObject Navigate(AccessibleNavigation navdir)
		{
			if (this.GetChildCount() >= 0)
			{
				switch (navdir)
				{
				case AccessibleNavigation.Up:
				case AccessibleNavigation.Left:
				case AccessibleNavigation.Previous:
					if (this.Parent.GetChildCount() > 0)
					{
						return null;
					}
					break;
				case AccessibleNavigation.Down:
				case AccessibleNavigation.Right:
				case AccessibleNavigation.Next:
					if (this.Parent.GetChildCount() > 0)
					{
						return null;
					}
					break;
				case AccessibleNavigation.FirstChild:
					return this.GetChild(0);
				case AccessibleNavigation.LastChild:
					return this.GetChild(this.GetChildCount() - 1);
				}
			}
			if (this.systemIAccessible != null)
			{
				try
				{
					object obj = null;
					if (!this.SysNavigate((int)navdir, 0, out obj))
					{
						obj = this.systemIAccessible.accNavigate((int)navdir, 0);
					}
					return this.WrapIAccessible(obj);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
			}
			return null;
		}

		/// <summary>Modifies the selection or moves the keyboard focus of the accessible object.</summary>
		/// <param name="flags">One of the <see cref="T:System.Windows.Forms.AccessibleSelection" /> values.</param>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The selection cannot be performed.</exception>
		// Token: 0x06000836 RID: 2102 RVA: 0x00016F04 File Offset: 0x00015104
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public virtual void Select(AccessibleSelection flags)
		{
			if (this.systemIAccessible != null)
			{
				try
				{
					this.systemIAccessible.accSelect((int)flags, 0);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147352573)
					{
						throw ex;
					}
				}
				return;
			}
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00016F50 File Offset: 0x00015150
		private object AsVariant(AccessibleObject obj)
		{
			if (obj == this)
			{
				return 0;
			}
			return this.AsIAccessible(obj);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00016F64 File Offset: 0x00015164
		private IAccessible AsIAccessible(AccessibleObject obj)
		{
			if (obj != null && obj.systemWrapper)
			{
				return obj.systemIAccessible;
			}
			return obj;
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x00016F79 File Offset: 0x00015179
		// (set) Token: 0x0600083A RID: 2106 RVA: 0x00016F81 File Offset: 0x00015181
		internal int AccessibleObjectId
		{
			get
			{
				return this.accObjId;
			}
			set
			{
				this.accObjId = value;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x00016F8A File Offset: 0x0001518A
		internal bool IsClientObject
		{
			get
			{
				return this.AccessibleObjectId == -4;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x00016F96 File Offset: 0x00015196
		internal bool IsNonClientObject
		{
			get
			{
				return this.AccessibleObjectId == 0;
			}
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00016FA1 File Offset: 0x000151A1
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		internal IAccessible GetSystemIAccessibleInternal()
		{
			return this.systemIAccessible;
		}

		/// <summary>Associates an object with an instance of an <see cref="T:System.Windows.Forms.AccessibleObject" /> based on the handle of the object.</summary>
		/// <param name="handle">An <see cref="T:System.IntPtr" /> that contains the handle of the object.</param>
		// Token: 0x0600083E RID: 2110 RVA: 0x00016FA9 File Offset: 0x000151A9
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected void UseStdAccessibleObjects(IntPtr handle)
		{
			this.UseStdAccessibleObjects(handle, this.AccessibleObjectId);
		}

		/// <summary>Associates an object with an instance of an <see cref="T:System.Windows.Forms.AccessibleObject" /> based on the handle and the object id of the object.</summary>
		/// <param name="handle">An <see cref="T:System.IntPtr" /> that contains the handle of the object.</param>
		/// <param name="objid">An Int that defines the type of object that the <paramref name="handle" /> parameter refers to.</param>
		// Token: 0x0600083F RID: 2111 RVA: 0x00016FB8 File Offset: 0x000151B8
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected void UseStdAccessibleObjects(IntPtr handle, int objid)
		{
			Guid guid = new Guid("{618736E0-3C3D-11CF-810C-00AA00389B71}");
			object obj = null;
			int num = UnsafeNativeMethods.CreateStdAccessibleObject(new HandleRef(this, handle), objid, ref guid, ref obj);
			Guid guid2 = new Guid("{00020404-0000-0000-C000-000000000046}");
			object obj2 = null;
			num = UnsafeNativeMethods.CreateStdAccessibleObject(new HandleRef(this, handle), objid, ref guid2, ref obj2);
			if (obj != null || obj2 != null)
			{
				this.systemIAccessible = (IAccessible)obj;
				this.systemIEnumVariant = (UnsafeNativeMethods.IEnumVariant)obj2;
				this.systemIOleWindow = obj as UnsafeNativeMethods.IOleWindow;
			}
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00017034 File Offset: 0x00015234
		private bool SysNavigate(int navDir, object childID, out object retObject)
		{
			retObject = null;
			if (!childID.Equals(0))
			{
				return false;
			}
			AccessibleObject accessibleObject;
			if (!this.GetSysChild((AccessibleNavigation)navDir, out accessibleObject))
			{
				return false;
			}
			retObject = ((accessibleObject == null) ? null : this.AsVariant(accessibleObject));
			return true;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00017071 File Offset: 0x00015271
		internal void ValidateChildID(ref object childID)
		{
			if (childID == null)
			{
				childID = 0;
				return;
			}
			if (childID.Equals(-2147352572))
			{
				childID = 0;
				return;
			}
			if (!(childID is int))
			{
				childID = 0;
			}
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000170B0 File Offset: 0x000152B0
		private AccessibleObject WrapIAccessible(object iacc)
		{
			IAccessible accessible = iacc as IAccessible;
			if (accessible == null)
			{
				return null;
			}
			if (this.systemIAccessible == iacc)
			{
				return this;
			}
			return new AccessibleObject(accessible);
		}

		/// <summary>Gets a <see cref="T:System.Reflection.MethodInfo" /> object corresponding to a specified method, using a Type array to choose from among overloaded methods. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Type[],System.Reflection.ParameterModifier[])" />.</summary>
		/// <param name="name">The name of the member to find.</param>
		/// <param name="bindingAttr">The binding attributes used to control the search.</param>
		/// <param name="binder">An object that implements <see cref="T:System.Reflection.Binder" />, containing properties related to this method.</param>
		/// <param name="types">An array used to choose among overloaded methods.</param>
		/// <param name="modifiers">An array of parameter modifiers used to make binding work with parameter signatures in which the types have been modified.</param>
		/// <returns>The requested method that matches all the specified parameters.</returns>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">The object implements multiple methods with the same name.</exception>
		// Token: 0x06000843 RID: 2115 RVA: 0x000170DA File Offset: 0x000152DA
		MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
		{
			return typeof(IAccessible).GetMethod(name, bindingAttr, binder, types, modifiers);
		}

		/// <summary>Gets a <see cref="T:System.Reflection.MethodInfo" /> object corresponding to a specified method under specified search constraints. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetMethod(System.String,System.Reflection.BindingFlags)" />.</summary>
		/// <param name="name">The name of the member to find.</param>
		/// <param name="bindingAttr">The binding attributes used to control the search.</param>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object containing the method information, with the match being based on the method name and search constraints specified in <paramref name="bindingAttr" />.</returns>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">The object implements multiple methods with the same name.</exception>
		// Token: 0x06000844 RID: 2116 RVA: 0x000170F2 File Offset: 0x000152F2
		MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr)
		{
			return typeof(IAccessible).GetMethod(name, bindingAttr);
		}

		/// <summary>Gets an array of <see cref="T:System.Reflection.MethodInfo" /> objects with all public methods or all methods of the current class. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetMethods(System.Reflection.BindingFlags)" />.</summary>
		/// <param name="bindingAttr">The binding attributes used to control the search.</param>
		/// <returns>An array of <see cref="T:System.Reflection.MethodInfo" /> objects containing all the methods defined for this reflection object that meet the search constraints specified in <see langword="bindingAttr" />.</returns>
		// Token: 0x06000845 RID: 2117 RVA: 0x00017105 File Offset: 0x00015305
		MethodInfo[] IReflect.GetMethods(BindingFlags bindingAttr)
		{
			return typeof(IAccessible).GetMethods(bindingAttr);
		}

		/// <summary>Gets the <see cref="T:System.Reflection.FieldInfo" /> object corresponding to the specified field and binding flag. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetField(System.String,System.Reflection.BindingFlags)" />.</summary>
		/// <param name="name">The name of the field to find.</param>
		/// <param name="bindingAttr">The binding attributes used to control the search.</param>
		/// <returns>A <see cref="T:System.Reflection.FieldInfo" /> object containing the field information for the named object that meets the search constraints specified in <paramref name="bindingAttr" />.</returns>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">The object implements multiple fields with the same name.</exception>
		// Token: 0x06000846 RID: 2118 RVA: 0x00017117 File Offset: 0x00015317
		FieldInfo IReflect.GetField(string name, BindingFlags bindingAttr)
		{
			return typeof(IAccessible).GetField(name, bindingAttr);
		}

		/// <summary>Gets an array of <see cref="T:System.Reflection.FieldInfo" /> objects corresponding to all fields of the current class. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetFields(System.Reflection.BindingFlags)" />.</summary>
		/// <param name="bindingAttr">The binding attributes used to control the search.</param>
		/// <returns>An array of <see cref="T:System.Reflection.FieldInfo" /> objects containing all the field information for this reflection object that meets the search constraints specified in <paramref name="bindingAttr" />.</returns>
		// Token: 0x06000847 RID: 2119 RVA: 0x0001712A File Offset: 0x0001532A
		FieldInfo[] IReflect.GetFields(BindingFlags bindingAttr)
		{
			return typeof(IAccessible).GetFields(bindingAttr);
		}

		/// <summary>Gets a <see cref="T:System.Reflection.PropertyInfo" /> object corresponding to a specified property under specified search constraints. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetProperty(System.String,System.Reflection.BindingFlags)" />.</summary>
		/// <param name="name">The name of the property to find.</param>
		/// <param name="bindingAttr">The binding attributes used to control the search.</param>
		/// <returns>A <see cref="T:System.Reflection.PropertyInfo" /> object for the located property that meets the search constraints specified in <paramref name="bindingAttr" />, or <see langword="null" /> if the property was not located.</returns>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">The object implements multiple methods with the same name.</exception>
		// Token: 0x06000848 RID: 2120 RVA: 0x0001713C File Offset: 0x0001533C
		PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr)
		{
			return typeof(IAccessible).GetProperty(name, bindingAttr);
		}

		/// <summary>Gets a <see cref="T:System.Reflection.PropertyInfo" /> object corresponding to a specified property with specified search constraints. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetProperty(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Type,System.Type[],System.Reflection.ParameterModifier[])" />.</summary>
		/// <param name="name">The name of the member to find.</param>
		/// <param name="bindingAttr">The binding attributes used to control the search.</param>
		/// <param name="binder">An object that implements Binder, containing properties related to this method.</param>
		/// <param name="returnType">An array used to choose among overloaded methods.</param>
		/// <param name="types">An array of parameter modifiers used to make binding work with parameter signatures in which the types have been modified.</param>
		/// <param name="modifiers">An array used to choose the parameter modifiers.</param>
		/// <returns>A <see cref="T:System.Reflection.PropertyInfo" /> object for the located property, if a property with the specified name was located in this reflection object, or <see langword="null" /> if the property was not located.</returns>
		// Token: 0x06000849 RID: 2121 RVA: 0x0001714F File Offset: 0x0001534F
		PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			return typeof(IAccessible).GetProperty(name, bindingAttr, binder, returnType, types, modifiers);
		}

		/// <summary>Gets an array of <see cref="T:System.Reflection.PropertyInfo" /> objects corresponding to all public properties or to all properties of the current class. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetProperties(System.Reflection.BindingFlags)" />.</summary>
		/// <param name="bindingAttr">The binding attribute used to control the search.</param>
		/// <returns>An array of <see cref="T:System.Reflection.PropertyInfo" /> objects for all the properties defined on the reflection object.</returns>
		// Token: 0x0600084A RID: 2122 RVA: 0x00017169 File Offset: 0x00015369
		PropertyInfo[] IReflect.GetProperties(BindingFlags bindingAttr)
		{
			return typeof(IAccessible).GetProperties(bindingAttr);
		}

		/// <summary>Gets an array of <see cref="T:System.Reflection.MemberInfo" /> objects corresponding to all public members or to all members that match a specified name. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetMember(System.String,System.Reflection.BindingFlags)" />.</summary>
		/// <param name="name">The name of the member to find.</param>
		/// <param name="bindingAttr">The binding attributes used to control the search.</param>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects matching the name parameter.</returns>
		// Token: 0x0600084B RID: 2123 RVA: 0x0001717B File Offset: 0x0001537B
		MemberInfo[] IReflect.GetMember(string name, BindingFlags bindingAttr)
		{
			return typeof(IAccessible).GetMember(name, bindingAttr);
		}

		/// <summary>Gets an array of <see cref="T:System.Reflection.MemberInfo" /> objects corresponding either to all public members or to all members of the current class. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetMembers(System.Reflection.BindingFlags)" />.</summary>
		/// <param name="bindingAttr">The binding attributes used to control the search.</param>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects containing all the member information for this reflection object.</returns>
		// Token: 0x0600084C RID: 2124 RVA: 0x0001718E File Offset: 0x0001538E
		MemberInfo[] IReflect.GetMembers(BindingFlags bindingAttr)
		{
			return typeof(IAccessible).GetMembers(bindingAttr);
		}

		/// <summary>Invokes a specified member. For a description of this member, see <see cref="M:System.Reflection.IReflect.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[])" />.</summary>
		/// <param name="name">The name of the member to find.</param>
		/// <param name="invokeAttr">One of the <see cref="T:System.Reflection.BindingFlags" /> invocation attributes.</param>
		/// <param name="binder">One of the <see cref="T:System.Reflection.BindingFlags" /> bit flags. Implements Binder, containing properties related to this method.</param>
		/// <param name="target">The object on which to invoke the specified member. This parameter is ignored for static members.</param>
		/// <param name="args">An array of objects that contains the number, order, and type of the parameters of the member to be invoked. This is an empty array if there are no parameters.</param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects.</param>
		/// <param name="culture">An instance of <see cref="T:System.Globalization.CultureInfo" /> used to govern the coercion of types.</param>
		/// <param name="namedParameters">A String array of parameters.</param>
		/// <returns>The specified member.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="invokeAttr" /> is <see cref="F:System.Reflection.BindingFlags.CreateInstance" /> and another bit flag is also set.
		/// -or-
		/// <paramref name="invokeAttr" /> is not <see cref="F:System.Reflection.BindingFlags.CreateInstance" /> and name is <see langword="null" />.
		/// -or-
		/// <paramref name="invokeAttr" /> is not an invocation attribute from <see cref="T:System.Reflection.BindingFlags" />.
		/// -or-
		/// <paramref name="invokeAttr" /> specifies both get and set for a property or field.
		/// -or-
		/// <paramref name="invokeAttr" /> specifies both a field set and an Invoke method.<paramref name="args" /> is provided for a field get operation.
		/// -or-
		/// More than one argument is specified for a field set operation.</exception>
		/// <exception cref="T:System.MissingFieldException">The field or property cannot be found.</exception>
		/// <exception cref="T:System.MissingMethodException">The method cannot be found.</exception>
		/// <exception cref="T:System.Security.SecurityException">A private member is invoked without the necessary <see cref="T:System.Security.Permissions.ReflectionPermission" />.</exception>
		// Token: 0x0600084D RID: 2125 RVA: 0x000171A0 File Offset: 0x000153A0
		object IReflect.InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			if (args.Length == 0)
			{
				MemberInfo[] member = typeof(IAccessible).GetMember(name);
				if (member != null && member.Length != 0 && member[0] is PropertyInfo)
				{
					MethodInfo getMethod = ((PropertyInfo)member[0]).GetGetMethod();
					if (getMethod != null && getMethod.GetParameters().Length != 0)
					{
						args = new object[getMethod.GetParameters().Length];
						for (int i = 0; i < args.Length; i++)
						{
							args[i] = 0;
						}
					}
				}
			}
			return typeof(IAccessible).InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		/// <summary>Gets the underlying type that represents the <see cref="T:System.Reflection.IReflect" /> object. For a description of this member, see <see cref="P:System.Reflection.IReflect.UnderlyingSystemType" />.</summary>
		/// <returns>The underlying type that represents the <see cref="T:System.Reflection.IReflect" /> object.</returns>
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x00017238 File Offset: 0x00015438
		Type IReflect.UnderlyingSystemType
		{
			get
			{
				return typeof(IAccessible);
			}
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00017244 File Offset: 0x00015444
		UnsafeNativeMethods.IRawElementProviderSimple UnsafeNativeMethods.IRawElementProviderHwndOverride.GetOverrideProviderForHwnd(IntPtr hwnd)
		{
			return this.GetOverrideProviderForHwnd(hwnd);
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x00016240 File Offset: 0x00014440
		bool UnsafeNativeMethods.IRangeValueProvider.IsReadOnly
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x0001724D File Offset: 0x0001544D
		double UnsafeNativeMethods.IRangeValueProvider.LargeChange
		{
			get
			{
				return this.LargeChange;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x00017255 File Offset: 0x00015455
		double UnsafeNativeMethods.IRangeValueProvider.Maximum
		{
			get
			{
				return this.Maximum;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x0001725D File Offset: 0x0001545D
		double UnsafeNativeMethods.IRangeValueProvider.Minimum
		{
			get
			{
				return this.Minimum;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x00017265 File Offset: 0x00015465
		double UnsafeNativeMethods.IRangeValueProvider.SmallChange
		{
			get
			{
				return this.SmallChange;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x0001726D File Offset: 0x0001546D
		double UnsafeNativeMethods.IRangeValueProvider.Value
		{
			get
			{
				return this.RangeValue;
			}
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00017275 File Offset: 0x00015475
		void UnsafeNativeMethods.IRangeValueProvider.SetValue(double value)
		{
			this.SetValue(value);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00017280 File Offset: 0x00015480
		object[] UnsafeNativeMethods.ISelectionProvider.GetSelection()
		{
			return this.GetSelection();
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x00017295 File Offset: 0x00015495
		bool UnsafeNativeMethods.ISelectionProvider.CanSelectMultiple
		{
			get
			{
				return this.CanSelectMultiple;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x0001729D File Offset: 0x0001549D
		bool UnsafeNativeMethods.ISelectionProvider.IsSelectionRequired
		{
			get
			{
				return this.IsSelectionRequired;
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x000172A5 File Offset: 0x000154A5
		void UnsafeNativeMethods.ISelectionItemProvider.Select()
		{
			this.SelectItem();
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x000172AD File Offset: 0x000154AD
		void UnsafeNativeMethods.ISelectionItemProvider.AddToSelection()
		{
			this.AddToSelection();
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x000172B5 File Offset: 0x000154B5
		void UnsafeNativeMethods.ISelectionItemProvider.RemoveFromSelection()
		{
			this.RemoveFromSelection();
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x000172BD File Offset: 0x000154BD
		bool UnsafeNativeMethods.ISelectionItemProvider.IsSelected
		{
			get
			{
				return this.IsItemSelected;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x000172C5 File Offset: 0x000154C5
		UnsafeNativeMethods.IRawElementProviderSimple UnsafeNativeMethods.ISelectionItemProvider.SelectionContainer
		{
			get
			{
				return this.ItemSelectionContainer;
			}
		}

		/// <summary>Raises the UI automation notification event.</summary>
		/// <param name="notificationKind">The type of notification.</param>
		/// <param name="notificationProcessing">An indicator of how to process notifications.</param>
		/// <param name="notificationText">The text of the notification.</param>
		/// <returns>
		///   <see langword="true" /> if the operation succeeds; <see langword="false" /> if the underlying Windows infrastucture is not available or the operation failed. Call <see cref="M:System.Runtime.InteropServices.Marshal.GetLastWin32Error" /> for details.</returns>
		// Token: 0x0600085F RID: 2143 RVA: 0x000172D0 File Offset: 0x000154D0
		public bool RaiseAutomationNotification(AutomationNotificationKind notificationKind, AutomationNotificationProcessing notificationProcessing, string notificationText)
		{
			if (!AccessibilityImprovements.Level3 || !AccessibleObject.notificationEventAvailable || LocalAppContextSwitches.NoClientNotifications)
			{
				return false;
			}
			int num = 1;
			try
			{
				num = UnsafeNativeMethods.UiaRaiseNotificationEvent(this, notificationKind, notificationProcessing, notificationText, string.Empty);
			}
			catch (EntryPointNotFoundException)
			{
				AccessibleObject.notificationEventAvailable = false;
			}
			return num == 0;
		}

		/// <summary>Raises the LiveRegionChanged UI automation event.</summary>
		/// <returns>
		///   <see langword="true" /> if the operation succeeds; <see langword="False" /> otherwise.</returns>
		/// <exception cref="T:System.NotSupportedException">Accessibility object live regions are not supported.</exception>
		// Token: 0x06000860 RID: 2144 RVA: 0x00017324 File Offset: 0x00015524
		public virtual bool RaiseLiveRegionChanged()
		{
			throw new NotSupportedException(SR.GetString("AccessibleObjectLiveRegionNotSupported"));
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00017338 File Offset: 0x00015538
		internal bool RaiseAutomationEvent(int eventId)
		{
			if (UnsafeNativeMethods.UiaClientsAreListening() && !LocalAppContextSwitches.NoClientNotifications)
			{
				int num = UnsafeNativeMethods.UiaRaiseAutomationEvent(this, eventId);
				return num == 0;
			}
			return false;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00017364 File Offset: 0x00015564
		internal bool RaiseAutomationPropertyChangedEvent(int propertyId, object oldValue, object newValue)
		{
			if (UnsafeNativeMethods.UiaClientsAreListening() && !LocalAppContextSwitches.NoClientNotifications)
			{
				int num = UnsafeNativeMethods.UiaRaiseAutomationPropertyChangedEvent(this, propertyId, oldValue, newValue);
				return num == 0;
			}
			return false;
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00017390 File Offset: 0x00015590
		internal bool RaiseStructureChangedEvent(UnsafeNativeMethods.StructureChangeType structureChangeType, int[] runtimeId)
		{
			if (UnsafeNativeMethods.UiaClientsAreListening() && !LocalAppContextSwitches.NoClientNotifications)
			{
				int num = UnsafeNativeMethods.UiaRaiseStructureChangedEvent(this, structureChangeType, runtimeId, (runtimeId == null) ? 0 : runtimeId.Length);
				return num == 0;
			}
			return false;
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x000173C3 File Offset: 0x000155C3
		void UnsafeNativeMethods.IScrollItemProvider.ScrollIntoView()
		{
			this.ScrollIntoView();
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void ScrollIntoView()
		{
		}

		// Token: 0x04000530 RID: 1328
		private IAccessible systemIAccessible;

		// Token: 0x04000531 RID: 1329
		private UnsafeNativeMethods.IEnumVariant systemIEnumVariant;

		// Token: 0x04000532 RID: 1330
		private UnsafeNativeMethods.IEnumVariant enumVariant;

		// Token: 0x04000533 RID: 1331
		private UnsafeNativeMethods.IOleWindow systemIOleWindow;

		// Token: 0x04000534 RID: 1332
		private bool systemWrapper;

		// Token: 0x04000535 RID: 1333
		private int accObjId = -4;

		// Token: 0x04000536 RID: 1334
		private static bool notificationEventAvailable = true;

		// Token: 0x04000537 RID: 1335
		internal const int RuntimeIDFirstItem = 42;

		// Token: 0x020005FD RID: 1533
		private class EnumVariantObject : UnsafeNativeMethods.IEnumVariant
		{
			// Token: 0x060061B0 RID: 25008 RVA: 0x00168E65 File Offset: 0x00167065
			public EnumVariantObject(AccessibleObject owner)
			{
				this.owner = owner;
			}

			// Token: 0x060061B1 RID: 25009 RVA: 0x00168E74 File Offset: 0x00167074
			public EnumVariantObject(AccessibleObject owner, int currentChild)
			{
				this.owner = owner;
				this.currentChild = currentChild;
			}

			// Token: 0x060061B2 RID: 25010 RVA: 0x00168E8A File Offset: 0x0016708A
			void UnsafeNativeMethods.IEnumVariant.Clone(UnsafeNativeMethods.IEnumVariant[] v)
			{
				v[0] = new AccessibleObject.EnumVariantObject(this.owner, this.currentChild);
			}

			// Token: 0x060061B3 RID: 25011 RVA: 0x00168EA0 File Offset: 0x001670A0
			void UnsafeNativeMethods.IEnumVariant.Reset()
			{
				this.currentChild = 0;
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					if (this.owner.systemIEnumVariant != null)
					{
						this.owner.systemIEnumVariant.Reset();
					}
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}

			// Token: 0x060061B4 RID: 25012 RVA: 0x00168EF4 File Offset: 0x001670F4
			void UnsafeNativeMethods.IEnumVariant.Skip(int n)
			{
				this.currentChild += n;
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					if (this.owner.systemIEnumVariant != null)
					{
						this.owner.systemIEnumVariant.Skip(n);
					}
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}

			// Token: 0x060061B5 RID: 25013 RVA: 0x00168F50 File Offset: 0x00167150
			int UnsafeNativeMethods.IEnumVariant.Next(int n, IntPtr rgvar, int[] ns)
			{
				if (this.owner.IsClientObject)
				{
					int childCount;
					int[] sysChildOrder;
					if ((childCount = this.owner.GetChildCount()) >= 0)
					{
						this.NextFromChildCollection(n, rgvar, ns, childCount);
					}
					else if (this.owner.systemIEnumVariant == null)
					{
						this.NextEmpty(n, rgvar, ns);
					}
					else if ((sysChildOrder = this.owner.GetSysChildOrder()) != null)
					{
						this.NextFromSystemReordered(n, rgvar, ns, sysChildOrder);
					}
					else
					{
						this.NextFromSystem(n, rgvar, ns);
					}
				}
				else
				{
					this.NextFromSystem(n, rgvar, ns);
				}
				if (ns[0] != n)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x060061B6 RID: 25014 RVA: 0x00168FD8 File Offset: 0x001671D8
			private void NextFromSystem(int n, IntPtr rgvar, int[] ns)
			{
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					this.owner.systemIEnumVariant.Next(n, rgvar, ns);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				this.currentChild += ns[0];
			}

			// Token: 0x060061B7 RID: 25015 RVA: 0x0016902C File Offset: 0x0016722C
			private void NextFromSystemReordered(int n, IntPtr rgvar, int[] ns, int[] newOrder)
			{
				int num = 0;
				while (num < n && this.currentChild < newOrder.Length && AccessibleObject.EnumVariantObject.GotoItem(this.owner.systemIEnumVariant, newOrder[this.currentChild], AccessibleObject.EnumVariantObject.GetAddressOfVariantAtIndex(rgvar, num)))
				{
					this.currentChild++;
					num++;
				}
				ns[0] = num;
			}

			// Token: 0x060061B8 RID: 25016 RVA: 0x00169088 File Offset: 0x00167288
			private void NextFromChildCollection(int n, IntPtr rgvar, int[] ns, int childCount)
			{
				int num = 0;
				while (num < n && this.currentChild < childCount)
				{
					this.currentChild++;
					Marshal.GetNativeVariantForObject(this.currentChild, AccessibleObject.EnumVariantObject.GetAddressOfVariantAtIndex(rgvar, num));
					num++;
				}
				ns[0] = num;
			}

			// Token: 0x060061B9 RID: 25017 RVA: 0x001690D4 File Offset: 0x001672D4
			private void NextEmpty(int n, IntPtr rgvar, int[] ns)
			{
				ns[0] = 0;
			}

			// Token: 0x060061BA RID: 25018 RVA: 0x001690DC File Offset: 0x001672DC
			private static bool GotoItem(UnsafeNativeMethods.IEnumVariant iev, int index, IntPtr variantPtr)
			{
				int[] array = new int[1];
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					iev.Reset();
					iev.Skip(index);
					iev.Next(1, variantPtr, array);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				return array[0] == 1;
			}

			// Token: 0x060061BB RID: 25019 RVA: 0x00169130 File Offset: 0x00167330
			private static IntPtr GetAddressOfVariantAtIndex(IntPtr variantArrayPtr, int index)
			{
				int num = 8 + IntPtr.Size * 2;
				return (IntPtr)((long)variantArrayPtr + (long)index * (long)num);
			}

			// Token: 0x04003899 RID: 14489
			private int currentChild;

			// Token: 0x0400389A RID: 14490
			private AccessibleObject owner;
		}
	}
}
