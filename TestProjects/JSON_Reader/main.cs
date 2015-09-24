using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Globalization;
using OpponentInformation;

namespace JSON_Reader
{
    
    class main
    {
        static void Main(string[] args)
        {

            //string json;
            //Hashtable o;
            //bool success = true;

            //json = "{\"name\":123,\"name2\":-456e8}";
            //o = (Hashtable)JSON.JsonDecode(json);
            //success = success && ((double)o["name"] == 123);
            //success = success && ((double)o["name2"] == -456e8);

            if (args.Length != 3)
            {
                Console.WriteLine("Usage: genWorkDay personality_file hours regions > pipeOutput");
                System.Environment.Exit(1);
            }
            // An example of how the OpponentInfo would be *used*
            // You simply create the object, then initialize it with characteristics from a config file.
            // Then you tell it to write out a days work into another file.
            OpponentInfo op = new OpponentInfo();
            if (op.LoadPersonalityFile(args[0]))
            {
                Console.WriteLine("File was loaded");
                op.WriteDaysWork(Convert.ToSingle(args[1]), Convert.ToInt32(args[2]));
            }
            else
            {
                Console.WriteLine("FILE LOAD FAILED");
            }
        }
    }
}
