﻿namespace FuelManager
{
    //[HarmonyPatch(typeof(GearItem), nameof(GearItem.Deserialize), new Type[] { typeof(GearItemSaveDataProxy) })]
    [HarmonyPatch(typeof(GearItem), nameof(GearItem.Awake))]
    public static class AddComponents_JerrycanRusty
    {
        // "GEAR_JerrycanRusty"
        public static void Prefix(ref GearItem __instance)
        {
            if (__instance == null) return;
            if (string.IsNullOrWhiteSpace(__instance.name)) return;

            if (CommonUtilities.NormalizeName(__instance.name) == "GEAR_JerrycanRusty")
            {
                Main.Logger.Log($"Patching \'GEAR_JerrycanRusty\'", FlaggedLoggingLevel.Debug);

                //FuelItemAPI.AddRepair(__instance, Constants.REPAIR_HARVEST_GEAR, new int[] { 1 }, Constants.REPAIR_TOOLS, "Play_RepairingMetal");
                FuelItemAPI.AddHarvest(ref __instance, Constants.REPAIR_HARVEST_GEAR, [2], Constants.HARVEST_TOOLS, "Play_HarvestingMetalSaw");
                FuelItemAPI.AddMillable(ref __instance, Constants.REPAIR_HARVEST_GEAR, [1], Constants.REPAIR_HARVEST_GEAR, [2], true, 30, 60, SkillType.ToolRepair);
            }
        }
    }
}
