using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Obligatorisk_oppgave_1;

/// <summary>
/// AI brukt for Feilsøking under koding samt anbefallinger og informasjons søking om anbefaling.
/// koden er planlagt og skrevet i forhold til Oppgaveteksten.
/// 
/// Tillegg: Jeg simplifiserte koden i forhold til ID ettersom jeg prøvde å få det til å fungere på en annen måte først.
/// Forsøke bestod av å bruke kode som skulle legge til S for student før ID, (og L for lærer) dette endte opp med å ikke fungere som jeg ønsket, og jeg endte opp med å bruke int for ID, og heller legge til 1000 for lærere, og 2000 for studenter. 
/// Dette fungerte bedre, og var enklere å implementere i forhold til å finne brukere basert på ID senere i koden.
/// 
/// 
/// </summary>

public class Lexicanum
{
    public List<Kurs> AlleKurs = new List<Kurs>();
    public List<Bruker> AlleBrukere = new List<Bruker>();
    public List<Bok> AlleBøker = new List<Bok>();
    public List<Ansatt> AlleLærere = new List<Ansatt>();
    public List<Student> AlleStudenter = new List<Student>();
    public List<string> KarakterOversikt = new List<string>();
    public List<string> UtlånsHistorikk = new List<string>();

    public Lexicanum()
    {
        // Kurs - (int kode, string navn, int poeng, int maksStudenter)
        RegistrerKurs(300, "Ancient History", 5, 30);
        RegistrerKurs(400, "Abominable Intelligence History", 10, 45);


        // Ansatt - (int id, string navn, string epost, string passord, string stilling, string avdeling)
        RegistrerAnsatt(1001, "Magnus Aurelius", "Aurelius4Evar@Teach.com", "123", "Faglærer", "Foreleser");
        AlleBrukere.Add(AlleLærere.Last());
        RegistrerAnsatt(1002, "Octavia Voss", "Voss@Teach.com", "123", "Bibliotekar", "Bibliotek");
        AlleBrukere.Add(AlleLærere.Last());
        RegistrerAnsatt(1003, "Lucius Blackwood", "LuciBlack@Teach.com", "123", "Faglærer", "Administrasjon");
        AlleBrukere.Add(AlleLærere.Last());


        // Studenter - (int id, string navn, string epost, string passord)
        RegistrerStudent(2001, "Acolyte Lyra", "Lyr@Stud.Teach.com", "321");
        AlleBrukere.Add(AlleStudenter.Last());
        RegistrerStudent(2002, "Acolyte Orion", "Orion@Stud.Teach.com", "321");
        AlleBrukere.Add(AlleStudenter.Last());
        RegistrerStudent(2003, "Acolyte Selene", "Selen@Stud.Teach.Com", "321");
        AlleBrukere.Add(AlleStudenter.Last());

        // Utvekslingsstudent - (int id, string navn, string epost, string passord, string hjemUni, string land, string periode)
        RegistrerUtvekslingsStudent(3001, "Acolyte Nova", "Nova@stud.teach.com", "321", "University of Eldoria", "Eldoria", "2024-2025");
        AlleBrukere.Add(AlleStudenter.Last());


        //Bøker - (string tittel, string forfatter, string utgivelsesår, int isbn, int antallEksemplarer)
        RegistrerBok("The History of Abominable Intelligence.", "Aurelius M", "12.08.90", 129834, 1);
        RegistrerBok("Ancient Civilizations: A Comprehensive Guide.", "Dr. Magnus Aurelius", "05.03.85", 987654, 4);
        RegistrerBok("The Rise and Fall of the Roman Empire.", "Aurelius M", "22.11.88", 123456, 5);
        RegistrerBok("Medieval Europe: A Historical Overview.", "Dr. Magnus Aurelius", "15.07.92", 654321, 3);

        //Karakterer - (int studentId, int kursKode, string resultat)
        SettKarakter(2001, 300, "A");
        SettKarakter(2002, 300, "B");
        SettKarakter(2003, 300, "C");
        SettKarakter(2001, 400, "B");


    }

