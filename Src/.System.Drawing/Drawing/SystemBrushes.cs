using System;

namespace System.Drawing
{
	/// <summary>Each property of the <see cref="T:System.Drawing.SystemBrushes" /> class is a <see cref="T:System.Drawing.SolidBrush" /> that is the color of a Windows display element.</summary>
	// Token: 0x02000033 RID: 51
	public sealed class SystemBrushes
	{
		// Token: 0x06000505 RID: 1285 RVA: 0x00003800 File Offset: 0x00001A00
		private SystemBrushes()
		{
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the active window's border.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the active window's border.</returns>
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00017467 File Offset: 0x00015667
		public static Brush ActiveBorder
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ActiveBorder);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of the active window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of the active window's title bar.</returns>
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x00017473 File Offset: 0x00015673
		public static Brush ActiveCaption
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ActiveCaption);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text in the active window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of the active window's title bar.</returns>
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x0001747F File Offset: 0x0001567F
		public static Brush ActiveCaptionText
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ActiveCaptionText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the application workspace.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the application workspace.</returns>
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x0001748B File Offset: 0x0001568B
		public static Brush AppWorkspace
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.AppWorkspace);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the face color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the face color of a 3-D element.</returns>
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x00017497 File Offset: 0x00015697
		public static Brush ButtonFace
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ButtonFace);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the highlight color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the highlight color of a 3-D element.</returns>
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x000174A3 File Offset: 0x000156A3
		public static Brush ButtonHighlight
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ButtonHighlight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the shadow color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the shadow color of a 3-D element.</returns>
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x000174AF File Offset: 0x000156AF
		public static Brush ButtonShadow
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ButtonShadow);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the face color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the face color of a 3-D element.</returns>
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x000174BB File Offset: 0x000156BB
		public static Brush Control
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.Control);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the highlight color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the highlight color of a 3-D element.</returns>
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x000174C7 File Offset: 0x000156C7
		public static Brush ControlLightLight
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ControlLightLight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the light color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the light color of a 3-D element.</returns>
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x000174D3 File Offset: 0x000156D3
		public static Brush ControlLight
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ControlLight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the shadow color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the shadow color of a 3-D element.</returns>
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x000174DF File Offset: 0x000156DF
		public static Brush ControlDark
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ControlDark);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the dark shadow color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the dark shadow color of a 3-D element.</returns>
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x000174EB File Offset: 0x000156EB
		public static Brush ControlDarkDark
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ControlDarkDark);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of text in a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of text in a 3-D element.</returns>
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x000174F7 File Offset: 0x000156F7
		public static Brush ControlText
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ControlText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the desktop.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the desktop.</returns>
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x00017503 File Offset: 0x00015703
		public static Brush Desktop
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.Desktop);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the lightest color in the color gradient of an active window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the lightest color in the color gradient of an active window's title bar.</returns>
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x0001750F File Offset: 0x0001570F
		public static Brush GradientActiveCaption
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.GradientActiveCaption);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the lightest color in the color gradient of an inactive window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the lightest color in the color gradient of an inactive window's title bar.</returns>
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0001751B File Offset: 0x0001571B
		public static Brush GradientInactiveCaption
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.GradientInactiveCaption);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of dimmed text.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of dimmed text.</returns>
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x00017527 File Offset: 0x00015727
		public static Brush GrayText
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.GrayText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of selected items.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of selected items.</returns>
		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00017533 File Offset: 0x00015733
		public static Brush Highlight
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.Highlight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text of selected items.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text of selected items.</returns>
		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x0001753F File Offset: 0x0001573F
		public static Brush HighlightText
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.HighlightText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color used to designate a hot-tracked item.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color used to designate a hot-tracked item.</returns>
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x0001754B File Offset: 0x0001574B
		public static Brush HotTrack
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.HotTrack);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of an inactive window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of an inactive window's title bar.</returns>
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x00017557 File Offset: 0x00015757
		public static Brush InactiveCaption
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.InactiveCaption);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of an inactive window's border.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of an inactive window's border.</returns>
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00017563 File Offset: 0x00015763
		public static Brush InactiveBorder
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.InactiveBorder);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text in an inactive window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text in an inactive window's title bar.</returns>
		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0001756F File Offset: 0x0001576F
		public static Brush InactiveCaptionText
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.InactiveCaptionText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of a ToolTip.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of a ToolTip.</returns>
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x0001757B File Offset: 0x0001577B
		public static Brush Info
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.Info);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text of a ToolTip.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> is the color of the text of a ToolTip.</returns>
		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x00017587 File Offset: 0x00015787
		public static Brush InfoText
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.InfoText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of a menu's background.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of a menu's background.</returns>
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x00017593 File Offset: 0x00015793
		public static Brush Menu
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.Menu);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of a menu bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of a menu bar.</returns>
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0001759F File Offset: 0x0001579F
		public static Brush MenuBar
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.MenuBar);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color used to highlight menu items when the menu appears as a flat menu.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color used to highlight menu items when the menu appears as a flat menu.</returns>
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x000175AB File Offset: 0x000157AB
		public static Brush MenuHighlight
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.MenuHighlight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of a menu's text.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of a menu's text.</returns>
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x000175B7 File Offset: 0x000157B7
		public static Brush MenuText
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.MenuText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of a scroll bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background of a scroll bar.</returns>
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x000175C3 File Offset: 0x000157C3
		public static Brush ScrollBar
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.ScrollBar);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background in the client area of a window.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the background in the client area of a window.</returns>
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x000175CF File Offset: 0x000157CF
		public static Brush Window
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.Window);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of a window frame.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of a window frame.</returns>
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x000175DB File Offset: 0x000157DB
		public static Brush WindowFrame
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.WindowFrame);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text in the client area of a window.</summary>
		/// <returns>A <see cref="T:System.Drawing.SolidBrush" /> that is the color of the text in the client area of a window.</returns>
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x000175E7 File Offset: 0x000157E7
		public static Brush WindowText
		{
			get
			{
				return SystemBrushes.FromSystemColor(SystemColors.WindowText);
			}
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Brush" /> from the specified <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <param name="c">The <see cref="T:System.Drawing.Color" /> structure from which to create the <see cref="T:System.Drawing.Brush" />.</param>
		/// <returns>The <see cref="T:System.Drawing.Brush" /> this method creates.</returns>
		// Token: 0x06000527 RID: 1319 RVA: 0x000175F4 File Offset: 0x000157F4
		public static Brush FromSystemColor(Color c)
		{
			if (!c.IsSystemColor)
			{
				throw new ArgumentException(SR.GetString("ColorNotSystemColor", new object[] { c.ToString() }));
			}
			Brush[] array = (Brush[])SafeNativeMethods.Gdip.ThreadData[SystemBrushes.SystemBrushesKey];
			if (array == null)
			{
				array = new Brush[33];
				SafeNativeMethods.Gdip.ThreadData[SystemBrushes.SystemBrushesKey] = array;
			}
			int num = (int)c.ToKnownColor();
			if (num > 167)
			{
				num -= 141;
			}
			num--;
			if (array[num] == null)
			{
				array[num] = new SolidBrush(c, true);
			}
			return array[num];
		}

		// Token: 0x0400031B RID: 795
		private static readonly object SystemBrushesKey = new object();
	}
}
