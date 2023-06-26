using System;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Defines identifiers for the standard set of commands that are available to most applications.</summary>
	// Token: 0x020005FF RID: 1535
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class StandardCommands
	{
		// Token: 0x04002B0F RID: 11023
		private static readonly Guid standardCommandSet = StandardCommands.ShellGuids.VSStandardCommandSet97;

		// Token: 0x04002B10 RID: 11024
		private static readonly Guid ndpCommandSet = new Guid("{74D21313-2AEE-11d1-8BFB-00A0C90F26F7}");

		// Token: 0x04002B11 RID: 11025
		private const int cmdidDesignerVerbFirst = 8192;

		// Token: 0x04002B12 RID: 11026
		private const int cmdidDesignerVerbLast = 8448;

		// Token: 0x04002B13 RID: 11027
		private const int cmdidArrangeIcons = 12298;

		// Token: 0x04002B14 RID: 11028
		private const int cmdidLineupIcons = 12299;

		// Token: 0x04002B15 RID: 11029
		private const int cmdidShowLargeIcons = 12300;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignBottom command. This field is read-only.</summary>
		// Token: 0x04002B16 RID: 11030
		public static readonly CommandID AlignBottom = new CommandID(StandardCommands.standardCommandSet, 1);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignHorizontalCenters command. This field is read-only.</summary>
		// Token: 0x04002B17 RID: 11031
		public static readonly CommandID AlignHorizontalCenters = new CommandID(StandardCommands.standardCommandSet, 2);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignLeft command. This field is read-only.</summary>
		// Token: 0x04002B18 RID: 11032
		public static readonly CommandID AlignLeft = new CommandID(StandardCommands.standardCommandSet, 3);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignRight command. This field is read-only.</summary>
		// Token: 0x04002B19 RID: 11033
		public static readonly CommandID AlignRight = new CommandID(StandardCommands.standardCommandSet, 4);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignToGrid command. This field is read-only.</summary>
		// Token: 0x04002B1A RID: 11034
		public static readonly CommandID AlignToGrid = new CommandID(StandardCommands.standardCommandSet, 5);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignTop command. This field is read-only.</summary>
		// Token: 0x04002B1B RID: 11035
		public static readonly CommandID AlignTop = new CommandID(StandardCommands.standardCommandSet, 6);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignVerticalCenters command. This field is read-only.</summary>
		// Token: 0x04002B1C RID: 11036
		public static readonly CommandID AlignVerticalCenters = new CommandID(StandardCommands.standardCommandSet, 7);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ArrangeBottom command. This field is read-only.</summary>
		// Token: 0x04002B1D RID: 11037
		public static readonly CommandID ArrangeBottom = new CommandID(StandardCommands.standardCommandSet, 8);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ArrangeRight command. This field is read-only.</summary>
		// Token: 0x04002B1E RID: 11038
		public static readonly CommandID ArrangeRight = new CommandID(StandardCommands.standardCommandSet, 9);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the BringForward command. This field is read-only.</summary>
		// Token: 0x04002B1F RID: 11039
		public static readonly CommandID BringForward = new CommandID(StandardCommands.standardCommandSet, 10);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the BringToFront command. This field is read-only.</summary>
		// Token: 0x04002B20 RID: 11040
		public static readonly CommandID BringToFront = new CommandID(StandardCommands.standardCommandSet, 11);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the CenterHorizontally command. This field is read-only.</summary>
		// Token: 0x04002B21 RID: 11041
		public static readonly CommandID CenterHorizontally = new CommandID(StandardCommands.standardCommandSet, 12);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the CenterVertically command. This field is read-only.</summary>
		// Token: 0x04002B22 RID: 11042
		public static readonly CommandID CenterVertically = new CommandID(StandardCommands.standardCommandSet, 13);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ViewCode command. This field is read-only.</summary>
		// Token: 0x04002B23 RID: 11043
		public static readonly CommandID ViewCode = new CommandID(StandardCommands.standardCommandSet, 333);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Document Outline command. This field is read-only.</summary>
		// Token: 0x04002B24 RID: 11044
		public static readonly CommandID DocumentOutline = new CommandID(StandardCommands.standardCommandSet, 239);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Copy command. This field is read-only.</summary>
		// Token: 0x04002B25 RID: 11045
		public static readonly CommandID Copy = new CommandID(StandardCommands.standardCommandSet, 15);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Cut command. This field is read-only.</summary>
		// Token: 0x04002B26 RID: 11046
		public static readonly CommandID Cut = new CommandID(StandardCommands.standardCommandSet, 16);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Delete command. This field is read-only.</summary>
		// Token: 0x04002B27 RID: 11047
		public static readonly CommandID Delete = new CommandID(StandardCommands.standardCommandSet, 17);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Group command. This field is read-only.</summary>
		// Token: 0x04002B28 RID: 11048
		public static readonly CommandID Group = new CommandID(StandardCommands.standardCommandSet, 20);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the HorizSpaceConcatenate command. This field is read-only.</summary>
		// Token: 0x04002B29 RID: 11049
		public static readonly CommandID HorizSpaceConcatenate = new CommandID(StandardCommands.standardCommandSet, 21);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the HorizSpaceDecrease command. This field is read-only.</summary>
		// Token: 0x04002B2A RID: 11050
		public static readonly CommandID HorizSpaceDecrease = new CommandID(StandardCommands.standardCommandSet, 22);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the HorizSpaceIncrease command. This field is read-only.</summary>
		// Token: 0x04002B2B RID: 11051
		public static readonly CommandID HorizSpaceIncrease = new CommandID(StandardCommands.standardCommandSet, 23);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the HorizSpaceMakeEqual command. This field is read-only.</summary>
		// Token: 0x04002B2C RID: 11052
		public static readonly CommandID HorizSpaceMakeEqual = new CommandID(StandardCommands.standardCommandSet, 24);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Paste command. This field is read-only.</summary>
		// Token: 0x04002B2D RID: 11053
		public static readonly CommandID Paste = new CommandID(StandardCommands.standardCommandSet, 26);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Properties command. This field is read-only.</summary>
		// Token: 0x04002B2E RID: 11054
		public static readonly CommandID Properties = new CommandID(StandardCommands.standardCommandSet, 28);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Redo command. This field is read-only.</summary>
		// Token: 0x04002B2F RID: 11055
		public static readonly CommandID Redo = new CommandID(StandardCommands.standardCommandSet, 29);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the MultiLevelRedo command. This field is read-only.</summary>
		// Token: 0x04002B30 RID: 11056
		public static readonly CommandID MultiLevelRedo = new CommandID(StandardCommands.standardCommandSet, 30);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SelectAll command. This field is read-only.</summary>
		// Token: 0x04002B31 RID: 11057
		public static readonly CommandID SelectAll = new CommandID(StandardCommands.standardCommandSet, 31);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SendBackward command. This field is read-only.</summary>
		// Token: 0x04002B32 RID: 11058
		public static readonly CommandID SendBackward = new CommandID(StandardCommands.standardCommandSet, 32);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SendToBack command. This field is read-only.</summary>
		// Token: 0x04002B33 RID: 11059
		public static readonly CommandID SendToBack = new CommandID(StandardCommands.standardCommandSet, 33);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToControl command. This field is read-only.</summary>
		// Token: 0x04002B34 RID: 11060
		public static readonly CommandID SizeToControl = new CommandID(StandardCommands.standardCommandSet, 35);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToControlHeight command. This field is read-only.</summary>
		// Token: 0x04002B35 RID: 11061
		public static readonly CommandID SizeToControlHeight = new CommandID(StandardCommands.standardCommandSet, 36);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToControlWidth command. This field is read-only.</summary>
		// Token: 0x04002B36 RID: 11062
		public static readonly CommandID SizeToControlWidth = new CommandID(StandardCommands.standardCommandSet, 37);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToFit command. This field is read-only.</summary>
		// Token: 0x04002B37 RID: 11063
		public static readonly CommandID SizeToFit = new CommandID(StandardCommands.standardCommandSet, 38);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToGrid command. This field is read-only.</summary>
		// Token: 0x04002B38 RID: 11064
		public static readonly CommandID SizeToGrid = new CommandID(StandardCommands.standardCommandSet, 39);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SnapToGrid command. This field is read-only.</summary>
		// Token: 0x04002B39 RID: 11065
		public static readonly CommandID SnapToGrid = new CommandID(StandardCommands.standardCommandSet, 40);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the TabOrder command. This field is read-only.</summary>
		// Token: 0x04002B3A RID: 11066
		public static readonly CommandID TabOrder = new CommandID(StandardCommands.standardCommandSet, 41);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Undo command. This field is read-only.</summary>
		// Token: 0x04002B3B RID: 11067
		public static readonly CommandID Undo = new CommandID(StandardCommands.standardCommandSet, 43);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the MultiLevelUndo command. This field is read-only.</summary>
		// Token: 0x04002B3C RID: 11068
		public static readonly CommandID MultiLevelUndo = new CommandID(StandardCommands.standardCommandSet, 44);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Ungroup command. This field is read-only.</summary>
		// Token: 0x04002B3D RID: 11069
		public static readonly CommandID Ungroup = new CommandID(StandardCommands.standardCommandSet, 45);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the VertSpaceConcatenate command. This field is read-only.</summary>
		// Token: 0x04002B3E RID: 11070
		public static readonly CommandID VertSpaceConcatenate = new CommandID(StandardCommands.standardCommandSet, 46);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the VertSpaceDecrease command. This field is read-only.</summary>
		// Token: 0x04002B3F RID: 11071
		public static readonly CommandID VertSpaceDecrease = new CommandID(StandardCommands.standardCommandSet, 47);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the VertSpaceIncrease command. This field is read-only.</summary>
		// Token: 0x04002B40 RID: 11072
		public static readonly CommandID VertSpaceIncrease = new CommandID(StandardCommands.standardCommandSet, 48);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the VertSpaceMakeEqual command. This field is read-only.</summary>
		// Token: 0x04002B41 RID: 11073
		public static readonly CommandID VertSpaceMakeEqual = new CommandID(StandardCommands.standardCommandSet, 49);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ShowGrid command. This field is read-only.</summary>
		// Token: 0x04002B42 RID: 11074
		public static readonly CommandID ShowGrid = new CommandID(StandardCommands.standardCommandSet, 103);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ViewGrid command. This field is read-only.</summary>
		// Token: 0x04002B43 RID: 11075
		public static readonly CommandID ViewGrid = new CommandID(StandardCommands.standardCommandSet, 125);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Replace command. This field is read-only.</summary>
		// Token: 0x04002B44 RID: 11076
		public static readonly CommandID Replace = new CommandID(StandardCommands.standardCommandSet, 230);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the PropertiesWindow command. This field is read-only.</summary>
		// Token: 0x04002B45 RID: 11077
		public static readonly CommandID PropertiesWindow = new CommandID(StandardCommands.standardCommandSet, 235);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the LockControls command. This field is read-only.</summary>
		// Token: 0x04002B46 RID: 11078
		public static readonly CommandID LockControls = new CommandID(StandardCommands.standardCommandSet, 369);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the F1Help command. This field is read-only.</summary>
		// Token: 0x04002B47 RID: 11079
		public static readonly CommandID F1Help = new CommandID(StandardCommands.standardCommandSet, 377);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ArrangeIcons command. This field is read-only.</summary>
		// Token: 0x04002B48 RID: 11080
		public static readonly CommandID ArrangeIcons = new CommandID(StandardCommands.ndpCommandSet, 12298);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the LineupIcons command. This field is read-only.</summary>
		// Token: 0x04002B49 RID: 11081
		public static readonly CommandID LineupIcons = new CommandID(StandardCommands.ndpCommandSet, 12299);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ShowLargeIcons command. This field is read-only.</summary>
		// Token: 0x04002B4A RID: 11082
		public static readonly CommandID ShowLargeIcons = new CommandID(StandardCommands.ndpCommandSet, 12300);

		/// <summary>Gets the first of a set of verbs. This field is read-only.</summary>
		// Token: 0x04002B4B RID: 11083
		public static readonly CommandID VerbFirst = new CommandID(StandardCommands.ndpCommandSet, 8192);

		/// <summary>Gets the last of a set of verbs. This field is read-only.</summary>
		// Token: 0x04002B4C RID: 11084
		public static readonly CommandID VerbLast = new CommandID(StandardCommands.ndpCommandSet, 8448);

		// Token: 0x020008AD RID: 2221
		private static class VSStandardCommands
		{
			// Token: 0x040037E8 RID: 14312
			internal const int cmdidAlignBottom = 1;

			// Token: 0x040037E9 RID: 14313
			internal const int cmdidAlignHorizontalCenters = 2;

			// Token: 0x040037EA RID: 14314
			internal const int cmdidAlignLeft = 3;

			// Token: 0x040037EB RID: 14315
			internal const int cmdidAlignRight = 4;

			// Token: 0x040037EC RID: 14316
			internal const int cmdidAlignToGrid = 5;

			// Token: 0x040037ED RID: 14317
			internal const int cmdidAlignTop = 6;

			// Token: 0x040037EE RID: 14318
			internal const int cmdidAlignVerticalCenters = 7;

			// Token: 0x040037EF RID: 14319
			internal const int cmdidArrangeBottom = 8;

			// Token: 0x040037F0 RID: 14320
			internal const int cmdidArrangeRight = 9;

			// Token: 0x040037F1 RID: 14321
			internal const int cmdidBringForward = 10;

			// Token: 0x040037F2 RID: 14322
			internal const int cmdidBringToFront = 11;

			// Token: 0x040037F3 RID: 14323
			internal const int cmdidCenterHorizontally = 12;

			// Token: 0x040037F4 RID: 14324
			internal const int cmdidCenterVertically = 13;

			// Token: 0x040037F5 RID: 14325
			internal const int cmdidCode = 14;

			// Token: 0x040037F6 RID: 14326
			internal const int cmdidCopy = 15;

			// Token: 0x040037F7 RID: 14327
			internal const int cmdidCut = 16;

			// Token: 0x040037F8 RID: 14328
			internal const int cmdidDelete = 17;

			// Token: 0x040037F9 RID: 14329
			internal const int cmdidFontName = 18;

			// Token: 0x040037FA RID: 14330
			internal const int cmdidFontSize = 19;

			// Token: 0x040037FB RID: 14331
			internal const int cmdidGroup = 20;

			// Token: 0x040037FC RID: 14332
			internal const int cmdidHorizSpaceConcatenate = 21;

			// Token: 0x040037FD RID: 14333
			internal const int cmdidHorizSpaceDecrease = 22;

			// Token: 0x040037FE RID: 14334
			internal const int cmdidHorizSpaceIncrease = 23;

			// Token: 0x040037FF RID: 14335
			internal const int cmdidHorizSpaceMakeEqual = 24;

			// Token: 0x04003800 RID: 14336
			internal const int cmdidLockControls = 369;

			// Token: 0x04003801 RID: 14337
			internal const int cmdidInsertObject = 25;

			// Token: 0x04003802 RID: 14338
			internal const int cmdidPaste = 26;

			// Token: 0x04003803 RID: 14339
			internal const int cmdidPrint = 27;

			// Token: 0x04003804 RID: 14340
			internal const int cmdidProperties = 28;

			// Token: 0x04003805 RID: 14341
			internal const int cmdidRedo = 29;

			// Token: 0x04003806 RID: 14342
			internal const int cmdidMultiLevelRedo = 30;

			// Token: 0x04003807 RID: 14343
			internal const int cmdidSelectAll = 31;

			// Token: 0x04003808 RID: 14344
			internal const int cmdidSendBackward = 32;

			// Token: 0x04003809 RID: 14345
			internal const int cmdidSendToBack = 33;

			// Token: 0x0400380A RID: 14346
			internal const int cmdidShowTable = 34;

			// Token: 0x0400380B RID: 14347
			internal const int cmdidSizeToControl = 35;

			// Token: 0x0400380C RID: 14348
			internal const int cmdidSizeToControlHeight = 36;

			// Token: 0x0400380D RID: 14349
			internal const int cmdidSizeToControlWidth = 37;

			// Token: 0x0400380E RID: 14350
			internal const int cmdidSizeToFit = 38;

			// Token: 0x0400380F RID: 14351
			internal const int cmdidSizeToGrid = 39;

			// Token: 0x04003810 RID: 14352
			internal const int cmdidSnapToGrid = 40;

			// Token: 0x04003811 RID: 14353
			internal const int cmdidTabOrder = 41;

			// Token: 0x04003812 RID: 14354
			internal const int cmdidToolbox = 42;

			// Token: 0x04003813 RID: 14355
			internal const int cmdidUndo = 43;

			// Token: 0x04003814 RID: 14356
			internal const int cmdidMultiLevelUndo = 44;

			// Token: 0x04003815 RID: 14357
			internal const int cmdidUngroup = 45;

			// Token: 0x04003816 RID: 14358
			internal const int cmdidVertSpaceConcatenate = 46;

			// Token: 0x04003817 RID: 14359
			internal const int cmdidVertSpaceDecrease = 47;

			// Token: 0x04003818 RID: 14360
			internal const int cmdidVertSpaceIncrease = 48;

			// Token: 0x04003819 RID: 14361
			internal const int cmdidVertSpaceMakeEqual = 49;

			// Token: 0x0400381A RID: 14362
			internal const int cmdidZoomPercent = 50;

			// Token: 0x0400381B RID: 14363
			internal const int cmdidBackColor = 51;

			// Token: 0x0400381C RID: 14364
			internal const int cmdidBold = 52;

			// Token: 0x0400381D RID: 14365
			internal const int cmdidBorderColor = 53;

			// Token: 0x0400381E RID: 14366
			internal const int cmdidBorderDashDot = 54;

			// Token: 0x0400381F RID: 14367
			internal const int cmdidBorderDashDotDot = 55;

			// Token: 0x04003820 RID: 14368
			internal const int cmdidBorderDashes = 56;

			// Token: 0x04003821 RID: 14369
			internal const int cmdidBorderDots = 57;

			// Token: 0x04003822 RID: 14370
			internal const int cmdidBorderShortDashes = 58;

			// Token: 0x04003823 RID: 14371
			internal const int cmdidBorderSolid = 59;

			// Token: 0x04003824 RID: 14372
			internal const int cmdidBorderSparseDots = 60;

			// Token: 0x04003825 RID: 14373
			internal const int cmdidBorderWidth1 = 61;

			// Token: 0x04003826 RID: 14374
			internal const int cmdidBorderWidth2 = 62;

			// Token: 0x04003827 RID: 14375
			internal const int cmdidBorderWidth3 = 63;

			// Token: 0x04003828 RID: 14376
			internal const int cmdidBorderWidth4 = 64;

			// Token: 0x04003829 RID: 14377
			internal const int cmdidBorderWidth5 = 65;

			// Token: 0x0400382A RID: 14378
			internal const int cmdidBorderWidth6 = 66;

			// Token: 0x0400382B RID: 14379
			internal const int cmdidBorderWidthHairline = 67;

			// Token: 0x0400382C RID: 14380
			internal const int cmdidFlat = 68;

			// Token: 0x0400382D RID: 14381
			internal const int cmdidForeColor = 69;

			// Token: 0x0400382E RID: 14382
			internal const int cmdidItalic = 70;

			// Token: 0x0400382F RID: 14383
			internal const int cmdidJustifyCenter = 71;

			// Token: 0x04003830 RID: 14384
			internal const int cmdidJustifyGeneral = 72;

			// Token: 0x04003831 RID: 14385
			internal const int cmdidJustifyLeft = 73;

			// Token: 0x04003832 RID: 14386
			internal const int cmdidJustifyRight = 74;

			// Token: 0x04003833 RID: 14387
			internal const int cmdidRaised = 75;

			// Token: 0x04003834 RID: 14388
			internal const int cmdidSunken = 76;

			// Token: 0x04003835 RID: 14389
			internal const int cmdidUnderline = 77;

			// Token: 0x04003836 RID: 14390
			internal const int cmdidChiseled = 78;

			// Token: 0x04003837 RID: 14391
			internal const int cmdidEtched = 79;

			// Token: 0x04003838 RID: 14392
			internal const int cmdidShadowed = 80;

			// Token: 0x04003839 RID: 14393
			internal const int cmdidCompDebug1 = 81;

			// Token: 0x0400383A RID: 14394
			internal const int cmdidCompDebug2 = 82;

			// Token: 0x0400383B RID: 14395
			internal const int cmdidCompDebug3 = 83;

			// Token: 0x0400383C RID: 14396
			internal const int cmdidCompDebug4 = 84;

			// Token: 0x0400383D RID: 14397
			internal const int cmdidCompDebug5 = 85;

			// Token: 0x0400383E RID: 14398
			internal const int cmdidCompDebug6 = 86;

			// Token: 0x0400383F RID: 14399
			internal const int cmdidCompDebug7 = 87;

			// Token: 0x04003840 RID: 14400
			internal const int cmdidCompDebug8 = 88;

			// Token: 0x04003841 RID: 14401
			internal const int cmdidCompDebug9 = 89;

			// Token: 0x04003842 RID: 14402
			internal const int cmdidCompDebug10 = 90;

			// Token: 0x04003843 RID: 14403
			internal const int cmdidCompDebug11 = 91;

			// Token: 0x04003844 RID: 14404
			internal const int cmdidCompDebug12 = 92;

			// Token: 0x04003845 RID: 14405
			internal const int cmdidCompDebug13 = 93;

			// Token: 0x04003846 RID: 14406
			internal const int cmdidCompDebug14 = 94;

			// Token: 0x04003847 RID: 14407
			internal const int cmdidCompDebug15 = 95;

			// Token: 0x04003848 RID: 14408
			internal const int cmdidExistingSchemaEdit = 96;

			// Token: 0x04003849 RID: 14409
			internal const int cmdidFind = 97;

			// Token: 0x0400384A RID: 14410
			internal const int cmdidGetZoom = 98;

			// Token: 0x0400384B RID: 14411
			internal const int cmdidQueryOpenDesign = 99;

			// Token: 0x0400384C RID: 14412
			internal const int cmdidQueryOpenNew = 100;

			// Token: 0x0400384D RID: 14413
			internal const int cmdidSingleTableDesign = 101;

			// Token: 0x0400384E RID: 14414
			internal const int cmdidSingleTableNew = 102;

			// Token: 0x0400384F RID: 14415
			internal const int cmdidShowGrid = 103;

			// Token: 0x04003850 RID: 14416
			internal const int cmdidNewTable = 104;

			// Token: 0x04003851 RID: 14417
			internal const int cmdidCollapsedView = 105;

			// Token: 0x04003852 RID: 14418
			internal const int cmdidFieldView = 106;

			// Token: 0x04003853 RID: 14419
			internal const int cmdidVerifySQL = 107;

			// Token: 0x04003854 RID: 14420
			internal const int cmdidHideTable = 108;

			// Token: 0x04003855 RID: 14421
			internal const int cmdidPrimaryKey = 109;

			// Token: 0x04003856 RID: 14422
			internal const int cmdidSave = 110;

			// Token: 0x04003857 RID: 14423
			internal const int cmdidSaveAs = 111;

			// Token: 0x04003858 RID: 14424
			internal const int cmdidSortAscending = 112;

			// Token: 0x04003859 RID: 14425
			internal const int cmdidSortDescending = 113;

			// Token: 0x0400385A RID: 14426
			internal const int cmdidAppendQuery = 114;

			// Token: 0x0400385B RID: 14427
			internal const int cmdidCrosstabQuery = 115;

			// Token: 0x0400385C RID: 14428
			internal const int cmdidDeleteQuery = 116;

			// Token: 0x0400385D RID: 14429
			internal const int cmdidMakeTableQuery = 117;

			// Token: 0x0400385E RID: 14430
			internal const int cmdidSelectQuery = 118;

			// Token: 0x0400385F RID: 14431
			internal const int cmdidUpdateQuery = 119;

			// Token: 0x04003860 RID: 14432
			internal const int cmdidParameters = 120;

			// Token: 0x04003861 RID: 14433
			internal const int cmdidTotals = 121;

			// Token: 0x04003862 RID: 14434
			internal const int cmdidViewCollapsed = 122;

			// Token: 0x04003863 RID: 14435
			internal const int cmdidViewFieldList = 123;

			// Token: 0x04003864 RID: 14436
			internal const int cmdidViewKeys = 124;

			// Token: 0x04003865 RID: 14437
			internal const int cmdidViewGrid = 125;

			// Token: 0x04003866 RID: 14438
			internal const int cmdidInnerJoin = 126;

			// Token: 0x04003867 RID: 14439
			internal const int cmdidRightOuterJoin = 127;

			// Token: 0x04003868 RID: 14440
			internal const int cmdidLeftOuterJoin = 128;

			// Token: 0x04003869 RID: 14441
			internal const int cmdidFullOuterJoin = 129;

			// Token: 0x0400386A RID: 14442
			internal const int cmdidUnionJoin = 130;

			// Token: 0x0400386B RID: 14443
			internal const int cmdidShowSQLPane = 131;

			// Token: 0x0400386C RID: 14444
			internal const int cmdidShowGraphicalPane = 132;

			// Token: 0x0400386D RID: 14445
			internal const int cmdidShowDataPane = 133;

			// Token: 0x0400386E RID: 14446
			internal const int cmdidShowQBEPane = 134;

			// Token: 0x0400386F RID: 14447
			internal const int cmdidSelectAllFields = 135;

			// Token: 0x04003870 RID: 14448
			internal const int cmdidOLEObjectMenuButton = 136;

			// Token: 0x04003871 RID: 14449
			internal const int cmdidObjectVerbList0 = 137;

			// Token: 0x04003872 RID: 14450
			internal const int cmdidObjectVerbList1 = 138;

			// Token: 0x04003873 RID: 14451
			internal const int cmdidObjectVerbList2 = 139;

			// Token: 0x04003874 RID: 14452
			internal const int cmdidObjectVerbList3 = 140;

			// Token: 0x04003875 RID: 14453
			internal const int cmdidObjectVerbList4 = 141;

			// Token: 0x04003876 RID: 14454
			internal const int cmdidObjectVerbList5 = 142;

			// Token: 0x04003877 RID: 14455
			internal const int cmdidObjectVerbList6 = 143;

			// Token: 0x04003878 RID: 14456
			internal const int cmdidObjectVerbList7 = 144;

			// Token: 0x04003879 RID: 14457
			internal const int cmdidObjectVerbList8 = 145;

			// Token: 0x0400387A RID: 14458
			internal const int cmdidObjectVerbList9 = 146;

			// Token: 0x0400387B RID: 14459
			internal const int cmdidConvertObject = 147;

			// Token: 0x0400387C RID: 14460
			internal const int cmdidCustomControl = 148;

			// Token: 0x0400387D RID: 14461
			internal const int cmdidCustomizeItem = 149;

			// Token: 0x0400387E RID: 14462
			internal const int cmdidRename = 150;

			// Token: 0x0400387F RID: 14463
			internal const int cmdidImport = 151;

			// Token: 0x04003880 RID: 14464
			internal const int cmdidNewPage = 152;

			// Token: 0x04003881 RID: 14465
			internal const int cmdidMove = 153;

			// Token: 0x04003882 RID: 14466
			internal const int cmdidCancel = 154;

			// Token: 0x04003883 RID: 14467
			internal const int cmdidFont = 155;

			// Token: 0x04003884 RID: 14468
			internal const int cmdidExpandLinks = 156;

			// Token: 0x04003885 RID: 14469
			internal const int cmdidExpandImages = 157;

			// Token: 0x04003886 RID: 14470
			internal const int cmdidExpandPages = 158;

			// Token: 0x04003887 RID: 14471
			internal const int cmdidRefocusDiagram = 159;

			// Token: 0x04003888 RID: 14472
			internal const int cmdidTransitiveClosure = 160;

			// Token: 0x04003889 RID: 14473
			internal const int cmdidCenterDiagram = 161;

			// Token: 0x0400388A RID: 14474
			internal const int cmdidZoomIn = 162;

			// Token: 0x0400388B RID: 14475
			internal const int cmdidZoomOut = 163;

			// Token: 0x0400388C RID: 14476
			internal const int cmdidRemoveFilter = 164;

			// Token: 0x0400388D RID: 14477
			internal const int cmdidHidePane = 165;

			// Token: 0x0400388E RID: 14478
			internal const int cmdidDeleteTable = 166;

			// Token: 0x0400388F RID: 14479
			internal const int cmdidDeleteRelationship = 167;

			// Token: 0x04003890 RID: 14480
			internal const int cmdidRemove = 168;

			// Token: 0x04003891 RID: 14481
			internal const int cmdidJoinLeftAll = 169;

			// Token: 0x04003892 RID: 14482
			internal const int cmdidJoinRightAll = 170;

			// Token: 0x04003893 RID: 14483
			internal const int cmdidAddToOutput = 171;

			// Token: 0x04003894 RID: 14484
			internal const int cmdidOtherQuery = 172;

			// Token: 0x04003895 RID: 14485
			internal const int cmdidGenerateChangeScript = 173;

			// Token: 0x04003896 RID: 14486
			internal const int cmdidSaveSelection = 174;

			// Token: 0x04003897 RID: 14487
			internal const int cmdidAutojoinCurrent = 175;

			// Token: 0x04003898 RID: 14488
			internal const int cmdidAutojoinAlways = 176;

			// Token: 0x04003899 RID: 14489
			internal const int cmdidEditPage = 177;

			// Token: 0x0400389A RID: 14490
			internal const int cmdidViewLinks = 178;

			// Token: 0x0400389B RID: 14491
			internal const int cmdidStop = 179;

			// Token: 0x0400389C RID: 14492
			internal const int cmdidPause = 180;

			// Token: 0x0400389D RID: 14493
			internal const int cmdidResume = 181;

			// Token: 0x0400389E RID: 14494
			internal const int cmdidFilterDiagram = 182;

			// Token: 0x0400389F RID: 14495
			internal const int cmdidShowAllObjects = 183;

			// Token: 0x040038A0 RID: 14496
			internal const int cmdidShowApplications = 184;

			// Token: 0x040038A1 RID: 14497
			internal const int cmdidShowOtherObjects = 185;

			// Token: 0x040038A2 RID: 14498
			internal const int cmdidShowPrimRelationships = 186;

			// Token: 0x040038A3 RID: 14499
			internal const int cmdidExpand = 187;

			// Token: 0x040038A4 RID: 14500
			internal const int cmdidCollapse = 188;

			// Token: 0x040038A5 RID: 14501
			internal const int cmdidRefresh = 189;

			// Token: 0x040038A6 RID: 14502
			internal const int cmdidLayout = 190;

			// Token: 0x040038A7 RID: 14503
			internal const int cmdidShowResources = 191;

			// Token: 0x040038A8 RID: 14504
			internal const int cmdidInsertHTMLWizard = 192;

			// Token: 0x040038A9 RID: 14505
			internal const int cmdidShowDownloads = 193;

			// Token: 0x040038AA RID: 14506
			internal const int cmdidShowExternals = 194;

			// Token: 0x040038AB RID: 14507
			internal const int cmdidShowInBoundLinks = 195;

			// Token: 0x040038AC RID: 14508
			internal const int cmdidShowOutBoundLinks = 196;

			// Token: 0x040038AD RID: 14509
			internal const int cmdidShowInAndOutBoundLinks = 197;

			// Token: 0x040038AE RID: 14510
			internal const int cmdidPreview = 198;

			// Token: 0x040038AF RID: 14511
			internal const int cmdidOpen = 261;

			// Token: 0x040038B0 RID: 14512
			internal const int cmdidOpenWith = 199;

			// Token: 0x040038B1 RID: 14513
			internal const int cmdidShowPages = 200;

			// Token: 0x040038B2 RID: 14514
			internal const int cmdidRunQuery = 201;

			// Token: 0x040038B3 RID: 14515
			internal const int cmdidClearQuery = 202;

			// Token: 0x040038B4 RID: 14516
			internal const int cmdidRecordFirst = 203;

			// Token: 0x040038B5 RID: 14517
			internal const int cmdidRecordLast = 204;

			// Token: 0x040038B6 RID: 14518
			internal const int cmdidRecordNext = 205;

			// Token: 0x040038B7 RID: 14519
			internal const int cmdidRecordPrevious = 206;

			// Token: 0x040038B8 RID: 14520
			internal const int cmdidRecordGoto = 207;

			// Token: 0x040038B9 RID: 14521
			internal const int cmdidRecordNew = 208;

			// Token: 0x040038BA RID: 14522
			internal const int cmdidInsertNewMenu = 209;

			// Token: 0x040038BB RID: 14523
			internal const int cmdidInsertSeparator = 210;

			// Token: 0x040038BC RID: 14524
			internal const int cmdidEditMenuNames = 211;

			// Token: 0x040038BD RID: 14525
			internal const int cmdidDebugExplorer = 212;

			// Token: 0x040038BE RID: 14526
			internal const int cmdidDebugProcesses = 213;

			// Token: 0x040038BF RID: 14527
			internal const int cmdidViewThreadsWindow = 214;

			// Token: 0x040038C0 RID: 14528
			internal const int cmdidWindowUIList = 215;

			// Token: 0x040038C1 RID: 14529
			internal const int cmdidNewProject = 216;

			// Token: 0x040038C2 RID: 14530
			internal const int cmdidOpenProject = 217;

			// Token: 0x040038C3 RID: 14531
			internal const int cmdidOpenSolution = 218;

			// Token: 0x040038C4 RID: 14532
			internal const int cmdidCloseSolution = 219;

			// Token: 0x040038C5 RID: 14533
			internal const int cmdidFileNew = 221;

			// Token: 0x040038C6 RID: 14534
			internal const int cmdidFileOpen = 222;

			// Token: 0x040038C7 RID: 14535
			internal const int cmdidFileClose = 223;

			// Token: 0x040038C8 RID: 14536
			internal const int cmdidSaveSolution = 224;

			// Token: 0x040038C9 RID: 14537
			internal const int cmdidSaveSolutionAs = 225;

			// Token: 0x040038CA RID: 14538
			internal const int cmdidSaveProjectItemAs = 226;

			// Token: 0x040038CB RID: 14539
			internal const int cmdidPageSetup = 227;

			// Token: 0x040038CC RID: 14540
			internal const int cmdidPrintPreview = 228;

			// Token: 0x040038CD RID: 14541
			internal const int cmdidExit = 229;

			// Token: 0x040038CE RID: 14542
			internal const int cmdidReplace = 230;

			// Token: 0x040038CF RID: 14543
			internal const int cmdidGoto = 231;

			// Token: 0x040038D0 RID: 14544
			internal const int cmdidPropertyPages = 232;

			// Token: 0x040038D1 RID: 14545
			internal const int cmdidFullScreen = 233;

			// Token: 0x040038D2 RID: 14546
			internal const int cmdidProjectExplorer = 234;

			// Token: 0x040038D3 RID: 14547
			internal const int cmdidPropertiesWindow = 235;

			// Token: 0x040038D4 RID: 14548
			internal const int cmdidTaskListWindow = 236;

			// Token: 0x040038D5 RID: 14549
			internal const int cmdidOutputWindow = 237;

			// Token: 0x040038D6 RID: 14550
			internal const int cmdidObjectBrowser = 238;

			// Token: 0x040038D7 RID: 14551
			internal const int cmdidDocOutlineWindow = 239;

			// Token: 0x040038D8 RID: 14552
			internal const int cmdidImmediateWindow = 240;

			// Token: 0x040038D9 RID: 14553
			internal const int cmdidWatchWindow = 241;

			// Token: 0x040038DA RID: 14554
			internal const int cmdidLocalsWindow = 242;

			// Token: 0x040038DB RID: 14555
			internal const int cmdidCallStack = 243;

			// Token: 0x040038DC RID: 14556
			internal const int cmdidAutosWindow = 747;

			// Token: 0x040038DD RID: 14557
			internal const int cmdidThisWindow = 748;

			// Token: 0x040038DE RID: 14558
			internal const int cmdidAddNewItem = 220;

			// Token: 0x040038DF RID: 14559
			internal const int cmdidAddExistingItem = 244;

			// Token: 0x040038E0 RID: 14560
			internal const int cmdidNewFolder = 245;

			// Token: 0x040038E1 RID: 14561
			internal const int cmdidSetStartupProject = 246;

			// Token: 0x040038E2 RID: 14562
			internal const int cmdidProjectSettings = 247;

			// Token: 0x040038E3 RID: 14563
			internal const int cmdidProjectReferences = 367;

			// Token: 0x040038E4 RID: 14564
			internal const int cmdidStepInto = 248;

			// Token: 0x040038E5 RID: 14565
			internal const int cmdidStepOver = 249;

			// Token: 0x040038E6 RID: 14566
			internal const int cmdidStepOut = 250;

			// Token: 0x040038E7 RID: 14567
			internal const int cmdidRunToCursor = 251;

			// Token: 0x040038E8 RID: 14568
			internal const int cmdidAddWatch = 252;

			// Token: 0x040038E9 RID: 14569
			internal const int cmdidEditWatch = 253;

			// Token: 0x040038EA RID: 14570
			internal const int cmdidQuickWatch = 254;

			// Token: 0x040038EB RID: 14571
			internal const int cmdidToggleBreakpoint = 255;

			// Token: 0x040038EC RID: 14572
			internal const int cmdidClearBreakpoints = 256;

			// Token: 0x040038ED RID: 14573
			internal const int cmdidShowBreakpoints = 257;

			// Token: 0x040038EE RID: 14574
			internal const int cmdidSetNextStatement = 258;

			// Token: 0x040038EF RID: 14575
			internal const int cmdidShowNextStatement = 259;

			// Token: 0x040038F0 RID: 14576
			internal const int cmdidEditBreakpoint = 260;

			// Token: 0x040038F1 RID: 14577
			internal const int cmdidDetachDebugger = 262;

			// Token: 0x040038F2 RID: 14578
			internal const int cmdidCustomizeKeyboard = 263;

			// Token: 0x040038F3 RID: 14579
			internal const int cmdidToolsOptions = 264;

			// Token: 0x040038F4 RID: 14580
			internal const int cmdidNewWindow = 265;

			// Token: 0x040038F5 RID: 14581
			internal const int cmdidSplit = 266;

			// Token: 0x040038F6 RID: 14582
			internal const int cmdidCascade = 267;

			// Token: 0x040038F7 RID: 14583
			internal const int cmdidTileHorz = 268;

			// Token: 0x040038F8 RID: 14584
			internal const int cmdidTileVert = 269;

			// Token: 0x040038F9 RID: 14585
			internal const int cmdidTechSupport = 270;

			// Token: 0x040038FA RID: 14586
			internal const int cmdidAbout = 271;

			// Token: 0x040038FB RID: 14587
			internal const int cmdidDebugOptions = 272;

			// Token: 0x040038FC RID: 14588
			internal const int cmdidDeleteWatch = 274;

			// Token: 0x040038FD RID: 14589
			internal const int cmdidCollapseWatch = 275;

			// Token: 0x040038FE RID: 14590
			internal const int cmdidPbrsToggleStatus = 282;

			// Token: 0x040038FF RID: 14591
			internal const int cmdidPropbrsHide = 283;

			// Token: 0x04003900 RID: 14592
			internal const int cmdidDockingView = 284;

			// Token: 0x04003901 RID: 14593
			internal const int cmdidHideActivePane = 285;

			// Token: 0x04003902 RID: 14594
			internal const int cmdidPaneNextTab = 286;

			// Token: 0x04003903 RID: 14595
			internal const int cmdidPanePrevTab = 287;

			// Token: 0x04003904 RID: 14596
			internal const int cmdidPaneCloseToolWindow = 288;

			// Token: 0x04003905 RID: 14597
			internal const int cmdidPaneActivateDocWindow = 289;

			// Token: 0x04003906 RID: 14598
			internal const int cmdidDockingViewFloater = 291;

			// Token: 0x04003907 RID: 14599
			internal const int cmdidAutoHideWindow = 292;

			// Token: 0x04003908 RID: 14600
			internal const int cmdidMoveToDropdownBar = 293;

			// Token: 0x04003909 RID: 14601
			internal const int cmdidFindCmd = 294;

			// Token: 0x0400390A RID: 14602
			internal const int cmdidStart = 295;

			// Token: 0x0400390B RID: 14603
			internal const int cmdidRestart = 296;

			// Token: 0x0400390C RID: 14604
			internal const int cmdidAddinManager = 297;

			// Token: 0x0400390D RID: 14605
			internal const int cmdidMultiLevelUndoList = 298;

			// Token: 0x0400390E RID: 14606
			internal const int cmdidMultiLevelRedoList = 299;

			// Token: 0x0400390F RID: 14607
			internal const int cmdidToolboxAddTab = 300;

			// Token: 0x04003910 RID: 14608
			internal const int cmdidToolboxDeleteTab = 301;

			// Token: 0x04003911 RID: 14609
			internal const int cmdidToolboxRenameTab = 302;

			// Token: 0x04003912 RID: 14610
			internal const int cmdidToolboxTabMoveUp = 303;

			// Token: 0x04003913 RID: 14611
			internal const int cmdidToolboxTabMoveDown = 304;

			// Token: 0x04003914 RID: 14612
			internal const int cmdidToolboxRenameItem = 305;

			// Token: 0x04003915 RID: 14613
			internal const int cmdidToolboxListView = 306;

			// Token: 0x04003916 RID: 14614
			internal const int cmdidWindowUIGetList = 308;

			// Token: 0x04003917 RID: 14615
			internal const int cmdidInsertValuesQuery = 309;

			// Token: 0x04003918 RID: 14616
			internal const int cmdidShowProperties = 310;

			// Token: 0x04003919 RID: 14617
			internal const int cmdidThreadSuspend = 311;

			// Token: 0x0400391A RID: 14618
			internal const int cmdidThreadResume = 312;

			// Token: 0x0400391B RID: 14619
			internal const int cmdidThreadSetFocus = 313;

			// Token: 0x0400391C RID: 14620
			internal const int cmdidDisplayRadix = 314;

			// Token: 0x0400391D RID: 14621
			internal const int cmdidOpenProjectItem = 315;

			// Token: 0x0400391E RID: 14622
			internal const int cmdidPaneNextPane = 316;

			// Token: 0x0400391F RID: 14623
			internal const int cmdidPanePrevPane = 317;

			// Token: 0x04003920 RID: 14624
			internal const int cmdidClearPane = 318;

			// Token: 0x04003921 RID: 14625
			internal const int cmdidGotoErrorTag = 319;

			// Token: 0x04003922 RID: 14626
			internal const int cmdidTaskListSortByCategory = 320;

			// Token: 0x04003923 RID: 14627
			internal const int cmdidTaskListSortByFileLine = 321;

			// Token: 0x04003924 RID: 14628
			internal const int cmdidTaskListSortByPriority = 322;

			// Token: 0x04003925 RID: 14629
			internal const int cmdidTaskListSortByDefaultSort = 323;

			// Token: 0x04003926 RID: 14630
			internal const int cmdidTaskListFilterByNothing = 325;

			// Token: 0x04003927 RID: 14631
			internal const int cmdidTaskListFilterByCategoryCodeSense = 326;

			// Token: 0x04003928 RID: 14632
			internal const int cmdidTaskListFilterByCategoryCompiler = 327;

			// Token: 0x04003929 RID: 14633
			internal const int cmdidTaskListFilterByCategoryComment = 328;

			// Token: 0x0400392A RID: 14634
			internal const int cmdidToolboxAddItem = 329;

			// Token: 0x0400392B RID: 14635
			internal const int cmdidToolboxReset = 330;

			// Token: 0x0400392C RID: 14636
			internal const int cmdidSaveProjectItem = 331;

			// Token: 0x0400392D RID: 14637
			internal const int cmdidViewForm = 332;

			// Token: 0x0400392E RID: 14638
			internal const int cmdidViewCode = 333;

			// Token: 0x0400392F RID: 14639
			internal const int cmdidPreviewInBrowser = 334;

			// Token: 0x04003930 RID: 14640
			internal const int cmdidBrowseWith = 336;

			// Token: 0x04003931 RID: 14641
			internal const int cmdidSearchSetCombo = 307;

			// Token: 0x04003932 RID: 14642
			internal const int cmdidSearchCombo = 337;

			// Token: 0x04003933 RID: 14643
			internal const int cmdidEditLabel = 338;

			// Token: 0x04003934 RID: 14644
			internal const int cmdidExceptions = 339;

			// Token: 0x04003935 RID: 14645
			internal const int cmdidToggleSelMode = 341;

			// Token: 0x04003936 RID: 14646
			internal const int cmdidToggleInsMode = 342;

			// Token: 0x04003937 RID: 14647
			internal const int cmdidLoadUnloadedProject = 343;

			// Token: 0x04003938 RID: 14648
			internal const int cmdidUnloadLoadedProject = 344;

			// Token: 0x04003939 RID: 14649
			internal const int cmdidElasticColumn = 345;

			// Token: 0x0400393A RID: 14650
			internal const int cmdidHideColumn = 346;

			// Token: 0x0400393B RID: 14651
			internal const int cmdidTaskListPreviousView = 347;

			// Token: 0x0400393C RID: 14652
			internal const int cmdidZoomDialog = 348;

			// Token: 0x0400393D RID: 14653
			internal const int cmdidFindNew = 349;

			// Token: 0x0400393E RID: 14654
			internal const int cmdidFindMatchCase = 350;

			// Token: 0x0400393F RID: 14655
			internal const int cmdidFindWholeWord = 351;

			// Token: 0x04003940 RID: 14656
			internal const int cmdidFindSimplePattern = 276;

			// Token: 0x04003941 RID: 14657
			internal const int cmdidFindRegularExpression = 352;

			// Token: 0x04003942 RID: 14658
			internal const int cmdidFindBackwards = 353;

			// Token: 0x04003943 RID: 14659
			internal const int cmdidFindInSelection = 354;

			// Token: 0x04003944 RID: 14660
			internal const int cmdidFindStop = 355;

			// Token: 0x04003945 RID: 14661
			internal const int cmdidFindHelp = 356;

			// Token: 0x04003946 RID: 14662
			internal const int cmdidFindInFiles = 277;

			// Token: 0x04003947 RID: 14663
			internal const int cmdidReplaceInFiles = 278;

			// Token: 0x04003948 RID: 14664
			internal const int cmdidNextLocation = 279;

			// Token: 0x04003949 RID: 14665
			internal const int cmdidPreviousLocation = 280;

			// Token: 0x0400394A RID: 14666
			internal const int cmdidTaskListNextError = 357;

			// Token: 0x0400394B RID: 14667
			internal const int cmdidTaskListPrevError = 358;

			// Token: 0x0400394C RID: 14668
			internal const int cmdidTaskListFilterByCategoryUser = 359;

			// Token: 0x0400394D RID: 14669
			internal const int cmdidTaskListFilterByCategoryShortcut = 360;

			// Token: 0x0400394E RID: 14670
			internal const int cmdidTaskListFilterByCategoryHTML = 361;

			// Token: 0x0400394F RID: 14671
			internal const int cmdidTaskListFilterByCurrentFile = 362;

			// Token: 0x04003950 RID: 14672
			internal const int cmdidTaskListFilterByChecked = 363;

			// Token: 0x04003951 RID: 14673
			internal const int cmdidTaskListFilterByUnchecked = 364;

			// Token: 0x04003952 RID: 14674
			internal const int cmdidTaskListSortByDescription = 365;

			// Token: 0x04003953 RID: 14675
			internal const int cmdidTaskListSortByChecked = 366;

			// Token: 0x04003954 RID: 14676
			internal const int cmdidStartNoDebug = 368;

			// Token: 0x04003955 RID: 14677
			internal const int cmdidFindNext = 370;

			// Token: 0x04003956 RID: 14678
			internal const int cmdidFindPrev = 371;

			// Token: 0x04003957 RID: 14679
			internal const int cmdidFindSelectedNext = 372;

			// Token: 0x04003958 RID: 14680
			internal const int cmdidFindSelectedPrev = 373;

			// Token: 0x04003959 RID: 14681
			internal const int cmdidSearchGetList = 374;

			// Token: 0x0400395A RID: 14682
			internal const int cmdidInsertBreakpoint = 375;

			// Token: 0x0400395B RID: 14683
			internal const int cmdidEnableBreakpoint = 376;

			// Token: 0x0400395C RID: 14684
			internal const int cmdidF1Help = 377;

			// Token: 0x0400395D RID: 14685
			internal const int cmdidPropSheetOrProperties = 397;

			// Token: 0x0400395E RID: 14686
			internal const int cmdidTshellStep = 398;

			// Token: 0x0400395F RID: 14687
			internal const int cmdidTshellRun = 399;

			// Token: 0x04003960 RID: 14688
			internal const int cmdidMarkerCmd0 = 400;

			// Token: 0x04003961 RID: 14689
			internal const int cmdidMarkerCmd1 = 401;

			// Token: 0x04003962 RID: 14690
			internal const int cmdidMarkerCmd2 = 402;

			// Token: 0x04003963 RID: 14691
			internal const int cmdidMarkerCmd3 = 403;

			// Token: 0x04003964 RID: 14692
			internal const int cmdidMarkerCmd4 = 404;

			// Token: 0x04003965 RID: 14693
			internal const int cmdidMarkerCmd5 = 405;

			// Token: 0x04003966 RID: 14694
			internal const int cmdidMarkerCmd6 = 406;

			// Token: 0x04003967 RID: 14695
			internal const int cmdidMarkerCmd7 = 407;

			// Token: 0x04003968 RID: 14696
			internal const int cmdidMarkerCmd8 = 408;

			// Token: 0x04003969 RID: 14697
			internal const int cmdidMarkerCmd9 = 409;

			// Token: 0x0400396A RID: 14698
			internal const int cmdidMarkerLast = 409;

			// Token: 0x0400396B RID: 14699
			internal const int cmdidMarkerEnd = 410;

			// Token: 0x0400396C RID: 14700
			internal const int cmdidReloadProject = 412;

			// Token: 0x0400396D RID: 14701
			internal const int cmdidUnloadProject = 413;

			// Token: 0x0400396E RID: 14702
			internal const int cmdidDetachAttachOutline = 420;

			// Token: 0x0400396F RID: 14703
			internal const int cmdidShowHideOutline = 421;

			// Token: 0x04003970 RID: 14704
			internal const int cmdidSyncOutline = 422;

			// Token: 0x04003971 RID: 14705
			internal const int cmdidRunToCallstCursor = 423;

			// Token: 0x04003972 RID: 14706
			internal const int cmdidNoCmdsAvailable = 424;

			// Token: 0x04003973 RID: 14707
			internal const int cmdidContextWindow = 427;

			// Token: 0x04003974 RID: 14708
			internal const int cmdidAlias = 428;

			// Token: 0x04003975 RID: 14709
			internal const int cmdidGotoCommandLine = 429;

			// Token: 0x04003976 RID: 14710
			internal const int cmdidEvaluateExpression = 430;

			// Token: 0x04003977 RID: 14711
			internal const int cmdidImmediateMode = 431;

			// Token: 0x04003978 RID: 14712
			internal const int cmdidEvaluateStatement = 432;

			// Token: 0x04003979 RID: 14713
			internal const int cmdidFindResultWindow1 = 433;

			// Token: 0x0400397A RID: 14714
			internal const int cmdidFindResultWindow2 = 434;

			// Token: 0x0400397B RID: 14715
			internal const int cmdidWindow1 = 570;

			// Token: 0x0400397C RID: 14716
			internal const int cmdidWindow2 = 571;

			// Token: 0x0400397D RID: 14717
			internal const int cmdidWindow3 = 572;

			// Token: 0x0400397E RID: 14718
			internal const int cmdidWindow4 = 573;

			// Token: 0x0400397F RID: 14719
			internal const int cmdidWindow5 = 574;

			// Token: 0x04003980 RID: 14720
			internal const int cmdidWindow6 = 575;

			// Token: 0x04003981 RID: 14721
			internal const int cmdidWindow7 = 576;

			// Token: 0x04003982 RID: 14722
			internal const int cmdidWindow8 = 577;

			// Token: 0x04003983 RID: 14723
			internal const int cmdidWindow9 = 578;

			// Token: 0x04003984 RID: 14724
			internal const int cmdidWindow10 = 579;

			// Token: 0x04003985 RID: 14725
			internal const int cmdidWindow11 = 580;

			// Token: 0x04003986 RID: 14726
			internal const int cmdidWindow12 = 581;

			// Token: 0x04003987 RID: 14727
			internal const int cmdidWindow13 = 582;

			// Token: 0x04003988 RID: 14728
			internal const int cmdidWindow14 = 583;

			// Token: 0x04003989 RID: 14729
			internal const int cmdidWindow15 = 584;

			// Token: 0x0400398A RID: 14730
			internal const int cmdidWindow16 = 585;

			// Token: 0x0400398B RID: 14731
			internal const int cmdidWindow17 = 586;

			// Token: 0x0400398C RID: 14732
			internal const int cmdidWindow18 = 587;

			// Token: 0x0400398D RID: 14733
			internal const int cmdidWindow19 = 588;

			// Token: 0x0400398E RID: 14734
			internal const int cmdidWindow20 = 589;

			// Token: 0x0400398F RID: 14735
			internal const int cmdidWindow21 = 590;

			// Token: 0x04003990 RID: 14736
			internal const int cmdidWindow22 = 591;

			// Token: 0x04003991 RID: 14737
			internal const int cmdidWindow23 = 592;

			// Token: 0x04003992 RID: 14738
			internal const int cmdidWindow24 = 593;

			// Token: 0x04003993 RID: 14739
			internal const int cmdidWindow25 = 594;

			// Token: 0x04003994 RID: 14740
			internal const int cmdidMoreWindows = 595;

			// Token: 0x04003995 RID: 14741
			internal const int cmdidTaskListTaskHelp = 598;

			// Token: 0x04003996 RID: 14742
			internal const int cmdidClassView = 599;

			// Token: 0x04003997 RID: 14743
			internal const int cmdidMRUProj1 = 600;

			// Token: 0x04003998 RID: 14744
			internal const int cmdidMRUProj2 = 601;

			// Token: 0x04003999 RID: 14745
			internal const int cmdidMRUProj3 = 602;

			// Token: 0x0400399A RID: 14746
			internal const int cmdidMRUProj4 = 603;

			// Token: 0x0400399B RID: 14747
			internal const int cmdidMRUProj5 = 604;

			// Token: 0x0400399C RID: 14748
			internal const int cmdidMRUProj6 = 605;

			// Token: 0x0400399D RID: 14749
			internal const int cmdidMRUProj7 = 606;

			// Token: 0x0400399E RID: 14750
			internal const int cmdidMRUProj8 = 607;

			// Token: 0x0400399F RID: 14751
			internal const int cmdidMRUProj9 = 608;

			// Token: 0x040039A0 RID: 14752
			internal const int cmdidMRUProj10 = 609;

			// Token: 0x040039A1 RID: 14753
			internal const int cmdidMRUProj11 = 610;

			// Token: 0x040039A2 RID: 14754
			internal const int cmdidMRUProj12 = 611;

			// Token: 0x040039A3 RID: 14755
			internal const int cmdidMRUProj13 = 612;

			// Token: 0x040039A4 RID: 14756
			internal const int cmdidMRUProj14 = 613;

			// Token: 0x040039A5 RID: 14757
			internal const int cmdidMRUProj15 = 614;

			// Token: 0x040039A6 RID: 14758
			internal const int cmdidMRUProj16 = 615;

			// Token: 0x040039A7 RID: 14759
			internal const int cmdidMRUProj17 = 616;

			// Token: 0x040039A8 RID: 14760
			internal const int cmdidMRUProj18 = 617;

			// Token: 0x040039A9 RID: 14761
			internal const int cmdidMRUProj19 = 618;

			// Token: 0x040039AA RID: 14762
			internal const int cmdidMRUProj20 = 619;

			// Token: 0x040039AB RID: 14763
			internal const int cmdidMRUProj21 = 620;

			// Token: 0x040039AC RID: 14764
			internal const int cmdidMRUProj22 = 621;

			// Token: 0x040039AD RID: 14765
			internal const int cmdidMRUProj23 = 622;

			// Token: 0x040039AE RID: 14766
			internal const int cmdidMRUProj24 = 623;

			// Token: 0x040039AF RID: 14767
			internal const int cmdidMRUProj25 = 624;

			// Token: 0x040039B0 RID: 14768
			internal const int cmdidSplitNext = 625;

			// Token: 0x040039B1 RID: 14769
			internal const int cmdidSplitPrev = 626;

			// Token: 0x040039B2 RID: 14770
			internal const int cmdidCloseAllDocuments = 627;

			// Token: 0x040039B3 RID: 14771
			internal const int cmdidNextDocument = 628;

			// Token: 0x040039B4 RID: 14772
			internal const int cmdidPrevDocument = 629;

			// Token: 0x040039B5 RID: 14773
			internal const int cmdidTool1 = 630;

			// Token: 0x040039B6 RID: 14774
			internal const int cmdidTool2 = 631;

			// Token: 0x040039B7 RID: 14775
			internal const int cmdidTool3 = 632;

			// Token: 0x040039B8 RID: 14776
			internal const int cmdidTool4 = 633;

			// Token: 0x040039B9 RID: 14777
			internal const int cmdidTool5 = 634;

			// Token: 0x040039BA RID: 14778
			internal const int cmdidTool6 = 635;

			// Token: 0x040039BB RID: 14779
			internal const int cmdidTool7 = 636;

			// Token: 0x040039BC RID: 14780
			internal const int cmdidTool8 = 637;

			// Token: 0x040039BD RID: 14781
			internal const int cmdidTool9 = 638;

			// Token: 0x040039BE RID: 14782
			internal const int cmdidTool10 = 639;

			// Token: 0x040039BF RID: 14783
			internal const int cmdidTool11 = 640;

			// Token: 0x040039C0 RID: 14784
			internal const int cmdidTool12 = 641;

			// Token: 0x040039C1 RID: 14785
			internal const int cmdidTool13 = 642;

			// Token: 0x040039C2 RID: 14786
			internal const int cmdidTool14 = 643;

			// Token: 0x040039C3 RID: 14787
			internal const int cmdidTool15 = 644;

			// Token: 0x040039C4 RID: 14788
			internal const int cmdidTool16 = 645;

			// Token: 0x040039C5 RID: 14789
			internal const int cmdidTool17 = 646;

			// Token: 0x040039C6 RID: 14790
			internal const int cmdidTool18 = 647;

			// Token: 0x040039C7 RID: 14791
			internal const int cmdidTool19 = 648;

			// Token: 0x040039C8 RID: 14792
			internal const int cmdidTool20 = 649;

			// Token: 0x040039C9 RID: 14793
			internal const int cmdidTool21 = 650;

			// Token: 0x040039CA RID: 14794
			internal const int cmdidTool22 = 651;

			// Token: 0x040039CB RID: 14795
			internal const int cmdidTool23 = 652;

			// Token: 0x040039CC RID: 14796
			internal const int cmdidTool24 = 653;

			// Token: 0x040039CD RID: 14797
			internal const int cmdidExternalCommands = 654;

			// Token: 0x040039CE RID: 14798
			internal const int cmdidPasteNextTBXCBItem = 655;

			// Token: 0x040039CF RID: 14799
			internal const int cmdidToolboxShowAllTabs = 656;

			// Token: 0x040039D0 RID: 14800
			internal const int cmdidProjectDependencies = 657;

			// Token: 0x040039D1 RID: 14801
			internal const int cmdidCloseDocument = 658;

			// Token: 0x040039D2 RID: 14802
			internal const int cmdidToolboxSortItems = 659;

			// Token: 0x040039D3 RID: 14803
			internal const int cmdidViewBarView1 = 660;

			// Token: 0x040039D4 RID: 14804
			internal const int cmdidViewBarView2 = 661;

			// Token: 0x040039D5 RID: 14805
			internal const int cmdidViewBarView3 = 662;

			// Token: 0x040039D6 RID: 14806
			internal const int cmdidViewBarView4 = 663;

			// Token: 0x040039D7 RID: 14807
			internal const int cmdidViewBarView5 = 664;

			// Token: 0x040039D8 RID: 14808
			internal const int cmdidViewBarView6 = 665;

			// Token: 0x040039D9 RID: 14809
			internal const int cmdidViewBarView7 = 666;

			// Token: 0x040039DA RID: 14810
			internal const int cmdidViewBarView8 = 667;

			// Token: 0x040039DB RID: 14811
			internal const int cmdidViewBarView9 = 668;

			// Token: 0x040039DC RID: 14812
			internal const int cmdidViewBarView10 = 669;

			// Token: 0x040039DD RID: 14813
			internal const int cmdidViewBarView11 = 670;

			// Token: 0x040039DE RID: 14814
			internal const int cmdidViewBarView12 = 671;

			// Token: 0x040039DF RID: 14815
			internal const int cmdidViewBarView13 = 672;

			// Token: 0x040039E0 RID: 14816
			internal const int cmdidViewBarView14 = 673;

			// Token: 0x040039E1 RID: 14817
			internal const int cmdidViewBarView15 = 674;

			// Token: 0x040039E2 RID: 14818
			internal const int cmdidViewBarView16 = 675;

			// Token: 0x040039E3 RID: 14819
			internal const int cmdidViewBarView17 = 676;

			// Token: 0x040039E4 RID: 14820
			internal const int cmdidViewBarView18 = 677;

			// Token: 0x040039E5 RID: 14821
			internal const int cmdidViewBarView19 = 678;

			// Token: 0x040039E6 RID: 14822
			internal const int cmdidViewBarView20 = 679;

			// Token: 0x040039E7 RID: 14823
			internal const int cmdidViewBarView21 = 680;

			// Token: 0x040039E8 RID: 14824
			internal const int cmdidViewBarView22 = 681;

			// Token: 0x040039E9 RID: 14825
			internal const int cmdidViewBarView23 = 682;

			// Token: 0x040039EA RID: 14826
			internal const int cmdidViewBarView24 = 683;

			// Token: 0x040039EB RID: 14827
			internal const int cmdidSolutionCfg = 684;

			// Token: 0x040039EC RID: 14828
			internal const int cmdidSolutionCfgGetList = 685;

			// Token: 0x040039ED RID: 14829
			internal const int cmdidManageIndexes = 675;

			// Token: 0x040039EE RID: 14830
			internal const int cmdidManageRelationships = 676;

			// Token: 0x040039EF RID: 14831
			internal const int cmdidManageConstraints = 677;

			// Token: 0x040039F0 RID: 14832
			internal const int cmdidTaskListCustomView1 = 678;

			// Token: 0x040039F1 RID: 14833
			internal const int cmdidTaskListCustomView2 = 679;

			// Token: 0x040039F2 RID: 14834
			internal const int cmdidTaskListCustomView3 = 680;

			// Token: 0x040039F3 RID: 14835
			internal const int cmdidTaskListCustomView4 = 681;

			// Token: 0x040039F4 RID: 14836
			internal const int cmdidTaskListCustomView5 = 682;

			// Token: 0x040039F5 RID: 14837
			internal const int cmdidTaskListCustomView6 = 683;

			// Token: 0x040039F6 RID: 14838
			internal const int cmdidTaskListCustomView7 = 684;

			// Token: 0x040039F7 RID: 14839
			internal const int cmdidTaskListCustomView8 = 685;

			// Token: 0x040039F8 RID: 14840
			internal const int cmdidTaskListCustomView9 = 686;

			// Token: 0x040039F9 RID: 14841
			internal const int cmdidTaskListCustomView10 = 687;

			// Token: 0x040039FA RID: 14842
			internal const int cmdidTaskListCustomView11 = 688;

			// Token: 0x040039FB RID: 14843
			internal const int cmdidTaskListCustomView12 = 689;

			// Token: 0x040039FC RID: 14844
			internal const int cmdidTaskListCustomView13 = 690;

			// Token: 0x040039FD RID: 14845
			internal const int cmdidTaskListCustomView14 = 691;

			// Token: 0x040039FE RID: 14846
			internal const int cmdidTaskListCustomView15 = 692;

			// Token: 0x040039FF RID: 14847
			internal const int cmdidTaskListCustomView16 = 693;

			// Token: 0x04003A00 RID: 14848
			internal const int cmdidTaskListCustomView17 = 694;

			// Token: 0x04003A01 RID: 14849
			internal const int cmdidTaskListCustomView18 = 695;

			// Token: 0x04003A02 RID: 14850
			internal const int cmdidTaskListCustomView19 = 696;

			// Token: 0x04003A03 RID: 14851
			internal const int cmdidTaskListCustomView20 = 697;

			// Token: 0x04003A04 RID: 14852
			internal const int cmdidTaskListCustomView21 = 698;

			// Token: 0x04003A05 RID: 14853
			internal const int cmdidTaskListCustomView22 = 699;

			// Token: 0x04003A06 RID: 14854
			internal const int cmdidTaskListCustomView23 = 700;

			// Token: 0x04003A07 RID: 14855
			internal const int cmdidTaskListCustomView24 = 701;

			// Token: 0x04003A08 RID: 14856
			internal const int cmdidTaskListCustomView25 = 702;

			// Token: 0x04003A09 RID: 14857
			internal const int cmdidTaskListCustomView26 = 703;

			// Token: 0x04003A0A RID: 14858
			internal const int cmdidTaskListCustomView27 = 704;

			// Token: 0x04003A0B RID: 14859
			internal const int cmdidTaskListCustomView28 = 705;

			// Token: 0x04003A0C RID: 14860
			internal const int cmdidTaskListCustomView29 = 706;

			// Token: 0x04003A0D RID: 14861
			internal const int cmdidTaskListCustomView30 = 707;

			// Token: 0x04003A0E RID: 14862
			internal const int cmdidTaskListCustomView31 = 708;

			// Token: 0x04003A0F RID: 14863
			internal const int cmdidTaskListCustomView32 = 709;

			// Token: 0x04003A10 RID: 14864
			internal const int cmdidTaskListCustomView33 = 710;

			// Token: 0x04003A11 RID: 14865
			internal const int cmdidTaskListCustomView34 = 711;

			// Token: 0x04003A12 RID: 14866
			internal const int cmdidTaskListCustomView35 = 712;

			// Token: 0x04003A13 RID: 14867
			internal const int cmdidTaskListCustomView36 = 713;

			// Token: 0x04003A14 RID: 14868
			internal const int cmdidTaskListCustomView37 = 714;

			// Token: 0x04003A15 RID: 14869
			internal const int cmdidTaskListCustomView38 = 715;

			// Token: 0x04003A16 RID: 14870
			internal const int cmdidTaskListCustomView39 = 716;

			// Token: 0x04003A17 RID: 14871
			internal const int cmdidTaskListCustomView40 = 717;

			// Token: 0x04003A18 RID: 14872
			internal const int cmdidTaskListCustomView41 = 718;

			// Token: 0x04003A19 RID: 14873
			internal const int cmdidTaskListCustomView42 = 719;

			// Token: 0x04003A1A RID: 14874
			internal const int cmdidTaskListCustomView43 = 720;

			// Token: 0x04003A1B RID: 14875
			internal const int cmdidTaskListCustomView44 = 721;

			// Token: 0x04003A1C RID: 14876
			internal const int cmdidTaskListCustomView45 = 722;

			// Token: 0x04003A1D RID: 14877
			internal const int cmdidTaskListCustomView46 = 723;

			// Token: 0x04003A1E RID: 14878
			internal const int cmdidTaskListCustomView47 = 724;

			// Token: 0x04003A1F RID: 14879
			internal const int cmdidTaskListCustomView48 = 725;

			// Token: 0x04003A20 RID: 14880
			internal const int cmdidTaskListCustomView49 = 726;

			// Token: 0x04003A21 RID: 14881
			internal const int cmdidTaskListCustomView50 = 727;

			// Token: 0x04003A22 RID: 14882
			internal const int cmdidObjectSearch = 728;

			// Token: 0x04003A23 RID: 14883
			internal const int cmdidCommandWindow = 729;

			// Token: 0x04003A24 RID: 14884
			internal const int cmdidCommandWindowMarkMode = 730;

			// Token: 0x04003A25 RID: 14885
			internal const int cmdidLogCommandWindow = 731;

			// Token: 0x04003A26 RID: 14886
			internal const int cmdidShell = 732;

			// Token: 0x04003A27 RID: 14887
			internal const int cmdidSingleChar = 733;

			// Token: 0x04003A28 RID: 14888
			internal const int cmdidZeroOrMore = 734;

			// Token: 0x04003A29 RID: 14889
			internal const int cmdidOneOrMore = 735;

			// Token: 0x04003A2A RID: 14890
			internal const int cmdidBeginLine = 736;

			// Token: 0x04003A2B RID: 14891
			internal const int cmdidEndLine = 737;

			// Token: 0x04003A2C RID: 14892
			internal const int cmdidBeginWord = 738;

			// Token: 0x04003A2D RID: 14893
			internal const int cmdidEndWord = 739;

			// Token: 0x04003A2E RID: 14894
			internal const int cmdidCharInSet = 740;

			// Token: 0x04003A2F RID: 14895
			internal const int cmdidCharNotInSet = 741;

			// Token: 0x04003A30 RID: 14896
			internal const int cmdidOr = 742;

			// Token: 0x04003A31 RID: 14897
			internal const int cmdidEscape = 743;

			// Token: 0x04003A32 RID: 14898
			internal const int cmdidTagExp = 744;

			// Token: 0x04003A33 RID: 14899
			internal const int cmdidPatternMatchHelp = 745;

			// Token: 0x04003A34 RID: 14900
			internal const int cmdidRegExList = 746;

			// Token: 0x04003A35 RID: 14901
			internal const int cmdidDebugReserved1 = 747;

			// Token: 0x04003A36 RID: 14902
			internal const int cmdidDebugReserved2 = 748;

			// Token: 0x04003A37 RID: 14903
			internal const int cmdidDebugReserved3 = 749;

			// Token: 0x04003A38 RID: 14904
			internal const int cmdidWildZeroOrMore = 754;

			// Token: 0x04003A39 RID: 14905
			internal const int cmdidWildSingleChar = 755;

			// Token: 0x04003A3A RID: 14906
			internal const int cmdidWildSingleDigit = 756;

			// Token: 0x04003A3B RID: 14907
			internal const int cmdidWildCharInSet = 757;

			// Token: 0x04003A3C RID: 14908
			internal const int cmdidWildCharNotInSet = 758;

			// Token: 0x04003A3D RID: 14909
			internal const int cmdidFindWhatText = 759;

			// Token: 0x04003A3E RID: 14910
			internal const int cmdidTaggedExp1 = 760;

			// Token: 0x04003A3F RID: 14911
			internal const int cmdidTaggedExp2 = 761;

			// Token: 0x04003A40 RID: 14912
			internal const int cmdidTaggedExp3 = 762;

			// Token: 0x04003A41 RID: 14913
			internal const int cmdidTaggedExp4 = 763;

			// Token: 0x04003A42 RID: 14914
			internal const int cmdidTaggedExp5 = 764;

			// Token: 0x04003A43 RID: 14915
			internal const int cmdidTaggedExp6 = 765;

			// Token: 0x04003A44 RID: 14916
			internal const int cmdidTaggedExp7 = 766;

			// Token: 0x04003A45 RID: 14917
			internal const int cmdidTaggedExp8 = 767;

			// Token: 0x04003A46 RID: 14918
			internal const int cmdidTaggedExp9 = 768;

			// Token: 0x04003A47 RID: 14919
			internal const int cmdidEditorWidgetClick = 769;

			// Token: 0x04003A48 RID: 14920
			internal const int cmdidCmdWinUpdateAC = 770;

			// Token: 0x04003A49 RID: 14921
			internal const int cmdidSlnCfgMgr = 771;

			// Token: 0x04003A4A RID: 14922
			internal const int cmdidAddNewProject = 772;

			// Token: 0x04003A4B RID: 14923
			internal const int cmdidAddExistingProject = 773;

			// Token: 0x04003A4C RID: 14924
			internal const int cmdidAddNewSolutionItem = 774;

			// Token: 0x04003A4D RID: 14925
			internal const int cmdidAddExistingSolutionItem = 775;

			// Token: 0x04003A4E RID: 14926
			internal const int cmdidAutoHideContext1 = 776;

			// Token: 0x04003A4F RID: 14927
			internal const int cmdidAutoHideContext2 = 777;

			// Token: 0x04003A50 RID: 14928
			internal const int cmdidAutoHideContext3 = 778;

			// Token: 0x04003A51 RID: 14929
			internal const int cmdidAutoHideContext4 = 779;

			// Token: 0x04003A52 RID: 14930
			internal const int cmdidAutoHideContext5 = 780;

			// Token: 0x04003A53 RID: 14931
			internal const int cmdidAutoHideContext6 = 781;

			// Token: 0x04003A54 RID: 14932
			internal const int cmdidAutoHideContext7 = 782;

			// Token: 0x04003A55 RID: 14933
			internal const int cmdidAutoHideContext8 = 783;

			// Token: 0x04003A56 RID: 14934
			internal const int cmdidAutoHideContext9 = 784;

			// Token: 0x04003A57 RID: 14935
			internal const int cmdidAutoHideContext10 = 785;

			// Token: 0x04003A58 RID: 14936
			internal const int cmdidAutoHideContext11 = 786;

			// Token: 0x04003A59 RID: 14937
			internal const int cmdidAutoHideContext12 = 787;

			// Token: 0x04003A5A RID: 14938
			internal const int cmdidAutoHideContext13 = 788;

			// Token: 0x04003A5B RID: 14939
			internal const int cmdidAutoHideContext14 = 789;

			// Token: 0x04003A5C RID: 14940
			internal const int cmdidAutoHideContext15 = 790;

			// Token: 0x04003A5D RID: 14941
			internal const int cmdidAutoHideContext16 = 791;

			// Token: 0x04003A5E RID: 14942
			internal const int cmdidAutoHideContext17 = 792;

			// Token: 0x04003A5F RID: 14943
			internal const int cmdidAutoHideContext18 = 793;

			// Token: 0x04003A60 RID: 14944
			internal const int cmdidAutoHideContext19 = 794;

			// Token: 0x04003A61 RID: 14945
			internal const int cmdidAutoHideContext20 = 795;

			// Token: 0x04003A62 RID: 14946
			internal const int cmdidAutoHideContext21 = 796;

			// Token: 0x04003A63 RID: 14947
			internal const int cmdidAutoHideContext22 = 797;

			// Token: 0x04003A64 RID: 14948
			internal const int cmdidAutoHideContext23 = 798;

			// Token: 0x04003A65 RID: 14949
			internal const int cmdidAutoHideContext24 = 799;

			// Token: 0x04003A66 RID: 14950
			internal const int cmdidAutoHideContext25 = 800;

			// Token: 0x04003A67 RID: 14951
			internal const int cmdidAutoHideContext26 = 801;

			// Token: 0x04003A68 RID: 14952
			internal const int cmdidAutoHideContext27 = 802;

			// Token: 0x04003A69 RID: 14953
			internal const int cmdidAutoHideContext28 = 803;

			// Token: 0x04003A6A RID: 14954
			internal const int cmdidAutoHideContext29 = 804;

			// Token: 0x04003A6B RID: 14955
			internal const int cmdidAutoHideContext30 = 805;

			// Token: 0x04003A6C RID: 14956
			internal const int cmdidAutoHideContext31 = 806;

			// Token: 0x04003A6D RID: 14957
			internal const int cmdidAutoHideContext32 = 807;

			// Token: 0x04003A6E RID: 14958
			internal const int cmdidAutoHideContext33 = 808;

			// Token: 0x04003A6F RID: 14959
			internal const int cmdidShellNavBackward = 809;

			// Token: 0x04003A70 RID: 14960
			internal const int cmdidShellNavForward = 810;

			// Token: 0x04003A71 RID: 14961
			internal const int cmdidShellNavigate1 = 811;

			// Token: 0x04003A72 RID: 14962
			internal const int cmdidShellNavigate2 = 812;

			// Token: 0x04003A73 RID: 14963
			internal const int cmdidShellNavigate3 = 813;

			// Token: 0x04003A74 RID: 14964
			internal const int cmdidShellNavigate4 = 814;

			// Token: 0x04003A75 RID: 14965
			internal const int cmdidShellNavigate5 = 815;

			// Token: 0x04003A76 RID: 14966
			internal const int cmdidShellNavigate6 = 816;

			// Token: 0x04003A77 RID: 14967
			internal const int cmdidShellNavigate7 = 817;

			// Token: 0x04003A78 RID: 14968
			internal const int cmdidShellNavigate8 = 818;

			// Token: 0x04003A79 RID: 14969
			internal const int cmdidShellNavigate9 = 819;

			// Token: 0x04003A7A RID: 14970
			internal const int cmdidShellNavigate10 = 820;

			// Token: 0x04003A7B RID: 14971
			internal const int cmdidShellNavigate11 = 821;

			// Token: 0x04003A7C RID: 14972
			internal const int cmdidShellNavigate12 = 822;

			// Token: 0x04003A7D RID: 14973
			internal const int cmdidShellNavigate13 = 823;

			// Token: 0x04003A7E RID: 14974
			internal const int cmdidShellNavigate14 = 824;

			// Token: 0x04003A7F RID: 14975
			internal const int cmdidShellNavigate15 = 825;

			// Token: 0x04003A80 RID: 14976
			internal const int cmdidShellNavigate16 = 826;

			// Token: 0x04003A81 RID: 14977
			internal const int cmdidShellNavigate17 = 827;

			// Token: 0x04003A82 RID: 14978
			internal const int cmdidShellNavigate18 = 828;

			// Token: 0x04003A83 RID: 14979
			internal const int cmdidShellNavigate19 = 829;

			// Token: 0x04003A84 RID: 14980
			internal const int cmdidShellNavigate20 = 830;

			// Token: 0x04003A85 RID: 14981
			internal const int cmdidShellNavigate21 = 831;

			// Token: 0x04003A86 RID: 14982
			internal const int cmdidShellNavigate22 = 832;

			// Token: 0x04003A87 RID: 14983
			internal const int cmdidShellNavigate23 = 833;

			// Token: 0x04003A88 RID: 14984
			internal const int cmdidShellNavigate24 = 834;

			// Token: 0x04003A89 RID: 14985
			internal const int cmdidShellNavigate25 = 835;

			// Token: 0x04003A8A RID: 14986
			internal const int cmdidShellNavigate26 = 836;

			// Token: 0x04003A8B RID: 14987
			internal const int cmdidShellNavigate27 = 837;

			// Token: 0x04003A8C RID: 14988
			internal const int cmdidShellNavigate28 = 838;

			// Token: 0x04003A8D RID: 14989
			internal const int cmdidShellNavigate29 = 839;

			// Token: 0x04003A8E RID: 14990
			internal const int cmdidShellNavigate30 = 840;

			// Token: 0x04003A8F RID: 14991
			internal const int cmdidShellNavigate31 = 841;

			// Token: 0x04003A90 RID: 14992
			internal const int cmdidShellNavigate32 = 842;

			// Token: 0x04003A91 RID: 14993
			internal const int cmdidShellNavigate33 = 843;

			// Token: 0x04003A92 RID: 14994
			internal const int cmdidShellWindowNavigate1 = 844;

			// Token: 0x04003A93 RID: 14995
			internal const int cmdidShellWindowNavigate2 = 845;

			// Token: 0x04003A94 RID: 14996
			internal const int cmdidShellWindowNavigate3 = 846;

			// Token: 0x04003A95 RID: 14997
			internal const int cmdidShellWindowNavigate4 = 847;

			// Token: 0x04003A96 RID: 14998
			internal const int cmdidShellWindowNavigate5 = 848;

			// Token: 0x04003A97 RID: 14999
			internal const int cmdidShellWindowNavigate6 = 849;

			// Token: 0x04003A98 RID: 15000
			internal const int cmdidShellWindowNavigate7 = 850;

			// Token: 0x04003A99 RID: 15001
			internal const int cmdidShellWindowNavigate8 = 851;

			// Token: 0x04003A9A RID: 15002
			internal const int cmdidShellWindowNavigate9 = 852;

			// Token: 0x04003A9B RID: 15003
			internal const int cmdidShellWindowNavigate10 = 853;

			// Token: 0x04003A9C RID: 15004
			internal const int cmdidShellWindowNavigate11 = 854;

			// Token: 0x04003A9D RID: 15005
			internal const int cmdidShellWindowNavigate12 = 855;

			// Token: 0x04003A9E RID: 15006
			internal const int cmdidShellWindowNavigate13 = 856;

			// Token: 0x04003A9F RID: 15007
			internal const int cmdidShellWindowNavigate14 = 857;

			// Token: 0x04003AA0 RID: 15008
			internal const int cmdidShellWindowNavigate15 = 858;

			// Token: 0x04003AA1 RID: 15009
			internal const int cmdidShellWindowNavigate16 = 859;

			// Token: 0x04003AA2 RID: 15010
			internal const int cmdidShellWindowNavigate17 = 860;

			// Token: 0x04003AA3 RID: 15011
			internal const int cmdidShellWindowNavigate18 = 861;

			// Token: 0x04003AA4 RID: 15012
			internal const int cmdidShellWindowNavigate19 = 862;

			// Token: 0x04003AA5 RID: 15013
			internal const int cmdidShellWindowNavigate20 = 863;

			// Token: 0x04003AA6 RID: 15014
			internal const int cmdidShellWindowNavigate21 = 864;

			// Token: 0x04003AA7 RID: 15015
			internal const int cmdidShellWindowNavigate22 = 865;

			// Token: 0x04003AA8 RID: 15016
			internal const int cmdidShellWindowNavigate23 = 866;

			// Token: 0x04003AA9 RID: 15017
			internal const int cmdidShellWindowNavigate24 = 867;

			// Token: 0x04003AAA RID: 15018
			internal const int cmdidShellWindowNavigate25 = 868;

			// Token: 0x04003AAB RID: 15019
			internal const int cmdidShellWindowNavigate26 = 869;

			// Token: 0x04003AAC RID: 15020
			internal const int cmdidShellWindowNavigate27 = 870;

			// Token: 0x04003AAD RID: 15021
			internal const int cmdidShellWindowNavigate28 = 871;

			// Token: 0x04003AAE RID: 15022
			internal const int cmdidShellWindowNavigate29 = 872;

			// Token: 0x04003AAF RID: 15023
			internal const int cmdidShellWindowNavigate30 = 873;

			// Token: 0x04003AB0 RID: 15024
			internal const int cmdidShellWindowNavigate31 = 874;

			// Token: 0x04003AB1 RID: 15025
			internal const int cmdidShellWindowNavigate32 = 875;

			// Token: 0x04003AB2 RID: 15026
			internal const int cmdidShellWindowNavigate33 = 876;

			// Token: 0x04003AB3 RID: 15027
			internal const int cmdidOBSDoFind = 877;

			// Token: 0x04003AB4 RID: 15028
			internal const int cmdidOBSMatchCase = 878;

			// Token: 0x04003AB5 RID: 15029
			internal const int cmdidOBSMatchSubString = 879;

			// Token: 0x04003AB6 RID: 15030
			internal const int cmdidOBSMatchWholeWord = 880;

			// Token: 0x04003AB7 RID: 15031
			internal const int cmdidOBSMatchPrefix = 881;

			// Token: 0x04003AB8 RID: 15032
			internal const int cmdidBuildSln = 882;

			// Token: 0x04003AB9 RID: 15033
			internal const int cmdidRebuildSln = 883;

			// Token: 0x04003ABA RID: 15034
			internal const int cmdidDeploySln = 884;

			// Token: 0x04003ABB RID: 15035
			internal const int cmdidCleanSln = 885;

			// Token: 0x04003ABC RID: 15036
			internal const int cmdidBuildSel = 886;

			// Token: 0x04003ABD RID: 15037
			internal const int cmdidRebuildSel = 887;

			// Token: 0x04003ABE RID: 15038
			internal const int cmdidDeploySel = 888;

			// Token: 0x04003ABF RID: 15039
			internal const int cmdidCleanSel = 889;

			// Token: 0x04003AC0 RID: 15040
			internal const int cmdidCancelBuild = 890;

			// Token: 0x04003AC1 RID: 15041
			internal const int cmdidBatchBuildDlg = 891;

			// Token: 0x04003AC2 RID: 15042
			internal const int cmdidBuildCtx = 892;

			// Token: 0x04003AC3 RID: 15043
			internal const int cmdidRebuildCtx = 893;

			// Token: 0x04003AC4 RID: 15044
			internal const int cmdidDeployCtx = 894;

			// Token: 0x04003AC5 RID: 15045
			internal const int cmdidCleanCtx = 895;

			// Token: 0x04003AC6 RID: 15046
			internal const int cmdidMRUFile1 = 900;

			// Token: 0x04003AC7 RID: 15047
			internal const int cmdidMRUFile2 = 901;

			// Token: 0x04003AC8 RID: 15048
			internal const int cmdidMRUFile3 = 902;

			// Token: 0x04003AC9 RID: 15049
			internal const int cmdidMRUFile4 = 903;

			// Token: 0x04003ACA RID: 15050
			internal const int cmdidMRUFile5 = 904;

			// Token: 0x04003ACB RID: 15051
			internal const int cmdidMRUFile6 = 905;

			// Token: 0x04003ACC RID: 15052
			internal const int cmdidMRUFile7 = 906;

			// Token: 0x04003ACD RID: 15053
			internal const int cmdidMRUFile8 = 907;

			// Token: 0x04003ACE RID: 15054
			internal const int cmdidMRUFile9 = 908;

			// Token: 0x04003ACF RID: 15055
			internal const int cmdidMRUFile10 = 909;

			// Token: 0x04003AD0 RID: 15056
			internal const int cmdidMRUFile11 = 910;

			// Token: 0x04003AD1 RID: 15057
			internal const int cmdidMRUFile12 = 911;

			// Token: 0x04003AD2 RID: 15058
			internal const int cmdidMRUFile13 = 912;

			// Token: 0x04003AD3 RID: 15059
			internal const int cmdidMRUFile14 = 913;

			// Token: 0x04003AD4 RID: 15060
			internal const int cmdidMRUFile15 = 914;

			// Token: 0x04003AD5 RID: 15061
			internal const int cmdidMRUFile16 = 915;

			// Token: 0x04003AD6 RID: 15062
			internal const int cmdidMRUFile17 = 916;

			// Token: 0x04003AD7 RID: 15063
			internal const int cmdidMRUFile18 = 917;

			// Token: 0x04003AD8 RID: 15064
			internal const int cmdidMRUFile19 = 918;

			// Token: 0x04003AD9 RID: 15065
			internal const int cmdidMRUFile20 = 919;

			// Token: 0x04003ADA RID: 15066
			internal const int cmdidMRUFile21 = 920;

			// Token: 0x04003ADB RID: 15067
			internal const int cmdidMRUFile22 = 921;

			// Token: 0x04003ADC RID: 15068
			internal const int cmdidMRUFile23 = 922;

			// Token: 0x04003ADD RID: 15069
			internal const int cmdidMRUFile24 = 923;

			// Token: 0x04003ADE RID: 15070
			internal const int cmdidMRUFile25 = 924;

			// Token: 0x04003ADF RID: 15071
			internal const int cmdidGotoDefn = 925;

			// Token: 0x04003AE0 RID: 15072
			internal const int cmdidGotoDecl = 926;

			// Token: 0x04003AE1 RID: 15073
			internal const int cmdidBrowseDefn = 927;

			// Token: 0x04003AE2 RID: 15074
			internal const int cmdidShowMembers = 928;

			// Token: 0x04003AE3 RID: 15075
			internal const int cmdidShowBases = 929;

			// Token: 0x04003AE4 RID: 15076
			internal const int cmdidShowDerived = 930;

			// Token: 0x04003AE5 RID: 15077
			internal const int cmdidShowDefns = 931;

			// Token: 0x04003AE6 RID: 15078
			internal const int cmdidShowRefs = 932;

			// Token: 0x04003AE7 RID: 15079
			internal const int cmdidShowCallers = 933;

			// Token: 0x04003AE8 RID: 15080
			internal const int cmdidShowCallees = 934;

			// Token: 0x04003AE9 RID: 15081
			internal const int cmdidDefineSubset = 935;

			// Token: 0x04003AEA RID: 15082
			internal const int cmdidSetSubset = 936;

			// Token: 0x04003AEB RID: 15083
			internal const int cmdidCVGroupingNone = 950;

			// Token: 0x04003AEC RID: 15084
			internal const int cmdidCVGroupingSortOnly = 951;

			// Token: 0x04003AED RID: 15085
			internal const int cmdidCVGroupingGrouped = 952;

			// Token: 0x04003AEE RID: 15086
			internal const int cmdidCVShowPackages = 953;

			// Token: 0x04003AEF RID: 15087
			internal const int cmdidQryManageIndexes = 954;

			// Token: 0x04003AF0 RID: 15088
			internal const int cmdidBrowseComponent = 955;

			// Token: 0x04003AF1 RID: 15089
			internal const int cmdidPrintDefault = 956;

			// Token: 0x04003AF2 RID: 15090
			internal const int cmdidBrowseDoc = 957;

			// Token: 0x04003AF3 RID: 15091
			internal const int cmdidStandardMax = 1000;

			// Token: 0x04003AF4 RID: 15092
			internal const int cmdidFormsFirst = 24576;

			// Token: 0x04003AF5 RID: 15093
			internal const int cmdidFormsLast = 28671;

			// Token: 0x04003AF6 RID: 15094
			internal const int cmdidVBEFirst = 32768;

			// Token: 0x04003AF7 RID: 15095
			internal const int msotcidBookmarkWellMenu = 32769;

			// Token: 0x04003AF8 RID: 15096
			internal const int cmdidZoom200 = 32770;

			// Token: 0x04003AF9 RID: 15097
			internal const int cmdidZoom150 = 32771;

			// Token: 0x04003AFA RID: 15098
			internal const int cmdidZoom100 = 32772;

			// Token: 0x04003AFB RID: 15099
			internal const int cmdidZoom75 = 32773;

			// Token: 0x04003AFC RID: 15100
			internal const int cmdidZoom50 = 32774;

			// Token: 0x04003AFD RID: 15101
			internal const int cmdidZoom25 = 32775;

			// Token: 0x04003AFE RID: 15102
			internal const int cmdidZoom10 = 32784;

			// Token: 0x04003AFF RID: 15103
			internal const int msotcidZoomWellMenu = 32785;

			// Token: 0x04003B00 RID: 15104
			internal const int msotcidDebugPopWellMenu = 32786;

			// Token: 0x04003B01 RID: 15105
			internal const int msotcidAlignWellMenu = 32787;

			// Token: 0x04003B02 RID: 15106
			internal const int msotcidArrangeWellMenu = 32788;

			// Token: 0x04003B03 RID: 15107
			internal const int msotcidCenterWellMenu = 32789;

			// Token: 0x04003B04 RID: 15108
			internal const int msotcidSizeWellMenu = 32790;

			// Token: 0x04003B05 RID: 15109
			internal const int msotcidHorizontalSpaceWellMenu = 32791;

			// Token: 0x04003B06 RID: 15110
			internal const int msotcidVerticalSpaceWellMenu = 32800;

			// Token: 0x04003B07 RID: 15111
			internal const int msotcidDebugWellMenu = 32801;

			// Token: 0x04003B08 RID: 15112
			internal const int msotcidDebugMenuVB = 32802;

			// Token: 0x04003B09 RID: 15113
			internal const int msotcidStatementBuilderWellMenu = 32803;

			// Token: 0x04003B0A RID: 15114
			internal const int msotcidProjWinInsertMenu = 32804;

			// Token: 0x04003B0B RID: 15115
			internal const int msotcidToggleMenu = 32805;

			// Token: 0x04003B0C RID: 15116
			internal const int msotcidNewObjInsertWellMenu = 32806;

			// Token: 0x04003B0D RID: 15117
			internal const int msotcidSizeToWellMenu = 32807;

			// Token: 0x04003B0E RID: 15118
			internal const int msotcidCommandBars = 32808;

			// Token: 0x04003B0F RID: 15119
			internal const int msotcidVBOrderMenu = 32809;

			// Token: 0x04003B10 RID: 15120
			internal const int msotcidMSOnTheWeb = 32810;

			// Token: 0x04003B11 RID: 15121
			internal const int msotcidVBDesignerMenu = 32816;

			// Token: 0x04003B12 RID: 15122
			internal const int msotcidNewProjectWellMenu = 32817;

			// Token: 0x04003B13 RID: 15123
			internal const int msotcidProjectWellMenu = 32818;

			// Token: 0x04003B14 RID: 15124
			internal const int msotcidVBCode1ContextMenu = 32819;

			// Token: 0x04003B15 RID: 15125
			internal const int msotcidVBCode2ContextMenu = 32820;

			// Token: 0x04003B16 RID: 15126
			internal const int msotcidVBWatchContextMenu = 32821;

			// Token: 0x04003B17 RID: 15127
			internal const int msotcidVBImmediateContextMenu = 32822;

			// Token: 0x04003B18 RID: 15128
			internal const int msotcidVBLocalsContextMenu = 32823;

			// Token: 0x04003B19 RID: 15129
			internal const int msotcidVBFormContextMenu = 32824;

			// Token: 0x04003B1A RID: 15130
			internal const int msotcidVBControlContextMenu = 32825;

			// Token: 0x04003B1B RID: 15131
			internal const int msotcidVBProjWinContextMenu = 32826;

			// Token: 0x04003B1C RID: 15132
			internal const int msotcidVBProjWinContextBreakMenu = 32827;

			// Token: 0x04003B1D RID: 15133
			internal const int msotcidVBPreviewWinContextMenu = 32828;

			// Token: 0x04003B1E RID: 15134
			internal const int msotcidVBOBContextMenu = 32829;

			// Token: 0x04003B1F RID: 15135
			internal const int msotcidVBForms3ContextMenu = 32830;

			// Token: 0x04003B20 RID: 15136
			internal const int msotcidVBForms3ControlCMenu = 32831;

			// Token: 0x04003B21 RID: 15137
			internal const int msotcidVBForms3ControlCMenuGroup = 32832;

			// Token: 0x04003B22 RID: 15138
			internal const int msotcidVBForms3ControlPalette = 32833;

			// Token: 0x04003B23 RID: 15139
			internal const int msotcidVBForms3ToolboxCMenu = 32834;

			// Token: 0x04003B24 RID: 15140
			internal const int msotcidVBForms3MPCCMenu = 32835;

			// Token: 0x04003B25 RID: 15141
			internal const int msotcidVBForms3DragDropCMenu = 32836;

			// Token: 0x04003B26 RID: 15142
			internal const int msotcidVBToolBoxContextMenu = 32837;

			// Token: 0x04003B27 RID: 15143
			internal const int msotcidVBToolBoxGroupContextMenu = 32838;

			// Token: 0x04003B28 RID: 15144
			internal const int msotcidVBPropBrsHostContextMenu = 32839;

			// Token: 0x04003B29 RID: 15145
			internal const int msotcidVBPropBrsContextMenu = 32840;

			// Token: 0x04003B2A RID: 15146
			internal const int msotcidVBPalContextMenu = 32841;

			// Token: 0x04003B2B RID: 15147
			internal const int msotcidVBProjWinProjectContextMenu = 32842;

			// Token: 0x04003B2C RID: 15148
			internal const int msotcidVBProjWinFormContextMenu = 32843;

			// Token: 0x04003B2D RID: 15149
			internal const int msotcidVBProjWinModClassContextMenu = 32844;

			// Token: 0x04003B2E RID: 15150
			internal const int msotcidVBProjWinRelDocContextMenu = 32845;

			// Token: 0x04003B2F RID: 15151
			internal const int msotcidVBDockedWindowContextMenu = 32846;

			// Token: 0x04003B30 RID: 15152
			internal const int msotcidVBShortCutForms = 32847;

			// Token: 0x04003B31 RID: 15153
			internal const int msotcidVBShortCutCodeWindows = 32848;

			// Token: 0x04003B32 RID: 15154
			internal const int msotcidVBShortCutMisc = 32849;

			// Token: 0x04003B33 RID: 15155
			internal const int msotcidVBBuiltInMenus = 32850;

			// Token: 0x04003B34 RID: 15156
			internal const int msotcidPreviewWinFormPos = 32851;

			// Token: 0x04003B35 RID: 15157
			internal const int msotcidVBAddinFirst = 33280;
		}

		// Token: 0x020008AE RID: 2222
		private static class ShellGuids
		{
			// Token: 0x04003B36 RID: 15158
			internal static readonly Guid VSStandardCommandSet97 = new Guid("{5efc7975-14bc-11cf-9b2b-00aa00573819}");

			// Token: 0x04003B37 RID: 15159
			internal static readonly Guid guidDsdCmdId = new Guid("{1F0FD094-8e53-11d2-8f9c-0060089fc486}");

			// Token: 0x04003B38 RID: 15160
			internal static readonly Guid SID_SOleComponentUIManager = new Guid("{5efc7974-14bc-11cf-9b2b-00aa00573819}");

			// Token: 0x04003B39 RID: 15161
			internal static readonly Guid GUID_VSTASKCATEGORY_DATADESIGNER = new Guid("{6B32EAED-13BB-11d3-A64F-00C04F683820}");

			// Token: 0x04003B3A RID: 15162
			internal static readonly Guid GUID_PropertyBrowserToolWindow = new Guid(-285584864, -7528, 4560, new byte[] { 143, 120, 0, 160, 201, 17, 0, 87 });
		}
	}
}
