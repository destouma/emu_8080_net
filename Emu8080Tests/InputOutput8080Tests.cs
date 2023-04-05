using Microsoft.VisualStudio.TestTools.UnitTesting;
using Emu8080;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emu8080.Tests
{
    [TestClass()]
    public class InputOutput8080Tests
    {

        [TestMethod()]
        public void InTest1()
        {
            InputOutput8080 io = new InputOutput8080();
            Assert.AreEqual((byte)0x0f, io.In(0));
        }

/*        [TestMethod()]
        public void InTest2()
        {
            InputOutput8080 io = new InputOutput8080();
            io.inPort1 = 0x01;
            Assert.AreEqual((byte)0x01, io.In(1));
        }*/

        [TestMethod()]
        public void InTest3()
        {
            InputOutput8080 io = new InputOutput8080();
            Assert.AreEqual((byte)0x00, io.In(2));
        }
/*       [TestMethod()]
        public void InTest4()
        {
            InputOutput8080 io = new InputOutput8080();
            io.shift0 = 0x01;
            io.shift1 = 0x02;
            io.shiftOffset = 0x03;
            Assert.AreEqual((byte)0x00, io.In(3));
        }*/
    }
}