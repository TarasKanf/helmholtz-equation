using System.Collections.Generic;

namespace SocialNetwork.Services.CSVExport
{
    public interface IDataExport<T>
    {
        string Extention { get; }

        void Save(string filePath, List<T> itemList, bool append);

        void Save(string filePath, bool append);
    }
}