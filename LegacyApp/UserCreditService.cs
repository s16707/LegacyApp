namespace LegacyApp;

internal class UserCreditService : IUserCreditService
{
    private readonly IUserCreditRepository _userCreditRepository = new UserCreditRepository();
    
    public void CalculateUserCreditLimit(User user)
    {
        if (user.Client.Type == "VeryImportantClient")
        {
            user.HasCreditLimit = false;
        }
        else
        {
            user.HasCreditLimit = true;
            user.CreditLimit = _userCreditRepository.GetUserCreditLimit(user.LastName);
            if (user.Client.Type == "ImportantClient")
                user.CreditLimit *= 2; // Double the credit limit for important clients
        }
    }
}

internal interface IUserCreditService
{
    void CalculateUserCreditLimit(User user);
}