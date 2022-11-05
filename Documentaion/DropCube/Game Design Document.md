
## Game Synopsis

Drop Cube is a iOS unity game that has the player controlling a cube that must navigate through and endless array of platforms in order to stay on screen as the camera slowly descends

## Purpose/Aim


## Game Research


## Character


## Level Design
Room Spawning is going to consist of a few components that build up the room spawner.

At the highest level there will be a Spawner that spawns new room objects directly under the previous one at a controlled interval. This interval will probably get faster as the game progresses.

At the next level, there is a Room. A room consists of 2 Walls and a Floor with a door, possibly a switch to open the door too. A Wall will be a seperate object and the floor will also be another object. These objects are spawned via script and their positions are calculated using some randomisation.

![](https://media.milanote.com/p/images/1O7X2v10HobE22/f4U/Room.png)

## Art Style


## Rules

#### Camera Movement:
The camera moves downwards with the player but not at the same rate as the player. It will slow down when the player is at the top of the screen to give them a chance to catch up, and speed up when the player is at the bottom of the screen in order to catch up to the player. The Camera will also speed up over time.

#### Death
If the Player can't keep up with the movement of the Camera, the player will lose

#### Points
The player will be awarded Points for each new room that they enter.

## Mechanics

**Movement**:
* Touch Input

**Level Mechanics**:
* Doors
* Switches
* Room Spawning

**Secondary**:
* Points

## Controls

The Touch controls are controlled using Unitys Input System. The Input System generates the classes based on preset behaviour for touching the screen. From there the input is delivered to the InputManager where the Events are created. The PlayerInput class then subscribes to the TouchEvents from the InputManager with a concrete implementation for what those controls do and how the player behaves once the input has been triggered.


### Player Movement

To move the player, the Event from the input system passes a Vector2 as an argument to the Subscribed Move method in the PlayerInput class. This Vector2 provides the method with the Screen Coordinates of where the player has pressed on their screen in screen space.

This allows us to provide a direction to the player by using the middle of the screen as a zero direction, left as -1 and right as 1. Providing this value to the Vector3.MoveToward function allows for smooth movement in the direction of which the player is touching the screen.

Lastly 2 short Raycasts are projected out to the left and right of the player to detect if they have hit a wall. If the player detects a wall the direction will be set to zero.

## Camera

The camera movement will need to have some smart logic to it so that the player doesn’t fall off the bottom of the screen. I plan to try to speed up the camera moving up the screen when the player is at the very bottom of the screen than if the player is at the very top of the screen. this will give the player a chance to make a come back when they are at the top of the screen and keep them on the screen when they are at the bottom.

![](https://media.milanote.com/p/resized/1OHnou1msRNV80/1OHnou1msRNV80-DnXbz-huge.png)



## Architecture


## Programming
