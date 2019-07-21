# ATC-8

## Overview

**ATC-8** (Advanced Toy 8-bit Computer) itself is a virtual machine designed to mimic a non-existing video game console. Unlike PICO-8, ATC-8 has higher specs and it's designed as an video game console emulator. That means that an user can create a game for it using only available assembly language. 

All the projects are written in C# using .NET Core 2.0. Also GUI module uses MonoGame framework.

*Please note: I do not understand assembly languages perfectly, moreover I have little knowledge about them, so I'm writting ATC-8's CPU assembly language, specification and documentation only using my experience in some OOP languages like C#, scripting languages like Lua and functional languages like Scala or F#. Also into that experience bag goes ASM lessons from my university.*

## Specifications

**ATC-8** has all the components that a video game console needs and the console is comporable to NES:

- **CPU:** 16-bit ATC-1901 (yyMm, yy - last two numbers of the current year, M - major version, m - minor version). The CPU uses AASM.
- **RAM:** 32KB (256Kb), 32KB video RAM (including sprite bank) ([0x0000] - [0x3FFFF])
- **Display:** 256x256 pixels
- **Colors:** 64 colors palette
- **Cartridge size:** 32K (16K for sprites, 16K for code & other data)
- **Sound:** 4 channel, 64 definable chip blerps
- **Input:** Up to 2 8-button controllers
- **Sprites:** Single bank of 128 8x8 sprites
- **Map:** 8-bit cells, size of map is unlimited
- **Internal memory:** 16KB for OS and game saves
- **Code:** AASM Assembly Language

**RAM** and **Video RAM** can be changed at any time. The available methods to do that are listed below.

**Colors** have alpha, red, green and blue channels. Colors can be defined by using hexadecimal notation in format **AARRGGBB**, where A - alpha channel (FF - completely transparent, 0 - vice versa), R - red channel, G - green channel, B - blue channel. Color palette can contain *any* combinations of the channel, but its amount is limited to 64. If a color is getting out of bounds, it replaces the first color. Colors are stored in the **Video RAM** at range [0x0000]-[0x0080]. Alpha channel can be omited like this: 0xFFAACC (00 - Alpha, FF - Red, AA - Green, CC - Blue).

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

### Registers table

| Register(s) | Description | Access Rights |
| --- | --- | --- |
| ax, bx, cx, dx, acx, bcx | Common registers that can be directly be read/written from anywhere | Internal: RW, External: RW |
| jd (jump data) | gets or sets jump data | Internal: RW, External: RW |
| sp (stack pointer) | gets or sets the current stack pointer | Internal: RW, External: RW |
| bp (base pointer)  | gets or sets the current base pointer | Internal: RW, External: RW |
| ex (extra/excess) | gets either extra or excess | Internal: RW, External: R |
| ia (interrupt address) | gets the last interruption | Internal: RW, External: R |
| pc (program counter) | gets the current instruction's index | Internal: RW, External: R |
| dp (debug pointer )| contains pointer to the next debug point in a program | Internal: RW, External: NONE |

Every register is 2 bytes (16 bits) long.

#### Registers description

Every register was added in order to make every operation reaching only its own register and no other so the every register performs its own functions.

Below is description of every register.

##### Common registers

There are 6 common registers that can be written/read from anywhere: **ax**, **bx**, **cx**, **dx**, **acx** and **bcx**.

They can be used for any purpose, e.g. transfering data between sections.

##### Jump data

**Jump data** (**jd**) regiser is the primary way to handle if a jump operation must be proceeded. For example, instruction `jgx label` will be proceeded only if data stored in the **JD** register is greater or equal to 0.

It is free to be read and written from anywhere as any common register.

##### Stack pointer and Base pointer

Unlike other ASM languages, stack pointer is used in specific way. It returns pointer on the next element after the latest element of the stack. Example of stack usage:

```nasm
pop ax ; delete and write the latest element from the stack to ax register
psh bx ; push bx's value to the stack
peek cx ; write the latest element's value from the stack to cx register
clr [0x010] ; removes all the elements from the stack and writes count of all elements to address 0x010
```

Stack has unlimited size.

Base pointer (bp), in other hand, contains the base address of the stack.

##### Extra/excess

Extra/excess (EX) register helps getting info about operation overflowing or underflowing after the last executed operation. Mostly that is helpful after an arithmetic operation to determine if 16-bit value was overflowed.

##### Interrupt address

Interrupt address (IA) register contains the address of the last interruption that was performed.

##### Program counter

Program counter (PC) register contains the current instruction's index. It is essential for debugging as with the PC register you will know at which instruction there was an error.

##### Debug pointer

Debug pointer (DP) register is available for internal use only. It is utility register that helps debugger to know the next debug point in a program.

## Assembly Language Documentation

### CPU and data

The registers can store any data or addresses.
If you want to store an address in the memory, you should use square brackets and place your address within it,
for example: ```set 15, [0x0100]``` writes the number *15* into the *0x0100* memory position.
You can use decimal, heximal or binary numbers:

- ```0b010101``` - binary
- ```0x00ff``` - heximal
- ```129``` - decimal

Every memory address has 16 bit offset,
that means when you're getting and writting to address [0x100] value 0b1111000000000001 (16 bit),
the memory at position [0x100] will have this data:

