using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Microsoft.Xaml.Behaviors.Core
{
	// Token: 0x0200003A RID: 58
	public class ChangePropertyAction : TargetedTriggerAction<object>
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000881D File Offset: 0x00006A1D
		// (set) Token: 0x0600020D RID: 525 RVA: 0x0000882F File Offset: 0x00006A2F
		public string PropertyName
		{
			get
			{
				return (string)base.GetValue(ChangePropertyAction.PropertyNameProperty);
			}
			set
			{
				base.SetValue(ChangePropertyAction.PropertyNameProperty, value);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000883D File Offset: 0x00006A3D
		// (set) Token: 0x0600020F RID: 527 RVA: 0x0000884A File Offset: 0x00006A4A
		public object Value
		{
			get
			{
				return base.GetValue(ChangePropertyAction.ValueProperty);
			}
			set
			{
				base.SetValue(ChangePropertyAction.ValueProperty, value);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00008858 File Offset: 0x00006A58
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0000886A File Offset: 0x00006A6A
		public Duration Duration
		{
			get
			{
				return (Duration)base.GetValue(ChangePropertyAction.DurationProperty);
			}
			set
			{
				base.SetValue(ChangePropertyAction.DurationProperty, value);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000887D File Offset: 0x00006A7D
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0000888F File Offset: 0x00006A8F
		public bool Increment
		{
			get
			{
				return (bool)base.GetValue(ChangePropertyAction.IncrementProperty);
			}
			set
			{
				base.SetValue(ChangePropertyAction.IncrementProperty, value);
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000088A4 File Offset: 0x00006AA4
		protected override void Invoke(object parameter)
		{
			if (base.AssociatedObject == null)
			{
				return;
			}
			if (string.IsNullOrEmpty(this.PropertyName))
			{
				return;
			}
			if (base.Target == null)
			{
				return;
			}
			Type type = base.Target.GetType();
			PropertyInfo property = type.GetProperty(this.PropertyName);
			this.ValidateProperty(property);
			object obj = this.Value;
			TypeConverter typeConverter = TypeConverterHelper.GetTypeConverter(property.PropertyType);
			Exception ex = null;
			try
			{
				if (this.Value != null)
				{
					if (typeConverter != null && typeConverter.CanConvertFrom(this.Value.GetType()))
					{
						obj = typeConverter.ConvertFrom(null, CultureInfo.InvariantCulture, this.Value);
					}
					else
					{
						typeConverter = TypeConverterHelper.GetTypeConverter(this.Value.GetType());
						if (typeConverter != null && typeConverter.CanConvertTo(property.PropertyType))
						{
							obj = typeConverter.ConvertTo(null, CultureInfo.InvariantCulture, this.Value, property.PropertyType);
						}
					}
				}
				if (this.Duration.HasTimeSpan)
				{
					this.ValidateAnimationPossible(type);
					object currentPropertyValue = ChangePropertyAction.GetCurrentPropertyValue(base.Target, property);
					this.AnimatePropertyChange(property, currentPropertyValue, obj);
				}
				else
				{
					if (this.Increment)
					{
						obj = this.IncrementCurrentValue(property);
					}
					property.SetValue(base.Target, obj, new object[0]);
				}
			}
			catch (FormatException ex)
			{
			}
			catch (ArgumentException ex)
			{
			}
			catch (MethodAccessException ex)
			{
			}
			if (ex != null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionStringTable.ChangePropertyActionCannotSetValueExceptionMessage, new object[]
				{
					(this.Value != null) ? this.Value.GetType().Name : "null",
					this.PropertyName,
					property.PropertyType.Name
				}), ex);
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00008A5C File Offset: 0x00006C5C
		private void AnimatePropertyChange(PropertyInfo propertyInfo, object fromValue, object newValue)
		{
			Storyboard storyboard = new Storyboard();
			Timeline timeline;
			if (typeof(double).IsAssignableFrom(propertyInfo.PropertyType))
			{
				timeline = this.CreateDoubleAnimation((double)fromValue, (double)newValue);
			}
			else if (typeof(Color).IsAssignableFrom(propertyInfo.PropertyType))
			{
				timeline = this.CreateColorAnimation((Color)fromValue, (Color)newValue);
			}
			else if (typeof(Point).IsAssignableFrom(propertyInfo.PropertyType))
			{
				timeline = this.CreatePointAnimation((Point)fromValue, (Point)newValue);
			}
			else
			{
				timeline = this.CreateKeyFrameAnimation(fromValue, newValue);
			}
			timeline.Duration = this.Duration;
			storyboard.Children.Add(timeline);
			if (base.TargetObject == null && base.TargetName != null && base.Target is Freezable)
			{
				Storyboard.SetTargetName(storyboard, base.TargetName);
			}
			else
			{
				Storyboard.SetTarget(storyboard, (DependencyObject)base.Target);
			}
			Storyboard.SetTargetProperty(storyboard, new PropertyPath(propertyInfo.Name, new object[0]));
			storyboard.Completed += delegate(object o, EventArgs e)
			{
				propertyInfo.SetValue(this.Target, newValue, new object[0]);
			};
			storyboard.FillBehavior = FillBehavior.Stop;
			FrameworkElement frameworkElement = base.AssociatedObject as FrameworkElement;
			if (frameworkElement != null)
			{
				storyboard.Begin(frameworkElement);
				return;
			}
			storyboard.Begin();
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00008BE0 File Offset: 0x00006DE0
		private static object GetCurrentPropertyValue(object target, PropertyInfo propertyInfo)
		{
			FrameworkElement frameworkElement = target as FrameworkElement;
			target.GetType();
			object obj = propertyInfo.GetValue(target, null);
			if (frameworkElement != null && (propertyInfo.Name == "Width" || propertyInfo.Name == "Height") && double.IsNaN((double)obj))
			{
				if (propertyInfo.Name == "Width")
				{
					obj = frameworkElement.ActualWidth;
				}
				else
				{
					obj = frameworkElement.ActualHeight;
				}
			}
			return obj;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00008C68 File Offset: 0x00006E68
		private void ValidateAnimationPossible(Type targetType)
		{
			if (this.Increment)
			{
				throw new InvalidOperationException(ExceptionStringTable.ChangePropertyActionCannotIncrementAnimatedPropertyChangeExceptionMessage);
			}
			if (!typeof(DependencyObject).IsAssignableFrom(targetType))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, ExceptionStringTable.ChangePropertyActionCannotAnimateTargetTypeExceptionMessage, new object[] { targetType.Name }));
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00008CC0 File Offset: 0x00006EC0
		private Timeline CreateKeyFrameAnimation(object newValue, object fromValue)
		{
			ObjectAnimationUsingKeyFrames objectAnimationUsingKeyFrames = new ObjectAnimationUsingKeyFrames();
			DiscreteObjectKeyFrame discreteObjectKeyFrame = new DiscreteObjectKeyFrame
			{
				KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0L)),
				Value = fromValue
			};
			DiscreteObjectKeyFrame discreteObjectKeyFrame2 = new DiscreteObjectKeyFrame
			{
				KeyTime = KeyTime.FromTimeSpan(this.Duration.TimeSpan),
				Value = newValue
			};
			objectAnimationUsingKeyFrames.KeyFrames.Add(discreteObjectKeyFrame);
			objectAnimationUsingKeyFrames.KeyFrames.Add(discreteObjectKeyFrame2);
			return objectAnimationUsingKeyFrames;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00008D31 File Offset: 0x00006F31
		private Timeline CreatePointAnimation(Point fromValue, Point newValue)
		{
			return new PointAnimation
			{
				From = new Point?(fromValue),
				To = new Point?(newValue)
			};
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00008D50 File Offset: 0x00006F50
		private Timeline CreateColorAnimation(Color fromValue, Color newValue)
		{
			return new ColorAnimation
			{
				From = new Color?(fromValue),
				To = new Color?(newValue)
			};
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00008D6F File Offset: 0x00006F6F
		private Timeline CreateDoubleAnimation(double fromValue, double newValue)
		{
			return new DoubleAnimation
			{
				From = new double?(fromValue),
				To = new double?(newValue)
			};
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00008D90 File Offset: 0x00006F90
		private void ValidateProperty(PropertyInfo propertyInfo)
		{
			if (propertyInfo == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionStringTable.ChangePropertyActionCannotFindPropertyNameExceptionMessage, new object[]
				{
					this.PropertyName,
					base.Target.GetType().Name
				}));
			}
			if (!propertyInfo.CanWrite)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionStringTable.ChangePropertyActionPropertyIsReadOnlyExceptionMessage, new object[]
				{
					this.PropertyName,
					base.Target.GetType().Name
				}));
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00008E1C File Offset: 0x0000701C
		private object IncrementCurrentValue(PropertyInfo propertyInfo)
		{
			if (!propertyInfo.CanRead)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, ExceptionStringTable.ChangePropertyActionCannotIncrementWriteOnlyPropertyExceptionMessage, new object[] { propertyInfo.Name }));
			}
			object value = propertyInfo.GetValue(base.Target, null);
			Type propertyType = propertyInfo.PropertyType;
			TypeConverter typeConverter = TypeConverterHelper.GetTypeConverter(propertyInfo.PropertyType);
			object obj = this.Value;
			if (obj == null || value == null)
			{
				return obj;
			}
			if (typeConverter.CanConvertFrom(obj.GetType()))
			{
				obj = TypeConverterHelper.DoConversionFrom(typeConverter, obj);
			}
			object obj2;
			if (typeof(double).IsAssignableFrom(propertyType))
			{
				obj2 = (double)value + (double)obj;
			}
			else if (typeof(int).IsAssignableFrom(propertyType))
			{
				obj2 = (int)value + (int)obj;
			}
			else if (typeof(float).IsAssignableFrom(propertyType))
			{
				obj2 = (float)value + (float)obj;
			}
			else if (typeof(string).IsAssignableFrom(propertyType))
			{
				obj2 = (string)value + (string)obj;
			}
			else
			{
				obj2 = ChangePropertyAction.TryAddition(value, obj);
			}
			return obj2;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00008F50 File Offset: 0x00007150
		private static object TryAddition(object currentValue, object value)
		{
			Type type = value.GetType();
			Type type2 = currentValue.GetType();
			MethodInfo methodInfo = null;
			object obj = value;
			foreach (MethodInfo methodInfo2 in type2.GetMethods())
			{
				if (string.Compare(methodInfo2.Name, "op_Addition", StringComparison.Ordinal) == 0)
				{
					ParameterInfo[] parameters = methodInfo2.GetParameters();
					Type parameterType = parameters[1].ParameterType;
					if (parameters[0].ParameterType.IsAssignableFrom(type2))
					{
						if (!parameterType.IsAssignableFrom(type))
						{
							TypeConverter typeConverter = TypeConverterHelper.GetTypeConverter(parameterType);
							if (!typeConverter.CanConvertFrom(type))
							{
								goto IL_B7;
							}
							obj = TypeConverterHelper.DoConversionFrom(typeConverter, value);
						}
						if (methodInfo != null)
						{
							throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionStringTable.ChangePropertyActionAmbiguousAdditionOperationExceptionMessage, new object[] { type2.Name }));
						}
						methodInfo = methodInfo2;
					}
				}
				IL_B7:;
			}
			object obj2;
			if (methodInfo != null)
			{
				obj2 = methodInfo.Invoke(null, new object[] { currentValue, obj });
			}
			else
			{
				obj2 = value;
			}
			return obj2;
		}

		// Token: 0x040000AF RID: 175
		public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register("PropertyName", typeof(string), typeof(ChangePropertyAction), null);

		// Token: 0x040000B0 RID: 176
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(ChangePropertyAction), null);

		// Token: 0x040000B1 RID: 177
		public static readonly DependencyProperty DurationProperty = DependencyProperty.Register("Duration", typeof(Duration), typeof(ChangePropertyAction), null);

		// Token: 0x040000B2 RID: 178
		public static readonly DependencyProperty IncrementProperty = DependencyProperty.Register("Increment", typeof(bool), typeof(ChangePropertyAction), null);
	}
}
