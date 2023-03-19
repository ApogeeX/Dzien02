using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dzien02
{
    internal class JSONSerializer1
    {
        
        class Rates
        {
            [JsonProperty("currency")]
            public string CurrencyName { get; set; }
            
            [JsonProperty("code")]
            public string CurrencyCode { get; set; }

            [JsonProperty("mid")]
            public double AverageRate { get; set; }
        }

        public static void NBP()
        {
            WebClient wb = new WebClient();
            string s = wb.DownloadString("https://api.nbp.pl/api/exchangerates/tables/A/?format=json");
            JArray ja = JArray.Parse(s);
            IList<JToken> results = ja[0]["rates"].Children().ToList();

            List<Rates> rates = new List<Rates>();
            foreach (JToken token in results) 
            { 
                Rates rate = token.ToObject<Rates>();
                rates.Add(rate);
            }
        }
        class MyUser
        {
            public string FName { get; set; }
            public string LName { get; set; }
        }
        public static void ApplyJson()
        {
            string s = "{ 'FName' : 'Jan', 'LName' : 'Kowlaski' }";
            MyUser user = JsonConvert.DeserializeObject<MyUser>(s, new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
            });
            Console.WriteLine($"{user.FName}, {user.LName}");
        }

        public static void Create()
        {
            EmployeeJSON emp1 = new EmployeeJSON()
            {
                Id = 123, FirstName = "Jan", LastName = "Kowalski", 
                IsManager = false,
                StartAt = new DateTime(2022, 1, 1)
            };
            EmployeeJSON emp2 = new EmployeeJSON()
            {
                Id = 123,
                FirstName = "Jan",
                LastName = "Kowalski",
                IsManager = false,
                StartAt = new DateTime(2022, 1, 1)
            };
            EmployeeJSON emp3 = new EmployeeJSON()
            {
                Id = 123,
                FirstName = "Jan",
                LastName = "Kowalski",
                IsManager = false,
                StartAt = new DateTime(2022, 1, 1)
            };

            //emp.SetToken(Guid.NewGuid().ToString());

            EmployeeJSON[] empArray = new EmployeeJSON[]
            {
                emp1, emp2, emp3
            };

            // serializacja
            using (FileStream fs = new FileStream("json1.json", FileMode.Create))
            {
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(EmployeeJSON[]));
                js.WriteObject(fs, empArray);
            }

            //deserializacja
            using (FileStream fs = new FileStream("json1.json", FileMode.Open))
            {
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(EmployeeJSON[]));
                EmployeeJSON[] empJson = js.ReadObject(fs) as EmployeeJSON[];

                if (empJson != null)
                {
                    Console.WriteLine(empJson.Length);
                }
            }

            JavaScriptSerializer js2 = new JavaScriptSerializer();
            string s = js2.Serialize(empArray);
            File.WriteAllText("json2.json", s);

            EmployeeJSON[] emps2 = js2.Deserialize<EmployeeJSON[]>(s);
            Console.WriteLine(emps2.Length);


            // Serializacja za pomocą Newtonsoft.JSON

            s = JsonConvert.SerializeObject(empArray, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            File.WriteAllText("json3.json", s);

            emps2 = JsonConvert.DeserializeObject<EmployeeJSON[]>(s);

            Console.ReadKey();
        }
    }
}
