using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Displays a message window, also known as a dialog box, which presents a message to the user. It is a modal window, blocking other actions in the application until the user closes it. A <see cref="T:System.Windows.Forms.MessageBox" /> can contain text, buttons, and symbols that inform and instruct the user.</summary>
	// Token: 0x020002F8 RID: 760
	public class MessageBox
	{
		// Token: 0x0600304A RID: 12362 RVA: 0x00002843 File Offset: 0x00000A43
		private MessageBox()
		{
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x000D9220 File Offset: 0x000D7420
		private static DialogResult Win32ToDialogResult(int value)
		{
			switch (value)
			{
			case 1:
				return DialogResult.OK;
			case 2:
				return DialogResult.Cancel;
			case 3:
				return DialogResult.Abort;
			case 4:
				return DialogResult.Retry;
			case 5:
				return DialogResult.Ignore;
			case 6:
				return DialogResult.Yes;
			case 7:
				return DialogResult.No;
			default:
				return DialogResult.No;
			}
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x0600304C RID: 12364 RVA: 0x000D9257 File Offset: 0x000D7457
		internal static HelpInfo HelpInfo
		{
			get
			{
				if (MessageBox.helpInfoTable != null && MessageBox.helpInfoTable.Length != 0)
				{
					return MessageBox.helpInfoTable[MessageBox.helpInfoTable.Length - 1];
				}
				return null;
			}
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x000D927C File Offset: 0x000D747C
		private static void PopHelpInfo()
		{
			if (MessageBox.helpInfoTable != null)
			{
				if (MessageBox.helpInfoTable.Length == 1)
				{
					MessageBox.helpInfoTable = null;
					return;
				}
				int num = MessageBox.helpInfoTable.Length - 1;
				HelpInfo[] array = new HelpInfo[num];
				Array.Copy(MessageBox.helpInfoTable, array, num);
				MessageBox.helpInfoTable = array;
			}
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x000D92C4 File Offset: 0x000D74C4
		private static void PushHelpInfo(HelpInfo hpi)
		{
			int num = 0;
			HelpInfo[] array;
			if (MessageBox.helpInfoTable == null)
			{
				array = new HelpInfo[num + 1];
			}
			else
			{
				num = MessageBox.helpInfoTable.Length;
				array = new HelpInfo[num + 1];
				Array.Copy(MessageBox.helpInfoTable, array, num);
			}
			array[num] = hpi;
			MessageBox.helpInfoTable = array;
		}

		/// <summary>Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button.</summary>
		/// <param name="text">The text to display in the message box.</param>
		/// <param name="caption">The text to display in the title bar of the message box.</param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box.</param>
		/// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box.</param>
		/// <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" /> values that specifies the default button for the message box.</param>
		/// <param name="options">One of the <see cref="T:System.Windows.Forms.MessageBoxOptions" /> values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
		/// <param name="displayHelpButton">
		///   <see langword="true" /> to show the Help button; otherwise, <see langword="false" />. The default is <see langword="false" />.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.  
		/// -or-  
		/// <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.  
		/// -or-  
		/// The <paramref name="defaultButton" /> specified is not a member of <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> specified both <see cref="F:System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly" /> and <see cref="F:System.Windows.Forms.MessageBoxOptions.ServiceNotification" />.  
		/// -or-  
		/// <paramref name="buttons" /> specified an invalid combination of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.</exception>
		// Token: 0x0600304F RID: 12367 RVA: 0x000D930C File Offset: 0x000D750C
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, bool displayHelpButton)
		{
			return MessageBox.ShowCore(null, text, caption, buttons, icon, defaultButton, options, displayHelpButton);
		}

		/// <summary>Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file.</summary>
		/// <param name="text">The text to display in the message box.</param>
		/// <param name="caption">The text to display in the title bar of the message box.</param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box.</param>
		/// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box.</param>
		/// <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" /> values that specifies the default button for the message box.</param>
		/// <param name="options">One of the <see cref="T:System.Windows.Forms.MessageBoxOptions" /> values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
		/// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.  
		/// -or-  
		/// <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.  
		/// -or-  
		/// The <paramref name="defaultButton" /> specified is not a member of <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> specified both <see cref="F:System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly" /> and <see cref="F:System.Windows.Forms.MessageBoxOptions.ServiceNotification" />.  
		/// -or-  
		/// <paramref name="buttons" /> specified an invalid combination of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.</exception>
		// Token: 0x06003050 RID: 12368 RVA: 0x000D9320 File Offset: 0x000D7520
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath)
		{
			HelpInfo helpInfo = new HelpInfo(helpFilePath);
			return MessageBox.ShowCore(null, text, caption, buttons, icon, defaultButton, options, helpInfo);
		}

		/// <summary>Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file.</summary>
		/// <param name="owner">An implementation of <see cref="T:System.Windows.Forms.IWin32Window" /> that will own the modal dialog box.</param>
		/// <param name="text">The text to display in the message box.</param>
		/// <param name="caption">The text to display in the title bar of the message box.</param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box.</param>
		/// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box.</param>
		/// <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" /> values that specifies the default button for the message box.</param>
		/// <param name="options">One of the <see cref="T:System.Windows.Forms.MessageBoxOptions" /> values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
		/// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.  
		/// -or-  
		/// <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.  
		/// -or-  
		/// The <paramref name="defaultButton" /> specified is not a member of <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> specified both <see cref="F:System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly" /> and <see cref="F:System.Windows.Forms.MessageBoxOptions.ServiceNotification" />.  
		/// -or-  
		/// <paramref name="buttons" /> specified an invalid combination of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.</exception>
		// Token: 0x06003051 RID: 12369 RVA: 0x000D9344 File Offset: 0x000D7544
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath)
		{
			HelpInfo helpInfo = new HelpInfo(helpFilePath);
			return MessageBox.ShowCore(owner, text, caption, buttons, icon, defaultButton, options, helpInfo);
		}

		/// <summary>Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file and Help keyword.</summary>
		/// <param name="text">The text to display in the message box.</param>
		/// <param name="caption">The text to display in the title bar of the message box.</param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box.</param>
		/// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box.</param>
		/// <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" /> values that specifies the default button for the message box.</param>
		/// <param name="options">One of the <see cref="T:System.Windows.Forms.MessageBoxOptions" /> values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
		/// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
		/// <param name="keyword">The Help keyword to display when the user clicks the Help button.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.  
		/// -or-  
		/// <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.  
		/// -or-  
		/// The <paramref name="defaultButton" /> specified is not a member of <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> specified both <see cref="F:System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly" /> and <see cref="F:System.Windows.Forms.MessageBoxOptions.ServiceNotification" />.  
		/// -or-  
		/// <paramref name="buttons" /> specified an invalid combination of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.</exception>
		// Token: 0x06003052 RID: 12370 RVA: 0x000D936C File Offset: 0x000D756C
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, string keyword)
		{
			HelpInfo helpInfo = new HelpInfo(helpFilePath, keyword);
			return MessageBox.ShowCore(null, text, caption, buttons, icon, defaultButton, options, helpInfo);
		}

		/// <summary>Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file and Help keyword.</summary>
		/// <param name="owner">An implementation of <see cref="T:System.Windows.Forms.IWin32Window" /> that will own the modal dialog box.</param>
		/// <param name="text">The text to display in the message box.</param>
		/// <param name="caption">The text to display in the title bar of the message box.</param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box.</param>
		/// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box.</param>
		/// <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" /> values that specifies the default button for the message box.</param>
		/// <param name="options">One of the <see cref="T:System.Windows.Forms.MessageBoxOptions" /> values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
		/// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
		/// <param name="keyword">The Help keyword to display when the user clicks the Help button.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.  
		/// -or-  
		/// <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.  
		/// -or-  
		/// The <paramref name="defaultButton" /> specified is not a member of <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> specified both <see cref="F:System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly" /> and <see cref="F:System.Windows.Forms.MessageBoxOptions.ServiceNotification" />.  
		/// -or-  
		/// <paramref name="buttons" /> specified an invalid combination of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.</exception>
		// Token: 0x06003053 RID: 12371 RVA: 0x000D9394 File Offset: 0x000D7594
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, string keyword)
		{
			HelpInfo helpInfo = new HelpInfo(helpFilePath, keyword);
			return MessageBox.ShowCore(owner, text, caption, buttons, icon, defaultButton, options, helpInfo);
		}

		/// <summary>Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file and <see langword="HelpNavigator" />.</summary>
		/// <param name="text">The text to display in the message box.</param>
		/// <param name="caption">The text to display in the title bar of the message box.</param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box.</param>
		/// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box.</param>
		/// <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" /> values that specifies the default button for the message box.</param>
		/// <param name="options">One of the <see cref="T:System.Windows.Forms.MessageBoxOptions" /> values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
		/// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
		/// <param name="navigator">One of the <see cref="T:System.Windows.Forms.HelpNavigator" /> values.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.  
		/// -or-  
		/// <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.  
		/// -or-  
		/// The <paramref name="defaultButton" /> specified is not a member of <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> specified both <see cref="F:System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly" /> and <see cref="F:System.Windows.Forms.MessageBoxOptions.ServiceNotification" />.  
		/// -or-  
		/// <paramref name="buttons" /> specified an invalid combination of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.</exception>
		// Token: 0x06003054 RID: 12372 RVA: 0x000D93BC File Offset: 0x000D75BC
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator)
		{
			HelpInfo helpInfo = new HelpInfo(helpFilePath, navigator);
			return MessageBox.ShowCore(null, text, caption, buttons, icon, defaultButton, options, helpInfo);
		}

		/// <summary>Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file and <see langword="HelpNavigator" />.</summary>
		/// <param name="owner">An implementation of <see cref="T:System.Windows.Forms.IWin32Window" /> that will own the modal dialog box.</param>
		/// <param name="text">The text to display in the message box.</param>
		/// <param name="caption">The text to display in the title bar of the message box.</param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box.</param>
		/// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box.</param>
		/// <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" /> values that specifies the default button for the message box.</param>
		/// <param name="options">One of the <see cref="T:System.Windows.Forms.MessageBoxOptions" /> values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
		/// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
		/// <param name="navigator">One of the <see cref="T:System.Windows.Forms.HelpNavigator" /> values.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.  
		/// -or-  
		/// <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.  
		/// -or-  
		/// The <paramref name="defaultButton" /> specified is not a member of <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> specified both <see cref="F:System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly" /> and <see cref="F:System.Windows.Forms.MessageBoxOptions.ServiceNotification" />.  
		/// -or-  
		/// <paramref name="buttons" /> specified an invalid combination of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.</exception>
		// Token: 0x06003055 RID: 12373 RVA: 0x000D93E4 File Offset: 0x000D75E4
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator)
		{
			HelpInfo helpInfo = new HelpInfo(helpFilePath, navigator);
			return MessageBox.ShowCore(owner, text, caption, buttons, icon, defaultButton, options, helpInfo);
		}

		/// <summary>Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file, <see langword="HelpNavigator" />, and Help topic.</summary>
		/// <param name="text">The text to display in the message box.</param>
		/// <param name="caption">The text to display in the title bar of the message box.</param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box.</param>
		/// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box.</param>
		/// <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" /> values that specifies the default button for the message box.</param>
		/// <param name="options">One of the <see cref="T:System.Windows.Forms.MessageBoxOptions" /> values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
		/// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
		/// <param name="navigator">One of the <see cref="T:System.Windows.Forms.HelpNavigator" /> values.</param>
		/// <param name="param">The numeric ID of the Help topic to display when the user clicks the Help button.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.  
		/// -or-  
		/// <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.  
		/// -or-  
		/// The <paramref name="defaultButton" /> specified is not a member of <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> specified both <see cref="F:System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly" /> and <see cref="F:System.Windows.Forms.MessageBoxOptions.ServiceNotification" />.  
		/// -or-  
		/// <paramref name="buttons" /> specified an invalid combination of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.</exception>
		// Token: 0x06003056 RID: 12374 RVA: 0x000D940C File Offset: 0x000D760C
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, object param)
		{
			HelpInfo helpInfo = new HelpInfo(helpFilePath, navigator, param);
			return MessageBox.ShowCore(null, text, caption, buttons, icon, defaultButton, options, helpInfo);
		}

		/// <summary>Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file, <see langword="HelpNavigator" />, and Help topic.</summary>
		/// <param name="owner">An implementation of <see cref="T:System.Windows.Forms.IWin32Window" /> that will own the modal dialog box.</param>
		/// <param name="text">The text to display in the message box.</param>
		/// <param name="caption">The text to display in the title bar of the message box.</param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box.</param>
		/// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box.</param>
		/// <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" /> values that specifies the default button for the message box.</param>
		/// <param name="options">One of the <see cref="T:System.Windows.Forms.MessageBoxOptions" /> values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
		/// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
		/// <param name="navigator">One of the <see cref="T:System.Windows.Forms.HelpNavigator" /> values.</param>
		/// <param name="param">The numeric ID of the Help topic to display when the user clicks the Help button.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.  
		/// -or-  
		/// <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.  
		/// -or-  
		/// The <paramref name="defaultButton" /> specified is not a member of <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> specified both <see cref="F:System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly" /> and <see cref="F:System.Windows.Forms.MessageBoxOptions.ServiceNotification" />.  
		/// -or-  
		/// <paramref name="buttons" /> specified an invalid combination of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.</exception>
		// Token: 0x06003057 RID: 12375 RVA: 0x000D9434 File Offset: 0x000D7634
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, object param)
		{
			HelpInfo helpInfo = new HelpInfo(helpFilePath, navigator, param);
			return MessageBox.ShowCore(owner, text, caption, buttons, icon, defaultButton, options, helpInfo);
		}

		/// <summary>Displays a message box with the specified text, caption, buttons, icon, default button, and options.</summary>
		/// <param name="text">The text to display in the message box.</param>
		/// <param name="caption">The text to display in the title bar of the message box.</param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box.</param>
		/// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box.</param>
		/// <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" /> values that specifies the default button for the message box.</param>
		/// <param name="options">One of the <see cref="T:System.Windows.Forms.MessageBoxOptions" /> values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.  
		/// -or-  
		/// <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.  
		/// -or-  
		/// The <paramref name="defaultButton" /> specified is not a member of <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> specified both <see cref="F:System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly" /> and <see cref="F:System.Windows.Forms.MessageBoxOptions.ServiceNotification" />.  
		/// -or-  
		/// <paramref name="buttons" /> specified an invalid combination of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.</exception>
		// Token: 0x06003058 RID: 12376 RVA: 0x000D945D File Offset: 0x000D765D
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
		{
			return MessageBox.ShowCore(null, text, caption, buttons, icon, defaultButton, options, false);
		}

		/// <summary>Displays a message box with the specified text, caption, buttons, icon, and default button.</summary>
		/// <param name="text">The text to display in the message box.</param>
		/// <param name="caption">The text to display in the title bar of the message box.</param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box.</param>
		/// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box.</param>
		/// <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" /> values that specifies the default button for the message box.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.  
		/// -or-  
		/// <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.  
		/// -or-  
		/// <paramref name="defaultButton" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property.</exception>
		// Token: 0x06003059 RID: 12377 RVA: 0x000D946E File Offset: 0x000D766E
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
		{
			return MessageBox.ShowCore(null, text, caption, buttons, icon, defaultButton, (MessageBoxOptions)0, false);
		}

		/// <summary>Displays a message box with specified text, caption, buttons, and icon.</summary>
		/// <param name="text">The text to display in the message box.</param>
		/// <param name="caption">The text to display in the title bar of the message box.</param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box.</param>
		/// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="buttons" /> parameter specified is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.  
		///  -or-  
		///  The <paramref name="icon" /> parameter specified is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property.</exception>
		// Token: 0x0600305A RID: 12378 RVA: 0x000D947E File Offset: 0x000D767E
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			return MessageBox.ShowCore(null, text, caption, buttons, icon, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0, false);
		}

		/// <summary>Displays a message box with specified text, caption, and buttons.</summary>
		/// <param name="text">The text to display in the message box.</param>
		/// <param name="caption">The text to display in the title bar of the message box.</param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="buttons" /> parameter specified is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property.</exception>
		// Token: 0x0600305B RID: 12379 RVA: 0x000D948D File Offset: 0x000D768D
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
		{
			return MessageBox.ShowCore(null, text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0, false);
		}

		/// <summary>Displays a message box with specified text and caption.</summary>
		/// <param name="text">The text to display in the message box.</param>
		/// <param name="caption">The text to display in the title bar of the message box.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		// Token: 0x0600305C RID: 12380 RVA: 0x000D949C File Offset: 0x000D769C
		public static DialogResult Show(string text, string caption)
		{
			return MessageBox.ShowCore(null, text, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0, false);
		}

		/// <summary>Displays a message box with specified text.</summary>
		/// <param name="text">The text to display in the message box.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		// Token: 0x0600305D RID: 12381 RVA: 0x000D94AB File Offset: 0x000D76AB
		public static DialogResult Show(string text)
		{
			return MessageBox.ShowCore(null, text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0, false);
		}

		/// <summary>Displays a message box in front of the specified object and with the specified text, caption, buttons, icon, default button, and options.</summary>
		/// <param name="owner">An implementation of <see cref="T:System.Windows.Forms.IWin32Window" /> that will own the modal dialog box.</param>
		/// <param name="text">The text to display in the message box.</param>
		/// <param name="caption">The text to display in the title bar of the message box.</param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box.</param>
		/// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box.</param>
		/// <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" /> values the specifies the default button for the message box.</param>
		/// <param name="options">One of the <see cref="T:System.Windows.Forms.MessageBoxOptions" /> values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.  
		/// -or-  
		/// <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.  
		/// -or-  
		/// <paramref name="defaultButton" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> specified both <see cref="F:System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly" /> and <see cref="F:System.Windows.Forms.MessageBoxOptions.ServiceNotification" />.  
		/// -or-  
		/// <paramref name="options" /> specified <see cref="F:System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly" /> or <see cref="F:System.Windows.Forms.MessageBoxOptions.ServiceNotification" /> and specified a value in the <paramref name="owner" /> parameter. These two options should be used only if you invoke the version of this method that does not take an <paramref name="owner" /> parameter.  
		/// -or-  
		/// <paramref name="buttons" /> specified an invalid combination of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.</exception>
		// Token: 0x0600305E RID: 12382 RVA: 0x000D94BE File Offset: 0x000D76BE
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
		{
			return MessageBox.ShowCore(owner, text, caption, buttons, icon, defaultButton, options, false);
		}

		/// <summary>Displays a message box in front of the specified object and with the specified text, caption, buttons, icon, and default button.</summary>
		/// <param name="owner">An implementation of <see cref="T:System.Windows.Forms.IWin32Window" /> that will own the modal dialog box.</param>
		/// <param name="text">The text to display in the message box.</param>
		/// <param name="caption">The text to display in the title bar of the message box.</param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box.</param>
		/// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box.</param>
		/// <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" /> values that specifies the default button for the message box.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.  
		/// -or-  
		/// <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.  
		/// -or-  
		/// <paramref name="defaultButton" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property.</exception>
		// Token: 0x0600305F RID: 12383 RVA: 0x000D94D0 File Offset: 0x000D76D0
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
		{
			return MessageBox.ShowCore(owner, text, caption, buttons, icon, defaultButton, (MessageBoxOptions)0, false);
		}

		/// <summary>Displays a message box in front of the specified object and with the specified text, caption, buttons, and icon.</summary>
		/// <param name="owner">An implementation of <see cref="T:System.Windows.Forms.IWin32Window" /> that will own the modal dialog box.</param>
		/// <param name="text">The text to display in the message box.</param>
		/// <param name="caption">The text to display in the title bar of the message box.</param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box.</param>
		/// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.  
		/// -or-  
		/// <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property.</exception>
		// Token: 0x06003060 RID: 12384 RVA: 0x000D94E1 File Offset: 0x000D76E1
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			return MessageBox.ShowCore(owner, text, caption, buttons, icon, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0, false);
		}

		/// <summary>Displays a message box in front of the specified object and with the specified text, caption, and buttons.</summary>
		/// <param name="owner">An implementation of <see cref="T:System.Windows.Forms.IWin32Window" /> that will own the modal dialog box.</param>
		/// <param name="text">The text to display in the message box.</param>
		/// <param name="caption">The text to display in the title bar of the message box.</param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property.</exception>
		// Token: 0x06003061 RID: 12385 RVA: 0x000D94F1 File Offset: 0x000D76F1
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons)
		{
			return MessageBox.ShowCore(owner, text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0, false);
		}

		/// <summary>Displays a message box in front of the specified object and with the specified text and caption.</summary>
		/// <param name="owner">An implementation of <see cref="T:System.Windows.Forms.IWin32Window" /> that will own the modal dialog box.</param>
		/// <param name="text">The text to display in the message box.</param>
		/// <param name="caption">The text to display in the title bar of the message box.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		// Token: 0x06003062 RID: 12386 RVA: 0x000D9500 File Offset: 0x000D7700
		public static DialogResult Show(IWin32Window owner, string text, string caption)
		{
			return MessageBox.ShowCore(owner, text, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0, false);
		}

		/// <summary>Displays a message box in front of the specified object and with the specified text.</summary>
		/// <param name="owner">An implementation of <see cref="T:System.Windows.Forms.IWin32Window" /> that will own the modal dialog box.</param>
		/// <param name="text">The text to display in the message box.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		// Token: 0x06003063 RID: 12387 RVA: 0x000D950F File Offset: 0x000D770F
		public static DialogResult Show(IWin32Window owner, string text)
		{
			return MessageBox.ShowCore(owner, text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0, false);
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x000D9524 File Offset: 0x000D7724
		private static DialogResult ShowCore(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, HelpInfo hpi)
		{
			DialogResult dialogResult = DialogResult.None;
			try
			{
				MessageBox.PushHelpInfo(hpi);
				dialogResult = MessageBox.ShowCore(owner, text, caption, buttons, icon, defaultButton, options, true);
			}
			finally
			{
				MessageBox.PopHelpInfo();
			}
			return dialogResult;
		}

		// Token: 0x06003065 RID: 12389 RVA: 0x000D9564 File Offset: 0x000D7764
		private static DialogResult ShowCore(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, bool showHelp)
		{
			if (!ClientUtils.IsEnumValid(buttons, (int)buttons, 0, 5))
			{
				throw new InvalidEnumArgumentException("buttons", (int)buttons, typeof(MessageBoxButtons));
			}
			if (!WindowsFormsUtils.EnumValidator.IsEnumWithinShiftedRange(icon, 4, 0, 4))
			{
				throw new InvalidEnumArgumentException("icon", (int)icon, typeof(MessageBoxIcon));
			}
			if (!WindowsFormsUtils.EnumValidator.IsEnumWithinShiftedRange(defaultButton, 8, 0, 2))
			{
				throw new InvalidEnumArgumentException("defaultButton", (int)defaultButton, typeof(DialogResult));
			}
			if (!SystemInformation.UserInteractive && (options & (MessageBoxOptions.ServiceNotification | MessageBoxOptions.DefaultDesktopOnly)) == (MessageBoxOptions)0)
			{
				throw new InvalidOperationException(SR.GetString("CantShowModalOnNonInteractive"));
			}
			if (owner != null && (options & (MessageBoxOptions.ServiceNotification | MessageBoxOptions.DefaultDesktopOnly)) != (MessageBoxOptions)0)
			{
				throw new ArgumentException(SR.GetString("CantShowMBServiceWithOwner"), "options");
			}
			if (showHelp && (options & (MessageBoxOptions.ServiceNotification | MessageBoxOptions.DefaultDesktopOnly)) != (MessageBoxOptions)0)
			{
				throw new ArgumentException(SR.GetString("CantShowMBServiceWithHelp"), "options");
			}
			if ((options & ~(MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading)) != (MessageBoxOptions)0)
			{
				IntSecurity.UnmanagedCode.Demand();
			}
			IntSecurity.SafeSubWindows.Demand();
			int num = (showHelp ? 16384 : 0);
			num |= (int)(buttons | (MessageBoxButtons)icon | (MessageBoxButtons)defaultButton | (MessageBoxButtons)options);
			IntPtr intPtr = IntPtr.Zero;
			if (showHelp || (options & (MessageBoxOptions.ServiceNotification | MessageBoxOptions.DefaultDesktopOnly)) == (MessageBoxOptions)0)
			{
				if (owner == null)
				{
					intPtr = UnsafeNativeMethods.GetActiveWindow();
				}
				else
				{
					intPtr = Control.GetSafeHandle(owner);
				}
			}
			IntPtr intPtr2 = IntPtr.Zero;
			if (Application.UseVisualStyles)
			{
				if (UnsafeNativeMethods.GetModuleHandle("shell32.dll") == IntPtr.Zero && UnsafeNativeMethods.LoadLibraryFromSystemPathIfAvailable("shell32.dll") == IntPtr.Zero)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					throw new Win32Exception(lastWin32Error, SR.GetString("LoadDLLError", new object[] { "shell32.dll" }));
				}
				intPtr2 = UnsafeNativeMethods.ThemingScope.Activate();
			}
			Application.BeginModalMessageLoop();
			DialogResult dialogResult;
			try
			{
				dialogResult = MessageBox.Win32ToDialogResult(SafeNativeMethods.MessageBox(new HandleRef(owner, intPtr), text, caption, num));
			}
			finally
			{
				Application.EndModalMessageLoop();
				UnsafeNativeMethods.ThemingScope.Deactivate(intPtr2);
			}
			UnsafeNativeMethods.SendMessage(new HandleRef(owner, intPtr), 7, 0, 0);
			return dialogResult;
		}

		// Token: 0x040013E0 RID: 5088
		private const int IDOK = 1;

		// Token: 0x040013E1 RID: 5089
		private const int IDCANCEL = 2;

		// Token: 0x040013E2 RID: 5090
		private const int IDABORT = 3;

		// Token: 0x040013E3 RID: 5091
		private const int IDRETRY = 4;

		// Token: 0x040013E4 RID: 5092
		private const int IDIGNORE = 5;

		// Token: 0x040013E5 RID: 5093
		private const int IDYES = 6;

		// Token: 0x040013E6 RID: 5094
		private const int IDNO = 7;

		// Token: 0x040013E7 RID: 5095
		private const int HELP_BUTTON = 16384;

		// Token: 0x040013E8 RID: 5096
		[ThreadStatic]
		private static HelpInfo[] helpInfoTable;
	}
}
