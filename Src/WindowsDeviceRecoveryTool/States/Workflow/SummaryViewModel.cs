using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Workflow
{
	// Token: 0x02000020 RID: 32
	[Export]
	public class SummaryViewModel : BaseViewModel, ICanHandle<FlashResultMessage>, ICanHandle
	{
		// Token: 0x06000168 RID: 360 RVA: 0x000094E0 File Offset: 0x000076E0
		[ImportingConstructor]
		public SummaryViewModel(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext)
		{
			this.appContext = appContext;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000169 RID: 361 RVA: 0x000094F4 File Offset: 0x000076F4
		// (set) Token: 0x0600016A RID: 362 RVA: 0x0000950C File Offset: 0x0000770C
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

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000954C File Offset: 0x0000774C
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00009564 File Offset: 0x00007764
		public bool IsPassed
		{
			get
			{
				return this.isPassed;
			}
			set
			{
				base.SetValue<bool>(() => this.IsPassed, ref this.isPassed, value);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600016D RID: 365 RVA: 0x000095A4 File Offset: 0x000077A4
		// (set) Token: 0x0600016E RID: 366 RVA: 0x000095BC File Offset: 0x000077BC
		public string FinishText
		{
			get
			{
				return this.finishText;
			}
			set
			{
				base.SetValue<string>(() => this.FinishText, ref this.finishText, value);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600016F RID: 367 RVA: 0x000095FC File Offset: 0x000077FC
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00009614 File Offset: 0x00007814
		public string ResultMessage
		{
			get
			{
				return this.resultMessage;
			}
			set
			{
				base.SetValue<string>(() => this.ResultMessage, ref this.resultMessage, value);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00009654 File Offset: 0x00007854
		// (set) Token: 0x06000172 RID: 370 RVA: 0x0000966C File Offset: 0x0000786C
		public string InstructionMessage
		{
			get
			{
				return this.instructionMessage;
			}
			set
			{
				base.SetValue<string>(() => this.InstructionMessage, ref this.instructionMessage, value);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000173 RID: 371 RVA: 0x000096AC File Offset: 0x000078AC
		// (set) Token: 0x06000174 RID: 372 RVA: 0x000096C4 File Offset: 0x000078C4
		private FlashResultMessage Message
		{
			get
			{
				return this.message;
			}
			set
			{
				base.SetValue<FlashResultMessage>(() => this.Message, ref this.message, value);
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00009704 File Offset: 0x00007904
		public void Handle(FlashResultMessage flashResultMessage)
		{
			this.Message = flashResultMessage;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00009710 File Offset: 0x00007910
		public override void OnStarted()
		{
			base.OnStarted();
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("Summary"), ""));
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(false));
			base.EventAggregator.Publish<BlockWindowMessage>(new BlockWindowMessage(false, null, null));
			this.RefreshResults();
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00009774 File Offset: 0x00007974
		private void RefreshResults()
		{
			this.IsPassed = this.Message.Result;
			this.ResultMessage = LocalizationManager.GetTranslation(this.Message.Result ? "OperationPassed" : "OperationFailed");
			this.InstructionMessage = this.ConstructExtendedMessage(this.Message.ExtendedMessage);
			this.FinishText = ((!this.IsPassed && this.appContext.SelectedManufacturer == PhoneTypes.Htc) ? LocalizationManager.GetTranslation("ButtonContinue") : LocalizationManager.GetTranslation("ButtonFinish"));
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00009804 File Offset: 0x00007A04
		private string ConstructExtendedMessage(List<string> extendedMessage)
		{
			string text = string.Empty;
			bool flag = extendedMessage == null || extendedMessage.Count == 0;
			if (flag)
			{
				text = string.Empty;
			}
			else
			{
				bool flag2 = extendedMessage.Count == 1;
				if (flag2)
				{
					bool flag3 = string.IsNullOrEmpty(this.Message.Argument);
					if (flag3)
					{
						text = string.Format(LocalizationManager.GetTranslation(extendedMessage[0]), new object[0]);
					}
					else
					{
						text = string.Format(LocalizationManager.GetTranslation(extendedMessage[0]), this.Message.Argument);
					}
				}
				else
				{
					bool flag4 = extendedMessage.Count == 2;
					if (flag4)
					{
						text = string.Format(LocalizationManager.GetTranslation(extendedMessage[0]), LocalizationManager.GetTranslation(extendedMessage[1]));
					}
				}
			}
			return text;
		}

		// Token: 0x040000BF RID: 191
		private string resultMessage;

		// Token: 0x040000C0 RID: 192
		private string instructionMessage;

		// Token: 0x040000C1 RID: 193
		private bool isPassed;

		// Token: 0x040000C2 RID: 194
		private string finishText;

		// Token: 0x040000C3 RID: 195
		private Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;

		// Token: 0x040000C4 RID: 196
		private FlashResultMessage message;
	}
}
