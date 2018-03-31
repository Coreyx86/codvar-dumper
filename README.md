# codvar dumper

This program was made to dump all of the developer variables (DVARs) from various Call of Duty games (steam versions only) in real time using my MemoryLib API which is also available on my Github.

Since Call of Duty games are pretty much the same from platform to platform this code could be adopted to be used on PS3/XBOX360/PS4 or any other system that has the capabilities of reading/writing to userland processes.

In order to adopt this program for other systems you need to:

1.)Reverse engineer the dvar_s structure of the game you want to use. (Although its rare the structures will change)

2.)Replace the use of my MemoryLib API with whatever API you would need to use on your respected platform. (I.E: PS3API for PS3, PS4API for PS4, etc.)


TO DO:
Add multithreading.
Add the abililty to edit the DVAR Values in Realtime.
Add the ability to save all dvars to a .txt format.

Created by Corey Nelson