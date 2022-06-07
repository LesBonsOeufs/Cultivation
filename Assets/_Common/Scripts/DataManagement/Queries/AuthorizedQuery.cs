///-----------------------------------------------------------------
///   Author : Gabriel Bernabeu                    
///   Date   : 17/05/2022 11:00
///-----------------------------------------------------------------

using UnityEngine;
using UnityEngine.Events;

namespace Com.GabrielBernabeu.Common.DataManagement.Queries {
    public abstract class AuthorizedQuery : Query
    {
        protected const int HTTP_UNAUTHORIZED_CODE = 401;
        protected static string _token = "";

        public UnityAction unauthorizedCallback;

        public AuthorizedQuery(MonoBehaviour sender, UnityAction successCallback = null, UnityAction errorCallback = null,
            UnityAction unauthorizedCallback = null, UnityAction generalCallback = null) 
            : base(sender, successCallback, errorCallback, generalCallback)
        {
            this.unauthorizedCallback = unauthorizedCallback;
        }

        public static void SetToken(string token)
        {
            _token = token;
        }
    }
}
