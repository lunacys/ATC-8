# ATC-8

## Overview

**ATC-8** (Advanced Toy 8-bit Computer) itself is a virtual machine designed to mimic a non-existing video game console. Unlike PICO-8, ATC-8 has higher specs and it's designed as an video game console emulator. That means that an user can create a game for it using only available assembly language. 

All the projects are written in C# using .NET Core 2.0. Also GUI module uses MonoGame framework.

*Please note: I do not understand assembly languages perfectly, moreover I have little knowledge about them, so I'm writting ATC-8's CPU assembly language, specification and documentation only using my experience in some OOP languages like C#, scripting languages like Lua and functional languages like Scala or F#. Also into that experience bag goes ASM lessons from my university.*

*Based on this, this repo is mostly for educational purposes*

## Project structure

**ATC-8** has the following project structure:

- `ATC-8.Common` - library with all basic functions shared between all modules
- `ATC-8.Cpu` - **CPU** module implementation
- `ATC-8.Ram` - **RAM** module implementation
- `ATC-8.InternalMemory` - **Internal memory** module implementation
- `ATC-8.Display` - **Display**, **Colors**, **Sprites** and **Map** modules implementation
- `ATC-8.Cartridge` - **Cartridge** module implementation
- `ATC-8.Sound` - **Sound** module implementation (sound synthesis & output)
- `ATC-8.Input` - **Input** module implementation (gamepad input)
- `ATC-8.Emulator` - Emulator, takes all the modules and composes an usable interface between them (bus)
- `ATC-8.Gui` - Graphical user interface for the emulator (VM)
- `ATC-8.Game` - Puzzle game based on the ATC-8


## Building

In order to build **ATC-8** you need to **.NET Core 2.0** or higher to be installed. Simply open the sln file using Visual Studio and you're ready to go. 

**ATC-8.Emulator** uses **MonoGame**, **MonoGame.Extended** and **lunge** frameworks.


## More Info

You can get a better perspective of the ATC-8 looking at the docs which are placed in the `Docs` directory.