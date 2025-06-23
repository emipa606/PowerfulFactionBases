using System.Linq;
using RimWorld;
using RimWorld.BaseGen;
using UnityEngine;
using Verse;
using Verse.AI.Group;

namespace PowerfulFactionBases;

public class SymbolResolver_EdgeDefense : SymbolResolver
{
    private const int DefaultCellsPerTurret = 30;

    private const int DefaultCellsPerMortar = 75;
    public static bool UseVanillaTurret;

    public override void Resolve(ResolveParams rp)
    {
        var map = BaseGen.globalSettings.map;
        var faction = rp.faction ?? Find.FactionManager.RandomEnemyFaction();
        var valueOrDefault = rp.edgeDefenseGuardsCount.GetValueOrDefault();
        int width;
        if (rp.edgeDefenseWidth.HasValue)
        {
            width = rp.edgeDefenseWidth.Value;
        }
        else if (rp.edgeDefenseMortarsCount is > 0)
        {
            width = 4;
        }
        else
        {
            width = Rand.Bool ? 2 : 4;
        }

        width = Mathf.Clamp(width, 1, Mathf.Min(rp.rect.Width, rp.rect.Height) / 2);
        int edgeDefenseTurretsCount;
        int mortarsCount;
        bool wide;
        bool single;
        bool defined;
        switch (width)
        {
            case 1:
                edgeDefenseTurretsCount = rp.edgeDefenseTurretsCount.GetValueOrDefault();
                mortarsCount = 0;
                wide = false;
                single = true;
                defined = true;
                break;
            case 2:
                edgeDefenseTurretsCount = rp.edgeDefenseTurretsCount ?? rp.rect.EdgeCellsCount / DefaultCellsPerTurret;
                mortarsCount = 0;
                wide = false;
                single = false;
                defined = true;
                break;
            case 3:
                edgeDefenseTurretsCount = rp.edgeDefenseTurretsCount ?? rp.rect.EdgeCellsCount / DefaultCellsPerTurret;
                mortarsCount = rp.edgeDefenseMortarsCount ?? rp.rect.EdgeCellsCount / DefaultCellsPerMortar;
                wide = mortarsCount == 0;
                single = false;
                defined = true;
                break;
            default:
                edgeDefenseTurretsCount = rp.edgeDefenseTurretsCount ?? rp.rect.EdgeCellsCount / DefaultCellsPerTurret;
                mortarsCount = rp.edgeDefenseMortarsCount ?? rp.rect.EdgeCellsCount / DefaultCellsPerMortar;
                wide = true;
                single = false;
                defined = false;
                break;
        }

        if (faction != null && (int)faction.def.techLevel < 4)
        {
            edgeDefenseTurretsCount = 0;
            mortarsCount = 0;
        }

        if (valueOrDefault > 0)
        {
            var singlePawnLord = rp.singlePawnLord ??
                                 LordMaker.MakeNewLord(faction, new LordJob_DefendBase(faction, rp.rect.CenterCell),
                                     map);
            for (var i = 0; i < valueOrDefault; i++)
            {
                var value = new PawnGenerationRequest(faction.RandomPawnKind(), faction,
                    PawnGenerationContext.NonPlayer, -1, false, false, false, true, true);
                var resolveParams = rp;
                resolveParams.faction = faction;
                resolveParams.singlePawnLord = singlePawnLord;
                resolveParams.singlePawnGenerationRequest = value;
                resolveParams.singlePawnSpawnCellExtraPredicate = resolveParams.singlePawnSpawnCellExtraPredicate ??
                                                                  delegate(IntVec3 x)
                                                                  {
                                                                      var cellRect = rp.rect;
                                                                      for (var m = 0; m < width; m++)
                                                                      {
                                                                          if (cellRect.IsOnEdge(x))
                                                                          {
                                                                              return true;
                                                                          }

                                                                          cellRect = cellRect.ContractedBy(1);
                                                                      }

                                                                      return true;
                                                                  };
                BaseGen.symbolStack.Push("pawn", resolveParams);
            }
        }

        var rect = rp.rect;
        for (var j = 0; j < width; j++)
        {
            if (j % 2 == 0)
            {
                var resolveParams2 = rp;
                resolveParams2.faction = faction;
                resolveParams2.rect = rect;
                BaseGen.symbolStack.Push("edgeSandbags", resolveParams2);
                if (!wide)
                {
                    break;
                }
            }

            rect = rect.ContractedBy(1);
        }

        var rect2 = defined ? rp.rect : rp.rect.ContractedBy(1);
        for (var k = 0; k < mortarsCount; k++)
        {
            var resolveParams3 = rp;
            resolveParams3.faction = faction;
            resolveParams3.rect = rect2;
            BaseGen.symbolStack.Push("edgeMannedMortar", resolveParams3);
        }

        var rect3 = single ? rp.rect : rp.rect.ContractedBy(1);
        for (var l = 0; l < edgeDefenseTurretsCount; l++)
        {
            var resolveParams4 = rp;
            resolveParams4.faction = faction;
            if (rp.settlementPawnGroupPoints > 4000f)
            {
                if (UseVanillaTurret)
                {
                    var singleThingDef = DefDatabase<ThingDef>.AllDefsListForReading.Where(x =>
                        x.category == ThingCategory.Building &&
                        x.defName is "Turret_Autocannon" or "Turret_Sniper").RandomElement();
                    resolveParams4.singleThingDef = singleThingDef;
                }
                else
                {
                    var singleThingDef2 = DefDatabase<ThingDef>.AllDefsListForReading.Where(x =>
                        x.category == ThingCategory.Building && x.building.IsTurret && x.BaseMaxHitPoints > 199 &&
                        x.defName != "Turret_AutoMortar" && !x.building.shipPart).RandomElement();
                    resolveParams4.singleThingDef = singleThingDef2;
                }
            }
            else
            {
                resolveParams4.singleThingDef = ThingDefOf.Turret_MiniTurret;
            }

            resolveParams4.rect = rect3;
            resolveParams4.edgeThingAvoidOtherEdgeThings = rp.edgeThingAvoidOtherEdgeThings ?? true;
            BaseGen.symbolStack.Push("edgeThing", resolveParams4);
        }
    }
}