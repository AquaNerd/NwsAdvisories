using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using MapControl;

namespace NWS.Requests {
    public static class NwsCapRequest {
        private const string BASE_URL = "http://alerts.weather.gov/cap/{0}.php?x={1}";
        public enum LocationType {
            County = 0,
            State = 1,
            Zone = 2,
            Country = 3
        }

        public static List<CapMessage> Request(LocationType locationType, string code) {
            try {
                List<CapMessage> results = new List<CapMessage>();
                string url;
                if (locationType == LocationType.State || locationType == LocationType.Country) {
                    url = string.Format(BASE_URL, code, "0");
                } else {
                    url = string.Format(BASE_URL, "wwaatmget", code + "&y=0");
                }
                XDocument doc = XDocument.Load(url);

                if (doc.Root.Elements().Where(i => i.Name.LocalName == "entry").Elements().Where(i => i.Name.LocalName == "areaDesc").Count() > 0) {
                    results = (from ele in doc.Root.Elements().Where(i => i.Name.LocalName == "entry")
                               select new CapMessage {
                                   AlertLink = new Uri(ele.Elements().First(i => i.Name.LocalName == "link").Attribute("href").Value),
                                   Areas = ele.Elements().First(i => i.Name.LocalName == "areaDesc").Value.Split(';'),
                                   AuthorName = ele.Elements().First(i => i.Name.LocalName == "author").Elements().First(i => i.Name.LocalName == "name").Value,
                                   Category = ele.Elements().First(i => i.Name.LocalName == "category").Value,
                                   Certainty = ele.Elements().First(i => i.Name.LocalName == "certainty").Value,
                                   EffectiveTimestamp = GetDateTime(ele.Elements().First(i => i.Name.LocalName == "effective").Value),
                                   Event = ele.Elements().First(i => i.Name.LocalName == "event").Value,
                                   ExpiresTimestamp = GetDateTime(ele.Elements().First(i => i.Name.LocalName == "expires").Value),
                                   Generator = doc.Root.Elements().First(i => i.Name.LocalName == "generator").Value,
                                   Geocodes = GetValueNameDictionary(ele.Elements().First(i => i.Name.LocalName == "geocode").Descendants()),
                                   Id = ele.Elements().First(i => i.Name.LocalName == "id").Value,
                                   Locations = LocationCollection.Parse(ele.Elements().First(i => i.Name.LocalName == "polygon").Value),
                                   MsgType = ele.Elements().First(i => i.Name.LocalName == "msgType").Value,
                                   Parameters = GetValueNameDictionary(ele.Elements().First(i => i.Name.LocalName == "parameter").Descendants()),
                                   PublishedTimestamp = GetDateTime(ele.Elements().First(i => i.Name.LocalName == "published").Value),
                                   Severity = ele.Elements().First(i => i.Name.LocalName == "severity").Value,
                                   Summary = ele.Elements().First(i => i.Name.LocalName == "summary").Value,
                                   Title = ele.Elements().First(i => i.Name.LocalName == "title").Value,
                                   UpdatedTimestamp = GetDateTime(ele.Elements().First(i => i.Name.LocalName == "updated").Value),
                                   Urgency = ele.Elements().First(i => i.Name.LocalName == "urgency").Value
                               }).ToList();
                }

                return results;
            } catch {
                return null;
            }
        }

        private static DateTime GetDateTime(string input) {
            if (input == null) {
                return DateTime.Now;
            }

            DateTime value;

            if (DateTime.TryParse(input, out value)) {
                return value;
            }

            return DateTime.Now;
        }

        private static Dictionary<string, string> GetValueNameDictionary(IEnumerable<XElement> elements) {
            Dictionary<string, string> results = new Dictionary<string, string>();
            string firstAttribute = "";
            string secondAttribute = "";
            for (int i = 0; i < elements.Count(); i++) {
                if ((i + 1) % 2 == 0) {
                    secondAttribute = elements.ElementAt(i).Value;
                    results.Add(firstAttribute, secondAttribute);
                } else {
                    firstAttribute = elements.ElementAt(i).Value;
                }
            }

            return results;
        }
    }
}
