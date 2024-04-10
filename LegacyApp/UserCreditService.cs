namespace LegacyApp;

internal class UserCreditService : IUserCreditService
{
    private readonly IUserCreditRepository _userCreditRepository = new UserCreditRepository();

    public int CalculateUserCreditLimit(Client client, string userLastName)
    {
        var creditLimit = _userCreditRepository.GetUserCreditLimit(userLastName);

        if (client.IsImportantClient())
            creditLimit *= 2; // Double the credit limit for important clients
        
        return creditLimit;
    }
}

internal interface IUserCreditService
{
    int CalculateUserCreditLimit(Client client, string userLastName);
}