# DiskGenius License Bypass

Written in .NET Framework 4.2, this license bypass allows fake licenses to be used with DiskGenius to trick the software into thinking its legit.

It's believed that DG (DiskGenius) checks for a valid license and does its fancy license check stuff when the program first starts. But in addition to this, that is the only point in time where DG runs that check.

The way this works is it stores the license file and related files for licensing, into either Memory, or in a temporary folder, waiting a few seconds after DG starts, then restores the files.

Working with DiskGenius 5.4.3.1342 x64 Professional