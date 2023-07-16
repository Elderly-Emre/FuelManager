﻿
namespace FuelManager
{
    [HarmonyPatch(typeof(GearItem), nameof(GearItem.Deserialize))]
    internal static class GearItem_Deserialize_LampFuel
    {
        private static void Postfix(GearItem __instance)
        {
            if (ItemUtils.NormalizeName(__instance.name) == "GEAR_LampFuel")
            {
                FuelItemAPI.AddRepair(__instance, true);
                FuelItemAPI.AddHarvest(__instance);
            }
        }
    }
}
