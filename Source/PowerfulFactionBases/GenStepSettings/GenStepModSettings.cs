using Verse;

namespace GenStepSettings;

public class GenStepModSettings : ModSettings
{
    public int Count = 2;

    public int RefDefaultPawnsPoints = 5000;

    public int RefMenuSettlementSize = 50;

    public bool RefUseVanillaTurret;

    public bool RefVanillaMortarCount = true;

    public override void ExposeData()
    {
        Scribe_Values.Look(ref Count, "count", 2);
        Scribe_Values.Look(ref RefMenuSettlementSize, "refMenuSettlementSize", 50);
        Scribe_Values.Look(ref RefDefaultPawnsPoints, "refDefaultPawnsPoints", 5000);
        Scribe_Values.Look(ref RefVanillaMortarCount, "refVanillaMortarCount", true);
        Scribe_Values.Look(ref RefUseVanillaTurret, "refUseVanillaTurret");
        base.ExposeData();
    }
}