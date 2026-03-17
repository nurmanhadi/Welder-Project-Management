namespace WelderProjectManagement.Exceptions;

public class ClientNotFoundException : Exception
{
    public ClientNotFoundException()
    {
    }

    public ClientNotFoundException(long id) : base($"client {id} not found")
    {
    }
}
