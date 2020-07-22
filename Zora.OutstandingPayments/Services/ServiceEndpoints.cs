using System;
using System.Linq;

namespace Zora.OutstandingPayments.Services
{
    public class ServiceEndpoints
    {
   
        public string Students { get; private set; }

        public string Payments { get; private set; }

        public string this[string service]
         => this.GetType()
             .GetProperties()
             .Where(pr => string
                 .Equals(pr.Name, service, StringComparison.CurrentCultureIgnoreCase))
             .Select(pr => (string)pr.GetValue(this))
             .FirstOrDefault()
             ?? throw new InvalidOperationException(
                 $"External service with name '{service}' does not exists.");
    }
}
