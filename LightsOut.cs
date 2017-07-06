//
//Made for VAPENATION @ YPWP Servers
//Console 2017
//steam: tehvual
//
///////////////////////////////////////

/**
Turns all Lights out at night then allows user to turn them back on again.
This is usfull to save on CPU but was designed for servers that run fast gathering (1000x), it allows users to see
who is home at night becuase normally the lights will run 24/7 due to the high resource rate.
**/

using System;
using Oxide.Core;
using System.Collections.Generic;
using Oxide.Core.Libraries.Covalence;

namespace Oxide.Plugins
{
    [Info("LightsOut", "Console (tehvual, vual)", "1.0.0")]
    [Description("Turn off all lights at night, user must manually turn on again, this allows users on modded server with lots of wood to find houses at night with players in them. Without this mod the fires burn 24/7. Also saves on CPU")]

    class LightsOut : RustPlugin
    {
        TOD_Sky sky;
        //public List<string> lightsList;
        public List<string> lightsList = new List<string>();
        bool MessageDone;

        void Loaded()
        {
            Puts("Loaded: LightsOut by Console (tehvual, vual)");
            sky  = TOD_Sky.Instance;
        }

        void Unload()
        {
            Puts("Unloaded: LightsOut by Console (tehvual, vual)");
        }

        protected override void LoadDefaultConfig()
        {
            Puts("Default configuration created");
        }

        void OnConsumeFuel(BaseOven oven, Item fuel, ItemModBurnable burnable)
    	{
          if(!MessageDone)
          {
            Puts("Fire Burning.... LIGHTS OUT !! time is " + sky.Cycle.DateTime + "|" + oven.ToString());
            PrintToChat("Night time, Fires Buring.... LIGHTS OUT !! time is " + sky.Cycle.DateTime + " Dont forget to /vote ^_*");
            lightsList.Add("MessageDone");
            MessageDone = true;
          }
          //DEBUG LINE //PrintToChat(" Is Night: " + sky.IsNight.ToString() + "|" + oven.ToString() + "| RUST Time: " + sky.Cycle.DateTime.ToString("HH:mm:ss")); //DEBUG
          //if (sky.Cycle.Hour >= 18 && sky.Cycle.Hour <= 6) // <-- another way to do it with more targeted times ?
          if(sky.IsNight)
            {
              if(lightsList != null)
              {
                 if(lightsList.Contains(oven.ToString()) == false)
                 {
                   oven.StopCooking();
                   oven.SetFlag(BaseEntity.Flags.On, false);
                   lightsList.Add(oven.ToString());
                 }
              }
            }else{
                if(lightsList != null)
                {
                  if(lightsList.Count > 0)
                  {
                      lightsList.Clear();
                  }
                }
              }
        }
    }
}
