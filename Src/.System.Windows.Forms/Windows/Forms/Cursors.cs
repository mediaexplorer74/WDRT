using System;

namespace System.Windows.Forms
{
	/// <summary>Provides a collection of <see cref="T:System.Windows.Forms.Cursor" /> objects for use by a Windows Forms application.</summary>
	// Token: 0x02000177 RID: 375
	public sealed class Cursors
	{
		// Token: 0x06001409 RID: 5129 RVA: 0x00002843 File Offset: 0x00000A43
		private Cursors()
		{
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x00043742 File Offset: 0x00041942
		internal static Cursor KnownCursorFromHCursor(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				return null;
			}
			return new Cursor(handle);
		}

		/// <summary>Gets the cursor that appears when an application starts.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears when an application starts.</returns>
		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x00043759 File Offset: 0x00041959
		public static Cursor AppStarting
		{
			get
			{
				if (Cursors.appStarting == null)
				{
					Cursors.appStarting = new Cursor(32650, 0);
				}
				return Cursors.appStarting;
			}
		}

		/// <summary>Gets the arrow cursor.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the arrow cursor.</returns>
		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x0004377D File Offset: 0x0004197D
		public static Cursor Arrow
		{
			get
			{
				if (Cursors.arrow == null)
				{
					Cursors.arrow = new Cursor(32512, 0);
				}
				return Cursors.arrow;
			}
		}

		/// <summary>Gets the crosshair cursor.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the crosshair cursor.</returns>
		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x000437A1 File Offset: 0x000419A1
		public static Cursor Cross
		{
			get
			{
				if (Cursors.cross == null)
				{
					Cursors.cross = new Cursor(32515, 0);
				}
				return Cursors.cross;
			}
		}

		/// <summary>Gets the default cursor, which is usually an arrow cursor.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the default cursor.</returns>
		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x0600140E RID: 5134 RVA: 0x000437C5 File Offset: 0x000419C5
		public static Cursor Default
		{
			get
			{
				if (Cursors.defaultCursor == null)
				{
					Cursors.defaultCursor = new Cursor(32512, 0);
				}
				return Cursors.defaultCursor;
			}
		}

		/// <summary>Gets the I-beam cursor, which is used to show where the text cursor appears when the mouse is clicked.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the I-beam cursor.</returns>
		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x000437E9 File Offset: 0x000419E9
		public static Cursor IBeam
		{
			get
			{
				if (Cursors.iBeam == null)
				{
					Cursors.iBeam = new Cursor(32513, 0);
				}
				return Cursors.iBeam;
			}
		}

		/// <summary>Gets the cursor that indicates that a particular region is invalid for the current operation.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that indicates that a particular region is invalid for the current operation.</returns>
		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x0004380D File Offset: 0x00041A0D
		public static Cursor No
		{
			get
			{
				if (Cursors.no == null)
				{
					Cursors.no = new Cursor(32648, 0);
				}
				return Cursors.no;
			}
		}

		/// <summary>Gets the four-headed sizing cursor, which consists of four joined arrows that point north, south, east, and west.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the four-headed sizing cursor.</returns>
		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x00043831 File Offset: 0x00041A31
		public static Cursor SizeAll
		{
			get
			{
				if (Cursors.sizeAll == null)
				{
					Cursors.sizeAll = new Cursor(32646, 0);
				}
				return Cursors.sizeAll;
			}
		}

		/// <summary>Gets the two-headed diagonal (northeast/southwest) sizing cursor.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents two-headed diagonal (northeast/southwest) sizing cursor.</returns>
		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001412 RID: 5138 RVA: 0x00043855 File Offset: 0x00041A55
		public static Cursor SizeNESW
		{
			get
			{
				if (Cursors.sizeNESW == null)
				{
					Cursors.sizeNESW = new Cursor(32643, 0);
				}
				return Cursors.sizeNESW;
			}
		}

		/// <summary>Gets the two-headed vertical (north/south) sizing cursor.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the two-headed vertical (north/south) sizing cursor.</returns>
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x00043879 File Offset: 0x00041A79
		public static Cursor SizeNS
		{
			get
			{
				if (Cursors.sizeNS == null)
				{
					Cursors.sizeNS = new Cursor(32645, 0);
				}
				return Cursors.sizeNS;
			}
		}

		/// <summary>Gets the two-headed diagonal (northwest/southeast) sizing cursor.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the two-headed diagonal (northwest/southeast) sizing cursor.</returns>
		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x0004389D File Offset: 0x00041A9D
		public static Cursor SizeNWSE
		{
			get
			{
				if (Cursors.sizeNWSE == null)
				{
					Cursors.sizeNWSE = new Cursor(32642, 0);
				}
				return Cursors.sizeNWSE;
			}
		}

