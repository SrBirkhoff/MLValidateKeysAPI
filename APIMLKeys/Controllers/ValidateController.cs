using APIMLKeys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace APIMLKeys.Controllers
{
    public class ValidateController : ApiController
    {
        private List<Keys> keys = new List<Keys>();

        [HttpPost][HttpGet]
        [ResponseType(typeof(Keys))]
        public HttpResponseMessage CheckKeysGETPOST(string clientID, string clientSecret)
        {
            keys.Clear();

            if(!string.IsNullOrEmpty(clientID) && !string.IsNullOrEmpty(clientSecret))
            {
                int RequestCode = MercadoLivreGetSessionStatus(clientID, clientSecret); // tries to get a Mercado Livre Access token
                string retorno = requestsStatusChecker(RequestCode);
                keys.Add(new Keys(clientID, clientSecret, retorno));
                
                if(RequestCode == 200) {
                    return Request.CreateResponse(HttpStatusCode.OK, keys);
                } else if (RequestCode == 400)
                {
                    //return Request.CreateResponse(HttpStatusCode.Unauthorized, keys);
                    return Request.CreateResponse(HttpStatusCode.NonAuthoritativeInformation, keys);
                }
                else {
                   return Request.CreateResponse(HttpStatusCode.InternalServerError, keys);
                }
            }
            else
            {
                keys.Add(new Keys("","", "Valores de clientID e/ou clientSecret vázios ou inválidos, por favor informe corretamente os dados no formato URL/api/Validate?clientID=IDENTIFICACAO&clientSecret=CHAVE"));
                //return Request.CreateResponse(HttpStatusCode.BadRequest, keys);
                return Request.CreateResponse(HttpStatusCode.PartialContent, keys);
            }
        }

        public static int MercadoLivreGetSessionStatus(string idMercadoLivre, string secretKeyMercadoLivre)
        {

            var webRequest = WebRequest.CreateHttp("https://api.mercadolibre.com/oauth/token?grant_type=client_credentials&client_id=" + idMercadoLivre + "&client_secret=" + secretKeyMercadoLivre);
            webRequest.Method = "POST";

            try
            {
                var webResponse = (HttpWebResponse)webRequest.GetResponse();
                var WebStatusCode = webResponse.StatusCode;

                Console.WriteLine("OK> " + (int)WebStatusCode);

                return (int)WebStatusCode;
            }
            catch (WebException we)
            {
                var WebStatusCode = ((HttpWebResponse)we.Response).StatusCode;
                Console.WriteLine("Error> " + (int)WebStatusCode);

                return (int)WebStatusCode;
            }

        }

        public static string requestsStatusChecker(int RequestCode)
        {
            if (RequestCode == 200) // verify the HTTP code
            {
                return "OK";
            }
            else if (RequestCode == 400)
            {
                return "Inválido";
            }
            else
            {
                return "ERROR";
            }

        }
    }
}
