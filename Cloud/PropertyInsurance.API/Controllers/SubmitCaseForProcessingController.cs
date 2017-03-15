using Microsoft.Azure.Mobile.Server.Config;
using PropertyInsurance.API.Helper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace PropertyInsurance.API.Controllers
{
    [Authorize, MobileAppController]
    public class SubmitCaseForProcessingController : ApiController
    {
        public async Task<HttpResponseMessage> Post([FromBody] Models.MobilePropertyInsurance value)
        {
            try
            {
                var properties = new Dictionary<string, string>();

                properties.Add("ClaimDescription", value.ClaimDescription == null
                    ? string.Empty : value.ClaimDescription);

                properties.Add("ClaimDateTime", value.ClaimDateTime.ToString());

                //Retrieve current user's email 
                var customerEmail = ClaimsPrincipal.Current.FindFirst("emails").Value;

                string claimsAdjusterEmail = (await MtcsHelper.GetMtcByCustomerEmail(customerEmail))["adjuster"]["email"].ToString();

                var PropertyInsurance = new Models.PropertyInsurance
                {
                    claimDetailsPageUrl = Settings.ClaimDetailsPageUrl,
                    claimsAdjusterEmail = claimsAdjusterEmail,
                    CustomerEmail = customerEmail,
                    CorrelationId = "0",
                    ImageUrl = value.ImageUrl,
                    BlobFilePath = new Uri(value.ImageUrl).PathAndQuery,
                    Properties = properties,
                    TagExpression = value.Tag != null ? value.Tag : string.Empty
                };

                var json = new JavaScriptSerializer().Serialize(PropertyInsurance);

                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                HttpClient httpClient = new HttpClient();

                var httpResponse = await httpClient.PostAsync(Settings.ApprovePictureLogicApp, httpContent);

                var responseContent = await httpResponse.Content.ReadAsStringAsync();

                return Request.CreateResponse(HttpStatusCode.OK, responseContent, new MediaTypeHeaderValue("application/json"));
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
}
    }
}