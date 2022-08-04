# DiskGenius License Bypass

Written in .NET Framework 4.2, this license bypass allows fake licenses to be used with DiskGenius to trick the software into thinking its legit.

It's believed that DG (DiskGenius) checks for a valid license and does its fancy license check stuff when the program first starts. But in addition to this, that is the only point in time where DG runs that check.

The way this works is it stores the license file and related files for licensing, into either Memory, or in a temporary folder, waiting a few seconds after DG starts, then restores the files.

Working with DiskGenius 5.4.3.1342 x64 Professional

## PLEASE NOTE

You must have a "legit" DiskGenius license that doesn't work, in order for this to work, that includes the msimg32 dll that comes with said "legit" license.
It will not be provided for you. This bypass was made after DiskGenius implemented a "legit" license check.

## Using

1. Install [DiskGenius](https://www.diskgenius.com/)

2. Go to your DiskGenius installation folder (Usually `C:\Program Files\DiskGenius`)

3. Rename DiskGenius.exe to DiskGenius_.exe

4. Copy the [latest release](https://github.com/flleeppyy/DiskGeniusBypass/releases/latest) into the folder, choosing your preferred storage method, and rename it to DiskGenius.exe

## License

Licensed under the [MIT](LICENSE) license
