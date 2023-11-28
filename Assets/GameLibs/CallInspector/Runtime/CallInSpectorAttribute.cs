using System;

[AttributeUsage(AttributeTargets.Field)]
public class CallInSpectorAttribute : Attribute
{
    public CallInSpectorAttribute()
    {
    }

    public string Name { get; set; }
}