using PowerfulFactionBases;
using Verse;

namespace GenStepSettings;

[StaticConstructorOnStartup]
public class SettingsImplementer
{
    static SettingsImplementer()
    {
        var genStepSettlement = (GenStep_Settlement)DefDatabase<GenStepDef>.GetNamed("Settlement").genStep;
        genStepSettlement.count = LoadedModManager.GetMod<GenStepSettings>().Settings.Count;
        GenStep_Settlement.MenuSettlementSize =
            LoadedModManager.GetMod<GenStepSettings>().Settings.RefMenuSettlementSize;
        GenStep_Settlement.DefaultPawnsPoints =
            LoadedModManager.GetMod<GenStepSettings>().Settings.RefDefaultPawnsPoints;
        GenStep_Settlement.VanillaMortarCount =
            LoadedModManager.GetMod<GenStepSettings>().Settings.RefVanillaMortarCount;
        SymbolResolver_EdgeDefense.UseVanillaTurret =
            LoadedModManager.GetMod<GenStepSettings>().Settings.RefUseVanillaTurret;
    }
}