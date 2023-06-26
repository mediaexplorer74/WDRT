using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Internal;

namespace System.Windows.Forms.Design
{
	/// <summary>Provides a user interface for a <see cref="T:System.Windows.Forms.Design.WindowsFormsComponentEditor" />.</summary>
	// Token: 0x02000486 RID: 1158
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ToolboxItem(false)]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public partial class ComponentEditorForm : Form
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.ComponentEditorForm" /> class.</summary>
		/// <param name="component">The component to be edited.</param>
		/// <param name="pageTypes">The set of <see cref="T:System.Windows.Forms.Design.ComponentEditorPage" /> objects to be shown in the form.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="component" /> is not an <see cref="T:System.ComponentModel.IComponent" />.</exception>
		// Token: 0x06004DD5 RID: 19925 RVA: 0x00141710 File Offset: 0x0013F910
		public ComponentEditorForm(object component, Type[] pageTypes)
		{
			if (!(component is IComponent))
			{
				throw new ArgumentException(SR.GetString("ComponentEditorFormBadComponent"), "component");
			}
			this.component = (IComponent)component;
			this.pageTypes = pageTypes;
			this.dirty = false;
			this.firstActivate = true;
			this.activePage = -1;
			this.initialActivePage = 0;
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MinimizeBox = false;
			base.MaximizeBox = false;
			base.ShowInTaskbar = false;
			base.Icon = null;
			base.StartPosition = FormStartPosition.CenterParent;
			this.OnNewObjects();
			this.OnConfigureUI();
		}

		// Token: 0x06004DD6 RID: 19926 RVA: 0x001417BC File Offset: 0x0013F9BC
		internal virtual void ApplyChanges(bool lastApply)
		{
			if (this.dirty)
			{
				IComponentChangeService componentChangeService = null;
				if (this.component.Site != null)
				{
					componentChangeService = (IComponentChangeService)this.component.Site.GetService(typeof(IComponentChangeService));
					if (componentChangeService != null)
					{
						try
						{
							componentChangeService.OnComponentChanging(this.component, null);
						}
						catch (CheckoutException ex)
						{
							if (ex == CheckoutException.Canceled)
							{
								return;
							}
							throw ex;
						}
					}
				}
				for (int i = 0; i < this.pageSites.Length; i++)
				{
					if (this.pageSites[i].Dirty)
					{
						this.pageSites[i].GetPageControl().ApplyChanges();
						this.pageSites[i].Dirty = false;
					}
				}
				if (componentChangeService != null)
				{
					componentChangeService.OnComponentChanged(this.component, null, null, null);
				}
				this.applyButton.Enabled = false;
				this.cancelButton.Text = SR.GetString("CloseCaption");
				this.dirty = false;
				if (!lastApply)
				{
					for (int j = 0; j < this.pageSites.Length; j++)
					{
						this.pageSites[j].GetPageControl().OnApplyComplete();
					}
				}
			}
		}

		/// <summary>Resize the form according to the setting of <see cref="P:System.Windows.Forms.Form.AutoSizeMode" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the form will automatically resize; <see langword="false" /> if it must be manually resized.</returns>
		// Token: 0x17001329 RID: 4905
		// (get) Token: 0x06004DD7 RID: 19927 RVA: 0x00108FA6 File Offset: 0x001071A6
		// (set) Token: 0x06004DD8 RID: 19928 RVA: 0x00108FAE File Offset: 0x001071AE
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Form.AutoSize" /> property changes.</summary>
		// Token: 0x14000409 RID: 1033
		// (add) Token: 0x06004DD9 RID: 19929 RVA: 0x00108FB7 File Offset: 0x001071B7
		// (remove) Token: 0x06004DDA RID: 19930 RVA: 0x00108FC0 File Offset: 0x001071C0
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler AutoSizeChanged
		{
			add
			{
				base.AutoSizeChanged += value;
			}
			remove
			{
				base.AutoSizeChanged -= value;
			}
		}

		// Token: 0x06004DDB RID: 19931 RVA: 0x001418DC File Offset: 0x0013FADC
		private void OnButtonClick(object sender, EventArgs e)
		{
			if (sender == this.okButton)
			{
				this.ApplyChanges(true);
				base.DialogResult = DialogResult.OK;
				return;
			}
			if (sender == this.cancelButton)
			{
				base.DialogResult = DialogResult.Cancel;
				return;
			}
			if (sender == this.applyButton)
			{
				this.ApplyChanges(false);
				return;
			}
			if (sender == this.helpButton)
			{
				this.ShowPageHelp();
			}
		}

		// Token: 0x06004DDC RID: 19932 RVA: 0x00141934 File Offset: 0x0013FB34
		private void OnConfigureUI()
		{
			Font font = Control.DefaultFont;
			if (this.component.Site != null)
			{
				IUIService iuiservice = (IUIService)this.component.Site.GetService(typeof(IUIService));
				if (iuiservice != null)
				{
					font = (Font)iuiservice.Styles["DialogFont"];
				}
			}
			this.Font = font;
			this.okButton = new Button();
			this.cancelButton = new Button();
			this.applyButton = new Button();
			this.helpButton = new Button();
			this.selectorImageList = new ImageList();
			this.selectorImageList.ImageSize = new Size(16, 16);
			this.selector = new ComponentEditorForm.PageSelector();
			this.selector.ImageList = this.selectorImageList;
			this.selector.AfterSelect += this.OnSelChangeSelector;
			Label label = new Label();
			label.BackColor = SystemColors.ControlDark;
			int num = 90;
			if (this.pageSites != null)
			{
				for (int i = 0; i < this.pageSites.Length; i++)
				{
					ComponentEditorPage pageControl = this.pageSites[i].GetPageControl();
					string title = pageControl.Title;
					Graphics graphics = base.CreateGraphicsInternal();
					int num2 = (int)graphics.MeasureString(title, this.Font).Width;
					graphics.Dispose();
					this.selectorImageList.Images.Add(pageControl.Icon.ToBitmap());
					this.selector.Nodes.Add(new TreeNode(title, i, i));
					if (num2 > num)
					{
						num = num2;
					}
				}
			}
			num += 10;
			string text = string.Empty;
			ISite site = this.component.Site;
			if (site != null)
			{
				text = SR.GetString("ComponentEditorFormProperties", new object[] { site.Name });
			}
			else
			{
				text = SR.GetString("ComponentEditorFormPropertiesNoName");
			}
			this.Text = text;
			Rectangle rectangle = new Rectangle(12 + num, 16, this.maxSize.Width, this.maxSize.Height);
			this.pageHost.Bounds = rectangle;
			label.Bounds = new Rectangle(rectangle.X, 6, rectangle.Width, 4);
			if (this.pageSites != null)
			{
				Rectangle rectangle2 = new Rectangle(0, 0, rectangle.Width, rectangle.Height);
				for (int j = 0; j < this.pageSites.Length; j++)
				{
					ComponentEditorPage pageControl2 = this.pageSites[j].GetPageControl();
					pageControl2.GetControl().Bounds = rectangle2;
				}
			}
			int width = SystemInformation.FixedFrameBorderSize.Width;
			Rectangle rectangle3 = rectangle;
			Size size = new Size(rectangle3.Width + 3 * (6 + width) + num, rectangle3.Height + 4 + 24 + 23 + 2 * width + SystemInformation.CaptionHeight);
			base.Size = size;
			this.selector.Bounds = new Rectangle(6, 6, num, rectangle3.Height + 4 + 12 + 23);
			rectangle3.X = rectangle3.Width + rectangle3.X - 80;
			rectangle3.Y = rectangle3.Height + rectangle3.Y + 6;
			rectangle3.Width = 80;
			rectangle3.Height = 23;
			this.helpButton.Bounds = rectangle3;
			this.helpButton.Text = SR.GetString("HelpCaption");
			this.helpButton.Click += this.OnButtonClick;
			this.helpButton.Enabled = false;
			this.helpButton.FlatStyle = FlatStyle.System;
			rectangle3.X -= 86;
			this.applyButton.Bounds = rectangle3;
			this.applyButton.Text = SR.GetString("ApplyCaption");
			this.applyButton.Click += this.OnButtonClick;
			this.applyButton.Enabled = false;
			this.applyButton.FlatStyle = FlatStyle.System;
			rectangle3.X -= 86;
			this.cancelButton.Bounds = rectangle3;
			this.cancelButton.Text = SR.GetString("CancelCaption");
			this.cancelButton.Click += this.OnButtonClick;
			this.cancelButton.FlatStyle = FlatStyle.System;
			base.CancelButton = this.cancelButton;
			rectangle3.X -= 86;
			this.okButton.Bounds = rectangle3;
			this.okButton.Text = SR.GetString("OKCaption");
			this.okButton.Click += this.OnButtonClick;
			this.okButton.FlatStyle = FlatStyle.System;
			base.AcceptButton = this.okButton;
			base.Controls.Clear();
			base.Controls.AddRange(new Control[] { this.selector, label, this.pageHost, this.okButton, this.cancelButton, this.applyButton, this.helpButton });
			this.AutoScaleBaseSize = new Size(5, 14);
			base.ApplyAutoScaling();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Activated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004DDD RID: 19933 RVA: 0x00141E50 File Offset: 0x00140050
		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);
			if (this.firstActivate)
			{
				this.firstActivate = false;
				this.selector.SelectedNode = this.selector.Nodes[this.initialActivePage];
				this.pageSites[this.initialActivePage].Active = true;
				this.activePage = this.initialActivePage;
				this.helpButton.Enabled = this.pageSites[this.activePage].GetPageControl().SupportsHelp();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HelpRequested" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.HelpEventArgs" /> that contains the event data.</param>
		// Token: 0x06004DDE RID: 19934 RVA: 0x00141ED5 File Offset: 0x001400D5
		protected override void OnHelpRequested(HelpEventArgs e)
		{
			base.OnHelpRequested(e);
			this.ShowPageHelp();
		}

		// Token: 0x06004DDF RID: 19935 RVA: 0x00141EE4 File Offset: 0x001400E4
		private void OnNewObjects()
		{
			this.pageSites = null;
			this.maxSize = new Size(258, 24 * this.pageTypes.Length);
			this.pageSites = new ComponentEditorForm.ComponentEditorPageSite[this.pageTypes.Length];
			for (int i = 0; i < this.pageTypes.Length; i++)
			{
				this.pageSites[i] = new ComponentEditorForm.ComponentEditorPageSite(this.pageHost, this.pageTypes[i], this.component, this);
				ComponentEditorPage pageControl = this.pageSites[i].GetPageControl();
				Size size = pageControl.Size;
				if (size.Width > this.maxSize.Width)
				{
					this.maxSize.Width = size.Width;
				}
				if (size.Height > this.maxSize.Height)
				{
					this.maxSize.Height = size.Height;
				}
			}
			for (int j = 0; j < this.pageSites.Length; j++)
			{
				this.pageSites[j].GetPageControl().Size = this.maxSize;
			}
		}

		/// <summary>Switches between component editor pages.</summary>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewEventArgs" /> that contains the event data.</param>
		/// <exception cref="T:System.ComponentModel.Design.CheckoutException">A designer file is checked into source code control and cannot be changed.</exception>
		// Token: 0x06004DE0 RID: 19936 RVA: 0x00141FF0 File Offset: 0x001401F0
		protected virtual void OnSelChangeSelector(object source, TreeViewEventArgs e)
		{
			if (this.firstActivate)
			{
				return;
			}
			int index = this.selector.SelectedNode.Index;
			if (index == this.activePage)
			{
				return;
			}
			if (this.activePage != -1)
			{
				if (this.pageSites[this.activePage].AutoCommit)
				{
					this.ApplyChanges(false);
				}
				this.pageSites[this.activePage].Active = false;
			}
			this.activePage = index;
			this.pageSites[this.activePage].Active = true;
			this.helpButton.Enabled = this.pageSites[this.activePage].GetPageControl().SupportsHelp();
		}

		/// <summary>Provides a method to override in order to preprocess input messages before they are dispatched.</summary>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" /> that specifies the message to preprocess.</param>
		/// <returns>
		///   <see langword="true" /> if the specified message is for a component editor page; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004DE1 RID: 19937 RVA: 0x00142094 File Offset: 0x00140294
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public override bool PreProcessMessage(ref Message msg)
		{
			return (this.pageSites != null && this.pageSites[this.activePage].GetPageControl().IsPageMessage(ref msg)) || base.PreProcessMessage(ref msg);
		}

		// Token: 0x06004DE2 RID: 19938 RVA: 0x001420C1 File Offset: 0x001402C1
		internal virtual void SetDirty()
		{
			this.dirty = true;
			this.applyButton.Enabled = true;
			this.cancelButton.Text = SR.GetString("CancelCaption");
		}

		/// <summary>Shows the form. The form will have no owner window.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values indicating the result code returned from the dialog box.</returns>
		// Token: 0x06004DE3 RID: 19939 RVA: 0x001420EB File Offset: 0x001402EB
		public virtual DialogResult ShowForm()
		{
			return this.ShowForm(null, 0);
		}

		/// <summary>Shows the specified page of the specified form. The form will have no owner window.</summary>
		/// <param name="page">The index of the page to show.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values indicating the result code returned from the dialog box.</returns>
		// Token: 0x06004DE4 RID: 19940 RVA: 0x001420F5 File Offset: 0x001402F5
		public virtual DialogResult ShowForm(int page)
		{
			return this.ShowForm(null, page);
		}

		/// <summary>Shows the form with the specified owner.</summary>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.IWin32Window" /> to own the dialog.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values indicating the result code returned from the dialog box.</returns>
		// Token: 0x06004DE5 RID: 19941 RVA: 0x001420FF File Offset: 0x001402FF
		public virtual DialogResult ShowForm(IWin32Window owner)
		{
			return this.ShowForm(owner, 0);
		}

		/// <summary>Shows the form and the specified page with the specified owner.</summary>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.IWin32Window" /> to own the dialog.</param>
		/// <param name="page">The index of the page to show.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values indicating the result code returned from the dialog box.</returns>
		// Token: 0x06004DE6 RID: 19942 RVA: 0x00142109 File Offset: 0x00140309
		public virtual DialogResult ShowForm(IWin32Window owner, int page)
		{
			this.initialActivePage = page;
			base.ShowDialog(owner);
			return base.DialogResult;
		}

		// Token: 0x06004DE7 RID: 19943 RVA: 0x00142120 File Offset: 0x00140320
		private void ShowPageHelp()
		{
			if (this.pageSites[this.activePage].GetPageControl().SupportsHelp())
			{
				this.pageSites[this.activePage].GetPageControl().ShowHelp();
			}
		}

		// Token: 0x040033CC RID: 13260
		private IComponent component;

		// Token: 0x040033CD RID: 13261
		private Type[] pageTypes;

		// Token: 0x040033CE RID: 13262
		private ComponentEditorForm.ComponentEditorPageSite[] pageSites;

		// Token: 0x040033CF RID: 13263
		private Size maxSize = Size.Empty;

		// Token: 0x040033D0 RID: 13264
		private int initialActivePage;

		// Token: 0x040033D1 RID: 13265
		private int activePage;

		// Token: 0x040033D2 RID: 13266
		private bool dirty;

		// Token: 0x040033D3 RID: 13267
		private bool firstActivate;

		// Token: 0x040033D4 RID: 13268
		private Panel pageHost = new Panel();

		// Token: 0x040033D5 RID: 13269
		private ComponentEditorForm.PageSelector selector;

		// Token: 0x040033D6 RID: 13270
		private ImageList selectorImageList;

		// Token: 0x040033D7 RID: 13271
		private Button okButton;

		// Token: 0x040033D8 RID: 13272
		private Button cancelButton;

		// Token: 0x040033D9 RID: 13273
		private Button applyButton;

		// Token: 0x040033DA RID: 13274
		private Button helpButton;

		// Token: 0x040033DB RID: 13275
		private const int BUTTON_WIDTH = 80;

		// Token: 0x040033DC RID: 13276
		private const int BUTTON_HEIGHT = 23;

		// Token: 0x040033DD RID: 13277
		private const int BUTTON_PAD = 6;

		// Token: 0x040033DE RID: 13278
		private const int MIN_SELECTOR_WIDTH = 90;

		// Token: 0x040033DF RID: 13279
		private const int SELECTOR_PADDING = 10;

		// Token: 0x040033E0 RID: 13280
		private const int STRIP_HEIGHT = 4;

		// Token: 0x0200084D RID: 2125
		private sealed class ComponentEditorPageSite : IComponentEditorPageSite
		{
			// Token: 0x06007047 RID: 28743 RVA: 0x0019ACF0 File Offset: 0x00198EF0
			internal ComponentEditorPageSite(Control parent, Type pageClass, IComponent component, ComponentEditorForm form)
			{
				this.component = component;
				this.parent = parent;
				this.isActive = false;
				this.isDirty = false;
				if (form == null)
				{
					throw new ArgumentNullException("form");
				}
				this.form = form;
				try
				{
					this.pageControl = (ComponentEditorPage)SecurityUtils.SecureCreateInstance(pageClass);
				}
				catch (TargetInvocationException ex)
				{
					throw new TargetInvocationException(SR.GetString("ExceptionCreatingCompEditorControl", new object[] { ex.ToString() }), ex.InnerException);
				}
				this.pageControl.SetSite(this);
				this.pageControl.SetComponent(component);
			}

			// Token: 0x17001881 RID: 6273
			// (set) Token: 0x06007048 RID: 28744 RVA: 0x0019AD98 File Offset: 0x00198F98
			internal bool Active
			{
				set
				{
					if (value)
					{
						this.pageControl.CreateControl();
						this.pageControl.Activate();
					}
					else
					{
						this.pageControl.Deactivate();
					}
					this.isActive = value;
				}
			}

			// Token: 0x17001882 RID: 6274
			// (get) Token: 0x06007049 RID: 28745 RVA: 0x0019ADC7 File Offset: 0x00198FC7
			internal bool AutoCommit
			{
				get
				{
					return this.pageControl.CommitOnDeactivate;
				}
			}

			// Token: 0x17001883 RID: 6275
			// (get) Token: 0x0600704A RID: 28746 RVA: 0x0019ADD4 File Offset: 0x00198FD4
			// (set) Token: 0x0600704B RID: 28747 RVA: 0x0019ADDC File Offset: 0x00198FDC
			internal bool Dirty
			{
				get
				{
					return this.isDirty;
				}
				set
				{
					this.isDirty = value;
				}
			}

			// Token: 0x0600704C RID: 28748 RVA: 0x0019ADE5 File Offset: 0x00198FE5
			public Control GetControl()
			{
				return this.parent;
			}

			// Token: 0x0600704D RID: 28749 RVA: 0x0019ADED File Offset: 0x00198FED
			internal ComponentEditorPage GetPageControl()
			{
				return this.pageControl;
			}

			// Token: 0x0600704E RID: 28750 RVA: 0x0019ADF5 File Offset: 0x00198FF5
			public void SetDirty()
			{
				if (this.isActive)
				{
					this.Dirty = true;
				}
				this.form.SetDirty();
			}

			// Token: 0x04004378 RID: 17272
			internal IComponent component;

			// Token: 0x04004379 RID: 17273
			internal ComponentEditorPage pageControl;

			// Token: 0x0400437A RID: 17274
			internal Control parent;

			// Token: 0x0400437B RID: 17275
			internal bool isActive;

			// Token: 0x0400437C RID: 17276
			internal bool isDirty;

			// Token: 0x0400437D RID: 17277
			private ComponentEditorForm form;
		}

		// Token: 0x0200084E RID: 2126
		internal sealed class PageSelector : TreeView
		{
			// Token: 0x0600704F RID: 28751 RVA: 0x0019AE14 File Offset: 0x00199014
			public PageSelector()
			{
				base.HotTracking = true;
				base.HideSelection = false;
				this.BackColor = SystemColors.Control;
				base.Indent = 0;
				base.LabelEdit = false;
				base.Scrollable = false;
				base.ShowLines = false;
				base.ShowPlusMinus = false;
				base.ShowRootLines = false;
				base.BorderStyle = BorderStyle.None;
				base.Indent = 0;
				base.FullRowSelect = true;
			}

			// Token: 0x17001884 RID: 6276
			// (get) Token: 0x06007050 RID: 28752 RVA: 0x0019AE80 File Offset: 0x00199080
			protected override CreateParams CreateParams
			{
				[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					CreateParams createParams = base.CreateParams;
					createParams.ExStyle |= 131072;
					return createParams;
				}
			}

			// Token: 0x06007051 RID: 28753 RVA: 0x0019AEA8 File Offset: 0x001990A8
			private void CreateDitherBrush()
			{
				short[] array = new short[] { -21846, 21845, -21846, 21845, -21846, 21845, -21846, 21845 };
				IntPtr intPtr = SafeNativeMethods.CreateBitmap(8, 8, 1, 1, array);
				if (intPtr != IntPtr.Zero)
				{
					this.hbrushDither = SafeNativeMethods.CreatePatternBrush(new HandleRef(null, intPtr));
					SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr));
				}
			}

			// Token: 0x06007052 RID: 28754 RVA: 0x0019AF00 File Offset: 0x00199100
			private void DrawTreeItem(string itemText, int imageIndex, IntPtr dc, NativeMethods.RECT rcIn, int state, int backColor, int textColor)
			{
				IntNativeMethods.SIZE size = new IntNativeMethods.SIZE();
				IntNativeMethods.RECT rect = default(IntNativeMethods.RECT);
				IntNativeMethods.RECT rect2 = new IntNativeMethods.RECT(rcIn.left, rcIn.top, rcIn.right, rcIn.bottom);
				ImageList imageList = base.ImageList;
				IntPtr intPtr = IntPtr.Zero;
				if ((state & 2) != 0)
				{
					intPtr = SafeNativeMethods.SelectObject(new HandleRef(null, dc), new HandleRef(base.Parent, base.Parent.FontHandle));
				}
				if ((state & 1) != 0 && this.hbrushDither != IntPtr.Zero)
				{
					this.FillRectDither(dc, rcIn);
					SafeNativeMethods.SetBkMode(new HandleRef(null, dc), 1);
				}
				else
				{
					SafeNativeMethods.SetBkColor(new HandleRef(null, dc), backColor);
					IntUnsafeNativeMethods.ExtTextOut(new HandleRef(null, dc), 0, 0, 6, ref rect2, null, 0, null);
				}
				IntUnsafeNativeMethods.GetTextExtentPoint32(new HandleRef(null, dc), itemText, size);
				rect.left = rect2.left + 16 + 8;
				rect.top = rect2.top + (rect2.bottom - rect2.top - size.cy >> 1);
				rect.bottom = rect.top + size.cy;
				rect.right = rect2.right;
				SafeNativeMethods.SetTextColor(new HandleRef(null, dc), textColor);
				IntUnsafeNativeMethods.DrawText(new HandleRef(null, dc), itemText, ref rect, 34820);
				SafeNativeMethods.ImageList_Draw(new HandleRef(imageList, imageList.Handle), imageIndex, new HandleRef(null, dc), 4, rect2.top + (rect2.bottom - rect2.top - 16 >> 1), 1);
				if ((state & 2) != 0)
				{
					int num = SafeNativeMethods.SetBkColor(new HandleRef(null, dc), ColorTranslator.ToWin32(SystemColors.ControlLightLight));
					rect.left = rect2.left;
					rect.top = rect2.top;
					rect.bottom = rect2.top + 1;
					rect.right = rect2.right;
					IntUnsafeNativeMethods.ExtTextOut(new HandleRef(null, dc), 0, 0, 2, ref rect, null, 0, null);
					rect.bottom = rect2.bottom;
					rect.right = rect2.left + 1;
					IntUnsafeNativeMethods.ExtTextOut(new HandleRef(null, dc), 0, 0, 2, ref rect, null, 0, null);
					SafeNativeMethods.SetBkColor(new HandleRef(null, dc), ColorTranslator.ToWin32(SystemColors.ControlDark));
					rect.left = rect2.left;
					rect.right = rect2.right;
					rect.top = rect2.bottom - 1;
					rect.bottom = rect2.bottom;
					IntUnsafeNativeMethods.ExtTextOut(new HandleRef(null, dc), 0, 0, 2, ref rect, null, 0, null);
					rect.left = rect2.right - 1;
					rect.top = rect2.top;
					IntUnsafeNativeMethods.ExtTextOut(new HandleRef(null, dc), 0, 0, 2, ref rect, null, 0, null);
					SafeNativeMethods.SetBkColor(new HandleRef(null, dc), num);
				}
				if (intPtr != IntPtr.Zero)
				{
					SafeNativeMethods.SelectObject(new HandleRef(null, dc), new HandleRef(null, intPtr));
				}
			}

			// Token: 0x06007053 RID: 28755 RVA: 0x0019B1EC File Offset: 0x001993EC
			protected override void OnHandleCreated(EventArgs e)
			{
				base.OnHandleCreated(e);
				int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4380, 0, 0);
				num += 6;
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4379, num, 0);
				if (this.hbrushDither == IntPtr.Zero)
				{
					this.CreateDitherBrush();
				}
			}

			// Token: 0x06007054 RID: 28756 RVA: 0x0019B254 File Offset: 0x00199454
			private void OnCustomDraw(ref Message m)
			{
				NativeMethods.NMTVCUSTOMDRAW nmtvcustomdraw = (NativeMethods.NMTVCUSTOMDRAW)m.GetLParam(typeof(NativeMethods.NMTVCUSTOMDRAW));
				int dwDrawStage = nmtvcustomdraw.nmcd.dwDrawStage;
				if (dwDrawStage == 1)
				{
					m.Result = (IntPtr)48;
					return;
				}
				if (dwDrawStage == 2)
				{
					m.Result = (IntPtr)4;
					return;
				}
				if (dwDrawStage != 65537)
				{
					m.Result = (IntPtr)0;
					return;
				}
				TreeNode treeNode = TreeNode.FromHandle(this, nmtvcustomdraw.nmcd.dwItemSpec);
				if (treeNode != null)
				{
					int num = 0;
					int uItemState = nmtvcustomdraw.nmcd.uItemState;
					if ((uItemState & 64) != 0 || (uItemState & 16) != 0)
					{
						num |= 2;
					}
					if ((uItemState & 1) != 0)
					{
						num |= 1;
					}
					this.DrawTreeItem(treeNode.Text, treeNode.ImageIndex, nmtvcustomdraw.nmcd.hdc, nmtvcustomdraw.nmcd.rc, num, ColorTranslator.ToWin32(SystemColors.Control), ColorTranslator.ToWin32(SystemColors.ControlText));
				}
				m.Result = (IntPtr)4;
			}

			// Token: 0x06007055 RID: 28757 RVA: 0x0019B34C File Offset: 0x0019954C
			protected override void OnHandleDestroyed(EventArgs e)
			{
				base.OnHandleDestroyed(e);
				if (!base.RecreatingHandle && this.hbrushDither != IntPtr.Zero)
				{
					SafeNativeMethods.DeleteObject(new HandleRef(this, this.hbrushDither));
					this.hbrushDither = IntPtr.Zero;
				}
			}

			// Token: 0x06007056 RID: 28758 RVA: 0x0019B38C File Offset: 0x0019958C
			private void FillRectDither(IntPtr dc, NativeMethods.RECT rc)
			{
				IntPtr intPtr = SafeNativeMethods.SelectObject(new HandleRef(null, dc), new HandleRef(this, this.hbrushDither));
				if (intPtr != IntPtr.Zero)
				{
					int num = SafeNativeMethods.SetTextColor(new HandleRef(null, dc), ColorTranslator.ToWin32(SystemColors.ControlLightLight));
					int num2 = SafeNativeMethods.SetBkColor(new HandleRef(null, dc), ColorTranslator.ToWin32(SystemColors.Control));
					SafeNativeMethods.PatBlt(new HandleRef(null, dc), rc.left, rc.top, rc.right - rc.left, rc.bottom - rc.top, 15728673);
					SafeNativeMethods.SetTextColor(new HandleRef(null, dc), num);
					SafeNativeMethods.SetBkColor(new HandleRef(null, dc), num2);
				}
			}

			// Token: 0x06007057 RID: 28759 RVA: 0x0019B444 File Offset: 0x00199644
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			protected override void WndProc(ref Message m)
			{
				if (m.Msg == 8270)
				{
					NativeMethods.NMHDR nmhdr = (NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR));
					if (nmhdr.code == -12)
					{
						this.OnCustomDraw(ref m);
						return;
					}
				}
				base.WndProc(ref m);
			}

			// Token: 0x0400437E RID: 17278
			private const int PADDING_VERT = 3;

			// Token: 0x0400437F RID: 17279
			private const int PADDING_HORZ = 4;

			// Token: 0x04004380 RID: 17280
			private const int SIZE_ICON_X = 16;

			// Token: 0x04004381 RID: 17281
			private const int SIZE_ICON_Y = 16;

			// Token: 0x04004382 RID: 17282
			private const int STATE_NORMAL = 0;

			// Token: 0x04004383 RID: 17283
			private const int STATE_SELECTED = 1;

			// Token: 0x04004384 RID: 17284
			private const int STATE_HOT = 2;

			// Token: 0x04004385 RID: 17285
			private IntPtr hbrushDither;
		}
	}
}
