namespace Restaurants.Domin;

public class UserNotConnected(string message ="User is not connected") : Exception(message)
{

}
