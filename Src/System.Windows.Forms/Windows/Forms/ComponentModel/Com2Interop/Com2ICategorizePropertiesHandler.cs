using System;
using System.ComponentModel;
using System.Globalization;
using System.Security;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x02000499 RID: 1177
	[SuppressUnmanagedCodeSecurity]
	internal class Com2ICategorizePropertiesHandler : Com2ExtendedBrowsingHandler
	{
		// Token: 0x1700134D RID: 4941
		// (get) Token: 0x06004E8E RID: 20110 RVA: 0x00143303 File Offset: 0x00141503
		public override Type Interface
		{
			get
			{
				return typeof(NativeMethods.ICategorizeProperties);
			}
		}

		// Token: 0x06004E8F RID: 20111 RVA: 0x00143310 File Offset: 0x00141510
		private string GetCategoryFromObject(object obj, int dispid)
		{
			if (obj == null)
			{
				return null;
			}
			if (obj is NativeMethods.ICategorizeProperties)
			{
				NativeMethods.ICategorizeProperties categorizeProperties = (NativeMethods.ICategorizeProperties)obj;
				try
				{
					int num = 0;
					if (categorizeProperties.MapPropertyToCategory(dispid, ref num) == 0)
					{
						string text = null;
						switch (num)
						{
						case -11:
							return SR.GetString("PropertyCategoryDDE");
						case -10:
							return SR.GetString("PropertyCategoryScale");
						case -9:
							return SR.GetString("PropertyCategoryText");
						case -8:
							return SR.GetString("PropertyCategoryList");
						case -7:
							return SR.GetString("PropertyCategoryData");
						case -6:
							return SR.GetString("PropertyCategoryBehavior");
						case -5:
							return SR.GetString("PropertyCategoryAppearance");
						case -4:
							return SR.GetString("PropertyCategoryPosition");
						case -3:
							return SR.GetString("PropertyCategoryFont");
						case -2:
							return SR.GetString("PropertyCategoryMisc");
						case -1:
							return "";
						default:
							if (categorizeProperties.GetCategoryName(num, CultureInfo.CurrentCulture.LCID, out text) == 0)
							{
								return text;
							}
							break;
						}
					}
				}
				catch
				{
				}
			}
			return null;
		}

		// Token: 0x06004E90 RID: 20112 RVA: 0x00143444 File Offset: 0x00141644
		public override void SetupPropertyHandlers(Com2PropertyDescriptor[] propDesc)
		{
			if (propDesc == null)
			{
				return;
			}
			for (int i = 0; i < propDesc.Length; i++)
			{
				propDesc[i].QueryGetBaseAttributes += this.OnGetAttributes;
			}
		}

		// Token: 0x06004E91 RID: 20113 RVA: 0x00143478 File Offset: 0x00141678
		private void OnGetAttributes(Com2PropertyDescriptor sender, GetAttributesEvent attrEvent)
		{
			string categoryFromObject = this.GetCategoryFromObject(sender.TargetObject, sender.DISPID);
			if (categoryFromObject != null && categoryFromObject.Length > 0)
			{
				attrEvent.Add(new CategoryAttribute(categoryFromObject));
			}
		}
	}
}
