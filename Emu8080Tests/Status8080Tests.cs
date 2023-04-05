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

        [TestMethod()]
        public void SetPSWTestAll()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0x95);
            Assert.IsTrue(status.s);
            Assert.IsTrue(status.z);
            Assert.IsTrue(status.ac);
            Assert.IsTrue(status.p);
            Assert.IsTrue(status.cy);
        }
        [TestMethod()]
        public void SetPSWTestAll2()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0x2A);
            Assert.IsFalse(status.s);
            Assert.IsFalse(status.z);
            Assert.IsFalse(status.ac);
            Assert.IsFalse(status.p);
            Assert.IsFalse(status.cy);
        }

        [TestMethod()]
        public void SetPSWTestAll3()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0x00);
            Assert.IsFalse(status.s);
            Assert.IsFalse(status.z);
            Assert.IsFalse(status.ac);
            Assert.IsTrue(status.p);
            Assert.IsFalse(status.cy);
        }
        [TestMethod()]
        public void SetPSWTestAll4()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0x7F);
            Assert.IsFalse(status.s);
            Assert.IsFalse(status.z);
            Assert.IsFalse(status.ac);
            Assert.IsTrue(status.p);
            Assert.IsFalse(status.cy);
        }

        [TestMethod()]  
        public void SetPSWTestAll5()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0xFF);
            Assert.IsTrue(status.s);
            Assert.IsFalse(status.z);
            Assert.IsFalse(status.ac);
            Assert.IsTrue(status.p);
            Assert.IsFalse(status.cy);
        }
        [TestMethod()]
        public void SetPSWTestAll6()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0x80);
            Assert.IsTrue(status.s);
            Assert.IsFalse(status.z);
            Assert.IsFalse(status.ac);
            Assert.IsTrue(status.p);
            Assert.IsFalse(status.cy);
        }

        [TestMethod()]
        public void SetPSWTestAll7()
        {
            Status8080 status = new Status8080();
            status.SetPSW(0xFF);
            Assert.AreEqual((byte) 0xff, status.GetPSW());
        }
    }
}