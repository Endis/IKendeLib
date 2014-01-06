<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="fbfcf1a1-0b87-45e8-90f4-3d0ce9137b0c" namespace="KGlue" xmlSchemaNamespace="urn:KGlue" assemblyName="KGlue" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
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
    <configurationSection name="AppSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="appSection">
      <elementProperties>
        <elementProperty name="Items" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="items" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/fbfcf1a1-0b87-45e8-90f4-3d0ce9137b0c/ItemCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElement name="Item">
      <attributeProperties>
        <attributeProperty name="Path" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="path" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/fbfcf1a1-0b87-45e8-90f4-3d0ce9137b0c/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Name" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="name" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/fbfcf1a1-0b87-45e8-90f4-3d0ce9137b0c/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Args" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="args" isReadOnly="false" defaultValue="&quot;&quot;">
          <type>
            <externalTypeMoniker name="/fbfcf1a1-0b87-45e8-90f4-3d0ce9137b0c/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="ItemCollection" collectionType="AddRemoveClearMapAlternate" xmlItemName="item" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/fbfcf1a1-0b87-45e8-90f4-3d0ce9137b0c/Item" />
      </itemType>
    </configurationElementCollection>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>