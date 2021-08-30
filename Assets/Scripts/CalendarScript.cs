using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CalendarScript : MonoBehaviour
{
    public TextMeshProUGUI clockGUI;
    public TextMeshProUGUI dayGUI;
    public TextMeshProUGUI monthGUI;
    public TextMeshProUGUI yearGUI;

    //Days ENUM
    public enum DAY
    {
        MON, TUE, WED, THU, FRI, SAT, SUN
    };

    //Months ENUM
    public enum MONTH
    {
        JAN = 1, FEB, MAR, APR, MAY, JUN, JUL, AUG, SEP, OCT, NOV, DEC
    };

    [SerializeField]
    DAY day;
    [SerializeField]
    MONTH month = MONTH.JAN;
    [SerializeField]
    int monthDate = 1;
    [SerializeField]
    int yearDate = 2020;

    int hour, minute;

    [SerializeField]
    float halfHourTime = 1;
    float timePassed = 0;

    //Setters
    public void setHalfHourTime(float newHRTime) { halfHourTime = newHRTime; }//don't make this lower than 0.01 or the engine can't keep up
    public void setCalendar(int newMinute, int newHour, int newMonthDate, MONTH newMonth, int newYear, DAY newWeekDay) 
    {
        minute = newMinute;
        hour = newHour;
        monthDate = newMonthDate;
        month = newMonth;
        yearDate = newYear;
        day = newWeekDay;
    }

    //Getters
    public int getMinute() { return minute; }
    public int getHour() { return hour; }
    public int getDate() { return monthDate; }
    public MONTH getMonth() { return month; }
    public int getYear() { return yearDate; }
    public DAY getWeekDay() { return day; }

    void Start()
    {
        hour = 0;
        minute = 0;

        UpdateClockOutput();
        UpdateDayOutput();
        monthGUI.text = month.ToString();
        yearGUI.text = yearDate.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;

        if (timePassed >= halfHourTime)
		{
            timePassed = timePassed - halfHourTime;//reset the clock

			if (minute == 0)
			{
                minute = 30;
			}
            else if (minute >= 30)
			{
                minute = 0;
                hour++;

                if (hour >= 24)
				{
                    hour = 0;
                    NextDay();
				}
			}
		}

        UpdateClockOutput();
    }

    void NextDay()
	{
        if(day == DAY.SUN) { day = DAY.MON; }
		else { day++; }
        
        monthDate++;

        if (monthDate > 28 && month == MONTH.FEB)//Leap year should be accounted for
		{
            if((yearDate % 4) != 0) 
            {
                monthDate = 1;
                NextMonth();
            }
            
            if (monthDate > 29)
			{
                monthDate = 1;
                NextMonth();
            }
		}
        else if (monthDate > 30 && (month == MONTH.APR || month == MONTH.JUN || month == MONTH.SEP || month == MONTH.NOV))//30 days hath september, April, June and November
		{
            monthDate = 1;
            NextMonth();
		}
        else if (monthDate > 31 && (month == MONTH.JAN || month == MONTH.MAR || month == MONTH.MAY || month == MONTH.JUL || month == MONTH.AUG || month == MONTH.OCT || month == MONTH.NOV || month == MONTH.DEC))//All the rest have 31, save February alone
		{
            monthDate = 1;
            NextMonth();
        }

        UpdateDayOutput();
	}

    void NextMonth()
	{
        if(month == MONTH.DEC)
		{
            month = MONTH.JAN;
            NextYear();
		}
		else { month++; }

        monthGUI.text = month.ToString();
	}

    void NextYear()
	{
        yearDate++;

        yearGUI.text = yearDate.ToString();
	}

    void UpdateClockOutput()
	{
        string hourTidy = "0";
        string minuteTidy = "00";

        if (hour < 10)
		{
            hourTidy = hourTidy + hour.ToString();
		}
		else { hourTidy = hour.ToString(); }

        if (minute == 0) { minuteTidy = "00"; }
		else { minuteTidy = minute.ToString(); }

        clockGUI.text = hourTidy + ":" + minuteTidy;
    }

    void UpdateDayOutput()
	{
        string monthTidy = "";

        if (monthDate < 10) { monthTidy = "0" + monthDate.ToString(); }
        else { monthTidy = monthDate.ToString(); }

        dayGUI.text = monthTidy;
    }
}
