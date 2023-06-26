using System;
using System.ComponentModel.Composition;
using System.Threading;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting;
using Microsoft.WindowsDeviceRecoveryTool.Messages;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Workflow
{
	// Token: 0x02000022 RID: 34
	[Export]
	public class SurveyViewModel : BaseViewModel
	{
		// Token: 0x0600017C RID: 380 RVA: 0x00009948 File Offset: 0x00007B48
		[ImportingConstructor]
		public SurveyViewModel(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext)
		{
			this.appContext = appContext;
			this.SubmitAndContinueCommand = new DelegateCommand<object>(new Action<object>(this.SubmitSurvey), new Func<object, bool>(this.CanSubmit));
			this.ContinueNoSubmitCommand = new DelegateCommand<object>(new Action<object>(this.Continue));
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600017D RID: 381 RVA: 0x000099A0 File Offset: 0x00007BA0
		// (set) Token: 0x0600017E RID: 382 RVA: 0x000099A8 File Offset: 0x00007BA8
		public DelegateCommand<object> SubmitAndContinueCommand { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600017F RID: 383 RVA: 0x000099B1 File Offset: 0x00007BB1
		// (set) Token: 0x06000180 RID: 384 RVA: 0x000099B9 File Offset: 0x00007BB9
		public DelegateCommand<object> ContinueNoSubmitCommand { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000181 RID: 385 RVA: 0x000099C4 File Offset: 0x00007BC4
		// (set) Token: 0x06000182 RID: 386 RVA: 0x000099DC File Offset: 0x00007BDC
		public bool Question1
		{
			get
			{
				return this.question1;
			}
			set
			{
				bool flag = this.question1 != value;
				if (flag)
				{
					this.question1 = value;
					base.RaisePropertyChanged<bool>(() => this.Question1);
					this.SubmitAndContinueCommand.RaiseCanExecuteChanged();
				}
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00009A48 File Offset: 0x00007C48
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00009A60 File Offset: 0x00007C60
		public bool Question2
		{
			get
			{
				return this.question2;
			}
			set
			{
				bool flag = this.question2 != value;
				if (flag)
				{
					this.question2 = value;
					base.RaisePropertyChanged<bool>(() => this.Question2);
					this.SubmitAndContinueCommand.RaiseCanExecuteChanged();
				}
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00009ACC File Offset: 0x00007CCC
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00009AE4 File Offset: 0x00007CE4
		public bool Question3
		{
			get
			{
				return this.question3;
			}
			set
			{
				bool flag = this.question3 != value;
				if (flag)
				{
					this.question3 = value;
					base.RaisePropertyChanged<bool>(() => this.Question3);
					this.SubmitAndContinueCommand.RaiseCanExecuteChanged();
				}
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00009B50 File Offset: 0x00007D50
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00009B68 File Offset: 0x00007D68
		public bool Question4
		{
			get
			{
				return this.question4;
			}
			set
			{
				bool flag = this.question4 != value;
				if (flag)
				{
					this.question4 = value;
					base.RaisePropertyChanged<bool>(() => this.Question4);
					this.SubmitAndContinueCommand.RaiseCanExecuteChanged();
				}
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00009BD4 File Offset: 0x00007DD4
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00009BEC File Offset: 0x00007DEC
		public bool Question5
		{
			get
			{
				return this.question5;
			}
			set
			{
				bool flag = this.question5 != value;
				if (flag)
				{
					this.question5 = value;
					base.RaisePropertyChanged<bool>(() => this.Question5);
					this.SubmitAndContinueCommand.RaiseCanExecuteChanged();
				}
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00009C58 File Offset: 0x00007E58
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00009C70 File Offset: 0x00007E70
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				bool flag = this.description != value;
				if (flag)
				{
					this.description = value;
					base.RaisePropertyChanged<string>(() => this.Description);
					this.SubmitAndContinueCommand.RaiseCanExecuteChanged();
				}
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00009CDC File Offset: 0x00007EDC
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00009CF4 File Offset: 0x00007EF4
		public bool InsiderProgramQuestion
		{
			get
			{
				return this.insiderProgramQuestion;
			}
			set
			{
				bool flag = this.insiderProgramQuestion != value;
				if (flag)
				{
					this.insiderProgramQuestion = value;
					base.RaisePropertyChanged<bool>(() => this.InsiderProgramQuestion);
					this.SubmitAndContinueCommand.RaiseCanExecuteChanged();
				}
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00009D60 File Offset: 0x00007F60
		public override void OnStarted()
		{
			base.OnStarted();
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("Survey1"), ""));
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(true));
			this.CleanSurvey();
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00009DB0 File Offset: 0x00007FB0
		private bool CanSubmit(object obj)
		{
			return this.Question1 || this.Question2 || this.Question3 || this.Question4 || this.Question5 || !string.IsNullOrEmpty(this.Description) || this.InsiderProgramQuestion;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00009E00 File Offset: 0x00008000
		private void SubmitSurvey(object obj)
		{
			SurveyReport survey = new SurveyReport
			{
				Question1 = this.Question1,
				Question2 = this.Question2,
				Question3 = this.Question3,
				Question4 = this.Question4,
				Question5 = this.Question5,
				Description = this.Description,
				InsiderProgramQuestion = this.InsiderProgramQuestion,
				ManufacturerHardwareVariant = this.appContext.CurrentPhone.HardwareVariant,
				ManufacturerHardwareModel = this.appContext.CurrentPhone.HardwareModel,
				Imei = this.appContext.CurrentPhone.Imei,
				ManufacturerName = this.appContext.CurrentPhone.ReportManufacturerName,
				ManufacturerProductLine = this.appContext.CurrentPhone.ReportManufacturerProductLine,
				PhoneType = this.appContext.CurrentPhone.Type
			};
			base.Commands.Run((FlowController c) => c.SurveyCompleted(survey, CancellationToken.None));
			this.Continue(obj);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00009F94 File Offset: 0x00008194
		private void Continue(object parameter)
		{
			bool offlinePackage = this.appContext.CurrentPhone.PackageFileInfo.OfflinePackage;
			if (offlinePackage)
			{
				base.Commands.Run((AppController c) => c.SwitchToState("PackageIntegrityCheckState"));
			}
			else
			{
				base.Commands.Run((AppController c) => c.SwitchToState("DownloadPackageState"));
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000A088 File Offset: 0x00008288
		private void CleanSurvey()
		{
			this.Question1 = (this.Question2 = (this.Question3 = (this.Question4 = (this.Question5 = false))));
			this.Description = string.Empty;
			this.InsiderProgramQuestion = false;
		}

		// Token: 0x040000C7 RID: 199
		private Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;

		// Token: 0x040000C8 RID: 200
		private bool question1;

		// Token: 0x040000C9 RID: 201
		private bool question2;

		// Token: 0x040000CA RID: 202
		private bool question3;

		// Token: 0x040000CB RID: 203
		private bool question4;

		// Token: 0x040000CC RID: 204
		private bool question5;

		// Token: 0x040000CD RID: 205
		private string description;

		// Token: 0x040000CE RID: 206
		private bool insiderProgramQuestion;
	}
}
