using System;
using System.ComponentModel;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Collects the characteristics associated with flow layouts.</summary>
	// Token: 0x02000258 RID: 600
	[DefaultProperty("FlowDirection")]
	public class FlowLayoutSettings : LayoutSettings
	{
		// Token: 0x060025BF RID: 9663 RVA: 0x000AF9F3 File Offset: 0x000ADBF3
		internal FlowLayoutSettings(IArrangedElement owner)
			: base(owner)
		{
		}

		/// <summary>Gets the current flow layout engine.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> currently being used.</returns>
		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x060025C0 RID: 9664 RVA: 0x000AF974 File Offset: 0x000ADB74
		public override LayoutEngine LayoutEngine
		{
			get
			{
				return FlowLayout.Instance;
			}
		}

		/// <summary>Gets or sets a value indicating the flow direction of consecutive controls.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.FlowDirection" /> indicating the flow direction of consecutive controls in the container. The default is <see cref="F:System.Windows.Forms.FlowDirection.LeftToRight" />.</returns>
		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x060025C1 RID: 9665 RVA: 0x000AF9FC File Offset: 0x000ADBFC
		// (set) Token: 0x060025C2 RID: 9666 RVA: 0x000AFA09 File Offset: 0x000ADC09
		[SRDescription("FlowPanelFlowDirectionDescr")]
		[DefaultValue(FlowDirection.LeftToRight)]
		[SRCategory("CatLayout")]
		public FlowDirection FlowDirection
		{
			get
			{
				return FlowLayout.GetFlowDirection(base.Owner);
			}
			set
			{
				FlowLayout.SetFlowDirection(base.Owner, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the contents should be wrapped or clipped when they exceed the original boundaries of their container.</summary>
		/// <returns>
		///   <see langword="true" /> if the contents should be wrapped; otherwise, <see langword="false" /> if the contents should be clipped. The default is <see langword="true" />.</returns>
		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x060025C3 RID: 9667 RVA: 0x000AFA17 File Offset: 0x000ADC17
		// (set) Token: 0x060025C4 RID: 9668 RVA: 0x000AFA24 File Offset: 0x000ADC24
		[SRDescription("FlowPanelWrapContentsDescr")]
		[DefaultValue(true)]
		[SRCategory("CatLayout")]
		public bool WrapContents
		{
			get
			{
				return FlowLayout.GetWrapContents(base.Owner);
			}
			set
			{
				FlowLayout.SetWrapContents(base.Owner, value);
			}
		}

		/// <summary>Sets the value that represents the flow break setting of the control.</summary>
		/// <param name="child">The child control.</param>
		/// <param name="value">The flow break value to set.</param>
		// Token: 0x060025C5 RID: 9669 RVA: 0x000AFA34 File Offset: 0x000ADC34
		public void SetFlowBreak(object child, bool value)
		{
			IArrangedElement arrangedElement = FlowLayout.Instance.CastToArrangedElement(child);
			if (this.GetFlowBreak(child) != value)
			{
				CommonProperties.SetFlowBreak(arrangedElement, value);
			}
		}

		/// <summary>Returns a value that represents the flow break setting of the control.</summary>
		/// <param name="child">The child control.</param>
		/// <returns>
		///   <see langword="true" /> if the flow break is set; otherwise, <see langword="false" />.</returns>
		// Token: 0x060025C6 RID: 9670 RVA: 0x000AFA60 File Offset: 0x000ADC60
		public bool GetFlowBreak(object child)
		{
			IArrangedElement arrangedElement = FlowLayout.Instance.CastToArrangedElement(child);
			return CommonProperties.GetFlowBreak(arrangedElement);
		}
	}
}
