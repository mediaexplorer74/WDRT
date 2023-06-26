using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x0200033E RID: 830
	internal class RelatedCurrencyManager : CurrencyManager
	{
		// Token: 0x060035C2 RID: 13762 RVA: 0x000F318C File Offset: 0x000F138C
		internal RelatedCurrencyManager(BindingManagerBase parentManager, string dataField)
			: base(null)
		{
			this.Bind(parentManager, dataField);
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x000F31A0 File Offset: 0x000F13A0
		internal void Bind(BindingManagerBase parentManager, string dataField)
		{
			this.UnwireParentManager(this.parentManager);
			this.parentManager = parentManager;
			this.dataField = dataField;
			this.fieldInfo = parentManager.GetItemProperties().Find(dataField, true);
			if (this.fieldInfo == null || !typeof(IList).IsAssignableFrom(this.fieldInfo.PropertyType))
			{
				throw new ArgumentException(SR.GetString("RelatedListManagerChild", new object[] { dataField }));
			}
			this.finalType = this.fieldInfo.PropertyType;
			this.WireParentManager(this.parentManager);
			this.ParentManager_CurrentItemChanged(parentManager, EventArgs.Empty);
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x000F3241 File Offset: 0x000F1441
		private void UnwireParentManager(BindingManagerBase bmb)
		{
			if (bmb != null)
			{
				bmb.CurrentItemChanged -= this.ParentManager_CurrentItemChanged;
				if (bmb is CurrencyManager)
				{
					(bmb as CurrencyManager).MetaDataChanged -= this.ParentManager_MetaDataChanged;
				}
			}
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x000F3277 File Offset: 0x000F1477
		private void WireParentManager(BindingManagerBase bmb)
		{
			if (bmb != null)
			{
				bmb.CurrentItemChanged += this.ParentManager_CurrentItemChanged;
				if (bmb is CurrencyManager)
				{
					(bmb as CurrencyManager).MetaDataChanged += this.ParentManager_MetaDataChanged;
				}
			}
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x000F32B0 File Offset: 0x000F14B0
		internal override PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			PropertyDescriptor[] array;
			if (listAccessors != null && listAccessors.Length != 0)
			{
				array = new PropertyDescriptor[listAccessors.Length + 1];
				listAccessors.CopyTo(array, 1);
			}
			else
			{
				array = new PropertyDescriptor[1];
			}
			array[0] = this.fieldInfo;
			return this.parentManager.GetItemProperties(array);
		}

		// Token: 0x060035C7 RID: 13767 RVA: 0x0001FC0F File Offset: 0x0001DE0F
		public override PropertyDescriptorCollection GetItemProperties()
		{
			return this.GetItemProperties(null);
		}

		// Token: 0x060035C8 RID: 13768 RVA: 0x000F32F8 File Offset: 0x000F14F8
		internal override string GetListName()
		{
			string listName = this.GetListName(new ArrayList());
			if (listName.Length > 0)
			{
				return listName;
			}
			return base.GetListName();
		}

		// Token: 0x060035C9 RID: 13769 RVA: 0x000F3322 File Offset: 0x000F1522
		protected internal override string GetListName(ArrayList listAccessors)
		{
			listAccessors.Insert(0, this.fieldInfo);
			return this.parentManager.GetListName(listAccessors);
		}

		// Token: 0x060035CA RID: 13770 RVA: 0x000F333D File Offset: 0x000F153D
		private void ParentManager_MetaDataChanged(object sender, EventArgs e)
		{
			base.OnMetaDataChanged(e);
		}

		// Token: 0x060035CB RID: 13771 RVA: 0x000F3348 File Offset: 0x000F1548
		private void ParentManager_CurrentItemChanged(object sender, EventArgs e)
		{
			if (RelatedCurrencyManager.IgnoreItemChangedTable.Contains(this.parentManager))
			{
				return;
			}
			int listposition = this.listposition;
			try
			{
				base.PullData();
			}
			catch (Exception ex)
			{
				base.OnDataError(ex);
			}
			if (this.parentManager is CurrencyManager)
			{
				CurrencyManager currencyManager = (CurrencyManager)this.parentManager;
				if (currencyManager.Count > 0)
				{
					this.SetDataSource(this.fieldInfo.GetValue(currencyManager.Current));
					this.listposition = ((this.Count > 0) ? 0 : (-1));
					goto IL_DC;
				}
				currencyManager.AddNew();
				try
				{
					RelatedCurrencyManager.IgnoreItemChangedTable.Add(currencyManager);
					currencyManager.CancelCurrentEdit();
					goto IL_DC;
				}
				finally
				{
					if (RelatedCurrencyManager.IgnoreItemChangedTable.Contains(currencyManager))
					{
						RelatedCurrencyManager.IgnoreItemChangedTable.Remove(currencyManager);
					}
				}
			}
			this.SetDataSource(this.fieldInfo.GetValue(this.parentManager.Current));
			this.listposition = ((this.Count > 0) ? 0 : (-1));
			IL_DC:
			if (listposition != this.listposition)
			{
				this.OnPositionChanged(EventArgs.Empty);
			}
			this.OnCurrentChanged(EventArgs.Empty);
			this.OnCurrentItemChanged(EventArgs.Empty);
		}

		// Token: 0x04001F5E RID: 8030
		private BindingManagerBase parentManager;

		// Token: 0x04001F5F RID: 8031
		private string dataField;

		// Token: 0x04001F60 RID: 8032
		private PropertyDescriptor fieldInfo;

		// Token: 0x04001F61 RID: 8033
		private static List<BindingManagerBase> IgnoreItemChangedTable = new List<BindingManagerBase>();
	}
}
