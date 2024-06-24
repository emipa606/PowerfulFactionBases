using Verse;

namespace GenStepSettings;

public class GenStepModSettings : ModSettings
{
    public int count = 2;

    public int refDefaultPawnsPoints = 5000;

    public int refMenuSettlementSize = 50;

    public bool refUseVanillaTurret;

    public bool refVanillaMortarCount = true;

    public override void ExposeData()
    {
        Scribe_Values.Look(ref count, "count", 2);
        Scribe_Values.Look(ref refMenuSettlementSize, "refMenuSettlementSize", 50);
        Scribe_Values.Look(ref refDefaultPawnsPoints, "refDefaultPawnsPoints", 5000);
        Scribe_Values.Look(ref refVanillaMortarCount, "refVanillaMortarCount", true);
        Scribe_Values.Look(ref refUseVanillaTurret, "refUseVanillaTurret");
        base.ExposeData();
    }
}