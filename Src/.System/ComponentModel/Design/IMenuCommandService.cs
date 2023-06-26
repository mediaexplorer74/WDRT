using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides methods to manage the global designer verbs and menu commands available in design mode, and to show some types of shortcut menus.</summary>
	// Token: 0x020005F1 RID: 1521
	[ComVisible(true)]
	public interface IMenuCommandService
	{
		/// <summary>Gets a collection of the designer verbs that are currently available.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" /> that contains the designer verbs that are currently available.</returns>
		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x06003824 RID: 14372
		DesignerVerbCollection Verbs { get; }

		/// <summary>Adds the specified standard menu command to the menu.</summary>
		/// <param name="command">The <see cref="T:System.ComponentModel.Design.MenuCommand" /> to add.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.ComponentModel.Design.CommandID" /> of the specified <see cref="T:System.ComponentModel.Design.MenuCommand" /> is already present on a menu.</exception>
		// Token: 0x06003825 RID: 14373
		void AddCommand(MenuCommand command);

		/// <summary>Adds the specified designer verb to the set of global designer verbs.</summary>
		/// <param name="verb">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to add.</param>
		// Token: 0x06003826 RID: 14374
		void AddVerb(DesignerVerb verb);

		/// <summary>Searches for the specified command ID and returns the menu command associated with it.</summary>
		/// <param name="commandID">The <see cref="T:System.ComponentModel.Design.CommandID" /> to search for.</param>
		/// <returns>The <see cref="T:System.ComponentModel.Design.MenuCommand" /> associated with the command ID, or <see langword="null" /> if no command is found.</returns>
		// Token: 0x06003827 RID: 14375
		MenuCommand FindCommand(CommandID commandID);

		/// <summary>Invokes a menu or designer verb command matching the specified command ID.</summary>
		/// <param name="commandID">The <see cref="T:System.ComponentModel.Design.CommandID" /> of the command to search for and execute.</param>
		/// <returns>
		///   <see langword="true" /> if the command was found and invoked successfully; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003828 RID: 14376
		bool GlobalInvoke(CommandID commandID);

		/// <summary>Removes the specified standard menu command from the menu.</summary>
		/// <param name="command">The <see cref="T:System.ComponentModel.Design.MenuCommand" /> to remove.</param>
		// Token: 0x06003829 RID: 14377
		void RemoveCommand(MenuCommand command);

		/// <summary>Removes the specified designer verb from the collection of global designer verbs.</summary>
		/// <param name="verb">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to remove.</param>
		// Token: 0x0600382A RID: 14378
		void RemoveVerb(DesignerVerb verb);

		/// <summary>Shows the specified shortcut menu at the specified location.</summary>
		/// <param name="menuID">The <see cref="T:System.ComponentModel.Design.CommandID" /> for the shortcut menu to show.</param>
		/// <param name="x">The x-coordinate at which to display the menu, in screen coordinates.</param>
		/// <param name="y">The y-coordinate at which to display the menu, in screen coordinates.</param>
		// Token: 0x0600382B RID: 14379
		void ShowContextMenu(CommandID menuID, int x, int y);
	}
}
