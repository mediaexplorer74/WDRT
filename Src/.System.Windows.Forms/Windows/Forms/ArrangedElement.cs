using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x02000123 RID: 291
	internal abstract class ArrangedElement : Component, IArrangedElement, IComponent, IDisposable
	{
		// Token: 0x0600095C RID: 2396 RVA: 0x000199EC File Offset: 0x00017BEC
		internal ArrangedElement()
		{
			this.Padding = this.DefaultPadding;
			this.Margin = this.DefaultMargin;
			this.state[ArrangedElement.stateVisible] = true;
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x00019A49 File Offset: 0x00017C49
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x00019A51 File Offset: 0x00017C51
		ArrangedElementCollection IArrangedElement.Children
		{
			get
			{
				return this.GetChildren();
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x00019A59 File Offset: 0x00017C59
		IArrangedElement IArrangedElement.Container
		{
			get
			{
				return this.GetContainer();
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x00019A61 File Offset: 0x00017C61
		protected virtual Padding DefaultMargin
		{
			get
			{
				return Padding.Empty;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x00019A61 File Offset: 0x00017C61
		protected virtual Padding DefaultPadding
		{
			get
			{
				return Padding.Empty;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x00019A68 File Offset: 0x00017C68
		public virtual Rectangle DisplayRectangle
		{
			get
			{
				return this.Bounds;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000963 RID: 2403
		public abstract LayoutEngine LayoutEngine { get; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x00019A7D File Offset: 0x00017C7D
		// (set) Token: 0x06000965 RID: 2405 RVA: 0x00019A85 File Offset: 0x00017C85
		public Padding Margin
		{
			get
			{
				return CommonProperties.GetMargin(this);
			}
			set
			{
				value = LayoutUtils.ClampNegativePaddingToZero(value);
				if (this.Margin != value)
				{
					CommonProperties.SetMargin(this, value);
				}
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x00019AA4 File Offset: 0x00017CA4
		// (set) Token: 0x06000967 RID: 2407 RVA: 0x00019AB2 File Offset: 0x00017CB2
		public virtual Padding Padding
		{
			get
			{
				return CommonProperties.GetPadding(this, this.DefaultPadding);
			}
			set
			{
				value = LayoutUtils.ClampNegativePaddingToZero(value);
				if (this.Padding != value)
				{
					CommonProperties.SetPadding(this, value);
				}
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x00019AD1 File Offset: 0x00017CD1
		// (set) Token: 0x06000969 RID: 2409 RVA: 0x00019AD9 File Offset: 0x00017CD9
		public virtual IArrangedElement Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x00019AE2 File Offset: 0x00017CE2
		public virtual bool ParticipatesInLayout
		{
			get
			{
				return this.Visible;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00019AEA File Offset: 0x00017CEA
		PropertyStore IArrangedElement.Properties
		{
			get
			{
				return this.Properties;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x00019AF2 File Offset: 0x00017CF2
		private PropertyStore Properties
		{
			get
			{
				return this.propertyStore;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x00019AFA File Offset: 0x00017CFA
		// (set) Token: 0x0600096E RID: 2414 RVA: 0x00019B0C File Offset: 0x00017D0C
		public virtual bool Visible
		{
			get
			{
				return this.state[ArrangedElement.stateVisible];
			}
			set
			{
				if (this.state[ArrangedElement.stateVisible] != value)
				{
					this.state[ArrangedElement.stateVisible] = value;
					if (this.Parent != null)
					{
						LayoutTransaction.DoLayout(this.Parent, this, PropertyNames.Visible);
					}
				}
			}
		}

		// Token: 0x0600096F RID: 2415
		protected abstract IArrangedElement GetContainer();

		// Token: 0x06000970 RID: 2416
		protected abstract ArrangedElementCollection GetChildren();

		// Token: 0x06000971 RID: 2417 RVA: 0x00019B4C File Offset: 0x00017D4C
		public virtual Size GetPreferredSize(Size constrainingSize)
		{
			return this.LayoutEngine.GetPreferredSize(this, constrainingSize - this.Padding.Size) + this.Padding.Size;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00019B8E File Offset: 0x00017D8E
		public virtual void PerformLayout(IArrangedElement container, string propertyName)
		{
			if (this.suspendCount <= 0)
			{
				this.OnLayout(new LayoutEventArgs(container, propertyName));
			}
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00019BA8 File Offset: 0x00017DA8
		protected virtual void OnLayout(LayoutEventArgs e)
		{
			bool flag = this.LayoutEngine.Layout(this, e);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00019BC3 File Offset: 0x00017DC3
		protected virtual void OnBoundsChanged(Rectangle oldBounds, Rectangle newBounds)
		{
			((IArrangedElement)this).PerformLayout(this, PropertyNames.Size);
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00019BD1 File Offset: 0x00017DD1
		public void SetBounds(Rectangle bounds, BoundsSpecified specified)
		{
			this.SetBoundsCore(bounds, specified);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00019BDC File Offset: 0x00017DDC
		protected virtual void SetBoundsCore(Rectangle bounds, BoundsSpecified specified)
		{
			if (bounds != this.bounds)
			{
				Rectangle rectangle = this.bounds;
				this.bounds = bounds;
				this.OnBoundsChanged(rectangle, bounds);
			}
		}

		// Token: 0x040005EA RID: 1514
		private Rectangle bounds = Rectangle.Empty;

		// Token: 0x040005EB RID: 1515
		private IArrangedElement parent;

		// Token: 0x040005EC RID: 1516
		private BitVector32 state;

		// Token: 0x040005ED RID: 1517
		private PropertyStore propertyStore = new PropertyStore();

		// Token: 0x040005EE RID: 1518
		private int suspendCount;

		// Token: 0x040005EF RID: 1519
		private static readonly int stateVisible = BitVector32.CreateMask();

		// Token: 0x040005F0 RID: 1520
		private static readonly int stateDisposing = BitVector32.CreateMask(ArrangedElement.stateVisible);

		// Token: 0x040005F1 RID: 1521
		private static readonly int stateLocked = BitVector32.CreateMask(ArrangedElement.stateDisposing);

		// Token: 0x040005F2 RID: 1522
		private static readonly int PropControlsCollection = PropertyStore.CreateKey();

		// Token: 0x040005F3 RID: 1523
		private Control spacer = new Control();
	}
}
