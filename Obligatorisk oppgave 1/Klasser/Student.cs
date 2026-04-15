using System;
using System.Collections.Generic;
using System.Text;

namespace Obligatorisk_oppgave_1;

public class Student : Bruker //arver fra Bruker-klassen, som inneholder id, navn, epost og passord. Student-klassen har i tillegg en liste over kurs og en ordbok for karakterer, der nøkkelen er kurs-id og verdien er karakteren for det kurset.
{
    public List<Kurs> Kursliste { get; set; } = new List<Kurs>();
    public Dictionary<int, string> Karakterer { get; set; } = new Dictionary<int, string>();

    public Student(int id, string navn, string epost, string passord)
        : base(id, navn, epost, passord, "Student")
    {

    }
}