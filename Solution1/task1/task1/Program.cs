using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Xml.Linq;

namespace task1
{
    class Program
    {
        static int tableWidth = 93;
        static void Main(string[] args)
        {

            IPrint print = new Print();
            FullWork fullWork = new(print);



        
        }

        

        
    }
    
        public static class ListExtensions
        {
            public static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize)
            {
                return source
                    .Select((x, i) => new { Index = i, Value = x })
                    .GroupBy(x => x.Index / chunkSize)
                    .Select(x => x.Select(v => v.Value).ToList())
                    .ToList();
            }
        }
    
}
