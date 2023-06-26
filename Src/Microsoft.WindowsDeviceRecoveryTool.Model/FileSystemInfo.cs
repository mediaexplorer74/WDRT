using System;
using System.IO;
using System.Security;
using System.Security.Permissions;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.Model
{
	// Token: 0x02000010 RID: 16
	public static class FileSystemInfo
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00002F7C File Offset: 0x0000117C
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00002F83 File Offset: 0x00001183
		public static string CustomPackagesPath { private get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00002F8C File Offset: 0x0000118C
		public static string AppNamePrefix
		{
			get
			{
				return "WDRT";
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00002FA4 File Offset: 0x000011A4
		public static string DefaultPackagesPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Microsoft\\Packages\\");
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00002FCC File Offset: 0x000011CC
		public static string DefaultProductsPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Microsoft\\Packages\\Products\\");
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00002FF4 File Offset: 0x000011F4
		public static string DefaultFfuPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Microsoft\\Packages\\");
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000096 RID: 150 RVA: 0x0000301C File Offset: 0x0000121C
		public static string NokiaProductsPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Nokia\\Packages\\Products\\");
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003044 File Offset: 0x00001244
		public static string NokiaPackagesPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Nokia\\Packages\\");
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000098 RID: 152 RVA: 0x0000306C File Offset: 0x0000126C
		public static string HtcPackagesPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "HTC\\Packages\\");
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003094 File Offset: 0x00001294
		public static string HtcProductsPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "HTC\\Packages\\Products\\");
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000030BC File Offset: 0x000012BC
		public static string LgePackagesPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "LGE\\Packages\\");
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000030E4 File Offset: 0x000012E4
		public static string BluPackagesPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "BLU\\Packages\\");
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600009C RID: 156 RVA: 0x0000310C File Offset: 0x0000130C
		public static string BluProductsPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "BLU\\Packages\\Products\\");
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003134 File Offset: 0x00001334
		public static string LgeProductsPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "LGE\\Packages\\Products\\");
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000315C File Offset: 0x0000135C
		public static string McjPackagesPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "MCJ\\Packages\\");
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003184 File Offset: 0x00001384
		public static string McjProductsPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "MCJ\\Packages\\Products\\");
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000031AC File Offset: 0x000013AC
		public static string AlcatelPackagesPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Alcatel\\Packages");
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000031D4 File Offset: 0x000013D4
		public static string AlcatelProductsPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Alcatel\\Packages\\Products\\");
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000031FC File Offset: 0x000013FC
		public static string AcerProductsPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Acer\\Packages\\Products\\");
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003224 File Offset: 0x00001424
		public static string TrinityProductsPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Trinity\\Packages\\Products\\");
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000324C File Offset: 0x0000144C
		public static string UnistrongProductsPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Unistrong\\Packages\\Products\\");
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003274 File Offset: 0x00001474
		public static string YEZZProductsPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "YEZZ\\Packages\\Products\\");
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x0000329C File Offset: 0x0000149C
		public static string DiginnosProductsPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Diginnos\\Packages\\Products\\");
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000032C4 File Offset: 0x000014C4
		public static string VAIOProductsPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "VAIO\\Packages\\Products\\");
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000032EC File Offset: 0x000014EC
		public static string InversenetProductsPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Inversenet\\Packages\\Products\\");
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003314 File Offset: 0x00001514
		public static string FreetelProductsPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Freetel\\Packages\\Products\\");
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000AA RID: 170 RVA: 0x0000333C File Offset: 0x0000153C
		public static string FunkerProductsPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Funker\\Packages\\Products\\");
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003364 File Offset: 0x00001564
		public static string MicromaxProductsPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Micromax\\Packages\\Products\\");
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000AC RID: 172 RVA: 0x0000338C File Offset: 0x0000158C
		public static string XOLOProductsPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "XOLO\\Packages\\Products\\");
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000033B4 File Offset: 0x000015B4
		public static string KMProductsPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "KM\\Packages\\Products\\");
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000033DC File Offset: 0x000015DC
		public static string JenesisProductsPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Jenesis\\Packages\\Products\\");
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003404 File Offset: 0x00001604
		public static string GomobileProductsPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Gomobile\\Packages\\Products\\");
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000342C File Offset: 0x0000162C
		public static string HPProductsPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "HP\\Packages\\Products\\");
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003454 File Offset: 0x00001654
		public static string LenovoProductsPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Lenovo\\Packages\\Products\\");
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x0000347C File Offset: 0x0000167C
		public static string ZebraProductsPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Zebra\\Packages\\Products\\");
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000034A4 File Offset: 0x000016A4
		public static string HoneywellProductPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Honeywell\\Packages\\Products\\");
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000034CC File Offset: 0x000016CC
		public static string PanasonicProductPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Panasonic\\Packages\\Products\\");
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000034F4 File Offset: 0x000016F4
		public static string TrekStorProductPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TrekStor\\Packages\\Products\\");
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000351C File Offset: 0x0000171C
		public static string WileyfoxProductsPath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Wilefox\\Packages\\Products\\");
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003544 File Offset: 0x00001744
		public static string AppPath
		{
			get
			{
				return AppDomain.CurrentDomain.BaseDirectory;
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003560 File Offset: 0x00001760
		public static string AppDataPath(SpecialFolder specialFolder)
		{
			string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Microsoft\\Care Suite\\Windows Device Recovery Tool");
			switch (specialFolder)
			{
			case SpecialFolder.Traces:
				text = Path.Combine(text, "Traces");
				break;
			case SpecialFolder.Reports:
				text = Path.Combine(text, "Reports");
				break;
			case SpecialFolder.Exports:
				text = Path.Combine(text, "Exports");
				break;
			case SpecialFolder.AppUpdate:
				text = Path.Combine(text, "Updates");
				break;
			case SpecialFolder.Configurations:
				text = Path.Combine(text, "Configuration");
				break;
			}
			text += "\\";
			bool flag = !Directory.Exists(text);
			if (flag)
			{
				Directory.CreateDirectory(text);
			}
			return text;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003614 File Offset: 0x00001814
		public static long GetDirectorySize(string directory)
		{
			bool flag = !Directory.Exists(directory);
			long num;
			if (flag)
			{
				num = 0L;
			}
			else
			{
				string[] files = Directory.GetFiles(Environment.ExpandEnvironmentVariables(directory), "*.*", SearchOption.AllDirectories);
				long num2 = 0L;
				foreach (string text in files)
				{
					FileInfo fileInfo = new FileInfo(text);
					try
					{
						num2 += fileInfo.Length;
					}
					catch (FileNotFoundException)
					{
					}
				}
				num = num2;
			}
			return num;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000369C File Offset: 0x0000189C
		public static bool CheckIfPathIsValid(string path)
		{
			bool flag = !Directory.Exists(path) || path.Contains("..");
			return !flag;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000036D0 File Offset: 0x000018D0
		public static bool CheckIfFileIsValid(string filePath)
		{
			bool flag = !File.Exists(filePath);
			return !flag;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000036F8 File Offset: 0x000018F8
		public static void CheckAndCreateFile(string filePath)
		{
			bool flag = !File.Exists(filePath);
			if (flag)
			{
				File.Create(filePath);
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000371C File Offset: 0x0000191C
		public static void CheckAndCreatePath(string path)
		{
			bool flag = !Directory.Exists(path);
			if (flag)
			{
				Directory.CreateDirectory(path);
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003740 File Offset: 0x00001940
		public static bool CheckDirectoryWritePermission(string path)
		{
			bool flag;
			try
			{
				using (File.Create(Path.Combine(FileSystemInfo.CustomPackagesPath, Path.GetRandomFileName()), 1, FileOptions.DeleteOnClose))
				{
				}
				flag = true;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000037A0 File Offset: 0x000019A0
		public static bool CheckPermission(string path)
		{
			bool flag = false;
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			FileIOPermission fileIOPermission = new FileIOPermission(FileIOPermissionAccess.Write, path);
			permissionSet.AddPermission(fileIOPermission);
			bool flag2 = permissionSet.IsSubsetOf(AppDomain.CurrentDomain.PermissionSet);
			if (flag2)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000037E8 File Offset: 0x000019E8
		public static string GetLumiaPackagesPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text;
			if (flag)
			{
				text = Path.Combine(FileSystemInfo.CustomPackagesPath, productPath);
			}
			else
			{
				text = Path.Combine(FileSystemInfo.DefaultPackagesPath, productPath);
			}
			return text;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003828 File Offset: 0x00001A28
		public static string GetLumiaProductsPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text2;
			if (flag)
			{
				string text = Path.Combine(FileSystemInfo.CustomPackagesPath, "Products");
				text2 = Path.Combine(text, productPath);
			}
			else
			{
				text2 = Path.Combine(FileSystemInfo.DefaultProductsPath, productPath);
			}
			return text2;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003874 File Offset: 0x00001A74
		public static string GetHtcPackagesPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text;
			if (flag)
			{
				text = Path.Combine(FileSystemInfo.CustomPackagesPath, productPath);
			}
			else
			{
				text = Path.Combine(FileSystemInfo.HtcPackagesPath, productPath);
			}
			return text;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000038B4 File Offset: 0x00001AB4
		public static string GetHtcProductsPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text2;
			if (flag)
			{
				string text = Path.Combine(FileSystemInfo.CustomPackagesPath, "Products");
				text2 = Path.Combine(text, productPath);
			}
			else
			{
				text2 = Path.Combine(FileSystemInfo.HtcProductsPath, productPath);
			}
			return text2;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003900 File Offset: 0x00001B00
		public static string GetLgePackagesPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text;
			if (flag)
			{
				text = Path.Combine(FileSystemInfo.CustomPackagesPath, productPath);
			}
			else
			{
				text = Path.Combine(FileSystemInfo.LgePackagesPath, productPath);
			}
			return text;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003940 File Offset: 0x00001B40
		public static string GetLgeProductsPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text2;
			if (flag)
			{
				string text = Path.Combine(FileSystemInfo.CustomPackagesPath, "Products");
				text2 = Path.Combine(text, productPath);
			}
			else
			{
				text2 = Path.Combine(FileSystemInfo.LgeProductsPath, productPath);
			}
			return text2;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000398C File Offset: 0x00001B8C
		public static string GetMcjPackagesPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text;
			if (flag)
			{
				text = Path.Combine(FileSystemInfo.CustomPackagesPath, productPath);
			}
			else
			{
				text = Path.Combine(FileSystemInfo.McjPackagesPath, productPath);
			}
			return text;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000039CC File Offset: 0x00001BCC
		public static string GetMcjProductsPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text2;
			if (flag)
			{
				string text = Path.Combine(FileSystemInfo.CustomPackagesPath, "Products");
				text2 = Path.Combine(text, productPath);
			}
			else
			{
				text2 = Path.Combine(FileSystemInfo.McjProductsPath, productPath);
			}
			return text2;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003A18 File Offset: 0x00001C18
		public static string GetBluProductsPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text2;
			if (flag)
			{
				string text = Path.Combine(FileSystemInfo.CustomPackagesPath, "Products");
				text2 = Path.Combine(text, productPath);
			}
			else
			{
				text2 = Path.Combine(FileSystemInfo.BluProductsPath, productPath);
			}
			return text2;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003A64 File Offset: 0x00001C64
		public static string GetAlcatelProductsPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text2;
			if (flag)
			{
				string text = Path.Combine(FileSystemInfo.CustomPackagesPath, "Products");
				text2 = Path.Combine(text, productPath);
			}
			else
			{
				text2 = Path.Combine(FileSystemInfo.AlcatelProductsPath, productPath);
			}
			return text2;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003AB0 File Offset: 0x00001CB0
		public static string GetAcerProductsPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text2;
			if (flag)
			{
				string text = Path.Combine(FileSystemInfo.CustomPackagesPath, "Products");
				text2 = Path.Combine(text, productPath);
			}
			else
			{
				text2 = Path.Combine(FileSystemInfo.AcerProductsPath, productPath);
			}
			return text2;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003AFC File Offset: 0x00001CFC
		public static string GetTrinityProductsPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text2;
			if (flag)
			{
				string text = Path.Combine(FileSystemInfo.CustomPackagesPath, "Products");
				text2 = Path.Combine(text, productPath);
			}
			else
			{
				text2 = Path.Combine(FileSystemInfo.TrinityProductsPath, productPath);
			}
			return text2;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003B48 File Offset: 0x00001D48
		public static string GetUnistrongProductsPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text2;
			if (flag)
			{
				string text = Path.Combine(FileSystemInfo.CustomPackagesPath, "Products");
				text2 = Path.Combine(text, productPath);
			}
			else
			{
				text2 = Path.Combine(FileSystemInfo.UnistrongProductsPath, productPath);
			}
			return text2;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003B94 File Offset: 0x00001D94
		public static string GetYEZZProductsPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text2;
			if (flag)
			{
				string text = Path.Combine(FileSystemInfo.CustomPackagesPath, "Products");
				text2 = Path.Combine(text, productPath);
			}
			else
			{
				text2 = Path.Combine(FileSystemInfo.YEZZProductsPath, productPath);
			}
			return text2;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003BE0 File Offset: 0x00001DE0
		public static string GetDiginnosProductsPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text2;
			if (flag)
			{
				string text = Path.Combine(FileSystemInfo.CustomPackagesPath, "Products");
				text2 = Path.Combine(text, productPath);
			}
			else
			{
				text2 = Path.Combine(FileSystemInfo.DiginnosProductsPath, productPath);
			}
			return text2;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003C2C File Offset: 0x00001E2C
		public static string GetVAIOProductsPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text2;
			if (flag)
			{
				string text = Path.Combine(FileSystemInfo.CustomPackagesPath, "Products");
				text2 = Path.Combine(text, productPath);
			}
			else
			{
				text2 = Path.Combine(FileSystemInfo.VAIOProductsPath, productPath);
			}
			return text2;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003C78 File Offset: 0x00001E78
		public static string GetInversenetProductsPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text2;
			if (flag)
			{
				string text = Path.Combine(FileSystemInfo.CustomPackagesPath, "Products");
				text2 = Path.Combine(text, productPath);
			}
			else
			{
				text2 = Path.Combine(FileSystemInfo.InversenetProductsPath, productPath);
			}
			return text2;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003CC4 File Offset: 0x00001EC4
		public static string GetFreetelProductsPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text2;
			if (flag)
			{
				string text = Path.Combine(FileSystemInfo.CustomPackagesPath, "Products");
				text2 = Path.Combine(text, productPath);
			}
			else
			{
				text2 = Path.Combine(FileSystemInfo.FreetelProductsPath, productPath);
			}
			return text2;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003D10 File Offset: 0x00001F10
		public static string GetFunkerProductsPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text2;
			if (flag)
			{
				string text = Path.Combine(FileSystemInfo.CustomPackagesPath, "Products");
				text2 = Path.Combine(text, productPath);
			}
			else
			{
				text2 = Path.Combine(FileSystemInfo.FunkerProductsPath, productPath);
			}
			return text2;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003D5C File Offset: 0x00001F5C
		public static string GetMicromaxProductsPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text2;
			if (flag)
			{
				string text = Path.Combine(FileSystemInfo.CustomPackagesPath, "Products");
				text2 = Path.Combine(text, productPath);
			}
			else
			{
				text2 = Path.Combine(FileSystemInfo.MicromaxProductsPath, productPath);
			}
			return text2;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003DA8 File Offset: 0x00001FA8
		public static string GetXOLOProductsPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text2;
			if (flag)
			{
				string text = Path.Combine(FileSystemInfo.CustomPackagesPath, "Products");
				text2 = Path.Combine(text, productPath);
			}
			else
			{
				text2 = Path.Combine(FileSystemInfo.XOLOProductsPath, productPath);
			}
			return text2;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003DF4 File Offset: 0x00001FF4
		public static string GetKMProductsPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text2;
			if (flag)
			{
				string text = Path.Combine(FileSystemInfo.CustomPackagesPath, "Products");
				text2 = Path.Combine(text, productPath);
			}
			else
			{
				text2 = Path.Combine(FileSystemInfo.KMProductsPath, productPath);
			}
			return text2;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003E40 File Offset: 0x00002040
		public static string GetJenesisProdcutsPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text2;
			if (flag)
			{
				string text = Path.Combine(FileSystemInfo.CustomPackagesPath, "Products");
				text2 = Path.Combine(text, productPath);
			}
			else
			{
				text2 = Path.Combine(FileSystemInfo.JenesisProductsPath, productPath);
			}
			return text2;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003E8C File Offset: 0x0000208C
		public static string GetGomobileProductsPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text2;
			if (flag)
			{
				string text = Path.Combine(FileSystemInfo.CustomPackagesPath, "Products");
				text2 = Path.Combine(text, productPath);
			}
			else
			{
				text2 = Path.Combine(FileSystemInfo.GomobileProductsPath, productPath);
			}
			return text2;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003ED8 File Offset: 0x000020D8
		public static string GetHPProductsPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text2;
			if (flag)
			{
				string text = Path.Combine(FileSystemInfo.CustomPackagesPath, "Products");
				text2 = Path.Combine(text, productPath);
			}
			else
			{
				text2 = Path.Combine(FileSystemInfo.HPProductsPath, productPath);
			}
			return text2;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003F24 File Offset: 0x00002124
		public static string GetLenovoProductsPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text2;
			if (flag)
			{
				string text = Path.Combine(FileSystemInfo.CustomPackagesPath, "Products");
				text2 = Path.Combine(text, productPath);
			}
			else
			{
				text2 = Path.Combine(FileSystemInfo.LenovoProductsPath, productPath);
			}
			return text2;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003F70 File Offset: 0x00002170
		public static string GetZebraProductsPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text2;
			if (flag)
			{
				string text = Path.Combine(FileSystemInfo.CustomPackagesPath, "Products");
				text2 = Path.Combine(text, productPath);
			}
			else
			{
				text2 = Path.Combine(FileSystemInfo.ZebraProductsPath, productPath);
			}
			return text2;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003FBC File Offset: 0x000021BC
		public static string GetHoneywellProductsPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text2;
			if (flag)
			{
				string text = Path.Combine(FileSystemInfo.CustomPackagesPath, "Products");
				text2 = Path.Combine(text, productPath);
			}
			else
			{
				text2 = Path.Combine(FileSystemInfo.HoneywellProductPath, productPath);
			}
			return text2;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004008 File Offset: 0x00002208
		public static string GetPanasonicProductsPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text2;
			if (flag)
			{
				string text = Path.Combine(FileSystemInfo.CustomPackagesPath, "Products");
				text2 = Path.Combine(text, productPath);
			}
			else
			{
				text2 = Path.Combine(FileSystemInfo.PanasonicProductPath, productPath);
			}
			return text2;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004054 File Offset: 0x00002254
		public static string GetTrekStorProductsPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text2;
			if (flag)
			{
				string text = Path.Combine(FileSystemInfo.CustomPackagesPath, "Products");
				text2 = Path.Combine(text, productPath);
			}
			else
			{
				text2 = Path.Combine(FileSystemInfo.TrekStorProductPath, productPath);
			}
			return text2;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000040A0 File Offset: 0x000022A0
		public static string GetWileyfoxProductsPath(string productPath = "")
		{
			bool flag = !string.IsNullOrWhiteSpace(FileSystemInfo.CustomPackagesPath);
			string text2;
			if (flag)
			{
				string text = Path.Combine(FileSystemInfo.CustomPackagesPath, "Products");
				text2 = Path.Combine(text, productPath);
			}
			else
			{
				text2 = Path.Combine(FileSystemInfo.WileyfoxProductsPath, productPath);
			}
			return text2;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000040EC File Offset: 0x000022EC
		public static string GetCustomProductsPath()
		{
			bool flag = !string.IsNullOrEmpty(FileSystemInfo.CustomPackagesPath);
			string text;
			if (flag)
			{
				text = Path.Combine(FileSystemInfo.CustomPackagesPath, "Products");
			}
			else
			{
				text = string.Empty;
			}
			return text;
		}
	}
}
