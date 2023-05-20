namespace SostavSD.Shared
{
    public class PrintType
    {
        internal static List<string> AllTypes
        {
            get
            {
                return new List<string> {
                OneSided,
                TwoSided,
                DontPrint,
                };
            }
        }

        public const string OneSided = "односторонняя";
        public const string TwoSided = "двухстороняя";
        public const string DontPrint = "не печатать";
    }
}
