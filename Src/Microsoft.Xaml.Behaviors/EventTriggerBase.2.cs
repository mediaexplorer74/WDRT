using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x02000010 RID: 16
	public abstract class EventTriggerBase : TriggerBase
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002CA4 File Offset: 0x00000EA4
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

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002CE5 File Offset: 0x00000EE5
		protected Type SourceTypeConstraint
		{
			get
			{
				return this.sourceTypeConstraint;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002CED File Offset: 0x00000EED
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002CFA File Offset: 0x00000EFA
		public object SourceObject
		{
			get
			{
				return base.GetValue(EventTriggerBase.SourceObjectProperty);
			}
			set
			{
				base.SetValue(EventTriggerBase.SourceObjectProperty, value);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002D08 File Offset: 0x00000F08
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002D1A File Offset: 0x00000F1A
		public string SourceName
		{
			get
			{
				return (string)base.GetValue(EventTriggerBase.SourceNameProperty);
			}
			set
			{
				base.SetValue(EventTriggerBase.SourceNameProperty, value);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002D28 File Offset: 0x00000F28
		public object Source
		{
			get
			{
				object obj = base.AssociatedObject;
				if (this.SourceObject != null)
				{
					obj = this.SourceObject;
				}
				else if (this.IsSourceNameSet)
				{
					obj = this.SourceNameResolver.Object;
					if (obj != null && !this.SourceTypeConstraint.IsAssignableFrom(obj.GetType()))
					{
						throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, ExceptionStringTable.RetargetedTypeConstraintViolatedExceptionMessage, new object[]
						{
							base.GetType().Name,
							obj.GetType(),
							this.SourceTypeConstraint,
							"Source"
						}));
					}
				}
				return obj;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002DBB File Offset: 0x00000FBB
		private NameResolver SourceNameResolver
		{
			get
			{
				return this.sourceNameResolver;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002DC3 File Offset: 0x00000FC3
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002DCB File Offset: 0x00000FCB
		private bool IsSourceChangedRegistered
		{
			get
			{
				return this.isSourceChangedRegistered;
			}
			set
			{
				this.isSourceChangedRegistered = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002DD4 File Offset: 0x00000FD4
		private bool IsSourceNameSet
		{
			get
			{
				return !string.IsNullOrEmpty(this.SourceName) || base.ReadLocalValue(EventTriggerBase.SourceNameProperty) != DependencyProperty.UnsetValue;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002DFA File Offset: 0x00000FFA
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002E02 File Offset: 0x00001002
		private bool IsLoadedRegistered { get; set; }

		// Token: 0x0600004C RID: 76 RVA: 0x00002E0B File Offset: 0x0000100B
		internal EventTriggerBase(Type sourceTypeConstraint)
			: base(typeof(DependencyObject))
		{
			this.sourceTypeConstraint = sourceTypeConstraint;
			this.sourceNameResolver = new NameResolver();
			this.RegisterSourceChanged();
		}

		// Token: 0x0600004D RID: 77
		protected abstract string GetEventName();

		// Token: 0x0600004E RID: 78 RVA: 0x00002E35 File Offset: 0x00001035
		protected virtual void OnEvent(EventArgs eventArgs)
		{
			base.InvokeActions(eventArgs);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002E3E File Offset: 0x0000103E
		private void OnSourceChanged(object oldSource, object newSource)
		{
			if (base.AssociatedObject != null)
			{
				this.OnSourceChangedImpl(oldSource, newSource);
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002E50 File Offset: 0x00001050
		internal virtual void OnSourceChangedImpl(object oldSource, object newSource)
		{
			if (string.IsNullOrEmpty(this.GetEventName()))
			{
				return;
			}
			if (string.Compare(this.GetEventName(), "Loaded", StringComparison.Ordinal) != 0)
			{
				if (oldSource != null && this.SourceTypeConstraint.IsAssignableFrom(oldSource.GetType()))
				{
					this.UnregisterEvent(oldSource, this.GetEventName());
				}
				if (newSource != null && this.SourceTypeConstraint.IsAssignableFrom(newSource.GetType()))
				{
					this.RegisterEvent(newSource, this.GetEventName());
				}
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002EC4 File Offset: 0x000010C4
		protected override void OnAttached()
		{
			base.OnAttached();
			DependencyObject associatedObject = base.AssociatedObject;
			Behavior behavior = associatedObject as Behavior;
			FrameworkElement frameworkElement = associatedObject as FrameworkElement;
			this.RegisterSourceChanged();
			if (behavior != null)
			{
				DependencyObject associatedObject2 = ((IAttachedObject)behavior).AssociatedObject;
				behavior.AssociatedObjectChanged += this.OnBehaviorHostChanged;
			}
			else
			{
				if (this.SourceObject != null || frameworkElement == null)
				{
					try
					{
						this.OnSourceChanged(null, this.Source);
						goto IL_67;
					}
					catch (InvalidOperationException)
					{
						goto IL_67;
					}
				}
				this.SourceNameResolver.NameScopeReferenceElement = frameworkElement;
			}
			IL_67:
			if (string.Compare(this.GetEventName(), "Loaded", StringComparison.Ordinal) == 0 && frameworkElement != null && !Interaction.IsElementLoaded(frameworkElement))
			{
				this.RegisterLoaded(frameworkElement);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002F70 File Offset: 0x00001170
		protected override void OnDetaching()
		{
			base.OnDetaching();
			Behavior behavior = base.AssociatedObject as Behavior;
			FrameworkElement frameworkElement = base.AssociatedObject as FrameworkElement;
			try
			{
				this.OnSourceChanged(this.Source, null);
			}
			catch (InvalidOperationException)
			{
			}
			this.UnregisterSourceChanged();
			if (behavior != null)
			{
				behavior.AssociatedObjectChanged -= this.OnBehaviorHostChanged;
			}
			this.SourceNameResolver.NameScopeReferenceElement = null;
			if (string.Compare(this.GetEventName(), "Loaded", StringComparison.Ordinal) == 0 && frameworkElement != null)
			{
				this.UnregisterLoaded(frameworkElement);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003004 File Offset: 0x00001204
		private void OnBehaviorHostChanged(object sender, EventArgs e)
		{
			this.SourceNameResolver.NameScopeReferenceElement = ((IAttachedObject)sender).AssociatedObject as FrameworkElement;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003024 File Offset: 0x00001224
		private static void OnSourceObjectChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			EventTriggerBase eventTriggerBase = (EventTriggerBase)obj;
			object @object = eventTriggerBase.SourceNameResolver.Object;
			if (args.NewValue == null)
			{
				eventTriggerBase.OnSourceChanged(args.OldValue, @object);
				return;
			}
			if (args.OldValue == null && @object != null)
			{
				eventTriggerBase.UnregisterEvent(@object, eventTriggerBase.GetEventName());
			}
			eventTriggerBase.OnSourceChanged(args.OldValue, args.NewValue);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003089 File Offset: 0x00001289
		private static void OnSourceNameChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			((EventTriggerBase)obj).SourceNameResolver.Name = (string)args.NewValue;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000030A7 File Offset: 0x000012A7
		private void RegisterSourceChanged()
		{
			if (!this.IsSourceChangedRegistered)
			{
				this.SourceNameResolver.ResolvedElementChanged += this.OnSourceNameResolverElementChanged;
				this.IsSourceChangedRegistered = true;
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000030CF File Offset: 0x000012CF
		private void UnregisterSourceChanged()
		{
			if (this.IsSourceChangedRegistered)
			{
				this.SourceNameResolver.ResolvedElementChanged -= this.OnSourceNameResolverElementChanged;
				this.IsSourceChangedRegistered = false;
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000030F7 File Offset: 0x000012F7
		private void OnSourceNameResolverElementChanged(object sender, NameResolvedEventArgs e)
		{
			if (this.SourceObject == null)
			{
				this.OnSourceChanged(e.OldObject, e.NewObject);
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003113 File Offset: 0x00001313
		private void RegisterLoaded(FrameworkElement associatedElement)
		{
			if (!this.IsLoadedRegistered && associatedElement != null)
			{
				associatedElement.Loaded += new RoutedEventHandler(this.OnEventImpl);
				this.IsLoadedRegistered = true;
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003139 File Offset: 0x00001339
		private void UnregisterLoaded(FrameworkElement associatedElement)
		{
			if (this.IsLoadedRegistered && associatedElement != null)
			{
				associatedElement.Loaded -= new RoutedEventHandler(this.OnEventImpl);
				this.IsLoadedRegistered = false;
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003160 File Offset: 0x00001360
		private void RegisterEvent(object obj, string eventName)
		{
			EventInfo @event = obj.GetType().GetEvent(eventName);
			if (@event == null)
			{
				if (this.SourceObject != null)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionStringTable.EventTriggerCannotFindEventNameExceptionMessage, new object[]
					{
						eventName,
						obj.GetType().Name
					}));
				}
				return;
			}
			else
			{
				if (EventTriggerBase.IsValidEvent(@event))
				{
					this.eventHandlerMethodInfo = typeof(EventTriggerBase).GetMethod("OnEventImpl", BindingFlags.Instance | BindingFlags.NonPublic);
					@event.AddEventHandler(obj, Delegate.CreateDelegate(@event.EventHandlerType, this, this.eventHandlerMethodInfo));
					return;
				}
				if (this.SourceObject != null)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ExceptionStringTable.EventTriggerBaseInvalidEventExceptionMessage, new object[]
					{
						eventName,
						obj.GetType().Name
					}));
				}
				return;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000322C File Offset: 0x0000142C
		private static bool IsValidEvent(EventInfo eventInfo)
		{
			Type eventHandlerType = eventInfo.EventHandlerType;
			if (typeof(Delegate).IsAssignableFrom(eventInfo.EventHandlerType))
			{
				ParameterInfo[] parameters = eventHandlerType.GetMethod("Invoke").GetParameters();
				return parameters.Length == 2 && typeof(object).IsAssignableFrom(parameters[0].ParameterType) && typeof(EventArgs).IsAssignableFrom(parameters[1].ParameterType);
			}
			return false;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000032A4 File Offset: 0x000014A4
		private void UnregisterEvent(object obj, string eventName)
		{
			if (string.Compare(eventName, "Loaded", StringComparison.Ordinal) == 0)
			{
				FrameworkElement frameworkElement = obj as FrameworkElement;
				if (frameworkElement != null)
				{
					this.UnregisterLoaded(frameworkElement);
					return;
				}
			}
			else
			{
				this.UnregisterEventImpl(obj, eventName);
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000032DC File Offset: 0x000014DC
		private void UnregisterEventImpl(object obj, string eventName)
		{
			Type type = obj.GetType();
			if (this.eventHandlerMethodInfo == null)
			{
				return;
			}
			EventInfo @event = type.GetEvent(eventName);
			@event.RemoveEventHandler(obj, Delegate.CreateDelegate(@event.EventHandlerType, this, this.eventHandlerMethodInfo));
			this.eventHandlerMethodInfo = null;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003327 File Offset: 0x00001527
		private void OnEventImpl(object sender, EventArgs eventArgs)
		{
			this.OnEvent(eventArgs);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003330 File Offset: 0x00001530
		internal void OnEventNameChanged(string oldEventName, string newEventName)
		{
			if (base.AssociatedObject != null)
			{
				FrameworkElement frameworkElement = this.Source as FrameworkElement;
				if (frameworkElement != null && string.Compare(oldEventName, "Loaded", StringComparison.Ordinal) == 0)
				{
					this.UnregisterLoaded(frameworkElement);
				}
				else if (!string.IsNullOrEmpty(oldEventName))
				{
					this.UnregisterEvent(this.Source, oldEventName);
				}
				if (frameworkElement != null && string.Compare(newEventName, "Loaded", StringComparison.Ordinal) == 0)
				{
					this.RegisterLoaded(frameworkElement);
					return;
				}
				if (!string.IsNullOrEmpty(newEventName))
				{
					this.RegisterEvent(this.Source, newEventName);
				}
			}
		}

		// Token: 0x04000022 RID: 34
		private Type sourceTypeConstraint;

		// Token: 0x04000023 RID: 35
		private bool isSourceChangedRegistered;

		// Token: 0x04000024 RID: 36
		private NameResolver sourceNameResolver;

		// Token: 0x04000025 RID: 37
		private MethodInfo eventHandlerMethodInfo;

		// Token: 0x04000026 RID: 38
		public static readonly DependencyProperty SourceObjectProperty = DependencyProperty.Register("SourceObject", typeof(object), typeof(EventTriggerBase), new PropertyMetadata(new PropertyChangedCallback(EventTriggerBase.OnSourceObjectChanged)));

		// Token: 0x04000027 RID: 39
		public static readonly DependencyProperty SourceNameProperty = DependencyProperty.Register("SourceName", typeof(string), typeof(EventTriggerBase), new PropertyMetadata(new PropertyChangedCallback(EventTriggerBase.OnSourceNameChanged)));
	}
}
