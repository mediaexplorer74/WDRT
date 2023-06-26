using System;
using System.Diagnostics;

namespace System.ComponentModel
{
	// Token: 0x020000F6 RID: 246
	internal static class CompModSwitches
	{
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000BF13 File Offset: 0x0000A113
		public static TraceSwitch ActiveX
		{
			get
			{
				if (CompModSwitches.activeX == null)
				{
					CompModSwitches.activeX = new TraceSwitch("ActiveX", "Debug ActiveX sourcing");
				}
				return CompModSwitches.activeX;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000BF35 File Offset: 0x0000A135
		public static TraceSwitch DataCursor
		{
			get
			{
				if (CompModSwitches.dataCursor == null)
				{
					CompModSwitches.dataCursor = new TraceSwitch("Microsoft.WFC.Data.DataCursor", "DataCursor");
				}
				return CompModSwitches.dataCursor;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000BF57 File Offset: 0x0000A157
		public static TraceSwitch DataGridCursor
		{
			get
			{
				if (CompModSwitches.dataGridCursor == null)
				{
					CompModSwitches.dataGridCursor = new TraceSwitch("DataGridCursor", "DataGrid cursor tracing");
				}
				return CompModSwitches.dataGridCursor;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000BF79 File Offset: 0x0000A179
		public static TraceSwitch DataGridEditing
		{
			get
			{
				if (CompModSwitches.dataGridEditing == null)
				{
					CompModSwitches.dataGridEditing = new TraceSwitch("DataGridEditing", "DataGrid edit related tracing");
				}
				return CompModSwitches.dataGridEditing;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000BF9B File Offset: 0x0000A19B
		public static TraceSwitch DataGridKeys
		{
			get
			{
				if (CompModSwitches.dataGridKeys == null)
				{
					CompModSwitches.dataGridKeys = new TraceSwitch("DataGridKeys", "DataGrid keystroke management tracing");
				}
				return CompModSwitches.dataGridKeys;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000BFBD File Offset: 0x0000A1BD
		public static TraceSwitch DataGridLayout
		{
			get
			{
				if (CompModSwitches.dataGridLayout == null)
				{
					CompModSwitches.dataGridLayout = new TraceSwitch("DataGridLayout", "DataGrid layout tracing");
				}
				return CompModSwitches.dataGridLayout;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0000BFDF File Offset: 0x0000A1DF
		public static TraceSwitch DataGridPainting
		{
			get
			{
				if (CompModSwitches.dataGridPainting == null)
				{
					CompModSwitches.dataGridPainting = new TraceSwitch("DataGridPainting", "DataGrid Painting related tracing");
				}
				return CompModSwitches.dataGridPainting;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000C001 File Offset: 0x0000A201
		public static TraceSwitch DataGridParents
		{
			get
			{
				if (CompModSwitches.dataGridParents == null)
				{
					CompModSwitches.dataGridParents = new TraceSwitch("DataGridParents", "DataGrid parent rows");
				}
				return CompModSwitches.dataGridParents;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000C023 File Offset: 0x0000A223
		public static TraceSwitch DataGridScrolling
		{
			get
			{
				if (CompModSwitches.dataGridScrolling == null)
				{
					CompModSwitches.dataGridScrolling = new TraceSwitch("DataGridScrolling", "DataGrid scrolling");
				}
				return CompModSwitches.dataGridScrolling;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000C045 File Offset: 0x0000A245
		public static TraceSwitch DataGridSelection
		{
			get
			{
				if (CompModSwitches.dataGridSelection == null)
				{
					CompModSwitches.dataGridSelection = new TraceSwitch("DataGridSelection", "DataGrid selection management tracing");
				}
				return CompModSwitches.dataGridSelection;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0000C067 File Offset: 0x0000A267
		public static TraceSwitch DataObject
		{
			get
			{
				if (CompModSwitches.dataObject == null)
				{
					CompModSwitches.dataObject = new TraceSwitch("DataObject", "Enable tracing for the DataObject class.");
				}
				return CompModSwitches.dataObject;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000C089 File Offset: 0x0000A289
		public static TraceSwitch DataView
		{
			get
			{
				if (CompModSwitches.dataView == null)
				{
					CompModSwitches.dataView = new TraceSwitch("DataView", "DataView");
				}
				return CompModSwitches.dataView;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0000C0AB File Offset: 0x0000A2AB
		public static TraceSwitch DebugGridView
		{
			get
			{
				if (CompModSwitches.debugGridView == null)
				{
					CompModSwitches.debugGridView = new TraceSwitch("PSDEBUGGRIDVIEW", "Debug PropertyGridView");
				}
				return CompModSwitches.debugGridView;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000C0CD File Offset: 0x0000A2CD
		public static TraceSwitch DGCaptionPaint
		{
			get
			{
				if (CompModSwitches.dgCaptionPaint == null)
				{
					CompModSwitches.dgCaptionPaint = new TraceSwitch("DGCaptionPaint", "DataGridCaption");
				}
				return CompModSwitches.dgCaptionPaint;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0000C0EF File Offset: 0x0000A2EF
		public static TraceSwitch DGEditColumnEditing
		{
			get
			{
				if (CompModSwitches.dgEditColumnEditing == null)
				{
					CompModSwitches.dgEditColumnEditing = new TraceSwitch("DGEditColumnEditing", "Editing related tracing");
				}
				return CompModSwitches.dgEditColumnEditing;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0000C111 File Offset: 0x0000A311
		public static TraceSwitch DGRelationShpRowLayout
		{
			get
			{
				if (CompModSwitches.dgRelationShpRowLayout == null)
				{
					CompModSwitches.dgRelationShpRowLayout = new TraceSwitch("DGRelationShpRowLayout", "Relationship row layout");
				}
				return CompModSwitches.dgRelationShpRowLayout;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000C133 File Offset: 0x0000A333
		public static TraceSwitch DGRelationShpRowPaint
		{
			get
			{
				if (CompModSwitches.dgRelationShpRowPaint == null)
				{
					CompModSwitches.dgRelationShpRowPaint = new TraceSwitch("DGRelationShpRowPaint", "Relationship row painting");
				}
				return CompModSwitches.dgRelationShpRowPaint;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0000C155 File Offset: 0x0000A355
		public static TraceSwitch DGRowPaint
		{
			get
			{
				if (CompModSwitches.dgRowPaint == null)
				{
					CompModSwitches.dgRowPaint = new TraceSwitch("DGRowPaint", "DataGrid Simple Row painting stuff");
				}
				return CompModSwitches.dgRowPaint;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0000C177 File Offset: 0x0000A377
		public static TraceSwitch DragDrop
		{
			get
			{
				if (CompModSwitches.dragDrop == null)
				{
					CompModSwitches.dragDrop = new TraceSwitch("DragDrop", "Debug OLEDragDrop support in Controls");
				}
				return CompModSwitches.dragDrop;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0000C199 File Offset: 0x0000A399
		public static TraceSwitch FlowLayout
		{
			get
			{
				if (CompModSwitches.flowLayout == null)
				{
					CompModSwitches.flowLayout = new TraceSwitch("FlowLayout", "Debug flow layout");
				}
				return CompModSwitches.flowLayout;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000C1BB File Offset: 0x0000A3BB
		public static TraceSwitch ImeMode
		{
			get
			{
				if (CompModSwitches.imeMode == null)
				{
					CompModSwitches.imeMode = new TraceSwitch("ImeMode", "Debug IME Mode");
				}
				return CompModSwitches.imeMode;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0000C1DD File Offset: 0x0000A3DD
		public static TraceSwitch LayoutPerformance
		{
			get
			{
				if (CompModSwitches.layoutPerformance == null)
				{
					CompModSwitches.layoutPerformance = new TraceSwitch("LayoutPerformance", "Tracks layout events which impact performance.");
				}
				return CompModSwitches.layoutPerformance;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000C1FF File Offset: 0x0000A3FF
		public static TraceSwitch LayoutSuspendResume
		{
			get
			{
				if (CompModSwitches.layoutSuspendResume == null)
				{
					CompModSwitches.layoutSuspendResume = new TraceSwitch("LayoutSuspendResume", "Tracks SuspendLayout/ResumeLayout.");
				}
				return CompModSwitches.layoutSuspendResume;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0000C221 File Offset: 0x0000A421
		public static BooleanSwitch LifetimeTracing
		{
			get
			{
				if (CompModSwitches.lifetimeTracing == null)
				{
					CompModSwitches.lifetimeTracing = new BooleanSwitch("LifetimeTracing", "Track lifetime events. This will cause objects to track the stack at creation and dispose.");
				}
				return CompModSwitches.lifetimeTracing;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0000C243 File Offset: 0x0000A443
		public static TraceSwitch MSAA
		{
			get
			{
				if (CompModSwitches.msaa == null)
				{
					CompModSwitches.msaa = new TraceSwitch("MSAA", "Debug Microsoft Active Accessibility");
				}
				return CompModSwitches.msaa;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000C265 File Offset: 0x0000A465
		public static TraceSwitch MSOComponentManager
		{
			get
			{
				if (CompModSwitches.msoComponentManager == null)
				{
					CompModSwitches.msoComponentManager = new TraceSwitch("MSOComponentManager", "Debug MSO Component Manager support");
				}
				return CompModSwitches.msoComponentManager;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0000C287 File Offset: 0x0000A487
		public static TraceSwitch RichLayout
		{
			get
			{
				if (CompModSwitches.richLayout == null)
				{
					CompModSwitches.richLayout = new TraceSwitch("RichLayout", "Debug layout in RichControls");
				}
				return CompModSwitches.richLayout;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000C2A9 File Offset: 0x0000A4A9
		public static TraceSwitch SetBounds
		{
			get
			{
				if (CompModSwitches.setBounds == null)
				{
					CompModSwitches.setBounds = new TraceSwitch("SetBounds", "Trace changes to control size/position.");
				}
				return CompModSwitches.setBounds;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000C2CB File Offset: 0x0000A4CB
		public static TraceSwitch HandleLeak
		{
			get
			{
				if (CompModSwitches.handleLeak == null)
				{
					CompModSwitches.handleLeak = new TraceSwitch("HANDLELEAK", "HandleCollector: Track Win32 Handle Leaks");
				}
				return CompModSwitches.handleLeak;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000C2ED File Offset: 0x0000A4ED
		public static BooleanSwitch TraceCollect
		{
			get
			{
				if (CompModSwitches.traceCollect == null)
				{
					CompModSwitches.traceCollect = new BooleanSwitch("TRACECOLLECT", "HandleCollector: Trace HandleCollector operations");
				}
				return CompModSwitches.traceCollect;
			}
		}

		// Token: 0x04000406 RID: 1030
		private static TraceSwitch activeX;

		// Token: 0x04000407 RID: 1031
		private static TraceSwitch flowLayout;

		// Token: 0x04000408 RID: 1032
		private static TraceSwitch dataCursor;

		// Token: 0x04000409 RID: 1033
		private static TraceSwitch dataGridCursor;

		// Token: 0x0400040A RID: 1034
		private static TraceSwitch dataGridEditing;

		// Token: 0x0400040B RID: 1035
		private static TraceSwitch dataGridKeys;

		// Token: 0x0400040C RID: 1036
		private static TraceSwitch dataGridLayout;

		// Token: 0x0400040D RID: 1037
		private static TraceSwitch dataGridPainting;

		// Token: 0x0400040E RID: 1038
		private static TraceSwitch dataGridParents;

		// Token: 0x0400040F RID: 1039
		private static TraceSwitch dataGridScrolling;

		// Token: 0x04000410 RID: 1040
		private static TraceSwitch dataGridSelection;

		// Token: 0x04000411 RID: 1041
		private static TraceSwitch dataObject;

		// Token: 0x04000412 RID: 1042
		private static TraceSwitch dataView;

		// Token: 0x04000413 RID: 1043
		private static TraceSwitch debugGridView;

		// Token: 0x04000414 RID: 1044
		private static TraceSwitch dgCaptionPaint;

		// Token: 0x04000415 RID: 1045
		private static TraceSwitch dgEditColumnEditing;

		// Token: 0x04000416 RID: 1046
		private static TraceSwitch dgRelationShpRowLayout;

		// Token: 0x04000417 RID: 1047
		private static TraceSwitch dgRelationShpRowPaint;

		// Token: 0x04000418 RID: 1048
		private static TraceSwitch dgRowPaint;

		// Token: 0x04000419 RID: 1049
		private static TraceSwitch dragDrop;

		// Token: 0x0400041A RID: 1050
		private static TraceSwitch imeMode;

		// Token: 0x0400041B RID: 1051
		private static TraceSwitch msaa;

		// Token: 0x0400041C RID: 1052
		private static TraceSwitch msoComponentManager;

		// Token: 0x0400041D RID: 1053
		private static TraceSwitch layoutPerformance;

		// Token: 0x0400041E RID: 1054
		private static TraceSwitch layoutSuspendResume;

		// Token: 0x0400041F RID: 1055
		private static TraceSwitch richLayout;

		// Token: 0x04000420 RID: 1056
		private static TraceSwitch setBounds;

		// Token: 0x04000421 RID: 1057
		private static BooleanSwitch lifetimeTracing;

		// Token: 0x04000422 RID: 1058
		private static TraceSwitch handleLeak;

		// Token: 0x04000423 RID: 1059
		private static BooleanSwitch traceCollect;
	}
}
