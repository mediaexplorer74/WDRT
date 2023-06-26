using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Displays a dialog box from which the user can select a file.</summary>
	// Token: 0x0200024D RID: 589
	[DefaultEvent("FileOk")]
	[DefaultProperty("FileName")]
	public abstract class FileDialog : CommonDialog
	{
		// Token: 0x06002552 RID: 9554 RVA: 0x000AE359 File Offset: 0x000AC559
		internal FileDialog()
		{
			this.Reset();
		}

		/// <summary>Gets or sets a value indicating whether the dialog box automatically adds an extension to a file name if the user omits the extension.</summary>
		/// <returns>
		///   <see langword="true" /> if the dialog box adds an extension to a file name if the user omits the extension; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06002553 RID: 9555 RVA: 0x000AE379 File Offset: 0x000AC579
		// (set) Token: 0x06002554 RID: 9556 RVA: 0x000AE386 File Offset: 0x000AC586
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("FDaddExtensionDescr")]
		public bool AddExtension
		{
			get
			{
				return this.GetOption(int.MinValue);
			}
			set
			{
				IntSecurity.FileDialogCustomization.Demand();
				this.SetOption(int.MinValue, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box displays a warning if the user specifies a file name that does not exist.</summary>
		/// <returns>
		///   <see langword="true" /> if the dialog box displays a warning if the user specifies a file name that does not exist; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06002555 RID: 9557 RVA: 0x000AE39E File Offset: 0x000AC59E
		// (set) Token: 0x06002556 RID: 9558 RVA: 0x000AE3AB File Offset: 0x000AC5AB
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("FDcheckFileExistsDescr")]
		public virtual bool CheckFileExists
		{
			get
			{
				return this.GetOption(4096);
			}
			set
			{
				IntSecurity.FileDialogCustomization.Demand();
				this.SetOption(4096, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box displays a warning if the user specifies a path that does not exist.</summary>
		/// <returns>
		///   <see langword="true" /> if the dialog box displays a warning when the user specifies a path that does not exist; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06002557 RID: 9559 RVA: 0x000AE3C3 File Offset: 0x000AC5C3
		// (set) Token: 0x06002558 RID: 9560 RVA: 0x000AE3D0 File Offset: 0x000AC5D0
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("FDcheckPathExistsDescr")]
		public bool CheckPathExists
		{
			get
			{
				return this.GetOption(2048);
			}
			set
			{
				IntSecurity.FileDialogCustomization.Demand();
				this.SetOption(2048, value);
			}
		}

		/// <summary>Gets or sets the default file name extension.</summary>
		/// <returns>The default file name extension. The returned string does not include the period. The default value is an empty string ("").</returns>
		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06002559 RID: 9561 RVA: 0x000AE3E8 File Offset: 0x000AC5E8
		// (set) Token: 0x0600255A RID: 9562 RVA: 0x000AE3FE File Offset: 0x000AC5FE
		[SRCategory("CatBehavior")]
		[DefaultValue("")]
		[SRDescription("FDdefaultExtDescr")]
		public string DefaultExt
		{
			get
			{
				if (this.defaultExt != null)
				{
					return this.defaultExt;
				}
				return "";
			}
			set
			{
				if (value != null)
				{
					if (value.StartsWith("."))
					{
						value = value.Substring(1);
					}
					else if (value.Length == 0)
					{
						value = null;
					}
				}
				this.defaultExt = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box returns the location of the file referenced by the shortcut or whether it returns the location of the shortcut (.lnk).</summary>
		/// <returns>
		///   <see langword="true" /> if the dialog box returns the location of the file referenced by the shortcut; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x0600255B RID: 9563 RVA: 0x000AE42D File Offset: 0x000AC62D
		// (set) Token: 0x0600255C RID: 9564 RVA: 0x000AE43D File Offset: 0x000AC63D
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("FDdereferenceLinksDescr")]
		public bool DereferenceLinks
		{
			get
			{
				return !this.GetOption(1048576);
			}
			set
			{
				IntSecurity.FileDialogCustomization.Demand();
				this.SetOption(1048576, !value);
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x0600255D RID: 9565 RVA: 0x000AE458 File Offset: 0x000AC658
		internal string DialogCaption
		{
			get
			{
				int windowTextLength = SafeNativeMethods.GetWindowTextLength(new HandleRef(this, this.dialogHWnd));
				StringBuilder stringBuilder = new StringBuilder(windowTextLength + 1);
				UnsafeNativeMethods.GetWindowText(new HandleRef(this, this.dialogHWnd), stringBuilder, stringBuilder.Capacity);
				return stringBuilder.ToString();
			}
		}

		/// <summary>Gets or sets a string containing the file name selected in the file dialog box.</summary>
		/// <returns>The file name selected in the file dialog box. The default value is an empty string ("").</returns>
		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x0600255E RID: 9566 RVA: 0x000AE4A0 File Offset: 0x000AC6A0
		// (set) Token: 0x0600255F RID: 9567 RVA: 0x000AE4F0 File Offset: 0x000AC6F0
		[SRCategory("CatData")]
		[DefaultValue("")]
		[SRDescription("FDfileNameDescr")]
		public string FileName
		{
			get
			{
				if (this.fileNames == null)
				{
					return "";
				}
				if (this.fileNames[0].Length > 0)
				{
					if (this.securityCheckFileNames)
					{
						IntSecurity.DemandFileIO(FileIOPermissionAccess.AllAccess, this.fileNames[0]);
					}
					return this.fileNames[0];
				}
				return "";
			}
			set
			{
				IntSecurity.FileDialogCustomization.Demand();
				if (value == null)
				{
					this.fileNames = null;
				}
				else
				{
					this.fileNames = new string[] { value };
				}
				this.securityCheckFileNames = false;
			}
		}

		/// <summary>Gets the file names of all selected files in the dialog box.</summary>
		/// <returns>An array of type <see cref="T:System.String" />, containing the file names of all selected files in the dialog box.</returns>
		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06002560 RID: 9568 RVA: 0x000AE520 File Offset: 0x000AC720
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FDFileNamesDescr")]
		public string[] FileNames
		{
			get
			{
				string[] fileNamesInternal = this.FileNamesInternal;
				if (this.securityCheckFileNames)
				{
					foreach (string text in fileNamesInternal)
					{
						IntSecurity.DemandFileIO(FileIOPermissionAccess.AllAccess, text);
					}
				}
				return fileNamesInternal;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06002561 RID: 9569 RVA: 0x000AE559 File Offset: 0x000AC759
		internal string[] FileNamesInternal
		{
			get
			{
				if (this.fileNames == null)
				{
					return new string[0];
				}
				return (string[])this.fileNames.Clone();
			}
		}

		/// <summary>Gets or sets the current file name filter string, which determines the choices that appear in the "Save as file type" or "Files of type" box in the dialog box.</summary>
		/// <returns>The file filtering options available in the dialog box.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Filter" /> format is invalid.</exception>
		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06002562 RID: 9570 RVA: 0x000AE57A File Offset: 0x000AC77A
		// (set) Token: 0x06002563 RID: 9571 RVA: 0x000AE590 File Offset: 0x000AC790
		[SRCategory("CatBehavior")]
		[DefaultValue("")]
		[Localizable(true)]
		[SRDescription("FDfilterDescr")]
		public string Filter
		{
			get
			{
				if (this.filter != null)
				{
					return this.filter;
				}
				return "";
			}
			set
			{
				if (value != this.filter)
				{
					if (value != null && value.Length > 0)
					{
						string[] array = value.Split(new char[] { '|' });
						if (array == null || array.Length % 2 != 0)
						{
							throw new ArgumentException(SR.GetString("FileDialogInvalidFilter"));
						}
					}
					else
					{
						value = null;
					}
					this.filter = value;
				}
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06002564 RID: 9572 RVA: 0x000AE5F0 File Offset: 0x000AC7F0
		private string[] FilterExtensions
		{
			get
			{
				string text = this.filter;
				ArrayList arrayList = new ArrayList();
				if (this.defaultExt != null)
				{
					arrayList.Add(this.defaultExt);
				}
				if (text != null)
				{
					string[] array = text.Split(new char[] { '|' });
					if (this.filterIndex * 2 - 1 >= array.Length)
					{
						throw new InvalidOperationException(SR.GetString("FileDialogInvalidFilterIndex"));
					}
					if (this.filterIndex > 0)
					{
						string[] array2 = array[this.filterIndex * 2 - 1].Split(new char[] { ';' });
						foreach (string text2 in array2)
						{
							int num = (this.supportMultiDottedExtensions ? text2.IndexOf('.') : text2.LastIndexOf('.'));
							if (num >= 0)
							{
								arrayList.Add(text2.Substring(num + 1, text2.Length - (num + 1)));
							}
						}
					}
				}
				string[] array4 = new string[arrayList.Count];
				arrayList.CopyTo(array4, 0);
				return array4;
			}
		}

		/// <summary>Gets or sets the index of the filter currently selected in the file dialog box.</summary>
		/// <returns>A value containing the index of the filter currently selected in the file dialog box. The default value is 1.</returns>
		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06002565 RID: 9573 RVA: 0x000AE6F1 File Offset: 0x000AC8F1
		// (set) Token: 0x06002566 RID: 9574 RVA: 0x000AE6F9 File Offset: 0x000AC8F9
		[SRCategory("CatBehavior")]
		[DefaultValue(1)]
		[SRDescription("FDfilterIndexDescr")]
		public int FilterIndex
		{
			get
			{
				return this.filterIndex;
			}
			set
			{
				this.filterIndex = value;
			}
		}

		/// <summary>Gets or sets the initial directory displayed by the file dialog box.</summary>
		/// <returns>The initial directory displayed by the file dialog box. The default is an empty string ("").</returns>
		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06002567 RID: 9575 RVA: 0x000AE702 File Offset: 0x000AC902
		// (set) Token: 0x06002568 RID: 9576 RVA: 0x000AE718 File Offset: 0x000AC918
		[SRCategory("CatData")]
		[DefaultValue("")]
		[SRDescription("FDinitialDirDescr")]
		public string InitialDirectory
		{
			get
			{
				if (this.initialDir != null)
				{
					return this.initialDir;
				}
				return "";
			}
			set
			{
				IntSecurity.FileDialogCustomization.Demand();
				this.initialDir = value;
			}
		}

		/// <summary>Gets the Win32 instance handle for the application.</summary>
		/// <returns>A Win32 instance handle for the application.</returns>
		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06002569 RID: 9577 RVA: 0x00026FEA File Offset: 0x000251EA
		protected virtual IntPtr Instance
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return UnsafeNativeMethods.GetModuleHandle(null);
			}
		}

		/// <summary>Gets values to initialize the <see cref="T:System.Windows.Forms.FileDialog" />.</summary>
		/// <returns>A bitwise combination of internal values that initializes the <see cref="T:System.Windows.Forms.FileDialog" />.</returns>
		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x0600256A RID: 9578 RVA: 0x000AE72B File Offset: 0x000AC92B
		protected int Options
		{
			get
			{
				return this.options & 1051421;
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box restores the directory to the previously selected directory before closing.</summary>
		/// <returns>
		///   <see langword="true" /> if the dialog box restores the current directory to the previously selected directory if the user changed the directory while searching for files; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x0600256B RID: 9579 RVA: 0x000AE739 File Offset: 0x000AC939
		// (set) Token: 0x0600256C RID: 9580 RVA: 0x000AE742 File Offset: 0x000AC942
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("FDrestoreDirectoryDescr")]
		public bool RestoreDirectory
		{
			get
			{
				return this.GetOption(8);
			}
			set
			{
				IntSecurity.FileDialogCustomization.Demand();
				this.SetOption(8, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the Help button is displayed in the file dialog box.</summary>
		/// <returns>
		///   <see langword="true" /> if the dialog box includes a help button; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x0600256D RID: 9581 RVA: 0x000AE756 File Offset: 0x000AC956
		// (set) Token: 0x0600256E RID: 9582 RVA: 0x000AE760 File Offset: 0x000AC960
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("FDshowHelpDescr")]
		public bool ShowHelp
		{
			get
			{
				return this.GetOption(16);
			}
			set
			{
				this.SetOption(16, value);
			}
		}

		/// <summary>Gets or sets whether the dialog box supports displaying and saving files that have multiple file name extensions.</summary>
		/// <returns>
		///   <see langword="true" /> if the dialog box supports multiple file name extensions; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x0600256F RID: 9583 RVA: 0x000AE76B File Offset: 0x000AC96B
		// (set) Token: 0x06002570 RID: 9584 RVA: 0x000AE773 File Offset: 0x000AC973
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("FDsupportMultiDottedExtensionsDescr")]
		public bool SupportMultiDottedExtensions
		{
			get
			{
				return this.supportMultiDottedExtensions;
			}
			set
			{
				this.supportMultiDottedExtensions = value;
			}
		}

		/// <summary>Gets or sets the file dialog box title.</summary>
		/// <returns>The file dialog box title. The default value is an empty string ("").</returns>
		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06002571 RID: 9585 RVA: 0x000AE77C File Offset: 0x000AC97C
		// (set) Token: 0x06002572 RID: 9586 RVA: 0x000AE792 File Offset: 0x000AC992
		[SRCategory("CatAppearance")]
		[DefaultValue("")]
		[Localizable(true)]
		[SRDescription("FDtitleDescr")]
		public string Title
		{
			get
			{
				if (this.title != null)
				{
					return this.title;
				}
				return "";
			}
			set
			{
				IntSecurity.FileDialogCustomization.Demand();
				this.title = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box accepts only valid Win32 file names.</summary>
		/// <returns>
		///   <see langword="true" /> if the dialog box accepts only valid Win32 file names; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06002573 RID: 9587 RVA: 0x000AE7A5 File Offset: 0x000AC9A5
		// (set) Token: 0x06002574 RID: 9588 RVA: 0x000AE7B5 File Offset: 0x000AC9B5
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("FDvalidateNamesDescr")]
		public bool ValidateNames
		{
			get
			{
				return !this.GetOption(256);
			}
			set
			{
				IntSecurity.FileDialogCustomization.Demand();
				this.SetOption(256, !value);
			}
		}

		/// <summary>Occurs when the user clicks on the Open or Save button on a file dialog box.</summary>
		// Token: 0x1400019A RID: 410
		// (add) Token: 0x06002575 RID: 9589 RVA: 0x000AE7D0 File Offset: 0x000AC9D0
		// (remove) Token: 0x06002576 RID: 9590 RVA: 0x000AE7E3 File Offset: 0x000AC9E3
		[SRDescription("FDfileOkDescr")]
		public event CancelEventHandler FileOk
		{
			add
			{
				base.Events.AddHandler(FileDialog.EventFileOk, value);
			}
			remove
			{
				base.Events.RemoveHandler(FileDialog.EventFileOk, value);
			}
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x000AE7F8 File Offset: 0x000AC9F8
		private bool DoFileOk(IntPtr lpOFN)
		{
			NativeMethods.OPENFILENAME_I openfilename_I = (NativeMethods.OPENFILENAME_I)UnsafeNativeMethods.PtrToStructure(lpOFN, typeof(NativeMethods.OPENFILENAME_I));
			int num = this.options;
			int num2 = this.filterIndex;
			string[] array = this.fileNames;
			bool flag = this.securityCheckFileNames;
			bool flag2 = false;
			try
			{
				this.options = (this.options & -2) | (openfilename_I.Flags & 1);
				this.filterIndex = openfilename_I.nFilterIndex;
				this.charBuffer.PutCoTaskMem(openfilename_I.lpstrFile);
				this.securityCheckFileNames = true;
				Thread.MemoryBarrier();
				if ((this.options & 512) == 0)
				{
					this.fileNames = new string[] { this.charBuffer.GetString() };
				}
				else
				{
					this.fileNames = this.GetMultiselectFiles(this.charBuffer);
				}
				if (this.ProcessFileNames())
				{
					CancelEventArgs cancelEventArgs = new CancelEventArgs();
					if (NativeWindow.WndProcShouldBeDebuggable)
					{
						this.OnFileOk(cancelEventArgs);
						flag2 = !cancelEventArgs.Cancel;
					}
					else
					{
						try
						{
							this.OnFileOk(cancelEventArgs);
							flag2 = !cancelEventArgs.Cancel;
						}
						catch (Exception ex)
						{
							Application.OnThreadException(ex);
						}
					}
				}
			}
			finally
			{
				if (!flag2)
				{
					this.securityCheckFileNames = flag;
					Thread.MemoryBarrier();
					this.fileNames = array;
					this.options = num;
					this.filterIndex = num2;
				}
			}
			return flag2;
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x000AE950 File Offset: 0x000ACB50
		internal static bool FileExists(string fileName)
		{
			bool flag = false;
			try
			{
				new FileIOPermission(FileIOPermissionAccess.Read, IntSecurity.UnsafeGetFullPath(fileName)).Assert();
				try
				{
					flag = File.Exists(fileName);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			catch (PathTooLongException)
			{
			}
			return flag;
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x000AE9A4 File Offset: 0x000ACBA4
		private string[] GetMultiselectFiles(UnsafeNativeMethods.CharBuffer charBuffer)
		{
			string text = charBuffer.GetString();
			string text2 = charBuffer.GetString();
			if (text2.Length == 0)
			{
				return new string[] { text };
			}
			if (text[text.Length - 1] != '\\')
			{
				text += "\\";
			}
			ArrayList arrayList = new ArrayList();
			do
			{
				if (text2[0] != '\\' && (text2.Length <= 3 || text2[1] != ':' || text2[2] != '\\'))
				{
					text2 = text + text2;
				}
				arrayList.Add(text2);
				text2 = charBuffer.GetString();
			}
			while (text2.Length > 0);
			string[] array = new string[arrayList.Count];
			arrayList.CopyTo(array, 0);
			return array;
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x000AEA55 File Offset: 0x000ACC55
		internal bool GetOption(int option)
		{
			return (this.options & option) != 0;
		}

		/// <summary>Defines the common dialog box hook procedure that is overridden to add specific functionality to the file dialog box.</summary>
		/// <param name="hWnd">The handle to the dialog box window.</param>
		/// <param name="msg">The message received by the dialog box.</param>
		/// <param name="wparam">Additional information about the message.</param>
		/// <param name="lparam">Additional information about the message.</param>
		/// <returns>Returns zero if the default dialog box procedure processes the message; returns a nonzero value if the default dialog box procedure ignores the message.</returns>
		// Token: 0x0600257B RID: 9595 RVA: 0x000AEA64 File Offset: 0x000ACC64
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
		{
			if (msg == 78)
			{
				this.dialogHWnd = UnsafeNativeMethods.GetParent(new HandleRef(null, hWnd));
				try
				{
					UnsafeNativeMethods.OFNOTIFY ofnotify = (UnsafeNativeMethods.OFNOTIFY)UnsafeNativeMethods.PtrToStructure(lparam, typeof(UnsafeNativeMethods.OFNOTIFY));
					switch (ofnotify.hdr_code)
					{
					case -606:
						if (this.ignoreSecondFileOkNotification)
						{
							if (this.okNotificationCount != 0)
							{
								this.ignoreSecondFileOkNotification = false;
								UnsafeNativeMethods.SetWindowLong(new HandleRef(null, hWnd), 0, new HandleRef(null, NativeMethods.InvalidIntPtr));
								return NativeMethods.InvalidIntPtr;
							}
							this.okNotificationCount = 1;
						}
						if (!this.DoFileOk(ofnotify.lpOFN))
						{
							UnsafeNativeMethods.SetWindowLong(new HandleRef(null, hWnd), 0, new HandleRef(null, NativeMethods.InvalidIntPtr));
							return NativeMethods.InvalidIntPtr;
						}
						break;
					case -604:
						this.ignoreSecondFileOkNotification = true;
						this.okNotificationCount = 0;
						break;
					case -602:
					{
						NativeMethods.OPENFILENAME_I openfilename_I = (NativeMethods.OPENFILENAME_I)UnsafeNativeMethods.PtrToStructure(ofnotify.lpOFN, typeof(NativeMethods.OPENFILENAME_I));
						int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, this.dialogHWnd), 1124, IntPtr.Zero, IntPtr.Zero);
						if (num > openfilename_I.nMaxFile)
						{
							try
							{
								int num2 = num + 2048;
								UnsafeNativeMethods.CharBuffer charBuffer = UnsafeNativeMethods.CharBuffer.CreateBuffer(num2);
								IntPtr intPtr = charBuffer.AllocCoTaskMem();
								Marshal.FreeCoTaskMem(openfilename_I.lpstrFile);
								openfilename_I.lpstrFile = intPtr;
								openfilename_I.nMaxFile = num2;
								this.charBuffer = charBuffer;
								Marshal.StructureToPtr(openfilename_I, ofnotify.lpOFN, true);
								Marshal.StructureToPtr(ofnotify, lparam, true);
							}
							catch
							{
							}
						}
						this.ignoreSecondFileOkNotification = false;
						break;
					}
					case -601:
						CommonDialog.MoveToScreenCenter(this.dialogHWnd);
						break;
					}
				}
				catch
				{
					if (this.dialogHWnd != IntPtr.Zero)
					{
						UnsafeNativeMethods.EndDialog(new HandleRef(this, this.dialogHWnd), IntPtr.Zero);
					}
					throw;
				}
			}
			return IntPtr.Zero;
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x000AEC80 File Offset: 0x000ACE80
		private static string MakeFilterString(string s, bool dereferenceLinks)
		{
			if (s == null || s.Length == 0)
			{
				if (dereferenceLinks && Environment.OSVersion.Version.Major >= 5)
				{
					s = " |*.*";
				}
				else if (s == null)
				{
					return null;
				}
			}
			int length = s.Length;
			char[] array = new char[length + 2];
			s.CopyTo(0, array, 0, length);
			for (int i = 0; i < length; i++)
			{
				if (array[i] == '|')
				{
					array[i] = '\0';
				}
			}
			array[length + 1] = '\0';
			return new string(array);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.FileDialog.FileOk" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
		// Token: 0x0600257D RID: 9597 RVA: 0x000AECF8 File Offset: 0x000ACEF8
		protected void OnFileOk(CancelEventArgs e)
		{
			CancelEventHandler cancelEventHandler = (CancelEventHandler)base.Events[FileDialog.EventFileOk];
			if (cancelEventHandler != null)
			{
				cancelEventHandler(this, e);
			}
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x000AED28 File Offset: 0x000ACF28
		private bool ProcessFileNames()
		{
			if ((this.options & 256) == 0)
			{
				string[] filterExtensions = this.FilterExtensions;
				for (int i = 0; i < this.fileNames.Length; i++)
				{
					string text = this.fileNames[i];
					if ((this.options & -2147483648) != 0 && !Path.HasExtension(text))
					{
						bool flag = (this.options & 4096) != 0;
						for (int j = 0; j < filterExtensions.Length; j++)
						{
							string extension = Path.GetExtension(text);
							string text2 = text.Substring(0, text.Length - extension.Length);
							if (filterExtensions[j].IndexOfAny(new char[] { '*', '?' }) == -1)
							{
								text2 = text2 + "." + filterExtensions[j];
							}
							if (!flag || FileDialog.FileExists(text2))
							{
								text = text2;
								break;
							}
						}
						this.fileNames[i] = text;
					}
					if (!this.PromptUserIfAppropriate(text))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x000AEE20 File Offset: 0x000AD020
		internal bool MessageBoxWithFocusRestore(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			IntPtr focus = UnsafeNativeMethods.GetFocus();
			bool flag;
			try
			{
				flag = RTLAwareMessageBox.Show(null, message, caption, buttons, icon, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0) == DialogResult.Yes;
			}
			finally
			{
				UnsafeNativeMethods.SetFocus(new HandleRef(null, focus));
			}
			return flag;
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x000AEE68 File Offset: 0x000AD068
		private void PromptFileNotFound(string fileName)
		{
			this.MessageBoxWithFocusRestore(SR.GetString("FileDialogFileNotFound", new object[] { fileName }), this.DialogCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x000AEE8E File Offset: 0x000AD08E
		internal virtual bool PromptUserIfAppropriate(string fileName)
		{
			if ((this.options & 4096) != 0 && !FileDialog.FileExists(fileName))
			{
				this.PromptFileNotFound(fileName);
				return false;
			}
			return true;
		}

		/// <summary>Resets all properties to their default values.</summary>
		// Token: 0x06002582 RID: 9602 RVA: 0x000AEEB0 File Offset: 0x000AD0B0
		public override void Reset()
		{
			this.options = -2147481596;
			this.title = null;
			this.initialDir = null;
			this.defaultExt = null;
			this.fileNames = null;
			this.filter = null;
			this.filterIndex = 1;
			this.supportMultiDottedExtensions = false;
			this._customPlaces.Clear();
		}

		/// <summary>Specifies a common dialog box.</summary>
		/// <param name="hWndOwner">A value that represents the window handle of the owner window for the common dialog box.</param>
		/// <returns>
		///   <see langword="true" /> if the file could be opened; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002583 RID: 9603 RVA: 0x000AEF04 File Offset: 0x000AD104
		protected override bool RunDialog(IntPtr hWndOwner)
		{
			if (Control.CheckForIllegalCrossThreadCalls && Application.OleRequired() != ApartmentState.STA)
			{
				throw new ThreadStateException(SR.GetString("DebuggingExceptionOnly", new object[] { SR.GetString("ThreadMustBeSTA") }));
			}
			this.EnsureFileDialogPermission();
			if (this.UseVistaDialogInternal)
			{
				return this.RunDialogVista(hWndOwner);
			}
			return this.RunDialogOld(hWndOwner);
		}

		// Token: 0x06002584 RID: 9604
		internal abstract void EnsureFileDialogPermission();

		// Token: 0x06002585 RID: 9605 RVA: 0x000AEF60 File Offset: 0x000AD160
		private bool RunDialogOld(IntPtr hWndOwner)
		{
			NativeMethods.WndProc wndProc = new NativeMethods.WndProc(this.HookProc);
			NativeMethods.OPENFILENAME_I openfilename_I = new NativeMethods.OPENFILENAME_I();
			bool flag;
			try
			{
				this.charBuffer = UnsafeNativeMethods.CharBuffer.CreateBuffer(8192);
				if (this.fileNames != null)
				{
					this.charBuffer.PutString(this.fileNames[0]);
				}
				openfilename_I.lStructSize = Marshal.SizeOf(typeof(NativeMethods.OPENFILENAME_I));
				if (Environment.OSVersion.Platform != PlatformID.Win32NT || Environment.OSVersion.Version.Major < 5)
				{
					openfilename_I.lStructSize = 76;
				}
				openfilename_I.hwndOwner = hWndOwner;
				openfilename_I.hInstance = this.Instance;
				openfilename_I.lpstrFilter = FileDialog.MakeFilterString(this.filter, this.DereferenceLinks);
				openfilename_I.nFilterIndex = this.filterIndex;
				openfilename_I.lpstrFile = this.charBuffer.AllocCoTaskMem();
				openfilename_I.nMaxFile = 8192;
				openfilename_I.lpstrInitialDir = this.initialDir;
				openfilename_I.lpstrTitle = this.title;
				openfilename_I.Flags = this.Options | 8912928;
				openfilename_I.lpfnHook = wndProc;
				openfilename_I.FlagsEx = 16777216;
				if (this.defaultExt != null && this.AddExtension)
				{
					openfilename_I.lpstrDefExt = this.defaultExt;
				}
				flag = this.RunFileDialog(openfilename_I);
			}
			finally
			{
				this.charBuffer = null;
				if (openfilename_I.lpstrFile != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(openfilename_I.lpstrFile);
				}
			}
			return flag;
		}

		// Token: 0x06002586 RID: 9606
		internal abstract bool RunFileDialog(NativeMethods.OPENFILENAME_I ofn);

		// Token: 0x06002587 RID: 9607 RVA: 0x000AF0E0 File Offset: 0x000AD2E0
		internal void SetOption(int option, bool value)
		{
			if (value)
			{
				this.options |= option;
				return;
			}
			this.options &= ~option;
		}

		/// <summary>Provides a string version of this object.</summary>
		/// <returns>A string version of this object.</returns>
		// Token: 0x06002588 RID: 9608 RVA: 0x000AF104 File Offset: 0x000AD304
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString() + ": Title: " + this.Title + ", FileName: ");
			try
			{
				stringBuilder.Append(this.FileName);
			}
			catch (Exception ex)
			{
				stringBuilder.Append("<");
				stringBuilder.Append(ex.GetType().FullName);
				stringBuilder.Append(">");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06002589 RID: 9609 RVA: 0x000AF184 File Offset: 0x000AD384
		internal virtual bool SettingsSupportVistaDialog
		{
			get
			{
				return !this.ShowHelp && (Application.VisualStyleState & VisualStyleState.ClientAreaEnabled) == VisualStyleState.ClientAreaEnabled;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x0600258A RID: 9610 RVA: 0x000AF19C File Offset: 0x000AD39C
		internal bool UseVistaDialogInternal
		{
			get
			{
				if (UnsafeNativeMethods.IsVista && this._autoUpgradeEnabled && this.SettingsSupportVistaDialog)
				{
					new EnvironmentPermission(PermissionState.Unrestricted).Assert();
					try
					{
						return SystemInformation.BootMode == BootMode.Normal;
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					return false;
				}
				return false;
			}
		}

		// Token: 0x0600258B RID: 9611
		internal abstract FileDialogNative.IFileDialog CreateVistaDialog();

		// Token: 0x0600258C RID: 9612 RVA: 0x000AF1F0 File Offset: 0x000AD3F0
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		private bool RunDialogVista(IntPtr hWndOwner)
		{
			FileDialogNative.IFileDialog fileDialog = this.CreateVistaDialog();
			this.OnBeforeVistaDialog(fileDialog);
			FileDialog.VistaDialogEvents vistaDialogEvents = new FileDialog.VistaDialogEvents(this);
			uint num;
			fileDialog.Advise(vistaDialogEvents, out num);
			bool flag;
			try
			{
				int num2 = fileDialog.Show(hWndOwner);
				flag = num2 == 0;
			}
			finally
			{
				fileDialog.Unadvise(num);
				GC.KeepAlive(vistaDialogEvents);
			}
			return flag;
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x000AF24C File Offset: 0x000AD44C
		internal virtual void OnBeforeVistaDialog(FileDialogNative.IFileDialog dialog)
		{
			dialog.SetDefaultExtension(this.DefaultExt);
			dialog.SetFileName(this.FileName);
			if (!string.IsNullOrEmpty(this.InitialDirectory))
			{
				try
				{
					FileDialogNative.IShellItem shellItemForPath = FileDialog.GetShellItemForPath(this.InitialDirectory);
					dialog.SetDefaultFolder(shellItemForPath);
					dialog.SetFolder(shellItemForPath);
				}
				catch (FileNotFoundException)
				{
				}
			}
			dialog.SetTitle(this.Title);
			dialog.SetOptions(this.GetOptions());
			this.SetFileTypes(dialog);
			this._customPlaces.Apply(dialog);
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x000AF2D8 File Offset: 0x000AD4D8
		private FileDialogNative.FOS GetOptions()
		{
			FileDialogNative.FOS fos = (FileDialogNative.FOS)(this.options & 1063690);
			fos |= FileDialogNative.FOS.FOS_DEFAULTNOMINIMODE;
			return fos | FileDialogNative.FOS.FOS_FORCEFILESYSTEM;
		}

		// Token: 0x0600258F RID: 9615
		internal abstract string[] ProcessVistaFiles(FileDialogNative.IFileDialog dialog);

		// Token: 0x06002590 RID: 9616 RVA: 0x000AF300 File Offset: 0x000AD500
		private bool HandleVistaFileOk(FileDialogNative.IFileDialog dialog)
		{
			int num = this.options;
			int num2 = this.filterIndex;
			string[] array = this.fileNames;
			bool flag = this.securityCheckFileNames;
			bool flag2 = false;
			try
			{
				this.securityCheckFileNames = true;
				Thread.MemoryBarrier();
				uint num3;
				dialog.GetFileTypeIndex(out num3);
				this.filterIndex = (int)num3;
				this.fileNames = this.ProcessVistaFiles(dialog);
				if (this.ProcessFileNames())
				{
					CancelEventArgs cancelEventArgs = new CancelEventArgs();
					if (NativeWindow.WndProcShouldBeDebuggable)
					{
						this.OnFileOk(cancelEventArgs);
						flag2 = !cancelEventArgs.Cancel;
					}
					else
					{
						try
						{
							this.OnFileOk(cancelEventArgs);
							flag2 = !cancelEventArgs.Cancel;
						}
						catch (Exception ex)
						{
							Application.OnThreadException(ex);
						}
					}
				}
			}
			finally
			{
				if (!flag2)
				{
					this.securityCheckFileNames = flag;
					Thread.MemoryBarrier();
					this.fileNames = array;
					this.options = num;
					this.filterIndex = num2;
				}
				else if ((this.options & 4) != 0)
				{
					this.options &= -2;
				}
			}
			return flag2;
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x000AF404 File Offset: 0x000AD604
		private void SetFileTypes(FileDialogNative.IFileDialog dialog)
		{
			FileDialogNative.COMDLG_FILTERSPEC[] filterItems = this.FilterItems;
			dialog.SetFileTypes((uint)filterItems.Length, filterItems);
			if (filterItems.Length != 0)
			{
				dialog.SetFileTypeIndex((uint)this.filterIndex);
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06002592 RID: 9618 RVA: 0x000AF432 File Offset: 0x000AD632
		private FileDialogNative.COMDLG_FILTERSPEC[] FilterItems
		{
			get
			{
				return FileDialog.GetFilterItems(this.filter);
			}
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x000AF440 File Offset: 0x000AD640
		private static FileDialogNative.COMDLG_FILTERSPEC[] GetFilterItems(string filter)
		{
			List<FileDialogNative.COMDLG_FILTERSPEC> list = new List<FileDialogNative.COMDLG_FILTERSPEC>();
			if (!string.IsNullOrEmpty(filter))
			{
				string[] array = filter.Split(new char[] { '|' });
				if (array.Length % 2 == 0)
				{
					for (int i = 1; i < array.Length; i += 2)
					{
						FileDialogNative.COMDLG_FILTERSPEC comdlg_FILTERSPEC;
						comdlg_FILTERSPEC.pszSpec = array[i];
						comdlg_FILTERSPEC.pszName = array[i - 1];
						list.Add(comdlg_FILTERSPEC);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x000AF4A8 File Offset: 0x000AD6A8
		internal static FileDialogNative.IShellItem GetShellItemForPath(string path)
		{
			FileDialogNative.IShellItem shellItem = null;
			IntPtr zero = IntPtr.Zero;
			uint num = 0U;
			if (0 <= UnsafeNativeMethods.Shell32.SHILCreateFromPath(path, out zero, ref num) && 0 <= UnsafeNativeMethods.Shell32.SHCreateShellItem(IntPtr.Zero, IntPtr.Zero, zero, out shellItem))
			{
				return shellItem;
			}
			throw new FileNotFoundException();
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x000AF4E8 File Offset: 0x000AD6E8
		internal static string GetFilePathFromShellItem(FileDialogNative.IShellItem item)
		{
			string text;
			item.GetDisplayName((FileDialogNative.SIGDN)2147647488U, out text);
			return text;
		}

		/// <summary>Gets the custom places collection for this <see cref="T:System.Windows.Forms.FileDialog" /> instance.</summary>
		/// <returns>The custom places collection for this <see cref="T:System.Windows.Forms.FileDialog" /> instance.</returns>
		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06002596 RID: 9622 RVA: 0x000AF503 File Offset: 0x000AD703
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public FileDialogCustomPlacesCollection CustomPlaces
		{
			get
			{
				return this._customPlaces;
			}
		}

		/// <summary>Gets or sets a value indicating whether this <see cref="T:System.Windows.Forms.FileDialog" /> instance should automatically upgrade appearance and behavior when running on Windows Vista.</summary>
		/// <returns>
		///   <see langword="true" /> if this <see cref="T:System.Windows.Forms.FileDialog" /> instance should automatically upgrade appearance and behavior when running on Windows Vista; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06002597 RID: 9623 RVA: 0x000AF50B File Offset: 0x000AD70B
		// (set) Token: 0x06002598 RID: 9624 RVA: 0x000AF513 File Offset: 0x000AD713
		[DefaultValue(true)]
		public bool AutoUpgradeEnabled
		{
			get
			{
				return this._autoUpgradeEnabled;
			}
			set
			{
				this._autoUpgradeEnabled = value;
			}
		}

		// Token: 0x04000F83 RID: 3971
		private const int FILEBUFSIZE = 8192;

		/// <summary>Owns the <see cref="E:System.Windows.Forms.FileDialog.FileOk" /> event.</summary>
		// Token: 0x04000F84 RID: 3972
		protected static readonly object EventFileOk = new object();

		// Token: 0x04000F85 RID: 3973
		internal const int OPTION_ADDEXTENSION = -2147483648;

		// Token: 0x04000F86 RID: 3974
		internal int options;

		// Token: 0x04000F87 RID: 3975
		private string title;

		// Token: 0x04000F88 RID: 3976
		private string initialDir;

		// Token: 0x04000F89 RID: 3977
		private string defaultExt;

		// Token: 0x04000F8A RID: 3978
		private string[] fileNames;

		// Token: 0x04000F8B RID: 3979
		private bool securityCheckFileNames;

		// Token: 0x04000F8C RID: 3980
		private string filter;

		// Token: 0x04000F8D RID: 3981
		private int filterIndex;

		// Token: 0x04000F8E RID: 3982
		private bool supportMultiDottedExtensions;

		// Token: 0x04000F8F RID: 3983
		private bool ignoreSecondFileOkNotification;

		// Token: 0x04000F90 RID: 3984
		private int okNotificationCount;

		// Token: 0x04000F91 RID: 3985
		private UnsafeNativeMethods.CharBuffer charBuffer;

		// Token: 0x04000F92 RID: 3986
		private IntPtr dialogHWnd;

		// Token: 0x04000F93 RID: 3987
		private bool _autoUpgradeEnabled = true;

		// Token: 0x04000F94 RID: 3988
		private FileDialogCustomPlacesCollection _customPlaces = new FileDialogCustomPlacesCollection();

		// Token: 0x0200068A RID: 1674
		private class VistaDialogEvents : FileDialogNative.IFileDialogEvents
		{
			// Token: 0x06006746 RID: 26438 RVA: 0x001827C8 File Offset: 0x001809C8
			public VistaDialogEvents(FileDialog dialog)
			{
				this._dialog = dialog;
			}

			// Token: 0x06006747 RID: 26439 RVA: 0x001827D7 File Offset: 0x001809D7
			public int OnFileOk(FileDialogNative.IFileDialog pfd)
			{
				if (!this._dialog.HandleVistaFileOk(pfd))
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x06006748 RID: 26440 RVA: 0x0001180C File Offset: 0x0000FA0C
			public int OnFolderChanging(FileDialogNative.IFileDialog pfd, FileDialogNative.IShellItem psiFolder)
			{
				return 0;
			}

			// Token: 0x06006749 RID: 26441 RVA: 0x000070A6 File Offset: 0x000052A6
			public void OnFolderChange(FileDialogNative.IFileDialog pfd)
			{
			}

			// Token: 0x0600674A RID: 26442 RVA: 0x000070A6 File Offset: 0x000052A6
			public void OnSelectionChange(FileDialogNative.IFileDialog pfd)
			{
			}

			// Token: 0x0600674B RID: 26443 RVA: 0x001827EA File Offset: 0x001809EA
			public void OnShareViolation(FileDialogNative.IFileDialog pfd, FileDialogNative.IShellItem psi, out FileDialogNative.FDE_SHAREVIOLATION_RESPONSE pResponse)
			{
				pResponse = FileDialogNative.FDE_SHAREVIOLATION_RESPONSE.FDESVR_DEFAULT;
			}

			// Token: 0x0600674C RID: 26444 RVA: 0x000070A6 File Offset: 0x000052A6
			public void OnTypeChange(FileDialogNative.IFileDialog pfd)
			{
			}

			// Token: 0x0600674D RID: 26445 RVA: 0x001827EA File Offset: 0x001809EA
			public void OnOverwrite(FileDialogNative.IFileDialog pfd, FileDialogNative.IShellItem psi, out FileDialogNative.FDE_OVERWRITE_RESPONSE pResponse)
			{
				pResponse = FileDialogNative.FDE_OVERWRITE_RESPONSE.FDEOR_DEFAULT;
			}

			// Token: 0x04003A9A RID: 15002
			private FileDialog _dialog;
		}
	}
}
