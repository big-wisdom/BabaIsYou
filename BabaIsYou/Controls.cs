using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Components;
using Microsoft.Xna.Framework.Input;

namespace BabaIsYou
{
    public class Controls
    {
        private Dictionary<Keys, Direction> keyFirstControls = new Dictionary<Keys, Direction>()
        {
            { Keys.Up, Direction.Up },
            { Keys.Right, Direction.Right },
            { Keys.Down, Direction.Down },
            { Keys.Left, Direction.Left },
        };

        private Dictionary<Direction, Keys> controlFirstControls;


        public List<Direction> controlsList {
            get {
                return new List<Direction>(keyFirstControls.Values);
            }
        }

        public Controls()
        {
            loadSomething(); // initialize controls from local storage file
            // loadSomething also calls saveSomething if nothing is there which
            // will persist the current state of the controls
        }

        private void initializeDefaults()
        {
            controlFirstControls = swapDictionary<Keys, Direction>(keyFirstControls);
            saveSomething();
        }

        public void persist()
        {
            saveSomething();
        }

        public Nullable<Direction> getControl(Keys key)
        {
            if (keyFirstControls.ContainsKey(key))
                return keyFirstControls[key];
            else
                return null;
        }

        public Keys getKey(Direction control)
        { 
            return controlFirstControls[control];
        }

        public bool setControl(Keys key, Direction control)
        { 
            if (keyFirstControls.ContainsKey(key)) // check if key is already used
            {
                if (keyFirstControls[key] == control) // if control is already right just leave it
                    return true;
                else
                    return false; // this eliminate duplicate keys from being added
            }
            else
            {
                Keys currentKey = controlFirstControls[control];
                controlFirstControls[control] = key;
                keyFirstControls.Remove(currentKey); // remove the old control
                keyFirstControls[key] = control;
                return true;
            }
        }

        public Dictionary<TValue, TKey> swapDictionary<TKey, TValue>(Dictionary<TKey, TValue> source)
        {
            Dictionary<TValue, TKey> result = new Dictionary<TValue, TKey>();
            foreach (var entry in source)
            {
                if (!result.ContainsKey(entry.Value))
                {
                    result.Add(entry.Value, entry.Key); // in case there are duplicate values in the original (should't be a problem here)
                }
                else
                {
                    return null;
                }
            }
            return result;
        }

        private bool saving = false;
        private void saveSomething()
        {
            lock (this)
            {
                if (!this.saving)
                {
                    this.saving = true;
                    //
                    // Create something to save
                    finalizeSaveAsync(ControlsPersist.fromDictionary(controlFirstControls));
                }
            }
        }

        private async void finalizeSaveAsync(ControlsPersist state)
        {
            await Task.Run(() =>
            {
                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    try
                    {
                        using (IsolatedStorageFileStream fs = storage.OpenFile("Controls.xml", FileMode.Create))
                        {
                            if (fs != null)
                            {
                                XmlSerializer mySerializer = new XmlSerializer(typeof(ControlsPersist));
                                mySerializer.Serialize(fs, state);
                            }
                        }
                    }
                    catch (IsolatedStorageException)
                    {
                        throw new Exception("Controls.xml does not exist");
                        // Ideally show something to the user, but this is demo code :)
                    }
                }

                this.saving = false;
            });
        }

        private bool loading = false;
        private void loadSomething()
        {
            lock (this)
            {
                if (!this.loading)
                {
                    this.loading = true;
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    finalizeLoadAsync();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                }
            }
        }
        private ControlsPersist m_loadedState = null;


        private async Task finalizeLoadAsync()
        {
            await Task.Run(() =>
            {
                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    // storage.Remove();
                    try
                    {
                        if (storage.FileExists("Controls.xml"))
                        {
                            using (IsolatedStorageFileStream fs = storage.OpenFile("Controls.xml", FileMode.Open))
                            {
                                if (fs != null)
                                {
                                    XmlSerializer mySerializer = new XmlSerializer(typeof(ControlsPersist));
                                    m_loadedState = (ControlsPersist)mySerializer.Deserialize(fs);
                                    controlFirstControls = m_loadedState.ControlsFirstDictionary();
                                    keyFirstControls = swapDictionary<Direction, Keys>(controlFirstControls);
                                }
                            }
                        }
                        else
                        {
                            initializeDefaults();
                        }
                    }
                    catch (IsolatedStorageException)
                    {
                        // Ideally show something to the user, but this is demo code :)
                    }
                }

                this.loading = false;
            });
        }

    }
}
