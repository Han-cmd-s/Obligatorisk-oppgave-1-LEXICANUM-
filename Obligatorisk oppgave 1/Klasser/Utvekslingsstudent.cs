using System;
using System.Collections.Generic;
using System.Text;

namespace Obligatorisk_oppgave_1;

public class Utvekslingsstudent : Student //arver fra Student-klassen, som igjen arver fra Bruker-klassen
{
    public string Hjemuniversitet { get; set; }
    public string Land { get; set; }
    public string Periode { get; set; } 

    public Utvekslingsstudent(int id, string navn, string epost, string passord, string hjemUni, string land, string periode) 
        : base(id, navn, epost, passord) 
    {
        Hjemuniversitet = hjemUni;
        Land = land;
        Periode = periode;
    }
}
