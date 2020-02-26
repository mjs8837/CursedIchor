# Architecture

## Overview

## General Approach


## State Machine(s)
**MainMenu**
* The main menu for our game is displayed and the user can choose between a couple different options.
* Can transition to the deckbuilder state, and the game level 1 state.
    - Press "Enter" to go from main menu to level 1
    - Press "B" to go to the deck builder
    
**DeckBuilder**
* The deck builder doesn't really do anything but display text that says that it doesn't work yet
* Can transition to the main menu state.
    - WIP

**GameLevel1**
* First level of our game where the player, enemies, the scenery, and the UI are drawn and the game begins.
* Can transition between the pause menu, boss level, and game over screens.
    - Complete the level by killing all enemies to move to the boss level
    - Go to game over if player health is <= 0
    - Go to pause by pressing "P"

**BossLevel**
* Second and final level of our game which includes a fight with the boss along with the other base things of the game
    - These base things include, the player, the scenery, and the UI yet again
* Can transition between the pause menu, end game, and game over screens.
* Defeat boss to go to EndGame State
    - Complete the level by killing the boss to move the the end game state
    - Go to game over if player health is <= 0
    - Go to pause by pressing "P"

**Pause**
* A pause state for our game where the player has the ability to stop playing if they need to.
* Can transition back to the previous game state.
    - Press "P" to go back to the previous game state.

**EndGame**
* The state that is seen when the player beats the boss in the boss level
* Consists of some text that congratulates the player for beating the game
    * Should look nicer by the time the game is finished
* Can transition back to the main menu.
    - Press "M" to get back to the main menu

**GameOver**
* The state that is seen when the player dies.
* Consists of some text that tells the player that they died
    * Again, should look nicer by the time the game is finished.
* Can transition back to the main menu.
    - Press "M" to get back to the main menu

