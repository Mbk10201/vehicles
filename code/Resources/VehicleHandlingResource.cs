using Mbk.Vehicles.Enums;
using Sandbox;

namespace Mbk.Vehicles.Resources;

[GameResource( "Vehicle Handling", "handling", "Describe and configure a handling for a vehicle", Icon = "tire_repair", IconBgColor = "white", IconFgColor = "dodgerblue" )]
public partial class VehicleHandlingResource : GameResource
{
	[Description( "The amount of wheels on this vehicle." )]
	public int WheelsCount { get; private set; } = 4;

	[Description( "The amount of seats on this vehicle." )]
	public int SeatsCount { get; private set; } = 4;

	[Description( "The mass of the vehicle (KG)." )]
	public float Mass { get; private set; } = 1000f;

	[Description( "The oil volume of the vehicle." )]
	public float OilVolume { get; private set; } = 5f;

	[Description( "The coolan volume of the vehicle." )]
	public float CoolantVolume { get; private set; } = 10f;

	[Description( "The type of transmission (automatic or manual)." )]
	public eCarTransmission Transmission { get; private set; } = eCarTransmission.Automatic;

	[Description( "The type of drive wheel (automatic or manual)." )]
	public eDriveWheel DriveWheel { get; private set; } = eDriveWheel.RWD;

	[Description( "The vehicle category (EX: SUV)." )]
	public eCarCategory Category { get; private set; }

	[Description( "The amount of cylinders." )]
	public int Cylinders { get; private set; } = 6;

	[Description( "The gears mount if manual." )]
	public int GearsCount { get; private set; } = 6;

	[Description( "The centimer cube of the engine." )]
	public int CM3 { get; private set; } = 2000;

	[Description( "The horses power of the vehicle." )]
	public int HorsePower { get; private set; }

	[Description( "The max speed of the vehicle. (KM/H)" )]
	public int MaxSpeed { get; private set; } = 250;

	[Description( "The price of the vehicle. (€)" )]
	public int Price { get; private set; }
	
	[Description( "If the vehicle has ABS" )]
	public bool HasABS { get; private set; } = true;

	[Range( 0f, 0.5f ), Category( "Gears"), Description("Shifting delay")]
	public float GearShiftingDelay { get; private set; } = 0.35f;

	[Range(0.25f, 1f), Category( "Gears" ), Description( "Shifting threshold" )]
	public float GearShiftingThreshold { get; private set; } = 0.75f;

	[Range( 0.1f, 0.9f ), Category( "Gears" ), Description( "Clutch inertia" )]
	public float ClutchInertia { get; private set; } = 0.25f;

	[Range( 2500f, 10000f ), Category( "Gears" ), Description( "Shifting up when engine RPM is high enough." )]
	public float GearShiftUpRPM { get; private set; } = 6500f;

	[Range( 2700f, 3500f ), Category( "Gears" ), Description( "Shifting down when engine RPM is low enough." )]
	public float GearShiftDownRPM { get; private set; } = 3500f;

	[Category( "Fuel" ), Description( "The type of fuel (gasoline, diesel or electric)." )]
	public eFuelType FuelType { get; private set; } = eFuelType.Gasoline;

	[Range( 5f, 120f ), Category( "Fuel" ), Description( "The tank volume of the vehicle." )]
	public float TankVolume { get; private set; } = 50f;

	[Category( "Fuel" ), Description( "The consumption of liter at 100KM." )]
	public float ConsumptionRatio { get; private set; } = 0.1f;

	[Category( "Engine" ), Description( "Auto create engine torque curve. If min/max engine rpm, engine torque, max engine torque at rpm, or top speed has been changed at runtime, it will generate new curve with them." )]
	public bool AutoGenerateEngineRPMCurve { get; private set; } = true;

	[Category( "Engine" ), Description( "Maximum engine torque at target RPM." )]
	public float MaxEngineTorque { get; private set; } = 300f;

	[Category( "Engine" ), Description( "Maximum peek of the engine at this RPM." )]
	public float MaxEngineTorqueAtRPM { get; private set; } = 5500f;

	[Category( "Engine" ), Description( "Minimum engine RPM." )]
	public float MinEngineRPM { get; private set; } = 1000f;

	[Category( "Engine" ), Description( "Maximum engine RPM." )]
	public float MaxEngineRPM { get; private set; } = 7000f;

	[Range( 0.02f, 0.4f ), Category( "Engine" ), Description( "Engine inertia. Engine reacts faster on lower values." )]
	public float EngineInertia { get; private set; } = 0.15f;

	[Category( "Engine" ), Description( "Rev limiter above maximum engine RPM. Cuts gas when RPM exceeds maximum engine RPM." )]
	public bool UseRevLimiter { get; private set; } = true;

	[Category( "Engine" ), Description( "Exhaust blows flame when driver cuts gas at certain RPMs." )]
	public bool UseExhaustFlame { get; private set; } = true;

	[Category( "Heat" ), Description( "Enable / Disable engine heat." )]
	public bool UseEngineHeat { get; private set; } = true;

	[Category( "Heat" ), Description( "Engine heat." )]
	public float EngineHeat { get; private set; } = 15f;

	[Category( "Heat" ), Description( "Engine cooling water engage point." )]
	public float EngineCoolingWaterThreshold { get; private set; } = 90f;

	[Category( "Heat" ), Description( "Engine heat multiplier." )]
	public float EngineHeatRate { get; private set; } = 1f;

	[Category( "Heat" ), Description( "Engine cool multiplier." )]
	public float EngineCoolRate { get; private set; } = 1f;

	protected override void PostReload()
	{
		if ( Game.IsServer || Game.IsClient )
		{
			Vehicles.ReloadDefinitions();
		}

		base.PostReload();
	}
}
