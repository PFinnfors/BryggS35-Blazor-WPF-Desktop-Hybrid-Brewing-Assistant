using System.Xml.Serialization;

namespace WebviewAppShared.Data.Models
{
    public class Recipe
    {
        
        public string Name { get; set; }
        public string Version { get; set; }
        public string Author { get; set; }
        public string LastUsed { get; set; }
        public string Type { get; set; }
        public string Brewer { get; set; }
        public string BatchSize { get; set; }
        public string BoilSize { get; set; }
        public string BoilTime { get; set; }
        public string ABV { get; set; }
        public string OG { get; set; }
        public string FG { get; set; }
    }
}
