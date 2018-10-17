using CrmDeploy.Enums;

namespace CrmDeploy.Configuration
{
    public class StepConfiguration
    {
        public SdkMessageNames SdkMessageNames { get; set; }

        public string PrimaryEntityName { get; set; }

        public string SecondaryEntityName { get; set; } = string.Empty;

        public string FilteringAttributes { get; set; }
    }
}