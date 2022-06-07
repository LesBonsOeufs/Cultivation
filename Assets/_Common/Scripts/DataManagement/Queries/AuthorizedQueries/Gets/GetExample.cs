using Com.GabrielBernabeu.Common.DataManagement.ServerTranslation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Com.GabrielBernabeu.Common.DataManagement.Queries.AuthorizedQueries.Gets {
    public class GetExample : AuthorizedQuery
    {
        public static List<ExampleData> ReceivedData => _receivedData;
        protected static List<ExampleData> _receivedData;

        public GetExample(MonoBehaviour sender, UnityAction successCallback = null, UnityAction errorCallback = null,
            UnityAction unauthorizedCallback = null, UnityAction generalCallback = null)
            : base(sender, successCallback, errorCallback, unauthorizedCallback, generalCallback) { }

        protected override IEnumerator Communicate()
        {
            using (UnityWebRequest lRequest = UnityWebRequest.Get(SERVER_URL + "/example"))
            {
                lRequest.SetRequestHeader("Authorization", "Bearer " + _token);

                yield return lRequest.SendWebRequest();

                if (lRequest.result == UnityWebRequest.Result.ConnectionError || lRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    //Debug.Log(lRequest.error);
                    //Debug.Log(lRequest.downloadHandler.text);
                    errorCallback?.Invoke();

                    if (lRequest.responseCode == HTTP_UNAUTHORIZED_CODE)
                        unauthorizedCallback?.Invoke();
                }
                else
                {
                    //Debug.Log(lRequest.downloadHandler.text);
                    _receivedData = JsonUtility.FromJson<ExampleDataCollection>(lRequest.downloadHandler.text).datas;
                    successCallback?.Invoke();

                    //foreach (ExampleData item in _receivedData)
                    //{
                    //    Debug.Log(item.consumableId + ", " + item.amount);
                    //}
                }
                generalCallback?.Invoke();
            }
        }

        [Serializable]
        private struct ExampleDataCollection
        {
            public List<ExampleData> datas;
        }
    }
}
