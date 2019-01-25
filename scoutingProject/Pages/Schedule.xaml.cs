using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Xml;

namespace scoutingProject.Pages
{
    /// <summary>
    /// Interaction logic for Schedule.xaml
    /// </summary>
    public partial class Schedule : Page
    {
        Classes_For_Function.runadbcommand runadbcommand = new Classes_For_Function.runadbcommand();

        public Schedule()
        {

            InitializeComponent();
        }

        private void SendScheduleClick(object sender, RoutedEventArgs e)
        {
            runadbcommand.runADBCommand("shell mkdir -m 777 sdcard/scouting");
            var currentuser = System.Environment.GetEnvironmentVariable("USERPROFILE");
            var outgoingschedulepath = currentuser + "\\SkoutResources\\outGoingFiles";
            if (ScheduleText.Text.Length > 0)
            {
                if (ScheduleText.Text.Contains("/feed"))
                {
                    IDictionary<int, string> dict = new Dictionary<int, string>();
                    XmlDocument doc1 = new XmlDocument();
                    doc1.Load(ScheduleText.Text);
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

                    string[] comma = new string[] { "," };

                    string device1 = "";
                    string device2 = "";
                    string device3 = "";
                    string device4 = "";
                    string device5 = "";
                    string device6 = "";

                    foreach (KeyValuePair<int, string> entry in dict)
                    {
                        string game = entry.Value;
                        string[] justGameNumber = game.Split(comma, StringSplitOptions.RemoveEmptyEntries);
                        List<string> justNums = new List<string>(justGameNumber);
                        justNums.RemoveAt(0);
                        justNums.RemoveAt(3);
                        device1 = device1 + justNums.ElementAt(0) + ",";
                        device2 = device2 + justNums.ElementAt(1) + ",";
                        device3 = device3 + justNums.ElementAt(2) + ",";
                        device4 = device4 + justNums.ElementAt(3) + ",";
                        device5 = device5 + justNums.ElementAt(4) + ",";
                        device6 = device6 + justNums.ElementAt(5) + ",";
                    }

                    int step = 1;

                    string devicefinal1 = device1.Substring(0, device1.Length - 1);
                    string devicefinal2 = device2.Substring(0, device2.Length - 1);
                    string devicefinal3 = device3.Substring(0, device3.Length - 1);
                    string devicefinal4 = device4.Substring(0, device4.Length - 1);
                    string devicefinal5 = device5.Substring(0, device5.Length - 1);
                    string devicefinal6 = device6.Substring(0, device6.Length - 1);
                    //moves files to backup Archive
                    if (Directory.EnumerateFileSystemEntries(currentuser + @"\SkoutResources\outGoingFiles").Any())
                    {
                        String directoryName = currentuser + "\\SkoutResources\\scheduleArchive";
                        DirectoryInfo dirInfo = new DirectoryInfo(directoryName);
                        if (dirInfo.Exists == false)
                            Directory.CreateDirectory(directoryName);

                        List<String> MySchedulesFiles = Directory
                                           .GetFiles(currentuser + @"\SkoutResources\outGoingFiles", "*.*", SearchOption.AllDirectories).ToList();

                        foreach (string file in MySchedulesFiles)
                        {
                            FileInfo mFile = new FileInfo(file);
                            // to remove name collisions
                            if (new FileInfo(dirInfo + "\\" + mFile.Name).Exists == false)
                            {
                                mFile.MoveTo(dirInfo + "\\" + mFile.Name);
                            }
                        }


                    }

                    System.IO.File.WriteAllText(outgoingschedulepath + "//Device1.csv", devicefinal1);
                    System.IO.File.WriteAllText(outgoingschedulepath + "//Device2.csv", devicefinal2);
                    System.IO.File.WriteAllText(outgoingschedulepath + "//Device3.csv", devicefinal3);
                    System.IO.File.WriteAllText(outgoingschedulepath + "//Device4.csv", devicefinal4);
                    System.IO.File.WriteAllText(outgoingschedulepath + "//Device5.csv", devicefinal5);
                    System.IO.File.WriteAllText(outgoingschedulepath + "//Device6.csv", devicefinal6);

                    string output = runadbcommand.runADBCommand("devices");
                    List<String> listOfDeviceNumbers = new List<string>();
                    if (output.Length > 28)
                    {
                        if (!output.Contains("auth"))
                        {
                            int z = output.IndexOf("\n");
                            var y = output.Substring(z + 1);
                            //Initial Split
                            string[] lines = y.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                            //Extra Split
                            try
                            {
                                foreach (string m in lines)
                                {
                                    listOfDeviceNumbers.Add((m.Replace("\tdevice", "")).Trim());
                                }
                            }
                            catch (Exception) { }
                        }
                    }

                    
                    
                    foreach (string devicenum in listOfDeviceNumbers)
                    {
                        if(runadbcommand.runADBCommand("-s " + devicenum + " ls sdcard/scouting").Contains("Device"))
                        {
                            string outputinterst = runadbcommand.runADBCommand("-s " + devicenum + " ls sdcard/scouting");
                            var foo = outputinterst.IndexOf("Device");
                            var foooer = outputinterst.Substring(foo);
                            foooer = foooer.Substring(0, 11);
                            runadbcommand.runADBCommand("shell -s " + devicenum + " rm -f sdcard/scouting/" + foooer);
                        } 
                    }

                    foreach (string deviceNum in listOfDeviceNumbers)
                    {
                        runadbcommand.runADBCommand("-s " + deviceNum + " push " + outgoingschedulepath + "\\Device" + step + ".csv" + " sdcard/scouting");
                        if (step >= 7)
                        {
                            step = 1;
                            break;
                        }
                        else
                        {
                            step = step + 1;
                        }
                    }
                }         
                else
                {
                    MessageBox.Show("The URL you have entered is not RSS please see intructions on how to get RSS", "Error");
                }

            }
            else
            {
                MessageBox.Show("Please enter a Blue Aliance RSS url", "Error");
            }
            
        }
    }
}
