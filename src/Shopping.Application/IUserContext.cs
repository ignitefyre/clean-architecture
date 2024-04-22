namespace Shopping.Application;

public interface IUserContext
{
    bool IsAuthenticated { get; }

    string UserId { get; }
    
    string Name { get; }
    
    string Title { get; }

}