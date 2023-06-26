using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors.Core;

namespace Microsoft.Xaml.Behaviors.Layout
{
	// Token: 0x02000034 RID: 52
	public class MouseDragElementBehavior : Behavior<FrameworkElement>
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000198 RID: 408 RVA: 0x00006A84 File Offset: 0x00004C84
		// (remove) Token: 0x06000199 RID: 409 RVA: 0x00006ABC File Offset: 0x00004CBC
		public event MouseEventHandler DragBegun;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600019A RID: 410 RVA: 0x00006AF4 File Offset: 0x00004CF4
		// (remove) Token: 0x0600019B RID: 411 RVA: 0x00006B2C File Offset: 0x00004D2C
		public event MouseEventHandler Dragging;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600019C RID: 412 RVA: 0x00006B64 File Offset: 0x00004D64
		// (remove) Token: 0x0600019D RID: 413 RVA: 0x00006B9C File Offset: 0x00004D9C
		public event MouseEventHandler DragFinished;

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00006BD1 File Offset: 0x00004DD1
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00006BE3 File Offset: 0x00004DE3
		public double X
		{
			get
			{
				return (double)base.GetValue(MouseDragElementBehavior.XProperty);
			}
			set
			{
				base.SetValue(MouseDragElementBehavior.XProperty, value);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00006BF6 File Offset: 0x00004DF6
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00006C08 File Offset: 0x00004E08
		public double Y
		{
			get
			{
				return (double)base.GetValue(MouseDragElementBehavior.YProperty);
			}
			set
			{
				base.SetValue(MouseDragElementBehavior.YProperty, value);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00006C1B File Offset: 0x00004E1B
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00006C2D File Offset: 0x00004E2D
		public bool ConstrainToParentBounds
		{
			get
			{
				return (bool)base.GetValue(MouseDragElementBehavior.ConstrainToParentBoundsProperty);
			}
			set
			{
				base.SetValue(MouseDragElementBehavior.ConstrainToParentBoundsProperty, value);
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00006C40 File Offset: 0x00004E40
		private static void OnXChanged(object sender, DependencyPropertyChangedEventArgs args)
		{
			MouseDragElementBehavior mouseDragElementBehavior = (MouseDragElementBehavior)sender;
			mouseDragElementBehavior.UpdatePosition(new Point((double)args.NewValue, mouseDragElementBehavior.Y));
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00006C71 File Offset: 0x00004E71
		private static void OnYChanged(object sender, DependencyPropertyChangedEventArgs args)
		{
			MouseDragElementBehavior mouseDragElementBehavior = (MouseDragElementBehavior)sender;
			mouseDragElementBehavior.UpdatePosition(new Point(mouseDragElementBehavior.X, (double)args.NewValue));
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00006C98 File Offset: 0x00004E98
		private static void OnConstrainToParentBoundsChanged(object sender, DependencyPropertyChangedEventArgs args)
		{
			MouseDragElementBehavior mouseDragElementBehavior = (MouseDragElementBehavior)sender;
			mouseDragElementBehavior.UpdatePosition(new Point(mouseDragElementBehavior.X, mouseDragElementBehavior.Y));
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00006CC4 File Offset: 0x00004EC4
		private Point ActualPosition
		{
			get
			{
				Point transformOffset = MouseDragElementBehavior.GetTransformOffset(base.AssociatedObject.TransformToVisual(this.RootElement));
				return new Point(transformOffset.X, transformOffset.Y);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00006CFC File Offset: 0x00004EFC
		private Rect ElementBounds
		{
			get
			{
				Rect layoutRect = ExtendedVisualStateManager.GetLayoutRect(base.AssociatedObject);
				return new Rect(new Point(0.0, 0.0), new Size(layoutRect.Width, layoutRect.Height));
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00006D44 File Offset: 0x00004F44
		private FrameworkElement ParentElement
		{
			get
			{
				return base.AssociatedObject.Parent as FrameworkElement;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00006D58 File Offset: 0x00004F58
		private UIElement RootElement
		{
			get
			{
				DependencyObject dependencyObject = base.AssociatedObject;
				for (DependencyObject dependencyObject2 = dependencyObject; dependencyObject2 != null; dependencyObject2 = VisualTreeHelper.GetParent(dependencyObject))
				{
					dependencyObject = dependencyObject2;
				}
				return dependencyObject as UIElement;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00006D84 File Offset: 0x00004F84
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00006DCA File Offset: 0x00004FCA
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

		// Token: 0x060001AD RID: 429 RVA: 0x00006DE8 File Offset: 0x00004FE8
		private void UpdatePosition(Point point)
		{
			if (!this.settingPosition && base.AssociatedObject != null)
			{
				Point transformOffset = MouseDragElementBehavior.GetTransformOffset(base.AssociatedObject.TransformToVisual(this.RootElement));
				double num = (double.IsNaN(point.X) ? 0.0 : (point.X - transformOffset.X));
				double num2 = (double.IsNaN(point.Y) ? 0.0 : (point.Y - transformOffset.Y));
				this.ApplyTranslation(num, num2);
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00006E78 File Offset: 0x00005078
		private void ApplyTranslation(double x, double y)
		{
			if (this.ParentElement != null)
			{
				Point point = MouseDragElementBehavior.TransformAsVector(this.RootElement.TransformToVisual(this.ParentElement), x, y);
				x = point.X;
				y = point.Y;
				if (this.ConstrainToParentBounds)
				{
					FrameworkElement parentElement = this.ParentElement;
					Rect rect = new Rect(0.0, 0.0, parentElement.ActualWidth, parentElement.ActualHeight);
					GeneralTransform generalTransform = base.AssociatedObject.TransformToVisual(parentElement);
					Rect rect2 = this.ElementBounds;
					rect2 = generalTransform.TransformBounds(rect2);
					Rect rect3 = rect2;
					rect3.X += x;
					rect3.Y += y;
					if (!MouseDragElementBehavior.RectContainsRect(rect, rect3))
					{
						if (rect3.X < rect.Left)
						{
							double num = rect3.X - rect.Left;
							x -= num;
						}
						else if (rect3.Right > rect.Right)
						{
							double num2 = rect3.Right - rect.Right;
							x -= num2;
						}
						if (rect3.Y < rect.Top)
						{
							double num3 = rect3.Y - rect.Top;
							y -= num3;
						}
						else if (rect3.Bottom > rect.Bottom)
						{
							double num4 = rect3.Bottom - rect.Bottom;
							y -= num4;
						}
					}
				}
				this.ApplyTranslationTransform(x, y);
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00006FE4 File Offset: 0x000051E4
		internal void ApplyTranslationTransform(double x, double y)
		{
			Transform renderTransform = this.RenderTransform;
			TranslateTransform translateTransform = renderTransform as TranslateTransform;
			if (translateTransform == null)
			{
				TransformGroup transformGroup = renderTransform as TransformGroup;
				MatrixTransform matrixTransform = renderTransform as MatrixTransform;
				if (transformGroup != null)
				{
					if (transformGroup.Children.Count > 0)
					{
						translateTransform = transformGroup.Children[transformGroup.Children.Count - 1] as TranslateTransform;
					}
					if (translateTransform == null)
					{
						translateTransform = new TranslateTransform();
						transformGroup.Children.Add(translateTransform);
					}
				}
				else
				{
					if (matrixTransform != null)
					{
						Matrix matrix = matrixTransform.Matrix;
						matrix.OffsetX += x;
						matrix.OffsetY += y;
						this.RenderTransform = new MatrixTransform
						{
							Matrix = matrix
						};
						return;
					}
					TransformGroup transformGroup2 = new TransformGroup();
					translateTransform = new TranslateTransform();
					if (renderTransform != null)
					{
						transformGroup2.Children.Add(renderTransform);
					}
					transformGroup2.Children.Add(translateTransform);
					this.RenderTransform = transformGroup2;
				}
			}
			translateTransform.X += x;
			translateTransform.Y += y;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000070EC File Offset: 0x000052EC
		internal static Transform CloneTransform(Transform transform)
		{
			if (transform == null)
			{
				return null;
			}
			transform.GetType();
			ScaleTransform scaleTransform;
			if ((scaleTransform = transform as ScaleTransform) != null)
			{
				return new ScaleTransform
				{
					CenterX = scaleTransform.CenterX,
					CenterY = scaleTransform.CenterY,
					ScaleX = scaleTransform.ScaleX,
					ScaleY = scaleTransform.ScaleY
				};
			}
			RotateTransform rotateTransform;
			if ((rotateTransform = transform as RotateTransform) != null)
			{
				return new RotateTransform
				{
					Angle = rotateTransform.Angle,
					CenterX = rotateTransform.CenterX,
					CenterY = rotateTransform.CenterY
				};
			}
			SkewTransform skewTransform;
			if ((skewTransform = transform as SkewTransform) != null)
			{
				return new SkewTransform
				{
					AngleX = skewTransform.AngleX,
					AngleY = skewTransform.AngleY,
					CenterX = skewTransform.CenterX,
					CenterY = skewTransform.CenterY
				};
			}
			TranslateTransform translateTransform;
			if ((translateTransform = transform as TranslateTransform) != null)
			{
				return new TranslateTransform
				{
					X = translateTransform.X,
					Y = translateTransform.Y
				};
			}
			MatrixTransform matrixTransform;
			if ((matrixTransform = transform as MatrixTransform) != null)
			{
				return new MatrixTransform
				{
					Matrix = matrixTransform.Matrix
				};
			}
			TransformGroup transformGroup;
			if ((transformGroup = transform as TransformGroup) != null)
			{
				TransformGroup transformGroup2 = new TransformGroup();
				foreach (Transform transform2 in transformGroup.Children)
				{
					transformGroup2.Children.Add(MouseDragElementBehavior.CloneTransform(transform2));
				}
				return transformGroup2;
			}
			return null;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00007278 File Offset: 0x00005478
		private void UpdatePosition()
		{
			Point transformOffset = MouseDragElementBehavior.GetTransformOffset(base.AssociatedObject.TransformToVisual(this.RootElement));
			this.X = transformOffset.X;
			this.Y = transformOffset.Y;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000072B8 File Offset: 0x000054B8
		internal void StartDrag(Point positionInElementCoordinates)
		{
			this.relativePosition = positionInElementCoordinates;
			base.AssociatedObject.CaptureMouse();
			base.AssociatedObject.MouseMove += this.OnMouseMove;
			base.AssociatedObject.LostMouseCapture += this.OnLostMouseCapture;
			base.AssociatedObject.AddHandler(UIElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.OnMouseLeftButtonUp), false);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00007324 File Offset: 0x00005524
		internal void HandleDrag(Point newPositionInElementCoordinates)
		{
			double num = newPositionInElementCoordinates.X - this.relativePosition.X;
			double num2 = newPositionInElementCoordinates.Y - this.relativePosition.Y;
			Point point = MouseDragElementBehavior.TransformAsVector(base.AssociatedObject.TransformToVisual(this.RootElement), num, num2);
			this.settingPosition = true;
			this.ApplyTranslation(point.X, point.Y);
			this.UpdatePosition();
			this.settingPosition = false;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000739C File Offset: 0x0000559C
		internal void EndDrag()
		{
			base.AssociatedObject.MouseMove -= this.OnMouseMove;
			base.AssociatedObject.LostMouseCapture -= this.OnLostMouseCapture;
			base.AssociatedObject.RemoveHandler(UIElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.OnMouseLeftButtonUp));
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000073F3 File Offset: 0x000055F3
		private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.StartDrag(e.GetPosition(base.AssociatedObject));
			if (this.DragBegun != null)
			{
				this.DragBegun(this, e);
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000741C File Offset: 0x0000561C
		private void OnLostMouseCapture(object sender, MouseEventArgs e)
		{
			this.EndDrag();
			if (this.DragFinished != null)
			{
				this.DragFinished(this, e);
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00007439 File Offset: 0x00005639
		private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			base.AssociatedObject.ReleaseMouseCapture();
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00007446 File Offset: 0x00005646
		private void OnMouseMove(object sender, MouseEventArgs e)
		{
			this.HandleDrag(e.GetPosition(base.AssociatedObject));
			if (this.Dragging != null)
			{
				this.Dragging(this, e);
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00007470 File Offset: 0x00005670
		private static bool RectContainsRect(Rect rect1, Rect rect2)
		{
			return !rect1.IsEmpty && !rect2.IsEmpty && (rect1.X <= rect2.X && rect1.Y <= rect2.Y && rect1.X + rect1.Width >= rect2.X + rect2.Width) && rect1.Y + rect1.Height >= rect2.Y + rect2.Height;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000074F8 File Offset: 0x000056F8
		private static Point TransformAsVector(GeneralTransform transform, double x, double y)
		{
			Point point = transform.Transform(new Point(0.0, 0.0));
			Point point2 = transform.Transform(new Point(x, y));
			return new Point(point2.X - point.X, point2.Y - point.Y);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00007554 File Offset: 0x00005754
		private static Point GetTransformOffset(GeneralTransform transform)
		{
			return transform.Transform(new Point(0.0, 0.0));
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00007573 File Offset: 0x00005773
		protected override void OnAttached()
		{
			base.AssociatedObject.AddHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(this.OnMouseLeftButtonDown), false);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00007592 File Offset: 0x00005792
		protected override void OnDetaching()
		{
			base.AssociatedObject.RemoveHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(this.OnMouseLeftButtonDown));
		}

		// Token: 0x0400008B RID: 139
		private bool settingPosition;

		// Token: 0x0400008C RID: 140
		private Point relativePosition;

		// Token: 0x0400008D RID: 141
		private Transform cachedRenderTransform;

		// Token: 0x04000091 RID: 145
		public static readonly DependencyProperty XProperty = DependencyProperty.Register("X", typeof(double), typeof(MouseDragElementBehavior), new PropertyMetadata(double.NaN, new PropertyChangedCallback(MouseDragElementBehavior.OnXChanged)));

		// Token: 0x04000092 RID: 146
		public static readonly DependencyProperty YProperty = DependencyProperty.Register("Y", typeof(double), typeof(MouseDragElementBehavior), new PropertyMetadata(double.NaN, new PropertyChangedCallback(MouseDragElementBehavior.OnYChanged)));

		// Token: 0x04000093 RID: 147
		public static readonly DependencyProperty ConstrainToParentBoundsProperty = DependencyProperty.Register("ConstrainToParentBounds", typeof(bool), typeof(MouseDragElementBehavior), new PropertyMetadata(false, new PropertyChangedCallback(MouseDragElementBehavior.OnConstrainToParentBoundsChanged)));
	}
}