    //Registreringsfunksjoner for å legge til data i listene. Brukes i konstruktøren, og kan også brukes senere for å legge til mer data.
    public void RegistrerKurs(int kode, string navn, int poeng, int maks)
    {
        bool eksisterer = AlleKurs.Any(k => k.Kode == kode || k.Navn.Equals(navn, StringComparison.OrdinalIgnoreCase));

        if (eksisterer)
        {
            throw new Exception($">>>ERROR<<< Protocol {kode} already exists in the archives!");
        }
        Kurs nyttKurs = new Kurs(kode, navn, poeng, maks);
        AlleKurs.Add(nyttKurs);
    }

    public void RegistrerAnsatt(int id, string navn, string epost, string passord, string stilling, string avdeling)
    {
        Ansatt nyLærer = new Ansatt(id, navn, epost, passord, stilling, avdeling);
        AlleLærere.Add(nyLærer);
        AlleBrukere.Add(nyLærer);
    }

    public void RegistrerStudent(int id, string navn, string epost, string passord)
    {
        Student nyStudent = new Student(id, navn, epost, passord);
        AlleStudenter.Add(nyStudent);
        AlleBrukere.Add(nyStudent);
    }

    public void RegistrerBok(string tittel, string forfatter, string utgivelsesår, int isbn, int antallEksemplarer)
    {
        Bok nyBok = new Bok(tittel, forfatter, utgivelsesår, isbn, antallEksemplarer);
        AlleBøker.Add(nyBok);
    }

    public void RegistrerUtvekslingsStudent(int id, string navn, string epost, string passord, string hjemUni, string land, string periode)
    {
        Utvekslingsstudent nyUtvekslingsstudent = new Utvekslingsstudent(id, navn, epost, passord, hjemUni, land, periode);
        AlleStudenter.Add(nyUtvekslingsstudent);
        AlleBrukere.Add(nyUtvekslingsstudent);
    }
    //-------------------------Slutt av lister og liste-funksjoner. Funksjoner for menyen i Program.cs følger under------------------------------//

    public void LeggTilPensum(int kurskode, string bokTittel)
    {
        var kurs = AlleKurs.FirstOrDefault(k => k.Kode == kurskode);
        var bok = AlleBøker.FirstOrDefault(b => b.Tittel.Equals(bokTittel, StringComparison.OrdinalIgnoreCase));

        if (kurs != null && bok != null)
        {
            kurs.Pensumliste.Add(bok);
            Console.WriteLine($"Added {bok.Tittel} added as litterature for {kurs.Navn}. ");
        }
        else
        {
            Console.WriteLine("ERROR Couldnt find DataSlate and/or Course");
        }
    }

    public void SettKarakter(int studentId, int kursKode, string karakter)
    {
        string[] gyldige = { "A", "B", "C", "D", "E", "F" };
        if (!gyldige.Contains(karakter.ToUpper()))
        {
            Console.WriteLine(">>> ERROR: Invalid grade. Use A-F.");
            return;
        }
        var student = AlleStudenter.FirstOrDefault(s => s.Id == studentId);
        if (student != null &&  student.Kursliste.Any(k => k.Kode == kursKode))
        {
            KarakterOversikt.Add($"Student: {student.Navn} | Course: {kursKode} | Grade: {karakter}");
            Console.WriteLine($"Grade Recorded: " + student.Navn + " received " + karakter + " in course " + kursKode);
        }
    }
    //Brukes av case "2"
    public void TildelKurs(int studentId, int kursKode)
    {
        // sjekker kurs og studenter for om de er påmeldt kurs og påmelder deres kurs.
        var student = AlleStudenter.FirstOrDefault(s => s.Id == studentId);
        var kurs = AlleKurs.FirstOrDefault(k => k.Kode == kursKode);

        if (student == null || kurs == null) // sjekker for student og kurs
        throw new Exception(">>>ERROR: student or course not found in the archives!");

        // Sjekk om student allerede er påmeldt
        if (student.Kursliste.Contains(kurs))
        throw new Exception($">>>ERROR: {student.Navn} is already enrolled in {kurs.Navn}!");

        // Sjekk kapasitet i kurset ved å telle hvor mange studenter som allerede er påmeldt det aktuelle kurset. Dette gjøres ved å bruke LINQ for å telle antall studenter i AlleStudenter som har det aktuelle kurset i sin Kursliste. (ikke brukt per nå på grunn av større kurs og få studenter)
        int antallPåmeldte = AlleStudenter.Count(s => s.Kursliste.Contains(kurs));
        if (antallPåmeldte >= kurs.MaksStudenter)
        throw new Exception($">>>ERROR: {kurs.Navn} has reached maximum capacity!");

        student.Kursliste.Add(kurs);
        Console.WriteLine($">>> {student.Navn} successfully assigned to {kurs.Navn}.");
    }

