using System;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    private int _currentDay = 1;
    private int _currentMonth = 1;
    private int _currentYear = 1;

    private void OnEnable()
    {
        TimeSignals.OnGoToSleep.AddListener(OnGoToSleep);
    }

    private void OnDisable()
    {
        TimeSignals.OnGoToSleep.RemoveListener(OnGoToSleep);
    }

    private void OnGoToSleep()
    {
        IncreaseDay();
        var dayData = new DayData(_currentDay, _currentMonth, _currentYear);
        TimeSignals.OnStartNewDay.Dispatch(dayData);
        GameSignals.OnShowGameMessageSignal.Dispatch(dayData.GetStartDayMessage());
    }

    private void IncreaseDay()
    {
        _currentDay++;

        if (_currentDay <= 28) return;
        _currentDay = 1;
        _currentMonth++;

        if (_currentMonth <= 12) return;
        _currentMonth = 1;
        _currentYear++;
    }
}


public class DayData
{
    private int _day, _month, _year;

    public DayData(int day, int month, int year)
    {
        _day = day;
        _month = month;
        _year = year;
    }

    public string GetStartDayMessage()
    {
        return $"{GetDayInWeek(_day)}, Week {(_day - 1) / 7 + 1}, {GetMonth(_month)}";
    }

    public static string GetDayInWeek(int day)
    {
        return (day % 7) switch
        {
            1 => "Monday",
            2 => "Tuesday",
            3 => "Wednesday",
            4 => "Thursday",
            5 => "Friday",
            6 => "Saturday",
            0 => "Sunday",
            _ => ""
        };
    }

    public static string GetMonth(int month)
    {
        return month switch
        {
            1 => "January",
            2 => "February",
            3 => "March",
            4 => "April",
            5 => "May",
            6 => "June",
            7 => "July",
            8 => "April",
            9 => "September",
            10 => "October",
            11 => "November",
            12 => "December",
            _ => ""
        };
    }
}