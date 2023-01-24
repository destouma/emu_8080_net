// See https://aka.ms/new-console-template for more information
using Emu8080;

Console.WriteLine("8080 CPU Emulator");

Emulator8080 emulator = new Emulator8080(new ConsoleEmulator8080Listener());
// PC Windows
emulator.LoadFileInMemoryAt(0, "C:\\Users\\Manuel DESTOUESSE\\Development\\PERSO\\emu_8080_net\\Emu8080\\invaders.bin");
// Mac
//emulator.LoadFileInMemoryAt(0, "/Users/destouma/Development/PERSO/NETC#/emu_8080_net/Emu8080/invaders.bin");
emulator.Run();