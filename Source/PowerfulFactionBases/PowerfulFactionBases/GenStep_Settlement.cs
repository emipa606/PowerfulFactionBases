using System.Collections.Generic;
using System.Linq;
using RimWorld;
using RimWorld.BaseGen;
using UnityEngine;
using Verse;

namespace PowerfulFactionBases;

public class GenStep_Settlement : GenStep_Scatterer
{
    private static readonly List<IntVec3> tmpCandidates = [];

    public static int menuSettlementSize;

    public static bool vanillaMortarCount;

    public static float defaultPawnsPoints;

    public override int SeedPart => 1806208471;

    protected override bool CanScatterAt(IntVec3 c, Map map)
    {
        if (!base.CanScatterAt(c, map))
        {
            return false;
        }

        if (!c.Standable(map))
        {
            return false;
        }

        if (c.Roofed(map))
        {
            return false;
        }

        if (!map.reachability.CanReachMapEdge(c, TraverseParms.For(TraverseMode.PassDoors)))
        {
            return false;
        }

        var num = menuSettlementSize;
        if (menuSettlementSize < map.Size.x - 20 && menuSettlementSize >= 35)
        {
            return new CellRect(c.x - (num / 2), c.z - (num / 2), num, num).FullyContainedWithin(new CellRect(0, 0,
                map.Size.x, map.Size.z));
        }

        Log.Message("set settlement size is either too big or too small");
        num = 50;

        return new CellRect(c.x - (num / 2), c.z - (num / 2), num, num).FullyContainedWithin(new CellRect(0, 0,
            map.Size.x, map.Size.z));
    }

    protected override void ScatterAt(IntVec3 c, Map map, GenStepParams parms, int stackCount = 1)
    {
        var floatRange = new FloatRange(defaultPawnsPoints - 500f, defaultPawnsPoints + 500f);
        var num = menuSettlementSize;
        var num2 = menuSettlementSize;
        var rect = new CellRect(c.x - (num / 2), c.z - (num2 / 2), num, num2);
        var faction = map.ParentFaction != null && map.ParentFaction != Faction.OfPlayer
            ? map.ParentFaction
            : Find.FactionManager.RandomEnemyFaction();
        rect.ClipInsideMap(map);
        var resolveParams = default(ResolveParams);
        resolveParams.rect = rect;
        resolveParams.faction = faction;
        resolveParams.settlementPawnGroupPoints = floatRange.RandomInRange;
        resolveParams.edgeDefenseWidth = 4;
        resolveParams.edgeDefenseTurretsCount = Mathf.RoundToInt((float)menuSettlementSize / 5);
        resolveParams.edgeDefenseMortarsCount =
            vanillaMortarCount ? 2 : Mathf.RoundToInt((float)menuSettlementSize / 20);

        BaseGen.globalSettings.map = map;
        BaseGen.globalSettings.minBuildings = 1;
        BaseGen.globalSettings.minBarracks = 1;
        BaseGen.symbolStack.Push("settlement", resolveParams);

        if (faction != null && faction == Faction.OfEmpire)
        {
            BaseGen.globalSettings.minThroneRooms = 1;
            BaseGen.globalSettings.minLandingPads = 1;
        }

        BaseGen.Generate();
        if (faction != null && faction == Faction.OfEmpire && BaseGen.globalSettings.landingPadsGenerated == 0)
        {
            GenerateLandingPadNearby(resolveParams.rect, map, faction);
        }

        BaseGen.Generate();
    }

    public static void GenerateLandingPadNearby(CellRect rect, Map map, Faction faction)
    {
        var resolveParams = default(ResolveParams);
        MapGenerator.TryGetVar<List<CellRect>>("UsedRects", out var usedRects);
        tmpCandidates.Clear();
        var size = 9;
        tmpCandidates.Add(new IntVec3(rect.maxX + 1, 0, rect.CenterCell.z));
        tmpCandidates.Add(new IntVec3(rect.minX - size, 0, rect.CenterCell.z));
        tmpCandidates.Add(new IntVec3(rect.CenterCell.x, 0, rect.maxZ + 1));
        tmpCandidates.Add(new IntVec3(rect.CenterCell.x, 0, rect.minZ - size));
        if (!tmpCandidates.Where(delegate(IntVec3 x)
            {
                var r = new CellRect(x.x, x.z, size, size);
                return r.InBounds(map) && (usedRects == null || !usedRects.Any(y => y.Overlaps(r)));
            }).TryRandomElement(out var result))
        {
            return;
        }

        resolveParams.rect = new CellRect(result.x, result.z, size, size);
        resolveParams.faction = faction;
        BaseGen.globalSettings.map = map;
        BaseGen.symbolStack.Push("landingPad", resolveParams);
        BaseGen.Generate();
    }
}