using Sandbox;

namespace Mbk.Vehicles;

public static class ClientExtensions
{
	public static bool InVehicle( this IClient client )
	{
		return Vehicles.IsClientInVehicle(client);
	}

	public static bool IsVehicleDriver( this IClient client )
	{
		return Vehicles.IsClientDriver(client);
	}

	public static bool HasKeys( this IClient client, VehicleEntity vehicle )
	{
		return vehicle.HasKeys(client);
	}
}
