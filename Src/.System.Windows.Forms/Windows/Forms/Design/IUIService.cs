﻿using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.Design
{
	/// <summary>Enables interaction with the user interface of the development environment object that is hosting the designer.</summary>
	// Token: 0x02000489 RID: 1161
	[Guid("06A9C74B-5E32-4561-BE73-381B37869F4F")]
	public interface IUIService
	{
		/// <summary>Gets the collection of styles that are specific to the host's environment.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing style settings.</returns>
		// Token: 0x17001336 RID: 4918
		// (get) Token: 0x06004E19 RID: 19993
		IDictionary Styles { get; }

		/// <summary>Indicates whether the component can display a <see cref="T:System.Windows.Forms.Design.ComponentEditorForm" />.</summary>
		/// <param name="component">The component to check for support for displaying a <see cref="T:System.Windows.Forms.Design.ComponentEditorForm" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified component can display a component editor form; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004E1A RID: 19994
		bool CanShowComponentEditor(object component);

		/// <summary>Gets the window that should be used as the owner when showing dialog boxes.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.IWin32Window" /> that indicates the window to own any child dialog boxes.</returns>
		// Token: 0x06004E1B RID: 19995
		IWin32Window GetDialogOwnerWindow();

		/// <summary>Sets a flag indicating the UI has changed.</summary>
		// Token: 0x06004E1C RID: 19996
		void SetUIDirty();

		/// <summary>Attempts to display a <see cref="T:System.Windows.Forms.Design.ComponentEditorForm" /> for a component.</summary>
		/// <param name="component">The component for which to display a <see cref="T:System.Windows.Forms.Design.ComponentEditorForm" />.</param>
		/// <param name="parent">The <see cref="T:System.Windows.Forms.IWin32Window" /> to parent any dialog boxes to.</param>
		/// <returns>
		///   <see langword="true" /> if the attempt is successful; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The component does not support component editors.</exception>
		// Token: 0x06004E1D RID: 19997
		bool ShowComponentEditor(object component, IWin32Window parent);

		/// <summary>Attempts to display the specified form in a dialog box.</summary>
		/// <param name="form">The <see cref="T:System.Windows.Forms.Form" /> to display.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values indicating the result code returned by the dialog box.</returns>
		// Token: 0x06004E1E RID: 19998
		DialogResult ShowDialog(Form form);

		/// <summary>Displays the specified error message in a message box.</summary>
		/// <param name="message">The error message to display.</param>
		// Token: 0x06004E1F RID: 19999
		void ShowError(string message);

		/// <summary>Displays the specified exception and information about the exception in a message box.</summary>
		/// <param name="ex">The <see cref="T:System.Exception" /> to display.</param>
		// Token: 0x06004E20 RID: 20000
		void ShowError(Exception ex);

		/// <summary>Displays the specified exception and information about the exception in a message box.</summary>
		/// <param name="ex">The <see cref="T:System.Exception" /> to display.</param>
		/// <param name="message">A message to display that provides information about the exception.</param>
		// Token: 0x06004E21 RID: 20001
		void ShowError(Exception ex, string message);

		/// <summary>Displays the specified message in a message box.</summary>
		/// <param name="message">The message to display</param>
		// Token: 0x06004E22 RID: 20002
		void ShowMessage(string message);

		/// <summary>Displays the specified message in a message box with the specified caption.</summary>
		/// <param name="message">The message to display.</param>
		/// <param name="caption">The caption for the message box.</param>
		// Token: 0x06004E23 RID: 20003
		void ShowMessage(string message, string caption);

		/// <summary>Displays the specified message in a message box with the specified caption and buttons to place on the dialog box.</summary>
		/// <param name="message">The message to display.</param>
		/// <param name="caption">The caption for the dialog box.</param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values: <see cref="F:System.Windows.Forms.MessageBoxButtons.OK" />, <see cref="F:System.Windows.Forms.MessageBoxButtons.OKCancel" />, <see cref="F:System.Windows.Forms.MessageBoxButtons.YesNo" />, or <see cref="F:System.Windows.Forms.MessageBoxButtons.YesNoCancel" />.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values indicating the result code returned by the dialog box.</returns>
		// Token: 0x06004E24 RID: 20004
		DialogResult ShowMessage(string message, string caption, MessageBoxButtons buttons);

		/// <summary>Displays the specified tool window.</summary>
		/// <param name="toolWindow">A <see cref="T:System.Guid" /> identifier for the tool window. This can be a custom <see cref="T:System.Guid" /> or one of the predefined values from <see cref="T:System.ComponentModel.Design.StandardToolWindows" />.</param>
		/// <returns>
		///   <see langword="true" /> if the tool window was successfully shown; <see langword="false" /> if it could not be shown or found.</returns>
		// Token: 0x06004E25 RID: 20005
		bool ShowToolWindow(Guid toolWindow);
	}
}
