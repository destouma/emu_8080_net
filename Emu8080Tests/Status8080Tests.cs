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
    public class Status8080Tests
    {
        //[TestMethod()]
        //public void Status8080Test()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void CalcFlagZeroTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void CalcFlagSignTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void CalcFlagParityTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void CalcFlagCarryTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void CalcFlagAuxCarryTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void CalcLogicFlagsTest()
        //{
        //    Assert.Fail();
        //}

        [TestMethod()]
        public void GetPSWTest()
        {
            Status8080 status = new Status8080();

            Assert.AreEqual( (byte)0x00, status.GetPSW());
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
    }
}