namespace WelderProjectManagement.Exceptions;

public class ProjectNotFoundException : Exception
{
    public ProjectNotFoundException()
    {
    }

    public ProjectNotFoundException(long id) : base($"project {id} not found")
    {
    }
}
