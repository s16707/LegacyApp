﻿using System;

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

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            
            
            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
            {
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                }
            }
            else
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }
            }
            
            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }
    }
}
