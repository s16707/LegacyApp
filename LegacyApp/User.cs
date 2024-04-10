using System;

namespace LegacyApp
{
    internal class User
    {
        public Client Client { get; }
        public DateTime DateOfBirth { get; }
        public string EmailAddress { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public bool HasCreditLimit { get; }
        public int CreditLimit { get; }

        public User(
            Client client,
            DateTime dateOfBirth,
            string emailAddress,
            string firstName,
            string lastName,
            int creditLimit)
        {
            Client = client;
            DateOfBirth = dateOfBirth;
            EmailAddress = emailAddress;
            FirstName = firstName;
            LastName = lastName;
            HasCreditLimit = !client.IsVeryImportantClient();
            CreditLimit = creditLimit;
        }
    }
}