using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Windows.Forms
{
	/// <summary>Provides methods to manage a collection of <see cref="T:System.Drawing.Image" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000293 RID: 659
	[Designer("System.Windows.Forms.Design.ImageListDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxItemFilter("System.Windows.Forms")]
	[DefaultProperty("Images")]
	[TypeConverter(typeof(ImageListConverter))]
	[DesignerSerializer("System.Windows.Forms.Design.ImageListCodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionImageList")]
	public sealed class ImageList : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ImageList" /> class with default values for <see cref="P:System.Windows.Forms.ImageList.ColorDepth" />, <see cref="P:System.Windows.Forms.ImageList.ImageSize" />, and <see cref="P:System.Windows.Forms.ImageList.TransparentColor" />.</summary>
		// Token: 0x060029C6 RID: 10694 RVA: 0x000BDFF8 File Offset: 0x000BC1F8
		public ImageList()
		{
			if (!ImageList.isScalingInitialized)
			{
				if (DpiHelper.IsScalingRequired)
				{
					ImageList.maxImageWidth = DpiHelper.LogicalToDeviceUnitsX(256);
					ImageList.maxImageHeight = DpiHelper.LogicalToDeviceUnitsY(256);
				}
				ImageList.isScalingInitialized = true;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ImageList" /> class, associating it with a container.</summary>
		/// <param name="container">An object implementing <see cref="T:System.ComponentModel.IContainer" /> to associate with this instance of <see cref="T:System.Windows.Forms.ImageList" />.</param>
		// Token: 0x060029C7 RID: 10695 RVA: 0x000BE065 File Offset: 0x000BC265
		public ImageList(IContainer container)
			: this()
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			container.Add(this);
		}

		/// <summary>Gets the color depth of the image list.</summary>
		/// <returns>The number of available colors for the image. In the .NET Framework version 1.0, the default is <see cref="F:System.Windows.Forms.ColorDepth.Depth4Bit" />. In the .NET Framework version 1.1 or later, the default is <see cref="F:System.Windows.Forms.ColorDepth.Depth8Bit" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The color depth is not a valid <see cref="T:System.Windows.Forms.ColorDepth" /> enumeration value.</exception>
		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x060029C8 RID: 10696 RVA: 0x000BE082 File Offset: 0x000BC282
		// (set) Token: 0x060029C9 RID: 10697 RVA: 0x000BE08C File Offset: 0x000BC28C
		[SRCategory("CatAppearance")]
		[SRDescription("ImageListColorDepthDescr")]
		public ColorDepth ColorDepth
		{
			get
			{
				return this.colorDepth;
			}
			set
			{
				if (!ClientUtils.IsEnumValid_NotSequential(value, (int)value, new int[] { 4, 8, 16, 24, 32 }))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ColorDepth));
				}
				if (this.colorDepth != value)
				{
					this.colorDepth = value;
					this.PerformRecreateHandle("ColorDepth");
				}
			}
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x000BE0E9 File Offset: 0x000BC2E9
		private bool ShouldSerializeColorDepth()
		{
			return this.Images.Count == 0;
		}

		// Token: 0x060029CB RID: 10699 RVA: 0x000BE0F9 File Offset: 0x000BC2F9
		private void ResetColorDepth()
		{
			this.ColorDepth = ColorDepth.Depth8Bit;
		}

		/// <summary>Gets the handle of the image list object.</summary>
		/// <returns>The handle for the image list. The default is <see langword="null" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">Creating the handle for the <see cref="T:System.Windows.Forms.ImageList" /> failed.</exception>
		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x060029CC RID: 10700 RVA: 0x000BE102 File Offset: 0x000BC302
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ImageListHandleDescr")]
		public IntPtr Handle
		{
			get
			{
				if (this.nativeImageList == null)
				{
					this.CreateHandle();
				}
				return this.nativeImageList.Handle;
			}
		}

		/// <summary>Gets a value indicating whether the underlying Win32 handle has been created.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Windows.Forms.ImageList.Handle" /> has been created; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x060029CD RID: 10701 RVA: 0x000BE11D File Offset: 0x000BC31D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ImageListHandleCreatedDescr")]
		public bool HandleCreated
		{
			get
			{
				return this.nativeImageList != null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ImageList.ImageCollection" /> for this image list.</summary>
		/// <returns>The collection of images.</returns>
		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x060029CE RID: 10702 RVA: 0x000BE128 File Offset: 0x000BC328
		[SRCategory("CatAppearance")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ImageListImagesDescr")]
		[MergableProperty(false)]
		public ImageList.ImageCollection Images
		{
			get
			{
				if (this.imageCollection == null)
				{
					this.imageCollection = new ImageList.ImageCollection(this);
				}
				return this.imageCollection;
			}
		}

		/// <summary>Gets or sets the size of the images in the image list.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" /> that defines the height and width, in pixels, of the images in the list. The default size is 16 by 16. The maximum size is 256 by 256.</returns>
		/// <exception cref="T:System.ArgumentException">The value assigned is equal to <see cref="P:System.Drawing.Size.IsEmpty" />.  
		///  -or-  
		///  The value of the height or width is less than or equal to 0.  
		///  -or-  
		///  The value of the height or width is greater than 256.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The new size has a dimension less than 0 or greater than 256.</exception>
		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x060029CF RID: 10703 RVA: 0x000BE144 File Offset: 0x000BC344
		// (set) Token: 0x060029D0 RID: 10704 RVA: 0x000BE14C File Offset: 0x000BC34C
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[SRDescription("ImageListSizeDescr")]
		public Size ImageSize
		{
			get
			{
				return this.imageSize;
			}
			set
			{
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("InvalidArgument", new object[] { "ImageSize", "Size.Empty" }));
				}
				if (value.Width <= 0 || value.Width > ImageList.maxImageWidth)
				{
					throw new ArgumentOutOfRangeException("ImageSize", SR.GetString("InvalidBoundArgument", new object[]
					{
						"ImageSize.Width",
						value.Width.ToString(CultureInfo.CurrentCulture),
						1.ToString(CultureInfo.CurrentCulture),
						ImageList.maxImageWidth.ToString()
					}));
				}
				if (value.Height <= 0 || value.Height > ImageList.maxImageHeight)
				{
					throw new ArgumentOutOfRangeException("ImageSize", SR.GetString("InvalidBoundArgument", new object[]
					{
						"ImageSize.Height",
						value.Height.ToString(CultureInfo.CurrentCulture),
						1.ToString(CultureInfo.CurrentCulture),
						ImageList.maxImageHeight.ToString()
					}));
				}
				if (this.imageSize.Width != value.Width || this.imageSize.Height != value.Height)
				{
					this.imageSize = new Size(value.Width, value.Height);
					this.PerformRecreateHandle("ImageSize");
				}
			}
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x000BE0E9 File Offset: 0x000BC2E9
		private bool ShouldSerializeImageSize()
		{
			return this.Images.Count == 0;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ImageListStreamer" /> associated with this image list.</summary>
		/// <returns>
		///   <see langword="null" /> if the image list is empty; otherwise, a <see cref="T:System.Windows.Forms.ImageListStreamer" /> for this <see cref="T:System.Windows.Forms.ImageList" />.</returns>
		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x060029D2 RID: 10706 RVA: 0x000BE2B4 File Offset: 0x000BC4B4
		// (set) Token: 0x060029D3 RID: 10707 RVA: 0x000BE2CC File Offset: 0x000BC4CC
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DefaultValue(null)]
		[SRDescription("ImageListImageStreamDescr")]
		public ImageListStreamer ImageStream
		{
			get
			{
				if (this.Images.Empty)
				{
					return null;
				}
				return new ImageListStreamer(this);
			}
			set
			{
				if (value != null)
				{
					ImageList.NativeImageList nativeImageList = value.GetNativeImageList();
					if (nativeImageList != null && nativeImageList != this.nativeImageList)
					{
						bool handleCreated = this.HandleCreated;
						this.DestroyHandle();
						this.originals = null;
						this.nativeImageList = new ImageList.NativeImageList(SafeNativeMethods.ImageList_Duplicate(new HandleRef(nativeImageList, nativeImageList.Handle)));
						int num;
						int num2;
						if (SafeNativeMethods.ImageList_GetIconSize(new HandleRef(this, this.nativeImageList.Handle), out num, out num2))
						{
							this.imageSize = new Size(num, num2);
						}
						NativeMethods.IMAGEINFO imageinfo = new NativeMethods.IMAGEINFO();
						if (SafeNativeMethods.ImageList_GetImageInfo(new HandleRef(this, this.nativeImageList.Handle), 0, imageinfo))
						{
							NativeMethods.BITMAP bitmap = new NativeMethods.BITMAP();
							UnsafeNativeMethods.GetObject(new HandleRef(null, imageinfo.hbmImage), Marshal.SizeOf(bitmap), bitmap);
							short bmBitsPixel = bitmap.bmBitsPixel;
							if (bmBitsPixel <= 8)
							{
								if (bmBitsPixel != 4)
								{
									if (bmBitsPixel == 8)
									{
										this.colorDepth = ColorDepth.Depth8Bit;
									}
								}
								else
								{
									this.colorDepth = ColorDepth.Depth4Bit;
								}
							}
							else if (bmBitsPixel != 16)
							{
								if (bmBitsPixel != 24)
								{
									if (bmBitsPixel == 32)
									{
										this.colorDepth = ColorDepth.Depth32Bit;
									}
								}
								else
								{
									this.colorDepth = ColorDepth.Depth24Bit;
								}
							}
							else
							{
								this.colorDepth = ColorDepth.Depth16Bit;
							}
						}
						this.Images.ResetKeys();
						if (handleCreated)
						{
							this.OnRecreateHandle(new EventArgs());
							return;
						}
					}
				}
				else
				{
					this.DestroyHandle();
					this.Images.Clear();
				}
			}
		}

		/// <summary>Gets or sets an object that contains additional data about the <see cref="T:System.Windows.Forms.ImageList" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains additional data about the <see cref="T:System.Windows.Forms.ImageList" />.</returns>
		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x060029D4 RID: 10708 RVA: 0x000BE41D File Offset: 0x000BC61D
		// (set) Token: 0x060029D5 RID: 10709 RVA: 0x000BE425 File Offset: 0x000BC625
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

		/// <summary>Gets or sets the color to treat as transparent.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Color" /> values. The default is <see langword="Transparent" />.</returns>
		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x060029D6 RID: 10710 RVA: 0x000BE42E File Offset: 0x000BC62E
		// (set) Token: 0x060029D7 RID: 10711 RVA: 0x000BE436 File Offset: 0x000BC636
		[SRCategory("CatBehavior")]
		[SRDescription("ImageListTransparentColorDescr")]
		public Color TransparentColor
		{
			get
			{
				return this.transparentColor;
			}
			set
			{
				this.transparentColor = value;
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x060029D8 RID: 10712 RVA: 0x000BE440 File Offset: 0x000BC640
		private bool UseTransparentColor
		{
			get
			{
				return this.TransparentColor.A > 0;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ImageList.Handle" /> is recreated.</summary>
		// Token: 0x140001E9 RID: 489
		// (add) Token: 0x060029D9 RID: 10713 RVA: 0x000BE45E File Offset: 0x000BC65E
		// (remove) Token: 0x060029DA RID: 10714 RVA: 0x000BE477 File Offset: 0x000BC677
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SRDescription("ImageListOnRecreateHandleDescr")]
		public event EventHandler RecreateHandle
		{
			add
			{
				this.recreateHandler = (EventHandler)Delegate.Combine(this.recreateHandler, value);
			}
			remove
			{
				this.recreateHandler = (EventHandler)Delegate.Remove(this.recreateHandler, value);
			}
		}

		// Token: 0x140001EA RID: 490
		// (add) Token: 0x060029DB RID: 10715 RVA: 0x000BE490 File Offset: 0x000BC690
		// (remove) Token: 0x060029DC RID: 10716 RVA: 0x000BE4A9 File Offset: 0x000BC6A9
		internal event EventHandler ChangeHandle
		{
			add
			{
				this.changeHandler = (EventHandler)Delegate.Combine(this.changeHandler, value);
			}
			remove
			{
				this.changeHandler = (EventHandler)Delegate.Remove(this.changeHandler, value);
			}
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x000BE4C4 File Offset: 0x000BC6C4
		private Bitmap CreateBitmap(ImageList.Original original, out bool ownsBitmap)
		{
			Color customTransparentColor = this.transparentColor;
			ownsBitmap = false;
			if ((original.options & ImageList.OriginalOptions.CustomTransparentColor) != ImageList.OriginalOptions.Default)
			{
				customTransparentColor = original.customTransparentColor;
			}
			Bitmap bitmap;
			if (original.image is Bitmap)
			{
				bitmap = (Bitmap)original.image;
			}
			else if (original.image is Icon)
			{
				bitmap = ((Icon)original.image).ToBitmap();
				ownsBitmap = true;
			}
			else
			{
				bitmap = new Bitmap((Image)original.image);
				ownsBitmap = true;
			}
			if (customTransparentColor.A > 0)
			{
				Bitmap bitmap2 = bitmap;
				bitmap = (Bitmap)bitmap.Clone();
				bitmap.MakeTransparent(customTransparentColor);
				if (ownsBitmap)
				{
					bitmap2.Dispose();
				}
				ownsBitmap = true;
			}
			Size size = bitmap.Size;
			if ((original.options & ImageList.OriginalOptions.ImageStrip) != ImageList.OriginalOptions.Default)
			{
				if (size.Width == 0 || size.Width % this.imageSize.Width != 0)
				{
					throw new ArgumentException(SR.GetString("ImageListStripBadWidth"), "original");
				}
				if (size.Height != this.imageSize.Height)
				{
					throw new ArgumentException(SR.GetString("ImageListImageTooShort"), "original");
				}
			}
			else if (!size.Equals(this.ImageSize))
			{
				Bitmap bitmap3 = bitmap;
				bitmap = new Bitmap(bitmap3, this.ImageSize);
				if (ownsBitmap)
				{
					bitmap3.Dispose();
				}
				ownsBitmap = true;
			}
			return bitmap;
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x000BE614 File Offset: 0x000BC814
		private int AddIconToHandle(ImageList.Original original, Icon icon)
		{
			int num2;
			try
			{
				int num = SafeNativeMethods.ImageList_ReplaceIcon(new HandleRef(this, this.Handle), -1, new HandleRef(icon, icon.Handle));
				if (num == -1)
				{
					throw new InvalidOperationException(SR.GetString("ImageListAddFailed"));
				}
				num2 = num;
			}
			finally
			{
				if ((original.options & ImageList.OriginalOptions.OwnsImage) != ImageList.OriginalOptions.Default)
				{
					icon.Dispose();
				}
			}
			return num2;
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x000BE67C File Offset: 0x000BC87C
		private int AddToHandle(ImageList.Original original, Bitmap bitmap)
		{
			IntPtr intPtr = ControlPaint.CreateHBitmapTransparencyMask(bitmap);
			IntPtr intPtr2 = ControlPaint.CreateHBitmapColorMask(bitmap, intPtr);
			int num = SafeNativeMethods.ImageList_Add(new HandleRef(this, this.Handle), new HandleRef(null, intPtr2), new HandleRef(null, intPtr));
			SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr2));
			SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr));
			if (num == -1)
			{
				throw new InvalidOperationException(SR.GetString("ImageListAddFailed"));
			}
			return num;
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x000BE6E8 File Offset: 0x000BC8E8
		private void CreateHandle()
		{
			int num = 1;
			ColorDepth colorDepth = this.colorDepth;
			if (colorDepth <= ColorDepth.Depth8Bit)
			{
				if (colorDepth != ColorDepth.Depth4Bit)
				{
					if (colorDepth == ColorDepth.Depth8Bit)
					{
						num |= 8;
					}
				}
				else
				{
					num |= 4;
				}
			}
			else if (colorDepth != ColorDepth.Depth16Bit)
			{
				if (colorDepth != ColorDepth.Depth24Bit)
				{
					if (colorDepth == ColorDepth.Depth32Bit)
					{
						num |= 32;
					}
				}
				else
				{
					num |= 24;
				}
			}
			else
			{
				num |= 16;
			}
			IntPtr intPtr = UnsafeNativeMethods.ThemingScope.Activate();
			try
			{
				SafeNativeMethods.InitCommonControls();
				this.nativeImageList = new ImageList.NativeImageList(SafeNativeMethods.ImageList_Create(this.imageSize.Width, this.imageSize.Height, num, 4, 4));
			}
			finally
			{
				UnsafeNativeMethods.ThemingScope.Deactivate(intPtr);
			}
			if (this.Handle == IntPtr.Zero)
			{
				throw new InvalidOperationException(SR.GetString("ImageListCreateFailed"));
			}
			SafeNativeMethods.ImageList_SetBkColor(new HandleRef(this, this.Handle), -1);
			for (int i = 0; i < this.originals.Count; i++)
			{
				ImageList.Original original = (ImageList.Original)this.originals[i];
				if (original.image is Icon)
				{
					this.AddIconToHandle(original, (Icon)original.image);
				}
				else
				{
					bool flag = false;
					Bitmap bitmap = this.CreateBitmap(original, out flag);
					this.AddToHandle(original, bitmap);
					if (flag)
					{
						bitmap.Dispose();
					}
				}
			}
			this.originals = null;
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x000BE83C File Offset: 0x000BCA3C
		private void DestroyHandle()
		{
			if (this.HandleCreated)
			{
				this.nativeImageList.Dispose();
				this.nativeImageList = null;
				this.originals = new ArrayList();
			}
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x000BE864 File Offset: 0x000BCA64
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.originals != null)
				{
					foreach (object obj in this.originals)
					{
						ImageList.Original original = (ImageList.Original)obj;
						if ((original.options & ImageList.OriginalOptions.OwnsImage) != ImageList.OriginalOptions.Default)
						{
							((IDisposable)original.image).Dispose();
						}
					}
				}
				this.DestroyHandle();
			}
			base.Dispose(disposing);
		}

		/// <summary>Draws the image indicated by the specified index on the specified <see cref="T:System.Drawing.Graphics" /> at the given location.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw on.</param>
		/// <param name="pt">The location defined by a <see cref="T:System.Drawing.Point" /> at which to draw the image.</param>
		/// <param name="index">The index of the image in the <see cref="T:System.Windows.Forms.ImageList" /> to draw.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index is less than 0.  
		///  -or-  
		///  The index is greater than or equal to the count of images in the image list.</exception>
		// Token: 0x060029E3 RID: 10723 RVA: 0x000BE8E8 File Offset: 0x000BCAE8
		public void Draw(Graphics g, Point pt, int index)
		{
			this.Draw(g, pt.X, pt.Y, index);
		}

		/// <summary>Draws the image indicated by the given index on the specified <see cref="T:System.Drawing.Graphics" /> at the specified location.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw on.</param>
		/// <param name="x">The horizontal position at which to draw the image.</param>
		/// <param name="y">The vertical position at which to draw the image.</param>
		/// <param name="index">The index of the image in the <see cref="T:System.Windows.Forms.ImageList" /> to draw.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index is less than 0.  
		///  -or-  
		///  The index is greater than or equal to the count of images in the image list.</exception>
		// Token: 0x060029E4 RID: 10724 RVA: 0x000BE900 File Offset: 0x000BCB00
		public void Draw(Graphics g, int x, int y, int index)
		{
			this.Draw(g, x, y, this.imageSize.Width, this.imageSize.Height, index);
		}

		/// <summary>Draws the image indicated by the given index on the specified <see cref="T:System.Drawing.Graphics" /> using the specified location and size.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw on.</param>
		/// <param name="x">The horizontal position at which to draw the image.</param>
		/// <param name="y">The vertical position at which to draw the image.</param>
		/// <param name="width">The width, in pixels, of the destination image.</param>
		/// <param name="height">The height, in pixels, of the destination image.</param>
		/// <param name="index">The index of the image in the <see cref="T:System.Windows.Forms.ImageList" /> to draw.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index is less than 0.  
		///  -or-  
		///  The index is greater than or equal to the count of images in the image list.</exception>
		// Token: 0x060029E5 RID: 10725 RVA: 0x000BE924 File Offset: 0x000BCB24
		public void Draw(Graphics g, int x, int y, int width, int height, int index)
		{
			if (index < 0 || index >= this.Images.Count)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			IntPtr hdc = g.GetHdc();
			try
			{
				SafeNativeMethods.ImageList_DrawEx(new HandleRef(this, this.Handle), index, new HandleRef(g, hdc), x, y, width, height, -1, -1, 1);
			}
			finally
			{
				g.ReleaseHdcInternal(hdc);
			}
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x000BE9BC File Offset: 0x000BCBBC
		private void CopyBitmapData(BitmapData sourceData, BitmapData targetData)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < targetData.Height; i++)
			{
				IntPtr intPtr;
				IntPtr intPtr2;
				if (IntPtr.Size == 4)
				{
					intPtr = new IntPtr(sourceData.Scan0.ToInt32() + num);
					intPtr2 = new IntPtr(targetData.Scan0.ToInt32() + num2);
				}
				else
				{
					intPtr = new IntPtr(sourceData.Scan0.ToInt64() + (long)num);
					intPtr2 = new IntPtr(targetData.Scan0.ToInt64() + (long)num2);
				}
				UnsafeNativeMethods.CopyMemory(new HandleRef(this, intPtr2), new HandleRef(this, intPtr), Math.Abs(targetData.Stride));
				num += sourceData.Stride;
				num2 += targetData.Stride;
			}
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x000BEA84 File Offset: 0x000BCC84
		private unsafe static bool BitmapHasAlpha(BitmapData bmpData)
		{
			if (bmpData.PixelFormat != PixelFormat.Format32bppArgb && bmpData.PixelFormat != PixelFormat.Format32bppRgb)
			{
				return false;
			}
			bool flag = false;
			for (int i = 0; i < bmpData.Height; i++)
			{
				int num = i * bmpData.Stride;
				for (int j = 3; j < bmpData.Width * 4; j += 4)
				{
					byte* ptr = (byte*)((byte*)bmpData.Scan0.ToPointer() + num) + j;
					if (*ptr != 0)
					{
						return true;
					}
				}
			}
			return flag;
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x000BEAFC File Offset: 0x000BCCFC
		private Bitmap GetBitmap(int index)
		{
			if (index < 0 || index >= this.Images.Count)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			Bitmap bitmap = null;
			if (this.ColorDepth == ColorDepth.Depth32Bit)
			{
				NativeMethods.IMAGEINFO imageinfo = new NativeMethods.IMAGEINFO();
				if (SafeNativeMethods.ImageList_GetImageInfo(new HandleRef(this, this.Handle), index, imageinfo))
				{
					Bitmap bitmap2 = null;
					BitmapData bitmapData = null;
					BitmapData bitmapData2 = null;
					IntSecurity.ObjectFromWin32Handle.Assert();
					try
					{
						bitmap2 = Image.FromHbitmap(imageinfo.hbmImage);
						bitmapData = bitmap2.LockBits(new Rectangle(imageinfo.rcImage_left, imageinfo.rcImage_top, imageinfo.rcImage_right - imageinfo.rcImage_left, imageinfo.rcImage_bottom - imageinfo.rcImage_top), ImageLockMode.ReadOnly, bitmap2.PixelFormat);
						int num = bitmapData.Stride * this.imageSize.Height * index;
						if (ImageList.BitmapHasAlpha(bitmapData))
						{
							bitmap = new Bitmap(this.imageSize.Width, this.imageSize.Height, PixelFormat.Format32bppArgb);
							bitmapData2 = bitmap.LockBits(new Rectangle(0, 0, this.imageSize.Width, this.imageSize.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
							this.CopyBitmapData(bitmapData, bitmapData2);
						}
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
						if (bitmap2 != null)
						{
							if (bitmapData != null)
							{
								bitmap2.UnlockBits(bitmapData);
							}
							bitmap2.Dispose();
						}
						if (bitmap != null && bitmapData2 != null)
						{
							bitmap.UnlockBits(bitmapData2);
						}
					}
				}
			}
			if (bitmap == null)
			{
				bitmap = new Bitmap(this.imageSize.Width, this.imageSize.Height);
				Graphics graphics = Graphics.FromImage(bitmap);
				try
				{
					IntPtr hdc = graphics.GetHdc();
					try
					{
						SafeNativeMethods.ImageList_DrawEx(new HandleRef(this, this.Handle), index, new HandleRef(graphics, hdc), 0, 0, this.imageSize.Width, this.imageSize.Height, -1, -1, 1);
					}
					finally
					{
						graphics.ReleaseHdcInternal(hdc);
					}
				}
				finally
				{
					graphics.Dispose();
				}
			}
			bitmap.MakeTransparent(ImageList.fakeTransparencyColor);
			return bitmap;
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x000BED1C File Offset: 0x000BCF1C
		private void OnRecreateHandle(EventArgs eventargs)
		{
			if (this.recreateHandler != null)
			{
				this.recreateHandler(this, eventargs);
			}
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x000BED33 File Offset: 0x000BCF33
		private void OnChangeHandle(EventArgs eventargs)
		{
			if (this.changeHandler != null)
			{
				this.changeHandler(this, eventargs);
			}
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x000BED4C File Offset: 0x000BCF4C
		private void PerformRecreateHandle(string reason)
		{
			if (!this.HandleCreated)
			{
				return;
			}
			if (this.originals == null || this.Images.Empty)
			{
				this.originals = new ArrayList();
			}
			if (this.originals == null)
			{
				throw new InvalidOperationException(SR.GetString("ImageListCantRecreate", new object[] { reason }));
			}
			this.DestroyHandle();
			this.CreateHandle();
			this.OnRecreateHandle(new EventArgs());
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x000BEDBB File Offset: 0x000BCFBB
		private void ResetImageSize()
		{
			this.ImageSize = ImageList.DefaultImageSize;
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x000BEDC8 File Offset: 0x000BCFC8
		private void ResetTransparentColor()
		{
			this.TransparentColor = Color.LightGray;
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x000BEDD8 File Offset: 0x000BCFD8
		private bool ShouldSerializeTransparentColor()
		{
			return !this.TransparentColor.Equals(Color.LightGray);
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Windows.Forms.ImageList" />.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.ImageList" />.</returns>
		// Token: 0x060029EF RID: 10735 RVA: 0x000BEE08 File Offset: 0x000BD008
		public override string ToString()
		{
			string text = base.ToString();
			if (this.Images != null)
			{
				return string.Concat(new string[]
				{
					text,
					" Images.Count: ",
					this.Images.Count.ToString(CultureInfo.CurrentCulture),
					", ImageSize: ",
					this.ImageSize.ToString()
				});
			}
			return text;
		}

		// Token: 0x040010E9 RID: 4329
		private static Color fakeTransparencyColor = Color.FromArgb(13, 11, 12);

		// Token: 0x040010EA RID: 4330
		private static Size DefaultImageSize = new Size(16, 16);

		// Token: 0x040010EB RID: 4331
		private const int INITIAL_CAPACITY = 4;

		// Token: 0x040010EC RID: 4332
		private const int GROWBY = 4;

		// Token: 0x040010ED RID: 4333
		private const int MAX_DIMENSION = 256;

		// Token: 0x040010EE RID: 4334
		private static int maxImageWidth = 256;

		// Token: 0x040010EF RID: 4335
		private static int maxImageHeight = 256;

		// Token: 0x040010F0 RID: 4336
		private static bool isScalingInitialized;

		// Token: 0x040010F1 RID: 4337
		private ImageList.NativeImageList nativeImageList;

		// Token: 0x040010F2 RID: 4338
		private ColorDepth colorDepth = ColorDepth.Depth8Bit;

		// Token: 0x040010F3 RID: 4339
		private Color transparentColor = Color.Transparent;

		// Token: 0x040010F4 RID: 4340
		private Size imageSize = ImageList.DefaultImageSize;

		// Token: 0x040010F5 RID: 4341
		private ImageList.ImageCollection imageCollection;

		// Token: 0x040010F6 RID: 4342
		private object userData;

		// Token: 0x040010F7 RID: 4343
		private IList originals = new ArrayList();

		// Token: 0x040010F8 RID: 4344
		private EventHandler recreateHandler;

		// Token: 0x040010F9 RID: 4345
		private EventHandler changeHandler;

		// Token: 0x040010FA RID: 4346
		private bool inAddRange;

		// Token: 0x020006AA RID: 1706
		internal class Indexer
		{
			// Token: 0x17001692 RID: 5778
			// (get) Token: 0x0600686D RID: 26733 RVA: 0x0018431B File Offset: 0x0018251B
			// (set) Token: 0x0600686E RID: 26734 RVA: 0x00184323 File Offset: 0x00182523
			public virtual ImageList ImageList
			{
				get
				{
					return this.imageList;
				}
				set
				{
					this.imageList = value;
				}
			}

			// Token: 0x17001693 RID: 5779
			// (get) Token: 0x0600686F RID: 26735 RVA: 0x0018432C File Offset: 0x0018252C
			// (set) Token: 0x06006870 RID: 26736 RVA: 0x00184334 File Offset: 0x00182534
			public virtual string Key
			{
				get
				{
					return this.key;
				}
				set
				{
					this.index = -1;
					this.key = ((value == null) ? string.Empty : value);
					this.useIntegerIndex = false;
				}
			}

			// Token: 0x17001694 RID: 5780
			// (get) Token: 0x06006871 RID: 26737 RVA: 0x00184355 File Offset: 0x00182555
			// (set) Token: 0x06006872 RID: 26738 RVA: 0x0018435D File Offset: 0x0018255D
			public virtual int Index
			{
				get
				{
					return this.index;
				}
				set
				{
					this.key = string.Empty;
					this.index = value;
					this.useIntegerIndex = true;
				}
			}

			// Token: 0x17001695 RID: 5781
			// (get) Token: 0x06006873 RID: 26739 RVA: 0x00184378 File Offset: 0x00182578
			public virtual int ActualIndex
			{
				get
				{
					if (this.useIntegerIndex)
					{
						return this.Index;
					}
					if (this.ImageList != null)
					{
						return this.ImageList.Images.IndexOfKey(this.Key);
					}
					return -1;
				}
			}

			// Token: 0x04003AE7 RID: 15079
			private string key = string.Empty;

			// Token: 0x04003AE8 RID: 15080
			private int index = -1;

			// Token: 0x04003AE9 RID: 15081
			private bool useIntegerIndex = true;

			// Token: 0x04003AEA RID: 15082
			private ImageList imageList;
		}

		// Token: 0x020006AB RID: 1707
		internal class NativeImageList : IDisposable
		{
			// Token: 0x06006875 RID: 26741 RVA: 0x001843CA File Offset: 0x001825CA
			internal NativeImageList(IntPtr himl)
			{
				this.himl = himl;
			}

			// Token: 0x17001696 RID: 5782
			// (get) Token: 0x06006876 RID: 26742 RVA: 0x001843D9 File Offset: 0x001825D9
			internal IntPtr Handle
			{
				get
				{
					return this.himl;
				}
			}

			// Token: 0x06006877 RID: 26743 RVA: 0x001843E1 File Offset: 0x001825E1
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x06006878 RID: 26744 RVA: 0x001843F0 File Offset: 0x001825F0
			public void Dispose(bool disposing)
			{
				if (this.himl != IntPtr.Zero)
				{
					SafeNativeMethods.ImageList_Destroy(new HandleRef(null, this.himl));
					this.himl = IntPtr.Zero;
				}
			}

			// Token: 0x06006879 RID: 26745 RVA: 0x00184424 File Offset: 0x00182624
			~NativeImageList()
			{
				this.Dispose(false);
			}

			// Token: 0x04003AEB RID: 15083
			private IntPtr himl;
		}

		// Token: 0x020006AC RID: 1708
		private class Original
		{
			// Token: 0x0600687A RID: 26746 RVA: 0x00184454 File Offset: 0x00182654
			internal Original(object image, ImageList.OriginalOptions options)
				: this(image, options, Color.Transparent)
			{
			}

			// Token: 0x0600687B RID: 26747 RVA: 0x00184463 File Offset: 0x00182663
			internal Original(object image, ImageList.OriginalOptions options, int nImages)
				: this(image, options, Color.Transparent)
			{
				this.nImages = nImages;
			}

			// Token: 0x0600687C RID: 26748 RVA: 0x0018447C File Offset: 0x0018267C
			internal Original(object image, ImageList.OriginalOptions options, Color customTransparentColor)
			{
				if (!(image is Icon) && !(image is Image))
				{
					throw new InvalidOperationException(SR.GetString("ImageListEntryType"));
				}
				this.image = image;
				this.options = options;
				this.customTransparentColor = customTransparentColor;
				ImageList.OriginalOptions originalOptions = options & ImageList.OriginalOptions.CustomTransparentColor;
			}

			// Token: 0x04003AEC RID: 15084
			internal object image;

			// Token: 0x04003AED RID: 15085
			internal ImageList.OriginalOptions options;

			// Token: 0x04003AEE RID: 15086
			internal Color customTransparentColor = Color.Transparent;

			// Token: 0x04003AEF RID: 15087
			internal int nImages = 1;
		}

		// Token: 0x020006AD RID: 1709
		[Flags]
		private enum OriginalOptions
		{
			// Token: 0x04003AF1 RID: 15089
			Default = 0,
			// Token: 0x04003AF2 RID: 15090
			ImageStrip = 1,
			// Token: 0x04003AF3 RID: 15091
			CustomTransparentColor = 2,
			// Token: 0x04003AF4 RID: 15092
			OwnsImage = 4
		}

		/// <summary>Encapsulates the collection of <see cref="T:System.Drawing.Image" /> objects in an <see cref="T:System.Windows.Forms.ImageList" />.</summary>
		// Token: 0x020006AE RID: 1710
		[Editor("System.Windows.Forms.Design.ImageCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public sealed class ImageCollection : IList, ICollection, IEnumerable
		{
			/// <summary>Gets the collection of keys associated with the images in the <see cref="T:System.Windows.Forms.ImageList.ImageCollection" />.</summary>
			/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> containing the names of the images in the <see cref="T:System.Windows.Forms.ImageList.ImageCollection" />.</returns>
			// Token: 0x17001697 RID: 5783
			// (get) Token: 0x0600687D RID: 26749 RVA: 0x001844DC File Offset: 0x001826DC
			public StringCollection Keys
			{
				get
				{
					StringCollection stringCollection = new StringCollection();
					for (int i = 0; i < this.imageInfoCollection.Count; i++)
					{
						ImageList.ImageCollection.ImageInfo imageInfo = this.imageInfoCollection[i] as ImageList.ImageCollection.ImageInfo;
						if (imageInfo != null && imageInfo.Name != null && imageInfo.Name.Length != 0)
						{
							stringCollection.Add(imageInfo.Name);
						}
						else
						{
							stringCollection.Add(string.Empty);
						}
					}
					return stringCollection;
				}
			}

			// Token: 0x0600687E RID: 26750 RVA: 0x0018454B File Offset: 0x0018274B
			internal ImageCollection(ImageList owner)
			{
				this.owner = owner;
			}

			// Token: 0x0600687F RID: 26751 RVA: 0x0018456C File Offset: 0x0018276C
			internal void ResetKeys()
			{
				if (this.imageInfoCollection != null)
				{
					this.imageInfoCollection.Clear();
				}
				for (int i = 0; i < this.Count; i++)
				{
					this.imageInfoCollection.Add(new ImageList.ImageCollection.ImageInfo());
				}
			}

			// Token: 0x06006880 RID: 26752 RVA: 0x000070A6 File Offset: 0x000052A6
			[Conditional("DEBUG")]
			private void AssertInvariant()
			{
			}

			/// <summary>Gets the number of images currently in the list.</summary>
			/// <returns>The number of images in the list. The default is 0.</returns>
			// Token: 0x17001698 RID: 5784
			// (get) Token: 0x06006881 RID: 26753 RVA: 0x001845B0 File Offset: 0x001827B0
			[Browsable(false)]
			public int Count
			{
				get
				{
					if (this.owner.HandleCreated)
					{
						return SafeNativeMethods.ImageList_GetImageCount(new HandleRef(this.owner, this.owner.Handle));
					}
					int num = 0;
					foreach (object obj in this.owner.originals)
					{
						ImageList.Original original = (ImageList.Original)obj;
						if (original != null)
						{
							num += original.nImages;
						}
					}
					return num;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
			/// <returns>The object used to synchronize the <see cref="T:System.Windows.Forms.ImageList.ImageCollection" />.</returns>
			// Token: 0x17001699 RID: 5785
			// (get) Token: 0x06006882 RID: 26754 RVA: 0x00006A49 File Offset: 0x00004C49
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>
			///   <see langword="false" /> in all cases.</returns>
			// Token: 0x1700169A RID: 5786
			// (get) Token: 0x06006883 RID: 26755 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ImageList.ImageCollection" /> has a fixed size.</summary>
			/// <returns>
			///   <see langword="false" /> in all cases.</returns>
			// Token: 0x1700169B RID: 5787
			// (get) Token: 0x06006884 RID: 26756 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets a value indicating whether the list is read-only.</summary>
			/// <returns>Always <see langword="false" />.</returns>
			// Token: 0x1700169C RID: 5788
			// (get) Token: 0x06006885 RID: 26757 RVA: 0x0001180C File Offset: 0x0000FA0C
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ImageList" /> has any images.</summary>
			/// <returns>
			///   <see langword="true" /> if there are no images in the list; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
			// Token: 0x1700169D RID: 5789
			// (get) Token: 0x06006886 RID: 26758 RVA: 0x00184640 File Offset: 0x00182840
			public bool Empty
			{
				get
				{
					return this.Count == 0;
				}
			}

			/// <summary>Gets or sets an <see cref="T:System.Drawing.Image" /> at the specified index within the collection.</summary>
			/// <param name="index">The index of the image to get or set.</param>
			/// <returns>The image in the list specified by <paramref name="index" />.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The index is less than 0 or greater than or equal to <see cref="P:System.Windows.Forms.ImageList.ImageCollection.Count" />.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="image" /> is not a <see cref="T:System.Drawing.Bitmap" />.</exception>
			/// <exception cref="T:System.ArgumentNullException">The image to be assigned is <see langword="null" /> or not a <see cref="T:System.Drawing.Bitmap" />.</exception>
			/// <exception cref="T:System.InvalidOperationException">The image cannot be added to the list.</exception>
			// Token: 0x1700169E RID: 5790
			[Browsable(false)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			public Image this[int index]
			{
				get
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					return this.owner.GetBitmap(index);
				}
				set
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (value == null)
					{
						throw new ArgumentNullException("value");
					}
					if (!(value is Bitmap))
					{
						throw new ArgumentException(SR.GetString("ImageListBitmap"));
					}
					Bitmap bitmap = (Bitmap)value;
					bool flag = false;
					if (this.owner.UseTransparentColor)
					{
						bitmap = (Bitmap)bitmap.Clone();
						bitmap.MakeTransparent(this.owner.transparentColor);
						flag = true;
					}
					try
					{
						IntPtr intPtr = ControlPaint.CreateHBitmapTransparencyMask(bitmap);
						IntPtr intPtr2 = ControlPaint.CreateHBitmapColorMask(bitmap, intPtr);
						bool flag2 = SafeNativeMethods.ImageList_Replace(new HandleRef(this.owner, this.owner.Handle), index, new HandleRef(null, intPtr2), new HandleRef(null, intPtr));
						SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr2));
						SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr));
						if (!flag2)
						{
							throw new InvalidOperationException(SR.GetString("ImageListReplaceFailed"));
						}
					}
					finally
					{
						if (flag)
						{
							bitmap.Dispose();
						}
					}
				}
			}

			/// <summary>Gets or sets an image in an existing <see cref="T:System.Windows.Forms.ImageList.ImageCollection" />.</summary>
			/// <param name="index">The zero-based index of the image to get or set.</param>
			/// <returns>The image in the list specified by the index.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The index is less than 0 or greater than or equal to <see cref="P:System.Windows.Forms.ImageList.ImageCollection.Count" />.</exception>
			/// <exception cref="T:System.Exception">The attempt to replace the image failed.</exception>
			/// <exception cref="T:System.ArgumentNullException">The image to be assigned is <see langword="null" /> or not a bitmap.</exception>
			// Token: 0x1700169F RID: 5791
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					if (value is Image)
					{
						this[index] = (Image)value;
						return;
					}
					throw new ArgumentException(SR.GetString("ImageListBadImage"), "value");
				}
			}

			/// <summary>Gets an <see cref="T:System.Drawing.Image" /> with the specified key from the collection.</summary>
			/// <param name="key">The name of the image to retrieve from the collection.</param>
			/// <returns>The <see cref="T:System.Drawing.Image" /> with the specified key.</returns>
			// Token: 0x170016A0 RID: 5792
			public Image this[string key]
			{
				get
				{
					if (key == null || key.Length == 0)
					{
						return null;
					}
					int num = this.IndexOfKey(key);
					if (this.IsValidIndex(num))
					{
						return this[num];
					}
					return null;
				}
			}

			/// <summary>Adds an image with the specified key to the end of the collection.</summary>
			/// <param name="key">The name of the image.</param>
			/// <param name="image">The <see cref="T:System.Drawing.Image" /> to add to the collection.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="image" /> is <see langword="null" />.</exception>
			// Token: 0x0600688C RID: 26764 RVA: 0x00184844 File Offset: 0x00182A44
			public void Add(string key, Image image)
			{
				ImageList.ImageCollection.ImageInfo imageInfo = new ImageList.ImageCollection.ImageInfo();
				imageInfo.Name = key;
				ImageList.Original original = new ImageList.Original(image, ImageList.OriginalOptions.Default);
				this.Add(original, imageInfo);
			}

			/// <summary>Adds an icon with the specified key to the end of the collection.</summary>
			/// <param name="key">The name of the icon.</param>
			/// <param name="icon">The <see cref="T:System.Drawing.Icon" /> to add to the collection.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="icon" /> is <see langword="null" />.</exception>
			// Token: 0x0600688D RID: 26765 RVA: 0x00184870 File Offset: 0x00182A70
			public void Add(string key, Icon icon)
			{
				ImageList.ImageCollection.ImageInfo imageInfo = new ImageList.ImageCollection.ImageInfo();
				imageInfo.Name = key;
				ImageList.Original original = new ImageList.Original(icon, ImageList.OriginalOptions.Default);
				this.Add(original, imageInfo);
			}

			/// <summary>Adds the specified image to the <see cref="T:System.Windows.Forms.ImageList" />.</summary>
			/// <param name="value">The image to add to the list.</param>
			/// <returns>The index of the newly added image, or -1 if the image could not be added.</returns>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="value" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="value" /> is not a <see cref="T:System.Drawing.Bitmap" />.</exception>
			// Token: 0x0600688E RID: 26766 RVA: 0x0018489B File Offset: 0x00182A9B
			int IList.Add(object value)
			{
				if (value is Image)
				{
					this.Add((Image)value);
					return this.Count - 1;
				}
				throw new ArgumentException(SR.GetString("ImageListBadImage"), "value");
			}

			/// <summary>Adds the specified icon to the <see cref="T:System.Windows.Forms.ImageList" />.</summary>
			/// <param name="value">An <see cref="T:System.Drawing.Icon" /> to add to the list.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="value" /> is <see langword="null" />  
			/// -or-  
			/// value is not an <see cref="T:System.Drawing.Icon" />.</exception>
			// Token: 0x0600688F RID: 26767 RVA: 0x001848CE File Offset: 0x00182ACE
			public void Add(Icon value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.Add(new ImageList.Original(value.Clone(), ImageList.OriginalOptions.OwnsImage), null);
			}

			/// <summary>Adds the specified image to the <see cref="T:System.Windows.Forms.ImageList" />.</summary>
			/// <param name="value">A <see cref="T:System.Drawing.Bitmap" /> of the image to add to the list.</param>
			/// <exception cref="T:System.ArgumentNullException">The image being added is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentException">The image being added is not a <see cref="T:System.Drawing.Bitmap" />.</exception>
			// Token: 0x06006890 RID: 26768 RVA: 0x001848F4 File Offset: 0x00182AF4
			public void Add(Image value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				ImageList.Original original = new ImageList.Original(value, ImageList.OriginalOptions.Default);
				this.Add(original, null);
			}

			/// <summary>Adds the specified image to the <see cref="T:System.Windows.Forms.ImageList" />, using the specified color to generate the mask.</summary>
			/// <param name="value">A <see cref="T:System.Drawing.Bitmap" /> of the image to add to the list.</param>
			/// <param name="transparentColor">The <see cref="T:System.Drawing.Color" /> to mask this image.</param>
			/// <returns>The index of the newly added image, or -1 if the image cannot be added.</returns>
			/// <exception cref="T:System.ArgumentNullException">The image being added is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentException">The image being added is not a <see cref="T:System.Drawing.Bitmap" />.</exception>
			// Token: 0x06006891 RID: 26769 RVA: 0x00184920 File Offset: 0x00182B20
			public int Add(Image value, Color transparentColor)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				ImageList.Original original = new ImageList.Original(value, ImageList.OriginalOptions.CustomTransparentColor, transparentColor);
				return this.Add(original, null);
			}

			// Token: 0x06006892 RID: 26770 RVA: 0x0018494C File Offset: 0x00182B4C
			private int Add(ImageList.Original original, ImageList.ImageCollection.ImageInfo imageInfo)
			{
				if (original == null || original.image == null)
				{
					throw new ArgumentNullException("original");
				}
				int num = -1;
				if (original.image is Bitmap)
				{
					if (this.owner.originals != null)
					{
						num = this.owner.originals.Add(original);
					}
					if (this.owner.HandleCreated)
					{
						bool flag = false;
						Bitmap bitmap = this.owner.CreateBitmap(original, out flag);
						num = this.owner.AddToHandle(original, bitmap);
						if (flag)
						{
							bitmap.Dispose();
						}
					}
				}
				else
				{
					if (!(original.image is Icon))
					{
						throw new ArgumentException(SR.GetString("ImageListBitmap"));
					}
					if (this.owner.originals != null)
					{
						num = this.owner.originals.Add(original);
					}
					if (this.owner.HandleCreated)
					{
						num = this.owner.AddIconToHandle(original, (Icon)original.image);
					}
				}
				if ((original.options & ImageList.OriginalOptions.ImageStrip) != ImageList.OriginalOptions.Default)
				{
					for (int i = 0; i < original.nImages; i++)
					{
						this.imageInfoCollection.Add(new ImageList.ImageCollection.ImageInfo());
					}
				}
				else
				{
					if (imageInfo == null)
					{
						imageInfo = new ImageList.ImageCollection.ImageInfo();
					}
					this.imageInfoCollection.Add(imageInfo);
				}
				if (!this.owner.inAddRange)
				{
					this.owner.OnChangeHandle(new EventArgs());
				}
				return num;
			}

			/// <summary>Adds an array of images to the collection.</summary>
			/// <param name="images">The array of <see cref="T:System.Drawing.Image" /> objects to add to the collection.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="images" /> is <see langword="null" />.</exception>
			// Token: 0x06006893 RID: 26771 RVA: 0x00184AA0 File Offset: 0x00182CA0
			public void AddRange(Image[] images)
			{
				if (images == null)
				{
					throw new ArgumentNullException("images");
				}
				this.owner.inAddRange = true;
				foreach (Image image in images)
				{
					this.Add(image);
				}
				this.owner.inAddRange = false;
				this.owner.OnChangeHandle(new EventArgs());
			}

			/// <summary>Adds an image strip for the specified image to the <see cref="T:System.Windows.Forms.ImageList" />.</summary>
			/// <param name="value">A <see cref="T:System.Drawing.Bitmap" /> with the images to add.</param>
			/// <returns>The index of the newly added image, or -1 if the image cannot be added.</returns>
			/// <exception cref="T:System.ArgumentException">The image being added is <see langword="null" />.  
			///  -or-  
			///  The image being added is not a <see cref="T:System.Drawing.Bitmap" />.</exception>
			/// <exception cref="T:System.InvalidOperationException">The image cannot be added.  
			///  -or-  
			///  The width of image strip being added is 0, or the width is not equal to the existing image width.  
			///  -or-  
			///  The image strip height is not equal to existing image height.</exception>
			// Token: 0x06006894 RID: 26772 RVA: 0x00184B00 File Offset: 0x00182D00
			public int AddStrip(Image value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Width == 0 || value.Width % this.owner.ImageSize.Width != 0)
				{
					throw new ArgumentException(SR.GetString("ImageListStripBadWidth"), "value");
				}
				if (value.Height != this.owner.ImageSize.Height)
				{
					throw new ArgumentException(SR.GetString("ImageListImageTooShort"), "value");
				}
				int num = value.Width / this.owner.ImageSize.Width;
				ImageList.Original original = new ImageList.Original(value, ImageList.OriginalOptions.ImageStrip, num);
				return this.Add(original, null);
			}

			/// <summary>Removes all the images and masks from the <see cref="T:System.Windows.Forms.ImageList" />.</summary>
			// Token: 0x06006895 RID: 26773 RVA: 0x00184BB0 File Offset: 0x00182DB0
			public void Clear()
			{
				if (this.owner.originals != null)
				{
					this.owner.originals.Clear();
				}
				this.imageInfoCollection.Clear();
				if (this.owner.HandleCreated)
				{
					SafeNativeMethods.ImageList_Remove(new HandleRef(this.owner, this.owner.Handle), -1);
				}
				this.owner.OnChangeHandle(new EventArgs());
			}

			/// <summary>Not supported. The <see cref="M:System.Collections.IList.Contains(System.Object)" /> method indicates whether a specified object is contained in the list.</summary>
			/// <param name="image">The <see cref="T:System.Drawing.Image" /> to find in the list.</param>
			/// <returns>
			///   <see langword="true" /> if the image is found in the list; otherwise, <see langword="false" />.</returns>
			/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
			// Token: 0x06006896 RID: 26774 RVA: 0x0000A337 File Offset: 0x00008537
			[EditorBrowsable(EditorBrowsableState.Never)]
			public bool Contains(Image image)
			{
				throw new NotSupportedException();
			}

			/// <summary>Implements the <see cref="M:System.Collections.IList.Contains(System.Object)" /> method. Throws a <see cref="T:System.NotSupportedException" /> in all cases.</summary>
			/// <param name="image">The image to locate in the <see cref="T:System.Windows.Forms.ImageList.ImageCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
			// Token: 0x06006897 RID: 26775 RVA: 0x00184C1F File Offset: 0x00182E1F
			bool IList.Contains(object image)
			{
				return image is Image && this.Contains((Image)image);
			}

			/// <summary>Determines if the collection contains an image with the specified key.</summary>
			/// <param name="key">The key of the image to search for.</param>
			/// <returns>
			///   <see langword="true" /> to indicate an image with the specified key is contained in the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x06006898 RID: 26776 RVA: 0x00184C37 File Offset: 0x00182E37
			public bool ContainsKey(string key)
			{
				return this.IsValidIndex(this.IndexOfKey(key));
			}

			/// <summary>Not supported. The <see cref="M:System.Collections.IList.IndexOf(System.Object)" /> method returns the index of a specified object in the list.</summary>
			/// <param name="image">The <see cref="T:System.Drawing.Image" /> to find in the list.</param>
			/// <returns>The index of the image in the list.</returns>
			/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
			// Token: 0x06006899 RID: 26777 RVA: 0x0000A337 File Offset: 0x00008537
			[EditorBrowsable(EditorBrowsableState.Never)]
			public int IndexOf(Image image)
			{
				throw new NotSupportedException();
			}

			/// <summary>Implements the <see cref="M:System.Collections.IList.IndexOf(System.Object)" /> method. Throws a <see cref="T:System.NotSupportedException" /> in all cases.</summary>
			/// <param name="image">The image to find in the list.</param>
			/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
			// Token: 0x0600689A RID: 26778 RVA: 0x00184C46 File Offset: 0x00182E46
			int IList.IndexOf(object image)
			{
				if (image is Image)
				{
					return this.IndexOf((Image)image);
				}
				return -1;
			}

			/// <summary>Determines the index of the first occurrence of an image with the specified key in the collection.</summary>
			/// <param name="key">The key of the image to retrieve the index for.</param>
			/// <returns>The zero-based index of the first occurrence of an image with the specified key in the collection, if found; otherwise, -1.</returns>
			// Token: 0x0600689B RID: 26779 RVA: 0x00184C60 File Offset: 0x00182E60
			public int IndexOfKey(string key)
			{
				if (key == null || key.Length == 0)
				{
					return -1;
				}
				if (this.IsValidIndex(this.lastAccessedIndex) && this.imageInfoCollection[this.lastAccessedIndex] != null && WindowsFormsUtils.SafeCompareStrings(((ImageList.ImageCollection.ImageInfo)this.imageInfoCollection[this.lastAccessedIndex]).Name, key, true))
				{
					return this.lastAccessedIndex;
				}
				for (int i = 0; i < this.Count; i++)
				{
					if (this.imageInfoCollection[i] != null && WindowsFormsUtils.SafeCompareStrings(((ImageList.ImageCollection.ImageInfo)this.imageInfoCollection[i]).Name, key, true))
					{
						this.lastAccessedIndex = i;
						return i;
					}
				}
				this.lastAccessedIndex = -1;
				return -1;
			}

			/// <summary>Implements the <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" /> method. Throws a <see cref="T:System.NotSupportedException" /> in all cases.</summary>
			/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
			/// <param name="value">The object to insert into the collection.</param>
			/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
			// Token: 0x0600689C RID: 26780 RVA: 0x0000A337 File Offset: 0x00008537
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600689D RID: 26781 RVA: 0x00184D15 File Offset: 0x00182F15
			private bool IsValidIndex(int index)
			{
				return index >= 0 && index < this.Count;
			}

			/// <summary>Copies the items in this collection to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
			/// <param name="dest">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in the <see cref="T:System.Array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="dest" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="dest" /> is multidimensional.  
			/// -or-  
			/// The number of elements in the <see cref="T:System.Windows.Forms.ComboBox.ObjectCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination array.</exception>
			/// <exception cref="T:System.InvalidCastException">The type of the <see cref="T:System.Windows.Forms.ComboBox.ObjectCollection" /> cannot be cast automatically to the type of the destination array.</exception>
			// Token: 0x0600689E RID: 26782 RVA: 0x00184D28 File Offset: 0x00182F28
			void ICollection.CopyTo(Array dest, int index)
			{
				for (int i = 0; i < this.Count; i++)
				{
					dest.SetValue(this.owner.GetBitmap(i), index++);
				}
			}

			/// <summary>Returns an enumerator that can be used to iterate through the item collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the item collection.</returns>
			// Token: 0x0600689F RID: 26783 RVA: 0x00184D60 File Offset: 0x00182F60
			public IEnumerator GetEnumerator()
			{
				Image[] array = new Image[this.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.owner.GetBitmap(i);
				}
				return array.GetEnumerator();
			}

			/// <summary>Not supported. The <see cref="M:System.Collections.IList.Remove(System.Object)" /> method removes a specified object from the list.</summary>
			/// <param name="image">The <see cref="T:System.Drawing.Image" /> to remove from the list.</param>
			/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
			// Token: 0x060068A0 RID: 26784 RVA: 0x0000A337 File Offset: 0x00008537
			[EditorBrowsable(EditorBrowsableState.Never)]
			public void Remove(Image image)
			{
				throw new NotSupportedException();
			}

			/// <summary>Implements the <see cref="M:System.Collections.IList.Remove(System.Object)" />. Throws a <see cref="T:System.NotSupportedException" /> in all cases.</summary>
			/// <param name="image">The object to add to the <see cref="T:System.Windows.Forms.ImageList.ImageCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
			// Token: 0x060068A1 RID: 26785 RVA: 0x00184D9C File Offset: 0x00182F9C
			void IList.Remove(object image)
			{
				if (image is Image)
				{
					this.Remove((Image)image);
					this.owner.OnChangeHandle(new EventArgs());
				}
			}

			/// <summary>Removes an image from the list.</summary>
			/// <param name="index">The index of the image to remove.</param>
			/// <exception cref="T:System.InvalidOperationException">The image cannot be removed.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The index value was less than 0.  
			///  -or-  
			///  The index value is greater than or equal to the <see cref="P:System.Windows.Forms.ImageList.ImageCollection.Count" /> of images.</exception>
			// Token: 0x060068A2 RID: 26786 RVA: 0x00184DC4 File Offset: 0x00182FC4
			public void RemoveAt(int index)
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (!SafeNativeMethods.ImageList_Remove(new HandleRef(this.owner, this.owner.Handle), index))
				{
					throw new InvalidOperationException(SR.GetString("ImageListRemoveFailed"));
				}
				if (this.imageInfoCollection != null && index >= 0 && index < this.imageInfoCollection.Count)
				{
					this.imageInfoCollection.RemoveAt(index);
					this.owner.OnChangeHandle(new EventArgs());
				}
			}

			/// <summary>Removes the image with the specified key from the collection.</summary>
			/// <param name="key">The key of the image to remove from the collection.</param>
			// Token: 0x060068A3 RID: 26787 RVA: 0x00184E78 File Offset: 0x00183078
			public void RemoveByKey(string key)
			{
				int num = this.IndexOfKey(key);
				if (this.IsValidIndex(num))
				{
					this.RemoveAt(num);
				}
			}

			/// <summary>Sets the key for an image in the collection.</summary>
			/// <param name="index">The zero-based index of an image in the collection.</param>
			/// <param name="name">The name of the image to be set as the image key.</param>
			/// <exception cref="T:System.IndexOutOfRangeException">The specified index is less than 0 or greater than or equal to <see cref="P:System.Windows.Forms.ImageList.ImageCollection.Count" />.</exception>
			// Token: 0x060068A4 RID: 26788 RVA: 0x00184EA0 File Offset: 0x001830A0
			public void SetKeyName(int index, string name)
			{
				if (!this.IsValidIndex(index))
				{
					throw new IndexOutOfRangeException();
				}
				if (this.imageInfoCollection[index] == null)
				{
					this.imageInfoCollection[index] = new ImageList.ImageCollection.ImageInfo();
				}
				((ImageList.ImageCollection.ImageInfo)this.imageInfoCollection[index]).Name = name;
			}

			// Token: 0x04003AF5 RID: 15093
			private ImageList owner;

			// Token: 0x04003AF6 RID: 15094
			private ArrayList imageInfoCollection = new ArrayList();

			// Token: 0x04003AF7 RID: 15095
			private int lastAccessedIndex = -1;

			// Token: 0x020008BC RID: 2236
			internal class ImageInfo
			{
				// Token: 0x17001932 RID: 6450
				// (get) Token: 0x060072A1 RID: 29345 RVA: 0x001A2D43 File Offset: 0x001A0F43
				// (set) Token: 0x060072A2 RID: 29346 RVA: 0x001A2D4B File Offset: 0x001A0F4B
				public string Name
				{
					get
					{
						return this.name;
					}
					set
					{
						this.name = value;
					}
				}

				// Token: 0x04004531 RID: 17713
				private string name;
			}
		}
	}
}
