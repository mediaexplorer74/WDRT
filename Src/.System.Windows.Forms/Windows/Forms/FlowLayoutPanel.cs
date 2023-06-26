using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Represents a panel that dynamically lays out its contents horizontally or vertically.</summary>
	// Token: 0x02000257 RID: 599
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ProvideProperty("FlowBreak", typeof(Control))]
	[DefaultProperty("FlowDirection")]
	[Designer("System.Windows.Forms.Design.FlowLayoutPanelDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Docking(DockingBehavior.Ask)]
	[SRDescription("DescriptionFlowLayoutPanel")]
	public class FlowLayoutPanel : Panel, IExtenderProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.FlowLayoutPanel" /> class.</summary>
		// Token: 0x060025B6 RID: 9654 RVA: 0x000AF960 File Offset: 0x000ADB60
		public FlowLayoutPanel()
		{
			this._flowLayoutSettings = FlowLayout.CreateSettings(this);
		}

		/// <summary>Gets a cached instance of the panel's layout engine.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> for the panel's contents.</returns>
		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x060025B7 RID: 9655 RVA: 0x000AF974 File Offset: 0x000ADB74
		public override LayoutEngine LayoutEngine
		{
			get
			{
				return FlowLayout.Instance;
			}
		}

		/// <summary>Gets or sets a value indicating the flow direction of the <see cref="T:System.Windows.Forms.FlowLayoutPanel" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.FlowDirection" /> values indicating the direction of consecutive placement of controls in the panel. The default is <see cref="F:System.Windows.Forms.FlowDirection.LeftToRight" />.</returns>
		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x060025B8 RID: 9656 RVA: 0x000AF97B File Offset: 0x000ADB7B
		// (set) Token: 0x060025B9 RID: 9657 RVA: 0x000AF988 File Offset: 0x000ADB88
		[SRDescription("FlowPanelFlowDirectionDescr")]
		[DefaultValue(FlowDirection.LeftToRight)]
		[SRCategory("CatLayout")]
		[Localizable(true)]
		public FlowDirection FlowDirection
		{
			get
			{
				return this._flowLayoutSettings.FlowDirection;
			}
			set
			{
				this._flowLayoutSettings.FlowDirection = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.FlowLayoutPanel" /> control should wrap its contents or let the contents be clipped.</summary>
		/// <returns>
		///   <see langword="true" /> if the contents should be wrapped; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x060025BA RID: 9658 RVA: 0x000AF996 File Offset: 0x000ADB96
		// (set) Token: 0x060025BB RID: 9659 RVA: 0x000AF9A3 File Offset: 0x000ADBA3
		[SRDescription("FlowPanelWrapContentsDescr")]
		[DefaultValue(true)]
		[SRCategory("CatLayout")]
		[Localizable(true)]
		public bool WrapContents
		{
			get
			{
				return this._flowLayoutSettings.WrapContents;
			}
			set
			{
				this._flowLayoutSettings.WrapContents = value;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IExtenderProvider.CanExtend(System.Object)" />.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to receive the extender properties.</param>
		/// <returns>
		///   <see langword="true" /> if this object can provide extender properties to the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060025BC RID: 9660 RVA: 0x000AF9B4 File Offset: 0x000ADBB4
		bool IExtenderProvider.CanExtend(object obj)
		{
			Control control = obj as Control;
			return control != null && control.Parent == this;
		}

		/// <summary>Returns a value that represents the flow-break setting of the <see cref="T:System.Windows.Forms.FlowLayoutPanel" /> control.</summary>
		/// <param name="control">The child control.</param>
		/// <returns>
		///   <see langword="true" /> if the flow break is set; otherwise, <see langword="false" />.</returns>
		// Token: 0x060025BD RID: 9661 RVA: 0x000AF9D6 File Offset: 0x000ADBD6
		[DefaultValue(false)]
		[DisplayName("FlowBreak")]
		public bool GetFlowBreak(Control control)
		{
			return this._flowLayoutSettings.GetFlowBreak(control);
		}

		/// <summary>Sets the value that represents the flow-break setting of the <see cref="T:System.Windows.Forms.FlowLayoutPanel" /> control.</summary>
		/// <param name="control">The child control.</param>
		/// <param name="value">The flow-break value to set.</param>
		// Token: 0x060025BE RID: 9662 RVA: 0x000AF9E4 File Offset: 0x000ADBE4
		[DisplayName("FlowBreak")]
		public void SetFlowBreak(Control control, bool value)
		{
			this._flowLayoutSettings.SetFlowBreak(control, value);
		}

		// Token: 0x04000FAB RID: 4011
		private FlowLayoutSettings _flowLayoutSettings;
	}
}
