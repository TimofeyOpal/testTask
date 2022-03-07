using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace task1
{
    class Import
    {
        public Import()
        {    
        }


        public List<MemberParty> RemoveDublicate(IEnumerable<MemberParty> memberParties)
        {
            return memberParties.GroupBy(x => new { x.FirstName, x.LastName }, (key, group) => new
            {
                Key1 = key.FirstName,
                Key2 = key.LastName,
                Result = group.ToList().OrderBy(c => c.DateBirthday).First()
            }).Select(x => x.Result).OrderBy(x=>x.DateBirthday).ToList();
        }
        public List<MemberParty> GetData(string pathToDerictory)
        {
            List<MemberParty> memberParties = new();
            var files = Directory.GetFiles(pathToDerictory);
            foreach (string path in files)
            {
                if (path.EndsWith(".csv"))
                {
                    string[] lines = System.IO.File.ReadAllLines(path);
                    var members = lines.Select(x => x.Split(',')).Select(x => new MemberParty()
                    {
                        FirstName = x[0],
                        LastName = x[1],
                        DateBirthday = DateTime.Parse(x[2]),
                        Supplier = "Cервис 1"
                    });
                    memberParties.AddRange(members);
                }
                else if (path.EndsWith(".json"))
                {
                    var jsonString = System.IO.File.ReadAllText(path);
                    var members = JsonSerializer.Deserialize<List<MemberParty>>(jsonString);
                    members.ForEach(i => i.Supplier = "Ceрвис 2");
                    memberParties.AddRange(members);
                }
                else if (path.EndsWith(".xml"))
                {
                    var members = XElement.Load(path).Descendants("Participant").Select(x => new MemberParty()
                    {
                        FirstName = x.Element("Name").Value,
                        LastName = x.Element("Surname").Value,
                        DateBirthday = DateTime.Parse(x.Element("RegisterDate").Value),
                        Supplier = "Cервис 3"
                    });
                    memberParties.AddRange(members);
                }
               
            }
            return memberParties;
        }

    }
}
