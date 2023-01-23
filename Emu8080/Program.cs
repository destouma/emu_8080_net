// See https://aka.ms/new-console-template for more information
using Emu8080;

Console.WriteLine("8080 CPU Emulator");

Emulator8080 emulator = new Emulator8080(new ConsoleEmulator8080Listener());
emulator.LoadFileInMemoryAt(0, "/Users/destouma/Development/PERSO/NETC#/Emu8080/Emu8080/invaders.bin");
while (emulator.Emulate()) ;