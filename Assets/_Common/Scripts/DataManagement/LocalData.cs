using Com.GabrielBernabeu.Cultivation;
using System;

namespace Com.GabrielBernabeu.Common.DataManagement {
    [Serializable]
    public struct LocalData
    {
        public SeedType seedType;
        public string taskName;

        public LocalData(SeedType seedType, string taskName = null)
        {
            this.seedType = seedType;
            this.taskName = taskName;
        }
    }
}