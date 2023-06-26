using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Annotations;
using System.Windows.Media;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x0200001A RID: 26
	internal sealed class Serializer
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x000043AD File Offset: 0x000025AD
		private Serializer()
		{
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000043B8 File Offset: 0x000025B8
		public static Color HexStringToColor(string value)
		{
			if (value.Length != 8)
			{
				throw new InvalidOperationException("Serializer.HexStringToColor requires input of a 8-character hexadecimal string, but received '" + value + "'.");
			}
			byte b = byte.Parse(value.Substring(0, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			byte b2 = byte.Parse(value.Substring(2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			byte b3 = byte.Parse(value.Substring(4, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			byte b4 = byte.Parse(value.Substring(6, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			return Color.FromArgb(b, b2, b3, b4);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000444C File Offset: 0x0000264C
		public static string ColorToHexString(Color color)
		{
			string text = color.A.ToString("X2", CultureInfo.InvariantCulture);
			string text2 = color.R.ToString("X2", CultureInfo.InvariantCulture);
			string text3 = color.G.ToString("X2", CultureInfo.InvariantCulture);
			string text4 = color.B.ToString("X2", CultureInfo.InvariantCulture);
			return text + text2 + text3 + text4;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000044C8 File Offset: 0x000026C8
		public static void Serialize(Serializer.Data data, Stream stream)
		{
			data.SchemaVersion = Serializer.Data.CurrentSchemaVersion;
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
			{
				Encoding = Encoding.UTF8,
				Indent = true
			};
			using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
			{
				new XmlSerializer(typeof(Serializer.Data)).Serialize(xmlWriter, data);
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004534 File Offset: 0x00002734
		public static Serializer.Data Deserialize(string filePath)
		{
			Serializer.Data data;
			using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
			{
				data = Serializer.Deserialize(fileStream);
			}
			return data;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004570 File Offset: 0x00002770
		public static Serializer.Data Deserialize(Stream stream)
		{
			Serializer.Data data;
			try
			{
				data = new XmlSerializer(typeof(Serializer.Data)).Deserialize(stream) as Serializer.Data;
			}
			catch (InvalidOperationException)
			{
				data = null;
			}
			return data;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000045B0 File Offset: 0x000027B0
		public static int? GetSchemaVersion(string filePath)
		{
			int? num3;
			using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
			{
				using (XmlReader xmlReader = XmlReader.Create(fileStream))
				{
					while (xmlReader.Read())
					{
						if (xmlReader.NodeType == XmlNodeType.Element && StringComparer.InvariantCultureIgnoreCase.Equals(xmlReader.LocalName, "Data"))
						{
							xmlReader.MoveToAttribute("SchemaVersion");
							break;
						}
					}
					int? num = null;
					int num2;
					if (!xmlReader.EOF && int.TryParse(xmlReader.Value, out num2))
					{
						num = new int?(num2);
					}
					num3 = num;
				}
			}
			return num3;
		}

		// Token: 0x02000057 RID: 87
		public class Data
		{
			// Token: 0x170000BD RID: 189
			// (get) Token: 0x060002FE RID: 766 RVA: 0x0000C81D File Offset: 0x0000AA1D
			// (set) Token: 0x060002FF RID: 767 RVA: 0x0000C825 File Offset: 0x0000AA25
			[XmlAttribute]
			public int SchemaVersion { get; set; }

			// Token: 0x170000BE RID: 190
			// (get) Token: 0x06000300 RID: 768 RVA: 0x0000C82E File Offset: 0x0000AA2E
			// (set) Token: 0x06000301 RID: 769 RVA: 0x0000C836 File Offset: 0x0000AA36
			public Guid SketchFlowGuid { get; set; }

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x06000302 RID: 770 RVA: 0x0000C83F File Offset: 0x0000AA3F
			// (set) Token: 0x06000303 RID: 771 RVA: 0x0000C847 File Offset: 0x0000AA47
			public string StartScreen { get; set; }

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x06000304 RID: 772 RVA: 0x0000C850 File Offset: 0x0000AA50
			// (set) Token: 0x06000305 RID: 773 RVA: 0x0000C858 File Offset: 0x0000AA58
			public List<Serializer.Data.Screen> Screens { get; set; }

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x06000306 RID: 774 RVA: 0x0000C861 File Offset: 0x0000AA61
			// (set) Token: 0x06000307 RID: 775 RVA: 0x0000C869 File Offset: 0x0000AA69
			public string SharePointDocumentLibrary { get; set; }

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x06000308 RID: 776 RVA: 0x0000C872 File Offset: 0x0000AA72
			// (set) Token: 0x06000309 RID: 777 RVA: 0x0000C87A File Offset: 0x0000AA7A
			public string SharePointProjectName { get; set; }

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x0600030A RID: 778 RVA: 0x0000C883 File Offset: 0x0000AA83
			// (set) Token: 0x0600030B RID: 779 RVA: 0x0000C88B File Offset: 0x0000AA8B
			public int PrototypeRevision { get; set; }

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x0600030C RID: 780 RVA: 0x0000C894 File Offset: 0x0000AA94
			// (set) Token: 0x0600030D RID: 781 RVA: 0x0000C89C File Offset: 0x0000AA9C
			public string BrandingText { get; set; }

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x0600030E RID: 782 RVA: 0x0000C8A5 File Offset: 0x0000AAA5
			// (set) Token: 0x0600030F RID: 783 RVA: 0x0000C8AD File Offset: 0x0000AAAD
			public Serializer.Data.RuntimeOptionsData RuntimeOptions { get; set; }

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x06000310 RID: 784 RVA: 0x0000C8B6 File Offset: 0x0000AAB6
			// (set) Token: 0x06000311 RID: 785 RVA: 0x0000C8BE File Offset: 0x0000AABE
			public List<Serializer.Data.VisualTag> VisualTags { get; set; }

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x06000312 RID: 786 RVA: 0x0000C8C7 File Offset: 0x0000AAC7
			// (set) Token: 0x06000313 RID: 787 RVA: 0x0000C8CF File Offset: 0x0000AACF
			public Serializer.Data.ViewStateData ViewState { get; set; }

			// Token: 0x06000314 RID: 788 RVA: 0x0000C8D8 File Offset: 0x0000AAD8
			public Data()
			{
				this.SchemaVersion = Serializer.Data.DefaultSchemaVersion;
				this.RuntimeOptions = new Serializer.Data.RuntimeOptionsData();
				this.ViewState = new Serializer.Data.ViewStateData();
				this.VisualTags = new List<Serializer.Data.VisualTag>();
				this.Screens = new List<Serializer.Data.Screen>();
			}

			// Token: 0x040000F3 RID: 243
			public static readonly int CurrentSchemaVersion = 2;

			// Token: 0x040000F4 RID: 244
			public static readonly int DefaultSchemaVersion = 1;

			// Token: 0x040000F5 RID: 245
			public static readonly int MinValidSchemaVersion = 1;

			// Token: 0x02000067 RID: 103
			public class RuntimeOptionsData
			{
				// Token: 0x170000DD RID: 221
				// (get) Token: 0x06000363 RID: 867 RVA: 0x0000CF28 File Offset: 0x0000B128
				// (set) Token: 0x06000364 RID: 868 RVA: 0x0000CF30 File Offset: 0x0000B130
				public bool HideNavigation { get; set; }

				// Token: 0x170000DE RID: 222
				// (get) Token: 0x06000365 RID: 869 RVA: 0x0000CF39 File Offset: 0x0000B139
				// (set) Token: 0x06000366 RID: 870 RVA: 0x0000CF41 File Offset: 0x0000B141
				public bool HideAnnotationAndInk { get; set; }

				// Token: 0x170000DF RID: 223
				// (get) Token: 0x06000367 RID: 871 RVA: 0x0000CF4A File Offset: 0x0000B14A
				// (set) Token: 0x06000368 RID: 872 RVA: 0x0000CF52 File Offset: 0x0000B152
				public bool DisableInking { get; set; }

				// Token: 0x170000E0 RID: 224
				// (get) Token: 0x06000369 RID: 873 RVA: 0x0000CF5B File Offset: 0x0000B15B
				// (set) Token: 0x0600036A RID: 874 RVA: 0x0000CF63 File Offset: 0x0000B163
				public bool HideDesignTimeAnnotations { get; set; }

				// Token: 0x170000E1 RID: 225
				// (get) Token: 0x0600036B RID: 875 RVA: 0x0000CF6C File Offset: 0x0000B16C
				// (set) Token: 0x0600036C RID: 876 RVA: 0x0000CF74 File Offset: 0x0000B174
				public bool ShowDesignTimeAnnotationsAtStart { get; set; }
			}

			// Token: 0x02000068 RID: 104
			public class ViewStateData
			{
				// Token: 0x170000E2 RID: 226
				// (get) Token: 0x0600036E RID: 878 RVA: 0x0000CF85 File Offset: 0x0000B185
				// (set) Token: 0x0600036F RID: 879 RVA: 0x0000CF8D File Offset: 0x0000B18D
				public double Zoom { get; set; }

				// Token: 0x170000E3 RID: 227
				// (get) Token: 0x06000370 RID: 880 RVA: 0x0000CF96 File Offset: 0x0000B196
				// (set) Token: 0x06000371 RID: 881 RVA: 0x0000CF9E File Offset: 0x0000B19E
				public Point? Center { get; set; }
			}

			// Token: 0x02000069 RID: 105
			public class Screen
			{
				// Token: 0x170000E4 RID: 228
				// (get) Token: 0x06000373 RID: 883 RVA: 0x0000CFAF File Offset: 0x0000B1AF
				// (set) Token: 0x06000374 RID: 884 RVA: 0x0000CFB7 File Offset: 0x0000B1B7
				public ScreenType Type { get; set; }

				// Token: 0x170000E5 RID: 229
				// (get) Token: 0x06000375 RID: 885 RVA: 0x0000CFC0 File Offset: 0x0000B1C0
				// (set) Token: 0x06000376 RID: 886 RVA: 0x0000CFC8 File Offset: 0x0000B1C8
				public string ClassName { get; set; }

				// Token: 0x170000E6 RID: 230
				// (get) Token: 0x06000377 RID: 887 RVA: 0x0000CFD1 File Offset: 0x0000B1D1
				// (set) Token: 0x06000378 RID: 888 RVA: 0x0000CFD9 File Offset: 0x0000B1D9
				public string DisplayName { get; set; }

				// Token: 0x170000E7 RID: 231
				// (get) Token: 0x06000379 RID: 889 RVA: 0x0000CFE2 File Offset: 0x0000B1E2
				// (set) Token: 0x0600037A RID: 890 RVA: 0x0000CFEA File Offset: 0x0000B1EA
				public string FileName { get; set; }

				// Token: 0x170000E8 RID: 232
				// (get) Token: 0x0600037B RID: 891 RVA: 0x0000CFF3 File Offset: 0x0000B1F3
				// (set) Token: 0x0600037C RID: 892 RVA: 0x0000CFFB File Offset: 0x0000B1FB
				public List<Annotation> Annotations { get; set; }

				// Token: 0x170000E9 RID: 233
				// (get) Token: 0x0600037D RID: 893 RVA: 0x0000D004 File Offset: 0x0000B204
				// (set) Token: 0x0600037E RID: 894 RVA: 0x0000D00C File Offset: 0x0000B20C
				public Point Position { get; set; }

				// Token: 0x170000EA RID: 234
				// (get) Token: 0x0600037F RID: 895 RVA: 0x0000D015 File Offset: 0x0000B215
				// (set) Token: 0x06000380 RID: 896 RVA: 0x0000D01D File Offset: 0x0000B21D
				public int? VisualTag { get; set; }

				// Token: 0x06000381 RID: 897 RVA: 0x0000D026 File Offset: 0x0000B226
				public Screen()
				{
					this.Annotations = new List<Annotation>();
				}
			}

			// Token: 0x0200006A RID: 106
			public class VisualTag
			{
				// Token: 0x170000EB RID: 235
				// (get) Token: 0x06000382 RID: 898 RVA: 0x0000D039 File Offset: 0x0000B239
				// (set) Token: 0x06000383 RID: 899 RVA: 0x0000D041 File Offset: 0x0000B241
				public string Name { get; set; }

				// Token: 0x170000EC RID: 236
				// (get) Token: 0x06000384 RID: 900 RVA: 0x0000D04A File Offset: 0x0000B24A
				// (set) Token: 0x06000385 RID: 901 RVA: 0x0000D052 File Offset: 0x0000B252
				public string Color { get; set; }
			}
		}
	}
}
