using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    class FullWork
    {
        List<List<MemberParty>> memberParties = default;
        IPrint print = default;
        List<MemberParty> dataWithoutDublicate = default;
        public FullWork(IPrint print)
        {
            Console.WriteLine("Введите путь до дериктории с файлами");
            Import import = new();
            this.print = print;
            var data = import.GetData(Console.ReadLine().Trim());
            this.dataWithoutDublicate = import.RemoveDublicate(data);
            this.memberParties = dataWithoutDublicate.ChunkBy(5);
            Work();
           
        }
        public void Work()
        {
            bool isPrint = true;
           
            print.PrintRow("Имя", "Фамилия", "   Дата регистрации   ", "Поставщик");
            
            foreach (var item in memberParties[0])
            {
                print.PrintRow(item.FirstName, item.LastName, item.DateBirthday.ToString(), item.Supplier);
               
            }
            

            while (isPrint)
            {
                Console.WriteLine("Список комманд : get-page (тут номер страницы) -  search Ива, Для выхода напишите Exit");
                Console.WriteLine($"Количество страниц: {memberParties.Count}");
                Console.Write("Введите комманду : ");
                var info =  Console.ReadLine();
                if (info.StartsWith("get-page"))
                {
                  var arrData =  info.TrimEnd().Split(" ");
                  var pageNumber = arrData[arrData.Length - 1];

                 var a = int.TryParse(pageNumber, out int result);
                    if (a && result > 0 && result <= memberParties.Count)
                    {
                        Console.Clear();
                        
                        print.PrintRow("Имя", "Фамилия", "   Дата регистрации   ", "Поставщик");
                        
                        foreach (var item in memberParties[result-1])
                        {       
                            print.PrintRow(item.FirstName, item.LastName, item.DateBirthday.ToString(), item.Supplier);
                        }         
                    }
                    else Console.WriteLine("Такой страницы не найдено");
                }
                else if (info.StartsWith("search"))
                {
                    var searchDataInLower = info.TrimEnd().Split("search")[1].Trim().ToLower();
                    var data =  this.dataWithoutDublicate.Where(x => x.FirstName.ToLower().Contains(searchDataInLower) || x.LastName.ToLower().Contains(searchDataInLower));
                    Console.Clear();
                    
                    print.PrintRow("Имя", "Фамилия", "   Дата регистрации   ", "Поставщик");
                    
                    foreach (var item in data)
                    {   
                        print.PrintRow(item.FirstName, item.LastName, item.DateBirthday.ToString(), item.Supplier);            
                    }
                }
                else if (info.Trim() == "Exit")   
                    isPrint = false;
                

            }


            


           
            

        }
    }
}
