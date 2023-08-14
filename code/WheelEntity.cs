using Editor;
using Mbk.Vehicles.Resources;
using Sandbox;
using System.ComponentModel.DataAnnotations;

namespace Mbk.Vehicles;

/*
 * 
 * 
 * Source code taken from Source SDK 2013 made by Valve
 * The code has been rewrited and reworked completely 
 * 
 * 
*/

[Library( "rp_wheel" )]
[Display( Name = "Wheel" ), Category( "tire_repair" ), Icon( "public" )]
[HammerEntity]
public partial class WheelEntity : AnimatedEntity
{
	public WheelResource WheelResource { get; set; }

	public WheelEntity() {}

	public void SetResource( WheelResource wheelbase )
	{
		if ( wheelbase == null )
			return;

		WheelResource = wheelbase;

		var model = !string.IsNullOrEmpty( wheelbase.ModelPath ) ? wheelbase.ModelPath : "models/sbox_props/burger_box/burger_box.vmdl";

		if ( !string.IsNullOrEmpty( model ) )
		{
			SetModel( model );
			SetupPhysicsFromModel( PhysicsMotionType.Dynamic );
		}
	}

	public override void Spawn()
	{
		base.Spawn();
		
		Tags.Add( "wheel" );
		Tags.Add( "solid" );

		Transmit = TransmitType.Always;
		AnimateOnServer = true;
		UseAnimGraph = true;
	}
}
