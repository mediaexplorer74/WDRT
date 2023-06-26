using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms
{
	/// <summary>Provides pop-up or online Help for controls.</summary>
	// Token: 0x02000274 RID: 628
	[ProvideProperty("HelpString", typeof(Control))]
	[ProvideProperty("HelpKeyword", typeof(Control))]
	[ProvideProperty("HelpNavigator", typeof(Control))]
	[ProvideProperty("ShowHelp", typeof(Control))]
	[ToolboxItemFilter("System.Windows.Forms")]
	[SRDescription("DescriptionHelpProvider")]
	public class HelpProvider : Component, IExtenderProvider
	{
		/// <summary>Gets or sets a value specifying the name of the Help file associated with this <see cref="T:System.Windows.Forms.HelpProvider" /> object.</summary>
		/// <returns>The name of the Help file. This can be of the form C:\path\sample.chm or /folder/file.htm.</returns>
		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x0600281B RID: 10267 RVA: 0x000BA8C5 File Offset: 0x000B8AC5
		// (set) Token: 0x0600281C RID: 10268 RVA: 0x000BA8CD File Offset: 0x000B8ACD
		[Localizable(true)]
		[DefaultValue(null)]
		[Editor("System.Windows.Forms.Design.HelpNamespaceEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SRDescription("HelpProviderHelpNamespaceDescr")]
		public virtual string HelpNamespace
		{
			get
			{
				return this.helpNamespace;
			}
			set
			{
				this.helpNamespace = value;
			}
		}

		/// <summary>Gets or sets the object that contains supplemental data about the <see cref="T:System.Windows.Forms.HelpProvider" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains data about the <see cref="T:System.Windows.Forms.HelpProvider" />.</returns>
		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x0600281D RID: 10269 RVA: 0x000BA8D6 File Offset: 0x000B8AD6
		// (set) Token: 0x0600281E RID: 10270 RVA: 0x000BA8DE File Offset: 0x000B8ADE
		[SRCategory("CatData")]
		[Localizable(false)]
		[Bindable(true)]
		[SRDescription("ControlTagDescr")]
		[DefaultValue(null)]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				return this.userData;
			}
			set
			{
				this.userData = value;
			}
		}

		/// <summary>Specifies whether this object can provide its extender properties to the specified object.</summary>
		/// <param name="target">The object that the externder properties are provided to.</param>
		/// <returns>
		///   <see langword="true" /> if this object can provide its extender properties; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600281F RID: 10271 RVA: 0x000BA8E7 File Offset: 0x000B8AE7
		public virtual bool CanExtend(object target)
		{
			return target is Control;
		}

		/// <summary>Returns the Help keyword for the specified control.</summary>
		/// <param name="ctl">A <see cref="T:System.Windows.Forms.Control" /> from which to retrieve the Help topic.</param>
		/// <returns>The Help keyword associated with this control, or <see langword="null" /> if the <see cref="T:System.Windows.Forms.HelpProvider" /> is currently configured to display the entire Help file or is configured to provide a Help string.</returns>
		// Token: 0x06002820 RID: 10272 RVA: 0x000BA8F2 File Offset: 0x000B8AF2
		[DefaultValue(null)]
		[Localizable(true)]
		[SRDescription("HelpProviderHelpKeywordDescr")]
		public virtual string GetHelpKeyword(Control ctl)
		{
			return (string)this.keywords[ctl];
		}

		/// <summary>Returns the current <see cref="T:System.Windows.Forms.HelpNavigator" /> setting for the specified control.</summary>
		/// <param name="ctl">A <see cref="T:System.Windows.Forms.Control" /> from which to retrieve the Help navigator.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.HelpNavigator" /> setting for the specified control. The default is <see cref="F:System.Windows.Forms.HelpNavigator.AssociateIndex" />.</returns>
		// Token: 0x06002821 RID: 10273 RVA: 0x000BA908 File Offset: 0x000B8B08
		[DefaultValue(HelpNavigator.AssociateIndex)]
		[Localizable(true)]
		[SRDescription("HelpProviderNavigatorDescr")]
		public virtual HelpNavigator GetHelpNavigator(Control ctl)
		{
			object obj = this.navigators[ctl];
			if (obj != null)
			{
				return (HelpNavigator)obj;
			}
			return HelpNavigator.AssociateIndex;
		}

		/// <summary>Returns the contents of the pop-up Help window for the specified control.</summary>
		/// <param name="ctl">A <see cref="T:System.Windows.Forms.Control" /> from which to retrieve the Help string.</param>
		/// <returns>The Help string associated with this control. The default is <see langword="null" />.</returns>
		// Token: 0x06002822 RID: 10274 RVA: 0x000BA931 File Offset: 0x000B8B31
		[DefaultValue(null)]
		[Localizable(true)]
		[SRDescription("HelpProviderHelpStringDescr")]
		public virtual string GetHelpString(Control ctl)
		{
			return (string)this.helpStrings[ctl];
		}

		/// <summary>Returns a value indicating whether the specified control's Help should be displayed.</summary>
		/// <param name="ctl">A <see cref="T:System.Windows.Forms.Control" /> for which Help will be displayed.</param>
		/// <returns>
		///   <see langword="true" /> if Help will be displayed for the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002823 RID: 10275 RVA: 0x000BA944 File Offset: 0x000B8B44
		[Localizable(true)]
		[SRDescription("HelpProviderShowHelpDescr")]
		public virtual bool GetShowHelp(Control ctl)
		{
			object obj = this.showHelp[ctl];
			return obj != null && (bool)obj;
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x000BA96C File Offset: 0x000B8B6C
		private void OnControlHelp(object sender, HelpEventArgs hevent)
		{
			Control control = (Control)sender;
			string helpString = this.GetHelpString(control);
			string helpKeyword = this.GetHelpKeyword(control);
			HelpNavigator helpNavigator = this.GetHelpNavigator(control);
			if (!this.GetShowHelp(control))
			{
				return;
			}
			if (Control.MouseButtons != MouseButtons.None && helpString != null && helpString.Length > 0)
			{
				Help.ShowPopup(control, helpString, hevent.MousePos);
				hevent.Handled = true;
			}
			if (!hevent.Handled && this.helpNamespace != null)
			{
				if (helpKeyword != null && helpKeyword.Length > 0)
				{
					Help.ShowHelp(control, this.helpNamespace, helpNavigator, helpKeyword);
					hevent.Handled = true;
				}
				if (!hevent.Handled)
				{
					Help.ShowHelp(control, this.helpNamespace, helpNavigator);
					hevent.Handled = true;
				}
			}
			if (!hevent.Handled && helpString != null && helpString.Length > 0)
			{
				Help.ShowPopup(control, helpString, hevent.MousePos);
				hevent.Handled = true;
			}
			if (!hevent.Handled && this.helpNamespace != null)
			{
				Help.ShowHelp(control, this.helpNamespace);
				hevent.Handled = true;
			}
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x000BAA68 File Offset: 0x000B8C68
		private void OnQueryAccessibilityHelp(object sender, QueryAccessibilityHelpEventArgs e)
		{
			Control control = (Control)sender;
			e.HelpString = this.GetHelpString(control);
			e.HelpKeyword = this.GetHelpKeyword(control);
			e.HelpNamespace = this.HelpNamespace;
		}

		/// <summary>Specifies the Help string associated with the specified control.</summary>
		/// <param name="ctl">A <see cref="T:System.Windows.Forms.Control" /> with which to associate the Help string.</param>
		/// <param name="helpString">The Help string associated with the control.</param>
		// Token: 0x06002826 RID: 10278 RVA: 0x000BAAA2 File Offset: 0x000B8CA2
		public virtual void SetHelpString(Control ctl, string helpString)
		{
			this.helpStrings[ctl] = helpString;
			if (helpString != null && helpString.Length > 0)
			{
				this.SetShowHelp(ctl, true);
			}
			this.UpdateEventBinding(ctl);
		}

		/// <summary>Specifies the keyword used to retrieve Help when the user invokes Help for the specified control.</summary>
		/// <param name="ctl">A <see cref="T:System.Windows.Forms.Control" /> that specifies the control for which to set the Help topic.</param>
		/// <param name="keyword">The Help keyword to associate with the control.</param>
		// Token: 0x06002827 RID: 10279 RVA: 0x000BAACC File Offset: 0x000B8CCC
		public virtual void SetHelpKeyword(Control ctl, string keyword)
		{
			this.keywords[ctl] = keyword;
			if (keyword != null && keyword.Length > 0)
			{
				this.SetShowHelp(ctl, true);
			}
			this.UpdateEventBinding(ctl);
		}

		/// <summary>Specifies the Help command to use when retrieving Help from the Help file for the specified control.</summary>
		/// <param name="ctl">A <see cref="T:System.Windows.Forms.Control" /> for which to set the Help keyword.</param>
		/// <param name="navigator">One of the <see cref="T:System.Windows.Forms.HelpNavigator" /> values.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value of <paramref name="navigator" /> is not one of the <see cref="T:System.Windows.Forms.HelpNavigator" /> values.</exception>
		// Token: 0x06002828 RID: 10280 RVA: 0x000BAAF8 File Offset: 0x000B8CF8
		public virtual void SetHelpNavigator(Control ctl, HelpNavigator navigator)
		{
			if (!ClientUtils.IsEnumValid(navigator, (int)navigator, -2147483647, -2147483641))
			{
				throw new InvalidEnumArgumentException("navigator", (int)navigator, typeof(HelpNavigator));
			}
			this.navigators[ctl] = navigator;
			this.SetShowHelp(ctl, true);
			this.UpdateEventBinding(ctl);
		}

		/// <summary>Specifies whether Help is displayed for the specified control.</summary>
		/// <param name="ctl">A <see cref="T:System.Windows.Forms.Control" /> for which Help is turned on or off.</param>
		/// <param name="value">
		///   <see langword="true" /> if Help displays for the control; otherwise, <see langword="false" />.</param>
		// Token: 0x06002829 RID: 10281 RVA: 0x000BAB54 File Offset: 0x000B8D54
		public virtual void SetShowHelp(Control ctl, bool value)
		{
			this.showHelp[ctl] = value;
			this.UpdateEventBinding(ctl);
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x000BAB6F File Offset: 0x000B8D6F
		internal virtual bool ShouldSerializeShowHelp(Control ctl)
		{
			return this.showHelp.ContainsKey(ctl);
		}

		/// <summary>Removes the Help associated with the specified control.</summary>
		/// <param name="ctl">The control to remove Help from.</param>
		// Token: 0x0600282B RID: 10283 RVA: 0x000BAB7D File Offset: 0x000B8D7D
		public virtual void ResetShowHelp(Control ctl)
		{
			this.showHelp.Remove(ctl);
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x000BAB8C File Offset: 0x000B8D8C
		private void UpdateEventBinding(Control ctl)
		{
			if (this.GetShowHelp(ctl) && !this.boundControls.ContainsKey(ctl))
			{
				ctl.HelpRequested += this.OnControlHelp;
				ctl.QueryAccessibilityHelp += this.OnQueryAccessibilityHelp;
				this.boundControls[ctl] = ctl;
				return;
			}
			if (!this.GetShowHelp(ctl) && this.boundControls.ContainsKey(ctl))
			{
				ctl.HelpRequested -= this.OnControlHelp;
				ctl.QueryAccessibilityHelp -= this.OnQueryAccessibilityHelp;
				this.boundControls.Remove(ctl);
			}
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Windows.Forms.HelpProvider" />.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.HelpProvider" />.</returns>
		// Token: 0x0600282D RID: 10285 RVA: 0x000BAC2C File Offset: 0x000B8E2C
		public override string ToString()
		{
			string text = base.ToString();
			return text + ", HelpNamespace: " + this.HelpNamespace;
		}

		// Token: 0x0400108E RID: 4238
		private string helpNamespace;

		// Token: 0x0400108F RID: 4239
		private Hashtable helpStrings = new Hashtable();

		// Token: 0x04001090 RID: 4240
		private Hashtable showHelp = new Hashtable();

		// Token: 0x04001091 RID: 4241
		private Hashtable boundControls = new Hashtable();

		// Token: 0x04001092 RID: 4242
		private Hashtable keywords = new Hashtable();

		// Token: 0x04001093 RID: 4243
		private Hashtable navigators = new Hashtable();

		// Token: 0x04001094 RID: 4244
		private object userData;
	}
}
