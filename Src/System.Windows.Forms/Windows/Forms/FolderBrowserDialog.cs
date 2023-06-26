using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Windows.Forms
{
	/// <summary>Prompts the user to select a folder. This class cannot be inherited.</summary>
	// Token: 0x02000259 RID: 601
	[DefaultEvent("HelpRequest")]
	[DefaultProperty("SelectedPath")]
	[Designer("System.Windows.Forms.Design.FolderBrowserDialogDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionFolderBrowserDialog")]
	public sealed class FolderBrowserDialog : CommonDialog
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.FolderBrowserDialog" /> class.</summary>
		// Token: 0x060025C7 RID: 9671 RVA: 0x000AFA7F File Offset: 0x000ADC7F
		public FolderBrowserDialog()
		{
			this.Reset();
		}

		/// <summary>Occurs when the user clicks the Help button on the dialog box.</summary>
		// Token: 0x1400019B RID: 411
		// (add) Token: 0x060025C8 RID: 9672 RVA: 0x000AFA8D File Offset: 0x000ADC8D
		// (remove) Token: 0x060025C9 RID: 9673 RVA: 0x000AFA96 File Offset: 0x000ADC96
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler HelpRequest
		{
			add
			{
				base.HelpRequest += value;
			}
			remove
			{
				base.HelpRequest -= value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the New Folder button appears in the folder browser dialog box.</summary>
		/// <returns>
		///   <see langword="true" /> if the New Folder button is shown in the dialog box; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x060025CA RID: 9674 RVA: 0x000AFA9F File Offset: 0x000ADC9F
		// (set) Token: 0x060025CB RID: 9675 RVA: 0x000AFAA7 File Offset: 0x000ADCA7
		[Browsable(true)]
		[DefaultValue(true)]
		[Localizable(false)]
		[SRCategory("CatFolderBrowsing")]
		[SRDescription("FolderBrowserDialogShowNewFolderButton")]
		public bool ShowNewFolderButton
		{
			get
			{
				return this.showNewFolderButton;
			}
			set
			{
				this.showNewFolderButton = value;
			}
		}

		/// <summary>Gets or sets the path selected by the user.</summary>
		/// <returns>The path of the folder first selected in the dialog box or the last folder selected by the user. The default is an empty string ("").</returns>
		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x060025CC RID: 9676 RVA: 0x000AFAB0 File Offset: 0x000ADCB0
		// (set) Token: 0x060025CD RID: 9677 RVA: 0x000AFAED File Offset: 0x000ADCED
		[Browsable(true)]
		[DefaultValue("")]
		[Editor("System.Windows.Forms.Design.SelectedPathEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[SRCategory("CatFolderBrowsing")]
		[SRDescription("FolderBrowserDialogSelectedPath")]
		public string SelectedPath
		{
			get
			{
				if (this.selectedPath == null || this.selectedPath.Length == 0)
				{
					return this.selectedPath;
				}
				if (this.selectedPathNeedsCheck)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.selectedPath).Demand();
				}
				return this.selectedPath;
			}
			set
			{
				this.selectedPath = ((value == null) ? string.Empty : value);
				this.selectedPathNeedsCheck = false;
			}
		}

		/// <summary>Gets or sets the root folder where the browsing starts from.</summary>
		/// <returns>One of the <see cref="T:System.Environment.SpecialFolder" /> values. The default is <see langword="Desktop" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Environment.SpecialFolder" /> values.</exception>
		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x060025CE RID: 9678 RVA: 0x000AFB07 File Offset: 0x000ADD07
		// (set) Token: 0x060025CF RID: 9679 RVA: 0x000AFB0F File Offset: 0x000ADD0F
		[Browsable(true)]
		[DefaultValue(Environment.SpecialFolder.Desktop)]
		[Localizable(false)]
		[SRCategory("CatFolderBrowsing")]
		[SRDescription("FolderBrowserDialogRootFolder")]
		[TypeConverter(typeof(SpecialFolderEnumConverter))]
		public Environment.SpecialFolder RootFolder
		{
			get
			{
				return this.rootFolder;
			}
			set
			{
				if (!Enum.IsDefined(typeof(Environment.SpecialFolder), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(Environment.SpecialFolder));
				}
				this.rootFolder = value;
			}
		}

		/// <summary>Gets or sets the descriptive text displayed above the tree view control in the dialog box.</summary>
		/// <returns>The description to display. The default is an empty string ("").</returns>
		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x060025D0 RID: 9680 RVA: 0x000AFB45 File Offset: 0x000ADD45
		// (set) Token: 0x060025D1 RID: 9681 RVA: 0x000AFB4D File Offset: 0x000ADD4D
		[Browsable(true)]
		[DefaultValue("")]
		[Localizable(true)]
		[SRCategory("CatFolderBrowsing")]
		[SRDescription("FolderBrowserDialogDescription")]
		public string Description
		{
			get
			{
				return this.descriptionText;
			}
			set
			{
				this.descriptionText = ((value == null) ? string.Empty : value);
			}
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x000AFB60 File Offset: 0x000ADD60
		private static UnsafeNativeMethods.IMalloc GetSHMalloc()
		{
			UnsafeNativeMethods.IMalloc[] array = new UnsafeNativeMethods.IMalloc[1];
			UnsafeNativeMethods.Shell32.SHGetMalloc(array);
			return array[0];
		}

		/// <summary>Resets properties to their default values.</summary>
		// Token: 0x060025D3 RID: 9683 RVA: 0x000AFB7E File Offset: 0x000ADD7E
		public override void Reset()
		{
			this.rootFolder = Environment.SpecialFolder.Desktop;
			this.descriptionText = string.Empty;
			this.selectedPath = string.Empty;
			this.selectedPathNeedsCheck = false;
			this.showNewFolderButton = true;
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x000AFBAC File Offset: 0x000ADDAC
		protected override bool RunDialog(IntPtr hWndOwner)
		{
			IntPtr zero = IntPtr.Zero;
			bool flag = false;
			UnsafeNativeMethods.Shell32.SHGetSpecialFolderLocation(hWndOwner, (int)this.rootFolder, ref zero);
			if (zero == IntPtr.Zero)
			{
				UnsafeNativeMethods.Shell32.SHGetSpecialFolderLocation(hWndOwner, 0, ref zero);
				if (zero == IntPtr.Zero)
				{
					throw new InvalidOperationException(SR.GetString("FolderBrowserDialogNoRootFolder"));
				}
			}
			int num = 64;
			if (!this.showNewFolderButton)
			{
				num += 512;
			}
			if (Control.CheckForIllegalCrossThreadCalls && Application.OleRequired() != ApartmentState.STA)
			{
				throw new ThreadStateException(SR.GetString("DebuggingExceptionOnly", new object[] { SR.GetString("ThreadMustBeSTA") }));
			}
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			IntPtr intPtr3 = IntPtr.Zero;
			try
			{
				UnsafeNativeMethods.BROWSEINFO browseinfo = new UnsafeNativeMethods.BROWSEINFO();
				intPtr2 = Marshal.AllocHGlobal(260 * Marshal.SystemDefaultCharSize);
				intPtr3 = Marshal.AllocHGlobal(261 * Marshal.SystemDefaultCharSize);
				this.callback = new UnsafeNativeMethods.BrowseCallbackProc(this.FolderBrowserDialog_BrowseCallbackProc);
				browseinfo.pidlRoot = zero;
				browseinfo.hwndOwner = hWndOwner;
				browseinfo.pszDisplayName = intPtr2;
				browseinfo.lpszTitle = this.descriptionText;
				browseinfo.ulFlags = num;
				browseinfo.lpfn = this.callback;
				browseinfo.lParam = IntPtr.Zero;
				browseinfo.iImage = 0;
				intPtr = UnsafeNativeMethods.Shell32.SHBrowseForFolder(browseinfo);
				if (intPtr != IntPtr.Zero)
				{
					UnsafeNativeMethods.Shell32.SHGetPathFromIDListLongPath(intPtr, ref intPtr3);
					this.selectedPathNeedsCheck = true;
					this.selectedPath = Marshal.PtrToStringAuto(intPtr3);
					flag = true;
				}
			}
			finally
			{
				UnsafeNativeMethods.CoTaskMemFree(zero);
				if (intPtr != IntPtr.Zero)
				{
					UnsafeNativeMethods.CoTaskMemFree(intPtr);
				}
				if (intPtr3 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr3);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
				}
				this.callback = null;
			}
			return flag;
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x000AFD7C File Offset: 0x000ADF7C
		private int FolderBrowserDialog_BrowseCallbackProc(IntPtr hwnd, int msg, IntPtr lParam, IntPtr lpData)
		{
			if (msg != 1)
			{
				if (msg == 2)
				{
					if (lParam != IntPtr.Zero)
					{
						IntPtr intPtr = Marshal.AllocHGlobal(261 * Marshal.SystemDefaultCharSize);
						bool flag = UnsafeNativeMethods.Shell32.SHGetPathFromIDListLongPath(lParam, ref intPtr);
						Marshal.FreeHGlobal(intPtr);
						UnsafeNativeMethods.SendMessage(new HandleRef(null, hwnd), 1125, 0, flag ? 1 : 0);
					}
				}
			}
			else if (this.selectedPath.Length != 0)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(null, hwnd), NativeMethods.BFFM_SETSELECTION, 1, this.selectedPath);
			}
			return 0;
		}

		// Token: 0x04000FAC RID: 4012
		private Environment.SpecialFolder rootFolder;

		// Token: 0x04000FAD RID: 4013
		private string descriptionText;

		// Token: 0x04000FAE RID: 4014
		private string selectedPath;

		// Token: 0x04000FAF RID: 4015
		private bool showNewFolderButton;

		// Token: 0x04000FB0 RID: 4016
		private bool selectedPathNeedsCheck;

		// Token: 0x04000FB1 RID: 4017
		private UnsafeNativeMethods.BrowseCallbackProc callback;
	}
}
