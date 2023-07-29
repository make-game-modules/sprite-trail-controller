# Unity Sprite Tail Effect

[中文](https://github.com/make-game-modules/sprite-trail-controller/blob/main/README.zh-cn.md)

This is a Unity `SpriteTrailController` script. Its main function is to generate and control a series of sprite clones when a game object moves (controlled by keyboard arrow keys). These clones will offset in the direction of the game object's movement and gradually become transparent, providing a tail effect for the game object. When the game object stops moving, all clones will be destroyed.

## How to Install

In your Unity project, clone this repository at any location using Git.

## How to Use

Add the `SpriteTrailController` script to the game object you want to create the tail effect for, and then set the related parameters in the Unity editor.

## Parameter Settings

- `offset`: The offset of the clone.
- `transparency`: The transparency of the clone.

## Operating Principle

When the position of the game object changes, the script will create a series of clones. These clones will offset in the direction of the game object's movement and gradually become transparent. When the game object stops moving, all clones will be destroyed.

## Copyright Information

This project uses the MIT open source license. Everyone is welcome to improve and use the project.
