using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LinhowePerceptualSystem
{
    /// <summary>
    /// 感知系统管理器
    /// </summary>
    public static class PerceptualSystemManager
    {
        private static List<BaseTraigger> Traiggers = new List<BaseTraigger>();
        private static List<BaseSensor> Sensors = new List<BaseSensor>();
        private static List<BaseTraigger> ToBeRemovedTraiggers = new List<BaseTraigger>();
        private static List<BaseSensor> ToBeRemovedSensors = new List<BaseSensor>();

        static PerceptualSystemManager()
        {

        }
        public static void RegisterTrigger(BaseTraigger traigger)
        {
            if (!Traiggers.Contains(traigger))
                Traiggers.Add(traigger);
        }
        public static void RegisterSensor(BaseSensor sensor)
        {
            if (!Sensors.Contains(sensor))
                Sensors.Add(sensor);
        }

        public static void Update()
        {
            UpdateTriggers();
            TrySensors();
        }

        private static void UpdateTriggers()
        {
            foreach(BaseTraigger traigger in Traiggers)
            {
                if (traigger.ToBeRemoved)
                    ToBeRemovedTraiggers.Add(traigger);
                else if(traigger.isActiveAndEnabled)
                    traigger.UpdateInfo();
            }
            foreach(BaseTraigger traigger in ToBeRemovedTraiggers)
            {
                Traiggers.Remove(traigger);
            }
            ToBeRemovedTraiggers.Clear();
        }

        private static void TrySensors()
        {
            foreach (BaseSensor sensor in Sensors)
            {
                if (sensor.ToBeRemoved)
                    ToBeRemovedSensors.Add(sensor);
                else
                {
                    foreach(BaseTraigger traigger in Traiggers)
                    {
                        traigger.Try(sensor);
                    }
                }
            }
            foreach (BaseSensor sensor in ToBeRemovedSensors)
            {
                Sensors.Remove(sensor);
            }
            ToBeRemovedSensors.Clear();
        }
    }
}
