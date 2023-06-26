using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Represents a common dialog box that displays available colors along with controls that enable the user to define custom colors.</summary>
	// Token: 0x02000151 RID: 337
	[DefaultProperty("Color")]
	[SRDescription("DescriptionColorDialog")]
	public class ColorDialog : CommonDialog
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColorDialog" /> class.</summary>
		// Token: 0x06000D7E RID: 3454 RVA: 0x00026F03 File Offset: 0x00025103
		public ColorDialog()
		{
			this.customColors = new int[16];
			this.Reset();
		}

		/// <summary>Gets or sets a value indicating whether the user can use the dialog box to define custom colors.</summary>
		/// <returns>
		///   <see langword="true" /> if the user can define custom colors; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000D7F RID: 3455 RVA: 0x00026F1E File Offset: 0x0002511E
		// (set) Token: 0x06000D80 RID: 3456 RVA: 0x00026F2A File Offset: 0x0002512A
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("CDallowFullOpenDescr")]
		public virtual bool AllowFullOpen
		{
			get
			{
				return !this.GetOption(4);
			}
			set
			{
				this.SetOption(4, !value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box displays all available colors in the set of basic colors.</summary>
		/// <returns>
		///   <see langword="true" /> if the dialog box displays all available colors in the set of basic colors; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x00026F37 File Offset: 0x00025137
		// (set) Token: 0x06000D82 RID: 3458 RVA: 0x00026F44 File Offset: 0x00025144
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("CDanyColorDescr")]
		public virtual bool AnyColor
		{
			get
			{
				return this.GetOption(256);
			}
			set
			{
				this.SetOption(256, value);
			}
		}

		/// <summary>Gets or sets the color selected by the user.</summary>
		/// <returns>The color selected by the user. If a color is not selected, the default value is black.</returns>
		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000D83 RID: 3459 RVA: 0x00026F52 File Offset: 0x00025152
		// (set) Token: 0x06000D84 RID: 3460 RVA: 0x00026F5A File Offset: 0x0002515A
		[SRCategory("CatData")]
		[SRDescription("CDcolorDescr")]
		public Color Color
		{
			get
			{
				return this.color;
			}
			set
			{
				if (!value.IsEmpty)
				{
					this.color = value;
					return;
				}
				this.color = Color.Black;
			}
		}

		/// <summary>Gets or sets the set of custom colors shown in the dialog box.</summary>
		/// <returns>A set of custom colors shown by the dialog box. The default value is <see langword="null" />.</returns>
		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x00026F78 File Offset: 0x00025178
		// (set) Token: 0x06000D86 RID: 3462 RVA: 0x00026F8C File Offset: 0x0002518C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("CDcustomColorsDescr")]
		public int[] CustomColors
		{
			get
			{
				return (int[])this.customColors.Clone();
			}
			set
			{
				int num = ((value == null) ? 0 : Math.Min(value.Length, 16));
				if (num > 0)
				{
					Array.Copy(value, 0, this.customColors, 0, num);
				}
				for (int i = num; i < 16; i++)
				{
					this.customColors[i] = 16777215;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the controls used to create custom colors are visible when the dialog box is opened</summary>
		/// <returns>
		///   <see langword="true" /> if the custom color controls are available when the dialog box is opened; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x00026FD7 File Offset: 0x000251D7
		// (set) Token: 0x06000D88 RID: 3464 RVA: 0x00026FE0 File Offset: 0x000251E0
		[SRCategory("CatAppearance")]
		[DefaultValue(false)]
		[SRDescription("CDfullOpenDescr")]
		public virtual bool FullOpen
		{
			get
			{
				return this.GetOption(2);
			}
			set
			{
				this.SetOption(2, value);
			}
		}

		/// <summary>Gets the underlying window instance handle (HINSTANCE).</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> that contains the HINSTANCE value of the window handle.</returns>
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x00026FEA File Offset: 0x000251EA
		protected virtual IntPtr Instance
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return UnsafeNativeMethods.GetModuleHandle(null);
			}
		}

		/// <summary>Gets values to initialize the <see cref="T:System.Windows.Forms.ColorDialog" />.</summary>
		/// <returns>A bitwise combination of internal values that initializes the <see cref="T:System.Windows.Forms.ColorDialog" />.</returns>
		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x00026FF2 File Offset: 0x000251F2
		protected virtual int Options
		{
			get
			{
				return this.options;
			}
		}

		/// <summary>Gets or sets a value indicating whether a Help button appears in the color dialog box.</summary>
		/// <returns>
		///   <see langword="true" /> if the Help button is shown in the dialog box; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x00026FFA File Offset: 0x000251FA
		// (set) Token: 0x06000D8C RID: 3468 RVA: 0x00027003 File Offset: 0x00025203
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("CDshowHelpDescr")]
		public virtual bool ShowHelp
		{
			get
			{
				return this.GetOption(8);
			}
			set
			{
				this.SetOption(8, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box will restrict users to selecting solid colors only.</summary>
		/// <returns>
		///   <see langword="true" /> if users can select only solid colors; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000D8D RID: 3469 RVA: 0x0002700D File Offset: 0x0002520D
		// (set) Token: 0x06000D8E RID: 3470 RVA: 0x0002701A File Offset: 0x0002521A
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("CDsolidColorOnlyDescr")]
		public virtual bool SolidColorOnly
		{
			get
			{
				return this.GetOption(128);
			}
			set
			{
				this.SetOption(128, value);
			}
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00027028 File Offset: 0x00025228
		private bool GetOption(int option)
		{
			return (this.options & option) != 0;
		}

		/// <summary>Resets all options to their default values, the last selected color to black, and the custom colors to their default values.</summary>
		// Token: 0x06000D90 RID: 3472 RVA: 0x00027035 File Offset: 0x00025235
		public override void Reset()
		{
			this.options = 0;
			this.color = Color.Black;
			this.CustomColors = null;
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00027050 File Offset: 0x00025250
		private void ResetColor()
		{
			this.Color = Color.Black;
		}

		/// <summary>When overridden in a derived class, specifies a common dialog box.</summary>
		/// <param name="hwndOwner">A value that represents the window handle of the owner window for the common dialog box.</param>
		/// <returns>
		///   <see langword="true" /> if the dialog box was successfully run; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000D92 RID: 3474 RVA: 0x00027060 File Offset: 0x00025260
		protected override bool RunDialog(IntPtr hwndOwner)
		{
			NativeMethods.WndProc wndProc = new NativeMethods.WndProc(this.HookProc);
			NativeMethods.CHOOSECOLOR choosecolor = new NativeMethods.CHOOSECOLOR();
			IntPtr intPtr = Marshal.AllocCoTaskMem(64);
			bool flag;
			try
			{
				Marshal.Copy(this.customColors, 0, intPtr, 16);
				choosecolor.hwndOwner = hwndOwner;
				choosecolor.hInstance = this.Instance;
				choosecolor.rgbResult = ColorTranslator.ToWin32(this.color);
				choosecolor.lpCustColors = intPtr;
				int num = this.Options | 17;
				if (!this.AllowFullOpen)
				{
					num &= -3;
				}
				choosecolor.Flags = num;
				choosecolor.lpfnHook = wndProc;
				if (!SafeNativeMethods.ChooseColor(choosecolor))
				{
					flag = false;
				}
				else
				{
					if (choosecolor.rgbResult != ColorTranslator.ToWin32(this.color))
					{
						this.color = ColorTranslator.FromOle(choosecolor.rgbResult);
					}
					Marshal.Copy(intPtr, this.customColors, 0, 16);
					flag = true;
				}
			}
			finally
			{
				Marshal.FreeCoTaskMem(intPtr);
			}
			return flag;
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00027148 File Offset: 0x00025348
		private void SetOption(int option, bool value)
		{
			if (value)
			{
				this.options |= option;
				return;
			}
			this.options &= ~option;
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0002716C File Offset: 0x0002536C
		private bool ShouldSerializeColor()
		{
			return !this.Color.Equals(Color.Black);
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.ColorDialog" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Windows.Forms.ColorDialog" />.</returns>
		// Token: 0x06000D95 RID: 3477 RVA: 0x0002719C File Offset: 0x0002539C
		public override string ToString()
		{
			string text = base.ToString();
			return text + ",  Color: " + this.Color.ToString();
		}

		// Token: 0x04000787 RID: 1927
		private int options;

		// Token: 0x04000788 RID: 1928
		private int[] customColors;

		// Token: 0x04000789 RID: 1929
		private Color color;
	}
}
