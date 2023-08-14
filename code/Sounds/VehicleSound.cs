using Sandbox;
using System.Text.Json.Serialization;

namespace Mbk.Vehicles;

[Category("Sounds")]
public class VehicleSound
{
	public float Volume { get; set; }
	public string FilePath { get; set; }


	[JsonIgnore]
	public SoundFile SoundFile { get; set; }

	[JsonIgnore]
	public bool IsPlaying { get; set; }

	public void Play()
	{

	}
	
	public void Stop()
	{

	}
}
