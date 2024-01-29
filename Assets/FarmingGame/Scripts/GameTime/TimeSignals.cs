using strange.extensions.signal.impl;

public static class TimeSignals
{
    public static Signal<DayData> OnStartNewDay = new();
    public static Signal OnGoToSleep = new();
}