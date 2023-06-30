namespace FreeCourse.Services.Discount.Services.Constants.QueryConstants
{
    public static class Queries
    {
        public static string SelectQuery = "SELECT [%FILTER%] FROM [%TABLE%] ";
        public static string InsertQuery = "INSERT INTO [%TABLE%] ([%COLUMNS%]) VALUES ([%VALUES%]) ";
    }
}
