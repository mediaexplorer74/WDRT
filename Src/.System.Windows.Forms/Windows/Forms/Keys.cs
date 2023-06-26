using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies key codes and modifiers.</summary>
	// Token: 0x020002B5 RID: 693
	[Flags]
	[TypeConverter(typeof(KeysConverter))]
	[Editor("System.Windows.Forms.Design.ShortcutKeysEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[ComVisible(true)]
	public enum Keys
	{
		/// <summary>The bitmask to extract a key code from a key value.</summary>
		// Token: 0x04001134 RID: 4404
		KeyCode = 65535,
		/// <summary>The bitmask to extract modifiers from a key value.</summary>
		// Token: 0x04001135 RID: 4405
		Modifiers = -65536,
		/// <summary>No key pressed.</summary>
		// Token: 0x04001136 RID: 4406
		None = 0,
		/// <summary>The left mouse button.</summary>
		// Token: 0x04001137 RID: 4407
		LButton = 1,
		/// <summary>The right mouse button.</summary>
		// Token: 0x04001138 RID: 4408
		RButton = 2,
		/// <summary>The CANCEL key.</summary>
		// Token: 0x04001139 RID: 4409
		Cancel = 3,
		/// <summary>The middle mouse button (three-button mouse).</summary>
		// Token: 0x0400113A RID: 4410
		MButton = 4,
		/// <summary>The first x mouse button (five-button mouse).</summary>
		// Token: 0x0400113B RID: 4411
		XButton1 = 5,
		/// <summary>The second x mouse button (five-button mouse).</summary>
		// Token: 0x0400113C RID: 4412
		XButton2 = 6,
		/// <summary>The BACKSPACE key.</summary>
		// Token: 0x0400113D RID: 4413
		Back = 8,
		/// <summary>The TAB key.</summary>
		// Token: 0x0400113E RID: 4414
		Tab = 9,
		/// <summary>The LINEFEED key.</summary>
		// Token: 0x0400113F RID: 4415
		LineFeed = 10,
		/// <summary>The CLEAR key.</summary>
		// Token: 0x04001140 RID: 4416
		Clear = 12,
		/// <summary>The RETURN key.</summary>
		// Token: 0x04001141 RID: 4417
		Return = 13,
		/// <summary>The ENTER key.</summary>
		// Token: 0x04001142 RID: 4418
		Enter = 13,
		/// <summary>The SHIFT key.</summary>
		// Token: 0x04001143 RID: 4419
		ShiftKey = 16,
		/// <summary>The CTRL key.</summary>
		// Token: 0x04001144 RID: 4420
		ControlKey = 17,
		/// <summary>The ALT key.</summary>
		// Token: 0x04001145 RID: 4421
		Menu = 18,
		/// <summary>The PAUSE key.</summary>
		// Token: 0x04001146 RID: 4422
		Pause = 19,
		/// <summary>The CAPS LOCK key.</summary>
		// Token: 0x04001147 RID: 4423
		Capital = 20,
		/// <summary>The CAPS LOCK key.</summary>
		// Token: 0x04001148 RID: 4424
		CapsLock = 20,
		/// <summary>The IME Kana mode key.</summary>
		// Token: 0x04001149 RID: 4425
		KanaMode = 21,
		/// <summary>The IME Hanguel mode key. (maintained for compatibility; use <see langword="HangulMode" />)</summary>
		// Token: 0x0400114A RID: 4426
		HanguelMode = 21,
		/// <summary>The IME Hangul mode key.</summary>
		// Token: 0x0400114B RID: 4427
		HangulMode = 21,
		/// <summary>The IME Junja mode key.</summary>
		// Token: 0x0400114C RID: 4428
		JunjaMode = 23,
		/// <summary>The IME final mode key.</summary>
		// Token: 0x0400114D RID: 4429
		FinalMode = 24,
		/// <summary>The IME Hanja mode key.</summary>
		// Token: 0x0400114E RID: 4430
		HanjaMode = 25,
		/// <summary>The IME Kanji mode key.</summary>
		// Token: 0x0400114F RID: 4431
		KanjiMode = 25,
		/// <summary>The ESC key.</summary>
		// Token: 0x04001150 RID: 4432
		Escape = 27,
		/// <summary>The IME convert key.</summary>
		// Token: 0x04001151 RID: 4433
		IMEConvert = 28,
		/// <summary>The IME nonconvert key.</summary>
		// Token: 0x04001152 RID: 4434
		IMENonconvert = 29,
		/// <summary>The IME accept key, replaces <see cref="F:System.Windows.Forms.Keys.IMEAceept" />.</summary>
		// Token: 0x04001153 RID: 4435
		IMEAccept = 30,
		/// <summary>The IME accept key. Obsolete, use <see cref="F:System.Windows.Forms.Keys.IMEAccept" /> instead.</summary>
		// Token: 0x04001154 RID: 4436
		IMEAceept = 30,
		/// <summary>The IME mode change key.</summary>
		// Token: 0x04001155 RID: 4437
		IMEModeChange = 31,
		/// <summary>The SPACEBAR key.</summary>
		// Token: 0x04001156 RID: 4438
		Space = 32,
		/// <summary>The PAGE UP key.</summary>
		// Token: 0x04001157 RID: 4439
		Prior = 33,
		/// <summary>The PAGE UP key.</summary>
		// Token: 0x04001158 RID: 4440
		PageUp = 33,
		/// <summary>The PAGE DOWN key.</summary>
		// Token: 0x04001159 RID: 4441
		Next = 34,
		/// <summary>The PAGE DOWN key.</summary>
		// Token: 0x0400115A RID: 4442
		PageDown = 34,
		/// <summary>The END key.</summary>
		// Token: 0x0400115B RID: 4443
		End = 35,
		/// <summary>The HOME key.</summary>
		// Token: 0x0400115C RID: 4444
		Home = 36,
		/// <summary>The LEFT ARROW key.</summary>
		// Token: 0x0400115D RID: 4445
		Left = 37,
		/// <summary>The UP ARROW key.</summary>
		// Token: 0x0400115E RID: 4446
		Up = 38,
		/// <summary>The RIGHT ARROW key.</summary>
		// Token: 0x0400115F RID: 4447
		Right = 39,
		/// <summary>The DOWN ARROW key.</summary>
		// Token: 0x04001160 RID: 4448
		Down = 40,
		/// <summary>The SELECT key.</summary>
		// Token: 0x04001161 RID: 4449
		Select = 41,
		/// <summary>The PRINT key.</summary>
		// Token: 0x04001162 RID: 4450
		Print = 42,
		/// <summary>The EXECUTE key.</summary>
		// Token: 0x04001163 RID: 4451
		Execute = 43,
		/// <summary>The PRINT SCREEN key.</summary>
		// Token: 0x04001164 RID: 4452
		Snapshot = 44,
		/// <summary>The PRINT SCREEN key.</summary>
		// Token: 0x04001165 RID: 4453
		PrintScreen = 44,
		/// <summary>The INS key.</summary>
		// Token: 0x04001166 RID: 4454
		Insert = 45,
		/// <summary>The DEL key.</summary>
		// Token: 0x04001167 RID: 4455
		Delete = 46,
		/// <summary>The HELP key.</summary>
		// Token: 0x04001168 RID: 4456
		Help = 47,
		/// <summary>The 0 key.</summary>
		// Token: 0x04001169 RID: 4457
		D0 = 48,
		/// <summary>The 1 key.</summary>
		// Token: 0x0400116A RID: 4458
		D1 = 49,
		/// <summary>The 2 key.</summary>
		// Token: 0x0400116B RID: 4459
		D2 = 50,
		/// <summary>The 3 key.</summary>
		// Token: 0x0400116C RID: 4460
		D3 = 51,
		/// <summary>The 4 key.</summary>
		// Token: 0x0400116D RID: 4461
		D4 = 52,
		/// <summary>The 5 key.</summary>
		// Token: 0x0400116E RID: 4462
		D5 = 53,
		/// <summary>The 6 key.</summary>
		// Token: 0x0400116F RID: 4463
		D6 = 54,
		/// <summary>The 7 key.</summary>
		// Token: 0x04001170 RID: 4464
		D7 = 55,
		/// <summary>The 8 key.</summary>
		// Token: 0x04001171 RID: 4465
		D8 = 56,
		/// <summary>The 9 key.</summary>
		// Token: 0x04001172 RID: 4466
		D9 = 57,
		/// <summary>The A key.</summary>
		// Token: 0x04001173 RID: 4467
		A = 65,
		/// <summary>The B key.</summary>
		// Token: 0x04001174 RID: 4468
		B = 66,
		/// <summary>The C key.</summary>
		// Token: 0x04001175 RID: 4469
		C = 67,
		/// <summary>The D key.</summary>
		// Token: 0x04001176 RID: 4470
		D = 68,
		/// <summary>The E key.</summary>
		// Token: 0x04001177 RID: 4471
		E = 69,
		/// <summary>The F key.</summary>
		// Token: 0x04001178 RID: 4472
		F = 70,
		/// <summary>The G key.</summary>
		// Token: 0x04001179 RID: 4473
		G = 71,
		/// <summary>The H key.</summary>
		// Token: 0x0400117A RID: 4474
		H = 72,
		/// <summary>The I key.</summary>
		// Token: 0x0400117B RID: 4475
		I = 73,
		/// <summary>The J key.</summary>
		// Token: 0x0400117C RID: 4476
		J = 74,
		/// <summary>The K key.</summary>
		// Token: 0x0400117D RID: 4477
		K = 75,
		/// <summary>The L key.</summary>
		// Token: 0x0400117E RID: 4478
		L = 76,
		/// <summary>The M key.</summary>
		// Token: 0x0400117F RID: 4479
		M = 77,
		/// <summary>The N key.</summary>
		// Token: 0x04001180 RID: 4480
		N = 78,
		/// <summary>The O key.</summary>
		// Token: 0x04001181 RID: 4481
		O = 79,
		/// <summary>The P key.</summary>
		// Token: 0x04001182 RID: 4482
		P = 80,
		/// <summary>The Q key.</summary>
		// Token: 0x04001183 RID: 4483
		Q = 81,
		/// <summary>The R key.</summary>
		// Token: 0x04001184 RID: 4484
		R = 82,
		/// <summary>The S key.</summary>
		// Token: 0x04001185 RID: 4485
		S = 83,
		/// <summary>The T key.</summary>
		// Token: 0x04001186 RID: 4486
		T = 84,
		/// <summary>The U key.</summary>
		// Token: 0x04001187 RID: 4487
		U = 85,
		/// <summary>The V key.</summary>
		// Token: 0x04001188 RID: 4488
		V = 86,
		/// <summary>The W key.</summary>
		// Token: 0x04001189 RID: 4489
		W = 87,
		/// <summary>The X key.</summary>
		// Token: 0x0400118A RID: 4490
		X = 88,
		/// <summary>The Y key.</summary>
		// Token: 0x0400118B RID: 4491
		Y = 89,
		/// <summary>The Z key.</summary>
		// Token: 0x0400118C RID: 4492
		Z = 90,
		/// <summary>The left Windows logo key (Microsoft Natural Keyboard).</summary>
		// Token: 0x0400118D RID: 4493
		LWin = 91,
		/// <summary>The right Windows logo key (Microsoft Natural Keyboard).</summary>
		// Token: 0x0400118E RID: 4494
		RWin = 92,
		/// <summary>The application key (Microsoft Natural Keyboard).</summary>
		// Token: 0x0400118F RID: 4495
		Apps = 93,
		/// <summary>The computer sleep key.</summary>
		// Token: 0x04001190 RID: 4496
		Sleep = 95,
		/// <summary>The 0 key on the numeric keypad.</summary>
		// Token: 0x04001191 RID: 4497
		NumPad0 = 96,
		/// <summary>The 1 key on the numeric keypad.</summary>
		// Token: 0x04001192 RID: 4498
		NumPad1 = 97,
		/// <summary>The 2 key on the numeric keypad.</summary>
		// Token: 0x04001193 RID: 4499
		NumPad2 = 98,
		/// <summary>The 3 key on the numeric keypad.</summary>
		// Token: 0x04001194 RID: 4500
		NumPad3 = 99,
		/// <summary>The 4 key on the numeric keypad.</summary>
		// Token: 0x04001195 RID: 4501
		NumPad4 = 100,
		/// <summary>The 5 key on the numeric keypad.</summary>
		// Token: 0x04001196 RID: 4502
		NumPad5 = 101,
		/// <summary>The 6 key on the numeric keypad.</summary>
		// Token: 0x04001197 RID: 4503
		NumPad6 = 102,
		/// <summary>The 7 key on the numeric keypad.</summary>
		// Token: 0x04001198 RID: 4504
		NumPad7 = 103,
		/// <summary>The 8 key on the numeric keypad.</summary>
		// Token: 0x04001199 RID: 4505
		NumPad8 = 104,
		/// <summary>The 9 key on the numeric keypad.</summary>
		// Token: 0x0400119A RID: 4506
		NumPad9 = 105,
		/// <summary>The multiply key.</summary>
		// Token: 0x0400119B RID: 4507
		Multiply = 106,
		/// <summary>The add key.</summary>
		// Token: 0x0400119C RID: 4508
		Add = 107,
		/// <summary>The separator key.</summary>
		// Token: 0x0400119D RID: 4509
		Separator = 108,
		/// <summary>The subtract key.</summary>
		// Token: 0x0400119E RID: 4510
		Subtract = 109,
		/// <summary>The decimal key.</summary>
		// Token: 0x0400119F RID: 4511
		Decimal = 110,
		/// <summary>The divide key.</summary>
		// Token: 0x040011A0 RID: 4512
		Divide = 111,
		/// <summary>The F1 key.</summary>
		// Token: 0x040011A1 RID: 4513
		F1 = 112,
		/// <summary>The F2 key.</summary>
		// Token: 0x040011A2 RID: 4514
		F2 = 113,
		/// <summary>The F3 key.</summary>
		// Token: 0x040011A3 RID: 4515
		F3 = 114,
		/// <summary>The F4 key.</summary>
		// Token: 0x040011A4 RID: 4516
		F4 = 115,
		/// <summary>The F5 key.</summary>
		// Token: 0x040011A5 RID: 4517
		F5 = 116,
		/// <summary>The F6 key.</summary>
		// Token: 0x040011A6 RID: 4518
		F6 = 117,
		/// <summary>The F7 key.</summary>
		// Token: 0x040011A7 RID: 4519
		F7 = 118,
		/// <summary>The F8 key.</summary>
		// Token: 0x040011A8 RID: 4520
		F8 = 119,
		/// <summary>The F9 key.</summary>
		// Token: 0x040011A9 RID: 4521
		F9 = 120,
		/// <summary>The F10 key.</summary>
		// Token: 0x040011AA RID: 4522
		F10 = 121,
		/// <summary>The F11 key.</summary>
		// Token: 0x040011AB RID: 4523
		F11 = 122,
		/// <summary>The F12 key.</summary>
		// Token: 0x040011AC RID: 4524
		F12 = 123,
		/// <summary>The F13 key.</summary>
		// Token: 0x040011AD RID: 4525
		F13 = 124,
		/// <summary>The F14 key.</summary>
		// Token: 0x040011AE RID: 4526
		F14 = 125,
		/// <summary>The F15 key.</summary>
		// Token: 0x040011AF RID: 4527
		F15 = 126,
		/// <summary>The F16 key.</summary>
		// Token: 0x040011B0 RID: 4528
		F16 = 127,
		/// <summary>The F17 key.</summary>
		// Token: 0x040011B1 RID: 4529
		F17 = 128,
		/// <summary>The F18 key.</summary>
		// Token: 0x040011B2 RID: 4530
		F18 = 129,
		/// <summary>The F19 key.</summary>
		// Token: 0x040011B3 RID: 4531
		F19 = 130,
		/// <summary>The F20 key.</summary>
		// Token: 0x040011B4 RID: 4532
		F20 = 131,
		/// <summary>The F21 key.</summary>
		// Token: 0x040011B5 RID: 4533
		F21 = 132,
		/// <summary>The F22 key.</summary>
		// Token: 0x040011B6 RID: 4534
		F22 = 133,
		/// <summary>The F23 key.</summary>
		// Token: 0x040011B7 RID: 4535
		F23 = 134,
		/// <summary>The F24 key.</summary>
		// Token: 0x040011B8 RID: 4536
		F24 = 135,
		/// <summary>The NUM LOCK key.</summary>
		// Token: 0x040011B9 RID: 4537
		NumLock = 144,
		/// <summary>The SCROLL LOCK key.</summary>
		// Token: 0x040011BA RID: 4538
		Scroll = 145,
		/// <summary>The left SHIFT key.</summary>
		// Token: 0x040011BB RID: 4539
		LShiftKey = 160,
		/// <summary>The right SHIFT key.</summary>
		// Token: 0x040011BC RID: 4540
		RShiftKey = 161,
		/// <summary>The left CTRL key.</summary>
		// Token: 0x040011BD RID: 4541
		LControlKey = 162,
		/// <summary>The right CTRL key.</summary>
		// Token: 0x040011BE RID: 4542
		RControlKey = 163,
		/// <summary>The left ALT key.</summary>
		// Token: 0x040011BF RID: 4543
		LMenu = 164,
		/// <summary>The right ALT key.</summary>
		// Token: 0x040011C0 RID: 4544
		RMenu = 165,
		/// <summary>The browser back key (Windows 2000 or later).</summary>
		// Token: 0x040011C1 RID: 4545
		BrowserBack = 166,
		/// <summary>The browser forward key (Windows 2000 or later).</summary>
		// Token: 0x040011C2 RID: 4546
		BrowserForward = 167,
		/// <summary>The browser refresh key (Windows 2000 or later).</summary>
		// Token: 0x040011C3 RID: 4547
		BrowserRefresh = 168,
		/// <summary>The browser stop key (Windows 2000 or later).</summary>
		// Token: 0x040011C4 RID: 4548
		BrowserStop = 169,
		/// <summary>The browser search key (Windows 2000 or later).</summary>
		// Token: 0x040011C5 RID: 4549
		BrowserSearch = 170,
		/// <summary>The browser favorites key (Windows 2000 or later).</summary>
		// Token: 0x040011C6 RID: 4550
		BrowserFavorites = 171,
		/// <summary>The browser home key (Windows 2000 or later).</summary>
		// Token: 0x040011C7 RID: 4551
		BrowserHome = 172,
		/// <summary>The volume mute key (Windows 2000 or later).</summary>
		// Token: 0x040011C8 RID: 4552
		VolumeMute = 173,
		/// <summary>The volume down key (Windows 2000 or later).</summary>
		// Token: 0x040011C9 RID: 4553
		VolumeDown = 174,
		/// <summary>The volume up key (Windows 2000 or later).</summary>
		// Token: 0x040011CA RID: 4554
		VolumeUp = 175,
		/// <summary>The media next track key (Windows 2000 or later).</summary>
		// Token: 0x040011CB RID: 4555
		MediaNextTrack = 176,
		/// <summary>The media previous track key (Windows 2000 or later).</summary>
		// Token: 0x040011CC RID: 4556
		MediaPreviousTrack = 177,
		/// <summary>The media Stop key (Windows 2000 or later).</summary>
		// Token: 0x040011CD RID: 4557
		MediaStop = 178,
		/// <summary>The media play pause key (Windows 2000 or later).</summary>
		// Token: 0x040011CE RID: 4558
		MediaPlayPause = 179,
		/// <summary>The launch mail key (Windows 2000 or later).</summary>
		// Token: 0x040011CF RID: 4559
		LaunchMail = 180,
		/// <summary>The select media key (Windows 2000 or later).</summary>
		// Token: 0x040011D0 RID: 4560
		SelectMedia = 181,
		/// <summary>The start application one key (Windows 2000 or later).</summary>
		// Token: 0x040011D1 RID: 4561
		LaunchApplication1 = 182,
		/// <summary>The start application two key (Windows 2000 or later).</summary>
		// Token: 0x040011D2 RID: 4562
		LaunchApplication2 = 183,
		/// <summary>The OEM Semicolon key on a US standard keyboard (Windows 2000 or later).</summary>
		// Token: 0x040011D3 RID: 4563
		OemSemicolon = 186,
		/// <summary>The OEM 1 key.</summary>
		// Token: 0x040011D4 RID: 4564
		Oem1 = 186,
		/// <summary>The OEM plus key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x040011D5 RID: 4565
		Oemplus = 187,
		/// <summary>The OEM comma key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x040011D6 RID: 4566
		Oemcomma = 188,
		/// <summary>The OEM minus key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x040011D7 RID: 4567
		OemMinus = 189,
		/// <summary>The OEM period key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x040011D8 RID: 4568
		OemPeriod = 190,
		/// <summary>The OEM question mark key on a US standard keyboard (Windows 2000 or later).</summary>
		// Token: 0x040011D9 RID: 4569
		OemQuestion = 191,
		/// <summary>The OEM 2 key.</summary>
		// Token: 0x040011DA RID: 4570
		Oem2 = 191,
		/// <summary>The OEM tilde key on a US standard keyboard (Windows 2000 or later).</summary>
		// Token: 0x040011DB RID: 4571
		Oemtilde = 192,
		/// <summary>The OEM 3 key.</summary>
		// Token: 0x040011DC RID: 4572
		Oem3 = 192,
		/// <summary>The OEM open bracket key on a US standard keyboard (Windows 2000 or later).</summary>
		// Token: 0x040011DD RID: 4573
		OemOpenBrackets = 219,
		/// <summary>The OEM 4 key.</summary>
		// Token: 0x040011DE RID: 4574
		Oem4 = 219,
		/// <summary>The OEM pipe key on a US standard keyboard (Windows 2000 or later).</summary>
		// Token: 0x040011DF RID: 4575
		OemPipe = 220,
		/// <summary>The OEM 5 key.</summary>
		// Token: 0x040011E0 RID: 4576
		Oem5 = 220,
		/// <summary>The OEM close bracket key on a US standard keyboard (Windows 2000 or later).</summary>
		// Token: 0x040011E1 RID: 4577
		OemCloseBrackets = 221,
		/// <summary>The OEM 6 key.</summary>
		// Token: 0x040011E2 RID: 4578
		Oem6 = 221,
		/// <summary>The OEM singled/double quote key on a US standard keyboard (Windows 2000 or later).</summary>
		// Token: 0x040011E3 RID: 4579
		OemQuotes = 222,
		/// <summary>The OEM 7 key.</summary>
		// Token: 0x040011E4 RID: 4580
		Oem7 = 222,
		/// <summary>The OEM 8 key.</summary>
		// Token: 0x040011E5 RID: 4581
		Oem8 = 223,
		/// <summary>The OEM angle bracket or backslash key on the RT 102 key keyboard (Windows 2000 or later).</summary>
		// Token: 0x040011E6 RID: 4582
		OemBackslash = 226,
		/// <summary>The OEM 102 key.</summary>
		// Token: 0x040011E7 RID: 4583
		Oem102 = 226,
		/// <summary>The PROCESS KEY key.</summary>
		// Token: 0x040011E8 RID: 4584
		ProcessKey = 229,
		/// <summary>Used to pass Unicode characters as if they were keystrokes. The Packet key value is the low word of a 32-bit virtual-key value used for non-keyboard input methods.</summary>
		// Token: 0x040011E9 RID: 4585
		Packet = 231,
		/// <summary>The ATTN key.</summary>
		// Token: 0x040011EA RID: 4586
		Attn = 246,
		/// <summary>The CRSEL key.</summary>
		// Token: 0x040011EB RID: 4587
		Crsel = 247,
		/// <summary>The EXSEL key.</summary>
		// Token: 0x040011EC RID: 4588
		Exsel = 248,
		/// <summary>The ERASE EOF key.</summary>
		// Token: 0x040011ED RID: 4589
		EraseEof = 249,
		/// <summary>The PLAY key.</summary>
		// Token: 0x040011EE RID: 4590
		Play = 250,
		/// <summary>The ZOOM key.</summary>
		// Token: 0x040011EF RID: 4591
		Zoom = 251,
		/// <summary>A constant reserved for future use.</summary>
		// Token: 0x040011F0 RID: 4592
		NoName = 252,
		/// <summary>The PA1 key.</summary>
		// Token: 0x040011F1 RID: 4593
		Pa1 = 253,
		/// <summary>The CLEAR key.</summary>
		// Token: 0x040011F2 RID: 4594
		OemClear = 254,
		/// <summary>The SHIFT modifier key.</summary>
		// Token: 0x040011F3 RID: 4595
		Shift = 65536,
		/// <summary>The CTRL modifier key.</summary>
		// Token: 0x040011F4 RID: 4596
		Control = 131072,
		/// <summary>The ALT modifier key.</summary>
		// Token: 0x040011F5 RID: 4597
		Alt = 262144
	}
}
