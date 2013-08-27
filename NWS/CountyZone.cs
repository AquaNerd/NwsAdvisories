using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapControl;

namespace NWS {
    public class CountyZone {
        public enum TimeZoneEnum { 
            AtlanticStandard,
            EasternStandard,
            EasternNonAdv,
            CentralStandard,
            MountainStandard ,
            PacificStandard,
            AlaskaStandard,
            HawaiiAleutianStandard,
            GuamMarianas,
            SamoaStandard
        }

        public enum FeAreaEnum {
            Northern,
            Eastern,
            Central,
            NorthEastern,
            NorthWestern,
            NorthCentral,
            EastCentral,
            Middle,
            BigBend,
            Southern,
            Western,
            PanHandle,
            SouthEastern,
            SouthWestern,
            SouthCentral,
            WestCentral,
            Piedmont,
            Upstate,
            East,
            EastCentralUpper,
            EasternUpper,
            NorthCentralUpper,
            SouthCentralUpper,
            WesternUpper,
            South,
            None
        }

        public string State { get; set; }
        public string Zone { get; set; }
        public string CountyWarningArea { get; set; }
        public string Name { get; set; }
        public string CountyName { get; set; }
        public string FIPS { get; set; }
        public TimeZoneEnum TimeZone { get; set; }
        public FeAreaEnum FeArea { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public LocationCollection Locations { get; set; }
    }
}