using System;

namespace LegacyApp
{
    public class UserService
    {
        // Introduced here interfaces but dependency injection cannot be achieved because 'Program.cs' file cannot be changed.
        private readonly IUserCreditService _userCreditService = new UserCreditService();
        private readonly IClientRepository _clientRepository = new ClientRepository();

        // Method to add a user
        // Delegating tasks such as input validation, credit limit calculation, and data storage
        // to separate methods (Single Responsibility Principle).
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            // Validate user
            UserValidator.ValidateUser(firstName, lastName, email, dateOfBirth);

            var client = _clientRepository.GetById(clientId);

            if (client is null)
                return false;

            // Calculate credit limit based on client type
            var creditLimit = _userCreditService.CalculateUserCreditLimit(client, lastName);

            var user = new User(client, dateOfBirth, email, firstName, lastName, creditLimit);

            // Check if credit limit meets criteria
            if (user.HasCreditLimit && user.CreditLimit < 500)
                return false;

            // Add user to data storage
            UserDataAccess.AddUser(user);

            return true;
        }
    }
}