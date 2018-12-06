using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerProduction.OPC
{
    public static class Buttons
    {
        public static int RESET = 1;
        public static int START = 2;
        public static int STOP = 3;
        public static int ABORT = 4;
        public static int CLEAR = 5;
    }
    public static class States
    {
        public static int Deactivated = 0;
        public static int Clearing = 1;
        public static int Stopped = 2;
        public static int Starting = 3;
        public static int Idle = 4;
        public static int Suspended = 5;
        public static int Execute = 6;
        public static int Stopping = 7;
        public static int Aborting = 8;
        public static int Aborted = 9;
        public static int Holding = 10;
        public static int Held = 11;
        public static int Resetting = 15;
        public static int Completing = 16;
        public static int Complete = 17;
        public static int Deactivating = 18;
        public static int Activating = 19;
    }
}
