using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;

namespace ExtractEDIFACTMessages
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ParseService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ParseService.svc or ParseService.svc.cs at the Solution Explorer and start debugging.
    public class ParseService : IParseService
    {
        public void DoWork()
        {
            HttpWebRequest request = CreateWebRequest();
            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(@"<InputDocument>
	                                    <DeclarationList>
		                                <Declaration >
                                        <DeclarationHeader>
                                           < Jurisdiction > IE </ Jurisdiction >
                                           < CWProcedure > IMPORT </ CWProcedure >
                                           < DeclarationDestination > CUSTOMSWAREIE </ DeclarationDestination >
                                           < DocumentRef > 71Q0019681 </ DocumentRef >
                                           < SiteID > DUB </ SiteID >
                                           < AccountCode > G0779837 </ AccountCode >
                                        </ DeclarationHeader >
                                        </ Declaration >
                                        </ DeclarationList >
                                      </ InputDocument >");

            using (Stream stream = request.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    string soapResult = rd.ReadToEnd();
                    Console.WriteLine(soapResult);
                }
            }
        }

        /// <summary>
        /// Create a soap webrequest to [Url]
        /// </summary>
        /// <returns></returns>
        public HttpWebRequest CreateWebRequest()
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(@"http://dev.nl/Rvl.Demo.TestWcfServiceApplication/SoapWebService.asmx");
            webRequest.Headers.Add(@"SOAP:Action");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }
    }
}
