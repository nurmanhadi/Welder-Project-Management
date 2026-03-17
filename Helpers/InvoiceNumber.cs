namespace WelderProjectManagement.Helpers;

public class GenerateInvoiceNumber
{
    public static string New(int countToday)
    {
        return $"INV-{DateTime.UtcNow:yyyyMMdd}-{(countToday + 1):0000}";
    }
}
