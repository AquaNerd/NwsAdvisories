using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NWS.Util {
    public static class AlertColors {
        public static Dictionary<string, string> GetColors(string fileNamePath) {
            Dictionary<string, string> colors = new Dictionary<string, string>();

            using (var textReader = new StreamReader(fileNamePath)) {
                string[] csv = textReader.ReadToEnd().Replace("\r", "").Split('\n');

                foreach (string line in csv.Skip(1)) {
                    if (!string.IsNullOrWhiteSpace(line)) {
                        string[] cols = line.Split(',');
                        colors.Add(cols[0].ToString().TrimEnd(' '), cols[3].ToString().Trim());
                    }
                }
            }

            return colors;
        }
    }
}