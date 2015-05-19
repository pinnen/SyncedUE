Notes on Build Environment.
===========================

16:23 2015-04-26/GF


In NetCenter PC:s the used files are stored here: 
C:\Users\netcenter\Documents\GitHub\InstallationsFiles


1) 
Install Visual Studio 2013
--------------------------

Files in: 
C:\Users\netcenter\Documents\GitHub\InstallationsFiles\Visual Studio Ultimate 2013 32-bit (English)\vs2013_rtm_ult_enu\vs2013_rtm_ult_enu.iso

NB Requires: DAEMONS TOOLS Ligth, 
C:\Users\netcenter\Documents\GitHub\InstallationsFiles\DTLiteInstaller.exe
From:
http://www.daemon-tools.cc/downloads


2) 
Install MonoGame support
------------------------

File:
C:\Users\netcenter\Documents\GitHub\InstallationsFiles\MonoGameSetup.exe
From:
http://www.monogame.net/downloads/


3)
Install XBox 360 support (om det inte kommer in automatiskt...)
------------------------

File:
C:\Users\netcenter\Documents\GitHub\NetCenter\InstallationsFiles\Xbox360_64Eng.exe
From:
https://www.microsoft.com/hardware/sv-se/d/xbox-360-controller-for-windows






Notes on GITHUB
===============

How to get old versions

Alt.1
-----

In GitHub: 
a) Select version in history make a note of the hex value at the top right: e.g. "c320ad3"
b) Right-click on projevt on left side and select "Open in Git Shell"!

In Git Shell:
git reset --hard <akt-nr>, ex: git reset --hard c320ad3

Restore to current version:
in Git Hub: Sync!


Alt.2
-----

See:
http://githowto.com/getting_old_versions

FIRST: Requires addition in C:\Users\netcenter\.gitconfig (or other pas as sepcified in "Git Shell" shortcut!
[alias]
  hist = log --pretty=format:\"%h %ad | %s%d [%an]\" --graph --date=short


Commands in Git Shell:
1) git hist
	Will show a long list of history

2) Select hexvalue in relevant point in history:
| | | * 610337a 2014-09-30 | Crystal Update [Dennis]
| | | *   cf3d3ef 2014-09-30 | Merge with main [Göran]
| | | |\
| | | * | 2249eca 2014-09-30 | Added release [Göran]
	e.g. "cf3d3ef"

3) git checkout cf3d3ef

4) Now you can experiment with the old version...

5) Restore to original state with command:
git checkout master

