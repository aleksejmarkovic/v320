using M320_SmartHome;

namespace SmartHomeSimulation.Test
{
    [TestClass]
    public class TestJalousiesteuerung
    {
        [TestMethod]
        public void TestHoehereAussentemp_true()
        {
            // Arrange
            int zeitdauerMinuten = 30;

            var wettersensor = new WettersensorMock(15, 35, true);
            var wohnung = new Wohnung(wettersensor);

            wohnung.SetTemperaturvorgabe("Küche", 30);
            wohnung.SetPersonenImZimmer("Küche", false);

            wohnung.GenerateWetterdaten(zeitdauerMinuten);

            // Act
            var kueche = wohnung.GetZimmer<ZimmerMitJalousiesteuerung>("Küche");

            // Assert
            Assert.AreEqual(kueche.JalousieHeruntergefahren, true);
        }

        [TestMethod]
        public void TestTiefereAussentemp_false()
        {
            // Arrange
            int zeitdauerMinuten = 30;

            var wettersensor = new WettersensorMock(25, 35, true);
            var wohnung = new Wohnung(wettersensor);

            wohnung.SetTemperaturvorgabe("Küche", 30);
            wohnung.SetPersonenImZimmer("Küche", false);

            wohnung.GenerateWetterdaten(zeitdauerMinuten);

            // Act
            var kueche = wohnung.GetZimmer<ZimmerMitJalousiesteuerung>("Küche");

            // Assert
            Assert.AreEqual(kueche.JalousieHeruntergefahren, false);
        }

        [TestMethod]
        public void TestPersonImRaum_false()
        {
            // Arrange
            int zeitdauerMinuten = 30;

            var wettersensor = new WettersensorMock(45, 35, true);
            var wohnung = new Wohnung(wettersensor);

            wohnung.SetTemperaturvorgabe("Küche", 30);
            wohnung.SetPersonenImZimmer("Küche", false);

            wohnung.GenerateWetterdaten(zeitdauerMinuten);

            // Act
            var kueche = wohnung.GetZimmer<ZimmerMitJalousiesteuerung>("Küche");

            // Assert
            Assert.AreEqual(kueche.JalousieHeruntergefahren, true);
        }

        // working
        [TestMethod]
        public void TestHoehereTempPersonImRaum_false()
        {
            // Arrange
            int zeitdauerMinuten = 30;

            var wettersensor = new WettersensorMock(15, 35, true);
            var wohnung = new Wohnung(wettersensor);

            wohnung.SetTemperaturvorgabe("Küche", 30);
            wohnung.SetPersonenImZimmer("Küche", true);

            wohnung.GenerateWetterdaten(zeitdauerMinuten);

            // Act
            var kueche = wohnung.GetZimmer<ZimmerMitJalousiesteuerung>("Küche");

            // Assert
            Assert.AreEqual(kueche.JalousieHeruntergefahren, false);
        }
    }
}