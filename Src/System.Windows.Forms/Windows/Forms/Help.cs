using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Encapsulates the HTML Help 1.0 engine.</summary>
	// Token: 0x0200026F RID: 623
	public class Help
	{
		// Token: 0x060027FC RID: 10236 RVA: 0x00002843 File Offset: 0x00000A43
		private Help()
		{
		}

		/// <summary>Displays the contents of the Help file at the specified URL.</summary>
		/// <param name="parent">A <see cref="T:System.Windows.Forms.Control" /> that identifies the parent of the Help dialog box.</param>
		/// <param name="url">The path and name of the Help file.</param>
		// Token: 0x060027FD RID: 10237 RVA: 0x000BA0FA File Offset: 0x000B82FA
		public static void ShowHelp(Control parent, string url)
		{
			Help.ShowHelp(parent, url, HelpNavigator.TableOfContents, null);
		}

		/// <summary>Displays the contents of the Help file found at the specified URL for a specific topic.</summary>
		/// <param name="parent">A <see cref="T:System.Windows.Forms.Control" /> that identifies the parent of the Help dialog box.</param>
		/// <param name="url">The path and name of the Help file.</param>
		/// <param name="navigator">One of the <see cref="T:System.Windows.Forms.HelpNavigator" /> values.</param>
		// Token: 0x060027FE RID: 10238 RVA: 0x000BA109 File Offset: 0x000B8309
		public static void ShowHelp(Control parent, string url, HelpNavigator navigator)
		{
			Help.ShowHelp(parent, url, navigator, null);
		}

		/// <summary>Displays the contents of the Help file found at the specified URL for a specific keyword.</summary>
		/// <param name="parent">A <see cref="T:System.Windows.Forms.Control" /> that identifies the parent of the Help dialog box.</param>
		/// <param name="url">The path and name of the Help file.</param>
		/// <param name="keyword">The keyword to display Help for.</param>
		// Token: 0x060027FF RID: 10239 RVA: 0x000BA114 File Offset: 0x000B8314
		public static void ShowHelp(Control parent, string url, string keyword)
		{
			if (keyword != null && keyword.Length != 0)
			{
				Help.ShowHelp(parent, url, HelpNavigator.Topic, keyword);
				return;
			}
			Help.ShowHelp(parent, url, HelpNavigator.TableOfContents, null);
		}

		/// <summary>Displays the contents of the Help file located at the URL supplied by the user.</summary>
		/// <param name="parent">A <see cref="T:System.Windows.Forms.Control" /> that identifies the parent of the Help dialog box.</param>
		/// <param name="url">The path and name of the Help file.</param>
		/// <param name="command">One of the <see cref="T:System.Windows.Forms.HelpNavigator" /> values.</param>
		/// <param name="parameter">A string that contains the topic identifier.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="parameter" /> is an integer.</exception>
		// Token: 0x06002800 RID: 10240 RVA: 0x000BA13C File Offset: 0x000B833C
		public static void ShowHelp(Control parent, string url, HelpNavigator command, object parameter)
		{
			int helpFileType = Help.GetHelpFileType(url);
			if (helpFileType == 2)
			{
				Help.ShowHTML10Help(parent, url, command, parameter);
				return;
			}
			if (helpFileType != 3)
			{
				return;
			}
			Help.ShowHTMLFile(parent, url, command, parameter);
		}

		/// <summary>Displays the index of the specified Help file.</summary>
		/// <param name="parent">A <see cref="T:System.Windows.Forms.Control" /> that identifies the parent of the Help dialog box.</param>
		/// <param name="url">The path and name of the Help file.</param>
		// Token: 0x06002801 RID: 10241 RVA: 0x000BA16C File Offset: 0x000B836C
		public static void ShowHelpIndex(Control parent, string url)
		{
			Help.ShowHelp(parent, url, HelpNavigator.Index, null);
		}

		/// <summary>Displays a Help pop-up window.</summary>
		/// <param name="parent">A <see cref="T:System.Windows.Forms.Control" /> that identifies the parent of the Help dialog box.</param>
		/// <param name="caption">The message to display in the pop-up window.</param>
		/// <param name="location">A value that specifies the horizontal and vertical coordinates at which to display the pop-up window, relative to the upper-left corner of the screen.</param>
		// Token: 0x06002802 RID: 10242 RVA: 0x000BA17C File Offset: 0x000B837C
		public static void ShowPopup(Control parent, string caption, Point location)
		{
			NativeMethods.HH_POPUP hh_POPUP = new NativeMethods.HH_POPUP();
			IntPtr intPtr = Marshal.StringToCoTaskMemAuto(caption);
			try
			{
				hh_POPUP.pszText = intPtr;
				hh_POPUP.idString = 0;
				hh_POPUP.pt = new NativeMethods.POINT(location.X, location.Y);
				hh_POPUP.clrBackground = Color.FromKnownColor(KnownColor.Window).ToArgb() & 16777215;
				Help.ShowHTML10Help(parent, null, HelpNavigator.Topic, hh_POPUP);
			}
			finally
			{
				Marshal.FreeCoTaskMem(intPtr);
			}
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x000BA200 File Offset: 0x000B8400
		private static void ShowHTML10Help(Control parent, string url, HelpNavigator command, object param)
		{
			IntSecurity.UnmanagedCode.Demand();
			string text = url;
			Uri uri = Help.Resolve(url);
			if (uri != null)
			{
				text = uri.AbsoluteUri;
			}
			if (uri == null || uri.IsFile)
			{
				StringBuilder stringBuilder = new StringBuilder();
				string text2 = ((uri != null && uri.IsFile) ? uri.LocalPath : url);
				uint num = UnsafeNativeMethods.GetShortPathName(text2, stringBuilder, 0U);
				if (num > 0U)
				{
					stringBuilder.Capacity = (int)num;
					num = UnsafeNativeMethods.GetShortPathName(text2, stringBuilder, num);
					text = stringBuilder.ToString();
				}
			}
			HandleRef handleRef;
			if (parent != null)
			{
				handleRef = new HandleRef(parent, parent.Handle);
			}
			else
			{
				handleRef = new HandleRef(null, UnsafeNativeMethods.GetActiveWindow());
			}
			string text3 = param as string;
			if (text3 != null)
			{
				object obj;
				int num2 = Help.MapCommandToHTMLCommand(command, text3, out obj);
				string text4 = obj as string;
				if (text4 != null)
				{
					SafeNativeMethods.HtmlHelp(handleRef, text, num2, text4);
					return;
				}
				if (obj is int)
				{
					SafeNativeMethods.HtmlHelp(handleRef, text, num2, (int)obj);
					return;
				}
				if (obj is NativeMethods.HH_FTS_QUERY)
				{
					SafeNativeMethods.HtmlHelp(handleRef, text, num2, (NativeMethods.HH_FTS_QUERY)obj);
					return;
				}
				if (obj is NativeMethods.HH_AKLINK)
				{
					SafeNativeMethods.HtmlHelp(NativeMethods.NullHandleRef, text, 0, null);
					SafeNativeMethods.HtmlHelp(handleRef, text, num2, (NativeMethods.HH_AKLINK)obj);
					return;
				}
				SafeNativeMethods.HtmlHelp(handleRef, text, num2, (string)param);
				return;
			}
			else
			{
				if (param == null)
				{
					object obj;
					SafeNativeMethods.HtmlHelp(handleRef, text, Help.MapCommandToHTMLCommand(command, null, out obj), 0);
					return;
				}
				if (param is NativeMethods.HH_POPUP)
				{
					SafeNativeMethods.HtmlHelp(handleRef, text, 14, (NativeMethods.HH_POPUP)param);
					return;
				}
				if (param.GetType() == typeof(int))
				{
					throw new ArgumentException(SR.GetString("InvalidArgument", new object[] { "param", "Integer" }));
				}
				return;
			}
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x000BA3C0 File Offset: 0x000B85C0
		private static void ShowHTMLFile(Control parent, string url, HelpNavigator command, object param)
		{
			Uri uri = Help.Resolve(url);
			if (uri == null)
			{
				throw new ArgumentException(SR.GetString("HelpInvalidURL", new object[] { url }), "url");
			}
			string scheme = uri.Scheme;
			if (scheme == "http" || scheme == "https")
			{
				new WebPermission(NetworkAccess.Connect, url).Demand();
			}
			else
			{
				IntSecurity.UnmanagedCode.Demand();
			}
			if (command != HelpNavigator.Topic)
			{
				if (command - HelpNavigator.TableOfContents > 2)
				{
				}
			}
			else if (param != null && param is string)
			{
				uri = new Uri(uri.ToString() + "#" + (string)param);
			}
			HandleRef handleRef;
			if (parent != null)
			{
				handleRef = new HandleRef(parent, parent.Handle);
			}
			else
			{
				handleRef = new HandleRef(null, UnsafeNativeMethods.GetActiveWindow());
			}
			UnsafeNativeMethods.ShellExecute_NoBFM(handleRef, null, uri.ToString(), null, null, 1);
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x000BA4A4 File Offset: 0x000B86A4
		private static Uri Resolve(string partialUri)
		{
			Uri uri = null;
			if (!string.IsNullOrEmpty(partialUri))
			{
				try
				{
					uri = new Uri(partialUri);
				}
				catch (UriFormatException)
				{
				}
				catch (ArgumentNullException)
				{
				}
			}
			if (uri != null && uri.Scheme == "file")
			{
				string localPath = NativeMethods.GetLocalPath(partialUri);
				new FileIOPermission(FileIOPermissionAccess.Read, localPath).Assert();
				try
				{
					if (!File.Exists(localPath))
					{
						uri = null;
					}
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			if (uri == null)
			{
				try
				{
					uri = new Uri(new Uri(AppDomain.CurrentDomain.SetupInformation.ApplicationBase), partialUri);
				}
				catch (UriFormatException)
				{
				}
				catch (ArgumentNullException)
				{
				}
				if (uri != null && uri.Scheme == "file")
				{
					string text = uri.LocalPath + uri.Fragment;
					new FileIOPermission(FileIOPermissionAccess.Read, text).Assert();
					try
					{
						if (!File.Exists(text))
						{
							uri = null;
						}
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
			}
			return uri;
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x000BA5D0 File Offset: 0x000B87D0
		private static int GetHelpFileType(string url)
		{
			if (url == null)
			{
				return 3;
			}
			Uri uri = Help.Resolve(url);
			if (uri == null || uri.Scheme == "file")
			{
				string text = Path.GetExtension((uri == null) ? url : (uri.LocalPath + uri.Fragment)).ToLower(CultureInfo.InvariantCulture);
				if (text == ".chm" || text == ".col")
				{
					return 2;
				}
			}
			return 3;
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x000BA650 File Offset: 0x000B8850
		private static int MapCommandToHTMLCommand(HelpNavigator command, string param, out object htmlParam)
		{
			htmlParam = param;
			if (string.IsNullOrEmpty(param) && (command == HelpNavigator.AssociateIndex || command == HelpNavigator.KeywordIndex))
			{
				return 2;
			}
			switch (command)
			{
			case HelpNavigator.Topic:
				return 0;
			case HelpNavigator.TableOfContents:
				return 1;
			case HelpNavigator.Index:
				return 2;
			case HelpNavigator.Find:
				htmlParam = new NativeMethods.HH_FTS_QUERY
				{
					pszSearchQuery = param
				};
				return 3;
			case HelpNavigator.AssociateIndex:
			case HelpNavigator.KeywordIndex:
				break;
			case HelpNavigator.TopicId:
				try
				{
					htmlParam = int.Parse(param, CultureInfo.InvariantCulture);
					return 15;
				}
				catch
				{
					return 2;
				}
				break;
			default:
				return (int)command;
			}
			htmlParam = new NativeMethods.HH_AKLINK
			{
				pszKeywords = param,
				fIndexOnFail = true,
				fReserved = false
			};
			if (command != HelpNavigator.KeywordIndex)
			{
				return 19;
			}
			return 13;
		}

		// Token: 0x04001060 RID: 4192
		internal static readonly TraceSwitch WindowsFormsHelpTrace;

		// Token: 0x04001061 RID: 4193
		private const int HH_DISPLAY_TOPIC = 0;

		// Token: 0x04001062 RID: 4194
		private const int HH_HELP_FINDER = 0;

		// Token: 0x04001063 RID: 4195
		private const int HH_DISPLAY_TOC = 1;

		// Token: 0x04001064 RID: 4196
		private const int HH_DISPLAY_INDEX = 2;

		// Token: 0x04001065 RID: 4197
		private const int HH_DISPLAY_SEARCH = 3;

		// Token: 0x04001066 RID: 4198
		private const int HH_SET_WIN_TYPE = 4;

		// Token: 0x04001067 RID: 4199
		private const int HH_GET_WIN_TYPE = 5;

		// Token: 0x04001068 RID: 4200
		private const int HH_GET_WIN_HANDLE = 6;

		// Token: 0x04001069 RID: 4201
		private const int HH_ENUM_INFO_TYPE = 7;

		// Token: 0x0400106A RID: 4202
		private const int HH_SET_INFO_TYPE = 8;

		// Token: 0x0400106B RID: 4203
		private const int HH_SYNC = 9;

		// Token: 0x0400106C RID: 4204
		private const int HH_ADD_NAV_UI = 10;

		// Token: 0x0400106D RID: 4205
		private const int HH_ADD_BUTTON = 11;

		// Token: 0x0400106E RID: 4206
		private const int HH_GETBROWSER_APP = 12;

		// Token: 0x0400106F RID: 4207
		private const int HH_KEYWORD_LOOKUP = 13;

		// Token: 0x04001070 RID: 4208
		private const int HH_DISPLAY_TEXT_POPUP = 14;

		// Token: 0x04001071 RID: 4209
		private const int HH_HELP_CONTEXT = 15;

		// Token: 0x04001072 RID: 4210
		private const int HH_TP_HELP_CONTEXTMENU = 16;

		// Token: 0x04001073 RID: 4211
		private const int HH_TP_HELP_WM_HELP = 17;

		// Token: 0x04001074 RID: 4212
		private const int HH_CLOSE_ALL = 18;

		// Token: 0x04001075 RID: 4213
		private const int HH_ALINK_LOOKUP = 19;

		// Token: 0x04001076 RID: 4214
		private const int HH_GET_LAST_ERROR = 20;

		// Token: 0x04001077 RID: 4215
		private const int HH_ENUM_CATEGORY = 21;

		// Token: 0x04001078 RID: 4216
		private const int HH_ENUM_CATEGORY_IT = 22;

		// Token: 0x04001079 RID: 4217
		private const int HH_RESET_IT_FILTER = 23;

		// Token: 0x0400107A RID: 4218
		private const int HH_SET_INCLUSIVE_FILTER = 24;

		// Token: 0x0400107B RID: 4219
		private const int HH_SET_EXCLUSIVE_FILTER = 25;

		// Token: 0x0400107C RID: 4220
		private const int HH_SET_GUID = 26;

		// Token: 0x0400107D RID: 4221
		private const int HTML10HELP = 2;

		// Token: 0x0400107E RID: 4222
		private const int HTMLFILE = 3;
	}
}
