using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows picture box control for displaying an image.</summary>
	// Token: 0x0200031A RID: 794
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("Image")]
	[DefaultBindingProperty("Image")]
	[Docking(DockingBehavior.Ask)]
	[Designer("System.Windows.Forms.Design.PictureBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionPictureBox")]
	public class PictureBox : Control, ISupportInitialize
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PictureBox" /> class.</summary>
		// Token: 0x06003282 RID: 12930 RVA: 0x000E27D0 File Offset: 0x000E09D0
		public PictureBox()
		{
			base.SetState2(2048, true);
			this.pictureBoxState = new BitVector32(12);
			base.SetStyle(ControlStyles.Opaque | ControlStyles.Selectable, false);
			base.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);
			this.TabStop = false;
			this.savedSize = base.Size;
		}

		/// <summary>Overrides the <see cref="P:System.Windows.Forms.Control.AllowDrop" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if drag-and-drop operations are allowed in the control; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06003283 RID: 12931 RVA: 0x000B8E3D File Offset: 0x000B703D
		// (set) Token: 0x06003284 RID: 12932 RVA: 0x000B8E45 File Offset: 0x000B7045
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool AllowDrop
		{
			get
			{
				return base.AllowDrop;
			}
			set
			{
				base.AllowDrop = value;
			}
		}

		/// <summary>Indicates the border style for the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> enumeration values. The default is <see cref="F:System.Windows.Forms.BorderStyle.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.BorderStyle" /> values.</exception>
		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06003285 RID: 12933 RVA: 0x000E2832 File Offset: 0x000E0A32
		// (set) Token: 0x06003286 RID: 12934 RVA: 0x000E283C File Offset: 0x000E0A3C
		[DefaultValue(BorderStyle.None)]
		[SRCategory("CatAppearance")]
		[DispId(-504)]
		[SRDescription("PictureBoxBorderStyleDescr")]
		public BorderStyle BorderStyle
		{
			get
			{
				return this.borderStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(BorderStyle));
				}
				if (this.borderStyle != value)
				{
					this.borderStyle = value;
					base.RecreateHandle();
					this.AdjustSize();
				}
			}
		}

		// Token: 0x06003287 RID: 12935 RVA: 0x000E288C File Offset: 0x000E0A8C
		private Uri CalculateUri(string path)
		{
			Uri uri;
			try
			{
				uri = new Uri(path);
			}
			catch (UriFormatException)
			{
				path = Path.GetFullPath(path);
				uri = new Uri(path);
			}
			return uri;
		}

		/// <summary>Cancels an asynchronous image load.</summary>
		// Token: 0x06003288 RID: 12936 RVA: 0x000E28C8 File Offset: 0x000E0AC8
		[SRCategory("CatAsynchronous")]
		[SRDescription("PictureBoxCancelAsyncDescr")]
		public void CancelAsync()
		{
			this.pictureBoxState[2] = true;
		}

		/// <summary>Overrides the <see cref="P:System.Windows.Forms.Control.CausesValidation" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the control causes validation to be performed on any controls requiring validation when it receives focus; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06003289 RID: 12937 RVA: 0x000E28D7 File Offset: 0x000E0AD7
		// (set) Token: 0x0600328A RID: 12938 RVA: 0x000E28DF File Offset: 0x000E0ADF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool CausesValidation
		{
			get
			{
				return base.CausesValidation;
			}
			set
			{
				base.CausesValidation = value;
			}
		}

		/// <summary>Overrides the <see cref="E:System.Windows.Forms.Control.CausesValidationChanged" /> property.</summary>
		// Token: 0x14000251 RID: 593
		// (add) Token: 0x0600328B RID: 12939 RVA: 0x000E28E8 File Offset: 0x000E0AE8
		// (remove) Token: 0x0600328C RID: 12940 RVA: 0x000E28F1 File Offset: 0x000E0AF1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler CausesValidationChanged
		{
			add
			{
				base.CausesValidationChanged += value;
			}
			remove
			{
				base.CausesValidationChanged -= value;
			}
		}

		/// <summary>Overrides the <see cref="P:System.Windows.Forms.Control.CreateParams" /> property.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x0600328D RID: 12941 RVA: 0x000E28FC File Offset: 0x000E0AFC
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				BorderStyle borderStyle = this.borderStyle;
				if (borderStyle != BorderStyle.FixedSingle)
				{
					if (borderStyle == BorderStyle.Fixed3D)
					{
						createParams.ExStyle |= 512;
					}
				}
				else
				{
					createParams.Style |= 8388608;
				}
				return createParams;
			}
		}

		/// <summary>Gets a value indicating the mode for Input Method Editor (IME) for the <see cref="T:System.Windows.Forms.PictureBox" />.</summary>
		/// <returns>Always <see cref="F:System.Windows.Forms.ImeMode.Disable" />.</returns>
		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x0600328E RID: 12942 RVA: 0x00023BD7 File Offset: 0x00021DD7
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Disable;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x0600328F RID: 12943 RVA: 0x000E2946 File Offset: 0x000E0B46
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 50);
			}
		}

		/// <summary>Gets or sets the image to display when an error occurs during the image-loading process or if the image load is canceled.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> to display if an error occurs during the image-loading process or if the image load is canceled.</returns>
		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06003290 RID: 12944 RVA: 0x000E2954 File Offset: 0x000E0B54
		// (set) Token: 0x06003291 RID: 12945 RVA: 0x000E29BC File Offset: 0x000E0BBC
		[SRCategory("CatAsynchronous")]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("PictureBoxErrorImageDescr")]
		public Image ErrorImage
		{
			get
			{
				if (this.errorImage == null && this.pictureBoxState[8])
				{
					if (this.defaultErrorImage == null)
					{
						if (PictureBox.defaultErrorImageForThread == null)
						{
							PictureBox.defaultErrorImageForThread = new Bitmap(typeof(PictureBox), "ImageInError.bmp");
						}
						this.defaultErrorImage = PictureBox.defaultErrorImageForThread;
					}
					this.errorImage = this.defaultErrorImage;
				}
				return this.errorImage;
			}
			set
			{
				if (this.ErrorImage != value)
				{
					this.pictureBoxState[8] = false;
				}
				this.errorImage = value;
			}
		}

		/// <summary>Overrides the <see cref="P:System.Windows.Forms.Control.ForeColor" /> property.</summary>
		/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultForeColor" /> property.</returns>
		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x06003292 RID: 12946 RVA: 0x0001A0E7 File Offset: 0x000182E7
		// (set) Token: 0x06003293 RID: 12947 RVA: 0x00013024 File Offset: 0x00011224
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PictureBox.ForeColor" /> property changes.</summary>
		// Token: 0x14000252 RID: 594
		// (add) Token: 0x06003294 RID: 12948 RVA: 0x0005A8EE File Offset: 0x00058AEE
		// (remove) Token: 0x06003295 RID: 12949 RVA: 0x0005A8F7 File Offset: 0x00058AF7
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ForeColorChanged
		{
			add
			{
				base.ForeColorChanged += value;
			}
			remove
			{
				base.ForeColorChanged -= value;
			}
		}

		/// <summary>Gets or sets the font of the text displayed by the control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property.</returns>
		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x06003296 RID: 12950 RVA: 0x0001A0D6 File Offset: 0x000182D6
		// (set) Token: 0x06003297 RID: 12951 RVA: 0x0001A0DE File Offset: 0x000182DE
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PictureBox.Font" /> property changes.</summary>
		// Token: 0x14000253 RID: 595
		// (add) Token: 0x06003298 RID: 12952 RVA: 0x0005A900 File Offset: 0x00058B00
		// (remove) Token: 0x06003299 RID: 12953 RVA: 0x0005A909 File Offset: 0x00058B09
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler FontChanged
		{
			add
			{
				base.FontChanged += value;
			}
			remove
			{
				base.FontChanged -= value;
			}
		}

		/// <summary>Gets or sets the image that is displayed by <see cref="T:System.Windows.Forms.PictureBox" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> to display.</returns>
		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x0600329A RID: 12954 RVA: 0x000E29DB File Offset: 0x000E0BDB
		// (set) Token: 0x0600329B RID: 12955 RVA: 0x000E29E3 File Offset: 0x000E0BE3
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[Bindable(true)]
		[SRDescription("PictureBoxImageDescr")]
		public Image Image
		{
			get
			{
				return this.image;
			}
			set
			{
				this.InstallNewImage(value, PictureBox.ImageInstallationType.DirectlySpecified);
			}
		}

		/// <summary>Gets or sets the path or URL for the image to display in the <see cref="T:System.Windows.Forms.PictureBox" />.</summary>
		/// <returns>The path or URL for the image to display in the <see cref="T:System.Windows.Forms.PictureBox" />.</returns>
		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x0600329C RID: 12956 RVA: 0x000E29ED File Offset: 0x000E0BED
		// (set) Token: 0x0600329D RID: 12957 RVA: 0x000E29F8 File Offset: 0x000E0BF8
		[SRCategory("CatAsynchronous")]
		[Localizable(true)]
		[DefaultValue(null)]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("PictureBoxImageLocationDescr")]
		public string ImageLocation
		{
			get
			{
				return this.imageLocation;
			}
			set
			{
				this.imageLocation = value;
				this.pictureBoxState[32] = !string.IsNullOrEmpty(this.imageLocation);
				if (string.IsNullOrEmpty(this.imageLocation) && this.imageInstallationType != PictureBox.ImageInstallationType.DirectlySpecified)
				{
					this.InstallNewImage(null, PictureBox.ImageInstallationType.DirectlySpecified);
				}
				if (this.WaitOnLoad && !this.pictureBoxState[64] && !string.IsNullOrEmpty(this.imageLocation))
				{
					this.Load();
				}
				base.Invalidate();
			}
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x0600329E RID: 12958 RVA: 0x000E2A74 File Offset: 0x000E0C74
		private Rectangle ImageRectangle
		{
			get
			{
				return this.ImageRectangleFromSizeMode(this.sizeMode);
			}
		}

		// Token: 0x0600329F RID: 12959 RVA: 0x000E2A84 File Offset: 0x000E0C84
		private Rectangle ImageRectangleFromSizeMode(PictureBoxSizeMode mode)
		{
			Rectangle rectangle = LayoutUtils.DeflateRect(base.ClientRectangle, base.Padding);
			if (this.image != null)
			{
				switch (mode)
				{
				case PictureBoxSizeMode.Normal:
				case PictureBoxSizeMode.AutoSize:
					rectangle.Size = this.image.Size;
					break;
				case PictureBoxSizeMode.CenterImage:
					rectangle.X += (rectangle.Width - this.image.Width) / 2;
					rectangle.Y += (rectangle.Height - this.image.Height) / 2;
					rectangle.Size = this.image.Size;
					break;
				case PictureBoxSizeMode.Zoom:
				{
					Size size = this.image.Size;
					float num = Math.Min((float)base.ClientRectangle.Width / (float)size.Width, (float)base.ClientRectangle.Height / (float)size.Height);
					rectangle.Width = (int)((float)size.Width * num);
					rectangle.Height = (int)((float)size.Height * num);
					rectangle.X = (base.ClientRectangle.Width - rectangle.Width) / 2;
					rectangle.Y = (base.ClientRectangle.Height - rectangle.Height) / 2;
					break;
				}
				}
			}
			return rectangle;
		}

		/// <summary>Gets or sets the image displayed in the <see cref="T:System.Windows.Forms.PictureBox" /> control when the main image is loading.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> displayed in the picture box control when the main image is loading.</returns>
		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x060032A0 RID: 12960 RVA: 0x000E2BE8 File Offset: 0x000E0DE8
		// (set) Token: 0x060032A1 RID: 12961 RVA: 0x000E2C50 File Offset: 0x000E0E50
		[SRCategory("CatAsynchronous")]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("PictureBoxInitialImageDescr")]
		public Image InitialImage
		{
			get
			{
				if (this.initialImage == null && this.pictureBoxState[4])
				{
					if (this.defaultInitialImage == null)
					{
						if (PictureBox.defaultInitialImageForThread == null)
						{
							PictureBox.defaultInitialImageForThread = new Bitmap(typeof(PictureBox), "PictureBox.Loading.bmp");
						}
						this.defaultInitialImage = PictureBox.defaultInitialImageForThread;
					}
					this.initialImage = this.defaultInitialImage;
				}
				return this.initialImage;
			}
			set
			{
				if (this.InitialImage != value)
				{
					this.pictureBoxState[4] = false;
				}
				this.initialImage = value;
			}
		}

		// Token: 0x060032A2 RID: 12962 RVA: 0x000E2C70 File Offset: 0x000E0E70
		private void InstallNewImage(Image value, PictureBox.ImageInstallationType installationType)
		{
			this.StopAnimate();
			this.image = value;
			LayoutTransaction.DoLayoutIf(this.AutoSize, this, this, PropertyNames.Image);
			this.Animate();
			if (installationType != PictureBox.ImageInstallationType.ErrorOrInitial)
			{
				this.AdjustSize();
			}
			this.imageInstallationType = installationType;
			base.Invalidate();
			CommonProperties.xClearPreferredSizeCache(this);
		}

		/// <summary>Gets or sets the Input Method Editor(IME) mode supported by this control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x060032A3 RID: 12963 RVA: 0x0001A051 File Offset: 0x00018251
		// (set) Token: 0x060032A4 RID: 12964 RVA: 0x0001A059 File Offset: 0x00018259
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new ImeMode ImeMode
		{
			get
			{
				return base.ImeMode;
			}
			set
			{
				base.ImeMode = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PictureBox.ImeMode" /> property changes.</summary>
		// Token: 0x14000254 RID: 596
		// (add) Token: 0x060032A5 RID: 12965 RVA: 0x00023F70 File Offset: 0x00022170
		// (remove) Token: 0x060032A6 RID: 12966 RVA: 0x00023F79 File Offset: 0x00022179
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ImeModeChanged
		{
			add
			{
				base.ImeModeChanged += value;
			}
			remove
			{
				base.ImeModeChanged -= value;
			}
		}

		/// <summary>Displays the image specified by the <see cref="P:System.Windows.Forms.PictureBox.ImageLocation" /> property of the <see cref="T:System.Windows.Forms.PictureBox" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.PictureBox.ImageLocation" /> is <see langword="null" /> or an empty string.</exception>
		// Token: 0x060032A7 RID: 12967 RVA: 0x000E2CC0 File Offset: 0x000E0EC0
		[SRCategory("CatAsynchronous")]
		[SRDescription("PictureBoxLoad0Descr")]
		public void Load()
		{
			if (this.imageLocation == null || this.imageLocation.Length == 0)
			{
				throw new InvalidOperationException(SR.GetString("PictureBoxNoImageLocation"));
			}
			this.pictureBoxState[32] = false;
			PictureBox.ImageInstallationType imageInstallationType = PictureBox.ImageInstallationType.FromUrl;
			Image image;
			try
			{
				this.DisposeImageStream();
				Uri uri = this.CalculateUri(this.imageLocation);
				if (uri.IsFile)
				{
					this.localImageStreamReader = new StreamReader(uri.LocalPath);
					image = Image.FromStream(this.localImageStreamReader.BaseStream);
				}
				else
				{
					using (WebClient webClient = new WebClient())
					{
						this.uriImageStream = webClient.OpenRead(uri.ToString());
						image = Image.FromStream(this.uriImageStream);
					}
				}
			}
			catch
			{
				if (!base.DesignMode)
				{
					throw;
				}
				image = this.ErrorImage;
				imageInstallationType = PictureBox.ImageInstallationType.ErrorOrInitial;
			}
			this.InstallNewImage(image, imageInstallationType);
		}

		/// <summary>Sets the <see cref="P:System.Windows.Forms.PictureBox.ImageLocation" /> to the specified URL and displays the image indicated.</summary>
		/// <param name="url">The path for the image to display in the <see cref="T:System.Windows.Forms.PictureBox" />.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="url" /> is <see langword="null" /> or an empty string.</exception>
		/// <exception cref="T:System.Net.WebException">
		///   <paramref name="url" /> refers to an image on the Web that cannot be accessed.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="url" /> refers to a file that is not an image.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="url" /> refers to a file that does not exist.</exception>
		// Token: 0x060032A8 RID: 12968 RVA: 0x000E2DB0 File Offset: 0x000E0FB0
		[SRCategory("CatAsynchronous")]
		[SRDescription("PictureBoxLoad1Descr")]
		public void Load(string url)
		{
			this.ImageLocation = url;
			this.Load();
		}

		/// <summary>Loads the image asynchronously.</summary>
		// Token: 0x060032A9 RID: 12969 RVA: 0x000E2DC0 File Offset: 0x000E0FC0
		[SRCategory("CatAsynchronous")]
		[SRDescription("PictureBoxLoadAsync0Descr")]
		public void LoadAsync()
		{
			if (this.imageLocation == null || this.imageLocation.Length == 0)
			{
				throw new InvalidOperationException(SR.GetString("PictureBoxNoImageLocation"));
			}
			if (this.pictureBoxState[1])
			{
				return;
			}
			this.pictureBoxState[1] = true;
			if ((this.Image == null || this.imageInstallationType == PictureBox.ImageInstallationType.ErrorOrInitial) && this.InitialImage != null)
			{
				this.InstallNewImage(this.InitialImage, PictureBox.ImageInstallationType.ErrorOrInitial);
			}
			this.currentAsyncLoadOperation = AsyncOperationManager.CreateOperation(null);
			if (this.loadCompletedDelegate == null)
			{
				this.loadCompletedDelegate = new SendOrPostCallback(this.LoadCompletedDelegate);
				this.loadProgressDelegate = new SendOrPostCallback(this.LoadProgressDelegate);
				this.readBuffer = new byte[4096];
			}
			this.pictureBoxState[32] = false;
			this.pictureBoxState[2] = false;
			this.contentLength = -1;
			this.tempDownloadStream = new MemoryStream();
			WebRequest webRequest = WebRequest.Create(this.CalculateUri(this.imageLocation));
			new WaitCallback(this.BeginGetResponseDelegate).BeginInvoke(webRequest, null, null);
		}

		// Token: 0x060032AA RID: 12970 RVA: 0x000E2ED0 File Offset: 0x000E10D0
		private void BeginGetResponseDelegate(object arg)
		{
			WebRequest webRequest = (WebRequest)arg;
			webRequest.BeginGetResponse(new AsyncCallback(this.GetResponseCallback), webRequest);
		}

		// Token: 0x060032AB RID: 12971 RVA: 0x000E2EF8 File Offset: 0x000E10F8
		private void PostCompleted(Exception error, bool cancelled)
		{
			AsyncOperation asyncOperation = this.currentAsyncLoadOperation;
			this.currentAsyncLoadOperation = null;
			if (asyncOperation != null)
			{
				asyncOperation.PostOperationCompleted(this.loadCompletedDelegate, new AsyncCompletedEventArgs(error, cancelled, null));
			}
		}

		// Token: 0x060032AC RID: 12972 RVA: 0x000E2F2C File Offset: 0x000E112C
		private void LoadCompletedDelegate(object arg)
		{
			AsyncCompletedEventArgs asyncCompletedEventArgs = (AsyncCompletedEventArgs)arg;
			Image image = this.ErrorImage;
			PictureBox.ImageInstallationType imageInstallationType = PictureBox.ImageInstallationType.ErrorOrInitial;
			if (!asyncCompletedEventArgs.Cancelled && asyncCompletedEventArgs.Error == null)
			{
				try
				{
					image = Image.FromStream(this.tempDownloadStream);
					imageInstallationType = PictureBox.ImageInstallationType.FromUrl;
				}
				catch (Exception ex)
				{
					asyncCompletedEventArgs = new AsyncCompletedEventArgs(ex, false, null);
				}
			}
			if (!asyncCompletedEventArgs.Cancelled)
			{
				this.InstallNewImage(image, imageInstallationType);
			}
			this.tempDownloadStream = null;
			this.pictureBoxState[2] = false;
			this.pictureBoxState[1] = false;
			this.OnLoadCompleted(asyncCompletedEventArgs);
		}

		// Token: 0x060032AD RID: 12973 RVA: 0x000E2FC0 File Offset: 0x000E11C0
		private void LoadProgressDelegate(object arg)
		{
			this.OnLoadProgressChanged((ProgressChangedEventArgs)arg);
		}

		// Token: 0x060032AE RID: 12974 RVA: 0x000E2FD0 File Offset: 0x000E11D0
		private void GetResponseCallback(IAsyncResult result)
		{
			if (this.pictureBoxState[2])
			{
				this.PostCompleted(null, true);
				return;
			}
			try
			{
				WebRequest webRequest = (WebRequest)result.AsyncState;
				WebResponse webResponse = webRequest.EndGetResponse(result);
				this.contentLength = (int)webResponse.ContentLength;
				this.totalBytesRead = 0;
				Stream responseStream = webResponse.GetResponseStream();
				responseStream.BeginRead(this.readBuffer, 0, 4096, new AsyncCallback(this.ReadCallBack), responseStream);
			}
			catch (Exception ex)
			{
				this.PostCompleted(ex, false);
			}
		}

		// Token: 0x060032AF RID: 12975 RVA: 0x000E3064 File Offset: 0x000E1264
		private void ReadCallBack(IAsyncResult result)
		{
			if (this.pictureBoxState[2])
			{
				this.PostCompleted(null, true);
				return;
			}
			Stream stream = (Stream)result.AsyncState;
			try
			{
				int num = stream.EndRead(result);
				if (num > 0)
				{
					this.totalBytesRead += num;
					this.tempDownloadStream.Write(this.readBuffer, 0, num);
					stream.BeginRead(this.readBuffer, 0, 4096, new AsyncCallback(this.ReadCallBack), stream);
					if (this.contentLength != -1)
					{
						int num2 = (int)(100f * ((float)this.totalBytesRead / (float)this.contentLength));
						if (this.currentAsyncLoadOperation != null)
						{
							this.currentAsyncLoadOperation.Post(this.loadProgressDelegate, new ProgressChangedEventArgs(num2, null));
						}
					}
				}
				else
				{
					this.tempDownloadStream.Seek(0L, SeekOrigin.Begin);
					if (this.currentAsyncLoadOperation != null)
					{
						this.currentAsyncLoadOperation.Post(this.loadProgressDelegate, new ProgressChangedEventArgs(100, null));
					}
					this.PostCompleted(null, false);
					Stream stream2 = stream;
					stream = null;
					stream2.Close();
				}
			}
			catch (Exception ex)
			{
				this.PostCompleted(ex, false);
				if (stream != null)
				{
					stream.Close();
				}
			}
		}

		/// <summary>Loads the image at the specified location, asynchronously.</summary>
		/// <param name="url">The path for the image to display in the <see cref="T:System.Windows.Forms.PictureBox" />.</param>
		// Token: 0x060032B0 RID: 12976 RVA: 0x000E3190 File Offset: 0x000E1390
		[SRCategory("CatAsynchronous")]
		[SRDescription("PictureBoxLoadAsync1Descr")]
		public void LoadAsync(string url)
		{
			this.ImageLocation = url;
			this.LoadAsync();
		}

		/// <summary>Occurs when the asynchronous image-load operation is completed, been canceled, or raised an exception.</summary>
		// Token: 0x14000255 RID: 597
		// (add) Token: 0x060032B1 RID: 12977 RVA: 0x000E319F File Offset: 0x000E139F
		// (remove) Token: 0x060032B2 RID: 12978 RVA: 0x000E31B2 File Offset: 0x000E13B2
		[SRCategory("CatAsynchronous")]
		[SRDescription("PictureBoxLoadCompletedDescr")]
		public event AsyncCompletedEventHandler LoadCompleted
		{
			add
			{
				base.Events.AddHandler(PictureBox.loadCompletedKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(PictureBox.loadCompletedKey, value);
			}
		}

		/// <summary>Occurs when the progress of an asynchronous image-loading operation has changed.</summary>
		// Token: 0x14000256 RID: 598
		// (add) Token: 0x060032B3 RID: 12979 RVA: 0x000E31C5 File Offset: 0x000E13C5
		// (remove) Token: 0x060032B4 RID: 12980 RVA: 0x000E31D8 File Offset: 0x000E13D8
		[SRCategory("CatAsynchronous")]
		[SRDescription("PictureBoxLoadProgressChangedDescr")]
		public event ProgressChangedEventHandler LoadProgressChanged
		{
			add
			{
				base.Events.AddHandler(PictureBox.loadProgressChangedKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(PictureBox.loadProgressChangedKey, value);
			}
		}

		// Token: 0x060032B5 RID: 12981 RVA: 0x000E31EB File Offset: 0x000E13EB
		private void ResetInitialImage()
		{
			this.pictureBoxState[4] = true;
			this.initialImage = this.defaultInitialImage;
		}

		// Token: 0x060032B6 RID: 12982 RVA: 0x000E3206 File Offset: 0x000E1406
		private void ResetErrorImage()
		{
			this.pictureBoxState[8] = true;
			this.errorImage = this.defaultErrorImage;
		}

		// Token: 0x060032B7 RID: 12983 RVA: 0x000E3221 File Offset: 0x000E1421
		private void ResetImage()
		{
			this.InstallNewImage(null, PictureBox.ImageInstallationType.DirectlySpecified);
		}

		/// <summary>Gets or sets a value indicating whether control's elements are aligned to support locales using right-to-left languages.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.RightToLeft" /> values.</returns>
		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x060032B8 RID: 12984 RVA: 0x000E322B File Offset: 0x000E142B
		// (set) Token: 0x060032B9 RID: 12985 RVA: 0x000C5F21 File Offset: 0x000C4121
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override RightToLeft RightToLeft
		{
			get
			{
				return base.RightToLeft;
			}
			set
			{
				base.RightToLeft = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PictureBox.RightToLeft" /> property changes.</summary>
		// Token: 0x14000257 RID: 599
		// (add) Token: 0x060032BA RID: 12986 RVA: 0x000E3233 File Offset: 0x000E1433
		// (remove) Token: 0x060032BB RID: 12987 RVA: 0x000E323C File Offset: 0x000E143C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler RightToLeftChanged
		{
			add
			{
				base.RightToLeftChanged += value;
			}
			remove
			{
				base.RightToLeftChanged -= value;
			}
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x000E3245 File Offset: 0x000E1445
		private bool ShouldSerializeInitialImage()
		{
			return !this.pictureBoxState[4];
		}

		// Token: 0x060032BD RID: 12989 RVA: 0x000E3256 File Offset: 0x000E1456
		private bool ShouldSerializeErrorImage()
		{
			return !this.pictureBoxState[8];
		}

		// Token: 0x060032BE RID: 12990 RVA: 0x000E3267 File Offset: 0x000E1467
		private bool ShouldSerializeImage()
		{
			return this.imageInstallationType == PictureBox.ImageInstallationType.DirectlySpecified && this.Image != null;
		}

		/// <summary>Indicates how the image is displayed.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.PictureBoxSizeMode" /> values. The default is <see cref="F:System.Windows.Forms.PictureBoxSizeMode.Normal" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.PictureBoxSizeMode" /> values.</exception>
		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x060032BF RID: 12991 RVA: 0x000E327C File Offset: 0x000E147C
		// (set) Token: 0x060032C0 RID: 12992 RVA: 0x000E3284 File Offset: 0x000E1484
		[DefaultValue(PictureBoxSizeMode.Normal)]
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[SRDescription("PictureBoxSizeModeDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public PictureBoxSizeMode SizeMode
		{
			get
			{
				return this.sizeMode;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 4))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(PictureBoxSizeMode));
				}
				if (this.sizeMode != value)
				{
					if (value == PictureBoxSizeMode.AutoSize)
					{
						this.AutoSize = true;
						base.SetStyle(ControlStyles.FixedWidth | ControlStyles.FixedHeight, true);
					}
					if (value != PictureBoxSizeMode.AutoSize)
					{
						this.AutoSize = false;
						base.SetStyle(ControlStyles.FixedWidth | ControlStyles.FixedHeight, false);
						this.savedSize = base.Size;
					}
					this.sizeMode = value;
					this.AdjustSize();
					base.Invalidate();
					this.OnSizeModeChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when <see cref="P:System.Windows.Forms.PictureBox.SizeMode" /> changes.</summary>
		// Token: 0x14000258 RID: 600
		// (add) Token: 0x060032C1 RID: 12993 RVA: 0x000E3312 File Offset: 0x000E1512
		// (remove) Token: 0x060032C2 RID: 12994 RVA: 0x000E3325 File Offset: 0x000E1525
		[SRCategory("CatPropertyChanged")]
		[SRDescription("PictureBoxOnSizeModeChangedDescr")]
		public event EventHandler SizeModeChanged
		{
			add
			{
				base.Events.AddHandler(PictureBox.EVENT_SIZEMODECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(PictureBox.EVENT_SIZEMODECHANGED, value);
			}
		}

		/// <summary>Gets or sets a value that indicates whether the user can give the focus to this control by using the TAB key.</summary>
		/// <returns>
		///   <see langword="true" /> if the user can give the focus to the control by using the TAB key; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x060032C3 RID: 12995 RVA: 0x000B2395 File Offset: 0x000B0595
		// (set) Token: 0x060032C4 RID: 12996 RVA: 0x000B239D File Offset: 0x000B059D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool TabStop
		{
			get
			{
				return base.TabStop;
			}
			set
			{
				base.TabStop = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PictureBox.TabStop" /> property changes.</summary>
		// Token: 0x14000259 RID: 601
		// (add) Token: 0x060032C5 RID: 12997 RVA: 0x000B23A6 File Offset: 0x000B05A6
		// (remove) Token: 0x060032C6 RID: 12998 RVA: 0x000B23AF File Offset: 0x000B05AF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TabStopChanged
		{
			add
			{
				base.TabStopChanged += value;
			}
			remove
			{
				base.TabStopChanged -= value;
			}
		}

		/// <summary>Gets or sets the tab index value.</summary>
		/// <returns>The tab index value.</returns>
		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x060032C7 RID: 12999 RVA: 0x000B2372 File Offset: 0x000B0572
		// (set) Token: 0x060032C8 RID: 13000 RVA: 0x000B237A File Offset: 0x000B057A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new int TabIndex
		{
			get
			{
				return base.TabIndex;
			}
			set
			{
				base.TabIndex = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PictureBox.TabIndex" /> property changes.</summary>
		// Token: 0x1400025A RID: 602
		// (add) Token: 0x060032C9 RID: 13001 RVA: 0x000B2383 File Offset: 0x000B0583
		// (remove) Token: 0x060032CA RID: 13002 RVA: 0x000B238C File Offset: 0x000B058C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TabIndexChanged
		{
			add
			{
				base.TabIndexChanged += value;
			}
			remove
			{
				base.TabIndexChanged -= value;
			}
		}

		/// <summary>Gets or sets the text of the <see cref="T:System.Windows.Forms.PictureBox" />.</summary>
		/// <returns>The text of the <see cref="T:System.Windows.Forms.PictureBox" />.</returns>
		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x060032CB RID: 13003 RVA: 0x00013814 File Offset: 0x00011A14
		// (set) Token: 0x060032CC RID: 13004 RVA: 0x00023FE9 File Offset: 0x000221E9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PictureBox.Text" /> property changes.</summary>
		// Token: 0x1400025B RID: 603
		// (add) Token: 0x060032CD RID: 13005 RVA: 0x00046591 File Offset: 0x00044791
		// (remove) Token: 0x060032CE RID: 13006 RVA: 0x0004659A File Offset: 0x0004479A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TextChanged
		{
			add
			{
				base.TextChanged += value;
			}
			remove
			{
				base.TextChanged -= value;
			}
		}

		/// <summary>Overrides the <see cref="E:System.Windows.Forms.Control.Enter" /> property.</summary>
		// Token: 0x1400025C RID: 604
		// (add) Token: 0x060032CF RID: 13007 RVA: 0x000E3338 File Offset: 0x000E1538
		// (remove) Token: 0x060032D0 RID: 13008 RVA: 0x000E3341 File Offset: 0x000E1541
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Enter
		{
			add
			{
				base.Enter += value;
			}
			remove
			{
				base.Enter -= value;
			}
		}

		/// <summary>Occurs when a key is released when the control has focus.</summary>
		// Token: 0x1400025D RID: 605
		// (add) Token: 0x060032D1 RID: 13009 RVA: 0x000B9104 File Offset: 0x000B7304
		// (remove) Token: 0x060032D2 RID: 13010 RVA: 0x000B910D File Offset: 0x000B730D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyEventHandler KeyUp
		{
			add
			{
				base.KeyUp += value;
			}
			remove
			{
				base.KeyUp -= value;
			}
		}

		/// <summary>Occurs when a key is pressed when the control has focus.</summary>
		// Token: 0x1400025E RID: 606
		// (add) Token: 0x060032D3 RID: 13011 RVA: 0x000B9116 File Offset: 0x000B7316
		// (remove) Token: 0x060032D4 RID: 13012 RVA: 0x000B911F File Offset: 0x000B731F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyEventHandler KeyDown
		{
			add
			{
				base.KeyDown += value;
			}
			remove
			{
				base.KeyDown -= value;
			}
		}

		/// <summary>Occurs when a key is pressed when the control has focus.</summary>
		// Token: 0x1400025F RID: 607
		// (add) Token: 0x060032D5 RID: 13013 RVA: 0x000B9128 File Offset: 0x000B7328
		// (remove) Token: 0x060032D6 RID: 13014 RVA: 0x000B9131 File Offset: 0x000B7331
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyPressEventHandler KeyPress
		{
			add
			{
				base.KeyPress += value;
			}
			remove
			{
				base.KeyPress -= value;
			}
		}

		/// <summary>Occurs when input focus leaves the <see cref="T:System.Windows.Forms.PictureBox" />.</summary>
		// Token: 0x14000260 RID: 608
		// (add) Token: 0x060032D7 RID: 13015 RVA: 0x000E334A File Offset: 0x000E154A
		// (remove) Token: 0x060032D8 RID: 13016 RVA: 0x000E3353 File Offset: 0x000E1553
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Leave
		{
			add
			{
				base.Leave += value;
			}
			remove
			{
				base.Leave -= value;
			}
		}

		// Token: 0x060032D9 RID: 13017 RVA: 0x000E335C File Offset: 0x000E155C
		private void AdjustSize()
		{
			if (this.sizeMode == PictureBoxSizeMode.AutoSize)
			{
				base.Size = base.PreferredSize;
				return;
			}
			base.Size = this.savedSize;
		}

		// Token: 0x060032DA RID: 13018 RVA: 0x000E3380 File Offset: 0x000E1580
		private void Animate()
		{
			this.Animate(!base.DesignMode && base.Visible && base.Enabled && this.ParentInternal != null);
		}

		// Token: 0x060032DB RID: 13019 RVA: 0x000E33AC File Offset: 0x000E15AC
		private void StopAnimate()
		{
			this.Animate(false);
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x000E33B8 File Offset: 0x000E15B8
		private void Animate(bool animate)
		{
			if (animate != this.currentlyAnimating)
			{
				if (animate)
				{
					if (this.image != null)
					{
						ImageAnimator.Animate(this.image, new EventHandler(this.OnFrameChanged));
						this.currentlyAnimating = animate;
						return;
					}
				}
				else if (this.image != null)
				{
					ImageAnimator.StopAnimate(this.image, new EventHandler(this.OnFrameChanged));
					this.currentlyAnimating = animate;
				}
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.PictureBox" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release managed and unmanaged resources; <see langword="false" /> to release unmanaged resources only.</param>
		// Token: 0x060032DD RID: 13021 RVA: 0x000E341E File Offset: 0x000E161E
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.StopAnimate();
			}
			this.DisposeImageStream();
			base.Dispose(disposing);
		}

		// Token: 0x060032DE RID: 13022 RVA: 0x000E3436 File Offset: 0x000E1636
		private void DisposeImageStream()
		{
			if (this.localImageStreamReader != null)
			{
				this.localImageStreamReader.Dispose();
				this.localImageStreamReader = null;
			}
			if (this.uriImageStream != null)
			{
				this.uriImageStream.Dispose();
				this.localImageStreamReader = null;
			}
		}

		// Token: 0x060032DF RID: 13023 RVA: 0x000E346C File Offset: 0x000E166C
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			if (this.image == null)
			{
				return CommonProperties.GetSpecifiedBounds(this).Size;
			}
			Size size = this.SizeFromClientSize(Size.Empty) + base.Padding.Size;
			return this.image.Size + size;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.EnabledChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060032E0 RID: 13024 RVA: 0x000E34C0 File Offset: 0x000E16C0
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			this.Animate();
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x000E34D0 File Offset: 0x000E16D0
		private void OnFrameChanged(object o, EventArgs e)
		{
			if (base.Disposing || base.IsDisposed)
			{
				return;
			}
			if (base.InvokeRequired && base.IsHandleCreated)
			{
				object obj = this.internalSyncObject;
				lock (obj)
				{
					if (this.handleValid)
					{
						base.BeginInvoke(new EventHandler(this.OnFrameChanged), new object[] { o, e });
					}
					return;
				}
			}
			base.Invalidate();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060032E2 RID: 13026 RVA: 0x000E355C File Offset: 0x000E175C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnHandleDestroyed(EventArgs e)
		{
			object obj = this.internalSyncObject;
			lock (obj)
			{
				this.handleValid = false;
			}
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060032E3 RID: 13027 RVA: 0x000E35A4 File Offset: 0x000E17A4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnHandleCreated(EventArgs e)
		{
			object obj = this.internalSyncObject;
			lock (obj)
			{
				this.handleValid = true;
			}
			base.OnHandleCreated(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.PictureBox.LoadCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" /> that contains the event data.</param>
		// Token: 0x060032E4 RID: 13028 RVA: 0x000E35EC File Offset: 0x000E17EC
		protected virtual void OnLoadCompleted(AsyncCompletedEventArgs e)
		{
			AsyncCompletedEventHandler asyncCompletedEventHandler = (AsyncCompletedEventHandler)base.Events[PictureBox.loadCompletedKey];
			if (asyncCompletedEventHandler != null)
			{
				asyncCompletedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.PictureBox.LoadProgressChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.ProgressChangedEventArgs" /> that contains the event data.</param>
		// Token: 0x060032E5 RID: 13029 RVA: 0x000E361C File Offset: 0x000E181C
		protected virtual void OnLoadProgressChanged(ProgressChangedEventArgs e)
		{
			ProgressChangedEventHandler progressChangedEventHandler = (ProgressChangedEventHandler)base.Events[PictureBox.loadProgressChangedKey];
			if (progressChangedEventHandler != null)
			{
				progressChangedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="pe">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x060032E6 RID: 13030 RVA: 0x000E364C File Offset: 0x000E184C
		protected override void OnPaint(PaintEventArgs pe)
		{
			if (this.pictureBoxState[32])
			{
				try
				{
					if (this.WaitOnLoad)
					{
						this.Load();
					}
					else
					{
						this.LoadAsync();
					}
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsCriticalException(ex))
					{
						throw;
					}
					this.image = this.ErrorImage;
				}
			}
			if (this.image != null)
			{
				this.Animate();
				ImageAnimator.UpdateFrames(this.Image);
				Rectangle rectangle = ((this.imageInstallationType == PictureBox.ImageInstallationType.ErrorOrInitial) ? this.ImageRectangleFromSizeMode(PictureBoxSizeMode.CenterImage) : this.ImageRectangle);
				pe.Graphics.DrawImage(this.image, rectangle);
			}
			base.OnPaint(pe);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.VisibleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x060032E7 RID: 13031 RVA: 0x000E36F4 File Offset: 0x000E18F4
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			this.Animate();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ParentChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060032E8 RID: 13032 RVA: 0x000E3703 File Offset: 0x000E1903
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
			this.Animate();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060032E9 RID: 13033 RVA: 0x000E3712 File Offset: 0x000E1912
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (this.sizeMode == PictureBoxSizeMode.Zoom || this.sizeMode == PictureBoxSizeMode.StretchImage || this.sizeMode == PictureBoxSizeMode.CenterImage || this.BackgroundImage != null)
			{
				base.Invalidate();
			}
			this.savedSize = base.Size;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.PictureBox.SizeModeChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060032EA RID: 13034 RVA: 0x000E3750 File Offset: 0x000E1950
		protected virtual void OnSizeModeChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[PictureBox.EVENT_SIZEMODECHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Windows.Forms.PictureBox" /> control.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.PictureBox" />.</returns>
		// Token: 0x060032EB RID: 13035 RVA: 0x000E3780 File Offset: 0x000E1980
		public override string ToString()
		{
			string text = base.ToString();
			return text + ", SizeMode: " + this.sizeMode.ToString("G");
		}

		/// <summary>Gets or sets a value indicating whether an image is loaded synchronously.</summary>
		/// <returns>
		///   <see langword="true" /> if an image-loading operation is completed synchronously; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x060032EC RID: 13036 RVA: 0x000E37B4 File Offset: 0x000E19B4
		// (set) Token: 0x060032ED RID: 13037 RVA: 0x000E37C3 File Offset: 0x000E19C3
		[SRCategory("CatAsynchronous")]
		[Localizable(true)]
		[DefaultValue(false)]
		[SRDescription("PictureBoxWaitOnLoadDescr")]
		public bool WaitOnLoad
		{
			get
			{
				return this.pictureBoxState[16];
			}
			set
			{
				this.pictureBoxState[16] = value;
			}
		}

		/// <summary>Signals the object that initialization is starting.</summary>
		// Token: 0x060032EE RID: 13038 RVA: 0x000E37D3 File Offset: 0x000E19D3
		void ISupportInitialize.BeginInit()
		{
			this.pictureBoxState[64] = true;
		}

		/// <summary>Signals to the object that initialization is complete.</summary>
		// Token: 0x060032EF RID: 13039 RVA: 0x000E37E3 File Offset: 0x000E19E3
		void ISupportInitialize.EndInit()
		{
			if (this.ImageLocation != null && this.ImageLocation.Length != 0 && this.WaitOnLoad)
			{
				this.Load();
			}
			this.pictureBoxState[64] = false;
		}

		// Token: 0x04001E6F RID: 7791
		private BorderStyle borderStyle;

		// Token: 0x04001E70 RID: 7792
		private Image image;

		// Token: 0x04001E71 RID: 7793
		private PictureBoxSizeMode sizeMode;

		// Token: 0x04001E72 RID: 7794
		private Size savedSize;

		// Token: 0x04001E73 RID: 7795
		private bool currentlyAnimating;

		// Token: 0x04001E74 RID: 7796
		private AsyncOperation currentAsyncLoadOperation;

		// Token: 0x04001E75 RID: 7797
		private string imageLocation;

		// Token: 0x04001E76 RID: 7798
		private Image initialImage;

		// Token: 0x04001E77 RID: 7799
		private Image errorImage;

		// Token: 0x04001E78 RID: 7800
		private int contentLength;

		// Token: 0x04001E79 RID: 7801
		private int totalBytesRead;

		// Token: 0x04001E7A RID: 7802
		private MemoryStream tempDownloadStream;

		// Token: 0x04001E7B RID: 7803
		private const int readBlockSize = 4096;

		// Token: 0x04001E7C RID: 7804
		private byte[] readBuffer;

		// Token: 0x04001E7D RID: 7805
		private PictureBox.ImageInstallationType imageInstallationType;

		// Token: 0x04001E7E RID: 7806
		private SendOrPostCallback loadCompletedDelegate;

		// Token: 0x04001E7F RID: 7807
		private SendOrPostCallback loadProgressDelegate;

		// Token: 0x04001E80 RID: 7808
		private bool handleValid;

		// Token: 0x04001E81 RID: 7809
		private object internalSyncObject = new object();

		// Token: 0x04001E82 RID: 7810
		private Image defaultInitialImage;

		// Token: 0x04001E83 RID: 7811
		private Image defaultErrorImage;

		// Token: 0x04001E84 RID: 7812
		[ThreadStatic]
		private static Image defaultInitialImageForThread = null;

		// Token: 0x04001E85 RID: 7813
		[ThreadStatic]
		private static Image defaultErrorImageForThread = null;

		// Token: 0x04001E86 RID: 7814
		private static readonly object defaultInitialImageKey = new object();

		// Token: 0x04001E87 RID: 7815
		private static readonly object defaultErrorImageKey = new object();

		// Token: 0x04001E88 RID: 7816
		private static readonly object loadCompletedKey = new object();

		// Token: 0x04001E89 RID: 7817
		private static readonly object loadProgressChangedKey = new object();

		// Token: 0x04001E8A RID: 7818
		private const int PICTUREBOXSTATE_asyncOperationInProgress = 1;

		// Token: 0x04001E8B RID: 7819
		private const int PICTUREBOXSTATE_cancellationPending = 2;

		// Token: 0x04001E8C RID: 7820
		private const int PICTUREBOXSTATE_useDefaultInitialImage = 4;

		// Token: 0x04001E8D RID: 7821
		private const int PICTUREBOXSTATE_useDefaultErrorImage = 8;

		// Token: 0x04001E8E RID: 7822
		private const int PICTUREBOXSTATE_waitOnLoad = 16;

		// Token: 0x04001E8F RID: 7823
		private const int PICTUREBOXSTATE_needToLoadImageLocation = 32;

		// Token: 0x04001E90 RID: 7824
		private const int PICTUREBOXSTATE_inInitialization = 64;

		// Token: 0x04001E91 RID: 7825
		private BitVector32 pictureBoxState;

		// Token: 0x04001E92 RID: 7826
		private StreamReader localImageStreamReader;

		// Token: 0x04001E93 RID: 7827
		private Stream uriImageStream;

		// Token: 0x04001E94 RID: 7828
		private static readonly object EVENT_SIZEMODECHANGED = new object();

		// Token: 0x020007C9 RID: 1993
		private enum ImageInstallationType
		{
			// Token: 0x040041BB RID: 16827
			DirectlySpecified,
			// Token: 0x040041BC RID: 16828
			ErrorOrInitial,
			// Token: 0x040041BD RID: 16829
			FromUrl
		}
	}
}
