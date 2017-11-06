using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Collections;
using System.IO;

namespace WebBrowserForLand
{
    public class XmlStorageHelper
    {
        /// <summary>
        /// �ڲ��ࣺ����ʱ��Ϣ��assembly���ƣ�����������
        /// </summary>
        class Runtime
        {
            /// <summary>
            /// �������������ƣ�
            /// </summary>
            public string FullTypeName = string.Empty;
            /// <summary>
            /// ���п����������汾��Ϣ��
            /// </summary>
            public string AssemblyName = string.Empty;
            /// <summary>
            /// �жϸ����п��Ƿ��Ѿ������صı�ʾ
            /// </summary>
            bool isAssemblyLoaded = false;
            /// <summary>
            /// �Ѿ����ص����п�ʵ��
            /// </summary>
            Assembly loadedAssembly = null;

            public Runtime()
            {
            }

            public Runtime(string typeName, string asmName)
            {
                FullTypeName = typeName;
                AssemblyName = asmName;
            }

            /// <summary>
            /// �������п��е�ʵ��
            /// </summary>
            /// <param name="className">������ȫ����</param>
            /// <returns>�����ɹ��󷵻�ʵ��������ʧ�ܷ���Null</returns>
            public Type GetTypeByFullName(string className)
            {
                Assembly asm = LoadedAssembly;
                if (asm == null)
                    return null;
                return asm.GetType(className);
            }

            /// <summary>
            /// ��ȡ���п��ʵ��
            /// </summary>
            Assembly LoadedAssembly
            {
                get
                {
                    if (!isAssemblyLoaded)
                    {
                        isAssemblyLoaded = true;
                        try
                        {
                            loadedAssembly = Assembly.Load(AssemblyName);
                            if (loadedAssembly == null)
                            {
                                string exePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                                string assemblyFile = Path.Combine(exePath, AssemblyName);
                                if (File.Exists(assemblyFile))
                                    loadedAssembly = Assembly.LoadFile(assemblyFile);
                            }
                        }
                        catch
                        {
                        }
                    }
                    return loadedAssembly;
                }
            }
        }

        /// <summary>
        /// XML�ĵ�����
        /// </summary>
        XmlDocument xmldoc = new XmlDocument();

        const string Collection_Tag = "InnerCollection";
        const string Dictionary_Tag = "InnerDictionary";

        List<Runtime> runtimeList = new List<Runtime>();

        void AddRuntime(string typeName, string asmName)
        {
            foreach (Runtime rt in runtimeList)
            {
                if (rt.FullTypeName.Equals(typeName) && rt.AssemblyName.Equals(asmName))
                {
                    return;
                }
            }
            //add new runtime
            Runtime r = new Runtime(typeName, asmName);
            runtimeList.Add(r);
        }

        void SetCollectionToXml(ICollection colObject, XmlElement parentEle)
        {
            if (colObject == null)
                return;
            foreach (object o in colObject)
            {
                if (o == null)
                    continue;
                if (IsBasicType(o.GetType()))
                {
                    AddRuntime(o.GetType().FullName, o.GetType().Assembly.GetName().Name);
                    string attValue = o.ToString();
                    XmlElement subEle = xmldoc.CreateElement(o.GetType().Name);
                    subEle.SetAttribute("value", attValue);
                    parentEle.AppendChild(subEle);
                }
                //customer data type
                else
                {
                    SetValueToXml(o, parentEle);
                }
            }
        }

        void SetDictionaryToXml(IDictionary dictObject, XmlElement parentEle)
        {
            if (dictObject == null)
                return;
            foreach (DictionaryEntry entry in dictObject)
            {
                XmlElement entryEle = xmldoc.CreateElement(entry.GetType().Name);
                if (IsBasicType(entry.Key.GetType()))
                {
                    AddRuntime(entry.Key.GetType().FullName, entry.Key.GetType().Assembly.GetName().Name);
                    XmlElement keyEle = xmldoc.CreateElement(entry.Key.GetType().Name);
                    keyEle.SetAttribute("key", entry.Key.ToString());
                    entryEle.AppendChild(keyEle);
                }
                else
                {
                    SetValueToXml(entry.Key, entryEle);
                }
                if (IsBasicType(entry.Value.GetType()))
                {
                    AddRuntime(entry.Value.GetType().FullName, entry.Value.GetType().Assembly.GetName().Name);
                    XmlElement valueEle = xmldoc.CreateElement(entry.Value.GetType().Name);
                    valueEle.SetAttribute("value", entry.Value.ToString());
                    entryEle.AppendChild(valueEle);
                }
                else
                {
                    SetValueToXml(entry.Value, entryEle);
                }
                parentEle.AppendChild(entryEle);
            }
        }

