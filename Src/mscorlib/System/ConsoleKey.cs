using System;

namespace System
{
	/// <summary>Specifies the standard keys on a console.</summary>
	// Token: 0x020000C6 RID: 198
	[Serializable]
	public enum ConsoleKey
	{
		/// <summary>The BACKSPACE key.</summary>
		// Token: 0x04000497 RID: 1175
		Backspace = 8,
		/// <summary>The TAB key.</summary>
		// Token: 0x04000498 RID: 1176
		Tab,
		/// <summary>The CLEAR key.</summary>
		// Token: 0x04000499 RID: 1177
		Clear = 12,
		/// <summary>The ENTER key.</summary>
		// Token: 0x0400049A RID: 1178
		Enter,
		/// <summary>The PAUSE key.</summary>
		// Token: 0x0400049B RID: 1179
		Pause = 19,
		/// <summary>The ESC (ESCAPE) key.</summary>
		// Token: 0x0400049C RID: 1180
		Escape = 27,
		/// <summary>The SPACEBAR key.</summary>
		// Token: 0x0400049D RID: 1181
		Spacebar = 32,
		/// <summary>The PAGE UP key.</summary>
		// Token: 0x0400049E RID: 1182
		PageUp,
		/// <summary>The PAGE DOWN key.</summary>
		// Token: 0x0400049F RID: 1183
		PageDown,
		/// <summary>The END key.</summary>
		// Token: 0x040004A0 RID: 1184
		End,
		/// <summary>The HOME key.</summary>
		// Token: 0x040004A1 RID: 1185
		Home,
		/// <summary>The LEFT ARROW key.</summary>
		// Token: 0x040004A2 RID: 1186
		LeftArrow,
		/// <summary>The UP ARROW key.</summary>
		// Token: 0x040004A3 RID: 1187
		UpArrow,
		/// <summary>The RIGHT ARROW key.</summary>
		// Token: 0x040004A4 RID: 1188
		RightArrow,
		/// <summary>The DOWN ARROW key.</summary>
		// Token: 0x040004A5 RID: 1189
		DownArrow,
		/// <summary>The SELECT key.</summary>
		// Token: 0x040004A6 RID: 1190
		Select,
		/// <summary>The PRINT key.</summary>
		// Token: 0x040004A7 RID: 1191
		Print,
		/// <summary>The EXECUTE key.</summary>
		// Token: 0x040004A8 RID: 1192
		Execute,
		/// <summary>The PRINT SCREEN key.</summary>
		// Token: 0x040004A9 RID: 1193
		PrintScreen,
		/// <summary>The INS (INSERT) key.</summary>
		// Token: 0x040004AA RID: 1194
		Insert,
		/// <summary>The DEL (DELETE) key.</summary>
		// Token: 0x040004AB RID: 1195
		Delete,
		/// <summary>The HELP key.</summary>
		// Token: 0x040004AC RID: 1196
		Help,
		/// <summary>The 0 key.</summary>
		// Token: 0x040004AD RID: 1197
		D0,
		/// <summary>The 1 key.</summary>
		// Token: 0x040004AE RID: 1198
		D1,
		/// <summary>The 2 key.</summary>
		// Token: 0x040004AF RID: 1199
		D2,
		/// <summary>The 3 key.</summary>
		// Token: 0x040004B0 RID: 1200
		D3,
		/// <summary>The 4 key.</summary>
		// Token: 0x040004B1 RID: 1201
		D4,
		/// <summary>The 5 key.</summary>
		// Token: 0x040004B2 RID: 1202
		D5,
		/// <summary>The 6 key.</summary>
		// Token: 0x040004B3 RID: 1203
		D6,
		/// <summary>The 7 key.</summary>
		// Token: 0x040004B4 RID: 1204
		D7,
		/// <summary>The 8 key.</summary>
		// Token: 0x040004B5 RID: 1205
		D8,
		/// <summary>The 9 key.</summary>
		// Token: 0x040004B6 RID: 1206
		D9,
		/// <summary>The A key.</summary>
		// Token: 0x040004B7 RID: 1207
		A = 65,
		/// <summary>The B key.</summary>
		// Token: 0x040004B8 RID: 1208
		B,
		/// <summary>The C key.</summary>
		// Token: 0x040004B9 RID: 1209
		C,
		/// <summary>The D key.</summary>
		// Token: 0x040004BA RID: 1210
		D,
		/// <summary>The E key.</summary>
		// Token: 0x040004BB RID: 1211
		E,
		/// <summary>The F key.</summary>
		// Token: 0x040004BC RID: 1212
		F,
		/// <summary>The G key.</summary>
		// Token: 0x040004BD RID: 1213
		G,
		/// <summary>The H key.</summary>
		// Token: 0x040004BE RID: 1214
		H,
		/// <summary>The I key.</summary>
		// Token: 0x040004BF RID: 1215
		I,
		/// <summary>The J key.</summary>
		// Token: 0x040004C0 RID: 1216
		J,
		/// <summary>The K key.</summary>
		// Token: 0x040004C1 RID: 1217
		K,
		/// <summary>The L key.</summary>
		// Token: 0x040004C2 RID: 1218
		L,
		/// <summary>The M key.</summary>
		// Token: 0x040004C3 RID: 1219
		M,
		/// <summary>The N key.</summary>
		// Token: 0x040004C4 RID: 1220
		N,
		/// <summary>The O key.</summary>
		// Token: 0x040004C5 RID: 1221
		O,
		/// <summary>The P key.</summary>
		// Token: 0x040004C6 RID: 1222
		P,
		/// <summary>The Q key.</summary>
		// Token: 0x040004C7 RID: 1223
		Q,
		/// <summary>The R key.</summary>
		// Token: 0x040004C8 RID: 1224
		R,
		/// <summary>The S key.</summary>
		// Token: 0x040004C9 RID: 1225
		S,
		/// <summary>The T key.</summary>
		// Token: 0x040004CA RID: 1226
		T,
		/// <summary>The U key.</summary>
		// Token: 0x040004CB RID: 1227
		U,
		/// <summary>The V key.</summary>
		// Token: 0x040004CC RID: 1228
		V,
		/// <summary>The W key.</summary>
		// Token: 0x040004CD RID: 1229
		W,
		/// <summary>The X key.</summary>
		// Token: 0x040004CE RID: 1230
		X,
		/// <summary>The Y key.</summary>
		// Token: 0x040004CF RID: 1231
		Y,
		/// <summary>The Z key.</summary>
		// Token: 0x040004D0 RID: 1232
		Z,
		/// <summary>The left Windows logo key (Microsoft Natural Keyboard).</summary>
		// Token: 0x040004D1 RID: 1233
		LeftWindows,
		/// <summary>The right Windows logo key (Microsoft Natural Keyboard).</summary>
		// Token: 0x040004D2 RID: 1234
		RightWindows,
		/// <summary>The Application key (Microsoft Natural Keyboard).</summary>
		// Token: 0x040004D3 RID: 1235
		Applications,
		/// <summary>The Computer Sleep key.</summary>
		// Token: 0x040004D4 RID: 1236
		Sleep = 95,
		/// <summary>The 0 key on the numeric keypad.</summary>
		// Token: 0x040004D5 RID: 1237
		NumPad0,
		/// <summary>The 1 key on the numeric keypad.</summary>
		// Token: 0x040004D6 RID: 1238
		NumPad1,
		/// <summary>The 2 key on the numeric keypad.</summary>
		// Token: 0x040004D7 RID: 1239
		NumPad2,
		/// <summary>The 3 key on the numeric keypad.</summary>
		// Token: 0x040004D8 RID: 1240
		NumPad3,
		/// <summary>The 4 key on the numeric keypad.</summary>
		// Token: 0x040004D9 RID: 1241
		NumPad4,
		/// <summary>The 5 key on the numeric keypad.</summary>
		// Token: 0x040004DA RID: 1242
		NumPad5,
		/// <summary>The 6 key on the numeric keypad.</summary>
		// Token: 0x040004DB RID: 1243
		NumPad6,
		/// <summary>The 7 key on the numeric keypad.</summary>
		// Token: 0x040004DC RID: 1244
		NumPad7,
		/// <summary>The 8 key on the numeric keypad.</summary>
		// Token: 0x040004DD RID: 1245
		NumPad8,
		/// <summary>The 9 key on the numeric keypad.</summary>
		// Token: 0x040004DE RID: 1246
		NumPad9,
		/// <summary>The Multiply key (the multiplication key on the numeric keypad).</summary>
		// Token: 0x040004DF RID: 1247
		Multiply,
		/// <summary>The Add key (the addition key on the numeric keypad).</summary>
		// Token: 0x040004E0 RID: 1248
		Add,
		/// <summary>The Separator key.</summary>
		// Token: 0x040004E1 RID: 1249
		Separator,
		/// <summary>The Subtract key (the subtraction key on the numeric keypad).</summary>
		// Token: 0x040004E2 RID: 1250
		Subtract,
		/// <summary>The Decimal key (the decimal key on the numeric keypad).</summary>
		// Token: 0x040004E3 RID: 1251
		Decimal,
		/// <summary>The Divide key (the division key on the numeric keypad).</summary>
		// Token: 0x040004E4 RID: 1252
		Divide,
		/// <summary>The F1 key.</summary>
		// Token: 0x040004E5 RID: 1253
		F1,
		/// <summary>The F2 key.</summary>
		// Token: 0x040004E6 RID: 1254
		F2,
		/// <summary>The F3 key.</summary>
		// Token: 0x040004E7 RID: 1255
		F3,
		/// <summary>The F4 key.</summary>
		// Token: 0x040004E8 RID: 1256
		F4,
		/// <summary>The F5 key.</summary>
		// Token: 0x040004E9 RID: 1257
		F5,
		/// <summary>The F6 key.</summary>
		// Token: 0x040004EA RID: 1258
		F6,
		/// <summary>The F7 key.</summary>
		// Token: 0x040004EB RID: 1259
		F7,
		/// <summary>The F8 key.</summary>
		// Token: 0x040004EC RID: 1260
		F8,
		/// <summary>The F9 key.</summary>
		// Token: 0x040004ED RID: 1261
		F9,
		/// <summary>The F10 key.</summary>
		// Token: 0x040004EE RID: 1262
		F10,
		/// <summary>The F11 key.</summary>
		// Token: 0x040004EF RID: 1263
		F11,
		/// <summary>The F12 key.</summary>
		// Token: 0x040004F0 RID: 1264
		F12,
		/// <summary>The F13 key.</summary>
		// Token: 0x040004F1 RID: 1265
		F13,
		/// <summary>The F14 key.</summary>
		// Token: 0x040004F2 RID: 1266
		F14,
		/// <summary>The F15 key.</summary>
		// Token: 0x040004F3 RID: 1267
		F15,
		/// <summary>The F16 key.</summary>
		// Token: 0x040004F4 RID: 1268
		F16,
		/// <summary>The F17 key.</summary>
		// Token: 0x040004F5 RID: 1269
		F17,
		/// <summary>The F18 key.</summary>
		// Token: 0x040004F6 RID: 1270
		F18,
		/// <summary>The F19 key.</summary>
		// Token: 0x040004F7 RID: 1271
		F19,
		/// <summary>The F20 key.</summary>
		// Token: 0x040004F8 RID: 1272
		F20,
		/// <summary>The F21 key.</summary>
		// Token: 0x040004F9 RID: 1273
		F21,
		/// <summary>The F22 key.</summary>
		// Token: 0x040004FA RID: 1274
		F22,
		/// <summary>The F23 key.</summary>
		// Token: 0x040004FB RID: 1275
		F23,
		/// <summary>The F24 key.</summary>
		// Token: 0x040004FC RID: 1276
		F24,
		/// <summary>The Browser Back key (Windows 2000 or later).</summary>
		// Token: 0x040004FD RID: 1277
		BrowserBack = 166,
		/// <summary>The Browser Forward key (Windows 2000 or later).</summary>
		// Token: 0x040004FE RID: 1278
		BrowserForward,
		/// <summary>The Browser Refresh key (Windows 2000 or later).</summary>
		// Token: 0x040004FF RID: 1279
		BrowserRefresh,
		/// <summary>The Browser Stop key (Windows 2000 or later).</summary>
		// Token: 0x04000500 RID: 1280
		BrowserStop,
		/// <summary>The Browser Search key (Windows 2000 or later).</summary>
		// Token: 0x04000501 RID: 1281
		BrowserSearch,
		/// <summary>The Browser Favorites key (Windows 2000 or later).</summary>
		// Token: 0x04000502 RID: 1282
		BrowserFavorites,
		/// <summary>The Browser Home key (Windows 2000 or later).</summary>
		// Token: 0x04000503 RID: 1283
		BrowserHome,
		/// <summary>The Volume Mute key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x04000504 RID: 1284
		VolumeMute,
		/// <summary>The Volume Down key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x04000505 RID: 1285
		VolumeDown,
		/// <summary>The Volume Up key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x04000506 RID: 1286
		VolumeUp,
		/// <summary>The Media Next Track key (Windows 2000 or later).</summary>
		// Token: 0x04000507 RID: 1287
		MediaNext,
		/// <summary>The Media Previous Track key (Windows 2000 or later).</summary>
		// Token: 0x04000508 RID: 1288
		MediaPrevious,
		/// <summary>The Media Stop key (Windows 2000 or later).</summary>
		// Token: 0x04000509 RID: 1289
		MediaStop,
		/// <summary>The Media Play/Pause key (Windows 2000 or later).</summary>
		// Token: 0x0400050A RID: 1290
		MediaPlay,
		/// <summary>The Start Mail key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x0400050B RID: 1291
		LaunchMail,
		/// <summary>The Select Media key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x0400050C RID: 1292
		LaunchMediaSelect,
		/// <summary>The Start Application 1 key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x0400050D RID: 1293
		LaunchApp1,
		/// <summary>The Start Application 2 key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x0400050E RID: 1294
		LaunchApp2,
		/// <summary>The OEM 1 key (OEM specific).</summary>
		// Token: 0x0400050F RID: 1295
		Oem1 = 186,
		/// <summary>The OEM Plus key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x04000510 RID: 1296
		OemPlus,
		/// <summary>The OEM Comma key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x04000511 RID: 1297
		OemComma,
		/// <summary>The OEM Minus key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x04000512 RID: 1298
		OemMinus,
		/// <summary>The OEM Period key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x04000513 RID: 1299
		OemPeriod,
		/// <summary>The OEM 2 key (OEM specific).</summary>
		// Token: 0x04000514 RID: 1300
		Oem2,
		/// <summary>The OEM 3 key (OEM specific).</summary>
		// Token: 0x04000515 RID: 1301
		Oem3,
		/// <summary>The OEM 4 key (OEM specific).</summary>
		// Token: 0x04000516 RID: 1302
		Oem4 = 219,
		/// <summary>The OEM 5 (OEM specific).</summary>
		// Token: 0x04000517 RID: 1303
		Oem5,
		/// <summary>The OEM 6 key (OEM specific).</summary>
		// Token: 0x04000518 RID: 1304
		Oem6,
		/// <summary>The OEM 7 key (OEM specific).</summary>
		// Token: 0x04000519 RID: 1305
		Oem7,
		/// <summary>The OEM 8 key (OEM specific).</summary>
		// Token: 0x0400051A RID: 1306
		Oem8,
		/// <summary>The OEM 102 key (OEM specific).</summary>
		// Token: 0x0400051B RID: 1307
		Oem102 = 226,
		/// <summary>The IME PROCESS key.</summary>
		// Token: 0x0400051C RID: 1308
		Process = 229,
		/// <summary>The PACKET key (used to pass Unicode characters with keystrokes).</summary>
		// Token: 0x0400051D RID: 1309
		Packet = 231,
		/// <summary>The ATTN key.</summary>
		// Token: 0x0400051E RID: 1310
		Attention = 246,
		/// <summary>The CRSEL (CURSOR SELECT) key.</summary>
		// Token: 0x0400051F RID: 1311
		CrSel,
		/// <summary>The EXSEL (EXTEND SELECTION) key.</summary>
		// Token: 0x04000520 RID: 1312
		ExSel,
		/// <summary>The ERASE EOF key.</summary>
		// Token: 0x04000521 RID: 1313
		EraseEndOfFile,
		/// <summary>The PLAY key.</summary>
		// Token: 0x04000522 RID: 1314
		Play,
		/// <summary>The ZOOM key.</summary>
		// Token: 0x04000523 RID: 1315
		Zoom,
		/// <summary>A constant reserved for future use.</summary>
		// Token: 0x04000524 RID: 1316
		NoName,
		/// <summary>The PA1 key.</summary>
		// Token: 0x04000525 RID: 1317
		Pa1,
		/// <summary>The CLEAR key (OEM specific).</summary>
		// Token: 0x04000526 RID: 1318
		OemClear
	}
}
