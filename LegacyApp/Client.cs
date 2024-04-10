namespace LegacyApp
{
    internal class Client
    {
        public string Name { get; init; }
        public int ClientId { get; init; }
        public string Email { get; init; }
        public string Address { get; init; }
        public string Type { get; init; }

        public bool IsVeryImportantClient() => Type == "VeryImportantClient";
        public bool IsImportantClient() => Type == "ImportantClient";
    }
}