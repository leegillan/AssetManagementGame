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
    DAY Day;
    [SerializeField]
    MONTH Month = MONTH.JAN;
    [SerializeField]
    int MonthDate = 1;
    [SerializeField]
    int YearDate = 2020;

    int hour, minute;

    [SerializeField]
    float halfHourTime = 1;
    float timePassed = 0;
    public int getDate() { return MonthDate; }
    public MONTH getMonth() { return Month; }

    void Start()
    {
        hour = 0;
        minute = 0;

        UpdateClockOutput();
        UpdateDayOutput();
        monthGUI.text = Month.ToString();
        yearGUI.text = YearDate.ToString();
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
        if(Day == DAY.SUN) { Day = DAY.MON; }
		else { Day++; }
        
        MonthDate++;

        if (MonthDate > 28 && Month == MONTH.FEB)//Leap year should be accounted for
		{
            if((YearDate % 4) != 0) 
            {
                MonthDate = 1;
                NextMonth();
            }
            
            if (MonthDate > 29)
			{
                MonthDate = 1;
                NextMonth();
            }
		}
        else if (MonthDate > 30 && (Month == MONTH.APR || Month == MONTH.JUN || Month == MONTH.SEP || Month == MONTH.NOV))//30 days hath september, April, June and November
		{
            MonthDate = 1;
            NextMonth();
		}
        else if (MonthDate > 31 && (Month == MONTH.JAN || Month == MONTH.MAR || Month == MONTH.MAY || Month == MONTH.JUL || Month == MONTH.AUG || Month == MONTH.OCT || Month == MONTH.NOV || Month == MONTH.DEC))//All the rest have 31, save February alone
		{
            MonthDate = 1;
            NextMonth();
        }

        UpdateDayOutput();
	}

    void NextMonth()
	{
        if(Month == MONTH.DEC)
		{
            Month = MONTH.JAN;
            NextYear();
		}
		else { Month++; }

        monthGUI.text = Month.ToString();
	}

    void NextYear()
	{
        YearDate++;

        yearGUI.text = YearDate.ToString();
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

        if (MonthDate < 10) { monthTidy = "0" + MonthDate.ToString(); }
        else { monthTidy = MonthDate.ToString(); }

        dayGUI.text = monthTidy;
    }
}
