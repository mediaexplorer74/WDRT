using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000434 RID: 1076
	internal class WebBrowserContainer : UnsafeNativeMethods.IOleContainer, UnsafeNativeMethods.IOleInPlaceFrame
	{
		// Token: 0x06004B40 RID: 19264 RVA: 0x00139C0D File Offset: 0x00137E0D
		internal WebBrowserContainer(WebBrowserBase parent)
		{
			this.parent = parent;
		}

		// Token: 0x06004B41 RID: 19265 RVA: 0x00139C27 File Offset: 0x00137E27
		int UnsafeNativeMethods.IOleContainer.ParseDisplayName(object pbc, string pszDisplayName, int[] pchEaten, object[] ppmkOut)
		{
			if (ppmkOut != null)
			{
				ppmkOut[0] = null;
			}
			return -2147467263;
		}

		// Token: 0x06004B42 RID: 19266 RVA: 0x00139C38 File Offset: 0x00137E38
		int UnsafeNativeMethods.IOleContainer.EnumObjects(int grfFlags, out UnsafeNativeMethods.IEnumUnknown ppenum)
		{
			ppenum = null;
			if ((grfFlags & 1) != 0)
			{
				ArrayList arrayList = new ArrayList();
				this.ListAXControls(arrayList, true);
				if (arrayList.Count > 0)
				{
					object[] array = new object[arrayList.Count];
					arrayList.CopyTo(array, 0);
					ppenum = new AxHost.EnumUnknown(array);
					return 0;
				}
			}
			ppenum = new AxHost.EnumUnknown(null);
			return 0;
		}

		// Token: 0x06004B43 RID: 19267 RVA: 0x0003BC68 File Offset: 0x00039E68
		int UnsafeNativeMethods.IOleContainer.LockContainer(bool fLock)
		{
			return -2147467263;
		}

		// Token: 0x06004B44 RID: 19268 RVA: 0x00139C8B File Offset: 0x00137E8B
		IntPtr UnsafeNativeMethods.IOleInPlaceFrame.GetWindow()
		{
			return this.parent.Handle;
		}

		// Token: 0x06004B45 RID: 19269 RVA: 0x0001180C File Offset: 0x0000FA0C
		int UnsafeNativeMethods.IOleInPlaceFrame.ContextSensitiveHelp(int fEnterMode)
		{
			return 0;
		}

		// Token: 0x06004B46 RID: 19270 RVA: 0x0003BC68 File Offset: 0x00039E68
		int UnsafeNativeMethods.IOleInPlaceFrame.GetBorder(NativeMethods.COMRECT lprectBorder)
		{
			return -2147467263;
		}

		// Token: 0x06004B47 RID: 19271 RVA: 0x0003BC68 File Offset: 0x00039E68
		int UnsafeNativeMethods.IOleInPlaceFrame.RequestBorderSpace(NativeMethods.COMRECT pborderwidths)
		{
			return -2147467263;
		}

		// Token: 0x06004B48 RID: 19272 RVA: 0x0003BC68 File Offset: 0x00039E68
		int UnsafeNativeMethods.IOleInPlaceFrame.SetBorderSpace(NativeMethods.COMRECT pborderwidths)
		{
			return -2147467263;
		}

		// Token: 0x06004B49 RID: 19273 RVA: 0x00139C98 File Offset: 0x00137E98
		int UnsafeNativeMethods.IOleInPlaceFrame.SetActiveObject(UnsafeNativeMethods.IOleInPlaceActiveObject pActiveObject, string pszObjName)
		{
			if (pActiveObject == null)
			{
				if (this.ctlInEditMode != null)
				{
					this.ctlInEditMode.SetEditMode(WebBrowserHelper.AXEditMode.None);
					this.ctlInEditMode = null;
				}
				return 0;
			}
			WebBrowserBase webBrowserBase = null;
			UnsafeNativeMethods.IOleObject oleObject = pActiveObject as UnsafeNativeMethods.IOleObject;
			if (oleObject != null)
			{
				try
				{
					UnsafeNativeMethods.IOleClientSite clientSite = oleObject.GetClientSite();
					WebBrowserSiteBase webBrowserSiteBase = clientSite as WebBrowserSiteBase;
					if (webBrowserSiteBase != null)
					{
						webBrowserBase = webBrowserSiteBase.GetAXHost();
					}
				}
				catch (COMException ex)
				{
				}
				if (this.ctlInEditMode != null)
				{
					this.ctlInEditMode.SetSelectionStyle(WebBrowserHelper.SelectionStyle.Selected);
					this.ctlInEditMode.SetEditMode(WebBrowserHelper.AXEditMode.None);
				}
				if (webBrowserBase == null)
				{
					this.ctlInEditMode = null;
				}
				else if (!webBrowserBase.IsUserMode)
				{
					this.ctlInEditMode = webBrowserBase;
					webBrowserBase.SetEditMode(WebBrowserHelper.AXEditMode.Object);
					webBrowserBase.AddSelectionHandler();
					webBrowserBase.SetSelectionStyle(WebBrowserHelper.SelectionStyle.Active);
				}
			}
			return 0;
		}

		// Token: 0x06004B4A RID: 19274 RVA: 0x0001180C File Offset: 0x0000FA0C
		int UnsafeNativeMethods.IOleInPlaceFrame.InsertMenus(IntPtr hmenuShared, NativeMethods.tagOleMenuGroupWidths lpMenuWidths)
		{
			return 0;
		}

		// Token: 0x06004B4B RID: 19275 RVA: 0x0003BC68 File Offset: 0x00039E68
		int UnsafeNativeMethods.IOleInPlaceFrame.SetMenu(IntPtr hmenuShared, IntPtr holemenu, IntPtr hwndActiveObject)
		{
			return -2147467263;
		}

		// Token: 0x06004B4C RID: 19276 RVA: 0x0003BC68 File Offset: 0x00039E68
		int UnsafeNativeMethods.IOleInPlaceFrame.RemoveMenus(IntPtr hmenuShared)
		{
			return -2147467263;
		}

		// Token: 0x06004B4D RID: 19277 RVA: 0x0003BC68 File Offset: 0x00039E68
		int UnsafeNativeMethods.IOleInPlaceFrame.SetStatusText(string pszStatusText)
		{
			return -2147467263;
		}

		// Token: 0x06004B4E RID: 19278 RVA: 0x0003BC68 File Offset: 0x00039E68
		int UnsafeNativeMethods.IOleInPlaceFrame.EnableModeless(bool fEnable)
		{
			return -2147467263;
		}

		// Token: 0x06004B4F RID: 19279 RVA: 0x00012E4E File Offset: 0x0001104E
		int UnsafeNativeMethods.IOleInPlaceFrame.TranslateAccelerator(ref NativeMethods.MSG lpmsg, short wID)
		{
			return 1;
		}

		// Token: 0x06004B50 RID: 19280 RVA: 0x00139D54 File Offset: 0x00137F54
		private void ListAXControls(ArrayList list, bool fuseOcx)
		{
			Hashtable hashtable = this.GetComponents();
			if (hashtable == null)
			{
				return;
			}
			Control[] array = new Control[hashtable.Keys.Count];
			hashtable.Keys.CopyTo(array, 0);
			if (array != null)
			{
				foreach (Control control in array)
				{
					WebBrowserBase webBrowserBase = control as WebBrowserBase;
					if (webBrowserBase != null)
					{
						if (fuseOcx)
						{
							object activeXInstance = webBrowserBase.activeXInstance;
							if (activeXInstance != null)
							{
								list.Add(activeXInstance);
							}
						}
						else
						{
							list.Add(control);
						}
					}
				}
			}
		}

		// Token: 0x06004B51 RID: 19281 RVA: 0x00139DCE File Offset: 0x00137FCE
		private Hashtable GetComponents()
		{
			return this.GetComponents(this.GetParentsContainer());
		}

		// Token: 0x06004B52 RID: 19282 RVA: 0x00139DDC File Offset: 0x00137FDC
		private IContainer GetParentsContainer()
		{
			IContainer parentIContainer = this.GetParentIContainer();
			if (parentIContainer != null)
			{
				return parentIContainer;
			}
			return this.assocContainer;
		}

		// Token: 0x06004B53 RID: 19283 RVA: 0x00139DFC File Offset: 0x00137FFC
		private IContainer GetParentIContainer()
		{
			ISite site = this.parent.Site;
			if (site != null && site.DesignMode)
			{
				return site.Container;
			}
			return null;
		}

		// Token: 0x06004B54 RID: 19284 RVA: 0x00139E28 File Offset: 0x00138028
		private Hashtable GetComponents(IContainer cont)
		{
			this.FillComponentsTable(cont);
			return this.components;
		}

		// Token: 0x06004B55 RID: 19285 RVA: 0x00139E38 File Offset: 0x00138038
		private void FillComponentsTable(IContainer container)
		{
			if (container != null)
			{
				ComponentCollection componentCollection = container.Components;
				if (componentCollection != null)
				{
					this.components = new Hashtable();
					foreach (object obj in componentCollection)
					{
						IComponent component = (IComponent)obj;
						if (component is Control && component != this.parent && component.Site != null)
						{
							this.components.Add(component, component);
						}
					}
					return;
				}
			}
			bool flag = true;
			Control[] array = new Control[this.containerCache.Values.Count];
			this.containerCache.Values.CopyTo(array, 0);
			if (array != null)
			{
				if (array.Length != 0 && this.components == null)
				{
					this.components = new Hashtable();
					flag = false;
				}
				for (int i = 0; i < array.Length; i++)
				{
					if (flag && !this.components.Contains(array[i]))
					{
						this.components.Add(array[i], array[i]);
					}
				}
			}
			this.GetAllChildren(this.parent);
		}

		// Token: 0x06004B56 RID: 19286 RVA: 0x00139F58 File Offset: 0x00138158
		private void GetAllChildren(Control ctl)
		{
			if (ctl == null)
			{
				return;
			}
			if (this.components == null)
			{
				this.components = new Hashtable();
			}
			if (ctl != this.parent && !this.components.Contains(ctl))
			{
				this.components.Add(ctl, ctl);
			}
			foreach (object obj in ctl.Controls)
			{
				Control control = (Control)obj;
				this.GetAllChildren(control);
			}
		}

		// Token: 0x06004B57 RID: 19287 RVA: 0x00139FEC File Offset: 0x001381EC
		private bool RegisterControl(WebBrowserBase ctl)
		{
			ISite site = ctl.Site;
			if (site != null)
			{
				IContainer container = site.Container;
				if (container != null)
				{
					if (this.assocContainer != null)
					{
						return container == this.assocContainer;
					}
					this.assocContainer = container;
					IComponentChangeService componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
					if (componentChangeService != null)
					{
						componentChangeService.ComponentRemoved += this.OnComponentRemoved;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004B58 RID: 19288 RVA: 0x0013A054 File Offset: 0x00138254
		private void OnComponentRemoved(object sender, ComponentEventArgs e)
		{
			Control control = e.Component as Control;
			if (sender == this.assocContainer && control != null)
			{
				this.RemoveControl(control);
			}
		}

		// Token: 0x06004B59 RID: 19289 RVA: 0x0013A080 File Offset: 0x00138280
		internal void AddControl(Control ctl)
		{
			if (this.containerCache.Contains(ctl))
			{
				throw new ArgumentException(SR.GetString("AXDuplicateControl", new object[] { this.GetNameForControl(ctl) }), "ctl");
			}
			this.containerCache.Add(ctl, ctl);
			if (this.assocContainer == null)
			{
				ISite site = ctl.Site;
				if (site != null)
				{
					this.assocContainer = site.Container;
					IComponentChangeService componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
					if (componentChangeService != null)
					{
						componentChangeService.ComponentRemoved += this.OnComponentRemoved;
					}
				}
			}
		}

		// Token: 0x06004B5A RID: 19290 RVA: 0x0013A116 File Offset: 0x00138316
		internal void RemoveControl(Control ctl)
		{
			this.containerCache.Remove(ctl);
		}

		// Token: 0x06004B5B RID: 19291 RVA: 0x0013A124 File Offset: 0x00138324
		internal static WebBrowserContainer FindContainerForControl(WebBrowserBase ctl)
		{
			if (ctl != null)
			{
				if (ctl.container != null)
				{
					return ctl.container;
				}
				ScrollableControl containingControl = ctl.ContainingControl;
				if (containingControl != null)
				{
					WebBrowserContainer webBrowserContainer = ctl.CreateWebBrowserContainer();
					if (webBrowserContainer.RegisterControl(ctl))
					{
						webBrowserContainer.AddControl(ctl);
						return webBrowserContainer;
					}
				}
			}
			return null;
		}

		// Token: 0x06004B5C RID: 19292 RVA: 0x0013A168 File Offset: 0x00138368
		internal string GetNameForControl(Control ctl)
		{
			string text = ((ctl.Site != null) ? ctl.Site.Name : ctl.Name);
			return text ?? "";
		}

		// Token: 0x06004B5D RID: 19293 RVA: 0x0013A19C File Offset: 0x0013839C
		internal void OnUIActivate(WebBrowserBase site)
		{
			if (this.siteUIActive == site)
			{
				return;
			}
			if (this.siteUIActive != null && this.siteUIActive != site)
			{
				WebBrowserBase webBrowserBase = this.siteUIActive;
				webBrowserBase.AXInPlaceObject.UIDeactivate();
			}
			site.AddSelectionHandler();
			this.siteUIActive = site;
			ContainerControl containingControl = site.ContainingControl;
			if (containingControl != null && containingControl.Contains(site))
			{
				containingControl.SetActiveControlInternal(site);
			}
		}

		// Token: 0x06004B5E RID: 19294 RVA: 0x0013A1FE File Offset: 0x001383FE
		internal void OnUIDeactivate(WebBrowserBase site)
		{
			this.siteUIActive = null;
			site.RemoveSelectionHandler();
			site.SetSelectionStyle(WebBrowserHelper.SelectionStyle.Selected);
			site.SetEditMode(WebBrowserHelper.AXEditMode.None);
		}

		// Token: 0x06004B5F RID: 19295 RVA: 0x0013A21C File Offset: 0x0013841C
		internal void OnInPlaceDeactivate(WebBrowserBase site)
		{
			if (this.siteActive == site)
			{
				this.siteActive = null;
				ContainerControl containerControl = this.parent.FindContainerControlInternal();
				if (containerControl != null)
				{
					containerControl.SetActiveControlInternal(null);
				}
			}
		}

		// Token: 0x06004B60 RID: 19296 RVA: 0x0013A24F File Offset: 0x0013844F
		internal void OnExitEditMode(WebBrowserBase ctl)
		{
			if (this.ctlInEditMode == ctl)
			{
				this.ctlInEditMode = null;
			}
		}

		// Token: 0x0400280F RID: 10255
		private WebBrowserBase parent;

		// Token: 0x04002810 RID: 10256
		private IContainer assocContainer;

		// Token: 0x04002811 RID: 10257
		private WebBrowserBase siteUIActive;

		// Token: 0x04002812 RID: 10258
		private WebBrowserBase siteActive;

		// Token: 0x04002813 RID: 10259
		private Hashtable containerCache = new Hashtable();

		// Token: 0x04002814 RID: 10260
		private Hashtable components;

		// Token: 0x04002815 RID: 10261
		private WebBrowserBase ctlInEditMode;
	}
}
