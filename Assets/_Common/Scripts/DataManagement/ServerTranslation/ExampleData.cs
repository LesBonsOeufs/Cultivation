using System;

namespace Com.GabrielBernabeu.Common.DataManagement.ServerTranslation {
    [Serializable]
    public struct ExampleData
    {
        public int info1;
        public int info2;

        public ExampleData(int info1, int info2)
        {
            this.info1 = info1;
            this.info2 = info2;
        }
    }
}
