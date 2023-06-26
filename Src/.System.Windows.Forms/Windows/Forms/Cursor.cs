using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Represents the image used to paint the mouse pointer.</summary>
	// Token: 0x02000175 RID: 373
	[TypeConverter(typeof(CursorConverter))]
	[Editor("System.Drawing.Design.CursorEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[Serializable]
	public sealed class Cursor : IDisposable, ISerializable
	{
		// Token: 0x060013D6 RID: 5078 RVA: 0x000427BC File Offset: 0x000409BC
		internal Cursor(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			if (enumerator == null)
			{
				return;
			}
			while (enumerator.MoveNext())
			{
				if (string.Equals(enumerator.Name, "CursorData", StringComparison.OrdinalIgnoreCase))
				{
					this.cursorData = (byte[])enumerator.Value;
					if (this.cursorData != null)
					{
						this.LoadPicture(new UnsafeNativeMethods.ComStreamFromDataStream(new MemoryStream(this.cursorData)));
					}
				}
				else if (string.Compare(enumerator.Name, "CursorResourceId", true, CultureInfo.InvariantCulture) == 0)
				{
					this.LoadFromResourceId((int)enumerator.Value);
				}
			}
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x00042861 File Offset: 0x00040A61
		internal Cursor(int nResourceId, int dummy)
		{
			this.LoadFromResourceId(nResourceId);
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x00042884 File Offset: 0x00040A84
		internal Cursor(string resource, int dummy)
		{
			Stream manifestResourceStream = typeof(Cursor).Module.Assembly.GetManifestResourceStream(typeof(Cursor), resource);
			this.cursorData = new byte[manifestResourceStream.Length];
			manifestResourceStream.Read(this.cursorData, 0, Convert.ToInt32(manifestResourceStream.Length));
			this.LoadPicture(new UnsafeNativeMethods.ComStreamFromDataStream(new MemoryStream(this.cursorData)));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Cursor" /> class from the specified Windows handle.</summary>
		/// <param name="handle">An <see cref="T:System.IntPtr" /> that represents the Windows handle of the cursor to create.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="handle" /> is <see cref="F:System.IntPtr.Zero" />.</exception>
		// Token: 0x060013D9 RID: 5081 RVA: 0x00042910 File Offset: 0x00040B10
		public Cursor(IntPtr handle)
		{
			IntSecurity.UnmanagedCode.Demand();
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentException(SR.GetString("InvalidGDIHandle", new object[] { typeof(Cursor).Name }));
			}
			this.handle = handle;
			this.ownHandle = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Cursor" /> class from the specified file.</summary>
		/// <param name="fileName">The cursor file to load.</param>
		// Token: 0x060013DA RID: 5082 RVA: 0x00042984 File Offset: 0x00040B84
		public Cursor(string fileName)
		{
			FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			try
			{
				this.cursorData = new byte[fileStream.Length];
				fileStream.Read(this.cursorData, 0, Convert.ToInt32(fileStream.Length));
			}
			finally
			{
				fileStream.Close();
			}
			this.LoadPicture(new UnsafeNativeMethods.ComStreamFromDataStream(new MemoryStream(this.cursorData)));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Cursor" /> class from the specified resource with the specified resource type.</summary>
		/// <param name="type">The resource <see cref="T:System.Type" />.</param>
		/// <param name="resource">The name of the resource.</param>
		// Token: 0x060013DB RID: 5083 RVA: 0x00042A10 File Offset: 0x00040C10
		public Cursor(Type type, string resource)
			: this(type.Module.Assembly.GetManifestResourceStream(type, resource))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Cursor" /> class from the specified data stream.</summary>
		/// <param name="stream">The data stream to load the <see cref="T:System.Windows.Forms.Cursor" /> from.</param>
		// Token: 0x060013DC RID: 5084 RVA: 0x00042A2C File Offset: 0x00040C2C
		public Cursor(Stream stream)
		{
			this.cursorData = new byte[stream.Length];
			stream.Read(this.cursorData, 0, Convert.ToInt32(stream.Length));
			this.LoadPicture(new UnsafeNativeMethods.ComStreamFromDataStream(new MemoryStream(this.cursorData)));
		}

		/// <summary>Gets or sets the bounds that represent the clipping rectangle for the cursor.</summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that represents the clipping rectangle for the <see cref="T:System.Windows.Forms.Cursor" />, in screen coordinates.</returns>
		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x00042A92 File Offset: 0x00040C92
		// (set) Token: 0x060013DE RID: 5086 RVA: 0x00042A99 File Offset: 0x00040C99
		public static Rectangle Clip
		{
			get
			{
				return Cursor.ClipInternal;
			}
			set
			{
				if (!value.IsEmpty)
				{
					IntSecurity.AdjustCursorClip.Demand();
				}
				Cursor.ClipInternal = value;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x00042AB4 File Offset: 0x00040CB4
		// (set) Token: 0x060013E0 RID: 5088 RVA: 0x00042AF0 File Offset: 0x00040CF0
		internal static Rectangle ClipInternal
		{
			get
			{
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				SafeNativeMethods.GetClipCursor(ref rect);
				return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
			}
			set
			{
				if (value.IsEmpty)
				{
					UnsafeNativeMethods.ClipCursor(null);
					return;
				}
				NativeMethods.RECT rect = NativeMethods.RECT.FromXYWH(value.X, value.Y, value.Width, value.Height);
				UnsafeNativeMethods.ClipCursor(ref rect);
			}
		}

		/// <summary>Gets or sets a cursor object that represents the mouse cursor.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> that represents the mouse cursor. The default is <see langword="null" /> if the mouse cursor is not visible.</returns>
		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x00042B38 File Offset: 0x00040D38
		// (set) Token: 0x060013E2 RID: 5090 RVA: 0x00042B3F File Offset: 0x00040D3F
		public static Cursor Current
		{
			get
			{
				return Cursor.CurrentInternal;
			}
			set
			{
				IntSecurity.ModifyCursor.Demand();
				Cursor.CurrentInternal = value;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x060013E3 RID: 5091 RVA: 0x00042B54 File Offset: 0x00040D54
		// (set) Token: 0x060013E4 RID: 5092 RVA: 0x00042B78 File Offset: 0x00040D78
		internal static Cursor CurrentInternal
		{
			get
			{
				IntPtr cursor = SafeNativeMethods.GetCursor();
				IntSecurity.UnmanagedCode.Assert();
				return Cursors.KnownCursorFromHCursor(cursor);
			}
			set
			{
				IntPtr intPtr = ((value == null) ? IntPtr.Zero : value.handle);
				UnsafeNativeMethods.SetCursor(new HandleRef(value, intPtr));
			}
		}

		/// <summary>Gets the handle of the cursor.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> that represents the cursor's handle.</returns>
		/// <exception cref="T:System.Exception">The handle value is <see cref="F:System.IntPtr.Zero" />.</exception>
		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x00042BA9 File Offset: 0x00040DA9
		public IntPtr Handle
		{
			get
			{
				if (this.handle == IntPtr.Zero)
				{
					throw new ObjectDisposedException(SR.GetString("ObjectDisposed", new object[] { base.GetType().Name }));
				}
				return this.handle;
			}
		}

		/// <summary>Gets the cursor hot spot.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> representing the cursor hot spot.</returns>
		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x00042BE8 File Offset: 0x00040DE8
		public Point HotSpot
		{
			get
			{
				Point point = Point.Empty;
				NativeMethods.ICONINFO iconinfo = new NativeMethods.ICONINFO();
				Icon icon = null;
				IntSecurity.ObjectFromWin32Handle.Assert();
				try
				{
					icon = Icon.FromHandle(this.Handle);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				try
				{
					SafeNativeMethods.GetIconInfo(new HandleRef(this, icon.Handle), iconinfo);
					point = new Point(iconinfo.xHotspot, iconinfo.yHotspot);
				}
				finally
				{
					if (iconinfo.hbmMask != IntPtr.Zero)
					{
						SafeNativeMethods.ExternalDeleteObject(new HandleRef(null, iconinfo.hbmMask));
						iconinfo.hbmMask = IntPtr.Zero;
					}
					if (iconinfo.hbmColor != IntPtr.Zero)
					{
						SafeNativeMethods.ExternalDeleteObject(new HandleRef(null, iconinfo.hbmColor));
						iconinfo.hbmColor = IntPtr.Zero;
					}
					icon.Dispose();
				}
				return point;
			}
		}

		/// <summary>Gets or sets the cursor's position.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> that represents the cursor's position in screen coordinates.</returns>
		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x00042CCC File Offset: 0x00040ECC
		// (set) Token: 0x060013E8 RID: 5096 RVA: 0x00042CF7 File Offset: 0x00040EF7
		public static Point Position
		{
			get
			{
				NativeMethods.POINT point = new NativeMethods.POINT();
				UnsafeNativeMethods.GetCursorPos(point);
				return new Point(point.x, point.y);
			}
			set
			{
				IntSecurity.AdjustCursorPosition.Demand();
				UnsafeNativeMethods.SetCursorPos(value.X, value.Y);
			}
		}

		/// <summary>Gets the size of the cursor object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the width and height of the <see cref="T:System.Windows.Forms.Cursor" />.</returns>
		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x00042D17 File Offset: 0x00040F17
		public Size Size
		{
			get
			{
				if (Cursor.cursorSize.IsEmpty)
				{
					Cursor.cursorSize = new Size(UnsafeNativeMethods.GetSystemMetrics(13), UnsafeNativeMethods.GetSystemMetrics(14));
				}
				return Cursor.cursorSize;
			}
		}

		/// <summary>Gets or sets the object that contains data about the <see cref="T:System.Windows.Forms.Cursor" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains data about the <see cref="T:System.Windows.Forms.Cursor" />. The default is <see langword="null" />.</returns>
		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x00042D42 File Offset: 0x00040F42
		// (set) Token: 0x060013EB RID: 5099 RVA: 0x00042D4A File Offset: 0x00040F4A
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

		/// <summary>Copies the handle of this <see cref="T:System.Windows.Forms.Cursor" />.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> that represents the cursor's handle.</returns>
		// Token: 0x060013EC RID: 5100 RVA: 0x00042D54 File Offset: 0x00040F54
		public IntPtr CopyHandle()
		{
			Size size = this.Size;
			return SafeNativeMethods.CopyImage(new HandleRef(this, this.Handle), 2, size.Width, size.Height, 0);
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x00042D89 File Offset: 0x00040F89
		private void DestroyHandle()
		{
			if (this.ownHandle)
			{
				UnsafeNativeMethods.DestroyCursor(new HandleRef(this, this.handle));
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.Cursor" />.</summary>
		// Token: 0x060013EE RID: 5102 RVA: 0x00042DA5 File Offset: 0x00040FA5
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x00042DB4 File Offset: 0x00040FB4
		private void Dispose(bool disposing)
		{
			if (this.handle != IntPtr.Zero)
			{
				this.DestroyHandle();
				this.handle = IntPtr.Zero;
			}
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x00042DDC File Offset: 0x00040FDC
		private void DrawImageCore(Graphics graphics, Rectangle imageRect, Rectangle targetRect, bool stretch)
		{
			targetRect.X += (int)graphics.Transform.OffsetX;
			targetRect.Y += (int)graphics.Transform.OffsetY;
			int num = 13369376;
			IntPtr hdc = graphics.GetHdc();
			try
			{
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
				Size size = this.Size;
				int num6;
				int num7;
				if (!imageRect.IsEmpty)
				{
					num2 = imageRect.X;
					num3 = imageRect.Y;
					num6 = imageRect.Width;
					num7 = imageRect.Height;
				}
				else
				{
					num6 = size.Width;
					num7 = size.Height;
				}
				int num8;
				int num9;
				if (!targetRect.IsEmpty)
				{
					num4 = targetRect.X;
					num5 = targetRect.Y;
					num8 = targetRect.Width;
					num9 = targetRect.Height;
				}
				else
				{
					num8 = size.Width;
					num9 = size.Height;
				}
				int num10;
				int num11;
				int num12;
				int num13;
				if (stretch)
				{
					if (num8 == num6 && num9 == num7 && num2 == 0 && num3 == 0 && num == 13369376 && num6 == size.Width && num7 == size.Height)
					{
						SafeNativeMethods.DrawIcon(new HandleRef(graphics, hdc), num4, num5, new HandleRef(this, this.handle));
						return;
					}
					num10 = size.Width * num8 / num6;
					num11 = size.Height * num9 / num7;
					num12 = num8;
					num13 = num9;
				}
				else
				{
					if (num2 == 0 && num3 == 0 && num == 13369376 && size.Width <= num8 && size.Height <= num9 && size.Width == num6 && size.Height == num7)
					{
						SafeNativeMethods.DrawIcon(new HandleRef(graphics, hdc), num4, num5, new HandleRef(this, this.handle));
						return;
					}
					num10 = size.Width;
					num11 = size.Height;
					num12 = ((num8 < num6) ? num8 : num6);
					num13 = ((num9 < num7) ? num9 : num7);
				}
				if (num == 13369376)
				{
					SafeNativeMethods.IntersectClipRect(new HandleRef(this, this.Handle), num4, num5, num4 + num12, num5 + num13);
					SafeNativeMethods.DrawIconEx(new HandleRef(graphics, hdc), num4 - num2, num5 - num3, new HandleRef(this, this.handle), num10, num11, 0, NativeMethods.NullHandleRef, 3);
				}
			}
			finally
			{
				graphics.ReleaseHdcInternal(hdc);
			}
		}

		/// <summary>Draws the cursor on the specified surface, within the specified bounds.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw the <see cref="T:System.Windows.Forms.Cursor" />.</param>
		/// <param name="targetRect">The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the <see cref="T:System.Windows.Forms.Cursor" />.</param>
		// Token: 0x060013F1 RID: 5105 RVA: 0x00043048 File Offset: 0x00041248
		public void Draw(Graphics g, Rectangle targetRect)
		{
			this.DrawImageCore(g, Rectangle.Empty, targetRect, false);
		}

		/// <summary>Draws the cursor in a stretched format on the specified surface, within the specified bounds.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw the <see cref="T:System.Windows.Forms.Cursor" />.</param>
		/// <param name="targetRect">The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the <see cref="T:System.Windows.Forms.Cursor" />.</param>
		// Token: 0x060013F2 RID: 5106 RVA: 0x00043058 File Offset: 0x00041258
		public void DrawStretched(Graphics g, Rectangle targetRect)
		{
			this.DrawImageCore(g, Rectangle.Empty, targetRect, true);
		}

		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x060013F3 RID: 5107 RVA: 0x00043068 File Offset: 0x00041268
		~Cursor()
		{
			this.Dispose(false);
		}

		/// <summary>Serializes the object.</summary>
		/// <param name="si">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> class.</param>
		// Token: 0x060013F4 RID: 5108 RVA: 0x00043098 File Offset: 0x00041298
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			if (this.cursorData != null)
			{
				si.AddValue("CursorData", this.cursorData, typeof(byte[]));
				return;
			}
			if (this.resourceId != 0)
			{
				si.AddValue("CursorResourceId", this.resourceId, typeof(int));
				return;
			}
			throw new SerializationException(SR.GetString("CursorNonSerializableHandle"));
		}

		/// <summary>Hides the cursor.</summary>
		// Token: 0x060013F5 RID: 5109 RVA: 0x00043101 File Offset: 0x00041301
		public static void Hide()
		{
			IntSecurity.AdjustCursorClip.Demand();
			UnsafeNativeMethods.ShowCursor(false);
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x00043114 File Offset: 0x00041314
		private void LoadFromResourceId(int nResourceId)
		{
			this.ownHandle = false;
			try
			{
				this.resourceId = nResourceId;
				this.handle = SafeNativeMethods.LoadCursor(NativeMethods.NullHandleRef, nResourceId);
			}
			catch (Exception ex)
			{
				this.handle = IntPtr.Zero;
			}
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x00043160 File Offset: 0x00041360
		private Size GetIconSize(IntPtr iconHandle)
		{
			Size size = this.Size;
			NativeMethods.ICONINFO iconinfo = new NativeMethods.ICONINFO();
			SafeNativeMethods.GetIconInfo(new HandleRef(this, iconHandle), iconinfo);
			NativeMethods.BITMAP bitmap = new NativeMethods.BITMAP();
			if (iconinfo.hbmColor != IntPtr.Zero)
			{
				UnsafeNativeMethods.GetObject(new HandleRef(null, iconinfo.hbmColor), Marshal.SizeOf(typeof(NativeMethods.BITMAP)), bitmap);
				SafeNativeMethods.IntDeleteObject(new HandleRef(null, iconinfo.hbmColor));
				size = new Size(bitmap.bmWidth, bitmap.bmHeight);
			}
			else if (iconinfo.hbmMask != IntPtr.Zero)
			{
				UnsafeNativeMethods.GetObject(new HandleRef(null, iconinfo.hbmMask), Marshal.SizeOf(typeof(NativeMethods.BITMAP)), bitmap);
				size = new Size(bitmap.bmWidth, bitmap.bmHeight / 2);
			}
			if (iconinfo.hbmMask != IntPtr.Zero)
			{
				SafeNativeMethods.IntDeleteObject(new HandleRef(null, iconinfo.hbmMask));
			}
			return size;
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x00043258 File Offset: 0x00041458
		private void LoadPicture(UnsafeNativeMethods.IStream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			try
			{
				Guid guid = typeof(UnsafeNativeMethods.IPicture).GUID;
				UnsafeNativeMethods.IPicture picture = null;
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
				try
				{
					picture = UnsafeNativeMethods.OleCreateIPictureIndirect(null, ref guid, true);
					UnsafeNativeMethods.IPersistStream persistStream = (UnsafeNativeMethods.IPersistStream)picture;
					persistStream.Load(stream);
					if (picture == null || picture.GetPictureType() != 3)
					{
						throw new ArgumentException(SR.GetString("InvalidPictureType", new object[] { "picture", "Cursor" }), "picture");
					}
					IntPtr intPtr = picture.GetHandle();
					Size size = this.GetIconSize(intPtr);
					if (DpiHelper.IsScalingRequired)
					{
						size = DpiHelper.LogicalToDeviceUnits(size, 0);
					}
					this.handle = SafeNativeMethods.CopyImageAsCursor(new HandleRef(this, intPtr), 2, size.Width, size.Height, 0);
					this.ownHandle = true;
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
					if (picture != null)
					{
						Marshal.ReleaseComObject(picture);
					}
				}
			}
			catch (COMException ex)
			{
				throw new ArgumentException(SR.GetString("InvalidPictureFormat"), "stream", ex);
			}
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x00043378 File Offset: 0x00041578
		internal void SavePicture(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (this.resourceId != 0)
			{
				throw new FormatException(SR.GetString("CursorCannotCovertToBytes"));
			}
			try
			{
				stream.Write(this.cursorData, 0, this.cursorData.Length);
			}
			catch (SecurityException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException(SR.GetString("InvalidPictureFormat"));
			}
		}

		/// <summary>Displays the cursor.</summary>
		// Token: 0x060013FA RID: 5114 RVA: 0x000433F4 File Offset: 0x000415F4
		public static void Show()
		{
			UnsafeNativeMethods.ShowCursor(true);
		}

		/// <summary>Retrieves a human readable string representing this <see cref="T:System.Windows.Forms.Cursor" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents this <see cref="T:System.Windows.Forms.Cursor" />.</returns>
		// Token: 0x060013FB RID: 5115 RVA: 0x00043400 File Offset: 0x00041600
		public override string ToString()
		{
			string text;
			if (!this.ownHandle)
			{
				text = TypeDescriptor.GetConverter(typeof(Cursor)).ConvertToString(this);
			}
			else
			{
				text = base.ToString();
			}
			return "[Cursor: " + text + "]";
		}

		/// <summary>Returns a value indicating whether two instances of the <see cref="T:System.Windows.Forms.Cursor" /> class are equal.</summary>
		/// <param name="left">A <see cref="T:System.Windows.Forms.Cursor" /> to compare.</param>
		/// <param name="right">A <see cref="T:System.Windows.Forms.Cursor" /> to compare.</param>
		/// <returns>
		///   <see langword="true" /> if two instances of the <see cref="T:System.Windows.Forms.Cursor" /> class are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x060013FC RID: 5116 RVA: 0x00043446 File Offset: 0x00041646
		public static bool operator ==(Cursor left, Cursor right)
		{
			return left == null == (right == null) && (left == null || left.handle == right.handle);
		}

		/// <summary>Returns a value indicating whether two instances of the <see cref="T:System.Windows.Forms.Cursor" /> class are not equal.</summary>
		/// <param name="left">A <see cref="T:System.Windows.Forms.Cursor" /> to compare.</param>
		/// <param name="right">A <see cref="T:System.Windows.Forms.Cursor" /> to compare.</param>
		/// <returns>
		///   <see langword="true" /> if two instances of the <see cref="T:System.Windows.Forms.Cursor" /> class are not equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x060013FD RID: 5117 RVA: 0x0004346A File Offset: 0x0004166A
		public static bool operator !=(Cursor left, Cursor right)
		{
			return !(left == right);
		}

		/// <summary>Retrieves the hash code for the current <see cref="T:System.Windows.Forms.Cursor" />.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Windows.Forms.Cursor" />.</returns>
		// Token: 0x060013FE RID: 5118 RVA: 0x00043476 File Offset: 0x00041676
		public override int GetHashCode()
		{
			return (int)this.handle;
		}

		/// <summary>Returns a value indicating whether this cursor is equal to the specified <see cref="T:System.Windows.Forms.Cursor" />.</summary>
		/// <param name="obj">The <see cref="T:System.Windows.Forms.Cursor" /> to compare.</param>
		/// <returns>
		///   <see langword="true" /> if this cursor is equal to the specified <see cref="T:System.Windows.Forms.Cursor" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060013FF RID: 5119 RVA: 0x00043483 File Offset: 0x00041683
		public override bool Equals(object obj)
		{
			return obj is Cursor && this == (Cursor)obj;
		}

		// Token: 0x04000946 RID: 2374
		private static Size cursorSize = Size.Empty;

		// Token: 0x04000947 RID: 2375
		private byte[] cursorData;

		// Token: 0x04000948 RID: 2376
		private IntPtr handle = IntPtr.Zero;

		// Token: 0x04000949 RID: 2377
		private bool ownHandle = true;

		// Token: 0x0400094A RID: 2378
		private int resourceId;

		// Token: 0x0400094B RID: 2379
		private object userData;
	}
}
