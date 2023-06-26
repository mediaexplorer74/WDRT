using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Microsoft.WindowsDeviceRecoveryTool.Framework
{
	// Token: 0x02000083 RID: 131
	public class RegionManager : DependencyObject
	{
		// Token: 0x0600046F RID: 1135 RVA: 0x000169C8 File Offset: 0x00014BC8
		private RegionManager()
		{
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x000169E0 File Offset: 0x00014BE0
		public static RegionManager Instance
		{
			get
			{
				return RegionManager.Nested.NestedInstance;
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x000169F7 File Offset: 0x00014BF7
		public static void SetRegionName(ContentControl element, string value)
		{
			element.SetValue(RegionManager.RegionNameProperty, value);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00016A08 File Offset: 0x00014C08
		public static string GetRegionName(ContentControl element)
		{
			return (string)element.GetValue(RegionManager.RegionNameProperty);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00016A2A File Offset: 0x00014C2A
		public void ShowView(string regionName, FrameworkElement content)
		{
			this.regions[regionName].Content = content;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00016A40 File Offset: 0x00014C40
		public void AddRegion(string name, ContentControl control)
		{
			this.regions.Add(name, control);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00016A54 File Offset: 0x00014C54
		public ContentControl GetRegion(string name)
		{
			return this.regions[name];
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00016A72 File Offset: 0x00014C72
		public void HideRegion(string name)
		{
			this.regions[name].Visibility = Visibility.Collapsed;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00016A88 File Offset: 0x00014C88
		public void RemoveRegion(string name)
		{
			this.regions.Remove(name);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00016A98 File Offset: 0x00014C98
		public void ShowRegion(string name)
		{
			this.regions[name].Visibility = Visibility.Visible;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00016AB0 File Offset: 0x00014CB0
		private static void OnSetRegionNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ContentControl contentControl = d as ContentControl;
			bool flag = contentControl != null;
			if (flag)
			{
				RegionManager.Instance.AddRegion(e.NewValue as string, contentControl);
			}
		}

		// Token: 0x0400021B RID: 539
		private readonly IDictionary<string, ContentControl> regions = new Dictionary<string, ContentControl>();

		// Token: 0x0400021C RID: 540
		public static readonly DependencyProperty RegionNameProperty = DependencyProperty.RegisterAttached("RegionName", typeof(string), typeof(RegionManager), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(RegionManager.OnSetRegionNameChanged)));

		// Token: 0x0200012F RID: 303
		private class Nested
		{
			// Token: 0x040003E7 RID: 999
			internal static readonly RegionManager NestedInstance = new RegionManager();
		}
	}
}
