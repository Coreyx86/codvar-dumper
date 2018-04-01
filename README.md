# codvar dumper

This program was made to dump all of the developer variables (DVARs) from various Call of Duty games (steam versions only) in real time using my MemoryLib API which is also available on my Github.

Since Call of Duty games are pretty much the same from platform to platform this code could be adopted to be used on PS3/XBOX360/PS4 or any other system that has the capabilities of reading/writing to userland processes.

In order to adopt this program for other systems you need to:

1.)Reverse engineer the dvar_s structure of the game you want to use. (Although its rare the structures will change)

2.)Replace the use of my MemoryLib API with whatever API you would need to use on your respected platform. (I.E: PS3API for PS3, PS4API for PS4, etc.)

P.S: I'm not sure about the newer call of duty games, but an easy way to find the memory addresses for the DVAR count and the DVAR arrays is to search for this string "CAN'T CREATE DVAR '%S': %I DVARS ALREADY" this will XREF to a function that has both addresses referenced there. 


TO DO:
Add multithreading.
<strike>Add the abililty to edit the DVAR Values in Realtime.</strike>
Add the ability to save all dvars to a .txt format.

Created by Corey Nelson