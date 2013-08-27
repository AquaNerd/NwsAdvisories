using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapControl;

namespace NWS {
    public class CapMessage {
        public string Id { get; set; }
        public string MsgType { get; set; }
        public string Category { get; set; }
        public string Urgency { get; set; }
        public string Severity { get; set; }
        public string Certainty { get; set; }

        public string Generator { get; set; }
        public DateTime UpdatedTimestamp { get; set; }
        public string AuthorName { get; set; }
        public string Title { get; set; }
        public Uri AlertLink { get; set; }
        public string Summary { get; set; }
        public string Event { get; set; }
        public DateTime EffectiveTimestamp { get; set; }
        public DateTime ExpiresTimestamp { get; set; }
        public string[] Areas { get; set; }
        public Dictionary<string, string> Geocodes { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public LocationCollection Locations { get; set; }
        public DateTime PublishedTimestamp { get; set; }
    }
}