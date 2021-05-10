using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace lab6_7.Models
{
    static class ItemSerializer
    {
        public static string Serialize<T>(IEnumerable<T> items, string filename)
        {
            string error = null;
            try
            {
                string result = JsonConvert.SerializeObject(items, Formatting.Indented);
                using (StreamWriter sw = new StreamWriter(filename, false))
                {
                    sw.Write(result);
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return error;
        }

        public static (IEnumerable<T>, string) DeserializeFromFile<T>(string filename)
        {
            string error = null;
            string json;
            List<T> items = null;

            using (StreamReader sr = new StreamReader(filename))
            {
                json = sr.ReadToEnd();
            }

            try
            {
                items = JsonConvert.DeserializeObject<List<T>>(json);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return (items, error);
        }
    }
}
