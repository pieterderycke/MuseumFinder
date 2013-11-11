using System;
namespace MuseumFinder.Util
{
    public interface IStorageHelper
    {
        T GetSetting<T>(string key);
        T GetSetting<T>(string key, T defaultVal);
        void RemoveSetting(string key);
        bool StoreSetting(string key, object value, bool overwrite);
    }
}
