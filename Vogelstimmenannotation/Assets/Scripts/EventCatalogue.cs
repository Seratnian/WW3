using System;
using Object = System.Object;

public static class EventCatalogue
{
    public static event EventHandler MenuOpened;
    public static event EventHandler MenuClosed;
    public static event EventHandler IdentificationOpened;
    public static event EventHandler IdentificationClosed;
    public static event EventHandler BirdHit;

    public static void OnMenuOpened(Object obj, EventArgs eventArgs)
    {
        if (MenuOpened != null)
            MenuOpened(obj, eventArgs);
    }

    public static void OnMenuClosed(Object obj, EventArgs eventArgs)
    {
        if (MenuClosed != null)
            MenuClosed(obj, eventArgs);
    }

    public static void OnIdentificationOpened(Object obj, EventArgs eventArgs)
    {
        if (IdentificationOpened != null)
            IdentificationOpened(obj, eventArgs);
    }

    public static void OnIdentificationClosed(Object obj, EventArgs eventArgs)
    {
        if (IdentificationClosed != null)
            IdentificationClosed(obj, eventArgs);
    }

    public static void OnBirdHit(Object obj, EventArgs eventArgs)
    {
        if (BirdHit != null)
            BirdHit(obj, eventArgs);
    }
}