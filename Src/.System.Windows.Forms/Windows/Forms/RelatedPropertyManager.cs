using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000340 RID: 832
	internal class RelatedPropertyManager : PropertyManager
	{
		// Token: 0x060035CF RID: 13775 RVA: 0x000F349B File Offset: 0x000F169B
		internal RelatedPropertyManager(BindingManagerBase parentManager, string dataField)
			: base(RelatedPropertyManager.GetCurrentOrNull(parentManager), dataField)
		{
			this.Bind(parentManager, dataField);
		}

		// Token: 0x060035D0 RID: 13776 RVA: 0x000F34B4 File Offset: 0x000F16B4
		private void Bind(BindingManagerBase parentManager, string dataField)
		{
			this.parentManager = parentManager;
			this.dataField = dataField;
			this.fieldInfo = parentManager.GetItemProperties().Find(dataField, true);
			if (this.fieldInfo == null)
			{
				throw new ArgumentException(SR.GetString("RelatedListManagerChild", new object[] { dataField }));
			}
			parentManager.CurrentItemChanged += this.ParentManager_CurrentItemChanged;
			this.Refresh();
		}

		// Token: 0x060035D1 RID: 13777 RVA: 0x000F351C File Offset: 0x000F171C
		internal override string GetListName()
		{
			string listName = this.GetListName(new ArrayList());
			if (listName.Length > 0)
			{
				return listName;
			}
			return base.GetListName();
		}

		// Token: 0x060035D2 RID: 13778 RVA: 0x000F3546 File Offset: 0x000F1746
		protected internal override string GetListName(ArrayList listAccessors)
		{
			listAccessors.Insert(0, this.fieldInfo);
			return this.parentManager.GetListName(listAccessors);
		}

		// Token: 0x060035D3 RID: 13779 RVA: 0x000F3564 File Offset: 0x000F1764
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

		// Token: 0x060035D4 RID: 13780 RVA: 0x000F35A9 File Offset: 0x000F17A9
		private void ParentManager_CurrentItemChanged(object sender, EventArgs e)
		{
			this.Refresh();
		}

		// Token: 0x060035D5 RID: 13781 RVA: 0x000F35B1 File Offset: 0x000F17B1
		private void Refresh()
		{
			this.EndCurrentEdit();
			this.SetDataSource(RelatedPropertyManager.GetCurrentOrNull(this.parentManager));
			this.OnCurrentChanged(EventArgs.Empty);
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x060035D6 RID: 13782 RVA: 0x000F35D5 File Offset: 0x000F17D5
		internal override Type BindType
		{
			get
			{
				return this.fieldInfo.PropertyType;
			}
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x060035D7 RID: 13783 RVA: 0x000F35E2 File Offset: 0x000F17E2
		public override object Current
		{
			get
			{
				if (this.DataSource == null)
				{
					return null;
				}
				return this.fieldInfo.GetValue(this.DataSource);
			}
		}

		// Token: 0x060035D8 RID: 13784 RVA: 0x000F3600 File Offset: 0x000F1800
		private static object GetCurrentOrNull(BindingManagerBase parentManager)
		{
			if (parentManager.Position < 0 || parentManager.Position >= parentManager.Count)
			{
				return null;
			}
			return parentManager.Current;
		}

		// Token: 0x04001F63 RID: 8035
		private BindingManagerBase parentManager;

		// Token: 0x04001F64 RID: 8036
		private string dataField;

		// Token: 0x04001F65 RID: 8037
		private PropertyDescriptor fieldInfo;
	}
}