```none
Hexadecimal version:
_________________
_0_|_F_0_0_1_|_0_
   |         |
[0x100]   [0x101]
```

In order to change offsets you can use `ofs` instuction.

If you need to write data in specific position excluding usage of offsets, you can use this syntax:

```nasm
set 15, [0x100$8] ; where 8 is 1 bit long offset
```

Memory dump will look like this (this is the way strings are written in):

```none
Hexadecimal version:
_________________
_0_|_0_0_F_0_|_0_
   |   |     |
[0x100]|  [0x101] OR [0x100$20]
       |
     [0x100$8]
```

*The behavior of overflowing depends on ASM configuration. This is listed below.*

Normally the primary data types are **WORD** (2 bytes long) and **STR** (first byte is size of the string, next N bytes are ASCII characters 1 byte long),
but user can define their own type using specs that are listed below.

Let's take a more detailed look at strings.
We'll take a sample string "EXAMPLES" and write it to [0x100]. It has size of 8 (0x08). ASCII values are:

```none
E - 0x45
X - 0x58
A - 0x41
M - 0x4D
P - 0x50
L - 0x4C
E - 0x45
S - 0x53
```

Thus memory dump will look like this:

```none
Hexadecimal version:
_________________________________________________________
   |  8   E  |  X   A  |  M   P  |  L   E  |  S   0  |
_________________________________________________________
_0_|_0_8_4_5_|_5_8_4_1_|_4_D_5_0_|_4_C_4_5_|_5_3_0_0_|_0_
   |         |         |         |         |         |
[0x100]   [0x101]   [0x102]   [0x103]   [0x104]   [0x105]
```

### Data Banks

There are three banks of data used in ATC:

- **Bank 0** - Assembly language code
- **Bank 1** - Interrupt table
- **Bank 2** - Sprite and background data

You can set the location of each bank in order to use it using the following command: ```.bank <[0;2]>```.

The stored default values are:

- **Bank 0** - [0x8000] - `__BANKN0`
- **Bank 1** - [0xFFFA] - `__BANKN1`
- **Bank 2** - [0x0000] - `__BANKN2`

These values can be changed using `set` command:

```nasm
set __BANKN0, [0x9000]
set __BANKN1, [0xFFFF]
set __BANKN2, [0x0000]
```

If you want to transfer execution to the bank order, use `.org <bank_pos>`. The bank position is stored in variable \_\_BANKN[0;2]. Sample: `.org [0x8000]` or `.org __BANKN0`.

The semicolon symbol is used for commenting code. All line after a semicolon is ignored by the parser.

### More detailed look at data types

#### Built-in data types

As mentioned above, there are two primary data types: WORD and STRING.

#### Defining your own type

You can freely define your own type using special syntax. You can describe this data:

| Data | Description | Required | Notes |
| --- | --- | --- | --- |
| Type name | Name of your type by which this data type can be used | YES | In type name you can use only alphanumeric characters and an underscore |
| Data size | Defines the size of your type | YES | If your type is simple fixed-size type, this step is easy. The dynamic-sized type examples can be found below |
| Memory mapping | How data of this type will be stored in RAM | NO | Normally it can be stored automatically, but your type may be stored incorrectly and thus throw an error |
| Register mapping | How data of this type will use registers | NO | When you use a register, you have EXTERNAL type of access |

##### Syntax overview

###### Defining a fixed-size type

Defining of types with fixed size is relatively easy.

```nasm
.inclib "std/typedef.ats"                   ; include type defining from the standard library

.typedef MYTYPE                             ; Defining type with name 'MYTYPE'
    .define DATASIZE FIXED, 8, BYTE         ; Defining data size of the type
    .contain X WORD, "RW", "RW"             ; Add field 'X' with internal and external access rights as RW (Read/Write)
    .contain Y WORD, "RW", "RW"
    .default X, 0                           ; Set default values for fields
    .default Y, 0
    .define MEMMAP, {  }
    .end
```

### Bank usage examples

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
| `0x04` | `mul a, b` | sets **a** to **a \* b**, sets **EX** to `((a*b)>>16)&0xffff` (treats **a**, **b** as unsigned)|
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

### Code Examples

#### A simple "game" example

This is complete example of making a simple game, where you can control a 8x8 square.

Used concepts:

 - Language basics
 - Using standard library
 - Custom types
 - Debugging
 - Using images
 - Using gamepad controls (in this case they are mapped to keyboard)
 - Complex language constructions

```nasm
.inclib "std"
.inclib "std/ge"
.inclib "std/types"

.bank 2
.org 0x0000

; included data goes to Video RAM
.incbin "main.bkg"      ; include background data
.incbin "main.spr"      ; include sprite data

; defining raw sprite data wrapper
; every sprite is 8x8 pixels wide
; every pixel is 16 bits (4 byte) long
; so the size of the SPRITE is 8*8*4=1024 bytes per sprite EXCLUDING properties
; there can be maximum of 128 sprites
.typedef SPRITE
    define DATASIZE, 1024
    
    prop X, (WORD)
    rigths X, RW
    prop Y, (WORD)
    rights Y, RW

    use MEMMAP, DEFAULT
    use REGMAP, NONE
.end

.bank 0     ; set the current bank to 0
.org 0x8000 ; goto location 0x8000



```
