﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace LegacyApp
{
    public class UserCreditService
    {
        /// <summary>
        /// Simulating database
        /// </summary>
        private static readonly Dictionary<string, int> Database =
            new Dictionary<string, int>()
            {
                {"Kowalski", 200},
                {"Malewski", 20000},
                {"Smith", 10000},
                {"Doe", 3000},
                {"Kwiatkowski", 1000}
            };

        /// <summary>
        /// This method is simulating contact with remote service which is used to get info about someone's credit limit
        /// </summary>
        /// <returns>Client's credit limit</returns>
        internal int GetCreditLimit(string lastName)
        {
            var randomWaitingTime = new Random().Next(3000);
            Thread.Sleep(randomWaitingTime);

            return Database.GetValueOrDefault(lastName);
        }
    }
}