        void SetValueToXml(object xmlObject, XmlElement parentEle)
        {
            SetValueToXml(xmlObject, parentEle, null);
        }

        void SetValueToXml(object xmlObject, XmlElement parentEle, string tagName)
        {
            if (xmlObject == null)
                return;
            XmlElement classEle = null;
            Type objType = xmlObject.GetType();
            //1.�����Ƿ�Ϊ����
            if (IsCollection(objType))
            {
                classEle = null;
                string typeString = objType.FullName.Replace('`', '#');
                if (IsDictionary(objType))
                {
                    classEle = xmldoc.CreateElement(Dictionary_Tag);
                    classEle.SetAttribute("TypeName", typeString);
                    SetDictionaryToXml((IDictionary)xmlObject, classEle);
                }
                else
                {
                    classEle = xmldoc.CreateElement(Collection_Tag);
                    classEle.SetAttribute("TypeName", typeString);
                    SetCollectionToXml((ICollection)xmlObject, classEle);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(tagName))
                    classEle = xmldoc.CreateElement(objType.Name);
                else
                    classEle = xmldoc.CreateElement(tagName);
                AddRuntime(objType.FullName, objType.Assembly.GetName().Name);
                foreach (FieldInfo field in objType.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty))
                {
                    object objValue = field.GetValue(xmlObject);
                    //����Ǽ������ͣ�����Ҫ��ȡ�¼�
                    if (IsCollection(field.FieldType))
                    {
                        XmlElement colEle = xmldoc.CreateElement(field.Name);
                        if (IsDictionary(field.FieldType))  //�Ƿ�Ϊ�ֵ�
                        {
                            SetDictionaryToXml((IDictionary)objValue, colEle);
                        }
                        else
                        {
                            SetCollectionToXml((ICollection)objValue, colEle);
                        }
                        classEle.AppendChild(colEle);
                    }
                    else
                    {
                        if (IsBasicType(field.FieldType))
                        {
                            string attValue = "";
                            if (objValue != null)
                                attValue = objValue.ToString();
                            classEle.SetAttribute(field.Name, attValue);
                        }
                        //customer data type
                        else
                        {
                            SetValueToXml(objValue, classEle, field.Name);
                        }
                    }
                }
            }
            if (parentEle != null)
                parentEle.AppendChild(classEle);
            else
                xmldoc.AppendChild(classEle);
        }

        void GetCollectionFromXml(IList colObject, XmlNode colNode)
        {
            if (colObject == null || colNode == null)
                return;
            //��ʼ�������е�ÿ��Ԫ�أ�����Ԫ����ӵ�������
            foreach (XmlNode eleNode in colNode.ChildNodes)
            {
                Type eleType = null;
                if (eleNode.Name.Equals(Collection_Tag) || eleNode.Name.Equals(Dictionary_Tag))
                {
                    XmlAttribute typeAtt = eleNode.Attributes["TypeName"];
                    if (typeAtt != null)
                    {
                        string typeString = typeAtt.Value.Replace('#', '`');
                        try
                        {
                            eleType = Type.GetType(typeString);
                        }
                        catch { }
                    }
                }
                else
                {
                    eleType = TryToGetType(eleNode.Name);
                }
                if (eleType == null)
                    continue;
                if (IsBasicType(eleType))
                {
                    XmlAttribute att = eleNode.Attributes["value"];
                    if (att != null)
                    {
                        try
                        {
                            if (eleType.IsSubclassOf(typeof(Enum)))
                            {
                                object enumValue = Enum.Parse(eleType, att.Value);
                                colObject.Add(enumValue);
                            }
                            else
                            {
                                //ת��ΪĿ������
                                object ovalue = Convert.ChangeType(att.Value, eleType);
                                colObject.Add(ovalue);
                            }

                        }
                        catch { }
                    }
                }
                else
                {
                    object o = Activator.CreateInstance(eleType, true);
                    GetValueFromXml(o, eleNode);
                    colObject.Add(o);
                }
            }
        }

