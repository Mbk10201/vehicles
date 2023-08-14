$modelname	"roleplay/vehicles/audi_s8"
$cdmaterials "models\roleplay\vehicles\audi_s8\"
$cdmaterials "models\roleplay\vehicles\shared\"

$bodygroup "Body"
{
	studio "body.smd"
}
$bodygroup "Body2"
{
	studio "body2.smd"
}
$bodygroup "Body3"
{
	studio "body3.smd"
}
$bodygroup "Body4"
{
	studio "body4.smd"
}
$bodygroup "glass"
{
	studio "glass.smd"
	studio "privacy_glass.smd"
}
$bodygroup "steering_wheel"
{
	studio "steering_wheel.smd"
}
// Wings
$bodygroup "wing"
{
	blank
	studio "wing_00.smd"
	studio "wing_01.smd"
}
// Wheels
$bodygroup "rubber"
{
	studio "rubber.smd"
	blank
}

$bodygroup "wheels"
{
	studio "vossen_01.smd"
	studio "vossen_02.smd"
	studio "vossen_03.smd"
	studio "vorsteiner_01.smd"
	studio "vorsteiner_02.smd"
	studio "vorsteiner_03.smd"
	studio "vorsteiner_04.smd"
	studio "vorsteiner_05.smd"
	blank
}

$bodygroup "caliper"
{
	studio "caliper.smd"
	blank
}
$bodygroup "brake"
{
	studio "brake.smd"
	blank
}

$texturegroup "skinfamilies"
{
	{ "skin"   }
	{ "skin1"  }
	{ "skin2"  }
	{ "skin3"  }
	{ "skin4"  }
	{ "skin5"  }
	{ "skin6"  }
	{ "skin7"  }
	{ "skin8"  }
	{ "skin9"  }
	{ "skin10" }
	{ "skin11" }
	{ "skin12" }
}

$collisionmodel "phy.smd" 
{
	$concave
	$mass 1800.00
	$inertia 1.00
	$damping 0.00
	$rotdamping 0.00
}

$origin 0 0 0 0
$poseparameter vehicle_steer -1 1
$poseparameter vehicle_wheel_fl_height 0 1 //0.00 0.53
$poseparameter vehicle_wheel_fr_height 0 1
$poseparameter vehicle_wheel_rl_height 0 1
$poseparameter vehicle_wheel_rr_height 0 1
$poseparameter vehicle_wheel_fl_spin -180.00 180.00 wrap
$poseparameter vehicle_wheel_fr_spin -180.00 180.00 wrap
$poseparameter vehicle_wheel_rl_spin -180.00 180.00 wrap
$poseparameter vehicle_wheel_rr_spin -180.00 180.00 wrap
$attachment "vehicle_driver_eyes" "Camera001" 2.00 0.00 0.00 rotate -0 0 -90
$attachment "vehicle_feet_passenger0" "Camera001" -5.00 -34.00 0.00 rotate -0 5 -90
$attachment "vehicle_feet_passenger1" "Camera001" -5.00 -34.00 40.00 rotate -0 5 -90
$attachment "vehicle_feet_passenger2" "Camera001" -47.00 -34.00 40.00 rotate -0 5 -90
$attachment "vehicle_feet_passenger3" "Camera001" -47.00 -34.00 0.00 rotate -0 5 -90
$attachment "vehicle_engine" "Bone002" 180.50 30.00 -6.00 rotate -0 0 -90
$attachment "wheel_fl" "fl_wheel" 0.00 0.00 0.00 rotate 0 0 0
$attachment "wheel_fr" "fr_wheel" 0.00 0.00 0.00 rotate 0 0 0
$attachment "wheel_rr" "br_wheel" 0.00 0.00 0.00 rotate 0 0 0
$attachment "wheel_rl" "bl_wheel" 0.00 0.00 0.00 rotate 0 0 0
$attachment "exit1" "Bone002" 115.00 5.00 -55.00 rotate -0 0 0
$attachment "enter1" "Bone002" 115.00 5.00 -55.00 rotate -0 0 0
//$attachment "light_01" "light_01" 0 0 0 rotate -0 0 0
//$attachment "light_02" "light_02" 0 0 0 rotate -0 0 0
$attachment "exhaust" "Exhaust" 0 0 0 rotate -0 0 0
$attachment "exhaust2" "Exhaust2" 0 0 0 rotate -0 0 0
$attachment "rl_light" "rl_light" 0 0 0 rotate -0 0 0
$attachment "rr_light" "rr_light" 0 0 0 rotate -0 0 0
$attachment "fl_light" "fl_light" 0 0 0 rotate -0 0 0
$attachment "fr_light" "fr_light" 0 0 0 rotate -0 0 0
$hboxset "default"


