using System;

namespace System.Drawing
{
	/// <summary>Pens for all the standard colors. This class cannot be inherited.</summary>
	// Token: 0x02000029 RID: 41
	public sealed class Pens
	{
		// Token: 0x060003BC RID: 956 RVA: 0x00003800 File Offset: 0x00001A00
		private Pens()
		{
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060003BD RID: 957 RVA: 0x000124CC File Offset: 0x000106CC
		public static Pen Transparent
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.TransparentKey];
				if (pen == null)
				{
					pen = new Pen(Color.Transparent, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.TransparentKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00012510 File Offset: 0x00010710
		public static Pen AliceBlue
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.AliceBlueKey];
				if (pen == null)
				{
					pen = new Pen(Color.AliceBlue, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.AliceBlueKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060003BF RID: 959 RVA: 0x00012554 File Offset: 0x00010754
		public static Pen AntiqueWhite
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.AntiqueWhiteKey];
				if (pen == null)
				{
					pen = new Pen(Color.AntiqueWhite, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.AntiqueWhiteKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00012598 File Offset: 0x00010798
		public static Pen Aqua
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.AquaKey];
				if (pen == null)
				{
					pen = new Pen(Color.Aqua, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.AquaKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x000125DC File Offset: 0x000107DC
		public static Pen Aquamarine
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.AquamarineKey];
				if (pen == null)
				{
					pen = new Pen(Color.Aquamarine, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.AquamarineKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00012620 File Offset: 0x00010820
		public static Pen Azure
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.AzureKey];
				if (pen == null)
				{
					pen = new Pen(Color.Azure, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.AzureKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x00012664 File Offset: 0x00010864
		public static Pen Beige
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.BeigeKey];
				if (pen == null)
				{
					pen = new Pen(Color.Beige, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.BeigeKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x000126A8 File Offset: 0x000108A8
		public static Pen Bisque
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.BisqueKey];
				if (pen == null)
				{
					pen = new Pen(Color.Bisque, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.BisqueKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x000126EC File Offset: 0x000108EC
		public static Pen Black
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.BlackKey];
				if (pen == null)
				{
					pen = new Pen(Color.Black, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.BlackKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00012730 File Offset: 0x00010930
		public static Pen BlanchedAlmond
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.BlanchedAlmondKey];
				if (pen == null)
				{
					pen = new Pen(Color.BlanchedAlmond, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.BlanchedAlmondKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x00012774 File Offset: 0x00010974
		public static Pen Blue
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.BlueKey];
				if (pen == null)
				{
					pen = new Pen(Color.Blue, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.BlueKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x000127B8 File Offset: 0x000109B8
		public static Pen BlueViolet
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.BlueVioletKey];
				if (pen == null)
				{
					pen = new Pen(Color.BlueViolet, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.BlueVioletKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x000127FC File Offset: 0x000109FC
		public static Pen Brown
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.BrownKey];
				if (pen == null)
				{
					pen = new Pen(Color.Brown, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.BrownKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00012840 File Offset: 0x00010A40
		public static Pen BurlyWood
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.BurlyWoodKey];
				if (pen == null)
				{
					pen = new Pen(Color.BurlyWood, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.BurlyWoodKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060003CB RID: 971 RVA: 0x00012884 File Offset: 0x00010A84
		public static Pen CadetBlue
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.CadetBlueKey];
				if (pen == null)
				{
					pen = new Pen(Color.CadetBlue, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.CadetBlueKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060003CC RID: 972 RVA: 0x000128C8 File Offset: 0x00010AC8
		public static Pen Chartreuse
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.ChartreuseKey];
				if (pen == null)
				{
					pen = new Pen(Color.Chartreuse, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.ChartreuseKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0001290C File Offset: 0x00010B0C
		public static Pen Chocolate
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.ChocolateKey];
				if (pen == null)
				{
					pen = new Pen(Color.Chocolate, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.ChocolateKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060003CE RID: 974 RVA: 0x00012950 File Offset: 0x00010B50
		public static Pen Coral
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.ChoralKey];
				if (pen == null)
				{
					pen = new Pen(Color.Coral, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.ChoralKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060003CF RID: 975 RVA: 0x00012994 File Offset: 0x00010B94
		public static Pen CornflowerBlue
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.CornflowerBlueKey];
				if (pen == null)
				{
					pen = new Pen(Color.CornflowerBlue, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.CornflowerBlueKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x000129D8 File Offset: 0x00010BD8
		public static Pen Cornsilk
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.CornsilkKey];
				if (pen == null)
				{
					pen = new Pen(Color.Cornsilk, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.CornsilkKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00012A1C File Offset: 0x00010C1C
		public static Pen Crimson
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.CrimsonKey];
				if (pen == null)
				{
					pen = new Pen(Color.Crimson, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.CrimsonKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00012A60 File Offset: 0x00010C60
		public static Pen Cyan
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.CyanKey];
				if (pen == null)
				{
					pen = new Pen(Color.Cyan, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.CyanKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x00012AA4 File Offset: 0x00010CA4
		public static Pen DarkBlue
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.DarkBlueKey];
				if (pen == null)
				{
					pen = new Pen(Color.DarkBlue, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.DarkBlueKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00012AE8 File Offset: 0x00010CE8
		public static Pen DarkCyan
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.DarkCyanKey];
				if (pen == null)
				{
					pen = new Pen(Color.DarkCyan, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.DarkCyanKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x00012B2C File Offset: 0x00010D2C
		public static Pen DarkGoldenrod
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.DarkGoldenrodKey];
				if (pen == null)
				{
					pen = new Pen(Color.DarkGoldenrod, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.DarkGoldenrodKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00012B70 File Offset: 0x00010D70
		public static Pen DarkGray
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.DarkGrayKey];
				if (pen == null)
				{
					pen = new Pen(Color.DarkGray, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.DarkGrayKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00012BB4 File Offset: 0x00010DB4
		public static Pen DarkGreen
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.DarkGreenKey];
				if (pen == null)
				{
					pen = new Pen(Color.DarkGreen, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.DarkGreenKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x00012BF8 File Offset: 0x00010DF8
		public static Pen DarkKhaki
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.DarkKhakiKey];
				if (pen == null)
				{
					pen = new Pen(Color.DarkKhaki, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.DarkKhakiKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00012C3C File Offset: 0x00010E3C
		public static Pen DarkMagenta
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.DarkMagentaKey];
				if (pen == null)
				{
					pen = new Pen(Color.DarkMagenta, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.DarkMagentaKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00012C80 File Offset: 0x00010E80
		public static Pen DarkOliveGreen
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.DarkOliveGreenKey];
				if (pen == null)
				{
					pen = new Pen(Color.DarkOliveGreen, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.DarkOliveGreenKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060003DB RID: 987 RVA: 0x00012CC4 File Offset: 0x00010EC4
		public static Pen DarkOrange
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.DarkOrangeKey];
				if (pen == null)
				{
					pen = new Pen(Color.DarkOrange, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.DarkOrangeKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00012D08 File Offset: 0x00010F08
		public static Pen DarkOrchid
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.DarkOrchidKey];
				if (pen == null)
				{
					pen = new Pen(Color.DarkOrchid, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.DarkOrchidKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00012D4C File Offset: 0x00010F4C
		public static Pen DarkRed
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.DarkRedKey];
				if (pen == null)
				{
					pen = new Pen(Color.DarkRed, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.DarkRedKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00012D90 File Offset: 0x00010F90
		public static Pen DarkSalmon
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.DarkSalmonKey];
				if (pen == null)
				{
					pen = new Pen(Color.DarkSalmon, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.DarkSalmonKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00012DD4 File Offset: 0x00010FD4
		public static Pen DarkSeaGreen
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.DarkSeaGreenKey];
				if (pen == null)
				{
					pen = new Pen(Color.DarkSeaGreen, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.DarkSeaGreenKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00012E18 File Offset: 0x00011018
		public static Pen DarkSlateBlue
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.DarkSlateBlueKey];
				if (pen == null)
				{
					pen = new Pen(Color.DarkSlateBlue, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.DarkSlateBlueKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00012E5C File Offset: 0x0001105C
		public static Pen DarkSlateGray
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.DarkSlateGrayKey];
				if (pen == null)
				{
					pen = new Pen(Color.DarkSlateGray, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.DarkSlateGrayKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00012EA0 File Offset: 0x000110A0
		public static Pen DarkTurquoise
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.DarkTurquoiseKey];
				if (pen == null)
				{
					pen = new Pen(Color.DarkTurquoise, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.DarkTurquoiseKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x00012EE4 File Offset: 0x000110E4
		public static Pen DarkViolet
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.DarkVioletKey];
				if (pen == null)
				{
					pen = new Pen(Color.DarkViolet, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.DarkVioletKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00012F28 File Offset: 0x00011128
		public static Pen DeepPink
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.DeepPinkKey];
				if (pen == null)
				{
					pen = new Pen(Color.DeepPink, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.DeepPinkKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x00012F6C File Offset: 0x0001116C
		public static Pen DeepSkyBlue
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.DeepSkyBlueKey];
				if (pen == null)
				{
					pen = new Pen(Color.DeepSkyBlue, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.DeepSkyBlueKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x00012FB0 File Offset: 0x000111B0
		public static Pen DimGray
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.DimGrayKey];
				if (pen == null)
				{
					pen = new Pen(Color.DimGray, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.DimGrayKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00012FF4 File Offset: 0x000111F4
		public static Pen DodgerBlue
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.DodgerBlueKey];
				if (pen == null)
				{
					pen = new Pen(Color.DodgerBlue, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.DodgerBlueKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00013038 File Offset: 0x00011238
		public static Pen Firebrick
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.FirebrickKey];
				if (pen == null)
				{
					pen = new Pen(Color.Firebrick, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.FirebrickKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0001307C File Offset: 0x0001127C
		public static Pen FloralWhite
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.FloralWhiteKey];
				if (pen == null)
				{
					pen = new Pen(Color.FloralWhite, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.FloralWhiteKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x000130C0 File Offset: 0x000112C0
		public static Pen ForestGreen
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.ForestGreenKey];
				if (pen == null)
				{
					pen = new Pen(Color.ForestGreen, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.ForestGreenKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x00013104 File Offset: 0x00011304
		public static Pen Fuchsia
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.FuchiaKey];
				if (pen == null)
				{
					pen = new Pen(Color.Fuchsia, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.FuchiaKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x00013148 File Offset: 0x00011348
		public static Pen Gainsboro
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.GainsboroKey];
				if (pen == null)
				{
					pen = new Pen(Color.Gainsboro, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.GainsboroKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0001318C File Offset: 0x0001138C
		public static Pen GhostWhite
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.GhostWhiteKey];
				if (pen == null)
				{
					pen = new Pen(Color.GhostWhite, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.GhostWhiteKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x000131D0 File Offset: 0x000113D0
		public static Pen Gold
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.GoldKey];
				if (pen == null)
				{
					pen = new Pen(Color.Gold, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.GoldKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00013214 File Offset: 0x00011414
		public static Pen Goldenrod
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.GoldenrodKey];
				if (pen == null)
				{
					pen = new Pen(Color.Goldenrod, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.GoldenrodKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00013258 File Offset: 0x00011458
		public static Pen Gray
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.GrayKey];
				if (pen == null)
				{
					pen = new Pen(Color.Gray, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.GrayKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0001329C File Offset: 0x0001149C
		public static Pen Green
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.GreenKey];
				if (pen == null)
				{
					pen = new Pen(Color.Green, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.GreenKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x000132E0 File Offset: 0x000114E0
		public static Pen GreenYellow
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.GreenYellowKey];
				if (pen == null)
				{
					pen = new Pen(Color.GreenYellow, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.GreenYellowKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00013324 File Offset: 0x00011524
		public static Pen Honeydew
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.HoneydewKey];
				if (pen == null)
				{
					pen = new Pen(Color.Honeydew, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.HoneydewKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00013368 File Offset: 0x00011568
		public static Pen HotPink
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.HotPinkKey];
				if (pen == null)
				{
					pen = new Pen(Color.HotPink, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.HotPinkKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x000133AC File Offset: 0x000115AC
		public static Pen IndianRed
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.IndianRedKey];
				if (pen == null)
				{
					pen = new Pen(Color.IndianRed, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.IndianRedKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x000133F0 File Offset: 0x000115F0
		public static Pen Indigo
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.IndigoKey];
				if (pen == null)
				{
					pen = new Pen(Color.Indigo, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.IndigoKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00013434 File Offset: 0x00011634
		public static Pen Ivory
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.IvoryKey];
				if (pen == null)
				{
					pen = new Pen(Color.Ivory, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.IvoryKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00013478 File Offset: 0x00011678
		public static Pen Khaki
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.KhakiKey];
				if (pen == null)
				{
					pen = new Pen(Color.Khaki, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.KhakiKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x000134BC File Offset: 0x000116BC
		public static Pen Lavender
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.LavenderKey];
				if (pen == null)
				{
					pen = new Pen(Color.Lavender, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.LavenderKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00013500 File Offset: 0x00011700
		public static Pen LavenderBlush
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.LavenderBlushKey];
				if (pen == null)
				{
					pen = new Pen(Color.LavenderBlush, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.LavenderBlushKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x00013544 File Offset: 0x00011744
		public static Pen LawnGreen
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.LawnGreenKey];
				if (pen == null)
				{
					pen = new Pen(Color.LawnGreen, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.LawnGreenKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00013588 File Offset: 0x00011788
		public static Pen LemonChiffon
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.LemonChiffonKey];
				if (pen == null)
				{
					pen = new Pen(Color.LemonChiffon, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.LemonChiffonKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x000135CC File Offset: 0x000117CC
		public static Pen LightBlue
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.LightBlueKey];
				if (pen == null)
				{
					pen = new Pen(Color.LightBlue, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.LightBlueKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x00013610 File Offset: 0x00011810
		public static Pen LightCoral
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.LightCoralKey];
				if (pen == null)
				{
					pen = new Pen(Color.LightCoral, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.LightCoralKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x00013654 File Offset: 0x00011854
		public static Pen LightCyan
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.LightCyanKey];
				if (pen == null)
				{
					pen = new Pen(Color.LightCyan, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.LightCyanKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00013698 File Offset: 0x00011898
		public static Pen LightGoldenrodYellow
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.LightGoldenrodYellowKey];
				if (pen == null)
				{
					pen = new Pen(Color.LightGoldenrodYellow, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.LightGoldenrodYellowKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x000136DC File Offset: 0x000118DC
		public static Pen LightGreen
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.LightGreenKey];
				if (pen == null)
				{
					pen = new Pen(Color.LightGreen, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.LightGreenKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00013720 File Offset: 0x00011920
		public static Pen LightGray
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.LightGrayKey];
				if (pen == null)
				{
					pen = new Pen(Color.LightGray, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.LightGrayKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x00013764 File Offset: 0x00011964
		public static Pen LightPink
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.LightPinkKey];
				if (pen == null)
				{
					pen = new Pen(Color.LightPink, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.LightPinkKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x000137A8 File Offset: 0x000119A8
		public static Pen LightSalmon
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.LightSalmonKey];
				if (pen == null)
				{
					pen = new Pen(Color.LightSalmon, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.LightSalmonKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x000137EC File Offset: 0x000119EC
		public static Pen LightSeaGreen
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.LightSeaGreenKey];
				if (pen == null)
				{
					pen = new Pen(Color.LightSeaGreen, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.LightSeaGreenKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x00013830 File Offset: 0x00011A30
		public static Pen LightSkyBlue
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.LightSkyBlueKey];
				if (pen == null)
				{
					pen = new Pen(Color.LightSkyBlue, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.LightSkyBlueKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x00013874 File Offset: 0x00011A74
		public static Pen LightSlateGray
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.LightSlateGrayKey];
				if (pen == null)
				{
					pen = new Pen(Color.LightSlateGray, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.LightSlateGrayKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x000138B8 File Offset: 0x00011AB8
		public static Pen LightSteelBlue
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.LightSteelBlueKey];
				if (pen == null)
				{
					pen = new Pen(Color.LightSteelBlue, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.LightSteelBlueKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x000138FC File Offset: 0x00011AFC
		public static Pen LightYellow
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.LightYellowKey];
				if (pen == null)
				{
					pen = new Pen(Color.LightYellow, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.LightYellowKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00013940 File Offset: 0x00011B40
		public static Pen Lime
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.LimeKey];
				if (pen == null)
				{
					pen = new Pen(Color.Lime, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.LimeKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00013984 File Offset: 0x00011B84
		public static Pen LimeGreen
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.LimeGreenKey];
				if (pen == null)
				{
					pen = new Pen(Color.LimeGreen, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.LimeGreenKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x000139C8 File Offset: 0x00011BC8
		public static Pen Linen
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.LinenKey];
				if (pen == null)
				{
					pen = new Pen(Color.Linen, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.LinenKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00013A0C File Offset: 0x00011C0C
		public static Pen Magenta
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.MagentaKey];
				if (pen == null)
				{
					pen = new Pen(Color.Magenta, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.MagentaKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x00013A50 File Offset: 0x00011C50
		public static Pen Maroon
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.MaroonKey];
				if (pen == null)
				{
					pen = new Pen(Color.Maroon, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.MaroonKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x00013A94 File Offset: 0x00011C94
		public static Pen MediumAquamarine
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.MediumAquamarineKey];
				if (pen == null)
				{
					pen = new Pen(Color.MediumAquamarine, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.MediumAquamarineKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x00013AD8 File Offset: 0x00011CD8
		public static Pen MediumBlue
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.MediumBlueKey];
				if (pen == null)
				{
					pen = new Pen(Color.MediumBlue, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.MediumBlueKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00013B1C File Offset: 0x00011D1C
		public static Pen MediumOrchid
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.MediumOrchidKey];
				if (pen == null)
				{
					pen = new Pen(Color.MediumOrchid, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.MediumOrchidKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x00013B60 File Offset: 0x00011D60
		public static Pen MediumPurple
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.MediumPurpleKey];
				if (pen == null)
				{
					pen = new Pen(Color.MediumPurple, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.MediumPurpleKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x00013BA4 File Offset: 0x00011DA4
		public static Pen MediumSeaGreen
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.MediumSeaGreenKey];
				if (pen == null)
				{
					pen = new Pen(Color.MediumSeaGreen, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.MediumSeaGreenKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x00013BE8 File Offset: 0x00011DE8
		public static Pen MediumSlateBlue
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.MediumSlateBlueKey];
				if (pen == null)
				{
					pen = new Pen(Color.MediumSlateBlue, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.MediumSlateBlueKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x00013C2C File Offset: 0x00011E2C
		public static Pen MediumSpringGreen
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.MediumSpringGreenKey];
				if (pen == null)
				{
					pen = new Pen(Color.MediumSpringGreen, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.MediumSpringGreenKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x00013C70 File Offset: 0x00011E70
		public static Pen MediumTurquoise
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.MediumTurquoiseKey];
				if (pen == null)
				{
					pen = new Pen(Color.MediumTurquoise, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.MediumTurquoiseKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x00013CB4 File Offset: 0x00011EB4
		public static Pen MediumVioletRed
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.MediumVioletRedKey];
				if (pen == null)
				{
					pen = new Pen(Color.MediumVioletRed, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.MediumVioletRedKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00013CF8 File Offset: 0x00011EF8
		public static Pen MidnightBlue
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.MidnightBlueKey];
				if (pen == null)
				{
					pen = new Pen(Color.MidnightBlue, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.MidnightBlueKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x00013D3C File Offset: 0x00011F3C
		public static Pen MintCream
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.MintCreamKey];
				if (pen == null)
				{
					pen = new Pen(Color.MintCream, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.MintCreamKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x00013D80 File Offset: 0x00011F80
		public static Pen MistyRose
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.MistyRoseKey];
				if (pen == null)
				{
					pen = new Pen(Color.MistyRose, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.MistyRoseKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x00013DC4 File Offset: 0x00011FC4
		public static Pen Moccasin
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.MoccasinKey];
				if (pen == null)
				{
					pen = new Pen(Color.Moccasin, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.MoccasinKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x00013E08 File Offset: 0x00012008
		public static Pen NavajoWhite
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.NavajoWhiteKey];
				if (pen == null)
				{
					pen = new Pen(Color.NavajoWhite, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.NavajoWhiteKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x00013E4C File Offset: 0x0001204C
		public static Pen Navy
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.NavyKey];
				if (pen == null)
				{
					pen = new Pen(Color.Navy, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.NavyKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x00013E90 File Offset: 0x00012090
		public static Pen OldLace
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.OldLaceKey];
				if (pen == null)
				{
					pen = new Pen(Color.OldLace, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.OldLaceKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x00013ED4 File Offset: 0x000120D4
		public static Pen Olive
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.OliveKey];
				if (pen == null)
				{
					pen = new Pen(Color.Olive, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.OliveKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x00013F18 File Offset: 0x00012118
		public static Pen OliveDrab
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.OliveDrabKey];
				if (pen == null)
				{
					pen = new Pen(Color.OliveDrab, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.OliveDrabKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00013F5C File Offset: 0x0001215C
		public static Pen Orange
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.OrangeKey];
				if (pen == null)
				{
					pen = new Pen(Color.Orange, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.OrangeKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x00013FA0 File Offset: 0x000121A0
		public static Pen OrangeRed
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.OrangeRedKey];
				if (pen == null)
				{
					pen = new Pen(Color.OrangeRed, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.OrangeRedKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x00013FE4 File Offset: 0x000121E4
		public static Pen Orchid
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.OrchidKey];
				if (pen == null)
				{
					pen = new Pen(Color.Orchid, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.OrchidKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x00014028 File Offset: 0x00012228
		public static Pen PaleGoldenrod
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.PaleGoldenrodKey];
				if (pen == null)
				{
					pen = new Pen(Color.PaleGoldenrod, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.PaleGoldenrodKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0001406C File Offset: 0x0001226C
		public static Pen PaleGreen
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.PaleGreenKey];
				if (pen == null)
				{
					pen = new Pen(Color.PaleGreen, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.PaleGreenKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x000140B0 File Offset: 0x000122B0
		public static Pen PaleTurquoise
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.PaleTurquoiseKey];
				if (pen == null)
				{
					pen = new Pen(Color.PaleTurquoise, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.PaleTurquoiseKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x000140F4 File Offset: 0x000122F4
		public static Pen PaleVioletRed
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.PaleVioletRedKey];
				if (pen == null)
				{
					pen = new Pen(Color.PaleVioletRed, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.PaleVioletRedKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x00014138 File Offset: 0x00012338
		public static Pen PapayaWhip
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.PapayaWhipKey];
				if (pen == null)
				{
					pen = new Pen(Color.PapayaWhip, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.PapayaWhipKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0001417C File Offset: 0x0001237C
		public static Pen PeachPuff
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.PeachPuffKey];
				if (pen == null)
				{
					pen = new Pen(Color.PeachPuff, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.PeachPuffKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x000141C0 File Offset: 0x000123C0
		public static Pen Peru
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.PeruKey];
				if (pen == null)
				{
					pen = new Pen(Color.Peru, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.PeruKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00014204 File Offset: 0x00012404
		public static Pen Pink
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.PinkKey];
				if (pen == null)
				{
					pen = new Pen(Color.Pink, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.PinkKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x00014248 File Offset: 0x00012448
		public static Pen Plum
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.PlumKey];
				if (pen == null)
				{
					pen = new Pen(Color.Plum, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.PlumKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0001428C File Offset: 0x0001248C
		public static Pen PowderBlue
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.PowderBlueKey];
				if (pen == null)
				{
					pen = new Pen(Color.PowderBlue, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.PowderBlueKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x000142D0 File Offset: 0x000124D0
		public static Pen Purple
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.PurpleKey];
				if (pen == null)
				{
					pen = new Pen(Color.Purple, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.PurpleKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00014314 File Offset: 0x00012514
		public static Pen Red
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.RedKey];
				if (pen == null)
				{
					pen = new Pen(Color.Red, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.RedKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x00014358 File Offset: 0x00012558
		public static Pen RosyBrown
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.RosyBrownKey];
				if (pen == null)
				{
					pen = new Pen(Color.RosyBrown, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.RosyBrownKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0001439C File Offset: 0x0001259C
		public static Pen RoyalBlue
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.RoyalBlueKey];
				if (pen == null)
				{
					pen = new Pen(Color.RoyalBlue, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.RoyalBlueKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x000143E0 File Offset: 0x000125E0
		public static Pen SaddleBrown
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.SaddleBrownKey];
				if (pen == null)
				{
					pen = new Pen(Color.SaddleBrown, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.SaddleBrownKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x00014424 File Offset: 0x00012624
		public static Pen Salmon
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.SalmonKey];
				if (pen == null)
				{
					pen = new Pen(Color.Salmon, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.SalmonKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x00014468 File Offset: 0x00012668
		public static Pen SandyBrown
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.SandyBrownKey];
				if (pen == null)
				{
					pen = new Pen(Color.SandyBrown, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.SandyBrownKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x000144AC File Offset: 0x000126AC
		public static Pen SeaGreen
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.SeaGreenKey];
				if (pen == null)
				{
					pen = new Pen(Color.SeaGreen, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.SeaGreenKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x000144F0 File Offset: 0x000126F0
		public static Pen SeaShell
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.SeaShellKey];
				if (pen == null)
				{
					pen = new Pen(Color.SeaShell, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.SeaShellKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x00014534 File Offset: 0x00012734
		public static Pen Sienna
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.SiennaKey];
				if (pen == null)
				{
					pen = new Pen(Color.Sienna, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.SiennaKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x00014578 File Offset: 0x00012778
		public static Pen Silver
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.SilverKey];
				if (pen == null)
				{
					pen = new Pen(Color.Silver, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.SilverKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x000145BC File Offset: 0x000127BC
		public static Pen SkyBlue
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.SkyBlueKey];
				if (pen == null)
				{
					pen = new Pen(Color.SkyBlue, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.SkyBlueKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x00014600 File Offset: 0x00012800
		public static Pen SlateBlue
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.SlateBlueKey];
				if (pen == null)
				{
					pen = new Pen(Color.SlateBlue, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.SlateBlueKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00014644 File Offset: 0x00012844
		public static Pen SlateGray
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.SlateGrayKey];
				if (pen == null)
				{
					pen = new Pen(Color.SlateGray, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.SlateGrayKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x00014688 File Offset: 0x00012888
		public static Pen Snow
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.SnowKey];
				if (pen == null)
				{
					pen = new Pen(Color.Snow, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.SnowKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x000146CC File Offset: 0x000128CC
		public static Pen SpringGreen
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.SpringGreenKey];
				if (pen == null)
				{
					pen = new Pen(Color.SpringGreen, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.SpringGreenKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x00014710 File Offset: 0x00012910
		public static Pen SteelBlue
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.SteelBlueKey];
				if (pen == null)
				{
					pen = new Pen(Color.SteelBlue, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.SteelBlueKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x00014754 File Offset: 0x00012954
		public static Pen Tan
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.TanKey];
				if (pen == null)
				{
					pen = new Pen(Color.Tan, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.TanKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x00014798 File Offset: 0x00012998
		public static Pen Teal
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.TealKey];
				if (pen == null)
				{
					pen = new Pen(Color.Teal, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.TealKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x000147DC File Offset: 0x000129DC
		public static Pen Thistle
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.ThistleKey];
				if (pen == null)
				{
					pen = new Pen(Color.Thistle, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.ThistleKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x00014820 File Offset: 0x00012A20
		public static Pen Tomato
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.TomatoKey];
				if (pen == null)
				{
					pen = new Pen(Color.Tomato, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.TomatoKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x00014864 File Offset: 0x00012A64
		public static Pen Turquoise
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.TurquoiseKey];
				if (pen == null)
				{
					pen = new Pen(Color.Turquoise, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.TurquoiseKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x000148A8 File Offset: 0x00012AA8
		public static Pen Violet
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.VioletKey];
				if (pen == null)
				{
					pen = new Pen(Color.Violet, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.VioletKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x000148EC File Offset: 0x00012AEC
		public static Pen Wheat
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.WheatKey];
				if (pen == null)
				{
					pen = new Pen(Color.Wheat, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.WheatKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x00014930 File Offset: 0x00012B30
		public static Pen White
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.WhiteKey];
				if (pen == null)
				{
					pen = new Pen(Color.White, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.WhiteKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x00014974 File Offset: 0x00012B74
		public static Pen WhiteSmoke
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.WhiteSmokeKey];
				if (pen == null)
				{
					pen = new Pen(Color.WhiteSmoke, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.WhiteSmokeKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x000149B8 File Offset: 0x00012BB8
		public static Pen Yellow
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.YellowKey];
				if (pen == null)
				{
					pen = new Pen(Color.Yellow, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.YellowKey] = pen;
				}
				return pen;
			}
		}

		/// <summary>A system-defined <see cref="T:System.Drawing.Pen" /> object with a width of 1.</summary>
		/// <returns>A <see cref="T:System.Drawing.Pen" /> object set to a system-defined color.</returns>
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x000149FC File Offset: 0x00012BFC
		public static Pen YellowGreen
		{
			get
			{
				Pen pen = (Pen)SafeNativeMethods.Gdip.ThreadData[Pens.YellowGreenKey];
				if (pen == null)
				{
					pen = new Pen(Color.YellowGreen, true);
					SafeNativeMethods.Gdip.ThreadData[Pens.YellowGreenKey] = pen;
				}
				return pen;
			}
		}

		// Token: 0x0400026F RID: 623
		private static readonly object TransparentKey = new object();

		// Token: 0x04000270 RID: 624
		private static readonly object AliceBlueKey = new object();

		// Token: 0x04000271 RID: 625
		private static readonly object AntiqueWhiteKey = new object();

		// Token: 0x04000272 RID: 626
		private static readonly object AquaKey = new object();

		// Token: 0x04000273 RID: 627
		private static readonly object AquamarineKey = new object();

		// Token: 0x04000274 RID: 628
		private static readonly object AzureKey = new object();

		// Token: 0x04000275 RID: 629
		private static readonly object BeigeKey = new object();

		// Token: 0x04000276 RID: 630
		private static readonly object BisqueKey = new object();

		// Token: 0x04000277 RID: 631
		private static readonly object BlackKey = new object();

		// Token: 0x04000278 RID: 632
		private static readonly object BlanchedAlmondKey = new object();

		// Token: 0x04000279 RID: 633
		private static readonly object BlueKey = new object();

		// Token: 0x0400027A RID: 634
		private static readonly object BlueVioletKey = new object();

		// Token: 0x0400027B RID: 635
		private static readonly object BrownKey = new object();

		// Token: 0x0400027C RID: 636
		private static readonly object BurlyWoodKey = new object();

		// Token: 0x0400027D RID: 637
		private static readonly object CadetBlueKey = new object();

		// Token: 0x0400027E RID: 638
		private static readonly object ChartreuseKey = new object();

		// Token: 0x0400027F RID: 639
		private static readonly object ChocolateKey = new object();

		// Token: 0x04000280 RID: 640
		private static readonly object ChoralKey = new object();

		// Token: 0x04000281 RID: 641
		private static readonly object CornflowerBlueKey = new object();

		// Token: 0x04000282 RID: 642
		private static readonly object CornsilkKey = new object();

		// Token: 0x04000283 RID: 643
		private static readonly object CrimsonKey = new object();

		// Token: 0x04000284 RID: 644
		private static readonly object CyanKey = new object();

		// Token: 0x04000285 RID: 645
		private static readonly object DarkBlueKey = new object();

		// Token: 0x04000286 RID: 646
		private static readonly object DarkCyanKey = new object();

		// Token: 0x04000287 RID: 647
		private static readonly object DarkGoldenrodKey = new object();

		// Token: 0x04000288 RID: 648
		private static readonly object DarkGrayKey = new object();

		// Token: 0x04000289 RID: 649
		private static readonly object DarkGreenKey = new object();

		// Token: 0x0400028A RID: 650
		private static readonly object DarkKhakiKey = new object();

		// Token: 0x0400028B RID: 651
		private static readonly object DarkMagentaKey = new object();

		// Token: 0x0400028C RID: 652
		private static readonly object DarkOliveGreenKey = new object();

		// Token: 0x0400028D RID: 653
		private static readonly object DarkOrangeKey = new object();

		// Token: 0x0400028E RID: 654
		private static readonly object DarkOrchidKey = new object();

		// Token: 0x0400028F RID: 655
		private static readonly object DarkRedKey = new object();

		// Token: 0x04000290 RID: 656
		private static readonly object DarkSalmonKey = new object();

		// Token: 0x04000291 RID: 657
		private static readonly object DarkSeaGreenKey = new object();

		// Token: 0x04000292 RID: 658
		private static readonly object DarkSlateBlueKey = new object();

		// Token: 0x04000293 RID: 659
		private static readonly object DarkSlateGrayKey = new object();

		// Token: 0x04000294 RID: 660
		private static readonly object DarkTurquoiseKey = new object();

		// Token: 0x04000295 RID: 661
		private static readonly object DarkVioletKey = new object();

		// Token: 0x04000296 RID: 662
		private static readonly object DeepPinkKey = new object();

		// Token: 0x04000297 RID: 663
		private static readonly object DeepSkyBlueKey = new object();

		// Token: 0x04000298 RID: 664
		private static readonly object DimGrayKey = new object();

		// Token: 0x04000299 RID: 665
		private static readonly object DodgerBlueKey = new object();

		// Token: 0x0400029A RID: 666
		private static readonly object FirebrickKey = new object();

		// Token: 0x0400029B RID: 667
		private static readonly object FloralWhiteKey = new object();

		// Token: 0x0400029C RID: 668
		private static readonly object ForestGreenKey = new object();

		// Token: 0x0400029D RID: 669
		private static readonly object FuchiaKey = new object();

		// Token: 0x0400029E RID: 670
		private static readonly object GainsboroKey = new object();

		// Token: 0x0400029F RID: 671
		private static readonly object GhostWhiteKey = new object();

		// Token: 0x040002A0 RID: 672
		private static readonly object GoldKey = new object();

		// Token: 0x040002A1 RID: 673
		private static readonly object GoldenrodKey = new object();

		// Token: 0x040002A2 RID: 674
		private static readonly object GrayKey = new object();

		// Token: 0x040002A3 RID: 675
		private static readonly object GreenKey = new object();

		// Token: 0x040002A4 RID: 676
		private static readonly object GreenYellowKey = new object();

		// Token: 0x040002A5 RID: 677
		private static readonly object HoneydewKey = new object();

		// Token: 0x040002A6 RID: 678
		private static readonly object HotPinkKey = new object();

		// Token: 0x040002A7 RID: 679
		private static readonly object IndianRedKey = new object();

		// Token: 0x040002A8 RID: 680
		private static readonly object IndigoKey = new object();

		// Token: 0x040002A9 RID: 681
		private static readonly object IvoryKey = new object();

		// Token: 0x040002AA RID: 682
		private static readonly object KhakiKey = new object();

		// Token: 0x040002AB RID: 683
		private static readonly object LavenderKey = new object();

		// Token: 0x040002AC RID: 684
		private static readonly object LavenderBlushKey = new object();

		// Token: 0x040002AD RID: 685
		private static readonly object LawnGreenKey = new object();

		// Token: 0x040002AE RID: 686
		private static readonly object LemonChiffonKey = new object();

		// Token: 0x040002AF RID: 687
		private static readonly object LightBlueKey = new object();

		// Token: 0x040002B0 RID: 688
		private static readonly object LightCoralKey = new object();

		// Token: 0x040002B1 RID: 689
		private static readonly object LightCyanKey = new object();

		// Token: 0x040002B2 RID: 690
		private static readonly object LightGoldenrodYellowKey = new object();

		// Token: 0x040002B3 RID: 691
		private static readonly object LightGreenKey = new object();

		// Token: 0x040002B4 RID: 692
		private static readonly object LightGrayKey = new object();

		// Token: 0x040002B5 RID: 693
		private static readonly object LightPinkKey = new object();

		// Token: 0x040002B6 RID: 694
		private static readonly object LightSalmonKey = new object();

		// Token: 0x040002B7 RID: 695
		private static readonly object LightSeaGreenKey = new object();

		// Token: 0x040002B8 RID: 696
		private static readonly object LightSkyBlueKey = new object();

		// Token: 0x040002B9 RID: 697
		private static readonly object LightSlateGrayKey = new object();

		// Token: 0x040002BA RID: 698
		private static readonly object LightSteelBlueKey = new object();

		// Token: 0x040002BB RID: 699
		private static readonly object LightYellowKey = new object();

		// Token: 0x040002BC RID: 700
		private static readonly object LimeKey = new object();

		// Token: 0x040002BD RID: 701
		private static readonly object LimeGreenKey = new object();

		// Token: 0x040002BE RID: 702
		private static readonly object LinenKey = new object();

		// Token: 0x040002BF RID: 703
		private static readonly object MagentaKey = new object();

		// Token: 0x040002C0 RID: 704
		private static readonly object MaroonKey = new object();

		// Token: 0x040002C1 RID: 705
		private static readonly object MediumAquamarineKey = new object();

		// Token: 0x040002C2 RID: 706
		private static readonly object MediumBlueKey = new object();

		// Token: 0x040002C3 RID: 707
		private static readonly object MediumOrchidKey = new object();

		// Token: 0x040002C4 RID: 708
		private static readonly object MediumPurpleKey = new object();

		// Token: 0x040002C5 RID: 709
		private static readonly object MediumSeaGreenKey = new object();

		// Token: 0x040002C6 RID: 710
		private static readonly object MediumSlateBlueKey = new object();

		// Token: 0x040002C7 RID: 711
		private static readonly object MediumSpringGreenKey = new object();

		// Token: 0x040002C8 RID: 712
		private static readonly object MediumTurquoiseKey = new object();

		// Token: 0x040002C9 RID: 713
		private static readonly object MediumVioletRedKey = new object();

		// Token: 0x040002CA RID: 714
		private static readonly object MidnightBlueKey = new object();

		// Token: 0x040002CB RID: 715
		private static readonly object MintCreamKey = new object();

		// Token: 0x040002CC RID: 716
		private static readonly object MistyRoseKey = new object();

		// Token: 0x040002CD RID: 717
		private static readonly object MoccasinKey = new object();

		// Token: 0x040002CE RID: 718
		private static readonly object NavajoWhiteKey = new object();

		// Token: 0x040002CF RID: 719
		private static readonly object NavyKey = new object();

		// Token: 0x040002D0 RID: 720
		private static readonly object OldLaceKey = new object();

		// Token: 0x040002D1 RID: 721
		private static readonly object OliveKey = new object();

		// Token: 0x040002D2 RID: 722
		private static readonly object OliveDrabKey = new object();

		// Token: 0x040002D3 RID: 723
		private static readonly object OrangeKey = new object();

		// Token: 0x040002D4 RID: 724
		private static readonly object OrangeRedKey = new object();

		// Token: 0x040002D5 RID: 725
		private static readonly object OrchidKey = new object();

		// Token: 0x040002D6 RID: 726
		private static readonly object PaleGoldenrodKey = new object();

		// Token: 0x040002D7 RID: 727
		private static readonly object PaleGreenKey = new object();

		// Token: 0x040002D8 RID: 728
		private static readonly object PaleTurquoiseKey = new object();

		// Token: 0x040002D9 RID: 729
		private static readonly object PaleVioletRedKey = new object();

		// Token: 0x040002DA RID: 730
		private static readonly object PapayaWhipKey = new object();

		// Token: 0x040002DB RID: 731
		private static readonly object PeachPuffKey = new object();

		// Token: 0x040002DC RID: 732
		private static readonly object PeruKey = new object();

		// Token: 0x040002DD RID: 733
		private static readonly object PinkKey = new object();

		// Token: 0x040002DE RID: 734
		private static readonly object PlumKey = new object();

		// Token: 0x040002DF RID: 735
		private static readonly object PowderBlueKey = new object();

		// Token: 0x040002E0 RID: 736
		private static readonly object PurpleKey = new object();

		// Token: 0x040002E1 RID: 737
		private static readonly object RedKey = new object();

		// Token: 0x040002E2 RID: 738
		private static readonly object RosyBrownKey = new object();

		// Token: 0x040002E3 RID: 739
		private static readonly object RoyalBlueKey = new object();

		// Token: 0x040002E4 RID: 740
		private static readonly object SaddleBrownKey = new object();

		// Token: 0x040002E5 RID: 741
		private static readonly object SalmonKey = new object();

		// Token: 0x040002E6 RID: 742
		private static readonly object SandyBrownKey = new object();

		// Token: 0x040002E7 RID: 743
		private static readonly object SeaGreenKey = new object();

		// Token: 0x040002E8 RID: 744
		private static readonly object SeaShellKey = new object();

		// Token: 0x040002E9 RID: 745
		private static readonly object SiennaKey = new object();

		// Token: 0x040002EA RID: 746
		private static readonly object SilverKey = new object();

		// Token: 0x040002EB RID: 747
		private static readonly object SkyBlueKey = new object();

		// Token: 0x040002EC RID: 748
		private static readonly object SlateBlueKey = new object();

		// Token: 0x040002ED RID: 749
		private static readonly object SlateGrayKey = new object();

		// Token: 0x040002EE RID: 750
		private static readonly object SnowKey = new object();

		// Token: 0x040002EF RID: 751
		private static readonly object SpringGreenKey = new object();

		// Token: 0x040002F0 RID: 752
		private static readonly object SteelBlueKey = new object();

		// Token: 0x040002F1 RID: 753
		private static readonly object TanKey = new object();

		// Token: 0x040002F2 RID: 754
		private static readonly object TealKey = new object();

		// Token: 0x040002F3 RID: 755
		private static readonly object ThistleKey = new object();

		// Token: 0x040002F4 RID: 756
		private static readonly object TomatoKey = new object();

		// Token: 0x040002F5 RID: 757
		private static readonly object TurquoiseKey = new object();

		// Token: 0x040002F6 RID: 758
		private static readonly object VioletKey = new object();

		// Token: 0x040002F7 RID: 759
		private static readonly object WheatKey = new object();

		// Token: 0x040002F8 RID: 760
		private static readonly object WhiteKey = new object();

		// Token: 0x040002F9 RID: 761
		private static readonly object WhiteSmokeKey = new object();

		// Token: 0x040002FA RID: 762
		private static readonly object YellowKey = new object();

		// Token: 0x040002FB RID: 763
		private static readonly object YellowGreenKey = new object();
	}
}
