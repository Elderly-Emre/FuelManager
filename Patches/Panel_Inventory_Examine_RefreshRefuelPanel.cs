﻿namespace FuelManager.Patches
{
    using System;
    using System.Reflection;
    using Il2Cpp;
    using Il2CppTLD.Gear;
    using MelonLoader;
    using HarmonyLib;
    using ModSettings;
    using ModComponent;
    using UnityEngine;
    [HarmonyPatch(typeof(Panel_Inventory_Examine), nameof(Panel_Inventory_Examine.RefreshRefuelPanel))]
    internal class Panel_Inventory_Examine_RefreshRefuelPanel
    {
        private static bool Prefix(Panel_Inventory_Examine __instance)
        {
            if (!FuelUtils.IsFuelItem(__instance.m_GearItem)) return true;

            __instance.m_RefuelPanel.SetActive(false);
            __instance.m_Button_Refuel.gameObject.SetActive(true);

            float currentLiters = FuelUtils.GetIndividualCurrentLiters(__instance.m_GearItem);
            float capacityLiters = FuelUtils.GetIndividualCapacityLiters(__instance.m_GearItem);
            float totalCurrent = FuelUtils.GetTotalCurrentLiters(__instance.m_GearItem);
            float totalCapacity = FuelUtils.GetTotalCapacityLiters(__instance.m_GearItem);

            bool fuelIsAvailable = totalCurrent > FuelUtils.MIN_LITERS;
            bool canRefuel = fuelIsAvailable && !Mathf.Approximately(currentLiters, capacityLiters);

            __instance.m_Refuel_X.gameObject.SetActive(!canRefuel);
            __instance.m_Button_Refuel.gameObject.GetComponent<Panel_Inventory_Examine_MenuItem>().SetDisabled(!canRefuel);

            __instance.m_MouseRefuelButton.SetActive(canRefuel);
            __instance.m_RequiresFuelMessage.SetActive(false);

            __instance.m_LanternFuelAmountLabel.text =
                FuelUtils.GetLiquidQuantityStringNoOunces(currentLiters) +
                "/" +
                FuelUtils.GetLiquidQuantityStringNoOunces(capacityLiters);

            __instance.m_FuelSupplyAmountLabel.text =
                FuelUtils.GetLiquidQuantityStringNoOunces(totalCurrent) +
                "/" +
                FuelUtils.GetLiquidQuantityStringNoOunces(totalCapacity);

            __instance.UpdateWeightAndConditionLabels();

            return false;
        }
    }
}
