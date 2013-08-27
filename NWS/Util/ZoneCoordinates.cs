using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catfood.Shapefile;
using MapControl;

namespace NWS.Util {
    public static class ZoneCoordinates {
        public static List<CountyZone> GetCountyZoneInformation(string shapeFilePath, string countyFilePath) {
            List<CountyZone> results = new List<CountyZone>();

            //using (Shapefile shapeFile = new Shapefile(shapeFilePath)) {
            //    foreach (Shape shape in shapeFile) {
            //        CountyZone zone = new CountyZone();
            //        string[] metaData = shape.GetMetadataNames();
            //        zone.FIPS = shape.GetMetadata("fips");
            //        //zone.FeArea = (CountyZone.FeAreaEnum)Enum.Parse(typeof(CountyZone.FeAreaEnum), shape.GetMetadata("fe_area") == "" ? "None" : shape.GetMetadata("fe_area"));
            //        zone.CountyName = shape.GetMetadata("countyname");
            //        zone.CountyWarningArea = shape.GetMetadata("cws");
            //        zone.Latitude = shape.GetMetadata("lat");
            //        zone.Longitude = shape.GetMetadata("lon");
            //        zone.State = shape.GetMetadata("state");
            //        //zone.TimeZone = (CountyZone.TimeZoneEnum)Enum.Parse(typeof(CountyZone.TimeZoneEnum), shape.GetMetadata("time_zone"));
            //        if (shape.Type == ShapeType.Polygon) {
            //            ShapePolygon polygon = shape as ShapePolygon;
            //            string polyString = "";
            //            foreach (PointD point in ((ShapePolygon)shape).Parts[0]) {
            //                polyString = polyString + point.Y.ToString() + "," + point.X.ToString() + " ";
            //            }
            //            zone.Locations = LocationCollection.Parse(polyString);
            //        }
            //        results.Add(zone);
            //    }
            //}

            StreamReader textReader = new StreamReader(countyFilePath);
            string[] csv = textReader.ReadToEnd().Replace("\r", "").Split('\n');
            textReader.Close();

            foreach (string line in csv) {
                if (!string.IsNullOrWhiteSpace(line)) {
                    string[] cols = line.Split('|');
                    Shapefile shapeFile = new Shapefile(shapeFilePath);
                    Shape shape = shapeFile.Where(i => i.Type == ShapeType.Polygon && i.GetMetadata("state") == cols[0] && i.GetMetadata("countyname") == cols[5]).First();
                    string polyString = "";
                    foreach (PointD point in ((ShapePolygon)shape).Parts[0]) {
                        polyString = polyString + point.Y.ToString() + "," + point.X.ToString() + " ";
                    }
                    results.Add(new CountyZone {
                        CountyName = cols[5],
                        CountyWarningArea = cols[2],
                        FeArea = CountyZone.FeAreaEnum.None,
                        FIPS = cols[6],
                        Latitude = cols[9],
                        Locations = LocationCollection.Parse(polyString),
                        Longitude = cols[10],
                        Name = cols[3],
                        State = cols[0],
                        TimeZone = CountyZone.TimeZoneEnum.AtlanticStandard,
                        Zone = cols[1]
                    });
                }
            }

            return results;
        }
    }
}