function chamfer_rect(x=1,y=1,a=0.1)
    = [ [-x+a,-y], [-x,-y+a], [-x,y-a], [-x+a,y], [x-a,y], [x,y-a], [x,-y+a], [x-a,-y] ];

scale([1/90,1/90,1/90]) { 
    union () {
        translate(v=[0,0,-4]) { linear_extrude(height=8) { polygon(chamfer_rect(45,35,20)); } };
        translate(v=[45,0,0]) { rotate(a=[0,90,0]) { linear_extrude(height=45) { polygon(chamfer_rect(3.7,5,2)); } } }
    }
}