        void GetDictionaryFromXml(IDictionary dictObject, XmlNode dictNode)
        {
            if (dictObject == null || dictNode == null)
                return;
            //��ʼ���ֵ��е�ÿ��Ԫ�أ�����Ԫ����ӵ��ֵ���
            foreach (XmlNode kvNode in dictNode.ChildNodes)
            {
                if (kvNode.Name.Equals("DictionaryEntry"))
                {
                    if (kvNode.ChildNodes.Count != 2)
                        continue;
                    object entryKey = new object();
                    object entryValue = new object();
                    XmlNode keyNode = kvNode.ChildNodes[0];
                    Type keyType = null;
                    if (keyNode.Name.Equals(Collection_Tag) || keyNode.Name.Equals(Dictionary_Tag))
                    {
                        XmlAttribute typeAtt = keyNode.Attributes["TypeName"];
                        if (typeAtt != null)
                        {
                            string typeString = typeAtt.Value.Replace('#', '`');
                            try
                            {
                                //entryKey = Activator.CreateInstance("mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", typeString);
                                keyType = Type.GetType(typeString);
                            }
                            catch { }
                        }
                    }
                    else
                    {
                        keyType = TryToGetType(keyNode.Name);
                    }
                    if (keyType == null)
                        continue;
                    if (IsBasicType(keyType))
                    {
                        XmlAttribute att = keyNode.Attributes["key"];
                        if (att != null)
                        {
                            try
                            {
                                if (keyType.IsSubclassOf(typeof(Enum)))
                                    entryKey = Enum.Parse(keyType, att.Value);
                                else
                                    entryKey = Convert.ChangeType(att.Value, keyType);
                            }
                            catch { }
                        }
                    }
                    else
                    {
                        entryKey = Activator.CreateInstance(keyType, true);
                        GetValueFromXml(entryKey, keyNode);
                    }
                    XmlNode valueNode = kvNode.ChildNodes[1];
                    Type valueType = null;
                    if (valueNode.Name.Equals(Collection_Tag) || valueNode.Name.Equals(Dictionary_Tag))
                    {
                        XmlAttribute typeAtt = valueNode.Attributes["TypeName"];
                        if (typeAtt != null)
                        {
                            string typeString = typeAtt.Value.Replace('#', '`');
                            try
                            {
                                //entryValue = Activator.CreateInstance("mscorlib", typeString);
                                valueType = Type.GetType(typeString);
                            }
                            catch { }
                        }
                    }
                    else
                        valueType = TryToGetType(valueNode.Name);
                    if (valueType == null)
                        continue;
                    if (IsBasicType(valueType))
                    {
                        XmlAttribute att = valueNode.Attributes["value"];
                        if (att != null)
                        {
                            try
                            {
                                if (valueType.IsSubclassOf(typeof(Enum)))
                                    entryValue = Enum.Parse(valueType, att.Value);
                                else
                                    entryValue = Convert.ChangeType(att.Value, valueType);
                            }
                            catch { }
                        }
                    }
                    else
                    {
                        entryValue = Activator.CreateInstance(valueType, true);
                        GetValueFromXml(entryValue, valueNode);
                    }
                    dictObject.Add(entryKey, entryValue);
                }//if

            }//foreach   
        }

        void GetValueFromXml(object xmlObject, XmlNode objectNode)
        {
            GetValueFromXml(xmlObject, objectNode, null);
        }


        void GetValueFromXml(object xmlObject, XmlNode objectNode, string eleName)
        {
            if (objectNode == null || xmlObject == null)
                return;
            if (objectNode.Name.Equals(Collection_Tag)) //��ͨ����
            {
                GetCollectionFromXml(xmlObject as IList, objectNode);
            }
            else if (objectNode.Name.Equals(Dictionary_Tag)) //�ֵ�
            {
                GetDictionaryFromXml(xmlObject as IDictionary, objectNode);
            }
            else
            {
                Type objType = xmlObject.GetType();
                string tagName = eleName;
                if (string.IsNullOrEmpty(tagName))
                    tagName = objType.Name;
                if (objectNode.Name.ToLower().Equals(tagName.ToLower()))
                {
                    XmlAttributeCollection attCol = objectNode.Attributes;
                    foreach (FieldInfo field in objType.GetFields(BindingFlags.Public | BindingFlags.Instance))
                    {
                        object objValue = field.GetValue(xmlObject);
                        //�ж��ֶ��Ƿ�Ϊ��������
                        if (IsCollection(field.FieldType))
                        {
                            foreach (XmlNode node in objectNode.ChildNodes)
                            {
                                if (node.Name.Equals(field.Name))
                                {
                                    //��ʼ������
                                    object colObject = Activator.CreateInstance(field.FieldType, true);
                                    if (IsDictionary(field.FieldType))
                                    {
                                        GetDictionaryFromXml(colObject as IDictionary, node);
                                    }
                                    else
                                    {
                                        GetCollectionFromXml(colObject as IList, node);
                                    }
                                    field.SetValue(xmlObject, colObject);
                                }
                            }

                        }
                        else  //�Ǽ�������
                        {
                            if (IsBasicType(field.FieldType)) //����������
                            {
                                XmlAttribute att = attCol[field.Name];
                                if (att != null)
                                {
                                    try
                                    {
                                        if (field.FieldType.IsSubclassOf(typeof(Enum)))
                                        {
                                            object enumValue = Enum.Parse(field.FieldType, att.Value);
                                            field.SetValue(xmlObject, enumValue);
                                        }
                                        else
                                        {
                                            //ת��ΪĿ������
                                            object ovalue = Convert.ChangeType(att.Value, field.FieldType);
                                            field.SetValue(xmlObject, ovalue);
                                        }

                                    }
                                    catch { }
                                }
                            }
                            else  //�Զ�������
                            {
                                foreach (XmlNode node in objectNode.ChildNodes)
                                {
                                    if (node.Name.Equals(field.Name))
                                    {
                                        object o = Activator.CreateInstance(field.FieldType, true);
                                        GetValueFromXml(o, node, field.Name);
                                        field.SetValue(xmlObject, o);
                                        break;
                                    }
                                }
                            }
                        }//else

                    }//foreach
                }
            }//else
        }

