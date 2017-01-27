using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fractal1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractal1.Tests
{
    [TestClass()]
    public class AreaTests
    {
        [TestMethod()]
        public void AreaTest()
        {
            IArea area = new Area(new Cartesian(100, 300), new Cartesian(400, 200));

            Assert.AreEqual(300, area.Top);
            Assert.AreEqual(100, area.Left);
            Assert.AreEqual(200, area.Bottom);
            Assert.AreEqual(400, area.Right);

            Assert.AreEqual(100, area.Height);
            Assert.AreEqual(300, area.Width);
        }

        [TestMethod()]
        public void TransformTest()
        {
            IArea area = new Area(new Cartesian(-100, 100), new Cartesian(100, -100));

            area.Transform(new Cartesian(100, 100), new Cartesian(1, 1));
            Assert.AreEqual(200, area.Top);
            Assert.AreEqual(0, area.Left);
            Assert.AreEqual(0, area.Bottom);
            Assert.AreEqual(200, area.Right);

            area.Transform(new Cartesian(300, 0), new Cartesian(1, 1));
            Assert.AreEqual(100, area.Top);
            Assert.AreEqual(200, area.Left);
            Assert.AreEqual(-100, area.Bottom);
            Assert.AreEqual(400, area.Right);

            area.Transform(new Cartesian(-200, -200), new Cartesian(1, 1));
            Assert.AreEqual(-100, area.Top);
            Assert.AreEqual(-300, area.Left);
            Assert.AreEqual(-300, area.Bottom);
            Assert.AreEqual(-100, area.Right);

            area.Transform(new Cartesian(0, 0), new Cartesian(1.1, 1.1));
            Assert.IsTrue(area.Top > 90.90 && area.Top < 90.91);
            Assert.IsTrue(area.Left > -90.91 && area.Left < -90.90);
            Assert.IsTrue(area.Bottom > -90.91 && area.Bottom < -90.90);
            Assert.IsTrue(area.Right > 90.90 && area.Right < 90.91);

        }

        [TestMethod()]
        public void ProportionFromPointTest()
        {
            IArea area = new Area(new Cartesian(100, 150), new Cartesian(200, 50));
            ICartesian proportion = area.ProportionFromPoint(new Cartesian(130, 120));

            Assert.AreEqual(0.3, proportion.X);
            Assert.AreEqual(0.7, proportion.Y);
        }

        [TestMethod()]
        public void PointFromProportionTest()
        {
            IArea area = new Area(new Cartesian(100, 150), new Cartesian(200, 50));
            ICartesian point = area.PointFromProportion(new Cartesian(0.3, 0.7));

            Assert.AreEqual(130, point.X);
            Assert.AreEqual(120, point.Y);
        }
    }
}