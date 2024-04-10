using System;

namespace LegacyApp
{
    public class UserService
    {
        //Method to add a user
        //Delegating tasks such as input validation, age calculation, credit limit setting, and data storage
        // to separate methods (Single Responsibility Principle).
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            // Checking for invalid inputs
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || !IsValidEmail(email))
                return false;

            // Checking age requirement
            var age = CalculateAge(dateOfBirth);
            if (age < 21)
                return false;

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            if (client is null)
                return false;

            //Create user object
            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            // Set credit limit based on client type
            SetCreditLimit(user);

            // Check if credit limit meets criteria
            if (user.HasCreditLimit && user.CreditLimit < 500)
                return false;

            // Add user to data storage
            UserDataAccess.AddUser(user);

            return true;
        }

        // Method to calculate age
        // Extracting age calculation into a separate method to improve code readability and promote
        // reusability (SRP).
        private static int CalculateAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
                age--;
            return age;
        }

        // Method to calculate age
        // Separating email validation into a separate method to improve code readability and promote
        // reusability (SRP).
        private static bool IsValidEmail(string email)
        {
            return email.Contains('@') && email.Contains('.');
        }

        // Method to set credit limit for a user
        // Extracting credit limit logic into a separate method improves code readability and promotes
        // reusability (SRP).
        private void SetCreditLimit(User user)
        {
            // Check client type and set credit limit accordingly
            if (user.Client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else
            {
                user.HasCreditLimit = true;
                var userCreditService = new UserCreditService();
                user.CreditLimit = userCreditService.GetCreditLimit(user.LastName);
                if (user.Client.Type == "ImportantClient")
                    user.CreditLimit *= 2; // Double the credit limit for important clients
            }
        }
    }
}