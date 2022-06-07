///-----------------------------------------------------------------
///   Author : Gabriel Bernabeu                    
///   Date   : 17/05/2022 10:40
///-----------------------------------------------------------------

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Com.GabrielBernabeu.Common.DataManagement.Queries {
    public class ConnectQuery : Query
    {
        private string nameId;
        private string password;
        private bool newUser;

        public ConnectQuery(MonoBehaviour sender, string nameId, string password, bool newUser = false, UnityAction successCallback = null, UnityAction errorCallback = null,
             UnityAction generalCallback = null) : base(sender, successCallback, errorCallback, generalCallback)
        {
            this.nameId = nameId;
            this.password = password;
            this.newUser = newUser;
        }

        protected override IEnumerator Communicate()
        {
            //Code permettant d'envoyer la data en JSON dans le body de la request.
            byte[] lSentData = System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(new ConnectInfos(nameId, password)));

            using (UnityWebRequest lRequest = new UnityWebRequest(SERVER_URL + (newUser ? "/signup" : "/login"),
                                                                  newUser ? UnityWebRequest.kHttpVerbPOST : UnityWebRequest.kHttpVerbGET))
            {
                lRequest.uploadHandler = new UploadHandlerRaw(lSentData);
                lRequest.downloadHandler = new DownloadHandlerBuffer();
                lRequest.uploadHandler.contentType = "application/json";

                yield return lRequest.SendWebRequest();

                if (lRequest.result == UnityWebRequest.Result.ConnectionError || lRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    //Debug.Log(lRequest.error);
                    //Debug.Log(lRequest.downloadHandler.text);
                    errorCallback?.Invoke();
                }
                else
                {
                    //Debug.Log(lRequest.downloadHandler.text);
                    AuthorizedQuery.SetToken(lRequest.downloadHandler.text);
                    successCallback?.Invoke();
                }

                generalCallback?.Invoke();
            }
        }

        [Serializable]
        private struct ConnectInfos
        {
            public string nameId;
            public string password;

            public ConnectInfos(string nameId, string password)
            {
                this.nameId = nameId;
                this.password = password;
            }
        }
    }
}
