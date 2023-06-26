using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Provides <see langword="static" />, predefined <see cref="T:System.Windows.Forms.Clipboard" /> format names. Use them to identify the format of data that you store in an <see cref="T:System.Windows.Forms.IDataObject" />.</summary>
	// Token: 0x02000178 RID: 376
	public class DataFormats
	{
		// Token: 0x06001427 RID: 5159 RVA: 0x00002843 File Offset: 0x00000A43
		private DataFormats()
		{
		}

		/// <summary>Returns a <see cref="T:System.Windows.Forms.DataFormats.Format" /> with the Windows Clipboard numeric ID and name for the specified format.</summary>
		/// <param name="format">The format name.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.DataFormats.Format" /> that has the Windows Clipboard numeric ID and the name of the format.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">Registering a new <see cref="T:System.Windows.Forms.Clipboard" /> format failed.</exception>
		// Token: 0x06001428 RID: 5160 RVA: 0x00043B4C File Offset: 0x00041D4C
		public static DataFormats.Format GetFormat(string format)
		{
			object obj = DataFormats.internalSyncObject;
			DataFormats.Format format2;
			lock (obj)
			{
				DataFormats.EnsurePredefined();
				for (int i = 0; i < DataFormats.formatCount; i++)
				{
					if (DataFormats.formatList[i].Name.Equals(format))
					{
						return DataFormats.formatList[i];
					}
				}
				for (int j = 0; j < DataFormats.formatCount; j++)
				{
					if (string.Equals(DataFormats.formatList[j].Name, format, StringComparison.OrdinalIgnoreCase))
					{
						return DataFormats.formatList[j];
					}
				}
				int num = SafeNativeMethods.RegisterClipboardFormat(format);
				if (num == 0)
				{
					throw new Win32Exception(Marshal.GetLastWin32Error(), SR.GetString("RegisterCFFailed"));
				}
				DataFormats.EnsureFormatSpace(1);
				DataFormats.formatList[DataFormats.formatCount] = new DataFormats.Format(format, num);
				format2 = DataFormats.formatList[DataFormats.formatCount++];
			}
			return format2;
		}

		/// <summary>Returns a <see cref="T:System.Windows.Forms.DataFormats.Format" /> with the Windows Clipboard numeric ID and name for the specified ID.</summary>
		/// <param name="id">The format ID.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.DataFormats.Format" /> that has the Windows Clipboard numeric ID and the name of the format.</returns>
		// Token: 0x06001429 RID: 5161 RVA: 0x00043C40 File Offset: 0x00041E40
		public static DataFormats.Format GetFormat(int id)
		{
			return DataFormats.InternalGetFormat(null, (ushort)(id & 65535));
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x00043C50 File Offset: 0x00041E50
		private static DataFormats.Format InternalGetFormat(string strName, ushort id)
		{
			object obj = DataFormats.internalSyncObject;
			DataFormats.Format format;
			lock (obj)
			{
				DataFormats.EnsurePredefined();
				for (int i = 0; i < DataFormats.formatCount; i++)
				{
					if (DataFormats.formatList[i].Id == (int)id)
					{
						return DataFormats.formatList[i];
					}
				}
				StringBuilder stringBuilder = new StringBuilder(128);
				if (SafeNativeMethods.GetClipboardFormatName((int)id, stringBuilder, stringBuilder.Capacity) == 0)
				{
					stringBuilder.Length = 0;
					if (strName == null)
					{
						stringBuilder.Append("Format").Append(id);
					}
					else
					{
						stringBuilder.Append(strName);
					}
				}
				DataFormats.EnsureFormatSpace(1);
				DataFormats.formatList[DataFormats.formatCount] = new DataFormats.Format(stringBuilder.ToString(), (int)id);
				format = DataFormats.formatList[DataFormats.formatCount++];
			}
			return format;
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x00043D30 File Offset: 0x00041F30
		private static void EnsureFormatSpace(int size)
		{
			if (DataFormats.formatList == null || DataFormats.formatList.Length <= DataFormats.formatCount + size)
			{
				int num = DataFormats.formatCount + 20;
				DataFormats.Format[] array = new DataFormats.Format[num];
				for (int i = 0; i < DataFormats.formatCount; i++)
				{
					array[i] = DataFormats.formatList[i];
				}
				DataFormats.formatList = array;
			}
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x00043D84 File Offset: 0x00041F84
		private static void EnsurePredefined()
		{
			if (DataFormats.formatCount == 0)
			{
				DataFormats.formatList = new DataFormats.Format[]
				{
					new DataFormats.Format(DataFormats.UnicodeText, 13),
					new DataFormats.Format(DataFormats.Text, 1),
					new DataFormats.Format(DataFormats.Bitmap, 2),
					new DataFormats.Format(DataFormats.MetafilePict, 3),
					new DataFormats.Format(DataFormats.EnhancedMetafile, 14),
					new DataFormats.Format(DataFormats.Dif, 5),
					new DataFormats.Format(DataFormats.Tiff, 6),
					new DataFormats.Format(DataFormats.OemText, 7),
					new DataFormats.Format(DataFormats.Dib, 8),
					new DataFormats.Format(DataFormats.Palette, 9),
					new DataFormats.Format(DataFormats.PenData, 10),
					new DataFormats.Format(DataFormats.Riff, 11),
					new DataFormats.Format(DataFormats.WaveAudio, 12),
					new DataFormats.Format(DataFormats.SymbolicLink, 4),
					new DataFormats.Format(DataFormats.FileDrop, 15),
					new DataFormats.Format(DataFormats.Locale, 16)
				};
				DataFormats.formatCount = DataFormats.formatList.Length;
			}
		}

		/// <summary>Specifies the standard ANSI text format. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000969 RID: 2409
		public static readonly string Text = "Text";

		/// <summary>Specifies the standard Windows Unicode text format. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x0400096A RID: 2410
		public static readonly string UnicodeText = "UnicodeText";

		/// <summary>Specifies the Windows device-independent bitmap (DIB) format. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x0400096B RID: 2411
		public static readonly string Dib = "DeviceIndependentBitmap";

		/// <summary>Specifies a Windows bitmap format. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x0400096C RID: 2412
		public static readonly string Bitmap = "Bitmap";

		/// <summary>Specifies the Windows enhanced metafile format. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x0400096D RID: 2413
		public static readonly string EnhancedMetafile = "EnhancedMetafile";

		/// <summary>Specifies the Windows metafile format, which Windows Forms does not directly use. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x0400096E RID: 2414
		public static readonly string MetafilePict = "MetaFilePict";

		/// <summary>Specifies the Windows symbolic link format, which Windows Forms does not directly use. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x0400096F RID: 2415
		public static readonly string SymbolicLink = "SymbolicLink";

		/// <summary>Specifies the Windows Data Interchange Format (DIF), which Windows Forms does not directly use. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000970 RID: 2416
		public static readonly string Dif = "DataInterchangeFormat";

		/// <summary>Specifies the Tagged Image File Format (TIFF), which Windows Forms does not directly use. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000971 RID: 2417
		public static readonly string Tiff = "TaggedImageFileFormat";

		/// <summary>Specifies the standard Windows original equipment manufacturer (OEM) text format. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000972 RID: 2418
		public static readonly string OemText = "OEMText";

		/// <summary>Specifies the Windows palette format. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000973 RID: 2419
		public static readonly string Palette = "Palette";

		/// <summary>Specifies the Windows pen data format, which consists of pen strokes for handwriting software; Windows Forms does not use this format. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000974 RID: 2420
		public static readonly string PenData = "PenData";

		/// <summary>Specifies the Resource Interchange File Format (RIFF) audio format, which Windows Forms does not directly use. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000975 RID: 2421
		public static readonly string Riff = "RiffAudio";

		/// <summary>Specifies the wave audio format, which Windows Forms does not directly use. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000976 RID: 2422
		public static readonly string WaveAudio = "WaveAudio";

		/// <summary>Specifies the Windows file drop format, which Windows Forms does not directly use. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000977 RID: 2423
		public static readonly string FileDrop = "FileDrop";

		/// <summary>Specifies the Windows culture format, which Windows Forms does not directly use. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000978 RID: 2424
		public static readonly string Locale = "Locale";

		/// <summary>Specifies text in the HTML Clipboard format. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000979 RID: 2425
		public static readonly string Html = "HTML Format";

		/// <summary>Specifies text consisting of Rich Text Format (RTF) data. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x0400097A RID: 2426
		public static readonly string Rtf = "Rich Text Format";

		/// <summary>Specifies a comma-separated value (CSV) format, which is a common interchange format used by spreadsheets. This format is not used directly by Windows Forms. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x0400097B RID: 2427
		public static readonly string CommaSeparatedValue = "Csv";

		/// <summary>Specifies the Windows Forms string class format, which Windows Forms uses to store string objects. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x0400097C RID: 2428
		public static readonly string StringFormat = typeof(string).FullName;

		/// <summary>Specifies a format that encapsulates any type of Windows Forms object. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x0400097D RID: 2429
		public static readonly string Serializable = Application.WindowsFormsVersion + "PersistentObject";

		// Token: 0x0400097E RID: 2430
		private static DataFormats.Format[] formatList;

		// Token: 0x0400097F RID: 2431
		private static int formatCount = 0;

		// Token: 0x04000980 RID: 2432
		private static object internalSyncObject = new object();

		/// <summary>Represents a Clipboard format type.</summary>
		// Token: 0x02000643 RID: 1603
		public class Format
		{
			/// <summary>Gets the name of this format.</summary>
			/// <returns>The name of this format.</returns>
			// Token: 0x17001593 RID: 5523
			// (get) Token: 0x060064A2 RID: 25762 RVA: 0x001767FE File Offset: 0x001749FE
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			/// <summary>Gets the ID number for this format.</summary>
			/// <returns>The ID number for this format.</returns>
			// Token: 0x17001594 RID: 5524
			// (get) Token: 0x060064A3 RID: 25763 RVA: 0x00176806 File Offset: 0x00174A06
			public int Id
			{
				get
				{
					return this.id;
				}
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataFormats.Format" /> class with a Boolean that indicates whether a <see langword="Win32" /> handle is expected.</summary>
			/// <param name="name">The name of this format.</param>
			/// <param name="id">The ID number for this format.</param>
			// Token: 0x060064A4 RID: 25764 RVA: 0x0017680E File Offset: 0x00174A0E
			public Format(string name, int id)
			{
				this.name = name;
				this.id = id;
			}

			// Token: 0x040039A5 RID: 14757
			private readonly string name;

			// Token: 0x040039A6 RID: 14758
			private readonly int id;
		}
	}
}
