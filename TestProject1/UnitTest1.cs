using Xunit;
using Obligatorisk_oppgave_1;

namespace UnitTest1.Tests
{
    public class LexicanumTests
    {
        // TEST 1: Sjekker at man ikke kan registrere to kurs med samme kode
        [Fact]
        public void RegistrerKurs_ShouldThrowException_WhenDuplicateId()
        {
            var system = new Lexicanum(); 
            Assert.Throws<System.Exception>(() => system.RegistrerKurs(300, "New Course", 5, 10));
        }

        // TEST 2: Sjekker at studenter ikke kan meldes på samme kurs to ganger
        [Fact]
        public void TildelKurs_ShouldThrowException_IfAlreadyEnrolled()
        {
            var system = new Lexicanum();
            system.TildelKurs(2001, 300);
            // Prøver å melde Lyra (2001) på Ancient History (300) på nytt
            Assert.Throws<System.Exception>(() => system.TildelKurs(2001, 300));
        }

        // TEST 3: Sjekker at bok-lageret minker når noen låner
        [Fact]
        public void LånBok_ShouldDecreaseStock()
        {
            var system = new Lexicanum();
            string tittel = "Ancient Civilizations: A Comprehensive Guide."; 

            system.LånBok(2001, tittel);

            var bok = system.AlleBøker.First(b => b.Tittel == tittel);
            Assert.Equal(3, bok.AntallEksemplarer);
        }

        // TEST 4: Sjekker at systemet nekter lån hvis boken er tom (0 eks)
        [Fact]
        public void LånBok_ShouldThrowException_WhenOutOfStock()
        {
            var system = new Lexicanum();
            // "The History of Abominable Intelligence" har bare 1 kopi i koden din
            string tittel = "The History of Abominable Intelligence.";

            system.LånBok(2001, tittel); 

            // Neste person som prøver skal få feil
            Assert.Throws<System.Exception>(() => system.LånBok(2002, tittel));
        }
    }
}