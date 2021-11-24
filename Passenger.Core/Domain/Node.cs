namespace Passenger.Core.Domain;

public class Node
{
    public Node(string address, string longitude, string latitude)
    {
        Address = address;
        Longitude = longitude;
        Latitude = latitude;
    }

    public string Address { get; protected set; }
    public string Longitude { get; protected set; }
    public string Latitude { get; protected set; }
}