/*
found this on:
http://wiki.unity3d.com/index.php?title=DepthMask
It helps to make the Minimap circular, noting more
*/
Shader "Masked/Mask" {
 
	SubShader {
		// Render the mask after regular geometry, but before masked geometry and
		// transparent things.
 
		Tags {"Queue" = "Geometry+10" }
 
		// Don't draw in the RGBA channels; just the depth buffer
 
		ColorMask 0
		ZWrite On
 
		// Do nothing specific in the pass:
 
		Pass {}
	}
}
