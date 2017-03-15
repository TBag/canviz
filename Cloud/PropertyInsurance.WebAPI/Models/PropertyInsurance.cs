using System.Collections.Generic;

namespace PropertyInsurance.WebAPI.Models
{
    public class PropertyInsurance
    {
        public string CustomerEmail { get; set; }

        public string claimDetailsPageUrl { get; set; }
        
        public string claimsAdjusterEmail { get; set; }

        public string ImageUrl { get; set; }

        public string CorrelationId { get; set; }

        public Dictionary<string, string> Properties { get; set; }

        public string TagExpression { get; set; }

        public string BlobFilePath { get; set; }
    }
}