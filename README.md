# Eff2EmtGUI
EverQuest Eff2Emt Zone Sounds Converter (GUI Version)

Current Version: 1.1.1
Last Updated: 07/28/2015

Command Line Version: https://github.com/Shendare/Eff2Emt

#Instructions:

Note: Requires the .Net Framework 3.5 to be installed. It comes pre-installed on Windows 7 and later.

* Tell the program where to find your EverQuest directory through one of the following methods:
  
  * Drag-and-drop a .eff file from your EQ directory onto the window, or
  * Click the [ ... ] button to browse to your EQ directory, or
  * Copy and paste the EQ directory location into the textbox

* The zone list will automatically update with a listing of all zones for which it found a x_sounds.eff file for converting.
  (If you dropped .eff files onto the window, it will automatically select those zones for conversion.)

* Check the box for any and all zones you wish to convert .eff files into .emt files for.

* Click the Convert! button to begin the conversion.

#Notes:

* To select or deselect multiple zones at once:
  
      1. Click on the first zone you wish to convert
      2. Scroll over and hold shift while selecting the last zone you wish to convert. This highlights the whole block of zones.
      3. Now click the checkbox next to one of the highlighted zones, and all should be selected.
      4. You can go back and individually deselect any that became part of the block that you don't actually want converted.

* If a .emt file already exists for a selected zone, new entries will be merged into the file, skipping any duplicates

* The format line naming each field will be added to the top of .emt files, for human consumption.

* If you convert 1 to 3 zones at a time, the .emt files for them will be opened up in Notepad++ or Notepad after conversion.

* If you batch convert more than 3 zones at once, they will not be opened for you, under the assumption you know what you're doing.

#Disclaimer:

>Eff2EmtGUI is not affiliated with, endorsed by, approved by, or in any way associated with Daybreak Games or the EverQuest franchise, who reserve all copyrights and trademarks to their properties.

#License:

>Portions of this software's code not covered by another author's or entity's copyright are released under the Creative Commons Zero (CC0) public domain license.

>To the extent possible under law, Shendare (Jon D. Jackson) has waived all copyright and related or neighboring rights to this Eff2EmtGUI application. This work is published from: The United States.

>You may copy, modify, and distribute the work, even for commercial purposes, without asking permission.

>For more information, read the CC0 summary and full legal text here:

>https://creativecommons.org/publicdomain/zero/1.0/

#Release Notes:

7/28/2015 - Version 1.1.1

* Addition of CC0 License Information
* Release on Github

6/10/2015 - Version 1.1

* Lots of code cleanup

6/9/2015 - Version 1.0

* Initial Release
