///-----------------------------------------------------------------
///   Author : Gabriel Bernabeu                    
///   Date   : 17/05/2022 12:51
///-----------------------------------------------------------------

using System.Collections.Generic;

namespace Com.GabrielBernabeu.Common.DataManagement {
    public static class QueryQueuing
    {
        private static Queue<Query> queryQueue = new Queue<Query>();

        public static void AddQuery(Query query)
        {
            queryQueue.Enqueue(query);
            query.generalCallback += OnQueryEnd;

            if (queryQueue.Count == 1)
                query.Send();
        }

        private static void OnQueryEnd()
        {
            queryQueue.Dequeue();

            if (queryQueue.Count > 0)
                queryQueue.Peek().Send();
        }
    }
}
