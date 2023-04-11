using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;

namespace WebviewAppShared.Data.Components
{
    public class TableData
    {
        public TableData(object listObject)
        {
            Entries = new List<OrderedDictionary>();
            DisplayKeys = new List<string>();
            
            //OBS! TableData förväntar sig alltid en lista.
            var list = ((IEnumerable)listObject).Cast<object>().ToList();

            foreach (object anyClass in list)
            {
                // Samlar alla fält från anyClass i en OrderedDictionary.
                var properties = anyClass.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                var oDict = new OrderedDictionary();
                
                foreach (var p in properties)
                {
                    oDict.Add(p.Name, p.GetValue(anyClass));
                }

                Entries.Add(oDict);

                // Lägger till högst 4 av de första fälten så att de visas i Table.
                if (list.IndexOf(anyClass) == 0) //Vi gör bara detta första gången.
                for (int i = 0; (i < (oDict.Count - 1) && (i < 4)); i++)
                {
                        DisplayKeys.Add(oDict.Keys.Cast<object>().ElementAt(i).ToString());
                }
            }
        }
        public TableData()
        {
        }

        public List<OrderedDictionary> Entries { get; set; }
        public List<string> DisplayKeys { get; set; }
    }
}