		/// <summary>Gets the two-headed horizontal (west/east) sizing cursor.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the two-headed horizontal (west/east) sizing cursor.</returns>
		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x000438C1 File Offset: 0x00041AC1
		public static Cursor SizeWE
		{
			get
			{
				if (Cursors.sizeWE == null)
				{
					Cursors.sizeWE = new Cursor(32644, 0);
				}
				return Cursors.sizeWE;
			}
		}

		/// <summary>Gets the up arrow cursor, typically used to identify an insertion point.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the up arrow cursor.</returns>
		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x000438E5 File Offset: 0x00041AE5
		public static Cursor UpArrow
		{
			get
			{
				if (Cursors.upArrow == null)
				{
					Cursors.upArrow = new Cursor(32516, 0);
				}
				return Cursors.upArrow;
			}
		}

		/// <summary>Gets the wait cursor, typically an hourglass shape.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the wait cursor.</returns>
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001417 RID: 5143 RVA: 0x00043909 File Offset: 0x00041B09
		public static Cursor WaitCursor
		{
			get
			{
				if (Cursors.wait == null)
				{
					Cursors.wait = new Cursor(32514, 0);
				}
				return Cursors.wait;
			}
		}

		/// <summary>Gets the Help cursor, which is a combination of an arrow and a question mark.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the Help cursor.</returns>
		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x0004392D File Offset: 0x00041B2D
		public static Cursor Help
		{
			get
			{
				if (Cursors.help == null)
				{
					Cursors.help = new Cursor(32651, 0);
				}
				return Cursors.help;
			}
		}

		/// <summary>Gets the cursor that appears when the mouse is positioned over a horizontal splitter bar.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears when the mouse is positioned over a horizontal splitter bar.</returns>
		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x00043951 File Offset: 0x00041B51
		public static Cursor HSplit
		{
			get
			{
				if (Cursors.hSplit == null)
				{
					Cursors.hSplit = new Cursor("hsplit.cur", 0);
				}
				return Cursors.hSplit;
			}
		}

