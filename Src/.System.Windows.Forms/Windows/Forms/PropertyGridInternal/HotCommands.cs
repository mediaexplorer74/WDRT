using System;
using System.ComponentModel.Design;
using System.Drawing;
using System.Text;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x02000509 RID: 1289
	internal class HotCommands : PropertyGrid.SnappableControl
	{
		// Token: 0x060054A2 RID: 21666 RVA: 0x00162BB2 File Offset: 0x00160DB2
		internal HotCommands(PropertyGrid owner)
			: base(owner)
		{
			this.Text = "Command Pane";
		}

		// Token: 0x1700144A RID: 5194
		// (get) Token: 0x060054A3 RID: 21667 RVA: 0x00162BD4 File Offset: 0x00160DD4
		// (set) Token: 0x060054A4 RID: 21668 RVA: 0x00162BDC File Offset: 0x00160DDC
		public virtual bool AllowVisible
		{
			get
			{
				return this.allowVisible;
			}
			set
			{
				if (this.allowVisible != value)
				{
					this.allowVisible = value;
					if (value && this.WouldBeVisible)
					{
						base.Visible = true;
						return;
					}
					base.Visible = false;
				}
			}
		}

		// Token: 0x060054A5 RID: 21669 RVA: 0x00162C08 File Offset: 0x00160E08
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level3)
			{
				return new HotCommandsAccessibleObject(this, this.ownerGrid);
			}
			return base.CreateAccessibilityInstance();
		}

		// Token: 0x1700144B RID: 5195
		// (get) Token: 0x060054A6 RID: 21670 RVA: 0x00162C24 File Offset: 0x00160E24
		public override Rectangle DisplayRectangle
		{
			get
			{
				Size clientSize = base.ClientSize;
				return new Rectangle(4, 4, clientSize.Width - 8, clientSize.Height - 8);
			}
		}

		// Token: 0x1700144C RID: 5196
		// (get) Token: 0x060054A7 RID: 21671 RVA: 0x00162C54 File Offset: 0x00160E54
		public LinkLabel Label
		{
			get
			{
				if (this.label == null)
				{
					this.label = new LinkLabel();
					this.label.Dock = DockStyle.Fill;
					this.label.LinkBehavior = LinkBehavior.AlwaysUnderline;
					this.label.DisabledLinkColor = SystemColors.ControlDark;
					this.label.LinkClicked += this.LinkClicked;
					base.Controls.Add(this.label);
				}
				return this.label;
			}
		}

		// Token: 0x1700144D RID: 5197
		// (get) Token: 0x060054A8 RID: 21672 RVA: 0x00162CCA File Offset: 0x00160ECA
		public virtual bool WouldBeVisible
		{
			get
			{
				return this.component != null;
			}
		}

		// Token: 0x060054A9 RID: 21673 RVA: 0x00162CD8 File Offset: 0x00160ED8
		public override int GetOptimalHeight(int width)
		{
			if (this.optimalHeight == -1)
			{
				int num = (int)(1.5 * (double)this.Font.Height);
				int num2 = 0;
				if (this.verbs != null)
				{
					num2 = this.verbs.Length;
				}
				this.optimalHeight = num2 * num + 8;
			}
			return this.optimalHeight;
		}

		// Token: 0x060054AA RID: 21674 RVA: 0x0001AE7E File Offset: 0x0001907E
		public override int SnapHeightRequest(int request)
		{
			return request;
		}

		// Token: 0x1700144E RID: 5198
		// (get) Token: 0x060054AB RID: 21675 RVA: 0x000A83A1 File Offset: 0x000A65A1
		internal override bool SupportsUiaProviders
		{
			get
			{
				return AccessibilityImprovements.Level3;
			}
		}

		// Token: 0x060054AC RID: 21676 RVA: 0x00162D2C File Offset: 0x00160F2C
		private void LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				if (e.Link.Enabled)
				{
					((DesignerVerb)e.Link.LinkData).Invoke();
				}
			}
			catch (Exception ex)
			{
				RTLAwareMessageBox.Show(this, ex.Message, SR.GetString("PBRSErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
			}
		}

		// Token: 0x060054AD RID: 21677 RVA: 0x00162D90 File Offset: 0x00160F90
		private void OnCommandChanged(object sender, EventArgs e)
		{
			this.SetupLabel();
		}

		// Token: 0x060054AE RID: 21678 RVA: 0x00162D98 File Offset: 0x00160F98
		protected override void OnGotFocus(EventArgs e)
		{
			this.Label.FocusInternal();
			this.Label.Invalidate();
		}

		// Token: 0x060054AF RID: 21679 RVA: 0x00162DB1 File Offset: 0x00160FB1
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.optimalHeight = -1;
		}

		// Token: 0x060054B0 RID: 21680 RVA: 0x00162DC4 File Offset: 0x00160FC4
		internal void SetColors(Color background, Color normalText, Color link, Color activeLink, Color visitedLink, Color disabledLink)
		{
			this.Label.BackColor = background;
			this.Label.ForeColor = normalText;
			this.Label.LinkColor = link;
			this.Label.ActiveLinkColor = activeLink;
			this.Label.VisitedLinkColor = visitedLink;
			this.Label.DisabledLinkColor = disabledLink;
		}

		// Token: 0x060054B1 RID: 21681 RVA: 0x00162E1C File Offset: 0x0016101C
		public void Select(bool forward)
		{
			this.Label.FocusInternal();
		}

		// Token: 0x060054B2 RID: 21682 RVA: 0x00162E2C File Offset: 0x0016102C
		public virtual void SetVerbs(object component, DesignerVerb[] verbs)
		{
			if (this.verbs != null)
			{
				for (int i = 0; i < this.verbs.Length; i++)
				{
					this.verbs[i].CommandChanged -= this.OnCommandChanged;
				}
				this.component = null;
				this.verbs = null;
			}
			if (component == null || verbs == null || verbs.Length == 0)
			{
				base.Visible = false;
				this.Label.Links.Clear();
				this.Label.Text = null;
			}
			else
			{
				this.component = component;
				this.verbs = verbs;
				for (int j = 0; j < verbs.Length; j++)
				{
					verbs[j].CommandChanged += this.OnCommandChanged;
				}
				if (this.allowVisible)
				{
					base.Visible = true;
				}
				this.SetupLabel();
			}
			this.optimalHeight = -1;
		}

		// Token: 0x060054B3 RID: 21683 RVA: 0x00162EF8 File Offset: 0x001610F8
		private void SetupLabel()
		{
			this.Label.Links.Clear();
			StringBuilder stringBuilder = new StringBuilder();
			Point[] array = new Point[this.verbs.Length];
			int num = 0;
			bool flag = true;
			for (int i = 0; i < this.verbs.Length; i++)
			{
				if (this.verbs[i].Visible && this.verbs[i].Supported)
				{
					if (!flag)
					{
						stringBuilder.Append(Application.CurrentCulture.TextInfo.ListSeparator);
						stringBuilder.Append(" ");
						num += 2;
					}
					string text = this.verbs[i].Text;
					array[i] = new Point(num, text.Length);
					stringBuilder.Append(text);
					num += text.Length;
					flag = false;
				}
			}
			this.Label.Text = stringBuilder.ToString();
			for (int j = 0; j < this.verbs.Length; j++)
			{
				if (this.verbs[j].Visible && this.verbs[j].Supported)
				{
					LinkLabel.Link link = this.Label.Links.Add(array[j].X, array[j].Y, this.verbs[j]);
					if (!this.verbs[j].Enabled)
					{
						link.Enabled = false;
					}
				}
			}
		}

		// Token: 0x04003715 RID: 14101
		private object component;

		// Token: 0x04003716 RID: 14102
		private DesignerVerb[] verbs;

		// Token: 0x04003717 RID: 14103
		private LinkLabel label;

		// Token: 0x04003718 RID: 14104
		private bool allowVisible = true;

		// Token: 0x04003719 RID: 14105
		private int optimalHeight = -1;
	}
}
