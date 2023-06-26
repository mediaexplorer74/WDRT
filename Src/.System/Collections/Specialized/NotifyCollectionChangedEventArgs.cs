using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Specialized
{
	/// <summary>Provides data for the <see cref="E:System.Collections.Specialized.INotifyCollectionChanged.CollectionChanged" /> event.</summary>
	// Token: 0x020003B2 RID: 946
	[TypeForwardedFrom("WindowsBase, Version=3.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	[global::__DynamicallyInvokable]
	public class NotifyCollectionChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> class that describes a <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Reset" /> change.</summary>
		/// <param name="action">The action that caused the event. This must be set to <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Reset" />.</param>
		// Token: 0x06002367 RID: 9063 RVA: 0x000A793C File Offset: 0x000A5B3C
		[global::__DynamicallyInvokable]
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action)
		{
			if (action != NotifyCollectionChangedAction.Reset)
			{
				throw new ArgumentException(SR.GetString("WrongActionForCtor", new object[] { NotifyCollectionChangedAction.Reset }), "action");
			}
			this.InitializeAdd(action, null, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> class that describes a one-item change.</summary>
		/// <param name="action">The action that caused the event. This can be set to <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Reset" />, <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Add" />, or <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Remove" />.</param>
		/// <param name="changedItem">The item that is affected by the change.</param>
		/// <exception cref="T:System.ArgumentException">If <paramref name="action" /> is not Reset, Add, or Remove, or if <paramref name="action" /> is Reset and <paramref name="changedItem" /> is not null.</exception>
		// Token: 0x06002368 RID: 9064 RVA: 0x000A7990 File Offset: 0x000A5B90
		[global::__DynamicallyInvokable]
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object changedItem)
		{
			if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.Remove && action != NotifyCollectionChangedAction.Reset)
			{
				throw new ArgumentException(SR.GetString("MustBeResetAddOrRemoveActionForCtor"), "action");
			}
			if (action != NotifyCollectionChangedAction.Reset)
			{
				this.InitializeAddOrRemove(action, new object[] { changedItem }, -1);
				return;
			}
			if (changedItem != null)
			{
				throw new ArgumentException(SR.GetString("ResetActionRequiresNullItem"), "action");
			}
			this.InitializeAdd(action, null, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> class that describes a one-item change.</summary>
		/// <param name="action">The action that caused the event. This can be set to <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Reset" />, <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Add" />, or <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Remove" />.</param>
		/// <param name="changedItem">The item that is affected by the change.</param>
		/// <param name="index">The index where the change occurred.</param>
		/// <exception cref="T:System.ArgumentException">If <paramref name="action" /> is not Reset, Add, or Remove, or if <paramref name="action" /> is Reset and either <paramref name="changedItems" /> is not null or <paramref name="index" /> is not -1.</exception>
		// Token: 0x06002369 RID: 9065 RVA: 0x000A7A0C File Offset: 0x000A5C0C
		[global::__DynamicallyInvokable]
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object changedItem, int index)
		{
			if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.Remove && action != NotifyCollectionChangedAction.Reset)
			{
				throw new ArgumentException(SR.GetString("MustBeResetAddOrRemoveActionForCtor"), "action");
			}
			if (action != NotifyCollectionChangedAction.Reset)
			{
				this.InitializeAddOrRemove(action, new object[] { changedItem }, index);
				return;
			}
			if (changedItem != null)
			{
				throw new ArgumentException(SR.GetString("ResetActionRequiresNullItem"), "action");
			}
			if (index != -1)
			{
				throw new ArgumentException(SR.GetString("ResetActionRequiresIndexMinus1"), "action");
			}
			this.InitializeAdd(action, null, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> class that describes a multi-item change.</summary>
		/// <param name="action">The action that caused the event. This can be set to <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Reset" />, <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Add" />, or <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Remove" />.</param>
		/// <param name="changedItems">The items that are affected by the change.</param>
		// Token: 0x0600236A RID: 9066 RVA: 0x000A7AA0 File Offset: 0x000A5CA0
		[global::__DynamicallyInvokable]
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems)
		{
			if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.Remove && action != NotifyCollectionChangedAction.Reset)
			{
				throw new ArgumentException(SR.GetString("MustBeResetAddOrRemoveActionForCtor"), "action");
			}
			if (action == NotifyCollectionChangedAction.Reset)
			{
				if (changedItems != null)
				{
					throw new ArgumentException(SR.GetString("ResetActionRequiresNullItem"), "action");
				}
				this.InitializeAdd(action, null, -1);
				return;
			}
			else
			{
				if (changedItems == null)
				{
					throw new ArgumentNullException("changedItems");
				}
				this.InitializeAddOrRemove(action, changedItems, -1);
				return;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> class that describes a multi-item change or a <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Reset" /> change.</summary>
		/// <param name="action">The action that caused the event. This can be set to <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Reset" />, <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Add" />, or <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Remove" />.</param>
		/// <param name="changedItems">The items affected by the change.</param>
		/// <param name="startingIndex">The index where the change occurred.</param>
		/// <exception cref="T:System.ArgumentException">If <paramref name="action" /> is not Reset, Add, or Remove, if <paramref name="action" /> is Reset and either <paramref name="changedItems" /> is not null or <paramref name="startingIndex" /> is not -1, or if action is Add or Remove and <paramref name="startingIndex" /> is less than -1.</exception>
		/// <exception cref="T:System.ArgumentNullException">If <paramref name="action" /> is Add or Remove and <paramref name="changedItems" /> is null.</exception>
		// Token: 0x0600236B RID: 9067 RVA: 0x000A7B20 File Offset: 0x000A5D20
		[global::__DynamicallyInvokable]
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems, int startingIndex)
		{
			if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.Remove && action != NotifyCollectionChangedAction.Reset)
			{
				throw new ArgumentException(SR.GetString("MustBeResetAddOrRemoveActionForCtor"), "action");
			}
			if (action == NotifyCollectionChangedAction.Reset)
			{
				if (changedItems != null)
				{
					throw new ArgumentException(SR.GetString("ResetActionRequiresNullItem"), "action");
				}
				if (startingIndex != -1)
				{
					throw new ArgumentException(SR.GetString("ResetActionRequiresIndexMinus1"), "action");
				}
				this.InitializeAdd(action, null, -1);
				return;
			}
			else
			{
				if (changedItems == null)
				{
					throw new ArgumentNullException("changedItems");
				}
				if (startingIndex < -1)
				{
					throw new ArgumentException(SR.GetString("IndexCannotBeNegative"), "startingIndex");
				}
				this.InitializeAddOrRemove(action, changedItems, startingIndex);
				return;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> class that describes a one-item <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Replace" /> change.</summary>
		/// <param name="action">The action that caused the event. This can only be set to <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Replace" />.</param>
		/// <param name="newItem">The new item that is replacing the original item.</param>
		/// <param name="oldItem">The original item that is replaced.</param>
		/// <exception cref="T:System.ArgumentException">If <paramref name="action" /> is not Replace.</exception>
		// Token: 0x0600236C RID: 9068 RVA: 0x000A7BD0 File Offset: 0x000A5DD0
		[global::__DynamicallyInvokable]
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object newItem, object oldItem)
		{
			if (action != NotifyCollectionChangedAction.Replace)
			{
				throw new ArgumentException(SR.GetString("WrongActionForCtor", new object[] { NotifyCollectionChangedAction.Replace }), "action");
			}
			this.InitializeMoveOrReplace(action, new object[] { newItem }, new object[] { oldItem }, -1, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> class that describes a one-item <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Replace" /> change.</summary>
		/// <param name="action">The action that caused the event. This can be set to <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Replace" />.</param>
		/// <param name="newItem">The new item that is replacing the original item.</param>
		/// <param name="oldItem">The original item that is replaced.</param>
		/// <param name="index">The index of the item being replaced.</param>
		/// <exception cref="T:System.ArgumentException">If <paramref name="action" /> is not Replace.</exception>
		// Token: 0x0600236D RID: 9069 RVA: 0x000A7C38 File Offset: 0x000A5E38
		[global::__DynamicallyInvokable]
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object newItem, object oldItem, int index)
		{
			if (action != NotifyCollectionChangedAction.Replace)
			{
				throw new ArgumentException(SR.GetString("WrongActionForCtor", new object[] { NotifyCollectionChangedAction.Replace }), "action");
			}
			this.InitializeMoveOrReplace(action, new object[] { newItem }, new object[] { oldItem }, index, index);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> class that describes a multi-item <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Replace" /> change.</summary>
		/// <param name="action">The action that caused the event. This can only be set to <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Replace" />.</param>
		/// <param name="newItems">The new items that are replacing the original items.</param>
		/// <param name="oldItems">The original items that are replaced.</param>
		/// <exception cref="T:System.ArgumentException">If <paramref name="action" /> is not Replace.</exception>
		/// <exception cref="T:System.ArgumentNullException">If <paramref name="oldItems" /> or <paramref name="newItems" /> is null.</exception>
		// Token: 0x0600236E RID: 9070 RVA: 0x000A7CA4 File Offset: 0x000A5EA4
		[global::__DynamicallyInvokable]
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems)
		{
			if (action != NotifyCollectionChangedAction.Replace)
			{
				throw new ArgumentException(SR.GetString("WrongActionForCtor", new object[] { NotifyCollectionChangedAction.Replace }), "action");
			}
			if (newItems == null)
			{
				throw new ArgumentNullException("newItems");
			}
			if (oldItems == null)
			{
				throw new ArgumentNullException("oldItems");
			}
			this.InitializeMoveOrReplace(action, newItems, oldItems, -1, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> class that describes a multi-item <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Replace" /> change.</summary>
		/// <param name="action">The action that caused the event. This can only be set to <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Replace" />.</param>
		/// <param name="newItems">The new items that are replacing the original items.</param>
		/// <param name="oldItems">The original items that are replaced.</param>
		/// <param name="startingIndex">The index of the first item of the items that are being replaced.</param>
		/// <exception cref="T:System.ArgumentException">If <paramref name="action" /> is not Replace.</exception>
		/// <exception cref="T:System.ArgumentNullException">If <paramref name="oldItems" /> or <paramref name="newItems" /> is null.</exception>
		// Token: 0x0600236F RID: 9071 RVA: 0x000A7D14 File Offset: 0x000A5F14
		[global::__DynamicallyInvokable]
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems, int startingIndex)
		{
			if (action != NotifyCollectionChangedAction.Replace)
			{
				throw new ArgumentException(SR.GetString("WrongActionForCtor", new object[] { NotifyCollectionChangedAction.Replace }), "action");
			}
			if (newItems == null)
			{
				throw new ArgumentNullException("newItems");
			}
			if (oldItems == null)
			{
				throw new ArgumentNullException("oldItems");
			}
			this.InitializeMoveOrReplace(action, newItems, oldItems, startingIndex, startingIndex);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> class that describes a one-item <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Move" /> change.</summary>
		/// <param name="action">The action that caused the event. This can only be set to <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Move" />.</param>
		/// <param name="changedItem">The item affected by the change.</param>
		/// <param name="index">The new index for the changed item.</param>
		/// <param name="oldIndex">The old index for the changed item.</param>
		/// <exception cref="T:System.ArgumentException">If <paramref name="action" /> is not Move or <paramref name="index" /> is less than 0.</exception>
		// Token: 0x06002370 RID: 9072 RVA: 0x000A7D88 File Offset: 0x000A5F88
		[global::__DynamicallyInvokable]
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object changedItem, int index, int oldIndex)
		{
			if (action != NotifyCollectionChangedAction.Move)
			{
				throw new ArgumentException(SR.GetString("WrongActionForCtor", new object[] { NotifyCollectionChangedAction.Move }), "action");
			}
			if (index < 0)
			{
				throw new ArgumentException(SR.GetString("IndexCannotBeNegative"), "index");
			}
			object[] array = new object[] { changedItem };
			this.InitializeMoveOrReplace(action, array, array, index, oldIndex);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> class that describes a multi-item <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Move" /> change.</summary>
		/// <param name="action">The action that caused the event. This can only be set to <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Move" />.</param>
		/// <param name="changedItems">The items affected by the change.</param>
		/// <param name="index">The new index for the changed items.</param>
		/// <param name="oldIndex">The old index for the changed items.</param>
		/// <exception cref="T:System.ArgumentException">If <paramref name="action" /> is not Move or <paramref name="index" /> is less than 0.</exception>
		// Token: 0x06002371 RID: 9073 RVA: 0x000A7E04 File Offset: 0x000A6004
		[global::__DynamicallyInvokable]
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems, int index, int oldIndex)
		{
			if (action != NotifyCollectionChangedAction.Move)
			{
				throw new ArgumentException(SR.GetString("WrongActionForCtor", new object[] { NotifyCollectionChangedAction.Move }), "action");
			}
			if (index < 0)
			{
				throw new ArgumentException(SR.GetString("IndexCannotBeNegative"), "index");
			}
			this.InitializeMoveOrReplace(action, changedItems, changedItems, index, oldIndex);
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x000A7E74 File Offset: 0x000A6074
		internal NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems, int newIndex, int oldIndex)
		{
			this._action = action;
			this._newItems = ((newItems == null) ? null : ArrayList.ReadOnly(newItems));
			this._oldItems = ((oldItems == null) ? null : ArrayList.ReadOnly(oldItems));
			this._newStartingIndex = newIndex;
			this._oldStartingIndex = oldIndex;
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x000A7ED0 File Offset: 0x000A60D0
		private void InitializeAddOrRemove(NotifyCollectionChangedAction action, IList changedItems, int startingIndex)
		{
			if (action == NotifyCollectionChangedAction.Add)
			{
				this.InitializeAdd(action, changedItems, startingIndex);
				return;
			}
			if (action == NotifyCollectionChangedAction.Remove)
			{
				this.InitializeRemove(action, changedItems, startingIndex);
			}
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x000A7EEC File Offset: 0x000A60EC
		private void InitializeAdd(NotifyCollectionChangedAction action, IList newItems, int newStartingIndex)
		{
			this._action = action;
			this._newItems = ((newItems == null) ? null : ArrayList.ReadOnly(newItems));
			this._newStartingIndex = newStartingIndex;
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x000A7F0E File Offset: 0x000A610E
		private void InitializeRemove(NotifyCollectionChangedAction action, IList oldItems, int oldStartingIndex)
		{
			this._action = action;
			this._oldItems = ((oldItems == null) ? null : ArrayList.ReadOnly(oldItems));
			this._oldStartingIndex = oldStartingIndex;
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x000A7F30 File Offset: 0x000A6130
		private void InitializeMoveOrReplace(NotifyCollectionChangedAction action, IList newItems, IList oldItems, int startingIndex, int oldStartingIndex)
		{
			this.InitializeAdd(action, newItems, startingIndex);
			this.InitializeRemove(action, oldItems, oldStartingIndex);
		}

		/// <summary>Gets the action that caused the event.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NotifyCollectionChangedAction" /> value that describes the action that caused the event.</returns>
		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06002377 RID: 9079 RVA: 0x000A7F46 File Offset: 0x000A6146
		[global::__DynamicallyInvokable]
		public NotifyCollectionChangedAction Action
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this._action;
			}
		}

		/// <summary>Gets the list of new items involved in the change.</summary>
		/// <returns>The list of new items involved in the change.</returns>
		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06002378 RID: 9080 RVA: 0x000A7F4E File Offset: 0x000A614E
		[global::__DynamicallyInvokable]
		public IList NewItems
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this._newItems;
			}
		}

		/// <summary>Gets the list of items affected by a <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Replace" />, Remove, or Move action.</summary>
		/// <returns>The list of items affected by a <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Replace" />, Remove, or Move action.</returns>
		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06002379 RID: 9081 RVA: 0x000A7F56 File Offset: 0x000A6156
		[global::__DynamicallyInvokable]
		public IList OldItems
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this._oldItems;
			}
		}

		/// <summary>Gets the index at which the change occurred.</summary>
		/// <returns>The zero-based index at which the change occurred.</returns>
		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x0600237A RID: 9082 RVA: 0x000A7F5E File Offset: 0x000A615E
		[global::__DynamicallyInvokable]
		public int NewStartingIndex
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this._newStartingIndex;
			}
		}

		/// <summary>Gets the index at which a <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Move" />, Remove, or Replace action occurred.</summary>
		/// <returns>The zero-based index at which a <see cref="F:System.Collections.Specialized.NotifyCollectionChangedAction.Move" />, Remove, or Replace action occurred.</returns>
		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x0600237B RID: 9083 RVA: 0x000A7F66 File Offset: 0x000A6166
		[global::__DynamicallyInvokable]
		public int OldStartingIndex
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this._oldStartingIndex;
			}
		}

		// Token: 0x04001FCA RID: 8138
		private NotifyCollectionChangedAction _action;

		// Token: 0x04001FCB RID: 8139
		private IList _newItems;

		// Token: 0x04001FCC RID: 8140
		private IList _oldItems;

		// Token: 0x04001FCD RID: 8141
		private int _newStartingIndex = -1;

		// Token: 0x04001FCE RID: 8142
		private int _oldStartingIndex = -1;
	}
}
