# Test Code Examples

## A simple "game" example

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
