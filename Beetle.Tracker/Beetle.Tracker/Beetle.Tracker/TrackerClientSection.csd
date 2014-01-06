<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="1c5aa38c-91e4-438a-ab2c-bf58c36acbb2" namespace="Beetle.Tracker" xmlSchemaNamespace="urn:Beetle.Tracker" assemblyName="Beetle.Tracker" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
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
    <configurationSection name="TrackerClientSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="trackerClientSection">
      <attributeProperties>
        <attributeProperty name="Host" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="host" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/1c5aa38c-91e4-438a-ab2c-bf58c36acbb2/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Port" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="port" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/1c5aa38c-91e4-438a-ab2c-bf58c36acbb2/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="AppName" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="appName" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/1c5aa38c-91e4-438a-ab2c-bf58c36acbb2/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Connections" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="connections" isReadOnly="false" defaultValue="5">
          <type>
            <externalTypeMoniker name="/1c5aa38c-91e4-438a-ab2c-bf58c36acbb2/Int32" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationSection>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>