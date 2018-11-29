namespace System
{
    public static class DateTimeExtensions
    {
        public static int GetAge(this DateTime dataNascimento)
        {
            if (DateTime.Today.Month < dataNascimento.Month ||
                    DateTime.Today.Month == dataNascimento.Month &&
                    DateTime.Today.Day < dataNascimento.Day)            
                return DateTime.Today.Year - dataNascimento.Year - 1;                        
            return DateTime.Today.Year - dataNascimento.Year;
        }
    }
}
