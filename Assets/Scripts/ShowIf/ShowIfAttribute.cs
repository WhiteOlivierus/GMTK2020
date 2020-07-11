using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class ShowIfAttribute : PropertyAttribute
{
    public string ComparedPropertyName { get; private set; }
    public object ComparedValue { get; private set; }
    public ShowIfComparisonType ComparisonType { get; private set; }

    public ShowIfAttribute(string comparedPropertyName, object comparedValue, ShowIfComparisonType comparisonType)
    {
        ComparedPropertyName = comparedPropertyName;
        ComparedValue = comparedValue;
        ComparisonType = comparisonType;
    }
}
