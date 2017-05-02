# Manifold Ball

This is a pre-alpha technical preview of a VR experience allowing the
user to play a tennis-like game in a multiply-connected universe.

It was created using the Unity game engine by a research team in the
Mathematical Computing Laboratory at the University of Illinois at
Chicago.  (See "Development Team" below.)

This prototype has a number of known issues (see README.md for
details).

## QUICK START

You need an Oculus Rift headset and Oculus Touch controllers connected
to a computer running a version of Microsoft Windows supported by the
Oculus runtime.

If starting from source, open the main repository folder in Unity and
either select "build" with options suitable for your platform or run
the scene "MenuScene" in the editor.

If starting from a binary distribution, download the ZIP archive,
extract the ZIP file, and then run the EXE file.

For 32-bit windows users, the executable will have a name like:
```manifoldball-VERSION-32bit.exe```

For 64-bit windows users, the executable will have a name like:
```manifoldball-VERSION-64bit.exe```

In both cases VERSION will be replaced by a version number (like
"v0.2").

Put on the Oculus headset, dismiss the Health & Safety Warning if
necessary, and you will be presented with the main menu.

## Instructions

The application starts with a menu.  The top three menu options are
different spaces in which to play the game.  Select one of them by
pointing to it with the right touch controller and pressing the index
trigger.

You will find yourself standing on a small platform, with a red ball
hovering in front of you.  In the game, you are represented by a
simple avatar consisting of a white cube for your head and two cuboids
for your hands.  The green cuboid (initially your right hand) is a
racket that can be used to hit the ball.  The purple cuboid (initially
your left hand) is a glove, which can be used to pick up and move the
ball by holding the trigger button.

Due to the multiply-connected nature of the space in which the game is
set, you will see many copies of yourself and of the ball and platform
in all directions.  This is similar to the effect when you stand
between two parallel mirrors, except that in this case the repetition
happens in three independent directions and is sometimes mixed with
rotations (depending on which orbifold was selected in the main menu).

The game consists of using the glove and racket to hit the ball back
and forth to yourself.  Most of the time, if you select a direction at
random and hit the ball, it will eventually come back close enough to
be caught (though you may need to turn around to see it coming).

It may help to watch the video (demonstrating an earlier version of
the game):

https://www.youtube.com/watch?v=K6k6mYd5164


## Controls

In the MENU or ABOUT page:

* The laser pointer follows the right touch controller

* Right touch index trigger = activate a menu option

* Press both thumb sticks in = exit the game

* Escape key = exit the game

In the GAME:

* Right touch controller:
  * Button A = hold to slow the ball down
  * Button B = hold to speed the ball up
  * Thumbstick press = reset ball to starting position

* Left touch controller:
  * Button Y = switch glove and racket hands
  * Start button (recessed) = return to MENU
  * Thumbstick press = reset ball to starting position

* Touch controller for the glove (initially, the left one):
  * Either trigger button = grab the ball (hold to move it)

* Escape key = return to MENU


## Development Team

Horalia Armas
David Dumas <david@dumas.io>  (current maintainer)
Brandon Reichman <breich5@uic.edu>
Hai Tran <htran41@uic.edu>
