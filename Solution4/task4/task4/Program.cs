using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace task4
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ConnectApi connectApi = new("ключ");
            await connectApi.OutPutInfo();
        }

        

        



    }
}
