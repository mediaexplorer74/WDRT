using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Error
{
	// Token: 0x0200006F RID: 111
	public class ErrorTemplateSelector : DataTemplateSelector
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x00014F2B File Offset: 0x0001312B
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x00014F33 File Offset: 0x00013133
		public DataTemplate DefaultError { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00014F3C File Offset: 0x0001313C
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x00014F44 File Offset: 0x00013144
		public DataTemplate TryAgainError { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00014F4D File Offset: 0x0001314D
		// (set) Token: 0x060003DA RID: 986 RVA: 0x00014F55 File Offset: 0x00013155
		public DataTemplate AutoUpdateError { get; set; }

		// Token: 0x060003DB RID: 987 RVA: 0x00014F60 File Offset: 0x00013160
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			DataTemplate dataTemplate = this.SelectWorkflowExceptionTemplate(item);
			bool flag = dataTemplate != null;
			DataTemplate dataTemplate2;
			if (flag)
			{
				dataTemplate2 = dataTemplate;
			}
			else
			{
				bool flag2 = item is Exception;
				if (flag2)
				{
					dataTemplate2 = this.DefaultError;
				}
				else
				{
					dataTemplate2 = null;
				}
			}
			return dataTemplate2;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00014FA0 File Offset: 0x000131A0
		public bool IsImplemented(object exception)
		{
			return this.SelectTemplate(exception, null) != null;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00014FC0 File Offset: 0x000131C0
		public bool IsWorkflowException(Exception ex)
		{
			return this.SelectWorkflowExceptionTemplate(ex) != null;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00014FDC File Offset: 0x000131DC
		private DataTemplate SelectWorkflowExceptionTemplate(object item)
		{
			bool flag = item is DeviceNotFoundException || item is DownloadPackageException || item is CannotAccessDirectoryException || item is WebException || item is NoInternetConnectionException || item is NotEnoughSpaceException || item is PackageNotFoundException || item is FirmwareFileNotFoundException;
			DataTemplate dataTemplate;
			if (flag)
			{
				dataTemplate = this.TryAgainError;
			}
			else
			{
				bool flag2 = item is AutoUpdateException || item is PlannedServiceBreakException || item is AutoUpdateNotEnoughSpaceException;
				if (flag2)
				{
					dataTemplate = this.AutoUpdateError;
				}
				else
				{
					dataTemplate = null;
				}
			}
			return dataTemplate;
		}
	}
}
