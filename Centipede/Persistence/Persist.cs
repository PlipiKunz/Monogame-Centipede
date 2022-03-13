using System;
using System.Collections.Generic;
using System.Text;
//
// Added to support serialization
using System.IO;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

using Microsoft.Xna.Framework.Input;
using CS5410.Input;

namespace CS5410.Persistence
{
    public class Persist
    {
        static private bool saving = false;
        static private bool loading = false;
        static private string controlsFileName = "Controls.xml";
        static public bool? controlsExists = null;
        static public Dictionary<KeyboardActions, Keys> m_loadedControls = null;

        public void saveControls()
        {
            lock (this)
            {
                if (!Persist.saving)
                {
                    Persist.saving = true;
                    finalizeSaveControlsAsync(JsonConvert.SerializeObject(KeyboardPersistence.actionToKey));
                }
            }
        }

        public void save(String json, string fileName)
        {
            lock (this)
            {
                if (!Persist.saving)
                {
                    Persist.saving = true;
                    finalizeSaveControlsAsync(json);
                }
            }
        }
        private async void finalizeSaveControlsAsync(String dict)
        {
            
            await Task.Run(() =>
            {
                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    try
                    {
                        using (IsolatedStorageFileStream fs = storage.OpenFile(Persist.controlsFileName, FileMode.OpenOrCreate))
                        {
                            if (fs != null)
                            {
                                XmlSerializer mySerializer = new XmlSerializer(typeof(String));
                                mySerializer.Serialize(fs, dict);
                            }
                        }
                    }
                    catch (IsolatedStorageException)
                    {
                        // Ideally show something to the user, but this is demo code :)
                    }
                }

                Persist.saving = false;
            });
        }

        public void loadControls()
        {
            lock (this)
            {
                if (!Persist.loading)
                {
                    Persist.loading = true;
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    finalizeLoadControlsAsync();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                }
            }
        }
        private async Task finalizeLoadControlsAsync()
        {
            await Task.Run(() =>
            {
                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    try
                    {
                        if (storage.FileExists(Persist.controlsFileName))
                        {
                            using (IsolatedStorageFileStream fs = storage.OpenFile(Persist.controlsFileName, FileMode.Open))
                            {
                                if (fs != null)
                                {
                                    XmlSerializer mySerializer = new XmlSerializer(typeof(String));

                                    m_loadedControls = JsonConvert.DeserializeObject<Dictionary<KeyboardActions, Keys>>((String)mySerializer.Deserialize(fs));

                                    controlsExists = true;
                                }
                                else
                                {
                                    controlsExists = false;
                                }
                            }
                        }
                        else {
                            controlsExists = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        controlsExists = false;
                    }
                }

                Persist.loading = false;
            });
        }
    }
}
