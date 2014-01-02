<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="e299d892-3cd7-4eca-88db-3da02232964b" namespace="Beetle.Tracker" xmlSchemaNamespace="urn:Beetle.Tracker" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
  <typeDefinitions>
    <externalType name="String" namespace="System" />
    <externalType name="Boolean" namespace="System" />
    <externalType name="Int32" namespace="System" />
    <externalType name="Int64" namespace="System" />
    <externalType name="Single" namespace="System" />
    <externalType name="Double" namespace="System" />
    <externalType name="DateTime" namespace="System" />
    <externalType name="TimeSpan" namespace="System" />
  </typeDefinitions>
  <configurationElements>
    <configurationSection name="TrackerSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="trackerSection">
      <attributeProperties>
        <attributeProperty name="AppName" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="appName" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/e299d892-3cd7-4eca-88db-3da02232964b/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="Trackers" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="trackers" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/e299d892-3cd7-4eca-88db-3da02232964b/TrackerHostCollection" />
          </type>
        </elementProperty>
        <elementProperty name="Properties" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="properties" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/e299d892-3cd7-4eca-88db-3da02232964b/PropertyCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElement name="PropertyConfig">
      <attributeProperties>
        <attributeProperty name="Name" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="name" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/e299d892-3cd7-4eca-88db-3da02232964b/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Value" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="value" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/e299d892-3cd7-4eca-88db-3da02232964b/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="PropertyCollection" collectionType="AddRemoveClearMap" xmlItemName="propertyConfig" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/e299d892-3cd7-4eca-88db-3da02232964b/PropertyConfig" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="TrackerHostConf">
      <attributeProperties>
        <attributeProperty name="Host" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="host" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/e299d892-3cd7-4eca-88db-3da02232964b/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Port" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="port" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/e299d892-3cd7-4eca-88db-3da02232964b/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="Name" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="name" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/e299d892-3cd7-4eca-88db-3da02232964b/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="TrackerHostCollection" collectionType="AddRemoveClearMap" xmlItemName="tackerHostConf" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/e299d892-3cd7-4eca-88db-3da02232964b/TrackerHostConf" />
      </itemType>
    </configurationElementCollection>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>