        /// <summary>
        /// ���Ի�ȡָ��������
        /// </summary>
        /// <param name="typeName">�������ƣ������ƣ���ȫ����</param>
        /// <returns></returns>
        Type TryToGetType(string typeName)
        {
            Type t = null;
            t = Type.GetType(typeName, false);
            if (t != null)
                return t;
            foreach (Runtime r in runtimeList)
            {
                if (r.FullTypeName.EndsWith(typeName, StringComparison.CurrentCultureIgnoreCase))
                {
                    t = r.GetTypeByFullName(r.FullTypeName);
                    if (t != null)
                        return t;
                }
            }
            return t;
        }


        /// <summary>
        /// ��XML�ļ���ʼ������
        /// </summary>
        /// <param name="xmlObject">Ҫ��ʼ���Ķ���</param>
        /// <param name="xmlPath">XML�ļ�·��</param>
        /// <returns></returns>
        public bool LoadFromFile(object xmlObject, string xmlPath)
        {
            if (xmlObject == null)
                return false;
            Type objType = xmlObject.GetType();
            try
            {
                this.xmldoc.Load(xmlPath);
                this.LoadRuntimeList();
                string xmltag = "//";
                if (IsDictionary(objType))
                    xmltag += Dictionary_Tag;
                else if (IsCollection(objType))
                    xmltag += Collection_Tag;
                else
                    xmltag += objType.Name;
                XmlNode rootNode = this.xmldoc.SelectSingleNode(xmltag);
                if (rootNode != null)
                {
                    this.GetValueFromXml(xmlObject, rootNode);
                }
            }
            catch (Exception ex)
            {
                string msg = "���������ļ�ʧ�ܣ������쳣��" + ex.Message;
                throw new Exception(msg);
            }
            return true;
        }

