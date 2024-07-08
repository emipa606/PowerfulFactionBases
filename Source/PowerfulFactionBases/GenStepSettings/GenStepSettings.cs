using Mlie;
using PowerfulFactionBases;
using UnityEngine;
using Verse;

namespace GenStepSettings;

public class GenStepSettings : Mod
{
    private static string currentVersion;
    public readonly GenStepModSettings settings;

    public GenStepSettings(ModContentPack content)
        : base(content)
    {
        settings = GetSettings<GenStepModSettings>();
        currentVersion = VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        var listing_Standard = new Listing_Standard();
        listing_Standard.Begin(inRect);
        Text.Font = GameFont.Tiny;
        listing_Standard.Label("OPBaseRestartWarning".Translate());
        listing_Standard.Gap();
        Text.Font = GameFont.Small;
        listing_Standard.Label("OPBaseCountLabel".Translate());
        listing_Standard.Gap(4f);
        var buffer = settings.count.ToString();
        listing_Standard.TextFieldNumeric(ref settings.count, ref buffer, 1f);
        listing_Standard.Gap(6f);
        listing_Standard.Label("OPBaseSizeLabel".Translate());
        listing_Standard.Gap(4f);
        var buffer2 = settings.refMenuSettlementSize.ToString();
        listing_Standard.TextFieldNumeric(ref settings.refMenuSettlementSize, ref buffer2);
        listing_Standard.Gap(6f);
        listing_Standard.Label("OPBaseStrengthLabel".Translate());
        listing_Standard.Gap(4f);
        var buffer3 = settings.refDefaultPawnsPoints.ToString();
        listing_Standard.TextFieldNumeric(ref settings.refDefaultPawnsPoints, ref buffer3, 2000f);
        listing_Standard.Gap(6f);
        listing_Standard.CheckboxLabeled("VanillaMortarCountLabel".Translate(), ref settings.refVanillaMortarCount);
        listing_Standard.Gap(6f);
        listing_Standard.CheckboxLabeled("UseVanillaTurretLabel".Translate(), ref settings.refUseVanillaTurret);
        if (currentVersion != null)
        {
            listing_Standard.Gap();
            GUI.contentColor = Color.gray;
            listing_Standard.Label("CurrentModVersionLabel".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listing_Standard.End();
        base.DoSettingsWindowContents(inRect);
    }

    public override string SettingsCategory()
    {
        return "Powerful Faction Bases";
    }

    public override void WriteSettings()
    {
        base.WriteSettings();
        var genStep_Settlement = (GenStep_Settlement)DefDatabase<GenStepDef>.GetNamed("Settlement").genStep;
        genStep_Settlement.count = settings.count;
    }
}