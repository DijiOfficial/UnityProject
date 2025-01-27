# Unity Roguelike Game

## <ins>Introduction</ins>

This game made with Unity is a school project made entirely by myself as part of a course to learn the Unity engine. All the free assets used are linked at the bottom in the [Sources](#sources) section. 
Given two random words we were tasked to complete a prototype of a game as well as learn the Unity Engine within a month.
My words being "Endless" and "Grave", I decided to make a First Person Roguelike, Fast Paced Action, Shooter if the time allowed it. 
The use of the Endless key word was used for a constant life draining mechanic. The life drain would be Endless and part of the story with some exceptions that will be talked about later. This mechanic would allow me to force the action to be fast and constant as the life drain would inevitably kill the player if left unchecked.
As for the Grave key word, I decided to use it as a story directive. Plunging the character into Crypt like rooms with Grave like [Enemies](#enemies).
Below will provide more details of the game, all the info is the initial draft for the prototype. Read about the content that didn't make it in the [Cut Content](section)

## <ins>Game Draft</ins>

### <ins>The 3 C's</ins>

#### Camera

The Camera is First Person with no exceptions.

#### Character

You play as a once-powerful warrior who has risen from the grave, cursed by the undead powers of the [Grave King](). You are trapped in an endless cycle of death, striving to defeat the Grave King and reclaim your soul. 
However, the Grave King leeches your life force from the moment you set foot in his crypt, creating a fast-paced and deadly race to survive. Armed with your best weapon you set out to end his curse.

#### Controls

WASD to move / SPACEBAR (C) to jump/crouch-slide / MOUSE to look around / LEFT/RIGHT CLICK for attack and special (attack/power)/ E for special action and Q for special power

### <ins>Objectives</ins>

#### Goal

1. A roguelike experience
    - The player travels from room to room beating waves of enemies and bosses to reach the [Grave King]() and end the curse.
    - Three types of upgrades are available to the player.
        1. Permanent, bought with the souls of your enemies.
        2. Temporary, bought with coins. Resets after each run.
        3. Temporary, acquired through levels. Resets after each run.
    - Random aspect of the roguelike present through the temporary upgrades, room generation and enemy spawns.
2. Fast Paced Action
    - Life drain mechanic in combination with the healing system forces the player to constantly fight to keep up its health.
    - Combo system to heal faster and deal more damage to keep engagement in fights.
    - Some ranged enemies will run away from the player to create the illusion of being the predator.
    - Upgrades will also include movement upgrades.
    - Momentum can be used as a weapon allowing the player to rush into waves of enemies dealing AOE or concentrated damage

#### Failure

Failure is part of the Roguelike experience, fighting through the levels will allow the player to unlock Permanent upgrades, discovering new collectibles and items to render the player stronger each time. 
The player dies or loses the current run when its health reaches zero and no resurection powers are available.

### <ins>Features</ins>

#### Safe Heaven

The hub, one of my favorite area in any game. The accumulation of your progression can be found here. Connecting the player to everything the game as to offer it is the interactive menu of the game.
NPC, collectibles, weapons, tutorials, customization. This area serves as a transition before starting to the game allowing for fast, efficient and interacive, for the player to upgrades and get back straight into action!

This HUB will contain:
1. Shop
2. Arena
3. NPC
4. Collection
5. Access to the level

##### Shop

The shop contains permanent upgrades, permanent upgrades are kept once bought. These upgrades make the player stronger. they are bought using the souls of your enemies, theses souls are randomly dropped by enemies and bosses.

##### Arena

The arena serves two purposes, first as a simple enemy to test combos and damage on a punching bag for the player. 
As well as the ability to enter an endless mode where enemies constantly spawn there is no other goal than to survive as long as possible. 
Allows the player to practice combos, equipment and overall mechanics.

##### NPC's 

npc's are discovered by playing through the game, saving them will relocate them to the hub where they will provide additional lore information as well as permanent upgrades and help for the following runs.
They can provide sidequest/ side Objectives. completing them may further the npc's story and provide the player with rewards

##### Collectibles

Collectibles for the player to collect, they can provide lore descriptions and/or bonuses. They can be collected in some areas or by doing different challenges.

##### Level Access

Simple and efficient way to get into the game, Interactive way to get to play and select the difficulty.

#### Levels

Levels will be Square-ish Rooms with Random Obstacles.
- Static Room Layouts with Dynamic Obstacles: While the room shapes themselves are fixed, obstacles, traps, and enemy placements are randomized with each level.
- Environmental Hazards: Some rooms feature traps or cursed zones that accelerate health drain, forcing players to either avoid them or move quickly to stay alive. 
- Shrines can slow the curse temporarily but may be hard to reach or in difficult to navigate/ dangerous zones, requiring strategic choices.
- Randomized Enemy Encounters: Each room features randomly generated enemies that become progressively stronger the deeper you go into the crypt. Enemies may also steal your life force, further complicating combat as the health drain accelerates.
- Clearing a level will spawn a summoning circle in the center of the room allowing the player to move to the next room
- Every X room will contain a random boss with special ability/characteristics, defeating them will offer the player a new ability like double jump, dash, temporary flight etc...
- After X room you find the Grave King and can end him there to win
- Different obstacles to provide cover. some can be destroyed.

#### Obstacles and Hazards

- Tombstones/Pillars to provide cover from ranged attacks (may be destructible)
- Spiked Ground/Wall traps to deal damage and knocback the player
- Coffins to provide cover, may also serve a spawners
- Cursed Shrines that accelerate the life drain of the player 
- Holy Lantern shining lights/ or throwing balls of light damaging anyone it touches
- Decomposing enemies serving as Red Barrels
- Life shrines healing everyone in the area
- Teleporation circles allowing the player to quickly teleport within the room to avoid being overwhelmed
- Shrines to provide temporary bonuses at the cost of difficult challenges

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
- Bone Golem is Massive with large health pool but slow, can slam the ground to stun the Player (JUMP to avoid). Can't be stunned or knocked back. Can summon bone spike from summoning circles to immobilize player and deal low damage. Every 1/8th of health will become 1/8th smaller and take 1/8th more damage but will also spawn Skeletons with 1/8th original boss health health and 1/8th total resistance (can respawn after X seconds)

- Unknown warrior -> Has all the same abilities attacks and afflictions of the player (without Temporary upgrades included)

- The Leech -> Can cast ranged attack spells and summon enemies. Cooldown ability to teleport, High health and medium speed. Can cast Very high damage Spells but incantation is very slow. On 1/3rd health will enter rage mode becoming faster and more dangerous also creating decoys, spawning a last wave of enemies and inspiring them giving them faster/higher damage.

- Grave King has 3 phases
    1. Summons, The more summons the lower the health drain power, will attack player from range but can also try a grab attack on melee to steal players health and curse the player making it impossible to recover the lost health.
    2. 50% health: Switches to Life stealing spells, summoning but can now also teleport. (Minions are important for healing player)
    3. 25% health: Stops moving and Concentrates on life stealing aura, life Steal is now at maximum, keep summoning but now cast hold/freezing/slowing spells

### <ins>Mechanics</ins>

- Clearing a level will spawn a summoning circle in the center of the room allowing the player to move to the next room
    - Levels are cleared by killing all enemies spawned
- Enemy will scale with how many rooms you have cleared. New enemies and stronger enemies appear at later stages.
- Bosses will give special abilities upon defeat (Depending on which Mini Boss is Fought), Bone Golem will give player the ability to summon minions with 1/8th player health
- Grave King Endlessly life steals the player through the whole game, the more you progress the faster the life leeching becomes
- Health Recovery Through Combat
    - Life-Stealing Combat: Killing enemies restores health through their life essence. The amount of health recovered scales with enemy type, smaller enemies provide smaller health
    - Kill-Chaining Bonus: The faster you kill enemies in succession, the more health you recover. Killing enemies in rapid sequence grants a health recovery multiplier, encouraging fast and efficient combat. The succession kills can have a Doom like GLORY KILL, upon reaching the maximum succession kill you perform a glory kill and leech the complete life form of your enemy giving you back full health.
- Enemies will randomly drop Soul Fragments and/or can be obtained by completing a Chain kill, upon performing the Glory kill you'll receive a Soul Shard.
- Enemies will randomly drop coins to allow the player to buy temporary upgrades. 
- Player will gain xp whilst fighting allowing special temporary upgrades during each run
- Potential for different kinds of gameplay Melee/Ranged/ Mixed
- Permanent Upgrades can be bought at the Shop
- Temporary upgrades can be chosen between each level during the teleportation sequence, those can be abilities or simple upgrades making your character stronger for the current run
- Player starts with Sword and Dash (any direction and cooldown), fast, medium damage. Can buy Axe, Medium Speed, high damage, can hit more enemies. Can also Buy Hammer, Slow, high damage, can knockback enemies and stop spell casting.
- On every 3-4 hits, Sword deals double damage. Axe Deals bigger AOE, Hammer Slam temporary stun nearby enemies.
- Special Attacks (cooldown):
    - Sword Dash -> Dash forward and deal damage
    - Axe Throw -> throw your axe like boomerang to deal damage
    - Hammer Rage -> Swing your hammer frantically dealing massive damage around you but you when over stunned for X seconds.
- Special power 
    - Shield that temporarily blocks damage
- Momentum can be used to deal AOE damage or focused damaged with the addition of knocback
- Killing enemies will offers 1 combo, stacking combo up to 10 will allow for a glory kill refilling the player's health. the higher the combo the higher the damage and health received

### Upgrades
(upgrades will be listed soon)

## Cut Content

As said previously we only had about a month to complete this assignment as well as learn Unity and C#, not forgetting the other classes. Due to this time constraint and the the project being a solo project, some of the ideas and content didn't make it in the final product.
This includes:
- Leveling up and their upgrades
- Random Room generation, as we were not allowed any form of random generation.
    - regardless of the random generation constraint, most obstacles were cut as well:
        - Pillars are not destructible
        - Spiked Ground/Wall traps
        - Coffins
        - Cursed Shrines  
        - Holy Lanterns
        - Decomposing enemies
        - Life shrines
        - Teleporation circles
        - Shrines
- Bosses:
    - Bone Golem
    - Unknown warrior 
    - The Leech
    - Grave King can be reached. However it's currently a glorified Skeleton
- The Arena, Only the punching bag was added
- NPC's were all cut with their sidequests and rewards
- Collectibles
- The Wraith and Elite enemies
- Only the Necromancer's ability was implemented
- Life leeching does not get stronger the further the level
- The glory kill was implemented, but does not have teh desired visuals with it
- Ranged/Mixed gameplay
- Axe and Hammer
- Weapon bonus, sword slash, axe slash and hammer slam
- Special Attack
- Momentum was not implemented as a weapon

### Future Updates

I will not work on this project any more than I currently have. However, given a proper production I would like to revisit this game with more and improved content. Not limited to the cut content either.
I enjoyed working on this project, but the unity engine and more specifically its physics system is not what I want for the game. This prototype serves as such, a prototype for if I wish to make a complete game out of this.

## Sources

- [https://github.com/CodingQuest2023/Algorithms](https://github.com/CodingQuest2023/Algorithms)
- [https://www.youtube.com/watch?v=qRtrj6Pua2A&](https://www.youtube.com/watch?v=qRtrj6Pua2A&)
- [https://www.youtube.com/watch?v=2SuvO4Gi7uY&](https://www.youtube.com/watch?v=2SuvO4Gi7uY&)
- [https://gamedev.stackexchange.com/questions/201794/how-fast-or-scalable-is-wave-function-collapse](https://gamedev.stackexchange.com/questions/201794/how-fast-or-scalable-is-wave-function-collapse)
- [https://github.com/mxgmn/WaveFunctionCollapse](https://github.com/mxgmn/WaveFunctionCollapse)
- [https://www.youtube.com/watch?v=AdCgi9E90jw&t](https://www.youtube.com/watch?v=AdCgi9E90jw&t)