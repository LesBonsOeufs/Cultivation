///-----------------------------------------------------------------
///   Author : Gabriel Bernabeu                    
///   Date   : 17/05/2022 11:17
///-----------------------------------------------------------------

using Com.GabrielBernabeu.Common.DataManagement.ServerTranslation;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Com.GabrielBernabeu.Common.DataManagement.Queries.AuthorizedQueries.Posts {
    public class AddFish : AuthorizedQuery
    {
        private ExampleData exampleData;

        public AddFish(MonoBehaviour sender, ExampleData exampleData, UnityAction successCallback = null, UnityAction errorCallback = null,
            UnityAction unauthorizedCallback = null, UnityAction generalCallback = null)
            : base(sender, successCallback, errorCallback, unauthorizedCallback, generalCallback) 
        {
            this.exampleData = exampleData;
        }

        protected override IEnumerator Communicate()
        {
            byte[] lSentData = System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(exampleData));

            using (UnityWebRequest lRequest = new UnityWebRequest(SERVER_URL + "/example", UnityWebRequest.kHttpVerbPOST))
            {
                lRequest.SetRequestHeader("Authorization", "Bearer " + _token);
                lRequest.uploadHandler = new UploadHandlerRaw(lSentData);
                lRequest.downloadHandler = new DownloadHandlerBuffer();
                lRequest.uploadHandler.contentType = "application/json";

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
                    successCallback?.Invoke();
                }

                generalCallback?.Invoke();
            }
        }
    }
}
