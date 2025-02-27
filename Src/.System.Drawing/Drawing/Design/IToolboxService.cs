﻿using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;

namespace System.Drawing.Design
{
	/// <summary>Provides methods and properties to manage and query the toolbox in the development environment.</summary>
	// Token: 0x02000075 RID: 117
	[Guid("4BACD258-DE64-4048-BC4E-FEDBEF9ACB76")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IToolboxService
	{
		/// <summary>Gets the names of all the tool categories currently on the toolbox.</summary>
		/// <returns>A <see cref="T:System.Drawing.Design.CategoryNameCollection" /> containing the tool categories.</returns>
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000837 RID: 2103
		CategoryNameCollection CategoryNames { get; }

		/// <summary>Gets or sets the name of the currently selected tool category from the toolbox.</summary>
		/// <returns>The name of the currently selected category.</returns>
		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000838 RID: 2104
		// (set) Token: 0x06000839 RID: 2105
		string SelectedCategory { get; set; }

		/// <summary>Adds a new toolbox item creator for a specified data format.</summary>
		/// <param name="creator">A <see cref="T:System.Drawing.Design.ToolboxItemCreatorCallback" /> that can create a component when the toolbox item is invoked.</param>
		/// <param name="format">The data format that the creator handles.</param>
		// Token: 0x0600083A RID: 2106
		void AddCreator(ToolboxItemCreatorCallback creator, string format);

		/// <summary>Adds a new toolbox item creator for a specified data format and designer host.</summary>
		/// <param name="creator">A <see cref="T:System.Drawing.Design.ToolboxItemCreatorCallback" /> that can create a component when the toolbox item is invoked.</param>
		/// <param name="format">The data format that the creator handles.</param>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that represents the designer host to associate with the creator.</param>
		// Token: 0x0600083B RID: 2107
		void AddCreator(ToolboxItemCreatorCallback creator, string format, IDesignerHost host);

		/// <summary>Adds the specified project-linked toolbox item to the toolbox.</summary>
		/// <param name="toolboxItem">The linked <see cref="T:System.Drawing.Design.ToolboxItem" /> to add to the toolbox.</param>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> for the current design document.</param>
		// Token: 0x0600083C RID: 2108
		void AddLinkedToolboxItem(ToolboxItem toolboxItem, IDesignerHost host);

		/// <summary>Adds the specified project-linked toolbox item to the toolbox in the specified category.</summary>
		/// <param name="toolboxItem">The linked <see cref="T:System.Drawing.Design.ToolboxItem" /> to add to the toolbox.</param>
		/// <param name="category">The toolbox item category to add the toolbox item to.</param>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> for the current design document.</param>
		// Token: 0x0600083D RID: 2109
		void AddLinkedToolboxItem(ToolboxItem toolboxItem, string category, IDesignerHost host);

		/// <summary>Adds the specified toolbox item to the toolbox.</summary>
		/// <param name="toolboxItem">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to add to the toolbox.</param>
		// Token: 0x0600083E RID: 2110
		void AddToolboxItem(ToolboxItem toolboxItem);

		/// <summary>Adds the specified toolbox item to the toolbox in the specified category.</summary>
		/// <param name="toolboxItem">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to add to the toolbox.</param>
		/// <param name="category">The toolbox item category to add the <see cref="T:System.Drawing.Design.ToolboxItem" /> to.</param>
		// Token: 0x0600083F RID: 2111
		void AddToolboxItem(ToolboxItem toolboxItem, string category);

		/// <summary>Gets a toolbox item from the specified object that represents a toolbox item in serialized form.</summary>
		/// <param name="serializedObject">The object that contains the <see cref="T:System.Drawing.Design.ToolboxItem" /> to retrieve.</param>
		/// <returns>The <see cref="T:System.Drawing.Design.ToolboxItem" /> created from the serialized object.</returns>
		// Token: 0x06000840 RID: 2112
		ToolboxItem DeserializeToolboxItem(object serializedObject);

		/// <summary>Gets a toolbox item from the specified object that represents a toolbox item in serialized form, using the specified designer host.</summary>
		/// <param name="serializedObject">The object that contains the <see cref="T:System.Drawing.Design.ToolboxItem" /> to retrieve.</param>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> to associate with this <see cref="T:System.Drawing.Design.ToolboxItem" />.</param>
		/// <returns>The <see cref="T:System.Drawing.Design.ToolboxItem" /> created from deserialization.</returns>
		// Token: 0x06000841 RID: 2113
		ToolboxItem DeserializeToolboxItem(object serializedObject, IDesignerHost host);

		/// <summary>Gets the currently selected toolbox item.</summary>
		/// <returns>The <see cref="T:System.Drawing.Design.ToolboxItem" /> that is currently selected, or <see langword="null" /> if no toolbox item has been selected.</returns>
		// Token: 0x06000842 RID: 2114
		ToolboxItem GetSelectedToolboxItem();

		/// <summary>Gets the currently selected toolbox item if it is available to all designers, or if it supports the specified designer.</summary>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that the selected tool must be associated with for it to be returned.</param>
		/// <returns>The <see cref="T:System.Drawing.Design.ToolboxItem" /> that is currently selected, or <see langword="null" /> if no toolbox item is currently selected.</returns>
		// Token: 0x06000843 RID: 2115
		ToolboxItem GetSelectedToolboxItem(IDesignerHost host);

		/// <summary>Gets the entire collection of toolbox items from the toolbox.</summary>
		/// <returns>A <see cref="T:System.Drawing.Design.ToolboxItemCollection" /> that contains the current toolbox items.</returns>
		// Token: 0x06000844 RID: 2116
		ToolboxItemCollection GetToolboxItems();

		/// <summary>Gets the collection of toolbox items that are associated with the specified designer host from the toolbox.</summary>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that is associated with the toolbox items to retrieve.</param>
		/// <returns>A <see cref="T:System.Drawing.Design.ToolboxItemCollection" /> that contains the current toolbox items that are associated with the specified designer host.</returns>
		// Token: 0x06000845 RID: 2117
		ToolboxItemCollection GetToolboxItems(IDesignerHost host);

		/// <summary>Gets a collection of toolbox items from the toolbox that match the specified category.</summary>
		/// <param name="category">The toolbox item category to retrieve all the toolbox items from.</param>
		/// <returns>A <see cref="T:System.Drawing.Design.ToolboxItemCollection" /> that contains the current toolbox items that are associated with the specified category.</returns>
		// Token: 0x06000846 RID: 2118
		ToolboxItemCollection GetToolboxItems(string category);

		/// <summary>Gets the collection of toolbox items that are associated with the specified designer host and category from the toolbox.</summary>
		/// <param name="category">The toolbox item category to retrieve the toolbox items from.</param>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that is associated with the toolbox items to retrieve.</param>
		/// <returns>A <see cref="T:System.Drawing.Design.ToolboxItemCollection" /> that contains the current toolbox items that are associated with the specified category and designer host.</returns>
		// Token: 0x06000847 RID: 2119
		ToolboxItemCollection GetToolboxItems(string category, IDesignerHost host);

		/// <summary>Gets a value indicating whether the specified object which represents a serialized toolbox item can be used by the specified designer host.</summary>
		/// <param name="serializedObject">The object that contains the <see cref="T:System.Drawing.Design.ToolboxItem" /> to retrieve.</param>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> to test for support for the <see cref="T:System.Drawing.Design.ToolboxItem" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified object is compatible with the specified designer host; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000848 RID: 2120
		bool IsSupported(object serializedObject, IDesignerHost host);

		/// <summary>Gets a value indicating whether the specified object which represents a serialized toolbox item matches the specified attributes.</summary>
		/// <param name="serializedObject">The object that contains the <see cref="T:System.Drawing.Design.ToolboxItem" /> to retrieve.</param>
		/// <param name="filterAttributes">An <see cref="T:System.Collections.ICollection" /> that contains the attributes to test the serialized object for.</param>
		/// <returns>
		///   <see langword="true" /> if the object matches the specified attributes; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000849 RID: 2121
		bool IsSupported(object serializedObject, ICollection filterAttributes);

		/// <summary>Gets a value indicating whether the specified object is a serialized toolbox item.</summary>
		/// <param name="serializedObject">The object to inspect.</param>
		/// <returns>
		///   <see langword="true" /> if the object contains a toolbox item object; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600084A RID: 2122
		bool IsToolboxItem(object serializedObject);

		/// <summary>Gets a value indicating whether the specified object is a serialized toolbox item, using the specified designer host.</summary>
		/// <param name="serializedObject">The object to inspect.</param>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that is making this request.</param>
		/// <returns>
		///   <see langword="true" /> if the object contains a toolbox item object; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600084B RID: 2123
		bool IsToolboxItem(object serializedObject, IDesignerHost host);

		/// <summary>Refreshes the state of the toolbox items.</summary>
		// Token: 0x0600084C RID: 2124
		void Refresh();

		/// <summary>Removes a previously added toolbox item creator of the specified data format.</summary>
		/// <param name="format">The data format of the creator to remove.</param>
		// Token: 0x0600084D RID: 2125
		void RemoveCreator(string format);

		/// <summary>Removes a previously added toolbox creator that is associated with the specified data format and the specified designer host.</summary>
		/// <param name="format">The data format of the creator to remove.</param>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that is associated with the creator to remove.</param>
		// Token: 0x0600084E RID: 2126
		void RemoveCreator(string format, IDesignerHost host);

		/// <summary>Removes the specified toolbox item from the toolbox.</summary>
		/// <param name="toolboxItem">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to remove from the toolbox.</param>
		// Token: 0x0600084F RID: 2127
		void RemoveToolboxItem(ToolboxItem toolboxItem);

		/// <summary>Removes the specified toolbox item from the toolbox.</summary>
		/// <param name="toolboxItem">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to remove from the toolbox.</param>
		/// <param name="category">The toolbox item category to remove the <see cref="T:System.Drawing.Design.ToolboxItem" /> from.</param>
		// Token: 0x06000850 RID: 2128
		void RemoveToolboxItem(ToolboxItem toolboxItem, string category);

		/// <summary>Notifies the toolbox service that the selected tool has been used.</summary>
		// Token: 0x06000851 RID: 2129
		void SelectedToolboxItemUsed();

		/// <summary>Gets a serializable object that represents the specified toolbox item.</summary>
		/// <param name="toolboxItem">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to serialize.</param>
		/// <returns>An object that represents the specified <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
		// Token: 0x06000852 RID: 2130
		object SerializeToolboxItem(ToolboxItem toolboxItem);

		/// <summary>Sets the current application's cursor to a cursor that represents the currently selected tool.</summary>
		/// <returns>
		///   <see langword="true" /> if the cursor is set by the currently selected tool, <see langword="false" /> if there is no tool selected and the cursor is set to the standard windows cursor.</returns>
		// Token: 0x06000853 RID: 2131
		bool SetCursor();

		/// <summary>Selects the specified toolbox item.</summary>
		/// <param name="toolboxItem">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to select.</param>
		// Token: 0x06000854 RID: 2132
		void SetSelectedToolboxItem(ToolboxItem toolboxItem);
	}
}
