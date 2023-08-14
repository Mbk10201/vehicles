using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mbk.Vehicles.Resources;

[GameResource( "Brand", "brand", "Describe and configure a brand for a vehicle", Icon = "assignment_ind", IconBgColor = "white", IconFgColor = "dodgerblue" )]
public partial class BrandResource : GameResource
{
	[Description( "The name of the brand." )]
	public string Name { get; private set; }

	[Description( "The slogan of the brand." )]
	public string Slogan { get; private set; }

	[Description( "The logo image of the brand (SVG)." )]
	public string Image { get; private set; }

	[Description( "The country of the brand." )]
	public CountryResource Country { get; private set; }

	[Category( "Creation" ), Description( "The day of creation of the brand." )]
	public int Day { get; private set; }

	[Category( "Creation" ), Description( "The month of creation of the brand." )]
	public int Month { get; private set; }

	[Category( "Creation" ), Description( "The Year of creation of the brand." )]
	public int Year { get; private set; }

	protected override void PostReload()
	{
		if ( Game.IsServer || Game.IsClient )
		{
			Vehicles.ReloadDefinitions();
		}

		base.PostReload();
	}
}
