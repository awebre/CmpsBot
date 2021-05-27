namespace CmpsBot
{
    public static class Roles
    {
        public static string IT => "Information Technology";

        public static string Alumni => "Alumni";

        public static string Scientific => "Scientific";

        public static string OtherMajor => "Other Major";

        public static string PreMBA => "Pre-MBA";

        public static string DataScience => "Data Science";

        public static string Math => "Math";

        public static string Physics => "Physics";

        public static string[] CSMajors => new string[] {Scientific, IT, PreMBA, DataScience};
        
        public static string[] NonCSMajors => new string[] {Math, Physics, OtherMajor};
    }
}