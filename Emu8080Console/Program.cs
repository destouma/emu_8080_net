// See https://aka.ms/new-console-template for more information
using Emu8080;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("8080 CPU Emulator");

        //Emulator8080 emulator = new Emulator8080(new LiteConsoleEmulator8080Listener());
        Emulator8080 emulator = new Emulator8080(new ConsoleEmulator8080Listener());

<<<<<<< HEAD
// PC Windows
//emulator.LoadFileInMemoryAt(0, "C:\\Users\\Manuel DESTOUESSE\\Development\\PERSO\\emu_8080_net\\Emu8080\\invaders.bin");
// Mac
emulator.LoadFileInMemoryAt(0, "/Users/destouma/Development/PERSO/EMULATORS/emu_8080_net/invaders.bin");
emulator.Run();
=======
        // PC Windows
        emulator.LoadFileInMemoryAt(0, "C:\\Users\\Manuel DESTOUESSE\\Development\\PERSO\\emu_8080_net\\Emu8080Library\\invaders.bin");
        // Mac
        //emulator.LoadFileInMemoryAt(0, "/Users/destouma/Development/PERSO/EMULATORS/emu_8080_net/Emu8080/invaders.bin");
        emulator.Run();
    }
}
>>>>>>> 95e375cacdd8f55679b49bb431bd57dfd0f88cd4
