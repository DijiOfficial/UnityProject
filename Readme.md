# Unity Roguelike Game

You can play the latest playable version [here](https://github.com/DijiOfficial/UnityProject/releases/tag/v1.8)

Watch A Gameplay Demo here: 

https://github.com/DijiOfficial/UnityProject/blob/main/Media/GameplayVideo.mp4

## <ins>Introduction</ins>

This game, created using [Unity](https://assetstore.unity.com), is a school project I developed entirely by myself as part of a course to learn the [Unity engine](https://assetstore.unity.com). All the free assets used are credited and linked at the bottom in the [Sources](#sources) section.

As part of the project, we were given two random words and tasked with completing a game prototype while learning the Unity Engine within a month.
My assigned words were "**Endless**" and "**Grave**", which inspired me to design a First-Person Roguelike, Fast-Paced Action Shooter, time permitting.

The "**Endless**" keyword is represented by a constant life-draining mechanic. This mechanic is integral to both the gameplay and story, with a few exceptions. The endless life drain forces the gameplay to be fast-paced and relentless, as the player will inevitably die if they remain idle or passive.

The "**Grave**" keyword serves as a narrative and environmental theme, placing the player in crypt-like rooms filled with grave-inspired enemies.

The following [section](#game-draft) provide more details about the game. Note that all information is part of the initial draft for the prototype. For details on features that didn't make it into the final version, see the [Cut Content](#cut-content) section.

## <ins>Game Draft</ins>

### <ins>The 3 C's</ins>

#### Camera

The Camera is First Person with no exceptions.

#### Character

You play as a once-powerful warrior, resurrected from the grave and cursed by the undead powers of the [Grave King](#grave-king). Trapped in an endless cycle of death, your goal is to defeat the [Grave King](#grave-king) and reclaim your soul.

However, the [Grave King](#grave-king) continuously leeches your life force from the moment you set foot in his crypt, creating a fast-paced and deadly race for survival. Armed with your most trusted weapon, you set out to break his curse once and for all.

#### Controls

| Key/Control       | Action                       |  
|-------------------|------------------------------|  
| **W, A, S, D**    | Move                         |  
| **SPACEBAR (C)**  | Jump / Crouch-Slide          |  
| **Mouse**         | Look Around                  |  
| **Left Click**    | Attack                       |  
| **Right Click**   | Special Attack / Power       |  
| **E**             | Special Action               |  
| **Q**             | Special Power                |  

### <ins>Objectives</ins>

#### Goal

1. A Roguelike Experience
    - Players travel from room to room, defeating waves of enemies and bosses to ultimately face the [Grave King](#grave-king) and lift the curse.
    - Three Types of [Upgrades](#upgrades):
        - Permanent: Purchased using the souls of your enemies, these [upgrades](#upgrades) carry over between runs.
        - Temporary (Coin-Based): Bought with coins and reset after each run.
        - Temporary (Level-Based): Acquired during gameplay and reset after each run.
    - The roguelike randomness is evident in the procedurally generated room layouts, [enemy](#enemies) spawns, and temporary [upgrades](#upgrades).
2. Fast-Paced Action
    - The life drain mechanic, combined with the healing system, forces players to stay engaged in combat to maintain their health.
    - A combo system rewards players with faster healing and increased damage, encouraging aggressive gameplay.
    - Ranged enemies will strategically retreat, creating a dynamic where the player feels like the predator.
    - [Upgrades](#upgrades) include movement enhancements, allowing for faster and more fluid traversal.
    - Momentum as a Weapon: Players can harness their movement to deliver area-of-effect (AOE) or concentrated damage by rushing into waves of enemies.

#### Failure

Failure is an integral part of the roguelike experience. Progressing through [levels](#levels) enables the player to unlock permanent [upgrades](#upgrades), discover new [collectibles](#collectibles), and acquire items that make them stronger with each run.

A run ends when the player's health reaches zero, and no resurrection powers are available.

### <ins>Features</ins>

#### Safe Heaven

The hub is one of my favorite areas in any game. It serves as the central point where the accumulation of your progression is showcased. Connecting the player to everything the game has to offer, it functions as an interactive menu, blending immersion with utility.

From NPC's and collectibles to weapons, tutorials, and customization, this area provides a seamless transition before diving into the action. It's designed for fast, efficient, and interactive access, enabling players to upgrade and jump straight back into the fray.

The Hub Will Include:
1. [Shop](#shop): Purchase upgrades.
2. [Arena](#arena): Practice combat skills and test new upgrades as well enter the endless fight mode.
3. [NPC's](#npc's): Interact with characters for story progression, lore, side quests and rewards.
4. [Collection](#collectibles): View collectibles and track achievements.
5. [Access to Level](#level-access): Begin your journey.

##### Shop

The shop offers permanent [upgrades](#upgrades), which remain unlocked once purchased. These [upgrades](#upgrades) enhance the player's abilities, making them stronger over time.

[Upgrades](#upgrades) are purchased using the souls of your enemies. These souls are dropped randomly by enemies and bosses.

##### Arena

The arena serves two main purposes:

1. Combat Practice: It acts as a simple space where players can test combos, damage output, and experiment with new moves on a "punching bag" enemy.
2. Endless Mode: Players can enter an endless mode where enemies spawn continuously. The goal is simply to survive for as long as possible.

The arena provides a space for the player to practice combos, test different equipment, and refine their overall gameplay mechanics.

##### NPC's 

NPC's are discovered as the player progresses through the game. By saving them, they are relocated to the hub, where they offer additional lore, permanent upgrades, and assistance for future runs.

NPC's can also provide side quests or side objectives. Completing these tasks may advance the NPC's story and reward the player with valuable items or upgrades.

##### Collectibles

Collectibles are scattered throughout the game, offering players the chance to uncover lore descriptions and gain bonuses. They can be found in various areas or obtained by completing specific challenges.

Additionally, some collectibles can be acquired through the completion of achievements, which can also be tracked in the Collection.

##### Level Access

A simple and efficient way to dive into the game, with an interactive method for selecting the difficulty and starting your adventure.

#### Levels

**Levels** will consist of square-ish rooms with random obstacles and features:
- **Static Room Layouts with Dynamic Obstacles**: The room shapes themselves remain fixed, but the obstacles, traps, and enemy placements are randomized each time you enter.
- **Environmental Hazards**: Certain rooms contain traps or cursed zones that accelerate health drain. Players must either avoid these areas or act quickly to survive.
- **Shrines**: Some shrines can temporarily slow the curse, but they may be hard to reach or located in dangerous areas, requiring strategic decision-making.
- **Randomized Enemy Encounters**: Each room contains randomly generated enemies that grow stronger as you progress deeper into the crypt. Some enemies may even steal your life force, complicating combat as the health drain intensifies.
- **Level Completion**: Upon clearing a level, a summoning circle will appear in the center of the room, allowing you to move on to the next.
- **Boss Battles**: Every few rooms, you will face a random boss with special abilities and characteristics. Defeating them rewards you with new temporary ability.
- **Final Encounter**: After a set number of rooms, you will encounter the [Grave King](#grave-king) and can defeat him to win the game.
- **Obstacles for Cover**: Various obstacles can provide cover, and some can be destroyed to create new tactical opportunities.


#### Obstacles and Hazards

- **Tombstones/Pillars**: Provide cover from ranged attacks; may be destructible.
- **Spiked Ground/Wall Traps**: Deal damage and knock back the player.
- **Coffins**: Serve as cover, and may also act as spawners for enemies.
- **Cursed Shrines**: Accelerate the player's life drain, adding additional risk.
- **Holy Lanterns**: Emit damaging light beams or throw balls of light that harm anyone they touch.
- **Decomposing Enemies**: Function like red barrels, exploding when hit or triggered.
- **Life Shrines**: Heal everyone within their vicinity, aiding both enemies and the player.
- **Teleportation Circles**: Allow the player to quickly teleport within the room, helping avoid being overwhelmed.
- **Shrines of Temporary Bonuses**: Grant powerful, temporary bonuses at the cost of difficult challenges.

#### Enemies

| Enemy       | Type         | Damage | Health | Speed  | attack Speed | Special trait | 
| :---------: | :----------: | :----: | :----: | :----: | :----------: |:------------: |
| Skeleton    | Melee        | Medium | Medium | Medium | Medium       | Can get back up after being defeated |
| Necromancer | Ranged/Melee | High   | Medium | Medium | Low          | Can summon weaker enemies |
| Wraith      | Melee        | Low    | Medium | High   | Medium       | Can phase shift allowing them do dodge physical attacks |
| Zombie      | Melee        | High   | High   | Low    | Low          | Can stun the player temporarily on hit |
| Archer      | Ranged       | Medium | Low    | High   | Medium       | Arrows can cause AOE effect on miss |
| Others? | ... | ... | ... | ... | ... | ... |

Possibilty of adding Elite Enemies?

#### Bosses

- **Bone Golem**:  
  A massive, high-health enemy that moves slowly. It can slam the ground to stun the player (jump to avoid). The Bone Golem cannot be stunned or knocked back. It can summon bone spikes from summoning circles to immobilize the player and deal low damage.  
  - As it loses health (every 1/8th of its total health), the Bone Golem shrinks in size and takes 1/8th more damage. It also spawns skeletons with 1/8th of the boss's original health and resistance.

- **Unknown Warrior**:  
  This enemy mirrors the player's abilities, attacks, and afflictions (excluding temporary upgrades).

- **The Leech**:  
  A ranged spellcaster with the ability to summon enemies. The Leech has a cooldown-based teleportation ability, high health, and medium speed. It can cast very high-damage spells, though the incantation takes time to cast.  
  - When it reaches 1/3rd of its health, it enters a rage mode, becoming faster and more dangerous. It also creates decoys, spawns a final wave of enemies, and inspires them, increasing their speed and damage output.

- **Grave King** <a id="grave-king"></a>:  
  The Grave King has three distinct phases:  
  1. **Phase 1 (100% Health)**: The Grave King summons enemies. The more summons he creates, the lower the health drain power. He attacks from range but can also attempt a grab attack to steal the player's health and curse them, making it impossible to recover the lost health.  
  2. **Phase 2 (50% Health)**: The Grave King switches to life-stealing spells and summoning, gaining the ability to teleport. Minions become essential for the player's healing.  
  3. **Phase 3 (25% Health)**: The Grave King stops moving and focuses on a life-stealing aura, which reaches its maximum power. He continues summoning enemies while now casting hold, freezing, and slowing spells to hinder the player.

### <ins>Mechanics</ins>

- **Clearing a Level**:  
  - Clearing a level will spawn a summoning circle in the center of the room, allowing the player to move to the next room.  
  - Levels are cleared by killing all enemies that spawn.

- **Enemy Scaling**:  
  - Enemies scale with how many rooms the player has cleared. New and stronger enemies will appear at later stages.

- **Boss Rewards**:  
  - Defeating bosses grants the player special abilities based on which mini-boss was fought. For example, the Bone Golem will give the player the ability to summon minions with 1/8th of the player’s health.

- **Grave King Life Steal**:  
  - The Grave King endlessly life steals from the player throughout the game. The more the player progresses, the faster the life leeching becomes.

- **Health Recovery Through Combat**:  
  - **Life-Stealing Combat**: Killing enemies restores health through their life essence. The amount of health recovered scales with enemy type—smaller enemies provide less health.  
  - **Kill-Chaining Bonus**: Killing enemies in rapid succession grants a health recovery multiplier, encouraging fast and efficient combat.  
  - **Glory Kill**: Upon reaching the maximum kill chain, the player performs a glory kill and leeches the complete life essence of the enemy, restoring full health.

- **Soul Fragments and Soul Shards**:  
  - Enemies drop Soul Fragments randomly, which can be used for upgrades.  
  - Performing a Glory Kill grants a Soul Shard.

- **Coins**:  
  - Enemies will randomly drop coins, allowing the player to buy temporary upgrades.

- **XP and Temporary Upgrades**:  
  - The player gains XP while fighting, unlocking temporary upgrades that can be used during each run.

- **Gameplay Variability**:  
  - The game offers different kinds of gameplay styles: Melee, Ranged, or Mixed.

- **Upgrades**:  
  - **Permanent Upgrades**: These can be bought at the Shop using Soul Fragments.  
  - **Temporary Upgrades**: Chosen between each level during the teleportation sequence. These can be abilities or simple upgrades that make the character stronger for the current run.

- **Weapons and Abilities**:  
  - The player starts with a Sword and Dash (medium speed, medium damage, cooldown).  
  - Additional weapons can be bought:  
    - **Axe**: Medium speed, high damage, hits multiple enemies.  
    - **Hammer**: Slow speed, high damage, knocks back enemies and stops spell casting.

- **Weapon Special Effects**:  
  - **Sword**: Deals double damage every 3-4 hits.  
  - **Axe**: Deals a bigger AOE attack.  
  - **Hammer**: Slam attack temporarily stuns nearby enemies.

- **Special Attacks** (Cooldown):  
  - **Sword Dash**: Dash forward and deal damage.  
  - **Axe Throw**: Throw the axe like a boomerang to deal damage.  
  - **Hammer Rage**: Swing the hammer frantically to deal massive damage around the player, but leaves them stunned for a short time.

- **Special Power**:  
  - **Shield**: Temporarily blocks incoming damage.

- **Momentum**:  
  - Momentum can be used to deal AOE damage or focused damage, adding knockback effects.

- **Combo System**:  
  - Killing enemies adds 1 combo point.  
  - A combo can stack up to 10.  
  - At maximum combo, the player can perform a Glory Kill to refill health. The higher the combo, the more damage and health the player receives.

### Upgrades
(upgrades will be listed soon)

## Cut Content

As mentioned previously, we only had about a month to complete this assignment, learn [Unity](https://assetstore.unity.com) and C#. Due to this time constraint and the project being a solo effort, some of the ideas and content didn't make it into the final product.

This includes:
- Leveling up and their upgrades
- Random room generation, as we were not allowed any form of random generation.
    - Despite the random generation constraint, most obstacles were also cut:
        - Pillars are not destructible
        - Spiked ground/wall traps
        - Coffins
        - Cursed shrines
        - Holy lanterns
        - Decomposing enemies
        - Life shrines
        - Teleportation circles
        - Shrines

- **Bosses**:
    - Bone Golem
    - Unknown Warrior
    - The Leech
    - Grave King can be reached, but it's currently just a glorified skeleton.

- **The Arena**:
    - Only the punching bag was added.

- **NPCs**:
    - All NPCs were cut along with their side quests and rewards.

- **Collectibles**:
    - These were removed from the final product.

- **The Wraith and Elite Enemies**:
    - These enemies were also removed.

- **Only the Necromancer's ability**:
    - Was implemented.

- **Life Leeching**:
    - Does not get stronger the further you progress in the level.

- **Glory Kill**:
    - Implemented, but does not have the desired visuals.

- **Ranged/Mixed Gameplay**:
    - Not implemented.

- **Axe and Hammer**:
    - Were not added to the game.

- **Weapon Bonuses**:
    - Sword slash, axe slash, and hammer slam were not implemented.

- **Special Attack**:
    - Not implemented.

- **Momentum as a Weapon**:
    - Not implemented.


### Future Updates

I will not work on this project any more than I currently have. However, given a proper production I would like to revisit this game with more and improved content. Not limited to the cut content either.
I enjoyed working on this project, but the Unity engine and more specifically its physics system is not what I want for the game. This prototype serves as such, a prototype for if I wish to make a complete game out of this.

## Sources

These are the free assets I used from the [Unity](https://assetstore.unity.com) [Asset Store](https://assetstore.unity.com).
They are not sorted in any particular order, duplicate tags are different links.

- [Physics lerping](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676)
- [Dungeon](https://assetstore.unity.com/packages/3d/environments/dungeons/ultimate-low-poly-dungeon-143535)
- [Dungeon](https://assetstore.unity.com/packages/3d/environments/dungeons/low-poly-dungeons-lite-177937)
- [Dungeon](https://assetstore.unity.com/packages/3d/environments/dungeons/lite-dungeon-pack-low-poly-3d-art-by-gridness-242692)
- [Dungeon](https://assetstore.unity.com/packages/3d/environments/low-poly-medieval-market-262473)
- [Gold Assets](https://assetstore.unity.com/packages/3d/props/coin-treasure-bundle-with-animation-3d-250070)
- [Player](https://assetstore.unity.com/packages/3d/characters/humanoids/fantasy/modular-fantasy-knight-character-276754)
- [Zombie](https://assetstore.unity.com/packages/3d/characters/humanoids/zombie-30232)
- [Necromancer](https://assetstore.unity.com/packages/3d/characters/humanoids/fantasy/necromancer-army-ghoul-283690)
- [Vampire](https://assetstore.unity.com/packages/3d/characters/vampire-1-236808)
- [Skeleton](https://assetstore.unity.com/packages/3d/characters/humanoids/fantasy/rpg-skeleton-35463)
- [Weapons](https://assetstore.unity.com/packages/3d/props/weapons/free-low-poly-fantasy-rpg-weapons-248405)
- [Weapons](https://assetstore.unity.com/packages/3d/props/weapons/low-poly-rpg-fantasy-weapons-lite-226554)
- [Effects](https://assetstore.unity.com/packages/vfx/particles/spells/magic-effects-free-247933)
- [Effects](https://assetstore.unity.com/packages/vfx/particles/cartoon-fx-remaster-free-109565)
- [HUD](https://assetstore.unity.com/packages/2d/gui/fantasy-wooden-gui-free-103811)
- [Icons](https://assetstore.unity.com/packages/2d/gui/icons/game-input-controller-icons-free-285953)
- [Icons](https://assetstore.unity.com/packages/2d/gui/dark-brown-gui-kit-201086)


Should I have missed some reference, feel free to let me know.
