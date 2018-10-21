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

```csharp
 * 8 bit words
 * 0x10000 words of ram
 * 8 16-bit registers (ax, bx, cx, dx, si, di, sp, bp) which can be separated to 8-bit versions using aliases (ah-al, bh-bl, etc) (a higher - a lower)
 * program counter (PC)
 * stack pointer (SP)
 * extra/excess (EX)
 * interrupt address (IA)
```

## Assembly Language Documentation

### Opcodes

- ```set a, b``` - sets a to b
- ```mov a, b``` - moves the value of a to b
- ```xch a, b``` - exchanges the values of a and b
- ```db <[a] | a> [, defaultval]``` - defines a byte for either address *[a]* or variable *a* with default value as an optional parameter
- ```jmp <label>``` - transfer execution uncoditionally to a label
- ```jgx <label>``` - transfer execution conditionally to a label if value of *ax* register is greater or equals zero (>=0).
- ```jlx <label>``` - transfer execution conditionally to a label if value of *ax* register is less or equals zero (<=0).
- ```jex <label>``` - transfer execution conditionally to a label if value of *ax* register equals zero (==0).
- ```add a``` - adds value of variable *a*  to *ax* register and writes the result to it
- ```sub a``` - subtracts value of variable *a* and *ax* register and writes the result to it

### Examples

```csharp
begin:
    db a, 0     ; defines a byte for variable a filled with zeros
    mov ax, a   ; moves a value in acx register to variable a

```
