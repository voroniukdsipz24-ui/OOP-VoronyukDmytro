using System;

class Time
{
    private int hours;
    private int minutes;
    private int seconds;

    public int Hours
    {
        get => hours;
        set
        {
            if (value >= 0 && value < 24)
                hours = value;
            else
                throw new ArgumentOutOfRangeException("Години мають бути в межах 0–23");
        }
    }

    public int Minutes
    {
        get => minutes;
        set
        {
            if (value >= 0 && value < 60)
                minutes = value;
            else
                throw new ArgumentOutOfRangeException("Хвилини мають бути в межах 0–59");
        }
    }

    public int Seconds
    {
        get => seconds;
        set
        {
            if (value >= 0 && value < 60)
                seconds = value;
            else
                throw new ArgumentOutOfRangeException("Секунди мають бути в межах 0–59");
        }
    }

    public Time(int h = 0, int m = 0, int s = 0)
    {
        Hours = h;
        Minutes = m;
        Seconds = s;
    }

    public int this[int index]
    {
        get
        {
            return index switch
            {
                0 => hours,
                1 => minutes,
                2 => seconds,
                _ => throw new IndexOutOfRangeException("Індекс має бути 0..2")
            };
        }
        set
        {
            switch (index)
            {
                case 0: Hours = value; break;
                case 1: Minutes = value; break;
                case 2: Seconds = value; break;
                default: throw new IndexOutOfRangeException("Індекс має бути 0..2");
            }
        }
    }

    private int ToSeconds() => hours * 3600 + minutes * 60 + seconds;

    public static Time operator +(Time t, int addMinutes)
    {
        int total = t.ToSeconds() + addMinutes * 60;
        total = (total % (24 * 3600) + (24 * 3600)) % (24 * 3600);
        return FromSeconds(total);
    }

    public static Time operator -(Time t, int subMinutes)
    {
        return t + (-subMinutes);
    }

    public static bool operator ==(Time t1, Time t2) => t1.ToSeconds() == t2.ToSeconds();
    public static bool operator !=(Time t1, Time t2) => !(t1 == t2);
    public static bool operator <(Time t1, Time t2) => t1.ToSeconds() < t2.ToSeconds();
    public static bool operator >(Time t1, Time t2) => t1.ToSeconds() > t2.ToSeconds();
    public static bool operator <=(Time t1, Time t2) => t1.ToSeconds() <= t2.ToSeconds();
    public static bool operator >=(Time t1, Time t2) => t1.ToSeconds() >= t2.ToSeconds();

    public static Time operator ++(Time t)
    {
        return FromSeconds(t.ToSeconds() + 1); // +1 секунда
    }

    public static Time operator --(Time t)
    {
        return FromSeconds(t.ToSeconds() - 1); // -1 секунда
    }

    public static bool operator true(Time t) => t.ToSeconds() > 0;
    public static bool operator false(Time t) => t.ToSeconds() == 0;

    private static Time FromSeconds(int total)
    {
        total = (total % (24 * 3600) + (24 * 3600)) % (24 * 3600);
        int h = total / 3600;
        int m = (total % 3600) / 60;
        int s = total % 60;
        return new Time(h, m, s);
    }

    public override string ToString() => $"{hours:D2}:{minutes:D2}:{seconds:D2}";

    public override bool Equals(object? obj) => obj is Time t && this == t;
    public override int GetHashCode() => ToSeconds();
}
