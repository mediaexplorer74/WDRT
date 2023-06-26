using System;
using System.ComponentModel.Composition;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions;
using Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions.HTC;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Error
{
	// Token: 0x02000071 RID: 113
	[Export]
	public class ErrorViewModel : BaseViewModel, ICanHandle<ErrorMessage>, ICanHandle
	{
		// Token: 0x060003E4 RID: 996 RVA: 0x000150E4 File Offset: 0x000132E4
		[ImportingConstructor]
		public ErrorViewModel(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext)
		{
			this.AppContext = appContext;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x000150F8 File Offset: 0x000132F8
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x00015110 File Offset: 0x00013310
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
			set
			{
				base.SetValue<Exception>(() => this.Exception, ref this.exception, value);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00015150 File Offset: 0x00013350
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x00015168 File Offset: 0x00013368
		public Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext AppContext
		{
			get
			{
				return this.appContext;
			}
			set
			{
				base.SetValue<Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext>(() => this.AppContext, ref this.appContext, value);
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x000151A8 File Offset: 0x000133A8
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x000151C0 File Offset: 0x000133C0
		public bool ExpanderExpanded
		{
			get
			{
				return this.expanderExpanded;
			}
			set
			{
				base.SetValue<bool>(() => this.ExpanderExpanded, ref this.expanderExpanded, value);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x00015200 File Offset: 0x00013400
		public string ErrorHeader
		{
			get
			{
				AutoUpdateNotEnoughSpaceException ex = this.Exception as AutoUpdateNotEnoughSpaceException;
				bool flag = ex != null;
				string text;
				if (flag)
				{
					text = LocalizationManager.GetTranslation("Error_NotEnoughSpaceException");
				}
				else
				{
					text = LocalizationManager.GetTranslation("Error_" + this.Exception.GetType().Name);
				}
				return text;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x00015254 File Offset: 0x00013454
		public string ErrorDescription
		{
			get
			{
				NotEnoughSpaceException ex = this.Exception as NotEnoughSpaceException;
				bool flag = ex != null && ex.Needed == 0L;
				string text;
				if (flag)
				{
					text = string.Empty;
				}
				else
				{
					string customErrorMessage = this.GetCustomErrorMessage();
					bool flag2 = !string.IsNullOrWhiteSpace(customErrorMessage);
					if (flag2)
					{
						text = customErrorMessage;
					}
					else
					{
						DownloadPackageException ex2 = this.Exception as DownloadPackageException;
						bool flag3 = ex2 != null;
						if (flag3)
						{
							string translation = LocalizationManager.GetTranslation("ErrorDescription_" + ex2.GetType().Name);
							text = string.Format(translation, LocalizationManager.GetTranslation("ButtonTryAgain"), LocalizationManager.GetTranslation("ButtonExit"));
						}
						else
						{
							text = LocalizationManager.GetTranslation("ErrorDescription_" + this.Exception.GetType().Name);
						}
					}
				}
				return text;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00015324 File Offset: 0x00013524
		public string ErrorDetails
		{
			get
			{
				bool flag = this.Exception != null;
				string text;
				if (flag)
				{
					text = this.Exception.Message;
				}
				else
				{
					text = null;
				}
				return text;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x00015354 File Offset: 0x00013554
		public bool ErrorDetailsVisibile
		{
			get
			{
				return this.Exception != null && !string.IsNullOrWhiteSpace(this.Exception.Message) && !(this.Exception is UnauthorizedAccessException) && !(this.Exception is PlannedServiceBreakException) && !(this.Exception is RestartApplicationException);
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x000153B0 File Offset: 0x000135B0
		public void Handle(ErrorMessage message)
		{
			this.Exception = message.Exception;
			base.RaisePropertyChanged<string>(() => this.ErrorHeader);
			base.RaisePropertyChanged<string>(() => this.ErrorDescription);
			base.RaisePropertyChanged<string>(() => this.ErrorDetails);
			base.RaisePropertyChanged<bool>(() => this.ErrorDetailsVisibile);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x000154A4 File Offset: 0x000136A4
		public override void OnStarted()
		{
			base.OnStarted();
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("Error"), ""));
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(false));
			base.EventAggregator.Publish<BlockWindowMessage>(new BlockWindowMessage(false, null, null));
			bool flag = this.Exception != null;
			if (flag)
			{
				Tracer<ErrorViewModel>.WriteError(this.Exception);
			}
			this.ExpanderExpanded = false;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00015524 File Offset: 0x00013724
		private string GetCustomErrorMessage()
		{
			NotEnoughSpaceException ex = this.Exception as NotEnoughSpaceException;
			bool flag = ex != null;
			string text;
			if (flag)
			{
				string translation = LocalizationManager.GetTranslation("ErrorDescription_" + this.Exception.GetType().Name);
				text = string.Format(translation, ((ex.Needed - ex.Available) / 1024L / 1024L).ToString() + "MB", ex.Disk);
			}
			else
			{
				AutoUpdateNotEnoughSpaceException ex2 = this.Exception as AutoUpdateNotEnoughSpaceException;
				bool flag2 = ex2 != null;
				if (flag2)
				{
					string translation2 = LocalizationManager.GetTranslation("ErrorDescription_NotEnoughSpaceException");
					text = string.Format(translation2, ((ex2.Needed - ex2.Available) / 1024L / 1024L).ToString() + "MB", ex2.Disk);
				}
				else
				{
					UnauthorizedAccessException ex3 = this.Exception as UnauthorizedAccessException;
					bool flag3 = ex3 != null;
					if (flag3)
					{
						text = ex3.Message;
					}
					else
					{
						PlannedServiceBreakException ex4 = this.Exception as PlannedServiceBreakException;
						bool flag4 = ex4 != null;
						if (flag4)
						{
							string translation3 = LocalizationManager.GetTranslation("ErrorDescription_" + ex4.GetType().Name);
							text = string.Format(translation3, ex4.Start, ex4.End);
						}
						else
						{
							bool flag5 = this.Exception is HtcDeviceHandshakingException || this.Exception is HtcDeviceCommunicationException;
							if (flag5)
							{
								string translation4 = LocalizationManager.GetTranslation("ErrorDescription_HtcDeviceCommunicationException");
								text = string.Format(translation4, "boot-loader");
							}
							else
							{
								HtcUsbCommunicationException ex5 = this.Exception as HtcUsbCommunicationException;
								bool flag6 = ex5 != null;
								if (flag6)
								{
									string translation5 = LocalizationManager.GetTranslation("ErrorDescription_" + ex5.GetType().Name);
									text = string.Format(translation5, "boot-loader");
								}
								else
								{
									HtcServiceControlException ex6 = this.Exception as HtcServiceControlException;
									bool flag7 = ex6 != null;
									if (flag7)
									{
										string translation6 = LocalizationManager.GetTranslation("ErrorDescription_" + ex6.GetType().Name);
										text = string.Format(translation6, "WcesComm");
									}
									else
									{
										RestartApplicationException ex7 = this.Exception as RestartApplicationException;
										bool flag8 = ex7 != null;
										if (flag8)
										{
											string translation7 = LocalizationManager.GetTranslation("ErrorDescription_" + ex7.GetType().Name);
											text = string.Format(translation7, AppInfo.AppTitle());
										}
										else
										{
											text = null;
										}
									}
								}
							}
						}
					}
				}
			}
			return text;
		}

		// Token: 0x040001C0 RID: 448
		private Exception exception;

		// Token: 0x040001C1 RID: 449
		private Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;

		// Token: 0x040001C2 RID: 450
		private bool expanderExpanded;
	}
}
