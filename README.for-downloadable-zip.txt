Manifold Ball: Technical Preview Mar 2017
=========================================

This is a pre-alpha technical preview of a VR experience allowing the
user to play a tennis-like game in a multiply-connected universe.

It was created using Unity 3D by a research team in the Mathematical
Computing Laboratory at the University of Illinois at Chicago.  (See
"Development Team" below.)

This is an early prototype in which many things are broken, imperfect,
or inefficient.  It only includes the features that were somewhat
functional as of Mar 1, 2017.


QUICK START
-----------

You need an Oculus Rift headset and Oculus Touch controllers connected
to a computer running a version of Microsoft Windows supported by the
Oculus runtime.

Download the ZIP archive containing the Manifold Ball technical
preview application.  Extract the ZIP file, and then run the
EXE file.

For 32-bit windows users, the executable will be called:
  manifoldball-pre-alpha-0317-32bit.exe

For 64-bit windows users, the executable will be called:
  manifoldball-pre-alpha-0317-64bit.exe

Put on the Oculus headset, dismiss the Health & Safety Warning if
necessary, and you will be presented with the main menu.


Instructions
------------

The application starts with a menu.  Select "3-torus" to start the
game.  In the game itself, you will find yourself standing in a
3-dimensional torus.  You have a racket and a ball.  You will see many
copies of yourself and of the other objects in all directions.  You
can play catch or tennis with yourself.

For example, if you throw the ball forward, it will eventually hit you
in the back of the head.

If you hit the ball directly to the left, it will eventually come back
to the place where you hit it (approaching from the right).

It may help to watch the video:
https://www.youtube.com/watch?v=K6k6mYd5164


Controls
--------

In the MENU:

* The laser pointer follows the right touch controller

* Right touch controller buttons:
  * Index finger trigger button = activate a menu option

In the GAME:

* The left touch controller is represented by a green cube
  * Use this hand to grab the ball
  
* The right touch controller is represented by a blue paddle
  * Use this hand to hit the ball
  
* The player's head is shown as a white cube

* Right touch controller buttons:
  * Thumb stick button = reset the ball position
  * "A" button = apply drag (hold to slow ball down)

* Left touch controller buttons:
  * Middle finger grip button = grab the ball
      (hold to keep in hand; release button to throw)
  * Recessed menu button = pause game (toggle)


Known issues
------------

* The mirror cube menu option does nothing. (Not implemented.)

* It is not possible to exit the game gracefully.  (Press Alt-F4 to
  close the application.)

* High-speed racket-ball collisions do not work. If the racket swings
too quickly, it can pass through the ball or send the ball flying in a
random direction at high velocity

* More copies of the objects should be visible to create the proper
3-torus perspective.  Currently, we only render an 11x11x11 grid of
copies of the unit cell.

* The framerate is poor. we get about 55fps on a high-end CPU and GPU.
The game is CPU-bound as many object updates happen in a single CPU
thread running C# scripts.


Development Team
----------------

This is a Spring 2017 semester undergraduate research project of the
Mathematical Computing Laboratory at the University of Illinois at
Chicago (http://mcl.math.uic.edu/).

Faculty Supervisor: David Dumas <david@dumas.io>
Graduate Mentor: Hai Tran
Undergraduate Researchers: Horalia Armas, Brandon Reichman
