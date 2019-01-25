using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace scoutingProject.Classes_For_Function
{
    class runadbcommand
    {
        public string runADBCommand (string command_torun)
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = @"C:\Users\elias\Downloads\platform-tools-latest-windows\platform-tools\adb.exe";
            p.StartInfo.Arguments = command_torun;
            p.Start();
            return p.StandardOutput.ReadToEnd();
            p.WaitForExit();
        }
    }
}
