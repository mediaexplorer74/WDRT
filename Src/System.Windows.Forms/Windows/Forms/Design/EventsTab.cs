using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms.Design
{
	/// <summary>Provides a <see cref="T:System.Windows.Forms.Design.PropertyTab" /> that can display events for selection and linking.</summary>
	// Token: 0x02000488 RID: 1160
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class EventsTab : PropertyTab
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.EventsTab" /> class.</summary>
		/// <param name="sp">An <see cref="T:System.IServiceProvider" /> to use.</param>
		// Token: 0x06004E10 RID: 19984 RVA: 0x00142308 File Offset: 0x00140508
		public EventsTab(IServiceProvider sp)
		{
			this.sp = sp;
		}

		/// <summary>Gets the name of the tab.</summary>
		/// <returns>The name of the tab.</returns>
		// Token: 0x17001334 RID: 4916
		// (get) Token: 0x06004E11 RID: 19985 RVA: 0x00142317 File Offset: 0x00140517
		public override string TabName
		{
			get
			{
				return SR.GetString("PBRSToolTipEvents");
			}
		}

		/// <summary>Gets the Help keyword for the tab.</summary>
		/// <returns>The Help keyword for the tab.</returns>
		// Token: 0x17001335 RID: 4917
		// (get) Token: 0x06004E12 RID: 19986 RVA: 0x00142323 File Offset: 0x00140523
		public override string HelpKeyword
		{
			get
			{
				return "Events";
			}
		}

		/// <summary>Gets a value indicating whether the specified object can be extended.</summary>
		/// <param name="extendee">The object to test for extensibility.</param>
		/// <returns>
		///   <see langword="true" /> if the specified object can be extended; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004E13 RID: 19987 RVA: 0x0014232A File Offset: 0x0014052A
		public override bool CanExtend(object extendee)
		{
			return !Marshal.IsComObject(extendee);
		}

		// Token: 0x06004E14 RID: 19988 RVA: 0x00142335 File Offset: 0x00140535
		private void OnActiveDesignerChanged(object sender, ActiveDesignerEventArgs adevent)
		{
			this.currentHost = adevent.NewDesigner;
		}

		/// <summary>Gets the default property from the specified object.</summary>
		/// <param name="obj">The object to retrieve the default property of.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> indicating the default property.</returns>
		// Token: 0x06004E15 RID: 19989 RVA: 0x00142344 File Offset: 0x00140544
		public override PropertyDescriptor GetDefaultProperty(object obj)
		{
			IEventBindingService eventPropertyService = this.GetEventPropertyService(obj, null);
			if (eventPropertyService == null)
			{
				return null;
			}
			EventDescriptor defaultEvent = TypeDescriptor.GetDefaultEvent(obj);
			if (defaultEvent != null)
			{
				return eventPropertyService.GetEventProperty(defaultEvent);
			}
			return null;
		}

		// Token: 0x06004E16 RID: 19990 RVA: 0x00142374 File Offset: 0x00140574
		private IEventBindingService GetEventPropertyService(object obj, ITypeDescriptorContext context)
		{
			IEventBindingService eventBindingService = null;
			if (!this.sunkEvent)
			{
				IDesignerEventService designerEventService = (IDesignerEventService)this.sp.GetService(typeof(IDesignerEventService));
				if (designerEventService != null)
				{
					designerEventService.ActiveDesignerChanged += this.OnActiveDesignerChanged;
				}
				this.sunkEvent = true;
			}
			if (eventBindingService == null && this.currentHost != null)
			{
				eventBindingService = (IEventBindingService)this.currentHost.GetService(typeof(IEventBindingService));
			}
			if (eventBindingService == null && obj is IComponent)
			{
				ISite site = ((IComponent)obj).Site;
				if (site != null)
				{
					eventBindingService = (IEventBindingService)site.GetService(typeof(IEventBindingService));
				}
			}
			if (eventBindingService == null && context != null)
			{
				eventBindingService = (IEventBindingService)context.GetService(typeof(IEventBindingService));
			}
			return eventBindingService;
		}

		/// <summary>Gets all the properties of the event tab that match the specified attributes.</summary>
		/// <param name="component">The component to retrieve the properties of.</param>
		/// <param name="attributes">An array of <see cref="T:System.Attribute" /> that indicates the attributes of the event properties to retrieve.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the properties. This will be an empty <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> if the component does not implement an event service.</returns>
		// Token: 0x06004E17 RID: 19991 RVA: 0x00142435 File Offset: 0x00140635
		public override PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes)
		{
			return this.GetProperties(null, component, attributes);
		}

		/// <summary>Gets all the properties of the event tab that match the specified attributes and context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain context information.</param>
		/// <param name="component">The component to retrieve the properties of.</param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that indicates the attributes of the event properties to retrieve.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the properties. This will be an empty <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> if the component does not implement an event service.</returns>
		// Token: 0x06004E18 RID: 19992 RVA: 0x00142440 File Offset: 0x00140640
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object component, Attribute[] attributes)
		{
			IEventBindingService eventPropertyService = this.GetEventPropertyService(component, context);
			if (eventPropertyService == null)
			{
				return new PropertyDescriptorCollection(null);
			}
			EventDescriptorCollection events = TypeDescriptor.GetEvents(component, attributes);
			PropertyDescriptorCollection propertyDescriptorCollection = eventPropertyService.GetEventProperties(events);
			Attribute[] array = new Attribute[attributes.Length + 1];
			Array.Copy(attributes, 0, array, 0, attributes.Length);
			array[attributes.Length] = DesignerSerializationVisibilityAttribute.Content;
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component, array);
			if (properties.Count > 0)
			{
				ArrayList arrayList = null;
				for (int i = 0; i < properties.Count; i++)
				{
					PropertyDescriptor propertyDescriptor = properties[i];
					TypeConverter converter = propertyDescriptor.Converter;
					if (converter.GetPropertiesSupported())
					{
						object value = propertyDescriptor.GetValue(component);
						EventDescriptorCollection events2 = TypeDescriptor.GetEvents(value, attributes);
						if (events2.Count > 0)
						{
							if (arrayList == null)
							{
								arrayList = new ArrayList();
							}
							propertyDescriptor = TypeDescriptor.CreateProperty(propertyDescriptor.ComponentType, propertyDescriptor, new Attribute[] { MergablePropertyAttribute.No });
							arrayList.Add(propertyDescriptor);
						}
					}
				}
				if (arrayList != null)
				{
					PropertyDescriptor[] array2 = new PropertyDescriptor[arrayList.Count];
					arrayList.CopyTo(array2, 0);
					PropertyDescriptor[] array3 = new PropertyDescriptor[propertyDescriptorCollection.Count + array2.Length];
					propertyDescriptorCollection.CopyTo(array3, 0);
					Array.Copy(array2, 0, array3, propertyDescriptorCollection.Count, array2.Length);
					propertyDescriptorCollection = new PropertyDescriptorCollection(array3);
				}
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x040033E8 RID: 13288
		private IServiceProvider sp;

		// Token: 0x040033E9 RID: 13289
		private IDesignerHost currentHost;

		// Token: 0x040033EA RID: 13290
		private bool sunkEvent;
	}
}
