using HarmonyLib;
using System.Collections.Generic;
using XRL.Core;
using XRL.World;

namespace Kernelmethod.EasierFungalCures.Patches
{
    [HarmonyPatch(typeof(XRL.Core.XRLCore))]
    public class XRLCorePatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(XRLCore.GenerateFungalCure))]
        static void GenerateFungalCurePostfix(XRLCore __instance)
        {
            string originalWorm = __instance.Game.GetStringGameState("FungalCureWorm");

            // Reference: XRL.Core/XRLCore.cs: XRLCore.GenerateFungalCure
            string worm = new List<string>
            {
                "Leech Corpse", "Knollworm Corpse", "SegmentedMirthworm_Corpse",
            }.GetRandomElement();

            MetricsManager.LogInfo($"{Constants.ModName}: changing FungalCureWorm: {originalWorm} -> {worm}");
            __instance.Game.SetStringGameState("FungalCureWorm", worm);
            __instance.Game.SetStringGameState("FungalCureWormDisplay", ConsoleLib.Console.ColorUtility.StripFormatting(GameObjectFactory.Factory.CreateObject(worm).DisplayName));
        }
    }
}