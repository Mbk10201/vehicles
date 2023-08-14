using Mbk.Vehicles.Enums;
using Sandbox;
using System.Collections.Generic;
using System.Linq;

namespace Mbk.Vehicles.Resources;

[GameResource( "Vehicle", "vehicle", "Describe and configure a vehicle for the vehicle system.", Icon = "directions_car", IconBgColor = "white" )]
public partial class VehicleResource : GameResource
{
	public static VehicleResource GetCarByID( string identifier ) => Vehicles.VehicleList!.Single( x => x.Identifier == identifier );

	[Category( "UI" ), Description( "The model of the vehicle." )]
	public string Model { get; private set; }

	[Category( "UI" ), Description( "The branding information of the vehicle." )]
	public BrandResource Brand { get; private set; }

	[Category( "UI" ), Description( "The date of production of the vehicle." )]
	public string Date { get; private set; }

	[HideInEditor]
	[Category( "UI" ), Description( "The vehicle unique identifier." )]
	public string Identifier => ResourceName;

	[Category( "Spawn Settings" ), Description( "The model path of the vehicle." )]
	[ResourceType( "vmdl" )]
	public string ModelPath { get; private set; }

	[Category( "Spawn Settings" ), Description( "The list of wheels availables on this vehicle." )]
	public List<WheelOption> Wheels { get; private set; }

	[Category( "Spawn Settings" ), Description( "The vehicle performances / characteristic." )]
	public VehicleHandlingResource Handling { get; private set; }

	[Category( "ScreenUI" ), Description( "If this vehicle should have a panel to display the UI in game." )]
	public bool HasScreen { get; private set; } = false;

	[Category( "ScreenUI" ), Description( "[Required] The attachment name to attach the screen panel (usually 'screen')." )]
	public string AttachmentAt { get; private set; } = "screen";

	[Category( "ScreenUI" ), Description( "the screen panel boxing (rectangle) size on the vehicle screen model." )]
	public Rect PanelBounds { get; private set; } = new Rect( 0, 0, 0, 0);

	public WheelResource GetDefaultWheel()
	{
		return Wheels.SingleOrDefault(x => x.Default).Wheel;
	}

	protected override void PostReload()
	{
		if ( Game.IsServer || Game.IsClient )
		{
			Vehicles.ReloadDefinitions();
		}

		base.PostReload();
	}
}
