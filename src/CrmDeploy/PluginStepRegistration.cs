using CrmDeploy.Configuration;
using CrmDeploy.Entities;
using CrmDeploy.Enums;
using Microsoft.Xrm.Sdk;

namespace CrmDeploy
{
    public class PluginStepRegistration
    {

        public PluginStepRegistration(PluginTypeRegistration pluginTypeRegistration, string sdkMessageName, string primaryEntityName, string secondaryEntityName = "")
        {
            PluginTypeRegistration = pluginTypeRegistration;
            SdkMessageProcessingStep = new SdkMessageProcessingStep();
            SdkMessageName = sdkMessageName;
            PluginTypeRegistration.PluginType.PropertyChanged += PluginType_PropertyChanged;
            SdkMessageProcessingStep.plugintype_sdkmessageprocessingstep = pluginTypeRegistration.PluginType;
            SdkMessageProcessingStep.plugintypeid_sdkmessageprocessingstep = pluginTypeRegistration.PluginType;
            PrimaryEntityName = primaryEntityName;
            SecondaryEntityName = secondaryEntityName;
        }

        public PluginStepRegistration(PluginTypeRegistration pluginTypeRegistration, SdkMessageNames sdkMessageName, string primaryEntityName, string secondaryEntityName = "")
            : this(pluginTypeRegistration, sdkMessageName.ToString(), primaryEntityName, secondaryEntityName)
        { }

        public PluginStepRegistration(PluginTypeRegistration pluginTypeRegistration, StepConfiguration configuration)
            : this(pluginTypeRegistration, configuration.SdkMessageNames.ToString(), configuration.PrimaryEntityName, configuration.SecondaryEntityName)
        {
            SdkMessageProcessingStep.FilteringAttributes = configuration.FilteringAttributes;
        }

        private void PluginType_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var pluginType = PluginTypeRegistration.PluginType;
            
            if (e.PropertyName == "PluginTypeId")
                SdkMessageProcessingStep.EventHandler = pluginType.PluginTypeId == null ? null : new EntityReference(pluginType.LogicalName, pluginType.PluginTypeId.Value);
        }

        public PluginTypeRegistration PluginTypeRegistration { get; set; }

        public SdkMessageProcessingStep SdkMessageProcessingStep { get; set; }

        public string SdkMessageName { get; set; }

        public string PrimaryEntityName { get; set; }

        public string SecondaryEntityName { get; set; }


    }
}