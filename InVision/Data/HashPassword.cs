namespace InVision.Data
{
    public class HashPassword
    {
        public string? Hashed { get; set; }
        public byte[]? Salt { get; set; }

        public HashPassword()
        {
        }

        public HashPassword(string? hashed, byte[]? salt)
        {
            Hashed = hashed;
            Salt = salt;
        }
    }
}
