﻿using System;
using CrmDeploy.Configuration;
using CrmDeploy.Connection;
using CrmDeploy.Enums;
using Microsoft.Xrm.Sdk;

namespace CrmDeploy
{
    public class PluginStepOptionsBuilder
    {

        protected PluginStepRegistration PluginStepRegistration { get; set; }

        public PluginTypeOptionsBuilder PluginTypeOptions { get; set; }

        public PluginStepOptionsBuilder(PluginTypeOptionsBuilder pluginTypeOptionsBuilder,
                                        PluginStepRegistration pluginStepRegistration)
        {
            PluginTypeOptions = pluginTypeOptionsBuilder;
            PluginStepRegistration = pluginStepRegistration;
            Rank(1);
        }

        public PluginStepOptionsBuilder SupportedDeployment(PluginStepDeployment deployment)
        {
            var pl = PluginStepRegistration.SdkMessageProcessingStep;
            pl.SupportedDeployment = new OptionSetValue()
                {
                    Value = (int)deployment
                };
            return this;
        }

        [Obsolete]
        public PluginStepOptionsBuilder InvocationSource(PluginStepInvocationSource invocationSource)
        {
            var pl = PluginStepRegistration.SdkMessageProcessingStep;
            pl.InvocationSource = new OptionSetValue()
                {
                    Value = (int)invocationSource
                };
            return this;
        }

        public PluginStepOptionsBuilder Stage(PluginStepStage stage)
        {
            var pl = PluginStepRegistration.SdkMessageProcessingStep;
            pl.Stage = new OptionSetValue()
                {
                    Value = (int)stage
                };
            return this;
        }

        public PluginStepOptionsBuilder Mode(PluginStepMode mode)
        {
            var pl = PluginStepRegistration.SdkMessageProcessingStep;
            pl.Mode = new OptionSetValue()
                {
                    Value = (int)mode
                };
            return this;
        }

        /// <summary>
        /// The rank helps determine the order in which plugins are executed when there are multiple registrations for the same entity / sdk message. 
        /// </summary>
        /// <param name="rank"></param>
        /// <returns></returns>
        public PluginStepOptionsBuilder Rank(int rank)
        {
            var pl = PluginStepRegistration.SdkMessageProcessingStep;
            pl.Rank = rank;
            return this;
        }

        public PluginStepOptionsBuilder Synchronously()
        {
            this.Mode(PluginStepMode.Synchronous);
            return this;
        }

        public PluginStepOptionsBuilder Asynchronously()
        {
            this.Mode(PluginStepMode.Asynchronous);
            return this;
        }

        public PluginStepOptionsBuilder PostOperation()
        {
            this.Stage(PluginStepStage.PostOperation);
            return this;
        }

        public PluginStepOptionsBuilder PreOperation()
        {
            this.Stage(PluginStepStage.PreOperation);
            return this;
        }

        public PluginStepOptionsBuilder PreValidation()
        {
            this.Stage(PluginStepStage.PreValidation);
            return this;
        }

        public PluginStepOptionsBuilder OnlyOnCrmServer()
        {
            this.SupportedDeployment(PluginStepDeployment.ServerOnly);
            return this;
        }

        public PluginStepOptionsBuilder OnlyOffline()
        {
            this.SupportedDeployment(PluginStepDeployment.OfflineOnly);
            return this;
        }

        public PluginStepOptionsBuilder OnCrmServerAndOffline()
        {
            this.SupportedDeployment(PluginStepDeployment.Both);
            return this;
        }

        public IRegistrationDeployer DeployTo(string orgConnectionString)
        {
            return PluginTypeOptions.PluginAssemblyOptions.RegistrationOptions.DeployTo(orgConnectionString);
        }

        public IRegistrationDeployer DeployTo(ICrmServiceProvider crmserviceProvider)
        {
            return PluginTypeOptions.PluginAssemblyOptions.RegistrationOptions.DeployTo(crmserviceProvider);
        }

        public PluginTypeOptionsBuilder AndHasPlugin<T>() where T : IPlugin
        {
            return PluginTypeOptions.PluginAssemblyOptions.HasPlugin<T>();
        }

        public PluginStepOptionsBuilder AndExecutesOn(SdkMessageNames messageName, string primaryEntityName, string secondaryEntityName = "")
        {
            return AndExecutesOn(messageName.ToString(), primaryEntityName, secondaryEntityName);
        }

        public PluginStepOptionsBuilder AndExecutesOn(string messageName, string primaryEntityName, string secondaryEntityName = "")
        {
            return PluginTypeOptions.WhichExecutesOn(messageName, primaryEntityName, secondaryEntityName);
        }

        public PluginStepOptionsBuilder AndExecutesOn(StepConfiguration configuration)
        {
            return PluginTypeOptions.WhichExecutesOn(configuration);
        }
    }
}