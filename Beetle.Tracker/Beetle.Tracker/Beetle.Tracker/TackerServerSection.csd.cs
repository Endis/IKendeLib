//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.17929
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Beetle.Tracker
{
    
    
    /// <summary>
    /// The TrackerServerSection Configuration Section.
    /// </summary>
    public partial class TrackerServerSection : global::System.Configuration.ConfigurationSection
    {
        
        #region Singleton Instance
        /// <summary>
        /// The XML name of the TrackerServerSection Configuration Section.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        internal const string TrackerServerSectionSectionName = "trackerServerSection";
        
        /// <summary>
        /// Gets the TrackerServerSection instance.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        public static global::Beetle.Tracker.TrackerServerSection Instance
        {
            get
            {
                return ((global::Beetle.Tracker.TrackerServerSection)(global::System.Configuration.ConfigurationManager.GetSection(global::Beetle.Tracker.TrackerServerSection.TrackerServerSectionSectionName)));
            }
        }
        #endregion
        
        #region Xmlns Property
        /// <summary>
        /// The XML name of the <see cref="Xmlns"/> property.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        internal const string XmlnsPropertyName = "xmlns";
        
        /// <summary>
        /// Gets the XML namespace of this Configuration Section.
        /// </summary>
        /// <remarks>
        /// This property makes sure that if the configuration file contains the XML namespace,
        /// the parser doesn't throw an exception because it encounters the unknown "xmlns" attribute.
        /// </remarks>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        [global::System.Configuration.ConfigurationPropertyAttribute(global::Beetle.Tracker.TrackerServerSection.XmlnsPropertyName, IsRequired=false, IsKey=false, IsDefaultCollection=false)]
        public string Xmlns
        {
            get
            {
                return ((string)(base[global::Beetle.Tracker.TrackerServerSection.XmlnsPropertyName]));
            }
        }
        #endregion
        
        #region IsReadOnly override
        /// <summary>
        /// Gets a value indicating whether the element is read-only.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        public override bool IsReadOnly()
        {
            return false;
        }
        #endregion
        
        #region Host Property
        /// <summary>
        /// The XML name of the <see cref="Host"/> property.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        internal const string HostPropertyName = "host";
        
        /// <summary>
        /// Gets or sets the Host.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        [global::System.ComponentModel.DescriptionAttribute("The Host.")]
        [global::System.Configuration.ConfigurationPropertyAttribute(global::Beetle.Tracker.TrackerServerSection.HostPropertyName, IsRequired=false, IsKey=false, IsDefaultCollection=false)]
        public virtual string Host
        {
            get
            {
                return ((string)(base[global::Beetle.Tracker.TrackerServerSection.HostPropertyName]));
            }
            set
            {
                base[global::Beetle.Tracker.TrackerServerSection.HostPropertyName] = value;
            }
        }
        #endregion
        
        #region Port Property
        /// <summary>
        /// The XML name of the <see cref="Port"/> property.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        internal const string PortPropertyName = "port";
        
        /// <summary>
        /// Gets or sets the Port.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        [global::System.ComponentModel.DescriptionAttribute("The Port.")]
        [global::System.Configuration.ConfigurationPropertyAttribute(global::Beetle.Tracker.TrackerServerSection.PortPropertyName, IsRequired=false, IsKey=false, IsDefaultCollection=false, DefaultValue=10028)]
        public virtual int Port
        {
            get
            {
                return ((int)(base[global::Beetle.Tracker.TrackerServerSection.PortPropertyName]));
            }
            set
            {
                base[global::Beetle.Tracker.TrackerServerSection.PortPropertyName] = value;
            }
        }
        #endregion
        
        #region Trackers Property
        /// <summary>
        /// The XML name of the <see cref="Trackers"/> property.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        internal const string TrackersPropertyName = "trackers";
        
        /// <summary>
        /// Gets or sets the Trackers.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        [global::System.ComponentModel.DescriptionAttribute("The Trackers.")]
        [global::System.Configuration.ConfigurationPropertyAttribute(global::Beetle.Tracker.TrackerServerSection.TrackersPropertyName, IsRequired=true, IsKey=false, IsDefaultCollection=false)]
        public virtual global::Beetle.Tracker.AppTrackerConfigCollection Trackers
        {
            get
            {
                return ((global::Beetle.Tracker.AppTrackerConfigCollection)(base[global::Beetle.Tracker.TrackerServerSection.TrackersPropertyName]));
            }
            set
            {
                base[global::Beetle.Tracker.TrackerServerSection.TrackersPropertyName] = value;
            }
        }
        #endregion
    }
}
namespace Beetle.Tracker
{
    
    
    /// <summary>
    /// The AppTrackerConfig Configuration Element.
    /// </summary>
    public partial class AppTrackerConfig : global::System.Configuration.ConfigurationElement
    {
        
