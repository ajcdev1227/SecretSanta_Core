using Microsoft.Extensions.Options;
using SecretSanta_Core.Models;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace SecretSanta_Core.Services
{
    public class USPSAddressService
    {
        private readonly AppSettings _appSettings;

        public USPSAddressService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        
        public AddressModel VerifyAddress(AddressModel model)
        {
            var uspsUser = _appSettings.USPSUser;
            var xml = new StringBuilder();
            xml.Append($"<AddressValidateRequest USERID=\"{uspsUser}\">");
            xml.Append("<Revision>1</Revision>");
            xml.Append("<Address ID=\"0\">");
            xml.Append("<Address1></Address1>");
            xml.Append($"<Address2>{model.Address}</Address2>");
            xml.Append($"<City>{model.City}</City>");
            xml.Append($"<State>CA</State>");
            xml.Append($"<Zip5>{model.Zip}</Zip5>");
            xml.Append("<Zip4></Zip4>");
            xml.Append("</Address>");
            xml.Append("</AddressValidateRequest>");
            var path = $"https://secure.shippingapis.com/ShippingApi.dll?API=Verify&XML={xml.ToString()}";
            return GetAddress(path);
        }

        AddressModel GetAddress(string path)
        {
#pragma warning disable SYSLIB0014
            var client = new WebClient();
            var response = client.DownloadString(path);
#pragma warning restore SYSLIB0014
            return ExtractAddress(response);
        }

        AddressModel ExtractAddress(string xml)
        {

            var model = new AddressModel();
            var doc = XDocument.Parse(xml);
            bool error = doc.Descendants("Error").Any();
            if (!error)
            {
                var address = doc.Descendants("Address2");
                model.Address = address.Nodes().OfType<XText>().First().Value;
                var city = doc.Descendants("City");
                model.City = city.Nodes().OfType<XText>().First().Value;
                var zip = doc.Descendants("Zip5");
                model.Zip = zip.Nodes().OfType<XText>().First().Value;
            }


            return model;

        }
    }
}
