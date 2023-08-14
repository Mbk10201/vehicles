using Sandbox;
using System.Collections.Generic;

namespace Mbk.Vehicles;

public partial class Seat : EntityComponent
{
	[Net, Change]
	public IClient Client { get; set; }

	public void OnClientChanged( IClient oldValue, IClient newValue )
	{
		Log.Info($"Client variable changed: {oldValue} -> {newValue}");
	}

	[Net]
	public int Index { get; private set; }

	[Net]
	public bool SeatBelt { get; set; } = false;

	public bool IsFree => Client == null;

	protected override void OnActivate()
	{
		base.OnActivate();
	}

	public void AttachClient( IClient client )
	{
		if ( Entity == null )
			return;

		if ( client == null )
			return;

		Game.AssertServer();

		Log.Info( "AttachClient" );

		Client = client;
	}

	public void ExitClient( )
	{
		if ( Entity == null )
			return;

		if ( Client == null )
			return;
	}

	public void SetIndex( int index ) => Index = index;
}
