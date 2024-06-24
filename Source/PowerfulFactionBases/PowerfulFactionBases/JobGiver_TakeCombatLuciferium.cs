using RimWorld;
using Verse;
using Verse.AI;

namespace PowerfulFactionBases;

public class JobGiver_TakeCombatLuciferium : ThinkNode_JobGiver
{
    protected override Job TryGiveJob(Pawn pawn)
    {
        if (!pawn.kindDef.defName.Equals("Tribal_ChiefMeleeDefender"))
        {
            return null;
        }

        if (pawn.Downed || pawn.mindState.anyCloseHostilesRecently)
        {
            return null;
        }

        var thing = FindLuciferium(pawn);
        if (thing != null)
        {
            return new Job(JobDefOf.Ingest, thing)
            {
                count = 1
            };
        }

        thing = GenClosest.ClosestThingReachable(pawn.Position, pawn.Map,
            ThingRequest.ForGroup(ThingRequestGroup.HaulableAlways), PathEndMode.OnCell,
            TraverseParms.For(pawn), 10f, Validator);
        if (thing == null)
        {
            return null;
        }

        return new Job(JobDefOf.Ingest, thing)
        {
            count = 1
        };

        bool Validator(Thing t)
        {
            return t.def.defName.Equals("Luciferium") && t.IngestibleNow && pawn.RaceProps.CanEverEat(t) &&
                   pawn.CanReserve(t);
        }
    }

    private Thing FindLuciferium(Pawn pawn)
    {
        foreach (var thing in pawn.inventory.innerContainer)
        {
            if (thing.def.defName.Equals("Luciferium"))
            {
                return thing;
            }
        }

        return null;
    }
}