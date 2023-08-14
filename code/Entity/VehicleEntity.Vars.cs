using Mbk.Vehicles.Enums;
using Sandbox;
using System.Collections.Generic;

namespace Mbk.Vehicles;

public partial class VehicleEntity
{
	ParticleSystemEntity SmokeEntity { get; set; }

	[Net]
	public IList<Seat> Seats { get; private set; }

	[Net]
	public IList<IClient> WhoHasKeys { get; private set; }

	public bool[] DoorsLocked;
	
	public bool Locked
	{
		get
		{
			int lockeds = 0;

			for(int i = 0; i < DoorsLocked.Length; i++)
			{
				if ( DoorsLocked[i] )
					lockeds++;
			}

			return lockeds == DoorsLocked.Length;
		}
		set
		{
			for ( int i = 0; i < DoorsLocked.Length; i++ )
			{
				DoorsLocked[i] = true;
			}
		}
	}
	
	
	public bool ValidDriver => Seats[0].Client != null;
}
