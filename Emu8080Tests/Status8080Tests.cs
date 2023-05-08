using Emu8080;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Emu8080.Tests
{
    [TestClass()]
    public class Status8080Tests
    {

        [TestMethod()]
        public void GetPSWTest()
        {
            Status8080 status = new Status8080();

            Assert.AreEqual((byte)0x00, status.GetPSW());
        }

        [TestMethod()]
        public void GetPSWTestSign()
        {
            Status8080 status = new Status8080();
            status.s = true;
            status.z = false;
            status.ac = false;
            status.p = false;
            status.cy = false;

            Assert.AreEqual((byte)0x80, status.GetPSW());
        }

        [TestMethod()]
        public void GetPSWTestZero()
        {
            Status8080 status = new Status8080();
            status.s = false;
            status.z = true;
            status.ac = false;
            status.p = false;
            status.cy = false;

            Assert.AreEqual((byte)0x40, status.GetPSW());
        }

        [TestMethod()]
        public void GetPSWTestAuxCarry()
        {
            Status8080 status = new Status8080();
            status.s = false;
            status.z = false;
            status.ac = true;
            status.p = false;
            status.cy = false;

            Assert.AreEqual((byte)0x10, status.GetPSW());
        }

        [TestMethod()]
        public void GetPSWTestParity()
        {
            Status8080 status = new Status8080();
            status.s = false;
            status.z = false;
            status.ac = false;
            status.p = true;
            status.cy = false;

            Assert.AreEqual((byte)0x04, status.GetPSW());
        }

        [TestMethod()]
        public void GetPSWTestCarry()
        {
            Status8080 status = new Status8080();
            status.s = false;
            status.z = false;
            status.ac = false;
            status.p = false;
            status.cy = true;

            Assert.AreEqual((byte)0x01, status.GetPSW());
        }

        [TestMethod()]
        public void SetPSWTestSign()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0x80);
            Assert.IsTrue(status.s);
        }

        [TestMethod()]
        public void SetPSWTestZero()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0x40);
            Assert.IsTrue(status.z);
        }

        [TestMethod()]
        public void SetPSWTestAuxCarry()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0x10);
            Assert.IsTrue(status.ac);
        }

        [TestMethod()]
        public void SetPSWTestParity()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0x04);
            Assert.IsTrue(status.p);
        }

        [TestMethod()]
        public void SetPSWTestCarry()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0x01);
            Assert.IsTrue(status.cy);
        }

        // Decrement tests 0 
        [TestMethod()]
        public void DecrementTest00()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0x00);
            byte result = status.Decrement(0x01);
            Assert.AreEqual((byte)0x00, result);
        }

        [TestMethod()]
        public void DecrementTest01()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0x00);
            byte result = status.Decrement(0x01);
            Assert.IsTrue(status.z);
        }


        [TestMethod()]
        public void DecrementTest02()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0x00);
            byte result = status.Decrement(0x01);
            Assert.IsFalse(status.ac);
        }

        [TestMethod()]
        public void DecrementTest03()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0x00);
            byte result = status.Decrement(0x01);
            Assert.IsFalse(status.cy);
        }

        [TestMethod()]
        public void DecrementTest04()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0x00);
            byte result = status.Decrement(0x01);
            Assert.IsFalse(status.s);
        }

        // Decrement tests 1 
        [TestMethod()]
        public void DecrementTest10()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0x00);
            byte result = status.Decrement(0xff);
            Assert.AreEqual((byte)0xfe, result);
        }

        [TestMethod()]
        public void DecrementTest11()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0x00);
            byte result = status.Decrement(0xff);
            Assert.IsFalse(status.z);
        }


        [TestMethod()]
        public void DecrementTest12()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0x00);
            byte result = status.Decrement(0xff);
            Assert.IsFalse(status.ac);
        }

        [TestMethod()]
        public void DecrementTest13()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0x00);
            byte result = status.Decrement(0xff);
            Assert.IsFalse(status.cy);
        }

        [TestMethod()]
        public void DecrementTest14()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0x00);
            byte result = status.Decrement(0xff);
            Assert.IsTrue(status.s);
        }

        // Decrement test 2
        [TestMethod()]
        public void DecrementTest20()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0x00);
            byte result = status.Decrement(0x00);
            Assert.AreEqual((byte)0xff, result);
        }

        [TestMethod()]
        public void DecrementTest21()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0x00);
            byte result = status.Decrement(0x00);
            Assert.IsFalse(status.z);
        }


        [TestMethod()]
        public void DecrementTest22()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0x00);
            byte result = status.Decrement(0x00);
            Assert.IsFalse(status.ac);
        }

        [TestMethod()]
        public void DecrementTest23()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0x00);
            byte result = status.Decrement(0x00);
            Assert.IsFalse(status.cy);
        }

        [TestMethod()]
        public void DecrementTest24()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0x00);
            byte result = status.Decrement(0x00);
            Assert.IsTrue(status.s);
        }
        // Decrement test 3
        [TestMethod()]
        public void DecrementTest30()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0x00);
            byte result = status.Decrement(0x80);
            Assert.AreEqual((byte)0x7f, result);
        }

        //Add text
        [TestMethod()]
        public void AddTest()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0x00);
            byte result = status.Add(0x01, 0x01);
            Assert.AreEqual((byte)0x02, result);
        }

        [TestMethod()]
        public void AddTest2() { 
            Status8080 status = new Status8080();
            status.SetPSW(0x00);
            byte result = status.Add(0xfe, 0x01);
            Assert.AreEqual((byte)0xff, result);
        }

        [TestMethod()]
        public void AddTest3()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0x00);
            byte result = status.Add(0xfe, 0x05);
            Assert.AreEqual((byte)0x03, result);
        }

        [TestMethod()]
        public void AddTest4()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0x00);
            byte result = status.Add(0xff, 0x01);
            Assert.AreEqual((byte)0x00, result);
        }

    }
}