namespace DartsLearning;

public static class DartsVisit
{
    // Sum the score of the shots of three darts
    public static int CalculateVisitScore(string[] visit)
    {
        ArgumentNullException.ThrowIfNull(visit);
        
        var totalScore = 0;
        foreach (var shot in visit)
        {
            totalScore += ParseDartShot(shot);
        }
        return totalScore;
    }

    // Shot is in format XYY, where X is Single, Double or Triple and YY is a number that was hit 
    private static int ParseDartShot(string dart)
    {
        if (string.IsNullOrWhiteSpace(dart))
        {
            throw new FormatException("The value cannot be null, empty, or whitespace.");
        }
        
        dart = dart.Trim();

        var multiplier = dart[0] switch
        {
            'S' or 's' => 1,
            'D' or 'd' => 2,
            'T' or 't' => 3,
            _ => throw new FormatException($"Unsupported multiplier '{dart[0]}'.")
        };

        var numberPart = dart.AsSpan()[1..];
        if (!int.TryParse(numberPart, out int number))
        {
            throw new FormatException($"Unsupported number part '{numberPart}'.");
        }
        
        if (number < 0 || (number > 20 && number != 25))
        {
            throw new ArgumentOutOfRangeException(
                nameof(dart), 
                $"The number '{number}' is out of range."
            );
        }
        
        if (multiplier == 3 && number == 25)
        {
            throw new ArgumentException("Unsupported multiplier for bull.");
        }
        
        return number * multiplier;
    }
}