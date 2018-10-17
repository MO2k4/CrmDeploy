﻿using CrmDeploy.Configuration;
using CrmDeploy.Enums;

namespace CrmDeploy
{
    public class PluginTypeOptionsBuilder
    {
        protected PluginTypeRegistration PluginTypeRegistration { get; set; }

        public PluginAssemblyOptionsBuilder PluginAssemblyOptions { get; set; }

        public PluginTypeOptionsBuilder(PluginAssemblyOptionsBuilder pluginAssemblyOptionsBuilder, PluginTypeRegistration pluginTypeRegistration)
        {
            PluginAssemblyOptions = pluginAssemblyOptionsBuilder;
            PluginTypeRegistration = pluginTypeRegistration;
        }

        public PluginStepOptionsBuilder WhichExecutesOn(string sdkMessageName, string primaryEntityLogicalName, string secondaryEntityLogicalName = "")
        {
            var pluginStepRegistration = new PluginStepRegistration(this.PluginTypeRegistration, sdkMessageName, primaryEntityLogicalName, secondaryEntityLogicalName);
            PluginTypeRegistration.PluginStepRegistrations.Add(pluginStepRegistration);
            return new PluginStepOptionsBuilder(this, pluginStepRegistration);
        }

        public PluginStepOptionsBuilder WhichExecutesOn(SdkMessageNames sdkMessageName, string primaryEntityLogicalName, string secondaryEntityLogicalName = "")
        {
            var pluginStepRegistration = new PluginStepRegistration(this.PluginTypeRegistration, sdkMessageName, primaryEntityLogicalName, secondaryEntityLogicalName);
            PluginTypeRegistration.PluginStepRegistrations.Add(pluginStepRegistration);
            return new PluginStepOptionsBuilder(this, pluginStepRegistration);
        }

        public PluginStepOptionsBuilder WhichExecutesOn(StepConfiguration configuration)
        {
            var pluginStepRegistration = new PluginStepRegistration(PluginTypeRegistration, configuration);
            PluginTypeRegistration.PluginStepRegistrations.Add(pluginStepRegistration);
            return new PluginStepOptionsBuilder(this, pluginStepRegistration);
        }
    }
}