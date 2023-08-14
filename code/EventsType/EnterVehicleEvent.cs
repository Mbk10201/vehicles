using Sandbox;

namespace Mbk.Vehicles.EventsType;

public class ClientEnterVehicleEvent
{
	public IClient Client { get; private set; }
	public bool Successful { get; private set; }
	public Vector3 Position { get; private set; }

	public ClientEnterVehicleEvent( IClient client, bool successful, Vector3 position )
	{
		Client = client;
		Successful = successful;
		Position = position;
	}
}
