using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Microsoft.WindowsDeviceRecoveryTool.Controls
{
	// Token: 0x020000CF RID: 207
	public partial class PhonePowerCanvas : Grid, INotifyPropertyChanged
	{
		// Token: 0x0600066B RID: 1643 RVA: 0x0001E4AF File Offset: 0x0001C6AF
		public PhonePowerCanvas()
		{
			this.InitializeComponent();
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600066C RID: 1644 RVA: 0x0001E4C0 File Offset: 0x0001C6C0
		// (remove) Token: 0x0600066D RID: 1645 RVA: 0x0001E4F8 File Offset: 0x0001C6F8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x0001E530 File Offset: 0x0001C730
		// (set) Token: 0x0600066F RID: 1647 RVA: 0x0001E552 File Offset: 0x0001C752
		public Brush PhoneColor
		{
			get
			{
				return (Brush)base.GetValue(PhonePowerCanvas.PhoneColorProperty);
			}
			set
			{
				base.SetValue(PhonePowerCanvas.PhoneColorProperty, value);
				this.OnPropertyChanged("PhoneColor");
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x0001E570 File Offset: 0x0001C770
		// (set) Token: 0x06000671 RID: 1649 RVA: 0x0001E592 File Offset: 0x0001C792
		public Brush ButtonColor
		{
			get
			{
				return (Brush)base.GetValue(PhonePowerCanvas.ButtonColorProperty);
			}
			set
			{
				base.SetValue(PhonePowerCanvas.ButtonColorProperty, value);
				this.OnPropertyChanged("ButtonColor");
			}
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001E5B0 File Offset: 0x0001C7B0
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			bool flag = propertyChanged != null;
			if (flag)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x040002EA RID: 746
		public static readonly DependencyProperty PhoneColorProperty = DependencyProperty.Register("PhoneColor", typeof(Brush), typeof(PhonePowerCanvas), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0, 0, 0))));

		// Token: 0x040002EB RID: 747
		public static readonly DependencyProperty ButtonColorProperty = DependencyProperty.Register("ButtonColor", typeof(Brush), typeof(PhonePowerCanvas), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0, 0, 0))));
	}
}
