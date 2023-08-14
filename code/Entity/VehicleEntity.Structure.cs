using Mbk.Vehicles.Enums;
using System.Numerics;

namespace Mbk.Vehicles;

public partial class VehicleEntity
{
	public sControls Controls { get; private set; } = new();
	public sOperating Operating { get; private set; } = new();
	public sEngine Engine { get; private set; } = new();
	public sSteering Steering { get; private set; } = new();
	public sBody Body { get; private set; } = new();
	public sAxle[] Axles { get; private set; }
	public sGear[] Gears { get; private set; }

	public struct sControls
	{
		public float Throttle { get; set; }
		float Steering { get; set; }
		float Brake { get; set; }
		float Boost { get; set; }
		bool Handbrake { get; set; }
		bool HandbrakeLeft { get; set; }
		bool HandbrakeRight { get; set; }
		bool Brakepedal { get; set; }
		bool HasBrakePedal { get; set; }
		bool AnalogSteering { get; set; }
	}

	public struct sOperating
	{
		public int Speed { get; set; }
		public int EngineRPM { get; set; }
		public eGearType Gear { get; set; } = eGearType.N;
		public float BoostDelay { get; set; }
		public int BoostTimeLeft { get; set; }
		public float SkidSpeed { get; set; }
		public int SkidMaterial { get; set; }
		public float SteeringAngle { get; set; }
		public int WheelsNotInContact { get; set; }
		public int WheelsInContact { get; set; }
		public bool IsTorqueBoosting { get; set; }

		public sOperating()
		{
			
		}
	}

	public struct sEngine
	{
		public eEngineIgnition Ignition { get; set; }
		public float RPM { get; set; }
		public float RPMRaw { get; set; }
		public bool EngineRunning => Ignition == eEngineIgnition.On;

		private float OldEngineTorque { get; set; } = 0f;             // Old engine torque used for recreating the engine curve.
		private float OldMaxTorqueAtRPM { get; set; } = 0f;           // Old max torque used for recreating the engine curve.
		private float OldMinEngineRPM { get; set; } = 0f;             // Old min RPM used for recreating the engine curve.
		private float OldMaxEngineRPM { get; set; } = 0f;             // Old max RPM used for recreating the engine curve.

		public sEngine()
		{
			
		}
	}

	public struct sBody
	{
		// Maximum brake torque.,
		public float BrakeTorque { get; set; } = 2000f;

		// Applies downforce related with vehicle speed.
		public float DownForce { get; set; } = 25f;

		// Used for resetting the vehicle if upside down.
		public float ResetTime { get; set; } = 0f;


		public sBody()
		{
			
		}
	}

	public struct sSteering
	{
		public float DegreesSlow { get; set; }          // angle in degrees of steering at slow speed
		public float DegreesFast { get; set; }          // angle in degrees of steering at fast speed
		public float DegreesBoost { get; set; }        // angle in degrees of steering at fast speed
		public float SteeringRateSlow { get; set; }     // this is the speed the wheels are steered when the vehicle is slow
		public float SteeringRateFast { get; set; }     // this is the speed the wheels are steered when the vehicle is "fast"
		public float SteeringRestRateSlow { get; set; } // this is the speed at which the wheels move toward their resting state (straight ahead) at slow speed
		public float SteeringRestRateFast { get; set; } // this is the speed at which the wheels move toward their resting state (straight ahead) at fast speed
		public float SpeedSlow { get; set; }                // this is the max speed of "slow"
		public float SpeedFast { get; set; }                // this is the min speed of "fast"
		public float TurnThrottleReduceSlow { get; set; }       // this is the amount of throttle reduction to apply at the maximum steering angle
		public float TurnThrottleReduceFast { get; set; }       // this is the amount of throttle reduction to apply at the maximum steering angle
		public float BrakeSteeringRateFactor { get; set; }  // this scales the steering rate when the brake/handbrake is down
		public float ThrottleSteeringRestRateFactor { get; set; }   // this scales the steering rest rate when the throttle is down
		public float PowerSlideAccel { get; set; }      // scale of speed to acceleration
		public float BoostSteeringRestRateFactor { get; set; }  // this scales the steering rest rate when boosting
		public float BoostSteeringRateFactor { get; set; }  // this scales the steering rest rate when boosting
		public float SteeringExponent { get; set; }     // this makes the steering response non-linear.  The steering function is linear, then raised to this power

		public bool IsSkidAllowed { get; set; }        // true/false skid flag
		public bool DustCloud { get; set; }            // flag for creating a dustcloud behind vehicle
	};

	public struct sWheel
	{
		public float Radius { get; set; }
		public float Mass { get; set; }
		public float Inertia { get; set; }
		public float Damping { get; set; }      // usually 0
		public float Rotdamping { get; set; }       // usually 0
		public float FrictionScale { get; set; }    // 1.5 front, 1.8 rear
		public int MaterialIndex { get; set; }
		public int BrakeMaterialIndex { get; set; }
		public int SkidMaterialIndex { get; set; }
		public float SpringAdditionalLength { get; set; }   // 0 means the spring is at it's rest length

		public WheelEntity Entity { get; set; }

		public void Tick()
		{
			Log.Info( "[Vehicles] Wheel Tick" );
		}
	}

	public struct sSuspension
	{
		public float SpringConstant { get; set; }
		public float SpringDamping { get; set; }
		public float StabilizerConstant { get; set; }
		public float SpringDampingCompression { get; set; }
		public float MaxBodyForce { get; set; }
	}

	public struct sAxle
	{
		public Vector3 Offset { get; set; }                 // center of this axle in vehicle object space
		public Vector3 WheelOffset { get; set; }        // offset to wheel (assume other wheel is symmetric at -wheelOffset) from axle center
		public Vector3 RaytraceCenterOffset { get; set; }    // offset to center of axle for the raytrace data.
		public Vector3 RaytraceOffset { get; set; }          // offset to raytrace for non-wheel (some wheeled) vehicles
		public sWheel Wheel { get; set; } = new();
		public sSuspension Suspension { get; set; }
		public float TorqueFactor { get; set; }     // normalized to 1 across all axles 					// e.g. 0,1 for rear wheel drive - 0.5,0.5 for 4 wheel drive
		public float BrakeFactor { get; set; }      // normalized to 1 across all axles

		public sAxle()
		{
			
		}

		public void Tick()
		{
			Wheel.Tick();
		}
	}

	public struct sGear
	{
		public float MaxRatio { get; set; }
		public int MaxSpeed { get; set; }
		public int TargetSpeedForNextGear { get; set; }
	
		public void SetGear(float ratio, int speed, int targetSpeed) 
		{
			MaxRatio = ratio;
			MaxSpeed = speed;
			TargetSpeedForNextGear = targetSpeed;
        }
	}
}
