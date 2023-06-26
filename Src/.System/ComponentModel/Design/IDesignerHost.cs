using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface for managing designer transactions and components.</summary>
	// Token: 0x020005E8 RID: 1512
	[ComVisible(true)]
	public interface IDesignerHost : IServiceContainer, IServiceProvider
	{
		/// <summary>Gets a value indicating whether the designer host is currently loading the document.</summary>
		/// <returns>
		///   <see langword="true" /> if the designer host is currently loading the document; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x060037EE RID: 14318
		bool Loading { get; }

		/// <summary>Gets a value indicating whether the designer host is currently in a transaction.</summary>
		/// <returns>
		///   <see langword="true" /> if a transaction is in progress; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x060037EF RID: 14319
		bool InTransaction { get; }

		/// <summary>Gets the container for this designer host.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IContainer" /> for this host.</returns>
		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x060037F0 RID: 14320
		IContainer Container { get; }

		/// <summary>Gets the instance of the base class used as the root component for the current design.</summary>
		/// <returns>The instance of the root component class.</returns>
		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x060037F1 RID: 14321
		IComponent RootComponent { get; }

		/// <summary>Gets the fully qualified name of the class being designed.</summary>
		/// <returns>The fully qualified name of the base component class.</returns>
		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x060037F2 RID: 14322
		string RootComponentClassName { get; }

		/// <summary>Gets the description of the current transaction.</summary>
		/// <returns>A description of the current transaction.</returns>
		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x060037F3 RID: 14323
		string TransactionDescription { get; }

		/// <summary>Occurs when this designer is activated.</summary>
		// Token: 0x1400005E RID: 94
		// (add) Token: 0x060037F4 RID: 14324
		// (remove) Token: 0x060037F5 RID: 14325
		event EventHandler Activated;

		/// <summary>Occurs when this designer is deactivated.</summary>
		// Token: 0x1400005F RID: 95
		// (add) Token: 0x060037F6 RID: 14326
		// (remove) Token: 0x060037F7 RID: 14327
		event EventHandler Deactivated;

		/// <summary>Occurs when this designer completes loading its document.</summary>
		// Token: 0x14000060 RID: 96
		// (add) Token: 0x060037F8 RID: 14328
		// (remove) Token: 0x060037F9 RID: 14329
		event EventHandler LoadComplete;

		/// <summary>Adds an event handler for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosed" /> event.</summary>
		// Token: 0x14000061 RID: 97
		// (add) Token: 0x060037FA RID: 14330
		// (remove) Token: 0x060037FB RID: 14331
		event DesignerTransactionCloseEventHandler TransactionClosed;

		/// <summary>Adds an event handler for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosing" /> event.</summary>
		// Token: 0x14000062 RID: 98
		// (add) Token: 0x060037FC RID: 14332
		// (remove) Token: 0x060037FD RID: 14333
		event DesignerTransactionCloseEventHandler TransactionClosing;

		/// <summary>Adds an event handler for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionOpened" /> event.</summary>
		// Token: 0x14000063 RID: 99
		// (add) Token: 0x060037FE RID: 14334
		// (remove) Token: 0x060037FF RID: 14335
		event EventHandler TransactionOpened;

		/// <summary>Adds an event handler for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionOpening" /> event.</summary>
		// Token: 0x14000064 RID: 100
		// (add) Token: 0x06003800 RID: 14336
		// (remove) Token: 0x06003801 RID: 14337
		event EventHandler TransactionOpening;

		/// <summary>Activates the designer that this host is hosting.</summary>
		// Token: 0x06003802 RID: 14338
		void Activate();

		/// <summary>Creates a component of the specified type and adds it to the design document.</summary>
		/// <param name="componentClass">The type of the component to create.</param>
		/// <returns>The newly created component.</returns>
		// Token: 0x06003803 RID: 14339
		IComponent CreateComponent(Type componentClass);

		/// <summary>Creates a component of the specified type and name, and adds it to the design document.</summary>
		/// <param name="componentClass">The type of the component to create.</param>
		/// <param name="name">The name for the component.</param>
		/// <returns>The newly created component.</returns>
		// Token: 0x06003804 RID: 14340
		IComponent CreateComponent(Type componentClass, string name);

		/// <summary>Creates a <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> that can encapsulate event sequences to improve performance and enable undo and redo support functionality.</summary>
		/// <returns>A new instance of <see cref="T:System.ComponentModel.Design.DesignerTransaction" />. When you complete the steps in your transaction, you should call <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on this object.</returns>
		// Token: 0x06003805 RID: 14341
		DesignerTransaction CreateTransaction();

		/// <summary>Creates a <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> that can encapsulate event sequences to improve performance and enable undo and redo support functionality, using the specified transaction description.</summary>
		/// <param name="description">A title or description for the newly created transaction.</param>
		/// <returns>A new <see cref="T:System.ComponentModel.Design.DesignerTransaction" />. When you have completed the steps in your transaction, you should call <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on this object.</returns>
		// Token: 0x06003806 RID: 14342
		DesignerTransaction CreateTransaction(string description);

		/// <summary>Destroys the specified component and removes it from the designer container.</summary>
		/// <param name="component">The component to destroy.</param>
		// Token: 0x06003807 RID: 14343
		void DestroyComponent(IComponent component);

		/// <summary>Gets the designer instance that contains the specified component.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to retrieve the designer for.</param>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesigner" />, or <see langword="null" /> if there is no designer for the specified component.</returns>
		// Token: 0x06003808 RID: 14344
		IDesigner GetDesigner(IComponent component);

		/// <summary>Gets an instance of the specified, fully qualified type name.</summary>
		/// <param name="typeName">The name of the type to load.</param>
		/// <returns>The type object for the specified type name, or <see langword="null" /> if the type cannot be found.</returns>
		// Token: 0x06003809 RID: 14345
		Type GetType(string typeName);
	}
}
