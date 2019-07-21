# Standard opcodes

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