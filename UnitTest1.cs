using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SD_125_W22SD_Lab2.Data;
using SD_125_W22SD_Lab2.Models;


namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {

        public UnitTest1()
        {

            var testData = new List<Pass> {
                new Pass{ID = 1, Capacity = 3, Premium = false, Purchaser = "Lin", Vehicles = new List<Vehicle>() },
            }.AsQueryable();

            var mockPassSet = new Mock<DbSet<Pass>>();
            
            mockPassSet.As<IQueryable<Pass>>().Setup(m => m.Provider).Returns(testData.Provider);
            mockPassSet.As<IQueryable<Pass>>().Setup(m => m.Expression).Returns(testData.Expression);
            mockPassSet.As<IQueryable<Pass>>().Setup(m => m.ElementType).Returns(testData.ElementType);
            mockPassSet.As<IQueryable<Pass>>().Setup(m => m.GetEnumerator()).Returns(testData.GetEnumerator());

            var mockContext = new Mock<ParkingContext>();
           // mockContext.Setup(c => c.Passes).Returns(mockPassSet.Object);
        }

        [TestMethod]
        public void CreatePassTest(ParkingHelper parkingHelper)
        {
            Pass pass = new Pass();
            pass.ID = 1;
            pass.Capacity = 3;
            pass.Premium = false;
            pass.Purchaser = "Lin";
            pass.Vehicles = new List<Vehicle>();

            Pass createdPass= parkingHelper.CreatePass("Lin", false, 3);

            Assert.AreEqual(pass, createdPass);
        }

        [DataRow("Lin",false,0)]
        [DataRow("Lin", false, -1)]
        [DataRow("Li",false,3)]
        [TestMethod]
        public void CreatePass_WrongArgument_ThrowException(string purchaser, bool premium, int capacity)
        {
            throw new Exception("The Purchaser argument is not between 3 and 20 characters long, or Capacity is zero or less");
        }

        [TestMethod]
        public void CreateParkingSpot(ParkingHelper parkingHelper)
        {
            ParkingSpot parkingSpot = new ParkingSpot();
            parkingSpot.ID = 1;
            parkingSpot.Occupied = false;
            parkingSpot.Reservations = new List<Reservation>();

            ParkingSpot createdParkingSpot = parkingHelper.CreateParkingSpot();
            Assert.AreEqual(parkingSpot, createdParkingSpot);
        }
    }
}



