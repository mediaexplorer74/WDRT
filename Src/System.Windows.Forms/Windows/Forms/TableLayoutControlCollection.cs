using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;

namespace System.Windows.Forms
{
	/// <summary>Represents a collection of child controls in a table layout container.</summary>
	// Token: 0x0200038D RID: 909
	[ListBindable(false)]
	[DesignerSerializer("System.Windows.Forms.Design.TableLayoutControlCollectionCodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class TableLayoutControlCollection : Control.ControlCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TableLayoutControlCollection" /> class.</summary>
		/// <param name="container">The <see cref="T:System.Windows.Forms.TableLayoutPanel" /> control that contains the control collection.</param>
		// Token: 0x06003BDA RID: 15322 RVA: 0x00105DB8 File Offset: 0x00103FB8
		public TableLayoutControlCollection(TableLayoutPanel container)
			: base(container)
		{
			this._container = container;
		}

		/// <summary>Gets the parent <see cref="T:System.Windows.Forms.TableLayoutPanel" /> that contains the controls in the collection.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TableLayoutPanel" /> that contains the controls in the current collection.</returns>
		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x06003BDB RID: 15323 RVA: 0x00105DC8 File Offset: 0x00103FC8
		public TableLayoutPanel Container
		{
			get
			{
				return this._container;
			}
		}

		/// <summary>Adds the specified control to the collection and positions it at the specified cell.</summary>
		/// <param name="control">The control to add.</param>
		/// <param name="column">The column in which <paramref name="control" /> will be placed.</param>
		/// <param name="row">The row in which <paramref name="control" /> will be placed.</param>
		/// <exception cref="T:System.ArgumentException">Either <paramref name="column" /> or <paramref name="row" /> is less than -1.</exception>
		// Token: 0x06003BDC RID: 15324 RVA: 0x00105DD0 File Offset: 0x00103FD0
		public virtual void Add(Control control, int column, int row)
		{
			base.Add(control);
			this._container.SetColumn(control, column);
			this._container.SetRow(control, row);
		}

		// Token: 0x04002377 RID: 9079
		private TableLayoutPanel _container;
	}
}
