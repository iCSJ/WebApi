using CyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CyApi.BLL
{
    public class OptionService :CyService
    {
        public Task<Option> GetByKey(string key)
        {
            return Task.Run(() =>
            {
                return Db.Items<Option>().Where(p => p.Key == key).FirstOrDefault();
            });
        }
        public Task<int> SetKeyValue(string key, string value)
        {
            return Task.Run(() =>
            {
                Option obj = GetByKey(key).Result;
                if (obj == null)
                {
                    obj = new Option() { Key = key, Value = value };
                }
                return InsertOrUpdate(obj);
            });
        }
    }
}