        #region IsReadOnly override
        /// <summary>
        /// Gets a value indicating whether the element is read-only.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        public override bool IsReadOnly()
        {
            return false;
        }
        #endregion
        
        #region Name Property
        /// <summary>
        /// The XML name of the <see cref="Name"/> property.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        internal const string NamePropertyName = "name";
        
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        [global::System.ComponentModel.DescriptionAttribute("The Name.")]
        [global::System.Configuration.ConfigurationPropertyAttribute(global::Beetle.Tracker.AppTrackerConfig.NamePropertyName, IsRequired=true, IsKey=true, IsDefaultCollection=false)]
        public virtual string Name
        {
            get
            {
                return ((string)(base[global::Beetle.Tracker.AppTrackerConfig.NamePropertyName]));
            }
            set
            {
                base[global::Beetle.Tracker.AppTrackerConfig.NamePropertyName] = value;
            }
        }
        #endregion
        
        #region Type Property
        /// <summary>
        /// The XML name of the <see cref="Type"/> property.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        internal const string TypePropertyName = "type";
        
        /// <summary>
        /// Gets or sets the Type.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        [global::System.ComponentModel.DescriptionAttribute("The Type.")]
        [global::System.Configuration.ConfigurationPropertyAttribute(global::Beetle.Tracker.AppTrackerConfig.TypePropertyName, IsRequired=true, IsKey=false, IsDefaultCollection=false)]
        public virtual string Type
        {
            get
            {
                return ((string)(base[global::Beetle.Tracker.AppTrackerConfig.TypePropertyName]));
            }
            set
            {
                base[global::Beetle.Tracker.AppTrackerConfig.TypePropertyName] = value;
            }
        }
        #endregion
    }
}
namespace Beetle.Tracker
{
    
    
    /// <summary>
    /// A collection of AppTrackerConfig instances.
    /// </summary>
    [global::System.Configuration.ConfigurationCollectionAttribute(typeof(global::Beetle.Tracker.AppTrackerConfig), CollectionType=global::System.Configuration.ConfigurationElementCollectionType.AddRemoveClearMapAlternate, AddItemName="add", RemoveItemName="remove", ClearItemsName="clear")]
    public partial class AppTrackerConfigCollection : global::System.Configuration.ConfigurationElementCollection
    {
        
        #region Constants
        /// <summary>
        /// The XML name of the individual <see cref="global::Beetle.Tracker.AppTrackerConfig"/> instances in this collection.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        internal const string AppTrackerConfigPropertyName = "appTrackerConfig";
        #endregion
        
