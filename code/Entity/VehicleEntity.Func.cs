using Mbk.Vehicles.Enums;
using Mbk.Vehicles.EventsType;
using Sandbox;
using System.Linq;
using System.Threading.Tasks;

namespace Mbk.Vehicles;

public partial class VehicleEntity
{
	/*public int GetSpeed() => Operating.Speed;
	public int GetRPM() => Operating.EngineRPM;
	public float GetBoostDelay() => Operating.BoostDelay;
	public int GetBoostTimeLeft() => Operating.BoostTimeLeft;
	public float GetSkidSpeed() => Operating.SkidSpeed;
	public int GetSkidMaterial() => Operating.SkidMaterial;
	public float GetSteeringAngle() => Operating.SteeringAngle;
	public int GetMaxSpeed() => VehicleResource.Handling.MaxSpeed;
	public int GetMaxRPM() => VehicleResource.Handling.MaxRPM;
	public sWheel GetWheel(int index) => Axles[index].Wheel;
	public bool IsEngineOff() => Engine.Ignition == eEngineIgnition.Off;*/

	public int GetMaxWheels() => VehicleResource.Handling.WheelsCount;
	public int GetMaxSeats() => VehicleResource.Handling.SeatsCount;
	public float GetMass() => VehicleResource.Handling.Mass;
	public float GetMaxPetrolVolume() => VehicleResource.Handling.TankVolume;
	public float GetMaxOilVolume() => VehicleResource.Handling.OilVolume;
	public float GetMaxCoolantVolume() => VehicleResource.Handling.CoolantVolume;
	public float GetConsumptionRatio() => VehicleResource.Handling.ConsumptionRatio;
	public eCarTransmission GetTransmissionType() => VehicleResource.Handling.Transmission;
	public eFuelType GetFuelType() => VehicleResource.Handling.FuelType;
	public eCarCategory GetCategory() => VehicleResource.Handling.Category;
	public int GetMaxCylinders() => VehicleResource.Handling.Cylinders;
	public int GetMaxGears() => VehicleResource.Handling.GearsCount;
	public int GetCM3() => VehicleResource.Handling.CM3;
	public int GetEngineDisplacement() => VehicleResource.Handling.CM3;
	public int GetHP() => VehicleResource.Handling.HorsePower;
	public int GetHorsePower() => VehicleResource.Handling.HorsePower;
	public int GetPrice() => VehicleResource.Handling.Price;
	public bool HasABS() => VehicleResource.Handling.HasABS;

	public Seat GetDriver() => Seats[0];
	
	public void GetFreeSeat(out int index)
	{
		index = Seats.FindIndex( 0, Seats.Count(), (x => x == null) );
	}

	public void AddClientToSeat( int index, IClient client )
	{
		if ( index > Seats.Count )
			return;

		Game.AssertServer();

		Log.Info( $"[Vehicles] Added {client.Name} to the seat {index}" );

		var seat = Components.GetAll<Seat>().Single(x => x.Index == index);

		if ( seat == null )
			return;

		seat.AttachClient(client);

		Vehicles.ShowHud( To.Single( client ) );
	}

	public void UpdateBooster( float dt )
	{

	}

	public void SetThrottle( float value )
	{
	}

	public void SetSpringLength( int wheelIndex, float length )
	{

	}

	public void SetWheelFriction( int wheelIndex, float friction)
	{

	}

	protected virtual void OnVehicleEnter()
	{
		Event.Run( OnEnter, new ClientEnterVehicleEvent(Game.LocalClient, true, Game.LocalClient.Position) );
	}

	protected virtual void OnVehicleExit()
	{
		Event.Run( OnEnter, new ClientExitVehicleEvent( Game.LocalClient, true, Game.LocalClient.Position ) );
	}

	protected virtual void StartEngine( )
	{
		if ( Engine.Ignition == eEngineIgnition.On )
			return;

		_ = StartEngineDelayed();
	}

	public async Task StartEngineDelayed()
	{
		if ( Engine.Ignition == eEngineIgnition.On )
			return;

		/*engineStartSound = NewAudioSource( RCC_Settings.Instance.audioMixer, gameObject, engineSoundPosition, "Engine Start AudioSource", 1, 10, 1, engineStartClip, false, true, true );

		if ( engineStartSound.isPlaying )
			engineStartSound.Play();

		Engine.Ignition = eEngineIgnition.On;
		fuelInput = 1f;

		await Task.DelayRealtimeSeconds( 1f );
		*/
	}

	public void GiveKeys(IClient client)
	{
		if ( WhoHasKeys.Contains( client ) )
			return;

		Log.Info( $"[Vehicles] {client} has now the keys of {this}" );
		WhoHasKeys.Add( client );
	}

	public bool IsDoorLocked(int index) => DoorsLocked[index];
	public void UnlockDoor(int index) => DoorsLocked[index] = false;
	public void LockDoor( int index ) => DoorsLocked[index] = true;

	public bool HasKeys( IClient client ) => WhoHasKeys.Contains( client );
}
