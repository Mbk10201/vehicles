using Sandbox;
using System.Collections.Generic;

namespace Mbk.Vehicles;

public static partial class Vehicles
{
	private static BaseFileSystem fs => FileSystem.Data;
	public static IDictionary<string, VehicleSound> SoundsList { get; private set; }

	const string CONF_FILE = "vehicles_configuration.json";
	const string SOUNDS_FILE = "vehicles_sounds.json";

	private static void LoadConfiguration()
	{
		if ( !fs.FileExists( SOUNDS_FILE ) )
		{
			SoundsList = new Dictionary<string, VehicleSound>()
			{
				{
					"central_lock",
					new()
					{
						Volume = 1f,
						FilePath = "sounds/doors/central_lock.sound"
					} 
				},
				{
					"central_unlock",
					new()
					{
						Volume = 1f,
						FilePath = "sounds/doors/central_unlock.sound"
					}
				},
				{
					"door_open",
					new()
					{
						Volume = 1f,
						FilePath = "sounds/doors/door_open.sound"
					}
				},
				{
					"door_close",
					new()
					{
						Volume = 1f,
						FilePath = "sounds/doors/door_close.sound"
					}
				},
				{
					"hood_open",
					new()
					{
						Volume = 1f,
						FilePath = "sounds/doors/hood_open.sound"
					}
				},
				{
					"hood_close",
					new()
					{
						Volume = 1f,
						FilePath = "sounds/doors/hood_open.sound"
					}
				},
				{
					"insert_key",
					new()
					{
						Volume = 1f,
						FilePath = "sounds/doors/insert_key.sound"
					}
				},
				{
					"remove_key",
					new()
					{
						Volume = 1f,
						FilePath = "sounds/doors/remove_key.sound"
					}
				},
				{
					"trunk_open",
					new()
					{
						Volume = 1f,
						FilePath = "sounds/doors/trunk_open.sound"
					}
				},
				{
					"trunk_close",
					new()
					{
						Volume = 1f,
						FilePath = "sounds/doors/trunk_close.sound"
					}
				}
			};

			fs.WriteJson( SOUNDS_FILE, SoundsList );
		}
		else
			SoundsList = fs.ReadJsonOrDefault( SOUNDS_FILE, new Dictionary<string, VehicleSound>() );

		foreach( var sound in SoundsList )
		{
			if ( sound.Value.FilePath == "" )
				continue;

			sound.Value.SoundFile = SoundFile.Load( sound.Value.FilePath );
		}
	}
}
