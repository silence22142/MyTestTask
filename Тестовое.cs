using System.Text.RegularExpressions;
using System.Globalization;

namespace Тестовое
{
    [TestFixture]
    public class Tests
    {
        static List<string> ticketLines = File.ReadAllLines("..\\Receipt_006.txt").ToList();
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestDate()
        {
            var rawStr = Regex.Replace(ticketLines[2], @"\s+", "");
            var date = rawStr.Substring(2, 10);
            DateTime result;
            Assert.IsTrue(DateTime.TryParseExact(date, "d", new CultureInfo("ru-RU"), DateTimeStyles.None, out result));
        }

        [Test]
        public void TestStationIn()
        {
            var stationIn = Regex.Split(ticketLines[3], @"\s{2}");
            Assert.IsNotEmpty(stationIn[1]);
        }

        [Test]
        public void TestStationOut()
        {
            var stationOut = Regex.Split(ticketLines[4], @"\s{2}");
            Assert.IsNotEmpty(stationOut[1]);
        }

        [Test]
        public void TestTicketId()
        {
            var uniqueIds = Regex.Split(ticketLines[5], @"\s{3}");
            var ticketId = uniqueIds.First().Split(' ');
            Assert.IsTrue(Regex.IsMatch(ticketId[2], @"\d{5}"));
        }

        [Test]
        public void TestSerialNumber()
        {
            var uniqueIds = Regex.Split(ticketLines[5], @"\s{3}");
            var serialNumber = uniqueIds.Last().Split(' ');
            Assert.IsTrue(Regex.IsMatch(serialNumber[1], @"\d{13}"));
        }

        [Test]
        public void TestTicketInfo()
        {
            var info = ticketLines[6];
            Assert.IsTrue(Regex.IsMatch(info, @"Перевозка\sРазовый\s[А-Яа-я]+\s<*=*-*>\s[П,У]\s+\d+"));
        }

        [Test]
        public void TestPrice()
        {
            var rawPrice = ticketLines[7];
            var price = rawPrice.Split(' ').Last();
            Assert.IsTrue(Regex.IsMatch(price, @"=\d+.\d{2}"), "Неверный формат стоимости");
        }

        [Test]
        public void TestFinalResult()
        {
            var result = ticketLines[8];
            var finalPrice = result.Split(" ").Last();
            Assert.IsTrue(Regex.IsMatch(finalPrice, @"\d+,\d{2}"));
        }
    }
}