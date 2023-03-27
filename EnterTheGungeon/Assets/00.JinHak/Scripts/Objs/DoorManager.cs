using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : GSingleton<DoorManager>
{
    public delegate void DoorOpenHandler();
    public event DoorOpenHandler DoorOpen;

    public delegate void DoorCloseHandler();
    public event DoorCloseHandler DoorClose;

    public void AllDoorOpen()
    {
        DoorOpen();
    }
    
    public void AllDoorClose()
    {
        DoorClose();
    }
}
