using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    #region Doors
    public static event Action<int> OnDoorTriggerEnter;
    public static void DoorTriggerEnter(int id)
    {
        OnDoorTriggerEnter?.Invoke(id);
    }

    public static event Action<int> OnDoorTriggerExit;
    public static void DoorTriggerExit(int id)
    {
        OnDoorTriggerExit?.Invoke(id);
    }
    #endregion


    #region Cuerda
    public static event Action<int> OnCuerdaTriggerEnter;
    public static void CuerdaTriggerEnter(int id)
    {
        OnCuerdaTriggerEnter?.Invoke(id);
    }

    public static event Action<int> OnCuerdaTriggerExit;
    public static void CuerdaTriggerExit(int id)
    {
        OnCuerdaTriggerExit?.Invoke(id);
    }
    #endregion


    #region Presion

    #region Trigger

    public static event Action<int> OnPresionTriggerEnter;
    public static void PresionTriggerEnter(int id)
    {
        OnPresionTriggerEnter?.Invoke(id);
    }

    public static event Action<int> OnPresionTriggerExit;
    public static void PresionTriggerExit(int id)
    {
        OnPresionTriggerExit?.Invoke(id);
    }
    #endregion

    #region Alternate
    public static event Action<int> OnPresionOpen;
    public static void PresionOpen(int id)
    {
        OnPresionOpen?.Invoke(id);
    }

    public static event Action<int> OnPresionClose;
    public static void PresionClose(int id)
    {
        OnPresionClose?.Invoke(id);
    }
    #endregion

    #endregion
}
