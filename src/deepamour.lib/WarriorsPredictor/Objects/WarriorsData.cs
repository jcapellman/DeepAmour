using Microsoft.ML.Runtime.Api;

namespace deepamour.lib.WarriorsPredictor.Objects
{
    public class WarriorsData
    {
        [Column("0")]
        public int CurryPoints;
        
        [Column("1")]
        public int DurantPoints;

        [Column("2")]
        public int ThompsonPoints;

        [Column("3")]
        public int GreenPoints;

        [Column("4")]
        public int IguodalaPoints;

        [Column("5")]
        public bool WarriorsWin;
    }
}