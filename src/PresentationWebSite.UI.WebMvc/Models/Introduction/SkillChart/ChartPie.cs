using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace PresentationWebSite.UI.WebMvc.Models.Introduction.SkillChart
{
    [DataContract]
    public class ChartPie
    {
        [DataMember(Name = "chart")]
        public CharPieInfo Infos { get; set; }
        [DataMember(Name = "category")]
        public IEnumerable<ChartPieSlice> Slices { get; set; }

        public ChartPie()
        {
            Infos = new CharPieInfo();
        }

        public string ToJson()
        {
            using (var stream = new MemoryStream())
            {
                var ser = new DataContractJsonSerializer(typeof(ChartPie));
                ser.WriteObject(stream, this);
                stream.Position = 0;
                return new StreamReader(stream).ReadToEnd();
            }
        }
    }
}