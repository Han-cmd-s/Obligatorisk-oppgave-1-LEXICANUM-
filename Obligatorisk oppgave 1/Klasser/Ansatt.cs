using System;
using System.Collections.Generic;
using System.Text;

namespace Obligatorisk_oppgave_1;

public class Ansatt : Bruker //byttet fra Lærer til Ansatt
{
    public string Stilling { get; set; }
    public string Avdeling { get; set; }
    public List<Kurs> Kursliste { get; set; } = new List<Kurs>();

    public Ansatt(int id, string navn, string epost, string stilling, string avdeling)
        : base(id, navn, epost)
    {
        Stilling = stilling;
        Avdeling = avdeling;
    }

}

