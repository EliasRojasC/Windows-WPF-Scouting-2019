using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace scoutingProject.Classes_For_Function
{
    class ScheduleFunction
    {
        public void runxml(String RSSURL)
        {
            string ALLTHESHIT = @RSSURL;
            IDictionary<int, string> dict = new Dictionary<int, string>();
            XmlDocument doc1 = new XmlDocument();
            doc1.Load(ALLTHESHIT);
            XmlElement root = doc1.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("//item");
            string[] h1 = new string[] { "<h1>" };
            string[] li = new string[] { "<li>" };
            string dta;
            int dictline = 0;

            foreach (XmlNode node in nodes)
            {
                string tempf = node["title"].InnerText;
                string tempc = node["description"].InnerText;
                string[] blueRedSplit = tempc.Split(h1, StringSplitOptions.RemoveEmptyEntries);
                List<String> entry = new List<string>(blueRedSplit);
                entry.RemoveAt(0);
                string redInfo = entry.ElementAt(0);
                string blueInfo = entry.ElementAt(1);
                List<String> blueNumbers = new List<string>(blueInfo.Split(li, StringSplitOptions.RemoveEmptyEntries));
                List<String> redNumbers = new List<string>(redInfo.Split(li, StringSplitOptions.RemoveEmptyEntries));
                blueNumbers.RemoveAt(0);
                redNumbers.RemoveAt(0);
                List<String> blueTeamData = new List<String>();
                List<String> redTeamData = new List<String>();
                string teamStringBlue = "";
                string teamStringRed = "";

                foreach (string blueData in blueNumbers)
                {
                    if (blueData.Substring(2, 1) == "")
                    {
                        dta = blueData.Substring(0, 2);
                    }
                    else
                    {
                        if (blueData.Substring(3, 1) == "")
                        {
                            dta = blueData.Substring(0, 3);
                        }
                        else
                        {
                            dta = blueData.Substring(0, 4);
                        }
                    }
                    blueTeamData.Add(dta);
                    dta = "";
                }
                foreach (string redData in redNumbers)
                {
                    if (redData.Substring(2, 1) == "")
                    {
                        dta = redData.Substring(0, 2);
                    }
                    else
                    {
                        if (redData.Substring(3, 1) == "")
                        {
                            dta = redData.Substring(0, 3);
                        }
                        else
                        {
                            dta = redData.Substring(0, 4);
                        }
                    }
                    redTeamData.Add(dta);
                    dta = "";
                }

                foreach (string teamNum in blueTeamData)
                {
                    teamStringBlue = teamStringBlue + teamNum + ",";
                }

                foreach (string teamNum in redTeamData)
                {
                    teamStringRed = teamStringRed + teamNum + ",";
                }

                string UpdatedStringBlue = teamStringBlue.Substring(0, teamStringBlue.Length - 1);
                string UpdatedStringRed = teamStringRed.Substring(0, teamStringRed.Length - 1);

                dict.Add(dictline, "Blue:," + UpdatedStringBlue + "," + "Red:," + UpdatedStringRed);

                dictline = dictline + 1;
            }
        }
    }
}