$surfaceprop "metal"
$sequence "idle" "sequences" frame 10 10 fps 30
$sequence "turn" "sequences" frame 0 0 fps 30
$sequence "suspension" "sequences" frame 10 11 fps 30
$sequence "spin" "sequences" frame 0 0 fps 30
$animation neutral "sequences" frame 0 0 fps 30

$sequence "enter1" "sequences" frame 0 0 fps 20
$sequence "exit1" "sequences" frame 0 0 fps 20


//comment these out if your car has no speedometer from here (

//$weightlist speedo { 
//"speedo" 1.0 
//"tachometer" 1.0 
//}


//$animation 1 "sequences" frame 20 20 subtract neutral 0 weightlist speedo
//$animation 2 "sequences" frame 21 21 subtract neutral 0 weightlist speedo
//$animation 3 "sequences" frame 22 22 subtract neutral 0 weightlist speedo
//$animation 4 "sequences" frame 23 23 subtract neutral 0 weightlist speedo
//$animation 5 "sequences" frame 24 24 subtract neutral 0 weightlist speedo
//$animation 6 "sequences" frame 25 25 subtract neutral 0 weightlist speedo
//$animation 7 "sequences" frame 26 26 subtract neutral 0 weightlist speedo
//$animation 8 "sequences" frame 27 27 subtract neutral 0 weightlist speedo
//$animation 9 "sequences" frame 28 28 subtract neutral 0 weightlist speedo
//$animation 10 "sequences" frame 29 29 subtract neutral 0 weightlist speedo
//$animation 11 "sequences" frame 30 30 subtract neutral 0 weightlist speedo
//$animation 12 "sequences" frame 31 31 subtract neutral 0 weightlist speedo
//$animation 13 "sequences" frame 32 32 subtract neutral 0 weightlist speedo
//$animation 14 "sequences" frame 33 33 subtract neutral 0 weightlist speedo
//$animation 15 "sequences" frame 34 34 subtract neutral 0 weightlist speedo
//$animation 16 "sequences" frame 35 35 subtract neutral 0 weightlist speedo
//$animation 17 "sequences" frame 36 36 subtract neutral 0 weightlist speedo
//$animation 18 "sequences" frame 37 37 subtract neutral 0 weightlist speedo
//$animation 19 "sequences" frame 38 38 subtract neutral 0 weightlist speedo
//$animation 20 "sequences" frame 39  39 subtract neutral 0 weightlist speedo
//$animation 21 "sequences" frame 40 40 subtract neutral 0 weightlist speedo
//$animation 22 "sequences" frame 41 41 subtract neutral 0 weightlist speedo
//$animation 23 "sequences" frame 42 42 subtract neutral 0 weightlist speedo
//$animation 24 "sequences" frame 43 43 subtract neutral 0 weightlist speedo
//$animation 25 "sequences" frame 44 44 subtract neutral 0 weightlist speedo
//$animation 26 "sequences" frame 45 45 subtract neutral 0 weightlist speedo


//$sequence speedometer { 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 blendwidth 25 blend vehicle_guage 0 1 } weightlist speedo delta autoplay

//) to here

$weightlist front_axles { 
//comment out steering wheel if your car has no moving steering wheel
"steeringwheel" 1.0 
"fr_axle" 1.0 
"fl_axle" 1.0
}
$animation turn_left "sequences" frame 1 1 subtract neutral 0 weightlist front_axles
$animation turn_right "sequences" frame 2 2 subtract neutral 0 weightlist front_axles
$sequence turning { turn_left turn_right blend vehicle_steer -1 1 } weightlist front_axles delta autoplay

$weightlist wheel_fl { 
"fl_wheel" 1.0 
}
$weightlist axle_fl { 
"fl_axle" 1.0
}
$animation wheel_fl_low "sequences" frame 10 10 subtract suspension 0 weightlist axle_fl
$animation wheel_fl_high "sequences" frame 11 11 subtract suspension 0 weightlist axle_fl
$sequence wheel_fl_suspension { wheel_fl_low wheel_fl_high blend "vehicle_wheel_fl_height" 0 1.0 } weightlist wheel_fl delta autoplay

