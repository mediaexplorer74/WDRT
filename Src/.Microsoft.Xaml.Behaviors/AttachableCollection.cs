using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x02000003 RID: 3
	public abstract class AttachableCollection<T> : FreezableCollection<T>, IAttachedObject where T : DependencyObject, IAttachedObject
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002066 File Offset: 0x00000266
		protected DependencyObject AssociatedObject
		{
			get
			{
				base.ReadPreamble();
				return this.associatedObject;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002074 File Offset: 0x00000274
		internal AttachableCollection()
		{
			((INotifyCollectionChanged)this).CollectionChanged += this.OnCollectionChanged;
			this.snapshot = new Collection<T>();
		}

		// Token: 0x06000004 RID: 4
		protected abstract void OnAttached();

		// Token: 0x06000005 RID: 5
		protected abstract void OnDetaching();

		// Token: 0x06000006 RID: 6
		internal abstract void ItemAdded(T item);

		// Token: 0x06000007 RID: 7
		internal abstract void ItemRemoved(T item);

		// Token: 0x06000008 RID: 8 RVA: 0x0000209C File Offset: 0x0000029C
		[Conditional("DEBUG")]
		private void VerifySnapshotIntegrity()
		{
			if (base.Count == this.snapshot.Count)
			{
				int num = 0;
				while (num < base.Count && base[num] == this.snapshot[num])
				{
					num++;
				}
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020F0 File Offset: 0x000002F0
		private void VerifyAdd(T item)
		{
			if (this.snapshot.Contains(item))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, ExceptionStringTable.DuplicateItemInCollectionExceptionMessage, new object[]
				{
					typeof(T).Name,
					base.GetType().Name
				}));
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002148 File Offset: 0x00000348
		private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
			case NotifyCollectionChangedAction.Add:
			{
				using (IEnumerator enumerator = e.NewItems.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						T t = (T)((object)obj);
						try
						{
							this.VerifyAdd(t);
							this.ItemAdded(t);
						}
						finally
						{
							this.snapshot.Insert(base.IndexOf(t), t);
						}
					}
					return;
				}
				break;
			}
			case NotifyCollectionChangedAction.Remove:
				goto IL_12B;
			case NotifyCollectionChangedAction.Replace:
				break;
			case NotifyCollectionChangedAction.Move:
				return;
			case NotifyCollectionChangedAction.Reset:
				goto IL_17A;
			default:
				return;
			}
			foreach (object obj2 in e.OldItems)
			{
				T t2 = (T)((object)obj2);
				this.ItemRemoved(t2);
				this.snapshot.Remove(t2);
			}
			using (IEnumerator enumerator = e.NewItems.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj3 = enumerator.Current;
					T t3 = (T)((object)obj3);
					try
					{
						this.VerifyAdd(t3);
						this.ItemAdded(t3);
					}
					finally
					{
						this.snapshot.Insert(base.IndexOf(t3), t3);
					}
				}
				return;
			}
			IL_12B:
			using (IEnumerator enumerator = e.OldItems.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj4 = enumerator.Current;
					T t4 = (T)((object)obj4);
					this.ItemRemoved(t4);
					this.snapshot.Remove(t4);
				}
				return;
			}
			IL_17A:
			foreach (T t5 in this.snapshot)
			{
				this.ItemRemoved(t5);
			}
			this.snapshot = new Collection<T>();
			foreach (T t6 in this)
			{
				this.VerifyAdd(t6);
				this.ItemAdded(t6);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000023B4 File Offset: 0x000005B4
		DependencyObject IAttachedObject.AssociatedObject
		{
			get
			{
				return this.AssociatedObject;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023BC File Offset: 0x000005BC
		public void Attach(DependencyObject dependencyObject)
		{
			if (dependencyObject != this.AssociatedObject)
			{
				if (this.AssociatedObject != null)
				{
					throw new InvalidOperationException();
				}
				if (Interaction.ShouldRunInDesignMode || !(bool)base.GetValue(DesignerProperties.IsInDesignModeProperty))
				{
					base.WritePreamble();
					this.associatedObject = dependencyObject;
					base.WritePostscript();
				}
				this.OnAttached();
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002412 File Offset: 0x00000612
		public void Detach()
		{
			this.OnDetaching();
			base.WritePreamble();
			this.associatedObject = null;
			base.WritePostscript();
		}

		// Token: 0x0400000E RID: 14
		private Collection<T> snapshot;

		// Token: 0x0400000F RID: 15
		private DependencyObject associatedObject;
	}
}
