# GitHub Copilot Instructions for "Powerful Faction Bases (Continued)"

## Mod Overview and Purpose

The "Powerful Faction Bases (Continued)" mod is a concept project aimed at increasing the challenge posed by NPC faction bases in RimWorld. The base game does not scale the difficulty of these bases with player strength, often making them too easy to defeat. This mod enhances the existing faction bases by introducing stronger defenders and other challenges, creating an optional end-game experience for players seeking a formidable test of their skills. The bases become sprawling fortresses requiring significant effort and strategy to overcome.

## Key Features and Systems

- **Enemy Strength Scaling**: Significantly increased strength of enemy pawns defending the bases, offering a greater challenge without adding any overpowered items.
- **Powerful Enemy Variants**: Introduces new, stronger enemy variants exclusive to defending these bases, ensuring the game's raid difficulty remains unaffected.
- **Customizable Base Sizes and Defenses**: Mod settings allow players to adjust the size of NPC settlements and types of turrets they can use, tailoring the difficulty.
- **Adjustable Settlement Count**: Players can set the number of enemy settlements on a map, influencing enemy numbers and further adjusting the challenge level.

## Coding Patterns and Conventions

- **Class Definitions**: Each class is defined with clear, purpose-specific names, such as `GenStepModSettings`, `SettingsImplementer`, and `SymbolResolver_EdgeDefense`, to ensure clarity and maintainability.
- **Namespace Usage**: Ensure proper namespace usage to organize classes systematically, especially as the mod evolves and introduces more classes and features.
- **Method Structure**: Follow the C# convention of camelCase for private and local method names, while public methods start with a capitalized letter.
- **Comments**: Maintain thorough inline comments for method logic and specific algorithmic choices to aid future modifications and Copilot's understanding.

## XML Integration

- **Data Definitions**: Integrate XML for data definitions to specify NPC characteristics, settlement layouts, and other configuration parameters.
- **Mod Settings**: Leverage the RimWorld mod settings XML to offer users in-game customization options, as established in `GenStepSettings.cs`.
- **Translation Support**: Use XML to manage translations, ensuring wide accessibility and support for languages such as Japanese, contributed by community members.

## Harmony Patching

- **Patch Application**: Use Harmony to apply patches to the vanilla game's code, ensuring seamless integration of new features like increased defender strength and revised settlement rules.
- **Patch Organization**: Organize patches into a structured folder system and apply them conditionally, ensuring compatibility and ease of debugging.
- **Compatibility Handling**: Proactively handle mod conflicts by checking for known incompatible mods and offering detailed documentation on potential issues.

## Suggestions for Copilot

- **AI Assistance**: Utilize GitHub Copilot to suggest boilerplates for new classes or methods based on existing patterns within the mod, such as the procedural generation logic used in `GenStep_Settlement`.
- **Patch Recommendations**: Employ Copilot to propose ideal methods and property patches, especially where enemy scaling or base customization requires nuanced changes.
- **Mod Extensions**: Let Copilot suggest improvements or extensions based on similar mods and community trends, keeping the project innovative and engaging.

## Additional Notes

- Always ensure to test mod changes with minimal setup, slowly integrating other mods to pinpoint compatibility issues.
- Regularly update the mod’s GitHub repository with error solutions and improvements contributed by community testers.
- Use communication channels for feedback, like Discord, to gather user experience reports, enhancing mod quality and player satisfaction. 

By following these guidelines and utilizing features like Copilot, mod development can be streamlined, innovative, and aligned with player expectations for immersive experiences in RimWorld.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.
