using Mbk.Vehicles.Resources;
using Mbk.Vehicles.UI;
using Sandbox;
using Sandbox.UI;
using System.Collections.Generic;
using System.Linq;

namespace Mbk.Vehicles;

public static partial class Vehicles
{
	private static BaseFileSystem path => FileSystem.Data;

	public static IList<CountryResource> CountryList { get; private set; } = new List<CountryResource>();
	public static IList<BrandResource> BrandList { get; private set; } = new List<BrandResource>();
	public static IList<VehicleResource> VehicleList { get; private set; } = new List<VehicleResource>();
	public static IList<VehicleHandlingResource> VehicleHandlingList { get; private set; } = new List<VehicleHandlingResource>();
	public static IList<WheelResource> WheelsList { get; private set; } = new List<WheelResource>();

	public static TypeDescription HudPanelType { get; private set; }

	[GameEvent.Entity.PostSpawn]
	public static void Initialize()
	{
		Game.AssertServer();
		ReloadDefinitions();
		LoadConfiguration();

		foreach ( var type in TypeLibrary.GetTypes().Where( x => x.IsClass ) )
		{
			int count = type.Interfaces.Count( x => x.Name == "IVehicleHud" );
			if ( count > 0 )
			{
				HudPanelType = type;
				return;
			}
		}
	}

	public static void ReloadDefinitions()
	{
		CountryList.Clear();
		BrandList.Clear();
		VehicleList.Clear();
		VehicleHandlingList.Clear();

		var countries = ResourceLibrary.GetAll<CountryResource>();

		foreach ( var country in countries )
		{
			if ( !CountryList.Contains( country ) )
				CountryList.Add( country );

			Log.Info( $"[Vehicles] New country({country.ResourcePath}) loaded: {country.Name}" );
		}

		var brands = ResourceLibrary.GetAll<BrandResource>();

		foreach ( var brand in brands )
		{
			if ( !BrandList.Contains( brand ) )
				BrandList.Add( brand );

			Log.Info( $"[Vehicles] New brand({brand.ResourcePath}) loaded: {brand.Name}" );
		}

		var vehicles = ResourceLibrary.GetAll<VehicleResource>();

		foreach ( var vehicle in vehicles )
		{
			if(!VehicleList.Contains( vehicle ) )
				VehicleList.Add( vehicle );

			Log.Info( $"[Vehicles] New vehicle({vehicle})({vehicle.ResourcePath}) loaded: {vehicle.Model}" );
		}

		var handlings = ResourceLibrary.GetAll<VehicleHandlingResource>();

		foreach ( var handling in handlings )
		{
			if ( !VehicleHandlingList.Contains( handling ) )
				VehicleHandlingList.Add( handling );

			Log.Info( $"[Vehicles] New handling({handling.ResourcePath}) loaded: {handling.Category}" );
		}

		var wheels = ResourceLibrary.GetAll<WheelResource>();

		foreach ( var wheel in wheels )
		{
			if ( !WheelsList.Contains( wheel ) )
				WheelsList.Add( wheel );

			Log.Info( $"[Vehicles] New wheel({wheel.ResourcePath}) loaded: {wheel.Model} - {wheel.ModelPath}" );
		}
	}

	[ConCmd.Server]
	public static void SpawnVehicle(string identifier)
	{
		var pawn = ConsoleSystem.Caller.Pawn as Entity;

		foreach (var x in VehicleList)
		{
			Log.Info( x.Identifier );
		}

		var vehicle = VehicleList.SingleOrDefault( x => x.Identifier == identifier );

		if ( vehicle == null )
			return;

		var entity = new VehicleEntity( )
		{
			Position = pawn.Position + Vector3.Backward * 100,
			Owner = pawn,
		};

		entity.GiveKeys( ConsoleSystem.Caller );
		entity.SetResource( vehicle );
	}

	public static bool IsClientInVehicle(IClient client)
	{
		var list = Entity.All.OfType<VehicleEntity>();
		return list.ToList().Exists( x => x.Seats.ToList().Exists( j => j.Client == client ) );
	}

	public static bool IsClientDriver( IClient client )
	{
		if ( !IsClientInVehicle(client) )
			return false;

		var list = Entity.All.OfType<VehicleEntity>();
		return list.ToList().Exists( x => x.Seats[0].Client == client );
	}

	[ClientRpc]
	public static void ShowHud()
	{
		/*foreach ( var type in TypeLibrary.GetTypes().Where( x => x.IsClass ) )
		{
			int count = type.Interfaces.Count( x => x.Name == "IVehicleHud" );
			if ( count > 0 )
			{
				var hud = TypeLibrary.Create( type.TargetType.Name, type.TargetType );
				Game.RootPanel?.AddChild( (Panel)hud );
				return;
			}
		}*/

		Game.RootPanel?.AddChild<VehicleHud>();
	}

	[ClientRpc]
	public static void HideHud()
	{
		Game.RootPanel.Children.OfType<VehicleHud>().SingleOrDefault().Delete();
	}

	[GameEvent.Tick]
	public static void Tick()
	{
		//Log.Info( $"[{(Game.IsServer ? "SERVER" : "CLIENT")}] {Game.LocalClient.InVehicle()}" );
	}
}
