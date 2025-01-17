﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Nocturnal.Apis.qm;
using VRC;
using System.Diagnostics;
using System.IO;
using VRC.SDKBase;
using Nocturnal.Settings.wrappers;
using Nocturnal.Settings;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using UnityEngine;

namespace Nocturnal.Ui.qm
{
    internal class Main
    {
        internal static GameObject _mainpage = null;
        internal static GameObject _jumpimpulse = null;
        internal static bool _stopev7 = false;

        [DllImport("user32.dll")] //Set the active window

        public static extern IntPtr SetActiveWindow(IntPtr hWnd);



     
        internal static void Createmenu()
        {
            new Page("Nocturnal Menu", Settings.Download_Files.imagehandler.logo);
            _mainpage = submenu.Create("Nocturnal", null,true);
            var Main = submenu.Create("Main", _mainpage);
            _mainpage.SetActive(true);
            Anticrash.runanti(); 
            Toggles.Runantoggles();
            Ui.runui(); 
            Target.tarGetmenu();
            Pickups.pickups();
            World._World();
            Onuser._Onuser();
            Worldhistory.createrhistory();
            Tags.Tagsmenu(); 
            Chat._Chat();
            Discord.start();
            Mic.start();

            new NButton(extensions.Getmenu(_mainpage), "Close", () => Process.GetCurrentProcess().Kill(), true, null, 3, 6);
            new NButton(extensions.Getmenu(_mainpage), "Restart", () =>
            {
                string arguments = "";
                foreach (string stringi in Environment.GetCommandLineArgs())
                {
                    arguments += $"{stringi} ";
                }
                System.Diagnostics.Process vrc = new System.Diagnostics.Process();
                vrc.StartInfo.FileName = $"{Directory.GetCurrentDirectory()}\\VRChat.exe";
                vrc.StartInfo.Arguments = arguments;
                vrc.Start();
                Process.GetCurrentProcess().Kill();

            }, true, null, 3, 7);

            new NButton(extensions.Getmenu(_mainpage), "Change avi", () =>
            {
                try
                {
                    string aviid = "";
                    XRefedMethods.PopOutInput("Avatar id", value => aviid = value, () => {
                        Exploits.Misc.Changetoavi(aviid);
                    });
                }
                catch
                {
                    NocturnalC.Log("Cloud not change into that avatar");
                }
            }, true, null, 2, 6);

            new NButton(extensions.Getmenu(_mainpage), "Join By Id", () =>
            {
                string roomid = "";
                XRefedMethods.PopOutInput("Room Instance Id", value => roomid = value, () => {
                    if (!Networking.GoToRoom(roomid))
                    {
                        string[] array = roomid.Split(':');
                        new PortalInternal().Method_Private_Void_String_String_PDM_0(array[0], array[1]);
                    }
                });

            }, true, null, 2, 7);


            new NButton(extensions.Getmenu(_mainpage), "Delete P", () => Exploits.Misc.Deletportals(), true, null, 1, 6);

            new NButton(out _jumpimpulse, extensions.Getmenu(_mainpage), "Jump Imp", () =>
            {
                try
                {
                    XRefedMethods.PopOutNumbersKeyboard("Jump Impulse", value => ConfigVars.jumpimpulse = value, () => { });
                    Networking.LocalPlayer.SetJumpImpulse(ConfigVars.jumpimpulse);
                    _jumpimpulse.GetComponent<TMPro.TextMeshProUGUI>().text = $"[{Networking.LocalPlayer.GetJumpImpulse()}] Jump imp";
                }
                catch { }

            }, true, null, 1, 7);


            new NButton(extensions.Getmenu(_mainpage), "Save Config", () => {
                ConfigVars.saveconfig($"{Directory.GetCurrentDirectory()}\\Nocturnal V3\\Config\\Config.json"); NocturnalC.Log("Saved Config", "Settings", ConsoleColor.Green);
            }, false, Download_Files.imagehandler.Saveconfig, 1, 0);

            new NButton(extensions.Getmenu(_mainpage), "Enter Key", () =>
            {
                imports.SetForegroundWindow(imports.GetConsoleWindow());

                NocturnalC.Log("Enter Your Key", "Verification", ConsoleColor.Green);
                string getkey = Console.ReadLine();
                NocturnalC.Log("Enter A Username u want", "Verification", ConsoleColor.Red);
                string getusername = Console.ReadLine();

                var sendinfo = new jsonmanager.custommsg2()
                {
                    code = "3",

                    msg = getkey.Trim(),

                    msg2 = getusername.Trim(),
                };
                server.setup.sendmessage(Newtonsoft.Json.JsonConvert.SerializeObject(sendinfo));

                File.WriteAllText(Directory.GetCurrentDirectory() + "\\Nocturnal V3\\Config\\LogInfo.erp", Newtonsoft.Json.JsonConvert.SerializeObject(sendinfo));

                var newmsg = new jsonmanager.custommsg2()
                {
                    code = "4",

                    msg = JsonConvert.DeserializeObject<jsonmanager.custommsg2>(File.ReadAllText(Directory.GetCurrentDirectory() + "\\Nocturnal V3\\Config\\LogInfo.erp")).msg.Trim(),

                    msg2 = JsonConvert.DeserializeObject<jsonmanager.custommsg2>(File.ReadAllText(Directory.GetCurrentDirectory() + "\\Nocturnal V3\\Config\\LogInfo.erp")).msg2.Trim(),

                };

                server.setup.sendmessage(JsonConvert.SerializeObject(newmsg));

            }, false, Download_Files.imagehandler.EnterKey, 2, 0);

            new NToggle("Fly", extensions.Getmenu(_mainpage), () => 
            Inject_monos._FlyManager.gameObject.SetActive(!Inject_monos._FlyManager.activeSelf)
            , () =>
            Inject_monos._FlyManager.gameObject.SetActive(!Inject_monos._FlyManager.activeSelf),
            false, true, 0, 6);

            new NToggle("Esp", extensions.Getmenu(_mainpage), () =>
            {
                try
                {
                    ConfigVars.esp = true;
                    var player = PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0;
                    for (int i = 0; i < player.Count; i++)
                    {
                        if (player[i].field_Private_APIUser_0.id != VRC.Core.APIUser.CurrentUser.id)
                            player[i].transform.Find("SelectRegion/ESP").gameObject.SetActive(true);
                    }
                }
                catch { }
            
            }, () =>
            {
                try
                {
                    var player = PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0;
                    for (int i = 0; i < player.Count; i++)
                        if (player[i].field_Private_APIUser_0.id != VRC.Core.APIUser.CurrentUser.id)
                            player[i].transform.Find("SelectRegion/ESP").gameObject.SetActive(false);
                }
                catch { }
                ConfigVars.esp = false;
          
                
            }, ConfigVars.esp, true, 0, 7);
            new Submenubutton(_mainpage.Getmenu(), "Main", Main, Download_Files.imagehandler.Main, false, 0, 0);

            new Apis.Slider(extensions.Getmenu(Main), value => ConfigVars.Flyspeed = value, ConfigVars.Flyspeed, () =>
            {

            }, true, "Fly Speed");

            new NToggle("Mirror", extensions.Getmenu(Main), () => Exploits.Mirror.Togglemirror(true), () => Exploits.Mirror.Togglemirror(false));

            new NToggle("Optimized Mirror", extensions.Getmenu(Main), () => Exploits.Mirror.Togglemirror(true, true), () => Exploits.Mirror.Togglemirror(false));

            new NToggle("Ghost Mode", extensions.Getmenu(Main), () => _stopev7 = true, () => _stopev7 = false, _stopev7);

            new NToggle("Fake Lag", extensions.Getmenu(Main), () => Hooks.fakelag = true, () => Hooks.fakelag = false, Hooks.fakelag);

            new NButton(Main.Getmenu(), "Item Boom ball", () => Exploits.Setiteminhand.create<Nocturnal.Monobehaviours.Boomorbit>());

            new NButton(Main.Getmenu(), "Teleport ball", () => Exploits.Setiteminhand.create<Nocturnal.Monobehaviours.Teleportobj>());

            new NButton(Main.Getmenu(), "Udon Spam", () => Exploits.Udon.Spamudon());
            //                Settings.wrappers.extensions._AddTrailRender(VRC.Player.prop_Player_0.gameObject);

            new NToggle("Self Trail", extensions.Getmenu(Main), () => {
                ConfigVars.SelfTrail = true;
                Settings.wrappers.extensions._AddTrailRender(VRC.Player.prop_Player_0.gameObject);
            }, () => {
                ConfigVars.SelfTrail = false;
                try
                {
                    Component.DestroyImmediate(VRC.Player.prop_Player_0.gameObject.GetComponent<TrailRenderer>());
                } catch { }
            }, ConfigVars.SelfTrail);

            new NToggle("Everyone Trail", extensions.Getmenu(Main), () => {
                ConfigVars.EveryoneTrail = true;
                try
                {
                    var player = PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0;
                    for (int i = 0; i < player.Count; i++)
                    {
                        if (player[i].field_Private_APIUser_0.id != VRC.Core.APIUser.CurrentUser.id)
                            extensions._AddTrailRender(player[i].gameObject);
                    }
                }
                catch { }
                Settings.wrappers.extensions._AddTrailRender(VRC.Player.prop_Player_0.gameObject);
            }, () => {
                ConfigVars.EveryoneTrail = false;
                try
                {
                    var player = PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0;
                    for (int i = 0; i < player.Count; i++)
                    {
                        if (player[i].field_Private_APIUser_0.id != VRC.Core.APIUser.CurrentUser.id)
                            Component.DestroyImmediate(player[i].gameObject.GetComponent<TrailRenderer>());
                    }
                }
                catch { }
            }, ConfigVars.EveryoneTrail);


            new Minibuttn(Objects._QMexpand.transform.parent.gameObject, "Copy instance id to clipboard", () =>
            {
                var worldid = RoomManager.prop_String_0;
                NocturnalC.Log(worldid);
                System.Windows.Forms.Clipboard.SetText(worldid);
            }, Download_Files.imagehandler.clipboard);

            new Minibuttn(Objects._QMexpand.transform.parent.gameObject, "Previous Track (Spotify)", () =>
             {
                 var p = Process.GetProcessesByName("Spotify").FirstOrDefault();

                 try
                 {
                     Thread.Sleep(100);
                     imports.SetForegroundWindow(p.MainWindowHandle);
                     Thread.Sleep(100);
                     imports.keybd_event(0x11, 0, 0, 0);
                     imports.keybd_event(0x25, 0, 0, 0);
                     imports.keybd_event(0x11, 0, 2, 0);
                     imports.keybd_event(0x25, 0, 2, 0);
                 }
                 catch { }
                 Thread.Sleep(100);
                 imports.SetForegroundWindow(Main2._hwnd);
             }, Download_Files.imagehandler.prev);


            new Minibuttn(Objects._QMexpand.transform.parent.gameObject, "Play Pause (Spotify)", () =>
            {
                var p = Process.GetProcessesByName("Spotify").FirstOrDefault();

                try
                {
                    Thread.Sleep(100);

                    imports.SetForegroundWindow(p.MainWindowHandle);
                    Thread.Sleep(100);

                    imports.keybd_event(0x20, 0, 0, 0);
                    imports.keybd_event(0x20, 0, 2, 0);
                }
                catch { }

                Thread.Sleep(100);


                imports.SetForegroundWindow(Main2._hwnd);
            }, Download_Files.imagehandler.stopplay);


            new Minibuttn(Objects._QMexpand.transform.parent.gameObject, "Next Track (Spotify)", () =>
            {
                var p = Process.GetProcessesByName("Spotify").FirstOrDefault();
                try
                {
                    Thread.Sleep(100);
                    imports.SetForegroundWindow(p.MainWindowHandle);
                    Thread.Sleep(100);
                    imports.keybd_event(0x11, 0, 0, 0);
                    imports.keybd_event(0x27, 0, 0, 0);
                    imports.keybd_event(0x11, 0, 2, 0);
                    imports.keybd_event(0x27, 0, 2, 0);
                }
                catch { }
                Thread.Sleep(100);
                imports.SetForegroundWindow(Main2._hwnd);
            }, Download_Files.imagehandler.next);


            new Minibuttn(Objects._QMexpand.transform.parent.gameObject, "Mute (Discord)", () =>
            {
                var p = Process.GetProcessesByName("Discord").FirstOrDefault();
                try
                {
                    Thread.Sleep(100);
                    imports.SetForegroundWindow(p.MainWindowHandle);
                    Thread.Sleep(100);
                    imports.keybd_event(0xA2, 0, 0, 0);
                    imports.keybd_event(0xA0, 0, 0, 0);
                    imports.keybd_event(0x4D, 0, 0, 0);
                    imports.keybd_event(0xA2, 0, 0x0002, 0);
                    imports.keybd_event(0xA0, 0, 0x0002, 0);
                    imports.keybd_event(0x4D, 0, 0x0002, 0);
                }
                catch (Exception ex) { NocturnalC.Log(ex); }
                Thread.Sleep(100);
                imports.SetForegroundWindow(Main2._hwnd);
            }, Download_Files.imagehandler.mute);

            new Minibuttn(Objects._QMexpand.transform.parent.gameObject, "Defean (Discord)", () =>
            {
                var p = Process.GetProcessesByName("Discord").FirstOrDefault();
                try
                {
                    Thread.Sleep(100);
                    imports.SetForegroundWindow(p.MainWindowHandle);
                    Thread.Sleep(100);
                    imports.keybd_event(0xA2, 0, 0, 0);
                    imports.keybd_event(0xA0, 0, 0, 0);
                    imports.keybd_event(0x44, 0, 0, 0);
                    imports.keybd_event(0xA2, 0, 2, 0);
                    imports.keybd_event(0xA0, 0, 2, 0);
                    imports.keybd_event(0x44, 0, 2, 0);
                }
                catch (Exception ex) { NocturnalC.Log(ex); }
                Thread.Sleep(100);
                imports.SetForegroundWindow(Main2._hwnd);
            }, Download_Files.imagehandler.defean);

            var menu = UnityEngine.GameObject.Find("/UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window");

            GameObject tpbutton;
            new SmallButton(out tpbutton,menu.gameObject, () => Exploits.Setiteminhand.create<Nocturnal.Monobehaviours.Teleportobj>(), Download_Files.imagehandler.teleport);
            tpbutton.transform.localPosition = new UnityEngine.Vector3(-714.5056f, -562.5029f, -1.0042f);

            GameObject boombtn;
            new SmallButton(out boombtn, menu.gameObject, () => Exploits.Setiteminhand.create<Nocturnal.Monobehaviours.Boomorbit>(), Download_Files.imagehandler.items);
            boombtn.transform.localPosition = new UnityEngine.Vector3(-825, -562.5029f, -1.0042f);

            GameObject Mirror;
            new SmallButton(out Mirror, menu.gameObject, () =>
            {
                var mirrors = GameObject.FindObjectsOfType<VRC.SDK3.Components.VRCMirrorReflection>();
                for (int i = 0; i < mirrors.Length; i++)
                    if (mirrors[i].name == "NocturnalMirror")
                    {
                        GameObject.DestroyImmediate(mirrors[i].gameObject);
                        return;
                    }
                Exploits.Mirror.Togglemirror(true);

            }, Download_Files.imagehandler.Mirror);
            Mirror.transform.localPosition = new UnityEngine.Vector3(-714.5056f, -672.5029f, -1.0042f);


            GameObject Mirroroptimized;
            new SmallButton(out Mirroroptimized, menu.gameObject, () =>
            {
                var mirrors = GameObject.FindObjectsOfType<VRC.SDK3.Components.VRCMirrorReflection>();
                for (int i = 0; i < mirrors.Length; i++)
                    if (mirrors[i].name == "NocturnalMirror")
                    {
                        GameObject.DestroyImmediate(mirrors[i].gameObject);
                        return;
                    }
                Exploits.Mirror.Togglemirror(true, true);

            }, Download_Files.imagehandler.OptimizedMirror);
            Mirroroptimized.transform.localPosition = new UnityEngine.Vector3(-825, -672.5029f, -1.0042f);




         

            Objects._QMexpand.transform.SetSiblingIndex(6);

        }


    }
}
