# ATC-8

## Overview

**ATC-8** (Advanced Toy 8-bit Computer) itself is a virtual machine designed to mimic a non-existing video game console. Unlike PICO-8, ATC-8 has higher specs and it's designed as an video game console emulator. That means that an user can create a game for it using only available assembly language. 

All the projects are written in C# using .NET Core 2.0. Also GUI module uses MonoGame framework.

*Please note: I do not understand assembly languages perfectly, moreover I have little knowledge about them, so I'm writting ATC-8's CPU assembly language, specification and documentation only using my experience in some OOP languages like C#, scripting languages like Lua and functional languages like Scala or F#. Also into that experience bag goes ASM lessons from my university.*

## Specifications

**ATC-8** has all the components that a video game console needs and the console is comporable to NES:

- **CPU:** 16-bit ATC-1801 (yyMm, yy - last two numbers of the current year, M - major version, m - minor version). The CPU uses AASM.
- **RAM:** 32KB (256Kb), 32KB video RAM (including sprite bank)
- **Display:** 256x256 pixels
- **Colors:** 64 colors palette
- **Cartridge size:** 32K (16K for sprites, 16K for code & other data)
- **Sound:** 4 channel, 64 definable chip blerps
- **Input:** Up to 2 8-button controllers
- **Sprites:** Single bank of 128 8x8 sprites
- **Map:** 8-bit cells, size of map is unlimited
- **Internal memory:** 16KB for OS and game saves
- **Code:** AASM Assembly Language

**Colors** have alpha, red, green and blue channels. Colors can be defined by using hexadecimal notation in format **RRGGBBAA**, where A - alpha channel (FF - completely transparent, 0 - vice versa), R - red channel, G - green channel, B - blue channel. Color palette can contain *any* combinations of the channel, but its amount is limited to 64. If a color is getting out of bounds, it replaces the first color.

16K of **sprites** can be manually replaced by any other data using configuration.

**Sound** and **Input** specs are listed below.

**Sprites bank** is always available from anywhere as it is contains in the video RAM. If sprite size or sprite bank length exceeds its maximum values, behavior is undefined.

**Map** size is only limited to RAM.

**Internal memory** is free for reading and writing from any place.

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

## ATC-1801

**ATC-1801** is a CPU that is used by the ATC-8. It is significantly easier to program in that is a real assembly language (line the one for i386 CPU), but in other way it may be harder to use than some habitual high-level languages like Java or C#. 

### Registers table:

| Register(s) | Description | Access Rights |
| --- | --- | --- |
| ax, bx, cx, dx, acx, bcx | Common registers that can be directly be read/written from anywhere | Internal: RW, External: RW |
| jd (jump data) | gets or sets jump data | Internal: RW, External: RW |
| sp (stack pointer) | gets or sets the current stack pointer | Internal: RW, External: RW |
| bp (base pointer)  | gets or sets the current base pointer | Internal: RW, External: RW |
| ex (extra/excess) | gets either extra or excess | Internal: RW, External: R |
| ia (interrupt address) | gets the last interruption | Internal: RW, External: R |
| pc (programm counter) | gets the current instruction's index | Internal: RW, External: R |
| dp (debug pointer )| contains pointer to the next debug point in a program | Internal: RW, External: NONE |

Every register is 2 bytes long.

## Assembly Language Documentation

The registers can store any data or addresses. If you want to store an address in the memory, you should use square brackets and place your address within it, for example: ```mov 15, [0x0100]``` writes the number *15* into the *0x0100* memory position. You can use decimal, heximal or binary numbers:

- ```0b010101``` - binary
- ```0x00ff``` - heximal
- ```129``` - decimal

Every memory addres has 16 bit offset, that means when you're getting and writting to address [0x100] value 0b1111000000000001 (16 bit), the memory at position [0x100] will have this data:
```
Hexadecimal version:
_________________
_0_|_F_0_0_1_|_0_
   |         |
[0x100]   [0x101]
```
Normally the primary data types are **WORD** (2 bytes long) and **STR** (first byte is size of the string, next N bytes are ASCII characters 1 byte long), but user can define their own type using specs that are listed below.

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

The semicolon symbol is used for commenting code. All line after a semicolon is ignored by the parser.

### Examples

#### Bank 0

```nasm
.bank 0     ; set the current bank to 0
.org 0x8000 ; goto location 0x8000

; program's code would go here.
```

#### Bank 1

```nasm
.bank 1
.org 0xFFFA

dvar 0        ; location of NMI interrupt
dvar Start    ; code to run at reset, we give address of Start label that
dvar 0        ; location of VBlank interrupt
```

#### Bank 2

```nasm
.bank 2
.org 0x0000

.incbin "main.bkg"      ; include background data
.incbin "main.spr"      ; include sprite data
```

### Basic Opcodes

