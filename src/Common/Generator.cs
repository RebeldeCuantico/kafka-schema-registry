namespace Common
{
    public static class Generator
    {
        private static readonly Random RandomGenerator = new Random();

        public static string GetRegistration()
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";
           
            var lettersPart = new string(Enumerable.Range(0, 3).Select(_ => letters[RandomGenerator.Next(letters.Length)]).ToArray());           
            var numbersPart = new string(Enumerable.Range(0, 4).Select(_ => numbers[RandomGenerator.Next(numbers.Length)]).ToArray());
          
            return $"{lettersPart}{numbersPart}";
        }
       
        public static int GetSpeed()
        {
            return RandomGenerator.Next(50, 121);
        }
      
        public static string GetCoordinates()
        {
            double minLatitude = 36.0;
            double maxLatitude = 43.8;
            double minLongitude = -9.3;
            double maxLongitude = 4.3;

            double latitude = RandomGenerator.NextDouble() * (maxLatitude - minLatitude) + minLatitude;
            double longitude = RandomGenerator.NextDouble() * (maxLongitude - minLongitude) + minLongitude;

            return $"{latitude},{longitude}";
        }
    }

}
