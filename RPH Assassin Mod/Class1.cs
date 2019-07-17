/*using IniParser;
using IniParser.Model;*/
using Rage;
using Rage.Native;
using System;
using System.Drawing;

[assembly: Rage.Attributes.Plugin("RAGE Assassin Mod", Description = "Assassin mod for RagePluginHook", Author = "theuhdirector")]

namespace RPH_Assassin_Mod
{
    public static class EntryPoint
    {
        static void Main()
        {
            Game.LogTrivial("Plugin started");
            Game.DisplayNotification("RAGE Assassin Mod started. Getting a job for you now...");
            //Generate Job
            //Get position for ped
            Random rand = new Random();
            Vector3 targetPos = World.GetRandomPositionOnStreet();
            string targetStreetName = World.GetStreetName(targetPos);
            //Setup blip
            Color red = Color.FromName("Red");
            Ped target = new Ped(targetPos);
            Blip targetBlip = new Blip(target); 
            targetBlip.Color = red;
            //Misc vars
            //Vehicle targetVehicle; UNUSED
            bool targetKilled = false;
            int wantedLevel = rand.Next(2,5);
            int cashGiven = rand.Next(10000, 75000);
            //Calculate wanted level using magic and demons
            GameFiber.Yield();
            Game.LogTrivial("Job active");
            //Display job info
            Game.DisplayNotification($"Job found for you. Target is on {targetStreetName}. Kill them using whatever means possible and then escape the police.");
            targetBlip.EnableRoute(red);
            GameFiber.Yield();
            Game.LogTrivialDebug("Starting death check");
            //WHILE LOOPS DONT WORK
            DeathCheck:
            if (target.IsDead) 
               {
                targetKilled = true;
                Game.DisplayNotification($"Target down! Good job! Now escape the police. You have been given {wantedLevel.ToString()} stars.");
                targetBlip.Delete();
                Game.LocalPlayer.WantedLevel = wantedLevel;
                WantedCheck:
                if (Game.LocalPlayer.WantedLevel == 0)
                {
                    Game.DisplayNotification($"You have escaped from a {wantedLevel} star wanted level and been given ${cashGiven}!");
                    SetCashForPlayer(cashGiven);
                    Game.LogTrivial("Unloading");
                }
                else if (Game.LocalPlayer.Character.IsAlive == false)
                {
                    Game.DisplayNotification($"You have unsuccessfully escaped from a {wantedLevel} star wanted level, so you have only been given ${cashGiven / 4}.");
                    SetCashForPlayer(cashGiven / 4);
                    Game.LogTrivial("Unloading");
                }
                else
                {
                    GameFiber.Sleep(100);
                    goto WantedCheck;
                }

               }
            else
               {
                GameFiber.Sleep(100);
                goto DeathCheck;
               }

                    }
        public static void SetCashForPlayer(int amount)
        {
            //cash may or may not work with this
            NativeFunction.Natives.STAT_GET_INT(Game.GetHashKey($"sp0_total_cash"), out int value, -1);
            NativeFunction.Natives.STAT_SET_INT(Game.GetHashKey($"sp0_total_cash"), value + amount, true);

        }

    }
               

        }

    
