using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a verb that can be invoked from a designer.</summary>
	// Token: 0x020005D7 RID: 1495
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class DesignerVerb : MenuCommand
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerVerb" /> class.</summary>
		/// <param name="text">The text of the menu command that is shown to the user.</param>
		/// <param name="handler">The event handler that performs the actions of the verb.</param>
		// Token: 0x0600378B RID: 14219 RVA: 0x000EFF13 File Offset: 0x000EE113
		public DesignerVerb(string text, EventHandler handler)
			: base(handler, StandardCommands.VerbFirst)
		{
			this.Properties["Text"] = ((text == null) ? null : Regex.Replace(text, "\\(\\&.\\)", ""));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerVerb" /> class.</summary>
		/// <param name="text">The text of the menu command that is shown to the user.</param>
		/// <param name="handler">The event handler that performs the actions of the verb.</param>
		/// <param name="startCommandID">The starting command ID for this verb. By default, the designer architecture sets aside a range of command IDs for verbs. You can override this by providing a custom command ID.</param>
		// Token: 0x0600378C RID: 14220 RVA: 0x000EFF47 File Offset: 0x000EE147
		public DesignerVerb(string text, EventHandler handler, CommandID startCommandID)
			: base(handler, startCommandID)
		{
			this.Properties["Text"] = ((text == null) ? null : Regex.Replace(text, "\\(\\&.\\)", ""));
		}

		/// <summary>Gets or sets the description of the menu item for the verb.</summary>
		/// <returns>A string describing the menu item.</returns>
		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x0600378D RID: 14221 RVA: 0x000EFF78 File Offset: 0x000EE178
		// (set) Token: 0x0600378E RID: 14222 RVA: 0x000EFFA5 File Offset: 0x000EE1A5
		public string Description
		{
			get
			{
				object obj = this.Properties["Description"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.Properties["Description"] = value;
			}
		}

		/// <summary>Gets the text description for the verb command on the menu.</summary>
		/// <returns>A description for the verb command.</returns>
		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x0600378F RID: 14223 RVA: 0x000EFFB8 File Offset: 0x000EE1B8
		public string Text
		{
			get
			{
				object obj = this.Properties["Text"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
		}

		/// <summary>Overrides <see cref="M:System.Object.ToString" />.</summary>
		/// <returns>The verb's text, or an empty string ("") if the text field is empty.</returns>
		// Token: 0x06003790 RID: 14224 RVA: 0x000EFFE5 File Offset: 0x000EE1E5
		public override string ToString()
		{
			return this.Text + " : " + base.ToString();
		}
	}
}
