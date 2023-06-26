using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media.Animation;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x02000005 RID: 5
	public abstract class Behavior : Animatable, IAttachedObject
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000010 RID: 16 RVA: 0x0000244C File Offset: 0x0000064C
		// (remove) Token: 0x06000011 RID: 17 RVA: 0x00002484 File Offset: 0x00000684
		internal event EventHandler AssociatedObjectChanged;

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000024B9 File Offset: 0x000006B9
		protected Type AssociatedType
		{
			get
			{
				base.ReadPreamble();
				return this.associatedType;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000024C7 File Offset: 0x000006C7
		protected DependencyObject AssociatedObject
		{
			get
			{
				base.ReadPreamble();
				return this.associatedObject;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000024D5 File Offset: 0x000006D5
		internal Behavior(Type associatedType)
		{
			this.associatedType = associatedType;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024E4 File Offset: 0x000006E4
		protected virtual void OnAttached()
		{
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024E6 File Offset: 0x000006E6
		protected virtual void OnDetaching()
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024E8 File Offset: 0x000006E8
		protected override Freezable CreateInstanceCore()
		{
			return (Freezable)Activator.CreateInstance(base.GetType());
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000024FA File Offset: 0x000006FA
		private void OnAssociatedObjectChanged()
		{
			if (this.AssociatedObjectChanged != null)
			{
				this.AssociatedObjectChanged(this, new EventArgs());
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002515 File Offset: 0x00000715
		DependencyObject IAttachedObject.AssociatedObject
		{
			get
			{
				return this.AssociatedObject;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002520 File Offset: 0x00000720
		public void Attach(DependencyObject dependencyObject)
		{
			if (dependencyObject != this.AssociatedObject)
			{
				if (this.AssociatedObject != null)
				{
					throw new InvalidOperationException(ExceptionStringTable.CannotHostBehaviorMultipleTimesExceptionMessage);
				}
				if (dependencyObject != null && !this.AssociatedType.IsAssignableFrom(dependencyObject.GetType()))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, ExceptionStringTable.TypeConstraintViolatedExceptionMessage, new object[]
					{
						base.GetType().Name,
						dependencyObject.GetType().Name,
						this.AssociatedType.Name
					}));
				}
				base.WritePreamble();
				this.associatedObject = dependencyObject;
				base.WritePostscript();
				this.OnAssociatedObjectChanged();
				this.OnAttached();
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000025C6 File Offset: 0x000007C6
		public void Detach()
		{
			this.OnDetaching();
			base.WritePreamble();
			this.associatedObject = null;
			base.WritePostscript();
			this.OnAssociatedObjectChanged();
		}

		// Token: 0x04000010 RID: 16
		private Type associatedType;

		// Token: 0x04000011 RID: 17
		private DependencyObject associatedObject;
	}
}