    public void MeldAvKurs(int studentId, int kursKode) // brukes for å melde student ut av kurs i case "2"
    {
        var student = AlleStudenter.FirstOrDefault(s => s.Id == studentId);
        var kurs = student?.Kursliste.FirstOrDefault(k => k.Kode == kursKode);

        if (student != null && kurs != null) // Sjekker at både student og kurs finnes før den prøver å fjerne studenten fra kurslisten
        {
            student.Kursliste.Remove(kurs);
            Console.WriteLine($">>> {student.Navn} has been removed from {kurs.Navn}.");
        }
        else
        {
            Console.WriteLine(">>> ERROR: Could not find student or course enrollment.");
        }
    }
    // Brukes av case "3"
    public string VisKurs() // Viser alle kurs i systemet
    {
        string output = "";
        foreach (var kurs in AlleKurs) output += kurs.Navn + " (" + kurs.Kode + ")\n"; // Formaterer kursnavn og kode for visning
        return output;
    }

    public string VisstudentListe() // Viser alle studenter i systemet
    {
        string output = "";
        foreach (var s in AlleStudenter) output += s.Navn + " (" + s.Id + ")\n"; // Formaterer studentnavn og ID for visning
        return output;
    }

    public void VisTildelinger() //Oppdatert for å vise tildelinger i et mer lesbart format, inkludert kurs og pensum for hver student. Dette gir en bedre oversikt over hvilke kurs hver student er tildelt, og hvilke bøker som er pensum for disse kursene.
    {
        foreach (var s in AlleStudenter)
        {
            Console.WriteLine($"\n>>> {s.Navn.ToUpper()} (ID: {s.Id})");
            foreach (var k in s.Kursliste)
            {
                Console.WriteLine($"- {k.Navn} ({k.Kode})");
                // Viser pensum hvis det finnes
                if (k.Pensumliste.Any())
                {
                    string pensum = string.Join(", ", k.Pensumliste.Select(b => b.Tittel));
                    Console.WriteLine($"  [DATA SLATES: {pensum}]");
                }
            }
        }
    }
    public void VisMinStatus(int studentId) // Viser en mer detaljert status for en student, inkludert hvilke kurs de er tildelt og hvilke karakterer de har fått. Dette gir studenten en klar oversikt over deres akademiske status i systemet.
    {
        var student = AlleStudenter.FirstOrDefault(s => s.Id == studentId);
        if (student == null) return;

        Console.WriteLine($"\n=== ARCHIVAL RECORD FOR {student.Navn.ToUpper()} ===");

        Console.WriteLine("--- ENROLLED PROTOCOLS ---");
        foreach (var k in student.Kursliste)
        {
            Console.WriteLine($"> {k.Navn} ({k.Kode}) - {k.Poeng} Points");
        }

        Console.WriteLine("\n--- COMPLETED EVALUATIONS ---");
        
        var mineKarakterer = KarakterOversikt.Where(linje => linje.Contains(student.Navn));

        if (!mineKarakterer.Any())
        {
            Console.WriteLine(">>> NO EVALUATIONS RECORDED YET.");
        }
        else
        {
            foreach (var k in mineKarakterer) Console.WriteLine(k);
        }
    }

    // Brukes av case "4"
    public string VisKursInfo(int kode)
    {
        var kurs = AlleKurs.FirstOrDefault(k => k.Kode == kode);
        return kurs != null ? $"{kurs.Navn} ({kurs.Poeng} poeng)" : ">COULD NOT FIND COURSE<";
    }

