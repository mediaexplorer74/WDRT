using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace System.Net
{
	// Token: 0x02000229 RID: 553
	internal class TrackingValidationObjectDictionary : StringDictionary
	{
		// Token: 0x06001456 RID: 5206 RVA: 0x0006B93F File Offset: 0x00069B3F
		internal TrackingValidationObjectDictionary(IDictionary<string, TrackingValidationObjectDictionary.ValidateAndParseValue> validators)
		{
			this.IsChanged = false;
			this.validators = validators;
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x0006B958 File Offset: 0x00069B58
		private void PersistValue(string key, string value, bool addValue)
		{
			key = key.ToLowerInvariant();
			if (!string.IsNullOrEmpty(value))
			{
				if (this.validators != null && this.validators.ContainsKey(key))
				{
					object obj = this.validators[key](value);
					if (this.internalObjects == null)
					{
						this.internalObjects = new Dictionary<string, object>();
					}
					if (addValue)
					{
						this.internalObjects.Add(key, obj);
						base.Add(key, obj.ToString());
					}
					else
					{
						this.internalObjects[key] = obj;
						base[key] = obj.ToString();
					}
				}
				else if (addValue)
				{
					base.Add(key, value);
				}
				else
				{
					base[key] = value;
				}
				this.IsChanged = true;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x0006BA0B File Offset: 0x00069C0B
		// (set) Token: 0x06001459 RID: 5209 RVA: 0x0006BA13 File Offset: 0x00069C13
		internal bool IsChanged { get; set; }

		// Token: 0x0600145A RID: 5210 RVA: 0x0006BA1C File Offset: 0x00069C1C
		internal object InternalGet(string key)
		{
			if (this.internalObjects != null && this.internalObjects.ContainsKey(key))
			{
				return this.internalObjects[key];
			}
			return base[key];
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x0006BA48 File Offset: 0x00069C48
		internal void InternalSet(string key, object value)
		{
			if (this.internalObjects == null)
			{
				this.internalObjects = new Dictionary<string, object>();
			}
			this.internalObjects[key] = value;
			base[key] = value.ToString();
			this.IsChanged = true;
		}

		// Token: 0x1700043F RID: 1087
		public override string this[string key]
		{
			get
			{
				return base[key];
			}
			set
			{
				this.PersistValue(key, value, false);
			}
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x0006BA92 File Offset: 0x00069C92
		public override void Add(string key, string value)
		{
			this.PersistValue(key, value, true);
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x0006BA9D File Offset: 0x00069C9D
		public override void Clear()
		{
			if (this.internalObjects != null)
			{
				this.internalObjects.Clear();
			}
			base.Clear();
			this.IsChanged = true;
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x0006BABF File Offset: 0x00069CBF
		public override void Remove(string key)
		{
			if (this.internalObjects != null && this.internalObjects.ContainsKey(key))
			{
				this.internalObjects.Remove(key);
			}
			base.Remove(key);
			this.IsChanged = true;
		}

		// Token: 0x04001618 RID: 5656
		private IDictionary<string, object> internalObjects;

		// Token: 0x04001619 RID: 5657
		private readonly IDictionary<string, TrackingValidationObjectDictionary.ValidateAndParseValue> validators;

		// Token: 0x02000769 RID: 1897
		// (Invoke) Token: 0x06004233 RID: 16947
		internal delegate object ValidateAndParseValue(object valueToValidate);
	}
}
