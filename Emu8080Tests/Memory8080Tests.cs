using Microsoft.VisualStudio.TestTools.UnitTesting;
using Emu8080;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emu8080.Tests{
    [TestClass()]
    public class Memory8080Tests
    {
        [TestMethod()]
        public void WriteByteInRamAtTest()
        {
            Memory8080 memory = new Memory8080();
            Assert.IsTrue(memory.WriteByteInRamAt(0x2000, 0x01));
        }
        [TestMethod()]

        public void WriteByteInRamAtTest2()
        {
            Memory8080 memory = new Memory8080();
            Assert.IsFalse(memory.WriteByteInRamAt(0x1fff, 0x01));
        }

        [TestMethod()]
        public void WriteByteInRamAtTest3()
        {
            Memory8080 memory = new Memory8080();
            Assert.IsFalse(memory.WriteByteInRamAt(0x4000, 0x01));
        }

        [TestMethod()]
        public void WriteByteInRamAtTest4()
        {
            Memory8080 memory = new Memory8080();
            memory.WriteByteInRamAt(0x2000, 0x01);
            Assert.AreEqual( (byte)0x01, memory.ReadByteFromMemoryAt(0x2000));
        }

        [TestMethod()]  
        public void InitRomFromBufferTest()
        {
            Memory8080 memory = new Memory8080();
            byte[] buffer = new byte[0x1000];
            for (int i = 0; i < 0x1000; i++)
            {
                buffer[i] = 0x01;
            }
            Assert.IsTrue(memory.InitRomFromBuffer(0x2000, buffer));
        }

        [TestMethod()]      
        public void GetVideoRamTest()
        {
            Memory8080 memory = new Memory8080();
            byte[] buffer = new byte[0x1000];
            for (int i = 0; i < 0x1000; i++)
            {
                buffer[i] = 0x01;
            }
            Assert.IsTrue(memory.InitRomFromBuffer(0x2000, buffer));
            byte[] videoRam = memory.GetVideoRam();
            Assert.AreEqual(0x01, videoRam[0]);
        }

        

    }
}