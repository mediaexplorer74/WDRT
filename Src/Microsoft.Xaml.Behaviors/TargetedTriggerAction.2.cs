using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x0200001C RID: 28
	public abstract class TargetedTriggerAction : TriggerAction
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000046AB File Offset: 0x000028AB
		// (set) Token: 0x060000DF RID: 223 RVA: 0x000046B8 File Offset: 0x000028B8
		public object TargetObject
		{
			get
			{
				return base.GetValue(TargetedTriggerAction.TargetObjectProperty);
			}
			set
			{
				base.SetValue(TargetedTriggerAction.TargetObjectProperty, value);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000046C6 File Offset: 0x000028C6
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x000046D8 File Offset: 0x000028D8
		public string TargetName
		{
			get
			{
				return (string)base.GetValue(TargetedTriggerAction.TargetNameProperty);
			}
			set
			{
				base.SetValue(TargetedTriggerAction.TargetNameProperty, value);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x000046E8 File Offset: 0x000028E8
		protected object Target
		{
			get
			{
				object obj = base.AssociatedObject;
				if (this.TargetObject != null)
				{
					obj = this.TargetObject;
				}
				else if (this.IsTargetNameSet)
				{
					obj = this.TargetResolver.Object;
				}
				if (obj != null && !this.TargetTypeConstraint.IsAssignableFrom(obj.GetType()))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, ExceptionStringTable.RetargetedTypeConstraintViolatedExceptionMessage, new object[]
					{
						base.GetType().Name,
						obj.GetType(),
						this.TargetTypeConstraint,
						"Target"
					}));
				}
				return obj;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x0000477C File Offset: 0x0000297C
		protected sealed override Type AssociatedObjectTypeConstraint
		{
			get
			{
				TypeConstraintAttribute typeConstraintAttribute = TypeDescriptor.GetAttributes(base.GetType())[typeof(TypeConstraintAttribute)] as TypeConstraintAttribute;
				if (typeConstraintAttribute != null)
				{
					return typeConstraintAttribute.Constraint;
				}
				return typeof(DependencyObject);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000047BD File Offset: 0x000029BD
		protected Type TargetTypeConstraint
		{
			get
			{
				base.ReadPreamble();
				return this.targetTypeConstraint;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x000047CB File Offset: 0x000029CB
		private bool IsTargetNameSet
		{
			get
			{
				return !string.IsNullOrEmpty(this.TargetName) || base.ReadLocalValue(TargetedTriggerAction.TargetNameProperty) != DependencyProperty.UnsetValue;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x000047F1 File Offset: 0x000029F1
		private NameResolver TargetResolver
		{
			get
			{
				return this.targetResolver;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x000047F9 File Offset: 0x000029F9
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00004801 File Offset: 0x00002A01
		private bool IsTargetChangedRegistered
		{
			get
			{
				return this.isTargetChangedRegistered;
			}
			set
			{
				this.isTargetChangedRegistered = value;
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000480A File Offset: 0x00002A0A
		internal TargetedTriggerAction(Type targetTypeConstraint)
			: base(typeof(DependencyObject))
		{
			this.targetTypeConstraint = targetTypeConstraint;
			this.targetResolver = new NameResolver();
			this.RegisterTargetChanged();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004834 File Offset: 0x00002A34
		internal virtual void OnTargetChangedImpl(object oldTarget, object newTarget)
		{
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004838 File Offset: 0x00002A38
		protected override void OnAttached()
		{
			base.OnAttached();
			DependencyObject dependencyObject = base.AssociatedObject;
			Behavior behavior = dependencyObject as Behavior;
			this.RegisterTargetChanged();
			if (behavior != null)
			{
				dependencyObject = ((IAttachedObject)behavior).AssociatedObject;
				behavior.AssociatedObjectChanged += this.OnBehaviorHostChanged;
			}
			this.TargetResolver.NameScopeReferenceElement = dependencyObject as FrameworkElement;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000488C File Offset: 0x00002A8C
		protected override void OnDetaching()
		{
			Behavior behavior = base.AssociatedObject as Behavior;
			base.OnDetaching();
			this.OnTargetChangedImpl(this.TargetResolver.Object, null);
			this.UnregisterTargetChanged();
			if (behavior != null)
			{
				behavior.AssociatedObjectChanged -= this.OnBehaviorHostChanged;
			}
			this.TargetResolver.NameScopeReferenceElement = null;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000048E4 File Offset: 0x00002AE4
		private void OnBehaviorHostChanged(object sender, EventArgs e)
		{
			this.TargetResolver.NameScopeReferenceElement = ((IAttachedObject)sender).AssociatedObject as FrameworkElement;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004901 File Offset: 0x00002B01
		private void RegisterTargetChanged()
		{
			if (!this.IsTargetChangedRegistered)
			{
				this.TargetResolver.ResolvedElementChanged += this.OnTargetChanged;
				this.IsTargetChangedRegistered = true;
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004929 File Offset: 0x00002B29
		private void UnregisterTargetChanged()
		{
			if (this.IsTargetChangedRegistered)
			{
				this.TargetResolver.ResolvedElementChanged -= this.OnTargetChanged;
				this.IsTargetChangedRegistered = false;
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004951 File Offset: 0x00002B51
		private static void OnTargetObjectChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			((TargetedTriggerAction)obj).OnTargetChanged(obj, new NameResolvedEventArgs(args.OldValue, args.NewValue));
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004972 File Offset: 0x00002B72
		private static void OnTargetNameChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			((TargetedTriggerAction)obj).TargetResolver.Name = (string)args.NewValue;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004990 File Offset: 0x00002B90
		private void OnTargetChanged(object sender, NameResolvedEventArgs e)
		{
			if (base.AssociatedObject != null)
			{
				this.OnTargetChangedImpl(e.OldObject, e.NewObject);
			}
		}

		// Token: 0x04000051 RID: 81
		private Type targetTypeConstraint;

		// Token: 0x04000052 RID: 82
		private bool isTargetChangedRegistered;

		// Token: 0x04000053 RID: 83
		private NameResolver targetResolver;

		// Token: 0x04000054 RID: 84
		public static readonly DependencyProperty TargetObjectProperty = DependencyProperty.Register("TargetObject", typeof(object), typeof(TargetedTriggerAction), new FrameworkPropertyMetadata(new PropertyChangedCallback(TargetedTriggerAction.OnTargetObjectChanged)));

		// Token: 0x04000055 RID: 85
		public static readonly DependencyProperty TargetNameProperty = DependencyProperty.Register("TargetName", typeof(string), typeof(TargetedTriggerAction), new FrameworkPropertyMetadata(new PropertyChangedCallback(TargetedTriggerAction.OnTargetNameChanged)));
	}
}
