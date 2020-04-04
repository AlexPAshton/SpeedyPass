namespace SpeedyPass.Services
{
    interface IDataEncryptionService
    {
        string Salt { get; set; }

        string Encrypt(string fullEnteredPin);
        string Decrypt(string encryptedEnteredPin);
        bool CompareEqual(string plainText, string hashedText);
    }
}
