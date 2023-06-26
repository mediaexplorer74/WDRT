using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors.Layout;

namespace Microsoft.Xaml.Behaviors.Input
{
	// Token: 0x02000037 RID: 55
	public class TranslateZoomRotateBehavior : Behavior<FrameworkElement>
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00007911 File Offset: 0x00005B11
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x00007923 File Offset: 0x00005B23
		public ManipulationModes SupportedGestures
		{
			get
			{
				return (ManipulationModes)base.GetValue(TranslateZoomRotateBehavior.SupportedGesturesProperty);
			}
			set
			{
				base.SetValue(TranslateZoomRotateBehavior.SupportedGesturesProperty, value);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00007936 File Offset: 0x00005B36
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x00007948 File Offset: 0x00005B48
		public double TranslateFriction
		{
			get
			{
				return (double)base.GetValue(TranslateZoomRotateBehavior.TranslateFrictionProperty);
			}
			set
			{
				base.SetValue(TranslateZoomRotateBehavior.TranslateFrictionProperty, value);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000795B File Offset: 0x00005B5B
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x0000796D File Offset: 0x00005B6D
		public double RotationalFriction
		{
			get
			{
				return (double)base.GetValue(TranslateZoomRotateBehavior.RotationalFrictionProperty);
			}
			set
			{
				base.SetValue(TranslateZoomRotateBehavior.RotationalFrictionProperty, value);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00007980 File Offset: 0x00005B80
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x00007992 File Offset: 0x00005B92
		public bool ConstrainToParentBounds
		{
			get
			{
				return (bool)base.GetValue(TranslateZoomRotateBehavior.ConstrainToParentBoundsProperty);
			}
			set
			{
				base.SetValue(TranslateZoomRotateBehavior.ConstrainToParentBoundsProperty, value);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x000079A5 File Offset: 0x00005BA5
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x000079B7 File Offset: 0x00005BB7
		public double MinimumScale
		{
			get
			{
				return (double)base.GetValue(TranslateZoomRotateBehavior.MinimumScaleProperty);
			}
			set
			{
				base.SetValue(TranslateZoomRotateBehavior.MinimumScaleProperty, value);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001DA RID: 474 RVA: 0x000079CA File Offset: 0x00005BCA
		// (set) Token: 0x060001DB RID: 475 RVA: 0x000079DC File Offset: 0x00005BDC
		public double MaximumScale
		{
			get
			{
				return (double)base.GetValue(TranslateZoomRotateBehavior.MaximumScaleProperty);
			}
			set
			{
				base.SetValue(TranslateZoomRotateBehavior.MaximumScaleProperty, value);
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000079EF File Offset: 0x00005BEF
		private static void frictionChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000079F4 File Offset: 0x00005BF4
		private static object coerceFriction(DependencyObject sender, object value)
		{
			double num = (double)value;
			return Math.Max(0.0, Math.Min(1.0, num));
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00007A2C File Offset: 0x00005C2C
		// (set) Token: 0x060001DF RID: 479 RVA: 0x00007A72 File Offset: 0x00005C72
		private Transform RenderTransform
		{
			get
			{
				if (this.cachedRenderTransform == null || this.cachedRenderTransform != base.AssociatedObject.RenderTransform)
				{
					Transform transform = MouseDragElementBehavior.CloneTransform(base.AssociatedObject.RenderTransform);
					this.RenderTransform = transform;
				}
				return this.cachedRenderTransform;
			}
			set
			{
				if (this.cachedRenderTransform != value)
				{
					this.cachedRenderTransform = value;
					base.AssociatedObject.RenderTransform = value;
				}
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00007A90 File Offset: 0x00005C90
		private Point RenderTransformOriginInElementCoordinates
		{
			get
			{
				return new Point(base.AssociatedObject.RenderTransformOrigin.X * base.AssociatedObject.ActualWidth, base.AssociatedObject.RenderTransformOrigin.Y * base.AssociatedObject.ActualHeight);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00007AE0 File Offset: 0x00005CE0
		private Matrix FullTransformValue
		{
			get
			{
				Point renderTransformOriginInElementCoordinates = this.RenderTransformOriginInElementCoordinates;
				Matrix value = this.RenderTransform.Value;
				value.TranslatePrepend(-renderTransformOriginInElementCoordinates.X, -renderTransformOriginInElementCoordinates.Y);
				value.Translate(renderTransformOriginInElementCoordinates.X, renderTransformOriginInElementCoordinates.Y);
				return value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00007B2D File Offset: 0x00005D2D
		private MatrixTransform MatrixTransform
		{
			get
			{
				this.EnsureTransform();
				return (MatrixTransform)this.RenderTransform;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00007B40 File Offset: 0x00005D40
		private FrameworkElement ParentElement
		{
			get
			{
				return base.AssociatedObject.Parent as FrameworkElement;
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00007B54 File Offset: 0x00005D54
		internal void EnsureTransform()
		{
			MatrixTransform matrixTransform = this.RenderTransform as MatrixTransform;
			if (matrixTransform == null || matrixTransform.IsFrozen)
			{
				if (this.RenderTransform != null)
				{
					matrixTransform = new MatrixTransform(this.FullTransformValue);
				}
				else
				{
					matrixTransform = new MatrixTransform(Matrix.Identity);
				}
				this.RenderTransform = matrixTransform;
			}
			base.AssociatedObject.RenderTransformOrigin = new Point(0.0, 0.0);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00007BC4 File Offset: 0x00005DC4
		internal void ApplyRotationTransform(double angle, Point rotationPoint)
		{
			Matrix matrix = this.MatrixTransform.Matrix;
			matrix.RotateAt(angle, rotationPoint.X, rotationPoint.Y);
			this.MatrixTransform.Matrix = matrix;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00007C00 File Offset: 0x00005E00
		internal void ApplyScaleTransform(double scaleX, double scaleY, Point scalePoint)
		{
			double num = scaleX * this.lastScaleX;
			num = Math.Min(Math.Max(Math.Max(1E-06, this.MinimumScale), num), this.MaximumScale);
			scaleX = num / this.lastScaleX;
			this.lastScaleX = scaleX * this.lastScaleX;
			double num2 = scaleY * this.lastScaleY;
			num2 = Math.Min(Math.Max(Math.Max(1E-06, this.MinimumScale), num2), this.MaximumScale);
			scaleY = num2 / this.lastScaleY;
			this.lastScaleY = scaleY * this.lastScaleY;
			Matrix matrix = this.MatrixTransform.Matrix;
			matrix.ScaleAt(scaleX, scaleY, scalePoint.X, scalePoint.Y);
			this.MatrixTransform.Matrix = matrix;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00007CCC File Offset: 0x00005ECC
		internal void ApplyTranslateTransform(double x, double y)
		{
			Matrix matrix = this.MatrixTransform.Matrix;
			matrix.Translate(x, y);
			this.MatrixTransform.Matrix = matrix;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00007CFC File Offset: 0x00005EFC
		private void ManipulationStarting(object sender, ManipulationStartingEventArgs e)
		{
			FrameworkElement frameworkElement = this.ParentElement;
			if (frameworkElement == null || !frameworkElement.IsAncestorOf(base.AssociatedObject))
			{
				frameworkElement = base.AssociatedObject;
			}
			e.ManipulationContainer = frameworkElement;
			e.Mode = this.SupportedGestures;
			e.Handled = true;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00007D44 File Offset: 0x00005F44
		private void ManipulationInertiaStarting(object sender, ManipulationInertiaStartingEventArgs e)
		{
			double num = ((this.TranslateFriction == 1.0) ? 1.0 : (-0.00666 * Math.Log(1.0 - this.TranslateFriction)));
			double num2 = e.InitialVelocities.LinearVelocity.Length * num;
			e.TranslationBehavior = new InertiaTranslationBehavior
			{
				InitialVelocity = e.InitialVelocities.LinearVelocity,
				DesiredDeceleration = Math.Max(num2, 0.0)
			};
			double num3 = ((this.RotationalFriction == 1.0) ? 1.0 : (-0.00666 * Math.Log(1.0 - this.RotationalFriction)));
			double num4 = Math.Abs(e.InitialVelocities.AngularVelocity) * num3;
			e.RotationBehavior = new InertiaRotationBehavior
			{
				InitialVelocity = e.InitialVelocities.AngularVelocity,
				DesiredDeceleration = Math.Max(num4, 0.0)
			};
			e.Handled = true;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00007E5C File Offset: 0x0000605C
		private void ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
		{
			this.EnsureTransform();
			ManipulationDelta deltaManipulation = e.DeltaManipulation;
			Point point = new Point(base.AssociatedObject.ActualWidth / 2.0, base.AssociatedObject.ActualHeight / 2.0);
			Point point2 = this.FullTransformValue.Transform(point);
			this.ApplyScaleTransform(deltaManipulation.Scale.X, deltaManipulation.Scale.Y, point2);
			this.ApplyRotationTransform(deltaManipulation.Rotation, point2);
			this.ApplyTranslateTransform(deltaManipulation.Translation.X, deltaManipulation.Translation.Y);
			FrameworkElement frameworkElement = (FrameworkElement)e.ManipulationContainer;
			Rect rect = new Rect(frameworkElement.RenderSize);
			Rect rect2 = base.AssociatedObject.TransformToVisual(frameworkElement).TransformBounds(new Rect(base.AssociatedObject.RenderSize));
			if (e.IsInertial && this.ConstrainToParentBounds && !rect.Contains(rect2))
			{
				e.Complete();
			}
			e.Handled = true;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00007F74 File Offset: 0x00006174
		private void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			base.AssociatedObject.CaptureMouse();
			base.AssociatedObject.MouseMove += this.AssociatedObject_MouseMove;
			base.AssociatedObject.LostMouseCapture += this.AssociatedObject_LostMouseCapture;
			e.Handled = true;
			this.lastMousePoint = e.GetPosition(base.AssociatedObject);
			this.isDragging = true;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00007FDB File Offset: 0x000061DB
		private void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			base.AssociatedObject.ReleaseMouseCapture();
			e.Handled = true;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00007FEF File Offset: 0x000061EF
		private void AssociatedObject_LostMouseCapture(object sender, MouseEventArgs e)
		{
			this.isDragging = false;
			base.AssociatedObject.MouseMove -= this.AssociatedObject_MouseMove;
			base.AssociatedObject.LostMouseCapture -= this.AssociatedObject_LostMouseCapture;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00008028 File Offset: 0x00006228
		private void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.isDragging && !this.isAdjustingTransform)
			{
				this.isAdjustingTransform = true;
				Vector vector = e.GetPosition(base.AssociatedObject) - this.lastMousePoint;
				if ((this.SupportedGestures & ManipulationModes.TranslateX) == ManipulationModes.None)
				{
					vector.X = 0.0;
				}
				if ((this.SupportedGestures & ManipulationModes.TranslateY) == ManipulationModes.None)
				{
					vector.Y = 0.0;
				}
				Vector vector2 = this.FullTransformValue.Transform(vector);
				this.ApplyTranslateTransform(vector2.X, vector2.Y);
				this.lastMousePoint = e.GetPosition(base.AssociatedObject);
				this.isAdjustingTransform = false;
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000080DC File Offset: 0x000062DC
		protected override void OnAttached()
		{
			base.AssociatedObject.AddHandler(UIElement.ManipulationStartingEvent, new EventHandler<ManipulationStartingEventArgs>(this.ManipulationStarting), false);
			base.AssociatedObject.AddHandler(UIElement.ManipulationInertiaStartingEvent, new EventHandler<ManipulationInertiaStartingEventArgs>(this.ManipulationInertiaStarting), false);
			base.AssociatedObject.AddHandler(UIElement.ManipulationDeltaEvent, new EventHandler<ManipulationDeltaEventArgs>(this.ManipulationDelta), false);
			base.AssociatedObject.IsManipulationEnabled = true;
			base.AssociatedObject.AddHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(this.MouseLeftButtonDown), false);
			base.AssociatedObject.AddHandler(UIElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.MouseLeftButtonUp), false);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00008188 File Offset: 0x00006388
		protected override void OnDetaching()
		{
			base.AssociatedObject.RemoveHandler(UIElement.ManipulationStartingEvent, new EventHandler<ManipulationStartingEventArgs>(this.ManipulationStarting));
			base.AssociatedObject.RemoveHandler(UIElement.ManipulationInertiaStartingEvent, new EventHandler<ManipulationInertiaStartingEventArgs>(this.ManipulationInertiaStarting));
			base.AssociatedObject.RemoveHandler(UIElement.ManipulationDeltaEvent, new EventHandler<ManipulationDeltaEventArgs>(this.ManipulationDelta));
			base.AssociatedObject.IsManipulationEnabled = false;
			base.AssociatedObject.AddHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(this.MouseLeftButtonDown), false);
			base.AssociatedObject.AddHandler(UIElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.MouseLeftButtonUp), false);
		}

		// Token: 0x0400009C RID: 156
		private Transform cachedRenderTransform;

		// Token: 0x0400009D RID: 157
		private bool isDragging;

		// Token: 0x0400009E RID: 158
		private bool isAdjustingTransform;

		// Token: 0x0400009F RID: 159
		private Point lastMousePoint;

		// Token: 0x040000A0 RID: 160
		private double lastScaleX = 1.0;

		// Token: 0x040000A1 RID: 161
		private double lastScaleY = 1.0;

		// Token: 0x040000A2 RID: 162
		private const double HardMinimumScale = 1E-06;

		// Token: 0x040000A3 RID: 163
		public static readonly DependencyProperty SupportedGesturesProperty = DependencyProperty.Register("SupportedGestures", typeof(ManipulationModes), typeof(TranslateZoomRotateBehavior), new PropertyMetadata(ManipulationModes.All));

		// Token: 0x040000A4 RID: 164
		public static readonly DependencyProperty TranslateFrictionProperty = DependencyProperty.Register("TranslateFriction", typeof(double), typeof(TranslateZoomRotateBehavior), new PropertyMetadata(0.0, new PropertyChangedCallback(TranslateZoomRotateBehavior.frictionChanged), new CoerceValueCallback(TranslateZoomRotateBehavior.coerceFriction)));

		// Token: 0x040000A5 RID: 165
		public static readonly DependencyProperty RotationalFrictionProperty = DependencyProperty.Register("RotationalFriction", typeof(double), typeof(TranslateZoomRotateBehavior), new PropertyMetadata(0.0, new PropertyChangedCallback(TranslateZoomRotateBehavior.frictionChanged), new CoerceValueCallback(TranslateZoomRotateBehavior.coerceFriction)));

		// Token: 0x040000A6 RID: 166
		public static readonly DependencyProperty ConstrainToParentBoundsProperty = DependencyProperty.Register("ConstrainToParentBounds", typeof(bool), typeof(TranslateZoomRotateBehavior), new PropertyMetadata(false));

		// Token: 0x040000A7 RID: 167
		public static readonly DependencyProperty MinimumScaleProperty = DependencyProperty.Register("MinimumScale", typeof(double), typeof(TranslateZoomRotateBehavior), new PropertyMetadata(0.1));

		// Token: 0x040000A8 RID: 168
		public static readonly DependencyProperty MaximumScaleProperty = DependencyProperty.Register("MaximumScale", typeof(double), typeof(TranslateZoomRotateBehavior), new PropertyMetadata(10.0));
	}
}
