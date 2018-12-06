using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerProduction.OPC;

namespace WebApplication1.Models
{
    public class Beer
    {
        public int id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public TimeSpan TimeSpan {get => End-Start;}
        public int Good { get; set; }
        public int Bad { get; set; }
        public int Speed { get; set; }
    }

    public class Pilsner : Beer
    {
        public static int MaxSpeed => 600;
        public static float BeerOpcCmd => 0;
        public string PercentSpeed => (Speed / MaxSpeed * 100).ToString() + "%";
    }
    public class Wheat : Beer
    {
        public static int MaxSpeed => 300;
        public static float BeerOpcCmd => 1;
        public string PercentSpeed => (Speed / MaxSpeed * 100).ToString() + "%";
    }
    public class Ipa : Beer
    {
        public static int MaxSpeed => 150;
        public static float BeerOpcCmd => 2;
        public string PercentSpeed => (Speed / MaxSpeed * 100).ToString() + "%";
    }
    public class Stout : Beer
    {
        public static int MaxSpeed => 200;
        public static float BeerOpcCmd => 3;
        public string PercentSpeed => (Speed / MaxSpeed * 100).ToString() + "%";
    }
    public class Ale : Beer
    {
        public static int MaxSpeed => 100;
        public static float BeerOpcCmd => 4;
        public string PercentSpeed => (Speed / MaxSpeed * 100).ToString() + "%";
    }
    public class AlocFree : Beer
    {
        public static int MaxSpeed => 125;
        public static float BeerOpcCmd => 5;
        public string PercentSpeed => (Speed / MaxSpeed * 100).ToString() + "%";
    }
}
