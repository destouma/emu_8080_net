8080 Emulator
-------------

8080 emulator based on the emulator 101 tutorial.

It's still work in progress.

The goal is to write a "full" 8080 emulator.

Enjoy !

$~$

Usefull resources
-----------------

* Emulator tutorial: 
  * http://www.emulator101.com/
* Spaces invaders code explained: 
  * http://computerarcheology.com/Arcade/SpaceInvaders/Code.html
* 8080 cpu
  * https://en.wikipedia.org/wiki/Intel_8080
* 8080 Assembler
  * https://altairclone.com/downloads/manuals/8080%20Programmers%20Manual.pdf
* Space invaders ROM:
  * [invaders.e](./invaders/invaders.e)
  * [invaders.f](./invaders/invaders.f)
  * [invaders.g](./invaders/invaders.g)
  * [invaders.h](./invaders/invaders.h)
* 8080 CPU diag adm and bin
  * [cupdiag.asm](./cpudiag/cpudiag.asm)
  * [cpudiag.bin](./cpudiag/cpudiag.bin)

$~$

Create invaders.bin : (in invaders folder)
------------------------------------------
```
$ cat invaders.h > invaders
$ cat invaders.g >> invaders
$ cat invaders.f >> invaders
$ cat invaders.e >> invaders
$
```

$~$

Build 8080 Disassembler and Emulator : (in v1 folder)
-------------------------------------------------------
```
$ make clean
rm -f 8080dis 8080emu
$ make all
gcc -g -Wall -o 8080dis 8080dis.c
gcc -g -Wall -o 8080emu 8080emu.c
$
```

$~$

Run the Disassembler : (in v1 folder)
---------------------------------------
```
$ ./8080dis ./invaders/invaders.bin
```

output
```
0000 00           NOP
0001 00           NOP
0002 00           NOP
0003 c3 d4 18     JMP     $18d4
0006 00           NOP
0007 00           NOP
0008 f5           PUSH    PSW
0009 c5           PUSH    B
000a d5           PUSH    D
000b e5           PUSH    H
000c c3 8c 00     JMP     $008c
000f 00           NOP
0010 f5           PUSH    PSW
0011 c5           PUSH    B
0012 d5           PUSH    D
0013 e5           PUSH    H
0014 3e 80        MVI     A,#$80
0016 32 72 20     STA     $2072
0019 21 c0 20     LXI     H,#$20c0
001c 35           DCR     M

......

1fe5 0b           DCX     B
1fe6 00           NOP
1fe7 18           NOP
1fe8 04           INR     B
1fe9 11 12 26     LXI     D,#$2612
1fec 1c           INR     E
1fed 26 02        MVI     H,#$02
1fef 0e 08        MVI     C,#$08
1ff1 0d           DCR     C
1ff2 12           STAX    D
1ff3 0f           RRC
1ff4 14           INR     D
1ff5 12           STAX    D
1ff6 07           RLC
1ff7 26 00        MVI     H,#$00
1ff9 08           NOP
1ffa 08           NOP
1ffb 08           NOP
1ffc 08           NOP
1ffd 08           NOP
1ffe 00           NOP
1fff 00           NOP
```

$~$

Run the Emulator : (in v1 folder)
-----------------------------------
```
$ ./8080emu
```

output
```
0000 00           NOP                   ........  A $00 B $00 C $00 D $00 E $00 H $00 L $00 SP 0000
0001 00           NOP                   ........  A $00 B $00 C $00 D $00 E $00 H $00 L $00 SP 0000
0002 00           NOP                   ........  A $00 B $00 C $00 D $00 E $00 H $00 L $00 SP 0000
0003 c3 d4 18     JMP   $18d4           ........  A $00 B $00 C $00 D $00 E $00 H $00 L $00 SP 0000
18d4 31 00 24     LXI   SP,#$2400       ........  A $00 B $00 C $00 D $00 E $00 H $00 L $00 SP 2400
18d7 06 00        MVI   B,#$00          ........  A $00 B $00 C $00 D $00 E $00 H $00 L $00 SP 2400
18d9 cd e6 01     CALL  $01e6           ........  A $00 B $00 C $00 D $00 E $00 H $00 L $00 SP 23fe
01e6 11 00 1b     LXI   D,#$1b00        ........  A $00 B $00 C $00 D $1b E $00 H $00 L $00 SP 23fe
01e9 21 00 20     LXI   H,#$2000        ........  A $00 B $00 C $00 D $1b E $00 H $20 L $00 SP 23fe
01ec c3 32 1a     JMP   $1a32           ........  A $00 B $00 C $00 D $1b E $00 H $20 L $00 SP 23fe
1a32 1a           LDAX  D               ........  A $01 B $00 C $00 D $1b E $00 H $20 L $00 SP 23fe
1a33 77           MOV   M,A             ........  A $01 B $00 C $00 D $1b E $00 H $20 L $00 SP 23fe
1a34 23           INX   H               ........  A $01 B $00 C $00 D $1b E $00 H $20 L $01 SP 23fe
1a35 13           INX   D               ........  A $01 B $00 C $00 D $1b E $01 H $20 L $01 SP 23fe
1a36 05           DCR   B               s....p..  A $01 B $ff C $00 D $1b E $01 H $20 L $01 SP 23fe
1a37 c2 32 1a     JNZ   $1a32           s....p..  A $01 B $ff C $00 D $1b E $01 H $20 L $01 SP 23fe
1a32 1a           LDAX  D               s....p..  A $00 B $ff C $00 D $1b E $01 H $20 L $01 SP 23fe
1a33 77           MOV   M,A             s....p..  A $00 B $ff C $00 D $1b E $01 H $20 L $01 SP 23fe
1a34 23           INX   H               s....p..  A $00 B $ff C $00 D $1b E $01 H $20 L $02 SP 23fe
1a35 13           INX   D               s....p..  A $00 B $ff C $00 D $1b E $02 H $20 L $02 SP 23fe
1a36 05           DCR   B               s.......  A $00 B $fe C $00 D $1b E $02 H $20 L $02 SP 23fe
1a37 c2 32 1a     JNZ   $1a32           s.......  A $00 B $fe C $00 D $1b E $02 H $20 L $02 SP 23fe
1a32 1a           LDAX  D               s.......  A $00 B $fe C $00 D $1b E $02 H $20 L $02 SP 23fe
1a33 77           MOV   M,A             s.......  A $00 B $fe C $00 D $1b E $02 H $20 L $02 SP 23fe

....

```
