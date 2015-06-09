using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace Eff2EmtGUI
{
    public partial class Form1 : Form
    {
        public const string EMTLineFormat = ";?,SoundFile (wav=sound mp3/xmi=music),Unknown (0=OK 1=OK),WhenActive (0=Always 1=Daytime 2=Nighttime),Volume (1.0 = 100%),FadeInMS,FadeOutMS,WavLoopType (0=Constant 1=Delayed Repeat),X,Y,Z,WavFullVolRadius,WavMaxAudibleDist,NonZero = RandomizeLocation,ActivationRange,MinRepeatDelay,MaxRepeatDelay,xmiIndex,EchoLevel (50 = Max),IsEnvSound (for option toggle)";

        // Can be customized with any available text editors. Will be checked from top to bottom, and the first one found will be execute for an .emt file.
        public static List<string> TextEditors = new List<string>()
        {
            "C:\\Program Files (x86)\\Notepad++\\notepad++.exe",
            "C:\\Program Files\\Notepad++\\notepad++.exe",
            "C:\\Windows\notepad.exe"
        };
        
        public static Dictionary<int, string> HardCodedSoundFiles = new Dictionary<int,string>()
        {
            {  39, "death_me" },
            { 143, "thunder1" },
            { 144, "thunder2" }, // TODO: Find out what 144 actually is! Used in Qeynos at the same spot as #143, but at night.
            { 158, "wind_lp1" },
            { 159, "rainloop" },
            { 160, "torch_lp" },
            { 161, "watundlp" }
        };

        public static Dictionary<int, string> DefaultMusicFiles = new Dictionary<int, string>()
        {
            {  1, "bothunder.mp3" },
            {  2, "codecay.mp3" },
            {  3, "combattheme1.mp3" },
            {  4, "combattheme2.mp3" },
            {  5, "deaththeme.mp3" },
            {  6, "eqtheme.mp3" },
            {  7, "hohonor.mp3" },
            {  8, "poair.mp3" },
            {  9, "podisease.mp3" },
            { 10, "poearth.mp3" },
            { 11, "pofire.mp3" },
            { 12, "poinnovation.mp3" },
            { 13, "pojustice.mp3" },
            { 14, "poknowledge.mp3" },
            { 15, "ponightmare.mp3" },
            { 16, "postorms.mp3" },
            { 17, "potactics.mp3" },
            { 18, "potime.mp3" },
            { 19, "potorment.mp3" },
            { 20, "potranquility.mp3" },
            { 21, "povalor.mp3" },
            { 22, "powar.mp3" },
            { 23, "powater.mp3" },
            { 24, "solrotower.mp3" }
        };

        public struct EffSoundEntry
        {
            public Int32 UnkRef00;
            public Int32 UnkRef04;
            public Int32 Reserved;
            public Int32 Sequence;
            public float X;
            public float Y;
            public float Z;
            public float Radius;
            public Int32 Cooldown1;
            public Int32 Cooldown2;
            public Int32 RandomDelay;
            public Int32 Unk44;
            public Int32 SoundID1;
            public Int32 SoundID2;
            public Byte SoundType;
            public Byte UnkPad57;
            public Byte UnkPad58;
            public Byte UnkPad59;
            public Int32 AsDistance;
            public Int32 UnkRange64;
            public Int32 FadeOutMS;
            public Int32 UnkRange72;
            public Int32 FullVolRange;
            public Int32 UnkRange80;
        };

        public class EmtSoundEntry
        {
            public string EntryType = "2";
            public string SoundFile = "";
            public Int32 Reserved1 = 0;
            public Int32 WhenActive = 0;
            public float Volume = 1.0f;
            public Int32 FadeInMS = 500;
            public Int32 FadeOutMS = 1000;
            public Int32 WavLoopType = 0;
            public float X, Y, Z;
            public float WavFullVolRadius = 50;
            public float WavMaxAudibleDist = 50;
            public bool RandomizeLocation = false;
            public Int32 ActivationRange = 50;
            public Int32 MinRepeatDelay = 0;
            public Int32 MaxRepeatDelay = 0;
            public Int32 xmiIndex = 0;
            public Int32 EchoLevel = 0;
            public bool IsEnvSound = true;

            // ?,SoundFile (wav=sound mp3/xmi=music),Unknown (0=OK 1=OK),WhenActive (0=Always 1=Daytime 2=Nighttime),Volume (1.0 = 100%),FadeInMS,FadeOutMS,WavLoopType (0=Constant 1=Delayed Repeat),X,Y,Z,WavFullVolRadius,WavMaxAudibleDist,NonZero = RandomizeLocation,ActivationRange,MinRepeatDelay,MaxRepeatDelay,xmiIndex,EchoLevel (50 = Max),IsEnvSound (for option toggle)
            public bool _IsValid
            {
                get
                {
                    return ((SoundFile != null) && (SoundFile.Length > 0));
                }
            }
            
            public EmtSoundEntry Clone()
            {
                EmtSoundEntry _new = new EmtSoundEntry();

                _new.EntryType = this.EntryType;
                _new.SoundFile = this.SoundFile;
                _new.Reserved1 = this.Reserved1;
                _new.WhenActive = this.WhenActive;
                _new.Volume = this.Volume;
                _new.FadeInMS = this.FadeInMS;
                _new.FadeOutMS = this.FadeOutMS;
                _new.WavLoopType = this.WavLoopType;
                _new.X = this.X;
                _new.Y = this.Y;
                _new.Z = this.Z;
                _new.WavFullVolRadius = this.WavFullVolRadius;
                _new.WavMaxAudibleDist = this.WavMaxAudibleDist;
                _new.RandomizeLocation = this.RandomizeLocation;
                _new.ActivationRange = this.ActivationRange;
                _new.MinRepeatDelay = this.MinRepeatDelay;
                _new.MaxRepeatDelay = this.MaxRepeatDelay;
                _new.xmiIndex = this.xmiIndex;
                _new.EchoLevel = this.EchoLevel;
                _new.IsEnvSound = this.IsEnvSound;

                return _new;
            }
            
            public override string ToString()
            {
                if (!_IsValid)
                {
                    return "";
                }
                
                StringBuilder _line = new StringBuilder();

                _line.Append(EntryType);
                _line.Append(',');
                _line.Append(SoundFile);
                _line.Append(',');
                _line.Append(Reserved1);
                _line.Append(',');
                _line.Append(WhenActive);
                _line.Append(',');
                _line.Append(Volume.ToString("F1"));
                _line.Append(',');
                _line.Append(FadeInMS);
                _line.Append(',');
                _line.Append(FadeOutMS);
                _line.Append(',');
                _line.Append(WavLoopType);
                _line.Append(',');
                _line.Append(X.ToString("F1"));
                _line.Append(',');
                _line.Append(Y.ToString("F1"));
                _line.Append(',');
                _line.Append(Z.ToString("F1"));
                _line.Append(',');
                _line.Append(WavFullVolRadius.ToString("F1"));
                _line.Append(',');
                _line.Append(WavMaxAudibleDist.ToString("F1"));
                _line.Append(',');
                _line.Append(RandomizeLocation ? "1" : "0");
                _line.Append(',');
                _line.Append(ActivationRange);
                _line.Append(',');
                _line.Append(MinRepeatDelay);
                _line.Append(',');
                _line.Append(MaxRepeatDelay);
                _line.Append(',');
                _line.Append(xmiIndex);
                _line.Append(',');
                _line.Append(EchoLevel);
                _line.Append(',');
                _line.Append(IsEnvSound ? "1" : "0");

                return _line.ToString();
            }
        }

        public static Dictionary<int, string> mp3indexFiles = null;

        List<string> SoundBank_Emit;
        List<string> SoundBank_Loop;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textEQFolder.Text = Properties.Settings.Default.EQFolder;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.EQFolder = textEQFolder.Text;

            Properties.Settings.Default.Save();
        }

        private void CheckIfReady()
        {
            labelZonesSelected.Text = listZoneEffs.CheckedItems.Count.ToString();
            labelZoneCount.Text = listZoneEffs.Items.Count.ToString();
            
            buttonConvert.Enabled = (listZoneEffs.CheckedItems.Count > 0);
        }

        private void CheckZoneList()
        {
            listZoneEffs.Items.Clear();
            
            if (Directory.Exists(textEQFolder.Text))
            {
                FileInfo[] _files = new DirectoryInfo(textEQFolder.Text).GetFiles("*_sounds.eff");

                foreach (FileInfo _file in _files)
                {
                    listZoneEffs.Items.Add(_file.Name.Substring(0, _file.Name.Length - "_sounds.eff".Length));
                }
            }

            CheckIfReady();
        }

        private void textEQFolder_TextChanged(object sender, EventArgs e)
        {
            CheckZoneList();
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }

            Array _files = (Array)e.Data.GetData(DataFormats.FileDrop);

            if ((_files == null) || (_files.Length < 1))
            {
                return;
            }

            textEQFolder.Text = Path.GetDirectoryName(_files.GetValue(0).ToString());

            CheckZoneList();

            for (int _filenum = 0; _filenum < _files.Length; _filenum++)
            {
                FileInfo _file = new FileInfo(_files.GetValue(_filenum).ToString());

                if (_file.Exists)
                {
                    //MessageBox.Show(listZoneEffs.Items[0].Name);

                    for (int _zoneindex = 0; _zoneindex < listZoneEffs.Items.Count; _zoneindex++)
                    {
                        if (_file.Name.StartsWith(listZoneEffs.Items[_zoneindex].Text, StringComparison.CurrentCultureIgnoreCase))
                        {
                            listZoneEffs.Items[_zoneindex].Checked = true;
                            listZoneEffs.EnsureVisible(_zoneindex);
                            //listZoneEffs.TopIndex = Math.Max(_zoneindex - 3, 0);
                        }
                    }
                }
            }
        }

        private void buttonBrowseEQFolder_Click(object sender, EventArgs e)
        {
            folderBrowser.SelectedPath = textEQFolder.Text;
            
            if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textEQFolder.Text = folderBrowser.SelectedPath;

                CheckZoneList();
            }
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem _zoneItem in listZoneEffs.CheckedItems)
            {
                switch (ConvertFile(_zoneItem.Text))
                {
                    case System.Windows.Forms.DialogResult.OK:
                        string _emtFilename = textEQFolder.Text + "\\" + _zoneItem.Text + ".emt";
                        
                        // Don't open every newly merged .emt file if we're batch converting more than 3 files.
                        if ((listZoneEffs.CheckedItems.Count < 4) && File.Exists(_emtFilename))
                        {
                            // Open the file in Notepad++ if available, or Notepad if not. Other text file readers could be added as options here.

                            foreach (string _textEditor in TextEditors)
                            {
                                if (File.Exists(_textEditor))
                                {
                                    System.Diagnostics.Process.Start(_textEditor, _emtFilename);
                                    break;
                                }
                            }
                        }
                        break;
                    case System.Windows.Forms.DialogResult.Abort:
                        return;
                }
            }
        }

        // Return the entry from mp3index.txt on line number abs(ID)
        private string mp3indexFile(int ID)
        {
            if (ID < 0)
            {
                ID = -ID;
            }

            if (mp3indexFiles == null)
            {
                // Read the mp3index.txt file's entries, in case they've been changed from the defaults.

                if (File.Exists(textEQFolder.Text + "\\mp3index.txt"))
                {
                    string _line;
                    int _index = 1;

                    using (StreamReader _in = new StreamReader(textEQFolder.Text + "\\mp3index.txt"))
                    {
                        while ((_line = _in.ReadLine()) != null)
                        {
                            mp3indexFiles[_index++] = _line;
                        }
                    }
                }
                else
                {
                    // No mp3index.txt file found?? Use the defaults.

                    mp3indexFiles = DefaultMusicFiles;

                    MessageBox.Show("Note: Could not find the mp3index file at the following location:\n\n" + textEQFolder.Text + "\\mp3index.txt\n\nUsing default values from Live.", "No mp3index.txt - Using Defaults", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            string _result;

            if (mp3indexFiles.TryGetValue(ID, out _result))
            {
                return _result;
            }

            return "";
        }

        // Convert SoundID from ZoneNick_sounds.eff into a sound file name
        protected string SoundFileNumber(int SoundID)
        {
            string _result;

            // 0 == None
            if (SoundID == 0)
            {
                return "";
            }

            // < 0 == Music File Reference in mp3index.txt
            if (SoundID < 0)
            {
                return mp3indexFile(SoundID);
            }

            // 1 - 31 == Sound File Reference in ZoneNick_sndbnk.eff (EMIT section)
            if (SoundID < 32)
            {
                if ((SoundBank_Emit == null) || (SoundID > SoundBank_Emit.Count))
                {
                    return "";
                }
                else
                {
                    return SoundBank_Emit[SoundID - 1];
                }
            }

            // 162-169 == Sound File Reference in ZoneNick_sndbnk.eff (LOOP section)
            if (SoundID > 161)
            {
                SoundID -= 161;

                if ((SoundBank_Loop == null) || (SoundID > SoundBank_Loop.Count))
                {
                    return "";
                }
                else
                {
                    return SoundBank_Loop[SoundID - 1];
                }
            }

            // 32 - 161 == Hard-Coded Sound Files
            if (HardCodedSoundFiles.TryGetValue(SoundID, out _result))
            {
                return _result;
            }

            // Don't think we should ever get here, but if we do, it's an unsupported SoundID (maybe I missed a hard-coded sound file id?)
            return "";
        }

        // Converts ZoneNick_sounds.eff to ZoneNick.emt
        private DialogResult ConvertFile(string ZoneNick)
        {
            string _zoneSoundEntriesFilename = textEQFolder.Text + "\\" + ZoneNick + "_sounds.eff";
            string _zoneSoundBankFilename = textEQFolder.Text + "\\" + ZoneNick + "_sndbnk.eff";
            string _zoneSoundEmitterFilename = textEQFolder.Text + "\\" + ZoneNick + ".emt";

            List<string> _emtEntries = new List<string>();

            string _line;
            
            // Step 1 - Open ZoneNick_sounds.eff (Required)
            
            BinaryReader _effFile = null;

            while (_effFile == null)
            {
                try
                {
                    _effFile = new BinaryReader(File.OpenRead(_zoneSoundEntriesFilename));
                }
                catch (Exception ex)
                {
                    switch (MessageBox.Show("Could not create Sound Entries File:\n\n" + _zoneSoundEntriesFilename + "\n\nError Message:\n\n" + ex.Message, "Sound Entry Read Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error))
                    {
                        case System.Windows.Forms.DialogResult.Ignore:
                            return DialogResult.Ignore;
                        case System.Windows.Forms.DialogResult.Abort:
                            return DialogResult.Abort;
                    }
                }
            }

            // Step 2 - Open ZoneNick.emt if it exists, and read in the current contents for merging our new entries into
            
            if (File.Exists(_zoneSoundEmitterFilename))
            {
                // Read in existing lines for the merge.
                using (StreamReader _emtFileIn = new StreamReader(_zoneSoundEmitterFilename))
                {
                    while ((_line = _emtFileIn.ReadLine()) != null)
                    {
                        _emtEntries.Add(_line);
                    }
                }
            }

            // Step 3 - Create or replace ZoneNick.emt
            
            StreamWriter _emtFile = null;

            while (_emtFile == null)
            {
                try
                {
                    _emtFile = new StreamWriter(_zoneSoundEmitterFilename);
                }
                catch (Exception ex)
                {
                    switch (MessageBox.Show("Could not create Sound Emitter File:\n\n" + _zoneSoundEmitterFilename + "\n\nError Message:\n\n" + ex.Message, "Sound Emitter Creation Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error))
                    {
                        case System.Windows.Forms.DialogResult.Ignore:
                            return DialogResult.Ignore;
                        case System.Windows.Forms.DialogResult.Abort:
                            return DialogResult.Abort;
                    }
                }
            }

            // Step 4 - Read sound file references from ZoneNick_sndbnk.eff (Required unless only background music entries are in ZoneNick_sounds.eff)
            
            StreamReader _bnkFile = null;
            bool _tryAgain = true;

            SoundBank_Emit = new List<string>();
            SoundBank_Loop = new List<string>();

            bool _inEmitSection = true;

            while ((_bnkFile == null) && _tryAgain && File.Exists(_zoneSoundBankFilename))
            {
                try
                {
                    _bnkFile = new StreamReader(_zoneSoundBankFilename);
                }
                catch // (Exception ex)
                {
                    /*
                    switch (MessageBox.Show("Could not open Sound Bank File:\n\n" + _zoneSoundBankFilename + "\n\nError Message:\n\n" + ex.Message, "Sound Bank File Read Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error))
                    {
                        case System.Windows.Forms.DialogResult.Ignore:
                            _tryAgain = false;
                            break;
                        case System.Windows.Forms.DialogResult.Abort:
                            return DialogResult.Abort;
                    }
                    */
                }
            }

            if (_bnkFile != null)
            {
                while ((_line = _bnkFile.ReadLine()) != null)
                {
                    _line = _line.Trim();

                    if (_line == "EMIT")
                    {
                        _inEmitSection = true;
                    }
                    else if ((_line == "LOOP") || (_line == "RAND"))
                    {
                        _inEmitSection = false;
                    }
                    else if (_line != "")
                    {
                        if (_inEmitSection)
                        {
                            SoundBank_Emit.Add(_line);
                        }
                        else
                        {
                            SoundBank_Loop.Add(_line);
                        }
                    }
                }
                    
                _bnkFile.Close();
            }

            // Step 5 - Initialize ZoneNick.emt

            if ((_emtEntries.Count < 1) || (_emtEntries[0].Length < 1) || (_emtEntries[0][0] != ';'))
            {
                _emtFile.WriteLine(EMTLineFormat); // Add our format line for human-readable reference, if it isn't already there.
            }

            foreach (string _emtEntry in _emtEntries)
            {
                _emtFile.WriteLine(_emtEntry); // Write the existing entries to the file. We'll append any new ones to the end.
            }
            
            // Step 6 - Read binary entries from ZoneNick_sounds.eff and write text entries to ZoneNick.emt if they aren't already there.
            
            while ((_effFile.BaseStream.Length - _effFile.BaseStream.Position) >= 84)
            {
                EffSoundEntry _effEntry;

                _effEntry.UnkRef00 = _effFile.ReadInt32();
                _effEntry.UnkRef04 = _effFile.ReadInt32();
                _effEntry.Reserved = _effFile.ReadInt32();
                _effEntry.Sequence = _effFile.ReadInt32();
                _effEntry.X = _effFile.ReadSingle();
                _effEntry.Y = _effFile.ReadSingle();
                _effEntry.Z = _effFile.ReadSingle();
                _effEntry.Radius = _effFile.ReadSingle();
                _effEntry.Cooldown1 = _effFile.ReadInt32();
                _effEntry.Cooldown2 = _effFile.ReadInt32();
                _effEntry.RandomDelay = _effFile.ReadInt32();
                _effEntry.Unk44 = _effFile.ReadInt32();
                _effEntry.SoundID1 = _effFile.ReadInt32();
                _effEntry.SoundID2 = _effFile.ReadInt32();
                _effEntry.SoundType = _effFile.ReadByte();
                _effEntry.UnkPad57 = _effFile.ReadByte();
                _effEntry.UnkPad58 = _effFile.ReadByte();
                _effEntry.UnkPad59 = _effFile.ReadByte();
                _effEntry.AsDistance = _effFile.ReadInt32();
                _effEntry.UnkRange64 = _effFile.ReadInt32();
                _effEntry.FadeOutMS = _effFile.ReadInt32();
                _effEntry.UnkRange72 = _effFile.ReadInt32();
                _effEntry.FullVolRange = _effFile.ReadInt32();
                _effEntry.UnkRange80 = _effFile.ReadInt32();

                EmtSoundEntry _sound1 = new EmtSoundEntry();
                EmtSoundEntry _sound2;

                string _soundFile1;
                string _soundFile2;

                switch (_effEntry.SoundType)
                {
                    case 0: // Day/Night Sound Effect, Constant Volume
                        _soundFile1 = SoundFileNumber(_effEntry.SoundID1);
                        _soundFile2 = SoundFileNumber(_effEntry.SoundID2);
                        _sound1.WhenActive = 1;
                        break;
                    case 1: // Background Music
                        _soundFile1 = (_effEntry.SoundID1 < 0) ? SoundFileNumber(_effEntry.SoundID1) : (_effEntry.SoundID1 == 0) ? "" : ZoneNick + ".xmi";
                        if (_effEntry.SoundID1 == _effEntry.SoundID2)
                        {
                            _soundFile2 = "";
                            _sound1.WhenActive = 0; // Same music and location for day and night. No need to add two entries to .emt file.
                        }
                        else
                        {
                            _soundFile2 = (_effEntry.SoundID2 < 0) ? SoundFileNumber(_effEntry.SoundID2) : (_effEntry.SoundID2 == 0) ? "" : ZoneNick + ".xmi";
                            _sound1.WhenActive = 1;
                        }

                        _sound1.xmiIndex = ((_effEntry.SoundID1 > 0) && (_effEntry.SoundID1 < 32)) ? _effEntry.SoundID1 : 0;
                        break;
                    case 2: // Static Sound Effect
                        _soundFile1 = SoundFileNumber(_effEntry.SoundID1);
                        _soundFile2 = "";

                        _sound1.Volume = (_effEntry.AsDistance < 0) ? 0.0f : (_effEntry.AsDistance > 3000) ? 0.0f : (3000.0f - (float)_effEntry.AsDistance) / 3000.0f;
                        break;
                    case 3: // Day/Night Sound Effect, Volume by Distance
                        _soundFile1 = SoundFileNumber(_effEntry.SoundID1);
                        _soundFile2 = SoundFileNumber(_effEntry.SoundID2);

                        _sound1.Volume = (_effEntry.AsDistance < 0) ? 0.0f : (_effEntry.AsDistance > 3000) ? 0.0f : (3000.0f - (float)_effEntry.AsDistance) / 3000.0f;
                        _sound1.WhenActive = 1;
                        break;
                    default: // Unsupported
                        continue;
                }

                if ((_soundFile1.Length > 0) && (_soundFile1.IndexOf('.') < 0))
                {
                    _soundFile1 += ".wav";
                }

                _sound1.SoundFile = _soundFile1;
                
                _sound1.FadeOutMS = (_effEntry.FadeOutMS <= 0) ? 0 : (_effEntry.FadeOutMS < 100) ? 100 : _effEntry.FadeOutMS; // Sanity check: make sure FadeOutMS is either 0 or 100+ milliseconds
                
                // Fade in does not appear to be utilized in ZoneNick_sounds.eff, even if one of the Unk fields is supposed to map to it. Make sounds fade in at twice the speed of fading out, 
                // so a running player doesn't feel like a sound effect finished fading in after they already passed by.
                _sound1.FadeInMS = Math.Min(_sound1.FadeOutMS / 2, 5000); // Cap at 5 second FadeIn. Some music entries have a looooong FadeOut.

                _sound1.X = _effEntry.X;
                _sound1.Y = _effEntry.Y;
                _sound1.Z = _effEntry.Z;
                if (_effEntry.SoundType != 1) // Music files ignore cooldowns in ZoneNick.emt.
                {
                    _sound1.WavLoopType = (_effEntry.Cooldown1 <= 0) && (_effEntry.RandomDelay <= 0) ? 0 : 1;
                    _sound1.MinRepeatDelay = (_sound1.WavLoopType == 0) ? 0 : Math.Max(_effEntry.Cooldown1, 0);
                    _sound1.MaxRepeatDelay = (_sound1.WavLoopType == 0) ? 0 : Math.Max(_effEntry.Cooldown1, 0) + Math.Max(_effEntry.RandomDelay, 0);
                }
                _sound1.WavFullVolRadius = (_effEntry.SoundType == 0) ? _effEntry.Radius : Math.Max(_effEntry.FullVolRange, 0);
                _sound1.ActivationRange = (Int32)_effEntry.Radius;
                _sound1.WavMaxAudibleDist = _sound1.ActivationRange; // No fields in ZoneNick_sounds.eff appear to map to this, so use the activation range.
                _sound1.IsEnvSound = (_effEntry.SoundType != 1);

                if ((_soundFile2 == null) || (_soundFile2 == ""))
                {
                    _sound2 = new EmtSoundEntry();
                }
                else
                {
                    _sound2 = _sound1.Clone();

                    if (_soundFile2.IndexOf('.') < 0)
                    {
                        _soundFile2 += ".wav";
                    }

                    _sound2.SoundFile = _soundFile2;

                    if (_effEntry.SoundType != 1) // Music files ignore cooldowns in ZoneNick.emt.
                    {
                        _sound2.WavLoopType = (_effEntry.Cooldown2 <= 0) && (_effEntry.RandomDelay <= 0) ? 0 : 1;
                        _sound2.MinRepeatDelay = Math.Max(_effEntry.Cooldown2, 0);
                        _sound2.MaxRepeatDelay = Math.Max(_effEntry.Cooldown2, 0) + Math.Max(_effEntry.RandomDelay, 0);
                    }

                    switch (_effEntry.SoundType)
                    {
                        case 0: // Day/Night Sound Effect, Constant Volume
                            _sound2.WhenActive = 2;
                            break;
                        case 1: // Background Music
                            _sound2.WhenActive = 2;
                            _sound2.xmiIndex = ((_effEntry.SoundID2 > 0) && (_effEntry.SoundID2 < 32)) ? _effEntry.SoundID2 : 0;
                            break;
                        case 3: // Day/Night Sound Effect, Volume by Distance
                            _sound2.WhenActive = 2;
                            break;
                        default: // Unsupported
                            continue;
                    }
                }

                _soundFile1 = _sound1.ToString();
                _soundFile2 = _sound2.ToString();

                if ((_soundFile1.Length > 0) && (!_emtEntries.Contains(_soundFile1)))
                {
                    _emtEntries.Add(_soundFile1);
                    _emtFile.WriteLine(_soundFile1);
                }

                if ((_soundFile2.Length > 0) && (!_emtEntries.Contains(_soundFile2)))
                {
                    _emtEntries.Add(_soundFile2);
                    _emtFile.WriteLine(_soundFile2);
                }
            }

            _emtFile.Close();

            return System.Windows.Forms.DialogResult.OK;
        }

        private void listZoneEffs_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            CheckIfReady();
        }
    }
}