    public string SearchBøker(string query) //ble skiftet til query for å unngå forvirring med tittel og forfatter, og for å gjøre det mer generelt for søk i både tittel og forfatter. 
    {
        // LINQ for å finne alle bøker med tittel eller forfatter 
        var treff = AlleBøker
        .Where(b => b.Tittel.Contains(query, StringComparison.OrdinalIgnoreCase) || b.Forfatter.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();

        if (!treff.Any())
            return ">>> NO DATA FOUND."; // Hvis ingen treff, returner melding

        return string.Join("\n", treff.Select(b => $"- {b.Tittel} ({b.Forfatter}) | Copies: {b.AntallEksemplarer}")); // Returnerer en formatert liste over treff, inkludert antall eksemplarer tilgjengelig
    }
    // Brukes av case "6"
    public void LånBok(int id, string tittel)
    {
        var bruker = AlleBrukere.FirstOrDefault(b => b.Id == id); // Finner bruker basert på ID
        var bok = AlleBøker.FirstOrDefault(b => b.Tittel.Equals(tittel, StringComparison.OrdinalIgnoreCase)); // Finner bok basert på tittel.

        if (bruker == null) throw new Exception(">>> ERROR: USER NOT FOUND IN THE ARCHIVES!"); // Sjekker om bruker finnes, hvis ikke, returner melding

        if (bok == null)throw new Exception(">>> ERROR: DATA-SLATE NOT FOUND IN THE ARCHIVES!"); // Sjekker om bok finnes, hvis ikke, returner melding
        // sjekker om det er eksemplarer igjen av boken.
        if (bok.AntallEksemplarer <= 0) throw new Exception(">>> ERROR: NO COPIES LEFT IN THE ARCHIVES!"); // Sjekker om det er eksemplarer igjen, hvis ikke, returner melding

        else
        {

            bok.AntallEksemplarer--;

            if (bok.AntallEksemplarer == 0) bok.ErUtlånt = true; // Oppdaterer status til utlånt hvis det ikke er flere eksemplarer igjen
            UtlånsHistorikk.Add($"[LOAN] {bruker.Navn} borrowed '{bok.Tittel}' at {DateTime.Now}"); // for historikk av lån DateTime.Now for å registrere når boken ble lånt ut

            Console.WriteLine($">>> SUCCESS: '{bok.Tittel}' DISTRIBUTED TO {bruker.Navn}."); // Viser at boken er lånt ut til brukeren
            Console.WriteLine($">>> COPIES REMAINING: {bok.AntallEksemplarer}");
        }
    }
    // Brukes av case "7"
    public void ReturnerBok(int id, string tittel) // Finner bruker og bok basert på ID og tittel, og returnerer boken hvis den er utlånt.
    {
        var bruker = AlleBrukere.FirstOrDefault(b => b.Id == id);
        var bok = AlleBøker.FirstOrDefault(b => b.Tittel.Equals(tittel, StringComparison.OrdinalIgnoreCase));
        if (bruker != null && bok != null && bok.ErUtlånt) // Sjekker at både bruker og bok finnes, og at boken er utlånt før den kan returneres
        {
            //Øk antall eksemplarer tilbake til lageret når boken returneres
            bok.AntallEksemplarer++;

            //Oppdaterer status til ikke utlånt hvis det nå er eksemplarer tilgjengelig igjen
            bok.ErUtlånt = false;
            UtlånsHistorikk.Add($"[RETURN] {bruker.Navn} returned '{bok.Tittel}' at {DateTime.Now}"); //registrerer historikk retur DateTime.Now for å registrere når boken ble returnert

            Console.WriteLine($">>> SUCCESS: '{bok.Tittel}' returned by {bruker.Navn}.");
            Console.WriteLine($">>> COPIES NOW IN ARCHIVE: {bok.AntallEksemplarer}");
        }
        else
        {
            Console.WriteLine(">>> ERROR: Could not return book. Check ID or Title.");
        }
    }

    public void VisHistorikk()  //lagt til historikk for å lese av når og av hvem boken er utlånt ilag med tidligere kommandoer om lån og retur. Dette gir en oversikt over transaksjonshistorikken i arkivet.
    {
        Console.WriteLine("=== ARCHIVAL LOGS: TRANSACTION HISTORY ===");
        if (UtlånsHistorikk.Count == 0)
        {
            Console.WriteLine(">>> NO RECORDS FOUND IN THE ARCHIVES.");
        }
        else
        {
            foreach (var entry in UtlånsHistorikk)
            {
                Console.WriteLine(entry);
            }
        }
    }
}

