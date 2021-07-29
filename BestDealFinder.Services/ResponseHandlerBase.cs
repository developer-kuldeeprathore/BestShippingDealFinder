using Newtonsoft.Json;
using System;
using System.IO;
using System.Xml.Serialization;

namespace BestDealFinder.Services
{
    public abstract class ResponseHandlerBase
    {
        public T ParseFrom<T>(Models.ResponseType responseType, string response) where T : class
        {
            if (responseType == Models.ResponseType.Json)
            {
                return JsonConvert.DeserializeObject<T>(response);
            }
            else if (responseType == Models.ResponseType.Xml)
            {
                return FromXml<T>(response);
            }

            throw new InvalidOperationException("Unable to typecast please check response data");
        }

        public T FromXml<T>(string xmlResult) where T : class
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            using (StringReader stringReader = new StringReader(xmlResult))
            {
                return (T)xmlSerializer.Deserialize(stringReader);
            }
        }
    }
}