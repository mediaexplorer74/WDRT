using System;
using System.Windows;
using Microsoft.WindowsDeviceRecoveryTool.Common;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;

namespace Microsoft.WindowsDeviceRecoveryTool.States.BaseStates
{
	// Token: 0x0200007A RID: 122
	public class UiState<TView, TViewModel> : UiBaseState where TView : FrameworkElement where TViewModel : BaseViewModel
	{
		// Token: 0x06000412 RID: 1042 RVA: 0x00015B84 File Offset: 0x00013D84
		public UiState(TView view, TViewModel viewModel, string showInRegion = null)
		{
			this.view = view;
			this.viewModel = viewModel;
			bool flag = !string.IsNullOrWhiteSpace(showInRegion);
			if (flag)
			{
				RegionAttribute regionAttribute = this.GetRegionAttribute();
				bool flag2 = regionAttribute.Names.Contains(showInRegion);
				if (!flag2)
				{
					string text = string.Format(LocalizationManager.GetTranslation("MissingAttributeExceptionMessage"), showInRegion, view);
					throw new NotImplementedException(text);
				}
				this.showInRegion = showInRegion;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x00015BF8 File Offset: 0x00013DF8
		protected TView View
		{
			get
			{
				return this.view;
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00015C10 File Offset: 0x00013E10
		public override void Start()
		{
			this.view.DataContext = this.viewModel;
			this.VisibleRegions.ForEach(delegate(string region)
			{
				RegionManager.Instance.ShowRegion(region);
			});
			this.InvisibleRegions.ForEach(delegate(string region)
			{
				RegionManager.Instance.HideRegion(region);
			});
			this.ShowView();
			base.Start();
			this.viewModel.IsStarted = true;
			this.viewModel.OnStarted();
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00015CC1 File Offset: 0x00013EC1
		public override void Stop()
		{
			this.viewModel.IsStarted = false;
			this.viewModel.OnStopped();
			base.Stop();
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00015CF0 File Offset: 0x00013EF0
		private void ShowView()
		{
			RegionAttribute regionAttribute = this.GetRegionAttribute();
			string text = (string.IsNullOrWhiteSpace(this.showInRegion) ? regionAttribute.Names[0] : this.showInRegion);
			RegionManager.Instance.ShowView(text, this.view);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00015D40 File Offset: 0x00013F40
		private RegionAttribute GetRegionAttribute()
		{
			RegionAttribute attribute = this.view.GetAttribute<RegionAttribute>();
			bool flag = attribute == null;
			if (flag)
			{
				throw new NotImplementedException(string.Format("The class {0} should have RegionAttribute. Please correct it.", this.view.GetType().Name));
			}
			return attribute;
		}

		// Token: 0x040001CF RID: 463
		private readonly TView view;

		// Token: 0x040001D0 RID: 464
		private readonly TViewModel viewModel;

		// Token: 0x040001D1 RID: 465
		private readonly string showInRegion;
	}
}
