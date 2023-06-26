using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Markup;

namespace System.Windows.Input
{
	/// <summary>Defines a command.</summary>
	// Token: 0x020003A2 RID: 930
	[TypeForwardedFrom("PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[TypeConverter("System.Windows.Input.CommandConverter, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null")]
	[ValueSerializer("System.Windows.Input.CommandValueSerializer, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null")]
	[global::__DynamicallyInvokable]
	public interface ICommand
	{
		/// <summary>Occurs when changes occur that affect whether or not the command should execute.</summary>
		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06002298 RID: 8856
		// (remove) Token: 0x06002299 RID: 8857
		[global::__DynamicallyInvokable]
		event EventHandler CanExecuteChanged;

		/// <summary>Defines the method that determines whether the command can execute in its current state.</summary>
		/// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if this command can be executed; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600229A RID: 8858
		[global::__DynamicallyInvokable]
		bool CanExecute(object parameter);

		/// <summary>Defines the method to be called when the command is invoked.</summary>
		/// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
		// Token: 0x0600229B RID: 8859
		[global::__DynamicallyInvokable]
		void Execute(object parameter);
	}
}
