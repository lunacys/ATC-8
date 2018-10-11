# ATC-8

**ATC-8** (Advanced Toy 8-bit Computer) is a virtual machine designed to mimic a non-existing video game console. Unlike PICO-8, ATC-8 has higher specs and it's designed as an video game console emulator. That means that an user can create a game for it using only available assembly language. 

All the projects are written in C# using .NET Core 2.0. Also GUI module uses MonoGame framework.

## Specifications

**ATC-8** has all the components that a video game console needs and the console is comporable to NES:

 - **CPU:** 8-bit ATC-1801 (yyMm, yy - last two numbers of the current year, M - major version, m - minor version). The CPU uses AASM.
 - **RAM:** 32KB (256Kb), 32KB video RAM
 - **Display:** 128x128 pixels
 - **Colors:** 32 colors palette
 - **Cartridge size:** 64k (28k for sprites, 36k for code & other data)
 - **Sound:** 4 channel, 64 definable chip blerps
 - **Input:** Up to 4 8-button controllers
 - **Sprites:** Single bank of 128 8x8 sprites
 - **Map:** 8-bit cells, size of map is unlimited
 - **Internal memory:** 64KB

## Project structure

**ATC-8** has the following project structure:

 - `ATC-8.Common` - library with all basic functions shared between all modules
 - `ATC-8.Cpu` - **CPU** module implementation
 - `ATC-8.Ram` - **RAM** module implementation
 - `ATC-8.InternalMemory` - **Internal memory** module implementation
 - `ATC-8.Display` - **Display**, **Colors**, **Sprites** and **Map** modules implementation
 - `ATC-8.Cartridge` - **Cartridge reader** module implementation
 - `ATC-8.Sound` - **Sound** module implementation (sound synthesis & output)
 - `ATC-8.Input` - **Input** module implementation (gamepad input)
 - `ATC-8.Emulator` - Emulator, takes all the modules and composes an usable interface between them (bus)
 - `ATC-8.Gui` - Graphical user interface for the emulator (VM)

## ATC-1801
```
 * 8 bit words
 * 0x10000 words of ram
 * 8 registers (A, B, C, X, Y, Z, I, J)
```