# ATC-8

**ATC-8** (Advanced Toy 8-bit Computer) is a virtual machine designed to mimic a non-existant video game console. Unlike PICO-8, ATC-8 has higher specs and it's designed as an video game console emulator. That means that an user can create a game for it using only available assembly language. 

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