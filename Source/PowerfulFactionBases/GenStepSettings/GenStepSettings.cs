using Mlie;
using PowerfulFactionBases;
using UnityEngine;
using Verse;

namespace GenStepSettings;

public class GenStepSettings : Mod
{
    private static string currentVersion;
    public readonly GenStepModSettings Settings;

    public GenStepSettings(ModContentPack content)
        : base(content)
    {
        Settings = GetSettings<GenStepModSettings>();
        currentVersion = VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        var listingStandard = new Listing_Standard();
        listingStandard.Begin(inRect);
        Text.Font = GameFont.Tiny;
        listingStandard.Label("OPBaseRestartWarning".Translate());
        listingStandard.Gap();
        Text.Font = GameFont.Small;
        listingStandard.Label("OPBaseCountLabel".Translate());
        listingStandard.Gap(4f);
        var buffer = Settings.Count.ToString();
        listingStandard.TextFieldNumeric(ref Settings.Count, ref buffer, 1f);
        listingStandard.Gap(6f);
        listingStandard.Label("OPBaseSizeLabel".Translate());
        listingStandard.Gap(4f);
        var buffer2 = Settings.RefMenuSettlementSize.ToString();
        listingStandard.TextFieldNumeric(ref Settings.RefMenuSettlementSize, ref buffer2);
        listingStandard.Gap(6f);
        listingStandard.Label("OPBaseStrengthLabel".Translate());
        listingStandard.Gap(4f);
        var buffer3 = Settings.RefDefaultPawnsPoints.ToString();
        listingStandard.TextFieldNumeric(ref Settings.RefDefaultPawnsPoints, ref buffer3, 2000f);
        listingStandard.Gap(6f);
        listingStandard.CheckboxLabeled("VanillaMortarCountLabel".Translate(), ref Settings.RefVanillaMortarCount);
        listingStandard.Gap(6f);
        listingStandard.CheckboxLabeled("UseVanillaTurretLabel".Translate(), ref Settings.RefUseVanillaTurret);
        if (currentVersion != null)
        {
            listingStandard.Gap();
            GUI.contentColor = Color.gray;
            listingStandard.Label("CurrentModVersionLabel".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listingStandard.End();
        base.DoSettingsWindowContents(inRect);
    }

    public override string SettingsCategory()
    {
        return "Powerful Faction Bases";
    }

    public override void WriteSettings()
    {
        base.WriteSettings();
        var genStepSettlement = (GenStep_Settlement)DefDatabase<GenStepDef>.GetNamed("Settlement").genStep;
        genStepSettlement.count = Settings.Count;
    }
}