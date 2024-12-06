using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using WindowsGSM.Functions;
using WindowsGSM.GameServer.Engine;
using WindowsGSM.GameServer.Query;
using System.Collections.Generic;

namespace WindowsGSM.Plugins
{
    public class SBox : SteamCMDAgent
    {
        // - Plugin Details
        public Plugin Plugin = new Plugin
        {
            name = "WindowsGSM.SBox", // WindowsGSM.XXXX
            author = "Prefix (Edit from:ohmcodes)",
            description = "WindowsGSM plugin for supporting S&Box Dedicated Server",
            version = "1.0.0",
            url = "", // Github repository link (Best practice)
            color = "#FFA500" // Color Hex
        };

        // - Standard Constructor and properties
        public SBox(ServerConfig serverData) : base(serverData) => base.serverData = _serverData = serverData;
        private readonly ServerConfig _serverData;
        public string Error, Notice;

        // - Settings properties for SteamCMD installer
        public override bool loginAnonymous => true;
        public override string AppId => "1892930 -beta staging"; /* taken via https://steamdb.info/app/1892930/info/ */

        // - Game server Fixed variables
        public override string StartPath => @"sbox-server.exe"; // Game server start path
        public string FullName = "S&Box Dedicated Server"; // Game server FullName
        public object QueryMethod = new A2S(); // Query method should be use on current server type. Accepted value: null or new A2S() or new FIVEM() or new UT3()
        //+game facepunch.walker garry.scenemap +hostname My Dedicated Server
        // - Game server default values
        public string ServerName = "S&Box dedicated server";
        public string Defaultmap = "facepunch.walker garry.scenemap"; // Original (MapName)
        public bool AllowsEmbedConsole = true;  // Does this server support output redirect?
        public int PortIncrements = 2; // This tells WindowsGSM how many ports should skip after installation
        public string Maxplayers = "32"; // WGSM reads this as string but originally it is number or int (MaxPlayers)
        public string Port = "27015"; // WGSM reads this as string but originally it is number or int
        public string QueryPort = "27016"; // WGSM reads this as string but originally it is number or int (SteamQueryPort)
        public string Additional = "+net_shared_query_port False +net_use_fake_ip False +net_ping 1";


        private Dictionary<string, string> configData = new Dictionary<string, string>();


        // - Create a default cfg for the game server after installation
        public async void CreateServerCFG()
        {
            // no config yet
            //+servercfgfile server.cfg
        }

        // - Start server function, return its Process to WindowsGSM
        public async Task<Process> Start()
        {
            string shipExePath = Functions.ServerPath.GetServersServerFiles(_serverData.ServerID, StartPath);
            if (!File.Exists(shipExePath))
            {
                Error = $"{Path.GetFileName(shipExePath)} not found ({shipExePath})";
                return null;
            }

            // Start building parameters
            StringBuilder paramBuilder = new StringBuilder();

            // Add hostname if it's not empty
            if (!string.IsNullOrEmpty(_serverData.ServerName))
            {
                paramBuilder.Append($" +hostname \"{_serverData.ServerName}\"");
            }

            // Add map if it's not empty
            if (!string.IsNullOrEmpty(_serverData.ServerMap))
            {
                paramBuilder.Append($" +game {_serverData.ServerMap}");
            }

            // Add GSLT if it's not empty
            if (!string.IsNullOrEmpty(_serverData.ServerGSLT))
            {
                paramBuilder.Append($" +net_game_server_token {_serverData.ServerGSLT}");
            }

            // Add additional parameters if any
            if (!string.IsNullOrEmpty(_serverData.ServerParam))
            {
                paramBuilder.Append($" {_serverData.ServerParam}");
            }
            

            // Prepare Process
            var p = new Process
            {
                StartInfo =
                {
                    WorkingDirectory = ServerPath.GetServersServerFiles(_serverData.ServerID),
                    FileName = shipExePath,
                    Arguments = paramBuilder.ToString(),
                    WindowStyle = ProcessWindowStyle.Normal,
                    UseShellExecute = false
                },
                EnableRaisingEvents = true
            };

            // Start Process
            try
            {
                p.Start();

                return p;
            }
            catch (Exception e)
            {
                Error = e.Message;
                return null; // return null if fail to start
            }
        }
		
        // - Update server function
        public async Task<Process> Update(bool validate = false, string custom = null)
        {
            //custom = " -beta staging ";
            validate = true;
            var (p, error) = await Installer.SteamCMD.UpdateEx(_serverData.ServerID, AppId, validate, custom: custom, loginAnonymous: loginAnonymous);
            Error = error;
            await Task.Run(() => { p.WaitForExit(); });
            return p;
        }

        public bool IsInstallValid()
        {
            return File.Exists(Functions.ServerPath.GetServersServerFiles(_serverData.ServerID, StartPath));
        }
        // - Stop server function
        public async Task Stop(Process p)
        {
            await Task.Run(() =>
            {
                Functions.ServerConsole.SetMainWindow(p.MainWindowHandle);
                Functions.ServerConsole.SendWaitToMainWindow("quit{ENTER}");
                Task.Delay(5000);
            });
            await Task.Delay(2000);
        }

        public bool IsImportValid(string path)
        {
            string exePath = Path.Combine(path, "serverfiles/sbox-server.exe");
            Error = $"Invalid Path! Fail to find {Path.GetFileName(exePath)}";
            return File.Exists(exePath);
        }

        public string GetLocalBuild()
        {
            var steamCMD = new Installer.SteamCMD();
            return steamCMD.GetLocalBuild(_serverData.ServerID, AppId);
        }

        public async Task<string> GetRemoteBuild()
        {
            var steamCMD = new Installer.SteamCMD();
            return await steamCMD.GetRemoteBuild(AppId);
        }
    }
}
