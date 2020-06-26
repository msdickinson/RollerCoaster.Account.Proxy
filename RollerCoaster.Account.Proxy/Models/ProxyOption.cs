using System.Diagnostics.CodeAnalysis;

namespace RollerCoaster.Account.Proxy.Models
{
    [ExcludeFromCodeCoverage]
    public class ProxyOption
    {
        public double TimeoutInSeconds { get; set; }
        public int Retrys { get; set; }
        public string Resource { get; set; }
    }
}
