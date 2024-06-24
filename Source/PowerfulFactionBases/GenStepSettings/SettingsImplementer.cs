using PowerfulFactionBases;
using Verse;

namespace GenStepSettings;

[StaticConstructorOnStartup]
public class SettingsImplementer
{
    static SettingsImplementer()
    {
        var genStep_Settlement = (GenStep_Settlement)DefDatabase<GenStepDef>.GetNamed("Settlement").genStep;
        genStep_Settlement.count = LoadedModManager.GetMod<GenStepSettings>().settings.count;
        GenStep_Settlement.menuSettlementSize =
            LoadedModManager.GetMod<GenStepSettings>().settings.refMenuSettlementSize;
        GenStep_Settlement.defaultPawnsPoints =
            LoadedModManager.GetMod<GenStepSettings>().settings.refDefaultPawnsPoints;
        GenStep_Settlement.vanillaMortarCount =
            LoadedModManager.GetMod<GenStepSettings>().settings.refVanillaMortarCount;
        SymbolResolver_EdgeDefense.useVanillaTurret =
            LoadedModManager.GetMod<GenStepSettings>().settings.refUseVanillaTurret;
    }
}