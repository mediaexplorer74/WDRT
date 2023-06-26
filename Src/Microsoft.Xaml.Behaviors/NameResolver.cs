using System;
using System.Windows;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x02000018 RID: 24
	internal sealed class NameResolver
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060000BE RID: 190 RVA: 0x000040F0 File Offset: 0x000022F0
		// (remove) Token: 0x060000BF RID: 191 RVA: 0x00004128 File Offset: 0x00002328
		public event EventHandler<NameResolvedEventArgs> ResolvedElementChanged;

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x0000415D File Offset: 0x0000235D
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00004168 File Offset: 0x00002368
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				DependencyObject @object = this.Object;
				this.name = value;
				this.UpdateObjectFromName(@object);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x0000418A File Offset: 0x0000238A
		public DependencyObject Object
		{
			get
			{
				if (string.IsNullOrEmpty(this.Name) && this.HasAttempedResolve)
				{
					return this.NameScopeReferenceElement;
				}
				return this.ResolvedObject;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000041AE File Offset: 0x000023AE
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x000041B8 File Offset: 0x000023B8
		public FrameworkElement NameScopeReferenceElement
		{
			get
			{
				return this.nameScopeReferenceElement;
			}
			set
			{
				FrameworkElement frameworkElement = this.NameScopeReferenceElement;
				this.nameScopeReferenceElement = value;
				this.OnNameScopeReferenceElementChanged(frameworkElement);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000041DA File Offset: 0x000023DA
		private FrameworkElement ActualNameScopeReferenceElement
		{
			get
			{
				if (this.NameScopeReferenceElement == null || !Interaction.IsElementLoaded(this.NameScopeReferenceElement))
				{
					return null;
				}
				return this.GetActualNameScopeReference(this.NameScopeReferenceElement);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000041FF File Offset: 0x000023FF
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00004207 File Offset: 0x00002407
		private DependencyObject ResolvedObject { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00004210 File Offset: 0x00002410
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00004218 File Offset: 0x00002418
		private bool PendingReferenceElementLoad { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00004221 File Offset: 0x00002421
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00004229 File Offset: 0x00002429
		private bool HasAttempedResolve { get; set; }

		// Token: 0x060000CC RID: 204 RVA: 0x00004232 File Offset: 0x00002432
		private void OnNameScopeReferenceElementChanged(FrameworkElement oldNameScopeReference)
		{
			if (this.PendingReferenceElementLoad)
			{
				oldNameScopeReference.Loaded -= this.OnNameScopeReferenceLoaded;
				this.PendingReferenceElementLoad = false;
			}
			this.HasAttempedResolve = false;
			this.UpdateObjectFromName(this.Object);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004268 File Offset: 0x00002468
		private void UpdateObjectFromName(DependencyObject oldObject)
		{
			DependencyObject dependencyObject = null;
			this.ResolvedObject = null;
			if (this.NameScopeReferenceElement != null)
			{
				if (!Interaction.IsElementLoaded(this.NameScopeReferenceElement))
				{
					this.NameScopeReferenceElement.Loaded += this.OnNameScopeReferenceLoaded;
					this.PendingReferenceElementLoad = true;
					return;
				}
				if (!string.IsNullOrEmpty(this.Name))
				{
					FrameworkElement actualNameScopeReferenceElement = this.ActualNameScopeReferenceElement;
					if (actualNameScopeReferenceElement != null)
					{
						dependencyObject = actualNameScopeReferenceElement.FindName(this.Name) as DependencyObject;
					}
				}
			}
			this.HasAttempedResolve = true;
			this.ResolvedObject = dependencyObject;
			if (oldObject != this.Object)
			{
				this.OnObjectChanged(oldObject, this.Object);
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004301 File Offset: 0x00002501
		private void OnObjectChanged(DependencyObject oldTarget, DependencyObject newTarget)
		{
			if (this.ResolvedElementChanged != null)
			{
				this.ResolvedElementChanged(this, new NameResolvedEventArgs(oldTarget, newTarget));
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004320 File Offset: 0x00002520
		private FrameworkElement GetActualNameScopeReference(FrameworkElement initialReferenceElement)
		{
			FrameworkElement frameworkElement = initialReferenceElement;
			if (this.IsNameScope(initialReferenceElement))
			{
				frameworkElement = (initialReferenceElement.Parent as FrameworkElement) ?? frameworkElement;
			}
			return frameworkElement;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000434C File Offset: 0x0000254C
		private bool IsNameScope(FrameworkElement frameworkElement)
		{
			FrameworkElement frameworkElement2 = frameworkElement.Parent as FrameworkElement;
			return frameworkElement2 != null && frameworkElement2.FindName(this.Name) != null;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004379 File Offset: 0x00002579
		private void OnNameScopeReferenceLoaded(object sender, RoutedEventArgs e)
		{
			this.PendingReferenceElementLoad = false;
			this.NameScopeReferenceElement.Loaded -= this.OnNameScopeReferenceLoaded;
			this.UpdateObjectFromName(this.Object);
		}

		// Token: 0x04000047 RID: 71
		private string name;

		// Token: 0x04000048 RID: 72
		private FrameworkElement nameScopeReferenceElement;
	}
}
