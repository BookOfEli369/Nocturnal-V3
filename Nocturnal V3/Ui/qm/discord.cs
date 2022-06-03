﻿
using Nocturnal.Apis.qm;
using Nocturnal.Settings.wrappers;
using System.IO;
using Newtonsoft.Json;
namespace Nocturnal.Ui.qm
{
    internal class Discord
    {
        internal static TMPro.TextMeshProUGUI chattext = null;
        internal static void start()
        {
            var disc = submenu.Create("Discord", Main.mainpage);
            Main.mainpage.getmenu().Create("Discord", disc, Settings.Download_Files.Discord, true, 2, 4);

            Toggle.Create("Discord Presence", disc.getmenu(), () =>
            {
                Settings.Download_Files.runrpc.Invoke(Settings.Download_Files.runrpc, null);

                var str = JsonConvert.DeserializeObject<Settings.jsonmanager.discordrpc>(File.ReadAllText(Directory.GetCurrentDirectory() + "\\Nocturnal V3\\Config\\Discord.rpc"));
                str.ison = true;
                File.WriteAllText(Directory.GetCurrentDirectory() + "\\Nocturnal V3\\Config\\Discord.rpc", JsonConvert.SerializeObject(str));
                Settings.Download_Files.activitymanager.Invoke(Settings.Download_Files.activitymanager, new object[] { false });
                Settings.ConfigVars.discordrichpresence = true;


            }, () =>
            {
                try
                {
                    var str = JsonConvert.DeserializeObject<Settings.jsonmanager.discordrpc>(File.ReadAllText(Directory.GetCurrentDirectory() + "\\Nocturnal V3\\Config\\Discord.rpc"));
                    str.ison = false;
                    File.WriteAllText(Directory.GetCurrentDirectory() + "\\Nocturnal V3\\Config\\Discord.rpc", JsonConvert.SerializeObject(str));
                    Settings.Download_Files.activitymanager.Invoke(Settings.Download_Files.activitymanager, new object[] { true });
                    Settings.ConfigVars.discordrichpresence = false;

                }
                catch { }


            }, Settings.ConfigVars.discordrichpresence);
            Settings.Download_Files.activitymanager.Invoke(Settings.Download_Files.activitymanager, new object[] { !Settings.ConfigVars.discordrichpresence });


            Buttons.Create(disc.getmenu(), "Details", () =>
            {
                   var str = JsonConvert.DeserializeObject<Settings.jsonmanager.discordrpc>(File.ReadAllText(Directory.GetCurrentDirectory() + "\\Nocturnal V3\\Config\\Discord.rpc"));
                var tobstring = "";
                Apis.inputpopout.run("Details", value => tobstring = value, () => {
                str.Details = tobstring;
                File.WriteAllText(Directory.GetCurrentDirectory() + "\\Nocturnal V3\\Config\\Discord.rpc",JsonConvert.SerializeObject(str));
                    Settings.Download_Files.activitymanager.Invoke(Settings.Download_Files.activitymanager, new object[] {false});

                });
            });

            Buttons.Create(disc.getmenu(), "State", () =>
            {
                var str = JsonConvert.DeserializeObject<Settings.jsonmanager.discordrpc>(File.ReadAllText(Directory.GetCurrentDirectory() + "\\Nocturnal V3\\Config\\Discord.rpc"));
                var tobstring = "";
                Apis.inputpopout.run("State", value => tobstring = value, () => {
                    str.State = tobstring;
                    File.WriteAllText(Directory.GetCurrentDirectory() + "\\Nocturnal V3\\Config\\Discord.rpc", JsonConvert.SerializeObject(str));
                    Settings.Download_Files.activitymanager.Invoke(Settings.Download_Files.activitymanager, new object[] { false });

                });
            });
            Buttons.Create(disc.getmenu(), "Image", () =>
            {
                var str = JsonConvert.DeserializeObject<Settings.jsonmanager.discordrpc>(File.ReadAllText(Directory.GetCurrentDirectory() + "\\Nocturnal V3\\Config\\Discord.rpc"));
                var tobstring = "";
                Apis.inputpopout.run("Image", value => tobstring = value, () => {
                    str.LargeImage = tobstring;
                    File.WriteAllText(Directory.GetCurrentDirectory() + "\\Nocturnal V3\\Config\\Discord.rpc", JsonConvert.SerializeObject(str));
                    Settings.Download_Files.activitymanager.Invoke(Settings.Download_Files.activitymanager, new object[] { false });

                });
            });

            
        }

    }
}
