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
	// Token: 0x020000CE RID: 206
	public partial class PhoneVolumesCanvas : Grid, INotifyPropertyChanged
	{
		// Token: 0x06000660 RID: 1632 RVA: 0x0001E28B File Offset: 0x0001C48B
		public PhoneVolumesCanvas()
		{
			this.InitializeComponent();
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000661 RID: 1633 RVA: 0x0001E29C File Offset: 0x0001C49C
		// (remove) Token: 0x06000662 RID: 1634 RVA: 0x0001E2D4 File Offset: 0x0001C4D4
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x0001E30C File Offset: 0x0001C50C
		// (set) Token: 0x06000664 RID: 1636 RVA: 0x0001E32E File Offset: 0x0001C52E
		public Brush ButtonColor
		{
			get
			{
				return (Brush)base.GetValue(PhoneVolumesCanvas.ButtonColorProperty);
			}
			set
			{
				base.SetValue(PhoneVolumesCanvas.ButtonColorProperty, value);
				this.OnPropertyChanged("ButtonColor");
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x0001E34C File Offset: 0x0001C54C
		// (set) Token: 0x06000666 RID: 1638 RVA: 0x0001E36E File Offset: 0x0001C56E
		public Brush PhoneColor
		{
			get
			{
				return (Brush)base.GetValue(PhoneVolumesCanvas.PhoneColorProperty);
			}
			set
			{
				base.SetValue(PhoneVolumesCanvas.PhoneColorProperty, value);
				this.OnPropertyChanged("PhoneColor");
			}
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001E38C File Offset: 0x0001C58C
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			bool flag = propertyChanged != null;
			if (flag)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x040002E4 RID: 740
		public static readonly DependencyProperty PhoneColorProperty = DependencyProperty.Register("PhoneColor", typeof(Brush), typeof(PhoneVolumesCanvas), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0, 0, 0))));

		// Token: 0x040002E5 RID: 741
		public static readonly DependencyProperty ButtonColorProperty = DependencyProperty.Register("ButtonColor", typeof(Brush), typeof(PhoneVolumesCanvas), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0, 0, 0))));
	}
}