		/// <summary>Gets the cursor that appears when the mouse is positioned over a vertical splitter bar.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears when the mouse is positioned over a vertical splitter bar.</returns>
		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x0600141A RID: 5146 RVA: 0x00043975 File Offset: 0x00041B75
		public static Cursor VSplit
		{
			get
			{
				if (Cursors.vSplit == null)
				{
					Cursors.vSplit = new Cursor("vsplit.cur", 0);
				}
				return Cursors.vSplit;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is not moving, but the window can be scrolled in both a horizontal and vertical direction.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is not moving.</returns>
		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x00043999 File Offset: 0x00041B99
		public static Cursor NoMove2D
		{
			get
			{
				if (Cursors.noMove2D == null)
				{
					Cursors.noMove2D = new Cursor("nomove2d.cur", 0);
				}
				return Cursors.noMove2D;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is not moving, but the window can be scrolled in a horizontal direction.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is not moving.</returns>
		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x000439BD File Offset: 0x00041BBD
		public static Cursor NoMoveHoriz
		{
			get
			{
				if (Cursors.noMoveHoriz == null)
				{
					Cursors.noMoveHoriz = new Cursor("nomoveh.cur", 0);
				}
				return Cursors.noMoveHoriz;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is not moving, but the window can be scrolled in a vertical direction.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is not moving.</returns>
		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x0600141D RID: 5149 RVA: 0x000439E1 File Offset: 0x00041BE1
		public static Cursor NoMoveVert
		{
			get
			{
				if (Cursors.noMoveVert == null)
				{
					Cursors.noMoveVert = new Cursor("nomovev.cur", 0);
				}
				return Cursors.noMoveVert;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally to the right.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally to the right.</returns>
		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x00043A05 File Offset: 0x00041C05
		public static Cursor PanEast
		{
			get
			{
				if (Cursors.panEast == null)
				{
					Cursors.panEast = new Cursor("east.cur", 0);
				}
				return Cursors.panEast;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically upward and to the right.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically upward and to the right.</returns>
		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x0600141F RID: 5151 RVA: 0x00043A29 File Offset: 0x00041C29
		public static Cursor PanNE
		{
			get
			{
				if (Cursors.panNE == null)
				{
					Cursors.panNE = new Cursor("ne.cur", 0);
				}
				return Cursors.panNE;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling vertically in an upward direction.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling vertically in an upward direction.</returns>
		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x00043A4D File Offset: 0x00041C4D
		public static Cursor PanNorth
		{
			get
			{
				if (Cursors.panNorth == null)
				{
					Cursors.panNorth = new Cursor("north.cur", 0);
				}
				return Cursors.panNorth;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically upward and to the left.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically upward and to the left.</returns>
		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001421 RID: 5153 RVA: 0x00043A71 File Offset: 0x00041C71
		public static Cursor PanNW
		{
			get
			{
				if (Cursors.panNW == null)
				{
					Cursors.panNW = new Cursor("nw.cur", 0);
				}
				return Cursors.panNW;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically downward and to the right.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically downward and to the right.</returns>
		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x00043A95 File Offset: 0x00041C95
		public static Cursor PanSE
		{
			get
			{
				if (Cursors.panSE == null)
				{
					Cursors.panSE = new Cursor("se.cur", 0);
				}
				return Cursors.panSE;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling vertically in a downward direction.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling vertically in a downward direction.</returns>
		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x00043AB9 File Offset: 0x00041CB9
		public static Cursor PanSouth
		{
			get
			{
				if (Cursors.panSouth == null)
				{
					Cursors.panSouth = new Cursor("south.cur", 0);
				}
				return Cursors.panSouth;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically downward and to the left.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically downward and to the left.</returns>
		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x00043ADD File Offset: 0x00041CDD
		public static Cursor PanSW
		{
			get
			{
				if (Cursors.panSW == null)
				{
					Cursors.panSW = new Cursor("sw.cur", 0);
				}
				return Cursors.panSW;
			}
		}

		/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally to the left.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally to the left.</returns>
		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x00043B01 File Offset: 0x00041D01
		public static Cursor PanWest
		{
			get
			{
				if (Cursors.panWest == null)
				{
					Cursors.panWest = new Cursor("west.cur", 0);
				}
				return Cursors.panWest;
			}
		}

		/// <summary>Gets the hand cursor, typically used when hovering over a Web link.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the hand cursor.</returns>
		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x00043B25 File Offset: 0x00041D25
		public static Cursor Hand
		{
			get
			{
				if (Cursors.hand == null)
				{
					Cursors.hand = new Cursor("hand.cur", 0);
				}
				return Cursors.hand;
			}
		}

		// Token: 0x0400094D RID: 2381
		private static Cursor appStarting;

		// Token: 0x0400094E RID: 2382
		private static Cursor arrow;

		// Token: 0x0400094F RID: 2383
		private static Cursor cross;

		// Token: 0x04000950 RID: 2384
		private static Cursor defaultCursor;

		// Token: 0x04000951 RID: 2385
		private static Cursor iBeam;

		// Token: 0x04000952 RID: 2386
		private static Cursor no;

		// Token: 0x04000953 RID: 2387
		private static Cursor sizeAll;

		// Token: 0x04000954 RID: 2388
		private static Cursor sizeNESW;

		// Token: 0x04000955 RID: 2389
		private static Cursor sizeNS;

		// Token: 0x04000956 RID: 2390
		private static Cursor sizeNWSE;

		// Token: 0x04000957 RID: 2391
		private static Cursor sizeWE;

		// Token: 0x04000958 RID: 2392
		private static Cursor upArrow;

		// Token: 0x04000959 RID: 2393
		private static Cursor wait;

		// Token: 0x0400095A RID: 2394
		private static Cursor help;

		// Token: 0x0400095B RID: 2395
		private static Cursor hSplit;

		// Token: 0x0400095C RID: 2396
		private static Cursor vSplit;

		// Token: 0x0400095D RID: 2397
		private static Cursor noMove2D;

		// Token: 0x0400095E RID: 2398
		private static Cursor noMoveHoriz;

		// Token: 0x0400095F RID: 2399
		private static Cursor noMoveVert;

		// Token: 0x04000960 RID: 2400
		private static Cursor panEast;

		// Token: 0x04000961 RID: 2401
		private static Cursor panNE;

		// Token: 0x04000962 RID: 2402
		private static Cursor panNorth;

		// Token: 0x04000963 RID: 2403
		private static Cursor panNW;

		// Token: 0x04000964 RID: 2404
		private static Cursor panSE;

		// Token: 0x04000965 RID: 2405
		private static Cursor panSouth;

		// Token: 0x04000966 RID: 2406
		private static Cursor panSW;

		// Token: 0x04000967 RID: 2407
		private static Cursor panWest;

		// Token: 0x04000968 RID: 2408
		private static Cursor hand;
	}
}