        #region Overrides
        /// <summary>
        /// Gets the type of the <see cref="global::System.Configuration.ConfigurationElementCollection"/>.
        /// </summary>
        /// <returns>The <see cref="global::System.Configuration.ConfigurationElementCollectionType"/> of this collection.</returns>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        public override global::System.Configuration.ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return global::System.Configuration.ConfigurationElementCollectionType.AddRemoveClearMapAlternate;
            }
        }
        
        /// <summary>
        /// Gets the name used to identify this collection of elements
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        protected override string ElementName
        {
            get
            {
                return global::Beetle.Tracker.AppTrackerConfigCollection.AppTrackerConfigPropertyName;
            }
        }
        
        /// <summary>
        /// Indicates whether the specified <see cref="global::System.Configuration.ConfigurationElement"/> exists in the <see cref="global::System.Configuration.ConfigurationElementCollection"/>.
        /// </summary>
        /// <param name="elementName">The name of the element to verify.</param>
        /// <returns>
        /// <see langword="true"/> if the element exists in the collection; otherwise, <see langword="false"/>.
        /// </returns>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        protected override bool IsElementName(string elementName)
        {
            return (elementName == global::Beetle.Tracker.AppTrackerConfigCollection.AppTrackerConfigPropertyName);
        }
        
        /// <summary>
        /// Gets the element key for the specified configuration element.
        /// </summary>
        /// <param name="element">The <see cref="global::System.Configuration.ConfigurationElement"/> to return the key for.</param>
        /// <returns>
        /// An <see cref="object"/> that acts as the key for the specified <see cref="global::System.Configuration.ConfigurationElement"/>.
        /// </returns>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        protected override object GetElementKey(global::System.Configuration.ConfigurationElement element)
        {
            return ((global::Beetle.Tracker.AppTrackerConfig)(element)).Name;
        }
        
        /// <summary>
        /// Creates a new <see cref="global::Beetle.Tracker.AppTrackerConfig"/>.
        /// </summary>
        /// <returns>
        /// A new <see cref="global::Beetle.Tracker.AppTrackerConfig"/>.
        /// </returns>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        protected override global::System.Configuration.ConfigurationElement CreateNewElement()
        {
            return new global::Beetle.Tracker.AppTrackerConfig();
        }
        #endregion
        
        #region Indexer
        /// <summary>
        /// Gets the <see cref="global::Beetle.Tracker.AppTrackerConfig"/> at the specified index.
        /// </summary>
        /// <param name="index">The index of the <see cref="global::Beetle.Tracker.AppTrackerConfig"/> to retrieve.</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        public global::Beetle.Tracker.AppTrackerConfig this[int index]
        {
            get
            {
                return ((global::Beetle.Tracker.AppTrackerConfig)(base.BaseGet(index)));
            }
        }
        
        /// <summary>
        /// Gets the <see cref="global::Beetle.Tracker.AppTrackerConfig"/> with the specified key.
        /// </summary>
        /// <param name="name">The key of the <see cref="global::Beetle.Tracker.AppTrackerConfig"/> to retrieve.</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        public global::Beetle.Tracker.AppTrackerConfig this[object name]
        {
            get
            {
                return ((global::Beetle.Tracker.AppTrackerConfig)(base.BaseGet(name)));
            }
        }
        #endregion
        
        #region Add
        /// <summary>
        /// Adds the specified <see cref="global::Beetle.Tracker.AppTrackerConfig"/> to the <see cref="global::System.Configuration.ConfigurationElementCollection"/>.
        /// </summary>
        /// <param name="appTrackerConfig">The <see cref="global::Beetle.Tracker.AppTrackerConfig"/> to add.</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        public void Add(global::Beetle.Tracker.AppTrackerConfig appTrackerConfig)
        {
            base.BaseAdd(appTrackerConfig);
        }
        #endregion
        
        #region Remove
        /// <summary>
        /// Removes the specified <see cref="global::Beetle.Tracker.AppTrackerConfig"/> from the <see cref="global::System.Configuration.ConfigurationElementCollection"/>.
        /// </summary>
        /// <param name="appTrackerConfig">The <see cref="global::Beetle.Tracker.AppTrackerConfig"/> to remove.</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        public void Remove(global::Beetle.Tracker.AppTrackerConfig appTrackerConfig)
        {
            base.BaseRemove(this.GetElementKey(appTrackerConfig));
        }
        #endregion
        
        #region GetItem
        /// <summary>
        /// Gets the <see cref="global::Beetle.Tracker.AppTrackerConfig"/> at the specified index.
        /// </summary>
        /// <param name="index">The index of the <see cref="global::Beetle.Tracker.AppTrackerConfig"/> to retrieve.</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        public global::Beetle.Tracker.AppTrackerConfig GetItemAt(int index)
        {
            return ((global::Beetle.Tracker.AppTrackerConfig)(base.BaseGet(index)));
        }
        
        /// <summary>
        /// Gets the <see cref="global::Beetle.Tracker.AppTrackerConfig"/> with the specified key.
        /// </summary>
        /// <param name="name">The key of the <see cref="global::Beetle.Tracker.AppTrackerConfig"/> to retrieve.</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        public global::Beetle.Tracker.AppTrackerConfig GetItemByKey(string name)
        {
            return ((global::Beetle.Tracker.AppTrackerConfig)(base.BaseGet(((object)(name)))));
        }
        #endregion
        
        #region IsReadOnly override
        /// <summary>
        /// Gets a value indicating whether the element is read-only.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        public override bool IsReadOnly()
        {
            return false;
        }
        #endregion
    }
}
