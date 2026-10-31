# GitHub Copilot Instructions for Powerful Faction Bases (Continued) Mod

## Mod Overview and Purpose

Powerful Faction Bases (Continued) is an update to the NilchEis mod. The mod aims to increase the difficulty of attacking NPC faction bases by making them more formidable. This mod does not add any new overpowered items for enemies but instead increases the strength of the units that defend vanilla NPC bases. The mod also adds new powerful enemy variants for vanilla factions, which will only appear as defenders and therefore won't affect raid difficulty. The mod can be safely added or removed from existing save files.

## Key Features and Systems

- More enemy pawns defending their settlements.
- Adds new powerful enemy variants for vanilla factions.
- Removed limitation on types of turrets that can be used by NPC settlements.
- Can change size of NPC settlements from the mod settings.
- Change the number of enemy settlements in single map via mod settings.

## Coding Patterns and Conventions

- Use meaningful variable and method names.
- Follow the RimWorld coding conventions.
- Use comments to explain complex logic.
- Use proper error handling.
- Write unit tests for your code.

## XML Integration

- Use XML files to define mod settings and game entities.
- Use the `Defs` directory to store XML files.
- Use the `Patches` directory to store Harmony patch files.
- Use the `thingDef`, `pawnKindDef`, `dutyDef`, and `mapGeneratorDef` tags to define entities.
- Use the `kindDef` tag to define enemy kinds.

## Harmony Patching

- Use Harmony to patch existing RimWorld code.
- Use the `Patches.xml` file to define patches.
- Use the `[HarmonyPatch]` attribute to define patches.
- Use the `Transpiler` method to modify code.

## Suggestions for Copilot

- Use Copilot to generate XML files for new game entities.
- Use Copilot to generate Harmony patches for existing RimWorld code.
- Use Copilot to generate unit tests for your code.
- Use Copilot to generate code snippets for complex logic.
- Use Copilot to generate code comments for your code.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

