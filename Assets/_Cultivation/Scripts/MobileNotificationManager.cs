///-----------------------------------------------------------------
///   Author : Gabriel Bernabeu                    
///   Date   : 22/11/2022 23:30
///-----------------------------------------------------------------

using Com.GabrielBernabeu.Common.DataManagement;
using System;
using Unity.Notifications.Android;
using UnityEngine;

namespace Com.GabrielBernabeu.Cultivation 
{
    public class MobileNotificationManager : MonoBehaviour
    {
        private AndroidNotificationChannel defaultChannel;
        private int notificationId;

        public static MobileNotificationManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            defaultChannel = new AndroidNotificationChannel()
            {
                Id = "default_channel",
                Name = "Default Channel",
                Description = "For Generic Notifications",
                Importance = Importance.Default
            };

            AndroidNotificationCenter.RegisterNotificationChannel(defaultChannel);

            AndroidNotificationCenter.NotificationReceivedCallback lReceivedNotificationHandler =
                delegate (AndroidNotificationIntentData data)
                {
                    var msg = "Notification received: " + data.Id + "\n";
                    msg += "\n Notification received: ";
                    msg += "\n .Title: " + data.Notification.Title;
                    msg += "\n .Body: " + data.Notification.Text;
                    msg += "\n .Channel: " + data.Channel;
                    Debug.Log(msg);
                };

            AndroidNotificationCenter.OnNotificationReceived += lReceivedNotificationHandler;

            var lNotificationIntentData = AndroidNotificationCenter.GetLastNotificationIntent();

            if (lNotificationIntentData != null)
            {
                Debug.Log("App was opened with a notification!");
            }

            CleanDisplayedNotifications();
        }

        public void CleanDisplayedNotifications()
        {
            AndroidNotificationCenter.CancelAllDisplayedNotifications();
        }

        public void ResetNotifications()
        {
            AndroidNotificationCenter.CancelAllNotifications();
        }

        public void MakeNotification(string title, string text, DateTime fireTime, TimeSpan repeatInterval = default)
        {
            AndroidNotification lNotification = new AndroidNotification()
            {
                Title = title,
                Text = text,
                SmallIcon = "logo_small",
                LargeIcon = "logo_large",
                FireTime = fireTime,
                RepeatInterval = repeatInterval
            };

            notificationId = AndroidNotificationCenter.SendNotification(lNotification, "default_channel");
        }

        public void MakeRepeatingNotifications(LocalData data)
        {
            string lNotificationTitle = "Complete today's task!";
            string lNotificationText = "Get ready for your " + data.taskName + " session!";

            if (data.monday)
            {
                MakeNotification(lNotificationTitle, lNotificationText,
                    GetNextDayOfWeekDateAtHour(DayOfWeek.Monday), new TimeSpan(7, 0, 0, 0));
            }
            if (data.tuesday)
            {
                MakeNotification(lNotificationTitle, lNotificationText,
                    GetNextDayOfWeekDateAtHour(DayOfWeek.Tuesday), new TimeSpan(7, 0, 0, 0));
            }
            if (data.wednesday)
            {
                MakeNotification(lNotificationTitle, lNotificationText,
                    GetNextDayOfWeekDateAtHour(DayOfWeek.Wednesday), new TimeSpan(7, 0, 0, 0));
            }
            if (data.thursday)
            {
                MakeNotification(lNotificationTitle, lNotificationText,
                    GetNextDayOfWeekDateAtHour(DayOfWeek.Thursday), new TimeSpan(7, 0, 0, 0));
            }
            if (data.friday)
            {
                MakeNotification(lNotificationTitle, lNotificationText,
                    GetNextDayOfWeekDateAtHour(DayOfWeek.Friday), new TimeSpan(7, 0, 0, 0));
            }
            if (data.saturday)
            {
                MakeNotification(lNotificationTitle, lNotificationText,
                    GetNextDayOfWeekDateAtHour(DayOfWeek.Saturday), new TimeSpan(7, 0, 0, 0));
            }
            if (data.sunday)
            {
                MakeNotification(lNotificationTitle, lNotificationText,
                    GetNextDayOfWeekDateAtHour(DayOfWeek.Sunday), new TimeSpan(7, 0, 0, 0));
            }
        }

        private DateTime GetNextDayOfWeekDateAtHour(DayOfWeek dayOfWeek, int hour = 8)
        {
            if (hour < 0 || hour > 24)
            {
                Debug.LogError("Hour must be between 0 and 24!");
                return new DateTime();
            }    

            DateTime lToday = DateTime.Today;
            int lDaysUntilDayOfWeek = ((int)dayOfWeek - (int)lToday.DayOfWeek + 7) % 7;
            DateTime lReturnedValue = lToday.AddDays(lDaysUntilDayOfWeek);
            int lDeltaHours = hour - lReturnedValue.Hour;
            lReturnedValue = lReturnedValue.AddHours(lDeltaHours);
            lReturnedValue.AddMinutes(-lReturnedValue.Minute);
            lReturnedValue.AddSeconds(-lReturnedValue.Second);

            Debug.Log(lReturnedValue);

            return lReturnedValue;
        }

        private void OnDestroy()
        {
            if (Instance != this)
                return;

            Instance = null;
        }
    }
}