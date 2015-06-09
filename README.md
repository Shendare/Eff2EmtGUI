# Eff2EmtGUI
EverQuest Eff2Emt Zone Sounds Converter (GUI)

Note: Requires the .Net Framework 3.5 to be installed. It comes pre-installed on Windows 7 and later.

  1. Tell the program where to find your EverQuest directory through one of the following methods:
  
      * Drag-and-drop a .eff file from your EQ directory onto the window, or
      * Click the [ ... ] button to browse to your EQ directory, or
      * Copy and paste the EQ directory location into the textbox

  2. The zone list will automatically update with a listing of all zones for which it found a x_sounds.eff file for converting.
      (If you dropped .eff files onto the window, it will automatically select those zones for conversion.)

  3. Check the box for any and all zones you wish to convert .eff files into .emt files for.

  4. Click the Convert! button to begin the conversion.

Notes:

  * To select or deselect multiple zones at once:
  
      1. Click on the first zone you wish to convert
      2. Scroll over and hold shift while selecting the last zone you wish to convert. This highlights the whole block of zones.
      3. Now click the checkbox next to one of the highlighted zones, and all should be selected.
      4. You can go back and individually deselect any that became part of the block that you don't actually want converted.

  * If a .emt file already exists for a selected zone, new entries will be merged into the file, skipping any duplicates

  * The format line naming each field will be added to the top of .emt files, for human consumption.

  * If you convert 1 to 3 zones at a time, the .emt files for them will be opened up in Notepad++ or Notepad after conversion.

  * If you batch convert more than 3 zones at once, they will not be opened for you, under the assumption you know what you're doing.

  * A command-line c++ version is also in the works, for anyone who prefers those.
