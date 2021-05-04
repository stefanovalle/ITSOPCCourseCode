using ITSOPCCourseCode.Snap7.Example.Services;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ITSOPCCourseCode.Snap7.Example
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var plcCommunication = new PlcCommunication("127.0.0.1");

            var stringToWrite = "Test test";
            var stringConverted = Encoding.ASCII.GetBytes(stringToWrite);

            await plcCommunication.WriteAsync(stringConverted, 1, 0, (uint)stringToWrite.Length);

            var readed = await plcCommunication.ReadAsync(1, 0, (uint)stringToWrite.Length);
            
            var stringParsed = Encoding.ASCII.GetString(readed);

            Console.WriteLine(stringParsed);

        }
    }
}
