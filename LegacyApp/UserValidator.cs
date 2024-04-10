using System;

namespace LegacyApp;

// Class responsible for validating User.
internal static class UserValidator
{
    public static bool ValidateUser(string firstName, string lastName, string email, DateTime dateOfBirth)
    {
        return IsNameValid(firstName, lastName) && IsEmailValid(email) && IsAgeValid(dateOfBirth);
    }

    private static bool IsNameValid(string firstName, string lastName)
    {
        return !string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName);
    }

    private static bool IsEmailValid(string email)
    {
        return email.Contains('@') && email.Contains('.');
    }

    private static bool IsAgeValid(DateTime dateOfBirth)
    {
        var age = CalculateAge(dateOfBirth);
        return age >= 21;
    }

    private static int CalculateAge(DateTime dateOfBirth)
    {
        var now = DateTime.Now;
        var age = now.Year - dateOfBirth.Year;
        if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
            age--;
        return age;
    }
}