using System;
using System.Collections.Generic;
using System.IO;

namespace Conference_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Directory.GetCurrentDirectory();
            var pathInput = $"{path}\\Input.txt";

            FileStreamer fs = new FileStreamer();

            List<string> conferenceTalkList = fs.FileReader(pathInput);
        }
    }
}
