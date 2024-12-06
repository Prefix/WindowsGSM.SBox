# WindowsGSM.SBox
🧩WindowsGSM plugin that provides S&Box Dedicated server

## PLEASE ⭐STAR⭐ THE REPO IF YOU LIKE IT! THANKS!

### WindowsGSM Installation: 
1. Download  WindowsGSM https://windowsgsm.com/ 
2. Create a Folder at a Location you wan't all Server to be Installed and Run.
4. Drag WindowsGSM.Exe into previously created folder and execute it.

### Plugin Installation:
1. Download [latest](https://github.com/Prefix/WindowsGSM.SBox/releases/latest) release
2. Extract then Move **SBox.cs** folder to **plugins** folder
3. OR Press on the Puzzle Icon in the left bottom side and install this plugin by navigating to it and select the Zip File.
4. Click **[RELOAD PLUGINS]** button or restart WindowsGSM
5. Navigate "Servers" and Click "Install Game Server" and find "S&Box Dedicated Server [SBox.cs]

### Official Documentation
🗃️ [https://docs.facepunch.com/s/sbox-dev/doc/dedicated-servers-WGeGAD9U8d](https://docs.facepunch.com/s/sbox-dev/doc/dedicated-servers-WGeGAD9U8d)

### The Game
🕹️ https://store.steampowered.com/app/590830/sbox/
https://sbox.game/give-me-that - Login to web first with Steam

### Dedicated server info
🖥️ https://steamdb.info/app/1892930/info/

### Port Forwarding (Not must if you don't use net_ commands to expose IP/port)
- 27015 UDP
- 27016 TCP

### Connecting on a locked server (with password)
Connect via steamid/ip/or lobby in-game 
- connect STEAMID (from status command) get token for persistent steamid
- connect IP:Gameport
- via game lobby 

### Server not launching?
- Try Install Required Redist insde `_CommonRedist` folder
- Try Install DotNet

### Other notes
- The game is currently in Early Access Stage WGSM and this plugin is not taking liability if something happens to your server, the app is only for managing your server easily
- To add GLST to have persistent steamid for your server https://steamcommunity.com/dev/managegameservers

### Support
[Official Game Discord](https://discord.gg/sbox)

[WGSM](https://discord.com/channels/590590698907107340/645730252672335893)

### Give Love!
Thanks https://github.com/ohmcodes for plugin template

### GitHub Actions Workflow
This repository uses GitHub Actions to automatically zip the `SBox.cs` folder and push it to the release on every commit to the main branch. The workflow file is located at `.github/workflows/release.yml`.