$animation wheel_fl_spin0 "sequences" frame 0 0 subtract neutral 0 weightlist wheel_fl
$animation wheel_fl_spin120 "sequences" frame 5 5 subtract neutral 0 weightlist wheel_fl
$animation wheel_fl_spin240 "sequences" frame 6 6 subtract neutral 0 weightlist wheel_fl

$sequence wheel_fl_spin "wheel_fl_spin0" fps 30.00 {
  blendwidth 4
  blend vehicle_wheel_fl_spin -180.000000 180.000000
  delta
  autoplay
 wheel_fl_spin120 wheel_fl_spin240 wheel_fl_spin0
}

$weightlist wheel_fr { 
"fr_wheel" 1.0 
}
$weightlist axle_fr { 
"fr_axle" 1.0 
}
$animation wheel_fr_low "sequences" frame 10 10 subtract suspension 0 weightlist axle_fr
$animation wheel_fr_high "sequences" frame 11 11 subtract suspension 0 weightlist axle_fr
$sequence wheel_fr_suspension { wheel_fr_low wheel_fr_high blend "vehicle_wheel_fr_height" 0 1.0 } weightlist wheel_fr delta autoplay

$animation wheel_fr_spin0 "sequences" frame 0 0 subtract neutral 0 weightlist wheel_fr
$animation wheel_fr_spin120 "sequences" frame 5 5 subtract neutral 0 weightlist wheel_fr
$animation wheel_fr_spin240 "sequences" frame 6 6 subtract neutral 0 weightlist wheel_fr

$sequence wheel_fr_spin "wheel_fr_spin0" fps 30.00 {
  blendwidth 4
  blend vehicle_wheel_fr_spin -180.000000 180.000000
  delta
  autoplay
 wheel_fr_spin120 wheel_fr_spin240 wheel_fr_spin0
}

$weightlist wheel_rl { 
"bl_wheel" 1.0 
}
$weightlist axle_rl { 
"bl_axle" 1.0 
}
$animation wheel_rl_low "sequences" frame 10 10 subtract suspension 0 weightlist axle_rl
$animation wheel_rl_high "sequences" frame 11 11 subtract suspension 0 weightlist axle_rl
$sequence wheel_rl_suspension { wheel_rl_low wheel_rl_high blend "vehicle_wheel_rl_height" 0 1.0 } weightlist wheel_rl delta autoplay

$animation wheel_rl_spin0 "sequences" frame 0 0 subtract neutral 0 weightlist wheel_rl
$animation wheel_rl_spin120 "sequences" frame 5 5 subtract neutral 0 weightlist wheel_rl
$animation wheel_rl_spin240 "sequences" frame 6 6 subtract neutral 0 weightlist wheel_rl

$sequence wheel_rl_spin "wheel_rl_spin0" fps 30.00 {
  blendwidth 4
  blend vehicle_wheel_rl_spin -180.000000 180.000000
  delta
  autoplay
 wheel_rl_spin120 wheel_rl_spin240 wheel_rl_spin0
}


$weightlist wheel_rr { 
"br_wheel" 1.0 
}
$weightlist axle_rr { 
"br_axle" 1.0 
}
$animation wheel_rr_low "sequences" frame 10 10 subtract suspension 0 weightlist axle_rr
$animation wheel_rr_high "sequences" frame 11 11 subtract suspension 0 weightlist axle_rr
$sequence wheel_rr_suspension { wheel_rr_low wheel_rr_high blend "vehicle_wheel_rr_height" 0 1.0 } weightlist wheel_rr delta autoplay

$animation wheel_rr_spin0 "sequences" frame 0 0 subtract neutral 0 weightlist wheel_rr
$animation wheel_rr_spin120 "sequences" frame 5 5 subtract neutral 0 weightlist wheel_rr
$animation wheel_rr_spin240 "sequences" frame 6 6 subtract neutral 0 weightlist wheel_rr

$sequence wheel_rr_spin "wheel_rr_spin0" fps 30.00 {
  blendwidth 4
  blend vehicle_wheel_rr_spin -180.000000 180.000000
  delta
  autoplay
 wheel_rr_spin120 wheel_rr_spin240 wheel_rr_spin0
}
