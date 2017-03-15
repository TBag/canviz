using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace PropertyInsurance.WebAPI.Controllers
{
    [Authorize]
    public class UploadIncidentPictureController : ApiController
    {
        public HttpResponseMessage Post()
        {
            //Check if the request contains multipart / form - data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            try
            {
                string fileName = "sampleImage.jpg";

                fileName = Regex.Replace(Settings.Tenant, @"\.", "_") + "_" + HttpContext.Current.Request.Files[0].FileName;

                // Retrieve storage account from connection string.
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                    Settings.StorageConnectionString);

                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve a reference to a container.
                CloudBlobContainer container = blobClient.GetContainerReference(Settings.BlobContainerName);

                // Create the container if it doesn't already exist.
                container.CreateIfNotExists();

                container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                // Retrieve reference to a blob named "myblob".
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

                // Create or overwrite the "myblob" blob with contents from a local file.
                // Read the form data.
                blockBlob.UploadFromStream(HttpContext.Current.Request.Files[0].InputStream);

                return Request.CreateResponse(HttpStatusCode.OK, blockBlob.SnapshotQualifiedUri.ToString());
                
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
