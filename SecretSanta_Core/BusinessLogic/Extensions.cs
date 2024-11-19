namespace SecretSanta_Core.BusinessLogic
{
    public static class Extensions
    {

        public static int SafeInt(this int? input)
        {
            return input.HasValue ? input.Value : 0;
        }

        public static string SafeString(this string input)
        {
            return input != null ? input : String.Empty;
        }

        public static int GetSafeInt(this object o)
        {
            if (o == DBNull.Value) return 0;

            return Convert.ToInt32(o);
        }

        public static string GetSafeString(this object o)
        {
            if (o == DBNull.Value) return String.Empty;

            return o.ToString();
        }
    }
}
