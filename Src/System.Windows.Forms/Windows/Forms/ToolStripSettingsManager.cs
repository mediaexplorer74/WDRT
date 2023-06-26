using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Windows.Forms
{
	// Token: 0x02000401 RID: 1025
	internal class ToolStripSettingsManager
	{
		// Token: 0x060046D2 RID: 18130 RVA: 0x00128CDE File Offset: 0x00126EDE
		internal ToolStripSettingsManager(Form owner, string formKey)
		{
			this.form = owner;
			this.formKey = formKey;
		}

		// Token: 0x060046D3 RID: 18131 RVA: 0x00128CF4 File Offset: 0x00126EF4
		internal void Load()
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.FindToolStrips(true, this.form.Controls))
			{
				ToolStrip toolStrip = (ToolStrip)obj;
				if (toolStrip != null && !string.IsNullOrEmpty(toolStrip.Name))
				{
					ToolStripSettings toolStripSettings = new ToolStripSettings(this.GetSettingsKey(toolStrip));
					if (!toolStripSettings.IsDefault)
					{
						arrayList.Add(new ToolStripSettingsManager.SettingsStub(toolStripSettings));
					}
				}
			}
			this.ApplySettings(arrayList);
		}

		// Token: 0x060046D4 RID: 18132 RVA: 0x00128D9C File Offset: 0x00126F9C
		internal void Save()
		{
			foreach (object obj in this.FindToolStrips(true, this.form.Controls))
			{
				ToolStrip toolStrip = (ToolStrip)obj;
				if (toolStrip != null && !string.IsNullOrEmpty(toolStrip.Name))
				{
					ToolStripSettings toolStripSettings = new ToolStripSettings(this.GetSettingsKey(toolStrip));
					ToolStripSettingsManager.SettingsStub settingsStub = new ToolStripSettingsManager.SettingsStub(toolStrip);
					toolStripSettings.ItemOrder = settingsStub.ItemOrder;
					toolStripSettings.Name = settingsStub.Name;
					toolStripSettings.Location = settingsStub.Location;
					toolStripSettings.Size = settingsStub.Size;
					toolStripSettings.ToolStripPanelName = settingsStub.ToolStripPanelName;
					toolStripSettings.Visible = settingsStub.Visible;
					toolStripSettings.Save();
				}
			}
		}

		// Token: 0x060046D5 RID: 18133 RVA: 0x00128E74 File Offset: 0x00127074
		internal static string GetItemOrder(ToolStrip toolStrip)
		{
			StringBuilder stringBuilder = new StringBuilder(toolStrip.Items.Count);
			for (int i = 0; i < toolStrip.Items.Count; i++)
			{
				stringBuilder.Append((toolStrip.Items[i].Name == null) ? "null" : toolStrip.Items[i].Name);
				if (i != toolStrip.Items.Count - 1)
				{
					stringBuilder.Append(",");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060046D6 RID: 18134 RVA: 0x00128EFC File Offset: 0x001270FC
		private void ApplySettings(ArrayList toolStripSettingsToApply)
		{
			if (toolStripSettingsToApply.Count == 0)
			{
				return;
			}
			this.SuspendAllLayout(this.form);
			Dictionary<string, ToolStrip> dictionary = this.BuildItemOriginationHash();
			Dictionary<object, List<ToolStripSettingsManager.SettingsStub>> dictionary2 = new Dictionary<object, List<ToolStripSettingsManager.SettingsStub>>();
			foreach (object obj in toolStripSettingsToApply)
			{
				ToolStripSettingsManager.SettingsStub settingsStub = (ToolStripSettingsManager.SettingsStub)obj;
				object obj2 = ((!string.IsNullOrEmpty(settingsStub.ToolStripPanelName)) ? settingsStub.ToolStripPanelName : null);
				if (obj2 == null)
				{
					if (!string.IsNullOrEmpty(settingsStub.Name))
					{
						ToolStrip toolStrip = ToolStripManager.FindToolStrip(this.form, settingsStub.Name);
						this.ApplyToolStripSettings(toolStrip, settingsStub, dictionary);
					}
				}
				else
				{
					if (!dictionary2.ContainsKey(obj2))
					{
						dictionary2[obj2] = new List<ToolStripSettingsManager.SettingsStub>();
					}
					dictionary2[obj2].Add(settingsStub);
				}
			}
			ArrayList arrayList = this.FindToolStripPanels(true, this.form.Controls);
			foreach (object obj3 in arrayList)
			{
				ToolStripPanel toolStripPanel = (ToolStripPanel)obj3;
				foreach (object obj4 in toolStripPanel.Controls)
				{
					Control control = (Control)obj4;
					control.Visible = false;
				}
				string text = toolStripPanel.Name;
				if (string.IsNullOrEmpty(text) && toolStripPanel.Parent is ToolStripContainer && !string.IsNullOrEmpty(toolStripPanel.Parent.Name))
				{
					text = toolStripPanel.Parent.Name + "." + toolStripPanel.Dock.ToString();
				}
				toolStripPanel.BeginInit();
				if (dictionary2.ContainsKey(text))
				{
					List<ToolStripSettingsManager.SettingsStub> list = dictionary2[text];
					if (list != null)
					{
						foreach (ToolStripSettingsManager.SettingsStub settingsStub2 in list)
						{
							if (!string.IsNullOrEmpty(settingsStub2.Name))
							{
								ToolStrip toolStrip2 = ToolStripManager.FindToolStrip(this.form, settingsStub2.Name);
								this.ApplyToolStripSettings(toolStrip2, settingsStub2, dictionary);
								toolStripPanel.Join(toolStrip2, settingsStub2.Location);
							}
						}
					}
				}
				toolStripPanel.EndInit();
			}
			this.ResumeAllLayout(this.form, true);
		}

		// Token: 0x060046D7 RID: 18135 RVA: 0x001291D4 File Offset: 0x001273D4
		private void ApplyToolStripSettings(ToolStrip toolStrip, ToolStripSettingsManager.SettingsStub settings, Dictionary<string, ToolStrip> itemLocationHash)
		{
			if (toolStrip != null)
			{
				toolStrip.Visible = settings.Visible;
				toolStrip.Size = settings.Size;
				string itemOrder = settings.ItemOrder;
				if (!string.IsNullOrEmpty(itemOrder))
				{
					string[] array = itemOrder.Split(new char[] { ',' });
					Regex regex = new Regex("(\\S+)");
					int num = 0;
					while (num < toolStrip.Items.Count && num < array.Length)
					{
						Match match = regex.Match(array[num]);
						if (match != null && match.Success)
						{
							string value = match.Value;
							if (!string.IsNullOrEmpty(value) && itemLocationHash.ContainsKey(value))
							{
								toolStrip.Items.Insert(num, itemLocationHash[value].Items[value]);
							}
						}
						num++;
					}
				}
			}
		}

		// Token: 0x060046D8 RID: 18136 RVA: 0x001292A0 File Offset: 0x001274A0
		private Dictionary<string, ToolStrip> BuildItemOriginationHash()
		{
			ArrayList arrayList = this.FindToolStrips(true, this.form.Controls);
			Dictionary<string, ToolStrip> dictionary = new Dictionary<string, ToolStrip>();
			if (arrayList != null)
			{
				foreach (object obj in arrayList)
				{
					ToolStrip toolStrip = (ToolStrip)obj;
					foreach (object obj2 in toolStrip.Items)
					{
						ToolStripItem toolStripItem = (ToolStripItem)obj2;
						if (!string.IsNullOrEmpty(toolStripItem.Name))
						{
							dictionary[toolStripItem.Name] = toolStrip;
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060046D9 RID: 18137 RVA: 0x00129378 File Offset: 0x00127578
		private ArrayList FindControls(Type baseType, bool searchAllChildren, Control.ControlCollection controlsToLookIn, ArrayList foundControls)
		{
			if (controlsToLookIn == null || foundControls == null)
			{
				return null;
			}
			try
			{
				for (int i = 0; i < controlsToLookIn.Count; i++)
				{
					if (controlsToLookIn[i] != null && baseType.IsAssignableFrom(controlsToLookIn[i].GetType()))
					{
						foundControls.Add(controlsToLookIn[i]);
					}
				}
				if (searchAllChildren)
				{
					for (int j = 0; j < controlsToLookIn.Count; j++)
					{
						if (controlsToLookIn[j] != null && !(controlsToLookIn[j] is Form) && controlsToLookIn[j].Controls != null && controlsToLookIn[j].Controls.Count > 0)
						{
							foundControls = this.FindControls(baseType, searchAllChildren, controlsToLookIn[j].Controls, foundControls);
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsCriticalException(ex))
				{
					throw;
				}
			}
			return foundControls;
		}

		// Token: 0x060046DA RID: 18138 RVA: 0x00129454 File Offset: 0x00127654
		private ArrayList FindToolStripPanels(bool searchAllChildren, Control.ControlCollection controlsToLookIn)
		{
			return this.FindControls(typeof(ToolStripPanel), true, this.form.Controls, new ArrayList());
		}

		// Token: 0x060046DB RID: 18139 RVA: 0x00129477 File Offset: 0x00127677
		private ArrayList FindToolStrips(bool searchAllChildren, Control.ControlCollection controlsToLookIn)
		{
			return this.FindControls(typeof(ToolStrip), true, this.form.Controls, new ArrayList());
		}

		// Token: 0x060046DC RID: 18140 RVA: 0x0012949A File Offset: 0x0012769A
		private string GetSettingsKey(ToolStrip toolStrip)
		{
			if (toolStrip != null)
			{
				return this.formKey + "." + toolStrip.Name;
			}
			return string.Empty;
		}

		// Token: 0x060046DD RID: 18141 RVA: 0x001294BC File Offset: 0x001276BC
		private void ResumeAllLayout(Control start, bool performLayout)
		{
			Control.ControlCollection controls = start.Controls;
			for (int i = 0; i < controls.Count; i++)
			{
				this.ResumeAllLayout(controls[i], performLayout);
			}
			start.ResumeLayout(performLayout);
		}

		// Token: 0x060046DE RID: 18142 RVA: 0x001294F8 File Offset: 0x001276F8
		private void SuspendAllLayout(Control start)
		{
			start.SuspendLayout();
			Control.ControlCollection controls = start.Controls;
			for (int i = 0; i < controls.Count; i++)
			{
				this.SuspendAllLayout(controls[i]);
			}
		}

		// Token: 0x040026AF RID: 9903
		private Form form;

		// Token: 0x040026B0 RID: 9904
		private string formKey;

		// Token: 0x02000819 RID: 2073
		private struct SettingsStub
		{
			// Token: 0x06006F9B RID: 28571 RVA: 0x0019973C File Offset: 0x0019793C
			public SettingsStub(ToolStrip toolStrip)
			{
				this.ToolStripPanelName = string.Empty;
				ToolStripPanel toolStripPanel = toolStrip.Parent as ToolStripPanel;
				if (toolStripPanel != null)
				{
					if (!string.IsNullOrEmpty(toolStripPanel.Name))
					{
						this.ToolStripPanelName = toolStripPanel.Name;
					}
					else if (toolStripPanel.Parent is ToolStripContainer && !string.IsNullOrEmpty(toolStripPanel.Parent.Name))
					{
						this.ToolStripPanelName = toolStripPanel.Parent.Name + "." + toolStripPanel.Dock.ToString();
					}
				}
				this.Visible = toolStrip.Visible;
				this.Size = toolStrip.Size;
				this.Location = toolStrip.Location;
				this.Name = toolStrip.Name;
				this.ItemOrder = ToolStripSettingsManager.GetItemOrder(toolStrip);
			}

			// Token: 0x06006F9C RID: 28572 RVA: 0x00199808 File Offset: 0x00197A08
			public SettingsStub(ToolStripSettings toolStripSettings)
			{
				this.ToolStripPanelName = toolStripSettings.ToolStripPanelName;
				this.Visible = toolStripSettings.Visible;
				this.Size = toolStripSettings.Size;
				this.Location = toolStripSettings.Location;
				this.Name = toolStripSettings.Name;
				this.ItemOrder = toolStripSettings.ItemOrder;
			}

			// Token: 0x04004323 RID: 17187
			public bool Visible;

			// Token: 0x04004324 RID: 17188
			public string ToolStripPanelName;

			// Token: 0x04004325 RID: 17189
			public Point Location;

			// Token: 0x04004326 RID: 17190
			public Size Size;

			// Token: 0x04004327 RID: 17191
			public string ItemOrder;

			// Token: 0x04004328 RID: 17192
			public string Name;
		}
	}
}
