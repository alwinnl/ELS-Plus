﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELS.configuration;
using ELS.NUI;
using CitizenFX.Core;

namespace ELS.Light
{
    internal class Stage
    {
        internal configuration.Lights PRML;
        internal configuration.Lights SECL;
        internal configuration.Lights WRNL;
        private int vehicleId;

        internal Stage(configuration.Lights prml, configuration.Lights secl, configuration.Lights wrnl, int veh)
        {
            PRML = prml;
            SECL = secl;
            WRNL = wrnl;
            CurrentStage = 0;
            vehicleId = veh;
        }

        internal int CurrentStage { get; private set; }
        

        internal async Task NextStage()
        {
#if DEBUG
            CitizenFX.Core.Debug.WriteLine($"Current Light Stage is {CurrentStage} next stage is {CurrentStage + 1}");
#endif
            if (CurrentStage.Equals(3))
            {
#if DEBUG
                CitizenFX.Core.Debug.WriteLine($"Light stage is 3 Disabling light stages");
#endif
                SetStage(0);
                return;
            }
            SetStage(CurrentStage + 1);
        }

        internal void SetStage(int stage)
        {
            CurrentStage = stage;
            if (Game.PlayerPed.IsInPoliceVehicle && vehicleId == Game.PlayerPed.CurrentVehicle.GetNetworkId())
            {
                ElsUiPanel.ToggleStages(CurrentStage);
            }
        }

        internal int[] GetStage2Extras()
        {
            int[] arr = { 1, 4};
            if (!String.IsNullOrEmpty(PRML.ExtrasActiveAtLstg2))
            {
                switch(PRML.ExtrasActiveAtLstg2)
                {
                    case "1and4":
                        arr[0] = 1;
                        arr[1] = 4;
                        break;
                    case "2and3":
                        arr[0] = 2;
                        arr[1] = 3;
                        break;
                    case "1and3":
                        arr[0] = 1;
                        arr[1] = 3;
                        break;
                    case "2and4":
                        arr[0] = 2;
                        arr[1] = 4;
                        break;
                    case "1and2":
                        arr[0] = 1;
                        arr[1] = 2;
                        break;
                    case "3and4":
                        arr[0] = 3;
                        arr[1] = 4;
                        break;
                }
                
            }
            return arr;
        }
    }
}
