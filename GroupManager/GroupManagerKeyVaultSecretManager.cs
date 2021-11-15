using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;

namespace GroupManager
{
    public class GroupManagerKeyVaultSecretManager : KeyVaultSecretManager
    {
        public override string GetKey(KeyVaultSecret secret) => secret.Name.Replace("--", ConfigurationPath.KeyDelimiter);
    }
}
