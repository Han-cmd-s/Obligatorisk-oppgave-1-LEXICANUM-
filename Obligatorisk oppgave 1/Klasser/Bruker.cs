using System;
using System.Collections.Generic;
using System.Text;

namespace Obligatorisk_oppgave_1;
//basis klassen for Student, Utvekslingsstudent og Lærer. Alle har id, navn og epost, så disse er definert her for å unngå duplisering av kode i de andre klassene.
public class Bruker
{
    public int Id { get; set; } 
    public string Navn { get; set; }
    public string Epost { get; set; }
    public string Passord { get; set; }
    public string Rolle { get; set; }


    public Bruker(int id, string navn, string epost, string passord, string rolle)
    {
        Id = id;
        Navn = navn;
        Epost = epost;
        Passord = passord;
        Rolle = rolle;
    }

}
