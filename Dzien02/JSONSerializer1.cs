using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Dzien02
{
    internal class JSONSerializer1
    {
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
                    Console.ReadKey();
                }
            }
        }
    }
}
