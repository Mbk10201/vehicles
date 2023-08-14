using Sandbox;
using System.Collections.Generic;

namespace Mbk.Vehicles.Resources;

[GameResource( "Vehicle Wheel", "wheel", "Describe and configure a wheel for a vehicle.", Icon = "radio_button_checked", IconBgColor = "white" )]
public class WheelResource : GameResource
{
	[Category( "UI" ), Description( "The model of the wheel." )]
	public string Model { get; private set; }

	[Category( "UI" ), Description( "The branding information of the wheel." )]
	public BrandResource Brand { get; set; }

	[HideInEditor]
	[Category( "UI" ), Description( "The unique identifier of the wheel." )]
	public string Identifier => ResourceName;

	[Category( "Spawn Settings" ), Description( "The modelpath of the wheel." )]
	[ResourceType( "vmdl" )]
	public string ModelPath { get; private set; }

	protected override void PostReload()
	{
		if ( Game.IsServer || Game.IsClient )
		{
			Vehicles.ReloadDefinitions();
		}

		base.PostReload();
	}
}

public class WheelOption
{
	/// <summary>
	/// This define the default scale of this wheel for the car assigned.
	/// </summary>
	[Category( "Wheel Setup" )]
	public Vector3 Scale { get; set; } = new Vector3( 1f, 1f, 1f );

	[Category( "Wheel Setup" )]
	public WheelResource Wheel { get; set; }

	/// <summary>
	/// This define the actual wheel as the default one when the car spawns.
	/// Please enable this only on one wheel.
	/// </summary>
	[Category( "Wheel Setup" )]
	public bool Default { get; set; } = false;
}