| Code | Command | Description |
| --- | --- | --- |
| `0x00` | `dvr a [, defaultval]` | defines variable **a** with optional parameter **defaultvar** |
| `0x01` | `set a, b` | sets **a** to **b** |
| `0x02` | `add a, b` | sets **a** to **a + b**, sets **EX** to `0x0001` if there's an overflow, `0x0` otherwise |
| `0x03` | `sub a, b` | sets **a** to **a - b,** sets **EX** to `0xffff` if there's and underflow, `0x0` otherwise |
| `0x04` | `mul a, b` | sets **a** to **a * b**, sets **EX** to `((a*b)>>16)&0xffff` (treats **a**, **b** as unsigned)|
| `0x05` | `mli a, b` | like `mul`, but treat **a**, **b** as signed
| `0x06` | `div a, b` | sets **a** to **a/b**, sets **EX** to `((a<<16)/b)&0xffff`. if **b==0**, sets **a** and **EX** to 0 instead (treats **a, b** as unsigned)
| `0x07` | `dvi a, b` | like `div`, but treat **a, b** as signed. rounds towards 0 |
| `0x08` | `mod a, b` | sets **a** to **a%b**. if **b==0**, sets **a** to 0 instead |
| `0x09` | `mdi a, b` | like `mod`, but treat **a**, **b** as signed (`mdi -7, 16 == -7`) |
| `0x0A` | `and a, b` | sets **a** to `a&b` |
| `0x0B` | `bor a, b` | sets **a** to `a\|b` |
| `0x0C` | `xor a, b` | sets **a** to `a^b` |
| `0x0D` | `shr a, b` | sets **a** to `a>>b`, sets **EX** to `((a<<16)>>b)&0xffff` (logical shift) |
| `0x0E` | `asr a, b` | sets **a** to `a>>b`, sets **EX** to `((a<<16)>>>b)&0xffff` (arithmetic shift) (treats a as signed) |
| `0x0F` | `shl a, b` | sets **a** to `a<<b`, sets **EX** to `((a<<b)>>16)&0xffff` |
| `0x10` | `ifb a, b` | performs next instruction only if `(a&b)!=0` |
| `0x11` | `ifc a, b` | performs next instruction only if `(a&b)==0` |
| `0x12` | `ife a, b` | performs next instruction only if `a==b` |
| `0x13` | `ifn a, b` | performs next instruction only if `a!=b` |
| `0x14` | `ifg a, b` | performs next instruction only if `a>b` |
| `0x15` | `ifa a, b` | performs next instruction only if `a>b` (signed) |
| `0x16` | `ifl a, b` | performs next instruction only if `a<b` |
| `0x17` | `ifu a, b` | performs next instruction only if `a<b` (signed) |
| `0x18` | - | reserved |
| `0x19` | - | reserved |
| `0x1A` | `adx a, b` | sets **a** to **a+b+EX**, sets **EX** to `0x0001` if there is an overflow, `0x0` otherwise |
| `0x1B` | `sbx a, b` | sets **a** to **a-b+EX**, sets **EX** to `0xFFFF` if there is an underflow, `0x0` otherwise |
| `0x1C` | `sti a, b` | sets **a** to **b**, then increases **SP** and **BP** by one |
| `0x1D` | `std a, b` | sets **a** to **b**, then decreases **SP** and **BP** by one |
| `0x1E` | - | reserved |
| `0x1F` | - | reserved |
| `0x20` | `jsr a` | pushes the address of the next instruction to the stack, then sets **PC** to **a** |
| `0x21` | `int a` | triggers a software interrupt with message **a** |
| `0x22` | `iag a` | sets **a** to **IA** |
| `0x23` | `ias a` | sets **IA** to **a** |
| `0x24` | `rfi a` | disables interrupt queueing, pops a from stack, then pops **PC** from the stack |
| `0x25` | `iaq a` | if **a** is nonzero, interrupts will be added to the queue instead of triggered. if **a** is zero, interrupts will be triggered as normal again |
| `0x26` | `inc a` | increments **a** by one and sets result to **AX** |
| `0x27` | `dec a` | decrements **a** by one and sets result to **AX** |
| `0x28` | - | reserved |
| `0x29` | - | reserved |
| `0x2A` | - | reserved |
| `0x2B` | - | reserved |
| `0x2C` | - | reserved |
| `0x2D` | - | reserved |
| `0x2E` | - | reserved |
| `0x2F` | - | reserved |
| `0x30` | `jmp a` | transfers execution uncoditionally to label **a** |
| `0x31` | `jgx a` | transfers execution to label **a** if **JD>=0** |
| `0x32` | `jlx a` | transfers execution to label **a** if **JD<=0** |
| `0x33` | `jex a` | transfers execution to label **a** if **JD==0** |
| `0x34` | `jsg a` | transfers execution to label **a** if **JD>0** |
| `0x35` | `jsl a` | transfers execution to label **a** if **JD<0** |
| `0x36` | `jne a` | transfers execution to label **a** if **JD!=0** |
| `0x37` | `jti a` | transfers execution to index **a**. **a** can be either negative or positive. if **a** is 0, do nothing |

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
