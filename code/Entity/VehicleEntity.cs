using Editor;
using Mbk.Vehicles.Enums;
using Mbk.Vehicles.Resources;
using Sandbox;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mbk.Vehicles;

[Library( "vehicle" )]
[Display( Name = "Vehicle" ), Category( "directions_car" ), Icon( "public" )]
[HammerEntity]
public partial class VehicleEntity : AnimatedEntity, IUse
{
	public virtual bool CanBePorted => true;
	public virtual string DisplayName => "Voiture";

	public bool Debug { get; set; } = true;
	
	public VehicleResource VehicleResource { get; private set; }

	public VehicleEntity()
	{
		WhoHasKeys = new List<IClient>();
	}

	~VehicleEntity() 
	{
		Log.Info( "Vehicle destroyed" );
	}

	public void SetResource( VehicleResource vehiclebase )
	{
		if ( vehiclebase == null )
			return;

		Game.AssertServer();

		VehicleResource = vehiclebase;

		var model = !string.IsNullOrEmpty( vehiclebase.ModelPath ) ? vehiclebase.ModelPath : "models/sbox_props/burger_box/burger_box.vmdl";

		if ( !string.IsNullOrEmpty( model ) )
		{
			SetModel( model );
			SetupPhysicsFromModel( PhysicsMotionType.Dynamic, false );
		}

		DebugOverlay.Skeleton( this, Color.White, 100000 );

		SetupSmoke();
		SetupWheels();
		SetupSeats();
		SetupGears();
	}

	public override void Spawn()
	{
		Event.Run( OnSpawn, this );

		Tags.Add( "vehicle" );
		Tags.Add( "solid" );

		EnableTouch = true;
		//UseAnimGraph = true;
		EnableHitboxes = true;
		Transmit = TransmitType.Always;
		SurroundingBoundsMode = SurroundingBoundsType.Hitboxes;

		base.Spawn();
	}

	void SetupSmoke()
	{
		SmokeEntity = new()
		{
			ParticleSystemName = "smokeparticlesmallcig.vpcf",
			Transform = GetAttachment( "exhaust01" ).Value
		};
	}

	void SetupWheels()
	{
		int maxwheels = GetMaxWheels();

		Axles = new sAxle[maxwheels];

		for(int i = 0; i < maxwheels; i++)
		{
			var wheel = Axles[i].Wheel;

			wheel.Entity = new WheelEntity()
			{
				Parent = this,
				Transform = GetBoneTransform( GetWheelNameByIndex(i) )
			};

			//wheel.Entity.SetResource( VehicleResource.GetDefaultWheel() );
			wheel.Entity.SetResource( Vehicles.WheelsList[0] );

			Axles[i].Wheel = wheel;
		}
	}

	void SetupSeats()
	{
		Game.AssertServer();

		int maxseats = GetMaxSeats();

		DoorsLocked = new bool[maxseats];

		for ( int i = 0; i < maxseats; i++ )
		{
			Log.Info($"Seat number {i} -> created" );
			
			var seat = Components.Create<Seat>();
			seat.SetIndex( i );

			DoorsLocked[i] = true;
		}
	}

	void SetupGears()
	{
		int maxgears = GetMaxGears();

		Gears = new sGear[maxgears];
	}

	string GetWheelNameByIndex( int index )
	{
		switch(index)
		{
			case 0: return "fl_wheel";
			case 1: return "fr_wheel";
			case 2: return "rl_wheel";
			case 3: return "rr_wheel";
			default: return "root";
		}
	}

	public override void FrameSimulate( IClient cl )
	{
		base.FrameSimulate( cl );

		if ( Input.Pressed( "Slot0" ) )
			SetGear( 0 );

		if ( Input.Pressed( "Slot1" ) )
			SetGear( 1 );

		if ( Input.Pressed( "Slot2" ) )
			SetGear( 2 );

		if ( Input.Pressed( "Slot3" ) )
			SetGear( 3 );

		if ( Input.Pressed( "Slot4" ) )
			SetGear( 4 );

		if ( Input.Pressed( "Slot5" ) )
			SetGear( 5 );
	}

	public bool OnUse( Entity user )
	{
		var client = user.Client;

		if ( client == null )
			return false;

		if ( client.InVehicle() )
			return false;

		var tr = Trace.Ray( client.Pawn.AimRay, 512f )
				.Size( 1.0f ) 
				.UseHitboxes()
				.DynamicOnly()
				.Ignore(client.Pawn)
				.WithTag( "vehicle" )
				.Run();

		if ( tr.Hit )
		{
			DebugOverlay.TraceResult( tr, 10f );
			Log.Info( tr.Hitbox.HasAnyTags( "door", "trunk", "engine", "fl_wheel", "fr_wheel", "rl_wheel", "rr_wheel", "door_fl", "door_fr" ) );

			if ( tr.Hitbox.HasTag( "door_fl" ) && !IsDoorLocked( 0 ) )
			{
				if ( Seats[0].IsFree )
					AddClientToSeat( 0, client );
				else
					Log.Warning( "There is already someone on this seat." );

				return false;
			}

			if ( tr.Hitbox.HasTag( "door_fr" ) && !IsDoorLocked( 1 ) )
			{
				if ( Seats[1].IsFree )
					AddClientToSeat( 1, client );
				else
					Log.Warning( "There is already someone on this seat." );

				return false;
			}

			if ( tr.Hitbox.HasTag( "door_rl" ) && !IsDoorLocked( 2 ) )
			{
				if ( Seats[2].IsFree )
					AddClientToSeat( 2, client );
				else
					Log.Warning( "There is already someone on this seat." );

				return false;
			}

			if ( tr.Hitbox.HasTag( "door_rr" ) && !IsDoorLocked( 3 ) )
			{
				if ( Seats[3].IsFree )
					AddClientToSeat( 3, client );
				else
					Log.Warning( "There is already someone on this seat." );

				return false;
			}
		}

		return false;
	}

