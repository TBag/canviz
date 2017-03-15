using Newtonsoft.Json;
using PropertyInsurance.Web.Models;
using PropertyInsurance.Web.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PropertyInsurance.Web.Controllers
{
    [Authorize]
    public class ClaimController : Controller
    {
        public async Task<ActionResult> Index()
        {
            await InitUserProfile();
            InitMockClaimList();
            return View();
        }

        public async Task<ActionResult> Builders()
        {
            await InitUserProfile();
            InitMockBuilderList();
            return View();
        }

        public ActionResult Consent()
        {
            var stateMarker = Guid.NewGuid().ToString();
            var consentUrl = AuthorizationHelper.GetUrl(stateMarker);
            return new RedirectResult(consentUrl);
        }

        public async Task<ActionResult> Detail(string id)
        {
            ViewBag.Message = "This is Claim Detail";
            await InitUserProfile();
            ViewBag.Claim = ClaimUtil.GetMockClaimViewModel();
            InitMockClaimList();
            //ViewBag.ClaimImage = await ClaimUtil.GetMockImageFromBlob();
            return View();
        }

        [HttpPost, Route("approve")]
        public async Task<ActionResult> Approve(int id, bool approved)
        {
            var url = Constants.ClaimApproverUrl;
            var tag = await ClaimUtil.GetMockCutomerEmail();
            var postJson = approved ? new {
                message= "The claim has been approved",
                tag = tag

            } :new{
                message = "The claim has been declined",
                tag = tag
            };
            var response = await PostTo(url,postJson);
            return Json(response);
        }

        private void InitMockClaimList()
        {
            ViewBag.ClaimHistoryList = ClaimUtil.GetMockClaimViewModelList();
        }

        private void InitMockBuilderList()
        {
            ViewBag.BuilderList = ClaimUtil.GetMockBuilderViewModelList();
        }

        private async Task InitUserProfile()
        {
            var thumbnail = await ClaimUtil.GetThumbnailString();
            if (thumbnail.IndexOf("~/") >= 0)
            {
                thumbnail = Url.Content(thumbnail);//default image
            }
            ViewBag.Thumbnail = thumbnail;
        }

        private static async Task<HttpResponseMessage> PostTo(string url, string content, string mediaType = "text/plain")
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            requestMessage.Content = new StringContent(content, Encoding.UTF8, mediaType);

            var client = new HttpClient();
            var responseMessage = await client.SendAsync(requestMessage);
            responseMessage.EnsureSuccessStatusCode();
            return responseMessage;
        }

        private static Task<HttpResponseMessage> PostTo(string url, object obj)
        {
            var content = JsonConvert.SerializeObject(obj);
            return PostTo(url, content, "application/json");
        }
    }
}