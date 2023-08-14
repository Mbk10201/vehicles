using Sandbox;
using Sandbox.Diagnostics;
using System;

namespace Mbk.Vehicles;

public partial class VehicleEntity
{
	new public void PlaySound( string name )
	{
		if ( !Vehicles.SoundsList.ContainsKey( name ) )
		{
			throw new Exception( $"[Vehicles] Sound name {name} not found !" );
		}

		var file = Vehicles.SoundsList[name];

		var sound = Sound.FromEntity( file.FilePath, this );
		sound.SetVolume( file.Volume );
	}

	/*[ConCmd.Server]
	public static void TestSound(string sound)
	{
		foreach(var vehicle in Vehicles.SpawnedVehicles)
		{
			vehicle.PlaySound( sound );
		}
	}*/
}
