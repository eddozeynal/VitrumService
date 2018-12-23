using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ERPService
{
    public class CollectionChangesComparer<T>
    {
        public string KeyFieldName { get; set; }

        private List<T> initialList;
        private List<T> finalList;

        public void SetInitialList(List<T> list)
        {
            initialList = list;
        }

        public void SetFinalList(List<T> list)
        {
            finalList = list;
        }

        public List<T> GetInserts()
        {
            List<T> list = new List<T>();
            Type mType = typeof(T);
            IList<PropertyInfo> props = new List<PropertyInfo>(mType.GetProperties());

            List<object> oldKeyList = new List<object>();

            foreach (T oldItem in initialList)
            {
                object oldKeyObject = props.Where(x => x.Name == KeyFieldName).First().GetValue(oldItem, null);
                oldKeyList.Add(oldKeyObject);
            }

            foreach (T item in finalList)
            {
                object keyObject = props.Where(x => x.Name == KeyFieldName).First().GetValue(item, null);
                if (!oldKeyList.Contains(keyObject)) list.Add(item);

            }

            return list;
        }

        public List<T> GetUpdates()
        {
            List<T> list = new List<T>();
            Type mType = typeof(T);
            IList<PropertyInfo> props = new List<PropertyInfo>(mType.GetProperties());

            List<object> oldKeyList = new List<object>();

            foreach (T oldItem in initialList)
            {
                object oldKeyObject = props.Where(x => x.Name == KeyFieldName).First().GetValue(oldItem, null);
                oldKeyList.Add(oldKeyObject);
            }

            foreach (T item in finalList)
            {
                object keyObject = props.Where(x => x.Name == KeyFieldName).First().GetValue(item, null);
                if (oldKeyList.Contains(keyObject))
                {
                    foreach (T oldItem in initialList)
                    {
                        object oldKeyObject = props.Where(x => x.Name == KeyFieldName).First().GetValue(oldItem, null);
                        if (oldKeyObject.Equals(keyObject))
                        {
                            // Bu hemin objectdir
                            foreach (var prop in props)
                            {
                                if (!prop.GetValue(item, null).Equals(prop.GetValue(oldItem, null)))
                                {
                                    if (!list.Contains(item)) list.Add(item);
                                }
                            }
                        }
                    }
                }

            }

            return list;
        }

        public List<T> GetDeletes()
        {
            List<T> list = new List<T>();
            Type mType = typeof(T);
            IList<PropertyInfo> props = new List<PropertyInfo>(mType.GetProperties());

            List<object> newKeyList = new List<object>();

            foreach (T newItem in finalList)
            {
                object newKeyObject = props.Where(x => x.Name == KeyFieldName).First().GetValue(newItem, null);
                newKeyList.Add(newKeyObject);
            }

            foreach (T olditem in initialList)
            {
                object oldkeyObject = props.Where(x => x.Name == KeyFieldName).First().GetValue(olditem, null);
                if (!newKeyList.Contains(oldkeyObject)) list.Add(olditem);

            }

            return list;
        }

    }
}