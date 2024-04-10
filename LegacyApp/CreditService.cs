namespace LegacyApp;

internal class CreditService : ICreditService
{
    private readonly IClientCreditRepository _clientCreditRepository = new ClientCreditRepository();

    public int CalculateClientCreditLimit(Client client)
    {
        var creditLimit = _clientCreditRepository.GetClientCreditLimit(client.Name);

        if (client.IsImportantClient())
            creditLimit *= 2; // Double the credit limit for important clients
        
        return creditLimit;
    }
}

internal interface ICreditService
{
    int CalculateClientCreditLimit(Client client);
}