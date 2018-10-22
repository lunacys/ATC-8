# ATC-8

**ATC-8** (Advanced Toy 8-bit Computer) is a virtual machine designed to mimic a non-existing video game console. Unlike PICO-8, ATC-8 has higher specs and it's designed as an video game console emulator. That means that an user can create a game for it using only available assembly language. 

All the projects are written in C# using .NET Core 2.0. Also GUI module uses MonoGame framework.

## Specifications

**ATC-8** has all the components that a video game console needs and the console is comporable to NES:

- **CPU:** 8-bit ATC-1801 (yyMm, yy - last two numbers of the current year, M - major version, m - minor version). The CPU uses AASM.
- **RAM:** 2KB (16Kb), 2KB video RAM
- **Display:** 256x256 pixels
- **Colors:** 32 colors palette
- **Cartridge size:** 64k (28k for sprites, 36k for code & other data)
- **Sound:** 4 channel, 64 definable chip blerps
- **Input:** Up to 2 8-button controllers
- **Sprites:** Single bank of 128 8x8 sprites
- **Map:** 8-bit cells, size of map is unlimited
- **Internal memory:** 16KB
- **Code:** ATCPU Assembly

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
 * 16 bit words
 * 0x10000 words of ram
 * 8 registers (ax, bx, cx, dx, si, di, sp, bp) which can be separated to 8-bit versions using aliases (ah-al, bh-bl, etc) (a higher - a lower)
 * stack pointer (SP)
 * keys pressed (KP)
 * keys unpressed (KU)
```

## Assembly Language Documentation

The registers can store any data or addresses. If you want to store an address in the memory, you should use square brackets and place your address within it, for example: ```mov 15, [0x0100]``` writes the number *15* into the *0x0100* memory position. You can use decimal, heximal or binary numbers:
- ```0b010101``` - binary
- ```0x00ff``` - heximal
- ```129``` - decimal 

There are three banks of data used in ATC:
- **Bank 0** - Assembly language code
- **Bank 1** - Interrupt table
- **Bank 2** - Sprite and background data

You must set the location of each bank in order to use it using the following command: ```.bank <[0;2]>```.
The default values are:
- **Bank 0** - [0x8000]
- **Bank 1** - [0xFFFA]
- **Bank 2** - [0x0000]

If you want to transfer execution to the bank order, use ```.org <bank_pos>```. The bank position is stored in variable __BANKN[0;2]. Sample: ```.org [0x8000]``` or ```.org __BANKN0```.

The semicolon symbol is used for commenting code. All line after a semicolon is ignored by the interpreter.

#### Examples

##### Bank 0

```csharp
.bank 0     ; set the current bank to 0
.org 0x8000 ; goto location 0x8000

; program's code would go here.
```

##### Bank 1

```csharp
.bank 1
.org 0xFFFA

dw 0        ; location of NMI interrupt
dw Start    ; code to run at reset, we give address of Start label that
dw 0        ; location of VBlank interrupt
```

##### Bank 2

```csharp
.bank 2
.org 0x0000

.incbin "main.bkg"      ; include background data
.incbin "main.spr"      ; include sprite data
```

### Opcodes

- ```set a, b``` - sets *a* to *b*
- ```get a, b``` - gets the value of *a* and writes it to *b*
- ```mov a, b``` - moves the value of *a* to *b*
- ```xch a, b``` - exchanges the values of *a* and *b*
- ```db <[a] | a> [, defaultval]``` - defines a byte for either address *[a]* or variable *a* with default value as an optional parameter
- ```jmp <label>``` - transfer execution uncoditionally to a label
- ```jgx <label>``` - transfer execution conditionally to a label if value of *ax* register is greater or equals zero (>=0).
- ```jlx <label>``` - transfer execution conditionally to a label if value of *ax* register is less or equals zero (<=0).
- ```jex <label>``` - transfer execution conditionally to a label if value of *ax* register equals zero (==0).
- ```add a``` - adds value of variable *a*  to *ax* register and writes the result to it
- ```sub a``` - subtracts value of variable *a* and *ax* register and writes the result to it
- ```mul a``` - multiplies *a* with the value of the *ax* register and writes the result to it.
- ```div a``` - divides *a* with the value of the *ax* register and writes the result to it.

### Examples

#### Drawing a blue backgroung and a sprite moving to the right

```nasm
.bank 2
.org 0x0000

.incbin "main.spr"      ; include sprite data

; let's say that the sprite is placed at [0,0] and its size is 8x8 pixels

.bank 0                 ; set the current bank to 0
.org 0x8000             ; goto location 0x8000

; now writing the game code

dvar myspr, [0x0000]             ; allocate memory for the sprite (dvar - define variable)
dvar bkg, 0x0000FF    ; set the background color as blue
dvar x, 0x0000
dvar y, 0x0000 
dvar proceed, 0x0001
dvar reset, 0x0000

mov proceed, ax
mov reset, bx

begin:
    jnz kp, end         ; if any key is down (kp register is not zero), end the program 
    .move myspr, x, y
    jmp drawbkg
    .draw myspr, x, y    
    jmp begin           ; jump at the begin

drawbkg:
    .draw 

    jnz drawbkg

end:
    .dbug "exiting"
```
