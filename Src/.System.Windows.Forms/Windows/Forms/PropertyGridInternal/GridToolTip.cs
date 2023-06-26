using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x02000508 RID: 1288
	internal class GridToolTip : Control
	{
		// Token: 0x06005496 RID: 21654 RVA: 0x00162688 File Offset: 0x00160888
		internal GridToolTip(Control[] controls)
		{
			this.controls = controls;
			base.SetStyle(ControlStyles.UserPaint, false);
			this.Font = controls[0].Font;
			this.toolInfos = new NativeMethods.TOOLINFO_T[controls.Length];
			for (int i = 0; i < controls.Length; i++)
			{
				controls[i].HandleCreated += this.OnControlCreateHandle;
				controls[i].HandleDestroyed += this.OnControlDestroyHandle;
				if (controls[i].IsHandleCreated)
				{
					this.SetupToolTip(controls[i]);
				}
			}
		}

		// Token: 0x17001448 RID: 5192
		// (get) Token: 0x06005497 RID: 21655 RVA: 0x00162725 File Offset: 0x00160925
		// (set) Token: 0x06005498 RID: 21656 RVA: 0x00162730 File Offset: 0x00160930
		public string ToolTip
		{
			get
			{
				return this.toolTipText;
			}
			set
			{
				if (base.IsHandleCreated || !string.IsNullOrEmpty(value))
				{
					this.Reset();
				}
				if (value != null && value.Length > this.maximumToolTipLength)
				{
					value = value.Substring(0, this.maximumToolTipLength) + "...";
				}
				this.toolTipText = value;
				if (base.IsHandleCreated)
				{
					bool visible = base.Visible;
					if (visible)
					{
						base.Visible = false;
					}
					if (value == null || value.Length == 0)
					{
						this.dontShow = true;
						value = "";
					}
					else
					{
						this.dontShow = false;
					}
					for (int i = 0; i < this.controls.Length; i++)
					{
						UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.TTM_UPDATETIPTEXT, 0, this.GetTOOLINFO(this.controls[i]));
					}
					if (visible && !this.dontShow)
					{
						base.Visible = true;
					}
				}
			}
		}

		// Token: 0x17001449 RID: 5193
		// (get) Token: 0x06005499 RID: 21657 RVA: 0x0016280C File Offset: 0x00160A0C
		protected override CreateParams CreateParams
		{
			get
			{
				SafeNativeMethods.InitCommonControlsEx(new NativeMethods.INITCOMMONCONTROLSEX
				{
					dwICC = 8
				});
				CreateParams createParams = new CreateParams();
				createParams.Parent = IntPtr.Zero;
				createParams.ClassName = "tooltips_class32";
				createParams.Style |= 3;
				createParams.ExStyle = 0;
				createParams.Caption = this.ToolTip;
				return createParams;
			}
		}

		// Token: 0x0600549A RID: 21658 RVA: 0x0016286C File Offset: 0x00160A6C
		private NativeMethods.TOOLINFO_T GetTOOLINFO(Control c)
		{
			int num = Array.IndexOf<Control>(this.controls, c);
			if (this.toolInfos[num] == null)
			{
				this.toolInfos[num] = new NativeMethods.TOOLINFO_T();
				this.toolInfos[num].cbSize = Marshal.SizeOf(typeof(NativeMethods.TOOLINFO_T));
				this.toolInfos[num].uFlags |= 273;
			}
			this.toolInfos[num].lpszText = this.toolTipText;
			this.toolInfos[num].hwnd = c.Handle;
			this.toolInfos[num].uId = c.Handle;
			return this.toolInfos[num];
		}

		// Token: 0x0600549B RID: 21659 RVA: 0x00162913 File Offset: 0x00160B13
		private void OnControlCreateHandle(object sender, EventArgs e)
		{
			this.SetupToolTip((Control)sender);
		}

		// Token: 0x0600549C RID: 21660 RVA: 0x00162921 File Offset: 0x00160B21
		private void OnControlDestroyHandle(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.TTM_DELTOOL, 0, this.GetTOOLINFO((Control)sender));
			}
		}

		// Token: 0x0600549D RID: 21661 RVA: 0x00162950 File Offset: 0x00160B50
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			for (int i = 0; i < this.controls.Length; i++)
			{
				if (this.controls[i].IsHandleCreated)
				{
					this.SetupToolTip(this.controls[i]);
				}
			}
		}

		// Token: 0x0600549E RID: 21662 RVA: 0x00162994 File Offset: 0x00160B94
		internal void PositionToolTip(Control parent, Rectangle itemRect)
		{
			if (this._positioned && DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				return;
			}
			base.Visible = false;
			NativeMethods.RECT rect = NativeMethods.RECT.FromXYWH(itemRect.X, itemRect.Y, itemRect.Width, itemRect.Height);
			base.SendMessage(1055, 1, ref rect);
			Point point = parent.PointToScreen(new Point(rect.left, rect.top));
			base.Location = point;
			int num = base.Location.X + base.Size.Width - SystemInformation.VirtualScreen.Width;
			if (num > 0)
			{
				point.X -= num;
				base.Location = point;
			}
			base.Visible = true;
			this._positioned = true;
		}

		// Token: 0x0600549F RID: 21663 RVA: 0x00162A60 File Offset: 0x00160C60
		private void SetupToolTip(Control c)
		{
			if (base.IsHandleCreated)
			{
				SafeNativeMethods.SetWindowPos(new HandleRef(this, base.Handle), NativeMethods.HWND_TOPMOST, 0, 0, 0, 0, 19);
				(int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.TTM_ADDTOOL, 0, this.GetTOOLINFO(c));
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1048, 0, SystemInformation.MaxWindowTrackSize.Width);
			}
		}

		// Token: 0x060054A0 RID: 21664 RVA: 0x00162ADC File Offset: 0x00160CDC
		public void Reset()
		{
			string toolTip = this.ToolTip;
			this.toolTipText = "";
			for (int i = 0; i < this.controls.Length; i++)
			{
				(int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.TTM_UPDATETIPTEXT, 0, this.GetTOOLINFO(this.controls[i]));
			}
			this.toolTipText = toolTip;
			base.SendMessage(1053, 0, 0);
			this._positioned = false;
		}

		// Token: 0x060054A1 RID: 21665 RVA: 0x00162B58 File Offset: 0x00160D58
		protected override void WndProc(ref Message msg)
		{
			int msg2 = msg.Msg;
			if (msg2 != 24)
			{
				if (msg2 == 132)
				{
					msg.Result = (IntPtr)(-1);
					return;
				}
			}
			else if ((int)(long)msg.WParam != 0 && this.dontShow)
			{
				msg.WParam = IntPtr.Zero;
			}
			base.WndProc(ref msg);
		}

		// Token: 0x0400370E RID: 14094
		private Control[] controls;

		// Token: 0x0400370F RID: 14095
		private string toolTipText;

		// Token: 0x04003710 RID: 14096
		private NativeMethods.TOOLINFO_T[] toolInfos;

		// Token: 0x04003711 RID: 14097
		private bool dontShow;

		// Token: 0x04003712 RID: 14098
		private Point lastMouseMove = Point.Empty;

		// Token: 0x04003713 RID: 14099
		private int maximumToolTipLength = 1000;

		// Token: 0x04003714 RID: 14100
		private bool _positioned;
	}
}