        /// <summary>
        /// ��XML�ַ����г�ʼ������
        /// </summary>
        /// <param name="xmlObject">Ҫ��ʼ���Ķ���</param>
        /// <param name="xmlString">XML�ַ���</param>
        /// <returns></returns>
        public bool LoadFromString(object xmlObject, string xmlString)
        {
            if (string.IsNullOrEmpty(xmlString))
                return false;
            if (xmlObject == null)
                return false;
            Type objType = xmlObject.GetType();
            try
            {
                this.xmldoc.LoadXml(xmlString);
                this.LoadRuntimeList();
                string xmltag = "//";
                if (IsDictionary(objType))
                    xmltag += Dictionary_Tag;
                else if (IsCollection(objType))
                    xmltag += Collection_Tag;
                else
                    xmltag += objType.Name;
                XmlNode rootNode = this.xmldoc.SelectSingleNode(xmltag);
                if (rootNode != null)
                {
                    this.GetValueFromXml(xmlObject, rootNode);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// ���������л���XML�ļ���
        /// </summary>
        /// <param name="xmlObject">Ҫ���л��Ķ���</param>
        /// <param name="xmlPath">XML�ļ�·��</param>
        /// <returns></returns>
        public bool SaveToFile(object xmlObject, string xmlPath)
        {
            if (xmlObject == null)
                return false;
            Type objType = xmlObject.GetType();
            try
            {
                this.xmldoc = new XmlDocument();    //���¹���һ��XML�ĵ�
                this.runtimeList.Clear();
                XmlDeclaration xDeclare = this.xmldoc.CreateXmlDeclaration("1.0", "GB2312", null);
                this.xmldoc.AppendChild(xDeclare);
                this.SetValueToXml(xmlObject, null);
                SaveRuntimeList();
                this.xmldoc.Save(xmlPath);
                return true;
            }
            catch (XmlException ex)
            {
                return false;
            }
        }

        /// <summary>
        /// ���������ݸ�ʽ��Ϊ�ַ������Ա㱣�浽���ݿ���
        /// </summary>
        /// <param name="xmlObject">Ҫ���л��Ķ���</param>
        /// <returns></returns>
        public string ConvertToString(object xmlObject)
        {
            try
            {
                this.xmldoc = new XmlDocument();    //���¹���һ��XML�ĵ�
                this.runtimeList.Clear();
                XmlDeclaration xDeclare = this.xmldoc.CreateXmlDeclaration("1.0", "GB2312", null);
                this.xmldoc.AppendChild(xDeclare);
                this.SetValueToXml(xmlObject, null);
                this.SaveRuntimeList();
                return this.xmldoc.InnerXml;
            }
            catch (XmlException ex)
            {
                string msg = "ת��XML���ݸ�ʽʧ�ܣ������쳣��" + ex.Message;
                return "";
            }
        }

        /// <summary>
        /// ��������ʱ��̬����Ϣ
        /// </summary>
        void LoadRuntimeList()
        {
            this.runtimeList.Clear();
            try
            {
                XmlNode runNode = this.xmldoc.SelectSingleNode("//RuntimeList");
                if (runNode != null)
                {
                    //��ȡ��һ���ӽڵ��ֵ
                    XmlNodeList nodeList = runNode.ChildNodes;
                    if (nodeList != null && nodeList.Count > 0)
                    {
                        for (int i = 0; i < nodeList.Count; i++)
                        {
                            XmlNode node = nodeList[i];
                            if (node.Name.ToLower().Equals("runtime"))
                            {
                                Runtime r = new Runtime();
                                XmlAttributeCollection attCol = node.Attributes;
                                for (int j = 0; j < attCol.Count; j++)
                                {
                                    XmlAttribute att = attCol[j];
                                    if (att.Name.ToLower().Equals("fulltypename"))
                                        r.FullTypeName = att.Value;
                                    if (att.Name.ToLower().Equals("assemblyname"))
                                        r.AssemblyName = att.Value;
                                }
                                this.runtimeList.Add(r);
                            }
                        }
                    }
                }
            }
            catch (XmlException ex)
            {
            }
        }

        /// <summary>
        /// ��������ʱ��̬����Ϣ
        /// </summary>
        /// <param name="refNode"></param>
        void SaveRuntimeList()
        {
            try
            {
                XmlElement listEle = this.xmldoc.CreateElement("RuntimeList");
                XmlNode refNode = this.xmldoc.ChildNodes[1].ChildNodes[0];
                this.xmldoc.ChildNodes[1].InsertBefore(listEle, refNode);
                foreach (Runtime r in runtimeList)
                {
                    XmlElement runtimeEle = this.xmldoc.CreateElement("Runtime");
                    runtimeEle.SetAttribute("FullTypeName", r.FullTypeName);
                    runtimeEle.SetAttribute("AssemblyName", r.AssemblyName);
                    listEle.AppendChild(runtimeEle);
                }
            }
            catch (XmlException)
            {
            }
        }


        /// <summary>
        /// �ж������Ƿ�Ϊ������������
        /// </summary>
        /// <param name="typeName">��������</param>
        /// <returns></returns>
        bool IsBasicType(Type type)
        {
            ////����ʹ��contains�����ж��Ƿ�����ַ�����byte,int16,int32,int64�Ѱ�����sbyte,uint16,uint32,uint64��
            //string basictypes = "string|uint16|uint32|uint64|datetime|double|single|boolean|sbyte|char|decimal";
            //return basictypes.Contains(typeName.ToLower());

            if (type.IsValueType)
                return true;
            else if (type.Name.ToLower().Equals("string") || type.Name.ToLower().Equals("datetime"))
                return true;
            else
                return false;
        }

        /// <summary>
        /// �жϸ��������Ƿ��Ǽ�������
        /// </summary>
        /// <param name="type">��������</param>
        /// <returns></returns>
        bool IsCollection(Type type)
        {
            if (type == null)
                return false;
            Type[] inters = type.GetInterfaces();
            foreach (Type t in inters)
            {
                if (t.Name.Equals(typeof(ICollection).Name))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// �жϸ��������Ƿ�Ϊ�ֵ�����
        /// </summary>
        /// <param name="type">��������</param>
        /// <returns></returns>
        bool IsDictionary(Type type)
        {
            if (type == null)
                return false;
            Type[] inters = type.GetInterfaces();
            foreach (Type t in inters)
            {
                if (t.Name.Equals(typeof(IDictionary).Name))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
