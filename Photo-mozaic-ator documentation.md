# Photo-mozaic-ator documentation

## About

Creator: Lukáš Caha

Supervisor: Mgr. Pavel Ježek, Ph.D.

Course: Programming in C# NPRG035

Language: C#

Framework: Windows Forms Application

## Abstract

Goal of this project was to create user friendly windows application for creating mozaics from photos.

## Events

Because the program is written in WinForms framework it's mainly event driven. This is a map of events sorted to few cathegories.

![image-20210723170758702](C:\Users\Lukas\AppData\Roaming\Typora\typora-user-images\image-20210723170758702.png)

## Color distance strategies

There are currently 3 available color distance metrics:

* Square RBG metric
* Bitwise distance
* CIE76 color metric

## How to work with program

![image-20210723171011665](C:\Users\Lukas\AppData\Roaming\Typora\typora-user-images\image-20210723171011665.png)

![image-20210723171019033](C:\Users\Lukas\AppData\Roaming\Typora\typora-user-images\image-20210723171019033.png)

## Sources of tiles and project

* Source code https://github.com/LukasCaha/Photo-mozaic-ator
* Pokemon dataset https://lukascaha.com/pokemon/pokemon_images.zip • Needs to be tiled
  * Tiled http://lukascaha.com/mozaicator/pokemon.zip
* People dataset https://thispersondoesnotexist.com/
  * Needs to be scraped and then tiled
  * Tiled http://lukascaha.com/mozaicator/people.zip
* Minecraft blocks tileset http://lukascaha.com/mozaicator/minecraft.zip
  * Ready to go
* Rubiks cube tiles http://lukascaha.com/mozaicator/rubiks.zip
