using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x0200013B RID: 315
	internal class BindToObject
	{
		// Token: 0x06000C3C RID: 3132 RVA: 0x00023099 File Offset: 0x00021299
		private void PropValueChanged(object sender, EventArgs e)
		{
			if (this.bindingManager != null)
			{
				this.bindingManager.OnCurrentChanged(EventArgs.Empty);
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x000230B4 File Offset: 0x000212B4
		private bool IsDataSourceInitialized
		{
			get
			{
				if (this.dataSourceInitialized)
				{
					return true;
				}
				ISupportInitializeNotification supportInitializeNotification = this.dataSource as ISupportInitializeNotification;
				if (supportInitializeNotification == null || supportInitializeNotification.IsInitialized)
				{
					this.dataSourceInitialized = true;
					return true;
				}
				if (this.waitingOnDataSource)
				{
					return false;
				}
				supportInitializeNotification.Initialized += this.DataSource_Initialized;
				this.waitingOnDataSource = true;
				return false;
			}
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0002310F File Offset: 0x0002130F
		internal BindToObject(Binding owner, object dataSource, string dataMember)
		{
			this.owner = owner;
			this.dataSource = dataSource;
			this.dataMember = new BindingMemberInfo(dataMember);
			this.CheckBinding();
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x00023144 File Offset: 0x00021344
		private void DataSource_Initialized(object sender, EventArgs e)
		{
			ISupportInitializeNotification supportInitializeNotification = this.dataSource as ISupportInitializeNotification;
			if (supportInitializeNotification != null)
			{
				supportInitializeNotification.Initialized -= this.DataSource_Initialized;
			}
			this.waitingOnDataSource = false;
			this.dataSourceInitialized = true;
			this.CheckBinding();
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x00023188 File Offset: 0x00021388
		internal void SetBindingManagerBase(BindingManagerBase lManager)
		{
			if (this.bindingManager == lManager)
			{
				return;
			}
			if (this.bindingManager != null && this.fieldInfo != null && this.bindingManager.IsBinding && !(this.bindingManager is CurrencyManager))
			{
				this.fieldInfo.RemoveValueChanged(this.bindingManager.Current, new EventHandler(this.PropValueChanged));
				this.fieldInfo = null;
			}
			this.bindingManager = lManager;
			this.CheckBinding();
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x000231FF File Offset: 0x000213FF
		internal string DataErrorText
		{
			get
			{
				return this.errorText;
			}
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00023208 File Offset: 0x00021408
		private string GetErrorText(object value)
		{
			IDataErrorInfo dataErrorInfo = value as IDataErrorInfo;
			string text = string.Empty;
			if (dataErrorInfo != null)
			{
				if (this.fieldInfo == null)
				{
					text = dataErrorInfo.Error;
				}
				else
				{
					text = dataErrorInfo[this.fieldInfo.Name];
				}
			}
			return text ?? string.Empty;
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x00023254 File Offset: 0x00021454
		internal object GetValue()
		{
			object obj = this.bindingManager.Current;
			this.errorText = this.GetErrorText(obj);
			if (this.fieldInfo != null)
			{
				obj = this.fieldInfo.GetValue(obj);
			}
			return obj;
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x00023290 File Offset: 0x00021490
		internal Type BindToType
		{
			get
			{
				if (this.dataMember.BindingField.Length == 0)
				{
					Type type = this.bindingManager.BindType;
					if (typeof(Array).IsAssignableFrom(type))
					{
						type = type.GetElementType();
					}
					return type;
				}
				if (this.fieldInfo != null)
				{
					return this.fieldInfo.PropertyType;
				}
				return null;
			}
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x000232EC File Offset: 0x000214EC
		internal void SetValue(object value)
		{
			object obj = null;
			if (this.fieldInfo != null)
			{
				obj = this.bindingManager.Current;
				if (obj is IEditableObject)
				{
					((IEditableObject)obj).BeginEdit();
				}
				if (!this.fieldInfo.IsReadOnly)
				{
					this.fieldInfo.SetValue(obj, value);
				}
			}
			else
			{
				CurrencyManager currencyManager = this.bindingManager as CurrencyManager;
				if (currencyManager != null)
				{
					currencyManager[currencyManager.Position] = value;
					obj = value;
				}
			}
			this.errorText = this.GetErrorText(obj);
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x00023369 File Offset: 0x00021569
		internal BindingMemberInfo BindingMemberInfo
		{
			get
			{
				return this.dataMember;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x00023371 File Offset: 0x00021571
		internal object DataSource
		{
			get
			{
				return this.dataSource;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x00023379 File Offset: 0x00021579
		internal PropertyDescriptor FieldInfo
		{
			get
			{
				return this.fieldInfo;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x00023381 File Offset: 0x00021581
		internal BindingManagerBase BindingManagerBase
		{
			get
			{
				return this.bindingManager;
			}
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0002338C File Offset: 0x0002158C
		internal void CheckBinding()
		{
			if (this.owner != null && this.owner.BindableComponent != null && this.owner.ControlAtDesignTime())
			{
				return;
			}
			if (this.owner.BindingManagerBase != null && this.fieldInfo != null && this.owner.BindingManagerBase.IsBinding && !(this.owner.BindingManagerBase is CurrencyManager))
			{
				this.fieldInfo.RemoveValueChanged(this.owner.BindingManagerBase.Current, new EventHandler(this.PropValueChanged));
			}
			if (this.owner != null && this.owner.BindingManagerBase != null && this.owner.BindableComponent != null && this.owner.ComponentCreated && this.IsDataSourceInitialized)
			{
				string bindingField = this.dataMember.BindingField;
				this.fieldInfo = this.owner.BindingManagerBase.GetItemProperties().Find(bindingField, true);
				if (this.owner.BindingManagerBase.DataSource != null && this.fieldInfo == null && bindingField.Length > 0)
				{
					throw new ArgumentException(SR.GetString("ListBindingBindField", new object[] { bindingField }), "dataMember");
				}
				if (this.fieldInfo != null && this.owner.BindingManagerBase.IsBinding && !(this.owner.BindingManagerBase is CurrencyManager))
				{
					this.fieldInfo.AddValueChanged(this.owner.BindingManagerBase.Current, new EventHandler(this.PropValueChanged));
					return;
				}
			}
			else
			{
				this.fieldInfo = null;
			}
		}

		// Token: 0x040006F4 RID: 1780
		private PropertyDescriptor fieldInfo;

		// Token: 0x040006F5 RID: 1781
		private BindingMemberInfo dataMember;

		// Token: 0x040006F6 RID: 1782
		private object dataSource;

		// Token: 0x040006F7 RID: 1783
		private BindingManagerBase bindingManager;

		// Token: 0x040006F8 RID: 1784
		private Binding owner;

		// Token: 0x040006F9 RID: 1785
		private string errorText = string.Empty;

		// Token: 0x040006FA RID: 1786
		private bool dataSourceInitialized;

		// Token: 0x040006FB RID: 1787
		private bool waitingOnDataSource;
	}
}
