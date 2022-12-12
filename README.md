# Project Village

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

_REPLACE OR REMOVE EVERYTING BETWEEN "\_"_

### Student Info

-   Name: Jonathan Luke
-   Section: 03/04

## Simulation Design

I will attempt to bring a small village of NPCs to life! They will gather food/hunt and guard the village as well.

### Controls

-   Walk: WASD, navigates through the world
    Attack: mouse/LMB, can damage enemy fauna

## Hunter

Will go out and hunt small animals

### Passive

**Objective:** kill small animals 

#### Steering Behaviors

- Custom Seek - nearest small animal
- Flee - large animals 
- Wander
- Seperation - other hunters
   
#### State Transistions

- The hunters will become aggresive if the Large Animal goes on a rampage

### Aggresive

**Objective:** To go out and hunt a large animal

#### Steering Behaviors

- Seek large animals 
- ignore small animals
- Seperation - hunters
   
#### State Transistions

- Will become passive if large animal dies

## Large Animal

A larger animal that will seek out food and try to survive while fleeing hunters 

### Hibernate

**Objective:** The animal will wander in it's cave and be docile

#### Steering Behaviors

- Seek - it's home
- Flee - hunters and the village 
- Seperate - other large animals
- wander - around it's cave
   
#### State Transistions

- if the animals hunger is low
   
### Hunt(animal)

**Objective:** The animal will become desperately hungry and target the player and hunters

#### Steering Behaviors

- Seek - small animals, hunters, player, village (if low enough seek it's home)
- wander
- Seperation - other large animals
- Flee - when health is low flee from everything
   
#### State Transistions

- when hunger drops below a threshold 

## Sources

-  Myself :)

## Make it Your Own

- I will make all my own art 

## Known Issues

I've gone back on crafting I will drop the entire element 

### Requirements not completed

_If you did not complete a project requirement, notate that here_

