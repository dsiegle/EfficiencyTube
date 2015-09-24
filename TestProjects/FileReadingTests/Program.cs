// This program outputs an opponent work day file, using a personality file as input
// Usage: genWorkDay input_personality_file hours regions
//
// Typically you will redirect the output to a file of your choice.
//
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenWorkDay
{
    class Program
    {

        class OpponentInfo
        {
            #region fields
            enum Gender { MALE = 0, FEMALE = 1, OTHER = 2};
            Gender m_gender;

            struct Efficiency
            {
                public float value;
                public float variability;
            }
            Efficiency m_efficiency;

            struct Talking
            {
                float probability;
                int times;
            }
			Talking m_talking;
			
            int m_regions;
            #endregion fields

            static string StripComment(String s)
            {
                int idx = s.IndexOf("//");
                return (idx != -1) ? s.Remove(idx) : s;
            }
            public void WriteDaysWork(float lengthOfDayInHours, int numRegionsInDay)
            {
                GenerateHeader(Gender.FEMALE);
            }

            bool Tokenize(String s, String[] token)
            {
                return false;
            }

            bool ReadEfficiencyBlock(StreamReader sr)
            {
                int numTokens = 0;
                bool done = false;

                String s;
                while ((s = sr.ReadLine()) != null)
                {
                    String[] token;
                    bool Tokenize(s,token);
                    s = StripComment(s);
                    String[] w = s.Split(':');
                    if (w.Length != 2)      // Make sure we have 2 items (both key/value)
                    {
                        // Yikes! bad file format
                        // Output error and return false
                        // TODO: How to output stderr versus stdout?
                        Console.WriteLine("Malformed input line: {0}", s);
                        return false;
                    }
                    switch(w[0]) {
                        case "value":
                            m_efficiency.value = Convert.ToSingle(w[1]);
                            numTokens++;
                            break;
                        case "variability":
                            m_efficiency.variability = Convert.ToSingle(w[1]);
                            numTokens++;
                            break;
                        case "endefficiency":
                            done = true;    // Exit the loop
                            numTokens++;
                            break;
                        default:
                            // Error?  Didn't get a keyword
                            break;
                    }
                    if (done) break;    // Leave the while loop
                }
                // Check to see if we got all 3 tokens value/variability/endefficiency
                if (numTokens < 3)
                {
                    // Didn't get enough tokens.
                }
            }

            public bool LoadPersonalityFile(String fname)
            {
                Console.WriteLine("LoadPersonalityFile(): fname = {0}", fname);
                return true;

            }
            String Generate(string attribute, string[] first, string[] middle, string[] last)
            {
                Random rand = new Random();
                int i1 = rand.Next(first.Length);
                int i2 = rand.Next(middle.Length);
                int i3 = rand.Next(last.Length);
                String s = attribute + " " + first[i1] + middle[i2] + last[i3];
                return s;
            }
            void GenerateHeader(Gender gen)
            {
                String tmp = (gen == Gender.MALE) ? GenerateMaleFirstName() : GenerateFemaleFirstName();
                String[] fname = {""};
                fname[0] = tmp;     // Convert the first name to an array of length 1 to match Generate argument requirements.
                String[] mname = { "A. ", "B. ", "C. ", "D. " };
                String[] lname = { "Ashton", "Jones", "Brady", "Berkowitz", "Smith", "Clark", "Bradley", "Feinbaum" };
                string s = Generate("name:", fname, mname, lname);
                Console.WriteLine(s);

                String[] fcomp = { "Pear ", "Globo ", "Integrated ", "Foobar " };
                String[] mcomp = {"Compu ", "Educational ", "Partners "};
                String[] lcomp = { "Tech", "Corp", "Inc.", "Global", "Research", "Limited" };
                s = Generate("company:", fcomp, mcomp, lcomp);
                Console.WriteLine(s);

                String[] fpos = { "Chief ", "Lead ", "Senior ", "Junior ", "Intern " };
                String[] mpos = { "", "", "", "", "", "" };
                String[] lpos = { "Bottle Washer", "Programmer", "Manager", "Director", "Secretary", "Coder" };
                s = Generate("position:", fpos, mpos, lpos);
                Console.WriteLine(s);
            }
            string GenerateFemaleFirstName()
            {
                String[] fname = { "Alice ", "Tammy ", "Susie ", "April " };
                Random rand = new Random();
                int i = rand.Next(fname.Length);
                return fname[i];
            }
            string GenerateMaleFirstName()
            {
                String[] fname = { "Bob ", "Bill ", "Alfred ", "Stanley " };
                Random rand = new Random();
                int i = rand.Next(fname.Length);
                return fname[i];
            }
        }

        //-----------------------------------------------------------------------------------
        static void Main(string[] args)
        {
            if (args.Length != 3) {
                Console.WriteLine("Usage: genWorkDay personality_file hours regions > pipeOutput");
                System.Environment.Exit(1);
            }
            // An example of how the OpponentInfo would be *used*
            // You simply create the object, then initialize it with characteristics from a config file.
            // Then you tell it to write out a days work into another file.
            OpponentInfo op = new OpponentInfo();
            if (op.LoadPersonalityFile(args[0])) {
                op.WriteDaysWork(Convert.ToSingle(args[1]), Convert.ToInt32(args[2]));
            }
        }
    }
}