[State diagram](https://ibb.co/MGWhvqv)


## OO Design

GameObject Class (GameObject.cs) - All game objects including player, enemies, background, cards, etc.
* METHODS
    * Update Method:
        - Virtual method for all classes that inherit from GameObject.
    * Draw Method:
        - Virtual method that calls the base draw method and uses the specific game object's properties
        - that all children can inherit from and override.  This can be used by any child class of GameObject.
    * Action Method:
        - Virtual method for all classes that inherit from GameObject.
    * Scrolling Method:
        - Virtual method that any child class of GameObject that needs to scroll can inherit and override.
        - Changes the x position of any scrolling game object by a passed in amount

[INHERITS FROM GAMEOBJECT] Enemy Class (Enemy.cs) - All enemies including boss
* METHODS
    * Collide Method:
        - Method specific to all enemies that checks if it is colliding with any player object
    * TakeDamage Method:
        - Decreases the health of the enemy based on the passed in damage int
        - If the health drops below 0, alive is set to false

[INHERITS FROM ENEMY] Enemy1 Class (Enemy1.cs) - Vampire enemies
* METHODS
    * Update Method:
        - Overriden method that calls the enemy collide method as well as the chase method
    * Chase Method:
        - Takes in a player x positions and player y position and moves the enemy toward the player accordingly
        - Also sets bounds for the vampire enemies

[INHERITS FROM ENEMY] Boss Class (Boss.cs) - Dracula (WIP)
* METHODS
    * Action Method:
        - Overriden action method that checks if the boss is ready to shoot another projectile
        - If so, calls ShootProjectile()
    * Movement Method:
        - Adjusts the y position of the boss based on the y position of the player
    * ProjectileReset Method:
        - Resets the x and y positions of the boss projectile to the x and y of the boss object
    * ShootProjectile Method:
        - Moves the projectile toward the left side of the screen
    * Draw Method:
        - Calls the base draw method and draws the projectiles as well
    * Update Method:
        - Overriden update method that checks the player and calls movement and collide accordingly

[INHERITS FROM GAMEOBJECT] Card class (Cards.cs) - Card abilities for the player to use
* METHODS
    * SetCardNumbers Method:
        - Checks the name of the card, and sets the properties of it based on the name.
    * Update Method:
        - Overriden update method that checks if the card is active and an offensive ability
        - Calls Collide and DeactiviateIfNoCollision if both conditions are met.
    * Action Method:
        - Overriden action method that checks the name of a card and checks if it is active and usable
        - If all conditions are met, then checks if it used
        - If used, the mana is taken away and it can't be used again right away.
        - Then the action method for that card type is called based on the name of the card.
    * Collide Method:
        - Loops through the passed in enemy list and checks if any of the enemies are colliding with the player, checks if they're alive, and chekcs if they're collidable.
        - If those conditions are met, the enemy takes a set amount of damage, and gets knocked back based on which side of the action the enemy is on.
        - Also sets active to false and usable to false.
    * StartValues Method:
        - Sets the card object to be on top of the player
        - Then checks the name of the card and moves based on the name
    * FireBallAction Method:
        - Moves the fireball across the screen at a set value.
    * MeteorAction Method:
        - Moves the meteor down and across the screen at set values
        - Checks when it should disappear
    * HealAction Method:
        - Checks if the card is used
        - Heals the player that was passed into the method
    * SacrificeAction Method:
        - Checks if the card is used
        - Then checks if the health of the passed in player is greater than 1
        - If true, then a certain amount of the player's health is taken, and mana is returned
    * DeactivateIfNoCollision Method:
        - Checks for when there is a collision on the screen
        - If there is, it will be set to inactive and unusable
    * Scrolling Method:
        - Overriden scrolling method that moves the object across the screen, and constantly sets the start and end positions of the object

[INHERITS FROM GAMEOBJECT] Door class (Door.cs) - Door object
* THERE ARE NO METHODS IN THE DOOR CLASS!

[INHERITS FROM GAMEOBJECT] Weapon class (Weapon.cs) -
* METHODS
    * Upate Method: 
        - Calls the action method for the weapon
        - Checks if the active time is greater than 0 and subtracts 1 if it is
        - Otherwise sets active time to 0
        - If the active time is false, then the weapon is no longer "alive"
        - All at the same time, the weapon is checking for collisions by calling CollisionCheck
    * Action Method:
        - Gets the state of the keyboard and checks if the space key is pressed a single time
        - If it's pressed, the sword spawns in
    * CollisionCheck Method:
        - Loops through the passed in enemy list and checks if there is an intersection between them and the weapon and checks if the sword is "alive"
        - If those conditions are met, the enemy in contact takes damage and gets knocked back
    * SingleKeyPress Method:
        - Takes in a key and checks if it is currently up, and checks if the previous state was down

[INHERITS FROM GAMEOBJECT] Player class (Player.cs) -
* METHODS
    * AddImmunity Method:
        - Makes the player immune for a set time
    * ImmuneUpdate Method:
        - Checks if the immunity is more than 0
        - If it is then immune is decresed by 1
    * SometimesUnlucky Method:
        - Checks if the player health is <= 0
        - If it is, the player is no longer alive
    * Draw Method:
        - Overriden draw method that checks if the sword is "alive"
        - If it is, the sword calls its draw method
        - Also calls the base draw method for the player itself
    * Movement Method:
        - Creates a keyboard state to track what key is being pressed
        - Sets a speed for the player to move
        - Checks what key is being pressed, and moves the player in that direction
        - Also sets the bounds that the player can move along

[INHERITS FROM GAMEOBJECT] Background Element (BackgroundEelements.cs) -
* METHODS
    * Update Method:
        - Overriden update method that calls the reset location method
    * ResetLocation Method:
        - Checks if any object is completey off the screen and sets the position to the edge of the screen


[INHERITS FROM GAMEOBJECT] Boss Projectile Class (BossProjectile.cs) -
* METHODS
    * Update Method:
        - Overriden update method that calls the base update method as well as the action and collide methods
    * Action Method:
        - Overriden action method that alters the countdown variable and checks if the boss is ready to fire a projectile
        - If the countdown is <= 0, the projectile reset method is called.
        - Otherwise the move projectile method is called
        - If the projectile reaches a certain point, the projectile is no longer active
    * MoveProjectile:
        - Moves the projectile in the negative x direction
    * ProjectileReset Method:
        - Resets the position of the projectile based on the boss object
    * Collide Method:
        - Checks if the player is immune, the projectile is active, and there is an intersection with the player
        - If all these conditions are met, the player takes damage and is knocked back
        - The player is also immune for a certain amount of time after the collision

[Class diagram](https://ibb.co/VDc5KNf)

## External Tool
* Our main external tool is a level editor
    - It takes in a file name, a level width, and a number of enemies
    - With this information, it creates a file that places X's for every enemy, a D for the door
    - an @ for the player location, and dashes for everything else.
* The level editor creates the file and places it in a folder called levels within the program folder inside the group repo.

* Our other external tool is a deck editor
    - It takes in a deck name, and then a max of 12 cards which you can choose up to 3 of a specific card type.]
    - With this information, it creates a file that writes the total number of cards, then writes each card type selected
    - and its number of said card
* The deck editor creates the file and places it in a forlder called decks within the program folder inside the group repo.

* A sample sheet would look something like:
* 3
* Meteor,1
* Heal,1
* Sacrifice,1


