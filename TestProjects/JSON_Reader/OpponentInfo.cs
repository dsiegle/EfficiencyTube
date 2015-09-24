using System;
using System.Collections;
using System.IO;
using Procurios.Public;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace OpponentInformation
{
    class OpponentInfo
    {
        #region fields
        // What if this was just the HashTable returned by the JSON parse?
        Hashtable m_info;

        enum Gender { MALE = 0, FEMALE = 1, OTHER = 2 };
        //Gender m_gender;

        //struct Efficiency
        //{
        //    public float value;
        //    public float variability;
        //}
        //Efficiency m_efficiency;

        //struct Talking
        //{
        //    float probability;
        //    int times;
        //}
        //Talking m_talking;

        //int m_regions;
        #endregion fields

        //=============================================================================================
        public bool LoadPersonalityFile(String fname)
        {
            try
            {
                using (StreamReader sr = new StreamReader(fname))
                {
                    string json = sr.ReadToEnd();
                    Hashtable o;
                    bool success = false;

                    m_info = (Hashtable)JSON.JsonDecode(json);
                    if (m_info != null) return true;
                    //success = success && ((string)m_info["gender"] == "male");
                    // success = success && ((double)m_info["efficiency_value"] == 70.0);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("LoadPersonalityFile() failed. {0}", e.ToString());
            }
            return false;
        }
        
        //=============================================================================================
        public void WriteDaysWork(float lengthOfDayInHours, int numRegionsInDay)
        {
            Console.WriteLine("lengthOfDayInHours={0}, numRegionsInDay={1}", lengthOfDayInHours, numRegionsInDay);
            GenerateHeader((string)m_info["gender"]);
        }

        //=============================================================================================
        String Generate(string attribute, string[] first, string[] middle, string[] last)
        {
            Random rand = new Random();
            int i1 = rand.Next(first.Length);
            int i2 = rand.Next(middle.Length);
            int i3 = rand.Next(last.Length);
            String s = attribute + " " + first[i1] + middle[i2] + last[i3];
            return s;
        }
        
        //=============================================================================================
        void GenerateHeader(string gen)
        {

            String tmp = (gen == "male") ? GenerateMaleFirstName() : GenerateFemaleFirstName();
            String[] fname = { "" };
            fname[0] = tmp;     // Convert the first name to an array of length 1 to match Generate argument requirements.
            String[] mname = { "A. ", "B. ", "C. ", "D. " };
            String[] lname = { "Ashton", "Jones", "Brady", "Berkowitz", "Smith", "Clark", "Bradley", "Feinbaum" };
            string s = Generate("name:", fname, mname, lname);
            Console.WriteLine(s);

            String[] fcomp = { "Pear ", "Globo ", "Integrated ", "Foobar " };
            String[] mcomp = { "Compu ", "Educational ", "Partners " };
            String[] lcomp = { "Tech", "Corp", "Inc.", "Global", "Research", "Limited" };
            s = Generate("company:", fcomp, mcomp, lcomp);
            Console.WriteLine(s);

            String[] fpos = { "Chief ", "Lead ", "Senior ", "Junior ", "Intern " };
            String[] mpos = { "", "", "", "", "", "" };
            String[] lpos = { "Bottle Washer", "Programmer", "Manager", "Director", "Secretary", "Coder" };
            s = Generate("position:", fpos, mpos, lpos);
            Console.WriteLine(s);
        }

        //=============================================================================================
        string GenerateFemaleFirstName()
        {
            String[] fname = { "Alice ", "Tammy ", "Susie ", "April " };
            Random rand = new Random();
            int i = rand.Next(fname.Length);
            return fname[i];
        }
        
        //=============================================================================================
        string GenerateMaleFirstName()
        {
            String[] fname = { "Bob ", "Bill ", "Alfred ", "Stanley " };
            Random rand = new Random();
            int i = rand.Next(fname.Length);
            return fname[i];
        }
    }
}

