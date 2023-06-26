using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Maintains a <see cref="T:System.Windows.Forms.Binding" /> between an object's property and a data-bound control property.</summary>
	// Token: 0x0200032F RID: 815
	public class PropertyManager : BindingManagerBase
	{
		/// <summary>Gets the object to which the data-bound property belongs.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the object to which the property belongs.</returns>
		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06003524 RID: 13604 RVA: 0x000F1611 File Offset: 0x000EF811
		public override object Current
		{
			get
			{
				return this.dataSource;
			}
		}

		// Token: 0x06003525 RID: 13605 RVA: 0x000F1619 File Offset: 0x000EF819
		private void PropertyChanged(object sender, EventArgs ea)
		{
			this.EndCurrentEdit();
			this.OnCurrentChanged(EventArgs.Empty);
		}

		// Token: 0x06003526 RID: 13606 RVA: 0x000F162C File Offset: 0x000EF82C
		internal override void SetDataSource(object dataSource)
		{
			if (this.dataSource != null && !string.IsNullOrEmpty(this.propName))
			{
				this.propInfo.RemoveValueChanged(this.dataSource, new EventHandler(this.PropertyChanged));
				this.propInfo = null;
			}
			this.dataSource = dataSource;
			if (this.dataSource != null && !string.IsNullOrEmpty(this.propName))
			{
				this.propInfo = TypeDescriptor.GetProperties(dataSource).Find(this.propName, true);
				if (this.propInfo == null)
				{
					throw new ArgumentException(SR.GetString("PropertyManagerPropDoesNotExist", new object[]
					{
						this.propName,
						dataSource.ToString()
					}));
				}
				this.propInfo.AddValueChanged(dataSource, new EventHandler(this.PropertyChanged));
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PropertyManager" /> class.</summary>
		// Token: 0x06003527 RID: 13607 RVA: 0x000F16EE File Offset: 0x000EF8EE
		public PropertyManager()
		{
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x000F16F6 File Offset: 0x000EF8F6
		internal PropertyManager(object dataSource)
			: base(dataSource)
		{
		}

		// Token: 0x06003529 RID: 13609 RVA: 0x000F16FF File Offset: 0x000EF8FF
		internal PropertyManager(object dataSource, string propName)
		{
			this.propName = propName;
			this.SetDataSource(dataSource);
		}

		// Token: 0x0600352A RID: 13610 RVA: 0x000F1715 File Offset: 0x000EF915
		internal override PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			return ListBindingHelper.GetListItemProperties(this.dataSource, listAccessors);
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x0600352B RID: 13611 RVA: 0x000F1723 File Offset: 0x000EF923
		internal override Type BindType
		{
			get
			{
				return this.dataSource.GetType();
			}
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x000F1730 File Offset: 0x000EF930
		internal override string GetListName()
		{
			return TypeDescriptor.GetClassName(this.dataSource) + "." + this.propName;
		}

		/// <summary>Suspends the data binding between a data source and a data-bound property.</summary>
		// Token: 0x0600352D RID: 13613 RVA: 0x000F1750 File Offset: 0x000EF950
		public override void SuspendBinding()
		{
			this.EndCurrentEdit();
			if (this.bound)
			{
				try
				{
					this.bound = false;
					this.UpdateIsBinding();
				}
				catch
				{
					this.bound = true;
					this.UpdateIsBinding();
					throw;
				}
			}
		}

		/// <summary>When overridden in a derived class, resumes data binding.</summary>
		// Token: 0x0600352E RID: 13614 RVA: 0x000F179C File Offset: 0x000EF99C
		public override void ResumeBinding()
		{
			this.OnCurrentChanged(new EventArgs());
			if (!this.bound)
			{
				try
				{
					this.bound = true;
					this.UpdateIsBinding();
				}
				catch
				{
					this.bound = false;
					this.UpdateIsBinding();
					throw;
				}
			}
		}

		/// <summary>When overridden in a derived class, gets the name of the list supplying the data for the binding.</summary>
		/// <param name="listAccessors">An <see cref="T:System.Collections.ArrayList" /> containing the table's bound properties.</param>
		/// <returns>The name of the list supplying the data for the binding.</returns>
		// Token: 0x0600352F RID: 13615 RVA: 0x000F17EC File Offset: 0x000EF9EC
		protected internal override string GetListName(ArrayList listAccessors)
		{
			return "";
		}

		/// <summary>When overridden in a derived class, cancels the current edit.</summary>
		// Token: 0x06003530 RID: 13616 RVA: 0x000F17F4 File Offset: 0x000EF9F4
		public override void CancelCurrentEdit()
		{
			IEditableObject editableObject = this.Current as IEditableObject;
			if (editableObject != null)
			{
				editableObject.CancelEdit();
			}
			base.PushData();
		}

		/// <summary>When overridden in a derived class, ends the current edit.</summary>
		// Token: 0x06003531 RID: 13617 RVA: 0x000F181C File Offset: 0x000EFA1C
		public override void EndCurrentEdit()
		{
			bool flag;
			base.PullData(out flag);
			if (flag)
			{
				IEditableObject editableObject = this.Current as IEditableObject;
				if (editableObject != null)
				{
					editableObject.EndEdit();
				}
			}
		}

		/// <summary>Updates the current <see cref="T:System.Windows.Forms.Binding" /> between a data binding and a data-bound property.</summary>
		// Token: 0x06003532 RID: 13618 RVA: 0x000F184C File Offset: 0x000EFA4C
		protected override void UpdateIsBinding()
		{
			for (int i = 0; i < base.Bindings.Count; i++)
			{
				base.Bindings[i].UpdateIsBinding();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingManagerBase.CurrentChanged" /> event.</summary>
		/// <param name="ea">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003533 RID: 13619 RVA: 0x000F1880 File Offset: 0x000EFA80
		protected internal override void OnCurrentChanged(EventArgs ea)
		{
			base.PushData();
			if (this.onCurrentChangedHandler != null)
			{
				this.onCurrentChangedHandler(this, ea);
			}
			if (this.onCurrentItemChangedHandler != null)
			{
				this.onCurrentItemChangedHandler(this, ea);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingManagerBase.CurrentItemChanged" /> event.</summary>
		/// <param name="ea">An <see cref="T:System.EventArgs" /> containing the event data.</param>
		// Token: 0x06003534 RID: 13620 RVA: 0x000F18B2 File Offset: 0x000EFAB2
		protected internal override void OnCurrentItemChanged(EventArgs ea)
		{
			base.PushData();
			if (this.onCurrentItemChangedHandler != null)
			{
				this.onCurrentItemChangedHandler(this, ea);
			}
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x06003535 RID: 13621 RVA: 0x000F1611 File Offset: 0x000EF811
		internal override object DataSource
		{
			get
			{
				return this.dataSource;
			}
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06003536 RID: 13622 RVA: 0x000F18CF File Offset: 0x000EFACF
		internal override bool IsBinding
		{
			get
			{
				return this.dataSource != null;
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the position in the underlying list that controls bound to this data source point to.</summary>
		/// <returns>A zero-based index that specifies a position in the underlying list.</returns>
		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06003537 RID: 13623 RVA: 0x0001180C File Offset: 0x0000FA0C
		// (set) Token: 0x06003538 RID: 13624 RVA: 0x000070A6 File Offset: 0x000052A6
		public override int Position
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		/// <summary>When overridden in a derived class, gets the number of rows managed by the <see cref="T:System.Windows.Forms.BindingManagerBase" />.</summary>
		/// <returns>The number of rows managed by the <see cref="T:System.Windows.Forms.BindingManagerBase" />.</returns>
		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06003539 RID: 13625 RVA: 0x00012E4E File Offset: 0x0001104E
		public override int Count
		{
			get
			{
				return 1;
			}
		}

		/// <summary>When overridden in a derived class, adds a new item to the underlying list.</summary>
		// Token: 0x0600353A RID: 13626 RVA: 0x000F18DA File Offset: 0x000EFADA
		public override void AddNew()
		{
			throw new NotSupportedException(SR.GetString("DataBindingAddNewNotSupportedOnPropertyManager"));
		}

		/// <summary>When overridden in a derived class, deletes the row at the specified index from the underlying list.</summary>
		/// <param name="index">The index of the row to delete.</param>
		// Token: 0x0600353B RID: 13627 RVA: 0x000F18EB File Offset: 0x000EFAEB
		public override void RemoveAt(int index)
		{
			throw new NotSupportedException(SR.GetString("DataBindingRemoveAtNotSupportedOnPropertyManager"));
		}

		// Token: 0x04001F38 RID: 7992
		private object dataSource;

		// Token: 0x04001F39 RID: 7993
		private string propName;

		// Token: 0x04001F3A RID: 7994
		private PropertyDescriptor propInfo;

		// Token: 0x04001F3B RID: 7995
		private bool bound;
	}
}
