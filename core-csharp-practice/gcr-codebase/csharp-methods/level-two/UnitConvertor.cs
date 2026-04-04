using System;
/// <summary>
/// Simple UnitConvertor with static conversion methods (straight one-liners).
/// </summary>
public static class UnitConvertor
{
    // kilometers / miles
    public static double KmToMiles(double km) => km * 0.621371;
    public static double MilesToKm(double miles) => miles * 1.60934;

    // meters / feet / inches / cm / yards
    public static double MetersToFeet(double m) => m * 3.28084;
    public static double FeetToMeters(double ft) => ft * 0.3048;
    public static double YardsToFeet(double yards) => yards * 3.0;
    public static double FeetToYards(double ft) => ft * 0.333333;
    public static double MetersToInches(double m) => m * 39.3701;
    public static double InchesToMeters(double inches) => inches * 0.0254;
    public static double InchesToCentimeters(double inches) => inches * 2.54;

    // temperature
    public static double FahrenheitToCelsius(double f) => (f - 32) * 5.0 / 9.0;
    public static double CelsiusToFahrenheit(double c) => (c * 9.0 / 5.0) + 32;

    // mass / volume
    public static double PoundsToKilograms(double pounds) => pounds * 0.453592;
    public static double KilogramsToPounds(double kg) => kg * 2.20462;
    public static double GallonsToLiters(double gallons) => gallons * 3.78541;
    public static double LitersToGallons(double liters) => liters * 0.264172;
}