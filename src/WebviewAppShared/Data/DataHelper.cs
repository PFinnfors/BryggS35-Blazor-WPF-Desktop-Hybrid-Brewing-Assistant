using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using WebviewAppShared.Data.Models;

namespace WebviewAppShared.Data
{
    public class DataHelper
    {
        public static string DataPath { get; } = "WebviewAppShared.Data.Models";

        public static List<object> GetData(string folderName, string dataName)
        {
            Type dataType = Type.GetType(dataName);

            var dataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName);
            if (!Directory.Exists(dataFolder))
                Directory.CreateDirectory(dataFolder);

            var dataFiles = Directory.GetFiles(dataFolder, "*.xml");
            var dataObjects = new List<object>();

            foreach (string dataFile in dataFiles)
            {
                var xDoc = XDocument.Load(dataFile);

                
                var serializer = new XmlSerializer(dataType);
                var reader = xDoc.CreateReader();
                var temp = serializer.Deserialize(reader);
                var res = Convert.ChangeType(temp, dataType);




                dynamic classObject = Map(xDoc);
                dataObjects.Add(classObject);

                //var recipe = new Recipe
                //{
                //    Name = xDoc.Root.Element("Name").Value,
                //    Description = xDoc.Root.Element("Description").Value,
                //    Ingredients = xDoc.Root.Element("Ingredients").Value,
                //    Directions = xDoc.Root.Element("Directions").Value
                //};
                //recipes.Add(recipe);
            }

            return dataObjects;
        }

        public static dynamic Map(XDocument xDoc)
        {
            dynamic classObject = new ExpandoObject();

            var root = xDoc.Root;
            foreach (var element in root.Elements())
            {
                ((IDictionary<string, object>)classObject)[element.Name.LocalName] = element.Value;
            }

            return classObject;
        }
    }
}
