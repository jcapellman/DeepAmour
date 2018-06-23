using Microsoft.ML.Runtime.Api;

namespace deepamour.lib.WarriorsPredictor.Objects
{
    public class WarriorsData
    {
        public float CurryPoints { get => Features[0]; set => Features[0] = value; }

        public float DurantPoints { get => Features[1]; set => Features[1] = value; }

        public float ThompsonPoints { get => Features[2]; set => Features[2] = value; }

        public float GreenPoints { get => Features[3]; set => Features[3] = value; }

        public float IguodalaPoints { get => Features[4]; set => Features[4] = value; }

        public float WarriorsWin { get => Features[5]; set => Features[5] = value; }
        
        [Column("0-5")]
        [VectorType(6)]
        public float[] Features = new float[6];
        
        [Column("6")]
        [ColumnName("Label")]
        public float Label;
    }
}