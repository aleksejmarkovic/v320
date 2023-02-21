using M320_SmartHome;

namespace SmartHomeSimulation.Test
{
    [TestClass]
    public class TestMarkisesteuerung
    {
        [TestMethod]
        public void TestRegenWind_true()
        {
            // Arrange
            int zeitdauerMinuten = 30;

            var wettersensor = new WettersensorMock(15, 35, true);
            var wohnung = new Wohnung(wettersensor);

            wohnung.SetTemperaturvorgabe("Wintergarten", 30);
            wohnung.SetPersonenImZimmer("Wintergarten", false);

            wohnung.GenerateWetterdaten(zeitdauerMinuten);

            // Act
            var wintergarten = wohnung.GetZimmer<ZimmerMitMarkisensteuerung>("Wintergarten");

            // Assert
            Assert.AreEqual(wintergarten.MarkiseOffen, true);
        }

        [TestMethod]
        public void TestWind_false()
        {
            // Arrange
            int zeitdauerMinuten = 30;

            var wettersensor = new WettersensorMock(15, 35, false);
            var wohnung = new Wohnung(wettersensor);

            wohnung.SetTemperaturvorgabe("Wintergarten", 30);
            wohnung.SetPersonenImZimmer("Wintergarten", false);

            wohnung.GenerateWetterdaten(zeitdauerMinuten);

            // Act
            var wintergarten = wohnung.GetZimmer<ZimmerMitMarkisensteuerung>("Wintergarten");

            // Assert
            Assert.AreEqual(wintergarten.MarkiseOffen, false);
        }

        [TestMethod]
        public void TestHoehereAussentemp_false()
        {
            // Arrange
            int zeitdauerMinuten = 30;

            var wettersensor = new WettersensorMock(15, 20, false);
            var wohnung = new Wohnung(wettersensor);

            wohnung.SetTemperaturvorgabe("Wintergarten", 30);
            wohnung.SetPersonenImZimmer("Wintergarten", false);

            wohnung.GenerateWetterdaten(zeitdauerMinuten);

            // Act
            var wintergarten = wohnung.GetZimmer<ZimmerMitMarkisensteuerung>("Wintergarten");

            // Assert
            Assert.AreEqual(wintergarten.MarkiseOffen, false);
        }

        [TestMethod]
        public void TestHoehereTempKeinWindKeinRegen_true()
        {
            // Arrange
            int zeitdauerMinuten = 30;

            var wettersensor = new WettersensorMock(35, 35, false);
            var wohnung = new Wohnung(wettersensor);

            wohnung.SetTemperaturvorgabe("Wintergarten", 30);
            wohnung.SetPersonenImZimmer("Wintergarten", false);

            wohnung.GenerateWetterdaten(zeitdauerMinuten);

            // Act
            var wintergarten = wohnung.GetZimmer<ZimmerMitMarkisensteuerung>("Wintergarten");

            // Assert
            Assert.AreEqual(wintergarten.MarkiseOffen, true);
        }
    }
}