using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace Microsoft.WindowsDeviceRecoveryTool.Styles
{
	// Token: 0x02000002 RID: 2
	public class TiltEffect : DependencyObject
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static bool GetIsTiltEnabled(DependencyObject source)
		{
			return (bool)source.GetValue(TiltEffect.IsTiltEnabledProperty);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002072 File Offset: 0x00000272
		public static void SetIsTiltEnabled(DependencyObject source, bool value)
		{
			source.SetValue(TiltEffect.IsTiltEnabledProperty, value);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002088 File Offset: 0x00000288
		public static Vector3D GetRotationVector(DependencyObject source)
		{
			return (Vector3D)source.GetValue(TiltEffect.RotationVectorProperty);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020AA File Offset: 0x000002AA
		public static void SetRotationVector(DependencyObject source, Vector3D value)
		{
			source.SetValue(TiltEffect.RotationVectorProperty, value);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020C0 File Offset: 0x000002C0
		public static void OnIsTiltEnabledChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
		{
			FrameworkElement frameworkElement = target as FrameworkElement;
			bool flag = frameworkElement != null;
			if (flag)
			{
				bool flag2 = (bool)args.NewValue;
				if (flag2)
				{
					frameworkElement.MouseEnter += TiltEffect.OnMouseEnter;
				}
				else
				{
					frameworkElement.MouseEnter -= TiltEffect.OnMouseEnter;
				}
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000211B File Offset: 0x0000031B
		private static void OnMouseEnter(object sender, MouseEventArgs args)
		{
			TiltEffect.TryStartTiltEffect(sender as FrameworkElement, args);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000212C File Offset: 0x0000032C
		private static void TryStartTiltEffect(FrameworkElement frameworkElement, MouseEventArgs args)
		{
			bool flag = TiltEffect.currentTiltElement != null && !object.Equals(TiltEffect.currentTiltElement, frameworkElement);
			if (flag)
			{
				TiltEffect.EndTiltEffect(TiltEffect.currentTiltElement);
			}
			TiltEffect.currentTiltElement = frameworkElement;
			TiltEffect.currentTiltElement.MouseMove += TiltEffect.OnCurrentTiltElementMouseMove;
			TiltEffect.currentTiltElement.MouseLeftButtonUp += TiltEffect.OnCurrentTiltElementMouseLeftButtonUp;
			TiltEffect.currentTiltElement.MouseLeave += TiltEffect.OnCurrentTiltElementMouseLeave;
			TiltEffect.currentTiltElementCenter = new Point(TiltEffect.currentTiltElement.ActualWidth / 2.0, TiltEffect.currentTiltElement.ActualHeight / 2.0);
			TiltEffect.ContinueTiltEffect(frameworkElement, args);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021E8 File Offset: 0x000003E8
		private static void ContinueTiltEffect(IInputElement element, MouseEventArgs args)
		{
			Point position = args.GetPosition(element);
			Point point = new Point(Math.Min(Math.Max(position.X / (TiltEffect.currentTiltElementCenter.X * 2.0), 0.0), 1.0), Math.Min(Math.Max(position.Y / (TiltEffect.currentTiltElementCenter.Y * 2.0), 0.0), 1.0));
			bool flag = double.IsNaN(point.X) || double.IsNaN(point.Y);
			if (!flag)
			{
				double num = Math.Abs(point.X - 0.5);
				double num2 = Math.Abs(point.Y - 0.5);
				double num3 = (double)(-(double)Math.Sign(point.X - 0.5));
				double num4 = (double)Math.Sign(point.Y - 0.5);
				double num5 = num + num2;
				double num6 = ((num + num2 > 0.0) ? (num / (num + num2)) : 0.0);
				double num7 = num5 * 0.3 * 180.0 / 3.1415926535897931;
				double num8 = (1.0 - num5) * 3.0;
				double num9 = -num8;
				double num10 = num7 * num6 * num3;
				double num11 = num7 * (1.0 - num6) * num4;
				Vector3D vector3D = new Vector3D(num11, -num10, num9);
				TiltEffect.SetRotationVector(TiltEffect.currentTiltElement, vector3D);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000239C File Offset: 0x0000059C
		private static void EndTiltEffect(IInputElement element)
		{
			bool flag = element != null;
			if (flag)
			{
				element.MouseMove -= TiltEffect.OnCurrentTiltElementMouseMove;
				element.MouseLeftButtonUp -= TiltEffect.OnCurrentTiltElementMouseLeftButtonUp;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000023DA File Offset: 0x000005DA
		private static void OnCurrentTiltElementMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			TiltEffect.EndTiltEffect(sender as FrameworkElement);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000023DA File Offset: 0x000005DA
		private static void OnCurrentTiltElementMouseLeave(object sender, MouseEventArgs e)
		{
			TiltEffect.EndTiltEffect(sender as FrameworkElement);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023E9 File Offset: 0x000005E9
		private static void OnCurrentTiltElementMouseMove(object sender, MouseEventArgs args)
		{
			TiltEffect.ContinueTiltEffect(sender as FrameworkElement, args);
		}

		// Token: 0x04000001 RID: 1
		private const double MaxAngle = 0.3;

		// Token: 0x04000002 RID: 2
		private const double MaxDepression = 3.0;

		// Token: 0x04000003 RID: 3
		private static FrameworkElement currentTiltElement;

		// Token: 0x04000004 RID: 4
		private static Point currentTiltElementCenter;

		// Token: 0x04000005 RID: 5
		public static readonly DependencyProperty IsTiltEnabledProperty = DependencyProperty.RegisterAttached("IsTiltEnabled", typeof(bool), typeof(TiltEffect), new PropertyMetadata(new PropertyChangedCallback(TiltEffect.OnIsTiltEnabledChanged)));

		// Token: 0x04000006 RID: 6
		public static readonly DependencyProperty RotationVectorProperty = DependencyProperty.RegisterAttached("RotationVector", typeof(Vector3D), typeof(TiltEffect), null);
	}
}
