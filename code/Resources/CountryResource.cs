using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mbk.Vehicles.Resources;

[GameResource( "Country", "country", "Describe and configure a country for a vehicle", Icon = "flag", IconBgColor = "white", IconFgColor = "dodgerblue" )]
public partial class CountryResource : GameResource
{
	[Description( "The name of the country." )]
	public string Name { get; private set; }

	[Description( "The flag image of the country (SVG)" )]
	public string Image { get; private set; }

	protected override void PostReload()
	{
		if ( Game.IsServer || Game.IsClient )
		{
			Vehicles.ReloadDefinitions();
		}

		base.PostReload();
	}
}