	public bool IsUsable( Entity user )
	{
		return true;
	}

	public override void Simulate( IClient client )
	{
		Log.Info( "Simulate driver" );

		if ( Seats[0].Client != client )
			return;

		if( Vehicles.IsClientInVehicle( client ) )
		{

		}


		Log.Info("Simulate driver");
	}

	public void SetGear(int gear)
	{
		if ( GetTransmissionType() == eCarTransmission.Automatic )
			return;
		
		Log.Info( "SetGear" );
		//SetAnimParameter( "gear", gear );
	}

	/*[GameEvent.Client.BuildInput]
	public static void ButtonsClient()
	{
		if ( Input.Pressed( "Use" ) )
		{
			var tr = Trace.Ray( Game.LocalPawn.AimRay, 512f )
				.Size( 1.0f )
				.Ignore( Game.LocalPawn )
				.Run();

			DebugOverlay.TraceResult( tr, 10f );
		}
	}*/

	/*[GameEvent.Physics.PreStep]
	public void OnPrePhysicsStep()
	{
		var body = Parent.PhysicsGroup;

		_previousLength = _currentLength;
		_currentLength = (length * parent.Scale) - tr.Distance;

		var springVelocity = (_currentLength - _previousLength) / dt;
		var springForce = body.Mass * 50.0f * _currentLength;
		var damperForce = body.Mass * (1.5f + (1.0f - tr.Fraction) * 3.0f) * springVelocity;
		var velocity = body.GetVelocityAtPoint( wheelAttachPos );
		var speed = velocity.Length;
		var speedDot = MathF.Abs( speed ) > 0.0f ? MathF.Abs( MathF.Min( Vector3.Dot( velocity, rotation.Up.Normal ) / speed, 0.0f ) ) : 0.0f;
		var speedAlongNormal = speedDot * speed;
		var correctionMultiplier = (1.0f - tr.Fraction) * (speedAlongNormal / 1000.0f);
		var correctionForce = correctionMultiplier * 50.0f * speedAlongNormal / dt;

		body.ApplyImpulseAt( wheelAttachPos, tr.Normal * (springForce + damperForce + correctionForce) * dt );
	}*/

	public void Tick()
	{
		/*for ( int i = 0; i < GetMaxWheels(); i++ )
		{
			Axles[i].Tick();
		}*/
	}

	[ConCmd.Server]
	public static void Unlock()
	{
		Log.Info( "[Vehicles] Unlock" );

		var pawn = ConsoleSystem.Caller.Pawn as Entity;

		var tr = Trace.Ray( pawn.AimRay, 512f )
				.Size( 1.0f )
				.UseHitboxes()
				.DynamicOnly()
				.Ignore( pawn )
				.WithTag("vehicle")
				.Run();

		if ( tr.Hit )
		{
			var vehicle = tr.Entity as VehicleEntity;

			if ( !ConsoleSystem.Caller.HasKeys( vehicle ) )
			{
				Log.Info( "[Vehicles] You dont have the keys of this vehicle." );
				return;
			}
			else
				Log.Info( "[Vehicles] You have the keys" );

			if ( tr.Hitbox.HasTag( "door_fl_lock" ) )
			{
				if ( vehicle.IsDoorLocked( 0 ) )
				{
					vehicle.UnlockDoor( 0 );
					Log.Info( "[Vehicles] Door 0 unlocked" );
				}
				else
				{
					vehicle.LockDoor( 0 );
					Log.Info( "[Vehicles] Door 0 locked" );
				}
			}

			if ( tr.Hitbox.HasTag( "door_fr_lock" ) )
			{
				if ( vehicle.IsDoorLocked( 1 ) )
				{
					vehicle.UnlockDoor( 1 );
					Log.Info( "[Vehicles] Door 1 unlocked" );
				}
				else
				{
					vehicle.LockDoor( 1 );
					Log.Info( "[Vehicles] Door 1 locked" );
				}
			}

			if ( tr.Hitbox.HasTag( "door_rl_lock" ) )
			{
				if ( vehicle.IsDoorLocked( 2 ) )
				{
					vehicle.UnlockDoor( 2 );
					Log.Info( "[Vehicles] Door 2 unlocked" );
				}
				else
				{
					vehicle.LockDoor( 2 );
					Log.Info( "[Vehicles] Door 2 locked" );
				}
			}

			if ( tr.Hitbox.HasTag( "door_rr_lock" ) )
			{
				if ( vehicle.IsDoorLocked( 3 ) )
				{
					vehicle.UnlockDoor( 3 );
					Log.Info( "[Vehicles] Door 3 unlocked" );
				}
				else
				{
					vehicle.LockDoor( 3 );
					Log.Info( "[Vehicles] Door 3 locked" );
				}
			}
		}
	}

	[ConCmd.Client]
	public static void InVehicle()
	{
		var client = ConsoleSystem.Caller;

		Log.Info( Vehicles.IsClientInVehicle( client ));
	}
}
