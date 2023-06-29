using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Library.Extension
{
    public static class ObjectExtension
    {
        #region PUBLIB

        /// <summary>
        /// 包含變數
        /// </summary>
        /// <param name="expandoObj">物件</param>
        /// <param name="name">包含變數</param>
        /// <returns></returns>
        public static bool HasProperty(this ExpandoObject expandoObj, string name)
        {
            return ((IDictionary<string, object>)expandoObj).ContainsKey(name);
        }

        /// <summary>
        /// ConvertUtcTime
        /// </summary>
        public static void ConvertUtcTime<T>(this T Model)
        {
            if (Model == null)
                return;

            var ModelType = Model.GetType();
            if (ModelType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(ModelType))
            {

                var ModelI = Model as IEnumerable;

                if (ModelI != null) {
                    foreach (var Temp in ModelI)
                    {
                        Temp.ConvertUtcTime();
                    } // end foreach
                }

            
            }
            else
            {
                //Reflection 去訪問所有Property
                foreach (var PropInfo in Model.GetType().GetProperties())
                {
                    var PropertyName = PropInfo.Name;
                    var PropertyValue = PropInfo.GetValue(Model);
                    var PropertyType = PropInfo.PropertyType;

                    if (PropertyValue == null)
                    {
                        continue;
                    } // end if

                    // 結構式Property 需要深入拜訪
                    if (PropertyType.IsSystemType() == false)
                    {
                        PropertyValue.ConvertUtcTime();
                    }
                    else if (PropertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(PropertyType))
                    {
                        var PropertyValueI = PropertyValue as IEnumerable;

                        if (PropertyValueI != null)
                        {
                            foreach (var Temp in PropertyValueI)
                            {
                                Temp.ConvertUtcTime();
                            } // end foreach
                        }
                    }
                    else if (PropertyType == typeof(DateTime))
                    {
                        var utcTime = DateTime.SpecifyKind((DateTime)PropertyValue, DateTimeKind.Utc);
                        PropInfo.SetValue(Model, utcTime);
                    } // end if
                } // end foreach
            }
        } // end ConvertTimeTick

        /// <summary>
        /// 將資料模型轉換成KeyValuePair
        /// </summary>
        public static string ConvertToPostString(this object Model, bool AllowNullOrEmpty = false)
        {
            if (Model == null)
                return string.Empty;

            var Result = new List<string>();

            foreach (var property in Model.GetType().GetProperties())
            {
                var Key = property.Name;
                var Value = property.GetValue(Model);

                if (AllowNullOrEmpty || Value != null)
                    Result.Add(string.Format("{0}={1}", Key, Value == null ? "" : Value.ToString()));
            } // end foreach

            return string.Join("&", Result);
        } // end ConvertToPostString

        /// <summary>
        /// 兩個資料模型作映射
        /// </summary>
        public static T1 Mapper<T1, T2>(this T1 BaseModel, T2 RefModel)
        {
            var BaseType = BaseModel.GetType();
            var RefType = RefModel.GetType();

            // 預設型別 禁止
            if (BaseType.IsSystemType() || RefType.IsSystemType())
                return BaseModel;

            //Reflection 去訪問所有Property
            foreach (var BasePropInfo in BaseType.GetProperties())
            {
                var RefPropInfo = RefType.GetProperty(BasePropInfo.Name);
                if (RefPropInfo != null)
                {
                    var RefPropValue = RefPropInfo.GetValue(RefModel);

                    var BasePropertyType = Nullable.GetUnderlyingType(BasePropInfo.PropertyType) ?? BasePropInfo.PropertyType;
                    var RefPropertyType = Nullable.GetUnderlyingType(RefPropInfo.PropertyType) ?? RefPropInfo.PropertyType;

                    //型別一樣 直接賦值
                    if (BasePropertyType == RefPropertyType)
                    {
                        if (BasePropInfo.CanWrite)
                            BasePropInfo.SetValue(BaseModel, RefPropValue);
                    }
                    else if (BasePropInfo.PropertyType.IsSystemType() == false && RefPropInfo.PropertyType.IsSystemType() == false)
                    {
                        // Mapper
                        if (BasePropInfo.CanRead)
                            BasePropInfo.GetValue(BaseModel).Mapper(RefPropValue);
                    } // end if
                } // end if                
            } // end foreach

            return BaseModel;
        } // end Mapper

        /// <summary>
        /// 兩個資料模型作映射
        /// </summary>
        public static T1 MapperNotNull<T1, T2>(this T1 BaseModel, T2 RefModel)
        {
            var BaseType = BaseModel.GetType();
            var RefType = RefModel.GetType();

            // 預設型別 禁止
            if (BaseType.IsSystemType() || RefType.IsSystemType())
                return BaseModel;

            //Reflection 去訪問所有Property
            foreach (var BasePropInfo in BaseType.GetProperties())
            {
                var RefPropInfo = RefType.GetProperty(BasePropInfo.Name);
                if (RefPropInfo != null)
                {
                    var RefPropValue = RefPropInfo.GetValue(RefModel);
                    if (RefPropValue != null)
                    {
                        var BasePropertyType = Nullable.GetUnderlyingType(BasePropInfo.PropertyType) ?? BasePropInfo.PropertyType;
                        var RefPropertyType = Nullable.GetUnderlyingType(RefPropInfo.PropertyType) ?? RefPropInfo.PropertyType;

                        //型別一樣 直接賦值
                        if (BasePropertyType == RefPropertyType)
                        {
                            if (BasePropInfo.CanWrite)
                                BasePropInfo.SetValue(BaseModel, RefPropValue);
                        }
                        else if (BasePropInfo.PropertyType.IsSystemType() == false && RefPropInfo.PropertyType.IsSystemType() == false)
                        {
                            // Mapper
                            if (BasePropInfo.CanRead)
                                BasePropInfo.GetValue(BaseModel).Mapper(RefPropValue);
                        } // end if
                    } // end if
                } // end if                
            } // end foreach

            return BaseModel;
        } // end Mapper

        /// <summary>
        /// Check is System Type [格式類型驗證]
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsSystemType(this Type type)
        {
            //return type.FullName.StartsWith("System");
            return type.Assembly == typeof(object).Assembly;
        }

        /// <summary>
        /// 轉換CSV檔案
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="filePath"></param>
        public static void CreateCSV<T>(this List<T> list, string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                CreateHeader(list, sw);
                CreateRows(list, sw);
            }
        }

        /// <summary>
        /// 轉換資料表格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this List<T> list)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in list)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

      
        /*
                /// <summary>
                /// 轉換資料表格式 原始轉換有問題 查詢只有_TEST使用
                /// </summary>
                /// <typeparam name="T"></typeparam>
                /// <param name="list"></param>
                /// <returns></returns>
                public static DataTable ToDataTable<T>(this List<T> list)
                {
                    var props = typeof(T).GetProperties();
                    var dt = new DataTable();

                    foreach (var item in props)
                        dt.Columns.Add(item.Name, item.PropertyType);

                    foreach (var item in list)
                    {
                        var tempList = new ArrayList();
                        foreach (PropertyInfo pi in props)
                        {
                            var obj = pi.GetValue(item);
                            tempList.Add(obj);
                        }
                        var array = tempList.ToArray();
                        dt.LoadDataRow(array, true);
                    }

                    return dt;
                }
        */
        /// <summary>
        /// 轉換XML格式
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string XmlSerialize(this object model)
        {
            XmlSerializer ser = new XmlSerializer(model.GetType());
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);
            ser.Serialize(writer, model);
            return sb.ToString();
        }

        /// <summary>
        /// 匯出XML格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        //public static T XmlDeserialize<T>(this string xmlString)
        //{
        //    XmlDocument xdoc = new XmlDocument();
        //    xdoc.LoadXml(xmlString);

        //    if (xdoc.FirstChild.NodeType == XmlNodeType.XmlDeclaration)
        //        xdoc.RemoveChild(xdoc.FirstChild);

        //    XmlNodeReader reader = new XmlNodeReader(xdoc.DocumentElement);
        //    XmlSerializer ser = new XmlSerializer(typeof(T));
        //    object obj = ser.Deserialize(reader);
        //    return (T)obj;
        //}
        #endregion

        #region PRIVATE

        /// <summary>
        /// 新增標頭
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="sw"></param>
        private static void CreateHeader<T>(List<T> list, StreamWriter sw)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            for (int i = 0; i < properties.Length - 1; i++)
            {
                sw.Write(properties[i].Name + ",");
            }
            var lastProp = properties[properties.Length - 1].Name;
            sw.Write(lastProp + sw.NewLine);
        }

        /// <summary>
        /// 新增內容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="sw"></param>
        private static void CreateRows<T>(List<T> list, StreamWriter sw)
        {
            foreach (var item in list)
            {
                PropertyInfo[] properties = typeof(T).GetProperties();
                for (int i = 0; i < properties.Length - 1; i++)
                {
                    var prop = properties[i];
                    sw.Write(prop.GetValue(item) + ",");
                }
                var lastProp = properties[properties.Length - 1];
                sw.Write(lastProp.GetValue(item) + sw.NewLine);
            }
        }


        public static T As<T>(this object self)
        {
            var t = typeof(T);
            var u = Nullable.GetUnderlyingType(t);
            var decidedType = u ?? t;
            var nullable = (u != null);

            if (self == null || Convert.IsDBNull(self))
            {
                if (decidedType.IsValueType && !nullable)
                {
                    // 數值型別不可接受 NULL 值
                    throw new ArgumentNullException();
                }

                object dummy = null;
                return (T)dummy;
            }

            if (decidedType.IsEnum)
            {
                return (T)Enum.Parse(decidedType, self.ToString());
            }

            return (T)Convert.ChangeType(self, decidedType);
        }

        public static T As<T>(this object self, T defaultValue)
        {
            if (self == null || Convert.IsDBNull(self)) return defaultValue;
            return self.As<T>();
        }

        #endregion
    }
}
