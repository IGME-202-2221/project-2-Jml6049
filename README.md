# Project Village

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

_REPLACE OR REMOVE EVERYTING BETWEEN "\_"_

### Student Info

-   Name: Jonathan Luke
-   Section: 03/04

## Simulation Design

I will attempt to bring a small village of NPCs to life! They will gather food/hunt and guard the village as well as craft.

### Controls

-   Walk: WASD, navigates through the world
    Attack: mouse/LMB, can damage enemy fauna
    Craft: UI Input, can make weapons to help hunt or gaurd village

## Hunter

Will go out and hunt small animals

### Passive

**Objective:** kill small animals and bring their meat back to a store house in the village

#### Steering Behaviors

- Seek - nearest small animal
- Flee - large animals in range
- Obstacles - walls and small animals
- Seperation - other hunters
   
#### State Transistions

- When hunter is in range of small animal
- When there is not enough meat in the village
- When there no threat to the village

### Aggresive

**Objective:** To go out and hunt a large animal

#### Steering Behaviors

- Seek large animals 
- ignore small animals
- Obstacles - walls trees and large animals
- Seperation - small animals 
   
#### State Transistions

- If a large animal wanders too close to the town or hunter

## Large Animal

A larger animal that will seek out food and try to survive while fleeing hunters 

### Forage

**Objective:** The animal will wander in search of small animals

#### Steering Behaviors

- Seek - small animals
- Flee - hunters and the village 
- Obstacles - hunters trees and the village
- Seperation - hunters,players
   
#### State Transistions

- if the animals hunger is relatively high
   
### Hunt(animal)

**Objective:** The animal will become desperately hungry and devour everything in its immediate sight or if there is none to the village and to hunters

#### Steering Behaviors

- Seek - small animals, hunters, player, village
- Obstacles - walls, trees
- Seperation - none?
   
#### State Transistions

- when hunger drops below a threshold 

## Sources

-  Myself :)

## Make it Your Own

- I will make all my own assests and be adding a crafting system and combat into my game

## Known Issues

_List any errors, lack of error checking, or specific information that I need to know to run your program_

### Requirements not completed

_If you did not complete a project requirement, notate that here_

