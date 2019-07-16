/*using IniParser;
using IniParser.Model;*/
using Rage;
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
                Game.DisplayNotification("Target down! Good job!");
                targetBlip.Delete();
                Game.LogTrivial("Unloading");
               }
            else
               {
                GameFiber.Sleep(100);
                goto DeathCheck;
               }
                    }

                }
               

        }

    
