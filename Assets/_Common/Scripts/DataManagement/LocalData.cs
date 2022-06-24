using Com.GabrielBernabeu.Cultivation;
using System;

namespace Com.GabrielBernabeu.Common.DataManagement {
    [Serializable]
    public struct LocalData
    {
        public SeedType seedType;
        public string taskName;

        public bool monday;
        public bool tuesday;
        public bool wednesday;
        public bool thursday;
        public bool friday;
        public bool saturday;
        public bool sunday;

        public string lastTaskDoneDate;

        public LocalData(SeedType seedType, string taskName, 
                         bool monday, bool tuesday, bool wednesday, bool thursday, bool friday, bool saturday, bool sunday)
        {
            this.seedType = seedType;
            this.taskName = taskName;

            this.monday = monday;
            this.tuesday = tuesday;
            this.wednesday = wednesday;
            this.thursday = thursday;
            this.friday = friday;
            this.saturday = saturday;
            this.sunday = sunday;

            lastTaskDoneDate = DateTime.Now.AddDays(-1).ToShortDateString();
        }
    }
}