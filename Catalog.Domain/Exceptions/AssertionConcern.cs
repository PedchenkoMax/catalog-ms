using System.Numerics;
using System.Text.RegularExpressions;

namespace Catalog.Domain.Exceptions;

public static class AssertionConcern
{ 

    public static void AssertArgumentLength(string stringValue, int maximum, string message)
    {
        int length = stringValue.Trim().Length;
        if (length > maximum)
        {
            throw new DomainException(message);
        }
    }

    public static void AssertArgumentLength(string stringValue, int minimum, int maximum, string message)
    {
        int length = stringValue.Trim().Length;
        if (length < minimum || length > maximum)
        {
            throw new DomainException(message);
        }
    }

    public static void AssertArgumentMatches(string pattern, string stringValue, string message)
    {
        Regex regex = new Regex(pattern);

        if (!regex.IsMatch(stringValue))
        {
            throw new DomainException(message);
        }
    }

    public static void AssertArgumentNotEmpty(string stringValue, string message)
    {
        if (stringValue == null || stringValue.Trim().Length == 0)
        {
            throw new DomainException(message);
        }
    }  

    public static void AssertArgumentNotNull(object object1, string message)
    {
        if (object1 == null)
        {
            throw new DomainException(message);
        }
    }
   
    public static void AssertArgumentRange<T>(T value, T minimum, T maximum, string message) where T : INumber<T>
    {
        if (value < minimum || value > maximum)
        {
            throw new DomainException(message);
        }
    }
    
    public static void AssertArgumentGreaterThan<T>(T value, T minimum, string message) where T : INumber<T>
    {
        if (value < minimum)
        {
            throw new DomainException(message);
        }
    }
    

    
}