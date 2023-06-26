using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms.Design
{
	/// <summary>Provides a base implementation for a <see cref="T:System.Windows.Forms.Design.ComponentEditorPage" />.</summary>
	// Token: 0x02000487 RID: 1159
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class ComponentEditorPage : Panel
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.ComponentEditorPage" /> class.</summary>
		// Token: 0x06004DE8 RID: 19944 RVA: 0x00142152 File Offset: 0x00140352
		public ComponentEditorPage()
		{
			this.commitOnDeactivate = false;
			this.firstActivate = true;
			this.loadRequired = false;
			this.loading = 0;
			base.Visible = false;
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>
		///   <see langword="true" /> if enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700132A RID: 4906
		// (get) Token: 0x06004DE9 RID: 19945 RVA: 0x000FFC09 File Offset: 0x000FDE09
		// (set) Token: 0x06004DEA RID: 19946 RVA: 0x000FFC11 File Offset: 0x000FDE11
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

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x1400040A RID: 1034
		// (add) Token: 0x06004DEB RID: 19947 RVA: 0x000FFC1A File Offset: 0x000FDE1A
		// (remove) Token: 0x06004DEC RID: 19948 RVA: 0x000FFC23 File Offset: 0x000FDE23
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

		/// <summary>Gets or sets the page site.</summary>
		/// <returns>The page site.</returns>
		// Token: 0x1700132B RID: 4907
		// (get) Token: 0x06004DED RID: 19949 RVA: 0x0014217D File Offset: 0x0014037D
		// (set) Token: 0x06004DEE RID: 19950 RVA: 0x00142185 File Offset: 0x00140385
		protected IComponentEditorPageSite PageSite
		{
			get
			{
				return this.pageSite;
			}
			set
			{
				this.pageSite = value;
			}
		}

		/// <summary>Gets or sets the component to edit.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IComponent" /> this page allows you to edit.</returns>
		// Token: 0x1700132C RID: 4908
		// (get) Token: 0x06004DEF RID: 19951 RVA: 0x0014218E File Offset: 0x0014038E
		// (set) Token: 0x06004DF0 RID: 19952 RVA: 0x00142196 File Offset: 0x00140396
		protected IComponent Component
		{
			get
			{
				return this.component;
			}
			set
			{
				this.component = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the page is being activated for the first time.</summary>
		/// <returns>
		///   <see langword="true" /> if the page has not previously been activated; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700132D RID: 4909
		// (get) Token: 0x06004DF1 RID: 19953 RVA: 0x0014219F File Offset: 0x0014039F
		// (set) Token: 0x06004DF2 RID: 19954 RVA: 0x001421A7 File Offset: 0x001403A7
		protected bool FirstActivate
		{
			get
			{
				return this.firstActivate;
			}
			set
			{
				this.firstActivate = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a component must be loaded before editing can occur.</summary>
		/// <returns>
		///   <see langword="true" /> if a component must be loaded before editing can occur; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700132E RID: 4910
		// (get) Token: 0x06004DF3 RID: 19955 RVA: 0x001421B0 File Offset: 0x001403B0
		// (set) Token: 0x06004DF4 RID: 19956 RVA: 0x001421B8 File Offset: 0x001403B8
		protected bool LoadRequired
		{
			get
			{
				return this.loadRequired;
			}
			set
			{
				this.loadRequired = value;
			}
		}

		/// <summary>Indicates how many load dependencies remain until loading has been completed.</summary>
		/// <returns>The number of remaining load dependencies.</returns>
		// Token: 0x1700132F RID: 4911
		// (get) Token: 0x06004DF5 RID: 19957 RVA: 0x001421C1 File Offset: 0x001403C1
		// (set) Token: 0x06004DF6 RID: 19958 RVA: 0x001421C9 File Offset: 0x001403C9
		protected int Loading
		{
			get
			{
				return this.loading;
			}
			set
			{
				this.loading = value;
			}
		}

		/// <summary>Specifies whether the editor should apply its changes before it is deactivated.</summary>
		/// <returns>
		///   <see langword="true" /> if the editor should apply its changes; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001330 RID: 4912
		// (get) Token: 0x06004DF7 RID: 19959 RVA: 0x001421D2 File Offset: 0x001403D2
		// (set) Token: 0x06004DF8 RID: 19960 RVA: 0x001421DA File Offset: 0x001403DA
		public bool CommitOnDeactivate
		{
			get
			{
				return this.commitOnDeactivate;
			}
			set
			{
				this.commitOnDeactivate = value;
			}
		}

		/// <summary>Gets the creation parameters for the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that indicates the creation parameters for the control.</returns>
		// Token: 0x17001331 RID: 4913
		// (get) Token: 0x06004DF9 RID: 19961 RVA: 0x001421E4 File Offset: 0x001403E4
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.Style &= -12582913;
				return createParams;
			}
		}

		/// <summary>Gets or sets the icon for the page.</summary>
		/// <returns>An <see cref="T:System.Drawing.Icon" /> used to represent the page.</returns>
		// Token: 0x17001332 RID: 4914
		// (get) Token: 0x06004DFA RID: 19962 RVA: 0x0014220B File Offset: 0x0014040B
		// (set) Token: 0x06004DFB RID: 19963 RVA: 0x00142235 File Offset: 0x00140435
		public Icon Icon
		{
			get
			{
				if (this.icon == null)
				{
					this.icon = new Icon(typeof(ComponentEditorPage), "ComponentEditorPage.ico");
				}
				return this.icon;
			}
			set
			{
				this.icon = value;
			}
		}

		/// <summary>Gets the title of the page.</summary>
		/// <returns>The title of the page.</returns>
		// Token: 0x17001333 RID: 4915
		// (get) Token: 0x06004DFC RID: 19964 RVA: 0x0010714C File Offset: 0x0010534C
		public virtual string Title
		{
			get
			{
				return base.Text;
			}
		}

		/// <summary>Activates and displays the page.</summary>
		// Token: 0x06004DFD RID: 19965 RVA: 0x0014223E File Offset: 0x0014043E
		public virtual void Activate()
		{
			if (this.loadRequired)
			{
				this.EnterLoadingMode();
				this.LoadComponent();
				this.ExitLoadingMode();
				this.loadRequired = false;
			}
			base.Visible = true;
			this.firstActivate = false;
		}

		/// <summary>Applies changes to all the components being edited.</summary>
		// Token: 0x06004DFE RID: 19966 RVA: 0x0014226F File Offset: 0x0014046F
		public virtual void ApplyChanges()
		{
			this.SaveComponent();
		}

		/// <summary>Deactivates and hides the page.</summary>
		// Token: 0x06004DFF RID: 19967 RVA: 0x00034465 File Offset: 0x00032665
		public virtual void Deactivate()
		{
			base.Visible = false;
		}

		/// <summary>Increments the loading counter.</summary>
		// Token: 0x06004E00 RID: 19968 RVA: 0x00142277 File Offset: 0x00140477
		protected void EnterLoadingMode()
		{
			this.loading++;
		}

		/// <summary>Decrements the loading counter.</summary>
		// Token: 0x06004E01 RID: 19969 RVA: 0x00142287 File Offset: 0x00140487
		protected void ExitLoadingMode()
		{
			this.loading--;
		}

		/// <summary>Gets the control that represents the window for this page.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that represents the window for this page.</returns>
		// Token: 0x06004E02 RID: 19970 RVA: 0x00006A49 File Offset: 0x00004C49
		public virtual Control GetControl()
		{
			return this;
		}

		/// <summary>Gets the component that is to be edited.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IComponent" /> that is to be edited.</returns>
		// Token: 0x06004E03 RID: 19971 RVA: 0x0014218E File Offset: 0x0014038E
		protected IComponent GetSelectedComponent()
		{
			return this.component;
		}

		/// <summary>Processes messages that could be handled by the page.</summary>
		/// <param name="msg">The message to process.</param>
		/// <returns>
		///   <see langword="true" /> if the page processed the message; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004E04 RID: 19972 RVA: 0x00142297 File Offset: 0x00140497
		public virtual bool IsPageMessage(ref Message msg)
		{
			return this.PreProcessMessage(ref msg);
		}

		/// <summary>Gets a value indicating whether the page is being activated for the first time.</summary>
		/// <returns>
		///   <see langword="true" /> if this is the first time the page is being activated; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004E05 RID: 19973 RVA: 0x0014219F File Offset: 0x0014039F
		protected bool IsFirstActivate()
		{
			return this.firstActivate;
		}

		/// <summary>Gets a value indicating whether the page is being loaded.</summary>
		/// <returns>
		///   <see langword="true" /> if the page is being loaded; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004E06 RID: 19974 RVA: 0x001422A0 File Offset: 0x001404A0
		protected bool IsLoading()
		{
			return this.loading != 0;
		}

		/// <summary>Loads the component into the page user interface (UI).</summary>
		// Token: 0x06004E07 RID: 19975
		protected abstract void LoadComponent();

		/// <summary>Called when the page and any sibling pages have applied their changes.</summary>
		// Token: 0x06004E08 RID: 19976 RVA: 0x001422AB File Offset: 0x001404AB
		public virtual void OnApplyComplete()
		{
			this.ReloadComponent();
		}

		/// <summary>Reloads the component for the page.</summary>
		// Token: 0x06004E09 RID: 19977 RVA: 0x001422B3 File Offset: 0x001404B3
		protected virtual void ReloadComponent()
		{
			if (!base.Visible)
			{
				this.loadRequired = true;
			}
		}

		/// <summary>Saves the component from the page user interface (UI).</summary>
		// Token: 0x06004E0A RID: 19978
		protected abstract void SaveComponent();

		/// <summary>Sets the page as changed since the last load or save.</summary>
		// Token: 0x06004E0B RID: 19979 RVA: 0x001422C4 File Offset: 0x001404C4
		protected virtual void SetDirty()
		{
			if (!this.IsLoading())
			{
				this.pageSite.SetDirty();
			}
		}

		/// <summary>Sets the component to be edited.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to be edited.</param>
		// Token: 0x06004E0C RID: 19980 RVA: 0x001422D9 File Offset: 0x001404D9
		public virtual void SetComponent(IComponent component)
		{
			this.component = component;
			this.loadRequired = true;
		}

		/// <summary>Sets the site for this page.</summary>
		/// <param name="site">The site for this page.</param>
		// Token: 0x06004E0D RID: 19981 RVA: 0x001422E9 File Offset: 0x001404E9
		public virtual void SetSite(IComponentEditorPageSite site)
		{
			this.pageSite = site;
			this.pageSite.GetControl().Controls.Add(this);
		}

		/// <summary>Shows Help information if the page supports Help information.</summary>
		// Token: 0x06004E0E RID: 19982 RVA: 0x000070A6 File Offset: 0x000052A6
		public virtual void ShowHelp()
		{
		}

		/// <summary>Gets a value indicating whether the editor supports Help.</summary>
		/// <returns>
		///   <see langword="true" /> if the editor supports Help; otherwise, <see langword="false" />. The default implementation returns <see langword="false" />.</returns>
		// Token: 0x06004E0F RID: 19983 RVA: 0x0001180C File Offset: 0x0000FA0C
		public virtual bool SupportsHelp()
		{
			return false;
		}

		// Token: 0x040033E1 RID: 13281
		private IComponentEditorPageSite pageSite;

		// Token: 0x040033E2 RID: 13282
		private IComponent component;

		// Token: 0x040033E3 RID: 13283
		private bool firstActivate;

		// Token: 0x040033E4 RID: 13284
		private bool loadRequired;

		// Token: 0x040033E5 RID: 13285
		private int loading;

		// Token: 0x040033E6 RID: 13286
		private Icon icon;

		// Token: 0x040033E7 RID: 13287
		private bool commitOnDeactivate;
	}
}
