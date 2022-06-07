
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
///-----------------------------------------------------------------
///   Author : Gabriel Bernabeu                    
///   Date   : 17/05/2022 10:27
///-----------------------------------------------------------------
namespace Com.GabrielBernabeu.Common.DataManagement {
    public abstract class Query
    {
        protected const string SERVER_URL = "https://marmite-f2p-http.herokuapp.com/user";

        public UnityAction successCallback;
        public UnityAction errorCallback;
        public UnityAction generalCallback;

        private MonoBehaviour sender;

        public Query(MonoBehaviour sender, UnityAction successCallback = null, UnityAction errorCallback = null,
                     UnityAction generalCallback = null)
        {
            this.sender = sender;
            this.successCallback = successCallback;
            this.errorCallback = errorCallback;
            this.generalCallback = generalCallback;
        }

        public void Send()
        {
            sender.StartCoroutine(Communicate());
        }

        protected abstract IEnumerator Communicate();
    }
}
