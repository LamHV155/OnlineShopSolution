using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopSolution.Service.Common
{
    public interface IStorageService
    {
        string GetFileUrl(string fileName);
        Task SaveFileAsync(Stream mediabinaryStream, string fileName);

        Task DeleteFileAsync(string fileName);
    }
}
