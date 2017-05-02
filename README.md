# manifoldball

A VR game for the Oculus Rift + Touch in which the user can play ball in multiply-connected manifolds and orbifolds.

Initially developed in a Spring 2017 undergraduate research project of the
[Mathematical Computing Laboratory](http://mcl.math.uic.edu/)
at [UIC](http://uic.edu/).

## Status

A basic game of racquetball is implemented.  One hand is a racket that
can hit the ball, the other is a glove which can grab and reposition
the ball.

The game can be played in three orbifolds:

* The three-dimensional torus
* The two-fold cover of S^3 branched over the Borromean rings
* The mirrored cube orbifold

## Documentation

Game play documentation and control bindings are described in [docs/INSTRUCTIONS.md](docs/INSTRUCTIONS.md).

## Known issues

Major:

* In the non-torus spaces, the ball can only be hit once.  The copy of the ball supporting interaction does not wrap to remain in the fundamental domain, so all of the copies gradually drift away.

* Shading of objects in the mirror cube case are wrong, giving the objects an "inside out" appearance.

* To ensure a reasonable frame rate, only about 1000 fundamental domains are drawn.

Minor:

* More scenery is needed to give the player a sense of relative position and direction (e.g. to tell which copies are upside-down).

* Collision physics are not accurate (especially noticeable with moving ball and stationary racket)

* Distant copies of objects are shown with the same brightness as near ones; adding "fog" so that they gradually fade out would improve the illusion of an infinite lattice.

## Requirements

* [Unity 3D](http://unity3d.com/version) version 5.5
* Oculus Rift HMD
* Oculus Touch hand controllers

## Development Team

* Horalia Armas
* David Dumas (<david@dumas.io>)
* Brandon Reichman
* Hai Tran (<htran41@uic.edu>)
