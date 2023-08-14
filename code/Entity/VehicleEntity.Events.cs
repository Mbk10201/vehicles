using Mbk.Vehicles.EventsType;
using Sandbox;
using System;

namespace Mbk.Vehicles;

public partial class VehicleEntity
{
	public const string OnSpawn = "OnSpawn";
	public class OnSpawnAttribute : EventAttribute
	{
		public OnSpawnAttribute() : base( OnSpawn ) { }
	}

	public const string OnEngineStart = "OnEngineStart";
	public class OnEngineStartAttribute : EventAttribute
	{
		public OnEngineStartAttribute() : base( OnEngineStart ) { }
	}

	public const string OnEngineStop = "OnEngineStop";
	public class OnEngineStopAttribute : EventAttribute
	{
		public OnEngineStopAttribute() : base( OnEngineStop ) { }
	}

	public const string OnEnter = "OnEnter";
	[MethodArguments( new Type[] { typeof( ClientEnterVehicleEvent ) } )]
	public class OnEnterAttribute : EventAttribute
	{
		public OnEnterAttribute() : base( OnEnter ) { }
	}

	public const string OnExit = "OnExit";
	[MethodArguments( new Type[] { typeof( ClientExitVehicleEvent ) } )]
	public class OnExitAttribute : EventAttribute
	{
		public OnExitAttribute() : base( OnExit ) { }
	}
}
